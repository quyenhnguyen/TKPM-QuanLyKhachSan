using QuanLiKhachSan.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLiKhachSan.ViewModel
{
    public class LeTanTraCuuViewModel : BaseViewModel
    {

        private List<HOADONTHUEPHONG> _dsHoaDon;
        public List<HOADONTHUEPHONG> dsHoaDon { get => _dsHoaDon; set => OnPropertyChanged(ref _dsHoaDon, value); }

        private List<PHONG> _dsPhong;
        public List<PHONG> dsPhong { get => _dsPhong; set => OnPropertyChanged(ref _dsPhong, value); }

        private bool _TinhTrangThue;
        public bool TinhTrangThue { get => _TinhTrangThue; set => OnPropertyChanged(ref _TinhTrangThue, value); }

        private bool _TinhTrangTT;
        public bool TinhTrangTT { get => _TinhTrangTT; set => OnPropertyChanged(ref _TinhTrangTT, value); }

        private List<DICHVU> _dsDichVu;
        public List<DICHVU> dsDichVu { get => _dsDichVu; set => OnPropertyChanged(ref _dsDichVu, value); }

        private List<LOAIDV> _dsLoaiDV;
        public List<LOAIDV> dsLoaiDichVu { get => _dsLoaiDV; set => OnPropertyChanged(ref _dsLoaiDV, value); }

        private LOAIDV _selectedLoai { get; set; }
        public LOAIDV selectedLoai
        {
            get { return _selectedLoai; }
            set
            {
                if (_selectedLoai != value)
                {
                    _selectedLoai = value;
                    isChoose = true;
                    dsDichVu = DatabaseQuery.danhSachDichVuTheoLoai(_selectedLoai.LoaiDVID);
                }
            }
        }
        bool isChoose { get; set; }
        public ICommand TatCaDichVuBtnCommand { get; set; }











        //*****CONSTRUCTOR**********
        public LeTanTraCuuViewModel()
        {
            isChoose = true;
            //MessageBox.Show("hihi");
            dsHoaDon = DatabaseQuery.truyVanDanhSachHoaDon();
            dsPhong = DatabaseQuery.truyVanDanhSachPhong();
            dsDichVu = DatabaseQuery.truyVanDanhSachDichVu();
            dsLoaiDichVu = DatabaseQuery.danhSachLoaiDichVu();


            foreach (HOADONTHUEPHONG HD in dsHoaDon)
            {
                TinhTrangTT = HD.ThoiGianTra == null ? true : false;
            }

            foreach (PHONG phong in dsPhong)
            {
                TinhTrangThue = phong.TinhTrangThue;
            }

            TatCaDichVuBtnCommand = new RelayCommand<object>((p) => { return isChoose == true; }, (p) =>
            {
                isChoose = false;
                dsDichVu = DatabaseQuery.truyVanDanhSachDichVu();
                //Xem thông tin trong csdl
            });
        }
    }
}
