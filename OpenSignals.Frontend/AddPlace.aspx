<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddPlace.aspx.cs" Inherits="OpenSignals.Frontend.AddPlace" %>

<%@ Register Src="Includes/Footer.ascx" TagName="Footer" TagPrefix="uc1" %>
<%@ Register Src="Includes/Head.ascx" TagName="Head" TagPrefix="uc2" %>
<%@ Register Src="Includes/Header.ascx" TagName="Header" TagPrefix="uc3" %>
<%@ Register Src="Includes/Analytics.ascx" TagName="Analytics" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Mettiaposto.it - Invia una segnalazione a {0}</title>
    <uc2:Head ID="ucHead" runat="server" />
    <meta property="og:title" id="ogTitle" runat="server" content="Mettiaposto.it - Invia una segnalazione a {0}" />
    <meta property="og:description" runat="server" id="metaOgDescription" content="Invia una segnalazione di disservizi, problemi e malfunzionamenti a {0}" />
    <meta property="og:image" content="http://www.mettiaposto.it/images/logo.png" />
    <script src="http://maps.google.com/maps/api/js?sensor=true&amp;region=it" type="text/javascript"></script>
    <script src="/js/mapManager.js" type="text/javascript"></script>
    <script src="/Ajax/JSONService.ashx?proxy" type="text/javascript"></script>
    <script src="/js/places.functions.js" type="text/javascript"></script>
    <uc4:Analytics ID="Analytics1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <uc3:Header ID="Header1" CheckCity="false" runat="server" SelectedTab="3" />
    <div id="content">
        <div class="twocols">
            <div class="left">
                <h1 class="title">
                    Aggiungi la tua città</h1>
                <div id="validationError">
                </div>
                <div id="messages">
                </div>
                <div id="submitForm" class="submitForm">
                    <div class="success message" style="display: block">
                        Se la tua città non è presente, puoi aggiungerla tu stesso compilando il form sottostante.
                        Dopo la verifica sarà pubblicata e potrai inviare segnalazioni.
                    </div>
                    <ol>
                        <li>
                            <label>
                                Città</label>
                            <asp:TextBox Enabled="false" ID="txtCity" runat="server"></asp:TextBox>
                            <div class="legend">
                                Sposta il segnalino sulla mappa al centro della tua città e imposta la zoom della mappa in modo che tutto il territorio comunale sia
                                compreso dalla mappa.
                        </div>
                        </li>
                        <li>
                            <label>
                                E-mail</label>
                            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                            <div class="legend">
                               Inserisci la tua email per essere avvertito quando la tua città sarà pubblicata.</div>
                        </li>
                    </ol>
                    <ul>
                        <li>Le informazioni personali vengono utilizzate nel rispetto delle leggi sulla <a
                            href="/pages/privacy.aspx">privacy</a>.</li>
                        <li>Ricorda che Mettiaposto.it ti permette di segnalare problemi fisici del tuo quartiere:
                            se non trovi una categoria adatta probabilmente il tuo problema non è sottomissibile
                            via mettiaposto.it Prova ad usare il sito del Comune di Milano o altri canali</li>
                    </ul>
                    <div class="buttons">
                        <input class="success" type="button" value="Invia" onclick="addPlace(); return false;" />
                    </div>
                </div>
            </div>
            <div class="right">
                <div id="tabs" class="mapTabs">
                    <ul>
                        <li><a href="#map">Mappa</a></li>
                    </ul>
                    <div map="true" id="map" mapdiv="map_canvas">
                        <div class="message ajax" style="display: block;">
                            <img style="margin-top: 250px; margin-bottom: 200px;" alt="Caricamento in corso.."
                                src="/images/ajax-loader.gif" /></div>
                    </div>
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
    <uc1:Footer ID="Footer1" CheckCity="false" runat="server" />
    </form>
</body>
</html>
