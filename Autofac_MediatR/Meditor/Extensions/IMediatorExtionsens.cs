﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Autofac_MediatR
{
    public static class IMediatorExtionsens
    {
        /// <summary>
        /// 异步发送多条消息，无返回值
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="mediator"></param>
        /// <param name="requests"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task SendAllAsync(this IMediator mediator, IEnumerable<IRequest> requests, CancellationToken cancellationToken = default(CancellationToken))
        {
            var exceptions = new List<Exception>();
            Parallel.ForEach(requests, async request =>
            {
                try
                {
                    await mediator.Send(request, cancellationToken).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            });
            if (exceptions.Any())
            {
                throw new AggregateException(exceptions);
            }
        }

        public static async Task SendAllAsyncByPagination(this IMediator mediator, IEnumerable<IRequest> requests, int PageSize = 20, CancellationToken cancellationToken = default(CancellationToken))
        {
            var requestsCount = requests.Count();
            var exceptions = new List<Exception>();
            Parallel.ForEach(requests, async request =>
            {
                try
                {
                    await mediator.Send(request, cancellationToken).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            });
            if (exceptions.Any())
            {
                throw new AggregateException(exceptions);
            }
        }
        //public IEnumerable<T> GetListByPagination<T>(IEnumerable<T> list)
        //{
        //    lock (lockObj)
        //    {
        //        var s = list.Skip(pageIndex * pageSize).Take(pageSize);
        //        pageIndex++;
        //        return s;
        //    }
        //}
        /// <summary>
        /// 同步发送多条消息，无返回值
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="mediator"></param>
        /// <param name="requests"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task SendAll(this IMediator mediator, IEnumerable<IRequest> requests, CancellationToken cancellationToken = default(CancellationToken))
        {
            var exceptions = new List<Exception>();
            foreach (var request in requests)
            {
                await mediator.Send(request, cancellationToken).ConfigureAwait(false);
            }
        }
    }
}
