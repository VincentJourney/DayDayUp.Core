using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autofac_MediatR
{
    public interface IPeople
    {
        void Run();
    }
    public class People : IPeople
    {
        private readonly IStudent _student;

        public People(IStudent student)
        {
            _student = student;
        }

        public virtual void Run()
        {
            _student.Study("1");
        }
    }
    public class PeopleX : People
    {
        private readonly IStudent _student;
        private readonly int _a;

        public PeopleX(IStudent student, int a) : base(student)
        {
            _student = student;
            _a = a;
        }

        public override void Run()
        {
            Console.WriteLine(_a);
        }
    }

}
