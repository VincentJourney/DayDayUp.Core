using DesignPattern;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using TaskUnitTest;

namespace MainConsole
{
    class Program
    {
        static void Main(string[] args)
        {


            #region 中间件模式，解析管道模型
            // DesignPattern.MiddleWarePattern.UseMiddleWare.MiddleWareAnalysis();
            DesignPattern.MiddleWarePattern.UseMiddleWare.BuildUse();
            #endregion

            var result = EnumExtension.GetRuleInfos();
            var result1 = result.Where(s => s.Id == 1).Select(s => s.Title).OrderByDescending(s => s).ToList();
            MainImageRuleEnum.R01.GetRemark();

            B b = new B();
            A a = new A();
            a.Fun2(b);
            b.Fun2(a);

            Console.ReadKey();

        }
        private static int Lindexi()
        {
            return 2;
        }

        class Foo
        {
            public int N
            {
                get
                {
                    Console.WriteLine("Hi.");
                    return 1;
                }
            }
        }
    }

    public class B : A
    {
        public B() { count++; }

        public override void Fun1(int i)
        {
            base.Fun1(i + count);
        }

    }
    public class A
    {

        protected static int count = 1;
        static A()
        {
            count++;
        }

        public virtual void Fun1(int i) { Console.WriteLine(i); }
        public void Fun2(A a) { a.Fun1(5); Fun1(2); }
    }

    public class Student
    {
        private int _c;
        public string a { get; set; }
        public bool b { get; set; } = true;
        public int c
        {
            get => _c;
            set
            {
                if (b)
                {
                    _c = value > 1 ? value = 0 : value;
                }
                else
                {
                    _c = value;
                }
            }
        }
    }

    public enum MainImageRuleEnum
    {
        [Remark("规则1", "具体规则1")]
        R01,
        [Remark("规则2", "具体规则2")]
        R02,
    }
    public class RemarkAttribute : Attribute
    {
        private string _title;
        private string _detail;
        public RemarkAttribute(string title, string detail)
        {
            _title = title;
            _detail = detail;
        }
        public string Title
        {
            get => _title;
            set => _title = value;
        }
        public string Detail
        {
            get => _detail;
            set => _detail = value;
        }
    }

    /// <summary>
    /// 枚举扩展类
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// 获取枚举的备注信息
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        public static RuleInfo GetRemark(this Enum em)
        {
            Type type = em.GetType();
            System.Reflection.FieldInfo fd = type.GetField(em.ToString());
            if (fd == null)
                return null;
            var asd = (int)fd.GetValue(em);

            var attrs = fd.GetCustomAttributes(typeof(RemarkAttribute), false).FirstOrDefault();
            var a = attrs as RemarkAttribute;
            return new RuleInfo
            {
                Detail = a.Detail,
                Title = a.Title
            };
        }

        public static List<RuleInfo> GetRuleInfos()
        {
            var result = new List<RuleInfo>();
            var type = typeof(MainImageRuleEnum);
            var fieldInfos = type.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.GetField);
            foreach (var field in fieldInfos)
            {
                var value = field.GetValue(null);
                var valueEnum = (MainImageRuleEnum)value;
                var detail = valueEnum.GetRemark();
                result.Add(detail);
            }
            return result;
        }
    }

    public class RuleInfo
    {
        public string Title { get; set; }
        public string Detail { get; set; }
        public int Id { get; set; }
    }


}
