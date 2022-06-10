var currentdt;
var frmdate;
var SOID;
var MTID
var MTTYPE

$(document).ready(function () {
    var qs = getQueryStrings();
    if (qs["MENUID"] != undefined && qs["MENUID"] != "") {
        menuID = qs["MENUID"];
    }
    if (qs["USERID"] != undefined && qs["USERID"] != "") {
        SOID = qs["USERID"];
    }
    if (qs["MTID"] != undefined && qs["MTID"] != "") {
        MTID  = qs["MTID"];
    }
    if (qs["USERTYPE"] != undefined && qs["USERTYPE"] != "") {
        MTTYPE = qs["USERTYPE"];
    }
   


    $("#ddlspan").change(function () {
        spanChangeFunction();
    })

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

    $("#btnSearch").click(function () {

        if (MTID == '0AA9353F-D350-4380-BC84-6ED5D0031E24') {
            $('body').append('<div style="" id="loadingDiv"><div class="loader">Loading Wait Please...</div></div>');
            setTimeout(removeLoader, 1000);
            bindGridmT(MTID,MTTYPE);

        }
        else if (MTID == '7F62F951-9D1F-4B8D-803B-74EEBA468CEE') {
            $('body').append('<div style="" id="loadingDiv"><div class="loader">Loading Wait Please...</div></div>');
            setTimeout(removeLoader, 1000);
            bindGrid(MTID);

        }

    })

  
        //$("#btnSearch").click(function () {
        //    setTimeout(removeLoader, 500);
        //    bindGrid();
            
        //})
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


function bindGrid(MTID) {
    debugger
   
    
    var fromDate = $('#txtfromdate').val();
    var toDate = $('#txttodate').val();
   
    /* bind ledger Report */
    var srl = 0;
    srl = srl + 1;
    $.ajax({
        type: "POST",
        url: "/distributorkyc/binddistributorKycReport",
        data: { USERID: SOID, fromDate, toDate, MTID },
        dataType: "json",
        success: function (response) {
            debugger;
            var tr;
            tr = $('#grdReportGrid');
            tr.append("<thead style='background - color: cornflowerblue; font - size: 14px'><th>ASMCODE</th><th> ASMNAME</th><th> SOCODE</th><th> SONAME</th><th> DATE</th><th>MOBILE2</th><th>CITYNAME</th><th>DISTRIBUTORNAME</th><th>DIST_CODE</th><th>STATE_NAME</th><th>DEPOT_NAME</th><th>DIST_ADDRESS</th><th>CONTACTPERSON</th><th>CONTACTPERSON2</th><th>PIN</th><th>DOB</th><th>AnniversaryDate</th><th>REMARKS</th><th>COMPANY_NAME</th><th>OTHERS_COMPANY</th></tr></thead>");
            $('#grdReportGrid').DataTable().destroy();
            $("#grdReportGrid tbody tr").remove();
            for (var i = 0; i < response.length; i++) {
                tr = $('<tr/>');
                //tr.append("<td style='text-align: center; display: none;'>" + response[i].DISTRIBUTORID + "</td>");//0
                //tr.append("<td style='text-align: center; display: none;'>" + response[i].DIST_TYPEID + "</td>");//1
                tr.append("<td>" + response[i].ASMCODE + "</td>");//1
                tr.append("<td>" + response[i].ASMNAME + "</td>");//2
                tr.append("<td>" + response[i].SOCODE + "</td>");//3
                tr.append("<td>" + response[i].SONAME + "</td>");//4
                tr.append("<td>" + response[i].DATE + "</td>");//5
               
                tr.append("<td>" + response[i].MOBILE2 + "</td>");//7
                tr.append("<td>" + response[i].CITYNAME + "</td>");//8
                tr.append("<td>" + response[i].DISTRIBUTORNAME + "</td>");//9
                tr.append("<td>" + response[i].DIST_CODE + "</td>");//10
                tr.append("<td>" + response[i].STATE_NAME + "</td>");//11
                tr.append("<td>" + response[i].DEPOT_NAME + "</td>");//12
                tr.append("<td>" + response[i].DIST_ADDRESS + "</td>");//13
                tr.append("<td>" + response[i].CONTACTPERSON + "</td>");//14
                tr.append("<td>" + response[i].CONTACTPERSON2 + "</td>");//15
                tr.append("<td>" + response[i].PIN + "</td>");//16
                tr.append("<td>" + response[i].DOB + "</td>");//17
                tr.append("<td>" + response[i].ANVDATE + "</td>");//17
                tr.append("<td>" + response[i].REMARKS + "</td>");//19
                tr.append("<td>" + response[i].COMPANY_NAME + "</td>");//20
                tr.append("<td>" + response[i].OTHERS_COMPANY + "</td>");//21
               
              

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
                        title: 'Distributor Kyc' ,
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
debugger;
function bindGridmT(MTID, MTTYPE) {
    var fromDate = $('#txtfromdate').val();
    var toDate = $('#txttodate').val();
    debugger
    var srl = 0;
    srl = srl + 1;
    $.ajax({
        type: "POST",
        url: "/distributorkyc/binddistributorKycReport",
        data: { USERID: SOID, fromDate, toDate, USERTYPE: MTTYPE, MTID },
        dataType: "json",
        success: function (response) {
            debugger;
            var tr;
            tr = $('#grdReportGridMt');
            tr.append("<thead style='background - color: cornflowerblue; font - size: 14px'><th> DEPOTNAME</th><th>KAOCODE</th><th>KAONAME</th><th>KAMCODE</th><th>KAMNAME</th><th>CUSTOMERCODE</th><th>CUSTOMERNAME</th><th>CONTACTPERSON1</th><th>CONTACTPERSON2</th><th>Address</th><th>CITYNAME</th><th>PIN</th><th>WHATSAPP NO</th><th>OTHERS COMPANY</th><th>COMPANY NAME</th><th>Total godown size</th><th>Godown Size</th><th>NO_sale_person</th><th>No_of_delivry_person</th><th>Total_anual_turnover</th><th>McNROE_turn_over</th><th>REMARKS</th><th>DATE</th></tr></thead>");
            $('#grdReportGridMt').DataTable().destroy();
            $("#grdReportGridMt tbody tr").remove();
            for (var i = 0; i < response.length; i++) {
                tr = $('<tr/>');
                //tr.append("<td style='text-align: center; display: none;'>" + response[i].DISTRIBUTORID + "</td>");//0
                //tr.append("<td style='text-align: center; display: none;'>" + response[i].DIST_TYPEID + "</td>");//1
                tr.append("<td>" + response[i].DEPOT_NAME + "</td>");//1
                tr.append("<td>" + response[i].KAECODE + "</td>");//2
                tr.append("<td>" + response[i].KAENAME + "</td>");//3
                tr.append("<td>" + response[i].KAMCODE + "</td>");//4
                tr.append("<td>" + response[i].KAMNAME + "</td>");//5
                tr.append("<td>" + response[i].DIST_CODE + "</td>");//6
                //tr.append("<td>" + response[i].CUSTOMERCODE + "</td>");//7
                tr.append("<td>" + response[i].DISTRIBUTORNAME + "</td>");//8
                tr.append("<td>" + response[i].CONTACTPERSON + "</td>");//9
                tr.append("<td>" + response[i].CONTACTPERSON2 + "</td>");//10
                tr.append("<td>" + response[i].ADDRESS2 + "</td>");//11
                tr.append("<td>" + response[i].CITYNAME + "</td>");//12
                tr.append("<td>" + response[i].PIN + "</td>");//13
                tr.append("<td>" + response[i].WHATSAPPNO + "</td>");//14
                tr.append("<td>" + response[i].OTHERS_COMPANY + "</td>");//15
                tr.append("<td>" + response[i].COMPANY_NAME + "</td>");//16
                tr.append("<td>" + response[i].Total_Godown_Size + "</td>");//17
                tr.append("<td>" + response[i].Godown_Size_McNROE + "</td>");//18
                tr.append("<td>" + response[i].No_Sales_Person + "</td>");//19
                tr.append("<td>" + response[i].No_Delivery_Person + "</td>");//20
                tr.append("<td>" + response[i].Total_Annual_Turnover_Lakh + "</td>");//20
                tr.append("<td>" + response[i].McNROE_Turnover_Lakh + "</td>");//21
                tr.append("<td>" + response[i].REMARKS + "</td>");//22
                tr.append("<td>" + response[i].DATE + "</td>");//23



                $("#grdReportGridMt").append(tr);
            }
            tr.append("</tbody>");

            $('#grdReportGridMt').DataTable({
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






