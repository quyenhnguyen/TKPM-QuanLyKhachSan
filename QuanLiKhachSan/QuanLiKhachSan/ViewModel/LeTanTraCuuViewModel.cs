using QuanLiKhachSan.Model;
using QuanLiKhachSan.View;
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
    public class LeTanTraCuuViewModel : BaseViewModel
    {

        private BindingList<HOADONTHUEPHONG> _dsHoaDon;
        public BindingList<HOADONTHUEPHONG> dsHoaDon { get => _dsHoaDon; set => OnPropertyChanged(ref _dsHoaDon, value); }

        private BindingList<PHONG> _dsPhong;
        public BindingList<PHONG> dsPhong { get { return new BindingList<PHONG>(DatabaseQuery.truyVanDanhSachPhong()); } set => OnPropertyChanged(ref _dsPhong, value); }

        private bool _TinhTrangThue;
        public bool TinhTrangThue { get => _TinhTrangThue; set => OnPropertyChanged(ref _TinhTrangThue, value); }

        private bool _TinhTrangTT;
        public bool TinhTrangTT { get => _TinhTrangTT; set => OnPropertyChanged(ref _TinhTrangTT, value); }

        private BindingList<DICHVU> _dsDichVu;
        public BindingList<DICHVU> dsDichVu { get => _dsDichVu; set => OnPropertyChanged(ref _dsDichVu, value); }
        private BindingList<KHACHHANG> _dsKhachHang;
        public BindingList<KHACHHANG> dsKhachHang { get => _dsKhachHang; set => OnPropertyChanged(ref _dsKhachHang, value); }

        private List<LOAIDV> _dsLoaiDV;
        public List<LOAIDV> dsLoaiDichVu { get => _dsLoaiDV; set => OnPropertyChanged(ref _dsLoaiDV, value); }
        private HOADONTHUEPHONG _HoaDonChon;
        public HOADONTHUEPHONG HoaDonChon
        {
            get => _HoaDonChon; set
            {
                OnPropertyChanged(ref _HoaDonChon, value);
                LeTan_ChiTietHoaDon dialog = new LeTan_ChiTietHoaDon(_HoaDonChon.MaHoaDon);
                dialog.ShowDialog();
            }
        }
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
        private string _txtSearch;
        public string txtSearch { get => _txtSearch; set => OnPropertyChanged(ref _txtSearch, value); }
        bool isChoose { get; set; }
        public ICommand TatCaDichVuBtnCommand { get; set; }
        public ICommand TimKiemBtnCommand { get; set; }


        //*****CONSTRUCTOR**********
        public LeTanTraCuuViewModel()
        {
            isChoose = true;
            //MessageBox.Show("hihi");
            dsHoaDon = new BindingList<HOADONTHUEPHONG>(DatabaseQuery.truyVanDanhSachHoaDon());
            //dsPhong = new BindingList<PHONG>(DatabaseQuery.truyVanDanhSachPhong());
            dsDichVu = new BindingList<DICHVU>(DatabaseQuery.truyVanDanhSachDichVu());
            dsLoaiDichVu = DatabaseQuery.danhSachLoaiDichVu();
            dsKhachHang = new BindingList<KHACHHANG>(DatabaseQuery.danhSachKhachHang());


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
                dsDichVu = new BindingList<DICHVU>(DatabaseQuery.truyVanDanhSachDichVu());
                //Xem thông tin trong csdl
            });

            TimKiemBtnCommand = new RelayCommand<TabControl>((p) => { return isChoose == true; }, (p) =>
            {
                int TabDangChon = p.SelectedIndex;
                switch (TabDangChon)
                {
                    //hóa đơn
                    case 0:
                        dsHoaDon = DatabaseQuery.TimKiemHoaDon(txtSearch);
                        break;
                    //phòng
                    case 1:
                        dsPhong = DatabaseQuery.TimKiemPhong(txtSearch);
                        break;
                    //dịch vụ
                    case 2:
                        dsDichVu = DatabaseQuery.TimKiemDichVu(txtSearch);
                        break;
                    //Khách hàng
                    case 3:
                        dsKhachHang = DatabaseQuery.TimKiemKhachHang(txtSearch);
                        break;

                    default:
                        break;
                }


            });
        }
    }
}
