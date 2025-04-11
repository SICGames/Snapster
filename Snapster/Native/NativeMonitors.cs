using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using com.HellStormGames.Interlop;
using com.HellStormGames.Interlop.Native;


namespace com.HellStormGames.Interlop
{
    public class NativeMonitors
    {
        public const int ERROR_SUCCESS = 0;

        public delegate bool EnumMonitorsDelegate(IntPtr hMonitor, IntPtr hdcMonitor, ref Rect lprcMonitor, IntPtr dwData);
        [DllImport("user32.dll")]
        public static extern int GetDisplayConfigBufferSizes(
          QUERY_DEVICE_CONFIG_FLAGS Flags,
          out uint NumPathArrayElements,
          out uint NumModeInfoArrayElements
      );

        [DllImport("user32.dll")]
        public static extern int QueryDisplayConfig(
            QUERY_DEVICE_CONFIG_FLAGS Flags,
            ref uint NumPathArrayElements,
            [Out] DISPLAYCONFIG_PATH_INFO[] PathInfoArray,
            ref uint NumModeInfoArrayElements,
            [Out] DISPLAYCONFIG_MODE_INFO[] ModeInfoArray,
            IntPtr CurrentTopologyId
        );

        [DllImport("user32.dll")]
        public static extern int DisplayConfigGetDeviceInfo(
            ref DISPLAYCONFIG_TARGET_DEVICE_NAME deviceName
        );

        // EnumDisplayDevices function
        [DllImport(SnapsterInterlop.User32, CharSet = CharSet.Ansi)]
        public static extern bool EnumDisplayDevices(string lpDevice, uint iDevNum, ref DISPLAY_DEVICE lpDisplayDevice, uint dwFlags);

        [DllImport(SnapsterInterlop.ShCore, CharSet = CharSet.Auto)]
        public static extern IntPtr GetDpiForMonitor(IntPtr hMonitor, MonitorDpiType dpiType, out int lpDpiX, out int lpDpiY);

        [DllImport(SnapsterInterlop.User32, CharSet = CharSet.Auto)]
        public static extern bool GetMonitorInfo(IntPtr hMonitor, ref MonitorInfoEx lpmi);

        [DllImport(SnapsterInterlop.User32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumDisplayMonitors(IntPtr hdc,
                                                      IntPtr lprcClip,
                                                      EnumMonitorsDelegate lpfnEnum,
                                                      IntPtr dwData);
    }
}
