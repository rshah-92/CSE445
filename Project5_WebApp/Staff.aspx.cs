using System;
using System.Web;

namespace Project5_WebApp
{
    public partial class Staff : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie c = Request.Cookies["UserInfo"];
            lblMsg.Text = c != null ? "Staff view for " + c["Username"] : "No cookie found.";
        }
    }
}