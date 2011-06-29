<%@ Page Language="C#" Inherits="OpenSignals.Framework.Core.Base.BasePage" %>

<%@ Register Src="Includes/Analytics.ascx" TagName="Analytics" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Mettiaposto.it - Segnala disservizi, problemi e malfunzionamenti della tua città!
    </title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="description" runat="server" id="metaDescription" content="Segnala disservizi, problemi e malfunzionamenti della tua città" />
    <meta name="keywords" runat="server" id="metaKeywords" content="segnala, disservizi, città, tua città, mia città, milano, comune, problemi, buche, graffiti, spazzatura" />
    <link rel="canonical" href="http://www.mettiaposto.it/" />
    <link rel="icon" href="http://www.mettiaposto.it/favicon.ico" />
    <link rel="shortcut icon" href="http://www.mettiaposto.it/favicon.ico" />
    <link rel="image_src" href="http://www.mettiaposto.it/images/logo.png" />
    <meta property="fb:app_id" content="183751108307062" />
    <meta property="fb:admins" content="1437896726,800734657" />
    <meta property="og:site_name" content="Mettiaposto.it" />
    <meta property="og:type" content="website" />
    <meta property="og:title" content="Mettiaposto.it - Stiamo arrivando!" />
    <meta property="og:description" content="Segnala disservizi, problemi e malfunzionamenti della tua città" />
    <meta property="og:image" content="http://www.mettiaposto.it/images/logo.png" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.12/jquery-ui.min.js"></script>
    <link href="/css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="/css/teaser.css" media="projection,screen" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="http://launchrock.com/api/lr.api.0.2.css" />
    <script type="text/javascript" src="http://launchrock.com/api/lr.api.0.5-min.js"></script>
    <script type="text/javascript" src="/js/launch.js"></script>
    <uc4:Analytics ID="Analytics1" runat="server" />
</head>
<body>
    <div id="logo">
        <img src="/images/teaser/logointro.png" alt="Mettiaposto.it" />
    </div>
    <div id="wrapper">
        <div id="container">
        </div>
        <a href="#" title="Partecipa al sondaggio per valutare l'interesse e le necessità dei cittadini"
            onclick="doPoll(); return false;" class="poll"><span>Partecipa al sondaggio</span></a>
    </div>
</body>
</html>
