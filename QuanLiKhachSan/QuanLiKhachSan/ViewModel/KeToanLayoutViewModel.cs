using QuanLiKhachSan.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLiKhachSan.ViewModel
{
    public class KeToanLayoutViewModel : BaseViewModel
    {
        private object _CurrentDataContext;
        public object CurrentDataContext
        {
            get => _CurrentDataContext;
            set
            {
                _CurrentDataContext = value;
                OnPropertyChanged();
            }
        }

        private NHANVIEN _NhanVienDangNhap;
        public NHANVIEN NhanVienDangNhap
        {
            get => _NhanVienDangNhap;
            set
            {
                _NhanVienDangNhap = value;
                OnPropertyChanged();
            }
        }
        #region All Command
        public ICommand btnBaoCaoCommand { get; set; }
        public ICommand btnNhanVienCommand { get; set; }
        public ICommand btnTaiKhoanCommand { get; set; }
        #endregion

        public KeToanLayoutViewModel()
        {
            int MaNhanVien = 1;
            NhanVienDangNhap = DatabaseQuery.truyVanNhanVien(MaNhanVien);
            object ucBaoCao = new KeToanBaoCaoViewModel();
            object ucNhanVien = new KeToanQLNhanVienViewModel();
            object ucTaiKhoan = new KeToanTaiKhoanViewModel();

            CurrentDataContext = ucBaoCao;

            btnBaoCaoCommand = new RelayCommand<object>((p) => { return CurrentDataContext != ucBaoCao; }, (p) =>
            {

                ucBaoCao = new KeToanBaoCaoViewModel();
                CurrentDataContext = ucBaoCao;
            });
            btnNhanVienCommand = new RelayCommand<object>((p) => { return CurrentDataContext != ucNhanVien; }, (p) =>
            {
                CurrentDataContext = ucNhanVien;
            });
            btnTaiKhoanCommand = new RelayCommand<object>((p) => { return CurrentDataContext != ucTaiKhoan; }, (p) =>
            {
                CurrentDataContext = ucTaiKhoan;
            });
        }
    }

}

