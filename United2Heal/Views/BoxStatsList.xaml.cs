using System;
using System.Collections.Generic;
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
        }

        public async Task DisplayLoadingScreen()
        {
            LoadingIcon.IsRunning = true;
            LoadingIcon.IsVisible = true;

            stackLayout.Opacity = 0.1;

            //await ViewModel.GetGroupList();
            while (ViewModel.LoadingGroups)
            {
                await Task.Delay(100);
            }

            //GroupListView.ItemsSource = ViewModel.ItemsList;

            stackLayout.Opacity = 1;

            LoadingIcon.IsRunning = false;
            LoadingIcon.IsVisible = false;
        }

        private void SearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            //var keyword = GroupSearchBar.Text;

            //GroupListView.ItemsSource = ViewModel.ItemsList.Where(itemName =>
            //    itemName.ToLower().Contains(keyword.ToLower()));
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }


}

