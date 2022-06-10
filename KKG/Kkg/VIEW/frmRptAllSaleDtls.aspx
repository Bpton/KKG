<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="frmRptAllSaleDtls.aspx.cs" Inherits="VIEW_frmRptAllSaleDtls" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
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

    <script language="javascript" type="text/javascript">
        function MakeStaticHeader(gridId, height, width, headerHeight, isFooter) {
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
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>All Sale Details
                                </h6>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlDisplay" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_title">
                                                <asp:Label ID="Label1" Text="From&nbsp;Date" runat="server"></asp:Label>
                                            </td>
                                            <td class="field_input">
                                                <asp:TextBox ID="txtfromdate" runat="server" Enabled="false" Width="70" placeholder="dd/MM/yyyy"
                                                    MaxLength="10"></asp:TextBox>
                                                <asp:ImageButton ID="Imgfrom" runat="server" ImageUrl="~/images/calendar.png" Width="24px"
                                                    Height="24px" ImageAlign="AbsMiddle" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="Imgfrom" runat="server"
                                                    TargetControlID="txtfromdate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                            <td class="field_title">
                                                <asp:Label ID="lbltodt" Text="To Date" runat="server"></asp:Label>
                                            </td>
                                            <td class="field_input">
                                                <asp:TextBox ID="txttodate" runat="server" Enabled="false" Width="70" placeholder="dd/MM/yyyy"
                                                    MaxLength="10"></asp:TextBox>
                                                <asp:ImageButton ID="ImgToDate" runat="server" ImageUrl="~/images/calendar.png" Width="24px"
                                                    Height="24px" ImageAlign="AbsMiddle" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImgToDate" runat="server"
                                                    TargetControlID="txttodate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>

                                            <td class="field_title">
                                                <asp:Label ID="lblsaletype" Text="Sale&nbsp;Type" runat="server"></asp:Label>
                                            </td>
                                            <td class="field_input">
                                                <asp:DropDownList ID="ddlsaletype" runat="server" class="chosen-select" AppendDataBoundItems="True" Width="110px">
                                                    <asp:ListItem Text="All" Value="0" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Trading Sale" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Sale Invoice" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Export Sale" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="Coporate Sale" Value="4"></asp:ListItem>
                                                    <asp:ListItem Text="Stock Transfer" Value="5"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td class="field_title">
                                                <asp:Label ID="lblproduct" Text="Product" runat="server"></asp:Label>
                                            </td>
                                            <td class="field_input">
                                                <%--<asp:ListBox ID="ddlnewproduct" runat="server" SelectionMode="Multiple"
                                                    TabIndex="4" Width="200" ValidationGroup="ADD" AppendDataBoundItems="True"></asp:ListBox>
                                                <asp:CustomValidator ID="rvproduct" runat="server" ValidateEmptyText="true"
                                                    ControlToValidate="ddlnewproduct" ValidationGroup="ADD" ErrorMessage="Required!"
                                                    Display="Dynamic" ForeColor="Red"></asp:CustomValidator>--%>

                                                <asp:DropDownList ID="ddlProduct" runat="server" class="chosen-select" AppendDataBoundItems="True" Width="320px">
                                                </asp:DropDownList>
                                            </td>

                                            <td class="field_input">
                                                <div class="btn_24_blue">
                                                    <span class="icon exclamation_co"></span>
                                                    <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn_link" OnClick="btnShow_Click"
                                                        ValidationGroup="Show" />
                                                </div>
                                            </td>
                                            <td class="field_input">
                                                <div class="btn_24_blue">
                                                    <span class="icon doc_excel_table_co"></span>
                                                    <a href="#" onclick="exportToExcel();">
                                                        <asp:Button ID="btnExport" runat="server" CssClass="btn_link" Text="Export To Excel" OnClick="ExportToExcel" /></a>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px;">
                                                <div style="margin: 0 auto; width: 1265px;">
                                                    <div style="overflow: hidden;" id="DivHeaderRow">
                                                    </div>
                                                    <div id="DivMainContent" style="overflow: scroll;" onscroll="OnScrollDiv(this);">
                                                        <asp:GridView ID="gvallsaledtls" runat="server" AutoGenerateColumns="False" Width="100%" GridLines="Horizontal" CssClass="zebra">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Sl No">
                                                                    <ItemTemplate>
                                                                        <%# Container.DataItemIndex + 1 %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="PRODUCT OWNER" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblproductowner" runat="server" Text='<%# Bind("PRODUCTOWNER") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="SALE TYPE">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblsaletype" runat="server" Text='<%# Bind("SALETYPE") %>' Width="90"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="INVOICE NO">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblsaleinvoiceno" runat="server" Text='<%# Bind("SALEINVOICENO") %>' Width="90"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="INVOICE DATE">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblsaleinvoicedate" runat="server" Text='<%# Bind("SALEINVOICEDATE") %>' Width="70"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="VENDOR NAME">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblvendorname" runat="server" Text='<%# Bind("DISTRIBUTORNAME") %>' Width="220"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="GST NO">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgstno" runat="server" Text='<%# Bind("GSTNO") %>' Width="100"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="POSTING NO">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblpostingno" runat="server" Text='<%# Bind("POSTINGNO") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="BRAND">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBrand" runat="server" Text='<%# Bind("BRAND") %>' Width="120"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="CATNAME">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCatname" runat="server" Text='<%# Bind("CATEGORY") %>' Width="110"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="PRODUCT NAME">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblProductName" runat="server" Text='<%# Bind("ITEMNAME") %>' Height="25" Width="250"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="MRP">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblmrp" runat="server" Text='<%# Bind("MRP") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="HSN">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblhsn" runat="server" Text='<%# Bind("HSN") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="QTY">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblqtyinpcs" runat="server" Text='<%# Bind("QTYINPCS") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="RATE">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblrate" runat="server" Text='<%# Bind("RATE") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="AMOUNT">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblamount" runat="server" Text='<%# Bind("AMOUNT") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="OUTPUT CGST(%)">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbloutputcgst" runat="server" Text='<%# Bind("OUTPUTCGST") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="OUTPUT CGST VALUE">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbloutputcgstVALUE" runat="server" Text='<%# Bind("OUTPUTCGSTVALUE") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="OUTPUT SGST(%)">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbloutputsgst" runat="server" Text='<%# Bind("OUTPUTSGST") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="OUTPUT SGST VALUE">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbloutputsgstvalue" runat="server" Text='<%# Bind("OUTPUTSGSTVALUE") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="OUTPUT IGST(%)">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbloutputigst" runat="server" Text='<%# Bind("OUTPUTIGST") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="OUTPUT IGST VALUE">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbloutputsgstvalue" runat="server" Text='<%# Bind("OUTPUTIGSTVALUE") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="TCS (%)">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbltcs" runat="server" Text='<%# Bind("TCS") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="TCS VALUE">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbltcsvalue" runat="server" Text='<%# Bind("TCSVALUE") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="TAX VALUE">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbltaxvalue" runat="server" Text='<%# Bind("TAXVALUE") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="TOTAL AMT">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbltotalamt" runat="server" Text='<%# Bind("TOTALAMT") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                        <div class="clear">
                                                        </div>
                                                    </div>
                                                    <div id="DivFooterRow" style="overflow: hidden;">
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                    <span class="clear"></span>
                </div>
                <span class="clear"></span>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
