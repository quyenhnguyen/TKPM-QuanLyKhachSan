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
    class PopUpImportExcelViewModel : BaseViewModel
    {
        public ICommand DongYBtnCommand { get; set; }
        public ICommand ThoatBtnComamand { get; set; }

        public int index {get; set;}
        public PopUpImportExcelViewModel()
        {
            DongYBtnCommand = new RelayCommand<ListBox>((p) => { return true; }, (p) =>
            {
                //r á ở đây là lấy đc loại, khi chọn cái thứ nahats thì index=0, tieeos theo là 1 2
                index = p.SelectedIndex;
                Window.GetWindow(p).DialogResult=true;
            });
            ThoatBtnComamand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                
                Window.GetWindow(p).DialogResult = false;
            });
        }
    }
}
