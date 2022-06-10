<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmDistrictMaster.aspx.cs" Inherits="VIEW_frmDistrictMaster" %>

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
            <div class="page_title">
                <span class="title_icon"><span class="computer_imac"></span></span>
                <h3>
                    District Master</h3>
            </div>
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>
                                    District Details</h6>
                                <div class="btn_30_light" style="float: right;" id="btnaddhide" runat="server">
                                    <span class="icon add_co"></span>
                                    <asp:Button ID="btnAdd" runat="server" Text="Add New Record" CssClass="btn_link" OnClick="btnAdd_Click"
                                        CausesValidation="false" /></div>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlAdd" runat="server">
                                 <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                        <td width="15%" class="field_title">Sate <span class="req">*</span></td>
                                        <td class="field_input">
                                            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td width="23%">
                                                 
                                                                <asp:DropDownList ID="ddlState" runat="server"  data-placeholder="Select State"
                                                                    AppendDataBoundItems="True" Width ="250" class="chosen-select">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfv_ddlState" runat="server" ControlToValidate="ddlState"
                                                                    InitialValue="0" SetFocusOnError="true" ErrorMessage="State is required!" ForeColor="Red" >
                                                                    </asp:RequiredFieldValidator>
                                                               
                                                           
                                                    </td>
                                                
                                                </tr>
                                            </table>
                                        </td>
                                        </tr>
                                        <tr>
                                        <td class="field_title"><asp:Label ID="Label4" runat="server" Text="District"></asp:Label> <span class="req">*</span></td>
                                        <td class="field_input">
                                        <asp:TextBox ID="txtdistrict" runat="server" CssClass="mid" ToolTip="Enter District Name"> </asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfv_txtdistrict" runat="server" ControlToValidate="txtdistrict"
                                                            SetFocusOnError="true" ErrorMessage="District is required!" Display="None"></asp:RequiredFieldValidator>
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                                            TargetControlID="rfv_txtdistrict" PopupPosition="Right" HighlightCssClass="errormassage"
                                                            WarningIconImageUrl="../images/050.png">
                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                        </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td style="padding:8px 0">
                                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn_small btn_blue"
                                                                OnClick="btnSave_Click" />
                                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn_small btn_blue"
                                                                OnClick="btnCancel_Click" CausesValidation="false" />
                                                            <asp:HiddenField ID="Hdn_Fld" runat="server" />
                                            </td>
                                        </tr>
                                        </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlDisplay" runat="server">
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
                                    <cc1:Grid ID="gvDistrict" runat="server" CallbackMode="false" Serialize="true" FolderStyle="../GridStyles/premiere_blue"
                                        AutoGenerateColumns="false" AllowPageSizeSelection="false" OnDeleteCommand="DeleteRecord" PageSize="200" EnableRecordHover="true"
                                        AllowAddingRecords="false" AllowFiltering="true">
                                        <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnClientDelete="OnDelete" />
                                        <FilteringSettings InitialState="Visible" />
                                        <Columns>
                                            <cc1:Column ID="Column1" DataField="STATE_ID" ReadOnly="true" HeaderText="City_ID"
                                                runat="server" Visible="false" />
                                            <cc1:Column ID="Column2" DataField="STATE_NAME" HeaderText="State" runat="server"
                                                Width="140">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                </FilterOptions>
                                            </cc1:Column>
                                            <cc1:Column ID="Column3" DataField="DISTRICT_ID" HeaderText="District" runat="server"
                                                Width="80" Visible="false">
                                               
                                            </cc1:Column>
                                            <cc1:Column ID="Column4" DataField="DISTRICT_NAME" HeaderText="District" runat="server" Width="180">
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
                                            <cc1:Column HeaderText="Delete" AllowEdit="false" AllowDelete="true" Width="80" Visible="false">
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
                                                    <a href="javascript: //" class="action-icons c-delete" title="Delete" onclick="gvDistrict.delete_record(this)">
                                                    </a>
                                                </Template>
                                            </cc1:GridTemplate>
                                        </Templates>
                                        <ScrollingSettings ScrollWidth="100%" />
                                    </cc1:Grid>
                                    <asp:Button ID="btngridedit" runat="server" Text="GridSave" Style="display: none"
                                        OnClick="btngridedit_Click" CausesValidation="false" />
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

                    function CallServerMethod(oLink) {
                        var iRecordIndex = oLink.id.toString().replace("btnGridEdit_", "");
                        document.getElementById("<%=Hdn_Fld.ClientID %>").value = gvDistrict.Rows[iRecordIndex].Cells[2].Value;
                        document.getElementById("<%=btngridedit.ClientID %>").click();

                    }

                    function OnBeforeDelete(record) {
                        record.Error = '';
                        document.getElementById("<%=Hdn_Fld.ClientID %>").value = record.DISTRICT_ID;
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

                        gvDistrict.addFilterCriteria('STATE_NAME', OboutGridFilterCriteria.Contains, searchValue);
                        gvDistrict.addFilterCriteria('DISTRICT_NAME', OboutGridFilterCriteria.Contains, searchValue);
                       
                        gvDistrict.executeFilter();
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