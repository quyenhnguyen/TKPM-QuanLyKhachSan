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
    public class DangNhapViewModel : BaseViewModel
    {
        bool isLoaded = false;
        bool isRemember { get; set; }
        bool isLogin { get; set; }

        private NHANVIEN userLogin { get; set; }
        private string _UserInput;
        private string _PassInput;
        public string UserInput { get => _UserInput; set { OnPropertyChanged(ref _UserInput, value); } }


        public ICommand LoginCommand { get; set; }
        public ICommand PassWordEnter { get; set; }
        private SecurityModel security = new SecurityModel();
        private bool checkCondition()
        {
            if (_UserInput == "" || _UserInput == null)
            {
                MessageBox.Show("Chưa nhập ID hoặc Email");
                return false;
            }
            if (_PassInput == "" || _PassInput == null)
            {
                MessageBox.Show("Chưa nhập password");
                return false;
            }
            return true;
        }
        private void Login(PasswordBox p)
        {
            _PassInput = p.Password.ToString();
            if (checkCondition())
            {
                isLogin = checkUserPassword();
                if (isLogin)
                {
                    int loai = layChucVu();
                    UserService.LoadUser(userLogin);
                    if (loai == 1)
                    {
                        MessageBox.Show("Admin");
                       
                    }
                    else if (loai == 2)///
                    {
                        MessageBox.Show("Lễ Tân");
                        LeTan_Layout LetanWindow = new LeTan_Layout();
                        // close cai man hinh login nua ủa
                        LetanWindow.Show();
                    }
                    else
                    {
                        MessageBox.Show("Kế toán");
                    }
                }
                else
                {
                    MessageBox.Show("Sai tài khoản hoặc mật khẩu");
                }
            }
        }
        private bool checkUserPassword()
        {
            List<NHANVIEN> res =  DatabaseQueryTN.thongtinDangNhap(_UserInput, SecurityModel.Encrypt(_PassInput));
            if(res.Count < 1)
            {
                return false;
            }
            userLogin = res[0];
            return true;
        }
        private int layChucVu()
        {
            return DatabaseQueryTN.layChucVu(_UserInput);
        }
        public DangNhapViewModel()
        {
            if (isLoaded) return;
            if (!isLoaded)
            {
                isRemember = false;
                LoginCommand = new RelayCommand<PasswordBox>(
                    (p) => {
                        return true;
                    },
                    (p) =>
                    {
                        isLoaded = true;
                        Login(p);
                    });


                PassWordEnter = new RelayCommand<PasswordBox>(
                    (p) => {
                        return true;
                    },
                    (p) =>
                    {
                        isLoaded = true;
                        Login(p);
                    });
            }
        }
    }
}
