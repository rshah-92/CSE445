using System;
using System.Web.UI;

namespace WebApplication1.Controls
{
    public partial class CaptchaControl : UserControl
    {
        // Generate a new math question and store answer in session
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GenerateQuestion();
            }
        }

        private void GenerateQuestion()
        {
            Random rnd = new Random();
            int a = rnd.Next(1, 10);
            int b = rnd.Next(1, 10);

            // Store the correct answer in session
            Session["CaptchaAnswer"] = a + b;

            // Show the question to the user
            lblCaptchaQuestion.Text = $"What is {a} + {b}?";
            lblCaptchaResult.Text = "";
            txtCaptchaAnswer.Text = "";
        }

        // Called from the parent page to validate the captcha
        public bool IsValid()
        {
            if (Session["CaptchaAnswer"] == null) return false;

            int correct = (int)Session["CaptchaAnswer"];
            int userAnswer;

            if (int.TryParse(txtCaptchaAnswer.Text.Trim(), out userAnswer))
            {
                return userAnswer == correct;
            }
            return false;
        }

        // Refresh button generates a new question
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            GenerateQuestion();
        }

        // Show feedback after a validation attempt
        public void ShowResult(bool passed)
        {
            if (passed)
            {
                lblCaptchaResult.Text = "Captcha passed!";
                lblCaptchaResult.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblCaptchaResult.Text = "Wrong answer, try again.";
                lblCaptchaResult.ForeColor = System.Drawing.Color.Red;
                GenerateQuestion();
            }
        }
    }
}


