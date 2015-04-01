namespace WebFrame.Utils.ConnectionStringCryptoHelper
{
    partial class frmCrypto
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
            this.tbClearText = new System.Windows.Forms.TextBox();
            this.tbChiperText = new System.Windows.Forms.TextBox();
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.btnDencrypt = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbClearText
            // 
            this.tbClearText.Location = new System.Drawing.Point(22, 49);
            this.tbClearText.Multiline = true;
            this.tbClearText.Name = "tbClearText";
            this.tbClearText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbClearText.Size = new System.Drawing.Size(284, 73);
            this.tbClearText.TabIndex = 0;
            // 
            // tbChiperText
            // 
            this.tbChiperText.Location = new System.Drawing.Point(22, 162);
            this.tbChiperText.Multiline = true;
            this.tbChiperText.Name = "tbChiperText";
            this.tbChiperText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbChiperText.Size = new System.Drawing.Size(284, 73);
            this.tbChiperText.TabIndex = 1;
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.Location = new System.Drawing.Point(312, 49);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(78, 73);
            this.btnEncrypt.TabIndex = 2;
            this.btnEncrypt.Text = "Şifrele";
            this.btnEncrypt.UseVisualStyleBackColor = true;
            this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
            // 
            // btnDencrypt
            // 
            this.btnDencrypt.Location = new System.Drawing.Point(313, 162);
            this.btnDencrypt.Name = "btnDencrypt";
            this.btnDencrypt.Size = new System.Drawing.Size(78, 73);
            this.btnDencrypt.TabIndex = 3;
            this.btnDencrypt.Text = "Şifreyi Çöz";
            this.btnDencrypt.UseVisualStyleBackColor = true;
            this.btnDencrypt.Click += new System.EventHandler(this.btnDencrypt_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(17, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(191, 26);
            this.label1.TabIndex = 4;
            this.label1.Text = "Şifrelenecek Metin";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(17, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(180, 26);
            this.label2.TabIndex = 5;
            this.label2.Text = "Şifrelenmiş Metin";
            // 
            // frmCrypto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 274);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDencrypt);
            this.Controls.Add(this.btnEncrypt);
            this.Controls.Add(this.tbChiperText);
            this.Controls.Add(this.tbClearText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "frmCrypto";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "CryptoForm";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion







        private System.Windows.Forms.TextBox tbClearText;
        private System.Windows.Forms.TextBox tbChiperText;
        private System.Windows.Forms.Button btnEncrypt;
        private System.Windows.Forms.Button btnDencrypt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

