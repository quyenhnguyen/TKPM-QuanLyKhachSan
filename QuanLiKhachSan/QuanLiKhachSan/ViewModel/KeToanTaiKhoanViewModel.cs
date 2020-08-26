using QuanLiKhachSan.Model;
using QuanLiKhachSan.View;
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
    public class KeToanTaiKhoanViewModel : BaseViewModel
    {
        private NHANVIEN _NhanVienDangNhap;
        public NHANVIEN NhanVienDangNhap { get => _NhanVienDangNhap; set { OnPropertyChanged(ref _NhanVienDangNhap, value); } }
        int MaNV { get; set; }

        private bool _KiemTraDoiMatKhau;
        public bool KiemTraDoiMatKhau { get => _KiemTraDoiMatKhau; set { OnPropertyChanged(ref _KiemTraDoiMatKhau, value); } }
        private string _isAdmin;
        public string isAdmin { get => _isAdmin; set { OnPropertyChanged(ref _isAdmin, value); } }
        public ICommand DoiMatKhauCommand { get; set; }


        public ICommand chuyenQuanLy { get; set; }
        public ICommand chuyenLeTan { get; set; }
        public KeToanTaiKhoanViewModel()
        {
            MaNV = UserService.GetCurrentUser.NhanVienID;
            NhanVienDangNhap = DatabaseQuery.truyVanNhanVien(MaNV);
            UserService.LoadUser(NhanVienDangNhap);
            if (NhanVienDangNhap.Loai == 1)
            {
                isAdmin = "Visible";
            }
            else
            {
                isAdmin = "Hidden";
            }
            chuyenQuanLy = new RelayCommand<UserControl>((p) => { return true; }, (p) =>
            {
                QuanLy_Layout keToan = new QuanLy_Layout();
                keToan.Show();
                Window.GetWindow(p).Close();
            });
            chuyenLeTan = new RelayCommand<UserControl>((p) => { return true; }, (p) =>
            {
                LeTan_Layout LetanWindow = new LeTan_Layout();
                LetanWindow.Show();
                Window.GetWindow(p).Close();
            });
            DoiMatKhauCommand = new RelayCommand<UserControl>((p) => { return true; }, (p) =>
            {
                string matKhauMoi = (p.FindName("MatKhauMoi") as PasswordBox).Password;
                string old = (p.FindName("MauKhauCu") as PasswordBox).Password;
                if (string.IsNullOrEmpty(old) || string.IsNullOrEmpty(matKhauMoi))
                    DatabaseQuery.MyMessageBox("Chưa điền đầy đủ thông tin");
                else
                    ResetPassword(old, matKhauMoi);
            });
        }
        void ResetPassword(string OldPassword, string NewPassword)
        {
            string a = SecurityModel.Encrypt(OldPassword);
            if (SecurityModel.Encrypt(OldPassword).Equals(NhanVienDangNhap.MatKhau))
            {
                try
                {
                    string x = SecurityModel.Encrypt(NewPassword);
                    NhanVienDangNhap.MatKhau = SecurityModel.Encrypt(NewPassword);
                    DatabaseQuery.capNhatCSDL();

                    DatabaseQuery.MyMessageBox("Thay đổi mật khẩu thành công");
                    KiemTraDoiMatKhau = false;
                }
                catch
                {
                    DatabaseQuery.MyMessageBox("Thay đổi mật khẩu thất bại");
                }

            }
            else
            {
                DatabaseQuery.MyMessageBox("Mật khẩu cũ không đúng.");
            }

        }

    }
}
