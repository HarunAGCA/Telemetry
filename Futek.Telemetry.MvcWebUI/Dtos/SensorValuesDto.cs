using Futek.Telemetry.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Futek.Telemetry.MvcWebUI.Dtos
{
    public class SensorValuesDto
    {
        public List<SensorValue> SensorValues { get; set; }
    }
}
