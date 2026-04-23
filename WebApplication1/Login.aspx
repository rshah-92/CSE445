<%@ Page Language="C#" AutoEventWireup="true" 
    CodeBehind="Login.aspx.cs" 
    Inherits="WebApplication1.Login" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Login — Campus Event Board</title>
    <style>
        body { font-family: Arial, sans-serif; max-width: 400px; 
               margin: 80px auto; padding: 20px; }
        h2 { color: #8C1D40; }
        .field { margin-bottom: 12px; }
        label { display: block; font-size: 13px; margin-bottom: 4px; }
        input[type=text], input[type=password] { 
            width: 100%; padding: 8px; box-sizing: border-box; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Campus Event Board — Login</h2>

        <div class="field">
            <asp:Label runat="server" Text="Username:" />
            <asp:TextBox ID="txtUsername" runat="server" />
        </div>
        <div class="field">
            <asp:Label runat="server" Text="Password:" />
            <asp:TextBox ID="txtPassword" runat="server" 
                TextMode="Password" />
        </div>

        <asp:Button ID="btnLogin" runat="server" Text="Login" 
            OnClick="btnLogin_Click" 
            style="padding:8px 20px; background:#8C1D40; 
                   color:white; border:none; cursor:pointer;" />
        <br /><br />
        <asp:Label ID="lblError" runat="server" 
            style="color:red; font-size:13px;" />

        <p style="font-size:12px; color:#666; margin-top:20px;">
            Don't have an account? 
            <asp:HyperLink runat="server" NavigateUrl="~/Member.aspx">
                Register here</asp:HyperLink>
        </p>
    </form>
</body>
</html>


