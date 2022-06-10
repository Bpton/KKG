<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmFragranceMaster.aspx.cs" Inherits="VIEW_frmFragranceMaster" %>

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
                <h3>Fragrance</h3>
            </div>--%>
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>Colour Details</h6>
                               <div class="btn_30_light" style="float: right;" id="btnaddhide" runat="server">
                                    <span class="icon add_co"></span>
                                    <asp:Button ID="btnAddfrg" runat="server" Text="Add New Record" CssClass="btn_link"
                                        OnClick="btnAddfrg_Click" CausesValidation="false"/></div>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td width="120" class="field_title"><asp:Label ID="lblfrgCode" Text="Code" runat="server"></asp:Label><span class="req">*</span></td>
                                            <td class="field_input">
                                                <asp:TextBox ID="txtfrgCode" runat="server" CssClass="mid" MaxLength="03"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="CV_frgCode" runat="server" Display="None" ErrorMessage="Code is required!"
                                                            ControlToValidate="txtfrgCode" ValidateEmptyText="false" ClientValidationFunction="validateNOPName"
                                                            OnServerValidate="CV_frgCode_ServerValidate" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                                            TargetControlID="CV_frgCode" PopupPosition="Right" HighlightCssClass="errormassage"
                                                            WarningIconImageUrl="../images/050.png">
                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title"><asp:Label ID="lblfrgName" Text="Name" runat="server"></asp:Label><span class="req">*</span></td>
                                            <td class="field_input">
                                                <asp:TextBox ID="txtfrgName" runat="server" CssClass="mid" MaxLength="50"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="CV_frgName" runat="server" Display="None" ErrorMessage="Name is required!"
                                                            ControlToValidate="txtfrgName" ValidateEmptyText="false" ClientValidationFunction="validateNOPName"
                                                            OnServerValidate="CV_frgName_ServerValidate" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                            TargetControlID="CV_frgName" PopupPosition="Right" HighlightCssClass="errormassage"
                                                            WarningIconImageUrl="../images/050.png">
                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title"><asp:Label ID="lblfrgDescription" Text="Description" runat="server"></asp:Label><span class="req">*</span></td>
                                            <td class="field_input">
                                                <asp:TextBox ID="txtfrgDescription" runat="server" CssClass="mid" MaxLength="50"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="CV_frgDescription" runat="server" Display="None"
                                                            ErrorMessage="Description is required!" ControlToValidate="txtfrgDescription"
                                                            ValidateEmptyText="false" ClientValidationFunction="validatefrgDescription" OnServerValidate="CV_frgDescription_ServerValidate"
                                                            SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                                            TargetControlID="CV_frgDescription" PopupPosition="Right" HighlightCssClass="errormassage"
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
                                                <asp:Button ID="btnfrgSubmit" runat="server" Text="Save" CssClass="btn_link" OnClick="btnfrgSubmit_Click" />
                                            </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                            <div class="btn_24_blue">
                                                <span class="icon cross_octagon_co"></span>
                                                <asp:Button ID="btnfrgCancel" runat="server" Text="Cancel" CssClass="btn_link" OnClick="btnfrgCancel_Click" CausesValidation="false" />
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
                                        <cc1:Grid ID="gvfrg" runat="server" CallbackMode="true" Serialize="true" FolderStyle="../GridStyles/premiere_blue"
                                        AutoGenerateColumns="false" AllowPageSizeSelection="false" OnDeleteCommand="DeleteRecord"
                                        AllowAddingRecords="false" AllowFiltering="true" 
                                            onrowdatabound="gvfrg_RowDataBound">
                                        <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnClientDelete="OnDelete" />
                                        <FilteringSettings InitialState="Visible" />
                                        <Columns>
                                            <cc1:Column DataField="FRGID" ReadOnly="true" HeaderText="ID" runat="server" Visible="false" />
                                            <cc1:Column ID="Column1" DataField="FRGCODE" HeaderText="CODE" runat="server" Width="240">
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
                                            <cc1:Column DataField="FRGNAME" HeaderText=" NAME" runat="server" Width="240"  SortOrder="Asc">
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
                                            <cc1:Column DataField="FRGDESCRIPTION" HeaderText="DESCRIPTION" runat="server" Width="280">
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
                                            <cc1:Column DataField="ACTIVE" HeaderText="ACTIVE" runat="server"
                                                Width="140">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="Contains" />
                                                </FilterOptions>
                                            </cc1:Column>
                                            <cc1:Column DataField="CBU" HeaderText="CBU" runat="server" Width="140"
                                                Visible="false">
                                            </cc1:Column>
                                            <cc1:Column DataField="DTOC" HeaderText="DTOC" runat="server" Width="140" Visible="false">
                                            </cc1:Column>
                                            <cc1:Column AllowEdit="true" AllowDelete="true" HeaderText="Edit" runat="server"
                                                Width="80">
                                                <TemplateSettings TemplateId="editBtnTemplate" />
                                            </cc1:Column>
                                            <cc1:Column HeaderText="Delete" AllowEdit="false" AllowDelete="true" Width="80" Visible="false">
                                                <TemplateSettings TemplateId="deleteBtnTemplate" />
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
                                                    <a href="javascript: //" class="action-icons c-delete" title="Delete" onclick="gvfrg.delete_record(this)"  >
                                                    </a>                                             
                                                    </Template>
                                                    </cc1:GridTemplate>
                                        </Templates>
                                        <ScrollingSettings ScrollWidth="100%" ScrollHeight="290" />
                                    </cc1:Grid>
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
                        document.getElementById("<%=Hdn_Fld.ClientID %>").value = record.FRGID;
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

                        gvfrg.addFilterCriteria('FRGCODE', OboutGridFilterCriteria.Contains, searchValue);
                        gvfrg.addFilterCriteria('FRGNAME', OboutGridFilterCriteria.Contains, searchValue);
                        gvfrg.addFilterCriteria('FRGDESCRIPTION', OboutGridFilterCriteria.Contains, searchValue);

                        gvfrg.executeFilter();

                        searchTimeout = null;

                        return false;
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
                        document.getElementById("<%=txtfrgCode.ClientID %>").value = '';
                        document.getElementById("<%=txtfrgName.ClientID %>").value = '';
                        document.getElementById("<%=txtfrgDescription.ClientID %>").value = '';
                        document.getElementById("<%=chkActive.ClientID %>").checked = '';
                        document.getElementById("<%=Hdn_Fld.ClientID %>").value = '';

                    }

                    function populateEditControls(iRecordIndex) {

                        document.getElementById("<%=txtfrgCode.ClientID%>").value = gvfrg.Rows[iRecordIndex].Cells[1].Value;
                        document.getElementById("<%=txtfrgName.ClientID%>").value = gvfrg.Rows[iRecordIndex].Cells[2].Value;
                        document.getElementById("<%=txtfrgDescription.ClientID%>").value = gvfrg.Rows[iRecordIndex].Cells[3].Value;
                        var activevalue = gvfrg.Rows[iRecordIndex].Cells[4].Value;

                        if (activevalue == "Active") {
                            document.getElementById("<%=chkActive.ClientID%>").checked = true;
                        }
                        else {
                            document.getElementById("<%=chkActive.ClientID%>").checked = false;
                        }

                        document.getElementById("<%=Hdn_Fld.ClientID %>").value = gvfrg.Rows[iRecordIndex].Cells[0].Value;

                        document.getElementById("<%=txtfrgCode.ClientID %>").disabled = true;
                        document.getElementById("<%=txtfrgName.ClientID %>").disabled = true;
                        document.getElementById("<%=txtfrgDescription.ClientID %>").disabled = true;
                        document.getElementById("<%=chkActive.ClientID%>").disabled = true;
                        document.getElementById("<%=btnfrgSubmit.ClientID%>").disabled = true;
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