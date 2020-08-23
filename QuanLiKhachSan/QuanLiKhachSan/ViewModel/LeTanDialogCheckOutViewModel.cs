using QuanLiKhachSan.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLiKhachSan.ViewModel
{
    public class LeTanDialogCheckOutViewModel : BaseViewModel
    {
        public int MaHoaDon { get; set; }

        public HOADONTHUEPHONG _HoaDon;
        public HOADONTHUEPHONG HoaDon { get => _HoaDon; set => OnPropertyChanged(ref _HoaDon, value); }
        public KHACHHANG _KH;
        public KHACHHANG KH { get => _KH; set => OnPropertyChanged(ref _KH, value); }

        private DateTime _ngayCheckOut;
        public DateTime NgayCheckOut { get => _ngayCheckOut; set => OnPropertyChanged(ref _ngayCheckOut, value); }
        private double _tienPhong;
        public double TienPhong { get => _tienPhong; set => OnPropertyChanged(ref _tienPhong, value); }
        private double _tienTraLai;
        public double TienTraLai { get => _tienTraLai; set => OnPropertyChanged(ref _tienTraLai, value); }

        public ICommand ThoatBtnComamand { get; set; }
        public ICommand DongYBtnCommand { get; set; }
        public ICommand TienKhachDuaEnterCommand { get; set; }

        public LeTanDialogCheckOutViewModel(int maHD)
        {
            MaHoaDon = maHD;
            HoaDon = DatabaseQuery.truyVanHoaDonDangThueMaHD(MaHoaDon);
            KH = DatabaseQuery.truyVanKhachHangMaHD(MaHoaDon);
            PHONG phong = DatabaseQuery.truyVanTenPhongMaHoaDon(MaHoaDon);

            NgayCheckOut = DateTime.Now;
            TienPhong = DatabaseQuery.TinhTienThuePhong(HoaDon.ThoiGianThue, NgayCheckOut, phong.DonGia);

            ThoatBtnComamand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

            DongYBtnCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.DialogResult = true;
                //lưu xún csdl cả hóa đơn với đầy đủ các trường
            });
            TienKhachDuaEnterCommand = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                TienTraLai = Convert.ToDouble(p.Text.ToString()) - TienPhong;
            });

        }
    }
}
