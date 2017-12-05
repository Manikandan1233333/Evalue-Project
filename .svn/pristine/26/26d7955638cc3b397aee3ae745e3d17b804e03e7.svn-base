<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ConfirmingEmail.ascx.cs" Inherits="MSC.Controls.ConfirmingEmail" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
Thank you for your order with California State Automobile Association.  Your credit card
number xxxx-xxxx-xxxx-<%=Order.Card.CCNumber.Substring(12,4)%> has been charged <%=Order.Lines.Total.ToString("C")%>.

To view your temporary membership cards that you may print and use until your new cards
arrive, please go to <%=CardURL%>
<asp:Label ID="DefaultFrom" runat="server" Visible="False">California State Automobile Association<webmaster@aaaemail.com></asp:Label>
<asp:Label ID="Subject" runat="server" Visible="False">CSAA order confirmation</asp:Label>
<asp:Label ID="SendEmail" Runat="server" Visible="False">True</asp:Label>

