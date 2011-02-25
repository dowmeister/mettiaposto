<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Footer.ascx.cs" Inherits="OpenSignals.Frontend.Includes.Footer" %>
</div>
<div id="footer">
    &copy; 2010 <a href="/index.aspx">Home</a> | <a href="/milano/invia.aspx">Segnala</a>
    | <a href="/pages/info.aspx">FAQ</a> | <a href="/pages/privacy.aspx">Privacy</a>
    | <a href="/contact.aspx">Contattaci</a> | <a href="/m/">Mobile</a>
</div>
<div id="feedback">
    <a class="handle" href="http://link-for-non-js-users.html">Content</a>
    <div class="submitForm">
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
