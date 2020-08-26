using LiveCharts;
using LiveCharts.Wpf;
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
    class KeToanBaoCaoDoanhThuViewModel : BaseViewModel
    {
        private string[] _Labels;
        public string[] Labels { get => _Labels; set => OnPropertyChanged(ref _Labels, value); }
        public Func<double, string> Formatter { get; set; }

        private DateTime _dateBD;
        public DateTime NgayBatDau { get => _dateBD; set => OnPropertyChanged(ref _dateBD, value); }
        private DateTime _dateKT;
        public DateTime NgayKetThuc { get => _dateKT; set => OnPropertyChanged(ref _dateKT, value); }
        private BindingList<ThongTinBaoCao> _listDichVu;
        public BindingList<ThongTinBaoCao> ListBaoCaoDichVu { get => _listDichVu; set => OnPropertyChanged(ref _listDichVu, value); }

        public ICommand XemBaoCaoCommand { get; set; }

        private SeriesCollection _SeriesCollection;
        public SeriesCollection SeriesCollection { get => _SeriesCollection; set => OnPropertyChanged(ref _SeriesCollection, value); }

        private Visibility _TonTaiHoaDon;
        public Visibility TonTaiHoaDon { get => _TonTaiHoaDon; set => OnPropertyChanged(ref _TonTaiHoaDon, value); }
        private Visibility _TonTaiBaoCao;
        public Visibility TonTaiBaoCao { get => _TonTaiBaoCao; set => OnPropertyChanged(ref _TonTaiBaoCao, value); }
        private double _TongDoanhThu;
        public double TongDoanhThu { get => _TongDoanhThu; set => OnPropertyChanged(ref _TongDoanhThu, value); }
        public KeToanBaoCaoDoanhThuViewModel()
        {
            NgayBatDau = DateTime.Now;
            NgayKetThuc = DateTime.Now;
            XemBaoCaoCommand = new RelayCommand<object>((p) => { return true; }, (p) => { HienThongTinBaoCao(); });

        }
        public void HienThongTinBaoCao()
        {
            ListBaoCaoDichVu = DatabaseQuery.truyVanHoaDonBaoCaoTheoLoiNhuan(NgayBatDau, NgayKetThuc);


            //Vẽ biểu đồ
            SeriesCollection = new SeriesCollection
            {
                 new ColumnSeries
                 {
                     Title = "Doanh Thu",
                     Values = new ChartValues<double> {}
                 }
            };
            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Chi Phí",
                Values = new ChartValues<double> { }
            });

            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Lọi nhuận",
                Values = new ChartValues<double> { }
            });
            //List ngày
            Labels = new string[ListBaoCaoDichVu.Count];
            int i = 0;
            foreach (ThongTinBaoCao bc in ListBaoCaoDichVu)
            {
                Labels[i++] = bc.TenDonVi; ;
                SeriesCollection[0].Values.Add(bc.DoanhThu);
                SeriesCollection[1].Values.Add(bc.ChiPhi);
                SeriesCollection[2].Values.Add(bc.LoiNhuan);

            }
            Formatter = value => value.ToString("N");
            TongDoanhThu = ListBaoCaoDichVu.Sum(x => x.LoiNhuan);
        }




    }
}
