using QuanLiKhachSan.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLiKhachSan.Model
{
    public class DatabaseQuery
    {
        public DatabaseQuery()
        {

        }
        public static void capNhatCSDL()
        {
            DataProvider.ISCreated.DB.SaveChanges();
        }

        public static List<NHANVIEN> danhSachNhanVien()
        {
            return DataProvider.ISCreated.DB.NHANVIENs.ToList();
        }

        public static List<PHONG> danhSachPhong()
        {

            return DataProvider.ISCreated.DB.PHONGs.ToList();
        }
        public static int truyVanSoLuongPhongTrong()
        {
            return DataProvider.ISCreated.DB.PHONGs.Where(x => x.TinhTrangThue == false).ToList().Count;
        }
        public static int truyVanSoLuongDangThue()
        {
            return DataProvider.ISCreated.DB.PHONGs.Where(x => x.TinhTrangThue == true).ToList().Count;
        }
        public static List<LOAIDV> danhSachLoaiDichVu()
        {

            return DataProvider.ISCreated.DB.LOAIDVs.ToList();
        }

        public static List<DICHVU> danhSachDichVuTheoLoai(string maLoai)
        {
            List<DICHVU> dsPhong = new List<DICHVU>();
            return DataProvider.ISCreated.DB.DICHVUs.Where(x => x.LoaiDVID == maLoai).ToList();
        }

        public static string truyVanTenLoaiPhong(string phongID)
        {
            string res = DataProvider.ISCreated.DB.Database.SqlQuery<string>("SELECT LP.TenLoai FROM PHONG P JOIN LOAIPHONG LP ON   P.LoaiPhongID=LP.LoaiPhongID WHERE P.PhongID=@id", new SqlParameter("@id", phongID)).FirstOrDefault();
            return res;
        }

        public static PHONG truyVanTenPhongMaHoaDon(int MaHoaDon)
        {
            PHONG res = DataProvider.ISCreated.DB.Database.SqlQuery<PHONG>("SELECT P.* FROM PHONG P JOIN HOADONTHUEPHONG LP ON   P.PhongID=LP.Phong WHERE LP.MaHoaDon=@id", new SqlParameter("@id", MaHoaDon)).FirstOrDefault();
            return res;
        }

        public static PHONG truyVanPhong(string phongID)
        {
            PHONG res = new PHONG();
            res = DataProvider.ISCreated.DB.Database.SqlQuery<PHONG>("SELECT * FROM PHONG WHERE PhongID=@id", new SqlParameter("@id", phongID)).FirstOrDefault();
            return res;
        }

        public static HOADONTHUEPHONG truyVanHoaDonDangThue(string phongID)
        {

            HOADONTHUEPHONG res = new HOADONTHUEPHONG();
            res = DataProvider.ISCreated.DB.Database.SqlQuery<HOADONTHUEPHONG>("SELECT * FROM HOADONTHUEPHONG  WHERE Phong = @id and ThoiGianTra IS NULL", new SqlParameter("@id", phongID)).FirstOrDefault();
            return res;
        }
        public static HOADONTHUEPHONG truyVanHoaDonDangThueMaHD(int maHD)
        {

            HOADONTHUEPHONG res = new HOADONTHUEPHONG();
            res = DataProvider.ISCreated.DB.Database.SqlQuery<HOADONTHUEPHONG>("SELECT * FROM HOADONTHUEPHONG  WHERE MaHoaDon = @id", new SqlParameter("@id", maHD)).FirstOrDefault();
            return res;
        }
        public static KHACHHANG truyVanKhachHangMaHD(int maHD)
        {

            KHACHHANG res = new KHACHHANG();
            res = DataProvider.ISCreated.DB.Database.SqlQuery<KHACHHANG>("SELECT KH.* FROM HOADONTHUEPHONG HD JOIN KHACHHANG KH ON HD.MaKhachHang=KH.MaKH  WHERE MaHoaDon = @id", new SqlParameter("@id", maHD)).FirstOrDefault();
            return res;
        }
        public static NHANVIEN truyVanNhanVienMaHD(int maHD)
        {

            NHANVIEN res = new NHANVIEN();
            res = DataProvider.ISCreated.DB.Database.SqlQuery<NHANVIEN>("SELECT KH.* FROM HOADONTHUEPHONG HD JOIN NHANVIEN KH ON HD.NhanVienTaoHoaDon=KH.NhanVienID  WHERE MaHoaDon = @id", new SqlParameter("@id", maHD)).FirstOrDefault();
            return res;
        }
        public static LOAIPHONG truyVanLoaiPhong(string maPhong)
        {

            LOAIPHONG res = new LOAIPHONG();
            res = DataProvider.ISCreated.DB.Database.SqlQuery<LOAIPHONG>("SELECT LP.* FROM PHONG P JOIN LOAIPHONG LP ON P.LoaiPhongID=LP.LoaiPhongID  WHERE P.PhongID = @id", new SqlParameter("@id", maPhong)).FirstOrDefault();
            return res;
        }
        public static List<HOADONTHUEPHONG> truyVanDanhSachHoaDon()
        {
            return DataProvider.ISCreated.DB.HOADONTHUEPHONGs.ToList();
        }
        public static List<PHONG> truyVanDanhSachPhong()
        {
            return DataProvider.ISCreated.DB.PHONGs.ToList();
        }
        public static LOAIDV truyVanLoaiDichVu(string maDichVu)
        {

            LOAIDV res = new LOAIDV();
            res = DataProvider.ISCreated.DB.Database.SqlQuery<LOAIDV>("SELECT LP.* FROM DICHVU P JOIN LOAIDV LP ON P.LoaiDVID=LP.LoaiDVID  WHERE P.DichVuID = @id", new SqlParameter("@id", maDichVu)).FirstOrDefault();
            return res;
        }
        public static List<DICHVU> truyVanDanhSachDichVu()
        {
            return DataProvider.ISCreated.DB.DICHVUs.ToList();
        }

        /// <summary>
        /// Hóa đơn có các trường: THời gian thuê, nhân viên tạo, phòng, mã khách hàng, trả về hóa đơn vừa thêm để lấy mã hóa đơn
        /// </summary>
        /// <param name="HD"></param>
        public static HOADONTHUEPHONG themHoaDonThuePhong(HOADONTHUEPHONG HD)
        {
            HD.NgayTao = DateTime.Now;
            HOADONTHUEPHONG res = new HOADONTHUEPHONG();
            res = DataProvider.ISCreated.DB.HOADONTHUEPHONGs.Add(HD);
            DataProvider.ISCreated.DB.SaveChanges();
            return res;
        }
        public static KHACHHANG themKhachHangMoi(KHACHHANG KH)
        {
            KH.NgayTao = DateTime.Now;
            KHACHHANG res = new KHACHHANG();
            res = DataProvider.ISCreated.DB.KHACHHANGs.Add(KH);
            DataProvider.ISCreated.DB.SaveChanges();
            return res;
        }
        public static void capNhatKhachHang(KHACHHANG KH)
        {
            KHACHHANG old = DataProvider.ISCreated.DB.KHACHHANGs.Where(x => x.MaKH == KH.MaKH).SingleOrDefault();
            old.HoTen = KH.HoTen;
            old.CMND = KH.CMND;
            old.DiaChi = KH.DiaChi;
            old.SDT = KH.SDT;
            capNhatCSDL();
        }
        public static HOADONTHUEPHONG checkOutHoaDon(HOADONTHUEPHONG HD)
        {
            HOADONTHUEPHONG res = DataProvider.ISCreated.DB.HOADONTHUEPHONGs.Where(x => x.MaHoaDon == HD.MaHoaDon).SingleOrDefault();
            res.ThoiGianTra = HD.ThoiGianTra;
            res.TongTien = HD.TongTien;
            //TIỀN DỊCH VỤ
            //for list dv thêm và cộng lại

            //Cập nhật lại tình trạng phòng
            res.PHONG1.TinhTrangThue = false;
            capNhatCSDL();
            return res;
        }
        public static double TinhTienDichVuHoaDon(int MaHoaDon)
        {
            double tien = 0;
            BindingList<LICHSUTHEMDICHVU> DanhSachDV = truyVanDanhSachLichSuThemDV(MaHoaDon);
            foreach (LICHSUTHEMDICHVU item in DanhSachDV)
                tien += item.SoLuong * item.DICHVU.GiaBan;
            return tien;
        }
        public static double TinhTienThuePhongHoaDon(HOADONTHUEPHONG HD)
        {
            double tien = 0;
            if (HD.ThoiGianTra != null)
            {
                tien = TinhTienThuePhong(HD.ThoiGianThue, (DateTime)HD.ThoiGianTra, HD.PHONG1.DonGia);
            }
            return tien;
        }
        public static double TinhTienThuePhong(DateTime ThoiGianThue, DateTime ThoiGianTra, double GiaPhong)
        {
            double TienThue;
            //DateTime ThoiGianThue = DateTime.ParseExact("2020-02-17 14:00", "yyyy-MM-dd HH:mm", null);
            //DateTime ThoiGianTra = DateTime.ParseExact("2020-02-18 18:00", "yyyy-MM-dd HH:mm", null);
            if (DateTime.Compare(ThoiGianThue.Date, ThoiGianTra.Date) == 0) return (double)GiaPhong / 2;

            var SoNgay = (ThoiGianTra.Date).Subtract(ThoiGianThue.Date).Days - 1;//số ngày ở đủ (khoảng giữa)
            if (SoNgay < 0) SoNgay = 0;

            //////CHECK IN
            TienThue = SoNgay * GiaPhong;
            DateTime DateCheckIn1 = ThoiGianThue;
            TimeSpan ts = new TimeSpan(9, 00, 0);
            DateCheckIn1 = DateCheckIn1.Date + ts;

            DateTime DateCheckIn2 = ThoiGianThue;
            ts = new TimeSpan(14, 00, 0);
            DateCheckIn2 = DateCheckIn2.Date + ts;

            int res1 = DateTime.Compare(ThoiGianThue, DateCheckIn1);
            int res2 = DateTime.Compare(ThoiGianThue, DateCheckIn2);
            //check in trước 9h-> được hưởng giá 50% giá gốc
            if (res1 < 0)
                TienThue = TienThue + ((double)50 / 100) * GiaPhong;
            else
                //check in 9h<checkIn<14h
                if (res2 < 0)
                TienThue = TienThue + ((double)30 / 100) * GiaPhong;
            //sau 14h
            else
                TienThue = TienThue + GiaPhong;

            /////CHECKOUT, tính tiền phụ thu
            DateTime DateCheckOut1 = ThoiGianTra;
            ts = new TimeSpan(12, 00, 0);
            DateCheckOut1 = DateCheckOut1.Date + ts;

            DateTime DateCheckOut2 = ThoiGianTra;
            ts = new TimeSpan(15, 00, 0);
            DateCheckOut2 = DateCheckOut2.Date + ts;

            DateTime DateCheckOut3 = ThoiGianTra;
            ts = new TimeSpan(18, 00, 0);
            DateCheckOut3 = DateCheckOut3.Date + ts;

            int res3 = DateTime.Compare(ThoiGianTra, DateCheckOut1);
            int res4 = DateTime.Compare(ThoiGianTra, DateCheckOut2);
            int res5 = DateTime.Compare(ThoiGianTra, DateCheckOut3);

            /////check out sau 12h-> phụ thu
            if (res3 >= 0)
                //check out 12h<checkOut<15h
                if (res4 < 0)
                    TienThue = TienThue + ((double)30 / 100) * GiaPhong;
                //check out 12h<=checkOut<=15h
                else if (res5 < 0)
                    TienThue = TienThue + ((double)50 / 100) * GiaPhong;
                else TienThue = TienThue + GiaPhong;
            return TienThue;
        }

        public static DICHVU truyVanDichVu(string DichVuID)
        {
            return DataProvider.ISCreated.DB.DICHVUs.Where(x => x.DichVuID == DichVuID).SingleOrDefault();
        }
        public static HOADONTHUEPHONG truyVanHoaDon(int ma)
        {
            return DataProvider.ISCreated.DB.HOADONTHUEPHONGs.Where(x => x.MaHoaDon == ma).SingleOrDefault();
        }
        public static void themLichSuDichVu(LICHSUTHEMDICHVU ls)
        {
            DataProvider.ISCreated.DB.LICHSUTHEMDICHVUs.Add(ls);
            DataProvider.ISCreated.DB.SaveChanges();
        }
        //thêm danh sách các dịch vụ tạm vào lịch sử
        public static void themDanhSachDichVuVaoHoaDon(BindingList<LSTHEMDICHVU> dsDV, int MaHoaDon)
        {
            //DateTime tg = DateTime.Now;
            foreach (LSTHEMDICHVU item in dsDV)
            {
                LICHSUTHEMDICHVU ls = new LICHSUTHEMDICHVU();
                //ls.ThoiGianThem = tg;
                ls.DichVuID = item.DichVuID;
                ls.SoLuong = item.SoLuong;
                ls.ThoiGianThem = item.ThoiGianThem;
                ls.MaHoaDon = MaHoaDon;
                themLichSuDichVu(ls);
            }
        }
        public static BindingList<LICHSUTHEMDICHVU> truyVanDanhSachLichSuThemDV(int MaHoaDon)
        {
            List<LICHSUTHEMDICHVU> list = DataProvider.ISCreated.DB.LICHSUTHEMDICHVUs.Where(x => x.MaHoaDon == MaHoaDon).ToList();
            BindingList<LICHSUTHEMDICHVU> res = new BindingList<LICHSUTHEMDICHVU>(list);
            return res;
        }
        public static void MyMessageBox(string messageBoxText)
        {
            string caption = "Notification";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Information;
            MessageBox.Show(messageBoxText, caption, button, icon);
        }
    }
}
