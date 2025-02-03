using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace com.HellStormGames.Imaging.ScreenCapture
{
    public class MonitorConfiguration : IMonitorConfiguration
    {
        public static IMonitorConfiguration MainMonitor = new PrimaryMonitorConfiguration();
        private static IMonitorConfiguration _configuration;
        Monitor _monitor;
        public Monitor Monitor
        {
            get => _monitor;
            set => _monitor = value;
        }
        public bool InvalidMonitorIndexSelected { get; set; }

        public static IMonitorConfiguration ByIndex(int value)
        {
            _configuration = new MonitorSelectionConfiguration(value);

            return _configuration;
        }
        public void Release()
        {
            MainMonitor.Release();
            _configuration.Release();
        }
    }
}
