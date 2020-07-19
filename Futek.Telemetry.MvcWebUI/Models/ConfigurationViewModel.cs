using System.IO.Ports;

namespace Futek.Telemetry.MvcWebUI.Models
{
    public class ConfigurationViewModel
    {
        public string PortName { get; set; }
        public int BaudRate { get; set; }
    }
}