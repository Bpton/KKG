<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmProductionReport.aspx.cs" Inherits="VIEW_frmProductionReport" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <link href="../css/bootstrap-multiselect.css" rel="stylesheet" />
        <script src="../js/table2excel.js"></script>
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
        $("body").on("click", "#btnExport", function () {
            alert("File Getting Ready Click "+" Ok "+" and Wait for Some time");
            $("[id*=grdRpt]").table2excel({
                filename: "Production Order Rreport.xls"
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
     <asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
            <asp:Image ID="Image1" ImageUrl="../images/103.gif" AlternateText="Processing"
                runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
     <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
        PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="page_title">
                <span class="title_icon"><span class="computer_imac"></span></span>
                <h3>Production Report</h3>
            </div>
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_content">
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
                                                         <td width="60">
                                                            <asp:Label ID="Label120" runat="server" Text="Status"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlstatus" Width="100" runat="server" class="chosen-select"
                                                                 data-placeholder="Choose Waybill Filter">
                                                                <asp:ListItem Text="Select All" Selected="True" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="START"  Value="S"></asp:ListItem>
                                                                <asp:ListItem Text="PENDING" Value="P"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="23%" valign="top" colspan="4">
                                                            <div class="btn_24_blue">
                                                                <span class="icon find_co"></span>
                                                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn_link"
                                                                    ValidationGroup="Search" OnClick="btnSearch_Click"  />
                                                            </div>
                                                        </td>
                                                        <td width="18%"  colspan="2">
                                                             <div class="btn_24_green">
                                                              <span class="icon excel_document_b"></span>
                                                               <input type="button" id="btnExport" style="color:#1D6F42" value="EXCEL"/>
                                                                 </div>
                                                          
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>


                               
                                            <div style="overflow: hidden; width: 100%;" id="Div2">
                                            </div>
                                    <div style="overflow: scroll; height: 350px; width: 100%;">
                                            <asp:GridView ID="grdRpt" runat="server" Width="100%" CssClass="zebra"
                                                AutoGenerateColumns="false" ShowFooter="false" AlternatingRowStyle-BackColor="AliceBlue"
                                                EmptyDataText="No Records Available" >
                                                <Columns>
                                                    <asp:BoundField DataField="PRODUCTION_ORDER_NUMBER" HeaderText="PRODUCTION_ORDER_NUMBER" HeaderStyle-Wrap="false" />
                                                    <asp:BoundField DataField="PRODUCTION_START_DATE" HeaderText="DATE" HeaderStyle-Wrap="false" />
                                                    <asp:BoundField DataField="ITEMNAME" HeaderText="PRODUCTION ITEMNAME" HeaderStyle-Wrap="false" />
                                                    <asp:BoundField DataField="DEPTNAME" HeaderText="DEPTNAME" HeaderStyle-Wrap="false" />
                                                    <asp:BoundField DataField="PROCESSSTATUS" HeaderText="PROCESSSTATUS" HeaderStyle-Wrap="false" />
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
                <span class="clear"></span>
            </div>

            <script type="text/javascript">
                
                    function isNumberKeyWithDot(evt) {
                        var charCode = (evt.which) ? evt.which : event.keyCode;
                        if ((charCode < 48 || charCode > 57) && (charCode != 8) && (charCode != 46) && (charCode != 45))
                            return false;

                        return true;
                    }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

