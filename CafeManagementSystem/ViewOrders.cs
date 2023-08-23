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

        private void OrdersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (PrintViewDialog.ShowDialog() == DialogResult.OK)
            {
                PrintDocument.Print();
            }
        }

        private void PrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            DrawHeaderText(e.Graphics);
            DrawOrderSummaryHeader(e.Graphics);
            PrintDocument_Data(sender, e);
            DrawOrderSummaryFooter(e.Graphics);
        }

        private void DrawHeaderText(Graphics graphics)
        {
            graphics.DrawString("☕**Caffeiné Cornér Software**☕",
                new Font("Century", 25, FontStyle.Bold), Brushes.Red, new Point(130, 50));
        }

        private void DrawOrderSummaryHeader(Graphics graphics)
        {
            graphics.DrawString("*****Order  Summary*****",
                new Font("Century", 25, FontStyle.Bold), Brushes.Red, new Point(170, 120));
        }

        private void PrintDocument_Data(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            PrintOrderDetails(e.Graphics);
        }

        private void PrintOrderDetails(Graphics graphics)
        {
            var selectedRow = OrdersGV.SelectedRows[0];

            PrintOrderDetailLine(graphics, "Order Number:", selectedRow.Cells[0].Value.ToString(), 200);
            PrintOrderDetailLine(graphics, "Date:", selectedRow.Cells[1].Value.ToString(), 250);
            PrintOrderDetailLine(graphics, "Seller Name:", selectedRow.Cells[2].Value.ToString(), 300);
            PrintOrderDetailLine(graphics, "Amount:", selectedRow.Cells[3].Value.ToString(), 350);
        }

        private void PrintOrderDetailLine(Graphics graphics, string label, string value, int yOffset)
        {
            graphics.DrawString(label + " " + value,
                new Font("Century", 15, FontStyle.Bold), Brushes.Black, new Point(330, yOffset));
        }
        private void DrawOrderSummaryFooter(Graphics graphics)
        {
            graphics.DrawString("*****Order  Summary*****",
                new Font("Century", 25, FontStyle.Bold), Brushes.Red, new Point(170, 420));
        }
    }
}
