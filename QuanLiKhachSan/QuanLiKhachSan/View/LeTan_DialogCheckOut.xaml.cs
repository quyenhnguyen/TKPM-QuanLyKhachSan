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
    /// Interaction logic for LeTan_DialogCheckOut.xaml
    /// </summary>
    public partial class LeTan_DialogCheckOut : Window
    {
        public LeTan_DialogCheckOut()
        {
            InitializeComponent();
        }
        public LeTan_DialogCheckOut(int maHD)
        {
            InitializeComponent();
            this.DataContext = new LeTanDialogCheckOutViewModel(maHD);
            ((LeTanDialogCheckOutViewModel)DataContext).MaHoaDon = maHD;
        }
    }
}
