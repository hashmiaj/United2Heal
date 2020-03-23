using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.ObjectModel;
using United2Heal.Models;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace United2Heal.ViewModels
{
    public class LoginViewModel
    {

        public LoginViewModel()
        {
            LoadGroups();
        }

        public async Task LoadGroups()
        {
            HttpClient client = new HttpClient();

            var uri = new Uri(String.Format("https://u2h.herokuapp.com/api/v1.0/VCU/boxes"));

            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                String content = await response.Content.ReadAsStringAsync();
                List<Box> boxes = new List<Box>();

                BoxResults boxResults = JsonConvert.DeserializeObject<BoxResults>(content);

                for (int i = 0; i < boxResults.Boxes.Count; i++)
                {
                    boxes.Add(boxResults.Boxes[i]);
                }

            }



        }
    }
}
