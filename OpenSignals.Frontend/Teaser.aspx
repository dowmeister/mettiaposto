<%@ Page Language="C#" Inherits="OpenSignals.Framework.Core.Base.BasePage" %>

<%
    RegisterAjaxSessionKey();  
%>
<%@ Register Src="Includes/Footer.ascx" TagName="Footer" TagPrefix="uc1" %>
<%@ Register Src="Includes/Head.ascx" TagName="Head" TagPrefix="uc2" %>
<%@ Register Src="Includes/Header.ascx" TagName="Header" TagPrefix="uc3" %>
<%@ Register Src="Includes/Analytics.ascx" TagName="Analytics" TagPrefix="uc4" %>
<%@ Register Src="Includes/StaticFileManager.ascx" TagName="StaticFileManager" TagPrefix="uc5" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Mettiaposto.it - Segnala disservizi, problemi e malfunzionamenti della tua città!
    </title>
    <uc2:Head ID="ucHead" runat="server" />
    <meta property="og:title" content="Mettiaposto.it - Stiamo arrivando!" />
    <meta property="og:description" content="Segnala disservizi, problemi e malfunzionamenti della tua città" />
    <meta property="og:image" content="http://www.mettiaposto.it/images/logo.png" />
    <script src="/Ajax/JSONService.ashx?proxy" type="text/javascript"></script>
    <script src="http://connect.facebook.net/it_IT/all.js" type="text/javascript"></script>
    <script src="http://static.ak.fbcdn.net/connect.php/js/FB.Share" type="text/javascript"></script>
    <script type="text/javascript" src="http://platform.twitter.com/widgets.js"></script>
    <uc4:Analytics ID="Analytics1" runat="server" />
</head>
<body>
    <form runat="server" id="teaserForm">
    <uc3:Header ID="Header1" runat="server" CheckCity="false" HideTabs="true" />
    <div id="content" class="teaser">
        <div class="subscribe">
            <h2>
                Mettiaposto è un servizio nuovo, nato dall'esigenza di dare visiblità ai cittadini
                dei problemi della propria città
                <br />
                e per permettere loro di segnalare ciò che non va per aiutare le amministrazioni
                territoriali a focalizzare i veri problemi più vicini ai cittadini che possono essere
                risolti con un pò di semplice manutenzione</h2>
            <br />
            <br />
            <div id="newsletter">
                <strong>Per essere informato quando il servizio sarà aperto al pubblico, inserisci il
                    tuo indirizzo email nell'area di testo sottostante</strong>
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
