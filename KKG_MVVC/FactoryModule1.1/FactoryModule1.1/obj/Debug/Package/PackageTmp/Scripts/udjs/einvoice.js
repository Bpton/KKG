
var currentdt;
var frmdate;
var todate;
var menuid;
var userid;
var tpuid;
var Iuserid;
var FINYEAR;
var UTNAME;

$(document).ready(function () {

    var qs = getQueryStrings();

    if (qs["USERID"] != undefined && qs["USERID"] != "") {
        userid = qs["USERID"].trim();
    }
    if (qs["UTNAME"] != undefined && qs["UTNAME"] != "") {
        UTNAME = qs["UTNAME"].trim();
    }
    //finyearChecking();

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
    bindAllDepot();


    /*Depot Change Event*/

    $('#Depot').change(function (e) {
        BindPendingEinvoiceGrid();
        e.preventDefault();
    });
});

//For Current Date
function getCurrentDate() {
    today_date = new Date();
    today_Date_Str = ((today_date.getDate() < 10) ? "0" : "") + String(today_date.getDate()) + "/" + ((today_date.getMonth() < 9) ? "0" : "") + String(today_date.getMonth() + 1) + "/" + today_date.getFullYear();
    return today_Date_Str;
}

//For Financial year checking
function finyearChecking() {
    debugger;
    $.ajax({
        type: "POST",
        url: "/Reports/Reportfinyrchk",
        data: { _finyr: FINYEAR },
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

function bindAllDepot() {
    var Depot = $("#Depot");
    $("#dialog").dialog({
        autoOpen: true,
        modal: true,
        title: "Loading.."
    });
    $("#imgLoader").css("visibility", "visible");
    $.ajax({
        type: "POST",
        url: "/Reports/BindDepot_Primary",
        data: '{}',
        dataType: "json",
        success: function (response) {
            Depot.empty().append('<option selected="selected" value="0">Select Depot</option>');
            Depot.append('<option value="-1">All</option>');
            $.each(response, function () {
                Depot.append($("<option></option>").val(this['BRID']).html(this['BRNAME']));
            });
            $("#Depot").chosen({
                search_contains: true
            });
            $("#Depot").trigger("chosen:updated");

            $("#imgLoader").css("visibility", "hidden");
            $("#dialog").dialog("close");
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

function BindPendingEinvoiceGrid() {
    debugger;
    var fromdDate = $('#txtfromdate').val().trim();
    var toDate = $('#txttodate').val().trim();
    var DepotId = $('#Depot').val().trim();
    /* bind einvoice Report */
    var srl = 0;
    srl = srl + 1;
    $("#dialog").dialog({
        autoOpen: true,
        modal: true,
        title: "Loading.."
    });
    $("#imgLoader").css("visibility", "visible");
    $.ajax({
        type: "POST",
        url: "/Reports/BindPendingEinvoiceGrid",
        data: { FromDate: fromdDate, ToDate: toDate, depotID: DepotId },
        dataType: "json",
        success: function (response) {
           
            //alert("Success");

            var tr;
            tr = $('#eInvoiceGrid');
            tr.append("<thead><tr><th>Sl. No.</th><th>Ack.No.</th><th>Ack. Date</th><th>Invoice No</th><th>Invoice Date</th><th>Depot/Factory</th><th>Party</th>");

            $('#eInvoiceGrid').DataTable().destroy();
            $("#eInvoiceGrid tbody tr").remove();
            for (var i = 0; i < response.length; i++) {
                tr = $('<tr/>');
                tr.append("<td style='text-align: center'>" + srl + "</td>");//0
                tr.append("<td>" + response[i].AckNo + "</td>");
                tr.append("<td>" + response[i].AckDt + "</td>");
                tr.append("<td>" + response[i].DOCNO + "</td>");
                tr.append("<td>" + response[i].DATE + "</td>");
                tr.append("<td>" + response[i].DEPOT + "</td>");
                tr.append("<td>" + response[i].partyname + "</td>");
                $("#eInvoiceGrid").append(tr);
            }
            tr.append("</tbody>");
            RowCount();
            $('#eInvoiceGrid').DataTable({
                "sScrollX": '100%',
                "sScrollXInner": "110%",
                "scrollY": "238px",
                "scrollCollapse": true,
                "dom": 'Bfrtip',
                buttons: [
                    {
                        extend: 'excelHtml5',
                        title: 'GT Invoice List'
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
            $("#imgLoader").css("visibility", "hidden");
            $("#dialog").dialog("close");

            $("#imgLoader").css("visibility", "hidden");
            $("#dialog").dialog("close");
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }


    });
}

/*Serial Number counting*/
function RowCount() {
    var table = document.getElementById("eInvoiceGrid");
    var rowCount = document.getElementById("eInvoiceGrid").rows.length - 1;
    if (rowCount > 0) {
        for (var i = 1; i <= rowCount; i++) {
            table.rows[i].cells[0].innerHTML = i.toString();
        }
    }
}

function ClearControls() {
    $('#eInvoiceGrid').empty();
}