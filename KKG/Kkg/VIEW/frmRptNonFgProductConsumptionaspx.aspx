<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmRptNonFgProductConsumptionaspx.aspx.cs"
    Inherits="VIEW_frmRptNonFgProductConsumptionaspx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/bootstrap-multiselect.css" rel="stylesheet" />
    <script type="text/javascript" src="../js/bootstrap.js"></script>
    <script type="text/javascript" src="../js/bootstrap-multiselect.js"></script>
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
    <style type="text/css">
        .messagealert {
            width: 30%;
            position: absolute;
            top: 0px;
            padding-left: inherit;
            font-size: 15px;
            color: black;
            font-family: emoji;
        }
    </style>

    <style type="text/css">
        .alert-danger {
            color: orange;
            background-color: rgb(253, 247, 247);
            border-color: rgb(217, 83, 79);
        }
    </style>

    <script type="text/javascript">
        function ShowMessage(message, messagetype) {
            var cssclass;
            switch (messagetype) {
                case 'Success':
                    cssclass = 'alert-success'
                    break;
                case 'Error':
                    cssclass = 'alert-danger'
                    break;
                case 'Warning':
                    cssclass = 'alert-warning'
                    break;
                default:
                    cssclass = 'alert-info'
            }
            $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #fff; background-color: #BEF3B1; float:left;" class="alert fade in ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>');

            setTimeout(function () {
                $("#alert_div").fadeTo(2000, 500).slideUp(500, function () {
                    $("#alert_div").remove();
                });
            }, 1000);
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
                <h3>Consumption Report</h3>
            </div>
            <div id="contentarea">
                <div class="grid_container">
                    <div class="widget_wrap">
                        <div class="widget_content">


                            <asp:Panel ID="pnlAdd" runat="server">
                                <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                    <tr>
                                        <td class="field_input">
                                            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                <tr>

                                                    <td class="field_title">
                                                        <asp:Label ID="Label8" runat="server" Text="From Date"></asp:Label>
                                                    </td>
                                                    <td width="23%">
                                                        <asp:TextBox ID="txtReqFromDate" runat="server" Width="70" placeholder="dd/mm/yyyy" MaxLength="10" Enabled="false"></asp:TextBox>
                                                        <asp:ImageButton ID="ImageButtonReqFromDate" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle" runat="server" Height="24" />
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender6" PopupButtonID="ImageButtonReqFromDate"
                                                            runat="server" TargetControlID="txtReqFromDate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                        </ajaxToolkit:CalendarExtender>
                                                    </td>
                                                    <td class="field_title">
                                                        <asp:Label ID="Label28" runat="server" Text="To Date"></asp:Label>
                                                    </td>
                                                    <td width="23%">
                                                        <asp:TextBox ID="txttoDate" runat="server" Width="70" placeholder="dd/mm/yyyy" MaxLength="10" Enabled="false"></asp:TextBox>
                                                        <asp:ImageButton ID="ImageButtontoDate" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle" runat="server" Height="24" />
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtendertoDate" PopupButtonID="ImageButtontoDate"
                                                            runat="server" TargetControlID="txttoDate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                        </ajaxToolkit:CalendarExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>

                                                        <span class="icon page_white_get_co"></span>
                                                        <asp:Button ID="btnShowReport" runat="server" Text="Show Report" CssClass="btn btn-success" OnClick="btnShowReport_Click" />
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
                                                    <legend>CONSUMPTION REPORT</legend>
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
                            </asp:Panel>

                        </div>
                    </div>
                    <div class="messagealert" id="alert_container">
                    </div>
                    <span class="clear"></span>
                </div>
                <span class="clear"></span>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

