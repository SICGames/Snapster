using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace com.HellStormGames.Imaging
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Region
    {
        public Region()
        {
            this.X = 0;
            this.Y = 0;
            this.Width = 0;
            this.Height = 0;
        }
        public Region(int x, int y, int width, int height) 
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }
        public Region(Region region)
        {
            this.X = region.X;
            this.Y = region.Y;
            this.Width = region.Width;
            this.Height = region.Height;
        }

        public static Region Empty { get { return new Region(); } }
        public int X;
        public int Y;
        public int Width;
        public int Height;
    }
}
