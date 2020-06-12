using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using United2Heal.Models;
using United2Heal.Utilities;

namespace United2Heal.ViewModels
{
    public class SearchItemListViewModel
    {
        public List<Item> Items;

        public List<string> ItemsList;

        ////Observable Collections are just lists that make it easier to pass it along from View Model to View.
        ////This specific variable is our list of 'groups' for our Group Picker in the Login View.
        //private List<string> itemList;
        //public List<string> ItemList
        //{
        //    get { return ItemList; }
        //    set
        //    {
        //        if (itemList != value)
        //        {
        //            itemList = value;
        //            OnPropertyChanged("itemList");
        //        }
        //    }
        //}

        //This is just a boolean that returns false once its done loading the data. See line 102.
        private bool loadingItems;
        public bool LoadingItems
        {
            get { return loadingItems; }
            set
            {
                if (loadingItems != value)
                {
                    loadingItems = value;
                    OnPropertyChanged("loadingItems");
                }
            }
        }

        //Handles all the changes to any variables we declare with the OnPropertyChanged method
        public event PropertyChangingEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
            {
                return;
            }

            PropertyChanged(this, new PropertyChangingEventArgs(propertyName));
        }

        public SearchItemListViewModel()
        {
            LoadingItems = true;
            ItemsList = new List<string>();
        }

        public async Task GetItemList()
        {
            LoadingItems = true;

            ItemsList.Clear();

            HttpClient client = new HttpClient();

            var authData = GlobalVariables.SchoolName.Equals("VCU") ? string.Format("{0}:{1}", "VCU", "united") : string.Format("{0}:{1}", "GMU", "u2hgmu");
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);

            Uri uri = new Uri(String.Format("https://u2h.herokuapp.com/api/v1.0/items"));

            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                String content = await response.Content.ReadAsStringAsync();
                Items = new List<Item>();

                ItemResults itemResults = JsonConvert.DeserializeObject<ItemResults>(content);

                for (int i = 0; i < itemResults.Items.Count; i++)
                {
                    Items.Add(itemResults.Items[i]);
                    ItemsList.Add(itemResults.Items[i].name);
                }

            }

            ItemsList = new List<string>(ItemsList.OrderBy(i => i));

            LoadingItems = false;
        }
    }
}
