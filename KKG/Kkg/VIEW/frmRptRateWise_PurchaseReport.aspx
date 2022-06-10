<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true"
    CodeFile="frmRptRateWise_PurchaseReport.aspx.cs" Inherits="VIEW_frmRptRateWise_PurchaseReport" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/bootstrap-multiselect.css" rel="stylesheet" />
    <script type="text/javascript" src="../js/bootstrap.js"></script>
    <script type="text/javascript" src="../js/bootstrap-multiselect.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
        $(function () {
            $('#ContentPlaceHolder1_ddldepot').multiselect({
                includeSelectAllOption: true
            });
            $("#ContentPlaceHolder1_ddldepot").multiselect('selectAll', false);
            $("#ContentPlaceHolder1_ddldepot").multiselect('updateButtonText');
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $('#ContentPlaceHolder1_ddlCategory').multiselect({
                includeSelectAllOption: true
            });
            $("#ContentPlaceHolder1_ddlCategory").multiselect('selectAll', false);
            $("#ContentPlaceHolder1_ddlCategory").multiselect('updateButtonText');
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $('#ContentPlaceHolder1_ddlBrand').multiselect({
                includeSelectAllOption: true
            });
            $("#ContentPlaceHolder1_ddlBrand").multiselect('selectAll', false);
            $("#ContentPlaceHolder1_ddlBrand").multiselect('updateButtonText');
        });

        $(function () {
            $('#ContentPlaceHolder1_ddlProduct').multiselect({
                includeSelectAllOption: true
            });
            $("#ContentPlaceHolder1_ddlProduct").multiselect('selectAll', false);
            $("#ContentPlaceHolder1_ddlProduct").multiselect('updateButtonText');
        });
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
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
            <%--  <asp:PostBackTrigger ControlID="btnshow"  />--%>
        </Triggers>
        <ContentTemplate>
            <div id="content">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active">
                                <span class="h_icon_he list_images"></span>
                                <h6>RATE WISE PURCHASE REPORT</h6>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="InputTable" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px;">
                                                <fieldset>
                                                    <legend>RATE WISE PURCHASE REPORT</legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td class="field_title" width="100">
                                                                <asp:Label ID="Label9" runat="server" Text="FROM DATE"></asp:Label>
                                                            </td>
                                                            <td class="field_input" width="150">
                                                                <asp:TextBox ID="txtfromdate" runat="server" Width="80" Enabled="false" placeholder="dd/mm/yyyy"
                                                                    MaxLength="10" ValidationGroup="ADD" onkeypress="return isNumberKeyWithslash(event);"></asp:TextBox>
                                                                <asp:ImageButton ID="imgPopup" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender4" PopupButtonID="imgPopup" runat="server"
                                                                    TargetControlID="txtfromdate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>
                                                            <td class="field_title" width="100">
                                                                <asp:Label ID="Label12" runat="server" Text="TO Date"></asp:Label>
                                                            </td>
                                                            <td class="field_input" width="150">
                                                                <asp:TextBox ID="txttodate" runat="server" Width="80" Enabled="false" placeholder="dd/mm/yyyy"
                                                                    MaxLength="10" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="ADD"></asp:TextBox>
                                                                <asp:ImageButton ID="Imgfrom" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="Imgfrom" runat="server"
                                                                    TargetControlID="txttodate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>
                                                            <td class="field_title" width="50">
                                                                <asp:Label ID="lbldepot" runat="server" Text="Depot"> </asp:Label>
                                                            </td>
                                                            <td class="field_input" width="80">
                                                                <asp:ListBox ID="ddldepot" runat="server" SelectionMode="Multiple" TabIndex="4"
                                                                    Width="200%" AppendDataBoundItems="True" ValidationGroup="ADD" name="options[]"
                                                                    multiple="multiple"></asp:ListBox>
                                                            </td>

                                                            <td class="field_title" width="50">
                                                                <asp:Label ID="Label3" runat="server" Text="Vendor"> </asp:Label>
                                                            </td>
                                                            <td class="field_input" width="80">
                                                                <asp:DropDownList ID="ddlVendor" runat="server" AppendDataBoundItems="true"
                                                                    Width="250" Height="28" class="chosen-select" data-placeholder="Choose" ValidationGroup="ADD">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvddlVendor" runat="server" ControlToValidate="ddlVendor"
                                                                    ValidationGroup="ADD" ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!"
                                                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field_title">
                                                                <asp:Label ID="lblBrand" Text="Primary Item" runat="server"></asp:Label>&nbsp;<span class="req">*</span>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:ListBox ID="ddlBrand" Width="200" runat="server" SelectionMode="Multiple"
                                                                    data-placeholder="Choose Primary Item" ValidationGroup="ADD" AutoPostBack="True" AppendDataBoundItems="True"
                                                                    OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged"></asp:ListBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlBrand"
                                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="ADD"
                                                                    ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                                            </td>

                                                            <td class="field_title">
                                                                <asp:Label ID="lblCategory" Text="Sub Item" runat="server"></asp:Label>&nbsp;<span
                                                                    class="req">*</span>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:ListBox ID="ddlCategory" Width="210" runat="server" SelectionMode="Multiple"
                                                                    data-placeholder="Choose Sub Item" ValidationGroup="ADD" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"></asp:ListBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorCategory" runat="server" ControlToValidate="ddlCategory"
                                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="ADD"
                                                                    ForeColor="Red" InitialValue="0">
                                                                </asp:RequiredFieldValidator>
                                                            </td>

                                                            <td class="field_title" width="50">
                                                                <asp:Label ID="Label7" runat="server" Text="Product"></asp:Label>
                                                            </td>
                                                            <td class="field_input" width="80">
                                                                <asp:ListBox ID="ddlProduct" runat="server" SelectionMode="Multiple" data-placeholder="Choose Item"
                                                                    AppendDataBoundItems="true" ValidationGroup="ADD"></asp:ListBox>
                                                                <asp:RequiredFieldValidator ID="rfvddlProduct" runat="server" ControlToValidate="ddlProduct"
                                                                    ValidationGroup="ADD" ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!"
                                                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </td>

                                                            <td class="field_title" width="150">
                                                                <asp:Label ID="Label4" runat="server" Text="Report Type"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:DropDownList ID="ddlReportType" Width="180" runat="server" class="chosen-select"
                                                                    data-placeholder="">
                                                                    <asp:ListItem Text="Vendor Wise" Value="0"></asp:ListItem>
                                                                    <asp:ListItem Text="Item wise & Vendor wise" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="Item wise" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="Item wise Details" Value="3"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>

                                                        <tr>

                                                            <td width="220" colspan="4" class="field_input">
                                                                <div class="btn_24_blue">
                                                                    <span class="icon exclamation_co"></span>
                                                                    <asp:Button ID="btnshow" runat="server" Text="Show" CssClass="btn_link" ValidationGroup="ADD"
                                                                        OnClick="btnshow_Click" />
                                                                </div>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                                <div class="btn_24_blue">
                                                                    <span class="icon doc_excel_table_co"></span><a href="#" onclick="exportToExcel();">
                                                                        <asp:Button ID="btnExport" runat="server" Text="Export Excel" CssClass="btn_link"
                                                                            OnClick="ExportToExcel" CausesValidation="false" /></a>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="gridcontent-inner" id="divProductDetails" runat="server">
                                        <fieldset>
                                            <legend>Details</legend>
                                            <div style="overflow: hidden;" id="DivHeaderRow">
                                            </div>
                                            <div id="DivMainContent" style="overflow: scroll; height: 300px;" onscroll="OnScrollDiv(this);">
                                                <asp:GridView ID="grdRatewisePurchase" EmptyDataText="There are no records available." OnRowDataBound="grdRatewisePurchase_RowDataBound"
                                                    CssClass="zebra" runat="server" AutoGenerateColumns="true" Width="100%" ShowFooter="false">
                                                </asp:GridView>
                                                <div class="clear">
                                                </div>
                                            </div>
                                            <div id="DivFooterRow" style="overflow: hidden;">
                                            </div>
                                        </fieldset>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function exportToExcel() {
            grdstock.exportToExcel();
        }
    </script>
</asp:Content>