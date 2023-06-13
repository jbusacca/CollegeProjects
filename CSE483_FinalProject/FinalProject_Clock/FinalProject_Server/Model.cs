﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Net.Sockets;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using TimeDataDLL;

namespace FinalProject_Server
{
    class Model : INotifyPropertyChanged
    {
        private static UInt32 _remotePort = 5000;
        private static String _remoteIPAddress = "127.0.0.1";

        private UdpClient _dataSocket;

        public TimeData.StructTimeData clockTime;

        Byte[] sendBytes;

        private bool _timeFormat;
        public bool TimeFormat
        {
            get { return _timeFormat; }
            set
            {
                _timeFormat = value;
                OnPropertyChanged("TimeFormat");
            }
        }

        private string _hourInput;
        public string HourInput
        {
            get { return _hourInput; }
            set
            {
                _hourInput = value;
                OnPropertyChanged("HourInput");
            }
        }

        private string _minuteInput;
        public string MinuteInput
        {
            get { return _minuteInput; }
            set
            {
                _minuteInput = value;
                OnPropertyChanged("MinuteInput");
            }
        }

        private string _secondInput;
        public string SecondInput
        {
            get { return _secondInput; }
            set
            {
                _secondInput = value;
                OnPropertyChanged("SecondInput");
            }
        }
        public Model()
        {
            try
            {
                _dataSocket = new UdpClient();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
        }
        public void Send(string button)
        {
            IPEndPoint remoteHost = new IPEndPoint(IPAddress.Parse(_remoteIPAddress), (int)_remotePort);

            BinaryFormatter format = new BinaryFormatter();
            MemoryStream memStream = new MemoryStream();

            switch (button)
            {
                case "setTimeButton":
                    clockTime = new TimeDataDLL.TimeData.StructTimeData(int.Parse(_hourInput), int.Parse(_minuteInput), int.Parse(_secondInput), false, _timeFormat);
                    format.Serialize(memStream, clockTime);
                    sendBytes = memStream.ToArray();
                    //sendBytes = new Byte[] { (Byte)int.Parse(_hourInput), (Byte)int.Parse(_minuteInput), (Byte)int.Parse(_secondInput), (Byte)Convert.ToInt32(false), (Byte)Convert.ToInt32(_timeFormat) };
                    break;
                case "setNowButton":
                    DateTime timeNow = DateTime.Now;
                    clockTime = new TimeDataDLL.TimeData.StructTimeData(timeNow.Hour, timeNow.Minute, timeNow.Second, false, _timeFormat);
                    format.Serialize(memStream, clockTime);
                    sendBytes = memStream.ToArray();
                    //sendBytes = new Byte[] { (Byte)timeNow.Hour, (Byte)timeNow.Minute, (Byte)timeNow.Second, (Byte)Convert.ToInt32(false), (Byte)Convert.ToInt32(_timeFormat) };
                    break;
                case "setAlarmButton":
                    clockTime = new TimeDataDLL.TimeData.StructTimeData(int.Parse(_hourInput), int.Parse(_minuteInput), int.Parse(_secondInput), true, _timeFormat);
                    format.Serialize(memStream, clockTime);
                    sendBytes = memStream.ToArray();
                    //clockTime = new TimeDataDLL.TimeData.StructTimeData(int.Parse(_hourInput), int.Parse(_minuteInput), int.Parse(_secondInput), true, _timeFormat);
                    //sendBytes = new Byte[] { (Byte)int.Parse(_hourInput), (Byte)int.Parse(_minuteInput), (Byte)int.Parse(_secondInput), (Byte)Convert.ToInt32(true), (Byte)Convert.ToInt32(_timeFormat) };
                    break;
                default:
                    sendBytes = new Byte[] { };
                    break;
            }

            try
            {
                _dataSocket.Send(sendBytes, sendBytes.Length, remoteHost);
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.ToString());
                return;
            }
        }

        //switch statement, takes button name as input param. 
        // if setTimeButton, sets entered values w/ new instance 
        // if setNow, method gets current system time
        // if setAlarm, method creates new DLL struct w/ entered values 
        //otherwise send empty byte array 

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }

        }
        //notify GUI property change
    }
}

