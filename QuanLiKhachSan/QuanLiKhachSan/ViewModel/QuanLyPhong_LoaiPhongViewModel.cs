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
        private bool isAddRoom = true;

        private bool _choPhepDoi = true;
        public bool choPhepThayDoi { get => _choPhepDoi; set => OnPropertyChanged(ref _choPhepDoi, value); }
        private string _timLoaiInput;
        public string timPhongInput { get => _timLoaiInput; set => OnPropertyChanged(ref _timLoaiInput, value); }
        public ICommand timLoaiPhong { get; set; }
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
        public QuanLyPhong_LoaiPhongViewModel()
        {
            listLoaiPhong = new BindingList<LOAIPHONG>(DatabaseQueryTN.danhsachLoaiPhong());

            timLoaiPhong = new RelayCommand<Object>((P) => { return true; }, (p) =>
            {
                listLoaiPhong = new BindingList<LOAIPHONG>(DatabaseQueryTN.timKiemLoaiPhong(timPhongInput));
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
            // click button them / luuw
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
                        MessageBox.Show("Không thể thêm loại phòng "  + e.ToString());
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
                //hmm, 
            });






        }
    }
}
