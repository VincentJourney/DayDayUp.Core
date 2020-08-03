using System;
using System.Collections.Generic;
using System.Text;

namespace Grammar.Generic
{
    public class Method
    {
        public static void Test()
        {
            var stuCollection = Collection<Student>.CreateInstance("Collection_Student");

            var peoCollection = Collection<People>.CreateInstance("Collection_People");
            //peoCollection = stuCollection;
            stuCollection.CallMyName();
            peoCollection.CallMyName();
            stuCollection.CallMyName("1");

        }


        /// <summary>
        /// 逆变 in 关键字   进（只限制为入参）
        /// 1，泛型参数定义的类型只能作为方法参数的类型，不能作为返回值类型
        /// 2，且该类型是接口方法的参数类型的基类型；
        /// 就是说 传参类型可以是T，也可以是T的派生类
        /// </summary>
        public static void Test_in()
        {
            Student s = new Student("Test_in");
            Action<People> actionPeople = t => t.CallMyName();
            Action<Student> actionStudent = actionPeople;
            actionStudent(s);

            //Action<string> action3 = t => { Console.WriteLine(t.GetType()); };
            //Action<object> action4 = action3;

        }

        /// <summary>
        /// 协变 out 关键字  出（只限制为出参）
        /// 1，泛型参数定义的类型只能作为方法返回值类型
        /// 2，且该类型是接口方法的参数类型的基类型；
        /// </summary>
        public static void Test_out()
        {
            Student s = new Student("Test_out");
            Apple a = new Apple();
            People p = s;

            Func<People> funcPeople;

            Func<Student> func3 = () => s;
            funcPeople = func3;

            //Func<Student> func2 = funcPeople;

            //Func<Apple> funcApple = () => a;
            //funcPeople = funcApple;
            //funcPeople = null;
            funcPeople?.Invoke().CallMyName();
        }
    }
}
