using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BusinessLogicLayer;

namespace UiLayer
{
    public class CCategoryNotifyObject : ViewModelBase
    {
        private ObservableCollection<CSubCategoryNotufyObject> _subCategories;

        public String Title { get; set; }
        public ObservableCollection<CSubCategoryNotufyObject> SubCategories
        {
            get => _subCategories;
            set
            {
                _subCategories = value;
                OnPropertyChanged();
            }
        }
        public Decimal SpendedForAllTimes
        {
            get => SubCategories.Sum(s => s.Payments.Sum(p => p.Spended));
            set => OnPropertyChanged();
        }
        public Decimal SpendedThisMonth
        {
            get => SubCategories.Sum(s => s.Payments.Where(p => p.Date.Month == DateTime.Now.Month).Sum(p => p.Spended));
            set => OnPropertyChanged();
        }


        public CSubCategoryNotufyObject SelectedSubCategory { get; set; }


        public CCategoryNotifyObject(string title, ObservableCollection<CSubCategoryNotufyObject> subCategories)
        {
            Title = title;
            SubCategories = subCategories;
        }



        public void Update()
        {
            OnPropertyChanged("SpendedForAllTimes");
            OnPropertyChanged("SpendedThisMonth");
            OnPropertyChanged("SubCategories");
        }
    }
}
