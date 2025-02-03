using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace com.HellStormGames.Imaging
{
    [StructLayout(LayoutKind.Sequential)]
    public struct DPI
    {
        public DPI()
        {
            this.X = 0;
            this.Y = 0;
        }
        public static DPI Empty { get { return new DPI(); } }
        public UInt32 X;
        public UInt32 Y;
    }
}
