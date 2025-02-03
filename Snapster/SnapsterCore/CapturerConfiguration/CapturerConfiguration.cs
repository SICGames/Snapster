using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.HellStormGames.Imaging.ScreenCapture
{
    public class CapturerConfiguration
    {
        readonly SnapsterConfiguration _SnapsterConfiguration;
        readonly Action<ICapturerContext> _capturercontext;
        readonly IMonitorConfiguration _monitorConfiguration;
        public IMonitorConfiguration MonitorConfiguration  => _monitorConfiguration;
        public CapturerConfiguration(SnapsterConfiguration configuration, Action<ICapturerContext> capturercontext) 
        { 
            _SnapsterConfiguration = configuration;
            _capturercontext = capturercontext;
        }
        public SnapsterConfiguration Commit(ICapturerContext context)
        {
            var capturecontext = context;
            _capturercontext(capturecontext);
            return _SnapsterConfiguration;
        }
    }
}
