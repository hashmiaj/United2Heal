using System;
using System.Collections.Generic;
using United2Heal.Models;
using Xamarin.Forms;

namespace United2Heal.Views
{
    public partial class BoxedItemPage : ContentPage
    {
        private BoxedItem boxedItem;

        public BoxedItemPage(BoxedItem Item)
        {
            InitializeComponent();

            boxedItem = Item;

            ItemName.Text = boxedItem.ItemName;
            ItemId.Text = boxedItem.ItemID;
            ItemQuant.Text = boxedItem.ItemQuantity;
            Expiration.Text = boxedItem.ExpirationDate;
            ItemBox.Text = boxedItem.GroupName + boxedItem.BoxNumber;
        }
    }
}
