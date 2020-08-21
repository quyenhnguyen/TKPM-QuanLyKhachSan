using QuanLiKhachSan.Model;
using QuanLiKhachSan.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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

        private List<LOAIDV> _dsLoaiDichVu;
        public List<LOAIDV> DSLoaiDichVu { get => _dsLoaiDichVu; set => OnPropertyChanged(ref _dsLoaiDichVu, value); }

        private LOAIDV _LoaiDVChon;
        public LOAIDV LoaiDichVuChon { get => _LoaiDVChon; set { OnPropertyChanged(ref _LoaiDVChon, value); } }

        private List<DICHVU> _DSDichVuTheoLoai;
        public List<DICHVU> DSDichVuTheoLoai { get => _DSDichVuTheoLoai; set => OnPropertyChanged(ref _DSDichVuTheoLoai, value); }

        private DICHVU _DichVuChon;
        public DICHVU DichVuChon
        {
            get => _DichVuChon; set
            {
                OnPropertyChanged(ref _DichVuChon, value);
            }
        }
        bool isCheckin { get; set; }
        bool isCheckout { get; set; }

        private string _tenDichVu;
        public string TenDichVu { get => _tenDichVu; set => OnPropertyChanged(ref _tenDichVu, value); }



        //***2 giao diện*****
        public SolidColorBrush _mauNen;
        public SolidColorBrush MauNen { get => _mauNen; set { OnPropertyChanged(ref _mauNen, value); } }

        public Visibility _hienThiDangThue;
        public Visibility HienThiDangThue { get => _hienThiDangThue; set { OnPropertyChanged(ref _hienThiDangThue, value); } }

        public Visibility _hienThiConTrong;
        public Visibility HienThiConTrong { get => _hienThiConTrong; set { OnPropertyChanged(ref _hienThiConTrong, value); } }

        public HOADONTHUEPHONG _HoaDonDangThue;
        public HOADONTHUEPHONG HoaDonDangThue { get => _HoaDonDangThue; set { OnPropertyChanged(ref _HoaDonDangThue, value); } }
        public KHACHHANG KHDangthue { get; set; }
        public int MaNhanVien { get; set; }

        //tạo class lịch sử thêm dv mới với huộc tính sl có thể tự cập nhật
        private BindingList<LSTHEMDICHVU> _DSDichVuTam;
        public BindingList<LSTHEMDICHVU> DanhSachDichVuTam { get => _DSDichVuTam; set { OnPropertyChanged(ref _DSDichVuTam, value); } }
        private BindingList<LSTHEMDICHVU> _DanhSachDichVuDaThem;
        public BindingList<LSTHEMDICHVU> DanhSachDichVuDaThem { get => _DanhSachDichVuDaThem; set { OnPropertyChanged(ref _DanhSachDichVuDaThem, value); } }

        private BindingList<LICHSUTHEMDICHVU> _LichSuThemDichVu;
        public BindingList<LICHSUTHEMDICHVU> LichSuThemDichVu { get => _LichSuThemDichVu; set { OnPropertyChanged(ref _LichSuThemDichVu, value); } }

        private int _SoLuongDichVu;
        public int SoLuongDichVu { get => _SoLuongDichVu; set => OnPropertyChanged(ref _SoLuongDichVu, value); }

        private double _TongThanhToan;
        public double TongThanhToan { get => _TongThanhToan; set => OnPropertyChanged(ref _TongThanhToan, value); }

        //Command
        public ICommand LoaiDichVuCommand { get; set; }
        public ICommand CheckinBtnCommand { get; set; }
        public ICommand KhachThuePhongBtnCommand { get; set; }
        public ICommand ChinhSuaKhachBtnCommand { get; set; }
        public ICommand InHoaDonCommand { get; set; }
        public ICommand LuuHoaDonCommand { get; set; }
        public ICommand CheckOutBtnCommand { get; set; }
        public ICommand DichVuChonCommand { get; set; }
        public ICommand TangSoLuongCommand { get; set; }
        public ICommand GiamSoLuongCommand { get; set; }
        public ICommand ThemDichVuBtnCommand { get; set; }
        public ICommand XoaDichVuThemCommand { get; set; }










        //******************constructor******************************
        public LeTanCheckInViewModel(string ma)
        {
            MaPhong = ma;
            PHONG phongHienTai = DatabaseQuery.truyVanPhong(MaPhong);
            MaNhanVien = 1;
            isCheckin = false;
            isCheckout = false;
            DanhSachDichVuTam = new BindingList<LSTHEMDICHVU>();
            DanhSachDichVuDaThem = new BindingList<LSTHEMDICHVU>();
            SoLuongDichVu = 1;

            //HIỆN THỊ GIAO DIỆN
            if (phongHienTai.TinhTrangThue == false) giaoDienPhongTrong();
            else
            {
                giaoDienPhongDaThue();
                isCheckin = true;
                //Đã thuê
                HoaDonDangThue = DatabaseQuery.truyVanHoaDonDangThue(MaPhong);
                KHDangthue = DatabaseQuery.truyVanKhachHangMaHD(HoaDonDangThue.MaHoaDon);
                LichSuThemDichVu = DatabaseQuery.truyVanDanhSachLichSuThemDV(HoaDonDangThue.MaHoaDon);
                //Hiện thị ds dv đã thêm
                BusinessModel.ThemDichVuVaoDanhSachDaThem(DanhSachDichVuDaThem, LichSuThemDichVu);
                TongThanhToan = DatabaseQuery.TinhTienDichVuHoaDon(HoaDonDangThue.MaHoaDon);

            }
            //HIỆN THỊ loại dịch vụ
            TenLoaiPhong = DatabaseQuery.truyVanTenLoaiPhong(MaPhong);
            DSLoaiDichVu = DatabaseQuery.danhSachLoaiDichVu();
            LoaiDichVuChon = DSLoaiDichVu[0];
            DSDichVuTheoLoai = DatabaseQuery.danhSachDichVuTheoLoai(LoaiDichVuChon.LoaiDVID);
            LoaiDichVuCommand = new RelayCommand<LOAIDV>((p) => { return LoaiDichVuChon != p; }, (p) =>
              {
                  LoaiDichVuChon = p;
                  DSDichVuTheoLoai = DatabaseQuery.danhSachDichVuTheoLoai(LoaiDichVuChon.LoaiDVID);
              });

            #region CHECK IN
            // ****************CHECK IN ********************
            CheckinBtnCommand = new RelayCommand<object>((p) => { return isCheckin == false; }, (p) =>
            {
                LeTan_KhachHang wd = new LeTan_KhachHang();//nhập thông tin khách hàng
                if (wd.ShowDialog() == true)
                {
                    KHACHHANG KH = ((LeTanKhachHangViewModel)wd.DataContext).KHMoi;
                    //Thêm khách hàng mới vào csdl
                    KHDangthue = DatabaseQuery.themKhachHangMoi(KH);
                    //Thêm hóa đơn mới vào csdl
                    HoaDonDangThue = new HOADONTHUEPHONG();
                    HoaDonDangThue.MaKhachHang = KHDangthue.MaKH;
                    HoaDonDangThue.NhanVienTaoHoaDon = MaNhanVien;
                    HoaDonDangThue.Phong = MaPhong;
                    HoaDonDangThue.ThoiGianThue = DateTime.Now;
                    HoaDonDangThue = DatabaseQuery.themHoaDonThuePhong(HoaDonDangThue);//them hoa don

                    //Cập nhật csdl tình trạng phòng đã bị thuê
                    HoaDonDangThue.PHONG1.TinhTrangThue = true;
                    DatabaseQuery.capNhatCSDL();

                    //ĐỔi giao diện
                    DatabaseQuery.MyMessageBox("Check in thành công!");
                    giaoDienPhongDaThue();//hienj giao diện đã thuê
                    isCheckin = true;
                }

            });
            //*****************END CHECK IN *****************
            #endregion

            #region CHECKOUT
            // ****************CHECK OUT ********************
            InHoaDonCommand = new RelayCommand<object>((p) => { return isCheckout == true; }, (p) =>
            {

                DatabaseQuery.MyMessageBox("Đã in thành công hóa đơn");
            });
            LuuHoaDonCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                //DatabaseQuery.MyMessageBox("Đã lưu thành công hóa đơn");
                LeTan_ChiTietHoaDon dialog = new LeTan_ChiTietHoaDon(HoaDonDangThue.MaHoaDon);
                dialog.Show();
            });
            CheckOutBtnCommand = new RelayCommand<object>((p) => { return isCheckout == false; }, (p) =>
            {
                LeTan_DialogCheckOut dialog = new LeTan_DialogCheckOut(HoaDonDangThue.MaHoaDon);

                if (dialog.ShowDialog() == true)
                {
                    //Thêm vào lịch sử thêm dịch vụ CHECKOUT, số lượng:1, Giá là tiền phòng
                    LSTHEMDICHVU dvDaThem = new LSTHEMDICHVU();
                    dvDaThem.SoLuong = 1;
                    dvDaThem.TenDichVu = "TIỀN THUÊ PHÒNG";
                    dvDaThem.DonGia = ((LeTanDialogCheckOutViewModel)dialog.DataContext).TienPhong;
                    dvDaThem.ThoiGianThem = DateTime.Now;
                    DanhSachDichVuDaThem.Add(dvDaThem);
                    TongThanhToan += DatabaseQuery.TinhTienThuePhongHoaDon(HoaDonDangThue);

                    //Cập nhật lại hóa đơn hiện tại với ngày checkout vào csdl
                    HoaDonDangThue.ThoiGianTra = ((LeTanDialogCheckOutViewModel)dialog.DataContext).NgayCheckOut;
                    HoaDonDangThue.TongTien = ((LeTanDialogCheckOutViewModel)dialog.DataContext).TienPhong;
                    HoaDonDangThue = DatabaseQuery.checkOutHoaDon(HoaDonDangThue);


                    //ĐỔi giao diện
                    DatabaseQuery.MyMessageBox("Check out thành công!");
                    //giaoDienPhongTrong();//hienj giao diện đã thuê
                    isCheckout = true;
                }
            });
            //*****************END CHECK OUT *****************
            #endregion

            KhachThuePhongBtnCommand = new RelayCommand<object>((p) => { return isCheckin == true; }, (p) =>
            {
                //Xem thông tin khách thuê phòng
                LeTan_KhachHang wd = new LeTan_KhachHang(HoaDonDangThue.MaHoaDon, false);
                wd.ShowDialog();
            });
            ChinhSuaKhachBtnCommand = new RelayCommand<object>((p) => { return isCheckin == true; }, (p) =>
            {
                //chỉnh sửa thông tin khách thuê
                LeTan_KhachHang wd = new LeTan_KhachHang(HoaDonDangThue.MaHoaDon, true);
                if (wd.ShowDialog() == true)
                {
                    KHACHHANG KH = ((LeTanKhachHangViewModel)wd.DataContext).KHMoi;
                    KH.MaKH = (int)HoaDonDangThue.MaKhachHang;
                    //Cập nhật  lại khách  hàng hiện tại của hóa đơn là KHMoi
                    DatabaseQuery.capNhatKhachHang(KH);
                    DatabaseQuery.MyMessageBox("Cập nhật thông tin thành công !");
                }
            });

            //Xử lí khi chọn 1 dịch vụ sẽ thêm vào ds các dịch vụ tạm cho hóa đơn đó
            DichVuChonCommand = new RelayCommand<object>((p) => { return (phongHienTai.TinhTrangThue == true && isCheckout == false) || (phongHienTai.TinhTrangThue == false && isCheckin == true); }, (p) =>
            {
                LSTHEMDICHVU ls = new LSTHEMDICHVU();
                ls.DichVuID = DichVuChon.DichVuID;
                ls.SoLuong = 1;
                ls.TenDichVu = DichVuChon.TenDichVu;
                ls.DonGia = DichVuChon.GiaBan;
                ls.ThanhTien = ls.DonGia * ls.SoLuong;
                ls.ThoiGianThem = DateTime.Now;
                DanhSachDichVuTam.Add(ls);
            });
            GiamSoLuongCommand = new RelayCommand<LSTHEMDICHVU>((p) => { return p.SoLuong > 0; }, (p) =>
              {
                  p.SoLuong--;
              });
            TangSoLuongCommand = new RelayCommand<LSTHEMDICHVU>((p) => { return true; }, (p) =>
            {
                p.SoLuong++;
            });
            ThemDichVuBtnCommand = new RelayCommand<object>((p) => { return DanhSachDichVuTam.Count > 0; }, (p) =>
              {
                  //Thêm vào csdl
                  DatabaseQuery.themDanhSachDichVuVaoHoaDon(DanhSachDichVuTam, HoaDonDangThue.MaHoaDon);
                  //Cập nhật ở giao diện chi tiết hóa đơn
                  foreach (LSTHEMDICHVU item in DanhSachDichVuTam)
                      DanhSachDichVuDaThem.Add(item);
                  TongThanhToan = DatabaseQuery.TinhTienDichVuHoaDon(HoaDonDangThue.MaHoaDon);

                  //Sau khi thêm thì xóa hết item ở LichSuTam
                  DanhSachDichVuTam.Clear();
              });
            XoaDichVuThemCommand = new RelayCommand<LSTHEMDICHVU>((p) => { return true; }, (p) =>
           {
               DanhSachDichVuTam.Remove(p);
           });

        }

        public void giaoDienPhongDaThue()
        {
            title = MaPhong + " ~ ĐANG THUÊ";
            MauNen = (SolidColorBrush)Application.Current.Resources["XanhNhatMauNen"];
            HienThiDangThue = Visibility.Visible;
            HienThiConTrong = Visibility.Collapsed;
        }
        public void giaoDienPhongTrong()
        {
            title = MaPhong + " ~ CHECK IN";
            MauNen = (SolidColorBrush)Application.Current.Resources["XamNhat"];
            HienThiConTrong = Visibility.Visible;
            HienThiDangThue = Visibility.Collapsed;

        }
    }
}
