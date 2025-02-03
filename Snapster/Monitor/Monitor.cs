﻿using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace com.HellStormGames.Imaging.ScreenCapture
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
    public struct Monitor
    {
        public Monitor()
        {
            ID = 0;
            Name = String.Empty;
            DeviceName = String.Empty;
            MonitorName = String.Empty; 
            MonitorHandle = IntPtr.Zero;
            hDCMonitor = IntPtr.Zero;
            ScreenBounds = Region.Empty;
            WorkAreaBounds = Region.Empty;
            Dpi = DPI.Empty;
            isPrimary = 0;
        }
        public Int32 ID;
        [MarshalAs(UnmanagedType.LPWStr)]
        public String Name;
        [MarshalAs(UnmanagedType.LPWStr)]
        public String DeviceName;
        [MarshalAs(UnmanagedType.LPWStr)]
        public String MonitorName;
        public IntPtr MonitorHandle;
        public IntPtr hDCMonitor;
        public Region ScreenBounds;
        public Region WorkAreaBounds;
        public DPI Dpi;
        public Int32 isPrimary;
        public bool IsPrimary
        {
            get
            {
                return isPrimary != 0;
            }
        }
    }
}
