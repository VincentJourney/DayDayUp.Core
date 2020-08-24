using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TaskUnitTest
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskOptimizeByPaging
    {
        public TaskOptimizeByPaging(int _pageSize) => pageSize = _pageSize;

        private static readonly object lockObj = new object();
        private int pageSize;
        private int pageIndex = 0;

        public void TaskRunWait(Action ac, int threadNum = 10, bool needWait = true)
        {
            var taskList = new List<Task>();
            for (int i = 0; i < threadNum; i++)
            {
                var task = Task.Run(() => ac());
                taskList.Add(task);
            }
            if (needWait)
                Task.WaitAll(taskList.ToArray());
        }
        public void TaskRunWaitByPaging<T>(IEnumerable<T> ts, Action<T> ac, int threadNum = 10, bool needWait = true)
        {
            var taskList = new List<Task>();
            for (int i = 0; i < threadNum; i++)
            {
                var task = Task.Run(() =>
                {
                    var newTs = GetListByPagination(ts);
                    while (newTs.Count() > 0)
                    {
                        foreach (var item in newTs)
                            ac(item);
                        newTs = GetListByPagination(ts);
                    }
                });
                taskList.Add(task);
            }
            if (needWait)
                Task.WaitAll(taskList.ToArray());
        }
        public IEnumerable<T> GetListByPagination<T>(IEnumerable<T> list)
        {
            lock (lockObj)
            {
                var s = pageIndex * pageSize;
                pageIndex++;
                return list.Skip(s).Take(pageSize);
            }
        }
        public static void StopWatchAction(Action ac)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            ac();
            stopwatch.Stop();
            Console.WriteLine($"花费时间 {stopwatch.Elapsed.TotalSeconds}");
        }
    }
}
