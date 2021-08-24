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
        }
    }

    public class EbayCreateSkuExcutor : ICreateSkuExcutor
    {
        public void Excute(string name)
        {
        }
    }
}
