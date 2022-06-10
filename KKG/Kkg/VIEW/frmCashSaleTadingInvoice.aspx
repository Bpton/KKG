<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmCashSaleTadingInvoice.aspx.cs" Inherits="VIEW_frmCashSaleTadingInvoice" %>


<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"
    TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                                <h6 runat="server" id="hdInvoice"></h6>
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
                                                    <legend runat="server" id="lgndInvoice"></legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                        <tr runat="server" id="trAutoInvoiceNo">
                                                            <td class="field_title">
                                                                <asp:Label ID="lblStockRcvdNo" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input" colspan="8">
                                                                <asp:TextBox ID="txtSaleInvoiceNo" runat="server" Width="200" placeholder="Auto Generate No."
                                                                    Enabled="false" Font-Bold="true"> </asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        
                                        </tr>
                                        <tr>
                                            <td width="80" class="field_title">
                                                <asp:Label ID="Label2" runat="server"></asp:Label>
                                            </td>
                                            <td width="110" class="field_input">
                                                <asp:TextBox ID="txtInvoiceDate" runat="server" Enabled="false" Width="70" placeholder="dd/MM/yyyy"
                                                    MaxLength="10" ValidationGroup="Save"></asp:TextBox>
                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/calendar.png"
                                                    Width="24px" Height="24px" ImageAlign="AbsMiddle" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtenderInvoiceDate" TargetControlID="txtInvoiceDate"
                                                    PopupButtonID="ImageButton1" runat="server" Format="dd/MM/yyyy" Enabled="true"
                                                    BehaviorID="CalendarExtenderDespatchDate" CssClass="cal_Theme1" />
                                            </td>
                                            <td width="70" class="field_title">
                                                <asp:Label ID="Label14" Text="Customer" runat="server"></asp:Label>&nbsp;<span class="req">*</span>
                                            </td>
                                            <td class="field_input" width="210">
                                                <asp:DropDownList ID="ddlDistributor" runat="server" class="chosen-select" data-placeholder="Select Customer"
                                                    AppendDataBoundItems="True" Width="200px" ValidationGroup="Save" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlDistributor_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="CV_ddlDistributor" runat="server" ControlToValidate="ddlDistributor"
                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="*" ValidationGroup="Save"
                                                    ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                            </td>
                                            <td width="100" class="field_title">
                                                <asp:Label ID="Label13" Text="Payment Mode" runat="server"></asp:Label>&nbsp;<span
                                                    class="req">*</span>
                                            </td>
                                            <td class="field_input" width="100">
                                                <asp:DropDownList ID="ddlPaymentMode" Width="85" runat="server" class="chosen-select"
                                                    data-placeholder="Choose Payment Mode" ValidationGroup="Save">
                                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Advance" Value="Cash"></asp:ListItem>
                                                    <asp:ListItem Text="Credit" Value="Credit"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorPaymentMode" runat="server"
                                                    ControlToValidate="ddlPaymentMode" ValidateEmptyText="false" SetFocusOnError="true"
                                                    ErrorMessage="*" ValidationGroup="Save" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title">
                                                <asp:Label ID="lblTransportMode" Text="Transport" runat="server"></asp:Label>
                                            </td>
                                            <td class="field_input">
                                                <asp:DropDownList ID="ddlTransportMode" Width="100" runat="server" class="chosen-select"
                                                    data-placeholder="Choose Transport Mode">
                                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="By Road" Value="By Road"></asp:ListItem>
                                                    <asp:ListItem Text="By Rail" Value="By Rail"></asp:ListItem>
                                                    <asp:ListItem Text="By Air" Value="By Air"></asp:ListItem>
                                                    <asp:ListItem Text="By Ship" Value="By Ship"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td width="92" class="field_title">
                                                <asp:Label ID="Label38" Text="Transporter" runat="server"></asp:Label>&nbsp;<span
                                                    class="req">*</span>
                                            </td>
                                            <td class="field_input">
                                                <asp:DropDownList ID="ddlTransporter" Width="200" runat="server" class="chosen-select"
                                                    data-placeholder="Select Transporter" AppendDataBoundItems="True" ValidationGroup="Save">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorTransporter" runat="server"
                                                    ControlToValidate="ddlTransporter" ValidateEmptyText="false" SetFocusOnError="true"
                                                    ErrorMessage="*" ValidationGroup="Save" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                                &nbsp;<td id="td2" runat="server">
                                                    <asp:Button ID="btnTransporter" runat="server" CausesValidation="false" Enabled="true"
                                                        CssClass="h_icon add_co" ToolTip="Add Sub Item Type" OnClick="btnTransporter_Click" />
                                                    <asp:Button ID="btnRefreshCategory" runat="server" CssClass="h_icon arrow_refresh_co" ToolTip="Refresh" OnClick="btnRefreshCategory_Click" CausesValidation="false" />
                                                </td>
                                            </td>
                                            <td class="field_title" style="width: 10px">
                                                <asp:Label ID="lblWayBill" Text="E-WayBill No." runat="server"></asp:Label>
                                            </td>
                                            <td class="field_input">
                                                <asp:TextBox ID="ddlWaybill" Width="100px" runat="server"
                                                    placeholder="WayBill No">
                                                </asp:TextBox>
                                            </td>
                                            <td class="field_title">
                                                <asp:Label ID="lblVehicle" Text="Vehicle No." runat="server"></asp:Label>
                                            </td>
                                            <td class="field_input">
                                                <asp:TextBox ID="txtVehicle" runat="server" Width="80" placeholder="Vehicle No"> </asp:TextBox>
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title">
                                                <asp:Label ID="lblLRGRNo" Text="LR/GR No" runat="server"></asp:Label>

                                            </td>
                                            <td class="field_input">
                                                <asp:TextBox ID="txtLRGRNo" runat="server" Width="95" MaxLength="20" placeholder="LR/GR No"> </asp:TextBox>
                                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                                                    ControlToValidate="txtLRGRNo" ValidateEmptyText="false" SetFocusOnError="true"
                                                                    ErrorMessage="*" ValidationGroup="Save" ForeColor="Red" InitialValue=""></asp:RequiredFieldValidator>--%>
                                            </td>
                                            <td class="field_title">
                                                <asp:Label ID="lblLRGRDate" Text="LR/GR Date" runat="server"></asp:Label>
                                            </td>
                                            <td class="field_input">
                                                <asp:TextBox ID="txtLRGRDate" runat="server" Enabled="false" Width="70" placeholder="dd/MM/yyyy"
                                                    MaxLength="10"></asp:TextBox>
                                                <asp:ImageButton ID="imgbtnLRGRCalendar" runat="server" ImageUrl="~/images/calendar.png"
                                                    Width="24px" Height="24px" ImageAlign="AbsMiddle" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtenderLRGRDate" TargetControlID="txtLRGRDate"
                                                    PopupButtonID="imgbtnLRGRCalendar" runat="server" Format="dd/MM/yyyy" Enabled="true"
                                                    BehaviorID="CalendarExtenderLRGRDate" CssClass="cal_Theme1" />
                                            </td>
                                            <td class="field_title" style="display: none">
                                                <asp:Label ID="Label11" Text="Gate Pass Date" runat="server"></asp:Label>
                                            </td>
                                            <td class="field_input" style="display: none">
                                                <asp:TextBox ID="txtGetPassDate" runat="server" Enabled="false" Width="70" placeholder="dd/MM/yyyy"
                                                    MaxLength="10"></asp:TextBox>
                                                <asp:ImageButton ID="ImageButtonGetPass" runat="server" ImageUrl="~/images/calendar.png"
                                                    Width="24px" Height="24px" ImageAlign="AbsMiddle" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtenderGetPassDate" TargetControlID="txtGetPassDate"
                                                    PopupButtonID="ImageButtonGetPass" runat="server" Format="dd/MM/yyyy" Enabled="true"
                                                    BehaviorID="CalendarExtenderGetPassDate" CssClass="cal_Theme1" />
                                            </td>
                                            <td class="field_title" style="display: none">
                                                <asp:Label ID="Label10" runat="server"></asp:Label>&nbsp;<span class="req">*</span>
                                            </td>
                                            <td class="field_input" style="display: none">
                                                <asp:TextBox ID="txtGetpass" runat="server" Width="80" MaxLength="20"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_titleTr">
                                                <asp:Label ID="lblinsurancecompname" Text="INSURANCE CO." runat="server"></asp:Label>
                                            </td>
                                            <td class="field_inputTr">
                                                <asp:DropDownList ID="ddlinsurancecompname" Width="195px" runat="server" class="chosen-select"
                                                    data-placeholder="" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlinsurancecompname_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="field_titleTr">
                                                <asp:Label ID="Label5" Text="Policy No" runat="server"></asp:Label>
                                            </td>
                                            <td class="field_inputTr">
                                                <asp:DropDownList ID="ddlinsuranceno" runat="server" AppendDataBoundItems="true"
                                                    Style="width: 200px; background-color: Black" class="chosen-select" data-placeholder="-- Select Insurance No --">
                                                    <asp:ListItem Text="SELECT POLICY NO" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td width="70" class="field_title">
                                                <asp:Label ID="Label3" Text="Billing Customer" runat="server"></asp:Label>&nbsp;<span class="req">*</span>
                                            </td>
                                            <td class="field_input" width="210">
                                                <asp:DropDownList ID="ddlShippingAdress" runat="server" class="chosen-select" data-placeholder="Select Shipping Customer"
                                                    AppendDataBoundItems="True" Width="200px" ValidationGroup="Save" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlShippingAdress_SelectedIndexChanged">
                                                </asp:DropDownList>

                                            </td>

                                            <td class="field_input" width="110">
                                                <asp:TextBox ID="lblShippingAdress" runat="server" Width="110" MaxLength="20" Enabled="false"></asp:TextBox>
                                                <span class="label-info"></td>

                                            <td class="field_input" valign="top">
                                                <asp:TextBox runat="server" ID="txtBillingAdress" Width="200" MaxLength="50"
                                                    placeholder="Adress" TextMode="MultiLine" ValidationGroup="A"></asp:TextBox>&nbsp;
                                                                   <span class="label-info">
                                                                       <label>
                                                                           Billing Adress</label>
                                            </td>
                                            <td class="field_titleTr" width="110">
                                                <asp:Label ID="lblwaybillno" Text="Way Bill No" runat="server"></asp:Label>
                                            </td>
                                            <td class="field_inputTr">
                                                <asp:TextBox ID="txtwaybilno" runat="server" placeholder="Enter WayBill No" Width="195px"
                                                    AutoCompleteType="Disabled" onfocus="disableautocompletion(this.id);"></asp:TextBox>
                                            </td>
                                            <td class="field_titleTr">
                                                <asp:Label ID="lblwaybildate" runat="server" Text="Waybill&nbsp;Date"></asp:Label>
                                            </td>

                                            <td class="field_inputTr">
                                                <asp:TextBox ID="txtwaybilldate" runat="server" Width="70" Enabled="false" placeholder="dd/mm/yyyy"
                                                    MaxLength="10" ValidationGroup="checksave" onkeypress="return isNumberKeyWithslash(event);">
                                                </asp:TextBox>
                                                <asp:ImageButton ID="imgPopupwaybilldate" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                    runat="server" Height="24" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender7" PopupButtonID="imgPopupwaybilldate"
                                                    runat="server" TargetControlID="txtwaybilldate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>
                                                <%--<asp:RequiredFieldValidator ID="CV_WayBillDate" runat="server" ControlToValidate="txtwaybilldate"
                                                                                ValidationGroup="checksave" SetFocusOnError="true" ErrorMessage="* Required!"
                                                                                Font-Bold="true" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                            </td>
                                        </tr>
                                        <tr style="display: none">
                                            <td class="field_title">
                                                <asp:Label ID="lblTPU" Text="Despatch" runat="server"></asp:Label>&nbsp;<span class="req">*</span>
                                            </td>
                                            <td class="field_input" colspan="8">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td>
                                                            <asp:DropDownList ID="ddlDepot" Width="220" runat="server" AutoPostBack="true" class="chosen-select"
                                                                data-placeholder="Choose Depot" AppendDataBoundItems="True" ValidationGroup="Save"
                                                                OnSelectedIndexChanged="ddlDepot_SelectedIndexChanged">
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
                                                        <td width="15">&nbsp;
                                                        </td>
                                                        <td width="50" class="innerfield_title" runat="server" id="tdFormLabel" style="display: none;">
                                                            <asp:Label ID="Label39" Text="C.Form" runat="server"></asp:Label>
                                                        </td>
                                                        <td width="90" runat="server" id="tdFormControl" style="display: none;">
                                                            <asp:DropDownList ID="ddlForm" runat="server" Width="90px" ValidationGroup="Save"
                                                                AutoPostBack="true" class="chosen-select" OnSelectedIndexChanged="ddlForm_SelectedIndexChanged">
                                                                <asp:ListItem Text="Y" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="N" Value="1"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="15" class="innerfield_title">&nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr style="display: none">
                                            <td class="field_title">
                                                <asp:Label ID="Label35" Text="Delivery Point" runat="server"></asp:Label>
                                            </td>
                                            <td class="field_input" colspan="8">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="220">
                                                            <asp:DropDownList ID="ddlDeliveryAddress" runat="server" class="chosen-select" data-placeholder="Select Address"
                                                                AppendDataBoundItems="True" Width="200px">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="115" class="innerfield_title">
                                                            <asp:Label ID="LabelICDS" Text="ICDS / Phase II No." runat="server"></asp:Label>&nbsp;<span
                                                                class="req">*</span>
                                                        </td>
                                                        <td width="205">
                                                            <asp:TextBox runat="server" ID="txtICDS" placeholder="ICDS No" Width="195"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorICDSNo" runat="server" ControlToValidate="txtICDS"
                                                                ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="Save"
                                                                ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td width="90" class="innerfield_title">
                                                            <asp:Label ID="LabelICDSDate" Text="&nbsp;&nbsp;ICDS Date" runat="server"></asp:Label>&nbsp;<span
                                                                class="req">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtICDSDate" runat="server" Enabled="false" Width="110" placeholder="dd/MM/yyyy"
                                                                MaxLength="10"></asp:TextBox>
                                                            <asp:ImageButton ID="ImageButtonICDS" runat="server" ImageUrl="~/images/calendar.png"
                                                                Width="24px" Height="24px" ImageAlign="AbsMiddle" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorICDSDate" runat="server" ControlToValidate="txtICDSDate"
                                                                ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="Save"
                                                                ForeColor="Red"></asp:RequiredFieldValidator>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtenderICDS" TargetControlID="txtICDSDate"
                                                                PopupButtonID="ImageButtonICDS" runat="server" Format="dd/MM/yyyy" Enabled="true"
                                                                BehaviorID="CalendarExtenderICDSDate" CssClass="cal_Theme1" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    </fieldset>
                                            </td>
                                        </tr>
                                        <tr id="trproductadd" runat="server">
                                            <td class="field_input" style="padding-left: 10px">
                                                <fieldset>
                                                    <legend id="LgndPoDetails" runat="server">Sale Order Details</legend>
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td width="310" class="field_input">
                                                                <asp:DropDownList ID="ddlProduct" runat="server" class="chosen-select" data-placeholder="Select Item"
                                                                    AppendDataBoundItems="True" Width="300" ValidationGroup="A" AutoPostBack="true"
                                                                    OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <span class="label_intro">
                                                                    <label>
                                                                        PRODUCT</label>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlProduct"
                                                                        ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="*" ValidationGroup="A"
                                                                        ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator></span>
                                                            </td>
                                                            <td class="field_input" width="70">
                                                                <asp:DropDownList ID="ddlPacksize" runat="server" class="chosen-select" data-placeholder="UOM"
                                                                    AppendDataBoundItems="True" Width="70" ValidationGroup="A">
                                                                </asp:DropDownList>
                                                                <span class="label_intro">
                                                                    <label>
                                                                        UOM</label>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlPacksize"
                                                                        ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="*" ValidationGroup="A"
                                                                        ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator></span>
                                                            </td>
                                                            <td class="field_input" width="50">
                                                                <asp:TextBox runat="server" ID="txtMRP" Width="50" placeholder="MRP" Enabled="false"></asp:TextBox>
                                                                <asp:HiddenField runat="server" ID="hdn_PackSizeQC" />
                                                                <asp:HiddenField runat="server" ID="hdnbilltype" />
                                                                <asp:TextBox runat="server" ID="txtOrderDate" CssClass="full" placeholder="Order Date"
                                                                    Visible="false" Enabled="false"></asp:TextBox>
                                                                <span class="label_intro">
                                                                    <label>
                                                                        MRP</label></span>
                                                            </td>
                                                            <td class="field_input" width="50">
                                                                <asp:TextBox runat="server" ID="txtBaseCost" Width="50" placeholder="RATE"></asp:TextBox>
                                                                <span class="label_intro">
                                                                    <label>
                                                                        RATE</label></span>
                                                            </td>

                                                            <td class="field_input" width="50">
                                                                <asp:TextBox runat="server" ID="txtRateDiscAmnt" Width="50" placeholder="PERCENTAGE" onkeypress="return isNumberKeyWithDot(event);"></asp:TextBox>
                                                                <span class="label_intro">
                                                                    <label>
                                                                        DISCOUNT</label></span>
                                                            </td>

                                                            <td class="field_input" width="60">
                                                                <asp:TextBox runat="server" ID="txtAssementPercentage" CssClass="full" placeholder="Assesment(%)"
                                                                    Enabled="false" Visible="false"></asp:TextBox>
                                                            </td>
                                                           
                                                            <td class="field_input" width="90">
                                                                <asp:TextBox runat="server" ID="txtRemainingQty" Width="70" placeholder="Remaining Qty"
                                                                    Enabled="false"></asp:TextBox>
                                                                <span class="label_intro">
                                                                    <label>
                                                                        REMAINING QTY</label></span>
                                                            </td>
                                                            <td class="field_input" width="80">
                                                                <asp:TextBox runat="server" ID="txtStockQty" Width="70" placeholder="Stock Qty" Enabled="false"></asp:TextBox>
                                                                <span class="label_intro">
                                                                    <label>
                                                                        STOCK QTY</label></span>
                                                            </td>
                                                            <td class="field_input" width="90">
                                                                <asp:TextBox runat="server" ID="txtDeliveredQtyPCS" Width="65" Text="0.00" onkeypress="return isNumberKeyWithDot(event);" 
                                                                    OnTextChanged="txtDeliveredQtyPCS_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                <asp:TextBox runat="server" ID="txtDeliveredQty" Width="50" placeholder="CASE" ValidationGroup="A"
                                                                    Text="0.00" onkeypress="return isNumberKey(event);" Visible="false"></asp:TextBox>&nbsp;
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorDeliveredQty" runat="server"
                                                                    ControlToValidate="txtDeliveredQty" ValidateEmptyText="false" SetFocusOnError="true"
                                                                    ErrorMessage="*" ValidationGroup="A" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                <span class="label_intro">
                                                                    <label>
                                                                        INVOICE QTY</label>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorDeliveredQtyPCS" runat="server"
                                                                        ControlToValidate="txtDeliveredQtyPCS" ValidateEmptyText="false" SetFocusOnError="true"
                                                                        ErrorMessage="Req" ValidationGroup="A" ForeColor="Red"></asp:RequiredFieldValidator></span>
                                                            </td>
                                                            <td class="field_input" width="90">
                                                                <asp:TextBox runat="server" ID="txtCboxQty" Width="50" placeholder="C BOX QTY" ValidationGroup="A"
                                                                    Text="0.00" onkeypress="return isNumberKeyWithDot(event);"></asp:TextBox>&nbsp;
                                                                   <span class="label_intro">
                                                                       <label>
                                                                           C BOX QTY</label>
                                                            </td>


                                            </td>
                                        </tr>
                                    <tr>
                                        <td class="field_input" valign="top">
                                            <asp:CheckBox ID="chkFree" runat="server" Text="&nbsp;&nbsp;Free Issue" TabIndex="1"
                                                Visible="false" />
                                            <div class="btn_24_blue">
                                                <span class="icon add_co"></span>
                                                <asp:Button ID="btnADDGrid" runat="server" Text="Add" CssClass="btn_link" OnClick="btnADDGrid_Click"
                                                    ValidationGroup="A" />
                                            </div>
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                    </tr>







                                    <tr style="display: none">
                                        <td class="field_title">
                                            <asp:Label ID="Label4" Text="Batch" runat="server"></asp:Label>&nbsp;<span class="req">*</span>
                                        </td>
                                        <td class="field_input" colspan="5">
                                            <asp:DropDownList ID="ddlBatch" runat="server" Width="700px" CssClass="common-select"
                                                Height="24px" AutoPostBack="true" AppendDataBoundItems="True" Style="font-family: 'Courier New', Courier, monospace"
                                                OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged" ValidationGroup="A">
                                                <asp:ListItem Text="Select Batch" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlBatch" ValidateEmptyText="false" 
                                                            SetFocusOnError="true" ErrorMessage="Required!"  ValidationGroup="A" ForeColor="Red" InitialValue="0" ></asp:RequiredFieldValidator>--%>
                                        </td>
                                        <td class="field_input">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="7">
                                            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                <tr>
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
                                            <legend runat="server" id="lgndProductDetails"></legend>
                                            <div style="overflow: hidden; width: 100%;" id="DivHeaderRow">
                                            </div>
                                            <div style="overflow: scroll; margin-bottom: 8px; margin-right: 6px;" onscroll="OnScrollDiv(this)"
                                                id="DivMainContent">
                                                <asp:GridView ID="grdAddDespatch" runat="server" Width="100%" ShowFooter="true" AutoGenerateColumns="true"
                                                    EmptyDataText="No Records Available" CssClass="reportgrid">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="DELETE" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <nav style="height: 0px; z-index: 2; padding-left: 50px; position: relative; text-align: center;"><span class="badge green"><%# Container.DataItemIndex + 1 %></span></nav>
                                                                <%-- <asp:ImageButton ID="btn_TempDelete" runat="server" CssClass="action-icons c-delete"
                                                                    OnClientClick="return confirm('Are you sure you want to Delete?')" ToolTip="Delete"
                                                                    OnClick="btn_TempDelete_Click" />--%>

                                                                <asp:Button ID="btn_TempDelete" runat="server" class="action-icons c-delete"
                                                                    OnClientClick="return confirm('Are you sure you want to Delete?')" ToolTip="Delete"
                                                                    OnClick="btn_TempDelete_Click" />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <RowStyle HorizontalAlign="Right"></RowStyle>
                                                </asp:GridView>
                                                <asp:HiddenField ID="hdndtDespatchDelete" runat="server" />
                                                <asp:HiddenField ID="hdndtPOIDDelete" runat="server" />
                                                <asp:HiddenField ID="hdndtPRODUCTIDDelete" runat="server" />
                                                <asp:HiddenField ID="hdndtBATCHDelete" runat="server" />
                                                <asp:HiddenField ID="hdnGroupID" runat="server" />
                                                <asp:HiddenField ID="hdnProductType" runat="server" />
                                            </div>
                                            <div id="DivFooterRow" style="overflow: hidden">
                                            </div>
                                            <div style="overflow: hidden; width: 100%; display: none;" id="DivHeaderRowScheme">
                                            </div>
                                            <div style="overflow: scroll; margin-bottom: 8px; margin-right: 6px; display: none;"
                                                onscroll="OnScrollDivScheme(this)" id="DivMainContentScheme">
                                                <asp:GridView ID="grdQtySchemeDetails" runat="server" Width="100%" ShowFooter="true"
                                                    AutoGenerateColumns="true" EmptyDataText="No Records Available" CssClass="zebra">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="DELETE" ItemStyle-Width="20" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <nav style="height: 0px; z-index: 2; padding-left: 50px; position: relative; text-align: center;"><span class="badge green"><%# Container.DataItemIndex + 1 %></span></nav>
                                                                <asp:ImageButton ID="btn_TempDelete" runat="server" CssClass="action-icons c-delete"
                                                                    OnClientClick="return confirm('Are you sure you want to Delete?')" ToolTip="Delete"
                                                                    OnClick="btn_TempDeleteFree_Click" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:HiddenField runat="server" ID="hdn_PackSizeSaleOrder" />
                                                <asp:HiddenField runat="server" ID="hdn_Remaining" />
                                                <asp:HiddenField runat="server" ID="hdn_CurrentQty" />
                                            </div>
                                            <div id="DivFooterRowScheme" style="overflow: hidden; display: none;">
                                            </div>
                                            <div style="overflow: hidden; width: 100%; display: none;" id="Div1">
                                            </div>
                                            <div style="overflow: scroll; margin-bottom: 8px; margin-right: 6px; display: none;"
                                                id="Div2">
                                                <asp:GridView ID="grdGST" runat="server" Width="100%" ShowFooter="true" AutoGenerateColumns="true"
                                                    EmptyDataText="No Records Available" CssClass="zebra">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SL No." ItemStyle-Width="20" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div id="Div3" style="overflow: hidden; display: none;">
                                            </div>
                                        </fieldset>
                                    </div>

                                    <table width="30%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr align="right">
                                            <td class="field_input" width="150">
                                                <asp:Label ID="Label1" Text="Freight Amnt" runat="server" Font-Bold="true"></asp:Label>
                                            </td>

                                            <td class="field_input" width="150">
                                                <asp:TextBox runat="server" ID="txtUserInputFreight" AutoPostBack="true" Width="50" placeholder="Freight Amnt" ValidationGroup="A"
                                                    Text="0.00" onkeypress="return isNumberKey(event);" OnTextChanged="txtUserInputFreight_TextChanged"></asp:TextBox>&nbsp;
                                                                  
                                                                    
                                            </td>
                                            <td class="field_input" width="100">
                                                <asp:Label ID="lbltcs" Text="TCS" runat="server" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td class="field_input" width="150">
                                                <asp:RadioButtonList ID="rdbtcs" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                                                    OnSelectedIndexChanged="rdbtcs_SelectedIndexChanged">
                                                    <asp:ListItem Text="Yes" Value="Y" />
                                                    <asp:ListItem Text="No" Value="N" Selected="True" />
                                                </asp:RadioButtonList>
                                            </td>

                                        </tr>
                                    </table>

                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" valign="top" style="padding-left: 10px; width: 50%;">
                                                <fieldset style="display: none;">
                                                    <legend>Gross Tax Details</legend>
                                                    <div class="gridcontent-inner">
                                                        <cc1:Grid ID="grdTax" runat="server" CallbackMode="true" Serialize="true" AutoGenerateColumns="false"
                                                            FolderStyle="../GridStyles/premiere_blue" AllowAddingRecords="false" AllowFiltering="true"
                                                            AllowSorting="false" AllowPageSizeSelection="false" PageSize="200" AllowPaging="false">
                                                            <FilteringSettings InitialState="Visible" />
                                                            <Columns>
                                                                <cc1:Column ID="Column2" DataField="SLNO" HeaderText="Sl No" runat="server" Width="50"
                                                                    Visible="false">
                                                                    <TemplateSettings TemplateId="tplTaxNumbering" />
                                                                </cc1:Column>
                                                                <cc1:Column ID="Column53" DataField="NAME" ReadOnly="true" HeaderText="TAX NAME"
                                                                    runat="server" Width="170" Wrap="true" />
                                                                <cc1:Column ID="Column54" DataField="PERCENTAGE" ReadOnly="true" HeaderText="TAX(%)"
                                                                    runat="server" Wrap="true" Width="90" />
                                                                <cc1:Column ID="Column111" DataField="TAXVALUE" ReadOnly="true" HeaderText="TAX VALUE"
                                                                    runat="server" Wrap="true" Width="90" />
                                                            </Columns>
                                                            <FilteringSettings MatchingType="AnyFilter" />
                                                            <Templates>
                                                                <cc1:GridTemplate runat="server" ID="tplTaxNumbering">
                                                                    <Template>
                                                                        <asp:Label ID="lblslnoTax" runat="server" Text='<%#Container.PageRecordIndex+1 %>' />
                                                                    </Template>
                                                                </cc1:GridTemplate>
                                                            </Templates>
                                                            <ScrollingSettings ScrollWidth="100%" ScrollHeight="180" />
                                                        </cc1:Grid>
                                                    </div>
                                                </fieldset>
                                                <fieldset style="display: none;">
                                                    <legend>Terms & Conditions</legend>
                                                    <div class="gridcontent-inner">
                                                        <cc1:Grid ID="grdTerms" runat="server" CallbackMode="true" Serialize="true" AutoGenerateColumns="false"
                                                            FolderStyle="../GridStyles/premiere_blue" AllowAddingRecords="false" AllowFiltering="true"
                                                            ShowFooter="false" AllowSorting="false" OnRowDataBound="grdTerms_RowDataBound"
                                                            AllowPageSizeSelection="false" PageSize="200" AllowPaging="false">
                                                            <FilteringSettings InitialState="Visible" />
                                                            <Columns>
                                                                <cc1:Column ID="Column3" DataField="SLNO" HeaderText="Sl No" runat="server" Width="60">
                                                                    <TemplateSettings TemplateId="tplTermsNumbering" />
                                                                </cc1:Column>
                                                                <cc1:Column ID="Column10" DataField="DESCRIPTION" ReadOnly="true" HeaderText="TERMS & CONDITIONS"
                                                                    runat="server" Width="300" Wrap="true" />
                                                                <cc1:Column ID="Column5" DataField="ID" ReadOnly="true" HeaderText="TERMSID" runat="server"
                                                                    Width="60">
                                                                    <TemplateSettings TemplateId="CheckTemplateTERMS" HeaderTemplateId="HeaderTemplateTERMS" />
                                                                </cc1:Column>
                                                            </Columns>
                                                            <FilteringSettings MatchingType="AnyFilter" />
                                                            <Templates>
                                                                <cc1:GridTemplate runat="server" ID="tplTermsNumbering">
                                                                    <Template>
                                                                        <asp:Label ID="lblslnoTax" runat="server" Text='<%#Container.PageRecordIndex+1 %>' />
                                                                    </Template>
                                                                </cc1:GridTemplate>
                                                                <cc1:GridTemplate runat="server" ID="CheckTemplateTERMS">
                                                                    <Template>
                                                                        <asp:HiddenField runat="server" ID="hdnTERMSName" Value='<%# Container.DataItem["ID"] %>' />
                                                                        <asp:CheckBox ID="ChkIDTERMS" runat="server" Text=" " ToolTip="<%# Container.Value %>" />
                                                                    </Template>
                                                                </cc1:GridTemplate>
                                                            </Templates>
                                                            <ScrollingSettings ScrollWidth="100%" ScrollHeight="180" />
                                                        </cc1:Grid>
                                                    </div>
                                                </fieldset>
                                                <fieldset>
                                                    <legend>Remarks & Save</legend>
                                                    <table border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td width="80" class="innerfield_title">
                                                                <label>
                                                                    Remarks</label>
                                                            </td>
                                                            <td width="320">
                                                                <asp:TextBox ID="txtRemarks" runat="server" placeholder="Remarks if any...." CssClass="mid"
                                                                    TextMode="MultiLine" MaxLength="50" Style="width: 300px; height: 70px;"> </asp:TextBox>
                                                            </td>
                                                            <td width="100" align="left" class="innerfield_title">
                                                                <asp:Label ID="lblCheckerNote" Text="Rejection Note" runat="server" />
                                                            </td>
                                                            <td width="310">
                                                                <asp:TextBox ID="txtCheckerNote" runat="server" TextMode="MultiLine" Enabled="false"
                                                                    Style="width: 300px; height: 70px" MaxLength="255"> </asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;
                                                            </td>
                                                            <td colspan="3" style="padding-top: 10px;">
                                                                <div class="btn_24_blue" id="btnsubmitdiv" runat="server">
                                                                    <span class="icon disk_co"></span>
                                                                    <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="btn_link" OnClick="btnSubmit_Click"
                                                                        AccessKey="S" ValidationGroup="Save" />
                                                                </div>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                                <div class="btn_24_blue" id="btnCanceldiv" runat="server">
                                                                    <span class="icon cross_octagon_co"></span>
                                                                    <asp:Button ID="btnCancel" runat="server" Text="CLOSE" CssClass="btn_link" CausesValidation="false"
                                                                        OnClick="btnCancel_Click" />
                                                                </div>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                                <div class="btn_24_green" id="divbtnapprove" runat="server">
                                                                    <span class="icon approve_co"></span>
                                                                    <asp:LinkButton ID="btnApprove" runat="server" Text="Approve" CssClass="btn_link"
                                                                        CausesValidation="false" OnClick="btnApprove_Click" OnClientClick="return confirm('Are you sure you want to approved this invoice?')" />
                                                                </div>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                                <div class="btn_24_red" id="divbtnreject" runat="server">
                                                                    <span class="icon reject_co"></span>
                                                                    <asp:LinkButton ID="btnReject" runat="server" Text="Reject" CssClass="btn_link" CausesValidation="false"
                                                                        OnClick="btnReject_Click" OnClientClick="return confirm('Are you sure you want to reject this invoice?')" />
                                                                </div>
                                                                <asp:HiddenField runat="server" ID="hdnDespatchID" />
                                                                <asp:HiddenField runat="server" ID="hdnWaybillNo" />
                                                                <asp:HiddenField runat="server" ID="hdnCustomerID" />
                                                                <asp:HiddenField runat="server" ID="hdnCFormNo" />
                                                                <asp:HiddenField runat="server" ID="hdnCFormDate" />
                                                                <asp:HiddenField runat="server" ID="Hdn_Print" />
                                                                <asp:HiddenField runat="server" ID="hdnGrossSchemeID" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                            <td class="field_input" valign="top" width="80%" style="padding-left: 10px">
                                                <fieldset>
                                                    <legend>Amount&nbsp;Details</legend>
                                                    <table border="2" cellspacing="0" cellpadding="0" width="100%">
                                                        <tr>
                                                            <td valign="top" width="50%">
                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                    <tr>
                                                                        <td class="innerfield_title" style="color: #1e69de;">
                                                                            <label>
                                                                                Total Pcs</label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox runat="server" ID="txtTotPCS" BackColor="Transparent" Font-Bold="true"
                                                                                ReadOnly="true" CssClass="x_large" placeholder="Total Pcs" Style="text-align: right;"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="innerfield_title" style="color: #1e69de;">
                                                                            <label>
                                                                                Total Case</label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox runat="server" ID="txtTotCase" BackColor="Transparent" Font-Bold="true"
                                                                                onkeypress="return isNumberKeyDot(event);" CssClass="x_large" placeholder="Total Case"
                                                                                Style="text-align: right;"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="display: none;">
                                                                        <td class="innerfield_title" style="color: #1e69de;">
                                                                            <label>
                                                                                Total MRP</label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox runat="server" ID="txtTotMRP" BackColor="Transparent" Font-Bold="true"
                                                                                ReadOnly="true" CssClass="x_large" placeholder="Total MRP" Style="text-align: right;"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="innerfield_title" style="color: #1e69de;">
                                                                            <label>
                                                                                Total Rate</label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox runat="server" ID="txtAmount" CssClass="x_large" placeholder="Total Basic"
                                                                                Style="text-align: right;" BackColor="Transparent" Font-Bold="true" ReadOnly="true"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="innerfield_title" style="color: #1e69de;">
                                                                            <label>
                                                                                Total Tax</label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox runat="server" ID="txtTotTax" CssClass="x_large" placeholder="Total Tax"
                                                                                Style="text-align: right;" BackColor="Transparent" Font-Bold="true" ReadOnly="true"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="innerfield_title" style="color: #1e69de;">
                                                                            <label>
                                                                                Rate&nbsp;+&nbsp;Tax</label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox runat="server" ID="txtNetAmt" CssClass="x_large" placeholder="Basic&nbsp;+&nbsp;Tax"
                                                                                Style="text-align: right;" BackColor="Transparent" Font-Bold="true" ReadOnly="true"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="innerfield_title" style="display: none">
                                                                            <label>
                                                                                Scheme Amt.</label>
                                                                        </td>
                                                                        <td style="display: none">
                                                                            <asp:TextBox runat="server" ID="txttotalscheme" CssClass="x_large" BackColor="Transparent" Font-Bold="true"
                                                                                ReadOnly="true" placeholder="Scheme Amt." Style="text-align: right;"></asp:TextBox>
                                                                        </td>
                                                                        <td class="innerfield_title" style="color: #1e69de;">
                                                                            <label>
                                                                                Freight Amt.</label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox runat="server" ID="txtFreightAmnt" CssClass="x_large" BackColor="Transparent" Font-Bold="true"
                                                                                ReadOnly="true" placeholder="Scheme Amt." Style="text-align: right;"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="innerfield_title" style="display: none">
                                                                            <label>
                                                                                Special&nbsp;Disc.</label>
                                                                        </td>
                                                                        <td style="display: none">
                                                                            <asp:TextBox runat="server" ID="txtTotDisc" CssClass="x_large" placeholder="Special&nbsp;Disc." Style="text-align: right;"
                                                                                BackColor="Transparent" Font-Bold="true" ReadOnly="true"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td valign="top" style="width: 50%; padding-right: 20px">
                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">

                                                                    <tr>
                                                                        <td class="innerfield_title" style="padding-left: 20px; color: #1e69de;">
                                                                            <label>
                                                                                Gross Amt.</label>
                                                                        </td>
                                                                        <td style="float: right;">
                                                                            <asp:TextBox runat="server" ID="txtTotalGross" placeholder="Gross Amount" BackColor="Transparent"
                                                                                Font-Bold="true" ReadOnly="true" Style="text-align: right;"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="innerfield_title" style="padding-left: 20px; color: #1e69de;">
                                                                            <label>
                                                                                Round Off</label>
                                                                        </td>
                                                                        <td style="float: right;">
                                                                            <asp:TextBox runat="server" ID="txtRoundoff" placeholder="Round Off" BackColor="Transparent" AutoPostBack="true" 
                                                                                Font-Bold="true" Style="text-align: right;" OnTextChanged="txtRoundoff_TextChanged"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="innerfield_title" style="padding-left: 20px; color: #1e69de;">
                                                                            <label>
                                                                                Net Amt.</label>
                                                                        </td>
                                                                        <td style="float: right;">
                                                                            <asp:TextBox runat="server" ID="txtFinalAmt" placeholder="Net Amtount" BackColor="Transparent"
                                                                                Font-Bold="true" ReadOnly="true" Style="text-align: right;"></asp:TextBox>
                                                                        </td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td class="innerfield_title" style="padding-left: 20px; color: #1e69de;">
                                                                            <label>
                                                                                TCS(%)</label>
                                                                        </td>
                                                                        <td style="float: right;">
                                                                            <asp:TextBox runat="server" ID="txttcspercent" BackColor="Transparent"
                                                                                Font-Bold="true" ReadOnly="true" Style="text-align: right;"></asp:TextBox>
                                                                        </td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td class="innerfield_title" style="padding-left: 20px; color: #1e69de;">
                                                                            <label>
                                                                                TCS AMT</label>
                                                                        </td>
                                                                        <td style="float: right;">
                                                                            <asp:TextBox runat="server" ID="txttcsamt" BackColor="Transparent"
                                                                                Font-Bold="true" ReadOnly="true" Style="text-align: right;"></asp:TextBox>
                                                                        </td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td class="innerfield_title" style="padding-left: 20px; color: #1e69de;">
                                                                            <label>
                                                                                NET AMOUNT(WITH TCS)</label>
                                                                        </td>
                                                                        <td style="float: right;">
                                                                            <asp:TextBox runat="server" ID="txtnetwithtcsamt" BackColor="Transparent"
                                                                                Font-Bold="true" ReadOnly="true" Style="text-align: right;"></asp:TextBox>
                                                                        </td>
                                                                    </tr>

                                                                    <tr style="display: none;">
                                                                        <td class="innerfield_title" style="padding-left: 20px; color: #1e69de;">
                                                                            <label>
                                                                                Other Charge</label>
                                                                        </td>
                                                                        <td style="float: right;">
                                                                            <asp:TextBox runat="server" ID="txtOtherCharge" CssClass="x-large" placeholder="Other Charge"
                                                                                Style="text-align: right;" Text="0"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="display: none;">
                                                                        <td class="innerfield_title" style="padding-left: 20px; color: #1e69de;">
                                                                            <label>
                                                                                Adj. Amt</label>
                                                                        </td>
                                                                        <td style="float: right;">
                                                                            <asp:TextBox runat="server" ID="txtAdj" CssClass="x-large" placeholder="Adj. Amt"
                                                                                Style="text-align: right;" Text="0"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                        <tr style="display: none;">
                                            <td width="125" class="field_input">
                                                <asp:TextBox runat="server" ID="TextBox1" CssClass="x_large" placeholder="Tot.MRP"
                                                    Enabled="false" Visible="false"></asp:TextBox>
                                                <asp:TextBox runat="server" ID="txtRebate" CssClass="x_large" placeholder="Rebate(%)"
                                                    Enabled="false"></asp:TextBox>
                                            </td>
                                            <td width="90" class="field_title">
                                                <asp:Label ID="Label37" Text="Rebate Amt" runat="server"></asp:Label>
                                            </td>
                                            <td width="125" class="field_input">
                                                <asp:TextBox runat="server" ID="txtRebateValue" CssClass="x_large" placeholder="Rebate Amt"
                                                    Enabled="false"></asp:TextBox>
                                            </td>
                                            <td width="100" class="field_title">
                                                <asp:Label ID="Label40" Text="Add. Rebate(%)" runat="server"></asp:Label>
                                            </td>
                                            <td width="130" class="field_input">
                                                <asp:TextBox runat="server" ID="txtAddRebate" CssClass="x_large" placeholder="Add.Rebate(%)"
                                                    Enabled="false"></asp:TextBox>
                                            </td>
                                            <td width="90" class="field_title">
                                                <asp:Label ID="Label41" Text="Add.Rebate Amt" runat="server"></asp:Label>
                                            </td>
                                            <td class="field_input" width="130">
                                                <asp:TextBox runat="server" ID="txtAddRebateValue" CssClass="x_large" placeholder="Add.Rebate Amt"
                                                    Enabled="false"></asp:TextBox>
                                            </td>
                                            <td width="130" class="field_input">
                                                <asp:TextBox runat="server" ID="txtTotGrossWght" CssClass="x_large" placeholder="Tot.Grs Wt"
                                                    Enabled="true" Text="0"></asp:TextBox>
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
                                                        <td width="110">
                                                            <asp:TextBox ID="txtFromDate" runat="server" MaxLength="10" Width="70" Enabled="false"
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
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="110">
                                                            <asp:TextBox ID="txtToDate" runat="server" MaxLength="10" Width="70" Enabled="false"
                                                                placeholder="dd/mm/yyyy" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="Search"
                                                                Font-Bold="true"></asp:TextBox>
                                                            <asp:ImageButton ID="imgPopuptodate" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtenderToDate" PopupButtonID="imgPopuptodate"
                                                                runat="server" TargetControlID="txtToDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                        <td width="200">
                                                            <div class="btn_24_blue">
                                                                <span class="icon find_co"></span>
                                                                <asp:Button ID="btnSearchInvoice" runat="server" Text="Search" CssClass="btn_link"
                                                                    ValidationGroup="Search" OnClick="btnSearchInvoice_Click" />
                                                            </div>
                                                        </td>
                                                        <td width="60">
                                                            <asp:Label ID="Label120" runat="server" Text="Filter" Visible="false"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlWaybillFilter" Width="150" runat="server" class="chosen-select"
                                                                data-placeholder="Choose Waybill Filter Mode" AutoPostBack="true" OnSelectedIndexChanged="ddlWaybillFilter_SelectedIndexChanged"
                                                                Visible="false">
                                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="Without Waybill" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="With Waybill" Value="2"></asp:ListItem>
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
                                        <cc1:Grid ID="grdDespatchHeader" runat="server" CallbackMode="false" Serialize="true"
                                            FolderStyle="../GridStyles/premiere_blue" AutoGenerateColumns="false" OnDeleteCommand="DeleteRecordInvoice"
                                            OnRowDataBound="grdDespatchHeader_RowDataBound" AllowAddingRecords="false" AllowFiltering="true"
                                            AllowPageSizeSelection="true" PageSize="50">
                                            <ClientSideEvents OnBeforeClientDelete="OnBeforeInvoiceDelete" OnClientDelete="OnDeleteInvoiceDetails" />
                                            <FilteringSettings InitialState="Visible" />
                                            <Columns>
                                                <cc1:Column ID="Column12" DataField="Sl" HeaderText="SL" runat="server" Width="60">
                                                    <TemplateSettings TemplateId="slnoTemplateIN" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column19" DataField="SALEINVOICEID" HeaderText="SALEINVOICEID" runat="server"
                                                    Width="60" Visible="false" />
                                                <cc1:Column ID="Column91" DataField="SALEINVOICENO" HeaderText="INVOICE NO." runat="server"
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
                                                <cc1:Column ID="Column8" DataField="SALEINVOICEDATE" HeaderText="DATE" runat="server"
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
                                                <cc1:Column ID="Column99" DataField="WAYBILLNO" HeaderText="WAYBILL NO" runat="server"
                                                    Width="120" Visible="false">
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
                                                    Width="150" Wrap="true" Visible="false">
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
                                                <cc1:Column ID="Column199" DataField="DISTRIBUTORNAME" HeaderText="CUSTOMER" runat="server"
                                                    Width="250" Wrap="true" />
                                                <cc1:Column ID="Column89" DataField="DEPOTNAME" HeaderText="FACTORY" runat="server"
                                                    Width="150" Wrap="true" Visible="false">
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
                                                    Width="80" />
                                                <cc1:Column ID="Column17" DataField="TRANSPORTERNAME" HeaderText="TRANSPORTER" runat="server"
                                                    Width="170" Wrap="true">
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
                                                <cc1:Column ID="Column1" DataField="FORMREQUIRED" HeaderText="FORMREQ." runat="server"
                                                    Width="90" Visible="false" />
                                                <cc1:Column ID="Column4" DataField="FORMNO" HeaderText="FORMNO" runat="server" Width="60"
                                                    Visible="false" />
                                                <cc1:Column ID="Column6" DataField="FORMDATE" HeaderText="FORMDATE" runat="server"
                                                    Width="60" Visible="false" />
                                                <cc1:Column ID="Column24" DataField="ISVERIFIED" HeaderText="ISVERIFIED" Visible="false"
                                                    runat="server" Width="100" />
                                                <cc1:Column ID="Column312" DataField="NETAMOUNT" HeaderText="NET AMT" runat="server"
                                                    Align="right" Width="80" />

                                                <cc1:Column ID="Column25" DataField="ISVERIFIEDDESC" HeaderText="APPROVAL" runat="server"
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

                                                <cc1:Column ID="Column9" DataField="INTRANSITDESC" HeaderText="STOCK STATUS" runat="server"
                                                    Visible="false" Width="100" Wrap="true">
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

                                                <cc1:Column ID="Column11" DataField="USERNAME" HeaderText="ENTRY USER" runat="server"
                                                    Width="100" />


                                                <cc1:Column ID="Column311" DataField="GATEPASSNO" HeaderText="INVOICE NO" runat="server"
                                                    Width="80" Wrap="true" Visible="false">
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
                                                <cc1:Column ID="Column26" DataField="NEXTLEVELID" HeaderText="NEXTLEVELID" runat="server"
                                                    Visible="false" Width="100" />
                                                <cc1:Column ID="Column129" AllowEdit="false" AllowDelete="false" HeaderText="Update Waybill"
                                                    runat="server" Width="70" Wrap="true" Visible="false">
                                                    <TemplateSettings TemplateId="editWaybillBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column888" AllowEdit="false" AllowDelete="false" HeaderText="Update C-Form"
                                                    runat="server" Width="60" Wrap="true" Visible="false">
                                                    <TemplateSettings TemplateId="editCFormBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column7" AllowEdit="false" AllowDelete="false" HeaderText="PRINT"
                                                    runat="server" Width="60" Wrap="true">
                                                    <TemplateSettings TemplateId="PrintBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column92" AllowEdit="true" AllowDelete="true" HeaderText="EDIT" runat="server"
                                                    Width="60">
                                                    <TemplateSettings TemplateId="editBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column HeaderText="CANCEL" AllowEdit="false" AllowDelete="true" Width="90">
                                                    <TemplateSettings TemplateId="deleteBtnTemplateInvoice" />
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
                                                <cc1:GridTemplate runat="server" ID="editCFormBtnTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="filter_btn clipboard_text_co" id="btnGridCForm_<%# Container.PageRecordIndex %>"
                                                            onclick="CallServerMethodCForm(this)"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="deleteBtnTemplateInvoice">
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
                                        <asp:Button ID="btngrdedit" runat="server" Text="grdedit" Style="display: none" OnClick="btngrdedit_Click"
                                            CausesValidation="false" />
                                        <asp:Button ID="btngrdUpdateWaybill" runat="server" Text="Update Waybill" Style="display: none"
                                            OnClick="btngrdUpdateWaybill_Click" CausesValidation="false" />
                                        <asp:Button ID="btngrdUpdateForm" runat="server" Text="Update C-Form" Style="display: none"
                                            OnClick="btngrdUpdateForm_Click" CausesValidation="false" />
                                        <asp:Button ID="btnPrint" runat="server" Text="Print" Style="display: none" OnClick="btnPrint_Click"
                                            CausesValidation="false" />
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlAddEdit" runat="server" CssClass="modalPopup" Style="display: none; border-radius: 16px;"
                                    Width="80%" Height="87%" BackColor="White" BorderColor="#257daf"
                                    BorderWidth="8px" BorderStyle="Solid">
                                    <div style="background-color: #257daf; border-style: solid; border-color: Yellow; background-position: center; background-repeat: no-repeat; background-size: cover; height: 6%"
                                        align="center">
                                        <asp:Label Font-Bold="True" ID="Label26" runat="server" Text="Special Product Details"
                                            Font-Size="X-Large" ForeColor="Yellow"></asp:Label>
                                        <asp:ImageButton ID="imgclosebtn" ImageAlign="Right" ImageUrl="~/images/Cancel.png"
                                            Height="25px" Width="25px" runat="server" OnClick="imgclosebtn_click" />
                                        <hr />
                                    </div>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td width="13%" class="field_title">
                                                <asp:Label ID="Label28" Text="Product" runat="server"></asp:Label>
                                            </td>
                                            <td class="field_input">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="45%">
                                                            <asp:DropDownList ID="ddlFreeProduct" Width="350" runat="server" data-placeholder="Choose Product"
                                                                AutoPostBack="true" OnSelectedIndexChanged="ddlFreeProduct_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="15">&nbsp;
                                                        </td>
                                                        <td width="9%" class="innerfield_title">
                                                            <asp:Label ID="Label30" Text="Scheme Qty" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSchemeQty" runat="server" Width="192px" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="gridcontent-inner">
                                        <fieldset>
                                            <legend>PRODUCT DETAILS</legend>
                                            <div style="overflow: scroll; margin-bottom: 8px; height: 150px;">
                                                <asp:GridView ID="grdBatchDetails" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Available"
                                                    Width="100%" CssClass="zebra">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SL">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PRODUCTID" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgrdProductID" runat="server" Text='<%# Eval("PRODUCTID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PRODUCT">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgrdProductName" runat="server" Text='<%# Eval("PRODUCTNAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="BATCH NO">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgrdBatchNo" runat="server" Text='<%# Eval("BATCHNO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="STOCK QTY(PCS)">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgrdStockQty" runat="server" Text='<%# Eval("INVOICESTOCKQTY") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MRP">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgrdMRP" runat="server" Text='<%# Eval("MRP") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="BCP">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgrdBCPFree" runat="server" Text='<%# Eval("BCP") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MFG DATE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgrdMfgDate" runat="server" Text='<%# Eval("MFGDATE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="EXPR DATE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgrdExprDate" runat="server" Text='<%# Eval("EXPIRDATE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="QTY(PCS)">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgrdQty" runat="server" Text=""></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </fieldset>
                                    </div>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td align="center" colspan="2" style="padding-top: 10px;">
                                                <div class="btn_24_blue">
                                                    <span class="icon add_co"></span>
                                                    <asp:Button ID="btnBatchAdd" runat="server" CssClass="btn_link" Text="ADD" CausesValidation="false"
                                                        OnClick="btnBatchAdd_click" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="gridcontent-inner">
                                        <div style="overflow: scroll; margin-bottom: 8px; height: 100px;">
                                            <asp:GridView ID="grdBatchDetailsNew" runat="server" AutoGenerateColumns="false"
                                                EmptyDataText="No Records Available" Width="100%" CssClass="zebra">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PRODUCTID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgrdProductIDNew" runat="server" Text='<%# Eval("PRODUCTID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PRODUCT">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgrdProductNameNew" runat="server" Text='<%# Eval("PRODUCTNAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="BATCH NO">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgrdBatchNoNew" runat="server" Text='<%# Eval("BATCHNO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="STOCK QTY(PCS)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgrdStockQtyNew" runat="server" Text='<%# Eval("INVOICESTOCKQTY") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="MRP">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgrdMRPNew" runat="server" Text='<%# Eval("MRP") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="BCP">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgrdBCPFreeNew" runat="server" Text='<%# Eval("BRate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="MFG DATE">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgrdMfgDateNew" runat="server" Text='<%# Eval("MFDATE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="EXPR DATE">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgrdExprDateNew" runat="server" Text='<%# Eval("EXPRDATE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="QTY(PCS)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgrdQtyNew" runat="server" Text='<%# Eval("SCHEME_QTY") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td align="center" colspan="2" style="padding-top: 10px;">
                                                <div class="btn_24_blue">
                                                    <span class="icon disk_co"></span>
                                                    <asp:Button ID="btnBatchSubmit" runat="server" CssClass="btn_link" Text="Save" CausesValidation="false"
                                                        OnClick="btnBatchSubmit_click" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:LinkButton ID="lnkBatch" runat="server"></asp:LinkButton>
                                <ajaxToolkit:ModalPopupExtender ID="popup" runat="server" DropShadow="false" PopupControlID="pnlAddEdit"
                                    TargetControlID="lnkBatch" BackgroundCssClass="modalBackground">
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
                        <div id="light" class="white_content" runat="server">
                            <table width="50%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                <tr>
                                    <td width="10%" class="field_title">
                                        <asp:Label ID="Label20" runat="server" Text="Waybill No."></asp:Label>
                                    </td>
                                    <td width="20%" class="field_input">
                                        <asp:TextBox ID="txtWaybillUpdate" runat="server" Width="69%" placeholder="Waybill No"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <asp:Button ID="btnWaybillUpdate" runat="server" Text="Update" CssClass="btn_small btn_blue"
                                            OnClick="btnWaybillUpdate_Click" />&nbsp;&nbsp;<asp:Button ID="btnCloseLightbox"
                                                runat="server" Text="Close" CssClass="btn_small btn_blue" OnClick="btnCloseLightbox_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="fade" class="black_overlay" runat="server">
                        </div>
                    </div>
                    <div id="lightRejectionNote" class="white_content" runat="server">
                        <table width="50%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                            <tr>
                                <td width="10%" class="field_title">
                                    <asp:Label ID="Label17" runat="server" Text="Note"></asp:Label>&nbsp;<span class="req">*</span>
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
                                <td class="field_title">&nbsp;
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
                    <div id="light2" class="white_content" runat="server">
                        <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                            <tr>
                                <td width="10%" class="field_title">
                                    <asp:Label ID="Label15" runat="server" Text="C-Form No."></asp:Label>
                                </td>
                                <td width="15%" class="field_input">
                                    <asp:TextBox ID="txtCFormNo" runat="server" CssClass="x_large" placeholder="C-Form No."
                                        AutoCompleteType="Disabled" onfocus="disableautocompletion(this.id);"></asp:TextBox>
                                </td>
                                <td width="13%" class="field_title">
                                    <asp:Label ID="Label34" runat="server" Text="C-Form Date"></asp:Label>
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
                    <cc1:MessageBox ID="MessageBox1" runat="server" />
                </div>
            </div>
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



                var searchTimeout = null;
                function FilterTextBox_KeyUp() {
                    searchTimeout = window.setTimeout(performSearch, 500);
                }

                function performSearch() {
                    var searchValue = document.getElementById('FilterTextBox').value;
                    if (searchValue == FilterTextBox.WatermarkText) {
                        searchValue = '';
                    }
                    grdDespatchHeader.addFilterCriteria('SALEINVOICENO', OboutGridFilterCriteria.Contains, searchValue);
                    grdDespatchHeader.addFilterCriteria('SALEINVOICEDATE', OboutGridFilterCriteria.Contains, searchValue);
                    grdDespatchHeader.addFilterCriteria('TRANSPORTERNAME', OboutGridFilterCriteria.Contains, searchValue);
                    grdDespatchHeader.addFilterCriteria('DISTRIBUTORNAME', OboutGridFilterCriteria.Contains, searchValue);
                    grdDespatchHeader.addFilterCriteria('GATEPASSNO', OboutGridFilterCriteria.Contains, searchValue);
                    grdDespatchHeader.executeFilter();
                    searchTimeout = null;
                    return false;
                }

                function OnBeforeInvoiceDelete(record) {
                    record.Error = '';
                    document.getElementById("<%=hdnDespatchID.ClientID %>").value = record.SALEINVOICEID;
                    if (confirm("Are you sure you want to delete?"))
                        return true;
                    else
                        return false;
                }
                function OnDeleteInvoiceDetails(record) {
                    alert(record.Error);
                }

                function CallServerMethod(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnGridEdit_", "");
                    document.getElementById("<%=hdnDespatchID.ClientID %>").value = grdDespatchHeader.Rows[iRecordIndex].Cells[1].Value;
                    document.getElementById("<%=btngrdedit.ClientID %>").click();

                }

                function CallServerMethodWaybill(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnGridWaybill_", "");
                    document.getElementById("<%=hdnDespatchID.ClientID %>").value = grdDespatchHeader.Rows[iRecordIndex].Cells[1].Value;
                    document.getElementById("<%=hdnWaybillNo.ClientID %>").value = grdDespatchHeader.Rows[iRecordIndex].Cells[4].Value;
                    document.getElementById("<%=hdnCustomerID.ClientID %>").value = grdDespatchHeader.Rows[iRecordIndex].Cells[10].Value;
                    document.getElementById("<%=btngrdUpdateWaybill.ClientID %>").click();

                }
                function CallServerMethodPrint(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnPrint_", "");
                    document.getElementById("<%=hdnDespatchID.ClientID %>").value = grdDespatchHeader.Rows[iRecordIndex].Cells[1].Value;
                    document.getElementById("<%=Hdn_Print.ClientID %>").value = grdDespatchHeader.Rows[iRecordIndex].Cells[4].Value;
                    //            document.getElementById("<%=hdnCustomerID.ClientID %>").value = grdDespatchHeader.Rows[iRecordIndex].Cells[10].Value;
                    document.getElementById("<%=btnPrint.ClientID %>").click();

                }
                function CallServerMethodCForm(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnGridCForm_", "");
                    document.getElementById("<%=hdnDespatchID.ClientID %>").value = grdDespatchHeader.Rows[iRecordIndex].Cells[1].Value;
                    document.getElementById("<%=hdnCFormNo.ClientID %>").value = grdDespatchHeader.Rows[iRecordIndex].Cells[12].Value;
                    document.getElementById("<%=hdnCFormDate.ClientID %>").value = grdDespatchHeader.Rows[iRecordIndex].Cells[13].Value;
                    document.getElementById("<%=btngrdUpdateForm.ClientID %>").click();
                }

            </script>
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

