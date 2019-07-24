using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using BankerPro.Models;
using BankerPro.Views;
using BankerPro.Services;

namespace BankerPro.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private IUserService _userService = DependencyService.Get<IUserService>() ?? new UserService();
        public ObservableCollection<Item> Items { get; set; }

        private ObservableCollection<string> _values { get; set; }
        public ObservableCollection<string> values { get { return _values; } set { _values = value; } }
        public Command LoadItemsCommand { get; set; }
        public Command LoadValuesCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            values = new ObservableCollection<string>();
            LoadValues();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            LoadValuesCommand = new Command(async () => await LoadValues());

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Item;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });
        }

        async Task LoadValues()
        {
            _values.Clear();
            var data = await _userService.GetValues();

            foreach (var item in data)
            {
                _values.Add(item);
            }

        }
        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}