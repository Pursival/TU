using MySql.Data.MySqlClient;
using System.Data;

namespace SmartHomeSecuritySystem
{
    internal class SystemUsers
    {
        int userID;
        string userName;
        string userPassword;
        string userType;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public SystemUsers(int userID, string userName, string userPassword, string userType)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            UserID = userID;
            UserName = userName;
            UserPassword = userPassword;
            UserType = userType;
        }

        public int UserID { get => userID; set => userID = value; }
        public string UserName { get => userName; set => userName = value; }
        public string UserPassword { get => userPassword; set => userPassword = value; }
        public string UserType { get => userType; set => userType = value; }
    }
}
