<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Submit.aspx.cs" Inherits="FixMi.Frontend.Submit" %>

<%@ Register Src="Includes/Footer.ascx" TagName="Footer" TagPrefix="uc1" %>
<%@ Register Src="Includes/Head.ascx" TagName="Head" TagPrefix="uc2" %>
<%@ Register Src="Includes/Header.ascx" TagName="Header" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <uc2:Head ID="ucHead" runat="server" />
    <script src="http://maps.google.com/maps/api/js?sensor=true&region=it" type="text/javascript"></script>
    <script src="/js/map.js" type="text/javascript"></script>
    <script src="/js/jquery/plugins/ajaxfileupload.js" type="text/javascript"></script>
    <script src="/Ajax/JSONService.ashx?proxy" type="text/javascript"></script>
    <script src="/js/signal.functions.js" type="text/javascript"></script>
    <script>
        $(document).ready(function () {
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <uc3:Header ID="Header1" runat="server" />
    <div id="content">
        <div class="twocols">
            <div class="left">
                <h1 class="title">
                    Segnala un problema</h1>
                <div id="messages"></div>
                <a name="a"></a>
                <div id="submitForm" class="submitForm">
                    <ol>
                        <li>
                            <label>
                                Indirizzo</label>
                            <asp:TextBox ID="txtAddress" runat="server" CssClass="address" onblur="geolocationByAddress(this.value);"></asp:TextBox>
                            *
                            <div class="legend">
                                Digita un indirizzo, esempio: Via Ripamonti, 100</div>
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
                                Inserisci l'oggetto della segnalazione: sii sintetico e chiaro</div>
                        </li>
                        <li>
                            <label>
                                Descrizione</label>
                            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine"></asp:TextBox>
                            * </li>
                        <li>
                            <label>
                                Allega una foto</label>
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
                                <asp:CheckBox CssClass="checkbox" Checked="true" ID="chkPublicName" Text="Possiamo mostrare il tuo nome nel dettaglio della segnalazione?"
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
                        <li>Le informazioni personali vengono utilizzate nel rispetto delle leggi sulla privacy.</a></li>
                        <li>Per favore sii educato, preciso e vai dritto al punto.</li>
                        <li>Non abusare di questo servizio: l'abuso discredita la validità del servizio per
                            tutti gli utenti!</li>
                        <li>Non scrivere in maiuscolo</li>
                        <li>Ricorda che FixMi ti permette di segnalare problemi fisici del tuo quartiere: se
                            non trovi una categoria adatta probabilmente il tuo problema non è sottomissibile
                            via FixMi. Prova ad usare il sito del Comune di Milano o altri canali</li>
                    </ul>
                    <div class="buttons">
                        <input class="success" type="button" value="Invia" onclick="saveSignal(); return false;" />
                        <input class="reset" type="reset" value="Annulla" />
                    </div>
                </div>
            </div>
            <div class="right">
                <div id="tabs" class="mapTabs">
                    <ul>
                        <li><a href="#map_canvas">Mappa</a></li>
                    </ul>
                    <div map="true" id="mapContainer">
                        <div map="true" id="map_canvas" class="map">
                        </div>
                        <br />
                        <span id="completeAddress" style="display: none;">Indirizzo completo: </span>
                    </div>
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
    <div id="hshadow">
    </div>
    <uc1:Footer ID="Footer1" runat="server" />
    </form>
</body>
</html>
