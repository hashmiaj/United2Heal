﻿using System;
using System.Collections.Generic;
using United2Heal.Utilities;
using United2Heal.ViewModels;
using Xamarin.Forms;

namespace United2Heal.Views
{
    public partial class MainMenu : ContentPage
    {
        MainMenuViewModel ViewModel;
        public MainMenu()
        {
            InitializeComponent();
            ViewModel = new MainMenuViewModel();
            NavigationPage.SetHasBackButton(this, false);
            Title = "United2Heal";

            if (GlobalVariables.SchoolName.Equals("VCU"))
            {
                SchoolPicture.Source = "VCULogo.jpg";
            }
            else if (GlobalVariables.SchoolName.Equals("GMU"))
            {
                SchoolPicture.Source = "GMULogo.jpg";
            }
            GroupLabel.Text = "Group: " + GlobalVariables.GroupName;
 
        }
        public async void OnLogoutClicked(object sender, EventArgs e)
        {
            GlobalVariables.SchoolName = "";
            GlobalVariables.GroupName = "";
            await Navigation.PushAsync(new ChooseSchool());
        }
    }
}
