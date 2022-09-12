using MySql.Data.MySqlClient;
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
    public partial class UpdatingDeviceLayout : Form
    {
        public UpdatingDeviceLayout()
        {
            InitializeComponent();
        }

        private void dreamButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(bigTextBox1.Text)  || string.IsNullOrEmpty(bigTextBox2.Text) || string.IsNullOrEmpty(bigTextBox3.Text))
            {
                MessageBox.Show("No values typed in all fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                Devices device = new();
                device.DeviceType = bigTextBox1.Text.ToString();
                device.DeviceBrand = bigTextBox2.Text.ToString();
                device.DeviceLocation = bigTextBox3.Text.ToString();
                MySqlDataReader dr;
                SqlData data = new SqlData();
                MySqlConnection connection = data.getConnection();
                MySqlCommand command = new MySqlCommand("insert into devices(deviceType, deviceBrand, deviceLocation) values(@deviceType, @deviceBrand, @deviceLocation)", connection);
                command.Parameters.AddWithValue("@deviceType", device.DeviceType);
                command.Parameters.AddWithValue("@deviceBrand", device.DeviceBrand);
                command.Parameters.AddWithValue("@deviceLocation", device.DeviceLocation);

                connection.Open();
                dr = command.ExecuteReader();
                connection.Close();
                this.Close();
            }
        }
    }
}
