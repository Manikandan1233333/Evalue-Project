//04/02/2007 - New JScript to Validate date in various reports. Created as a part of STAR Retrofit III - CSR #5595
function CallStartDateFun(sender, args){
   var d=document.getElementById("start_date_day").value
   var m=document.getElementById("start_date_month").value
   var y=document.getElementById("start_date_year").value
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
       document.getElementById("start_date_day").value = dt.getDate();
       }
     return(true);
 }
 
 	function CallEndDateFun(sender, args){
   var d=document.getElementById("end_date_day").value
   var m=document.getElementById("end_date_month").value
   var y=document.getElementById("end_date_year").value
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
       document.getElementById("end_date_day").value = dt.getDate();
       }
      return(true);
 }
