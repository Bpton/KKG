var currentdt;
var frmdate;
var SOID;
var BSID

$(document).ready(function () {
    var qs = getQueryStrings();
    if (qs["MENUID"] != undefined && qs["MENUID"] != "") {
        menuID = qs["MENUID"];
    }
    if (qs["USERID"] != undefined && qs["USERID"] != "") {
        SOID = qs["USERID"];
    }
   


    $("#ddlspan").change(function () {
        spanChangeFunction();
    })

    $("#btnSearch").click(function () {
        
        $("#btnSearch").hide();
        bindUserJdGrid();
        
    })

})

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

function getCurrentDate() {
    today_date = new Date();
    today_Date_Str = ((today_date.getDate() < 10) ? "0" : "") + String(today_date.getDate()) + "/" + ((today_date.getMonth() < 9) ? "0" : "") + String(today_date.getMonth() + 1) + "/" + today_date.getFullYear();
    return today_Date_Str;
}



/*-----bindgrid----------*/
function bindUserJdGrid() {
    debugger;
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
        url: "/distributorkyc/bindUserJdReport",
        data: '{}',
        dataType: "json",
        success: function (response) {
            debugger;
            var tr;
            tr = $("#jdReportGrid");
            tr.append("<thead style='background - color: cornflowerblue; font - size: 14px'><th>Sl.No</th><th>Upload Date</th><th>User</th><th>User Type</th><th>Reporting Person</th><th>Reporting Type</th><th>Remarks</th><th>File</th></tr></thead>");

            $("#jdReportGrid").DataTable().destroy();
            $("#jdReportGrid tbody tr").remove();
            for (var i = 0; i < response.length; i++) {

                tr = $('<tr/>');
                tr.append("<td style='text-align: center'>" + srl + "</td>");//0
                tr.append("<td>" + response[i].UPLOADDATE + "</td>");//1
                tr.append("<td>" + response[i].UPLOADBY_USER + "</td>");//2
                tr.append("<td>" + response[i].PARTY_TYPE + "</td>");//3
                tr.append("<td>" + response[i].REPORTING_PERSON + "</td>");//4
                tr.append("<td>" + response[i].REPORTING_PERSON_USERTYPE + "</td>");//5
                tr.append("<td>" + response[i].REMARKS + "</td>");//6
                tr.append("<td><a href='" + response[i].FILE_PATH + "' target='_blank'  </a> <img src='../Images/download.png' width='20' height ='20' title='Download'</td>");

                $("#jdReportGrid").append(tr);
            }
            tr.append("</tbody>");
            RowCountjdList();
            $('#jdReportGrid').DataTable({
                "sScrollX": '100%',
                "sScrollXInner": "110%",
                "scrollY": "300px",
                "scrollCollapse": true,
                "dom": 'Bfrtip',
                buttons: [
                    {
                        extend: 'excelHtml5',
                        title: 'jd document List'
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
                fixedColumns: {
                    leftColumns: 7
                },
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
    $("#imgLoader").css("visibility", "hidden");
    $("#dialog").dialog("close");
}

function RowCountjdList() {
    debugger;
    var table = document.getElementById("jdReportGrid");
    var rowCount = document.getElementById("jdReportGrid").rows.length - 1;
    if (rowCount > 0) {
        for (var i = 1; i <= rowCount; i++) {
            table.rows[i].cells[0].innerHTML = i.toString();
        }
    }
}

//function removeLoader() {
//    $("#loadingDiv").fadeOut(500, function () {
//        // fadeOut complete. Remove the loading div
//        $("#loadingDiv").remove(); //makes page more lightweight 
//    });
//}

