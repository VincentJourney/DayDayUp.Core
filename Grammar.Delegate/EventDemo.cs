using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grammar.Delegate
{

    public class EventDemo
    {
        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="item"></param>
        public static void Subscribe(Item item)
        => item.ChangeMoney += str => Console.WriteLine(str);

        /// <summary>
        /// 价格变动
        /// </summary>
        /// <param name="item"></param>
        /// <param name="t"></param>
        public static void Change(Item item, int t) => item.Money = t;

        /// <summary>
        /// 商品
        /// </summary>
        public class Item
        {
            public event Action<string> ChangeMoney;
            private int _money;
            public string Name { get; set; }
            public int Money
            {
                get => _money;
                set
                {
                    if (value > _money)
                        ChangeMoney?.Invoke($"商品【{Name}】上涨，{_money} =》 {value}");
                    if (value < _money)
                        ChangeMoney?.Invoke($"商品【{Name}】下调，{_money} =》 {value}");
                    _money = value;
                }
            }
        }
    }


}
