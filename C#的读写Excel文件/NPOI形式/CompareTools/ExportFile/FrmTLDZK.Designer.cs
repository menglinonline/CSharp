namespace MerchantsinfoBankCardTrans
{
    partial class FrmDuiZhangDan
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
            this.btnSelectFolderName = new System.Windows.Forms.Button();
            this.txtFolderPath = new System.Windows.Forms.TextBox();
            this.btnGetData = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.btnOutput = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.clbTraderList = new System.Windows.Forms.CheckedListBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSelectFolderName
            // 
            this.btnSelectFolderName.Location = new System.Drawing.Point(403, 42);
            this.btnSelectFolderName.Name = "btnSelectFolderName";
            this.btnSelectFolderName.Size = new System.Drawing.Size(75, 23);
            this.btnSelectFolderName.TabIndex = 0;
            this.btnSelectFolderName.Text = "源目录";
            this.btnSelectFolderName.UseVisualStyleBackColor = true;
            this.btnSelectFolderName.Click += new System.EventHandler(this.btnSelectFolderName_Click);
            // 
            // txtFolderPath
            // 
            this.txtFolderPath.Location = new System.Drawing.Point(95, 44);
            this.txtFolderPath.Name = "txtFolderPath";
            this.txtFolderPath.Size = new System.Drawing.Size(302, 21);
            this.txtFolderPath.TabIndex = 1;
            // 
            // btnGetData
            // 
            this.btnGetData.Location = new System.Drawing.Point(183, 112);
            this.btnGetData.Name = "btnGetData";
            this.btnGetData.Size = new System.Drawing.Size(90, 23);
            this.btnGetData.TabIndex = 2;
            this.btnGetData.Text = "导出文件";
            this.btnGetData.UseVisualStyleBackColor = true;
            this.btnGetData.Click += new System.EventHandler(this.btnGetData_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(95, 76);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(302, 21);
            this.txtOutput.TabIndex = 4;
            // 
            // btnOutput
            // 
            this.btnOutput.Location = new System.Drawing.Point(403, 72);
            this.btnOutput.Name = "btnOutput";
            this.btnOutput.Size = new System.Drawing.Size(75, 23);
            this.btnOutput.TabIndex = 5;
            this.btnOutput.Text = "输出目录";
            this.btnOutput.UseVisualStyleBackColor = true;
            this.btnOutput.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "选择输出目录：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "选择源目录：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.clbTraderList);
            this.groupBox1.Location = new System.Drawing.Point(484, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(276, 154);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择需要导出的商户";
            // 
            // clbTraderList
            // 
            this.clbTraderList.FormattingEnabled = true;
            this.clbTraderList.Location = new System.Drawing.Point(9, 13);
            this.clbTraderList.Name = "clbTraderList";
            this.clbTraderList.Size = new System.Drawing.Size(256, 132);
            this.clbTraderList.TabIndex = 18;
            // 
            // FrmDuiZhangDan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 168);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnOutput);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.btnGetData);
            this.Controls.Add(this.txtFolderPath);
            this.Controls.Add(this.btnSelectFolderName);
            this.MaximizeBox = false;
            this.Name = "FrmDuiZhangDan";
            this.Text = "通联商户银行卡交易对账单导出";
            this.Load += new System.EventHandler(this.FrmDuiZhangDan_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelectFolderName;
        private System.Windows.Forms.TextBox txtFolderPath;
        private System.Windows.Forms.Button btnGetData;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Button btnOutput;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox clbTraderList;
    }
}