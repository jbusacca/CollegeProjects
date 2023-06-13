using FinalProject_Server;
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

namespace FinalProject_Server
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Model _model;
        public MainWindow()
        {
            InitializeComponent();
            _model = new Model();
            this.DataContext = _model;
            this.ResizeMode = ResizeMode.NoResize;
        }
        private void SetTime_ButtonClick(object sender, RoutedEventArgs e)
        {
            Button bt = (Button)sender;
            _model.Send(bt.Name);
        }
        private void NowTime_ButtonClick(object sender, RoutedEventArgs e)
        {
            Button bt = (Button)sender;
            _model.Send(bt.Name);
        }
        private void SetAlarm_ButtonClick(object sender, RoutedEventArgs e)
        {
            Button bt = (Button)sender;
            _model.Send(bt.Name);
        }
    }
}
