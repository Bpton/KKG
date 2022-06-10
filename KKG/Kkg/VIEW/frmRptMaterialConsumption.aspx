<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmRptMaterialConsumption.aspx.cs" Inherits="VIEW_frmRptMaterialConsumption" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/bootstrap-multiselect.css" rel="stylesheet" />
    <link href="../css/gijgo.min.css" rel="stylesheet" type="text/css" />
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        a.jp-current, a.jp-current:hover {
            color: #3349FF !important;
            font-weight: bold !important;
            background-color: darkkhaki !important;
        }

        a.jp-disabled, a.jp-disabled:hover {
            color: #bbb;
        }

            a.jp-current, a.jp-current:hover,
            a.jp-disabled, a.jp-disabled:hover {
                cursor: default;
                background: none;
            }
    </style>
    <style>
          #GridView1 {
            width: 100%;
            background-color: #fff;
            margin: 5px 0 10px 0;
            border: solid 1px #525252;
            border-collapse: collapse;
        }

            #GridView1 td {
                padding: 2px;
                border: solid 1px #c1c1c1;
                color: #717171;
            }

            #GridView1 th {
                padding: 4px 2px;
                color: #fff;
                background: #424242;
                border-left: solid 1px #525252;
                font-size: 0.9em;
            }

            #GridView1 .gridview_alter {
                background: #E7E7E7;
            }

            #GridView1 .gridview_pager {
                background: #424242;
            }

                #GridView1 .gridview_pager table {
                    margin: 5px 0;
                }

                #GridView1 .gridview_pager td {
                    border-width: 0;
                    padding: 0 6px;
                    border-left: solid 1px #666;
                    font-weight: bold;
                    color: #fff;
                    line-height: 12px;
                }

                #GridView1 .gridview_pager a {
                    color: #666;
                    text-decoration: none;
                }

                    #GridView1 .gridview_pager a:hover {
                        color: #000;
                        text-decoration: none;
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
                        <h6>Material Consumption Report
                        </h6>
                    </div>
                    <div class="widget_content">
                        <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">

                            <tr>
                                <td width="70" class="field_title">
                                    <label id="lblFromDateTo">FROM DATE</label>
                                    <span class="req">*</span>
                                </td>
                                <td width="190" class="field_input">
                                    <input id="txtfromdate" type="text" disabled="disabled" placeholder="dd/MM/yyyy" style="width: 120px;" mind />
                                </td> 

                                <td width="70" class="field_title">
                                    <label id="lblDateTo">TO DATE</label>
                                    <span class="req">*</span>
                                </td>
                                <td width="190" class="field_input">
                                    <input id="txttodate" type="text" placeholder="dd/MM/yyyy" disabled="disabled" style="width: 120px;" mind />
                                </td>

                                <td class="field_title"  runat="server">
                                        <label id="lblBS">Product</label>
                                        <span class="req">*</span>

                                    </td>
                                    <td class="field_input"  runat="server">
                                        <select id="ddlProduct" class="chosen-search" style="width: 250px;" >
                                        </select>
                                    </td>

                                <td width="105" class="field_input" colspan="2">
                                    <div class="btn_24_blue">
                                        <span class="icon exclamation_co"></span>
                                        <button type="button" id="btnShowData" class="btn_link">Show Report</button>
                                    </div>
                                  <div class="btn_24_blue">
                                            <span class="icon doc_excel_table_co"></span>
                                            <button type="button" id="btnExportExcel"
                                                onclick="tableToExcel('GridView1', 'abcd')" class="btn_link">
                                                Excel</button>
                                        </div>
                                </td>


                            </tr>
                        </table>

                        <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td" align="center">
                            <tr>
                                <td class="field_input" style="padding-left: 10px;">
                                    <div style="margin: 0 auto; width: 1200px;">
                                        <div style="overflow: hidden;" id="DivHeaderRow">
                                            <div class="team-callback team-callback01 pull-left"></div>
                                            <div class="team-callback team-callback02 pull-left"></div>
                                        </div>
                                        <div id="DivMainContent" style="overflow: scroll;">
                                        </div>
                                        <ul class="pagination pull-right"></ul>
                                        <div id="DivFooterRow" style="overflow: hidden;">
                                            <div class="team-callback team-callback01 pull-left"></div>
                                            <div class="team-callback team-callback02 pull-left"></div>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                         <div class="gridcontent" style="overflow-x: auto;">
                        <table id="GridView1"></table>
                    </div>
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

    <script type="text/javascript" src="../js/bootstrap.js"></script>
    <script type="text/javascript" src="../js/bootstrap-multiselect.js"></script>
    <script type="text/javascript" src="../js/gijgo.min.js"></script>
    <script type="text/javascript" src="../js/jquery.fileDownload.js"></script>

    <script type="text/javascript">
         var today = "";
        $(document).ready(function () {
            bindProduct();
            $('#btnExportExcel').prop('disabled', true);
            $('#btnexportDiv').removeClass("btn_24_blue");
            $('#btnexportDiv').addClass("btn_30_light");

            var finyear = '<%=Session["FINYEAR"]%>';
            var startyear = finyear.substring(0, 4);
            var endyear = finyear.substring(5, 9);
            var yearstart = "";
            var todayDate = "";
            var todayDate1 = "";
           
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


            $('#txttodate').datepicker({
                format: 'dd/mm/yyyy',
                uiLibrary: 'bootstrap4',
                iconsLibrary: 'fontawesome', 
                 value: todayDate1,
                maxDate: today
            });

            $('#txtfromdate').datepicker({
                format: 'dd/mm/yyyy',
                uiLibrary: 'bootstrap4',
                iconsLibrary: 'fontawesome',
                value: todayDate1,
               
              
            });
        });

        

        function LoadCalendarDates() {
            var SpanID = [];
            $("#ddlspan option:selected").each(function () {
                SpanID.push(this.value);
            });


            $.ajax({
                url: "<%=ServiceURL%>Ledger_Report_Services.asmx/GetCalendarDates",
                type: 'POST',
                data: JSON.stringify({ 'TimeSpan': SpanID.toString(), 'CalendarType': $("#ddlcalender option:selected").val() }),
                contentType: 'application/json;',
                dataType: 'json',
                async: true,
                success: function (result) {
                    if (result != null) {
                        if (result.d.TimeSpan.length > 0) {
                            $('#txtfromdate').val(result.d.TimeSpan[0]["STARTDATE"]);
                            $('#txttodate').val(result.d.TimeSpan[0]["ENDDATE"])
                        }
                        $("#loader").fadeOut(200, function () { $("#loader").modal('hide'); })

                    }
                    else {
                    }
                },
                error: function (result) {
                    $("#loader").fadeOut(200, function () { $("#loader").modal('hide'); })
                    $("#error-modal").dialog({ modal: true });
                }
            });
        }

        $('#btnShowData').click(function (event) {
            debugger;
            $("#DivMainContent").html("");
            $('#btnExportExcel').prop('disabled', true);
            $('#btnexportDiv').removeClass("btn_24_blue");
            $('#btnexportDiv').addClass("btn_30_light");
            $('.pagination').html("");
            $('.team-callback01').html("");
            $('.team-callback02').html("");
            LoadReport("Show Report", 1);
        });

        function LoadReport(CalledBy, PageNo) {
            debugger;
            var data = PrepareJSONData(CalledBy, PageNo);
            $.ajax({
                url: "<%=ServiceURL%>Ledger_Report_Services.asmx/bindConSumptionReport",
                type: 'POST',
                data: data,
                contentType: 'application/json;',
                dataType: 'json',
                async: true,
                success: function (result) {
                    debugger
                    if (result != null) {
                        if (result.d.ParentData.length > 0) {
                            $('#btnExportExcel').prop('disabled', false);
                            $('#btnexportDiv').removeClass("btn_30_light");
                            $('#btnexportDiv').addClass("btn_24_blue");
                        }
                        else {
                            $('#btnExportExcel').prop('disabled', true);
                            $('#btnexportDiv').addClass("btn_30_light");
                            $('#btnexportDiv').removeClass("btn_24_blue");
                        }
                        BindGrid(result);
                        $("#loader").fadeOut(200, function () { $("#loader").modal('hide'); })

                    }
                    else {
                        $("#loader").fadeOut(200, function () { $("#loader").modal('hide'); })
                        $('#btnExportExcel').prop('disabled', true);
                        $('#btnexportDiv').addClass("btn_30_light");
                        $('#btnexportDiv').removeClass("btn_24_blue");
                        $('.team-callback01').html('');
                        $('.team-callback02').html('');
                    }
                },
                error: function (result) {
                    $("#loader").fadeOut(200, function () { $("#loader").modal('hide'); })
                    $("#error-modal").dialog({ modal: true });
                    $('.team-callback01').html('');
                    $('.team-callback02').html('');
                }
            });
        }

        function PrepareJSONData(CalledBy, PageNo) {
            $("#GridView1").grid('destroy', true, true);
             $("#loader").fadeIn(500).modal('show');
            var data = JSON.stringify({
                'productid': $('#ddlProduct').val(),
                'fromdate': $('#txtfromdate').val(),
                'todate': $('#txttodate').val(),
                'Caller': CalledBy,
                'PageNo': PageNo
            });
            return data;
        }

        function BindGrid(result)
        {
            var grid = $('#GridView1').grid({
                dataSource: result.d.ParentData,
                title:'Material Consumption Report',
                selectionType: 'checkbox',
                fixedHeader: true,
                uiLibrary: 'bootstrap',
                width: 1900,
                /*headerFilter: true,*/
                autoGenerateColumns: true,
                detailTemplate: '<div><table/></div>'
                
                
            });
        }

        function bindProduct() {

            var listItems = "";
            $.ajax({
                url: "<%=ServiceURL%>Ledger_Report_Services.asmx/GetProductExceptFGWP",
                type: 'POST',
                contentType: 'application/json;',
                dataType: 'json',
                async: true,
                success: function (result) {
                    if (result != null) {
                        if (result.d.Product.length > 0) {
                             listItems = "<option value='0'>Select</option>";
                            for (var i = 0; i < result.d.Product.length; i++) {
                                listItems += "<option value='" + result.d.Product[i]["ID"] + "'>" + result.d.Product[i]["NAME"] + "</option>";
                            }

                            $("#ddlProduct").html(listItems);
                        }
                        else {
                        }
                        $("#loader").fadeOut(200, function () { $("#loader").modal('hide'); })

                    }
                    else {
                    }
                },
                error: function (result) {
                    $("#loader").fadeOut(200, function () { $("#loader").modal('hide'); })
                    $("#error-modal").dialog({ modal: true });
                }
            });

        }

       

       var tableToExcel = (function () {
            debugger;
            var uri = 'data:application/vnd.ms-excel;base64,'
                , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>'
                , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
                , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
            name = "acsd"
            return function (table, name) {
                if (!table.nodeType) table = document.getElementById(table)
                var ctx = "<B>KKG III "+''+ today+"</B> </tr><tr>"
                ctx += "<B>Material Consumption Report</B> </tr><tr>"
                var ctx1 = { worksheet: name || 'Worksheet', table: table.innerHTML + ctx }
                window.location.href = uri + base64(format(template, ctx1))
            }
        })()


    </script>
</asp:Content>



