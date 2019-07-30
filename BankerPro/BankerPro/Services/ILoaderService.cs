using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BankerPro.Services
{
   public interface ILoaderService
    {
        void Init(ContentPage loaderPage = null);
        void Show();
        void Hide();
    }
}
