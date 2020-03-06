using Futek.Telemetry.MvcWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Futek.Telemetry.MvcWebUI.Dtos
{
    public class ChartDto
    {
        public InstantSpeedChartModel InstantSpeedChartModel { get; set; }
        public SpeedHistoryChartModel SpeedHistoryChartModel { get; set; }
        public InstantTempratureChartModel InstantTempratureChartModel { get; set; }
        public TempratureHistoryChartModel TempratureHistoryChartModel { get; set; }

    }
}
