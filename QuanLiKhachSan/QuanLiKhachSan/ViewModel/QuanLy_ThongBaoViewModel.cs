using QuanLiKhachSan.Model;
using QuanLiKhachSan.Model.MappingClass;
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
    class QuanLy_ThongBaoViewModel : BaseViewModel
    {
        private string nguoigui = "Q&Q HOTEL";
        public ICommand GuiBtnCommand { get; set; }
        private BindingList<THEDANGKI> _dsDaDangKi;
        public BindingList<THEDANGKI> dsDaDangKi
        {
            get => _dsDaDangKi; set
            {
                OnPropertyChanged(ref _dsDaDangKi, value);
            }
        }
        public string NguoiGui
        {
            get => nguoigui;
            set
            {

            }
        }
        private string _tieuDe;
        public string TieuDe
        {
            get => _tieuDe; set
            {
                OnPropertyChanged(ref _tieuDe, value);
            }
        }
        private string _noiDung;
        public string NoiDung
        {
            get => _noiDung; set
            {
                OnPropertyChanged(ref _noiDung, value);
            }
        }
        private BindingList<THEDANGKI> _dsHashTag;
        public BindingList<THEDANGKI> dsHashTag
        {
            get => _dsHashTag; set
            {
                OnPropertyChanged(ref _dsHashTag, value);
            }
        }
        THEDANGKI _HasTagChon;
        public THEDANGKI HasTagChon { get => _HasTagChon; set { OnPropertyChanged(ref _HasTagChon, value); } }
        public QuanLy_ThongBaoViewModel()

        {
            BindingList<HASTAG> listHashtag = DatabaseQueryTN.danhSachHashtag();

            MappingManager mappingManager2 = new MappingManager();
            mappingManager2.AddRule(new string[] { "HastagID" }, new string[] { "ID" }, new ClassCopyOneToOne());
            mappingManager2.AddRule(new string[] { "Name" }, new string[] { "Name" }, new ClassCopyOneToOne());
            dsHashTag = BusinessModel.ChuyenDoiDanhSach<HASTAG, THEDANGKI>(listHashtag, mappingManager2);
            GuiBtnCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ObserverService obs = ObserverService.Instance;
                obs.GuiThongBao(HasTagChon.ID,TieuDe, NoiDung);
                MessageBox.Show("Gửi thông báo thành công");

            });
        }
    }
}
