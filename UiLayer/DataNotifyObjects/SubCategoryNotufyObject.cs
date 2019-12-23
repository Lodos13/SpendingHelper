using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BusinessLogicLayer;

namespace UiLayer
{
    public class CSubCategoryNotufyObject : ViewModelBase
    {
        public Int32 SubCategoryId {get;}
        public String Title { get; set; }
        public String Description { get; set; }
        public ObservableCollection<CPaymentNotifyObject> Payments { get; set; }
        public Decimal Sum { get => Payments.Sum(p => p.Spended); }
        public Decimal SumThisMonth
        {
            get
            {
                var v1 = Payments.Where(p => p.Date.Month == DateTime.Now.Month && p.Date.Year == DateTime.Now.Year);
                var v2 = v1.Sum(p => p.Spended);
                return v2;
            }
        }

        public CSubCategoryNotufyObject(Int32 id, String title, String description)
        {
            SubCategoryId = id;
            Title = title;
            Description = description;
            Payments = new ObservableCollection<CPaymentNotifyObject>();
        }

        public void Update()
        {
            OnPropertyChanged("Sum");
            OnPropertyChanged("SumThisMonth");
            OnPropertyChanged("Payments");
        }
    }
}