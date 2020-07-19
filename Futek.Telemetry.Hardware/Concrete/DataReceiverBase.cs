using Futek.Telemetry.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Futek.Telemetry.Hardware.Abstract
{
    public class DataReceiverBase
    {
        public bool IsConnected { get; protected set; } = false;
        public bool IsTestOk { get; protected set; } = false;
        public bool IsDataReceiving { get; protected set; } = false;

        protected const int ConnectionTimeout = 5000;
        protected const int DataReceivingTimeout = 500;

        public EventHandler<SensorValuesEventArgs> ReceivingDataParsed;

        protected virtual void OnRecevingDataParsed(List<SensorValue> sensorValuesArgs)
        {
            if (ReceivingDataParsed != null)
            {
                ReceivingDataParsed(this, new SensorValuesEventArgs { SensorValues = sensorValuesArgs });
            }
        }

    }


}
