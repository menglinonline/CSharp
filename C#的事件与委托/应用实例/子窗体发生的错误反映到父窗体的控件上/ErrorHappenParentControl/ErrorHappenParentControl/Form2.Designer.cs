namespace WindowsFormsApplication2
{
    partial class Form2
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
            this.btnConvert2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnConvert2
            // 
            this.btnConvert2.Location = new System.Drawing.Point(43, 59);
            this.btnConvert2.Name = "btnConvert2";
            this.btnConvert2.Size = new System.Drawing.Size(156, 23);
            this.btnConvert2.TabIndex = 0;
            this.btnConvert2.Text = "把String转化为Int";
            this.btnConvert2.UseVisualStyleBackColor = true;
            this.btnConvert2.Click += new System.EventHandler(this.btnConvert2_Click);
            // 
            // Form2
            // 
            this.ClientSize = new System.Drawing.Size(247, 186);
            this.Controls.Add(this.btnConvert2);
            this.Name = "Form2";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.Button btnConvert2;
    }
}