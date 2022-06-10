//#region Developer Info
/*
* For Erp itemLdger Report.cshtml Only
* Developer Name : Pritam Basu 
* Start Date     : 07/08/2020
* END Date       : 
*/
//#endregion
var currentdt;
var frmdate;
var todate;
var menuid;
var userid;
var UTNAME;
var tpuid;
var Iuserid;
var FINYEAR;

$(document).ready(function () {
    var qs = getQueryStrings();
    if (qs["MENUID"] != undefined && qs["MENUID"] != "") {
        menuid = qs["MENUID"].trim();
    }
    if (qs["USERID"] != undefined && qs["USERID"] != "") {
        userid = qs["USERID"].trim();
    }
    if (qs["UTNAME"] != undefined && qs["UTNAME"] != "") {
        UTNAME = qs["UTNAME"].trim();
    }
    if (qs["TPU"] != undefined && qs["TPU"] != "") {
        tpuid = qs["TPU"].trim();
    }
    if (qs["IUserID"] != undefined && qs["IUserID"] != "") {
        Iuserid = qs["IUserID"].trim();
    }
    if (qs["FINYEAR"] != undefined && qs["FINYEAR"] != "") {
        FINYEAR = qs["FINYEAR"].trim();
    }

    $('#pnlDisplay').css("display", "");
    finyearChecking();
    loadDepot(UTNAME, userid);
    LoadGroup();

   

    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": true,
        "preventDuplicates": true,
        "positionClass": "toast-top-center",
        "onclick": null,
        "showDuration": "500",
        "hideDuration": "500",
        "timeOut": "3000",
        "extendedTimeOut": "500",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "slideDown",
        "hideMethod": "slideUp"
    };
    $(".date-picker").datepicker({
        dateFormat: 'dd-mm-yy'
    });
    $("#txtfromdate").datepicker({
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        selectCurrent: true,
        todayBtn: "linked",
        showAnim: "slideDown",
        yearRange: "-3:+0",
        maxDate: new Date(currentdt),
        minDate: new Date(frmdate),
        dateFormat: "dd/mm/yy",
        showOn: 'button',
        buttonText: 'Show Date',
        buttonImageOnly: true,
        buttonImage: 'http://jqueryui.com/resources/demos/datepicker/images/calendar.gif'
    });
    $(".ui-datepicker-trigger").mouseover(function () {
        $(this).css('cursor', 'pointer');
    });
    $("#txtfromdate").val(getCurrentDate());
    $("#txttodate").datepicker({
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        selectCurrent: true,
        todayBtn: "linked",
        showAnim: "slideDown",
        yearRange: "-3:+0",
        maxDate: new Date(currentdt),
        minDate: new Date(frmdate),
        dateFormat: "dd/mm/yy",
        showOn: 'button',
        buttonText: 'Show Date',
        buttonImageOnly: true,
        buttonImage: 'http://jqueryui.com/resources/demos/datepicker/images/calendar.gif'
    });
    $(".ui-datepicker-trigger").mouseover(function () {
        $(this).css('cursor', 'pointer');
    });
    $("#txttodate").val(getCurrentDate());

    $("#ddlcalender").change(function () {
        var tag = "";
        var calanderId = $("#ddlcalender").val();
        if (calanderId == "5") {
            BindJC();
            $("#lblcalender").val("JC");
        }
        else {
            if (calanderId == "1") {
                $("#lblcalender").val("YEAR");
                tag = "Y";
            }
            else if (calanderId == "2") {
                $("#lblcalender").val("QUARTER");
                tag = "Q";
            }
            else if (calanderId == "3") {
                $("#lblcalender").val("MONTH");
                tag = "M";
            }
            else if (calanderId == "4") {
                $("#lblcalender").val("WEEK");
                tag = "W";
            }
            else {
                $("#txtfromdate").removeAttr("disabled"); 
                $("#txttodate").removeAttr("disabled"); 
            }
            bindTimeSpan(tag, FINYEAR);
        }
       

    })

    $("#ddlspan").change(function () {
        spanChangeFunction();
    })

        ("#btnSearch").click(function () {
            bindGrid();
        })
})

function finyearChecking() {
    debugger;
        $.ajax({
        type: "POST",
        url: "/Reports/Reportfinyrchk",
        data: { _finyr : FINYEAR},
        async: false,
        dataType: "json",
        success: function (response) {
                debugger;
                   $.each(response, function () {
                       currentdt = this['currentdt'];
                       frmdate = this['frmdate'];
                       todate = this['todate'];
        
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

function bindGrid() {

    var depot = $("#ddldepot option:selected");
    var depot1 = "";
    depot.each(function () {
        depot1 += $(this).val() + ',';
    });
    var depotId = depot1.substring(0, depot1.length - 1);
    var fromdDate = $('#txtfromdate').val();
    var toDate = $('#txttodate').val();
    var ledgerId = $('#ddlledger').val();
    /* bind ledger Report */
    var srl = 0;
    srl = srl + 1;
    $.ajax({
        type: "POST",
        url: "/Reports/BindLedgerReport",
        data: { fromDate, toDate, ledgerId, depotId, FINYEAR },
        dataType: "json",
        success: function (response) {
            debugger;
            var tr;
            tr = $('#grdReportGrid');
            tr.append("<thead style='background - color: cornflowerblue; font - size: 14px'><th> Depot</th><th>Date</th><th>Particulars</th><th>Vch.Type</th><th>Vch.No.</th><th>Doc.No.</th><th>Debit</th><th>Credit</th><th>Balance</th><th></th><th>Narration</th><th>all</th><th>voucher print</th></tr></thead>");
            $('#grdReportGrid').DataTable().destroy();
            $("#grdReportGrid tbody tr").remove();
            for (var i = 0; i < response.length; i++) {
                tr = $('<tr/>');
                tr.append("<td style='text-align: center; display: none;'>" + response[i].AccEntryID + "</td>");//0
                tr.append("<td style='text-align: center; display: none;'>" + response[i].AccEntryID + "</td>");//1
                tr.append("<td>" + response[i].REGIONNAME + "</td>");//2
                tr.append("<td>" + response[i].VOUCHERDATE + "</td>");//3
                tr.append("<td>" + response[i].LedgerName + "</td>");//4
                tr.append("<td>" + response[i].VoucherTypeNAME + "</td>");//5
                tr.append("<td>" + response[i].VoucherNo + "</td>");//6
                tr.append("<td>" + response[i].DOCNO + "</td>");//7
                tr.append("<td>" + response[i].Debit + "</td>");//8
                tr.append("<td>" + response[i].Credit + "</td>");//9
                tr.append("<td>" + response[i].Balance + "</td>");//10
                tr.append("<td>" + response[i].Balance_DR_CR + "</td>");//11
                tr.append("<td>" + response[i].Narration + "</td>");//12
                tr.append("<td style='text-align: center; display: none;'>" + response[i].AccEntryID + "</td>");//13
                tr.append("<td style='text-align: center; display: none;'>" + response[i].AccEntryID + "</td>");//14

                $("#grdReportGrid").append(tr);
            }
            tr.append("</tbody>");
          
            $('#grdReportGrid').DataTable({
                "sScrollX": '100%',
                "sScrollXInner": "110%",
                "scrollY": "300px",
                "scrollCollapse": true,
                "dom": 'Bfrtip',
                buttons: [
                    {
                        extend: 'excelHtml5',
                        title: 'Item Ledger' + ' for ' + productName + '(' + depotName + ')' + ' ' + fromDate + ' to ' + toDate
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
                "ordering": false
            });


        }
    })
}

function loadDepot(UTNAME, userid){
    $.ajax({
        type: "POST",
        url: "/Reports/Region_foraccounts",
        data: { UTNAME, userid },
        async: false,
        dataType: "json",
        success: function (response) {
            var ddldepot = $("#ddldepot");

            debugger;
            //alert(JSON.stringify(response));
            ddldepot.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(response, function () {
                ddldepot.val(this['BRNAME']);
                ddldepot.append($("<option value=''></option>").val(this['BRID']).html(this['BRNAME']));
            });
            ddldepot.chosen();
            ddldepot.trigger("chosen:updated");


        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function LoadGroup() {

}

function LedgerNameBind() {
    var depot = $("#ddldepot option:selected");
    var depot1 = "";
    depot.each(function () {
        depot1 += $(this).val() + ',';
    });
    var depotID = depot1.substring(0, depot1.length - 1);

    $.ajax({
        type: "POST",
        url: "/Reports/BindForLedgerReport_DepotWise",
        data: { DEPOTID: depotID },
        async: false,
        dataType: "json",
        success: function (response) {
            var ddlledger = $("#ddlledger");

            debugger;
            //alert(JSON.stringify(response));
            ddlledger.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(response, function () {
                ddlledger.val(this['LedgerName']);
                ddlledger.append($("<option value=''></option>").val(this['LedgerId']).html(this['LedgerName']));
            });
            ddlledger.chosen();
            ddlledger.trigger("chosen:updated");


        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });

}

function BindJC() {
    var mode="JCMASTER"
    $.ajax({
        type: "POST",
        url: "/Reports/LoadJc",
        data: { mode },
        async: false,
        dataType: "json",
        success: function (response) {
            var ddlspan = $("#ddlspan");
            debugger;
            //alert(JSON.stringify(response));
            ddlspan.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(response, function () {
                ddlspan.val(this['NAME']);
                ddlspan.append($("<option value=''></option>").val(this['JCID']).html(this['NAME']));
            });
            ddlspan.chosen();
            ddlspan.trigger("chosen:updated");


        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function bindTimeSpan(tag, FINYEAR) {
    var mode = "TIMESPAN";
    $.ajax({
        type: "POST",
        url: "/Reports/LoadTimeSpan",
        data: { mode,Tag : tag , Finyear : FINYEAR},
        async: false,
        dataType: "json",
        success: function (response) {
            var ddlspan = $("#ddlspan");
            debugger;
            ddlspan.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(response, function () {
                ddlspan.val(this['TIMESPAN']);
                ddlspan.append($("<option value=''></option>").val(this['TIMESPAN']).html(this['TIMESPAN']));
            });
            ddlspan.chosen();
            ddlspan.trigger("chosen:updated");


        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function spanChangeFunction() {
    var tag = $("#ddlcalender").val();
    var span = $("#ddlspan option:selected");
    var span1 = "";
    span.each(function () {
        span1 += $(this).val() + ',';
    });
    var spanId = span1.substring(0, span1.length - 1);
    $.ajax({
        type: "POST",
        url: "/Reports/FetchDateRange",
        data: { Span: spanId, tag, _FinYear: FINYEAR },
        async: false,
        dataType: "json",
        success: function (response) {
            $.each(response, function () {
                ("#txtfromdate").val(this['STARTDATE']);
                ("#txttodate").val(this['ENDDATE']);
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
