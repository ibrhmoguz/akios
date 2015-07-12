using System;
using System.Web.UI;
using Kobsis.Business;

namespace Akios.Web
{
    public partial class ImageForm : Page
    {
        public string ImageId
        {
            get
            {
                return !String.IsNullOrWhiteSpace(Request.QueryString["ImageID"]) ? Request.QueryString["ImageID"] : String.Empty;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ImajYukle();
            }
        }

        private void ImajYukle()
        {
            var imaj = new ImajBS().ImajGetirImajIdIle(this.ImageId);
            if (imaj.ImajData == null) 
                return;

            Response.ContentType = "image/jpg";
            Response.BinaryWrite(imaj.ImajData);
        }
    }
}