<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true"
    CodeFile="frmTPUProductionUpdate.aspx.cs" Inherits="VIEW_frmTPUProductionUpdate"
    Culture="en-GB" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register Namespace="OboutInc.Calendar2" Assembly="obout_Calendar2_Net" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function OnBeforeDelete(record) {
            record.Error = '';
            document.getElementById("<%=hdnfinal.ClientID %>").value = record.PRODUCTNAME;
            if (confirm("Are you sure you want to delete? "))
                return true;
            else
                return false;
        }
        function OnDelete(record) {
            alert(record.Error);
        }
    </script>
    <style type="text/css">
        .customStyle1
        {
            position: Relative;
            left: 950px !important;
            padding-top:10px;
            /*top: 450px !important;
            background-color: #A8AEBD;*/
            border: Bold;
            color: #fff;
            background-color: #8ab8ed;
            text-shadow: 0 1px 0 rgba(0,0,0,.26);
            -moz-box-shadow: inset 0 4px 9px rgba(0,0,0,.24);
            -webkit-box-shadow: inset 0 4px 9px rgba(0,0,0,.24);
            box-shadow: inset 0 4px 9px rgba(0,0,0,.24);
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <%--<div class="page_title">
                <span class="title_icon"><span class="computer_imac"></span></span>
                <h3 id="HProductionUp" runat="server">
                    PRODUCTION UPDATE</h3>
            </div>--%>
            <div id="content">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6 id="HProduction" runat="server">
                                    PRODUCTION DETAILS</h6>
                                <div class="btn_30_light" style="float: right;" id="dinadd" runat="server">
                                    <span class="icon add_co"></span>
                                    <asp:Button ID="btnAdd" runat="server" Text="Add New Record" CssClass="btn_link"
                                        OnClick="btnAdd_Click" CausesValidation="false" />
                                </div>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px;">
                                                <fieldset>
                                                    <legend id="lngdPUdtae" runat="server">PRODUCTION UPDATE INFO</legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="tableInner">
                                                        <tr>
                                                            <td width="80px" class="innerfield_title">
                                                                <asp:Label ID="lblPdate" runat="server" Text="Entry Date"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td width="150">
                                                                <asp:TextBox ID="txtPUdate" runat="server" MaxLength="15" Width="110" placeholder="DD/MM/YYYY"
                                                                    ValidationGroup="datecheckpodetail" onkeypress="return isNumberKeyWithslash(event);" Enabled="false" ></asp:TextBox>
                                                                <asp:ImageButton ID="imgPopup" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                    CausesValidation="false" runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender4" PopupButtonID="imgPopup" runat="server"
                                                                    TargetControlID="txtPUdate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                                <asp:RequiredFieldValidator ID="CV_PDate" runat="server" ControlToValidate="txtPUdate"
                                                                    ValidationGroup="datecheckpodetail" SetFocusOnError="true" ErrorMessage="Production Date is required!"
                                                                    Display="None"></asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                                                    TargetControlID="CV_PDate" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="../images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                                <asp:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="ValidateDate"
                                                                    ControlToValidate="txtPUdate" SetFocusOnError="true" ErrorMessage="* Invalid Date!"
                                                                    Display="None" ValidationGroup="datecheckpodetail" />
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server"
                                                                    TargetControlID="CustomValidator2" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="../images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                            </td>
                                                            <td width="110" class="innerfield_title" runat="server" id="divautogenerateheader">
                                                                <asp:Label ID="Label1" runat="server" Text="Production No"></asp:Label>
                                                            </td>
                                                            <td runat="server" id="divautogenerate">
                                                                <asp:TextBox ID="lblPUID" runat="server" Width="190" placeholder="Auto Generate No."
                                                                    Enabled="false" Font-Bold="true"> </asp:TextBox>
                                                            </td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlAdd1" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr >
                                            <td class="field_input" style="padding-left: 10px;">
                                                <fieldset>
                                                    <legend id="LgndTPU" runat="server">PURCHASE ORDER</legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="tableInner">
                                                        <tr style="display:none;">
                                                            <td width="80" class="innerfield_title">
                                                                <asp:Label ID="Label9" runat="server" Text="From Date"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td width="165">
                                                                <asp:TextBox ID="txtfromdate" runat="server" MaxLength="15" Width="120" placeholder="DD/MM/YYYY"
                                                                    ValidationGroup="datecheck" onkeypress="return isNumberKeyWithslash(event);" Enabled="false"></asp:TextBox>
                                                                <asp:ImageButton ID="ImageButton1" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                    CausesValidation="false" runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImageButton1"
                                                                    runat="server" TargetControlID="txtfromdate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                                <asp:RequiredFieldValidator ID="CV_FromDate" runat="server" ControlToValidate="txtfromdate"
                                                                    SetFocusOnError="true" ErrorMessage="From Date is required!" Display="None" ValidationGroup="datecheck"></asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
                                                                    TargetControlID="CV_FromDate" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="../images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                                <asp:CustomValidator ID="CV_FromDateValidation" runat="server" ClientValidationFunction="ValidateDate"
                                                                    ControlToValidate="txtfromdate" SetFocusOnError="true" ErrorMessage="* Invalid Date!"
                                                                    Display="None" ValidationGroup="datecheck" />
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" runat="server"
                                                                    TargetControlID="CV_FromDateValidation" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="../images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                                <asp:CompareValidator ID="Comparedate" ValidationGroup="datecheck" ForeColor="Red"
                                                                    runat="server" Display="None" ControlToValidate="txtfromdate" ControlToCompare="txttodate"
                                                                    Operator="LessThanEqual" Type="Date" ErrorMessage="Start date must be less than End date."></asp:CompareValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
                                                                    TargetControlID="Comparedate" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="../images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                            </td>
                                                            <td width="70" class="innerfield_title">
                                                                <asp:Label ID="Label10" runat="server" Text="To Date"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td width="165">
                                                                <asp:TextBox ID="txttodate" runat="server" MaxLength="15" Width="120" placeholder="DD/MM/YYYY"
                                                                    ValidationGroup="datecheck" Enabled="false"></asp:TextBox>
                                                                <asp:ImageButton ID="ImageButton2" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                    CausesValidation="false" runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImageButton2"
                                                                    runat="server" TargetControlID="txttodate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                                <asp:RequiredFieldValidator ID="CV_ToDate" runat="server" ControlToValidate="txttodate"
                                                                    SetFocusOnError="true" ErrorMessage="To Date is required!" Display="None" ValidationGroup="datecheck"></asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server"
                                                                    TargetControlID="CV_ToDate" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="../images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                                <asp:CustomValidator ID="CV_ToDateValidation" runat="server" ClientValidationFunction="ValidateDate"
                                                                    ControlToValidate="txttodate" SetFocusOnError="true" ErrorMessage="* Invalid Date!"
                                                                    Display="None" ValidationGroup="datecheck" />
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server"
                                                                    TargetControlID="CV_ToDateValidation" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="../images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                            </td>
                                                            <td width="110">
                                                            <div class="btn_24_blue">
                                                            <span class="icon find_co"></span>
                                                                <asp:Button ID="btngvfill" runat="server" Text="Search" CssClass="btn_link"
                                                                    ValidationGroup="datecheck" OnClick="btngvfill_Click" />
                                                             </div>
                                                            </td>                                                       
                                                            <td>
                                                                <asp:DropDownList ID="ddlPO" runat="server" AppendDataBoundItems="true" class="chosen-select"
                                                                    Style="width: 250px;" data-placeholder="-- Select --" OnSelectedIndexChanged="ddlPO_SelectedIndexChanged"
                                                                    AutoPostBack="true">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red"
                                                                    ErrorMessage="Required!" ControlToValidate="ddlPO" ValidateEmptyText="false"
                                                                    SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="80" class="innerfield_title">
                                                                <asp:Label ID="Label4" runat="server" Text="Product"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td colspan="5">
                                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                                    <tr>
                                                                        <td width="350">
                                                                <asp:DropDownList ID="ddlProductName" runat="server" Width="350"  AutoPostBack="true"
                                                                class="chosen-select" data-placeholder="Choose Product" AppendDataBoundItems="True" 
                                                                OnSelectedIndexChanged="ddlProductName_SelectedIndexChanged">
                                                                <asp:ListItem Text="SELECT PRODUCT NAME" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                
                                                            </td>
                                                            <td width="60" class="innerfield_title">
                                                                <asp:Label ID="Label5" runat="server" Text="Order No"></asp:Label>
                                                                 <span class="req">*</span>
                                                            </td>
                                                            <td width="640">
                                                                <asp:DropDownList ID="ddlpono" runat="server" Height="24px" CssClass="common-select" Width="95%" AutoPostBack="true" ValidationGroup="datecheckpodetail"
                                                                               AppendDataBoundItems="True"
                                                                              OnSelectedIndexChanged="ddlpono_SelectedIndexChanged" style="font-family: 'Courier New', Courier, monospace" >
                                                                <asp:ListItem Text="SELECT PO NO" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                
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
                                <div class="gridcontent-inner">
                                    <fieldset>
                                        <legend id="Legend1" runat="server">PRODUCT DETAILS</legend>                                                        
                                        <cc1:Grid ID="gvPOProduct" runat="server" CallbackMode="true" Serialize="true" FolderStyle="../GridStyles/premiere_blue"
                                            AutoGenerateColumns="false" AllowPageSizeSelection="false" AllowAddingRecords="false" PageSize="500" Width="100%"
                                            AllowSorting="false" AllowFiltering="false" AllowPaging="false" OnRowDataBound="gvPOProduct_RowDataBound"
                                            EnableRecordHover="true">
                                            <FilteringSettings InitialState="visible" />
                                            <Columns>
                                                <cc1:Column ID="Column4" DataField="PONO" ReadOnly="true" HeaderText="PO NO" Visible="false" runat="server"
                                                    Width="180" Wrap="true" />
                                                <cc1:Column ID="Column5" DataField="PRODUCTNAME" HeaderText="PRODUCT" runat="server"
                                                    Wrap="true" Width="350">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="EqualTo" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="DoesNotContain" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column6" DataField="TOTALQTY" HeaderText="ORDER QTY" runat="server"
                                                    Wrap="true" Width="180" />
                                                <cc1:Column DataField="REMAININGQTY" HeaderText="AVAILABLE QTY" Width="180"
                                                    Wrap="true">
                                                </cc1:Column>
                                                <cc1:Column DataField="" HeaderText="PRODUCTION QTY" Width="120" AllowEdit="true"
                                                    Wrap="true">
                                                    <TemplateSettings TemplateId="TplQuantity" />
                                                </cc1:Column>
                                                <cc1:Column DataField="UOMID" HeaderText="PACKSIZE" Width="210" AllowEdit="true"
                                                    Wrap="true">
                                                    <TemplateSettings TemplateId="tempUOM" />
                                                </cc1:Column>
                                                <cc1:Column ID="POID" DataField="POID" HeaderText="POID" runat="server" Width="10"
                                                    Visible="false" />
                                                <cc1:Column ID="columns77" DataField="PRODUCTID" HeaderText="PRODUCTID" runat="server"
                                                    Width="10" Visible="false" />
                                                <cc1:Column ID="Column7" DataField="MANFDATE" HeaderText="MFG. DATE" runat="server"
                                                    Wrap="true" AllowEdit="true" Width="130" Visible="false">
                                                    <TemplateSettings TemplateId="tempManfdate" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column11" DataField="BATCHNO" HeaderText="BATCHNO" runat="server"
                                                    AllowEdit="true" Wrap="true" Width="130"  Visible="false" >
                                                    <TemplateSettings TemplateId="tempbatchno"/>
                                                </cc1:Column>
                                                <cc1:Column ID="Column8" DataField="EXPRDATE" HeaderText="EXP. DATE" runat="server"
                                                    AllowEdit="true" Wrap="true" Width="130"  Visible="false" >
                                                    <TemplateSettings TemplateId="tempExpdate" />
                                                </cc1:Column>
                                            </Columns>
                                            <FilteringSettings MatchingType="AnyFilter" />
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="TplRemainigQuantity">
                                                </cc1:GridTemplate>
                                                <cc1:GridTemplate runat="server" ID="TplQuantity">
                                                    <Template>
                                                        <asp:HiddenField runat="server" ID="ProductID" Value='<%# Container.DataItem["PRODUCTID"] %>' />
                                                        <asp:HiddenField runat="server" ID="ProductName" Value='<%# Container.DataItem["PRODUCTNAME"] %>' />
                                                        <asp:TextBox runat="server" ID="Quantity" Text='<%# Container.DataItem["ACTLQTY"] %>' Height="20"
                                                            onkeypress="return isNumberKeyWithDot(event);" MaxLength="10" Width="90" AutoCompleteType="Disabled" onfocus="disableautocompletion(this.id);" />
                                                    </Template>
                                                </cc1:GridTemplate>
                                                <cc1:GridTemplate runat="server" ID="tempUOM" ControlID="ddlUOM">
                                                    <Template>
                                                        <asp:DropDownList ID="ddlUOM" runat="server" Width="100%" Height="20">
                                                        </asp:DropDownList>
                                                    </Template>
                                                </cc1:GridTemplate>
                                                <cc1:GridTemplate runat="server" ID="tempManfdate">
                                                    <Template>
                                                        <asp:TextBox runat="server" ID="txtmanfsate" Text='<%# Container.DataItem["MANFDATE"] %>'  Height="20"
                                                            Width="60%" ViewStateMode="Enabled" placeholder="dd/MM/yyyy" MaxLength="10" onkeypress="return isNumberKeyWithslash(event);" 
                                                            EnableViewState="true" OnTextChanged="txtmanfsate_TextChanged" AutoPostBack="true"   ForeColor="Black" />
                                                                
                                                            <cc1:Calendar ID="Calendar1" runat="server" DatePickerMode="true"  TextBoxId="txtmanfsate" DatePickerImagePath="../images/calendar.png" 
                                                            DatePickerSynchronize="true" AutoPostBack="true" CSSCalendar="customStyle1" DateFormat="dd/MM/yyyy" Columns="1" TextArrowLeft="&lt;" TextArrowRight="&gt;" Align="Under">
                                                            </cc1:Calendar>

                                                            <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ForeColor="Red"
                                                            ControlToValidate="txtmanfsate" ValidationGroup="A" ErrorMessage="Invalid" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$"></asp:RegularExpressionValidator>--%>
                                                    </Template>
                                                </cc1:GridTemplate>
                                                <cc1:GridTemplate runat="server" ID="tempbatchno">
                                                    <Template>
                                                        
                                                        <asp:TextBox runat="server" ID="txtbatchno" Text='<%# Container.DataItem["BATCHNO"] %>' Height="20" style="text-transform:uppercase;"
                                                            placeholder="Batch No" MaxLength="18" Width="100" EnableViewState="true" ViewStateMode="Enabled" onkeypress="return isNumberKeyWithoutamp(event);"
                                                            Enabled="true" ForeColor="Black"  AutoCompleteType="Disabled" onfocus="disableautocompletion(this.id);"/>
                                                    </Template>
                                                </cc1:GridTemplate>

                                                <cc1:GridTemplate runat="server" ID="tempExpdate">
                                                    <Template>
                                                        <asp:TextBox runat="server" ID="txtexpsate" Text='<%# Container.DataItem["EXPRDATE"] %>' Height="20"
                                                            Width="60%" ViewStateMode="Enabled" placeholder="dd/MM/yyyy" MaxLength="10" onkeypress="return isNumberKeyWithslash(event);"
                                                            EnableViewState="true" ForeColor="Black" />
                                                        <cc1:Calendar ID="Calendar2" runat="server" DatePickerMode="true"  TextBoxId="txtexpsate" DatePickerImagePath="../images/calendar.png"
                                                         DatePickerSynchronize="true" CSSCalendar="customStyle1" DateFormat="dd/MM/yyyy" Columns="1" TextArrowRight="&gt;" TextArrowLeft="&lt;">
                                                            </cc1:Calendar>
                                                        <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ForeColor="Red"
                                                            ControlToValidate="txtexpsate" ValidationGroup="A" ErrorMessage="Invalid" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$"></asp:RegularExpressionValidator>--%>
                                                    </Template>
                                                </cc1:GridTemplate>
                                                <%-- <cc1:GridTemplate runat="server" ID="tempExpdate">
                            <Template>                                               
                                <asp:Label ID="lblExpdate" runat="server" Text='<%# Container.DataItem["EXPRDATE"] %>' ></asp:Label>
                            </Template>
                        </cc1:GridTemplate>--%>
                                            </Templates>
                                            <ScrollingSettings ScrollWidth="100%" ScrollHeight="200" />
                                        </cc1:Grid>                                                        
                                    </fieldset>
                                </div>                                            
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td style="padding: 8px 0px 8px 10px;">
                                            <div class="btn_24_blue" >
                                                <span class="icon add_co"></span>
                                                <asp:Button ID="btnSubmit" runat="server" CausesValidation="false" CssClass="btn_link"
                                                    OnClick="btnSubmit_Click" Text="ADD" />
                                            </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                              <div class="btn_24_blue">
                                                    <span class="icon cross_octagon_co"></span>
                                                <asp:Button ID="btnCancel" runat="server" CausesValidation="false"
                                                    CssClass="btn_link" OnClick="btnCancel_Click" Text="Cancel" />
                                            </div>
                                                <asp:HiddenField ID="Hdn_Fld" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlDisplay2" runat="server">
                                    <div class="gridcontent-inner" id="divupdatedetails" runat="server">
                                    <fieldset>
                                      <legend id="Legend2" runat="server">PRODUCTION UPDATE DETAILS</legend>
                                        <cc1:Grid ID="gvPU" runat="server" CallbackMode="true" Serialize="true" FolderStyle="../GridStyles/premiere_blue"
                                            AutoGenerateColumns="false" AllowAddingRecords="false" AllowPageSizeSelection="false" AllowPaging="true" PageSize="500"
                                            EnableRecordHover="true" AllowFiltering="true" OnDeleteCommand="DeleteRecord">
                                            <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnClientDelete="OnDelete" />
                                            <FilteringSettings InitialState="visible" />
                                            <Columns>
                                                <cc1:Column ID="Column1" DataField="PONO" ReadOnly="true" HeaderText="PO NO" runat="server"
                                                    Wrap="true" Width="220" />
                                                <cc1:Column ID="Column2" DataField="PRODUCTNAME" HeaderText="PRODUCT" runat="server"
                                                    Wrap="true" Width="350">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="EqualTo" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="DoesNotContain" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column12" DataField="BATCHNO" HeaderText="BATCHNO" runat="server"
                                                    Wrap="true" Width="130" />
                                                <cc1:Column ID="Column3" DataField="TOTALQTY" HeaderText="ORDER QTY" runat="server"
                                                    Wrap="true" Width="200" />
                                                <cc1:Column DataField="REMAININGQTY" HeaderText="REMAINING QTY" Width="150"
                                                    Visible="false">
                                                </cc1:Column>
                                                <cc1:Column DataField="ACTLQTY" HeaderText="PRODUCTION QTY" Width="110" Wrap="true">
                                                </cc1:Column>
                                                <cc1:Column DataField="UOM" HeaderText="PACKSIZE" Width="200" Wrap="true">
                                                </cc1:Column>
                                                <cc1:Column ID="POIDEdit" DataField="POID" HeaderText="POID" runat="server" Width="180"
                                                    Visible="false" />
                                                <cc1:Column ID="PRODUCTIDEdit" DataField="PRODUCTID" HeaderText="PRODUCTID" runat="server"
                                                    Width="180" Visible="false" />
                                                <cc1:Column ID="Column9" DataField="MANFDATE" HeaderText="MFG. DATE" runat="server"
                                                    Wrap="true" Width="100">
                                                </cc1:Column>
                                                <cc1:Column ID="Column10" DataField="EXPRDATE" HeaderText="EXP. DATE" runat="server"
                                                    Wrap="true" Width="100">
                                                </cc1:Column>
                                                <cc1:Column HeaderText="DELETE" AllowEdit="false" AllowDelete="true" Width="80">
                                                    <TemplateSettings TemplateId="deleteBtnTemplate" />
                                                </cc1:Column>
                                            </Columns>
                                            <FilteringSettings MatchingType="AnyFilter" />
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="deleteBtnTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="action-icons c-delete" title="Delete" onclick="gvPU.delete_record(this)">
                                                        </a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <ScrollingSettings ScrollWidth="100%" ScrollHeight="200" />
                                        </cc1:Grid>
                                    </fieldset>
                                  </div>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="innerfield_title" style="padding-left: 20px;" width="110">
                                                <asp:Label ID="Label19" runat="server" Text="Total Pcs"></asp:Label>
                                            </td>
                                            <td class="innerfield_title" width="130">
                                            <asp:TextBox ID="txtcasepack" runat="server" MaxLength="15" CssClass="x-large" placeholder="Total Pcs" Enabled="false"
                                             Font-Bold="true"></asp:TextBox>
                                            </td>
                                            <td class="field_input" style="padding-left: 30px;">
                                            <div class="btn_24_blue" id="divbtnsave" runat="server">
                                              <span class="icon disk_co"></span>
                                                <asp:Button ID="btnSubmitFinal" runat="server" Text="SAVE" CssClass="btn_link"
                                                    ValidationGroup="pucheck" OnClick="btnSubmitFinal_Click" />
                                            </div>
                                                 &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
                                                    <span class="icon cross_octagon_co"></span>
                                                 <asp:Button ID="btnCancelFinal" runat="server" Text="Cancel" CssClass="btn_link"
                                                        CausesValidation="false" OnClick="btnCancelFinal_Click" />
                                                </div>
                                                <asp:HiddenField ID="hdnfinal" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlDisplay3" runat="server">
                                <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_title">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="90">
                                                            <asp:Label ID="Label2" runat="server" Text="From Date"></asp:Label> <span class="req">*</span>
                                                        </td>
                                                        <td width="165">
                                                            <asp:TextBox ID="txtsearchfromdate" runat="server" MaxLength="15" Width="120" Enabled="false" placeholder="dd/mm/yyyy"
                                                            ValidationGroup="datecheckme"></asp:TextBox>
                                                        <asp:ImageButton ID="imgPopupfromdatesearch" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle" runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="imgPopupfromdatesearch" runat="server" TargetControlID="txtsearchfromdate"
                                                               CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                        </ajaxToolkit:CalendarExtender>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtfromdate"
                                                            SetFocusOnError="true" ErrorMessage="From Date is required!" Display="None" ValidationGroup="datecheckme"></asp:RequiredFieldValidator>
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                            TargetControlID="RequiredFieldValidator2" PopupPosition="Right" HighlightCssClass="errormassage"
                                                            WarningIconImageUrl="../images/050.png">
                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                                        <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="ValidateDate"
                                                            ControlToValidate="txtfromdate" SetFocusOnError="true" ErrorMessage="* Invalid Date!"
                                                            Display="None" ValidationGroup="datecheckme" />
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                                            TargetControlID="CustomValidator1" PopupPosition="Right" HighlightCssClass="errormassage"
                                                            WarningIconImageUrl="../images/050.png">
                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                                        </td>
                                                        <td width="70"><asp:Label ID="Label3" runat="server" Text="To Date"></asp:Label> <span class="req">*</span></td>
                                                        <td width="165">
                                                            <asp:TextBox ID="txtsearchtodate" runat="server" MaxLength="10" Width="120" placeholder="dd/mm/yyyy"
                                                            ValidationGroup="datecheckme" Enabled="false"></asp:TextBox>
                                                        <asp:ImageButton ID="imgpopuptodatesearch" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle" runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender5" PopupButtonID="imgpopuptodatesearch" runat="server" TargetControlID="txtsearchtodate"
                                                               CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                        </ajaxToolkit:CalendarExtender>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtsearchtodate"
                                                            SetFocusOnError="true" ErrorMessage="To Date is required!" Display="None" ValidationGroup="datecheckme"></asp:RequiredFieldValidator>
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server"
                                                            TargetControlID="RequiredFieldValidator3" PopupPosition="Right" HighlightCssClass="errormassage"
                                                            WarningIconImageUrl="../images/050.png">
                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                                        <asp:CustomValidator ID="CustomValidator3" runat="server" ClientValidationFunction="ValidateDate"
                                                            ControlToValidate="txttodate" SetFocusOnError="true" ErrorMessage="* Invalid Date!"
                                                            Display="None" ValidationGroup="datecheckme" />
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server"
                                                            TargetControlID="CustomValidator3" PopupPosition="Right" HighlightCssClass="errormassage"
                                                            WarningIconImageUrl="../images/050.png">
                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                                        </td>                                                    
                                                        <td>
                                                            <div class="btn_24_blue">
                                                            <span class="icon find_co"></span>
                                                            <asp:Button ID="Button1" runat="server" Text="Search" CssClass="btn_link" ValidationGroup="datecheckme" OnClick="btnpusearch_Click" />
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
                                        <cc1:Grid ID="gvPUSheet" runat="server" CallbackMode="true" Serialize="true" FolderStyle="../GridStyles/premiere_blue"
                                            AutoGenerateColumns="false" AllowColumnResizing="true" AllowAddingRecords="false" AllowPaging="true" PageSize="500"
                                            EnableRecordHover="true" AllowPageSizeSelection="true" AllowFiltering="true">
                                            <FilteringSettings InitialState="visible" />
                                            <Columns>
                                                <cc1:Column DataField="ID" HeaderText="PRODUCTION ID" runat="server" Width="250"
                                                    Visible="false" />
                                                <cc1:Column ID="Column20" DataField="Sl" HeaderText="SL" runat="server" Width="60" Wrap="true">
                                                    <TemplateSettings TemplateId="slnoTemplateIN" />
                                                </cc1:Column>
                                                <cc1:Column DataField="PUNO" HeaderText="PRODUCTION NO" runat="server" Width="300" Wrap="true" />
                                                <cc1:Column DataField="PUDATE" HeaderText="PRODUCTION DATE" Width="100%" Wrap="true">
                                                </cc1:Column>
                                                <cc1:Column DataField="TOTALPRODUCT" HeaderText="TOTAL PRODUCT" Width="150" Wrap="true">
                                                </cc1:Column>
                                                <cc1:Column DataField="TOTALCASEPACK" HeaderText="TOTAL CASEPACK" Width="150" Wrap="true">
                                                </cc1:Column>
                                                <cc1:Column ID="Column13" AllowEdit="true" HeaderText="VIEW" runat="server" Width="60" Wrap="true">
                                                    <TemplateSettings TemplateId="viewBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column AllowEdit="true" HeaderText="EDIT" runat="server" Width="60" Wrap="true">
                                                    <TemplateSettings TemplateId="editBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column14" AllowEdit="true" HeaderText="DELETE" runat="server" Width="60" Wrap="true">
                                                    <TemplateSettings TemplateId="deleteBtnTemplate1" />
                                                </cc1:Column>
                                            </Columns>
                                            <FilteringSettings MatchingType="AnyFilter" />
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="editBtnTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="action-icons c-edit" title="Edit" id="btnGridEdit_<%# Container.PageRecordIndex %>"
                                                            onclick="CallServerMethod(this)"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="deleteBtnTemplate1">
                                                    <Template>
                                                        <a href="javascript: //" class="action-icons c-delete" title="Delete" id="btnGridDelete_<%# Container.PageRecordIndex %>"
                                                            onclick="CallDeleteMethod(this)"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="viewBtnTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="h_icon zoom_sl" title="View" id="btnGridView_<%# Container.PageRecordIndex %>"
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
                                        <asp:Button ID="btngridsave" runat="server" Text="GridSave" Style="display: none"
                                            OnClick="btngridsave_Click" CausesValidation="false" />
                                        <asp:Button ID="btngridView" runat="server" Text="GridView" Style="display: none"
                                            OnClick="btngridView_Click" CausesValidation="false" />
                                        <asp:Button ID="btnGridDelete" runat="server" Text="GridDelete" Style="display: none"
                                            OnClick="btnGridDelete_Click" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete this record?');"  />
                                        <asp:HiddenField ID="Hdnn_Fld" runat="server" />
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
                <cc1:MessageBox ID="MessageBox1" runat="server" />
                <span class="clear"></span>
            </div>
            <span class="clear"></span></div>
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
                    gvPUSheet.addFilterCriteria('PUNO', OboutGridFilterCriteria.Contains, searchValue);
                    gvPUSheet.addFilterCriteria('PUDATE', OboutGridFilterCriteria.Contains, searchValue);
                    gvPUSheet.executeFilter();
                    searchTimeout = null;
                    return false;
                }
                </script>
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
                function isNumberKeyWithslash(evt) {
                    var charCode = (evt.which) ? evt.which : event.keyCode;
                    if ((charCode < 48 || charCode > 57) && (charCode != 8) && (charCode != 47))
                        return false;

                    return true;
                }
            </script>
            <script type="text/javascript">
                function CallServerMethod(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnGridEdit_", "");
                    document.getElementById("<%=Hdnn_Fld.ClientID %>").value = gvPUSheet.Rows[iRecordIndex].Cells[0].Value;
                    document.getElementById("<%=btngridsave.ClientID %>").click();

                }
            </script>
            <script type="text/javascript">
                function CallDeleteMethod(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnGridDelete_", "");
                    document.getElementById("<%=Hdnn_Fld.ClientID %>").value = gvPUSheet.Rows[iRecordIndex].Cells[0].Value;
                    document.getElementById("<%=btnGridDelete.ClientID %>").click();

                }
            </script>
            <script type="text/javascript">
                function CallViewServerMethod(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnGridView_", "");
                    document.getElementById("<%=Hdnn_Fld.ClientID %>").value = gvPUSheet.Rows[iRecordIndex].Cells[0].Value;
                    document.getElementById("<%=btngridView.ClientID %>").click();
                }
            </script>
            <script type="text/javascript">
                function isNumberKeyWithDot(evt) {
                    var charCode = (evt.which) ? evt.which : event.keyCode;
                    if ((charCode < 48 || charCode > 57) && (charCode != 8) && (charCode != 46))
                        return false;

                    return true;
                }

                function isNumberKeyWithoutamp(evt) {
                    var charCode = (evt.which) ? evt.which : event.keyCode;
                    if ((charCode != 38) && (charCode != 39))
                        return true;

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
