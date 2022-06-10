<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true"
    CodeFile="frmStockAdjustmentFG_FAC.aspx.cs" Inherits="FACTORY_frmStockAdjustmentFG_FAC" %>

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
            <%--<div class="page_title">
                <span class="title_icon"><span class="computer_imac"></span></span>
                <h3>
                    Stock Journal</h3>
            </div>--%>
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>Stock Journal Details</h6>
                                <div class="btn_30_light" style="float: right;" id="btnaddhide" runat="server">
                                    <span class="icon add_co"></span>
                                    <asp:Button ID="btnnewentry" runat="server" Text="Add New Record" CssClass="btn_link"
                                        CausesValidation="false" OnClick="btnnewentry_Click" />
                                </div>
                                <div class="btn_30_light" style="float: right; display: none;" id="divstockreserve"
                                    runat="server">
                                    <span class="icon add_co"></span>
                                    <asp:Button ID="btnstockreserve" runat="server" Text="Stock Reserve" CssClass="btn_link"
                                        CausesValidation="false" OnClick="btnstockreserve_Click" />
                                </div>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr id="trAdd" runat="server">
                                            <td class="field_input" style="padding-left: 10px">
                                                <fieldset>
                                                    <legend>PRODUCT DETAILS</legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                                        <tr>
                                                            <td class="field_title" width="70">
                                                                <asp:Label ID="lblfromdepo" Text="Depot" runat="server"></asp:Label><span class="req">*</span>
                                                            </td>
                                                            <td class="field_input" width="260" colspan="2">
                                                                <asp:DropDownList ID="ddlfromdepot" runat="server" AppendDataBoundItems="true" Style="width: 250px;"
                                                                    class="chosen-select" ValidationGroup="check" data-placeholder="&nbsp;No Depot Found&nbsp;"
                                                                    OnSelectedIndexChanged="ddlfromdepot_SelectedChanged" AutoPostBack="true">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td class="field_title" width="70">
                                                                <asp:Label ID="Label1" runat="server" Text="Entry Date"></asp:Label><span class="req">*</span>
                                                            </td>
                                                            <td class="field_input" width="110">
                                                                <asp:TextBox ID="txtadjustmentdate" runat="server" Style="width: 70px;" 
                                                                    placeholder="dd/mm/yyyy" MaxLength="10" ValidationGroup="check" onkeypress="return isNumberKeyWithslash(event);">
                                                                </asp:TextBox>
                                                                <asp:ImageButton ID="imgPopuppodate" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarAdjustmentdate" PopupButtonID="imgPopuppodate"
                                                                    runat="server" TargetControlID="txtadjustmentdate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>
                                                            <td id="tradjunmentheader" runat="server" class="field_title" width="100">
                                                                <asp:Label ID="lblqualitycontrol" runat="server" Text="Journal No"></asp:Label>
                                                            </td>
                                                            <td id="tradjunmentno" runat="server" class="field_input">
                                                                <asp:TextBox ID="txtadjustmentno" runat="server" placeholder="Auto Generate Journal No"
                                                                    Enabled="false" Style="width: 235px;"></asp:TextBox><br />
                                                            </td>
                                                            <td width="200">
                                                                <asp:RadioButtonList ID="rdbtype" runat="server" AutoPostBack="true" ForeColor="blue" BackColor="AliceBlue"
                                                                    OnSelectedIndexChanged="rdbtype_SelectedIndexChanged"
                                                                    RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="With Stock" Value="G" Selected="True" style="color: #01773c; font-weight: bold;" />
                                                                    <asp:ListItem Text="WithOut Stock" Value="E" style="color: #ec00a2;" />
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field_title">
                                                                <asp:Label ID="lblstorelocation" Text="Store" runat="server"></asp:Label><span class="req">*</span>
                                                            </td>
                                                            <td width="180" class="field_input">
                                                                <asp:DropDownList ID="ddlstorelocation" runat="server" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlstorelocation_SelectedIndexChanged"
                                                                    Style="width: 170px;" ValidationGroup="check" class="chosen-select" data-placeholder="Select Store Loacation">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                                                    ValidationGroup="check" Font-Bold="true" ForeColor="Red" ControlToValidate="ddlstorelocation"
                                                                    ValidateEmptyText="false" SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
                                                            </td>
                                                            <td class="field_title" width="60">
                                                                <asp:Label ID="Label14" Text="Category" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input" colspan="2">
                                                                <asp:DropDownList ID="ddlCategory" runat="server" class="chosen-select" data-placeholder="Select Category"
                                                                    AppendDataBoundItems="True" AutoPostBack="true" ValidationGroup="A" Width="215"
                                                                    OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td width="70" class="field_title">
                                                                <asp:Label ID="Label11" Text="Product" runat="server"></asp:Label><span class="req">*</span>
                                                            </td>
                                                            <td width="310" class="field_input">
                                                                <asp:DropDownList ID="ddlproductname" runat="server" AppendDataBoundItems="true"
                                                                    Style="width: 300px; background-color: Black" ValidationGroup="check" class="chosen-select"
                                                                    data-placeholder="Select Product Name" OnSelectedIndexChanged="ddlproductname_SelectedChanged"
                                                                    AutoPostBack="true">
                                                                    <asp:ListItem Text="SELECT PRODUCT NAME" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*"
                                                                    ValidationGroup="check" Font-Bold="true" ForeColor="Red" ControlToValidate="ddlproductname"
                                                                    ValidateEmptyText="false" SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
                                                            </td>
                                                            <td width="60" class="field_title">
                                                                <asp:Label ID="Label13" Text="PackSize" runat="server"></asp:Label><span class="req">*</span>
                                                            </td>
                                                            <td class="field_input" width="130">
                                                                <asp:DropDownList ID="ddlpackingsize" runat="server" AppendDataBoundItems="true"
                                                                    Style="width: 120px;" ValidationGroup="check" class="chosen-select" data-placeholder="-- Select Pack Size --"
                                                                    OnSelectedIndexChanged="ddlpackingsize_SelectedIndexChanged" AutoPostBack="true">
                                                                    <asp:ListItem Text="SELECT PACK SIZE" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*"
                                                                    Font-Bold="true" ForeColor="Red" ControlToValidate="ddlpackingsize" ValidateEmptyText="false"
                                                                    ValidationGroup="check" SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
                                                            </td>
                                                            <td class="field_input">&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field_title">
                                                                <asp:Label ID="Label12" Text="Batch No" runat="server"></asp:Label><span class="req">*</span>
                                                            </td>
                                                            <td class="field_input" colspan="6">
                                                                <asp:DropDownList ID="ddlbatchno" runat="server" AppendDataBoundItems="true" ValidationGroup="check"
                                                                    CssClass="common-select" data-placeholder="Select Batch No" OnSelectedIndexChanged="ddlbatchno_SelectedIndexChanged"
                                                                    AutoPostBack="true" Style="width: 700px; font-family: 'Courier New', Courier, monospace;">
                                                                </asp:DropDownList>
                                                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="*"
                                                                    Font-Bold="true" ForeColor="Red" ControlToValidate="ddlbatchno" ValidateEmptyText="false"
                                                                    ValidationGroup="check" SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field_title">&nbsp;
                                                            </td>
                                                            <td class="field_input" colspan="7">
                                                                <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                                                    <tr>
                                                                        <td style="display:none">
                                                                            <asp:TextBox ID="txtbatchno" runat="server" placeholder="Batch No" Width="80"
                                                                                AutoCompleteType="Disabled" onfocus="disableautocompletion(this.id);"></asp:TextBox>
                                                                            <span class="label_intro">
                                                                                <asp:Label ID="lblbatchno" runat="server" Text="Batch No" Font-Bold="True"></asp:Label></span>
                                                                          <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                                                                Font-Bold="true" ForeColor="Red" ControlToValidate="txtbatchno" ValidateEmptyText="false"
                                                                                ValidationGroup="check" SetFocusOnError="true" Font-Size="Larger"></asp:RequiredFieldValidator>--%>
                                                                        </td>
                                                                        <td style="padding-top: 7px">
                                                                            <asp:TextBox ID="txtadjustmentqty" runat="server" placeholder="Journal Qty" CssClass="x_large"
                                                                                AutoCompleteType="Disabled" onfocus="disableautocompletion(this.id);"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="*"
                                                                                Font-Bold="true" ForeColor="Red" ControlToValidate="txtadjustmentqty" ValidateEmptyText="false"
                                                                                ValidationGroup="check" SetFocusOnError="true" Font-Size="Larger"></asp:RequiredFieldValidator>
                                                                            <span class="label_intro">
                                                                                <asp:Label ID="Label3" runat="server" Text="JOURNAL QTY" Font-Bold="True"></asp:Label></span>
                                                                        </td>
                                                                        <td style="padding-top: 7px">
                                                                            <asp:TextBox ID="txtstockqty" runat="server" placeholder="Stock Qty" Enabled="false"
                                                                                CssClass="x_large"></asp:TextBox>
                                                                            <span class="label_intro">
                                                                                <asp:Label ID="Label7" runat="server" Text="STOCK QTY" Font-Bold="True"></asp:Label></span>
                                                                        </td>
                                                                        <td style="padding-top: 16px">
                                                                            <asp:TextBox ID="txtprice" runat="server" placeholder="Rate" Enabled="false"
                                                                                CssClass="x_large"></asp:TextBox>
                                                                            <span class="label_intro">
                                                                                <asp:Label ID="Label8" runat="server" Text="RATE" Font-Bold="True"></asp:Label></span>
                                                                            <asp:RequiredFieldValidator ID="Reqprice" runat="server" ErrorMessage="*"
                                                                                Font-Bold="true" ForeColor="Red" ControlToValidate="txtprice" ValidateEmptyText="false"
                                                                                ValidationGroup="check" SetFocusOnError="true" Font-Size="Larger"></asp:RequiredFieldValidator>
                                                                        </td>
                                                                        <td width="20" style="padding-top: 16px">
                                                                            <asp:TextBox ID="txtmrp" runat="server" Width="50" placeholder="MRP" Enabled="false"></asp:TextBox>
                                                                            <span class="label_intro">
                                                                                <asp:Label ID="Label4" runat="server" Text="MRP" Font-Bold="True"></asp:Label></span>
                                                                            <asp:RequiredFieldValidator ID="Reqmrp" runat="server" ErrorMessage="Required!"
                                                                                Font-Bold="true" ForeColor="Red" ControlToValidate="txtmrp" ValidateEmptyText="false"
                                                                                ValidationGroup="check" SetFocusOnError="true" Font-Size="Larger"></asp:RequiredFieldValidator>
                                                                        </td>

                                                                        <td width="20" style="display:none">
                                                                            <asp:TextBox ID="txtassesment" runat="server" Width="50" placeholder="ASSES(%)" Enabled="false"></asp:TextBox>
                                                                            <span class="label_intro">
                                                                                <asp:Label ID="lblassesment" runat="server" Text="ASSES(%)" Font-Bold="True"></asp:Label></span>
                                                                          <%--  <asp:RequiredFieldValidator ID="Reqassesment" runat="server" ErrorMessage="Required!"
                                                                                Font-Bold="true" ForeColor="Red" ControlToValidate="txtassesment" ValidateEmptyText="false"
                                                                                ValidationGroup="check" SetFocusOnError="true" Font-Size="Larger"></asp:RequiredFieldValidator>--%>
                                                                        </td>

                                                                        <td width="100" style="display:none">
                                                                            <asp:TextBox ID="txtmfgdate" runat="server" Width="60" placeholder="dd/mm/yyyy" OnTextChanged="txtmfgdate_TextChanged" AutoPostBack="true"
                                                                                MaxLength="10" ValidationGroup="ADD" onkeypress="return isNumberKeyWithslash(event);"></asp:TextBox>
                                                                            <asp:ImageButton ID="ImageButtonMFDate" runat="server" ImageUrl="~/images/calendar.png" Width="24px" Height="24px" ImageAlign="AbsMiddle" />
                                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtenderMFDate" TargetControlID="txtmfgdate" PopupButtonID="ImageButtonMFDate"
                                                                                runat="server" Format="dd/MM/yyyy" Enabled="true" BehaviorID="CalendarExtenderMFDate" CssClass="cal_Theme1" />
                                                                            <span class="label_intro">
                                                                                <asp:Label ID="Label15" runat="server" Text="MFG DATE"></asp:Label></span>
                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorMFDate" runat="server" ForeColor="Red"
                                                                                ControlToValidate="txtmfgdate" ValidationGroup="A" ErrorMessage="Invalid"
                                                                                ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$"></asp:RegularExpressionValidator>
                                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorInvoiceDate" runat="server"
                                                                                ControlToValidate="txtmfgdate" ValidateEmptyText="false" SetFocusOnError="true"
                                                                                ErrorMessage="Required!" ValidationGroup="check" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                                        </td>

                                                                        <td width="100" style="display:none">
                                                                            <asp:TextBox ID="txtexpdate" runat="server" Width="60" placeholder="dd/mm/yyyy"
                                                                                MaxLength="10" ValidationGroup="ADD" onkeypress="return isNumberKeyWithslash(event);"></asp:TextBox>
                                                                            <asp:ImageButton ID="ImageButtonExprDate" runat="server" ImageUrl="~/images/calendar.png" Width="24px" Height="24px" ImageAlign="AbsMiddle" />
                                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtenderExprDate" TargetControlID="txtexpdate" PopupButtonID="ImageButtonExprDate"
                                                                                runat="server" Format="dd/MM/yyyy" Enabled="true" BehaviorID="CalendarExtenderExprDate" CssClass="cal_Theme1" />
                                                                            <span class="label_intro">
                                                                                <asp:Label ID="Label16" runat="server" Text="EXP DATE"></asp:Label></span>
                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorExprDate" runat="server" ForeColor="Red"
                                                                                ControlToValidate="txtexpdate" ValidationGroup="A" ErrorMessage="Invalid"
                                                                                ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$"></asp:RegularExpressionValidator>

                                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                                                                ControlToValidate="txtexpdate" ValidateEmptyText="false" SetFocusOnError="true"
                                                                                ErrorMessage="Required!" ValidationGroup="check" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                                        </td>

                                                                        <td class="field_title" width="70" style="padding-bottom: 16px;">
                                                                            <asp:Label ID="Label2" Text="Reason" runat="server"></asp:Label><span class="req">*</span>
                                                                        </td>
                                                                        <td class="field_input">
                                                                            <asp:DropDownList ID="ddlreason" runat="server" AppendDataBoundItems="true" Style="width: 200px;"
                                                                                ValidationGroup="check" class="chosen-select">
                                                                                <asp:ListItem Text="SELECT REASON NAME" Value="0"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="* Required!"
                                                                                Font-Bold="true" ForeColor="Red" ControlToValidate="ddlreason" ValidateEmptyText="false"
                                                                                ValidationGroup="check" SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
                                                                        </td>
                                                                        <td class="field_input" width="70" style="padding-bottom: 16px;">
                                                                            <div class="btn_24_blue" id="divadd" runat="server">
                                                                                <span class="icon add_co"></span>
                                                                                <asp:Button ID="btnadd" runat="server" Text="ADD" CssClass="btn_link" ValidationGroup="check"
                                                                                    OnClick="btnadd_Click" CausesValidation="true" />
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td class="field_title">&nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="gridcontent-inner">
                                        <fieldset>
                                            <legend>JOURNAL DETAILS</legend>
                                            <cc1:Grid ID="gvadjustment" runat="server" CallbackMode="true" AutoGenerateColumns="false"
                                                AllowFiltering="true" AllowPaging="true" PageSize="500" AllowPageSizeSelection="true"
                                                FolderStyle="../GridStyles/premiere_blue" AllowAddingRecords="false" AllowSorting="false"
                                                OnRowDataBound="gvadjustment_RowDataBound">
                                                <FilteringSettings InitialState="Visible" />
                                                <Columns>
                                                    <cc1:Column ID="Column11" DataField="GUID" ReadOnly="true" HeaderText="GUID" runat="server"
                                                        Visible="false" Width="60" />
                                                    <cc1:Column ID="Column1" DataField="PRODUCTID" ReadOnly="true" HeaderText="ID" runat="server"
                                                        Visible="false" Width="60" />
                                                    <cc1:Column ID="Column4" DataField="Sl" HeaderText="SL" runat="server" Width="60">
                                                        <TemplateSettings TemplateId="slnoTemplate" />
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column5" DataField="PRODUCTNAME" HeaderText="PRODUCT" runat="server"
                                                        Width="380">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column13" DataField="MRP" HeaderText="MRP" runat="server" Width="100"
                                                        Wrap="true" />
                                                    <cc1:Column ID="Column6" DataField="BATCHNO" HeaderText="BATCH NO" runat="server"
                                                        Width="125">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column2" DataField="ADJUSTMENTQTY" HeaderText="JOURNAL QTY" runat="server"
                                                        Width="100" Wrap="true">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column7" DataField="PACKINGSIZENAME" HeaderText="PACK SIZE" runat="server"
                                                        Width="100">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column8" DataField="REASONNAME" HeaderText="REASON" runat="server"
                                                        Width="100" Wrap="true">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column9" DataField="REASONID" HeaderText="REASONID" runat="server"
                                                        Visible="false" Width="200" Wrap="true">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column10" DataField="STORELOCATIONNAME" HeaderText="STORE LOCATION"
                                                        runat="server" Width="150" Wrap="true" />
                                                    <cc1:Column DataField="STORELOCATIONID" HeaderText="STORELOCATIONID" runat="server"
                                                        Visible="false" Width="200" Wrap="true">
                                                    </cc1:Column>
                                                    <cc1:Column DataField="MFDATE" HeaderText="MFDATE" runat="server" Visible="false"
                                                        Width="100" Wrap="true" />
                                                    <cc1:Column DataField="EXPRDATE" HeaderText="EXPRDATE" runat="server" Visible="false"
                                                        Width="100" Wrap="true" />
                                                    <cc1:Column DataField="ASSESMENTPERCENTAGE" HeaderText="ASSESMENTPERCENTAGE" runat="server"
                                                        Visible="false" Width="100" Wrap="true" />
                                                    <cc1:Column DataField="WEIGHT" HeaderText="WEIGHT" runat="server" Visible="false"
                                                        Width="100" Wrap="true" />
                                                    <cc1:Column HeaderText="DELETE" AllowEdit="false" AllowDelete="true" Width="80">
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
                                                <ScrollingSettings ScrollWidth="100%" ScrollHeight="200" />
                                            </cc1:Grid>
                                            <asp:Button ID="btngrddelete" runat="server" Text="grddelete" Style="display: none"
                                                OnClick="btngrddelete_Click" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete this record?');" />
                                        </fieldset>
                                    </div>
                                    <div class="gridcontent-inner">
                                        <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                            <tr>
                                                <td class="field_title" width="80">
                                                    <asp:Label ID="Label9" runat="server" Text="TOT.PCS(+ve)"></asp:Label>
                                                </td>
                                                <td class="field_input" width="80">
                                                    <asp:TextBox ID="txtPosPCS" runat="server" Style="width: 70" Enabled="false"
                                                        MaxLength="255"> </asp:TextBox>
                                                </td>
                                                <td class="field_title" width="80">
                                                    <asp:Label ID="Label10" runat="server" Text="TOT.PCS(-ve)"></asp:Label>
                                                </td>
                                                <td class="field_input" width="80">
                                                    <asp:TextBox ID="txtNegPCS" runat="server" Style="width: 70" Enabled="false"
                                                        MaxLength="70"> </asp:TextBox>
                                                </td>
                                                <td>&nbsp;</td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="gridcontent-inner">
                                        <fieldset>
                                            <legend>REMARKS & OTHERS</legend>
                                            <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                                <tr>
                                                    <td class="field_title" width="8%">Remarks
                                                    </td>
                                                    <td class="field_input" width="26%">
                                                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="mid" TextMode="MultiLine" Style="width: 90%; height: 80px"
                                                            MaxLength="255"> </asp:TextBox>
                                                    </td>
                                                    <td class="field_input" colspan="2">
                                                        <div class="btn_24_blue" id="divbtnsave" runat="server">
                                                            <span class="icon disk_co"></span>
                                                            <asp:Button ID="btnsave" runat="server" Text="Save" CssClass="btn_link" OnClick="btnsave_Click" />
                                                        </div>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                        <div class="btn_24_blue">
                                                            <span class="icon cross_octagon_co"></span>
                                                            <asp:Button ID="btncancel" runat="server" CssClass="btn_link" Text="Cancel" CausesValidation="false"
                                                                OnClick="btncancel_Click" />
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



                                                        <asp:HiddenField ID="hdn_adjustmentid" runat="server" />
                                                        <asp:HiddenField ID="hdn_guid" runat="server" />
                                                        <asp:HiddenField ID="hdn_lockadjustmentqty" runat="server" />
                                                        <asp:HiddenField ID="hdn_stockqtyinpcs" runat="server" />
                                                        <asp:HiddenField ID="hdn_editedstockqty" runat="server" />
                                                        <asp:HiddenField ID="hdn_ASSESMENTPERCENTAGE" runat="server" />
                                                        <asp:HiddenField ID="hdn_mfgdate" runat="server" />
                                                        <asp:HiddenField ID="hdn_exprdate" runat="server" />
                                                        <asp:HiddenField runat="server" ID="Hdn_Print" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlDisplay" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_title">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="90">
                                                            <asp:Label ID="Label17" runat="server" Text="From Date"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="110">
                                                            <asp:TextBox ID="txtfromdateins" runat="server" MaxLength="10" Width="70" Enabled="false"
                                                                placeholder="dd/mm/yyyy" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="datecheckins"
                                                                Font-Bold="true"></asp:TextBox>
                                                            <asp:ImageButton ID="imgPopup" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="Calendar1" PopupButtonID="imgPopup" runat="server"
                                                                TargetControlID="txtfromdateins" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <asp:RequiredFieldValidator ID="CV_FromDate_QC" runat="server" ControlToValidate="txtfromdateins"
                                                                SetFocusOnError="true" ErrorMessage="From Date is required!" Display="None" ValidationGroup="datecheckins"></asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server"
                                                                TargetControlID="CV_FromDate_QC" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                            <asp:CustomValidator ID="CV_FromDateValidation_qc" runat="server" ClientValidationFunction="ValidateDate"
                                                                ControlToValidate="txtfromdateins" SetFocusOnError="true" ErrorMessage="* Invalid Date!"
                                                                Display="None" ValidationGroup="datecheckins" />
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender28" runat="server"
                                                                TargetControlID="CV_FromDateValidation_qc" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                        </td>
                                                        <td width="70">
                                                            <asp:Label ID="Label18" runat="server" Text="To Date"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="110">
                                                            <asp:TextBox ID="txttodateins" runat="server" MaxLength="10" Width="70" Enabled="false"
                                                                placeholder="dd/mm/yyyy" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="datecheckins"
                                                                Font-Bold="true"></asp:TextBox>
                                                            <asp:ImageButton ID="ImageButton1" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender4" PopupButtonID="ImageButton1"
                                                                runat="server" TargetControlID="txttodateins" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <asp:RequiredFieldValidator ID="CV_ToDate_QC" runat="server" ControlToValidate="txttodateins"
                                                                SetFocusOnError="true" ErrorMessage="To Date is required!" Display="None" ValidationGroup="datecheckins"></asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server"
                                                                TargetControlID="CV_ToDate_QC" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                            <asp:CustomValidator ID="CV_ToDateValidation_QC" runat="server" ClientValidationFunction="ValidateDate"
                                                                ControlToValidate="txttodateins" SetFocusOnError="true" ErrorMessage="* Invalid Date!"
                                                                Display="None" ValidationGroup="datecheckins" />
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server"
                                                                TargetControlID="CV_ToDateValidation_QC" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                        </td>
                                                        <td>
                                                            <div class="btn_24_blue">
                                                                <span class="icon find_co"></span>
                                                                <asp:Button ID="btnSTfind" runat="server" Text="Search" CssClass="btn_link" ValidationGroup="datecheckins"
                                                                    OnClick="btnSTfind_Click" />
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
                                    <div class="gridcontent-inner" style="padding-left: 10px;">
                                        <cc1:Grid ID="gvStockAdjustmentDetails" runat="server" CallbackMode="false" Serialize="true"
                                            FolderStyle="../GridStyles/premiere_blue" AutoGenerateColumns="false" AllowPageSizeSelection="true"
                                            AllowPaging="true" PageSize="500" AllowAddingRecords="false" AllowFiltering="true" OnRowDataBound="gvStockAdjustmentDetails_RowDataBound"
                                            AllowSorting="false">
                                            <FilteringSettings InitialState="Visible" />
                                            <Columns>
                                                <cc1:Column ID="Column3" DataField="ADJUSTMENTID" HeaderText="ADJUSTMENTID" runat="server"
                                                    Width="60" Visible="false">
                                                </cc1:Column>
                                                <cc1:Column ID="Column20" DataField="Sl" HeaderText="SL" runat="server" Width="60">
                                                    <TemplateSettings TemplateId="slnoTemplateIN" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column21" DataField="ADJUSTMENTDATE" HeaderText="JOURNAL DATE" runat="server"
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
                                                <cc1:Column ID="Column22" DataField="ADJUSTMENTNO" HeaderText="JOURNAL NO" runat="server"
                                                    Wrap="true" Width="220">
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
                                                <cc1:Column ID="Column24" DataField="DEPOTNAME" HeaderText="DEPOT NAME" runat="server"
                                                    Wrap="true" Width="180">
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
                                                <cc1:Column ID="Column12" DataField="ADJUSTMENT_FROMMENU" HeaderText="JOURNAL TYPE"
                                                    runat="server" Wrap="true" Width="220">
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
                                                <cc1:Column ID="Column14" DataField="USERNAME" HeaderText="ENTRY USER"
                                                    runat="server" Wrap="true" Width="150">
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

                                                <cc1:Column ID="Column15" DataField="ISVERIFIEDDESC" HeaderText="STATUS"
                                                    runat="server" Wrap="true" Width="150">
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
                                                <cc1:Column ID="Column23" AllowEdit="true" AllowDelete="true" HeaderText="EDIT / VIEW"
                                                    runat="server" Width="100%">
                                                    <TemplateSettings TemplateId="editBtnTemplate" />
                                                </cc1:Column>

                                                <cc1:Column ID="Column30" AllowEdit="false" AllowDelete="false" HeaderText="PRINT"
                                                    runat="server" Width="80" Wrap="true">
                                                    <TemplateSettings TemplateId="PrintBtnTemplate" />
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
                                                <cc1:GridTemplate runat="server" ID="PrintBtnTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="filter_btn printer_co" id="btnPrint_<%# Container.PageRecordIndex %>"
                                                            onclick="CallServerMethodPrint(this)"></a>
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
                                            <ScrollingSettings ScrollWidth="100%" ScrollHeight="290" />
                                        </cc1:Grid>
                                        <asp:Button ID="btngrdedit" runat="server" Text="grdedit" Style="display: none" OnClick="btngrdedit_Click"
                                            CausesValidation="false" />
                                        <asp:Button ID="btnPrint" runat="server" Text="Print" Style="display: none" OnClick="btnPrint_Click"
                                            CausesValidation="false" />
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlreserve" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td"
                                        align="center">
                                        <tr>
                                            <td class="field_title" width="110">
                                                <asp:Label ID="Label5" runat="server" Text="Reserve Date"></asp:Label><span class="req">*</span>
                                            </td>
                                            <td class="field_input" width="250">
                                                <asp:TextBox ID="txtreservedate" runat="server" Style="width: 90px;" Enabled="false"
                                                    placeholder="dd/mm/yyyy" MaxLength="10" ValidationGroup="check" onkeypress="return isNumberKeyWithslash(event);">
                                                </asp:TextBox>
                                                <asp:ImageButton ID="ImageButton2" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                    runat="server" Height="24" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImageButton2"
                                                    runat="server" TargetControlID="txtreservedate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                            <td class="field_title" width="150">
                                                <asp:Label ID="Label6" runat="server" Text="Business Segment"></asp:Label><span class="req">*</span>
                                            </td>
                                            <td class="field_input" width="250">
                                                <asp:DropDownList ID="ddlbsegment" runat="server" AppendDataBoundItems="True" Width="180"
                                                    class="chosen-select" data-placeholder="Choose">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="field_input">
                                                <div class="btn_24_blue">
                                                    <span class="icon s_circle"></span>
                                                    <asp:Button ID="btnshow" runat="server" Text="Show" CssClass="btn_link" CausesValidation="false"
                                                        OnClick="btnshow_Click" />
                                                </div>
                                                <div class="btn_24_blue">
                                                    <span class="icon s_circle"></span>
                                                    <asp:Button ID="btnaddreserve" runat="server" Text="Reserve" CssClass="btn_link"
                                                        CausesValidation="false" OnClick="btnaddreserve_Click" OnClientClick="ShowConfirmBox(this,'<b>Are you sure you want to reserve total kolkata depot stock to Reserve Target Sales? Once you agree and wants to process completed, then you can not able to billing from your depot!</b>',80,500); return false;" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
                                                    <span class="icon cross_octagon_co"></span>
                                                    <asp:Button ID="btncancelreserver" runat="server" CssClass="btn_link" Text="Cancel"
                                                        CausesValidation="false" OnClick="btncancelreserver_Click" />
                                                </div>
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="gridcontent-inner" id="divaddstocktake" runat="server">
                                        <fieldset>
                                            <legend>Product Details</legend>
                                            <div style="overflow: hidden; width: 100%;" id="DivHeaderRow">
                                            </div>
                                            <%--onscroll="MakeStaticHeader('<%= grdclosingdetails.ClientID %>', 300, '100%' , 30 ,false)"--%>
                                            <div style="overflow: scroll; height: 300px;" onscroll="OnScrollDiv(this)" id="DivMainContent">
                                                <asp:GridView ID="grdstockdetails" EmptyDataText="There are no records available."
                                                    CssClass="zebra" runat="server" AutoGenerateColumns="False" onchange="MakeStaticHeader('<%= grdclosingdetails.ClientID %>', 300, '100%' , 30 ,false)"
                                                    Width="100%">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SL" Visible="false">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PRODUCTID" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblproductid" runat="server" Text='<%# Bind("PRODUCTID") %>' value='<%# Eval("PRODUCTID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="PRODUCTNAME" HeaderText="PRODUCTNAME" HeaderStyle-Wrap="false"></asp:BoundField>
                                                        <asp:TemplateField HeaderText="MRP">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblmrp" runat="server" Text='<%# Bind("MRP") %>' value='<%# Eval("MRP") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="BATCHNO" HeaderText="BATCH NO" HeaderStyle-Wrap="true"></asp:BoundField>
                                                        <asp:BoundField DataField="MFDATE" HeaderText="MFG DATE"></asp:BoundField>
                                                        <asp:BoundField DataField="EXPRDATE" HeaderText="EXP DATE"></asp:BoundField>
                                                        <asp:TemplateField HeaderText="OPENING STOCK">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblopeningstk" runat="server" Text='<%# Bind("QTY") %>' value='<%# Eval("QTY") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TRANSFER STOCK">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtclosingstockqty" AutoPostBack="false" runat="server" MaxLength="10"
                                                                    Style="text-align: right;" Width="70px" Height="20" Text='<%# Bind("QTY") %>'
                                                                    value='<%# Eval("QTY") %>' onkeypress="return isNumberKey(event);" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div id="DivFooterRow" style="overflow: hidden">
                                            </div>
                                        </fieldset>
                                    </div>
                                    <div class="gridcontent-inner" id="div1" runat="server">
                                        <fieldset>
                                            <legend>Product Details</legend>
                                            <div style="overflow: hidden; width: 100%;" id="Div2">
                                            </div>
                                            <%--onscroll="MakeStaticHeader('<%= grdclosingdetails.ClientID %>', 300, '100%' , 30 ,false)"--%>
                                            <div style="overflow: scroll; height: 300px;" onscroll="OnScrollDiv(this)" id="Div3">
                                                <asp:GridView ID="GridView1" EmptyDataText="There are no records available." CssClass="zebra"
                                                    runat="server" AutoGenerateColumns="False" onchange="MakeStaticHeader('<%= grdclosingdetails.ClientID %>', 300, '100%' , 30 ,false)"
                                                    Width="100%">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SL" Visible="false">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PRODUCTID" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblproductid" runat="server" Text='<%# Bind("PRODUCTID") %>' value='<%# Eval("PRODUCTID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="PRODUCTNAME" HeaderText="PRODUCTNAME" HeaderStyle-Wrap="false"></asp:BoundField>
                                                        <asp:TemplateField HeaderText="MRP" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblmrp" runat="server" Text='<%# Bind("MRP") %>' value='<%# Eval("MRP") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="BATCHNO" HeaderText="BATCH NO" HeaderStyle-Wrap="true"></asp:BoundField>
                                                        <asp:BoundField DataField="MFGDATE" HeaderText="MFG DATE"></asp:BoundField>
                                                        <asp:BoundField DataField="EXPDATE" HeaderText="EXP DATE"></asp:BoundField>
                                                        <asp:TemplateField HeaderText="PACKSIZEID" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblpacksizeid" runat="server" Text='<%# Bind("PACKSIZEID") %>' value='<%# Eval("PACKSIZEID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="PACKSIZENAME" HeaderText="SIZE"></asp:BoundField>
                                                        <asp:TemplateField HeaderText="OPENING STOCK">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblopeningstk" runat="server" Text='<%# Bind("OPENINGSTK") %>' value='<%# Eval("OPENINGSTK") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CLOSING STOCK">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtclosingstockqty" AutoPostBack="false" runat="server" MaxLength="10"
                                                                    Style="text-align: right;" Width="70px" Height="20" Text='<%# Bind("CLOSINGSTK") %>'
                                                                    value='<%# Eval("CLOSINGSTK") %>' onkeypress="return isNumberKey(event);" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div id="Div4" style="overflow: hidden">
                                            </div>
                                        </fieldset>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                    <cc1:ConfirmBox ID="ConfirmBox1" runat="server" />
                    <cc1:MessageBox ID="MessageBox1" runat="server" />
                </div>
            </div>
            <script type="text/javascript">
                function isNumberKeyWithDot(evt) {
                    var charCode = (evt.which) ? evt.which : event.keyCode;
                    if ((charCode < 48 || charCode > 57) && (charCode != 8) && (charCode != 46))
                        return false;

                    return true;
                }
                function CallServerMethodPrint(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnPrint_", "");
                    document.getElementById("<%=hdn_adjustmentid.ClientID %>").value = gvStockAdjustmentDetails.Rows[iRecordIndex].Cells[0].Value;
                    document.getElementById("<%=Hdn_Print.ClientID %>").value = gvStockAdjustmentDetails.Rows[iRecordIndex].Cells[4].Value;
                    document.getElementById("<%=btnPrint.ClientID %>").click();
                }

                function CallServerMethod(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnGridEdit_", "");
                    document.getElementById("<%=hdn_adjustmentid.ClientID %>").value = gvStockAdjustmentDetails.Rows[iRecordIndex].Cells[0].Value;
                    document.getElementById("<%=btngrdedit.ClientID %>").click();
                }

                function CallDeleteServerMethod(oLink) {
                    var iRecordIndexdelete = oLink.id.toString().replace("btnGridDelete_", "");
                    document.getElementById("<%=hdn_guid.ClientID %>").value = gvadjustment.Rows[iRecordIndexdelete].Cells[0].Value;
                    document.getElementById("<%=btngrddelete.ClientID %>").click();
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
                    gvStockAdjustmentDetails.addFilterCriteria('ADJUSTMENTDATE', OboutGridFilterCriteria.Contains, searchValue);
                    gvStockAdjustmentDetails.addFilterCriteria('ADJUSTMENTNO', OboutGridFilterCriteria.Contains, searchValue);
                    gvStockAdjustmentDetails.addFilterCriteria('DEPOTNAME', OboutGridFilterCriteria.Contains, searchValue);
                    gvStockAdjustmentDetails.executeFilter();
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
