using Agca.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Futek.Telemetry.Entities
{
   public class Sensor:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //Entity Framework navigation for relationship
        public List<SensorValue> SensorValues { get; set; }
    }
}
