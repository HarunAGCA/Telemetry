using Futek.Telemetry.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Futek.Telemetry.DataAccess.Concrete.EntityFramework
{
    public class TelemetryContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=HARUN;Database=Futek.Telemetry;User ID=sa;Password=VTYS;");
        }

        public DbSet<Sensor> Sensors { get; set; }  
        public DbSet<SensorValue> SensorValues { get; set; }
    }


}
