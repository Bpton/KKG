<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmRptBankReconciliation.aspx.cs" Inherits="VIEW_frmRptBankReconciliation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
            <div align="center">
                <asp:Image ID="Image1" ImageUrl="../images/loading123.gif" AlternateText="Processing" runat="server" />
                 <br /><b style="font-size:small;">Please wait.......  processing will take time to check order status!</b>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
        PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>BANK RECONCILIATION ENTRY
                                </h6>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlDisplay" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td width="100" class="field_title">Ledger
                                                            <span class="req">*</span>
                                            </td>
                                            <td width="190" class="field_input">
                                                <asp:DropDownList ID="ddlledgeraccount" runat="server" AppendDataBoundItems="true"
                                                    Width="180" class="chosen-select" data-placeholder="Choose" ValidationGroup="ADD">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RFV_ddlledgeraccount" runat="server" ValidationGroup="ADD"
                                                    ControlToValidate="ddlledgeraccount" ForeColor="Red" ErrorMessage="*" InitialValue="0"
                                                    SetFocusOnError="true" ValidateEmptyText="false"></asp:RequiredFieldValidator>
                                            </td>
                                            <td width="50" class="field_title">Depot
                                                            <span class="req">*</span>
                                            </td>
                                            <td width="140" class="field_input">
                                                <asp:DropDownList ID="ddldepot" runat="server" AppendDataBoundItems="true" Width="130"
                                                    class="chosen-select" data-placeholder="Choose" ValidationGroup="ADD">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="ADD"
                                                    ControlToValidate="ddldepot" ForeColor="Red" ErrorMessage="*" InitialValue="0"
                                                    SetFocusOnError="true" ValidateEmptyText="false"></asp:RequiredFieldValidator>
                                            </td>
                                            <td width="110" class="field_title">SHOW RECONCILED VOUCHERS
                                            </td>
                                            <td width="100" class="field_input">
                                                <asp:DropDownList ID="ddlReconciledVouchers" runat="server" class="chosen-select" data-placeholder="Select"
                                                    Width="100" ValidationGroup="Search">
                                                    <asp:ListItem Text="NO" Value="1" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="YES" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td width="70" class="field_title">
                                                Vouchers
                                                <span class="req">*</span>
                                            </td>
                                            <td width="70" class="field_input">
                                                <asp:DropDownList ID="ddlVouchers" runat="server" AppendDataBoundItems="true" Width="70" class="chosen-select" data-placeholder="Choose" ValidationGroup="ADD">
                                                </asp:DropDownList>
                                            </td>
                                            <td width="80" class="field_title">
                                                Narrations
                                            </td>
                                            <td width="90" class="field_input">
                                                <asp:DropDownList ID="ddlNarrations" runat="server" AppendDataBoundItems="true" Width="85" class="chosen-select" data-placeholder="Choose a Type">
                                                    <asp:ListItem Text="No" Value="1" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Yes" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td class="field_input">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title">
                                                (From - To) Date
                                            </td>
                                            <td class="field_input" colspan="2">
                                                <asp:TextBox ID="txtfromdate" runat="server" Enabled="false" Width="70" placeholder="dd/MM/yyyy"
                                                    MaxLength="10"></asp:TextBox>
                                                <asp:ImageButton ID="Imgfrom" runat="server" ImageUrl="~/images/calendar.png" Width="24px"
                                                    Height="24px" ImageAlign="AbsMiddle" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="Imgfrom" runat="server"
                                                    TargetControlID="txtfromdate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:TextBox ID="txttodate" runat="server" Enabled="false" Width="70" placeholder="dd/MM/yyyy"
                                                    MaxLength="10"></asp:TextBox>
                                                <asp:ImageButton ID="ImgToDate" runat="server" ImageUrl="~/images/calendar.png" Width="24px"
                                                    Height="24px" ImageAlign="AbsMiddle" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImgToDate" runat="server"
                                                    TargetControlID="txttodate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                            <td class="field_input">
                                                <div class="btn_24_blue">
                                                    <span class="icon exclamation_co"></span>
                                                    <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn_link" OnClick="btnShow_Click"
                                                        CausesValidation="true" ValidationGroup="ADD" />
                                                </div>
                                            </td>
                                            <td class="field_title">SEARCH ON
                                            </td>
                                            <td class="field_input">
                                                <asp:DropDownList ID="ddlsearch" runat="server" AppendDataBoundItems="true"
                                                    Width="100" class="chosen-select" data-placeholder="Choose a Type">
                                                    <asp:ListItem Text="Select" Value="0" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Particulars" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Voucher Type" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Vch No." Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="CHEQUENO" Value="4"></asp:ListItem>
                                                    <asp:ListItem Text="Narration" Value="5"></asp:ListItem>
                                                    <asp:ListItem Text="Credit" Value="6"></asp:ListItem>
                                                    <asp:ListItem Text="Debit" Value="7"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td class="field_input" colspan="3" style="padding-left:10px;">
                                                <asp:TextBox ID="txtsearchBox" runat="server" Width="210" ValidationGroup="SEARCH" placeholder="If any...."></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RFV_txtsearchBox" runat="server" ErrorMessage="*"
                                                    ControlToValidate="txtsearchBox" ValidateEmptyText="false" SetFocusOnError="true" ValidationGroup="SEARCH" ForeColor="Red"> 
                                                </asp:RequiredFieldValidator>
                                            </td>
                                            <td class="field_input">
                                                <div class="btn_24_blue">
                                                    <span class="icon find_co"></span>
                                                    <asp:Button ID="searchButton" runat="server" Text="Search" OnClick="searchButton_Click"
                                                        CssClass="btn_link" ValidationGroup="SEARCH" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="gridcontent-inner">
                                        <div style="overflow: hidden;" id="DivHeaderRow">
                                        </div>
                                        <div id="DivMainContent" style="overflow: scroll; height: 330px;" onscroll="OnScrollDiv(this);">
                                            <asp:GridView ID="grdledger" EmptyDataText="There are no records available." CssClass="reportgrid"
                                                runat="server" AutoGenerateColumns="False" Width="100%" ShowFooter="false">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSelect" runat="server" onclick="javascript: calculateTotal(this);" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="AccEntryID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAcc" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "AccEntryID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Depot" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl6" runat="server" Text='<%# Bind("REGIONNAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVOUCHERDATE" runat="server" Text='<%# Bind("VOUCHERDATE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ledgerid" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblledgerid" runat="server" Text='<%# Bind("LEDGERID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Particulars" ItemStyle-Wrap="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLedgerName" runat="server" Text='<%# Bind("LedgerName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Voucher Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVoucherTypeNAME" runat="server" Text='<%# Bind("VoucherTypeNAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bank Date" ItemStyle-Wrap="false">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="grdtxtbankdate" AutoPostBack="false" runat="server" MaxLength="10" Text='<%# Bind("ChequeRealisedDate") %>'
                                                                Style="text-align: right;" Width="70px" Height="20" Enabled="true"></asp:TextBox>
                                                            <asp:ImageButton ID="imgPopupbankdate" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtenderimgPopupbankdate" PopupButtonID="imgPopupbankdate"
                                                                runat="server" TargetControlID="grdtxtbankdate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Vch No." HeaderStyle-Wrap="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVoucherNo" runat="server" Text='<%# Bind("VoucherNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Debit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDebit" runat="server" Text='<%# Bind("Debit") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Credit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCredit" runat="server" Text='<%# Bind("Credit") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Balance" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl78" runat="server" Text='<%# Bind("Balance") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl79" runat="server" Text='<%# Bind("Balance_DR_CR") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CHQ/NEFT/RTGS" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl791" runat="server" Text='<%# Bind("CHEQUENO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Narration">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNarration" runat="server" Text='<%# Bind("Narration") %>' Width="250"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div id="DivFooterRow" style="overflow: hidden;">
                                        </div>
                                    </div>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td class="field_input">
                                                <div class="btn_24_blue">
                                                    <span class="icon disk_co"></span>
                                                    <asp:Button ID="btnsave" runat="server" Text="SAVE RECONCILIATION" CssClass="btn_link" OnClick="btnsave_Click" />
                                                </div>
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
            <script language="javascript" type="text/javascript">
                function calculateTotal(control) {
                    var todaydate = new Date();
                    var day = todaydate.getDate();
                    var month = todaydate.getMonth() + 1;
                    var year = todaydate.getFullYear();

                    if (day < 10) {
                        day = '0' + day;
                    }
                    if (month < 10) {
                        month = '0' + month;
                    }
                    var datestring = day + "/" + month + "/" + year;
                    var grid = document.getElementById("<%= grdledger.ClientID%>");
                    var rowData = control.parentNode.parentNode;
                    var rowIndex = rowData.rowIndex;
                    var txtdate = $("input[id*=grdtxtbankdate]")
                    if (control.checked) {
                        if (txtdate[rowIndex - 1].value == null || txtdate[rowIndex - 1].value == '') {
                            txtdate[rowIndex - 1].value = datestring;
                        }
                    }
                    else {
                        if (txtdate[rowIndex - 1].value == datestring) {
                            txtdate[rowIndex - 1].value = '';
                        }
                    }
                }
            </script>
            <script language="javascript" type="text/javascript">
                function MakeStaticHeader(gridId, height, width, headerHeight, isFooter) {
                    debugger;
                    var tbl = document.getElementById(gridId);
                    if (tbl) {
                        var DivHR = document.getElementById('DivHeaderRow');
                        var DivMC = document.getElementById('DivMainContent');
                        var DivFR = document.getElementById('DivFooterRow');


                        //*** Set divheaderRow Properties ****
                        DivHR.style.height = headerHeight + 'px';
                        //DivHR.style.width = (parseInt(width) - 16) + 'px';
                        DivHR.style.width = '98%';
                        DivHR.style.position = 'relative';
                        DivHR.style.top = '0px';
                        DivHR.style.zIndex = '10';
                        DivHR.style.verticalAlign = 'top';

                        //*** Set divMainContent Properties ****
                        DivMC.style.width = width + 'px';
                        DivMC.style.height = height + 'px';
                        DivMC.style.position = 'relative';
                        DivMC.style.top = -headerHeight + 'px';
                        DivMC.style.zIndex = '1';

                        //*** Set divFooterRow Properties ****
                        DivFR.style.width = (parseInt(width) - 16) + 'px';
                        DivFR.style.position = 'relative';
                        DivFR.style.top = -headerHeight + 'px';
                        DivFR.style.verticalAlign = 'top';
                        DivFR.style.paddingtop = '2px';

                        if (isFooter) {
                            var tblfr = tbl.cloneNode(true);
                            tblfr.removeChild(tblfr.getElementsByTagName('tbody')[0]);
                            var tblBody = document.createElement('tbody');
                            tblfr.style.width = '100%';
                            tblfr.cellSpacing = "0";
                            tblfr.border = "0px";
                            tblfr.rules = "none";
                            //*****In the case of Footer Row *******
                            tblBody.appendChild(tbl.rows[tbl.rows.length - 1]);
                            tblfr.appendChild(tblBody);
                            DivFR.appendChild(tblfr);
                        }
                        //****Copy Header in divHeaderRow****
                        DivHR.appendChild(tbl.cloneNode(true));
                    }
                }

                function OnScrollDiv(Scrollablediv) {
                    document.getElementById('DivHeaderRow').scrollLeft = Scrollablediv.scrollLeft;
                    document.getElementById('DivFooterRow').scrollLeft = Scrollablediv.scrollLeft;
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>