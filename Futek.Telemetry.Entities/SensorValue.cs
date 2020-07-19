using Futek.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Futek.Telemetry.Entities
{
    public class SensorValue:IEntity
    {
        public int Id { get; set; }
        public DateTime ValueReadingTime { get; set; }
        public double Value { get; set; }

        //Entity Framework navigations for relationship
        public Sensor Sensor { get; set; }
        public int SensorId { get; set; }

    }
}
