using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Linq;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.IO;

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

        public static Bitmap BitmapImageToBitmap(BitmapImage bitmapImage)
        {
            // BitmapImage bitmapImage = new BitmapImage(new Uri("../Images/test.png", UriKind.Relative));

            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(outStream);

                return new Bitmap(bitmap);
            }
        }
    }
}
