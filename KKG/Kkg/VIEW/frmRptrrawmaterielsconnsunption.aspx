<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmRptrrawmaterielsconnsunption.aspx.cs" Inherits="VIEW_frmRptrrawmaterielsconnsunption" %>


<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>
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
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
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
                                                    <td>
                                                        <asp:Label ID="Label120" runat="server" Text="Product"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlproduct" runat="server"  class="chosen-select"
                                                            Width="250" data-placeholder="Choose product" AppendDataBoundItems="true">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td width="110" valign="top">
                                                       
                                                            <span class="icon page_white_get_co"></span>
                                                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-success"
                                                                ValidationGroup="Search" OnClick="btnSearch_Click" />
                                                       
                                                    </td>

                                                     <td>

                                                        <span class="icon excel_document"></span>
                                                        <asp:Button ID="btnExport" OnClick="btnExport_Click" runat="server" CssClass="btn btn-primary"
                                                            Text="Excel Download" />

                                                    </td>

                                                </tr>
                                            </table>

                                             <div style="overflow: scroll; height: 600px; width: 100%" id="divProductDetails" runat="server">
                                                <fieldset>
                                                    <legend>RAW MATERIAL CONSUMPTION REPORT</legend>
                                                    <div style="overflow: hidden; width: 100%;" id="DivHeaderRow">
                                                    </div>
                                                    <div style="overflow: scroll; height: 500px; width: 100%" onscroll="OnScrollDiv(this)" id="DivMainContent">

                                                        <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                            <asp:GridView ID="grdRpt" runat="server" Width="100%" CssClass="reportgrid"
                                                                AutoGenerateColumns="true" ShowFooter="true" AlternatingRowStyle-BackColor="AliceBlue"
                                                                EmptyDataText="No Records Available">
                                                            </asp:GridView>
                                                        </table>
                                                    </div>
                                                </fieldset>
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

