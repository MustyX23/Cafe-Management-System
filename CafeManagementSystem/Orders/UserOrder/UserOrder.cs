using CafeManagementSystem.Orders.Models;
using CafeManagementSystem.Users.Models;
using CafeManagementSystem.Users.Models.UsersModels;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CafeManagementSystem
{
    public partial class UserOrder : Form
    {
        public UserOrder()
        {
            InitializeComponent();
        }

        DataTable table = new DataTable();
        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Mustafe\Documents\CafeDB.mdf;Integrated Security=True;Connect Timeout=30");
        private OrderData orderData = new OrderData();

        private void LogOut_Button(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
        }

        private void Items_Button(object sender, EventArgs e)
        {
            this.Hide();
            ItemsForm items = new ItemsForm();
            items.Show();
        }

        private void Exit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Users_Button(object sender, EventArgs e)
        {
            OpenUsersFormIfAdmin();
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
            string query = "SELECT * FROM Items WHERE ItemCategory = '" + Categories.SelectedItem.ToString() + "'";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, connection);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(sqlDataAdapter);
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);
            ItemsGV.DataSource = dataSet.Tables[0];
            connection.Close();
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FilterByCategory();
        }

        private void Refresh(object sender, EventArgs e)
        {
            Populate();
        }

        private void Order_Here(object sender, EventArgs e)
        {
            try
            {
                connection.Open();

                string query = "INSERT into Orders VALUES (@OrderNum, @DateLabel, @SellerName, @LabelAmount)";
                SqlCommand command = new SqlCommand(query, connection);

                // Use parameters to prevent SQL injection
                command.Parameters.AddWithValue("@OrderNum", OrderNum.Text);
                command.Parameters.AddWithValue("@DateLabel", DateLabel.Text);
                command.Parameters.AddWithValue("@SellerName", SellerName.Text);
                command.Parameters.AddWithValue("@LabelAmount", LabelAmount.Text);

                command.ExecuteNonQuery();

                MessageBox.Show("Order is Successfully Created!");
            }
            catch (SqlException)
            {
                MessageBox.Show("Order with this OrderNum is already in use. " +
                    "Try again with different OrderNum.");
            }
            finally
            {
                // Ensure the connection is closed regardless of success or exception
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private void ViewOrders(object sender, EventArgs e)
        {
            ViewOrders viewOrders = new ViewOrders();
            viewOrders.Show();
        }

        private void AddToCart(object sender, EventArgs e)
        {
            if (Quantity.Text == "")
            {
                MessageBox.Show("Set Quantity of the Item!");
            }
            else if (orderData.Flag == 0)
            {
                MessageBox.Show("Select the Item you want to Order!");
            }
            else
            {
                orderData.Num++;
                orderData.Total = orderData.Price * int.Parse(Quantity.Text);
                table.Rows.Add(orderData.Num, orderData.Item, orderData.Category, orderData.Price, orderData.Total);
                OrdersGV.DataSource = table;
                orderData.Flag = 0;
                orderData.Sum += orderData.Total;
                LabelAmount.Text = orderData.Sum.ToString() + " lv.";
            }
        }

        private void UserOrder_Load(object sender, EventArgs e)
        {
            Populate();
            table.Columns.Add("Num", typeof(int));
            table.Columns.Add("ItemName", typeof(string));
            table.Columns.Add("Category", typeof(string));
            table.Columns.Add("Unit Price", typeof(decimal));
            table.Columns.Add("Total", typeof(decimal));
            DateLabel.Text = DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
            SellerName.Text = Login.user;
        }

        private void ItemsGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            orderData.Item = ItemsGV.SelectedRows[0].Cells[1].Value.ToString();
            orderData.Category = ItemsGV.SelectedRows[0].Cells[2].Value.ToString();
            orderData.Price = decimal.Parse(ItemsGV.SelectedRows[0].Cells[3].Value.ToString());
            orderData.Flag = 1;
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


    }
}
