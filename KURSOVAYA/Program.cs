using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewtonMethod;
using System.Diagnostics;

namespace KURSOVAYA
{
    class Program
    {
        static void Print(double[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write("X[" + (i + 1) + "] = " + array[i] + " ");
            }
            Console.WriteLine();
        }

        static void Main()
        {
            Func<Vector, double> eq1 = a => 0.1 * a.X[0] * a.X[0] + a.X[0] + 0.2 * a.X[1] * a.X[1] - 0.3;
            Func<Vector, double> eq2 = a => 0.2 * a.X[0] * a.X[0] + a.X[1] - 0.1 * a.X[0] * a.X[1] - 0.7;

            Func<Vector, double> e1 = a => a.X[0] * a.X[1] - 8 * a.X[0] - 4 * a.X[2] +10;
            Func<Vector, double> e2 = a => 2 * a.X[0] - 3 * a.X[1] + a.X[2] * a.X[2] - 4;
            Func<Vector, double> e3 = a => 3 * a.X[0] - a.X[1] * a.X[2] - 3 * a.X[2] - 19;

            var c = Newton.SolveNewthon(e1, e2, e3);
            var d = Newton.IncompleteForecast(e1, e2, e3);
            var e = Newton.СompleteForecast(e1, e2, e3);

            Print(c);
            Print(d);
            Print(e);
            Console.ReadLine();
        }
    }
}
