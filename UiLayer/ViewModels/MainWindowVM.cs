using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using NLog;

using BusinessLogicLayer;
using MaterialDesignThemes.Wpf;


namespace UiLayer
{
    class MainWindowVM : ViewModelBase
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

        private readonly String _token;

        public Action CloseAction { get; set; }
        public Page CurrentPage { get; set; }


        public MainWindowVM(String token)
        {
            _token = token;

            RedirectToMyPageCommand.Execute(null);
        }


        private CRelayCommand _logoutCommand;
        public CRelayCommand LogoutCommand
        {
            get
            {
                return _logoutCommand ?? (_logoutCommand = new CRelayCommand((o) =>
                {
                    LogOutAsync();

                    Boolean isClosing = o as Boolean? ?? false;
                    if (!isClosing)
                    {
                        LoginWindow loginWindow = new LoginWindow();
                        loginWindow.Show();
                        try
                        {
                            CloseAction.Invoke();
                        }
                        catch (NullReferenceException)
                        {
                            log.Debug("When created MainWindowVM CloseAction wasn't bound");
                            #if DEBUG
                            throw;
                            #endif
                        }

                    }
                }));
            }
        }
        private async void LogOutAsync()
        {
            try
            {
                await Task.Run(() => ((new CBllFacadeForUIProxy()).LogOut(_token)));
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some Error occure when trying to log out in MainWindowVM. Message: {0}", ex.Message);
            }
        }

        private CRelayCommand _changeThemeCommand;
        public CRelayCommand ChangeThemeCommand
        {
            get
            {
                return _changeThemeCommand ?? (_changeThemeCommand = new CRelayCommand((o) =>
                {
                    try
                    {
                        Boolean isDark = o as Boolean? ?? false;

                        PaletteHelper paletteHelper = new PaletteHelper();
                        ITheme theme = paletteHelper.GetTheme();

                        theme.SetBaseTheme(isDark ? Theme.Dark : Theme.Light);
                        paletteHelper.SetTheme(theme);
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex, "Some Error occure when trying to change theme in MainWindowVM. Message: {0}", ex.Message);
                    }
                }));
            }
        }


        private CRelayCommand _redirectToMyPageCommand;
        public CRelayCommand RedirectToMyPageCommand
        {
            get
            {
                return _redirectToMyPageCommand ?? (_redirectToMyPageCommand = new CRelayCommand((o) =>
                {
                    try
                    {
                        CurrentPage = new PersonalPage(_token, LogoutCommand);
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex, "Some Error occure when trying to redirect to myPage in MainWindowVM. Message: {0}", ex.Message);
                    }
                }));
            }
        }

        private CRelayCommand _redirectToMyFamilyPageCommand;
        public CRelayCommand RedirectToMyFamilyPageCommand
        {
            get
            {
                return _redirectToMyFamilyPageCommand ?? (_redirectToMyFamilyPageCommand = new CRelayCommand((o) =>
                {
                    try
                    {
                        CurrentPage = new FamilyPage(_token, LogoutCommand, RedirectToMyPageCommand);
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex,"Some Error occure when trying to redirect to myFamilyPage in MainWindowVM. Message: {0}", ex.Message);
                    }
                }));
            }
        }

    }
}
