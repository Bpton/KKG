<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmTaxSheetMaster.aspx.cs" Inherits="VIEW_frmTaxSheetMaster" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


<script type="text/javascript" language="javascript">
    function CheckAllheader(Checkbox) {
        var GridVwHeaderCheckbox = document.getElementById("<%=grdcategorydetails.ClientID %>");
       
        var totalcount = 0;
        if (Checkbox.checked) {
            for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
                GridVwHeaderCheckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
                /*GridVwHeaderCheckbox.rows[i].style.color = '#389638';*/
                GridVwHeaderCheckbox.rows[i].cells[0].style.border = '1px solid #fff';

                totalcount = totalcount + 1;
            }
        }
        else {
            for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
                GridVwHeaderCheckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = false;
                /*GridVwHeaderCheckbox.rows[i].style.color = '#4B555E';*/
                GridVwHeaderCheckbox.rows[i].cells[0].style.border = '0px solid gray';
            }
        }
        
    }
            </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
            <asp:Image ID="Image1" ImageUrl="../images/loading123.gif" AlternateText="Processing"
                runat="server" />
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
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>
                                    Tax Master</h6>
                                <div class="btn_30_light" style="float: right;" id="btnaddhide" runat="server">
                                    <span class="icon add_co"></span>
                                    <asp:Button ID="btnAddTax" runat="server" Text="Add New Record" CssClass="btn_link"
                                        OnClick="btnAddDivision_Click" CausesValidation="false" /></div>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td width="100" class="field_title">
                                                <asp:Label ID="lblName" Text="Name" runat="server"></asp:Label><span class="req">*</span>
                                            </td>
                                            <td class="field_input">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="150">
                                                            <asp:TextBox ID="txtName" runat="server" Width="140"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="CV_Name" runat="server" ErrorMessage="Name is required!"
                                                                ControlToValidate="txtName" ValidateEmptyText="false" Display="None"> </asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                                TargetControlID="CV_Name" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                        </td>
                                                        <td class="innerfield_title" width="80">
                                                            <asp:Label ID="Label6" Text="Description" runat="server"></asp:Label><span class="req">*</span>
                                                        </td>
                                                        <td width="255">
                                                            <asp:TextBox ID="txtdescription" runat="server" Width="245" MaxLength="40"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfv_txtdescription" runat="server" ErrorMessage="Description is required!"
                                                                ControlToValidate="txtdescription" ValidateEmptyText="false" Display="None"> </asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
                                                                TargetControlID="rfv_txtdescription" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                        </td>
                                                        <td width="50" class="innerfield_title">
                                                            <asp:Label ID="lblCode" Text="Code" runat="server"></asp:Label><span class="req">*</span>
                                                        </td>
                                                        <td width="90">
                                                            <asp:TextBox ID="txtCode" runat="server" Width="80"></asp:TextBox>
                                                        </td>
                                                        <td width="85" class="innerfield_title">
                                                            <asp:Label ID="lblPercentage" Text="Percentage" runat="server"></asp:Label><span
                                                                class="req">*</span>
                                                        </td>
                                                        <td width="70">
                                                            <asp:TextBox ID="txtPercentage" runat="server" Width="50" onkeypress="return validateFloatKeyPress(this,event);"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="CV_Percentage" runat="server" ErrorMessage="Percentage is required!"
                                                                ControlToValidate="txtPercentage" ValidateEmptyText="false" Display="None"> </asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                                                TargetControlID="CV_Percentage" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                        </td>
                                                        <td width="85" class="innerfield_title">
                                                            <asp:Label ID="lbleffectivefrom" runat="server" Text="Effective To"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txteffectivefrom" runat="server" Width="70" MaxLength="10" Enabled="false"
                                                                onkeypress="return isNumberKeyWithslash(event);"></asp:TextBox>
                                                            <asp:ImageButton ID="imgPopup" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender4" PopupButtonID="imgPopup" runat="server"
                                                                TargetControlID="txteffectivefrom" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title" style="padding-bottom: 17px;">
                                                <asp:Label ID="Label3" Text="Applied To" runat="server"></asp:Label><span class="req">*</span>
                                            </td>
                                            <td class="field_input">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="150">
                                                            <asp:DropDownList ID="ddlappliedto" runat="server" AppendDataBoundItems="true" Style="width: 145px;"
                                                                class="chosen-select" data-placeholder="Choose a AppliedTo">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Required!"
                                                                Font-Bold="true" ForeColor="Red" ControlToValidate="ddlappliedto" ValidateEmptyText="false"
                                                                SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
                                                        </td>
                                                        <td class="innerfield_title" width="80" style="padding-bottom: 10px;">
                                                            <asp:Label ID="lblrelatedto" Text="Related To" runat="server"></asp:Label><span class="req">*</span>
                                                        </td>
                                                        <td width="255">
                                                            <asp:DropDownList ID="ddlRelatedto" runat="server" AppendDataBoundItems="true" Style="width: 250px;"
                                                                class="chosen-select" data-placeholder="Choose a RelatedTo">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required!"
                                                                Font-Bold="true" ForeColor="Red" ControlToValidate="ddlRelatedto" ValidateEmptyText="false"
                                                                SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
                                                        </td>
                                                        <td class="innerfield_title" width="50" style="padding-bottom: 10px;">
                                                            <asp:Label ID="Label5" Text="Group" runat="server"></asp:Label><span class="req">*</span>
                                                        </td>
                                                        <td style="padding-bottom: 10px;">
                                                            <asp:DropDownList ID="ddlaccountgroup" runat="server" AppendDataBoundItems="true"
                                                                Style="width: 230px;" class="chosen-select" data-placeholder="Choose a AppliedTo">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfv_ddlaccountgroup" runat="server" ErrorMessage="Required!"
                                                                Font-Bold="true" ForeColor="Red" ControlToValidate="ddlaccountgroup" ValidateEmptyText="false"
                                                                SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title">
                                                <asp:Label ID="lblPageName" Text="Menu Option" runat="server"></asp:Label><span class="req">*</span>
                                            </td>
                                            <td class="field_input">
                                                <cc1:Grid ID="gvReasonmap" runat="server" CallbackMode="false" Serialize="true" FolderStyle="../GridStyles/premiere_blue"
                                                    AutoGenerateColumns="false" AllowPageSizeSelection="false" AllowRecordSelection="false"
                                                    EnableRecordHover="true" AllowSorting="false" AllowAddingRecords="false" AllowFiltering="false"
                                                    AllowPaging="false" PageSize="500" OnRowDataBound="gvReasonmap_RowDataBound">
                                                    <Columns>
                                                        <cc1:Column ID="Column1" DataField="DESCRIPTION" HeaderText="MENU" runat="server"
                                                            Width="220">
                                                        </cc1:Column>
                                                        <cc1:Column ID="Column2" DataField="ID" ReadOnly="true" HeaderText="ID" Width="50"
                                                            runat="server">
                                                            <TemplateSettings TemplateId="CheckTemplateTPU" HeaderTemplateId="HeaderTemplateTPU" />
                                                        </cc1:Column>
                                                    </Columns>
                                                    <FilteringSettings MatchingType="AnyFilter" />
                                                    <Templates>
                                                        <cc1:GridTemplate runat="server" ID="CheckTemplateTPU">
                                                            <Template>
                                                                <asp:HiddenField runat="server" ID="hdnTPUName" Value='<%# Container.DataItem["PAGENAME"] %>' />
                                                                <asp:CheckBox ID="ChkIDTPU" runat="server" Text=" " ToolTip="<%# Container.Value %>" />
                                                            </Template>
                                                        </cc1:GridTemplate>
                                                    </Templates>
                                                    <ScrollingSettings ScrollWidth="40%" ScrollHeight="200" />
                                                </cc1:Grid>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title">
                                                <asp:Label ID="lblActive" Text="Active" runat="server"></asp:Label>
                                            </td>
                                            <td class="field_input">
                                                <asp:CheckBox ID="chkActive" runat="server" Text=" " />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td style="padding: 8px 0px;">
                                                <div class="btn_24_blue">
                                                    <span class="icon disk_co"></span>
                                                    <asp:Button ID="btnTaxSubmit" runat="server" Text="Save" CssClass="btn_link" OnClick="btnTaxSubmit_Click" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
                                                    <span class="icon cross_octagon_co"></span>
                                                    <asp:Button ID="btnTaxCancel" runat="server" Text="Cancel" CssClass="btn_link" OnClick="btnTaxCancel_Click"
                                                        CausesValidation="false" />
                                                </div>
                                                <asp:HiddenField ID="Hdn_Fld" runat="server" />
                                                <asp:HiddenField ID="hdnTaxName" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="PlntateWise" runat="server">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                        <tr>
                                            <td width="120" class="field_title">
                                                <asp:Label ID="lblPO" Text="NAME" runat="server"></asp:Label><span class="req">*</span>
                                            </td>
                                            <td class="field_input">
                                                <asp:TextBox ID="txtPname" runat="server" CssClass="large" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="trBSTPU">
                                            <td class="field_input" style="padding-left: 10px;" colspan="2">
                                                <fieldset>
                                                    <legend>State Wise</legend>
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                                        <tr>
                                                            <td valign="top">
                                                                <cc1:Grid ID="gvState" runat="server" CallbackMode="false" Serialize="true" FolderStyle="../GridStyles/premiere_blue"
                                                                    AutoGenerateColumns="false" AllowColumnResizing="false" AllowRecordSelection="false"
                                                                    EnableRecordHover="true" Width="500" AllowAddingRecords="false" AllowFiltering="false"
                                                                    AllowPageSizeSelection="true" PageSize="50" AllowPaging="false" AllowSorting="false"
                                                                    OnRowDataBound="gvState_RowDataBound">
                                                                    <ScrollingSettings ScrollHeight="250" />
                                                                    <FilteringSettings InitialState="Visible" />
                                                                    <Columns>
                                                                        <cc1:Column ID="Column4" DataField="State_Name" HeaderText="State " runat="server"
                                                                            Width="250">
                                                                            <FilterOptions>
                                                                                <cc1:FilterOption Type="NoFilter" />
                                                                                <cc1:FilterOption Type="Contains" />
                                                                                <cc1:FilterOption Type="StartsWith" />
                                                                            </FilterOptions>
                                                                        </cc1:Column>
                                                                        <cc1:Column ID="Column5" HeaderText="% age" runat="server" Width="150">
                                                                            <TemplateSettings TemplateId="TextTemplate" />
                                                                        </cc1:Column>
                                                                        <cc1:Column ID="Column6" DataField="State_ID" ReadOnly="true" HeaderText="ID" runat="server"
                                                                            Visible="false">
                                                                        </cc1:Column>
                                                                    </Columns>
                                                                    <FilteringSettings MatchingType="AnyFilter" />
                                                                    <Templates>
                                                                        <cc1:GridTemplate runat="server" ID="TextTemplate">
                                                                            <Template>
                                                                                <asp:HiddenField runat="server" ID="SName" Value='<%# Container.DataItem["State_ID"] %>' />
                                                                                <asp:TextBox ID="TxtperAge" runat="server" ToolTip="<%# Container.Value %>" onkeypress="return isNumberKeyWithDot(event);" />
                                                                            </Template>
                                                                        </cc1:GridTemplate>
                                                                    </Templates>
                                                                    <ScrollingSettings ScrollHeight="200" />
                                                                </cc1:Grid>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="trbtnStatesave">
                                            <td colspan="2" class="field_input" style="padding-left: 10px;">
                                                <div class="btn_24_blue">
                                                    <span class="icon disk_co"></span>
                                                    <asp:Button ID="btnStateSave" runat="server" Text="Save" CssClass="btn_link" CausesValidation="false"
                                                        OnClick="btnStateSave_Click" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
                                                    <span class="icon cross_octagon_co"></span>
                                                    <asp:Button ID="btnStateCancel" runat="server" Text="Cancel" CssClass="btn_link"
                                                        CausesValidation="false" OnClick="btnStateCancel_Click" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="plnException" runat="server">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                        <tr>
                                            <td width="120" class="field_title">
                                                <asp:Label ID="Label1" Text="NAME" runat="server"></asp:Label><span class="req">*</span>
                                            </td>
                                            <td class="field_input">
                                                <asp:TextBox ID="txtExceptionName" runat="server" CssClass="large" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="tr1">
                                            <td class="field_input" style="padding-left: 10px;" colspan="2">
                                                <fieldset>
                                                    <legend>Exception</legend>
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                                        <tr>
                                                            <td class="innerfield_title">
                                                                <asp:RadioButtonList ID="rdbList" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"
                                                                    OnSelectedIndexChanged="rdbList_SelectedIndexChanged" Width="90%" AutoPostBack="true"
                                                                    RepeatColumns="10">
                                                                </asp:RadioButtonList>
                                                                <asp:RequiredFieldValidator ID="rfv_rdbList" runat="server" ErrorMessage=" Required!"
                                                                    ControlToValidate="rdbList" ValidateEmptyText="false" Display="None" ValidationGroup='A'> 
                                                                </asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server"
                                                                    TargetControlID="rfv_rdbList" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="../images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                                    <tr>
                                                                        <td width="270">
                                                                            <asp:DropDownList ID="ddlexception" runat="server" Style="width: 250px;" class="chosen-select"
                                                                                data-placeholder="Choose an Item" ValidationGroup='A'>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="rfv_ddlexception" runat="server" ValidationGroup='A'
                                                                                ErrorMessage="*" Font-Bold="true" ForeColor="Red" ControlToValidate="ddlexception"
                                                                                ValidateEmptyText="false" SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
                                                                        </td>
                                                                        <td width="160">
                                                                            <asp:DropDownList ID="ddlordertype" runat="server" Width="150px" Height="28px" class="chosen-select"
                                                                                data-placeholder="SELECT ORDER TYPE">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td width="90">
                                                                            <asp:TextBox ID="txtexception" runat="server" Width="70" placeholder="Enter (%)Age"
                                                                                ValidationGroup='A' onkeypress="return validateFloatKeyPress(this,event);"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="rfv_txtexception" runat="server" ErrorMessage="*"
                                                                                ForeColor="Red" Font-Bold="true" ControlToValidate="txtexception" ValidateEmptyText="false"
                                                                                ValidationGroup='A'> 
                                                                            </asp:RequiredFieldValidator>
                                                                        </td>
                                                                        <td>
                                                                            <div class="btn_24_blue">
                                                                                <span class="icon add_co"></span>
                                                                                <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn_link" ValidationGroup='A'
                                                                                    OnClick="btnAdd_Click" />
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <cc1:Grid ID="gvexception" runat="server" CallbackMode="false" Serialize="true" FolderStyle="../GridStyles/premiere_blue"
                                                                                    AutoGenerateColumns="false" AllowColumnResizing="false" AllowRecordSelection="false"
                                                                                    EnableRecordHover="true" Width="500" AllowAddingRecords="false" AllowFiltering="false"
                                                                                    AllowPageSizeSelection="false" AllowPaging="false" AllowSorting="false" PageSize="500">
                                                                                    <FilteringSettings InitialState="Visible" />
                                                                                    <Columns>
                                                                                        <cc1:Column ID="GUID" DataField="VENDORID" HeaderText="GUID " runat="server" Width="80"
                                                                                            Visible="false">
                                                                                        </cc1:Column>
                                                                                        <cc1:Column ID="Column7" DataField="VENDORNAME" HeaderText="NAME " runat="server"
                                                                                            Width="30%">
                                                                                            <FilterOptions>
                                                                                                <cc1:FilterOption Type="NoFilter" />
                                                                                                <cc1:FilterOption Type="Contains" />
                                                                                                <cc1:FilterOption Type="StartsWith" />
                                                                                            </FilterOptions>
                                                                                        </cc1:Column>
                                                                                        <cc1:Column ID="Column16" DataField="ORDERTYPENAME" HeaderText="ORDER TYPE" runat="server"
                                                                                            Width="150">
                                                                                        </cc1:Column>
                                                                                        <cc1:Column ID="Column8" DataField="PERCENTAGE" HeaderText="% AGE" runat="server"
                                                                                            Width="100">
                                                                                        </cc1:Column>
                                                                                        <cc1:Column HeaderText="DELETE" AllowEdit="false" AllowDelete="true" Width="80">
                                                                                            <TemplateSettings TemplateId="deleteBtnTemplate1" />
                                                                                        </cc1:Column>
                                                                                    </Columns>
                                                                                    <FilteringSettings MatchingType="AnyFilter" />
                                                                                    <Templates>
                                                                                        <cc1:GridTemplate runat="server" ID="deleteBtnTemplate1">
                                                                                            <Template>
                                                                                                <a href="javascript: //" class="action-icons c-delete" id="btnGridDelete_<%# Container.PageRecordIndex %>"
                                                                                                    onclick="CallDeleteServerMethod(this)"></a>
                                                                                            </Template>
                                                                                        </cc1:GridTemplate>
                                                                                    </Templates>
                                                                                    <ScrollingSettings ScrollWidth="70%" ScrollHeight="220" />
                                                                                </cc1:Grid>
                                                                                <asp:Button ID="btngriddelete" runat="server" Text="griddelete" Style="display: none"
                                                                                    OnClick="btngriddelete_Click" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete this record?');" />
                                                                                <asp:HiddenField ID="hdndLoacationDelete" runat="server" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="tr2">
                                            <td colspan="2" class="field_input" style="padding-left: 10px;">
                                                <div class="btn_24_blue">
                                                    <span class="icon disk_co"></span>
                                                    <asp:Button ID="btnSaveEcetion" runat="server" Text="Save" CssClass="btn_link" CausesValidation="false"
                                                        OnClick="btnSaveEcetion_Click" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
                                                    <span class="icon cross_octagon_co"></span>
                                                    <asp:Button ID="Button2" runat="server" Text="Cancel" CssClass="btn_link" CausesValidation="false"
                                                        OnClick="btnStateCancel_Click" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="PlnProduct" runat="server">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                        <tr>
                                            <td width="80" class="field_title">
                                                <asp:Label ID="Label2" Text="NAME" runat="server"></asp:Label><span class="req">*</span>
                                            </td>
                                            <td class="field_input" colspan="6">
                                                <asp:TextBox ID="TxtPTaxname" runat="server" CssClass="large" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7" class="field_title">
                                                <asp:RadioButtonList ID="rdbExpProduct" runat="server" RepeatDirection="Horizontal"
                                                    RepeatColumns="9" RepeatLayout="Flow" Width="90%" OnSelectedIndexChanged="rdbExpProduct_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title" width="80" id="tdlblothers" runat="server">
                                                <asp:Label ID="Label10" runat="server" Text="For Others"></asp:Label>
                                                <span class="req">*</span>
                                            </td>
                                            <td colspan="7" class="field_title" id="tdother" runat="server" style="padding-bottom: 20px;">
                                                <asp:RadioButtonList ID="rdbbtn" runat="server" RepeatDirection="Horizontal" Width="10%"
                                                    OnSelectedIndexChanged="rdbbtn_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Text="RM-PM" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="FG" Value="1"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <tr>
                                                <td class="field_title" width="80">
                                                    <asp:Label ID="Label4" runat="server" Text="Type"></asp:Label>
                                                    <span class="req">*</span>
                                                </td>
                                                <td class="field_input" width="260">
                                                    <asp:DropDownList ID="ddlExpProduct" runat="server" class="chosen-select" data-placeholder="Choose an Item"
                                                        ValidationGroup="P" Style="width: 250px;">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="field_title" width="80">
                                                    <asp:Label ID="lblDiv" Text="Brand" runat="server"></asp:Label><span class="req">*</span>
                                                </td>
                                                <td class="field_input" width="180">
                                                    <asp:DropDownList ID="ddlDivision" runat="server" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"
                                                        ValidationGroup="P" AutoPostBack="true" AppendDataBoundItems="true" class="chosen-select"
                                                        Style="width: 180px;" data-placeholder="Choose a Brand">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="field_title" width="70">
                                                    <asp:Label ID="lblCat" Text="Category" runat="server"></asp:Label><span class="req">*</span>
                                                </td>
                                                <td class="field_input" width="200">
                                                    <asp:DropDownList ID="ddlCategory" runat="server" AppendDataBoundItems="true" class="chosen-select"
                                                        ValidationGroup="P" Style="width: 200px;" data-placeholder="Choose a Category"
                                                        OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <div class="btn_24_blue">
                                                        <span class="icon exclamation_co"></span>
                                                        <asp:Button ID="btnProShow" runat="server" Text="Show" CausesValidation="false" CssClass="btn_link"
                                                            ValidationGroup="P" OnClick="btnProShow_Click" />
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="tr3">
                                                <td class="field_input" style="padding-left: 10px;" colspan="7">
                                                    <fieldset>
                                                        <legend>Product</legend>
                                                        <table border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                                            <tr>
                                                                <td valign="top">
                                                                    <cc1:Grid ID="grdProduct" runat="server" CallbackMode="false" Serialize="true" FolderStyle="../GridStyles/premiere_blue"
                                                                        AutoGenerateColumns="false" AllowColumnResizing="false" AllowRecordSelection="false"
                                                                        EnableRecordHover="true" Width="50%" AllowAddingRecords="false" AllowFiltering="false"
                                                                        AllowPageSizeSelection="true" AllowPaging="false" PageSize="100" AllowSorting="false"
                                                                        OnRowDataBound="grdProduct_RowDataBound">
                                                                        <FilteringSettings InitialState="Visible" />
                                                                        <Columns>
                                                                            <cc1:Column ID="Column9" DataField="NAME" HeaderText="PRODUCT NAME " runat="server"
                                                                                Wrap="true" Width="100%">
                                                                                <FilterOptions>
                                                                                    <cc1:FilterOption Type="NoFilter" />
                                                                                    <cc1:FilterOption Type="Contains" />
                                                                                    <cc1:FilterOption Type="StartsWith" />
                                                                                </FilterOptions>
                                                                            </cc1:Column>
                                                                            <cc1:Column ID="Column10" HeaderText="% age" runat="server" Width="40%">
                                                                                <TemplateSettings TemplateId="TextTemplatePro" />
                                                                            </cc1:Column>
                                                                            <cc1:Column ID="Column11" DataField="ID" ReadOnly="true" HeaderText="ID" runat="server"
                                                                                Visible="false">
                                                                            </cc1:Column>
                                                                        </Columns>
                                                                        <FilteringSettings MatchingType="AnyFilter" />
                                                                        <Templates>
                                                                            <cc1:GridTemplate runat="server" ID="TextTemplatePro">
                                                                                <Template>
                                                                                    <asp:HiddenField runat="server" ID="SName" Value='<%# Container.DataItem["ID"] %>' />
                                                                                    <asp:TextBox ID="TxtperAgeProduct" runat="server" ToolTip="<%# Container.Value %>"
                                                                                        onkeypress="return isNumberKeyWithDot(event);" />
                                                                                </Template>
                                                                            </cc1:GridTemplate>
                                                                        </Templates>
                                                                        <ScrollingSettings ScrollWidth="50%" ScrollHeight="220" />
                                                                    </cc1:Grid>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </fieldset>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="tr4">
                                                <td colspan="7" class="field_input" style="padding-left: 10px;">
                                                    <div class="btn_24_blue">
                                                        <span class="icon disk_co"></span>
                                                        <asp:Button ID="BtnProSave" runat="server" Text="Save" CssClass="btn_link" CausesValidation="false"
                                                            OnClick="BtnProSave_Click" />
                                                    </div>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                    <div class="btn_24_blue">
                                                        <span class="icon cross_octagon_co"></span>
                                                        <asp:Button ID="BtnProCancel" runat="server" Text="Cancel" CssClass="btn_link" CausesValidation="false"
                                                            OnClick="btnStateCancel_Click" />
                                                    </div>
                                                </td>
                                            </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlcategory" runat="server">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                        <tr>
                                            <td class="field_title" width="120">
                                                <asp:Label ID="Label7" runat="server" Text="Tax Name"></asp:Label>
                                            </td>
                                            <td class="field_input">
                                                <asp:TextBox ID="txtopentaxname" runat="server" Width="400" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="130" class="field_title">
                                                <asp:Label ID="Label16" runat="server" Text="From Date"></asp:Label>
                                                <span class="req">*</span>
                                            </td>
                                            <td width="230" class="field_input">
                                                <asp:TextBox ID="txtFromDate1" runat="server" MaxLength="10" Width="120" Enabled="false"
                                                    placeholder="dd/MM/yyyy" onkeypress="return isNumberKeyWithslash(event);" 
                                                    Font-Bold="true"></asp:TextBox>
                                                <asp:ImageButton ID="ImageButton111" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                    runat="server" Height="24" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarFromDate" PopupButtonID="ImageButton111" runat="server"
                                                    TargetControlID="txtFromDate1" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                </ajaxToolkit:CalendarExtender>
                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorFromDate" runat="server" ControlToValidate="txtFromDate"
                                                    SetFocusOnError="true" ErrorMessage="Required!" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                            <%--</td>--%>
                                            <%--<td width="130" class="field_title">--%>
                                                <asp:Label ID="Label19" runat="server" Text="To Date" Font-Bold="true" ></asp:Label>
                                                <span class="req">*</span>
                                            <%--</td>--%>
                                            <%--<td width="255" class="field_input">--%>
                                                <asp:TextBox ID="txtToDate1" runat="server" MaxLength="10" Width="120" Enabled="false"
                                                    placeholder="dd/mm/yyyy" onkeypress="return isNumberKeyWithslash(event);" 
                                                    Font-Bold="true"></asp:TextBox>
                                                <asp:ImageButton ID="imgPopuptodate1" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                    runat="server" Height="24" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtenderToDate" PopupButtonID="imgPopuptodate1"
                                                    runat="server" TargetControlID="txtToDate1" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                </ajaxToolkit:CalendarExtender>
                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorToDate" runat="server" ControlToValidate="txtToDate"
                                                    SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="Search" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                            </td>
                                            <td class="field_input">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="gridcontent-inner">
                                        <fieldset>
                                            <legend>Category/SubItemwise Tax Details</legend>
                                            <div style="height: 300px; width: 58%; overflow: scroll;">
                                                <asp:GridView ID="grdcategorydetails" EmptyDataText="There are no records available."
                                                    CssClass="zebra" runat="server" AutoGenerateColumns="False" Width="100%" ShowHeader="true">
                                                    <Columns>

                                                       <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            <asp:CheckBox ID="chkAllSelect" runat="server" onclick="CheckAllheader(this);" Text=" " />
                                                                        </HeaderTemplate>
                                                                        <HeaderStyle Width="5px" />
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkSelect" runat="server" Text=" " class="round" Style="padding-left: 15px;"
                                                                            ToolTip='<%# Bind("CATID") %>'  />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="SL" HeaderStyle-Width="20">
                                                            <ItemTemplate>
                                                                <nav style="position: relative;"><span class="badge black"><%# Container.DataItemIndex + 1 %></span></nav>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CategoryID" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcatid" runat="server" Text='<%# Bind("CATID") %>' value='<%# Eval("CATID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="CATNAME" HeaderText="CATEGORY / SUBITEM" HeaderStyle-Wrap="false"
                                                            ItemStyle-Wrap="false"></asp:BoundField>
                                                        <asp:BoundField DataField="HSN" HeaderText="HSN/HSE" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                                            HeaderStyle-Width="60"></asp:BoundField>
                                                        <asp:TemplateField HeaderText="PERCENT %" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="65">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtpercentage" runat="server" Text='<%# Bind("PERCENTAGE") %>' Width="50"
                                                                    Height="20" Style="text-align: right;" />
                                                                <label>
                                                                    %</label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="FROM DATE" HeaderStyle-Width="150px">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="grdtxtfromdate" AutoPostBack="false" runat="server" MaxLength="10"
                                                                    Style="text-align: right;" Width="70px" Height="20" Enabled="false" Text='<%# Bind("FROMDATE") %>'/>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TO DATE" HeaderStyle-Width="150px">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="grdtxttodate" AutoPostBack="false" runat="server" MaxLength="10"
                                                                    Style="text-align: right;" Width="70px" Height="20" Enabled="false"  Text='<%# Bind("TODATE") %>'/>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </fieldset>
                                    </div>
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                        <tr>
                                            <td style="padding-left: 10px;" class="field_input">
                                                <div class="btn_24_blue">
                                                    <span class="icon disk_co"></span>
                                                    <asp:Button ID="btncategoryMappingsubmit" runat="server" Text="Save" CssClass="btn_link"
                                                        CausesValidation="false" OnClick="btncategoryMappingsubmit_Click" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
                                                    <span class="icon cross_octagon_co"></span>
                                                    <asp:Button ID="btncategoryMappingcancel" runat="server" Text="Cancel" CssClass="btn_link"
                                                        CausesValidation="false" OnClick="btncategoryMappingcancel_Click" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlsalvemapping" runat="server">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                        <tr>
                                            <td class="field_title" width="120">
                                                <asp:Label ID="lbltaxname" runat="server" Text="Tax Name"></asp:Label>
                                            </td>
                                            <td class="field_input">
                                                <asp:TextBox ID="txttaxname" runat="server" CssClass="large" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" class="field_input" style="padding-left: 10px;">
                                                <fieldset>
                                                    <legend>SlabWiseTax Details</legend>
                                                    <table border="0" cellspacing="0" cellpadding="0" class="form_container_td">

                                                        <tr>
                                                        <td>
                                                        <table border="0" cellspacing="0" cellpadding="0">
                                                             <td class="field_input" width="150">
                                                                <asp:DropDownList ID="ddlledger" runat="server" AppendDataBoundItems="true"
                                                                    class="chosen-select" ValidationGroup="ADD" Style="width: 150px;">
                                                                </asp:DropDownList>
                                                                <span class="label_intro">LEDGER                                                                  
                                                                </span>
                                                            </td>
                                                             <td class="field_input" width="150">
                                                                <asp:DropDownList ID="ddlsupplieditem" runat="server" AppendDataBoundItems="true"
                                                                    class="chosen-select" ValidationGroup="ADD" Style="width: 150px;" data-placeholder="Choose a Category">
                                                                </asp:DropDownList>
                                                                <span class="label_intro">TYPE
                                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="ADD"
                                                                        ForeColor="Red" ErrorMessage="* Req" ControlToValidate="ddlsupplieditem" ValidateEmptyText="false"
                                                                        SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>--%>
                                                                </span>
                                                            </td>

                                                             <td width="120" class="field_input">
                                                            <asp:TextBox ID="txtfromdate" runat="server" Enabled="false" Width="85" placeholder="dd/MM/yyyy"
                                                                MaxLength="10"></asp:TextBox>
                                                            <asp:ImageButton ID="Imgfrom" runat="server" ImageUrl="~/images/calendar.png" Width="24px"
                                                                Height="24px" ImageAlign="AbsMiddle" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="Imgfrom" runat="server"
                                                                TargetControlID="txtfromdate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <span class="label_intro">FROM DATE</span>
                                                        </td>
                                                        <td width="120" class="field_input">
                                                            <asp:TextBox ID="txttodate" runat="server" Enabled="false" Width="85" placeholder="dd/MM/yyyy"
                                                                MaxLength="10"></asp:TextBox>
                                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/calendar.png" Width="24px"
                                                                Height="24px" ImageAlign="AbsMiddle" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImageButton1" runat="server"
                                                                TargetControlID="txttodate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <span class="label_intro">TO DATE</span>
                                                        </td>
                                                             
                                                             
                                                        </table>
                                                        </td>
                                                        </tr>
                                                        <tr>
                                                        <td>
                                                        <table border="0" cellspacing="0" cellpadding="0">

                                                        <td class="field_input" width="100">
                                                                <asp:TextBox ID="txtfromamount" runat="server" Width="90" onkeypress="return isNumberKeyWithDot(event);"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfv_txtfromamount" ValidationGroup="ADD" runat="server"
                                                                    Display="None" ErrorMessage="From Amount is required!" ControlToValidate="txtfromamount"
                                                                    ValidateEmptyText="false" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                                                    TargetControlID="rfv_txtfromamount" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="../images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                                <span class="label_intro">FROM AMOUNT</span>
                                                            </td>
                                                            <td class="field_input" width="100">
                                                                <asp:TextBox ID="txttoamount" runat="server" Width="90" onkeypress="return isNumberKeyWithDot(event);"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfv_txttoamount" ValidationGroup="ADD" runat="server"
                                                                    Display="None" ErrorMessage="From Amount is required!" ControlToValidate="txttoamount"
                                                                    ValidateEmptyText="false" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
                                                                    TargetControlID="rfv_txttoamount" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="../images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                                <span class="label_intro">TO AMOUNT</span>
                                                            </td>
                                                        <td class="field_input" width="70">
                                                        <asp:Label ID="Label11" Text="SLAB WISE" runat="server"></asp:Label>
                                                                <%--SLAB WISE<span class="label_intro">&nbsp;</span>--%>
                                                            </td>
                                                            <td class="field_input" width="100">
                                                                <asp:TextBox ID="txtvalue" runat="server" Width="90" Text="0" onkeypress="return isNumberKeyWithDot(event);"
                                                                    OnClientClick="CalcualteTotalAmount();">
                                                                </asp:TextBox>
                                                                <span class="label_intro">VALUE</span>
                                                            </td>
                                                            <td class="field_input" width="40">
                                                                OR<span class="label_intro">&nbsp;</span>
                                                            </td>
                                                            <td class="field_input" width="70">
                                                                <asp:TextBox ID="txtslbpercentage" Text="0" runat="server" Width="60" onkeypress="return isNumberKeyWithDot(event);"
                                                                    OnClientClick="CalcualteTotalAmount();">
                                                                </asp:TextBox>
                                                                <span class="label_intro">PECENTAGE</span>
                                                            </td>
                                                             <td class="field_input" width="100">
                                                                <asp:TextBox ID="txtaddninfo" runat="server" Width="200" TextMode="MultiLine">
                                                                </asp:TextBox>
                                                                <span class="label_intro">ADDITIONAL INFO</span>
                                                            </td>
                                                            <td>
                                                                <div class="btn_24_blue">
                                                                    <span class="icon add_co"></span>
                                                                    <asp:Button ID="btnSlabAdd" runat="server" Text="ADD" CssClass="btn_link" ValidationGroup="ADD"
                                                                        OnClick="btnSlabAdd_Click" />
                                                                </div>
                                                                <span class="label_intro">&nbsp;</span>
                                                            </td>
                                                        </table>
                                                        </td>                                                            
                                                            
                                                        </tr>
                                                        <tr>
                                                            <td colspan="8">
                                                                <cc1:Grid ID="gvSlabwisetaxmapping" runat="server" Serialize="true" AllowAddingRecords="false"
                                                                    AutoGenerateColumns="false" AllowSorting="true" AllowPageSizeSelection="false"
                                                                    EnableRecordHover="true" FolderStyle="../GridStyles/premiere_blue" AllowPaging="false"
                                                                    PageSize="100">
                                                                    <Columns>
                                                                        <cc1:Column ID="Column31" DataField="SlNo" HeaderText="SL" runat="server" Width="60px">
                                                                            <TemplateSettings TemplateId="slnoAddress" />
                                                                        </cc1:Column>
                                                                        <cc1:Column ID="Column32" DataField="GUID" HeaderText="GUID" runat="server" Visible="false" />
                                                                        <cc1:Column ID="Column33" DataField="TAXID" HeaderText="TAXID" Visible="false" runat="server">
                                                                        </cc1:Column>
                                                                        <cc1:Column ID="Column13" DataField="TAXNAME" HeaderText="TAXNAME" Visible="false" runat="server">
                                                                        </cc1:Column>
                                                                        <cc1:Column ID="Column15" DataField="TYPEID" HeaderText="TYPEID" Visible="false"
                                                                            runat="server">
                                                                        </cc1:Column>
                                                                        <cc1:Column ID="Column17" DataField="TYPENAME" HeaderText="TYPE" runat="server">
                                                                        </cc1:Column>
                                                                         <cc1:Column ID="Column19" DataField="LEDGERID" HeaderText="LEDGERID" runat="server" Visible=false>
                                                                        </cc1:Column>
                                                                        <cc1:Column ID="Column20" DataField="LEDGERNAME" HeaderText="LEDGER" runat="server">
                                                                        </cc1:Column>
                                                                        <cc1:Column ID="Column34" DataField="FROMAMOUNT" HeaderText="FROM AMOUNT" runat="server">
                                                                        </cc1:Column>
                                                                        <cc1:Column ID="Column35" DataField="TOAMOUNT" HeaderText="TO AMOUNT" runat="server">
                                                                        </cc1:Column>
                                                                        <cc1:Column ID="Column22" DataField="FROMDATE" HeaderText="FROM DATE" runat="server">
                                                                        </cc1:Column>
                                                                        <cc1:Column ID="Column23" DataField="TODATE" HeaderText="TO DATE" runat="server">
                                                                        </cc1:Column>
                                                                        <cc1:Column ID="Column36" DataField="VALUE" HeaderText="VALUE" Width="100" runat="server">
                                                                        </cc1:Column>
                                                                        <cc1:Column ID="Column14" DataField="PERCENTAGE" HeaderText="PERCENTAGE" Width="100" runat="server">
                                                                        </cc1:Column>
                                                                        <cc1:Column ID="Column21" DataField="ADDITIONALINFO" HeaderText="ADDITIONAL INFO" Width="100" runat="server">
                                                                        </cc1:Column>
                                                                        <cc1:Column HeaderText="DELETE" AllowEdit="false" AllowDelete="true" Width="140" Wrap="true">
                                                                            <TemplateSettings TemplateId="deleteBtnSlabwisetaxmapping" />
                                                                        </cc1:Column>
                                                                    </Columns>
                                                                    <Templates>
                                                                        <cc1:GridTemplate runat="server" ID="deleteBtnSlabwisetaxmapping">
                                                                            <Template>
                                                                                <a href="javascript: //" class="action-icons c-delete" id="btnGridDelete_<%# Container.PageRecordIndex %>"
                                                                                    onclick="DeleteSlabwisetaxmapping(this)"></a>
                                                                            </Template>
                                                                        </cc1:GridTemplate>
                                                                        <cc1:GridTemplate runat="server" ID="slnoAddress">
                                                                            <Template>
                                                                                <asp:Label ID="lblslno" runat="server" Text='<%#Container.PageRecordIndex+1 %>'></asp:Label>
                                                                            </Template>
                                                                        </cc1:GridTemplate>
                                                                    </Templates>
                                                                    <ScrollingSettings ScrollWidth="65%" ScrollHeight="180" />
                                                                </cc1:Grid>
                                                                <asp:Button ID="btndelete" runat="server" CausesValidation="false" Text="grddelete"
                                                                    Style="display: none" OnClick="btndelete_Click" OnClientClick="return confirm('Are you sure you want to delete this record?');" />
                                                                <asp:HiddenField ID="hdn_Slabtax" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding-left: 10px;" colspan="8" class="field_input">
                                                                <div class="btn_24_blue">
                                                                    <span class="icon disk_co"></span>
                                                                    <asp:Button ID="btnSlabMappingSubmit" runat="server" Text="Save" CssClass="btn_link"
                                                                        CausesValidation="false" OnClick="btnSlabMappingSubmit_Click" />
                                                                </div>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                                <div class="btn_24_blue">
                                                                    <span class="icon cross_octagon_co"></span>
                                                                    <asp:Button ID="btnSlabMappingCancel" runat="server" Text="Cancel" CssClass="btn_link"
                                                                        CausesValidation="false" OnClick="btnSlabMappingCancel_Click" />
                                                                </div>
                                                            </td>
                                                        </tr>
                                        </tr>
                                    </table>
                                    </fieldset> </td> </tr> </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlvendorgroupmapping" runat="server">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                        <tr>
                                            <td class="field_title" width="120">
                                                <asp:Label ID="Label8" runat="server" Text="Tax Name"></asp:Label>
                                            </td>
                                            <td class="field_input" width="300">
                                                <asp:TextBox ID="txttaxnamegroup" runat="server" Width="300" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td class="field_title" width="60">
                                                <asp:Label ID="Label9" runat="server" Text="TYPE"></asp:Label>
                                            </td>
                                            <td class="field_input" width="100">
                                                <asp:DropDownList ID="ddlgrouptype" runat="server" AppendDataBoundItems="true" class="chosen-select"
                                                    Style="width: 100px;" data-placeholder="Choose a Type" AutoPostBack="true" OnSelectedIndexChanged="ddlgrouptype_SelectedIndexChanged">
                                                    <asp:ListItem Text="Vendor" Value="V"></asp:ListItem>
                                                    <asp:ListItem Text="Transporter" Value="T"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="gridcontent-inner">
                                        <fieldset>
                                            <legend>Vendor Group Tax Details</legend>
                                            <div style="height: 300px; width: 50%; overflow: scroll;">
                                                <asp:GridView ID="grdvendormapping" EmptyDataText="There are no records available."
                                                    CssClass="zebra" runat="server" AutoGenerateColumns="False" Width="100%" ShowHeader="true">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SL" HeaderStyle-Width="20">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="GROUPID" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgroupid" runat="server" Text='<%# Bind("GROUPID") %>' value='<%# Eval("GROUPID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="GROUPNAME" HeaderText="GROUP" HeaderStyle-Wrap="false"
                                                            ItemStyle-Wrap="false"></asp:BoundField>
                                                        <asp:TemplateField HeaderText="PERCENT %" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="60">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgrouppercentage" runat="server" Text='<%# Bind("PERCENTAGE") %>'
                                                                    Width="50" Height="20" Style="text-align: right;" />
                                                                <label>
                                                                    %</label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </fieldset>
                                    </div>
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                        <tr>
                                            <td style="padding-left: 10px;" class="field_input">
                                                <div class="btn_24_blue">
                                                    <span class="icon disk_co"></span>
                                                    <asp:Button ID="btngroupsubmit" runat="server" Text="Save" CssClass="btn_link" CausesValidation="false"
                                                        OnClick="btngroupsubmit_Click" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
                                                    <span class="icon cross_octagon_co"></span>
                                                    <asp:Button ID="btngroupcancel" runat="server" Text="Cancel" CssClass="btn_link"
                                                        CausesValidation="false" OnClick="btngroupcancel_Click" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlDisplay" runat="server">
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
                                        <cc1:Grid ID="gvPercentage" runat="server" CallbackMode="true" Serialize="true" FolderStyle="../GridStyles/premiere_blue"
                                            AutoGenerateColumns="false" AllowPageSizeSelection="false" OnDeleteCommand="DeleteRecord"
                                            EnableRecordHover="true" PageSize="100" AllowAddingRecords="false" AllowFiltering="true"
                                            OnRowDataBound="gvPercentage_RowDataBound">
                                            <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnClientDelete="OnDelete" />
                                            <FilteringSettings InitialState="Visible" />
                                            <Columns>
                                                <cc1:Column DataField="ID" ReadOnly="true" HeaderText="ID" runat="server" Visible="false" />
                                                <cc1:Column ID="Column18" DataField="SlNo" HeaderText="SL" runat="server" Width="50px">
                                                    <TemplateSettings TemplateId="slno" />
                                                </cc1:Column>
                                                <cc1:Column DataField="NAME" HeaderText="TAX NAME" runat="server" Width="230" Wrap="true">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column DataField="PERCENTAGE" HeaderText="PERCENT(%)" runat="server" Width="100"
                                                    ItemStyle-HorizontalAlign="Right">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column3" DataField="APPLICABLETO" HeaderText="APPLIED TO" runat="server"
                                                    Width="110" Wrap="true">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column12" DataField="RELATEDTO" HeaderText="RELATED TO" runat="server"
                                                    Width="200" Wrap="true">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column DataField="ACTIVE" HeaderText="STATUS" runat="server" Width="80">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column HeaderText="STATE WISE MAPPING" AllowEdit="false" AllowDelete="true"
                                                    Width="85" Wrap="true">
                                                    <TemplateSettings TemplateId="MappingTemplate" />
                                                </cc1:Column>
                                                <cc1:Column HeaderText="EXCEPTION MAPPING" AllowEdit="false" AllowDelete="true" Width="80"
                                                    Wrap="true">
                                                    <TemplateSettings TemplateId="MappingTemplateException" />
                                                </cc1:Column>
                                                <cc1:Column HeaderText="PRODUCT MAPPING" AllowEdit="false" AllowDelete="true" Width="75"
                                                    Wrap="true">
                                                    <TemplateSettings TemplateId="MappingTemplateProduct" />
                                                </cc1:Column>
                                                <cc1:Column HeaderText="CATEGORY MAPPING" AllowEdit="false" AllowDelete="true" Width="80"
                                                    Wrap="true">
                                                    <TemplateSettings TemplateId="MappingTemplateCategory" />
                                                </cc1:Column>
                                                <cc1:Column HeaderText="SLAB WISE MAPPING" AllowEdit="false" AllowDelete="true" Width="80"
                                                    Wrap="true">
                                                    <TemplateSettings TemplateId="MappingTemplateSlabWiseTax" />
                                                </cc1:Column>
                                                <cc1:Column HeaderText="VENDOR MAPPING" AllowEdit="false" AllowDelete="true" Width="70"
                                                    Wrap="true">
                                                    <TemplateSettings TemplateId="MappingTemplateVendor" />
                                                </cc1:Column>
                                                <cc1:Column AllowEdit="true" AllowDelete="true" HeaderText="EDIT" runat="server"
                                                    Width="70">
                                                    <TemplateSettings TemplateId="editBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column HeaderText="DELETE" AllowEdit="false" AllowDelete="true" Width="80" Visible="false">
                                                    <TemplateSettings TemplateId="deleteBtnTemplate" />
                                                </cc1:Column>
                                            </Columns>
                                            <FilteringSettings MatchingType="AnyFilter" />
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="MappingTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="h_icon zoom_sl" title='<%# Container.DataItem["NAME"] %>'
                                                            onclick="openBSMapping('<%# Container.DataItem["NAME"] %>','<%# Container.DataItem["ID"] %>')" />
                                                    </Template>
                                                </cc1:GridTemplate>
                                                <cc1:GridTemplate runat="server" ID="slno">
                                                    <Template>
                                                        <nav style="position: relative;"><span class="badge black"><asp:Label ID="lblslno" runat="server" Text='<%#Container.PageRecordIndex+1 %>'></asp:Label></span></nav>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="MappingTemplateException">
                                                    <Template>
                                                        <a href="javascript: //" class="h_icon map" title='<%# Container.DataItem["NAME"] %>'
                                                            onclick="openBSMappingNew('<%# Container.DataItem["NAME"] %>','<%# Container.DataItem["ID"] %>')" />
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="MappingTemplateProduct">
                                                    <Template>
                                                        <a href="javascript: //" class="filter_btn electricity_input" title='<%# Container.DataItem["NAME"] %>'
                                                            onclick="openBSMappingProduct('<%# Container.DataItem["NAME"] %>','<%# Container.DataItem["ID"] %>')" />
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="MappingTemplateCategory">
                                                    <Template>
                                                        <a href="javascript: //" class="filter_btn eyedropper" title='<%# Container.DataItem["NAME"] %>'
                                                            onclick="openCategoryMapping('<%# Container.DataItem["NAME"] %>','<%# Container.DataItem["ID"] %>')" />
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="MappingTemplateVendor">
                                                    <Template>
                                                        <a href="javascript: //" class="filter_btn airplane" title='<%# Container.DataItem["NAME"] %>'
                                                            onclick="openVendorGroupMapping('<%# Container.DataItem["NAME"] %>','<%# Container.DataItem["ID"] %>')" />
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="MappingTemplateSlabWiseTax">
                                                    <Template>
                                                        <a href="javascript: //" class="filter_btn switch_co" title='<%# Container.DataItem["NAME"] %>'
                                                            onclick="openMappingSlabWiseTax('<%# Container.DataItem["NAME"] %>','<%# Container.DataItem["ID"] %>')" />
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="editBtnTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="action-icons c-edit" title='<%# Container.DataItem["NAME"] %>'
                                                            id="btnGridEdit_<%# Container.PageRecordIndex %>" onclick="CallServerMethod(this)">
                                                        </a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="deleteBtnTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="action-icons c-delete" title='<%# Container.DataItem["NAME"] %>'
                                                            onclick="gvPercentage.delete_record(this)"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <ScrollingSettings ScrollWidth="100%" ScrollHeight="290" NumberOfFixedColumns="3" />
                                        </cc1:Grid>
                                        <asp:HiddenField runat="server" ID="hdnPid" />
                                        <asp:HiddenField runat="server" ID="hdnPid1" />
                                        <asp:Button ID="btngridEDIT" runat="server" Text="GridSave" Style="display: none"
                                            CausesValidation="false" OnClick="btngridEDIT_Click" />
                                        <asp:Button ID="btnGridState" runat="server" Text="GridSave" Style="display: none"
                                            CausesValidation="false" OnClick="btnGridState_Click" />
                                        <asp:Button ID="btnGridExp" runat="server" Text="GridSave" Style="display: none"
                                            CausesValidation="false" OnClick="btnGridExp_Click" />
                                        <asp:Button ID="btnGridProduct" runat="server" Text="GridSave" Style="display: none"
                                            CausesValidation="false" OnClick="btnGridProduct_Click" />
                                        <asp:Button ID="btngridcategory" runat="server" Text="Category/SubItem Mapping" Style="display: none"
                                            CausesValidation="false" OnClick="btngridcategory_Click" />
                                        <asp:Button ID="btngridGroup" runat="server" Text="Vendor Group Mapping" Style="display: none"
                                            CausesValidation="false" OnClick="btngridGroup_Click" />
                                        <asp:Button ID="btnSlabwiseTaxmapping" runat="server" Style="display: none" CausesValidation="false"
                                            OnClick="btnSlabwiseTaxmapping_Click" />
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
                function CalcualteTotalAmount() {
                    var mrp = document.getElementById("<%=txtvalue.ClientID %>").value;
                    var percentage = document.getElementById("<%=txtslbpercentage.ClientID %>").value;

                    if (mrp > "0") {
                        document.getElementById("<%=txtslbpercentage.ClientID %>").value = "0";
                    } else {
                        document.getElementById("<%=txtvalue.ClientID %>").value = "0";
                    }
                }
            </script>
            <script type="text/javascript">
                function CalcualteTotalAmountVALUR() {
                    var mrp = document.getElementById("<%=txtvalue.ClientID %>").value;
                    var percentage = document.getElementById("<%=txtslbpercentage.ClientID %>").value;

                    if (percentage != "0") {
                        document.getElementById("<%=txtvalue.ClientID %>").value = "0";
                    }
                }
            </script>
            <script type="text/javascript">


                $("txttoamount").click(function () {
                    if ($(this).hasClass('disabled')) {
                        //ENABLE CLICK
                        $(this).removeClass("disabled")
                .prop("readonly", false).tooltip({
                    disabled: false
                });

                    } else {
                        //DISABLE CLICK
                        $(this).addClass("disabled")
                .prop("readonly", true)
                .tooltip({
                    disabled: true
                });
                    }
                });


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
            <script type="text/javascript">
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
                    gvPercentage.addFilterCriteria('NAME', OboutGridFilterCriteria.Contains, searchValue);
                    gvPercentage.addFilterCriteria('PERCENTAGE', OboutGridFilterCriteria.Contains, searchValue);
                    gvPercentage.executeFilter();
                    searchTimeout = null;
                    return false;
                }


            </script>
            <script type="text/javascript">

                function isNumberKeyWithDot(evt) {
                    var charCode = (evt.which) ? evt.which : event.keyCode;
                    if ((charCode < 48 || charCode > 57) && (charCode != 8) && (charCode != 46))
                        return false;

                    return true;
                }

                function CallDeleteServerMethod(oLink) {
                    var iRecordIndexdelete = oLink.id.toString().replace("btnGridDelete_", "");
                    document.getElementById("<%=hdndLoacationDelete.ClientID %>").value = gvexception.Rows[iRecordIndexdelete].Cells[0].Value;
                    document.getElementById("<%=btngriddelete.ClientID %>").click();
                }
                function CallServerMethod(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnGridEdit_", "");
                    document.getElementById("<%=Hdn_Fld.ClientID %>").value = gvPercentage.Rows[iRecordIndex].Cells[0].Value;
                    document.getElementById("<%=btngridEDIT.ClientID %>").click();
                    document.getElementById("<%=btnaddhide.ClientID%>").style.display = "none";
                }

                function openBSMapping(pname, pid) {
                    document.getElementById("<%=hdnPid.ClientID %>").value = pid;
                    document.getElementById("<%=hdnPid1.ClientID %>").value = "BS";
                    document.getElementById("<%=txtPname.ClientID %>").value = pname;
                    document.getElementById("<%=btnGridState.ClientID %>").click();
                }

                function openBSMappingNew(pname, pid) {
                    document.getElementById("<%=hdnPid.ClientID %>").value = pid;
                    document.getElementById("<%=hdnPid1.ClientID %>").value = "PS";
                    document.getElementById("<%=txtExceptionName.ClientID %>").value = pname;
                    document.getElementById("<%=btnGridExp.ClientID %>").click();
                } btnGridProduct

                function openBSMappingProduct(pname, pid) {
                    document.getElementById("<%=hdnPid.ClientID %>").value = pid;
                    document.getElementById("<%=hdnPid1.ClientID %>").value = "PM";
                    document.getElementById("<%=TxtPTaxname.ClientID %>").value = pname;
                    document.getElementById("<%=btnGridProduct.ClientID %>").click();
                }
                function openCategoryMapping(taxname, taxid) {
                    document.getElementById("<%=Hdn_Fld.ClientID %>").value = taxid;
                    document.getElementById("<%=hdnTaxName.ClientID %>").value = taxname;
                    document.getElementById("<%=txtopentaxname.ClientID %>").value = taxname;
                    document.getElementById("<%=btngridcategory.ClientID %>").click();
                }
                function openMappingSlabWiseTax(taxname, taxid) {
                    document.getElementById("<%=Hdn_Fld.ClientID %>").value = taxid;
                    document.getElementById("<%=txttaxname.ClientID %>").value = taxname;
                    document.getElementById("<%=btnSlabwiseTaxmapping.ClientID %>").click();
                }
                function openVendorGroupMapping(taxname, taxid) {
                    document.getElementById("<%=Hdn_Fld.ClientID %>").value = taxid;
                    document.getElementById("<%=txttaxnamegroup.ClientID %>").value = taxname;
                    document.getElementById("<%=btngridGroup.ClientID %>").click();
                }

                function DeleteSlabwisetaxmapping(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnGridDelete_", "");
                    document.getElementById("<%=hdn_Slabtax.ClientID %>").value = gvSlabwisetaxmapping.Rows[iRecordIndex].Cells[1].Value;
                    document.getElementById("<%=btndelete.ClientID %>").click();

                }
            </script>
            <script type="text/javascript">
                function validateFloatKeyPress(el, evt) {
                    var charCode = (evt.which) ? evt.which : event.keyCode;

                    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                        return false;
                    }

                    if (charCode == 46 && el.value.indexOf(".") !== -1) {
                        return false;
                    }

                    if (el.value.indexOf(".") !== -1) {
                        var range = document.selection.createRange();

                        if (range.text != "") {
                        }
                        else {
                            var number = el.value.split('.');
                            if (number.length == 2 && number[1].length > 1)
                                return false;
                        }
                    }

                    return true;
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
