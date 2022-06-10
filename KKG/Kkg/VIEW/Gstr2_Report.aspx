<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true"
    CodeFile="Gstr2_Report.aspx.cs" Inherits="VIEW_Gstr2_Report" ClientIDMode="Static" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/bootstrap-multiselect.css" rel="stylesheet" />
    <link href="../css/gijgo.min.css" rel="stylesheet" type="text/css" />
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <style>
              #GridView1 tfoot {
            float: left !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div id="contentarea">
        <div class="grid_container">
            <div class="grid_12">
                <div class="widget_wrap">
                    <div class="widget_top active" id="trBtn" runat="server">
                        <span class="h_icon_he list_images"></span>
                        <h6>Goods and Service Tax - GSTR 2A
                        </h6>
                    </div>

                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">

                        <td>
                            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td class="field_title">
                                        <label id="lblDateFrom">From Date</label>
                                        <span class="req">*</span>
                                    </td>
                                    <td class="field_input">
                                        <input id="txtfromdate" type="text" placeholder="dd/MM/yyyy" style="width: 120px;" readonly />
                                    </td>
                                    <td class="field_title">
                                        <label id="lblDateTo">To Date</label>
                                        <span class="req">*</span>
                                    </td>
                                    <td class="field_input">
                                        <input id="txttodate" type="text" placeholder="dd/MM/yyyy" style="width: 120px;" readonly />
                                    </td>
                                    <td class="field_title">
                                        <label id="lblReportType">Report Type</label>
                                        <span class="req">*</span>
                                    </td>
                                    <td class="field_input">
                                        <select id="ddlReportType" style="width: 150px;">
                                            <option value = "G">GST</option>
                                            <option value = "I">ISD</option>
                                        </select>
                                    </td>
                                    <td class="field_input">
                                        <div class="btn_24_blue">
                                            <span class="icon exclamation_co"></span>

                                            <button type="button" id="btnExport" class="btn_link">Export</button>
                                        </div>
                                    </td>
                                    

                                </tr>



                            </table>
                        </td>

                    </table>
                    <div class="gridcontent" style="overflow-x: auto;">
                        <table id="GridView1"></table>
                    </div>
                </div>
            </div>
            <span class="clear"></span>
        </div>
        <span class="clear"></span>
    </div>



    <div class="modal fade" id="loader" tabindex="-1" role="dialog" aria-labelledby="loaderLabel" aria-hidden="true">
        <div class="modal-dialog modal-sm modal-msg">
            <div class="modal-content">
                <div class="modal-header hidden">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title" id="loaderLabel">Loading</h4>
                </div>
                <div class="modal-body">
                    <i class="fa fa-refresh fa-spin fa-3x text-center"></i>
                    <h3 class="text-center">Loading</h3>
                </div>
            </div>
        </div>
    </div>

    <div id="error-modal" title="Error" style="display: none;">
        There was a problem generating your report, please re-login and try again.
    </div>
    <script type="text/javascript" src="../js/bootstrap.js"></script>
    <script type="text/javascript" src="../js/bootstrap-multiselect.js"></script>
    <script type="text/javascript" src="../js/gijgo.min.js"></script>
    <script type="text/javascript" src="../js/jquery.fileDownload.js"></script>

    <script type="text/javascript">

        $(document).ready(function () {
            var finyear = '<%=Session["FINYEAR"]%>';
            var startyear = finyear.substring(0, 4);
            var endyear = finyear.substring(5, 9);
            var yearstart = "";
            var todayDate = "";
            var todayDate1 = "";
            var today = "";
            var today1 = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate());
            yearstart = new Date(startyear, 03, 01);


            if (today1 >= new Date(startyear, 03, 01) && today1 <= new Date(endyear, 02, 31)) {
                todayDate = ("0" + new Date().getDate()).slice(-2) + "/" + ("0" + (new Date().getMonth() + 1)).slice(-2) + "/" + new Date().getFullYear();
                todayDate1 = ("0" + new Date().getDate()).slice(-2) + "/" + ("0" + (new Date().getMonth() + 1)).slice(-2) + "/" + new Date().getFullYear();
                today = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate());
            }
            else {
                todayDate = ("0" + 1 + "/" + "0" + 4 + "/" + startyear);
                todayDate1 = (31 + "/" + "0" + 3 + "/" + endyear);
                today = new Date(endyear, 02, 31);
            }


            $('#txtfromdate').datepicker({
                format: 'dd/mm/yyyy',
                uiLibrary: 'bootstrap4',
                iconsLibrary: 'fontawesome',
                minDate: yearstart,
                value: todayDate,
                maxDate: function () {
                    return $('#txttodate').val();
                }
            });

            $('#txttodate').datepicker({
                format: 'dd/mm/yyyy',
                uiLibrary: 'bootstrap4',
                iconsLibrary: 'fontawesome',
                value: todayDate1,
                minDate: function () {
                    return $('#txtfromdate').val();
                },
                maxDate: today
            });
        });
        

      

        $('#btnExport').click(function (event) {
            debugger;
            var data = PrepareJSONData("Show Report");
            $("#loader").fadeIn(500).modal('show');
            $.ajax({
                 url: "<%=ServiceURL%>Ledger_Report_Services.asmx/GetGstrReport",
                type: 'POST',
                 data:data,
                contentType: 'application/json;',
                dataType: 'json',
                async: true,
                success: function (result) {
                    $("#loader").fadeOut(200, function () { $("#loader").modal('hide'); })
                    $.fileDownload(result.d);
                },
                error: function (result) {
                    $("#loader").fadeOut(200, function () { $("#loader").modal('hide'); })
                    $("#error-modal").dialog({ modal: true });
                }
            });
        });      

        function PrepareJSONData(CalledBy) {
            $('#GridView1').grid('destroy', true, true);
            $("#loader").fadeIn(500).modal('show');
            var ddlreporttype = $("#ddlReportType option:selected").val();
            var data = JSON.stringify({
                'fromdate': $('#txtfromdate').val(),
                'todate': $('#txttodate').val(),
                'reporttype': ddlreporttype,
                'Caller': CalledBy
            });
            return data;
        }
    </script>
</asp:Content>
