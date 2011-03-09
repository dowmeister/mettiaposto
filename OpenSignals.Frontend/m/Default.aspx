<%@ Page EnableViewState="false" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs"
    Inherits="OpenSignals.Frontend.Mobile.Default" %>

<%@ Register Src="/Includes/Analytics.ascx" TagName="Analytics" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Mettiaposto.it Mobile - Invia segnalazioni di disservizi, problemi della tua
        città o del tuo quartiere</title>
    <meta http-equiv="Content-Language" content="it" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0; user-scalable=no" />
    <meta name="HandheldFriendly" content="true" />
    <link rel="shortcut icon" href="http://www.mettiaposto.it/favicon.ico" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.4.4/jquery.min.js"></script>
    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.0a3/jquery.mobile-1.0a3.min.css" />
    <link href="/css/mobile.css" rel="Stylesheet" type="text/css" />
    <script src="http://code.jquery.com/mobile/1.0a3/jquery.mobile-1.0a3.min.js" type="text/javascript"></script>
    <script src="http://maps.google.com/maps/api/js?sensor=true&amp;region=it" type="text/javascript"></script>
    <script src="/js/mapManager.js" type="text/javascript"></script>
    <script src="/js/mobile.js" type="text/javascript"></script>
    <script src="/js/validation.js" type="text/javascript"></script>
    <script src="/js/functions.js" type="text/javascript"></script>
    <script src="/Ajax/JSONService.ashx?proxy" type="text/javascript"></script>
    <script src="/js/json.js" type="text/javascript"></script>
    <script src="/js/jquery/plugins/ajaxfileupload.js" type="text/javascript"></script>
    <uc4:Analytics ID="Analytics1" runat="server" EnableViewState="false" />
</head>
<body class="ui-mobile-viewport">
    <div id="defaultPage" data-role="page" data-theme="b" data-back-btn-text="Back">
        <div data-role="header" class="header">
            <div id="logo"> </div>
        </div>
        <div data-role="content" class="content">
            <div class="submit">
                <h1>
                    Invia la tua segnalazione</h1>
                <form id="form1" runat="server" enableviewstate="false">
                <div data-role="fieldcontain">
                    <label class="ui-input-text">
                        Posizione attuale</label>
                    <asp:TextBox Text="Localizzazione in corso.." TabIndex="1" EnableViewState="false"
                        ID="txtAddress" runat="server"></asp:TextBox>
                    <a title="Trova l'indirizzo sulla mappa" href="javascript:;" onclick="geolocalizeByAddress(); return false;">
                        <img alt="Trova l'indirizzo sulla mappa" src="/images/locate-me.png" /></a>
                    <br />
                    Se la localizzazione non è corretta puoi usare la <a href="javascript:;" onclick="initMap(); return false">
                        mappa</a> o modificare l'<a href="javascript:;" onclick="$('#txtAddress').focus(); return false;">indirizzo</a>.
                </div>
                <div style="display: none; width: 100%; height: 300px" id="map">
                </div>
                <div id="form" style="display: none;">
                    <div data-role="fieldcontain">
                        <label class="ui-input-text">
                            Oggetto della segnalazione</label>
                        <asp:TextBox TabIndex="2" ID="txtSubject" EnableViewState="false" runat="server"></asp:TextBox>
                    </div>
                    <div data-role="fieldcontain">
                        <label class="ui-input-text">
                            Descrizione della segnalazione</label>
                        <asp:TextBox TabIndex="3" ID="txtDescription" EnableViewState="false" runat="server"
                            TextMode="MultiLine" Rows="10" Columns="20"></asp:TextBox>
                    </div>
                    <div data-role="fieldcontain">
                        <label class="ui-input-text">
                            Categoria</label>
                        <asp:DropDownList TabIndex="4" data-native-menu="true" DataTextField="Name" DataValueField="CategoryID"
                            ID="ddlCategories" EnableViewState="false" runat="server">
                        </asp:DropDownList>
                    </div>
                    <div data-role="fieldcontain">
                        <label class="ui-input-text">
                            E-Mail</label>
                        <asp:TextBox TabIndex="5" EnableViewState="false" ID="txtEmail" runat="server"></asp:TextBox></div>
                    <div data-role="fieldcontain">
                        <label class="ui-input-text">
                            Carica una foto</label>
                        <asp:FileUpload EnableViewState="false" ID="fuFile" runat="server" />
                    </div>
                    <a href="javascript:;" class="ui-btn-right" data-role="button" onclick="sendSignal(); return false;"
                        data-icon="check">Invia</a>
                </div>
                </form>
            </div>
        </div>
        <div data-role="footer" id="mobile-footer" class="ui-bar" data-theme="b">
            <a data-role="button" rel="external" data-icon="home" href="/Default.aspx?nomobile=true">
                Sito Completo</a> <a data-icon="info" data-role="button" href="/m/content.aspx?page=info">
                    FAQ</a> <a data-role="button" data-icon="info" href="/m/content.aspx?page=privacy">Privacy</a>
        </div>
    </div>
    <div id="submitResult" class="messages" data-role="page" data-theme="b" data-back-btn-text="Back">
        <div data-role="header" class="header" data-backbtn="false">
            <h1>
                Mettiaposto.it</h1>
            <a href="#" onclick="$.mobile.changePage($('#defaultPage'));" data-role="button"
                data-icon="arrow-l">Indietro</a>
        </div>
        <div data-role="content" class="content">
            <div class="messages" id="messages">
            </div>
        </div>
    </div>
</body>
</html>
