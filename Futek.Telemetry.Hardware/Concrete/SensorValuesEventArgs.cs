using Futek.Telemetry.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Futek.Telemetry.Hardware
{
    public class SensorValuesEventArgs : EventArgs
    {
        public List<SensorValue> SensorValues { get; set; }
    }
}
