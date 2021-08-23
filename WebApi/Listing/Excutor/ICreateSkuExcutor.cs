using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi
{
    public interface ICreateSkuExcutor : IListingExcutor
    {
    }

    public class AeCreateSkuExcutor : ICreateSkuExcutor
    {
        public void Excute(string name)
        {
            Console.WriteLine(nameof(AeCreateSkuExcutor) + nameof(Excute));
            Console.WriteLine(name);
        }
    }

    public class EbayCreateSkuExcutor : ICreateSkuExcutor
    {
        public void Excute(string name)
        {
            Console.WriteLine(nameof(EbayCreateSkuExcutor) + nameof(Excute));
            Console.WriteLine(name);
        }
    }
}
