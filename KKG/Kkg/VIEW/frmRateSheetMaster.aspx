<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmRateSheetMaster.aspx.cs" Inherits="VIEW_frmRateSheetMaster" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register assembly="ProudMonkey.Common.Controls" namespace="ProudMonkey.Common.Controls" tagprefix="cc1" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../css/bootstrap-multiselect.css" rel="stylesheet" />
    <script type="text/javascript" src="../js/bootstrap.js"></script>
    <script type="text/javascript" src="../js/bootstrap-multiselect.js"></script>
<script type="text/javascript">

    
    $(document).ready(function () {
        $("form").bind("keypress", function (e) {
            if (e.keyCode == 13) {
                alert("Enter key is block for this page!!!!");/*AS PER REQUIREMENT*/
                return false;
            }
        });
    })

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

     $(function () {
            $('#ContentPlaceHolder1_ddlCategory').multiselect({
                includeSelectAllOption: true
            });
            $("#ContentPlaceHolder1_ddlCategory").multiselect('selectAll', false);
            $("#ContentPlaceHolder1_ddlCategory").multiselect('updateButtonText');
        });

    </script>
     <script type="text/javascript">
        function exportToExcel() {
            gvRatesheet.exportToExcel();
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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

<asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
            <asp:Image ID="Image1" ImageUrl="../images/loading123.gif" AlternateText="Processing"
                runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
        PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
        <%--<div class="page_title">
                <span class="title_icon"><span class="computer_imac"></span></span>
                <h3>PRODUCT RATE SHEET</h3>
            </div>--%>
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>Product Rate Sheet Details</h6>
                                     <div class="btn_30_light" style="float: right;" id="btnaddhide" runat="server">
                                    <span class="icon add_co"></span>
                                    <asp:Button ID="btnAddRateSheet" runat="server" Text="Add New Record" CssClass="btn_link"
                                        OnClick="btnAddRateSheet_Click" CausesValidation="false" /></div>
                        </div>
                        <div class="widget_content">
                         <asp:Panel ID="pnlAdd" runat="server">
                            <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">

                                        <tr id="hdnTrRdb" runat="server">
                                            <td width="130" class="field_title">
                                                <asp:Label ID="Label2" Text="Sales Location Type" runat="server"></asp:Label><span class="req">*</span>
                                            </td>
                                            <td width="255" class="field_input">
                                                  <asp:RadioButton ID="rdbTPU" runat="server" Text="TPU/VENDOR" GroupName="BR" 
                                                  AutoPostBack="true" CssClass="innerfield_title" oncheckedchanged="rdbTPU_CheckedChanged" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:RadioButton ID="rdbFactory" runat="server" Text="FACTORY" CssClass="innerfield_title"
                                                                GroupName="BR" oncheckedchanged="rdbFactory_CheckedChanged"  AutoPostBack="true" />
                                                               
                                            </td>
                                            <td class="field_input">&nbsp;</td>
                                            <td class="field_input">&nbsp;</td>
                                            <td class="field_input">&nbsp;</td>
                                            <td class="field_input">&nbsp;</td>
                                        </tr>
                                        <tr id="TrName" runat="server">
                                            <td width="130" class="field_title">
                                                <asp:Label ID="Label6" Text="Name" runat="server"></asp:Label><span class="req">*</span>
                                            </td>
                                            <td width="255" class="field_input">
                                                 <asp:Label ID="lblName"  runat="server"></asp:Label><span class="req"></span>                                                               
                                            </td>                                            
                                        </tr>
                                        <tr>
                                        <td width="100" class="field_title" runat="server" id="trSuplied">
                                                <asp:Label ID="Label7" Text="Supplied Item" runat="server"></asp:Label><span class="req">*</span>
                                            </td>
                                            <td width="200" class="field_input" runat="server" id="trSuplied2">
                                                <asp:DropDownList ID="ddlsupplieditem" runat="server" AppendDataBoundItems="true" class="chosen-select"
                                                    Style="width: 250px;" data-placeholder="Choose an ItemType" AutoPostBack="true"
                                                    onselectedindexchanged="ddlsupplieditem_SelectedIndexChanged" >
                                                </asp:DropDownList>
                                               <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="A" ForeColor="Red"
                                                    ErrorMessage="Vendor is required!" ControlToValidate="ddlVendor" ValidateEmptyText="false"
                                                    SetFocusOnError="true" InitialValue="0" > </asp:RequiredFieldValidator>--%>
                                            </td>
                                            <td width="100" class="field_title"  id="hdnVendor" runat="server">
                                                <asp:Label ID="Label1" Text="Vendor" runat="server"></asp:Label><span class="req">*</span>
                                            </td>
                                            <td width="200" class="field_input"  id="hdnVendor2" runat="server">
                                                <asp:DropDownList ID="ddlVendor" runat="server" AppendDataBoundItems="true" class="chosen-select"
                                                    Style="width: 250px;" data-placeholder="Choose a Vendor"  AutoPostBack="true"
                                                    onselectedindexchanged="ddlVendor_SelectedIndexChanged" >
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="A" ForeColor="Red"
                                                    ErrorMessage="Vendor is required!" ControlToValidate="ddlVendor" ValidateEmptyText="false"
                                                    SetFocusOnError="true" InitialValue="0" > </asp:RequiredFieldValidator>
                                            </td>
                                            <td width="130" class="field_title" id="hdnFactory" runat="server">
                                                <asp:Label ID="Label3" Text="Factory Name" runat="server"></asp:Label><span class="req">*</span>
                                            </td>
                                            <td width="255" class="field_input" id="hdnFactory2" runat="server">
                                                <asp:DropDownList ID="ddlFactory" runat="server" AppendDataBoundItems="true" class="chosen-select"
                                                    Style="width: 250px;" data-placeholder="Choose a Factory" 
                                                    >
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="A" ForeColor="Red"
                                                    ErrorMessage="Factory is required!" ControlToValidate="ddlFactory" ValidateEmptyText="false"
                                                    SetFocusOnError="true" InitialValue="0" > </asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="130" class="field_title">
                                                <asp:Label ID="lblDiv" Text="Brand/Primary Item" runat="server"></asp:Label><span class="req">*</span>
                                            </td>
                                            <td width="255" class="field_input">
                                                <asp:DropDownList ID="ddlDivision" runat="server" AppendDataBoundItems="true" class="chosen-select"
                                                    Style="width: 250px;" data-placeholder="Select Brand"  AutoPostBack="true"
                                                    onselectedindexchanged="ddlDivision_SelectedIndexChanged" >
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="A" ForeColor="Red"
                                                    ErrorMessage="Brand is required!" ControlToValidate="ddlDivision" ValidateEmptyText="false"
                                                    SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
                                            </td>
                                            <td width="130" class="field_title">
                                                <asp:Label ID="lblCat" Text="Category/Sub Item" runat="server"></asp:Label><span class="req">*</span>
                                            </td>
                                            <td width="255" class="field_input">
                                                <%--<asp:DropDownList ID="ddlCategory" runat="server" AppendDataBoundItems="true" class="chosen-select"
                                                    Style="width: 250px;" data-placeholder="Choose a Category"
                                                    OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" >
                                                </asp:DropDownList>  --%>

                                                <asp:ListBox ID="ddlCategory" runat="server" SelectionMode="Multiple" 
                                                                    TabIndex="4" Width="200" ValidationGroup="A" AppendDataBoundItems="True"  OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                                                </asp:ListBox>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="A" ForeColor="Red"
                                                    ErrorMessage="Category is required!" ControlToValidate="ddlCategory" ValidateEmptyText="false"
                                                    SetFocusOnError="true" InitialValue="0" > </asp:RequiredFieldValidator>                                              
                                            </td>
                                               <td class="field_input">
                                                <div class="btn_24_blue" >
                                                <span class="icon exclamation_co"></span>
                                                <asp:Button ID="btnshow" runat="server" Text="Show" CssClass="btn_link" onclick="btnshow_Click" />
                                                </div>
                                                
                                            </td>
                                            </tr>
                                            <tr>
                                            <td  width="130" class="field_title">
                                                            <asp:Label ID="Label16" runat="server" Text="From Date"></asp:Label>
                                                            <span class="req">*</span>
                                           </td>
                                           <td width="255" class="field_input">
                                                            <asp:TextBox ID="txtFromDate" runat="server" MaxLength="10" Width="120" Enabled="false"
                                                                placeholder="dd/MM/yyyy" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="Search"
                                                                Font-Bold="true"></asp:TextBox>
                                                            <asp:ImageButton ID="imgPopup" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarFromDate" PopupButtonID="imgPopup" runat="server"
                                                                TargetControlID="txtFromDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorFromDate" runat="server" ControlToValidate="txtFromDate"
                                                                SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="Search" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </td>

                                             <td width="130" class="field_title">
                                                            <asp:Label ID="Label19" runat="server" Text="To Date"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>

                                            <td width="255" class="field_input">
                                                            <asp:TextBox ID="txtToDate" runat="server" MaxLength="10" Width="120" Enabled="false"
                                                                placeholder="dd/mm/yyyy" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="Search"
                                                                Font-Bold="true"></asp:TextBox>
                                                            <asp:ImageButton ID="imgPopuptodate" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtenderToDate" PopupButtonID="imgPopuptodate"
                                                                runat="server" TargetControlID="txtToDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorToDate" runat="server" ControlToValidate="txtToDate"
                                                                SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="Search" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </td>

                                         
                                            <td class="field_input">&nbsp;</td>
                                        </tr>
                                        <tr>                                            
                                            <td class="field_title" colspan="5">
                                                <div class="gridcontent-inner"> 
                                                    <cc1:Grid ID="gvRSMap" runat="server" CallbackMode="false" Serialize="true" AutoGenerateColumns="false" AllowPaging="true" EnableRecordHover="true"
                                                    AllowAddingRecords="false" AllowFiltering="false" AllowSorting="false" AllowPageSizeSelection="false" FolderStyle="../GridStyles/premiere_blue" PageSize="100">
                                                <Columns>
                                                         <cc1:Column ID="Column16"  DataField="ID" ReadOnly="true" HeaderText="ID" runat="server" Width="80" Wrap="true">                                      
                                                            <TemplateSettings TemplateID="CheckTemplate" HeaderTemplateId="HeaderTemplate" />
                                                        </cc1:Column>

                                                    <cc1:Column ID="Column20" DataField="CATAGORYNAME" HeaderText="CATEGORY NAME" runat="server" Width="150%" Wrap="true">
                                                        <FilterOptions>
                                                            <cc1:FilterOption Type="NoFilter" />
                                                            <cc1:FilterOption Type="Contains" />
                                                            <cc1:FilterOption Type="StartsWith" />
                                                        </FilterOptions>
                                                    </cc1:Column>

                                                    <cc1:Column ID="Column8" DataField="NAME" HeaderText=" PRODUCT NAME" runat="server" Width="300%" Wrap="true">
                                                        <FilterOptions>
                                                            <cc1:FilterOption Type="NoFilter" />
                                                            <cc1:FilterOption Type="Contains" />
                                                            <cc1:FilterOption Type="StartsWith" />
                                                        </FilterOptions>
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column9" DataField="UOMID" ReadOnly="true" HeaderText="UOMID" runat="server" Visible="false"></cc1:Column>
                                                        
                                                    <cc1:Column ID="Column10" DataField="UNITNAME" HeaderText=" UNIT NAME" runat="server" Width="100%" Wrap="true">
                                                        <FilterOptions>
                                                            <cc1:FilterOption Type="NoFilter" />
                                                            <cc1:FilterOption Type="Contains" />
                                                            <cc1:FilterOption Type="StartsWith" />
                                                        </FilterOptions>
                                                    </cc1:Column>
                                                    <cc1:Column DataField="RMCOST" HeaderText="RM-PM Cost(RS)" AllowEdit="true" Width="100%">
                                                        <TemplateSettings TemplateId="rmcosetemplate" />
                                                    </cc1:Column>
                                                    <cc1:Column DataField="" HeaderText="Conversion Cost(RS)" AllowEdit="true" Width="100%">
                                                        <TemplateSettings TemplateId="trcosetemplate" />
                                                    </cc1:Column>
                                                    <cc1:Column DataField="" HeaderText="PCS/KG" AllowEdit="true" Width="100%">
                                                        <TemplateSettings TemplateId="PCS" />
                                                    </cc1:Column>
                                                     <cc1:Column ID="Column12" DataField="FROMDATE" HeaderText=" FROM DATE" runat="server" Width="100%"  Wrap="true" Visible="false">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column13" DataField="TODATE" HeaderText=" TO DATE" runat="server" Width="100%"  Wrap="true" Visible="false">
                                                    </cc1:Column>
                                                </Columns> 
                                                
                                                 <Templates>
                                                            <cc1:GridTemplate runat="server" ID="CheckTemplate">
                                                            <Template>
                                                             <asp:HiddenField runat="server" ID="hdnPRODUCTID" Value='<%# Container.DataItem["ID"] %>' />
                                                                <asp:HiddenField runat="server" ID="HdnCatID" Value='<%# Container.DataItem["CATID"] %>' />                                                                
                                                            <asp:CheckBox ID="ChkID" runat="server" Text=" " ToolTip="<%# Container.Value %>" style="padding-left: 5px;" />
                                                            </Template>
                                                            </cc1:GridTemplate>
                                                 </Templates>                           
                                                <Templates>
                                                    <cc1:GridTemplate ID="rmcosetemplate">
                                                        <Template>
                                                            <asp:HiddenField runat="server" ID="hdnPS" Value='<%# Container.DataItem["NAME"] %>' />
                                                            <asp:HiddenField runat="server" ID="HdnCatName" Value='<%# Container.DataItem["CATAGORYNAME"] %>' />
                                                            <asp:TextBox ID="txtRmcost" runat="server" Width="90" ToolTip='<%# Container.DataItem["ID"] %>' onkeypress="return isNumberKey(this,event);" Text='<%# Container.DataItem["RMCOST"] %>' />
                                                        </Template>
                                                    </cc1:GridTemplate>
                                                </Templates>
                                                <Templates>
                                                    <cc1:GridTemplate ID="trcosetemplate">
                                                        <Template>
                                                            <asp:TextBox ID="txttrnscost" runat="server" Width="90"  onkeypress="return isNumberKey(event);" Text='<%# Container.DataItem["TRANSFERCOST"] %>'></asp:TextBox>
                                                         </Template>
                                                    </cc1:GridTemplate>
                                                </Templates>
                                                <Templates>
                                                    <cc1:GridTemplate ID="PCS">
                                                        <Template>
                                                            <asp:TextBox ID="txtpcs" runat="server" Width="90"  onkeypress="return isNumberKey(event);" Text='<%# Container.DataItem["PCS"] %>'></asp:TextBox>
                                                         </Template>
                                                    </cc1:GridTemplate>
                                                </Templates>
                                                <ScrollingSettings ScrollWidth="80%" ScrollHeight="230"/>
                                            </cc1:Grid>
                                                </div>
                                            </td>
                                          
                                        </tr> 
                                        



                                        <tr>
                                            <td style="padding:8px 0;" colspan="5">
                                            <div class="btn_24_blue">
                                              <span class="icon disk_co"></span>
                                                <asp:Button ID="btnPBMapSubmit" runat="server" Text="Save" CssClass="btn_link" OnClick="btnPBMapSubmit_Click"  ValidationGroup="A" />
                                            </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                            <div class="btn_24_blue">
                                                <span class="icon cross_octagon_co"></span>
                                                <asp:Button ID="btnPBMapCancel" runat="server" Text="Cancel" CssClass="btn_link" OnClick="btnPBMapCancel_Click" CausesValidation="false" />
                                            </div>
                                                    <asp:HiddenField ID="Hdn_Fld" runat="server" />
                                            </td>
                                        </tr>                                     
                                    </table>
                            </asp:Panel>
                            <asp:Panel ID="pnlDisplay" runat="server">
                                      <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                      <tr >
                                            <td width="130" class="field_title">
                                                <asp:Label ID="Label4" Text="Sales Location Type" runat="server"></asp:Label><span class="req">*</span>
                                            </td>
                                            <td class="field_input">
                                                  <asp:RadioButton ID="rdbsearchTPU" runat="server" Text="TPU/VENDOR" GroupName="BR"
                                                                 AutoPostBack="true" CssClass="innerfield_title"
                                                      oncheckedchanged="rdbsearchTPU_CheckedChanged" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:RadioButton ID="rdbsearchFactory" runat="server" Text="FACTORY" CssClass="innerfield_title"
                                                                GroupName="BR" oncheckedchanged="rdbsearchFactory_CheckedChanged"  AutoPostBack="true" />
                                                               
                                            </td>
                                        </tr>
                                        <tr id="hdnVendor1" runat="server">
                                            <td class="field_title">Vendor Name</td>
                                            <td class="field_input">
                                                <asp:DropDownList ID="ddlSearchVendor" runat="server" 
                                                  AppendDataBoundItems="true" class="chosen-select"
                                                    Style="width: 250px;" data-placeholder="Choose a Vendor" 
                                                  AutoPostBack="True" 
                                                  onselectedindexchanged="ddlSearchVendor_SelectedIndexChanged" >
                                                </asp:DropDownList>

                                                <div class="btn_24_blue">
                                                                <span class="icon doc_excel_table_co"></span><a href="#" onclick="exportToExcel();">
                                                                    <asp:Button ID="btnExport" runat="server" Text="Export Excel" CssClass="btn_link" /></a>
                                                            </div>
                                            </td>
                                           
                                        </tr> 

                                        <tr id="hdnFactory1" runat="server">
                                            <td class="field_title">
                                                <asp:Label ID="Label5" Text="Factory Name" runat="server"></asp:Label><span class="req">*</span>
                                            </td>
                                            <td class="field_input">
                                                <asp:DropDownList ID="ddlsearchFactory" runat="server" 
                                                    AppendDataBoundItems="true" class="chosen-select"
                                                    Style="width: 250px;" data-placeholder="Choose a Factory" onselectedindexchanged="ddlsearchFactory_SelectedIndexChanged" AutoPostBack="true"
                                                    >
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="A" ForeColor="Red"
                                                    ErrorMessage="Factory is required!" ControlToValidate="ddlFactory" ValidateEmptyText="false"
                                                    SetFocusOnError="true" InitialValue="0" > </asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="padding:8px 0;">
                                                <div class="gridcontent">                                   
                                    <cc1:Grid ID="gvRatesheet" runat="server" CallbackMode="true" Serialize="true" FolderStyle="../GridStyles/premiere_blue"
                                        AutoGenerateColumns="False" AllowPageSizeSelection="true" OnDeleteCommand="DeleteRecord" EnableRecordHover="true"
                                        AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true"
                                        OnExported="gvRatesheet_Exported" OnExporting="gvRatesheet_Exporting">
                                         <ExportingSettings ExportColumnsFooter="true" ExportGroupFooter="true" ExportGroupHeader="true"
                                                FileName="Rate Sheet" ExportAllPages="true" ExportDetails="true" AppendTimeStamp="false" />
                                            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size: 10px;background-color: #DCE3E8;color: #0000FF;"
                                                CSSExportCellStyle="font-weight: normal;font-size: 10px;color: #0000;background-color: AFEEEE" />


                                        <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnClientDelete="OnDelete" />
                                        <FilteringSettings InitialState="Visible" />
                                        <Columns>
                                            <cc1:Column ID="Column1" DataField="ID" ReadOnly="true" HeaderText="ID" runat="server" Visible="false" />
                                            <cc1:Column ID="Column17" DataField="DIVID" ReadOnly="true" HeaderText="DIVID" runat="server" Visible="false" />
                                            <cc1:Column ID="Column7" DataField="DIVNAME" HeaderText="PRIMARY ITEM" runat="server" Width="100" Wrap="true">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                </FilterOptions>
                                            </cc1:Column>

                                            <cc1:Column ID="Column2" DataField="PRODUCTNAME" HeaderText="PRODUCT NAME" runat="server" Width="300" Wrap="true">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                </FilterOptions>
                                            </cc1:Column>

                                            <cc1:Column ID="Column21" DataField="CODE" HeaderText="PRODUCT CODE" runat="server" Width="300" Wrap="true">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                </FilterOptions>
                                            </cc1:Column>

                                             <cc1:Column ID="Column18" DataField="VENDORID" ReadOnly="true" HeaderText="VENDORID" runat="server" Visible="false" />
                                            <cc1:Column ID="Column19" DataField="VENDORNAME" HeaderText="VENDOR NAME" runat="server" Width="200" Wrap="true">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                </FilterOptions>
                                            </cc1:Column>


                                            <cc1:Column ID="Column3" DataField="RMCOST" HeaderText=" RM-PM Cost(RS)" runat="server" Width="100" Wrap="true">  
                                                  <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                </FilterOptions>                                             
                                            </cc1:Column>
                                            <cc1:Column ID="Column4"  DataField="TRANSFERCOST" HeaderText=" Conversion Cost(RS)" runat="server" Width="100" Wrap="true">                                                
                                            </cc1:Column>
                                            <cc1:Column ID="Column11"  DataField="PCS" HeaderText="PCS/KG" runat="server" Width="100" Wrap="true">                                                
                                            </cc1:Column>
                                             <cc1:Column ID="Column14" DataField="FROMDATE" HeaderText=" FROM DATE" runat="server" Width="100"  Wrap="true">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column15" DataField="TODATE" HeaderText=" TO DATE" runat="server" Width="100"  Wrap="true">
                                                    </cc1:Column>
                                            <cc1:Column ID="Column5"  DataField="PRODUCTID" HeaderText="PID" runat="server" Visible="false" />
                                            <cc1:Column ID="Column6"  HeaderText="Edit" AllowEdit="true" AllowDelete="true" runat="server" Width="70%" Visible="false">
                                                <TemplateSettings TemplateId="editBtnTemplate" />
                                            </cc1:Column>
                                            <cc1:Column HeaderText="Delete" AllowEdit="false" AllowDelete="true" Width="70">
                                                <TemplateSettings TemplateId="deleteBtnTemplate" />
                                            </cc1:Column>
                                        </Columns>
                                        <FilteringSettings MatchingType="AnyFilter" />
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
                                                    <a href="javascript: //" class="action-icons c-delete" title="Delete" onclick="gvRatesheet.delete_record(this)">
                                                    </a>
                                                </Template>
                                            </cc1:GridTemplate>
                                        </Templates>
                                        <ScrollingSettings ScrollWidth="100%" ScrollHeight="290" />
                                    </cc1:Grid>
                                    <asp:Button ID="btngridsave" runat="server" Text="Edit" Style="display: none" OnClick="btngridsave_Click" CausesValidation="false" />            
                                </asp:Panel>  
                                <asp:HiddenField ID="hdn_pid" runat="server" />
                                 </div>
                                            </td>
                                        </tr>                                                                          
                                    </table> 
                                    
                        </div>
                    </div>
                </div>
                <cc1:MessageBox ID="MessageBox1" runat="server" />  
                <span class="clear"></span>
            </div>
            <span class="clear"></span></div>
            
            <script type="text/javascript">
                function CallServerMethod(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnGridEdit_", "");
                    document.getElementById("<%=hdn_pid.ClientID %>").value = gvRatesheet.Rows[iRecordIndex].Cells[4].Value;
                    document.getElementById("<%=btngridsave.ClientID %>").click();
                }
                </script>
                
                 <script type="text/javascript">
                     
                     function isNumberKey(e1, evt) {
                         var charCode = (event.which) ? event.which : event.keyCode;

                         if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57))
                             return false;

                         //if dot sign entered more than once then don't allow to enter dot sign again. 46 is the code for dot sign
                         if (charCode == 46 && (elementRef.value.indexOf('.') >= 0))
                             return false;
                         else if (charCode == 46 && (elementRef.value.indexOf('.') <= 0)) //allow to enter dot sign if not entered.
                             return true;
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