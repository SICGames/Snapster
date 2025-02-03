using com.HellStormGames.Imaging.ScreenCapture.Interlop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace com.HellStormGames.Imaging
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ImageData : IDisposable
    {
        public int Width;
        public int Height;
        public int Channels;
        public int Stride;
        public int BitsPerPixel;
        public int RowSize;
        public int Size;
        public IntPtr Data;
        
        public void CreateWindowsLogo()
        {
            SnaptureInvoke.CreateWindowsLogo(this);
        }

        public bool Save(String filename)
        {
            return SnaptureInvoke.SaveImageDataToFile(this, filename);
        }

        public void Dispose()
        {
            Data = IntPtr.Zero;
            Width = Height = Channels = Stride = BitsPerPixel = RowSize = Size = 0;
        }

    }
}
