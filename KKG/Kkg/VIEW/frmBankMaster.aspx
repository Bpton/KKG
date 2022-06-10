<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmBankMaster.aspx.cs" Inherits="VIEW_frmBankMaster" %>

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
                    Bank Master</h3>
            </div>--%>
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>Bank Details</h6>
                                <div class="btn_30_light" style="float: right;"  id="btnaddhide" runat="server">
                                    <span class="icon add_co"></span>
                                    <asp:Button ID="btnAddBank" runat="server" Text="Add New Record" CssClass="btn_link"
                                        OnClick="btnAddBank_Click" CausesValidation="false" /></div>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td width="120" class="field_title">
                                                <asp:Label ID="lblBankName" Text="Name" runat="server"></asp:Label><span class="req">*</span>
                                            </td>
                                            <td class="field_input" colspan="5">
                                                <asp:TextBox ID="txtBankName" runat="server" CssClass="mid"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="CV_BankName" runat="server" ErrorMessage="Bank Name is required!"
                                                            ControlToValidate="txtBankName" ValidateEmptyText="false" Display="None" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                            TargetControlID="CV_BankName" PopupPosition="Right" HighlightCssClass="errormassage"
                                                            WarningIconImageUrl="../images/050.png">
                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>

                                        </tr>
                                        <tr>
                                            
                                            <td width="120" class="field_title">
                                                <asp:Label ID="Label10" Text="Accounts Group" runat="server"></asp:Label><span class="req">*</span>
                                            </td>
                                            <td class="field_input">
                                                <asp:DropDownList ID="ddlgroup" runat="server" Width="180" AppendDataBoundItems="True" ValidationGroup="ADD"
                                                    class="chosen-select" data-placeholder="Choose" >
                                                </asp:DropDownList>
                                                 <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* Required!"
                                                    ValidationGroup="ADD" Font-Bold="true" ForeColor="Red" ControlToValidate="ddlgroup"
                                                    SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>--%>
                                                 <asp:RequiredFieldValidator ID="ref_ddlgroup" runat="server" ErrorMessage="Accounts Group is required!"
                                                            ControlToValidate="ddlgroup" ValidateEmptyText="false" Display="None" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server"
                                                            TargetControlID="ref_ddlgroup" PopupPosition="Right" HighlightCssClass="errormassage"
                                                            WarningIconImageUrl="../images/050.png">
                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                            </tr>
                                        <tr>
                                            <td width="120" class="field_title">
                                                <asp:Label ID="Label2" Text="Address" runat="server"></asp:Label><span class="req">*</span>
                                            </td>
                                            <td class="field_input"  colspan="5">
                                                <asp:TextBox ID="txtaddress" runat="server" CssClass="mid" TextMode="MultiLine" Width="300"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfv_txtaddress" runat="server" ErrorMessage="Address is required!"
                                                            ControlToValidate="txtaddress" ValidateEmptyText="false" Display="None" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                                            TargetControlID="rfv_txtaddress" PopupPosition="Right" HighlightCssClass="errormassage"
                                                            WarningIconImageUrl="../images/050.png">
                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>

                                        </tr>
                                         <tr>
                                            <td width="120" class="field_title">
                                                <asp:Label ID="Label5" Text="branch" runat="server"></asp:Label><span class="req">*</span>
                                            </td>
                                            <td class="field_input"  colspan="5">
                                                <asp:TextBox ID="txtbranch" runat="server" CssClass="mid"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfv_txtbranch" runat="server" ErrorMessage="Branch is required!"
                                                            ControlToValidate="txtbranch" ValidateEmptyText="false" Display="None" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
                                                            TargetControlID="rfv_txtbranch" PopupPosition="Right" HighlightCssClass="errormassage"
                                                            WarningIconImageUrl="../images/050.png">
                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td width="120" class="field_title">
                                                <asp:Label ID="Label6" Text="A/c no" runat="server" onkeypress="return isNumberKey(event);"></asp:Label><span class="req">*</span>
                                            </td>
                                            <td class="field_input"  colspan="5">
                                                <asp:TextBox ID="txtacountno" runat="server" CssClass="mid"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfv_txtacountno" runat="server" ErrorMessage="A/C No. is required!"
                                                            ControlToValidate="txtacountno" ValidateEmptyText="false" Display="None" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server"
                                                            TargetControlID="rfv_txtacountno" PopupPosition="Right" HighlightCssClass="errormassage"
                                                            WarningIconImageUrl="../images/050.png">
                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td width="120" class="field_title">
                                                <asp:Label ID="Label3" Text="IFSC CODE" runat="server"></asp:Label><span class="req">*</span>
                                            </td>
                                            <td class="field_input"  colspan="5">
                                                <asp:TextBox ID="txtifsccode" runat="server" CssClass="mid"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfv_txtifsccode" runat="server" ErrorMessage="IFSC is required!"
                                                            ControlToValidate="txtifsccode" ValidateEmptyText="false" Display="None" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                                            TargetControlID="rfv_txtifsccode" PopupPosition="Right" HighlightCssClass="errormassage"
                                                            WarningIconImageUrl="../images/050.png">
                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>

                                        </tr>
                                          <tr>
                                            <td width="120" class="field_title">
                                                <asp:Label ID="Label4" Text="SWIFT CODE" runat="server"></asp:Label><span class="req">*</span>
                                            </td>
                                            <td class="field_input" colspan="5">
                                                <asp:TextBox ID="txtswiftcode" runat="server" CssClass="mid"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfv_txtswiftcode" runat="server" ErrorMessage="Swift Code is required!"
                                                            ControlToValidate="txtswiftcode" ValidateEmptyText="false" Display="None" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
                                                            TargetControlID="rfv_txtswiftcode" PopupPosition="Right" HighlightCssClass="errormassage"
                                                            WarningIconImageUrl="../images/050.png">
                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td width="120" class="field_title">
                                                <asp:Label ID="Label9" Text="BANK CODE" runat="server"></asp:Label><span class="req"></span>
                                            </td>
                                            <td class="field_input" colspan="5">
                                                <asp:TextBox ID="txtbankcode" runat="server" CssClass="mid"></asp:TextBox>
                                                        <%--<asp:RequiredFieldValidator ID="ReqBankCode" runat="server" ErrorMessage="Bank Code is Required!"
                                                            ControlToValidate="txtbankcode" ValidateEmptyText="false" Display="None" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server"
                                                            TargetControlID="ReqBankCode" PopupPosition="Right" HighlightCssClass="errormassage"
                                                            WarningIconImageUrl="../images/050.png">
                                                        </ajaxToolkit:ValidatorCalloutExtender>--%>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td class="field_title">
                                                <asp:Label ID="Label1" Text="Active" runat="server"></asp:Label>
                                            </td>
                                            <td class="field_input" width="80px" >
                                                <asp:CheckBox ID="chkActive" runat="server" Text=" " />
                                            </td>
                                            <td class="field_title" width="80px">
                                                <asp:Label ID="Label7" Text="export" runat="server"></asp:Label>
                                            </td>
                                            <td class="field_input" width="80px">
                                                <asp:CheckBox ID="chkexport" runat="server" Text=" " />
                                            </td>
                                           <td class="field_title" width="80px">
                                                <asp:Label ID="Label8" Text="IS FOR TARGET" runat="server"></asp:Label>
                                            </td>
                                            <td class="field_input">
                                                <asp:CheckBox ID="chktarget" runat="server" Text=" " />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td style="padding:8px 0;" colspan="5">
                                            <div class="btn_24_blue">
                                              <span class="icon disk_co"></span>
                                                <asp:Button ID="btnBankSubmit" runat="server" Text="Save" CssClass="btn_link" OnClick="btnBankSubmit_Click" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
                                                    <span class="icon cross_octagon_co"></span>
                                                    <asp:Button ID="btnBankCancel" runat="server" Text="Cancel" CssClass="btn_link" OnClick="btnBankCancel_Click" CausesValidation="false" />
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
                                    <cc1:Grid ID="gvBank" runat="server" CallbackMode="true" Serialize="true" FolderStyle="../GridStyles/premiere_blue"
                                        AutoGenerateColumns="false" OnDeleteCommand="DeleteRecord" AllowPageSizeSelection="false"
                                        AllowAddingRecords="false" AllowFiltering="true" 
                                            onrowdatabound="gvBank_RowDataBound">
                                        <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnClientDelete="OnDelete" />
                                        <FilteringSettings InitialState="Visible" />
                                        <Columns>
                                            <cc1:Column DataField="ID" ReadOnly="true" HeaderText="BankID" runat="server" Visible="false" />
                                            <cc1:Column DataField="BankNAME" HeaderText=" Name" runat="server" Width="300">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="EqualTo" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="DoesNotContain" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                </FilterOptions>
                                            </cc1:Column>
                                             <cc1:Column ID="Column1" DataField="ADDRESS" HeaderText=" ADDRESS" runat="server" Width="300" Wrap="true">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="EqualTo" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="DoesNotContain" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                </FilterOptions>
                                            </cc1:Column>
                                             <cc1:Column ID="Column2" DataField="IFSCCODE" HeaderText=" IFSC CODE" runat="server" Width="120">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="EqualTo" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="DoesNotContain" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                </FilterOptions>
                                            </cc1:Column>
                                             <cc1:Column ID="Column3" DataField="SWIFTCODE" HeaderText=" SWIFT CODE" runat="server" Width="120">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="EqualTo" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="DoesNotContain" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                </FilterOptions>
                                            </cc1:Column>
                                            <cc1:Column DataField="ACTIVE" HeaderText="ACTIVE" runat="server" Width="200">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="Contains" />
                                                </FilterOptions>
                                            </cc1:Column>
                                            <cc1:Column AllowEdit="true" AllowDelete="true" HeaderText="Edit" runat="server"
                                                Width="80">
                                                <TemplateSettings TemplateId="editBtnTemplate" />
                                            </cc1:Column>
                                            <cc1:Column HeaderText="Delete" AllowEdit="false" AllowDelete="true" Width="80">
                                                <TemplateSettings TemplateId="deleteBtnTemplate" />
                                            </cc1:Column>
                                        </Columns>
                                        <FilteringSettings MatchingType="AnyFilter" />
                                         <Templates>
                                                    <cc1:GridTemplate runat="server" ID="editBtnTemplate" >
                                                    <Template>
                                                   <a href="javascript: //" class="action-icons c-edit" id="btnGridEdit_<%# Container.PageRecordIndex %>"
                                                            onclick="CallServerMethod(this);"></a>
                                                     
                                                   
                                                                                                
                                                    </Template>
                                                    </cc1:GridTemplate>
                                        </Templates>
                                         <Templates>
                                                    <cc1:GridTemplate runat="server" ID="deleteBtnTemplate" >
                                                    <Template>
                                                    <a href="javascript: //" class="action-icons c-delete" title="Delete" onclick="gvBank.delete_record(this)"  >
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
                <script type="text/javascript">
                    function OnBeforeDelete(record) {
                        record.Error = '';
                        document.getElementById("<%=Hdn_Fld.ClientID %>").value = record.ID ;
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
                        gvBank.addFilterCriteria('BankNAME', OboutGridFilterCriteria.Contains, searchValue);
                        gvBank.addFilterCriteria('ACTIVE', OboutGridFilterCriteria.Contains, searchValue);
                        gvBank.executeFilter();
                        searchTimeout = null;
                        return false;
                    }
                </script>

                 <script type="text/javascript">
                     function CallServerMethod(oLink) {
                         var iRecordIndex = oLink.id.toString().replace("btnGridEdit_", "");
                         document.getElementById("<%=Hdn_Fld.ClientID %>").value = gvBank.Rows[iRecordIndex].Cells[0].Value;
                         document.getElementById("<%=btngridedit.ClientID %>").click();
                     }
                     function isNumberKey(evt) {
                         var charCode = (evt.which) ? evt.which : event.keyCode;
                         if ((charCode < 48 || charCode > 57) && (charCode != 8))
                             return false;

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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>