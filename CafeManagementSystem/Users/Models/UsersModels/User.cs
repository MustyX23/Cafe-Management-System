using CafeManagementSystem.Users.Models.UsersModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace CafeManagementSystem.Users.Models
{
    public abstract class User
    {
        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public abstract string Role { get; }

    }
}
