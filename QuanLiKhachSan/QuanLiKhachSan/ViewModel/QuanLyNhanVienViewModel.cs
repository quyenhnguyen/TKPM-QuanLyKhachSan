﻿using Microsoft.Win32;
using QuanLiKhachSan.Model;
using QuanLiKhachSan.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

        public ICommand exportNhanVienCommand { get; set; }
        public ICommand importNhanVienCommand { get; set; }
        public ICommand timNhanVienCommand { get; set; }
        public ICommand confirmButtonCommmand { get; set; }
        public ICommand cancelButtonCommmand { get; set; }
        public ICommand addNhanVienCommand { get; set; }
        public ICommand ChonAnhChiTietNhanVienCommand { get; set; }
        private string _timNhanVienInput = "";
        public string timNhanVienInput
        {
            get => _timNhanVienInput;
            set
            {
                _timNhanVienInput = value;
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
        private string _currentLNV = "Loại Nhân Viên";
        public string currentLoaiNV
        {
            get => _currentLNV;
            set
            {
                _currentLNV = value;
                OnPropertyChanged();
            }
        }
        private BindingList<LOAINHANVIEN> _listLoaiNV;
        public BindingList<LOAINHANVIEN> listLoaiNV
        {
            get
            {
                return _listLoaiNV;
            }
            set
            {
                _listLoaiNV = value;
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

                    showDetails(); // Hiển thị thông tin lên ScrollView
                }
            }
        }


        private LOAINHANVIEN itemLoaiNV { get; set; }
        public LOAINHANVIEN selecteLoaiNV
        {
            get { return itemLoaiNV; }
            set
            {
                if (itemLoaiNV != value)
                {
                    itemLoaiNV = value;

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
        public void reset()
        {
            isAddUser = true;
            cancelButtonName = "HỦY";
            confirmButtonName = "THÊM";
            currentLoaiNV = "Loại nhân viên";
            txtNhanVienID = 0;
            txtHoTen = null;
            txtSDT = null;
            txtTenDangNhap = null;
            txtNgaySinh = DateTime.MinValue;
            txtCMND = null;
            txtDiaChi = null;
            txtEmail = null;
            HinhAnhNhanVien = null;
        }
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
                currentLoaiNV = selectItem.LOAINHANVIEN.TenLoai;
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
                txtSDT = selectItem.SDT.ToString();
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
        }
        private bool checkCondition()
        {
            try
            {
                return (string.IsNullOrEmpty(txtLoai) ||
                string.IsNullOrEmpty(txtNgaySinh.ToString("dd/mm/yyyy")) ||
                string.IsNullOrEmpty(txtDiaChi) ||
                string.IsNullOrEmpty(txtTenDangNhap) ||
                string.IsNullOrEmpty(txtSDT) ||
                string.IsNullOrEmpty(txtCMND) ||
                string.IsNullOrEmpty(txtEmail) ||
                string.IsNullOrEmpty(txtNhanVienID.ToString()) ||
                string.IsNullOrEmpty(txtNgayTao.ToString()) ||
                string.IsNullOrEmpty(txtHoTen));
            }
            catch (Exception)
            {
                return true;
            }

        }
        public QuanLyNhanVienViewModel()
        {
            listNhanVien = new BindingList<NHANVIEN>(DatabaseQuery.danhSachNhanVien());
            listLoaiNV = new BindingList<LOAINHANVIEN>(DatabaseQueryTN.danhSachLoaiNV());
            exportNhanVienCommand = new RelayCommand<object>((P) => { return true; }, (p) =>
            {
                ConcreteModelFactory ModelFactory = new ConcreteModelFactory();
                Window w = ((Window)p);
                SaveFileDialog openFileDialog = new SaveFileDialog();
                string name = "";
                openFileDialog.Filter = "Excel Files|*.xlsx;*.xlsm;*.xlsb;*.xls";
                if (openFileDialog.ShowDialog() == true)
                {
                    name = openFileDialog.FileName;
                }
                else
                {
                    return;
                }
                ManHinhLoading wd = new ManHinhLoading();
                w.Dispatcher.Invoke(() =>
                {
                    wd.Show();
                });
                IModelName modelName = ModelFactory.Factory("NHANVIEN", name);
                Thread.Sleep(200);
                Thread newWindowThread = new Thread(() =>
                {
                    modelName.exportExcel();
                    w.Dispatcher.Invoke(() =>
                    {
                        wd.Close();
                    });
                });
                newWindowThread.Start();

            });
            importNhanVienCommand = new RelayCommand<Object>((P) => { return true; }, (p) =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Excel Files|*.xlsx;*.xlsm;*.xlsb;*.xls";
                string name = "";
                if (openFileDialog.ShowDialog() == true)
                {
                    name = openFileDialog.FileName;
                }
                else
                {
                    return;
                }
                Window w = ((Window)p);
                ManHinhLoading wd = new ManHinhLoading();
                w.Dispatcher.Invoke(() =>
                {
                    wd.Show();
                });
                ConcreteModelFactory ModelFactory = new ConcreteModelFactory();
                IModelName modelName = ModelFactory.Factory("NHANVIEN", name);
                Thread newWindowThread = new Thread(() =>
                {
                    w.Dispatcher.Invoke(() =>
                    {
                        modelName.importExcel();
                    });
                    listNhanVien = new BindingList<NHANVIEN>(DatabaseQueryTN.danhSachNhanVien());
                    w.Dispatcher.Invoke(() =>
                    {
                        wd.Close();
                    });
                });
                newWindowThread.Start();

            });
            timNhanVienCommand = new RelayCommand<Object>((P) => { return true; }, (p) =>
{
    listNhanVien = new BindingList<NHANVIEN>(DatabaseQueryTN.timKiemNhanVien(timNhanVienInput));
});
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
                currentLoaiNV = "Loại nhân viên";
                reset();
                DatabaseQuery.capNhatCSDL();
            });
            // Huỷ Thêm hoặc Xoá
            cancelButtonCommmand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                if (isAddUser)
                {
                    try
                    {
                        reset();
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
                        reset();
                    }
                    catch (Exception e)
                    {

                        SecurityModel.Log(e.ToString());
                        MessageBox.Show("Không thể xoá nhân viên này");
                    }
                }
                //hmm, 
            });
            // Thêm Mới hoặc Cập Nhật
            confirmButtonCommmand = new RelayCommand<Object>((p) =>
            {
                //if (checkCondition())
                //    return false;
                return true;
            }, (p) =>
            {
                if (checkCondition()) { MessageBox.Show("Điền đầy đủ thông tin"); return; }
                NHANVIEN newNV = new NHANVIEN();
                try
                {
                    if (isAddUser)
                    {

                        newNV.Loai = selecteLoaiNV.LoaiNVID;
                        newNV.LOAINHANVIEN = selecteLoaiNV;
                        txtLoai = selecteLoaiNV.LoaiNVID.ToString();
                    }
                    else
                    {

                        newNV.Loai = selectItem.Loai;
                        newNV.LOAINHANVIEN = selectItem.LOAINHANVIEN;
                    }
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
                }
                catch (Exception e)
                {
                    MessageBox.Show("Thông báo :" + e.ToString());
                    return;
                }
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
                    catch (Exception e)
                    {
                        SecurityModel.Log(e.ToString());
                        MessageBox.Show("Nhân viên đã tồn tại");
                    }
                }
                else // Cap Nhat
                {
                    try
                    {
                        newNV.TinhTrang = false;
                        DatabaseQueryTN.capNhatNhanVien(newNV);
                        MessageBox.Show("Đã cập nhật nhân viên");
                        listNhanVien = new BindingList<NHANVIEN>(DatabaseQuery.danhSachNhanVien());

                    }
                    catch (Exception e)
                    {
                        SecurityModel.Log(e.ToString());
                        MessageBox.Show("Không thể cập nhật nhân viên.");
                    }
                    //showDetails();
                }
            });

        }
    }
}
