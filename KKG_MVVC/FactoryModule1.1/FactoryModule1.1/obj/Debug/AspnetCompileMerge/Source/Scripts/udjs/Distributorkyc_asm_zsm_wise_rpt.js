var currentdt;
var frmdate;
var SMID;
var SMTYPE;

$(document).ready(function () {
    var qs = getQueryStrings();
    if (qs["MENUID"] != undefined && qs["MENUID"] != "") {
        menuID = qs["MENUID"];
    }
    if (qs["USERID"] != undefined && qs["USERID"] != "") {
        SMID = qs["USERID"];
    }
    if (qs["USERTYPE"] != undefined && qs["USERTYPE"] != "") {
        SMTYPE = qs["USERTYPE"];
    }


    $("#ddlspan").change(function () {
        spanChangeFunction();
    })

    //$(".date-picker").datepicker({
    //    dateFormat: 'dd-mm-yy'
    //});
    //$("#txtfromdate").datepicker({
    //    changeMonth: true,
    //    changeYear: true,
    //    showButtonPanel: true,
    //    selectCurrent: true,
    //    todayBtn: "linked",
    //    showAnim: "slideDown",
    //    yearRange: "-3:+0",
    //    maxDate: new Date(currentdt),
    //    minDate: new Date(frmdate),
    //    dateFormat: "dd/mm/yy",
    //    showOn: 'button',
    //    buttonText: 'Show Date',
    //    buttonImageOnly: true,
    //    buttonImage: 'http://jqueryui.com/resources/demos/datepicker/images/calendar.gif'
    //});
    //$(".ui-datepicker-trigger").mouseover(function () {
    //    $(this).css('cursor', 'pointer');
    //});
    //$("#txtfromdate").val(getCurrentDate());

    //$("#txttodate").datepicker({
    //    changeMonth: true,
    //    changeYear: true,
    //    showButtonPanel: true,
    //    selectCurrent: true,
    //    todayBtn: "linked",
    //    showAnim: "slideDown",
    //    yearRange: "-3:+0",
    //    maxDate: new Date(currentdt),
    //    minDate: new Date(frmdate),
    //    dateFormat: "dd/mm/yy",
    //    showOn: 'button',
    //    buttonText: 'Show Date',
    //    buttonImageOnly: true,
    //    buttonImage: 'http://jqueryui.com/resources/demos/datepicker/images/calendar.gif'
    //});
    //$(".ui-datepicker-trigger").mouseover(function () {
    //    $(this).css('cursor', 'pointer');
    //});
    //$("#txttodate").val(getCurrentDate());


    $("#btnSearch").click(function () {
      
        $('body').append('<div style="" id="loadingDiv"><div class="loader">Loading Wait Please...</div></div>');
        $("#btnSearch").hide();
        bindGrid();
       

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


function bindGrid() {
    debugger


    //var fromDate = $('#txtfromdate').val();
    //var toDate = $('#txttodate').val();

    /* bind ledger Report */
    var srl = 0;
    srl = srl + 1;
    $.ajax({
        type: "POST",
        url: "/distributorkyc/binddistributorKyc_asm_zsmReport",
        data: { USERID: SMID, USERTYPE: SMTYPE },
        dataType: "json",
        success: function (response) {
            debugger;
            var tr;
            tr = $('#grdReportGrid');
            tr.append("<thead style='background - color: cornflowerblue; font - size: 14px'><th> DEPOTNAME</th><th>ZSMCODE</th><th>ZSMNAME</th><th>RSMNAME</th><th>RSMCODE</th><th>SOCODE</th><th>SONAME</th><th>ASMCODE</th><th>ASMNAME</th><th>CODE</th><th>CUSTOMERTYPE</th><th>CUSTOMERNAME</th><th>OTHERS COMPANY</th><th> COMPANY</th><th>Total godown size</th><th>Godown Size</th><th>NO_sale_person</th><th>No_of_delivry_person</th><th>Total_anual_turnover</th><th>McNROE_turn_over</th><th>DATE</th></tr></thead>");
            $('#grdReportGrid').DataTable().destroy();
            $("#grdReportGrid tbody tr").remove();
            for (var i = 0; i < response.length; i++) {
                tr = $('<tr/>');
                //tr.append("<td style='text-align: center; display: none;'>" + response[i].DISTRIBUTORID + "</td>");//0
                //tr.append("<td style='text-align: center; display: none;'>" + response[i].DIST_TYPEID + "</td>");//1
                tr.append("<td>" + response[i].DEPOTNAME + "</td>");//2
                tr.append("<td>" + response[i].ZSMCODE + "</td>");//2
                tr.append("<td>" + response[i].ZSMNAME + "</td>");//2
                tr.append("<td>" + response[i].RSMNAME + "</td>");//2
                tr.append("<td>" + response[i].RSMCODE + "</td>");//2
                tr.append("<td>" + response[i].SOCODE + "</td>");//3
                tr.append("<td>" + response[i].SONAME + "</td>");//4
                tr.append("<td>" + response[i].ASMCODE + "</td>");//5
                tr.append("<td>" + response[i].ASMNAME + "</td>");//6
                tr.append("<td>" + response[i].CODE + "</td>");//7
                tr.append("<td>" + response[i].CUSTOMERTYPE + "</td>");//8
                tr.append("<td>" + response[i].CUSTOMERNAME + "</td>");//9
                tr.append("<td>" + response[i].OTHERS_COMPANY + "</td>");//10
                tr.append("<td>" + response[i].COMPANY_NAME + "</td>");//10
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
                        title: 'Distributor Kyc Report',
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

    $("#btnSearch").show();
    setTimeout(removeLoader, 2500);
   
}

function removeLoader() {
    $("#loadingDiv").fadeOut(2500, function () {
        // fadeOut complete. Remove the loading div
        $("#loadingDiv").remove(); //makes page more lightweight 
    });
}


