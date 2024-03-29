﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using United2Heal.Models;
using United2Heal.Utilities;

namespace United2Heal.ViewModels
{
    public class AddItemViewModel
    {
        List<Item> Items;
        public AddItemViewModel()
        {
            Items = new List<Item>();
        }

        public async void addItem(String itemName)
        {
            Item item = new Item();
            item.name = itemName;
           
            HttpClient client = new HttpClient();

            var authData = GlobalVariables.SchoolName.Equals("VCU") ? string.Format("{0}:{1}", "VCU", "united") : string.Format("{0}:{1}", "GMU", "u2hgmu");
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);

            Uri uri = new Uri(String.Format("https://u2h.herokuapp.com/api/v1.0/items"));

            var postItem = JsonConvert.SerializeObject(item);

            StringContent postContent = new StringContent(postItem, Encoding.UTF8, "application/json");
            
            await client.PostAsync(uri, postContent);
        }
    }
}
