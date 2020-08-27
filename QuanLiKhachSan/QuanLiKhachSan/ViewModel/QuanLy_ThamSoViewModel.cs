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
    public class QuanLy_ThamSoViewModel : BaseViewModel
    {

        private bool isAddPara = true;
        public ICommand timThamSoCommand { get; set; }
        public ICommand confirmButtonCommmand { get; set; }
        public ICommand cancelButtonCommmand { get; set; }
        public ICommand addThamSoCommand { get; set; }
        private bool _choPhepCapNhat = true;
        public bool choPhepThayDoi
        {
            get => _choPhepCapNhat;
            set
            {
                _choPhepCapNhat = value;
                OnPropertyChanged();
            }
        }
        private string _timThamSoInput = "";
        public string timThamSoInput
        {
            get => _timThamSoInput;
            set
            {
                _timThamSoInput = value;
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
        private BindingList<BANGTHAMSO> _listThamSo;
        public BindingList<BANGTHAMSO> listThamSo
        {
            get
            {
                return _listThamSo;
            }
            set
            {
                _listThamSo = value;
                OnPropertyChanged();
            }
        }
        private BANGTHAMSO item { get; set; }
        public BANGTHAMSO selectItem
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
        private string _ThamSoID;
        public string txtThamSoID { get => _ThamSoID; set => OnPropertyChanged(ref _ThamSoID, value); }
        private string _TenThamSo;
        public string txtTenThamSo { get => _TenThamSo; set => OnPropertyChanged(ref _TenThamSo, value); }
        private string _KieuDuLieu;
        public string txtKieuDuLieu { get => _KieuDuLieu; set => OnPropertyChanged(ref _KieuDuLieu, value); }
        private DateTime _NgayTao;
        public DateTime txtNgayTao { get => _NgayTao; set => OnPropertyChanged(ref _NgayTao, value); }
        private string _GiaTri;
        public string txtGiaTri { get => _GiaTri; set => OnPropertyChanged(ref _GiaTri, value); }
        public void showDetails()
        {

            if (selectItem != null)
            {
                choPhepThayDoi = true;
                   txtThamSoID = selectItem.MaThamSo;
                txtTenThamSo = selectItem.TenThamSo;
                txtKieuDuLieu = selectItem.KieuDuLieu;
                txtGiaTri = selectItem.GiaTri;
                txtNgayTao = selectItem.NgayTao;
                isAddPara = false;
                cancelButtonName = "XÓA";
                confirmButtonName = "LƯU";
            }
            else
            {
                choPhepThayDoi = false;
                isAddPara = true;
                cancelButtonName = "HỦY";
                confirmButtonName = "THÊM";
            }
            // DisplayedImagePath = selectItem.HINHANH;
        }
        public QuanLy_ThamSoViewModel()
        {
            listThamSo = new BindingList<BANGTHAMSO>(DatabaseQueryTN.danhSachThamSo());
            timThamSoCommand = new RelayCommand<Object>((P) => { return true; }, (p) =>
            {
                listThamSo = new BindingList<BANGTHAMSO>(DatabaseQueryTN.timKiemThamSo(timThamSoInput));
            });

            addThamSoCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                choPhepThayDoi = true;
                isAddPara = true;
                cancelButtonName = "HỦY";
                confirmButtonName = "THÊM";
                DatabaseQuery.capNhatCSDL();
            });
            // Huỷ Thêm hoặc Xoá
            cancelButtonCommmand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                if (isAddPara)
                {
                    try
                    {
                        selectItem = listThamSo[0];
                        isAddPara = false;
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
                        DatabaseQueryTN.xoaThamSo(selectItem);
                        DatabaseQuery.MyMessageBox("Đã xoá nhân viên này");
                        listThamSo = new BindingList<BANGTHAMSO>(DatabaseQueryTN.danhSachThamSo());

                    }
                    catch (Exception e)
                    {
                        SecurityModel.Log(e.ToString());
                        DatabaseQuery.MyMessageBox("Không thể xoá nhân viên này");
                    }
                }
                //hmm, 
            });
            // Thêm Mới hoặc Cập Nhật
            confirmButtonCommmand = new RelayCommand<Object>((p) =>
            {
                try
                {
                    if (string.IsNullOrEmpty(txtGiaTri) ||
                    string.IsNullOrEmpty(txtNgayTao.ToString("dd/mm/yyyy")) ||
                    string.IsNullOrEmpty(txtGiaTri) ||
                    string.IsNullOrEmpty(txtKieuDuLieu) ||
                    string.IsNullOrEmpty(txtThamSoID) ||
                    string.IsNullOrEmpty(txtTenThamSo))
                        return false;
                }
                catch (Exception) { return false; };
                return true;
            }, (p) =>
            {
                BANGTHAMSO newNV = new BANGTHAMSO();
                newNV.MaThamSo = txtThamSoID;
                newNV.KieuDuLieu = txtKieuDuLieu;
                newNV.TinhTrang = false;
                newNV.TenThamSo = txtTenThamSo;
                newNV.GiaTri = txtGiaTri;
                // Neu dang o che do them User
                if (isAddPara)
                {
                    if (DatabaseQueryTN.kiemTraTonTaiThamSo(txtThamSoID))
                    {
                        DatabaseQuery.MyMessageBox("Tham số đã tồn tại");
                        return;
                    }
                    try
                    {
                        DatabaseQueryTN.themMoiThamSo(newNV);
                        DatabaseQuery.MyMessageBox("Thêm mới tham số thành công");
                        listThamSo = new BindingList<BANGTHAMSO>(DatabaseQueryTN.danhSachThamSo());
                    }
                    catch
                    {
                        DatabaseQuery.MyMessageBox("Tham số đã tồn tại");
                    }
                }
                else // Cap Nhat
                {
                    try
                    {
                        DatabaseQueryTN.capNhatThamSo(newNV);
                        DatabaseQuery.MyMessageBox("Đã cập nhật tham số");
                        listThamSo = new BindingList<BANGTHAMSO>(DatabaseQueryTN.danhSachThamSo());

                    }
                    catch (Exception e)
                    {
                        SecurityModel.Log(e.ToString());
                        DatabaseQuery.MyMessageBox("Không thể cập nhật tham số.");
                    }
                }
            });

        }
    }
}
