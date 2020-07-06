using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using United2Heal.ViewModels;
using Xamarin.Forms;

namespace United2Heal.Views
{
    public partial class BoxItemList : ContentPage
    {
        BoxItemListViewModel ViewModel;

        private string GroupName = "";

        private string BoxNumber = "";

        public BoxItemList(string GroupName, string BoxNumber)
        {
            InitializeComponent();

            ViewModel = new BoxItemListViewModel(GroupName, BoxNumber);

            this.GroupName = GroupName;
            this.BoxNumber = BoxNumber;

            Title = "Select Box";
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

            GroupListView.ItemsSource = ViewModel.ItemListNames;

            stackLayout.Opacity = 1;

            LoadingIcon.IsRunning = false;
            LoadingIcon.IsVisible = false;
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var listView = (ListView)sender;
            string selectedItem = e.SelectedItem.ToString();

            int index = e.SelectedItemIndex;



        }
    }
}
