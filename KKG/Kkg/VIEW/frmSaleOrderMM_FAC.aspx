<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmSaleOrderMM_FAC.aspx.cs" Inherits="VIEW_frmSaleOrderMM_FAC" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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

    <style type="text/css">
        .Background {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .Popup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 1000px;
            height: 350px;
        }

        .lbl {
            font-size: 16px;
            font-style: italic;
            font-weight: bold;
        }
    </style>
    <asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
            <asp:Image ID="Image1" ImageUrl="~/images/loading123.gif" AlternateText="Processing"
                runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
        PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <triggers>
                <asp:AsyncPostBackTrigger ControlID="Btnshow" EventName="Click" />
            </triggers>
            <%--<div class="page_title">
                <span class="title_icon"><span class="computer_imac"></span></span>
                <h3>
                    Sale Order</h3>
            </div>--%>
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active">
                                <span class="h_icon_he list_images"></span>
                                <h6>Sale Order Details</h6>
                                <div id="divnew" runat="server" class="btn_30_light" style="float: right;">
                                    <span id="icon" class="icon add_co"></span>
                                    <asp:Button ID="btnnewentry" runat="server" Text="Add New Record" CssClass="btn_link"
                                        OnClick="btnnewentry_Click" CausesValidation="false" />
                                </div>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="InputTable" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px;">
                                                <blockquote class="quote_orange" id="divcancelorder" runat="server">
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td class="innerfield_title">
                                                                <asp:CheckBox ID="chkactive" runat="server" Text=" " /><asp:Label ID="Label13" Text="ORDER CANCEL"
                                                                    runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </blockquote>
                                                <fieldset>
                                                    <legend>Sale Order No</legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                                        <tr>
                                                        </tr>
                                                        <tr>
                                                            <td width="70" class="field_title">
                                                                <asp:Label ID="Label6" runat="server" Text="Order Date"></asp:Label>
                                                            </td>
                                                            <td width="110" class="field_input">
                                                                <asp:TextBox ID="txtsaleorderdate" runat="server" Width="70" placeholder="dd/mm/yyyy"
                                                                    Enabled="false" MaxLength="10" ValidationGroup="datecheckpodetail" onkeypress="return isNumberKeyWithslash(event);"></asp:TextBox>
                                                                <asp:ImageButton ID="imgPopuppodate" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" TabIndex="1" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender6" PopupButtonID="imgPopuppodate"
                                                                    runat="server" TargetControlID="txtsaleorderdate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>
                                                            <td id="Td1" class="field_title" width="75" runat="server">
                                                                <asp:Label ID="Label7" runat="server" Text="Ref.PO No"></asp:Label>
                                                            </td>
                                                            <td class="field_input" width="100">
                                                                <asp:TextBox ID="txtrefsaleorderno" runat="server" Width="100" MaxLength="25" placeholder="Ref. SaleOrder No"></asp:TextBox>
                                                            </td>
                                                            <td width="55" class="field_title">
                                                                <asp:Label ID="Label12" runat="server" Text="Ref.Date"></asp:Label>
                                                            </td>
                                                            <td class="field_input" width="110">
                                                                <asp:TextBox ID="txtrefsaleorderdate" runat="server" Width="65" placeholder="dd/mm/yyyy"
                                                                    Enabled="false" MaxLength="10" ValidationGroup="datecheckpodetail" onkeypress="return isNumberKeyWithslash(event);"></asp:TextBox>
                                                                <asp:ImageButton ID="imgrefPopuppodate" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="imgrefPopuppodate"
                                                                    runat="server" TargetControlID="txtrefsaleorderdate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>
                                                            <td width="90" class="field_title" id="divsaleorderno" runat="server">
                                                                <asp:Label ID="Label15" runat="server" Text="Order No"></asp:Label>
                                                            </td>
                                                            <td class="field_input" id="divsaleorderno1" runat="server" width="220">
                                                                <asp:TextBox ID="txtsaleorderno" runat="server" Width="213px"></asp:TextBox>
                                                            </td>
                                                            <td>&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field_title">
                                                                <asp:Label ID="Label19" runat="server" Text="Business Segment"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:DropDownList ID="ddlbsegment" runat="server" Width="102" ValidationGroup="POFooter"
                                                                    OnSelectedIndexChanged="ddlbsegment_SelectedIndexChanged" AutoPostBack="true" class="chosen-select"
                                                                    data-placeholder="SELECT BS NAME">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="Reqddlbsegment" runat="server" ControlToValidate="ddlbsegment"
                                                                    InitialValue="0" SetFocusOnError="true" ErrorMessage="*" ForeColor="Red" ValidationGroup="POFooter"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td class="field_title">
                                                                <asp:Label ID="Label5" runat="server" Text="Group"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:DropDownList ID="ddlgroup" runat="server" Width="102" ValidationGroup="POFooter"
                                                                    OnSelectedIndexChanged="ddlgroup_SelectedIndexChanged" AutoPostBack="true" class="chosen-select"
                                                                    data-placeholder="SELECT GROUP NAME">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="CV_TPUNAME" runat="server" ControlToValidate="ddlgroup"
                                                                    InitialValue="0" SetFocusOnError="true" ErrorMessage="*" ForeColor="Red" ValidationGroup="POFooter"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td class="field_title">
                                                                <asp:Label ID="Label11" runat="server" Text="Customer"></asp:Label>&nbsp;<span class="req">*</span>
                                                            </td>
                                                            <td class="field_input" colspan="3">
                                                                <asp:DropDownList ID="ddlcustomer" runat="server" Width="280" ValidationGroup="POFooter"
                                                                    class="chosen-select" data-placeholder="Select Customer Name">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlcustomer"
                                                                    InitialValue="0" SetFocusOnError="true" ErrorMessage="*" ForeColor="Red" ValidationGroup="POFooter"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td class="field_title" width="90">
                                                                <asp:Label ID="Label16" runat="server" Text="Delivery Terms"></asp:Label>
                                                            </td>
                                                            <td class="field_input" width="220">
                                                                <asp:DropDownList ID="ddldeliveryterms" runat="server" Width="220" Height="28px"
                                                                    ValidationGroup="POFooter" class="chosen-select" data-placeholder="Select">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr id="trCurrency" runat="server">
                                                            <td style="padding-bottom: 17px;" class="field_title">
                                                                <asp:Label ID="Label3" runat="server" Text="Currency"></asp:Label>&nbsp;<span class="req">*</span>
                                                            </td>
                                                            <td class="field_input" colspan="5">
                                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                                    <tr>
                                                                        <td width="170">
                                                                            <asp:DropDownList ID="ddlCurrency" runat="server" Width="150px" Height="28px" class="chosen-select"
                                                                                Enabled="false" data-placeholder="Select Currency">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td width="120" style="padding-bottom: 8px;" class="innerfield_title">
                                                                            <asp:Label ID="Label14" runat="server" Text="With MRP"></asp:Label>
                                                                        </td>
                                                                        <td style="padding-bottom: 8px;">
                                                                            <asp:CheckBox ID="chkMRPTag" runat="server" Text=" " />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr id="trICDS" runat="server">
                                                            <td style="padding-bottom: 17px;" class="field_title">
                                                                <asp:Label ID="Label17" runat="server" Text="ICDS"></asp:Label>
                                                            </td>
                                                            <td width="160">
                                                                <asp:TextBox ID="txtICDS" runat="server" placeholder="ICDS No" Width="195"></asp:TextBox>
                                                            </td>
                                                            <td class="field_input" colspan="5">
                                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                                    <tr>
                                                                        <td width="15%" class="innerfield_title">
                                                                            <asp:Label ID="LabelICDSDate" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ICDS Date" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td width="160">
                                                                            <asp:TextBox ID="txtICDSDate" runat="server" Enabled="false" Width="110" placeholder="dd/MM/yyyy"
                                                                                MaxLength="10"></asp:TextBox>
                                                                            <asp:ImageButton ID="ImageButtonICDS" runat="server" ImageUrl="~/images/calendar.png"
                                                                                Width="24px" Height="24px" ImageAlign="AbsMiddle" />
                                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtenderICDS" TargetControlID="txtICDSDate"
                                                                                PopupButtonID="ImageButtonICDS" runat="server" Format="dd/MM/yyyy" Enabled="true"
                                                                                BehaviorID="CalendarExtenderICDSDate" CssClass="cal_Theme1" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr runat="server" id="trAddProduct">
                                                            <td class="field_input" colspan="9">
                                                                <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                                                    <tr>
                                                                        <td style="padding-bottom: 20px;" width="70" class="field_title">
                                                                            <asp:Label ID="Label2" runat="server" Text="Product"></asp:Label>&nbsp;<span class="req">*</span>
                                                                        </td>
                                                                        <td valign="top" width="310" class="field_input">
                                                                            <asp:DropDownList ID="ddlProductName" runat="server" Width="300" AutoPostBack="true"
                                                                                ValidationGroup="datecheckpodetail" OnSelectedIndexChanged="ddlProductName_SelectedIndexChanged"
                                                                                class="chosen-select" data-placeholder="Select Product Name">
                                                                                <asp:ListItem Text="Select Product Name" Value="0"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="CV_PRODUCTNAME" runat="server" ControlToValidate="ddlProductName"
                                                                                ValidationGroup="datecheckpodetail" InitialValue="0" SetFocusOnError="true" ErrorMessage="*"
                                                                                ForeColor="Red"></asp:RequiredFieldValidator>
                                                                        </td>
                                                                        <td width="80" class="field_input">
                                                                            <asp:TextBox ID="txtqty" runat="server" Width="70" Text="0.00" onkeypress="return isNumberKey(event);"
                                                                                ValidationGroup="datecheckpodetail"></asp:TextBox>
                                                                            <span class="label_intro">
                                                                                <asp:Label ID="Label4" runat="server" Text="ORDER QTY"></asp:Label>
                                                                                <asp:RequiredFieldValidator ID="CV_QTY" runat="server" ControlToValidate="txtqty"
                                                                                    ValidationGroup="datecheckpodetail" SetFocusOnError="true" ErrorMessage="Req"
                                                                                    Display="None"></asp:RequiredFieldValidator></span>
                                                                        </td>
                                                                        <td width="80" class="field_input">
                                                                            <asp:DropDownList ID="ddlpackingsize" runat="server" Width="80" ValidationGroup="datecheckpodetail"
                                                                                class="chosen-select" data-placeholder="UOM">
                                                                                <asp:ListItem Selected="True" Text="UOM" Value="0"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <span class="label_intro">
                                                                                <asp:Label ID="Label8" runat="server" Text="UOM"></asp:Label>
                                                                                <asp:RequiredFieldValidator ID="CV_CASE" runat="server" ControlToValidate="ddlpackingsize"
                                                                                    ValidationGroup="datecheckpodetail" InitialValue="0" SetFocusOnError="true" ErrorMessage="*Req"
                                                                                    ForeColor="Red"></asp:RequiredFieldValidator></span>
                                                                        </td>
                                                                        <td width="60" class="field_input">
                                                                            <asp:TextBox ID="txtrate" runat="server" Width="60" Enabled="false" onkeypress="return isNumberKey(event);"
                                                                                ValidationGroup="datecheckpodetail"></asp:TextBox>
                                                                            <span class="label_intro">
                                                                                <asp:Label ID="Label1" runat="server" Text="RATE"></asp:Label>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtrate"
                                                                                    ValidationGroup="datecheckpodetail" InitialValue="" SetFocusOnError="true" ErrorMessage="*Req"
                                                                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                                                            </span>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtdiscount" runat="server" Width="90" Text="0"
                                                                                onkeypress="return isNumberKey(event);" ValidationGroup="datecheckpodetail"></asp:TextBox>
                                                                            <span class="label_intro">
                                                                                <asp:Label ID="Label23" runat="server" Text="Discount(%)"></asp:Label>

                                                                            </span>
                                                                        </td>
                                                                        <td width="110" class="field_input">
                                                                            <asp:TextBox ID="txtrequireddate" runat="server" Width="70" Enabled="false" placeholder="dd/mm/yyyy"
                                                                                MaxLength="10" ValidationGroup="datecheckpodetail" onkeypress="return isNumberKeyWithslash(event);"></asp:TextBox>
                                                                            <asp:ImageButton ID="imgPopup" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                                runat="server" Height="24" />
                                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender4" PopupButtonID="imgPopup" runat="server"
                                                                                TargetControlID="txtrequireddate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                            </ajaxToolkit:CalendarExtender>
                                                                            <span class="label_intro">
                                                                                <asp:Label ID="Label26" runat="server" Text="FROM DATE"></asp:Label>
                                                                        </td>
                                                                        <td width="110" class="field_input">
                                                                            <asp:TextBox ID="txttorequireddate" runat="server" Width="70" Enabled="false" placeholder="dd/mm/yyyy"
                                                                                MaxLength="10" ValidationGroup="datecheckpodetail" onkeypress="return isNumberKeyWithslash(event);"></asp:TextBox>
                                                                            <asp:ImageButton ID="imgpopupto" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                                runat="server" Height="24" />
                                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender5" PopupButtonID="imgpopupto" runat="server"
                                                                                TargetControlID="txttorequireddate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                            </ajaxToolkit:CalendarExtender>
                                                                            <span class="label_intro">
                                                                                <asp:Label ID="Label27" runat="server" Text="TO DATE"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="top" class="field_input">
                                                                            <div class="btn_24_blue">
                                                                                <span class="icon add_co"></span>
                                                                                <asp:Button ID="btnadd" runat="server" Text="Add" CssClass="btn_link" ValidationGroup="datecheckpodetail"
                                                                                    OnClick="btnadd_Click" />
                                                                            </div>
                                                                            <asp:HiddenField ID="Hdn_Fld" runat="server" />
                                                                            <asp:HiddenField ID="Hdn_productType" runat="server" />
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



                                    <body>



                                        <ajaxToolkit:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panl1" TargetControlID="Button1"
                                            CancelControlID="Button2" BackgroundCssClass="Background">
                                        </ajaxToolkit:ModalPopupExtender>
                                        <asp:Panel ID="Panl1" runat="server" CssClass="Popup" align="center" Style="display: none">
                                            <br />
                                            <legend>CUSTOMER DETAILS</legend>
                                            <legend>----------------</legend>
                                            <table>
                                                <tr>
                                                </tr>
                                            </table>
                                            <table>
                                                <tr>
                                                    <td width="70" class="field_title">
                                                        <asp:Label ID="Label20" runat="server" Text="Order Date"></asp:Label>
                                                    </td>
                                                    <td width="70" class="field_title">
                                                        <asp:Label ID="lblOrderDate" runat="server" Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="70" class="field_title">
                                                        <asp:Label ID="lbl221" runat="server" Text="Customer Name"></asp:Label>
                                                    </td>
                                                    <td width="450" class="field_title">
                                                        <asp:Label ID="lblCustomer" runat="server" Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>



                                            <legend>SALE ORDER PRODUCT DETAILS</legend>
                                            <legend>--------------------------</legend>

                                            <asp:GridView ID="grvSaleOrderCopy" runat="server" Width="100%" CssClass="zebra"
                                                AutoGenerateColumns="false" ShowFooter="false" AlternatingRowStyle-BackColor="AliceBlue"
                                                EmptyDataText="No Records Available">
                                                <Columns>
                                                    <asp:BoundField HeaderText="Product Name" DataField="PRODUCTNAME" />
                                                    <asp:BoundField HeaderText="Order Qty" DataField="ORDERQTY" />
                                                    <asp:BoundField HeaderText="Pack Size" DataField="UOMNAME" />
                                                    <asp:BoundField HeaderText="Rate" DataField="RATE" />
                                                    <asp:BoundField HeaderText="Amount" DataField="AMOUNT" />
                                                </Columns>

                                            </asp:GridView>


                                            <table>
                                                <tr>
                                                    <td width="70" class="field_title">
                                                        <asp:Label ID="Label21" runat="server" Text="Total Pcs"></asp:Label>
                                                    </td>
                                                    <td width="100" class="field_title">
                                                        <asp:Label ID="lblTotalPcs" runat="server" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    &nbsp;
                                     
                                            <td width="70" class="field_title">
                                                <asp:Label ID="lbl2220" runat="server" Text="Total Amount"></asp:Label>
                                            </td>
                                                    <td width="100" class="field_title">
                                                        <asp:Label ID="lblTotalAmnt" runat="server" Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table>
                                                <tr>
                                                </tr>
                                            </table>



                                            <legend>----------------------</legend>


                                            <div class="btn_24_green" runat="server" id="divBtnSave">
                                                <span class="icon disk_co"></span>
                                                <asp:Button ID="btnsave" runat="server" Text="Save" CssClass="btn_link" ValidationGroup="POFooter"
                                                    OnClick="btnsave_Click" />
                                            </div>
                                            <div class="btn_24_red">
                                                <span class="icon cross_octagon_co"></span>
                                                <asp:Button ID="Button2" runat="server" Text="Close" CssClass="btn_link" />
                                            </div>





                                        </asp:Panel>

                                    </body>





                                    <div class="gridcontent-inner">
                                        <fieldset>
                                            <legend>SALE ORDER PRODUCT DETAILS</legend>
                                            <cc1:Grid ID="gvSaleOrder" runat="server" CallbackMode="true" Serialize="true" ShowFooter="true"
                                                ShowColumnsFooter="true" AutoGenerateColumns="false" EnableRecordHover="true"
                                                AllowAddingRecords="false" PageSize="500" OnRowDataBound="gvSaleOrder_RowDataBound"
                                                AllowFiltering="true" AllowSorting="false" AllowPageSizeSelection="false" FolderStyle="../GridStyles/premiere_blue">
                                                <FilteringSettings InitialState="Visible" />
                                                <Columns>
                                                    <cc1:Column ID="Column1" DataField="PRODUCTID" ReadOnly="true" HeaderText="ID" runat="server"
                                                        Visible="false" Width="100" />
                                                    <cc1:Column ID="Column12" DataField="Sl" HeaderText="SL" runat="server" Width="60">
                                                        <TemplateSettings TemplateId="slnoTemplate" />
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column2" DataField="PRODUCTNAME" HeaderText="PRODUCT" runat="server"
                                                        Width="100%" Wrap="true">
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
                                                    <cc1:Column ID="Column3" DataField="ORDERQTY" HeaderText="ORDER QTY" runat="server"
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
                                                    <cc1:Column ID="Column11" DataField="UOMNAME" HeaderText="UOM" runat="server" Width="170"
                                                        Wrap="true">
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
                                                    <cc1:Column ID="Column5" DataField="RATE" HeaderText="RATE" runat="server" Width="80">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column9" DataField="DISCPER" HeaderText="DISCOUNT(%)" runat="server"
                                                        Width="80" Wrap="true">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column90" DataField="DISCAMNT" HeaderText="DISCOUNT(Amnt)" runat="server"
                                                        Width="80" Wrap="true">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column19" DataField="AMOUNT" HeaderText="AMOUNT" runat="server" Width="160">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column15" DataField="REQUIREDDATE" HeaderText="FROM DATE" runat="server"
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
                                                    <cc1:Column ID="Column10" DataField="REQUIREDTODATE" HeaderText="TO DATE" runat="server"
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
                                                    <cc1:Column HeaderText="DELETE" AllowEdit="false" AllowDelete="true" Width="70">
                                                        <TemplateSettings TemplateId="deleteBtnTemplate" />
                                                    </cc1:Column>
                                                </Columns>
                                                <FilteringSettings MatchingType="AnyFilter" />
                                                <Templates>
                                                    <cc1:GridTemplate runat="server" ID="deleteBtnTemplate">
                                                        <Template>
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
                                                <ScrollingSettings ScrollWidth="100%" ScrollHeight="220" />
                                            </cc1:Grid>
                                            <asp:Button ID="btngrddelete" runat="server" Text="grddelete" Style="display: none"
                                                OnClick="btngrddelete_Click" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete this record?');" />
                                        </fieldset>
                                    </div>
                                    <div class="gridcontent-inner" style="display: none;">
                                        <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                            <tr>
                                                <td class="field_title">
                                                    <div class="gridcontent-shortstock">
                                                        <fieldset>
                                                            <legend>Terms & Conditions</legend>
                                                            <cc1:Grid ID="grdTerms" runat="server" CallbackMode="true" Serialize="true" AutoGenerateColumns="false"
                                                                FolderStyle="../GridStyles/premiere_blue" EnableRecordHover="true" AllowAddingRecords="false"
                                                                AllowFiltering="false" AllowPaging="false" PageSize="100" AllowSorting="false"
                                                                OnRowDataBound="grdTerms_RowDataBound" AllowPageSizeSelection="false">
                                                                <FilteringSettings InitialState="Visible" />
                                                                <Columns>
                                                                    <cc1:Column ID="Column16" DataField="SLNO" HeaderText="Sl No" runat="server" Width="60">
                                                                        <TemplateSettings TemplateId="tplTermsNumbering" />
                                                                    </cc1:Column>
                                                                    <cc1:Column ID="Column17" DataField="DESCRIPTION" ReadOnly="true" HeaderText="TERMS & Conditions"
                                                                        runat="server" Width="320" Wrap="true" />
                                                                    <cc1:Column ID="Column18" DataField="ID" ReadOnly="true" HeaderText="TERMSID" runat="server"
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
                                                                <ScrollingSettings ScrollWidth="100%" ScrollHeight="160" />
                                                            </cc1:Grid>
                                                        </fieldset>
                                                    </div>
                                                </td>
                                                <td class="field_title">&nbsp;
                                                </td>
                                                <td class="field_title" runat="server" id="tdQty">
                                                    <div class="gridcontent-shortstock">
                                                        <fieldset>
                                                            <legend>Quantity Details</legend>
                                                            <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label18" runat="server" Text="Total Case"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtTotalCase" runat="server" Width="120" Text="0" Enabled="false"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label22" runat="server" Text="Total PCS"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtTotalPCS" runat="server" Width="120" Text="0" Enabled="false"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </fieldset>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px;">
                                                <fieldset>
                                                    <legend>REMARKS</legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td width="320">
                                                                <asp:TextBox ID="txtremarks" runat="server" MaxLength="255" TextMode="MultiLine"
                                                                    Height="50" CssClass="large" placeholder="Remarks" class="input_grow"></asp:TextBox>
                                                            </td>
                                                            <td width="15">&nbsp;
                                                            </td>
                                                            <td>


                                                                <asp:Button ID="Button1" CssClass="btn_link" runat="server" Text="Check&Save" Style="display: none" />
                                                                <div class="btn_24_blue">
                                                                    <span class="icon show"></span>
                                                                    <asp:Button ID="Btnshow" runat="server" Text="Check&Save" OnClick="Btnshow_Click" CssClass="btn_link" />
                                                                </div>

                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                                <div class="btn_24_red">
                                                                    <span class="icon cross_octagon_co"></span>
                                                                    <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn_link" OnClick="btncancel_Click"
                                                                        CausesValidation="false" />
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
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
                                                            <asp:Label ID="Label9" runat="server" Text="From Date"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="110">
                                                            <asp:TextBox ID="txtfromdate" runat="server" MaxLength="15" Width="70" Enabled="false"
                                                                placeholder="dd/mm/yyyy" ValidationGroup="datecheck"></asp:TextBox>
                                                            <asp:ImageButton ID="imgPopupfromdate" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopupfromdate"
                                                                runat="server" TargetControlID="txtfromdate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                        <td width="70">
                                                            <asp:Label ID="Label10" runat="server" Text="To Date"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="110">
                                                            <asp:TextBox ID="txttodate" runat="server" MaxLength="10" Width="70" placeholder="dd/mm/yyyy"
                                                                ValidationGroup="datecheck" Enabled="false"></asp:TextBox>
                                                            <asp:ImageButton ID="imgpopuptodate" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="imgpopuptodate"
                                                                runat="server" TargetControlID="txttodate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                        <td>
                                                            <div class="btn_24_blue">
                                                                <span class="icon find_co"></span>
                                                                <asp:Button ID="btngvfill" runat="server" Text="Search" CssClass="btn_link" ValidationGroup="datecheck"
                                                                    OnClick="btngvfill_Click" />
                                                            </div>
                                                            <asp:HiddenField ID="hdn_saleorderno" runat="server" />
                                                            <asp:HiddenField ID="hdn_saleorderdelete" runat="server" />
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
                                        <cc1:Grid ID="gvsaleorderdetailsdetails" runat="server" CallbackMode="true" Serialize="true"
                                            AutoGenerateColumns="false" AllowAddingRecords="false" AllowFiltering="true"
                                            AllowPageSizeSelection="true" PageSize="200" OnDeleteCommand="DeleteRecordSaleOrderDetails"
                                            FolderStyle="../GridStyles/premiere_blue" EnableRecordHover="true">
                                            <ClientSideEvents OnBeforeClientDelete="OnBeforesaleorderdetailsDelete" OnClientDelete="OnDeletesaleorderDetails" />
                                            <FilteringSettings InitialState="Visible" />
                                            <Columns>
                                                <cc1:Column ID="Column14" DataField="SALEORDERID" HeaderText="SALEORDERID" runat="server"
                                                    Width="10" Visible="false">
                                                </cc1:Column>
                                                <cc1:Column ID="Column4" DataField="Sl" HeaderText="SL" runat="server" Width="60">
                                                    <TemplateSettings TemplateId="slnoTemplatePO" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column6" DataField="SALEORDERDATE" HeaderText="ORDER DATE" runat="server"
                                                    Width="70%" Wrap="true">
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
                                                <cc1:Column ID="Column7" DataField="SALEORDERNO" HeaderText="ORDER NO" runat="server"
                                                    Width="100%" Wrap="true">
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
                                                <cc1:Column ID="Column8" DataField="CUSTOMERNAME" HeaderText="CUSTOMER NAME" runat="server"
                                                    Width="150%" Wrap="true">
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
                                                <cc1:Column ID="Column21" DataField="TOTALVALUE" HeaderText="TOTAL VALUE" runat="server"
                                                    Width="100%" Wrap="true">
                                                </cc1:Column>
                                                <cc1:Column ID="Column22" DataField="TOTALCASE" HeaderText="TOTAL CASE" runat="server"
                                                    Width="100%" Wrap="true">
                                                </cc1:Column>
                                                <cc1:Column ID="Column23" DataField="TOTALPCS" HeaderText="TOTAL PCS" runat="server"
                                                    Width="100%" Wrap="true">
                                                </cc1:Column>
                                                <cc1:Column ID="Column20" AllowEdit="true" AllowDelete="true" HeaderText="VIEW" runat="server"
                                                    Width="60">
                                                    <TemplateSettings TemplateId="VIEWBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column13" AllowEdit="true" AllowDelete="true" HeaderText="EDIT" runat="server"
                                                    Width="60">
                                                    <TemplateSettings TemplateId="editBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column HeaderText="DELETE" AllowEdit="false" AllowDelete="true" Width="95">
                                                    <TemplateSettings TemplateId="deleteBtnTemplatePO" />
                                                </cc1:Column>
                                            </Columns>
                                            <FilteringSettings MatchingType="AnyFilter" />
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="VIEWBtnTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="h_icon zoom_sl" title="View" id="btnGridView_<%# Container.PageRecordIndex %>"
                                                            onclick="CallViewServerMethod(this)"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                                <cc1:GridTemplate runat="server" ID="editBtnTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="action-icons c-edit" title="Edit" id="btnGridEdit_<%# Container.PageRecordIndex %>"
                                                            onclick="CallServerMethod(this)"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="deleteBtnTemplatePO">
                                                    <Template>
                                                        <a href="javascript: //" class="action-icons c-delete" title="Delete" onclick="gvsaleorderdetailsdetails.delete_record(this)"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                                <cc1:GridTemplate runat="server" ID="slnoTemplatePO">
                                                    <Template>
                                                        <asp:Label ID="lblslnoPO" runat="server" Text='<%#Container.PageRecordIndex+1 %>'></asp:Label>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <ScrollingSettings ScrollWidth="100%" ScrollHeight="290" />
                                        </cc1:Grid>
                                        <asp:Button ID="btngrdedit" runat="server" Text="grdedit" Style="display: none" OnClick="btngrdedit_Click"
                                            CausesValidation="false" />
                                        <asp:Button ID="btngrdview" runat="server" Text="View" Style="display: none" OnClick="btngrdview_Click"
                                            CausesValidation="false" />
                                        <asp:HiddenField ID="hdn_saleorderid" runat="server" />
                                        <asp:HiddenField ID="hdn_bsid" runat="server" />
                                        <asp:HiddenField ID="hdn_bsname" runat="server" />
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                    <cc1:MessageBox ID="MessageBox1" runat="server" />
                    <span class="clear"></span>
                </div>
                <span class="clear"></span>
            </div>
            <script type="text/javascript">
                function ValidateDate(sender, args) {
                    var dateString = document.getElementById(sender.controltovalidate).value;
                    var regex = /((0[0-9]|1[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$/;
                    if (regex.test(dateString)) {
                        var parts = dateString.split("/");
                        var dt = new Date(parts[1] + "/" + parts[0] + "/" + parts[2]);
                        args.IsValid = (dt.getDate() == parts[0] && dt.getMonth() + 1 == parts[1] && dt.getFullYear() == parts[2]);
                    } else {
                        args.IsValid = false;
                    }
                }
            </script>
            <script type="text/javascript">
                function isNumberKey(evt) {
                    var charCode = (evt.which) ? evt.which : event.keyCode;
                    if ((charCode < 48 || charCode > 57) && (charCode != 8))
                        return false;

                    return true;
                }

                function isNumberKeyWithslash(evt) {
                    var charCode = (evt.which) ? evt.which : event.keyCode;
                    if ((charCode < 48 || charCode > 57) && (charCode != 8) && (charCode != 47))
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

                function OnBeforeDelete(record) {
                    record.Error = '';
                    document.getElementById("<%=Hdn_Fld.ClientID %>").value = record.ID;
                    if (confirm("Are you sure you want to delete? "))
                        return true;
                    else
                        return false;
                }
                function OnDelete(record) {
                    alert(record.Error);
                }

                function CallServerMethod(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnGridEdit_", "");
                    document.getElementById("<%=hdn_saleorderid.ClientID %>").value = gvsaleorderdetailsdetails.Rows[iRecordIndex].Cells[0].Value;
                    document.getElementById("<%=hdn_saleorderno.ClientID %>").value = gvsaleorderdetailsdetails.Rows[iRecordIndex].Cells[3].Value;
                    document.getElementById("<%=btngrdedit.ClientID %>").click();
                }

                function CallViewServerMethod(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnGridView_", "");
                    document.getElementById("<%=hdn_saleorderid.ClientID %>").value = gvsaleorderdetailsdetails.Rows[iRecordIndex].Cells[0].Value;
                    document.getElementById("<%=hdn_saleorderno.ClientID %>").value = gvsaleorderdetailsdetails.Rows[iRecordIndex].Cells[3].Value;
                    document.getElementById("<%=btngrdview.ClientID %>").click();
                }

                function CallDeleteServerMethod(oLink) {
                    var iRecordIndexdelete = oLink.id.toString().replace("btnGridDelete_", "");
                    document.getElementById("<%=hdn_saleorderdelete.ClientID %>").value = gvSaleOrder.Rows[iRecordIndexdelete].Cells[0].Value;
                    document.getElementById("<%=btngrddelete.ClientID %>").click();
                }

                function OnBeforesaleorderdetailsDelete(record) {
                    record.Error = '';
                    document.getElementById("<%=hdn_saleorderid.ClientID %>").value = record.SALEORDERID;
                    if (confirm("Are you sure you want to delete? "))
                        return true;
                    else
                        return false;
                }
                function OnDeletesaleorderDetails(record) {
                    alert(record.Error);
                }

                function ShowPanel(ctlbtn, ctladd, ctlshow) {
                    if (document.getElementById(ctlbtn)) {
                        document.getElementById(ctladd).style.display = '';
                        document.getElementById(ctlshow).style.display = 'none';

                        return false;
                    }
                }

                function ClosePanel(ctlbtn, ctladd, ctlshow) {
                    if (document.getElementById(ctlbtn)) {
                        document.getElementById(ctlshow).style.display = true;
                        document.getElementById(ctladd).style.display = false;
                        document.getElementById("ctl00_ContentPlaceHolder1_btnAdd").disabled = false;
                        return false;
                    }
                }

                function ShowHideInputs() {

                    if (document.all.InputTable.style.display == "none") {
                        document.all.InputTable.style.display = "inline";
                        document.all.HideInput.value = " Hide ";
                    }
                    Else
                    {
                        document.all.InputTable.style.display = "none";
                        document.all.HideInput.value = " Show ";
                    }

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
