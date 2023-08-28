using System;
using System.Collections.Generic;
using System.Text;

namespace CafeManagementSystem.Users.Models.UsersModels
{
    public class Admin : User
    {
        public override string Role => "Admin";

        public Admin(string username, string password)
            : base(username, password)
        {
        }
    }
}
