using System;
using System.Collections.Generic;
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
        }

        private void OnVCULogoClicked(object sender, EventArgs e)
        {
            
        }

        private void OnGMULogoClicked(object sender, EventArgs e)
        {

        }
    }
}
