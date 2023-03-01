namespace TicketsScanner
{
    using System;
    using System.IO.Ports;

    public class Scanner
    {
        public string LastTicket { get; private set; }

        private readonly string _comPort;
        private SerialPort _serialPort;

        public Scanner(string comPort)
        {
            _comPort = comPort;

            _serialPort = new SerialPort(_comPort);

            if (_serialPort.IsOpen)
            {
                _serialPort.Close();
            }
        }

        public bool Start()
        {
            try
            {
                LastTicket = string.Empty;

                _serialPort.BaudRate = 9600;
                _serialPort.Parity = Parity.None;
                _serialPort.StopBits = StopBits.One;
                _serialPort.DataBits = 8;
                _serialPort.Handshake = Handshake.None;
                _serialPort.RtsEnable = true;
                _serialPort.Open();

                _serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

                return true;
            }
            catch (Exception message)
            {
                return false;
            }
        }

        public void Stop()
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.Close();
            }
        }

        public string GetNewTicket()
        {
            var temp = LastTicket;
            LastTicket = string.Empty;
            
            if(temp != string.Empty)
            {
                _serialPort.Close();
            }

            if (temp.Contains("\n"))
            {
                var checkedTemp = temp.Split('\n');
                temp = checkedTemp[checkedTemp.Length - 1];
            }

            return temp;
        }

        private void DataReceivedHandler(
                                object sender,
                                SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;

            LastTicket = sp.ReadExisting();
        }
    }
}