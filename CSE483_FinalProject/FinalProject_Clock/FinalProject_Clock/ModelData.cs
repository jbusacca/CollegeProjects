using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// added for INotifyPropertyChanged
using System.ComponentModel;

// added for brushes
using System.Windows.Media;

namespace FinalProject_Clock
{
    public partial class Model : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }

        }
        private String _pmText = "";
        public String PM_Text
        {
            get { return _pmText; }
            set
            {
                _pmText = value;
                OnPropertyChanged("PM_Text");
            }
        }

        private System.Windows.Visibility _pmText_Visible;
        public System.Windows.Visibility PM_Visible
        {
            get { return _pmText_Visible; }
            set
            {
                _pmText_Visible = value;
                OnPropertyChanged("PM_Visible");
            }
        }
    }
}
