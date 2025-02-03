using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.HellStormGames.Imaging.ScreenCapture
{
    public interface ICapturer
    {
        void Initialize();
        ImageData CaptureDesktop();
        ImageData CaptureRegion(Region region);
        IMonitorConfiguration GetMonitorConfiguration();
    }
}
