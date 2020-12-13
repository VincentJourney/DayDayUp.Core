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
            DesignPattern.MiddleWarePattern.UseMiddleWare.BuildUse();
            #endregion

            //  new TaskOptimization().Main();
            Console.ReadKey();
        }
    }

    public class Student
    {
        public string a { get; set; }
        public bool b { get; set; }
    }


}
