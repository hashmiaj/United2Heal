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
            NavigationPage.SetHasBackButton(this, false);
            this.BackgroundColor = Color.Blue;
            InitializeComponent();
            ViewModel = new ChooseSchoolViewModel();
            Title = "United2Heal";
            GlobalVariables.UserRole = "Volunteer";
        }

        private async void OnVCULogoClicked(object sender, EventArgs e)
        {
            GlobalVariables.SchoolName = "VCU";
            await Navigation.PushAsync(new Login());
        }

        private async void OnGMULogoClicked(object sender, EventArgs e)
        {
            GlobalVariables.SchoolName = "GMU";
            await Navigation.PushAsync(new Login());
        }

        private async void OnAdminButtonClicked(object sender, EventArgs e)
        {
            if(GlobalVariables.UserRole == "Volunteer"){
                UserRoleIcon.Text = "\uf4fc";
                UserRoleIcon.TextColor = Color.Green;
                UserRoleLabel.Text = "Admin";
                GlobalVariables.UserRole = "Admin";
            }
            else
            {
                UserRoleIcon.Text = "\uf007";
                UserRoleIcon.TextColor = Color.Black;
                UserRoleLabel.Text = "Volunteer";
                GlobalVariables.UserRole = "Volunteer";
            }

        }
    }
}
