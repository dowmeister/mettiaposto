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
        width: 100%;
        border: 1px solid #5BB1CA;
        }
        hr { border: 2px solid #5BB1CA; }
      </style>
    </head>
  </xsl:template>
  <xsl:template name="Footer">
    <hr/>
    Questa comunicazione è stata inviata da <a href="www.mettiaposto.it">www.mettiaposto.it</a> .
    Per informazioni, lamentele e brutte parole scrivi a <a href="mailto:info@mettiaposto.it">info@mettiaposto.it</a>
  </xsl:template>
</xsl:stylesheet>
