<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="OpenSignals.Frontend.Admin.Signals.Edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <osadmin:head runat="server" id="head"></osadmin:head>
</head>
<body>
    <form id="form1" runat="server">
    <osadmin:header runat="server" id="header"></osadmin:header>
    <table class="inputTable">
        <tr>
            <td class="label">
                Label
            </td>
            <td class="input">
                <input type="text" />
            </td>
        </tr>
        <tr>
            <td class="label">
                Label
            </td>
            <td class="input">
                <input type="checkbox" />
            </td>
        </tr>
        <tr>
            <td class="label">
                Label
            </td>
            <td class="input">
                <input type="password" />
            </td>
        </tr>
        <tr>
            <td class="label">
                Label
            </td>
            <td class="input">
                <input type="radio" />
            </td>
        </tr>
        <tr>
            <td class="label">
                Label
            </td>
            <td class="input">
                <select>
                </select>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="buttons">
                <asp:Button ID="btnSave" runat="server" CssClass="button" />
            </td>
        </tr>
    </table>
    <osadmin:footer runat="server" id="footer"></osadmin:footer>
    </form>
</body>
</html>
