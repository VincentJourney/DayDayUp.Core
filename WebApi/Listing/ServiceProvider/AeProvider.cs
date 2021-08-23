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
            Data.Add(Platform.Ae, GetListingExcutors());
        }

        public IEnumerable<Type> GetListingExcutors()
        {
            return new List<Type> {
                 typeof( AeCreateSkuExcutor),
                typeof( AeEnqueueExcutor)
            };
        }
    }
}
