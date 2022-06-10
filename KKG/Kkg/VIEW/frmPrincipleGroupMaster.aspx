<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmPrincipleGroupMaster.aspx.cs" Inherits="VIEW_frmPrincipleGroupMaster" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register assembly="ProudMonkey.Common.Controls" namespace="ProudMonkey.Common.Controls" tagprefix="cc1" %> 

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
            <%--<div class="page_title">
                <span class="title_icon"><span class="computer_imac"></span></span>
                <h3>Principal Group Master</h3>
            </div>--%>
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>Principal Group  Details</h6>
                              <div class="btn_30_light" style="float: right;" id="btnaddhide" runat="server">
                                    <span class="icon add_co"></span>
                                    <asp:Button ID="btnAddCatagory" runat="server" Text="Add New Record" CssClass="btn_link"
                                        OnClick="btnAddCatagory_Click" CausesValidation="false" /></div>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlAdd" runat="server">
                                <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                  <tr>
                                            <td class="field_title"> <asp:Label ID="lblbusinesssegment" Text="Business Segment" runat="server" ></asp:Label><span class="req">*</span></td>
                                            <td class="field_input">
                                                <asp:DropDownList ID="ddlbusinesssegment" runat="server" Width="230px" class="chosen-select"  data-placeholder="Choose Business" AppendDataBoundItems="true" >
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfv_ddlbusinesssegment" runat="server" ErrorMessage="Required" ControlToValidate="ddlbusinesssegment" ValidateEmptyText="false"
                                                    SetFocusOnError="true" InitialValue="0"  ForeColor="Red">
                                                </asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td class="field_title"> <asp:Label ID="Label4" Text="Currency" runat="server" ></asp:Label><span class="req">*</span></td>
                                            <td class="field_input">
                                                <asp:DropDownList ID="ddlcurency" runat="server" Width="230px" class="chosen-select"  data-placeholder="Choose Currency" AppendDataBoundItems="true" >
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfv_ddlcurency" runat="server" ErrorMessage="Required" ControlToValidate="ddlcurency" ValidateEmptyText="false"
                                                    SetFocusOnError="true" InitialValue="0"  ForeColor="Red">
                                                </asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="130" class="field_title"><asp:Label ID="lblcatcode" Text="Code" runat="server"></asp:Label><span class="req">*</span></td>
                                            <td class="field_input">
                                                <asp:TextBox ID="txtcatcode" runat="server" CssClass="mid" MaxLength="20"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="CV_catcode" runat="server" Display="None" ErrorMessage="  Code is required!"
                                                            ControlToValidate="txtcatcode" ValidateEmptyText="false" OnServerValidate="CV_catcode_ServerValidate"
                                                            SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                                            TargetControlID="CV_catcode" PopupPosition="Right" HighlightCssClass="errormassage"
                                                            WarningIconImageUrl="../images/050.png">
                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title"><asp:Label ID="lblcatname" Text="Name" runat="server"></asp:Label><span class="req">*</span></td>
                                            <td class="field_input">
                                                <asp:TextBox ID="txtcatname" runat="server" CssClass="mid" MaxLength="50"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="CV_catname" runat="server" Display="None" ErrorMessage="  Name is required!"
                                                    ControlToValidate="txtcatname" ValidateEmptyText="false" OnServerValidate="CV_catname_ServerValidate" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                    TargetControlID="CV_catname" PopupPosition="Right" HighlightCssClass="errormassage"
                                                    WarningIconImageUrl="../images/050.png">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title"><asp:Label ID="lblcatdescription" Text="Description" runat="server"></asp:Label><span class="req">*</span></td>
                                            <td class="field_input">
                                                <asp:TextBox ID="txtDescription" runat="server" CssClass="mid"  TextMode="MultiLine" Width="30%" MaxLength="50" >   </asp:TextBox>
                                                <asp:RequiredFieldValidator ID="CV_Description" runat="server" Display="None" ErrorMessage="Description is required!"
                                                    ControlToValidate="txtDescription" ValidateEmptyText="false" OnServerValidate="CV_Description_ServerValidate" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                                    TargetControlID="CV_Description" PopupPosition="Right" HighlightCssClass="errormassage"
                                                    WarningIconImageUrl="../images/050.png">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>

                                        </tr>
                                        
                                      
                                        <tr>
                                            <td class="field_title"><asp:Label ID="Label1" Text="Active" runat="server"></asp:Label></td>
                                            <td class="field_input"><asp:CheckBox ID="chkActive" runat="server" Text=" " /></td>
                                         </tr>
                                          <tr>
                                          
                                               <td class="field_title">
                                                <asp:Label ID="Label30" Text="IS TRANSFER TO HO" runat="server"   ></asp:Label>
                                                </td>
                                                <td class="field_input">
                                                <asp:CheckBox ID="chktransfertoho" runat="server" Text=" " 
                                                    oncheckedchanged="chktransfertoho_CheckedChanged"  AutoPostBack="true"/>
                                                </td>
                                                

                                            </tr>
                                             <tr id="trledger" runat="server">
                                             <td class="field_title"><asp:Label ID="Label8" Text="Posting Ledger To HO" runat="server"></asp:Label><span class="req">*</span></td>
                                             <td class="field_input">
                                             <asp:DropDownList ID="ddlledger" Width="200" runat="server" data-placeholder="Select Ledger"
                                                                AppendDataBoundItems="True" class="chosen-select" />

                                             
                                             </td>
                                        </tr>
                                              <tr>
                                                 <td class="field_title">
                                                <asp:Label ID="Label5" Text="IS CHAIN" runat="server"   ></asp:Label>
                                                 </td>
                                                   <td class="field_title">
                                                <asp:CheckBox ID="chkchain" runat="server" Text=" " />
                                                  </td>
                                              </tr>
                                                 <tr>
                                                   <td class="field_title">
                                                <asp:Label ID="Label6" Text="IS CLAIM CHAIN" runat="server"   ></asp:Label>
                                                </td>
                                                  <td class="field_title">
                                                <asp:CheckBox ID="chkclaimchain" runat="server" Text=" " />
                                                </td>
                                             
                                            
                                       </tr>

                                       
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td style="padding:8px 0px;">
                                            <div class="btn_24_blue">
                                              <span class="icon disk_co"></span>
                                                <asp:Button ID="btnCATsubmit" runat="server" Text="Save" CssClass="btn_link" OnClick="btnCATsubmit_Click" />
                                            </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                            <div class="btn_24_blue">
                                                <span class="icon cross_octagon_co"></span>
                                                <asp:Button ID="btnCATcancel" runat="server" Text="Cancel" CssClass="btn_link" OnClick="btnCATcancel_click" CausesValidation="false" />
                                            </div>
                                                <asp:HiddenField ID="Hdn_Fld" runat="server" />
                                                <asp:HiddenField ID="hdnBSid" runat="server" />
                                                <asp:HiddenField ID="HdnPredined" runat="server" />
                                            </td>
                                        </tr>                                     
                                    </table>                                    
                                </asp:Panel>
                                <asp:Panel ID="plnProductMapping" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td" >
                                        <tr>
                                            <td width="80" class="field_title">
                                                <asp:Label ID="LabelNAME" Text=" GROUP" runat="server"></asp:Label><span class="req">*</span></td>
                                             <td width="220" class="field_input" >
                                                <asp:TextBox ID="txtgroup" runat="server" CssClass="full" Enabled="false"></asp:TextBox></td>
                                            
                                             <td width="160" class="field_title">
                                                <asp:Label ID="lblbsname" Text="BUSINESSSEGMENT NAME" runat="server"></asp:Label><span class="req">*</span></td>
                                             <td width="220" class="field_input" >
                                                <asp:TextBox ID="txtBSname" runat="server" CssClass="full" Enabled="false" ></asp:TextBox>
                                            </td>
                                             <td class="field_title"> &nbsp;</td>
                                            
                                        </tr>
                                        <tr>
                                            <td colspan="5" class="field_input" style="padding-left: 10px;">
                                            
                                               <fieldset>
                                                 <legend>PRODUCT MAPPING</legend>
                                                    <table border="0" cellspacing="0" cellpadding="0" class="form_container_td">

                                                            <tr>
                                                                 <td width="60" class="field_title"  > <asp:Label ID="Label2" Text="Brand" runat="server" ></asp:Label><span class="req">*</span></td>
                                                                <td width="238" class="field_input">
                                                                <asp:DropDownList ID="ddldivision" runat="server" Width="230px" class="chosen-select" ValidationGroup="ADD"  data-placeholder="Choose Business" AppendDataBoundItems="true" >
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfv_ddlbrand" runat="server" ErrorMessage="Required" ControlToValidate="ddldivision" ValidateEmptyText="false"
                                                                SetFocusOnError="true" InitialValue="0"  ForeColor="Red" ValidationGroup="ADD"></asp:RequiredFieldValidator>
                                                               </td>
                                                               <td width="82" class="field_title" > <asp:Label ID="Label3" Text="Category" runat="server" ></asp:Label><span class="req">*</span></td>
                                                                <td width="238" class="field_input">
                                                                <asp:DropDownList ID="ddlCategory" runat="server" Width="230px" 
                                                                        class="chosen-select"  data-placeholder="Choose Business" 
                                                                        AppendDataBoundItems="true" AutoPostBack="True" 
                                                                        onselectedindexchanged="ddlCategory_SelectedIndexChanged" ValidationGroup="ADD" >
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfv_ddlCategory" runat="server" ErrorMessage="Required" ControlToValidate="ddlCategory" ValidateEmptyText="false"
                                                                SetFocusOnError="true" InitialValue="0"  ForeColor="Red" ValidationGroup="ADD" ></asp:RequiredFieldValidator>
                                                               </td>
                                                                <td class="field_title"> &nbsp;</td>
                                                            </tr>

                                                            <tr>
                                                                <td class="field_input" colspan="5">
                                                                    <div class="gridcontent-short">
                                                                    <ul>
                                                                        <li class="left">
                                                                        <cc1:Grid ID="gvproductmapping" runat="server" FolderStyle="../GridStyles/premiere_blue"  Serialize="true" 
                                                                    AllowFiltering="false" AutoGenerateColumns="false" AllowAddingRecords="false"  AllowPageSizeSelection="true" 
                                                                    PageSize="100" AllowSorting="false" AllowPaging="false"  >
                                                                     <FilteringSettings InitialState="Visible" />       
                                                                            
                                                                            <Columns>
                                                                                     <cc1:Column ID="Column1" DataField="NAME" HeaderText="PRODUCT NAME " runat="server"
                                                                                        Width="466" Wrap="true">
                                                                                     </cc1:Column>
                                                                                      
                                                                                     <cc1:Column ID="Column2" DataField="ID"  HeaderText="ID" Width="50" runat="server" Visible="false" ></cc1:Column>                                      
                                                                                     <cc1:CheckBoxSelectColumn ID="CheckBoxSelectColumn1" HeaderText=" " DataField="" ShowHeaderCheckBox="true" 
                                                                                                        ControlType="Obout" runat="server">
                                                                                     </cc1:CheckBoxSelectColumn >
                                                                                     
                                                                            </Columns> 
                                                                             <FilteringSettings MatchingType="AnyFilter" />
                                                                             
                                                                             <ScrollingSettings ScrollWidth="100%" ScrollHeight="180" /> 
                                                                    </cc1:Grid>
                                                                        </li>
                                                                        <li class="middle">                                                                            
                                                                             <asp:Button ID="btnproductadd" runat="server"  Text=""  ValidationGroup="ADD" CssClass="addbtn_blue" onclick="btnproductadd_Click" />                                                                             
                                                                        </li>
                                                                        <li class="right">
                                                                             <cc1:Grid ID="gvproductadd" runat="server"  Serialize="true" AllowAddingRecords="false"
                                                                              AutoGenerateColumns="false" AllowSorting="true" AllowPageSizeSelection="false" 
                                                                              FolderStyle="../GridStyles/premiere_blue" AllowPaging="false" PageSize="100">
                                                                     <Columns>
                                             
                                            
                                                                            <cc1:Column ID="Column3"   DataField="SlNo" HeaderText="SL" runat="server" Width="60">
                                                                               <TemplateSettings TemplateId="slnoTemplateFinal" />
                                                                            </cc1:Column> 
                                                                            <cc1:Column ID="Column4"    DataField="GUID" HeaderText="GUID" runat="server"  Visible="false"/>
                                                                            <cc1:Column ID="Column5"    DataField="BSID" HeaderText="BSID" runat="server" Visible="false" ></cc1:Column>
                                                                            <cc1:Column ID="Column6"    DataField="BSNAME" HeaderText="BSNAME" runat="server" Visible="false" ></cc1:Column>
                                                                            <cc1:Column ID="Column7"    DataField="GROUPID" HeaderText="GROUPID" runat="server" Visible="false" ></cc1:Column>
                                                                            <cc1:Column ID="Column8"    DataField="GROUPNAME" HeaderText="GROUPNAME" runat="server" Visible="false"></cc1:Column>
                                                                            <cc1:Column ID="Column9"    DataField="BRANDID" HeaderText="BRANDID" runat="server" Visible="false" ></cc1:Column>
                                                                            <cc1:Column ID="Column10"    DataField="BRANDNAME" HeaderText="BRANDANME" runat="server" Visible="false" ></cc1:Column>
                                                                            <cc1:Column ID="Column11"    DataField="CATEGORYID" HeaderText="CATEGORYID" runat="server" Visible="false" ></cc1:Column>
                                                                            <cc1:Column ID="Column12"    DataField="CATEGORYNAME" HeaderText="CATEGORYNAME" runat="server" Visible="false"></cc1:Column>
                                                                            <cc1:Column ID="Column13"    DataField="PRODUCTID" HeaderText="PRODUCTID ID"  runat="server" Visible="false" ></cc1:Column>
                                                                            <cc1:Column ID="Column14"    DataField="PRODUCTNAME" HeaderText="PRODUCT NAME" Width="300" runat="server"></cc1:Column>
                                                           
                                             
                                                                            <cc1:Column HeaderText="Delete" AllowEdit="false" AllowDelete="true" Width="80">
                                                                                <TemplateSettings TemplateId="deleteBtnTemplategroup" />
                                                                            </cc1:Column>
                                             
                                                                     </Columns>
                                                                     <Templates>
                                                                            <cc1:GridTemplate runat="server" ID="deleteBtnTemplategroup">
                                                                                <Template>
                                                                                    <a href="javascript: //" class="action-icons c-delete" id="btnGridDelete_<%# Container.PageRecordIndex %>"
                                                                                                             onclick="CallProductServerDeleteMethod(this)"></a>
                                                                                </Template>
                                                                            </cc1:GridTemplate>
                                                                                <cc1:GridTemplate runat="server" ID="slnoTemplateFinal">
                                                                            <Template>
                                                                                <asp:Label ID="lblslnoTaxFinal" runat="server" Text='<%#Container.PageRecordIndex+1 %>'></asp:Label>
                                                                            </Template>
                                                                            </cc1:GridTemplate>
                                                                     </Templates>
                                                                     <ScrollingSettings ScrollWidth="100%" ScrollHeight="180" />
                                                                     </cc1:Grid>
                                                    
                                                                     <asp:Button ID="btnproductdelete" runat="server" CausesValidation="false" Text="grddelete" Style="display: none" OnClick="btnproductdelete_Click"
                                                                                 OnClientClick="return confirm('Are you sure you want to delete this record?');"  />
                                                                     <asp:HiddenField ID="hdnProductDelete" runat="server" />
                                                                        </li>
                                                                    </ul>
                                                                    </div>
                                                                 </td>                                                                
                                                            </tr>
                                                  </table>
                                               </fieldset>
                                              
                                           </td> 
                                        </tr>
                                        <tr>
                                            
                                            <td style="padding-left:10px;" colspan="5"  class="field_input">
                                            <div class="btn_24_blue">
                                              <span class="icon disk_co"></span>
                                                <asp:Button ID="btnproductmappingsave" runat="server" Text="Save" CssClass="btn_link"  onclick="btnproductmappingsave_Click" CausesValidation="false"  />
                                            </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                            <div class="btn_24_blue" >
                                                <span class="icon cross_octagon_co"></span>
                                                <asp:Button ID="btnproductmappingcancel" runat="server" Text="Cancel" 
                                                    CssClass="btn_link"  CausesValidation="false" 
                                                    onclick="btnproductmappingcancel_Click" />
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
                                            <%-- <obout:OboutTextBox runat="server" ID="FilterTextBox" WatermarkText="Enter text to search the whole grid">
                                        <ClientSideEvents OnKeyUp="FilterTextBox_KeyUp" />--%>
                                            <%--   </obout:OboutTextBox>--%>
                                        </li>
                                        <li>
                                            <input name="" id="OboutButton2" type="submit" value="" class="search_btn" onclick="return performSearch();">
                                            <%-- <obout:OboutButton ID="OboutButton2" runat="server" Text="Search"  OnClientClick="return performSearch();" />--%>
                                        </li>
                                    </ul>
                                    <div class="gridcontent">
                                    <cc1:Grid ID="gvCATAGORY" runat="server" CallbackMode="true" Serialize="true" FolderStyle="../GridStyles/premiere_blue"
                                        AutoGenerateColumns="false" AllowPageSizeSelection="true" OnDeleteCommand="DeleteRecord"
                                        AllowAddingRecords="false" AllowFiltering="true" 
                                            onrowdatabound="gvCATAGORY_RowDataBound">
                                        <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnClientDelete="OnDelete" />
                                        <FilteringSettings InitialState="Visible" />
                                        <Columns>
                                            <cc1:Column DataField="DIS_CATID" ReadOnly="true" HeaderText="CATID" runat="server" Visible="false" />
                                            <cc1:Column   DataField="BUSINESSSEGMENTID" HeaderText="BUSINESSSEGMENT NAME" runat="server" Visible="false">
                                                
                                            </cc1:Column>
                                            <cc1:Column  DataField="BUSINESSSEGMENTNAME" HeaderText="BUSINESS SEGMENT " runat="server" Width="230">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="EqualTo" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="DoesNotContain" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                     </FilterOptions>
                                            </cc1:Column>
                                            <cc1:Column DataField="DIS_CATCODE" HeaderText=" CODE" runat="server" Width="220" Visible="false">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="EqualTo" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="DoesNotContain" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                </FilterOptions>
                                            </cc1:Column>
                                            <cc1:Column  DataField="DIS_CATNAME" HeaderText="NAME" runat="server"
                                                Width="240">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="EqualTo" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="DoesNotContain" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                </FilterOptions>
                                            </cc1:Column>
                                            <cc1:Column DataField="DIS_CATDESCRIPTION" HeaderText="DESCRIPTION" runat="server" Width="238">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="EqualTo" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="DoesNotContain" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                     </FilterOptions>
                                            </cc1:Column>
                                             <cc1:Column ID="Column15" DataField="CURRENCYNAME" HeaderText="CURRENCY" runat="server" Width="150">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="EqualTo" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="DoesNotContain" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                     </FilterOptions>
                                            </cc1:Column>
                                            <cc1:Column  DataField="ACTIVE" HeaderText="ACTIVE" SortOrder="Asc" runat="server"
                                                Width="190">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="Contains" />
                                                </FilterOptions>
                                            </cc1:Column>
                                             <cc1:Column ID="Column16" DataField="ISFINANCE_HO" HeaderText="TRANSFER TO HO" runat="server" Width="140">
                                            </cc1:Column>
                                             <cc1:Column ID="Column17" DataField="ISCHAIN" HeaderText="CHAIN" runat="server" Width="140">
                                            </cc1:Column>
                                            <cc1:Column ID="Column18" DataField="PREDEFINED" HeaderText="PREDEFINED" runat="server" Width="100" Visible="false">
                                            </cc1:Column>

                                            <cc1:Column AllowEdit="true" AllowDelete="true" HeaderText="Edit" runat="server"
                                                Width="80">
                                                <TemplateSettings TemplateId="editBtnTemplate" />
                                            </cc1:Column>
                                            <cc1:Column HeaderText="Delete" AllowEdit="false" AllowDelete="true" Width="80" Visible="false">
                                                <TemplateSettings TemplateId="deleteBtnTemplate" />
                                            </cc1:Column>
                                             <cc1:Column HeaderText="Product Mapping" AllowEdit="false" AllowDelete="true" Width="80" Wrap="true" Visible="false">
                                                <TemplateSettings TemplateId="ProductMappingBtnTemplate" />
                                            </cc1:Column>
                                        </Columns>
                                        <FilteringSettings MatchingType="AnyFilter" />
                                         <Templates>
                                                    <cc1:GridTemplate runat="server" ID="editBtnTemplate" >
                                                    <Template>
                                                    <a href="javascript: //" class="action-icons c-edit"  title="Edit" id="btnGridEdit_<%# Container.PageRecordIndex %>"
                                                         onclick="return CallServerMethod(this);" ></a>                                             
                                                    </Template>
                                                    </cc1:GridTemplate>
                                        </Templates>
                                         <Templates>
                                                    <cc1:GridTemplate runat="server" ID="deleteBtnTemplate" >
                                                    <Template>
                                                    <a href="javascript: //" class="action-icons c-delete" title="Delete" onclick="gvCATAGORY.delete_record(this)"  >
                                                    </a>                                             
                                                    </Template>
                                                    </cc1:GridTemplate>
                                        </Templates>
                                        <Templates>
                                                    <cc1:GridTemplate runat="server" ID="ProductMappingBtnTemplate" >
                                                    <Template>
                                                    <a href="javascript: //" class="h_icon map"  title="Product Mapping" 
                                                         onclick="openProductMapping('<%# Container.DataItem["DIS_CATID"] %>','<%# Container.DataItem["DIS_CATNAME"] %>','<%# Container.DataItem["BUSINESSSEGMENTID"] %>','<%# Container.DataItem["BUSINESSSEGMENTNAME"] %>')" ></a>                                             
                                                    </Template>
                                                    </cc1:GridTemplate>
                                        </Templates>
                                        <ScrollingSettings ScrollWidth="100%" ScrollHeight="290"  NumberOfFixedColumns="6"/>
                                    </cc1:Grid>
                                    <asp:Button ID="btngridedit" runat="server" Text="GridSave" 
                                        Style="display: none"  onclick="btngridedit_Click" CausesValidation="false"/>
                                        <asp:Button ID="btnProductMapping" runat="server" Text="PGProduct Mapping" 
                                        Style="display: none"  onclick="btnProductMapping_Click" CausesValidation="false"/>
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

                    function OnBeforeDelete(record) {
                        record.Error = '';
                        document.getElementById("<%=Hdn_Fld.ClientID %>").value = record.DIS_CATID;
                        document.getElementById("<%=HdnPredined.ClientID %>").value = record.PREDEFINED;
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
                        //         var searchValue = FilterTextBox.value();
                        if (searchValue == FilterTextBox.WatermarkText) {
                            searchValue = '';
                        }

                        gvCATAGORY.addFilterCriteria('DIS_CATCODE', OboutGridFilterCriteria.Contains, searchValue);
                        gvCATAGORY.addFilterCriteria('DIS_CATNAME', OboutGridFilterCriteria.Contains, searchValue);
                        gvCATAGORY.addFilterCriteria('DIS_CATDESCRIPTION', OboutGridFilterCriteria.Contains, searchValue);
                        gvCATAGORY.executeFilter();
                        searchTimeout = null;

                        return false;
                        }
                        </script>
                  
                  <script type="text/javascript">
                      function openProductMapping(groupid, group, BSid, BSname) {
                          document.getElementById("<%=Hdn_Fld.ClientID %>").value = groupid;
                          document.getElementById("<%=txtgroup.ClientID %>").value = group;
                            document.getElementById("<%=hdnBSid.ClientID %>").value = BSid;
                            document.getElementById("<%=txtBSname.ClientID %>").value = BSname;
                           
                            document.getElementById("<%=btnProductMapping.ClientID %>").click();
                        }

                </script>
                 <script type="text/javascript">
                     function CallProductServerDeleteMethod(oLink) {
                         var iRecordIndex = oLink.id.toString().replace("btnGridDelete_", "");
                         document.getElementById("<%=hdnProductDelete.ClientID %>").value = gvproductadd.Rows[iRecordIndex].Cells[1].Value;
                         document.getElementById("<%=btnproductdelete.ClientID %>").click();

                     }
                </script>

                 <script type="text/javascript">

                     function CallProductServerMethod(oLink) {
                         document.getElementById("<%=hdnProductDelete.ClientID %>").value = '';
                         var iRecordIndex = oLink.id.toString().replace("btnGridDelete_", "");
                         document.getElementById("<%=hdnProductDelete.ClientID %>").value = gvproductadd.Rows[iRecordIndex].Cells[1].Value;
                         document.getElementById("<%=hdnProductDelete.ClientID %>").click();
                     }
                </script>

                  <script type="text/javascript">
                      function CallServerMethod(oLink) {
                          var iRecordIndex = oLink.id.toString().replace("btnGridEdit_", "");
                          document.getElementById("<%=Hdn_Fld.ClientID %>").value = gvCATAGORY.Rows[iRecordIndex].Cells[0].Value;
                          document.getElementById("<%=btngridedit.ClientID %>").click();
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