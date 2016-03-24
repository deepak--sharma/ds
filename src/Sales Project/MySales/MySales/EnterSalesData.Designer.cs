namespace MySales
{
    partial class EnterSalesData
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.lblItem = new System.Windows.Forms.Label();
            this.txtItem = new System.Windows.Forms.TextBox();
            this.txtClient = new System.Windows.Forms.TextBox();
            this.lblClient = new System.Windows.Forms.Label();
            this.txtDealer = new System.Windows.Forms.TextBox();
            this.lblDealer = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBalance = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ddlWarranty = new System.Windows.Forms.ComboBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.lnkHome = new System.Windows.Forms.LinkLabel();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtDate = new System.Windows.Forms.DateTimePicker();
            this.lnkClear = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(88, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(224, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enter your sales information";
            // 
            // lblItem
            // 
            this.lblItem.AutoSize = true;
            this.lblItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItem.Location = new System.Drawing.Point(89, 90);
            this.lblItem.Name = "lblItem";
            this.lblItem.Size = new System.Drawing.Size(39, 13);
            this.lblItem.TabIndex = 1;
            this.lblItem.Text = "Item :";
            // 
            // txtItem
            // 
            this.txtItem.Location = new System.Drawing.Point(177, 83);
            this.txtItem.Name = "txtItem";
            this.txtItem.Size = new System.Drawing.Size(172, 20);
            this.txtItem.TabIndex = 2;
            // 
            // txtClient
            // 
            this.txtClient.Location = new System.Drawing.Point(177, 118);
            this.txtClient.Name = "txtClient";
            this.txtClient.Size = new System.Drawing.Size(172, 20);
            this.txtClient.TabIndex = 4;
            this.txtClient.Leave += new System.EventHandler(this.txtClient_Leave);
            // 
            // lblClient
            // 
            this.lblClient.AutoSize = true;
            this.lblClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClient.Location = new System.Drawing.Point(89, 125);
            this.lblClient.Name = "lblClient";
            this.lblClient.Size = new System.Drawing.Size(83, 13);
            this.lblClient.TabIndex = 3;
            this.lblClient.Text = "Client Name :";
            // 
            // txtDealer
            // 
            this.txtDealer.Location = new System.Drawing.Point(177, 153);
            this.txtDealer.Name = "txtDealer";
            this.txtDealer.Size = new System.Drawing.Size(172, 20);
            this.txtDealer.TabIndex = 6;
            this.txtDealer.Leave += new System.EventHandler(this.txtDealer_Leave);
            // 
            // lblDealer
            // 
            this.lblDealer.AutoSize = true;
            this.lblDealer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDealer.Location = new System.Drawing.Point(89, 160);
            this.lblDealer.Name = "lblDealer";
            this.lblDealer.Size = new System.Drawing.Size(52, 13);
            this.lblDealer.TabIndex = 5;
            this.lblDealer.Text = "Dealer :";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(89, 196);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(82, 13);
            this.lblDate.TabIndex = 7;
            this.lblDate.Text = "Purchase date :";
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(177, 274);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(172, 20);
            this.txtAmount.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(89, 281);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Amount :";
            // 
            // txtBalance
            // 
            this.txtBalance.Location = new System.Drawing.Point(177, 311);
            this.txtBalance.Name = "txtBalance";
            this.txtBalance.Size = new System.Drawing.Size(172, 20);
            this.txtBalance.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(89, 318);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Balance :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(89, 242);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Warranty :";
            // 
            // ddlWarranty
            // 
            this.ddlWarranty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlWarranty.FormattingEnabled = true;
            this.ddlWarranty.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24"});
            this.ddlWarranty.Location = new System.Drawing.Point(177, 233);
            this.ddlWarranty.Name = "ddlWarranty";
            this.ddlWarranty.Size = new System.Drawing.Size(172, 21);
            this.ddlWarranty.TabIndex = 10;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(134, 364);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 15;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // lnkHome
            // 
            this.lnkHome.AutoSize = true;
            this.lnkHome.Location = new System.Drawing.Point(294, 374);
            this.lnkHome.Name = "lnkHome";
            this.lnkHome.Size = new System.Drawing.Size(73, 13);
            this.lnkHome.TabIndex = 16;
            this.lnkHome.TabStop = true;
            this.lnkHome.Text = "Back to home";
            this.lnkHome.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkHome_LinkClicked);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // txtDate
            // 
            this.txtDate.Location = new System.Drawing.Point(177, 196);
            this.txtDate.Name = "txtDate";
            this.txtDate.Size = new System.Drawing.Size(172, 20);
            this.txtDate.TabIndex = 17;
            // 
            // lnkClear
            // 
            this.lnkClear.AutoSize = true;
            this.lnkClear.Location = new System.Drawing.Point(233, 374);
            this.lnkClear.Name = "lnkClear";
            this.lnkClear.Size = new System.Drawing.Size(57, 13);
            this.lnkClear.TabIndex = 18;
            this.lnkClear.TabStop = true;
            this.lnkClear.Text = "Clear Form";
            this.lnkClear.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkClear_LinkClicked);
            // 
            // EnterSalesData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 470);
            this.Controls.Add(this.lnkClear);
            this.Controls.Add(this.txtDate);
            this.Controls.Add(this.lnkHome);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.ddlWarranty);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtBalance);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.txtDealer);
            this.Controls.Add(this.lblDealer);
            this.Controls.Add(this.txtClient);
            this.Controls.Add(this.lblClient);
            this.Controls.Add(this.txtItem);
            this.Controls.Add(this.lblItem);
            this.Controls.Add(this.label1);
            this.Name = "EnterSalesData";
            this.Text = "Enter Sales Data";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EnterSalesData_FormClosed);
            this.Load += new System.EventHandler(this.EnterSalesData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblItem;
        private System.Windows.Forms.TextBox txtItem;
        private System.Windows.Forms.TextBox txtClient;
        private System.Windows.Forms.Label lblClient;
        private System.Windows.Forms.TextBox txtDealer;
        private System.Windows.Forms.Label lblDealer;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBalance;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox ddlWarranty;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.LinkLabel lnkHome;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.DateTimePicker txtDate;
        private System.Windows.Forms.LinkLabel lnkClear;
    }
}