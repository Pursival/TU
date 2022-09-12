using MySql.Data.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSecuritySystem
{
    public class UserActivity
    {
        private int activityID;
        private int deviceID;
        private int userID;
        private string stateOfDevice;
        private DateTime dateOfUse;
        public UserActivity() { }

        public UserActivity(int activityID, int deviceID, int userID, string stateOfDevice, DateTime dateOfUse)
        {
            this.ActivityID = activityID;
            this.DeviceID = deviceID;
            this.UserID = userID;
            this.StateOfDevice = stateOfDevice;
            this.DateOfUse = dateOfUse;
        }

        public int ActivityID { get => activityID; set => activityID = value; }
        public int DeviceID { get => deviceID; set => deviceID = value; }
        public int UserID { get => userID; set => userID = value; }
        public string StateOfDevice { get => stateOfDevice; set => stateOfDevice = value; }
        public DateTime DateOfUse { get => dateOfUse; set => dateOfUse = value; }
    }
}
