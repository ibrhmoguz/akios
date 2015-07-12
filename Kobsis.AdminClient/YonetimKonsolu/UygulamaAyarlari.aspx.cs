using System;
using System.Data;
using Kobsis.Business;
using Kobsis.Util;
using Kobsis.Web.Helper;

namespace Kobsis.Web.YonetimKonsolu
{
    public partial class UygulamaAyarlari : KobsisBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FormVerileriniGetir();
            }
        }

        private void FormVerileriniGetir()
        {
            DataTable dt = new ConfigBS().ConfigBilgileriniGetir();
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                if (row != null)
                {
                    if (row["ConfigName"] != DBNull.Value && row["ConfigName"].ToString() == "TESLIMAT_KOTA_KONTROLU")
                        if (row["ConfigValue"].ToString() == "1")
                            chcBoxTeslimatKotaKontrolu.Checked = true;
                        else
                            chcBoxTeslimatKotaKontrolu.Checked = false;

                }
                DataRow row1 = dt.Rows[1];
                if (row1 != null)
                {
                    if (row1["ConfigName"] != DBNull.Value && row1["ConfigName"].ToString() == "TESLIMAT_KOTA_VARSAYILAN")
                        txtTeslimatKotaVarsayilan.Text = row1["ConfigValue"].ToString();
                }
            }
        }

        protected void btnKaydet_Click(object sender, EventArgs e)
        {
            string varsayilan;
            if (chcBoxTeslimatKotaKontrolu.Checked)
            {
                if (string.IsNullOrEmpty(txtTeslimatKotaVarsayilan.Text))
                {
                    MessageBox.Uyari(this.Page, "Teslimat kota varsayılan değeri giriniz.");
                    return;
                }
                varsayilan = txtTeslimatKotaVarsayilan.Text;

                SessionManager.TeslimatKotaKontrolu = "1";
                SessionManager.TeslimatKotaVarsayilan = Convert.ToInt32(varsayilan);
            }
            else
            {
                varsayilan = "0";
                SessionManager.TeslimatKotaKontrolu = "0";
                SessionManager.TeslimatKotaVarsayilan = 0;
            }

            bool islemDurumu = new ConfigBS().ConfigDegerleriniKaydet(chcBoxTeslimatKotaKontrolu.Checked, varsayilan);

            if (islemDurumu)
            {
                MessageBox.Basari(this.Page, "Değerler kayıt edildi.");
                FormVerileriniGetir();
            }
            else
                MessageBox.Hata(this.Page, "Hata Oluştu");
        }
    }
}