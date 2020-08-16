using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiKhachSan.Model
{
    public class DatabaseQuery
    {
        public DatabaseQuery()
        {

        }

        public static List<NHANVIEN> danhSachNhanVien()
        {
            List<NHANVIEN> dsNV = new List<NHANVIEN>();
            return DataProvider.ISCreated.DB.NHANVIENs.ToList();
        }

        public static List<PHONG> danhSachPhong()
        {
            List<PHONG> dsPhong = new List<PHONG>();
            return DataProvider.ISCreated.DB.PHONGs.ToList();
        }
        public static List<LOAIDV> danhSachLoaiDichVu()
        {
            List<LOAIDV> dsPhong = new List<LOAIDV>();
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

            List<HOADONTHUEPHONG> res = new List<HOADONTHUEPHONG>();
            res = DataProvider.ISCreated.DB.HOADONTHUEPHONGs.ToList();
            foreach (HOADONTHUEPHONG hoaDon in res)
            {
                hoaDon.KHACHHANG = truyVanKhachHangMaHD(hoaDon.MaHoaDon);
                hoaDon.NHANVIEN = truyVanNhanVienMaHD(hoaDon.MaHoaDon);
            }
            return res;
        }
        public static List<PHONG> truyVanDanhSachPhong()
        {

            List<PHONG> res = new List<PHONG>();
            res = DataProvider.ISCreated.DB.PHONGs.ToList();
            foreach (PHONG phong in res)
            {
                phong.LOAIPHONG = truyVanLoaiPhong(phong.PhongID);
            }
            return res;
        }
        public static LOAIDV truyVanLoaiDichVu(string maDichVu)
        {

            LOAIDV res = new LOAIDV();
            res = DataProvider.ISCreated.DB.Database.SqlQuery<LOAIDV>("SELECT LP.* FROM DICHVU P JOIN LOAIDV LP ON P.LoaiDVID=LP.LoaiDVID  WHERE P.DichVuID = @id", new SqlParameter("@id", maDichVu)).FirstOrDefault();
            return res;
        }
        public static List<DICHVU> truyVanDanhSachDichVu()
        {

            List<DICHVU> res = new List<DICHVU>();
            res = DataProvider.ISCreated.DB.DICHVUs.ToList();
            foreach (DICHVU dichVu in res)
            {
                dichVu.LOAIDV = truyVanLoaiDichVu(dichVu.DichVuID);
            }
            return res;
        }
    }
}
