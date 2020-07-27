using QuanLiKhachSan.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiKhachSan.ViewModel
{
    public class LeTanDatPhongViewModel : BaseViewModel
    {
        private int _slPhong;
        public int SlPhong { get => _slPhong; set => OnPropertyChanged(ref _slPhong, value); }

        private int _slPhongTrong;
        public int SlPhongTrong { get => _slPhongTrong; set => OnPropertyChanged(ref _slPhongTrong, value); }

        private int _slPhongDangThue;
        public int SlPhongDangThue { get => _slPhongDangThue; set => OnPropertyChanged(ref _slPhongDangThue, value); }

        private BindingList<PHONG> _dsPhong;
        public BindingList<PHONG> DSPhong { get => _dsPhong; set => OnPropertyChanged(ref _dsPhong, value); }

        private PHONG _phongChon;
        public PHONG PhongChon { get => _phongChon; set => OnPropertyChanged(ref _phongChon, value); }


        public LeTanDatPhongViewModel()
        {

        }
    }
}
