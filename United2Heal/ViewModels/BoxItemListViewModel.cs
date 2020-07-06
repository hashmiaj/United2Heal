using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using United2Heal.Models;
using United2Heal.Utilities;

namespace United2Heal.ViewModels
{
    public class BoxItemListViewModel
    {
        
        public List<string> ItemListNames;

        public List<BoxedItem> ItemList;

        string GroupName = "";

        string BoxNumber = "";

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
                    OnPropertyChanged("loadingBoxes");
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

        public BoxItemListViewModel(string GroupName, string BoxNumber)
        {
            this.GroupName = GroupName;
            this.BoxNumber = BoxNumber;

            LoadingItems = false;
            ItemListNames = new List<string>();
            ItemList = new List<BoxedItem>();
        }

        public async Task GetItemList()
        {
            LoadingItems = true;

            ItemListNames.Clear();
            ItemList.Clear();

            string connsqlstring = "Server=united2heal.cxsnwexuvrto.us-east-1.rds.amazonaws.com;Port=3306;database=u2hdb;User Id=united2heal;Password=ilovevcu123;charset=utf8";


            using (MySqlConnection connection = new MySqlConnection(connsqlstring))
            {
                connection.Open();
                String queryCategory = "select * from u2hdb.ItemBox where GroupName = "
                    + "'" + GroupName + "' AND BoxNumber = '" + BoxNumber + "' AND School = '" + GlobalVariables.SchoolName + "' Order by ItemName";
                using (MySqlCommand command = new MySqlCommand(queryCategory, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            String name = reader["ItemName"].ToString();
                            ItemListNames.Add(name);

                            ItemList.Add(new BoxedItem
                            {
                                ItemBoxID = reader["ItemBoxID"].ToString(),
                                ItemID = reader["ItemID"].ToString(),
                                GroupName = reader["GroupName"].ToString(),
                                BoxNumber = reader["BoxNumber"].ToString(),
                                ItemName = name,
                                ItemQuantity = reader["ItemQuantity"].ToString(),
                                ExpirationDate = reader["ExpirationDate"].ToString(),
                                School = reader["School"].ToString(),
                            });
                        }
                        reader.Close();
                    }
                }
                connection.Close();

                LoadingItems = false;
            }
        }
    }
}
