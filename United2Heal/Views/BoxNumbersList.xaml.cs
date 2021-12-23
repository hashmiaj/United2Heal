using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using United2Heal.ViewModels;
using Xamarin.Forms;

namespace United2Heal.Views
{
    public partial class BoxNumbersList : ContentPage
    {
        BoxNumbersListViewModel ViewModel;

        private string GroupName = "";

        public BoxNumbersList(string GroupName)
        {
            InitializeComponent();

            ViewModel = new BoxNumbersListViewModel(GroupName);

            this.GroupName = GroupName;

            Title = "Select Box";
            DisplayLoadingScreen();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            GroupListView.SelectionMode = ListViewSelectionMode.Single;

        }


        public async Task DisplayLoadingScreen()
        {
            LoadingIcon.IsRunning = true;
            LoadingIcon.IsVisible = true;

            stackLayout.Opacity = 0.1;

            await ViewModel.GetBoxList();
            while (ViewModel.LoadingBoxes)
            {
                await Task.Delay(100);
            }

            GroupListView.ItemsSource = ViewModel.BoxList;

            stackLayout.Opacity = 1;

            LoadingIcon.IsRunning = false;
            LoadingIcon.IsVisible = false;
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var listView = (ListView)sender;
                string selectedBox = e.SelectedItem.ToString();
                GroupListView.SelectionMode = ListViewSelectionMode.None;
                await Navigation.PushAsync(new BoxItemList(GroupName, selectedBox));
            }
        }
    }
}
