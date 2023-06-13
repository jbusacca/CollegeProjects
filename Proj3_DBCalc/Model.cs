using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Media;

namespace Proj3_DBCalc
{
    public class Model : INotifyPropertyChanged
    {
        // define our property chage event handler, part of data binding
        public event PropertyChangedEventHandler PropertyChanged;

        // define out own type for calculator operations
        public enum CurrentOperation { OPERATION_ADD, OPERATION_SUBTRACT, OPERATION_MULTIPLY, OPERATION_DIVIDE };

        // property for the current calculator operation
        private CurrentOperation _TextBox_Operation;
        public CurrentOperation TextBox_Operation
        {
            get { return _TextBox_Operation; }
            set
            {
                _TextBox_Operation = value;
                if (_TextBox_Operation == CurrentOperation.OPERATION_ADD)
                    Result = _firstNumber + _secondNumber;
                else if (_TextBox_Operation == CurrentOperation.OPERATION_SUBTRACT)
                {
                    Result = _firstNumber - _secondNumber;
                }
                else if (_TextBox_Operation == CurrentOperation.OPERATION_MULTIPLY)
                {
                    Result = _firstNumber * _secondNumber;
                }
                else
                {
                    Result = _firstNumber / _secondNumber;
                }
                OnPropertyChanged("TextBox_Operation");
            }
        }

        // property for the first number
        private int _firstNumber;
        public int FirstNumber
        {
            get { return _firstNumber; }
            set
            {
                _firstNumber = value;
                DoCalculation();
                OnPropertyChanged("FirstNumber");
            }
        }

        // property for the second number
        private int _secondNumber;
        public int SecondNumber
        {
            get { return _secondNumber; }
            set
            {
                _secondNumber = value;
                DoCalculation();
                OnPropertyChanged("SecondNumber");
            }
        }

        // property for the result
        private int _result;
        public int Result
        {
            get { return _result; }
            set
            {
                _result = value;
                OnPropertyChanged("Result");
            }
        }

        // constructor
        public Model()
        {
            TextBox_Operation = CurrentOperation.OPERATION_ADD;
        }

        // perform calculation based upon current data and operation
        private void DoCalculation()
        {
            if (_TextBox_Operation == CurrentOperation.OPERATION_ADD)
                Result = FirstNumber + SecondNumber;
            else if (_TextBox_Operation == CurrentOperation.OPERATION_SUBTRACT)
            {
                Result = FirstNumber - SecondNumber;
            }
            else if (_TextBox_Operation == CurrentOperation.OPERATION_MULTIPLY)
            {
                Result = FirstNumber * SecondNumber;
            }
            else
            {
                Result = FirstNumber / SecondNumber;
            }
        }

        // implements method for data binding to any and all properties
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }

        }
    }
}