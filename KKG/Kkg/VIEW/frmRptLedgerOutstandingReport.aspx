<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true"
    CodeFile="frmRptLedgerOutstandingReport.aspx.cs" Inherits="VIEW_frmRptLedgerOutstandingReport" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/bootstrap-multiselect.css" rel="stylesheet" />
    <script type="text/javascript" src="../js/bootstrap.js"></script>
    <script type="text/javascript" src="../js/bootstrap-multiselect.js"></script>
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
    <script type="text/javascript">
        function exportToExcel() {
            grdinwardsupply.exportToExcel();
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
    <asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
            <asp:Image ID="Image1" ImageUrl="../images/loading123.gif" AlternateText="Processing"
                runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
        PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
        <ContentTemplate>
            <div id="content">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active">
                                <span class="h_icon_he list_images"></span>
                                <h6>Ledger Outstanding Report</h6>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="InputTable" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px;">
                                                <fieldset>
                                                    <legend>Ledger Outstanding Report</legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td width="110" class="field_title">
                                                                <asp:Label ID="Label1" Text="From Date" runat="server"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td width="200" class="field_input">
                                                                <asp:TextBox ID="txtfromdate" runat="server" Enabled="false" Width="120" placeholder="dd/MM/yyyy" 
                                                                    MaxLength="10"></asp:TextBox>
                                                                <asp:ImageButton ID="Imgfrom" runat="server" ImageUrl="~/images/calendar.png" Width="24px"
                                                                    Height="24px" ImageAlign="AbsMiddle" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="Imgfrom" runat="server"
                                                                    TargetControlID="txtfromdate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>
                                                            <td width="110" class="field_title">
                                                                <asp:Label ID="Label2" Text="To Date" runat="server"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td  class="field_input">
                                                                <asp:TextBox ID="txttodate" runat="server" Enabled="false" Width="120" placeholder="dd/MM/yyyy" 
                                                                    MaxLength="10"></asp:TextBox>
                                                                <asp:ImageButton ID="ImgToDate" runat="server" ImageUrl="~/images/calendar.png" Width="24px"
                                                                    Height="24px" ImageAlign="AbsMiddle" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImgToDate" runat="server"
                                                                    TargetControlID="txttodate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field_title">
                                                                <asp:Label ID="Label4" Text="Depot" runat="server"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td  class="field_input">
                                                                <asp:DropDownList ID="ddldepot" runat="server" AppendDataBoundItems="True" class="chosen-select"
                                                                    ValidationGroup="Show" OnSelectedIndexChanged="ddldepot_SelectedIndexChanged" AutoPostBack="true">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorddlbranch" runat="server" ErrorMessage="* Required!"
                                                                    ValidationGroup="Show" Font-Bold="true" ForeColor="Red" ControlToValidate="ddldepot"
                                                                    SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
                                                            </td>

                                                           <td class="field_title" >
                                                                <asp:Label ID="Label3" runat="server" Text="Customer"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:DropDownList ID="ddlCustomer" runat="server" class="chosen-select" ValidationGroup="Show"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rvddlcustomer" runat="server" ErrorMessage="* Required!"
                                                                    ValidationGroup="Show" Font-Bold="true" ForeColor="Red" ControlToValidate="ddlCustomer"
                                                                    SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>

                                                        <tr>

                                                            <td class="field_input" width="110">
                                                                <div class="btn_24_blue">
                                                                    <span class="icon exclamation_co"></span>
                                                                    <asp:Button ID="btnshow" runat="server" ValidationGroup="Show" Text="Show" CssClass="btn_link"
                                                                        OnClick="btnshow_Click" />
                                                                </div>
                                                            </td>
                                                            <td class="field_input" style="padding-left: 10px;" colspan="3">
                                                                <div class="btn_24_blue">
                                                                    <span class="icon doc_excel_table_co"></span>
                                                                    <asp:Button ID="btnExport" runat="server" CssClass="btn_link" Text="Export To Excel"
                                                                        OnClick="ExportToExcel" />
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                    </table>
                                 
                                    <div class="gridcontent">
                                        <div style="overflow: hidden; width: 100%;" id="DivHeaderRow">
                                        </div>
                                        <div style="overflow: scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent">
                                            <asp:GridView ID="grdledger" runat="server" AutoGenerateColumns="false"
                                                DataKeyNames="InvoiceID" OnRowDataBound="grdledger_RowDataBound" Width="100%" RowStyle-Height="27px"
                                                ShowFooter="true" Visible="true" ShowHeader="true" GridLines="Horizontal" CssClass="reportgrid">
                                                <FooterStyle Font-Bold="True" ForeColor="White" Height="20px" />
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <a href="JavaScript:divexpandcollapse('div<%# Eval("InvoiceID") %>');">
                                                                <img id="imgdiv<%# Eval("InvoiceID") %>" width="15px" border="0" src="../images/detail.gif"
                                                                    alt="" /></a>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="20px" VerticalAlign="Middle"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="InvoiceID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAcc" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "InvoiceID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Customer" ItemStyle-Wrap="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl6" runat="server" Text='<%# Bind("name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Invoice No" ItemStyle-Wrap="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl1" runat="server" Text='<%# Bind("InvoiceNo") %>' Width="75px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Invoice Date" ItemStyle-Wrap="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl2" runat="server" Text='<%# Bind("INVOICEDATE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Invoice Amt" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl3" runat="server" Text='<%# Bind("InvoiceAmt") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Invoice Paid" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl4" runat="server" Text='<%# Bind("AlreadyAmtPaid") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remaining Amt" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl77" runat="server" Text='<%# Bind("RemainingAmt") %>' Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td colspan="100%">
                                                                    <div id="div<%# Eval("InvoiceID") %>" style="overflow: auto; display: none; position: relative; left: 15px; overflow: auto">
                                                                        <asp:GridView ID="GV_LEDGER" runat="server" Width="40%" AutoGenerateColumns="false"
                                                                            CssClass="reportgridchild" ShowFooter="false" Visible="true" ShowHeader="true" GridLines="Horizontal"
                                                                            RowStyle-ForeColor="#9ba3d1" AlternatingRowStyle-BackColor="ControlLight" BorderColor="Black"
                                                                            BorderWidth="1px" RowStyle-BorderColor="Black" RowStyle-BorderWidth="1px" RowStyle-BorderStyle="Solid"
                                                                            BackColor="White" AlternatingRowStyle-ForeColor="Blue">
                                                                            <FooterStyle Font-Bold="True" ForeColor="White" Height="20px" />
                                                                            <RowStyle Height="20px" />
                                                                            <Columns>
                                                                                <asp:TemplateField ItemStyle-Width="20px">
                                                                                    <ItemTemplate>
                                                                                        <a href="JavaScript:divexpandcollapse('div1<%# Eval("InvoiceID") %>');"></a>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="20px" VerticalAlign="Middle"></ItemStyle>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="InvoiceID" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="LblLedgerId" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "InvoiceID") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Payment Date" ItemStyle-Wrap="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl15" runat="server" Text='<%# Bind("PRINTDATE") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Voucher No" ItemStyle-Wrap="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl15" runat="server" Text='<%# Bind("VoucherNo") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Amount Paid" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl17" runat="server" Text='<%# Bind("AmtPaid") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="InvoiceID" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblEntryID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "InvoiceID") %>'></asp:Label>
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
                                        </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
