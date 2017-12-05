<?xml version="1.0"?>
<!-- This stylesheet contains a template to generate a nicely formatted table
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
	<xsl:call-template name="root"/>
</xsl:template>

<xsl:template name="titleRow2"></xsl:template>

<!-- generates a table data cell -->
<xsl:template match="s:AttributeType[not(@rs:hidden='true')]" mode="data">
	<xsl:param name="i"/>
	<td>
		<xsl:choose>
			<xsl:when test="@rs:name='Call ID'">
				<a onclick="return popDetail(this)">
					<xsl:attribute name="href">Call_Item_Detail.asp?id=<xsl:value-of select="$i/@*[local-name()='id']"/><xsl:if test="$i/@*[local-name()='Reassigned']!=''">&amp;dup=1</xsl:if></xsl:attribute>
					<xsl:value-of select="user:tags(string($i/@*[local-name()=current()/@name]))" disable-output-escaping="yes"/>
				</a>
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="user:tags(string($i/@*[local-name()=current()/@name]))" disable-output-escaping="yes"/>
			</xsl:otherwise>
		</xsl:choose>
	</td>
</xsl:template>

<!-- generates a table summary cell -->
<xsl:template match="s:AttributeType[not(@rs:hidden='true')]" mode="sum">
	<xsl:param name="i"/>
	<td>
		<xsl:if test="$i/@*[local-name()='GROUPING']=2 and position()=4"><xsl:attribute name="class">red</xsl:attribute></xsl:if>
		<b><xsl:value-of select="user:tags(string($i/@*[local-name()=current()/@name]))" disable-output-escaping="yes"/></b>
	</td>
</xsl:template>

</xsl:stylesheet>
