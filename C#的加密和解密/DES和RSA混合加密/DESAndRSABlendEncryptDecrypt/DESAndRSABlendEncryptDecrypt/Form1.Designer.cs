namespace DESAndRSABlendEncryptDecrypt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDESKey = new System.Windows.Forms.TextBox();
            this.txtDESValue = new System.Windows.Forms.TextBox();
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAPIPublicKey = new System.Windows.Forms.TextBox();
            this.txtSalePrivateKey = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtContent = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtSign = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtEncryptContent = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtEncryptDESKey = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtAPIPrivateKey = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtSalePublicKey = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btnDencrypt = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtContent2 = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "商户自己的DES Key：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(143, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "商户的自己的DES Value：";
            // 
            // txtDESKey
            // 
            this.txtDESKey.Location = new System.Drawing.Point(151, 33);
            this.txtDESKey.Name = "txtDESKey";
            this.txtDESKey.Size = new System.Drawing.Size(320, 21);
            this.txtDESKey.TabIndex = 2;
            this.txtDESKey.Text = "EMMF6KD6W3";
            // 
            // txtDESValue
            // 
            this.txtDESValue.Location = new System.Drawing.Point(151, 67);
            this.txtDESValue.Name = "txtDESValue";
            this.txtDESValue.Size = new System.Drawing.Size(320, 21);
            this.txtDESValue.TabIndex = 3;
            this.txtDESValue.Text = "E3BV88BWQULQCCTMX29XYELJFRBOMC5L";
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.Location = new System.Drawing.Point(187, 720);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(154, 23);
            this.btnEncrypt.TabIndex = 4;
            this.btnEncrypt.Text = "我是商户我要加密";
            this.btnEncrypt.UseVisualStyleBackColor = true;
            this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "写接口的人的RSA公钥 ：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 358);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "商户自己的RSA私钥：";
            // 
            // txtAPIPublicKey
            // 
            this.txtAPIPublicKey.Location = new System.Drawing.Point(151, 105);
            this.txtAPIPublicKey.Multiline = true;
            this.txtAPIPublicKey.Name = "txtAPIPublicKey";
            this.txtAPIPublicKey.Size = new System.Drawing.Size(388, 232);
            this.txtAPIPublicKey.TabIndex = 7;
            this.txtAPIPublicKey.Text = resources.GetString("txtAPIPublicKey.Text");
            // 
            // txtSalePrivateKey
            // 
            this.txtSalePrivateKey.Location = new System.Drawing.Point(151, 355);
            this.txtSalePrivateKey.Multiline = true;
            this.txtSalePrivateKey.Name = "txtSalePrivateKey";
            this.txtSalePrivateKey.Size = new System.Drawing.Size(388, 232);
            this.txtSalePrivateKey.TabIndex = 8;
            this.txtSalePrivateKey.Text = resources.GetString("txtSalePrivateKey.Text");
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(-91, 611);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "要加密的内容：";
            // 
            // txtContent
            // 
            this.txtContent.Location = new System.Drawing.Point(151, 608);
            this.txtContent.Multiline = true;
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new System.Drawing.Size(388, 95);
            this.txtContent.TabIndex = 10;
            this.txtContent.Text = "{\"OrderNO\":\"123456789\",\"TransMoney\":\"100\"}";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtDESKey);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtContent);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtSalePrivateKey);
            this.groupBox1.Controls.Add(this.txtDESValue);
            this.groupBox1.Controls.Add(this.txtAPIPublicKey);
            this.groupBox1.Controls.Add(this.btnEncrypt);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(19, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(549, 761);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "加密";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 611);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "要加密的内容：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtSign);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtEncryptContent);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtEncryptDESKey);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(578, 25);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(444, 609);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "加密后的内容";
            // 
            // txtSign
            // 
            this.txtSign.Location = new System.Drawing.Point(19, 373);
            this.txtSign.Multiline = true;
            this.txtSign.Name = "txtSign";
            this.txtSign.Size = new System.Drawing.Size(388, 214);
            this.txtSign.TabIndex = 14;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(17, 358);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(275, 12);
            this.label9.TabIndex = 13;
            this.label9.Text = "使用商户自己的RSA私钥加密后的报文密文(Sign)：";
            // 
            // txtEncryptContent
            // 
            this.txtEncryptContent.Location = new System.Drawing.Point(19, 105);
            this.txtEncryptContent.Multiline = true;
            this.txtEncryptContent.Name = "txtEncryptContent";
            this.txtEncryptContent.Size = new System.Drawing.Size(388, 232);
            this.txtEncryptContent.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(17, 76);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(233, 12);
            this.label8.TabIndex = 4;
            this.label8.Text = "使用DESKey和DESValue加密后的报文密文：";
            // 
            // txtEncryptDESKey
            // 
            this.txtEncryptDESKey.Location = new System.Drawing.Point(245, 27);
            this.txtEncryptDESKey.Multiline = true;
            this.txtEncryptDESKey.Name = "txtEncryptDESKey";
            this.txtEncryptDESKey.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtEncryptDESKey.Size = new System.Drawing.Size(224, 46);
            this.txtEncryptDESKey.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 33);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(227, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "使用写接口人的RSA公钥加密后的DESKey：";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtContent2);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.btnDencrypt);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.txtAPIPrivateKey);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.txtSalePublicKey);
            this.groupBox3.Location = new System.Drawing.Point(1028, 25);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(444, 761);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "解密";
            // 
            // txtAPIPrivateKey
            // 
            this.txtAPIPrivateKey.Location = new System.Drawing.Point(19, 371);
            this.txtAPIPrivateKey.Multiline = true;
            this.txtAPIPrivateKey.Name = "txtAPIPrivateKey";
            this.txtAPIPrivateKey.Size = new System.Drawing.Size(388, 232);
            this.txtAPIPrivateKey.TabIndex = 12;
            this.txtAPIPrivateKey.Text = resources.GetString("txtAPIPrivateKey.Text");
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(17, 358);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(131, 12);
            this.label11.TabIndex = 4;
            this.label11.Text = "写接口的人的RSA私钥：";
            // 
            // txtSalePublicKey
            // 
            this.txtSalePublicKey.Location = new System.Drawing.Point(19, 42);
            this.txtSalePublicKey.Multiline = true;
            this.txtSalePublicKey.Name = "txtSalePublicKey";
            this.txtSalePublicKey.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSalePublicKey.Size = new System.Drawing.Size(388, 295);
            this.txtSalePublicKey.TabIndex = 3;
            this.txtSalePublicKey.Text = resources.GetString("txtSalePublicKey.Text");
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(17, 27);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(287, 12);
            this.label13.TabIndex = 15;
            this.label13.Text = "商户自己的RSA公钥（商户把他们的公钥给我们了）：";
            // 
            // btnDencrypt
            // 
            this.btnDencrypt.Location = new System.Drawing.Point(138, 732);
            this.btnDencrypt.Name = "btnDencrypt";
            this.btnDencrypt.Size = new System.Drawing.Size(154, 23);
            this.btnDencrypt.TabIndex = 16;
            this.btnDencrypt.Text = "我是API的人我要解密";
            this.btnDencrypt.UseVisualStyleBackColor = true;
            this.btnDencrypt.Click += new System.EventHandler(this.btnDencrypt_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(-5, 126);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(161, 12);
            this.label10.TabIndex = 12;
            this.label10.Text = "(我们把我们的公钥给商户了)";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(17, 621);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(89, 12);
            this.label12.TabIndex = 14;
            this.label12.Text = "解密后的内容：";
            // 
            // txtContent2
            // 
            this.txtContent2.Location = new System.Drawing.Point(21, 636);
            this.txtContent2.Multiline = true;
            this.txtContent2.Name = "txtContent2";
            this.txtContent2.Size = new System.Drawing.Size(386, 95);
            this.txtContent2.TabIndex = 14;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1473, 798);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "DES和RSA混合加密";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDESKey;
        private System.Windows.Forms.TextBox txtDESValue;
        private System.Windows.Forms.Button btnEncrypt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAPIPublicKey;
        private System.Windows.Forms.TextBox txtSalePrivateKey;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtContent;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtEncryptDESKey;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtEncryptContent;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtSign;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtAPIPrivateKey;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtSalePublicKey;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnDencrypt;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtContent2;
        private System.Windows.Forms.Label label12;
    }
}

