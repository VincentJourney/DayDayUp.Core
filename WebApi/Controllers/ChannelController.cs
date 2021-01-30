using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChannelController : ControllerBase
    {
        private readonly ILogger<ChannelController> _logger;

        private static readonly Channel<string> _channel = Channel.CreateUnbounded<string>();

        private static ChannelWriter<string> _channelWriter = _channel.Writer;

        private static ChannelReader<string> _channelReader = _channel.Reader;

        public ChannelController(ILogger<ChannelController> logger)
        {
            _logger = logger;
        }

        [HttpGet("Write")]
        public IActionResult Write(int id)
        {
            for (int i = 0; i < id; i++)
            {
                var mes = $"{nameof(Write)}_{i}";
                _channelWriter.TryWrite(mes);
                Console.WriteLine(mes);
            }
            //_channelWriter.Complete();
            return Ok();
        }

        [HttpGet("Write2")]
        public IActionResult Write2(int id)
        {
            for (int i = 0; i < id; i++)
            {
                var mes = $"{nameof(Write2)}_{i}";
                _channelWriter.TryWrite(mes);
                Console.WriteLine(mes);
            }
            //_channelWriter.Complete();
            return Ok();
        }

        [HttpGet("Reader")]
        public async Task Reader()
        {
            await foreach (var item in _channelReader.ReadAllAsync())
            {
                Console.WriteLine($"{nameof(Reader)}_{item}");
                await Task.Delay(1000);
            }
        }
        [HttpGet("Reader2")]
        public async Task Reader2()
        {
            await foreach (var item in _channelReader.ReadAllAsync())
            {

                Console.WriteLine($"{nameof(Reader2)}_{item}");
                if (item == $"{nameof(Write2)}_{5}")
                {
                    Console.WriteLine("--------------------");
                    _channelWriter.TryWrite(item);
                }
                await Task.Delay(1000);
            }
        }
    }
}
