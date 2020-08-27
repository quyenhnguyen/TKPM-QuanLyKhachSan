using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace QuanLiKhachSan.Model
{
    public class SecurityModel
    {
        public SecurityModel()
        {

        }
        public static void Log(string log)
        {
            try
            {
                FileStream stream = new FileStream("log.txt", FileMode.Append);
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    string time = DateTime.Now.ToString("dd/mm/yyyy HH:mm:ss");
                    writer.WriteLine("{0} : {1}", time, log);
                }
                stream.Close();
            }
            catch (Exception)
            {
            }
        }
        public static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
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
        public static byte[] ImageToByte2(BitmapImage img)
        {
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(img));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                return ms.ToArray();
            }
        }

        public static string Encrypt(string toEncrypt)
        {
            try
            {

                MD5 mh = MD5.Create();
                //Chuyển kiểu chuổi thành kiểu byte
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(toEncrypt);
                //mã hóa chuỗi đã chuyển
                byte[] hash = mh.ComputeHash(inputBytes);
                //tạo đối tượng StringBuilder (làm việc với kiểu dữ liệu lớn)
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
