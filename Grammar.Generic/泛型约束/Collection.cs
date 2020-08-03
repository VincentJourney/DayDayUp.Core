using System;

namespace Grammar.Generic
{
    public class Collection<T> where T : People
    {
        private static Collection<T> Instance;
        private static string name;

        private static readonly object clock = new object();
        private Collection(string Name) => name = Name;

        public static Collection<T> CreateInstance(string Name)
        {
            if (Instance == null)
                lock (clock)
                    if (Instance == null)
                        Instance = new Collection<T>(Name);
            return Instance;
        }
        public void CallMyName()
        {
            Console.WriteLine(name);
        }

        public void CallMyName(string Name)
        {
            People stu = new Student(Name);
            stu.CallMyName();
        }

    }
}
