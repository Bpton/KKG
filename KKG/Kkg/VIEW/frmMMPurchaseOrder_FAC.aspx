<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmMMPurchaseOrder_FAC.aspx.cs" Inherits="VIEW_frmMMPurchaseOrder_FAC" %>

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
        <Triggers>
            <asp:PostBackTrigger ControlID="btnPoQuotationUpload" />  
            <asp:PostBackTrigger ControlID="btnPoComprativeUpload" />
        </Triggers>
        <ContentTemplate>
            <%--<div class="page_title">
                <span class="title_icon"><span class="computer_imac"></span></span>
                <h3>Purchase Order</h3>
            </div>--%>
            <div id="contentarea" runat="server">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active">
                                <span class="h_icon_he list_images"></span>
                                <h6>Purchase Order Details</h6>
                                <div id="divadd" runat="server" class="btn_30_light" style="float: right;">
                                    <span id="icon" class="icon add_co"></span>
                                    <asp:Button ID="btnnewentry" runat="server" Text="Add New Record" CssClass="btn_link"
                                        OnClick="btnnewentry_Click" CausesValidation="false" />
                                </div>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="InputTable" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr id="divpono" runat="server">
                                            <td class="field_input" style="padding-left: 10px;">
                                                <fieldset>
                                                    <legend>Purchase Order No</legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td width="110" class="innerfield_title">
                                                                <asp:Label ID="Label15" runat="server" Text="PO No"></asp:Label>
                                                            </td>
                                                            <td width="250">
                                                                <asp:TextBox ID="txtpono" runat="server" Enabled="false" Width="200"></asp:TextBox>
                                                            </td>
                                                            <td>&nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px;">
                                                <fieldset>
                                                    <legend>TPU DETAILS</legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td width="110" class="innerfield_title">
                                                                <asp:Label ID="Label5" runat="server" Text="Vendor Name"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td width="260">
                                                                <asp:DropDownList ID="ddlTPUName" runat="server" Width="245px" Height="28px" ValidationGroup="POFooter"
                                                                    OnSelectedIndexChanged="ddlTPUName_SelectedIndexChanged" AutoPostBack="true"
                                                                    class="chosen-select" data-placeholder="-- SELECT TPU NAME --">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="CV_TPUNAME" runat="server" ControlToValidate="ddlTPUName"
                                                                    InitialValue="0" SetFocusOnError="true" ErrorMessage="Required!" ForeColor="Red"
                                                                    ValidationGroup="POFooter"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td width="80" class="innerfield_title">
                                                                <asp:Label ID="lblrefno" runat="server" Text="Quot.Ref.No"></asp:Label>
                                                            </td>
                                                            <td width="140">
                                                                <asp:TextBox ID="txtrefno" runat="server" Width="100%"></asp:TextBox>
                                                            </td>
                                                            <td width="130" class="innerfield_title" align="center">
                                                                <asp:Label ID="lblQuotdt" runat="server" Text="Quot.Ref.Date"></asp:Label>
                                                            </td>
                                                            <td width="165">
                                                                <asp:TextBox ID="txtQuotdt" runat="server" Width="120" placeholder="dd/mm/yyyy" MaxLength="10"
                                                                    onkeypress="return isNumberKeyWithslash(event);"></asp:TextBox>
                                                                <asp:ImageButton ID="imgQuotdt" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender8" PopupButtonID="imgQuotdt" runat="server"
                                                                    TargetControlID="txtQuotdt" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>
                                                            <td width="130" class="innerfield_title" align="center">
                                                                <asp:Label ID="Label11" runat="server" Text="Entry Date"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td width="165">
                                                                <asp:TextBox ID="txtpodate" runat="server" Width="120" placeholder="dd/mm/yyyy" MaxLength="10" Enabled="false"
                                                                    ValidationGroup="datecheckpodetail" onkeypress="return isNumberKeyWithslash(event);"></asp:TextBox>
                                                                <asp:ImageButton ID="imgPopuppodate" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="imgPopuppodate"
                                                                    runat="server" TargetControlID="txtpodate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                                <asp:RequiredFieldValidator ID="CV_PODate" runat="server" ControlToValidate="txtpodate"
                                                                    ValidationGroup="datecheckpodetail" SetFocusOnError="true" ErrorMessage="PO Date is required!"
                                                                    Display="None"></asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender17" runat="server"
                                                                    TargetControlID="CV_PODate" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="~/images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                            </td>
                                                            <td>&nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px;">
                                                <fieldset>
                                                    <legend>PRODUCT DETAILS</legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                        <tr id="trproduct" runat="server">
                                                            <td width="110" class="field_title">
                                                                <asp:Label ID="Label2" runat="server" Text="Product"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td width="330" class="field_input">
                                                                <asp:DropDownList ID="ddlProductName" runat="server" Width="330px" Height="28px"
                                                                    AutoPostBack="true" ValidationGroup="datecheckpodetail" OnSelectedIndexChanged="ddlProductName_SelectedIndexChanged"
                                                                    class="chosen-select" data-placeholder="-- SELECT PRODUCT NAME --">
                                                                    <asp:ListItem Text="-- SELECT PRODUCT NAME --" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="CV_PRODUCTNAME" runat="server" ControlToValidate="ddlProductName"
                                                                    ValidationGroup="datecheckpodetail" InitialValue="0" SetFocusOnError="true" ErrorMessage="Required!"
                                                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td width="50" class="field_title">
                                                                <asp:Label ID="Label8" runat="server" Text="Unit"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td width="246" class="field_input">
                                                                <asp:DropDownList ID="ddlpackingsize" runat="server" Width="245px" Height="28px"
                                                                    ValidationGroup="datecheckpodetail" class="chosen-select" data-placeholder="-- SELECT PRODUCT NAME --">
                                                                    <asp:ListItem Selected="True" Text="-- SELECT PACK SIZE --" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="CV_CASE" runat="server" ControlToValidate="ddlpackingsize"
                                                                    ValidationGroup="datecheckpodetail" InitialValue="0" SetFocusOnError="true" ErrorMessage="Required!"
                                                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td width="50" class="field_title">
                                                                <asp:Label ID="Label4" runat="server" Text="Qty"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtqty" runat="server" Width="60%" onkeypress="return isNumberKey(event);"
                                                                    ValidationGroup="datecheckpodetail" AutoCompleteType="Disabled" onfocus="disableautocompletion(this.id);"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="CV_QTY" runat="server" ControlToValidate="txtqty"
                                                                    ValidationGroup="datecheckpodetail" SetFocusOnError="true" ErrorMessage="Qty is required!"
                                                                    Display="None"></asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
                                                                    TargetControlID="CV_QTY" PopupPosition="BottomRight" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="~/images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                            </td>
                                                            <td width="110" class="innerfield_title">
                                                                <asp:Label ID="Label14" runat="server" Text="Currency Type"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td width="200">
                                                                <asp:DropDownList ID="ddlcurrencyname" runat="server" Width="150px" Height="28px" ValidationGroup="POFooter"
                                                                    class="chosen-select" data-placeholder="-- SELECT ">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="CURRENCY_ID" runat="server" ControlToValidate="ddlcurrencyname"
                                                                    InitialValue="0" SetFocusOnError="true" ErrorMessage="Required!" ForeColor="Red"
                                                                    ValidationGroup="POFooter"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr id="trdate" runat="server">
                                                            <td width="90" class="field_title">
                                                                <asp:Label ID="Label6" runat="server" Text="From Date"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td width="120" class="field_input">
                                                                <asp:TextBox ID="txtreqfromdate" runat="server" MaxLength="15" Width="70" Enabled="false"
                                                                    placeholder="dd/mm/yyyy" ValidationGroup="datecheck"></asp:TextBox>
                                                                <asp:ImageButton ID="imgPopupreqfromdate" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender6" PopupButtonID="imgPopupreqfromdate"
                                                                    runat="server" TargetControlID="txtreqfromdate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>
                                                            <td width="70" class="field_title">
                                                                <asp:Label ID="Label7" runat="server" Text="To Date"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td width="120" class="field_input">
                                                                <asp:TextBox ID="txtreqtodate" runat="server" MaxLength="10" Width="70" placeholder="dd/mm/yyyy"
                                                                    ValidationGroup="datecheck" Enabled="false"></asp:TextBox>
                                                                <asp:ImageButton ID="imgPopupreqtodate" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender7" PopupButtonID="imgPopupreqtodate"
                                                                    runat="server" TargetControlID="txtreqtodate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>
                                                            <td class="field_title">
                                                                <div class="btn_24_blue">
                                                                    <span class="icon find_co"></span>
                                                                    <asp:Button ID="btnindentseach" runat="server" Text="Search" CssClass="btn_link"
                                                                        OnClick="btnindentseach_Click" CausesValidation="false" />
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" id="trgvindentdetails"
                                                        runat="server">
                                                        <tr>
                                                            <td class="field_input">
                                                                <asp:GridView ID="gvindentdetails" EmptyDataText="There are no records available."
                                                                    CssClass="zebra" runat="server" AutoGenerateColumns="False" Width="50%">
                                                                    <PagerSettings Position="TopAndBottom" />
                                                                    <PagerStyle HorizontalAlign="Left" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="" HeaderStyle-Width="20">
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkreq" runat="server" Text=" " value='<%# Eval("INDENTID") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="SL" HeaderStyle-Width="20">
                                                                            <ItemTemplate>
                                                                                <%# Container.DataItemIndex + 1 %>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="" HeaderStyle-Width="20" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblINDENTID" runat="server" Text='<%# Eval("INDENTID") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="INDENTNO" HeaderText="INDENT NO" HeaderStyle-Wrap="false"></asp:BoundField>
                                                                        <asp:BoundField DataField="INDENTDATE" HeaderText="INDENT DATE" HeaderStyle-Wrap="true"></asp:BoundField>
                                                                        <asp:BoundField DataField="DEPTNAME" HeaderText="DEPARTMENT"></asp:BoundField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </td>

                                                            <td class="field_input" style="padding-left: 1px">
                                                                <div class="btn_24_blue">
                                                                    <span class="icon add_co"></span>
                                                                    <asp:Button ID="btnindentadd" runat="server" Text="Add" CssClass="btn_link" OnClick="btnindentadd_Click" />
                                                                </div>
                                                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                                            </td>

                                                        </tr>
                                                    </table>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" id="tradd" runat="server">
                                                        <tr>
                                                            <td width="110" class="field_title" valign="top" style="padding-top: 15px;">
                                                                <asp:Label ID="Label1" runat="server" Text="&nbsp;"></asp:Label>
                                                            </td>
                                                            <td width="132" class="field_input" valign="top">
                                                                <asp:TextBox ID="txtprice" runat="server" placeholder="Price" CssClass="x_large" Enabled="true"
                                                                    ValidationGroup="datecheckpodetail"></asp:TextBox>
                                                                <span class="label_intro">
                                                                    <asp:Label ID="Label12" align="right" runat="server" Text="Purchase&nbsp;Cost"></asp:Label></span>

                                                                <td class="field_input" runat="server">
                                                                    <asp:TextBox ID="txtlastrate" runat="server" Width="50px" onkeypress="return isNumberKey(event);" Enabled="false"></asp:TextBox>
                                                                    <span class="label_intro">
                                                                        <asp:Label ID="Label18" align="right" runat="server" Text="Last&nbsp;Rate"></asp:Label></span>
                                                                </td>

                                                                <td class="field_input" runat="server">
                                                                    <asp:TextBox ID="txtmaxrate" runat="server" Width="50px" onkeypress="return isNumberKey(event);" Enabled="false"></asp:TextBox>
                                                                    <span class="label_intro">
                                                                        <asp:Label ID="Label19" align="right" runat="server" Text="Max&nbsp;Rate"></asp:Label></span>
                                                                </td>

                                                                <td class="field_input" runat="server">
                                                                    <asp:TextBox ID="txtminrate" runat="server" Width="50px" onkeypress="return isNumberKey(event);" Enabled="false"></asp:TextBox>
                                                                    <span class="label_intro">
                                                                        <asp:Label ID="Label20" align="right" runat="server" Text="Min&nbsp;Rate"></asp:Label></span>
                                                                </td>

                                                                <td class="field_input" runat="server">
                                                                    <asp:TextBox ID="txtavgrate" runat="server" Width="50px" onkeypress="return isNumberKey(event);" Enabled="false"></asp:TextBox>
                                                                    <span class="label_intro">
                                                                        <asp:Label ID="Label23" align="right" runat="server" Text="Avg&nbsp;Rate"></asp:Label></span>
                                                                </td>
                                                                <td class="field_input" runat="server">
                                                                    <asp:TextBox ID="txtCgstPer" runat="server" Width="50px" onkeypress="return isNumberKey(event);" Enabled="false"></asp:TextBox>
                                                                    <span class="label_intro">
                                                                        <asp:Label ID="Label31" align="right" runat="server" Text="CGST&nbsp;(%)"></asp:Label></span>
                                                                </td>
                                                                <td class="field_input" runat="server" visible="false">
                                                                    <asp:TextBox ID="txtCgstId" runat="server" Width="50px" onkeypress="return isNumberKey(event);" Enabled="false"></asp:TextBox>
                                                                    <span class="label_intro">
                                                                        <asp:Label ID="Label34" align="right" runat="server" Text="CGST&nbsp;(%)"></asp:Label></span>
                                                                </td>
                                                                <td class="field_input" runat="server">
                                                                    <asp:TextBox ID="txtSgstPer" runat="server" Width="50px" onkeypress="return isNumberKey(event);" Enabled="false"></asp:TextBox>
                                                                    <span class="label_intro">
                                                                        <asp:Label ID="Label32" align="right" runat="server" Text="SGST&nbsp;(%)"></asp:Label></span>
                                                                </td>
                                                                <td class="field_input" runat="server" visible="false">
                                                                    <asp:TextBox ID="txtSgstId" runat="server" Width="50px" onkeypress="return isNumberKey(event);" Enabled="false"></asp:TextBox>
                                                                    <span class="label_intro">
                                                                        <asp:Label ID="Label35" align="right" runat="server" Text="CGST&nbsp;(%)"></asp:Label></span>
                                                                </td>
                                                                <td class="field_input" runat="server">
                                                                    <asp:TextBox ID="txtIgstPer" runat="server" Width="50px" onkeypress="return isNumberKey(event);" Enabled="false"></asp:TextBox>
                                                                    <span class="label_intro">
                                                                        <asp:Label ID="Label33" align="right" runat="server" Text="IGST&nbsp;(%)"></asp:Label></span>
                                                                </td>
                                                                <td class="field_input" runat="server" visible="false">
                                                                    <asp:TextBox ID="txtIgstId" runat="server" Width="50px" onkeypress="return isNumberKey(event);" Enabled="false"></asp:TextBox>
                                                                    <span class="label_intro">
                                                                        <asp:Label ID="Label36" align="right" runat="server" Text="CGST&nbsp;(%)"></asp:Label></span>
                                                                </td>

                                                                <asp:HiddenField runat="server" ID="hdnAssesment" />
                                                                <asp:HiddenField runat="server" ID="hdnMRP" />
                                                            </td>
                                                            <td width="140" class="field_input">
                                                                <asp:TextBox ID="txtrequireddate" runat="server" Width="90" Enabled="false" placeholder="dd/mm/yyyy"
                                                                    MaxLength="10" ValidationGroup="datecheckpodetail" onkeypress="return isNumberKeyWithslash(event);"></asp:TextBox>
                                                                <asp:ImageButton ID="imgPopup" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender4" PopupButtonID="imgPopup" runat="server"
                                                                    TargetControlID="txtrequireddate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                                <span class="label_intro">
                                                                    <asp:Label ID="Label26" runat="server" Text="Delivery From Date"></asp:Label>
                                                                    <span class="req">*</span></span>
                                                                <asp:RequiredFieldValidator ID="CV_Date" runat="server" ControlToValidate="txtrequireddate"
                                                                    ValidationGroup="datecheckpodetail" SetFocusOnError="true" ErrorMessage="Delivery From Date is required!"
                                                                    Display="None"></asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender15" runat="server"
                                                                    TargetControlID="CV_Date" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="~/images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                            </td>
                                                            <td width="140" class="field_input">
                                                                <asp:TextBox ID="txttorequireddate" runat="server" Width="90" Enabled="false" placeholder="dd/mm/yyyy"
                                                                    MaxLength="10" ValidationGroup="datecheckpodetail" onkeypress="return isNumberKeyWithslash(event);"></asp:TextBox>
                                                                <asp:ImageButton ID="imgpopupto" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender5" PopupButtonID="imgpopupto" runat="server"
                                                                    TargetControlID="txttorequireddate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                                <span class="label_intro">
                                                                    <asp:Label ID="Label27" runat="server" Text="Delivery To Date"></asp:Label>
                                                                    <span class="req">*</span></span>
                                                                <asp:RequiredFieldValidator ID="CV_To_Date" runat="server" ControlToValidate="txttorequireddate"
                                                                    ValidationGroup="datecheckpodetail" SetFocusOnError="true" ErrorMessage="Delivery To Date is required!"
                                                                    Display="None"></asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender19" runat="server"
                                                                    TargetControlID="CV_To_Date" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="~/images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                            </td>
                                                            <td class="field_input">
                                                                <div class="btn_24_blue">
                                                                    <span class="icon add_co"></span>
                                                                    <asp:Button ID="btnadd" runat="server" Text="Add" CssClass="btn_link" ValidationGroup="datecheckpodetail"
                                                                        OnClick="btnadd_Click" />
                                                                </div>
                                                                <asp:HiddenField ID="Hdn_Fld" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="gridcontent-inner">
                                        <fieldset>
                                            <legend>PURCHASE ORDER DETAILS</legend>
                                            <cc1:Grid ID="gvPurchaseOrder" runat="server" CallbackMode="true" Serialize="true"
                                                AutoGenerateColumns="false" EnableRecordHover="true" AllowAddingRecords="false"
                                                AllowFiltering="true" AllowSorting="false" AllowPageSizeSelection="true" AllowPaging="true"
                                                PageSize="100" FolderStyle="../GridStyles/premiere_blue">
                                                <FilteringSettings InitialState="Visible" />
                                                <Columns>
                                                    <cc1:Column ID="Column1" DataField="PRODUCTID" ReadOnly="true" HeaderText="ID" runat="server"
                                                        Visible="false" Width="70" />
                                                    <cc1:Column ID="Column12" DataField="Sl" HeaderText="SL" runat="server" Width="50"
                                                        Wrap="true">
                                                        <TemplateSettings TemplateId="slnoTemplate" />
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column2" DataField="PRODUCTNAME" HeaderText="Material" runat="server"
                                                        Width="350" Wrap="true">
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
                                                    <cc1:Column ID="Column3" DataField="PRODUCTQTY" HeaderText="Qty" runat="server" Width="100"
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
                                                    <cc1:Column ID="Column11" DataField="UOMNAME" HeaderText="Unit" runat="server" Width="80"
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
                                                    <cc1:Column ID="Column5" DataField="PRODUCTPRICE" HeaderText="Purchase Cost" runat="server"
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
                                                    <cc1:Column ID="Column9" DataField="PRODUCTAMOUNT" HeaderText="Basic Value " runat="server"
                                                        Width="80" Wrap="true">
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
                                                    <cc1:Column ID="Column22" DataField="LASTRATE" HeaderText="Last Rate " runat="server"
                                                        Width="80" Wrap="true">
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
                                                    <cc1:Column ID="Column23" DataField="MAXRATE" HeaderText="Max Rate " runat="server"
                                                        Width="80" Wrap="true">
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
                                                    <cc1:Column ID="Column24" DataField="AVGRATE" HeaderText="Avg Rate " runat="server"
                                                        Width="80" Wrap="true">
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
                                                    <cc1:Column ID="Column26" DataField="MINRATE" HeaderText="Min Rate " runat="server"
                                                        Width="80" Wrap="true">
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
                                                    <cc1:Column ID="Column27" DataField="CGST" HeaderText="CGST(%) " runat="server"
                                                        Width="80" Wrap="true">
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
                                                    <cc1:Column ID="Column33" DataField="CGSTVALUE" HeaderText="CgstAmnt" runat="server"
                                                        Width="80" Wrap="true">
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
                                                    <cc1:Column ID="Column30" DataField="CGSTTAXID" HeaderText="CGST(%) " runat="server"
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
                                                    <cc1:Column ID="Column28" DataField="SGST" HeaderText="SGST(%)" runat="server"
                                                        Width="80" Wrap="true">
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
                                                    <cc1:Column ID="Column34" DataField="SGSTVALUE" HeaderText="SgstAmnt" runat="server"
                                                        Width="80" Wrap="true">
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
                                                    <cc1:Column ID="Column31" DataField="SGSTTAXID" HeaderText="CGST(%) " runat="server"
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
                                                    <cc1:Column ID="Column29" DataField="IGST" HeaderText="IGST(%) " runat="server"
                                                        Width="80" Wrap="true">
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
                                                    <cc1:Column ID="Column35" DataField="IGSTVALUE" HeaderText="IgstAmnt" runat="server"
                                                        Width="80" Wrap="true">
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
                                                    <cc1:Column ID="Column32" DataField="IGSTTAXID" HeaderText="CGST(%) " runat="server"
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
                                                    <cc1:Column ID="Column14" DataField="MRP" HeaderText="MRP" runat="server" Width="80"
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
                                                    <cc1:Column ID="Column16" DataField="MRPVALUE" HeaderText="MRP Value" runat="server"
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
                                                    <cc1:Column ID="Column10" DataField="ASSESMENTPERCENTAGE" HeaderText="ASSESMENT(%)"
                                                        runat="server" Width="130" Wrap="true" Visible="false">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column17" DataField="Excise" HeaderText="Excise(%)" runat="server"
                                                        Width="130" Visible="false" Wrap="true">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column18" DataField="CST" HeaderText="CST(%)" runat="server" Width="130"
                                                        Visible="false">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column155" DataField="REQUIREDDATE" HeaderText="Delivery From Date"
                                                        runat="server" Width="100" Wrap="true">
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
                                                    <cc1:Column ID="Column109" DataField="REQUIREDTODATE" HeaderText="Delivery To Date"
                                                        runat="server" Width="100" Wrap="true">
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
                                                    <cc1:Column HeaderText="Delete" AllowEdit="false" AllowDelete="true" Width="65" Wrap="true">
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
                                                <ScrollingSettings ScrollWidth="100%" ScrollHeight="220" NumberOfFixedColumns="3" />
                                            </cc1:Grid>
                                            <asp:Button ID="btngrddelete" runat="server" Text="grddelete" Style="display: none"
                                                OnClick="btngrddelete_Click" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete this record?');" />
                                        </fieldset>
                                    </div>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px;">
                                                <fieldset>
                                                    <legend>PURCHASE ORDER TOTAL DETAILS</legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td width="150">
                                                                <asp:TextBox ID="txtgrosstotal" runat="server" MaxLength="15" CssClass="x-large"
                                                                    placeholder="Total Basic Value" Text="0" Font-Bold="true" Enabled="false" CausesValidation="true"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="CV_GrossTotal" runat="server" ControlToValidate="txtgrosstotal"
                                                                    SetFocusOnError="true" ErrorMessage="Total Basic Value is required!" Display="None"
                                                                    ValidationGroup="POFooter"></asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server"
                                                                    TargetControlID="CV_GrossTotal" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="~/images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                                <span class="label_intro">
                                                                    <asp:Label ID="Label3" runat="server" Text="Total Basic Value"></asp:Label></span>
                                                            </td>
                                                            <td width="15">&nbsp;
                                                            </td>
                                                            <td width="150">
                                                                <asp:TextBox ID="txtTotalMRP" runat="server" MaxLength="15" CssClass="x-large" Text="0"
                                                                    ValidationGroup="POFooter" onkeypress="return isNumberKeyWithDot(event);" placeholder="Total MRP"
                                                                    Enabled="false" Font-Bold="true"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="CV_Discount" runat="server" ControlToValidate="txtTotalMRP"
                                                                    SetFocusOnError="true" ErrorMessage="required!" Display="None" ValidationGroup="POFooter"></asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                                    TargetControlID="CV_Discount" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="~/images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                                <span class="label_intro">
                                                                    <asp:Label ID="Label16" runat="server" Text="Total MRP"></asp:Label></span>
                                                            </td>
                                                            <td width="15">&nbsp;
                                                            </td>
                                                            <td width="85">
                                                                <asp:TextBox ID="txtadjustment" runat="server" MaxLength="5" CssClass="x-large" placeholder="Adjustment"
                                                                    Text="0" ValidationGroup="POFooter" onChange="adjustmentcalculation()" onkeypress="return isNumberKeyWithDotMinus(event);"
                                                                    AutoCompleteType="Disabled" onfocus="disableautocompletion(this.id);" Visible ="false"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="CV_Adjustment" runat="server" ControlToValidate="txtadjustment"
                                                                    SetFocusOnError="true" ErrorMessage="Adjustment is required!" Display="None"
                                                                    ValidationGroup="POFooter"></asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server"
                                                                    TargetControlID="CV_Adjustment" PopupPosition="BottomLeft" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="~/images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                                <span class="label_intro">
                                                                    <asp:Label ID="Label17" runat="server" Text="Total Adjustment" Visible ="false"></asp:Label></span>
                                                            </td>
                                                            
                                                            <td width="15"></td>
                                                            <td width="50">
                                                                <asp:TextBox ID="txtDiscPer" runat="server" MaxLength="5" CssClass="x-large" placeholder="Disc Perc" Text="0" Visible="false"
                                                                    ValidationGroup="POFooter" Font-Bold="true" onkeyup="getdiscpercentage(this);" onkeypress="return isNumberKeyWithDot(event);"></asp:TextBox>
                                                            </td>
                                                            <td width="15">&nbsp;</td>
                                                            <td width="50">
                                                                <asp:TextBox ID="txtDiscAmnt" runat="server" MaxLength="5" CssClass="x-large" placeholder="Disc Amnt" Text="0" Visible="false"
                                                                    ValidationGroup="POFooter" Font-Bold="true" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtCgstValue" runat="server" MaxLength="15" Enabled="false" CssClass="x-large"
                                                                    Text="0"  Font-Bold="true" Width="55px"
                                                                    ForeColor="Green" ></asp:TextBox>
                                                                <span class="label_intro">
                                                                    <asp:Label ID="Label37" runat="server" Text="CGST VALUE"></asp:Label></span>
                                                            </td>
                                                             <td width="15">&nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtSgstValue" runat="server" MaxLength="15" Enabled="false" CssClass="x-large"
                                                                    Text="0"  Font-Bold="true" Width="55px"
                                                                    ForeColor="Green"></asp:TextBox>
                                                                <span class="label_intro">
                                                                    <asp:Label ID="Label38" runat="server" Text="SGST VALUE"></asp:Label></span>
                                                            </td>
                                                             <td width="15">&nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtIgstValue" runat="server" MaxLength="15" Enabled="false" CssClass="x-large"
                                                                    Text="0"  Font-Bold="true" Width="55px"
                                                                    ForeColor="Green"></asp:TextBox>
                                                                <span class="label_intro">
                                                                    <asp:Label ID="Label39" runat="server" Text="IGST VALUE"></asp:Label></span>
                                                            </td>
                                                            <td width="15">&nbsp;
                                                            </td>
                                                            <td width="150">
                                                                <asp:TextBox ID="txttotalamount" runat="server" MaxLength="15" CssClass="x-large"
                                                                    placeholder="Total Gross Value" Enabled="false" Text="0" ValidationGroup="POFooter"
                                                                    Font-Bold="true"></asp:TextBox>
                                                                <span class="label_intro">
                                                                    <asp:Label ID="Label13" runat="server" Text="Total Net Value"></asp:Label></span>
                                                            </td>
                                                            <td width="15">&nbsp;
                                                            </td>
                                                            <td width="150">
                                                                <asp:TextBox ID="txtpacking" runat="server" MaxLength="15" CssClass="x-large" placeholder="Packing"
                                                                    Text="0" onkeypress="return isNumberKeyWithDot(event);" Enabled="false" ValidationGroup="POFooter"
                                                                    Font-Bold="true" Visible="false"></asp:TextBox>
                                                                <span class="label_intro">
                                                                    <asp:Label ID="Label21" runat="server" Text="Packing" Visible="false"></asp:Label></span>
                                                            </td>
                                                            <td>&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="8" colspan="10"></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtsaletax" runat="server" MaxLength="15" CssClass="x-large" Visible="false"
                                                                    placeholder="Tot. CST Value" Text="0" onkeypress="return isNumberKeyWithDot(event);"
                                                                    Enabled="false" ValidationGroup="POFooter" Font-Bold="true"></asp:TextBox>
                                                                <span class="label_intro">
                                                                    <asp:Label ID="Label25" runat="server" Text="Tot. CST Value" Visible="false"></asp:Label>
                                                                </span>
                                                            </td>
                                                            <td width="15">&nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtexercise" runat="server" MaxLength="15" CssClass="x-large" placeholder="Tot. Excise Value"
                                                                    Text="0" Visible="false" ValidationGroup="POFooter" onkeypress="return isNumberKeyWithDot(event);"
                                                                    Enabled="false" Font-Bold="true"></asp:TextBox>
                                                                <span class="label_intro">
                                                                    <asp:Label ID="Label22" runat="server" Text="Tot. Excise Value" Visible="false"></asp:Label></span>
                                                            </td>
                                                            <td width="15">&nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtnettotal" runat="server" MaxLength="15" Enabled="false" CssClass="x-large"
                                                                    Text="0" ValidationGroup="POFooter" placeholder="Net Total" Font-Bold="true"
                                                                    ForeColor="Green" Visible="false"></asp:TextBox>
                                                                <span class="label_intro">
                                                                    <asp:Label ID="Label24" runat="server" Text="Net Total" Visible="false"></asp:Label></span>
                                                            </td>
                                                             <td width="15">&nbsp;
                                                            </td>
                                                            
                                                            <td width="15">&nbsp;
                                                            </td>
                                                            <td>&nbsp;
                                                            </td>
                                                            <td width="15">&nbsp;
                                                            </td>
                                                            <td>&nbsp;
                                                            </td>
                                                            <td>&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="8" colspan="10"></td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="gridcontent-inner">
                                        <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                            <tr style="display: none" runat="server">
                                                <td class="field_title">
                                                    <div class="gridcontent-shortstock">
                                                        <fieldset>
                                                            <legend>Terms & Conditions</legend>
                                                            <cc1:Grid ID="grdTerms" runat="server" CallbackMode="true" Serialize="true" AutoGenerateColumns="false"
                                                                FolderStyle="../GridStyles/premiere_blue" EnableRecordHover="true" AllowAddingRecords="false"
                                                                AllowFiltering="false" AllowPaging="false" PageSize="100" AllowSorting="false"
                                                                OnRowDataBound="grdTerms_RowDataBound" AllowPageSizeSelection="false" ViewStateMode="Disabled">
                                                                <FilteringSettings InitialState="Visible" />
                                                                <Columns>
                                                                    <cc1:Column ID="Column19" DataField="SLNO" HeaderText="Sl No" runat="server" Width="60">
                                                                        <TemplateSettings TemplateId="tplTermsNumbering" />
                                                                    </cc1:Column>
                                                                    <cc1:Column ID="Column20" DataField="DESCRIPTION" ReadOnly="true" HeaderText="TERMS & Conditions"
                                                                        runat="server" Width="320" Wrap="true" />
                                                                    <cc1:Column ID="Column21" DataField="ID" ReadOnly="true" HeaderText="TERMSID" runat="server"
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
                                            </tr>
                                            <tr>
                                                <td class="field_title" width="150">
                                                    <asp:Label ID="Label28" runat="server" Text="Terms & Condition"></asp:Label>
                                                </td>
                                                <td lenth="500px">
                                                    <asp:TextBox ID="txtTermsCondition" runat="server" TextMode="MultiLine" Width="700px" BackColor="YellowGreen"
                                                        placeholder="Terms And Condition" class="input_grow"></asp:TextBox>
                                                </td>
                                                 
                                                    <td class="field_title" id="tdlblrejection"  runat="server">
                                                    <asp:Label ID="lblRejection" runat="server" Text="Rejection"></asp:Label>
                                                </td>
                                                <td width="220" id="tdtxtrejection" runat="server">
                                                    <asp:TextBox ID="txtRejection" runat="server" TextMode="MultiLine" Width="100%"
                                                        placeholder="Rejection" class="input_grow" MaxLength="255"></asp:TextBox>
                                                </td>
                                                </div>
                                                
                                                <td class="field_title">
                                                    <asp:Label ID="lblshippingaddr" runat="server" Text="ShippingAddress"></asp:Label>
                                                </td>
                                                <td width="220">
                                                    <asp:TextBox ID="txtshippingaddr" runat="server" TextMode="MultiLine" Width="100%"
                                                        placeholder="ShippingAddress" class="input_grow" MaxLength="255"></asp:TextBox>
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
                                                            <td width="220">
                                                                <asp:TextBox ID="txtremarks" runat="server" TextMode="MultiLine" Width="100%" placeholder="Remarks"
                                                                    class="input_grow" MaxLength="255"></asp:TextBox>
                                                            </td>
                                                            <td width="100" align="left" class="innerfield_title">
                                                                <asp:Label ID="lblCheckerNote" Text="Rejection Note" runat="server"></asp:Label>
                                                            </td>
                                                            <td width="310">
                                                                <asp:TextBox ID="txtCheckerNote" runat="server" TextMode="MultiLine" Enabled="false"
                                                                    MaxLength="255" Style="width: 290px; height: 80px"> </asp:TextBox>
                                                            </td>
                                                            <td width="100" class="field_input" align="left">
                                                                Quotation upload
                                                                <asp:FileUpload ID="FileUpload2" runat="server" ClientIDMode="Static" />&nbsp;

                                                            </td>
                                                            <td width="170" class="field_input" align="left">
                                                                <div class="btn_24_blue">
                                                                    <span class="icon disk_co"></span>
                                                                    <asp:Button ID="btnPoQuotationUpload" runat="server" Text="UPLOAD" CssClass="btn_link"
                                                                        OnClick="btnPoQuotationUpload_Click" />
                                                                </div>
                                                            </td>
                                                            <td width="100" class="field_input">&nbsp;</td>
                                                            <td width="180" class="field_input">&nbsp;
                                                            </td>
                                                            
                                                            <td width="100" class="field_input" align="left">
                                                                PO Comparative statement upload
                                                                <asp:FileUpload ID="FileUpload1" runat="server" ClientIDMode="Static" />&nbsp;

                                                            </td>
                                                            <td width="170" class="field_input" align="left">
                                                                <div class="btn_24_blue">
                                                                    <span class="icon disk_co"></span>
                                                                    <asp:Button ID="btnPoComprativeUpload" runat="server" Text="UPLOAD" CssClass="btn_link"
                                                                        OnClick="btnPoComprativeUpload_Click" />
                                                                </div>
                                                            </td>
                                                            <td width="100" class="field_input">&nbsp;</td>
                                                            <td width="180" class="field_input">&nbsp;
                                                            </td>
                                                        </tr>

                                                        <td width="200">
                                                            <div class="btn_24_blue" id="btnsubmitdiv" runat="server">
                                                                <span class="icon disk_co"></span>
                                                                <asp:Button ID="btnsave" runat="server" Text="Save" CssClass="btn_link" ValidationGroup="POFooter"
                                                                    OnClick="btnsave_Click" />
                                                            </div>
                                                        </td>

                                                        &nbsp;&nbsp;
                                                                <td width="200">
                                                                    <div id="divbtncancel" runat="server">
                                                                        <div class="btn_24_blue">
                                                                            <span class="icon cross_octagon_co"></span>
                                                                            <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn_link" OnClick="btncancel_Click"
                                                                                CausesValidation="false" />
                                                                        </div>
                                                                    </div>
                                                                </td>
                                                        &nbsp;&nbsp;
                                                                 <td width="200">
                                                                     <div class="btn_24_green" id="btnHold" runat="server" visible="false">
                                                                         <span class="icon approve_co"></span>
                                                                         <asp:LinkButton ID="btnHoldApproved" runat="server" Text="Hold Approve" CssClass="btn_link"
                                                                             CausesValidation="false" OnClick="btnHoldApproved_Click" />
                                                                     </div>
                                                                 </td>
                                                        &nbsp;&nbsp;
                                                             
                                                                <td width="200">
                                                                    <div class="btn_24_green" id="btnConfi" runat="server" visible="false">
                                                                        <span class="icon approve_co"></span>
                                                                        <asp:LinkButton ID="btnConfirmed" runat="server" Text="Confirmed" CssClass="btn_link"
                                                                            CausesValidation="false" OnClick="btnConfirmed_Click" />
                                                                    </div>
                                                                </td>
                                                        &nbsp;&nbsp;
                                                                <td width="200">
                                                                    <div class="btn_24_green" id="divbtnapprove" runat="server">
                                                                        <span class="icon approve_co"></span>
                                                                        <asp:LinkButton ID="btnApprove" runat="server" Text="Approve" CssClass="btn_link"
                                                                            CausesValidation="false" OnClick="btnApprove_Click" />
                                                                    </div>
                                                                </td>
                                                        &nbsp;&nbsp;
                                                               <td width="200">
                                                                   <div class="btn_24_red" id="divbtnreject" runat="server">
                                                                       <span class="icon reject_co"></span>
                                                                       <asp:LinkButton ID="btnReject" runat="server" Text="Reject" CssClass="btn_link" CausesValidation="false"
                                                                           OnClick="btnReject_Click" />
                                                                   </div>
                                                               </td>
                                                        &nbsp;&nbsp;
                                                                <td width="200">
                                                                    <div class="btn_24_blue" style="display: none;" runat="server" id="div_btnPrint">
                                                                        <span class="icon printer_co"></span>
                                                                        <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btn_link" OnClick="btnPrint_Click"
                                                                            CausesValidation="false" />
                                                                    </div>
                                                                </td>
                                                        &nbsp;&nbsp;
                                                                <td width="300">
                                                                    <div class="btn_24_blue" runat="server" id="div1">
                                                                        <span class="icon document_b"></span>
                                                                        <asp:Button ID="btnDocuments" runat="server" Text="QUOTATION DOCUMENT STATUS" CssClass="btn_link" OnClick="btnDocuments_Click"
                                                                            CausesValidation="false" />
                                                                    </div>
                                                                </td>
                                                        &nbsp;&nbsp;
                                                                <td width="300">
                                                                    <div class="btn_24_green" runat="server" id="div2">
                                                                        <span class="icon document_b"></span>
                                                                        <asp:Button ID="btnComDocuments" runat="server" Text="COMPARATIVE DOCUMENT STATUS" CssClass="btn_link" OnClick="btnComDocuments_Click"
                                                                            CausesValidation="false" />
                                                                    </div>
                                                                </td>
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
                                                        <td width="70" class="field_input">
                                                            <asp:DropDownList ID="ddlPoType" Width="250" runat="server" class="chosen-select" AutoPostBack="true">
                                                                <asp:ListItem Selected="True" Value="" Text="All" />
                                                                <asp:ListItem Value="N" Text="Pending"/>
                                                                <%--<asp:ListItem Value="H" Text="Hold(P.K.Verma/H.Nigam)" />
                                                                <asp:ListItem Value="C" Text="Confirm(V.k. Sarda/Gla)" />
                                                                <asp:ListItem Value="A" Text="Wait for Approved(S.k.Taparia/Sunil Agwral)" />--%>
                                                                <asp:ListItem Value="Y" Text="Approved" />
                                                                <asp:ListItem Value="R" Text="Rejected" />
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                        <td width="90">
                                                            <asp:Label ID="Label9" runat="server" Text="From Date"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="165">
                                                            <asp:TextBox ID="txtfromdate" runat="server" MaxLength="15" Width="120" Enabled="false"
                                                                placeholder="dd/mm/yyyy" ValidationGroup="datecheck"></asp:TextBox>
                                                            <asp:ImageButton ID="imgPopupfromdate" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopupfromdate"
                                                                runat="server" TargetControlID="txtfromdate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <asp:RequiredFieldValidator ID="CV_FromDate" runat="server" ControlToValidate="txtfromdate"
                                                                SetFocusOnError="true" ErrorMessage="From Date is required!" Display="None" ValidationGroup="datecheck"></asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
                                                                TargetControlID="CV_FromDate" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="~/images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                            <asp:CustomValidator ID="CV_FromDateValidation" runat="server" ClientValidationFunction="ValidateDate"
                                                                ControlToValidate="txtfromdate" SetFocusOnError="true" ErrorMessage="* Invalid Date!"
                                                                Display="None" ValidationGroup="datecheck" />
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" runat="server"
                                                                TargetControlID="CV_FromDateValidation" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="~/images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                        </td>
                                                        <td width="70">
                                                            <asp:Label ID="Label10" runat="server" Text="To Date"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="165">
                                                            <asp:TextBox ID="txttodate" runat="server" MaxLength="10" Width="69%" placeholder="dd/mm/yyyy"
                                                                ValidationGroup="datecheck" Enabled="false"></asp:TextBox>
                                                            <asp:ImageButton ID="imgpopuptodate" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="imgpopuptodate"
                                                                runat="server" TargetControlID="txttodate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <asp:RequiredFieldValidator ID="CV_ToDate" runat="server" ControlToValidate="txttodate"
                                                                SetFocusOnError="true" ErrorMessage="To Date is required!" Display="None" ValidationGroup="datecheck"></asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server"
                                                                TargetControlID="CV_ToDate" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="~/images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                            <asp:CustomValidator ID="CV_ToDateValidation" runat="server" ClientValidationFunction="ValidateDate"
                                                                ControlToValidate="txttodate" SetFocusOnError="true" ErrorMessage="* Invalid Date!"
                                                                Display="None" ValidationGroup="datecheck" />
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server"
                                                                TargetControlID="CV_ToDateValidation" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="~/images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                        </td>
                                                        <td>
                                                            <div class="btn_24_blue">
                                                                <span class="icon find_co"></span>
                                                                <asp:Button ID="btngvfill" runat="server" Text="Search" CssClass="btn_link" ValidationGroup="datecheck"
                                                                    OnClick="btngvfill_Click" />
                                                            </div>
                                                            <asp:HiddenField ID="hdn_pofield" runat="server" />
                                                            <asp:HiddenField ID="hdn_pono" runat="server" />
                                                            <asp:HiddenField ID="hdn_podelete" runat="server" />
                                                            <asp:HiddenField ID="hdn_FromDate" runat="server" />
                                                            <asp:HiddenField ID="hdn_ToDate" runat="server" />
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
                                            AllowAddingRecords="false" PageSize="50" AllowPaging="true" AllowFiltering="true"
                                            AllowPageSizeSelection="true" OnDeleteCommand="DeleteRecordPoDetails" FolderStyle="../GridStyles/premiere_blue"
                                            EnableRecordHover="true" OnRowDataBound="gvpodetails_RowDataBound">
                                            <ClientSideEvents OnBeforeClientDelete="OnBeforepodetailsDelete" OnClientDelete="OnDeletePoDetails" />
                                            <FilteringSettings InitialState="Visible" />
                                            <Columns>
                                                <cc1:Column ID="Column4" DataField="Sl" HeaderText="SL" runat="server" Width="80"
                                                    Wrap="true">
                                                    <TemplateSettings TemplateId="slnoTemplatePO" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column6" DataField="PODATE" HeaderText="PO Date" runat="server" Width="150"
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
                                                <cc1:Column ID="Column7" DataField="PONO" HeaderText="PO NO" runat="server" Width="280"
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
                                                <cc1:Column ID="Column8" DataField="VENDORNAME" HeaderText="VENDOR NAME" runat="server"
                                                    Width="280" Wrap="true">
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
                                                <cc1:Column ID="Column8795" DataField="CREATEDFROM" HeaderText="RAISE_FROM" runat="server"
                                                    Width="100" Wrap="true" Visible="false">
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
                                                <cc1:Column ID="Column13" AllowEdit="true" AllowDelete="true" HeaderText="EDIT" runat="server"
                                                    Width="80" Wrap="true">
                                                    <TemplateSettings TemplateId="editBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column HeaderText="DELETE" AllowEdit="false" AllowDelete="true" Width="80" Wrap="true">
                                                    <TemplateSettings TemplateId="deleteBtnTemplatePO" />
                                                </cc1:Column>

                                                <cc1:Column ID="Column333" AllowEdit="false" AllowDelete="false" HeaderText="PRINT"
                                                    runat="server" Width="80" Wrap="true">
                                                    <TemplateSettings TemplateId="PrintBtnTemplate" />
                                                </cc1:Column>

                                                <cc1:Column ID="Column15" DataField="POID" HeaderText="POID" runat="server" Width="10"
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
                                            </Columns>
                                            <FilteringSettings MatchingType="AnyFilter" />
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="editBtnTemplate">
                                                    <Template>
                                                        <%--<asp:Button runat="server" ID="btn1" CssClass="action-icons c-edit" Text="Edit" OnCommand="EditRecord"  CommandArgument= '<%# Eval("[USERID]") %>' />--%>
                                                        <a href="javascript: //" class="action-icons c-edit" id="btnGridEdit_<%# Container.PageRecordIndex %>"
                                                            onclick="CallServerMethod(this)"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="deleteBtnTemplatePO">
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

                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="PrintBtnTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="filter_btn printer_co" id="btnPOPrint_<%# Container.PageRecordIndex %>"
                                                            onclick="CallServerMethodPrint(this)" title="Print"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>

                                            <%--<ScrollingSettings ScrollWidth="100%"  ScrollHeight="290" />--%>
                                        </cc1:Grid>
                                        <asp:Button ID="btngrdedit" runat="server" Text="grdedit" Style="display: none" OnClick="btngrdedit_Click"
                                            CausesValidation="false" />
                                        <asp:Button ID="btnPOPrint" runat="server" Text="Print" Style="display: none" OnClick="btnPOPrint_Click"
                                            CausesValidation="false" />
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
                function calculation() {
                    var totalamount = 0;
                    var nettotal = 0;

                    adjustmentcalculation();

                    var grosstotal = Number(document.getElementById("<%=txtgrosstotal.ClientID %>").value);
                    grosstotal.toFixed(2);
                    var adjustment = Number(document.getElementById("<%=txtadjustment.ClientID %>").value);

                    var exercise = Number(document.getElementById("<%=txtexercise.ClientID %>").value);
                    var salestax = Number(document.getElementById("<%=txtsaletax.ClientID %>").value);

                    //totalamount = grosstotal - discount + packing + exercise + salestax + othercharges;
                    nettotal = grosstotal + adjustment;
                    document.getElementById("<%=txttotalamount.ClientID %>").value = nettotal.toFixed(2);
                    document.getElementById("<%=txtnettotal.ClientID %>").value = nettotal.toFixed(2);
                }
            </script>
            <script type="text/javascript">
                function adjustmentcalculation() {
                    var nettotal = 0;
                    var totalamount = 0;
                    var grosstotal = Number(document.getElementById("<%=txtgrosstotal.ClientID %>").value);
                    grosstotal.toFixed(2);
                    var adjustment = Number(document.getElementById("<%=txtadjustment.ClientID %>").value);
                    //totalamount = grosstotal ;
                    nettotal = grosstotal + adjustment;
                    document.getElementById("<%=txttotalamount.ClientID %>").value = nettotal.toFixed(2);
                    //document.getElementById("<%=txtnettotal.ClientID %>").value = nettotal.toFixed(2);
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
                function OnDelete(record) {
                    alert(record.Error);
                }

                function CallServerMethod(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnGridEdit_", "");
                    document.getElementById("<%=hdn_pono.ClientID %>").value = gvpodetails.Rows[iRecordIndex].Cells[2].Value;
                    document.getElementById("<%=hdn_pofield.ClientID %>").value = gvpodetails.Rows[iRecordIndex].Cells[9].Value;
                    document.getElementById("<%=btngrdedit.ClientID %>").click();   

                }

                function CallDeleteServerMethod(oLink) {
                    debugger;
                    var iRecordIndexdelete = oLink.id.toString().replace("btnGridDelete_", "");
                    document.getElementById("<%=hdn_podelete.ClientID %>").value = gvPurchaseOrder.Rows[iRecordIndexdelete].Cells[0].Value;
                    document.getElementById("<%=hdn_FromDate.ClientID %>").value = gvPurchaseOrder.Rows[iRecordIndexdelete].Cells[25].Value;
                    document.getElementById("<%=hdn_ToDate.ClientID %>").value = gvPurchaseOrder.Rows[iRecordIndexdelete].Cells[26].Value;
                    document.getElementById("<%=btngrddelete.ClientID %>").click();
                }

                function CallServerMethodPrint(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnPOPrint_", "");
                    document.getElementById("<%=hdn_pofield.ClientID %>").value = gvpodetails.Rows[iRecordIndex].Cells[9].Value;
                    document.getElementById("<%=btnPOPrint.ClientID %>").click();

                }

                function OnBeforepodetailsDelete(record) {
                    record.Error = '';
                    document.getElementById("<%=hdn_pofield.ClientID %>").value = record.PONO;
                    if (confirm("Are you sure you want to delete? "))
                        return true;
                    else
                        return false;
                }
                function OnDeletePoDetails(record) {
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
                function getdiscpercentage(a) {  /*total net amount subtract from discount*/

                    var totalgross = 0;
                    var percentage = 0;
                    var discountper = 0;
                    var basicvalue = 0;
                    var idtotalamount = "ContentPlaceHolder1_txttotalamount";
                    var iddiscper = "ContentPlaceHolder1_txtDiscPer";
                    var iddiscamnt = "ContentPlaceHolder1_txtDiscAmnt";
                    var idTotalbasic = "ContentPlaceHolder1_txtgrosstotal";
                    totalgross = parseFloat(document.getElementById(idtotalamount).value).toFixed(2);
                    discountper = parseFloat(document.getElementById(iddiscper).value).toFixed(2);
                    basicvalue = parseFloat(document.getElementById(idTotalbasic).value).toFixed(2);
                    percentage = ((totalgross / 100) * discountper).toFixed(2);
                    if (discountper > 0) {
                        totalgross = basicvalue - percentage;
                        document.getElementById(iddiscamnt).value = percentage;
                    }
                    else {
                        totalgross = basicvalue;
                        document.getElementById(iddiscamnt).value = 0;
                    }
                    document.getElementById(idtotalamount).value = totalgross;
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>