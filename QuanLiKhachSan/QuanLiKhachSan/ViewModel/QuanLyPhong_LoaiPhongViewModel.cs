using QuanLiKhachSan.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLiKhachSan.ViewModel
{
    public class QuanLyPhong_LoaiPhongViewModel : BaseViewModel
    {
        // Quan Ly Loai Phong
        private bool isAddRoom = true;
        private bool _choPhepDoi = true;
        public bool choPhepThayDoi { get => _choPhepDoi; set => OnPropertyChanged(ref _choPhepDoi, value); }
        private string _timLoaiInput;
        public string timLoaiPhongInput { get => _timLoaiInput; set => OnPropertyChanged(ref _timLoaiInput, value); }
        public ICommand timLoaiPhongCommand { get; set; }
        public ICommand confirmButtonCommmand { get; set; }
        public ICommand cancelButtonCommmand { get; set; }
        public ICommand addPhongCommand { get; set; }
        private string _cancelButtonName = "HỦY";
        private string _confirmButtonName = "THÊM";
        public string cancelButtonName
        {
            get => _cancelButtonName;
            set
            {
                _cancelButtonName = value;
                OnPropertyChanged();
            }
        }
        public string confirmButtonName
        {
            get => _confirmButtonName;
            set
            {
                _confirmButtonName = value;
                OnPropertyChanged();
            }
        }
        private BindingList<LOAIPHONG> _listLoaiPhong;
        public BindingList<LOAIPHONG> listLoaiPhong
        {
            get
            {
                return _listLoaiPhong;
            }
            set
            {
                _listLoaiPhong = value;
                OnPropertyChanged();
            }
        }
        private string _maLoai;
        public string txtMaLoai { get => _maLoai; set => OnPropertyChanged(ref _maLoai, value); }
        private string _tenLoai;
        public string txtTen { get => _tenLoai; set => OnPropertyChanged(ref _tenLoai, value); }
        private DateTime _NgayTao;
        public DateTime txtNgayTao { get => _NgayTao; set => OnPropertyChanged(ref _NgayTao, value); }
        private LOAIPHONG item { get; set; }
        public LOAIPHONG selectItem
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
        public void showDetails()
        {

            if (selectItem != null)
            {
                choPhepThayDoi = false;
                txtMaLoai = selectItem.LoaiPhongID;
                txtTen = selectItem.TenLoai;
                txtNgayTao = selectItem.NgayTao;
                isAddRoom = false;
                cancelButtonName = "XÓA";
                confirmButtonName = "LƯU";
            }
            else
            {
                isAddRoom = true;
                choPhepThayDoi = true;
                cancelButtonName = "HỦY";
                confirmButtonName = "THÊM";
            }
            // DisplayedImagePath = selectItem.HINHANH;
        }
        private void reset()
        {
            txtTen = string.Empty;
            txtMaLoai = string.Empty;
        }

        // Quan Ly Phong

        private bool isAddnewRoom = true;
        private bool _choPhepUpdateRoom = true;
        public bool choPhepUpdateRoom { get => _choPhepUpdateRoom; set => OnPropertyChanged(ref _choPhepUpdateRoom, value); }
        private string _timPhongInput;
        public string timPhongInput { get => _timPhongInput; set => OnPropertyChanged(ref _timPhongInput, value); }
        public ICommand timPhongCommand { get; set; }
        public ICommand comfirmRoomBtnCommand { get; set; }
        public ICommand cancelRoomBtnCommand { get; set; }
        public ICommand addNewRoomCommand { get; set; }
        private string _roomCancelBtnName = "HỦY";
        private string _roomConfirmBtnName = "THÊM";
        public string roomCancelBtnName
        {
            get => _roomCancelBtnName;
            set
            {
                _roomCancelBtnName = value;
                OnPropertyChanged();
            }
        }
        public string roomConfirmBtnName
        {
            get => _roomConfirmBtnName;
            set
            {
                _roomConfirmBtnName = value;
                OnPropertyChanged();
            }
        }
        private BindingList<PHONG> _listPhong;
        public BindingList<PHONG> listPhong
        {
            get
            {
                return _listPhong;
            }
            set
            {
                _listPhong = value;
                OnPropertyChanged();
            }
        }
        private string _tenPhong;
        public string txtTenPhong { get => _tenPhong; set => OnPropertyChanged(ref _tenPhong, value); }
        private double _donGia;
        public double txtDonGia { get => _donGia; set => OnPropertyChanged(ref _donGia, value); }
        private string _IDphong;
        public string txtPhmgID { get => _IDphong; set => OnPropertyChanged(ref _IDphong, value); }
        private string _tinhTrangThue;
        public string txtTinhTrangThuePhong { get => _tinhTrangThue; set => OnPropertyChanged(ref _tinhTrangThue, value); }
        private DateTime _ngayTaoPhong;
        public DateTime txtNgayTaoPhong { get => _ngayTaoPhong; set => OnPropertyChanged(ref _ngayTaoPhong, value); }
        private PHONG itemPhong { get; set; }
        public PHONG selectPhong
        {
            get { return itemPhong; }
            set
            {
                if (itemPhong != value)
                {
                    itemPhong = value;

                    showDetailsPhong();
                }
            }
        }
        //chonLoaiPhongSelectedItem
        private LOAIPHONG _itemLoaiPhongChon { get; set; }
        public LOAIPHONG itemLoaiPhongChon
        {
            get { return _itemLoaiPhongChon; }
            set
            {
                if (_itemLoaiPhongChon != value)
                {
                    _itemLoaiPhongChon = value;
                }
            }
        }
        public void showDetailsPhong()
        {

            if (selectPhong != null)
            {
                choPhepUpdateRoom = selectPhong.TinhTrangThue;
                isAddnewRoom = false;
                txtTenPhong = selectPhong.TenPhong;
                txtDonGia = selectPhong.DonGia;
                txtNgayTaoPhong = selectPhong.NgayTao;
                txtPhmgID = selectPhong.PhongID;
                if (selectPhong.TinhTrangThue == false)
                {
                    txtTinhTrangThuePhong = "Chưa thuê";
                }
                else
                {
                    txtTinhTrangThuePhong = "Đã thuê";
                }
                roomCancelBtnName = "XÓA";
                roomConfirmBtnName = "LƯU";
            }
            else
            {
                isAddnewRoom = true;
                choPhepUpdateRoom = true;
                roomCancelBtnName = "HỦY";
                roomConfirmBtnName = "THÊM";
            }
            // DisplayedImagePath = selectItem.HINHANH;
        }
        private void resetRoom()
        {
            txtTenPhong = string.Empty;
            txtDonGia = 0;
            txtTinhTrangThuePhong = string.Empty;
            
        }


        public QuanLyPhong_LoaiPhongViewModel()
        {
            listLoaiPhong = new BindingList<LOAIPHONG>(DatabaseQueryTN.danhsachLoaiPhong());
            listPhong = new BindingList<PHONG>(DatabaseQueryTN.danhSachPhong());
            timLoaiPhongCommand = new RelayCommand<Object>((P) => { return true; }, (p) =>
            {
                listLoaiPhong = new BindingList<LOAIPHONG>(DatabaseQueryTN.timKiemLoaiPhong(timLoaiPhongInput));
            });
            timPhongCommand = new RelayCommand<Object>((P) => { return true; }, (p) =>
            {
                listPhong = new BindingList<PHONG>(DatabaseQueryTN.timKiemPhong(timPhongInput));
            });
            // CLick button add
            addPhongCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                isAddRoom = true;
                choPhepThayDoi = true;
                cancelButtonName = "HỦY";
                confirmButtonName = "THÊM";
                DatabaseQuery.capNhatCSDL();
            });
            // them moi phong
            addNewRoomCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                isAddnewRoom = true;
                choPhepUpdateRoom = true;
                roomCancelBtnName = "HỦY";
                roomConfirmBtnName = "THÊM";
                DatabaseQuery.capNhatCSDL();
            });
            // THEM MOI / CAP NHAT
            // 1. PHONG
            comfirmRoomBtnCommand = new RelayCommand<Object>((p) =>
            {
                //
                    // txtTinhTrangThuePhong = "Chưa thuê";
                if (itemLoaiPhongChon == null || string.IsNullOrEmpty(txtTenPhong.ToString()) || string.IsNullOrEmpty(itemLoaiPhongChon.TenLoai.ToString()) || string.IsNullOrEmpty(txtPhmgID.ToString()) ||
                string.IsNullOrEmpty(txtDonGia.ToString()))
                    return false;
                return true;
            }, (p) =>
            {
                PHONG newLP = new PHONG();
                newLP.PhongID = txtPhmgID;
                newLP.LoaiPhongID = itemLoaiPhongChon.LoaiPhongID;
                newLP.TenPhong = txtTenPhong;
                newLP.DonGia = txtDonGia;
                newLP.TinhTrangThue = true;
                // Neu dang o che do them User
                if (isAddnewRoom)
                {
                    if (DatabaseQueryTN.kiemTraTonTaiPhong(txtPhmgID))
                    {
                        MessageBox.Show("Phòng đã tồn tại");
                        return;

                    }
                    try
                    {
                        DatabaseQueryTN.themMoiPhong(newLP);
                        MessageBox.Show("Thêm mới phòng thành công");
                        listPhong = new BindingList<PHONG>(DatabaseQueryTN.danhSachPhong());
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Không thể thêm phòng " + e.ToString());
                    }
                }
                else // Cap Nhat
                {
                    try
                    {
                        DatabaseQueryTN.capNhatPhong(newLP);
                        listPhong = new BindingList<PHONG>(DatabaseQueryTN.danhSachPhong());
                        MessageBox.Show("Đã cập nhật loại phòng");

                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Không thể cập nhật loại phòng." + e.ToString());
                    }
                    //showDetails();
                }
            });

            confirmButtonCommmand = new RelayCommand<Object>((p) =>
            {
                if (string.IsNullOrEmpty(txtMaLoai.ToString()) ||
                string.IsNullOrEmpty(txtTen))
                    return false;
                return true;
            }, (p) =>
            {
                LOAIPHONG newLP = new LOAIPHONG();
                newLP.LoaiPhongID = txtMaLoai;
                newLP.TenLoai = txtTen;
                // Neu dang o che do them User
                if (isAddRoom)
                {
                    if (DatabaseQueryTN.kiemTraTonTaiLoaiPhong(txtMaLoai))
                    {
                        MessageBox.Show("Loại phòng đã tồn tại");
                        return;
                        //if (!DatabaseQueryTN.isDeleteRoom(txtMaLoai)) // true --> delete
                        //{
                        //}

                    }
                    try
                    {
                        DatabaseQueryTN.themMoiLoaiPhong(newLP);
                        MessageBox.Show("Thêm mới nhân viên thành công. Mật khẩu mặc định là CMND");
                        listLoaiPhong = new BindingList<LOAIPHONG>(DatabaseQueryTN.danhsachLoaiPhong());
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Không thể thêm loại phòng " + e.ToString());
                    }
                }
                else // Cap Nhat
                {
                    try
                    {
                        DatabaseQueryTN.capNhatLoaiPhong(newLP);
                        listLoaiPhong = new BindingList<LOAIPHONG>(DatabaseQueryTN.danhsachLoaiPhong());
                        MessageBox.Show("Đã cập nhật loại phòng");

                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Không thể cập nhật loại phòng." + e.ToString());
                    }
                    //showDetails();
                }
            });

            // Huỷ Thêm hoặc Xoá
            // 1. Loai Phong Huy / Xoa
            cancelButtonCommmand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                if (isAddRoom)
                {
                    try
                    {
                        selectItem = listLoaiPhong[0];
                        isAddRoom = false;
                        choPhepThayDoi = false;
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
                        DatabaseQueryTN.xoaLoaiPhong(selectItem);
                        MessageBox.Show("Đã xoá loại phòng này");
                        listLoaiPhong = new BindingList<LOAIPHONG>(DatabaseQueryTN.danhsachLoaiPhong());
                        reset();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Không thể xoá loại phòng này");
                    }
                }
            });
            // 2. Phong Huy / Xoa
            cancelRoomBtnCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                if (isAddnewRoom)
                {
                    try
                    {
                        selectPhong = listPhong[0];
                        isAddnewRoom = false;
                        roomCancelBtnName = "XOÁ";
                        roomConfirmBtnName = "LƯU"; // cuoi vl
                        DatabaseQuery.capNhatCSDL();
                    }
                    catch (Exception) { };
                    return;
                }
                else
                {
                    try
                    {
                        DatabaseQueryTN.xoaPhong(selectPhong);
                        MessageBox.Show("Đã xoá phòng này");
                        listPhong = new BindingList<PHONG>(DatabaseQueryTN.danhSachPhong());
                        resetRoom();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Không thể xoá phòng này");
                    }
                }
            });
        }
    }
}
