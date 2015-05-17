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
        
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window1 win = new Window1();
            this.Visibility = Visibility.Hidden;
            win.ShowDialog();
            this.Visibility = Visibility.Visible;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        

    }
}
