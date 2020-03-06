using Futek.Telemetry.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Futek.Telemetry.Business.Abstract
{
    public interface ISensorService
    {
        Sensor GetSensor(int sensorId);
    }
}
