<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmBusinessSegmentMaster.aspx.cs" Inherits="VIEW_frmBusinessSegmentMaster" %>

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
                <h3>
                    Business Segment Master</h3>
            </div>--%>
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>
                                    Business Segment Details
                                </h6>
                                <div class="btn_30_light" style="float: right;" id="btnaddhide" runat="server">
                                    <span class="icon add_co"></span>
                                    <asp:Button ID="btnAddBusinessSegment" runat="server" Text="Add New Record"
                                        CssClass="btn_link" OnClick="btnAddBusinessSegment_Click" CausesValidation="false" /></div>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td width="120" class="field_title"><asp:Label ID="lblBSCode" Text="Code" runat="server"></asp:Label><span class="req">*</span></td>
                                            <td class="field_input">
                                                <asp:TextBox ID="txtBSCode" runat="server" CssClass="mid" MaxLength="20"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="CV_BSCode" runat="server" Display="None" ErrorMessage="Business Segment Code is required!"
                                                            ControlToValidate="txtBSCode" ValidateEmptyText="false" OnServerValidate="CV_BSCode_ServerValidate"  SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                                            TargetControlID="CV_BSCode" PopupPosition="Right" HighlightCssClass="errormassage"
                                                            WarningIconImageUrl="../images/050.png">
                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title"><asp:Label ID="lblBSName" Text="Name" runat="server"></asp:Label><span class="req">*</span></td>
                                            <td class="field_input">
                                                 <asp:TextBox ID="txtBusinessSegmentName" runat="server" CssClass="required large" MaxLength="50"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="CV_BSName" runat="server" Display="None" ErrorMessage="Business Segment Name is required!"
                                                            ControlToValidate="txtBusinessSegmentName" ValidateEmptyText="false" 
                                                            OnServerValidate="CV_BSName_ServerValidate" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                            TargetControlID="CV_BSName" PopupPosition="Right" HighlightCssClass="errormassage"
                                                            WarningIconImageUrl="../images/050.png">
                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title"><asp:Label ID="lblBSDescription" Text="Description" runat="server"></asp:Label><span class="req">*</span></td>
                                            <td class="field_input">
                                                <asp:TextBox ID="txtBSDescription" runat="server" CssClass="required large" MaxLength="50"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="CV_BSDescription" runat="server" Display="None" ErrorMessage="Business Segment Description is required!"
                                                            ControlToValidate="txtBSDescription" ValidateEmptyText="false" 
                                                            OnServerValidate="CV_BSDescription_ServerValidate" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                                            TargetControlID="CV_BSDescription" PopupPosition="Right" HighlightCssClass="errormassage"
                                                            WarningIconImageUrl="../images/050.png">
                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                        </tr> 
                                         <tr>
                                            <td class="field_title"><asp:Label ID="Label1" Text="Active" runat="server"></asp:Label></td>
                                            <td class="field_input"><asp:CheckBox ID="chkActive" runat="server" Text=" " /></td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td style="padding:8px 0;">
                                            <div class="btn_24_blue">
                                                <span class="icon disk_co"></span>
                                                <asp:Button ID="btnBSSubmit" runat="server" Text="Save" CssClass="btn_link" OnClick="btnBSSubmit_Click" CausesValidation="false" />
                                            </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                             <div class="btn_24_blue">
                                                <span class="icon cross_octagon_co"></span>  
                                                <asp:Button ID="btnBSCancel" runat="server" Text="Cancel" CssClass="btn_link" OnClick="btnBSCancel_Click" CausesValidation="false" />
                                              </div>      
                                                        <asp:HiddenField ID="Hdn_Fld" runat="server" />
                                                         <asp:HiddenField ID="HdnPredined" runat="server" />
                                            </td>
                                        </tr>                                    
                                    </table>                                    
                                </asp:Panel>
                                 <asp:Panel ID="pnlBS" runat="server" style="display:none;">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">

                                            <tr>
                                            <td width="130" class="field_title">
                                            <asp:Label ID="lblBS" Text="Business Segment" runat="server"></asp:Label><span class="req">*</span>
                                            </td>
                                            <td class="field_input">
                                            <asp:TextBox ID="txtBS" runat="server" CssClass="required large"  Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                       
                                            <tr>
                                        <td colspan="2" class="field_input" style="padding-left: 10px;">
                                                <fieldset>
                                                    <legend>PRODUCT MAPPING</legend>
                                                    <table border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                                    <tr>
                                            <td class="field_title" width="60"><asp:Label ID="lblDiv" Text="Brand" runat="server"></asp:Label><span class="req">*</span></td>
                                            <td class="field_input" width="238">
                                                <asp:DropDownList ID="ddlDivision" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" class="chosen-select" Style="width: 250px;" data-placeholder="Choose a Brand"  >
                                                               
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None" ErrorMessage="Brand is required!"
                                                                    ControlToValidate="ddlDivision" ValidateEmptyText="false" SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
                                                                    TargetControlID="RequiredFieldValidator1" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="../images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                            <td class="field_title" width="80"><asp:Label ID="lblCat" Text="Category" runat="server"></asp:Label><span class="req">*</span></td>
                                            <td class="field_input">
                                                 <asp:DropDownList ID="ddlCategory" runat="server" AppendDataBoundItems="true" 
                                                                class="chosen-select" Style="width: 250px;" 
                                                                data-placeholder="Choose a Category" 
                                                                onselectedindexchanged="ddlCategory_SelectedIndexChanged" AutoPostBack="true"> 
                                                               
                                                            </asp:DropDownList>
                                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="None" ErrorMessage="Category is required!"
                                                                    ControlToValidate="ddlCategory" ValidateEmptyText="false" SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server"
                                                                    TargetControlID="RequiredFieldValidator2" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="../images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                            </tr>
                                            <tr>
                                            <td class="field_input" colspan="4">
                                            <div class="gridcontent-short">
                                             <ul>
                                             <li class="left">
                                        <cc1:Grid ID="gvPBMap" runat="server" CallbackMode="false" Serialize="true" FolderStyle="../GridStyles/premiere_blue"
                                        AutoGenerateColumns="false" AllowPageSizeSelection="true" AllowPaging="false" AllowSorting="false" PageSize="100"  
                                        AllowAddingRecords="false" AllowFiltering="false" >
                                        <FilteringSettings InitialState="Visible" />
                                    <Columns>                                   
                                    <cc1:Column ID="Column1"  DataField="NAME" HeaderText=" PRODUCT NAME" runat="server" Width="350">
                                        <FilterOptions>
                                            <cc1:FilterOption Type="NoFilter" />
                                            <cc1:FilterOption Type="Contains" />
                                            <cc1:FilterOption Type="StartsWith" />
                                        </FilterOptions>
                                    </cc1:Column>
                                 
                                   <cc1:Column ID="Column2"   DataField="ID" ReadOnly="true" HeaderText="ID" Width="100" runat="server" >                                      
                                    <TemplateSettings TemplateId="CheckProductTemplate" HeaderTemplateId="HeaderTemplate" />
                                    </cc1:Column>
                                    <cc1:CheckBoxSelectColumn ID="CheckBoxSelectColumn1" HeaderText="" DataField="" ShowHeaderCheckBox="true"
                                                                                        ControlType="Obout" runat="server">
                                                                                    </cc1:CheckBoxSelectColumn>
                                 
                                </Columns>
                                <FilteringSettings MatchingType="AnyFilter" />
                                 <Templates>
                            
                                </Templates>
                                <ScrollingSettings ScrollWidth="100%" ScrollHeight="220" />
                            </cc1:Grid>
                                            </li>
                                             <li class="middle">
                                              <asp:Button ID="btnproductadd" runat="server" Text="" CausesValidation="false" CssClass="addbtn_blue"  OnClick="btnproductadd_Click"/>
                                             </li>
                                             <li class="right">
                                                <cc1:Grid ID="gvProductadd" runat="server" Serialize="true" AllowAddingRecords="false"
                                                                                AutoGenerateColumns="false" AllowSorting="true" AllowPageSizeSelection="true"
                                                                                FolderStyle="../GridStyles/grand_gray" AllowPaging="false" PageSize="100">
                                                                                <Columns>
                                                                                    <cc1:Column ID="Column18" DataField="SlNo" HeaderText="SL" runat="server" Width="60">
                                                                                        <TemplateSettings TemplateId="slnoTemplateFinal" />
                                                                                    </cc1:Column>
                                                                                    <cc1:Column ID="Column3"  DataField="GUID" HeaderText="GUID" runat="server" Visible="false" />
                                                                                    <cc1:Column ID="Column4"  DataField="BUSNESSSEGMENTID" HeaderText="BUSNESSSEGMENTID" Visible="false" runat="server"></cc1:Column>
                                                                                    <cc1:Column ID="Column5"  DataField="BUSINESSSEGMENTNAME" HeaderText="BUSINESSSEGMENTNAME" Visible="false" Width="50" runat="server"></cc1:Column>                                                                                    
                                                                                    <cc1:Column ID="Column10"  DataField="PRODUCTID" HeaderText="PRODUCTID" Visible="false"  runat="server"></cc1:Column>
                                                                                    <cc1:Column ID="Column11"  DataField="PRODUCTNAME" HeaderText="PRODUCTNAME" Width="300" runat="server"></cc1:Column>
                                                                                    <cc1:Column HeaderText="Delete" AllowEdit="false" AllowDelete="true" Width="80">
                                                                                        <TemplateSettings TemplateId="deleteBtnTemplategroup" />
                                                                                    </cc1:Column>
                                                                                </Columns>
                                                                                <Templates>
                                                                                    <cc1:GridTemplate runat="server" ID="deleteBtnTemplategroup">
                                                                                        <Template>
                                                                                            <a href="javascript: //" class="action-icons c-delete" id="btnGridDelete_<%# Container.PageRecordIndex %>"
                                                                                                onclick="CallProductSegmentDeleteMethod(this)"></a>
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
                                                           <asp:Button ID="btnproductdelete" runat="server" CausesValidation="false" Text="grddelete"
                                                                    Style="display: none" OnClick="btnproductdelete_Click" OnClientClick="return confirm('Are you sure you want to delete this record?');" />
                                                                <asp:HiddenField ID="hdnproductdelete" runat="server" />
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
                                                <td>&nbsp;</td>
                                                <td style="padding-top:8px;">
                                                <div class="btn_24_blue">
                                                    <span class="icon disk_co"></span>
                                                    <asp:Button ID="btnPBMapSubmit" runat="server" Text="Save" CssClass="btn_link" OnClick="btnPBMapSubmit_Click" CausesValidation="false" />
                                                </div> 
                                                     &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
                                                    <span class="icon cross_octagon_co"></span>
                                                    <asp:Button ID="btnPBMapCancel" runat="server" Text="Cancel" CssClass="btn_link" OnClick="btnPBMapCancel_Click" CausesValidation="false" />
                                                </div>
                                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                                </td>
                                            </tr>
                                       
                                                                     
                                    </table>  
                                    </asp:Panel>
                                <asp:Panel ID="pnlDisplay" runat="server">
                                    <ul id="search_box">
                                        <li><input name="" id="FilterTextBox" type="text" class="search_input" placeholder="Enter text to search the whole grid"
                                                onkeyup="FilterTextBox_KeyUp();">
                                        <li>
                                            <input name="" id="OboutButton2" type="submit" value="" class="search_btn" onclick="return performSearch();">
                                        </li>
                                    </ul>
                                    <div class="gridcontent">
                                    <cc1:Grid ID="gvBusinessSegment" runat="server" CallbackMode="true" Serialize="true"
                                        FolderStyle="../GridStyles/premiere_blue" AutoGenerateColumns="false" AllowPageSizeSelection="false"
                                        OnDeleteCommand="DeleteRecord" AllowAddingRecords="false" 
                                            AllowFiltering="true" onrowdatabound="gvBusinessSegment_RowDataBound">
                                        <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnClientDelete="OnDelete" />
                                        <FilteringSettings InitialState="Visible" />
                                        <Columns>
                                            <cc1:Column DataField="BSID" ReadOnly="true" HeaderText="ID" runat="server" Visible="false" />
                                            <cc1:Column  DataField="BSCODE" HeaderText="CODE" runat="server" Width="100">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="EqualTo" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="DoesNotContain" />
                                                    <cc1:FilterOption Type="StartsWith" />                                                   
                                                </FilterOptions>
                                            </cc1:Column>
                                            <cc1:Column DataField="BSNAME" HeaderText="NAME" runat="server" Width="240"  SortOrder="Asc">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="EqualTo" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="DoesNotContain" />
                                                    <cc1:FilterOption Type="StartsWith" />                                                    
                                                </FilterOptions>
                                            </cc1:Column>
                                            <cc1:Column DataField="BSDESCRIPTION" HeaderText="DESCRIPTION" runat="server" Width="280">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="EqualTo" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="DoesNotContain" />
                                                    <cc1:FilterOption Type="StartsWith" />                                                   
                                                </FilterOptions>
                                            </cc1:Column>
                                            <cc1:Column DataField="ACTIVE" HeaderText="ACTIVE" runat="server"
                                                Width="140">
                                                 <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="Contains" />
                                                </FilterOptions>
                                            </cc1:Column>
                                            <cc1:Column ID="Column6" DataField="PREDEFINED" ReadOnly="true" HeaderText="PREDEFINED" runat="server" Visible="false" />
                                            <cc1:Column DataField="CBU" HeaderText="CBU" runat="server" Visible="false"
                                                Width="140">
                                            </cc1:Column>
                                            <cc1:Column DataField="DTOC" HeaderText="DTOC" runat="server" Visible="false" Width="180">
                                            </cc1:Column>
                                            <cc1:Column AllowEdit="true" AllowDelete="true" HeaderText="Edit" runat="server"
                                                Width="80">
                                                <TemplateSettings TemplateId="editBtnTemplate" />
                                            </cc1:Column>
                                            <cc1:Column HeaderText="Delete" AllowEdit="false" AllowDelete="true" Width="80" Visible="false">
                                                <TemplateSettings TemplateId="deleteBtnTemplate" />
                                            </cc1:Column>
                                             <cc1:Column HeaderText="Product Mapping" AllowEdit="false" AllowDelete="true" Width="120">
                                                <TemplateSettings TemplateId="MappingTemplate" />
                                            </cc1:Column>
                                        </Columns>
                                        <FilteringSettings MatchingType="AnyFilter" />
                                         <Templates>
                                                    <cc1:GridTemplate runat="server" ID="editBtnTemplate" >
                                                    <Template>
                                                   <a href="javascript: //" class="action-icons c-edit" title="Edit" id="btnGridEdit_<%# Container.PageRecordIndex %>"" onclick="attachFlyoutToLink(this,'update')"  >
                                                    Edit</a>                                             
                                                    </Template>
                                                    </cc1:GridTemplate>
                                        </Templates>
                                         <Templates>
                                                    <cc1:GridTemplate runat="server" ID="deleteBtnTemplate" >
                                                    <Template>
                                                    <a href="javascript: //" class="action-icons c-delete" title="Delete" onclick="gvBusinessSegment.delete_record(this)"  >
                                                    </a>                                             
                                                    </Template>
                                                    </cc1:GridTemplate>
                                        </Templates>
                                         <Templates>
                                                    <cc1:GridTemplate runat="server" ID="MappingTemplate" >
                                                    <Template>
                                                   
                                                    <a href="javascript: //" class="h_icon map"  title="Mapping"  onclick="openBSMapping('<%# Container.DataItem["BSNAME"] %>','<%# Container.DataItem["BSID"] %>')"  />                                                                                          
                                                    </Template>
                                                    </cc1:GridTemplate>
                                        </Templates>
                                        <ScrollingSettings ScrollWidth="100%" ScrollHeight="290"/>
                                    </cc1:Grid>                                    
                                    <asp:HiddenField runat="server" ID="hdnPid"  />
                                    <asp:Button ID="btngridsave" runat="server" Text="GridSave" Style="display: none" OnClick="btngridsave_Click" CausesValidation="false" />
                                     <asp:Button ID="btnProductMapping" runat="server" Text="Mapping" Style="display: none"
                                            OnClick="btnProductMapping_Click" CausesValidation="false" />
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                    <span class="clear"></span>
                    <cc1:MessageBox ID="MessageBox1" runat="server" /> 
                </div>
                <span class="clear"></span>
                <script type="text/javascript">
                    function OnBeforeDelete(record) {
                        record.Error = '';
                        document.getElementById("<%=Hdn_Fld.ClientID %>").value = record.BSID;
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

                        if (searchValue == FilterTextBox.WatermarkText) {
                            searchValue = '';
                        }

                        gvBusinessSegment.addFilterCriteria('BSCODE', OboutGridFilterCriteria.Contains, searchValue);
                        gvBusinessSegment.addFilterCriteria('BSNAME', OboutGridFilterCriteria.Contains, searchValue);
                        gvBusinessSegment.addFilterCriteria('BSDESCRIPTION', OboutGridFilterCriteria.Contains, searchValue);
                        gvBusinessSegment.executeFilter();
                        searchTimeout = null;
                        return false;
                    }
                </script>
                <script type="text/javascript">
                    function CallProductSegmentDeleteMethod(oLink) {
                        var iRecordIndex = oLink.id.toString().replace("btnGridDelete_", "");
                        document.getElementById("<%=hdnproductdelete.ClientID %>").value = gvProductadd.Rows[iRecordIndex].Cells[1].Value;
                        document.getElementById("<%=btnproductdelete.ClientID %>").click();

                    }
                    function disableautocompletion(id) {
                        var passwordControl = document.getElementById(id);
                        passwordControl.setAttribute("autocomplete", "off");
                    }
                </script>
                <script type="text/javascript">


                    function attachFlyoutToLink(oLink, Action) {
                        var ele = document.getElementById("<%=pnlDisplay.ClientID%>");
                        if (ele.style.display == "") {
                            ele.style.display = "none";
                            document.getElementById("<%=pnlAdd.ClientID%>").style.display = "";
                            document.getElementById("<%=btnaddhide.ClientID%>").style.display = "none";
                        }
                        clearFlyout();
                        populateEditControls(oLink.id.toString().replace("btnGridEdit_", ""));
                    }

                    function clearFlyout() {
                        document.getElementById("<%=txtBSCode.ClientID %>").value = '';
                        document.getElementById("<%=txtBusinessSegmentName.ClientID %>").value = '';
                        document.getElementById("<%=txtBSDescription.ClientID %>").value = '';
                        document.getElementById("<%=Hdn_Fld.ClientID %>").value = '';
                        document.getElementById("<%=chkActive.ClientID%>").checked = '';
                    }

                    function populateEditControls(iRecordIndex) {

                        document.getElementById("<%=txtBSCode.ClientID%>").value = gvBusinessSegment.Rows[iRecordIndex].Cells[1].Value;
                        document.getElementById("<%=txtBusinessSegmentName.ClientID%>").value = gvBusinessSegment.Rows[iRecordIndex].Cells[2].Value;
                        document.getElementById("<%=txtBSDescription.ClientID%>").value = gvBusinessSegment.Rows[iRecordIndex].Cells[3].Value;
                        var activevalue = gvBusinessSegment.Rows[iRecordIndex].Cells[4].Value;

                        if (activevalue == "Active") {
                            document.getElementById("<%=chkActive.ClientID%>").checked = true;
                        }
                        else {
                            document.getElementById("<%=chkActive.ClientID%>").checked = false;
                        }
                        document.getElementById("<%=Hdn_Fld.ClientID %>").value = gvBusinessSegment.Rows[iRecordIndex].Cells[0].Value;
                    }

                    function openBSMapping(pname, pid) {

                        document.getElementById("<%=Hdn_Fld.ClientID %>").value = pid;
                        document.getElementById("<%=txtBS.ClientID %>").value = pname;
                        document.getElementById("<%=btnProductMapping.ClientID %>").click();
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
            </div>
            
        </ContentTemplate>
        
    </asp:UpdatePanel>
    
</asp:Content>