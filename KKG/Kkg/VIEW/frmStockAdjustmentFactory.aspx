<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmStockAdjustmentFactory.aspx.cs" Inherits="VIEW_frmStockAdjustmentFactory" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register Namespace="OboutInc.Calendar2" Assembly="obout_Calendar2_Net" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
            <%--<div class="page_title">
           <span class="title_icon"><span class="computer_imac"></span></span>
              <h3>Openig Stock</h3>
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
                                                    <legend>Product Details</legend>
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tr id="tradjunmentheader" runat="server">
                                                            <td class="field_title" width="70">
                                                                <asp:Label ID="lblqualitycontrol" runat="server" Text="Journal No"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtadjustmentno" runat="server" placeholder="Auto Generate Journal No"
                                                                    Enabled="false" Style="width: 230px;"></asp:TextBox><br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rdbtype" runat="server" AutoPostBack="true" ForeColor="blue" BackColor="AliceBlue"
                                                                    OnSelectedIndexChanged="rdbtype_SelectedIndexChanged"
                                                                    RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="With Stock" Value="G" Selected="True" style="color: #01773c; font-weight: bold;" />
                                                                    <asp:ListItem Text="WithOut Stock" Value="E" style="color: #ec00a2;" />
                                                                </asp:RadioButtonList>
                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td width="70" class="field_title" style="padding-bottom: 24px;">
                                                                <asp:Label ID="lblDeptName" Text="FACTORY" runat="server"></asp:Label>&nbsp;<span
                                                                    class="req">*</span>
                                                            </td>
                                                            <td width="240" class="field_input" style="padding-bottom: 24px;">
                                                                <asp:DropDownList ID="ddlDeptName" Width="230" runat="server" class="chosen-select"
                                                                    data-placeholder="Choose Dept Name" AppendDataBoundItems="True" ValidationGroup="ADD">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfv_ddlDeptName" runat="server" ControlToValidate="ddlDeptName"
                                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="*" ValidationGroup="ADD"
                                                                    ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td width="110" class="field_input">
                                                                <asp:TextBox ID="txtopeningdate" runat="server" Width="65" placeholder="dd/mm/yyyy"
                                                                    MaxLength="10" Enabled="false"></asp:TextBox>
                                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/calendar.png"
                                                                    Width="24px" Height="24px" ImageAlign="AbsMiddle" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtopeningdate"
                                                                    PopupButtonID="ImageButton1" runat="server" Format="dd/MM/yyyy" Enabled="true"
                                                                    BehaviorID="CalendarExtender1" CssClass="cal_Theme1" />
                                                                <span class="label_intro">
                                                                    <asp:Label ID="Label3" runat="server" Text="JORNAL DATE"></asp:Label></span>
                                                            </td>
                                                            <td width="60" class="field_title" style="display:none;">
                                                                <asp:Label ID="lblBrand" Text="PR.ITEM" runat="server"></asp:Label>&nbsp;<span class="req">*</span>
                                                            </td>
                                                            <td width="160" class="field_input" style="display:none;">
                                                                <asp:DropDownList ID="ddlBrand" Width="150" runat="server" class="chosen-select"
                                                                    data-placeholder="Choose Primary Item" ValidationGroup="ADD" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                               <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlBrand"
                                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="*" ValidationGroup="ADD"
                                                                    ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                                            </td>
                                                            <td width="70" class="field_title" style="display:none;">
                                                                <asp:Label ID="lblCategory" Text="SUB ITEM" runat="server"></asp:Label>&nbsp;<span
                                                                    class="req">*</span>
                                                            </td>
                                                            <td class="field_input" width="180" style="display:none;">
                                                                <asp:DropDownList ID="ddlCategory" Width="170" runat="server" class="chosen-select"
                                                                    data-placeholder="Choose Sub Item" ValidationGroup="ADD" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                             <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidatorCategory" runat="server" ControlToValidate="ddlCategory"
                                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="*" ValidationGroup="ADD"
                                                                    ForeColor="Red" InitialValue="0">
                                                                </asp:RequiredFieldValidator><br />--%>
                                                            </td>
                                                            <td>&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field_title" width="70">
                                                                <asp:Label ID="lblProduct" Text="Product" runat="server"></asp:Label>&nbsp;<span
                                                                    class="req">*</span>
                                                            </td>
                                                            <td class="field_input" colspan="2">
                                                                <asp:DropDownList ID="ddlProduct" Width="350" class="chosen-select" runat="server"
                                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged"
                                                                    ValidationGroup="ADD">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfv_ddlProduct" runat="server" ControlToValidate="ddlProduct"
                                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="*" ValidationGroup="ADD"
                                                                    ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td class="field_title">
                                                                <asp:Label ID="lblPackingSize" runat="server" Text="UOM"></asp:Label>&nbsp;<span
                                                                    class="req">*</span>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:DropDownList ID="ddlPackingSize" Width="150" runat="server" class="chosen-select"
                                                                    ValidationGroup="ADD">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfv_ddlPackingSize" runat="server" ControlToValidate="ddlPackingSize"
                                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="*" ValidationGroup="ADD"
                                                                    ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td class="field_title">
                                                                <asp:Label ID="lblstorelocation" Text="Loaction" runat="server"></asp:Label>&nbsp;<span
                                                                    class="req">*</span>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:DropDownList ID="ddlstorelocation" runat="server" AppendDataBoundItems="true"
                                                                    Width="170" class="chosen-select" ValidationGroup="ADD" data-placeholder="Store Loacation" AutoPostBack="True" OnSelectedIndexChanged="ddlstorelocation_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                                                    ValidationGroup="ADD" Font-Bold="true" ForeColor="Red" ControlToValidate="ddlstorelocation"
                                                                    SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
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
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field_title" colspan="7">
                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                    <tr>
                                                                        <td valign="top" style="padding-top: 11px;" width="70">
                                                                            <asp:Label ID="lblStock" Text="DETAILS" runat="server"></asp:Label>&nbsp;<span class="req">*</span>
                                                                        </td>

                                                                        <td valign="top" width="90" style="display:none">
                                                                            <asp:TextBox ID="txtBatchno" runat="server" Width="80" MaxLength="15" placeholder="BATCHNO" Enabled="false"></asp:TextBox>
                                                                            <span class="label_intro">
                                                                                <asp:Label ID="Label6" runat="server" Text="BATCHNO"></asp:Label></span>
                                                                        </td>
                                                                        <td valign="top" width="80">
                                                                            <asp:TextBox ID="txtStockqty" runat="server" Width="70" placeholder="JOURNAL QTY"
                                                                                ValidationGroup="ADD" Text="0" MaxLength="10" onkeypress="return isNumberKeyWithDotMinus(event);"> </asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="rfv_txtStockqty" runat="server" Display="None" ErrorMessage="Journal qty is required!"
                                                                                ControlToValidate="txtStockqty" ValidateEmptyText="false" SetFocusOnError="true "
                                                                                ValidationGroup="ADD"> </asp:RequiredFieldValidator>
                                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                                                TargetControlID="rfv_txtStockqty" PopupPosition="BottomLeft" HighlightCssClass="errormassage"
                                                                                WarningIconImageUrl="~/images/050.png">
                                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                                            <span class="label_intro">
                                                                                <asp:Label ID="Label1" runat="server" Text="JOURNAL QTY"></asp:Label></span>
                                                                        </td>


                                                                        <td valign="top" width="80">
                                                                            <asp:TextBox ID="txtstockinhand" runat="server" Width="70" MaxLength="10" Enabled="false"
                                                                                placeholder="STOCKINHAND" Text="0"></asp:TextBox>
                                                                            <span class="label_intro">
                                                                                <asp:Label ID="lblstockinhand" runat="server" Text="STOCKIN HAND"></asp:Label>
                                                                            </span>
                                                                        </td>

                                                                        
                                                                        <td valign="top" width="50">
                                                                            <asp:TextBox ID="txtmrp" runat="server" Width="40" MaxLength="10" onkeypress="return isNumberKeyWithDot(event);"
                                                                                placeholder="MRP" Text="0" Enabled="false" ValidationGroup="ADD"></asp:TextBox>
                                                                            <span class="label_intro">
                                                                                <asp:Label ID="Label2" runat="server" Text="MRP"></asp:Label>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                                                                    Font-Bold="true" ForeColor="Red" ControlToValidate="txtmrp" SetFocusOnError="true "
                                                                                    ValidationGroup="ADD"> </asp:RequiredFieldValidator></span>
                                                                        </td>
                                                                        
                                                                        <td width="110" valign="top" style="display:none">
                                                                            <asp:TextBox ID="txtmfgdate" runat="server" Width="65" placeholder="dd/mm/yyyy" OnTextChanged="txtmfgdate_TextChanged"
                                                                                AutoPostBack="true" MaxLength="10" ValidationGroup="ADD" onkeypress="return isNumberKeyWithslash(event);"
                                                                                Enabled="false"></asp:TextBox>
                                                                            <asp:ImageButton ID="ImageButtonMFDate" runat="server" ImageUrl="~/images/calendar.png"
                                                                                Width="24px" Height="24px" ImageAlign="AbsMiddle" />
                                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtenderMFDate" TargetControlID="txtmfgdate"
                                                                                PopupButtonID="ImageButtonMFDate" runat="server" Format="dd/MM/yyyy" Enabled="true"
                                                                                BehaviorID="CalendarExtenderMFDate" CssClass="cal_Theme1" />
                                                                            <span class="label_intro">
                                                                                <asp:Label ID="Label4" runat="server" Text="MFG DATE"></asp:Label></span>
                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorMFDate" runat="server"
                                                                                ForeColor="Red" ControlToValidate="txtmfgdate" ValidationGroup="A" ErrorMessage="Invalid"
                                                                                ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$"></asp:RegularExpressionValidator>
                                                                        </td>
                                                                        <td width="110" valign="top" style="display:none">
                                                                            <asp:TextBox ID="txtexpdate" runat="server" Width="65" placeholder="dd/mm/yyyy" MaxLength="10"
                                                                                ValidationGroup="ADD" onkeypress="return isNumberKeyWithslash(event);" Enabled="false"></asp:TextBox>
                                                                            <asp:ImageButton ID="ImageButtonExprDate" runat="server" ImageUrl="~/images/calendar.png"
                                                                                Width="24px" Height="24px" ImageAlign="AbsMiddle" />
                                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtenderExprDate" TargetControlID="txtexpdate"
                                                                                PopupButtonID="ImageButtonExprDate" runat="server" Format="dd/MM/yyyy" Enabled="true"
                                                                                BehaviorID="CalendarExtenderExprDate" CssClass="cal_Theme1" />
                                                                            <span class="label_intro">
                                                                                <asp:Label ID="Label5" runat="server" Text="EXP DATE"></asp:Label></span>
                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorExprDate" runat="server"
                                                                                ForeColor="Red" ControlToValidate="txtexpdate" ValidationGroup="A" ErrorMessage="Invalid"
                                                                                ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$"></asp:RegularExpressionValidator>
                                                                        </td>
                                                                        <td valign="top" width="108" style="display: none;">
                                                                            <asp:TextBox ID="txtRate" runat="server" CssClass="x_large" MaxLength="10" onkeypress="return isNumberKeyWithDot(event);"
                                                                                placeholder="Rate" Text="0" ValidationGroup="ADD"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                                                                                Font-Bold="true" ForeColor="Red" ControlToValidate="txtRate" SetFocusOnError="true "
                                                                                ValidationGroup="ADD"> </asp:RequiredFieldValidator>
                                                                            <span class="label_intro">
                                                                                <asp:Label ID="lblRate" runat="server" Text="RATE"></asp:Label></span>
                                                                        </td>
                                                                        <td valign="top">
                                                                            <%--ValidationGroup="ADD"--%>
                                                                            <asp:DropDownList ID="ddlreason" runat="server" AppendDataBoundItems="true" Style="width: 200px;"
                                                                                class="chosen-select">
                                                                            </asp:DropDownList>
                                                                            <span class="label_intro">
                                                                                <asp:Label ID="Label8" runat="server" Text="REASON"></asp:Label></span>
                                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*Required"
                                                                                Font-Bold="true" ForeColor="Red" ControlToValidate="ddlreason" ValidateEmptyText="false"
                                                                                ValidationGroup="ADD" SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>--%>
                                                                        </td>
                                                                        <td valign="top" style="padding-right: 8px;">
                                                                            <div class="btn_24_blue" ID="divadd" runat="server">
                                                                                <span class="icon blue_block"></span>
                                                                                <asp:Button ID="btnAddStock" runat="server" Text="ADD" CssClass="btn_link" ValidationGroup="ADD"
                                                                                    OnClick="btnAddStock_Click" />
                                                                                <asp:HiddenField ID="hdn_adjustmentid" runat="server" />
                                                                                <asp:HiddenField ID="hdn_mfgdate" runat="server" />
                                                                                <asp:HiddenField ID="hdn_exprdate" runat="server" />
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
                                    <div class="gridcontent-inner">
                                        <fieldset>
                                            <legend>Jornal Stock Details</legend>
                                            <cc1:Grid ID="gvadjustment" runat="server" CallbackMode="true" AutoGenerateColumns="false"
                                                AllowPaging="true" PageSize="500" AllowPageSizeSelection="false" FolderStyle="../GridStyles/premiere_blue"
                                                AllowAddingRecords="false" AllowSorting="false">
                                                <FilteringSettings InitialState="Visible" />
                                                <Columns>
                                                    <cc1:Column ID="Column101" DataField="GUID" ReadOnly="true" HeaderText="GUID" runat="server"
                                                        Visible="false" Width="60" />
                                                    <cc1:Column ID="Column10" DataField="PRODUCTID" ReadOnly="true" HeaderText="ID" runat="server"
                                                        Visible="false" Width="60" />
                                                    <cc1:Column ID="Column40" DataField="Sl" HeaderText="SL" runat="server" Width="60">
                                                        <TemplateSettings TemplateId="slnoTemplate" />
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column50" DataField="PRODUCTNAME" HeaderText="PRODUCT" runat="server"
                                                        Width="350">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column100" DataField="BATCHNO" HeaderText="BATCH NO" runat="server"
                                                        Width="125">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column20" DataField="ADJUSTMENTQTY" HeaderText="JOURNAL QTY" runat="server"
                                                        Width="100" Wrap="true">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column200" DataField="PACKINGSIZENAME" HeaderText="UOM" runat="server"
                                                        Width="60">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column300" DataField="REASONNAME" HeaderText="REASON" runat="server"
                                                        Width="150" Wrap="true">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column900" DataField="REASONID" HeaderText="REASONID" runat="server"
                                                        Visible="false" Width="200" Wrap="true">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column150" DataField="STORELOCATIONNAME" HeaderText="STORE LOCATION"
                                                        runat="server" Width="150" Wrap="true" />
                                                    <cc1:Column ID="Column45" DataField="STORELOCATIONID" HeaderText="STORELOCATIONID"
                                                        runat="server" Visible="false" Width="200" Wrap="true">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column55" DataField="MFDATE" HeaderText="MFDATE" runat="server"
                                                        Width="90" Wrap="true" />
                                                    <cc1:Column ID="Column95" DataField="EXPRDATE" HeaderText="EXPRDATE" runat="server"
                                                        Width="90" Wrap="true" />
                                                    <cc1:Column ID="Column105" DataField="ASSESMENTPERCENTAGE" HeaderText="ASSESMENTPERCENTAGE"
                                                        runat="server" Visible="false" Width="100" Wrap="true" />
                                                    <cc1:Column ID="Column115" DataField="MRP" HeaderText="MRP" runat="server" Visible="false"
                                                        Width="100" Wrap="true" />
                                                    <cc1:Column ID="Column135" DataField="WEIGHT" HeaderText="WEIGHT" runat="server"
                                                        Visible="false" Width="100" Wrap="true" />
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
                                            <asp:Button ID="btngriddelete" runat="server" Text="grddelete" Style="display: none"
                                                OnClick="btngriddelete_Click" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete this record?');" />
                                            <asp:HiddenField ID="hdndtAddStockDelete" runat="server" />
                                            <asp:HiddenField ID="Hdnassementpercentage" runat="server" />
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                                <tr>
                                                    <td class="field_title" width="80">
                                                        <asp:Label ID="Label9" runat="server" Text="TOT.QTY(+ve)"></asp:Label>
                                                    </td>
                                                    <td class="field_input" width="60">
                                                        <asp:TextBox ID="txtPosPCS" runat="server" Width="60" Enabled="false" MaxLength="255"> </asp:TextBox>
                                                    </td>
                                                    <td class="field_title" width="80">
                                                        <asp:Label ID="Label10" runat="server" Text="TOT.QTY(-ve)"></asp:Label>
                                                    </td>
                                                    <td class="field_input" width="60">
                                                        <asp:TextBox ID="txtNegPCS" runat="server" Width="60" Enabled="false" MaxLength="255"> </asp:TextBox>
                                                    </td>
                                                    <td>&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </div>
                                    <div class="gridcontent-inner">
                                        <fieldset>
                                            <legend>Remarks & Save</legend>
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                                <tr>
                                                    <td class="field_title" width="70" style="padding-left: 10px">Remarks
                                                    </td>
                                                    <td class="field_input" width="26%">
                                                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="mid" TextMode="MultiLine" Style="width: 90%; height: 80px"
                                                            MaxLength="255"> </asp:TextBox>
                                                    </td>
                                                    <td class="field_input">
                                                        <div class="btn_24_blue" id="divbtnSubmit" runat="server">
                                                            <span class="icon disk_co"></span>
                                                            <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="btn_link" ValidationGroup="Save"
                                                                OnClick="btnSubmit_Click" />
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
                                                <div class="btn_24_red" id="divbtnreject" runat="server">
                                                    <span class="icon reject_co"></span>
                                                    <asp:LinkButton ID="btnReject" runat="server" Text="Reject" CssClass="btn_link" CausesValidation="false"
                                                        OnClick="btnReject_Click" />
                                                </div>


                                                        <asp:HiddenField runat="server" ID="hdnDepotID" />
                                                        <asp:HiddenField runat="server" ID="hdnbrandid" />
                                                        <asp:HiddenField runat="server" ID="hdncatagoryid" />
                                                        <asp:HiddenField runat="server" ID="hdnlocationid" />
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
                                                                placeholder="dd/mm/yyyy" ValidationGroup="datecheckins" Font-Bold="true"></asp:TextBox>
                                                            <asp:ImageButton ID="imgPopup" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="Calendar1" PopupButtonID="imgPopup" runat="server"
                                                                TargetControlID="txtfromdateins" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                        <td width="70">
                                                            <asp:Label ID="Label18" runat="server" Text="To Date"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="110">
                                                            <asp:TextBox ID="txttodateins" runat="server" MaxLength="10" Width="70" Enabled="false"
                                                                placeholder="dd/mm/yyyy" ValidationGroup="datecheckins" Font-Bold="true"></asp:TextBox>
                                                            <asp:ImageButton ID="ImageButton2" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender4" PopupButtonID="ImageButton2"
                                                                runat="server" TargetControlID="txttodateins" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                            </ajaxToolkit:CalendarExtender>
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
                                    <div class="gridcontent" style="padding-left: 8px;">
                                        <cc1:Grid ID="gvOpenStock" runat="server" CallbackMode="false" Serialize="true" FolderStyle="../GridStyles/premiere_blue"
                                            AutoGenerateColumns="false" AllowPageSizeSelection="true" AllowPaging="true" OnRowDataBound="gvOpenStock_RowDataBound"
                                            PageSize="500" AllowAddingRecords="false" AllowFiltering="true" AllowSorting="false">
                                            <FilteringSettings InitialState="Visible" />
                                            <Columns>
                                                <cc1:Column ID="Column3" DataField="ADJUSTMENTID" HeaderText="ADJUSTMENTID" runat="server"
                                                    Width="60" Visible="false">
                                                </cc1:Column>
                                                <cc1:Column ID="Column1" DataField="Sl" HeaderText="SL" runat="server" Width="60">
                                                    <TemplateSettings TemplateId="slnoTemplateIN" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column21" DataField="ADJUSTMENTDATE" HeaderText="JOURNAL DATE" runat="server"
                                                    Width="150">
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
                                                    Wrap="true" Width="150">
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
                                                <cc1:Column ID="Column24" DataField="DEPOTNAME" HeaderText="FACTORY NAME" runat="server"
                                                    Wrap="true" Width="200">
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

                                                <cc1:Column ID="Column2" DataField="USERNAME" HeaderText="ENTRY USER"
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

                                                <cc1:Column ID="Column4" DataField="ISVERIFIEDDESC" HeaderText="STATUS"
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
                                                    runat="server" Width="80">
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
                                        <asp:Button ID="btngridedit" runat="server" Text="grdedit" Style="display: none"
                                            CausesValidation="false" OnClick="btngridedit_Click" />
                                        <asp:Button ID="btnPrint" runat="server" Text="Print" Style="display: none"
                                            CausesValidation="false" OnClick="btnPrint_Click" />
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                    <cc1:MessageBox ID="MessageBox1" runat="server" />
                </div>
            </div>
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
                function CallServerMethodPrint(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnPrint_", "");
                    document.getElementById("<%=hdn_adjustmentid.ClientID %>").value = gvOpenStock.Rows[iRecordIndex].Cells[0].Value;
                    document.getElementById("<%=Hdn_Print.ClientID %>").value = gvOpenStock.Rows[iRecordIndex].Cells[0].Value;
                    document.getElementById("<%=btnPrint.ClientID %>").click();
                }


                function CallServerMethod(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnGridEdit_", "");
                    document.getElementById("<%=hdn_adjustmentid.ClientID %>").value = gvOpenStock.Rows[iRecordIndex].Cells[0].Value;
                    document.getElementById("<%=btngridedit.ClientID %>").click();
                }

                function CallDeleteServerMethod(oLink) {
                    var iRecordIndexdelete = oLink.id.toString().replace("btnGridDelete_", "");
                    document.getElementById("<%=hdndtAddStockDelete.ClientID %>").value = gvadjustment.Rows[iRecordIndexdelete].Cells[0].Value;
                    document.getElementById("<%=btngriddelete.ClientID %>").click();
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
                    gvOpenStock.addFilterCriteria('BRNAME', OboutGridFilterCriteria.Contains, searchValue);
                    gvOpenStock.addFilterCriteria('BRAND', OboutGridFilterCriteria.Contains, searchValue);
                    gvOpenStock.addFilterCriteria('CATAGORY', OboutGridFilterCriteria.Contains, searchValue);
                    gvOpenStock.executeFilter();
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

                function isNumberKeyWithDotMinus(evt) {
                    var charCode = (evt.which) ? evt.which : event.keyCode;
                    if ((charCode < 48 || charCode > 57) && (charCode != 8) && (charCode != 46) && (charCode != 45))
                        return false;

                    return true;
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>