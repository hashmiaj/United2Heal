using System;
using System.Collections.Generic;
using United2Heal.Utilities;
using Xamarin.Forms;

namespace United2Heal.Views
{
    public partial class AdminMainMenu : ContentPage
    {
        public AdminMainMenu()
        {
            InitializeComponent();
            if (GlobalVariables.SchoolName.Equals("VCU"))
            {
                SchoolPicture.Source = "VCULogo.jpg";
            }
            else if (GlobalVariables.SchoolName.Equals("GMU"))
            {
                SchoolPicture.Source = "GMULogo.jpg";
            }
        }
        public async void OnOpenNewBoxClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OpenNewBoxPage());
        }
        public async void OnLogoutClicked(object sender, EventArgs e)
        {
            GlobalVariables.SchoolName = "";
            GlobalVariables.GroupName = "";
            await Navigation.PushAsync(new ChooseSchool());
        }
    }
    
}
