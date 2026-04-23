<%@ Control Language="C#" AutoEventWireup="true" 
    CodeBehind="CaptchaControl.ascx.cs" 
    Inherits="WebApplication1.Controls.CaptchaControl" %>

<div style="margin: 10px 0;">
    <asp:Label ID="lblCaptchaQuestion" runat="server" 
        style="font-weight:bold; font-size:14px;" />
    <br />
    <asp:TextBox ID="txtCaptchaAnswer" runat="server" 
        placeholder="Enter answer here" 
        style="margin-top:5px; padding:5px; width:150px;" />
    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" 
        OnClick="btnRefresh_Click" 
        style="margin-left:8px; padding:5px 10px;" />
    <br />
    <asp:Label ID="lblCaptchaResult" runat="server" 
        style="font-size:12px; margin-top:4px;" />
</div>


