<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmRptInvoicePrint.aspx.cs" Inherits="VIEW_frmRptInvoicePrint" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<html>
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
       <asp:ScriptManager ID="ScriptManager1" runat="server" />
   
    <div class="white_content"  runat="server">
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="1000px" 
            Width="800px" style="margin-right: 0px">
        </rsweb:ReportViewer>
    </div>
     
    </form>
</body>
</html>
