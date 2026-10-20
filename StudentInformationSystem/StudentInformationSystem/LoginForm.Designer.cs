namespace BUsengSIS
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlBrand = new System.Windows.Forms.Panel();
            this.logoBrand = new BUsengSIS.LogoBox();
            this.lblBrandTitle = new System.Windows.Forms.Label();
            this.lblBrandSub = new System.Windows.Forms.Label();
            this.pnlLogin = new System.Windows.Forms.Panel();
            this.lblSignIn = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.chkShowPassword = new System.Windows.Forms.CheckBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.pnlBrand.SuspendLayout();
            this.pnlLogin.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlBrand
            //
            this.pnlBrand.BackColor = System.Drawing.Color.FromArgb(31, 58, 95);
            this.pnlBrand.Controls.Add(this.logoBrand);
            this.pnlBrand.Controls.Add(this.lblBrandTitle);
            this.pnlBrand.Controls.Add(this.lblBrandSub);
            this.pnlBrand.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlBrand.Name = "pnlBrand";
            this.pnlBrand.Size = new System.Drawing.Size(240, 370);
            this.pnlBrand.TabIndex = 1;
            //
            // logoBrand
            //
            this.logoBrand.BorderWidth = 2;
            this.logoBrand.Font = new System.Drawing.Font("Segoe UI Semibold", 19.5F);
            this.logoBrand.Letter = "B";
            this.logoBrand.Location = new System.Drawing.Point(92, 124);
            this.logoBrand.Name = "logoBrand";
            this.logoBrand.Size = new System.Drawing.Size(56, 56);
            this.logoBrand.TabStop = false;
            //
            // lblBrandTitle
            //
            this.lblBrandTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 16.5F);
            this.lblBrandTitle.ForeColor = System.Drawing.Color.White;
            this.lblBrandTitle.Location = new System.Drawing.Point(0, 190);
            this.lblBrandTitle.Name = "lblBrandTitle";
            this.lblBrandTitle.Size = new System.Drawing.Size(240, 34);
            this.lblBrandTitle.Text = "BUseng SIS";
            this.lblBrandTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblBrandSub
            //
            this.lblBrandSub.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblBrandSub.ForeColor = System.Drawing.Color.FromArgb(195, 207, 223);
            this.lblBrandSub.Location = new System.Drawing.Point(0, 230);
            this.lblBrandSub.Name = "lblBrandSub";
            this.lblBrandSub.Size = new System.Drawing.Size(240, 20);
            this.lblBrandSub.Text = "Student Information System";
            this.lblBrandSub.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // pnlLogin
            //
            this.pnlLogin.BackColor = System.Drawing.Color.White;
            this.pnlLogin.Controls.Add(this.lblSignIn);
            this.pnlLogin.Controls.Add(this.lblUsername);
            this.pnlLogin.Controls.Add(this.txtUsername);
            this.pnlLogin.Controls.Add(this.lblPassword);
            this.pnlLogin.Controls.Add(this.txtPassword);
            this.pnlLogin.Controls.Add(this.chkShowPassword);
            this.pnlLogin.Controls.Add(this.btnLogin);
            this.pnlLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLogin.Name = "pnlLogin";
            this.pnlLogin.TabIndex = 0;
            //
            // lblSignIn
            //
            this.lblSignIn.AutoSize = true;
            this.lblSignIn.Font = new System.Drawing.Font("Segoe UI Semibold", 13.5F);
            this.lblSignIn.Location = new System.Drawing.Point(44, 48);
            this.lblSignIn.Name = "lblSignIn";
            this.lblSignIn.Text = "Sign in";
            //
            // lblUsername
            //
            this.lblUsername.AutoSize = true;
            this.lblUsername.ForeColor = System.Drawing.Color.FromArgb(61, 75, 90);
            this.lblUsername.Location = new System.Drawing.Point(44, 96);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Text = "Username";
            //
            // txtUsername
            //
            this.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtUsername.Location = new System.Drawing.Point(44, 116);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(312, 25);
            this.txtUsername.TabIndex = 0;
            this.txtUsername.Text = "admin";
            //
            // lblPassword
            //
            this.lblPassword.AutoSize = true;
            this.lblPassword.ForeColor = System.Drawing.Color.FromArgb(61, 75, 90);
            this.lblPassword.Location = new System.Drawing.Point(44, 154);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Text = "Password";
            //
            // txtPassword
            //
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtPassword.Location = new System.Drawing.Point(44, 174);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(312, 25);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.Text = "admin123";
            this.txtPassword.UseSystemPasswordChar = true;
            //
            // chkShowPassword
            //
            this.chkShowPassword.AutoSize = true;
            this.chkShowPassword.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkShowPassword.Location = new System.Drawing.Point(44, 208);
            this.chkShowPassword.Name = "chkShowPassword";
            this.chkShowPassword.TabIndex = 2;
            this.chkShowPassword.Text = "Show password";
            this.chkShowPassword.UseVisualStyleBackColor = true;
            this.chkShowPassword.CheckedChanged += new System.EventHandler(this.chkShowPassword_CheckedChanged);
            //
            // btnLogin
            //
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(31, 58, 95);
            this.btnLogin.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(31, 58, 95);
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(44, 246);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(312, 36);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            //
            // LoginForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(244, 246, 248);
            this.ClientSize = new System.Drawing.Size(640, 370);
            this.Controls.Add(this.pnlLogin);
            this.Controls.Add(this.pnlBrand);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BUseng SIS - Login";
            this.pnlBrand.ResumeLayout(false);
            this.pnlLogin.ResumeLayout(false);
            this.pnlLogin.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlBrand;
        private BUsengSIS.LogoBox logoBrand;
        private System.Windows.Forms.Label lblBrandTitle;
        private System.Windows.Forms.Label lblBrandSub;
        private System.Windows.Forms.Panel pnlLogin;
        private System.Windows.Forms.Label lblSignIn;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.CheckBox chkShowPassword;
        private System.Windows.Forms.Button btnLogin;
    }
}
