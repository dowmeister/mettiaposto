﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FixMi.Frontend._Default" %>

<%@ Register Src="Includes/Footer.ascx" TagName="Footer" TagPrefix="uc1" %>
<%@ Register Src="Includes/Head.ascx" TagName="Head" TagPrefix="uc2" %>
<%@ Register Src="Includes/Header.ascx" TagName="Header" TagPrefix="uc3" %>
<%@ Register Src="Includes/Analytics.ascx" TagName="Analytics" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mettiaposto.it - Segnala disservizi, problemi e malfunzionamenti della tua città!
    </title>
    <uc2:Head ID="ucHead" runat="server" />
    <meta property="og:title" content="Mettiaposto.it" />
    <meta property="og:description" content="Segnala disservizi, problemi e malfunzionamenti della tua città" />
    <meta property="og:image" content="http://www.mettiaposto.it/images/logo.jpg" />
    <meta name="description" content="Segnala disservizi, problemi e malfunzionamenti della tua città" />
    <meta name="keywords" content="segnala, disservizi, città, tua città, mia città, milano, comune, problemi, buche, graffiti, spazzatura" />
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
                <h2>
                    Inserisci l'indirizzo per iniziare la segnalazione</h2>
                <div class="input">
                    <asp:TextBox MaxLength="150" ID="txtSearch" CssClass="search" runat="server" Text="CAP, indirizzo, quartiere..."></asp:TextBox>
                    a
                    <asp:DropDownList ID="ddlCities" DataValueField="Name" DataTextField="Name" runat="server">
                    </asp:DropDownList>
                </div>
                <div class="button" style="text-align: center;">
                    <input class="gobutton" id="searchButton" type="button" value="Vai" onclick="search(); return false;"></input>
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
