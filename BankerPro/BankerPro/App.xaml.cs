using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BankerPro.Services;
using BankerPro.Views;

namespace BankerPro
{
    public partial class App : Application
    {
       public static bool isLoggedIn = false;
        public static string BaseUrl = "http://172.17.240.65/";
        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<UserService>();

            if (isLoggedIn)
                MainPage = new AppShell();
            else
                MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
