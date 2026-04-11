<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs"
    Inherits="Project5_WebApp.Default" %>

<!DOCTYPE html>
<html>
<body>
<form runat="server">

<h2>Cookie Demo</h2>
Username: <asp:TextBox ID="txtUser" runat="server" /><br />
Campus: <asp:TextBox ID="txtCampus" runat="server" /><br />
<asp:Button ID="btnSave" runat="server" Text="Save Cookie" OnClick="btnSave_Click" /><br />
<asp:Label ID="lblCookie" runat="server" /><br /><br />

<h2>Password Hashing</h2>
Password: <asp:TextBox ID="txtPwd" runat="server" TextMode="Password" /><br />
<asp:Button ID="btnHash" runat="server" Text="Hash" OnClick="btnHash_Click" /><br />
<asp:Label ID="lblHash" runat="server" /><br /><br />

<h2>Weather TryIt</h2>
City (AZ only): <asp:TextBox ID="txtCity" runat="server" /><br />
Date (yyyy-MM-dd) 7 days forecast of dates allowed: <asp:TextBox ID="txtDate" runat="server" /><br />
<asp:Button ID="btnWeather" runat="server" Text="Get Weather" OnClick="btnWeather_Click" /><br />
<asp:Label ID="lblWeather" runat="server" /><br /><br />

<a href="Member.aspx">Member Page</a><br />
<a href="Staff.aspx">Staff Page</a>

<h2>Application and Components Summary Table</h2>

<table border="1" cellpadding="6" cellspacing="0" style="border-collapse:collapse;">
  <thead>
    <tr>
      <th><strong>Component Name</strong></th>
      <th><strong>Provider (Your Name)</strong></th>
      <th><strong>Description</strong></th>
      <th><strong>TryIt Location</strong></th>
      <th><strong>How to Test</strong></th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td><strong>SHA-256 Hashing (DLL Component)</strong></td>
      <td><strong>Sai</strong></td>
      <td>Hashes input using SHA-256 inside Project5_LocalComponents SecurityUtils file</td>
      <td>Default.aspx → Password Hashing section</td>
      <td>Enter password → click Hash → hashed value appears in label</td>
    </tr>
    <tr>
      <td><strong>Cookie Component</strong></td>
      <td><strong>Sai</strong></td>
      <td>Saves and loads cookie named UserInfo containing Username and Campus</td>
      <td>Default.aspx → Cookie Demo section</td>
      <td>Enter Username and Campus → click Save Cookie → refresh page to verify auto-fill</td>
    </tr>
    <tr>
      <td><strong>Session Component</strong></td>
      <td><strong>Sai</strong></td>
      <td>Stores temporary session data</td>
      <td>Default.aspx</td>
      <td>Enter value → Save Session → Load Session to verify stored value</td>
    </tr>
    <tr>
      <td><strong>Weather Service</strong></td>
      <td><strong>Sai</strong></td>
      <td>Provides weather info for AZ cities and uses Open-Meteo API</td>
      <td>Default.aspx → Weather TryIt section</td>
      <td>Enter city (AZ) and valid date → click Get Weather → result appears in label</td>
    </tr>
    <tr>
      <td><strong>Service Directory Page</strong></td>
      <td><strong>Sai</strong></td>
      <td>Lists services and TryIt links for testing</td>
      <td>Default.aspx which links to Member.aspx and Staff.aspx</td>
      <td>Click links to navigate to each TryIt page</td>
    </tr>
    <tr>
      <td><strong>Global.asax Application and Session Start</strong></td>
      <td><strong>Sai</strong></td>
      <td>Tracks application and session start events</td>
      <td>Global.asax file in project root</td>
      <td>TA verifies file exists and application starts without errors</td>
    </tr>
  </tbody>
</table>

</form>
</body>
</html>