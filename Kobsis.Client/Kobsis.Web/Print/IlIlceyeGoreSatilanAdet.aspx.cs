using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;
using Kobsis.Util;

namespace Kobsis.Web.Print
{
    public partial class IlIlceyeGoreSatilanAdet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                MusteriLogoAyarla();
                RaporOlustur();
            }
        }

        private void RaporOlustur()
        {
            DataSet ds = SessionManager.IlIlceyeGoreSatilanAdet;

            DataTable dt1 = YuzdeDegerleriHesapla(ds.Tables[0]);
            DataTable dt2 = YuzdeDegerleriHesapla(ds.Tables[1]);
            DataTable dt3 = YuzdeDegerleriHesapla(ds.Tables[2]);

            GridDoldur(grdRaporIl, dt1);
            GridDoldur(grdRaporIlce, dt2);
            GridDoldur(grdRaporSemt, dt3);
        }

        private void GridDoldur(GridView gv, DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                gv.DataSource = dt;
                gv.DataBind();
            }
            else
            {
                gv.DataSource = null;
                gv.DataBind();
            }
        }

        private void MusteriLogoAyarla()
        {
            if (SessionManager.MusteriBilgi.LogoID.HasValue)
                imgFirmaLogo.ImageUrl = "ImageForm.aspx?ImageID=" + SessionManager.MusteriBilgi.LogoID.Value;
            else
                imgFirmaLogo.ImageUrl = "/App_Themes/Theme/Raster/BlankProfile.gif";
        }

        private DataTable YuzdeDegerleriHesapla(DataTable dt)
        {
            decimal toplamAdet = Convert.ToDecimal(dt.AsEnumerable().Where(q => !q.Field<string>("Yillik").Equals("")).Sum(a => Convert.ToInt32(a.Field<string>("Yillik"))).ToString());
            decimal yuzde;

            if (toplamAdet != 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row["Yillik"] != DBNull.Value && row["Yillik"].ToString() != "0")
                    {
                        yuzde = Convert.ToDecimal((Convert.ToDecimal(row["Yillik"].ToString()) / toplamAdet));
                        row["Yuzde(%)"] = (yuzde * 100).ToString("0.00", CultureInfo.InvariantCulture);
                    }
                }
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (dt.Rows[i][j] != DBNull.Value && dt.Rows[i][j].ToString() == "0")
                        dt.Rows[i][j] = string.Empty;
                }
            }

            return dt;
        }
    }
}