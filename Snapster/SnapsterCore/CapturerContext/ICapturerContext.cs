using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.HellStormGames.Imaging.ScreenCapture
{
    public interface ICapturerContext
    {
        public ImageData Commit(CaptureEvent captureEvent);
        public IMonitorConfiguration GetMonitorConfiguration();
    }
}
