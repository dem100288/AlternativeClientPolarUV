using Emgu.CV;
using System.Windows.Media;
using System.Runtime.InteropServices;
using System;
using System.Windows.Media.Imaging;
using System.Windows;

namespace AlternativeClientPolarUV
{
    
    public static class MatHelper
    {
        [DllImport("gdi32")]
        private static extern int DeleteObject(IntPtr o);

        public static BitmapSource ToBitmapSource(Mat image)
        {
            using (System.Drawing.Bitmap source = image.ToBitmap())
            {
                IntPtr ptr = source.GetHbitmap(); //obtain the Hbitmap

                BitmapSource bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    ptr,
                    IntPtr.Zero,
                    Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions());

                DeleteObject(ptr); //release the HBitmap
                return bs;
            }
        }

        public static BitmapSource ToImageSource(this Mat mat)
        {
            return ToBitmapSource(mat);
        }
    }
}
