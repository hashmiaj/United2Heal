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
using MySql.Data.MySqlClient;

namespace United2Heal.ViewModels
{
    public class LoginViewModel
    {
        private List<Box> boxes;

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged("password");
                }
            }
        }
        //Observable Collections are just lists that make it easier to pass it along from View Model to View.
        //This specific variable is our list of 'groups' for our Group Picker in the Login View.
        private List<string> availableGroups;
        public List<string> AvailableGroups
        {
            get { return availableGroups; }
            set
            {
                if (availableGroups != value)
                {
                    availableGroups = value;
                    OnPropertyChanged("availableGroups");
                }
            }
        }

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



        /// <summary>
        /// Default constructor.
        /// </summary>
        public LoginViewModel()
        {
            LoadingGroups = true;
            AvailableGroups = new List<string>();
        }

        /// <summary>
        /// Async method that loads data from our API get request. Converts data to List of Box objects (currently). 
        /// </summary>
        /// <returns></returns>
        public async Task LoadGroups()
        {
            LoadingGroups = true;
            string connectionstring = "Server=united2heal.cxsnwexuvrto.us-east-1.rds.amazonaws.com;Port=3306;database=u2hdb;User Id=united2heal;Password=ilovevcu123;charset=utf8";
            using (MySqlConnection connection = new MySqlConnection(connectionstring))
            {
                connection.Open();
                String queryGroup = "select distinct GroupName from u2hdb.BoxTable where School = '" + GlobalVariables.SchoolName + "' and isOpen = " + 1 + " Order by GroupName";
                using (MySqlCommand command = new MySqlCommand(queryGroup, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AvailableGroups.Add(reader["GroupName"].ToString()); ;
                        }
                        reader.Close();
                    }
                }
                connection.Close();
            }
            LoadingGroups = false;

        }
        public async Task LoadPasswords()
        {
            string connsqlstring = "Server=united2heal.cxsnwexuvrto.us-east-1.rds.amazonaws.com;Port=3306;database=u2hdb;User Id=united2heal;Password=ilovevcu123;charset=utf8";

            using (MySqlConnection connection = new MySqlConnection(connsqlstring))
            {
                connection.Open();
                string queryPassword = "select Password from u2hdb.PasswordTable where School = '" + GlobalVariables.SchoolName + "' " +
                    "and Role = '" + GlobalVariables.UserRole + "'";

                using (MySqlCommand command = new MySqlCommand(queryPassword, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Password = reader.GetString(0);
                        }
                        reader.Close();
                    }
                }
                connection.Close();
            }

        }
    }
}
