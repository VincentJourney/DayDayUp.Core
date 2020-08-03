using System;
using static Grammar.Delegate.EventDemo;

namespace Grammar.Delegate
{
    class Program
    {
        static void Main(string[] args)
        {
            var item = new Item { Name = "键盘", Money = 200 };
            var item2 = new Item { Name = "鼠标", Money = 100 };
            Console.WriteLine($"【{item.Name}】初始价格{item.Money},【{item2.Name}】初始价格{item2.Money}");
            Console.WriteLine("犹豫买不买。。。");
            EventDemo.Buy_youyu(item);
            EventDemo.Buy_youyu(item2);
            var money1 = 0;
            var money2 = 0;
            do
            {
                Console.WriteLine("=====商家开始变动价格=====");
                Console.WriteLine("请输入键盘价格");
                var str = Console.ReadLine();
                money1 = int.Parse(str);
                EventDemo.biandong(item, money1);

                Console.WriteLine("请输入鼠标价格");
                var str2 = Console.ReadLine();
                money2 = int.Parse(str2);
                EventDemo.biandong(item2, money2);
            } while (money1 != 0 && money2 != 0);
        }
    }
}
