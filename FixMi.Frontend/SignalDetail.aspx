<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignalDetail.aspx.cs" Inherits="FixMi.Frontend.SignalDetail" %>

<%@ Register Src="Includes/Footer.ascx" TagName="Footer" TagPrefix="uc1" %>
<%@ Register Src="Includes/Head.ascx" TagName="Head" TagPrefix="uc2" %>
<%@ Register Src="Includes/Header.ascx" TagName="Header" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <uc2:Head ID="ucHead" runat="server" />
    <script src="http://maps.google.com/maps/api/js?sensor=true&region=it" type="text/javascript"></script>
    <script src="/js/map.js" type="text/javascript"></script>
    <script src="/js/jquery/plugins/ajaxfileupload.js" type="text/javascript"></script>
    <script src="/Ajax/JSONService.ashx?proxy" type="text/javascript"></script>
    <script src="/js/signal.functions.js" type="text/javascript"></script>
    <script>
        $(document).ready(function () {
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <uc3:Header ID="Header1" runat="server" />
    <div id="content">
        <div class="twocols">
            <div class="left">
                <h1 runat="server" id="divTitle" class="title">
                </h1>
                <div class="details">
                    <div class="message success" id="divResolved" runat="server" visible="false">
                        Segnalazione risolta</div>
                    <div class="message error" id="divExpired" runat="server" visible="false">
                        Segnalazione scaduta</div>
                    <div class="info">
                        Inviato
                        <asp:Label ID="ltTimeFrame" runat="server"></asp:Label>
                        fa da
                        <asp:Label ID="ltAuthor" runat="server"></asp:Label>
                        nella categoria "<asp:Label ID="ltCategory" runat="server"></asp:Label>"
                    </div>
                    <div class="description" id="divDescription" runat="server">
                    </div>
                    <div class="phto" id="divPhoto" runat="server" visible="false">
                        <asp:Image ID="imgPhoto" runat="server" />
                    </div>
                    <div class="tools">
                        <ul>
                            <li><a class="serviceLink" href="#comment" class="button">Commenta</a></li>
                            <li><a class="serviceLink" href="#subscribe" class="button">Tienimi informato</a></li>
                            <li><a class="serviceLink" href="#share" class="button">Condividi</a></li>
                            <li><a class="serviceLink" href="#report" class="button">Segnala</a></li>
                        </ul>
                        <div class="clear">
                        </div>
                    </div>
                    <div>
                        <div id="comment" class="submitForm serviceBox">
                        </div>
                        <div id="subscribe" class="serviceBox">
                        </div>
                        <div id="share" class="serviceBox">
                        </div>
                        <div id="report" class="submitForm serviceBox">
                        </div>
                    </div>
                </div>
            </div>
            <div class="right">
                <div id="tabs" class="mapTabs">
                    <ul>
                        <li><a href="#map_canvas">Mappa</a></li>
                    </ul>
                    <div map="true" id="mapContainer">
                        <div map="true" id="map_canvas" class="map">
                        </div>
                        <br />
                        <span id="completeAddress" style="display: none;">Indirizzo completo: </span>
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
