<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Footer.ascx.cs" Inherits="OpenSignals.Frontend.Includes.Footer" %>
</div>
<div id="footer">
    &copy; 2011 <a id="linkLogo" runat="server" href="/index.aspx">Home</a> | <a id="linkSignal"
        runat="server" href="/milano/invia.aspx">Segnala</a> | <a href="/pages/info.aspx">FAQ</a>
    | <a href="/pages/privacy.aspx">Privacy</a> | <a href="/m/">Mobile</a> | <a id="linkRss"
        runat="server">Feed</a>
</div>
<div id="feedback">
    <a class="handle" href="javascript:;">Feedback</a>
    <div id="feedbackSubmit" class="submitForm" style="display: block">
        <ol>
            <li>
                <label>
                    Nome</label>
                <asp:TextBox ID="txtFeedbackName" runat="server"></asp:TextBox></li>
            <li>
                <label>
                    Email</label>
                <asp:TextBox ID="txtFeedbackEmail" runat="server"></asp:TextBox></li>
            <li>
                <label>
                    Commento</label>
                <asp:TextBox ID="txtFeedbackComment" TextMode="MultiLine" runat="server"></asp:TextBox></li>
        </ol>
        <div class="buttons">
            <input class="success" type="button" value="Invia" onclick="sendFeedback('<%= this.ClientID %>'); return false;" />
        </div>
    </div>
    <div id="feedbackMessage">
    </div>
</div>
<div id="notExistingCity" style="display: none;">
    La città richiesta (<span id="notExistingCityLabel"></span>) non esiste nel database
    di Mettiaposto.
    <br />
    Puoi aggiungerla tu stesso, clicca sul bottone 'Aggiungila' per proseguire.
    <br />
    Una volta inserita, verrà controllata e sottoposta ad una veloce approvazione per
    verificare i dati e la posizione che hai inserito e verrai avvertito quando la tua richiesta
    verrà approvata.
</div>
<div id="alreadyRequestedCity" style="display: none;">
    La città richiesta (<span id="alreadyRequestedCityLabel"></span>) è stata già richiesta da un altro utente
    e verrà attivata quanto prima per permettere a tutti di inviare le segnalazioni.
    <br />
    <br />
    Stay tuned :)
</div>
<div id="alertDialog" style="display:none;">
    <div id="alertMessage"></div>
</div>
