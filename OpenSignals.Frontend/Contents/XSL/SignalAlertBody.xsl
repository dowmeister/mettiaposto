<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
  <xsl:output method="html" version="4.0" encoding="iso-8859-1" indent="yes" omit-xml-declaration="yes" />
  <xsl:include href="CommonTemplates.xsl"></xsl:include>
  <xsl:template match="/">
    <html>
      <xsl:call-template name="HtmlStyles"></xsl:call-template>
      <body>
        <div class="container">
          <b>La segnalazione che stai controllando è stata aggiornata</b>
          <br/>
          Un utente ha commentato la segnalazione.
          <br/>
          Puoi trovare la segnalazione all'indirizzo:
          <xsl:element name="a">
            <xsl:attribute name="href">
              http://www.mettiaposto.it/<xsl:value-of select="/SignalProxy/City"/>/<xsl:value-of select="/SignalProxy/SignalID"/>/segnalazione.aspx
            </xsl:attribute>
            http://www.mettiaposto.it/<xsl:value-of select="/SignalProxy/City"/>/<xsl:value-of select="/SignalProxy/SignalID"/>/segnalazione.aspx
          </xsl:element>
          <xsl:if test="/SignalProxy/Comment/SetSignalResolved = 'true'">
            <br/>
            <br/>
            <b>La segnalazione è stata risolta.</b>
            <br/>
            Clicca sul link sopra per visualizzare la segnalazione e il messaggio di risoluzione.
          </xsl:if>
        </div>
        <xsl:call-template name="Footer"></xsl:call-template>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
