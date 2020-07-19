using Futek.Telemetry.Entities;
using Futek.Telemetry.Entities.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Futek.Telemetry.Business.Abstract
{
    public interface ISensorValueService
    {
        SensorValue GetValue(int valueId);
        List<SensorValue> GetValues(int sensorId);
        List<SensorValueWithDetail> GetValuesBySensorId(int sensorId);
        SensorValueWithDetail GetValueWithDetailBySensorId(int sensorId);
        void AddValue(SensorValue value);

        Task AddValueAsync(SensorValue value);

        Task<SensorValue> GetValueAsync(int valueId);

        Task<List<SensorValue>> GetValuesAsync(int sensorId);

        Task<List<SensorValueWithDetail>> GetValuesBySensorIdAsync(int sensorId);

        Task<SensorValueWithDetail> GetValueWithDetailBySensorIdAsync(int sensorId);

        Task AddRangeAsync(List<SensorValue> sensorValues);
    }
}
