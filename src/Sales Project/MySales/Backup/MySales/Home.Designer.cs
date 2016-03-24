namespace MySales
{
    partial class Home
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
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lnkResetPwd = new System.Windows.Forms.LinkLabel();
            this.lnkExit = new System.Windows.Forms.LinkLabel();
            this.lnkViewSalesData = new System.Windows.Forms.LinkLabel();
            this.lnkEnterSalesData = new System.Windows.Forms.LinkLabel();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(140, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(307, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Welcome to Sales Management System";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lnkResetPwd);
            this.groupBox1.Controls.Add(this.lnkExit);
            this.groupBox1.Controls.Add(this.lnkViewSalesData);
            this.groupBox1.Controls.Add(this.lnkEnterSalesData);
            this.groupBox1.Location = new System.Drawing.Point(144, 81);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(330, 237);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select your option :-";
            // 
            // lnkResetPwd
            // 
            this.lnkResetPwd.AutoSize = true;
            this.lnkResetPwd.Enabled = false;
            this.lnkResetPwd.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkResetPwd.Location = new System.Drawing.Point(47, 132);
            this.lnkResetPwd.Name = "lnkResetPwd";
            this.lnkResetPwd.Size = new System.Drawing.Size(165, 24);
            this.lnkResetPwd.TabIndex = 3;
            this.lnkResetPwd.TabStop = true;
            this.lnkResetPwd.Text = "Reset Password";
            this.lnkResetPwd.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkResetPwd_LinkClicked);
            // 
            // lnkExit
            // 
            this.lnkExit.AutoSize = true;
            this.lnkExit.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkExit.Location = new System.Drawing.Point(47, 169);
            this.lnkExit.Name = "lnkExit";
            this.lnkExit.Size = new System.Drawing.Size(45, 24);
            this.lnkExit.TabIndex = 2;
            this.lnkExit.TabStop = true;
            this.lnkExit.Text = "Exit";
            this.lnkExit.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkExit_LinkClicked);
            // 
            // lnkViewSalesData
            // 
            this.lnkViewSalesData.AutoSize = true;
            this.lnkViewSalesData.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkViewSalesData.Location = new System.Drawing.Point(47, 90);
            this.lnkViewSalesData.Name = "lnkViewSalesData";
            this.lnkViewSalesData.Size = new System.Drawing.Size(158, 24);
            this.lnkViewSalesData.TabIndex = 1;
            this.lnkViewSalesData.TabStop = true;
            this.lnkViewSalesData.Text = "View sales data";
            this.lnkViewSalesData.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkViewSalesData_LinkClicked);
            // 
            // lnkEnterSalesData
            // 
            this.lnkEnterSalesData.AutoSize = true;
            this.lnkEnterSalesData.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkEnterSalesData.Location = new System.Drawing.Point(47, 43);
            this.lnkEnterSalesData.Name = "lnkEnterSalesData";
            this.lnkEnterSalesData.Size = new System.Drawing.Size(163, 24);
            this.lnkEnterSalesData.TabIndex = 0;
            this.lnkEnterSalesData.TabStop = true;
            this.lnkEnterSalesData.Text = "Enter sales data";
            this.lnkEnterSalesData.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 345);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Name = "Home";
            this.Text = "Home";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.LinkLabel lnkEnterSalesData;
        private System.Windows.Forms.LinkLabel lnkViewSalesData;
        private System.Windows.Forms.LinkLabel lnkExit;
        private System.Windows.Forms.LinkLabel lnkResetPwd;
    }
}