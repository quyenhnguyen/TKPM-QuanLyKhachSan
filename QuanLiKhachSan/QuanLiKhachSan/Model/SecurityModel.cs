using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiKhachSan.Model
{
    public class SecurityModel
    {
        private MD5 mh;
        public SecurityModel()
        {

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
