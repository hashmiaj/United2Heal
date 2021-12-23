using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Rg.Plugins.Popup.Services;
using United2Heal.Models;
using United2Heal.Utilities;
using United2Heal.ViewModels;
using Xamarin.Forms;

namespace United2Heal.Views
{
    public partial class ItemPage : ContentPage
    {
        Item CurrentItem;

        ItemPageViewModel ViewModel;

        public ItemPage(Item item)
        {
            InitializeComponent();
            Title = "Item Page";
            
            CurrentItem = item;

            ItemName.Text = CurrentItem.name;
            ItemId.Text = CurrentItem.id;

            ItemName.LineBreakMode = LineBreakMode.CharacterWrap;

            ViewModel = new ItemPageViewModel();
            DisplayLoadingScreen();

            MessagingCenter.Subscribe<string>("ChangeCheck", "Yes", (sender) =>
            {
                CheckBox.Text = "\uf14a";
                CheckBox.TextColor = Color.Blue;
                RemoveButton.IsVisible = true;
            });
        }

        public async Task DisplayLoadingScreen()
        {
            LoadingIcon.IsRunning = true;
            LoadingIcon.IsVisible = true;

            NameFrame.Opacity = 0.1;
            CodeFrame.Opacity = 0.1;
            QuantFrame.Opacity = 0.1;
            ExpFrame.Opacity = 0.1;
            BoxFrame.Opacity = 0.1;
            SubmitFrame.Opacity = 0.1;

            await ViewModel.GetItemBoxList();
            while (ViewModel.LoadingBoxes)
            {
                await Task.Delay(100);
            }

            ItemBoxPicker.ItemsSource = ViewModel.ItemBoxes;

            NameFrame.Opacity = 1;
            CodeFrame.Opacity = 1;
            QuantFrame.Opacity = 1;
            ExpFrame.Opacity = 1;
            BoxFrame.Opacity = 1;
            SubmitFrame.Opacity = 1;

            LoadingIcon.IsRunning = false;
            LoadingIcon.IsVisible = false;
        }

        public async void CheckBoxClicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PopupView());
        }

        public async void RemovedBoxClicked(object sender, EventArgs e)
        {
            GlobalVariables.datePicker = new DatePicker();
            CheckBox.Text = "\uf0c8";
            CheckBox.TextColor = Color.LightGray;
            RemoveButton.IsVisible = false;
        }

        public async void SubmitClicked(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(QuantEntry.Text))
            {
                await DisplayAlert("No Quanitity Listed!", "You must enter a quantity before submitting.", "Okay");
                return;
            }

            if(ItemBoxPicker.SelectedIndex == -1)
            {
                await DisplayAlert("No Box picked!", "You must select a box before submitting.", "Okay");
                return;
            }

            string date = "";
            if(CheckBox.TextColor != Color.Blue)
            {
                date = "None";
            }
            else
            {
                date = GlobalVariables.datePicker.Date.ToString();
            }

            bool answer = await DisplayAlert("Ready to Submit?", "Are you sure you would like to add the item to this box?" + Environment.NewLine + Environment.NewLine +
                "Item Name" + Environment.NewLine + "'" + ItemName.Text + "'" + Environment.NewLine + Environment.NewLine +
                "Item Code" + Environment.NewLine + "'" + ItemId.Text + "'" + Environment.NewLine + Environment.NewLine +
                "Item Quantity" + Environment.NewLine + "'" + QuantEntry.Text + "'" + Environment.NewLine + Environment.NewLine +
                "Item Expiration Date" + Environment.NewLine + "'" + date + "'" + Environment.NewLine + Environment.NewLine +
                "Item Box" + Environment.NewLine + "'" + ItemBoxPicker.SelectedItem.ToString() + "'", "Submit", "Cancel");

            if(answer)
            {
                MySqlConnection sqlconn;
                string connsqlstring = "Server=united2heal.cxsnwexuvrto.us-east-1.rds.amazonaws.com;Port=3306;database=u2hdb;User Id=united2heal;Password=ilovevcu123;charset=utf8";
                sqlconn = new MySqlConnection();
                sqlconn.ConnectionString = connsqlstring;
                sqlconn.Open();

                MySqlCommand sqlcmd;
                //string queryName = "select ItemBoxID, GroupName, BoxNumber, School, ExpirationDate from u2hdb.ItemBox where ItemName = " + "'" + ItemName.Text + "'";
                string queryName = "select ItemBoxID, GroupName, BoxNumber, School, ExpirationDate from u2hdb.ItemBox " +
                    "where ItemName = '" + ItemName.Text + "' AND GroupName = '" + GlobalVariables.GroupName + "'" +
                    " AND BoxNumber = '" + ItemBoxPicker.SelectedItem.ToString() + "' AND ExpirationDate = '" + date + "' AND " +
                    "School = '" + GlobalVariables.SchoolName + "'";
                sqlcmd = new MySqlCommand(queryName, sqlconn);

                string result = "";
                string groupname = "";
                string boxnumber = "";
                string school = "";
                string expdate = "";

                using (MySqlDataReader reader = sqlcmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = reader.GetString(0);
                        groupname = reader.GetString(1);
                        boxnumber = reader.GetString(2);
                        school = reader.GetString(3);
                        expdate = reader.GetString(4);
                    }
                }

                if (result == null)
                {
                    string QueryMaxID = "SELECT Max(ItemBoxID + 0) from u2hdb.ItemBox";
                    sqlcmd = new MySqlCommand(QueryMaxID, sqlconn);
                    Object MaxID = sqlcmd.ExecuteScalar();
                    string GetID = MaxID.ToString();

                    int code = Int32.Parse(GetID) + 1;

                    int quantity = 0;

                    if (QuantEntry.Text.Length == 0)
                    {
                        quantity = 1;
                    }
                    else
                    {
                        string quant = QuantEntry.Text;
                        quant.Trim();
                        quantity = Int32.Parse(quant);
                    }

                    string insertQuery = "insert into u2hdb.ItemBox (ItemBoxID, ItemID, GroupName, BoxNumber, ItemName, ItemQuantity, ExpirationDate, School) values " +
                        "( '" + code.ToString() + "', '" + ItemId.Text + "', '" + GlobalVariables.GroupName + "', '" + ItemBoxPicker.SelectedItem.ToString() +
                        "', '" + ItemName.Text + "', '" + quantity + "', '" + date + "', '" + GlobalVariables.SchoolName + "')";

                    sqlcmd = new MySqlCommand(insertQuery, sqlconn);

                    sqlcmd.ExecuteNonQuery();

                    sqlconn.Close();
                }
                else if (school != GlobalVariables.SchoolName || groupname != GlobalVariables.GroupName || boxnumber != ItemBoxPicker.SelectedItem.ToString() || expdate != date)
                {
                    string QueryMaxID = "SELECT Max(ItemBoxID + 0) from u2hdb.ItemBox";
                    sqlcmd = new MySqlCommand(QueryMaxID, sqlconn);
                    Object MaxID = sqlcmd.ExecuteScalar();
                    string GetID = MaxID.ToString();

                    int code = Int32.Parse(GetID) + 1;

                    int quantity = 0;

                    if (QuantEntry.Text.Length == 0)
                    {
                        quantity = 1;
                    }
                    else
                    {
                        string quant = QuantEntry.Text;
                        quant.Trim();
                        quantity = Int32.Parse(quant);
                    }

                    string insertQuery = "insert into u2hdb.ItemBox (ItemBoxID, ItemID, GroupName, BoxNumber, ItemName, ItemQuantity, ExpirationDate, School) values " +
                        "( '" + code.ToString() + "', '" + ItemId.Text + "', '" + GlobalVariables.GroupName + "', '" + ItemBoxPicker.SelectedItem.ToString() +
                        "', '" + ItemName.Text + "', '" + quantity + "', '" + date + "', '" + GlobalVariables.SchoolName + "')";

                    sqlcmd = new MySqlCommand(insertQuery, sqlconn);

                    sqlcmd.ExecuteNonQuery();

                    sqlconn.Close();
                }

                else if (school == GlobalVariables.SchoolName && groupname == GlobalVariables.GroupName && boxnumber == ItemBoxPicker.SelectedItem.ToString() && expdate == date)
                {
                    string queryQuantity = "select ItemQuantity from u2hdb.ItemBox where ItemBoxID = " + "'" + result + "'";

                    sqlcmd = new MySqlCommand(queryQuantity, sqlconn);
                    string quantity = sqlcmd.ExecuteScalar().ToString();

                    string text = QuantEntry.Text;

                    if (text.Length == 0)
                    {
                        text = "1";
                    }

                    int quant = Int32.Parse(quantity);
                    text.Trim();

                    int current = Int32.Parse(text);

                    quant = quant + current;

                    string updateQuery = "update u2hdb.ItemBox set ItemQuantity = " + "'" + quant.ToString() + "'" +
                        " where ItemBoxID = " + "'" + result + "'";

                    sqlcmd = new MySqlCommand(updateQuery, sqlconn);
                    sqlcmd.ExecuteNonQuery();
                }

                await DisplayAlert("Done!", "You've just added the following item to a box" + Environment.NewLine + Environment.NewLine +
                "Item Name" + Environment.NewLine + "'" + ItemName.Text + "'" + Environment.NewLine + Environment.NewLine +
                "Item Code" + Environment.NewLine + "'" + ItemId.Text + "'" + Environment.NewLine + Environment.NewLine +
                "Item Quantity" + Environment.NewLine + "'" + QuantEntry.Text + "'" + Environment.NewLine + Environment.NewLine +
                "Item Expiration Date" + Environment.NewLine + "'" + date + "'" + Environment.NewLine + Environment.NewLine +
                "Item Box" + Environment.NewLine + "'" + ItemBoxPicker.SelectedItem.ToString() + "'", "Okay");

                await Navigation.PushAsync(new MainMenu());
            }


        }
    }
}
