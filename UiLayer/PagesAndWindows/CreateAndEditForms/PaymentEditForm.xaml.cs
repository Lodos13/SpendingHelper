using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace UiLayer
{
    /// <summary>
    /// Interaction logic for PaymentEditForm.xaml
    /// </summary>
    public partial class PaymentEditForm : Window
    {
        public ObservableCollection<CCategoryNotifyObject> Categories { get; set; }

        /*
        public String Category { get => CategoryBox.Text; }
        public String SubCategory { get => SubCategoryBox.Text; }
        */

        public CCategoryNotifyObject Category { get; set; }
        public CSubCategoryNotufyObject SubCategory { get; set; }

        public DateTime? Date { get => DatePicker.SelectedDate; }
        public Decimal Sum
        {
            get
            {
                Decimal res;
                if (Decimal.TryParse(SumBox.Text, out res))
                    return res;
                return -1;
            }
        }


        public PaymentEditForm(ObservableCollection<CCategoryNotifyObject> categories, CPaymentNotifyObject payment = null)
        {
            Categories = categories;
            InitializeComponent();

            if (payment != null)
            {
                DatePicker.SelectedDate = payment.Date;
                SumBox.Text = payment.Spended.ToString();

                Category = categories.Where(c => c.Title.Equals(payment.Category)).FirstOrDefault();
                SubCategory = Category.SubCategories.Where(s => s.Title.Equals(payment.SubCategory)).FirstOrDefault();
            }

            DataContext = this;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
