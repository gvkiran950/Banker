using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankerPro.iOS;
using BankerPro.Services;
using BankerPro.Views;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XFPlatform = Xamarin.Forms.Platform.iOS.Platform;

[assembly:Xamarin.Forms.Dependency(typeof(LoaderServiceiOS))]
namespace BankerPro.iOS
{
    class LoaderServiceiOS : ILoaderService
    {
        private UIView _nativeView;
        private bool _isInitialized;
        public void Hide()
        {
            // Hide the page
            _nativeView.RemoveFromSuperview();
        }

        public void Init(ContentPage loaderPage = null)
        {
            // check if the page parameter is available
            if (loaderPage != null)
            {
                // build the loading page with native base
                loaderPage.Parent = Xamarin.Forms.Application.Current.MainPage;

                loaderPage.Layout(new Rectangle(0, 0,
                    Xamarin.Forms.Application.Current.MainPage.Width,
                    Xamarin.Forms.Application.Current.MainPage.Height));

                var renderer = loaderPage.GetOrCreateRenderer();

                _nativeView = renderer.NativeView;

                _isInitialized = true;
            }
        }

        public void Show()
        {
            // check if the user has set the page or not
            if (!_isInitialized)
                Init(new LoaderPage()); // set the default page

            // showing the native loading page
            UIApplication.SharedApplication.KeyWindow.AddSubview(_nativeView);
        }
    }

    internal static class PlatformExtension
    {
        public static IVisualElementRenderer GetOrCreateRenderer(this VisualElement bindable)
        {
            var renderer = XFPlatform.GetRenderer(bindable);

            if (renderer == null)
            {
                renderer = XFPlatform.CreateRenderer(bindable);
                XFPlatform.SetRenderer(bindable, renderer);
            }

            return renderer;
        }
    }
}