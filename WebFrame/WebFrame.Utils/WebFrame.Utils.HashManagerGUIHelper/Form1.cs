using System;
using System.Windows.Forms;
using WebFrame.DataType.Common.Cryptography;

namespace WebFrame.Utils.HashManagerGUIHelper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnMakeHash_Click(object sender, EventArgs e)
        {
            rbHashText.Clear();

            rbHashText.Text = HashManager.GetMd5Hash(rbClearText.Text.Trim());
            rbClearText.Clear();
        }
    }
}
