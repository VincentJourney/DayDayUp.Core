using DesignPattern;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MainConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 中间件模式，解析管道模型
            DesignPattern.MiddleWarePattern.UseMiddleWare.MiddleWareAnalysis();
            DesignPattern.MiddleWarePattern.UseMiddleWare.BuildUse();
            #endregion

        }
    }


}
