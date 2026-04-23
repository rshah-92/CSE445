using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web.UI;
using WebApplication1.Controls;

namespace WebApplication1
{
    public partial class Default : Page
    {
        // Live WebStrar URL for the RsvpService WCF endpoint
        private const string ServiceUrl =
            "https://webstrarportal.fulton.asu.edu/sites/website33/Page0/RsvpService.svc";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Populate stats banner from values set by Global.asax Application_Start
                lblAppStart.Text = Application["AppStartTime"]?.ToString() ?? "N/A";
                lblVisits.Text = Application["TotalVisits"]?.ToString() ?? "0";
            }
        }

        // Navigate to Member page — Forms auth redirects to Login if not authenticated
        protected void btnMember_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Member.aspx");
        }

        // Navigate to Staff page — Forms auth redirects to Login if not authenticated
        protected void btnStaff_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Staff.aspx");
        }

        // TryIt: Display values loaded by Global.asax into Application state
        protected void btnTestGlobal_Click(object sender, EventArgs e)
        {
            string startTime = Application["AppStartTime"]?.ToString() ?? "Not set";

            int visits = Application["TotalVisits"] != null
                ? (int)Application["TotalVisits"] : 0;

            string[] categories = Application["EventCategories"] as string[];
            string catList = categories != null
                ? string.Join(", ", categories) : "None loaded";

            lblGlobalResult.Text =
                $"App started: {startTime} | " +
                $"Total visits: {visits} | " +
                $"Event categories: {catList}";
        }

        // TryIt: Delegate validation to the CaptchaControl user control
        protected void btnTestCaptcha_Click(object sender, EventArgs e)
        {
            bool passed = MyCaptcha.IsValid();
            MyCaptcha.ShowResult(passed);
        }

        // TryIt: Call the live RsvpService on WebStrar using a raw SOAP 1.1 request.
        // HttpWebRequest is used directly so no generated service reference is needed.
        protected void btnTestRsvp_Click(object sender, EventArgs e)
        {
            string eventId = txtEventId.Text.Trim();
            if (string.IsNullOrEmpty(eventId))
            {
                lblRsvpResult.Text = "Please enter an Event ID.";
                return;
            }

            try
            {
                // Build a minimal SOAP 1.1 envelope for GetAttendeeCount
                string soapBody =
                    "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                    "<soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" " +
                                   "xmlns:tns=\"http://tempuri.org/\">" +
                      "<soap:Body>" +
                        "<tns:GetAttendeeCount>" +
                          "<tns:eventId>" + eventId + "</tns:eventId>" +
                        "</tns:GetAttendeeCount>" +
                      "</soap:Body>" +
                    "</soap:Envelope>";

                byte[] bytes = Encoding.UTF8.GetBytes(soapBody);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ServiceUrl);
                request.Method = "POST";
                request.ContentType = "text/xml; charset=utf-8";
                request.Headers.Add("SOAPAction",
                    "\"http://tempuri.org/IRsvpService/GetAttendeeCount\"");
                request.ContentLength = bytes.Length;

                // Write SOAP envelope to the request stream
                using (Stream stream = request.GetRequestStream())
                    stream.Write(bytes, 0, bytes.Length);

                // Read the XML response and extract the integer result
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string xml = reader.ReadToEnd();

                    string startTag = "<GetAttendeeCountResult>";
                    string endTag = "</GetAttendeeCountResult>";
                    int si = xml.IndexOf(startTag);
                    int ei = xml.IndexOf(endTag);

                    if (si >= 0 && ei > si)
                    {
                        string count = xml.Substring(
                            si + startTag.Length, ei - si - startTag.Length);
                        lblRsvpResult.Text =
                            $"Event {eventId} has {count} attendee(s).";
                    }
                    else
                    {
                        lblRsvpResult.Text =
                            "Response received but count could not be parsed.";
                    }
                }
            }
            catch (Exception ex)
            {
                lblRsvpResult.Text = $"Service error: {ex.Message}";
            }
        }
    }
}


