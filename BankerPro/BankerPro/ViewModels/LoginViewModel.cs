using BankerPro.Models;
using BankerPro.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using Xamarin.Essentials;
using BankerPro.Views;
using System.Threading.Tasks;

namespace BankerPro.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {

        private IUserService _userService = DependencyService.Get<IUserService>() ?? new UserService();
        public Command LoginCommand { get; }
        public LoginViewModel()
        {
            LoginCommand = new Command(async () => await Login(),() => !IsBusy);
        }

        private string _userName = string.Empty;
        private string _password = string.Empty;
        public string UserName { get { return _userName; } set { SetProperty(ref _userName, value); } }
        public string Password { get { return _password; } set { SetProperty(ref _password, value); } }
        public async Task Login()
        {           
            try
            {
                if (_userName == string.Empty && _password == string.Empty)
                {
                    await Application.Current.MainPage.DisplayAlert("Error Info", "User name/password are required", "Ok");
                }
                else
                {
                    IsBusy = true;
                    await Task.Delay(500);
                    User user = new User
                    {
                        UserName = _userName,
                        Password = _password
                    };

                    var response = _userService.Login(user);
                    await SecureStorage.SetAsync("auth_token", "Bearer " + response);                   
                    Application.Current.MainPage = new AppShell();
                }


            }
            catch (Exception)
            {
                IsBusy = false;
            }
            finally
            {                
                IsBusy = false;
            }
        }
    }
}
