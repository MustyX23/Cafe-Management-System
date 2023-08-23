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
    public partial class ItemsForm : Form
    {        
        public ItemsForm()
        {
            InitializeComponent();
        }

        SqlConnection connection = 
            new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Mustafe\Documents\CafeDB.mdf;Integrated Security=True;Connect Timeout=30");

        private void label8_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
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

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            UsersForm users = new UsersForm();
            users.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ItemNum.Text == "" || ItemName.Text == "" || ItemPrice.Text == "")
            {
                MessageBox.Show("Fill all the fields.");
            }
            else
            {
                connection.Open();
                string query = "INSERT into Items VALUES ('" + ItemNum.Text + "', '" + ItemName.Text + "', '" +ItemCat.SelectedItem.ToString() + "', '" + ItemPrice.Text + "')";
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                MessageBox.Show("Item is successfully created!");
                connection.Close();
                Populate();
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

        private void ItemsForm_Load(object sender, EventArgs e)
        {
            Populate();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (ItemNum.Text == "" || ItemName.Text == "")
            {
                MessageBox.Show("Select The Item you want to Delete");
            }
            else
            {
                connection.Open();
                string query = "DELETE FROM Items WHERE ItemNumber = '" + ItemNum.Text + "'";
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                MessageBox.Show("Item is successfully Deleted!");
                connection.Close();
                Populate();
            }
        }

        private void ItemsGV_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            ItemNum.Text = ItemsGV.SelectedRows[0].Cells[0].Value.ToString();
            ItemName.Text = ItemsGV.SelectedRows[0].Cells[1].Value.ToString();
            ItemCat.SelectedItem = ItemsGV.SelectedRows[0].Cells[2].Value.ToString();
            ItemPrice.Text = ItemsGV.SelectedRows[0].Cells[3].Value.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (ItemNum.Text == "" || ItemName.Text == "" || ItemPrice.Text == "")
            {
                MessageBox.Show("All fields must be filled.");
            }
            else
            {
                connection.Open();
                string query = "UPDATE Items SET ItemName = '" + ItemName.Text + "', ItemPrice = '" + ItemPrice.Text + "', ItemCategory = '" + ItemCat.SelectedItem.ToString() + "' WHERE ItemNumber = '" + ItemNum.Text + "'";
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                MessageBox.Show("Item is successfully Updated!");
                connection.Close();
                Populate();
            }
        }
    }
}
