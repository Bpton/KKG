<%@ page title="" language="C#" masterpagefile="~/VIEW/frmChildMaster.master" autoeventwireup="true" CodeFile="frmRptAgeinganalysis.aspx.cs" inherits="VIEW_frmRptAgeinganalysis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/bootstrap-multiselect.css" rel="stylesheet" />
    <link href="../css/gijgo.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <style>		
        #grdageing {
            border: 0px solid #B9C9D7;
            position: relative;
            width: 100%;
            background: #d5eaf0;
            border-collapse: collapse;
        }

            #grdageing table {
                display: table;
                background: #91c5d4;
            }

            #grdageing th {
                padding: 4px 2px;
                background: #91c5d4;
                border-left: solid 1px #fff;
                font-size: 0.9em;
            }

            #grdageing tbody th {
                font-family: arial;
                font-size: 12px;
                text-align: center;
                font-weight: bold;
                color: #000;
                background: #9196d4;
                /*padding: 8px 0px;*/
                border-left: 1px solid #fff;
                border-bottom: 1px solid #fff;
            }

                #grdageing tbody th[colspan],
                #grdageing tbody th[rowspan] {
                    background: #9196d4;
                }

                #grdageing tbody th a {
                    background: #9196d4;
                    color: #000;
                    text-decoration: none;
                }

                    #grdageing tbody th a:hover {
                        color: #b0cc7f;
                        text-decoration: underline;
                    }

                #grdageing tbody th:first-child {
                    border-left: 1px solid #c3c9ce;
                }

            #grdageing tbody td {
                border-bottom: 1px solid #fff;
                border-left: 1px solid #fff;
                padding: 0px 5px;
                height: 5px;
                font-family: arial;
                font-size: 12px;
                vertical-align: middle;
            }

                #grdageing tbody td a {
                    background: #9196d4;
                    color: #000;
                    text-decoration: none;
                }

                    #grdageing tbody td a:hover {
                        color: #b0cc7f;
                        text-decoration: underline;
                    }

                #grdageing tbody td:first-child {
                    border-left: 1px solid #c3c9ce;
                    background: #b1dae6;
                }

            #grdageing tbody tr:last-child td {
                border-left: 1px solid #fff;
                background: #d7e1c5;
                font-weight: bold;
            }

                #grdageing tbody tr:last-child td:first-child {
                    border-left: 1px solid #c3c9ce;
                    background: #b0cc7f;
                    font-weight: bold;
                }

            #grdageing tbody tr.subtotal td {
                border-left: 1px solid #fff;
                background: #66a9bd;
            }

                #grdageing tbody tr.subtotal td:first-child {
                    border-left: 1px solid #c3c9ce;
                    background: #66a9bd;
                    font-weight: bold;
                }

            #grdageing tbody tr.grandtotal td {
                border-left: 1px solid #fff;
                background: #d7e1c5;
                font-weight: bold;
            }

                #grdageing tbody tr.grandtotal td:first-child {
                    border-left: 1px solid #c3c9ce;
                    background: #b0cc7f;
                    font-weight: bold;
                }

            #grdageing tbody tr.zone td {
                border-left: 1px solid #fff;
                background: #bfc9ca;
            }

                #grdageing tbody tr.zone td:first-child {
                    border-left: 1px solid #c3c9ce;
                    background: #bfc9ca;
                    font-weight: bold;
                    color: #0c06fb;
                }

            #grdageing tbody th a:hover {
                color: #b0cc7f;
                text-decoration: underline;
            }

            #grdageing tr:nth-child(odd) {
                background: #b8d1f3;
            }

            #grdageing tr:nth-child(even) {
                background: #c6daf4;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="contentarea">
        <div class="grid_container">
            <div class="grid_12">
                <div class="widget_wrap">
                    <div class="widget_top active">
                        <span class="h_icon_he list_images"></span>
                        <h6>Debtors / Creditors Ageing Analysis Report</h6>
                        <div id="btnsummaryexportDiv" class="btn_30_light" style="float: right;">
                            <span class="icon doc_excel_table_co"></span>
                            <button type="button" id="btnsumExportExcel" class="btn_link">Download Ageing to Excel</button>
                        </div>
                    </div>
                    <div class="widget_content">
                        <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                            <tr>
                                <td class="field_title" width="80" runat="server">
                                    <label id="lbldate">AS ON DATE</label>
                                </td>
                                <td class="field_input" width="120">
                                    <input id="txtasondate" type="text" placeholder="dd/MM/yyyy" style="width: 70px;" readonly />
                                </td>
                                <td class="field_title" width="60">
                                    <label id="lbldepot">Depot</label>
                                </td>
                                <td class="field_input" width="120">
                                    <select id="ddldepot" multiple style="width: 120px;">
                                    </select>
                                </td>
                                <td class="field_title" width="60">
                                    <label id="lblgroup">Group</label>
                                </td>
                                <td class="field_input" width="120">
                                    <select id="ddlgroup" style="width: 120px;" onchange="ChangeSelection();">
                                    </select>
                                </td>
                                <td class="field_title" width="50">
                                    <label id="lblcalender">TYPE</label>
                                </td>
                                <td class="field_input" width="60">
                                    <select id="ddltype" style="width: 60px;" class="chosen-select">
                                        <option value="0" selected>ALL</option>
                                        <option value="1">DR</option>
                                        <option value="2">CR</option>
                                    </select>
                                </td>
                                <td class="field_title" width="50">
                                    <label id="lblshowby">SHOW</label>
                                </td>
                                <td class="field_input" width="130">
                                    <select id="ddlshowby" style="width: 130px;" class="chosen-select">
                                        <option value="3" selected>Ledger Summary</option>
					                    <option value="1">Ledger Bill Details</option>
                                        <option value="0">Branch Summary</option>
                                        <option value="2">Credit Days</option>
                                    </select>
                                </td>
                                <td class="field_title" width="70" style="display:none;" id="tdbasedonheader">
                                    <label id="lblbasedon">BASED ON</label>
                                </td>
                                <td class="field_input" width="120" style="display:none;" id="tdbasedondetails">
                                    <select id="ddlbasedon" style="width: 100px;">
                                        <option value="N">Voucher Date</option>
                                        <option value="Y" selected>Bill Date</option>
                                    </select>
                                </td>
                                <td class="field_input" width="100">
                                    <div class="btn_24_blue">
                                        <span class="icon exclamation_co"></span>
                                        <button type="button" id="btnShowData" class="btn_link">Show</button>
                                    </div>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                        <div class="gridcontent" style="overflow-x: auto; height: 400px;">
                            <table id="grdageing"></table>
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
                    <h4 class="modal-title" id="loaderLabel">Please wait...</h4>
                </div>
                <div class="modal-body">
                    <i class="fa fa-refresh fa-spin fa-3x text-center"></i><span style="font-size: xx-large; padding-left: 30px;">Please wait...</span>
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

    <script src="/Areas/Development/dist/libraries/jquery/jquery.js"></script>
    <script src="/Areas/Development/dist/modular/js/core.js" type="text/javascript"></script>
    <link href="/Areas/Development/dist/modular/css/core.css" rel="stylesheet" type="text/css">
    <link href="/Areas/Development/dist/modular/css/grid.css" rel="stylesheet" type="text/css">
    <script src="/Areas/Development/dist/modular/js/grid.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnsummaryexportDiv').css( 'visibility', 'hidden' );

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

            $('#txtasondate').datepicker({
                format: 'dd/mm/yyyy',
                uiLibrary: 'bootstrap4',
                iconsLibrary: 'fontawesome',
                minDate: yearstart,
                value: todayDate,
                maxDate: today,
            });

            BindRegion();
            BindSundryGroup();
        });

        function BindRegion() {
            var listItems = "";
            $.ajax({
                url: "<%=ServiceURL%>Ledger_Report_Services.asmx/GetRegion",
                type: 'POST',
                contentType: 'application/json;',
                dataType: 'json',
                async: true,
                success: function (result) {
                    if (result != null) {
                        if (result.d.Regions.length > 0) {
                            for (var i = 0; i < result.d.Regions.length; i++) {
                                listItems += "<option value='" + result.d.Regions[i]["BRID"] + "'>" + result.d.Regions[i]["BRNAME"] + "</option>";
                            }
                            $("#ddldepot").html(listItems);
                            $('#ddldepot').multiselect({
                                includeSelectAllOption: true
                            });
                            $("#ddldepot").multiselect('selectAll', true);
                            $("#ddldepot").multiselect('updateButtonText');
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

        
        function BindSundryGroup() {
            var listItems = "";
            $.ajax({
                url: "<%=ServiceURL%>Ledger_Report_Services.asmx/Bind_AllSundryGroupList",
                type: 'POST',
                contentType: 'application/json;',
                dataType: 'json',
                async: true,
                success: function (result) {
                    if (result != null) {
                        if (result.d.sundry_creditors.length > 0) {
                            for (var i = 0; i < result.d.sundry_creditors.length; i++) {
                                listItems += "<option value='" + result.d.sundry_creditors[i]["code"] + "'>" + result.d.sundry_creditors[i]["grpname"] + "</option>";
                            }
                            $("#ddlgroup").html(listItems);
                            //$('#ddlgroup').multiselect({
                            //    includeSelectAllOption: true
                            //});
                            //$("#ddlgroup").multiselect('selectAll', true);
                            $("#ddlgroup").multiselect('updateButtonText');
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



        function ChangeSelection() {
            var GroupName = [];
            $("#ddlgroup option:selected").each(function () {
                GroupName.push(this.text);
            });

            if (GroupName.toString().toLocaleLowerCase().indexOf("creditors") >= 0) {
                $('#tdbasedonheader').css('display', "");
                $('#tdbasedondetails').css('display', "");

                $("#ddlbasedon").multiselect('selectAll', true);
                $("#ddlbasedon").multiselect('updateButtonText');
            }
            else {
                $('#tdbasedonheader').css('display', 'none');
                $('#tdbasedondetails').css('display', 'none');

                $("#ddlbasedon").multiselect('selectAll', true);
                $("#ddlbasedon").multiselect('updateButtonText');
            }
        }

        $('#btnShowData').click(function (event) {
            var DepotID = [];
            $("#ddldepot option:selected").each(function () {
                DepotID.push(this.value);
            });

            if (DepotID.length == 0) {
                alert('Please select depot.');
                return;
            }

            var GroupName = [];
            $("#ddlgroup option:selected").each(function () {
                GroupName.push(this.text);
            });

            if (GroupName.toString().toLocaleLowerCase().indexOf("debtors") >= 0 && ($("#ddlshowby option:selected").val() == '2')) {
                alert('Show By - "Credit days" not applicable for selected group....Please checked.');
                return;
            }

            var data = PrepareJSONData("Show Report");
            $.ajax({
                url: "<%=ServiceURL%>Ledger_Report_Services.asmx/GetAgeingReport",
                type: 'POST',
                data: data,
                contentType: 'application/json;',
                dataType: 'json',
                async: true,
                success: function (result) {
                    if (result != null) {
                        if (result.d.ParentData.length > 0) {
                            $('#btnsumExportExcel').prop('disabled', false);
                            $('#btnsummaryexportDiv').removeClass("btn_30_light");
                            $('#btnsummaryexportDiv').addClass("btn_30_blue");
                            $('#btnsummaryexportDiv').css( 'visibility', 'visible' ).fadeIn( 20000 );
                        }
                        else {
                            $('#btnsumExportExcel').prop('disabled', true);
                            $('#btnsummaryexportDiv').addClass("btn_30_light");
                            $('#btnsummaryexportDiv').removeClass("btn_30_blue");
                            $('#btnsummaryexportDiv').css('visibility', 'hidden');
                        }

                        if (GroupName.toString().toLocaleLowerCase().indexOf("creditors") >= 0) {
                            if ($("#ddlshowby option:selected").val() == '2') {
                                BindCreditorBasedOnCreditDaysGrid(result);
                            }
                            else {
                                BindCreditorGrid(result);
                            }
                        }
                        else {
                            BindDebitorGrid(result);
                        }
                        $("#loader").fadeOut(200, function () { $("#loader").modal('hide'); })

                    }
                    else {
                        $('#btnsumExportExcel').prop('disabled', true);
                        $('#btnsummaryexportDiv').addClass("btn_30_light");
                        $('#btnsummaryexportDiv').removeClass("btn_30_blue");
                    }
                },
                error: function (result) {
                    $("#loader").fadeOut(200, function () { $("#loader").modal('hide'); })
                    $("#error-modal").dialog({ modal: true });
                }
            });
        });


        $('#btnsumExportExcel').click(function (event) {
            $("#loader").fadeIn(500).modal('show');
            $.ajax({
                url: "<%=ServiceURL%>Ledger_Report_Services.asmx/GetAgeingReportinExcel",
                type: 'POST',
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

        function BindDebitorGrid(result) {
            var grid = $('#grdageing').grid({
                dataSource: result.d.ParentData,
                primaryKey: 'PARTYNAME',
                selectionType: 'checkbox',
                uiLibrary: 'bootstrap',
                bodyRowHeight: 'autogrow',
                columns: [
                    { field: 'DEPOTNAME', title: 'Depot', width: 370, },
                    { field: 'PARTYNAME', title: 'Ledger', width: 250 },
                    { field: '[0-7 DAYS]', title: '[0-7 DAYS]', width: 70, align: 'right' },
                    { field: '[8-15 DAYS]', title: '[8-15 DAYS]', width: 80, align: 'right' },
                    { field: '[16-30 DAYS]', title: '[16-30 DAYS]', width: 90, align: 'right' },
                    { field: '[31-60 DAYS]', title: '[31-60 DAYS]', width: 90, align: 'right' },
                    { field: '[61-90 DAYS]', title: '[61-90 Days]', width: 90, align: 'right' },
                    { field: '[91-120 DAYS]', title: '[91-120 Days]', width: 90, align: 'right' },
                    { field: '[121-150 DAYS]', title: '[121-150 Days]', width: 95, align: 'right' },
                    { field: '[151-180 DAYS]', title: '[151-180 Days]', width: 95, align: 'right' },
                    { field: '[Above 180]', title: '[> 180 Days]', width: 90, align: 'right' },
                    { field: 'UNADJUSTED AMOUNT', title: 'Unadjusted Amount', width: 130, align: 'right' },
                    { field: 'NET AMOUNT', title: 'Net Amount', width: 110, align: 'right' }
                ],
                pager: { limit: 100, sizes: [5, 10, 50, 100, 500, 1000] },
                detailTemplate: '<div><table/></div>'
            });
        }


        function BindCreditorGrid(result) {
            var grid = $('#grdageing').grid({
                dataSource: result.d.ParentData,
                primaryKey: 'PARTYNAME',
                selectionType: 'checkbox',
                uiLibrary: 'bootstrap',
                bodyRowHeight: 'autogrow',
                columns: [
                    { field: 'DEPOTNAME', title: 'Depot', width: 370 },
                    { field: 'PARTYNAME', title: 'Ledger', width: 250 },
                    { field: 'CREDITDAY', title: 'Credit Days', width: 80, align: 'right' },
                    { field: '[0-30 DAYS]', title: '[0-30 Days]', width: 90, align: 'right' },
                    { field: '[31-45 DAYS]', title: '[31-45 Days]', width: 90, align: 'right' },
                    { field: '[46-60 DAYS]', title: '[46-60 Days]', width: 90, align: 'right' },
                    { field: '[61-90 DAYS]', title: '[61-90 Days]', width: 90, align: 'right' },
                    { field: '[91-120 DAYS]', title: '[91-120 Days]', width: 90, align: 'right' },
                    { field: '[121-150 DAYS]', title: '[121-150 Days]', width: 95, align: 'right' },
                    { field: '[151-180 DAYS]', title: '[151-180 Days]', width: 95, align: 'right' },
                    { field: '[Above 180]', title: '[> 180 Days]', width: 90, align: 'right' },
                    { field: 'NET AMOUNT', title: 'Net Amount', width: 110, align: 'right' }
                ],
                pager: { limit: 100, sizes: [5, 10, 50, 100, 500, 1000] },
                detailTemplate: '<div><table/></div>'
            });

            if ($("#ddlshowby option:selected").val() == '1') {
                grid.on('detailExpand', function (e, $detailWrapper, id) {
                    $.ajax({
                        url: "<%=ServiceURL%>Ledger_Report_Services.asmx/GetAgeingBillDetailsReport",
                        type: 'POST',
                        data: JSON.stringify({ 'FilterValue': id }),
                        contentType: 'application/json;',
                        dataType: 'json',
                        async: false,
                        success: function (result) {
                            if (result != null) {
                                $detailWrapper.find('table').grid({
                                    dataSource: result.d.ChildData,
                                    primaryKey: 'PARTYNAME',
                                    selectionType: 'checkbox',
                                    uiLibrary: 'bootstrap',
                                    columns: [
                                        { field: 'LedgerName', title: 'Ledger', width: 250 },
                                        { field: 'BILLNO', title: 'Bill No', width: 150 },
                                        { field: 'BILLDATE', title: 'Bill Date', width: 90, type: 'date', format: 'dd/mm/yyyy' },
                                        { field: 'InvoiceNo', title: 'Voucher No', width: 120 },
                                        { field: 'InvoiceDate', title: 'Voucher Date', width: 90, type: 'date', format: 'dd/mm/yyyy' },
                                        { field: '[0-30 DAYS]', title: '[0-30 Days]', width: 90, align: 'right' },
                                        { field: '[31-45 DAYS]', title: '[31-45 Days]', width: 90, align: 'right' },
                                        { field: '[46-60 DAYS]', title: '[46-60 Days]', width: 90, align: 'right' },
                                        { field: '[61-90 DAYS]', title: '[61-90 Days]', width: 90, align: 'right' },
                                        { field: '[91-120 DAYS]', title: '[91-120 Days]', width: 90, align: 'right' },
                                        { field: '[121-150 DAYS]', title: '[121-150 Days]', width: 95, align: 'right' },
                                        { field: '[151-180 DAYS]', title: '[151-180 Days]', width: 95, align: 'right' },
                                        { field: '[Above 180]', title: '[> 180 Days]', width: 90, align: 'right' },
                                        { field: 'NET AMOUNT', title: 'Net Amount', width: 110, align: 'right' }
                                    ],

                                    detailTemplate: '<div><table/></div>'
                                });
                            }
                        },
                        error: function (result) {
                            alert('Error occured.....');
                        }
                    });
                });
            }
            grid.on('detailCollapse', function (e, $detailWrapper, id) {
                $detailWrapper.find('table').grid('destroy', true, true);
            });
        }


        function BindCreditorBasedOnCreditDaysGrid(result) {
            var grid = $('#grdageing').grid({
                dataSource: result.d.ParentData,
                primaryKey: 'PARTYNAME',
                selectionType: 'checkbox',
                uiLibrary: 'bootstrap',
                bodyRowHeight: 'autogrow',
                columns: [
                    { field: 'DEPOTNAME', title: 'Depot', width: 370 },
                    { field: 'PARTYNAME', title: 'Ledger', width: 250 },
                    { field: 'Credit Days', title: 'Credit Days', width: 80, align: 'right' },
                    { field: 'Invoice Amount', title: 'Invoice Amount', width: 100, align: 'right' },
                    { field: 'Adjusted Amount', title: 'Adjusted Amount', width: 110, align: 'right' },
                    { field: 'Remaining Amount', title: 'Remaining Amount', width: 110, align: 'right' }
                ],
                pager: { limit: 100, sizes: [5, 10, 50, 100, 500, 1000] },
                detailTemplate: '<div><table/></div>'
            });

            if ($("#ddlshowby option:selected").val() == '2') {
                grid.on('detailExpand', function (e, $detailWrapper, id) {
                    $.ajax({
                        url: "<%=ServiceURL%>Ledger_Report_Services.asmx/GetAgeingBillDetailsReport",
                        type: 'POST',
                        data: JSON.stringify({ 'FilterValue': id }),
                        contentType: 'application/json;',
                        dataType: 'json',
                        async: false,
                        success: function (result) {
                            if (result != null) {
                                $detailWrapper.find('table').grid({
                                    dataSource: result.d.ChildData,
                                    primaryKey: 'PARTYNAME',
                                    selectionType: 'checkbox',
                                    uiLibrary: 'bootstrap',
                                    columns: [
                                        { field: 'BILLNO', title: 'Bill No' },
                                        { field: 'BILLDATE', title: 'Bill Date', width: 80, type: 'date', format: 'dd/mm/yyyy' },
                                        { field: 'InvoiceNo', title: 'Voucher No' },
                                        { field: 'Invoice Date', title: 'Voucher Date', width: 80, type: 'date', format: 'dd/mm/yyyy' },
                                        { field: 'Invoice Amount', title: 'Invoice Amount', width: 100, align: 'right' },
                                        { field: 'Adjusted Amount', title: 'Adjusted Amount', width: 110, align: 'right' },
                                        { field: 'Remaining Amount', title: 'Remaining Amount', width: 120, align: 'right' },
                                        { field: 'Invoice Branch', title: 'Invoice Branch', width: 370 }
                                    ],

                                    detailTemplate: '<div><table/></div>'
                                });
                            }
                        },
                        error: function (result) {
                            alert('Error occured.....');
                        }
                    });
                });
            }
            grid.on('detailCollapse', function (e, $detailWrapper, id) {
                $detailWrapper.find('table').grid('destroy', true, true);
            });
        }

        function PrepareJSONData(CalledBy) {
            $('#grdageing').grid('destroy', true, true);
            $("#loader").fadeIn(500).modal('show');
            var DepotID = [];
            $("#ddldepot option:selected").each(function () {
                DepotID.push(this.value);
            });

            var GroupID = [];
            $("#ddlgroup option:selected").each(function () {
                GroupID.push(this.value);
            });

            var Type = $("#ddltype option:selected").val();
            var Showby = $("#ddlshowby option:selected").val();
            var BasedON = $("#ddlbasedon option:selected").val();

            var data = JSON.stringify({
                'AsOnDate': $('#txtasondate').val(),
                'DepotID': DepotID.toString(),
                'GroupID': GroupID.toString(),
                'Type': Type,
                'Showby': Showby,
                'BasedON': BasedON,
                'Caller': CalledBy
            });

            return data;
        }
    </script>
</asp:Content>