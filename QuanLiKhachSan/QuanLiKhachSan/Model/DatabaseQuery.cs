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
        public string TaoDichVuID()
        {
            int id = DataProvider.ISCreated.DB.DICHVUs.ToList().Count;
            if (id < 10)
                return "DV00" + id;
            else if (id < 100)
                return "DV0" + id;
            return "DV" + id;

        }
        public static List<NHANVIEN> danhSachNhanVien()
        {
            return DataProvider.ISCreated.DB.NHANVIENs.Where(x => x.TinhTrang == false).ToList();
        }

        public static List<PHONG> danhSachPhong()
        {

            return DataProvider.ISCreated.DB.PHONGs.Where(x => x.TinhTrangTonTai == false).ToList();
        }
        public static int truyVanSoLuongPhongTrong()
        {
            return DataProvider.ISCreated.DB.PHONGs.Where(x => x.TinhTrangThue == false).ToList().Count;
        }
        public static int truyVanSoLuongDangThue()
        {
            return DataProvider.ISCreated.DB.PHONGs.Where(x => x.TinhTrangThue == true && x.TinhTrangTonTai == false).ToList().Count;
        }
        public static List<LOAIDV> danhSachLoaiDichVu()
        {

            return DataProvider.ISCreated.DB.LOAIDVs.Where(x => x.TinhTrang == false).ToList();
        }

        public static BindingList<DICHVU> danhSachDichVuTheoLoai(string maLoai)
        {
            List<DICHVU> dsPhong = DataProvider.ISCreated.DB.DICHVUs.Where(x => x.LoaiDVID == maLoai && x.TinhTrangTonTai == false).ToList();
            BindingList<DICHVU> res = new BindingList<DICHVU>(dsPhong);
            return res;
        }

        public static string truyVanTenLoaiPhong(string phongID)
        {
            string res = DataProvider.ISCreated.DB.Database.SqlQuery<string>("SELECT LP.TenLoai FROM PHONG P JOIN LOAIPHONG LP ON   P.LoaiPhongID=LP.LoaiPhongID WHERE P.PhongID=@id and P.TinhTrangTonTai=0", new SqlParameter("@id", phongID)).FirstOrDefault();
            return res;
        }

        public static PHONG truyVanTenPhongMaHoaDon(int MaHoaDon)
        {
            PHONG res = DataProvider.ISCreated.DB.Database.SqlQuery<PHONG>("SELECT P.* FROM PHONG P JOIN HOADONTHUEPHONG LP ON   P.PhongID=LP.Phong WHERE LP.MaHoaDon=@id and P.TinhTrangTonTai=0", new SqlParameter("@id", MaHoaDon)).FirstOrDefault();
            return res;
        }

        public static PHONG truyVanPhong(string phongID)
        {
            PHONG res = new PHONG();
            res = DataProvider.ISCreated.DB.Database.SqlQuery<PHONG>("SELECT * FROM PHONG WHERE PhongID=@id and TinhTrangTonTai=0", new SqlParameter("@id", phongID)).FirstOrDefault();
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
            return DataProvider.ISCreated.DB.Database.SqlQuery<HOADONTHUEPHONG>("SELECT * FROM HOADONTHUEPHONG  WHERE MaHoaDon = @id", new SqlParameter("@id", maHD)).FirstOrDefault();
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
            res = DataProvider.ISCreated.DB.Database.SqlQuery<NHANVIEN>("SELECT KH.* FROM HOADONTHUEPHONG HD JOIN NHANVIEN KH ON HD.NhanVienTaoHoaDon=KH.NhanVienID  WHERE HD.MaHoaDon = @id and KH.TinhTrang=0", new SqlParameter("@id", maHD)).FirstOrDefault();
            return res;
        }
        public static LOAIPHONG truyVanLoaiPhong(string maPhong)
        {

            LOAIPHONG res = new LOAIPHONG();
            res = DataProvider.ISCreated.DB.Database.SqlQuery<LOAIPHONG>("SELECT LP.* FROM PHONG P JOIN LOAIPHONG LP ON P.LoaiPhongID=LP.LoaiPhongID  WHERE P.PhongID = @id and P.TinhTrangTonTai=0", new SqlParameter("@id", maPhong)).FirstOrDefault();
            return res;
        }
        public static List<HOADONTHUEPHONG> truyVanDanhSachHoaDon()
        {
            return DataProvider.ISCreated.DB.HOADONTHUEPHONGs.ToList();
        }
        public static List<PHONG> truyVanDanhSachPhong()
        {
            return DataProvider.ISCreated.DB.PHONGs.Where(x => x.TinhTrangTonTai == false).ToList();
        }
        public static LOAIDV truyVanLoaiDichVu(string maDichVu)
        {

            LOAIDV res = new LOAIDV();
            res = DataProvider.ISCreated.DB.Database.SqlQuery<LOAIDV>("SELECT LP.* FROM DICHVU P JOIN LOAIDV LP ON P.LoaiDVID=LP.LoaiDVID  WHERE P.DichVuID = @id and P.TinhTrangTonTai=0", new SqlParameter("@id", maDichVu)).FirstOrDefault();
            return res;
        }
        public static List<DICHVU> truyVanDanhSachDichVu()
        {
            return DataProvider.ISCreated.DB.DICHVUs.Where(x => x.TinhTrangTonTai == false).ToList();
        }

        /// <summary>
        /// Hóa đơn có các trường: THời gian thuê, nhân viên tạo, phòng, mã khách hàng, trả về hóa đơn vừa thêm để lấy mã hóa đơn
        /// </summary>
        /// <param name="HD"></param>
        public static HOADONTHUEPHONG themHoaDonThuePhong(HOADONTHUEPHONG HD)
        {
            HD.NgayTao = DateTime.Now;
            HOADONTHUEPHONG res = DataProvider.ISCreated.DB.HOADONTHUEPHONGs.Add(HD);
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
        public static HOADONTHUEPHONG checkOutHoaDon(HOADONTHUEPHONG HD, DateTime GioRa)
        {
            HOADONTHUEPHONG res = DataProvider.ISCreated.DB.HOADONTHUEPHONGs.Where(x => x.MaHoaDon == HD.MaHoaDon).SingleOrDefault();
            res.ThoiGianTra = GioRa;
            //TÍnh tiền thanh toán
            res.TongTien = TinhTongThanhToan(res, GioRa, res.PHONG1.DonGia);

            //Cập nhật lại tình trạng phòng
            res.PHONG1.TinhTrangThue = false;
            capNhatCSDL();
            return res;
        }
        public static double TinhTongThanhToan(HOADONTHUEPHONG HD, DateTime ThoiGianTra, double DonGiaPhong)
        {
            double TienDichVu = TinhTienDichVuHoaDon(HD.MaHoaDon);
            double TienPhong = TinhTienThuePhong(HD.ThoiGianThue, ThoiGianTra, DonGiaPhong);
            return TienPhong + TienDichVu;
        }
        public static double TinhTienDichVuHoaDon(int MaHoaDon)
        {
            double tien = 0;
            BindingList<LICHSUTHEMDICHVU> DanhSachDV = truyVanDanhSachLichSuThemDV(MaHoaDon);
            foreach (LICHSUTHEMDICHVU item in DanhSachDV)
                tien += item.SoLuong * item.DICHVU.GiaBan;
            return tien;
        }
        public static double TinhTienThuePhong(DateTime ThoiGianThue, DateTime ThoiGianTra, double GiaPhong)
        {
            double TienThue;
            int ThoiGianCheckIn1 = int.Parse(DataProvider.ISCreated.DB.BANGTHAMSOes.Where(x => x.MaThamSo == "THOIGIANIN1").SingleOrDefault().GiaTri);
            int ThoiGianCheckIn2 = int.Parse(DataProvider.ISCreated.DB.BANGTHAMSOes.Where(x => x.MaThamSo == "THOIGIANIN2").SingleOrDefault().GiaTri);
            int ThoiGianCheckIn3 = int.Parse(DataProvider.ISCreated.DB.BANGTHAMSOes.Where(x => x.MaThamSo == "THOIGIANIN3").SingleOrDefault().GiaTri);

            int ThoiGianCheckOut1 = int.Parse(DataProvider.ISCreated.DB.BANGTHAMSOes.Where(x => x.MaThamSo == "THOIGIANOUT1").SingleOrDefault().GiaTri);
            int ThoiGianCheckOut2 = int.Parse(DataProvider.ISCreated.DB.BANGTHAMSOes.Where(x => x.MaThamSo == "THOIGIANOUT2").SingleOrDefault().GiaTri);
            int ThoiGianCheckOut3 = int.Parse(DataProvider.ISCreated.DB.BANGTHAMSOes.Where(x => x.MaThamSo == "THOIGIANOUT3").SingleOrDefault().GiaTri);

            int Gia30 = int.Parse(DataProvider.ISCreated.DB.BANGTHAMSOes.Where(x => x.MaThamSo == "GIA1").SingleOrDefault().GiaTri);
            int Gia50 = int.Parse(DataProvider.ISCreated.DB.BANGTHAMSOes.Where(x => x.MaThamSo == "GIA2").SingleOrDefault().GiaTri);
            int Gia100 = int.Parse(DataProvider.ISCreated.DB.BANGTHAMSOes.Where(x => x.MaThamSo == "GIA3").SingleOrDefault().GiaTri);

            //DateTime ThoiGianThue = DateTime.ParseExact("2020-02-17 14:00", "yyyy-MM-dd HH:mm", null);
            //DateTime ThoiGianTra = DateTime.ParseExact("2020-02-18 18:00", "yyyy-MM-dd HH:mm", null);
            if (DateTime.Compare(ThoiGianThue.Date, ThoiGianTra.Date) == 0) return (double)GiaPhong / 2;

            var SoNgay = (ThoiGianTra.Date).Subtract(ThoiGianThue.Date).Days - 1;//số ngày ở đủ (khoảng giữa)
            if (SoNgay < 0) SoNgay = 0;

            //////CHECK IN
            TienThue = SoNgay * GiaPhong;
            DateTime DateCheckIn1 = ThoiGianThue;
            TimeSpan ts = new TimeSpan(ThoiGianCheckIn2, 00, 0);
            DateCheckIn1 = DateCheckIn1.Date + ts;

            DateTime DateCheckIn2 = ThoiGianThue;
            ts = new TimeSpan(ThoiGianCheckIn3, 00, 0);
            DateCheckIn2 = DateCheckIn2.Date + ts;

            int res1 = DateTime.Compare(ThoiGianThue, DateCheckIn1);
            int res2 = DateTime.Compare(ThoiGianThue, DateCheckIn2);
            //check in trước 9h-> được hưởng giá 50% giá gốc
            if (res1 < 0)
                TienThue = TienThue + ((double)Gia50 / 100) * GiaPhong;
            else
                //check in 9h<checkIn<14h
                if (res2 < 0)
                TienThue = TienThue + ((double)Gia30 / 100) * GiaPhong;
            //sau 14h
            else
                TienThue = TienThue + GiaPhong * ((double)Gia100 / 100);

            /////CHECKOUT, tính tiền phụ thu
            DateTime DateCheckOut1 = ThoiGianTra;
            ts = new TimeSpan(ThoiGianCheckOut1, 00, 0);
            DateCheckOut1 = DateCheckOut1.Date + ts;

            DateTime DateCheckOut2 = ThoiGianTra;
            ts = new TimeSpan(ThoiGianCheckOut2, 00, 0);
            DateCheckOut2 = DateCheckOut2.Date + ts;

            DateTime DateCheckOut3 = ThoiGianTra;
            ts = new TimeSpan(ThoiGianCheckOut3, 00, 0);
            DateCheckOut3 = DateCheckOut3.Date + ts;

            int res3 = DateTime.Compare(ThoiGianTra, DateCheckOut1);
            int res4 = DateTime.Compare(ThoiGianTra, DateCheckOut2);
            int res5 = DateTime.Compare(ThoiGianTra, DateCheckOut3);

            /////check out sau 12h-> phụ thu
            if (res3 >= 0)
                //check out 12h<checkOut<15h
                if (res4 < 0)
                    TienThue = TienThue + ((double)Gia30 / 100) * GiaPhong;
                //check out 12h<=checkOut<=15h
                else if (res5 < 0)
                    TienThue = TienThue + ((double)Gia50 / 100) * GiaPhong;
                else TienThue = TienThue + ((double)Gia100 / 100) * GiaPhong;
            return TienThue;
        }

        public static DICHVU truyVanDichVu(string DichVuID)
        {
            return DataProvider.ISCreated.DB.DICHVUs.Where(x => x.DichVuID == DichVuID && x.TinhTrangTonTai == false).SingleOrDefault();
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

        public static BindingList<DICHVU> TimKiemDichVu(string searchStr)
        {
            BindingList<DICHVU> DanhSachDichVu = new BindingList<DICHVU>();
            var temp = new BindingList<DICHVU>(DataProvider.ISCreated.DB.DICHVUs.Where(x => x.TinhTrangTonTai == false).ToList());
            foreach (var item in temp)
            {
                if (item.TenDichVu.ToLower().Contains(searchStr.ToLower()))
                {
                    if (!DanhSachDichVu.Contains(item))
                    {
                        DanhSachDichVu.Add(item);
                    }
                }
                if (item.LOAIDV.TenLoai.ToLower().Contains(searchStr.ToLower()))
                {
                    if (!DanhSachDichVu.Contains(item))
                    {
                        DanhSachDichVu.Add(item);
                    }
                }
                if (item.GiaBan.ToString().ToLower().Contains(searchStr.ToLower()))
                {
                    if (!DanhSachDichVu.Contains(item))
                    {
                        DanhSachDichVu.Add(item);
                    }
                }
            }
            return DanhSachDichVu;
        }
        public static BindingList<HOADONTHUEPHONG> TimKiemHoaDon(string searchStr)
        {
            BindingList<HOADONTHUEPHONG> res = new BindingList<HOADONTHUEPHONG>();
            var temp = new BindingList<HOADONTHUEPHONG>(DataProvider.ISCreated.DB.HOADONTHUEPHONGs.ToList());
            foreach (var item in temp)
            {
                if (item.MaHoaDon.ToString().ToLower().Contains(searchStr.ToLower()))
                {
                    if (!res.Contains(item))
                    {
                        res.Add(item);
                    }
                }
                if (item.Phong.ToLower().Contains(searchStr.ToLower()))
                {
                    if (!res.Contains(item))
                    {
                        res.Add(item);
                    }
                }
                if (item.KHACHHANG.HoTen.ToLower().Contains(searchStr.ToLower()))
                {
                    if (!res.Contains(item))
                    {
                        res.Add(item);
                    }
                }
                if (item.KHACHHANG.CMND.ToString().ToLower().Contains(searchStr.ToLower()))
                {
                    if (!res.Contains(item))
                    {
                        res.Add(item);
                    }
                }
                if (item.KHACHHANG.SDT.ToString().ToLower().Contains(searchStr.ToLower()))
                {
                    if (!res.Contains(item))
                    {
                        res.Add(item);
                    }
                }
                if (item.NHANVIEN.HoTen.ToString().ToLower().Contains(searchStr.ToLower()))
                {
                    if (!res.Contains(item))
                    {
                        res.Add(item);
                    }
                }
                if (item.NHANVIEN.CMND.ToString().ToLower().Contains(searchStr.ToLower()))
                {
                    if (!res.Contains(item))
                    {
                        res.Add(item);
                    }
                }
                if (item.NHANVIEN.SDT.ToString().ToLower().Contains(searchStr.ToLower()))
                {
                    if (!res.Contains(item))
                    {
                        res.Add(item);
                    }
                }
            }
            return res;
        }
        public static BindingList<PHONG> TimKiemPhong(string searchStr)
        {
            BindingList<PHONG> res = new BindingList<PHONG>();
            var temp = new BindingList<PHONG>(DataProvider.ISCreated.DB.PHONGs.Where(X => X.TinhTrangTonTai == false).ToList());
            foreach (var item in temp)
            {
                if (item.PhongID.ToString().ToLower().Contains(searchStr.ToLower()))
                {
                    if (!res.Contains(item))
                    {
                        res.Add(item);
                    }
                }
                if (item.LoaiPhongID.ToLower().Contains(searchStr.ToLower()))
                {
                    if (!res.Contains(item))
                    {
                        res.Add(item);
                    }
                }
                if (item.LOAIPHONG.TenLoai.ToLower().Contains(searchStr.ToLower()))
                {
                    if (!res.Contains(item))
                    {
                        res.Add(item);
                    }
                }
                if (item.DonGia.ToString().ToLower().Contains(searchStr.ToLower()))
                {
                    if (!res.Contains(item))
                    {
                        res.Add(item);
                    }
                }

            }
            return res;
        }

        public static NHANVIEN truyVanNhanVien(int MaNV)
        {
            return DataProvider.ISCreated.DB.NHANVIENs.Where(x => x.NhanVienID == MaNV && x.TinhTrang == false).SingleOrDefault();
        }

        public static bool tonTaiHoaDonTheoNgay(DateTime NgayBD, DateTime NgayKT)
        {
            NgayBD = NgayBD.Date;
            NgayKT = NgayKT.Date;
            int i = DataProvider.ISCreated.DB.Database.SqlQuery<List<LICHSUTHEMDICHVU>>(
                "SELECT * FROM LICHSUTHEMDICHVU HD  WHERE CAST(HD.ThoiGianThem AS DATE) >= @BD and CAST(HD.ThoiGianThem AS DATE) <= @KT", new SqlParameter("@BD", NgayBD), new SqlParameter("@KT", NgayKT)).ToList().Count;
            return i > 0;
        }
        public static BindingList<ThongTinBaoCao> truyVanDoanhThuTheoDichVu(DateTime NgayBD, DateTime NgayKT)
        {
            BindingList<ThongTinBaoCao> DoanhThuDV = new BindingList<ThongTinBaoCao>();
            List<DICHVU> listDV = truyVanDanhSachDichVu();
            NgayBD = NgayBD.Date;
            NgayKT = NgayKT.Date;
            foreach (DICHVU dv in listDV)
            {
                ThongTinBaoCao baocao = new ThongTinBaoCao();
                baocao.TenDonVi = dv.TenDichVu;
                int soLuongDV = DataProvider.ISCreated.DB.Database.SqlQuery<int>("SELECT  sum(HD.SoLuong) FROM LICHSUTHEMDICHVU HD WHERE  CAST(HD.ThoiGianThem AS DATE) >= @BD and CAST(HD.ThoiGianThem AS DATE) <= @KT and HD.DichVuID = @id", new SqlParameter("@id", dv.DichVuID), new SqlParameter("@BD", NgayBD), new SqlParameter("@KT", NgayKT)).FirstOrDefault();
                baocao.DoanhThu = dv.GiaBan * soLuongDV;
                DoanhThuDV.Add(baocao);
            }

            return DoanhThuDV;
        }
        public static bool tonTaiPhongThueTheoNgay(DateTime NgayBD, DateTime NgayKT)
        {
            NgayBD = NgayBD.Date;
            NgayKT = NgayKT.Date;
            int i = DataProvider.ISCreated.DB.Database.SqlQuery<List<HOADONTHUEPHONG>>(
                "SELECT * FROM HOADONTHUEPHONG HD  WHERE CAST(HD.NgayTao AS DATE) >= @BD and CAST(HD.NgayTao AS DATE) <= @KT", new SqlParameter("@BD", NgayBD), new SqlParameter("@KT", NgayKT)).ToList().Count;
            return i > 0;
        }

        public static BindingList<ThongTinBaoCao> truyVanHoaDonBaoCaoTheoLoiNhuan(DateTime NgayBD, DateTime NgayKT)
        {
            NgayBD = NgayBD.Date;
            NgayKT = NgayKT.Date;
            BindingList<ThongTinBaoCao> DoanhThuPhong = new BindingList<ThongTinBaoCao>();
            List<DateTime> listNgay = Enumerable.Range(0, 1 + NgayKT.Subtract(NgayBD).Days).Select(offset => NgayBD.AddDays(offset)).ToList();


            foreach (DateTime date in listNgay)
            {
                ThongTinBaoCao baocao = new ThongTinBaoCao();
                baocao.TenDonVi = date.ToString("dd/MM/yyyy");
                //Tính tổng doanh thu của ngày đó (phòng+dịch vụ)
                int x = DataProvider.ISCreated.DB.Database.SqlQuery<int>("SELECT  count(*) FROM HOADONTHUEPHONG HD WHERE CAST(HD.ThoiGianTra AS DATE) = CAST(@date AS Date)", new SqlParameter("@date", date.Date)).FirstOrDefault();
                if (x > 0) baocao.DoanhThu = DataProvider.ISCreated.DB.Database.SqlQuery<double>("SELECT  sum(HD.TongTien) FROM HOADONTHUEPHONG HD WHERE CAST(HD.ThoiGianTra AS DATE) = CAST(@date AS Date)", new SqlParameter("@date", date.Date)).FirstOrDefault();
                else baocao.DoanhThu = 0;

                //hóa đơn đaz checkout r và thời gian thêm của lịch sử là = date
                int i = DataProvider.ISCreated.DB.Database.SqlQuery<int>("SELECT  count(*) FROM LICHSUTHEMDICHVU LS JOIN HOADONTHUEPHONG HD ON LS.MaHoaDon=HD.MaHoaDon JOIN DICHVU DV ON DV.DichVuID=LS.DichVuID WHERE CAST(LS.ThoiGianThem AS DATE) = CAST(@date AS Date) and HD.ThoiGianTra is not null", new SqlParameter("@date", date.Date)).FirstOrDefault();
                if (i > 0) baocao.ChiPhi = DataProvider.ISCreated.DB.Database.SqlQuery<double>("SELECT  sum(ls.SoLuong*DV.GiaCungCap) FROM LICHSUTHEMDICHVU LS JOIN HOADONTHUEPHONG HD ON LS.MaHoaDon=HD.MaHoaDon JOIN DICHVU DV ON DV.DichVuID=LS.DichVuID WHERE CAST(LS.ThoiGianThem AS DATE) = CAST(@date AS Date) and HD.ThoiGianTra is not null", new SqlParameter("@date", date.Date)).FirstOrDefault();
                else baocao.ChiPhi = 0;
                baocao.LoiNhuan = baocao.DoanhThu - baocao.ChiPhi;
                DoanhThuPhong.Add(baocao);
            }

            return DoanhThuPhong;
        }
        public static BindingList<ThongTinBaoCao> truyVanHoaDonBaoCaoTheoPhong(DateTime NgayBD, DateTime NgayKT)
        {
            BindingList<ThongTinBaoCao> DoanhThu = new BindingList<ThongTinBaoCao>();
            List<PHONG> listDV = danhSachPhong();
            NgayBD = NgayBD.Date;
            NgayKT = NgayKT.Date;
            foreach (PHONG dv in listDV)
            {
                ThongTinBaoCao baocao = new ThongTinBaoCao();
                baocao.TenDonVi = dv.TenPhong;
                int soLuongDV = DataProvider.ISCreated.DB.Database.SqlQuery<int>("SELECT  count(*) FROM HOADONTHUEPHONG HD WHERE CAST(HD.NgayTao AS DATE) >= @BD and CAST(HD.NgayTao AS DATE) <= @KT and HD.Phong = @id", new SqlParameter("@id", dv.PhongID), new SqlParameter("@BD", NgayBD), new SqlParameter("@KT", NgayKT)).FirstOrDefault();
                baocao.DoanhThu = soLuongDV;
                DoanhThu.Add(baocao);
            }

            return DoanhThu;
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
