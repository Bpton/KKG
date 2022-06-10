<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true"
    CodeFile="frmShiftStoreLocation_FAC.aspx.cs" Inherits="FACTORY_frmShiftStoreLocation_FAC" %>

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
            
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>
                                    Inter Store Location Transfer</h6>
                                <div class="btn_30_light" style="float: right;" id="btnaddhide" runat="server">
                                    <span class="icon add_co"></span>
                                    <asp:Button ID="btnnewentry" runat="server" Text="Add New Record" CssClass="btn_link"
                                        CausesValidation="false" OnClick="btnnewentry_Click" />
                                </div>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px">
                                                <fieldset>
                                                    <legend>Store Location Transfer INFORMATION</legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                                        <tr>
                                                            <td  class="field_title" width="100">
                                                                <asp:Label ID="lblfromdepo" Text="Name" runat="server"></asp:Label><span class="req">*</span>
                                                            </td>
                                                            <td class="field_input" width="200">
                                                                <asp:DropDownList ID="ddlfromdepot" runat="server" AppendDataBoundItems="true" Style="width: 200px;"
                                                                    class="chosen-select" ValidationGroup="check" data-placeholder="&nbsp;No Depot Found&nbsp;"
                                                                    OnSelectedIndexChanged="ddlfromdepot_SelectedChanged" AutoPostBack="true">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td  class="field_title" width="90">
                                                                <asp:Label ID="Label1" runat="server" Text="Entry Date"></asp:Label><span class="req">*</span>
                                                            </td>
                                                            <td class="field_input" width="140">
                                                                <asp:TextBox ID="txtadjustmentdate" runat="server" Style="width: 90px;" Enabled="false"
                                                                    placeholder="dd/mm/yyyy" MaxLength="10" ValidationGroup="check" onkeypress="return isNumberKeyWithslash(event);">
                                                                </asp:TextBox>
                                                                <asp:ImageButton ID="imgPopuppodate" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24"  />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="imgPopuppodate"
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
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                        <tr id="trAdd" runat="server">
                                            <td class="field_input" style="padding-left: 10px">
                                                <fieldset>
                                                    <legend>PRODUCT DETAILS</legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                                        <tr>
                                                            <td>
                                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                                    <tr>
                                                                        <td width="100" class="field_title">
                                                                            <asp:Label ID="lblstorelocation" Text="From Loaction" runat="server"></asp:Label>&nbsp;<span
                                                                                class="req">*</span>
                                                                        </td>
                                                                        <td width="210" class="field_input">
                                                                            <asp:DropDownList ID="ddlstorelocation" runat="server" AppendDataBoundItems="true"
                                                                                Style="width: 200px;" ValidationGroup="check" class="chosen-select" data-placeholder="Select Store Loacation">
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                                                                ValidationGroup="check" Font-Bold="true" ForeColor="Red" ControlToValidate="ddlstorelocation"
                                                                                ValidateEmptyText="false" SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
                                                                        </td>
                                                                        <td width="90" class="field_title">
                                                                            <asp:Label ID="Label5" Text="To Loaction" runat="server"></asp:Label>&nbsp;<span
                                                                                class="req">*</span>
                                                                        </td>
                                                                        <td width="210" class="field_input">
                                                                            <asp:DropDownList ID="ddltostorelocation" runat="server" AppendDataBoundItems="true"
                                                                                Style="width: 200px;" ValidationGroup="check" class="chosen-select" data-placeholder="Select Store Loacation">
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                                                                ValidationGroup="check" Font-Bold="true" ForeColor="Red" ControlToValidate="ddltostorelocation"
                                                                                ValidateEmptyText="false" SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
                                                                        </td>
                                                                        <td>&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="100" class="field_title">
                                                                            <asp:Label ID="Label11" Text="Product " runat="server"></asp:Label><span class="req">*</span>
                                                                        </td>
                                                                        <td width="400" class="field_input">
                                                                            <asp:DropDownList ID="ddlproductname" runat="server" AppendDataBoundItems="true"
                                                                                Style="width: 390px; background-color: Black" ValidationGroup="check" class="chosen-select"
                                                                                data-placeholder="Select Product Name" OnSelectedIndexChanged="ddlproductname_SelectedChanged"
                                                                                AutoPostBack="true">
                                                                                <asp:ListItem Text="SELECT PRODUCT NAME" Value="0"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*"
                                                                                ValidationGroup="check" Font-Bold="true" ForeColor="Red" ControlToValidate="ddlproductname"
                                                                                ValidateEmptyText="false" SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
                                                                        </td>
                                                                        <td class="field_title">
                                                                            <asp:Label ID="Label13" Text="Pack Size " runat="server"></asp:Label><span class="req">*</span>
                                                                        </td>
                                                                        <td class="field_input" colspan="2" >
                                                                            <asp:DropDownList ID="ddlpackingsize" runat="server" AppendDataBoundItems="true"
                                                                                Style="width: 200px;" ValidationGroup="check" class="chosen-select" data-placeholder="-- Select Pack Size --"
                                                                                OnSelectedIndexChanged="ddlpackingsize_SelectedIndexChanged" AutoPostBack="true">
                                                                                <asp:ListItem Text="-- SELECT PACK SIZE --" Value="0"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="* Required!"
                                                                                Font-Bold="true" ForeColor="Red" ControlToValidate="ddlpackingsize" ValidateEmptyText="false"
                                                                                ValidationGroup="check" SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
                                                                        </td>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                            <td class="field_title" style="padding-bottom:16px;">
                                                                <asp:Label ID="Label12" Text="Batch No " runat="server"></asp:Label><span class="req">*</span>
                                                            </td>
                                                            <td class="field_input" colspan="4">
                                                                <asp:DropDownList ID="ddlbatchno" runat="server" AppendDataBoundItems="true" ValidationGroup="check"
                                                                    CssClass="common-select" data-placeholder="Select Batch No" OnSelectedIndexChanged="ddlbatchno_SelectedIndexChanged"
                                                                    AutoPostBack="true" Style="width: 710px; font-family: 'Courier New', Courier, monospace;">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="* Required!"
                                                                    Font-Bold="true" ForeColor="Red" ControlToValidate="ddlbatchno" ValidateEmptyText="false"
                                                                    ValidationGroup="check" SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field_title">
                                                                &nbsp;
                                                            </td>
                                                            <td class="field_input" colspan="3">
                                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                                    <tr>
                                                                        <td class="field_input">
                                                                            <asp:TextBox ID="txtadjustmentqty" runat="server" placeholder="Journal Qty" CssClass="x_large"
                                                                                AutoCompleteType="Disabled" onfocus="disableautocompletion(this.id);"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="*"
                                                                                 ForeColor="Red" ControlToValidate="txtadjustmentqty" ValidateEmptyText="false"
                                                                                ValidationGroup="check" SetFocusOnError="true" Font-Size="Larger"></asp:RequiredFieldValidator>
                                                                            <span class="label_intro">
                                                                                <asp:Label ID="Label3" runat="server" Text="JOURNAL QTY"></asp:Label></span>
                                                                        </td>
                                                                        <td class="field_input">
                                                                            <asp:TextBox ID="txtstockqty" runat="server" placeholder="Stock Qty" Enabled="false"
                                                                                CssClass="full"></asp:TextBox>
                                                                            <span class="label_intro">
                                                                                <asp:Label ID="Label7" runat="server" Text="STOCK QTY"></asp:Label></span>
                                                                        </td>
                                                                        <td class="field_input">
                                                                            <asp:TextBox ID="txtprice" runat="server" placeholder="Depot Rate" Enabled="false"
                                                                                CssClass="x_large"></asp:TextBox>
                                                                            <span class="label_intro">
                                                                                <asp:Label ID="Label8" runat="server" Text="DEPOT RATE"></asp:Label></span>
                                                                        </td>
                                                                        <td class="field_input">
                                                                            <asp:TextBox ID="txtmrp" runat="server" placeholder="MRP" Enabled="false" CssClass="x_large"></asp:TextBox>
                                                                            <span class="label_intro">
                                                                                <asp:Label ID="Label4" runat="server" Text="MRP"></asp:Label></span>
                                                                        </td>
                                                                        <td class="field_title"  width="60" style="padding-bottom:16px;">
                                                                            <asp:Label ID="Label2" Text="Reason " runat="server"></asp:Label><span class="req">*</span>
                                                                        </td>
                                                                        <td class="field_input">
                                                                            <asp:DropDownList ID="ddlreason" runat="server" AppendDataBoundItems="true"
                                                                                ValidationGroup="check" class="chosen-select">
                                                                                <asp:ListItem Text="SELECT REASON NAME" Value="0"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                                                                Font-Bold="true" ForeColor="Red" ControlToValidate="ddlreason" ValidateEmptyText="false"
                                                                                ValidationGroup="check" SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
                                                                        </td>
                                                                        
                                                                     </tr>
                                                                 </table>
                                                            </td>
                                                            <td class="field_input" width="70" style="padding-bottom:16px;">
                                                                <div class="btn_24_blue" >
                                                                    <span class="icon add_co"></span>
                                                                    <asp:Button ID="btnadd" runat="server" Text="ADD" CssClass="btn_link" ValidationGroup="check"
                                                                    OnClick="btnadd_Click" CausesValidation="true" />
                                                                </div>
                                                                            
                                                            </td>
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
                                            <legend>TRASFER PRODUCT DETAILS</legend>
                                            <cc1:Grid ID="gvadjustment" runat="server" CallbackMode="true" AutoGenerateColumns="false"
                                                AllowPaging="true" PageSize="500" AllowPageSizeSelection="false" FolderStyle="../GridStyles/premiere_blue"
                                                AllowAddingRecords="false" AllowSorting="false">
                                                <FilteringSettings InitialState="Visible" />
                                                <Columns>
                                                    <cc1:Column ID="Column11" DataField="GUID" ReadOnly="true" HeaderText="GUID" runat="server"
                                                        Visible="false" Width="60" />
                                                    <cc1:Column ID="Column1" DataField="PRODUCTID" ReadOnly="true" HeaderText="ID" runat="server"
                                                        Visible="false" Width="60" />
                                                    <cc1:Column ID="Column4" DataField="Sl" HeaderText="SL" runat="server" Width="60">
                                                        <TemplateSettings TemplateId="slnoTemplate" />
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column5" DataField="PRODUCTNAME" HeaderText="PRODUCT NAME" runat="server"
                                                        Width="400">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column6" DataField="BATCHNO" HeaderText="BATCH NO" runat="server"
                                                        Width="125">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column2" DataField="ADJUSTMENTQTY" HeaderText="JOURNAL QTY" runat="server"
                                                        Width="100" Wrap="true">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column7" DataField="PACKINGSIZENAME" HeaderText="PACK SIZE" runat="server"
                                                        Width="200">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column8" DataField="REASONNAME" HeaderText="REASON" runat="server"
                                                        Width="200" Wrap="true">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column9" DataField="REASONID" HeaderText="REASONID" runat="server"
                                                        Visible="false" Width="200" Wrap="true">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column10" DataField="STORELOCATIONNAME" HeaderText="STORE LOCATION"
                                                        runat="server" Width="200" Wrap="true" />
                                                    <cc1:Column DataField="STORELOCATIONID" HeaderText="STORELOCATIONID" runat="server"
                                                        Visible="false" Width="200" Wrap="true">
                                                    </cc1:Column>
                                                    <cc1:Column DataField="MFDATE" HeaderText="MFDATE" runat="server" Visible="false"
                                                        Width="100" Wrap="true" />
                                                    <cc1:Column DataField="EXPRDATE" HeaderText="EXPRDATE" runat="server" Visible="false"
                                                        Width="100" Wrap="true" />
                                                    <cc1:Column DataField="ASSESMENTPERCENTAGE" HeaderText="ASSESMENTPERCENTAGE" runat="server"
                                                        Visible="false" Width="100" Wrap="true" />
                                                    <cc1:Column DataField="MRP" HeaderText="MRP" runat="server" Visible="false" Width="100"
                                                        Wrap="true" />
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
                                        <fieldset>
                                            <legend>REMARKS & OTHERS</legend>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_title" width="8%">
                                                Remarks
                                            </td>
                                            <td class="field_input" width="26%">
                                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="mid" TextMode="MultiLine" Style="width: 90%;
                                                    height: 80px" MaxLength="255"> </asp:TextBox>
                                            </td>
                                            <td class="field_input">
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
                                                <asp:HiddenField ID="hdn_adjustmentid" runat="server" />
                                                <asp:HiddenField ID="hdn_guid" runat="server" />
                                                <asp:HiddenField ID="hdn_lockadjustmentqty" runat="server" />
                                                <asp:HiddenField ID="hdn_stockqtyinpcs" runat="server" />
                                                <asp:HiddenField ID="hdn_editedstockqty" runat="server" />
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
                                                        <td width="165">
                                                            <asp:TextBox ID="txtfromdateins" runat="server" MaxLength="10" Width="120" Enabled="false"
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
                                                        <td width="165">
                                                            <asp:TextBox ID="txttodateins" runat="server" MaxLength="10" Width="120" Enabled="false"
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
                                            AllowPaging="true" PageSize="500" AllowAddingRecords="false" AllowFiltering="true"
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
                                                <cc1:Column ID="Column22" DataField="ADJUSTMENTNO" HeaderText="JOURNAL NO" runat="server" Wrap="true"
                                                    Width="220">
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
                                                <cc1:Column ID="Column24" DataField="DEPOTNAME" HeaderText="DEPOT NAME" runat="server" Wrap="true"
                                                    Width="180">
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
                                                <cc1:Column ID="Column12" DataField="ADJUSTMENT_FROMMENU" HeaderText="JOURNAL TYPE" runat="server" Wrap="true"
                                                    Width="220">
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
                                                <cc1:Column ID="Column23" AllowEdit="true" AllowDelete="true" HeaderText="EDIT / VIEW" runat="server"
                                                    Width="100%">
                                                    <TemplateSettings TemplateId="editBtnTemplate" />
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
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
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
