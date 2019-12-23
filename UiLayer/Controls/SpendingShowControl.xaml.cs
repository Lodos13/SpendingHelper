using System.Windows;
using System.Windows.Controls;

namespace UiLayer.Controls
{
    /// <summary>
    /// Interaction logic for SpendingShowControl.xaml
    /// </summary>
    public partial class SpendingShowControl : UserControl
    {
        public SpendingShowControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty SelectedPaymentProperty =
            DependencyProperty.Register(
                "SelectedPayment",
                //typeof(CSubCategoryNotufyObject),
                typeof(object),
                typeof(SpendingShowControl),
                new UIPropertyMetadata(null));
        internal object SelectedPayment
        {
            get => GetValue(SelectedPaymentProperty);
            set => SetValue(SelectedPaymentProperty, value);
        }
    }
}
