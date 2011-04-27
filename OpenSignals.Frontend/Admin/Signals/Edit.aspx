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
                Dettaglio Segnalazione</h1>
            <table class="inputTable">
                <tr>
                    <td class="label">
                        Stato</td>
                    <td class="input">
                        <asp:DropDownList ID="ddlStatus" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Città
                    </td>
                    <td class="input">
                        <asp:DropDownList ID="ddlPlaces" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Categoria
                    </td>
                    <td class="input">
                        <asp:DropDownList ID="ddlCategories" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Oggetto
                    </td>
                    <td class="input">
                       <asp:TextBox ID="txtSubject" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Descrizione
                    </td>
                    <td class="input">
                       <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Autore
                    </td>
                    <td class="input">
                        <asp:TextBox ID="txtAuthor" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Email
                    </td>
                    <td class="input">
                        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Allegato</td>
                    <td class="input">
                        <asp:Image ID="imgImage" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Posizione
                    </td>
                    <td class="input">
                        <div id="map"></div></td>
                </tr>
                <tr>
                    <td class="label">
                        Data di creazione:
                    </td>
                    <td class="input">
                        <asp:Label ID="lblCreationDate" runat="server"></asp:Label>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Ultima modifica:
                    </td>
                    <td class="input">
                        <asp:Label ID="lblUpdateDate" runat="server"></asp:Label>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Data risolunzione:
                    </td>
                    <td class="input">
                        <asp:Label ID="lblResolutionDate" runat="server"></asp:Label>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Commento:
                    </td>
                    <td class="input">
                        <asp:TextBox ID="txtResolutionComment" runat="server" TextMode="MultiLine"></asp:TextBox>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="buttons">
                        <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Salva" />
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
