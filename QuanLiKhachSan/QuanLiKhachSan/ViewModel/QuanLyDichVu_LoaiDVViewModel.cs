using Microsoft.Win32;
using QuanLiKhachSan.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

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
        private string _currentSelectLoaiDV;
        public string currentSelectLoaiDV { get => _currentSelectLoaiDV; set => OnPropertyChanged(ref _currentSelectLoaiDV, value); }

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
        private void resetDV()
        {
            txtIDDV = string.Empty;
            txtGiaBan = 0;
            txtGiaNhap = 0;
            txtDonVi = string.Empty;

        }

        // PART 2 : DICH VU
        private string _idLoai;
        public string txtIDDV { get => _idLoai; set => OnPropertyChanged(ref _idLoai, value); }
        private string _tenDichVu;
        public string txtTenDichVu { get => _tenDichVu; set => OnPropertyChanged(ref _tenDichVu, value); }
        private string _donVi;
        public string txtDonVi { get => _donVi; set => OnPropertyChanged(ref _donVi, value); }
        private double _giaNhap;
        public double txtGiaNhap { get => _giaNhap; set => OnPropertyChanged(ref _giaNhap, value); }
        private double _giaBan;
        public double txtGiaBan { get => _giaBan; set => OnPropertyChanged(ref _giaBan, value); }
        private BindingList<DICHVU> _listDV;
        public BindingList<DICHVU> listDV
        {
            get
            {
                return _listDV;
            }
            set
            {
                _listDV = value;
                OnPropertyChanged();
            }
        }

        public ICommand timDVCommand { get; set; }
        public ICommand dvconfirmButtonCommmand { get; set; }
        public ICommand dvcancelButtonCommmand { get; set; }
        public ICommand addDichVuCommand { get; set; }
        public ICommand ChonAnhDVCommand { get; set; }

        private string _dvcancelButtonName = "HỦY";
        private string _dvconfirmButtonName = "THÊM";
        public string dvcancelButtonName
        {
            get => _dvcancelButtonName;
            set
            {
                _dvcancelButtonName = value;
                OnPropertyChanged();
            }
        }
        public string dvconfirmButtonName
        {
            get => _dvconfirmButtonName;
            set
            {
                _dvconfirmButtonName = value;
                OnPropertyChanged();
            }
        }

        private bool isAddDichVu = true;
        private bool _choPhepDoiIDDichVu = true;
        public bool choPhepThayDoiIDDV { get => _choPhepDoiIDDichVu; set => OnPropertyChanged(ref _choPhepDoiIDDichVu, value); }
        private string _timDVInput;
        public string timDVInput { get => _timDVInput; set => OnPropertyChanged(ref _timDVInput, value); }
        private BitmapImage _HinhAnhDV;
        public BitmapImage HinhAnhDichVu
        {
            get { return _HinhAnhDV; }
            set { _HinhAnhDV = value; OnPropertyChanged(); }
        }

        private DICHVU dvItem { get; set; }
        public DICHVU selectDV
        {
            get { return dvItem; }
            set
            {
                if (dvItem != value)
                {
                    dvItem = value;

                    showDetailDV();
                }
            }
        }
        private LOAIDV ldvItem { get; set; }
        public LOAIDV chonLoaiDVchoDV// combo
        {
            get { return ldvItem; }
            set
            {
                if (ldvItem != value)
                {

                    currentSelectLoaiDV = value.TenLoai;
                    ldvItem = value;
                }
            }
        }
        public void showDetailDV()//
        {

            if (selectDV != null)
            {   choPhepThayDoiIDDV = false;
                chonLoaiDVchoDV = selectDV.LOAIDV;
                currentSelectLoaiDV = chonLoaiDVchoDV.TenLoai;
                txtIDDV = selectDV.DichVuID;
                txtTenDichVu = selectDV.TenDichVu;
                txtGiaBan = selectDV.GiaBan;
                txtGiaNhap = selectDV.GiaCungCap;
                txtDonVi = selectDV.DonVi;
                HinhAnhDichVu = SecurityModel.LoadImage(selectDV.HinhAnh);
                isAddDichVu = false;
                dvcancelButtonName = "XÓA";
                dvconfirmButtonName = "LƯU";
            }
            else
            {
                isAddDichVu = true;
                choPhepThayDoiIDDV = true;
                dvcancelButtonName = "HỦY";
                dvconfirmButtonName = "THÊM";
            }
            // DisplayedImagePath = selectItem.HINHANH;
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


            // PART 2 : DICH VU
            listDV = new BindingList<DICHVU>(DatabaseQueryTN.danhSachDivhVu());
            ChonAnhDVCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    Uri fileUri = new Uri(openFileDialog.FileName);
                    HinhAnhDichVu = new BitmapImage(new Uri(openFileDialog.FileName));
                }
            }
            );
            timDVCommand = new RelayCommand<Object>((P) => { return true; }, (p) =>
            {
                listDV = new BindingList<DICHVU>(DatabaseQueryTN.timKiemDV(timDVInput));
            });
            // CLick button add
            addDichVuCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                isAddDichVu = true;
                choPhepThayDoiIDDV = true;
                dvcancelButtonName = "HỦY";
                dvconfirmButtonName = "THÊM";
                currentSelectLoaiDV = "Chọn loại dịch vụ";
                DatabaseQuery.capNhatCSDL();
            });
            // THEM MOI / CAP NHAT
            // 1. PHONG
            dvconfirmButtonCommmand = new RelayCommand<Object>((p) =>
            {
                if (string.IsNullOrEmpty(txtIDDV.ToString()) ||
                string.IsNullOrEmpty(txtTenDichVu.ToString()) ||
                string.IsNullOrEmpty(txtDonVi.ToString()) ||
                string.IsNullOrEmpty(txtGiaNhap.ToString()) ||
                string.IsNullOrEmpty(txtGiaBan.ToString()) || HinhAnhDichVu == null)
                    return false;
                return true;
            }, (p) =>
            {
                DICHVU newLP = new DICHVU();
                newLP.DichVuID = txtIDDV;
                newLP.LoaiDVID = selectDV.LoaiDVID;
                newLP.LOAIDV = selectDV.LOAIDV;
                newLP.TenDichVu = txtTenDichVu;
                newLP.GiaBan = txtGiaBan;
                newLP.GiaCungCap = txtGiaNhap;
                newLP.DonVi = txtDonVi;
                newLP.LoaiDVID = chonLoaiDVchoDV.LoaiDVID;
                newLP.NgayTao = selectDV.NgayTao;
                newLP.HinhAnh = SecurityModel.ImageToByte2(HinhAnhDichVu);

                // Neu dang o che do them User
                if (isAddDichVu)
                {
                    if (DatabaseQueryTN.kiemTraTonDichVu(txtIDDV))
                    {
                        MessageBox.Show("Dịch vụ đã tồn tại");
                        return;
                    }
                    try
                    {
                        DatabaseQueryTN.themMoiDichVu(newLP);
                        MessageBox.Show("Thêm mới loại dịch vụ thành công");
                        listDV = new BindingList<DICHVU>(DatabaseQueryTN.danhSachDivhVu());
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Không thể thêm dịch vụ " + e.ToString());
                    }
                }
                else // Cap Nhat
                {
                    try
                    {
                        DatabaseQueryTN.capNhatDV(newLP);
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
            dvcancelButtonCommmand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                if (isAddDichVu)
                {
                    try
                    {
                        selectDV = listDV[0];
                        isAddDichVu = false;
                        choPhepThayDoiIDDV = false;
                        dvcancelButtonName = "XOÁ";
                        dvconfirmButtonName = "LƯU";
                        DatabaseQuery.capNhatCSDL();
                    }
                    catch (Exception) { };
                    return;
                }
                else
                {
                    try
                    {
                        DatabaseQueryTN.xoaDV(selectDV);
                        MessageBox.Show("Đã xoá dịch vụ này");
                        listLoaiDV = new BindingList<LOAIDV>(DatabaseQueryTN.danhsachLoaDV());
                        resetDV();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Không thể xoá loại dịch vụ này");
                    }
                }
            });
        }
    }
}
