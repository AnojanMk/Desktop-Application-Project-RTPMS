using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ral_Time_Product_Management
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            txtpass.UseSystemPasswordChar = true;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            {
                this.Close(); // Closes the current form
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            SqlConnection con = new SqlConnection("Data Source=ANOJAN-SATHU\\SQLEXPRESS;Initial Catalog=loginapp;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
            con.Open();
            string query = "SELECT COUNT (*) FROM loginapp WHERE username=@username AND password=@password";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@username", txtuser.Text);
            cmd.Parameters.AddWithValue("@password", txtpass.Text);
            int count = (int)cmd.ExecuteScalar();
            con.Close();
            {
                if (txtuser.Text == "e2410626" && txtpass.Text == "123")
                {

                    Dashboard dash = new Dashboard();
                    dash.Show();

                    MessageBox.Show("Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Invalid credentials");
                }
                
            }




        }

        private void txtuser_TextChanged(object sender, EventArgs e)
        {


            
        }

        private void ShowPassword_CheckedChanged(object sender, EventArgs e)
        {
           
            {
                txtpass.PasswordChar = checkbox1.Checked ? '\0' : '*';
            }
    }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            txtpass.UseSystemPasswordChar = true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}
