<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true"
    CodeFile="frmExportExcel.aspx.cs" Inherits="VIEW_frmExportExcel" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/bootstrap-multiselect.css" rel="stylesheet" />
    <script type="text/javascript" src="../js/bootstrap.js"></script>
    <script type="text/javascript" src="../js/bootstrap-multiselect.js"></script>
    <script src="../js/table2excel.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


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
    <style>
        .hidden {
            display: none;
        }
    </style>
    <script type="text/javascript">

        $("body").on("click", "#btnExport", function () {

            alert("File Getting Ready Click " + " Ok " + " and Wait for Some time");
            $("[id*=gvRptProgress]").table2excel({
                filename: "PoVsGrnQtyReport.xls"
            });


        });


        function ValidateListBox(sender, args) {
            args.IsValid = false;
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
            $('#ContentPlaceHolder1_ddldepot').multiselect({
                includeSelectAllOption: true
            });
            $("#ContentPlaceHolder1_ddldepot").multiselect('selectAll', false);
            $("#ContentPlaceHolder1_ddldepot").multiselect('updateButtonText');
        });


        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_beginRequest(BeginRequestHandler);
        prm.add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args) {
            var popup = $find('<%= modalPopup.ClientID %>');
            if (popup != null) {
                popup.show();
                $('#loading').show();
            }
        }

        function EndRequestHandler(sender, args) {
            var popup = $find('<%= modalPopup.ClientID %>');
            if (popup != null) {
                popup.hide();
                $('#loading').hide();
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
        <ContentTemplate>
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>Export Excel Report
                                </h6>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlDisplay" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="90" class="field_title">
                                                            <asp:Label ID="Label1" Text="From Date" runat="server"></asp:Label>
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
                                                        </td>
                                                        <td class="field_input">
                                                            <asp:TextBox ID="txttodate" runat="server" Enabled="false" Width="85" placeholder="dd/MM/yyyy"
                                                                MaxLength="10"></asp:TextBox>
                                                            <asp:ImageButton ID="ImgToDate" runat="server" ImageUrl="~/images/calendar.png" Width="24px"
                                                                Height="24px" ImageAlign="AbsMiddle" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImgToDate" runat="server"
                                                                TargetControlID="txttodate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                        <td class="field_title" width="90">
                                                            <asp:Label ID="Label4" Text="Depot" runat="server"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td class="field_input">
                                                            <asp:ListBox ID="ddldepot" runat="server" SelectionMode="Multiple" TabIndex="3" AppendDataBoundItems="True"
                                                                ValidationGroup="Show"></asp:ListBox>
                                                            <asp:CustomValidator ID="CustomValidator3" runat="server" ValidateEmptyText="true"
                                                                ControlToValidate="ddldepot" ValidationGroup="Show" ErrorMessage="Required!"
                                                                Display="Dynamic" ClientValidationFunction="ValidateListBox" ForeColor="Red"></asp:CustomValidator>
                                                        </td>
                                                    </tr>
                                                    <td>
                                                        <div class="btn_24_blue">
                                                            <span class="icon exclamation_co"></span>
                                                            <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn_link" OnClick="btnShow_Click"
                                                                ValidationGroup="Show" />
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="btn_24_blue" id="DivbtnExportDetails" runat="server">
                                                            <span class="icon doc_excel_table_co"></span>
                                                            <%--  <asp:Button ID="btnExport" runat="server" Text="Export Excel" CssClass="btn_link"/>--%>
                                                            <input type="button" id="btnExport" value="ExportExcel" style="color: black" />
                                                        </div>
                                                    </td>
                                                </table>



                                                <div style="overflow: scroll; height: 500px; width: 100%" onscroll="OnScrollDiv(this)" id="DivMainContent">
                                                    <fieldset>
                                                        <legend>PO VS GRN QTY REPORT</legend>
                                                        <div style="overflow: hidden; width: 100%;" id="DivHeaderRow">
                                                        </div>

                                                        <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                            <asp:GridView ID="gvRptProgress" AutoGenerateColumns="true" runat="server" CssClass="reportgrid">
                                                            </asp:GridView>
                                                        </table>
                                                    </fieldset>
                                                </div>




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

