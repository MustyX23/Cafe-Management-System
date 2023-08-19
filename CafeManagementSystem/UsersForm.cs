using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CafeManagementSystem
{
    public partial class UsersForm : Form
    {
        public UsersForm()
        {
            InitializeComponent();
        }

        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Mustafe\Documents\CafeDB.mdf;Integrated Security=True;Connect Timeout=30");

        private void Populate()
        {
            connection.Open();
            string query = "SELECT * FROM Users";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, connection);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(sqlDataAdapter);
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);
            UsersGV.DataSource = dataSet.Tables[0];
            connection.Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            UserOrder usersOrder = new UserOrder();
            usersOrder.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 login = new Form1();
            login.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            ItemsForm items = new ItemsForm();
            items.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connection.Open();
            string query = "INSERT into Users values ('" + UsersUsernameTBL.Text + "', '" + UsersPasswordTBL.Text + "', '" + UsersPhoneTBL.Text + "')";
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
            MessageBox.Show("User is successfully created!");
            connection.Close();
        }

        private void UsersForm_Load(object sender, EventArgs e)
        {
            Populate();
        }


    }
}
