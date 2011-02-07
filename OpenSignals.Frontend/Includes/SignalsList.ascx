<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SignalsList.ascx.cs"
    Inherits="OpenSignals.Frontend.Includes.SignalsList" %>
<asp:Repeater ID="rptList" runat="server" OnItemDataBound="rptList_ItemDataBound">
    <ItemTemplate>
        <div class="item">
            <div class="status">
                <asp:Image ID="status" runat="server" /></div>
            <div class="title">
                <a runat="server" id="title"></a>
            </div>
            <div class="legend">
                in
                <asp:Label ID="category" runat="server"></asp:Label>, inviato
                <asp:Label ID="timeframe" runat="server"></asp:Label>
            </div>
        </div>
    </ItemTemplate>
    <AlternatingItemTemplate>
        <div class="alt-item">
             <div class="status">
                <asp:Image ID="status" runat="server" /></div>
            <div class="title">
                <a runat="server" id="title"></a>
            </div>
           
            <div class="legend">
                in
                <asp:Label ID="category" runat="server"></asp:Label>, inviato
                <asp:Label ID="timeframe" runat="server"></asp:Label>
            </div>
        </div>
    </AlternatingItemTemplate>
    <SeparatorTemplate>
        <div class="clear">
        </div>
    </SeparatorTemplate>
</asp:Repeater>
<div class="pagination" runat="server" id="pagination">
</div>
<div class="clear">
</div>
