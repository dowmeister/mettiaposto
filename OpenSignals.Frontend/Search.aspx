<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="OpenSignals.Frontend.Search" %>

<%@ Register Src="Includes/Footer.ascx" TagName="Footer" TagPrefix="uc1" %>
<%@ Register Src="Includes/Head.ascx" TagName="Head" TagPrefix="uc2" %>
<%@ Register Src="Includes/Header.ascx" TagName="Header" TagPrefix="uc3" %>
<%@ Register Src="Includes/Analytics.ascx" TagName="Analytics" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Mettiaposto.it - Cerca segnalazioni a {0}</title>
    <uc2:Head ID="ucHead" runat="server" />
    <meta property="og:title" id="ogTitle" runat="server" content="Mettiaposto.it - Cerca segnalazioni a {0}" />
    <meta property="og:description" runat="server" id="metaOgDescription" content="Cerca segnalazione di disservizi, problemi e malfunzionamenti a {0} riportati dai cittadini" />
    <meta property="og:image" content="http://www.mettiaposto.it/images/logo.png" />
    <uc4:Analytics ID="Analytics1" runat="server" />
    <os:StaticFileManager ID="staticFileManager" ContextKey="search" runat="server">
        <os:StaticFile Url="/js/mapManager.js" Type="Javascript" />
        <os:StaticFile Url="/js/search.functions.js" Type="Javascript" />
    </os:StaticFileManager>
</head>
<body>
    <form id="form1" runat="server">
    <uc3:Header ID="Header1" runat="server" SelectedTab="2" />
    <div id="content">
        <div class="twocols">
            <div class="left">
                <h1 class="title">
                    Cerca</h1>
                <div id="submitForm" class="submitForm">
                    <ol>
                        <li>
                            <label>
                                Città</label>
                            <asp:Label CssClass="generic" ID="lblCity" runat="server"></asp:Label>
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
                                <asp:ListItem Value="2" Text="Risolte" />
                                <asp:ListItem Value="1" Text="Non Risolte" />
                            </asp:DropDownList>
                        </li>
                    </ol>
                    <div class="buttons">
                        <input class="success" type="button" value="Cerca" onclick="searchSignals(0); return false;" />
                    </div>
                </div>
                <div id="searchMessages">
                </div>
                <div id="searchList" class="list search">
                </div>
            </div>
            <div class="right">
                <div id="tabs" class="tabs">
                    <ul>
                        <li><a href="#map">Mappa</a></li>
                    </ul>
                    <div class="map" id="map">
                        <div class="mapLoader">
                        </div>
                    </div>
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
    <uc1:Footer ID="Footer1" runat="server" />
    </form>
    <script src="http://maps.google.com/maps/api/js?sensor=true&amp;region=it" type="text/javascript"></script>
    <script type="text/javascript" src="/js/StaticFileHandler.ashx?key=common,search"></script>
    <script src="/Ajax/JSONService.ashx?proxy" type="text/javascript"></script>
</body>
</html>
