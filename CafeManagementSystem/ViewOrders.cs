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
    public partial class ViewOrders : Form
    {
        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Mustafe\Documents\CafeDB.mdf;Integrated Security=True;Connect Timeout=30");
        public ViewOrders()
        {
            InitializeComponent();
        }
        private void Populate()
        {
            connection.Open();
            string query = "SELECT * FROM Orders";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, connection);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(sqlDataAdapter);
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);
            OrdersGV.DataSource = dataSet.Tables[0];
            connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void ViewOrders_Load(object sender, EventArgs e)
        {
            Populate();
        }
    }
}
