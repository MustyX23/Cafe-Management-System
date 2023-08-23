﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
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

        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            ItemsForm items = new ItemsForm();
            items.Show();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Hide();
            UsersForm users = new UsersForm();
            users.Show();
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
            string query = "SELECT * FROM Items WHERE ItemCategory = '"+Categories.SelectedItem.ToString()+"'";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, connection);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(sqlDataAdapter);
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);
            ItemsGV.DataSource = dataSet.Tables[0];
            connection.Close();
        }

        int num, flag;
        decimal price, total, sum;
        string item, category;

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FilterByCategory();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Populate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            connection.Open();
            string query = "INSERT into Orders VALUES (" + OrderNum.Text + ", '" + DateLabel.Text + "', '" + SellerName.Text + "', '" + LabelAmount.Text + "')";
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
            MessageBox.Show("Order is Successfully Created!");
            connection.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ViewOrders viewOrders = new ViewOrders();
            viewOrders.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Quantity.Text == "")
            {
                MessageBox.Show("Set Quantity of the Item!");
            }
            else if (flag == 0)
            {
                MessageBox.Show("Select the Item you want to Order!");
            }
            else
            {
                num++;
                total = price * int.Parse(Quantity.Text);
                table.Rows.Add(num, item, category, price, total);
                OrdersGV.DataSource = table;
                flag = 0;
                sum += total;
                LabelAmount.Text = sum.ToString() + " lv.";
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
            item = ItemsGV.SelectedRows[0].Cells[1].Value.ToString();
            category = ItemsGV.SelectedRows[0].Cells[2].Value.ToString();
            price = decimal.Parse(ItemsGV.SelectedRows[0].Cells[3].Value.ToString());
            flag = 1;
        }
    }
}
