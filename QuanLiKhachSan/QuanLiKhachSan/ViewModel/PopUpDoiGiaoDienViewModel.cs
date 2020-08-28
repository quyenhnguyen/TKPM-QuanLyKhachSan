using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLiKhachSan.ViewModel
{
    public class PopUpDoiGiaoDienViewModel : BaseViewModel
    {
        private int index { get; set; }
        public ICommand DoiGiaoDienCommand { get; set; }
        private string _GiaoDienChon;
        public string GiaoDienChon
        {
            get => _GiaoDienChon;
            set
            {
                OnPropertyChanged(ref _GiaoDienChon, value);

            }
        }


        public PopUpDoiGiaoDienViewModel()
        {
            DoiGiaoDienCommand = new RelayCommand<UserControl>((p) => { return true; }, (p) =>
            {
                index = (p.FindName("LoaiGiaoDien") as ComboBox).SelectedIndex;
                ResourceDictionary GiaoDienTienTienResource = new ResourceDictionary() { Source = new Uri("./DictionaryResources/Styles.xaml", UriKind.RelativeOrAbsolute) };
                ResourceDictionary GiaoDienCoBanResource = new ResourceDictionary() { Source = new Uri("./DictionaryResources/BasicTheme.xaml", UriKind.RelativeOrAbsolute) };
                Collection<ResourceDictionary> QLKSResource = Application.Current.Resources.MergedDictionaries;
                //Giao diện cơ bản
                if (index == 0)
                {
                    QLKSResource.Remove(QLKSResource.Where(x => x.Source.ToString().Equals(GiaoDienTienTienResource.Source.ToString())).Single());
                    QLKSResource.Add(GiaoDienCoBanResource);
                }
                //Tiên tiến
                if (index == 1)
                {
                    QLKSResource.Remove(QLKSResource.Where(x => x.Source.ToString().Equals(GiaoDienCoBanResource.Source.ToString())).Single());
                    QLKSResource.Add(GiaoDienTienTienResource);
                }
                FrameworkElement parent = p;
                while (parent.Parent != null)
                {
                    parent = parent.Parent as FrameworkElement;
                    DialogHost dialog = parent.TemplatedParent as DialogHost;//kiểm tra có phải dialoghost
                    if (dialog != null)
                    {
                        dialog.IsOpen = false;
                        break;
                    }
                }
            });
        }
    }
}
