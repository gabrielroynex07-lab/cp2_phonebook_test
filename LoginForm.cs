using Microsoft.Data.SqlClient;
using System;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;

namespace RegistrationNew_224102915
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            Hide(); 

            var registration = new Form1();
            registration.FormClosed += (s, args) => Close(); 
            registration.Show();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "";

            string email = txtEmail.Text.Trim().ToLowerInvariant();
            string password = txtPassword.Text;

            if (email.Length == 0 || password.Length == 0)
            {
                lblStatus.ForeColor = Color.Red;
                lblStatus.Text = "Enter your Email and Password.";
                return;
            }

            try
            {
                string connectionString =
                    ConfigurationManager.ConnectionStrings["RegistrationDb"]?.ConnectionString
                    ?? throw new InvalidOperationException("Connection string 'RegistrationDb' not found in App.config.");

                int userId = 0;
                string fullName = "";
                string storedHash = null;

                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(
                    "SELECT UserId, FullName, PasswordHash FROM dbo.Users WHERE Email = @Email", conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            userId = reader.GetInt32(0);
                            fullName = reader.GetString(1);
                            storedHash = reader.GetString(2);
                        }
                    }
                }

                if (storedHash == null || !BCrypt.Net.BCrypt.Verify(password, storedHash))
                {
                    lblStatus.ForeColor = Color.Red;
                    lblStatus.Text = "Invalid email or password.";
                    txtPassword.Clear();
                    return;
                }

                Session.UserId = userId;
                Session.FullName = fullName;
                txtPassword.Clear();
                Hide();

                var phonebook = new PhonebookForm();
                phonebook.FormClosed += (s, args) =>
                {
                    if (phonebook.LoggedOut)
                    {
                        Show();
                    }
                    else
                    {
                        Close();  
                    }
                };
                phonebook.Show();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Could not connect to the database: " + ex.Message,
                    "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Configuration error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}