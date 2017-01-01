namespace AccountChecking
{
    partial class FrmSingleConvert
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.opendlgYH = new System.Windows.Forms.OpenFileDialog();
            this.radGB2312 = new System.Windows.Forms.RadioButton();
            this.radUTF8 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnConvert = new System.Windows.Forms.Button();
            this.cbxTrader = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClearFileYH = new System.Windows.Forms.Button();
            this.lstFileYH = new System.Windows.Forms.ListBox();
            this.folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // opendlgYH
            // 
            this.opendlgYH.Multiselect = true;
            // 
            // radGB2312
            // 
            this.radGB2312.AutoSize = true;
            this.radGB2312.Checked = true;
            this.radGB2312.Location = new System.Drawing.Point(103, 26);
            this.radGB2312.Name = "radGB2312";
            this.radGB2312.Size = new System.Drawing.Size(59, 16);
            this.radGB2312.TabIndex = 5;
            this.radGB2312.TabStop = true;
            this.radGB2312.Text = "GB2312";
            this.radGB2312.UseVisualStyleBackColor = true;
            // 
            // radUTF8
            // 
            this.radUTF8.AutoSize = true;
            this.radUTF8.Location = new System.Drawing.Point(181, 26);
            this.radUTF8.Name = "radUTF8";
            this.radUTF8.Size = new System.Drawing.Size(53, 16);
            this.radUTF8.TabIndex = 5;
            this.radUTF8.TabStop = true;
            this.radUTF8.Text = "UTF-8";
            this.radUTF8.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnConvert);
            this.groupBox1.Controls.Add(this.radUTF8);
            this.groupBox1.Controls.Add(this.radGB2312);
            this.groupBox1.Controls.Add(this.cbxTrader);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnClearFileYH);
            this.groupBox1.Controls.Add(this.lstFileYH);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(363, 605);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(263, 55);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(75, 23);
            this.btnConvert.TabIndex = 15;
            this.btnConvert.Text = "转换";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // cbxTrader
            // 
            this.cbxTrader.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTrader.FormattingEnabled = true;
            this.cbxTrader.Location = new System.Drawing.Point(74, 54);
            this.cbxTrader.Name = "cbxTrader";
            this.cbxTrader.Size = new System.Drawing.Size(134, 20);
            this.cbxTrader.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "商户名称：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "文件编码：";
            // 
            // btnClearFileYH
            // 
            this.btnClearFileYH.Location = new System.Drawing.Point(263, 23);
            this.btnClearFileYH.Name = "btnClearFileYH";
            this.btnClearFileYH.Size = new System.Drawing.Size(75, 23);
            this.btnClearFileYH.TabIndex = 5;
            this.btnClearFileYH.Text = "清空文件";
            this.btnClearFileYH.UseVisualStyleBackColor = true;
            this.btnClearFileYH.Click += new System.EventHandler(this.btnClearFileYH_Click);
            // 
            // lstFileYH
            // 
            this.lstFileYH.AllowDrop = true;
            this.lstFileYH.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lstFileYH.FormattingEnabled = true;
            this.lstFileYH.ItemHeight = 12;
            this.lstFileYH.Location = new System.Drawing.Point(3, 214);
            this.lstFileYH.Name = "lstFileYH";
            this.lstFileYH.Size = new System.Drawing.Size(357, 388);
            this.lstFileYH.TabIndex = 1;
            this.lstFileYH.DragDrop += new System.Windows.Forms.DragEventHandler(this.lstFileYH_DragDrop);
            this.lstFileYH.DragEnter += new System.Windows.Forms.DragEventHandler(this.lstFileYH_DragEnter);
            // 
            // FrmSingleConvert
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 605);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmSingleConvert";
            this.Text = "转换";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog opendlgYH;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnClearFileYH;
        private System.Windows.Forms.ListBox lstFileYH;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxTrader;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radGB2312;
        private System.Windows.Forms.RadioButton radUTF8;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.FolderBrowserDialog folderBrowser;
    }
}

