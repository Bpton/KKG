//#region Developer Info
/*
* For FGRateSheet.cshtml Only
* Developer Name : Ajoy Rana
* Start Date     : 16/07/2020
* END Date       :
*/
//#endregion

$(document).ready(function () {

    var qs = getQueryStrings();

    loadervisible_on();

    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": true,
        "preventDuplicates": true,
        "positionClass": "toast-top-center",
        "onclick": null,
        "showDuration": "2000",
        "hideDuration": "2000",
        "timeOut": "3000",
        "extendedTimeOut": "2000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "slideDown",
        "hideMethod": "slideUp"
    };

    $(".date-picker").datepicker({
        dateFormat: 'dd-mm-yy'
    });

    if ('@ViewBag.messageid' == "0") {
        toastr.error('Something went wrong');
        return;
    }

    if (qs["CHECKER"].toString() == "TRUE") {
        $('#divdateheader').css("display", "none");
        $('#divfromdate').css("display", "none");
        $('#divtodate').css("display", "none");
        $("#btnsubmit").prop("value", "Approved");
    }

    if (qs["ISREPORT"].toString() == "Y") {
        $('#divdateheader').css("display", "none");
        $('#divfromdate').css("display", "none");
        $('#divtodate').css("display", "none");
        $('#divsubmit').css("display", "none");
    }


    $("#FROMDATE").val(getCurrentDate());
    $("#TODATE").val(getCurrentDate());

    bindbranch();
    binddivision()
    choosencontrol();
    grdheader(qs["CHECKER"].toString(), qs["ISREPORT"].toString());
    loadervisible_off();

    $("#btncancel").click(function (e) {
        resetall();
    });

    $("#btnshow").click(function (e) {
        loadervisible_on();

        loadratesheetlist(qs["CHECKER"].toString(), qs["ISREPORT"].toString());
        $("#CATID").chosen({
            search_contains: true
        });
        $("#CATID").trigger("chosen:updated");

        loadervisible_off();
    });

    $('#DIVID').change(function () {
        loadervisible_on();
        $("#CATID").chosen({
            search_contains: true
        });
        $("#CATID").trigger("chosen:updated");
        bindcategorylist($('#DIVID').val());

        $("#CATID").chosen({
            search_contains: true
        });
        $("#CATID").trigger("chosen:updated");
        loadervisible_off();
    });

    $("#btnsubmit").click(function (e) {
        if (confirm("Are you sure you want to proceed?")) {

            var _details_exists = 0;
            $('#grdratesheet tbody tr').each(function () {
                _details_exists = 1;
            });

            loadervisible_on();

            if (_details_exists == 1) {

                var chkcount = 0;
                $('#grdratesheet tbody tr').each(function () {
                    if ($(this).find('#chkproduct').prop('checked') == true) {
                        chkcount += 1;
                    }
                });

                if (chkcount > 0) {
                    var arrayrate = [];

                    $('#grdratesheet tbody tr').each(function () {
                        if ($(this).find('#chkproduct').prop('checked') == true) {
                            var ratedetail = {};

                            ratedetail.PRODUCTID = $(this).find('td:eq(0)').html().trim();
                            ratedetail.PRODUCTNAME = $(this).find('td:eq(3)').html().trim();
                            ratedetail.UNITVALUE = $(this).find('td:eq(4)').html().trim();
                            ratedetail.UOMNAME = $(this).find('td:eq(5)').html().trim();
                            ratedetail.RMCOST = $(this).closest("tr").find('#txtrmcostid').val();
                            ratedetail.PMCOST = $(this).closest("tr").find('#txtpmcostid').val();
                            ratedetail.CONVERSIONCOST = $(this).closest("tr").find('#txtconversioncosttid').val();
                            ratedetail.OVERHEADCOST = $(this).closest("tr").find('#txtoverheadcostid').val();
                            ratedetail.OTHERCOST = $(this).closest("tr").find('#txtothercostid').val();
                            ratedetail.TOTALCOST = $(this).find('td:eq(11)').html().trim();
                            ratedetail.PCS = $(this).closest("tr").find('#txtpcsid').val();

                            arrayrate.push(ratedetail);
                        }
                    });

                    var ratesave = {};
                    ratesave.BRID = $("#BRID").val();
                    ratesave.BRNAME = $("#BRID option:selected").text();
                    ratesave.DIVID = $("#DIVID").val();
                    ratesave.DIVNAME = $("#DIVID option:selected").text();
                    ratesave.CATID = $("#CATID").val();
                    ratesave.CATNAME = $("#CATID option:selected").text();
                    ratesave.FROMDATE = $("#FROMDATE").val();
                    ratesave.TODATE = $("#TODATE").val();
                    ratesave.CHECKER = qs["CHECKER"].toString()
                    ratesave.FGRateSheet = arrayrate;
                    
                    $.ajax({
                        url: "/FGCosting/ratesheetsubmitdata",
                        data: '{ratesheetsave:' + JSON.stringify(ratesave) + '}',
                        type: "POST",
                        contentType: "application/json;charset=utf-8",
                        async: false,
                        success: function (responseMessage) {
                            var messageid;
                            var messagetext;
                            $.each(responseMessage, function (key, item) {
                                messageid = item.MessageID;
                                messagetext = item.MessageText;
                            });

                            if (messageid == '1' || messageid == '2') {
                                loadervisible_off();
                                toastr.success(messagetext);
                                resetall();
                            }
                            else {
                                toastr.error(messagetext);
                            }
                        },
                        failure: function (response) {
                            toastr.error(response.responseText);
                        },
                        error: function (response) {
                            toastr.error(response.responseText);
                        }
                    });
                }
                else {
                    toastr.info('Please click atleast 1 product checkbox with proper rate');
                }
            }
            else {
                toastr.info('No product details found');
            }

            loadervisible_off();
        }
    });
});

$(function () {

    $("body").on("keyup", "#grdratesheet #txtrmcostid", function () {

        var row = $(this).closest("tr");
        var rmcost = parseFloat(row.find('#txtrmcostid').val());

        if (isNaN(rmcost)) {
            row.find('#txtrmcostid').val('0');
        }
        else if (rmcost == '') {
            row.find('#txtrmcostid').val('0');
        }

        headercalculation(row);
    })

    $("body").on("keyup", "#grdratesheet #txtpmcostid", function () {

        var row = $(this).closest("tr");
        var pmcost = parseFloat(row.find('#txtpmcostid').val().trim());

        if (isNaN(pmcost)) {
            row.find('#txtpmcostid').val('0');
        }
        else if (pmcost == '') {
            row.find('#txtpmcostid').val('0');
        }

        headercalculation(row);
    })

    $("body").on("keyup", "#grdratesheet #txtconversioncosttid", function () {

        var row = $(this).closest("tr");
        var conversioncost = parseFloat(row.find('#txtconversioncosttid').val().trim());

        if (isNaN(conversioncost)) {
            row.find('#txtconversioncosttid').val('0');
        }
        else if (conversioncost == '') {
            row.find('#txtconversioncosttid').val('0');
        }

        headercalculation(row);
    })

    $("body").on("keyup", "#grdratesheet #txtoverheadcostid", function () {

        var row = $(this).closest("tr");
        var overheadcost = parseFloat(row.find('#txtoverheadcostid').val().trim());

        if (isNaN(overheadcost)) {
            row.find('#txtoverheadcostid').val('0');
        }
        else if (overheadcost == '') {
            row.find('#txtoverheadcostid').val('0');
        }

        headercalculation(row);
    })

    $("body").on("keyup", "#grdratesheet #txtothercostid", function () {

        var row = $(this).closest("tr");
        var othercost = parseFloat(row.find('#txtothercostid').val().trim());

        if (isNaN(othercost)) {
            row.find('#txtothercostid').val('0');
        }
        else if (othercost == '') {
            row.find('#txtothercostid').val('0');
        }

        headercalculation(row);
    })


    $("body").on("click", "#grdratesheet #chkproduct", function () {
        //var row = $(this).closest("tr");
    })
});

function headercalculation(row) {
    var Slno = row.find('td:eq(2)').html().trim();
    var rmcost = parseFloat(row.find('#txtrmcostid').val().trim());
    var pmcost = parseFloat(row.find('#txtpmcostid').val().trim());
    var conversioncost = parseFloat(row.find('#txtconversioncosttid').val().trim());
    var overheadcost = parseFloat(row.find('#txtoverheadcostid').val().trim());
    var othercost = parseFloat(row.find('#txtothercostid').val().trim());
    var total = (parseFloat(rmcost + pmcost + conversioncost + overheadcost + othercost)).toFixed(2);

    var invslno;
    $('#grdratesheet tbody tr').each(function () {
        invrow = $(this).closest("tr");
        invslno = invrow.find('td:eq(2)').html().trim();

        if (invslno.toString() == Slno.toString()) {
            $(this).find('td:eq(11)').html(total);
        }
    });
}

function loadervisible_on() {
    $("#dialog").dialog({
        autoOpen: true,
        modal: true,
        title: "Loading.."
    });
    $("#imgLoader").css("visibility", "visible");
}

function loadervisible_off() {
    $("#imgLoader").css("visibility", "hidden");
    $("#dialog").dialog("close");
}

function getQueryStrings() {
    try {
        var assoc = {};
        var decode = function (s) { return decodeURIComponent(s.replace(/\+/g, " ")); };
        var queryString = location.search.substring(1);
        var keyValues = queryString.split('&');

        for (var i in keyValues) {
            var key = keyValues[i].split('=');
            if (key.length > 1) {
                assoc[decode(key[0])] = decode(key[1]);
            }
        }
        return assoc;
    }
    catch (ex) {
        swal("", "Some problem occurred please try again later", "info");
    }
}

function bindcategorylist(divid) {
    var CATID = $("#CATID");
    $.ajax({
        type: "post",
        url: "/FGCosting/LoadDivCategory",
        data: { DIVID: divid },
        datatype: "json",
        traditional: true,
        async: false,
        success: function (response) {
            CATID.empty().append('<option selected="selected" value="0">Select Sub Category</option>');
            $.each(response, function () {
                CATID.append($("<option></option>").val(this['CATID']).html(this['CATNAME']));
            });
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function bindbranch() {
    var BRID = $("#BRID");
    $.ajax({
        type: "post",
        url: "/FGCosting/LoadBranch",
        data: { },
        datatype: "json",
        traditional: true,
        async: false,
        success: function (response) {
            BRID.empty().append('<option selected="selected" value="0">Select Branch</option>');
            $.each(response, function () {
                BRID.append($("<option></option>").val(this['BRID']).html(this['BRNAME']));
            });
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function binddivision() {
    var DIVID = $("#DIVID");
    $.ajax({
        type: "post",
        url: "/FGCosting/LoadDivision",
        data: {},
        datatype: "json",
        traditional: true,
        async: false,
        success: function (response) {
            DIVID.empty().append('<option selected="selected" value="0">Select Category</option>');
            $.each(response, function () {
                DIVID.append($("<option></option>").val(this['DIVID']).html(this['DIVNAME']));
            });
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function grdheader(checker, isreport) {
    var tr;
    tr = $('#grdratesheet');

    if (checker == 'FALSE' && isreport == 'N') {
        tr.append("<thead><tr><th style='display:none;'>PRODUCTID</th><th></th><th style='text-align:center;'>SL</th><th style='text-align:center;'>PROCESSFRAMEWORK NAME</th><th style='text-align:center;'>UNIT</th><th style='text-align:center;'>UOM</th><th style='text-align:center;'>RM COST</th><th style='text-align:center;'>PM COST</th><th style='text-align:center;'>CONVERSION COST</th><th style='text-align:center;'>OVERHEAD COST</th><th style='text-align:center;'>OTHER COST</th><th style='text-align:center;'>TOTAL COST</th> <th style='text-align:center;'>PCS</th>");
    }
    else if (checker == 'TRUE' && isreport == 'N') {
        tr.append("<thead><tr><th style='display:none;'>PRODUCTID</th><th></th><th style='text-align:center;'>SL</th><th style='text-align:center;'>PROCESSFRAMEWORK NAME</th><th style='text-align:center;'>UNIT</th><th style='text-align:center;'>UOM</th><th style='text-align:center;'>RM COST</th><th style='text-align:center;'>PM COST</th><th style='text-align:center;'>CONVERSION COST</th><th style='text-align:center;'>OVERHEAD COST</th><th style='text-align:center;'>OTHER COST</th><th style='text-align:center;'>TOTAL COST</th> <th style='text-align:center;'>PCS</th><th style='text-align:center;'>FROM DATE</th><th style='text-align:center;'>TO DATE</th>");
    }
    else if (checker == 'FALSE' && isreport== 'Y') {
        tr.append("<thead><tr><th>SL</th><th>BRANCH</th><th>CATEGORY</th><th>SUB CATEGORY</th><th style='text-align:center;'>PROCESSFRAMEWORK NAME</th><th style='text-align:center;'>UNIT</th><th style='text-align:center;'>UOM</th><th style='text-align:center;'>RM COST</th><th style='text-align:center;'>PM COST</th><th style='text-align:center;'>CONVERSION COST</th><th style='text-align:center;'>OVERHEAD COST</th><th style='text-align:center;'>OTHER COST</th><th style='text-align:center;'>TOTAL COST</th> <th style='text-align:center;'>PCS</th><th style='text-align:center;'>FROM DATE</th><th style='text-align:center;'>TO DATE</th>");
    }

    $('#grdratesheet').DataTable().destroy();
    $("#grdratesheet tbody tr").remove();

    tr.append("</tbody>");

    $('#grdratesheet').DataTable({
        "sScrollX": '100%',
        "sScrollXInner": "110%",
        "scrollY": "238px",
        "scrollCollapse": true,
        "searching": true,
        "dom": 'Bfrtip',
        buttons: [
            {
                extend: 'excelHtml5',
                title: 'FG Dispatch List'
            }
        ],
        "initComplete": function (settings, json) {
            $('.dataTables_scrollBody thead tr').css({ visibility: 'collapse' });// this gets rid of duplicate headers
        },
        "scrollXInner": true,
        "bRetrieve": false,
        "bFilter": true,
        "bSortClasses": false,
        "bLengthChange": false,
        "bInfo": true,
        "bAutoWidth": false,
        "paging": true,
        "pagingType": "full_numbers",
        "bSort": false,
        "columnDefs": [
            { "orderable": false, "targets": 0 }
        ],
        "order": [],  // not set any order rule for any column.
        "ordering": true
    });
}

function loadratesheetlist(checker, isreport) {
    $('#grdratesheet tbody').empty();
    var _flag;

    if (checker == 'FALSE' && isreport == 'N') {
        _flag = '0';
    }
    else if (checker == 'TRUE' && isreport == 'N') {
        _flag = '1';
    }
    else if (checker == 'FALSE' && isreport == 'Y') {
        _flag = '2';
    }

    $.ajax({
        type: "POST",
        url: "/FGCosting/LoadFGRateSheetList",
        data: { BRANCHID: $('#BRID').val(), DIVID: $('#DIVID').val(), CATID: $('#CATID').val(), FROMDATE: $('#FROMDATE').val(), TODATE: $('#TODATE').val(), FLAG: _flag },
        datatype: "json",
        traditional: true,
        async: false,
        success: function (response) {
            var tr;
            tr = $('#grdratesheet');

            if (_flag == 0) {
                tr.append("<thead><tr><th style='display:none;'>PRODUCTID</th><th></th><th style='text-align:center;'>SL</th><th style='text-align:center;'>PROCESSFRAMEWORK NAME</th><th style='text-align:center;'>UNIT</th><th style='text-align:center;'>UOM</th><th style='text-align:center;'>RM COST</th><th style='text-align:center;'>PM COST</th><th style='text-align:center;'>CONVERSION COST</th><th style='text-align:center;'>OVERHEAD COST</th><th style='text-align:center;'>OTHER COST</th><th style='text-align:center;'>TOTAL COST</th> <th style='text-align:center;'>PCS</th>");

                $('#grdratesheet').DataTable().destroy();
                $("#grdratesheet tbody tr").remove();

                for (var i = 0; i < response.length; i++) {
                    tr = $('<tr/>');
                    tr.append("<td style='display:none'>" + response[i].PRODUCTID + "</td>");
                    tr.append("<td style='text-align: center;'><input type='checkbox' id='chkproduct' /></td>");
                    tr.append("<td><nav style='position:relative;text-align:left;'><span class='badge green'><span class='index'>" + (i + 1) + "</span></span></nav></td>");
                    tr.append("<td>" + response[i].PRODUCTNAME + "</td>");
                    tr.append("<td style='text-align: right;'>" + response[i].UNITVALUE + "</td>");
                    tr.append("<td style='text-align: right;'>" + response[i].UOMNAME + "</td>");
                    tr.append("<td style='width:5%'><input type='text' name='txtrmcost' id='txtrmcostid' value='" + response[i].RMCOST + "' onkeypress='return isNumberKeyWithDot(event)' style='text-align: right; width:100%;' /></td>");
                    tr.append("<td style='width:5%'><input type='text' name='txtpmcost' id='txtpmcostid' value='" + response[i].PMCOST + "' onkeypress='return isNumberKeyWithDot(event)' style='text-align: right; width:100%;' /></td>");
                    tr.append("<td style='width:5%'><input type='text' name='txtconversioncost' id='txtconversioncosttid' value='" + response[i].CONVERSIONCOST + "' onkeypress='return isNumberKeyWithDot(event)' style='text-align: right; width:100%;' /></td>");
                    tr.append("<td style='width:5%'><input type='text' name='txtoverheadcost' id='txtoverheadcostid' value='" + response[i].OVERHEADCOST + "' onkeypress='return isNumberKeyWithDot(event)' style='text-align: right; width:100%;' /></td>");
                    tr.append("<td style='width:5%'><input type='text' name='txtothercost' id='txtothercostid' value='" + response[i].OTHERCOST + "' onkeypress='return isNumberKeyWithDot(event)' style='text-align: right; width:100%;' /></td>");
                    tr.append("<td style='text-align: right;'>" + response[i].TOTALCOST + "</td>");
                    tr.append("<td style='width:5%'><input type='text' name='txtpcs' id='txtpcsid' value='" + response[i].PCS + "' onkeypress='return isNumberKeyWithDot(event)' style='text-align: right; width:100%;' /></td>");
                    $("#grdratesheet").append(tr);
                }

                tr.append("</tbody>");
            }
            else if (_flag == 1) {
                tr.append("<thead><tr><th style='display:none;'>PRODUCTID</th><th></th><th style='text-align:center;'>SL</th><th style='text-align:center;'>PROCESSFRAMEWORK NAME</th><th style='text-align:center;'>UNIT</th><th style='text-align:center;'>UOM</th><th style='text-align:center;'>RM COST</th><th style='text-align:center;'>PM COST</th><th style='text-align:center;'>CONVERSION COST</th><th style='text-align:center;'>OVERHEAD COST</th><th style='text-align:center;'>OTHER COST</th><th style='text-align:center;'>TOTAL COST</th> <th style='text-align:center;'>PCS</th><th style='text-align:center;'>FROM DATE</th><th style='text-align:center;'>TO DATE</th>");

                $('#grdratesheet').DataTable().destroy();
                $("#grdratesheet tbody tr").remove();

                for (var i = 0; i < response.length; i++) {
                    tr = $('<tr/>');
                    tr.append("<td style='display:none'>" + response[i].PRODUCTID + "</td>");
                    tr.append("<td style='text-align: center;'><input type='checkbox' id='chkproduct' /></td>");
                    tr.append("<td><nav style='position:relative;text-align:left;'><span class='badge green'><span class='index'>" + (i + 1) + "</span></span></nav></td>");
                    tr.append("<td>" + response[i].PRODUCTNAME + "</td>");
                    tr.append("<td style='text-align: right;'>" + response[i].UNITVALUE + "</td>");
                    tr.append("<td style='text-align: right;'>" + response[i].UOMNAME + "</td>");
                    tr.append("<td style='text-align: right;'>" + response[i].RMCOST + "</td>");
                    tr.append("<td style='text-align: right;'>" + response[i].PMCOST + "</td>");
                    tr.append("<td style='text-align: right;'>" + response[i].CONVERSIONCOST + "</td>");
                    tr.append("<td style='text-align: right;'>" + response[i].OVERHEADCOST + "</td>");
                    tr.append("<td style='text-align: right;'>" + response[i].OTHERCOST + "</td>");
                    tr.append("<td style='text-align: right;'>" + response[i].TOTALCOST + "</td>");
                    tr.append("<td style='text-align: right;'>" + response[i].PCS + "</td>");
                    tr.append("<td style='text-align: right;'>" + response[i].FROMDATE + "</td>");
                    tr.append("<td style='text-align: right;'>" + response[i].TODATE + "</td>");
                    $("#grdratesheet").append(tr);
                }

                tr.append("</tbody>");
            }
            else if (_flag == 2) {
                tr.append("<thead><tr><th>SL</th><th>BRANCH</th><th>CATEGORY</th><th>SUB CATEGORY</th><th style='text-align:center;'>PROCESSFRAMEWORK NAME</th><th style='text-align:center;'>UNIT</th><th style='text-align:center;'>UOM</th><th style='text-align:center;'>RM COST</th><th style='text-align:center;'>PM COST</th><th style='text-align:center;'>CONVERSION COST</th><th style='text-align:center;'>OVERHEAD COST</th><th style='text-align:center;'>OTHER COST</th><th style='text-align:center;'>TOTAL COST</th> <th style='text-align:center;'>PCS</th><th style='text-align:center;'>FROM DATE</th><th style='text-align:center;'>TO DATE</th>");

                $('#grdratesheet').DataTable().destroy();
                $("#grdratesheet tbody tr").remove();

                for (var i = 0; i < response.length; i++) {
                    tr = $('<tr/>');
                    tr.append("<td><nav style='position:relative;text-align:left;'><span class='badge green'><span class='index'>" + (i + 1) + "</span></span></nav></td>");
                    tr.append("<td>" + response[i].BRNAME + "</td>");
                    tr.append("<td>" + response[i].DIVNAME + "</td>");
                    tr.append("<td>" + response[i].CATNAME + "</td>");
                    tr.append("<td>" + response[i].PRODUCTNAME + "</td>");
                    tr.append("<td style='text-align: right;'>" + response[i].UNITVALUE + "</td>");
                    tr.append("<td style='text-align: right;'>" + response[i].UOMNAME + "</td>");
                    tr.append("<td style='text-align: right;'>" + response[i].RMCOST + "</td>");
                    tr.append("<td style='text-align: right;'>" + response[i].PMCOST + "</td>");
                    tr.append("<td style='text-align: right;'>" + response[i].CONVERSIONCOST + "</td>");
                    tr.append("<td style='text-align: right;'>" + response[i].OVERHEADCOST + "</td>");
                    tr.append("<td style='text-align: right;'>" + response[i].OTHERCOST + "</td>");
                    tr.append("<td style='text-align: right;'>" + response[i].TOTALCOST + "</td>");
                    tr.append("<td style='text-align: right;'>" + response[i].PCS + "</td>");
                    tr.append("<td style='text-align: center;'>" + response[i].FROMDATE + "</td>");
                    tr.append("<td style='text-align: center;'>" + response[i].TODATE + "</td>");
                    $("#grdratesheet").append(tr);
                }

                tr.append("</tbody>");
            }

            $('#grdratesheet').DataTable({
                "sScrollX": '100%',
                "sScrollXInner": "110%",
                "scrollY": "238px",
                "scrollCollapse": true,
                "searching": true,
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                "dom": 'Bfrtip',
                buttons: [
                    {
                        extend: 'excelHtml5',
                        title: 'FG RateSheet'
                    }
                ],
                "initComplete": function (settings, json) {
                    $('.dataTables_scrollBody thead tr').css({ visibility: 'collapse' });// this gets rid of duplicate headers
                },
                "scrollXInner": true,
                "bRetrieve": false,
                "bFilter": true,
                "bSortClasses": false,
                "bLengthChange": false,
                "bInfo": true,
                "bAutoWidth": true,
                "paging": true,
                "pagingType": "full_numbers",
                "bSort": true,
                "columnDefs": [
                    { "orderable": false, "targets": 0 }
                ],
                "order": [],  // not set any order rule for any column.
                "ordering": true
            });
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function resetall() {
    $('#grdratesheet tbody').empty();
    $("#BRID").val('0');
    $("#DIVID").val('0');
    $('#CATID').val('0');
    choosencontrol();
}

function choosencontrol() {
    $("#BRID").chosen({
        search_contains: true
    });
    $("#BRID").trigger("chosen:updated");

    $("#DIVID").chosen({
        search_contains: true
    });
    $("#DIVID").trigger("chosen:updated");

    $("#CATID").chosen({
        search_contains: true
    });
    $("#CATID").trigger("chosen:updated");
}

function generateguid() {
    var d = new Date().getTime();
    var guid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = (d + Math.random() * 16) % 16 | 0;
        d = Math.floor(d / 16);
        return (c == 'x' ? r : (r & 0x3 | 0x8)).toString(16);
    });
    return guid;
}

function getCurrentDate() {
    today_date = new Date();
    today_Date_Str = ((today_date.getDate() < 10) ? "0" : "") + String(today_date.getDate()) + "/" + ((today_date.getMonth() < 9) ? "0" : "") + String(today_date.getMonth() + 1) + "/" + today_date.getFullYear();
    return today_Date_Str;
}

function ShowDialogscheme(modal) {
    $("#overlay").show();
    $("#divqtyscheme").fadeIn(100);

    if (modal) {
        $("#overlay").unbind("click");
    }
    else {
        $("#overlay").click(function (e) {
            HideDialogscheme();
        });
    }
}

function HideDialogscheme() {
    $("#overlay").hide();
    $("#divqtyscheme").fadeOut(100);
}

function isNumberKey(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}

function isNumberKeyWithDot(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if ((charCode < 48 || charCode > 57) && (charCode != 8) && (charCode != 46))
        return false;

    return true;
}