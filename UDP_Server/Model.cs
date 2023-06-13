using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Sockets
using System.Net.Sockets;
using System.Net;

// Threads
using System.Threading;

// INotifyPropertyChanged
using System.ComponentModel;

namespace UDP_Server
{
    class Model : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private static int _localPort = 5000;
        private static string _localIPAddress = "127.0.0.1";

        // thread running in background waiting to receive data
        private Thread _receiveDataThread;

        // UDP socket that communicates over network
        UdpClient _dataSocket;


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

        public Model()
        {
            _dataSocket = new UdpClient(_localPort);

            // starts thread to wait for data to come in 
            ThreadStart threadFunction = new ThreadStart(ReceiveThreadFunction);
            _receiveDataThread = new Thread(threadFunction);
            _receiveDataThread.Start();
        }

        public void CloseServer()
        {
            // closes socket so app can exit
            if (_dataSocket != null) _dataSocket.Close();
            if (_receiveDataThread != null) _receiveDataThread.Abort();
        }

        // thread that waits for incoming messages
        private void ReceiveThreadFunction()
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(_localIPAddress), (int)_localPort);
            while (true)
            {
                try
                {
                    Byte[] receiveData = _dataSocket.Receive(ref endPoint);
                    MessageBox += DateTime.Now + ": Received - " + System.Text.Encoding.Default.GetString(receiveData) + "\n";
                }
                catch (SocketException ex)
                {
                    Console.WriteLine(ex.ToString());
                    return;
                }



                try
                {
                    Byte[] sendBytes = Encoding.ASCII.GetBytes(MessageBox);
                    _dataSocket.Send(sendBytes, sendBytes.Length, endPoint);
                    MessageBox += DateTime.Now + ": Sent - " + System.Text.Encoding.Default.GetString(sendBytes) + "\n";
                }
                catch (SocketException ex)
                {
                    Console.WriteLine(ex.ToString());
                    return;
                }
            }
        }
    }
}