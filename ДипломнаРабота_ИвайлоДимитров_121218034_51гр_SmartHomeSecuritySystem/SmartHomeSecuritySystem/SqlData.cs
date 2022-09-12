using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System.Data;

namespace SmartHomeSecuritySystem
{
    public class SqlData
    {
        private MySqlConnection sqlConnection = new MySqlConnection();
        private MySqlCommand sqlCommand = new MySqlCommand();
        private DataTable sqlData = new DataTable();
        private MySqlDataReader sqlReader;
        //private string sqlQuery;
        //private MySqlDataAdapter DataAdapter = new MySqlDataAdapter();
        //private DataSet ds = new DataSet();



        private string server = "localhost";
        private string username = "root";
        private string password = "990713a";
        private string database = "HomeSequritySystem";
        public MySqlConnection getConnection()
        {
            sqlConnection.ConnectionString = "server=" + server + ";" + "user id=" + username + ";"
                 + "password=" + password + ";" + "database=" + database;
            return sqlConnection;
        }
        public MySqlCommand getConn()
        {
            sqlConnection.ConnectionString = "server=" + server + ";" + "user id=" + username + ";"
               + "password=" + password + ";" + "database=" + database;
 
            SqlData data = new SqlData();
            MySqlConnection connection = sqlConnection;
            MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            return command;
        }
    }
}
