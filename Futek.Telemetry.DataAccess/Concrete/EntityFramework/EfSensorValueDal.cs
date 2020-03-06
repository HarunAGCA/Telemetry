using Agca.Core.DataAccess.EntityFramework;
using Futek.Telemetry.DataAccess.Abstract;
using Futek.Telemetry.Entities;
using Futek.Telemetry.Entities.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Futek.Telemetry.DataAccess.Concrete.EntityFramework
{
    public class EfSensorValueDal : EfEntityRepositoryBase<SensorValue, TelemetryContext>, ISensorValueDal
    {
        public SensorValueWithDetail GetValueWithDetail(int valueId)
        {
            SensorValueWithDetail result;
            using (var context = new TelemetryContext())
            {
                result = (from sensor in context.Sensors
                         join sensorValue in context.SensorValues
                         on sensor equals sensorValue.Sensor
                         where sensorValue.Id == valueId
                         
                         select new SensorValueWithDetail
                         {
                             Id = sensorValue.Id,
                             SensorName = sensor.Name,
                             Value = sensorValue.Value,
                             ValueReadingTime = sensorValue.ValueReadingTime
                         }).Single();

            }

            return result;

        }

        public List<SensorValueWithDetail> GetValuesWithDetailBySensorId(int sensorId)
        {
            List<SensorValueWithDetail> result;

            using (var context = new TelemetryContext())
            {
                result = (from sensor in context.Sensors
                          join value in context.SensorValues
                          on sensor equals value.Sensor
                          where sensor.Id == sensorId
                          select new SensorValueWithDetail
                          {
                              Id = value.Id,
                              SensorName = sensor.Name,
                              Value = value.Value,
                              ValueReadingTime = value.ValueReadingTime
                          }).ToList();
            }


            return result;
        }


    }
}
