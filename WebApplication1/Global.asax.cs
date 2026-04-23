using System;
using System.Web;

namespace WebApplication1
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Store app start time so we can display it on Default.aspx as proof this fired
            Application["AppStartTime"] = DateTime.Now.ToString();

            // Store event categories available in the Campus Event Board
            Application["EventCategories"] = new string[]
            {
                "Academic", "Social", "Sports", "Career", "Arts", "Community"
            };

            // Track total number of visits across all sessions
            Application["TotalVisits"] = 0;
        }

        void Session_Start(object sender, EventArgs e)
        {
            // Increment visit counter each time a new session starts
            Application.Lock();
            Application["TotalVisits"] = (int)Application["TotalVisits"] + 1;
            Application.UnLock();

            // Initialize the user's browsed events list for this session
            Session["BrowsedEvents"] = new System.Collections.Generic.List<string>();
        }

        void Session_End(object sender, EventArgs e)
        {
            // Session cleanup - fires when session times out
        }

        void Application_End(object sender, EventArgs e)
        {
            // App shutdown cleanup
        }

        void Application_Error(object sender, EventArgs e)
        {
            // Log any unhandled errors
            Exception ex = Server.GetLastError();
            Application["LastError"] = ex?.Message ?? "Unknown error";
        }
    }
}

