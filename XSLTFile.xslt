<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
    <xsl:output method="xml" indent="yes"/>
  <!--<xsl:output omit-xml-declaration="yes"/>-->

  <xsl:template match="root">
    <div>
      <xsl:apply-templates select="user" />
    </div>
  </xsl:template>
  <xsl:template match="user">
    <h2>
      <xsl:value-of select="@id"/>
    </h2>
    <ul>
      <xsl:apply-templates select="name" />
    </ul>
  </xsl:template>
</xsl:stylesheet>
