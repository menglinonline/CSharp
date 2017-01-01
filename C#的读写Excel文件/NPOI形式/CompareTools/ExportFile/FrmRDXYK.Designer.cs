namespace MerchantsinfoBankCardTrans
{
    partial class FrmRDXYK
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
            this.txtFolderPath = new System.Windows.Forms.TextBox();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.btnOutputFolder = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOuoutFile = new System.Windows.Forms.Button();
            this.btnSelectFolderName = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.clbTraderList = new System.Windows.Forms.CheckedListBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtFolderPath
            // 
            this.txtFolderPath.Location = new System.Drawing.Point(107, 12);
            this.txtFolderPath.Name = "txtFolderPath";
            this.txtFolderPath.Size = new System.Drawing.Size(338, 21);
            this.txtFolderPath.TabIndex = 0;
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(107, 56);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(338, 21);
            this.txtOutput.TabIndex = 1;
            // 
            // btnOutputFolder
            // 
            this.btnOutputFolder.Location = new System.Drawing.Point(451, 56);
            this.btnOutputFolder.Name = "btnOutputFolder";
            this.btnOutputFolder.Size = new System.Drawing.Size(75, 23);
            this.btnOutputFolder.TabIndex = 2;
            this.btnOutputFolder.Text = "输出目录";
            this.btnOutputFolder.UseVisualStyleBackColor = true;
            this.btnOutputFolder.Click += new System.EventHandler(this.btnOutputFolder_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "选择输出目录：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "RD文件：";
            // 
            // btnOuoutFile
            // 
            this.btnOuoutFile.Location = new System.Drawing.Point(227, 93);
            this.btnOuoutFile.Name = "btnOuoutFile";
            this.btnOuoutFile.Size = new System.Drawing.Size(75, 23);
            this.btnOuoutFile.TabIndex = 5;
            this.btnOuoutFile.Text = "导出文件";
            this.btnOuoutFile.UseVisualStyleBackColor = true;
            this.btnOuoutFile.Click += new System.EventHandler(this.btnOuoutFile_Click);
            // 
            // btnSelectFolderName
            // 
            this.btnSelectFolderName.Location = new System.Drawing.Point(451, 12);
            this.btnSelectFolderName.Name = "btnSelectFolderName";
            this.btnSelectFolderName.Size = new System.Drawing.Size(75, 23);
            this.btnSelectFolderName.TabIndex = 11;
            this.btnSelectFolderName.Text = "源目录";
            this.btnSelectFolderName.UseVisualStyleBackColor = true;
            this.btnSelectFolderName.Click += new System.EventHandler(this.btnSelectFolderName_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.clbTraderList);
            this.groupBox1.Location = new System.Drawing.Point(533, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(236, 137);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择需要导出的商户";
            // 
            // clbTraderList
            // 
            this.clbTraderList.FormattingEnabled = true;
            this.clbTraderList.Location = new System.Drawing.Point(9, 13);
            this.clbTraderList.Name = "clbTraderList";
            this.clbTraderList.Size = new System.Drawing.Size(215, 116);
            this.clbTraderList.TabIndex = 18;
            // 
            // FrmRDXYK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 144);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSelectFolderName);
            this.Controls.Add(this.btnOuoutFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOutputFolder);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.txtFolderPath);
            this.MaximizeBox = false;
            this.Name = "FrmRDXYK";
            this.Text = "RD信用卡导出";
            this.Load += new System.EventHandler(this.FrmRD_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFolderPath;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Button btnOutputFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOuoutFile;
        private System.Windows.Forms.Button btnSelectFolderName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox clbTraderList;
    }
}