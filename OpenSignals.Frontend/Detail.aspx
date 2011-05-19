<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="OpenSignals.Frontend.SignalDetail" %>

<%@ Register Src="Includes/Footer.ascx" TagName="Footer" TagPrefix="uc1" %>
<%@ Register Src="Includes/Head.ascx" TagName="Head" TagPrefix="uc2" %>
<%@ Register Src="Includes/Header.ascx" TagName="Header" TagPrefix="uc3" %>
<%@ Register Src="Includes/Analytics.ascx" TagName="Analytics" TagPrefix="uc4" %>
<%@ Import Namespace="OpenSignals.Framework.Core" %>
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
                        nella categoria "<asp:Label ID="ltCategory" runat="server"></asp:Label>" in
                        <asp:Label ID="lblAddress" runat="server"></asp:Label>
                    </div>
                    <div class="description">
                        <asp:PlaceHolder ID="plhDescription" runat="server"></asp:PlaceHolder>
                        <div class="photo" id="divPhoto" runat="server" visible="false">
                            <a runat="server" id="lnkPhoto">
                                <asp:Image ID="imgPhoto" runat="server" />
                            </a>
                        </div>
                    </div>
                    <div id="divStatusReopened" visible="false" runat="server" class="statusBox status-notResolved">
                        Segnalazione riaperta il {0}, motivo: {2}
                        <br />
                        <a href="#" onclick="changeStatus(2); return false;">Chiudi la segnalazione</a>
                    </div>
                    <div id="divStatusNotResolved" visible="false" runat="server" class="statusBox status-notResolved">
                        Segnalazione non risolta <a href="#" onclick="changeStatus(2); return false;">Chiudi
                            la segnalazione</a>
                    </div>
                    <div id="divStatusResolved" visible="false" runat="server" class="statusBox status-resolved">
                        Segnalazione risolta il {0}, segnalato da {1} con messaggio: {2}
                        <div>
                            <a href="#" onclick="changeStatus(4); return false;">Riapri la segnalazione</a>
                        </div>
                        <div class="clear">
                        </div>
                    </div>
                    <div id="divStatusExpired" visible="false" runat="server" class="statusBox status-expired">
                        Questa segnalazione è scaduta (2 mesi senza risposta)
                    </div>
                    <!-- <div class="shareBox">
                        <ul>                            
                            <li><a href="http://twitter.com/share" class="twitter-share-button" data-count="horizontal"
                                data-via="mettiaposto" data-lang="it">Tweet</a> </li>
                            <li><a name="fb_share" type="button">Convidivi</a> </li>
                            <li>
                                <fb:like layout="button_count" font="lucida grande" show_faces="false"></fb:like>
                            </li>
                            <li>
                                <fb:send font="lucida grande"></fb:send>
                            </li>
                        </ul>
                        <div class="clear">
                        </div>
                    </div> -->
                    <div class="shareBox">
                        <ul>
                            <li><a onclick="sharePopup(this); return false;" href="http://twitter.com/intent/tweet?text=<%= this.Title %>&url=<%= ((RewriteContext)GetFromContext("REWRITECONTEXT")).RewritedUrl %>&via=mettiaposto"
                                title="Condividi su Twitter">
                                <img src="/images/social_twitter.png" alt="Twitter" /></a> </li>
                            <li><a onclick="sharePopup(this); return false;" href="http://www.facebook.com/share.php?u=<%= ((RewriteContext)GetFromContext("REWRITECONTEXT")).RewritedUrl %>&t=<%= this.Title %>"
                                title="Condividi su Facebook">
                                <img src="/images/social_facebook.png" alt="Facebook" /></a> </li>
                            <li><a href="javascript:;" onclick="openSubscribeDialog(); return false;" title="Tienimi aggiornato!">
                                <img src="/images/social_update.png" alt="Aggiornami"></a> </li>
							<li><a href="javascript:;" onclick="openSubscribeDialog(); return false;" title="Riporta un abuso!">
                                <img src="/images/social_report.png" alt="Riporta un abuso"></a> </li>
                        </ul>
                        <div class="clear">
                        </div>
                    </div>
                    <div class="line">
                    </div>
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
                                <span id="userName"></span>(<a href="#" onclick="fbLogout(); return false;">Logout</a>)
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
                                    <asp:CheckBox CssClass="checkbox" Checked="true" ID="chkPublicName" Text="Vuoi mostrare il tuo nome nel commento?"
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
                    <div style="display: none" class="map" runat="server" id="mapNearby">
                        <div class="mapLoader">
                        </div>
                    </div>
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
    <div id="subscribeDialog" style="display: none">
        <div id="subscribeSignalMessages">
        </div>
        <div id="subscribeSignalForm" class="submitForm">
            <ol>
                <li>
                    <label>
                        E-mail</label>
                    <asp:TextBox ID="txtSubscribeEmail" runat="server"></asp:TextBox>
                </li>
            </ol>
        </div>
    </div>
    <div id="changeStatusDialog" style="display: none">
        <div class="submitForm">
            <ol>
                <li>
                    <label>
                        Nuovo stato</label>
                    <span id="newStatus"></span></li>
                <li>
                    <label>
                        Messaggio</label>
                    <asp:TextBox runat="server" ID="txtChangeStatusDescription" TextMode="MultiLine"></asp:TextBox>
                    <div class="legend">
                        Se si sta riaprendo una segnalazione, inserire un messaggio che spieghi perchè la segnalazione viene riaperta (es: il problema segnalato non è stato risolto correttamente); se si sta
                        chiudendo una segnalazione, è possibile indicare in che modo il problema è stato risolto (es.: la buca è stata chiusa con nuovo asfalto)
                    </div>
                </li>
            </ol>
        </div>
    </div>
    <uc1:Footer ID="Footer1" runat="server" />
    </form>
    <script src="http://maps.google.com/maps/api/js?sensor=true&amp;region=it" type="text/javascript"></script>
    <script src="http://connect.facebook.net/it_IT/all.js" type="text/javascript"></script>
    <!--<script src="http://static.ak.fbcdn.net/connect.php/js/FB.Share" type="text/javascript"></script>
    <script type="text/javascript" src="http://platform.twitter.com/widgets.js"></script>-->
    <script src="/Ajax/JSONService.ashx?proxy" type="text/javascript"></script>
    <script type="text/javascript" src="/js/StaticFileHandler.ashx?key=common,detail"></script>
    <script type="text/javascript">
        initDetailPage();
    </script>
</body>
</html>
