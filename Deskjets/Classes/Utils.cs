using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Linq;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace Deskjets.Classes
{
    static class Utils
    {
        public static bool IsWindowOpen<T>(string name = "") where T : Window
        {
            return string.IsNullOrEmpty(name)
               ? Application.Current.Windows.OfType<T>().Any()
               : Application.Current.Windows.OfType<T>().Any(w => w.Name.Equals(name));
        }

        public static void OpenWindow<T>(bool focusIfOpen = false) where T : Window
        {
            //daca este deja deschisa o fereastra ii da focus
            if (IsWindowOpen<T>() && focusIfOpen)
                Application.Current.Windows.OfType<T>().ToArray()[0].Focus();
            else
                Activator.CreateInstance<T>().Show();
        }

        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);
        public static BitmapSource BitmapToBitmapSource(Bitmap bitmap)
        {
            return Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(),
                IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }

        public static Bitmap BitmapImageToBitmap(BitmapImage bitmapImage)
        {
            // BitmapImage bitmapImage = new BitmapImage(new Uri("../Images/test.png", UriKind.Relative));

            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                Bitmap bitmap = new Bitmap(outStream);

                return new Bitmap(bitmap);
            }
        }

        public static System.Windows.Media.Brush StringToBrush(string colorString)
        {
            return (System.Windows.Media.SolidColorBrush)new System.Windows.Media.BrushConverter().ConvertFrom(colorString);
        }

        public static string GetAverageColor(Bitmap bmp)
        {
            int r = 0;
            int g = 0;
            int b = 0;
            int total = 0;

            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    Color clr = bmp.GetPixel(x, y);

                    r += clr.R;
                    g += clr.G;
                    b += clr.B;
                    total++;
                }
            }

            r /= total;
            g /= total;
            b /= total;

            Color averageColor = Color.FromArgb(r, g, b);
            return "#" + averageColor.R.ToString("X2") + averageColor.G.ToString("X2") + averageColor.B.ToString("X2");
        }
    }
}
