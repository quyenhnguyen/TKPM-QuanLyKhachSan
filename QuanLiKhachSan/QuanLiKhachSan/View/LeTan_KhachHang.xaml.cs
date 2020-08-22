using QuanLiKhachSan.Model;
using QuanLiKhachSan.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QuanLiKhachSan.View
{
    /// <summary>
    /// Interaction logic for LeTan_KhachHang.xaml
    /// </summary>
    public partial class LeTan_KhachHang : Window
    {
        public LeTan_KhachHang()
        {
            InitializeComponent();
            this.DataContext = new LeTanKhachHangViewModel();
        }
        public LeTan_KhachHang(int maHD, bool flag)
        {
            InitializeComponent();
            ((LeTanKhachHangViewModel)DataContext).MaHoaDon = maHD;
            ((LeTanKhachHangViewModel)DataContext).IsUpdate = flag;

            this.DataContext = new LeTanKhachHangViewModel(maHD, flag);
        }
    }
}
