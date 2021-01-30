using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DispatcherController : ControllerBase
    {
        private readonly ILogger<ChannelController> _logger;
        private readonly Dispatcher.Dispatcher _dispatcher;

        public DispatcherController(ILogger<ChannelController> logger, Dispatcher.Dispatcher dispatcher)
        {
            _logger = logger;
            _dispatcher = dispatcher;
        }

        [HttpGet("Write")]
        public IActionResult Write()
        {
            var dis = new Dispatcher.ConsumerExecutorDescriptor
            {
                ServiceTypeInfo = typeof(FakeSubscriber).GetTypeInfo(),
                ImplTypeInfo = typeof(FakeSubscriber).GetTypeInfo(),
                MethodInfo = typeof(FakeSubscriber).GetMethod(nameof(FakeSubscriber.OutputIntegerSub))
            };
            _dispatcher.Push(dis);
            return Ok();
        }
    }

    public class FakeSubscriber
    {
        public int OutputIntegerSub()
        {

            Console.WriteLine("sxxsa");
            return 1;
        }
    }
}
