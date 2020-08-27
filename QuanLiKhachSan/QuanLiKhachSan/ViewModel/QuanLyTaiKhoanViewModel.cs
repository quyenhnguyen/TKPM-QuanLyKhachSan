using Microsoft.Win32;
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
using System.Windows.Media.Imaging;

namespace QuanLiKhachSan.ViewModel
{
    public class QuanLyTaiKhoanViewModel : BaseViewModel
    {
        private NHANVIEN _NhanVienDangNhap;
        public NHANVIEN NhanVienDangNhap { get => _NhanVienDangNhap; set { OnPropertyChanged(ref _NhanVienDangNhap, value); } }
        int MaNV { get; set; }

        private bool _KiemTraDoiMatKhau;
        public bool KiemTraDoiMatKhau { get => _KiemTraDoiMatKhau; set { OnPropertyChanged(ref _KiemTraDoiMatKhau, value); } }
        private BitmapImage _avatar;
        public BitmapImage Avatar { get => _avatar; set { OnPropertyChanged(ref _avatar, value); } }

        public ICommand DoiMatKhauCommand { get; set; }
        public ICommand chuyenKeToan { get; set; }
        public ICommand chuyenLeTan { get; set; }
        public ICommand DoiAnhDaiDienCommand { get; set; }

        public QuanLyTaiKhoanViewModel()
        {
            MaNV = UserService.GetCurrentUser.NhanVienID;
            NhanVienDangNhap = DatabaseQuery.truyVanNhanVien(MaNV);

            //    KeToan_Layout keToan = new KeToan_Layout();
            //    keToan.Show();
            //}
            //            else
            //            {
            //                LeTan_Layout LetanWindow = new LeTan_Layout();
            //LetanWindow.Show();

            UserService.LoadUser(NhanVienDangNhap);
            Avatar = SecurityModel.LoadImage(NhanVienDangNhap.AnhDaiDien);
            chuyenKeToan = new RelayCommand<UserControl>((p) => { return true; }, (p) =>
            {
                KeToan_Layout keToan = new KeToan_Layout();
                keToan.Show();
                Window.GetWindow(p).Close();
            });
            DoiAnhDaiDienCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    try
                    {
                        Uri fileUri = new Uri(openFileDialog.FileName);
                        Avatar = new BitmapImage(new Uri(openFileDialog.FileName));
                        NhanVienDangNhap.AnhDaiDien = SecurityModel.ImageToByte2(Avatar);
                        DatabaseQueryTN.capNhatAvatar(NhanVienDangNhap);
                        DatabaseQuery.MyMessageBox("Đã cập nhật ảnh đại diện");
                    }
                    catch (Exception e)
                    {
                        SecurityModel.Log(e.ToString());
                    }
                }
            }
            );
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
                catch (Exception e)
                {
                    DatabaseQuery.MyMessageBox("Thay đổi mật khẩu thất bại");
                    SecurityModel.Log(e.ToString());
                }

            }
            else
            {
                DatabaseQuery.MyMessageBox("Mật khẩu cũ không đúng.");
            }

        }
    }
}
