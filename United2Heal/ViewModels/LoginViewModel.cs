using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.ObjectModel;
using United2Heal.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;

namespace United2Heal.ViewModels
{
    public class LoginViewModel
    {
        List<Box> boxes;

        public LoginViewModel()
        {
            LoadGroups();
        }

        public async Task LoadGroups()
        {
            HttpClient client = new HttpClient();

            var authData = string.Format("{0}:{1}", "VCU", "united");
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);

            var uri = new Uri(String.Format("https://u2h.herokuapp.com/api/v1.0/VCU/boxes"));

            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                String content = await response.Content.ReadAsStringAsync();
                boxes = new List<Box>();

                BoxResults boxResults = JsonConvert.DeserializeObject<BoxResults>(content);

                for (int i = 0; i < boxResults.Boxes.Count; i++)
                {
                    boxes.Add(boxResults.Boxes[i]);
                }

            }



        }
    }
}
