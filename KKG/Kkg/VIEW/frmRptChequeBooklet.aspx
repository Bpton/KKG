<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="frmRptChequeBooklet.aspx.cs" Inherits="VIEW_frmRptChequeBooklet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/bootstrap-multiselect.css" rel="stylesheet" />
    <script type="text/javascript" src="../js/bootstrap.js"></script>
    <script type="text/javascript" src="../js/bootstrap-multiselect.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<script type="text/javascript">        
        function ValidateListBox_ddlbsegment(sender, args) {
            var options = document.getElementById("<%=ddlbsegment.ClientID%>").options;
            for (var i = 0; i < options.length; i++) {
                if (options[i].selected == true) {
                    args.IsValid = true;
                    return;
                }
            }
            args.IsValid = false;
        }
    </script>--%>

    

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
                    tblfr.cellSpacing = "10";
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
            <%--  <asp:PostBackTrigger ControlID="btnshow"  />--%>
        </Triggers>
        <ContentTemplate>
            <div id="content">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active">
                                <span class="h_icon_he list_images"></span>
                                <h6>Cheque Booklet Report</h6>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="InputTable" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px;">
                                                <fieldset>
                                                    <legend>Cheque Booklet Report</legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                        <%--    <td class="field_title" width="60">
                                                                <asp:Label ID="Label9" runat="server" Text="FROM DATE"></asp:Label>
                                                            </td>
                                                            <td class="field_input" width="90">
                                                                <asp:TextBox ID="txtfromdate" runat="server" Width="80" Enabled="false" placeholder="dd/mm/yyyy" 
                                                                    MaxLength="10" ValidationGroup="ADD" onkeypress="return isNumberKeyWithslash(event);"></asp:TextBox>
                                                                <asp:ImageButton ID="imgPopup" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopup" runat="server"
                                                                    TargetControlID="txtfromdate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>
                                                            <td class="field_title" width="50">
                                                                <asp:Label ID="Label12" runat="server" Text="TO From"></asp:Label>
                                                            </td>
                                                            <td class="field_input" width="90">
                                                                <asp:TextBox ID="txttodate" runat="server" Width="80" Enabled="false" placeholder="dd/mm/yyyy" 
                                                                    MaxLength="10" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="ADD"></asp:TextBox>
                                                                <asp:ImageButton ID="Imgfrom" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="Imgfrom" runat="server"
                                                                    TargetControlID="txttodate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>--%>
                          
                                             

                                                            <td class="field_title" width="120">
                                                                <asp:Label ID="Label7" runat="server" Text="Bank Name"></asp:Label>
                                                            </td>
                                                            <td class="field_input" width="150">
                                                                 <asp:DropDownList ID="ddlBankName" runat="server" AppendDataBoundItems="true" Width="200" Height="28"
                                                                    class="chosen-select" data-placeholder="Choose" ValidationGroup="ADD" AutoPostBack="true" OnSelectedIndexChanged="ddlBankName_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td class="field_title" width="120">
                                                                <asp:Label ID="Label1" runat="server" Text="Booklet No."></asp:Label>
                                                            </td>
                                                            <td class="field_input" width="200">
                                                                 <asp:DropDownList ID="ddlBookletNo" runat="server" AppendDataBoundItems="true" Width="170" Height="28"
                                                                    class="chosen-select" data-placeholder="Choose" ValidationGroup="ADD">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td class="field_title" width="120">
                                                                 <asp:Label ID="LblReport" runat="server" Text="Report Type"></asp:Label>
                                                                </td>
                                                            <td class="field_title" width="200">
                                                                 <asp:DropDownList ID="ddlReport" Width="200" runat="server" class="chosen-select" >
                                                                                <asp:ListItem Value="Details" Text="Details" />
                                                                                <asp:ListItem Value="Summary" Text="Summary" />
                                                                                <asp:ListItem Value="Cancel" Text="Cancel" />
                                                                                <asp:ListItem Value="Disposal" Text="Disposal" />
                                                                                <asp:ListItem Value="AlreadyIssue" Text="Already Issue" />
                                                                                <asp:ListItem Value="Balance" Text="Unused Balance" />
                                                                            </asp:DropDownList>
                                                                </td>

                                                        </tr>
                                                       
                                                        <tr>
                                                            <td width="100" colspan="2" class="field_input">
                                                                <div class="btn_24_blue">
                                                                    <span class="icon exclamation_co"></span>
                                                                    <asp:Button ID="btnshow" runat="server" Text="Show" CssClass="btn_link" ValidationGroup="ADD"
                                                                        OnClick="btnshow_Click" />
                                                                </div>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                                <div class="btn_24_blue">
                                                                    <span class="icon doc_excel_table_co"></span><a href="#" onclick="exportToExcel();">
                                                                        <asp:Button ID="btnExport" runat="server" Text="Export Excel" CssClass="btn_link"
                                                                            OnClick="ExportToExcel" CausesValidation="false" /></a>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="gridcontent-inner" id="divProductDetails" runat="server">
                                        <fieldset>
                                            <legend>Details</legend>
                                            <div style="overflow: hidden;" id="DivHeaderRow"></div>
                                            <div id="DivMainContent" style="overflow: auto; margin-bottom: 8px; margin-right: 6px;" onscroll="OnScrollDiv(this);">
                                                <div id="divAll" runat="server">
                                                <asp:GridView ID="grdMigrationCode" EmptyDataText="There are no records available."
                                                    ShowFooter="true" Visible="true" ShowHeader="true" GridLines="Horizontal" OnRowDataBound="gv_RowDataBound"
                                                     RowStyle-Height="27"
                                                    CssClass="zebra" runat="server" AutoGenerateColumns="true" Width="100%">
                                                </asp:GridView>
                                               </div>
                                                <div id="divDetails" runat="server">

                                                      <asp:GridView ID="grdledger1" runat="server" AutoGenerateColumns="false" 
                                                DataKeyNames="AccEntryID"  Width="100%" OnRowCommand="grdledger1_RowCommand"
                                                ShowFooter="false" Visible="true" ShowHeader="true" GridLines="Horizontal" CssClass="zebra">
                                           <%--     <FooterStyle Font-Bold="True" ForeColor="White" Height="50px" />--%>
                                                <Columns>
                                                    											
                                                     <asp:TemplateField HeaderText="SL.NO." ItemStyle-Wrap="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl61" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="AccEntryID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAcc2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "AccEntryID") %>'></asp:Label>                                                         
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Auto Booklet No" ItemStyle-Wrap="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl13" runat="server" Text='<%# Bind("AutoBookletNo") %>' Width="75px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bank Name" ItemStyle-Wrap="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl24" runat="server" Text='<%# Bind("BankName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Party Name" ItemStyle-Wrap="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl35" runat="server" Text='<%# Bind("PartyName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                   
                                                     <asp:TemplateField HeaderText=" Depot Name" ItemStyle-Wrap="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl36" runat="server" Text='<%# Bind("DepotName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="BOOKLET NO" ItemStyle-Wrap="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl37" runat="server" Text='<%# Bind("BOOKLETNO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                         <asp:TemplateField HeaderText="Cheque Number" ItemStyle-Wrap="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl38" runat="server" Text='<%# Bind("CheqNumber") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Cheque Date" ItemStyle-Wrap="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl39" runat="server" Text='<%# Bind("ChequeDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Entry Date" ItemStyle-Wrap="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl310" runat="server" Text='<%# Bind("EntryDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Cheque Realised Date" ItemStyle-Wrap="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl311" runat="server" Text='<%# Bind("ChequeRealisedDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Vch.No." ItemStyle-Wrap="false">
                                                        <ItemTemplate>
                                                            <%--<asp:Label ID="lbl4" runat="server" Text='<%# Bind("VoucherNo") %>'></asp:Label>--%>                                                           
                                                      <%--      <asp:LinkButton ID="lnkvoucherno1" runat="server" ForeColor="Blue" CommandName="VoucherNo"
                                                                CommandArgument='<%#Eval("AccEntryID") %>'
                                                                Text='<%# Bind("VoucherNo") %>' />--%>

                                                              <asp:LinkButton ID="lnkvoucherno" runat="server" CommandName="VoucherNo" 
                                                            CommandArgument='<%# Eval("AccEntryID") %>' Text='<%# Bind("VoucherNo") %>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    		
                                                    <asp:TemplateField HeaderText="Amount" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl9412" runat="server" Text='<%# Bind("amount") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Approved" ItemStyle-Wrap="false" >
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl7713" runat="server" Text='<%# Bind("Approved") %>' Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="IsCancel" ItemStyle-Wrap="false" >
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl714" runat="server" Text='<%# Bind("IsCancel") %>' Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                   
                                                                                                    
                                                </Columns>
                                                <%--<HeaderStyle BackColor="Silver" Font-Bold="True" ForeColor="#3399FF" />--%>
                                            </asp:GridView>

                                                    </div>
                                                <div class="clear">
                                                </div>
                                            </div>
                                            <%--<div id="DivFooterRow" style="overflow: hidden;">
                                            </div>--%>
                                        </fieldset>
                                    </div> 
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function exportToExcel() {
            grdMigrationCode.exportToExcel();
        }
    </script>
</asp:Content>



