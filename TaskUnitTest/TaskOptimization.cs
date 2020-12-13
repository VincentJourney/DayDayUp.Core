using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System.Runtime.InteropServices;
using System.Collections;

namespace TaskUnitTest
{
    public class TaskOptimization
    {
        public void Main()
        {
            var list = new List<Student>();
            var list2 = new List<int>();
            for (int i = 0; i < 10000; i++)
            {
                var stu = new Student { a = i, b = true };
                list.Add(stu);
            }
            // Console.WriteLine($"list个数： {list.Distinct().Count()} ");
            for (int i = 0; i < 5000; i++)
            {
                list2.Add(i);
            }
            //Console.WriteLine($"list2个数： {list2.Distinct().Count()} ");
            Stopwatch s = new Stopwatch();
            s.Start();
            TaskListCopy2(list2);
            var s1 = s.Elapsed.TotalSeconds;
            Console.WriteLine(s1);
            Thread.Sleep(1000);
            var s2 = s.Elapsed.TotalSeconds;
            Console.WriteLine(s2);
            Console.ReadKey();
        }
        private void ListCopy(List<Student> list, List<int> list2)
        {
            TaskOptimizeByPaging.StopWatchAction(() =>
            {
                var copylist = new List<Student>();
                var copylist2 = new List<int>();
                foreach (var item in list)
                {
                    #region sql操作
                    copylist.Add(item);
                    Thread.Sleep(1);
                    foreach (var item2 in list2)
                    {
                        copylist2.Add(item2);
                        Thread.Sleep(1);
                    }
                    #endregion
                }
                Console.WriteLine($"原集合中个数：{list.Count} ，ListCopyAfter个数： {copylist.Count()} ");
            });
        }
        private void TaskListCopy(List<Student> list, List<int> list2)
        {
            TaskOptimizeByPaging.StopWatchAction(() =>
            {
                var pageList = new List<int>();
                var taskr = new TaskOptimizeByPaging(20);
                var hs = new Hashtable();
                var token = new CancellationTokenSource();
                taskr.TaskRunWaitByPaging(list2,
                   s =>
                   {
                       hs.Add(s, "");
                       pageList.Add(s);
                       // Console.WriteLine(s);
                   }, token, null, 2
               );
                Console.WriteLine(@$"list原集合中个数：{list2.Count()}
TaskListCopy个数： {pageList.Distinct().Count()} ");
                //TaskListCopy个数： {pageList2.Distinct().Count()} ");
                Console.WriteLine(@$"hs2中个数：{hs.Count}");

            });
        }

        private void TaskListCopy2(List<int> list2)
        {

            var pageList = new List<int>();
            var x = new ConcurrentBag<int>();
            var taskr = new TaskOptimizeByPaging(20);
            var taskList = new List<Task>();
            for (int i = 0; i < 2; i++)
            {
                var task = Task.Run(() =>
                {
                    var newTs = taskr.GetListByPagination(list2);
                    while (newTs.Count() > 0)
                    {
                        foreach (var item in newTs)
                        {

                        }
                        newTs = taskr.GetListByPagination(list2);
                    }
                });
                taskList.Add(task);
            }
            Task.WaitAll(taskList.ToArray());

            Console.WriteLine(@$"list原集合中个数：{list2.Count()}
TaskListCopy个数： {pageList.Distinct().Count()} ");
            //Console.WriteLine(@$"hs中个数：{hs.Count}");
            Console.WriteLine(@$"x中个数：{x.Count()}");


        }

        public class Student
        {
            public int a { get; set; }
            public bool b { get; set; }
        }
    }
}
