using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;

namespace Futek.Telemetry.Hardware.Abstract
{
    public abstract class SerialDataReceiverBase:DataReceiver,ISerialDataReceiver
    {
        protected SerialPort _serialPort;

        public void SetBaudRate(int baudRate)
        {
            if (_serialPort != null)
            {
                _serialPort.BaudRate = baudRate;
            }
        }
        public void SetPortName(string portName)
        {
            if (_serialPort != null)
            {
                _serialPort.PortName = portName;
            }
        }

        public SerialPort GetSerialPort()
        {
            return _serialPort;
        }
    }
}
