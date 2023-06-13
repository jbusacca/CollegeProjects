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

namespace FinalProject_Clock
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

            this.ResizeMode = ResizeMode.NoResize;

            _model = new Model();
            this.DataContext = _model;

            SevenSegmentLED.ItemsSource = _model.LEDCollection;
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            _model.InitModel();
        }

        private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //_model.CleanUp();
        }
    }
}
