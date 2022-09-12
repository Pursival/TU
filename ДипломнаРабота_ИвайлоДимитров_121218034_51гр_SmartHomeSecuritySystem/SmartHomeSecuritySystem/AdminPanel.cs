using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartHomeSecuritySystem
{
    public partial class AdminPanel : Form
    {
        public AdminPanel()
        {
            InitializeComponent();
        }

        private void AdminPanel_Load(object sender, EventArgs e)
        {

        }

        private void thunderLabel7_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlData data = new SqlData();
            MySqlConnection connection = data.getConnection();
            MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            command.CommandText = "select * from systemusers;";
            MySqlDataAdapter da = new MySqlDataAdapter(command);

            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView2.DataSource=ds;

            dataGridView2.FirstDisplayedScrollingRowIndex = dataGridView2.RowCount - 1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlData data = new SqlData();
            MySqlConnection connection = data.getConnection();
            MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            command.CommandText = "select * from useractivity;";
            MySqlDataAdapter da = new MySqlDataAdapter(command);

            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView2.DataSource = ds;
            dataGridView2.FirstDisplayedScrollingRowIndex = dataGridView2.RowCount - 1;
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}

