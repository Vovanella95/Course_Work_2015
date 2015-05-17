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
using System.IO;

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

        int system = 0;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double[] start = data.Text.Split(' ').Select(w => Convert.ToDouble(w)).ToArray();

            double[] c = null;
            try
            {
                var eqs = system == 0 ? SystemGenerator.GenerateSystem(Convert.ToInt32(eqNumber.Text)) :
                    SystemGenerator.GenerateSystem2(Convert.ToInt32(eqNumber.Text));
                if (newton1.IsChecked == true)
                {
                    c = Newton.SolveNewthon(eqs, start, accuracy.Value);
                }
                if (newton2.IsChecked == true)
                {
                    c = Newton.СompleteForecast(eqs, start, accuracy.Value);
                }
                if (newton3.IsChecked == true)
                {
                    c = Newton.IncompleteForecast(eqs, start, accuracy.Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }




            answer.Items.Clear();
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

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (system == 0)
            {
                system = 1;
                img.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Images\Sys2.jpg"));
            }
            else
            {
                system = 0;
                img.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Images\Sys1.jpg"));
            }
        }

        
    }
}
