using System;

namespace ServiceCollection
{
    class Program
    {
        static void Main(string[] args)
        {
            Container.RegisterType<IApple, Apple>();
            Container.RegisterType<IHuaWei, HuaWei>();
            Container.RegisterType<IPower, Power>();

            var c = new Container();

            var Power = c.Resolve<IPower>() as Power;
            var Apple = c.Resolve<IApple>();
            var HuaWei = c.Resolve<IHuaWei>();

            Power.pow();
            Apple.Discount();
            HuaWei.Call();
        }
    }
}
