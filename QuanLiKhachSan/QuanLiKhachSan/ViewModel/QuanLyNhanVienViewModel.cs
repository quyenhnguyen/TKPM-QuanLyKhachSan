using Microsoft.Win32;
using QuanLiKhachSan.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace QuanLiKhachSan.ViewModel
{
    public class QuanLyNhanVienViewModel : BaseViewModel
    {
        private bool isAddUser = true;
        public ICommand timNhanVienCommand { get; set; }
        public ICommand confirmButtonCommmand { get; set; }
        public ICommand cancelButtonCommmand { get; set; }
        public ICommand addNhanVienCommand { get; set; }
        public ICommand ChonAnhChiTietNhanVienCommand { get; set; }
        BitmapImage temp;

        private string _queryString = "";
        public string queryString
        {
            get => _queryString;
            set
            {
                _queryString = value;
                OnPropertyChanged();
            }
        }
        private string _cancelButtonName = "HỦY";
        public string cancelButtonName
        {
            get => _cancelButtonName;
            set
            {
                _cancelButtonName = value;
                OnPropertyChanged();
            }
        }
        private string _confirmButtonName = "THÊM";
        public string confirmButtonName
        {
            get => _confirmButtonName;
            set
            {
                _confirmButtonName = value;
                OnPropertyChanged();
            }
        }

        private BindingList<string> _listChucVu;
        public BindingList<string> listChucVu
        {
            get
            {
                return _listChucVu;
            }
            set
            {
                _listChucVu = value;
                OnPropertyChanged();
            }
        }

        private BindingList<NHANVIEN> _listNhanVien;
        public BindingList<NHANVIEN> listNhanVien
        {
            get
            {
                return _listNhanVien;
            }
            set
            {
                _listNhanVien = value;
                OnPropertyChanged();
            }
        }
        public ImageSource MyPhoto { get; set; }

        private BitmapImage _HinhAnhNhanVien;
        public BitmapImage HinhAnhNhanVien
        {
            get { return _HinhAnhNhanVien; }
            set { _HinhAnhNhanVien = value; OnPropertyChanged(); }
        }
        private NHANVIEN item { get; set; }
        public NHANVIEN selectItem
        {
            get { return item; }
            set
            {
                if (item != value)
                {
                    item = value;

                    showDetails();
                }
            }
        }

        private int _NhanVienID;
        public int txtNhanVienID { get => _NhanVienID; set => OnPropertyChanged(ref _NhanVienID, value); }

        private string _HoTen;
        public string txtHoTen { get => _HoTen; set => OnPropertyChanged(ref _HoTen, value); }

        private string _TenDangNhap;
        public string txtTenDangNhap { get => _TenDangNhap; set => OnPropertyChanged(ref _TenDangNhap, value); }

        private DateTime _NgaySinh;
        public DateTime txtNgaySinh { get => _NgaySinh; set => OnPropertyChanged(ref _NgaySinh, value); }

        private DateTime _NgayTao;
        public DateTime txtNgayTao { get => _NgayTao; set => OnPropertyChanged(ref _NgayTao, value); }

        private string _CMND;
        public string txtCMND { get => _CMND; set => OnPropertyChanged(ref _CMND, value); }

        private string _DiaChi;
        public string txtDiaChi { get => _DiaChi; set => OnPropertyChanged(ref _DiaChi, value); }

        private string _Email;
        public string txtEmail { get => _Email; set => OnPropertyChanged(ref _Email, value); }

        private string _SDT;
        public string txtSDT { get => _SDT; set => OnPropertyChanged(ref _SDT, value); }

        private string _Loai;
        public string txtLoai { get => _Loai; set => OnPropertyChanged(ref _Loai, value); }

        public void showDetails()
        {

            if (selectItem != null)
            {
                txtNhanVienID = selectItem.NhanVienID;
                txtHoTen = selectItem.HoTen;
                txtTenDangNhap = selectItem.TenDangNhap;
                txtNgaySinh = selectItem.NgaySinh;
                txtNgayTao = selectItem.NgayTao;
                txtCMND = selectItem.CMND.ToString();
                txtDiaChi = selectItem.DiaChi;
                txtEmail = selectItem.Email;
                txtEmail = selectItem.Email;
                if (selectItem.Loai == 1)
                {
                    txtLoai = "Quản Lý";
                }
                else if (selectItem.Loai == 2)
                {
                    txtLoai = "Kế Toán";
                }
                else
                {
                    txtLoai = "Lễ Tân";
                }
                HinhAnhNhanVien = SecurityModel.LoadImage(selectItem.AnhDaiDien);
                txtSDT = selectItem.SDT.ToString();//hiện ddcuo r á, CHITIETNHANVIEN chua khai bao kai
                //HinhAnhNhanVien = SeviceData.LoadImage(selectItem.HINHANH);
                isAddUser = false;
                cancelButtonName = "XÓA";
                confirmButtonName = "LƯU";
            }
            else
            {
                isAddUser = true;
                cancelButtonName = "HỦY";
                confirmButtonName = "THÊM";
            }
            // DisplayedImagePath = selectItem.HINHANH;
        }
        public QuanLyNhanVienViewModel()
        {
            listNhanVien = new BindingList<NHANVIEN>(DatabaseQuery.danhSachNhanVien());
            listChucVu = new BindingList<string>(new List<string> { "Kế Toán", "Lễ Tân", "Quản Lý" });
            txtLoai = listChucVu[0];
            ChonAnhChiTietNhanVienCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    Uri fileUri = new Uri(openFileDialog.FileName);
                    HinhAnhNhanVien = new BitmapImage(new Uri(openFileDialog.FileName));
                }
            }
            );
            addNhanVienCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                isAddUser = true;
                cancelButtonName = "HỦY";
                confirmButtonName = "THÊM";
                DatabaseQuery.capNhatCSDL();
            });
            // Huỷ Thêm hoặc Xoá
            cancelButtonCommmand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                if (isAddUser)
                {
                    try
                    {
                        selectItem = listNhanVien[0];
                        isAddUser = false;
                        cancelButtonName = "XOÁ";
                        confirmButtonName = "LƯU";
                        DatabaseQuery.capNhatCSDL();
                    }
                    catch (Exception) { };
                    return;
                }
                else
                {
                    try
                    {
                        DatabaseQueryTN.xoaNhanVien(selectItem);
                        MessageBox.Show("Đã xoá nhân viên này");
                        listNhanVien = new BindingList<NHANVIEN>(DatabaseQuery.danhSachNhanVien());

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Không thể xoá nhân viên này");
                    }
                }
                //hmm, 
            });
            // Thêm Mới hoặc Cập Nhật
            confirmButtonCommmand = new RelayCommand<Object>((p) =>
            {
                if (string.IsNullOrEmpty(txtLoai.ToString()) ||
                string.IsNullOrEmpty(txtNgaySinh.ToString("dd/mm/yyyy")) ||
                string.IsNullOrEmpty(txtDiaChi) ||
                string.IsNullOrEmpty(txtTenDangNhap.ToString()) ||
                string.IsNullOrEmpty(txtSDT.ToString()) ||
                string.IsNullOrEmpty(txtCMND.ToString()) ||
                string.IsNullOrEmpty(txtEmail.ToString()) ||
                string.IsNullOrEmpty(txtNhanVienID.ToString()) ||
                string.IsNullOrEmpty(txtNgayTao.ToString()) ||
                string.IsNullOrEmpty(txtHoTen.ToString()))
                    return false;
                return true;
            }, (p) =>
            {
                NHANVIEN newNV = new NHANVIEN();
                newNV.NhanVienID = txtNhanVienID;
                newNV.TenDangNhap = txtTenDangNhap;
                newNV.Email = txtEmail;
                newNV.TinhTrang = false;
                newNV.HoTen = txtHoTen;
                newNV.DiaChi = txtDiaChi;
                newNV.NgaySinh = txtNgaySinh;
                newNV.SDT = int.Parse(txtSDT);
                newNV.CMND = int.Parse(txtCMND);
                newNV.AnhDaiDien = SecurityModel.ImageToByte2(HinhAnhNhanVien);
                if (txtLoai == "Quản Lý") newNV.Loai = 1;
                else if (txtLoai == "Kế Toán") newNV.Loai = 2;
                else newNV.Loai = 3;
                // Neu dang o che do them User
                if (isAddUser)
                {
                    if (DatabaseQueryTN.kiemtraTonTai(txtTenDangNhap, txtEmail))
                    {
                        if (!DatabaseQueryTN.isDeleteUser(txtTenDangNhap, txtEmail)) // true --> delete
                        {
                            MessageBox.Show("Tài khoản đã tồn tại");
                            return;
                        }
                    }
                    newNV.MatKhau = SecurityModel.Encrypt(newNV.CMND.ToString());
                    //Hinh anh
                    try
                    {
                        DatabaseQueryTN.themNhanVienMoi(newNV);
                        MessageBox.Show("Thêm mới nhân viên thành công. Mật khẩu mặc định là CMND");
                        listNhanVien = new BindingList<NHANVIEN>(DatabaseQuery.danhSachNhanVien());
                    }
                    catch
                    {
                        MessageBox.Show("Nhân viên đã tồn tại");
                    }
                }
                else // Cap Nhat
                {
                    try
                    {
                        DatabaseQueryTN.capNhatNhanVien(newNV);
                        MessageBox.Show("Đã cập nhật nhân viên");
                        listNhanVien = new BindingList<NHANVIEN>(DatabaseQuery.danhSachNhanVien());

                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Không thể cập nhật nhân viên." + e.ToString());
                    }
                    //showDetails();
                }
            });

        }
    }
}
