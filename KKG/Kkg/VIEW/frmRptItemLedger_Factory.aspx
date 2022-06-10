<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmRptItemLedger_Factory.aspx.cs" Inherits="VIEW_frmRptItemLedger_Factory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
                                <h6>
                                    Item Ledger Report
                                </h6>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlDisplay" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td>
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="90" class="field_title">
                                                            <asp:Label ID="Label1" Text="From Date" runat="server"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="200" class="field_input">
                                                            <asp:TextBox ID="txtfromdate" runat="server" Enabled="false" Width="85" placeholder="dd/MM/yyyy"
                                                                MaxLength="10"></asp:TextBox>
                                                            <asp:ImageButton ID="Imgfrom" runat="server" ImageUrl="~/images/calendar.png" Width="24px"
                                                                Height="24px" ImageAlign="AbsMiddle" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="Imgfrom" runat="server"
                                                                TargetControlID="txtfromdate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                        <td class="field_title" width="90">
                                                            <asp:Label ID="Label2" Text="To Date" runat="server"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td class="field_input" width="200">
                                                            <asp:TextBox ID="txttodate" runat="server" Enabled="false" Width="85" placeholder="dd/MM/yyyy"
                                                                MaxLength="10"></asp:TextBox>
                                                            <asp:ImageButton ID="ImgToDate" runat="server" ImageUrl="~/images/calendar.png" Width="24px"
                                                                Height="24px" ImageAlign="AbsMiddle" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImgToDate" runat="server"
                                                                TargetControlID="txttodate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                        <td class="field_title" width="90">
                                                            <asp:Label ID="Label4" Text="Factory" runat="server"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td class="field_input" >
                                                            <asp:DropDownList ID="ddldepot" runat="server" AppendDataBoundItems="True" ValidationGroup="Show"
                                                                Width="200" class="chosen-select" data-placeholder="Select depot">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rvdepot" runat="server" ControlToValidate="ddldepot"
                                                                ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ForeColor="Red"
                                                                InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                          <td width="100" class="field_title" style="padding-bottom: 15px;">
                                                                <asp:Label ID="lblBrand" Text="PRIMARY ITEM" runat="server"></asp:Label>&nbsp;<span class="req">*</span>
                                                            </td>
                                                            <td width="218" class="field_input">
                                                                <asp:DropDownList ID="ddlBrand" Width="210" runat="server" class="chosen-select"
                                                                    data-placeholder="Choose Primary Item" ValidationGroup="ADD" AutoPostBack="True" OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlBrand"
                                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="ADD"
                                                                    ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td width="75" class="field_title" style="padding-bottom: 15px;">
                                                                <asp:Label ID="lblCategory" Text="SUB ITEM" runat="server"></asp:Label>&nbsp;<span
                                                                    class="req">*</span>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:DropDownList ID="ddlCategory" Width="210" runat="server" class="chosen-select"
                                                                    data-placeholder="Choose Sub Item" ValidationGroup="ADD" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorCategory" runat="server" ControlToValidate="ddlCategory"
                                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="ADD"
                                                                    ForeColor="Red" InitialValue="0">
                                                                </asp:RequiredFieldValidator><br />
                                                                <br />
                                                            </td>
                                                        <td width="90" class="field_title">
                                                            <asp:Label ID="Label5" Text="Product" runat="server"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="200" class="field_input">
                                                            <asp:DropDownList ID="ddlnewproduct" Width="250" runat="server" class="chosen-select"
                                                                data-placeholder="Select Product" AppendDataBoundItems="True" ValidationGroup="Show">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rvproduct" runat="server" ControlToValidate="ddlnewproduct"
                                                                ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ForeColor="Red"
                                                                InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                                        </td>
                                                        </tr>
                                                    <tr>
                                                        <td class="field_title" width="140">
                                                            <asp:Label ID="Label3" Text="Store Location" runat="server"></asp:Label>
                                                        </td>
                                                        <td class="field_input" colspan="3">
                                                            <asp:DropDownList ID="ddlstorelocation" Width="150" runat="server" ValidationGroup="Show"
                                                                class="chosen-select" AppendDataBoundItems="True">
                                                            </asp:DropDownList>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                            <div class="btn_24_blue">
                                                                <span class="icon exclamation_co"></span>
                                                                <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn_link" OnClick="btnShow_Click"
                                                                    ValidationGroup="Show" />
                                                            </div>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;
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
                                        <li>
                                            
                                        </li>
                                        <li>
                                           
                                        </li>
                                    </ul>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td"
                                        align="center">
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px;">
                                                <div style="margin: 0 auto; width: 1250px;">
                                                    <div style="overflow: hidden;" id="DivHeaderRow">
                                                    </div>
                                                    <div id="DivMainContent" style="overflow: scroll;" onscroll="OnScrollDiv(this);">
                                                        <asp:GridView ID="gvItemLedger" runat="server" AutoGenerateColumns="false" Width="100%" OnRowCommand="gvItemLedger_RowCommand"
                                                            OnRowDataBound="gv_RowDataBound" ShowFooter="true" Visible="true" ShowHeader="true"
                                                            GridLines="Horizontal" CssClass="reportgrid">
                                                            <HeaderStyle BackColor="Silver" Font-Bold="True" HorizontalAlign="Center" />
                                                             <RowStyle Height="20px" />

                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Date">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDate" runat="server" Text='<%# Bind("Date") %>' Height="25" Width="50"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Vch No.">
                                                                    <ItemTemplate>
                                                                       <%-- <asp:Label ID="lblVchno" runat="server" Text='<%# Bind("VCHNO") %>' Height="25" Width="180"></asp:Label>--%>
                                                                         <asp:LinkButton ID="lnkVchno" runat="server" ForeColor="Black" CommandName="VCHID"
                                                                            CommandArgument='<%#Eval("VCHID")+","+Eval("VCHNO")+","+Eval("VType") %>' Text='<%# Bind("VCHNO") %>' />                                                                            
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Batch No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBatch" runat="server" Text='<%# Bind("BATCH") %>' Height="25" Width="100"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="VType">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblVtype" runat="server" Text='<%# Bind("VType") %>' Height="25" Width="150"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Party">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblParty" runat="server" Text='<%# Bind("PARTY") %>' Height="25" Width="100"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Mrp">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMrp" runat="server" Text='<%# Bind("MRP") %>' Height="25" Width="50"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Opening">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblOpening" runat="server" Text='<%# Bind("OPENING") %>' Height="25" Width="80"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Inward" ItemStyle-HorizontalAlign="Right">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblInward" runat="server" Text='<%# Bind("INWARD") %>' Height="25" Width="80"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Outward">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblOutward" runat="server" Text='<%# Bind("OUTWARD") %>' Height="25" Width="80"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Balance">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBalance" runat="server" Text='<%# Bind("BALANCE") %>' Height="25" Width="80"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="VCHID" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblVchid" runat="server" Text='<%# Bind("VCHID") %>' Height="25" Width="80"></asp:Label>
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