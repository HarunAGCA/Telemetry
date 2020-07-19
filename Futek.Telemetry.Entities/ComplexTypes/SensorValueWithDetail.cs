using System;
using System.Collections.Generic;
using System.Text;

namespace Futek.Telemetry.Entities.ComplexTypes
{
    public class SensorValueWithDetail
    {
        public int Id { get; set; }
        public string SensorName { get; set; }
        public double Value { get; set; }
        public DateTime ValueReadingTime { get; set; }

    }
}
