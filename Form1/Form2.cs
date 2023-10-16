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

namespace Form1
{
    public partial class Form2 : Form
    {
        string connectionString = @"Data Source = charles\SQLEXPRESS; Initial Catalog = leaderboard; Integrated Security = True;";

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void login_btn_Click(object sender, EventArgs e)
        {
            if (username_txt.Text == "" || password_txt.Text == "")
            {
                MessageBox.Show("please fill mandatory fields");
                return;
            }
            else if (!IsUsernameExists(username_txt.Text))
            {
                MessageBox.Show("Login failed, the username does not exist");
                return;
            }
            else
            {
                // string query = "SELECT password FROM [leaderboard].[dbo].[Gamer] WHERE gamer_name = '" + username_txt.Text + "'";
                string query = $"SELECT password FROM [leaderboard].[dbo].[Gamer] WHERE gamer_name = '{username_txt.Text}'";

                using (SqlConnection sql_con = new SqlConnection(connectionString))
                using (SqlCommand sql_cmd = new SqlCommand(query, sql_con))
                {
                    sql_con.Open();
                    string storedHashedPassword = sql_cmd.ExecuteScalar() as string;
                    bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password_txt.Text, storedHashedPassword);
                    if (isPasswordValid)
                    {
                        MessageBox.Show("Login successful!");
                        Clear();
                        // link to the snake game here
                    }
                    else
                    {
                        MessageBox.Show("Incorrect password, please try again");
                        ClearPassword();
                    }
                }
            }
        }

        private void clear_btn_Click(object sender, EventArgs e)
        {
            Clear();
        }

        void Clear()
        {
            username_txt.Text = password_txt.Text = "";
        }

        void ClearPassword()
        {
            password_txt.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
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

        private void back_btn_Click(object sender, EventArgs e)
        {

        }
    }
}
