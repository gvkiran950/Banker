using BankerPro.Models;
using BankerPro.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace BankerPro
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public List<MenuInfo> menuInfos { get; set; }
        public AppShell()
        {
            InitializeComponent();
            BindMenus();
        }

        void BindMenus()
        {
            menuInfos = new List<MenuInfo>();
            menuInfos.Add(new MenuInfo { Title="Browse",Page = new ItemsPage(),Icon= "tab_feed.png" });
            menuInfos.Add(new MenuInfo { Title = "About", Page = new AboutPage(),Icon= "tab_about.png" });

            menuInfos.Select(x => new FlyoutItem {
                Title = x.Title,
                FlyoutIcon = x.Icon,
                Items =
                {                  
                    new ShellContent
                    {
                        Content = x.Page                         
                    }
                }                
            }).ForEach(Items.Add);
        }
       
    }
}
