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
using Simpro.Expr;

namespace FormPresentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Run_Click(object sender, RoutedEventArgs e)
        {


            ExprParser ep = new ExprParser();
            LambdaExpression m = ep.Parse("(int x, int y) => (x+x)+12*y-3");
           


            InputBox.Text = Newton.SolveNewthon(
                a => a.X[0] * a.X[1] - 2, 
                a => a.X[1]-a.X[0]-1
                )[0].ToString();
        }
    }
}
