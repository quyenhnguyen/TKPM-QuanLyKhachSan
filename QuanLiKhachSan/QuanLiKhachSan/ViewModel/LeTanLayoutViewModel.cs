using QuanLiKhachSan.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using QuanLiKhachSan.ViewModel;
using QuanLiKhachSan.Model;

namespace QuanLiKhachSan.ViewModel
{
    public class LeTanLayoutViewModel : BaseViewModel
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
        private string _txtTitle;
        public string txtTitle
        {
            get => _txtTitle;
            set { _txtTitle = value; OnPropertyChanged(); }
        }
        #region All Command
        public ICommand btnDatPhong_Command { get; set; }
        public ICommand btnTaiKhoan_Command { get; set; }
        public ICommand btnTraCuu_Command { get; set; }
        #endregion
        public LeTanLayoutViewModel()
        {
            //NHANVIEN b = new NHANVIEN();
            //b = DataProvider.ISCreated.DB.NHANVIENs.First();
            //MessageBox.Show(b.DiaChi);
            object ucDatPhong = new LeTanDatPhongViewModel();
            object ucTraCuu = new LeTanTraCuuViewModel();
            object ucTaiKhoan = new LeTanTaiKhoanViewModel();
            CurrentDataContext = ucDatPhong;
            txtTitle = "TRANG CHỦ ĐẶT PHÒNG";
            btnDatPhong_Command = new RelayCommand<object>((p) => { return CurrentDataContext != ucDatPhong; }, (p) =>
                {
                    txtTitle = "TRANG CHỦ ĐẶT PHÒNG";
                    //Cần gán DataContext bên View qua mà ko lấy được-> Dùng biến
                    CurrentDataContext = new LeTanDatPhongViewModel();//load lại mỗi khi click vào
                });
            btnTraCuu_Command = new RelayCommand<object>((p) => { return CurrentDataContext != ucTraCuu; }, (p) =>
              {
                  txtTitle = "TRA CỨU, QUẢN LÍ";
                  CurrentDataContext = ucTraCuu;
              });
            btnTaiKhoan_Command = new RelayCommand<object>((p) => { return CurrentDataContext != ucTaiKhoan; }, (p) =>
              {
                  txtTitle = "TRANG CÁ NHÂN";
                  CurrentDataContext = ucTaiKhoan;
              });
        }

    }
}
