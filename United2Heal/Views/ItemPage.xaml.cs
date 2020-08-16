using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

            }


        }
    }
}
