using System;
using System.Collections.Generic;
using System.Text;
using Agca.Core.DataAccess;
using Futek.Telemetry.Entities;
using Futek.Telemetry.Entities.ComplexTypes;

namespace Futek.Telemetry.DataAccess.Abstract
{
    public interface ISensorValueDal:IEntityRepository<SensorValue> 
    {
        SensorValueWithDetail GetValueWithDetail(int valueId);
        List<SensorValueWithDetail> GetValuesWithDetailBySensorId(int sensorId);
    }
}
