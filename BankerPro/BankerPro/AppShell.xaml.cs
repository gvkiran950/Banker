using BankerPro.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BankerPro
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            this.BindingContext = this;
        }

        public Command GoBakCommand => new Command(async () => await GoBack());

        public async Task GoBack()
        {
            bool res = await Shell.Current.DisplayAlert("Alert", "Do you want lagout from app", "Ok", "Cancel");
            if (res)
                Application.Current.MainPage = new LoginPage();

        }
    }
}
