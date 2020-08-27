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
    public class KeToanLayoutViewModel : BaseViewModel
    {
        private string _txtTitle;
        public string txtTitle { get => _txtTitle; set { _txtTitle = value; OnPropertyChanged(); } }
        private object _CurrentDataContext;
        public object CurrentDataContext { get => _CurrentDataContext; set { _CurrentDataContext = value; OnPropertyChanged(); } }

        private NHANVIEN _NhanVienDangNhap;
        public NHANVIEN NhanVienDangNhap { get => _NhanVienDangNhap; set { _NhanVienDangNhap = value; OnPropertyChanged(); } }
        #region All Command
        public ICommand btnBaoCaoCommand { get; set; }
        public ICommand btnNhanVienCommand { get; set; }
        public ICommand btnTaiKhoanCommand { get; set; }
        public ICommand btnDangXuat_Command { get; set; }

        #endregion

        public KeToanLayoutViewModel()
        {
            //int MaNhanVien = UserService.GetCurrentUser.NhanVienID;
            int MaNhanVien = 1;
            NhanVienDangNhap = DatabaseQuery.truyVanNhanVien(MaNhanVien);
            object ucBaoCao = new KeToanBaoCaoViewModel();
            object ucNhanVien = new KeToanQLNhanVienViewModel();
            object ucTaiKhoan = new KeToanTaiKhoanViewModel();

            CurrentDataContext = ucBaoCao;
            txtTitle = "BÁO CÁO, THỐNG KÊ";

            btnBaoCaoCommand = new RelayCommand<object>((p) => { return CurrentDataContext != ucBaoCao; }, (p) =>
            {

                ucBaoCao = new KeToanBaoCaoViewModel();
                CurrentDataContext = ucBaoCao;
                txtTitle = "TRANG CHỦ THUÊ, TRẢ PHÒNG";

            });
            btnNhanVienCommand = new RelayCommand<object>((p) => { return CurrentDataContext != ucNhanVien; }, (p) =>
            {
                CurrentDataContext = ucNhanVien;
                txtTitle = "QUẢN LÍ NHÂN VIÊN";

            });
            btnTaiKhoanCommand = new RelayCommand<object>((p) => { return CurrentDataContext != ucTaiKhoan; }, (p) =>
            {
                CurrentDataContext = ucTaiKhoan;
                txtTitle = "QUẢN LÍ TÀI KHOẢNS";

            });
            btnDangXuat_Command = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DangNhap login = new DangNhap();
                login.Show();
                ((Window)p).Close();
                //UserService._CurrentUser = null;
            });
        }
    }

}

