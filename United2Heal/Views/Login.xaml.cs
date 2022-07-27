using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using United2Heal.Utilities;
using United2Heal.ViewModels;
using Xamarin.Forms;

namespace United2Heal.Views
{
    public partial class Login : ContentPage
    {
        LoginViewModel ViewModel;
        public Login()
        {
            InitializeComponent();
            ViewModel = new LoginViewModel();
            
            DisplayLoadingScreen();

            if (GlobalVariables.SchoolName.Equals("VCU"))
            {
                Pic.Source="VCULogo.jpg";
            }
            else if (GlobalVariables.SchoolName.Equals("GMU"))
            {
                Pic.Source = "GMULogo.jpg";
            }
            //hi saniyah was here
        }

        public async Task DisplayLoadingScreen()
        {
            LoadingIcon.IsRunning = true;
            LoadingIcon.IsVisible = true;

            LoginFrame.Opacity = 0.1;
            PictureFrame.Opacity = 0.1;
            GroupFrame.Opacity = 0.1;
            TextFrame.Opacity = 0.1;
            SubmitButton.Opacity = 0.1;

            await ViewModel.LoadGroups();
            await ViewModel.LoadPasswords();
            while (ViewModel.LoadingGroups)
            {
                await Task.Delay(100);
            }

            GroupPicker.ItemsSource = ViewModel.AvailableGroups;

            LoginFrame.Opacity = 1;
            PictureFrame.Opacity = 1;
            GroupFrame.Opacity = 1;
            TextFrame.Opacity = 1;
            SubmitButton.Opacity = 1;

            LoadingIcon.IsRunning = false;
            LoadingIcon.IsVisible = false;
        }

        public async void Submit_Clicked(Object sender, EventArgs e)
        {
            bool CorrectPass = false;
            
            if (!string.IsNullOrEmpty(PasswordBox.Text) && PasswordBox.Text.Equals(ViewModel.Password))
            {
                CorrectPass = true;
            }
                if (GroupPicker.SelectedIndex == -1)
            {
                await DisplayAlert("Select a group!", "Please select a group to continue.", "Okay");
                return;
            }

            if(CorrectPass && (GroupPicker.SelectedIndex != -1))
            {
                GlobalVariables.GroupName = ViewModel.AvailableGroups[GroupPicker.SelectedIndex];
                if (GlobalVariables.UserRole == "Volunteer")
                {
                    await Navigation.PushAsync(new MainMenu());
                }
                else
                {
                    await Navigation.PushAsync(new AdminMainMenu());
                }
            }
            else
            {
                await DisplayAlert("Incorrect password!", "Please try again!", "Okay");
            }


        }
    }
}
