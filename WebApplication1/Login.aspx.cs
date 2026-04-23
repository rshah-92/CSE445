using System;
using System.Web;
using System.Web.Security;
using System.Xml.Linq;
using System.Linq;
using System.IO;

namespace WebApplication1
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // If already logged in, skip the login page
            if (User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Default.aspx");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (AuthenticateUser(username, password))
            {
                // Set the Forms auth cookie and redirect
                FormsAuthentication.SetAuthCookie(username, false);
                string returnUrl = Request.QueryString["ReturnUrl"];
                Response.Redirect(returnUrl ?? "~/Default.aspx");
            }
            else
            {
                lblError.Text = "Invalid username or password.";
            }
        }

        private bool AuthenticateUser(string username, string password)
        {
            // Check Staff.xml first
            string staffPath = Server.MapPath("~/App_Data/Staff.xml");
            if (File.Exists(staffPath))
            {
                XDocument staffDoc = XDocument.Load(staffPath);
                var staffMatch = staffDoc.Descendants("Staff")
                    .FirstOrDefault(s =>
                        s.Element("Username")?.Value == username &&
                        s.Element("Password")?.Value == password);
                if (staffMatch != null) return true;
            }

            // Check Member.xml 
            // TODO: hash the input password with DLL before comparing
            string memberPath = Server.MapPath("~/App_Data/Member.xml");
            if (File.Exists(memberPath))
            {
                XDocument memberDoc = XDocument.Load(memberPath);
                var memberMatch = memberDoc.Descendants("Member")
                    .FirstOrDefault(m =>
                        m.Element("Username")?.Value == username &&
                        m.Element("Password")?.Value == password);
                if (memberMatch != null) return true;
            }

            return false;
        }
    }
}


