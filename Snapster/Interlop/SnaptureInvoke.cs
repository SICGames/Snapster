using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Security;

namespace com.HellStormGames.Imaging.ScreenCapture.Interlop
{
    public class SnaptureInvoke
    {
        //-- Image Data
        [DllImport("libSnapture.dll", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
        public static extern void CreateImageDataFromHBITMAP(out ImageData ptr, IntPtr hBitmap);
        [DllImport("libSnapture.dll", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
        public static extern void CreateImageData(out ImageData imageDataPtr, int width, int height, int channels, byte[] data);
        [DllImport("libSnapture.dll", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
        public static extern bool SaveImageDataToFile(ImageData imagedata, [MarshalAs(UnmanagedType.LPWStr)]String filename);
        [DllImport("libSnapture.dll", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
        public static extern void ReleaseImageData(ImageData imagedata);

        //-- Image Data Test functions.
        [DllImport("libSnapture.dll", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
        public static extern void CreateWindowsLogo(ImageData imageData);

        //-- Snapster
        [DllImport("libSnapture.dll", ExactSpelling = true, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint ="CaptureDesktopScreen")]
        [SuppressUnmanagedCodeSecurity]
        public static extern IntPtr CaptureDesktopScreen(int x, int y, int width, int height);
        [DllImport("libSnapture.dll", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
        public static extern IntPtr CaptureRegion(int x, int y, int width, int height, int screenBoundsX, int screenBoundsY, int screenBoundsWidth, int screenBoundsHeight);
        [DllImport("libSnapture.dll", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl, EntryPoint ="ReleaseCapturedBitmap")]
        [SuppressUnmanagedCodeSecurity]
        public static extern void ReleaseCapturedBitmap(IntPtr bitmap);
        [DllImport("libSnapture.dll", ExactSpelling = true, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int InitDesktopDuplicationCapture(Monitor monitor);
        [DllImport("libSnapture.dll", ExactSpelling = true, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool DesktopDuplicationCaptureDesktop(out IntPtr bitmap);
        [DllImport("libSnapture.dll", ExactSpelling = true, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ReleaseDesktopDuplicationResources();
    }
}
