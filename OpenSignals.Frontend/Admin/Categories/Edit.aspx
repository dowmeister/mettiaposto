<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="OpenSignals.Frontend.Admin.Categories.Edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <osadmin:head runat="server" id="head"></osadmin:head>
</head>
<body>
    <form id="form1" runat="server">
    <osadmin:header runat="server" id="header"></osadmin:header>
    <div id="content">
        <div class="form">
            <h1>
                Modifica Categoria</h1>
            <table class="inputTable">
                <tr>
                    <td class="label">
                        Nome
                    </td>
                    <td class="input">
                        <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Attiva
                    </td>
                    <td class="input">
                        <asp:CheckBox ID="chkActive" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="buttons">
                        <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Salva" OnClick="btnSave_Click" />
                        <input type="reset" class="button" value="Annulla" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <osadmin:footer runat="server" id="footer"></osadmin:footer>
    </form>
</body>
</html>
