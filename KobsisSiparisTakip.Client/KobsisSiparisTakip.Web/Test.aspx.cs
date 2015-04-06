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
                KapiSeriFormaYazdir();
            }
        }

        private void GenerateForm()
        {
            this.form1.Controls.Add(new ScriptManager());
            this.form1.Controls.Add(new FormGenerator().Generate(this.KapiSeri));
        }

        private void KapiSeriFormaYazdir()
        {
            Control c = this.form1.FindControl("SiparisFormKontrolleri");
            Control clabel = KontrolBul(c, "lblKapiSeri");
            if (clabel is Label)
                ((Label)clabel).Text = Request.QueryString["KapiSeri"].ToString();
        }

        Control c1;
        private Control KontrolBul(Control c, string kontrolID)
        {
            if (c.ID == kontrolID)
                c1 = c;
            else
            {
                for (int i = 0; i < c.Controls.Count; i++)
                {
                    Control wcChild = (Control)c.Controls[i];
                    KontrolBul(wcChild, kontrolID);
                }
            }
            return c1;
        }
    }
}