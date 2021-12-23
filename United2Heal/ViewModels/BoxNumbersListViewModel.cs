using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using United2Heal.Utilities;

namespace United2Heal.ViewModels
{
    public class BoxNumbersListViewModel
    {

        public List<string> BoxList;

        string GroupName = "";

        //This is just a boolean that returns false once its done loading the data. See line 102.
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

        public BoxNumbersListViewModel(string GroupName)
        {
            this.GroupName = GroupName;
            LoadingBoxes = false;
            BoxList = new List<string>();
        }

        public async Task GetBoxList()
        {
            LoadingBoxes = true;
            BoxList.Clear();

            string connsqlstring = "Server=united2heal.cxsnwexuvrto.us-east-1.rds.amazonaws.com;Port=3306;database=u2hdb;User Id=united2heal;Password=ilovevcu123;charset=utf8";


            using (MySqlConnection connection = new MySqlConnection(connsqlstring))
            {
                connection.Open();
                String queryCategory = "select distinct BoxNumber from u2hdb.ItemBox where GroupName = "
                    + "'" + GroupName + "' && School = '" + GlobalVariables.SchoolName + "' ORDER BY BoxNumber + 0 ASC";
                using (MySqlCommand command = new MySqlCommand(queryCategory, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BoxList.Add(reader["BoxNumber"].ToString());
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
