using QuanLiKhachSan.Model;
using QuanLiKhachSan.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLiKhachSan.ViewModel
{
    public class QuanLy_LayoutViewModel : BaseViewModel
    {

        private object _CurrentDataContext;
        public object CurrentQuanLyDataContext { get => _CurrentDataContext; set { _CurrentDataContext = value; OnPropertyChanged(); } }

        private string _QuanLyTitle;
        public string QuanLyTitle
        {
            get => _QuanLyTitle;
            set { _QuanLyTitle = value; OnPropertyChanged(); }
        }
        private NHANVIEN _NhanVienDangNhap;
        public NHANVIEN NhanVienDangNhap { get => _NhanVienDangNhap; set { _NhanVienDangNhap = value; OnPropertyChanged(); } }
        #region All Command
        public ICommand btnQuanLy_NhanVien { get; set; }
        public ICommand btnQuanLy_Phong { get; set; }
        public ICommand btnQuanLy_DichVu { get; set; }
        public ICommand btnQuanLy_TaiKhoan { get; set; }
        public ICommand btnQuanLy_ThamSo { get; set; }
        public ICommand btnQuanLy_DangXuat { get; set; }


        #endregion

        public QuanLy_LayoutViewModel()
        {
            int MaNhanVien = UserService.GetCurrentUser.NhanVienID;
            NhanVienDangNhap = DatabaseQuery.truyVanNhanVien(MaNhanVien);
            object ucNhanVien = new QuanLyNhanVienViewModel();
            object ucPhong = new QuanLyPhong_LoaiPhongViewModel();
            object ucDichVu = new QuanLyDichVu_LoaiDVViewModel();
            object ucTaiKhoan = new QuanLyTaiKhoanViewModel();
            object ucThamSo = new QuanLy_ThamSoViewModel();
            CurrentQuanLyDataContext = ucNhanVien;
            QuanLyTitle = "QUẢN LÝ NHÂN VIÊN";
            btnQuanLy_NhanVien = new RelayCommand<object>((p) => { return CurrentQuanLyDataContext != ucNhanVien; }, (p) =>
            {
                QuanLyTitle = "QUẢN LÝ NHÂN VIÊN";
                ucPhong = new QuanLyNhanVienViewModel();
                CurrentQuanLyDataContext = ucNhanVien;
            });

            btnQuanLy_Phong = new RelayCommand<object>((p) => { return CurrentQuanLyDataContext != ucPhong; }, (p) =>
            {
                QuanLyTitle = "QUẢN LÝ PHÒNG, LOẠI PHÒNG";

                ucPhong = new QuanLyPhong_LoaiPhongViewModel();
                CurrentQuanLyDataContext = ucPhong;
            });

            btnQuanLy_DichVu = new RelayCommand<object>((p) => { return CurrentQuanLyDataContext != ucDichVu; }, (p) =>
            {
                QuanLyTitle = "QUẢN LÝ DỊCH VỤ, LOẠI DỊCH VỤ";
                CurrentQuanLyDataContext = ucDichVu;
            });

            btnQuanLy_TaiKhoan = new RelayCommand<object>((p) => { return CurrentQuanLyDataContext != ucTaiKhoan; }, (p) =>
            {
                QuanLyTitle = "QUẢN LÝ TÀI KHOẢN";
                ucTaiKhoan = new QuanLyTaiKhoanViewModel();
                CurrentQuanLyDataContext = ucTaiKhoan;
            });

            btnQuanLy_ThamSo = new RelayCommand<object>((p) => { return CurrentQuanLyDataContext != ucThamSo; }, (p) =>
            {
                QuanLyTitle = "QUẢN LÝ THAM SỐ";
                CurrentQuanLyDataContext = ucThamSo;
            });
            btnQuanLy_DangXuat = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DangNhap login = new DangNhap();
                login.Show();
                ((Window)p).Close();
            });


        }
    }
}
