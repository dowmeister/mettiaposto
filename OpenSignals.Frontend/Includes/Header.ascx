<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="OpenSignals.Frontend.Includes.Header" %>
<div id="skyline">
    <div id="header">
        <div id="logo">
            <a href="/index.aspx"></a>
        </div>
        <div id="menu">
            <ul class="cities sf-menu">
                <li class="current"><a href="#a">Sei a... Milano</a>
                    <ul>
                        <li><a href="#b">Bari</a></li>
                    </ul>
                </li>
            </ul>
            <ul class="tabs">
                <li runat="server" id="link1" class="homeOff"><a title="Torna alla homepage" class="small"
                    href="/index.aspx"><span>Home</span></a></li>
                <li runat="server" id="link2" class="tabOff"><a class="tabOff" title="Cerca fra le segnalazioni della tua città"
                    href="/milano/cerca.aspx">Cerca</a></li>
                <li runat="server" id="link3" class="tabOff"><a class="tabOff" title="Hai qualcosa da segnalare? Inserisci i dati richiesti e invia la tua segnalazione"
                    href="/milano/invia.aspx">Segnala</a></li>
                <li runat="server" id="link4" class="tabOff"><a class="tabOff" title="Domande, Risposte e altre utili informazioni"
                    href="/pages/info.aspx">FAQ</a></li>
                <li class="rss"><a class="small rss" href="/rss.ashx"><span>Iscriviti all'RSS delle
                    segnalazioni della tua città</span></a></li>
                <li class="facebook"><a class="small facebook" href="http://www.facebook.com/apps/application.php?id=183751108307062">
                    <span>Mettiaposto su Facebook</span></a></a></li>
                <li class="twitter"><a class="small twitter" href="http://twitter.com/mettiaposto"><span>
                    Mettiaposto su Twitter</span></a></a></li>
            </ul>
        </div>
    </div>
</div>
<div style="clear: both;">
</div>
<div id="wrapper">
