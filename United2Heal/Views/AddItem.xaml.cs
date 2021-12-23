using System;
using System.Collections.Generic;
using United2Heal.ViewModels;
using Xamarin.Forms;

namespace United2Heal.Views
{
    public partial class AddItem : ContentPage
    {
        AddItemViewModel ViewModel;
        public AddItem()
        {
            InitializeComponent();
            ViewModel = new AddItemViewModel();
        }

        public async void Submit_Clicked(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(ItemEntry.Text))
            {
                await DisplayAlert("Blank Name!", "Please enter a name for the item.", "Okay");
            }
            else
            {
                bool answer = await DisplayAlert("Confirm", "Are you sure?", "Submit", "Cancel");

                if(answer)
                {
                    ViewModel.addItem(ItemEntry.Text);

                    await DisplayAlert("Submitted!", Environment.NewLine + ItemEntry.Text +
                        Environment.NewLine +
                        Environment.NewLine +
                        "Has been added to the list!", "Okay");

                    //Code to navigate to the item page

                    await Navigation.PopAsync();
                }
            }
        }
    }
}
