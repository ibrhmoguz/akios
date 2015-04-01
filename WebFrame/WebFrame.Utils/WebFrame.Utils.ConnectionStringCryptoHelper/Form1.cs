using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebFrame.DataType.Common.Cryptography;

namespace WebFrame.Utils.ConnectionStringCryptoHelper
{
    public partial class frmCrypto : Form
    {
        public frmCrypto()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
           tbChiperText.Text = string.Empty;
           tbChiperText.Text=  Crytography.Encrypt(tbClearText.Text.Trim());
           tbClearText.Text = string.Empty;
        }

        private void btnDencrypt_Click(object sender, EventArgs e)
        {
            tbClearText.Text = string.Empty;
            tbClearText.Text = Crytography.Decrypt(tbChiperText.Text.Trim());
            tbChiperText.Text = string.Empty;
        }
    }
}
