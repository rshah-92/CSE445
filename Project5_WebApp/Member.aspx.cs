using System;
using System.Web;

namespace Project5_WebApp
{
    public partial class Member : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie c = Request.Cookies["UserInfo"];
            lblMsg.Text = c != null ? "Hello " + c["Username"] : "No cookie found.";
        }
    }
}