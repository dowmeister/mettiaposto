<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="OpenSignals.Frontend.Index" %>

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
    <meta property="og:image" content="http://www.mettiaposto.it/images/logo.png" />
    <uc4:Analytics ID="Analytics1" runat="server" />
    <os:StaticFileManager ID="staticFileManager" ContextKey="index" runat="server">
        <os:StaticFile Url="/js/mapManager.js" Type="Javascript" />
        <os:StaticFile Url="/js/index.functions.js" Type="Javascript" />
    </os:StaticFileManager>
</head>
<body>
    <form id="form1" runat="server">
    <uc3:Header ID="Header1" runat="server" TabSelected="1" />
    <div id="content">
        <div class="homeMap" id="map">
            <div class="mapLoader">
            </div>
        </div>
        <div class="clear">
        </div>
        <div style="float: left; top: -20; width: 430px">
            <h2 class="homeclaim">
                Hai visto qualcosa che non va nella tua città?</h2>
            Mettiaposto.it ti permette di inviare segnalazioni riguardanti problemi o disservizi
            del tuo quartiere, come buche, spazzatura in strada, marciapiedi danneggiati o graffiti
            sui muri.
            <br />
            Per dubbi, chiarimenti, informazioni, per capire come funziona questo sito <a href="/pages/info.aspx">
                leggi le FAQ</a>.
        </div>
        <div style="float: right; width: 470px">
            <div class="home">
                Inserisci l'indirizzo (compreso via, viale, piazza o altro)
                <div class="input">
                    <asp:TextBox MaxLength="150" ID="txtSearch" CssClass="search" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="clear" style="text-align: right; width: 470px">
                <input class="gobutton" id="searchButton" type="button" value="Segnala" onclick="search(); return false;"></input>
            </div>
        </div>
        <div class="clear">
        </div>
    </div>
    <uc1:Footer ID="Footer1" runat="server" />
    </form>
    <script src="http://maps.google.com/maps/api/js?sensor=true&amp;region=it" type="text/javascript"></script>
    <script src="/Ajax/JSONService.ashx?proxy" type="text/javascript"></script>
    <script type="text/javascript" src="/js/StaticFileHandler.ashx?key=common,index"></script>
</body>
</html>
