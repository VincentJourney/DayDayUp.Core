using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autofac_MediatR
{
    /// <summary>
    /// 发布策略
    /// </summary>
    public enum PublishStrategy
    {
        /// <summary>
        /// 同步发布
        /// 当所有处理程序完成时返回
        /// 遇到异常后继续执行，Exception is AggregateException
        /// </summary>
        SyncContinueOnException = 0,

        /// <summary>
        /// 同步发布
        /// 当所有处理程序完成时或异常时返回
        /// 遇到异常后停止执行，Exception is AggregateException
        /// </summary>
        SyncStopOnException = 1,

        /// <summary>
        /// 异步发布
        /// 当所有处理程序完成时返回
        /// 遇到异常后继续消费后续消息
        /// Exception is AggregateException
        /// </summary>
        Async = 2,

        /// <summary>
        /// 异步发布
        /// 无等待
        /// 无异常
        /// </summary>
        ParallelNoWait = 3,

        /// <summary>
        /// 异步发布
        /// 等待所有消息消费完成
        /// 消费所有消息后，抛出异常
        /// </summary>
        ParallelWhenAll = 4,

        /// <summary>
        /// 异步发布
        /// 等待直到某一消息消费完成
        /// 无异常
        /// </summary>
        ParallelWhenAny = 5,
    }
}
