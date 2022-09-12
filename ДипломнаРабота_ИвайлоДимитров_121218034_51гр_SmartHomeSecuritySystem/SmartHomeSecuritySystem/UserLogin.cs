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
    public partial class UserLogin : Form
    {
        
        public UserLogin()
        {
            InitializeComponent();
            this.AcceptButton = button1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            int userID;
            var ds = UserLoginManager.SelectAllLMSUsers(username, password);
            if ("Ivo".Equals(username)) {
                userID = 1;
            }
            else if ("Teo".Equals(username))
            {
                userID = 2;
            }
            else
            {
                userID = 3; 
            }
            if (ds.Tables[0].Rows.Count != 0)
            {
                
                this.Hide();
                UserPanel userPanel=new UserPanel(username,userID);
                userPanel.Show();
            
            }
            else
            {
                MessageBox.Show("Wrong Username or Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void UserLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
