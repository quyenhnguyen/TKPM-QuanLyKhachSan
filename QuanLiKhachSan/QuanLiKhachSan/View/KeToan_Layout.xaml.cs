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
    /// Interaction logic for KeToan_Layout.xaml
    /// </summary>
    public partial class KeToan_Layout : Window
    {
        public KeToan_Layout()
        {
            InitializeComponent();
            this.DataContext = new KeToanLayoutViewModel();
        }
    }
}
