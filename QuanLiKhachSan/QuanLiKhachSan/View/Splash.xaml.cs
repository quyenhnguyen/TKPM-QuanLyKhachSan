using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using QuanLiKhachSan.View;

namespace CFHotel
{
    /// <summary>
    /// Interaction logic for Splash.xaml
    /// </summary>
    public partial class Splash : Window
    {
        private const int time = 2;
        public Splash()
        {
            InitializeComponent();
            loadProcess();
        }
        public void loadProcess()
        {
            text.Text = "Power by Q&Q Corporation";
            text.Opacity = 0;
            processbar.Value = 0;
            Task.Run(() =>
            {
                for (int i = 0; i < time * 300; i++)
                {
                    Thread.Sleep(1);
                    this.Dispatcher.Invoke(() =>
                    {
                        processbar.Value = (i / time) / 3;
                        if (i == time * 5000 - 100)
                        {
                            component.Text = "Mọi thứ đã ổn định";
                        }
                        text.Opacity = (float)(i * 1.0 / 100);
                    });
                }
                Thread.Sleep(100);
                this.Dispatcher.Invoke(() =>
                {
                    Window signin = new DangNhap();
                    this.Close();
                    signin.Show();
                });
            });
        }
    }
}
