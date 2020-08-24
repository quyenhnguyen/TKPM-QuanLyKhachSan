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
    public class DatabaseQueryTN
    {
        public DatabaseQueryTN()
        {

        }
        public static void capNhatCSDL()
        {
            DataProvider.ISCreated.DB.SaveChanges();
        }
        public static List<NHANVIEN> danhSachNhanVien()
        {
            return DataProvider.ISCreated.DB.NHANVIENs.Where(x => (x.TinhTrang == false)).ToList();
        }
        public static List<PHONG> danhSachPhong()
        {
            return DataProvider.ISCreated.DB.PHONGs.Where(x => (x.TinhTrangTonTai == false)).ToList();
        }

        public static List<LOAINHANVIEN> danhSachLoaiNV()
        {
            return DataProvider.ISCreated.DB.LOAINHANVIENs.Where(x => (x.TinhTrang == false)).ToList();
        }

        public static List<LOAIPHONG> danhsachLoaiPhong()
        {
            return DataProvider.ISCreated.DB.LOAIPHONGs.Where(x => (x.TinhTrang == false)).ToList();
        }

        public static NHANVIEN themNhanVienMoi(NHANVIEN nv)
        {
            nv.NgayTao = DateTime.Now;
            NHANVIEN res = new NHANVIEN();
            res = DataProvider.ISCreated.DB.NHANVIENs.Add(nv);
            DataProvider.ISCreated.DB.SaveChanges();
            return res;
        }
        public static PHONG themMoiPhong(PHONG nv)
        {
            nv.NgayTao = DateTime.Now;
            PHONG res = new PHONG();
            res = DataProvider.ISCreated.DB.PHONGs.Add(nv);
            DataProvider.ISCreated.DB.SaveChanges();
            return res;
        }
        public static LOAIPHONG themMoiLoaiPhong(LOAIPHONG nv)
        {
            nv.NgayTao = DateTime.Now;
            LOAIPHONG res = new LOAIPHONG();
            res = DataProvider.ISCreated.DB.LOAIPHONGs.Add(nv);
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
        public static List<NHANVIEN> thongtinDangNhap(string user, string pass)
        {
            List<NHANVIEN> res = new List<NHANVIEN>();
            res = DataProvider.ISCreated.DB.NHANVIENs.Where(x => (x.TenDangNhap == user || x.Email == user) && x.MatKhau == pass).ToList();
            //res = DataProvider.ISCreated.DB.Database.SqlQuery<NHANVIEN>("SELECT * FROM NHANVIEN  WHERE Phong = @id and ThoiGianTra IS NULL", new SqlParameter("@id", phongID)).FirstOrDefault();
            return res;
        }
        public static int layChucVu(string user)
        {
            int res = DataProvider.ISCreated.DB.Database.SqlQuery<int>("SELECT LOAI FROM NHANVIEN WHERE TenDangNhap = @user or Email = @user", new SqlParameter("@user", user)).FirstOrDefault();
            return res;
        }

        public static int layNHANVIEN(string user)
        {
            int res = DataProvider.ISCreated.DB.Database.SqlQuery<int>("SELECT LOAI FROM NHANVIEN WHERE TenDangNhap = @user or Email = @user", new SqlParameter("@user", user)).FirstOrDefault();
            return res;
        }
        public static bool kiemtraTonTai(string user, string email)
        {
            int res = DataProvider.ISCreated.DB.NHANVIENs.Where(x => (x.TenDangNhap == user || x.Email == email)).ToList().Count;
            if (res > 0) return true;
            return false;
        }
        public static bool kiemTraTonTaiLoaiPhong(string loaiPhongID)
        {
            int res = DataProvider.ISCreated.DB.LOAIPHONGs.Where(x => (x.LoaiPhongID == loaiPhongID)).ToList().Count;
            if (res > 0) return true;
            return false;
        }
        public static bool kiemTraTonTaiPhong(string phongID)
        {
            int res = DataProvider.ISCreated.DB.PHONGs.Where(x => (x.LoaiPhongID == phongID)).ToList().Count;
            if (res > 0) return true;
            return false;
        }

        public static bool isDeleteRoomType(string loaiPhongID)
        {
            bool res = DataProvider.ISCreated.DB.Database.SqlQuery<bool>("SELECT TinhTrang FROM LOAIPHONG WHERE LoaiPhongID = @loaiPhongID", new SqlParameter("@loaiPhongID", loaiPhongID)).FirstOrDefault();
            return res;
        }
        public static bool isDeleteRoom(string PhongID)
        {
            bool res = DataProvider.ISCreated.DB.Database.SqlQuery<bool>("SELECT TinhTrangTonTai FROM LOAIPHONG WHERE PhongID = @PhongID", new SqlParameter("@PhongID", PhongID)).FirstOrDefault();
            return res;
        }
        public static void capNhatNhanVien(NHANVIEN nv)
        {
            NHANVIEN old = DataProvider.ISCreated.DB.NHANVIENs.Where(x => x.NhanVienID == nv.NhanVienID).SingleOrDefault();
            old.TenDangNhap = nv.TenDangNhap;
            old.HoTen = nv.HoTen;
            old.DiaChi = nv.DiaChi;
            old.NgaySinh = nv.NgaySinh;
            old.SDT = nv.SDT;
            old.CMND = nv.CMND;
            old.Loai = nv.Loai;
            old.Email = nv.Email;
            old.AnhDaiDien = nv.AnhDaiDien;
            capNhatCSDL();
        }
        public static void capNhatPhong(PHONG nv)
        {
            PHONG old = DataProvider.ISCreated.DB.PHONGs.Where(x => x.PhongID == nv.PhongID).SingleOrDefault();
            old.TenPhong = nv.TenPhong;
            old.DonGia = nv.DonGia;
            old.LoaiPhongID = nv.LoaiPhongID;
            old.TinhTrangTonTai = false;
            old.TinhTrangThue = true; // true --> chua thue
            try
            {
                old.TinhTrangThue = nv.TinhTrangThue;
            }
            catch (Exception)
            {

            }
            capNhatCSDL();
        }

        public static void capNhatLoaiPhong(LOAIPHONG nv)
        {
            LOAIPHONG old = DataProvider.ISCreated.DB.LOAIPHONGs.Where(x => x.LoaiPhongID == nv.LoaiPhongID).SingleOrDefault();
            old.TenLoai = nv.TenLoai;
            old.LoaiPhongID = nv.LoaiPhongID;
            capNhatCSDL();
        }
        public static void xoaNhanVien(NHANVIEN nv)
        {
            NHANVIEN old = DataProvider.ISCreated.DB.NHANVIENs.Where(x => x.NhanVienID == nv.NhanVienID).SingleOrDefault();
            old.TinhTrang = true;
            capNhatCSDL();
        }
        public static void xoaLoaiPhong(LOAIPHONG nv)
        {
            LOAIPHONG old = DataProvider.ISCreated.DB.LOAIPHONGs.Where(x => x.LoaiPhongID == nv.LoaiPhongID).SingleOrDefault();
            old.TinhTrang = true;
            capNhatCSDL();
        }
        public static void xoaPhong(PHONG nv)
        {
            PHONG old = DataProvider.ISCreated.DB.PHONGs.Where(x => x.PhongID == nv.PhongID).SingleOrDefault();
            old.TinhTrangTonTai = true;
            capNhatCSDL();
        }
        public static bool isDeleteUser(string userName, string email)
        {
            bool res = DataProvider.ISCreated.DB.Database.SqlQuery<bool>("SELECT TinhTrang FROM NHANVIEN WHERE TenDangNhap = @user or Email = @email", new SqlParameter("@user", userName), new SqlParameter("@email", email)).FirstOrDefault();
            return res;
        }

        public static BindingList<NHANVIEN> timKiemNhanVien(string value)
        {
            BindingList<NHANVIEN> NHANVIENs = new BindingList<NHANVIEN>();
            BindingList<NHANVIEN> temp = new BindingList<NHANVIEN>(DataProvider.ISCreated.DB.NHANVIENs.Where(x => x.TinhTrang != true).ToArray());
            foreach (var item in temp)
            {
                if (item.HoTen.ToLower().Contains(value.ToLower()))
                {
                    if (!NHANVIENs.Contains(item))
                    {
                        NHANVIENs.Add(item);
                    }
                }
                else if (item.DiaChi.ToLower().Contains(value.ToLower()))
                {
                    if (!NHANVIENs.Contains(item))
                    {
                        NHANVIENs.Add(item);
                    }

                }
                else if (item.CMND.ToString().ToLower().Contains(value.ToLower()))
                {
                    if (!NHANVIENs.Contains(item))
                    {
                        NHANVIENs.Add(item);
                    }

                }
                else if (item.NgaySinh.ToString("dd/mm/yyyy").ToLower().Contains(value.ToLower()))
                {
                    if (!NHANVIENs.Contains(item))
                    {
                        NHANVIENs.Add(item);
                    }

                }
                else if (item.SDT.ToString().ToLower().Contains(value.ToLower()))
                {
                    if (!NHANVIENs.Contains(item))
                    {
                        NHANVIENs.Add(item);
                    }
                }
                else if (item.TenDangNhap.ToString().ToLower().Contains(value.ToLower()))
                {
                    if (!NHANVIENs.Contains(item))
                    {
                        NHANVIENs.Add(item);
                    }
                }
            }
            if (NHANVIENs != null)
                return NHANVIENs;
            return null;
        }
        public static BindingList<LOAIPHONG> timKiemLoaiPhong(string value)
        {
            BindingList<LOAIPHONG> NHANVIENs = new BindingList<LOAIPHONG>();
            BindingList<LOAIPHONG> temp = new BindingList<LOAIPHONG>(DataProvider.ISCreated.DB.LOAIPHONGs.Where(x => (x.TinhTrang == false)).ToArray());
            foreach (var item in temp)
            {
                if (item.LoaiPhongID.ToLower().Contains(value.ToLower()))
                {
                    if (!NHANVIENs.Contains(item))
                    {
                        NHANVIENs.Add(item);
                    }
                }
                else if (item.TenLoai.ToLower().Contains(value.ToLower()))
                {
                    if (!NHANVIENs.Contains(item))
                    {
                        NHANVIENs.Add(item);
                    }

                }
                else if (item.NgayTao.ToString("dd/mm/yyyy").ToLower().Contains(value.ToLower()))
                {
                    if (!NHANVIENs.Contains(item))
                    {
                        NHANVIENs.Add(item);
                    }

                }
            }
            if (NHANVIENs != null)
                return NHANVIENs;
            return null;
        }

        public static BindingList<PHONG> timKiemPhong(string value)
        {
            BindingList<PHONG> NHANVIENs = new BindingList<PHONG>();
            BindingList <PHONG> temp = new BindingList<PHONG>(DataProvider.ISCreated.DB.PHONGs.Where(x => (x.TinhTrangTonTai == false)).ToArray());
            foreach (var item in temp)
            {
                if (item.PhongID.ToLower().Contains(value.ToLower()))
                {
                    if (!NHANVIENs.Contains(item))
                    {
                        NHANVIENs.Add(item);
                    }
                }
                else if (item.LOAIPHONG.LoaiPhongID.ToString().ToLower().Contains(value.ToLower()))
                {
                    if (!NHANVIENs.Contains(item))
                    {
                        NHANVIENs.Add(item);
                    }

                }
                else if (item.LOAIPHONG.TenLoai.ToString().ToLower().Contains(value.ToLower()))
                {
                    if (!NHANVIENs.Contains(item))
                    {
                        NHANVIENs.Add(item);
                    }

                }
                else if (item.TenPhong.ToString().ToLower().Contains(value.ToLower()))
                {
                    if (!NHANVIENs.Contains(item))
                    {
                        NHANVIENs.Add(item);
                    }

                }
                else if (item.TinhTrangThue.ToString().ToLower().Contains(value.ToLower()))
                {
                    if (!NHANVIENs.Contains(item))
                    {
                        NHANVIENs.Add(item);
                    }

                }
                else if (item.NgayTao.ToString("dd/mm/yyyy").ToLower().Contains(value.ToLower()))
                {
                    if (!NHANVIENs.Contains(item))
                    {
                        NHANVIENs.Add(item);
                    }

                }
            }
            if (NHANVIENs != null)
                return NHANVIENs;
            return null;
        }

        // END
    }
}
