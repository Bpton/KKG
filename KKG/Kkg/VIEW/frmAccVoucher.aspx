<%@ Page Title="Welcome to McNROE" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true"
    CodeFile="frmAccVoucher.aspx.cs" Inherits="VIEW_frmAccVoucher" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
       <style>
        th {
            background: cornflowerblue !important;
            color: white !important;
            position: sticky !important;
            top: 0;
            box-shadow: 0 2px 2px -1px rgb(255, 106, 0);
            width: 400px;
        }

        th, td {
            padding: 0.25rem;
        }
    </style>
    <asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
            <asp:Image ID="Image1" ImageUrl="../images/loading123.gif" AlternateText="Processing"
                runat="server" />
            <br />
            <b style="font-size: small;">Please wait...</b>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
        PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <i class="fa fa-info">Shortcut Key Info</i>
                            <div id="show" runat="server" style="align-content:center">
                                                                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <label style="color:blueviolet">ADD NEW(ALT+N), </label>&nbsp;
                                                                   <label style="color:orangered">DEBIT ADD(ALT+B), </label>&nbsp;
                                                                   <label style="color:blueviolet">CREDIT ADD(ALT+C), </label>&nbsp;
                                                                   <label style="color:orangered">DEBIT DELETE(ALT+H),</label>&nbsp;
                                                                   <label style="color:blueviolet">CREDIT DELETE(ALT+G),</label>&nbsp;
                                                                   <label style="color:orangered">SAVE(ALT+S),</label>
                                                                   <label style="color:blueviolet">SAVE WITH PRINT(ALT+P),</label>&nbsp;
                                                                   <label style="color:orangered">CLOSE(ALT+X),</label>&nbsp;
                            </div>
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>
                                    VOUCHER DETAILS</h6>
                                <div class="btn_30_light" style="float: right;" id="btnaddhide" runat="server">
                                    <span class="icon add_co"></span>
                                    <asp:Button ID="Btnadd" runat="server" Text="Add New Voucher" CssClass="btn_link"
                                        CausesValidation="false" OnClick="Btnadd_Click" UseSubmitBehavior="false" AccessKey="N" /></div>
                            </div>
                            <div class="widget_content">


                                <asp:Panel ID="pnlAdd" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" style="padding-left: 8px;">
                                                <fieldset>
                                                    <legend>Voucher Details</legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" style="background-color: #e3ede1;">
                                                        <tr id="trvoucherno" runat="server">
                                                            <td class="field_title">
                                                                Voucher No
                                                            </td>
                                                            <td colspan="5">
                                                                <asp:TextBox ID="txtvoucherno" runat="server" Width="180px" Enabled="false" Font-Bold="true" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="70" class="field_title">
                                                                Voucher<span class="req">*</span>
                                                            </td>
                                                            <td width="140" class="field_input">
                                                                <asp:DropDownList ID="ddlvouchertype" runat="server" class="chosen-select" Width="140"
                                                                    Enabled="false" OnSelectedIndexChanged="ddlvouchertype_SelectedIndexChanged"
                                                                    AutoPostBack="true">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlvouchertype"
                                                                    CssClass="dropup" Display="Dynamic" InitialValue="0" ErrorMessage="Select Group"
                                                                    ForeColor="Red" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                                                    TargetControlID="RequiredFieldValidator2" />
                                                            </td>
                                                            <td width="140" id="trpaymentmode" runat="server">
                                                                <table width="140" cellpadding="0" cellspacing="0" border="0">
                                                                    <tr>
                                                                        <td width="50" class="field_title">
                                                                            MODE&nbsp;&nbsp;<span class="req">*</span>
                                                                        </td>
                                                                        <td width="70" class="field_input">
                                                                            <asp:DropDownList ID="ddlMode" Width="70" runat="server" class="chosen-select" AutoPostBack="true"
                                                                                OnSelectedIndexChanged="ddlMode_SelectedIndexChanged">
                                                                                <asp:ListItem Value="B" Text="Bank" />
                                                                                <asp:ListItem Value="C" Text="Cash" />
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td width="61" class="field_title">
                                                                Region<span class="req">*</span>
                                                            </td>
                                                            <td width="200" class="field_input">
                                                                <asp:DropDownList ID="ddlregion" runat="server" class="chosen-select" Width="190"
                                                                    onchange="setRegionFromInner();" OnSelectedIndexChanged="ddlregion_SelectedIndexChanged"
                                                                    AutoPostBack="true">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlregion"
                                                                    CssClass="dropup" Display="Dynamic" InitialValue="0" ErrorMessage="Select Region"
                                                                    ForeColor="Red" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                                    TargetControlID="RequiredFieldValidator1" />
                                                            </td>
                                                            <td width="61" class="field_title">
                                                                Voucher Type<span class="req">*</span>
                                                            </td>
                                                            <td width="200" class="field_input">
                                                                <asp:DropDownList ID="ddlVoucher" runat="server" class="chosen-select" Width="150" OnSelectedIndexChanged="ddlVoucher_SelectedIndexChanged"
                                                                    AutoPostBack="true">
                                                                    <asp:ListItem Text="Normal" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="GST" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="GST-Govt." Value="3"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlregion"
                                                                    CssClass="dropup" Display="Dynamic" InitialValue="0" ErrorMessage="Select Voucher"
                                                                    ForeColor="Red" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                                                    TargetControlID="RequiredFieldValidator1" />
                                                            </td>
                                                            <td width="55" class="field_title">
                                                                Voucher Date<span class="req">*</span>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtvoucherdate" runat="server" Width="80" Enabled="true" Font-Bold="true" />
                                                                <asp:ImageButton ID="imgPopuppodate" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                    Enabled="false" runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtenderVoucherDate" PopupButtonID="imgPopuppodate"
                                                                    Enabled="false" runat="server" TargetControlID="txtvoucherdate" CssClass="cal_Theme1"
                                                                    Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>

                                                            </td>
                                                            <td width="50" class="field_title">
                                                                            Place Of Supply&nbsp;&nbsp;
                                                                        </td>
                                                                        <td width="90" class="field_input">
                                                                            <asp:DropDownList ID="ddlPlaceofSupply" Width="155" runat="server" class="chosen-select" AutoPostBack="true"
                                                                                OnSelectedIndexChanged="ddlPlaceofSupply_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                               </td>
                                                        </tr>
                                                    </table>
                                                    <table>
                                                        <div id="btngstonly" runat="server">
                                                            <td width="50" class="field_title">
                                                                            Party Name&nbsp;&nbsp;
                                                                        </td>
                                                                        <td width="70" class="field_input">
                                                                            <asp:DropDownList ID="ddlPartyName" Width="155" runat="server" class="chosen-select" AutoPostBack="true"
                                                                                OnSelectedIndexChanged="ddlPartyName_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                             <td  width="35" class="field_title">
                                                               GSTIN
                                                                  </td>
                                                                    <td class="field_input" width="5px">
                                                               <asp:TextBox ID="txtGstNo" runat="server" Text="" Width="104px" MaxLength="15" AutoCompleteType="Disabled"  Enabled="false" 
                                                                       CssClass="abacus"  placeholder="GSTIN"  onkeypress="return blockSpecialChar(event)"></asp:TextBox>
                                                               </td>
                                                            <td width="35" class="field_title">
                                                               Date
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtInvociedate" runat="server" Width="55" Enabled="false" Font-Bold="true" placeholder="GST ONLY" />
                                                               <asp:ImageButton ID="ImagInvDate" runat="server" ImageUrl="~/images/calendar.png" 
                                                                    Width="24px" Height="24px" ImageAlign="AbsMiddle" />
                                                             <ajaxToolkit:CalendarExtender ID="CalendarExtenderinvociedate" PopupButtonID="ImagInvDate" runat="server"
                                                                TargetControlID="txtInvociedate" Format="dd/MM/yyyy" CssClass="cal_Theme1" />                                                               
                                                            </td>
                                                             <td class="field_title">
                                                               <asp:Label ID="Label24" Text="Invoice No&nbsp;" runat="server"></asp:Label>
                                                                  </td>
                                                                    <td class="field_input" width="5px">
                                                               <asp:TextBox ID="txtInvoiceNo" runat="server" Text="0" Width="95px" MaxLength="20"                                                                    
                                                                       CssClass="abacus"  placeholder="GST ONLY" ></asp:TextBox>
                                                               </td>
                                                             <td class="field_title">
                                                               <asp:Label ID="Label25" Text="TAX(%)&nbsp;" runat="server"></asp:Label>
                                                                  </td>
                                                                    <td class="field_input" width="5px">
                                                               <asp:TextBox ID="txtTaxPercent" runat="server" Text="0" Width="95px" MaxLength="20"                                                                    
                                                                       CssClass="abacus"
                                                                   placeholder="Tax Percent" onkeypress="return isNumberKeyWithDot(event);" ></asp:TextBox>
                                                               </td>
                                                             <td class="field_title">
                                                               <asp:Label ID="Label26" Text="Taxable Value&nbsp;" runat="server"></asp:Label>
                                                                  </td>
                                                                    <td class="field_input" width="5px">
                                                               <asp:TextBox ID="txtTaxValue" runat="server" Text="0" Width="95px" MaxLength="20"                                                                   
                                                                       CssClass="abacus"  placeholder="Taxable" onkeypress="return isNumberKeyWithDot(event);" ></asp:TextBox>
                                                               </td>
                                                             <td class="field_title">
                                                               <asp:Label ID="Label27" Text="HSN&nbsp;" runat="server"></asp:Label>
                                                                  </td>
                                                                    <td class="field_input" width="5px">
                                                               <asp:TextBox ID="txtHsn" runat="server" Text="" Width="95px" MaxLength="20"                                                                     
                                                                       CssClass="abacus"  placeholder="HSN"  onkeypress="return blockSpecialChar(event)"></asp:TextBox>
                                                               </td>
                                                            <td>

                                                        <asp:Button ID="btnAddGstDetails" OnClick="btnAddGstDetails_Click" runat="server" CssClass="btn btn-primary"
                                                            Text="Add" />

                                                    </td>
                                                            </div>
                                                    </table>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                       
                                                        <tr>
                                                        <td>
                                                <fieldset>
                                                    <legend>Gst Details</legend>
                                                    <div style="overflow: hidden; width: 100%;height: 10%;" id="DivHeaderRow1">
                                                    </div>
                                                    <div style="overflow: scroll; height:10%; width: 100%" onscroll="OnScrollDiv(this)" id="DivMainContent1">

                                                        <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                            <asp:GridView ID="grdGStDetails" runat="server" Width="100%" CssClass="reportgrid"
                                                                AutoGenerateColumns="true" ShowFooter="true" AlternatingRowStyle-BackColor="AliceBlue"
                                                                EmptyDataText="No Records Available">
                                                                <Columns>
                                                                     <asp:TemplateField ItemStyle-Width="5" ItemStyle-HorizontalAlign="Center">
                                                                               <ItemTemplate>
                                                                                    <asp:Button ID="btngstDetailsDelete" runat="server" Text="" OnClick="btngstDetailsDelete_Click"
                                                                                    ToolTip="Details Delete" CssClass="action-icons c-delete" CausesValidation="false" 
                                                                                   OnClientClick="return confirm('Are you sure you want to delete this record?');" />
                                                                             </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </table>
                                                    </div>
                                                </fieldset>
                                                           </td>
                                                            </tr>
                                                    </table>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                        <tr id="innertr" runat="server">
                                                            <td>
                                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                                    <tr>
                                                                        <td colspan="4">
                                                                            <div class="gridcontent-short">
                                                                                <ul>
                                                                                    <li class="left3">
                                                                                        <fieldset>
                                                                                            <legend>Debit</legend>
                                                                                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                                                                                <tr id="traccountinfodebit" runat="server">
                                                                                                    <td align="left" class="field_title">
                                                                                                        <asp:Label ID="Label3" Text="Account&nbsp;&nbsp;" runat="server"></asp:Label><span
                                                                                                            class="req">*</span>
                                                                                                    </td>
                                                                                                    <td align="left" class="field_input">
                                                                                                        <asp:DropDownList ID="ddlAccTypeDr" Width="260" runat="server" class="chosen-select">
                                                                                                        </asp:DropDownList>
                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlAccTypeDr"
                                                                                                            Display="Dynamic" ErrorMessage="Required" InitialValue="0" ForeColor="red" ValidationGroup="debitadd">*</asp:RequiredFieldValidator>
                                                                                                    </td>
                                                                                                    <td class="field_title">
                                                                                                        <asp:Label ID="Label4" Text="Amount&nbsp;&nbsp;" runat="server"></asp:Label><span
                                                                                                            class="req">*</span>
                                                                                                    </td>
                                                                                                    <td class="field_input" width="20%">
                                                                                                        <asp:TextBox ID="txtAmtDr" runat="server" Text="" Width="80%" MaxLength="14" onkeypress="return isNumberKeyWithDot(event);"
                                                                                                            AutoCompleteType="Disabled" onfocus="disableautocompletion(this.id);" OnTextChanged="txtAmtDr_TextChanged"
                                                                                                            CssClass="ValidationRequired" AutoPostBack="true"></asp:TextBox>
                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorLoanAmt" runat="server" ControlToValidate="txtAmtDr"
                                                                                                            CssClass="twitterStyleTextbox" Display="Dynamic" ErrorMessage="*" ForeColor="Red"
                                                                                                            ValidationGroup="debitadd"></asp:RequiredFieldValidator>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr id="trdebitchequeno" runat="server">
                                                                                                    <td align="left" class="field_input" colspan="4">
                                                                                                        <table>
                                                                                                            <tr>
                                                                                                                <td valign="top">
                                                                                                                    <asp:RadioButtonList ID="rdbdebitpaymenttype" runat="server" AutoPostBack="true"
                                                                                                                        class="innerfield_title" OnSelectedIndexChanged="rdbdebitpaymenttype_SelectedIndexChanged"
                                                                                                                        RepeatDirection="Horizontal">
                                                                                                                    </asp:RadioButtonList>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    &nbsp;&nbsp;
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txtdebitchequeno" runat="server" placeholder="Enter No." Width="100"
                                                                                                                        MaxLength="17"></asp:TextBox>
                                                                                                                    <span class="label_intro">
                                                                                                                        <asp:Label ID="lbldebitpaymentno" align="right" runat="server" Text="No"></asp:Label>
                                                                                                                    </span>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    &nbsp;&nbsp;
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txtdebitchequedate" runat="server" Width="60" Enabled="false" Font-Bold="true" />
                                                                                                                    <asp:ImageButton ID="ImageButton1" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                                                                        runat="server" Height="24" />
                                                                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtenderDrChqDate" PopupButtonID="ImageButton1"
                                                                                                                        runat="server" TargetControlID="txtdebitchequedate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                                                                    </ajaxToolkit:CalendarExtender>
                                                                                                                    <span class="label_intro">
                                                                                                                        <asp:Label ID="lbldebitpaymentdate" align="right" runat="server" Text="Date"></asp:Label>
                                                                                                                    </span>
                                                                                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtdebitchequedate"
                                                                                                            ValidationGroup="debitadd" SetFocusOnError="true" ErrorMessage="Date is required!"
                                                                                                            Display="None"></asp:RequiredFieldValidator>
                                                                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
                                                                                                            TargetControlID="RequiredFieldValidator7" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                                                            WarningIconImageUrl="../images/050.png">
                                                                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtdebitchequeno"
                                                                                                            CssClass="twitterStyleTextbox" Display="Dynamic" ErrorMessage="Enter Cheque No"
                                                                                                            ForeColor="Red" ValidationGroup="debitadd">*</asp:RequiredFieldValidator>--%>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr id="trdebitbank" runat="server">
                                                                                                    <td align="left" class="field_title">
                                                                                                        <asp:Label ID="Label1" Text="Bank&nbsp;Name&nbsp;&nbsp;" runat="server"></asp:Label><span
                                                                                                            class="req">*</span>
                                                                                                    </td>
                                                                                                    <td align="left" class="field_input" colspan="3">
                                                                                                        <asp:DropDownList ID="ddldebitbankname" Width="300" runat="server" class="chosen-select">
                                                                                                        </asp:DropDownList>
                                                                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddldebitbankname"
                                                                                                            CssClass="dropup" Display="Dynamic" InitialValue="0" ErrorMessage="Select Bank"
                                                                                                            ForeColor="Red" ValidationGroup="debitadd">*</asp:RequiredFieldValidator>
                                                                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
                                                                                                            TargetControlID="RequiredFieldValidator6" />--%>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr id="trdebitbtn" runat="server">
                                                                                                    <td colspan="2" class="field_input" style="padding-left: 10px;">
                                                                                                        <asp:TextBox ID="txtdebitremarks" runat="server" TextMode="MultiLine" Height="30"
                                                                                                            placeholder="Remarks if any"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td align="left" class="field_input" id="tddebitsideoutstanding" runat="server">
                                                                                                        <%--<p style="margin:0px 0px; padding: 0px 0px;" class="smallpopup" id="tooltip1"><a href="#">OutStanding<span id="lbldebitsideoutstanding" runat="server">0</span></a></p>--%>
                                                                                                        <asp:Label ID="Label16" runat="server" Text="Balance"></asp:Label>&nbsp;&nbsp;&nbsp;
                                                                                                        <span>
                                                                                                            <asp:Label ID="lbldebitoutstanding" runat="server" Text="0.00"></asp:Label></span>
                                                                                                    </td>
                                                                                                    <td align="left" class="field_title" colspan="2">
                                                                                                        <div class="btn_24_blue">
                                                                                                            <span class="icon add_co"></span>
                                                                                                            <asp:Button ID="btndebitadd" runat="server" Text="Add" ValidationGroup="debitadd"
                                                                                                                CssClass="btn_link" OnClick="btndebitadd_Click" AccessKey="B" />
                                                                                                        </div>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                            <div class="gridcontent-inner">
                                                                                                <asp:GridView ID="gvdebit" EmptyDataText="There are no debit records available."
                                                                                                    CssClass="reportgrid" runat="server" AutoGenerateColumns="False" Width="100%">
                                                                                                    <Columns>
                                                                                                        <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5">
                                                                                                            <ItemTemplate>
                                                                                                                <nav style="position: relative; text-align: left;"><span class="badge black"><%# Container.DataItemIndex + 1 %></span></nav>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="ACCOUNT">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Button ID="btndebitgrdcostcenter" runat="server" Text='<%# Bind("IsCostCenter") %>'
                                                                                                                    Font-Bold="true" ForeColor="Red" OnClick="btndebitgrdcostcenter_Click" ToolTip='<%# string.Format("CostCenter Exists : {0}\n", Eval("IsCostCenter"))%>'
                                                                                                                    CssClass="h_icon macos" CausesValidation="false" Height="14" />
                                                                                                                <asp:Label ID="lbldrledgername" runat="server" Text='<%# Bind("LedgerName") %>' value='<%# Eval("LedgerName") %>'
                                                                                                                    ToolTip='<%# string.Format("Ledger : {0}\nAmount : {1}\nCostCenter Exists : {2}\nRemarks : {3}\n", Eval("LedgerName"), Eval("Amount"), Eval("IsCostCenter"), Eval("Remarks"))%>' />
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="DR" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:TextBox ID="txtdramount" runat="server" Text='<%# Bind("Amount") %>' value='<%# Eval("Amount") %>'
                                                                                                                    onkeypress="return isNumberKeyWithDot(event);" Style="text-align: right; width: 80px;"
                                                                                                                    onkeyup="toggleclsdrTextSelection();" MaxLength="14" />
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="GUID" Visible="false">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="lbldrguid" runat="server" Text='<%# Bind("GUID") %>' value='<%# Eval("GUID") %>' />
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:BoundField DataField="BankName" HeaderText="BankName" Visible="false" />
                                                                                                        <asp:BoundField DataField="PAYMENTTYPEID" HeaderText="TYPEID" Visible="false" />
                                                                                                        <asp:BoundField DataField="PAYMENTTYPENAME" HeaderText="TYPE" Visible="false" />
                                                                                                        <asp:BoundField DataField="ChequeNo" HeaderText="NO" Visible="false" />
                                                                                                        <asp:BoundField DataField="ChequeDate" HeaderText="DATE" Visible="false" />
                                                                                                        <asp:TemplateField HeaderText="INVOICE" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Button ID="btndebitgrdinvoice" runat="server" Text="" OnClick="btndebitgrdinvoice_Click"
                                                                                                                    ToolTip="Invoice Taging" CssClass="h_icon zoom_sl" CausesValidation="false" />
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="LedgerId" Visible="false">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="lbldrLedgerId" runat="server" Text='<%# Bind("LedgerId") %>' value='<%# Eval("LedgerId") %>' />
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="ChequeRealisedNo" Visible="false">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="lblRealisedno" runat="server" Text='<%# Bind("ChequeRealisedNo") %>'
                                                                                                                    value='<%# Eval("ChequeRealisedNo") %>' />
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="ChequeRealisedDate" Visible="false">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="lblRealiseddate" runat="server" Text='<%# Bind("ChequeRealisedDate") %>'
                                                                                                                    value='<%# Eval("ChequeRealisedDate") %>' />
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="INFO">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Button ID="btndebitgrdcheckRealised" runat="server" Text="" OnClick="btndebitgrdcheckRealised_Click"
                                                                                                                    ToolTip="CHQ/NEFT Release Details" CssClass="h_icon youtube" CausesValidation="false" />
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField ItemStyle-Width="5" ItemStyle-HorizontalAlign="Center">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Button ID="btndebitgrddelete" runat="server" Text="" OnClick="btndebitgrddelete_Click"
                                                                                                                    ToolTip="Ledger Delete" CssClass="action-icons c-delete" CausesValidation="false" AccessKey="H"
                                                                                                                    OnClientClick="return confirm('Are you sure you want to delete this record?');" />
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                    </Columns>
                                                                                                </asp:GridView>
                                                                                            </div>
                                                                                            <table id="tabledebittotalamount" runat="server" width="100%" border="0" cellspacing="0"
                                                                                                cellpadding="0" class="form_container_td">
                                                                                                <tr id="tr6" runat="server" style="background-color: #e3ede3;">
                                                                                                    <td align="right" class="field_title">
                                                                                                        <asp:Label ID="Label10" Text="Total Debit&nbsp;&nbsp;" runat="server"></asp:Label>
                                                                                                        <asp:TextBox ID="txtdebittotalamount" runat="server" Text="0" Width="16%" Style="text-align: right;"
                                                                                                            Font-Bold="True" Enabled="false"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td align="right" class="field_title" width="15">
                                                                                                        <asp:Button ID="btndrrecalculation" runat="server" CausesValidation="false" CssClass="h_icon arrow_refresh_co"
                                                                                                            OnClick="btndrrecalculation_Click" ToolTip="Debit Recalculation" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </fieldset>
                                                                                    </li>
                                                                                    <li class="right3">
                                                                                        <fieldset>
                                                                                            <legend>Credit</legend>
                                                                                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                                                                                <tr id="traccountinfocredit" runat="server">
                                                                                                    <td align="left" class="field_title">
                                                                                                        <asp:Label ID="Label5" Text="Account&nbsp;&nbsp;" runat="server"></asp:Label><span
                                                                                                            class="req">*</span>
                                                                                                    </td>
                                                                                                    <td align="left" class="field_input">
                                                                                                        <asp:DropDownList ID="ddlAcctypeCr" Width="260" runat="server" class="chosen-select">
                                                                                                        </asp:DropDownList>
                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlAcctypeCr"
                                                                                                            Display="Dynamic" ErrorMessage="*" InitialValue="0" ForeColor="red" ValidationGroup="creditadd">*</asp:RequiredFieldValidator>
                                                                                                    </td>
                                                                                                    <td class="field_title">
                                                                                                        <asp:Label ID="Label6" Text="Amount&nbsp;&nbsp;" runat="server"></asp:Label><span
                                                                                                            class="req">*</span>
                                                                                                    </td>
                                                                                                    <td class="field_input" width="20%">
                                                                                                        <asp:TextBox ID="txtAmntCr" runat="server" Text="" Width="80%" MaxLength="14" onkeypress="return isNumberKeyWithDot(event);"
                                                                                                            AutoCompleteType="Disabled" onfocus="disableautocompletion(this.id);" OnTextChanged="txtAmntCr_TextChanged"
                                                                                                            AutoPostBack="true"></asp:TextBox>
                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtAmntCr"
                                                                                                            CssClass="twitterStyleTextbox" Display="Dynamic" ErrorMessage="*" ForeColor="Red"
                                                                                                            ValidationGroup="creditadd"></asp:RequiredFieldValidator>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr id="trcreditchequeno" runat="server">
                                                                                                    <td align="left" class="field_input" colspan="4">
                                                                                                        <table>
                                                                                                            <tr>
                                                                                                                <td valign="top">
                                                                                                                    <asp:RadioButtonList ID="rdbcreditpaymenttype" runat="server" AutoPostBack="true"
                                                                                                                        class="innerfield_title" OnSelectedIndexChanged="rdbcreditpaymenttype_SelectedIndexChanged"
                                                                                                                        RepeatDirection="Horizontal">
                                                                                                                    </asp:RadioButtonList>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    &nbsp;&nbsp;&nbsp;
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txtcreditchequeno" runat="server" placeholder="Enter No" MaxLength="17"
                                                                                                                        Width="100"></asp:TextBox>
                                                                                                                    <span class="label_intro">
                                                                                                                        <asp:Label ID="lblcreditpaymentno" align="right" runat="server" Text="No"></asp:Label></span>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    &nbsp;&nbsp;
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txtcreditchequedate" runat="server" Width="60" Enabled="false" Font-Bold="true" />
                                                                                                                    <asp:ImageButton ID="ImageButton2" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                                                                        runat="server" Height="24" />
                                                                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtenderCrChqDate" PopupButtonID="ImageButton2"
                                                                                                                        runat="server" TargetControlID="txtcreditchequedate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                                                                    </ajaxToolkit:CalendarExtender>
                                                                                                                    <span class="label_intro">
                                                                                                                        <asp:Label ID="lblcreditpaymentdate" align="right" runat="server" Text="Date"></asp:Label></span>
                                                                                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtcreditchequedate"
                                                                                                            ValidationGroup="creditadd" SetFocusOnError="true" ErrorMessage="Date is required!"
                                                                                                            Display="None"></asp:RequiredFieldValidator>
                                                                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server"
                                                                                                            TargetControlID="RequiredFieldValidator9" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                                                            WarningIconImageUrl="../images/050.png">
                                                                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtcreditchequeno"
                                                                                                            CssClass="twitterStyleTextbox" Display="Dynamic" ErrorMessage="Enter Cheque No"
                                                                                                            ForeColor="Red" ValidationGroup="creditadd">*</asp:RequiredFieldValidator>--%>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr id="trcreditbank" runat="server">
                                                                                                    <td align="left" class="field_title">
                                                                                                        <asp:Label ID="Label7" Text="Bank Name" runat="server"></asp:Label><span class="req">*</span>
                                                                                                    </td>
                                                                                                    <td align="left" class="field_input" colspan="3">
                                                                                                        <asp:DropDownList ID="ddlcreditbankname" Width="250" runat="server" class="chosen-select">
                                                                                                        </asp:DropDownList>
                                                                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlcreditbankname"
                                                                                                            CssClass="dropup" InitialValue="0" ErrorMessage="Select Bank"
                                                                                                            ForeColor="Red" ValidationGroup="creditadd">*</asp:RequiredFieldValidator>
                                                                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server"
                                                                                                            TargetControlID="RequiredFieldValidator8" />--%>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr id="trcreditbtn" runat="server">
                                                                                                    <td colspan="2" class="field_input" style="padding-left: 10px;">
                                                                                                        <asp:TextBox ID="txtcreditremarks" runat="server" TextMode="MultiLine" Height="30"
                                                                                                            placeholder="Remarks if any"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td align="left" class="field_input" id="tdcreditsideoutstanding" runat="server">
                                                                                                        <%--<p style="margin:0px 0px; padding: 0px 0px;" class="smallpopup" id="tooltip2"><a href="#">OutStanding<span id="lblcreditsideoutstanding" runat="server">0</span></a></p>--%>
                                                                                                        <asp:Label ID="Label18" runat="server" Text="Balance"></asp:Label>&nbsp;&nbsp;&nbsp;
                                                                                                        <span>
                                                                                                            <asp:Label ID="lblcreditoutstanding" runat="server" Text="0.00"></asp:Label></span>
                                                                                                    </td>
                                                                                                    <td align="left" class="field_title" colspan="2">
                                                                                                        <div class="btn_24_blue">
                                                                                                            <span class="icon add_co"></span>
                                                                                                            <asp:Button ID="btncreditadd" runat="server" Text="Add" ValidationGroup="creditadd"
                                                                                                                CssClass="btn_link" OnClick="btncreditadd_Click" AccessKey="C" />
                                                                                                        </div>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                            <div class="gridcontent-inner">
                                                                                                <asp:GridView ID="gvcredit" EmptyDataText="There are no credit records available."
                                                                                                    CssClass="reportgrid" runat="server" AutoGenerateColumns="False" Width="100%">
                                                                                                    <Columns>
                                                                                                        <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="5">
                                                                                                            <ItemTemplate>
                                                                                                                <nav style="position: relative; text-align: left;"><span class="badge black"><%# Container.DataItemIndex + 1 %></span></nav>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="ACCOUNT">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Button ID="btncreditgrdcostcenter" runat="server" Text='<%# Bind("IsCostCenter") %>'
                                                                                                                    Font-Bold="true" ForeColor="Red" OnClick="btncreditgrdcostcenter_Click" ToolTip='<%# string.Format("CostCenter Exists : {0}\n", Eval("IsCostCenter"))%>'
                                                                                                                    CssClass="h_icon macos" CausesValidation="false" Height="14" />
                                                                                                                <asp:Label ID="lblcrledgerName" runat="server" Text='<%# Bind("LedgerName") %>' value='<%# Eval("LedgerName") %>'
                                                                                                                    ToolTip='<%# string.Format("Ledger : {0}\nAmount : {1}\nCostCenter Exists : {2}\nRemarks : {3}\n", Eval("LedgerName"), Eval("Amount"), Eval("IsCostCenter"), Eval("Remarks"))%>' />
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="CR" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:TextBox ID="txtcramount" runat="server" Text='<%# Bind("Amount") %>' value='<%# Eval("Amount") %>'
                                                                                                                    onkeypress="return isNumberKeyWithDot(event);" Style="text-align: right; width: 80px;"
                                                                                                                    onkeyup="toggleclscrTextSelection();" MaxLength="14" />
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="GUID" Visible="false">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="lblcrguid" runat="server" Text='<%# Bind("GUID") %>' value='<%# Eval("GUID") %>' />
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:BoundField DataField="BankName" HeaderText="BankName" Visible="false" />
                                                                                                        <asp:BoundField DataField="PAYMENTTYPEID" HeaderText="TYPEID" Visible="false" />
                                                                                                        <asp:BoundField DataField="PAYMENTTYPENAME" HeaderText="TYPE" Visible="false" />
                                                                                                        <asp:BoundField DataField="ChequeNo" HeaderText="NO" Visible="false" />
                                                                                                        <asp:BoundField DataField="ChequeDate" HeaderText="DATE" Visible="false" />
                                                                                                        <asp:TemplateField HeaderText="INVOICE" ItemStyle-HorizontalAlign="Center">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Button ID="btncreditgrdinvoice" runat="server" Text="" OnClick="btncreditgrdinvoice_Click"
                                                                                                                    ToolTip="Invoice Taging" CssClass="h_icon zoom_sl" CausesValidation="false" />
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="LedgerId" Visible="false">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="lblcrLedgerId" runat="server" Text='<%# Bind("LedgerId") %>' value='<%# Eval("LedgerId") %>' />
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField ItemStyle-Width="5">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Button ID="btncreditgrddelete" runat="server" Text="" OnClick="btncreditgrddelete_Click"
                                                                                                                    ToolTip="Ledger Delete" CssClass="action-icons c-delete" CausesValidation="false" AccessKey="G"
                                                                                                                    OnClientClick="return confirm('Are you sure you want to delete this record?');" />
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                    </Columns>
                                                                                                </asp:GridView>
                                                                                            </div>
                                                                                            <table id="tablecredittotalamount" runat="server" width="100%" border="0" cellspacing="0"
                                                                                                cellpadding="0" class="form_container_td">
                                                                                                <tr id="tr3" runat="server" style="background-color: #e3ede3;">
                                                                                                    <td align="right" class="field_title">
                                                                                                        <asp:Label ID="Label9" Text="Total Credit&nbsp;&nbsp;" runat="server"></asp:Label>
                                                                                                        <asp:TextBox ID="txtcredittotalamount" runat="server" Text="0" Width="16%" Enabled="false"
                                                                                                            Style="text-align: right;" Font-Bold="True"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td align="right" class="field_title" width="15">
                                                                                                        <asp:Button ID="btncrrecalculation" runat="server" CausesValidation="false" CssClass="h_icon arrow_refresh_co"
                                                                                                            OnClick="btncrrecalculation_Click" ToolTip="Credit Recalculation" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </fieldset>
                                                                                    </li>
                                                                                </ul>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="trho" runat="server" style="display: none;">
                                                                        <td width="9%" class="field_title">
                                                                            <asp:Label ID="Label21" Text="Transfer To HO" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td colspan="2" class="field_input">
                                                                            <asp:CheckBox ID="chktransferHO" runat="server" Checked="true" />
                                                                        </td>
                                                                    </tr>

                                                                    </table>

                                                                    <%--Start Paste Code--%>
                                                                     <asp:Panel ID="trinvoicedetails" runat="server" >
                                                                          <br />
                                     <div style="background-color: #257daf; border-style: solid; border-color: Yellow;
                                                                            background-position: center; background-repeat: no-repeat; background-size: cover;
                                                                            height: 3%" align="center">
                                     <%--   <div style="float: left;" class="btn_24_blue">
                                            <span class="icon rupee_co"></span>
                                        </div>--%>
                                        <%--<asp:Image ID="Image2" runat="server" ImageAlign="Left" class="icon rupee_co"/>--%>
                                        <asp:Label Font-Bold="True" ID="lblledgername" runat="server" Text="" Font-Size="Large"
                                            ForeColor="White"></asp:Label>
                                        <asp:Label Font-Bold="True" ID="Label14" runat="server" Text="Invoice Details" Font-Size="Large"
                                            ForeColor="White"></asp:Label>
                                        <%--<asp:ImageButton ID="imgrejectionbtn" ImageAlign="Right" ImageUrl="~/images/Cancel.png"
                                            Height="25px" Width="25px" runat="server" OnClick="imgrejectionbtn_click" Style="position: absolute;
                                            top: -15px; right: -15px;" />--%>
                                        <hr />
                                    </div>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" style="padding-left: 8px;">
                                                <fieldset>
                                                    <legend>Invoice Details</legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td"
                                                        style="display: none;">
                                                        <tr>
                                                            <td class="field_title" width="60">
                                                                Account
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtaccountname" runat="server" Width="250" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                      <div class="gridcontent-inner">
                                                     <div style="overflow: hidden; width: 100%;height: 10%;" id="DivHeaderRow">
                                                    </div>
                                                    <div style="overflow: scroll; height:10%; width: 100%" onscroll="OnScrollDiv(this)" id="DivMainContent">
                                                       

                                                            <asp:GridView ID="grd_InvoiceDetails" EmptyDataText="There are no records available."
                                                                CssClass="reportgrid" OnRowDataBound="grd_InvoiceDetails_RowDataBound" runat="server"
                                                                AutoGenerateColumns="False" Width="100%">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="SL" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <nav style="height: 0px; z-index: 2; padding-left: 14px; position: relative; text-align: center;"><span class="badge black"><%# Container.DataItemIndex + 1 %></span></nav>
                                                                            <asp:CheckBox ID="chkinv" runat="server" Text=" " onclick="toggleTypeheaderSelectionchecked(this);" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="LedgerId" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblledgerid" runat="server" Text='<%# Bind("LedgerId") %>' value='<%# Eval("LedgerId") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="LedgerName" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblledgername" runat="server" Text='<%# Bind("LedgerName") %>' value='<%# Eval("LedgerName") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="VoucherType" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblvouchertype" runat="server" Text='<%# Bind("VoucherType") %>' value='<%# Eval("VoucherType") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="BranchID" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblbranchid" runat="server" Text='<%# Bind("BranchID") %>' value='<%# Eval("BranchID") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="InvoiceID" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblinvoiceid" runat="server" Text='<%# Bind("InvoiceID") %>' value='<%# Eval("InvoiceID") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Invoice/Voucher No">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblinvoiceno" runat="server" Text='<%# Bind("InvoiceNo") %>' value='<%# Eval("InvoiceNo") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Invoice/Voucher Date">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblinvoicedate" runat="server" Text='<%# Bind("InvoiceDate") %>' value='<%# Eval("InvoiceDate") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Others Info">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgngno" runat="server" Text='<%# Bind("InvoiceOthers") %>' value='<%# Eval("InvoiceOthers") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Branch">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDepot" runat="server" Text='<%# Bind("InvoiceBranchName") %>' value='<%# Eval("InvoiceBranchName") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Invoice Amt">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblinvoiceamt" runat="server" Text='<%# Bind("InvoiceAmt") %>' value='<%# Eval("InvoiceAmt") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Already Paid Amt">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblalreadyamtpaid" runat="server" Style="text-align: right;" Text='<%# Bind("AlreadyAmtPaid") %>'
                                                                                value='<%# Eval("AlreadyAmtPaid") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                     <%-- D.Mondal 10/10/2018 Start --%>

                                                                      <asp:TemplateField HeaderText="Return Amt">
                                                                      <ItemTemplate>
                                                                              <asp:Label ID="lblReturnAmt" runat="server" Style="text-align: right;" Text='<%# Bind("ReturnAmt") %>'
                                                                                       value='<%# Eval("ReturnAmt") %>' />
                                                                      </ItemTemplate>
                                                                           <ItemStyle CssClass="gvItemCenter" />
                                                                           <HeaderStyle CssClass="gvHeaderCenter" />
                                                                      </asp:TemplateField>
                                                                      <%-- D.Mondal 10/10/2018 End --%>

                                                                    <asp:TemplateField HeaderText="Remaining Amt">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblremainingamtpaid" runat="server" Style="text-align: right;" Text='<%# Bind("RemainingAmtPaid") %>'
                                                                                value='<%# Eval("RemainingAmtPaid") %>' />
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="gvItemCenter" />
                                                                        <HeaderStyle CssClass="gvHeaderCenter" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Currently Paid Amt">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtamtpaid" AutoPostBack="false" runat="server" MaxLength="10" Width="100"
                                                                                onkeypress="return isNumberKeyWithDot(event);" onkeyup="toggleHeaderTextSelection();"
                                                                                Text='<%# Bind("AmtPaid") %>' value='<%# Eval("AmtPaid") %>' Style="text-align: right;" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Type" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbltxntype" runat="server" Text='<%# Bind("Type") %>' value='<%# Eval("Type") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <RowStyle HorizontalAlign="Right" />
                                                            </asp:GridView>
                                                             
                                                        </div>
                                               
                                                        <%--<div id="DivFooterRow" style="overflow:hidden"></div>--%>
</div>                                                   
                                                    <table id="tableinvoicepaidamount" runat="server" width="100%" border="0" cellspacing="0"
                                                        cellpadding="0" class="form_container_td">
                                                        <tr id="tr1" runat="server" align="right">
                                                           <td width="50%">
                                                             &nbsp;
                                                           </td>
                                                       <%--     <td align="right" class="field_title" width="75%">
                                                                <div class="btn_24_blue" id="divbtnpaidamount" runat="server">
                                                                    <span class="icon rupee_co"></span>
                                                                    <asp:Button ID="btnpaidamt" runat="server" Text="Bill Adjust" CssClass="btn_link"
                                                                         OnClientClick="return checkboxstatus();" OnClick="btnpaidamt_Click" CausesValidation="false" />
                                                                </div>
                                                            </td>--%>
                                                           <td align="right" class="field_title" width="15%">
                                                               <div class="btn_24_red" id="divbtnpaidamountclear" runat="server">
                                                                <span class="icon arrow_redo_co"></span>
                                                                 <asp:Button ID="btnpaidamountclear" runat="server" Text="Clear adjust" CssClass="btn_link"
                                                                   OnClick="btninvoiceclear_Click" CausesValidation="false" />
                                                                </div>
                                                            </td>
                                                            <td align="right" class="field_title" width="15%">
                                                            <div class="btn_24_green" id="divbtnpaidamount" runat="server">
                                                             <span class="icon rupee_co"></span>
                                                            <asp:Button ID="btnpaidamt" runat="server" Text="Bill Adjust" CssClass="btn_link"
                                                              OnClientClick="return checkboxstatus();" OnClick="btnpaidamt_Click" CausesValidation="false" />
                                                               </div>
                                                             </td>
                                                            <td align="right">
                                                                <div style="padding-right: 25%;">
                                                                    <asp:Label ID="Label11" Text="Total&nbsp;&nbsp;" runat="server" Font-Bold="true"></asp:Label>
                                                                    <asp:TextBox ID="txtpaidamount" runat="server" Width="100" Text="0" Enabled="false"
                                                                        Font-Bold="True" Style="text-align: right;"></asp:TextBox>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>

                                                                    <%--End Paste Code--%>
                                                              

                                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">

                                                                    <tr style="background-color: #e3ede1;">
                                                                        <td width="9%" align="left" class="field_title">
                                                                            <asp:Label ID="Label17" Text="Narration" runat="server"></asp:Label><span class="req">*</span>
                                                                        </td>
                                                                        <td align="left" class="field_input">
                                                                            <asp:TextBox ID="txtNarration" runat="server" Width="100%" Height="40" TextMode="MultiLine"
                                                                                Font-Bold="true" />
                                                                           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtNarration"
                                                                                SetFocusOnError="true" ErrorMessage="* Enter Narration" ForeColor="red" ValidationGroup="1"></asp:RequiredFieldValidator>--%>
                                                                        </td>

                                                                        
                                                                      <td width="9%" align="left" class="field_title">       
                                                                <asp:Label ID="Label8" runat="server" Text="Rejection Note"></asp:Label>
                                                                          </td>
                                                            
                                                                <td align="left" class="field_input">
                                                                <asp:TextBox ID="txtRejectionNote" runat="server" MaxLength="255" CssClass="x_large"
                                                                    TextMode="MultiLine" placeholder="rejection note....." Width="100%" Height="40"> </asp:TextBox>
                                                                </td>
                                                                        <td align="left" class="field_input" width="310" id="tdcheckernote" runat="server">
                                                                            <asp:TextBox ID="txtCheckerNote" runat="server" TextMode="MultiLine" Enabled="false"
                                                                                CssClass="x_large" Height="40" Font-Bold="true"> </asp:TextBox>
                                                                        </td>
                                                                        <td style="padding-left: 10px;">
                                                                            <div class="btn_24_blue" id="divbtnotherdetails" runat="server">
                                                                                <span class="icon airplane"></span>
                                                                                <asp:Button ID="btnotherdetails" runat="server" Text="Other Details" CssClass="btn_link"
                                                                                    OnClick="btnotherdetails_Click" />
                                                                            </div>
                                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                                            <div class="btn_24_blue" id="btndivsave" runat="server">
                                                                                <span class="icon disk_co"></span>
                                                                                <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="1" CssClass="btn_link" AccessKey="S"
                                                                                    OnClick="btnSave_Click" />
                                                                            </div>
                                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                                         
                                                                            <div class="btn_24_blue" id="divSavewithPrint" runat="server">
                                                                                <span class="icon printer_co"></span>
                                                                                <asp:Button ID="btnSavewithPrint" runat="server" Text="Save with Print" CssClass="btn_link"
                                                                                    OnClick="btnSavewithPrint_Click" ValidationGroup="1" AccessKey="P"/>
                                                                            </div>
                                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                                            <div class="btn_24_green" id="divbtnapprove" runat="server">
                                                                                <span class="icon approve_co"></span>
                                                                                <asp:LinkButton ID="btnApprove" runat="server" Text="Approve" CssClass="btn_link"
                                                                                    CausesValidation="false" OnClick="btnApprove_Click" />
                                                                            </div>
                                                                            &nbsp;&nbsp;&nbsp;&nbsp;

                                                                            
                                                            
                                                                           

                                                                            <div class="btn_24_red" id="divbtnreject" runat="server">
                                                                                <span class="icon reject_co"></span>
                                                                                <asp:LinkButton ID="btnReject" runat="server" Text="Reject" CssClass="btn_link" CausesValidation="false"
                                                                                    OnClick="btnReject_Click" />
                                                                            </div>
                                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                                            <div class="btn_24_blue" id="divbtnclose" runat="server">
                                                                                <span class="icon cross_octagon_co"></span>
                                                                                <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn_link" OnClick="btnClose_Click" AccessKey="X" />
                                                                            </div>
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
                                </asp:Panel>




                                <asp:Panel ID="pnlDisplay" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_title">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="60">
                                                            <asp:Label ID="Label22" runat="server" Text="Region"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="180">
                                                            <asp:DropDownList ID="ddldepot" runat="server" class="chosen-select" Width="220"
                                                                onchange="setRegionFromOuter()">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="90">
                                                            <asp:Label ID="Label12" runat="server" Text="From Date"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="110">
                                                            <asp:TextBox ID="txtsearchfromdate" runat="server" MaxLength="15" Width="70" Enabled="false" 
                                                                placeholder="dd/mm/yyyy" ValidationGroup="datecheckme"></asp:TextBox>
                                                            <asp:ImageButton ID="imgPopupfromdatesearch" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="imgPopupfromdatesearch" 
                                                                runat="server" TargetControlID="txtsearchfromdate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                        <td width="70">
                                                            <asp:Label ID="Label13" runat="server" Text="To Date"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="110">
                                                            <asp:TextBox ID="txtsearchtodate" runat="server" MaxLength="10" Width="70" placeholder="dd/mm/yyyy" 
                                                                ValidationGroup="datecheckme" Enabled="false"></asp:TextBox>
                                                            <asp:ImageButton ID="imgpopuptodatesearch" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtenderSrcToDate" PopupButtonID="imgpopuptodatesearch"
                                                                runat="server" TargetControlID="txtsearchtodate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                            </ajaxToolkit:CalendarExtender>
                                                    
                                                        </td>
                                                        <td width="100">  <%--add by Romaprosad 13092019--%>

                                                         
                                                            <asp:Label ID="Label28" runat="server" Text="Voucher Type"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="Dropdownvouchertype" runat="server" class="chosen-select" Width="200">
                                                                    
                                                                    <asp:ListItem Text="All" Value="0" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="Normal" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="GST" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="GST-Govt" Value="3"></asp:ListItem>
                                                            </asp:DropDownList>



                                                        </td>
                                                        <td>
                                                            <div class="btn_24_blue">
                                                                <span class="icon find_co"></span>
                                                                <asp:Button ID="btnvouchersearch" runat="server" Text="Search" CssClass="btn_link"
                                                                    ValidationGroup="datecheckme" OnClick="btnvouchersearch_Click" />
                                                            </div>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <div style="overflow: hidden; padding-left: 8px; width: 100%;" id="DivHeaderRow_voucher">
                                    </div>
                                    <div style="overflow: scroll; padding-left: 8px; border: 1px solid #ccc;" onscroll="OnScrollDiv_voucher(this)"
                                        id="DivMainContent_voucher">
                                        <asp:GridView ID="gvparentvoucher" EmptyDataText="There are no records available."
                                            CssClass="reportgrid" runat="server" AutoGenerateColumns="False" Width="100%" OnRowDataBound="grvaccentry_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="AccEntryID" ItemStyle-HorizontalAlign="Left" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAccEntryID" runat="server" Text='<%# Eval("AccEntryID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="5">
                                                    <ItemTemplate>
                                                        <nav style="position: relative; text-align: center;"><span class="badge black"><%# Container.DataItemIndex + 1 %></span></nav>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="VoucherTypeName" HeaderText="VOUCHER TYPE" />
                                                <asp:BoundField DataField="VoucherNo" HeaderText="VOUCHER NO" />
                                                <asp:BoundField DataField="Date" HeaderText="DATE" />
                                                <asp:BoundField DataField="BranchName" HeaderText="REGION" />
                                                <asp:BoundField DataField="LedgerName" HeaderText="PARTY" />
                                                <asp:BoundField DataField="Amount" HeaderText="AMOUNT" ItemStyle-HorizontalAlign="Right" />
                                                <asp:TemplateField HeaderText="APPROVED" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVoucherApproved" runat="server" Text='<%# Eval("VoucherApproved") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DAYEND" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVoucherDayEnd" runat="server" Text='<%# Eval("DAYENDTAG") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="5" HeaderText="PRINT" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" class="filter_btn printer_co"
                                                            CausesValidation="false" OnClientClick="ShowConfirmBox(this,'Are you sure you want to print this voucher?'); return false;" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="5" HeaderText="ALL" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnAllPrint" runat="server" Text="Print" OnClick="btnAllPrint_Click" class="filter_btn printer_co"
                                                            CausesValidation="false" OnClientClick="ShowConfirmBox(this,'Are you sure you want to print invoice print for this voucher?'); return false;" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="5" HeaderText="EDIT" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btngrdedit" runat="server" Text="Edit Voucher" OnClick="btngrdedit_Click"
                                                            class="action-icons c-edit" CausesValidation="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="5" HeaderText="DEL" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btngrdvoucherdelete" runat="server" Text="Delete Voucher" OnClick="btngrdvoucherdelete_Click"
                                                            class="action-icons c-delete" CausesValidation="false" OnClientClick="ShowConfirmBox(this,'Are you sure you want to delete voucher?'); return false;" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div id="DivFooterRow_voucher" style="overflow: hidden">
                                    </div>
                                    <cc1:ConfirmBox ID="ConfirmBox1" runat="server" />
                                    <asp:HiddenField ID="hdn_accid" runat="server" />
                                    <asp:HiddenField ID="hdn_ledgername" runat="server" />
                                    <asp:HiddenField ID="hdn_creditguid" runat="server" />
                                    <asp:HiddenField ID="hdn_debitguid" runat="server" />
                                    <asp:HiddenField ID="hdn_creditamount" runat="server" />
                                    <asp:HiddenField ID="hdn_debitamount" runat="server" />
                                    <asp:HiddenField ID="hdn_approved" runat="server" />
                                    <asp:HiddenField ID="hdn_dayend" runat="server" />
                                    <asp:HiddenField ID="hdn_accountid" runat="server" />
                                    <asp:HiddenField ID="hdn_accounttype" runat="server" />
                                    <asp:HiddenField ID="hdn_ledgercostcenter" runat="server" />
                                    <asp:HiddenField ID="hdn_RealisedNo" runat="server" />
                                    <asp:HiddenField ID="hdn_RealisedDate" runat="server" />
                                    <asp:HiddenField ID="hdn_transferid" runat="server" />
                                    <asp:HiddenField ID="hdn_paidamount" runat="server" />
                                    <asp:HiddenField ID="hdn_btncostcenterclick" runat="server" />
                                    <asp:HiddenField ID="hdnDevit" runat="server" />
                                    <asp:HiddenField ID="hdnCredit" runat="server" />
                                </asp:Panel>
                               <%--cut--%>
                                <asp:LinkButton ID="lnkFake" runat="server"></asp:LinkButton>
                             <%--   <ajaxToolkit:ModalPopupExtender ID="popup" runat="server" DropShadow="false" PopupControlID="trinvoicedetails"
                                    TargetControlID="lnkFake" BackgroundCssClass="modalBackground">
                                </ajaxToolkit:ModalPopupExtender>--%>
                                <asp:Panel ID="trcostcenterdetails" runat="server" CssClass="modalPopup" Style="display: none;
                                    border-radius: 16px;" Width="90%" Height="85%" BackColor="White" BorderColor="#257daf"
                                    BorderWidth="8px" BorderStyle="Solid">
                                    <div style="background-color: #257daf; border-style: solid; border-color: Yellow;
                                        background-position: center; background-repeat: no-repeat; background-size: cover;
                                        height: 6%" align="center">
                                        <div style="float: left;" class="btn_24_blue">
                                            <span class="icon rupee_co"></span>
                                        </div>
                                        <asp:Label Font-Bold="True" ID="Label15" runat="server" Text="Cost Center Details"
                                            Font-Size="X-Large" ForeColor="Yellow"></asp:Label>
                                        <asp:ImageButton ID="imgrejectioncostcenterbtn" ImageAlign="Right" ImageUrl="~/images/Cancel.png"
                                            Height="25px" Width="25px" runat="server" OnClick="imgrejectioncostcenterbtn_click"
                                            Style="position: absolute; top: -15px; right: -15px;" />
                                        <hr />
                                    </div>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" style="padding-left: 8px;">
                                                <fieldset>
                                                    <legend>Cost Center for Ledger :
                                                        <asp:Label ID="txtcostcenteraccountname" runat="server" ForeColor="Blue"></asp:Label>
                                                        &nbsp;&nbsp;Amount Rs:
                                                        <asp:Label ID="lblcostcenteramountshow" runat="server" ForeColor="Blue"></asp:Label>/-
                                                    </legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                                        <tr>
                                                            <td class="field_title" width="70">
                                                                Segment<span class="req">*</span>
                                                            </td>
                                                            <td class="field_input" width="200">
                                                                <asp:DropDownList ID="ddlcatagory" Width="190" Height="24" runat="server" ValidationGroup="cost">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlcatagory"
                                                                    InitialValue="0" ErrorMessage="*" ForeColor="Red" ValidationGroup="cost">*</asp:RequiredFieldValidator>
                                                            </td>
                                                            <td class="field_title" width="70">
                                                                Brand
                                                            </td>
                                                            <td class="field_input" width="170">
                                                                <asp:DropDownList ID="ddlbrand" Width="160" Height="24" runat="server" ValidationGroup="cost">
                                                                </asp:DropDownList>
                                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlbrand"
                                                                    InitialValue="0" ErrorMessage="*" ForeColor="Red"  ValidationGroup="cost">*</asp:RequiredFieldValidator>--%>
                                                            </td>
                                                            <td class="field_title" width="70">
                                                                Product
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:DropDownList ID="ddlproduct" Width="235" Height="24" runat="server" ValidationGroup="cost">
                                                                </asp:DropDownList>
                                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlproduct"
                                                                    InitialValue="0" ErrorMessage="*" ForeColor="Red"  ValidationGroup="cost">*</asp:RequiredFieldValidator>--%>
                                                            </td>
                                                            <td class="field_title" width="70">
                                                                Department<span class="req">*</span>
                                                            </td>
                                                            <td class="field_input" colspan="2">
                                                                <asp:DropDownList ID="ddldepartment" Width="170" Height="24" runat="server" ValidationGroup="cost">
                                                                </asp:DropDownList>
                                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddldepartment"
                                                                    InitialValue="0" ErrorMessage="*" ForeColor="Red"  ValidationGroup="cost">*</asp:RequiredFieldValidator>--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field_title" width="90">
                                                                <asp:Label ID="Label111" runat="server" Text="Date Range"></asp:Label>
                                                            </td>
                                                            <td width="150">
                                                                <asp:TextBox ID="txtcostcenterfromdate" runat="server" MaxLength="15" Width="65"
                                                                    Enabled="false" placeholder="dd/mm/yyyy" ValidationGroup="datecheckme"></asp:TextBox>
                                                                <asp:ImageButton ID="ImageButton3" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender4" PopupButtonID="ImageButton3"
                                                                    runat="server" TargetControlID="txtcostcenterfromdate" CssClass="cal_Theme1"
                                                                    Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                                <asp:TextBox ID="txtcostcentertodate" runat="server" MaxLength="10" Width="65" placeholder="dd/mm/yyyy"
                                                                    ValidationGroup="datecheckme" Enabled="false"></asp:TextBox>
                                                                <asp:ImageButton ID="ImageButton4" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtenderCCToDate" PopupButtonID="ImageButton4"
                                                                    runat="server" TargetControlID="txtcostcentertodate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>
                                                            <td class="field_title">
                                                                Branch<span class="req">*</span>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:DropDownList ID="ddlbranch" Width="160" Height="24" runat="server" ValidationGroup="cost">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlbranch"
                                                                    InitialValue="0" ErrorMessage="*" ForeColor="Red" ValidationGroup="cost">*</asp:RequiredFieldValidator>
                                                            </td>
                                                            <td class="field_title">
                                                                Cost Center
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:DropDownList ID="ddlcentername" Width="235" Height="24" runat="server" ValidationGroup="cost">
                                                                </asp:DropDownList>
                                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlcentername"
                                                                    InitialValue="0" ErrorMessage="*"  ForeColor="Red" ValidationGroup="cost">*</asp:RequiredFieldValidator>--%>
                                                            </td>
                                                            <td class="field_title">
                                                                Amount<span class="req">*</span>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtamount" runat="server" Width="80" Enabled="true" MaxLength="20"></asp:TextBox>
                                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ControlToValidate="txtamount" 
                                                                    InitialValue="" ErrorMessage="*Req" ForeColor="Red" ValidationGroup="cost">*</asp:RequiredFieldValidator>--%>
                                                            </td>
                                                            <td align="left" class="field_title">
                                                                <div class="btn_24_blue">
                                                                    <span class="icon add_co"></span>
                                                                    <asp:Button ID="btncostcenteradd" runat="server" Text="Add" CssClass="btn_link" OnClientClick="return amountzero_add();" OnClick="btncostcenteradd_Click"
                                                                        ValidationGroup="cost" CausesValidation="true" />
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <div class="gridcontent-inner" style="overflow: auto; height: 220px;">
                                                        <%--<div style="overflow: hidden; width: 100%;" id="DivHeaderRow">
                                                        </div>
                                                        <div style="overflow: scroll; border: 1px solid #ccc;" onscroll="MakeStaticHeader('<%= grdcostcenter.ClientID %>', 200, '99%' , 30 ,false)"
                                                            id="DivMainContent">--%>
                                                        <asp:GridView ID="grdcostcenter" EmptyDataText="There are no records available."
                                                            DataKeyNames="GUID" CssClass="reportgrid"
                                                            runat="server" AutoGenerateColumns="False" Width="100%" ShowFooter="true">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="SL" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <nav style="position: relative; text-align: center;"><span class="badge black"><%# Container.DataItemIndex + 1 %></span></nav>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="LedgerId" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblledgerid" runat="server" Text='<%# Bind("LedgerId") %>' value='<%# Eval("LedgerId") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="GUID" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCostcenterGUID" runat="server" Text='<%# Bind("GUID") %>' value='<%# Eval("GUID") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="BussinessSegment" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBSID" runat="server" Text='<%# Bind("CostCatagoryID") %>' value='<%# Eval("CostCatagoryID") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Brand" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblbrandid" runat="server" Text='<%# Bind("BrandId") %>' value='<%# Eval("BrandId") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Product" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblProductId" runat="server" Text='<%# Bind("ProductId") %>' value='<%# Eval("ProductId") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Departmentid" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDepartmentid" runat="server" Text='<%# Bind("DepartmentId") %>'
                                                                            value='<%# Eval("DepartmentId") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="BranchID" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBranchID" runat="server" Text='<%# Bind("BranchID") %>' value='<%# Eval("BranchID") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="CostCenterID" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCostCenterID" runat="server" Text='<%# Bind("CostCenterID") %>'
                                                                            value='<%# Eval("CostCenterID") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="COSTCENTER" ItemStyle-Wrap="true" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCostCenterName" runat="server" Text='<%# Bind("CostCenterName") %>'
                                                                            value='<%# Eval("CostCenterName") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="CATAGORY" ItemStyle-Wrap="true" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCostCatagoryName" runat="server" Text='<%# Bind("CostCatagoryName") %>'
                                                                            value='<%# Eval("CostCatagoryName") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="BRAND" ItemStyle-Wrap="true" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblbrandname" runat="server" Text='<%# Bind("BrandName") %>' value='<%# Eval("BrandName") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="PRODUCT" ItemStyle-Wrap="true" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblProductname" runat="server" Text='<%# Bind("ProductName") %>' value='<%# Eval("ProductName") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="DEPARTMENT" ItemStyle-Wrap="true" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDepartmentname" runat="server" Text='<%# Bind("DepartmentName") %>'
                                                                            value='<%# Eval("DepartmentName") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="FROM DATE" ItemStyle-Wrap="true" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblfromdate" runat="server" Text='<%# Bind("FromDate") %>' value='<%# Eval("FromDate") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="TO DATE" ItemStyle-Wrap="true" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbltodate" runat="server" Text='<%# Bind("ToDate") %>' value='<%# Eval("ToDate") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="BRANCH" ItemStyle-Wrap="true" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBranchName" runat="server" Text='<%# Bind("BranchName") %>' value='<%# Eval("BranchName") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="AMOUNT" ItemStyle-Wrap="true" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                                    <ItemTemplate>
                                                                        <%--<asp:Label ID="lblamount" runat="server" Text='<%# Bind("Amount") %>' value='<%# Eval("Amount") %>' />--%>
                                                                        <asp:TextBox ID="txtamount" runat="server" Style="text-align: right;" Text='<%# Bind("Amount") %>'
                                                                            value='<%# Eval("Amount") %>' Width="80" onkeypress="return isNumberKeyWithDot(event);"
                                                                            MaxLength="10" onkeyup="OverAllSharevoucher(this);" onchange="OverAllSharevoucher(this);">
                                                                        </asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblfooteramount" runat="server" Text='<%# Bind("totalamount") %>' value='<%# Eval("totalamount") %>' />
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:Button ID="btncostcenterdelete" runat="server" Text="" CssClass="action-icons c-delete"
                                                                            OnClientClick="return confirm('Are you sure you want to Delete?')" ToolTip="Delete"
                                                                            OnClick="btncostcenterdelete_Click" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <RowStyle HorizontalAlign="Right" />
                                                        </asp:GridView>
                                                    </div>
                                                    <%--<div id="DivFooterRow" style="overflow:hidden"></div>--%>
                                                    <%--</div>--%>
                                                    <table id="table1" runat="server" width="100%" border="0" cellspacing="0" cellpadding="0"
                                                        class="form_container_td">
                                                        <tr id="tr2" runat="server">
                                                            <td align="right" class="field_title">
                                                                <div class="btn_24_blue">
                                                                    <span class="icon disk_co"></span>
                                                                    <asp:Button ID="btncostcentersave" runat="server" Text="Save" CssClass="btn_link"
                                                                        OnClick="btncostcentersave_Click" OnClientClick="return amountzero_save();" CausesValidation="false" />
                                                                </div>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                                <div class="btn_24_blue">
                                                                    <span class="icon cross_octagon_co"></span>
                                                                    <asp:Button ID="btncostcenterncancel" runat="server" Text="Close" CssClass="btn_link"
                                                                        OnClick="btncostcenterncancel_Click" />
                                                                </div>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:LinkButton ID="lnkFakeCost" runat="server"></asp:LinkButton>
                                <ajaxToolkit:ModalPopupExtender ID="Popupcostcenter" runat="server" DropShadow="false"
                                    PopupControlID="trcostcenterdetails" TargetControlID="lnkFakeCost" BackgroundCssClass="modalBackground">
                                </ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="trtaxdetails" runat="server" CssClass="modalPopup" Style="display: none;
                                    border-radius: 16px;" Width="43%" Height="63%" BackColor="White" BorderColor="#257daf"
                                    BorderWidth="8px" BorderStyle="Solid">
                                    <div style="background-color: #257daf; border-style: solid; border-color: Yellow;
                                        background-position: center; background-repeat: no-repeat; background-size: cover;
                                        height: 8%" align="center">
                                        <div style="float: left;" class="btn_24_blue">
                                            <span class="icon rupee_co"></span>
                                        </div>
                                        <asp:Label Font-Bold="True" ID="Label2" runat="server" Text="Tax Details" Font-Size="X-Large"
                                            ForeColor="White"></asp:Label>
                                        <asp:ImageButton ID="imgrejectiontaxpanel" ImageAlign="Right" ImageUrl="~/images/Cancel.png"
                                            OnClientClick="TaxPopClosed();" UseSubmitBehavior="false" Height="25px" Width="25px"
                                            runat="server" Style="position: absolute; top: -15px; right: -15px;" />
                                        <hr />
                                    </div>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" style="padding-left: 8px; padding-right: 20px;">
                                                <fieldset>
                                                    <legend>TAX DEDUCTION</legend>
                                                    <div class="gridcontent-inner" style="height: 180px; overflow: scroll;">
                                                        <asp:GridView ID="grdapplicabletax" EmptyDataText="There are no records available."
                                                            CssClass="reportgrid" runat="server" AutoGenerateColumns="False" Width="100%"
                                                            ShowFooter="false">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="SL" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <nav style="height: 0px; z-index: 2; padding-left: 20px; position: relative; text-align: center;"><span class="badge black"><%# Container.DataItemIndex + 1 %></span></nav>
                                                                        <asp:CheckBox ID="chktaxid" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="TaxID" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbltaxid" runat="server" Text='<%# Bind("ID") %>' value='<%# Eval("ID") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="NAME">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbltaxname" runat="server" Text='<%# Bind("NAME") %>' value='<%# Eval("NAME") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="PERCENTAGE" ItemStyle-HorizontalAlign="Right">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbltaxpercent" runat="server" Style="text-align: right;" Text='<%# Bind("DEDUCTABLEPERCENT") %>'
                                                                            value='<%# Eval("DEDUCTABLEPERCENT") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="AMOUNT" ItemStyle-Wrap="true" ItemStyle-HorizontalAlign="Right">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtdeductableamount" runat="server" Text='<%# Bind("DEDUCTABLEAMOUNT") %>'
                                                                            AutoPostBack="false" MaxLength="10" Style="text-align: right;" Width="80" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </fieldset>
                                            </td>
                                        </tr>
                                    </table>
                                    
                                    <table id="table4" runat="server" width="100%" border="0" cellspacing="0" cellpadding="0"
                                        class="form_container_td">
                                        <tr id="tr7" runat="server">
                                            <td align="left" class="field_title" style="float: right; padding-right: 15px;">
                                                <div class="btn_24_blue">
                                                    <span class="icon add_co"></span>
                                                    <asp:Button ID="btntaxadd" runat="server" Text="Add" CssClass="btn_link" OnClick="btntaxadd_Click"
                                                        CausesValidation="false" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
                                                    <span class="icon cross_octagon_co"></span>
                                                    <asp:Button ID="btnclosetax" runat="server" Text="Close" CssClass="btn_link" OnClick="btnclosetax_Click" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:LinkButton ID="lnkFakeTax" runat="server"></asp:LinkButton>
                                <ajaxToolkit:ModalPopupExtender ID="PopupTax" runat="server" DropShadow="false" PopupControlID="trtaxdetails"
                                    TargetControlID="lnkFakeTax" BackgroundCssClass="modalBackground">
                                </ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="lightRejectionNote" runat="server" CssClass="modalPopup" Style="display: none;
                                    border-radius: 16px;" Width="50%" Height="48%" BackColor="White" BorderColor="#257daf"
                                    BorderWidth="8px" BorderStyle="Solid">
                                    <div style="background-color: #257daf; border-style: solid; border-color: Yellow;
                                        background-position: center; background-repeat: no-repeat; background-size: cover;
                                        height: 10%" align="center">
                                        <div style="float: left;" class="btn_24_blue">
                                            <span class="icon rupee_co"></span>
                                        </div>
                                        <asp:Label Font-Bold="True" ID="Label19" runat="server" Text="Voucher Rejection Note"
                                            Font-Size="X-Large" ForeColor="Yellow"></asp:Label>
                                        <asp:ImageButton ID="imgvoucherrejectionbtn" ImageAlign="Right" ImageUrl="~/images/Cancel.png"
                                            Height="25px" Width="25px" runat="server" OnClick="imgvoucherrejectionbtn_click" />
                                        <hr />
                                    </div>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input">
                                                <fieldset>
                                                    <legend>Rejection Details</legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                                        <tr>
                                                            
                                                        </tr>
                                                        <tr>
                                                            <td class="field_title">
                                                                &nbsp;
                                                            </td>
                                                            <td class="field_input" style="float: right;">
                                                                <div class="btn_24_blue">
                                                                    <span class="icon disk_co"></span>
                                                                    <asp:Button ID="btnRejectionNoteSubmit" runat="server" Text="Submit" CssClass="btn_link"
                                                                        ValidationGroup="RejectionNote" OnClick="btnRejectionNoteSubmit_Click" />
                                                                </div>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                                <div class="btn_24_blue">
                                                                    <span class="icon cross_octagon_co"></span>
                                                                    <asp:Button ID="btnRejectionCloseLightbox" runat="server" Text="Close" CssClass="btn_link"
                                                                        OnClick="btnRejectionCloseLightbox_Click" />
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:LinkButton ID="LinkButton1" runat="server"></asp:LinkButton>
                                <ajaxToolkit:ModalPopupExtender ID="RejectPopup" runat="server" DropShadow="false"
                                    PopupControlID="lightRejectionNote" TargetControlID="LinkButton1" BackgroundCssClass="modalBackground">
                                </ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlotherdetails" runat="server" CssClass="modalPopup" Style="display: none;
                                    border-radius: 16px;" Width="55%" Height="63%" BackColor="White" BorderColor="#257daf"
                                    BorderWidth="8px" BorderStyle="Solid">
                                    <div style="background-color: #257daf; border-style: solid; border-color: Yellow;
                                        background-position: center; background-repeat: no-repeat; background-size: cover;
                                        height: 8%" align="center">
                                        <div style="float: left;" class="btn_24_blue">
                                            <span class="icon rupee_co"></span>
                                        </div>
                                        <asp:Label Font-Bold="True" ID="Label20" runat="server" Text="Other Details" Font-Size="X-Large"
                                            ForeColor="Yellow"></asp:Label>
                                        <asp:ImageButton ID="imgrejectionotherdetailsbtn" ImageAlign="Right" ImageUrl="~/images/Cancel.png"
                                            Height="25px" Width="25px" runat="server" OnClick="imgrejectionotherdetailsbtn_click"
                                            Style="position: absolute; top: -15px; right: -15px;" />
                                        <hr />
                                    </div>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" style="padding-left: 8px;">
                                                <fieldset>
                                                    <legend>Other Details</legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                                        <tr>
                                                            <td class="field_title" width="80">
                                                                Bill No.
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtbillno" runat="server" MaxLength="25" Width="250" placeholder="Bill No"></asp:TextBox>
                                                            </td>
                                                            <td class="field_title" width="80">
                                                                Bill date
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtbilldate" runat="server" MaxLength="25" Width="65" Enabled="false"
                                                                    placeholder="dd/mm/yyyy"></asp:TextBox>
                                                                <asp:ImageButton ID="ImageButton5" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtenderOthersDate" PopupButtonID="ImageButton5"
                                                                    runat="server" TargetControlID="txtbilldate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field_title">
                                                                GR No.
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtgrno" runat="server" MaxLength="25" Width="250" placeholder="GR No"></asp:TextBox>
                                                            </td>
                                                            <td class="field_title" width="70">
                                                                GR Date
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtgrdate" runat="server" MaxLength="25" Width="65" Enabled="false"
                                                                    placeholder="dd/mm/yyyy"></asp:TextBox>
                                                                <asp:ImageButton ID="ImageButton6" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtenderGRDate" PopupButtonID="ImageButton6"
                                                                    runat="server" TargetControlID="txtgrdate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field_title">
                                                                Vehicle No.
                                                            </td>
                                                            <td class="field_input" colspan="3">
                                                                <asp:TextBox ID="txtvehicleno" runat="server" MaxLength="25" Width="250" placeholder="Vehicle No"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field_title">
                                                                Tranport
                                                            </td>
                                                            <td class="field_input" colspan="3">
                                                                <asp:TextBox ID="txttransport" runat="server" MaxLength="100" Width="450" placeholder="Transport"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field_title">
                                                                Way Bill No.
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtwaybillno" runat="server" MaxLength="25" Width="250" placeholder="WayBill No"></asp:TextBox>
                                                            </td>
                                                            <td class="field_title">
                                                                Way Bill Date
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtwaybilldate" runat="server" MaxLength="25" Width="65" Enabled="false"
                                                                    placeholder="dd/mm/yyyy"></asp:TextBox>
                                                                <asp:ImageButton ID="ImageButton7" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtenderWaybillDate" PopupButtonID="ImageButton7"
                                                                    runat="server" TargetControlID="txtwaybilldate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table id="table3" runat="server" width="100%" border="0" cellspacing="0" cellpadding="0"
                                                        class="form_container_td" align="right">
                                                        <tr id="tr4" runat="server">
                                                            <td align="right" class="field_title" style="padding-right: 50px;">
                                                                <div class="btn_24_blue">
                                                                    <span class="icon disk_co"></span>
                                                                    <asp:Button ID="btnotherdetailssave" runat="server" Text="Save" CssClass="btn_link"
                                                                        OnClick="btnotherdetailssave_Click" CausesValidation="false" />
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:LinkButton ID="lnkFakeOthersDetails" runat="server"></asp:LinkButton>
                                <ajaxToolkit:ModalPopupExtender ID="Popupotherdetails" runat="server" DropShadow="false"
                                    PopupControlID="pnlotherdetails" TargetControlID="lnkFakeOthersDetails" BackgroundCssClass="modalBackground">
                                </ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlChequeRealised" runat="server" CssClass="modalPopup" Style="display: none;
                                    border-radius: 16px;" Width="47%" Height="25%" BackColor="White" BorderColor="#257daf"
                                    BorderWidth="8px" BorderStyle="Solid">
                                    <div style="background-color: #257daf; border-style: solid; border-color: Yellow;
                                        background-position: center; background-repeat: no-repeat; background-size: cover;
                                        height: 20%" align="center">
                                        <div style="float: left;" class="btn_24_blue">
                                            <span class="icon rupee_co"></span>
                                        </div>
                                        <asp:Label Font-Bold="True" ID="Label23" runat="server" Text="Release Details" Font-Size="X-Large"
                                            ForeColor="Yellow"></asp:Label>
                                        <asp:ImageButton ID="imgcloserealished" ImageAlign="Right" ImageUrl="~/images/Cancel.png"
                                            Height="25px" Width="25px" runat="server" OnClick="imgcloserealished_click" Style="position: absolute;
                                            top: -15px; right: -15px;" />
                                        <hr />
                                    </div>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" style="padding-left: 8px;">
                                                <fieldset>
                                                    <legend>Release Details Info</legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                                        <tr>
                                                            <td class="field_title" width="80">
                                                                Released No.
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtRealisedno" runat="server" MaxLength="25" Width="100%" placeholder="Realised No."></asp:TextBox>
                                                            </td>
                                                            <td class="field_title" width="30">
                                                                date
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtRealisedDate" runat="server" MaxLength="25" Width="65" Enabled="false"
                                                                    placeholder="dd/mm/yyyy"></asp:TextBox>
                                                                <asp:ImageButton ID="ImageButton10" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtenderRealisedDate" PopupButtonID="ImageButton10"
                                                                    runat="server" TargetControlID="txtRealisedDate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>
                                                            <td align="right" class="field_title">
                                                                <div class="btn_24_blue">
                                                                    <span class="icon disk_co"></span>
                                                                    <asp:Button ID="btnChequeRealised" runat="server" Text="Save" CssClass="btn_link"
                                                                        OnClick="btnChequeRealised_Click" CausesValidation="false" />
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:LinkButton ID="lnkFakeRealised" runat="server"></asp:LinkButton>
                                <ajaxToolkit:ModalPopupExtender ID="popupChequeRealised" runat="server" DropShadow="false"
                                    PopupControlID="pnlChequeRealised" TargetControlID="lnkFakeRealised" BackgroundCssClass="modalBackground">
                                </ajaxToolkit:ModalPopupExtender>
                                <style type="text/css">
                                    .modalBackground
                                    {
                                        top: 0px;
                                        left: 0px;
                                        background-color: rgba(0,0,0,0.4); /*background-image: url(../images/img-raty/back-slash.png);
                                        background-image: url(../images/img-raty/images_slash.png);*/
                                        position: absolute;
                                        filter: alpha(opacity=60);
                                        -moz-opacity: 0.5;
                                        opacity: 0.5;
                                    }
                                </style>
                                <div id="Div1" class="white_content" runat="server">
                                    <table width="50%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td width="10%" class="field_title">
                                                <label>
                                                    Note</label>&nbsp;<span class="req">*</span>
                                            </td>
                                            <td width="20%" class="field_input">
                                                <asp:TextBox ID="TextBox1" runat="server" CssClass="x_large" TextMode="MultiLine"
                                                    Style="height: 119px" MaxLength="255"> </asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtRejectionNote"
                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="RejectionNote"
                                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title">
                                                &nbsp;
                                            </td>
                                            <td class="field_input">
                                                <asp:Button ID="Button1" runat="server" Text="Submit" CssClass="btn_small btn_blue"
                                                    ValidationGroup="RejectionNote" OnClick="btnRejectionNoteSubmit_Click" />&nbsp;&nbsp;<asp:Button
                                                        ID="Button2" runat="server" Text="Close" CssClass="btn_small btn_blue" OnClick="btnRejectionCloseLightbox_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="fadeRejectionNote" class="black_overlay" runat="server">
                                </div>
                            </div>
                        </div>
                    </div>
                    <cc1:MessageBox ID="MessageBox1" runat="server" />
                    <span class="clear"></span>
                </div>
                <span class="clear"></span>
                <script type="text/javascript">
                    function disableautocompletion(id) {
                        var TextBoxControl = document.getElementById(id);
                        TextBoxControl.setAttribute("autocomplete", "off");
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
                            DivHR.style.width = '97%';
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
                </script>
                <script language="javascript" type="text/javascript">
                    function MakeStaticHeader_voucher(gridId, height, width, headerHeight, isFooter) {
                        var tbl = document.getElementById(gridId);
                        if (tbl) {
                            var DivHR_inv = document.getElementById('DivHeaderRow_voucher');
                            var DivMC_inv = document.getElementById('DivMainContent_voucher');
                            var DivFR_inv = document.getElementById('DivFooterRow_voucher');

                            //*** Set divheaderRow Properties ****
                            DivHR_inv.style.height = headerHeight + 'px';
                            //DivHR_inv.style.width = (parseInt(width) - 16) + 'px';
                            DivHR_inv.style.width = '98%';
                            DivHR_inv.style.position = 'relative';
                            DivHR_inv.style.top = '0px';
                            DivHR_inv.style.zIndex = '10';
                            DivHR_inv.style.verticalAlign = 'top';

                            //*** Set divMainContent Properties ****
                            DivMC_inv.style.width = width + 'px';
                            DivMC_inv.style.height = height + 'px';
                            DivMC_inv.style.position = 'relative';
                            DivMC_inv.style.top = -headerHeight + 'px';
                            DivMC_inv.style.zIndex = '1';

                            //*** Set divFooterRow Properties ****
                            DivFR_inv.style.width = (parseInt(width) - 16) + 'px';
                            DivFR_inv.style.position = 'relative';
                            DivFR_inv.style.top = -headerHeight + 'px';
                            DivFR_inv.style.verticalAlign = 'top';
                            DivFR_inv.style.paddingtop = '2px';

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
                                DivFR_inv.appendChild(tblfr);
                            }
                            //****Copy Header in divHeaderRow****
                            DivHR_inv.appendChild(tbl.cloneNode(true));
                        }
                    }

                    function OnScrollDiv_voucher(Scrollablediv) {
                        document.getElementById('DivHeaderRow_voucher').scrollLeft = Scrollablediv.scrollLeft;
                        document.getElementById('DivFooterRow_voucher').scrollLeft = Scrollablediv.scrollLeft;
                    }
                </script>
                <script type="text/javascript">

                    function isNumberKeyWithDot(evt) {
                        var charCode = (evt.which) ? evt.which : event.keyCode;
                        if ((charCode < 48 || charCode > 57) && (charCode != 8) && (charCode != 46) && (charCode != 45))
                            return false;

                        return true;
                    }

                    function isNumberKey(evt) {
                        var charCode = (evt.which) ? evt.which : event.keyCode;
                        if (charCode < 48 || charCode > 57)
                            return false;

                        return true;
                    }

                    function setRegionFromOuter() {
                        var stroutter = document.getElementById("<%=ddldepot.ClientID %>").value;
                        document.getElementById("<%=ddlregion.ClientID %>").value = stroutter;
                        return false;
                    }

                    function setRegionFromInner() {
                        var strinner = document.getElementById("<%=ddlregion.ClientID %>").value;
                        document.getElementById("<%=ddldepot.ClientID %>").value = strinner;
                        document.getElementById("<%=ddlbranch.ClientID %>").value = strinner;
                        return false;
                    }

                    function TaxPopClosed() {
                        document.getElementById('<%= trtaxdetails.ClientID %>').style.display = 'none';
                        document.getElementById('<%= pnlAdd.ClientID %>').style.display = '';
                        return false;
                    }

                    function toggleclsdrTextSelection() {
                        var totaldramount = 0;
                        var grid = document.getElementById('ContentPlaceHolder1_gvdebit');
                        for (var ii = 0; ii < grid.rows.length - 1; ii++) {
                            var idtxtdramount = "ContentPlaceHolder1_gvdebit_txtdramount_" + ii;
                            var dramount = document.getElementById(idtxtdramount).value;
                            if (isNaN(dramount)) {
                                dramount = 0;
                            }
                            totaldramount = parseFloat(totaldramount) + parseFloat(dramount);
                        }
                        var idtxtnetfreepcs1 = "ContentPlaceHolder1_txtdebittotalamount";
                        document.getElementById(idtxtnetfreepcs1).value = totaldramount;
                    }

                    function toggleclscrTextSelection() {
                        var totaldramount = 0;
                        var grid = document.getElementById('ContentPlaceHolder1_gvcredit');
                        for (var ii = 0; ii < grid.rows.length - 1; ii++) {
                            var idtxtdramount = "ContentPlaceHolder1_gvcredit_txtcramount_" + ii;
                            var dramount = document.getElementById(idtxtdramount).value;
                            if (isNaN(dramount)) {
                                dramount = 0;
                            }
                            totaldramount = parseFloat(totaldramount) + parseFloat(dramount);
                        }
                        var idtxtnetfreepcs1 = "ContentPlaceHolder1_txtcredittotalamount";
                        document.getElementById(idtxtnetfreepcs1).value = totaldramount;
                    }
                </script>
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
                  <%--  $(document).ready(function () {

                        BlockUI("<%=trinvoicedetails.ClientID %>");
                        $.blockUI.defaults.css = {};
                    });--%>
                    function Hidepopup() {
                        $find("popup").hide();
                        return false;
                    }

                    $(document).ready(function () {

                        BlockUI("<%=trcostcenterdetails.ClientID %>");
                        $.blockUI.defaults.css = {};
                    });
                    function Hidepopup() {
                        $find("Popupcostcenter").hide();
                        return false;
                    }

                    $(document).ready(function () {

                        BlockUI("<%=trtaxdetails.ClientID %>");
                        $.blockUI.defaults.css = {};
                    });
                    function Hidepopup() {
                        $find("PopupTax").hide();
                        return false;
                    }

                    $(document).ready(function () {

                        BlockUI("<%=pnlotherdetails.ClientID %>");
                        $.blockUI.defaults.css = {};
                    });
                    function Hidepopup() {
                        $find("Popupotherdetails").hide();
                        return false;
                    }
                    $(document).ready(function () {

                        BlockUI("<%=pnlChequeRealised.ClientID %>");
                        $.blockUI.defaults.css = {};
                    });
                    function Hidepopup() {
                        $find("popupChequeRealised").hide();
                        return false;
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
                <script type="text/javascript" language="javascript">
                    function toggleTypeheaderSelectionchecked(checkbox) {
                        var holdid = checkbox.id;
                        var grid = document.getElementById('ContentPlaceHolder1_grd_InvoiceDetails');
                        var totalpaid = document.getElementById('ContentPlaceHolder1_txtpaidamount');
                        var total = 0;
                        var pos = holdid.substr(46, 5).trim();
                        var idtxt = "ContentPlaceHolder1_grd_InvoiceDetails_txtamtpaid_" + pos;
                        var idlbl = "ContentPlaceHolder1_grd_InvoiceDetails_lblremainingamtpaid_" + pos;

                        if (document.getElementById(holdid).checked == true) {
                            var remainpaid = document.getElementById(idlbl).innerHTML;
                            document.getElementById(idtxt).value = remainpaid;
                        }
                        else {
                            document.getElementById(idtxt).value = 0.00;
                        }

                        for (var ii = 0; ii < grid.rows.length - 1; ii++) {
                            var idtxt1 = "ContentPlaceHolder1_grd_InvoiceDetails_txtamtpaid_" + ii;
                            var remainpaid1 = document.getElementById(idtxt1).value;
                            if (isNaN(remainpaid1)) {
                                document.getElementById(idtxt1).value = 0;
                                remainpaid1 = 0;
                            }
                            total = parseFloat(total) + parseFloat(remainpaid1);
                        }
                        if (isNaN(total)) {
                            total = 0;
                        }
                        totalpaid.value = total;
                    }

                    function toggleHeaderTextSelection() {
                        var grid = document.getElementById('ContentPlaceHolder1_grd_InvoiceDetails');
                        var totalpaid = document.getElementById('ContentPlaceHolder1_txtpaidamount');
                        var total = 0;
                        for (var ii = 0; ii < grid.rows.length - 1; ii++) {
                            var idtxt = "ContentPlaceHolder1_grd_InvoiceDetails_txtamtpaid_" + ii;
                            var idc = "ContentPlaceHolder1_grd_InvoiceDetails_chkinv_" + ii;
                            var remainpaid = document.getElementById(idtxt).value;
                            if (isNaN(remainpaid)) {
                                document.getElementById(idtxt).value = 0;
                                remainpaid = 0;
                            }
                            else if (remainpaid == 0) {
                                document.getElementById(idc).checked = false;
                            }
                            else {
                                document.getElementById(idc).checked = true;
                            }
                            total = parseFloat(total) + parseFloat(remainpaid);
                            if (isNaN(total)) {
                                total = 0;
                            }
                            totalpaid.value = total;
                        }
                    }
                </script>

               <%--special charcter block functuion--%> 
                  <script type="text/javascript">
                        function blockSpecialChar(e){
                            var k;
                            document.all ? k = e.keyCode : k = e.which;
                            return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 32 || (k >= 48 && k <= 57));
                        }  
                  </script>

                <script type="text/javascript" language="javascript">
                     function OverAllSharevoucher(a) {
                         var diff = 0;
                         var rowData = a.parentNode.parentNode;
                         var rowIndex = rowData.rowIndex - 1;
                         var grd = document.getElementById('<%= grdcostcenter.ClientID %>');
                         var grid = document.getElementById('ContentPlaceHolder1_grdcostcenter');
                         var totalamount = 0;
                         for (var ii = 0; ii < grid.rows.length - 2; ii++) {
                             var idtxtamount = "ContentPlaceHolder1_grdcostcenter_txtamount_" + ii;
                             var amount = document.getElementById(idtxtamount).value;
                             if (isNaN(amount)) {
                                 amount = 0;
                             }
                             totalamount = parseFloat(totalamount) + parseFloat(amount);
                         }
                         var idtxtnetfreepcs12 = "ContentPlaceHolder1_grdcostcenter_lblfooteramount";
                         if (isNaN(totalamount)) {
                             totalamount = 0;
                         }
                         document.getElementById(idtxtnetfreepcs12).innerHTML = totalamount;

                     }

                     function amountzero_add() {
                         var costamount = 0;
                         var segment = document.getElementById('ContentPlaceHolder1_ddlcatagory');
                         var segmentvalue = segment.options[segment.selectedIndex].value;
                         var brnach = document.getElementById('ContentPlaceHolder1_ddlbranch');
                         var bracnhvalue = brnach.options[brnach.selectedIndex].value;
                         var costamount = document.getElementById('ContentPlaceHolder1_txtamount').value;
                         var flag = 1;
                         var department = document.getElementById('ContentPlaceHolder1_ddldepartment');
                         var departmentvalue = brnach.options[department.selectedIndex].value;

                         if (segmentvalue == 0) {
                             alert('You have not selected any segment')
                             return false;
                         }
                          if (departmentvalue == 0) {
                            alert('You have not selected any department')
                            return false;
                        }
                         if (bracnhvalue == 0) {
                             alert('You have not selected any branch')
                             return false;
                         }
                         if (isNaN(costamount)) {
                             costamount = 0;
                         }
                         if (costamount == 0) {
                             flag = 0;
                         }
                         if (flag == 0) {
                             alert('Amount can not be zero or blank');
                             return false;
                         }
                         else {
                             return true;
                         }
                     }

                     function amountzero_save() {
                         var totalamount = 0;
                         var costamount = 0;
                         var flag_totalamountvalidation = 1;
                         var flag_gridamountvalidation = 1;
                         var grid = document.getElementById('ContentPlaceHolder1_grdcostcenter');
                         var costamount = document.getElementById('ContentPlaceHolder1_lblcostcenteramountshow').innerHTML;
                         var totalamount = 0;
                         for (var ii = 0; ii < grid.rows.length - 2; ii++) {
                             var idtxtamount = "ContentPlaceHolder1_grdcostcenter_txtamount_" + ii;
                             var gridcostamount = document.getElementById(idtxtamount).value;

                             if (isNaN(gridcostamount)) {
                                 gridcostamount = 0;

                             }
                             totalamount = parseFloat(totalamount) + parseFloat(gridcostamount);
                         }
                         if (gridcostamount == 0) {
                             flag_gridamountvalidation = 0;

                         }
                         if (flag_gridamountvalidation == 0) {
                             alert('Costcenter amount can not blank or zero');
                             return false;
                         }

                         if (costamount != totalamount) {
                             flag_totalamountvalidation = 0;
                         }

                         if (flag_totalamountvalidation == 0) {
                             alert('Please check total cost center with ledger amount');
                             return false;
                         }
                         else {
                             return true;
                         }
                    }

                      function checkboxstatus() {
                        var GridVwHeaderCheckbox = document.getElementById("<%=grd_InvoiceDetails.ClientID %>");
                        var totalcount = 0;
                        for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
                            if (GridVwHeaderCheckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked == true) {
                                totalcount = totalcount + 1;
                            }
                        }
                        if (totalcount == 0) {
                            alert('Please choose atleast 1 invoice for adjust!');
                            return false;
                        }
                        else {
                            return true;
                        }
                    }

                </script>
                
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
