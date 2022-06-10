<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmRrtPoStatus.aspx.cs" Inherits="VIEW_frmRrtPoStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/bootstrap-multiselect.css" rel="stylesheet" />
    <script type="text/javascript" src="../js/bootstrap.js"></script>
    <script type="text/javascript" src="../js/bootstrap-multiselect.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
    <%--<script type="text/javascript">
        function ValidateListBox(sender, args) {
            var options = document.getElementById("<%=ddldepot.ClientID%>").options;
            for (var i = 0; i < options.length; i++) {
                if (options[i].selected == true) {
                    args.IsValid = true;
                    return;
                }
            }
            args.IsValid = false;
        }--%>

    <%--</script>--%>
    <script type="text/javascript">
        $(function () {
            $('#ContentPlaceHolder1_ddldepot').multiselect({
                includeSelectAllOption: true
            });
            $("#ContentPlaceHolder1_ddldepot").multiselect('selectAll', false);
            $("#ContentPlaceHolder1_ddldepot").multiselect('updateButtonText');
        });

        $(function () {
            $('#ContentPlaceHolder1_ddlspan').multiselect({
                includeSelectAllOption: true
            });
            $("#ContentPlaceHolder1_ddlspan").multiselect('selectAll', false);
            $("#ContentPlaceHolder1_ddlspan").multiselect('updateButtonText');
        });

    </script>
    <script type="text/javascript">
        function exportToExcel() {
            gvDepotwisePurchase.exportToExcel();
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
                                <h6>Productwise Purchase
                                </h6>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlDisplay" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td>
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="100" class="field_title">
                                                <asp:Label ID="Label1" Text="From Date" runat="server"></asp:Label>
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
                                            <td width="70" class="field_title">
                                                <asp:Label ID="Label2" Text="To Date" runat="server"></asp:Label>
                                            </td>
                                            <td width="190" class="field_input">
                                                <asp:TextBox ID="txttodate" runat="server" Enabled="false" Width="120" placeholder="dd/MM/yyyy"
                                                    MaxLength="10"></asp:TextBox>
                                                <asp:ImageButton ID="ImgToDate" runat="server" ImageUrl="~/images/calendar.png" Width="24px"
                                                    Height="24px" ImageAlign="AbsMiddle" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImgToDate" runat="server"
                                                    TargetControlID="txttodate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                                        <td class="field_title" width="100">
                                                            <asp:Label ID="Label5" runat="server" Text="Depot"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td class="field_input" width="100">
                                                            <asp:DropDownList ID="ddldepot" runat="server" TabIndex="4"  class="chosen-select"
                                                                    Width="200%" AppendDataBoundItems="True" ValidationGroup="ADD" name="options[]"
                                                                   ></asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="100" class="field_input">
                                                            <div class="btn_24_blue">
                                                                <span class="icon exclamation_co"></span>
                                                                <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn_link" OnClick="btnShow_Click"
                                                                    ValidationGroup="sales" />
                                                            </div>
                                                        </td>
                                                        <td width="100" class="field_input">
                                                            <div class="btn_24_blue">
                                                                <span class="icon doc_excel_table_co"></span>
                                                                <a href="#" onclick="exportToExcel();">
                                                                    <asp:Button ID="btnExport" runat="server" CssClass="btn_link" Text="Export To Excel" OnClick="ExportToExcel" /></a>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td"
                                        align="center">
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px;">
                                                <div style="margin: 0 auto; width: 1250px;">
                                                    
                                                    <div style="overflow: hidden; padding-left: 8px; width: 100%;" id="DivHeaderRow">
                                                    </div>                                                  
                                                        <div style="overflow: scroll; padding-left: 8px;" onscroll="OnScrollDiv(this)"  id="DivMainContent">
                                                        <asp:GridView ID="gvDepotwisePurchase" runat="server"
                                                            AutoGenerateColumns="false" Width="100%" OnRowDataBound="gvDepotwisePurchase_RowDataBound"
                                                            RowStyle-Height="25px" FooterStyle-Height="27px" Visible="true" ShowFooter="false" AlternatingRowStyle-BackColor="AliceBlue"
                                                            GridLines="Horizontal" CssClass="reportgridchild">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="10">
                                                                    <ItemTemplate>
                                                                        <nav style="position: relative; text-align: left;"><span class="badge black"><%# Container.DataItemIndex + 1 %></span></nav>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="PONO" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPONO" runat="server" Text='<%# Eval("PONO") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="USERNAME" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblUSERNAME" runat="server" Text='<%# Eval("USERNAME") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="CREATEDFROM" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCREATEDFROM" runat="server" Text='<%# Eval("CREATEDFROM") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="POSTATUS" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPOSTATUS" runat="server" Text='<%# Eval("POSTATUS") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="PODATE" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPODATE" runat="server" Text='<%# Eval("PODATE") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="PENDINGFOR" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPENDINGFOR" runat="server" Text='<%# Eval("PENDINGFOR") %>'></asp:Label>
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
                            <cc1:MessageBox ID="MessageBox1" runat="server" />
                        </div>
                    </div>
                    <span class="clear"></span>
                </div>
                <span class="clear"></span>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

