using DesignPattern;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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

            var list = new List<Student>();
            for (int i = 0; i < 10000; i++)
            {
                list.Add(new Student { a = i.ToString(), b = true });
            }
            ListCopyAsync(list);
            //ListCopy(list);
            Console.ReadLine();
            //list.ForEach(s => s.b = false);
            //list.ForEach(s => Console.WriteLine(s.b));
        }
        private static void ListCopy(List<Student> list)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var copylist = new List<Student>();
            foreach (var item in list)
            {
                #region sql操作
                item.b = false;
                copylist.Add(item);
                Thread.Sleep(5);
                #endregion
            }
            stopwatch.Stop();
            Console.WriteLine("ListCopy" + stopwatch.Elapsed.TotalSeconds);
            Console.WriteLine("ListCopy   " + copylist.Count());
        }
        private static void ListCopyAsync(List<Student> list)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var pageList = new ConcurrentBag<Student>();
            TaskRunWait(() =>
            {
                var alist = Pagination(list);
                while (alist.Count > 0)
                {
                    foreach (var item in alist)
                    {
                        #region sql操作
                        pageList.Add(item);
                        Thread.Sleep(5);
                        #endregion
                    }
                    alist = Pagination(list);
                }
            });
            Console.WriteLine("--" + pageList.Distinct().Count());
            stopwatch.Stop();
            Console.WriteLine("ListCopyAsync" + stopwatch.Elapsed.TotalSeconds);
        }
        private static List<Student> Pagination(List<Student> list)
        {
            lock (lockObj)
            {
                pageIndex++;
                return list.Skip(pageIndex * pageSize).Take((pageIndex + 1) * pageSize).ToList();
            }
        }
        private static int pageSize = 20;
        private static int pageIndex = 0;
        private static readonly object lockObj = new object();

        private static void TaskRunWait(Action ac, int threadNum = 10, bool needWait = true)
        {
            List<Task> taskList = new List<Task>();
            for (int i = 0; i < threadNum; i++)
            {
                var task = Task.Run(() => ac());
                taskList.Add(task);
            }
            if (needWait)
                Task.WaitAll(taskList.ToArray());
        }
    }

    public class Student
    {
        public string a { get; set; }
        public bool b { get; set; }
    }


}
