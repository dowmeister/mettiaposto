<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Footer.ascx.cs" Inherits="OpenSignals.Frontend.Includes.Footer" %>
</div>
<div id="footer">
    &copy; 2011 <a id="linkLogo" runat="server" href="/index.aspx">Home</a> | <a id="linkSignal"
        runat="server" href="/milano/invia.aspx">Segnala</a> | <a href="/pages/info.aspx">FAQ</a>
    | <a href="/pages/privacy.aspx">Privacy</a> | <a href="/m/">Mobile</a> | <a id="linkRss"
        runat="server">Feed</a>
</div>
<div id="feedback">
    <a class="handle" href="#">Feedback</a>
    <div class="submitForm" style="display: block">
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
            <input class="success" type="button" value="Invia" onclick="sendFeedback(); return false;" />
        </div>
    </div>
</div>
