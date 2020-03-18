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
            
        }
    }
}
