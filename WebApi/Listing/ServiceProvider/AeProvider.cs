using System;
using System.Collections.Generic;
using System.Text;
using WebApi;

namespace WebApi
{
    public class AeProvider : BaseProvider
    {
        private readonly string _platform = "Ae";

        public override void Load()
        {
            Data.Add(_platform, GetListingExcutors());
        }

        public IEnumerable<IListingExcutor> GetListingExcutors()
        {
            return new List<IListingExcutor> {
                 new AeCreateSkuExcutor(),
                new AeEnqueueExcutor()
            };
        }
    }
}
