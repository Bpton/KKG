<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmProductionWestage.aspx.cs" Inherits="VIEW_frmProductionWestage" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        function CheckAllheader(Checkbox) {
            var GridVwHeaderCheckbox = document.getElementById("<%=gvProductionWastage.ClientID %>");
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

    <script type="text/javascript" language="javascript">
        function calculate(a) {
            var IssueQtyid = 0;
            var IssueQty = 0;
            var ConsumblesQtyID = 0;
            var ConsumblesQty = 0;
            var BeforeWastage = 0;
            var ReturnQtyID = 0;
            var ReturnQty = 0;
            var WastageID = 0;
            var WastageQty = 0;
            var rowData = a.parentNode.parentNode;
            var rowIndex = rowData.rowIndex - 1;
            var grd = document.getElementById('<%= gvProductionWastage.ClientID %>');
            var grid = document.getElementById('ContentPlaceHolder1_gvProductionWastage');

            IssueQtyid = "ContentPlaceHolder1_gvProductionWastage_lblISSUEQTY_" + rowIndex;
            IssueQty = document.getElementById(IssueQtyid).value;

            //ConsumblesQtyID = "ContentPlaceHolder1_gvProductionWastage_txtCONSUMABLESQTY_" + rowIndex;
            //ConsumblesQty = document.getElementById(ConsumblesQtyID).value;

            WastageID = "ContentPlaceHolder1_gvProductionWastage_txtWastage_" + rowIndex;
            WastageQty = document.getElementById(WastageID).value;

            ReturnQtyID = "ContentPlaceHolder1_gvProductionWastage_lblReturnQty_" + rowIndex;
            //ReturnQty= document.getElementById(ReturnQtyID).value;

            BeforeWastage = parseInt(IssueQty) - parseInt(WastageQty);
            //alert(IssueQty);
            //alert(WastageQty);
            //alert(BeforeWastage);

            if (parseInt(WastageQty) > parseInt(IssueQty)) {
                alert("Wastage Qty Can Not be greater Than Production Qty.");
                document.getElementById(WastageID).value = 0;
                document.getElementById(ReturnQtyID).value = IssueQty;
            }
            else {
                ReturnQty = parseInt(BeforeWastage); //- WastageQty;
                document.getElementById(ReturnQtyID).value = ReturnQty.toFixed(3);
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
                                <h6>PRODUCTION ORDER WASTAGE</h6>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlADD" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td>
                                                <table width="95%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td class="field_title">
                                                            <asp:Label ID="lblfromdt" runat="server" Text="From Date"></asp:Label>
                                                        </td>
                                                        <td width="165">
                                                            <asp:TextBox ID="txtFromDate" runat="server" MaxLength="10" Width="90" Enabled="false"
                                                                placeholder="dd/MM/yyyy" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="Search"
                                                                Font-Bold="true"></asp:TextBox>
                                                            <asp:ImageButton ID="imgPopup" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarFromDate" PopupButtonID="imgPopup" runat="server"
                                                                TargetControlID="txtFromDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                        <td class="field_title">
                                                            <asp:Label ID="lbltodt" runat="server" Text="To Date"></asp:Label>
                                                        </td>
                                                        <td width="165">
                                                            <asp:TextBox ID="txtToDate" runat="server" MaxLength="10" Width="90" Enabled="false"
                                                                placeholder="dd/mm/yyyy" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="Search"
                                                                Font-Bold="true"></asp:TextBox>
                                                            <asp:ImageButton ID="imgPopuptodate" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtenderToDate" PopupButtonID="imgPopuptodate"
                                                                runat="server" TargetControlID="txtToDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>

                                                        <td class="field_title">
                                                            <asp:Label ID="lblbulkproduct" Text="BULK PRODUCT" runat="server"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td class="field_input">
                                                            <asp:DropDownList ID="ddlBulkProduct" Width="250" runat="server" class="chosen-select" AppendDataBoundItems="True"
                                                                OnSelectedIndexChanged="ddlBulkProduct_SelectedIndexChanged" AutoPostBack="true">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="ReqddlBulkProduct" runat="server" ControlToValidate="ddlBulkProduct"
                                                                SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="Search" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="field_title">
                                                            <asp:Label ID="lblproductionno" Text="PRODUCTION NO" runat="server"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td class="field_input">
                                                            <asp:DropDownList ID="ddlproductionno" Width="180" runat="server" class="chosen-select" 
                                                            OnSelectedIndexChanged="ddlproductionno_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="True">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="Reqddlproductionno" runat="server" ControlToValidate="ddlproductionno"
                                                                SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="Search" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </td>

                                                        <td class="field_title">
                                                            <asp:Label ID="lblbatchno" Text="BATCH NO" runat="server"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td class="field_input">
                                                            <asp:DropDownList ID="ddlbatchno" Width="150" runat="server" class="chosen-select" 
                                                             AppendDataBoundItems="True">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="Reqddlbatchno" runat="server" ControlToValidate="ddlbatchno"
                                                                SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="Search" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            <div class="btn_24_blue">
                                                                <span class="icon find_co"></span>
                                                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn_link" OnClick="btnSearch_Click" />
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="7" class="field_input" style="padding-left: 10px;">
                                                            <fieldset>
                                                                <legend>WASTAGE DETAILS</legend>
                                                                <div style="margin: 0 auto; width: 100%;">
                                                                    <div style="overflow: hidden;" id="DivHeaderRow">
                                                                    </div>
                                                                    <div id="DivMainContent" style="overflow: scroll; height: 400px;" onscroll="OnScrollDiv(this);">
                                                                        <asp:GridView ID="gvProductionWastage" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Available"
                                                                            Width="100%" CssClass="zebra">
                                                                            <Columns>
                                                                                <asp:TemplateField Visible="false">
                                                                                    <HeaderTemplate>
                                                                                        <asp:CheckBox ID="chkAllSelect" runat="server" onclick="CheckAllheader(this);" Text=" " />
                                                                                    </HeaderTemplate>
                                                                                    <HeaderStyle Width="10px" />
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chkSelect" runat="server" Text=" " />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="CATEGORYID" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblCATEGORYID" runat="server" Text='<%# Bind("CATEGORYID") %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="CATEGORYNAME">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblCATEGORYNAME" runat="server" Text='<%# Bind("CATEGORYNAME") %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="MATERIALID" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblMATERIALID" runat="server" Text='<%# Bind("MATERIALID") %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="MATERIALNAME">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblMATERIALNAME" runat="server" Text='<%# Bind("MATERIALNAME") %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="UOMID" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblUOMID" runat="server" Text='<%# Bind("UOMID") %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="UOMNAME">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblUOMNAME" runat="server" Text='<%# Bind("UOMNAME") %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="PRODUCTION WASTAGE" HeaderStyle-Width="130px">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="lblISSUEQTY" Width="100px" runat="server" Text='<%# Bind("QTY") %>' Enabled="false" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                 <asp:TemplateField HeaderText="CONSUMABLES QTY" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtCONSUMABLESQTY" runat="server" Text='<%# Bind("REWORKINGWASTAGE") %>' Enabled="false"/>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="REWORKING WASTAGE">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtWastage" Width="100px" runat="server" onkeyup="calculate(this);" Text="0" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="RETURN QTY">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="lblReturnQty" Width="100px" runat="server" Text='<%# Bind("RETURNQTY") %>' Enabled="false" />
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