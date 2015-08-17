using System;
using System.Collections.Specialized;
using System.Data;
using System.Web.UI.WebControls;
using Akios.Business;
using Akios.Util;
using Telerik.Web.UI;
using DataKey = System.Web.UI.WebControls.DataKey;

namespace Akios.AdminWebClient.YonetimKonsolu
{
    public partial class MusteriTanimlama : AkiosBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                MusterileriGetir();
            }
        }

        private void MusterileriGetir()
        {
            DataTable dt;
            if (SessionManager.MusteriListesi == null)
            {
                dt = new MusteriBS().MusteriListesiGetir();
                SessionManager.MusteriListesi = dt;
            }
            else
            {
                dt = SessionManager.MusteriListesi;
            }

            if (dt.Rows.Count > 0)
            {
                grdMusteriler.DataSource = dt;
                grdMusteriler.DataBind();
            }
            else
            {
                grdMusteriler.DataSource = null;
                grdMusteriler.DataBind();
            }
        }

        protected void grdMusteriler_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var img = e.Row.Cells[0].Controls[1] as Image;
                if (img != null)
                {
                    var dataKey = grdMusteriler.DataKeys[e.Row.RowIndex];
                    if (dataKey != null)
                    {
                        var orderedDictionary = dataKey.Values;
                        if (orderedDictionary != null && !string.IsNullOrEmpty(orderedDictionary["LogoID"].ToString()))
                        {
                            img.ImageUrl = "ImageForm.aspx?ImageID=" + orderedDictionary["LogoID"];
                        }
                        else
                        {
                            img.ImageUrl = "/App_Themes/Theme/Raster/BlankProfile.gif";
                        }
                    }
                }
            }
        }
    }
}