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
    }
}
