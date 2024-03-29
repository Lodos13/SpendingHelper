﻿using System;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            LoginWindowVM vm = new LoginWindowVM();
            DataContext = vm;
            if (vm.CloseAction == null)
                vm.CloseAction = new Action(this.Close);
        }

        private void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
                ((LoginWindowVM)this.DataContext).SecurePassword = ((PasswordBox)sender).SecurePassword;
        }

        private void PasswordRepeat_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
                ((LoginWindowVM)this.DataContext).SecurePasswordConfirmation = ((PasswordBox)sender).SecurePassword;
        }
    }
}
