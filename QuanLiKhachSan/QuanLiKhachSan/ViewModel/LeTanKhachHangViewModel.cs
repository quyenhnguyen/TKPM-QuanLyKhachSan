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

        public LeTanKhachHangViewModel()
        {
            ThoatBtnComamand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });
        }
    }
}
