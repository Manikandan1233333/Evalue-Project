<?xml version="1.0"?>
<!-- This stylesheet is used to convert an ADO rowset that contains
     extended column attributes into another stylesheet that adds these
     attributes to a primary xlm source by transforming it and adding
     attributes to the schema section of the document.
-->
<xsl:stylesheet version="1.0"
		xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
		xmlns:s="uuid:BDC6E3F0-6DA3-11d1-A2A3-00AA00C14882" 
		xmlns:dt="uuid:C2F41010-65B3-11d1-A29F-00AA00C14882" 
		xmlns:rs="urn:schemas-microsoft-com:rowset" 
		xmlns:z="#RowsetSchema"
                  	       	 >
<xsl:output method="xml" version="1.0" indent="yes" omit-xml-declaration="yes" />

<!-- Root template match -->
<xsl:template match="/">
	<xsl:text disable-output-escaping="yes">&#60;&#63;xml version="1.0"&#63;&#62;
</xsl:text>
  	<xsl:element name="xsl:stylesheet">
		<xsl:attribute name="version">1.0</xsl:attribute>
		<!--next two attributes force the namespaces to be included-->
		<xsl:attribute name="s:s"/>
		<xsl:attribute name="rs:rs"/>
		<xsl:element name="xsl:output">
			<xsl:attribute name="method">xml</xsl:attribute>
			<xsl:attribute name="version">1.0</xsl:attribute>
		</xsl:element>
		<xsl:element name="xsl:template">
			<xsl:attribute name="match">/ | @* | node()</xsl:attribute>
			<xsl:element name="xsl:copy">
				<xsl:element name="xsl:apply-templates">
					<xsl:attribute name="select">@* | node()</xsl:attribute>
				</xsl:element>
			</xsl:element>
		</xsl:element>
		<xsl:element name="xsl:template">
			<xsl:attribute name="match">//s:AttributeType</xsl:attribute>
			<xsl:element name="xsl:copy">
				<xsl:element name="xsl:choose">
					<xsl:apply-templates select="//rs:data/z:row"/>
				</xsl:element>
				<xsl:element name="xsl:apply-templates">
					<xsl:attribute name="select">@* | node()</xsl:attribute>
				</xsl:element>
			</xsl:element>
		</xsl:element>
	</xsl:element>
</xsl:template>

<xsl:template name="extended-name">
	<xsl:choose>
		<xsl:when test="@rs:name!=''">
			<xsl:value-of select="@rs:name"/>
		</xsl:when>
		<xsl:otherwise>
			<xsl:value-of select="@name"/>
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<!--build the when element for each row that adds the attributes to the schema-->
<xsl:template match="z:row">
	<xsl:variable name="i" select="."/>
	<xsl:element name="xsl:when">
		<xsl:attribute name="test">@rs:number=<xsl:value-of select="@col"/></xsl:attribute>
		<xsl:for-each select="//s:Schema/s:ElementType/s:AttributeType">
			<xsl:variable name="j" select="@name"/>
			<xsl:variable name="l" select="$i/@*[local-name()=$j]"/>
			<xsl:if test="not(@rs:hidden='true') and $j!='col' and not($l='')">
				<xsl:element name="xsl:attribute">
					<xsl:attribute name="name"><xsl:call-template name="extended-name"/></xsl:attribute>
						<xsl:value-of select="$l"/>
				</xsl:element>
			</xsl:if>
		</xsl:for-each>
	</xsl:element>
</xsl:template>

</xsl:stylesheet>
