<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmGRNMM.aspx.cs" Inherits="VIEW_frmGRNMM" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../js/jquery.blockUI.js" type="text/javascript"></script>
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
        function Hidepopup() {
            $find("popup").hide();
            return false;
        }
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


     <style>
        th {
            background:  #50D0FF !important;
            color: black !important;
            position: sticky !important;
            top: 0;
            box-shadow: 0 2px 2px -1px rgb(255, 106, 0);
            width: 400px;
        }

        th, td {
            padding: 0.25rem;
        }
    </style>

    <style>
.dropbtn {
  background-color: #04AA6D;
  color: white;
  padding: 16px;
  font-size: 16px;
  border: none;
  cursor: pointer;
}

.dropbtn:hover, .dropbtn:focus {
  background-color: #3e8e41;
}

#myInput {
  box-sizing: border-box;
  background-image: url('searchicon.png');
  background-position: 14px 12px;
  background-repeat: no-repeat;
  font-size: 16px;
  padding: 14px 20px 12px 45px;
  border: none;
  border-bottom: 1px solid #ddd;
}

#myInput:focus {outline: 3px solid #ddd;}




</style>

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

        function CallDeleteServerMethod(oLink) {
            var iRecordIndexdelete = oLink.id.toString().replace("btnGridDelete_", "");
            document.getElementById("<%=hdndtDespatchDelete.ClientID %>").value = grdAddDespatch.Rows[iRecordIndexdelete].Cells[1].Value;
            document.getElementById("<%=hdndtPOIDDelete.ClientID %>").value = grdAddDespatch.Rows[iRecordIndexdelete].Cells[2].Value;
            document.getElementById("<%=hdndtPRODUCTIDDelete.ClientID %>").value = grdAddDespatch.Rows[iRecordIndexdelete].Cells[3].Value;
            document.getElementById("<%=hdndtBATCHDelete.ClientID %>").value = grdAddDespatch.Rows[iRecordIndexdelete].Cells[12].Value;

        }

        function CallRejectionDeleteServerMethod(oLink) {
            var iRecordIndexdelete = oLink.id.toString().replace("btnGridRejectionDelete_", "");
            document.getElementById("<%=hdn_guid.ClientID %>").value = gvrejectiondetails.Rows[iRecordIndexdelete].Cells[0].Value;
            document.getElementById("<%=hdnpoid.ClientID %>").value = gvrejectiondetails.Rows[iRecordIndexdelete].Cells[2].Value;
            document.getElementById("<%=hdnproductid.ClientID %>").value = gvrejectiondetails.Rows[iRecordIndexdelete].Cells[4].Value;
            document.getElementById("<%=hdnRejBatch.ClientID %>").value = gvrejectiondetails.Rows[iRecordIndexdelete].Cells[7].Value;
            document.getElementById("<%=btnRejectiongrddelete.ClientID %>").click();

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



        function CallServerMethod(oLink) {
            var iRecordIndex = oLink.id.toString().replace("btnGridEdit_", "");
            document.getElementById("<%=hdnDespatchID.ClientID %>").value = grdDespatchHeader.Rows[iRecordIndex].Cells[1].Value;
            document.getElementById("<%=hdnReceivedID.ClientID %>").value = grdDespatchHeader.Rows[iRecordIndex].Cells[2].Value;
            document.getElementById("<%=btngrdedit.ClientID %>").click();
        }

        function CallServerViewMethod(oLink) {
            var iRecordIndex = oLink.id.toString().replace("btnGridEdit_", "");
            document.getElementById("<%=hdnDespatchID.ClientID %>").value = grdDespatchHeader.Rows[iRecordIndex].Cells[1].Value;
            document.getElementById("<%=hdnReceivedID.ClientID %>").value = grdDespatchHeader.Rows[iRecordIndex].Cells[2].Value;
            document.getElementById("<%=btnview.ClientID %>").click();
        }

        function CallServerMethodWaybill(oLink) {
            var iRecordIndex = oLink.id.toString().replace("btnGridWaybill_", "");
            document.getElementById("<%=hdnDespatchID.ClientID %>").value = grdDespatchHeader.Rows[iRecordIndex].Cells[1].Value;
            document.getElementById("<%=hdnWaybillKey.ClientID %>").value = grdDespatchHeader.Rows[iRecordIndex].Cells[5].Value;
            document.getElementById("<%=hdnWaybillNo.ClientID %>").value = grdDespatchHeader.Rows[iRecordIndex].Cells[17].Value;
            document.getElementById("<%=btngrdUpdateWaybill.ClientID %>").click();
        }

        function CallServerMethodPrint(oLink) {
            var iRecordIndex = oLink.id.toString().replace("btnPrint_", "");
            document.getElementById("<%=hdnDespatchID.ClientID %>").value = grdDespatchHeader.Rows[iRecordIndex].Cells[1].Value;
            document.getElementById("<%=hdnReceivedID.ClientID %>").value = grdDespatchHeader.Rows[iRecordIndex].Cells[2].Value;
            document.getElementById("<%=btnPrint.ClientID %>").click();
        }

        function CallServerMethodCForm(oLink) {
            var iRecordIndex = oLink.id.toString().replace("btnGridCForm_", "");
            document.getElementById("<%=hdnDespatchID.ClientID %>").value = grdDespatchHeader.Rows[iRecordIndex].Cells[1].Value;
            document.getElementById("<%=hdnCFormNo.ClientID %>").value = grdDespatchHeader.Rows[iRecordIndex].Cells[6].Value;
            document.getElementById("<%=hdnCFormDate.ClientID %>").value = grdDespatchHeader.Rows[iRecordIndex].Cells[5].Value;
            document.getElementById("<%=btngrdUpdateForm.ClientID %>").click();

        }


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
                                <div class="btn_30_light" style="float: right;" id="btnaddhide" runat="server">
                                    <span class="icon add_co"></span>
                                    <asp:Button ID="btnAdd" runat="server" Text="Add New Record" CssClass="btn_link"
                                        CausesValidation="false" OnClick="btnAdd_Click" />
                                </div>
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
                                                                    Width="24px" Height="24px" ImageAlign="AbsMiddle"/>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorDespatchDate" runat="server"
                                                                    ControlToValidate="txtDespatchDate" ValidateEmptyText="false" SetFocusOnError="true"
                                                                    ErrorMessage="Required!" ValidationGroup="Save" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtenderDespatchDate" TargetControlID="txtDespatchDate"
                                                                    PopupButtonID="ImageButton1" runat="server" Format="dd/MM/yyyy"
                                                                    BehaviorID="CalendarExtenderDespatchDate" CssClass="cal_Theme1" />
                                                            </td>
                                                            <td class="field_title">
                                                                <asp:Label ID="lblgatepassno" Text="GatePass No." runat="server"></asp:Label>&nbsp;<span
                                                                    class="req">*</span>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:DropDownList ID="ddlGatePassNo" runat="server" runat="server" class="chosen-select" AutoPostBack="true"
                                                                    data-placeholder="Gate Pass No" AppendDataBoundItems="True" Width="150px" ValidationGroup="Save" OnSelectedIndexChanged="ddlGatePassNo_SelectedIndexChanged"> </asp:DropDownList>
                                                            </td>
                                                            <td width="135" class="field_title">
                                                                <asp:Label ID="lblvendorfrom" Text="Vendor From" runat="server"></asp:Label>
                                                            </td>
                                                            <td width="160" class="field_input">
                                                                <asp:DropDownList ID="ddlVendorFrom" runat="server" class="chosen-select" AutoPostBack="true"
                                                                    data-placeholder="Vendor From" AppendDataBoundItems="True" Width="150px" ValidationGroup="Save" 
                                                                    OnSelectedIndexChanged="ddlVendorFrom_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0" Text="select type"></asp:ListItem>
                                                                    <asp:ListItem Value="P" Selected="True" Text="PO"></asp:ListItem>
                                                                    <asp:ListItem Value="J" Text="JOB ORDER"></asp:ListItem> 
                                                                    <asp:ListItem Value="C" Text="Consumable"></asp:ListItem>
                                                                    <asp:ListItem Value="R" Text="RGP"></asp:ListItem>
                                                                </asp:DropDownList>
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
                                                            
                                                        </tr>
                                                        <tr>
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
                                                                <asp:Label ID="lblVehicle" Text="Vehicle No." runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtVehicle" runat="server" CssClass="x_large" placeholder="Vehicle No"
                                                                    ValidationGroup="Save" AutoCompleteType="Disabled" onfocus="disableautocompletion(this.id);"> </asp:TextBox>
                                                             
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
                                                                    class="req"></span>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtLRGRNo" runat="server" CssClass="x_large" placeholder="LR/GR No"
                                                                    AutoCompleteType="Disabled" onfocus="disableautocompletion(this.id);"> </asp:TextBox>
                                                            </td>
                                                            <td class="field_title">
                                                                <asp:Label ID="lblLRGRDate" Text="LR/GR Date" runat="server"></asp:Label>&nbsp;<span
                                                                    class="req"></span>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtLRGRDate" runat="server" Enabled="false" Width="120" placeholder="dd/MM/yyyy"
                                                                    MaxLength="10"></asp:TextBox>
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
                                                                <asp:Label ID="Label44" Text="Received Store Location" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input" >
                                                                <asp:DropDownList ID="ddlStorelocation" Width="180" runat="server" class="chosen-select"
                                                                    data-placeholder="Select Store" AppendDataBoundItems="True">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td class="field_title" style="display: none;">
                                                                <asp:Label ID="lblWayBill" Text="WayBill Key" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input" style="display: none;">
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
                                                                    ValidationGroup="Save" AutoCompleteType="Disabled" onchange="CHKInvoiceNo();" Enabled="true" onkeypress="return isNumberKeyWithSlashHyphenAtZ(event);"
                                                                    onfocus="disableautocompletion(this.id);"> </asp:TextBox>
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
                                                            
                                                        </tr>
                                                        <tr>
                                                            <td class="field_title">
                                                                <asp:Label ID="lblgatepassdate" Text="GatePass Date" runat="server"></asp:Label>&nbsp;<span
                                                                    class="req">*</span>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtgatepassdate" runat="server" Width="120" placeholder="dd/MM/yyyy"
                                                                    Enabled="false" MaxLength="10" ValidationGroup="Save"> </asp:TextBox>
                                                                <asp:ImageButton ID="Imgbtngatepass" runat="server" ImageUrl="~/images/calendar.png"
                                                                    Width="24px" Height="24px" ImageAlign="AbsMiddle" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                                                    ControlToValidate="txtgatepassdate" ValidateEmptyText="false" SetFocusOnError="true"
                                                                    ErrorMessage="Required!" ValidationGroup="Save" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtgatepassdate"
                                                                    PopupButtonID="Imgbtngatepass" runat="server" Format="dd/MM/yyyy" Enabled="true"
                                                                    BehaviorID="CalendarExtender2" CssClass="cal_Theme1" />

                                                            </td>

                                                              <td class="field_title">
                                                                <asp:Label ID="Label45" Text="Devation No" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtDevationno" runat="server" CssClass="x_large" placeholder="DevationNo"
                                                                    ValidationGroup="Save" AutoCompleteType="Disabled" Enabled="true" 
                                                                   > </asp:TextBox>
                                                              
                                                            </td>

                                                            
                                                              <td class="field_title">
                                                                <asp:Label ID="Label50" Text="Devation date" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="TxtDevationdate" runat="server" CssClass="x_large" placeholder="Devationdate"
                                                                    ValidationGroup="Save" AutoCompleteType="Disabled" Enabled="false"></asp:TextBox>
                                                                     <asp:ImageButton ID="ImageButtonDEV" runat="server" ImageUrl="~/images/calendar.png"
                                                                    Width="24px" Height="24px" ImageAlign="AbsMiddle" />

                                                                          <ajaxToolkit:CalendarExtender ID="CalendarExtenderDEV" TargetControlID="TxtDevationdate"
                                                                    PopupButtonID="ImageButtonDEV" runat="server" Format="dd/MM/yyyy" Enabled="true"
                                                                    BehaviorID="CalendarExtenderDEV" CssClass="cal_Theme1" />                                                      


                                                            <td class="field_title" id="TdIssueProduct" runat="server" style="display:none">
                                                                <asp:Label ID="lblissueProduct" Text="Issue Product" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input" id="TdddlissueProduct" runat="server" style="display:none">
                                                                <asp:DropDownList ID="ddlissueProduct" Width="200" runat="server" class="chosen-select">
                                                                </asp:DropDownList>                                                               
                                                            </td>


                                                            <td class="field_title" id="Tdlblledger" runat="server">
                                                                <asp:Label ID="lblledger" Text="Ledger" runat="server"></asp:Label>&nbsp;<span class="req">*</span>
                                                            </td>
                                                            <td class="field_input" id="Tdddlledger" runat="server">
                                                                <asp:DropDownList ID="ddlledger" Width="200" runat="server" class="chosen-select">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="Reqrdddlledger" runat="server" ControlToValidate="ddlledger"
                                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="Approve"
                                                                    InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr id="TrWayBill" runat="server">
                                                            <td class="field_titleTr">
                                                                <asp:Label ID="lblWaybillDt" runat="server" Text="Waybill&nbsp;Date"></asp:Label>
                                                            </td>

                                                            <td class="field_inputTr">
                                                                <asp:TextBox ID="txtwaybilldate" runat="server" Width="120" Enabled="false" placeholder="dd/mm/yyyy"
                                                                    MaxLength="10" ValidationGroup="checksave" onkeypress="return isNumberKeyWithslash(event);">
                                                                </asp:TextBox>
                                                                <asp:ImageButton ID="imgPopupwaybilldate" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalWayBill" PopupButtonID="imgPopupwaybilldate"
                                                                    runat="server" TargetControlID="txtwaybilldate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>

                                                            <td class="field_titleTr" width="110">
                                                                <asp:Label ID="lblWaybillNo" Text="Way Bill No" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_inputTr">
                                                                <asp:TextBox ID="txtwaybilno" runat="server" placeholder="Enter WayBill No" Width="195px"
                                                                    AutoCompleteType="Disabled" onfocus="disableautocompletion(this.id);"></asp:TextBox>
                                                            </td>
                                                        </tr>

                                                        <tr style="display: none;">
                                                            <td class="field_title">
                                                                <asp:Label ID="lblinsurancecompname" Text="INSURANCE COMPANY" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:DropDownList ID="ddlinsurancecompname" Width="180" runat="server" class="chosen-select"
                                                                    data-placeholder="" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlinsurancecompname_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td class="field_title">
                                                                <asp:Label ID="Label32" Text="INSURANCE NUMBER" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:DropDownList ID="ddlInsuranceNumber" Width="180" runat="server" class="chosen-select"
                                                                    data-placeholder="" AppendDataBoundItems="True">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr style="display: none;">
                                                            <td class="field_title">
                                                                <asp:Label ID="Label11" Text="C Form" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:CheckBox ID="chkActive" runat="server" Text=" " onchange="Check()" />&nbsp<asp:Label
                                                                    ID="lbltext" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_title">&nbsp;
                                                            </td>
                                                            <td class="field_input">&nbsp;
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
                                                                <asp:Label ID="Label1" Text="PO" runat="server"></asp:Label> &nbsp;<span class="req">*</span>
 
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
                                                                
                                                                  
                                                                <asp:DropDownList ID="ddlproduct" runat="server" Height="24px" Width="1000px" AutoPostBack="true"
                                                                    ValidationGroup="A" AppendDataBoundItems="True"  OnSelectedIndexChanged="ddlproduct_SelectedIndexChanged"
                                                                    Style="font-family: 'Courier New', Courier, monospace"  class="chosen-select">
                                                                    <asp:ListItem Text="-- SELECT PO NO --" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                  
                                                                <asp:RequiredFieldValidator ID="CV_ddlproduct" runat="server" ControlToValidate="ddlproduct"
                                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="A"
                                                                    ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                                                      
                                                            </td>
                                                            </div>
                                                            <td class="field_title">
                                                                <asp:Label ID="Label3" Text="Batch No" runat="server" Visible="false"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox runat="server" ID="txtBatch" CssClass="large" placeholder="Batch No" Visible="false"></asp:TextBox>
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
                                                                            <asp:Label ID="Label6" Text="Mfg.Date" runat="server" Visible="false"></asp:Label>
                                                                        </td>
                                                                        <td width="150" class="field_input">
                                                                            <asp:TextBox runat="server" ID="txtMfDate" Width="110" placeholder="DD/MM/YYYY" MaxLength="10" Visible="false"
                                                                                AutoPostBack="true" OnTextChanged="txtMfDate_TextChanged" onkeypress="return isNumberKeyWithslash(event);"></asp:TextBox>
                                                                            <asp:ImageButton ID="ImageButtonMFDate" runat="server" ImageUrl="~/images/calendar.png" Visible="false"
                                                                                Width="24px" Height="24px" ImageAlign="AbsMiddle" />
                                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtenderMFDate" TargetControlID="txtMfDate"
                                                                                PopupButtonID="ImageButtonMFDate" runat="server" Format="dd/MM/yyyy" Enabled="true"
                                                                                BehaviorID="CalendarExtenderMFDate" CssClass="cal_Theme1" />
                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorMFDate" runat="server"
                                                                                ForeColor="Red" ControlToValidate="txtMfDate" ValidationGroup="A" ErrorMessage="Invalid"
                                                                                ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$"></asp:RegularExpressionValidator>
                                                                        </td>
                                                                        <td width="90" class="field_title">
                                                                            <asp:Label ID="Label8" Text="Exp.Date" runat="server" Visible="false"></asp:Label>
                                                                        </td>
                                                                        <td width="150" class="field_input">
                                                                            <asp:TextBox runat="server" ID="txtExprDate" Width="110" placeholder="DD/MM/YYYY" Visible="false"
                                                                                MaxLength="10" onkeypress="return isNumberKeyWithslash(event);"></asp:TextBox>
                                                                            <asp:ImageButton ID="ImageButtonExprDate" runat="server" ImageUrl="~/images/calendar.png" Visible="false"
                                                                                Width="24px" Height="24px" ImageAlign="AbsMiddle" />
                                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtenderExprDate" TargetControlID="txtExprDate"
                                                                                PopupButtonID="ImageButtonExprDate" runat="server" Format="dd/MM/yyyy" Enabled="true"
                                                                                BehaviorID="CalendarExtenderExprDate" CssClass="cal_Theme1" />
                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorExprDate" runat="server"
                                                                                ForeColor="Red" ControlToValidate="txtExprDate" ValidationGroup="A" ErrorMessage="Invalid"
                                                                                ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$"></asp:RegularExpressionValidator>
                                                                        </td>
                                                                        <td width="50" class="field_title">
                                                                            <asp:Label ID="Label7" Text="Rate" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td width="150" class="field_input">
                                                                            <asp:TextBox runat="server" ID="txtRate" Width="145" placeholder="Rate" Enabled="true"></asp:TextBox>
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
                                                                        <td width="90" class="field_title">
                                                                            <asp:Label ID="lblPOQTY" Text="PO Qty" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td class="field_input">
                                                                            <asp:TextBox runat="server" ID="txtPoQty" Width="145" placeholder="PO Qty" Enabled="false"></asp:TextBox>
                                                                            <asp:HiddenField ID="hdnPoQty" runat="server" />
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

                                                                        <td class="field_title" style="display: none">
                                                                            <asp:Label ID="lblissueqty" Text="Issue Qty" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td class="field_input" style="display: none">
                                                                            <asp:TextBox runat="server" ID="txtissueqty" Width="145" placeholder="Issue Qty" Enabled="false"></asp:TextBox>
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
                                                                                onkeypress="return isNumberKeyWithDot(event);" ValidationGroup="A" AutoCompleteType="Disabled"
                                                                                onfocus="disableautocompletion(this.id);"></asp:TextBox>
                                                                        </td>
                                                                        <td class="field_title" style="display:none">
                                                                            <asp:Label ID="lblitemwiseFreight" Text="ITEMWISE FREIGHT CHARGES." runat="server"></asp:Label>
                                                                        </td>
                                                                        <td class="field_input" style="display:none">
                                                                            <asp:TextBox runat="server" ID="txtitemwiseFreight" Width="145" placeholder="Enter Freight Charges"
                                                                                onkeypress="return isNumberKey(event);" ValidationGroup="A" AutoCompleteType="Disabled"
                                                                                onfocus="disableautocompletion(this.id);"></asp:TextBox>
                                                                        </td>
                                                                        <td class="field_title">
                                                                            <asp:Label ID="Label39" Text="Item wise additional" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td class="field_input">
                                                                            <asp:TextBox runat="server" ID="txtitemwiseAddCost" Width="145" placeholder="Enter Add Additional Cost"
                                                                                onkeypress="return isNumberKey(event);" ValidationGroup="A" AutoCompleteType="Disabled"
                                                                                onfocus="disableautocompletion(this.id);"></asp:TextBox>
                                                                        </td>
                                                                       <%-- new txtbox--%>
                                                                        <td class="field_title" style="width:450px" >
                                                                            <asp:Label ID="Label51" Text="Item wise description." runat="server"></asp:Label>
                                                                        </td>

                                                                     <td class="field_title" style="width:950px" >
                                                                     <asp:TextBox ID="Textitemdescription" runat="server"  Width="650px" CssClass="x_large" placeholder="itemdescription"
                                                                    ValidationGroup="Save" AutoCompleteType="Disabled"></asp:TextBox>
                                                                         
                                                                         </td>

                                                                         

                                                                        <td class="field_title">
                                                                            <asp:Button ID="btnADDGrid" runat="server" Text=" " CssClass="addbtn_blue" OnClick="btnADDGrid_Click"
                                                                                ValidationGroup="A" />


                                                                            <asp:RequiredFieldValidator ID="CV_txtDespatchQty" runat="server" ControlToValidate="txtDespatchQty"
                                                                                ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="A"
                                                                                ForeColor="Red"></asp:RequiredFieldValidator>
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
                                                                        <td class="field_title">
                                                                            <asp:Label ID="lblcatid" Visible="false" runat="server"></asp:Label>
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
                                                <asp:GridView ID="grdAddDespatch" runat="server" Width="100%" CssClass="reportgrid" AutoGenerateColumns="true"
                                                    EmptyDataText="No Records Available" OnRowDataBound="grdAddDespatch_OnRowDataBound"
                                                    ShowFooter="true" DataKeyNames="GUID,PRODUCTID">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="DELETE">
                                                            <ItemTemplate>
                                                                <%--<asp:ImageButton ID="btn_TempDelete" runat="server" class="action-icons c-delete"
                                                                    OnClientClick="return confirm('Are you sure you want to Delete?')" ToolTip="Delete"
                                                                    OnClick="btn_TempDelete_Click" />--%>

                                                                <asp:Button ID="btn_TempDelete" runat="server" class="action-icons c-delete"
                                                                    OnClientClick="return confirm('Are you sure you want to Delete?')" ToolTip="Delete"
                                                                    OnClick="btn_TempDelete_Click" />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="REJECTION" HeaderStyle-ForeColor="Red">
                                                            <ItemTemplate>
                                                                <nav style="height: 5px; z-index: 2; position: relative; text-align: center;">
                                                                    <asp:Label ID="lblrejvalue" runat="server" class="badge red">0</asp:Label>
                                                                </nav>
                                                                <asp:ImageButton ID="edit" runat="server" CausesValidation="false" ToolTip="REJECTION REASON"
                                                                    class="action-icons c-edit" Style="margin-left: 15px;" OnClick="btnRejectionReason"></asp:ImageButton>
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
                                            <asp:HiddenField ID="hdnJobOrderReceived" runat="server" />
                                            <asp:HiddenField ID="hdnRecvdQty" runat="server" />
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
                                                    <legend>Summary</legend>
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td width="95" class="innerfield_title">
                                                                <asp:Label ID="Label21" Text="Basic Value" runat="server"></asp:Label>
                                                            </td>
                                                            <td width="139">
                                                                <asp:TextBox runat="server" ID="txtAmount" Width="126" placeholder="Tot.Basic&nbsp;Value"
                                                                    Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td width="95" class="innerfield_title" style="display:none">
                                                                <asp:Label ID="lbl45" Text="FREIGHTAMT" runat="server"></asp:Label>
                                                            </td>
                                                            <td width="139" style="display:none">
                                                                <asp:TextBox runat="server" ID="txtFREIGHTAMT" Width="126" placeholder="To&nbsp;Value"
                                                                    Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td width="90" style="display:none">
                                                                <asp:Label ID="Label311" Text="MRP Value" runat="server"></asp:Label>
                                                            </td>
                                                            <td width="139" style="display:none">
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
                                                            <td width="145" class="innerfield_title" style="display: none">
                                                                <asp:Label ID="lblTotalItemWiseFreight" runat="server"></asp:Label>
                                                                <asp:Label ID="lblTotalItemWiseDist" runat="server"></asp:Label>
                                                                <asp:Label ID="lblTotalItemWiseAddCost" runat="server"></asp:Label>
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
                                                                        AllowSorting="false" OnRowDataBound="grdTax_RowDataBound" AllowPageSizeSelection="false"
                                                                        AllowPaging="false" PageSize="20">
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
                                            <td class="field_input" style="padding-left: 10px; display: none" colspan="2">
                                                <fieldset>
                                                    <legend>Additional Details</legend>
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td width="50" class="innerfield_title">
                                                                <asp:Label ID="Label40" Text="Bill Sundry" runat="server"></asp:Label>
                                                            </td>
                                                            <td width="80">
                                                                <asp:DropDownList ID="ddlsundry" runat="server" Width="200px" AutoPostBack="true"
                                                                    AppendDataBoundItems="True" class="chosen-select" data-placeholder="Select Product"
                                                                    OnSelectedIndexChanged="ddlsundry_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td width="50" class="innerfield_title">
                                                                <asp:Label ID="Label41" Text="%" runat="server"></asp:Label>
                                                            </td>
                                                            <td width="80">
                                                                <asp:TextBox runat="server" ID="txtpercent" CssClass="x_large" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td width="50" class="innerfield_title">
                                                                <asp:Label ID="Label42" Text="Amount" runat="server"></asp:Label>
                                                            </td>
                                                            <td width="80">
                                                                <asp:TextBox runat="server" ID="txtamt" CssClass="x_large"></asp:TextBox>
                                                            </td>
                                                            <td width="90" class="innerfield_title">
                                                                <div class="btn_24_blue">
                                                                    <span class="icon add_co"></span>
                                                                    <asp:Button ID="btntaxadd" runat="server" Text="ADD" CssClass="btn_link" OnClick="btntaxadd_Click" />
                                                                </div>
                                                            </td>
                                                            <td width="90" class="innerfield_title">
                                                                <asp:Label ID="lblledgerid" runat="server" Text="ledger" Visible="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="98" class="innerfield_title" colspan="8">
                                                                <div class="gridcontent-inner">
                                                                    <div style="overflow: hidden; width: 100%;" id="Div1">
                                                                    </div>
                                                                    <div style="overflow: scroll; margin-bottom: 8px;" onscroll="OnScrollDiv(this)" id="Div2">
                                                                        <asp:GridView ID="gvadd" runat="server" Width="100%" ShowFooter="true" AutoGenerateColumns="false"
                                                                            EmptyDataText="No Records Available" CssClass="zebra" OnRowDataBound="gvadd_OnRowDataBound">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="GUID" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl1" runat="server" Text='<%# Bind("GUID") %>' value='<%# Eval("GUID") %>'
                                                                                            Visible="false" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="DEALID" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl2" runat="server" Text='<%# Bind("TAXID") %>' value='<%# Eval("TAXID") %>'
                                                                                            Visible="false" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="BILL SUNDRY" HeaderStyle-Wrap="true" ItemStyle-Wrap="true">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblpacksizeid" runat="server" Text='<%# Bind("TAXNAME") %>' value='<%# Eval("TAXNAME") %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="PERCENTAGE" HeaderStyle-Wrap="true" ItemStyle-Wrap="true">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl3" runat="server" Text='<%# Bind("PERCENTAGE") %>' value='<%# Eval("PERCENTAGE") %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="AMOUNT" HeaderStyle-Wrap="true" ItemStyle-Wrap="true"
                                                                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl4" runat="server" Text='<%# Bind("AMOUNT") %>' value='<%# Eval("AMOUNT") %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="LEDGERID" HeaderStyle-Wrap="true" ItemStyle-Wrap="true"
                                                                                    Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl10" runat="server" Text='<%# Bind("LEDGERID") %>' value='<%# Eval("LEDGERID") %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="DELETE" ItemStyle-HorizontalAlign="Center">
                                                                                    <ItemTemplate>
                                                                                        <%-- <asp:ImageButton ID="btn_TempDeletetax" runat="server" CssClass="action-icons c-delete"
                                                                                            OnClientClick="return confirm('Are you sure you want to Delete?')" ToolTip="Delete"
                                                                                            OnClick="btn_TempDeletetax_Click" />--%>

                                                                                        <asp:Button ID="btn_TempDeletetax" runat="server" CssClass="action-icons c-delete"
                                                                                            OnClientClick="return confirm('Are you sure you want to Delete?')" ToolTip="Delete"
                                                                                            OnClick="btn_TempDeletetax_Click" />

                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </div>
                                                                    <div id="Div3" style="overflow: hidden">
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
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
                                                            <td width="90" class="innerfield_title" style="display: none">
                                                                <asp:Label ID="Label43" Text="Addn. Amt" runat="server"></asp:Label>
                                                            </td>
                                                            <td width="150" style="display: none">
                                                                <asp:TextBox runat="server" ID="txtaddnamt" CssClass="x_large" placeholder="Addn. Amt"
                                                                    Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td width="70" class="innerfield_title">
                                                                <asp:Label ID="Label27" Text="Round Off" runat="server"></asp:Label>
                                                            </td>
                                                            <td width="150">
                                                                <asp:TextBox runat="server" ID="txtRoundoff" CssClass="x_large" placeholder="Round Off"
                                                                    Enabled="true" AutoPostBack="true" OnTextChanged="txtRoundoff_TextChanged"></asp:TextBox>
                                                            </td>
                                                            <td width="120" class="innerfield_title">
                                                                <asp:Label ID="lblothchrg" Text="Other Charge Amt." runat="server" Enabled="false" Visible="false"></asp:Label>
                                                            </td>
                                                            <td width="138">
                                                                <asp:TextBox runat="server" ID="txtothchrg" onkeypress="return isNumberKey(event);"
                                                                    Width="122" AutoPostBack="true" placeholder="Other Charge Amt." OnTextChanged="txtothchrg_TextChanged" Visible="false">
                                                                </asp:TextBox>
                                                            </td>
                                                            <td width="120" class="innerfield_title" style="display: none">
                                                                <asp:Label ID="lblfreight" Text="Freight Charges." runat="server"></asp:Label>
                                                            </td>
                                                            <td width="150" style="display: none">
                                                                <asp:TextBox runat="server" ID="txtfreight" CssClass="x_large" placeholder="Freight Charges."
                                                                    Text="0.00" onkeypress="return isNumberKey(event);" Width="122" AutoPostBack="true"
                                                                    OnTextChanged="txtfreight_TextChanged">
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
                                                        <tr>

                                                        </tr>
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td align="left" class="field_title">TCS<span class="req">*</span></td>
                                                            <td align="left" class="field_input">
                                                               <asp:RadioButtonList ID="RbApplicable" AutoPostBack="true" RepeatDirection="Horizontal"
                                                                   runat="server"  OnSelectedIndexChanged="RbApplicable_SelectedIndexChanged" >
                                                                  <asp:ListItem Value="Y" >YES</asp:ListItem>
                                                               <asp:ListItem Value="N" >NO  </asp:ListItem>
                                                            </asp:RadioButtonList>
                                                          </td>
                                                        </tr>
                                                          <%--TCS Amounts Information--%>
                                                        <div  runat="server" id="divtcs">
                                                        <tr>

                                                             <td width="138" class="field_title">
                                                                <asp:Label ID="Label47" Text="TCS Applicable Amount" runat="server"></asp:Label>
                                                            </td>
                                                            <td width="100" class="field_input">
                                                                <asp:TextBox runat="server" ID="txtTCSApplicable" CssClass="x-large" placeholder="TCS Applicable" Enabled="false" AutoPostBack="True" OnTextChanged="txtTCSApplicable_TextChanged"></asp:TextBox>
                                                            </td>
                                                            
                                                            <td width="98" class="field_title">
                                                                <asp:Label ID="Label46" Text="TCS(%)" runat="server"></asp:Label>
                                                            </td>
                                                            <td width="138" class="field_input">
                                                                <asp:TextBox runat="server" ID="txtTCSPercent" Text="TCS(%)" CssClass="x_large" placeholder="TCS(%)" Enabled="false" ></asp:TextBox>
                                                                <asp:TextBox runat="server" ID="txtTCSLimit" CssClass="x_large" placeholder="TCS Limit(%)" Enabled="false" Visible="false"></asp:TextBox>
                                                            </td>
                                                            
                                                           
                                                            
                                                            <td width="75" class="field_title">
                                                                <asp:Label ID="Label48" Text="TCS" runat="server"></asp:Label>
                                                            </td>
                                                            <td width="100" class="field_input">
                                                                <asp:TextBox runat="server" ID="txtTCS" CssClass="x_large" placeholder="TCS" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td width="65" class="field_title">
                                                                 <asp:Label ID="Label49" Text="Net Amount (With TCS)" runat="server"></asp:Label>
                                                            </td>
                                                            <td width="100" class="field_input">
                                                                <asp:TextBox runat="server" ID="txtTCSNetAmt" CssClass="x_large" placeholder="Net Amount(With TCS)" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        </div>
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
                                                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="mid" TextMode="MultiLine" Style="width: 50%; height: 40px"
                                                                MaxLength="50"> </asp:TextBox>
                                                        </td>

                                                        <td align="left" width="100" class="innerfield_title">
                                                            <asp:Label ID="lblQcRemarks" Text="Qc Remarks" runat="server"></asp:Label>
                                                        </td>
                                                        <td width="325">
                                                            <asp:TextBox ID="txtQcRemarks" runat="server" CssClass="mid" TextMode="MultiLine" Style="width: 50%; height: 40px"
                                                                MaxLength="50" Enabled="false"> </asp:TextBox>
                                                        </td>

                                                        <td width="100" align="left" class="innerfield_title">
                                                            <asp:Label ID="lblCheckerNote" Text="Rejection Note" runat="server"></asp:Label>
                                                        </td>
                                                        <td width="310">
                                                            <asp:TextBox ID="txtCheckerNote" runat="server" TextMode="MultiLine" Enabled="false"
                                                                MaxLength="255" Style="width: 50%; height: 40px"> </asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="8" colspan="4"></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">&nbsp;
                                                        </td>
                                                        <td colspan="3">
                                                            <div class="btn_24_blue" id="btnsubmitdiv" runat="server">
                                                                <span class="icon disk_co"></span>
                                                                <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="btn_link" OnClick="btnSubmit_Click"
                                                                    ValidationGroup="Save" />
                                                            </div>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                            <div class="btn_24_blue">
                                                                <span class="icon cross_octagon_co"></span>
                                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn_link" CausesValidation="false"
                                                                    OnClick="btnCancel_Click" />
                                                            </div>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                            <div class="btn_24_green" id="divbtnapprove" runat="server">
                                                                <span class="icon approve_co"></span>
                                                                <asp:LinkButton ID="btnApprove" runat="server" Text="Approve" CssClass="btn_link"
                                                                    CausesValidation="false" OnClick="btnApprove_Click" />
                                                            </div>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                            <div class="btn_24_red" id="divbtnrejection" runat="server">
                                                                <span class="icon reject_co"></span>
                                                                <asp:LinkButton ID="btnreject" runat="server" Text="Reject" CssClass="btn_link" CausesValidation="false"
                                                                    OnClick="btnreject_Click" />
                                                            </div>
                                                            &nbsp;&nbsp;
                                                            <div class="btn_24_blue" runat="server" id="div_btnPrint" visible="false">
                                                                <span class="icon printer_co"></span>
                                                                <asp:Button ID="btn_print" runat="server" Text="Print" CssClass="btn_link" CausesValidation="false"
                                                                    OnClick="btnPrint_Click" />
                                                            </div>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                            <div class="btn_24_blue" id="divDocuments" runat="server">
                                                                <span class="icon exclamation_co"></span>
                                                                <asp:Button ID="btnDocuments" runat="server" Text="Qc_Documents" CssClass="btn_link"
                                                                    CausesValidation="false" OnClick="btnDocuments_Click" />
                                                            </div>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                            <div class="btn_24_blue" id="divGrnSample" runat="server">
                                                                <span class="icon exclamation_co"></span>
                                                                <asp:Button ID="btnGrnSample" runat="server" Text="StockinSample" CssClass="btn_link"
                                                                    CausesValidation="false" OnClick="btnGrnSample_Click" />
                                                            </div>
                                                            <td class="field_title" runat="server" id="tdlblcapacitychk" style="display:none">
                                                                <asp:Label ID="lblcapacitychk" runat="server" Text="Capacity Check" ForeColor="Blue"></asp:Label>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                            </td>
                                                            <td class="field_input" width="50" runat="server" id="tdChkCapacity" style="display:none">
                                                                <asp:CheckBox ID="ChkCapacity" runat="server"
                                                                    OnCheckedChanged="ChkCapacity_Check" CausesValidation="false" AutoPostBack="true" />
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                            <div class="btn_24_blue" id="divshowcapacity" runat="server">
                                                                <span class="icon exclamation_co"></span>
                                                                <asp:Button ID="btnshowcapacity" runat="server" Text="Documents" CssClass="btn_link"
                                                                    CausesValidation="false" OnClick="btnshowcapacity_Click" />
                                                            </div>
                                                            </td>
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
                                                        <td width="165">
                                                            <asp:TextBox ID="txtFromDate" runat="server" MaxLength="10" Width="80" Enabled="false"
                                                                placeholder="dd/MM/yyyy" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="Search"
                                                                Font-Bold="true"></asp:TextBox>
                                                            <asp:ImageButton ID="imgPopup" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarFromDate" PopupButtonID="imgPopup" runat="server"
                                                                TargetControlID="txtFromDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                        <td width="70">
                                                            <asp:Label ID="Label19" runat="server" Text="To Date"></asp:Label>
                                                        </td>
                                                        <td width="165">
                                                            <asp:TextBox ID="txtToDate" runat="server" MaxLength="10" Width="80" Enabled="false"
                                                                placeholder="dd/mm/yyyy" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="Search"
                                                                Font-Bold="true"></asp:TextBox>
                                                            <asp:ImageButton ID="imgPopuptodate" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtenderToDate" PopupButtonID="imgPopuptodate"
                                                                runat="server" TargetControlID="txtToDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                        <td width="110" valign="top">
                                                            <div class="btn_24_blue">
                                                                <span class="icon find_co"></span>
                                                                <asp:Button ID="btnSearchDespatch" runat="server" Text="Search" CssClass="btn_link"
                                                                    ValidationGroup="Search" OnClick="btnSearchDespatch_Click" />
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
                                            FolderStyle="../GridStyles/premiere_blue" AutoGenerateColumns="false" OnDeleteCommand="DeleteRecordDespatch"
                                            AllowAddingRecords="false" AllowFiltering="true" AllowPageSizeSelection="false"
                                            AllowPaging="true" PageSize="30" OnRowDataBound="grdDespatchHeader_RowDataBound">
                                            <ClientSideEvents OnBeforeClientDelete="OnBeforeDespatchDelete" OnClientDelete="OnDeleteDespatchDetails" />
                                            <FilteringSettings InitialState="Visible" />
                                            <Columns>
                                                <cc1:Column ID="Column12" DataField="Sl" HeaderText="SL" runat="server" Width="50">
                                                    <TemplateSettings TemplateId="slnoTemplateIN" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column19" DataField="STOCKRECEIVEDID" HeaderText="STOCKRECEIVEDID"
                                                    runat="server" Width="60" Visible="false" />
                                                <cc1:Column ID="Column1" DataField="STOCKDESPATCHID" HeaderText="STOCKDESPATCHID"
                                                    runat="server" Width="60" Visible="false" />
                                                <cc1:Column ID="Column91" DataField="STOCKRECEIVEDNO" HeaderText="GRN NO." runat="server"
                                                    Width="120">
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
                                                    Width="100">
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
                                                <cc1:Column ID="Column99" DataField="WAYBILLKEY" HeaderText="WAYBILL KEY" runat="server"
                                                    Width="100">
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
                                                <cc1:Column ID="Column14" DataField="INVOICENO" HeaderText="INVOICE NO" runat="server"
                                                    Width="100">
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
                                                    Width="90" Visible="false" />
                                                <cc1:Column ID="Column16" DataField="LRGRNO" HeaderText="LR/GR NO" runat="server"
                                                    Width="100">
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
                                                <cc1:Column ID="Column89" DataField="VENDORNAME" HeaderText="VENDORNAME" runat="server"
                                                    Width="180" Wrap="true">
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
                                                <cc1:Column ID="Column11" DataField="TotalNETAmount" HeaderText="NETAMOUNT" runat="server"
                                                    Width="180" Wrap="true">
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
                                                    Width="100" />
                                                <cc1:Column ID="Column7" DataField="FORMFLAG" HeaderText="FORMFLAG" runat="server"
                                                    Width="90" Visible="false" />
                                                <cc1:Column ID="Column26" DataField="NEXTLEVELID" HeaderText="NEXTLEVELID" runat="server"
                                                    Visible="false" Width="100" />
                                                <cc1:Column ID="Column24" DataField="ISVERIFIED" HeaderText="ISVERIFIED" Visible="false"
                                                    runat="server" Width="100" />
                                                <cc1:Column ID="Column25" DataField="ISVERIFIEDDESC" HeaderText="STATUS" runat="server"
                                                    Width="100" Wrap="true">
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
                                                <cc1:Column ID="Column4" DataField="WAYBILLNO" HeaderText="WAYBILLNO" Visible="false"
                                                    runat="server" Width="100" />
                                                <cc1:Column ID="Column129" AllowEdit="false" AllowDelete="false" HeaderText="Update Waybill"
                                                    runat="server" Width="60" Wrap="true" Visible="false">
                                                    <TemplateSettings TemplateId="editWaybillBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column888" AllowEdit="false" AllowDelete="false" HeaderText="Update C-Form"
                                                    runat="server" Width="60" Wrap="true" Visible="false">
                                                    <TemplateSettings TemplateId="editCFormBtnTemplate" />
                                                </cc1:Column>

                                                <cc1:Column ID="Column_Print" AllowEdit="false" AllowDelete="true" HeaderText="PRINT" runat="server"
                                                    Width="60">
                                                    <TemplateSettings TemplateId="PrintBtnTemplate" />
                                                </cc1:Column>

                                                <cc1:Column ID="Column92" AllowEdit="true" AllowDelete="true" HeaderText="EDIT" runat="server"
                                                    Width="60">
                                                    <TemplateSettings TemplateId="editBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column6" AllowEdit="true" AllowDelete="true" HeaderText="VIEW" runat="server"
                                                    Width="60">
                                                    <TemplateSettings TemplateId="ViewBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column HeaderText="DELETE" AllowEdit="false" AllowDelete="true" Width="75">
                                                    <TemplateSettings TemplateId="deleteBtnTemplateDespatch" />
                                                </cc1:Column>
                                            </Columns>
                                            <FilteringSettings MatchingType="AnyFilter" />
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="editBtnTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="action-icons c-edit" id="btnGridEdit_<%# Container.PageRecordIndex %>"
                                                            onclick="CallServerMethod(this)"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="ViewBtnTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="h_icon zoom_sl" id="btnGridEdit_<%# Container.PageRecordIndex %>"
                                                            onclick="CallServerViewMethod(this)"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="editWaybillBtnTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="filter_btn clipboard_text_co" id="btnGridWaybill_<%# Container.PageRecordIndex %>"
                                                            onclick="CallServerMethodWaybill(this)"></a>
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

                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="editCFormBtnTemplate" Visible="false">
                                                    <Template>
                                                        <a href="javascript: //" class="filter_btn clipboard_text_co" id="btnGridCForm_<%# Container.PageRecordIndex %>"
                                                            onclick="CallServerMethodCForm(this)"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="deleteBtnTemplateDespatch">
                                                    <Template>
                                                        <a href="javascript: //" class="action-icons c-delete" title="Delete" onclick="grdDespatchHeader.delete_record(this)"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                                <cc1:GridTemplate runat="server" ID="slnoTemplateIN">
                                                    <Template>
                                                        <asp:Label ID="lblslnoin" runat="server" Text='<%#Container.PageRecordIndex+1 %>'></asp:Label>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <ScrollingSettings ScrollWidth="100%" ScrollHeight="290" />
                                        </cc1:Grid>
                                        <asp:Button ID="btnPrint" runat="server" Text="Print" Style="display: none" OnClick="btnPrint_Click"
                                            CausesValidation="false" />
                                        <asp:Button ID="btngrdedit" runat="server" Text="grdedit" Style="display: none" OnClick="btngrdedit_Click"
                                            CausesValidation="false" />
                                        <asp:Button ID="btngrdUpdateWaybill" runat="server" Text="Update Waybill" Style="display: none"
                                            OnClick="btngrdUpdateWaybill_Click" CausesValidation="false" />
                                        <asp:Button ID="btngrdUpdateForm" runat="server" Text="Update C-Form" Style="display: none"
                                            OnClick="btngrdUpdateForm_Click" CausesValidation="false" />
                                        <asp:Button ID="btnview" runat="server" Text="View" Style="display: none" OnClick="btnview_Click"
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
                                        <asp:ImageButton ID="imgrejectionbtn" ImageAlign="Right" ImageUrl="~/images/Cancel.png"
                                            Height="25px" Width="25px" runat="server" />
                                        <hr />
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
                                                        <td width="30">&nbsp;
                                                        </td>
                                                        <td>
                                                            <div class="btn_24_blue">
                                                                <span class="icon add_co"></span>
                                                                <asp:Button ID="btnrejectionadd" runat="server" Text="ADD" CssClass="btn_link" ValidationGroup="check"
                                                                    OnClick="btnrejectionadd_click" CausesValidation="true" />
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="gridcontent-inner">
                                        <fieldset>
                                            <legend>REJECTION DETAILS</legend>
                                            <cc1:Grid ID="gvrejectiondetails" runat="server" CallbackMode="true" AutoGenerateColumns="false"
                                                FolderStyle="../GridStyles/premiere_blue" AllowAddingRecords="false" AllowFiltering="true"
                                                AllowPageSizeSelection="false" PageSize="50" AllowPaging="false" EnableRecordHover="true">
                                                <FilteringSettings InitialState="Visible" />
                                                <Columns>
                                                    <cc1:Column ID="Column1111" DataField="GUID" ReadOnly="true" HeaderText="GUID" runat="server"
                                                        Visible="false" Width="10" />
                                                    <cc1:Column ID="Column233" DataField="STOCKRECEIVEDID" ReadOnly="true" HeaderText="STOCKRECEIVEDID"
                                                        runat="server" Visible="false" Width="10" />
                                                    <cc1:Column ID="Column211" DataField="POID" ReadOnly="true" HeaderText="POID" runat="server"
                                                        Visible="false" Width="10" />
                                                    <cc1:Column ID="Column222" DataField="STOCKDESPATCHID" ReadOnly="true" HeaderText="STOCKDESPATCHID"
                                                        runat="server" Visible="false" Width="10" />
                                                    <cc1:Column ID="Column9" DataField="PRODUCTID" ReadOnly="true" HeaderText="ID" runat="server"
                                                        Visible="false" Width="10" />
                                                    <cc1:Column ID="Column112" DataField="Sl" HeaderText="SL" runat="server" Width="70"
                                                        Wrap="true">
                                                        <TemplateSettings TemplateId="slnoTemplate" />
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column13" DataField="PRODUCTNAME" HeaderText="PRODUCT NAME" runat="server"
                                                        Wrap="true" Width="380">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column18" DataField="BATCHNO" HeaderText="BATCH NO" runat="server"
                                                        Wrap="true" Width="120">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column51" DataField="REJECTIONQTY" HeaderText="QTY" runat="server"
                                                        Width="100" Wrap="true">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column20" DataField="PACKINGSIZENAME" HeaderText="PACK SIZE" runat="server"
                                                        Wrap="true" Width="200">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column21" DataField="REASONNAME" HeaderText="REJECTION REASON" runat="server"
                                                        Width="350" Wrap="true">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column22" DataField="REASONID" HeaderText="REASONID" runat="server"
                                                        Visible="false" Width="10" Wrap="true">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column28" DataField="DEPOTRATE" HeaderText="DEPOTRATE" runat="server"
                                                        Visible="false" Width="10" Wrap="true">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column29" DataField="STORELOCATIONID" HeaderText="STORELOCATIONID"
                                                        runat="server" Visible="false" Width="10" Wrap="true">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column30" DataField="STORELOCATIONNAME" HeaderText="STORELOCATIONNAME"
                                                        runat="server" Visible="false" Width="10" Wrap="true">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column331" DataField="MFDATE" HeaderText="MFDATE" runat="server"
                                                        Visible="false" Width="10" Wrap="true">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column332" DataField="EXPRDATE" HeaderText="EXPRDATE" runat="server"
                                                        Visible="false" Width="10" Wrap="true">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column333" DataField="ASSESMENTPERCENTAGE" HeaderText="ASSESMENTPERCENTAGE"
                                                        runat="server" Visible="false" Width="10" Wrap="true">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column334" DataField="MRP" HeaderText="MRP" runat="server" Visible="false"
                                                        Width="10" Wrap="true">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column335" DataField="WEIGHT" HeaderText="WEIGHT" runat="server"
                                                        Visible="false" Width="10" Wrap="true">
                                                    </cc1:Column>
                                                    <cc1:Column HeaderText="DELETE" AllowEdit="false" AllowDelete="true" Width="80" Wrap="true">
                                                        <TemplateSettings TemplateId="deleteBtnTemplate" />
                                                    </cc1:Column>
                                                </Columns>
                                                <FilteringSettings MatchingType="AnyFilter" />
                                                <Templates>
                                                    <cc1:GridTemplate runat="server" ID="deleteBtnTemplate">
                                                        <Template>
                                                            <a href="javascript: //" class="action-icons c-delete" id="btnGridRejectionDelete_<%# Container.PageRecordIndex %>"
                                                                onclick="CallRejectionDeleteServerMethod(this)"></a>
                                                        </Template>
                                                    </cc1:GridTemplate>
                                                    <cc1:GridTemplate runat="server" ID="slnoTemplate">
                                                        <Template>
                                                            <asp:Label ID="lblslno" runat="server" Text='<%#Container.PageRecordIndex+1 %>'></asp:Label>
                                                        </Template>
                                                    </cc1:GridTemplate>
                                                </Templates>
                                                <ScrollingSettings ScrollWidth="100%" ScrollHeight="110" NumberOfFixedColumns="7" />
                                            </cc1:Grid>
                                            <asp:Button ID="btnRejectiongrddelete" runat="server" Text="grddelete" Style="display: none"
                                                OnClick="btnRejectiongrddelete_Click" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete this record?');" />
                                            <asp:HiddenField runat="server" ID="hdn_guid" />
                                            <asp:HiddenField runat="server" ID="hdnpoid" />
                                            <asp:HiddenField runat="server" ID="hdnproductid" />
                                            <asp:HiddenField runat="server" ID="hdnReceivedID" />
                                            <asp:HiddenField runat="server" ID="hdnGRNID" />
                                            <asp:HiddenField runat="server" ID="hdnMfDate" />
                                            <asp:HiddenField runat="server" ID="hdnExprDate" />
                                            <asp:HiddenField runat="server" ID="hdnAFTERDISCOUNTAMT" />
                                            <asp:HiddenField runat="server" ID="hdnRCVDQTY" />
                                            <asp:HiddenField runat="server" ID="hdnRejBatch" />
                                        </fieldset>
                                    </div>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td align="center" colspan="2" style="padding-top: 10px;">
                                                <div class="btn_24_blue">
                                                    <span class="icon disk_co"></span>
                                                    <asp:Button ID="btnrejectionsubmit" runat="server" CssClass="btn_link" Text="Save"
                                                        CausesValidation="false" OnClick="btnrejectionsubmit_click" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
                                                    <span class="icon cross_octagon_co"></span>
                                                    <asp:Button ID="btnrejectioncancel" runat="server" CssClass="btn_link" Text="Cancel"
                                                        CausesValidation="false" OnClientClick="return Hidepopup()" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
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

                                <asp:Panel ID="Pnlworkorder" runat="server" CssClass="modalPopup" Style="display: none; border-radius: 16px;"
                                    Width="90%" Height="87%" BackColor="White" BorderColor="#257daf"
                                    BorderWidth="8px" BorderStyle="Solid">
                                    <div style="background-color: #257daf; border-style: solid; border-color: Yellow; background-position: center; background-repeat: no-repeat; background-size: cover; height: 6%"
                                        align="center">

                                        <asp:Label Font-Bold="True" ID="lblpagedetails" runat="server" Text="JOB ORDER DETAILS"
                                            Font-Size="X-Large" ForeColor="Yellow"></asp:Label>
                                        <asp:ImageButton ID="ImgpageCancel" ImageAlign="Right" ImageUrl="~/images/Cancel.png"
                                            Height="25px" Width="25px" runat="server" />
                                    </div>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td width="13%" class="field_title">
                                                <asp:Label ID="lblpopproduct" Text="Product" runat="server"></asp:Label>
                                            </td>
                                            <td class="field_input">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="45%">
                                                            <asp:TextBox ID="txtpopproduct" runat="server" CssClass="full" Enabled="false"
                                                                ForeColor="Black"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblJobdespatchID" runat="server" Visible="false"></asp:Label></td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="align-content: center" class="field_title">
                                                <asp:Label ID="lblreceive" runat="server" Text="Receive Qty"></asp:Label>
                                                <asp:TextBox ID="txtReceive" runat="server" Width="50px" OnTextChanged="txtReceive_TextChanged" AutoPostBack="true"
                                                    onkeypress="return isNumberKey(event);" onfocus="disableautocompletion(this.id);">
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>

                                    <div id="Div4" class="gridcontent-inner" runat="server">
                                        <fieldset>
                                            <legend>DISPATCH DETAILS</legend>
                                            <div style="overflow: hidden; width: 100%;" id="DivHeaderRow">
                                            </div>
                                            <div style="overflow: scroll; height: 250px;" onscroll="OnScrollDiv(this)" id="DivMainContent">
                                                <asp:GridView ID="gvJobOrderdetails" EmptyDataText="There are no records available."
                                                    CssClass="zebra" runat="server" AutoGenerateColumns="False" onchange="MakeStaticHeader('<%= grdclosingdetails.ClientID %>', 200, '100%' , 30 ,false)"
                                                    Width="100%">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SL">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="POID" Visible="false" HeaderStyle-Wrap="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPoID" runat="server" Text='<%# Eval("POID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="STOCKDESPATCHID" Visible="false" HeaderStyle-Wrap="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStockdespatchID" runat="server" Text='<%# Eval("STOCKDESPATCHID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <%--<asp:TemplateField HeaderText="DISPATCH NO">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStockdespatchNO" runat="server" Text='<%# Eval("STOCKDESPATCHNO") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>

                                                        <asp:TemplateField HeaderText="CATEGORYID" Visible="false" HeaderStyle-Wrap="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCategoryID" runat="server" Text='<%# Eval("CATEGORYID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CATEGORY NAME">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCategoryName" runat="server" Text='<%# Eval("CATEGORYNAME") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MATERIALID" Visible="false" HeaderStyle-Wrap="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMaterialID" runat="server" Text='<%# Eval("PRODUCTID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MATERIALNAME">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMaterialName" runat="server" Text='<%# Eval("PRODUCTNAME") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PO QTY">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPoQty" runat="server" Text='<%# Eval("POQTY") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ISSUE QTY">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblissueQTY" runat="server" Text='<%# Eval("ISSUEQTY") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DISPATCH QTY">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDisptchQty" runat="server" Text='<%# Eval("DISPTCHQTY") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="ALLREADY RECEIVED">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAllredy" runat="server" Text='<%# Eval("RECEIVEQTY") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="BALANCE QTY">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBalanceQty" runat="server" Text='<%# Eval("BALANCEQTY") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="RECEIVE QTY" HeaderStyle-Width="70">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtReceiveQty" runat="server" MaxLength="10"
                                                                    onkeypress="return isNumberKey(event);" onfocus="disableautocompletion(this.id);" Enabled="false"
                                                                    Style="text-align: right;" Width="70px" Height="20" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div id="DivFooterRow" style="overflow: hidden; align-content: center">
                                                <asp:Label ID="lbltotalrceived" Text="Total Received" runat="server" class="field_title"></asp:Label>
                                                <asp:TextBox ID="txttotalreceved" runat="server" Enabled="false" ForeColor="Black" class="field_title" Width="75px"></asp:TextBox>
                                            </div>
                                        </fieldset>
                                    </div>

                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td align="center" colspan="2" style="padding-top: 10px;">
                                                <div class="btn_24_blue">
                                                    <span class="icon disk_co"></span>
                                                    <asp:Button ID="BtnDispatchSave" runat="server" CssClass="btn_link" Text="Save"
                                                        CausesValidation="false" OnClick="BtnDispatchSave_click" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
                                                    <span class="icon cross_octagon_co"></span>
                                                    <asp:Button ID="BtnDispatchCancel" runat="server" CssClass="btn_link" Text="Cancel"
                                                        CausesValidation="false" OnClientClick="return Hidepopup()" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <ajaxToolkit:ModalPopupExtender ID="WorkOrderPopup" runat="server" DropShadow="false" PopupControlID="Pnlworkorder"
                                    TargetControlID="ddlpo" BackgroundCssClass="modalBackground">
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
                            <tr>
                                <td>&nbsp;
                                </td>
                                <td>
                                    <asp:Button ID="btnWaybillUpdate" runat="server" Text="Update" CssClass="btn_small btn_blue"
                                        OnClick="btnWaybillUpdate_Click" ValidationGroup="SaveWaybill" />&nbsp;&nbsp;<asp:Button
                                            ID="btnCloseLightbox" runat="server" Text="Close" CssClass="btn_small btn_blue"
                                            OnClick="btnCloseLightbox_Click" CausesValidation="false" />
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
                                    <asp:ImageButton ID="ImageButton233" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                        runat="server" Height="24" />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImageButton233"
                                        runat="server" TargetControlID="txtCFormPopupDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td class="field_input">
                                    <asp:Button ID="btnCFormUpdate" runat="server" Text="Update" CssClass="btn_small btn_blue"
                                        OnClick="btnCFormUpdate_Click" />&nbsp;&nbsp;<asp:Button ID="btnCloseLightbox2" runat="server"
                                            Text="Close" CssClass="btn_small btn_blue" OnClick="btnCloseLightbox2_Click" />
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
            <script language="javascript" type="text/javascript">
                function MakeStaticHeader(gridId, height, width, headerHeight, isFooter) {
                    var tbl = document.getElementById(gridId);
                    if (tbl) {
                        var DivHR = document.getElementById('DivHeaderRow');
                        var DivMC = document.getElementById('DivMainContent');
                        var DivFR = document.getElementById('DivFooterRow');

                        //*** Set divheaderRow Properties ****
                        DivHR.style.height = headerHeight + 'px';
                        //DivHR.style.width = (parseInt(width) - 16) + 'px';
                        DivHR.style.width = '98%';
                        DivHR.style.position = 'relative';
                        DivHR.style.top = '0px';
                        DivHR.style.zIndex = '10';
                        DivHR.style.verticalAlign = 'top';

                        //*** Set divMainContent Properties ****
                        DivMC.style.width = width + 'px';
                        DivMC.style.height = height + 'px';
                        DivMC.style.position = 'relative';
                        DivMC.style.top = -headerHeight + 'px';
                        DivMC.style.zIndex = '1';

                        //*** Set divFooterRow Properties ****
                        DivFR.style.width = (parseInt(width) - 16) + 'px';
                        DivFR.style.position = 'relative';
                        DivFR.style.top = -headerHeight + 'px';
                        DivFR.style.verticalAlign = 'top';
                        DivFR.style.paddingtop = '2px';

                        if (isFooter) {
                            var tblfr = tbl.cloneNode(true);
                            tblfr.removeChild(tblfr.getElementsByTagName('tbody')[0]);
                            var tblBody = document.createElement('tbody');
                            tblfr.style.width = '100%';
                            tblfr.cellSpacing = "0";
                            tblfr.border = "0px";
                            tblfr.rules = "none";
                            //*****In the case of Footer Row *******
                            tblBody.appendChild(tbl.rows[tbl.rows.length - 1]);
                            tblfr.appendChild(tblBody);
                            DivFR.appendChild(tblfr);
                        }
                        //****Copy Header in divHeaderRow****
                        DivHR.appendChild(tbl.cloneNode(true));
                    }
                }

                function OnScrollDiv(Scrollablediv) {
                    document.getElementById('DivHeaderRow').scrollLeft = Scrollablediv.scrollLeft;
                    document.getElementById('DivFooterRow').scrollLeft = Scrollablediv.scrollLeft;
                }

                function Tax_MakeStaticHeader(gridId, height, width, headerHeight, isFooter) {
                    var tbl = document.getElementById(gridId);
                    if (tbl) {
                        var DivHR = document.getElementById('Tax_DivHeaderRow');
                        var DivMC = document.getElementById('Tax_DivMainContent');
                        var DivFR = document.getElementById('Tax_DivFooterRow');

                        //*** Set divheaderRow Properties ****
                        DivHR.style.height = headerHeight + 'px';
                        //DivHR.style.width = (parseInt(width) - 16) + 'px';
                        DivHR.style.width = '100%';
                        DivHR.style.position = 'relative';
                        DivHR.style.top = '0px';
                        DivHR.style.zIndex = '10';
                        DivHR.style.verticalAlign = 'top';

                        //*** Set divMainContent Properties ****
                        DivMC.style.width = width + 'px';
                        DivMC.style.height = height + 'px';
                        DivMC.style.position = 'relative';
                        DivMC.style.top = -headerHeight + 'px';
                        DivMC.style.zIndex = '1';

                        //*** Set divFooterRow Properties ****
                        DivFR.style.width = (parseInt(width) - 16) + 'px';
                        DivFR.style.position = 'relative';
                        DivFR.style.top = -headerHeight + 'px';
                        DivFR.style.verticalAlign = 'top';
                        DivFR.style.paddingtop = '2px';

                        if (isFooter) {
                            var tblfr = tbl.cloneNode(true);
                            tblfr.removeChild(tblfr.getElementsByTagName('tbody')[0]);
                            var tblBody = document.createElement('tbody');
                            tblfr.style.width = '100%';
                            tblfr.cellSpacing = "0";
                            tblfr.border = "0px";
                            tblfr.rules = "none";
                            //*****In the case of Footer Row *******
                            tblBody.appendChild(tbl.rows[tbl.rows.length - 1]);
                            tblfr.appendChild(tblBody);
                            DivFR.appendChild(tblfr);
                        }
                        //****Copy Header in divHeaderRow****
                        DivHR.appendChild(tbl.cloneNode(true));
                    }
                }

                function Tax_OnScrollDiv(Scrollablediv) {
                    document.getElementById('Tax_DivHeaderRow').scrollLeft = Scrollablediv.scrollLeft;
                    document.getElementById('Tax_DivFooterRow').scrollLeft = Scrollablediv.scrollLeft;
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

                function isNumberKeyWithSlashHyphenAtZ(evt) {
                    var charCode = (evt.which) ? evt.which : event.keyCode;
                    if ((charCode < 47 || charCode > 57) && (charCode != 45) && (charCode < 65 || charCode > 90) && (charCode < 97 || charCode > 122))
                        return false;

                    return true;
                }
            </script>

            <script type="text/javascript">
                function CHKInvoiceNo() {
                    var txtInvoiceNo1 = "ContentPlaceHolder1_txtInvoiceNo";
                    var txtInvoiceNo = document.getElementById(txtInvoiceNo1).value;
                    var txtInvoiceNoL = txtInvoiceNo.length;
                    for (var i = 0; i < txtInvoiceNoL; i++) {

                        var n1 = txtInvoiceNo.charCodeAt(i);
                        if ((n1 >= 47 && n1 <= 57) || (n1 >= 65 && n1 <= 90) || (n1 >= 97 && n1 <= 122) || n1 == 45) {

                        }
                        else {
                            alert('Invalid Invoice No.');
                            document.getElementById(txtInvoiceNo1).value = "";
                            return false;
                        }
                    }
                    var j = 0;
                    for (var i = 0; i < txtInvoiceNoL; i++) {

                        var n1 = txtInvoiceNo.charCodeAt(i);
                        if (n1 != 48) {
                            j = 1;
                        }
                    }
                    if (j == 0) {
                        alert('Invalid Invoice No.');
                        document.getElementById(txtInvoiceNo1).value = "";
                        return false;
                    }


                    return true;
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>