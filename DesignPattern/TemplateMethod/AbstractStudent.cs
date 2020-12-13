using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern
{
    public abstract class AbstractStudent
    {
        public virtual string Name { get; set; } = "Abstract";

        public virtual void Eat()
        {
            Console.WriteLine($"eat:{Name}");
        }

        public void DoInDay()
        {
            Eat();
        }
    }

    public class StudentA : AbstractStudent
    {
        public override string Name { get; set; } = "A";

        public override void Eat()
        {
            Console.WriteLine($"eat:{Name}");
        }
    }
    public class StudentB : AbstractStudent
    {
    }
}
