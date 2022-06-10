<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmRptProductionStatus.aspx.cs" Inherits="VIEW_frmRptProductionStatus" %>


<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                <h3>PRODUCTION STATUS REPORT</h3>
            </div>
                <div id="contentarea">
                       <div class="grid_container">
                           <div class="grid_12">
                                <div class="widget_wrap">
                                      <div class="widget_content">
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
                                                         <td  style="display:none">
                                                            <asp:Label ID="Label120" runat="server" Text="Status"></asp:Label>
                                                        </td>
                                                        <td style="display:none">
                                                            <asp:DropDownList ID="ddlstatus" Width="100" runat="server" class="chosen-select"
                                                                 data-placeholder="Choose Waybill Filter">
                                                                <asp:ListItem Text="Select All" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="Pending" Selected="True" Value="N"></asp:ListItem>
                                                                <asp:ListItem Text="Confirm" Value="Y"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="110" valign="top">
                                                            <div class="btn_24_blue">
                                                                <span class="icon find_co"></span>
                                                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn_link"
                                                                    ValidationGroup="Search" OnClick="btnSearch_Click"/>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                                 
                                                         <div style="overflow: scroll; height: 350px; width: 100%;">
                                                      <asp:GridView ID="grdProduction" runat="server" AutoGenerateColumns="true"
                                                          CssClass="reportgrid"  EmptyDataText="No Records Available"
                                                          ShowFooter="true" AlternatingRowStyle-BackColor="AliceBlue">

                                                      </asp:GridView>
                                                             </div>
                                                      
                                            </td>
                                        </tr>
                                    </table>
                                       </div>
                                    </div>
                               </div>
                            <cc1:MessageBox ID="MessageBox1" runat="server" />
                               <span class="clear"></span>
                           </div>
                    </div>
              </ContentTemplate>
         </asp:UpdatePanel>
</asp:Content>

