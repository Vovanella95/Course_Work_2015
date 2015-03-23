﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtonMethod
{
    public static class Newton
    {
        public class Vector // нифига не понял // и не надо
        {
            public double[] X;
            public Vector(params double[] x)
            {
                X = new double[x.Length];
                Array.Copy(x, X, x.Length);
            }
        }

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

        public static double[] SolveNewthon(params Func<Vector, double>[] F) 
        {
            int N = F.Length;
            double[] Xk = new double[N];
            double eps = 0.000001;
            int Iterations = 0;

            for (int i = 0; i < N; i++)
            {
                Xk[i] = 1;
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

                bool c = true;
                for (int i = 0; i < N; i++)
                {
                    if (F[i](new Vector(Xk)) > eps)
                    {
                        c = false;
                    }
                }

                if (c)
                {
                    return Xk;
                }
            }
        }

        public static double[] IncompleteForecast(params Func<Vector, double>[] F) //чччч
        {
            int N = F.Length;
            double[] Xk = new double[N];
            double[] TempX = new double[N];
            double[] Fxn = new double[N];
            double eps = 0.000001;
            double beta = 0.1;
            double gamma = beta * beta;
            double[] Fxn1 = new double[N]; 

            for (int i = 0; i < N; i++)
            {
                Xk[i] = 1;
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
                    TempX[i] = Xk[i] + beta*dX[i];
                }

               


                for (int i = 0; i < N; i++)   
                {
                    Fxn[i] = F[i](new Vector (Xk));
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
                    gamma =(NormFn * gamma) / NormFn1;
                    if (beta > 1)
                    {
                        beta = 1;
                    }
                }
                Array.Copy(TempX, Xk, N);
                bool c = true;
                for (int i = 0; i < N; i++)
                {
                    if (F[i](new Vector(Xk)) > eps)
                    {
                        c = false;
                    }
                }
                if (c)
                {
                    return Xk;
                }
            }
        }

        public static double[] СompleteForecast(params Func<Vector, double>[] F) //W
        {
            int N = F.Length;
            double[] Xk = new double[N];
            double[] TempX = new double[N];
            double[] Fxn = new double[N]; // Это вектор Fn(Xn)
            double eps = 0.0001;
            double beta = 0.01;
            double[] Fxn1 = new double[N]; // Это вектор Fn(X(n+1))


            for (int i = 0; i < N; i++)
            {
                Xk[i] = 1;
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

                ///////////////////////////////////////////////////////
                //////// ТУТ ОСНОВНОЕ ОТЛИЧИЕ МОЕГО МЕТОДА НАФИГ //////


                for (int i = 0; i < N; i++)   // Тут я его задаю
                {
                    Fxn[i] = F[i](new Vector(Xk));
                    Fxn1[i] = F[i](new Vector(TempX));
                }


                double NormFn = Norm(Fxn);    // Так узнаю ||F(xn)||
                double NormFn1 = Norm(Fxn1);  // Так узнаю ||F(x(n+1)||
                double NormdX = Norm(dX);
                double NFDX = NormFn + NormdX;
                double NFDX1 = NormFn1 + NormdX;

                double gamma = beta * beta * NFDX  / NormFn1;
               
                double temp = beta;
                beta = (NormFn * gamma) / (NFDX* beta); // Так узнаю бета, но бета
                // должно быть как min(..., 1)
                // поэтому
                double NormFn2 = 0;
                gamma = gamma * NormFn * NFDX1 / NormdX * NormFn2; // Так находится Гамма


                if (beta > 1)
                {
                    beta = 1;
                }
                ///////////////// КОНЕЦ ОСНОВНОГО ОТЛИЧИЯ НАФИГ ////////////////////
                ////////////////////////////////////////////////////////////////////

                Array.Copy(TempX, Xk, N);
                bool c = true;
                for (int i = 0; i < N; i++)
                {
                    if (F[i](new Vector(Xk)) > eps)
                    {
                        c = false;
                    }
                }
                if (c)
                {

                    return Xk;
                }
            }
        }
    }
}
