using System;
using System.Linq;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Dispatcher
{
    public class Dispatcher
    {
        private readonly Channel<ConsumerExecutorDescriptor> _receivedChannel;
        private readonly SubscribeInvoker _executor;
        public Dispatcher(SubscribeInvoker subscribeInvoker)
        {
            _receivedChannel = Channel.CreateUnbounded<ConsumerExecutorDescriptor>();
            _executor = subscribeInvoker;
            Task.WhenAll(Enumerable.Range(0, 1)
             .Select(_ => Task.Factory.StartNew(Processing, CancellationToken.None, TaskCreationOptions.LongRunning, TaskScheduler.Default)).ToArray());
        }

        public void Push(ConsumerExecutorDescriptor consumerExecutorDescriptor)
        {
            //await _receivedChannel.Writer.WaitToWriteAsync();
            _receivedChannel.Writer.TryWrite(consumerExecutorDescriptor);
        }

        private async Task Processing()
        {
            try
            {
                await foreach (var item in _receivedChannel.Reader.ReadAllAsync())
                {
                    _executor.Invoke(item);
                }
                //while (await _receivedChannel.Reader.WaitToReadAsync())
                //{
                //    while (_receivedChannel.Reader.TryRead(out var message))
                //    {
                //        _executor.Invoke(message);
                //    }
                //}
            }
            catch (OperationCanceledException)
            {
                // expected
            }
        }
    }
}
