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
    if (qs["BSID"] != undefined && qs["BSID"] != "") {
        BSID = qs["BSID"];
    }


    $("#ddlspan").change(function () {
        spanChangeFunction();
    })

    $("#btnSearch").click(function () {
        
        if (BSID == '0AA9353F-D350-4380-BC84-6ED5D0031E24') {
            $('body').append('<div style="" id="loadingDiv"><div class="loader">Loading Wait Please...</div></div>');
            bindGridmT(BSID);
            setTimeout(removeLoader, 500);
            return;
        }
        else if (BSID == '7F62F951-9D1F-4B8D-803B-74EEBA468CEE') {
            $('body').append('<div style="" id="loadingDiv"><div class="loader">Loading Wait Please...</div></div>');
            bindGrid(BSID);
            setTimeout(removeLoader, 500);
            return;
        }

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

function bindGridmT(BSID) {
   
    debugger
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
        url: "/distributorkyc/binddistributorKycpanReport",
        data: { BSID: BSID },
        dataType: "json",
        success: function (response) {
            debugger;
            var tr;
            tr = $('#grdReportGridMT');
            tr.append("<thead style='background - color: cornflowerblue; font - size: 14px'><th> DEPOTNAME</th><th>KAOCODE</th><th>KAONAME</th><th>KAMCODE</th><th>KAMNAME</th><th>CODE</th><th>CUSTOMERTYPE</th><th>CUSTOMERNAME</th><th>CONTACTPERSON1</th><th>CONTACTPERSON2</th><th>Address</th><th>CITYNAME</th><th>PIN</th><th>WHATSAPP NO</th><th>OTHERS COMPANY</th><th>COMPANY_NAME</th><th>Total godown size</th><th>Godown Size</th><th>NO_sale_person</th><th>No_of_delivry_person</th><th>Total_anual_turnover</th><th>McNROE_turn_over</th><th>DATE</th></tr></thead>");
            $('#grdReportGridMT').DataTable().destroy();
            $("#grdReportGridMT tbody tr").remove();
            for (var i = 0; i < response.length; i++) {
                tr = $('<tr/>');
                //tr.append("<td style='text-align: center; display: none;'>" + response[i].DISTRIBUTORID + "</td>");//0
                //tr.append("<td style='text-align: center; display: none;'>" + response[i].DIST_TYPEID + "</td>");//1
                tr.append("<td>" + response[i].DEPOTNAME + "</td>");//1
                tr.append("<td>" + response[i].KAOCODE + "</td>");//2
                tr.append("<td>" + response[i].KAONAME + "</td>");//3
                tr.append("<td>" + response[i].KAMCODE + "</td>");//4
                tr.append("<td>" + response[i].KAMNAME + "</td>");//5
                tr.append("<td>" + response[i].CODE + "</td>");//6
                tr.append("<td>" + response[i].CUSTOMERTYPE + "</td>");//7
                tr.append("<td>" + response[i].CUSTOMERNAME + "</td>");//8
                tr.append("<td>" + response[i].CONTACTPERSON1 + "</td>");//9
                tr.append("<td>" + response[i].CONTACTPERSON2 + "</td>");//10
                tr.append("<td>" + response[i].ADDRESS + "</td>");//11
                tr.append("<td>" + response[i].CITYNAME + "</td>");//12
                tr.append("<td>" + response[i].PIN + "</td>");//13
                tr.append("<td>" + response[i].WHATSAPPNO + "</td>");//14
                tr.append("<td>" + response[i].OTHERS_COMPANY + "</td>");//15
                tr.append("<td>" + response[i].COMPANY_NAME + "</td>");//15
                tr.append("<td>" + response[i].Total_Godown_Size + "</td>");//16
                tr.append("<td>" + response[i].Godown_Size_McNROE + "</td>");//17
                tr.append("<td>" + response[i].No_Sales_Person + "</td>");//18
                tr.append("<td>" + response[i].No_Delivery_Person + "</td>");//19
                tr.append("<td>" + response[i].Total_Annual_Turnover_Lakh + "</td>");//20
                tr.append("<td>" + response[i].McNROE_Turnover_Lakh + "</td>");//21
                tr.append("<td>" + response[i].ISKYC_DATE + "</td>");//22



                $("#grdReportGridMT").append(tr);
            }
            tr.append("</tbody>");

            $('#grdReportGridMT').DataTable({
                "sScrollX": '100%',
                "sScrollXInner": "110%",
                "scrollY": "300px",
                "scrollCollapse": true,
                "dom": 'Bfrtip',
                buttons: [
                    {
                        extend: 'excelHtml5',
                        title: 'Panindia Kyc Report',
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
    $("#imgLoader").css("visibility", "hidden");
    $("#dialog").dialog("close");
}

function bindGrid(BSID) {
    debugger


    //var fromDate = $('#txtfromdate').val();
    //var toDate = $('#txttodate').val();

    /* bind ledger Report */
    var srl = 0;
    srl = srl + 1;
    $.ajax({
        type: "POST",
        url: "/distributorkyc/binddistributorKycpanReport",
        data: { BSID: BSID },
        dataType: "json",
        success: function (response) {
            debugger;
            var tr;
            tr = $('#grdReportGrid');
            tr.append("<thead style='background - color: cornflowerblue; font - size: 14px'><th> DEPOTNAME</th><th>SOCODE</th><th>SONAME</th><th>ASMCODE</th><th>ASMNAME</th><th>CODE</th><th>CUSTOMERTYPE</th><th>CUSTOMERNAME</th><th>OTHERS COMPANY</th><th>COMPANY</th><th>Conatact person</th><th>Address</th><th>Pin</th><th>Cityname</th><th>DOB</th><th>Anv Date</th><th>Total godown size</th><th>Godown Size</th><th>NO_sale_person</th><th>No_of_delivry_person</th><th>Total_anual_turnover</th><th>McNROE_turn_over</th><th>DATE</th></tr></thead>");
            $('#grdReportGrid').DataTable().destroy();
            $("#grdReportGrid tbody tr").remove();
            for (var i = 0; i < response.length; i++) {
                tr = $('<tr/>');
                //tr.append("<td style='text-align: center; display: none;'>" + response[i].DISTRIBUTORID + "</td>");//0
                //tr.append("<td style='text-align: center; display: none;'>" + response[i].DIST_TYPEID + "</td>");//1
                tr.append("<td>" + response[i].DEPOTNAME + "</td>");//2
                tr.append("<td>" + response[i].SOCODE + "</td>");//3
                tr.append("<td>" + response[i].SONAME + "</td>");//4
                tr.append("<td>" + response[i].ASMCODE + "</td>");//5
                tr.append("<td>" + response[i].ASMNAME + "</td>");//6
                tr.append("<td>" + response[i].CODE + "</td>");//7
                tr.append("<td>" + response[i].CUSTOMERTYPE + "</td>");//8
                tr.append("<td>" + response[i].CUSTOMERNAME + "</td>");//9
                tr.append("<td>" + response[i].OTHERS_COMPANY + "</td>");//10
                tr.append("<td>" + response[i].COMPANY_NAME + "</td>");//10
                tr.append("<td>" + response[i].CONTACTPERSON1 + "</td>");//10
                tr.append("<td>" + response[i].ADDRESS + "</td>");//10
                tr.append("<td>" + response[i].PIN + "</td>");//10
                tr.append("<td>" + response[i].CITYNAME + "</td>");//10
                tr.append("<td>" + response[i].DOB + "</td>");//10
                tr.append("<td>" + response[i].ANVDATE + "</td>");//10
                tr.append("<td>" + response[i].Total_Godown_Size + "</td>");//11
                tr.append("<td>" + response[i].Godown_Size_McNROE + "</td>");//12
                tr.append("<td>" + response[i].No_Sales_Person + "</td>");//13
                tr.append("<td>" + response[i].No_Delivery_Person + "</td>");//14
                tr.append("<td>" + response[i].Total_Annual_Turnover_Lakh + "</td>");//15
                tr.append("<td>" + response[i].McNROE_Turnover_Lakh + "</td>");//16
                tr.append("<td>" + response[i].ISKYC_DATE + "</td>");//17
                


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
                        title: 'Panindia Kyc Report',
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

function removeLoader() {
    $("#loadingDiv").fadeOut(1000, function () {
        // fadeOut complete. Remove the loading div
        $("#loadingDiv").remove(); //makes page more lightweight 
    });
}


