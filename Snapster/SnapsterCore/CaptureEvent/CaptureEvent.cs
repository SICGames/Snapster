using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.HellStormGames.Imaging.ScreenCapture
{
    public class CaptureEvent
    {
        public Region Region { get; private set; }
        public CaptureType  CaptureType { get; private set; }

        public CaptureEvent(CaptureType type, Region region)
        {
            CaptureType = type;
            Region = region;
        }   
    }
}
