using QuanLiKhachSan.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLiKhachSan.ViewModel
{
    public class LeTanDialogCheckOutViewModel : BaseViewModel
    {
        public int MaHoaDon { get; set; }

        public HOADONTHUEPHONG HoaDon { get; set; }
        private string _ngayCheckIn;
        public string NgayCheckIn { get => _ngayCheckIn; set => OnPropertyChanged(ref _ngayCheckIn, value); }
        private string _ngayCheckOut;
        public string NgayCheckOut { get => _ngayCheckOut; set => OnPropertyChanged(ref _ngayCheckOut, value); }
        private string _tienPhong;
        public string TienPhong { get => _tienPhong; set => OnPropertyChanged(ref _tienPhong, value); }

        public ICommand ThoatBtnComamand { get; set; }
        public ICommand DongYBtnCommand { get; set; }
        public LeTanDialogCheckOutViewModel(int maHD)
        {
            MaHoaDon = maHD;
            HoaDon = DatabaseQuery.truyVanHoaDonDangThueMaHD(MaHoaDon);
            //tính tiền phòng: (lấy ngày ra - ngày vào)* đơn giá
            NgayCheckIn = HoaDon.ThoiGianThue.ToString("dd/mm/yyyy-HH:mm:ss");
            NgayCheckOut = DateTime.Now.ToString("dd/mm/yyyy-HH:mm:ss");

            ThoatBtnComamand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

            DongYBtnCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.DialogResult = true;
                //lưu xún csdl cả hóa đơn với đầy đủ các trường
            });
        }
    }
}
