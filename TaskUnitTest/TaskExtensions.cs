using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
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
        public void TaskRunWaitByPaging<T>(IEnumerable<T> collection, Action<T> action, CancellationTokenSource cancellationToken
            , Func<T, bool> condition = null,
            int threadNum = 5, bool needWait = true)
        {
            #region 校验

            if (action == null)
                return;

            if (collection == null || !collection.Any())
                return;

            if (cancellationToken == null)
                throw new ArgumentException("CancellationTokenSource is Null");

            var count = collection.Count();
            if (count == 1)
                threadNum = 1;
            else if (count <= 20)
                threadNum = 2;
            else if (count <= 50)
                threadNum = 3;
            else if (count <= 100)
                threadNum = 4;
            else
                threadNum = 5;

            #endregion

            var taskList = new List<Task>();
            for (int i = 0; i < threadNum; i++)
            {
                var task = Task.Run(() =>
                {
                    var newTs = GetListByPagination(collection);
                    while (newTs.Count() > 0)
                    {
                        foreach (var item in newTs)
                        {
                            cancellationToken.Token.ThrowIfCancellationRequested();

                            if (condition?.Invoke(item) ?? false)
                                break;
                            action(item);
                        }
                        newTs = GetListByPagination(collection);
                    }
                }, cancellationToken.Token);
                taskList.Add(task);
            }

            if (needWait)
            {
                try
                {
                    Task.WaitAll(taskList.ToArray(), cancellationToken.Token);
                }
                catch (OperationCanceledException ex)
                {
                    Console.WriteLine(ex.Message + ex.InnerException?.Message);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public IEnumerable<T> GetListByPagination<T>(IEnumerable<T> list)
        {
            lock (lockObj)
            {
                var s = list.Skip(pageIndex * pageSize).Take(pageSize);
                pageIndex++;
                return s;
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
