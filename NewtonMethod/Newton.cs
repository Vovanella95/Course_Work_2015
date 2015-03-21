using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtonMethod
{
    public static class Newton
    {
        public class Vector
        {
            public double[] X;
            public Vector (params double[] x)
            {
                X = new double[x.Length];
                Array.Copy(x,X,x.Length);
            }
        }


        public static double[] Roots(double[,] matrix)
        {

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (matrix[i, i] != 0)
                {
                    SetZeroDown(ref matrix, i);
                }else
                {
                    for (int temp = 0; temp < matrix.GetLength(0); temp++)
                    {
                        if(matrix[temp,i]!=0)
                        {
                            SwapLines(ref matrix, temp, i);
                            SetZeroDown(ref matrix, i);
                            break;
                        }
                    }
                }
            }


            double[] Roots = new double[matrix.GetLength(0)];

            for (int i = matrix.GetLength(0)-1; i >=0; i--)
            {
                double summ = 0;
                for (int j = i+1; j < matrix.GetLength(0); j++)
                {
                    summ += matrix[i, j] * Roots[j];
                }
                Roots[i] = (matrix[i, matrix.GetLength(0)] - summ) / matrix[i, i];
            }

            return Roots;
        }


        private static void SetZeroDown(ref double[,] matrix, int i)
        {
            for (int ii = i+1; ii < matrix.GetLength(0); ii++)
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



        public static double[] SolveNewthon(params Func<Vector,double>[] F)
        {
            int N = F.Length;
            double[] Xk = new double[N];
            double eps = 0.00001;

            for (int i = 0; i < N; i++)
            {
                Xk[i] = 1;
            }



            while (true)
            {
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


    }

    
}
