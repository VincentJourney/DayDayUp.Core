using DesignPattern;
using DesignPattern.Command;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

using TaskUnitTest;

namespace MainConsole
{
    class Program
    {
        public static Expression<Func<TEntity, bool>> ExpresstionExtension<TEntity, TParams>(string entityProportyName, IEnumerable<TParams> dataSource, int pageSize = 1000)
        {
            var param = dataSource.FirstOrDefault();
            ParameterExpression entityExpression = Expression.Parameter(typeof(TEntity), nameof(TEntity));
            MemberExpression entityProp = Expression.Property(entityExpression, typeof(TEntity).GetProperty(entityProportyName));
            var paramExpression = Expression.Constant(param);
            var equalMethod = Expression.Equal(entityProp, paramExpression);
            var expression = Expression.Lambda<Func<TEntity, bool>>(equalMethod, new ParameterExpression[] { entityExpression });
            return expression;
        }
        /// <summary>
        /// WhereBetween表达式
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <typeparam name="TParams">参数类型</typeparam>
        /// <param name="propName">参数名称</param>
        /// <param name="source">数据源</param>
        /// <returns></returns>
        private static Expression<Func<TEntity, bool>> WhereBetweenExpression<TEntity, TParams>(string propName, TParams from, TParams to)
        {
            ParameterExpression entityExpression = Expression.Parameter(typeof(TEntity), nameof(TEntity));
            var property = typeof(TEntity).GetProperty(propName);
            MemberExpression entityProp = Expression.Property(entityExpression, property);
            var paramFromExpression = Expression.Convert(Expression.Constant(from), property.PropertyType);
            var paramToExpression = Expression.Convert(Expression.Constant(to), property.PropertyType);
            var greaterThanOrMethod = Expression.GreaterThanOrEqual(entityProp, paramFromExpression);
            var lessMethod = Expression.LessThan(entityProp, paramToExpression);
            var exp = Expression.And(greaterThanOrMethod, lessMethod);
            var expression = Expression.Lambda<Func<TEntity, bool>>(exp, new ParameterExpression[] { entityExpression });
            return expression;
        }

        /// <summary>
        /// 扩展 Between 操作符
        /// 使用 var query = db.People.Between(person => person.Age, 18, 21);
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <param name="low"></param>
        /// <param name="high"></param>
        /// <returns></returns>
        public static Expression<Func<TSource, bool>> Between<TSource, TKey>(
                Expression<Func<TSource, TKey>> keySelector,
                TKey low,
                TKey high)
        {
            Expression key = Expression.Invoke(keySelector, keySelector.Parameters.ToArray());
            Expression lowerBound = Expression.GreaterThanOrEqual(key, Expression.Convert(Expression.Constant(low), typeof(TKey)));
            Expression upperBound = Expression.LessThanOrEqual(key, Expression.Convert(Expression.Constant(high), typeof(TKey)));
            Expression and = Expression.AndAlso(lowerBound, upperBound);
            Expression<Func<TSource, bool>> lambda = Expression.Lambda<Func<TSource, bool>>(and, keySelector.Parameters);
            return lambda;
        }

        /// <summary>
        /// WhereContains表达式
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <typeparam name="TParams">数据源类型</typeparam>
        /// <param name="propName">属性名称</param>
        /// <param name="source">数据源</param>
        /// <returns></returns>
        private static Expression<Func<TEntity, bool>> WhereContainsExpression<TEntity, TParams>(string propName, IEnumerable<TParams> source)
        {
            //entity.prop
            ParameterExpression entityExpression = Expression.Parameter(typeof(TEntity), nameof(TEntity));
            MemberExpression entityProp = Expression.Property(entityExpression, typeof(TEntity).GetProperty(propName));

            //source
            var dataSource = Expression.Constant(source);

            //source.Contains
            var containsMethod = typeof(Enumerable).GetMethods()
                .FirstOrDefault(info => info.GetParameters().Length == 2 && info.Name == nameof(Enumerable.Contains))
                .MakeGenericMethod(typeof(TParams));

            //Enumerable.Contains(source,entity.prop)
            var containsCall = Expression.Call(null, containsMethod, dataSource, entityProp);

            //entity => source.Contains(name)
            var expression = Expression.Lambda<Func<TEntity, bool>>(containsCall, new ParameterExpression[] { entityExpression });

            return expression;
        }

        private static Expression<Func<TEntity, bool>> WhereContainsExpression<TEntity, TParams>(Expression<Func<TEntity, TParams>> keySelector, IEnumerable<TParams> source)
        {
            //entity.prop
            Expression key = Expression.Invoke(keySelector, keySelector.Parameters.ToArray());
            //source
            var dataSource = Expression.Constant(source);

            //source.Contains
            var containsMethod = typeof(Enumerable).GetMethods()
                .FirstOrDefault(info => info.GetParameters().Length == 2 && info.Name == nameof(Enumerable.Contains))
                .MakeGenericMethod(typeof(TParams));

            //Enumerable.Contains(source,entity.prop)
            var containsCall = Expression.Call(null, containsMethod, dataSource, key);

            //entity => source.Contains(name)
            var expression = Expression.Lambda<Func<TEntity, bool>>(containsCall, keySelector.Parameters);

            return expression;
        }

        /// <summary>
        /// WhereBetween表达式
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <typeparam name="TParams">参数类型</typeparam>
        /// <param name="from">起始</param>
        /// <param name="to">结束</param>
        /// <returns></returns>
        private static Expression<Func<TEntity, bool>> WhereBetweenExpression<TEntity, TParams>(Expression<Func<TEntity, TParams>> keySelector, TParams from, TParams to)
        {
            Expression key = Expression.Invoke(keySelector, keySelector.Parameters.ToArray());
            var paramFromExpression = Expression.Constant(from);
            var paramToExpression = Expression.Constant(to);
            var greaterThanOrMethod = Expression.GreaterThanOrEqual(key, paramFromExpression);
            var lessMethod = Expression.LessThan(key, paramToExpression);
            var exp = Expression.AndAlso(greaterThanOrMethod, lessMethod);
            var expression = Expression.Lambda<Func<TEntity, bool>>(exp, keySelector.Parameters);
            return expression;
        }

        /// <summary>
        /// 通过反射给新的对象同名字段赋值 （用于自定义Model反射给数据库Model 需注意 int16跟int32的转换）
        /// </summary>
        /// <typeparam name="TSource">源对象类型</typeparam>
        /// <typeparam name="TResult">目标对象类型</typeparam>
        /// <param name="source">源对象</param>
        /// <param name="toResult">目标对象</param>
        public static TResult ReflexEntity<TSource, TResult>(TSource source, TResult toResult)
        {
            if (ReferenceEquals(source, null))
                throw new ArgumentNullException(nameof(source));

            var sType = source.GetType();
            var tType = toResult.GetType();
            foreach (PropertyInfo sourceProp in sType.GetProperties())
            {
                foreach (PropertyInfo toProp in tType.GetProperties())
                {
                    if (toProp.Name.Replace("_", "").ToUpper() == sourceProp.Name.Replace("_", "").ToUpper())
                    {
                        var value = sourceProp.GetValue(source, null);
                        if (sourceProp.PropertyType.FullName.Contains("System.Int"))
                        {//此处判断下Int32类型，如果是则强转

                            if (toProp.PropertyType.FullName.Contains("System.Int16"))
                            {
                                value = Convert.ToInt16(value);
                            }
                            else if (toProp.PropertyType.FullName.Contains("System.Int32"))
                            {
                                value = Convert.ToInt32(value);
                            }
                            else if (toProp.PropertyType.FullName.Contains("System.Int64"))
                            {

                                value = Convert.ToInt64(value);
                            }
                        }
                        toProp.SetValue(toResult, value, null);
                        break;
                    }
                    PropertyInfo targetPP = tType.GetProperty(sourceProp.Name.Replace("_", "").ToUpper());

                }
            }
            return toResult;
        }

        class A1
        {
            public string name { get; set; }
            public int? num { get; set; }
            public A2 aaa { get; set; }
        }

        class A2
        {
            public string name { get; set; }
            public int? num { get; set; }
            public A2? aaa { get; set; }
        }

        static async Task Main(string[] args)
        {

            var a = new A1 { name = "1", num = null, aaa = new A2 { name = "1" } };
            var a2 = new A2();

            var a1 = JsonConvert.SerializeObject(a);
            a2 = JsonConvert.DeserializeObject<A2>(a1);
            var a123 = a2;





            //Console.WriteLine($"1,当前线程 {Thread.CurrentThread.ManagedThreadId}");
            //await TestAsync();
            //Console.WriteLine($"2,当前线程  {Thread.CurrentThread.ManagedThreadId}");

            Console.ReadKey();

            #region 中间件模式，解析管道模型
            // DesignPattern.MiddleWarePattern.UseMiddleWare.MiddleWareAnalysis();
            //    DesignPattern.MiddleWarePattern.UseMiddleWare.BuildUse();
            #endregion

            var sku = "SZ-AH24-I324511";
            var index = sku.LastIndexOf('-') + 1;
            string productCode = sku.Substring(index);
            Console.WriteLine(productCode);



            Console.ReadLine();
        }

    }
}
