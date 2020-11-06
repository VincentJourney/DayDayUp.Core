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
                Console.WriteLine("_mediator.Send");
                var list = new List<CustomRequest> {
                    new CustomRequest { a = 1 },
                    new CustomRequest { a = 2 },
                    new CustomRequest { a = 3 },
                };
                await _mediator.SendAsync(list, async: true);

                Console.WriteLine("_asyncPublisher.Publish");
                await _asyncPublisher.Publish(new CustomNotification { MsgId = "1" }, PublishStrategy.ParallelNoWait);

                Console.WriteLine("_mediator.Publish");
                await _mediator.Publish(new CustomNotification { MsgId = "1" });
                //throw new Exception("test");
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
