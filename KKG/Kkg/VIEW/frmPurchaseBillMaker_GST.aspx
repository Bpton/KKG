<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true"
    CodeFile="frmPurchaseBillMaker_GST.aspx.cs" Inherits="FACTORY_frmPurchaseBillMaker_GST" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function pageload(sender, args) {
            //calculation();
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
    </script>
    <script type="text/javascript">
        function disableautocompletion(id) {
            var TextBoxControl = document.getElementById(id);
            TextBoxControl.setAttribute("autocomplete", "off");
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active">
                                <span class="h_icon_he list_images"></span>
                                <h6>Purchase Bill Maker</h6>
                                <div id="divadd" runat="server" class="btn_30_light" style="float: right;">
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
                                                <fieldset>
                                                    <legend>PURCHASE BILL MAKER DETAILS</legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                        <tr id="divpobillno" runat="server">
                                                            <td width="90" class="field_title">
                                                                <asp:Label ID="lblpubill" runat="server" Text="Purchase No"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td width="110" class="field_input" colspan="3">
                                                                <asp:TextBox runat="server" ID="txtPbillno" Width="180" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td>&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr id="trproduct" runat="server">
                                                            <td width="90" class="field_title">
                                                                <asp:Label ID="lblentrydt" runat="server" Text="Entry Date"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td width="110" class="field_input">
                                                                <asp:TextBox ID="txtentrydt" runat="server" Width="70" Enabled="false" placeholder="dd/mm/yyyy"
                                                                    MaxLength="10"></asp:TextBox>
                                                                <asp:ImageButton ID="ImgEntryDt" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender4" PopupButtonID="ImgEntryDt" runat="server"
                                                                    TargetControlID="txtentrydt" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>
                                                            <td width="50" class="field_title">
                                                                <asp:Label ID="lblrefno" runat="server" Text="Ref No"></asp:Label>
                                                            </td>
                                                            <td width="80" class="field_input">
                                                                <asp:TextBox ID="txtrefno" runat="server" Width="74" onkeypress="return isNumberKey(event);"
                                                                    AutoCompleteType="Disabled" onfocus="disableautocompletion(this.id);"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="Reqtxtrefno" runat="server" ValidationGroup="POBILL"
                                                                    ForeColor="Red" ErrorMessage="RefNo is required!" ControlToValidate="txtrefno"
                                                                    ValidateEmptyText="false" SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
                                                            </td>
                                                            <td width="60" class="field_title">
                                                                <asp:Label ID="lbldt" runat="server" Text="Ref Date"></asp:Label>
                                                            </td>
                                                            <td width="110" class="field_input">
                                                                <asp:TextBox ID="txtrefdt" runat="server" Width="70" Enabled="false" placeholder="dd/mm/yyyy"
                                                                    MaxLength="10"></asp:TextBox>
                                                                <asp:ImageButton ID="ImgDt" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImgDt" runat="server"
                                                                    TargetControlID="txtrefdt" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>
                                                            <td width="60" class="field_title" id="tdlblledger" runat="server">
                                                                <asp:Label ID="lblledger" Text="Ledger" runat="server"></asp:Label>&nbsp;<span class="req">*</span>
                                                            </td>
                                                            <td class="field_input" id="tdddlledger" runat="server">
                                                                <asp:DropDownList ID="ddlledger" ValidationGroup="approval" Width="240" runat="server"
                                                                    class="chosen-select">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="Reqrdddlledger" runat="server" ControlToValidate="ddlledger"
                                                                    InitialValue="0" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="approval"
                                                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr id="trdate" runat="server">
                                                            <td width="90" class="field_title">
                                                                <asp:Label ID="lblVendor2" runat="server" Text="Vendor Name"></asp:Label>
                                                            </td>
                                                            <td class="field_input" colspan="3">
                                                                <asp:DropDownList ID="ddlvendor2" runat="server" Width="260" class="chosen-select"
                                                                    data-placeholder="SELECT VENDOR NAME">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="reqddlvendor2" runat="server" ValidationGroup="POBILL"
                                                                    ForeColor="Red" ErrorMessage="Vendor is required!" ControlToValidate="ddlvendor2"
                                                                    ValidateEmptyText="false" SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
                                                            </td>
                                                            <td width="80" class="field_title">
                                                                <asp:Label ID="lblfrondt" runat="server" Text="From Date"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td width="110" class="field_input">
                                                                <asp:TextBox ID="txtfromdt" runat="server" Width="70" Enabled="false" placeholder="dd/mm/yyyy"
                                                                    MaxLength="10">
                                                                </asp:TextBox>
                                                                <asp:ImageButton ID="ImgFromDt" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" PopupButtonID="ImgFromDt"
                                                                    TargetControlID="txtfromdt" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>
                                                            <td width="60" class="field_title">
                                                                <asp:Label ID="lbltodt" runat="server" Text="To Date"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td width="110" class="field_input">
                                                                <asp:TextBox ID="txtdate" runat="server" MaxLength="10" Width="70" placeholder="dd/mm/yyyy"
                                                                    ValidationGroup="datecheck" Enabled="false"></asp:TextBox>
                                                                <asp:ImageButton ID="imgPopupreqtodate" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender7" PopupButtonID="imgPopupreqtodate"
                                                                    runat="server" TargetControlID="txtdate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>
                                                            <td class="field_title">
                                                                <div class="btn_24_blue">
                                                                    <span class="icon find_co"></span>
                                                                    <asp:Button ID="btngrnserach" runat="server" Text="Search" CssClass="btn_link" OnClick="btngrnsearch_Click"
                                                                        ValidationGroup="POBILL" CausesValidation="false" />
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
                                            <legend>GRN DETAILS</legend>
                                            <asp:GridView ID="gvPurchasebill" EmptyDataText="There are no records available."
                                                CssClass="zebra" runat="server" OnRowDataBound="gvPurchasebill_OnRowDataBound"
                                                ShowFooter="true" AutoGenerateColumns="false" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="" HeaderStyle-Width="5">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkreq" runat="server" Text="" Checked='<%# (Eval("IsSelected").ToString().Trim() == "1" ? true : false) %>'
                                                                value='<%# Eval("GRNID") %>' onclick="javascript: Highlight1(this);" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" HeaderStyle-Width="20" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGrnID" runat="server" value='<%# Eval("GRNID") %>' Text='<%# Eval("GRNID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SL" HeaderStyle-Width="2">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="GRN NO" HeaderStyle-Width="20">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgrnno" runat="server" value='<%# Eval("GRNNO") %>' Text='<%# Eval("GRNNO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="GRN DATE" HeaderStyle-Width="20">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgrndt" runat="server" value='<%# Eval("GRNDATE") %>' Text='<%# Eval("GRNDATE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="LRGR NO" HeaderStyle-Width="20">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLrno" runat="server" value='<%# Eval("LRGRNO") %>' Text='<%# Eval("LRGRNO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="VEHICHLE NO" HeaderStyle-Width="20">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVechlno" runat="server" value='<%# Eval("VEHICHLENO") %>' Text='<%# Eval("VEHICHLENO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="TRANSPORTER ID" HeaderStyle-Width="5" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTransID" runat="server" value='<%# Eval("TRANSPORTERID") %>' Text='<%# Eval("TRANSPORTERID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="TRANSPORTER NAME" HeaderStyle-Width="20">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTransName" runat="server" value='<%# Eval("TRANSPORTER") %>' Text='<%# Eval("TRANSPORTER") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="RECEIVE QTY" HeaderStyle-Width="20">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblReceiveQty" runat="server" value='<%# Eval("RECEIVEDQTY") %>' Text='<%# Eval("RECEIVEDQTY") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="EXCHANGE RATE" HeaderStyle-Width="5">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtExchangeRate" runat="server" value='<%# Eval("EXCHANGERATE") %>' Width="50px" AutoPostBack="true"
                                                                OnTextChanged="txtExchangeRate_OnTextChanged"
                                                                Text='<%# Eval("EXCHANGERATE") %>' Height="18" Enabled="false"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="BASIC AMT" HeaderStyle-Width="20">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBasicAmt" runat="server" value='<%# Eval("BASICAMT") %>' Text='<%# Eval("BASICAMT") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="INPUT CGST" HeaderStyle-Width="20">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCGST" runat="server" value='<%# Eval("INPUTCGST") %>' Text='<%# Eval("INPUTCGST") %>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="INPUT SGST" HeaderStyle-Width="20">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSGST" runat="server" value='<%# Eval("INPUTSGST") %>' Text='<%# Eval("INPUTSGST") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="INPUT IGST" HeaderStyle-Width="20">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIGST" runat="server" value='<%# Eval("INPUTIGST") %>' Text='<%# Eval("INPUTIGST") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="TAXVALUE" HeaderStyle-Width="20">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltaxvalue" runat="server" value='<%# Eval("TAXVALUE") %>' Text='<%# Eval("TAXVALUE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="BILLED DATE" HeaderStyle-Width="10" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblbilldt" runat="server" value='<%# Eval("BILLDATE") %>' Text='<%# Eval("BILLDATE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ROUND OFF" HeaderStyle-Width="20">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblroundoff" runat="server" value='<%# Eval("ROUNDOFF") %>' Text='<%# Eval("ROUNDOFF") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="NET AMT" HeaderStyle-Width="20">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnetAmt" runat="server" value='<%# Eval("NETAMT") %>' Text='<%# Eval("NETAMT") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </fieldset>
                                    </div>
                                   
                                      <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td" style="display:none">
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px;">
                                                <fieldset>
                                                    <legend>TDS Details    &nbsp;&nbsp;
                                                                <div class="btn_24_blue" id="div_TDS" runat="server">
                                                                   <span id="icon" class="icon add_co"></span>
                                                                    <asp:Button ID="btnTDS" runat="server" Text="TDS Calculation" CssClass="btn_link" OnClick="btnTDS_Click"
                                                                        CausesValidation="false" />
                                                                </div></legend>
                                                    <table width="70%" cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td width="30">
                                                               <asp:Label ID="lblDeduction" Text="TDS Apllicable Amount" runat="server"></asp:Label>
                                                            </td>
                                                            <td width="30" align="left" class="innerfield_title">
                                                                <asp:Label ID="lblDeductableAmount" Text="0.00" runat="server"></asp:Label>
                                                            </td>
                                                            <td width="10" >
                                                                <asp:Label ID="lblPer" Text="Percentage (%)" runat="server"></asp:Label>
                                                            </td>                                                            
                                                             <td width="10" align="left" >
                                                                <asp:Label ID="lblPercentage" Text="0.00" runat="server"></asp:Label>
                                                            </td>
                                                            <td width="20">
                                                                <asp:Label ID="lblDeductionAmountText" Text="Deduction Amount" runat="server"></asp:Label>
                                                            </td>
                                                             <td width="20" align="left" class="innerfield_title">
                                                                <asp:Label ID="lblDeductionAmount" Text="0.00" runat="server"></asp:Label>
                                                            </td>
                                                             <td width="20">
                                                                <asp:Label ID="lblNetAmountText" Text="Net Amount" runat="server"></asp:Label>
                                                            </td>
                                                             <td width="20" align="left" class="innerfield_title">
                                                                <asp:Label ID="lblNetAmount" Text="0.00" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                    </table>
                                
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px;">
                                                <fieldset>
                                                    <legend>REMARKS </legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td width="220">
                                                                <asp:TextBox ID="txtremarks" runat="server" TextMode="MultiLine" Width="100%" Height="50"
                                                                    placeholder="Remarks" class="input_grow" MaxLength="55"></asp:TextBox>
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
                                                            <td>
                                                                <br />
                                                                <div class="btn_24_blue" id="btnsubmitdiv" runat="server">
                                                                    <span class="icon disk_co"></span>
                                                                    <asp:Button ID="btnsave" runat="server" Text="Save" CssClass="btn_link" ValidationGroup="POBILL"
                                                                        OnClick="btnsave_Click" />
                                                                </div>
                                                                &nbsp;&nbsp;
                                                                <div class="btn_24_blue">
                                                                    <span class="icon cross_octagon_co"></span>
                                                                    <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn_link" OnClick="btncancel_Click"
                                                                        CausesValidation="false" />
                                                                </div>
                                                                <div class="btn_24_green" id="divbtnapprove" runat="server">
                                                                    <span class="icon approve_co"></span>
                                                                    <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="btn_link" ValidationGroup="approval"
                                                                        OnClick="btnApprove_Click" />
                                                                </div>
                                                                &nbsp;&nbsp;
                                                                <div class="btn_24_red" id="divbtnreject" runat="server">
                                                                    <span class="icon reject_co"></span>
                                                                    <asp:LinkButton ID="btnReject" runat="server" Text="Reject" CssClass="btn_link" CausesValidation="false"
                                                                        OnClick="btnReject_Click" />
                                                                </div>
                                                                &nbsp;&nbsp;
                                                                <%--<div class="btn_24_blue" style="display:none;"  runat="server" id="div_btnPrint">
                                                            <span class="icon printer_co"></span>
                                                             <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btn_link" OnClick="btnPrint_Click" CausesValidation="false" />
                                                        </div>--%>
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
                                                        <td width="70" class="innerfield_title">
                                                            <asp:Label ID="lblvendor" runat="server" Text="Vendor"></asp:Label>
                                                        </td>
                                                        <td width="280">
                                                            <asp:DropDownList ID="ddlvendor" runat="server" Width="260" class="chosen-select"
                                                                data-placeholder="SELECT VENDOR NAME">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="90">
                                                            <asp:Label ID="Label9" runat="server" Text="From Date"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="120">
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
                                                        <td width="120">
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
                                                            <asp:HiddenField ID="hdn_PoBillNo" runat="server" />
                                                            <asp:HiddenField ID="hdn_pofield" runat="server" />
                                                            <asp:HiddenField ID="hdn_pono" runat="server" />
                                                            <asp:HiddenField ID="hdn_podelete" runat="server" />
                                                            <asp:HiddenField ID="hdn_FromDate" runat="server" />
                                                            <asp:HiddenField ID="hdn_ToDate" runat="server" />
                                                            <asp:HiddenField ID="Hdn_Fld" runat="server" />
                                                            <asp:HiddenField ID="Hdn_BasicAmt" runat="server" />
                                                            <asp:HiddenField ID="Hdn_TDSFlag" runat="server" />
                                                            <asp:HiddenField ID="Hdn_TDSID" runat="server" />
                                                            <asp:HiddenField ID="Hdn_TDS_DefaultAmt" runat="server" />
                                                            <asp:HiddenField ID="Hdn_NetAmt" runat="server" />
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
                                        <cc1:Grid ID="gvpodetails" runat="server" CallbackMode="true" Serialize="true" AutoGenerateColumns="false"
                                            AllowAddingRecords="false" PageSize="100" AllowPaging="true" AllowFiltering="true"
                                            AllowPageSizeSelection="true" OnDeleteCommand="DeleteRecordPoDetails" FolderStyle="../GridStyles/premiere_blue"
                                            EnableRecordHover="true" OnRowDataBound="gvpodetails_RowDataBound">
                                            <ClientSideEvents OnBeforeClientDelete="OnBeforeBILLdetailsDelete" OnClientDelete="OnDeleteBillDetails" />
                                            <FilteringSettings InitialState="Visible" />
                                            <Columns>
                                                <cc1:Column ID="Column4" DataField="Sl" HeaderText="SL" runat="server" Width="80"
                                                    Wrap="true">
                                                    <TemplateSettings TemplateId="slnoTemplatePO" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column6" DataField="ENTRYDATE" HeaderText="BILL DATE" runat="server"
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
                                                <cc1:Column ID="Column7" DataField="PUBNO" HeaderText="BILL NO" runat="server" Width="180"
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
                                                <cc1:Column ID="COLNETAMT" DataField="NETAMT" HeaderText="NET AMOUNT" runat="server"
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
                                                <cc1:Column ID="COLBILLAMT" DataField="BASICAMT" HeaderText="BASIC AMOUNT" runat="server"
                                                    Width="150" Wrap="true">
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
                                                <cc1:Column ID="Column8" DataField="ALREADYAMT" HeaderText="ALREADY BILLED AMT" runat="server"
                                                    Width="180" Wrap="true" Visible="false">
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
                                                <cc1:Column ID="Column1" DataField="Approval" HeaderText="APPROVAL" runat="server"
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

                                                <cc1:Column ID="Column_Print" AllowEdit="false" AllowDelete="true" HeaderText="PRINT" runat="server"
                                                    Width="60">
                                                    <TemplateSettings TemplateId="PrintBtnTemplate" />
                                                </cc1:Column>

                                                <cc1:Column ID="Column13" AllowEdit="true" AllowDelete="true" HeaderText="EDIT" runat="server"
                                                    Width="80" Wrap="true">
                                                    <TemplateSettings TemplateId="editBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column HeaderText="DELETE" AllowEdit="false" AllowDelete="true" Width="80" Wrap="true">
                                                    <TemplateSettings TemplateId="deleteBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column15" DataField="PUBID" HeaderText="PUBID" runat="server" Width="10"
                                                    Wrap="true" Visible="false">
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
                                                <cc1:Column ID="Column3" AllowEdit="true" AllowDelete="true" HeaderText="VIEW" runat="server"
                                                    Width="60" Visible="false">
                                                    <TemplateSettings TemplateId="ViewBtnTemplate" />
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
                                                <cc1:GridTemplate runat="server" ID="PrintBtnTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="filter_btn printer_co" id="btnPrint_<%# Container.PageRecordIndex %>"
                                                            onclick="CallServerMethodPrint(this)"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>

                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="editBtnTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="action-icons c-edit" id="btnGridEdit_<%# Container.PageRecordIndex %>"
                                                            onclick="CallServerMethod(this)"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="deleteBtnTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="action-icons c-delete" title="Delete" onclick="gvpodetails.delete_record(this)"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                                <cc1:GridTemplate runat="server" ID="slnoTemplatePO">
                                                    <Template>
                                                        <asp:Label ID="lblslnoPO" runat="server" Text='<%#Container.PageRecordIndex+1 %>'></asp:Label>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <%--<ScrollingSettings ScrollWidth="100%"  ScrollHeight="290" />--%>
                                        </cc1:Grid>
                                        <asp:Button ID="btngrdedit" runat="server" Text="grdedit" Style="display: none" OnClick="btngrdedit_Click"
                                            CausesValidation="false" />
                                        <asp:Button ID="btnview" runat="server" Text="View" Style="display: none" OnClick="btnview_Click"
                                            CausesValidation="false" />
                                        <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btn_link" CausesValidation="false"
                                            Style="display: none" OnClick="btnPrint_Click" />

                                        <asp:HiddenField ID="hdn_convertionqty" runat="server" />
                                        <asp:HiddenField ID="hdnCST" runat="server" />
                                        <asp:HiddenField ID="hdnExcise" runat="server" />
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
                var searchTimeout = null;
                function FilterTextBox_KeyUp() {
                    searchTimeout = window.setTimeout(performSearch, 500);
                }

                function performSearch() {
                    var searchValue = document.getElementById('FilterTextBox').value;
                    if (searchValue == FilterTextBox.WatermarkText) {
                        searchValue = '';
                    }
                    gvpodetails.addFilterCriteria('PODATE', OboutGridFilterCriteria.Contains, searchValue);
                    gvpodetails.addFilterCriteria('PONO', OboutGridFilterCriteria.Contains, searchValue);
                    gvpodetails.addFilterCriteria('TPUNAME', OboutGridFilterCriteria.Contains, searchValue);
                    gvpodetails.executeFilter();
                    searchTimeout = null;
                    return false;
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

                function CallServerMethod(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnGridEdit_", "");
                    document.getElementById("<%=hdn_PoBillNo.ClientID %>").value = gvpodetails.Rows[iRecordIndex].Cells[10].Value;
                    document.getElementById("<%=btngrdedit.ClientID %>").click();
                }

                function CallServerMethodPrint(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnPrint_", "");
                    document.getElementById("<%=hdn_PoBillNo.ClientID %>").value = gvpodetails.Rows[iRecordIndex].Cells[10].Value;
                    document.getElementById("<%=btnPrint.ClientID %>").click();
                }

                function CallServerViewMethod(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnGridEdit_", "");
                    document.getElementById("<%=hdn_PoBillNo.ClientID %>").value = gvpodetails.Rows[iRecordIndex].Cells[10].Value;
                    document.getElementById("<%=btnview.ClientID %>").click();
                }

                function OnBeforeBILLdetailsDelete(record) {
                    record.Error = '';
                    //alert(record.PUBID);
                    document.getElementById("<%=hdn_PoBillNo.ClientID %>").value = record.PUBID;
                    if (confirm("Are you sure you want to delete? "))
                        return true;
                    else
                        return false;
                }
                function OnDeleteBillDetails(record) {
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
            <script type="text/javascript">
                function ChangeQuantityEnable(id, enable) {
                    document.getElementById(id).disabled = !enable;
                }
            </script>
            <script language="javascript" type="text/javascript">
                function Highlight1(control) {
                    var grid = document.getElementById("<%= gvPurchasebill.ClientID%>");
                    var rowData = control.parentNode.parentNode;
                    var rowIndex = rowData.rowIndex - 1;
                    //var txtExchangeRate = $("input[id*=txtExchangeRate]")

                    //var txtExchangeRate = "ContentPlaceHolder1_gvPurchasebill_txtExchangeRate_" + rowIndex;
                    //var txtExchange = document.getElementById(txtExchangeRate);

                    if (control.checked) {
                        //$("input[type=text]").removeAttr("disabled");
                        $("input[id*=txtExchangeRate]").removeAttr("disabled");


                    } else {
                        //$("input[type=text]").attr("disabled", "disabled");
                        $("input[id*=txtExchangeRate]").attr("disabled", "disabled");
                    }
                }
            </script>

            <script type="text/javascript">
                $(function () {
                    $(".QuantityClass").on('change keyup paste', function () {

                        //Ctrl+Shift+J in Google Chrome to bring up the console which allows you to debug javascript
                        debugger;

                        var textBox = this;
                        var quantity = $(textBox).val();
                        var tableRows = $(textBox).parent().parent().children();

                        if (quantity != "") {
                            var price = tableRows[0].children[0].innerHTML;

                            var itemTotal = price * quantity;

                            tableRows[2].children[0].innerHTML = itemTotal;
                        }
                        else
                            tableRows[2].children[0].innerHTML = "";
                    });
                });
            </script>




        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
