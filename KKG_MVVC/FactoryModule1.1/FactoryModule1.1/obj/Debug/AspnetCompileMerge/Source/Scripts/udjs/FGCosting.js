//#region Developer Info
/*
* For FGCosting.cshtml Only
* Developer Name : Ajoy Rana
* Start Date     : 18/07/2020
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

    $("#FROMDATE").val(getCurrentDate());
    $("#TODATE").val(getCurrentDate());

    bindbranch();
    binddivision()
    choosencontrol();
    //grdheader();
    binditemlist($('#DIVID').val(), $('#CATID').val(), $('#BRID').val());
    loadervisible_off();

    $("#btncancel").click(function (e) {
        resetall();
    });

    $("#btnshow").click(function (e) {
        loadervisible_on();

        var product = $("#PRODUCTID option:selected");
        var productlist = "";
        product.each(function () {
            productlist += $(this).val() + ',';
        });
        var resproductlist = productlist.substring(0, productlist.length - 1);


        if ($("#REPORTTYPE").val() == 'FGBOM') {
            loadfgbomlist(resproductlist);
        }
        else if ($("#REPORTTYPE").val() == 'BULKCOST') {
            loadfgbulkcostlist(resproductlist);
        }
        else if ($("#REPORTTYPE").val() == 'BULKBOM') {
            loadfgbulkbomlist(resproductlist);
        }
        else if ($("#REPORTTYPE").val() == 'MATERIAL') {
            loadmaterialratechartlist(resproductlist);
        }
        
        loadervisible_off();
    });

    $('#DIVID').change(function () {
        loadervisible_on();

        bindcategorylist($('#DIVID').val());

        $("#CATID").chosen({
            search_contains: true
        });
        $("#CATID").trigger("chosen:updated");

        loadervisible_off();
    });

    $('#CATID').change(function () {
        loadervisible_on();

        binditemlist($('#DIVID').val(), $('#CATID').val(), $('#BRID').val());

        loadervisible_off();
    });
});

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

function binditemlist(divid, catid, branchid) {
    var PRODUCTID = $("#PRODUCTID");
    $.ajax({
        type: "post",
        url: "/FGCosting/LoadFGItem",
        data: { DIVID: divid, CATID: catid, BRANCHID: branchid },
        datatype: "json",
        traditional: true,
        async: false,
        success: function (response) {
            PRODUCTID.empty();
            $.each(response, function () {
                PRODUCTID.append($("<option></option>").val(this['PRODUCTID']).html(this['PRODUCTNAME']));
            });

            $('#PRODUCTID').multiselect('destroy');
            $('#PRODUCTID').multiselect({
                columns: 1, placeholder: 'Select FG Item',
                includeSelectAllOption: true
            });

            $("#PRODUCTID").multiselect('selectAll', false);
            $("#PRODUCTID").multiselect('updateButtonText');
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

function grdheader() {
    var tr;
    tr = $('#grdcosting');

    tr.append("<thead><tr><th style='text-align:center;'>SL</th><th style='text-align:center;'>PROCESSFRAMEWORK NAME</th><th style='text-align:center;'>UNIT</th><th style='text-align:center;'>UOM</th><th style='text-align:center;'>RM COST</th><th style='text-align:center;'>PM COST</th>");

    $('#grdcosting').DataTable().destroy();
    $("#grdcosting tbody tr").remove();

    tr.append("</tbody>");

    $('#grdcosting').DataTable({
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

function loadfgbomlist(productlist) {
    $('#grdcosting tbody').empty();

    $.ajax({
        type: "POST",
        url: "/FGCosting/LoadFGBOMReport",
        data: { ITEMID: productlist, BRANCHID: $('#BRID').val(), FROMDATE: $('#FROMDATE').val(), TODATE: $('#TODATE').val() },
        datatype: "json",
        traditional: true,
        async: false,
        success: function (response) {
            var tr;
            tr = $('#grdcosting');

            tr.append("<thead><tr><th style='text-align:center;'>SL</th><th style='text-align:center;'>PROCESSFRAMEWORK NAME</th><th style='text-align:center;'>BOM MATERIAL</th><th style='text-align:center;'>CATEGORY</th><th style='text-align:center;'>SUB CATEGORY</th><th style='text-align:center;'>TYPE OF MATERIAL</th><th style='text-align:center;'>UNIT</th><th style='text-align:center;'>ML</th><th style='text-align:center;'>DENSITY</th><th style='text-align:center;'>QTY</th><th style='text-align:center;'>REFQTY</th><th style='text-align:center;'>RM COST</th><th style='text-align:center;'>PM COST</th><th style='text-align:center;'>PM COST/PC</th> <th style='text-align:center;'>RMPM COST</th>");

            $('#grdcosting').DataTable().destroy();
            $("#grdcosting tbody tr").remove();

            for (var i = 0; i < response.length; i++) {
                tr = $('<tr/>');
                tr.append("<td>" + response[i].SLNO + "</td>");
                tr.append("<td>" + response[i].PROCESSFRAMEWORKNAME + "</td>");
                tr.append("<td>" + response[i].BOMMATERIAL + "</td>");
                tr.append("<td>" + response[i].CATEGORY + "</td>");
                tr.append("<td>" + response[i].SUBCATEGORY + "</td>");
                tr.append("<td>" + response[i].TYPEOFMATERIAL + "</td>");
                tr.append("<td>" + response[i].UNIT + "</td>");
                tr.append("<td style='text-align: right;'>" + response[i].ML + "</td>");
                tr.append("<td style='text-align: right;'>" + response[i].DENSITY + "</td>");    
                tr.append("<td style='text-align: right;'>" + response[i].QTY + "</td>");
                tr.append("<td style='text-align: right;'>" + response[i].REFQTY + "</td>");
                tr.append("<td style='text-align: right;'>" + response[i].RMCOST + "</td>");
                tr.append("<td style='text-align: right;'>" + response[i].PMCOST + "</td>");
                tr.append("<td style='text-align: right;'>" + response[i].PMCOST_PC + "</td>");
                tr.append("<td style='text-align: right;'>" + response[i].RMPMCOST + "</td>");
                $("#grdcosting").append(tr);
            }

            tr.append("</tbody>");

            $('#grdcosting').DataTable({
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
                        title: 'FG BOM'
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

function loadfgbulkcostlist(productlist) {
    $('#grdcosting tbody').empty();

    $.ajax({
        type: "POST",
        url: "/FGCosting/LoadFGBulkCostReport",
        data: { ITEMID: productlist, BRANCHID: $('#BRID').val(), FROMDATE: $('#FROMDATE').val(), TODATE: $('#TODATE').val() },
        datatype: "json",
        traditional: true,
        async: false,
        success: function (response) {
            var tr;
            tr = $('#grdcosting');

            tr.append("<thead><tr><th style='text-align:center;'>SL</th><th style='text-align:center;'>FG DESCRIPTION</th><th style='text-align:center;'>BOM MATERIAL</th><th style='text-align:center;'>ML</th><th style='text-align:center;'>RM Cost/Kg</th><th style='text-align:center;'>Density</th><th style='text-align:center;'>Cost Per Ltr</th><th style='text-align:center;'>Bulk Cost/Bottle</th>");

            $('#grdcosting').DataTable().destroy();
            $("#grdcosting tbody tr").remove();

            for (var i = 0; i < response.length; i++) {
                tr = $('<tr/>');
                tr.append("<td>" + response[i].SLNO + "</td>");
                tr.append("<td>" + response[i].FGDESCRIPTION + "</td>");
                tr.append("<td>" + response[i].BOMMATERIAL + "</td>");
                tr.append("<td style='text-align: right;'>" + response[i].ML + "</td>");
                tr.append("<td style='text-align: right;'>" + response[i].RMCost_Kg + "</td>");
                tr.append("<td style='text-align: right;'>" + response[i].Density + "</td>");
                tr.append("<td style='text-align: right;'>" + response[i].CostPerLtr + "</td>");
                tr.append("<td style='text-align: right;'>" + response[i].BulkCost_Bottle + "</td>");
                $("#grdcosting").append(tr);
            }

            tr.append("</tbody>");

            $('#grdcosting').DataTable({
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
                        title: 'Bulk Cost'
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

function loadfgbulkbomlist(productlist) {
    $('#grdcosting tbody').empty();

    $.ajax({
        type: "POST",
        url: "/FGCosting/LoadFGBulkBOMReport",
        data: { ITEMID: productlist, BRANCHID: $('#BRID').val(), FROMDATE: $('#FROMDATE').val(), TODATE: $('#TODATE').val() },
        datatype: "json",
        traditional: true,
        async: false,
        success: function (response) {
            var tr;
            tr = $('#grdcosting');

            tr.append("<thead><tr><th style='text-align:center;'>SL</th><th style='text-align:center;'>PROCESSFRAMEWORK NAME</th><th style='text-align:center;'>BOM MATERIAL</th><th style='text-align:center;'>TYPE OF MATERIAL</th><th style='text-align:center;'>UNIT</th><th style='text-align:center;'>QTY</th><th style='text-align:center;'>REFQTY</th><th style='text-align:center;'>Rate</th><th style='text-align:center;'>Value</th><th style='text-align:center;'>Cost/Kg</th><th style='text-align:center;'>Cost per KG ( Wastage 2%)</th><th style='text-align:center;'>Density</th><th style='text-align:center;'>Volume</th><th style='text-align:center;'>V/V(Ltr)</th>");

            $('#grdcosting').DataTable().destroy();
            $("#grdcosting tbody tr").remove();

            for (var i = 0; i < response.length; i++) {
                tr = $('<tr/>');
                tr.append("<td>" + response[i].SLNO + "</td>");
                tr.append("<td>" + response[i].PROCESSFRAMEWORKNAME + "</td>");
                tr.append("<td>" + response[i].BOMMATERIAL + "</td>");
                tr.append("<td>" + response[i].TYPEOFMATERIAL + "</td>");
                tr.append("<td>" + response[i].UNIT + "</td>");
                tr.append("<td style='text-align: right;'>" + response[i].QTY + "</td>");
                tr.append("<td style='text-align: right;'>" + response[i].REFQTY + "</td>");
                tr.append("<td style='text-align: right;'>" + response[i].Rate + "</td>");
                tr.append("<td style='text-align: right;'>" + response[i].Value + "</td>");
                tr.append("<td style='text-align: right;'>" + response[i].CostKg + "</td>");
                tr.append("<td style='text-align: right;'>" + response[i].CostperKG_Wastage_2PERCENT + "</td>");
                tr.append("<td style='text-align: right;'>" + response[i].Density + "</td>");
                tr.append("<td style='text-align: right;'>" + response[i].Volume + "</td>");
                tr.append("<td style='text-align: right;'>" + response[i].VV_Ltr + "</td>");
                $("#grdcosting").append(tr);
            }

            tr.append("</tbody>");

            $('#grdcosting').DataTable({
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
                        title: 'Bulk BOM'
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

function loadmaterialratechartlist(productlist) {
    $('#grdcosting tbody').empty();

    $.ajax({
        type: "POST",
        url: "/FGCosting/LoadMaterialRateChartReport",
        data: { ITEMID: productlist, BRANCHID: $('#BRID').val(), FROMDATE: $('#FROMDATE').val(), TODATE: $('#TODATE').val() },
        datatype: "json",
        traditional: true,
        async: false,
        success: function (response) {
            var tr;
            tr = $('#grdcosting');

            tr.append("<thead><tr><th style='text-align:center;'>BOM MATERIAL</th><th style='text-align:center;'>TYPE OF MATERIAL</th><th style='text-align:center;'>UNIT</th><th style='text-align:center;'>RATE</th><th style='text-align:center;'>Jan Qty</th><th style='text-align:center;'>Jan BasicAmt</th><th style='text-align:center;'>Jan Rate</th><th style='text-align:center;'>Feb Qty</th><th style='text-align:center;'>Feb BasicAmt</th><th style='text-align:center;'>Feb Rate</th>< th style = 'text-align:center;' > Mar Qty</th > <th style='text-align:center;'>Mar BasicAmt</th> <th style='text-align:center;'>Mar Rate</th><th style='text-align:center;'>Apr Qty</th> <th style='text-align:center;'>Apr BasicAmt</th> <th style='text-align:center;'>Apr Rate</th><th style='text-align:center;'>May Qty</th> <th style='text-align:center;'>May BasicAmt</th> <th style='text-align:center;'>May Rate</th><th style='text-align:center;'>Jun Qty</th> <th style='text-align:center;'>Jun BasicAmt</th> <th style='text-align:center;'>Jun Rate</th><th style='text-align:center;'>Jul Qty</th> <th style='text-align:center;'>Jul BasicAmt</th> <th style='text-align:center;'>Jul Rate</th><th style='text-align:center;'>Aug Qty</th> <th style='text-align:center;'>Aug BasicAmt</th> <th style='text-align:center;'>Aug Rate</th><th style='text-align:center;'>Sep Qty</th> <th style='text-align:center;'>Sep BasicAmt</th> <th style='text-align:center;'>Sep Rate</th><th style='text-align:center;'>Oct Qty</th> <th style='text-align:center;'>Oct BasicAmt</th> <th style='text-align:center;'>Oct Rate</th><th style='text-align:center;'>Nov Qty</th> <th style='text-align:center;'>Nov BasicAmt</th> <th style='text-align:center;'>Nov Rate</th><th style='text-align:center;'>Dec Qty</th> <th style='text-align:center;'>Dec BasicAmt</th> <th style='text-align:center;'>Dec Rate</th><th style='text-align:center;'>Total Qty</th> <th style='text-align:center;'>Total BasicAmt</th> <th style='text-align:center;'>Avg Rate</th><th style='text-align:center;'>Current Rate or Avg Rate Higher</th> <th style='text-align:center;'>Freight</th> <th style='text-align:center;'>Unloading/Other Charges</th><th style='text-align:center;'>Landed Cost</th>");

            $('#grdcosting').DataTable().destroy();
            $("#grdcosting tbody tr").remove();

            for (var i = 0; i < response.length; i++) {
                tr = $('<tr/>');
                tr.append("<td>" + response[i].BOMMATERIAL + "</td>");
                tr.append("<td>" + response[i].TYPEOFMATERIAL + "</td>");
                tr.append("<td>" + response[i].UNIT + "</td>");
                tr.append("<td style='text-align: right;'>" + response[i].RATE + "</td>");
                tr.append("<td style='text-align: right;'>" + response[i].Jan_Qty + "</td>");
                tr.append("<td style='text-align: right;'>" + response[i].Jan_BasicAmt + "</td>");
                tr.append("<td style='text-align: right;'>" + response[i].Jan_Rate + "</td>");

                tr.append("<td style='text-align: right;'>" + response[i].Feb_Qty + "</td>");
                tr.append("<td style='text-align: right;'>" + response[i].Feb_BasicAmt + "</td>");
                tr.append("<td style='text-align: right;'>" + response[i].Feb_Rate + "</td>");

                tr.append("<td style='text-align: right;'>" + response[i].Mar_Qty + "</td>");
                tr.append("<td style='text-align: right;'>" + response[i].Mar_BasicAmt + "</td>");
                tr.append("<td style='text-align: right;'>" + response[i].Mar_Rate + "</td>");

                tr.append("<td style='text-align: right;'>" + response[i].Apr_Qty + "</td>");
                tr.append("<td style='text-align: right;'>" + response[i].Apr_BasicAmt + "</td>");
                tr.append("<td style='text-align: right;'>" + response[i].Apr_Rate + "</td>");

                tr.append("<td style='text-align: right;'>" + response[i].May_Qty + "</td>");
                tr.append("<td style='text-align: right;'>" + response[i].May_BasicAmt + "</td>");
                tr.append("<td style='text-align: right;'>" + response[i].May_Rate + "</td>");

                tr.append("<td style='text-align: right;'>" + response[i].Jun_Qty + "</td>");
                tr.append("<td style='text-align: right;'>" + response[i].Jun_BasicAmt + "</td>");
                tr.append("<td style='text-align: right;'>" + response[i].Jun_Rate + "</td>");

                tr.append("<td style='text-align: right;'>" + response[i].Jul_Qty + "</td>");
                tr.append("<td style='text-align: right;'>" + response[i].Jul_BasicAmt + "</td>");
                tr.append("<td style='text-align: right;'>" + response[i].Jul_Rate + "</td>");

                tr.append("<td style='text-align: right;'>" + response[i].Aug_Qty + "</td>");
                tr.append("<td style='text-align: right;'>" + response[i].Aug_BasicAmt + "</td>");
                tr.append("<td style='text-align: right;'>" + response[i].Aug_Rate + "</td>");

                tr.append("<td style='text-align: right;'>" + response[i].Sep_Qty + "</td>");
                tr.append("<td style='text-align: right;'>" + response[i].Sep_BasicAmt + "</td>");
                tr.append("<td style='text-align: right;'>" + response[i].Sep_Rate + "</td>");

                tr.append("<td style='text-align: right;'>" + response[i].Oct_Qty + "</td>");
                tr.append("<td style='text-align: right;'>" + response[i].Oct_BasicAmt + "</td>");
                tr.append("<td style='text-align: right;'>" + response[i].Oct_Rate + "</td>");

                tr.append("<td style='text-align: right;'>" + response[i].Nov_Qty + "</td>");
                tr.append("<td style='text-align: right;'>" + response[i].Nov_BasicAmt + "</td>");
                tr.append("<td style='text-align: right;'>" + response[i].Nov_Rate + "</td>");

                tr.append("<td style='text-align: right;'>" + response[i].Dec_Qty + "</td>");
                tr.append("<td style='text-align: right;'>" + response[i].Dec_BasicAmt + "</td>");
                tr.append("<td style='text-align: right;'>" + response[i].Dec_Rate + "</td>");

                tr.append("<td style='text-align: right;'>" + response[i].Total_Qty + "</td>");
                tr.append("<td style='text-align: right;'>" + response[i].Total_BasicAmt + "</td>");
                tr.append("<td style='text-align: right;'>" + response[i].Avg_Rate + "</td>");

                tr.append("<td style='text-align: right;'>" + response[i].CurrentRateorAvgRate_Higher + "</td>");
                tr.append("<td style='text-align: right;'>" + response[i].Freight + "</td>");
                tr.append("<td style='text-align: right;'>" + response[i].Unloading_Other_Charges + "</td>");

                tr.append("<td style='text-align: right;'>" + response[i].LandedCost + "</td>");

                $("#grdcosting").append(tr);
            }

            tr.append("</tbody>");

            $('#grdcosting').DataTable({
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
                        title: 'Bulk BOM'
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
    $('#grdcosting tbody').empty();
    $("#BRID").val('0');
    $("#DIVID").val('0');
    $('#CATID').val('0');
    $('#PRODUCTID').val('0');
    $('#REPORTTYPE').val('0');
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

    $("#REPORTTYPE").chosen({
        search_contains: true
    });
    $("#REPORTTYPE").trigger("chosen:updated");
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