<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true"
    CodeFile="frmUploadBill.aspx.cs" Inherits="VIEW_frmUploadBill" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"
    TagPrefix="cc1" %>
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
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
             <asp:PostBackTrigger ControlID="btngeneraltemp" />
        </Triggers>
         
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
                                <h6>
                                    Upload Bill  Details</h6>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                    <tr>
                                    <td class="field_title" width="100">
                                      <asp:Label ID="Label122" Text="Depot" runat="server"></asp:Label><span class="req">*</span>
                                    </td>
                                     <td class="field_input" width="150">
                                                <asp:DropDownList ID="ddldepot" runat="server" AppendDataBoundItems="true"
                                                    Width="150" class="chosen-select" ValidationGroup="ADD" data-placeholder="-- Store Loacation --">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="* Required!"
                                                    ValidationGroup="ADD" Font-Bold="true" ForeColor="Red" ControlToValidate="ddldepot"
                                                    SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
                                            </td>

                                        <td class="field_title" width="100">
                                      <asp:Label ID="Label1" Text="Voucher Type" runat="server"></asp:Label><span class="req">*</span>
                                    </td>

                                    <td class="field_input" width="150">
                                                <asp:DropDownList ID="ddlvoucher" runat="server" AppendDataBoundItems="true"
                                                    Width="150" class="chosen-select" ValidationGroup="ADD" data-placeholder="-- Store Loacation --">
                                                     <asp:ListItem Text="Payment" Value="9"></asp:ListItem>
                                                     <asp:ListItem Text="Receipt" Value="10"></asp:ListItem>
                                                </asp:DropDownList>
                                                
                                            </td>
                                       <td class="field_input"  colspan="4">
                                                 <div class="btn_24_blue">
                                                <span class="icon doc_excel_table_co"></span>
                                               <asp:Button ID="btngeneraltemp" runat="server"  Text="Generate Template" CssClass="btn_link" 
                                               OnClick="btngeneraltemp_Click"/>
                                            </div>      
                                            </td>

                                    </tr>
                                        <tr>
                                           
                                        </tr>
                                        <tr>
                                            <td class="field_title">
                                                <asp:Label ID="Label2" Text="Upload File" runat="server"></asp:Label><span class="req">*</span>
                                            </td>
                                            <td class="field_input" >
                                                <asp:FileUpload ID="FileUpload1" runat="server" />
                                            </td>
                                           
                                             <td class="field_input"  style="padding-left:21px" colspan="3">
                                                <div class="btn_24_blue">
                                                    <span class="icon disk_co"></span>
                                                    <asp:Button ID="btnUpload" runat="server" Text="SAVE" CssClass="btn_link" ValidationGroup="ADD"
                                                        OnClick="btnUpload_Click" />
                                                </div>
                                            <%--</td>
                                             <td class="field_input"  style="padding-left:21px">--%>
                                                <div class="btn_24_blue">
                                                    <span class="icon disk_co"></span>
                                                    <asp:Button ID="btnrest" runat="server" Text="RESET" CssClass="btn_link"  CausesValidation="false"
                                                        OnClick="btnrest_Click" />
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                           
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                    <cc1:MessageBox ID="MessageBox1" runat="server" />
                    <span class="clear"></span>
                </div>
                <span class="clear"></span>
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
