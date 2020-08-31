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
    public class KeToanBaoCaoViewModel : BaseViewModel
    {
        private string _TieuDe;
        public string TieuDe { get => _TieuDe; set { _TieuDe = value; OnPropertyChanged(); } }
        private object _CurrentDataContext;
        public object CurrentDataContext { get => _CurrentDataContext; set { _CurrentDataContext = value; OnPropertyChanged(); } }
        public ICommand BaoCaoDichVuCommand { get; set; }
        public ICommand BaoCaoPhongCommand { get; set; }
        public ICommand BaoCaoDoanhThuCommand { get; set; }

        public ICommand troVeCommand { get; set; }






        public KeToanBaoCaoViewModel()
        {
            object ucBaoCaoDichVu = new KeToanBaoCaoDichVuViewModel();
            object ucBaoCaoDoanhThu = new KeToanBaoCaoDoanhThuViewModel();
            object ucBaoCaoPhong = new KeToanBaoCaoPhongViewModel();

            BaoCaoDichVuCommand = new RelayCommand<UserControl>((p) => { return true; }, (p) =>
            {
                HienThongTinThongKeDichVu(p);
                CurrentDataContext = ucBaoCaoDichVu;
                TieuDe = "BÁO CÁO THỐNG KÊ THEO DỊCH VỤ";
            });
            BaoCaoPhongCommand = new RelayCommand<UserControl>((p) => { return true; }, (p) =>
            {
                HienThongTinThongKeDichVu(p);
                CurrentDataContext = ucBaoCaoPhong;
                TieuDe = "BÁO CÁO DOANH SỐ THEO PHÒNG";

            });
            BaoCaoDoanhThuCommand = new RelayCommand<UserControl>((p) => { return true; }, (p) =>
            {
                HienThongTinThongKeDichVu(p);
                CurrentDataContext = ucBaoCaoDoanhThu;
                TieuDe = "BÁO CÁO DOANH THU, LỢI NHUẬN";

            });

            //button trở về click
            troVeCommand = new RelayCommand<UserControl>((p) => { return true; }, (p) =>
            {
                hienManHinhChinhBaoCao(p);
            });
        }
        public void HienThongTinThongKeDichVu(UserControl p)
        {
            (p.FindName("MenuBaoCao") as Grid).Visibility = Visibility.Collapsed;
            (p.FindName("ThongTinHeader") as Border).Visibility = Visibility.Visible;
            (p.FindName("ContentControlBC") as ContentControl).Visibility = Visibility.Visible;
        }
        void hienManHinhChinhBaoCao(UserControl uc)
        {
            (uc.FindName("MenuBaoCao") as Grid).Visibility = Visibility.Visible;
            (uc.FindName("ThongTinHeader") as Border).Visibility = Visibility.Collapsed;
            (uc.FindName("ContentControlBC") as ContentControl).Visibility = Visibility.Collapsed;

        }
    }
}
