<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="FixMi.Frontend.Search" %>

<%@ Register Src="Includes/Footer.ascx" TagName="Footer" TagPrefix="uc1" %>
<%@ Register Src="Includes/Head.ascx" TagName="Head" TagPrefix="uc2" %>
<%@ Register Src="Includes/Header.ascx" TagName="Header" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <uc2:Head ID="ucHead" runat="server" />
    <script src="http://maps.google.com/maps/api/js?sensor=true&amp;region=it" type="text/javascript"></script>
    <script src="/js/map.js" type="text/javascript"></script>
    <script src="/Ajax/JSONService.ashx?proxy" type="text/javascript"></script>
    <script src="/js/search.functions.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <uc3:Header ID="Header1" runat="server" />
    <div id="content">
        <div class="twocols">
            <div class="left">
                <h1 class="title">
                    Cerca</h1>
                <div class="message error" id="validationError">
                </div>
                <div id="messages">
                </div>
                <div id="submitForm" class="submitForm">
                    <ol>
                        <li>
                            <label>
                                Città</label>
                            <asp:Label ID="lblCity" runat="server"></asp:Label>
                        </li>
                        <li>
                            <label>
                                Indirizzo</label>
                            <asp:TextBox ID="txtAddress" runat="server" CssClass="address"></asp:TextBox>
                        </li>
                        <li>
                            <label>
                                Categoria</label>
                            <asp:DropDownList DataTextField="Name" DataValueField="CategoryID" AppendDataBoundItems="true"
                                ID="ddlCategories" runat="server">
                                <asp:ListItem Value="-1" Text="-- Scegli una categoria --" />
                            </asp:DropDownList>
                        </li>
                        <li>
                            <label>
                                C.A.P.</label>
                            <asp:TextBox ID="txtZip" runat="server" CssClass="address"></asp:TextBox>
                        </li>
                        <li>
                            <label>
                                Stato</label>
                            <asp:DropDownList DataTextField="Name" DataValueField="CategoryID" AppendDataBoundItems="true"
                                ID="ddlStatus" runat="server">
                                <asp:ListItem Value="-1" Text="Risolte e non Risolte" />
                                <asp:ListItem Value="1" Text="Risolte" />
                                <asp:ListItem Value="1" Text="Non Risolte" />
                            </asp:DropDownList>
                        </li>
                    </ol>
                    <div class="buttons">
                        <input class="success" type="button" value="Cerca" onclick="searchSignals(); return false;" />
                    </div>
                </div>
                <div id="list" class="list">
                    
                </div>
            </div>
            <div class="right">
                <div id="tabs" class="mapTabs">
                    <ul>
                        <li><a href="#map">Mappa</a></li>
                    </ul>
                    <div map="true" id="map" mapdiv="map_canvas">
                        <div map="true" id="map_canvas" class="map">
                        </div>
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
