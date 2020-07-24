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
    }
}
