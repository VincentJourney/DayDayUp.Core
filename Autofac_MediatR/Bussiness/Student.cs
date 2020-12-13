using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Serilog;

namespace Autofac_MediatR
{
    public interface IStudent
    {
        Task Study(string name);
    }

    public class Student : IStudent
    {
        private readonly IMediator _mediator;
        private readonly AsyncPublisher _asyncPublisher;
        private readonly ILogger _logger;
        public Student(IMediator mediator, ILogger logger, AsyncPublisher asyncPublisher)
        {
            _mediator = mediator;
            _logger = logger;
            _asyncPublisher = asyncPublisher;
        }
        public async Task Study(string name)
        {
            try
            {

                var list = new List<CustomRequest>
                {
                    new CustomRequest { a = 1 },
                    new CustomRequest { a = 2 },
                    new CustomRequest { a = 3 },
                };
                ////单播
                //Console.WriteLine("_mediator.Send");
                _mediator.Send(list.FirstOrDefault());
                Console.WriteLine("zasdasd");
                //Console.WriteLine("_mediator.SendAllAsync");
                ////异步广播
                //try
                //{
                //    await _mediator.SendAllAsync(list);
                //}
                //catch (AggregateException ex)
                //{
                //    var exM = string.Join(",", ex.InnerExceptions.Select(s => s.Message));
                //    Console.WriteLine(exM);
                //}

                //Console.WriteLine("_mediator.SendAll");
                ////同步广播
                //await _mediator.SendAll(list);

                ////异步发布订阅
                //Console.WriteLine("_asyncPublisher.Publish");
                //try
                //{
                //    await _asyncPublisher.Publish(new CustomNotification { MsgId = "1" }, PublishStrategy.ParallelNoWait);
                //}
                //catch (AggregateException ex)
                //{
                //    Console.WriteLine(ex.Message);
                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine(ex.Message);
                //}
                throw new Exception("aaa");
                //同步发布订阅
                //Console.WriteLine("_mediator.PublishOne");
                //await _mediator.Publish(new CustomNotification { MsgId = "1" });
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "异常", 1, 1);
                _logger.Information(ex, "异常", 1, 1);
                _logger.Warning(ex, "异常", 1, 1);
                _logger.Fatal(ex, "异常", 1, 1);
                _logger.Debug(ex, "异常", 1, 1);
                _logger.Verbose(ex, "异常", 1, 1);
            }
        }
    }
}
