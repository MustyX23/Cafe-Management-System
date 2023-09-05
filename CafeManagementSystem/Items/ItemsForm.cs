using CafeManagementSystem.Users.Models;
using CafeManagementSystem.Users.Models.UsersModels;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CafeManagementSystem
{
    public partial class ItemsForm : Form
    {
        private SqlConnection connection;

        public ItemsForm()
        {
            InitializeComponent();
            connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Mustafe\Documents\CafeDB.mdf;Integrated Security=True;Connect Timeout=30");
        }

        private void Exit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Order_Button(object sender, EventArgs e)
        {
            this.Hide();
            UserOrder order = new UserOrder();
            order.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Hide();
            Login login = new Login();
            login.Show();
        }

        private void Users_Button(object sender, EventArgs e)
        {
            OpenUsersFormIfAdmin();
        }

        private void Add_Button(object sender, EventArgs e)
        {
            if (ValidateInputFields())
            {
                InsertItemIntoDatabase();
                Populate();
            }
        }

        private bool ValidateInputFields()
        {
            if (ItemNum.Text == "" || ItemName.Text == "" || ItemPrice.Text == "")
            {
                MessageBox.Show("Fill all the fields.");
                return false;
            }
            else if (ItemCat.SelectedItem == null)
            {
                MessageBox.Show("Select an item category.");
                return false;
            }
            return true;
        }

        private void InsertItemIntoDatabase()
        {
            try
            {
                connection.Open();
                string query = $"INSERT into Items VALUES ('{ItemNum.Text}', '{ItemName.Text}', '{ItemCat.SelectedItem.ToString()}', '{ItemPrice.Text}')";
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                MessageBox.Show("Item is successfully created!");
            }
            catch (SqlException)
            {
                MessageBox.Show("An item with the same ItemNumber already exists.");
            }
            finally
            {
                connection.Close();
            }
        }

        private void Populate()
        {
            connection.Open();
            string query = "SELECT * FROM Items";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, connection);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(sqlDataAdapter);
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);
            ItemsGV.DataSource = dataSet.Tables[0];
            connection.Close();
        }
        private void FilterByCategory()
        {
            connection.Open();
            string query = "SELECT * FROM Items WHERE ItemCategory = '" + ItemCat.SelectedItem.ToString() + "'";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, connection);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(sqlDataAdapter);
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);
            ItemsGV.DataSource = dataSet.Tables[0];
            connection.Close();
        }

        private void Delete_Button(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ItemNum.Text) || string.IsNullOrEmpty(ItemName.Text))
            {
                MessageBox.Show("Select The Item you want to Delete");
            }
            else
            {
                DeleteItemFromDatabase();
                Populate();
            }
        }

        private void DeleteItemFromDatabase()
        {
            try
            {
                connection.Open();
                string query = $"DELETE FROM Items WHERE ItemNumber = '{ItemNum.Text}'";
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                MessageBox.Show("Item is successfully Deleted!");
            }
            finally
            {
                connection.Close();
            }
        }

        private void ItemsGV_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            ItemNum.Text = ItemsGV.SelectedRows[0].Cells[0].Value.ToString();
            ItemName.Text = ItemsGV.SelectedRows[0].Cells[1].Value.ToString();
            ItemCat.SelectedItem = ItemsGV.SelectedRows[0].Cells[2].Value.ToString();
            ItemPrice.Text = ItemsGV.SelectedRows[0].Cells[3].Value.ToString();
        }

        private void Edit_Button(object sender, EventArgs e)
        {
            if (ValidateInputFields())
            {
                UpdateItemInDatabase();
                Populate();
            }
        }

        private void UpdateItemInDatabase()
        {
            try
            {
                connection.Open();
                string query = $"UPDATE Items SET ItemName = '{ItemName.Text}', ItemPrice = '{ItemPrice.Text}', ItemCategory = '{ItemCat.SelectedItem.ToString()}' WHERE ItemNumber = '{ItemNum.Text}'";
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                MessageBox.Show("Item is successfully Updated!");
            }
            finally
            {
                connection.Close();
            }
        }

        private void ItemCat_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FilterByCategory();
        }

        private void OpenUsersFormIfAdmin()
        {
            User authenticatedUser = UserManager.GetUserFromDatabase(Login.user, connection.ToString());
            if (authenticatedUser.Role != "Admin")
            {
                MessageBox.Show("Access Denied!", "You don't have permission to visit this section.");
            }
            else
            {
                Hide();
                UsersForm users = new UsersForm();
                users.Show();
            }
        }

        private void ItemsForm_Load(object sender, EventArgs e)
        {
            Populate();
        }
    }
}