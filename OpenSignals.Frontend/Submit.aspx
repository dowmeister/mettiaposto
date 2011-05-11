<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Submit.aspx.cs" Inherits="OpenSignals.Frontend.Submit" %>

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
    <uc4:Analytics ID="Analytics1" runat="server" />
    <os:StaticFileManager ID="staticFileManager" ContextKey="submit" runat="server">
        <os:StaticFile Url="/js/mapManager.js" Type="Javascript" />
        <os:StaticFile Url="/js/signal.functions.js" Type="Javascript" />
        <os:StaticFile Url="/js/jquery/plugins/ajaxfileupload.js" Type="Javascript" />
    </os:StaticFileManager>
</head>
<body>
    <form id="form1" runat="server">
    <uc3:Header ID="Header1" runat="server" SelectedTab="3" />
    <div id="content">
        <div class="twocols">
            <div class="left">
                <h1 class="title">
                    Segnala un problema</h1>
                <div id="submitForm" class="submitForm">
                    <div class="success message" style="display: block">
                        Se hai qualche dubbio, prima di inviare la tua segnalazione leggi le <a href="/pages/info.aspx">
                            FAQ</a>.
                    </div>
                    <ol>
                        <li>
                            <label>
                                Città</label>
                            <asp:Label CssClass="generic" ID="ltCity" runat="server"></asp:Label>
                        </li>
                        <li>
                            <label>
                                Indirizzo</label>
                            <asp:TextBox ID="txtAddress" runat="server" CssClass="address" onblur="geolocateByAddress();"></asp:TextBox>
                            *
                            <div class="legend">
                                Puoi spostare il segnalino di pericolo che vedi sulla mappa per migliorare la precisione della segnalazione.<br />
                                L'indirizzo si aggiornerà automaticamente.</div>
                        </li>
                        <li>
                            <label>
                                Categoria</label>
                            <asp:DropDownList DataTextField="Name" DataValueField="CategoryID" AppendDataBoundItems="true"
                                ID="ddlCategories" runat="server">
                                <asp:ListItem Value="-1" Text="-- Scegli una categoria --" />
                            </asp:DropDownList>
                            * </li>
                        <li>
                            <label>
                                Oggetto</label>
                            <asp:TextBox ID="txtSubject" runat="server"></asp:TextBox>
                            *
                            <div class="legend">
                                Inserisci un titolo sintetico e chiaro</div>
                        </li>
                        <li>
                            <label>
                                Descrizione</label>
                            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine"></asp:TextBox>
                            * </li>
                        <li>
                            <label>
                                Criticità</label>
                            <div id="criticalSlider" style="background: #256488; display:inline-block; width:200px"></div> <span style="margin-left:10px;" id="criticalLevel">Bassa</span>
                        </li>
                        <li>
                            <label>
                                Carica una foto</label>
                            <asp:FileUpload ID="fuFile" runat="server" />
                            <div class="legend">
                                Puoi caricare solo file di tipo IMMAGINE
                            </div>
                        </li>
                        <li>
                            <label>
                                Nome</label>
                            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                            <div class="subfield">
                                <asp:CheckBox CssClass="checkbox" Checked="true" ID="chkPublicName" Text="Mostra il mio nome nella segnalazione"
                                    runat="server" /></div>
                            <div class="legend">
                                Nome ed indirizzo email non sono obbligatori ma ti consigliamo di inserirli per
                                maggiore trasparenza</div>
                        </li>
                        <li>
                            <label>
                                E-mail</label>
                            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                            <div class="legend">
                                Il tuo indirizzo email non verrà mai mostrato pubblicamente</div>
                        </li>
                    </ol>
                    <ul>
                        <li>Le informazioni personali vengono utilizzate nel rispetto delle leggi sulla <a
                            href="/pages/privacy.aspx">privacy</a>.</li>
                        <li>Per favore sii educato, preciso e vai dritto al punto.</li>
                        <li>Non abusare di questo servizio: l'abuso discredita la validità del servizio per
                            tutti gli utenti!</li>
                        <li>Non scrivere in maiuscolo</li>
                        <li>Ricorda che Mettiaposto.it ti permette di segnalare problemi fisici del tuo quartiere:
                            se non trovi una categoria adatta probabilmente il tuo problema non è sottomissibile
                            via mettiaposto.it Prova ad usare il sito del Comune di Milano o altri canali</li>
                    </ul>
                    <div class="buttons">
                        <input class="success" type="button" value="Invia" onclick="saveSignal(); return false;" />
                        <%--<input class="reset" type="reset" value="Annulla" />--%>
                    </div>
                </div>
                <div id="messages">
                </div>
            </div>
            <div class="right">
                <div id="tabs" class="tabs">
                    <ul>
                        <li><a href="#map">Mappa</a></li>
                    </ul>
                    <div class="map" id="map">
                        <div class="mapLoader">
                        </div>
                    </div>
                </div>
                <span id="completeAddress" style="display: none;">Indirizzo completo: </span>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
    <uc1:Footer ID="Footer1" runat="server" />
    </form>
    <script src="http://maps.google.com/maps/api/js?sensor=true&amp;region=it" type="text/javascript"></script>
    <script type="text/javascript" src="/js/StaticFileHandler.ashx?key=common,submit"></script>
    <script src="/Ajax/JSONService.ashx?proxy" type="text/javascript"></script>
    <script type="text/javascript">
        initSubmitPage();
    </script>
</body>
</html>
