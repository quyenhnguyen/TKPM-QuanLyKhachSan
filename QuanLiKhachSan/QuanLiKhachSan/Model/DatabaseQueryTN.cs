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
        // DANHSACH
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
        public static List<LOAIDV> danhsachLoaDV()
        {
            return DataProvider.ISCreated.DB.LOAIDVs.Where(x => (x.TinhTrang == false)).ToList();
        }
        public static List<DICHVU> danhSachDivhVu()
        {
            return DataProvider.ISCreated.DB.DICHVUs.Where(x => (x.TinhTrangTonTai == false)).ToList();
        }
        public static List<BANGTHAMSO> danhSachThamSo()
        {
            return DataProvider.ISCreated.DB.BANGTHAMSOes.Where(x => (x.TinhTrang == false)).ToList();
        }

        // THEMMOI
        public static PHONG themMoiPhong(PHONG nv)
        {
            nv.NgayTao = DateTime.Now;
            PHONG res = new PHONG();
            res = DataProvider.ISCreated.DB.PHONGs.Add(nv);
            DataProvider.ISCreated.DB.SaveChanges();
            return res;
        }
        public static DICHVU themMoiDichVu(DICHVU nv)
        {
            nv.NgayTao = DateTime.Now;
            DICHVU res = new DICHVU();
            res = DataProvider.ISCreated.DB.DICHVUs.Add(nv);
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
        public static LOAIDV themMoiLoaiDV(LOAIDV nv)
        {
            nv.NgayTao = DateTime.Now;
            LOAIDV res = new LOAIDV();
            res = DataProvider.ISCreated.DB.LOAIDVs.Add(nv);
            DataProvider.ISCreated.DB.SaveChanges();
            return res;
        }
        public static BANGTHAMSO themMoiThamSo(BANGTHAMSO nv)
        {
            nv.NgayTao = DateTime.Now;
            BANGTHAMSO res = new BANGTHAMSO();
            res = DataProvider.ISCreated.DB.BANGTHAMSOes.Add(nv);
            DataProvider.ISCreated.DB.SaveChanges();
            return res;
        }
        public static NHANVIEN themNhanVienMoi(NHANVIEN nv)
        {
            nv.NgayTao = DateTime.Now;
            NHANVIEN res = new NHANVIEN();
            res = DataProvider.ISCreated.DB.NHANVIENs.Add(nv);
            DataProvider.ISCreated.DB.SaveChanges();
            return res;
        }
        // CAPNHAT
        public static void capNhatLoaiPhong(LOAIPHONG nv)
        {
            LOAIPHONG old = DataProvider.ISCreated.DB.LOAIPHONGs.Where(x => x.LoaiPhongID == nv.LoaiPhongID).SingleOrDefault();
            old.TenLoai = nv.TenLoai;
            old.TinhTrang = nv.TinhTrang;
            old.LoaiPhongID = nv.LoaiPhongID;
            capNhatCSDL();
        }
        public static void capNhatLoaiDV(LOAIDV nv)
        {
            LOAIDV old = DataProvider.ISCreated.DB.LOAIDVs.Where(x => x.LoaiDVID == nv.LoaiDVID).SingleOrDefault();
            old.TinhTrang = nv.TinhTrang;
            old.TenLoai = nv.TenLoai;
            capNhatCSDL();
        }
        public static void capNhatDV(DICHVU nv)
        {
            DICHVU old = DataProvider.ISCreated.DB.DICHVUs.Where(x => x.DichVuID == nv.DichVuID).SingleOrDefault();
            old.TinhTrangTonTai = nv.TinhTrangTonTai;
            old.TenDichVu = nv.TenDichVu;
            old.GiaBan = nv.GiaBan;
            old.GiaCungCap = nv.GiaCungCap;
            old.LoaiDVID = nv.LoaiDVID;
            old.DonVi = nv.DonVi;
            old.HinhAnh = nv.HinhAnh;
            old.NgayTao = nv.NgayTao;
            old.LOAIDV = nv.LOAIDV;
            capNhatCSDL();
        }
        public static void capNhatDVImport(DICHVU nv)
        {
            DICHVU old = DataProvider.ISCreated.DB.DICHVUs.Where(x => x.DichVuID == nv.DichVuID).SingleOrDefault();
            old.TinhTrangTonTai = nv.TinhTrangTonTai;
            old.TenDichVu = nv.TenDichVu;
            old.GiaBan = nv.GiaBan;
            old.GiaCungCap = nv.GiaCungCap;
            old.LoaiDVID = nv.LoaiDVID;
            old.DonVi = nv.DonVi;
            old.HinhAnh = nv.HinhAnh;
            old.NgayTao = nv.NgayTao;
            capNhatCSDL();
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
            old.TinhTrang = nv.TinhTrang;
            capNhatCSDL();
        }

        public static void capNhatNhanVienByUsername(NHANVIEN nv)
        {
            //tuwf tuwf
            NHANVIEN old = DataProvider.ISCreated.DB.NHANVIENs.Where(x => x.TenDangNhap == nv.TenDangNhap || x.Email == nv.Email).SingleOrDefault();
            old.TenDangNhap = nv.TenDangNhap;
            old.HoTen = nv.HoTen;
            old.DiaChi = nv.DiaChi;
            old.NgaySinh = nv.NgaySinh;
            old.SDT = nv.SDT;
            old.CMND = nv.CMND;
            old.Loai = nv.Loai;
            old.Email = nv.Email;
            old.AnhDaiDien = nv.AnhDaiDien;
            old.TinhTrang = nv.TinhTrang;
            capNhatCSDL();
        }
        public static void capNhatPhong(PHONG nv)
        {
            PHONG old = DataProvider.ISCreated.DB.PHONGs.Where(x => x.PhongID == nv.PhongID).SingleOrDefault();
            old.TenPhong = nv.TenPhong;
            old.DonGia = nv.DonGia;
            old.LoaiPhongID = nv.LoaiPhongID;
            old.TinhTrangTonTai = false;
            old.TinhTrangThue = true; // false --> chua thue
            try
            {
                old.TinhTrangTonTai = nv.TinhTrangTonTai;
                old.TinhTrangThue = nv.TinhTrangThue;
            }
            catch (Exception)
            {

            }
            capNhatCSDL();
        }
        public static void capNhatThamSo(BANGTHAMSO nv)
        {
            BANGTHAMSO old = DataProvider.ISCreated.DB.BANGTHAMSOes.Where(x => x.MaThamSo == nv.MaThamSo).SingleOrDefault();
            old.GiaTri = nv.GiaTri;
            old.KieuDuLieu = nv.KieuDuLieu;
            old.NgayTao = nv.NgayTao;
            old.TenThamSo = nv.TenThamSo;
            capNhatCSDL();
        }
        public static void capNhatAvatar(NHANVIEN nv)
        {
            NHANVIEN old = DataProvider.ISCreated.DB.NHANVIENs.Where(x => x.NhanVienID == nv.NhanVienID).SingleOrDefault();
            old.AnhDaiDien = nv.AnhDaiDien;
            capNhatCSDL();
        }
        // KIEM TRA TON TAI
        public static List<NHANVIEN> thongtinDangNhap(string user, string pass)
        {
            List<NHANVIEN> res = new List<NHANVIEN>();
            res = DataProvider.ISCreated.DB.NHANVIENs.Where(x => (x.TenDangNhap == user || x.Email == user) && x.MatKhau == pass && x.TinhTrang == false).ToList();
            //res = DataProvider.ISCreated.DB.Database.SqlQuery<NHANVIEN>("SELECT * FROM NHANVIEN  WHERE Phong = @id and ThoiGianTra IS NULL", new SqlParameter("@id", phongID)).FirstOrDefault();
            return res;
        }
        public static bool kiemtraTonTai(string user, string email)
        {
            int res = DataProvider.ISCreated.DB.NHANVIENs.Where(x => (x.TenDangNhap == user || x.Email == email)).ToList().Count;
            if (res > 0) return true;
            return false;
        }
        public static bool checkDuplicate(int ID, string user, string email)
        {
            int res = DataProvider.ISCreated.DB.NHANVIENs.Where(x => (x.NhanVienID == ID && x.TenDangNhap == user && x.Email == email)).ToList().Count;
            if (res > 0) return true;
            return false;
        }
        public static bool kiemtraTonTaiImport(int NVID)
        {
            int res = DataProvider.ISCreated.DB.NHANVIENs.Where(x => (x.NhanVienID == NVID)).ToList().Count;
            if (res > 0) return true;
            return false;
        }
        public static bool kiemTraTonTaiLoaiPhong(string loaiPhongID)
        {
            int res = DataProvider.ISCreated.DB.LOAIPHONGs.Where(x => (x.LoaiPhongID == loaiPhongID)).ToList().Count;
            if (res > 0) return true;
            return false;
        }
        public static bool kiemTraTonTaiLoaiPhongImport(string loaiPhongID)
        {
            int res = DataProvider.ISCreated.DB.LOAIPHONGs.Where(x => (x.LoaiPhongID == loaiPhongID && x.TinhTrang == false)).ToList().Count;
            if (res > 0) return true;
            return false;
        }
        public static bool kiemTraTonTaiLoaiDV(string loaiPhongID)
        {
            int res = DataProvider.ISCreated.DB.LOAIDVs.Where(x => (x.LoaiDVID == loaiPhongID)).ToList().Count;
            if (res > 0) return true;
            return false;
        }
        public static bool kiemTraTonTaiPhong(string phongID)
        {
            int res = DataProvider.ISCreated.DB.PHONGs.Where(x => (x.PhongID == phongID)).ToList().Count;
            if (res > 0) return true;
            return false;
        }
        public static bool kiemTraTonDichVu(string phongID)
        {
            int res = DataProvider.ISCreated.DB.DICHVUs.Where(x => (x.DichVuID == phongID)).ToList().Count;
            if (res > 0) return true;
            return false;
        }
        public static bool kiemTraTonTaiThamSo(string phongID)
        {
            int res = DataProvider.ISCreated.DB.BANGTHAMSOes.Where(x => (x.MaThamSo == phongID)).ToList().Count;
            if (res > 0) return true;
            return false;
        }

        // USED
        public static bool isUsedLoaiPhong(string lp)
        {
            int res = DataProvider.ISCreated.DB.PHONGs.Where(x => (x.LoaiPhongID == lp && x.TinhTrangThue == true)).ToList().Count;
            if (res > 0) return true;
            return false;
        }
        public static bool isUsedPhong(string lp)
        {
            int res = DataProvider.ISCreated.DB.PHONGs.Where(x => (x.PhongID == lp && x.TinhTrangThue == true)).ToList().Count;
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
        public static bool isDeleteUser(string userName, string email)
        {
            bool res = DataProvider.ISCreated.DB.Database.SqlQuery<bool>("SELECT TinhTrang FROM NHANVIEN WHERE TenDangNhap = @user or Email = @email", new SqlParameter("@user", userName), new SqlParameter("@email", email)).FirstOrDefault();
            return res;
        }
        public static bool isDeleteService(string userName)
        {
            bool res = DataProvider.ISCreated.DB.Database.SqlQuery<bool>("SELECT TinhTrangTonTai FROM DICHVU WHERE TenDangNhap = @DichVuID", new SqlParameter("@user", userName)).FirstOrDefault();
            return res;
        }
        public static bool isDeleteServiceType(string userName)
        {
            bool res = DataProvider.ISCreated.DB.Database.SqlQuery<bool>("SELECT TinhTrang FROM LOAIDV WHERE LoaiDVID = @user", new SqlParameter("@user", userName)).FirstOrDefault();
            return res;
        }

        //


        // XOA
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

            List<PHONG> listPhong = DataProvider.ISCreated.DB.PHONGs.Where(x => (x.LoaiPhongID == old.LoaiPhongID && x.TinhTrangTonTai == false && x.TinhTrangThue == false)).ToList();
            foreach (PHONG item in listPhong)
            {
                item.TinhTrangTonTai = true;
            }

            capNhatCSDL();
        }
        public static void xoaLoaiDV(LOAIDV nv)
        {
            LOAIDV old = DataProvider.ISCreated.DB.LOAIDVs.Where(x => x.LoaiDVID == nv.LoaiDVID).SingleOrDefault();
            old.TinhTrang = true;

            List<DICHVU> listDV = DataProvider.ISCreated.DB.DICHVUs.Where(x => (x.LoaiDVID == old.LoaiDVID && x.TinhTrangTonTai == false)).ToList();
            foreach (DICHVU item in listDV)
            {
                item.TinhTrangTonTai = true;
            }
            capNhatCSDL();
        }
        public static void xoaDV(DICHVU nv)
        {
            DICHVU old = DataProvider.ISCreated.DB.DICHVUs.Where(x => x.DichVuID == nv.DichVuID).SingleOrDefault();
            old.TinhTrangTonTai = true;
            capNhatCSDL();
        }
        public static void xoaPhong(PHONG nv)
        {
            PHONG old = DataProvider.ISCreated.DB.PHONGs.Where(x => x.PhongID == nv.PhongID).SingleOrDefault();
            old.TinhTrangTonTai = true;
            capNhatCSDL();
        }
        public static void xoaThamSo(BANGTHAMSO nv)
        {
            BANGTHAMSO old = DataProvider.ISCreated.DB.BANGTHAMSOes.Where(x => x.TenThamSo == nv.TenThamSo).SingleOrDefault();
            old.TinhTrang = true;
            capNhatCSDL();
        }



        // TIM KIEM
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
                else if (item.LOAINHANVIEN.TenLoai.ToLower().Contains(value.ToLower()))
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
            BindingList<PHONG> temp = new BindingList<PHONG>(DataProvider.ISCreated.DB.PHONGs.Where(x => (x.TinhTrangTonTai == false)).ToArray());
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
        public static BindingList<LOAIDV> timKiemLoaiDV(string value)
        {
            BindingList<LOAIDV> NHANVIENs = new BindingList<LOAIDV>();
            BindingList<LOAIDV> temp = new BindingList<LOAIDV>(DataProvider.ISCreated.DB.LOAIDVs.Where(x => (x.TinhTrang == false)).ToArray());
            foreach (var item in temp)
            {
                if (item.LoaiDVID.ToLower().Contains(value.ToLower()))
                {
                    if (!NHANVIENs.Contains(item))
                    {
                        NHANVIENs.Add(item);
                    }
                }

                if (item.TenLoai.ToLower().Contains(value.ToLower()))
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
        public static BindingList<DICHVU> timKiemDV(string value)
        {
            BindingList<DICHVU> NHANVIENs = new BindingList<DICHVU>();
            BindingList<DICHVU> temp = new BindingList<DICHVU>(DataProvider.ISCreated.DB.DICHVUs.Where(x => (x.TinhTrangTonTai == false)).ToArray());
            foreach (var item in temp)
            {
                if (item.DichVuID.ToLower().Contains(value.ToLower()))
                {
                    if (!NHANVIENs.Contains(item))
                    {
                        NHANVIENs.Add(item);
                    }
                }
                if (item.LoaiDVID.ToLower().Contains(value.ToLower()))
                {
                    if (!NHANVIENs.Contains(item))
                    {
                        NHANVIENs.Add(item);
                    }
                }

                if (item.TenDichVu.ToLower().Contains(value.ToLower()))
                {
                    if (!NHANVIENs.Contains(item))
                    {
                        NHANVIENs.Add(item);
                    }
                }
                if (item.GiaCungCap.ToString().ToLower().Contains(value.ToLower()))
                {
                    if (!NHANVIENs.Contains(item))
                    {
                        NHANVIENs.Add(item);
                    }
                }
                if (item.GiaBan.ToString().ToLower().Contains(value.ToLower()))
                {
                    if (!NHANVIENs.Contains(item))
                    {
                        NHANVIENs.Add(item);
                    }
                }
                if (item.LOAIDV.TenLoai.ToLower().Contains(value.ToLower()))
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
        public static BindingList<BANGTHAMSO> timKiemThamSo(string value)
        {
            BindingList<BANGTHAMSO> NHANVIENs = new BindingList<BANGTHAMSO>();
            BindingList<BANGTHAMSO> temp = new BindingList<BANGTHAMSO>(DataProvider.ISCreated.DB.BANGTHAMSOes.Where(x => (x.TinhTrang == false)).ToArray());
            foreach (var item in temp)
            {
                if (item.TenThamSo.ToString().ToLower().Contains(value.ToLower()))
                {
                    if (!NHANVIENs.Contains(item))
                    {
                        NHANVIENs.Add(item);
                    }
                }
                if (item.GiaTri.ToString().ToLower().Contains(value.ToLower()))
                {
                    if (!NHANVIENs.Contains(item))
                    {
                        NHANVIENs.Add(item);
                    }
                }
                if (item.MaThamSo.ToLower().Contains(value.ToLower()))
                {
                    if (!NHANVIENs.Contains(item))
                    {
                        NHANVIENs.Add(item);
                    }
                }
                if (item.KieuDuLieu.ToLower().Contains(value.ToLower()))
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
        // LAY THONG TIN
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


        // Excell Area
        public static List<LOAIDV> danhsachAllLoaDV()
        {
            return DataProvider.ISCreated.DB.LOAIDVs.ToList();
        }
        public static List<DICHVU> danhsachAllDV()
        {
            return DataProvider.ISCreated.DB.DICHVUs.ToList();
        }
        public static List<NHANVIEN> danhsachAllNhanVien()
        {
            return DataProvider.ISCreated.DB.NHANVIENs.ToList();
        }
        public static List<LOAIPHONG> danhsachAllLoaiPhong()
        {
            return DataProvider.ISCreated.DB.LOAIPHONGs.ToList();
        }
        public static List<PHONG> danhsachAllPhong()
        {
            return DataProvider.ISCreated.DB.PHONGs.ToList();
        }
        public static List<DSDANGKYTB> danhSachDangKyThongBao()
        {
            return DataProvider.ISCreated.DB.DSDANGKYTBs.ToList();
        }
        public static string[] getColumnName(string name)
        {
            if (name == "DICHVU")
            {
                return typeof(DICHVU).GetProperties().Select(property => property.Name).ToArray();
            }
            else if (name == "LOAIDV")
            {
                return typeof(LOAIDV).GetProperties().Select(property => property.Name).ToArray();
            }
            else if (name == "NHANVIEN")
            {
                return typeof(NHANVIEN).GetProperties().Select(property => property.Name).ToArray();
            }
            else if (name == "PHONG")
            {
                return typeof(PHONG).GetProperties().Select(property => property.Name).ToArray();
            }
            else if (name == "LOAIPHONG")
            {
                return typeof(LOAIPHONG).GetProperties().Select(property => property.Name).ToArray();
            }
            else if (name == "THAMSO")
            {
                return typeof(BANGTHAMSO).GetProperties().Select(property => property.Name).ToArray();
            }

            else
            {
                return null;
            }
        }
        public static void updateInsertLoaiPhong(LOAIPHONG lp)
        {
            List<PHONG> listPhong = DataProvider.ISCreated.DB.PHONGs.Where(x => (x.LoaiPhongID == lp.LoaiPhongID && x.TinhTrangTonTai == false)).ToList();
            foreach (PHONG item in listPhong)
            {
                item.TinhTrangTonTai = true;
            }
            capNhatCSDL();
        }
        public static void updateInsertLoaiDV(LOAIDV lp)
        {
            List<DICHVU> listPhong = DataProvider.ISCreated.DB.DICHVUs.Where(x => (x.LoaiDVID == lp.LoaiDVID && x.TinhTrangTonTai == false)).ToList();
            foreach (DICHVU item in listPhong)
            {
                item.TinhTrangTonTai = true;
            }
            capNhatCSDL();
        }


        // END
        public static DSDANGKYTB themNguoiDangKi(DSDANGKYTB nv)
        {
            DSDANGKYTB res = new DSDANGKYTB();
            res = DataProvider.ISCreated.DB.DSDANGKYTBs.Add(nv);
            DataProvider.ISCreated.DB.SaveChanges();
            return res;
        }
        public static string tenLoaiThongBao(int loaiThongBao)
        {
            LOAITB res = new LOAITB();
            res = DataProvider.ISCreated.DB.LOAITBs.Find(loaiThongBao);
            if (res != null)
            {
                return res.LOAITB1;
            }
            return "";
        }
    }
}

