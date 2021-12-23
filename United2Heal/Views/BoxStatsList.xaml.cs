using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using United2Heal.ViewModels;
using Xamarin.Forms;

namespace United2Heal.Views
{
    public partial class BoxStatsList : ContentPage
    {
        BoxStatsListViewModel ViewModel;

        public BoxStatsList()
        {
            InitializeComponent();
            ViewModel = new BoxStatsListViewModel();
            Title = "Select Group";

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

            await ViewModel.GetGroupsList();
            while (ViewModel.LoadingGroups)
            {
                await Task.Delay(100);
            }

            GroupListView.ItemsSource = ViewModel.GroupsList;

            stackLayout.Opacity = 1;

            LoadingIcon.IsRunning = false;
            LoadingIcon.IsVisible = false;
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var listView = (ListView)sender;
                string selectedGroup = e.SelectedItem.ToString();

                GroupListView.SelectionMode = ListViewSelectionMode.None;
                await Navigation.PushAsync(new BoxNumbersList(selectedGroup));

            }
        }
    }


}

