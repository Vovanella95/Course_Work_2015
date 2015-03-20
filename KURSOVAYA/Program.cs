using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewtonMethod;

namespace KURSOVAYA
{
    class Program
    {
        static void Main(string[] args)
        {
            var c = Newton.Roots(new double[,]
                {{0, 2, 2, 2},
                 {4, 0 ,0, 4},
                 {6, 2, 0, 0}
                });
        }
    }
}
