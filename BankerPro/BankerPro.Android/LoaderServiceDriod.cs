using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BankerPro.Droid;
using BankerPro.Services;
using BankerPro.Views;
using Plugin.CurrentActivity;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XFPlatform = Xamarin.Forms.Platform.Android.Platform;

[assembly:Xamarin.Forms.Dependency(typeof(LoaderServiceDriod))]
namespace BankerPro.Droid
{
    class LoaderServiceDriod : ILoaderService
    {
        private Android.Views.View _nativeView;
        private Dialog _dialog;
        private bool _isInitialized;
        
        public void Init(ContentPage loaderPage = null)
        {
            if(loaderPage != null)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    loaderPage.Parent = Xamarin.Forms.Application.Current.MainPage;
                    loaderPage.Layout(new Rectangle(0, 0,
                        Xamarin.Forms.Application.Current.MainPage.Width,
                        Xamarin.Forms.Application.Current.MainPage.Height));

                    var renderer = loaderPage.GetOrCreateRenderer();

                    _nativeView = renderer.View;

                    _dialog = new Dialog(CrossCurrentActivity.Current.Activity);
                    _dialog.RequestWindowFeature((int)WindowFeatures.NoTitle);
                    _dialog.SetCancelable(false);
                    _dialog.SetContentView(_nativeView);
                    Window window = _dialog.Window;
                    window.SetLayout(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
                    window.ClearFlags(WindowManagerFlags.DimBehind);
                    window.SetBackgroundDrawable(new ColorDrawable(Android.Graphics.Color.Transparent));

                    _isInitialized = true;
                });
            }
        }
        private void XamFormsPage_Appearing(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var animation = new Animation(callback: d => ((ContentPage)sender).Content.Rotation = d,
                                          start: ((ContentPage)sender).Content.Rotation,
                                          end: ((ContentPage)sender).Content.Rotation + 360,
                                          easing: Easing.Linear);
                animation.Commit(((ContentPage)sender).Content, "RotationLoopAnimation", 16, 800, null, null, () => true);
            });
        }
        public void Show()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (!_isInitialized)
                    Init(new LoaderPage());

                _dialog.Show();
            });
        }
        public void Hide()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                _dialog.Hide();
            });
        }

    }

    internal static class PlatformExtension
    {
        public static IVisualElementRenderer GetOrCreateRenderer(this VisualElement bindable)
        {
            var renderer = XFPlatform.GetRenderer(bindable);
            if (renderer == null)
            {
                renderer = XFPlatform.CreateRendererWithContext(bindable, CrossCurrentActivity.Current.Activity);
                XFPlatform.SetRenderer(bindable, renderer);
            }
            return renderer;
        }
    }
}