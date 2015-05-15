using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KobsisSiparisTakip.Web.Helper
{
    public class LiteralWebControl : WebControl
    {
        public string Content { get; set; }

        public LiteralWebControl(string content)
            : base()
        {
            this.Content = content;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.Write(this.Content);
        }
    }
}