<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true"
    CodeFile="frmRptGSTReturnOutward.aspx.cs" Inherits="VIEW_frmRptGSTReturnOutward" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/bootstrap-multiselect.css" rel="stylesheet" />
    <script type="text/javascript" src="../js/bootstrap.js"></script>
    <script type="text/javascript" src="../js/bootstrap-multiselect.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        $(function () {
            $('#ContentPlaceHolder1_ddldepot').multiselect({
                includeSelectAllOption: true
            });
            $("#ContentPlaceHolder1_ddldepot").multiselect('selectAll', false);
            $("#ContentPlaceHolder1_ddldepot").multiselect('updateButtonText');
        });
    </script>
    <script type="text/javascript">
        function ValidateListBoxDepot(sender, args) {
            var options = document.getElementById("<%=ddldepot.ClientID%>").options;
             for (var i = 0; i < options.length; i++) {
                 if (options[i].selected == true) {
                     args.IsValid = true;
                     return;
                 }
             }
             args.IsValid = false;
         }
    </script>
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_beginRequest(BeginRequestHandler);
        prm.add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args) {
            var popup = $find('<%= modalPopup.ClientID %>');
            if (popup != null) {
                popup.show();
            }
        }

        function EndRequestHandler(sender, args) {
            var popup = $find('<%= modalPopup.ClientID %>');
            if (popup != null) {
                popup.hide();
            }
        }
    </script>
    <script type="text/javascript">
        function exportToExcel() {
            grdinwardsupply.exportToExcel();
        }
    </script>
    <asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
            <asp:Image ID="Image1" ImageUrl="../images/loading123.gif" AlternateText="Processing"
                runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
        PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="content">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active">
                                <span class="h_icon_he list_images"></span>
                                <h6>GST Outward Return Report</h6>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="InputTable" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px;">
                                                <fieldset>
                                                    <legend>GST Outward Return Report</legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td width="90" class="field_title">
                                                                <asp:Label ID="Label1" Text="From Date" runat="server"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td width="100" class="field_input">
                                                                <asp:TextBox ID="txtfromdate" runat="server" Enabled="false" Width="120" placeholder="dd/MM/yyyy"
                                                                    MaxLength="10"></asp:TextBox>
                                                                <asp:ImageButton ID="Imgfrom" runat="server" ImageUrl="~/images/calendar.png" Width="24px"
                                                                    Height="24px" ImageAlign="AbsMiddle" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="Imgfrom" runat="server"
                                                                    TargetControlID="txtfromdate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>
                                                            <td width="90" class="field_title">
                                                                <asp:Label ID="Label2" Text="To Date" runat="server"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td width="100" class="field_input">
                                                                <asp:TextBox ID="txttodate" runat="server" Enabled="false" Width="120" placeholder="dd/MM/yyyy"
                                                                    MaxLength="10"></asp:TextBox>
                                                                <asp:ImageButton ID="ImgToDate" runat="server" ImageUrl="~/images/calendar.png" Width="24px"
                                                                    Height="24px" ImageAlign="AbsMiddle" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImgToDate" runat="server"
                                                                    TargetControlID="txttodate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field_title" width="90">
                                                                <asp:Label ID="Label9" runat="server" Text="STATE"></asp:Label>
                                                            </td>
                                                            <td class="field_input" width="100">
                                                                <asp:DropDownList ID="ddlstate" Width="200" runat="server" class="chosen-select"
                                                                    data-placeholder="Select" AppendDataBoundItems="True" ValidationGroup="ADD" OnSelectedIndexChanged="ddlstateselectedindexchange"
                                                                    AutoPostBack="True">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="CV_state" runat="server" InitialValue="0" ErrorMessage="Required!"
                                                                    ForeColor="Red" ControlToValidate="ddlstate" ValidateEmptyText="false" ValidationGroup="Show"
                                                                    SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td class="field_title" width="90">
                                                                <asp:Label ID="Label3" runat="server" Text="Depot"></asp:Label>
                                                            </td>
                                                            <td width="100" class="field_input">
                                                                <asp:ListBox ID="ddldepot" runat="server" SelectionMode="Multiple" TabIndex="4"></asp:ListBox>
                                                                <asp:CustomValidator ID="cvdepot" runat="server" ValidateEmptyText="true" ControlToValidate="ddldepot"
                                                                    ValidationGroup="Show" ErrorMessage="Required!" Display="Dynamic" ClientValidationFunction="ValidateListBoxDepot"
                                                                    ForeColor="Red"></asp:CustomValidator>
                                                            </td>
                                                        </tr>

                                                        <tr>

                                                            <td class="field_input" width="90">
                                                                <div class="btn_24_blue">
                                                                    <span class="icon exclamation_co"></span>
                                                                    <asp:Button ID="btnshow" runat="server" ValidationGroup="Show" Text="Show" CssClass="btn_link"
                                                                        OnClick="btnshow_Click" />
                                                                </div>
                                                            </td>
                                                            <td class="field_input" align="left" width="100" colspan="3">
                                                                <div class="btn_24_blue">
                                                                    <span class="icon doc_excel_table_co"></span><a href="#" onclick="exportToExcel();">
                                                                        <asp:Button ID="btnExport" runat="server" Text="Export Excel" CssClass="btn_link" /></a>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="gridcontent-inner">
                                        <fieldset>
                                            <legend>DETAILS</legend>
                                            <div class="gridcontent" style="margin-bottom: 8px;">
                                                <cc1:Grid ID="grdinwardsupply" runat="server" AllowPaging="true" CallbackMode="true"
                                                    Serialize="true" FolderStyle="../GridStyles/premiere_blue" AllowPageSizeSelection="true"
                                                    AllowSorting="true" PageSize="50" ShowColumnsFooter="true" AllowAddingRecords="false"
                                                    ViewStateMode="Enabled" AllowFiltering="true" FolderExports="resources/exports"
                                                    OnExporting="gvSummaryreport_Exporting" OnExported="gvSummaryreport_Exported">
                                                    <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size: 10px;background-color: #DCE3E8;"
                                                        CSSExportCellStyle="font-weight: normal;font-size: 10px;color: #0000;" />
                                                    <MasterDetailSettings LoadingMode="OnLoad" State="Collapsed" ShowEmptyDetails="false" />
                                                    <ExportingSettings ExportAllPages="true" ExportDetails="true" ExportColumnsFooter="true"
                                                        AppendTimeStamp="false" FileName="GST_Outward_Return_Report" />
                                                    <ScrollingSettings ScrollWidth="100%" ScrollHeight="250" />
                                                </cc1:Grid>
                                            </div>
                                        </fieldset>
                                        <cc1:MessageBox ID="MessageBox1" runat="server" />
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
