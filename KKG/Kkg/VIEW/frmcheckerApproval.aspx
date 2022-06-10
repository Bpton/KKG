<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true"
    CodeFile="frmcheckerApproval.aspx.cs" Inherits="VIEW_frmcheckerApproval" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                                    Checker Approval</h6>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlADD" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_title" width="50">
                                                <asp:Label ID="Label1" runat="server" Text="Depot"></asp:Label>
                                                <span class="req">*</span>
                                            </td>
                                            <td class="field_input" width="130">
                                                <asp:DropDownList ID="ddlDepot" Width="120" runat="server" class="chosen-select"
                                                    data-placeholder="Choose Depot" AppendDataBoundItems="True" ValidationGroup="Save" 
                                                    OnSelectedIndexChanged="ddlDepot_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                </asp:DropDownList>
                                                <%--<asp:RequiredFieldValidator ID="rfv_ddlDepot" runat="server" ControlToValidate="ddlDepot"
                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="*" ValidationGroup="Save"
                                                    ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                            </td>
                                            <td class="field_title" width="55">
                                                <asp:Label ID="Label3" runat="server" Text="Module"></asp:Label>
                                                <span class="req">*</span>
                                            </td>
                                            <td class="field_input" id="td1" runat="server" width="130">
                                                <asp:DropDownList ID="ddlModule" Width="150" runat="server" class="chosen-select"
                                                    data-placeholder="Choose Module" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlModule_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                    <%--<asp:ListItem Text="Depot Stock Received" Value="30"></asp:ListItem>
                                                    <asp:ListItem Text="Depot Stock Transfer" Value="29"></asp:ListItem>
                                                    <asp:ListItem Text="Factory Stock Received" Value="31"></asp:ListItem>
                                                    <asp:ListItem Text="Gross Sale Return" Value="27"></asp:ListItem>
                                                    <asp:ListItem Text="Invoice Sale Return" Value="26"></asp:ListItem>
                                                    <asp:ListItem Text="Purchase Received" Value="28"></asp:ListItem>
                                                    <asp:ListItem Text="Sale Invoice" Value="1" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Transporter Bill" Value="25"></asp:ListItem>
                                                    <asp:ListItem Text="Voucher Journal" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Voucher Payment" Value="15"></asp:ListItem>
                                                    <asp:ListItem Text="Voucher Receipt" Value="16"></asp:ListItem>
                                                    <asp:ListItem Text="Debit Note" Value="14"></asp:ListItem>
                                                    <asp:ListItem Text="Credit Note" Value="13"></asp:ListItem>
                                                    <asp:ListItem Text="Contra" Value="17"></asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </td>
                                            <td id="tdgroup" runat="server" class="field_title" width="55">
                                                <asp:Label ID="lblgroup" runat="server" Text="Group"></asp:Label>
                                                <span class="req">*</span>
                                            </td>
                                            <td class="field_input" id="tdBusinessSegment" runat="server" width="210">
                                                <asp:DropDownList ID="ddlBusinessSegment" Width="210" runat="server" class="chosen-select"
                                                    data-placeholder="Select Group" AppendDataBoundItems="True" ValidationGroup="Save">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="field_title" width="80">
                                                <asp:Label ID="Label16" runat="server" Text="(From-To)"></asp:Label>
                                                <span class="req">*</span>
                                            </td>
                                            <td class="field_input" width="95">
                                                <asp:TextBox ID="txtFromDate" runat="server" MaxLength="10" Width="60" Enabled="false" AutoPostBack="true" 
                                                    placeholder="dd/MM/yyyy" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="Search"
                                                    Font-Bold="true"></asp:TextBox>
                                                <asp:ImageButton ID="imgPopup" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                    runat="server" Height="24" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarFromDate" PopupButtonID="imgPopup" runat="server"
                                                    TargetControlID="txtFromDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                            <td class="field_input" width="95">
                                                <asp:TextBox ID="txtToDate" runat="server" MaxLength="10" Width="60" Enabled="false" AutoPostBack="true" 
                                                    placeholder="dd/mm/yyyy" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="Search"
                                                    Font-Bold="true"></asp:TextBox>
                                                <asp:ImageButton ID="imgPopuptodate" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                    runat="server" Height="24" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtenderToDate" PopupButtonID="imgPopuptodate"
                                                    runat="server" TargetControlID="txtToDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                            <td class="field_input" width="110">
                                                <div class="btn_24_blue">
                                                    <span class="icon find_co"></span>
                                                    <asp:Button ID="btnSearchInvoice" runat="server" Text="Search" CssClass="btn_link"
                                                        ValidationGroup="Save" OnClick="btnSearchInvoice_Click" />
                                                </div>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="gridcontent-inner" id="trinvoice" runat="server">
                                        <fieldset>
                                            <legend>INVOICE DETAILS</legend>
                                            <div style="overflow: scroll; height: 350px; width: 100%;">
                                                <asp:GridView ID="gvUnlockInvoice" runat="server" Width="100%" CssClass="reportgrid"
                                                    AutoGenerateColumns="false" ShowFooter="false" AlternatingRowStyle-BackColor="AliceBlue"
                                                    EmptyDataText="No Records Available">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkAllSelect" runat="server" onclick="CheckAllheader(this);" Text=" " />
                                                            </HeaderTemplate>
                                                            <HeaderStyle Width="25px" />
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelect" runat="server" Text=" "  class="round" Style="float:left; padding-right:25px;" ToolTip='<%# Bind("DISTRIBUTORNAME") %>' onclick="setrowcolor(this);" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SALEINVOICEID" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSALEINVOICEID" runat="server" Text='<%# Bind("SALEINVOICEID") %>'
                                                                    value='<%# Eval("SALEINVOICEID") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SL" ItemStyle-Wrap="false" ItemStyle-Width="10px">
                                                            <ItemTemplate>
                                                                <nav style="position: relative;"><span class="badge black"><%# Container.DataItemIndex + 1 %></span></nav>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-Wrap="false">
                                                            <HeaderTemplate>&nbsp;
                                                                <asp:CheckBox ID="chkAllSelectinvHO" runat="server" onclick="CheckAllheaderinvHO(this);" Visible="false"
                                                                    Text="&nbsp;&nbsp;TO HO" />
                                                            </HeaderTemplate>
                                                            <%--<HeaderStyle Width="90px" />--%>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelectinv1" runat="server" Text=" " Visible="false"
                                                                    ToolTip='<%# Bind("DISTRIBUTORNAME") %>' onclick="setrowcolorinv(this);" />
                                                                <%--<asp:Label ID="lbltoho" runat="server" ForeColor="Red">&nbsp;No</asp:Label>--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-Wrap="false">
                                                            <HeaderTemplate>&nbsp;
                                                                <asp:CheckBox ID="chkAllSelectinvREVERSE" runat="server" onclick="CheckAllheaderinvREVERSE(this);"
                                                                    Text="&nbsp;&nbsp;REVERSE" />
                                                            </HeaderTemplate>
                                                            <%--<HeaderStyle Width="90px" />--%>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelectReverse" runat="server" Text=" "
                                                                    ToolTip='<%# Bind("DISTRIBUTORNAME") %>' onclick="setrowcolorReverse(this);" Checked='<%# bool.Parse(Eval("ISREVERSEAPPLICABLE").ToString() == "Y" ? "True": "False")%>' />
                                                                <%--<asp:Label ID="lbltoho" runat="server" ForeColor="Red">&nbsp;No</asp:Label>--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="INVOICE NO" ItemStyle-Width="160" ItemStyle-Wrap="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSALEINVOICENO" runat="server" Text='<%# Bind("SALEINVOICENO") %>'
                                                                    ToolTip='<%# Bind("DISTRIBUTORNAME") %>' value='<%# Eval("SALEINVOICENO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DATE" ItemStyle-Width="60" ItemStyle-Wrap="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSALEINVOICEDATE" runat="server" Text='<%# Bind("SALEINVOICEDATE") %>'
                                                                    ToolTip='<%# Bind("DISTRIBUTORNAME") %>' value='<%# Eval("SALEINVOICEDATE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="PARTY" DataField="DISTRIBUTORNAME" ItemStyle-Wrap="false" />
                                                        <%--<asp:BoundField HeaderText="TYPE" DataField="BILLTYPE" ItemStyle-Wrap="false" />--%>
                                                        <asp:TemplateField HeaderText="TYPE" ItemStyle-Width="60" ItemStyle-Wrap="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBILLTYPE" runat="server" Text='<%# Bind("BILLTYPE") %>' ToolTip='<%# Bind("DISTRIBUTORNAME") %>'
                                                                    value='<%# Eval("BILLTYPE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="INV NO" DataField="GETPASSNO" HeaderStyle-Wrap="false"
                                                            ItemStyle-Width="80" ItemStyle-Wrap="false" />
                                                        <asp:BoundField HeaderText="SP.DISC" DataField="TOTALSPECIALDISCVALUE" ItemStyle-Width="70"
                                                            ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" />
                                                        <asp:BoundField HeaderText="REBAT" DataField="GROSSREBATEVALUE" ItemStyle-Width="50"
                                                            ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" />
                                                        <asp:BoundField HeaderText="TDS" DataField="TOTALTDS" ItemStyle-Width="50" ItemStyle-Wrap="false"
                                                            ItemStyle-HorizontalAlign="Right" />
                                                        <asp:BoundField HeaderText="TAX" DataField="TAXVALUE" ItemStyle-Width="60" ItemStyle-Wrap="false"
                                                            ItemStyle-HorizontalAlign="Right" />
                                                        <asp:BoundField HeaderText="ROUND OFF" DataField="ROUNDOFFVALUE" ItemStyle-Width="80"
                                                            ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false" />
                                                        <asp:BoundField HeaderText="NET AMT" DataField="TOTALSALEINVOICEVALUE" ItemStyle-Width="70"
                                                            ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false" />
                                                        <asp:BoundField HeaderText="APPROVAL" DataField="ISVERIFIEDDESC" ItemStyle-Width="70"
                                                            ItemStyle-Wrap="false" ItemStyle-ForeColor="Blue" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:TemplateField HeaderText="PREVIEW" ItemStyle-Width="30" ItemStyle-Wrap="false">
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnPrint" runat="server" CausesValidation="false" class="filter_btn airplane"
                                                                    Style="margin-left: 10px;" ToolTip='<%# Bind("SALEINVOICENO") %>' OnClick="btnPrint_Click" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </fieldset>
                                    </div>
                                    <div class="gridcontent-inner" id="trvoucher" runat="server">
                                        <fieldset>
                                            <legend>VOUCHER DETAILS</legend>
                                            <div style="overflow: scroll; height: 350px; width: 100%;">
                                                <asp:GridView ID="gvUnlockVoucher" runat="server" Width="100%" CssClass="reportgrid"
                                                    AutoGenerateColumns="false" ShowFooter="false" AlternatingRowStyle-BackColor="AliceBlue"
                                                    EmptyDataText="No Records Available">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkAllSelectvou" runat="server" onclick="CheckAllheadervou(this);"
                                                                    Text=" " />
                                                            </HeaderTemplate>
                                                            <HeaderStyle Width="25px" />
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelectvou" runat="server" Text=" " class="round" Style="padding-left: 25px;"
                                                                    ToolTip='<%# Bind("LedgerName") %>' onclick="setrowcolorvou(this);" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="AccEntryID" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAccEntryID" runat="server" Text='<%# Bind("AccEntryID") %>' value='<%# Eval("AccEntryID") %>'
                                                                    Visible="false"></asp:Label>
                                                                 <asp:Label ID="lblISTRANSFERTOHO" runat="server" Text='<%# Bind("ISTRANSFERTOHO") %>' value='<%# Eval("ISTRANSFERTOHO") %>'
                                                                    Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SL" ItemStyle-Wrap="false" ItemStyle-Width="10px">
                                                            <ItemTemplate>
                                                                <nav style="position: relative;"><span class="badge black"><%# Container.DataItemIndex + 1 %></span></nav>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkAllSelectvouHO" runat="server" onclick="CheckAllheadervouHO(this);"
                                                                    Text="&nbsp;&nbsp;TO HO" />
                                                            </HeaderTemplate>
                                                            <HeaderStyle Width="50px" />
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelectvou1" runat="server" Text=" " Style="padding-left: 15px;"
                                                                    ToolTip='<%# Bind("LedgerName") %>' onclick="setrowcolorvou(this);" />
                                                                <%--<asp:Label ID="lbltoho" runat="server" ForeColor="Red">&nbsp;No</asp:Label>--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="VOUCHER NO" ItemStyle-Width="160" ItemStyle-Wrap="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblVOUCHERNO" runat="server" Text='<%# Bind("VoucherNo") %>' ToolTip='<%# Bind("LedgerName") %>'
                                                                    value='<%# Eval("VoucherNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DATE" ItemStyle-Width="60" ItemStyle-Wrap="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblVOUCHERDate" runat="server" Text='<%# Bind("Date") %>' ToolTip='<%# Bind("LedgerName") %>'
                                                                    value='<%# Eval("Date") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="PARTY" DataField="LedgerName" ItemStyle-Width="25%" />
                                                        <asp:BoundField HeaderText="REGION" DataField="BranchName" ItemStyle-Width="70" ItemStyle-Wrap="false" />
                                                        <asp:BoundField HeaderText="AMOUNT" DataField="Amount" ItemStyle-Width="70" ItemStyle-Wrap="false"
                                                            ItemStyle-HorizontalAlign="Right" />
                                                        <asp:BoundField HeaderText="NARRATION" DataField="NARRATION" HeaderStyle-Wrap="false" ItemStyle-Width="25%"/>
                                                        <asp:BoundField HeaderText="APPROVAL" DataField="VoucherApproved" ItemStyle-Width="70"
                                                            ItemStyle-Wrap="false" ItemStyle-ForeColor="Blue" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:TemplateField HeaderText="PREVIEW" ItemStyle-Width="30" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnvoucherPrint" runat="server" CausesValidation="false" class="filter_btn airplane" 
                                                                   ToolTip='<%# Bind("VoucherNo") %>' OnClick="btnvoucherPrint_Click" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </fieldset>
                                    </div>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px; width:50%;">
                                                <div class="btn_24_green" id="divbtnapprove" runat="server">
                                                    <span class="icon approve_co"></span>
                                                    <asp:LinkButton ID="btnApprove" runat="server" Text="Approve" CssClass="btn_link"
                                                        CausesValidation="false" OnClick="btnApprove_Click" OnClientClick="return confirm('Are you sure you want to approved above invoices/vouchers?')" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_red" id="divbtnreject" runat="server">
                                                    <span class="icon reject_co"></span>
                                                    <asp:LinkButton ID="btnReject" runat="server" Text="Reject" CssClass="btn_link" CausesValidation="false"
                                                        OnClick="btnReject_Click" OnClientClick="return confirm('Are you sure you want to reject above invoices/vouchers?')" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
                                                    <span class="icon cross_octagon_co"></span>
                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn_link" CausesValidation="false"
                                                        OnClick="btnCancel_Click" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Label ID="lbltotalcount" runat="server" Text="" Font-Bold="true" ForeColor="Gray"
                                                    Font-Size="Small"></asp:Label>
                                            </td>
                                            <td style="float:left; width:96%;">
                                                <asp:TextBox ID="txtRejectionNote" runat="server" TextMode="MultiLine" placeholder="Rejection Note if any.........."
                                        Style="height: 40px; width:100%;"> </asp:TextBox>
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
            </div>
            <script type="text/javascript" language="javascript">
                function CheckAllheader(Checkbox) {
                    var GridVwHeaderCheckbox = document.getElementById("<%=gvUnlockInvoice.ClientID %>");
                    var lbltotalcount = document.getElementById("<%=lbltotalcount.ClientID %>");
                    var totalcount = 0;
                    if (Checkbox.checked) {
                        for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
                            GridVwHeaderCheckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
                            /*GridVwHeaderCheckbox.rows[i].style.color = '#389638';
                            GridVwHeaderCheckbox.rows[i].cells[0].style.border = '1px solid #fff';*/

                            totalcount = totalcount + 1;
                        }
                    }
                    else {
                        for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
                            GridVwHeaderCheckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = false;
                            /*GridVwHeaderCheckbox.rows[i].style.color = '#4B555E';
                            GridVwHeaderCheckbox.rows[i].cells[0].style.border = '0px solid gray';*/
                        }
                    }
                    if (totalcount > 0) {
                        lbltotalcount.style.display = "";
                        lbltotalcount.innerHTML = 'You have selected : ' + totalcount + ' invoices.';
                    }
                    else {
                        lbltotalcount.style.display = "none";
                    }
                }
            </script>
            <script type="text/javascript">
                function setrowcolor(chb) {
                    if (chb.checked) {
                        /*chb.parentNode.parentNode.style.color = '#389638';
                        chb.parentNode.parentNode.style.border = '1px solid #fff';*/
                    }
                    else {
                        /*chb.parentNode.parentNode.style.color = '#4B555E';
                        chb.parentNode.parentNode.style.border = '0px solid gray';*/
                    }

                    var GridVwHeaderCheckbox = document.getElementById("<%=gvUnlockInvoice.ClientID %>");
                    var lbltotalcount = document.getElementById("<%=lbltotalcount.ClientID %>");
                    var totalcount = 0;

                    for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
                        if (GridVwHeaderCheckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked == true) {
                            totalcount = totalcount + 1;
                        }
                    }
                    if (totalcount > 0) {
                        lbltotalcount.style.display = "";
                        lbltotalcount.innerHTML = 'You have selected : ' + totalcount + ' invoices.';
                    }
                    else {
                        lbltotalcount.style.display = "none";
                    }
                }
            </script>
            <script type="text/javascript" language="javascript">
                function CheckAllheadervou(Checkbox) {
                    var GridVwHeaderCheckbox = document.getElementById("<%=gvUnlockVoucher.ClientID %>");
                    var lbltotalcount = document.getElementById("<%=lbltotalcount.ClientID %>");
                    var totalcount = 0;
                    if (Checkbox.checked) {
                        for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
                            GridVwHeaderCheckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
                            /*GridVwHeaderCheckbox.rows[i].style.color = '#389638';
                            GridVwHeaderCheckbox.rows[i].cells[0].style.border = '1px solid #fff';*/

                            totalcount = totalcount + 1;
                        }
                    }
                    else {
                        for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
                            GridVwHeaderCheckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = false;
                            /*GridVwHeaderCheckbox.rows[i].style.color = '#4B555E';
                            GridVwHeaderCheckbox.rows[i].cells[0].style.border = '0px solid gray';*/
                        }
                    }
                    if (totalcount > 0) {
                        lbltotalcount.style.display = "";
                        lbltotalcount.innerHTML = 'You have selected : ' + totalcount + ' invoices.';
                    }
                    else {
                        lbltotalcount.style.display = "none";
                    }
                }
            </script>
            <script type="text/javascript">
                function setrowcolorvou(chb) {
                    if (chb.checked) {
                        /*chb.parentNode.parentNode.style.color = '#389638';
                        chb.parentNode.parentNode.style.border = '1px solid #fff';*/
                    }
                    else {
                        /*chb.parentNode.parentNode.style.color = '#4B555E';
                        chb.parentNode.parentNode.style.border = '0px solid gray';*/
                    }

                    var GridVwHeaderCheckbox = document.getElementById("<%=gvUnlockVoucher.ClientID %>");
                    var lbltotalcount = document.getElementById("<%=lbltotalcount.ClientID %>");
                    var totalcount = 0;

                    for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
                        if (GridVwHeaderCheckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked == true) {
                            totalcount = totalcount + 1;
                        }
                    }
                    if (totalcount > 0) {
                        lbltotalcount.style.display = "";
                        lbltotalcount.innerHTML = 'You have selected : ' + totalcount + ' invoices.';
                    }
                    else {
                        lbltotalcount.style.display = "none";
                    }
                }
            </script>
            <script type="text/javascript">
                function CheckAllheadervouHO(Checkbox) {
                    var GridVwHeaderCheckbox = document.getElementById("<%=gvUnlockVoucher.ClientID %>");
                    if (Checkbox.checked) {
                        for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
                            GridVwHeaderCheckbox.rows[i].cells[2].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
                            /*GridVwHeaderCheckbox.rows[i].style.color = '#389638';
                            GridVwHeaderCheckbox.rows[i].cells[2].style.border = '1px solid #fff';*/
                        }
                    }
                    else {
                        for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
                            GridVwHeaderCheckbox.rows[i].cells[2].getElementsByTagName("INPUT")[0].checked = false;
                            /*GridVwHeaderCheckbox.rows[i].style.color = '#4B555E';
                            GridVwHeaderCheckbox.rows[i].cells[2].style.border = '0px solid gray';*/
                        }
                    }
                }
            </script>
            <script type="text/javascript">
                function CheckAllheaderinvHO(Checkbox) {
                    var GridVwHeaderCheckbox = document.getElementById("<%=gvUnlockInvoice.ClientID %>");
                    if (Checkbox.checked) {
                        for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
                            GridVwHeaderCheckbox.rows[i].cells[2].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
                            /*GridVwHeaderCheckbox.rows[i].style.color = '#389638';
                            GridVwHeaderCheckbox.rows[i].cells[2].style.border = '1px solid #fff';*/
                        }
                    }
                    else {
                        for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
                            GridVwHeaderCheckbox.rows[i].cells[2].getElementsByTagName("INPUT")[0].checked = false;
                            /*GridVwHeaderCheckbox.rows[i].style.color = '#4B555E';
                            GridVwHeaderCheckbox.rows[i].cells[2].style.border = '0px solid gray';*/
                        }
                    }
                }
            </script>
            <script type="text/javascript">
                function CheckAllheaderinvREVERSE(Checkbox) {
                    var GridVwHeaderCheckbox = document.getElementById("<%=gvUnlockInvoice.ClientID %>");
                    if (Checkbox.checked) {
                        for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
                            GridVwHeaderCheckbox.rows[i].cells[3].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
                            /*GridVwHeaderCheckbox.rows[i].style.color = '#389638';
                            GridVwHeaderCheckbox.rows[i].cells[3].style.border = '1px solid #fff';*/
                        }
                    }
                    else {
                        for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
                            GridVwHeaderCheckbox.rows[i].cells[3].getElementsByTagName("INPUT")[0].checked = false;
                            /*GridVwHeaderCheckbox.rows[i].style.color = '#4B555E';
                            GridVwHeaderCheckbox.rows[i].cells[3].style.border = '0px solid gray';*/
                        }
                    }
                }
            </script>
            <style type="text/css">
                .round
                {
                    position: relative;
                }
                
                .round label
                {
                    background-color: #fff;
                    border: 1px solid #ccc;
                    border-radius: 50%;
                    height: 16px;
                    left: 10px;
                    position: absolute;
                    bottom: 10;
                    width: 16px;
                }
                
                .round label:after
                {
                    border: 2px solid #fff;
                    border-top: none;
                    border-right: none;
                    content: "";
                    height: 6px;
                    left: 2px;
                    bottom: 3px;
                    opacity: 0;
                    position: absolute;
                    top: 2px;
                    transform: rotate(-45deg);
                    width: 10px;
                }
                
                .round input[type="checkbox"]
                {
                    visibility: hidden;
                }
                
                .round input[type="checkbox"]:checked + label
                {
                    background-color: #66bb6a;
                    border-color: #66bb6a;
                }
                
                .round input[type="checkbox"]:checked + label:after
                {
                    opacity: 1;
                }
                
                
                body
                {
                    background-color: #f1f2f3;
                    -webkit-box-align: center;
                    -ms-flex-align: center;
                    align-items: center;
                }
                
                .container
                {
                    margin: 0 auto;
                }
            </style>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
