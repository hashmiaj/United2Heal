using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using United2Heal.Models;
using United2Heal.Utilities;
using United2Heal.ViewModels;
using Xamarin.Forms;

namespace United2Heal.Views
{
    public partial class SearchItemList : ContentPage
    {
        SearchItemListViewModel ViewModel;

        public SearchItemList()
        {
            InitializeComponent();

            ViewModel = new SearchItemListViewModel();

            DisplayLoadingScreen();
            
        }

        public async Task DisplayLoadingScreen()
        {
            LoadingIcon.IsRunning = true;
            LoadingIcon.IsVisible = true;

            stackLayout.Opacity = 0.1;

            await ViewModel.GetItemList();
            while (ViewModel.LoadingItems)
            {
                await Task.Delay(100);
            }

            ItemListView.ItemsSource = ViewModel.ItemsList;

            stackLayout.Opacity = 1;

            LoadingIcon.IsRunning = false;
            LoadingIcon.IsVisible = false;
        }

        private void SearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = ItemSearchBar.Text;

            ItemListView.ItemsSource = ViewModel.ItemsList.Where(itemName =>
                itemName.ToLower().Contains(keyword.ToLower()));
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            String itemName = (String)e.SelectedItem;
            Item item = new Item();
            item.name = itemName;

            for(int i = 0; i < ViewModel.Items.Count; i++)
            {
                if(itemName.Equals(ViewModel.Items[i].name))
                {
                    item.id = ViewModel.Items[i].id;
                }
            }

            Navigation.PushAsync(new ItemPage(item));
        }
    }
}
