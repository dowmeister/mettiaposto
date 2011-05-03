<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="OpenSignals.Frontend.SignalDetail" %>

<%@ Register Src="Includes/Footer.ascx" TagName="Footer" TagPrefix="uc1" %>
<%@ Register Src="Includes/Head.ascx" TagName="Head" TagPrefix="uc2" %>
<%@ Register Src="Includes/Header.ascx" TagName="Header" TagPrefix="uc3" %>
<%@ Register Src="Includes/Analytics.ascx" TagName="Analytics" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Mettiaposto.it - {0} in {1} a {2}</title>
    <uc2:Head ID="ucHead" runat="server" />
    <meta property="og:title" id="ogTitle" runat="server" content="Mettiaposto.it - {0} in {1} a {2}" />
    <meta property="og:description" runat="server" id="metaOgDescription" content="" />
    <meta property="og:image" runat="server" id="ogImage" content="http://www.mettiaposto.it/images/logo.png" />
    <link rel="stylesheet" type="text/css" href="/js/jquery/plugins/fancybox/jquery.fancybox-1.3.4.css"
        media="screen" />
    <uc4:Analytics ID="Analytics1" runat="server" />
    <os:StaticFileManager ID="staticFileManager" ContextKey="detail" runat="server">
        <os:StaticFile Url="/js/jquery/plugins/ajaxfileupload.js" Type="Javascript" />
        <os:StaticFile Url="/js/mapManager.js" Type="Javascript" />
        <os:StaticFile Url="/js/signal.functions.js" Type="Javascript" />
        <os:StaticFile Url="/js/facebook.js" Type="Javascript" />
        <os:StaticFile Url="/js/comments.functions.js" Type="Javascript" />
        <os:StaticFile Url="/js/jquery/plugins/fancybox/jquery.fancybox-1.3.4.pack.js" Type="Javascript" />
    </os:StaticFileManager>
</head>
<body>
    <div id="fb-root">
    </div>
    <form id="form1" runat="server">
    <uc3:Header ID="Header1" runat="server" SelectedTab="2" />
    <div id="content">
        <div class="twocols">
            <div class="left">
                <h1 runat="server" id="divTitle" class="title">
                </h1>
                <div class="details">
                    <div class="info">
                        Inviato
                        <asp:Label ID="ltTimeFrame" runat="server"></asp:Label>
                        da
                        <asp:Label ID="ltAuthor" runat="server"></asp:Label>
                        nella categoria "<asp:Label ID="ltCategory" runat="server"></asp:Label>"
                        in
                        <asp:Label ID="lblAddress" runat="server"></asp:Label>
                    </div>
                    <div class="message success" style="display: block; margin-top: 10px;" id="divResolved"
                        runat="server" visible="false">
                        Segnalazione risolta</div>
                    <div class="message error" id="divExpired" runat="server" visible="false">
                        Segnalazione scaduta</div>
                    <div class="description">
                        <asp:PlaceHolder ID="plhDescription" runat="server"></asp:PlaceHolder>
                        <div class="photo" id="divPhoto" runat="server" visible="false">
                            <a runat="server" id="lnkPhoto">
                                <asp:Image ID="imgPhoto" runat="server" />
                            </a>
                        </div>
                    </div>
                    <div class="shareBox">
                        <ul>
                            <li><a name="fb_share" type="button">Convidivi su Facebook</a> </li>
                            <li><a href="http://twitter.com/share" class="twitter-share-button" data-count="horizontal"
                                data-via="mettiaposto" data-lang="it">Tweet</a> </li>
                            <li>
                                <fb:like layout="button_count" show_faces="false"></fb:like>
                            </li>
                        </ul>
                        <div class="clear">
                        </div>
                    </div>
                    <div id="commentsBox" class="serviceBox" style="display: block;">
                        <div id="commentsMessages">
                        </div>
                        <div id="comments" class="list">
                        </div>
                        <div class="clear">
                        </div>
                        <div id="commentForm" class="submitForm">
                            <button id="btnFBLogin" class="facebook-button" onclick="fbLogin(); return false;">
                                Login con Facebook</button>
                            <ol>
                                <li class="loginStatus" id="loginStatus" style="display: none;">
                                    <img id="userAvatar" style="vertical-align: middle" />
                                    <span id="userName"></span> (<a href="#" onclick="fbLogout(); return false;">Logout</a>)
                                </li>
                                <li>
                                    <label>
                                        Commento</label>
                                    <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine"></asp:TextBox>
                                </li>
                                <li id="nameContainer">
                                    <label>
                                        Nome</label>
                                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                                    <div class="subfield">
                                        <asp:CheckBox CssClass="checkbox" Checked="true" ID="chkPublicName" Text="Possiamo mostrare il tuo nome nel commento?"
                                            runat="server" /></div>
                                </li>
                                <li id="emailContainer">
                                    <label>
                                        E-mail</label>
                                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                                </li>
                                <li>
                                    <label>
                                        Allega una foto</label>
                                    <asp:FileUpload ID="fuFile" runat="server" />
                                    <div class="legend">
                                        Puoi caricare solo file di tipo IMMAGINE
                                    </div>
                                </li>
                            </ol>
                            <div class="buttons">
                                <input class="success" type="button" value="Commenta" onclick="addComment(); return false;" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="right">
                <div id="tabs" class="tabs">
                    <ul>
                        <li><a href="#map">Mappa</a></li>
                        <li runat="server" id="liMapNearby"><a href="#mapNearby">Dintorni</a></li>
                    </ul>
                    <div class="map" id="map">
                        <div class="mapLoader">
                        </div>
                    </div>
                    <div class="map" runat="server" id="mapNearby">
                        <div class="mapLoader">
                        </div>
                    </div>
                </div>
                <div id="subscribe" class="serviceBox" style="display: block;">
                    <h4>
                        Iscriviti agli aggiornamenti via email per questa segnalazione</h4>
                    <div id="subscribeSignalMessages">
                    </div>
                    <div class="submitForm">
                        <ol>
                            <li>
                                <label>
                                    E-mail</label>
                                <asp:TextBox ID="txtSubscribeEmail" runat="server"></asp:TextBox>
                            </li>
                        </ol>
                        <div class="buttons">
                            <input class="success" type="button" value="Iscriviti" onclick="subscribeSignal(); return false;" />
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
    <script src="http://connect.facebook.net/it_IT/all.js" type="text/javascript"></script>
    <script src="http://static.ak.fbcdn.net/connect.php/js/FB.Share" type="text/javascript"></script>
    <script type="text/javascript" src="http://platform.twitter.com/widgets.js"></script>
    <script src="/Ajax/JSONService.ashx?proxy" type="text/javascript"></script>
    <script type="text/javascript" src="/js/StaticFileHandler.ashx?key=common,detail"></script>
    <script type="text/javascript">
        initDetailPage();
    </script>
</body>
</html>
