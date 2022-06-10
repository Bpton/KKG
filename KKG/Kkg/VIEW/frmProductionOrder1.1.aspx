<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmProductionOrder1.1.aspx.cs" Inherits="VIEW_frmProductionOrder1_1" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

      <style type="text/css">  
        .Background  
        {  
            background-color: Black;  
            filter: alpha(opacity=90);  
            opacity: 0.8;  
        }  
        .Popup  
        {  
            background-color: #FFFFFF;  
            border-width: 3px;  
            border-style: solid;  
            border-color: black;  
            padding-top: 10px;  
            padding-left: 10px;  
            width: 1000px;  
            height: 500px;  
        }  
        .lbl  
        {  
            font-size:16px;  
            font-style:italic;  
            font-weight:bold;  
        }  
    </style> 
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
                                <h6>Manage Production Order</h6>
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
                                                <fieldset>
                                                    <legend>Production Details</legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                                        <tr>
                                                            <td class="field_title" id="divsaleorderno" runat="server">
                                                                <asp:Label ID="Label15" runat="server" Text="Requsition No"></asp:Label>
                                                            </td>
                                                            <td class="field_input" id="divsaleorderno1" runat="server">
                                                                <asp:TextBox ID="txtrequisitionno" runat="server" Width="120" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td class="field_title">
                                                                <asp:Label ID="Label6" runat="server" Text="Entry Date"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtrequisiondate" runat="server" Width="80" placeholder="dd/mm/yyyy"
                                                                    Enabled="false" MaxLength="10" ValidationGroup="datecheckpodetail" onkeypress="return isNumberKeyWithslash(event);"></asp:TextBox>
                                                                <asp:ImageButton ID="imgPopuppodate" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender6" PopupButtonID="imgPopuppodate"
                                                                    runat="server" TargetControlID="txtrequisiondate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtrequisiondate"
                                                                    ValidationGroup="datecheckpodetail" SetFocusOnError="true" ErrorMessage="Requistion Date is required!"
                                                                    Display="None"></asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                                                    TargetControlID="RequiredFieldValidator2" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="~/images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                            </td>
                                                            <td class="field_title">
                                                                <asp:Label ID="lblfactory" runat="server" Text="Factory"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:DropDownList ID="ddlfactory" runat="server" AppendDataBoundItems="true" class="chosen-select"
                                                                    Style="width: 380px;" data-placeholder="Choose a Factory" OnSelectedIndexChanged="ddlfactory_SelectedIndexChanged" AutoPostBack="true">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="Reqddlfactory" runat="server" ControlToValidate="ddlfactory"
                                                                    InitialValue="0" SetFocusOnError="true" ErrorMessage="*" ForeColor="Red" ValidationGroup="POFooter"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="trAddProduct">
                                            <td class="field_input" style="padding-left: 10px;">
                                                <fieldset>
                                                    <legend>Material Details</legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                                        <tr>
                                                            <td width="90" class="field_title">
                                                                <asp:Label ID="Label3" runat="server" Text="CatEgory"></asp:Label>&nbsp;<span class="req">*</span>
                                                            </td>
                                                            <td width="170" class="field_input" >
                                                                <asp:DropDownList ID="ddlsupplieditem" runat="server" Width="220" AutoPostBack="true"
                                                                    ValidationGroup="datecheckpodetail" OnSelectedIndexChanged="ddlsupplieditem_SelectedIndexChanged"
                                                                    class="chosen-select" data-placeholder="Select CatEgory">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlsupplieditem"
                                                                    ValidationGroup="datecheckpodetail" InitialValue="0" SetFocusOnError="true" ErrorMessage="Required"
                                                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>&nbsp;
                                                            </td>

                                                            <td class="field_title" Width="220">
                                                                <asp:Label ID="Label5" runat="server" Text=" Destination Department"></asp:Label>
                                                            </td>
                                                            <td class="field_input" colspan="8">
                                                                <asp:DropDownList ID="ddldepartment" runat="server" Width="220" AutoPostBack="true"
                                                                    ValidationGroup="POFooter" class="chosen-select" data-placeholder="Select Department" OnSelectedIndexChanged="ddldepartment_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfv_ddldepartment" runat="server" ControlToValidate="ddldepartment"
                                                                    ValidationGroup="POFooter" InitialValue="0" SetFocusOnError="true" ErrorMessage="Required"
                                                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </td>

                                                            <td>&nbsp;
                                                            </td>

                                                             <td Width="220" class="field_title">
                                                                <asp:Label ID="lblStorelocation" runat="server" Text="Destination Storelocation"></asp:Label>&nbsp;<span class="req">*</span>
                                                            </td>
                                                            <td width="100" class="field_input" colspan="4">
                                                                <asp:DropDownList ID="ddlStorelocation" runat="server" Width="220" AutoPostBack="true"
                                                                    ValidationGroup="datecheckpodetail" OnSelectedIndexChanged="ddlStorelocation_SelectedIndexChanged"
                                                                    class="chosen-select" data-placeholder="Select Storelocation">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="90" class="field_title">
                                                                <asp:Label ID="Label2" runat="server" Text="Material"></asp:Label>&nbsp;<span class="req">*</span>
                                                            </td>
                                                            <td width="360" class="field_input">
                                                                <asp:DropDownList ID="ddlProductName" runat="server" Width="350" ValidationGroup="datecheckpodetail" AutoPostBack="true"
                                                                    class="chosen-select" data-placeholder="Select Material" OnSelectedIndexChanged="ddlProductName_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="CV_PRODUCTNAME" runat="server" ControlToValidate="ddlProductName"
                                                                    ValidationGroup="datecheckpodetail" InitialValue="0" SetFocusOnError="true" ErrorMessage="*"
                                                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </td>
                                                            
                                                             <td width="130 class="field_title">
                                                               <span class="label-info">Stock Qty
                                                                <asp:Label ID="lblFgStockQty" runat="server" Text="0"></asp:Label>&nbsp;
                                                                     </span>
                                                            </td>
                                                            <td width="50" class="field_title">
                                                                <asp:Label ID="Label8" runat="server" Text="UNIT"></asp:Label>&nbsp;<span class="req">*</span>
                                                            </td>
                                                            <td width="130" class="field_input">
                                                                <asp:DropDownList ID="ddlpackingsize" runat="server" Width="120" ValidationGroup="datecheckpodetail"
                                                                    class="chosen-select" data-placeholder="Select Unit">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="CV_CASE" runat="server" ControlToValidate="ddlpackingsize"
                                                                    ValidationGroup="datecheckpodetail" InitialValue="0" SetFocusOnError="true" ErrorMessage="*"
                                                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td width="40" class="field_title" style="display:none">
                                                               STOCK QTY
                                                            </td>
                                                            <td width="60" class="field_input" style="display:none">
                                                                <asp:TextBox ID="txtStockQty" runat="server" Width="50" 
                                                                    data-placeholder="Stock Qty">
                                                                </asp:TextBox>
                                                            </td>

                                                            <td width="40" class="field_title">
                                                                <asp:Label ID="Label4" runat="server" Text="Qty"></asp:Label>&nbsp;<span class="req">*</span>
                                                            </td>
                                                            <td width="80" class="field_input">
                                                                <asp:TextBox ID="txtqty" runat="server" Width="70" onkeypress="return isNumberKey(event);"
                                                                    ValidationGroup="datecheckpodetail" OnTextChanged="txtqty_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="CV_QTY" runat="server" ControlToValidate="txtqty"
                                                                    ValidationGroup="datecheckpodetail" SetFocusOnError="true" ErrorMessage="Qty is required!"
                                                                    Display="None"></asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
                                                                    TargetControlID="CV_QTY" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="~/images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                            </td>
                                                            <td width="80" class="field_title">
                                                                <asp:Label ID="Label1" runat="server" Text="Required By"></asp:Label>&nbsp;<span
                                                                    class="req">*</span>
                                                            </td>
                                                            <td width="110" class="field_input" colspan="8">
                                                                <asp:TextBox ID="txtrequiredfromdate" runat="server" Width="65" Enabled="false" placeholder="dd/mm/yyyy"
                                                                    MaxLength="10" ValidationGroup="datecheckpodetail" onkeypress="return isNumberKeyWithslash(event);"></asp:TextBox>
                                                                <asp:ImageButton ID="imgPopup" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender4" PopupButtonID="imgPopup" runat="server"
                                                                    TargetControlID="txtrequiredfromdate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                                <%--<span class="label_intro">
                                                                    <asp:Label ID="Label26" runat="server" Text="Required From Date"></asp:Label>
                                                                    <span class="req">*</span></span>--%>
                                                                <asp:RequiredFieldValidator ID="CV_Date" runat="server" ControlToValidate="txtrequiredfromdate"
                                                                    ValidationGroup="datecheckpodetail" SetFocusOnError="true" ErrorMessage="Required Date is required!"
                                                                    Display="None"></asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender15" runat="server"
                                                                    TargetControlID="CV_Date" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="../images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                                <asp:CustomValidator ID="CV_SchduledDateValidation" runat="server" ClientValidationFunction="ValidateDate"
                                                                    ControlToValidate="txtrequiredfromdate" SetFocusOnError="true" ErrorMessage="* Invalid Date!"
                                                                    Display="None" ValidationGroup="datecheckpodetail" />
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender16" runat="server"
                                                                    TargetControlID="CV_SchduledDateValidation" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="../images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                            </td>
                                                            <td width="120" class="field_input" style="display: none;">
                                                                <asp:TextBox ID="txttorequireddate" runat="server" Width="70" Enabled="false" placeholder="dd/mm/yyyy"
                                                                    MaxLength="10" ValidationGroup="datecheckpodetail" onkeypress="return isNumberKeyWithslash(event);"></asp:TextBox>
                                                                <asp:ImageButton ID="imgpopupto" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender5" PopupButtonID="imgpopupto" runat="server"
                                                                    TargetControlID="txttorequireddate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                                <%--<span class="label_intro">
                                                                    <asp:Label ID="Label27" runat="server" Text="Required To Date"></asp:Label>
                                                                    <span class="req">*</span></span>--%>
                                                                <asp:RequiredFieldValidator ID="CV_To_Date" runat="server" ControlToValidate="txttorequireddate"
                                                                    ValidationGroup="datecheckpodetail" SetFocusOnError="true" ErrorMessage="Required To Date is required!"
                                                                    Display="None"></asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender19" runat="server"
                                                                    TargetControlID="CV_To_Date" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="~/images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                                <asp:CustomValidator ID="CV_SchduledTODateValidation" runat="server" ClientValidationFunction="ValidateDate"
                                                                    ControlToValidate="txttorequireddate" SetFocusOnError="true" ErrorMessage="* Invalid Date!"
                                                                    Display="None" ValidationGroup="datecheckpodetail" />
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender20" runat="server"
                                                                    TargetControlID="CV_SchduledTODateValidation" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="~/images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                            </td>
                                                            <td class="field_title" colspan="8">
                                                            <asp:Button ID="Button1" CssClass="btn_link" runat="server" Text="Check&Save"   Style="display: none"/>
                                                            <div class="btn_24_blue" style="display:none">
                                                                    <span class="icon show"></span>
                                                                <asp:Button ID="Btnshow" runat="server" Text="Bom"  OnClick="Btnshow_Click" CssClass="btn_link"  />
                                                                  </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                              <body>  
                                 <ajaxToolkit:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panl1" TargetControlID="Button1"  
                                     CancelControlID="Button2" BackgroundCssClass="Background">  
                                </ajaxToolkit:ModalPopupExtender>  
                                     <asp:Panel ID="Panl1" runat="server" CssClass="Popup" align="center" style = "display:none">  
                         <br/>  
                             
                                         <legend>Bom Details</legend>
                                         <legend>--------------------------</legend>
                                     <div style="overflow: scroll; height: 300px;" onscroll="OnScrollDiv(this)" id="DivMainContent">
                                         <asp:GridView ID="gvBomDetails" runat="server" Width="100%" CssClass="zebra" OnRowDataBound="gvBomDetails_RowDataBound"
                                                AutoGenerateColumns="false" ShowFooter="false" AlternatingRowStyle-BackColor="AliceBlue"
                                                EmptyDataText="No Records Available" Style="height: 10px; overflow: auto">
                                             <Columns>

                                                 <asp:BoundField HeaderText="Row Number" DataField="SLNO" />
                                                 <asp:BoundField HeaderText="Category Name" DataField="CATNAME" />
                                                 <asp:BoundField HeaderText="Product Name" DataField="NAME" />
                                                 <asp:BoundField HeaderText="Pack Size" DataField="UOMNAME" />
                                                 <asp:BoundField HeaderText="QTY" DataField="QTY" />

                                                 <asp:TemplateField HeaderText="NETQTY">
                                                            <ItemTemplate>
                                                                 <asp:Label ID="grdLblNetQty" runat="server" Text='<%# Bind("NETQTY") %>' />
                                                                </ItemTemplate>
                                                  </asp:TemplateField> 
                                                 
                                                 <asp:TemplateField HeaderText="STOCKQTY">
                                                      <ItemTemplate>
                                                                 <asp:Label ID="grdLblStockQty" runat="server" Text='<%# Bind("STOCKQTY") %>' />
                                                        </ItemTemplate>
                                                  </asp:TemplateField>
                                                 
                                                 <asp:BoundField HeaderText="MAPPED TOLOCATION" DataField="TOLOCATION" />
                                                 
                                             </Columns>

                                         </asp:GridView>
                                      </div>
                                        <legend>----------------------</legend>
                                             <div class="btn_24_red">
                                                   <span class="icon cross_octagon_co"></span>
                                                 <asp:Button ID="Button2" runat="server" Text="Close"  CssClass="btn_link"/>  
                                                  </div>
                                             
                                             </asp:Panel>  
                                       </body>
                                                       
                                                        <tr>
                                                            <td class="field_input">
                                                                <div class="btn_24_blue">
                                                                    <span class="icon add_co"></span>
                                                                    <asp:Button ID="btnadd" runat="server" Text="Add" CssClass="btn_link" OnClick="btnadd_Click"
                                                                        CausesValidation="false" />
                                                                </div>
                                                                <asp:HiddenField ID="Hdn_Fld" runat="server" />
                                                                <asp:HiddenField ID="hdn_requisitionguiddelete" runat="server" />
                                                                 <asp:HiddenField ID="hdn_ProductStockQty" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="gridcontent-inner">
                                        <fieldset>
                                            <legend>PRODUCTION MATERIAL DETAILS</legend>
                                            <cc1:Grid ID="gvRequisition" runat="server" CallbackMode="true" Serialize="true"
                                                ShowFooter="true" ShowColumnsFooter="true" AutoGenerateColumns="false" EnableRecordHover="true"
                                                AllowAddingRecords="false" PageSize="500" AllowFiltering="true" AllowSorting="false"
                                                AllowPageSizeSelection="false" FolderStyle="../GridStyles/premiere_blue">
                                                <FilteringSettings InitialState="Visible" />
                                                <Columns>
                                                    <cc1:Column ID="Column16" DataField="GUID" ReadOnly="true" HeaderText="ID" runat="server"
                                                        Visible="false" Width="100" />
                                                    <cc1:Column ID="Column12" DataField="Sl" HeaderText="SL" runat="server" Width="60">
                                                        <TemplateSettings TemplateId="slnoTemplate" />
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column1" DataField="CATEGORYID" HeaderText="CATEGORY" runat="server"
                                                        Visible="false" Width="200" Wrap="true">
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
                                                    <cc1:Column ID="Column5" DataField="CATEGORYNAME" HeaderText="CATEGORY" runat="server"
                                                        Width="200" Wrap="true">
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
                                                    <cc1:Column ID="Column2" DataField="ITEMNAME" HeaderText="MATERIAL" runat="server"
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
                                                   <%-- <cc1:Column ID="Column8" DataField="STOCKQTY" HeaderText="STOCK QTY" runat="server"
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
                                                    </cc1:Column>--%>

                                                    <cc1:Column ID="Column17" DataField="PRODUCTIONQTY" HeaderText="PRODUCTION QTY" runat="server"
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

                                                    <cc1:Column ID="Column18" DataField="BUFFERQTY" HeaderText="BUFFER QTY" runat="server"
                                                        Visible="false" Width="200" Wrap="true" >
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

                                                    <cc1:Column ID="Column3" DataField="QTY" HeaderText="PRODUCTION QTY" runat="server"
                                                        Width="200" Wrap="true" Visible="false">
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
                                                    <cc1:Column ID="Column11" DataField="PACKSIZENAME" HeaderText="UNIT" runat="server" Width="170"
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
                                                    <cc1:Column ID="Column15" DataField="REQUIREDFROMDATE" HeaderText="REQUIRED FROM DATE"
                                                        runat="server" Width="150" Wrap="true">
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
                                                    <cc1:Column ID="Column10" DataField="REQUIREDTODATE" HeaderText="REQUIRED TO DATE" runat="server"
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
                                                    <cc1:Column HeaderText="DELETE" AllowEdit="false" AllowDelete="true" Width="100%" >
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
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px;">
                                                <fieldset>
                                                    <legend>REMARKS & OTHERS</legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td width="320">
                                                                <asp:TextBox ID="txtremarks" runat="server" MaxLength="255" Height="60" TextMode="MultiLine"
                                                                    CssClass="large" placeholder="Remarks" class="input_grow"></asp:TextBox>
                                                            </td>
                                                            <td width="15">&nbsp;
                                                            </td>
                                                            <td>
                                                                <div class="btn_24_green" runat="server" id="divBtnSave">
                                                                    <span class="icon green"></span>
                                                                    <asp:Button ID="btnsave" runat="server" Text="SAVE" CssClass="btn_24_green" ValidationGroup="POFooter"
                                                                        OnClick="btnsave_Click" />
                                                                </div>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                                <div class="btn_24_blue">
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
                                                        </td>
                                                        <td width="165">
                                                            <asp:TextBox ID="txtfromdate" runat="server" Width="80" Enabled="false"
                                                                placeholder="dd/mm/yyyy" ValidationGroup="datecheck"></asp:TextBox>
                                                            <asp:ImageButton ID="imgPopupfromdate" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopupfromdate"
                                                                runat="server" TargetControlID="txtfromdate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                        <td width="70">
                                                            <asp:Label ID="Label10" runat="server" Text="To Date"></asp:Label>
                                                        </td>
                                                        <td width="165">
                                                            <asp:TextBox ID="txttodate" runat="server" Width="80" placeholder="dd/mm/yyyy"
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
                                                                <asp:Button ID="btnseach" runat="server" Text="Search" CssClass="btn_link" ValidationGroup="datecheck"
                                                                    OnClick="btnseach_Click" />
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
                                        <cc1:Grid ID="gvRequisitiondetails" runat="server" CallbackMode="false" Serialize="true"
                                            AutoGenerateColumns="false" AllowAddingRecords="false" AllowFiltering="true"
                                            AllowPageSizeSelection="true" PageSize="500" FolderStyle="../GridStyles/premiere_blue"
                                            EnableRecordHover="true">
                                            <FilteringSettings InitialState="Visible" />
                                            <Columns>
                                                <cc1:Column ID="Column14" DataField="PRODUCTIONID" HeaderText="PRODUCTIONID" runat="server"
                                                    Width="10" Visible="false">
                                                </cc1:Column>
                                                <cc1:Column ID="Column4" DataField="Sl" HeaderText="SL" runat="server" Width="60">
                                                    <TemplateSettings TemplateId="slnoTemplatePO" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column6" DataField="PRODUCTIONDATE" HeaderText="PRODUCTION DATE"
                                                    runat="server" Width="70%" Wrap="true">
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
                                                <cc1:Column ID="Column7" DataField="PRODUCTIONNO" HeaderText="PRODUCTION NO" runat="server"
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
                                               
                                                <cc1:Column ID="Column23" DataField="CREATEDBY" HeaderText="CREATED FROM" runat="server"
                                                    Width="150" Wrap="true" Visible="false">
                                                </cc1:Column>
                                                <cc1:Column ID="Column20" AllowEdit="true" AllowDelete="true" HeaderText="VIEW" runat="server"
                                                    Width="60">
                                                    <TemplateSettings TemplateId="VIEWBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column13" AllowEdit="true" AllowDelete="true" HeaderText="EDIT" runat="server"
                                                    Width="60">
                                                    <TemplateSettings TemplateId="editBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column333" AllowEdit="false" AllowDelete="false" HeaderText="PRINT"
                                                    runat="server" Width="80" Wrap="true" Visible="false">
                                                    <TemplateSettings TemplateId="PrintBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column HeaderText="DELETE" AllowEdit="false" AllowDelete="true" Width="95" Visible="false">
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
                                                <cc1:GridTemplate runat="server" ID="PrintBtnTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="filter_btn printer_co" id="btnPOPrint_<%# Container.PageRecordIndex %>"
                                                            onclick="CallServerMethodPrint(this)" title="Print"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="deleteBtnTemplatePO">
                                                    <Template>
                                                        <a href="javascript: //" class="action-icons c-delete" title="Delete" id="btnGridFinalDelete_<%# Container.PageRecordIndex %>"
                                                            onclick="CallFinalDeleteServerMethod(this)"></a>
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
                                        <asp:Button ID="btnPOPrint" runat="server" Text="Print"  Style="display: none" OnClick="btnNRGPPrint_Click"
                                            CausesValidation="false" />
                                        <cc1:ConfirmBox ID="ConfirmBox1" runat="server" />
                                        <asp:Button ID="btnfinalgrdDelete" runat="server" Text="Delete" Style="display: none"
                                            OnClick="btnfinalgrdDelete_Click" CausesValidation="false" OnClientClick="ShowConfirmBox(this,'Are you sure you want to delete this record?'); return false;" />
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

                function CallServerMethod(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnGridEdit_", "");
                    document.getElementById("<%=Hdn_Fld.ClientID %>").value = gvRequisitiondetails.Rows[iRecordIndex].Cells[0].Value;
                    document.getElementById("<%=btngrdedit.ClientID %>").click();

                }

                function CallViewServerMethod(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnGridView_", "");
                    document.getElementById("<%=Hdn_Fld.ClientID %>").value = gvRequisitiondetails.Rows[iRecordIndex].Cells[0].Value;
                    document.getElementById("<%=btngrdview.ClientID %>").click();
                }


                function CallDeleteServerMethod(oLink) {
                    var iRecordIndexdelete = oLink.id.toString().replace("btnGridDelete_", "");
                    document.getElementById("<%=hdn_requisitionguiddelete.ClientID %>").value = gvRequisition.Rows[iRecordIndexdelete].Cells[0].Value;
                    document.getElementById("<%=btngrddelete.ClientID %>").click();
                }

                function CallFinalDeleteServerMethod(oLink) {
                    var iRecordIndexdelete = oLink.id.toString().replace("btnGridFinalDelete_", "");
                    document.getElementById("<%=Hdn_Fld.ClientID %>").value = gvRequisitiondetails.Rows[iRecordIndexdelete].Cells[0].Value;
                    document.getElementById("<%=btnfinalgrdDelete.ClientID %>").click();
                }

                function CallServerMethodPrint(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnPOPrint_", "");
                    document.getElementById("<%=Hdn_Fld.ClientID %>").value = gvRequisitiondetails.Rows[iRecordIndex].Cells[0].Value;
                    document.getElementById("<%=btnPOPrint.ClientID %>").click();

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

