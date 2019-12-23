using PropertyChanged;
using System;
using System.ComponentModel;

namespace UiLayer
{
    public class CPaymentNotifyObject : ViewModelBase
    {
        public Int32 PaymentId { get; private set; }
        public DateTime Date { get; set; }
        public String Category { get; set; }
        public String SubCategory { get; set; }
        public Decimal Spended { get; set; }

        public CPaymentNotifyObject(Int32 paymentId, DateTime date, string category, string subCategory, decimal spended)
        {
            PaymentId = paymentId;
            Date = date;
            Category = category;
            SubCategory = subCategory;
            Spended = spended;
        }
    }
}
