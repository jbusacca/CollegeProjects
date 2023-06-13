using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// INotifyPropertyChanged
using System.ComponentModel;

// observable collections
using System.Collections.ObjectModel;

// Brush
using System.Windows.Media;
using System.Windows.Shapes;

namespace Project5
{
    public partial class Model : INotifyPropertyChanged
    {
        public ObservableCollection<Shape> RectCollection;
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Shape> EllipseCollection;

        public void InitModel(UInt32 height, UInt32 width)
        {

            _drawingAreaHeight = height;
            _drawingAreaWidth = width;

            RectCollection = new ObservableCollection<Shape>();
            EllipseCollection = new ObservableCollection<Shape>();

            ResetRectangles();
            ResetEllipses();
        }
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private Random randomNumber = new Random(Guid.NewGuid().GetHashCode());
        private UInt32 _drawingAreaHeight = 100;
        private UInt32 _drawingAreaWidth = 100;

        private UInt32 _numRects = 100;

        public UInt32 NumRects
        {
            get { return _numRects; }
            set
            {
                _numRects = value;
                OnPropertyChanged("NumRects");
            }
        }

        private void SetRandomShapes(Shape shape)
        {

            shape.Height = randomNumber.Next(10, 50);
            shape.Width = shape.Height;
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();

            mySolidColorBrush.Color = Color.FromArgb(255, (byte)randomNumber.Next(0, 255), (byte)randomNumber.Next(0, 255), (byte)randomNumber.Next(0, 255));
            shape.Fill = mySolidColorBrush;
            shape.CanvasTop = randomNumber.Next(0, (int)(_drawingAreaHeight - shape.Height));
            shape.CanvasLeft = randomNumber.Next(0, (int)(_drawingAreaWidth - shape.Width));
        }
        public void ResetRectangles()
        {
            RectCollection.Clear();

            for (int rects = 0; rects < _numRects; rects++)
            {
                Shape temp = new Shape();
                SetRandomShapes(temp);
                RectCollection.Add(temp);
            }
        }

        public void ResetEllipses()
        {
            EllipseCollection.Clear();

            for (int ellipses = 0; ellipses < _numRects; ellipses++)
            {
                Shape temp = new Shape();
                SetRandomShapes(temp);
                EllipseCollection.Add(temp);
            }
        }
    }

    public class Shape : INotifyPropertyChanged
    {
        private double _canvasTop;
        public double CanvasTop
        {
            get { return _canvasTop; }
            set
            {
                _canvasTop = value;
                OnPropertyChanged("CanvasTop");
            }
        }

        private double _canvasLeft;
        public double CanvasLeft
        {
            get { return _canvasLeft; }
            set
            {
                _canvasLeft = value;
                OnPropertyChanged("CanvasLeft");
            }
        }

        private double _height;
        public double Height
        {
            get { return _height; }
            set
            {
                _height = value;
                OnPropertyChanged("Height");
            }
        }

        private double _width;
        public double Width
        {
            get { return _width; }
            set
            {
                _width = value;
                OnPropertyChanged("Width");
            }
        }

        private Brush _fill;
        public Brush Fill
        {
            get { return _fill; }
            set
            {
                _fill = value;
                OnPropertyChanged("Fill");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
