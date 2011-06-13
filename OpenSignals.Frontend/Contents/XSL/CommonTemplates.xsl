<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
  <xsl:output method="html" indent="yes"/>
  <xsl:template name="HtmlStyles">
    <head>
      <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
      <title>Mettiaposto.it - Comunicazione di Servizio</title>
      <style>
        body
        {
        font-size: 12px;
        font-family: verdana,arial,sans-serif;
        color: #333333;
        }
        .container
        {
        padding: 20px;
        width: 300px;
        background-color: #FFFFFF;
        border: 1px solid #DDDDDD;
        }
        h1
        {
        font-size: 14px;
        }
        .little
        {
        font-size: 10px;
        color: Grey;
        }
        hr { border: 2px solid #5BB1CA; }
        .footer
        {
        border-top: border: 1px solid #DDDDDD;
        font-size: 10px;
        }
      </style>
    </head>
  </xsl:template>
  <xsl:template name="Footer">
    <div style="border-top: border: 1px solid #DDDDDD; font-size: 10px; margin-top: 10px;">
      Questa comunicazione è stata inviata tramite <a href="http://www.mettiaposto.it">www.mettiaposto.it</a>
      poichè hai inserito, sottoscritto o commentato una segnalazione. Per visualizzare
      la Privacy Policy <a href="http://www.mettiaposto.it/pages/privacy.aspx">clicca qui</a>
      <br />
      Puoi scriverci anche all'indirizzo <a href="mailto:info@mettiaposto.it">info@mettiaposto.it</a>
    </div>
  </xsl:template>
</xsl:stylesheet>
