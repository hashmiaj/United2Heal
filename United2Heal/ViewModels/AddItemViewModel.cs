using System;
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
            String code = "";
           

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
                var sortedList = itemResults.Items.OrderBy(x => x.id);
                int maxInt = Int32.MinValue;

                foreach(var obj in sortedList)
                {
                    int num = Int32.Parse(obj.id);
                    if (num > maxInt)
                        maxInt = num;
                }

                code = maxInt.ToString();
            }

            int itemCode = Int32.Parse(code) + 1;
            String url = "https://u2h.herokuapp.com/api/v1.0/items";
            item.id = itemCode.ToString();

            var postItem = JsonConvert.SerializeObject(item);

            StringContent postContent = new StringContent(postItem, Encoding.UTF8, "application/json");
            
            var postResponse = await client.PostAsync(uri, postContent);
        }
    }
}
