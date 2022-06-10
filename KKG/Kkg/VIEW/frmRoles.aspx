<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmRoles.aspx.cs" Inherits="VIEW_frmRoles" %>

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
                <h3>Roles</h3>
            </div>--%>
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>Role Details</h6>
                                <div class="btn_30_light" style="float: right;" id="btnaddhide" runat="server">
                                    <span class="icon add_co"></span>
                                    <asp:Button ID="btnAddUserType" runat="server" Text="Add New Record" CssClass="btn_link"
                                        OnClick="btnAddUserType_Click" CausesValidation="false" /></div>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td width="120" class="field_title"><asp:Label ID="lblUTCode" Text="Code" runat="server"></asp:Label> <span class="req">*</span></td>
                                            <td class="field_input">
                                                <asp:TextBox ID="txtUTCode" runat="server" CssClass="mid" MaxLength="20"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="CV_UTCode" runat="server" Display="None" ErrorMessage="UserType Code is required!"
                                                            ControlToValidate="txtUTCode" ValidateEmptyText="false" SetFocusOnError="true" ValidationGroup="A" ></asp:RequiredFieldValidator>
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                                            TargetControlID="CV_UTCode" PopupPosition="Right" HighlightCssClass="errormassage" 
                                                            WarningIconImageUrl="../images/050.png">
                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title"><asp:Label ID="lblUTName" Text="Name" runat="server"></asp:Label> <span class="req">*</span></td>
                                            <td class="field_input">
                                                <asp:TextBox ID="txtUTName" runat="server" CssClass="mid"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="CV_UTName" runat="server" Display="None" ErrorMessage="UserType Name is required!"
                                                           ControlToValidate="txtUTName" ValidateEmptyText="false" SetFocusOnError="true"  ValidationGroup="A"></asp:RequiredFieldValidator>
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                            TargetControlID="CV_UTName" PopupPosition="Right" HighlightCssClass="errormassage" 
                                                            WarningIconImageUrl="../images/050.png">
                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title"><asp:Label ID="lblDescription" Text="Description" runat="server"></asp:Label> <span class="req">*</span></td>
                                            <td class="field_input">
                                                <asp:TextBox ID="txtDescription" runat="server" CssClass="mid" ></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="CV_Description" runat="server" Display="None" ErrorMessage="Description is required!"
                                                            ControlToValidate="txtDescription" ValidateEmptyText="false" SetFocusOnError="true"  ValidationGroup="A" ></asp:RequiredFieldValidator>
                                               <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                                            TargetControlID="CV_Description" PopupPosition="Right" HighlightCssClass="errormassage" 
                                                            WarningIconImageUrl="../images/050.png">
                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                        </tr>
                                          <tr>
                                            <td class="field_title">
                                                <asp:Label ID="lblApplicableto" runat="server" Text="APPLICABLE TO"></asp:Label>
                                                <span class="req">*</span>
                                            </td>
                                            <td class="field_input">
                                                    <asp:DropDownList ID="ddlApplicableto" runat="server" CssClass="chosen-select" Width="250">
                                                        <asp:ListItem Value="D">Distribution Channel</asp:ListItem>
                                                        <asp:ListItem Value="C">Company</asp:ListItem>
                                                        <asp:ListItem Value="T">Third Party</asp:ListItem>
                                                    </asp:DropDownList>
                                                     <asp:RequiredFieldValidator ID="rfv_ddlApplicableto" runat="server"  ErrorMessage=" ReportTo is rquired!" ForeColor="Red"
                                                            ControlToValidate="ddlApplicableto" ValidateEmptyText="false" SetFocusOnError="true"  ValidationGroup="A"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                          <tr>
                                            <td class="field_title"><asp:Label ID="Label2" Text="Report To" runat="server"></asp:Label> <span class="req">*</span></td>
                                            <td class="field_input">
                                                <asp:ListBox  ID="ddlReportto" runat="server" data-placeholder="Choose ReportTo" CssClass="chosen-select" SelectionMode="Multiple" tabindex="4" Width="280"></asp:ListBox>
                                                   <asp:RequiredFieldValidator ID="rfv_ddlReportto" runat="server"  ErrorMessage=" ReportTo is rquired!" ForeColor="Red"
                                                            ControlToValidate="ddlReportto" ValidateEmptyText="false" SetFocusOnError="true" InitialValue="0"  ValidationGroup="A"></asp:RequiredFieldValidator>
                                                     <%--<ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
                                                            TargetControlID="rfv_ddlReportto" PopupPosition="Right" HighlightCssClass="errormassage"
                                                            WarningIconImageUrl="../images/050.png" >
                                                        </ajaxToolkit:ValidatorCalloutExtender>--%>   
                                            </td>
                                        </tr>
                                        <tr  id="Similaras" runat="server" >
                                            <td class="field_title">
                                                <asp:Label ID="lblSimilaras" runat="server" Text="SIMILAR AS"></asp:Label>
                                                <span class="req">*</span>
                                            </td>
                                            <td class="field_input">
                                                    <asp:DropDownList ID="ddlSimilaras" runat="server" CssClass="chosen-select" Width="250" AppendDataBoundItems="True" ValidationGroup="Show">
                                                    </asp:DropDownList>
                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"  ErrorMessage=" ReportTo is rquired!" ForeColor="Red"
                                                            ControlToValidate="ddlSimilaras" ValidateEmptyText="false" SetFocusOnError="true"  ValidationGroup="A"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        
                                       
                                        <tr>
                                            <td class="field_title"><asp:Label ID="Label1" Text="Active" runat="server"></asp:Label></td>
                                            <td class="field_input"><asp:CheckBox ID="chkActive" runat="server" Text=" " /></td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td style="padding:8px 0;">
                                            <div class="btn_24_blue" >
                                                <span class="icon disk_co"></span>
                                                <asp:Button ID="btnUTSubmit" runat="server" Text="Save" CssClass="btn_link" OnClick="btnUTSubmit_Click"  ValidationGroup="A" />
                                            </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue" >
                                                <span class="icon cross_octagon_co"></span>
                                                <asp:Button ID="btnUTCancel" runat="server" Text="Cancel" CssClass="btn_link" OnClick="btnUTCancel_Click" CausesValidation="false" />
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
                                        <cc1:Grid ID="gvUserType" runat="server" CallbackMode="true" Serialize="true" FolderStyle="../GridStyles/premiere_blue"
                                        AutoGenerateColumns="false" OnDeleteCommand="DeleteRecord" PageSize="200" EnableRecordHover="true"
                                            AllowAddingRecords="false" AllowFiltering="true" AllowPageSizeSelection="true" 
                                            onrowdatabound="gvUserType_RowDataBound">
                                        <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnClientDelete="OnDelete" />
                                        <FilteringSettings InitialState="Visible" />
                                        <Columns>
                                            <cc1:Column DataField="UTID" ReadOnly="true" HeaderText="UTID" runat="server" Visible="false" />
                                            <cc1:Column ID="Column20" DataField="Sl" HeaderText="SL" runat="server" Width="60">
                                                <TemplateSettings TemplateId="slnoTemplateIN" />
                                            </cc1:Column>
                                            <cc1:Column ID="Column1" DataField="UTCODE" HeaderText=" CODE" runat="server" Width="80" Wrap="true">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="EqualTo" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="DoesNotContain" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                </FilterOptions>
                                            </cc1:Column>
                                            <cc1:Column DataField="UTNAME" HeaderText="ROLE NAME" runat="server" Width="210" Wrap="true">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="EqualTo" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="DoesNotContain" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                            </cc1:Column>
                                            <cc1:Column DataField="UTDESCRIPTION" HeaderText="DESCRIPTION" runat="server" Width="210" Wrap="true">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="EqualTo" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="DoesNotContain" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                            </cc1:Column>
                                              <cc1:Column  DataField="PARENTNAME" HeaderText="REPORTING TO" runat="server" Width="100%" Wrap="true">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="EqualTo" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="DoesNotContain" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                            </cc1:Column>
                                            <cc1:Column DataField="ACTIVE" HeaderText="STATUS" runat="server" Width="80">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="EqualTo" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="DoesNotContain" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                </FilterOptions>
                                            </cc1:Column>
                                            <cc1:Column AllowEdit="true" AllowDelete="true" HeaderText="EDIT" runat="server" Width="60" Wrap="true">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="EqualTo" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="DoesNotContain" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                </FilterOptions>
                                                <TemplateSettings TemplateId="editBtnTemplate" />
                                            </cc1:Column>
                                            <cc1:Column HeaderText="DELETE" AllowEdit="false" AllowDelete="true" Width="80" Wrap="true" Visible="false">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="EqualTo" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="DoesNotContain" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                </FilterOptions>
                                                <TemplateSettings TemplateId="deleteBtnTemplate" />
                                            </cc1:Column>
                                        </Columns>
                                        <FilteringSettings MatchingType="AnyFilter" />
                                         <Templates>
                                                    <cc1:GridTemplate runat="server" ID="editBtnTemplate" >
                                                    <Template>
                                                    <a href="javascript: //" class="action-icons c-edit" title="Edit" id="btnGridEdit_<%# Container.PageRecordIndex %>"" onclick="CallServerMethod(this)"  >
                                                    </a>                                             
                                                    </Template>
                                                    </cc1:GridTemplate>
                                        </Templates>
                                         <Templates>
                                                    <cc1:GridTemplate runat="server" ID="deleteBtnTemplate" >
                                                    <Template>
                                                    <a href="javascript: //" class="action-icons c-delete" title="Delete" onclick="gvUserType.delete_record(this)"  >
                                                    </a>                                             
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
                                        <ScrollingSettings ScrollWidth="100%" ScrollHeight="320" />
                                    </cc1:Grid>
                                      <asp:Button ID="btngridedit" runat="server" Text="gridedit" Style="display: none" CausesValidation="false"   OnClick="btngridedit_Click"/>
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
                    function OnBeforeDelete(record) {
                        record.Error = '';
                        document.getElementById("<%=Hdn_Fld.ClientID %>").value = record.UTID;
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
                        gvUserType.addFilterCriteria('UTCODE', OboutGridFilterCriteria.Contains, searchValue);
                        gvUserType.addFilterCriteria('UTNAME', OboutGridFilterCriteria.Contains, searchValue);
                        gvUserType.addFilterCriteria('UTDESCRIPTION', OboutGridFilterCriteria.Contains, searchValue);
                        gvUserType.executeFilter();
                        searchTimeout = null;
                        return false;
                    }
                </script>
                <script type="text/javascript">
                    function CallServerMethod(oLink) {
                        var iRecordIndex = oLink.id.toString().replace("btnGridEdit_", "");

                        document.getElementById("<%=Hdn_Fld.ClientID %>").value = gvUserType.Rows[iRecordIndex].Cells[0].Value;
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>