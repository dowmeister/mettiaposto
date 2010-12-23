<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FixMi.Frontend._Default" %>

<%@ Register Src="Includes/Footer.ascx" TagName="Footer" TagPrefix="uc1" %>
<%@ Register Src="Includes/Head.ascx" TagName="Head" TagPrefix="uc2" %>
<%@ Register Src="Includes/Header.ascx" TagName="Header" TagPrefix="uc3" %>
<%@ Register src="Includes/Analytics.ascx" tagname="Analytics" tagprefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <uc2:Head ID="ucHead" runat="server" />
    <script src="/js/index.functions.js" type="text/javascript"></script>
    <uc4:Analytics ID="Analytics1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <uc3:Header ID="Header1" runat="server" />
    <div id="content">
        <div class="claim">
            <a href="/milano/invia.aspx"></a>
        </div>
        <div class="presentation">
            <div class="statsBox totalSignals">
                <div>
                    Segnalazioni<br />
                    totali:
                    <div>
                        <asp:Literal ID="ltTotals" runat="server"></asp:Literal></div>
                </div>
            </div>
            <div class="home">
                <div class="start">
                    <h2>
                        Inserisci un CAP, l'indirizzo o un quartiere di Milano per segnalare un problema</h2>
                    <asp:TextBox ID="txtSearch" CssClass="search" runat="server" Text="CAP, indirizzo, quartiere..."></asp:TextBox>
                    <input class="success" id="searchButton" type="button" value="Vai" onclick="search(); return false;"></input>
                </div>
            </div>
            <div class="statsBox resolvedSignals">
                <div>
                    Segnalazioni<br />
                    risolte:
                    <div>
                        <asp:Literal ID="ltResolved" runat="server"></asp:Literal></div>
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
    <div id="hshadow">
    </div>
    <uc1:Footer ID="Footer1" runat="server" />
    </form>
</body>
</html>
