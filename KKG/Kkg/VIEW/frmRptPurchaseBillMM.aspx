<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmRptPurchaseBillMM.aspx.cs" Inherits="VIEW_frmRptPurchaseBillMM" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<html>
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
   
    <div class="white_content">
        <rsweb:ReportViewer ID="ReportViewer1" runat="server"
            Width="100%">
        </rsweb:ReportViewer>
    </div>
    </form>
</body>
</html>