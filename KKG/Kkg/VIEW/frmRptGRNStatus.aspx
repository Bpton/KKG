<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmRptGRNStatus.aspx.cs" Inherits="VIEW_frmRptGRNStatus" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../js/jquery.blockUI.js" type="text/javascript"></script>
    <link href="../css/bootstrap-multiselect.css" rel="stylesheet" />
    <script type="text/javascript" src="../js/bootstrap.js"></script>
    <script type="text/javascript" src="../js/bootstrap-multiselect.js"></script>
    <script type="text/javascript">
        function BlockUI(elementID) {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_beginRequest(function () {
                $("#" + elementID).block({
                    message: '<table align = "center"><tr><td>' +
                        '<img src="../images/loading123.gif"/></td></tr></table>',
                    css: {},
                    overlayCSS: {
                        background: 'transparent'
                    }
                });
            });
            prm.add_endRequest(function () {
                $("#" + elementID).unblock();
            });
        }
        $(document).ready(function () {

            BlockUI("<%=pnlAddEdit.ClientID %>");
            $.blockUI.defaults.css = {};
        });

    </script>
    <script type="text/javascript">
        window.onload = function () {
            oboutGrid.prototype.selectRecordByClick = function () {
                return;
            }
            oboutGrid.prototype.showSelectionArea = function () {
                return;
            }
        }

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if ((charCode < 48 || charCode > 57) && (charCode != 8))
                return false;

            return true;
        }

        function isNumberKeyWithDot(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if ((charCode < 48 || charCode > 57) && (charCode != 8) && (charCode != 46))
                return false;

            return true;
        }

        function isNumberKeyWithDotMinus(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if ((charCode < 48 || charCode > 57) && (charCode != 8) && (charCode != 46) && (charCode != 45))
                return false;

            return true;
        }

        function isNumberKeyWithslash(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if ((charCode < 48 || charCode > 57) && (charCode != 8) && (charCode != 47))
                return false;

            return true;
        }

        function TabchangeTPU() {
            elem = document.getElementById('<%= ddlpo.ClientID %>');
            elem.focus();
        }

        function TabchangePO() {
            elem = document.getElementById('<%= ddlproduct.ClientID %>');
            elem.focus();
        }

        function TabchangePackSize() {
            elem = document.getElementById('<%= txtDespatchQty.ClientID %>');
            elem.focus();
        }


    </script>

    

    <script type="text/javascript">
        function exportToExcel() {
            grdDespatchHeader.exportToExcel();
        }
    </script>

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

        var searchTimeout = null;
        function FilterTextBox_KeyUp() {
            searchTimeout = window.setTimeout(performSearch, 500);
        }

        function performSearch() {
            var searchValue = document.getElementById('FilterTextBox').value;
            if (searchValue == FilterTextBox.WatermarkText) {
                searchValue = '';
            }
            grdDespatchHeader.addFilterCriteria('STOCKDESPATCHNO', OboutGridFilterCriteria.Contains, searchValue);
            grdDespatchHeader.addFilterCriteria('DESPATCHDATE', OboutGridFilterCriteria.Contains, searchValue);
            grdDespatchHeader.addFilterCriteria('WAYBILLNO', OboutGridFilterCriteria.Contains, searchValue);
            grdDespatchHeader.addFilterCriteria('INVOICENO', OboutGridFilterCriteria.Contains, searchValue);
            grdDespatchHeader.addFilterCriteria('VEHICHLENO', OboutGridFilterCriteria.Contains, searchValue);
            grdDespatchHeader.addFilterCriteria('LRGRNO', OboutGridFilterCriteria.Contains, searchValue);
            grdDespatchHeader.addFilterCriteria('TRANSPORTERNAME', OboutGridFilterCriteria.Contains, searchValue);
            grdDespatchHeader.addFilterCriteria('MOTHERDEPOTNAME', OboutGridFilterCriteria.Contains, searchValue);
            grdDespatchHeader.executeFilter();
            searchTimeout = null;
            return false;
        }

        function OnBeforeDespatchDelete(record) {
            record.Error = '';
            document.getElementById("<%=hdnDespatchID.ClientID %>").value = record.STOCKDESPATCHID;
            if (confirm("Are you sure you want to delete? "))
                return true;
            else
                return false;
        }
        function OnDeleteDespatchDetails(record) {
            alert(record.Error);
        }
        function CallServerViewMethod(oLink) {
            var iRecordIndex = oLink.id.toString().replace("btnGridEdit_", "");
            document.getElementById("<%=hdnDespatchID.ClientID %>").value = grdDespatchHeader.Rows[iRecordIndex].Cells[1].Value;
            document.getElementById("<%=hdnReceivedID.ClientID %>").value = grdDespatchHeader.Rows[iRecordIndex].Cells[2].Value;
            document.getElementById("<%=btnview.ClientID %>").click();
        }

        function CallServerMethodPrint(oLink) {
            var iRecordIndex = oLink.id.toString().replace("btnPrint_", "");
            document.getElementById("<%=hdnDespatchID.ClientID %>").value = grdDespatchHeader.Rows[iRecordIndex].Cells[1].Value;
            document.getElementById("<%=hdnReceivedID.ClientID %>").value = grdDespatchHeader.Rows[iRecordIndex].Cells[2].Value;
            document.getElementById("<%=btnPrint.ClientID %>").click();
        }

    </script>
    <script type="text/javascript">
        $(function () {
            $('#ContentPlaceHolder1_ddlinvtype').multiselect({
                includeSelectAllOption: true
            });
            $("#ContentPlaceHolder1_ddlinvtype").multiselect('selectAll', false);
            $("#ContentPlaceHolder1_ddlinvtype").multiselect('updateButtonText');
        });
    </script>
    <script type="text/javascript">
        function Check() {
            var chkstatus = document.getElementById('<%=chkActive.ClientID %>');
            if (chkstatus.checked) {
                document.getElementById('<%=lbltext.ClientID %>').innerHTML = "Yes!";
                document.getElementById('<%=lbltext.ClientID %>').style.color = 'green';
            } else {
                document.getElementById('<%=lbltext.ClientID %>').innerHTML = "No!";
                document.getElementById('<%=lbltext.ClientID %>').style.color = 'red';
            }
        }
    </script>
    <script type="text/javascript">
        function disableautocompletion(id) {
            var TextBoxControl = document.getElementById(id);
            TextBoxControl.setAttribute("autocomplete", "off");
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <%-- <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>--%>
        <ContentTemplate>
            <div id="content">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>
                                    <asp:Label ID="lblpagename" runat="server"></asp:Label>
                                </h6>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px">
                                                <fieldset>
                                                    <legend>GRN Entry Details</legend>
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tr runat="server" id="trAutoDespatchNo">
                                                            <td class="field_title">
                                                                <asp:Label ID="lblStockRcvdNo" Text="GRN No." runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input" colspan="5">
                                                                <asp:TextBox ID="txtDespatchNo" runat="server" Width="270" placeholder="Auto Generate No."
                                                                    Enabled="false" Font-Bold="true"> </asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="165" class="field_title">
                                                                <asp:Label ID="Label2" Text="GRN Entry Date" runat="server"></asp:Label>&nbsp;<span
                                                                    class="req">*</span>
                                                            </td>
                                                            <td width="165" class="field_input">
                                                                <asp:TextBox ID="txtDespatchDate" runat="server" Enabled="false" Width="120" placeholder="dd/MM/yyyy"
                                                                    MaxLength="10" ValidationGroup="Save"></asp:TextBox>
                                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/calendar.png"
                                                                    Width="24px" Height="24px" ImageAlign="AbsMiddle" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorDespatchDate" runat="server"
                                                                    ControlToValidate="txtDespatchDate" ValidateEmptyText="false" SetFocusOnError="true"
                                                                    ErrorMessage="Required!" ValidationGroup="Save" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtenderDespatchDate" TargetControlID="txtDespatchDate"
                                                                    PopupButtonID="ImageButton1" runat="server" Format="dd/MM/yyyy" Enabled="true"
                                                                    BehaviorID="CalendarExtenderDespatchDate" CssClass="cal_Theme1" />
                                                            </td>
                                                            <td width="135" class="field_title">
                                                                <asp:Label ID="lblTPU" Text="Vendor" runat="server"></asp:Label>&nbsp;<span class="req">*</span>
                                                            </td>
                                                            <td width="160" class="field_input">
                                                                <asp:DropDownList ID="ddlTPU" runat="server" class="chosen-select" AutoPostBack="true"
                                                                    data-placeholder="Select TPU" AppendDataBoundItems="True" Width="180px" ValidationGroup="Save"
                                                                    OnSelectedIndexChanged="ddlTPU_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="CV_ddlTPU" runat="server" ControlToValidate="ddlTPU"
                                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="A"
                                                                    ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                                                <asp:HiddenField ID="hdnSuppliedItem" runat="server" />
                                                            </td>
                                                            <td width="135" class="field_title">
                                                                <asp:Label ID="lblTransporter" Text="Transporter" runat="server"></asp:Label>&nbsp;<span
                                                                    class="req">*</span>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:DropDownList ID="ddlTransporter" Width="180" runat="server" class="chosen-select"
                                                                    data-placeholder="Choose Transporter" AppendDataBoundItems="True" ValidationGroup="Save">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorTransporter" runat="server"
                                                                    ControlToValidate="ddlTransporter" ValidateEmptyText="false" SetFocusOnError="true"
                                                                    ErrorMessage="Required!" ValidationGroup="Save" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field_title">
                                                                <asp:Label ID="lblTransportMode" Text="Mode of Transport" runat="server"></asp:Label>&nbsp;<span
                                                                    class="req">*</span>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:DropDownList ID="ddlTransportMode" Width="150" runat="server" class="chosen-select"
                                                                    data-placeholder="Choose Transport Mode" ValidationGroup="Save">
                                                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                                    <asp:ListItem Text="By Road" Value="By Road"></asp:ListItem>
                                                                    <asp:ListItem Text="By Rail" Value="By Rail"></asp:ListItem>
                                                                    <asp:ListItem Text="By Air" Value="By Air"></asp:ListItem>
                                                                    <asp:ListItem Text="By Ship" Value="By Ship"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorTransportMode" runat="server"
                                                                    ControlToValidate="ddlTransportMode" ValidateEmptyText="false" SetFocusOnError="true"
                                                                    ErrorMessage="Required!" ValidationGroup="Save" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td class="field_title">
                                                                <asp:Label ID="lblVehicle" Text="Vehicle No." runat="server"></asp:Label>&nbsp;<span
                                                                    class="req">*</span>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtVehicle" runat="server" CssClass="x_large" placeholder="Vehicle No" TextMode="MultiLine"
                                                                    ValidationGroup="Save" AutoCompleteType="Disabled" onfocus="disableautocompletion(this.id);"> </asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorVehicle" runat="server" ControlToValidate="txtVehicle"
                                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="Save"
                                                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td class="field_title" style="display: none;">
                                                                <asp:Label ID="Label14" Text="Mother Depot/Depot" runat="server"></asp:Label>&nbsp;<span
                                                                    class="req">*</span>
                                                            </td>
                                                            <td class="field_input" style="display: none;">
                                                                <asp:DropDownList ID="ddlDepot" Width="180" runat="server" Enabled="false" class="chosen-select"
                                                                    data-placeholder="Choose Depot" AppendDataBoundItems="True" ValidationGroup="Save">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorDepot" runat="server" ControlToValidate="ddlDepot"
                                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="Save"
                                                                    ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlDepot"
                                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="Save"
                                                                    ForeColor="Red" InitialValue="-1"></asp:RequiredFieldValidator>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlDepot"
                                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="Save"
                                                                    ForeColor="Red" InitialValue="-3"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field_title">
                                                                <asp:Label ID="lblLRGRNo" Text="LR/GR No" runat="server"></asp:Label>&nbsp;<span
                                                                    class="req">*</span>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtLRGRNo" runat="server" CssClass="x_large" placeholder="LR/GR No"
                                                                    ValidationGroup="Save" AutoCompleteType="Disabled" onfocus="disableautocompletion(this.id);"> </asp:TextBox>
                                                            </td>
                                                            <td class="field_title">
                                                                <asp:Label ID="lblLRGRDate" Text="LR/GR Date" runat="server"></asp:Label>&nbsp;<span
                                                                    class="req">*</span>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtLRGRDate" runat="server" Enabled="false" Width="120" placeholder="dd/MM/yyyy"
                                                                    MaxLength="10" ValidationGroup="Save"></asp:TextBox>
                                                                <asp:ImageButton ID="imgbtnLRGRCalendar" runat="server" ImageUrl="~/images/calendar.png"
                                                                    Width="24px" Height="24px" ImageAlign="AbsMiddle" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorLRGRDate" runat="server" ControlToValidate="txtLRGRDate"
                                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="Save"
                                                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtenderLRGRDate" TargetControlID="txtLRGRDate"
                                                                    PopupButtonID="imgbtnLRGRCalendar" runat="server" Format="dd/MM/yyyy" Enabled="true"
                                                                    BehaviorID="CalendarExtenderLRGRDate" CssClass="cal_Theme1" />
                                                            </td>
                                                            <td class="field_title">
                                                                <asp:Label ID="lblWayBill" Text="WayBill Key" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:DropDownList ID="ddlWaybill" Width="180" runat="server" class="chosen-select"
                                                                    data-placeholder="Select Waybill Key" AppendDataBoundItems="True">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field_title">
                                                                <asp:Label ID="lblInvoiceNo" Text="Invoice No" runat="server"></asp:Label>&nbsp;<span
                                                                    class="req">*</span>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtInvoiceNo" runat="server" CssClass="x_large" placeholder="Invoice No"
                                                                    ValidationGroup="Save" AutoCompleteType="Disabled" onfocus="disableautocompletion(this.id);"> </asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorInvoiceNo" runat="server" ControlToValidate="txtInvoiceNo"
                                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="Save"
                                                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td class="field_title">
                                                                <asp:Label ID="lblInvoiceDate" Text="Invoice Date" runat="server"></asp:Label>&nbsp;<span
                                                                    class="req">*</span>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtInvoiceDate" runat="server" Width="120" placeholder="dd/MM/yyyy"
                                                                    Enabled="false" MaxLength="10" ValidationGroup="Save"> </asp:TextBox>
                                                                <asp:ImageButton ID="imgbtnInvoiceCalendar" runat="server" ImageUrl="~/images/calendar.png"
                                                                    Width="24px" Height="24px" ImageAlign="AbsMiddle" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorInvoiceDate" runat="server"
                                                                    ControlToValidate="txtInvoiceDate" ValidateEmptyText="false" SetFocusOnError="true"
                                                                    ErrorMessage="Required!" ValidationGroup="Save" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtenderInvoiceDate" TargetControlID="txtInvoiceDate"
                                                                    PopupButtonID="imgbtnInvoiceCalendar" runat="server" Format="dd/MM/yyyy" Enabled="true"
                                                                    BehaviorID="CalendarExtenderInvoiceDate" CssClass="cal_Theme1" />
                                                            </td>
                                                            <td class="field_title">
                                                                <asp:Label ID="lblgatepassno" Text="GatePass No." runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtgatepassno" runat="server" placeholder="Enter GatePass No" AutoCompleteType="Disabled"
                                                                    onfocus="disableautocompletion(this.id);" Width="175"> </asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field_title">
                                                                <asp:Label ID="lblgatepassdate" Text="GatePass Date" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtgatepassdate" runat="server" Width="120" placeholder="dd/MM/yyyy"
                                                                    Enabled="false" MaxLength="10"> </asp:TextBox>
                                                                <asp:ImageButton ID="Imgbtngatepass" runat="server" ImageUrl="~/images/calendar.png"
                                                                    Width="24px" Height="24px" ImageAlign="AbsMiddle" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtgatepassdate"
                                                                    PopupButtonID="Imgbtngatepass" runat="server" Format="dd/MM/yyyy" Enabled="true"
                                                                    BehaviorID="CalendarExtender2" CssClass="cal_Theme1" />
                                                            </td>
                                                            <td class="field_title" style="display: none">
                                                                <asp:Label ID="lblinsurancecompname" Text="INSURANCE COMPANY" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input" style="display: none">
                                                                <asp:DropDownList ID="ddlinsurancecompname" Width="180" runat="server" class="chosen-select"
                                                                    data-placeholder="" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlinsurancecompname_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td class="field_title" style="display: none">
                                                                <asp:Label ID="Label32" Text="INSURANCE NUMBER" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input" style="display: none">
                                                                <asp:DropDownList ID="ddlInsuranceNumber" Width="180" runat="server" class="chosen-select"
                                                                    data-placeholder="" AppendDataBoundItems="True">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field_title" style="display:none">
                                                                <asp:Label ID="Label11" Text="C Form" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input" style="display:none">
                                                                <asp:CheckBox ID="chkActive" runat="server" Text=" " onchange="Check()" />&nbsp<asp:Label
                                                                    ID="lbltext" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_title">
                                                                <asp:Label ID="lblInrRate" runat="server" ForeColor="Red"></asp:Label>
                                                            </td>
                                                            <td class="field_title">
                                                                <asp:Label ID="lblExchangeRate" runat="server" ForeColor="Red"></asp:Label>
                                                            </td>
                                                            <td class="field_title">&nbsp;
                                                            </td>
                                                            <td class="field_input">&nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                        <tr id="trpodetails" runat="server">
                                            <td class="field_input" style="padding-left: 10px">
                                                <fieldset>
                                                    <legend id="LgndPoDetails" runat="server">Purchase Order Details</legend>
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td width="110" class="field_title">
                                                                <asp:Label ID="Label1" Text="PO" runat="server"></asp:Label>&nbsp;<span class="req">*</span>
                                                            </td>
                                                            <td width="190" class="field_input">
                                                                <asp:DropDownList ID="ddlpo" runat="server" class="chosen-select" data-placeholder="Select PO"
                                                                    AppendDataBoundItems="True" AutoPostBack="true" Width="350px" ValidationGroup="A"
                                                                    OnSelectedIndexChanged="ddlpo_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="CV_ddlpo" runat="server" ControlToValidate="ddlpo"
                                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="A"
                                                                    ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td class="field_title">&nbsp;
                                                            </td>
                                                            <td class="field_input">&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field_title">
                                                                <asp:Label ID="lblPO" Text="Material" runat="server"></asp:Label>&nbsp;<span class="req">*</span>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:Label ID="lblproductid" runat="server" Text="" Visible="false"></asp:Label>
                                                                <asp:Label ID="lblproductname" runat="server" Text="" Visible="false"></asp:Label>
                                                                <asp:DropDownList ID="ddlproduct" runat="server" Height="24px" Width="650px" AutoPostBack="true"
                                                                    ValidationGroup="A" AppendDataBoundItems="True" CssClass="common-select" OnSelectedIndexChanged="ddlproduct_SelectedIndexChanged"
                                                                    Style="font-family: 'Courier New', Courier, monospace">
                                                                    <asp:ListItem Text="-- SELECT PO NO --" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="CV_ddlproduct" runat="server" ControlToValidate="ddlproduct"
                                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="A"
                                                                    ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td class="field_title">
                                                                <asp:Label ID="Label3" Text="Batch No" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox runat="server" ID="txtBatch" CssClass="large" placeholder="Batch No"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr id="podate" runat="server" visible="false">
                                                            <td class="field_title">
                                                                <asp:Label ID="lblPoDate" Text="PO Date" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input" colspan="3">
                                                                <asp:TextBox runat="server" ID="txtPoDate" Enabled="false" CssClass="mid" placeholder="Purchase Order Date"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4">
                                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                                    <tr>
                                                                        <td width="80" class="field_title">
                                                                            <asp:Label ID="Label4" Text="Unit" runat="server"></asp:Label>&nbsp;<span class="req">*</span>
                                                                        </td>
                                                                        <td class="field_input">
                                                                            <asp:DropDownList ID="ddlPackSize" runat="server" class="chosen-select" data-placeholder="Select Packing Size"
                                                                                AppendDataBoundItems="True" Width="180px" ValidationGroup="A">
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="CV_ddlPackSize" runat="server" ControlToValidate="ddlPackSize"
                                                                                ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="A"
                                                                                ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                                                        </td>
                                                                        <td width="90" class="field_title">
                                                                            <asp:Label ID="Label6" Text="Mfg.Date" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td width="150" class="field_input">
                                                                            <asp:TextBox runat="server" ID="txtMfDate" Width="110" placeholder="DD/MM/YYYY" MaxLength="10"
                                                                                onkeypress="return isNumberKeyWithslash(event);"></asp:TextBox>
                                                                            <asp:ImageButton ID="ImageButtonMFDate" runat="server" ImageUrl="~/images/calendar.png"
                                                                                Width="24px" Height="24px" ImageAlign="AbsMiddle" />
                                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtenderMFDate" TargetControlID="txtMfDate"
                                                                                PopupButtonID="ImageButtonMFDate" runat="server" Format="dd/MM/yyyy" Enabled="true"
                                                                                BehaviorID="CalendarExtenderMFDate" CssClass="cal_Theme1" />
                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorMFDate" runat="server"
                                                                                ForeColor="Red" ControlToValidate="txtMfDate" ValidationGroup="A" ErrorMessage="Invalid"
                                                                                ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$"></asp:RegularExpressionValidator>
                                                                        </td>
                                                                        <td width="90" class="field_title">
                                                                            <asp:Label ID="Label8" Text="Exp.Date" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td width="150" class="field_input">
                                                                            <asp:TextBox runat="server" ID="txtExprDate" Width="110" placeholder="DD/MM/YYYY"
                                                                                MaxLength="10" onkeypress="return isNumberKeyWithslash(event);"></asp:TextBox>
                                                                            <asp:ImageButton ID="ImageButtonExprDate" runat="server" ImageUrl="~/images/calendar.png"
                                                                                Width="24px" Height="24px" ImageAlign="AbsMiddle" />
                                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtenderExprDate" TargetControlID="txtExprDate"
                                                                                PopupButtonID="ImageButtonExprDate" runat="server" Format="dd/MM/yyyy" Enabled="true"
                                                                                BehaviorID="CalendarExtenderExprDate" CssClass="cal_Theme1" />
                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorExprDate" runat="server"
                                                                                ForeColor="Red" ControlToValidate="txtExprDate" ValidationGroup="A" ErrorMessage="Invalid"
                                                                                ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$"></asp:RegularExpressionValidator>
                                                                        </td>
                                                                        <td width="70" class="field_title">
                                                                            <asp:Label ID="Label15" Text="MRP" runat="server" Visible="false"></asp:Label>
                                                                        </td>
                                                                        <td width="150" class="field_input">
                                                                            <asp:TextBox runat="server" ID="txtMRP" Width="145" placeholder="MRP" Visible="false"
                                                                                Enabled="false"></asp:TextBox>
                                                                        </td>
                                                                        <td width="140">&nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="50" class="field_title">
                                                                            <asp:Label ID="Label7" Text="Rate" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td width="150" class="field_input">
                                                                            <asp:TextBox runat="server" ID="txtRate" Width="145" placeholder="Rate" Enabled="false"></asp:TextBox>
                                                                        </td>
                                                                        <td width="90" class="field_title">
                                                                            <asp:Label ID="lblPOQTY" Text="PO Qty" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td class="field_input">
                                                                            <asp:TextBox runat="server" ID="txtPoQty" Width="145" placeholder="PO Qty" Enabled="false"></asp:TextBox>
                                                                        </td>
                                                                        <td width="60" class="field_title">
                                                                            <asp:Label ID="Label13" runat="server" Text="Received Qty"></asp:Label>
                                                                        </td>
                                                                        <td class="field_input">
                                                                            <asp:TextBox runat="server" ID="txtAlreadyDespatchQty" Width="145" placeholder="Already Receive Qty"
                                                                                Enabled="false"></asp:TextBox>
                                                                            <asp:HiddenField runat="server" ID="hdn_Remaining" />
                                                                            <asp:HiddenField runat="server" ID="hdn_CurrentQty" />
                                                                        </td>
                                                                        <td class="field_title">
                                                                            <asp:Label ID="Label9" Text="Remaining Qty" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td class="field_input">
                                                                            <asp:TextBox runat="server" ID="txtQcQty" Width="145" placeholder="Remaining Qty"
                                                                                Enabled="false"></asp:TextBox>
                                                                            <asp:HiddenField runat="server" ID="hdn_PackSizeQC" />
                                                                        </td>
                                                                        <td width="50" class="field_title">
                                                                            <asp:Label ID="Label12" Text="Allocated&nbsp;Qty" runat="server" Visible="false"></asp:Label>
                                                                        </td>
                                                                        <td class="field_input">
                                                                            <asp:TextBox runat="server" ID="txtAllocatedQty" Width="145" placeholder="Allocated Qty"
                                                                                Enabled="false" Visible="false"></asp:TextBox>
                                                                        </td>
                                                                        <td>&nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="field_title">
                                                                            <asp:Label ID="Label10" Text="Receive Qty." runat="server"></asp:Label>&nbsp;<span
                                                                                class="req">*</span>
                                                                        </td>
                                                                        <td class="field_input">
                                                                            <asp:TextBox runat="server" ID="txtDespatchQty" Width="145" placeholder="Enter Despatch Qty"
                                                                                onkeypress="return isNumberKey(event);" ValidationGroup="A" AutoCompleteType="Disabled"
                                                                                onfocus="disableautocompletion(this.id);"></asp:TextBox>
                                                                        </td>
                                                                        <td class="field_title">
                                                                            <asp:Label ID="Label29" runat="server" Text="Stock Qty" Visible="false"></asp:Label>
                                                                        </td>
                                                                        <td class="field_input">
                                                                            <asp:TextBox runat="server" ID="txtTotDespatch" Width="145" placeholder="Stock Qty"
                                                                                Enabled="false" Visible="false"></asp:TextBox>
                                                                        </td>
                                                                        <td class="field_input">
                                                                            <asp:TextBox runat="server" ID="txtWeight" Width="145" placeholder="Weight" Enabled="false"
                                                                                Visible="false"></asp:TextBox>
                                                                        </td>
                                                                        <td class="field_title">
                                                                            <asp:Label ID="Label18" Text="Gross Weight" runat="server" Visible="false"></asp:Label>&nbsp;
                                                                            <asp:Label ID="Label17" Text="Assesment(%)" runat="server" Visible="false"></asp:Label>
                                                                        </td>
                                                                        <td class="field_input">
                                                                            <asp:TextBox runat="server" ID="txtAssementPercentage" Width="145" placeholder="Assesment(%)"
                                                                                Enabled="false" Visible="false"></asp:TextBox>&nbsp;
                                                                            <asp:TextBox runat="server" ID="txtTotalAssement" Width="145" placeholder="Gross Weight"
                                                                                Enabled="false" Visible="false"></asp:TextBox>
                                                                        </td>
                                                                        <td>&nbsp;
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="gridcontent-inner">
                                        <fieldset>
                                            <legend>Product Details</legend>
                                            <div style="overflow: hidden; width: 100%;" id="DivHeaderRow">
                                            </div>
                                            <div style="overflow: scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent">
                                                <asp:GridView ID="grdAddDespatch" runat="server" Width="100%" CssClass="zebra" AutoGenerateColumns="true"
                                                    EmptyDataText="No Records Available" OnRowDataBound="grdAddDespatch_OnRowDataBound"
                                                    ShowFooter="true" DataKeyNames="GUID,PRODUCTID">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="DELETE">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btn_TempDelete" runat="server" class="action-icons c-delete"
                                                                    OnClientClick="return confirm('Are you sure you want to Delete?')" ToolTip="Delete" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="REJECTION" HeaderStyle-ForeColor="Red">
                                                            <ItemTemplate>
                                                                <nav style="height: 5px; z-index: 2; position: relative; text-align: center;">
                                                                    <asp:Label ID="lblrejvalue" runat="server" class="badge red">0</asp:Label></nav>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div id="DivFooterRow" style="overflow: hidden">
                                            </div>
                                            <asp:HiddenField ID="hdndtDespatchDelete" runat="server" />
                                            <asp:HiddenField ID="hdndtPOIDDelete" runat="server" />
                                            <asp:HiddenField ID="hdndtPRODUCTIDDelete" runat="server" />
                                            <asp:HiddenField ID="hdndtBATCHDelete" runat="server" />
                                            <asp:HiddenField ID="hdnViewMode" runat="server" />
                                        </fieldset>
                                    </div>
                                    <div class="gridcontent-inner" style="display: none;">
                                        <fieldset>
                                            <legend>Product TAX Details</legend>
                                            <div style="overflow: hidden; width: 100%; display: none;" id="Tax_DivHeaderRow">
                                            </div>
                                            <div style="overflow: scroll; display: none;" onscroll="Tax_OnScrollDiv(this)" id="Tax_DivMainContent">
                                                <asp:GridView ID="gvProductTax" runat="server" Width="100%" CssClass="zebra" Visible="false"
                                                    AutoGenerateColumns="false" EmptyDataText="No Records Available" OnRowDataBound="gvProductTax_OnRowDataBound"
                                                    ShowFooter="true">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="PRODUCT">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblproduct" runat="server" Text='<%# Eval("PRODUCTNAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="BATCH">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBatch" runat="server" Text='<%# Eval("BATCHNO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TAX NAME">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTAXName" runat="server" Text='<%# Eval("TAXNAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TAX(%)">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblpercentage" runat="server" Text='<%# Eval("PERCENTAGE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="VALUE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblvalue" runat="server" Text='<%# Eval("TAXVALUE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div id="Tax_DivFooterRow" style="overflow: hidden; display: none;">
                                            </div>
                                        </fieldset>
                                    </div>
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px" colspan="2">
                                                <fieldset>
                                                    <legend>Item Wise Amount And Tax</legend>
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td width="95" class="innerfield_title">
                                                                <asp:Label ID="Label21" Text="Basic Value" runat="server"></asp:Label>
                                                            </td>
                                                            <td width="139">
                                                                <asp:TextBox runat="server" ID="txtAmount" Width="126" placeholder="Tot.Basic&nbsp;Value"
                                                                    Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td width="90" class="innerfield_title">
                                                                <asp:Label ID="Label311" Text="MRP Value" runat="server"></asp:Label>
                                                            </td>
                                                            <td width="139">
                                                                <asp:TextBox runat="server" ID="txtTotMRP" Width="126" placeholder="Tot.MRP Value"
                                                                    Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td width="90" class="innerfield_title">
                                                                <asp:Label ID="Label22" Text="Tax Value" runat="server"></asp:Label>
                                                            </td>
                                                            <td width="139">
                                                                <asp:TextBox runat="server" ID="txtTotTax" Width="126" placeholder="Tot. Tax Value"
                                                                    Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td width="145" class="innerfield_title">
                                                                <asp:Label ID="Label23" Text="Basic&nbsp;Value&nbsp;+&nbsp;Tax&nbsp;Value" runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox runat="server" ID="txtNetAmt" Width="126" placeholder="Basic&nbsp;+&nbsp;Tax&nbsp;Value"
                                                                    Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                        <tr style="display: none;">
                                            <td class="field_input" style="padding-left: 10px">
                                                <table width="90%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td>
                                                            <fieldset>
                                                                <legend>Terms & Condition</legend>
                                                                <div class="gridcontent-shortstock">
                                                                    <cc1:Grid ID="grdTerms" runat="server" CallbackMode="true" Serialize="true" AutoGenerateColumns="false"
                                                                        FolderStyle="../GridStyles/premiere_blue" AllowAddingRecords="false" AllowFiltering="true"
                                                                        AllowSorting="false" OnRowDataBound="grdTerms_RowDataBound" AllowPageSizeSelection="false"
                                                                        AllowPaging="false" PageSize="20">
                                                                        <FilteringSettings InitialState="Visible" />
                                                                        <Columns>
                                                                            <cc1:Column ID="Column3" DataField="SLNO" HeaderText="Sl No" runat="server" Width="60">
                                                                                <TemplateSettings TemplateId="tplTermsNumbering" />
                                                                            </cc1:Column>
                                                                            <cc1:Column ID="Column10" DataField="DESCRIPTION" ReadOnly="true" HeaderText="TERMS & Condition"
                                                                                runat="server" Width="250" Wrap="true" />
                                                                            <cc1:Column ID="Column5" DataField="ID" ReadOnly="true" HeaderText="TERMSID" runat="server"
                                                                                Width="60">
                                                                                <TemplateSettings TemplateId="CheckTemplateTERMS" HeaderTemplateId="HeaderTemplateTERMS" />
                                                                            </cc1:Column>
                                                                        </Columns>
                                                                        <FilteringSettings MatchingType="AnyFilter" />
                                                                        <Templates>
                                                                            <cc1:GridTemplate runat="server" ID="tplTermsNumbering">
                                                                                <Template>
                                                                                    <asp:Label ID="lblslnoTax" runat="server" Text='<%#Container.PageRecordIndex+1 %>'></asp:Label>
                                                                                </Template>
                                                                            </cc1:GridTemplate>
                                                                            <cc1:GridTemplate runat="server" ID="CheckTemplateTERMS">
                                                                                <Template>
                                                                                    <asp:HiddenField runat="server" ID="hdnTERMSName" Value='<%# Container.DataItem["ID"] %>' />
                                                                                    <asp:CheckBox ID="ChkIDTERMS" runat="server" ToolTip="<%# Container.Value %>" Text=" " />
                                                                                </Template>
                                                                            </cc1:GridTemplate>
                                                                        </Templates>
                                                                        <ScrollingSettings ScrollWidth="100%" ScrollHeight="100" />
                                                                    </cc1:Grid>
                                                                </div>
                                                            </fieldset>
                                                        </td>
                                                        <td width="25">&nbsp;
                                                        </td>
                                                        <td>
                                                            <fieldset>
                                                                <legend>Gross Tax Details</legend>
                                                                <div class="gridcontent-shortstock">
                                                                    <cc1:Grid ID="grdTax" runat="server" CallbackMode="true" Serialize="true" AutoGenerateColumns="false"
                                                                        FolderStyle="../GridStyles/premiere_blue" AllowAddingRecords="false" AllowFiltering="true"
                                                                        AllowSorting="false" AllowPageSizeSelection="false" AllowPaging="false" PageSize="20">
                                                                        <FilteringSettings InitialState="Visible" />
                                                                        <Columns>
                                                                            <cc1:Column ID="Column2" DataField="SLNO" HeaderText="Sl No" runat="server" Width="70">
                                                                                <TemplateSettings TemplateId="tplTaxNumbering" />
                                                                            </cc1:Column>
                                                                            <cc1:Column ID="Column53" DataField="NAME" ReadOnly="true" HeaderText="TAX NAME"
                                                                                runat="server" Width="450" Wrap="true" />
                                                                            <cc1:Column ID="Column54" DataField="PERCENTAGE" ReadOnly="true" HeaderText="TAX(%)"
                                                                                runat="server" Width="200" />
                                                                            <cc1:Column ID="Column111" DataField="TAXVALUE" ReadOnly="true" HeaderText="Total Tax Value"
                                                                                runat="server" Width="200" />
                                                                        </Columns>
                                                                        <FilteringSettings MatchingType="AnyFilter" />
                                                                        <Templates>
                                                                            <cc1:GridTemplate runat="server" ID="tplTaxNumbering">
                                                                                <Template>
                                                                                    <asp:Label ID="lblslnoTax" runat="server" Text='<%#Container.PageRecordIndex+1 %>'></asp:Label>
                                                                                </Template>
                                                                            </cc1:GridTemplate>
                                                                        </Templates>
                                                                        <ScrollingSettings ScrollWidth="100%" ScrollHeight="180" />
                                                                    </cc1:Grid>
                                                                </div>
                                                            </fieldset>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px" colspan="2">
                                                <fieldset>
                                                    <legend>Gross Amount Details</legend>
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td width="95" class="innerfield_title">
                                                                <asp:Label ID="Label24" Text="Gross Amount" runat="server"></asp:Label>
                                                            </td>
                                                            <td width="150">
                                                                <asp:TextBox runat="server" ID="txtTotalGross" CssClass="x_large" placeholder="Gross Amount"
                                                                    Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td width="70" class="innerfield_title">
                                                                <asp:Label ID="Label27" Text="Round Off" runat="server"></asp:Label>
                                                            </td>
                                                            <td width="150">
                                                                <asp:TextBox runat="server" ID="txtRoundoff" CssClass="x_large" placeholder="Round Off"
                                                                    Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td width="120" class="innerfield_title">
                                                                <asp:Label ID="lblothchrg" Text="Other Charge Amt." runat="server"></asp:Label>
                                                            </td>
                                                            <td width="138">
                                                                <asp:TextBox runat="server" ID="txtothchrg" onkeypress="return isNumberKey(event);"
                                                                    Width="122" placeholder="Other Charge Amt.">
                                                                </asp:TextBox>
                                                            </td>
                                                            <td width="120" class="innerfield_title" style="display: none">
                                                                <asp:Label ID="lblfreight" Text="Freight Charges." runat="server"></asp:Label>
                                                            </td>
                                                            <td width="150" style="display: none">
                                                                <asp:TextBox runat="server" ID="txtfreight" CssClass="x_large" placeholder="Freight Charges."
                                                                    Text="0.00" onkeypress="return isNumberKey(event);" Width="122">
                                                                </asp:TextBox>
                                                            </td>
                                                            <td width="60" class="innerfield_title">
                                                                <asp:Label ID="Label33" Text="Net Amt." runat="server"></asp:Label>
                                                            </td>
                                                            <td width="150">
                                                                <asp:TextBox runat="server" ID="txtFinalAmt" CssClass="x_large" placeholder="Net Amt."
                                                                    Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td width="90" class="innerfield_title">&nbsp;
                                                            </td>
                                                            <td>&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr style="display: none;">
                                                            <td width="98" class="innerfield_title">&nbsp;
                                                            </td>
                                                            <td width="150">&nbsp;
                                                            </td>
                                                            <td width="70" class="innerfield_title">
                                                                <asp:Label ID="Label25" Text="Other Charge" runat="server" Visible="false"></asp:Label>&nbsp;
                                                            </td>
                                                            <td width="150">
                                                                <asp:TextBox runat="server" ID="txtOtherCharge" CssClass="x_large" placeholder="Other Charge"
                                                                    Text="0" Visible="false"></asp:TextBox>
                                                            </td>
                                                            <td width="60" class="innerfield_title">
                                                                <asp:Label ID="Label26" Text="Adj. Amt" runat="server" Visible="false"></asp:Label>
                                                            </td>
                                                            <td width="150">
                                                                <asp:TextBox runat="server" ID="txtAdj" CssClass="x_large" placeholder="Adj. Amt"
                                                                    Text="0" Visible="false"></asp:TextBox>
                                                            </td>
                                                            <td width="90" class="innerfield_title">&nbsp;
                                                            </td>
                                                            <td>&nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 8px 10px" colspan="2">
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td align="left" width="100" class="innerfield_title">
                                                            <asp:Label ID="lblRemarks" Text="Remarks" runat="server"></asp:Label>
                                                        </td>
                                                        <td width="325">
                                                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="mid" TextMode="MultiLine" Style="width: 90%; height: 80px"
                                                                MaxLength="255"> </asp:TextBox>
                                                        </td>
                                                        <td width="100" align="left" class="innerfield_title">
                                                            <asp:Label ID="lblCheckerNote" Text="Rejection Note" runat="server"></asp:Label>
                                                        </td>
                                                        <td width="310">
                                                            <asp:TextBox ID="txtCheckerNote" runat="server" TextMode="MultiLine" Enabled="false"
                                                                MaxLength="255" Style="width: 290px; height: 80px"> </asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">&nbsp;
                                                        </td>
                                                        <td colspan="3">
                                                            <div class="btn_24_blue">
                                                                <span class="icon cross_octagon_co"></span>
                                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn_link" CausesValidation="false"
                                                                    OnClick="btnCancel_Click" />
                                                            </div>
                                                        </td>
                                                        <asp:HiddenField runat="server" ID="hdnDespatchID" />
                                                        <asp:HiddenField runat="server" ID="hdnWaybillNo" />
                                                        <asp:HiddenField runat="server" ID="hdnCFormNo" />
                                                        <asp:HiddenField runat="server" ID="hdnCFormDate" />
                                                        <asp:HiddenField runat="server" ID="hdnWaybillKey" />
                                                        <asp:HiddenField runat="server" ID="HDNISVERIFIEDCHECKER1" />
                                                        <asp:HiddenField runat="server" ID="HDNSTOCKIN" />
                                            </td>
                                        </tr>
                                    </table>
                                    </td> </tr> </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlDisplay" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_title">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="90">
                                                            <asp:Label ID="Label16" runat="server" Text="From Date"></asp:Label>                                                            
                                                        </td>
                                                        <td width="105">
                                                            <asp:TextBox ID="txtFromDate" runat="server" MaxLength="10" Width="60" Enabled="false"
                                                                placeholder="dd/MM/yyyy" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="Search"
                                                                Font-Bold="true"></asp:TextBox>
                                                            <asp:ImageButton ID="imgPopup" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarFromDate" PopupButtonID="imgPopup" runat="server"
                                                                TargetControlID="txtFromDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                            </ajaxToolkit:CalendarExtender>                                                            
                                                        </td>
                                                        <td width="70">
                                                            <asp:Label ID="Label19" runat="server" Text="To Date"></asp:Label>
                                                        </td>
                                                        <td width="105">
                                                            <asp:TextBox ID="txtToDate" runat="server" MaxLength="10" Width="60" Enabled="false"
                                                                placeholder="dd/mm/yyyy" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="Search"
                                                                Font-Bold="true"></asp:TextBox>
                                                            <asp:ImageButton ID="imgPopuptodate" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtenderToDate" PopupButtonID="imgPopuptodate"
                                                                runat="server" TargetControlID="txtToDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                            </ajaxToolkit:CalendarExtender>                                                            
                                                        </td>
                                                        <td width="80">
                                                            <asp:Label ID="Label40" runat="server" Text="Report Type"></asp:Label>
                                                        </td>
                                                         <td class="field_input" width="50">
                                                                            <asp:DropDownList ID="ddlreportype" Width="80" runat="server" class="chosen-select"
                                                                                data-placeholder="Choose Transport Mode">
                                                                                <asp:ListItem Text="Header" Value="0"  Selected="True"></asp:ListItem>
                                                                                <asp:ListItem Text="Details" Value="1"> </asp:ListItem>
                                                                                </asp:DropDownList>
                                                                        </td>

                                                        <td class="field_title" width="50">
                                                                <asp:Label ID="Label41" runat="server" Text="Product"></asp:Label>
                                                            </td>
                                                            <td class="field_input" width="75">
                                                                <asp:DropDownList ID="ddlSearchproduct" runat="server" class="chosen-select" Style="width: 250px;"
                                                                    data-placeholder="Choose a Product" AppendDataBoundItems="true">
                                                                </asp:DropDownList>
                                                            </td>

                                                        <td class="field_title" width="80">
                                                            <asp:Label ID="Label39" runat="server" Text="GRN Status"></asp:Label>
                                                        </td>
                                                        <td class="field_input" width="50">                                                           
                                                            <asp:ListBox ID="ddlinvtype" runat="server" AppendDataBoundItems="True" multiple="multiple"
                                                                 name="options[]" SelectionMode="Multiple" Width="160" ValidationGroup="ADD" >                                                                
                                                                <asp:ListItem Text="GRN Entry" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="IQC" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="IQA" Value="3"></asp:ListItem>
                                                                <asp:ListItem Text="GRN Stockin" Value="4"></asp:ListItem>
                                                                <asp:ListItem Text="Accounts Checker-1" Value="5"></asp:ListItem>
                                                                <asp:ListItem Text="Approved" Value="6"></asp:ListItem>
                                                            </asp:ListBox>
                                                        </td>
                                                        </tr>
                                                    <tr>
                                                        <td width="110" valign="center">
                                                            <div class="btn_24_blue">
                                                                <span class="icon find_co"></span>
                                                                <asp:Button ID="btnSearchDespatch" runat="server" Text="Search" CssClass="btn_link"
                                                                    ValidationGroup="Search" OnClick="btnSearchDespatch_Click" />
                                                            </div>
                                                        </td>
                                                        <td valign="center">
                                                            <div class="btn_24_blue">
                                                                <span class="icon doc_excel_table_co"></span><a href="#" onclick="exportToExcel();">
                                                                    <asp:Button ID="btnExport" runat="server" Text="Export Excel" CssClass="btn_link" /></a>
                                                            </div>
                                                        </td>

                                                        <td width="60">
                                                            <asp:Label ID="Label120" runat="server" Text="Filter" Visible="false"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlWaybillFilter" Width="150" runat="server" class="chosen-select"
                                                                Visible="false" data-placeholder="Choose Waybill Filter" AutoPostBack="true"
                                                                OnSelectedIndexChanged="ddlWaybillFilter_SelectedIndexChanged">
                                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="Without Waybill" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="With Waybill" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="With C-Form" Value="3"></asp:ListItem>
                                                                <asp:ListItem Text="Without C-Form" Value="4"></asp:ListItem>
                                                            </asp:DropDownList>
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
                                    <div class="gridcontent-inner" style="padding-left: 8px;">
                                        <cc1:Grid ID="grdDespatchHeader" runat="server" CallbackMode="true" Serialize="true"
                                            FolderStyle="../GridStyles/premiere_blue" AutoGenerateColumns="false" AllowAddingRecords="false"
                                            AllowFiltering="true" AllowPageSizeSelection="false" AllowPaging="true" PageSize="30"
                                            OnExported="grdDespatchHeader_Exported" OnExporting="grdDespatchHeader_Exporting"
                                            OnRowDataBound="grdDespatchHeader_RowDataBound">
                                            <ExportingSettings ExportColumnsFooter="true" ExportGroupFooter="true" ExportGroupHeader="true"
                                                FileName="GRNList" ExportAllPages="true" ExportDetails="true" AppendTimeStamp="false" />
                                            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size: 10px;background-color: #DCE3E8;color: #0000FF;"
                                                CSSExportCellStyle="font-weight: normal;font-size: 10px;color: #0000;background-color: AFEEEE" />
                                            <ClientSideEvents OnBeforeClientDelete="OnBeforeDespatchDelete" OnClientDelete="OnDeleteDespatchDetails" />
                                            <FilteringSettings InitialState="Visible" />
                                            <Columns>
                                                <cc1:Column ID="Column12" DataField="SL" HeaderText="SL" runat="server" Width="50">
                                                </cc1:Column>
                                                <cc1:Column ID="Column19" DataField="STOCKRECEIVEDID" HeaderText="STOCKRECEIVEDID"
                                                    runat="server" Width="60" Visible="false" />
                                                <cc1:Column ID="Column1" DataField="STOCKDESPATCHID" HeaderText="STOCKDESPATCHID"
                                                    runat="server" Width="60" Visible="false" />
                                                <cc1:Column ID="Column91" DataField="STOCKRECEIVEDNO" HeaderText="GRN NO." runat="server"
                                                    Width="105">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="EqualTo" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="DoesNotContain" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                        <cc1:FilterOption Type="EndsWith" />
                                                        <cc1:FilterOption Type="IsEmpty" />
                                                        <cc1:FilterOption Type="IsNotEmpty" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column910" DataField="GATEPASSNO" HeaderText="GATE PASS NO" runat="server"
                                                    Width="105">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="EqualTo" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="DoesNotContain" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                        <cc1:FilterOption Type="EndsWith" />
                                                        <cc1:FilterOption Type="IsEmpty" />
                                                        <cc1:FilterOption Type="IsNotEmpty" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column8" DataField="STOCKRECEIVEDDATE" HeaderText="DATE" runat="server"
                                                    Width="80">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="EqualTo" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="DoesNotContain" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                        <cc1:FilterOption Type="EndsWith" />
                                                        <cc1:FilterOption Type="IsEmpty" />
                                                        <cc1:FilterOption Type="IsNotEmpty" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column id="idGRNVALUE" DataField="GRNVALUE" HeaderText="GRN VALUE"  runat="server" >
                                                </cc1:Column>
                                                <cc1:Column ID="Column14" DataField="INVOICENO" HeaderText="INVOICE NO" runat="server"
                                                    Width="80">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="EqualTo" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="DoesNotContain" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                        <cc1:FilterOption Type="EndsWith" />
                                                        <cc1:FilterOption Type="IsEmpty" />
                                                        <cc1:FilterOption Type="IsNotEmpty" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                 <cc1:Column id="Column9" DataField="INVOICEDATE" HeaderText="INVOICE DATE"  runat="server" >
                                                </cc1:Column>

                                               <cc1:Column ID="Column89" DataField="VENDORNAME" HeaderText="VENDORNAME" runat="server"
                                                    Width="120" Wrap="true">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="EqualTo" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="DoesNotContain" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                        <cc1:FilterOption Type="EndsWith" />
                                                        <cc1:FilterOption Type="IsEmpty" />
                                                        <cc1:FilterOption Type="IsNotEmpty" />
                                                    </FilterOptions>
                                                </cc1:Column>

                                                <cc1:Column ID="Column11" DataField="VENDORGSTNO" HeaderText="VENDOR GST NO" runat="server"
                                                    Width="90"/>

                                                <cc1:Column ID="Column13" DataField="DESPATCHFROM" HeaderText="DESPATCHED FROM" runat="server"
                                                    Width="90"/>

                                                 <cc1:Column ID="Column17" DataField="TRANSPORTERNAME" HeaderText="TRANSPORTER" runat="server"
                                                    Width="120" Wrap="true">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="EqualTo" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="DoesNotContain" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                        <cc1:FilterOption Type="EndsWith" />
                                                        <cc1:FilterOption Type="IsEmpty" />
                                                        <cc1:FilterOption Type="IsNotEmpty" />
                                                    </FilterOptions>
                                                </cc1:Column>

                                                <cc1:Column ID="Column15" DataField="VEHICHLENO" HeaderText="VEHICHLE NO" runat="server"
                                                    Width="90"/>
                                                
                                                <cc1:Column ID="Column4" DataField="PRODUCTNAME" HeaderText="ITEM NAME" runat="server"
                                                    Width="120" Wrap="true">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="EqualTo" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="DoesNotContain" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                        <cc1:FilterOption Type="EndsWith" />
                                                        <cc1:FilterOption Type="IsEmpty" />
                                                        <cc1:FilterOption Type="IsNotEmpty" />
                                                    </FilterOptions>
                                                </cc1:Column>

                                                <cc1:Column ID="Column18" DataField="ITEMCODE" HeaderText="ITEM CODE" runat="server"
                                                    Width="90"/>
                                                <cc1:Column ID="Column20" DataField="UOM" HeaderText="UOM" runat="server"
                                                    Width="90"/>
                                                <cc1:Column ID="Column21" DataField="ITEMQTY" HeaderText="ITEM QTY" runat="server"
                                                    Width="90"/>

                                                <cc1:Column ID="Column24" DataField="RATE" HeaderText="RATE" runat="server"
                                                    Width="90"/>

                                                <cc1:Column ID="Column22" DataField="QCSTATUS" HeaderText="QC Approved" runat="server"
                                                    Width="90"/>

                                                <cc1:Column ID="Column23" DataField="ISVERIFIEDSTOCKIN" HeaderText="GRN-STOCK IN" runat="server"
                                                    Width="90"/>

                                                <cc1:Column ID="Column27" DataField="ISVERIFIED" HeaderText="AccountsApproved" runat="server"
                                                    Width="90"/>
                                                
                                                <cc1:Column ID="clcancel" DataField="CANCELSTATUS" HeaderText="CANCEL_STATUS" runat="server"
                                                    Width="90"/>


                                                
                                                <cc1:Column ID="Column16" DataField="LRGRNO" HeaderText="LR/GR NO" runat="server"
                                                    Width="90" Visible="false">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="EqualTo" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="DoesNotContain" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                        <cc1:FilterOption Type="EndsWith" />
                                                        <cc1:FilterOption Type="IsEmpty" />
                                                        <cc1:FilterOption Type="IsNotEmpty" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                               
                                           



                                                <cc1:Column ID="Column79" DataField="FINYEAR" HeaderText="FINYEAR" runat="server"
                                                    Width="80" Visible="false" />
                                                <cc1:Column ID="Column7" DataField="FORMFLAG" HeaderText="FORMFLAG" runat="server"
                                                    Width="90" Visible="false" />
                                                <cc1:Column ID="Column26" DataField="NEXTLEVELID" HeaderText="NEXTLEVELID" runat="server"
                                                    Visible="false" Width="100"  />
                                                <cc1:Column ID="Column25" DataField="LASTSTATUS" HeaderText="LAST STATUS" runat="server"
                                                    Width="140" Wrap="true" Visible="false">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="EqualTo" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="DoesNotContain" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                        <cc1:FilterOption Type="EndsWith" />
                                                        <cc1:FilterOption Type="IsEmpty" />
                                                        <cc1:FilterOption Type="IsNotEmpty" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column1098" DataField="GRN" HeaderText="GRN" Visible="false" runat="server"
                                                    Width="100" />
                                                <cc1:Column ID="ColUsername" DataField="USERNAME" HeaderText="ENTRY USER" runat="server"
                                                    Width="100" Visible="false" />
                                                <cc1:Column ID="colComingStatus" DataField="COMINGSTATUS" HeaderText="COMING STATUS"
                                                    runat="server" Width="140" Visible="false" />
                                                <cc1:Column ID="Colnxtuser" DataField="NEXTENTRYUSER" HeaderText="NEXT ENTRYUSER"
                                                    runat="server" Width="100" Visible="false" />
                                                <cc1:Column ID="Column92" AllowEdit="true" AllowDelete="true" HeaderText="Edit" runat="server"
                                                    Visible="false" Width="60">
                                                    <TemplateSettings TemplateId="editBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column6" AllowEdit="true" AllowDelete="true" HeaderText="VIEW" runat="server"
                                                    Width="50">
                                                    <TemplateSettings TemplateId="ViewBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column_Print" AllowEdit="false" AllowDelete="true" HeaderText="PRINT" runat="server"
                                                    Width="60">
                                                    <TemplateSettings TemplateId="PrintBtnTemplate" />
                                                </cc1:Column>

                                            </Columns>
                                            <FilteringSettings MatchingType="AnyFilter" />
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="ViewBtnTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="h_icon zoom_sl" id="btnGridEdit_<%# Container.PageRecordIndex %>"
                                                            onclick="CallServerViewMethod(this)"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="slnoTemplateIN">
                                                    <Template>
                                                        <asp:Label ID="lblslnoin" runat="server" Text='<%#Container.PageRecordIndex+1 %>'></asp:Label>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="PrintBtnTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="filter_btn printer_co" id="btnPrint_<%# Container.PageRecordIndex %>"
                                                            onclick="CallServerMethodPrint(this)"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <ScrollingSettings ScrollWidth="100%" ScrollHeight="290" />
                                        </cc1:Grid>
                                        <asp:Button ID="btnview" runat="server" Text="View" Style="display: none" OnClick="btnview_Click"
                                            CausesValidation="false" />
                                        <asp:Button ID="btnPrint" runat="server" Text="Print" Style="display: none" OnClick="btnPrint_Click"
                                            CausesValidation="false" />
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlAddEdit" runat="server" CssClass="modalPopup" Style="display: none; border-radius: 16px;"
                                    Width="80%" Height="87%" BackColor="White" BorderColor="#257daf"
                                    BorderWidth="8px" BorderStyle="Solid">
                                    <div style="background-color: #257daf; border-style: solid; border-color: Yellow; background-position: center; background-repeat: no-repeat; background-size: cover; height: 6%"
                                        align="center">
                                        <asp:Image ID="Image2" runat="server" ImageAlign="Left" class="action-icons c-edit" />
                                        <asp:Label Font-Bold="True" ID="Label28" runat="server" Text="GRN Rejection Details"
                                            Font-Size="X-Large" ForeColor="Yellow"></asp:Label>
                                    </div>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td width="13%" class="field_title">
                                                <asp:Label ID="Label34" Text="Product" runat="server"></asp:Label>
                                            </td>
                                            <td class="field_input">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="45%">
                                                            <asp:TextBox ID="txtproductnameonrejection" runat="server" CssClass="full" Enabled="false"
                                                                ForeColor="Black"></asp:TextBox>
                                                        </td>
                                                        <td width="15">&nbsp;
                                                        </td>
                                                        <td width="9%" class="innerfield_title">
                                                            <asp:Label ID="Label35" Text="Batch No" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtproductbatchnoonrejection" runat="server" Width="192px" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title">
                                                <asp:Label ID="Label36" Text="Rejection Qty" runat="server"></asp:Label><span class="req">*</span>
                                            </td>
                                            <td class="field_input">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="45%">
                                                            <asp:TextBox ID="txtrejectionqty" runat="server" placeholder="Rejection Qty" Width="40%"
                                                                MaxLength="10" ForeColor="Black" ValidationGroup="check" onkeypress="return isNumberKeyWithDot(event);"
                                                                AutoCompleteType="Disabled" onfocus="disableautocompletion(this.id);"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*Required"
                                                                Font-Bold="true" ForeColor="Red" ControlToValidate="txtrejectionqty" ValidateEmptyText="false"
                                                                ValidationGroup="check" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                        </td>
                                                        <td width="15">&nbsp;
                                                        </td>
                                                        <td width="9%" class="innerfield_title">
                                                            <asp:Label ID="Label37" Text="PackSize/Unit" runat="server"></asp:Label><span class="req">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlrejectionpacksize" runat="server" AppendDataBoundItems="true"
                                                                class="common-select" Width="200" ValidationGroup="check">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*Required"
                                                                Font-Bold="true" ForeColor="Red" ControlToValidate="ddlrejectionpacksize" ValidateEmptyText="false"
                                                                ValidationGroup="check" SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title">
                                                <asp:Label ID="Label38" Text="Reason" runat="server"></asp:Label><span class="req">*</span>
                                            </td>
                                            <td class="field_input">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="45%">
                                                            <asp:DropDownList ID="ddlrejectionreason" runat="server" AppendDataBoundItems="true"
                                                                class="common-select" Width="450" ValidationGroup="check">
                                                                <asp:ListItem Text="-- SELECT REASON NAME --" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*Required"
                                                                Font-Bold="true" ForeColor="Red" ControlToValidate="ddlrejectionreason" ValidateEmptyText="false"
                                                                ValidationGroup="check" SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="gridcontent-inner">
                                        <asp:HiddenField runat="server" ID="hdn_guid" />
                                        <asp:HiddenField runat="server" ID="hdnpoid" />
                                        <asp:HiddenField runat="server" ID="hdnproductid" />
                                        <asp:HiddenField runat="server" ID="hdnReceivedID" />
                                        <asp:HiddenField runat="server" ID="hdnGRNID" />
                                        <asp:HiddenField runat="server" ID="hdnMfDate" />
                                        <asp:HiddenField runat="server" ID="hdnExprDate" />
                                    </div>
                                </asp:Panel>
                                <asp:LinkButton ID="lnkFake" runat="server"></asp:LinkButton>
                                <ajaxToolkit:ModalPopupExtender ID="popup" runat="server" DropShadow="false" PopupControlID="pnlAddEdit"
                                    TargetControlID="lnkFake" BackgroundCssClass="modalBackground">
                                </ajaxToolkit:ModalPopupExtender>
                                <style type="text/css">
                                    .modalBackground {
                                        top: 0px;
                                        left: 0px;
                                        background-color: rgba(0,0,0,0.5);
                                        filter: alpha(opacity=60);
                                        -moz-opacity: 0.5;
                                        opacity: 0.5;
                                    }
                                </style>
                            </div>
                        </div>
                    </div>
                    <div id="light" class="white_content" runat="server">
                        <table width="50%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                            <tr>
                                <td width="10%" class="field_title">
                                    <asp:Label ID="Label5" runat="server" Text="Waybill Key"></asp:Label>
                                </td>
                                <td width="20%" class="field_input">
                                    <asp:TextBox ID="txtWayBillKey" runat="server" Width="69%" placeholder="Waybill Key"
                                        AutoCompleteType="Disabled" onfocus="disableautocompletion(this.id);" ValidationGroup="SaveWaybill"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorWayBillKey" runat="server"
                                        ControlToValidate="txtWayBillKey" ValidateEmptyText="false" SetFocusOnError="true"
                                        ErrorMessage="Required!" ValidationGroup="SaveWaybill" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td width="10%" class="field_title">
                                    <asp:Label ID="Label20" runat="server" Text="Waybill No."></asp:Label>
                                </td>
                                <td width="20%" class="field_input">
                                    <asp:TextBox ID="txtWaybillUpdate" runat="server" Width="69%" placeholder="Waybill No"
                                        AutoCompleteType="Disabled" onfocus="disableautocompletion(this.id);" ValidationGroup="SaveWaybill"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorWaybillUpdate" runat="server"
                                        ControlToValidate="txtWaybillUpdate" ValidateEmptyText="false" SetFocusOnError="true"
                                        ErrorMessage="Required!" ValidationGroup="SaveWaybill" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="fade" class="black_overlay" runat="server">
                    </div>
                    <div id="light2" class="white_content" runat="server">
                        <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                            <tr>
                                <td width="10%" class="field_title">
                                    <asp:Label ID="Label30" runat="server" Text="C-Form No."></asp:Label>
                                </td>
                                <td width="15%" class="field_input">
                                    <asp:TextBox ID="txtCFormNo" runat="server" CssClass="x_large" placeholder="C-Form No."
                                        AutoCompleteType="Disabled" onfocus="disableautocompletion(this.id);"></asp:TextBox>
                                </td>
                                <td width="13%" class="field_title">
                                    <asp:Label ID="Label31" runat="server" Text="C-Form Date"></asp:Label>
                                </td>
                                <td width="18%" class="field_input">
                                    <asp:TextBox ID="txtCFormPopupDate" runat="server" Width="69%" placeholder="C-Form Date."
                                        Enabled="false"></asp:TextBox>
                                    <asp:ImageButton ID="ImageButton233" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                        runat="server" Height="24" />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImageButton233"
                                        runat="server" TargetControlID="txtCFormPopupDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5" height="200">&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="fade2" class="black_overlay" runat="server">
                    </div>
                </div>
                <cc1:MessageBox ID="MessageBox1" runat="server" />
            </div>            
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>