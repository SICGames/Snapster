using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using com.HellStormGames.Imaging.ScreenCapture.Interlop;

namespace com.HellStormGames.Imaging.ScreenCapture
{
    public class DisplayMonitor : IDisposable
    {
        private System.IntPtr _ptrHandle;
        private System.IntPtr _monitorsHandle;

        public DisplayMonitor() 
        { 
            _ptrHandle = SnaptureInvoke.CreateDisplayMonitorClass();
        }
        
        public async Task<MonitorCollection> GetMonitorsAsync()
        {
            return await Task.Run(() => GetMonitors());
        }
        /// <summary>
        /// Gets Collection of attached monitors.
        /// </summary>
        /// <returns></returns>
        public MonitorCollection GetMonitors()
        {
            uint numMonitors = 0;
            IntPtr monitors = IntPtr.Zero;
            MonitorCollection MonitorCollection = new MonitorCollection();

            //-- access violation exception
            SnaptureInvoke.DisplayMonitorGetMonitors(_ptrHandle, out monitors, out numMonitors);
            var sizeInBytes = Marshal.SizeOf(typeof(Monitor));
            
            //--  other monitors not changing values: isPrimary stays true. 
            for(var m = 0; m < numMonitors; m++)
            {
                IntPtr intPtr = IntPtr.Add(monitors, m  * sizeInBytes);
                Monitor tmp_Monitor = Marshal.PtrToStructure<Monitor>(intPtr);
                MonitorCollection.Add(tmp_Monitor); 
            }

            sizeInBytes = 0;
            try
            {
                SnaptureInvoke.FreeMemoryResource(monitors);
            }
            catch (System.Exception ex)
            {

            }
            monitors = IntPtr.Zero;
            
            return MonitorCollection;
        }

        /// <summary>
        /// Cleans up and destroys PtrHandle of wrapped DisplayMonitor
        /// </summary>
        public void Dispose()
        {
            if (_ptrHandle != System.IntPtr.Zero)
            {
                SnaptureInvoke.DestroyDisplayMonitorClass(_ptrHandle);
            }
        }
    }
}
