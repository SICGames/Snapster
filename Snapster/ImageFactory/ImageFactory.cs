using com.HellStormGames.Imaging.ScreenCapture.Interlop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace com.HellStormGames.Imaging
{
    public class ImageFactory
    {
        private ImageData ImageData { get; set; }
        public ImageFactory() 
        { 
        }
        public static ImageData Create(int width, int height, int channels)
        {
            ImageData imagedata = new ImageData();

            SnaptureInvoke.CreateImageData(out imagedata, width, height, channels, null);
            
            return imagedata;
        }
        public static ImageData Create(int width, int height, int channels, byte[] data)
        {
            ImageData imagedata = new ImageData();

            SnaptureInvoke.CreateImageData(out imagedata, width, height, channels, data);

            return imagedata;
        }
        public static ImageData FromHBitmap(IntPtr hBitmap)
        {
            ImageData image = new ImageData();
            SnaptureInvoke.CreateImageDataFromHBITMAP(out image, hBitmap);
            return image;
        }
    }
}
