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
    /// Interaction logic for SingleIntForm.xaml
    /// </summary>
    public partial class SingleIntForm : Window
    {
        public Int32 Result
        {
            get
            {
                if (ResultTextBox.Text == null)
                    return -1;

                Int32 res;
                if (Int32.TryParse(ResultTextBox.Text, out res))
                    return res;
                return -1;
            }
        }

        public SingleIntForm(String helpingText)
        {
            InitializeComponent();

            HelpingTextBlock.Text = helpingText;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
