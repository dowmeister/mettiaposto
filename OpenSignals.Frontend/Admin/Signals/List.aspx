<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="OpenSignals.Frontend.Admin.Signals.List" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <osadmin:head runat="server" id="head"></osadmin:head>
    <script>
        function performAction(params) {
            switch (params.action) {
                case 'delete':
                    if (confirm('Cancellare la segnalazione?'))
                        _performAction(params);
                    break;
                case 'approve':
                    if (confirm('Confermare la segnalazione?'))
                        _performAction(params);
                    break;
                case 'reject':
                    if (confirm('Rifiutare la segnalazione?'))
                        _performAction(params);
                    break;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <osadmin:header runat="server" id="header"></osadmin:header>
    <div id="content">
        <div class="form">
            <h1>
                Elenco Segnalazioni</h1>
            <asp:Table EnableViewState="false" ID="tblList" runat="server" CssClass="listTable">
            </asp:Table>
        </div>
    </div>
    <osadmin:footer runat="server" id="footer"></osadmin:footer>
    <asp:LinkButton ID="lnkAction" runat="server" onclick="lnkAction_Click"></asp:LinkButton>
    </form>
</body>
</html>
