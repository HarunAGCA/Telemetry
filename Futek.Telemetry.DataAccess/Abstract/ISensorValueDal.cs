using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Futek.Core.DataAccess;
using Futek.Telemetry.Entities;
using Futek.Telemetry.Entities.ComplexTypes;

namespace Futek.Telemetry.DataAccess.Abstract
{
    public interface ISensorValueDal:IEntityRepository<SensorValue> 
    {
        SensorValueWithDetail GetValueWithDetail(int valueId);
        List<SensorValueWithDetail> GetValuesWithDetailBySensorId(int sensorId);

        Task<SensorValueWithDetail> GetValueWithDetailAsync(int valueId);
        Task<List<SensorValueWithDetail>> GetValuesWithDetailBySensorIdAsync(int sensorId);

    }
}
