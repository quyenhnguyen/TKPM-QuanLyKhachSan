using QuanLiKhachSan.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLiKhachSan.ViewModel
{
    public class LeTanKhachHangViewModel : BaseViewModel
    {

        public ICommand ThoatBtnComamand { get; set; }
        public ICommand LuuThongTinBtnCommand { get; set; }

        public ICommand ThemMoiBtnCommand { get; set; }

        private string _HoTenInput;
        public string HoTenInput { get => _HoTenInput; set { OnPropertyChanged(ref _HoTenInput, value); } }
        private string _DiaChiInput;
        public string DiaChiInput { get => _DiaChiInput; set { OnPropertyChanged(ref _DiaChiInput, value); } }
        private string _CMNDInput;
        public string CMNDInput { get => _CMNDInput; set { OnPropertyChanged(ref _CMNDInput, value); } }
        private string _DienThoaiInput;
        public string DienThoaiInput { get => _DienThoaiInput; set { OnPropertyChanged(ref _DienThoaiInput, value); } }

        public int MaHoaDon { get; set; }
        public Visibility _ThemMoi;
        public Visibility ThemMoi { get => _ThemMoi; set { OnPropertyChanged(ref _ThemMoi, value); } }
        public Visibility _CapNhat;
        public Visibility IsCapNhat { get => _CapNhat; set { OnPropertyChanged(ref _CapNhat, value); } }

        public bool _IsUpdate;
        public bool IsUpdate { get => _IsUpdate; set { OnPropertyChanged(ref _IsUpdate, value); } }

        public KHACHHANG KHMoi;
        public LeTanKhachHangViewModel()
        {
            IsUpdate = true;
            IsCapNhat = Visibility.Collapsed;
            ThemMoi = Visibility.Visible;
            ThoatBtnComamand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {

                p.Close();
            });

            ThemMoiBtnCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {

                if (HoTenInput == null || DiaChiInput == null || CMNDInput == null || DienThoaiInput == null)
                {
                    DatabaseQuery.MyMessageBox("Chưa điền đầy đủ thông tin !");
                }
                else
                {
                    KHMoi = new KHACHHANG();
                    KHMoi.HoTen = HoTenInput;
                    KHMoi.DiaChi = DiaChiInput;
                    KHMoi.CMND = decimal.Parse(CMNDInput);
                    KHMoi.SDT = decimal.Parse(DienThoaiInput);
                    p.DialogResult = true;
                }


            });
        }
        public LeTanKhachHangViewModel(int maHD, bool flag)
        {
            ThemMoi = Visibility.Collapsed;
            IsCapNhat = Visibility.Visible;
            MaHoaDon = maHD;
            IsUpdate = flag;
            KHACHHANG KH = DatabaseQuery.truyVanKhachHangMaHD(MaHoaDon);
            HoTenInput = KH.HoTen;
            CMNDInput = KH.CMND.ToString(); ;
            DiaChiInput = KH.DiaChi;
            DienThoaiInput = KH.SDT.ToString();

            ThoatBtnComamand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

            LuuThongTinBtnCommand = new RelayCommand<Window>((p) => { return IsUpdate; }, (p) =>
             {
                 KHMoi = new KHACHHANG();
                 KHMoi.HoTen = HoTenInput;
                 KHMoi.DiaChi = DiaChiInput;
                 KHMoi.CMND = decimal.Parse(CMNDInput);
                 KHMoi.SDT = decimal.Parse(DienThoaiInput);
                 KHMoi.NgayTao = DateTime.Now;
                 if (HoTenInput == null || DiaChiInput == null || CMNDInput == null || DienThoaiInput == null)
                 {
                     DatabaseQuery.MyMessageBox("Chưa điền đầy đủ thông tin !");
                 }
                 else
                 {
                     KHMoi = new KHACHHANG();
                     KHMoi.HoTen = HoTenInput;
                     KHMoi.DiaChi = DiaChiInput;
                     KHMoi.CMND = decimal.Parse(CMNDInput);
                     KHMoi.SDT = decimal.Parse(DienThoaiInput);
                     KHMoi.NgayTao = DateTime.Now;
                     p.DialogResult = true;
                 }
             });
        }
    }
}
