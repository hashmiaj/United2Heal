using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using United2Heal.ViewModels;
using Xamarin.Forms;

namespace United2Heal.Views
{
    public partial class Login : ContentPage
    {
        LoginViewModel ViewModel;
        public Login(String school)
        {
            InitializeComponent();
            ViewModel = new LoginViewModel();
            

            DisplayLoadingScreen();

            if (school.Equals("VCU"))
            {
                Pic.Source="VCULogo.jpg";
            }
            else if (school.Equals("GMU"))
            {
                Pic.Source = "GMULogo.jpg";
            }

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
            while (ViewModel.LoadingGroups)
            {
                await Task.Delay(100);
            }

            GroupPicker.ItemsSource = ViewModel.availableGroups;

            LoginFrame.Opacity = 1;
            PictureFrame.Opacity = 1;
            GroupFrame.Opacity = 1;
            TextFrame.Opacity = 1;
            SubmitButton.Opacity = 1;

            LoadingIcon.IsRunning = false;
            LoadingIcon.IsVisible = false;
        }

        void Submit_Clicked(System.Object sender, System.EventArgs e)
        {

        }
    }

}
