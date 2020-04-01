using System;
using System.Collections.Generic;
using United2Heal.Utilities;
using United2Heal.ViewModels;
using Xamarin.Forms;

namespace United2Heal.Views
{
    public partial class ChooseSchool : ContentPage
    {
        ChooseSchoolViewModel ViewModel;

        public ChooseSchool()
        {
            InitializeComponent();
            ViewModel = new ChooseSchoolViewModel();

            Title = "United2Heal";
        }

        private async void OnVCULogoClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login("VCU"));
            GlobalVariables.SchoolName = "VCU";
        }

        private async void OnGMULogoClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login("GMU"));
            GlobalVariables.SchoolName = "GMU";
        }
    }
}
