using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Dispatcher
{
    public class SubscribeInvoker
    {
        private readonly ConcurrentDictionary<string, ObjectMethodExecutor> _executors;
        public SubscribeInvoker()
        {
            _executors = new ConcurrentDictionary<string, ObjectMethodExecutor>();
        }

        public object Invoke(ConsumerExecutorDescriptor context)
        {
            var methodInfo = context.MethodInfo;
            var reflectedType = methodInfo.ReflectedType.Name;
            var key = $"{methodInfo.Module.Name}_{reflectedType}_{methodInfo.MetadataToken}";
            var executor = _executors.GetOrAdd(key, x => ObjectMethodExecutor.Create(methodInfo, context.ImplTypeInfo));
            var obj = Activator.CreateInstance(context.ImplTypeInfo);
            return executor.Execute(obj, null);
        }
    }
}
