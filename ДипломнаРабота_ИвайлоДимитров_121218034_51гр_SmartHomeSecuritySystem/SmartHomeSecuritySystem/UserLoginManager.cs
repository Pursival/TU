using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSecuritySystem
{
     public static class UserLoginManager
    {
        public static DataSet SelectAllLMSUsers(string username, string password)
        {
            SqlData data = new SqlData();
            MySqlConnection connection = data.getConnection(); 
            MySqlCommand command= new MySqlCommand();
            command.Connection = connection;
            command.CommandText = "select * from systemusers where userName = '" + username + "' and userPassword = '" + password + "' ;";
            MySqlDataAdapter da = new MySqlDataAdapter(command);

            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
    }
}
