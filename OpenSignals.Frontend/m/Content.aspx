<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Content.aspx.cs" Inherits="OpenSignals.Frontend.Mobile.Content" %>

<%@ Register Src="/Includes/Analytics.ascx" TagName="Analytics" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Mettiaposto.it Mobile - Invia segnalazioni di disservizi, problemi della tua
        città o del tuo quartiere</title>
    <meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0; user-scalable=no" />
    <meta name="HandheldFriendly" content="True" />
    <link rel="shortcut icon" href="http://www.mettiaposto.it/favicon.ico" />
    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.0a4.1/jquery.mobile-1.0a4.1.min.css" />
    <link href="/css/mobile.css" rel="Stylesheet" type="text/css" />
    <script src="http://code.jquery.com/jquery-1.5.2.min.js" type="text/javascript"></script>
    <script src="http://code.jquery.com/mobile/1.0a4.1/jquery.mobile-1.0a4.1.min.js" type="text/javascript"></script>
    <uc4:Analytics ID="Analytics1" runat="server" />
</head>
<body class="ui-mobile-viewport">
    <div data-role="page" data-theme="b" data-back-btn-text="Indietro">
        <div data-role="header" class="header">
            <h1>Mettiaposto Mobile</h1>
            <a href="/m/"  data-iconpos="notext" rel="external" data-role="button"
                data-icon="home" class="ui-btn-right" data-direction="reverse">Home</a>
        </div>
        <div data-role="content" class="content" runat="server" id="divContent">
        </div>
        <div data-role="footer" class="ui-bar" data-theme="b">
            <h4><a rel="external" data-role="button" data-icon="home" href="/">Sito Completo</a> <a data-icon="info"
                data-role="button" href="/m/" rel="external">Segnala</a> <a data-icon="info"
                data-role="button" href="/m/content.aspx?page=info">FAQ</a> <a data-role="button"
                    data-icon="info" href="/m/content.aspx?page=privacy">Privacy</a></h4>
        </div>
    </div>
</body>
</html>
