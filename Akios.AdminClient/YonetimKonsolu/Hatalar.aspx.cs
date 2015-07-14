using System;
using System.Data;
using Akios.AdminWebClient.Helper;
using Akios.Business;
using Akios.DataType;
using Akios.Util;

namespace Akios.AdminWebClient.YonetimKonsolu
{
    public partial class Hatalar : AkiosBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionManager.KullaniciBilgi.Rol == KullaniciRol.Kullanici)
            {
                MessageBox.Hata(this, "Bu sayfaya erişim yetkiniz yoktur!");
                return;
            }

            if (!Page.IsPostBack)
            {
                HatalariYukle();
            }
        }

        private void HatalariYukle()
        {
            DataTable dt = new YonetimKonsoluBS().HatalariGetir();

            if (dt.Rows.Count > 0)
            {
                grdHatalar.DataSource = dt;
                lblhataSayisi.Text = dt.Rows.Count.ToString();
            }
            else
                grdHatalar.DataSource = null;

            grdHatalar.DataBind();
        }
    }
}