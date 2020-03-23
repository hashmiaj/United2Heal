using System;
using System.Collections.Generic;
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

            
            
            if (school.Equals("VCU"))
            {
                Pic.Source="VCULogo.jpg";
            }
            else if (school.Equals("GMU"))
            {
                Pic.Source = "GMULogo.jpg";
            }

        }

        void Submit_Clicked(System.Object sender, System.EventArgs e)
        {

        }
    }

}
