using QuanLiKhachSan.Model;
using QuanLiKhachSan.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLiKhachSan.ViewModel
{

    public class LeTanTaiKhoanViewModel : BaseViewModel
    {
        private NHANVIEN _NhanVienDangNhap;
        public NHANVIEN NhanVienDangNhap { get => _NhanVienDangNhap; set { OnPropertyChanged(ref _NhanVienDangNhap, value); } }
        int MaNV { get; set; }
        private bool _KiemTraDoiMatKhau;
        public bool KiemTraDoiMatKhau { get => _KiemTraDoiMatKhau; set { OnPropertyChanged(ref _KiemTraDoiMatKhau, value); } }
        private string _isUser;
        public string isUser { get => _isUser; set { OnPropertyChanged(ref _isUser, value); } }
        private string _UserRole;
        public string UserRole { get => _UserRole; set { OnPropertyChanged(ref _UserRole, value); } }
        private int _checkUser;
        public int checkUser { get => _checkUser; set { OnPropertyChanged(ref _checkUser, value); } }
        public ICommand DoiMatKhauCommand { get; set; }
        public ICommand ChuyenDoiUser { get; set; }
        public LeTanTaiKhoanViewModel()
        {
            MaNV = UserService.GetCurrentUser.NhanVienID;
            checkUser = UserService.GetCurrentUser.Loai;
            if (checkUser == 1)
            {
                UserRole = "Chuyển về quản lý";
                isUser = "Visible";
            }
            else if(checkUser==2)
            {
                UserRole = "Chuyển về kế toán";
                isUser = "Visible";
            }
            else
            {
                isUser = "Hidden";
            }
            ChuyenDoiUser = new RelayCommand<UserControl>((p) => { return true; }, (p) =>
            {
                UserService.LoadUser(NhanVienDangNhap);
                if (checkUser == 1)
                {
                    QuanLy_Layout LetanWindow = new QuanLy_Layout();
                    LetanWindow.Show();
                    Window.GetWindow(p).Close();
                }
                else if (checkUser == 2)
                {
                    KeToan_Layout LetanWindow = new KeToan_Layout();
                    LetanWindow.Show();
                    Window.GetWindow(p).Close();
                }
                else { return; }
            });

            NhanVienDangNhap = DatabaseQuery.truyVanNhanVien(MaNV);
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

            if (SecurityModel.Encrypt(OldPassword).Equals(NhanVienDangNhap.MatKhau))
            {
                try
                {
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
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
    }
}
