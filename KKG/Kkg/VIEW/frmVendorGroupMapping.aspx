<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmVendorGroupMapping.aspx.cs" Inherits="VIEW_frmVendorGroupMapping" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        function CheckAllheader(Checkbox) {
            var GridVwHeaderCheckbox = document.getElementById("<%=gvVendor.ClientID %>");
            for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
                GridVwHeaderCheckbox.rows[i].cells[2].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
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
                <h3>Beat Master</h3>
            </div>--%>
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>
                                    Vendor Group Mapping
                                </h6>
                                <div class="btn_30_light" style="float: right;" id="btnaddhide" runat="server">
                                    <span class="icon add_co"></span>
                                    <asp:Button ID="btnAddnewRecord" runat="server" Text="Add New Record" CssClass="btn_link"
                                        OnClick="btnAddnewRecord_Click" CausesValidation="false" /></div>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td width="130" align="left" class="field_title">
                                                <asp:Label ID="lblCode" Text="Vendor Group Code" runat="server"></asp:Label>
                                                <span class="req">*</span>
                                            </td>
                                            <td  class="field_input" width="200">
                                                <asp:TextBox ID="txtCode" runat="server" CssClass="x_large" MaxLength="20"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="CV_txtCode" runat="server" Display="None" ErrorMessage=" Code is required!"
                                                    ControlToValidate="txtCode" ValidateEmptyText="false" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender19" runat="server"
                                                    TargetControlID="CV_txtCode" PopupPosition="Right" HighlightCssClass="errormassage"
                                                    WarningIconImageUrl="../images/050.png">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                             <td class="field_title" width="130">
                                            <asp:Label ID="lblName" Text="Vendor Group Name" runat="server"></asp:Label>
                                            <span class="req">*</span>
                                        </td>
                                        <td class="field_input">
                                            <asp:TextBox ID="txtName" runat="server" CssClass="mid"> </asp:TextBox>
                                            <asp:RequiredFieldValidator ID="CV_Name" runat="server" Display="None" ErrorMessage="Name is required!"
                                                ControlToValidate="txtName" ValidateEmptyText="false" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                TargetControlID="CV_Name" PopupPosition="Right" HighlightCssClass="errormassage"
                                                WarningIconImageUrl="../images/050.png">
                                            </ajaxToolkit:ValidatorCalloutExtender>
                                        </td>
                                        </tr>
                                        <tr>
                                       
                                        </tr>
                                        <td class="field_input" style="padding-left: 10px;" colspan="2">
                                        <table>
                                        
                                        <tr>
                                        <td>
                                          <fieldset>
                                                <legend>Vendor Details</legend>
                                                <div style="margin: 0 auto; width: 400px;">
                                                    <div style="overflow: hidden;" id="DivHeaderRow" >
                                                    </div>
                                                    <div id="DivMainContent" style="overflow: scroll; height: 300px;" onscroll="OnScrollDiv(this);">
                                                        <asp:GridView ID="gvVendor" runat="server" Width="100%" CssClass="zebra" AutoGenerateColumns="false"
                                                            EmptyDataText="No Records Available" ShowFooter="true">
                                                            <Columns>

                                                                <asp:TemplateField HeaderText="Sl No" HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <%# Container.DataItemIndex + 1 %>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle  HorizontalAlign="Left"></HeaderStyle>
                                                                    <ItemStyle  HorizontalAlign="Right"></ItemStyle>
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="VENDORID" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblVENDORID" runat="server" Text='<%# Bind("VENDORID") %>' value='<%# Eval("VENDORID") %>'
                                                                            Visible="false"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="VENDOR NAME">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblVENDORNAME" runat="server" Text='<%# Bind("VENDORNAME") %>' value='<%# Eval("VENDORNAME") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox ID="chkAllSelect" runat="server" onclick="CheckAllheader(this);" Text=" " />
                                                                    </HeaderTemplate>
                                                                    <HeaderStyle Width="10px" />
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkSelect" runat="server" Text=" " />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                    <div id="DivFooterRow" style="overflow: hidden;">
                                                    </div>
                                                </div>
                                            </fieldset>
                                        </td>
                                        </tr>
                                        </table>
                                            
                                        </td>
                                        <tr>
                                            <td>
                                            </td>
                                            <td style="padding: 8px 0px;">
                                                <div class="btn_24_blue">
                                                    <span class="icon disk_co"></span>
                                                    <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="btn_link" OnClick="btnSubmit_Click" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
                                                    <span class="icon cross_octagon_co"></span>
                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn_link" OnClick="btnCancel_Click"
                                                        CausesValidation="false" />
                                                </div>
                                                <asp:HiddenField ID="Hdn_Fld" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlDisplay" runat="server">
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
                                        <cc1:Grid ID="gvVendorGroupMapping" runat="server" CallbackMode="true" Serialize="true"
                                            FolderStyle="../GridStyles/premiere_blue" AutoGenerateColumns="false" AllowPageSizeSelection="false"
                                            OnDeleteCommand="DeleteRecord" PageSize="200" AllowPaging="true" AllowAddingRecords="false"
                                            AllowFiltering="true" EnableRecordHover="true">
                                            <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnClientDelete="OnDelete" />
                                            <FilteringSettings InitialState="Visible" />
                                            <Columns>
                                                <cc1:Column DataField="GROUPID" ReadOnly="true" HeaderText="GROUPID" runat="server"
                                                    Visible="false" />
                                                <cc1:Column ID="Column12" DataField="Sl" HeaderText="SL" runat="server" Width="5%">
                                                    <TemplateSettings TemplateId="slnoTemplateIN" />
                                                </cc1:Column>
                                                <cc1:Column DataField="GROUPCODE" HeaderText="GROUP CODE" runat="server" Width="50"
                                                    Wrap="true">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column DataField="GROUPNAME" HeaderText="GROUP NAME" runat="server" Wrap="true"
                                                    Width="100">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column AllowEdit="true" AllowDelete="true" HeaderText="EDIT" runat="server"
                                                    Width="70">
                                                    <TemplateSettings TemplateId="editBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column HeaderText="DELETE" AllowEdit="false" AllowDelete="true" Width="70">
                                                    <TemplateSettings TemplateId="deleteBtnTemplate" />
                                                </cc1:Column>
                                            </Columns>
                                            <FilteringSettings MatchingType="AnyFilter" />
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="editBtnTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="action-icons c-edit" id="btnGridEdit_<%# Container.PageRecordIndex %>"
                                                            onclick="CallServerMethod(this);"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                                <cc1:GridTemplate runat="server" ID="slnoTemplateIN">
                                                    <Template>
                                                        <asp:Label ID="lblslnoin" runat="server" Text='<%#Container.PageRecordIndex+1 %>'></asp:Label>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="deleteBtnTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="action-icons c-delete" title="Delete" onclick="gvVendorGroupMapping.delete_record(this)">
                                                        </a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <ScrollingSettings ScrollWidth="100%" ScrollHeight="290" />
                                        </cc1:Grid>
                                        <asp:Button ID="btngridedit" runat="server" Text="Gridedit" CausesValidation="false"
                                            Style="display: none" OnClick="btngridedit_Click" />
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
                    document.getElementById("<%=Hdn_Fld.ClientID %>").value = record.GROUPID;
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
                function isNumberKey(evt) {
                    var charCode = (evt.which) ? evt.which : event.keyCode;
                    if (charCode < 48 || charCode > 57)
                        return false;
                    return true;
                }    
            </script>
            <script type="text/javascript">
                function CallServerMethod(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnGridEdit_", "");
                    document.getElementById("<%=Hdn_Fld.ClientID %>").value = gvVendorGroupMapping.Rows[iRecordIndex].Cells[0].Value;
                    document.getElementById("<%=btngridedit.ClientID %>").click();
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

                    gvVendorGroupMapping.addFilterCriteria('ADV_CATEGORYNAME', OboutGridFilterCriteria.Contains, searchValue);
                    gvVendorGroupMapping.addFilterCriteria('ADV_CATEGORYCODE', OboutGridFilterCriteria.Contains, searchValue);
                    gvVendorGroupMapping.addFilterCriteria('NAME', OboutGridFilterCriteria.Contains, searchValue);

                    gvVendorGroupMapping.executeFilter();
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