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
        static void Main(string[] args)
        {

            Stopwatch sw = new Stopwatch();
            sw.Start();
            var c = Newton.SolveNewthon(
                a => 0.1*a.X[0]*a.X[0] + a.X[0]*a.X[1]*a.X[1] + 0.2*a.X[1]*a.X[2]-0.3,
                a => 0.2*a.X[0]*a.X[0] + a.X[1]-0.1*a.X[0]*a.X[1]-0.7,
                a => 8.2*a.X[0]*a.X[1]*a.X[2] - 12.2*a.X[2]*a.X[2]*a.X[0]+a.X[3]+0.4,
                a => 9.2*a.X[3] + 10.3*a.X[0]*a.X[0]-a.X[3]*a.X[3]*a.X[3] - a.X[1],
                a => 12.0*a.X[4]*a.X[4]*a.X[0]*a.X[2]+10*a.X[4]-10*a.X[0]*a.X[3]+2
                    
                                        );
            sw.Stop();
            var Time1 = sw.ElapsedTicks;


            sw.Restart();
            var d = Newton.IncompleteForecast(
                a => 0.1 * a.X[0] * a.X[0] + a.X[0] * a.X[1] * a.X[1] + 0.2 * a.X[1] * a.X[2] - 0.3,
                a => 0.2 * a.X[0] * a.X[0] + a.X[1] - 0.1 * a.X[0] * a.X[1] - 0.7,
                a => 8.2 * a.X[0] * a.X[1] * a.X[2] - 12.2 * a.X[2] * a.X[2] * a.X[0] + a.X[3] + 0.4,
                a => 9.2 * a.X[3] + 10.3 * a.X[0] * a.X[0] - a.X[3] * a.X[3] * a.X[3] - a.X[1],
                a => 12.0 * a.X[4] * a.X[4] * a.X[0] * a.X[2] + 10 * a.X[4] - 10 * a.X[0] * a.X[3] + 2
                                        );
            sw.Stop();
            var Time2 = sw.ElapsedTicks;
        }
    }
}
