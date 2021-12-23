using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using United2Heal.Utilities;

namespace United2Heal.ViewModels
{
    public class ItemPageViewModel
    {
        private List<string> itemBoxes;
        public List<string> ItemBoxes
        {
            get { return itemBoxes; }
            set
            {
                if (itemBoxes != value)
                {
                    itemBoxes = value;
                    OnPropertyChanged("itemBoxes");
                }
            }
        }

        private bool loadingBoxes;
        public bool LoadingBoxes
        {
            get { return loadingBoxes; }
            set
            {
                if (loadingBoxes != value)
                {
                    loadingBoxes = value;
                    OnPropertyChanged("loadingBoxes");
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

        public ItemPageViewModel()
        {
            ItemBoxes = new List<string>();
        }

        public async Task GetItemBoxList()
        {
            ItemBoxes.Clear();
            LoadingBoxes = true;
            string connsqlstring = "Server=united2heal.cxsnwexuvrto.us-east-1.rds.amazonaws.com;Port=3306;database=u2hdb;User Id=united2heal;Password=ilovevcu123;charset=utf8";

            using (MySqlConnection connection = new MySqlConnection(connsqlstring))
            {
                connection.Open();
                string queryBoxName = "select distinct BoxNumber from u2hdb.BoxTable where isOpen = 1 && GroupName = '" + GlobalVariables.GroupName + "'" +
                    " && School = '" + GlobalVariables.SchoolName + "' Order by BoxNumber + 0 ASC";

                using (MySqlCommand command = new MySqlCommand(queryBoxName, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ItemBoxes.Add(reader.GetString(0));
                        }
                        reader.Close();
                    }
                }
                connection.Close();
            }

            LoadingBoxes = false;
        }
    }
}
