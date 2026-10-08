namespace RegistrationNew_224102915
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblFullName = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblDob = new System.Windows.Forms.Label();
            this.lblConfirm = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.dtpDateOfBirth = new System.Windows.Forms.DateTimePicker();
            this.btnRegister = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblFullName
            // 
            this.lblFullName.AutoSize = true;
            this.lblFullName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblFullName.Location = new System.Drawing.Point(95, 49);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(85, 20);
            this.lblFullName.TabIndex = 0;
            this.lblFullName.Text = "Full Name";
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblStatus.ForeColor = System.Drawing.Color.Lime;
            this.lblStatus.Location = new System.Drawing.Point(75, 301);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 20);
            this.lblStatus.TabIndex = 1;
            // 
            // lblDob
            // 
            this.lblDob.AutoSize = true;
            this.lblDob.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblDob.Location = new System.Drawing.Point(75, 253);
            this.lblDob.Name = "lblDob";
            this.lblDob.Size = new System.Drawing.Size(105, 20);
            this.lblDob.TabIndex = 3;
            this.lblDob.Text = "Date of Birth";
            // 
            // lblConfirm
            // 
            this.lblConfirm.AutoSize = true;
            this.lblConfirm.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblConfirm.Location = new System.Drawing.Point(33, 202);
            this.lblConfirm.Name = "lblConfirm";
            this.lblConfirm.Size = new System.Drawing.Size(147, 20);
            this.lblConfirm.TabIndex = 4;
            this.lblConfirm.Text = "Confirm Password";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblPassword.Location = new System.Drawing.Point(97, 151);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(83, 20);
            this.lblPassword.TabIndex = 5;
            this.lblPassword.Text = "Password";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblEmail.Location = new System.Drawing.Point(129, 100);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(51, 20);
            this.lblEmail.TabIndex = 6;
            this.lblEmail.Text = "Email";
            // 
            // txtFullName
            // 
            this.txtFullName.Location = new System.Drawing.Point(195, 49);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(189, 22);
            this.txtFullName.TabIndex = 7;
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.Location = new System.Drawing.Point(195, 202);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.Size = new System.Drawing.Size(189, 22);
            this.txtConfirmPassword.TabIndex = 9;
            this.txtConfirmPassword.UseSystemPasswordChar = true;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(195, 151);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(189, 22);
            this.txtPassword.TabIndex = 10;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(195, 100);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(189, 22);
            this.txtEmail.TabIndex = 11;
            // 
            // dtpDateOfBirth
            // 
            this.dtpDateOfBirth.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateOfBirth.Location = new System.Drawing.Point(194, 252);
            this.dtpDateOfBirth.Name = "dtpDateOfBirth";
            this.dtpDateOfBirth.ShowCheckBox = true;
            this.dtpDateOfBirth.Size = new System.Drawing.Size(189, 22);
            this.dtpDateOfBirth.TabIndex = 12;
            // 
            // btnRegister
            // 
            this.btnRegister.Location = new System.Drawing.Point(184, 355);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(97, 33);
            this.btnRegister.TabIndex = 13;
            this.btnRegister.Text = "Register";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 436);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.dtpDateOfBirth);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtConfirmPassword);
            this.Controls.Add(this.txtFullName);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblConfirm);
            this.Controls.Add(this.lblDob);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblFullName);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblDob;
        private System.Windows.Forms.Label lblConfirm;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.DateTimePicker dtpDateOfBirth;
        private System.Windows.Forms.Button btnRegister;
    }
}

