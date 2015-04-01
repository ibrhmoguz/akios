namespace WebFrame.Utils.HashManagerGUIHelper
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.rbClearText = new System.Windows.Forms.RichTextBox();
            this.rbHashText = new System.Windows.Forms.RichTextBox();
            this.btnMakeHash = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(254, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Hash Uygulanacak Metin";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(12, 169);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(243, 26);
            this.label2.TabIndex = 1;
            this.label2.Text = "Hash Uygulanmış Metin";
            // 
            // rbClearText
            // 
            this.rbClearText.Location = new System.Drawing.Point(17, 48);
            this.rbClearText.Name = "rbClearText";
            this.rbClearText.Size = new System.Drawing.Size(238, 93);
            this.rbClearText.TabIndex = 2;
            this.rbClearText.Text = "";
            // 
            // rbHashText
            // 
            this.rbHashText.Location = new System.Drawing.Point(17, 212);
            this.rbHashText.Name = "rbHashText";
            this.rbHashText.ReadOnly = true;
            this.rbHashText.Size = new System.Drawing.Size(238, 93);
            this.rbHashText.TabIndex = 3;
            this.rbHashText.Text = "";
            // 
            // btnMakeHash
            // 
            this.btnMakeHash.Location = new System.Drawing.Point(261, 48);
            this.btnMakeHash.Name = "btnMakeHash";
            this.btnMakeHash.Size = new System.Drawing.Size(76, 93);
            this.btnMakeHash.TabIndex = 4;
            this.btnMakeHash.Text = "Hash Uygula";
            this.btnMakeHash.UseVisualStyleBackColor = true;
            this.btnMakeHash.Click += new System.EventHandler(this.btnMakeHash_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 339);
            this.Controls.Add(this.btnMakeHash);
            this.Controls.Add(this.rbHashText);
            this.Controls.Add(this.rbClearText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "HashManagerGUI";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox rbClearText;
        private System.Windows.Forms.RichTextBox rbHashText;
        private System.Windows.Forms.Button btnMakeHash;
    }
}

