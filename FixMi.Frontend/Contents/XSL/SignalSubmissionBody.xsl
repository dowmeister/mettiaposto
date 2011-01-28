<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
  <xsl:output method="html" version="4.0" encoding="iso-8859-1" indent="yes" omit-xml-declaration="yes" />
  <xsl:include href="CommonTemplates.xsl"></xsl:include>
  <xsl:template match="/">
    <html>
      <xsl:call-template name="HtmlStyles"></xsl:call-template>
      <body>
        <div class="container">
          <b>Nuova segnalazione inserita</b>
          <br/>
          URL:
          <xsl:element name="a">
            <xsl:attribute name="href">
              http://www.mettiaposto.it/<xsl:value-of select="/Signal/City"/>/<xsl:value-of select="/Signal/SignalID"/>/segnalazione.aspx
            </xsl:attribute>
            http://www.mettiaposto.it/<xsl:value-of select="/Signal/City"/>/<xsl:value-of select="/Signal/SignalID"/>/segnalazione.aspx
          </xsl:element>
        </div>
        <xsl:call-template name="Footer"></xsl:call-template>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
