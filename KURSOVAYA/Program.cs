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
                Console.Write("X["+(i+1)+"] = "+array[i] + " ");
            }
            Console.WriteLine();
        }

        static void Main()
        {

            Func<Vector, double> eq1 = a => 0.1 * a.X[0] * a.X[0] + a.X[0] + 0.2 * a.X[1] * a.X[1] - 0.3;
            Func<Vector, double> eq2 = a => 0.2 * a.X[0] * a.X[0] + a.X[1] - 0.1 * a.X[0] * a.X[1] - 0.7;

            var c = Newton.SolveNewthon(eq1,eq2);
            Print(c);

            var d = Newton.IncompleteForecast(eq1,eq2);
            Print(d);
            var e = Newton.СompleteForecast(eq1,eq2);
            Print(e);

            Console.ReadLine();

        }
    }
}
