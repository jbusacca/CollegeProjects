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
using static Proj3_DBCalc.Model;

namespace Proj3_DBCalc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Model _myModel;

        public MainWindow()
        {
            InitializeComponent();
            _myModel = new Model();
            this.DataContext = _myModel;
        }

        private void TextBox_FirstNum_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox box = (TextBox)sender;
            if (e.Key == Key.Enter)
            {
                ((TextBox)sender).GetBindingExpression(TextBox.TextProperty).UpdateSource();
            }
        }

        private void TextBox_SecondNum_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox box = (TextBox)sender;
            if (e.Key == Key.Enter)
            {
                ((TextBox)sender).GetBindingExpression(TextBox.TextProperty).UpdateSource();
            }
        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            _myModel.TextBox_Operation = Model.CurrentOperation.OPERATION_ADD;
        }

        private void Sub_Click(object sender, RoutedEventArgs e)
        {
            _myModel.TextBox_Operation = Model.CurrentOperation.OPERATION_SUBTRACT;
        }

        private void Mult_Click(object sender, RoutedEventArgs e)
        {
            _myModel.TextBox_Operation = Model.CurrentOperation.OPERATION_MULTIPLY;
        }

        private void Div_Click(object sender, RoutedEventArgs e)
        {
            _myModel.TextBox_Operation = Model.CurrentOperation.OPERATION_DIVIDE;
        }
    }
}
