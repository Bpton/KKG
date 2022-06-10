<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true"
    CodeFile="frmTpuPOClosing.aspx.cs" Inherits="VIEW_frmTpuPOClosing" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        function CheckAllheader(Checkbox) {
            var GridVwHeaderCheckbox = document.getElementById("<%=gvPurchaseOrderClose.ClientID %>");
            for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
                GridVwHeaderCheckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
    </script>

    <script type="text/javascript">
        function myFunction() {
            var ddl = document.getElementById("<%=ddlstatus.ClientID%>");
            var SelVal = ddl.options[ddl.selectedIndex].value;

            if (SelVal == 'N') {
                return confirm('Are you sure you want to Close above PO(s)?');
            }
            else {
                return confirm('Are you sure you want to Open above PO(s)?');
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
            <asp:Image ID="Image1" ImageUrl="../images/loading123.gif" AlternateText="Processing"
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
                                <h6>TPU PURCHASE ORDER CLOSING </h6>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlADD" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_title">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label16" runat="server" Text="From Date"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtFromDate" runat="server" MaxLength="10" Width="80" Enabled="false"
                                                                placeholder="dd/MM/yyyy" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="Search"
                                                                Font-Bold="true"></asp:TextBox>
                                                            <asp:ImageButton ID="imgPopup" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarFromDate" PopupButtonID="imgPopup" runat="server"
                                                                TargetControlID="txtFromDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorFromDate" runat="server" ControlToValidate="txtFromDate"
                                                                SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="Search" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label19" runat="server" Text="To Date"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtToDate" runat="server" MaxLength="10" Width="80" Enabled="false"
                                                                placeholder="dd/mm/yyyy" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="Search"
                                                                Font-Bold="true"></asp:TextBox>
                                                            <asp:ImageButton ID="imgPopuptodate" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtenderToDate" PopupButtonID="imgPopuptodate"
                                                                runat="server" TargetControlID="txtToDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorToDate" runat="server" ControlToValidate="txtToDate"
                                                                SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="Search" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" Text="STATUS"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlstatus" runat="server" class="chosen-select" data-placeholder="Select"
                                                                Width="100px" ValidationGroup="Search">
                                                                <asp:ListItem Text="OPEN" Value="N"></asp:ListItem>
                                                                <asp:ListItem Text="CLOSED" Value="Y"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label2" runat="server" Text="TPU"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlTPUName" runat="server" Width="260px" Height="28px"
                                                                OnSelectedIndexChanged="ddlTPUName_SelectedIndexChanged" AutoPostBack="true"
                                                                class="chosen-select" data-placeholder="ALL">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <div class="btn_24_blue">
                                                                <span class="icon find_co"></span>
                                                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn_link" OnClick="btnSearch_Click" />
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="10" class="field_input" style="padding-left: 10px;">
                                                            <fieldset>
                                                                <legend>PURCHASE ORDER</legend>
                                                                <div style="margin: 0 auto; width: 100%;">
                                                                    <div style="overflow: hidden;" id="DivHeaderRow">
                                                                    </div>
                                                                    <div id="DivMainContent" style="overflow: scroll; height: 300px;" onscroll="OnScrollDiv(this);">
                                                                        <asp:GridView ID="gvPurchaseOrderClose" runat="server" Width="100%" CssClass="zebra"
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
                                                                                <%--<asp:TemplateField HeaderText="SL" ItemStyle-Wrap="false" ItemStyle-Width="10px">
                                                                                    <ItemTemplate>
                                                                                        <nav style="position: relative;"><span class="badge black"><%# Container.DataItemIndex + 1 %></span></nav>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>--%>
                                                                                <asp:TemplateField HeaderText="POID" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblPOID" runat="server" Text='<%# Bind("POID") %>' value='<%# Eval("POID") %>'
                                                                                            Visible="false"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="PO NO" ItemStyle-Width="300px">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblPONO" runat="server" Text='<%# Bind("PONO") %>' value='<%# Eval("PONO") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="PO DATE" ItemStyle-Width="60px">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblPODATE" runat="server" Text='<%# Bind("PODT") %>' value='<%# Eval("PODT") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="ORDER QTY(PCs)" ItemStyle-Width="40px">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblORDERQTY" runat="server" Text='<%# Bind("ORDERQTY") %>' value='<%# Eval("ORDERQTY") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <%--<asp:TemplateField HeaderText="DISPATCHED QTY(PCs)" ItemStyle-Width="60px">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblINVOICEQTY" runat="server" Text='<%# Bind("INVOICEQTY") %>' value='<%# Eval("INVOICEQTY") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>--%>

                                                                               <%-- <asp:TemplateField HeaderText="REMAINING QTY(PCs)" ItemStyle-Width="60px">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblREMAININGQTY" runat="server" Text='<%# Bind("REMAININGQTY") %>' value='<%# Eval("REMAININGQTY") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>--%>

                                                                                <%--<asp:TemplateField HeaderText="PRODUCTIVITY (%)" ItemStyle-Width="60px">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblPRODUCTIVITY" runat="server" Text='<%# Bind("PRODUCTIVITY") %>' value='<%# Eval("PRODUCTIVITY") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>--%>

                                                                                <asp:BoundField HeaderText="VENDOR NAME" DataField="VENDORNAME" ItemStyle-Width="250px" />
                                                                                <asp:TemplateField HeaderText="CLOSING REASON" ItemStyle-Width="160px">
                                                                                    <ItemTemplate>
                                                                                        <asp:DropDownList ID="ddlClosingReason" runat="server" class="chosen-select"
                                                                                            AppendDataBoundItems="True" Width="160px" ValidationGroup="Save">
                                                                                            <asp:ListItem Text="Cancel" Value="0"></asp:ListItem>
                                                                                            <asp:ListItem Text="Change in Plan" Value="1"></asp:ListItem>
                                                                                            <asp:ListItem Text="Vendor not delivered" Value="2"></asp:ListItem>
                                                                                            <asp:ListItem Text="Others" Value="3"></asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="CLOSING REMARKS" ItemStyle-Wrap="true" ItemStyle-Width="80px">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="grdtxtRemarks" runat="server" MaxLength="50" Width="160px">
                                                                                        </asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="CLOSEING DATE" ItemStyle-Wrap="true" ItemStyle-Width="180px">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="grdtxtClosingdate" AutoPostBack="false" runat="server" MaxLength="10"
                                                                                            Text='<%# Bind("CLOSEDDATE") %>' Style="text-align: right;" Width="60px" Height="20"
                                                                                            Enabled="false"></asp:TextBox>
                                                                                        <asp:ImageButton ID="imgPopupbankdate" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
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

                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div class="btn_24_blue">
                                                                <span class="icon exclamation_co"></span>
                                                                <asp:Button ID="btnsave" runat="server" Text="Save" CssClass="btn_link" OnClick="btnsave_Click"
                                                                    OnClientClick="return myFunction()" />
                                                                <%--OnClientClick="return confirm('Are you sure you want to close above PO(s)?')" />--%>
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
