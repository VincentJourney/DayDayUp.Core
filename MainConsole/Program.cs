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
            // DesignPattern.MiddleWarePattern.UseMiddleWare.BuildUse();
            #endregion

            var strrr = @"<b>* SODIAL is a registered trademark. ONLY Authorized seller of SODIAL can sell under SODIAL listings.Our products will enhance your experience to unparalleled inspiration.</b> <br />SODIAL(R) 3M Solar Panel Extension Cable 10 AWG PV Wire Male Female Connector<br />Connector:<br />Maximum working current : 25A (4mm2)<br />Maximum working voltage : DC1000V<br />Contact resistance : less than equal 0.5mΩ<br />Insulation resistance :less than 500MΩ<br />single-core cable cross-section : 6mm2<br />Ambient temperature : -40 ℃ ~ + 105 ℃<br />Protection class: IP67<br />Safety class : Ⅱ<br />Pollution degree : 2<br />Operating temperature : -40 ~ +85 ℃<br />Insertion force : less than equal 50N<br />Withdrawal force : greater than equal 50N<br />Insulation Material : PC, black<br />Contact : Copper cn, sn Nickel<br />Flammability rating : UL94-VO<br />Terminal Connection: Crimp<br />Cable:<br />Nominal cross section : 6.0mm2<br />Rated Current: 70A<br />Rated Voltage:600V/1800V (AC/ DC)<br />Ambient Temperature Range:-40℃ ~ +125℃<br />Strand Design(No.x？(mm)):84 /0.30<br />Conductor diameter: 3.42mm<br />Outer diameter:7.15mm<br />Available Cable Length:3M<br />Package Contents:<br />1 x 10 AWG Solar Extension Cable With Connectors Male And Female<br />Note:Light shooting and different displays may cause the color of the item in the picture a little different from the real thing.The measurement allowed error is +/- 1-3cm.
";
            var separator = ":";
            var s = strrr.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in s)
            {
                var ass = item.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            }
            short? aa = 1;

            int? bb = null;
            aa = (short?)bb;

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
