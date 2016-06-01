using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

enum State {Solid, Liquid};

namespace QualScheme
{
    class Program
    {
        static void Main(string[] args)
        {
            State x = State.Solid;

            if (x == State.Solid)
            {
                Console.WriteLine("Solid!");
            }

            else if (x == State.Liquid)
            {
                Console.WriteLine("Liquid!");
            }

            Console.ReadKey();
                  
        }
    }
}
