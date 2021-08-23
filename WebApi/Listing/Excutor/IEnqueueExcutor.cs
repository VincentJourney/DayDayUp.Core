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
            Console.WriteLine(nameof(AeEnqueueExcutor) + nameof(Excute));
            Console.WriteLine(name);
        }
    }

    public class EbayEnqueueExcutor : IEnqueueExcutor
    {
        public void Excute(string name)
        {
            Console.WriteLine(nameof(EbayEnqueueExcutor) + nameof(Excute));
            Console.WriteLine(name);
        }
    }
}
