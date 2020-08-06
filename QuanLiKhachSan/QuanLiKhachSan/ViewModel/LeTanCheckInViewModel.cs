using QuanLiKhachSan.Model;
using QuanLiKhachSan.View;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace QuanLiKhachSan.ViewModel
{
    public class LeTanCheckInViewModel : BaseViewModel
    {
        private string _title;
        public string title { get => _title; set { OnPropertyChanged(ref _title, value); } }

        private string _maPhong;
        public string MaPhong { get => _maPhong; set { OnPropertyChanged(ref _maPhong, value); } }

        private string _tenLoaiPhong;
        public string TenLoaiPhong { get => _tenLoaiPhong; set => OnPropertyChanged(ref _tenLoaiPhong, value); }

        private string _ngayCheckIn;
        public string NgayCheckIn { get => _ngayCheckIn; set => OnPropertyChanged(ref _ngayCheckIn, value); }

        private List<LOAIDV> _dsLoaiDichVu;
        public List<LOAIDV> DSLoaiDichVu { get => _dsLoaiDichVu; set => OnPropertyChanged(ref _dsLoaiDichVu, value); }

        private LOAIDV _DVChon;
        public LOAIDV LoaiDichVuChon { get => _DVChon; set { OnPropertyChanged(ref _DVChon, value); } }

        private List<DICHVU> _DSDichVuTheoLoai;
        public List<DICHVU> DSDichVuTheoLoai { get => _DSDichVuTheoLoai; set => OnPropertyChanged(ref _DSDichVuTheoLoai, value); }

        private DICHVU _DichVuChon;
        public DICHVU DichVuChon { get => _DichVuChon; set { OnPropertyChanged(ref _DichVuChon, value); } }
        bool isCheckin { get; set; }
        bool isCheckout { get; set; }

        //***2 giao diện*****
        public SolidColorBrush _mauNen;
        public SolidColorBrush MauNen { get => _mauNen; set { OnPropertyChanged(ref _mauNen, value); } }

        public Visibility _hienThiDangThue;
        public Visibility HienThiDangThue { get => _hienThiDangThue; set { OnPropertyChanged(ref _hienThiDangThue, value); } }

        public Visibility _hienThiConTrong;
        public Visibility HienThiConTrong { get => _hienThiConTrong; set { OnPropertyChanged(ref _hienThiConTrong, value); } }

        HOADONTHUEPHONG HoaDonDangThue { get; set; }


        //Command
        public ICommand LoaiDichVuCommand { get; set; }
        public ICommand CheckinBtnCommand { get; set; }
        public ICommand KhachThuePhongBtnCommand { get; set; }
        public ICommand ChinhSuaKhachBtnCommand { get; set; }
        public ICommand InHoaDonCommand { get; set; }
        public ICommand LuuHoaDonCommand { get; set; }
        public ICommand CheckOutBtnCommand { get; set; }




        //******************constructor******************************
        public LeTanCheckInViewModel(string ma)
        {
            MaPhong = ma;
            PHONG phongHienTai = DatabaseQuery.truyVanPhong(MaPhong);
            isCheckin = false;
            isCheckout = false;

            bool TinhTrangThue = phongHienTai.TinhTrangThue;

            if (TinhTrangThue == false) giaoDienPhongTrong();
            else giaoDienPhongDaThue();

            TenLoaiPhong = DatabaseQuery.truyVanTenLoaiPhong(MaPhong);
            DSLoaiDichVu = DatabaseQuery.danhSachLoaiDichVu();
            LoaiDichVuChon = DSLoaiDichVu[0];
            DSDichVuTheoLoai = DatabaseQuery.danhSachDichVuTheoLoai(LoaiDichVuChon.LoaiDVID);

            LoaiDichVuCommand = new RelayCommand<LOAIDV>((p) => { return LoaiDichVuChon != p; }, (p) =>
              {
                  LoaiDichVuChon = p;
                  DSDichVuTheoLoai = DatabaseQuery.danhSachDichVuTheoLoai(LoaiDichVuChon.LoaiDVID);
              });

            KhachThuePhongBtnCommand = new RelayCommand<object>((p) => { return isCheckin == true; }, (p) =>
            {
                //Xem thông tin trong csdl
            });
            ChinhSuaKhachBtnCommand = new RelayCommand<object>((p) => { return isCheckin == true; }, (p) =>
            {
            });

            // ****************CHECK IN ********************
            CheckinBtnCommand = new RelayCommand<object>((p) => { return isCheckin == false; }, (p) =>
            {

                LeTan_KhachHang wd = new LeTan_KhachHang();

                if (wd.ShowDialog() == true)
                {
                    KHACHHANG KH = ((LeTanKhachHangViewModel)wd.DataContext).KHMoi;
                    MessageBox.Show("HỌ TÊN LÀ: " + KH.HoTen);
                    NgayCheckIn = DateTime.Now.ToString("dd/mm/yyyy-HH:mm:ss");
                    //Thêm vào csdl hóa đơn mới
                    isCheckin = true;
                }

            });
            //*****************END CHECK IN *****************


            // ****************CHECK OUT ********************
            InHoaDonCommand = new RelayCommand<object>((p) => { return isCheckout == true; }, (p) =>
            {

            });
            LuuHoaDonCommand = new RelayCommand<object>((p) => { return isCheckout == true; }, (p) =>
            {

            });
            CheckOutBtnCommand = new RelayCommand<object>((p) => { return isCheckout == false; }, (p) =>
            {
                LeTan_DialogCheckOut dialog = new LeTan_DialogCheckOut(HoaDonDangThue.MaHoaDon);

                if (dialog.ShowDialog() == true)
                {
                    //Thêm vào csdl hóa đơn mới
                    //Đã thêm thành công hóa đơn
                    isCheckout = true;
                }
            });
            //*****************END CHECK OUT *****************
        }

        public void giaoDienPhongDaThue()
        {
            title = MaPhong + " ~ ĐANG THUÊ ";
            MauNen = (SolidColorBrush)Application.Current.Resources["XanhNhatMauNen"];
            HienThiDangThue = Visibility.Visible;
            HienThiConTrong = Visibility.Collapsed;

            isCheckin = true;



            //khi truy vấn csdl
            HoaDonDangThue = DatabaseQuery.truyVanHoaDonDangThue(MaPhong);
            //NgayCheckIn = HoaDonDangThue.ThoiGianThue.ToString("dd/mm/yyyy-HH:mm:ss");
            NgayCheckIn = "31/07/2020 - 2:30:33";
        }
        public void giaoDienPhongTrong()
        {
            title = "CHECK IN PHÒNG " + MaPhong;
            MauNen = (SolidColorBrush)Application.Current.Resources["XamNhat"];
            HienThiConTrong = Visibility.Visible;
            HienThiDangThue = Visibility.Collapsed;


        }
    }
}
