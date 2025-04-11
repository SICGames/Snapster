using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using com.HellStormGames.Imaging.ScreenCapture.Interlop;
using com.HellStormGames.Interlop;
using com.HellStormGames.Interlop.Native;
using static com.HellStormGames.Interlop.NativeMonitors;


namespace com.HellStormGames.Imaging.ScreenCapture
{
    public class DisplayMonitor : IDisposable
    {
      
        public DisplayMonitor() 
        { 
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

            NativeMonitors.EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, delegate (IntPtr hMonitor, IntPtr hdcMonitor, ref Rect lprcMonitor, IntPtr dwData)
            {
                MonitorInfoEx monitorInfo = new MonitorInfoEx();
                monitorInfo.Size = Marshal.SizeOf(typeof(MonitorInfoEx));
                if (NativeMonitors.GetMonitorInfo(hMonitor, ref monitorInfo))
                {
                    Monitor monitor = new Monitor();
                    monitor.ID = MonitorCollection.Count();
                    monitor.Name = monitorInfo.DeviceName;
                    
                    DISPLAY_DEVICE dd = new DISPLAY_DEVICE();
                    dd.cb = Marshal.SizeOf(dd);
                    EnumDisplayDevices(null, (UInt32)monitor.ID, ref dd, 0);
                    monitor.DeviceName = dd.DeviceString;

                    monitor.ScreenBounds = new Region(monitorInfo.Monitor.Left, monitorInfo.Monitor.Top, monitorInfo.Monitor.Width - monitorInfo.Monitor.Left, monitorInfo.Monitor.Height - monitorInfo.Monitor.Top);
                    monitor.WorkAreaBounds = new Region(monitorInfo.WorkArea.Left, monitorInfo.WorkArea.Top, monitorInfo.WorkArea.Width - monitorInfo.WorkArea.Left, monitorInfo.WorkArea.Height - monitor.WorkAreaBounds.X);
                        
                    monitor.isPrimary = monitorInfo.Flags == 1 ? 1 : 0;
                    int dpix, dpiy;
                    DPI dpi = DPI.Empty;
                    GetDpiForMonitor(hMonitor, MonitorDpiType.MDT_Effective, out dpix, out dpiy);
                    dpi.X = (UInt32)dpix;
                    dpi.Y = (UInt32)dpiy;

                    monitor.Dpi = dpi;
                    var monitornames = GetMonitorFriendlyNames();
                    monitor.MonitorName = monitornames[monitor.ID];

                    monitor.MonitorHandle = hMonitor;
                    monitor.hDCMonitor = hdcMonitor;
                    MonitorCollection.Add(monitor);

                }
                return true;
            }, IntPtr.Zero);

            return MonitorCollection;
        }

        private List<string> GetMonitorFriendlyNames()
        {
            List<string> monitorNames = new List<string>();
            uint PathCount, ModeCount;

            int error = GetDisplayConfigBufferSizes(QUERY_DEVICE_CONFIG_FLAGS.QDC_ONLY_ACTIVE_PATHS,
                out PathCount, out ModeCount);
            if (error != ERROR_SUCCESS)
                throw new Win32Exception(error);

            DISPLAYCONFIG_PATH_INFO[] DisplayPaths = new DISPLAYCONFIG_PATH_INFO[PathCount];
            DISPLAYCONFIG_MODE_INFO[] DisplayModes = new DISPLAYCONFIG_MODE_INFO[ModeCount];
            error = QueryDisplayConfig(QUERY_DEVICE_CONFIG_FLAGS.QDC_ONLY_ACTIVE_PATHS,
                ref PathCount, DisplayPaths, ref ModeCount, DisplayModes, IntPtr.Zero);
            if (error != ERROR_SUCCESS)
                throw new Win32Exception(error);

            for (int i = 0; i < ModeCount; i++)
            {
                if (DisplayModes[i].infoType == DISPLAYCONFIG_MODE_INFO_TYPE.DISPLAYCONFIG_MODE_INFO_TYPE_TARGET)
                {
                    var monitorName = MonitorFriendlyName(DisplayModes[i].adapterId, DisplayModes[i].id);
                    if(String.IsNullOrEmpty(monitorName) == false)
                    {
                        monitorNames.Add(monitorName);
                    }
                }
            }
            return monitorNames;
        }
        private string MonitorFriendlyName(LUID adapterId, uint targetId)
        {
            
            DISPLAYCONFIG_TARGET_DEVICE_NAME deviceName = new DISPLAYCONFIG_TARGET_DEVICE_NAME();
            deviceName.header.size = (uint)Marshal.SizeOf(typeof(DISPLAYCONFIG_TARGET_DEVICE_NAME));
            deviceName.header.adapterId = adapterId;
            deviceName.header.id = targetId;
            deviceName.header.type = DISPLAYCONFIG_DEVICE_INFO_TYPE.DISPLAYCONFIG_DEVICE_INFO_GET_TARGET_NAME;
            int error = DisplayConfigGetDeviceInfo(ref deviceName);
            if (error != ERROR_SUCCESS)
                throw new Win32Exception(error);
            return deviceName.monitorFriendlyDeviceName;
        }

        /// <summary>
        /// Cleans up and destroys PtrHandle of wrapped DisplayMonitor
        /// </summary>
        public void Dispose()
        {
         
        }
    }
}
