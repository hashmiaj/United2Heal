using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.ObjectModel;
using United2Heal.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;
using United2Heal.Utilities;
using System.ComponentModel;
using System.Linq;

namespace United2Heal.ViewModels
{
    public class LoginViewModel
    {
        private List<Box> boxes;

        public ObservableCollection<String> availableGroups;

        private bool loadingGroups;
        public bool LoadingGroups
        {
            get { return loadingGroups; }
            set
            {
                if (loadingGroups != value)
                {
                    loadingGroups = value;
                    OnPropertyChanged("loadingItems");
                }
            }
        }

        public event PropertyChangingEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
            {
                return;
            }

            PropertyChanged(this, new PropertyChangingEventArgs(propertyName));
        }

        public LoginViewModel()
        {
            LoadingGroups = true;
            availableGroups = new ObservableCollection<string>();
        }

        public async Task LoadGroups()
        {
            LoadingGroups = true;

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

            HashSet<String> boxSet = new HashSet<string>();

            for (int i = 0; i < boxes.Count; i++)
            {
                boxSet.Add(boxes[i].group);
            }

            
            for (int i = 0; i < boxes.Count; i++)
            {
                if (boxSet.Contains(boxes[i].group) && boxes[i].is_open.Equals("1") && boxes[i].School.Equals(GlobalVariables.SchoolName))
                {
                    availableGroups.Add(boxes[i].group);
                }
            }

            availableGroups = new ObservableCollection<string>(availableGroups.OrderBy(i => i));

            LoadingGroups = false;
            
        }
    }
}
