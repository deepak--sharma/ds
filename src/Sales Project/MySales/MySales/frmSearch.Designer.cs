namespace MySales
{
    partial class frmSearch
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
            this.dgSalesData = new System.Windows.Forms.DataGridView();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lnkHome = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dgSalesData)).BeginInit();
            this.SuspendLayout();
            // 
            // dgSalesData
            // 
            this.dgSalesData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSalesData.Location = new System.Drawing.Point(12, 149);
            this.dgSalesData.Name = "dgSalesData";
            this.dgSalesData.Size = new System.Drawing.Size(698, 309);
            this.dgSalesData.TabIndex = 0;
            this.dgSalesData.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgSalesData_CellClick);
            this.dgSalesData.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgSalesData_CellFormatting);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(12, 86);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lnkHome
            // 
            this.lnkHome.AutoSize = true;
            this.lnkHome.Location = new System.Drawing.Point(107, 96);
            this.lnkHome.Name = "lnkHome";
            this.lnkHome.Size = new System.Drawing.Size(73, 13);
            this.lnkHome.TabIndex = 17;
            this.lnkHome.TabStop = true;
            this.lnkHome.Text = "Back to home";
            this.lnkHome.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkHome_LinkClicked);
            // 
            // frmSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 470);
            this.Controls.Add(this.lnkHome);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dgSalesData);
            this.Name = "frmSearch";
            this.Text = "Search Form";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmSearch_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dgSalesData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgSalesData;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.LinkLabel lnkHome;
    }
}