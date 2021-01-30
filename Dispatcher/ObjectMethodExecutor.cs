using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dispatcher
{
    class ObjectMethodExecutor
    {
        private delegate object MethodExecutor(object target, params object[] parameters);
        private delegate void VoidMethodExecutor(object target, object[] parameters);
        private readonly MethodExecutor _executor;
        public MethodInfo MethodInfo { get; }
        public ParameterInfo[] MethodParameters { get; }

        public TypeInfo TargetTypeInfo { get; }
        public Type MethodReturnType { get; internal set; }
        public static ObjectMethodExecutor Create(MethodInfo methodInfo, TypeInfo targetTypeInfo)
        {
            return new ObjectMethodExecutor(methodInfo, targetTypeInfo, null);
        }


        private ObjectMethodExecutor(MethodInfo methodInfo, TypeInfo targetTypeInfo, object[] parameterDefaultValues)
        {
            if (methodInfo == null)
            {
                throw new ArgumentNullException(nameof(methodInfo));
            }

            MethodInfo = methodInfo;
            MethodParameters = methodInfo.GetParameters();
            TargetTypeInfo = targetTypeInfo;
            MethodReturnType = methodInfo.ReturnType;


            // Upstream code may prefer to use the sync-executor even for async methods, because if it knows
            // that the result is a specific Task<T> where T is known, then it can directly cast to that type
            // and await it without the extra heap allocations involved in the _executorAsync code path.
            _executor = GetExecutor(methodInfo, targetTypeInfo);
        }
        private static MethodExecutor GetExecutor(MethodInfo methodInfo, TypeInfo targetTypeInfo)
        {
            // Parameters to executor
            var targetParameter = Expression.Parameter(typeof(object), "target");
            var parametersParameter = Expression.Parameter(typeof(object[]), "parameters");

            // Build parameter list
            var parameters = new List<Expression>();
            var paramInfos = methodInfo.GetParameters();
            for (var i = 0; i < paramInfos.Length; i++)
            {
                var paramInfo = paramInfos[i];
                var valueObj = Expression.ArrayIndex(parametersParameter, Expression.Constant(i));
                var valueCast = Expression.Convert(valueObj, paramInfo.ParameterType);

                // valueCast is "(Ti) parameters[i]"
                parameters.Add(valueCast);
            }

            // Call method
            var instanceCast = Expression.Convert(targetParameter, targetTypeInfo.AsType());
            var methodCall = Expression.Call(instanceCast, methodInfo, parameters);

            // methodCall is "((Ttarget) target) method((T0) parameters[0], (T1) parameters[1], ...)"
            // Create function
            if (methodCall.Type == typeof(void))
            {
                var lambda = Expression.Lambda<VoidMethodExecutor>(methodCall, targetParameter, parametersParameter);
                var voidExecutor = lambda.Compile();
                return WrapVoidMethod(voidExecutor);
            }
            else
            {
                // must coerce methodCall to match ActionExecutor signature
                var castMethodCall = Expression.Convert(methodCall, typeof(object));
                var lambda = Expression.Lambda<MethodExecutor>(castMethodCall, targetParameter, parametersParameter);
                return lambda.Compile();
            }
        }

        private static MethodExecutor WrapVoidMethod(VoidMethodExecutor executor)
        {
            return delegate (object target, object[] parameters)
            {
                executor(target, parameters);
                return null;
            };
        }

        public object Execute(object target, params object[] parameters) => _executor(target, parameters);

    }
}
