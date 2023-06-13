using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Sockets
using System.Net.Sockets;
using System.Net;

// INotifyPropertyChanged
using System.ComponentModel;


namespace UDP_Client
{
    class Model : INotifyPropertyChanged
    {

        private static UInt32 _remotePort = 5000;
        private static String _remoteIPAddress = "127.0.0.1";

        // UDP Socket that communicates over network
        private UdpClient _dataSocket;

        public TimeDataDLL.TimeData.StructTimeData clockTime;



        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private String _MessageBox;
        public String MessageBox
        {
            get { return _MessageBox; }
            set
            {
                _MessageBox = value;
                OnPropertyChanged("MessageBox");
            }
        }

        private String _statusBox;
        public String StatusBox
        {
            get { return _statusBox; }
            set
            {
                _statusBox = value;
                OnPropertyChanged("StatusBox");
            }
        }

        // added for loopback
        private String _loopbackBox;
        public String LoopbackBox
        {
            get { return _loopbackBox; }
            set
            {
                _loopbackBox = value;
                OnPropertyChanged("LoopbackBox");
            }
        }

        public Model()
        {
            try
            {
                // sets up UDP socket and binds to local port
                _dataSocket = new UdpClient();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
        }

        public void SendMessage()
        {
            clockTime = new TimeDataDLL.TimeData.StructTimeData();

            IPEndPoint remoteHost = new IPEndPoint(IPAddress.Parse(_remoteIPAddress), (int)_remotePort);
            Byte[] sendBytes = Encoding.ASCII.GetBytes(clockTime.ToString());

            try
            {
                _dataSocket.Send(sendBytes, sendBytes.Length, remoteHost);
                StatusBox += DateTime.Now + ":" + "Message Sent Successfully" + "\n";
            }
            catch (SocketException ex)
            {
                StatusBox = StatusBox + DateTime.Now + ":" + ex.ToString();
                return;
            }


            // added for loopback
            try
            {
                Byte[] receiveData = _dataSocket.Receive(ref remoteHost);
                StatusBox += DateTime.Now + ":" + "Received Loopback data" + "\n";
                LoopbackBox += DateTime.Now + ": " + System.Text.Encoding.Default.GetString(receiveData) + "\n";
            }
            catch (SocketException ex)
            {
                StatusBox = StatusBox + DateTime.Now + ":" + ex.ToString();
                return;
            }
        }

    }
}