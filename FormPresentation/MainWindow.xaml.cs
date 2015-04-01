using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NewtonMethod;
using System.Linq.Expressions;
using System.Diagnostics;

namespace FormPresentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public class SystemEq
    {
        public Func<NewtonMethod.Vector,double>[] Funcs;
        public SystemEq (params Func<NewtonMethod.Vector,double>[] funcs)
        {
            Funcs = new Func<NewtonMethod.Vector,double>[funcs.Length];
            Array.Copy(funcs, Funcs, funcs.Length);
        }
    }


    

    public partial class MainWindow : Window
    {

        SystemEq[] ExcampleSystem = {


                // Системы добавлять туц <<<<<<<>>>>>>>>>

                new SystemEq(
                    a=>a.X[0]+a.X[1]*a.X[0]+4,
                    a=>a.X[0]*a.X[0]-3*a.X[1]+1
                    ),

                new SystemEq(
                    a=>a.X[0]+a.X[1]*a.X[0]+4-a.X[2],
                    a=>a.X[0]*a.X[0]-3*a.X[1]+1-a.X[2],
                    a=>a.X[0]*a.X[2]+a.X[1]-3*a.X[0]-2
                ),

                new SystemEq(
                    a=>a.X[0]+a.X[1]*a.X[0]+4-a.X[2],
                    a=>a.X[0]*a.X[0]-3*a.X[1]+1-a.X[2],
                    a=>a.X[0]*a.X[2]+a.X[1]-3*a.X[0]-2
                ),

                new SystemEq(
                    a=>a.X[0]-a.X[1]*a.X[1]*a.X[2]+a.X[3]-2,
                    a=>a.X[1]*a.X[1]+a.X[0]*a.X[3]+a.X[2]+1,
                    a=>a.X[3]*a.X[3]-a.X[2]*a.X[2]+a.X[1]-5,
                    a=>a.X[0]-a.X[2]+3*a.X[1]-9*a.X[2]+10
                    )
                // <<<<<<<<<<<<<<<<<    >>>>>>>>>>>>>>>>>>>
                                        };

        public MainWindow()
        {
            InitializeComponent();
            
        }




        private void Button_Click(object sender, RoutedEventArgs e)
        {
            

        }

        public string ArrToStr(double[] args)
        {
            string answer = "";
            for (int i = 0; i < args.Length-1; i++)
            {
                answer += "X[" + (i + 1) + "] = " + args[i]+"\n";
            }
            answer += "||F(x)|| = " + args[args.Length - 1];
            return answer;
        }

        string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location).Replace("bin\\Debug","")+"Images\\";

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Images.Source = new BitmapImage(new Uri(path+"1.PNG"));
            EqNumber.Content = "Количество уравнений: " + ExcampleSystem[0].Funcs.Length;
            Answer.Text = ArrToStr(Newton.SolveNewthon(ExcampleSystem[0].Funcs));
            sw.Stop();
            EqNumber.Content = EqNumber.Content.ToString() + "   (Потрачено " + sw.ElapsedTicks + "ts)"; // ts это тики системы
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Images.Source = new BitmapImage(new Uri(path + "2.PNG"));
            EqNumber.Content = "Количество уравнений: " + ExcampleSystem[1].Funcs.Length;
            Answer.Text = ArrToStr(Newton.SolveNewthon(ExcampleSystem[1].Funcs));
            sw.Stop();
            EqNumber.Content = EqNumber.Content.ToString() + "   (Потрачено " + sw.ElapsedTicks + "ts)";
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Images.Source = new BitmapImage(new Uri(path + "3.PNG"));
            EqNumber.Content = "Количество уравнений: " + ExcampleSystem[2].Funcs.Length;
            Answer.Text = ArrToStr(Newton.SolveNewthon(ExcampleSystem[2].Funcs));
            sw.Stop();
            EqNumber.Content = EqNumber.Content.ToString() + "   (Потрачено " + sw.ElapsedTicks + "ts)";
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Images.Source = new BitmapImage(new Uri(path + "4.PNG"));
            EqNumber.Content = "Количество уравнений: " + ExcampleSystem[3].Funcs.Length;
            Answer.Text = ArrToStr(Newton.SolveNewthon(ExcampleSystem[3].Funcs));
            sw.Stop();
            EqNumber.Content = EqNumber.Content.ToString() + "   (Потрачено " + sw.ElapsedTicks + "ts)";
        }

    }
}
