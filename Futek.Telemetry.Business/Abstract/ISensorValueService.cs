using Futek.Telemetry.Entities;
using Futek.Telemetry.Entities.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Futek.Telemetry.Business.Abstract
{
    public interface ISensorValueService
    {
        SensorValue GetValue(int valueId);
        List<SensorValue> GetValues(int sensorId);
        List<SensorValueWithDetail> GetValuesBySensorId(int sensorId);
        SensorValueWithDetail GetValueWithDetailBySensorId(int sensorId);
        void AddValue(SensorValue value);
    }
}
