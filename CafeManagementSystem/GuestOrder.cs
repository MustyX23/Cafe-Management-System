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
        }

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
        private void GuestOrder_Load(object sender, EventArgs e)
        {
            Populate();
            table.Columns.Add("Num", typeof(int));
            table.Columns.Add("ItemName", typeof(string));
            table.Columns.Add("Category", typeof(string));
            table.Columns.Add("Unit Price", typeof(decimal));
            table.Columns.Add("Total", typeof(decimal));
            OrdersGV.DataSource = table;
            DateLabel.Text = DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 login = new Form1();
            login.Show();
        }

        private void Categories_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FilterByCategory();
        }

        int num, flag;
        decimal price, total, sum;

        private void ItemsGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            item = ItemsGV.SelectedRows[0].Cells[1].Value.ToString();
            category = ItemsGV.SelectedRows[0].Cells[2].Value.ToString();
            price = decimal.Parse(ItemsGV.SelectedRows[0].Cells[3].Value.ToString());
            flag = 1;
        }

        private void OrdersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void LabelAmount_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            connection.Open();
            string query = "INSERT into Orders VALUES (" + OrderNum.Text + ", '" + DateLabel.Text + "', '" + Guest.Text + "', '" + LabelAmount.Text + "')";
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
            MessageBox.Show("Order is Successfully Created!");
            connection.Close();
        }

        string item, category;

        private void button3_Click(object sender, EventArgs e)
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

        DataTable table = new DataTable();


    }
}
