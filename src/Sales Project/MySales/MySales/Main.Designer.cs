namespace MySales
{
    partial class frmMain
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
            this.rdbAIM = new System.Windows.Forms.RadioButton();
            this.rdbPayroll = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnGo = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rdbAIM
            // 
            this.rdbAIM.AutoSize = true;
            this.rdbAIM.Enabled = false;
            this.rdbAIM.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbAIM.Location = new System.Drawing.Point(60, 37);
            this.rdbAIM.Name = "rdbAIM";
            this.rdbAIM.Size = new System.Drawing.Size(371, 20);
            this.rdbAIM.TabIndex = 0;
            this.rdbAIM.Text = "Account Information Manager (Under Construction)";
            this.rdbAIM.UseVisualStyleBackColor = true;
            // 
            // rdbPayroll
            // 
            this.rdbPayroll.AutoSize = true;
            this.rdbPayroll.Checked = true;
            this.rdbPayroll.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbPayroll.Location = new System.Drawing.Point(60, 84);
            this.rdbPayroll.Name = "rdbPayroll";
            this.rdbPayroll.Size = new System.Drawing.Size(155, 24);
            this.rdbPayroll.TabIndex = 1;
            this.rdbPayroll.TabStop = true;
            this.rdbPayroll.Text = "Payroll Manager";
            this.rdbPayroll.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnGo);
            this.groupBox1.Controls.Add(this.rdbAIM);
            this.groupBox1.Controls.Add(this.rdbPayroll);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(446, 183);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Where you want to go?";
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(182, 128);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(75, 23);
            this.btnGo.TabIndex = 2;
            this.btnGo.Text = "Go";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // frmMain
            // 
            this.AcceptButton = this.btnGo;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 226);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmMain";
            this.Text = "Main";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rdbAIM;
        private System.Windows.Forms.RadioButton rdbPayroll;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnGo;
    }
}