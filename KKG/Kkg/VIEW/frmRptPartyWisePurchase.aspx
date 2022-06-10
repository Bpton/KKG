<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true"
    CodeFile="frmRptPartyWisePurchase.aspx.cs" Inherits="FACTORY_frmRptPartyWisePurchase" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
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
    <script type="text/javascript">
        function ValidateListBox(sender, args) {
            var options = document.getElementById("<%=ddldepot.ClientID%>").options;
            for (var i = 0; i < options.length; i++) {
                if (options[i].selected == true) {
                    args.IsValid = true;
                    return;
                }
            }
            args.IsValid = false;
        }
        function ValidateListBox_ddlBrand(sender, args) {
            var options = document.getElementById("<%=ddlbrand.ClientID%>").options;
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

        $(function () {
            $('#ContentPlaceHolder1_ddlbrand').multiselect({
                includeSelectAllOption: true
            });
            //$("#ContentPlaceHolder1_ddlbrand").multiselect('selectAll', false);
            //$("#ContentPlaceHolder1_ddlbrand").multiselect('updateButtonText');
        });

        $(function () {
            $('#ContentPlaceHolder1_ddlcategory').multiselect({
                includeSelectAllOption: true
            });
            $("#ContentPlaceHolder1_ddlcategory").multiselect('selectAll', false);
            $("#ContentPlaceHolder1_ddlcategory").multiselect('updateButtonText');
        });

        $(function () {
            $('#ContentPlaceHolder1_ddlproduct').multiselect({
                includeSelectAllOption: true
            });
            $("#ContentPlaceHolder1_ddlproduct").multiselect('selectAll', false);
            $("#ContentPlaceHolder1_ddlproduct").multiselect('updateButtonText');
        });

        $(function () {
            $('#ContentPlaceHolder1_ddlgroupby').multiselect({
                includeSelectAllOption: true
            });
            $("#ContentPlaceHolder1_ddlgroupby").multiselect('selectAll', false);
            $("#ContentPlaceHolder1_ddlgroupby").multiselect('updateButtonText');
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
                                <h6>Party and Productwise Purchase
                                </h6>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlDisplay" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td>
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td class="field_title" width="100">
                                                            <asp:Label ID="Label8" runat="server" Text="Calender Details"></asp:Label>
                                                        </td>
                                                        <td class="field_input" width="100">
                                                            <asp:DropDownList ID="ddlcalender" runat="server" AppendDataBoundItems="True" Width="140"
                                                                AutoPostBack="true" Height="28" class="chosen-select" data-placeholder="Choose"
                                                                OnSelectedIndexChanged="ddlcalender_SelectedIndexChanged">
                                                                <asp:ListItem Selected="True" Text="SELECT" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="YEARLY" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="QUARTERLY" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="MONTHLY" Value="3"></asp:ListItem>
                                                                <asp:ListItem Text="PERIOD" Value="4"></asp:ListItem>
                                                                <%--<asp:ListItem Text="JC" Value="5"></asp:ListItem>--%>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td class="field_title" width="50">
                                                            <asp:Label ID="lblcalender" runat="server" Text="Year"></asp:Label>
                                                        </td>
                                                        <td class="field_input" width="50">
                                                            <asp:ListBox ID="ddlspan" runat="server" SelectionMode="Multiple" TabIndex="4" OnSelectedIndexChanged="ddlspan_SelectedIndexChanged"
                                                                Width="250px" AppendDataBoundItems="True" ValidationGroup="ADD" AutoPostBack="true"></asp:ListBox>
                                                        </td>
                                                        <td width="90" class="field_title">
                                                            <asp:Label ID="Label1" Text="From Date" runat="server"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="120" class="field_input">
                                                            <asp:TextBox ID="txtfromdate" runat="server" Enabled="false" Width="100" placeholder="dd/MM/yyyy"
                                                                MaxLength="10" ValidationGroup="sales"></asp:TextBox>
                                                            <asp:ImageButton ID="Imgfrom" runat="server" ImageUrl="~/images/calendar.png" Width="24px"
                                                                Height="24px" ImageAlign="AbsMiddle" ValidationGroup="sales" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="Imgfrom" runat="server"
                                                                TargetControlID="txtfromdate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                        <td width="70" class="field_title">
                                                            <asp:Label ID="Label2" Text="To Date" runat="server"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="190" class="field_input">
                                                            <asp:TextBox ID="txttodate" runat="server" Enabled="false" Width="100" placeholder="dd/MM/yyyy"
                                                                MaxLength="10" ValidationGroup="sales"></asp:TextBox>
                                                            <asp:ImageButton ID="ImgToDate" runat="server" ImageUrl="~/images/calendar.png" Width="24px"
                                                                Height="24px" ImageAlign="AbsMiddle" ValidationGroup="sales" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImgToDate" runat="server"
                                                                TargetControlID="txttodate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="field_title" width="100">
                                                            <asp:Label ID="Label5" runat="server" Text="Depot"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td class="field_input" width="100">
                                                            <asp:ListBox ID="ddldepot" runat="server" AppendDataBoundItems="True"
                                                                AutoPostBack="true" OnSelectedIndexChanged="ddldepot_OnSelectedIndexChanged"
                                                                SelectionMode="Multiple" TabIndex="4" ValidationGroup="sales"></asp:ListBox>
                                                        </td>
                                                        <td class="field_title" width="75">
                                                            <asp:Label ID="Label4" runat="server" Text="Brand"></asp:Label><span class="req">*</span>
                                                        </td>
                                                        <td class="field_input" width="75">
                                                            <asp:ListBox ID="ddlbrand" runat="server" SelectionMode="Multiple" TabIndex="4"
                                                                ValidationGroup="ADD" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlbrand_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                                                            <asp:CustomValidator ID="CustomValidator10" runat="server" ValidateEmptyText="true"
                                                                ControlToValidate="ddlbrand" ValidationGroup="ADD" ErrorMessage="Required!" Display="Dynamic"
                                                                ClientValidationFunction="ValidateListBox_ddlBrand" ForeColor="Red"></asp:CustomValidator>
                                                        </td>

                                                        <td class="field_title" width="100">
                                                            <asp:Label ID="Label3" runat="server" Text="Category"></asp:Label><span class="req">*</span>
                                                        </td>
                                                        <td class="field_input" width="100">
                                                            <asp:ListBox ID="ddlcategory" runat="server" SelectionMode="Multiple"
                                                                TabIndex="4" Width="200" ValidationGroup="ADD" AppendDataBoundItems="True"
                                                                OnSelectedIndexChanged="ddlcategory_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                                                        </td>
                                                        <td class="field_title" width="50">
                                                            <asp:Label ID="Label10" runat="server" Text="Product"></asp:Label>
                                                        </td>
                                                        <td class="field_input" width="75">
                                                            <asp:ListBox ID="ddlproduct" runat="server" SelectionMode="Multiple"
                                                                TabIndex="4" Width="250" ValidationGroup="ADD" AppendDataBoundItems="True"></asp:ListBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="90" class="field_title">
                                                            <asp:Label ID="Label6" runat="server" Text="TYPE"></asp:Label>
                                                        </td>
                                                        <td width="200" class="field_input">
                                                            <%--<ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="Imgfrom" runat="server"
                                                                TargetControlID="txtfromdate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                            </ajaxToolkit:CalendarExtender>--%>
                                                            <asp:ListBox ID="ddlgroupby" runat="server" SelectionMode="Multiple" AppendDataBoundItems="True"
                                                                TabIndex="4" ValidationGroup="ADD" Width="140">
                                                                <%--<asp:ListItem Text="ALL" Value="0"></asp:ListItem>--%>
                                                                <asp:ListItem Text="TPU RECEIVED" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="STOCK RECEIVED" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="FACTORY RECEIVED" Value="3"></asp:ListItem>
                                                                <asp:ListItem Text="PURCHASE RETURN" Value="4"></asp:ListItem>
                                                            </asp:ListBox>
                                                        </td>

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
                                                <div style="margin: 0 auto; width: 1300px;">
                                                    <div style="overflow: hidden;" id="DivHeaderRow">
                                                    </div>
                                                    <div id="DivMainContent" style="overflow: scroll;" onscroll="OnScrollDiv(this);">
                                                        <asp:GridView ID="gvDepotwisePurchase" runat="server"
                                                            AutoGenerateColumns="true" Width="100%" OnRowDataBound="gvDepotwisePurchase_OnRowDataBound"
                                                            RowStyle-Height="25px" FooterStyle-Height="27px" Visible="true"
                                                            ShowHeader="true" GridLines="Horizontal" CssClass="zebra">
                                                            <%--<Columns>
                                                                <asp:BoundField DataField="PRODUCTNAME" />
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:HyperLink ID="lnk" CommandArgument='<%# Eval("PRODUCTNAME") %>' CommandName ="selectState" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>--%>
                                                            <HeaderStyle BackColor="Silver" Font-Bold="True" ForeColor="#3399FF" />
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
