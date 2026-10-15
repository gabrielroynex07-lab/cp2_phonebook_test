using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace RegistrationNew_224102915
{
    public partial class PhonebookForm : Form
    {
        private readonly ContactRepository _repo = new ContactRepository();
        private List<Contact> _allContacts = new List<Contact>();

        public bool LoggedOut { get; private set; }

        public PhonebookForm()
        {
            InitializeComponent();

            Text = "Phonebook - 224102915_Nelondo Gabriel - " + Session.FullName;
            lblWelcome.Text = "Welcome, " + Session.FullName;

            dgvContacts.ReadOnly = true;
            dgvContacts.AllowUserToAddRows = false;
            dgvContacts.MultiSelect = false;
            dgvContacts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvContacts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            txtSearch.TextChanged += txtSearch_TextChanged;
            dgvContacts.CellDoubleClick += dgvContacts_CellDoubleClick;

            LoadContacts();
        }


        private void LoadContacts()
        {
            try
            {
                _allContacts = _repo.GetAll();
                ApplySearch();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("The contacts could not be loaded: " + ex.Message,
                    "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Configuration error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static bool Matches(string text, string term)
        {
            return text != null && text.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private void ApplySearch()
        {
            string term = txtSearch.Text.Trim();
            List<Contact> shown;

            if (term.Length == 0)
            {
                shown = _allContacts;
            }
            else
            {
                shown = new List<Contact>();
                foreach (Contact c in _allContacts)
                {
                    if (Matches(c.FirstName, term) || Matches(c.LastName, term) || Matches(c.PhoneNumber, term))
                    {
                        shown.Add(c);
                    }
                }
            }

            dgvContacts.DataSource = shown;

            string[] hidden = { "ContactId", "UserId", "Address", "Notes", "CreatedAt", "UpdatedAt" };
            foreach (string name in hidden)
            {
                if (dgvContacts.Columns[name] != null)
                {
                    dgvContacts.Columns[name].Visible = false;
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplySearch();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ApplySearch();
        }

        private Contact GetSelectedContact()
        {
            return dgvContacts.CurrentRow?.DataBoundItem as Contact;
        }


        private static string OrDash(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? "-" : value;
        }

        // Shared by the View button and the double-click
        private void ShowDetails()
        {
            Contact c = GetSelectedContact();
            if (c == null)
            {
                MessageBox.Show("Select a contact in the list first.", "View",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string details =
                "First name: " + OrDash(c.FirstName) + Environment.NewLine +
                "Last name: " + OrDash(c.LastName) + Environment.NewLine +
                "Phone: " + OrDash(c.PhoneNumber) + Environment.NewLine +
                "Email: " + OrDash(c.Email) + Environment.NewLine +
                "Address: " + OrDash(c.Address) + Environment.NewLine +
                "Notes: " + OrDash(c.Notes) + Environment.NewLine +
                "Category: " + OrDash(c.Category) + Environment.NewLine +
                "Created: " + ToLocalText(c.CreatedAt) + Environment.NewLine +
                "Updated: " + (c.UpdatedAt.HasValue ? ToLocalText(c.UpdatedAt.Value) : "-");

            MessageBox.Show(details, "Contact details",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private static string ToLocalText(DateTime utcValue)
        {
            DateTime utc = DateTime.SpecifyKind(utcValue, DateTimeKind.Utc);
            return utc.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            ShowDetails();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var form = new frmContact())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadContacts();
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Contact selected = GetSelectedContact();
            if (selected == null)
            {
                MessageBox.Show("Select a contact in the list first.", "Edit",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var form = new frmContact(selected))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadContacts();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Contact selected = GetSelectedContact();
            if (selected == null)
            {
                MessageBox.Show("Select a contact in the list first.", "Delete",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult answer = MessageBox.Show(
                "Delete " + selected.FirstName + " " + selected.LastName +
                " (" + selected.PhoneNumber + ")? This cannot be undone.",
                "Confirm delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2); 
            if (answer != DialogResult.Yes) return;

            try
            {
                int rows = _repo.Delete(selected.ContactId);
                if (rows == 0)
                {
                    MessageBox.Show("This contact no longer exists. The list will be refreshed.",
                        "Not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                LoadContacts();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("The contact could not be deleted: " + ex.Message,
                    "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Session.Clear();
            LoggedOut = true;
            Close();
        }
        private void dgvContacts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            ShowDetails();
        }
    }
}