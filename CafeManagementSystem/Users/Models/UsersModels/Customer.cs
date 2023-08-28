using System;
using System.Collections.Generic;
using System.Text;

namespace CafeManagementSystem.Users.Models.UsersModels
{
    public class Customer : User
    {
        public override string Role => "Customer";

        public Customer(string username, string password)
            : base(username, password)
        {
        }
    }
}
