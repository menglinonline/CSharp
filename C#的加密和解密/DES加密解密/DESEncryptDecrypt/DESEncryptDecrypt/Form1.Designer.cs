namespace DESEncryptDecrypt
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnDESEncrypt = new System.Windows.Forms.Button();
            this.txtDESEncrypt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDESDecrypt = new System.Windows.Forms.TextBox();
            this.btnDESDecrypt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "加密内容：";
            // 
            // btnDESEncrypt
            // 
            this.btnDESEncrypt.Location = new System.Drawing.Point(298, 136);
            this.btnDESEncrypt.Name = "btnDESEncrypt";
            this.btnDESEncrypt.Size = new System.Drawing.Size(91, 32);
            this.btnDESEncrypt.TabIndex = 1;
            this.btnDESEncrypt.Text = "DES加密";
            this.btnDESEncrypt.UseVisualStyleBackColor = true;
            this.btnDESEncrypt.Click += new System.EventHandler(this.btnDESEncrypt_Click);
            // 
            // txtDESEncrypt
            // 
            this.txtDESEncrypt.Location = new System.Drawing.Point(98, 33);
            this.txtDESEncrypt.Multiline = true;
            this.txtDESEncrypt.Name = "txtDESEncrypt";
            this.txtDESEncrypt.Size = new System.Drawing.Size(533, 87);
            this.txtDESEncrypt.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 197);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "解密内容：";
            // 
            // txtDESDecrypt
            // 
            this.txtDESDecrypt.Location = new System.Drawing.Point(98, 197);
            this.txtDESDecrypt.Multiline = true;
            this.txtDESDecrypt.Name = "txtDESDecrypt";
            this.txtDESDecrypt.Size = new System.Drawing.Size(533, 87);
            this.txtDESDecrypt.TabIndex = 4;
            // 
            // btnDESDecrypt
            // 
            this.btnDESDecrypt.Location = new System.Drawing.Point(298, 300);
            this.btnDESDecrypt.Name = "btnDESDecrypt";
            this.btnDESDecrypt.Size = new System.Drawing.Size(91, 29);
            this.btnDESDecrypt.TabIndex = 5;
            this.btnDESDecrypt.Text = "DES解密";
            this.btnDESDecrypt.UseVisualStyleBackColor = true;
            this.btnDESDecrypt.Click += new System.EventHandler(this.btnDESDecrypt_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 343);
            this.Controls.Add(this.btnDESDecrypt);
            this.Controls.Add(this.txtDESDecrypt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDESEncrypt);
            this.Controls.Add(this.btnDESEncrypt);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "DES加密解密";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDESEncrypt;
        private System.Windows.Forms.TextBox txtDESEncrypt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDESDecrypt;
        private System.Windows.Forms.Button btnDESDecrypt;
    }
}

