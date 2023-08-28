using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace CafeManagementSystem.Users.Models.UsersModels
{
    public static class UserManager
    {
        public static User GetUserFromDatabase(string username, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Mustafe\Documents\CafeDB.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                connection.Open();

                string query = "SELECT Username, Password, Phone, Role FROM Users WHERE Username = @Username";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string fetchedUsername = reader.GetString(0);
                        string fetchedPassword = reader.GetString(1);
                        string fetchedRole = reader.GetString(3);

                        if (fetchedRole == "Admin")
                        {
                            return new Admin(fetchedUsername, fetchedPassword);
                        }
                        else if (fetchedRole == "Staff")
                        {
                            return new Staff(fetchedUsername, fetchedPassword);
                        }
                        // Handle other roles as needed
                    }
                }
            }
            return null;
        }
    }
}
