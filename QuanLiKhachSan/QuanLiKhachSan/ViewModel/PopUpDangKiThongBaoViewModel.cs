using QuanLiKhachSan.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLiKhachSan.ViewModel
{
    public class PopUpDangKiThongBaoViewModel : BaseViewModel
    {
        public string myname
        {
            get; set;

        }
        public ICommand XoaThongBaoCommand { get; set; }


        private BindingList<LOAITHONGBAO> listLap1;
        public BindingList<LOAITHONGBAO> listLap
        {
            get => listLap1; set
            {
                OnPropertyChanged(ref listLap1, value);
            }
        }



        public PopUpDangKiThongBaoViewModel()
        {
            listLap = DatabaseQueryTN.getLoaiTB();

            XoaThongBaoCommand = new RelayCommand<LOAITHONGBAO>((p) => { return true; }, (p) =>
            {
                listLap.Remove(p);
            });

        }
    }
}