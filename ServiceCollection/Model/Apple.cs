using System;

namespace ServiceCollection
{
    public class Apple : IApple
    {
        public Apple(IHuaWei huaWei)
        {
            Console.WriteLine($"{this.GetType().Name}需要{huaWei.GetType().Name}");
        }

        public void Discount()
        {
            Console.WriteLine("Discount");
        }
    }
}
