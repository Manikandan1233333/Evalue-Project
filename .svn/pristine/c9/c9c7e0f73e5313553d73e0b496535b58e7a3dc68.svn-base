<%@ Control Language="c#" AutoEventWireup="True" Codebehind="Dates.ascx.cs" Inherits="MSCR.Controls.Dates" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<!-- STAR Retrofit III Changes: -->
<!-- 04/02/2007 - Changes done as part of CSR #5595-->
<!-- STAR Retrofit III.Ch1: Modified the code to include javascript Date Validation function-->
<!-- STAR Retrofit III.Ch2: Removed autopost attribute to prevent round trips to server. 
							Added Custom Validator to invoke Date Validation function whenever month value has been changed-->
<!-- STAR Retrofit III.Ch1: START - Modified the code to include javascript Date Validation function-->
<script language="javascript" type="text/javascript">
	function CallStartDateFun(sender, args){
   var d=document.getElementById('<%=start_date_day.ClientID %>').value
   var m=document.getElementById('<%=start_date_month.ClientID %>').value
   var y=document.getElementById('<%=start_date_year.ClientID %>').value
   if(IsValidStartDate(d,m,y))
        args.IsValid=true;
   else
        args.IsValid=false;
        
	}
	
	function IsValidStartDate(Day,Mn,Yr){
    var DateVal = Mn + "/" + Day + "/" + Yr;
    var dt = new Date(DateVal);
    
    if(dt.getDate()!=Day){
        var dtDiff = dt.getDate();
        Day = Day - dtDiff;
        DateVal = Mn + "/" + Day + "/" + Yr;
        dt = new Date(DateVal);
       document.getElementById('<%=start_date_day.ClientID %>').value = dt.getDate();
       // return(false);
        }
    return(true);
 }
 
 	function CallEndDateFun(sender, args){
   var d=document.getElementById('<%=end_date_day.ClientID %>').value
   var m=document.getElementById('<%=end_date_month.ClientID %>').value
   var y=document.getElementById('<%=end_date_year.ClientID %>').value
   if(IsValidEndDate(d,m,y))
        args.IsValid=true;
   else
        args.IsValid=false;
        
	}
	
	function IsValidEndDate(Day,Mn,Yr){
    var DateVal = Mn + "/" + Day + "/" + Yr;
    var dt = new Date(DateVal);
    
    if(dt.getDate()!=Day){
        var dtDiff = dt.getDate();
        Day = Day - dtDiff;
        DateVal = Mn + "/" + Day + "/" + Yr;
        dt = new Date(DateVal);
       document.getElementById('<%=end_date_day.ClientID %>').value = dt.getDate();
       // return(false);
        }
    return(true);
 }
 
</script>
<!-- STAR Retrofit III.Ch1: END -->
<tr>
	<td colspan="5" height="10"></td>
</tr>
<tr>
	<td class="arial_12_bold">&nbsp;&nbsp;</td>
	<td class="arial_12_bold">Start Date</td>
	<td class="arial_12_bold">End Date</td>
	<td class="arial_12_bold">Start Time</td>
	<td class="arial_12_bold">End Time</td>
</tr>
<tr>
	<td class="arial_12_bold">&nbsp;&nbsp;</td>
	<td class="arial_12" height="12">
		<!-- STAR Retrofit III.Ch2: START - Removed autopost attribute to prevent round trips to server. 
							   Added Custom Validator to invoke Date Validation function whenever month value has been changed-->
		<asp:dropdownlist id="start_date_month" runat="server"></asp:dropdownlist><asp:dropdownlist id="start_date_day" runat="server"></asp:dropdownlist><asp:dropdownlist id="start_date_year" runat="server"></asp:dropdownlist>
		<asp:customvalidator id="CustomStartDateValidatorCtl" runat="server" ClientValidationFunction="CallStartDateFun" ControlToValidate="start_date_day"></asp:customvalidator><asp:customvalidator id="CustomStartMonthValidatorCtl" runat="server" ClientValidationFunction="CallStartDateFun" ControlToValidate="start_date_month"></asp:customvalidator><asp:customvalidator id="CustomStartYearValidatorCtl" runat="server" ClientValidationFunction="CallStartDateFun" ControlToValidate="start_date_year"></asp:customvalidator>
	</td>
	<td class="arial_12" height="12">
		<asp:dropdownlist id="end_date_month" runat="server"></asp:dropdownlist><asp:dropdownlist id="end_date_day" runat="server"></asp:dropdownlist><asp:dropdownlist id="end_date_year" runat="server"></asp:dropdownlist></td>
	<asp:customvalidator id="CustomEndDateValidatorCtl" runat="server" ClientValidationFunction="CallEndDateFun" ControlToValidate="end_date_day"></asp:customvalidator><asp:customvalidator id="CustomEndMonthValidatorCtl" runat="server" ClientValidationFunction="CallEndDateFun" ControlToValidate="end_date_month"></asp:customvalidator><asp:customvalidator id="CustomEndYearValidatorCtl" runat="server" ClientValidationFunction="CallEndDateFun" ControlToValidate="end_date_year"></asp:customvalidator>
	<!-- STAR Retrofit III.Ch2: END -->
	<td class="arial_12_bold">
		<asp:dropdownlist id="start_time_hour" runat="server"></asp:dropdownlist>
		<asp:dropdownlist id="start_time_minute" runat="server"></asp:dropdownlist></td>
	<td class="arial_12_bold">
		<asp:dropdownlist id="end_time_hour" runat="server"></asp:dropdownlist>
		<asp:dropdownlist id="end_time_minute" runat="server"></asp:dropdownlist></td>
</tr>
