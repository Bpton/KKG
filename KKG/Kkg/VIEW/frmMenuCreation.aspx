<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="frmMenuCreation.aspx.cs" Inherits="VIEW_frmMenuCreation" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"
    TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
            <asp:Image ID="Image1" ImageUrl="../images/loading123.gif" AlternateText="Processing"
                runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
        PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnMenuSubmit" />
        </Triggers>
        <ContentTemplate>
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>
                                    Menu Create</h6>
                                <div class="btn_30_light" style="float: right;" id="divadd" runat="server">
                                    <span class="icon add_co"></span>
                                    <asp:Button ID="btnAddMenu" runat="server" Text="Add New Menu " CssClass="btn_link"
                                        OnClick="btnAddMenu_Click" CausesValidation="false" /></div>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_title">
                                                <asp:Label ID="lblmenuname" runat="server" Text="Menu Name"></asp:Label><span class="req">*</span>
                                            </td>
                                            <td class="field_input" colspan="3">
                                                <asp:TextBox ID="txtmenuname" runat="server" CssClass="mid" ValidationGroup="Save"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="tfv_txtmenuname" runat="server" ErrorMessage=" Name is required!"
                                                    ControlToValidate="txtmenuname" ValidateEmptyText="false" Display="None" SetFocusOnError="true"
                                                    ValidationGroup="Save"> </asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                    TargetControlID="tfv_txtmenuname" PopupPosition="Right" HighlightCssClass="errormassage"
                                                    WarningIconImageUrl="../images/050.png">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title">
                                                <asp:Label ID="lblcode" runat="server" Text=" Code"></asp:Label><span class="req">*</span>
                                            </td>
                                            <td class="field_input" colspan="3">
                                                <asp:TextBox ID="txtcode" runat="server" CssClass="mid" ValidationGroup="Save"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfv_txtcode" runat="server" ErrorMessage="Code is required!"
                                                    ValidationGroup="Save" ControlToValidate="txtcode" ValidateEmptyText="false"
                                                    Display="None" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                                    TargetControlID="rfv_txtcode" PopupPosition="Right" HighlightCssClass="errormassage"
                                                    WarningIconImageUrl="../images/050.png">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title">
                                                <asp:Label ID="lbldescription" runat="server" Text=" Description"></asp:Label><span
                                                    class="req">*</span>
                                            </td>
                                            <td class="field_input" colspan="3">
                                                <asp:TextBox ID="txtdescription" runat="server" CssClass="mid" TextMode="MultiLine"
                                                    Width="30%" ValidationGroup="Save">
                                                </asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfv_des" runat="server" ErrorMessage="Description is required!"
                                                    ValidationGroup="Save" ControlToValidate="txtdescription" ValidateEmptyText="false"
                                                    Display="None" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                                    TargetControlID="rfv_des" PopupPosition="Right" HighlightCssClass="errormassage"
                                                    WarningIconImageUrl="../images/050.png">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="15%" class="field_title">
                                                <asp:Label ID="lblParentPageName" runat="server" Text="Parent Page Name"></asp:Label>
                                            </td>
                                            <td class="field_input" colspan="3">
                                                <asp:DropDownList ID="ddlParentPageName" runat="server" ValidationGroup="Save" AppendDataBoundItems="True"
                                                    Width="30%" class="chosen-select" data-placeholder="Choose a Name">
                                                    <%--<asp:ListItem Value="0" Text="Main" Selected="True"></asp:ListItem>--%>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="tfv_ddlparent" runat="server" ErrorMessage="Required!"
                                                    ValidationGroup="Save" ControlToValidate="ddlParentPageName" ValidateEmptyText="false"
                                                    SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title">
                                                <asp:Label ID="lblPageUrl" runat="server" Text="Page Url"></asp:Label>
                                                <span class="label_intro">&nbsp;</span>
                                            </td>
                                            <td class="field_input" width="250px">
                                                <asp:FileUpload ID="PageUrlUpload" runat="server" />
                                                <asp:HiddenField ID="txtPageUrl" runat="server" />
                                                <span class="label_intro">&nbsp;</span>
                                            </td>
                                            <td class="field_input" width="20px">
                                                <asp:TextBox ID="txtmark" runat="server" width="10px" Text="?" Enabled="false"></asp:TextBox>
                                                <span class="label_intro">&nbsp;</span>
                                            </td>
                                            <td class="field_input">
                                                <asp:TextBox ID="txtquerystring" runat="server" CssClass="large"></asp:TextBox>
                                                <span class="label_intro">
                                                    <asp:Label ID="Label3" Text="Query string" runat="server"></asp:Label></span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_input">&nbsp;</td>
                                            <td class="field_input" colspan="3">
                                                <asp:Label ID="Label1" runat="server" Visible="False"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="15%" class="field_title">
                                                <asp:Label ID="lblddltype" runat="server" Text="Type"></asp:Label>
                                            </td>
                                            <td class="field_input" colspan="3">
                                                <asp:DropDownList ID="ddltype" runat="server" AppendDataBoundItems="True" Width="150"
                                                    class="chosen-select" data-placeholder="Choose a Brand" ValidationGroup="Save">
                                                    <asp:ListItem Value="M" Text="Master" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="A" Text="Account"></asp:ListItem>
                                                    <asp:ListItem Value="C" Text="Claim"></asp:ListItem>
                                                    <asp:ListItem Value="T" Text="Transaction"></asp:ListItem>
                                                    <asp:ListItem Value="R" Text="Report"></asp:ListItem>
                                                    <asp:ListItem Value="O" Text="Others"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfv_ddltye" runat="server" ErrorMessage="Required!"
                                                    ControlToValidate="ddltype" ValidateEmptyText="false" SetFocusOnError="true"
                                                    ValidationGroup="Save"> </asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="15%" class="field_title">
                                                <asp:Label ID="Label2" runat="server" Text="Reason Required"></asp:Label>
                                            </td>
                                            <td class="field_input" colspan="3">
                                                <asp:DropDownList ID="ddlreason" runat="server" ValidationGroup="Save" AppendDataBoundItems="True"
                                                    Width="150" class="chosen-select" data-placeholder="Choose a Brand">
                                                    <asp:ListItem Value="0" Text="-Select-" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="No"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Required!"
                                                    ValidationGroup="Save" ControlToValidate="ddlreason" ValidateEmptyText="false"
                                                    SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title">
                                                <asp:Label ID="lblactive" Text="Active" runat="server"></asp:Label>
                                            </td>
                                            <td class="field_input" colspan="3">
                                                <asp:CheckBox ID="chkActive" runat="server" Text=" " />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td style="padding: 8px 0;" id="refresh" colspan="3">
                                                <div class="btn_24_blue">
                                                    <span class="icon disk_co"></span>
                                                    <asp:Button ID="btnMenuSubmit" runat="server" Text="Submit" CssClass="btn_link" ValidationGroup="Save"
                                                        OnClick="btnMenuSubmit_Click" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
                                                    <span class="icon cross_octagon_co"></span>
                                                    <asp:Button ID="btnMenuRefresh" runat="server" Text="Cancel" CssClass="btn_link"
                                                        OnClick="btnMenuRefresh_Click" CausesValidation="false" />
                                                </div>
                                                <asp:HiddenField ID="Hdn_Fld" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td class="field_input" colspan="3">
                                                <asp:Label ID="lblMsg" runat="server"></asp:Label>
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
                                        <li style="font-size: medium; padding-left: 40px; float: right;">
                                            <asp:LinkButton ID="lnkmenurights" runat="server" OnClick="lnkmenurights_Click" ForeColor="Red"
                                                Font-Underline="true">Back To Menu Rights</asp:LinkButton>
                                        </li>
                                    </ul>
                                    <div class="gridcontent">
                                        <cc1:Grid ID="gvMenu" runat="server" CallbackMode="false" Serialize="true" FolderStyle="../GridStyles/premiere_blue"
                                            AutoGenerateColumns="false" AllowPageSizeSelection="true" OnDeleteCommand="DeleteRecord"
                                            AllowAddingRecords="false" AllowFiltering="true" PageSize="500" EnableRecordHover="true">
                                            <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnClientDelete="OnDelete" />
                                            <FilteringSettings InitialState="Visible" />
                                            <Columns>
                                                <cc1:Column ID="Column20" DataField="Sl" HeaderText="SL" runat="server" Width="60">
                                                    <TemplateSettings TemplateId="slnoTemplateIN" />
                                                </cc1:Column>
                                                <cc1:Column ID="ID" DataField="ID" ReadOnly="true" HeaderText="MENUID" runat="server"
                                                    Width="70" Align="right" />
                                                <cc1:Column ID="PARENTID" DataField="ParentID" ReadOnly="true" HeaderText="PARENTID"
                                                    runat="server" Visible="false" />
                                                <cc1:Column ID="CHILDID" DataField="ChildID" ReadOnly="true" HeaderText="CHILDID"
                                                    runat="server" Width="80" Align="right" />
                                                <cc1:Column DataField="ParentPageName" HeaderText=" PARENT PAGE NAME" runat="server"
                                                    Width="200" Wrap="true">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column DataField="ChildPageName" HeaderText=" CHILD PAGE NAME" runat="server"
                                                    Width="220" Wrap="true">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column DataField="PageURL" HeaderText=" PAGE URL" runat="server" Width="100%"
                                                    Wrap="true">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column AllowEdit="true" AllowDelete="true" HeaderText="EDIT" runat="server"
                                                    Width="80" Align="center">
                                                    <TemplateSettings TemplateId="editBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column HeaderText="DELETE" AllowEdit="false" AllowDelete="true" Width="80" Align="center">
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
                                                        <a href="javascript: //" class="action-icons c-delete" title="Delete" onclick="gvMenu.delete_record(this)">
                                                        </a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="slnoTemplateIN">
                                                    <Template>
                                                        <nav style="position: relative;"><span class="badge black"><asp:Label ID="lblslno" runat="server" Text='<%#Container.PageRecordIndex+1 %>'></asp:Label></span></nav>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <ScrollingSettings ScrollWidth="100%" ScrollHeight="450" />
                                        </cc1:Grid>
                                        <asp:Button ID="btngridsave" runat="server" Text="GridSave" Style="display: none"
                                            CausesValidation="false" OnClick="btngridsave_Click" />
                                    </div>
                                </asp:Panel>
                                <asp:HiddenField ID="hdnID" runat="server" />
                            </div>
                        </div>
                    </div>
                    <span class="clear"></span>
                    <cc1:MessageBox ID="MessageBox1" runat="server" />
                </div>
                <span class="clear"></span>
                <script type="text/javascript">
                    function CallServerMethod(oLink) {
                        var iRecordIndex = oLink.id.toString().replace("btnGridEdit_", "");
                        document.getElementById("<%=Hdn_Fld.ClientID %>").value = gvMenu.Rows[iRecordIndex].Cells[1].Value;
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
                <script type="text/javascript">
                    function SetFileName(PageUrlUpload, txtPageUrl) {
                        var arrFileName = document.getElementById(PageUrlUpload).value.split('\\');
                        document.getElementById(txtPageUrl).value = arrFileName[arrFileName.length - 1];
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

                        gvMenu.addFilterCriteria('ParentPageName', OboutGridFilterCriteria.Contains, searchValue);
                        gvMenu.addFilterCriteria('ChildPageName', OboutGridFilterCriteria.Contains, searchValue);
                        gvMenu.addFilterCriteria('PageURL', OboutGridFilterCriteria.Contains, searchValue);


                        gvMenu.executeFilter();

                        searchTimeout = null;

                        return false;
                    }
                </script>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
