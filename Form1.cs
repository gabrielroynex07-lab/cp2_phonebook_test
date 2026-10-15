using Microsoft.Data.SqlClient;
using System;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;

namespace RegistrationNew_224102915
{
    public partial class Form1 : Form
    {
        public bool BackToLogin { get; private set; }
        public Form1()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "";

            string fullName = txtFullName.Text.Trim();
            string email = txtEmail.Text.Trim().ToLowerInvariant();
            string password = txtPassword.Text;
            string confirm = txtConfirmPassword.Text;
            DateTime? dob = dtpDateOfBirth.Checked ? dtpDateOfBirth.Value.Date : (DateTime?)null;

            if (fullName.Length == 0)
            {
                ShowError("Full name is required.", txtFullName); return;
            }
            if (!email.Contains("@") || !email.Contains("."))
            {
                ShowError("Please enter a valid email address.", txtEmail); return;
            }
            if (password.Length < 8)
            {
                ShowError("Password must be at least 8 characters.", txtPassword); return;
            }
            if (password != confirm)
            {
                ShowError("Passwords do not match.", txtConfirmPassword); return;
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            string connectionString =
                ConfigurationManager.ConnectionStrings["RegistrationDb"]?.ConnectionString
                ?? throw new InvalidOperationException("Connection string 'RegistrationDb' not found in App.config.");

            const string sql = @"
INSERT INTO dbo.Users (FullName, Email, PasswordHash, DateOfBirth)
VALUES (@FullName, @Email, @PasswordHash, @DateOfBirth);";

            try
            {
                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@FullName", fullName);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                    cmd.Parameters.AddWithValue("@DateOfBirth", (object)dob ?? DBNull.Value);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery(); 

                    lblStatus.ForeColor = Color.Green;
                    lblStatus.Text = $"Welcome, {fullName}! Account saved.";
                    ClearForm();
                }
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                ShowError("That email address is already registered.", txtEmail);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Database error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowError(string message, Control focusTarget)
        {
            lblStatus.ForeColor = Color.Red;
            lblStatus.Text = message;
            focusTarget.Focus();
        }

        private void ClearForm()
        {
            btnCancel.Text = "Back to Login";
            txtFullName.Clear();
            txtEmail.Clear();
            txtPassword.Clear();
            txtConfirmPassword.Clear();
            dtpDateOfBirth.Checked = false;
            txtFullName.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            BackToLogin = true;
            Close();
        }
    }
}