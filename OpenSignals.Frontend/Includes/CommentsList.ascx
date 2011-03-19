<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CommentsList.ascx.cs"
    Inherits="OpenSignals.Frontend.Includes.CommentsList" %>
<asp:Repeater ID="rptList" runat="server" OnItemDataBound="rptList_ItemDataBound">
    <ItemTemplate>
        <div class="item">
            <div class="avatar">
                <asp:Image ID="avatar" ImageUrl="/images/avatar.png" runat="server" /></div>
            <div class="comment" runat="server" id="comment">
            </div>
            <div class="legend">
                inviato da
                <asp:Label ID="author" runat="server"></asp:Label>,
                <asp:Label ID="timeframe" runat="server"></asp:Label>
            </div>
            <div class="photo" id="divPhoto" runat="server" visible="false">
                <a runat="server" id="lnkPhoto" runat="server">
                    <asp:Image ID="imgPhoto" runat="server" />
                </a>
            </div>
        </div>
    </ItemTemplate>
    <AlternatingItemTemplate>
        <div class="alt-item">
            <div class="avatar">
                <asp:Image ID="avatar" ImageUrl="/images/avatar.png" runat="server" /></div>
            <div class="comment" runat="server" id="comment">
            </div>
            <div class="legend">
                inviato da
                <asp:Label ID="author" runat="server"></asp:Label>,
                <asp:Label ID="timeframe" runat="server"></asp:Label>
            </div>
            <div class="photo" id="divPhoto" runat="server" visible="false">
                <a runat="server" id="lnkPhoto" runat="server">
                    <asp:Image ID="imgPhoto" runat="server" />
                </a>
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
