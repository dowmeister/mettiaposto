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
        <div style="float: left; width: 500px; height: 500px;" id="map">
            <div class="message ajax" style="display: block;">
                <img style="margin-top: 250px; margin-bottom: 200px;" alt="Caricamento in corso.."
                    src="/images/ajax-loader.gif" /></div>
        </div>
        <div style="float: right; width: 430px">
            <h2 class="homeclaim">
                Hai visto qualcosa che non va nella tua città? Mettiaposto.it può aiutarti!!</h2>
            Perchè ti permette di inviare segnalazioni riguardanti problemi o disservizi del
            tuo quartiere, come buche, spazzatura in strada, marciapiedi danneggiati o graffiti
            sui muri.
            <br />
            Per dubbi, chiarimenti, informazioni, per capire come funziona questo sito <a href="/pages/info.aspx">
                leggi le FAQ</a>.
            <h2 class="homeclaim">
                Segnala un poblema!</h2>
            <div class="home">
                Inserisci l'indirizzo (compreso via, viale, piazza o altro)
                <div class="input">
                    <asp:TextBox MaxLength="150" ID="txtSearch" CssClass="search" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="clear" style="text-align: right; width: 440px">
                <input class="gobutton" id="searchButton" type="button" value="Segnala" onclick="search(); return false;"></input>
            </div>
        </div>
        <div style="clear: both;">
        </div>
    </div>
    <uc1:Footer ID="Footer1" runat="server" />
    <script src="http://maps.google.com/maps/api/js?sensor=true&amp;region=it" type="text/javascript"></script>
    <script src="/Ajax/JSONService.ashx?proxy" type="text/javascript"></script>
    <script type="text/javascript" src="/js/StaticFileHandler.ashx?key=common,index"></script>
    </form>
</body>
</html>
