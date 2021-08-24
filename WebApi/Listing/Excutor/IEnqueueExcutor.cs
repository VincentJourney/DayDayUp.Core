using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi
{
    public interface IEnqueueExcutor : IListingExcutor
    {
    }

    public class AeEnqueueExcutor : IEnqueueExcutor
    {
        public void Excute(string name)
        {
        }
    }

    public class EbayEnqueueExcutor : IEnqueueExcutor
    {
        public void Excute(string name)
        {
        }
    }
}
