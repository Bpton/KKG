<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmQualityControl_FAC.aspx.cs" Inherits="VIEW_frmQualityControl_FAC" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"
    TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .MyDDL {
            width: 10px;
            direction: rtl;
            text-align: left;
        }
    </style>
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <%--<div class="page_title">
                <span class="title_icon"><span class="computer_imac"></span></span>
                <h3>Quality Assurance</h3>
            </div>--%>
            <div id="content">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active">
                                <span class="h_icon_he list_images"></span>
                                <h6>Quality Assurance Details</h6>
                                <div class="btn_30_light" style="float: right;" id="divnewentry" runat="server">
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
                                                    <legend>QUALITY ASSURANCE ENTRY </legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td width="110" class="innerfield_title" style="padding-left: 10px;">
                                                                <asp:Label ID="Label1" runat="server" Text="Entry Date"></asp:Label><span class="req">*</span>
                                                            </td>
                                                            <td width="145">
                                                                <asp:TextBox ID="txtQCDate" runat="server" Width="60" Enabled="false" placeholder="dd/mm/yyyy"
                                                                    MaxLength="10" ValidationGroup="POFooter" Font-Bold="true" onkeypress="return isNumberKeyWithslash(event);"></asp:TextBox>
                                                                <asp:ImageButton ID="imgPopuppodate" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender5" PopupButtonID="imgPopuppodate"
                                                                    runat="server" TargetControlID="txtQCDate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                                <asp:RequiredFieldValidator ID="CV_QCDate" runat="server" ControlToValidate="txtQCDate"
                                                                    ValidationGroup="datecheckpodetail" SetFocusOnError="true" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender17" runat="server"
                                                                    TargetControlID="CV_QCDate" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="../images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                            </td>
                                                            <td width="80" class="innerfield_title" style="padding-left: 10px;">
                                                                <asp:Label ID="Label6" runat="server" Text="PO End Time"></asp:Label>
                                                            </td>

                                                            <td width="90" class="innerfield_title" style="padding-left: 10px;">
                                                                <asp:Label ID="lblpotime" runat="server" ForeColor="Red"></asp:Label>
                                                            </td>


                                                            <td width="90" class="innerfield_title" style="padding-left: 10px;">
                                                                <asp:Label ID="Label3" runat="server" Text="QA. Ref No"></asp:Label>
                                                            </td>
                                                            <td width="135">
                                                                <asp:TextBox ID="txtQCRefNo" runat="server" Width="130" placeholder="QA. Ref No"
                                                                    MaxLength="26"></asp:TextBox>

                                                            </td>
                                                            <td width="70" class="innerfield_title">
                                                                <asp:Label ID="Label9" runat="server" Text="Factory"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td width="400">
                                                                <asp:DropDownList ID="ddlTPUName" Width="400" runat="server" class="chosen-select"
                                                                    OnSelectedIndexChanged="ddlTPUName_SelectedIndexChanged" AutoPostBack="true"
                                                                    ValidationGroup="POFooter" Enabled="false">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="CV_TPUNAME" runat="server" ControlToValidate="ddlTPUName"
                                                                    InitialValue="0" SetFocusOnError="true" ErrorMessage="TPU Name is required!"
                                                                    Display="None" ValidationGroup="POFooter"></asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
                                                                    TargetControlID="CV_TPUNAME" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="../images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                            </td>

                                                            <td width="70" runat="server" id="fldAutoQcNumberheader" class="innerfield_title">
                                                                <asp:Label ID="lblqualitycontrol" runat="server" Text="QA No"></asp:Label>
                                                            </td>
                                                            <td runat="server" id="fldAutoQcNumber" width="70">
                                                                <asp:TextBox ID="txtqualitycontrolno" runat="server" placeholder="Auto Generate Quality Assurance No"
                                                                    Width="180" Enabled="false"></asp:TextBox>
                                                            </td>

                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                        <tr id="trproductdetails" runat="server">
                                            <td class="field_input" style="padding-left: 10px;">
                                                <fieldset>
                                                    <legend>PRODUCTION DETAILS</legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td width="110" class="innerfield_title" style="padding-left: 10px; padding-bottom: 10px;">
                                                                <asp:Label ID="Label4" runat="server" Text="Product"></asp:Label><span class="req">*</span>
                                                            </td>
                                                            <td colspan="3" style="padding-bottom: 10px;">
                                                                <asp:DropDownList ID="ddlProductName" runat="server" Width="420" AutoPostBack="true"
                                                                    class="chosen-select" data-placeholder="Choose Product" AppendDataBoundItems="True"
                                                                    OnSelectedIndexChanged="ddlProductName_SelectedIndexChanged">
                                                                    <asp:ListItem Text="-- SELECT PRODUCT NAME --" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field_title" style="padding-bottom: 12px;">
                                                                <asp:Label ID="Label13" runat="server" Text="PRODUCTION NO"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td width="505" class="field_input">
                                                                <asp:DropDownList ID="ddlpono" runat="server" Height="24px" CssClass="common-select"
                                                                    Width="620px" ValidationGroup="POFooter" AppendDataBoundItems="True" Style="font-family: 'Courier New', Courier, monospace"
                                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlpono_SelectedIndexChanged">
                                                                    <asp:ListItem Text="SELECT PRODUCTION NO" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="CV_PRODUCTION" runat="server" ControlToValidate="ddlpono"
                                                                    ValidationGroup="POFooter" InitialValue="0" SetFocusOnError="true" ErrorMessage="*"
                                                                    Font-Bold="true"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td width="60" class="field_title" style="padding-bottom: 12px;">
                                                                <asp:Label ID="Label10" runat="server" Text="PO No"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td class="field_input" style="padding-bottom: 14px;">
                                                                <asp:DropDownList ID="ddlponoNew" runat="server" Width="235" class="chosen-select"
                                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlponoNew_SelectedIndexChanged">
                                                                    <asp:ListItem Text="SELECT PO NO" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="gridcontent-inner" id="divqcdetails" runat="server">
                                        <fieldset>
                                            <legend>CHECKED QUALITY ASSURANCE DETAILS</legend>
                                            <cc1:Grid ID="gvqcentry" runat="server" CallbackMode="true" AllowAddingRecords="false" EnableRecordHover="true"
                                                AllowSorting="false" AutoGenerateColumns="false" PageSize="200" AllowPageSizeSelection="false"
                                                AllowPaging="true" AllowFiltering="false" Serialize="true" FolderStyle="../GridStyles/premiere_blue"
                                                OnRowDataBound="gvqcentry_RowDataBound">
                                                <Columns>
                                                    <cc1:Column ID="Column11" DataField="GUID" ReadOnly="true" HeaderText="GUID" runat="server"
                                                        Visible="false" Width="60" />
                                                    <cc1:Column ID="Column1" DataField="PRODUCTID" ReadOnly="true" HeaderText="ID" runat="server"
                                                        Visible="false" Width="60" />
                                                    <cc1:Column ID="Column4" DataField="Sl" HeaderText="SL" runat="server" Width="50">
                                                        <TemplateSettings TemplateId="slnoTemplate" />
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column5" DataField="PRODUCTNAME" HeaderText="PRODUCT NAME" runat="server"
                                                        Width="320" Wrap="true">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column27" DataField="BATCHNO" HeaderText="BATCH NO" runat="server"
                                                        Width="120" Wrap="true">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column6" DataField="PRODUCTIONQTY" HeaderText="PRODUCTION QTY" runat="server"
                                                        Wrap="true" Width="120">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column2" DataField="ALREADYQCQTY" HeaderText="QA QTY" runat="server"
                                                        Wrap="true" Width="90">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column3" DataField="" HeaderText="ACCEPTED QTY BASED ON AQL" runat="server"
                                                        AllowEdit="true" Wrap="true" Width="110">
                                                        <TemplateSettings TemplateId="CurrentQCQty" />
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column36" DataField="" HeaderText="REJECTED QTY BASED ON AQL" runat="server"
                                                        AllowEdit="true" Wrap="true" Width="110">
                                                        <TemplateSettings TemplateId="RejectedQCQty" />
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column7" DataField="PACKINGSIZENAME" HeaderText="PACK SIZE" runat="server"
                                                        Width="120" Wrap="true">
                                                    </cc1:Column>
                                                    <cc1:Column DataField="REASONNAME" HeaderText="REJECTED QTY REASON" Width="180" AllowEdit="true"
                                                        Wrap="true">
                                                        <TemplateSettings TemplateId="tempReason" />
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column30" DataField="POID" HeaderText="POID" runat="server" Visible="false" />
                                                    <cc1:Column ID="Column31" DataField="PUID" HeaderText="PUID" runat="server" Visible="false" />
                                                </Columns>
                                                <Templates>
                                                    <cc1:GridTemplate runat="server" ID="slnoTemplate">
                                                        <Template>
                                                            <asp:Label ID="lblslno" runat="server" Text='<%#Container.PageRecordIndex+1 %>'></asp:Label>
                                                        </Template>
                                                    </cc1:GridTemplate>
                                                    <cc1:GridTemplate runat="server" ID="tempReason" ControlID="ddlreason">
                                                        <Template>
                                                            <asp:DropDownList ID="ddlreason" runat="server" Width="100%" Height="18" CssClass="MyDDL"
                                                                dir="ltl">
                                                            </asp:DropDownList>
                                                        </Template>
                                                    </cc1:GridTemplate>
                                                    <cc1:GridTemplate runat="server" ID="CurrentQCQty">
                                                        <Template>
                                                            <asp:HiddenField runat="server" ID="QCGUID" Value='<%# Container.DataItem["PRODUCTNAME"] %>' />
                                                            <asp:TextBox runat="server" ID="txtcurrentqcqty" Text="0" MaxLength="10" Width="80"
                                                                Height="18" onkeypress="return isNumberKeyWithDot(event);" AutoCompleteType="Disabled"
                                                                onfocus="disableautocompletion(this.id);" />
                                                        </Template>
                                                    </cc1:GridTemplate>
                                                    <cc1:GridTemplate runat="server" ID="RejectedQCQty">
                                                        <Template>
                                                            <asp:HiddenField runat="server" ID="QCGUIDREJQTY" Value='<%# Container.DataItem["PRODUCTNAME"] %>' />
                                                            <asp:TextBox runat="server" ID="txtrejectedqcqty" Text="0" MaxLength="10" Width="80"
                                                                Height="18" onkeypress="return isNumberKeyWithDot(event);" AutoCompleteType="Disabled"
                                                                onfocus="disableautocompletion(this.id);" />
                                                        </Template>
                                                    </cc1:GridTemplate>
                                                </Templates>
                                                <ScrollingSettings ScrollWidth="100%" ScrollHeight="220" />
                                            </cc1:Grid>
                                            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td style="padding-top: 10px;">
                                                        <div class="btn_24_blue">
                                                            <span class="icon add_co"></span>
                                                            <asp:Button ID="btnadd" runat="server" Text="Add" CssClass="btn_link" ValidationGroup="POFooter"
                                                                OnClick="btnadd_Click" />
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>

                                    </div>

                                    <div class="gridcontent-inner">
                                        <fieldset>
                                            <legend>ACCEPTED / REJECTED Quality Assurance DETAILS BASED ON AQL</legend>
                                            <cc1:Grid ID="gvQCUpdate" runat="server" CallbackMode="true" AllowAddingRecords="false" EnableRecordHover="true"
                                                AllowSorting="false" AutoGenerateColumns="false" PageSize="500" AllowPageSizeSelection="false"
                                                AllowPaging="false" AllowFiltering="false" FolderStyle="../GridStyles/premiere_blue">
                                                <Columns>
                                                    <cc1:Column ID="Column8" DataField="GUID" ReadOnly="true" HeaderText="GUID" runat="server"
                                                        Visible="false" />
                                                    <cc1:Column ID="Column9" DataField="PRODUCTID" ReadOnly="true" HeaderText="ID" runat="server"
                                                        Visible="false" />
                                                    <cc1:Column ID="Column10" DataField="Sl" HeaderText="SL" runat="server" Width="50">
                                                        <TemplateSettings TemplateId="slnoTemplateqc" />
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column17" DataField="PONO" HeaderText="PO NO" runat="server" Width="140"
                                                        Wrap="true" />
                                                    <cc1:Column ID="Column18" DataField="PUNO" HeaderText="PRODUCTION NO" runat="server"
                                                        Width="140" Wrap="true" />
                                                    <cc1:Column ID="Column12" DataField="PRODUCTNAME" HeaderText="PRODUCT NAME" runat="server"
                                                        Width="300" Wrap="true">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column26" DataField="BATCHNO" HeaderText="BATCH NO" runat="server"
                                                        Width="140" Wrap="true">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column13" DataField="PRODUCTIONQTY" HeaderText="PRODUCTION QTY" runat="server"
                                                        Wrap="true" Width="120">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column14" DataField="ALREADYQCQTY" HeaderText="QA QTY" runat="server"
                                                        Wrap="true" Width="80">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column15" DataField="CURRENTQCQTY" HeaderText="ACCEPTED QTY BASED ON AQL"
                                                        runat="server" Wrap="true" Width="110">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column37" DataField="REJECTEDQCQTY" HeaderText="REJECTED QTY BASED ON AQL"
                                                        runat="server" Wrap="true" Width="110">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column19" DataField="REMAININGQCQTY" HeaderText="REMAINIG QTY" runat="server"
                                                        Wrap="true" Width="100" Visible="false">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column33" DataField="TOTALQCQTY" HeaderText="TOTAL QTY" runat="server"
                                                        Wrap="true" Width="80">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column16" DataField="PACKINGSIZENAME" HeaderText="PACK SIZE" runat="server"
                                                        Width="100" Wrap="true">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column34" DataField="REASONNAME" HeaderText="REJECTED QTY REASON"
                                                        runat="server" Width="200" Wrap="true">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column35" DataField="REASONID" HeaderText="REASONID" runat="server"
                                                        Width="10" Wrap="true" Visible="false">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column28" DataField="POID" HeaderText="POID" runat="server" Visible="false" />
                                                    <cc1:Column ID="Column29" DataField="PUID" HeaderText="PUID" runat="server" Visible="false" />
                                                    <cc1:Column HeaderText="DELETE" AllowEdit="false" AllowDelete="true" Width="70">
                                                        <TemplateSettings TemplateId="deleteBtnTemplate" />
                                                    </cc1:Column>
                                                </Columns>
                                                <Templates>
                                                    <cc1:GridTemplate runat="server" ID="deleteBtnTemplate">
                                                        <Template>
                                                            <a href="javascript: //" class="action-icons c-delete" id="btnGridDelete_<%# Container.PageRecordIndex %>"
                                                                onclick="CallDeleteServerMethod(this)"></a>
                                                        </Template>
                                                    </cc1:GridTemplate>
                                                    <cc1:GridTemplate runat="server" ID="slnoTemplateqc">
                                                        <Template>
                                                            <asp:Label ID="lblslnoqc" runat="server" Text='<%#Container.PageRecordIndex+1 %>'></asp:Label>
                                                        </Template>
                                                    </cc1:GridTemplate>
                                                </Templates>
                                                <ScrollingSettings ScrollWidth="100%" ScrollHeight="220" />
                                            </cc1:Grid>
                                            <asp:Button ID="btngrddelete" runat="server" Text="grddelete" Style="display: none"
                                                OnClick="btngrddelete_Click" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete this record?');" />
                                            <asp:HiddenField ID="hdn_guid" runat="server" />
                                            <asp:HiddenField ID="hdn_qcno" runat="server" />
                                            <asp:HiddenField ID="hdn_tpuid" runat="server" />
                                            <asp:HiddenField ID="hdn_QCDate" runat="server" />
                                            <asp:HiddenField ID="hdn_QANo" runat="server" />
                                        </fieldset>
                                    </div>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px;">
                                                <fieldset>
                                                    <legend>OTHERS DETAILS</legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td width="290">
                                                                <asp:TextBox ID="txtremarks" runat="server" MaxLength="255" TextMode="MultiLine" Width="100%"
                                                                    placeholder="Remarks" class="input_grow"></asp:TextBox>

                                                            </td>
                                                            <td width="10">&nbsp;</td>
                                                            <td class="innerfield_title" width="130" style="padding-left: 10px;">
                                                                <asp:TextBox ID="txtcasepack" runat="server" MaxLength="15" Width="100px" placeholder="Total Case" Enabled="false"
                                                                    Font-Bold="true"></asp:TextBox>
                                                                <span class="label_intro">
                                                                    <asp:Label ID="Label17" runat="server" Text="ACCEPTED CASE QTY" Font-Bold="True"></asp:Label></span>
                                                            </td>
                                                            <td class="innerfield_title" width="130">
                                                                <asp:TextBox ID="txtrejectedcasepack" runat="server" MaxLength="15" Width="100px" placeholder="Total Case" Enabled="false"
                                                                    Font-Bold="true"></asp:TextBox>
                                                                <span class="label_intro">
                                                                    <asp:Label ID="Label2" runat="server" Text="REJECTED CASE QTY" Font-Bold="True"></asp:Label></span>
                                                            </td>
                                                            <td width="130" class="innerfield_title" style="padding-left: 10px;" runat="server" id="Td1">
                                                                <asp:Label ID="chk" runat="server" Text="QA DOCUMENT UPLOAD" ForeColor="Blue"></asp:Label>
                                                            </td>
                                                            <td align="left" class="field_input" style="padding-bottom: 20px;" width="10">
                                                                <asp:CheckBox ID="chkfileupload" runat="server" Text=" " OnCheckedChanged="chkfileupload_check"
                                                                    CausesValidation="false" AutoPostBack="true" />
                                                            </td>
                                                            <td width="130">
                                                                <div class="btn_24_blue" id="divshow" runat="server">
                                                                    <span class="icon exclamation_co"></span>
                                                                    <asp:Button ID="btnshow" runat="server" Text="Documents" CssClass="btn_link"
                                                                        CausesValidation="false" OnClick="btnshow_Click" />
                                                                </div>
                                                            </td>

                                                            <td>
                                                                <div class="btn_24_blue" id="divbtnapprove" runat="server">
                                                                    <span class="icon disk_co"></span>
                                                                    <asp:Button ID="btnapprove" runat="server" Text="Approve" CssClass="btn_link" OnClick="btnapprove_Click" />
                                                                </div>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                                <div class="btn_24_red" id="divbtnrejection" runat="server">
                                                                    <span class="icon reject_co"></span>
                                                                    <asp:LinkButton ID="btnreject" runat="server" Text="Reject" CssClass="btn_link" CausesValidation="false"
                                                                        OnClick="btnreject_Click" />
                                                                </div>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
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
                                                            </td>
                                                            <td>&nbsp;</td>
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
                                                            <asp:Label ID="Label5" runat="server" Text="From Date"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="165">
                                                            <asp:TextBox ID="txtfromdateins" runat="server" MaxLength="10" Width="120" Enabled="false"
                                                                placeholder="dd/mm/yyyy" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="datecheckins"
                                                                Font-Bold="true"></asp:TextBox>
                                                            <asp:ImageButton ID="imgPopup" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="Calendar1" PopupButtonID="imgPopup" runat="server"
                                                                TargetControlID="txtfromdateins" CssClass="cal_Theme1" Format="dd/MM/yyyy">
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
                                                            <asp:Label ID="Label11" runat="server" Text="To Date"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="165">
                                                            <asp:TextBox ID="txttodateins" runat="server" MaxLength="10" Width="120" Enabled="false"
                                                                placeholder="dd/mm/yyyy" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="datecheckins"
                                                                Font-Bold="true"></asp:TextBox>
                                                            <asp:ImageButton ID="ImageButton1" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImageButton1"
                                                                runat="server" TargetControlID="txttodateins" CssClass="cal_Theme1" Format="dd/MM/yyyy">
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
                                                                <asp:Button ID="btnQCfind" runat="server" Text="Search" CssClass="btn_link" ValidationGroup="datecheckins"
                                                                    OnClick="btnQCfind_Click" />
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
                                        <cc1:Grid ID="gvCurrentQcdetails" runat="server" CallbackMode="true" Serialize="true" PageSize="200" EnableRecordHover="true"
                                            FolderStyle="../GridStyles/premiere_blue" AutoGenerateColumns="false" AllowColumnResizing="true"
                                            AllowAddingRecords="false" AllowFiltering="true" OnRowDataBound="gvCurrentQcdetails_RowDataBound">
                                            <FilteringSettings InitialState="Visible" />
                                            <Columns>
                                                <cc1:Column ID="Column20" DataField="Sl" HeaderText="SL" runat="server" Width="60">
                                                    <TemplateSettings TemplateId="slnoTemplateIN" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column21" DataField="QCDATE" HeaderText="QA DATE" runat="server" Width="80" Wrap="true">
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
                                                <cc1:Column ID="Column22" DataField="QCNO" HeaderText="QA NO" runat="server" Width="180" Wrap="true">
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
                                                <cc1:Column ID="Column24" DataField="TPUNAME" HeaderText="FACTORY" runat="server" Width="110" Wrap="true">
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
                                                <cc1:Column ID="Column25" DataField="TPUID" HeaderText="TPUID" runat="server" Width="60"
                                                    Visible="false">
                                                </cc1:Column>
                                                <cc1:Column ID="Column32" DataField="QCID" HeaderText="QCID" runat="server" Width="60"
                                                    Visible="false">
                                                </cc1:Column>

                                                <cc1:Column ID="Column40" DataField="TOTALPRODUCT" HeaderText="TOT.No.PRODUCT" runat="server"
                                                    Width="130" Wrap="true" AllowEdit="true" ItemStyle-HorizontalAlign="Center">
                                                    <TemplateSettings TemplateId="tpl_PrNo" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column38" DataField="TOTALACCEPTEDCASE" HeaderText="TOT ACCPT CASE" runat="server" Width="140" Wrap="true">
                                                </cc1:Column>
                                                <cc1:Column ID="Column39" DataField="TOTALREJECTEDCASE" HeaderText="TOT REJCTD CASE" runat="server" Width="140" Wrap="true">
                                                </cc1:Column>
                                                <cc1:Column ID="Column43" DataField="ISVERIFIEDDESC" HeaderText="QA STATUS" runat="server" Width="100" Wrap="true">
                                                </cc1:Column>
                                                <cc1:Column ID="Column44" DataField="QCREFNO" HeaderText="QC REF NO" runat="server" Width="180" Wrap="true">
                                                </cc1:Column>

                                                <cc1:Column ID="Column41" AllowEdit="true" AllowDelete="true" HeaderText="VIEW" runat="server" Wrap="true"
                                                    Width="60" Visible="false">
                                                    <TemplateSettings TemplateId="viewBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column23" AllowEdit="true" AllowDelete="true" HeaderText="EDIT" runat="server" Wrap="true"
                                                    Width="60">
                                                    <TemplateSettings TemplateId="editBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column42" AllowEdit="true" AllowDelete="true" HeaderText="DELETE" runat="server" Wrap="true"
                                                    Width="70">
                                                    <TemplateSettings TemplateId="deleteqcBtnTemplate" />
                                                </cc1:Column>
                                            </Columns>
                                            <FilteringSettings MatchingType="AnyFilter" />

                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="tpl_PrNo">
                                                    <Template>
                                                        <a href="javascript: //" id="btnGridProduct_<%# Container.PageRecordIndex %>" title="View Product"
                                                            onclick="CallProductServerMethod(this)"><%# Container.DataItem["TOTALPRODUCT"]%></a>
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
                                                <cc1:GridTemplate runat="server" ID="deleteqcBtnTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="action-icons c-delete" id="btnGridQCDelete_<%# Container.PageRecordIndex %>"
                                                            onclick="CallQCDeleteServerMethod(this)"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="viewBtnTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="h_icon zoom_sl" id="btnGridView_<%# Container.PageRecordIndex %>"
                                                            onclick="CallViewServerMethod(this)"></a>
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
                                        <asp:Button ID="btngrdqcdelete" runat="server" Text="Delete" Style="display: none" OnClick="btngrdqcdelete_Click"
                                            CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete this record?');" />
                                        <asp:Button ID="btngrdview" runat="server" Text="grdedit" Style="display: none" OnClick="btngrdview_Click"
                                            CausesValidation="false" />
                                        <asp:Button ID="btngrdProduct" runat="server" Text="grdProduct" Style="display: none" OnClick="btngrdProduct_Click"
                                            CausesValidation="false" />
                                    </div>
                                </asp:Panel>

                                <div id="light" class="white_content" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_title">
                                                <asp:Label ID="Label920" runat="server" Text="QA No."></asp:Label>
                                            </td>
                                            <td class="field_input">
                                                <asp:TextBox ID="txtPopUpQCNo" runat="server" Enabled="false" CssClass="large"></asp:TextBox>
                                            </td>
                                            <td class="field_title">
                                                <asp:Label ID="Label930" runat="server" Text="QA.Date"></asp:Label>
                                            </td>
                                            <td class="field_input">
                                                <asp:TextBox ID="txtPopUPQcDate" runat="server" Enabled="false" CssClass="large"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_input" colspan="4">
                                                <div class="gridcontent-inner">
                                                    <fieldset>
                                                        <legend>PRODUCT DETAILS</legend>
                                                        <div style="overflow: scroll; margin-bottom: 8px; height: 260px;">
                                                            <asp:GridView ID="grdQCProductDetails" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Available"
                                                                Width="100%" CssClass="zebra" PageSize="50">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="SL">
                                                                        <ItemTemplate>
                                                                            <%# Container.DataItemIndex + 1 %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="PRODUCTID" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPOPgrdProductID" runat="server" Text='<%# Eval("PRODUCTID") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="PRODUCT">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPOPgrdProductName" runat="server" Text='<%# Eval("PRODUCTNAME") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="BATCH NO">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPOPgrdBatchNo" runat="server" Text='<%# Eval("BATCHNO") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="PACK SIZE">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPOPgrdPackSize" runat="server" Text='<%# Eval("PACKINGSIZENAME") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="PRODUCTION QTY">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPOPgrdProductionQty" runat="server" Text='<%# Eval("PRODUCTIONQTY") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="QA QTY">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPOPgrdCurrentQC" runat="server" Text='<%# Eval("QCQTY") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="RJCTD.QTY">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPOPgrdRejQty" runat="server" Text='<%# Eval("REJECTEDQCQTY") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="REM.QTY">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPOPgrdRemQty" runat="server" Text='<%# Eval("REMAININGQCQTY") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </fieldset>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" class="field_input">
                                                <asp:Button ID="btnCloseLightbox" runat="server" Text="Close" CssClass="btn_small btn_blue" OnClick="btnCloseLightbox_Click" />

                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="fade" class="black_overlay" runat="server">
                                </div>


                                <script type="text/javascript">
                                    function isNumberKeyWithDot(evt) {
                                        var charCode = (evt.which) ? evt.which : event.keyCode;
                                        if ((charCode < 48 || charCode > 57) && (charCode != 8) && (charCode != 46))
                                            return false;

                                        return true;
                                    }

                                    function CallDeleteServerMethod(oLink) {
                                        var iRecordIndexdelete = oLink.id.toString().replace("btnGridDelete_", "");
                                        document.getElementById("<%=hdn_guid.ClientID %>").value = gvQCUpdate.Rows[iRecordIndexdelete].Cells[0].Value;
                                        document.getElementById("<%=btngrddelete.ClientID %>").click();
                                    }

                                    function CallServerMethod(oLink) {
                                        var iRecordIndex = oLink.id.toString().replace("btnGridEdit_", "");
                                        document.getElementById("<%=hdn_qcno.ClientID %>").value = gvCurrentQcdetails.Rows[iRecordIndex].Cells[5].Value;
                                        document.getElementById("<%=hdn_tpuid.ClientID %>").value = gvCurrentQcdetails.Rows[iRecordIndex].Cells[4].Value;
                                        document.getElementById("<%=btngrdedit.ClientID %>").click();

                                    }

                                    function CallQCDeleteServerMethod(oLink) {
                                        var iRecordIndex = oLink.id.toString().replace("btnGridQCDelete_", "");
                                        document.getElementById("<%=hdn_qcno.ClientID %>").value = gvCurrentQcdetails.Rows[iRecordIndex].Cells[5].Value;
                                        document.getElementById("<%=hdn_tpuid.ClientID %>").value = gvCurrentQcdetails.Rows[iRecordIndex].Cells[4].Value;
                                        document.getElementById("<%=btngrdqcdelete.ClientID %>").click();

                                    }

                                    function CallViewServerMethod(oLink) {
                                        var iRecordIndex = oLink.id.toString().replace("btnGridView_", "");
                                        document.getElementById("<%=hdn_qcno.ClientID %>").value = gvCurrentQcdetails.Rows[iRecordIndex].Cells[5].Value;
                                        document.getElementById("<%=hdn_tpuid.ClientID %>").value = gvCurrentQcdetails.Rows[iRecordIndex].Cells[4].Value;
                                        document.getElementById("<%=btngrdview.ClientID %>").click();

                                    }


                                    function CallProductServerMethod(oLink) {
                                        var iRecordIndex = oLink.id.toString().replace("btnGridProduct_", "");
                                        document.getElementById("<%=hdn_qcno.ClientID %>").value = gvCurrentQcdetails.Rows[iRecordIndex].Cells[5].Value;
                                        document.getElementById("<%=hdn_QANo.ClientID %>").value = gvCurrentQcdetails.Rows[iRecordIndex].Cells[2].Value;
                                        document.getElementById("<%=hdn_QCDate.ClientID %>").value = gvCurrentQcdetails.Rows[iRecordIndex].Cells[1].Value;
                                        document.getElementById("<%=btngrdProduct.ClientID %>").click();

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
                                        gvCurrentQcdetails.addFilterCriteria('QCDATE', OboutGridFilterCriteria.Contains, searchValue);
                                        gvCurrentQcdetails.addFilterCriteria('QCNO', OboutGridFilterCriteria.Contains, searchValue);
                                        gvCurrentQcdetails.addFilterCriteria('TPUNAME', OboutGridFilterCriteria.Contains, searchValue);
                                        gvCurrentQcdetails.executeFilter();
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
                                <cc2:MessageBox ID="MessageBox1" runat="server" />
                            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>