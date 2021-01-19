using QuanLiKhachSan.Model;
using QuanLiKhachSan.Model.MappingClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLiKhachSan.ViewModel
{
    public class PopUpDangKiThongBaoViewModel : BaseViewModel
    {
        public string myname
        {
            get; set;

        }
        public ICommand XoaThongBaoCommand { get; set; }


        private BindingList<THEDANGKI> _dsHashTag;
        public BindingList<THEDANGKI> dsHashTag
        {
            get => _dsHashTag; set
            {
                OnPropertyChanged(ref _dsHashTag, value);
            }
        }



        public PopUpDangKiThongBaoViewModel()
        {
            //listLap = DatabaseQueryTN.getLoaiTB();

            //XoaThongBaoCommand = new RelayCommand<LOAITHONGBAO>((p) => { return true; }, (p) =>
            //{
            //    //listLap.Remove(p);
            //});
            MappingManager mappingManager2 = new MappingManager();
            mappingManager2.AddRule(new string[] { "HastagID" }, new string[] { "ID" }, new ClassCopyOneToOne());
            mappingManager2.AddRule(new string[] { "Name" }, new string[] { "Name" }, new ClassCopyOneToOne());



            HASTAG tba = new HASTAG();
            tba.HastagID = 100;
            tba.Name = "Thong bao 100";

            THEDANGKI tbb = new THEDANGKI();

            mappingManager2.MapObjectToObject<HASTAG, THEDANGKI>(tba, tbb);
        }
    }
}