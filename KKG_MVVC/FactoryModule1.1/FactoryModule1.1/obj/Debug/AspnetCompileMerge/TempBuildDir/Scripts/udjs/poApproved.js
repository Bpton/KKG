/*developer info
Pritam Basu
Purpose:purchase order approve
start date:18/11/2020
end date:18/11/2020
*/
var currentdt;
var currentdt1;
var frmdate;
var todate;
var today;
$(document).ready(function () {
    var qs = getQueryStrings();
    if (qs["FINYEAR"].toString() != undefined && qs["FINYEAR"].toString() != "") {
        $("#hdnFinYear").val(qs["FINYEAR"].toString());
    }

    if (qs["USERID"].toString() != undefined && qs["USERID"].toString() != "") {
        $("#hdnUserID").val(qs["USERID"].toString());
    }

    $('#pnlDisplay').show();
    finyearChecking();
    loadDepot();
    $("#ddldepot").prop("disabled", false);
    $("#ddldepot").chosen({
        search_contains: true
    });
    $("#ddldepot").trigger("chosen:updated");

    toastr.options = {
        "closeButton": true,
        "debug": false,
        "positionClass": "toast-top-center",
        "onclick": null,
        "showDuration": "500",
        "hideDuration": "1200",
        "timeOut": "3200",
        "extendedTimeOut": "6200",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "slideDown",
        "hideMethod": "slideUp"
    };

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
    $("#txtfromdate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
    $("#txttodate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());

    /*po search*/
    $("#btnSearch").click(function (e) {
      bindPoGrid();
    })
    /*PO APPROVED*/
    $("body").on("click", "#grdPoApproved .gvApproved", function () {
        if (confirm("Do you want to Approve this Po?")) {
            var poid = "";
            var row = $(this).closest("tr");
            poid = row.find('td:eq(1)').html();
            approvedPo(poid);
        }
    })
})

function loadDepot() {
    var ddldepot = $("#ddldepot");
    $.ajax({
        type: "POST",
        url: "/mmpo/getFactory",
        data: {},
        async: false,
        dataType: "json",
        success: function (response) {
            ddldepot.empty().append('<option selected="selected" value="0">---SELECT---</option>');
            $.each(response, function () {
                ddldepot.append($("<option></option>").val(this['DEPOTID']).html(this['DEPOTNAME']));
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

function bindPoGrid() {
    var srl = 0;
    srl = srl + 1;
    $.ajax({
        type: "POST",
        url: "/mmpo/bindPendingPo",
        data: { FromDate: $('#txtfromdate').val().trim(), ToDate: $('#txttodate').val().trim(), Depotid: $('#ddldepot').val(),FinYear: $('#hdnFinYear').val().trim() },
        dataType: "json",
        success: function (response) {
            var tr;
            tr = $('#grdPoApproved');
            tr.append("<thead><tr><th>Sl. No.</th><th style='display: none'>POID</th><th>Purchase Number</th><th>Purchase Order Date</th><th>RaisedFrom</th><th>VendorName</th><th>Amount</th><th>Status</th><th>Finyear</th><th>Approve</th>");

            $('#grdPoApproved').DataTable().destroy();
            $("#grdPoApproved tbody tr").remove();
            for (var i = 0; i < response.length; i++) {
                tr = $('<tr/>');
                tr.append("<td style='text-align: center'>" + srl + "</td>");//0
                tr.append("<td style='display: none'>" + response[i].POID + "</td>");
                if (response[i].STATUS.trim() == 'APPROVED') {
                    tr.append("<td style='text-align: center'><font color='#00B300'>" + response[i].PONO + "</td>");
                }
                else {
                    tr.append("<td style='text-align: center'><font color='#B30000'>" + response[i].PONO + "</td>");
                }
                tr.append("<td>" + response[i].Todate + "</td>");
                tr.append("<td>" + response[i].ENTRYFROM + "</td>");
                tr.append("<td>" + response[i].VENDORNAME + "</td>");
                tr.append("<td>" + response[i].NETAMT + "</td>");
                if (response[i].STATUS.trim() == 'APPROVED') {
                    tr.append("<td style='text-align: center'><font color='#00B300'>" + response[i].STATUS + "</td>");
                }
                else {
                    tr.append("<td style='text-align: center'><font color='#B30000'>" + response[i].STATUS + "</td>");
                }
                tr.append("<td>" + response[i].FINYEAR + "</td>");

                tr.append("<td style='text-align: center'><input type='image' class='gvApproved'  id='btnApproved'  <img src='../Images/success.png' width='20' height ='20' title='Approved'/></input></td>");
                $("#grdPoApproved").append(tr);
            }
            tr.append("</tbody>");
            RowCount();
            $('#grdPoApproved').DataTable({
                "sScrollX": '100%',
                "sScrollXInner": "110%",
                "scrollY": "238px",
                "scrollCollapse": true,
                "dom": 'Bfrtip',
                buttons: [
                    {
                        extend: 'excelHtml5',
                        title: 'Purchase Order List'
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
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function approvedPo(poid) {
    var message = "";
    $.ajax({
        type: "POST",
        url: "/mmpo/updatePo",
        data: { POID: poid, USERID: $("#hdnUserID").val()},
        async: false,
        dataType: "json",
        success: function (response) {
            message = response[0].response;
            if (message == 1) {
                toastr.success('Approved Done');
                bindPoGrid();
                return;
            }
            else {
                toastr.error('Error On Updating');
                return;
            }

        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function finyearChecking(finyear) {

    //debugger;
    //fin yr check
    var currentdt;
    var frmdate;
    var todate;
    $.ajax({
        type: "POST",
        url: "/TranDepot/finyrchk",
        data: { FinYear: $("#hdnFinYear").val() },
        async: false,
        dataType: "json",
        success: function (response) {
            //debugger;
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

function RowCount() {
    var table = document.getElementById("grdPoApproved");
    var rowCount = document.getElementById("grdPoApproved").rows.length - 1;
    if (rowCount > 0) {
        for (var i = 1; i <= rowCount; i++) {
            table.rows[i].cells[0].innerHTML = i.toString();
        }
    }
}
