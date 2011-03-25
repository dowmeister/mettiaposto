<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="OpenSignals.Frontend.Admin.Includes.Header" %>
<div id="wrapper">
    <div id="header">
        <ul id="menu" class="sf-menu">
            <li><a href="/Admin/Signals/List.aspx">Segnalazioni</a>
                <ul>
                    <li><a href="/Admin/Comments/List.aspx">Commenti</a></li></ul>
            </li>
            <li><a href="/Admin/Places/List.aspx">Città</a>
                <ul>
                    <li><a href="/Admin/Places/Edit.aspx">Aggiungi Città</a></li></ul>
            </li>
            <li><a href="/Admin/Categories/List.aspx">Categorie</a>
                <ul>
                    <li><a href="/Admin/Categories/Edit.aspx">Aggiungi Categoria</a></li></ul>
            </li>
            <li><a href="/Admin/Users/List.aspx">Utenti</a>
                <ul>
                    <li><a href="/Admin/Users/Edit.aspx">Aggiunti Utente</a></li>
                    <li><a href="/Admin/Users/RolesList.aspx">Ruoli</a></li>
                    <li><a href="/Admin/Users/EditRole.aspx">Aggiungi Ruolo</a></li>
                </ul>
            </li>
            <li><a href="/Admin/Panel/Configuration.aspx">Configurazione</a></li>
        </ul>
        <div id="loginInfo">
            Utente: Admin |
            <asp:LinkButton ID="lnkLogout" runat="server" onclick="lnkLogout_Click">Logout</asp:LinkButton>
        </div>
    </div>
</div>

