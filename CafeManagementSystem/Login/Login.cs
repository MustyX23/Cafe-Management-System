using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CafeManagementSystem
{
    public partial class Login : Form
    {
        public static string user;

        public Login()
        {
            InitializeComponent();
        }

        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Mustafe\Documents\CafeDB.mdf;Integrated Security=True;Connect Timeout=30");

        private void Login_Button(object sender, EventArgs e)
        {
            user = Username.Text;

            if (string.IsNullOrEmpty(Username.Text) || string.IsNullOrEmpty(Password.Text))
            {
                MessageBox.Show("Enter a valid Username or Password.");
                return;
            }

            connection.Open();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password = @Password", connection);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Username", Username.Text);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Password", Password.Text);

            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            if (dataTable.Rows[0][0].ToString() == "1")
            {
                OpenUserOrderForm();
            }
            else
            {
                MessageBox.Show("Wrong Username or Password!");
            }
            connection.Close();
        }

        private void OpenGuest_Button(object sender, EventArgs e)
        {
            OpenGuestForm();
        }

        private void Exit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBoxShowPassword_Click(object sender, EventArgs e)
        {
            // Toggle password visibility
            Password.UseSystemPasswordChar = !Password.UseSystemPasswordChar;
        }

        private void OpenUserOrderForm()
        {
            UserOrder userOrder = new UserOrder();
            userOrder.Show();
            this.Hide();
        }
        private void OpenGuestForm()
        {
            this.Hide();
            GuestOrder guest = new GuestOrder();
            guest.Show();
        }
    }
}
