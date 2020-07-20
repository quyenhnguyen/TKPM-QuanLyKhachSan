using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLiKhachSan.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public bool isLoaded = false;

        public MainViewModel()
        {
            if (isLoaded != true)
            {
                MessageBox.Show("hii");
            }

        }
    }
}
