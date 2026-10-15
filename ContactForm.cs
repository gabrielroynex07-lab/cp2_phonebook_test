using Microsoft.Data.SqlClient;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace RegistrationNew_224102915
{
    public partial class frmContact : Form
    {
        private readonly Contact _contact;   
        private readonly bool _isEdit;

 
        public frmContact()
        {
            InitializeComponent();

            txtContactId.ReadOnly = true;
            txtUserId.ReadOnly = true;
            txtCreatedAt.ReadOnly = true;
            txtUpdateAt.ReadOnly = true;

            cbCategory.DropDownStyle = ComboBoxStyle.DropDownList;

            txtUserId.Text = Session.UserId.ToString();
            txtContactId.Text = "(automatic)";
            txtCreatedAt.Text = "Set automatically when saved";
            txtUpdateAt.Text = "Set automatically when saved";
            Text = "Add Contact_224102915_Nelondo Gabriel";
        }

        public frmContact(Contact contact) : this()
        {
            _contact = contact;
            _isEdit = true;
            Text = "Edit Contact_224102915_Nelondo Gabriel";

            txtContactId.Text = contact.ContactId.ToString();
            txtFirstName.Text = contact.FirstName;
            txtLastName.Text = contact.LastName;
            txtPhoneNumber.Text = contact.PhoneNumber;
            txtEmail.Text = contact.Email ?? "";
            txtAddress.Text = contact.Address ?? "";
            txtNotes.Text = contact.Notes ?? "";
            cbCategory.SelectedIndex = cbCategory.FindStringExact(contact.Category ?? "");

            txtCreatedAt.Text = ToLocalText(contact.CreatedAt);
            txtUpdateAt.Text = contact.UpdatedAt.HasValue
                ? ToLocalText(contact.UpdatedAt.Value)
                : "Not updated yet";
        }

        private static string ToLocalText(DateTime utcValue)
        {
            DateTime utc = DateTime.SpecifyKind(utcValue, DateTimeKind.Utc);
            return utc.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
        }

        private bool ValidateInput()
        {
            string first = txtFirstName.Text.Trim();
            string last = txtLastName.Text.Trim();
            string phone = txtPhoneNumber.Text.Replace(" ", "");
            string email = txtEmail.Text.Trim();

            if (first.Length == 0 || first.Length > 50)
            {
                Warn("First name is required (maximum 50 characters).", txtFirstName);
                return false;
            }
            if (last.Length == 0 || last.Length > 50)
            {
                Warn("Last name is required (maximum 50 characters).", txtLastName);
                return false;
            }
            if (!Regex.IsMatch(phone, @"^\+?[0-9]{7,15}$"))
            {
                Warn("Phone number is required: 7 to 15 digits, spaces allowed, optional + at the start.",
                    txtPhoneNumber);
                return false;
            }
            if (email.Length > 0 && (!email.Contains("@") || !email.Contains(".")))
            {
                Warn("Please enter a valid email address, or leave it empty.", txtEmail);
                return false;
            }
            return true;
        }

        private void Warn(string message, Control focusTarget)
        {
            MessageBox.Show(message, "Check your input",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            focusTarget.Focus();
        }

        private static string NullIfEmpty(string value)
        {
            value = value.Trim();
            return value.Length == 0 ? null : value;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            var contact = new Contact
            {
                ContactId = _isEdit ? _contact.ContactId : 0,
                UserId = Session.UserId,  
                FirstName = txtFirstName.Text.Trim(),
                LastName = txtLastName.Text.Trim(),
                PhoneNumber = txtPhoneNumber.Text.Replace(" ", ""),
                Email = NullIfEmpty(txtEmail.Text),
                Address = NullIfEmpty(txtAddress.Text),
                Notes = NullIfEmpty(txtNotes.Text),
                Category = cbCategory.SelectedIndex >= 0 ? cbCategory.SelectedItem.ToString() : null,
            };

            try
            {
                var repo = new ContactRepository();

                if (_isEdit)
                {
                    int rows = repo.Update(contact);   
                    if (rows == 0)
                    {
                        MessageBox.Show("This contact no longer exists. The list will be refreshed.",
                            "Not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    repo.Insert(contact);              
                }

                DialogResult = DialogResult.OK;
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                MessageBox.Show("You already have a contact with that number.",
                    "Duplicate contact", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhoneNumber.Focus();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("The contact could not be saved: " + ex.Message,
                    "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}