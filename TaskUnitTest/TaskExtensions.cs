using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskUnitTest
{
    public class TaskExtensions
    {
        public TaskExtensions(int _pageSize)
        {
            pageSize = _pageSize;
        }
        public static void TaskRunWait(Action ac, int threadNum = 10, bool needWait = true)
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
        public List<T> GetByPagination<T>(List<T> list)
        {
            lock (lockObj)
            {
                var s = pageIndex * pageSize;
                pageIndex++;
                return list.Skip(s).Take(pageSize).ToList();
            }
        }
        private int pageSize;
        private int pageIndex = 0;
        private static readonly object lockObj = new object();
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
