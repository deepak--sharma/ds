namespace MySales
{
    partial class frmGeneratePayroll
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
            this.cbMonth = new System.Windows.Forms.ComboBox();
            this.lblMonth = new System.Windows.Forms.Label();
            this.lblYear = new System.Windows.Forms.Label();
            this.cbYear = new System.Windows.Forms.ComboBox();
            this.btnGenPayroll = new System.Windows.Forms.Button();
            this.lblProcessing = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lvPayroll = new System.Windows.Forms.ListView();
            this.chImg = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chEmpName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chAdvance = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chOt = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbMonth
            // 
            this.cbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMonth.FormattingEnabled = true;
            this.cbMonth.Items.AddRange(new object[] {
            "January",
            "February",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December"});
            this.cbMonth.Location = new System.Drawing.Point(63, 27);
            this.cbMonth.Name = "cbMonth";
            this.cbMonth.Size = new System.Drawing.Size(121, 21);
            this.cbMonth.TabIndex = 0;
            // 
            // lblMonth
            // 
            this.lblMonth.AutoSize = true;
            this.lblMonth.Location = new System.Drawing.Point(17, 27);
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Size = new System.Drawing.Size(37, 13);
            this.lblMonth.TabIndex = 1;
            this.lblMonth.Text = "Month";
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.Location = new System.Drawing.Point(17, 71);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(29, 13);
            this.lblYear.TabIndex = 2;
            this.lblYear.Text = "Year";
            // 
            // cbYear
            // 
            this.cbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbYear.FormattingEnabled = true;
            this.cbYear.Location = new System.Drawing.Point(63, 68);
            this.cbYear.Name = "cbYear";
            this.cbYear.Size = new System.Drawing.Size(121, 21);
            this.cbYear.TabIndex = 3;
            // 
            // btnGenPayroll
            // 
            this.btnGenPayroll.Location = new System.Drawing.Point(230, 55);
            this.btnGenPayroll.Name = "btnGenPayroll";
            this.btnGenPayroll.Size = new System.Drawing.Size(111, 34);
            this.btnGenPayroll.TabIndex = 5;
            this.btnGenPayroll.Text = "Generate Payroll";
            this.btnGenPayroll.UseVisualStyleBackColor = true;
            this.btnGenPayroll.Click += new System.EventHandler(this.btnGenPayroll_Click);
            // 
            // lblProcessing
            // 
            this.lblProcessing.AutoSize = true;
            this.lblProcessing.Location = new System.Drawing.Point(450, 35);
            this.lblProcessing.Name = "lblProcessing";
            this.lblProcessing.Size = new System.Drawing.Size(0, 13);
            this.lblProcessing.TabIndex = 8;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(12, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(727, 468);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblMonth);
            this.groupBox3.Controls.Add(this.cbMonth);
            this.groupBox3.Controls.Add(this.lblProcessing);
            this.groupBox3.Controls.Add(this.lblYear);
            this.groupBox3.Controls.Add(this.btnGenPayroll);
            this.groupBox3.Controls.Add(this.cbYear);
            this.groupBox3.Location = new System.Drawing.Point(7, 19);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(714, 107);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lvPayroll);
            this.groupBox2.Location = new System.Drawing.Point(7, 132);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(714, 330);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            // 
            // lvPayroll
            // 
            this.lvPayroll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lvPayroll.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chImg,
            this.chEmpName,
            this.chAdvance,
            this.chOt,
            this.chStatus});
            this.lvPayroll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvPayroll.FullRowSelect = true;
            this.lvPayroll.Location = new System.Drawing.Point(6, 12);
            this.lvPayroll.MultiSelect = false;
            this.lvPayroll.Name = "lvPayroll";
            this.lvPayroll.ShowItemToolTips = true;
            this.lvPayroll.Size = new System.Drawing.Size(708, 312);
            this.lvPayroll.TabIndex = 9;
            this.lvPayroll.UseCompatibleStateImageBehavior = false;
            this.lvPayroll.View = System.Windows.Forms.View.Details;
            // 
            // chImg
            // 
            this.chImg.Text = "";
            this.chImg.Width = 70;
            // 
            // chEmpName
            // 
            this.chEmpName.Text = "Name";
            this.chEmpName.Width = 200;
            // 
            // chAdvance
            // 
            this.chAdvance.Text = "Advance";
            this.chAdvance.Width = 150;
            // 
            // chOt
            // 
            this.chOt.Text = "Over Time (hrs.)";
            this.chOt.Width = 150;
            // 
            // chStatus
            // 
            this.chStatus.Text = "Status";
            this.chStatus.Width = 150;
            // 
            // frmGeneratePayroll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 486);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "frmGeneratePayroll";
            this.Text = "Generate Payroll";
            this.Load += new System.EventHandler(this.FrmGeneratePayrollLoad);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbMonth;
        private System.Windows.Forms.Label lblMonth;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.ComboBox cbYear;
        private System.Windows.Forms.Button btnGenPayroll;
        private System.Windows.Forms.Label lblProcessing;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView lvPayroll;
        private System.Windows.Forms.ColumnHeader chImg;
        private System.Windows.Forms.ColumnHeader chEmpName;
        private System.Windows.Forms.ColumnHeader chAdvance;
        private System.Windows.Forms.ColumnHeader chOt;
        private System.Windows.Forms.ColumnHeader chStatus;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}