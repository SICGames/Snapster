﻿using com.HellStormGames.Imaging.ScreenCapture.Interlop;
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
        public WindowsGDICaptureContext(IMonitorConfiguration monitorConfiguration) 
        { 
            _monitorConfiguration = monitorConfiguration;
        }

        public ImageData Commit(CaptureEvent captureEvent)
        {
            IntPtr bitmap = IntPtr.Zero;
            if(captureEvent.CaptureType == CaptureType.Desktop)
            {
                bitmap = SnaptureInvoke.CaptureDesktopScreen(_monitorConfiguration.Monitor);
            }
            else if(captureEvent.CaptureType == CaptureType.Region)
            {
                bitmap = SnaptureInvoke.CaptureRegion(_monitorConfiguration.Monitor, 
                    captureEvent.Region.X,
                    captureEvent.Region.Y, 
                    captureEvent.Region.Width, 
                    captureEvent.Region.Height);
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
