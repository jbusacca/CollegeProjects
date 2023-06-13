using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Configuration;
using System.Reflection;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Markup;
using System.Windows.Controls;
using TimeDataDLL;
using FinalProject_Clock;
using System.Xml;

namespace FinalProject_Clock
{
    public partial class Model
    {
        private UInt32 _hours;
        private UInt32 _minutes;
        private UInt32 _seconds;
        private UInt32 _alarmhours;
        private UInt32 _alarmminutes;
        private UInt32 _alarmseconds;

        //Socket Related
        private static UInt32 _localPort = 5000;
        private static String _localIPAddress = "127.0.0.1";
        private static UInt32 _remotePort = 5001;
        private static String _remoteIPAddress = "127.0.0.1";
        UdpClient _dataSocket;

        //Game Data format 
        TimeData.StructTimeData timeData;

        //Thread Related
        private Thread _receiveDataThread;
        private bool _isThreadReceiveRunning = false;


        enum CLOCK_STATUS { _12HR, _24HR };
        enum ALARM_STATUS { ALARM, NOALARM }
        CLOCK_STATUS currentClockstatus;
        ALARM_STATUS currentAlarmstatus;

        public ObservableCollection<LED> LEDCollection;
        private static UInt32 _numLEDs = 6;

        System.Windows.Threading.DispatcherTimer dispatcherTimer;

        public string PM_Text { get; private set; }

        public Model()
        {
            LEDCollection = new ObservableCollection<LED>();

            for (int i = 0; i < _numLEDs; i++)
            {
                int tempLEDLeft;
                if (i == 1 | i == 3 | i == 5)
                {
                    tempLEDLeft = (10 + i * 60) - 10;
                }
                else
                {
                    tempLEDLeft = 10 + i * 60;
                }

                LEDCollection.Add(new LED()
                {
                    TopHorizontal_Visible = System.Windows.Visibility.Visible,
                    MiddleHorizontal_Visible = System.Windows.Visibility.Visible,
                    BottomHorizontal_Visible = System.Windows.Visibility.Visible,
                    TopLeftVertical_Visible = System.Windows.Visibility.Visible,
                    BottomLeftVertical_Visible = System.Windows.Visibility.Visible,
                    TopRightVertical_Visible = System.Windows.Visibility.Visible,
                    BottomRightVertical_Visible = System.Windows.Visibility.Visible,

                    LEDTop = 70,
                    LEDLeft = tempLEDLeft,
                    LEDValue = 8
                });
            }

            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

            currentClockstatus = CLOCK_STATUS._24HR;
            currentAlarmstatus = ALARM_STATUS.NOALARM;
            _hours = 23;
            _minutes = 59;
            _seconds = 55;
        }

        public void InitModel()
        {
            try
            {
                _dataSocket = new UdpClient((int)_localPort);
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }

            ThreadStart threadFunction;
            threadFunction = new ThreadStart(ReceiveThreadFunction);
            _receiveDataThread = new Thread(threadFunction);
            //Console.Write(DateTime.Now + ":" + " Waiting for other UDP peer to join.\n");
            _isThreadReceiveRunning = true;
            _receiveDataThread.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            _seconds++;
            UpdateTime();
            CheckForAlarm();
        }
        private void UpdateTime()
        {
            if (_seconds > 59)
            {
                _seconds = 0;
                _minutes++;
            }
            if (_minutes > 59)
            {
                _minutes = 0;
                _hours++;
            }
            if (_hours > 23)
            {
                _hours = 0;
            }

            switch (currentClockstatus)
            {
                case CLOCK_STATUS._12HR:
                    if (_hours > 11)
                    {
                        UInt32 _tempHours = _hours - 12;
                        if (_hours == 12)
                        {
                            LEDCollection[0].LEDValue = 1;
                            LEDCollection[1].LEDValue = 2;
                            PM_Text = "PM";
                        }
                        else
                        {
                            LEDCollection[0].LEDValue = _tempHours / 10;
                            LEDCollection[1].LEDValue = _tempHours % 10;
                            PM_Text = "PM";
                        }

                    }
                    else if (_hours <= 11)
                    {
                        if (_hours == 0)
                        {
                            LEDCollection[0].LEDValue = 1;
                            LEDCollection[1].LEDValue = 2;
                            PM_Text = "AM";
                        }
                        else
                        {
                            LEDCollection[0].LEDValue = _hours / 10;
                            LEDCollection[1].LEDValue = _hours % 10;
                            PM_Text = "AM";
                        }
                    }
                    break;
                case CLOCK_STATUS._24HR:
                    LEDCollection[0].LEDValue = _hours / 10;
                    LEDCollection[1].LEDValue = _hours % 10;
                    PM_Text = "";
                    break;
            }
            LEDCollection[2].LEDValue = _minutes / 10;
            LEDCollection[3].LEDValue = _minutes % 10;
            LEDCollection[4].LEDValue = _seconds / 10;
            LEDCollection[5].LEDValue = _seconds % 10;
        }

        private void CheckForAlarm()
        {

        }

        private void ReceiveThreadFunction()
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
            try
            {
                while (_isThreadReceiveRunning)
                {
                    try
                    {
                        Byte[] receiveData = _dataSocket.Receive(ref endPoint);

                        //DeSerialiser
                        BinaryFormatter formatter = new BinaryFormatter();
                        MemoryStream stream = new MemoryStream();

                        //Update Game Data
                        stream = new System.IO.MemoryStream(receiveData);
                        timeData = (TimeData.StructTimeData)formatter.Deserialize(stream);

                        //Data Handling
                        if (timeData.is24HourTime == true)
                        {
                            currentClockstatus = CLOCK_STATUS._24HR;
                        }
                        else if (timeData.is24HourTime == false)
                        {
                            currentClockstatus = CLOCK_STATUS._12HR;
                        }

                        if (timeData.isAlarmTime == true)
                        {
                            _alarmhours = (UInt32)timeData.hour;
                            _alarmminutes = (UInt32)timeData.minute;
                            _alarmseconds = (UInt32)timeData.second;
                            currentAlarmstatus = ALARM_STATUS.ALARM;
                        }
                        else
                        {
                            _hours = (UInt32)timeData.hour;
                            _minutes = (UInt32)timeData.minute;
                            _seconds = (UInt32)timeData.second;
                        }
                    }
                    catch (SocketException ex)
                    {
                        Console.WriteLine(ex.ToString());
                        return;
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.ToString());
                    }
                }
            }
            catch (ThreadAbortException)
            {
                Console.Write("Thread Aborted\n");
            }
        }
    }
}
