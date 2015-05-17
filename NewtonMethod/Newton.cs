using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtonMethod
{

    public static class SystemGenerator
    {
        private static Func<Vector, double> GenerateEq(int i)
        {
            return new Func<Vector, double>(w =>
            {
                double num = 0;
                if (i < w.X.Length - 2)
                {
                    for (int j = 0; j < w.X.Length; j++)
                    {
                        num += i == j ? 2 * w.X[j] : w.X[j];
                    }
                    num -= w.X.Length + 1;
                }
                if (i == w.X.Length - 2)
                {
                    return Math.Atan(w.X[0]) + Math.Atan(w.X[w.X.Length - 1]) - 2 * Math.Atan(1);
                }

                if (i == w.X.Length - 1)
                {
                    num = 1;
                    for (int j = 0; j < w.X.Length; j++)
                    {
                        num *= w.X[j];
                    }
                    num -= 1;
                }
                return num;
            }
            );
        }
        public static Func<Vector, double>[] GenerateSystem(int n)
        {
            List<Func<Vector, double>> System = new List<Func<Vector, double>>();
            for (int i = 0; i < n; i++)
            {
                System.Add(GenerateEq(i));
            }
            return System.ToArray();
        }

        private static Func<Vector, double> GenerateEq2(int i)
        {
            return new Func<Vector, double>(w =>
            {
                double num = 0;
                if (i < w.X.Length - 3)
                {
                    for (int j = 0; j < w.X.Length; j++)
                    {
                        num += i == j ? 2 * w.X[j] : w.X[j];
                    }
                    num -= w.X.Length + 1;
                }
                if (i == w.X.Length - 3)
                {
                    return Math.Pow(Math.Sin(w.X[0]), 2) + Math.Pow(Math.Cos(w.X[w.X.Length - 1]), 3) - Math.Pow(Math.Sin(1), 2) - Math.Pow(Math.Cos(1), 3);
                }
                if (i == w.X.Length - 2)
                {
                    return Math.Atan(w.X[0]) + Math.Atan(w.X[w.X.Length - 1]) - 2 * Math.Atan(1);
                }
                if (i == w.X.Length - 1)
                {
                    num = 1;
                    for (int j = 0; j < w.X.Length; j++)
                    {
                        num *= w.X[j];
                    }
                    num -= 1;
                }
                return num;
            }
            );
        }
        public static Func<Vector, double>[] GenerateSystem2(int n)
        {
            List<Func<Vector, double>> System = new List<Func<Vector, double>>();
            for (int i = 0; i < n; i++)
            {
                System.Add(GenerateEq2(i));
            }
            return System.ToArray();




        }
    }




    public class Vector
    {
        public double[] X;
        public Vector(params double[] x)
        {
            X = new double[x.Length];
            Array.Copy(x, X, x.Length);
        }
    }


    public static class Newton
    {
        private static double[] Roots(double[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (matrix[i, i] != 0)
                {
                    SetZeroDown(ref matrix, i);
                }
                else
                {
                    for (int temp = 0; temp < matrix.GetLength(0); temp++)
                    {
                        if (matrix[temp, i] != 0)
                        {
                            SwapLines(ref matrix, temp, i);
                            SetZeroDown(ref matrix, i);
                            break;
                        }
                    }
                }
            }


            double[] Roots = new double[matrix.GetLength(0)];

            for (int i = matrix.GetLength(0) - 1; i >= 0; i--)
            {
                double summ = 0;
                for (int j = i + 1; j < matrix.GetLength(0); j++)
                {
                    summ += matrix[i, j] * Roots[j];
                }
                Roots[i] = (matrix[i, matrix.GetLength(0)] - summ) / matrix[i, i];
            }
            return Roots;
        }

        private static double Norm(double[] x)
        {
            double summ = 0;
            for (int i = 0; i < x.Length; i++)
            {
                summ += x[i] * x[i];
            }
            return Math.Sqrt(summ);
        }

        private static void SetZeroDown(ref double[,] matrix, int i)
        {
            for (int ii = i + 1; ii < matrix.GetLength(0); ii++)
            {
                double k = -matrix[ii, i] / matrix[i, i];
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[ii, j] += k * matrix[i, j];
                }
            }
        }

        private static void SwapLines(ref double[,] matrix, int i1, int i2)
        {
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                var temp = matrix[i1, i];
                matrix[i1, i] = matrix[i2, i];
                matrix[i2, i] = temp;
            }
        }

        public static double[] SolveNewthon(Func<Vector, double>[] F, double[] first = null, double eps = 0.00001)
        {
            int N = F.Length;
            double[] Xk = new double[N];
            int Iterations = 0;


            if (first == null)
            {
                for (int i = 0; i < N; i++)
                {
                    Xk[i] = 1;
                }
            }
            else
            {
                Array.Copy(first, Xk, first.Count());
            }



            while (true)
            {



                Iterations++;
                double[,] J = new double[N, N + 1];

                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        double[] X = new double[N];
                        Array.Copy(Xk, X, N);
                        X[j] += eps;
                        J[i, j] = (F[i](new Vector(X)) - F[i](new Vector(Xk))) / eps;
                    }

                    J[i, N] = -F[i](new Vector(Xk));
                }

                double[] dX = Roots(J);


                for (int i = 0; i < N; i++)
                {
                    Xk[i] += dX[i];
                }

                double[] Fn = new double[N];
                for (int i = 0; i < N; i++)
                {
                    var temp = Xk.Take(Xk.Count() - 1).ToArray();
                    Fn[i] = F[i](new Vector(temp));
                }
                if (Norm(Fn) < eps)
                {
                    var ans = new List<double>(Xk);
                    ans.Add(Norm(Fn));
                    return ans.ToArray();
                }
                if (Iterations > 1000)
                {
                    throw new Exception("Не удается найти решение системы данным методом");
                }
            }
        }

        public static double[] IncompleteForecast(Func<Vector, double>[] F, double[] first = null, double eps = 0.00001)
        {
            int N = F.Length;
            double[] Xk = new double[N];
            double[] TempX = new double[N];
            double[] Fxn = new double[N];
            double beta = 0.1;
            double gamma = beta * beta;
            double[] Fxn1 = new double[N];

            if (first == null)
            {
                for (int i = 0; i < N; i++)
                {
                    Xk[i] = 1;
                }
            }
            else
            {
                Array.Copy(first, Xk, first.Count());
            }

            int Iterations = 0;
            while (true)
            {
                Iterations++;
                double[,] J = new double[N, N + 1];

                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        double[] X = new double[N];
                        Array.Copy(Xk, X, N);
                        X[j] += eps;
                        J[i, j] = (F[i](new Vector(X)) - F[i](new Vector(Xk))) / eps;
                    }
                    J[i, N] = -F[i](new Vector(Xk));
                }

                double[] dX = Roots(J);

                for (int i = 0; i < N; i++)
                {
                    TempX[i] = Xk[i] + beta * dX[i];
                }

                for (int i = 0; i < N; i++)
                {
                    Fxn[i] = F[i](new Vector(Xk));
                    Fxn1[i] = F[i](new Vector(TempX));
                }
                double NormFn = Norm(Fxn);
                double NormFn1 = Norm(Fxn1);

                if (NormFn1 < NormFn)
                {
                    beta = 1;
                }
                else
                {
                    beta = (NormFn * gamma) / (NormFn1 * beta);
                    gamma = (NormFn * gamma) / NormFn1;
                    if (beta > 1)
                    {
                        beta = 1;
                    }
                }
                Array.Copy(TempX, Xk, N);

                if (NormFn1 < eps)
                {
                    var ans = new List<double>(Xk);
                    ans.Add(NormFn1);
                    return ans.ToArray();
                }
                if (Iterations > 1000)
                {
                    throw new Exception("Не удается найти решение системы данным методом");
                }
            }
        }

        public static double[] СompleteForecast(Func<Vector, double>[] F, double[] first = null, double eps = 0.00001)
        {
            int N = F.Length;
            double[] Xk = new double[N];
            double[] TempX = new double[N];
            double[] TempXn = new double[N];
            double[] Fxn = new double[N];
            double beta = 0.1;
            double[] Fxn1 = new double[N];
            double[] Fxn2 = new double[N];
            double[] Fxndx = new double[N];
            double[] Fxndx1 = new double[N];

            if (first == null)
            {
                for (int i = 0; i < N; i++)
                {
                    Xk[i] = 1;
                }
            }
            else
            {
                Array.Copy(first, Xk, first.Count());
            }

            int Iterations = 0;
            while (true)
            {
                Iterations++;
                double[,] J = new double[N, N + 1];

                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        double[] X = new double[N];
                        Array.Copy(Xk, X, N);
                        X[j] += eps;
                        J[i, j] = (F[i](new Vector(X)) - F[i](new Vector(Xk))) / eps;
                    }
                    J[i, N] = -F[i](new Vector(Xk));
                }

                double[] dX = Roots(J);

                for (int i = 0; i < N; i++)
                {
                    TempX[i] = Xk[i] + beta * dX[i];
                }

                double[,] JJ = new double[N, N + 1];

                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        double[] X = new double[N];
                        Array.Copy(TempX, X, N);
                        X[j] += eps;
                        JJ[i, j] = (F[i](new Vector(X)) - F[i](new Vector(TempX))) / eps;
                    }
                    JJ[i, N] = -F[i](new Vector(TempX));
                }

                double[] dXn = Roots(JJ);

                for (int i = 0; i < N; i++)
                {
                    TempXn[i] = TempX[i] + beta * dXn[i];
                }

                for (int i = 0; i < N; i++)
                {
                    Fxn[i] = F[i](new Vector(Xk));
                    Fxn1[i] = F[i](new Vector(TempX));
                    Fxn2[i] = F[i](new Vector(TempXn));
                    Fxndx[i] = F[i](new Vector(Xk)) + dX[i];
                    Fxndx1[i] = F[i](new Vector(TempX)) + dXn[i];
                }

                double NormFn = Norm(Fxn);
                double NormFn1 = Norm(Fxn1);
                double NormFn2 = Norm(Fxn2);
                double NormFdx = Norm(Fxndx);
                double NormFdx1 = Norm(Fxndx1);
                double gamma = beta * beta * NormFdx / NormFn1;

                if (NormFn1 < NormFn)
                {
                    beta = 1;
                }
                else
                {
                    beta = (NormFn * gamma) / (NormFdx * beta);
                    gamma = (gamma * NormFn * NormFdx1) / (NormFdx * NormFn2);
                    if (beta > 1)
                    {
                        beta = 1;
                    }
                }
                Array.Copy(TempX, Xk, N);
                if (NormFn1 < eps)
                {
                    var ans = new List<double>(Xk);
                    ans.Add(NormFn1);
                    return ans.ToArray();
                }
                if (Iterations > 1000)
                {
                    throw new Exception("Не удается найти решение системы данным методом");
                }

            }
        }
    }
}
