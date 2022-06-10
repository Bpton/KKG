<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true"
    CodeFile="frmTermsMaster.aspx.cs" Inherits="VIEW_frmTermsMaster" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register assembly="ProudMonkey.Common.Controls" namespace="ProudMonkey.Common.Controls" tagprefix="cc1" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">     
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                <h3>Terms & Condition Master</h3>
            </div>--%>
         
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>Terms & Condition Details</h6>
                                     <div class="btn_30_light" style="float: right;" id="btnaddhide" runat="server">
                                     <span class="icon add_co"></span>
                                    <asp:Button ID="btnAddTerms" runat="server" Text="Add New Record" CssClass="btn_link"
                                         OnClick="btnAddTerms_Click" CausesValidation="false"  /></div>
                                        
                            </div>
                            <div class="widget_content">                              
                                <asp:Panel ID="pnlAdd" runat="server">
                                     <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td width="130" class="field_title"><asp:Label ID="lblName" Text="Name" runat="server"></asp:Label><span class="req">*</span></td>
                                            <td class="field_input">
                                                <asp:TextBox ID="txtName" runat="server" CssClass="mid"></asp:TextBox>                                                     
                                                        <asp:RequiredFieldValidator ID="CV_Name" runat="server" Display="None" ErrorMessage="Name is required!"
                                                            ControlToValidate="txtName" ValidateEmptyText="false" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                            TargetControlID="CV_Name" PopupPosition="Right" HighlightCssClass="errormassage"
                                                            WarningIconImageUrl="../images/050.png">
                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title"><asp:Label ID="Label1" Text="Description" runat="server"></asp:Label><span class="req">*</span></td>
                                            <td class="field_input">
                                            <asp:TextBox ID="Textdesc" runat="server"  TextMode="MultiLine" Width="280"></asp:TextBox>                                                     
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None" ErrorMessage="Description is required!"
                                                            ControlToValidate="Textdesc" ValidateEmptyText="false" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                                            TargetControlID="RequiredFieldValidator1" PopupPosition="BottomRight" HighlightCssClass="errormassage"
                                                            WarningIconImageUrl="../images/050.png">
                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title"><asp:Label ID="lblPageName" Text="Menu Option" runat="server"></asp:Label><span class="req">*</span></td>
                                            <td class="field_input">
                                                <cc1:Grid ID="gvtermsmap" runat="server" CallbackMode="false" Serialize="true" FolderStyle="../GridStyles/premiere_blue"
                                        AutoGenerateColumns="false" AllowPaging="false" AllowPageSizeSelection="false" AllowRecordSelection="false"
                                        AllowAddingRecords="false" PageSize="1000" OnRowDataBound="gvtermsmap_RowDataBound"   >                               
                                        <Columns>                             
                                     
                                        <cc1:Column ID="Column4"  DataField="PAGENAME" HeaderText="MENU OPTION" runat="server" Width="250" >
                                        
                                        </cc1:Column>
                                        <cc1:Column ID="Column5"  DataField="ID" ReadOnly="true" HeaderText="ID" Width="100" runat="server" >                                      
                                        <TemplateSettings TemplateID="CheckTemplateTPU" HeaderTemplateId="HeaderTemplateTPU" />
                                        </cc1:Column>
                                        </Columns>
                                        <FilteringSettings MatchingType="AnyFilter" />

                                        <Templates>
                                       <%-- <cc1:GridTemplate runat="server" ID="HeaderTemplateTPU">
                                        <Template>
                                        <input type="checkbox"  id="chk_all" onclick="checkAll()" />
                                        </Template>
                                        </cc1:GridTemplate>--%>
                                        <cc1:GridTemplate runat="server" ID="CheckTemplateTPU">
                                        <Template>
                                        <asp:HiddenField runat="server" ID="hdnTPUName" Value='<%# Container.DataItem["PAGENAME"] %>' />                               
                                        <asp:CheckBox ID="ChkIDTPU" runat="server" Text=" " ToolTip="<%# Container.Value %>" />
                                        </Template>
                                        </cc1:GridTemplate>
                                        </Templates>
                                        <ScrollingSettings ScrollWidth="100%" ScrollHeight="200" />
                                        </cc1:Grid>
                                            </td>
                                        </tr> 
                                        <tr>
                                            <td class="field_title"><asp:Label ID="LabelActive" Text="IsApproved" runat="server"></asp:Label></td>
                                            <td class="field_input"><asp:CheckBox ID="chkApproved" runat="server" Text=" " /></td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td style="padding:8px 0;">
                                            <div class="btn_24_blue">
                                              <span class="icon disk_co"></span>
                                                <asp:Button ID="btnTermsSubmit" runat="server" Text="Save" CssClass="btn_link" onclick="btnTermsSubmit_Click"  />
                                            </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                            <div class="btn_24_blue">
                                                <span class="icon cross_octagon_co"></span>
                                                <asp:Button ID="btnTermsCancel" runat="server" Text="Cancel"  CssClass="btn_link" onclick="btnTermsCancel_Click" CausesValidation="false" />
                                            </div>
                                                <asp:HiddenField ID="Hdn_Fld" runat="server" />
                                            </td>
                                        </tr>                                   
                                    </table>                
                                </asp:Panel>
                                <asp:Panel ID="pnlDisplay" runat="server">  
                                 <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td" id="Excelid" runat="server">                                                                       
                                            <tr>
                                                <td>
                                                    &nbsp;</td>
                                                <td ID="tdExcel" runat="server" class="field_input">
                                                    <div class="btn_24_blue">
                                                        <span class="icon doc_excel_table_co"></span><a href="#" 
                                                            onclick="exportToExcel();">
                                                        <asp:Button ID="btnExport" runat="server" CssClass="btn_link" 
                                                            Text="Export Excel" />
                                                        </a>
                                                    </div>
                                                </td>
                                            </tr>
                                            </table>
                                <ul id="search_box">                    
                                    <li>
                                        <input name="" id="FilterTextBox" type="text" class="search_input" placeholder="Enter text to search the whole grid" onkeyup="FilterTextBox_KeyUp();">                                      
                                    </li>
                                    <li>
                                        <input name="" id="OboutButton2" type="submit" value="" class="search_btn" onclick="return performSearch();">                                      
                                     </li>                                                                                  
                                </ul>
                                <div class="gridcontent">
                                    <cc1:Grid ID="gvTerms" runat="server" CallbackMode="true" Serialize="true" FolderStyle="../GridStyles/premiere_blue"
                                        AutoGenerateColumns="false" AllowPageSizeSelection="false" 
                                        OnDeleteCommand="DeleteRecord" AllowAddingRecords="false"
                                        AllowFiltering="true" onrowdatabound="gvTerms_RowDataBound"  FolderExports="resources/exports" OnExported="gvTerms_Exported" OnExporting="gvTerms_Exporting">

                                     <ExportingSettings ExportColumnsFooter="true" ExportGroupFooter="true" ExportGroupHeader="true" 
                                     FileName="Terms_Conditions_Details" ExportAllPages="true" ExportDetails="true"/>

                                        <ClientSideEvents  OnBeforeClientDelete="OnBeforeDelete" OnClientDelete="OnDelete"   />                                        
			                           <FilteringSettings InitialState="Visible" />
                                        <FilteringSettings MatchingType="AnyFilter" /> 
                                        <Columns>
                                            <cc1:Column DataField="ID" ReadOnly="true" HeaderText="ID" runat="server" Visible="false" />
                                            
                                            <cc1:Column DataField="NAME" HeaderText="NAME" runat="server" Width="200px" >
                                                <FilterOptions>
                                                <cc1:FilterOption Type="NoFilter" />				       
                                                <cc1:FilterOption Type="Contains" />				        
                                                <cc1:FilterOption Type="StartsWith" />				        
                                                </FilterOptions>	
                                           </cc1:Column> 
                                            <cc1:Column DataField="DESCRIPTION" HeaderText="DESCRIPTION" runat="server" Width="450px"  Visible="false" >
                                                <FilterOptions>
                                                <cc1:FilterOption Type="NoFilter" />				       
                                                <cc1:FilterOption Type="Contains" />				        
                                                <cc1:FilterOption Type="StartsWith" />				        
                                                </FilterOptions>	
                                           </cc1:Column>   
                                            <cc1:Column ID="Column3" DataField="MOID" ReadOnly="true" HeaderText="ID" runat="server" Visible="false" />
                                            <cc1:Column ID="Column1" DataField="MONAME" HeaderText="MENU OPTIONS" runat="server" Width="140" Visible="false" >
                                                <FilterOptions>
                                                <cc1:FilterOption Type="NoFilter" />				       
                                                <cc1:FilterOption Type="Contains" />				        
                                                <cc1:FilterOption Type="StartsWith" />				        
                                                </FilterOptions>	
                                           </cc1:Column>  
                                           <cc1:Column ID="Column6" DataField="MAPPEDMENU" HeaderText="MAPPED MENU" runat="server" Width="880px">
                                                <FilterOptions>
                                                <cc1:FilterOption Type="NoFilter" />				       
                                                <cc1:FilterOption Type="Contains" />				        
                                                <cc1:FilterOption Type="StartsWith" />				        
                                                </FilterOptions>	
                                           </cc1:Column>  
                                            <cc1:Column ID="Column2" DataField="ISAPPROVED" HeaderText="ISACTIVE" SortOrder="Asc" runat="server"
                                                Width="120px">
                                                 <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="Contains" />
                                                </FilterOptions>
                                            </cc1:Column>                                       
                                          
                                          
                                            <cc1:Column AllowEdit="true" AllowDelete="true" HeaderText="Edit" runat="server" Width="80">
                                                <TemplateSettings TemplateId="editBtnTemplate" />
                                            </cc1:Column>
                                            <cc1:Column HeaderText="Delete" AllowEdit="false" AllowDelete="true" Width="80" >
                                            <TemplateSettings TemplateId="deleteBtnTemplate" />
                                            </cc1:Column>
                                        </Columns>                                                    
                                        <Templates>
                                                    <cc1:GridTemplate runat="server" ID="editBtnTemplate" >
                                                    <Template>
                                                    <a href="javascript: //" class="action-icons c-edit" id="btnGridEdit_<%# Container.PageRecordIndex %>"
                                                        onclick="CallServerMethod(this)"></a>                                          
                                                    </Template>
                                                    </cc1:GridTemplate>
                                        </Templates>
                                         <Templates>
                                                    <cc1:GridTemplate runat="server" ID="deleteBtnTemplate" >
                                                    <Template>
                                                    <a href="javascript: //" class="action-icons c-delete" title="Delete" onclick="gvTerms.delete_record(this)"  >
                                                    </a>                                             
                                                    </Template>
                                                    </cc1:GridTemplate>
                                        </Templates>
                                        <ScrollingSettings ScrollWidth="100%" ScrollHeight="290" />
                                        </cc1:Grid>  
                                        <asp:Button ID="btngridsave" runat="server" Text="GridSave" Style="display: none" OnClick="btngridsave_Click" CausesValidation="false" />
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
                   function exportToExcel() {
                       gvTerms.exportToExcel();
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

            gvTerms.addFilterCriteria('MONAME', OboutGridFilterCriteria.Contains, searchValue);
            gvTerms.addFilterCriteria('NAME', OboutGridFilterCriteria.Contains, searchValue);
            gvTerms.addFilterCriteria('DESCRIPTION', OboutGridFilterCriteria.Contains, searchValue);
            gvTerms.executeFilter();

            searchTimeout = null;

            return false;
        }
    </script>
    <script type="text/javascript">
        function CallServerMethod(oLink) {
            var iRecordIndex = oLink.id.toString().replace("btnGridEdit_", "");
            document.getElementById("<%=btnaddhide.ClientID%>").style.display = "none";
            document.getElementById("<%=Hdn_Fld.ClientID %>").value = gvTerms.Rows[iRecordIndex].Cells[0].Value;
            document.getElementById("<%=btngridsave.ClientID %>").click();
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
