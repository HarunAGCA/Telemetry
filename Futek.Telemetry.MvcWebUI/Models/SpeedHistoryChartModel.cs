using Futek.Telemetry.Entities.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Futek.Telemetry.MvcWebUI.Models
{
    public class SpeedHistoryChartModel
    {
        public string ChartLabel { get; set; }
        public string XAxisLabel { get; set; }
        public string YAxisLabel { get; set; }
        public List<float> Values { get; set; }
        public List<DateTime> ReadingTimes { get; set; }
    }
}
