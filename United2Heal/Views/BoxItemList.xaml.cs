using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using United2Heal.Models;
using United2Heal.Utilities;
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
            if (e.SelectedItem != null)
            {
                var listView = (ListView)sender;
                string selectedItem = e.SelectedItem.ToString();
                int index = e.SelectedItemIndex;
                GroupListView.SelectionMode = ListViewSelectionMode.None;

                BoxedItem boxedItem = new BoxedItem();

                MySqlConnection sqlconn;
                string connsqlstring = "Server=united2heal.cxsnwexuvrto.us-east-1.rds.amazonaws.com;Port=3306;database=u2hdb;User Id=united2heal;Password=ilovevcu123;charset=utf8";
                sqlconn = new MySqlConnection();
                sqlconn.ConnectionString = connsqlstring;
                sqlconn.Open();

                MySqlCommand sqlcmd;
                string queryName = "select ItemBoxID, ItemID, ItemName, ItemQuantity, " +
                    "GroupName, BoxNumber, School, ExpirationDate from u2hdb.ItemBox " +
                    "where ItemName = '" + selectedItem + "' AND GroupName = '" + GroupName + "'" +
                    " AND BoxNumber = '" + BoxNumber + "' AND School = '" + GlobalVariables.SchoolName + "'";
                sqlcmd = new MySqlCommand(queryName, sqlconn);

                using (MySqlDataReader reader = sqlcmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        boxedItem.ItemID = reader.GetString(1);
                        boxedItem.ItemName = reader.GetString(2);
                        boxedItem.ItemQuantity = reader.GetString(3);
                        boxedItem.GroupName = reader.GetString(4);
                        boxedItem.BoxNumber = reader.GetString(5);
                        boxedItem.School = reader.GetString(6);
                        boxedItem.ExpirationDate = reader.GetString(7);
                    }
                }

                await Navigation.PushAsync(new BoxedItemPage(boxedItem)); 
            }

        }
    }
}
