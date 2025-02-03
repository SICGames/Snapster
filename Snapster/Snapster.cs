using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.HellStormGames.Imaging.ScreenCapture
{
    /*
     * Snapster.Capturer = new SnapsterConfiguration().UseContext.GdiContext(MonitorCollection.Primary).CreateCapturer();
     * Snapster.Capturer = new SnapsterConfiguration().UseContext.GdiContext().FromMonitorIndex(0).CreateCapturer();
     * Snapster.Capturer = new SnapsterConfiguration(GdiContext, MonitorCollection[0]).CreateCapturer();
    */
    public class Snapster
    {
        static ICapturer _Capturer;
        public static ICapturer Capturer
        {
            get => _Capturer;
            set => _Capturer = value;
        }
        public static IMonitorConfiguration MonitorConfiguration => _Capturer.GetMonitorConfiguration();
        //-- may not be needed in most casess. Only when utilizing Desktop Duplication API.
        public static void Initialize()
        {
            _Capturer?.Initialize();  
        }

        public static ImageData CaptureDesktop()
        {
            return _Capturer.CaptureDesktop();
        }
        public static ImageData CaptureRegion(Region region)
        {
            return _Capturer.CaptureRegion(region);
        }

        public static void Release()
        {
            (_Capturer as IDisposable)?.Dispose();  
        }
    }
}
