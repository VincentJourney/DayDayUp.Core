using DesignPattern;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
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

            {
                //var studentA = new StudentA();
                //studentA.DoInDay();
                //var studentB = new StudentB();
                //studentB.DoInDay();
            }

            var list = new List<int>();
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            Parallel.For(0, 10000, i =>
            {
                list.Add(i);
            });
            stopWatch.Stop();

            Console.WriteLine(stopWatch.Elapsed.TotalSeconds);
            Console.ReadLine();
            var modelS = new List<InventoryReviseSettingModel>
            {
                new InventoryReviseSettingModel{
                    Sku="1",
                    SaleSite="1",
                   SellerAccount="1",
                   ExtendedCondition="1",
                   Platform="1"
                },
                new InventoryReviseSettingModel{
                    Sku="2",
                    SaleSite="2",
                   SellerAccount="2",
                   ExtendedCondition="2",
                   Platform="2"
                }
            };
            var newModel = GetExportList(modelS);
            foreach (var item in newModel)
            {
                var a = item.GetType().GetProperties();
                foreach (var item2 in a)
                {
                    Console.WriteLine(item2.Name);
                }

            }

            Console.ReadLine();
        }

        public static List<AbstractInventoryReviseSettingExportModel> GetExportList(List<InventoryReviseSettingModel> models)
        {
            return models.Select(InventoryReviseSettingForSiteExportModel.Selector).ToList();


        }
    }


}
