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
        private int _MaHoaDon;
        public int MaHoaDon { get => _MaHoaDon; set => OnPropertyChanged(ref _MaHoaDon, value); }

        private BindingList<LSTHEMDICHVU> _DanhSachDichVuDaThem;
        public BindingList<LSTHEMDICHVU> DanhSachDichVuDaThem { get => _DanhSachDichVuDaThem; set { OnPropertyChanged(ref _DanhSachDichVuDaThem, value); } }

        private BindingList<LICHSUTHEMDICHVU> _LichSuThemDichVu;
        public BindingList<LICHSUTHEMDICHVU> LichSuThemDichVu { get => _LichSuThemDichVu; set { OnPropertyChanged(ref _LichSuThemDichVu, value); } }

        private double _TienDichVu;
        public double TienDichVu { get => _TienDichVu; set => OnPropertyChanged(ref _TienDichVu, value); }
        private double _TienPhong;
        public double TienPhong { get => _TienPhong; set => OnPropertyChanged(ref _TienPhong, value); }
        private HOADONTHUEPHONG _HD;
        public HOADONTHUEPHONG HD { get => _HD; set => OnPropertyChanged(ref _HD, value); }
        private bool _isCheckOut;
        public bool isCheckOut { get => _isCheckOut; set => OnPropertyChanged(ref _isCheckOut, value); }
        public LeTanChiTietHoaDonViewModel(int MaHD)
        {
            MaHoaDon = MaHD;
            HD = DatabaseQuery.truyVanHoaDon(MaHoaDon);

            isCheckOut = HD.ThoiGianTra != null;

            DanhSachDichVuDaThem = new BindingList<LSTHEMDICHVU>();
            LichSuThemDichVu = DatabaseQuery.truyVanDanhSachLichSuThemDV(MaHoaDon);
            //Hiện thị ds dv đã thêm
            BusinessModel.ThemDichVuVaoDanhSachDaThem(DanhSachDichVuDaThem, LichSuThemDichVu);

            //tiền thanh toán
            TienDichVu = DatabaseQuery.TinhTienDichVuHoaDon(MaHoaDon);
            if (HD.ThoiGianTra != null) TienPhong = DatabaseQuery.TinhTienThuePhong(HD.ThoiGianThue, (DateTime)HD.ThoiGianTra, HD.PHONG1.DonGia);

            ////khi nào disable btn xem chi tieetss thì mở này ra
            //LSTHEMDICHVU checkout = new LSTHEMDICHVU();
            //checkout.TenDichVu = "TIỀN PHÒNG";
            //checkout.DonVi = "Ngày";
            //checkout.ThoiGianThem = (DateTime)HD.ThoiGianTra;
            //checkout.SoLuong = -HD.ThoiGianThue.Date.Subtract(((DateTime)HD.ThoiGianTra).Date).Days;
            //checkout.ThanhTien = DatabaseQuery.TinhTienThuePhongHoaDon(HD);
            //checkout.DonGia = HD.PHONG1.DonGia;
            //DanhSachDichVuDaThem.Add(checkout);

        }

    }

}
