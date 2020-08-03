using System;

namespace ServiceCollection
{
    public class HuaWei : IHuaWei
    {
        public HuaWei(IPower power)
        {
            Console.WriteLine($"{this.GetType().Name}需要{power.GetType().Name}");
        }

        public void Call()
        {
            Console.WriteLine($"{this.GetType().Name}打电话呢");
        }
    }
}
