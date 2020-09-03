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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QuanLiKhachSan.View
{
    /// <summary>
    /// Interaction logic for ManHinhLoading.xaml
    /// </summary>
    /// 

    public partial class ManHinhLoading : Window
    {
        public Storyboard story { get; set; }
        public ManHinhLoading()
        {
            InitializeComponent();
            story = (Storyboard)TryFindResource("mystoryboard");
            story.Begin();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
