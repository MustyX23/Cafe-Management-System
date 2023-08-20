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
    public partial class UserOrder : Form
    {
        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Mustafe\Documents\CafeDB.mdf;Integrated Security=True;Connect Timeout=30");
        public UserOrder()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 login = new Form1();
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

        private void label1_Click(object sender, EventArgs e)
        {

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

        int num, flag;
        decimal price, total;
        string item, category;

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
            }
        }


        DataTable table = new DataTable();

        private void UserOrder_Load(object sender, EventArgs e)
        {
            Populate();
            table.Columns.Add("Num", typeof(int));
            table.Columns.Add("ItemName", typeof(string));
            table.Columns.Add("Category", typeof(string));
            table.Columns.Add("Unit Price", typeof(decimal));
            table.Columns.Add("Total", typeof(decimal));
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
