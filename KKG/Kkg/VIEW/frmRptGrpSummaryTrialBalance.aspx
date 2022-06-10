<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmRptGrpSummaryTrialBalance.aspx.cs" Inherits="VIEW_frmRptGrpSummaryTrialBalance" %>

<%@ Register Assembly="GridViewTree" Namespace="DigitalTools" TagPrefix="DT" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
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
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>
                                    <asp:Label ID="lbltype" runat="server" Text="Group Summary Report"></asp:Label>
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
                                                            <asp:Label ID="Label4" runat="server" Text="Region"></asp:Label>
                                                        </td>
                                                        <td class="field_input" width="320">
                                                            <asp:DropDownList ID="ddlregion" runat="server" AppendDataBoundItems="True" Width="310"
                                                                Height="28" class="chosen-select" data-placeholder="Choose">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="80" class="field_title">
                                                            <asp:Label ID="Label1" Text="From Date" runat="server"></asp:Label>
                                                        </td>
                                                        <td width="110" class="field_input">
                                                            <asp:TextBox ID="txtfromdate" runat="server" Enabled="false" Width="65" placeholder="dd/MM/yyyy" 
                                                                MaxLength="10"></asp:TextBox>
                                                            <asp:ImageButton ID="Imgfrom" runat="server" ImageUrl="~/images/calendar.png" Width="24px"
                                                                Height="24px" ImageAlign="AbsMiddle" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="Imgfrom" runat="server"
                                                                TargetControlID="txtfromdate" CssClass="cal_Theme1" Format="dd-MM-yyyy">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                        <td width="60" class="field_title">
                                                            <asp:Label ID="Label2" Text="To Date" runat="server"></asp:Label>
                                                        </td>
                                                        <td width="110" class="field_input">
                                                            <asp:TextBox ID="txttodate" runat="server" Enabled="false" Width="70" placeholder="dd/MM/yyyy" 
                                                                MaxLength="10"></asp:TextBox>
                                                            <asp:ImageButton ID="ImgToDate" runat="server" ImageUrl="~/images/calendar.png" Width="24px"
                                                                Height="24px" ImageAlign="AbsMiddle" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImgToDate" runat="server"
                                                                TargetControlID="txttodate" CssClass="cal_Theme1" Format="dd-MM-yyyy">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                        </tr>
                                                        <tr>
                                                        <td class="field_title" width="60">
                                                            <asp:Label ID="Label3" runat="server" Text="Group"></asp:Label>
                                                        </td>
                                                        <td class="field_input" width="320">
                                                            <asp:DropDownList ID="ddlgroup" runat="server" AppendDataBoundItems="True" Width="310"
                                                                Height="28" class="chosen-select" data-placeholder="Choose">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="90" class="field_input">
                                                            <div class="btn_24_blue">
                                                                <span class="icon exclamation_co"></span>
                                                                <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn_link" OnClick="btnShow_Click"
                                                                    ValidationGroup="Show" />
                                                            </div>
                                                        </td>
                                                        <td width="150" class="field_input">
                                                            <div class="btn_24_blue">
                                                                <span class="icon doc_excel_table_co"></span><a href="#">
                                                                    <asp:Button ID="btnExport" runat="server" Text="Export Excel" CssClass="btn_link"
                                                                        CausesValidation="false" OnClick="btnExport_Click" /></a>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                         <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <div class="gridcontent">
                                        <div style="overflow: hidden; width: 100%;" id="DivHeaderRow">
                                        </div>
                                        <div style="overflow: scroll; margin-bottom: 8px;" onscroll="OnScrollDiv(this)" id="DivMainContent">
                                            <asp:GridView ID="grdledger" runat="server" Width="100%" AutoGenerateColumns="false" OnRowDataBound="grdledger_RowDataBound"
                                                CssClass="trialbalancegrid" EmptyDataText="No Records Available" DataKeyNames="grpname">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="1" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl114" runat="server" Text='<%# Bind("Stage") %>' Width="425px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PARTICULARS">
                                                        <ItemTemplate>
                                                           <%-- <asp:Label ID="lbl15" runat="server" Text='<%# Bind("grpname") %>' Width="425px"></asp:Label>--%>
                                                             <a href="#"  title="Ledger Report" style="font-weight: bold;"><%# Eval("grpname")%></a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="OPENING DEBIT" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl16" runat="server" Text='<%# Bind("openingDEBIT") %>' Width="105px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="OPENING CREDIT" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl17" runat="server" Text='<%# Bind("openingCREDIT") %>' Width="105px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DEBIT" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl18" runat="server" Text='<%# Bind("debit") %>' Width="105px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CREDIT" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl19" runat="server" Text='<%# Bind("CREDIT") %>' Width="105px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CLOSING DEBIT" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl12" runat="server" Text='<%# Bind("closingdebit") %>' Width="105px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CLSOING CREDIT" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl10" runat="server" Text='<%# Bind("closingCREDIT") %>' Width="105px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                             <asp:GridView ID="grdledger_for_excel" runat="server" Width="100%" AutoGenerateColumns="false" OnRowDataBound="grdledger_RowDataBound"
                                                CssClass="trialbalancegrid" EmptyDataText="No Records Available" Visible="false">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="1" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl114" runat="server" Text='<%# Bind("Stage") %>' Width="425px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PARTICULARS">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl15" runat="server" Text='<%# Bind("NAME") %>' Width="425px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="OPENING DEBIT" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl16" runat="server" Text='<%# Bind("openingDEBIT") %>' Width="105px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="OPENING CREDIT" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl17" runat="server" Text='<%# Bind("openingCREDIT") %>' Width="105px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DEBIT" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl18" runat="server" Text='<%# Bind("debit") %>' Width="105px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CREDIT" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl19" runat="server" Text='<%# Bind("CREDIT") %>' Width="105px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CLOSING DEBIT" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl12" runat="server" Text='<%# Bind("closingdebit") %>' Width="105px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CLSOING CREDIT" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl10" runat="server" Text='<%# Bind("closingCREDIT") %>' Width="105px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>

                                        </div>
                                        <div id="DivFooterRow" style="overflow: hidden">
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                    <span class="clear"></span>
                </div>
                <span class="clear"></span>
            </div>
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