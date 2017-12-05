<?xml version="1.0"?>
<!-- This stylesheet contains a template to generate the cpms report
     from the xml output by ADO V2.5 and higher.  It takes two parameters, siteRoot
     which defines the http root path of the application, and doc-title which is the
     title of the page and the caption on the table.  If extended column attributes,
     width and align are present, it will utilize them.  
-->
<xsl:stylesheet version="1.0"
		xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
		xmlns:s="uuid:BDC6E3F0-6DA3-11d1-A2A3-00AA00C14882" 
		xmlns:dt="uuid:C2F41010-65B3-11d1-A29F-00AA00C14882" 
		xmlns:rs="urn:schemas-microsoft-com:rowset" 
		xmlns:z="#RowsetSchema"
		xmlns:msxsl="urn:schemas-microsoft-com:xslt"
		xmlns:user="http://www.csaa.com"
		exclude-result-prefixes="s dt rs z msxsl user"
                  	       	 >
<xsl:include href="Report_Includes.xsl"/>

<!--Root template defines the HMTL document & the report table.  It explicitly calls
    other templates in a specific order to build the table -->
<xsl:template match="/">
			<xsl:apply-templates select="//s:Schema" mode="csv"/>
			<xsl:apply-templates select="//rs:data/z:row" mode="csv"/>
</xsl:template>

<xsl:template match="s:Schema" mode="csv">
		<xsl:for-each select="s:ElementType/s:AttributeType[not(@rs:hidden='true')]">
		<xsl:text>"</xsl:text>
		<xsl:value-of select="@name"/>
		<xsl:text>"</xsl:text>
		<xsl:if test="position()!=last()">,</xsl:if>
		</xsl:for-each>
		<xsl:text>&#xd;&#xa;</xsl:text>
	</xsl:template>
	
<!-- generates a table data row -->
<xsl:template match="z:row" mode="csv">
		<xsl:apply-templates select="//s:Schema/*/*" mode="data"><xsl:with-param name="i" select="."/></xsl:apply-templates>
		<xsl:text>&#xd;&#xa;</xsl:text>
</xsl:template>

<!-- generates a table data cell -->
<xsl:template match="s:AttributeType[not(@rs:hidden='true')]" mode="data">
	<xsl:param name="i"/>
		<xsl:if test="s:datatype/@dt:type='string' or s:datatype/@dt:type='dateTime'">"</xsl:if>
		<xsl:value-of select="user:tags(string($i/@*[local-name()=current()/@name]))" disable-output-escaping="no"/>
		<xsl:if test="s:datatype/@dt:type='string' or s:datatype/@dt:type='dateTime'">"</xsl:if>		
		<xsl:if test="position()!=last()-1">,</xsl:if>	
</xsl:template>

</xsl:stylesheet>
