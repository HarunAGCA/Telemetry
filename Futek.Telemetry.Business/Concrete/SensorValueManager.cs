using Futek.Telemetry.Business.Abstract;
using Futek.Telemetry.DataAccess.Abstract;
using Futek.Telemetry.Entities;
using Futek.Telemetry.Entities.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Futek.Telemetry.Business.Concrete
{
    public class SensorValueManager : ISensorValueService
    {
        private ISensorValueDal _sensorValueDal;

        public SensorValueManager(ISensorValueDal sensorValueDal)
        {
            _sensorValueDal = sensorValueDal;       
        }

        public void AddValue(SensorValue value)
        {
            _sensorValueDal.Add(value);
        }

        public SensorValue GetValue(int valueId)
        {
            return _sensorValueDal.Get(sv => sv.Id == valueId);
        }

        public List<SensorValue> GetValues(int sensorId)
        {
            return _sensorValueDal.GetList(sv => sv.SensorId == sensorId);
        }

        public List<SensorValueWithDetail> GetValuesBySensorId(int sensorId)
        {
            return _sensorValueDal.GetValuesWithDetailBySensorId(sensorId);
        }

        public SensorValueWithDetail GetValueWithDetailBySensorId(int sensorId)
        {
            return _sensorValueDal.GetValueWithDetail(sensorId);
        }

        public async Task AddValueAsync(SensorValue value)
        {
            await _sensorValueDal.AddAsync(value);
        }

        public async Task<SensorValue> GetValueAsync(int valueId)
        {
            return await _sensorValueDal.GetAsync(sv => sv.Id == valueId);
        }

        public async Task<List<SensorValue>> GetValuesAsync(int sensorId)
        {
            return await _sensorValueDal.GetListAsync(sv => sv.SensorId == sensorId);
        }

        public async Task<List<SensorValueWithDetail>> GetValuesBySensorIdAsync(int sensorId)
        {
            return await _sensorValueDal.GetValuesWithDetailBySensorIdAsync(sensorId);
        }

        public async Task<SensorValueWithDetail> GetValueWithDetailBySensorIdAsync(int sensorId)
        {
            return await _sensorValueDal.GetValueWithDetailAsync(sensorId);
        }

        public async Task AddRangeAsync(List<SensorValue> sensorValues)
        {
            await _sensorValueDal.AddRangeAsync(sensorValues);
        }
    }
}
