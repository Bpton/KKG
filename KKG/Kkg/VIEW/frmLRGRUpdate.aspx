<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true"
    CodeFile="frmLRGRUpdate.aspx.cs" Inherits="VIEW_frmLRGRUpdate" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

<script type="text/javascript" language="javascript">
    function CheckAllheader(Checkbox) {
        var GridVwHeaderCheckbox = document.getElementById("<%=gvUnlockInvoice.ClientID %>");
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
                                <h6>
                                    LR/GR NO Update</h6>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlADD" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_title">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td valign="top" style="padding-top: 10px;" width="50">
                                                            <asp:Label ID="Label1" runat="server" Text="Depot"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td class="field_input" width="210">
                                                            <asp:DropDownList ID="ddlDepot" Width="210" runat="server" class="chosen-select"
                                                                data-placeholder="Choose Transporter" AppendDataBoundItems="True" ValidationGroup="Save">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfv_ddlDepot" runat="server" ControlToValidate="ddlDepot"
                                                                ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="Save"
                                                                ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td valign="top" style="padding-top: 10px;" width="50">
                                                            <asp:Label ID="Label2" runat="server" Text="MODULE"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td class="field_input" id="td1" runat="server">
                                                            <asp:DropDownList ID="ddlModule" Width="210" runat="server" class="chosen-select"
                                                                data-placeholder="Choose Transporter" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlModule_SelectedIndexChanged"
                                                                AutoPostBack="true">
                                                                <asp:ListItem Text="Sale Invoice" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Stock Transfer" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="Purchase Stock  Receipt" Value="3"></asp:ListItem>
                                                                <%--<asp:ListItem Text="factory Despatch" Value="4" ></asp:ListItem>--%>
                                                                <asp:ListItem Text="Sale Return" Value="5"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td valign="top" style="padding-top: 10px;" width="50" id="tdGroup" runat="server">
                                                            <asp:Label ID="lblGroup" runat="server"  Text="Group"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td class="field_input" width="210" id="tdBusinessSegment" runat="server">
                                                            <asp:DropDownList ID="ddlBusinessSegment" Width="210" runat="server" class="chosen-select"
                                                                data-placeholder="Choose Transporter" AppendDataBoundItems="True" ValidationGroup="Save">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfv_ddlBusinessSegment" runat="server" ControlToValidate="ddlBusinessSegment"
                                                                ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="Save"
                                                                ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" style="padding-top: 10px;" width="50">
                                                            <asp:Label ID="Label16" runat="server" Text="From Date"></asp:Label>
                                                        </td>
                                                        <td width="105">
                                                            <asp:TextBox ID="txtFromDate" runat="server" MaxLength="10" Width="70" Enabled="false" 
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
                                                        <td valign="top" style="padding-top: 10px;" width="50">
                                                            <asp:Label ID="Label19" runat="server" Text="To Date"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="105">
                                                            <asp:TextBox ID="txtToDate" runat="server" MaxLength="10" Width="70" Enabled="false"
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
                                                        <td valign="top" width="110">
                                                            <div class="btn_24_blue">
                                                                <span class="icon find_co"></span>
                                                                <asp:Button ID="btnSearchInvoice" runat="server" Text="Search" CssClass="btn_link"
                                                                    ValidationGroup="Save" OnClick="btnSearchInvoice_Click" />
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <fieldset>
                                                    <legend>INVOICE DETAILS</legend>
                                                    <div style="margin: 0 auto; width: 100%;">
                                                        <div style="overflow: hidden;" id="DivHeaderRow" style="position: absolute">
                                                        </div>
                                                        <div id="DivMainContent" style="overflow: scroll; height: 300px;">
                                                            <asp:GridView ID="gvUnlockInvoice" runat="server" Width="100%" CssClass="zebra" AutoGenerateColumns="false"
                                                                EmptyDataText="No Records Available" onrowdatabound="gvUnlockInvoice_RowDataBound">
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

                                                                    <asp:TemplateField HeaderText="SL No.">
                                                                        <ItemTemplate>
                                                                            <%#Container.DataItemIndex+1%>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="2%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="SALEINVOICEID" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSALEINVOICEID" runat="server" Text='<%# Bind("SALEINVOICEID") %>'
                                                                                value='<%# Eval("SALEINVOICEID") %>' Visible="false"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField HeaderText="SALEINVOICE NO" DataField="SALEINVOICENO" ItemStyle-Width="180" />
                                                                    <asp:BoundField HeaderText="DATE" DataField="SALEINVOICEDATE" ItemStyle-Width="70" />
                                                                    <asp:BoundField HeaderText="PARTY" DataField="DISTRIBUTORNAME" ItemStyle-Width="200px"
                                                                        ItemStyle-Wrap="true" />
                                                                    <asp:BoundField HeaderText="NETAMOUNT" DataField="NETAMOUNT" ItemStyle-Width="70px" />
                                                                    <asp:BoundField HeaderText="GNG/INVOICE NO" DataField="GATEPASSNO" ItemStyle-Width="80px" Visible="false" />
                                                                    <asp:TemplateField HeaderText="VEHICHLE NO" ItemStyle-Width="50px" HeaderStyle-Width="50px">
                                                                        <ItemTemplate>
                                                                           
                                                                            <asp:TextBox ID="txtvehichleno" runat="server" Text='<%# Bind("VEHICHLENO") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="TOTAL CASE " ItemStyle-Width="50px" HeaderStyle-Width="50px">
                                                                        <ItemTemplate>
                                                                           
                                                                            <asp:TextBox ID="txttotalcasepack" runat="server" Text='<%# Bind("TOTALCASE") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    
                                                                    <asp:TemplateField HeaderText="LR/GR NO" ItemStyle-Width="50px" HeaderStyle-Width="50px">
                                                                        <ItemTemplate>
                                                                           
                                                                            <asp:TextBox ID="txtlrgrno" runat="server" Text='<%# Bind("LRGRNO") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="LR/GR DATE " ItemStyle-Width="60px" HeaderStyle-Width="100px">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="grdtxtlrgrdate" runat="server" MaxLength="10"  Width="60px" 
                                                                             Text='<%# Eval("LRGRDATE").ToString()=="01/01/1900" ? "":Eval("LRGRDATE")%>'
                                                                                Enabled="false"></asp:TextBox>
                                                                            <asp:ImageButton ID="imgPopupbankdate" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                                runat="server" Height="24" />
                                                                                 
                                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtenderimgPopupbankdate" PopupButtonID="imgPopupbankdate"
                                                                                runat="server" TargetControlID="grdtxtlrgrdate" CssClass="cal_Theme1" PopupPosition="Left"
                                                                                Format="dd/MM/yyyy">
                                                                            </ajaxToolkit:CalendarExtender>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Transporter">
                                                                        
                                                                        <ItemTemplate>
                                                                          
                                                                            <asp:DropDownList ID="ddlTransporter" runat="server" Width="260px">
                                                                            </asp:DropDownList>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="PRINT" ItemStyle-Width="30" ItemStyle-Wrap="false">
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnPrint" runat="server" CausesValidation="false" class="filter_btn printer_co"
                                                                    Style="margin-left: 10px;" ToolTip='<%# Bind("SALEINVOICEID") %>' OnClick="btnPrint_Click" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                        <div id="DivFooterRow" style="overflow: hidden;">
                                                        </div>
                                                    </div>
                                                </fieldset>
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td valign="middle" width="110">
                                                            <div class="btn_24_blue">
                                                                <span class="icon exclamation_co"></span>
                                                                <asp:Button ID="Button1" runat="server" Text="Save" CssClass="btn_link" OnClick="btnsave_Click" />
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
