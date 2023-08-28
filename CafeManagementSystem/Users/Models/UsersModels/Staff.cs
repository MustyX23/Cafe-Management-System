using System;
using System.Collections.Generic;
using System.Text;

namespace CafeManagementSystem.Users.Models.UsersModels
{
    public class Staff : User
    {
        public Staff(string username, string password)
            : base(username, password)
        {
        }

        public override string Role => "Staff";
    }
}
