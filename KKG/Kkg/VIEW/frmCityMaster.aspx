<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmCityMaster.aspx.cs" Inherits="VIEW_frmCityMaster" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
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
                    City Master</h3>
            </div>--%>
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>
                                    City Details</h6>
                                <div class="btn_30_light" style="float: right;" id="btnaddhide" runat="server">
                                    <span class="icon add_co"></span>
                                    <asp:Button ID="btnAddCity" runat="server" Text="Add New Record" CssClass="btn_link" OnClick="btnAddCity_Click"
                                        CausesValidation="false" /></div>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlAdd" runat="server">
                                 <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                        <td width="120" class="field_title">Location <span class="req">*</span></td>
                                        <td class="field_input">
                                            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td width="23%">
                                                    <div class="select-style-inner" style="width: 180px;">
                                                                <asp:DropDownList ID="ddlState1" runat="server" AutoPostBack="true" data-placeholder="Select State"
                                                                    OnSelectedIndexChanged="ddlState1_SelectedIndexChanged" AppendDataBoundItems="true">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlState1"
                                                                    InitialValue="0" SetFocusOnError="true" ErrorMessage="State is required!" Display="None"></asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                                    TargetControlID="RequiredFieldValidator2" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="../images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                            </div>
                                                            <span class="label_intro">State</span>
                                                    </td>
                                                    <td>
                                                    <div class="select-style-inner" style="width: 180px;">
                                                                <asp:DropDownList ID="ddlDistrict" runat="server" AutoPostBack="true" data-placeholder="Select District"
                                                                    AppendDataBoundItems="True">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlDistrict"
                                                                    InitialValue="0" SetFocusOnError="true" ErrorMessage="District is required!"
                                                                    Display="None"></asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                                                    TargetControlID="RequiredFieldValidator3" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="../images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                            </div>
                                                            <span class=" label_intro">District</span>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        </tr>
                                        <tr>
                                        <td class="field_title"><asp:Label ID="Label4" runat="server" Text="City"></asp:Label> <span class="req">*</span></td>
                                        <td class="field_input">
                                        <asp:TextBox ID="txtcity" runat="server" CssClass="mid" ToolTip="Enter City Name" MaxLength="80"> </asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtcity"
                                                            SetFocusOnError="true" ErrorMessage="City is required!" Display="None"></asp:RequiredFieldValidator>
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                                            TargetControlID="RequiredFieldValidator1" PopupPosition="Right" HighlightCssClass="errormassage"
                                                            WarningIconImageUrl="../images/050.png">
                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                        </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td style="padding:8px 0">
                                            <div class="btn_24_blue">
                                              <span class="icon disk_co"></span>
                                                <asp:Button ID="btnSaveCity" runat="server" Text="Save" CssClass="btn_link" OnClick="btnSaveCity_Click" />
                                             </div>
                                             &nbsp;&nbsp;&nbsp;&nbsp;
                                             <div class="btn_24_blue">
                                                <span class="icon cross_octagon_co"></span>
                                                <asp:Button ID="btnCancelCity" runat="server" Text="Cancel" CssClass="btn_link" OnClick="btnCancelCity_Click" CausesValidation="false" />
                                             </div>
                                                <asp:HiddenField ID="Hdn_Fld" runat="server" />
                                            </td>
                                        </tr>
                                        </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlDisplay" runat="server">
                                <tr>
                                <td>
                                <table  width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                <td width="60" class="field_title">
                                <asp:Label ID="lblstate" runat="server" Text="State"></asp:Label>
                                </td>
                                    <td width="150" class="field_title">                                                    
                                                <asp:DropDownList ID="ddlsearchstate" runat="server" AutoPostBack="true" Width="150" class="chosen-select" data-placeholder="ALL"
                                                    OnSelectedIndexChanged="ddlsearchstate_SelectedIndexChanged" AppendDataBoundItems="true">
                                                </asp:DropDownList>
                                               
                                            
                                    </td>
                                    <td width="60" class="field_title"><asp:Label ID="Label1" runat="server" Text="District"></asp:Label></td>
                                    <td width="180" class="field_title">
                                    
                                                <asp:DropDownList ID="ddlsearchdistrict" runat="server" Width="150" AutoPostBack="true" class="chosen-select" data-placeholder="ALL"
                                                    AppendDataBoundItems="True" OnSelectedIndexChanged="ddlsearchdistrict_SelectedIndexChanged">
                                                </asp:DropDownList> 
                                    </td>
                                    <td class="field_input" id="Excelid" runat="server">
                                            <div class="btn_24_blue">
                                                <span class="icon doc_excel_table_co"></span>
                                                <a href="#" onclick="exportToExcel();"><asp:Button ID="btnExport" runat="server" Text="Export Excel" CssClass="btn_link"/></a>
                                            </div>                                                
                                            </td>
                                            <td>&nbsp;</td>
                                     </table>
                                     </td>
                                                </tr>
                                    <ul id="search_box">
                                        <li>
                                            <input name="" id="FilterTextBox" type="text" class="search_input" placeholder="Enter text to search the whole grid"
                                                onkeyup="FilterTextBox_KeyUp();" />
                                        </li>
                                        <li>
                                            <input name="" id="OboutButton2" type="submit" value="" class="search_btn" onclick="return performSearch();">
                                        </li>
                                    </ul>
                                    <div class="gridcontent">
                                    <cc1:Grid ID="gvCity" runat="server" CallbackMode="false" Serialize="true" FolderStyle="../GridStyles/premiere_blue"
                                        AutoGenerateColumns="false" AllowPageSizeSelection="true" OnDeleteCommand="DeleteRecord" FolderExports="resources/exports" OnExported="gvCity_Exported"
                                        AllowAddingRecords="false" AllowFiltering="true" PageSize="50" OnExporting="gvCity_Exporting">
                                        <ExportingSettings ExportColumnsFooter="true" ExportGroupFooter="true" ExportGroupHeader="true" FileName="CityDetails" ExportAllPages="true" ExportDetails="true"/>
                                        <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnClientDelete="OnDelete" />
                                        <FilteringSettings InitialState="Visible" />
                                        <Columns>
                                            <cc1:Column ID="Column1" DataField="City_ID" ReadOnly="true" HeaderText="City_ID"
                                                runat="server" Visible="false" />
                                            <cc1:Column ID="Column2" DataField="State_Name" HeaderText="State" runat="server"
                                                Width="350px">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                </FilterOptions>
                                            </cc1:Column>
                                            <cc1:Column ID="Column3" DataField="District_Name" HeaderText="District" runat="server"
                                                Width="400px">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                </FilterOptions>
                                            </cc1:Column>
                                            <cc1:Column ID="Column4" DataField="City_Name" HeaderText="City" runat="server" Width="400px">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                </FilterOptions>
                                            </cc1:Column>
                                            <cc1:Column ID="Column9" AllowEdit="true" AllowDelete="true" HeaderText="Edit" runat="server"
                                                Width="80">
                                                <TemplateSettings TemplateId="editBtnTemplate" />
                                            </cc1:Column>
                                            <cc1:Column HeaderText="Delete" AllowEdit="false" AllowDelete="true" Width="80" visible="false">
                                                <TemplateSettings TemplateId="deleteBtnTemplate" />
                                            </cc1:Column>
                                        </Columns>
                                        <FilteringSettings MatchingType="AnyFilter" />
                                        <Templates>
                                            <cc1:GridTemplate runat="server" ID="editBtnTemplate">
                                                <Template>
                                                    <%--<asp:Button runat="server" CssClass="action-icons c-edit" ID="btn1"  Text="Execute " OnCommand="EditRecord"  CommandArgument= '<%# Eval("[City_ID]") %>' />--%>
                                                    <a href="javascript: //" class="action-icons c-edit" id="btnGridEdit_<%# Container.PageRecordIndex %>"
                                                        onclick="CallServerMethod(this)"></a>
                                                </Template>
                                            </cc1:GridTemplate>
                                        </Templates>
                                        <Templates>
                                            <cc1:GridTemplate runat="server" ID="deleteBtnTemplate">
                                                <Template>
                                                    <a href="javascript: //" class="action-icons c-delete" title="Delete" onclick="gvCity.delete_record(this)">
                                                    </a>
                                                </Template>
                                            </cc1:GridTemplate>
                                        </Templates>
                                        <ScrollingSettings ScrollWidth="100%" ScrollHeight="290" />
                                    </cc1:Grid>
                                    <asp:Button ID="btngridsave" runat="server" Text="GridSave" Style="display: none"
                                        OnClick="btngridsave_Click" CausesValidation="false" />
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                    <cc1:MessageBox ID="MessageBox1" runat="server" /> 
                    <span class="clear"></span>
                </div>
                <span class="clear"></span>
                <script type="text/javascript">
                    function exportToExcel() {
                        gvCity.exportToExcel();
                    }         
		        </script>
                <script type="text/javascript">

                    function CallServerMethod(oLink) {
                        var iRecordIndex = oLink.id.toString().replace("btnGridEdit_", "");
                        document.getElementById("<%=Hdn_Fld.ClientID %>").value = gvCity.Rows[iRecordIndex].Cells[0].Value;
                        document.getElementById("<%=btngridsave.ClientID %>").click();

                    }

                    function OnBeforeDelete(record) {
                        record.Error = '';
                        document.getElementById("<%=Hdn_Fld.ClientID %>").value = record.USERID;
                        if (confirm("Are you sure you want to delete? "))
                            return true;
                        else
                            return false;
                    }
                    function OnDelete(record) {
                        alert(record.Error);
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

                        gvCity.addFilterCriteria('State_Name', OboutGridFilterCriteria.Contains, searchValue);
                        gvCity.addFilterCriteria('District_Name', OboutGridFilterCriteria.Contains, searchValue);
                        gvCity.addFilterCriteria('City_Name', OboutGridFilterCriteria.Contains, searchValue);
                        gvCity.executeFilter();
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>