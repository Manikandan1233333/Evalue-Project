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
<xsl:output method="html" version="3.0" encoding="UTF-8" omit-xml-declaration="yes"/>
<xsl:param name="siteRoot" select="'/'"/>
<xsl:param name="doc-title" select="'Report'"/>

<msxsl:script language="JScript" implements-prefix="user">
	var gt = String.fromCharCode(62);
	var lt = String.fromCharCode(60);
	var br = String.fromCharCode(60,66,82,62);
	var amp = String.fromCharCode(38);
	function tags(st) {
	  return st
		.replace(new RegExp(amp,'g'),amp+'amp;')
		.replace(new RegExp(gt,'g'),amp+'gt;')
		.replace(new RegExp(lt,'g'),amp+'lt;')
		.replace(/\r/g,'')
		.replace(/\n/g,br+'\r\n')
	}
	var lastCol = 1, numVisible = 1;
	function getLastColumn() {
		return lastCol;
	}
	function visibleCols() {
		return numVisible;
	}
	function visibleCols(nodelist) {
		var n=0;
		for (var i=0; i&lt;nodelist.length; i++) {
			if (nodelist.item(i).getAttribute('rs:hidden')!='true') {
				n++;
				lastCol = n;
			}
		}
		numVisible = n;
		return n;
	}
	function isBlank(val, def) {
		return (val!='')?val:def;
	}
	function checkZero(st) {
		return (st=='0')?'':tags(st);
	}
</msxsl:script>

<!--Root template defines the HMTL document & the report table.  It explicitly calls
    other templates in a specific order to build the table -->
<xsl:template name="root">
	<xsl:choose>
		<xsl:when test="count(//rs:data/z:row)=0"><xsl:call-template name="noData"/></xsl:when>
		<xsl:otherwise>
			<table cellspacing="0" cellpadding="0" align="center" valign="top" width="100%" style="border-collapse:collapse;" rules="groups" frame="void" border="1" id="RPT">
			<xsl:apply-templates select="//s:Schema" mode="colGroupDef"/>
			<xsl:call-template name="titleRow"/>
			<xsl:call-template name="blankRow"/>
			<xsl:call-template name="titleRow2"/>
			<xsl:apply-templates select="//s:Schema" mode="header"/>
			<xsl:call-template name="blankRow"/>
			<xsl:apply-templates select="//rs:data"/>
			</table>
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<!-- template for no data message -->
<xsl:template name="noData">
	<b><center>There is no matching data.</center></b>
</xsl:template>

<!-- template for table title row -->
<xsl:template name="titleRow">
	<tr class="darkRow">
		<td class="header">
		<xsl:attribute name="colspan">
			<xsl:value-of select="user:visibleCols(//s:Schema/s:ElementType/s:AttributeType)"/>
		</xsl:attribute>
		<xsl:value-of select="$doc-title"/></td>
	</tr>
</xsl:template>

<!-- generates a blank row -->
<xsl:template name="blankRow">
	<tr>
		<td height="15"><xsl:attribute name="colspan"><xsl:value-of select="user:visibleCols()"/></xsl:attribute></td>
	</tr>
</xsl:template>

<!-- generates the colgroup tags to define the table columns -->
<xsl:template match="s:AttributeType[position()=1 or @colspan>0]" mode="colGroupDef">
	<colgroup>
		<xsl:variable name="next" select="number(following-sibling::*[@colspan>0]/@rs:number)"/>
		<xsl:apply-templates select=". | following-sibling::*[not($next) or $next>@rs:number]" mode="colDefinition"/>
	</colgroup>
</xsl:template>

<!-- generates the col tag to define the table columns -->
<xsl:template match="s:AttributeType[not(@rs:hidden='true')]" mode="colDefinition">
	<col>
		<xsl:attribute name="align"><xsl:value-of select="user:isBlank(string(@align), 'left')"/></xsl:attribute>
		<xsl:if test="@width!=''"><xsl:attribute name="width"><xsl:value-of select="@width"/></xsl:attribute></xsl:if>
		<xsl:if test="@colStyle!=''"><xsl:attribute name="style"><xsl:value-of select="@colStyle"/></xsl:attribute></xsl:if>
	</col>
</xsl:template>

<!-- template to build the column title row(s) -->
<xsl:template match="s:Schema" mode="header">
	<xsl:if test="count(./*/*[@colspan>0])!=0">
		<TR><xsl:apply-templates mode="colGroup"/></TR>
	</xsl:if>
	<TR><xsl:apply-templates mode="colHead"/></TR>
</xsl:template>

<!-- named template used to draw the column names that handles extended column names-->
<xsl:template name="extended-name">
	<xsl:choose>
		<xsl:when test="@Column_Head!=''">
			<xsl:value-of select="user:tags(string(@Column_Head))" disable-output-escaping="yes"/>
		</xsl:when>
		<xsl:when test="@rs:name!=''">
			<xsl:value-of select="@rs:name"/>
		</xsl:when>
		<xsl:otherwise>
			<xsl:value-of select="@name"/>
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<!-- generates column group cells -->
<xsl:template match="s:AttributeType[@colspan>0 and not(@rs:hidden='true')]" mode="colGroup">
	<td class="labels" align="center">
		<xsl:attribute name="colspan"><xsl:value-of select="@colspan"/></xsl:attribute>
		<xsl:value-of select="@colGroup"/>
	</td>
</xsl:template>

<!-- generates column heading cell -->
<xsl:template match="s:AttributeType[not(@rs:hidden='true')]" mode="colHead">
	<td>
		<xsl:attribute name="class"><xsl:value-of select="user:isBlank(string(@headClass), 'labels')"/></xsl:attribute>
		<xsl:choose>
			<xsl:when test="@sortType!=''">
				<a href="js:Sort Table">
					<xsl:attribute name="onclick">RPT.sort(columnIndex(this), '<xsl:value-of select="@sortType"/>'); return false;</xsl:attribute>
					<xsl:call-template name="extended-name"/>
				</a>
			</xsl:when>
			<xsl:otherwise>
				<xsl:call-template name="extended-name"/>
			</xsl:otherwise>
		</xsl:choose>
	</td>
</xsl:template>

<!-- generates a table data row -->
<xsl:template match="z:row[not(@GROUPING) or @GROUPING=0]">
	<tr valign="bottom" sortable="true">
		<xsl:if test="position() mod 2!=0"><xsl:attribute name="class">lightRow</xsl:attribute></xsl:if>
		<xsl:apply-templates select="//s:Schema/*/*" mode="data"><xsl:with-param name="i" select="."/></xsl:apply-templates>
	</tr>
</xsl:template>

<!-- generates a table summary row -->
<xsl:template match="z:row[@GROUPING>0 and position()!=2]">
	<xsl:call-template name="blankRow"/>
	<tr valign="bottom">
		<xsl:apply-templates select="//s:Schema/*/*" mode="sum"><xsl:with-param name="i" select="."/></xsl:apply-templates>
	</tr>
</xsl:template>

</xsl:stylesheet>