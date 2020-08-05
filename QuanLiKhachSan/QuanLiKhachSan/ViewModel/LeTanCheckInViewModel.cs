using QuanLiKhachSan.Model;
using QuanLiKhachSan.View;
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
    public class LeTanCheckInViewModel : BaseViewModel
    {
        private string _title;
        public string title { get => _title; set { OnPropertyChanged(ref _title, value); } }

        private string _maPhong;
        public string MaPhong { get => _maPhong; set { OnPropertyChanged(ref _maPhong, value); } }

        private List<LOAIDV> _dsLoaiDichVu;
        public List<LOAIDV> DSLoaiDichVu { get => _dsLoaiDichVu; set => OnPropertyChanged(ref _dsLoaiDichVu, value); }

        private LOAIDV _DVChon;
        public LOAIDV LoaiDichVuChon
        {
            get => _DVChon;
            set
            {
                OnPropertyChanged(ref _DVChon, value);
            }
        }
        private List<DICHVU> _DSDichVuTheoLoai;
        public List<DICHVU> DSDichVuTheoLoai { get => _DSDichVuTheoLoai; set => OnPropertyChanged(ref _DSDichVuTheoLoai, value); }

        private DICHVU _DichVuChon;
        public DICHVU DichVuChon
        {
            get => _DichVuChon;
            set
            {
                OnPropertyChanged(ref _DichVuChon, value);
            }
        }

        public ICommand loaiDichVuCommand { get; set; }
        public ICommand checkinBtnCommand { get; set; }
        public ICommand KhachThuePhongBtnCommand { get; set; }
        public ICommand ChinhSuaKhachBtnCommand { get; set; }




        public LeTanCheckInViewModel(string ma)
        {
            title = "CHECK IN PHÒNG " + ma;
            bool isCheckin = false;
            DSLoaiDichVu = DatabaseQuery.danhSachLoaiDichVu();
            LoaiDichVuChon = DSLoaiDichVu[0];
            DSDichVuTheoLoai = DatabaseQuery.danhSachDichVuTheoLoai(LoaiDichVuChon.LoaiDVID);

            loaiDichVuCommand = new RelayCommand<LOAIDV>((p) => { return LoaiDichVuChon != p; }, (p) =>
              {
                  LoaiDichVuChon = p;
                  DSDichVuTheoLoai = DatabaseQuery.danhSachDichVuTheoLoai(LoaiDichVuChon.LoaiDVID);
              });
            checkinBtnCommand = new RelayCommand<object>((p) => { return isCheckin == false; }, (p) =>
              {
                  isCheckin = true;
              });
            KhachThuePhongBtnCommand = new RelayCommand<object>((p) => { return isCheckin == true; }, (p) =>
            {
                LeTan_KhachHang wd = new LeTan_KhachHang();
                wd.ShowDialog();
            });
            ChinhSuaKhachBtnCommand = new RelayCommand<object>((p) => { return isCheckin == true; }, (p) =>
            {
            });
        }
    }
}
