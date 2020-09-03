using Microsoft.Win32;
using QuanLiKhachSan.Model;
using QuanLiKhachSan.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
            isAddDV = true;
            choPhepThayDoi = true;
            loaidvcancelButtonName = "HỦY";
            loaidvconfirmButtonName = "THÊM";
            currentSelectLoaiDV = "Chọn loại dịch vụ";
            txtTenDV = string.Empty;
            txtMaLoaiDV = string.Empty;
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

        private void resetDV()
        {
            isAddDichVu = true;
            choPhepThayDoiIDDV = true;
            txtIDDV = string.Empty;
            txtTenDichVu = null;
            selectDV = null;
            txtGiaBan = 0;
            txtGiaNhap = 0;
            currentSelectLoaiDV = "Chọn loại dịch vụ";
            txtDonVi = string.Empty;
            HinhAnhDichVu = null;
            dvconfirmButtonName = "THÊM";
            dvcancelButtonName = "HUỶ";
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
            {
                choPhepThayDoiIDDV = false;
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
                resetDV();
            }
            // DisplayedImagePath = selectItem.HINHANH;
        }
        private bool checkCondition()
        {
            try
            {
                return (string.IsNullOrEmpty(txtIDDV) ||
                        string.IsNullOrEmpty(txtTenDichVu) ||
                        string.IsNullOrEmpty(txtDonVi) ||
                       (txtGiaNhap == 0) ||
                       (txtGiaBan == 0) || HinhAnhDichVu == null);
            }
            catch (Exception) { return true; }
        }
        // Quan Ly Phong

        //excel command

        public ICommand exportLoaiDVCommand { get; set; }
        public ICommand importLoaiDVCommand { get; set; }
        public ICommand exportDVCommand { get; set; }
        public ICommand importDVCommand { get; set; }

        public QuanLyDichVu_LoaiDVViewModel()
        {

            listLoaiDV = new BindingList<LOAIDV>(DatabaseQueryTN.danhsachLoaDV());
            timLoaiDVCommand = new RelayCommand<Object>((P) => { return true; }, (p) =>
            {
                listLoaiDV = new BindingList<LOAIDV>(DatabaseQueryTN.timKiemLoaiDV(timLoaiDVInput));
            });
            // Them loai dich vu
            addDVCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                reset();
                DatabaseQuery.capNhatCSDL();
            });
            // THEM MOI / CAP NHAT
            // 1. Them moi / cap nhat loai dich vu
            loaidvconfirmButtonCommmand = new RelayCommand<Object>((p) =>
            {
                if (string.IsNullOrEmpty(txtMaLoaiDV) ||
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
                        DatabaseQuery.MyMessageBox("loại dịch vụ đã tồn tại");
                        return;
                        //if (!DatabaseQueryTN.isDeleteRoom(txtMaLoai)) // true --> delete
                        //{
                        //}

                    }
                    try
                    {
                        reset();
                        DatabaseQueryTN.themMoiLoaiDV(newLP);
                        DatabaseQuery.MyMessageBox("Thêm mới loại dịch vụ thành công");
                        listLoaiDV = new BindingList<LOAIDV>(DatabaseQueryTN.danhsachLoaDV());
                    }
                    catch (Exception e)
                    {
                        DatabaseQuery.MyMessageBox("Không thể thêm loại dịch vụ " + e.ToString());
                    }
                }
                else // Cap Nhat
                {
                    try
                    {
                        newLP.TinhTrang = false;
                        DatabaseQueryTN.capNhatLoaiDV(newLP);
                        listLoaiDV = new BindingList<LOAIDV>(DatabaseQueryTN.danhsachLoaDV());
                        DatabaseQuery.MyMessageBox("Đã cập nhật loại dịch vụ");
                    }
                    catch (Exception e)
                    {
                        DatabaseQuery.MyMessageBox("Không thể cập nhật loại dịch vụ." + e.ToString());
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
                        DatabaseQueryTN.xoaLoaiDV(selectItem);
                        DatabaseQuery.MyMessageBox("Đã xoá dịch vụ này");
                        listLoaiDV = new BindingList<LOAIDV>(DatabaseQueryTN.danhsachLoaDV());
                        listDV = new BindingList<DICHVU>(DatabaseQueryTN.danhSachDivhVu());
                        reset();
                    }
                    catch (Exception)
                    {
                        DatabaseQuery.MyMessageBox("Không thể xoá dịch vụ này");
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
            // Them moi dich vu
            addDichVuCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                resetDV();
                DatabaseQuery.capNhatCSDL();
            });
            // THEM MOI / CAP NHAT
            // 1. PHONG
            dvconfirmButtonCommmand = new RelayCommand<Object>((p) =>
            {
                if (checkCondition())
                    return false;
                return true;
            }, (p) =>
            {
                if (checkCondition()) { DatabaseQuery.MyMessageBox("Chưa nhập đầy đủ thông tin"); return; }
                DICHVU newLP = new DICHVU();
                try
                {
                    newLP.DichVuID = txtIDDV;
                    if (isAddDichVu)
                    {
                        newLP.LoaiDVID = chonLoaiDVchoDV.LoaiDVID;
                        newLP.LOAIDV = chonLoaiDVchoDV;
                    }
                    else
                    {
                        newLP.NgayTao = selectDV.NgayTao;
                        newLP.LoaiDVID = chonLoaiDVchoDV.LoaiDVID;
                        newLP.LOAIDV = chonLoaiDVchoDV;
                    }
                    newLP.TenDichVu = txtTenDichVu;
                    newLP.GiaBan = txtGiaBan;
                    newLP.GiaCungCap = txtGiaNhap;
                    newLP.DonVi = txtDonVi;
                    newLP.HinhAnh = SecurityModel.ImageToByte2(HinhAnhDichVu);
                }
                catch (Exception e)
                {
                    DatabaseQuery.MyMessageBox("Không thể thêm/cập nhật");
                    SecurityModel.Log(e.ToString());
                    return;
                }

                // Neu dang o che do them User
                if (isAddDichVu)
                {
                    if (DatabaseQueryTN.kiemTraTonDichVu(txtIDDV))
                    {
                        DatabaseQuery.MyMessageBox("Dịch vụ đã tồn tại");
                        return;
                    }
                    try
                    {
                        DatabaseQueryTN.themMoiDichVu(newLP);
                        DatabaseQuery.MyMessageBox("Thêm mới dịch vụ thành công");
                        listDV = new BindingList<DICHVU>(DatabaseQueryTN.danhSachDivhVu());
                    }
                    catch (Exception e)
                    {
                        DatabaseQuery.MyMessageBox("Không thể thêm dịch vụ ");
                        SecurityModel.Log(e.ToString());
                    }
                }
                else // Cap Nhat
                {
                    try
                    {
                        newLP.TinhTrangTonTai = false;
                        DatabaseQueryTN.capNhatDV(newLP);
                        listDV = new BindingList<DICHVU>(DatabaseQueryTN.danhSachDivhVu());
                        DatabaseQuery.MyMessageBox("Đã cập nhật dịch vụ");
                    }
                    catch (Exception e)
                    {
                        DatabaseQuery.MyMessageBox("Không thể cập nhật dịch vụ.");
                        SecurityModel.Log(e.ToString());
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
                        resetDV();
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
                        DatabaseQuery.MyMessageBox("Đã xoá dịch vụ này");
                        listDV = new BindingList<DICHVU>(DatabaseQueryTN.danhSachDivhVu());
                        resetDV();
                    }
                    catch (Exception e)
                    {
                        DatabaseQuery.MyMessageBox("Không thể xoá dịch vụ này");
                        SecurityModel.Log(e.ToString());
                    }
                }
            });


            //excel command implement
            exportLoaiDVCommand = new RelayCommand<Object>((P) => { return true; }, (p) =>
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
                IModelName modelName = ModelFactory.Factory("LOAIDV", name);
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
            importLoaiDVCommand = new RelayCommand<Object>((P) => { return true; }, (p) =>
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
                IModelName modelName = ModelFactory.Factory("LOAIDV", name);
                Thread newWindowThread = new Thread(() =>
                {
                    w.Dispatcher.Invoke(() =>
                    {
                        modelName.importExcel();
                    });

                    listLoaiDV = new BindingList<LOAIDV>(DatabaseQueryTN.danhsachLoaDV());
                    listDV = new BindingList<DICHVU>(DatabaseQueryTN.danhSachDivhVu());
                    w.Dispatcher.Invoke(() =>
                    {
                        wd.Close();
                    });
                });
                newWindowThread.Start();
                //ConcreteModelFactory ModelFactory = new ConcreteModelFactory();
                //IModelName modelName = ModelFactory.Factory("LOAIDV", "");
                //modelName.importExcel();
                //listLoaiDV = new BindingList<LOAIDV>(DatabaseQueryTN.danhsachLoaDV());
                //listDV = new BindingList<DICHVU>(DatabaseQueryTN.danhSachDivhVu());

            });
            exportDVCommand = new RelayCommand<Object>((P) => { return true; }, (p) =>
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
                IModelName modelName = ModelFactory.Factory("DICHVU", name);
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
            importDVCommand = new RelayCommand<Object>((P) => { return true; }, (p) =>
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
                IModelName modelName = ModelFactory.Factory("DICHVU", name);
                Thread newWindowThread = new Thread(() =>
                {
                    w.Dispatcher.Invoke(() =>
                    {
                        modelName.importExcel();
                    });
                    listDV = new BindingList<DICHVU>(DatabaseQueryTN.danhSachDivhVu());
                    w.Dispatcher.Invoke(() =>
                    {
                        wd.Close();
                    });
                });
                newWindowThread.Start();
                //ConcreteModelFactory ModelFactory = new ConcreteModelFactory();
                //IModelName modelName = ModelFactory.Factory("DICHVU", "");
                //modelName.importExcel();
                //listDV = new BindingList<DICHVU>(DatabaseQueryTN.danhSachDivhVu());

            });

        }
    }
}
