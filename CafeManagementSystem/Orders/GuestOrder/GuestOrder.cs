using CafeManagementSystem.Orders.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CafeManagementSystem
{
    public partial class GuestOrder : Form
    {
        public GuestOrder()
        {
            InitializeComponent();
            InitializeTable();
        }

        private DataTable table = new DataTable();
        private OrderData orderData = new OrderData();

        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Mustafe\Documents\CafeDB.mdf;Integrated Security=True;Connect Timeout=30");

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
        private void InitializeTable()
        {
            table.Columns.Add("Num", typeof(int));
            table.Columns.Add("ItemName", typeof(string));
            table.Columns.Add("Category", typeof(string));
            table.Columns.Add("Unit Price", typeof(decimal));
            table.Columns.Add("Total", typeof(decimal));
            OrdersGV.DataSource = table;
        }
        private void GuestOrder_Load(object sender, EventArgs e)
        {
            Populate();
            DateLabel.Text = DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
        }

        private void exitLabel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void logOut_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
        }

        private void Categories_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FilterByCategory();
        }       

        private void ItemsGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            orderData.Item = ItemsGV.SelectedRows[0].Cells[1].Value.ToString();
            orderData.Category = ItemsGV.SelectedRows[0].Cells[2].Value.ToString();
            orderData.Price = decimal.Parse(ItemsGV.SelectedRows[0].Cells[3].Value.ToString());
            orderData.Flag = 1;
        }
        private void PlaceOrder_Button(object sender, EventArgs e)
        {
            connection.Open();
            string query = "INSERT into Orders VALUES (" + OrderNum.Text + ", '" + DateLabel.Text + "', '" + Guest.Text + "', '" + LabelAmount.Text + "')";
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
            MessageBox.Show("Order is Successfully Created!");
            connection.Close();
        }        

        private void AddButon_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Quantity.Text))
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
                LabelAmount.Text = $"{orderData.Sum} lv.";
            }
        }
    }
}
