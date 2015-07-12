using System;
using System.Data;
using System.Web.UI.WebControls;
using Kobsis.Util;

namespace Akios.Web.Print
{
    public partial class SeriyeGoreSatilanAdet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                MusteriLogoAyarla();
                RaporOlustur();
            }
        }

        private void MusteriLogoAyarla()
        {
            if (SessionManager.MusteriBilgi.LogoID.HasValue)
                imgFirmaLogo.ImageUrl = "ImageForm.aspx?ImageID=" + SessionManager.MusteriBilgi.LogoID.Value;
            else
                imgFirmaLogo.ImageUrl = "/App_Themes/Theme/Raster/BlankProfile.gif";
        }

        private void RaporOlustur()
        {
            GridDoldur(grdSatisAdetRapor, SessionManager.SatisAdetListesi);
            GridDoldur(grdSatisTutarRapor, SessionManager.SatisTutarListesi);
        }

        private void GridDoldur(GridView gv, DataTable dtSatisAdet)
        {
            if (dtSatisAdet.Rows.Count > 0)
            {
                gv.DataSource = dtSatisAdet;
                gv.DataBind();
            }
            else
            {
                gv.DataSource = null;
                gv.DataBind();
            }
        }
    }
}