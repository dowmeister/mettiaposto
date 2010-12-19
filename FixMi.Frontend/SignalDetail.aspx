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
    <script src="http://static.ak.fbcdn.net/connect.php/js/FB.Share" type="text/javascript"></script>
    <script type="text/javascript" src="http://platform.twitter.com/widgets.js"></script>
    <script>
        /*$(document).ready(function () {
            document.write('<script type="text/javascript" src="http://google-maps-utility-library-v3.googlecode.com/svn/tags/markermanager/1.0/src/markermanager_packed.js"><' + '/script>');
        });*/
        document.write('<script type="text/javascript" src="http://google-maps-utility-library-v3.googlecode.com/svn/tags/markermanager/1.0/src/markermanager.js"><' + '/script>');
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
                        da
                        <asp:Label ID="ltAuthor" runat="server"></asp:Label>
                        nella categoria "<asp:Label ID="ltCategory" runat="server"></asp:Label>"
                        <br />
                        Indirizzo:
                        <asp:Label ID="lblAddress" runat="server"></asp:Label>
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
                    <div style="display: block">
                        <div id="comment" class="submitForm serviceBox">
                            <ol>
                                <li>
                                    <label>
                                        Commento</label>
                                    <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine"></asp:TextBox>
                                    * </li>
                                <li>
                                <li>
                                    <label>
                                        Nome</label>
                                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                                    <div class="subfield">
                                        <asp:CheckBox CssClass="checkbox" Checked="true" ID="chkPublicName" Text="Possiamo mostrare il tuo nome nel dettaglio della segnalazione?"
                                            runat="server" /></div>
                                    <div class="legend">
                                        Nome ed indirizzo email non sono obbligatori ma ti consigliamo di inserirli per
                                        maggiore trasparenza</div>
                                </li>
                                <li>
                                    <label>
                                        E-mail</label>
                                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                                    <div class="legend">
                                        Ricevi via email aggiornamenti su questa segnalazione</div>
                                </li>
                            </ol>
                            <div class="buttons">
                                <input class="success" type="button" value="Invia" onclick="saveSignal(); return false;" />
                                <input class="reset" type="reset" value="Annulla" />
                            </div>
                        </div>
                        <div id="subscribe" class="submitForm serviceBox">
                            <ol>
                                <li>
                                    <label>
                                        E-mail</label>
                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                    <div class="legend">
                                        Il tuo indirizzo email non verrà mai mostrato pubblicamente</div>
                                </li>
                            </ol>
                            <div class="buttons">
                                <input class="success" type="button" value="Iscriviti" onclick="saveSignal(); return false;" />
                                <input class="reset" type="reset" value="Annulla" />
                            </div>
                        </div>
                        <div id="share" class="shareBox serviceBox">
                            <ul>
                                <li><a name="fb_share" type="button">Convidivi su Facebook</a> </li>
                                <li><a href="http://twitter.com/share" class="twitter-share-button" data-count="horizontal">
                                    Tweet</a> </li>
                            </ul>
                            <div class="clear">
                            </div>
                        </div>
                        <div id="report" class="submitForm serviceBox">
                        </div>
                    </div>
                </div>
            </div>
            <div class="right">
                <div id="tabs" class="mapTabs">
                    <ul>
                        <li><a href="#map">Mappa</a></li>
                        <li><a href="#nearby">Dintorni</a></li>
                    </ul>
                    <div map="true" id="map" mapDiv="map_canvas">
                        <div id="map_canvas" class="map">
                        </div>
                        <br />
                        <span id="completeAddress" style="display: none;">Indirizzo completo: </span>
                    </div>
                     <div map="true" runat="server" id="nearby" mapDiv="mapNearby">
                        <div id="mapNearby" class="map">
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
