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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLiKhachSan.View
{
    /// <summary>
    /// Interaction logic for LeTan_CheckinPhongTrong.xaml
    /// </summary>
    public partial class LeTan_CheckinPhongTrong : UserControl
    {
        public LeTan_CheckinPhongTrong()
        {
            InitializeComponent();
        }
        public LeTan_CheckinPhongTrong(string maPhong)
        {
            InitializeComponent();
            ((LeTanCheckInViewModel)DataContext).MaPhong = maPhong;
            this.DataContext = new LeTanCheckInViewModel(maPhong);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
