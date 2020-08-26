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
using System.Windows.Data;
using System.Windows.Input;

namespace QuanLiKhachSan.ViewModel
{
    class KeToanBaoCaoDichVuViewModel : BaseViewModel
    {
        private DateTime _dateBD;
        public DateTime NgayBatDau { get => _dateBD; set => OnPropertyChanged(ref _dateBD, value); }
        private DateTime _dateKT;
        public DateTime NgayKetThuc { get => _dateKT; set => OnPropertyChanged(ref _dateKT, value); }
        private BindingList<ThongTinBaoCao> _listDichVu;
        public BindingList<ThongTinBaoCao> ListBaoCaoDichVu { get => _listDichVu; set => OnPropertyChanged(ref _listDichVu, value); }

        public ICommand XemBaoCaoCommand { get; set; }

        private SeriesCollection _Series;
        public SeriesCollection Series { get => _Series; set => OnPropertyChanged(ref _Series, value); }

        private Visibility _TonTaiHoaDon;
        public Visibility TonTaiHoaDon { get => _TonTaiHoaDon; set => OnPropertyChanged(ref _TonTaiHoaDon, value); }
        private Visibility _TonTaiBaoCao;
        public Visibility TonTaiBaoCao { get => _TonTaiBaoCao; set => OnPropertyChanged(ref _TonTaiBaoCao, value); }
        private double _TongDoanhThu;
        public double TongDoanhThu { get => _TongDoanhThu; set => OnPropertyChanged(ref _TongDoanhThu, value); }
        public KeToanBaoCaoDichVuViewModel()
        {
            TonTaiBaoCao = Visibility.Hidden;
            TonTaiHoaDon = Visibility.Hidden;
            NgayBatDau = DateTime.Now;
            NgayKetThuc = DateTime.Now;
            XemBaoCaoCommand = new RelayCommand<object>((p) => { return true; }, (p) => { HienThongTinBaoCao(); });
        }

        public void HienThongTinBaoCao()
        {
            if (!DatabaseQuery.tonTaiHoaDonTheoNgay(NgayBatDau, NgayKetThuc))
            {
                TonTaiBaoCao = Visibility.Collapsed;
                TonTaiHoaDon = Visibility.Visible;
                return;
            }
            TonTaiBaoCao = Visibility.Visible;
            TonTaiHoaDon = Visibility.Collapsed;
            ListBaoCaoDichVu = DatabaseQuery.truyVanDoanhThuTheoDichVu(NgayBatDau, NgayKetThuc);

            //Vẽ biểu đồ
            Series = new SeriesCollection();
            Func<ChartPoint, string> labelPoint2 = chartPoint =>
            string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
            foreach (ThongTinBaoCao bc in ListBaoCaoDichVu)
                Series.Add(new PieSeries
                {
                    Title = bc.TenDonVi,
                    Values = new ChartValues<double> { bc.DoanhThu },
                    PushOut = 15,
                    DataLabels = true,
                    LabelPoint = labelPoint2
                });
            TongDoanhThu = ListBaoCaoDichVu.Sum(x => x.DoanhThu);
        }
    }

}

