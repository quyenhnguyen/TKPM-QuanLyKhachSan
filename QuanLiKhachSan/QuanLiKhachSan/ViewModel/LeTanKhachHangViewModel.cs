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
        public ICommand ThemMoiBtnCommand { get; set; }

        public string HoTenInput { get; set; }
        public string DiaChiInput { get; set; }
        public string CMNDInput { get; set; }
        public string DienThoaiInput { get; set; }


        public KHACHHANG KHMoi;
        public LeTanKhachHangViewModel()
        {

            ThoatBtnComamand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {

                p.Close();
            });

            ThemMoiBtnCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                KHMoi = new KHACHHANG();
                KHMoi.HoTen = HoTenInput;
                KHMoi.DiaChi = DiaChiInput;
                KHMoi.CMND = decimal.Parse(CMNDInput);
                KHMoi.SDT = decimal.Parse(DienThoaiInput);
                KHMoi.NgayTao = DateTime.Now;
                p.DialogResult = true;

            });
        }
    }
}
