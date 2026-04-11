<%@ Page="" Language="C#" %>

	<!DOCTYPE html>
	<html>
		<body>
			<form runat="server">

				<h2>Weather TryIt (Arizona Cities)</h2>

				City (AZ only):
				<asp:TextBox ID="txtCity" runat="server" /><br />

				Date (yyyy-MM-dd):
				<asp:TextBox ID="txtDate" runat="server" /><br />

				<asp:Button ID="btnGet" runat="server" Text="Get Weather" OnClick="btnGet_Click" /><br /><br />

				<asp:Label ID="lblResult" runat="server" />

			</form>

			<script runat="server">
				protected void btnGet_Click(object sender, EventArgs e)
				{
				Project5_Service.WeatherService svc = new Project5_Service.WeatherService();
				lblResult.Text = svc.GetWeather(txtCity.Text, txtDate.Text);
				}
			</script>

		</body>
	</html>