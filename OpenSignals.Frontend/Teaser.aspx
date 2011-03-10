<%@ Page Language="C#" %>
<%@ Register Src="Includes/Footer.ascx" TagName="Footer" TagPrefix="uc1" %>
<%@ Register Src="Includes/Head.ascx" TagName="Head" TagPrefix="uc2" %>
<%@ Register Src="Includes/Header.ascx" TagName="Header" TagPrefix="uc3" %>
<%@ Register Src="Includes/Analytics.ascx" TagName="Analytics" TagPrefix="uc4" %>
<%@ Register Src="Includes/StaticFileManager.ascx" TagName="StaticFileManager" TagPrefix="uc5" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Mettiaposto.it - Segnala disservizi, problemi e malfunzionamenti della tua città!</title>
    <uc2:Head ID="ucHead" runat="server" />
    <meta property="og:title" content="Mettiaposto.it" />
    <meta property="og:description" content="Segnala disservizi, problemi e malfunzionamenti della tua città" />
    <meta property="og:image" content="http://www.mettiaposto.it/images/logo.png" />
    <uc4:Analytics ID="Analytics1" runat="server" />
</head>
<body>
    <uc3:Header ID="Header1" runat="server" CheckCity="false" HideTabs="true" />
    <div id="content" class="teaser">
        
    </div>
</body>
</html>
