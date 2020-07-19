using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;

namespace Futek.Telemetry.Hardware.Abstract
{
    public interface ISerialDataReceiver:IDataReceiver
    {
        void SetBaudRate(int baudRate);
        void SetPortName(string portName);
        SerialPort GetSerialPort();
    }
}
