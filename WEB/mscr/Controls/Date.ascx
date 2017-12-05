<%@ Control Language="c#" AutoEventWireup="True" Codebehind="Date.ascx.cs" Inherits="MSCR.Controls.Date" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<!-- 2/5/2007 STAR Retrofit II - Added New User Control as a part of CSR 5157 for displaying start and end time in Payment Reconciliation Report-->
<!-- STAR Retrofit III Changes: -->
<!-- 04/02/2007 - Changes done as part of CSR #5595-->
<!-- STAR Retrofit III.Ch1: Modified the code to include javascript Date Validation function-->
<!-- STAR Retrofit III.Ch2: Removed autopost attribute to prevent round trips to server. 
							   Added Custom Validator to invoke Date Validation function whenever month value has been changed-->
<!-- STAR Retrofit III.Ch1: Modified the code to include javascript Date Validation function-->
<!-- CSR 5716.Ch1: Modified to set the start time as per the selected start date-->
<!-- CSR 5716.Ch2: Modified to set the end time as per the selected end date-->
<!-- 01/17/2008 CPM Migration.Ch1: Modified to set the start time and end time as per the selected start date and end date from the year 2008 onwards. -->
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
        //CPM Migration.Ch1: START - Modified to set the start time and end time as per the selected start date and end date from the year 2008 onwards.
        var dtCutoff = new Date("8/3/2007");
        if(dt > dtCutoff)
			document.getElementById('<%=start_time.ClientID %>').innerHTML ='9:00:00 PM';
		else
			document.getElementById('<%=start_time.ClientID %>').innerHTML ='9:02:00 PM';
		//CPM Migration.Ch1: END
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
        //CPM Migration.Ch1: START - Modified to set the start time and end time as per the selected start date and end date from the year 2008 onwards.
          var dtCutoff = new Date("8/3/2007");
         if(dt > dtCutoff)
			document.getElementById('<%=end_time.ClientID %>').innerHTML ='8:59:59 PM';
		else
			document.getElementById('<%=end_time.ClientID %>').innerHTML ='9:01:59 PM';
		//CPM Migration.Ch1: END
      return(true);
 }
</script>
<!--STAR Retrofit III.Ch1: END --><tr>
	<td height="10" colspan="5"></td>
</tr>
<tr>
	<td class="arial_12_bold">&nbsp;&nbsp;</td>
	<td class="arial_12_bold">Start Date</td>
	<td class="arial_12_bold">End Date</td>
	<!-- start and end time description --><td class="arial_12_bold">Start Time</td>
	<td class="arial_12_bold">End Time</td>
</tr>
<tr>
	<td class="arial_12_bold">&nbsp;&nbsp;</td>
	<td class="arial_12" height="12">
		<!-- STAR Retrofit III.Ch2: START - Removed autopost attribute to prevent round trips to server. 
							   Added Custom Validator to invoke Date Validation function whenever month value has been changed--><asp:dropdownlist id="start_date_month" runat="server"></asp:dropdownlist><asp:dropdownlist id="start_date_day" runat="server"></asp:dropdownlist><asp:dropdownlist id="start_date_year" runat="server"></asp:dropdownlist><asp:customvalidator id="CustomStartDateValidatorCtl" runat="server" ControlToValidate="start_date_day" ClientValidationFunction="CallStartDateFun"></asp:customvalidator><asp:customvalidator id="CustomStartMonthValidatorCtl" runat="server" ControlToValidate="start_date_month" ClientValidationFunction="CallStartDateFun"></asp:customvalidator><asp:customvalidator id="CustomStartYearValidatorCtl" runat="server" ControlToValidate="start_date_year" ClientValidationFunction="CallStartDateFun"></asp:customvalidator></td>
	<td class="arial_12" height="12">
		<asp:dropdownlist id="end_date_month" runat="server"></asp:dropdownlist><asp:dropdownlist id="end_date_day" runat="server"></asp:dropdownlist><asp:dropdownlist id="end_date_year" runat="server"></asp:dropdownlist>
		<asp:customvalidator id="CustomEndDateValidatorCtl" runat="server" ClientValidationFunction="CallEndDateFun" ControlToValidate="end_date_day"></asp:customvalidator><asp:customvalidator id="CustomEndMonthValidatorCtl" runat="server" ClientValidationFunction="CallEndDateFun" ControlToValidate="end_date_month"></asp:customvalidator><asp:customvalidator id="CustomEndYearValidatorCtl" runat="server" ClientValidationFunction="CallEndDateFun" ControlToValidate="end_date_year"></asp:customvalidator>
		<!-- STAR Retrofit III.Ch2: END --></td>
	<!-- Lables to display start and end time -->
	<td class="arial_12">
		<asp:Label id="start_time" Runat="server"></asp:Label>
	</td>
	<td class="arial_12">
		<asp:Label id="end_time" Runat="server"></asp:Label>
		&nbsp;
	</td>
</tr>
