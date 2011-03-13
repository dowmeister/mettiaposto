<%@ Page Language="C#" Inherits="OpenSignals.Framework.Core.Base.BasePage" %>

<%
    RegisterAjaxSessionKey();  
%>
<%@ Register Src="Includes/Analytics.ascx" TagName="Analytics" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Mettiaposto.it - Segnala disservizi, problemi e malfunzionamenti della tua città!
    </title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="description" runat="server" id="metaDescription" content="Segnala disservizi, problemi e malfunzionamenti della tua città" />
    <meta name="keywords" runat="server" id="metaKeywords" content="segnala, disservizi, città, tua città, mia città, milano, comune, problemi, buche, graffiti, spazzatura" />
    <link rel="canonical" href="http://www.mettiaposto.it/" />
    <link rel="icon" href="http://www.mettiaposto.it/favicon.ico" />
    <link rel="shortcut icon" href="http://www.mettiaposto.it/favicon.ico" />
    <link rel="image_src" href="http://www.mettiaposto.it/images/logo.png" />
    <link href="/css/teaser.css" media="projection,screen" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.5.1/jquery.min.js"></script>
    <meta property="fb:app_id" content="183751108307062" />
    <meta property="fb:admins" content="1437896726,800734657" />
    <meta property="og:site_name" content="Mettiaposto.it" />
    <meta property="og:type" content="website" />
    <meta property="og:title" content="Mettiaposto.it - Stiamo arrivando!" />
    <meta property="og:description" content="Segnala disservizi, problemi e malfunzionamenti della tua città" />
    <meta property="og:image" content="http://www.mettiaposto.it/images/logo.png" />
    <script src="/js/functions.js" type="text/javascript"></script>
    <script src="/js/json.js" type="text/javascript"></script>
    <script src="/Ajax/JSONService.ashx?proxy" type="text/javascript"></script>
    <script src="http://connect.facebook.net/it_IT/all.js" type="text/javascript"></script>
    <script src="http://static.ak.fbcdn.net/connect.php/js/FB.Share" type="text/javascript"></script>
    <script type="text/javascript" src="http://platform.twitter.com/widgets.js"></script>
    <uc4:Analytics ID="Analytics1" runat="server" />
</head>
<body>
    <form runat="server" id="teaserForm">
    <div id="logo">
        <img src="/images/teaser/logointro.png" alt="Mettiaposto.it" />
    </div>
    <div class="subscribe">
        <div id="newsletter">
            <h2>
                Benvenuto su <strong>Mettiaposto.it</strong>, il nuovo servizio per segnalare ciò
                che non va nella tua città! Hai notato rifiuti, graffiti, buche stradali, buche
                in strada, illuminazione non funzionante, verde pubblico in cattivo stato...?
                <br />
                <br />
                <strong>Fra qualche settimana potrai usare Mettiaposto per le tue segnalazioni!</strong>
            </h2>
            <br />
            <br />
            <strong>Vuoi essere informato su quando sarà attivo il servizio al pubblico?
                <br />
                Inserisci la tua mail e iscriviti alla newsletter! </strong>
            <br />
            <input type="text" id="txtEmail" name="txtEmail" />
            <input type="button" class="success" value="Iscriviti" onclick="subscribe(); return false;" />
        </div>
        <div id="message">
        </div>
        <div class="shareBox">
            <ul>
                <li><a name="fb_share" type="button">Convidivi su Facebook</a> </li>
                <li><a href="http://twitter.com/share" class="twitter-share-button" data-count="horizontal"
                    data-via="mettiaposto" data-lang="it">Tweet</a> </li>
                <li>
                    <fb:like layout="button_count" show_faces="false"></fb:like>
                </li>
            </ul>
            <div class="clear">
            </div>
        </div>
    </div>
    <div id="fb-root">
    </div>
    <script type="text/javascript">
        $(document).ready(function () { FB.init({ appId: '183751108307062', cookie: true, xfbml: true }); });

        function subscribe()
        {
            if ($('#txtEmail').val() != '')
            {
                $('#newsletter').hide();
                writeAjax('#message');
                var proxy = new JSONService();
                proxy.subscribeToNewsletter({ ajaxSessionKey: ajaxSessionKey, email: $('#txtEmail').val() });
                hideAjax('#message');
                writeMessage('Iscrizione avvenuta con successo', 'Grazie per esserti iscritto al lancio della beta, torna a trovarci!', '#message');
            }
            else
            {
                alert("Inserisci un'email valida");
            }
        }
    </script>
    </form>
</body>
</html>
