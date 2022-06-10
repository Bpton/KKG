<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true"
    CodeFile="frmRptProgressReport.aspx.cs" Inherits="VIEW_frmRptProgressReport" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/bootstrap-multiselect.css" rel="stylesheet" />
    <script type="text/javascript" src="../js/bootstrap.js"></script>
    <script type="text/javascript" src="../js/bootstrap-multiselect.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function ValidateListBox(sender, args) {
            args.IsValid = false;
            var options = document.getElementById("<%=ddldepot.ClientID%>").options;
            for (var i = 0; i < options.length; i++) {
                if (options[i].selected == true) {
                    args.IsValid = true;
                    return;
                }
            }
            args.IsValid = false;

            var options = document.getElementById("<%=ddlcategory.ClientID%>").options;
            for (var i = 0; i < options.length; i++) {
                if (options[i].selected == true) {
                    args.IsValid = true;
                    return;
                }
            }
            args.IsValid = false;
            var options = document.getElementById("<%=ddlbrand.ClientID%>").options;
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
        $(function () {
            $('#ContentPlaceHolder1_ddldepot').multiselect({
                includeSelectAllOption: true
            });
            $("#ContentPlaceHolder1_ddldepot").multiselect('selectAll', false);
            $("#ContentPlaceHolder1_ddldepot").multiselect('updateButtonText');
        });

        $(function () {
            $('#ContentPlaceHolder1_ddlbrand').multiselect({
                includeSelectAllOption: true
            });
            $("#ContentPlaceHolder1_ddlbrand").multiselect('selectAll', false);
            $("#ContentPlaceHolder1_ddlbrand").multiselect('updateButtonText');
        });
        $(function () {
            $('#ContentPlaceHolder1_ddlcategory').multiselect({
                includeSelectAllOption: true
            });
            $("#ContentPlaceHolder1_ddlcategory").multiselect('selectAll', false);
            $("#ContentPlaceHolder1_ddlcategory").multiselect('updateButtonText');
        });

         $(function () {
            $('#ContentPlaceHolder1_ddlType').multiselect({
                includeSelectAllOption: true
            });
            $("#ContentPlaceHolder1_ddlType").multiselect('selectAll', false);
            $("#ContentPlaceHolder1_ddlType").multiselect('updateButtonText');
        });
    </script>
    <script type="text/javascript">

        function exportToExcel() {
            var recordcount = gvRptProgress.getTotalNumberOfRecords();
            if (recordcount > 0) {
                gvRptProgress.exportToExcel();
            }
        }

        function exportToExcelSummery() {
            var recordcount = gvRptProgressSummeryData.getTotalNumberOfRecords();
            if (recordcount > 0) {
                gvRptProgressSummeryData.exportToExcel();
            }
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
    <asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
            <asp:Image ID="Image1" ImageUrl="../images/loading123.gif" AlternateText="Processing"
                runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
        PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>Progress Report Details
                                </h6>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlDisplay" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td>
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="90" class="field_title">
                                                            <asp:Label ID="Label1" Text="From Date" runat="server"></asp:Label>
                                                        </td>
                                                        <td width="200" class="field_input">
                                                            <asp:TextBox ID="txtfromdate" runat="server" Enabled="false" Width="85" placeholder="dd/MM/yyyy"
                                                                MaxLength="10"></asp:TextBox>
                                                            <asp:ImageButton ID="Imgfrom" runat="server" ImageUrl="~/images/calendar.png" Width="24px"
                                                                Height="24px" ImageAlign="AbsMiddle" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="Imgfrom" runat="server"
                                                                TargetControlID="txtfromdate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                        <td class="field_title" width="90">
                                                            <asp:Label ID="Label2" Text="To Date" runat="server"></asp:Label>
                                                        </td>
                                                        <td class="field_input">
                                                            <asp:TextBox ID="txttodate" runat="server" Enabled="false" Width="85" placeholder="dd/MM/yyyy"
                                                                MaxLength="10"></asp:TextBox>
                                                            <asp:ImageButton ID="ImgToDate" runat="server" ImageUrl="~/images/calendar.png" Width="24px"
                                                                Height="24px" ImageAlign="AbsMiddle" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImgToDate" runat="server"
                                                                TargetControlID="txttodate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                        <td class="field_title" width="90">
                                                            <asp:Label ID="Label4" Text="Depot" runat="server"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td class="field_input">
                                                            <%-- <asp:DropDownList ID="ddldepot" runat="server" Width="300" class="chosen-select"
                                                                data-placeholder="Select depot" AppendDataBoundItems="True" ValidationGroup="Show">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rvdepot" runat="server" ControlToValidate="ddldepot"
                                                                ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ForeColor="Red"
                                                                InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>--%>

                                                            <asp:ListBox ID="ddldepot" runat="server" SelectionMode="Multiple" TabIndex="3" AppendDataBoundItems="True"
                                                                ValidationGroup="Show"></asp:ListBox>
                                                            <asp:CustomValidator ID="CustomValidator3" runat="server" ValidateEmptyText="true"
                                                                ControlToValidate="ddldepot" ValidationGroup="Show" ErrorMessage="Required!"
                                                                Display="Dynamic" ClientValidationFunction="ValidateListBox" ForeColor="Red"></asp:CustomValidator>
                                                        </td>

                                                        <td class="field_title" style="display:none">
                                                            <asp:Label ID="lbldetails" Text="Details" runat="server"></asp:Label>
                                                        </td>
                                                        <td class="field_input" style="display:none">
                                                            <asp:DropDownList ID="ddldetails" runat="server" Width="100" class="chosen-select"
                                                                data-placeholder="Select depot" AppendDataBoundItems="True" ValidationGroup="Show">
                                                                <asp:ListItem Text="Yes" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="No" Value="1"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>

                                                        <td class="field_title" width="100">
                                                            <asp:Label ID="Label6" runat="server" Text="Store Location"></asp:Label>
                                                        </td>
                                                        <td class="field_input" width="100">
                                                            <asp:DropDownList ID="ddlstorelocation" Width="150" runat="server" ValidationGroup="ADD"
                                                                class="chosen-select" AppendDataBoundItems="True">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="90" class="field_title">
                                                            <asp:Label ID="Label7" Text="TYPE" runat="server"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="200" class="field_input">
                                                            <asp:ListBox ID="ddlType" runat="server" SelectionMode="Multiple" TabIndex="4" AppendDataBoundItems="True"
                                                                ValidationGroup="Show" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged"></asp:ListBox>
                                                        </td>

                                                        <td width="90" class="field_title">
                                                            <asp:Label ID="Label5" Text="Brand" runat="server"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="200" class="field_input">
                                                            <asp:ListBox ID="ddlbrand" runat="server" SelectionMode="Multiple" TabIndex="4" AppendDataBoundItems="True"
                                                                ValidationGroup="Show" AutoPostBack="true" OnSelectedIndexChanged="ddlbrand_SelectedIndexChanged"></asp:ListBox>
                                                            <asp:CustomValidator ID="CustomValidator1" runat="server" ValidateEmptyText="true"
                                                                ControlToValidate="ddlbrand" ValidationGroup="Show" ErrorMessage="Required!"
                                                                Display="Dynamic" ClientValidationFunction="ValidateListBox" ForeColor="Red"></asp:CustomValidator>
                                                        </td>
                                                        <td class="field_title">
                                                            <asp:Label ID="Label3" Text="Category" runat="server"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td class="field_input" colspan="3">
                                                            <asp:ListBox ID="ddlcategory" runat="server" SelectionMode="Multiple" TabIndex="4"
                                                                Width="280px" AppendDataBoundItems="True"></asp:ListBox>
                                                            <asp:CustomValidator ID="CustomValidator2" runat="server" ValidateEmptyText="true"
                                                                ControlToValidate="ddlcategory" ValidationGroup="Show" ErrorMessage="Required!"
                                                                Display="Dynamic" ClientValidationFunction="ValidateListBox" ForeColor="Red"></asp:CustomValidator>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                            <div class="btn_24_blue">
                                                                <span class="icon exclamation_co"></span>
                                                                <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn_link" OnClick="btnShow_Click"
                                                                    ValidationGroup="Show" />
                                                            </div>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                            <div class="btn_24_blue" id="DivbtnExportDetails" runat="server">
                                                                <span class="icon doc_excel_table_co"></span><a href="#" onclick="exportToExcel();">
                                                                    <asp:Button ID="btnExport" runat="server" Text="Export Excel" CssClass="btn_link" /></a>
                                                            </div>
                                                            <div class="btn_24_blue" id="DivbtnExportSummery" runat="server">
                                                                <span class="icon doc_excel_table_co"></span><a href="#" onclick="exportToExcelSummery();">
                                                                    <asp:Button ID="btnExportSummery" runat="server" Text="Export Excel" CssClass="btn_link" /></a>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <ul id="search_box">
                                        <li>
                                            <input name="" id="FilterTextBox" type="text" class="search_input" placeholder="Enter text to search the whole grid"
                                                onkeyup="FilterTextBox_KeyUp();">
                                        </li>
                                        <li>
                                            <input name="" id="OboutButton2" type="submit" value="" class="search_btn" onclick="return performSearch();">
                                        </li>
                                    </ul>
                                    <div class="gridcontent" id="divDetailsData" runat="server">
                                        <cc1:Grid ID="gvRptProgress" runat="server" CallbackMode="true" Serialize="true"
                                            FolderStyle="../GridStyles/premiere_blue" AutoGenerateColumns="false" PageSizeOptions="-1" PageSize="1000"
                                            EnableRecordHover="true" AllowAddingRecords="false" AllowPageSizeSelection="true"
                                            AllowSorting="false" ShowColumnsFooter="true" OnExported="gvRptProgress_Exported"
                                            ShowEmptyDetails="false" AllowFiltering="true" FolderExports="resources/exports"
                                            OnExporting="gvRptProgress_Exporting" OnRowDataBound="gvRptProgress_RowDataBound">
                                            <ExportingSettings ExportColumnsFooter="true" ExportGroupFooter="true" ExportGroupHeader="true"
                                                FileName="ProgressReport" ExportAllPages="true" ExportDetails="true" AppendTimeStamp="false" />
                                            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size: 10px;background-color: #DCE3E8;color: #0000FF;"
                                                CSSExportCellStyle="font-weight: normal;font-size: 10px;color: #0000;background-color: AFEEEE" />
                                            <FilteringSettings InitialState="Visible" />
                                            <Columns>
                                                <cc1:Column DataField="Slno" ReadOnly="true" HeaderText="SL.NO." runat="server"/>
                                                <cc1:Column DataField="ItemCode" HeaderText="ITEM CODE" runat="server" Width="70px">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>

                                                 <cc1:Column DataField="TYPE" HeaderText="TYPE" runat="server" Width="70px">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>

                                                <cc1:Column DataField="ItemName" HeaderText="ITEMNAME" runat="server" Width="80px">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column DataField="ItemType" HeaderText="PRIMARY_ITEM/BRAND" runat="server" Width="300px">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>

                                                <cc1:Column DataField="ItemSubType" HeaderText="ITEMSUBTYPE" runat="server" Width="100px"
                                                    Visible="false">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column DataField="UOMNAME" HeaderText="UOMNAME" runat="server" Width="100px" Align="right">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column DataField="rate" HeaderText="RATE" runat="server" Width="120px" Align="right" Visible="false">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column DataField="Openning" HeaderText="OPENNING" runat="server" Align="right"
                                                    Width="120px">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column DataField="InwardPurchase" HeaderText="INWARDPURCHASE" FooterText="true" Align="right"
                                                    runat="server" Width="120px">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>

                                                <cc1:Column ID="Column6" DataField="Return" HeaderText="RETURN" Align="right" runat="server"
                                                    Width="140px">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>

                                                <cc1:Column ID="Column7" DataField="InwardOthers" HeaderText="INWARDOTHERS" Align="right" runat="server"
                                                    Width="140px">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>

                                                <cc1:Column ID="Column4" DataField="StockJournalInward" HeaderText="STOCKJOURNALINWARD" runat="server" Align="right">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>

                                                <cc1:Column DataField="OUTWARDISSUE" HeaderText="OutwardIssue" runat="server" Width="120px" Align="right">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column DataField="OutwardReturns(PurchaseReturn)" HeaderText="OUTWARDRETURNS(PURCHASERETURN)" runat="server" Align="right" Width="120px">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column DataField="OutwardOthers" HeaderText="OUTWARDOTHERS" runat="server" Align="right" Width="120px">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column10" DataField="Sales" HeaderText="SALES"
                                                    runat="server" Align="right">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>

                                                <cc1:Column ID="Column5" DataField="StockJournalOutward" HeaderText="STOCKJOURNALOUTWARD" Align="right" runat="server"
                                                    Width="130px">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>

                                                <cc1:Column DataField="TotalOutwards" HeaderText="TOTALOUTWARDS" runat="server" Align="right">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>

                                                <cc1:Column DataField="closing" HeaderText="CLOSEING" runat="server" Align="right">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>

                                                <cc1:Column DataField="ClosingValue" HeaderText="CLOSEINGVALUE" runat="server" Align="right" Visible="false">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                            </Columns>

                                            <FilteringSettings MatchingType="AnyFilter" />
                                            <ScrollingSettings ScrollWidth="100%" ScrollHeight="290" />
                                        </cc1:Grid>
                                    </div>
                                    <div class="gridcontent" id="divSummeryData" runat="server">
                                        <cc1:Grid ID="gvRptProgressSummeryData" runat="server" CallbackMode="true" Serialize="true"
                                            FolderStyle="../GridStyles/premiere_blue" AutoGenerateColumns="false" PageSizeOptions="-1" PageSize="1000"
                                            EnableRecordHover="true" AllowAddingRecords="false" AllowPageSizeSelection="true"
                                            AllowSorting="false" ShowColumnsFooter="true" ShowEmptyDetails="false" AllowFiltering="true" FolderExports="resources/exports"
                                            OnExported="gvRptProgressSummeryData_Exported" OnExporting="gvRptProgressSummeryData_Exporting"
                                            OnRowDataBound="gvRptProgressSummeryData_RowDataBound">
                                            <ExportingSettings ExportColumnsFooter="true" ExportGroupFooter="true" ExportGroupHeader="true"
                                                FileName="ProgressReport" ExportAllPages="true" ExportDetails="true" AppendTimeStamp="false" />
                                            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size: 10px;background-color: #DCE3E8;color: #0000FF;"
                                                CSSExportCellStyle="font-weight: normal;font-size: 10px;color: #0000;background-color: AFEEEE" />
                                            <FilteringSettings InitialState="Visible" />
                                            <Columns>
                                                <cc1:Column DataField="SLNO" ReadOnly="true" HeaderText="SL.NO." runat="server" Visible="false" />

                                                <cc1:Column DataField="TYPE" HeaderText="TYPE" runat="server" Width="50px">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>

                                                <cc1:Column DataField="CATNAME" HeaderText="CATEGORY" runat="server" Width="160px">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column DataField="PRODUCTNAME" HeaderText="PRODUCT" runat="server" Width="300px">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>

                                                <cc1:Column DataField="OPENING" HeaderText="OPENING" runat="server" Width="200px" Align="right">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>

                                                <cc1:Column DataField="INWARDS_TOTAL" HeaderText="TOTAL INWARDS" runat="server" Align="right" Width="200px">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>

                                                <cc1:Column DataField="OUTWARD_TOTAL" HeaderText="TOTAL OUTWARD" runat="server" Align="right" Width="200px">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>

                                                <cc1:Column DataField="CLOSING" HeaderText="CLOSING" runat="server" Align="right" Width="200px">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                            </Columns>
                                            <FilteringSettings MatchingType="AnyFilter" />
                                            <ScrollingSettings ScrollWidth="100%" ScrollHeight="290" />
                                        </cc1:Grid>
                                    </div>

                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                    <span class="clear"></span>
                </div>
                <span class="clear"></span>
            </div>
            <script type="text/javascript">
                var searchTimeout = null;
                function FilterTextBox_KeyUp() {
                    searchTimeout = window.setTimeout(performSearch, 500);
                }

                function performSearch() {
                    var searchValue = document.getElementById('FilterTextBox').value;
                    if (searchValue == FilterTextBox.WatermarkText) {
                        searchValue = '';
                    }
                    gvRptProgress.addFilterCriteria('PRODUCTNAME', OboutGridFilterCriteria.Contains, searchValue);
                    gvRptProgress.addFilterCriteria('OPENING', OboutGridFilterCriteria.Contains, searchValue);
                    gvRptProgress.executeFilter();
                    searchTimeout = null;
                    return false;
                }
            </script>
            <script type="text/javascript">
                oboutGrid.prototype._resizeColumn = oboutGrid.prototype.resizeColumn;
                oboutGrid.prototype.resizeColumn = function (columnIndex, amountToResize, keepGridWidth) {
                    this._resizeColumn(columnIndex, amountToResize, false);
                    var width = this._getColumnWidth();
                    if (width > 0) {
                        if (this.ScrollWidth == '0px') {
                            this.GridMainContainer.style.width = width + 'px';
                        } else {
                            var scrollWidth = parseInt(this.ScrollWidth);
                            if (width < scrollWidth) {
                                this.GridMainContainer.style.width = width + 'px';
                                this.HorizontalScroller.style.display = 'none';
                            } else {
                                this.HorizontalScroller.firstChild.firstChild.style.width = width + 'px';
                                this.HorizontalScroller.style.display = '';
                            }
                        }
                    }
                }

                oboutGrid.prototype._getColumnWidth = function () {
                    var totalWidth = 0;
                    for (var i = 0; i < this.ColumnsCollection.length; i++) {
                        if (this.ColumnsCollection[i].Visible) {
                            totalWidth += this.ColumnsCollection[i].Width;
                        }
                    }

                    return totalWidth;
                }
            </script>
            <script type="text/javascript">
                function redirect() {
                    var url = "frmRptItemLedger.aspx";
                    window.location(url);
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
