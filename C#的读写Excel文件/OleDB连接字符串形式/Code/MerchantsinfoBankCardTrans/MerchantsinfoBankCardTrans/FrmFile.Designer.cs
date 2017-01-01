namespace MerchantsinfoBankCardTrans
{
    partial class FrmFile
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tbFileCount = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnGatData = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.btnOutput = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 202);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(762, 267);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // tbFileCount
            // 
            this.tbFileCount.Location = new System.Drawing.Point(42, 12);
            this.tbFileCount.Name = "tbFileCount";
            this.tbFileCount.Size = new System.Drawing.Size(39, 21);
            this.tbFileCount.TabIndex = 2;
            this.tbFileCount.Text = "5";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(96, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "指定文件数量";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnGatData
            // 
            this.btnGatData.Location = new System.Drawing.Point(314, 486);
            this.btnGatData.Name = "btnGatData";
            this.btnGatData.Size = new System.Drawing.Size(129, 23);
            this.btnGatData.TabIndex = 4;
            this.btnGatData.Text = "获取数据和创建文件";
            this.btnGatData.UseVisualStyleBackColor = true;
            this.btnGatData.Click += new System.EventHandler(this.btnGatData_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(212, 12);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(318, 21);
            this.txtOutput.TabIndex = 5;
            // 
            // btnOutput
            // 
            this.btnOutput.Location = new System.Drawing.Point(536, 12);
            this.btnOutput.Name = "btnOutput";
            this.btnOutput.Size = new System.Drawing.Size(75, 23);
            this.btnOutput.TabIndex = 6;
            this.btnOutput.Text = "输出目录";
            this.btnOutput.UseVisualStyleBackColor = true;
            this.btnOutput.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // FrmFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(786, 522);
            this.Controls.Add(this.btnOutput);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.btnGatData);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbFileCount);
            this.Controls.Add(this.richTextBox1);
            this.Name = "FrmFile";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox tbFileCount;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnGatData;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Button btnOutput;
    }
}

