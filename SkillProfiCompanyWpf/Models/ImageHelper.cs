using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SkillProfiCompanyWpf.Models
{
    public class ImageHelper
    {
        public static BitmapImage ConvertFromBase64ToBitmapImage(string base64Image)
        {
            if (base64Image != null)
            {
                string[] str = base64Image.Split(',');

                if (str.Length != 1)
                {
                    base64Image = str[1];
                }

                byte[] imageData = Convert.FromBase64String(base64Image);

                if (imageData == null || imageData.Length == 0)
                {
                    return null;
                }
                var image = new BitmapImage();
                using (var mem = new MemoryStream(imageData))
                {
                    mem.Position = 0;
                    image.BeginInit();
                    image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.UriSource = null;
                    image.StreamSource = mem;
                    image.EndInit();
                }
                image.Freeze();
                return image;
            }
            return null;
        }
    }
}
