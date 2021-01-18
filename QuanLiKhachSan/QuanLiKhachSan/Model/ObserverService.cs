using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiKhachSan.Model
{
    public class ObserverService
    {
        private static ObserverService instance = null;
        private List<DSDANGKYTB> DsSubcriber = null;
        public ObserverService()
        {
            DsSubcriber = DatabaseQueryTN.danhSachDangKyThongBao();

        }
        public static ObserverService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ObserverService();
                }
                return instance;
            }
        }
        public bool Subcribe(int NhanVienID, int LoaiThongBao)
        {
            for (int i = 0; i < DsSubcriber.Count; i++)
            {
                if (DsSubcriber[i].NhanVienID == NhanVienID)
                {
                    return false;
                }
            }
            try
            {
                DSDANGKYTB tem = new DSDANGKYTB();
                tem.NhanVienID = NhanVienID;
                tem.LoaiTB = LoaiThongBao;
                DatabaseQueryTN.themNguoiDangKi(tem);
                DsSubcriber.Add(tem);
            }
            catch (Exception exep)
            {
                Console.WriteLine(exep.ToString());
                return false;
            }
            return true;
        }
        public string GuiThongBao(int LoaiThongBao, string msg)
        {
            string tenLoaiTB = DatabaseQueryTN.tenLoaiThongBao(LoaiThongBao);
            if (tenLoaiTB == "")
            {
                return "Không có loại thông báo!!!";
            }
            List<string> listSubcriberThisNoti = new List<string>();
            foreach (DSDANGKYTB item in DsSubcriber)
            {
                if (item.LoaiTB == LoaiThongBao)
                {
                    listSubcriberThisNoti.Add(item.Email);
                }
            }
            string html = prepareTemplate(tenLoaiTB, msg);
            return Email(html, listSubcriberThisNoti);
        }
        private string Email(string htmlString, List<string> listSubcriberThisNoti)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("githubreporter@zohomail.com");
                foreach (string item in listSubcriberThisNoti)
                {
                    message.To.Add(new MailAddress(item));
                }
                message.Subject = "Test";
                message.IsBodyHtml = true;
                message.Body = htmlString;
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Host = "smtp.zoho.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("tnquang.769@gmail.com", "quang7699");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                return "success".ToString();
            }
            catch (Exception exf)
            {
                Console.WriteLine(exf.ToString());
                return exf.ToString();
            }
        }
        public List<DSDANGKYTB> danhSachNhanThongBao()
        {
            return DsSubcriber;
        }
        private string prepareTemplate(string tieude, string noidung)
        {
            try
            {
                string messageBody = $"<h2>Chủ đề: {tieude}</h2>" +
                    $"<h2>Nội dung:</h2><br>" +
                    $"<p>{noidung}</p>";
                return messageBody;
            }
            catch (Exception ex)
            {
                return "Error";
            }
        }
    }
}
