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

namespace CalculatorTest
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //CalculatorModel cm = new CalculatorModel(new Curve(2, 4, 5));
            
           
        }

        private void c_btn_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as CalculatorVM).Expression = "";
        }

        private void p0_btn_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as CalculatorVM).Expression += "P0";
        }

        private void btn_7_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as CalculatorVM).Expression += (sender as Button).Content;
        }

        private void p_btn_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as CalculatorVM).Expression += "P(,)";
        }

     

      

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((DataContext as CalculatorVM).ResultObject is Point)
            {
                Point p = (DataContext as CalculatorVM).ResultObject as Point;
                MessageBox.Show($"Порядок точки P({p.X},{p.Y}) равен {p.Order}");
            }
        }

        private void back_point_btn_Click(object sender, RoutedEventArgs e)
        {
            if ((DataContext as CalculatorVM).ResultObject is Point)
            {
                Point p = (DataContext as CalculatorVM).ResultObject as Point;
                Point p1 = (p.Order - 1) * p;
                MessageBox.Show($"Точка P({p1.X},{p1.Y}) является обратной к точке P({p.X},{p.Y})");
            }
        }

        private void points_list_box_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            (DataContext as CalculatorVM).Expression+="P" +(sender as ListBox).SelectedItem;
        }
    }
}
