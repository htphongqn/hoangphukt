using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;

namespace Appketoan.UIs
{
    public partial class login : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["Name"] == null)
            {
                HttpContext.Current.Response.Redirect("~/logout.aspx");
            }
            else
            {
                lnkUserLogin.Text = Utils.CStrDef(HttpContext.Current.Session["Name"], "");
            }
        }
    }
}