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

        public void Run()
        {
            _student.Study("1");
        }
    }
}
