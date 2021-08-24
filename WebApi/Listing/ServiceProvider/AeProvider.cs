using System;
using System.Collections.Generic;
using System.Text;
using WebApi;

namespace WebApi
{
    public class AeProvider : BaseProvider
    {
        public AeProvider(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Load()
        {
            Data.Add(Platform.Ae, ExcutorSource());
        }

        public IEnumerable<Type> ExcutorSource()
        {
            return new List<Type> {
                 typeof( AeCreateSkuExcutor),
                typeof( AeEnqueueExcutor)
            };
        }
    }
}
