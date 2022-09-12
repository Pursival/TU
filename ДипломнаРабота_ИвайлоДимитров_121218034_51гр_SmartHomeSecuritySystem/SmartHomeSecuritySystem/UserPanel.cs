using MySql.Data.MySqlClient;
using MySql.Data.Types;
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
    public partial class UserPanel : Form
    {
        private int userID;
        private string[] state = new string[] {"On", "On" , "On" , "On" , "On" , "On" , "On" , "On" };
        // private ImageList imagelst;//
        private string username;
        List<Bitmap> data = new List<Bitmap>();
        private int[] deviceIDs = new int[] { 1, 2, 3 };
        UserActivity userActivity = new UserActivity();
        List<UserActivity> list = new List<UserActivity>();

        List<UserActivity> autoActivitiesList = new();
        public UserPanel(string username, int userId)
        {
            this.userID = userId;
            this.username = username;
            InitializeComponent();

            InitializeBackgroundWorker();
            // imagelst = new ImageList();

        }
        private void InitializeBackgroundWorker()
        {
            backgroundWorker1.DoWork +=
                new DoWorkEventHandler(backgroundWorker1_DoWork);

        }

        private void UserPanel_Load(object sender, EventArgs e)
        {

            userActivity.UserID = userID;
            headerLabel1.Text = username.ToString();
            dateTimePicker1.Hide();
            dateTimePicker2.Hide();
            label1.Hide();
            label2.Hide();
            button4.Hide();

            this.lostCancelButton1.Enabled = false;
            this.lostAcceptButton1.Enabled = false;
            //label1.Text = username.ToString();
            if (username != "Ivo")
            {
                button2.Hide();
            }
            //data[0] = new Bitmap(@"C:\Users\crazzyninja\source\repos\SmartHomeSecuritySystem\SmartHomeSecuritySystem\Ui pictures\LightOnn.png");
            //imagelst.Images.Add("LightOnn", i);

            // data[1] = new Bitmap(@"C:\Users\crazzyninja\source\repos\SmartHomeSecuritySystem\SmartHomeSecuritySystem\Ui pictures\LightOff.png");
            // imagelst.Images.Add("LightOff", i);

        }

        
        private void writeActivityToDB(UserActivity activity)
        {
            //string readCommand = "insert into useractivity(deviceID, userID, stateOfDevice, dateOfUse) values(@deviceID, @userID, @stateOfDevice, @dateOfUse)";
            MySqlDataReader dr;
            SqlData data = new SqlData();
            MySqlConnection connection = data.getConnection();
            MySqlCommand command = new MySqlCommand("insert into useractivity(deviceID, userID, stateOfDevice, dateOfUse) values(@deviceID, @userID, @stateOfDevice, @dateOfUse)", connection);
            // command.Connection = connection;
            // command.CommandText= "insert into useractivity(deviceID, userID, stateOfDevice, dateOfUse) values('"+activity.DeviceID+ "','" +activity.UserID + "','" +activity.StateOfDevice + "','" +activity.DateOfUse + "'); ";
            MySqlParameter param;
            //param = new MySqlParameter("@deviceID", MySqlDbType.Int64);
            //param.Value = activity.DateOfUse;
            command.Parameters.AddWithValue("@deviceID", activity.DeviceID);
            // param = new MySqlParameter("@userID", MySqlDbType.Int64);
            // param.Value = activity.DateOfUse;
            command.Parameters.AddWithValue("@userID", activity.UserID);
            //param = new MySqlParameter("@stateOfDevice", MySqlDbType.VarChar);
            // param.Value = activity.DateOfUse;
            command.Parameters.AddWithValue("@stateOfDevice", activity.StateOfDevice);
            param = new MySqlParameter("@dateOfUse", MySqlDbType.DateTime);
            param.Value = activity.DateOfUse;
            command.Parameters.Add(param);

            connection.Open();
            dr = command.ExecuteReader();
            connection.Close();
        }
        private void UserPanel_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            MySqlDataReader sqlReader;
            SqlData data = new SqlData();
            MySqlCommand sqlCommand = data.getConn();
            sqlCommand.Connection.Open();
            sqlCommand.CommandText = "select * from UserActivity where userID = '" + userID + "';";
            sqlReader = sqlCommand.ExecuteReader();
            while (sqlReader.Read())
            {
                UserActivity nc = new UserActivity(Convert.ToInt32(sqlReader["activityID"]), Convert.ToInt32(sqlReader["deviceID"]), Convert.ToInt32(sqlReader["userID"]), sqlReader["stateOfDevice"].ToString(), (DateTime)sqlReader["dateOfUse"]);
                list.Add(nc);
            }
            sqlReader.Close();
            sqlCommand.Connection.Close();
            dateTimePicker1.Show();
            dateTimePicker2.Show();
            label1.Show();
            label2.Show();
            button4.Show();
            dateTimePicker1.MinDate = list.First().DateOfUse;
            dateTimePicker2.MinDate = list.First().DateOfUse;
            dateTimePicker1.MaxDate = list.Last().DateOfUse;
            dateTimePicker2.MaxDate = list.Last().DateOfUse;







            // activities=ds.
            // univesal method to connect to db
            // methods to crud
            //method to collect all dates with current user
            //and in main method button click call new method to start replaying activity async
            //should be a button to stop it green/red
            //select start and end date which repeats untill stopped
            //
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

      

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserLogin userLogin = new UserLogin();
            userLogin.Show();
        }


        private void button4_Click(object sender, EventArgs e)
        {
            DateTime first = new DateTime();
            DateTime last = new DateTime();
            first = dateTimePicker1.Value;
            last = dateTimePicker2.Value;
            dateTimePicker1.Hide();
            dateTimePicker2.Hide();
            label1.Hide();
            label2.Hide();
            button4.Hide();
            autoActivitiesList =
                (from activity in list
                 where activity.DateOfUse >= first && activity.DateOfUse <= last
                 select activity).ToList();

            this.lostAcceptButton1.Enabled = true;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            autoSecuritySystemCycle();
        }




        private void lostAcceptButton1_Click(object sender, EventArgs e)
        {
            this.lostAcceptButton1.Enabled = false;
            this.button1.Enabled = false;
            this.lostCancelButton1.Enabled = true;
            backgroundWorker1.RunWorkerAsync(autoActivitiesList);
        }

        private void lostCancelButton1_Click(object sender, EventArgs e)
        {
            this.backgroundWorker1.CancelAsync();

            this.lostCancelButton1.Enabled = false;

            this.lostAcceptButton1.Enabled = true;
            this.button1.Enabled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var frm = new UpdatingDeviceLayout();
            frm.Location = this.Location;
            frm.StartPosition = FormStartPosition.Manual;
            frm.FormClosing += delegate { this.Show(); };
            frm.Show();
            this.Hide();

        }
        private void autoSecuritySystemCycle()
        {
            int deviceID;
            do {
                foreach (var act in autoActivitiesList)
                {
                    deviceID = act.DeviceID;
                    var span = (act.DateOfUse.Hour - DateTime.Now.Hour) * 360000 + (act.DateOfUse.Minute - DateTime.Now.Minute) * 60000 + (act.DateOfUse.Second - DateTime.Now.Second) * 1000;
                    if (span <= 0)
                    {
                        span = 0;
                    }
                    else
                    {
                        Task.Delay(span);
                    }

                    switch (deviceID)
                    {
                        case 0:
                            continue;
                        case 1:
                            tvDevice(act.StateOfDevice);
                            break;
                        case 2:
                            soundSystem(act.StateOfDevice);
                            break;
                        case 3:
                            closetLamp(act.StateOfDevice);
                            break;
                        case 4:
                            bathLamp(act.StateOfDevice);
                            break;
                        case 5:
                            bedroomLamp(act.StateOfDevice);
                            break;
                        case 6:
                            terraceLamp(act.StateOfDevice);
                            break;
                        case 7:
                            kitchenLamp(act.StateOfDevice);
                            break;
                        case 8:
                            livingroomLamp(act.StateOfDevice);
                            break;
                        default:
                            break;
                    }
                }
            } while (this.lostCancelButton1.Enabled == true);
            
            // 7 switch cases and state=state at the record then call buttonclick func  
        }

        private void bathLamp(string state)
        {
            if (state.Equals("On"))
            {
                
                pictureBox1.Image = Image.FromFile(@"C:\Users\crazzyninja\source\repos\SmartHomeSecuritySystem\SmartHomeSecuritySystem\Ui pictures\LightOnn.png");
            }
            else
            {
                pictureBox1.Image = Image.FromFile(@"C:\Users\crazzyninja\source\repos\SmartHomeSecuritySystem\SmartHomeSecuritySystem\Ui pictures\LightOff.png");
            }
        }
        private void livingroomLamp(string state)
        {
            if (state.Equals("On"))
            {
                pictureBox5.Image = Image.FromFile(@"C:\Users\crazzyninja\source\repos\SmartHomeSecuritySystem\SmartHomeSecuritySystem\Ui pictures\LightOnn.png");

            }
            else
            {
                pictureBox5.Image = Image.FromFile(@"C:\Users\crazzyninja\source\repos\SmartHomeSecuritySystem\SmartHomeSecuritySystem\Ui pictures\LightOff.png");
                
            }
        }
        private void closetLamp(string state)
        {
            if (state.Equals("On"))
            {
                pictureBox3.Image = Image.FromFile(@"C:\Users\crazzyninja\source\repos\SmartHomeSecuritySystem\SmartHomeSecuritySystem\Ui pictures\LightOnn.png");
            }
            else
            {
                pictureBox3.Image = Image.FromFile(@"C:\Users\crazzyninja\source\repos\SmartHomeSecuritySystem\SmartHomeSecuritySystem\Ui pictures\LightOff.png");
                 }
        }
        private void terraceLamp(string state)
        {
            if (state.Equals("On"))
            {
                hopePictureBox1.Image = Image.FromFile(@"C:\Users\crazzyninja\source\repos\SmartHomeSecuritySystem\SmartHomeSecuritySystem\Ui pictures\LightOnn.png");
            }
            else
            {
                hopePictureBox1.Image = Image.FromFile(@"C:\Users\crazzyninja\source\repos\SmartHomeSecuritySystem\SmartHomeSecuritySystem\Ui pictures\LightOff.png");
                }
        }
        private void kitchenLamp(string state)
        {
            if (state.Equals("On"))
            {

                pictureBox8.Image = Image.FromFile(@"C:\Users\crazzyninja\source\repos\SmartHomeSecuritySystem\SmartHomeSecuritySystem\Ui pictures\LightOnn.png");
            }
            else
            {
                pictureBox8.Image = Image.FromFile(@"C:\Users\crazzyninja\source\repos\SmartHomeSecuritySystem\SmartHomeSecuritySystem\Ui pictures\LightOff.png");
            }
        }
        private void bedroomLamp(string state)
        {
            if (state.Equals("On"))
            {
                pictureBox4.Image = Image.FromFile(@"C:\Users\crazzyninja\source\repos\SmartHomeSecuritySystem\SmartHomeSecuritySystem\Ui pictures\LightOnn.png");
            }
            else
            {
                pictureBox4.Image = Image.FromFile(@"C:\Users\crazzyninja\source\repos\SmartHomeSecuritySystem\SmartHomeSecuritySystem\Ui pictures\LightOff.png");
            }
        }

        private void tvDevice(string state)
        {
            if (state.Equals("On"))
            {
                pictureBox6.Image = Image.FromFile(@"C:\Users\crazzyninja\source\repos\SmartHomeSecuritySystem\SmartHomeSecuritySystem\Ui pictures\TVon.png");

            }
            else
            {
                pictureBox6.Image = Image.FromFile(@"C:\Users\crazzyninja\source\repos\SmartHomeSecuritySystem\SmartHomeSecuritySystem\Ui pictures\TVoff.png");
            }
        }
        private void soundSystem(string state)
        {
            if (state.Equals("On"))
            {
                pictureBox7.Image = Image.FromFile(@"C:\Users\crazzyninja\source\repos\SmartHomeSecuritySystem\SmartHomeSecuritySystem\Ui pictures\SpeakerOn2.png");

            }
            else
            {
                pictureBox7.Image = Image.FromFile(@"C:\Users\crazzyninja\source\repos\SmartHomeSecuritySystem\SmartHomeSecuritySystem\Ui pictures\SpeakerOff2.png");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var frm = new AdminPanel();
            frm.Location = this.Location;
            frm.StartPosition = FormStartPosition.Manual;
            frm.FormClosing += delegate { this.Show(); };
            frm.Show();
            this.Hide();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {//bathroom
            userActivity.DeviceID = 4;
            if (state[0].Equals("On"))
            {
                state[0] = "Off";
                pictureBox1.Image = Image.FromFile(@"C:\Users\crazzyninja\source\repos\SmartHomeSecuritySystem\SmartHomeSecuritySystem\Ui pictures\LightOff.png");//save data to sql with current device and user
                userActivity.DateOfUse = DateTime.Now;
                userActivity.StateOfDevice = state[0];
                writeActivityToDB(userActivity);
                //директно да пише в базата 
            }
            else
            {
                DateTime dateNow = DateTime.Now;
                state[0] = "On";
                pictureBox1.Image = Image.FromFile(@"C:\Users\crazzyninja\source\repos\SmartHomeSecuritySystem\SmartHomeSecuritySystem\Ui pictures\LightOnn.png");//save data to sql with current device and user
                DateTime date = DateTime.Now;
                // string dateNow=date.ToString("yyyy-MM-dd hh:mm:ss");
                userActivity.DateOfUse = date;
                userActivity.StateOfDevice = state[0];
                writeActivityToDB(userActivity);
            }
        }
        private void hopePictureBox1_MouseDown(object sender, MouseEventArgs e)
        {//terrace
            userActivity.DeviceID = 6;
            if (state[1].Equals("On"))
            {
                state[1] = "Off";
                hopePictureBox1.Image = Image.FromFile(@"C:\Users\crazzyninja\source\repos\SmartHomeSecuritySystem\SmartHomeSecuritySystem\Ui pictures\LightOff.png");//save data to sql with current device and user
                userActivity.DateOfUse = DateTime.Now;
                userActivity.StateOfDevice = state[1];
                writeActivityToDB(userActivity);
                //директно да пише в базата 
            }
            else
            {
                DateTime dateNow = DateTime.Now;
                state[1] = "On";
                hopePictureBox1.Image = Image.FromFile(@"C:\Users\crazzyninja\source\repos\SmartHomeSecuritySystem\SmartHomeSecuritySystem\Ui pictures\LightOnn.png");//save data to sql with current device and user
                DateTime date = DateTime.Now;
                // string dateNow=date.ToString("yyyy-MM-dd hh:mm:ss");
                userActivity.DateOfUse = date;
                userActivity.StateOfDevice = state[1];
                writeActivityToDB(userActivity);
            }
        }
        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {//closet
            userActivity.DeviceID = 3;
            if (state[2].Equals("On"))
            {
                state[2] = "Off";
                pictureBox3.Image = Image.FromFile(@"C:\Users\crazzyninja\source\repos\SmartHomeSecuritySystem\SmartHomeSecuritySystem\Ui pictures\LightOff.png");//save data to sql with current device and user
                userActivity.DateOfUse = DateTime.Now;
                userActivity.StateOfDevice = state[2];
                writeActivityToDB(userActivity);
                //директно да пише в базата 
            }
            else
            {
                DateTime dateNow = DateTime.Now;
                state[2] = "On";
                pictureBox3.Image = Image.FromFile(@"C:\Users\crazzyninja\source\repos\SmartHomeSecuritySystem\SmartHomeSecuritySystem\Ui pictures\LightOnn.png");//save data to sql with current device and user
                DateTime date = DateTime.Now;
                // string dateNow=date.ToString("yyyy-MM-dd hh:mm:ss");
                userActivity.DateOfUse = date;
                userActivity.StateOfDevice = state[2];
                writeActivityToDB(userActivity);
            }
        }
        private void pictureBox4_MouseDown(object sender, MouseEventArgs e)
        {//bedroom
            userActivity.DeviceID = 5;
            if (state[3].Equals("On"))
            {
                state[3] = "Off";
                pictureBox4.Image = Image.FromFile(@"C:\Users\crazzyninja\source\repos\SmartHomeSecuritySystem\SmartHomeSecuritySystem\Ui pictures\LightOff.png");//save data to sql with current device and user
                userActivity.DateOfUse = DateTime.Now;
                userActivity.StateOfDevice = state[3];
                writeActivityToDB(userActivity);
                //директно да пише в базата 
            }
            else
            {
                DateTime dateNow = DateTime.Now;
                state[3] = "On";
                pictureBox4.Image = Image.FromFile(@"C:\Users\crazzyninja\source\repos\SmartHomeSecuritySystem\SmartHomeSecuritySystem\Ui pictures\LightOnn.png");//save data to sql with current device and user
                DateTime date = DateTime.Now;
                // string dateNow=date.ToString("yyyy-MM-dd hh:mm:ss");
                userActivity.DateOfUse = date;
                userActivity.StateOfDevice = state[3];
                writeActivityToDB(userActivity);
            }
        }
        private void pictureBox5_MouseDown(object sender, MouseEventArgs e)
        {//livingroom
            userActivity.DeviceID = 8;
            if (state[4].Equals("On"))
            {
                state[4] = "Off";
                pictureBox5.Image = Image.FromFile(@"C:\Users\crazzyninja\source\repos\SmartHomeSecuritySystem\SmartHomeSecuritySystem\Ui pictures\LightOff.png");//save data to sql with current device and user
                userActivity.DateOfUse = DateTime.Now;
                userActivity.StateOfDevice = state[4];
                writeActivityToDB(userActivity);
                //директно да пише в базата 
            }
            else
            {
                DateTime dateNow = DateTime.Now;
                state[4] = "On";
                pictureBox5.Image = Image.FromFile(@"C:\Users\crazzyninja\source\repos\SmartHomeSecuritySystem\SmartHomeSecuritySystem\Ui pictures\LightOnn.png");//save data to sql with current device and user
                DateTime date = DateTime.Now;
                // string dateNow=date.ToString("yyyy-MM-dd hh:mm:ss");
                userActivity.DateOfUse = date;
                userActivity.StateOfDevice = state[4];
                writeActivityToDB(userActivity);
            }

        }

        private void pictureBox6_MouseDown(object sender, MouseEventArgs e)
        {//tv
            userActivity.DeviceID = 1;
            if (state[5].Equals("On"))
            {
                state[5] = "Off";
                pictureBox6.Image = Image.FromFile(@"C:\Users\crazzyninja\source\repos\SmartHomeSecuritySystem\SmartHomeSecuritySystem\Ui pictures\TVoff.png");//save data to sql with current device and user
                userActivity.DateOfUse = DateTime.Now;
                userActivity.StateOfDevice = state[5];
                writeActivityToDB(userActivity);
                //директно да пише в базата 
            }
            else
            {
                DateTime dateNow = DateTime.Now;
                state[5] = "On";
                pictureBox6.Image = Image.FromFile(@"C:\Users\crazzyninja\source\repos\SmartHomeSecuritySystem\SmartHomeSecuritySystem\Ui pictures\TVon.png");//save data to sql with current device and user
                DateTime date = DateTime.Now;
                // string dateNow=date.ToString("yyyy-MM-dd hh:mm:ss");
                userActivity.DateOfUse = date;
                userActivity.StateOfDevice = state[5];
                writeActivityToDB(userActivity);
            }

        }

        private void pictureBox7_MouseDown(object sender, MouseEventArgs e)
        {//audiosystem
            userActivity.DeviceID = 2;
            if (state[6].Equals("On"))
            {
                state[6] = "Off";
                pictureBox7.Image = Image.FromFile(@"C:\Users\crazzyninja\source\repos\SmartHomeSecuritySystem\SmartHomeSecuritySystem\Ui pictures\SpeakerOff2.png");//save data to sql with current device and user
                userActivity.DateOfUse = DateTime.Now;
                userActivity.StateOfDevice = state[6];
                writeActivityToDB(userActivity);
                //директно да пише в базата 
            }
            else
            {
                DateTime dateNow = DateTime.Now;
                state[6] = "On";
                pictureBox7.Image = Image.FromFile(@"C:\Users\crazzyninja\source\repos\SmartHomeSecuritySystem\SmartHomeSecuritySystem\Ui pictures\SpeakerOn2.png");//save data to sql with current device and user
                DateTime date = DateTime.Now;
                // string dateNow=date.ToString("yyyy-MM-dd hh:mm:ss");
                userActivity.DateOfUse = date;
                userActivity.StateOfDevice = state[6];
                writeActivityToDB(userActivity);
            }
        }

        private void pictureBox8_MouseDown(object sender, MouseEventArgs e)
        {//kitchen
            userActivity.DeviceID = 7;
            if (state[7].Equals("On"))
            {
                state[7] = "Off";
                pictureBox8.Image = Image.FromFile(@"C:\Users\crazzyninja\source\repos\SmartHomeSecuritySystem\SmartHomeSecuritySystem\Ui pictures\LightOff.png");//save data to sql with current device and user
                userActivity.DateOfUse = DateTime.Now;
                userActivity.StateOfDevice = state[7];
                writeActivityToDB(userActivity);
                //директно да пише в базата 
            }
            else
            {
                DateTime dateNow = DateTime.Now;
                state[7] = "On";
                pictureBox8.Image = Image.FromFile(@"C:\Users\crazzyninja\source\repos\SmartHomeSecuritySystem\SmartHomeSecuritySystem\Ui pictures\LightOnn.png");//save data to sql with current device and user
                DateTime date = DateTime.Now;
                // string dateNow=date.ToString("yyyy-MM-dd hh:mm:ss");
                userActivity.DateOfUse = date;
                userActivity.StateOfDevice = state[7];
                writeActivityToDB(userActivity);
            }
        }

       
    }
}