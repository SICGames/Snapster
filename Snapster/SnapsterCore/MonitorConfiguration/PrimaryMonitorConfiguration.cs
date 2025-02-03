using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.HellStormGames.Imaging.ScreenCapture
{
    public class PrimaryMonitorConfiguration : IMonitorConfiguration
    {
        readonly DisplayMonitor _displymonitor;
        readonly MonitorCollection _monitors;
        Monitor _monitor;
        public Monitor Monitor 
        {
            get => _monitor;
            set => _monitor = value;
        }
        public bool InvalidMonitorIndexSelected { get; set; }

        public PrimaryMonitorConfiguration() 
        { 
            _displymonitor = new DisplayMonitor();
            _monitors = _displymonitor.GetMonitors();
            _monitor = _monitors.FirstOrDefault();
        }

        public void Release()
        {
            _displymonitor?.Dispose();
            _monitors?.Dispose();
        }
    }
}
