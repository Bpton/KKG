<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmRptBOMReport.aspx.cs" Inherits="VIEW_frmRptBOMReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/bootstrap-multiselect.css" rel="stylesheet" />
    <script type="text/javascript" src="../js/bootstrap.js"></script>
    <script type="text/javascript" src="../js/bootstrap-multiselect.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <style>
        th {
            background: cornflowerblue !important;
            color: white !important;
            position: sticky !important;
            top: 0;
            box-shadow: 0 2px 2px -1px rgb(255, 106, 0);
            width: 400px;
        }

        th, td {
            padding: 0.25rem;
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
    <script language="javascript" type="text/javascript">
        function MakeStaticHeader(gridId, height, width, headerHeight, isFooter) {
            //debugger;
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
        $(function () {
            $('#ContentPlaceHolder1_ddlframework').multiselect({
                includeSelectAllOption: true
            });
            $("#ContentPlaceHolder1_ddlframework").multiselect('selectAll', true);
            $("#ContentPlaceHolder1_ddlframework").multiselect('updateButtonText');
        });
    </script>

    <script type="text/javascript">
        $(function () {
            $('#ContentPlaceHolder1_ddlProcess').multiselect({
                includeSelectAllOption: true
            });
            $("#ContentPlaceHolder1_ddlProcess").multiselect('selectAll', true);
            $("#ContentPlaceHolder1_ddlProcess").multiselect('updateButtonText');
        });
    </script>

    <script type="text/javascript">
        $(function () {
            $('#ContentPlaceHolder1_ddlBrand').multiselect({
                includeSelectAllOption: true
            });
            $("#ContentPlaceHolder1_ddlBrand").multiselect('selectAll', true);
            $("#ContentPlaceHolder1_ddlBrand").multiselect('updateButtonText');
        });

        function ValidateListBox_ddlBrand(sender, args) {
            var options = document.getElementById("<%=ddlBrand.ClientID%>").options;
            for (var i = 0; i < options.length; i++) {
                if (options[i].selected == true) {
                    args.IsValid = true;
                    return;
                }
            }
            args.IsValid = false;
        }

    </script>


    <asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
            <asp:Image ID="Image1" ImageUrl="~/images/loading123.gif" AlternateText="Processing"
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
                                <h6>BOM Report
                                </h6>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlDisplay" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td>
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td class="field_title" width="90">
                                                            <asp:Label ID="Label4" Text="Factory" runat="server"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td class="field_input" colspan="3">
                                                            <asp:DropDownList ID="ddldepot" runat="server" AppendDataBoundItems="True" ValidationGroup="Show"
                                                                Width="350" class="chosen-select" data-placeholder="Select depot">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rvdepot" runat="server" ControlToValidate="ddldepot"
                                                                ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ForeColor="Red"
                                                                InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="100" class="field_title" style="padding-bottom: 15px;">
                                                            <asp:Label ID="lblBrand" Text="PRIMARY ITEM" runat="server"></asp:Label>&nbsp;<span
                                                                class="req">*</span>
                                                        </td>
                                                        <td width="218" class="field_input">
                                                            <asp:ListBox ID="ddlBrand" runat="server" SelectionMode="Multiple" AutoPostBack="true"
                                                                Width="280px" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged"></asp:ListBox>
                                                            <%--<asp:CustomValidator ID="CustomValidator2" runat="server" ValidateEmptyText="true"
                                                                ControlToValidate="ddlBrand" ValidationGroup="ADD" ErrorMessage="Required!"
                                                                Display="Dynamic" ClientValidationFunction="ValidateListBox" ForeColor="Red"></asp:CustomValidator>--%>
                                                        </td>
                                                        <td width="100" class="field_title" style="padding-bottom: 15px;">
                                                            <asp:Label ID="lblframework" Text="FRAMEWORK" runat="server"></asp:Label>&nbsp;<span
                                                                class="req">*</span>
                                                        </td>
                                                        <td class="field_input">
                                                            <asp:ListBox ID="ddlframework" runat="server" SelectionMode="Multiple" TabIndex="4"
                                                                Width="280px" AppendDataBoundItems="True" ValidationGroup="ADD"></asp:ListBox>
                                                            <asp:CustomValidator ID="CustomValidator1" runat="server" ValidateEmptyText="true"
                                                                ControlToValidate="ddlframework" ValidationGroup="ADD" ErrorMessage="Required!"
                                                                Display="Dynamic" ClientValidationFunction="ValidateListBox" ForeColor="Red"></asp:CustomValidator>
                                                            <br />
                                                            <br />
                                                        </td>
                                                        <td width="100" class="field_title" style="padding-bottom: 15px;">
                                                            <asp:Label ID="lblprocess" Text="PROCESS" runat="server"></asp:Label>&nbsp;<span
                                                                class="req">*</span>
                                                        </td>
                                                        <td class="field_input">
                                                            <asp:ListBox ID="ddlProcess" Width="280px" runat="server" SelectionMode="Multiple" TabIndex="4" 
                                                                data-placeholder="Choose PROCESS" ValidationGroup="Show" AppendDataBoundItems="True">
                                                            </asp:ListBox>
                                                            <asp:CustomValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlProcess"
                                                                ValidateEmptyText="true" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="Show"
                                                                ForeColor="Red" InitialValue="0" Display="Dynamic" ClientValidationFunction="ValidateListBox"></asp:CustomValidator>
                                                            <br />
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="field_title" width="140">
                                                            <div class="btn_24_blue">
                                                                <span class="icon exclamation_co"></span>
                                                                <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn_link" OnClick="btnShow_Click"
                                                                    ValidationGroup="Show" />
                                                            </div>
                                                        </td>
                                                        <td class="field_title" width="140" colspan="3">&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <div class="btn_24_blue">
                                                                <span class="icon doc_excel_table_co"></span>
                                                                <asp:Button ID="btnExport" runat="server" CssClass="btn_link" Text="Export To Excel"
                                                                    OnClick="ExportToExcel" /></a>
                                                            </div>
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
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td align="left" class="field_input" style="padding-left: 10px;">
                                                <asp:Panel ID="PanelGridView" runat="server" CssClass="reportgrid">
                                                </asp:Panel>
                                            </td>
                                        </tr>

                                        <asp:GridView ID="grdBomReport" runat="server" Width="100%" ShowFooter="true"
                                            EmptyDataRowStyle-Height="30px" FooterStyle-Height="40px" AutoGenerateColumns="true"
                                            EmptyDataText="No Records Available" CssClass="zebra" AlternatingRowStyle-BackColor="AliceBlue" Font-Bold="true">
                                            <%--<Columns>
                                    <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <nav style="height: 2px; z-index: 1; padding-left: 10px; position: relative; text-align: left;"><span class="badge black"><%# Container.DataItemIndex + 1 %></span></nav>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>--%>
                                            <RowStyle HorizontalAlign="left"></RowStyle>
                                        </asp:GridView>
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
