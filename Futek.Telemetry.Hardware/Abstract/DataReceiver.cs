using Futek.Telemetry.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Futek.Telemetry.Hardware.Abstract
{
    public abstract class DataReceiver : DataReceiverBase, IDataReceiver
    {
        
        public abstract bool Connect();
        public abstract bool Disconnect();
        public abstract void ParseİncomingValues(string rawString);
        public abstract void StartReceivingData();
        public abstract bool StopReceivingData();
        public abstract bool TestTheCommunication();
        
       
    }
}
