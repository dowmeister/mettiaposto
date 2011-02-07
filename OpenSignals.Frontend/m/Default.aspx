<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="OpenSignals.Frontend.m.Default" %>
<%@ Register Src="/Includes/Analytics.ascx" TagName="Analytics" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mettiaposto.it Mobile - Invia segnalazioni di disservizi, problemi della tua
        città o del tuo quartiere</title>
    <meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0; user-scalable=no" />
    <meta name="HandheldFriendly" content="True" />
    <link rel="shortcut icon" href="http://www.mettiaposto.it/favicon.ico" />
    <link href="/css/mobile.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.4.4/jquery.min.js"></script>
    <script src="http://maps.google.com/maps/api/js?sensor=true&amp;region=it" type="text/javascript"></script>
    <script src="/js/map.js" type="text/javascript"></script>
    <script src="/js/mobile.js" type="text/javascript"></script>
    <script src="/js/validation.js" type="text/javascript"></script>
    <script src="/js/functions.js" type="text/javascript"></script>
    <script src="/Ajax/JSONService.ashx?proxy" type="text/javascript"></script>
    <script src="/js/json.js" type="text/javascript"></script>
    <script src="/js/jquery/plugins/ajaxfileupload.js" type="text/javascript"></script>
    <uc4:analytics id="Analytics1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="wrapper">
        <div class="header">
            <img src="/images/logo_beta.jpg" />
        </div>
        <div class="content">
            <div id="messages">
            </div>
            <div class="submit">
                <h1>
                    Invia la tua segnalazione</h1>
                <ul>
                    <li>
                        <label>
                            Posizione attuale</label></li>
                    <li>
                        <asp:TextBox onblur="geolocalizeByAddress()" ID="txtAddress" runat="server"></asp:TextBox>
                        <br />
                        Se la localizzazione non è corretta puoi usare la <a href="javascript:;" onclick="initMap(); return false">
                            mappa</a> o modificare l'<a href="javascript:;" onclick="$('#txtAddress').focus(); return false;">indirizzo</a>
                        mappa</a>
                        <br />
                        <div style="display: none; width: 100%; height: 300px" id="map">
                        </div>
                    </li>
                    <li>
                        <label>
                            Oggetto della segnalazione</label></li>
                    <li>
                        <asp:TextBox ID="txtSubject" runat="server"></asp:TextBox></li>
                    <li>
                        <label>
                            Descrizione della segnalazione</label></li>
                    <li>
                        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="10" Columns="20"></asp:TextBox></li>
                    <li>
                        <label>
                            Categoria</label></li>
                    <li>
                        <asp:DropDownList DataTextField="Name" DataValueField="CategoryID" ID="ddlCategories"
                            runat="server">
                        </asp:DropDownList>
                    </li>
                    <li>
                        <label>
                            E-Mail</label></li>
                    <li>
                        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox></li>
                    <li>
                        <label>
                            Carica una foto</label></li>
                    <li>
                        <asp:FileUpload ID="fuFile" runat="server" /></li>
                </ul>
                <div class="button">
                    <a href="#" onclick="sendSignal();">Invia</a>
                </div>
            </div>
        </div>
        <div class="footer">
            <a href="/">Sito Completo</a> | <a href="/m/content.aspx?page=faq">FAQ</a> | <a href="/m/content.aspx?page=privacy">
                Privacy</a>
        </div>
    </div>
    </form>
</body>
</html>
