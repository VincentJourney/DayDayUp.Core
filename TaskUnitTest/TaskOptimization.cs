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
            //ListCopy(list);
            Console.ReadKey();
        }
        private void ListCopy(List<Student> list)
        {
            TaskExtensions.StopWatchAction(() =>
            {
                var copylist = new List<Student>();
                foreach (var item in list)
                {
                    #region sql操作
                    copylist.Add(item); Thread.Sleep(5);
                    #endregion
                }
                Console.WriteLine($"原集合中个数：{list.Count} ，ListCopyAfter个数： {copylist.Count()} ");
            });
        }
        private void TaskListCopy(List<Student> list)
        {
            TaskExtensions.StopWatchAction(() =>
            {
                var pageList = new ConcurrentBag<Student>();
                for (int i = 0; i < 5; i++)
                {
                    TaskExtensions.TaskRunWait(() =>
                    {
                        var newList = list.GetByPagination();
                        while (newList.Count() > 0)
                        {
                            foreach (var item in newList)
                            {
                                #region sql操作
                                pageList.Add(item); Thread.Sleep(5);
                                #endregion
                            }
                            newList = list.GetByPagination();
                        }
                    });

                }

                Console.WriteLine($"原集合中个数：{list.Count} ，TaskListCopy个数： {pageList.Distinct().Count()} ");
            });
        }

        public class Student
        {
            public string a { get; set; }
            public bool b { get; set; }
        }
    }
}
