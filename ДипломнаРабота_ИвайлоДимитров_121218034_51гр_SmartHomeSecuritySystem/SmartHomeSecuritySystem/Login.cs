using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System.Data;
using System;

namespace SmartHomeSecuritySystem
{
    public partial class Login : Form
    {
  
        public Login()
        {
            InitializeComponent();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            uploadDt();
          // button1.PerformClick();
        }
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
        public void uploadDt()
        {
            List<UserActivity> list = new List<UserActivity>();
            sqlConnection.ConnectionString = "server=" + server + ";" + "user id=" + username + ";"
                + "password=" + password + ";" + "database=" + database;
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "Select * from HomeSequritySystem.useractivity";


            //sqlReader.
            //sqlData.Load(sqlReader);
            sqlReader = sqlCommand.ExecuteReader();
            while (sqlReader.Read())
            {
                UserActivity nc = new UserActivity(Convert.ToInt32(sqlReader["activityID"]), Convert.ToInt32(sqlReader["deviceID"]), Convert.ToInt32(sqlReader["userID"]), sqlReader["stateOfDevice"].ToString(), (DateTime)sqlReader["dateOfUse"]);
                list.Add(nc);
            }
            sqlReader.Close();
            sqlConnection.Close();
            //should return a list of data objects
            dataGridView1.DataSource = list;

            //List<Button> buttons = new List<Button>();
            //for (int i = 0; i < 2; i++)
            //{
            //    Button newButton = new Button();
            //    buttons.Add(newButton);
            //    this.Controls.Add(newButton);
            //}
            //foreach (DataRow row in sqlData.Rows)
            //{
            //    UserActivity nc = new UserActivity(Int32.Parse(row[0].ToString()), Int32.Parse(row[1].ToString()), Int32.Parse(row[2].ToString()), row[3].ToString(), row[4].ToString());
            //    list.Add(nc);
            //}
            //MySqlDataAdapter da = new MySqlDataAdapter(sqlCommand);
            //DataSet ds = new DataSet();
            //da.Fill(ds);

            //while (sqlReader.Read())
            //{

            //   // var data = sqlReader.GetString(0);
            //      var data = JsonConvert.DeserializeObject<UserActivity>(sqlReader["stateOfDevice"].ToString(),);
            //    list.Add(data);
            //}
            //sqlData.Load(sqlReader);
            //foreach (DataRow row in sqlData.Rows){
            //    UserActivity activity = new UserActivity(row[0].ToString, row[1].ToString, row[2].ToString, row[3].ToString, row[4].ToString);
            //    list.Add(activity);
            //}

            //  if (list.)


            //var activityList=sqlData.

        }
    

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SqlData s = new SqlData();
        }
    }
}