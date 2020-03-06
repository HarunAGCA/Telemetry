using Futek.Telemetry.Business.Abstract;
using Futek.Telemetry.DataAccess.Abstract;
using Futek.Telemetry.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Futek.Telemetry.Business.Concrete
{
    public class SensorManager:ISensorService
    {
       private ISensorDal _sensorDal;

        public SensorManager(ISensorDal sensorDal)
        {
            _sensorDal = sensorDal;
        }

        public Sensor GetSensor(int sensorId)
        {
            return _sensorDal.Get(s => s.Id == sensorId);
        }
    }
}
