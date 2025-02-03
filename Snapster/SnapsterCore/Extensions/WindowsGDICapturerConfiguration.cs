using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace com.HellStormGames.Imaging.ScreenCapture
{
    public static class WindowsGDICapturerConfiguration
    {
        public static SnapsterConfiguration WindowsGDI(this CapturerConfiguration configuration, IMonitorConfiguration monitorConfiguration)
        {
            return CreateWindowsGDI(configuration.Commit, monitorConfiguration);
        }
        static SnapsterConfiguration CreateWindowsGDI(
            this Func<ICapturerContext,SnapsterConfiguration> addContext,
            IMonitorConfiguration monitorConfiguration)
        {
            if (addContext == null)
            {
                throw new ArgumentNullException(nameof(addContext));
            }
            ICapturerContext context = new WindowsGDICaptureContext(monitorConfiguration);

            return addContext(context);
        }
    }
}
