using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.HellStormGames.Imaging.ScreenCapture
{
    public class MonitorSelectionConfiguration : IMonitorConfiguration
    {
        Monitor _monitor;
        public Monitor Monitor
        {
            get => _monitor;
            set => _monitor = value;
        }

        readonly DisplayMonitor _displaymonitor;
        readonly MonitorCollection _monitorcollection;
        public bool InvalidMonitorIndexSelected { get; set; }

        public MonitorSelectionConfiguration(int value)
        {
            try
            {
                _displaymonitor = new DisplayMonitor();
                _monitorcollection = _displaymonitor.GetMonitors();
                _monitor = _monitorcollection.Get(value);
                InvalidMonitorIndexSelected = false;
            }
            catch (Exception ex)
            {
                _monitor = _monitorcollection.Get(0);
                InvalidMonitorIndexSelected = true;
            }
        }

        public void Release()
        {
            _displaymonitor?.Dispose();
            _monitorcollection?.Dispose();  
        }
    }
}
