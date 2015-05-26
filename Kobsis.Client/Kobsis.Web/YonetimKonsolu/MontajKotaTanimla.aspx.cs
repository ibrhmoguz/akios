using System;
using System.Data;
using System.Web.UI.WebControls;
using Kobsis.Business;
using Kobsis.Util;
using Kobsis.Web.Helper;

namespace Kobsis.Web.YonetimKonsolu
{
    public partial class MontajKotaTanimla : KobsisBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                MontajKotalariListele();
            }
        }

        private void MontajKotalariListele()
        {
            DataTable dt = new MontajBS().MontajKotaListele(SessionManager.MusteriBilgi.MusteriID.Value);
            if (dt != null && dt.Rows.Count > 0)
            {
                grdMontajKota.DataSource = dt;
                grdMontajKota.DataBind();
            }
            else
            {
                grdMontajKota.DataSource = null;
                grdMontajKota.DataBind();
            }
        }

        protected void btnKaydet_Click(object sender, EventArgs e)
        {
            if (rdtMontajTarih.SelectedDate == null)
            {
                MessageBox.Uyari(this.Page, "Montaj tarihi seçmelisiniz!");
                return;
            }

            int kota;
            if (string.IsNullOrEmpty(txtMontajKota.Text) || !Int32.TryParse(txtMontajKota.Text, out kota))
            {
                MessageBox.Uyari(this.Page, "Montaj kotası girmelisiniz!");
                return;
            }

            bool islemDurumu = new MontajBS().MontajKotaKaydet(rdtMontajTarih.SelectedDate.Value, kota, !chcBoxMontajKabul.Checked, SessionManager.MusteriBilgi.MusteriID.Value);
            MontajKotalariListele();
            if (islemDurumu)
            {
                MessageBox.Basari(this.Page, "Montaj kota bilgisi eklendi.");
                txtMontajKota.Text = string.Empty;
                rdtMontajTarih.SelectedDate = null;
                chcBoxMontajKabul.Checked = false;
            }
            else
                MessageBox.Hata(this.Page, "Montaj kota bilgisi eklenmedi.");
        }

        protected void grdMontajKota_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            bool islemDurum = new MontajBS().MontajKotaSil(e.Keys[0].ToString());
            MontajKotalariListele();
            if (islemDurum)
            {
                MessageBox.Basari(this.Page, "Montaj kota bilgisi silindi.");
            }
            else
                MessageBox.Hata(this.Page, "Montaj kota bilgisi silinmedi.");
        }
    }
}