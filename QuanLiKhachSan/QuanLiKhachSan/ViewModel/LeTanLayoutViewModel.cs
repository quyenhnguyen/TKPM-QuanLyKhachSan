using QuanLiKhachSan.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using QuanLiKhachSan.ViewModel;
using QuanLiKhachSan.Model;

namespace QuanLiKhachSan.ViewModel
{
    public class LeTanLayoutViewModel : BaseViewModel
    {
        private object _CurrentDataContext;
        public object CurrentDataContext { get => _CurrentDataContext; set { _CurrentDataContext = value; OnPropertyChanged(); } }
        private string _txtTitle;
        public string txtTitle { get => _txtTitle; set { _txtTitle = value; OnPropertyChanged(); } }
        private NHANVIEN _NhanVienDangNhap;
        public NHANVIEN NhanVienDangNhap { get => _NhanVienDangNhap; set { _NhanVienDangNhap = value; OnPropertyChanged(); } }
        #region All Command
        public ICommand btnDatPhong_Command { get; set; }
        public ICommand btnTaiKhoan_Command { get; set; }
        public ICommand btnTraCuu_Command { get; set; }
        public ICommand btnDangXuat_Command { get; set; }

        #endregion
        public LeTanLayoutViewModel()
        {
            int MaNhanVien = UserService.GetCurrentUser.NhanVienID;
            NhanVienDangNhap = new NHANVIEN();
            NhanVienDangNhap = DatabaseQuery.truyVanNhanVien(MaNhanVien);
            object ucDatPhong = new LeTanDatPhongViewModel();
            object ucTraCuu = new LeTanTraCuuViewModel();
            object ucTaiKhoan = new LeTanTaiKhoanViewModel();

            CurrentDataContext = ucDatPhong;
            txtTitle = "TRANG CHỦ THUÊ, TRẢ PHÒNG";
            btnDatPhong_Command = new RelayCommand<object>((p) => { return CurrentDataContext != ucDatPhong; }, (p) =>
                {
                    txtTitle = "TRANG CHỦ THUÊ, TRẢ PHÒNG";
                    ucDatPhong = new LeTanDatPhongViewModel();
                    CurrentDataContext = ucDatPhong;
                });
            btnTraCuu_Command = new RelayCommand<object>((p) => { return CurrentDataContext != ucTraCuu; }, (p) =>
              {
                  txtTitle = "TRA CỨU, QUẢN LÍ";
                  ucTraCuu = new LeTanTraCuuViewModel();
                  CurrentDataContext = ucTraCuu;
              });
            btnTaiKhoan_Command = new RelayCommand<object>((p) => { return CurrentDataContext != ucTaiKhoan; }, (p) =>
              {
                  txtTitle = "QUẢN LÍ TÀI KHOẢN";
                  ucTaiKhoan = new LeTanTaiKhoanViewModel();
                  CurrentDataContext = ucTaiKhoan;
              });
            btnDangXuat_Command = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DangNhap login = new DangNhap();
                login.Show();
                ((Window)p).Close();
            });

        }

    }
}
