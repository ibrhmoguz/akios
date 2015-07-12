using System;
using System.Data;
using System.Web.UI.WebControls;
using Akios.Web.Helper;
using Kobsis.Business;
using Kobsis.Util;

namespace Akios.Web.YonetimKonsolu
{
    public partial class TeslimatKotaTanimla : KobsisBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                TeslimatKotalariListele();
            }
        }

        private void TeslimatKotalariListele()
        {
            DataTable dt = new TeslimatBS().TeslimatKotaListele(SessionManager.MusteriBilgi.MusteriID.Value);
            if (dt != null && dt.Rows.Count > 0)
            {
                grdTeslimatKota.DataSource = dt;
                grdTeslimatKota.DataBind();
            }
            else
            {
                grdTeslimatKota.DataSource = null;
                grdTeslimatKota.DataBind();
            }
        }

        protected void btnKaydet_Click(object sender, EventArgs e)
        {
            if (rdtTeslimatTarih.SelectedDate == null)
            {
                MessageBox.Uyari(this.Page, "Teslimat tarihi seçmelisiniz!");
                return;
            }

            int kota;
            if (string.IsNullOrEmpty(txtTeslimatKota.Text) || !Int32.TryParse(txtTeslimatKota.Text, out kota))
            {
                MessageBox.Uyari(this.Page, "Teslimat kotası girmelisiniz!");
                return;
            }

            bool islemDurumu = new TeslimatBS().TeslimatKotaKaydet(rdtTeslimatTarih.SelectedDate.Value, kota, !chcBoxTeslimatKabul.Checked, SessionManager.MusteriBilgi.MusteriID.Value);
            TeslimatKotalariListele();
            if (islemDurumu)
            {
                MessageBox.Basari(this.Page, "Teslimat kota bilgisi eklendi.");
                txtTeslimatKota.Text = string.Empty;
                rdtTeslimatTarih.SelectedDate = null;
                chcBoxTeslimatKabul.Checked = false;
            }
            else
                MessageBox.Hata(this.Page, "Teslimat kota bilgisi eklenmedi.");
        }

        protected void grdTeslimatKota_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            bool islemDurum = new TeslimatBS().TeslimatKotaSil(e.Keys[0].ToString());
            TeslimatKotalariListele();
            if (islemDurum)
            {
                MessageBox.Basari(this.Page, "Teslimat kota bilgisi silindi.");
            }
            else
                MessageBox.Hata(this.Page, "Teslimat kota bilgisi silinmedi.");
        }
    }
}