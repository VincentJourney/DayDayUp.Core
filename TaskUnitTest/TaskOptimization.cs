using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace TaskUnitTest
{
    public class TaskOptimization
    {
        public void Main()
        {
            var list = new List<Student>();
            for (int i = 0; i < 10000; i++)
            {
                list.Add(new Student { a = i.ToString(), b = true });
            }
            TaskListCopy(list);
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

        private static void TaskListCopy(List<Student> list)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var pageList = new ConcurrentBag<Student>();
            TaskExtensions.TaskRunWait(() =>
            {
                var alist = TaskExtensions.Pagination(list);
                while (alist.Count > 0)
                {
                    foreach (var item in alist)
                    {
                        #region sql操作
                        pageList.Add(item);
                        Thread.Sleep(5);
                        #endregion
                    }
                    alist = TaskExtensions.Pagination(list);
                }
            }, needWait: false);
            Console.WriteLine("--" + pageList.Distinct().Count());
            stopwatch.Stop();
            Console.WriteLine("ListCopyAsync" + stopwatch.Elapsed.TotalSeconds);
        }

        public class Student
        {
            public string a { get; set; }
            public bool b { get; set; }
        }
    }
}
