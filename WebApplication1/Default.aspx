<%@ Page Language="C#" AutoEventWireup="true"
   CodeBehind="Default.aspx.cs"
   Inherits="WebApplication1.Default" %>
<%@ Register TagPrefix="uc" TagName="Captcha"
   Src="~/Controls/CaptchaControl.ascx" %>


<!DOCTYPE html>
<html>
<head runat="server">
   <title>Campus Event Board</title>
   <style>
       body { font-family: Arial, sans-serif; max-width: 900px;
              margin: 0 auto; padding: 20px; }
       h1 { color: #8C1D40; }
       h2 { color: #333; border-bottom: 2px solid #8C1D40;
            padding-bottom: 5px; margin-top: 30px; }
       .nav-buttons { margin: 20px 0; }
       table { width: 100%; border-collapse: collapse; margin-top: 10px; }
       th { background: #8C1D40; color: white; padding: 8px;
            text-align: left; }
       td { padding: 8px; border: 1px solid #ddd;
            vertical-align: top; font-size: 13px; }
       tr:nth-child(even) { background: #f9f9f9; }
       .tryit-box { background: #f0f0f0; padding: 15px;
                    margin: 10px 0; border-radius: 5px; }
       .info-box { background: #fff8e1; padding: 15px;
                   border-left: 4px solid #FFC627; margin: 15px 0; }
       .stat { display: inline-block; background: #8C1D40;
               color: white; padding: 6px 14px; border-radius: 4px;
               margin-right: 10px; font-size: 13px; }
   </style>
</head>
<body>
   <form id="form1" runat="server">


       <h1>Campus Event Board</h1>
       <p>A web application for ASU students to post, browse, and RSVP
          to campus events. Members can submit events for approval and
          track their attendance history. Staff can approve or reject
          submitted events and manage categories.</p>


       <!-- App state stats populated by Global.asax Application_Start -->
       <div style="margin:10px 0;">
           <span class="stat">App started:
               <asp:Label ID="lblAppStart" runat="server" /></span>
           <span class="stat">Total visits:
               <asp:Label ID="lblVisits" runat="server" /></span>
       </div>


       <!-- Navigation -->
       <h2>Access Pages</h2>
       <div class="nav-buttons">
           <asp:Button ID="btnMember" runat="server" Text="Go to Member Page"
               OnClick="btnMember_Click"
               style="padding:8px 16px; margin-right:10px;
                      background:#8C1D40; color:white; border:none;
                      cursor:pointer; font-size:14px;" />
           <asp:Button ID="btnStaff" runat="server" Text="Go to Staff Page"
               OnClick="btnStaff_Click"
               style="padding:8px 16px; background:#333; color:white;
                      border:none; cursor:pointer; font-size:14px;" />
       </div>


       <!-- TA Test Instructions -->
       <h2>How to Test This Application</h2>
       <div class="info-box">
           <strong>For the TA/Grader:</strong>
           <ul style="margin-top:8px;">
               <li>Member login Username: <strong>testuser</strong>
                   Password: <strong>Test123!</strong></li>
               <li>Staff login Username: <strong>TA</strong>
                   Password: <strong>Cse445!</strong></li>
               <li>Use the TryIt sections below to test each component
                   individually without logging in</li>
               <li>The captcha generates a random math question enter
                   the correct sum and click Validate Captcha</li>
               <li>For the RSVP service TryIt, enter any Event ID (e.g. EVT001)
                   and click Get Attendee Count</li>
           </ul>
       </div>


       <!-- TryIt: Global.asax -->
       <h2>TryIt Global.asax Event Handler</h2>
       <div class="tryit-box">
           <p style="margin:0 0 8px;">
               <strong>Component:</strong> Global.asax Application_Start and
               Session_Start event handlers.<br />
               Application_Start loads event categories into Application state
               and records the start time. Session_Start increments the visit
               counter and initialises the user's browsed-events list.<br />
               <strong>Input:</strong> None &nbsp;|&nbsp;
               <strong>Output:</strong> Application state values displayed below.
           </p>
           <asp:Button ID="btnTestGlobal" runat="server"
               Text="Show Application State"
               OnClick="btnTestGlobal_Click" />
           <br /><br />
           <asp:Label ID="lblGlobalResult" runat="server" />
       </div>


       <!-- TryIt: Captcha User Control -->
       <h2>TryIt Captcha User Control</h2>
       <div class="tryit-box">
           <p style="margin:0 0 8px;">
               <strong>Component:</strong> CaptchaControl.ascx reusable
               .ascx user control.<br />
               Generates a random addition question and stores the answer in
               Session state. Exposes IsValid() → bool and ShowResult(bool)
               for use by any host page.<br />
               <strong>Input:</strong> User-entered integer &nbsp;|&nbsp;
               <strong>Output:</strong> Pass / fail feedback shown inline.
           </p>
           <uc:Captcha ID="MyCaptcha" runat="server" />
           <asp:Button ID="btnTestCaptcha" runat="server"
               Text="Validate Captcha"
               OnClick="btnTestCaptcha_Click"
               style="margin-top:8px;" />
       </div>


       <!-- TryIt: RSVP Web Service -->
       <h2>TryIt RSVP Web Service</h2>
       <div class="tryit-box">
           <p style="margin:0 0 8px;">
               <strong>Component:</strong> RsvpService.svc WCF SOAP service
               deployed on WebStrar.<br />
               Operations: AddRsvp(eventId, username) → string,
               CancelRsvp(eventId, username) → string,
               GetAttendeeCount(eventId) → int.<br />
               State is persisted in App_Data/Rsvp.xml between calls.<br />
               <strong>Input:</strong> Event ID text box &nbsp;|&nbsp;
               <strong>Output:</strong> Attendee count returned by the service.
           </p>
           <asp:Label runat="server" Text="Event ID: " />
           <asp:TextBox ID="txtEventId" runat="server"
               Text="EVT001" style="width:100px; margin-right:8px;" />
           <asp:Button ID="btnTestRsvp" runat="server"
               Text="Get Attendee Count"
               OnClick="btnTestRsvp_Click" />
           <br /><br />
           <asp:Label ID="lblRsvpResult" runat="server" />
       </div>


       <!-- Service Directory Table -->
       <h2>Service Directory</h2>
       <table>
           <tr>
               <th>Provider</th>
               <th>Type</th>
               <th>Operation</th>
               <th>Input &rarr; Output</th>
               <th>Description</th>
           </tr>
           <tr>
               <td>Rudra Shah</td>
               <td>ASPX page</td>
               <td>Default.aspx</td>
               <td>None &rarr; HTML page</td>
               <td>Public homepage with service directory and TryIt sections
                   for all components</td>
           </tr>
           <tr>
               <td>Rudra Shah</td>
               <td>Global.asax</td>
               <td>Application_Start, Session_Start, Session_End,
                   Application_End, Application_Error</td>
               <td>None &rarr; Application / Session state loaded</td>
               <td>Loads event categories, tracks visit count per session,
                   initialises per-session browsed-events list, logs errors</td>
           </tr>
           <tr>
               <td>Rudra Shah</td>
               <td>User control (.ascx)</td>
               <td>IsValid() &rarr; bool, ShowResult(bool)</td>
               <td>User integer input &rarr; bool</td>
               <td>Math captcha stored in Session state; reused on Member
                   signup page</td>
           </tr>
           <tr>
               <td>Rudra Shah</td>
               <td>WCF service (.svc)</td>
               <td>AddRsvp(eventId, username) &rarr; string;
                   CancelRsvp(eventId, username) &rarr; string;
                   GetAttendeeCount(eventId) &rarr; int</td>
               <td>string, string &rarr; string / int</td>
               <td>RSVP service persisting state to Rsvp.xml; deployed on
                   WebStrar for event attendance tracking</td>
           </tr>
       </table>


   </form>
</body>
</html>




