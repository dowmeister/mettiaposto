<%@ Control Language="C#" AutoEventWireup="true" Inherits="OpenSignals.Framework.Web.Controls.SignalDetail" %>
<div id="infoWindowContainer">
    <h2 runat="server" id="title">
    </h2>
    <div class="info">
        Inviato
        <asp:Literal ID="timeframe" runat="server"></asp:Literal>
        nella categoria '<asp:Literal ID="category" runat="server"></asp:Literal>', indirizzo:
        <asp:Literal ID="address" runat="server"></asp:Literal>
    </div>
    <div runat="server" id="divImage" class="image" visible="false">
        <asp:Image ID="imgImage" runat="server" />
    </div>
    <a runat="server" id="lnkDetail" runat="server">Visualizza il dettaglio della segnalazione</a>
</div>
