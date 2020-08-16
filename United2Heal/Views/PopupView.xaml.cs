using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Services;
using United2Heal.Utilities;
using Xamarin.Forms;

namespace United2Heal.Views
{
    public partial class PopupView
    {
        public PopupView()
        {
            InitializeComponent();

            datePicker.Date = GlobalVariables.datePicker.Date;
        }


        public async void SubmitButtonClicked(object sender, EventArgs e)
        {
            GlobalVariables.datePicker = datePicker;
            MessagingCenter.Send<string>("ChangeCheck", "Yes");
            await PopupNavigation.Instance.PopAllAsync();
        }

        public async void CancelButtonClicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAllAsync();
        }
    }
}
