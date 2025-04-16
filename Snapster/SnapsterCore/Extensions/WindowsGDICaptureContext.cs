using com.HellStormGames.Imaging.ScreenCapture.Interlop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.HellStormGames.Imaging.ScreenCapture
{
    public class WindowsGDICaptureContext : ICapturerContext, IDisposable
    {
        readonly IMonitorConfiguration _monitorConfiguration;
        readonly double _dpi;
        public double DPI => _dpi;
        public WindowsGDICaptureContext(IMonitorConfiguration monitorConfiguration, double DPI = 300) 
        { 
            _monitorConfiguration = monitorConfiguration;
            _dpi = DPI;
        }

        public ImageData Commit(CaptureEvent captureEvent)
        {
            IntPtr bitmap = IntPtr.Zero;
            if(captureEvent.CaptureType == CaptureType.Desktop)
            {
                bitmap = SnaptureInvoke.CaptureDesktopScreen(_monitorConfiguration.Monitor.ScreenBounds.X, 
                    _monitorConfiguration.Monitor.ScreenBounds.Y, 
                    _monitorConfiguration.Monitor.ScreenBounds.Width, 
                    _monitorConfiguration.Monitor.ScreenBounds.Height);
            }
            else if(captureEvent.CaptureType == CaptureType.Region)
            {
                bitmap = SnaptureInvoke.CaptureRegion(captureEvent.Region.X,
                    captureEvent.Region.Y, 
                    captureEvent.Region.Width, 
                    captureEvent.Region.Height,
                    _monitorConfiguration.Monitor.ScreenBounds.X,
                    _monitorConfiguration.Monitor.ScreenBounds.Y,
                    _monitorConfiguration.Monitor.ScreenBounds.Width,
                    _monitorConfiguration.Monitor.ScreenBounds.Height);
            }


            ImageData imagedata = ImageFactory.FromHBitmap(bitmap);

            SnaptureInvoke.ReleaseCapturedBitmap(bitmap);
            return imagedata;
        }

        public void Dispose()
        {
            _monitorConfiguration.Release();
        }

        public IMonitorConfiguration GetMonitorConfiguration()
        {
            return _monitorConfiguration;
        }
    }
}
