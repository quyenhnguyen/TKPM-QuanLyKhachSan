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
    public class QuanLyDichVu_LoaiDVViewModel : BaseViewModel
    {
        // Quan Ly Loai Phong
        private bool isAddDV = true;
        private bool _choPhepDoi = true;
        public bool choPhepThayDoi { get => _choPhepDoi; set => OnPropertyChanged(ref _choPhepDoi, value); }
        private string _timLoaiInput;
        public string timLoaiDVInput { get => _timLoaiInput; set => OnPropertyChanged(ref _timLoaiInput, value); }

        public ICommand timLoaiDVCommand { get; set; }
        public ICommand loaidvconfirmButtonCommmand { get; set; }
        public ICommand loaidvcancelButtonCommmand { get; set; }
        public ICommand addDVCommand { get; set; }

        private string _cancelButtonName = "HỦY";
        private string _confirmButtonName = "THÊM";
        public string loaidvcancelButtonName
        {
            get => _cancelButtonName;
            set
            {
                _cancelButtonName = value;
                OnPropertyChanged();
            }
        }
        public string loaidvconfirmButtonName
        {
            get => _confirmButtonName;
            set
            {
                _confirmButtonName = value;
                OnPropertyChanged();
            }
        }
        private BindingList<LOAIDV> _listLoaiDV;
        public BindingList<LOAIDV> listLoaiDV
        {
            get
            {
                return _listLoaiDV;
            }
            set
            {
                _listLoaiDV = value;
                OnPropertyChanged();
            }
        }
        private string _maLoai;
        public string txtMaLoaiDV { get => _maLoai; set => OnPropertyChanged(ref _maLoai, value); }
        private string _tenLoai;
        public string txtTenDV { get => _tenLoai; set => OnPropertyChanged(ref _tenLoai, value); }
        private DateTime _NgayTao;
        public DateTime txtNgayTao { get => _NgayTao; set => OnPropertyChanged(ref _NgayTao, value); }
        private LOAIDV item { get; set; }
        public LOAIDV selectItem
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
                txtMaLoaiDV = selectItem.LoaiDVID;
                txtTenDV = selectItem.TenLoai;
                txtNgayTao = selectItem.NgayTao;
                isAddDV = false;
                loaidvcancelButtonName = "XÓA";
                loaidvconfirmButtonName = "LƯU";
            }
            else
            {
                isAddDV = true;
                choPhepThayDoi = true;
                loaidvcancelButtonName = "HỦY";
                loaidvconfirmButtonName = "THÊM";
            }
            // DisplayedImagePath = selectItem.HINHANH;
        }
        private void reset()
        {
            txtTenDV = string.Empty;
            txtMaLoaiDV = string.Empty;
        }

        // Quan Ly Phong
        public QuanLyDichVu_LoaiDVViewModel()
        {
            listLoaiDV = new BindingList<LOAIDV>(DatabaseQueryTN.danhsachLoaDV());
            timLoaiDVCommand = new RelayCommand<Object>((P) => { return true; }, (p) =>
            {
                listLoaiDV = new BindingList<LOAIDV>(DatabaseQueryTN.timKiemLoaiDV(timLoaiDVInput));
            });
            // CLick button add
            addDVCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                isAddDV = true;
                choPhepThayDoi = true;
                loaidvcancelButtonName = "HỦY";
                loaidvconfirmButtonName = "THÊM";
                DatabaseQuery.capNhatCSDL();
            });
            // THEM MOI / CAP NHAT
            // 1. PHONG
            loaidvconfirmButtonCommmand = new RelayCommand<Object>((p) =>
            {
                if (string.IsNullOrEmpty(txtMaLoaiDV.ToString()) ||
                string.IsNullOrEmpty(txtTenDV))
                    return false;
                return true;
            }, (p) =>
            {
                LOAIDV newLP = new LOAIDV();
                newLP.LoaiDVID = txtMaLoaiDV;
                newLP.TenLoai = txtTenDV;
                // Neu dang o che do them User
                if (isAddDV)
                {
                    if (DatabaseQueryTN.kiemTraTonTaiLoaiDV(txtMaLoaiDV))
                    {
                        MessageBox.Show("loại dịch vụ đã tồn tại");
                        return;
                        //if (!DatabaseQueryTN.isDeleteRoom(txtMaLoai)) // true --> delete
                        //{
                        //}

                    }
                    try
                    {
                        DatabaseQueryTN.themMoiLoaiDV(newLP);
                        MessageBox.Show("Thêm mới loại dịch vụ thành công");
                        listLoaiDV = new BindingList<LOAIDV>(DatabaseQueryTN.danhsachLoaDV());
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Không thể thêm loại dịch vụ " + e.ToString());
                    }
                }
                else // Cap Nhat
                {
                    try
                    {
                        DatabaseQueryTN.capNhatLoaiDV(newLP);
                        listLoaiDV = new BindingList<LOAIDV>(DatabaseQueryTN.danhsachLoaDV());
                        MessageBox.Show("Đã cập nhật loại dịch vụ");
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Không thể cập nhật loại dịch vụ." + e.ToString());
                    }
                    //showDetails();
                }
            });

            // Huỷ Thêm hoặc Xoá
            // 1. Loai Phong Huy / Xoa
            loaidvcancelButtonCommmand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                if (isAddDV)
                {
                    try
                    {
                        selectItem = listLoaiDV[0];
                        isAddDV = false;
                        choPhepThayDoi = false;
                        loaidvcancelButtonName = "XOÁ";
                        loaidvconfirmButtonName = "LƯU";
                        DatabaseQuery.capNhatCSDL();
                    }
                    catch (Exception) { };
                    return;
                }
                else
                {
                    try
                    {
                        DatabaseQueryTN.xoaLoaiDV(selectItem);
                        MessageBox.Show("Đã xoá loại dịch vụ này");
                        listLoaiDV = new BindingList<LOAIDV>(DatabaseQueryTN.danhsachLoaDV());
                        reset();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Không thể xoá loại dịch vụ này");
                    }
                }
            });
            // 2. Phong Huy / Xoa
            
        }
    }
}
