﻿using QuanLiKhachSan.ViewModel;
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

        public static NHANVIEN themNhanVienMoi(NHANVIEN nv)
        {
            nv.NgayTao = DateTime.Now;
            NHANVIEN res = new NHANVIEN();
            res = DataProvider.ISCreated.DB.NHANVIENs.Add(nv);
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

        public static int layNhanVien(string user)
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
            capNhatCSDL();
        }

        public static void xoaNhanVien(NHANVIEN nv)
        {
            NHANVIEN old = DataProvider.ISCreated.DB.NHANVIENs.Where(x => x.NhanVienID == nv.NhanVienID).SingleOrDefault();
            old.TinhTrang = true;
            capNhatCSDL();
        }
    }
}