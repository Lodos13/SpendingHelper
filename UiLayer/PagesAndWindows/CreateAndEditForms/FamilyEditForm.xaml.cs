using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UiLayer
{
    /// <summary>
    /// Interaction logic for FamilyEditForm.xaml
    /// </summary>
    public partial class FamilyEditForm : Window
    {
        public String FamilyName { get => FamilyNameBox.Text; }
        public Int32 Budget
        {
            get
            {
                if (BudgetBox.Text == null)
                    return 0;

                Int32 res;
                if (Int32.TryParse(BudgetBox.Text, out res))
                    return res;
                return -1;
            }
        }

        public FamilyEditForm()
        {
            InitializeComponent();
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
