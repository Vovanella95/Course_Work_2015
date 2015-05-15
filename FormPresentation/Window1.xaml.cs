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
using System.Windows.Shapes;
using NewtonMethod;

namespace FormPresentation
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Func<NewtonMethod.Vector, double> e1 = a => a.X[0] * a.X[1] - 8 * a.X[0] - 4 * a.X[2] + 10;
            Func<NewtonMethod.Vector, double> e2 = a => 2 * a.X[0] - 3 * a.X[1] + a.X[2] * a.X[2] - 4;
            Func<NewtonMethod.Vector, double> e3 = a => 3 * a.X[0] - a.X[1] * a.X[2] - 3 * a.X[2] - 19;

            double[] start = data.Text.Split(' ').Select(w => Convert.ToDouble(w)).ToArray();

            var c = Newton.SolveNewthon(new Func<NewtonMethod.Vector, double>[] { e1, e2, e3 }, start, accuracy.Value);




            for (int i = 0; i < c.Length-1; i++)
            {
                answer.Items.Add(
                    new ListBoxItem()
                    {
                        Content = "X["+(i+1)+"] = "+c[i]
                    }
                    );
            }
            answer.Items.Add(
                new ListBoxItem()
                {
                    Content = "Y(x) = " + c.Last()
                }
                );
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            eqNumber.Text = Convert.ToString(Convert.ToInt32(eqNumber.Text) + 1);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            eqNumber.Text = Convert.ToInt32(eqNumber.Text)>3? Convert.ToString(Convert.ToInt32(eqNumber.Text) - 1):"3";
        }

        
    }
}
