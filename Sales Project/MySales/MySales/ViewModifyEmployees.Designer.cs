namespace MySales
{
    partial class ViewModifyEmployees
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSearchFN = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvEmp = new System.Windows.Forms.DataGridView();
            this.ssMessage = new System.Windows.Forms.StatusStrip();
            this.lblRcdCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmp)).BeginInit();
            this.ssMessage.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtSearchFN);
            this.groupBox1.Location = new System.Drawing.Point(18, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(661, 112);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search Filter";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "First name";
            // 
            // txtSearchFN
            // 
            this.txtSearchFN.Location = new System.Drawing.Point(77, 19);
            this.txtSearchFN.Name = "txtSearchFN";
            this.txtSearchFN.Size = new System.Drawing.Size(145, 20);
            this.txtSearchFN.TabIndex = 0;
            this.txtSearchFN.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSearchFN_KeyUp);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.dgvEmp);
            this.groupBox2.Location = new System.Drawing.Point(18, 152);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(661, 300);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Employee List";
            // 
            // dgvEmp
            // 
            this.dgvEmp.AllowUserToAddRows = false;
            this.dgvEmp.AllowUserToDeleteRows = false;
            this.dgvEmp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvEmp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEmp.Location = new System.Drawing.Point(7, 20);
            this.dgvEmp.MultiSelect = false;
            this.dgvEmp.Name = "dgvEmp";
            this.dgvEmp.ReadOnly = true;
            this.dgvEmp.Size = new System.Drawing.Size(648, 274);
            this.dgvEmp.TabIndex = 0;
            this.dgvEmp.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEmp_CellClick);
            this.dgvEmp.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvEmp_CellFormatting);
            // 
            // ssMessage
            // 
            this.ssMessage.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblRcdCount});
            this.ssMessage.Location = new System.Drawing.Point(0, 463);
            this.ssMessage.Name = "ssMessage";
            this.ssMessage.Size = new System.Drawing.Size(691, 22);
            this.ssMessage.TabIndex = 2;
            this.ssMessage.Text = "statusStrip1";
            // 
            // lblRcdCount
            // 
            this.lblRcdCount.Name = "lblRcdCount";
            this.lblRcdCount.Size = new System.Drawing.Size(0, 17);
            // 
            // ViewModifyEmployees
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 485);
            this.Controls.Add(this.ssMessage);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ViewModifyEmployees";
            this.Text = "View/Modify Employees";
            this.Load += new System.EventHandler(this.ViewModifyEmployees_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmp)).EndInit();
            this.ssMessage.ResumeLayout(false);
            this.ssMessage.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvEmp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSearchFN;
        private System.Windows.Forms.StatusStrip ssMessage;
        private System.Windows.Forms.ToolStripStatusLabel lblRcdCount;
    }
}