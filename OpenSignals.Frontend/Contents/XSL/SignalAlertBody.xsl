<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
  <xsl:output method="html" version="4.0" encoding="iso-8859-1" indent="yes" omit-xml-declaration="yes" />
  <xsl:include href="CommonTemplates.xsl"></xsl:include>
  <xsl:template match="/">
    <html>
      <xsl:call-template name="HtmlStyles"></xsl:call-template>
      <body>
        <span style="font-size: 10px; color: #666666;">Mettiaposto.it</span>
        <h1 style="font-size: 14px; background-color: #44A2D8; padding: 10px">
          Segnalazione aggiornata
        </h1>
        <div style="padding: 20px; background-color: #FFFFFF; border: 1px solid #44A2D8;">
          <b>La segnalazione che stai controllando o che hai creato è stata aggiornata.</b>
          <xsl:if test="/SignalProxy/Status = '2'">
            <br/>
            <br/>
            <b>La segnalazione è stata risolta.</b>
            <br/>
            <br/>
            Clicca sul link sotto per visualizzare la segnalazione e il messaggio di risoluzione.
          </xsl:if>
          <xsl:if test="/SignalProxy/Status = '4'">
            <br/>
            <br/>
            <b>La segnalazione è stata riaperta.</b>
            <br/>
            <br/>
            Clicca sul link sotto per visualizzare la segnalazione e il messaggio di risoluzione.
          </xsl:if>
          <br/>
          <br/>
          Puoi trovare la segnalazione qui: 
          <xsl:element name="a">
            <xsl:attribute name="href">
              http://www.mettiaposto.it/<xsl:value-of select="/SignalProxy/City"/>/<xsl:value-of select="/SignalProxy/SignalID"/>/segnalazione.aspx
            </xsl:attribute>
            http://www.mettiaposto.it/<xsl:value-of select="/SignalProxy/City"/>/<xsl:value-of select="/SignalProxy/SignalID"/>/segnalazione.aspx
          </xsl:element>
        </div>
        <xsl:call-template name="Footer"></xsl:call-template>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
