using System;
using System.Collections.Generic;
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
    }
}
