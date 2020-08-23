using QuanLiKhachSan.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace QuanLiKhachSan.ViewModel
{
    public class LeTanChiTietHoaDonViewModel : BaseViewModel
    {
        public int MaHoaDon { get; set; }
        private BindingList<LSTHEMDICHVU> _DanhSachDichVuDaThem;
        public BindingList<LSTHEMDICHVU> DanhSachDichVuDaThem { get => _DanhSachDichVuDaThem; set { OnPropertyChanged(ref _DanhSachDichVuDaThem, value); } }

        private BindingList<LICHSUTHEMDICHVU> _LichSuThemDichVu;
        public BindingList<LICHSUTHEMDICHVU> LichSuThemDichVu { get => _LichSuThemDichVu; set { OnPropertyChanged(ref _LichSuThemDichVu, value); } }

        private double _TongThanhToan;
        public double TongThanhToan { get => _TongThanhToan; set => OnPropertyChanged(ref _TongThanhToan, value); }

        public LeTanChiTietHoaDonViewModel(int MaHD)
        {
            MaHoaDon = MaHD;
            HOADONTHUEPHONG HD = DatabaseQuery.truyVanHoaDon(MaHoaDon);

            DanhSachDichVuDaThem = new BindingList<LSTHEMDICHVU>();
            LichSuThemDichVu = DatabaseQuery.truyVanDanhSachLichSuThemDV(MaHoaDon);
            //Hiện thị ds dv đã thêm
            BusinessModel.ThemDichVuVaoDanhSachDaThem(DanhSachDichVuDaThem, LichSuThemDichVu);

            ////khi nào disable btn xem chi tieetss thì mở này ra
            //LSTHEMDICHVU checkout = new LSTHEMDICHVU();
            //checkout.TenDichVu = "TIỀN PHÒNG";
            //checkout.DonVi = "Ngày";
            //checkout.ThoiGianThem = (DateTime)HD.ThoiGianTra;
            //checkout.SoLuong = -HD.ThoiGianThue.Date.Subtract(((DateTime)HD.ThoiGianTra).Date).Days;
            //checkout.ThanhTien = DatabaseQuery.TinhTienThuePhongHoaDon(HD);
            //checkout.DonGia = HD.PHONG1.DonGia;
            //DanhSachDichVuDaThem.Add(checkout);
            //TongThanhToan = checkout.ThanhTien + DatabaseQuery.TinhTienDichVuHoaDon(MaHoaDon);
        }

    }

}
