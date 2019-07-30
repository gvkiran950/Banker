using BankerPro.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BankerPro.Services
{
    public class Loader 
    {
        public static void Hide()
        {
            Device.BeginInvokeOnMainThread(() => { DependencyService.Get<ILoaderService>().Hide();
            });
        }

        public static void Show()
        {
            Device.BeginInvokeOnMainThread(() => {
                DependencyService.Get<ILoaderService>().Init(new LoaderPage());
                DependencyService.Get<ILoaderService>().Show();
            });
        }
    }
}
