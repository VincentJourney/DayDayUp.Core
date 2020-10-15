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
            //DesignPattern.MiddleWarePattern.UseMiddleWare.BuildUse();
            #endregion

            //decimal aa = decimal.Round(4M / 90, 2);
            //Console.WriteLine(aa);
            //int.TryParse("  asdfa ", out var number);
            //Console.WriteLine(number);

            var str = " *1231*231";
            str = null;
            var ints = str.Trim().IndexOf("*");
            Console.WriteLine(ints);
            EnumExtension.GetRuleInfos();

            Ck(null);

            var R01 = MainImageRuleEnum.R01;
            var R02 = MainImageRuleEnum.R02;
            var asd = R01.GetRemark().Detail;
            var asd2 = R02.GetRemark().Detail;


            var dic = new Dictionary<Dictionary<string, int>, int>();
            dic.Add(new Dictionary<string, int> { { "MY", 1 } }, 1);
            dic.Add(new Dictionary<string, int> { { "MY", 1 } }, 2);
            dic.Add(new Dictionary<string, int> { { "MY", 1 } }, 3);
            dic.Add(new Dictionary<string, int> { { "MY", 1 } }, 4);

            var s = dic;
            Console.ReadKey();
        }

        public static void Ck([NotNull] Student stu)
        {

        }
        public static void Ck1([NotNull] Student stu)
        {
            //  Check.NotNull(stu, nameof(stu));
        }
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
