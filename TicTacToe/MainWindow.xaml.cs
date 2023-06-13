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

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Model _model;
        public MainWindow()
        {
            InitializeComponent();

            // make it so the user cannot resize the window
            this.ResizeMode = ResizeMode.NoResize;

            // create an instance of our Model
            _model = new Model();
            this.DataContext = _model;

            MyItemsControl.ItemsSource = _model.TileCollection;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var selectedButton = e.OriginalSource as FrameworkElement;
            if (selectedButton != null)
            {
                var currentTile = selectedButton.DataContext as Tile;

                if (currentTile.TileTaken)
                {
                    _model.StatusText = "Try Again. Tile is occupied.";
                    return;
                }
                _model.UserSelection(currentTile.TileName);
            }
            
        }

        private void Clear_Button_Click(object sender, RoutedEventArgs e)
        {
            _model.Clear();
        }
    }
}