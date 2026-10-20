namespace StudentInformationSystem
{
    partial class LogForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlGrid = new StudentInformationSystem.BorderPanel();
            this.dgvLog = new System.Windows.Forms.DataGridView();
            this.colTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStudentId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLog)).BeginInit();
            this.SuspendLayout();
            //
            // pnlGrid
            //
            this.pnlGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlGrid.BackColor = System.Drawing.Color.White;
            this.pnlGrid.Controls.Add(this.dgvLog);
            this.pnlGrid.Location = new System.Drawing.Point(16, 16);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Padding = new System.Windows.Forms.Padding(1);
            this.pnlGrid.Size = new System.Drawing.Size(608, 296);
            //
            // dgvLog
            //
            this.dgvLog.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colTime, this.colUser, this.colAction, this.colStudentId});
            this.dgvLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLog.Name = "dgvLog";
            this.dgvLog.TabIndex = 0;
            //
            // colTime
            //
            this.colTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colTime.HeaderText = "Time";
            this.colTime.Name = "colTime";
            this.colTime.Width = 170;
            //
            // colUser
            //
            this.colUser.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colUser.HeaderText = "User";
            this.colUser.Name = "colUser";
            this.colUser.Width = 120;
            //
            // colAction
            //
            this.colAction.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colAction.HeaderText = "Action";
            this.colAction.Name = "colAction";
            this.colAction.Width = 120;
            //
            // colStudentId
            //
            this.colStudentId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colStudentId.HeaderText = "Student ID";
            this.colStudentId.Name = "colStudentId";
            //
            // btnClose
            //
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(170, 179, 189);
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.btnClose.Location = new System.Drawing.Point(540, 324);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(84, 30);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            //
            // LogForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(244, 246, 248);
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(640, 370);
            this.Controls.Add(this.pnlGrid);
            this.Controls.Add(this.btnClose);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(480, 300);
            this.Name = "LogForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Activity Log";
            this.pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLog)).EndInit();
            this.ResumeLayout(false);
        }

        private StudentInformationSystem.BorderPanel pnlGrid;
        private System.Windows.Forms.DataGridView dgvLog;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAction;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStudentId;
        private System.Windows.Forms.Button btnClose;
    }
}
