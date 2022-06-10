<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmRptLedger_Reports.aspx.cs" Inherits="VIEW_frmRptLedger_Reports" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
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
                                    Ledger Report
                                </h6>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlDisplay" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td>
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td class="field_title" width="60">
                                                            <asp:Label ID="Label4" runat="server" Text="Calender"></asp:Label>
                                                        </td>
                                                        <td class="field_input" width="90">
                                                            <asp:DropDownList ID="ddlcalender" runat="server" AppendDataBoundItems="True" Width="90"
                                                                AutoPostBack="true" Height="28" class="chosen-select" data-placeholder="Choose"
                                                                OnSelectedIndexChanged="ddlcalender_SelectedIndexChanged">
                                                                <asp:ListItem Selected="True" Text="SELECT" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="YEARLY" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="QUARTERLY" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="MONTHLY" Value="3"></asp:ListItem>
                                                                <asp:ListItem Text="WEEKLY" Value="4"></asp:ListItem>
                                                                <asp:ListItem Text="JC" Value="5"></asp:ListItem>
                                                                <asp:ListItem Text="PERIOD" Value="6"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td class="field_title" width="50">
                                                            <asp:Label ID="lblcalender" runat="server" Text="Year"></asp:Label>
                                                        </td>
                                                        <td class="field_input" width="100">
                                                            <asp:ListBox ID="ddlspan" runat="server" SelectionMode="Multiple" TabIndex="4" OnSelectedIndexChanged="ddlspan_SelectedIndexChanged"
                                                                Width="100" AppendDataBoundItems="True" ValidationGroup="ADD" AutoPostBack="true">
                                                            </asp:ListBox>
                                                        </td>
                                                        <td width="40" class="field_title">
                                                            <asp:Label ID="Label1" Text="From" runat="server"></asp:Label>
                                                        </td>
                                                        <td width="110" class="field_input">
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
                                                        <td width="130" class="field_input">
                                                            <asp:TextBox ID="txttodate" runat="server" Enabled="false" Width="60" placeholder="dd/MM/yyyy" 
                                                                MaxLength="10"></asp:TextBox>
                                                            <asp:ImageButton ID="ImgToDate" runat="server" ImageUrl="~/images/calendar.png" Width="24px"
                                                                Height="24px" ImageAlign="AbsMiddle" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImgToDate" runat="server"
                                                                TargetControlID="txttodate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                        <td width="50" class="field_title">
                                                            <asp:Label ID="Label6" Text="Region" runat="server"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="90" class="field_input">
                                                            <asp:ListBox ID="ddldepot" runat="server" SelectionMode="Multiple" TabIndex="4" AppendDataBoundItems="True"
                                                                ValidationGroup="Show"  OnSelectedIndexChanged="ddldepot_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                                                            <asp:CustomValidator ID="CustomValidator12" runat="server" ValidateEmptyText="true"
                                                                ControlToValidate="ddldepot" ValidationGroup="Show" ErrorMessage="*" Font-Bold="true"
                                                                Display="Dynamic" ClientValidationFunction="ValidateListBoxdepot" ForeColor="Red"></asp:CustomValidator>
                                                        </td>
                                                        <td width="50" class="field_title" id="td_lblmode" runat="server">
                                                            View&nbsp;<span class="req">*</span>
                                                        </td>
                                                        <td width="100" class="field_input" id="td_ddlmode" runat="server">
                                                            <asp:DropDownList ID="ddlviewmode" Width="100" runat="server" class="chosen-select">
                                                                <asp:ListItem Value="A" Text="Header" Selected="True" />
                                                                <asp:ListItem Value="B" Text="Header+Details" />
                                                                <asp:ListItem Value="C" Text="Header+Details+Cost Center" />
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="50" class="field_title">
                                                            <asp:Label ID="Label3" Text="Ledger" runat="server"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td class="field_input" colspan="5">
                                                            <asp:DropDownList ID="ddlledger" runat="server" AppendDataBoundItems="true" Width="400"
                                                                Height="28" class="chosen-select" data-placeholder="Choose a Type" ValidationGroup="Show">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="Show"
                                                                ForeColor="Red" ErrorMessage="*" ControlToValidate="ddlledger" ValidateEmptyText="false"
                                                                Font-Bold="true" SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td class="field_input">
                                                            <div class="btn_24_blue">
                                                                <span class="icon exclamation_co"></span>
                                                                <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn_link" OnClick="btnShow_Click"
                                                                    ValidationGroup="Show" />
                                                            </div>
                                                        </td>
                                                        <td class="field_input" style="padding-left: 10px;" colspan="4">
                                                            <div class="btn_24_blue">
                                                                <span class="icon doc_excel_table_co"></span>
                                                                <asp:Button ID="btnExport" runat="server" CssClass="btn_link" Text="Export To Excel"
                                                                    OnClick="ExportToExcel" />
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="gridcontent">
                                        <div style="overflow: hidden; width: 100%;" id="DivHeaderRow">
                                        </div>
                                        <div style="overflow: scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent">
                                            <asp:GridView ID="grdledger" runat="server" AutoGenerateColumns="false"
                                                DataKeyNames="AccEntryID" OnRowDataBound="grdledger_RowDataBound" Width="100%" OnRowCommand="grdledger_RowCommand"
                                                ShowFooter="false" Visible="true" ShowHeader="true" GridLines="Horizontal" CssClass="reportgrid">
                                                <FooterStyle Font-Bold="True" ForeColor="White" Height="20px" />
                                                <Columns>
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
                                                            <asp:Label ID="lblAcc" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "AccEntryID") %>'></asp:Label>
                                                            <%--<asp:Label ID="lblAcc" runat="server" Text='<%# Bind("AccEntryID") %>' Width="225px" Visible="false"></asp:Label>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Depot" ItemStyle-Wrap="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl6" runat="server" Text='<%# Bind("REGIONNAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date" ItemStyle-Wrap="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl1" runat="server" Text='<%# Bind("VOUCHERDATE") %>' Width="75px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Particulars" ItemStyle-Wrap="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl2" runat="server" Text='<%# Bind("LedgerName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Vch.Type" ItemStyle-Wrap="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl3" runat="server" Text='<%# Bind("VoucherTypeNAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Vch.No." ItemStyle-Wrap="false">
                                                        <ItemTemplate>
                                                            <%--<asp:Label ID="lbl4" runat="server" Text='<%# Bind("VoucherNo") %>'></asp:Label>--%>                                                           
                                                            <asp:LinkButton ID="lnkvoucherno" runat="server" ForeColor="Blue" CommandName="VoucherNo"
                                                                CommandArgument='<%#Eval("AccEntryID")+","+Eval("DOCNO") %>'
                                                                Text='<%# Bind("VoucherNo") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Doc.No." ItemStyle-Wrap="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl94" runat="server" Text='<%# Bind("DOCNO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Debit" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl77" runat="server" Text='<%# Bind("Debit") %>' Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Credit" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl7" runat="server" Text='<%# Bind("Credit") %>' Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Balance" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl78" runat="server" Text='<%# Bind("Balance") %>' Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl79" runat="server" Text='<%# Bind("Balance_DR_CR") %>' Width="25px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Narration" ItemStyle-Wrap="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl72" runat="server" Text='<%# Bind("Narration") %>' Width="400"></asp:Label>
                                                        </ItemTemplate>
                                                       
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-Width="20px" HeaderText="ALL">
                                                        <ItemTemplate>
                                                            <a href="javascript: //" class="filter_btn printer_co" id="btnPrint_<%# Eval("AccEntryID") %>"
                                                            onclick="CallServerMethodPrint(this)">
                                                                <img id="imgdiv<%# Eval("AccEntryID") %>" width="15px" border="0" src="../images/print.gif"
                                                                    alt="" />
                                                            </a>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="20px" VerticalAlign="Middle"></ItemStyle>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField ItemStyle-Width="20px" HeaderText="Voucher Print">
                                                        <ItemTemplate>
                                                            <a href="javascript: //" class="filter_btn printer_co" id="btnaccPrint_<%# Eval("AccEntryID") %>"
                                                            onclick="CallServerMethodaccPrint(this)">
                                                                <img id="accimgdiv<%# Eval("AccEntryID") %>" width="15px" border="0" src="../images/print.gif"
                                                                    alt="" />
                                                            </a>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="20px" VerticalAlign="Middle"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td >&nbsp;</td>
                                                                <td >&nbsp;</td>
                                                                <td colspan="12">
                                                                    <div id="div<%# Eval("AccEntryID") %>" style="overflow: auto; display: none; position: relative;
                                                                        left: 15px; overflow: auto">
                                                                        <asp:GridView ID="GV_LEDGER" runat="server" Width="40%" AutoGenerateColumns="false"
                                                                            CssClass="reportgridchild" ShowFooter="false" Visible="true" ShowHeader="true" GridLines="Horizontal"
                                                                            RowStyle-ForeColor="#9ba3d1" AlternatingRowStyle-BackColor="ControlLight" BorderColor="Black"
                                                                            BorderWidth="1px" RowStyle-BorderColor="Black" RowStyle-BorderWidth="1px" RowStyle-BorderStyle="Solid"
                                                                            OnRowDataBound="GV_LEDGER_OnRowDataBound" BackColor="White" AlternatingRowStyle-ForeColor="Blue"
                                                                            >
                                                                            <FooterStyle Font-Bold="True" ForeColor="White" Height="20px" />
                                                                            <RowStyle Height="20px" />
                                                                            <Columns>
                                                                             <asp:TemplateField ItemStyle-Width="20px">
                                                                                    <ItemTemplate>
                                                                                        <a href="JavaScript:divexpandcollapse('div1<%# Eval("LedgerId1") %>');">
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
                                                                                <asp:TemplateField HeaderText="Particulars" ItemStyle-Wrap="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl15" runat="server" Text='<%# Bind("NAME") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Debit" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl16" runat="server" Text='<%# Bind("DEBIT") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Credit" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl17" runat="server" Text='<%# Bind("CREDIT") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>                                                                               
                                                                                 <asp:TemplateField HeaderText="AccEntryID" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblEntryID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "AccEntryID") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            
                                                                                            <td colspan="1">
                                                                                                <div id="div1<%# Eval("LedgerId1") %>" style="overflow: auto; display: none; position: relative;
                                                                                                    left: 15px;">
                                                                                                   <%-- <asp:GridView ID="GV_INVOICE" runat="server" Width="70%" AutoGenerateColumns="false"
                                                                                                        DataKeyNames="LedgerId1" ShowFooter="true" CssClass="reportgridchild" GridLines="Horizontal"
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
                                                                                                    </asp:GridView>--%>
                                                                                                    <asp:GridView ID="GV_COSTCENTRE" runat="server" Width="95%" AutoGenerateColumns="false" RowStyle-Height="27px"
                                                                                                        DataKeyNames="LedgerID" CssClass="reportgridchild" GridLines="Horizontal" CellPadding="0" ShowFooter="true"
                                                                                                        CellSpacing="0">
                                                                                                        <Columns>
                                                                                                            <asp:TemplateField HeaderText="LedgerID" Visible="false">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lblCostCentreID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "LedgerID") %>'></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                             <asp:TemplateField HeaderText="AccEntryID" Visible="false">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lblAccEntryIDD" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "AccEntryID") %>'></asp:Label>
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
                                                <%--<HeaderStyle BackColor="Silver" Font-Bold="True" ForeColor="#3399FF" />--%>
                                            </asp:GridView>
                                        </div>
                                        <%--<div id="DivFooterRow" style="overflow:hidden;">
                                                    </div>--%>

                                           <asp:GridView ID="gv_for_excel" runat="server" AutoGenerateColumns="true"
                                            OnRowDataBound="grdledger_RowDataBound" Width="100%" ShowFooter="false" Visible="false"
                                            ShowHeader="true" GridLines="Horizontal" CssClass="reportgrid">
                                            <FooterStyle Font-Bold="True" ForeColor="White" Height="20px" />                                             
                                        </asp:GridView>
                                        <asp:GridView ID="gv_for_excel_details" runat="server" AutoGenerateColumns="false"
                                            OnRowDataBound="grdledger_RowDataBound" Width="100%" ShowFooter="false" Visible="false"
                                            ShowHeader="true" GridLines="Horizontal" CssClass="reportgrid">
                                            <FooterStyle Font-Bold="True" ForeColor="White" Height="20px" />
                                               <Columns>
                                                    <asp:TemplateField HeaderText="AccEntryID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAcc" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "AccEntryID") %>'></asp:Label>
                                                            <%--<asp:Label ID="lblAcc" runat="server" Text='<%# Bind("AccEntryID") %>' Width="225px" Visible="false"></asp:Label>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                   
                                                    <asp:TemplateField HeaderText="Depot" ItemStyle-Wrap="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl6" runat="server" Text='<%# Bind("REGIONNAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date" ItemStyle-Wrap="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl1" runat="server" Text='<%# Bind("VOUCHERDATE") %>' Width="75px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Particulars" ItemStyle-Wrap="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl2" runat="server" Text='<%# Bind("LedgerName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Vch.Type" ItemStyle-Wrap="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl3" runat="server" Text='<%# Bind("VoucherTypeNAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Vch.No." ItemStyle-Wrap="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl4" runat="server" Text='<%# Bind("VoucherNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Debit" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl77" runat="server" Text='<%# Bind("Debit") %>' Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Credit" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl7" runat="server" Text='<%# Bind("Credit") %>' Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Balance" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl78" runat="server" Text='<%# Bind("Balance") %>' Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl79" runat="server" Text='<%# Bind("Balance_DR_CR") %>' Width="25px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Narration" ItemStyle-Wrap="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl72" runat="server" Text='<%# Bind("Narration") %>' Width="400"></asp:Label>
                                                        </ItemTemplate>                                                       
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td colspan="100%">
                                                                
                                                                        <asp:GridView ID="GV_LEDGER" runat="server" Width="40%" AutoGenerateColumns="false"
                                                                            CssClass="reportgrid" ShowFooter="false" Visible="true" ShowHeader="true" GridLines="Horizontal"
                                                                            RowStyle-ForeColor="#9ba3d1" AlternatingRowStyle-BackColor="ControlLight" BorderColor="Black"
                                                                            BorderWidth="1px" RowStyle-BorderColor="Black" RowStyle-BorderWidth="1px" RowStyle-BorderStyle="Solid"
                                                                            OnRowDataBound="GV_LEDGER_OnRowDataBound" BackColor="White" AlternatingRowStyle-ForeColor="Blue"
                                                                            >
                                                                            <FooterStyle Font-Bold="True" ForeColor="White" Height="20px" />
                                                                            <RowStyle Height="20px" />
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Particulars" ItemStyle-Wrap="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl15" runat="server" Text='<%# Bind("NAME") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Debit" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl16" runat="server" Text='<%# Bind("DEBIT") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Credit" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl17" runat="server" Text='<%# Bind("CREDIT") %>'></asp:Label>
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
                                        <asp:HiddenField runat="server" ID="Hdn_Print" />
                                         <asp:Button ID="btnPrint" runat="server" Text="Print" Style="display: none" OnClick="btnPrint_Click"
                                            CausesValidation="false" />
                                             <asp:Button ID="btnaccPrint" runat="server" Text="Print" Style="display: none" OnClick="btnaccPrint_Click"
                                            CausesValidation="false" />
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
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
                        DivHR.style.width = (parseInt(width) - 16) + 'px';
                        //DivHR.style.width = '98%';
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

                }

            </script>
            <script type="text/javascript">
                function ValidateListBoxdepot(sender, args) {
                    var options = document.getElementById("<%=ddldepot.ClientID%>").options;
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
                    $('#ContentPlaceHolder1_ddldepot').multiselect({
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
                $(function () {
                    $('#ContentPlaceHolder1_ddldepot').multiselect({
                        includeSelectAllOption: true
                    });
                    //            $("#ContentPlaceHolder1_ddldepot").multiselect('selectAll', false);
                    //            $("#ContentPlaceHolder1_ddldepot").multiselect('updateButtonText');
                });

                $(function () {
                    $('#ContentPlaceHolder1_ddlspan').multiselect({
                        includeSelectAllOption: true
                        //            $("#ContentPlaceHolder1_ddldepot").multiselect('selectAll', false);
                        //            $("#ContentPlaceHolder1_ddldepot").multiselect('updateButtonText');
                    });

                });
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

            function CallServerMethodPrint(oLink) {
            var iRecordIndex = oLink.id.toString().replace("btnPrint_", "");
            document.getElementById("<%=Hdn_Print.ClientID %>").value = iRecordIndex;
            document.getElementById("<%=btnPrint.ClientID %>").click();

        }

        function CallServerMethodaccPrint(oLink) {
            var iRecordIndex = oLink.id.toString().replace("btnaccPrint_", "");
            document.getElementById("<%=Hdn_Print.ClientID %>").value = iRecordIndex;
            document.getElementById("<%=btnaccPrint.ClientID %>").click();

        }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>