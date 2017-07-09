using System;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;
using Akios.Business;
using Akios.DataType;
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

        protected void btnKaydet_OnClick(object sender, EventArgs e)
        {
            if (logoFileUpload.HasFile)
            {
                var fileExtension = Path.GetExtension(logoFileUpload.FileName);
                var fileExtension = 
                switch (fileExtension)
                {
                    case ".gif":

                }

                  Select Case extension
            Case 
                MIMEType = "image/gif"
            Case ".jpg", ".jpeg", ".jpe"
                MIMEType = "image/jpeg"
            Case ".png"
                MIMEType = "image/png"
            Case Else
                'Invalid file type uploaded
                Label1.Text = "Not a Valid file format"
            }

            var musteri = new Musteri()
            {
                Adi = !string.IsNullOrEmpty(txtAd.Text) ? txtAd.Text : null,
                Faks = !string.IsNullOrEmpty(txtFaks.Text) ? txtFaks.Text : null,
                Adres = !string.IsNullOrEmpty(txtAdres.Text) ? txtAdres.Text : null,
                Kod = !string.IsNullOrEmpty(txtKod.Text) ? txtKod.Text : null,
                Mobil = !string.IsNullOrEmpty(txtCep.Text) ? txtCep.Text : null,
                Mail = !string.IsNullOrEmpty(txtMail.Text) ? txtMail.Text : null,
                Tel = !string.IsNullOrEmpty(txtTel.Text) ? txtTel.Text : null,
                Web = !string.IsNullOrEmpty(txtWeb.Text) ? txtWeb.Text : null,
                YetkiliKisi = !string.IsNullOrEmpty(txtYetkiliKisi.Text) ? txtYetkiliKisi.Text : null
            };




            MusterileriGetir();
        }
    }
}