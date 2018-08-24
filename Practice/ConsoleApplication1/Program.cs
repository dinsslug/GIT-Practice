using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 100000000; i++)
            {
                Console.WriteLine("TEST STRING " + (i + 1));
            }
            //Console.WriteLine("계속하려면 아무 키나 누르십시오.");
            //Console.ReadKey(true);
        }
    }
}
