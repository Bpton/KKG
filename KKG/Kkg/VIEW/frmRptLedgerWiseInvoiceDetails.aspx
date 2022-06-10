<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmRptLedgerWiseInvoiceDetails.aspx.cs" Inherits="VIEW_frmRptLedgerWiseInvoiceDetails" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <link href="../css/bootstrap-multiselect.css" rel="stylesheet" />
    <script type="text/javascript" src="../js/bootstrap.js"></script>
    <script type="text/javascript" src="../js/bootstrap-multiselect.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">



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


         function ValidateListBox_ddlbsegment(sender, args) {
            var options = document.getElementById("<%=ddlLedger.ClientID%>").options;
            for (var i = 0; i < options.length; i++) {
                if (options[i].selected == true) {
                    args.IsValid = true;
                    return;
                }
            }
            args.IsValid = false;
        }

        $(function () {
            $('#ContentPlaceHolder1_ddlPono').multiselect({
                includeSelectAllOption: true
            });
        });

        </script>
     


      <asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
            <asp:Image ID="Image1" ImageUrl="../images/103.gif" AlternateText="Processing"
                runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
        PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
            <asp:PostBackTrigger ControlID="btnExcel" />
        </Triggers>
           <ContentTemplate>
            <div class="page_title">
                <span class="title_icon"><span class="computer_imac"></span></span>
                <h3>Ledger Wise Invoice Report</h3>
            </div>
                <div id="contentarea">
                    <div class="grid_container">
                         <div class="grid_12">
                             <div class="widget_wrap">

                                     <asp:Panel ID="pnlDisplay" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_title">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="90">
                                                            <asp:Label ID="Label16" runat="server" Text="From Date"></asp:Label>
                                                        </td>
                                                        <td width="165">
                                                            <asp:TextBox ID="txtFromDate" runat="server" MaxLength="10" Width="80" Enabled="false"
                                                                placeholder="dd/MM/yyyy" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="Search"
                                                                Font-Bold="true"></asp:TextBox>
                                                            <asp:ImageButton ID="ImageButton1" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImageButton1" runat="server"
                                                                TargetControlID="txtFromDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                        <td width="70">
                                                            <asp:Label ID="Label19" runat="server" Text="To Date"></asp:Label>
                                                        </td>
                                                        <td width="165">
                                                            <asp:TextBox ID="txtToDate" runat="server" MaxLength="10" Width="80" Enabled="false"
                                                                placeholder="dd/mm/yyyy" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="Search"
                                                                Font-Bold="true"></asp:TextBox>
                                                            <asp:ImageButton ID="imgPopuptodate" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtenderToDate" PopupButtonID="imgPopuptodate"
                                                                runat="server" TargetControlID="txtToDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>

                                                        <td width="70" class="field_title">
                                                            <asp:Label ID="Label1" runat="server" Text="Ledger Name"></asp:Label>
                                                        </td>
                                                        <td class="field_input">
                                                            <asp:ListBox ID="ddlLedger" runat="server" SelectionMode="Multiple" ValidationGroup="ADD"
                                                                AppendDataBoundItems="True" TabIndex="4" Width="150px"></asp:ListBox>
                                                        </td>
                                                        
                                                        <td width="110" valign="top">
                                                           
                                                                <span class="icon application_tree_co"></span>
                                                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-info"
                                                                    ValidationGroup="Search" OnClick="btnSearch_Click" />
                                                            
                                                        </td>

                                                         <td width="110" valign="top">
                                                           
                                                                <span class="icon excel_document"></span>
                                                                <asp:Button ID="btnExcel" runat="server" Text="Export Excel" CssClass="btn btn-success"
                                                                    ValidationGroup="Search" OnClick="btnExcel_Click" />
                                                            
                                                        </td>


                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>


                                    <div style="overflow: scroll; height: 350px; width: 100%;">
                                        <asp:GridView ID="grdParent" runat="server" Width="100%" CssClass="reportgrid" OnRowDataBound="grvParent_RowDataBound"
                                            AutoGenerateColumns="false" ShowFooter="true" AlternatingRowStyle-BackColor="AliceBlue"
                                            EmptyDataText="No Records Available">
                                           <Columns>
                                               <asp:TemplateField HeaderText="AccEntryID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVOUCHERID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "VOUCHERID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                <asp:BoundField ItemStyle-Width="150px" DataField="PARTYID" HeaderText="PARTYID" Visible="false" />
                                               <asp:BoundField ItemStyle-Width="150px" DataField="PARTYNAME" HeaderText="PARTY NAME" />
                                                <asp:BoundField ItemStyle-Width="150px" DataField="VOUCHERTYPE" HeaderText="VOUCHER TYPE" />
                                                <asp:BoundField ItemStyle-Width="150px" DataField="VOUCCHERDATE" HeaderText="VOUCCHER DATE" />
                                                <asp:BoundField ItemStyle-Width="150px" DataField="VOUCHERNO" HeaderText="VOUCHER NO"/>
                                             <asp:BoundField ItemStyle-Width="150px" DataField="DEBIT" HeaderText="DEBIT" />
                                             <asp:BoundField ItemStyle-Width="150px" DataField="CREDIT" HeaderText="CREDIT" />
                                                <asp:TemplateField HeaderText="APPROVED">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAPPROVED" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "APPROVED") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                             <asp:BoundField ItemStyle-Width="150px" DataField="APPROVEDDATE" HeaderText="APPROVEDDATE" />
                                               <asp:TemplateField HeaderText="INVOICE DETAILS" HeaderStyle-BackColor="#66ff99">
                                                 <ItemTemplate>
                                                         <asp:GridView ID="grdChild" runat="server" AutoGenerateColumns="false" CssClass = "ChildGrid">
                                                             <Columns>
                                                                  <asp:BoundField ItemStyle-Width="150px" DataField="INVOICENID" HeaderText="INVOICENID" Visible="false" />
                                                                  <asp:BoundField ItemStyle-Width="150px" DataField="" HeaderText="VOUCHERID" Visible="false" />
                                                                 <asp:BoundField ItemStyle-Width="150px" DataField="SLNO" HeaderText="SL NO" />
                                                                 <asp:BoundField ItemStyle-Width="150px" DataField="INVOICENO" HeaderText="INVOICE NO" />
                                                                 <asp:BoundField ItemStyle-Width="150px" DataField="INVOICEAMNT" HeaderText="INVOICE AMNT" />
                                                                 <asp:BoundField ItemStyle-Width="150px" DataField="ADJUSTMENTAMNT" HeaderText="ADJUSTMENT AMNT" />
                                                                 <asp:BoundField ItemStyle-Width="150px" DataField="INVOICETYPE" HeaderText="INVOICE TYPE" />
                                                             </Columns>
                                                         </asp:GridView>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                         </Columns>
                                        </asp:GridView>
                                    </div>
                                </asp:Panel>

                                 </div>
                             </div>
                        </div>
                    <cc1:MessageBox ID="MessageBox1" runat="server" />
                    <span class="clear"></span>
                    </div>
               </ContentTemplate>
          </asp:UpdatePanel>
</asp:Content>

