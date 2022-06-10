<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true"
    CodeFile="frmRptVoucherReport.aspx.cs" Inherits="VIEW_frmRptVoucherReport" %>

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
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <Triggers>           
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
        <ContentTemplate>
            <%--<div class="page_title">
                <span class="title_icon"><span class="computer_imac"></span></span>
                <h3>Beat Master</h3>
            </div>--%>
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>
                                    Voucher Report<%--&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<font size="14" color="yellow">(REPORT DATA AVAILABLE LATEST BY LAST 11:59 pm THAN TODAY )</font>--%>
                                </h6>
                                <div class="btn_30_light" style="float: right;" id="btnaddhide" runat="server">
                                                                <span class="icon doc_excel_table_co"></span><b>
                                                               <a href="#" onclick="exportToExcel();">
                                                                     <asp:Button ID="btnExport" runat="server"  CssClass="btn_link" Text="Export To Excel" OnClick = "ExportToExcel" /></a>
                                                                     </b>
                                                            </div>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlDisplay" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td>
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="70" class="field_title">
                                                            Voucher&nbsp;<span class="req">*</span>
                                                        </td>
                                                        <td width="100" class="field_input">
                                                            <asp:ListBox ID="ddlvouchertype" runat="server" SelectionMode="Multiple" TabIndex="4"
                                                                Width="100" AppendDataBoundItems="True" ValidationGroup="Show" OnSelectedIndexChanged="ddlvouchertype_SelectedIndexChanged"
                                                                AutoPostBack="true"></asp:ListBox>
                                                            <asp:CustomValidator ID="CustomValidator1" runat="server" ValidateEmptyText="true"
                                                                ControlToValidate="ddlvouchertype" ValidationGroup="Show" ErrorMessage="*" Display="Dynamic"
                                                                ClientValidationFunction="ValidateListBox" ForeColor="Red"></asp:CustomValidator>
                                                        </td>
                                                         <td width="60" class="field_title" id="trpaymentmodelbl" runat="server">
                                                            PAYMENT MODE&nbsp;<span class="req">*</span>
                                                        </td>
                                                        <td width="60" class="field_input" id="trddlpaymentmode" runat="server">
                                                            <asp:DropDownList ID="ddlMode" Width="60" runat="server" class="chosen-select">
                                                                <asp:ListItem Value="S" Text="ALL" />
                                                                <asp:ListItem Value="B" Text="Bank" />
                                                                <asp:ListItem Value="C" Text="Cash" />
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="55" class="field_title">
                                                            Region&nbsp;<span class="req">*</span>
                                                        </td>
                                                        <td width="100" class="field_input">
                                                            <asp:ListBox ID="ddlregion" runat="server" SelectionMode="Multiple" TabIndex="4"
                                                                Width="100" AppendDataBoundItems="True" ValidationGroup="Show"></asp:ListBox>
                                                            <asp:CustomValidator ID="CustomValidator2" runat="server" ValidateEmptyText="true"
                                                                ControlToValidate="ddlregion" ValidationGroup="Show" ErrorMessage="Required!"
                                                                Display="Dynamic" ClientValidationFunction="ValidateListBox" ForeColor="Red"></asp:CustomValidator>
                                                        </td>
                                                       
                                                        <td width="50" class="field_title" id="td_lblmode" runat="server">
                                                            View&nbsp;<span class="req">*</span>
                                                        </td>
                                                        <td width="118" class="field_input" id="td_ddlmode" runat="server">
                                                            <asp:DropDownList ID="ddlviewmode" Width="115" runat="server" class="chosen-select">
                                                                <asp:ListItem Value="A" Text="Header" />
                                                                <asp:ListItem Value="B" Text="Header+Details" Selected="True" />
                                                                <asp:ListItem Value="C" Text="Header+Details+ Cost Center+Invoice" />
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="40" class="field_title">
                                                            <asp:Label ID="Label1" Text="From" runat="server"></asp:Label>
                                                        </td>
                                                        <td width="100" class="field_input">
                                                            <asp:TextBox ID="txtfromdate" runat="server" Enabled="false" Width="60" placeholder="dd/MM/yyyy" 
                                                                MaxLength="10"></asp:TextBox>
                                                            <asp:ImageButton ID="Imgfrom" runat="server" ImageUrl="~/images/calendar.png" Width="24px"
                                                                Height="24px" ImageAlign="AbsMiddle" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="Imgfrom" runat="server"
                                                                TargetControlID="txtfromdate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                        <td width="20" class="field_title">
                                                            <asp:Label ID="Label2" Text="To" runat="server"></asp:Label>
                                                        </td>
                                                        <td width="100" class="field_input">
                                                            <asp:TextBox ID="txttodate" runat="server" Enabled="false" Width="60" placeholder="dd/MM/yyyy" 
                                                                MaxLength="10"></asp:TextBox>
                                                            <asp:ImageButton ID="ImgToDate" runat="server" ImageUrl="~/images/calendar.png" Width="24px"
                                                                Height="24px" ImageAlign="AbsMiddle" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImgToDate" runat="server"
                                                                TargetControlID="txttodate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                        <td width="90" class="field_title">
                                                            <div class="btn_24_blue">
                                                                <span class="icon exclamation_co"></span>
                                                                <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn_link" OnClick="btnShow_Click"
                                                                    ValidationGroup="Show" />
                                                            </div>
                                                        </td>
                                                       <td width="150" class="field_input">
                                                         
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <ul id="search_box">
                                        <li></li>
                                        <li></li>
                                    </ul>
                                    <div>
                                        <div style="overflow: hidden; width: 100%;" id="DivHeaderRow">
                                        </div>
                                        <div style="overflow: scroll; border: 1px solid #c3c9ce; margin-bottom: 8px;" onscroll="OnScrollDiv(this)"
                                            id="DivMainContent">
                                            <asp:GridView ID="GV_VOUCHER" runat="server" AutoGenerateColumns="false" Width="100%"
                                                DataKeyNames="AccEntryID" CssClass="reportgrid" GridLines="Horizontal" CellPadding="0"
                                                CellSpacing="0" OnRowDataBound="GV_VOUCHER_OnRowDataBound" Visible="true" EmptyDataText="No Records Available..."
                                                BorderWidth="0px">
                                                <Columns>
                                                    <asp:BoundField DataField="slno" HeaderText="SL" HeaderStyle-HorizontalAlign="Left" />
                                                    <asp:TemplateField ItemStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <a href="JavaScript:divexpandcollapse('div<%# Eval("AccEntryID") %>');">
                                                                <img id="imgdiv<%# Eval("AccEntryID") %>" width="15px" border="0" src="../images/detail.gif"
                                                                    alt="" /></a>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="20px" VerticalAlign="Middle"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="AccEntryID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAccEntryID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "AccEntryID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="TYPE" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltype" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "VoucherTypeID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="VoucherTypeName" HeaderText="Vch.Type" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-Wrap="false" />
                                                    <asp:BoundField DataField="VoucherNo" HeaderText="Voucher No" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-Wrap="false" />
                                                    <asp:BoundField DataField="Date" HeaderText="Date" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-Wrap="false" />
                                                    <asp:BoundField DataField="party" HeaderText="Party" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-Width="20%" ItemStyle-Wrap="false" />
                                                    <asp:BoundField DataField="Amount" HeaderText="Amount" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false" />
                                                    <asp:BoundField DataField="Mode" HeaderText="Mode" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-Wrap="false" />
                                                    <asp:BoundField DataField="BranchName" HeaderText="Region" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-Width="100px" ItemStyle-Wrap="false" />
                                                    <asp:BoundField DataField="Approved" HeaderText="Approved" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                        ItemStyle-Width="100px" ItemStyle-Wrap="false" />
                                                    <asp:BoundField DataField="ApproveDate" HeaderText="ApprovedDate" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-Width="100px" />
                                                    <asp:BoundField DataField="Narration" HeaderText="Narration" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-Width="350px" ItemStyle-Wrap="true" />
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td colspan="100%">
                                                                    <div id="div<%# Eval("AccEntryID") %>" style="overflow: auto; display: none; position: relative;
                                                                        left: 15px; overflow: auto">
                                                                        <asp:GridView ID="GV_LEDGER" runat="server" Width="60%" AutoGenerateColumns="false"
                                                                            DataKeyNames="LedgerId" ShowFooter="true" CssClass="reportgridchild" GridLines="Horizontal"
                                                                            CellPadding="0" CellSpacing="0"  OnRowDataBound="GV_LEDGER_OnRowDataBound">
                                                                            <FooterStyle Font-Bold="True" ForeColor="White" />
                                                                            <Columns>
                                                                                <asp:TemplateField ItemStyle-Width="20px">
                                                                                    <ItemTemplate>
                                                                                        <a href="JavaScript:divexpandcollapse('div1<%# Eval("LedgerId") %>');">
                                                                                            <img id="imgdiv1<%# Eval("LedgerId") %>" width="15px" border="0" src="../images/detail.gif"
                                                                                                alt="" /></a>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="20px" VerticalAlign="Middle"></ItemStyle>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="LedgerId" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="LblLedgerId" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "LedgerId") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="AccEntryID" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblEntryID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "AccEntryID") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="SL" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="20"
                                                                                    ItemStyle-Wrap="false">
                                                                                    <ItemTemplate>
                                                                                        <%# Container.DataItemIndex + 1 %>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Ledger" ItemStyle-Width="400" ItemStyle-Wrap="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="LedgerName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "LedgerName") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Type" ItemStyle-Width="40" ItemStyle-Wrap="false"
                                                                                    Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="TxnType" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TxnType") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="DebitAmount" HeaderText="Debit" HeaderStyle-HorizontalAlign="Left"
                                                                                    DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="80"
                                                                                    ItemStyle-Wrap="false" />
                                                                                <asp:BoundField DataField="CreditAmount" HeaderText="Credit" HeaderStyle-HorizontalAlign="Left"
                                                                                    DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="80"
                                                                                    ItemStyle-Wrap="false" />
                                                                                <asp:BoundField DataField="ChequeNo" HeaderText="Cheque No" HeaderStyle-HorizontalAlign="Left"
                                                                                    ItemStyle-Width="70" ItemStyle-Wrap="false" />
                                                                                <asp:BoundField DataField="ChequeDate" HeaderText="Cheque Date" HeaderStyle-HorizontalAlign="Left"
                                                                                    ItemStyle-Width="70" ItemStyle-Wrap="false" />
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <td colspan="100%">
                                                                                                <div id="div1<%# Eval("LedgerId") %>" style="overflow: auto; display: none; position: relative;
                                                                                                    left: 15px;">
                                                                                                    <asp:GridView ID="GV_INVOICE" runat="server" Width="70%" AutoGenerateColumns="false"
                                                                                                        DataKeyNames="LedgerId" ShowFooter="true" CssClass="reportgridchild" GridLines="Horizontal"
                                                                                                        CellPadding="0" CellSpacing="0" >
                                                                                                        <FooterStyle Font-Bold="True" ForeColor="Black" />
                                                                                                        <Columns>
                                                                                                            <asp:TemplateField HeaderText="InvoiceID" Visible="false">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lblRetailerID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "InvoiceID") %>'></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="SL" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="5"
                                                                                                                ItemStyle-Wrap="false">
                                                                                                                <ItemTemplate>
                                                                                                                    <%# Container.DataItemIndex + 1 %>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:BoundField DataField="InvoiceNo" HeaderText="Invoice No" ItemStyle-Height="16px"
                                                                                                                HeaderStyle-HorizontalAlign="Left" DataFormatString="{0:N2}" ItemStyle-Width="40"
                                                                                                                ItemStyle-Wrap="false" />
                                                                                                            <asp:BoundField DataField="InvoiceAmt" HeaderText="Invoice Amt" ItemStyle-Height="16px"
                                                                                                                HeaderStyle-HorizontalAlign="Left" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right"
                                                                                                                ItemStyle-Width="40" ItemStyle-Wrap="false" />
                                                                                                            <asp:BoundField DataField="AmtReceived" HeaderText="Amt Received" ItemStyle-Height="16px"
                                                                                                                HeaderStyle-HorizontalAlign="Left" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right"
                                                                                                                ItemStyle-Width="40" ItemStyle-Wrap="false" />
                                                                                                            <asp:BoundField DataField="OutStanding" HeaderText="OutStanding" ItemStyle-Height="16px"
                                                                                                                HeaderStyle-HorizontalAlign="Left" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right"
                                                                                                                ItemStyle-Width="40" ItemStyle-Wrap="false" />
                                                                                                        </Columns>
                                                                                                    </asp:GridView>
                                                                                                    <asp:GridView ID="GV_COSTCENTRE" runat="server" Width="95%" AutoGenerateColumns="false" RowStyle-Height="27px"
                                                                                                        DataKeyNames="LedgerID" CssClass="reportgridchild" GridLines="Horizontal" CellPadding="0" ShowFooter="true"
                                                                                                        CellSpacing="0">
                                                                                                        <Columns>
                                                                                                            <asp:TemplateField HeaderText="LedgerID" Visible="false">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lblCostCentreID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "LedgerID") %>'></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="SL" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="10"
                                                                                                                ItemStyle-Wrap="false">
                                                                                                                <ItemTemplate>
                                                                                                                    <%# Container.DataItemIndex + 1 %>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:BoundField DataField="CostCatagoryID" HeaderText="CostCatagoryID" Visible="false"
                                                                                                                HeaderStyle-HorizontalAlign="Left" />
                                                                                                            <asp:BoundField DataField="CostCatagoryName" HeaderText="Cost Catagory" HeaderStyle-HorizontalAlign="Left" />
                                                                                                            <asp:BoundField DataField="CostCenterID" HeaderText="CostCenterID" Visible="false"
                                                                                                                HeaderStyle-HorizontalAlign="Left" />
                                                                                                            <asp:BoundField DataField="CostCenterName" HeaderText="CostCenter" HeaderStyle-HorizontalAlign="Left" />
                                                                                                            <asp:BoundField DataField="amount" HeaderText="Amount" HeaderStyle-HorizontalAlign="Left" />
                                                                                                            <asp:BoundField DataField="TxnType" HeaderText="Type" HeaderStyle-HorizontalAlign="Left" />
                                                                                                        </Columns>
                                                                                                    </asp:GridView>
                                                                                                </div>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                             <asp:GridView ID="gv_for_excel" runat="server" AutoGenerateColumns="true"
                                            OnRowDataBound="GV_LEDGER_OnRowDataBound" Width="100%" ShowFooter="false" Visible="false"
                                            ShowHeader="true" GridLines="Horizontal" CssClass="reportgrid">
                                            <FooterStyle Font-Bold="True" ForeColor="White" Height="20px" />
                                        </asp:GridView>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                    <cc1:MessageBox ID="MessageBox1" runat="server" />
                    <span class="clear"></span>
                </div>
                <span class="clear"></span>
            </div>
            <link href="../css/bootstrap-multiselect.css" rel="stylesheet" />
            <script type="text/javascript" src="../js/bootstrap.js"></script>
            <script type="text/javascript" src="../js/bootstrap-multiselect.js"></script>
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
                function ValidateListBox(sender, args) {
                    var options = document.getElementById("<%=ddlvouchertype.ClientID%>").options;
                    for (var i = 0; i < options.length; i++) {
                        if (options[i].selected == true) {
                            args.IsValid = true;
                            return;
                        }
                    }
                    args.IsValid = false;
                }

                function ValidateListBox(sender, args) {
                    var options = document.getElementById("<%=ddlregion.ClientID%>").options;
                    for (var i = 0; i < options.length; i++) {
                        if (options[i].selected == true) {
                            args.IsValid = true;
                            return;
                        }
                    }
                    args.IsValid = false;
                }
            </script>
            <script type="text/javascript">
                $(function () {
                    $('#ContentPlaceHolder1_ddlregion').multiselect({
                        includeSelectAllOption: true
                    });
                });

                $(function () {
                    $('#ContentPlaceHolder1_ddlvouchertype').multiselect({
                        includeSelectAllOption: true
                    });
                });
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
            <script language="javascript" type="text/javascript">
                function divexpandcollapse(divname) {
                    var div = document.getElementById(divname);
                    var img = document.getElementById('img' + divname);
                    if (div.style.display == "none") {
                        div.style.display = "inline";
                        img.src = "../images/close.gif";
                    } else {
                        div.style.display = "none";
                        img.src = "../images/detail.gif";
                    }
                }

                function divexpandcollapseChild(divname) {
                    var div1 = document.getElementById(divname);
                    var div2 = document.getElementById(divname);
                    var img = document.getElementById('img' + divname);
                    if (div1.style.display == "none") {
                        div1.style.display = "inline";
                        img.src = "../images/close.gif";
                    } else {
                        div1.style.display = "none";
                        img.src = "../images/detail.gif"; ;
                    }
                    if (div2.style.display == "none") {
                        div2.style.display = "inline";
                        img.src = "../images/close.gif";
                    } else {
                        div2.style.display = "none";
                        img.src = "../images/detail.gif"; ;
                    }
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
