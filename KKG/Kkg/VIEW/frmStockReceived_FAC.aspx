<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmStockReceived_FAC.aspx.cs" Inherits="VIEW_frmStockReceived_FAC" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../js/jquery.blockUI.js" type="text/javascript"></script>
    <script type="text/javascript">
        function BlockUI(elementID) {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_beginRequest(function () {
                $("#" + elementID).block({ message: '<table align = "center"><tr><td>' +
     '<img src="../images/loading123.gif"/></td></tr></table>',
                    css: {},
                    overlayCSS: { background: 'transparent'
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

        function CallDeleteServerMethod(oLink) {
            var iRecordIndexdelete = oLink.id.toString().replace("btnGridDelete_", "");
            document.getElementById("<%=hdn_guid.ClientID %>").value = gvrejectiondetails.Rows[iRecordIndexdelete].Cells[0].Value;
            document.getElementById("<%=btngrddelete.ClientID %>").click();
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
    </script>
    <script type="text/javascript">
        function disableautocompletion(id) {
            var TextBoxControl = document.getElementById(id);
            TextBoxControl.setAttribute("autocomplete", "off");
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
            grdReceivedHeader.addFilterCriteria('STOCKDESPATCHNO', OboutGridFilterCriteria.Contains, searchValue);
            grdReceivedHeader.addFilterCriteria('DESPATCHDATE', OboutGridFilterCriteria.Contains, searchValue);
            grdReceivedHeader.addFilterCriteria('WAYBILLNO', OboutGridFilterCriteria.Contains, searchValue);
            grdReceivedHeader.addFilterCriteria('INVOICENO', OboutGridFilterCriteria.Contains, searchValue);
            grdReceivedHeader.addFilterCriteria('VEHICHLENO', OboutGridFilterCriteria.Contains, searchValue);
            grdReceivedHeader.addFilterCriteria('LRGRNO', OboutGridFilterCriteria.Contains, searchValue);
            grdReceivedHeader.addFilterCriteria('TRANSPORTERNAME', OboutGridFilterCriteria.Contains, searchValue);
            grdReceivedHeader.addFilterCriteria('MOTHERDEPOTNAME', OboutGridFilterCriteria.Contains, searchValue);
            grdReceivedHeader.addFilterCriteria('TPUNAME', OboutGridFilterCriteria.Contains, searchValue);
            grdReceivedHeader.executeFilter();
            searchTimeout = null;
            return false;
        }

        function OnBeforeDespatchDelete(record) {
            record.Error = '';
            document.getElementById("<%=hdnReceivedID.ClientID %>").value = record.STOCKRECEIVEDID;
            if (confirm("Are you sure you want to delete? "))
                return true;
            else
                return false;
        }
        function OnDeleteReceivedDetails(record) {
            alert(record.Error);
        }

        function CallServerMethod(oLink) {
            var iRecordIndex = oLink.id.toString().replace("btnGridEdit_", "");
            document.getElementById("<%=hdnReceivedID.ClientID %>").value = grdReceivedHeader.Rows[iRecordIndex].Cells[1].Value;
            document.getElementById("<%=hdnDepotID.ClientID %>").value = grdReceivedHeader.Rows[iRecordIndex].Cells[18].Value;
            document.getElementById("<%=btngrdedit.ClientID %>").click();

        }

    </script>
    <script type="text/javascript">
        function CallServerMethodPrint(oLink) {
            var iRecordIndex = oLink.id.toString().replace("btnPrint_", "");
            document.getElementById("<%=hdnReceivedID.ClientID %>").value = grdReceivedHeader.Rows[iRecordIndex].Cells[1].Value;
            document.getElementById("<%=Hdn_Print.ClientID %>").value = grdReceivedHeader.Rows[iRecordIndex].Cells[4].Value;
            document.getElementById("<%=btnPrint.ClientID %>").click();

        }
    </script>
    <asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
            <asp:Image ID="Image1" ImageUrl="~/images/loading123.gif" AlternateText="Processing"
                runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
        PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>
                                    Purchase Stock Receipt Details</h6>
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
                                                    <legend>Received Entry Details</legend>
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                                        <tr runat="server" id="trAutoReceivedNo">
                                                            <td class="field_title">
                                                                <asp:Label ID="lblStockRcvdNo" Text="Received No." runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input" colspan="1">
                                                                <asp:TextBox ID="txtRceivedNo" runat="server" Width="300" placeholder="Auto Generate No."
                                                                    Font-Bold="true"> </asp:TextBox>
                                                            </td>
                                                            <td width="95" class="field_title">
                                                                <asp:Label ID="Label3" Text="Received Date" runat="server"></asp:Label>
                                                            </td>
                                                            <td width="198" class="field_input">
                                                                <asp:TextBox ID="txtreceiveddate" runat="server" Width="120" placeholder="dd/MM/yyyy"
                                                                    Enabled="false" Font-Bold="true"> </asp:TextBox>
                                                                <asp:ImageButton ID="imgbtnCalendar" runat="server" ImageUrl="~/images/calendar.png"
                                                                    Width="24px" Height="24px" ImageAlign="AbsMiddle" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtreceiveddate"
                                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="Save"
                                                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtreceiveddate"
                                                                    PopupButtonID="imgbtnCalendar" runat="server" Format="dd/MM/yyyy" Enabled="true"
                                                                    CssClass="cal_Theme1" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="100" class="field_title">
                                                                <asp:Label ID="Label1" Text="Invoice No." runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtDespatchNo" runat="server" Width="120"></asp:TextBox>
                                                                </td>
                                                            <td width="203" style="display:none">
                                                                <asp:DropDownList ID="ddldespatchno" Width="200" runat="server" class="chosen-select"
                                                                    OnSelectedIndexChanged="ddldespatchno_SelectedIndexChanged" AutoPostBack="true">
                                                                </asp:DropDownList>
                                                            </td>
                                                             <td class="field_title">
                                                                <asp:Label ID="lblInvoiceDate" Text="Invoice Date" runat="server"></asp:Label>&nbsp;<span
                                                                    class="req">*</span>
                                                            </td>
                                                             <td class="field_input">
                                                                <asp:TextBox ID="txtInvoiceDate" runat="server" Width="120" placeholder="dd/MM/yyyy"
                                                                    Enabled="false" Font-Bold="true"> </asp:TextBox>
                                                                  <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/calendar.png"
                                                                    Width="24px" Height="24px" ImageAlign="AbsMiddle" />
                                                                 <ajaxToolkit:CalendarExtender ID="CalendarExtender4" TargetControlID="txtInvoiceDate"
                                                                    PopupButtonID="ImageButton1" runat="server" Format="dd/MM/yyyy" Enabled="true"
                                                                    CssClass="cal_Theme1" />
                                                            </td>
                                                            
                                                            <td width="110" class="field_title" style="display:none">
                                                                <asp:Label ID="Label2" Text="Despatch Date" runat="server"></asp:Label>&nbsp;<span
                                                                    class="req">*</span>
                                                            </td>
                                                            <td class="field_input" style="display:none">
                                                                <asp:TextBox ID="txtDespatchDate" runat="server" Width="120" placeholder="dd/MM/yyyy"
                                                                    MaxLength="10" AutoCompleteType="Disabled" onfocus="disableautocompletion(this.id);"></asp:TextBox>
                                                                
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field_title">
                                                                <asp:Label ID="lblTransportMode" Text="Mode of Transport" runat="server"></asp:Label>&nbsp;<span
                                                                    class="req">*</span>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:DropDownList ID="ddlTransportMode" Width="180" runat="server" class="chosen-select"
                                                                    data-placeholder="Choose Transport Mode" ValidationGroup="Save" disabled="disabled">
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
                                                                <asp:Label ID="lblTransporter" Text="Transporter" runat="server"></asp:Label>&nbsp;<span
                                                                    class="req">*</span>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:DropDownList ID="ddlTransporter" Width="200" runat="server" class="chosen-select"
                                                                    data-placeholder="Choose Transporter" AppendDataBoundItems="True" ValidationGroup="Save">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorTransporter" runat="server"
                                                                    ControlToValidate="ddlTransporter" ValidateEmptyText="false" SetFocusOnError="true"
                                                                    ErrorMessage="Required!" ValidationGroup="Save" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td class="field_title">
                                                                <asp:Label ID="Label14" Text="Received&nbsp;Depot" runat="server"></asp:Label>&nbsp;<span
                                                                    class="req">*</span>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:DropDownList ID="ddlDepot" Width="300" runat="server" class="chosen-select"
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
                                                                <asp:TextBox ID="txtLRGRNo" runat="server" Width="170" placeholder="LR/GR No" ValidationGroup="Save"
                                                                    AutoCompleteType="Disabled" onfocus="disableautocompletion(this.id);"> </asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorLRGRNo" runat="server" ControlToValidate="txtLRGRNo"
                                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="Save"
                                                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td class="field_title">
                                                                <asp:Label ID="lblLRGRDate" Text="LR/GR Date" runat="server"></asp:Label>&nbsp;<span
                                                                    class="req">*</span>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtLRGRDate" runat="server" Width="170" placeholder="dd/MM/yyyy"
                                                                    MaxLength="10" ValidationGroup="Save" AutoCompleteType="Disabled" onfocus="disableautocompletion(this.id);"></asp:TextBox>
                                                            </td>
                                                            <td class="field_title">
                                                                <asp:Label ID="Label4" Text="TPU NAME" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:DropDownList ID="ddlTPU" runat="server" class="chosen-select" data-placeholder="Choose TPU"
                                                                    AppendDataBoundItems="True" Width="300">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr style="display: none;">
                                                            <td class="field_title">
                                                                <asp:Label ID="lblInvoiceNo" Text="Invoice No" runat="server"></asp:Label>&nbsp;<span
                                                                    class="req">*</span>
                                                            </td>
                                                            <td class="field_input" colspan="5">
                                                                <asp:TextBox ID="txtInvoiceNo" runat="server" Width="170" placeholder="Invoice No"
                                                                    ValidationGroup="Save" AutoCompleteType="Disabled" onfocus="disableautocompletion(this.id);"> </asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorInvoiceNo" runat="server" ControlToValidate="txtInvoiceNo"
                                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="Save"
                                                                    ForeColor="Red"></asp:RequiredFieldValidator><br />
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field_title">
                                                                <asp:Label ID="lblVehicle" Text="Vehicle No." runat="server"></asp:Label>&nbsp;<span
                                                                    class="req">*</span>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtVehicle" runat="server" Width="170" placeholder="Vehicle No"
                                                                    ValidationGroup="Save" AutoCompleteType="Disabled" onfocus="disableautocompletion(this.id);"> </asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorVehicle" runat="server" ControlToValidate="txtVehicle"
                                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="Save"
                                                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td class="field_title">
                                                                <asp:Label ID="lblWayBill" Text="WayBill No." runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtWayBill" runat="server" Width="170" placeholder="Waybill Key"
                                                                    AutoCompleteType="Disabled" onfocus="disableautocompletion(this.id);"> </asp:TextBox>
                                                            </td>
                                                             <td class="field_title">
                                                                <asp:Label ID="Label31" Text="E-Invoice No" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtEInvoiceNo" runat="server" Width="170" placeholder="E-Invoice No"
                                                                    AutoCompleteType="Disabled" MaxLength="15" onfocus="disableautocompletion(this.id);"> </asp:TextBox>
                                                            </td>
                                                           
                                                        </tr>
                                                        <tr>
                                                             <td class="field_title">
                                                                <asp:Label ID="Label13" Text="Gatepass No." runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtGatepassNo" runat="server" Width="170" placeholder="Gatepass No"
                                                                    AutoCompleteType="Disabled" onfocus="disableautocompletion(this.id);"> </asp:TextBox>
                                                            </td>
                                                            <td class="field_title">
                                                                <asp:Label ID="Label15" Text="Gatepass Date" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtGatepassDate" runat="server" Width="120" placeholder="dd/MM/yyyy"
                                                                    Enabled="false" MaxLength="10"> </asp:TextBox>&nbsp;
                                                                <asp:ImageButton ID="ImageButton111" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImageButton111"
                                                                    runat="server" TargetControlID="txtGatepassDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>

                                                        </tr>
                                                        <tr style="display: none;">
                                                            <td class="field_title">
                                                                <asp:Label ID="Label8" Text="CForm No." runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtCFormNo" runat="server" Width="170" placeholder="C Form No" Enabled="false"> </asp:TextBox>
                                                            </td>
                                                            <td class="field_title">
                                                                <asp:Label ID="Label9" Text="C-Form Date" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtCFormDate" runat="server" Width="170" placeholder="dd/MM/yyyy"
                                                                    Enabled="false" MaxLength="10" AutoCompleteType="Disabled" onfocus="disableautocompletion(this.id);"> </asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field_title">
                                                                <asp:Label ID="Label18" Text="INSURANCE COMPANY" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:DropDownList ID="ddlinsurancename" runat="server" class="chosen-select" data-placeholder="Choose Insurance Company"
                                                                    AppendDataBoundItems="True" Width="210" Enabled="false">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td class="field_title">
                                                                <asp:Label ID="Label20" Text="Insurance No." runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtinsuranceno" runat="server" Width="170" AutoCompleteType="Disabled"
                                                                    onfocus="disableautocompletion(this.id);"> </asp:TextBox>&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field_title">
                                                                <asp:Label ID="Label25" Text="Total Case" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtTotalCase" runat="server" Width="120" Enabled="false"> </asp:TextBox>
                                                            </td>
                                                            <td class="field_title">
                                                                <asp:Label ID="Label26" Text="Total PCS" runat="server"></asp:Label>
                                                            </td>

                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtTotalPCS" runat="server" Width="120" Enabled="false"> </asp:TextBox>
                                                            </td>
                                                            <td width="100" class="field_title" style="display:none">
                                                                <asp:Label ID="Label30" Text="Delivery Date" runat="server" ></asp:Label>
                                                            </td>
                                                            <td width="200" class="field_input" style="display:none">
                                                                <asp:TextBox ID="txtdeliverydate" runat="server" Width="120" Enabled="false" CssClass="x-large" ></asp:TextBox>
                                                            </td>
                                                            <td class="field_title" id="Tdlblledger" runat="server">
                                                                <asp:Label ID="lblledger" Text="Ledger" runat="server"></asp:Label>&nbsp;<span class="req">*</span>
                                                            </td>
                                                            <td class="field_input" id="Tdddlledger" runat="server">
                                                                <asp:DropDownList ID="ddlledger" Width="200" runat="server" class="chosen-select" AppendDataBoundItems="True">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="Reqrdddlledger" runat="server" ControlToValidate="ddlledger"
                                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="Approve"
                                                                    InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                         <tr id="TrExchangrate" runat="server">
                                                        <td class="field_title">
                                                                <asp:Label ID="lblexchangrate" Text="Exchange Rate" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtexchangrate" runat="server" Width="120" AutoPostBack="true" Text="0.00"
                                                                 onkeypress="return isNumberKey(event);" OnTextChanged="txtexchangrate_TextChanged" 
                                                                 onfocus="disableautocompletion(this.id);">                                                                
                                                                </asp:TextBox>
                                                            </td>
                                                             <td class="field_title">
                                                                <asp:Label ID="lblInrRate" runat="server" ForeColor="Red"></asp:Label>
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
                                            <div style="overflow: scroll; height: 300px;" onscroll="OnScrollDiv(this)" id="DivMainContent">
                                                <asp:GridView ID="grdAddDespatch" runat="server" Width="100%" CssClass="reportgrid" AutoGenerateColumns="true"
                                                    EmptyDataText="No Records Available" ShowFooter="true">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SL">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="REJECTION" HeaderStyle-ForeColor="Red" Visible="false">
                                                            <ItemTemplate>
                                                                <nav style="height: 5px; z-index: 2; position: relative; text-align: center;"><asp:Label ID="lblrejvalue" runat="server" class="badge red">0</asp:Label></nav>
                                                                <asp:ImageButton ID="edit" runat="server" value='<%# Eval("GUID") %>' CausesValidation="false"
                                                                    ToolTip="REJECTION REASON" class="action-icons c-edit" Style="margin-left: 15px;"
                                                                    OnClick="btnRejectionReason"></asp:ImageButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div id="DivFooterRow" style="overflow: hidden">
                                            </div>
                                            <br />
                                            <asp:Button ID="btnreceivedadd" runat="server" Text="Calculate" CssClass="btn_small btn_blue"
                                                ValidationGroup="Save" Visible="false" />
                                        </fieldset>
                                    </div>
                                    <div class="gridcontent-inner" style="display: none;">
                                        <fieldset>
                                            <legend>Product TAX Details</legend>
                                            <div style="overflow: hidden; width: 100%;" id="DivHeaderRowScheme">
                                            </div>
                                            <div style="overflow: scroll;" onscroll="OnScrollDivScheme(this)" id="DivMainContentScheme">
                                                <asp:GridView ID="gvProductTax" runat="server" Width="100%" AlternatingRowStyle-Height="25px"
                                                    RowStyle-Height="25px" CssClass="zebra" AutoGenerateColumns="false" EmptyDataText="No Records Available"
                                                    OnRowDataBound="gvProductTax_OnRowDataBound" ShowFooter="true">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SL">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="2%" />
                                                        </asp:TemplateField>
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
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="VALUE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblvalue" runat="server" Text='<%# Eval("TAXVALUE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div id="DivFooterRowScheme" style="overflow: hidden">
                                            </div>
                                        </fieldset>
                                    </div>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px">
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td>
                                                            <fieldset>
                                                                <legend>Item Wise Amount And Tax</legend>
                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                    <tr>
                                                                        <td width="98" class="innerfield_title">
                                                                            <asp:Label ID="Label21" Text="Tot.Basic Amt." runat="server"></asp:Label>
                                                                        </td>
                                                                        <td width="138">
                                                                            <asp:TextBox runat="server" ID="txtAmount" placeholder="Tot.Basic Amt." Enabled="false"
                                                                                Width="122"></asp:TextBox>
                                                                        </td>
                                                                        <td width="90" class="innerfield_title">
                                                                            <asp:Label ID="Label311" Text="Tot.MRP" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td width="138">
                                                                            <asp:TextBox runat="server" ID="txtTotMRP" Width="122" placeholder="Tot.MRP" Enabled="false"></asp:TextBox>
                                                                        </td>
                                                                        <td width="90" class="innerfield_title">
                                                                            <asp:Label ID="Label22" Text="Tax Amt." runat="server"></asp:Label>
                                                                        </td>
                                                                        <td width="138">
                                                                            <asp:TextBox runat="server" ID="txtTotTax" placeholder="Tax Amt." Enabled="false"
                                                                                Width="122"></asp:TextBox>
                                                                        </td>
                                                                        <td width="142" class="innerfield_title">
                                                                            <asp:Label ID="Label23" Text="(Basic&nbsp;+&nbsp;Tax)&nbsp;Amt." runat="server"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox runat="server" ID="txtNetAmt" placeholder="(Basic&nbsp;+&nbsp;Tax)&nbsp;Amt."
                                                                                Enabled="false" Width="122"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </fieldset>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px">
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td valign="top">
                                                            <fieldset>
                                                                <legend>Terms & Condition</legend>
                                                                <div class="gridcontent-shortstock">
                                                                    <cc1:Grid ID="grdTerms" runat="server" CallbackMode="true" Serialize="true" AutoGenerateColumns="false"
                                                                        FolderStyle="../GridStyles/premiere_blue" AllowAddingRecords="false" AllowFiltering="false"
                                                                        AllowPaging="false" AllowSorting="false" AllowPageSizeSelection="false" PageSize="100"
                                                                        EnableRecordHover="true">
                                                                        <FilteringSettings InitialState="Visible" />
                                                                        <Columns>
                                                                            <cc1:Column ID="Column3" DataField="SLNO" HeaderText="Sl No" runat="server" Width="60">
                                                                                <TemplateSettings TemplateId="tplTermsNumbering" />
                                                                            </cc1:Column>
                                                                            <cc1:Column ID="Column10" DataField="DESCRIPTION" ReadOnly="true" HeaderText="TERMS & Conditions"
                                                                                runat="server" Width="380" Wrap="true" />
                                                                            <cc1:Column ID="Column5" DataField="ID" ReadOnly="true" HeaderText="TERMSID" runat="server"
                                                                                Visible="false">
                                                                            </cc1:Column>
                                                                        </Columns>
                                                                        <FilteringSettings MatchingType="AnyFilter" />
                                                                        <Templates>
                                                                            <cc1:GridTemplate runat="server" ID="tplTermsNumbering">
                                                                                <Template>
                                                                                    <asp:Label ID="lblslnoTax" runat="server" Text='<%#Container.PageRecordIndex+1 %>'></asp:Label>
                                                                                </Template>
                                                                            </cc1:GridTemplate>
                                                                        </Templates>
                                                                        <ScrollingSettings ScrollWidth="100%" ScrollHeight="150" />
                                                                    </cc1:Grid>
                                                                </div>
                                                            </fieldset>
                                                        </td>
                                                        <td width="6" valign="top">
                                                        </td>
                                                        <td valign="top" style="display: none;">
                                                            <fieldset>
                                                                <legend style="display: none;">Gross Tax Details</legend>
                                                                <div class="gridcontent-shortstock" style="display: none;">
                                                                    <cc1:Grid ID="grdTax" runat="server" CallbackMode="false" Serialize="true" AutoGenerateColumns="false"
                                                                        AllowPaging="false" FolderStyle="../GridStyles/premiere_blue" AllowAddingRecords="false"
                                                                        PageSize="100" AllowFiltering="false" AllowSorting="false" AllowPageSizeSelection="false"
                                                                        EnableRecordHover="true">
                                                                        <FilteringSettings InitialState="Visible" />
                                                                        <Columns>
                                                                            <cc1:Column ID="Column2" DataField="SLNO" HeaderText="Sl No" runat="server" Width="50">
                                                                                <TemplateSettings TemplateId="tplTaxNumbering" />
                                                                            </cc1:Column>
                                                                            <cc1:Column ID="Column53" DataField="NAME" ReadOnly="true" HeaderText="TAX NAME"
                                                                                runat="server" Width="150" Wrap="true" />
                                                                            <cc1:Column ID="Column54" DataField="PERCENTAGE" ReadOnly="true" HeaderText="TAX(%)"
                                                                                runat="server" Width="100" />
                                                                            <cc1:Column ID="Column111" DataField="TAXVALUE" ReadOnly="true" HeaderText="Value"
                                                                                runat="server" Width="120" />
                                                                        </Columns>
                                                                        <FilteringSettings MatchingType="AnyFilter" />
                                                                        <Templates>
                                                                            <cc1:GridTemplate runat="server" ID="tplTaxNumbering">
                                                                                <Template>
                                                                                    <asp:Label ID="lblslnoTax" runat="server" Text='<%#Container.PageRecordIndex+1 %>'></asp:Label>
                                                                                </Template>
                                                                            </cc1:GridTemplate>
                                                                        </Templates>
                                                                        <ScrollingSettings ScrollWidth="100%" ScrollHeight="150" />
                                                                    </cc1:Grid>
                                                                </div>
                                                            </fieldset>
                                                        </td>
                                                        <td width="8">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="field_input" style="padding-left: 10px; display:none" colspan="2">
                                                            <fieldset>
                                                                <legend>Additional Details</legend>
                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                    <tr style="display:none">
                                                                        <td width="50" class="innerfield_title">
                                                                            <asp:Label ID="Label40" Text="Bill Sundry" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td width="80">
                                                                            <asp:DropDownList ID="ddlsundry" runat="server" Width="200px" AutoPostBack="true"
                                                                                AppendDataBoundItems="True" class="chosen-select" data-placeholder="Select Product">
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
                                                                            <div class="btn_24_blue" style="display: none">
                                                                                <span class="icon add_co"></span>
                                                                                <asp:Button ID="btntaxadd" runat="server" Text="ADD" CssClass="btn_link" />
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
                                                                                           <%-- <asp:TemplateField HeaderText="DELETE" ItemStyle-HorizontalAlign="Center">
                                                                                                <ItemTemplate>
                                                                                                    <asp:ImageButton ID="btn_TempDeletetax" runat="server" CssClass="action-icons c-delete"
                                                                                                        OnClientClick="return confirm('Are you sure you want to Delete?')" ToolTip="Delete" />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>--%>
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
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px">
                                                <fieldset>
                                                    <legend>Gross Amount Details</legend>
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td width="98" class="innerfield_title">
                                                                <asp:Label ID="Label24" Text="Gross&nbsp;Amt." runat="server"></asp:Label>
                                                            </td>
                                                            <td width="138">
                                                                <asp:TextBox runat="server" ID="txtTotalGross" Width="122" placeholder="Gross&nbsp;Amt."
                                                                    Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td width="90" class="innerfield_title" style="display:none">
                                                                <asp:Label ID="Label43" Text="Addn. Amt" runat="server"></asp:Label>
                                                            </td>
                                                            <td width="150" style="display:none">
                                                                <asp:TextBox runat="server" ID="txtaddnamt" CssClass="x_large" placeholder="Addn. Amt"
                                                                    Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td width="75" class="innerfield_title">
                                                                <asp:Label ID="Label27" Text="R/O" runat="server"></asp:Label>
                                                            </td>
                                                            <td width="138" class="innerfield_title">
                                                                <asp:TextBox runat="server" ID="txtRoundoff" placeholder="R/O" Enabled="true" Width="122" AutoPostBack="true" onkeypress="return isNumberKeyWithDotMinus(event);"  OnTextChanged="txtRoundoff_TextChanged">
                                                                </asp:TextBox>
                                                            </td>
                                                            <td width="120" class="innerfield_title">
                                                                <asp:Label ID="lblothchrg" Text="Other Charge Amt." runat="server"></asp:Label>
                                                            </td>
                                                            <td width="138">
                                                                <asp:TextBox runat="server" ID="txtothchrg" onkeypress="return isNumberKey(event);"
                                                                    Width="122" AutoPostBack="true" Enabled="false" placeholder="Other Charge Amt."
                                                                    OnTextChanged="txtothchrg_TextChanged">
                                                                </asp:TextBox>
                                                            </td>
                                                            <td width="60" class="innerfield_title">
                                                                <asp:Label ID="Label33" Text="Net Amt." runat="server"></asp:Label>
                                                            </td>
                                                            <td width="138">
                                                                <asp:TextBox runat="server" ID="txtFinalAmt" Width="122" placeholder="Net Amt." Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td width="8%" class="innerfield_title">
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr style="display: none;">
                                                            <td class="innerfield_title">
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td class="innerfield_title">
                                                                <asp:Label ID="Label28" Text="Other Charge" runat="server" Visible="false"></asp:Label>&nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:TextBox runat="server" ID="txtOtherCharge" Width="122" placeholder="Other Charge"
                                                                    Text="0" AutoCompleteType="Disabled" onfocus="disableautocompletion(this.id);"></asp:TextBox>
                                                            </td>
                                                            <td class="innerfield_title">
                                                                <asp:Label ID="Label29" Text="Adj. Amt" runat="server" Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox runat="server" ID="txtAdj" Width="122" placeholder="Adj.&nbsp;Amt" Text="0"
                                                                    AutoCompleteType="Disabled" onfocus="disableautocompletion(this.id);"></asp:TextBox>
                                                            </td>
                                                            <td class="innerfield_title">
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    
                                                </fieldset>
                                            </td>
                                        </tr>
                                       <tr>
                                            <td class="field_input" style="padding-left: 10px">

                                        <fieldset>
                                         
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                             <div runat="server" id="Tcs_div"> 
                                                        <tr>
                                                            <td align="left" class="field_title" >TCS<span class="req">*</span></td>
                                                            <td align="left" class="field_input">
                                                             
                                                                <asp:RadioButtonList ID="RbApplicable" AutoPostBack="true" RepeatDirection="Horizontal"
                                                                   runat="server"  OnSelectedIndexChanged="RbApplicable_SelectedIndexChanged" >
                                                                    <asp:ListItem Value="Y" >YES</asp:ListItem>
                                                               <asp:ListItem Value="N" >NO  </asp:ListItem>
                                                            </asp:RadioButtonList>
                                                          </td>
                                                        </tr>
                                                        </div> 
                                                        <tr>
                                                             <td width="100" class="field_title">
                                                                <asp:Label ID="Label47" Text="TCS Applicable Amount" runat="server"></asp:Label>
                                                            </td>
                                                            <td width="100" class="field_input">
                                                                <asp:TextBox runat="server" ID="txtTCSApplicable" CssClass="x-large" placeholder="TCS Applicable" Enabled="false" AutoPostBack="True" OnTextChanged="txtTCSApplicable_TextChanged"></asp:TextBox>
                                                            </td>
                                                            
                                                            <td width="98" class="field_title">
                                                                <asp:Label ID="Label46" Text="TCS(%)" runat="server"></asp:Label>
                                                            </td>
                                                            <td width="138" class="field_input">
                                                                <asp:TextBox runat="server" ID="txtTCSPercent" Text="00" CssClass="x_large" placeholder="TCS(%)" Enabled="false" ></asp:TextBox>
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
                                           </table>
                                             </fieldset>
                                                </td>
                                           </tr>
                                        <tr>
                                            <td style="padding: 8px 10px">
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td align="left" width="100" class="innerfield_title">
                                                            <asp:Label ID="lblRemarks" Text="Remarks" runat="server"></asp:Label>
                                                        </td>
                                                        <td width="325">
                                                            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Style="width: 300px;
                                                                height: 80px" MaxLength="50"> </asp:TextBox>
                                                        </td>
                                                        <td width="15">
                                                            &nbsp;
                                                        </td>
                                                        <td width="100" align="left" class="innerfield_title">
                                                            <asp:Label ID="lblCheckerNote" Text="Rejection Note" runat="server"></asp:Label>
                                                        </td>
                                                        <td width="310">
                                                            <asp:TextBox ID="txtCheckerNote" runat="server" TextMode="MultiLine" Enabled="false"
                                                                MaxLength="255" Style="width: 290px; height: 80px"> </asp:TextBox>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="8" colspan="6">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            &nbsp;
                                                        </td>
                                                        <td colspan="5">
                                                            <div class="btn_24_blue" id="btnsubmitdiv" runat="server">
                                                                <span class="icon disk_co"></span>
                                                                <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="btn_link" OnClick="btnSubmit_Click"
                                                                    ValidationGroup="Save" />
                                                            </div>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                            <div class="btn_24_blue" id="divbtnCancel" runat="server">
                                                                <span class="icon cross_octagon_co"></span>
                                                                <asp:Button ID="btnCancel" runat="server" Text="CLOSE" CssClass="btn_link" CausesValidation="false"
                                                                    OnClick="btnCancel_Click" />
                                                            </div>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                            <div class="btn_24_green" id="divbtnapprove" runat="server">
                                                                <span class="icon approve_co"></span>
                                                                <asp:LinkButton ID="btnApprove" runat="server" Text="Approve" CssClass="btn_link"
                                                                    CausesValidation="false" OnClick="btnApprove_Click" ValidationGroup="Approve" />
                                                            </div>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                            <div class="btn_24_red" id="divbtnreject" runat="server">
                                                                <span class="icon reject_co"></span>
                                                                <asp:LinkButton ID="btnReject" runat="server" Text="Reject" CssClass="btn_link" CausesValidation="false"
                                                                    OnClick="btnReject_Click" />
                                                            </div>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                            <div class="btn_24_blue" id="divDocuments" runat="server">
                                                                <span class="icon exclamation_co"></span>
                                                                <asp:Button ID="btnDocuments" runat="server" Text="Qc_Documents" CssClass="btn_link"
                                                                    CausesValidation="false" OnClick="btnDocuments_Click" />
                                                            </div>
                                                            <asp:HiddenField runat="server" ID="hdnReceivedID" />
                                                            <asp:HiddenField runat="server" ID="hdnDepotID" />
                                                            <asp:HiddenField runat="server" ID="hdnWaybillNo" />
                                                            <asp:HiddenField runat="server" ID="hdn_guid" />
                                                            <asp:HiddenField runat="server" ID="hdnpoid" />
                                                            <asp:HiddenField runat="server" ID="hdnproductid" />
                                                            <asp:HiddenField runat="server" ID="hdnSaleOrderID" />
                                                            <asp:HiddenField runat="server" ID="hdnSaleOrderNo" />
                                                            <asp:HiddenField runat="server" ID="hdnMFDATE" />
                                                            <asp:HiddenField runat="server" ID="hdnEXPRDATE" />
                                                            <asp:HiddenField runat="server" ID="Hdn_Print" />
                                                            <asp:HiddenField runat="server" ID="hdnMRP" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlDisplay" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_title">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="90">
                                                            <asp:Label ID="Label16" runat="server" Text="From Date"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="165">
                                                            <asp:TextBox ID="txtFromDate" runat="server" MaxLength="10" Width="120" Enabled="false"
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
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="165">
                                                            <asp:TextBox ID="txtToDate" runat="server" MaxLength="10" Width="69%" Enabled="false"
                                                                placeholder="dd/mm/yyyy" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="Search"
                                                                Font-Bold="true"></asp:TextBox>
                                                            <asp:ImageButton ID="imgPopuptodate" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="imgPopuptodate"
                                                                runat="server" TargetControlID="txtToDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                        <td>
                                                            <div class="btn_24_blue">
                                                                <span class="icon find_co"></span>
                                                                <asp:Button ID="btnSearchDespatch" runat="server" Text="Search" CssClass="btn_link"
                                                                    ValidationGroup="Search" OnClick="btnSearchDespatch_Click" />
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
                                    <div class="gridcontent">
                                        <cc1:Grid ID="grdReceivedHeader" runat="server" CallbackMode="false" Serialize="true"
                                            FolderStyle="../GridStyles/premiere_blue" AutoGenerateColumns="false" OnDeleteCommand="DeleteRecordReceived"
                                            AllowAddingRecords="false" AllowFiltering="true" AllowPageSizeSelection="true"
                                            PageSize="100" AllowPaging="true" EnableRecordHover="true" OnRowDataBound="grdReceivedHeader_RowDataBound">
                                            <ClientSideEvents OnBeforeClientDelete="OnBeforeDespatchDelete" OnClientDelete="OnDeleteReceivedDetails" />
                                            <FilteringSettings InitialState="Visible" />
                                            <Columns>
                                                <cc1:Column ID="Column12" DataField="Sl" HeaderText="SL" runat="server" Width="50">
                                                    <TemplateSettings TemplateId="slnoTemplateIN" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column1" DataField="STOCKRECEIVEDID" HeaderText="STOCKRECEIVEDID"
                                                    runat="server" Width="60" Visible="false" />
                                                <cc1:Column ID="Column19" DataField="STOCKDESPATCHID" HeaderText="STOCKDESPATCHID"
                                                    runat="server" Width="60" Visible="false" />
                                                <cc1:Column ID="Column91" DataField="STOCKRECEIVEDNO" HeaderText="RECEIVED NO." runat="server"
                                                    Width="110" Wrap="true">
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
                                                <cc1:Column ID="Column99" DataField="WAYBILLNO" HeaderText="WAYBILL NO" runat="server" Visible="false"
                                                    Width="95">
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
                                                <cc1:Column ID="Column15" DataField="VEHICHLENO" HeaderText="VEHICHLE NO" runat="server" 
                                                    Width="90" Visible="false" />
                                                <cc1:Column ID="Column16" DataField="LRGRNO" HeaderText="LR/GR NO" runat="server" Visible="false"
                                                    Width="90">
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
                                                <cc1:Column ID="Column31" DataField="TPUNAME" HeaderText="VENDOR" HeaderAlign="Center" runat="server"
                                                    Width="200" Wrap="true">
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
                                                <cc1:Column ID="Column36" DataField="TOTALAMOUNT" HeaderText="TOTALAMOUNT" HeaderAlign="Center" runat="server"
                                                    Width="200" Wrap="true">
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
                                                <cc1:Column ID="Column89" DataField="MOTHERDEPOTNAME" HeaderText="FACTORY" runat="server" Visible="false"
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
                                                <cc1:Column ID="Column17" DataField="TRANSPORTERNAME" HeaderText="TRANSPORTER" runat="server"
                                                    Width="160" Wrap="true">
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
                                                <cc1:Column ID="Column79" DataField="FINYEAR" HeaderText="FINYEAR" runat="server" Visible="false"
                                                    Width="100" />
                                                <cc1:Column ID="Column26" DataField="NEXTLEVELID" HeaderText="NEXTLEVELID" runat="server"
                                                    Visible="false" Width="100" />
                                                <cc1:Column ID="Column24" DataField="ISVERIFIED" HeaderText="ISVERIFIED" Visible="false"
                                                    runat="server" Width="100" />
                                                <cc1:Column ID="Column32" DataField="GRN" HeaderText="GRN" Visible="TRUE" runat="server"
                                                    Width="60" />

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
                                                
                                                <cc1:Column ID="Column27" DataField="MOTHERDEPOTID" HeaderText="MOTHERDEPOTID" runat="server"
                                                    Width="100" Visible="false" />
                                                <cc1:Column ID="Column33" DataField="TOTALCASE" HeaderText="TOTAL CASE" runat="server"
                                                    Width="100" Visible="false" />
                                                <cc1:Column ID="Column34" DataField="TOTALPCS" HeaderText="TOTAL PCS" runat="server"
                                                    Width="100" Visible="false" />

                                                <%-- <cc1:Column ID="ColUsername" DataField="USERNAME" HeaderText="ENTRY USER" runat="server"
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

                                                <cc1:Column ID="ColApproveby" DataField="APPROVEBY" HeaderText="APPROVE BY" runat="server"
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
                                                </cc1:Column>--%>

                                                <cc1:Column ID="Column35" AllowEdit="false" AllowDelete="false" HeaderText="PRINT"
                                                    runat="server" Width="60" Wrap="true">
                                                    <TemplateSettings TemplateId="PrintBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column92" AllowEdit="true" AllowDelete="true" HeaderText="EDIT" runat="server"
                                                    Width="60">
                                                    <TemplateSettings TemplateId="editBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column HeaderText="CANCEL" AllowEdit="false" AllowDelete="true" Width="65">
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
                                                <cc1:GridTemplate runat="server" ID="deleteBtnTemplateDespatch">
                                                    <Template>
                                                        <a href="javascript: //" class="action-icons c-delete" title="Delete" onclick="grdReceivedHeader.delete_record(this)">
                                                        </a>
                                                    </Template>
                                                </cc1:GridTemplate>
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
                                            <ScrollingSettings ScrollWidth="100%" ScrollHeight="290" NumberOfFixedColumns="5" />
                                        </cc1:Grid>
                                        <asp:Button ID="btngrdedit" runat="server" Text="grdedit" Style="display: none" OnClick="btngrdedit_Click"
                                            CausesValidation="false" />
                                        <asp:Button ID="btnPrint" runat="server" Text="Print" Style="display: none" OnClick="btnPrint_Click"
                                            CausesValidation="false" />
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlAddEdit" runat="server" CssClass="modalPopup" Style="display: none;
                                    border-radius: 16px;" Width="80%" Height="87%" BackColor="White" BorderColor="#257daf"
                                    BorderWidth="8px" BorderStyle="Solid">
                                    <div style="background-color: #257daf; border-style: solid; border-color: Yellow;
                                        background-position: center; background-repeat: no-repeat; background-size: cover;
                                        height: 6%" align="center">
                                        <asp:Image ID="Image2" runat="server" ImageAlign="Left" class="action-icons c-edit" />
                                        <asp:Label Font-Bold="True" ID="Label5" runat="server" Text="Purchase Stock Receipt Rejection Details"
                                            Font-Size="X-Large" ForeColor="Yellow"></asp:Label>
                                        <asp:ImageButton ID="imgrejectionbtn" ImageAlign="Right" ImageUrl="~/images/Cancel.png"
                                            Height="25px" Width="25px" runat="server" OnClick="imgrejectionbtn_click" />
                                        <hr />
                                    </div>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td width="13%" class="field_title">
                                                <asp:Label ID="Label6" Text="Product" runat="server"></asp:Label>
                                            </td>
                                            <td class="field_input">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="45%">
                                                            <asp:TextBox ID="txtproductnameonrejection" runat="server" CssClass="full" Enabled="false"
                                                                ForeColor="Black"></asp:TextBox>
                                                        </td>
                                                        <td width="15">
                                                            &nbsp;
                                                        </td>
                                                        <td width="9%" class="innerfield_title">
                                                            <asp:Label ID="Label7" Text="Batch No" runat="server"></asp:Label>
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
                                                <asp:Label ID="Label10" Text="Rejection Qty" runat="server"></asp:Label><span class="req">*</span>
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
                                                        <td width="15">
                                                            &nbsp;
                                                        </td>
                                                        <td width="9%" class="innerfield_title">
                                                            <asp:Label ID="Label11" Text="PackSize" runat="server"></asp:Label><span class="req">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlrejectionpacksize" runat="server" AppendDataBoundItems="true"
                                                                class="common-select" Width="200" ValidationGroup="check">
                                                                <asp:ListItem Text="-- SELECT PACKSIZE --" Value="0"></asp:ListItem>
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
                                                <asp:Label ID="Label12" Text="Reason" runat="server"></asp:Label><span class="req">*</span>
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
                                                        <td width="30">
                                                            &nbsp;
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
                                                    <cc1:Column ID="Column11" DataField="GUID" ReadOnly="true" HeaderText="GUID" runat="server"
                                                        Visible="false" Width="10" />
                                                    <cc1:Column ID="Column23" DataField="STOCKRECEIVEDID" ReadOnly="true" HeaderText="STOCKRECEIVEDID"
                                                        runat="server" Visible="false" Width="10" />
                                                    <cc1:Column ID="Column21" DataField="POID" ReadOnly="true" HeaderText="POID" runat="server"
                                                        Visible="false" Width="10" />
                                                    <cc1:Column ID="Column22" DataField="STOCKDESPATCHID" ReadOnly="true" HeaderText="STOCKDESPATCHID"
                                                        runat="server" Visible="false" Width="10" />
                                                    <cc1:Column ID="Column4" DataField="PRODUCTID" ReadOnly="true" HeaderText="ID" runat="server"
                                                        Visible="false" Width="10" />
                                                    <cc1:Column ID="Column6" DataField="Sl" HeaderText="SL" runat="server" Width="70"
                                                        Wrap="true">
                                                        <TemplateSettings TemplateId="slnoTemplate" />
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column7" DataField="PRODUCTNAME" HeaderText="PRODUCT NAME" runat="server"
                                                        Wrap="true" Width="380">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column9" DataField="BATCHNO" HeaderText="BATCH NO" runat="server"
                                                        Wrap="true" Width="120">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column51" DataField="REJECTIONQTY" HeaderText="QTY" runat="server"
                                                        Width="100" Wrap="true">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column13" DataField="PACKINGSIZENAME" HeaderText="PACK SIZE" runat="server"
                                                        Wrap="true" Width="200">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column18" DataField="REASONNAME" HeaderText="REJECTION REASON" runat="server"
                                                        Width="350" Wrap="true">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column20" DataField="REASONID" HeaderText="REASONID" runat="server"
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
                                                            <%--<a href="javascript: //" class="action-icons c-delete" title="Delete" onclick="gvPurchaseOrder.delete_record(this)"></a>--%>
                                                            <a href="javascript: //" class="action-icons c-delete" id="btnGridDelete_<%# Container.PageRecordIndex %>"
                                                                onclick="CallDeleteServerMethod(this)"></a>
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
                                            <asp:Button ID="btngrddelete" runat="server" Text="grddelete" Style="display: none"
                                                OnClick="btngrddelete_Click" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete this recordPRICE');" />
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
                                    .modalBackground
                                    {
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
                    <div id="lightRejectionNote" class="white_content" runat="server">
                        <table width="50%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                            <tr>
                                <td width="10%" class="field_title">
                                    <asp:Label ID="Label17" runat="server" Text="Note"></asp:Label>&nbsp;<span class="req">*t="Note"></asp:Label>&nbsp;<span
                                        class="req">*</span>
                                </td>
                                <td width="20%" class="field_input">
                                    <asp:TextBox ID="txtRejectionNote" runat="server" CssClass="x_large" TextMode="MultiLine"
                                        Style="height: 119px" MaxLength="255"> </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="CV_txtRejectionNote" runat="server" ControlToValidate="txtRejectionNote"
                                        ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="RejectionNote"
                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="field_title">
                                    &nbsp;
                                </td>
                                <td class="field_input">
                                    <asp:Button ID="btnRejectionNoteSubmit" runat="server" Text="Submit" CssClass="btn_small btn_blue"
                                        ValidationGroup="RejectionNote" OnClick="btnRejectionNoteSubmit_Click" />&nbsp;&nbsp;<asp:Button
                                            ID="btnRejectionCloseLightbox" runat="server" Text="Close" CssClass="btn_small btn_blue"
                                            OnClick="btnRejectionCloseLightbox_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="fadeRejectionNote" class="black_overlay" runat="server">
                    </div>
                    <cc1:MessageBox ID="MessageBox1" runat="server" />
                </div>
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

                function MakeStaticHeaderScheme(gridId, height, width, headerHeight, isFooter) {
                    var tbl = document.getElementById(gridId);
                    if (tbl) {
                        var DivHR = document.getElementById('DivHeaderRowScheme');
                        var DivMC = document.getElementById('DivMainContentScheme');
                        var DivFR = document.getElementById('DivFooterRowScheme');

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

                function OnScrollDivScheme(Scrollablediv) {
                    document.getElementById('DivHeaderRowScheme').scrollLeft = Scrollablediv.scrollLeft;
                    document.getElementById('DivFooterRowScheme').scrollLeft = Scrollablediv.scrollLeft;
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>