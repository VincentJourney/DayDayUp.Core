using DesignPattern;

using Newtonsoft.Json;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
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

            //var stus = new Student();
            //Console.WriteLine(stus.GetHashCode());
            //Console.WriteLine(Student._dic.GetHashCode());
            //stus.Show();
            //var stus2 = new Student();
            //Console.WriteLine(stus2.GetHashCode());
            //Console.WriteLine(Student._dic.GetHashCode());
            //stus2.Show();

            Parallel.For(0, 3, i =>
            {
                var stus = new Student();
                Console.WriteLine(stus.GetHashCode());
                Console.WriteLine(Student._dic.GetHashCode());
                stus.Show();
            });


            //log();
            log2();
            Console.ReadLine();
        }

        public static void log()
        {
            try
            {
                throw new Exception("异常");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //MethodBase mb = MethodBase.GetCurrentMethod();
            //string systemModule = Environment.NewLine;
            //systemModule += "模块名:" + mb.Module.ToString() + Environment.NewLine;
            //systemModule += "命名空间名:" + mb.ReflectedType.Namespace + Environment.NewLine;
            ////完全限定名，包括命名空间
            //systemModule += "类名:" + mb.ReflectedType.FullName + Environment.NewLine;
            //systemModule += "方法名:" + mb.Name;
            //Console.WriteLine(systemModule);
        }

        public static void log2()
        {
            try
            {
                throw new Exception("异常");
            }
            catch (Exception ex)
            {
                throw new Exception("123", ex);
            }

            //StackTrace ss = new StackTrace(true);
            ////index:0为本身的方法；1为调用方法；2为其上上层，依次类推
            //MethodBase mb = ss.GetFrame(1).GetMethod();

            //StackFrame[] sfs = ss.GetFrames();
            //string systemModule = Environment.NewLine;
            //systemModule += "模块名:" + mb.Module.ToString() + Environment.NewLine;
            //systemModule += "命名空间名:" + mb.DeclaringType.Namespace + Environment.NewLine;
            ////仅有类名
            //systemModule += "类名:" + mb.DeclaringType.Name + Environment.NewLine;
            //systemModule += "方法名:" + mb.Name;
            //Console.WriteLine(systemModule);
        }


        public class Student
        {
            public static Dictionary<int, int> _dic = new Dictionary<int, int>();
            static Student()
            {
                var a = new Random().Next(0, 999);
                _dic.Add(a, a);
            }
            public void Show()
            {
                Console.WriteLine(_dic.Keys.Sum());
            }
            public string Name { get; set; }
            public int Id { get; set; }
            public int Monney { get; set; }
        }
    }
}
