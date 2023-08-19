﻿using System;
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
            Populate();
        }

        private void UsersForm_Load(object sender, EventArgs e)
        {
            Populate();
        }

        private void UsersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            UsersUsernameTBL.Text = UsersGV.SelectedRows[0].Cells[0].Value.ToString();
            UsersPasswordTBL.Text = UsersGV.SelectedRows[0].Cells[1].Value.ToString();
            UsersPhoneTBL.Text = UsersGV.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (UsersUsernameTBL.Text == "")
            {
                MessageBox.Show("Select The User you want to Delete");
            }
            else
            {
                connection.Open();
                string query = "DELETE FROM Users WHERE Username = '"+UsersUsernameTBL.Text+"'";
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                MessageBox.Show("User is successfully Deleted!");
                connection.Close();
                Populate();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (UsersUsernameTBL.Text == "" || UsersPasswordTBL.Text == "" || UsersPhoneTBL.Text == "")
            {
                MessageBox.Show("All fields must be filled.");
            }
            else
            {
                connection.Open();
                string query = "UPDATE Users SET Phone = '"+UsersPhoneTBL.Text+"', Password = '"+UsersPasswordTBL.Text+"' WHERE Username = '"+UsersUsernameTBL.Text+"'";
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                MessageBox.Show("User is successfully Updated!");
                connection.Close();
                Populate();
            }
        }
    }
}
