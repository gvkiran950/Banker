using BankerPro.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace BankerPro.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {

        public Command LoginCommand { get; set; }
        public LoginViewModel()
        {
            LoginCommand = new Command(Login);
        }
        private string _userName = string.Empty;
        private string _password = string.Empty;
        public string UserName { get { return _userName; } set { SetProperty(ref _userName, value); } }
        public string Password { get { return _password; } set { SetProperty(ref _password, value); } }
        public void Login()
        {
            IsBusy = true;

            try
            {
                if (_userName == string.Empty && _password == string.Empty)
                {
                    Application.Current.MainPage.DisplayAlert("Error Info", "User name/password are required", "Ok");
                }
                else
                {
                    App.isLoggedIn = true;
                    Application.Current.MainPage = new AppShell();
                }
                    

            }
            catch (Exception ex)
            {
                // Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
