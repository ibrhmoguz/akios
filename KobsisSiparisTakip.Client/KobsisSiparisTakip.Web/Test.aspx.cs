using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KobsisSiparisTakip.Web.Util;

namespace KobsisSiparisTakip.Web
{
    public partial class Test : System.Web.UI.Page
    {
        public string KapiSeri
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(Request.QueryString["KapiSeri"]))
                {
                    return Request.QueryString["KapiSeri"].ToString();
                }
                else
                    return String.Empty;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SessionManager.MusteriId = "1";
                GenerateForm();
            }
        }

        private void GenerateForm()
        {
            this.form1.Controls.Add(new FormGenerator().Generate(this.KapiSeri));
        }
    }
}