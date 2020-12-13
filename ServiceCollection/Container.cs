using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ServiceCollection
{
    public class Container
    {
        /// <summary>
		/// 缓存
		/// </summary>
		private static Dictionary<string, Type> dic = new Dictionary<string, Type>();

        /// <summary>
        /// 注册类型
        /// </summary>
        /// <typeparam name="IT">接口类型</typeparam>
        /// <typeparam name="T">实例类型</typeparam>
        public static void RegisterType<IT, T>()
        {
            dic.Add(typeof(IT).FullName, typeof(T));
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <typeparam name="T">需构造的类型</typeparam>
        /// <returns></returns>
        public T Resolve<T>()
        {
            var targetKey = typeof(T).FullName;
            if (dic.ContainsKey(targetKey))
            {
                Type targetType = dic[targetKey];//需要创建的类型
                return (T)this.Create(targetType);
            }
            else
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// 递归构造函数注入
        /// </summary>
        /// <param name="targetType">需构造的类型</param>
        /// <returns></returns>
        private object Create(Type targetType)
        {
            ConstructorInfo conInfo = targetType.GetConstructors().OrderByDescending(s => s.GetParameters().Length).FirstOrDefault();//获取构造函数参数最多的
            ParameterInfo[] paramArray = conInfo.GetParameters();//构造函数的参数
            if (paramArray.Length == 0)
            {
                return Activator.CreateInstance(targetType);
            }

            List<object> list = new List<object>();//需要构造的实例的所有构造函数集合
            foreach (var para in paramArray)
            {
                var paraType = para.ParameterType.FullName;//构造函数参数类型
                if (dic.ContainsKey(paraType))
                {
                    Type paraTatgetType = dic[paraType];//需要构造的参数类型注册的类型
                    list.Add(this.Create(paraTatgetType));
                }
                else
                {
                    throw new Exception();
                }
            }
            return Activator.CreateInstance(targetType, list.ToArray());
        }
    }
}
