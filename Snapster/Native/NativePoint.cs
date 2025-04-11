using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace com.HellStormGames.Interlop.Native
{
    [StructLayout(LayoutKind.Sequential)]
    public struct POINTL
    {
        int x;
        int y;
    }
}
