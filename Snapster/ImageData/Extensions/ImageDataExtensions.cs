using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace com.HellStormGames.Imaging.Extensions
{
    public static class ImageDataExtensions
    {
        public static Bitmap ToBitmap(this ImageData imagedata)
        {
            PixelFormat format = PixelFormat.Format32bppRgb;
            var bitsperpixel = imagedata.BitsPerPixel;
            if(bitsperpixel == 24)
            {
                format = PixelFormat.Format24bppRgb;
            }
            else if(bitsperpixel == 8)
            {
                format = PixelFormat.Format8bppIndexed;
            }
            else if(bitsperpixel == 32)
            {
                format = PixelFormat.Format32bppRgb;
            }

            Bitmap bitmap = new Bitmap(imagedata.Width, imagedata.Height, format);
            var bmpinfo = bitmap.LockBits(new Rectangle(0, 0, imagedata.Width, imagedata.Height), ImageLockMode.WriteOnly, format);
            IntPtr scan0Ptr = bmpinfo.Scan0;
            bmpinfo.Stride = imagedata.Stride;

            var length = Math.Abs(bmpinfo.Stride) * imagedata.Height;
            byte[] bytes = new byte[length];

            //-- put pixel data into bytes then back into scan0.
            Marshal.Copy(imagedata.Data, bytes, 0, bytes.Length);
            Marshal.Copy(bytes, 0, scan0Ptr, length);

            //-- unlock bitmap info data
            bitmap.UnlockBits(bmpinfo);

            //-- clean up on isle 5.
            bytes = null;
            return bitmap;
        }

        public static ImageData ToImageData(this Bitmap bitmap)
        {
            var channels = 4;
            var format = bitmap.PixelFormat;
            if (format == PixelFormat.Format8bppIndexed)
            {
                channels = 1;
            }
            else if (format == PixelFormat.Format32bppRgb)
            {
                channels = 4;
            }
               else if(format == PixelFormat.Format24bppRgb)
            {
                channels = 3;
            }
            else if(format==PixelFormat.Format32bppArgb)
            {
                channels = 4;
            }
            var bitmapinfo = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);

            var length = Math.Abs(bitmapinfo.Stride) * bitmapinfo.Height;
            byte[] bytes = new byte[length];
            Marshal.Copy(bitmapinfo.Scan0, bytes, 0, bytes.Length);

            ImageData imageData = ImageFactory.Create(bitmapinfo.Width, bitmapinfo.Height, channels, bytes);

            bitmap.UnlockBits(bitmapinfo);
            return imageData;

            imageData.Width = bitmap.Width;
            imageData.Height = bitmap.Height;
            
            imageData.Stride = bitmapinfo.Stride;
            imageData.Size = bitmapinfo.Stride * bitmapinfo.Height;
            imageData.RowSize = bitmapinfo.Stride / channels;
            imageData.Channels = channels;
            imageData.BitsPerPixel = channels * 8;
            imageData.Data = bitmapinfo.Scan0;

            bitmap.UnlockBits(bitmapinfo);

            return imageData;
        }
    }
}
