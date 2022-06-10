<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmProductionOrderClosingMM.aspx.cs" Inherits="VIEW_frmProductionOrderClosingMM" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        function CheckAllheader(Checkbox) {
            var GridVwHeaderCheckbox = document.getElementById("<%=gvProductionOrderClose.ClientID %>");
            for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
                GridVwHeaderCheckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
    </script>
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
            <asp:Image ID="Image1" ImageUrl="~/images/loading123.gif" AlternateText="Processing"
                runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
        PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>PRODUCTION ORDER CLOSING</h6>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlADD" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_title">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="90">
                                                            <asp:Label ID="Label16" runat="server" Text="From Date"></asp:Label>
                                                        </td>
                                                        <td width="120">
                                                            <asp:TextBox ID="txtFromDate" runat="server" MaxLength="10" Width="80" Enabled="false"
                                                                placeholder="dd/MM/yyyy" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="Search"
                                                                Font-Bold="true"></asp:TextBox>
                                                            <asp:ImageButton ID="imgPopup" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarFromDate" PopupButtonID="imgPopup" runat="server"
                                                                TargetControlID="txtFromDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                        <td width="70">
                                                            <asp:Label ID="Label19" runat="server" Text="To Date"></asp:Label>
                                                        </td>
                                                        <td width="120">
                                                            <asp:TextBox ID="txtToDate" runat="server" MaxLength="10" Width="80" Enabled="false"
                                                                placeholder="dd/mm/yyyy" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="Search"
                                                                Font-Bold="true"></asp:TextBox>
                                                            <asp:ImageButton ID="imgPopuptodate" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtenderToDate" PopupButtonID="imgPopuptodate"
                                                                runat="server" TargetControlID="txtToDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label7" Text="BULK PRODUCT" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlBulkProduct" Width="220" runat="server" class="chosen-select" AppendDataBoundItems="True">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlBulkProduct"
                                                                SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="Search" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            <div class="btn_24_blue">
                                                                <span class="icon find_co"></span>
                                                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn_link" OnClick="btnSearch_Click" />
                                                            </div>
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="7" class="field_input" style="padding-left: 10px;">
                                                            <fieldset>
                                                                <legend>PRODUCTION ORDER</legend>
                                                                <div style="margin: 0 auto; width: 100%;">
                                                                    <div style="overflow: hidden;" id="DivHeaderRow">
                                                                    </div>
                                                                    <div id="DivMainContent" style="overflow: scroll; height: 400px;" onscroll="OnScrollDiv(this);">
                                                                        <asp:GridView ID="gvProductionOrderClose" runat="server" Width="100%" CssClass="zebra"
                                                                            AutoGenerateColumns="false" EmptyDataText="No Records Available">
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <HeaderTemplate>
                                                                                        <asp:CheckBox ID="chkAllSelect" runat="server" onclick="CheckAllheader(this);" Text=" " />
                                                                                    </HeaderTemplate>
                                                                                    <HeaderStyle Width="10px" />
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chkSelect" runat="server" Text=" " />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="PRODUCTION_ORDERID" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblPRODUCTION_ORDERID" runat="server" Text='<%# Bind("PRODUCTION_ORDERID") %>'
                                                                                            value='<%# Eval("PRODUCTION_ORDERID") %>' Visible="false"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="PRODUCTION NO" ItemStyle-Width="150px">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblPRODUCTIONNO" runat="server" Text='<%# Bind("PRODUCTIONNO") %>'
                                                                                            value='<%# Eval("PRODUCTIONNO") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="ENTRY DATE" ItemStyle-Width="100px">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblENTRYDATE" runat="server" Text='<%# Bind("ENTRY_DATE") %>' value='<%# Eval("ENTRY_DATE") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="BULK NAME" ItemStyle-Width="250px">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblBULKNAME" runat="server" Text='<%# Bind("PRODUCTALIAS") %>' value='<%# Eval("PRODUCTALIAS") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField HeaderText="BATCH NO" DataField="BatchNO" ItemStyle-Width="100px" />
                                                                                <asp:TemplateField HeaderText="BULK QTY" ItemStyle-Width="100px">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblBulkqty" runat="server" Text='<%# Bind("BULKQTY") %>' value='<%# Eval("BULKQTY") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="STORE RETURN QTY" ItemStyle-Width="100px">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblStoreReturnqty" runat="server" Text='<%# Bind("STORERETURN") %>' value='<%# Eval("STORERETURN") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="USE QTY" ItemStyle-Width="80px">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblUseqty" runat="server" Text='<%# Bind("USEQTY") %>' value='<%# Eval("USEQTY") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="BALANCE QTY" ItemStyle-Width="80px">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblBalanceqty" runat="server" Text='<%# Bind("BALANCEQTY") %>' value='<%# Eval("BALANCEQTY") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="CLOSEING DATE " ItemStyle-Wrap="true" ItemStyle-Width="140px">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="grdtxtClosingdate" AutoPostBack="false" runat="server" MaxLength="20"
                                                                                            Text='<%# Bind("CLOSEDDATE") %>' Style="text-align: right;" Width="60px" Height="20"
                                                                                            Enabled="false"></asp:TextBox>

                                                                                        <asp:ImageButton ID="imgPopupbankdate" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                                            runat="server" Height="24" />
                                                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtenderimgPopupbankdate" PopupButtonID="imgPopupbankdate"
                                                                                            runat="server" TargetControlID="grdtxtClosingdate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                                        </ajaxToolkit:CalendarExtender>

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
                                                        <td valign="middle" width="110">
                                                            <div class="btn_24_blue">
                                                                <span class="icon exclamation_co"></span>
                                                                <asp:Button ID="btnsave" runat="server" Text="Save" CssClass="btn_link" OnClick="btnsave_Click" />
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
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