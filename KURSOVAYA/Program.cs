using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewtonMethod;
using System.Diagnostics;
using Simpro.Expr;
using System.Linq.Expressions;

namespace KURSOVAYA
{
    class Program
    {
        static void Main(string[] args)
        {


            ExprParser ep = new ExprParser();
            LambdaExpression m = ep.Parse("(int x, int y) => (x+x)+12*y-3");
            var dd = ep.Run(m, 2, 6);
            


            Stopwatch sw = new Stopwatch();
            sw.Start();
            var c = Newton.SolveNewthon(
                a => 0.1 * a.X[0] * a.X[0] + a.X[0] + 0.2 * a.X[1] * a.X[1] - 0.3,
                a => 0.2 * a.X[0] * a.X[0] + a.X[1] - 0.1 * a.X[0] * a.X[1] - 0.7);
                sw.Stop();
            var Time1 = sw.ElapsedTicks;


            sw.Start();
            var d = Newton.IncompleteForecast(
                a => 0.1 * a.X[0] * a.X[0] + a.X[0] + 0.2 * a.X[1] * a.X[1] - 0.3,
                a => 0.2 * a.X[0] * a.X[0] + a.X[1] - 0.1 * a.X[0] * a.X[1] - 0.7);
            sw.Stop();
            var Time2 = sw.ElapsedTicks;
        }
    }
}
