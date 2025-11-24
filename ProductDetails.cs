using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ral_Time_Product_Management
{
    public partial class ProductDetails : Form
    {
        public ProductDetails()
        {
            InitializeComponent();
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox8.TextChanged += new System.EventHandler(this.textBox8_TextChanged);
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            MessageBox.Show("Text changed!");
        }


        private void textBox5_TextChanged(object sender, EventArgs e)
        {


        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {


        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {



        }
        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Open Dashboard form
            Dashboard dashboardForm = new Dashboard();
            dashboardForm.Show();

            // Close current ProductDetails form
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            {
                string input = textBox7.Text.Trim(); // 

                if (string.IsNullOrWhiteSpace(input))
                {
                    MessageBox.Show("Product ID cannot be empty");
                    return;
                }

                if (!int.TryParse(input, out int productId))
                {
                    MessageBox.Show("Please enter a valid numeric Product ID");
                    return;
                }
            }
            {
                // 1. Open SQL Connection
                using (SqlConnection con = new SqlConnection(@"Data Source=ANOJAN-SATHU\SQLEXPRESS;Initial Catalog=loginapp;Integrated Security=True;Encrypt=True;TrustServerCertificate=True"))
                {
                    con.Open();

                    // 2. Prepare SQL command (Id is auto-increment, so we skip it)
                    string query = "INSERT INTO products (Id, ProductName, company, Price, Quantity, TotalPrice) " +
                                   "VALUES (@Id, @ProductName, @company, @Price, @Quantity, @TotalPrice)";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // 3. Add parameters
                        cmd.Parameters.AddWithValue("@Id", int.Parse(textBox7.Text));
                        cmd.Parameters.AddWithValue("@ProductName", textBox12.Text);
                        cmd.Parameters.AddWithValue("@company", textBox8.Text);
                        cmd.Parameters.AddWithValue("@Price", decimal.Parse(textBox9.Text));
                        cmd.Parameters.AddWithValue("@Quantity", int.Parse(textBox10.Text));
                        cmd.Parameters.AddWithValue("@TotalPrice", decimal.Parse(textBox11.Text));

                        // 4. Execute the query
                        cmd.ExecuteNonQuery();
                    }

                    // 5. Close connection
                    con.Close();

                    // 6. Show success message
                    MessageBox.Show("Record Saved Successfully!");
                }
            }
        }





        private void button11_Click(object sender, EventArgs e)
        {

            {
                using (SqlConnection con = new SqlConnection(@"Data Source=ANOJAN-SATHU\SQLEXPRESS;Initial Catalog=loginapp;Integrated Security=True;Encrypt=True;TrustServerCertificate=True"))
                {
                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM products", con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }



        }

        private void button8_Click(object sender, EventArgs e)
        {
            {
                string text = textBox7.Text.Trim();  // Trim added

                if (string.IsNullOrEmpty(text))
                {
                    MessageBox.Show("ID cannot be empty.");
                    return;
                }

                if (!int.TryParse(text, out int Id))
                {
                    MessageBox.Show("Please enter a valid numeric ID.");
                    return;
                }

                try
                {
                    using (SqlConnection con = new SqlConnection(@"Data Source=ANOJAN-SATHU\SQLEXPRESS;Initial Catalog=loginapp;Integrated Security=True;Encrypt=True;TrustServerCertificate=True"))
                    {
                        con.Open();
                        string query = "DELETE FROM products WHERE Id = @Id";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@Id", Id);
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Record deleted successfully.");
                            }
                            else
                            {
                                MessageBox.Show("No record found with this ID.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

                
            
        
               
                
            
        



        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Confirm deletion
            DialogResult result = MessageBox.Show("Are you sure you want to delete ALL records?",
                                                  "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
                return;

            string connectionString = @"Data Source=ANOJAN-SATHU\SQLEXPRESS;Initial Catalog=loginapp;Integrated Security=True;Encrypt=True;TrustServerCertificate=True"; //  DB connection string

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                
                
                    con.Open();
                    string query = "DELETE FROM products"; // products table records delete 

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();
                        MessageBox.Show(rowsAffected + " records deleted successfully.");
                    }

                    // DataGridView
                    dataGridView1.DataSource = null;
                    dataGridView1.Rows.Clear();

                    // TextBox clear
                    textBox7.Clear();
                    textBox12.Clear();
                    textBox8.Clear();
                    textBox9.Clear();
                    textBox10.Clear();
                    textBox11.Clear();
                
             }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                textBox7.Text = dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            // Get values from textboxes
            string idText = textBox7.Text.Trim();  // ID textbox
            string name = textBox12.Text.Trim();
            string company = textBox8.Text.Trim();
            string priceText = textBox9.Text.Trim();
            string quantityText = textBox10.Text.Trim();
            string totalText = textBox11.Text.Trim();

            // Basic validation
            if (string.IsNullOrEmpty(idText))
            {
                MessageBox.Show("ID cannot be empty.");
                return;
            }

            if (!int.TryParse(idText, out int id))
            {
                MessageBox.Show("Please enter a valid numeric ID.");
                return;
            }

            if (!decimal.TryParse(priceText, out decimal price))
            {
                MessageBox.Show("Please enter a valid price.");
                return;
            }

            // TryParse using CultureInfo.InvariantCulture to avoid comma/dot issues
            if (!decimal.TryParse(priceText, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal Price))
            {
                MessageBox.Show("Please enter a valid price (only numbers, no letters).");
                return;
            }

            if (!int.TryParse(quantityText, out int quantity))
            {
                MessageBox.Show("Please enter a valid quantity (whole number).");
                return;
            }

            if (!decimal.TryParse(totalText, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal total))
            {
                MessageBox.Show("Please enter a valid total price.");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=ANOJAN-SATHU\SQLEXPRESS;Initial Catalog=loginapp;Integrated Security=True;Encrypt=True;TrustServerCertificate=True"))
                {
                    con.Open();

                    string query = @"UPDATE products 
                             SET ProductName = @ProductName,
                                 Company = @Company,
                                 Price = @Price,
                                 Quantity = @Quantity,
                                 TotalPrice = @TotalPrice
                             WHERE Id = @Id";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.Parameters.AddWithValue("@ProductName", name);
                        cmd.Parameters.AddWithValue("@Company", company);
                        cmd.Parameters.AddWithValue("@Price", price);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);
                        cmd.Parameters.AddWithValue("@TotalPrice", total);

                        int rows = cmd.ExecuteNonQuery();

                        if (rows > 0)
                            MessageBox.Show("Product updated successfully!");
                        else
                            MessageBox.Show("No record found with that ID.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
        
  
     
  











