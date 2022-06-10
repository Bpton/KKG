<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmRptPartyOutstandingReport.aspx.cs" Inherits="VIEW_frmRptPartyOutstandingReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../css/bootstrap-multiselect.css" rel="stylesheet" />
    <script type="text/javascript" src="../js/bootstrap.js"></script>
    <script type="text/javascript" src="../js/bootstrap-multiselect.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
        function ValidateListBox(sender, args) {
            var options = document.getElementById("<%=ddldepot.ClientID%>").options;
            for (var i = 0; i < options.length; i++) {
                if (options[i].selected == true) {
                    args.IsValid = true;
                    return;
                }
            }
            args.IsValid = false;
        }
    </script>
    <script type="text/javascript">
        $(function () {
//            $('#ContentPlaceHolder1_ddldepot').multiselect({
//                includeSelectAllOption: true
//            });
//            $("#ContentPlaceHolder1_ddldepot").multiselect('selectAll', false);
            $("#ContentPlaceHolder1_ddldepot").multiselect('updateButtonText');
        });

        $(function () {
            $('#ContentPlaceHolder1_ddlspan').multiselect({
                includeSelectAllOption: true
            });
            $("#ContentPlaceHolder1_ddlspan").multiselect('selectAll', false);
            $("#ContentPlaceHolder1_ddlspan").multiselect('updateButtonText');
        });

        $(function () {
            $('#ContentPlaceHolder1_ddlbsegment').multiselect({
                includeSelectAllOption: true
            });
                        $("#ContentPlaceHolder1_ddlbsegment").multiselect('selectAll', false);
                        $("#ContentPlaceHolder1_ddlbsegment").multiselect('updateButtonText');
        });

        $(function () {
            $('#ContentPlaceHolder1_ddlledger').multiselect({
                includeSelectAllOption: true
            });
            //            $("#ContentPlaceHolder1_ddlbsegment").multiselect('selectAll', false);
            //            $("#ContentPlaceHolder1_ddlbsegment").multiselect('updateButtonText');
        });

    </script>
    <script type="text/javascript">
        function exportToExcel() {
            gvSummaryreport.exportToExcel();
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
                                    Party Outstanding Report
                                </h6>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlDisplay" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td>
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td class="field_title" width="200">
                                                            <asp:Label ID="Label1" runat="server" Text="From Date"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td class="field_input" width="200" colspan="2">
                                                            <asp:TextBox ID="txtfromdate" runat="server" Enabled="false" MaxLength="10" 
                                                                placeholder="dd/MM/yyyy" ValidationGroup="sales" Width="120"></asp:TextBox>
                                                            <asp:ImageButton ID="Imgfrom" runat="server" Height="24px" 
                                                                ImageAlign="AbsMiddle" ImageUrl="~/images/calendar.png" ValidationGroup="sales" 
                                                                Width="24px" />
                                                        </td>
                                                        <td class="field_title" width="200">
                                                            <asp:Label ID="Label2" runat="server" Text="To Date"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td class="field_input" width="200">
                                                            <asp:TextBox ID="txttodate" runat="server" Enabled="false" MaxLength="10" 
                                                                placeholder="dd/MM/yyyy" ValidationGroup="sales" Width="120"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" 
                                                                CssClass="cal_Theme1" Format="dd/MM/yyyy" PopupButtonID="ImgToDate" 
                                                                TargetControlID="txttodate">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <asp:ImageButton ID="ImgToDate" runat="server" Height="24px" 
                                                                ImageAlign="AbsMiddle" ImageUrl="~/images/calendar.png" ValidationGroup="sales" 
                                                                Width="24px" />
                                                        </td>
                                                        <td width="90" class="field_title">
                                                            <asp:Label ID="Label4" runat="server" Text="Group By"></asp:Label>
                                                        </td>
                                                        <td width="200" class="field_input">
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="Imgfrom" runat="server"
                                                                TargetControlID="txtfromdate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <asp:DropDownList ID="ddlgroupby" runat="server" AppendDataBoundItems="True" 
                                                                class="chosen-select" data-placeholder="Select" Width="140">
                                                                <asp:ListItem Selected="True" Text="Transaction Details" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Opening Breakup" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="On Account Receipts" Value="3"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="70" class="field_title">
                                                            &nbsp;</td>
                                                        <td width="100" class="field_input">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="field_title" width="100">
                                                            <asp:Label ID="Label5" runat="server" Text="Depot"></asp:Label>
                                                        </td>
                                                        <td class="field_input" width="100" colspan="2">
                                                            <%--<asp:CustomValidator ID="CustomValidator12" runat="server" 
                                                                ClientValidationFunction="ValidateDropDownList" ControlToValidate="ddlbsegment" 
                                                                Display="Dynamic" ErrorMessage="Required!" ForeColor="Red" 
                                                                ValidateEmptyText="true" ValidationGroup="Show"></asp:CustomValidator>--%>
                                                            <asp:ListBox ID="ddldepot" runat="server" AppendDataBoundItems="True" 
                                                                TabIndex="4" 
                                                                ValidationGroup="ADD" Width="200%" 
                                                                OnSelectedIndexChanged="ddldepot_OnSelectedIndexChanged"></asp:ListBox>
                                                        </td>
                                                        <td width="100" class="field_title">                                                            
                                                            <asp:Label ID="Label12" runat="server" Text="Group"></asp:Label>
                                                            <span class="req">*</span></td>
                                                        <td width="150" class="field_input">
                                                            <asp:CustomValidator ID="CustomValidator12" runat="server" 
                                                                ClientValidationFunction="ValidateListBox" ControlToValidate="ddlgroup" 
                                                                Display="Dynamic" ErrorMessage="Required!" ForeColor="Red" 
                                                                ValidateEmptyText="true" ValidationGroup="Show"></asp:CustomValidator>
                                                            <br />
                                                            <asp:ListBox ID="ddlgroup" runat="server" AppendDataBoundItems="True" 
                                                                TabIndex="4" 
                                                                ValidationGroup="sales" AutoPostBack="True" 
                                                                onselectedindexchanged="ddlgroup_SelectedIndexChanged"></asp:ListBox>
                                                        </td>
                                                         <td class="field_title" width="150">
                                                             <asp:Label ID="Label3" runat="server" Text="Ledger"></asp:Label>
                                                        </td>
                                                        <td class="field_input" width="150">
                                                            <asp:ListBox ID="ddlledger" runat="server" AppendDataBoundItems="True"
                                                            TabIndex="4"
                                                                ValidationGroup="sales">
                                                            </asp:ListBox>
                                                        </td>
                                                        <td class="field_title" width="150">
                                                            <asp:Label ID="Label7" runat="server" Text="Business Segment" Visible="False"></asp:Label>
                                                        </td>
                                                        <td class="field_input" width="100">
                                                            <asp:DropDownList ID="ddlamt" runat="server" AppendDataBoundItems="True" 
                                                                class="chosen-select" data-placeholder="Choose" Height="28px" Visible="False" 
                                                                Width="65px">
                                                                <asp:ListItem Selected="True" Text="INR" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="THOUSAND" Value="1000"></asp:ListItem>
                                                                <asp:ListItem Text="LAKH" Value="100000"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:ListBox ID="ddlbsegment" runat="server" AppendDataBoundItems="True" 
                                                                AutoPostBack="True" multiple="multiple" SelectionMode="Multiple" TabIndex="4" 
                                                                ValidationGroup="sales" 
                                                                
                                                                Visible="False">
                                                            </asp:ListBox>
                                                        </td>                                                     
                                                    </tr>

                                                    
                                                    <tr>
                                                       <td width="105" class="field_input">
                                                            <div class="btn_24_blue">
                                                                <span class="icon exclamation_co"></span>
                                                                <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn_link" OnClick="btnShow_Click"
                                                                    ValidationGroup="sales" />
                                                            </div>
                                                           
                                                        </td>
                                                        <td width="150" class="field_input">
                                                         <div class="btn_24_blue">
                                                                <span class="icon doc_excel_table_co"></span>
                                                               <a href="#">
                                                                     <asp:Button ID="btnExport" runat="server"  CssClass="btn_link" Text="Export To Excel" OnClick = "btnExport_Click" /></a>
                                                            </div>
                                                        </td>
                                                        <td class="field_input" style="width: 75px" width="150">
                                                            &nbsp;</td>
                                                    </tr>                                                    
                                                </table>
                                            </td>
                                        </tr>
                                    </table>

                                   <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td"
                                        align="center" >
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px;">
                                                <div style="margin: 0 auto; width:1100px;">
                                                    <div style="overflow: hidden;" id="DivHeaderRow">
                                                    </div>
                                                    <div id="DivMainContent" style="overflow: scroll;" onscroll="OnScrollDiv(this);">
                                                        <asp:GridView ID="gvSummaryreport" runat="server"
                                                            Width="100%" ShowFooter="True" GridLines="Horizontal" 
                                                            OnRowDataBound="gvSummaryreport_OnRowDataBound" RowStyle-Height="27px" FooterStyle-Height="27px"
                                                           CssClass="reportgrid" EmptyDataText="No Records Available....." 
                                                            AutoGenerateColumns="true">
                                                            <FooterStyle Height="27px" />
                                                            <HeaderStyle BackColor="Silver" Font-Bold="True" ForeColor="#000000" />
                                                            <RowStyle Height="27px" />
                                                        </asp:GridView>
                                                        
                                                        <div class="clear">
                                                        </div>
                                                    </div>
                                                    <div id="DivFooterRow" style="overflow: hidden;">
                                                        <asp:GridView ID="grdledger_for_excel" runat="server" Width="100%" 
                                                            AutoGenerateColumns="false" OnRowDataBound="grdledger_RowDataBound"
                                                 EmptyDataText="No Records Available" Visible="false" CssClass="reportgrid">
                                                        </asp:GridView>
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

