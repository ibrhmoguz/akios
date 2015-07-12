using System.Web.UI;
using System.Web.UI.WebControls;

namespace Akios.Util
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