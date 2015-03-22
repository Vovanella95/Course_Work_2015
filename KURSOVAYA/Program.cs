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
            var c = Newton.SolveNewthon(
                a => 0.1*a.X[0]*a.X[0] + a.X[0] + 0.2*a.X[1]*a.X[1]-0.3,
                a => 0.2*a.X[0]*a.X[0] + a.X[1]-0.1*a.X[0]*a.X[1]-0.7
                                        );
        }
    }
}
