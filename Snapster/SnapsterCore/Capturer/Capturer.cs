using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.HellStormGames.Imaging.ScreenCapture
{
    public class Capturer : ICapturer, ICapturerContext, IDisposable
    {
        readonly ICapturerContext capturercontext;
        readonly Action? dispose;

        public Capturer(ICapturerContext capturer, Action dispose)
        {
            this.capturercontext = capturer;
            this.dispose = dispose;
        }

        public ImageData CaptureDesktop()
        {
            var capture_event = new CaptureEvent(CaptureType.Desktop,com.HellStormGames.Imaging.Region.Empty);
            return SendCommit(capture_event);
        }

        public ImageData CaptureRegion(Region region)
        {
            var capture_event = new CaptureEvent(CaptureType.Region, region);
            return SendCommit(capture_event);
        }

        public ImageData SendCommit(CaptureEvent captureevent) {
            return Commit(captureevent);
        }
        public void Dispose()
        {
            dispose?.Invoke();
        }

        public ImageData Commit(CaptureEvent captureEvent)
        {
            return capturercontext.Commit(captureEvent);
        }

        public void Initialize()
        {
            
        }

        public IMonitorConfiguration GetMonitorConfiguration()
        {
            return capturercontext.GetMonitorConfiguration();
        }
    }
}
