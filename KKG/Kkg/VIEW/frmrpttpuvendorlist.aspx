<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true"
    CodeFile="frmrpttpuvendorlist.aspx.cs" Inherits="VIEW_frmrpttpuvendorlist" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/bootstrap-multiselect.css" rel="stylesheet" />
    <script type="text/javascript" src="../js/bootstrap.js"></script>
    <script type="text/javascript" src="../js/bootstrap-multiselect.js"></script>
    <%--<script src="../js/fixedheader.js" type="text/javascript"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function MakeStaticHeaderWorkStationSource(gridId, height, width, headerHeight, isFooter) {
            debugger;
            var tbl = document.getElementById(gridId);
            if (tbl) {
                var DivHR = document.getElementById('DivHeaderRowWorkStationSource');
                var DivMC = document.getElementById('DivMainContentWorkStationSource');
                var DivFR = document.getElementById('DivFooterRowWorkStationSource');

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



        function OnScrollDivWorkstationSource(Scrollablediv) {
            document.getElementById('DivHeaderRowWorkStationSource').scrollLeft = Scrollablediv.scrollLeft;
            document.getElementById('DivFooterRowWorkStationSource').scrollLeft = Scrollablediv.scrollLeft;
        }
        
    </script>
    <script type="text/javascript">

        $(function () {
            $('#ContentPlaceHolder1_ddlstate').multiselect({
                includeSelectAllOption: true
            });
          
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $('#ContentPlaceHolder1_ddlbsegment').multiselect({
                includeSelectAllOption: true
            });
            $("#ContentPlaceHolder1_ddlbsegment").multiselect('selectAll', false);
            $("#ContentPlaceHolder1_ddlbsegment").multiselect('updateButtonText');
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $('#ContentPlaceHolder1_ddlcategory ').multiselect({
                includeSelectAllOption: true
            });
            $("#ContentPlaceHolder1_ddlcategory").multiselect('selectAll', false);
            $("#ContentPlaceHolder1_ddlcategory").multiselect('updateButtonText');
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $('#ContentPlaceHolder1_ddlbrand').multiselect({
                includeSelectAllOption: true
            });
            $("#ContentPlaceHolder1_ddlbrand").multiselect('selectAll', false);
            $("#ContentPlaceHolder1_ddlbrand").multiselect('updateButtonText');
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $('#ContentPlaceHolder1_ddlunit').multiselect({
                includeSelectAllOption: true
            });
            $("#ContentPlaceHolder1_ddlunit").multiselect('selectAll', false);
            $("#ContentPlaceHolder1_ddlunit").multiselect('updateButtonText');
        });
    </script>
    <script type="text/javascript">
          $(function () {
            $('#ContentPlaceHolder1_ddlspan').multiselect({
                includeSelectAllOption: true
            });
    </script>
    <script type="text/javascript">
        function exportToExcel() {
            grdSaleReport.exportToExcel();
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
                                <h6>
                                    TPU VENDOR LIST
                                </h6>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlDisplay" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                       <%-- <tr>
                                            <td width="90" class="field_title">
                                                <asp:Label ID="Label1" Text="From Date" runat="server"></asp:Label>
                                                <span class="req">*</span>
                                            </td>
                                            <td width="200" class="field_input">
                                                <asp:TextBox ID="txtfromdate" runat="server" Enabled="false" Width="120" placeholder="dd/MM/yyyy"
                                                    MaxLength="10"></asp:TextBox>
                                                <asp:ImageButton ID="Imgfrom" runat="server" ImageUrl="~/images/calendar.png" Width="24px"
                                                    Height="24px" ImageAlign="AbsMiddle" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="Imgfrom" runat="server"
                                                    TargetControlID="txtfromdate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                            <td width="70" class="field_title">
                                                <asp:Label ID="Label2" Text="To Date" runat="server"></asp:Label>
                                                <span class="req">*</span>
                                            </td>
                                            <td width="190" class="field_input">
                                                <asp:TextBox ID="txttodate" runat="server" Enabled="false" Width="120" placeholder="dd/MM/yyyy"
                                                    MaxLength="10"></asp:TextBox>
                                                <asp:ImageButton ID="ImgToDate" runat="server" ImageUrl="~/images/calendar.png" Width="24px"
                                                    Height="24px" ImageAlign="AbsMiddle" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImgToDate" runat="server"
                                                    TargetControlID="txttodate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                            <td class="field_title" width="150">
                                                <asp:Label ID="Label3" runat="server" Text="VENDOR NAME"></asp:Label>
                                            </td>
                                            <td class="field_input" width="100">
                                                <asp:DropDownList ID="ddlvendorname" runat="server" AppendDataBoundItems="True" Width="160"
                                                    Height="28" class="chosen-select" data-placeholder="Choose">                                                   
                                                </asp:DropDownList>
                                            </td>
                                            

                                        </tr>--%>
                                        
                                        <tr> 
                                             <td class="field_title" width="150">
                                                <asp:Label ID="Label4" runat="server" Text="SUPLIEDITEM"></asp:Label>
                                            </td>
                                            

                                             <td width="150">
                                                 <asp:DropDownList ID="ddltype" Width="130" runat="server" class="chosen-select"
                                                     data-placeholder="Choose Product Type">
                                                     <%--<asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                     <asp:ListItem Text="Normal Product" Value="N"></asp:ListItem>
                                                     <asp:ListItem Text="Combo Product" Value="C"></asp:ListItem>--%>
                                                 </asp:DropDownList>
                                                                        </td>
                                            <td width="105" class="field_input">
                                                <div class="btn_24_blue">
                                                    <span class="icon exclamation_co"></span>
                                                    <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn_link" OnClick="btnShow_Click"
                                                        ValidationGroup="Show" />
                                                </div>
                                            </td>

                                            <td width="150" class="field_input" colspan="7">
                                                <div class="btn_24_blue">
                                                    <span class="icon doc_excel_table_co"></span><a href="#" onclick="exportToExcel();">
                                                        <asp:Button ID="btnExport" runat="server" CssClass="btn_link" Text="Export To Excel"
                                                            OnClick="ExportToExcel" /></a>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px;">
                                                <div style="margin: 0 auto; width: 1300px;">
                                                    <div style="overflow: hidden" id="DivHeaderRowWorkStationSource">
                                                    </div>
                                                    <div style="overflow: scroll;" onscroll="OnScrollDivWorkstationSource(this)" id="DivMainContentWorkStationSource">
                                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true" OnRowDataBound="GridView1_RowDataBound"
                                                            EmptyDataText="No Records Available" Width="100%" Visible="true" ShowHeader="true" ShowFooter="true" RowStyle-Height="27px"
                                                            GridLines="Horizontal" CssClass="reportgrid">
                                                            
                                                            <HeaderStyle BackColor="Silver" Font-Bold="True" ForeColor="#3399FF" />
                                                        </asp:GridView>
                                                    </div>
                                                    <div id="DivFooterRowWorkStationSource" style="overflow: hidden">
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
