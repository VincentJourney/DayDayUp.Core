using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Autofac_MediatR
{
    public class AsyncPublisher
    {
        private readonly ServiceFactory _serviceFactory;

        public AsyncPublisher(ServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;

            _publishStrategies[PublishStrategy.Async] = new CustomMediator(_serviceFactory, AsyncContinueOnException);
            _publishStrategies[PublishStrategy.ParallelNoWait] = new CustomMediator(_serviceFactory, ParallelNoWait);
            _publishStrategies[PublishStrategy.ParallelWhenAll] = new CustomMediator(_serviceFactory, ParallelWhenAll);
            _publishStrategies[PublishStrategy.ParallelWhenAny] = new CustomMediator(_serviceFactory, ParallelWhenAny);
            _publishStrategies[PublishStrategy.SyncContinueOnException] = new CustomMediator(_serviceFactory, SyncContinueOnException);
            _publishStrategies[PublishStrategy.SyncStopOnException] = new CustomMediator(_serviceFactory, SyncStopOnException);
        }

        /// <summary>
        /// 策略集
        /// </summary>
        private readonly static IDictionary<PublishStrategy, IMediator> _publishStrategies = new Dictionary<PublishStrategy, IMediator>();

        /// <summary>
        /// 默认策略 同步发布
        /// </summary>
        private readonly PublishStrategy _defaultStrategy = PublishStrategy.SyncContinueOnException;

        /// <summary>
        /// 同步发布
        /// </summary>
        /// <typeparam name="TNotification"></typeparam>
        /// <param name="notification">消息</param>
        /// <returns></returns>
        public Task Publish<TNotification>(TNotification notification) where TNotification : INotification
        {
            return Publish(notification, _defaultStrategy, default(CancellationToken));
        }

        /// <summary>
        /// 根据策略发布
        /// </summary>
        /// <typeparam name="TNotification"></typeparam>
        /// <param name="notification">消息</param>
        /// <param name="strategy">策略</param>
        /// <returns></returns>
        public Task Publish<TNotification>(TNotification notification, PublishStrategy strategy) where TNotification : INotification
        {
            return Publish(notification, strategy, default(CancellationToken));
        }

        /// <summary>
        /// 同步发布消息
        /// </summary>
        /// <typeparam name="TNotification"></typeparam>
        /// <param name="notification">消息</param>
        /// <param name="cancellationToken">取消标记</param>
        /// <returns></returns>
        public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken) where TNotification : INotification
        {
            return Publish(notification, _defaultStrategy, cancellationToken);
        }

        /// <summary>
        /// 根据策略发布消息
        /// </summary>
        /// <typeparam name="TNotification"></typeparam>
        /// <param name="notification">消息</param>
        /// <param name="strategy">策略</param>
        /// <param name="cancellationToken">取消标记</param>
        /// <returns></returns>
        public Task Publish<TNotification>(TNotification notification, PublishStrategy strategy, CancellationToken cancellationToken) where TNotification : INotification
        {
            if (!_publishStrategies.TryGetValue(strategy, out var mediator))
            {
                throw new ArgumentException($"Unknown Strategy: {strategy}");
            }

            return mediator.Publish(notification, cancellationToken);
        }

        /// <summary>
        /// 异步消费消息，等待到所有消费完成
        /// </summary>
        /// <param name="handlers"></param>
        /// <returns></returns>
        private Task ParallelWhenAll(IEnumerable<Task> handlers)
        {
            var tasks = new List<Task>();
            foreach (var handler in handlers)
            {
                tasks.Add(Task.Run(() => handler));
            }

            return Task.WhenAll(tasks);
        }

        /// <summary>
        /// 异步消费消息，等待到有消息消费完成
        /// </summary>
        /// <param name="handlers"></param>
        /// <returns></returns>
        private Task ParallelWhenAny(IEnumerable<Task> handlers)
        {
            var tasks = new List<Task>();

            foreach (var handler in handlers)
            {
                tasks.Add(handler);
            }

            return Task.WhenAny(tasks);
        }

        /// <summary>
        /// 异步消费消息不会等待
        /// </summary>
        /// <param name="handlers"></param>
        /// <returns></returns>
        private Task ParallelNoWait(IEnumerable<Task> handlers)
        {
            foreach (var handler in handlers)
            {
                Task.Run(() => handler);
            }

            return Task.FromResult(string.Empty);
        }

        /// <summary>
        /// 异步消费消息
        /// 等待到所有消费完成
        /// 遇到异常后继续消费后续消息
        /// </summary>
        /// <param name="handlers"></param>
        /// <returns></returns>
        private async Task AsyncContinueOnException(IEnumerable<Task> handlers)
        {
            var tasks = new List<Task>();
            var exceptions = new List<Exception>();

            foreach (var handler in handlers)
            {
                try
                {
                    tasks.Add(handler);
                }
                catch (Exception ex) when (!(ex is OutOfMemoryException || ex is StackOverflowException))
                {
                    exceptions.Add(ex);
                }
            }

            try
            {
                await Task.WhenAll(tasks).ConfigureAwait(false);
            }
            catch (AggregateException ex)
            {
                exceptions.AddRange(ex.Flatten().InnerExceptions);
            }
            catch (Exception ex) when (!(ex is OutOfMemoryException || ex is StackOverflowException))
            {
                exceptions.Add(ex);
            }

            if (exceptions.Any())
            {
                throw new AggregateException(exceptions);
            }
        }

        /// <summary>
        /// 同步消费
        /// 遇到异常后停止消费后续消息
        /// </summary>
        /// <param name="handlers"></param>
        /// <returns></returns>
        private async Task SyncStopOnException(IEnumerable<Task> handlers)
        {
            foreach (var handler in handlers)
            {
                await handler.ConfigureAwait(false);
            }
        }

        /// <summary>
        /// 同步消费
        /// 遇到异常后继续消费后续消息
        /// </summary>
        /// <param name="handlers"></param>
        /// <returns></returns>
        private async Task SyncContinueOnException(IEnumerable<Task> handlers)
        {
            var exceptions = new List<Exception>();

            foreach (var handler in handlers)
            {
                try
                {
                    await handler.ConfigureAwait(false);
                }
                catch (AggregateException ex)
                {
                    exceptions.AddRange(ex.Flatten().InnerExceptions);
                }
                catch (Exception ex) when (!(ex is OutOfMemoryException || ex is StackOverflowException))
                {
                    exceptions.Add(ex);
                }
            }

            if (exceptions.Any())
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
