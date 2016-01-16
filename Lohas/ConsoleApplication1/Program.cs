using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lohas.Business.Component.Contract;
using Lohas.Core.Ioc;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var r=ObjectFactory.Resolve<IClass1>();
            Console.WriteLine(r.SayHello());
            var r1 = ObjectFactory.Resolve<IClass1>();
            Console.WriteLine(r1.SayHello());


            Console.WriteLine(r.GetHashCode());
            Console.WriteLine(r1.GetHashCode());
            Console.ReadLine();
        }
    }
}
