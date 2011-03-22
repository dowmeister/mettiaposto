<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="OpenSignals.Frontend.Admin.Includes.Header" %>
<div id="wrapper">
    <div id="header">
        <ul id="menu" class="sf-menu">
            <li><a href="#">Segnalazioni</a>
                <ul>
                    <li><a href="#">Commenti</a></li></ul>
            </li>
            <li><a href="#">Città</a>
                <ul>
                    <li><a href="#">Aggiungi Città</a></li></ul>
            </li>
            <li><a href="#">Categorie</a>
                <ul>
                    <li><a href="#">Aggiungi Categoria</a></li></ul>
            </li>
            <li><a href="#">Utenti</a>
                <ul>
                    <li><a href="#">Aggiunti Utente</a></li>
                    <li><a href="#">Ruoli</a></li>
                    <li><a href="#">Aggiungi Ruolo</a></li>
                </ul>
            </li>
            <li><a href="#">Configurazione</a></li>
        </ul>
        <div id="loginInfo">
            Utente: Admin |
            <asp:LinkButton ID="lnkLogout" runat="server" onclick="lnkLogout_Click">Logout</asp:LinkButton>
        </div>
    </div>
</div>

