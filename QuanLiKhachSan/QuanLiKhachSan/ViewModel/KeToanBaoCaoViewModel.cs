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
        private object _CurrentDataContext;
        public object CurrentDataContext { get => _CurrentDataContext; set { _CurrentDataContext = value; OnPropertyChanged(); } }
        public ICommand BaoCaoDichVuCommand { get; set; }
        public ICommand BaoCaoPhongCommand { get; set; }
        public ICommand BaoCaoDoanhThuCommand { get; set; }







        public KeToanBaoCaoViewModel()
        {



            object ucBaoCaoDichVu = new KeToanBaoCaoDichVuViewModel();
            object ucBaoCaoDoanhThu = new KeToanBaoCaoDoanhThuViewModel();
            object ucBaoCaoPhong = new KeToanBaoCaoPhongViewModel();

            BaoCaoDichVuCommand = new RelayCommand<UserControl>((p) => { return true; }, (p) =>
            {
                HienThongTinThongKeDichVu(p);
                CurrentDataContext = ucBaoCaoDichVu;
            });
            BaoCaoPhongCommand = new RelayCommand<UserControl>((p) => { return true; }, (p) =>
            {
                HienThongTinThongKeDichVu(p);
                CurrentDataContext = ucBaoCaoPhong;
            });
            BaoCaoDoanhThuCommand = new RelayCommand<UserControl>((p) => { return true; }, (p) =>
            {
                HienThongTinThongKeDichVu(p);
                CurrentDataContext = ucBaoCaoDoanhThu;
            });
        }
        public void HienThongTinThongKeDichVu(UserControl p)
        {
            (p.FindName("MenuBaoCao") as Grid).Visibility = Visibility.Collapsed;
        }
    }
}
