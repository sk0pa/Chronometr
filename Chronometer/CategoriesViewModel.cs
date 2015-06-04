using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronometer
{
    public class CategoriesViewModel : ViewModelBase
    {
        public CategoriesViewModel()
        {
            LoadCategories();
        }

        public ObservableCollection<Category> Categories
        {
            get
            {
                return Data.Categories;
            }
            set
            {
                Data.Categories = value;
            }
        }

        private async void LoadCategories()
        {
            Categories = await Data.LoadCategories();
            RaisePropertyChanged("Categories");
        }
    }
}
