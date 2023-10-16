using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using BCrypt.Net;



namespace Form1
{
    public partial class Form1 : Form
    {
        string connectionString = @"Data Source = charles\SQLEXPRESS; Initial Catalog = leaderboard; Integrated Security = True;";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void Register_Click(object sender, EventArgs e)
        {
            if (username_txt.Text == "" || password_txt.Text == "" || confirmpassword_txt.Text == "")
            {
                MessageBox.Show("Please fill mandatory fields");
                return;
            }
            else if (password_txt.Text != confirmpassword_txt.Text)
            {
                MessageBox.Show("Password do not match");
                return;
            }
            else if (IsUsernameExists(username_txt.Text))
            {
                MessageBox.Show("Username Already exist please try another, please use another");
                return;
            }
            else
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password_txt.Text); // Hash the password here

                using (SqlConnection sql_con = new SqlConnection(connectionString))
                {
                    sql_con.Open();
                    SqlCommand sql_cmd = new SqlCommand(
                        @"INSERT INTO[dbo].[Gamer]
                        ([gamer_name], 
                        [password])
                        VALUES
                        (@username, @hashedPassword)", sql_con
                    );
                    sql_cmd.Parameters.AddWithValue("@username", username_txt.Text);
                    sql_cmd.Parameters.AddWithValue("@hashedPassword", hashedPassword);
                    sql_cmd.ExecuteNonQuery();
                    //sql_con.Close();
                    MessageBox.Show("Successfully registered!");
                    Clear();
                }
            }
        }
        void Clear()
        {
            username_txt.Text = password_txt.Text = confirmpassword_txt.Text = "";
        }

        private void username_tb_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private bool IsUsernameExists(string gamer_name)
        {
            using (SqlConnection sql_con = new SqlConnection(connectionString))
            {
                sql_con.Open();
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM [leaderboard].[dbo].[Gamer] WHERE gamer_name = @gamer_name", sql_con))
                {
                    command.Parameters.AddWithValue("@gamer_name", gamer_name);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }

        }
    }
}
