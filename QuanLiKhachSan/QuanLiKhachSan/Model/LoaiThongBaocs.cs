using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiKhachSan.Model
{
    public abstract class LoaiThongBao
    {
        protected string tieuDe;
        protected string noiDung;
        protected abstract void setTieuDe(string td);
        protected abstract void setNoiDung(string nd);
        protected string getThongBao()
        {
            string msg = $"<p><b>[TB] {this.tieuDe}</b></p>" +
                $"<p>Nội Dung:</p><br>" +
                $"{this.noiDung}";
            return msg;
        }
        public string getNoiDung()
        {
            return noiDung;
        }
        public string getTieuDe()
        {
            return tieuDe;
        }


    }
    public class HoaDonThongBao : LoaiThongBao
    {
    
        public HoaDonThongBao(string td, string nd)
        {
            setTieuDe(td);
            setNoiDung(nd);
        }
        protected override void setTieuDe(string nd)
        {
            this.tieuDe = "Hóa đơn : " + nd;
        }

        protected override void setNoiDung(string td)
        {
            this.noiDung = "Chi Tiết Hóa Đơn : <br>";
        }
    }
    public class LoaiThongBaoFactory
    {
        public  LoaiThongBaoFactory() { }
        public LoaiThongBao createLoai(int loaiThongBao, string td, string nd)
        {
            if (loaiThongBao==1)
            {
                return new HoaDonThongBao(td,nd);
            }
            return new HoaDonThongBao(td, nd);
        }
    }
}
