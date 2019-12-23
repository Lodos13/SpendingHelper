using System;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using PropertyChanged;
using NLog;

using BusinessLogicLayer;
using Utils;

namespace UiLayer
{
    class LoginWindowVM : ViewModelBase
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

        public Action CloseAction { get; set; }

        public Boolean IsRegisterMod { get; set; }
        public String Name { get; set; }
        public String Username { get; set; }
        public String Email { get; set; }
        [DoNotNotify] public SecureString SecurePassword { private get; set; }
        [DoNotNotify] public SecureString SecurePasswordConfirmation { private get; set; }
        public Boolean IsEmailValid { get; set; }
        public Boolean IsPasswordValid { get; set; }
        public Boolean IsPasswordConfirmed { get; set; }

        public LoginWindowVM()
        {
            IsRegisterMod = false;
        }

        private CRelayCommand _loginCommand;
        private CRelayCommand _registerCommand;

        public CRelayCommand LoginCommand
        {
            get
            {
                return _loginCommand ?? (_loginCommand = new CRelayCommand((o) => 
                {
                    if(IsRegisterMod)
                    {
                        IsRegisterMod = false;
                        return;
                    }

                    CheckLoginAsync();
                }));
            }
        }
        private async void CheckLoginAsync()
        {
            try
            {
                CLoginVerificationResult res = await Task.Run(() => (new CBllFacadeForUIProxy()).VerifyPassword(Username, SecurePassword.ConvertToUnsecureString()));
                if (!res.IsVerified)
                {
                    MessageBox.Show(res.Error);
                    return;
                }

                RedirectToMainWindow(res.Token);
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to check Login in LoginWindowVM. Message: {0}", ex.Message);
                MessageBox.Show("Произошло непредвиденное исключение в CheckLoginAsync: " + ex.Message);
            }
            
        }

        public CRelayCommand RegisterCommand
        {
            get
            {
                return _registerCommand ?? (_registerCommand = new CRelayCommand((o) =>
                {
                    if (!IsRegisterMod)
                    {
                        IsRegisterMod = true;
                        return;
                    }

                    RegisterUserAsync();
                }));
            }
        }
        private async void RegisterUserAsync()
        {
            try
            {
                if (Name == null || Username == null || Email == null || SecurePassword == null || SecurePasswordConfirmation == null)
                {
                    MessageBox.Show("All fields must be filled!");
                    return;
                }

                CRegistationResult registerResult = await Task.Run(() => (new CBllFacadeForUIProxy()).RegisterPerson(Name, Username, Email, SecurePassword.ConvertToUnsecureString(), SecurePasswordConfirmation.ConvertToUnsecureString()));
                IsEmailValid = registerResult.IsEmailValidated;
                IsPasswordValid = registerResult.IsPasswordValidated;
                IsPasswordConfirmed = registerResult.IsPasswordConfirmed;

                if (!registerResult.IsRegistered)
                {
                    StringBuilder sBuilder = new StringBuilder();

                    if (!IsEmailValid)
                        sBuilder.Append("Email format is incorrect" + Environment.NewLine);

                    if (!IsPasswordValid)
                        sBuilder.Append("Password must have 1 upper character, 1 lover character, 1 number and be 6 letters or longer" + Environment.NewLine);

                    if (!IsPasswordConfirmed)
                        sBuilder.Append("Password doesn't match" + Environment.NewLine);

                    if (!registerResult.IsEmailFree)
                        sBuilder.Append("User with this Email already exist" + Environment.NewLine);

                    if (!registerResult.IsUsernameFree)
                        sBuilder.Append("User with this username already exist" + Environment.NewLine);

                    if (IsEmailValid && IsPasswordValid && IsPasswordConfirmed && registerResult.IsEmailFree && registerResult.IsUsernameFree)
                        sBuilder.Append("An unknown error has occurred");

                    MessageBox.Show(sBuilder.ToString());
                    return;
                }

                RedirectToMainWindow(registerResult.Token);
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to register Login in LoginWindowVM. Message: {0}", ex.Message);
                MessageBox.Show("Произошло непредвиденное исключение в RegisterUserAsync: " + ex.Message);
            }
        }

        private void RedirectToMainWindow(string token)
        {
            MainWindow mainWindow = new MainWindow(token);
            mainWindow.Show();

            try
            {
                CloseAction.Invoke();
            }
            catch (NullReferenceException)
            {
                log.Debug("When created LoginWindowVM CloseAction wasn't bound");
                throw;
            }
        }
    }
}
