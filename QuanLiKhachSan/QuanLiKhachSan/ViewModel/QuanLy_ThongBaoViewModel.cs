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
            get => "Q&Q HOTEL";
            set
            {

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
                //Gửi thông báo
                MessageBox.Show("Gửi thông báo đi");
                MessageBox.Show(HasTagChon.Name);

            });
        }
    }
}
