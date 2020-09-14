using DesignPattern;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            //   DesignPattern.MiddleWarePattern.UseMiddleWare.BuildUse();
            #endregion


            var stus = new List<Student>
            {
                new Student{a="1",c=1 },
                new Student{a="2",c=2 },
                new Student{a="3",b=false,c=3 },

            };
            var stu2 = new List<Student>();

            stus.AddRange(stu2);

            var R01 = MainImageRuleEnum.R01;
            var R02 = MainImageRuleEnum.R02;
            var asd = R01.GetRemark().Detail;
            var asd2 = R02.GetRemark().Detail;
            Console.ReadKey();
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
    }

    public class RuleInfo
    {
        public string Title { get; set; }
        public string Detail { get; set; }
        public int Id { get; set; }
    }


}
