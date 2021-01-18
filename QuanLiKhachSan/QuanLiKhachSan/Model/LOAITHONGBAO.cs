using QuanLiKhachSan.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiKhachSan.Model
{
    public class LOAITHONGBAO : BaseViewModel
    {
        private string _id;
        public string ID
        {
            get => _id; set
            {
                OnPropertyChanged(ref _id, value);
            }
        }
        private string _name;
        public string Name
        {
            get => _name; set
            {
                OnPropertyChanged(ref _name, value);
            }
        }
    }
}
