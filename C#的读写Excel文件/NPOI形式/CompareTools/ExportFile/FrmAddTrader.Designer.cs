namespace CompareTools
{
    partial class FrmAddTrader
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
            this.cbTrader5 = new System.Windows.Forms.CheckBox();
            this.cbTrader1 = new System.Windows.Forms.CheckBox();
            this.cbTrader4 = new System.Windows.Forms.CheckBox();
            this.cbTrader2 = new System.Windows.Forms.CheckBox();
            this.cbTrader3 = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnTrader = new System.Windows.Forms.Button();
            this.txtTraderNo = new System.Windows.Forms.TextBox();
            this.txtTraderName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbTraderList = new System.Windows.Forms.ListBox();
            this.btnDeleteTrader = new System.Windows.Forms.Button();
            this.cbTrader6 = new System.Windows.Forms.CheckBox();
            this.cbTrader7 = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbTrader7);
            this.groupBox1.Controls.Add(this.cbTrader5);
            this.groupBox1.Controls.Add(this.cbTrader1);
            this.groupBox1.Controls.Add(this.cbTrader4);
            this.groupBox1.Controls.Add(this.cbTrader2);
            this.groupBox1.Controls.Add(this.cbTrader3);
            this.groupBox1.Location = new System.Drawing.Point(29, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(243, 180);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "请选择要添加的商户号";
            // 
            // cbTrader5
            // 
            this.cbTrader5.AutoSize = true;
            this.cbTrader5.Location = new System.Drawing.Point(15, 112);
            this.cbTrader5.Name = "cbTrader5";
            this.cbTrader5.Size = new System.Drawing.Size(126, 16);
            this.cbTrader5.TabIndex = 13;
            this.cbTrader5.Text = "通联信用卡 商户号";
            this.cbTrader5.UseVisualStyleBackColor = true;
            this.cbTrader5.Click += new System.EventHandler(this.cbTrader_Click);
            // 
            // cbTrader1
            // 
            this.cbTrader1.AutoSize = true;
            this.cbTrader1.Location = new System.Drawing.Point(15, 20);
            this.cbTrader1.Name = "cbTrader1";
            this.cbTrader1.Size = new System.Drawing.Size(198, 16);
            this.cbTrader1.TabIndex = 9;
            this.cbTrader1.Text = "系统订单和银行订单对账 商户号";
            this.cbTrader1.UseVisualStyleBackColor = true;
            this.cbTrader1.Click += new System.EventHandler(this.cbTrader_Click);
            // 
            // cbTrader4
            // 
            this.cbTrader4.AutoSize = true;
            this.cbTrader4.Location = new System.Drawing.Point(15, 88);
            this.cbTrader4.Name = "cbTrader4";
            this.cbTrader4.Size = new System.Drawing.Size(144, 16);
            this.cbTrader4.TabIndex = 12;
            this.cbTrader4.Text = "日日结|随心付商户号";
            this.cbTrader4.UseVisualStyleBackColor = true;
            this.cbTrader4.Click += new System.EventHandler(this.cbTrader_Click);
            // 
            // cbTrader2
            // 
            this.cbTrader2.AutoSize = true;
            this.cbTrader2.Location = new System.Drawing.Point(15, 43);
            this.cbTrader2.Name = "cbTrader2";
            this.cbTrader2.Size = new System.Drawing.Size(114, 16);
            this.cbTrader2.TabIndex = 10;
            this.cbTrader2.Text = "文件抽取 商户号";
            this.cbTrader2.UseVisualStyleBackColor = true;
            this.cbTrader2.Click += new System.EventHandler(this.cbTrader_Click);
            // 
            // cbTrader3
            // 
            this.cbTrader3.AutoSize = true;
            this.cbTrader3.Location = new System.Drawing.Point(15, 65);
            this.cbTrader3.Name = "cbTrader3";
            this.cbTrader3.Size = new System.Drawing.Size(126, 16);
            this.cbTrader3.TabIndex = 11;
            this.cbTrader3.Text = "通联对账单 商户号";
            this.cbTrader3.UseVisualStyleBackColor = true;
            this.cbTrader3.Click += new System.EventHandler(this.cbTrader_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnTrader);
            this.groupBox3.Controls.Add(this.txtTraderNo);
            this.groupBox3.Controls.Add(this.txtTraderName);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Location = new System.Drawing.Point(29, 222);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(243, 118);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "添加商户";
            // 
            // btnTrader
            // 
            this.btnTrader.Location = new System.Drawing.Point(73, 77);
            this.btnTrader.Name = "btnTrader";
            this.btnTrader.Size = new System.Drawing.Size(75, 23);
            this.btnTrader.TabIndex = 4;
            this.btnTrader.Text = "添加商户";
            this.btnTrader.UseVisualStyleBackColor = true;
            this.btnTrader.Click += new System.EventHandler(this.btnAddTrader_Click);
            // 
            // txtTraderNo
            // 
            this.txtTraderNo.Location = new System.Drawing.Point(73, 50);
            this.txtTraderNo.Name = "txtTraderNo";
            this.txtTraderNo.Size = new System.Drawing.Size(100, 21);
            this.txtTraderNo.TabIndex = 3;
            // 
            // txtTraderName
            // 
            this.txtTraderName.Location = new System.Drawing.Point(73, 19);
            this.txtTraderName.Name = "txtTraderName";
            this.txtTraderName.Size = new System.Drawing.Size(100, 21);
            this.txtTraderName.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "商户号：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "商户名：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbTraderList);
            this.groupBox2.Location = new System.Drawing.Point(29, 387);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(243, 214);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "已有商户";
            // 
            // lbTraderList
            // 
            this.lbTraderList.FormattingEnabled = true;
            this.lbTraderList.ItemHeight = 12;
            this.lbTraderList.Location = new System.Drawing.Point(15, 19);
            this.lbTraderList.Name = "lbTraderList";
            this.lbTraderList.Size = new System.Drawing.Size(222, 184);
            this.lbTraderList.TabIndex = 0;
            // 
            // btnDeleteTrader
            // 
            this.btnDeleteTrader.Location = new System.Drawing.Point(191, 358);
            this.btnDeleteTrader.Name = "btnDeleteTrader";
            this.btnDeleteTrader.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteTrader.TabIndex = 5;
            this.btnDeleteTrader.Text = "删除商户";
            this.btnDeleteTrader.UseVisualStyleBackColor = true;
            this.btnDeleteTrader.Click += new System.EventHandler(this.btnDeleteTrader_Click);
            // 
            // cbTrader6
            // 
            this.cbTrader6.AutoSize = true;
            this.cbTrader6.Location = new System.Drawing.Point(44, 147);
            this.cbTrader6.Name = "cbTrader6";
            this.cbTrader6.Size = new System.Drawing.Size(102, 16);
            this.cbTrader6.TabIndex = 14;
            this.cbTrader6.Text = "银联RD 商户号";
            this.cbTrader6.UseVisualStyleBackColor = true;
            this.cbTrader6.Click += new System.EventHandler(this.cbTrader_Click);
            // 
            // cbTrader7
            // 
            this.cbTrader7.AutoSize = true;
            this.cbTrader7.Location = new System.Drawing.Point(15, 156);
            this.cbTrader7.Name = "cbTrader7";
            this.cbTrader7.Size = new System.Drawing.Size(138, 16);
            this.cbTrader7.TabIndex = 15;
            this.cbTrader7.Text = "银联RD信用卡 商户号";
            this.cbTrader7.UseVisualStyleBackColor = true;
            this.cbTrader7.Click += new System.EventHandler(this.cbTrader_Click);
            // 
            // FrmAddTrader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 613);
            this.Controls.Add(this.cbTrader6);
            this.Controls.Add(this.btnDeleteTrader);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmAddTrader";
            this.Text = "添加商户";
            this.Load += new System.EventHandler(this.FrmAddTrader_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnTrader;
        private System.Windows.Forms.TextBox txtTraderNo;
        private System.Windows.Forms.TextBox txtTraderName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox lbTraderList;
        private System.Windows.Forms.Button btnDeleteTrader;
        private System.Windows.Forms.CheckBox cbTrader1;
        private System.Windows.Forms.CheckBox cbTrader2;
        private System.Windows.Forms.CheckBox cbTrader3;
        private System.Windows.Forms.CheckBox cbTrader4;
        private System.Windows.Forms.CheckBox cbTrader5;
        private System.Windows.Forms.CheckBox cbTrader6;
        private System.Windows.Forms.CheckBox cbTrader7;
    }
}