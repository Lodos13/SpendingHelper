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
    /// Interaction logic for FamilyPage.xaml
    /// </summary>
    public partial class FamilyPage : Page
    {
        internal FamilyPage(String token, CRelayCommand logoutCommand, CRelayCommand redirectToMyPageCommand)
        {
            InitializeComponent();

            DataContext = new FamilyPageVM(token, logoutCommand,  redirectToMyPageCommand);
        }
    }
}
