namespace AccountChecking
{
    partial class FrmSingleCheck
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
            this.btnCompare = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ctrPanel = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCent = new System.Windows.Forms.TextBox();
            this.txtRate = new System.Windows.Forms.TextBox();
            this.radGB2312 = new System.Windows.Forms.RadioButton();
            this.radUTF8 = new System.Windows.Forms.RadioButton();
            this.btnClearFileST = new System.Windows.Forms.Button();
            this.lstFileST = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radZSF = new System.Windows.Forms.RadioButton();
            this.radQtopay = new System.Windows.Forms.RadioButton();
            this.cbxTrader = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.radSpecial = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.radTenant = new System.Windows.Forms.RadioButton();
            this.radUnoin = new System.Windows.Forms.RadioButton();
            this.btnClearFileYH = new System.Windows.Forms.Button();
            this.lstFileYH = new System.Windows.Forms.ListBox();
            this.opendlgST = new System.Windows.Forms.OpenFileDialog();
            this.savedlg = new System.Windows.Forms.SaveFileDialog();
            this.groupBox2.SuspendLayout();
            this.ctrPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // opendlgYH
            // 
            this.opendlgYH.Multiselect = true;
            // 
            // btnCompare
            // 
            this.btnCompare.Location = new System.Drawing.Point(369, 238);
            this.btnCompare.Name = "btnCompare";
            this.btnCompare.Size = new System.Drawing.Size(75, 23);
            this.btnCompare.TabIndex = 5;
            this.btnCompare.Text = "对比";
            this.btnCompare.UseVisualStyleBackColor = true;
            this.btnCompare.Click += new System.EventHandler(this.btnCompare_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ctrPanel);
            this.groupBox2.Controls.Add(this.radGB2312);
            this.groupBox2.Controls.Add(this.radUTF8);
            this.groupBox2.Controls.Add(this.btnClearFileST);
            this.groupBox2.Controls.Add(this.lstFileST);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox2.Location = new System.Drawing.Point(447, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(367, 479);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "系统账单";
            // 
            // ctrPanel
            // 
            this.ctrPanel.Controls.Add(this.label3);
            this.ctrPanel.Controls.Add(this.label4);
            this.ctrPanel.Controls.Add(this.txtCent);
            this.ctrPanel.Controls.Add(this.txtRate);
            this.ctrPanel.Location = new System.Drawing.Point(6, 51);
            this.ctrPanel.Name = "ctrPanel";
            this.ctrPanel.Size = new System.Drawing.Size(252, 35);
            this.ctrPanel.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(18, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 14);
            this.label3.TabIndex = 6;
            this.label3.Text = "手续费率：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(156, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "% ";
            // 
            // txtCent
            // 
            this.txtCent.Location = new System.Drawing.Point(196, 10);
            this.txtCent.MaxLength = 4;
            this.txtCent.Name = "txtCent";
            this.txtCent.Size = new System.Drawing.Size(53, 21);
            this.txtCent.TabIndex = 7;
            this.txtCent.Text = "0.02";
            // 
            // txtRate
            // 
            this.txtRate.Location = new System.Drawing.Point(101, 10);
            this.txtRate.MaxLength = 4;
            this.txtRate.Name = "txtRate";
            this.txtRate.Size = new System.Drawing.Size(53, 21);
            this.txtRate.TabIndex = 7;
            this.txtRate.Text = "0";
            // 
            // radGB2312
            // 
            this.radGB2312.AutoSize = true;
            this.radGB2312.Checked = true;
            this.radGB2312.Location = new System.Drawing.Point(27, 28);
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
            this.radUTF8.Location = new System.Drawing.Point(121, 28);
            this.radUTF8.Name = "radUTF8";
            this.radUTF8.Size = new System.Drawing.Size(53, 16);
            this.radUTF8.TabIndex = 5;
            this.radUTF8.TabStop = true;
            this.radUTF8.Text = "UTF-8";
            this.radUTF8.UseVisualStyleBackColor = true;
            // 
            // btnClearFileST
            // 
            this.btnClearFileST.Location = new System.Drawing.Point(264, 63);
            this.btnClearFileST.Name = "btnClearFileST";
            this.btnClearFileST.Size = new System.Drawing.Size(75, 23);
            this.btnClearFileST.TabIndex = 4;
            this.btnClearFileST.Text = "清空文件";
            this.btnClearFileST.UseVisualStyleBackColor = true;
            this.btnClearFileST.Click += new System.EventHandler(this.btnClearFileST_Click);
            // 
            // lstFileST
            // 
            this.lstFileST.AllowDrop = true;
            this.lstFileST.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lstFileST.FormattingEnabled = true;
            this.lstFileST.ItemHeight = 12;
            this.lstFileST.Location = new System.Drawing.Point(3, 112);
            this.lstFileST.Name = "lstFileST";
            this.lstFileST.Size = new System.Drawing.Size(361, 364);
            this.lstFileST.TabIndex = 2;
            this.lstFileST.DragDrop += new System.Windows.Forms.DragEventHandler(this.lstFileST_DragDrop);
            this.lstFileST.DragEnter += new System.Windows.Forms.DragEventHandler(this.lstFileST_DragEnter);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radZSF);
            this.groupBox1.Controls.Add(this.radQtopay);
            this.groupBox1.Controls.Add(this.cbxTrader);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.radSpecial);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.radTenant);
            this.groupBox1.Controls.Add(this.radUnoin);
            this.groupBox1.Controls.Add(this.btnClearFileYH);
            this.groupBox1.Controls.Add(this.lstFileYH);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(363, 479);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "银行账单";
            // 
            // radZSF
            // 
            this.radZSF.AutoSize = true;
            this.radZSF.Location = new System.Drawing.Point(173, 49);
            this.radZSF.Name = "radZSF";
            this.radZSF.Size = new System.Drawing.Size(59, 16);
            this.radZSF.TabIndex = 16;
            this.radZSF.Text = "攒善付";
            this.radZSF.UseVisualStyleBackColor = true;
            this.radZSF.CheckedChanged += new System.EventHandler(this.radZSF_CheckedChanged);
            // 
            // radQtopay
            // 
            this.radQtopay.AutoSize = true;
            this.radQtopay.Location = new System.Drawing.Point(96, 49);
            this.radQtopay.Name = "radQtopay";
            this.radQtopay.Size = new System.Drawing.Size(71, 16);
            this.radQtopay.TabIndex = 15;
            this.radQtopay.Text = "中付支付";
            this.radQtopay.UseVisualStyleBackColor = true;
            this.radQtopay.CheckedChanged += new System.EventHandler(this.radQtopay_CheckedChanged);
            // 
            // cbxTrader
            // 
            this.cbxTrader.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTrader.FormattingEnabled = true;
            this.cbxTrader.Location = new System.Drawing.Point(74, 71);
            this.cbxTrader.Name = "cbxTrader";
            this.cbxTrader.Size = new System.Drawing.Size(134, 20);
            this.cbxTrader.TabIndex = 14;
            this.cbxTrader.SelectedIndexChanged += new System.EventHandler(this.cbxTrader_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "商户名称：";
            // 
            // radSpecial
            // 
            this.radSpecial.AutoSize = true;
            this.radSpecial.Location = new System.Drawing.Point(214, 26);
            this.radSpecial.Name = "radSpecial";
            this.radSpecial.Size = new System.Drawing.Size(71, 16);
            this.radSpecial.TabIndex = 12;
            this.radSpecial.Text = "通联商户";
            this.radSpecial.UseVisualStyleBackColor = true;
            this.radSpecial.CheckedChanged += new System.EventHandler(this.radSpecial_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "来源类型：";
            // 
            // radTenant
            // 
            this.radTenant.AutoSize = true;
            this.radTenant.Checked = true;
            this.radTenant.Location = new System.Drawing.Point(149, 26);
            this.radTenant.Name = "radTenant";
            this.radTenant.Size = new System.Drawing.Size(59, 16);
            this.radTenant.TabIndex = 7;
            this.radTenant.TabStop = true;
            this.radTenant.Text = "RD文件";
            this.radTenant.UseVisualStyleBackColor = true;
            this.radTenant.CheckedChanged += new System.EventHandler(this.radTenant_CheckedChanged);
            // 
            // radUnoin
            // 
            this.radUnoin.AutoSize = true;
            this.radUnoin.Location = new System.Drawing.Point(96, 26);
            this.radUnoin.Name = "radUnoin";
            this.radUnoin.Size = new System.Drawing.Size(47, 16);
            this.radUnoin.TabIndex = 6;
            this.radUnoin.Text = "银联";
            this.radUnoin.UseVisualStyleBackColor = true;
            this.radUnoin.CheckedChanged += new System.EventHandler(this.radUnoin_CheckedChanged);
            // 
            // btnClearFileYH
            // 
            this.btnClearFileYH.Location = new System.Drawing.Point(214, 69);
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
            this.lstFileYH.Location = new System.Drawing.Point(3, 112);
            this.lstFileYH.Name = "lstFileYH";
            this.lstFileYH.Size = new System.Drawing.Size(357, 364);
            this.lstFileYH.TabIndex = 1;
            this.lstFileYH.DragDrop += new System.Windows.Forms.DragEventHandler(this.lstFileYH_DragDrop);
            this.lstFileYH.DragEnter += new System.Windows.Forms.DragEventHandler(this.lstFileYH_DragEnter);
            // 
            // opendlgST
            // 
            this.opendlgST.FileName = "openFileDialog1";
            this.opendlgST.Multiselect = true;
            // 
            // savedlg
            // 
            this.savedlg.Filter = "xls|*.xls|xlsx|.*xlsx";
            // 
            // FrmSingleCheck
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 479);
            this.Controls.Add(this.btnCompare);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmSingleCheck";
            this.Text = "对账";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ctrPanel.ResumeLayout(false);
            this.ctrPanel.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog opendlgYH;
        private System.Windows.Forms.Button btnCompare;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnClearFileST;
        private System.Windows.Forms.ListBox lstFileST;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnClearFileYH;
        private System.Windows.Forms.ListBox lstFileYH;
        private System.Windows.Forms.OpenFileDialog opendlgST;
        private System.Windows.Forms.SaveFileDialog savedlg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radTenant;
        private System.Windows.Forms.RadioButton radUnoin;
        private System.Windows.Forms.RadioButton radSpecial;
        private System.Windows.Forms.ComboBox cbxTrader;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radQtopay;
        private System.Windows.Forms.RadioButton radGB2312;
        private System.Windows.Forms.RadioButton radUTF8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtRate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel ctrPanel;
        private System.Windows.Forms.TextBox txtCent;
        private System.Windows.Forms.RadioButton radZSF;
    }
}

