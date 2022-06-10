<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true"
    CodeFile="frmAdminUnlock.aspx.cs" Inherits="VIEW_frmAdminUnlock" %>

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
                                    Invoice Details</h6>
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
                                                    data-placeholder="Choose Transporter" AppendDataBoundItems="True" ValidationGroup="Save">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfv_ddlDepot" runat="server" ControlToValidate="ddlDepot"
                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="*" ValidationGroup="Save"
                                                    ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="field_title" width="50">
                                                <asp:Label ID="Label3" runat="server" Text="Module"></asp:Label>
                                            </td>
                                            <td width="140" class="field_input" id="td1" runat="server">
                                                <asp:DropDownList ID="ddlModule" Width="140" runat="server" class="chosen-select"
                                                    data-placeholder="Choose Module" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlModule_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                    <%--<asp:ListItem Text="Sale Invoice" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Stock Transfer" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Purchase Stock Receipt" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="Depot Stock Receipt" Value="4"></asp:ListItem>
                                                    <asp:ListItem Text="Sale Return" Value="5"></asp:ListItem>
                                                    <asp:ListItem Text="Advance Receipt" Value="16"></asp:ListItem>
                                                    <asp:ListItem Text="Advance Payment" Value="15"></asp:ListItem>
                                                    <asp:ListItem Text="Voucher Journal" Value="18"></asp:ListItem>
                                                    <asp:ListItem Text="Transporter Bill" Value="17"></asp:ListItem>
                                                    <asp:ListItem Text="Purchase Bill(Factory)" Value="19"></asp:ListItem>
                                                    <asp:ListItem Text="Trading Invoice" Value="20"></asp:ListItem>
                                                    <asp:ListItem Text="Stock Despatch(Factory)" Value="21"></asp:ListItem>
                                                    <asp:ListItem Text="Factory Stock Receipt" Value="22"></asp:ListItem>
                                                    <asp:ListItem Text="Factory Quality Control" Value="23" Enabled="false"></asp:ListItem>
                                                    <asp:ListItem Text="Account Checker-1(Factory) " Value="24" ></asp:ListItem>
                                                    <asp:ListItem Text="Factory GRN Stock IN " Value="25" ></asp:ListItem>
                                                    <asp:ListItem Text="Credit Note" Value="13" ></asp:ListItem>
                                                    <asp:ListItem Text="Debit Note" Value="14" ></asp:ListItem>
                                                    <asp:ListItem Text="Contra" Value="26" ></asp:ListItem>
                                                    <asp:ListItem Text="Quality Control" Value="27" ></asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </td>
                                            <td class="field_title" width="100" id="tdlblBusinessSegment" runat="server">
                                                <asp:Label ID="Label2" runat="server" Text="Business Segment"></asp:Label>
                                                <span class="req">*</span>
                                            </td>
                                            <td width="210" class="field_input" id="tdBusinessSegment" runat="server">
                                                <asp:DropDownList ID="ddlBusinessSegment" Width="210" runat="server" class="chosen-select"
                                                    data-placeholder="Choose Transporter" AppendDataBoundItems="True" ValidationGroup="Save">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="field_title" width="105">
                                                <asp:Label ID="Label16" runat="server" Text="(From-To) Date"></asp:Label>
                                            </td>
                                            <td class="field_input" width="110">
                                                <asp:TextBox ID="txtFromDate" runat="server" MaxLength="10" Width="60" Enabled="false"
                                                    placeholder="dd/MM/yyyy" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="Search"
                                                    Font-Bold="true"></asp:TextBox>
                                                <asp:ImageButton ID="imgPopup" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                    runat="server" Height="24" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarFromDate" PopupButtonID="imgPopup" runat="server"
                                                    TargetControlID="txtFromDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                            <td class="field_input" width="110">
                                                <asp:TextBox ID="txtToDate" runat="server" MaxLength="10" Width="60" Enabled="false"
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
                                        <tr id="trinvoice" runat="server">
                                            <td colspan="11" class="field_input" style="padding-left: 10px;">
                                                <fieldset>
                                                    <legend>INVOICE DETAILS</legend>
                                                    <div style="margin: 0 auto; width: 100%;">
                                                        <div style="overflow: hidden;" id="DivHeaderRow">
                                                        </div>
                                                        <div id="DivMainContent" style="overflow: scroll; height: 300px;" onscroll="OnScrollDiv(this);">
                                                            <asp:GridView ID="gvUnlockInvoice" runat="server" Width="100%" CssClass="zebra" AutoGenerateColumns="false"
                                                                EmptyDataText="No Records Available">
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            <asp:CheckBox ID="chkAllSelect" runat="server" onclick="CheckAllheader(this);" Text=" " />
                                                                        </HeaderTemplate>
                                                                        <HeaderStyle Width="5px" />
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkSelect" runat="server" Text=" " class="round" Style="padding-left: 15px;"
                                                                            ToolTip='<%# Bind("DISTRIBUTORNAME") %>' onclick="setrowcolor(this);" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="SL" ItemStyle-Wrap="false" ItemStyle-Width="10px">
                                                                    <ItemTemplate>
                                                                        <nav style="position: relative;"><span class="badge black"><%# Container.DataItemIndex + 1 %></span></nav>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="SALEINVOICEID" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSALEINVOICEID" runat="server" Text='<%# Bind("SALEINVOICEID") %>'
                                                                                value='<%# Eval("SALEINVOICEID") %>' Visible="false"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField HeaderText="BILL NO" DataField="SALEINVOICENO" ItemStyle-Width="200px" />
                                                                    <asp:BoundField HeaderText="DATE" DataField="SALEINVOICEDATE" ItemStyle-Width="200px" />
                                                                    <asp:BoundField HeaderText="PARTY" DataField="DISTRIBUTORNAME" ItemStyle-Wrap="false" />
                                                                    <asp:BoundField HeaderText="NETAMOUNT" DataField="NETAMOUNT" ItemStyle-Width="200px"
                                                                        ItemStyle-HorizontalAlign="Right" />
                                                                    <asp:BoundField HeaderText="GNG NO" DataField="GATEPASSNO" ItemStyle-Width="200px" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                        <div id="DivFooterRow" style="overflow: hidden;">
                                                        </div>
                                                    </div>
                                                </fieldset>
                                            </td>
                                        </tr>
                                         <tr id="trvoucher" runat="server">
                                            <td colspan="11" class="field_input" style="padding-left: 10px;">
                                                <fieldset>
                                                    <legend>VOUCHER DETAILS</legend>
                                                    <div style="overflow: scroll; height: 300px; width: 100%;">
                                                        <asp:GridView ID="gvUnlockVoucher" runat="server" Width="100%" CssClass="zebra" AutoGenerateColumns="false" 
                                                            EmptyDataText="No Records Available" ForeColor="#384046">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox ID="chkAllSelectvou" runat="server" onclick="CheckAllheadervou(this);" Text=" " />
                                                                    </HeaderTemplate>
                                                                    <HeaderStyle Width="5px" />
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkSelectvou" runat="server" Text=" " class="round" Style="padding-left: 15px;"
                                                                            ToolTip='<%# Bind("LedgerName") %>' onclick="setrowcolorvou(this);" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="AccEntryID" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAccEntryID" runat="server" Text='<%# Bind("AccEntryID") %>'
                                                                            value='<%# Eval("AccEntryID") %>' Visible="false"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="SL" ItemStyle-Wrap="false" ItemStyle-Width="10px">
                                                                    <ItemTemplate>
                                                                        <nav style="position: relative;"><span class="badge black"><%# Container.DataItemIndex + 1 %></span></nav>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <HeaderTemplate>
                                                                       <asp:CheckBox ID="chkAllSelectvouHO" runat="server" onclick="CheckAllheadervouHO(this);" Text="&nbsp;&nbsp;TO HO"/>
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
                                                                        <asp:Label ID="lblVOUCHERNO" runat="server" Text='<%# Bind("VoucherNo") %>'
                                                                            ToolTip='<%# Bind("LedgerName") %>' value='<%# Eval("VoucherNo") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="DATE" ItemStyle-Width="60" ItemStyle-Wrap="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblVOUCHERDate" runat="server" Text='<%# Bind("Date") %>'
                                                                            ToolTip='<%# Bind("LedgerName") %>' value='<%# Eval("Date") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="PARTY" DataField="LedgerName" ItemStyle-Wrap="false" />
                                                                <asp:BoundField HeaderText="REGION" DataField="BranchName" ItemStyle-Width="70" ItemStyle-Wrap="false" />
                                                                <asp:BoundField HeaderText="AMOUNT" DataField="Amount" ItemStyle-Width="70" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" />
                                                                <asp:BoundField HeaderText="APPROVAL" DataField="VoucherApproved" ItemStyle-Width="70" ItemStyle-Wrap="false" ItemStyle-ForeColor="Blue" ItemStyle-HorizontalAlign="Center" />
                                                                <asp:TemplateField HeaderText="PREVIEW" ItemStyle-Width="30" ItemStyle-Wrap="false">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnvoucherPrint" runat="server" CausesValidation="false" class="filter_btn airplane"
                                                                            Style="margin-left: 25px;" ToolTip='<%# Bind("VoucherNo") %>' OnClick="btnvoucherPrint_Click" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </fieldset>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_input" colspan="11" style="padding-left: 10px;">
                                                <div class="btn_24_blue">
                                                    <span class="icon exclamation_co"></span>
                                                    <asp:Button ID="btnsave" runat="server" Text="UNLOCK" CssClass="btn_link" OnClick="btnsave_Click" OnClientClick="return confirm('Are you sure you want to unlocked above invoices?')" />
                                                </div>
                                                 &nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Label ID="lbltotalcount" runat="server" Text="" Font-Bold="true" ForeColor="Gray"
                                                    Font-Size="Small"></asp:Label>
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
                            /*GridVwHeaderCheckbox.rows[i].style.color = '#389638';*/
                            GridVwHeaderCheckbox.rows[i].cells[0].style.border = '1px solid #fff';

                            totalcount = totalcount + 1;
                        }
                    }
                    else {
                        for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
                            GridVwHeaderCheckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = false;
                            /*GridVwHeaderCheckbox.rows[i].style.color = '#4B555E';*/
                            GridVwHeaderCheckbox.rows[i].cells[0].style.border = '0px solid gray';
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
                        /*chb.parentNode.parentNode.style.color = '#389638';*/
                        chb.parentNode.parentNode.style.border = '1px solid #fff';
                    }
                    else {
                        /*chb.parentNode.parentNode.style.color = '#4B555E';*/
                        chb.parentNode.parentNode.style.border = '0px solid gray';
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
                            /*GridVwHeaderCheckbox.rows[i].style.color = '#389638';*/
                            GridVwHeaderCheckbox.rows[i].cells[0].style.border = '1px solid #fff';

                            totalcount = totalcount + 1;
                        }
                    }
                    else {
                        for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
                            GridVwHeaderCheckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = false;
                            /*GridVwHeaderCheckbox.rows[i].style.color = '#4B555E';*/
                            GridVwHeaderCheckbox.rows[i].cells[0].style.border = '0px solid gray';
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
                        /*chb.parentNode.parentNode.style.color = '#389638';*/
                        chb.parentNode.parentNode.style.border = '1px solid #fff';
                    }
                    else {
                        /*chb.parentNode.parentNode.style.color = '#4B555E';*/
                        chb.parentNode.parentNode.style.border = '0px solid gray';
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
                            /*GridVwHeaderCheckbox.rows[i].style.color = '#389638';*/
                            GridVwHeaderCheckbox.rows[i].cells[2].style.border = '1px solid #fff';
                        }
                    }
                    else {
                        for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
                            GridVwHeaderCheckbox.rows[i].cells[2].getElementsByTagName("INPUT")[0].checked = false;
                            /*GridVwHeaderCheckbox.rows[i].style.color = '#4B555E';*/
                            GridVwHeaderCheckbox.rows[i].cells[2].style.border = '0px solid gray';
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
