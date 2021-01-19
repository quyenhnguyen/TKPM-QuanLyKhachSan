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
        private List<THONGTINDANGKI> DsSubcriber = null;
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
                if (DsSubcriber[i].NhanVienID == NhanVienID || DieuKienDangKy(LoaiThongBao, NhanVienID) == false)
                {
                    return false;
                }
            }
            try
            {
                THONGTINDANGKI tem = new THONGTINDANGKI();
                tem.NhanVienID = NhanVienID;
                tem.HastagID = LoaiThongBao;
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
        public bool Unsubcribe(int NhanVienID, int LoaiThongBao)
        {
            for (int i = 0; i < DsSubcriber.Count; i++)
            {
                if (DsSubcriber[i].NhanVienID == NhanVienID && DsSubcriber[i].HastagID == LoaiThongBao)
                {
                    try
                    {
                        DatabaseQueryTN.xoaNguoiDangKi(DsSubcriber[i]);
                        DsSubcriber.RemoveAt(i);
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public string GuiThongBao(int LoaiThongBao, string td, string msg)
        {
            string tenLoaiTB = DatabaseQueryTN.tenLoaiThongBao(LoaiThongBao);
            if (tenLoaiTB == "")
            {
                return "Không có loại thông báo!";
            }
            List<string> listSubcriberThisNoti = new List<string>();
            foreach (THONGTINDANGKI item in DsSubcriber)
            {
                if (item.HastagID == LoaiThongBao)
                {
                    listSubcriberThisNoti.Add(item.NHANVIEN.Email);
                }
            }
            LoaiThongBaoFactory ft = new LoaiThongBaoFactory();
            LoaiThongBao a = ft.createLoai(LoaiThongBao, td, msg);
            string noidung = prepareTemplate(a.getTieuDe(), a.getNoiDung());
            return Email(a.getTieuDe(), noidung, listSubcriberThisNoti);
        }
        private bool DieuKienDangKy(int loai, int nvid)
        {
            NHANVIEN nv = DatabaseQueryTN.getNhanVienbyId(nvid);
            if (nv == null) return false;
            if (loai == 2 && nv.Loai == 3 )
            {
                return false;
            }
            return true;
        }
        private string Email(string tieuDe, string htmlString, List<string> listSubcriberThisNoti)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("githubreporter@zohomail.com", "Q&Q Hotel");
                foreach (string item in listSubcriberThisNoti)
                {
                    message.To.Add(new MailAddress(item));
                }
                message.Subject = tieuDe;
                message.IsBodyHtml = true;
                message.Body = htmlString;
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Host = "smtp.zoho.com";
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
        public List<THONGTINDANGKI> danhSachNhanThongBao()
        {
            return DsSubcriber;
        }
        private string prepareTemplate(string tieude, string noidung)
        {
            try
            {
                string messageBody = @"
                    <head>
    <meta http-equiv='Content-Type' content='text/html; charset=utf-8'>
    <meta name='viewport' content='width=device-width'>
    <meta http-equiv='X-UA-Compatible' content='IE=edge'>
    <title></title>
    <link href='https://fonts.googleapis.com/css?family=Cabin' rel='stylesheet' type='text/css'>
    <!--<![endif]-->
    <style type='text/css'>
        .quang-block {
            padding-left: 30px;
            padding-right: 30px;
            font-size: 12px;
            display: inline-block;
        }
        .big-block {
            border-top: 0px solid transparent;
            border-left: 0px solid transparent;
            border-bottom: 0px solid transparent;
            border-right: 0px solid transparent;
            padding-top: 0px;
            padding-bottom: 0px;
            padding-right: 0px;
            padding-left: 0px;
        }
        
        .sumary {
            color: #000000;
            font-family: 'Lato', Tahoma, Verdana, Segoe, sans-serif;
            line-height: 1.2;
            padding-top: 5px;
            padding-right: 5px;
            padding-left: 30px;
        }
        
        .sumary-block {
            padding-top: 10px;
            padding-right: 5px;
            padding-bottom: 10px;
            padding-left: 50px;
        }
        
        .quang {
            display: block !important;  text-align: revert;
            margin: 0px, 10px;
            text-decoration: none;
            display: inline-block;
            color: #000000;
            border-radius: 0px;
            -webkit-border-radius: 0px;
            -moz-border-radius: 0px;
            width: auto;
            width: auto;
            border-top: 1px solid #f6d16c;
            border-right: 1px solid #f6d16c;
            border-bottom: 1px solid #f6d16c;
            border-left: 1px solid #f6d16c;
            padding-top: 1px;
            padding-bottom: 2px;
            font-family: Bitter, Georgia, Times, Times New Roman, serif;
            text-align: center;
            word-break: keep-all;
        }
        
        body {
            margin: 0;
            padding: 0;
        }
        
        table,
        td,
        tr {
            vertical-align: top;
            border-collapse: collapse;
        }
        
        * {
            line-height: inherit;
        }
        
        a[x-apple-data-detectors=true] {
            color: inherit !important;
            text-decoration: none !important;
        }
    </style>
    <style type='text/css' id='media-query'>
        @media (max-width: 795px) {
            .block-grid,
            .col {
                min-width: 320px !important;
                max-width: 100% !important;
                display: block !important;
            }
            .block-grid {
                width: 100% !important;
            }
            .col {
                width: 100% !important;
            }
            .col>div {
                margin: 0 auto;
            }
            img.fullwidth,
            img.fullwidthOnMobile {
                max-width: 100% !important;
            }
            .no-stack .col {
                min-width: 0 !important;
                display: table-cell !important;
            }
            .no-stack.two-up .col {
                width: 50% !important;
            }
            .no-stack .col.num4 {
                width: 33% !important;
            }
            .no-stack .col.num8 {
                width: 66% !important;
            }
            .no-stack .col.num4 {
                width: 33% !important;
            }
            .no-stack .col.num3 {
                width: 25% !important;
            }
            .no-stack .col.num6 {
                width: 50% !important;
            }
            .no-stack .col.num9 {
                width: 75% !important;
            }
            .video-block {
                max-width: none !important;
            }
            .mobile_hide {
                min-height: 0px;
                max-height: 0px;
                max-width: 0px;
                display: none;
                overflow: hidden;
                font-size: 0px;
            }
            .desktop_hide {
                display: block !important;
                max-height: none !important;
            }
        }
    </style>
    <style type='text/css' id='icon-media-query'>
        @media (max-width: 795px) {
            .icons-inner {
                text-align: center;
            }
            .icons-inner td {
                margin: 0 auto;
            }
        }
    </style>
</head>

<body class='clean-body' style='margin: 0; padding: 0; -webkit-text-size-adjust: 100%; background-color: #d6d6d6;'>
    <table class='nl-container' style='table-layout: fixed; vertical-align: top; min-width: 320px; Margin: 0 auto; border-spacing: 0; border-collapse: collapse; mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #d6d6d6; width: 100%;' cellpadding='0'
        cellspacing='0' role='presentation' width='100%' bgcolor='#d6d6d6' valign='top'>
        <tbody>
            <tr style='vertical-align: top;' valign='top'>
                <td style='word-break: break-word; vertical-align: top;' valign='top'>
                    <div style='background-image:url('root/img/memphis_black_right.png');background-position:top center;background-repeat:no-repeat;background-color:#f6d16c;'>
                        <div class='block-grid ' style='Margin: 0 auto; min-width: 320px; max-width: 775px; overflow-wrap: break-word; word-wrap: break-word; word-break: break-word; background-color: transparent;'>
                            <div style='border-collapse: collapse;display: table;width: 100%;background-color:transparent;'>
                                <div class='col num12' style='min-width: 320px; max-width: 775px; display: table-cell; vertical-align: top; width: 775px;'>
                                    <div style='width:100% !important;'>

                                        <div class='big-block'>
                                            <!--<![endif]-->
                                            <table class='divider' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed; vertical-align: top; border-spacing: 0; border-collapse: collapse; mso-table-lspace: 0pt; mso-table-rspace: 0pt; min-width: 100%; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%;'
                                                role='presentation' valign='top'>
                                                <tbody>
                                                    <tr style='vertical-align: top;' valign='top'>
                                                        <td class='divider_inner' style='word-break: break-word; vertical-align: top; min-width: 100%; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; padding-top: 10px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px;' valign='top'>
                                                            <table class='divider_content' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed; vertical-align: top; border-spacing: 0; border-collapse: collapse; mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-top: 1px solid #000000; width: 100%;'
                                                                align='center' role='presentation' valign='top'>
                                                                <tbody>
                                                                    <tr style='vertical-align: top;' valign='top'>
                                                                        <td style='word-break: break-word; vertical-align: top; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%;' valign='top'><span></span></td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <div style='color:#ffffff;font-family:Bitter, Georgia, Times, Times New Roman, serif;line-height:1.5;padding-top:15px;padding-right:40px;padding-bottom:0px;padding-left:40px;'>
                                                <div style='line-height: 1.5; font-size: 12px; color: #ffffff; font-family: Bitter, Georgia, Times, Times New Roman, serif; mso-line-height-alt: 18px;'>
                                                    <p style='font-size: 30px; line-height: 1.5; word-break: break-word; text-align: center; mso-line-height-alt: 45px; margin: 0;'><span style='font-size: 30px; color: #000000;'><strong>
"
+ tieude+@"</strong></span></p>
                                                </div>
                                            </div>
                                            <div style='color:#000000;font-family:'Lato', Tahoma, Verdana, Segoe, sans-serif;line-height:1.8;padding-top:8px;padding-right:3px;padding-bottom:3px;padding-left:3px;'>
                                                <div style='font-size: 14px; line-height: 1.8; font-family: 'Lato', Tahoma, Verdana, Segoe, sans-serif; color: #000000; mso-line-height-alt: 25px;'>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div style='background-color:transparent;'>
                        <div class='block-grid mixed-two-up no-stack' style='Margin: 0 auto; min-width: 320px; max-width: 775px; overflow-wrap: break-word; word-wrap: break-word; word-break: break-word; background-color: #ffffff;'>
                            <div style='border-collapse: collapse;display: table;width: 100%;background-color:#ffffff;'>
                                <div class='col num4' style='display: table-cell; vertical-align: top; max-width: 320px; min-width: 256px; width: 258px;'>
                                    <div style='width:100% !important;'>

                                        <div style='border-top:0px dotted transparent; border-left:0px dotted transparent; border-bottom:0px dotted transparent; border-right:0px dotted transparent; padding-top:0px; padding-bottom:0px; padding-right: 0px; padding-left: 0px;'>
                                            <!--<![endif]-->
                                            <table class='divider' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed; vertical-align: top; border-spacing: 0; border-collapse: collapse; mso-table-lspace: 0pt; mso-table-rspace: 0pt; min-width: 100%; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%;'
                                                role='presentation' valign='top'>
                                                <tbody>
                                                    <tr style='vertical-align: top;' valign='top'>
                                                        <td class='divider_inner' style='word-break: break-word; vertical-align: top; min-width: 100%; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; padding-top: 10px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px;' valign='top'>
                                                            <table class='divider_content' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed; vertical-align: top; border-spacing: 0; border-collapse: collapse; mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-top: 1px solid #BBBBBB; width: 100%;'
                                                                align='center' role='presentation' valign='top'>
                                                                <tbody>
                                                                    <tr style='vertical-align: top;' valign='top'>
                                                                        <td style='word-break: break-word; vertical-align: top; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%;' valign='top'><span></span></td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                                <div class='col num8' style='display: table-cell; vertical-align: top; min-width: 320px; max-width: 512px; width: 516px;'>
                                    <div style='width:100% !important;'>

                                        <div style='border-top:0px solid transparent; border-left:0px solid transparent; border-bottom:0px solid transparent; border-right:0px solid transparent; padding-top:5px; padding-bottom:5px; padding-right: 0px; padding-left: 0px;'>
                                            <!--<![endif]-->
                                            <table class='divider' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed; vertical-align: top; border-spacing: 0; border-collapse: collapse; mso-table-lspace: 0pt; mso-table-rspace: 0pt; min-width: 100%; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%;'
                                                role='presentation' valign='top'>
                                                <tbody>
                                                    <tr style='vertical-align: top;' valign='top'>
                                                        <td class='divider_inner' style='word-break: break-word; vertical-align: top; min-width: 100%; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; padding-top: 5px; padding-right: 5px; padding-bottom: 5px; padding-left: 5px;' valign='top'>
                                                            <table class='divider_content' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed; vertical-align: top; border-spacing: 0; border-collapse: collapse; mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-top: 1px solid #BBBBBB; width: 100%;'
                                                                align='center' role='presentation' valign='top'>
                                                                <tbody>
                                                                    <tr style='vertical-align: top;' valign='top'>
                                                                        <td style='word-break: break-word; vertical-align: top; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%;' valign='top'><span></span></td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div style='background-color:transparent;'>
                        <div class='block-grid mixed-two-up' style='Margin: 0 auto; min-width: 320px; max-width: 775px; overflow-wrap: break-word; word-wrap: break-word; word-break: break-word; background-color: #f8f8f8;'>
                            <div style='border-collapse: collapse;display: table;width: 100%;background-color:#f8f8f8;'>
                                <div class='col num8' style='display: table-cell; vertical-align: top; min-width: 320px; max-width: 512px; width: 516px;'>
                                    <div style='width:100% !important;'>
                                        <div class='big-block'>
                                            <div class='sumary'>
                                                <div style='line-height: 1.2; font-size: 12px; font-family: 'Lato', Tahoma, Verdana, Segoe, sans-serif; color: #000000; mso-line-height-alt: 14px;'>
                                                    <p style='line-height: 1.2; font-family: Lato, Tahoma, Verdana, Segoe, sans-serif; word-break: break-word; font-size: 18px; mso-line-height-alt: 22px; margin: 0;'><span style=' font-size: 18px;'>Chi tiết</span></p>
                                                </div>
                                            </div>
                                            <div class='button-container sumary-block'>"
+ noidung+ @"
                                                <br>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div style='background-color:transparent;'>
                        <div class='block-grid ' style='Margin: 0 auto; min-width: 320px; max-width: 775px; overflow-wrap: break-word; word-wrap: break-word; word-break: break-word; background-color: #ffffff;'>
                            <div style='border-collapse: collapse;display: table;width: 100%;background-color:#ffffff;'>
                                <div class='col num12' style='min-width: 320px; max-width: 775px; display: table-cell; vertical-align: top; width: 775px;'>
                                    <div style='width:100% !important;'>

                                        <div class='big-block'>
                                            <!--<![endif]-->
                                            <table class='divider' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed; vertical-align: top; border-spacing: 0; border-collapse: collapse; mso-table-lspace: 0pt; mso-table-rspace: 0pt; min-width: 100%; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%;'
                                                role='presentation' valign='top'>
                                                <tbody>
                                                    <tr style='vertical-align: top;' valign='top'>
                                                        <td class='divider_inner' style='word-break: break-word; vertical-align: top; min-width: 100%; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; padding-top: 10px; padding-right: 15px; padding-bottom: 10px; padding-left: 15px;' valign='top'>
                                                            <table class='divider_content' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed; vertical-align: top; border-spacing: 0; border-collapse: collapse; mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-top: 1px solid #E9EBEB; width: 100%;'
                                                                align='center' role='presentation' valign='top'>
                                                                <tbody>
                                                                    <tr style='vertical-align: top;' valign='top'>
                                                                        <td style='word-break: break-word; vertical-align: top; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%;' valign='top'><span></span></td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
        </tbody>
    </table>
</body>

</html>
";
                return messageBody;
            }
            catch (Exception ex)
            {
                return "Error";
            }
        }
    }
}
