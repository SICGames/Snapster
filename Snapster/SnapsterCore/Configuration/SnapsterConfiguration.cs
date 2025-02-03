using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.HellStormGames.Imaging.ScreenCapture
{
    public class SnapsterConfiguration
    {
        readonly List<ICapturerContext> _capturerContexts = new List<ICapturerContext>();
        public CapturerConfiguration CapturerContext { get; private set; }
        public IMonitorConfiguration MonitorConfiguration { get; private set; }
        private bool CreatedCapturer = false;

        public SnapsterConfiguration() 
        {
            
            //-- only allow 1 Capturer Context.
            CapturerContext = new CapturerConfiguration(this, c =>
            {
                if (_capturerContexts.Count > 1)
                {
                    _capturerContexts.Clear();
                }
                _capturerContexts.Add(c);
            });
        }

        public Capturer CreateCapturer()
        {
            if(CreatedCapturer)
            {
                throw new Exception("Too many Capturers.");
            }

            CreatedCapturer = true;

            ICapturerContext capturer = null;
            if (_capturerContexts.Count > 0)
            {
                capturer = _capturerContexts.First();
            }

            void Dispose()
            {
                foreach (var context in _capturerContexts)
                {
                    (context as IDisposable).Dispose();
                }
            }

            return new Capturer(capturer, Dispose);
        }
    }
}
