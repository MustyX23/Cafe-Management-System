using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private void button1_Click(object sender, EventArgs e)
        {
            //UserOrder userOrder = new UserOrder();
            //userOrder.Show();
            //this.Hide();
            user = Username.Text;

            if (Username.Text == "" || Password.Text == "")
            {
                MessageBox.Show("Enter a valid Username or Password.");
            }
            else
            {
                connection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT COUNT(*) FROM Users WHERE Username = '"+Username.Text+"' AND Password = '"+Password.Text+"'", connection);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                if (dataTable.Rows[0][0].ToString() == "1")
                {
                    UserOrder userOrder = new UserOrder();
                    userOrder.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Wrong Username or Password!");
                }
                connection.Close();
            }

        }
        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            GuestOrder guest = new GuestOrder();
            guest.Show();
        }
        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
