<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CommentsList.ascx.cs"
    Inherits="FixMi.Frontend.Includes.CommentsList" %>
<asp:Repeater ID="rptList" runat="server" 
    onitemdatabound="rptList_ItemDataBound">
    <ItemTemplate>
        <div class="item">
            <div class="comment" runat="server" id="comment">
            </div>
            <div class="legend">
                <asp:Label ID="timeframe" runat="server"></asp:Label>
            </div>
        </div>
    </ItemTemplate>
    <AlternatingItemTemplate>
        <div class="item">
            <div class="comment" runat="server" id="comment">
            </div>
            <div class="legend">
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
