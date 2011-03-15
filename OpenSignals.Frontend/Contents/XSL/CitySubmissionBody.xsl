<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
    <xsl:output method="html" version="4.0" encoding="iso-8859-1" indent="yes" omit-xml-declaration="yes" />
    <xsl:include href="CommonTemplates.xsl"></xsl:include>
    <xsl:template match="/">
        <html>
            <xsl:call-template name="HtmlStyles"></xsl:call-template>
            <body>
                <div class="container">
                    <b>Città richiesta:</b>
                    <br/>
                    Città: <xsl:value-of select="CityRequest/City"/>
                    <br/>
                    Email: <xsl:value-of select="CityRequest/Email"/>
                </div>
                <xsl:call-template name="Footer"></xsl:call-template>
            </body>
        </html>
    </xsl:template>
</xsl:stylesheet>

