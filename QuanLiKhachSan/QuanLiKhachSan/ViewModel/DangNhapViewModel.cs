using QuanLiKhachSan.Model;
using QuanLiKhachSan.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
        public ICommand PasswordChangedCommand { get; set; }
        private SecurityModel security = new SecurityModel();
        private bool checkCondition()
        {
            if (_UserInput == "" || _UserInput == null)
            {
                DatabaseQuery.MyMessageBox("Chưa nhập ID hoặc Email");
                return false;
            }
            if (_PassInput == "" || _PassInput == null)
            {
                DatabaseQuery.MyMessageBox("Chưa nhập password");
                return false;
            }
            return true;
        }
        private void Login(Window p)
        {
            //_PassInput = p.Password.ToString();
            if (checkCondition())
            {
                isLogin = checkUserPassword();
                if (isLogin)
                {
                    int loai = layChucVu();
                    UserService._CurrentUser = null;
                    UserService.LoadUser(userLogin);
                    if (loai == 1)
                    {

                        QuanLy_Layout quanliwd = new QuanLy_Layout();
                        quanliwd.Show();
                    }
                    else if (loai == 2)
                    {
                        KeToan_Layout keToan = new KeToan_Layout();
                        keToan.Show();
                    }
                    else
                    {
                        LeTan_Layout LetanWindow = new LeTan_Layout();
                        LetanWindow.Show();
                    }
                }
                else
                {
                    DatabaseQuery.MyMessageBox("Sai tài khoản hoặc mật khẩu");
                    return;
                }
                p.Close();
            }
        }
        private bool checkUserPassword()
        {
            List<NHANVIEN> res = DatabaseQueryTN.thongtinDangNhap(_UserInput, SecurityModel.Encrypt(_PassInput));
            if (res.Count < 1)
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
            //0 là giao diện cơ bản
            //1 là giao diện tiên tiến
            //Đọc file để xem là giao diện nào
            ResourceDictionary GiaoDienTienTienResource = new ResourceDictionary() { Source = new Uri("./DictionaryResources/Styles.xaml", UriKind.RelativeOrAbsolute) };
            ResourceDictionary GiaoDienCoBanResource = new ResourceDictionary() { Source = new Uri("./DictionaryResources/BasicTheme.xaml", UriKind.RelativeOrAbsolute) };
            Collection<ResourceDictionary> QLKSResource = Application.Current.Resources.MergedDictionaries;
            QLKSResource.Remove(GiaoDienCoBanResource);
            QLKSResource.Remove(GiaoDienTienTienResource);

            try
            {
                using (StreamReader sr = File.OpenText("app.config"))
                {
                    int loai = int.Parse(sr.ReadLine());
                    PopUpDoiGiaoDienViewModel.ChuyenDoiGiaoDien(loai);
                }
            }
            catch (Exception e)
            {
                //Nếu file ko có thông tin=> mặc định là giao diện cơ bản
                PopUpDoiGiaoDienViewModel.ChuyenDoiGiaoDien(0);

            }


            if (isLoaded) return;
            if (!isLoaded)
            {
                isRemember = false;
                LoginCommand = new RelayCommand<Window>(
                    (p) =>
                    {
                        return true;
                    },
                    (p) =>
                    {
                        isLoaded = true;
                        Login(p);
                    });


                PassWordEnter = new RelayCommand<Window>(
                    (p) =>
                    {
                        return true;
                    },
                    (p) =>
                    {
                        isLoaded = true;
                        Login(p);
                    });
                PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { _PassInput = p.Password; });
            }
        }
    }
}
