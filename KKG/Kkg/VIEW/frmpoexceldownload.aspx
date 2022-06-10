<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmpoexceldownload.aspx.cs" Inherits="VIEW_frmpoexceldownload" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
     <tr>
     <td class="field_input" style="padding-left: 10px;">
                                <div class="btn_24_blue">
                                    <span class="icon doc_excel_table_co"></span>
                                    <button type="button" id="btnpdf" runat="server"
                                         class="btn_link" onserverclick="btnpdf_ServerClick">
                                       Click Here To Download Excel</button>

                                </div>
                            </td>
         </tr>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <div  class="white_content"  runat="server">
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="1000px" 
            Width="800px" style="margin-right: 0px">
        </rsweb:ReportViewer>
        </div>
       
    </form>
</body>
</html>
