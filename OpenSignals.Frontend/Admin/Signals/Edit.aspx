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
    <div id="content">
        <div class="form">
            <h1>
                Modifica Segnalazione</h1>
            <table class="inputTable">
                <tr>
                    <td class="label">
                        Stato
                    </td>
                    <td class="input">
                        <asp:DropDownList ID="DropDownList2" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Città
                    </td>
                    <td class="input">
                        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Categoria
                    </td>
                    <td class="input">
                        <asp:DropDownList ID="DropDownList1" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Oggetto
                    </td>
                    <td class="input">
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Descrizione
                    </td>
                    <td class="input">
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Posizione
                    </td>
                    <td class="input">
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Data creazione
                    </td>
                    <td class="input">
                        <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Ultima modifica
                    </td>
                    <td class="input">
                        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="buttons">
                        <asp:Button Text="Salva" ID="btnSave" runat="server" CssClass="button" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <osadmin:footer runat="server" id="footer"></osadmin:footer>
    </form>
</body>
</html>
