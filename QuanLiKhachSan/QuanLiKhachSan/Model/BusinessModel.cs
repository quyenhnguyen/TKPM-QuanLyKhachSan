using QuanLiKhachSan.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiKhachSan.Model
{
    public class THEDANGKI : BaseViewModel
    {
        private int _id;
        public int ID
        {
            get => _id; set
            {
                OnPropertyChanged(ref _id, value);
            }
        }
        private string _name;
        public string Name
        {
            get => _name; set
            {
                OnPropertyChanged(ref _name, value);
            }
        }
    }
    public class LSTHEMDICHVU : BaseViewModel
    {
        private int _sl;
        public int SoLuong { get => _sl; set { OnPropertyChanged(ref _sl, value); } }
        private DateTime _ThoiGianThem;
        public DateTime ThoiGianThem { get => _ThoiGianThem; set { OnPropertyChanged(ref _ThoiGianThem, value); } }

        private double _DonGia;
        public double DonGia { get => _DonGia; set { OnPropertyChanged(ref _DonGia, value); } }
        private double _ThanhTien;
        public double ThanhTien { get => _ThanhTien; set { OnPropertyChanged(ref _ThanhTien, value); } }
        public string DichVuID { get; set; }
        public string _TenDichVu;
        public string TenDichVu { get => _TenDichVu; set { OnPropertyChanged(ref _TenDichVu, value); } }
        public string _DonVi;
        public string DonVi { get => _DonVi; set { OnPropertyChanged(ref _DonVi, value); } }
    }

    public class ThongTinBaoCao : BaseViewModel
    {
        public string _TenDonVi;
        public string TenDonVi { get => _TenDonVi; set { OnPropertyChanged(ref _TenDonVi, value); } }

        public double _DoanhThu;
        public double DoanhThu { get => _DoanhThu; set { OnPropertyChanged(ref _DoanhThu, value); } }

        public double _LoiNhuan;
        public double LoiNhuan { get => _LoiNhuan; set { OnPropertyChanged(ref _LoiNhuan, value); } }

        public double _ChiPhi;
        public double ChiPhi { get => _ChiPhi; set { OnPropertyChanged(ref _ChiPhi, value); } }
    }

    public class BusinessModel
    {
        public static void ThemDichVuVaoDanhSachDaThem(BindingList<LSTHEMDICHVU> DanhSachDichVuDaThem, BindingList<LICHSUTHEMDICHVU> list)
        {
            foreach (LICHSUTHEMDICHVU ls in list)
            {
                LSTHEMDICHVU dvDaThem = new LSTHEMDICHVU();
                DICHVU dv = DatabaseQuery.truyVanDichVuCuaHoaDon(ls.DichVuID);
                dvDaThem.SoLuong = ls.SoLuong;
                dvDaThem.TenDichVu = dv.TenDichVu;
                dvDaThem.DonGia = dv.GiaBan;
                dvDaThem.ThoiGianThem = ls.ThoiGianThem;
                dvDaThem.ThanhTien = dv.GiaBan * ls.SoLuong;
                dvDaThem.DonVi = dv.DonVi;
                DanhSachDichVuDaThem.Add(dvDaThem);
            }
        }
    }


}
