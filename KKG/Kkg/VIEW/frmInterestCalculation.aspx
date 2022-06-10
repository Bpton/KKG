<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmInterestCalculation.aspx.cs" Inherits="VIEW_frmInterestCalculation" %>

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

    <script type="text/javascript">
        function validateFloatKeyPress(el, evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;

            if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }

            if (charCode == 46 && el.value.indexOf(".") !== -1) {
                return false;
            }

            if (el.value.indexOf(".") !== -1) {
                var range = document.selection.createRange();

                if (range.text != "") {
                }
                else {
                    var number = el.value.split('.');
                    if (number.length == 2 && number[1].length > 1)
                        return false;
                }
            }

            return true;
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
                                <h6>Interest Calculation</h6>
                            </div>
                            <div class="widget_content">

                                <asp:Panel ID="pnlDisplay" runat="server">
                                    <tr>
                                        <td>
                                            <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                                <td width="150" class="field_title">
                                                    <asp:Label ID="lblstate" runat="server" Text="Ledger (Unsecured Loan)"></asp:Label>
                                                </td>
                                                <td width="150" class="field_title">
                                                    <asp:DropDownList ID="ddlLEDGER" runat="server" Width="400" Height="28" class="chosen-select" data-placeholder="Select Ledger"
                                                        AppendDataBoundItems="true">
                                                    </asp:DropDownList>
                                                </td>

                                                <td>&nbsp;</td>

                                                 <td class="field_title" width="100">
                                                                <asp:Label ID="Label11" runat="server" Text="Interest % "></asp:Label>
                                                            </td>
                                                            <td class="field_input" width="50">
                                                                <asp:TextBox ID="txtpercentage" runat="server" MaxLength="5" Text="12.00"  onkeypress="return validateFloatKeyPress(this,event);"></asp:TextBox>
                                                            </td>

                                                <td class="field_title" width="100">
                                                                <asp:Label ID="Label9" runat="server" Text="DATE (As on)"></asp:Label>
                                                            </td>

                                                <td>&nbsp;</td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtdate" runat="server" Width="100" Enabled="false" placeholder="dd/mm/yyyy"
                                                                    MaxLength="10" ValidationGroup="ADD" onkeypress="return isNumberKeyWithslash(event);"></asp:TextBox>
                                                                <asp:ImageButton ID="imgPopup" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender4" PopupButtonID="imgPopup" runat="server"
                                                                    TargetControlID="txtdate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>

                                                     <td>&nbsp;</td>

                                                <td class="field_input" > 
                                                    <div class="btn_24_blue">
                                                        <span class="icon exclamation_co"></span>
                                                        <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn_link" OnClick="btnshow_click"
                                                            ValidationGroup="Show" />
                                                    </div>
                                                </td>

                                                <td class="field_input" style="padding-left: 10px;" >
                                                            <div class="btn_24_blue">
                                                                <span class="icon doc_excel_table_co"></span>
                                                                <asp:Button ID="btnExport" runat="server" CssClass="btn_link" Text="Export To Excel"
                                                                   onclick="Export_To_Excel"  />
                                                            </div>
                                                        </td>

                                            </table>
                                        </td>
                                    </tr>

                                    <tr><td>&nbsp&nbsp</td></tr>

                                    <div class="gridcontent">
                                        <div style="overflow: hidden; width: 100%;" id="DivHeaderRow">
                                        </div>
                                        <div style="overflow: scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent">
                                            <asp:GridView ID="grdinterest" runat="server" AutoGenerateColumns="true" CssClass="reportgrid" Width="100%" RowStyle-Height="30"
                                                ShowFooter="false" Visible="true" GridLines="Horizontal">
                                                <FooterStyle Font-Bold="True" ForeColor="White" Height="30px" Width="20px" />

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
                <script type="text/javascript">
                    function exportToExcel() {
                        gvCity.exportToExcel();
                    }
                </script>
                <script type="text/javascript">


</script>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
