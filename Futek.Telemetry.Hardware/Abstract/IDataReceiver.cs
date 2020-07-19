using System;
using System.Collections.Generic;
using System.Text;

namespace Futek.Telemetry.Hardware.Abstract
{
    public interface IDataReceiver
    {
        public bool Connect();
        public bool Disconnect();
        public bool TestTheCommunication();
        public void StartReceivingData();
        public bool StopReceivingData();
        public void ParseİncomingValues(string rawString);


    }
}
