using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using United2Heal.Models;
using United2Heal.Utilities;

namespace United2Heal.ViewModels
{
    public class BoxStatsListViewModel
    {

        public List<string> GroupsList;

        //This is just a boolean that returns false once its done loading the data. See line 102.
        private bool loadingGroups;
        public bool LoadingGroups
        {
            get { return loadingGroups; }
            set
            {
                if (loadingGroups != value)
                {
                    loadingGroups = value;
                    OnPropertyChanged("loadingGroups");
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

        public BoxStatsListViewModel()
        {
            LoadingGroups = false;
            GroupsList = new List<string>();

        }

        //public async Task GetItemList()
        //{
        //    LoadingGroups = true;

        //    GroupsList.Clear();

        //    HttpClient client = new HttpClient();

        //    var authData = GlobalVariables.SchoolName.Equals("VCU") ? string.Format("{0}:{1}", "VCU", "united") : string.Format("{0}:{1}", "GMU", "u2hgmu");
        //    var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);

        //    Uri uri = new Uri(String.Format("https://u2h.herokuapp.com/api/v1.0/items"));

        //    var response = await client.GetAsync(uri);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        String content = await response.Content.ReadAsStringAsync();
        //        ItemResults itemResults = JsonConvert.DeserializeObject<ItemResults>(content);

        //        for (int i = 0; i < itemResults.Items.Count; i++)
        //        {
        //            GroupsList.Add(ItemResults)
        //        }

        //    }

        //    ItemsList = new List<string>(ItemsList.OrderBy(i => i));

        //    LoadingItems = false;
        //}

    }
}
