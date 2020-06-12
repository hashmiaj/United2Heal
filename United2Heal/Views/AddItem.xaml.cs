using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace United2Heal.Views
{
    public partial class AddItem : ContentPage
    {
        public AddItem()
        {
            InitializeComponent();
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
                    //Post request code...

                    await DisplayAlert("Submitted!", "Item: " + ItemEntry.Text + " has been added", "Okay");
                    await Navigation.PopModalAsync();

                    //Code to navigate to the item page
                }
            }
        }
    }
}
