using Project5_LocalComponents;
using Project5_Service;
using System;
using System.Web;

namespace Project5_WebApp
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HttpCookie c = Request.Cookies["UserInfo"];
                if (c != null)
                {
                    txtUser.Text = c["Username"];
                    txtCampus.Text = c["Campus"];
                    lblCookie.Text = "Welcome back, " + c["Username"];
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            HttpCookie c = new HttpCookie("UserInfo");
            c["Username"] = txtUser.Text;
            c["Campus"] = txtCampus.Text;
            c.Expires = DateTime.Now.AddDays(7);
            Response.Cookies.Add(c);

            lblCookie.Text = "Cookie saved!";
        }

        protected void btnHash_Click(object sender, EventArgs e)
        {
            lblHash.Text = SecurityUtils.ComputeSha256(txtPwd.Text);
        }

        protected void btnWeather_Click(object sender, EventArgs e)
        {
            var svc = new WeatherService();
            lblWeather.Text = svc.GetWeather(txtCity.Text, txtDate.Text);
        }
    }
}