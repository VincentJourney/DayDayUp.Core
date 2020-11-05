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
        private readonly ILogger _logger;
        public Student(IMediator mediator, ILogger logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        public async Task Study(string name)
        {
            try
            {
                var a = await _mediator.Send(new CustomRequest { a = "hah" });
                _mediator.Publish(new CustomNotification { MsgId = "1" });
                throw new Exception("test");
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
