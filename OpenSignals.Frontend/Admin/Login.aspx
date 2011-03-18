<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="OpenSignals.Frontend.Admin.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>OpenSignals Admin - Login</title>
    <osadmin:head runat="server" id="head"></osadmin:head>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Login LoginButtonText="Login" ID="LoginBox" runat="server" UserNameLabelText="Username" PasswordLabelText="Password"
        RememberMeText="Ricorda login" FailureText="Credenziali non valide" TitleText="OpenSignals Admin - Login">
    </asp:Login>
    </form>
</body>
</html>
