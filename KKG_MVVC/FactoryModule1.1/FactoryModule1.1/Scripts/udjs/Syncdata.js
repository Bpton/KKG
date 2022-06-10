var MENUID
var CHECKER;
var DEPOTID;
var currentdt;
var frmdate;
var todate;
var FINYEAR;
var UserID;
var TPU;
var usertype;
$(document).ready(function () {

    var qs = getQueryStrings();
    FINYEAR = qs["FINYEAR"];
    finyrchk();
    Bindsync();
    $("#btnsearch").click(function (e) {
        Bindsync();
    })
})


function finyrchk() {

    //date validation
    $.ajax({
        type: "POST",
        url: "/TranDepotStock/finyrchk",
        //  data: '{}',
        data: { finyr: FINYEAR },
        async: false,
        dataType: "json",

        success: function (response) {


            //alert(JSON.stringify(response));

            //.append('<option selected="selected" value="0">Select Brancht</option>');;
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

    $("#txtfrmdate").datepicker({
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
   
    
    $("#txtfrmdate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
    $("#txttodate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
   

}
function Bindsync() {

    var frmdate = $('#txtfrmdate').val();
    var todate = $('#txttodate').val();
    var srl = 0;
    $.ajax({
        type: "POST",
        url: "/Sync/Cubesync",
        data: { FromDate: frmdate, ToDate: todate, CheckerFlag: CHECKER, depotID: DEPOTID, type: 'FG', finyr: FINYEAR, userid: UserID },
        dataType: "json",
        success: function (response) {
            var tableNew = '<table id="dispatchGrid" class="table table-striped table-bordered dt-responsive nowrap" style="width:100%">';
            tableNew = tableNew + "<thead><tr><th style='display: none'>tablename</th><th>Sl. No.</th><th>Invoice Dt.</th><th>Invoice No.</th><th >Module</th></tr></thead><tbody>";


            for (var i = 0; i < response.length; i++) {
                srl = srl + 1;
                tableNew = tableNew + " <tr>";

                tableNew = tableNew + "<td style='display: none'>" + response[i].TBL_NAME + "</td>";
                tableNew = tableNew + "<td style='text-align: center'>" + srl + "</td>";
                tableNew = tableNew + "<td >" + response[i].INVOICEDATE + "</td>";
                tableNew = tableNew + "<td>" + response[i].INV_NO + "</td>";
                tableNew = tableNew + "<td>" + response[i].MODULENAME + "</td>";
               


            }
            document.getElementById("dispatchdiv").innerHTML = tableNew + '</tbody></table>';
            //RowCountDispatchList();
            $('#dispatchGridFG').DataTable({
                "sScrollX": '100%',
                "sScrollXInner": "110%",
                "scrollY": "238px",
                "scrollCollapse": true,
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
                "ordering": false
            });
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