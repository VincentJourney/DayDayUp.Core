using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskUnitTest
{
    public class TaskExtensions
    {
        public static void TaskRunWait(Action ac, int threadNum = 10, bool needWait = true)
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
        public static List<T> Pagination<T>(List<T> list)
        {
            if (list.Any())
                return default;

            lock (lockObj) pageIndex++;
            var startIndex = pageIndex * pageSize;
            var endIndex = (pageIndex + 1) * pageSize;
            return list.Skip(startIndex).Take(pageSize).ToList();
        }
        private static int pageSize = 20;
        private static int pageIndex = 0;
        private static readonly object lockObj = new object();
    }
}
