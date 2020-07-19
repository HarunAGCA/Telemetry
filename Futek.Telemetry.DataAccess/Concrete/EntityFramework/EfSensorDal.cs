
using Futek.Core.DataAccess.EntityFramework;
using Futek.Telemetry.DataAccess.Abstract;
using Futek.Telemetry.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Futek.Telemetry.DataAccess.Concrete.EntityFramework
{
    public class EfSensorDal:EfEntityRepositoryBase<Sensor,TelemetryContext>,ISensorDal
    {
    }
}
