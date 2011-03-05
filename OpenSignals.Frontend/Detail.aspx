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
    <meta property="fb:admins" content="1437896726" />
    <script src="http://maps.google.com/maps/api/js?sensor=true&amp;region=it" type="text/javascript"></script>
    <script src="/js/mapManager.js" type="text/javascript"></script>
    <script src="/js/jquery/plugins/ajaxfileupload.js" type="text/javascript"></script>
    <script src="/Ajax/JSONService.ashx?proxy" type="text/javascript"></script>
    <script src="/js/signal.functions.js" type="text/javascript"></script>
    <script src="http://connect.facebook.net/it_IT/all.js" type="text/javascript"></script>
    <script src="http://static.ak.fbcdn.net/connect.php/js/FB.Share" type="text/javascript"></script>
    <script type="text/javascript" src="http://platform.twitter.com/widgets.js"></script>
    <link rel="stylesheet" type="text/css" href="/js/jquery/plugins/fancybox/jquery.fancybox-1.3.4.css"
        media="screen" />
    <script type="text/javascript" src="/js/jquery/plugins/fancybox/jquery.fancybox-1.3.4.pack.js"></script>
    <uc4:Analytics ID="Analytics1" runat="server" />
    <script type="text/javascript">
        $(document).ready(function () {
            initDetailPage();
        });
    </script>
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
                        <br />
                        Indirizzo:
                        <asp:Label ID="lblAddress" runat="server"></asp:Label>
                    </div>
                    <div class="message success" style="display: block; margin-top: 10px;" id="divResolved"
                        runat="server" visible="false">
                        Segnalazione risolta</div>
                    <div class="message error" id="divExpired" runat="server" visible="false">
                        Segnalazione scaduta</div>
                    <div class="description" id="divDescription" runat="server">
                    </div>
                    <div class="photo" id="divPhoto" runat="server" visible="false">
                        <a runat="server" id="lnkPhoto">
                            <asp:Image ID="imgPhoto" runat="server" />
                        </a>
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
                    <!--<div class="tools">
                        <ul>
                            <li><a class="serviceLink button" id="commentsLink" href="#commentsBox">Commenti</a></li>
                            <li><a class="serviceLink button" href="#share">Condividi</a></li>
                            <li><a class="serviceLink button" href="#subscribe">Tienimi informato</a></li>
                            <%-- <li><a class="serviceLink button" href="#report">Segnala</a></li> --%>
                        </ul>
                        <div class="clear">
                        </div>
                    </div>-->
                    <div style="display: block">
                        <div id="commentsBox" class="serviceBox" style="display: block;">
                            <%--<div id="commentsMessages">
                            </div>
                            <div id="comments" class="list">
                            </div>-->
                            <fb:comments href="" num_posts="5" width="435"></fb:comments> --%>
                            <div id="disqus_thread">
                            </div>
                            <script type="text/javascript">
                                var disqus_developer = 1;
                                var disqus_shortname = 'mettiapostoit';
                                var disqus_identifier = 'signal_detail_' + currentMarker.id;
                                var disqus_url = '<%= GetRewriteContext().RewritedUrl %>';
                                (function () {
                                    var dsq = document.createElement('script'); dsq.type = 'text/javascript'; dsq.async = true;
                                    dsq.src = 'http://' + disqus_shortname + '.disqus.com/embed.js';
                                    (document.getElementsByTagName('head')[0] || document.getElementsByTagName('body')[0]).appendChild(dsq);
                                })();
                            </script>
                            <ul>
                                <li>Le informazioni personali vengono utilizzate nel rispetto delle leggi sulla <a
                                    href="/pages/privacy.aspx">privacy</a>.</li>
                                <li>Per favore sii educato.</li>
                                <li>Non abusare di questo servizio: l'abuso discredita la validità del servizio per
                                    tutti gli utenti!</li>
                                <li>Non scrivere in maiuscolo</li>
                            </ul>
                            <%-- 
                                                        <div class="clear">
                            </div>
                            <div id="submitCommentMessage">
                            </div>
                            <div id="commentForm" class="submitForm">
                                <button id="btnFBLogin" class="facebook-button" onclick="fbLogin(); return false;">
                                    Accedi con Facebook</button>
                                <ol>
                                    <li>
                                        <label>
                                            Aggiorna stato</label>
                                        <asp:DropDownList ID="ddlStatus" runat="server">
                                            <asp:ListItem Text="Segnalazione non risolta" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Segnalazione risolta" Value="2" />
                                        </asp:DropDownList>
                                        <div class="legend">
                                            Se la segnalazione è stata risolta, seleziona dalla tendina "Segnalazione Risolta"
                                            e inserisci un commento.
                                        </div>
                                    </li>
                                    <li>
                                        <label>
                                            Commento</label>
                                        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine"></asp:TextBox>
                                    </li>
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
                                            Il tuo indirizzo email non verrà mai mostrato pubblicamente
                                        </div>
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
                                    <input class="success" type="button" value="Invia" onclick="addComment(); return false;" />
                                    <input class="reset" type="reset" value="Annulla" />
                                </div>
                            </div>--%>
                        </div>
                        <div id="subscribe" class="serviceBox">
                            <div id="subscribeSignalMessages">
                            </div>
                            <div class="submitForm">
                                <ol>
                                    <li>
                                        <label>
                                            E-mail</label>
                                        <asp:TextBox ID="txtSubscribeEmail" runat="server"></asp:TextBox>
                                        <div class="legend">
                                            Ricevi via email aggiornamenti su questa segnalazione</div>
                                    </li>
                                </ol>
                                <div class="buttons">
                                    <input class="success" type="button" value="Iscriviti" onclick="subscribeSignal(); return false;" />
                                    <!--<input class="reset" type="reset" value="Annulla" />-->
                                </div>
                            </div>
                        </div>
                        <%--<div id="report" class="serviceBox">
                            <div class="submitForm">
                                <ol>
                                    <li>
                                        <label>
                                            E-mail</label>
                                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                    </li>
                                </ol>
                                <div class="buttons">
                                    <input class="success" type="button" value="Iscriviti" onclick="saveSignal(); return false;" />
                                    <input class="reset" type="reset" value="Annulla" />
                                </div>
                            </div>
                        </div> --%>
                    </div>
                </div>
            </div>
            <div class="right">
                <div id="tabs" class="mapTabs">
                    <ul>
                        <li><a href="#map">Mappa</a></li>
                        <li><a href="#mapNearby">Dintorni</a></li>
                    </ul>
                    <div map="true" class="map" id="map" mapdiv="map">
                    </div>
                    <div map="true" class="map" runat="server" id="mapNearby" mapdiv="mapNearby">
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
    <style>
        .fbFeedbackContent .postToProfile
        {
            float: left;
        }
    </style>
</body>
</html>
