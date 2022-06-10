//#region Developer Info
/*
* For Erp itemLdger Report.cshtml Only
* Developer Name : Pritam Basu 
* Start Date     : 01/08/2020
* END Date       : 
*/
//#endregion


var currentdt;
var frmdate;
var todate;
var menuid;
var userid;
var tpuid;
var Iuserid;
var FINYEAR;


$(document).ready(function () {
    debugger;
    $('body').append('<div style="" id="loadingDiv"><div class="loader">Loading Wait Please...</div></div>');
    var qs = getQueryStrings();
    if (qs["MENUID"] != undefined && qs["MENUID"] != "") {
        menuid = qs["MENUID"].trim();
    }
    if (qs["USERID"] != undefined && qs["USERID"] != "") {
        userid = qs["USERID"].trim();
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
   
    finyearChecking();

   
    $('#pnlDisplay').css("display", "");

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

  
      //GETTPU();
      
      bindsourceDepot();
      LoadDepotName();
      LoadAllProduct();
      LoadStorelocation();
    setTimeout(removeLoader, 500);

    $("#btnSearch").click(function () {
        $('body').append('<div style="" id="loadingDiv"><div class="loader">Loading Wait Please...</div></div>');
        if ($("#ddldepot").val() == "0" || $("#ddldepot").val() == "") {
            toastr.warning("Please Select Depot");
            setTimeout(removeLoader, 500);
            return;
        }
        if ($("#ddlnewproduct").val() == "0" || $("#ddlnewproduct").val() == "")
        {
            toastr.warning("Please Select Product");
            setTimeout(removeLoader, 500);
            return;
        }
        bindItemLdgerReport();
        setTimeout(removeLoader, 500);
    })
    

})

function removeLoader() {
    $("#loadingDiv").fadeOut(500, function () {
        // fadeOut complete. Remove the loading div
        $("#loadingDiv").remove(); //makes page more lightweight 
    });
}

function bindsourceDepot() {
   
    $.ajax({
        type: "POST",
        url: "/Reports/GetSourceDepotFromUser",
        data: { userid: userid},
        async: false,
        dataType: "json",
        success: function (response) {
            //alert(JSON.stringify(response));
            document.getElementById("hdn_Depot").value = response[0].BRID;
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
    
   
}

function bindItemLdgerReport() {
    var fromDate = $("#txtfromdate").val();
    var toDate = $("#txttodate").val();
    var depot = $("#ddldepot").val();
    var depotName = $("#ddldepot option:selected").text();
    var product = $("#ddlnewproduct").val();
    var productName = $("#ddlnewproduct option:selected").text();
    var storeLocation = $("#ddlstorelocation").val();
    if (storeLocation == "5FA9501B-E8D0-4A0F-B917-17C636655514") {
        /* bind item ledger for Export only*/
        var srl = 0;
        srl = srl + 1;
        $.ajax({
            type: "POST",
            url: "/Reports/LoadItemLedgerExport",
            data: { fromDate, toDate, depot, product, storeLocation },
            dataType: "json",
            success: function (response) {
                debugger;
                var tr;
                tr = $('#grdItemLedger');
                tr.append("<thead style='background - color: cornflowerblue; font - size: 14px'><th> Sl.</th><th>Date</th><th>Vch No.</th><th>VType</th><th>Party</th><th>Batch No.</th><th>Manf.Date</th><th>Exp.Date</th><th>MRP</th><th>Opening</th><th>Inward</th><th>Outward</th><th>Balance</th><th>In-Transit</th></tr></thead>");
                $('#grdItemLedger').DataTable().destroy();
                $("#grdItemLedger tbody tr").remove();
                for (var i = 0; i < response.length; i++) {
                    tr = $('<tr/>');
                    tr.append("<td style='text-align: center; display: none;'>" + srl + "</td>");//1
                    tr.append("<td>" + response[i].Date + "</td>");//2
                    tr.append("<td>" + response[i].VType + "</td>");//3
                    tr.append("<td>" + response[i].Party + "</td>");//4
                    tr.append("<td>" + response[i].BatchNo + "</td>");//5
                    tr.append("<td>" + response[i].ManfDate + "</td>");//6
                    tr.append("<td>" + response[i].ExpDate + "</td>");//7
                    tr.append("<td>" + response[i].MRP + "</td>");//8
                    tr.append("<td>" + response[i].Opening + "</td>");//9
                    tr.append("<td>" + response[i].Inward + "</td>");//10
                    tr.append("<td>" + response[i].Outward + "</td>");//11
                    tr.append("<td>" + response[i].Balance + "</td>");//12
                    tr.append("<td>" + response[i].INTRANSIST + "</td>");//13

                    $("#grdItemLedger").append(tr);
                }
                tr.append("</tbody>");
                RowCountProcessList();
                $('#grdItemLedger').DataTable({
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
    else {
        var srl = 0;
        srl = srl + 1;
        $.ajax({
            type: "POST",
            url: "/Reports/LoadItemLedger",
            data: { fromDate, toDate, depot, product, storeLocation },
            dataType: "json",
            success: function (response) {
                debugger;
                var tr;
                tr = $('#grdItemLedger');
                tr.append("<thead style='background - color: cornflowerblue; font - size: 14px'><th> Sl.</th><th>Date</th><th>Vch No.</th><th>VType</th><th>Party</th><th>Batch No.</th><th>Manf.Date</th><th>Exp.Date</th><th>MRP</th><th>Opening</th><th>Inward</th><th>Outward</th><th>Balance</th><th>In-Transit</th></tr></thead>");
                $('#grdItemLedger').DataTable().destroy();
                $("#grdItemLedger tbody tr").remove();
                for (var i = 0; i < response.length; i++) {
                    tr = $('<tr/>');
                    tr.append("<td>" + srl + "</td>");//1
                    tr.append("<td>" + response[i].Date + "</td>");//2
                    tr.append("<td>" + response[i].VchNo + "</td>");//2
                    tr.append("<td>" + response[i].VType + "</td>");//3
                    tr.append("<td>" + response[i].Party + "</td>");//4
                    tr.append("<td>" + response[i].BatchNo + "</td>");//5
                    tr.append("<td>" + response[i].ManfDate + "</td>");//6
                    tr.append("<td>" + response[i].ExpDate + "</td>");//7
                    tr.append("<td>" + response[i].MRP + "</td>");//8
                    tr.append("<td>" + response[i].Opening + "</td>");//9
                    tr.append("<td>" + response[i].Inward + "</td>");//10
                    tr.append("<td>" + response[i].Outward + "</td>");//11
                    tr.append("<td>" + response[i].Balance + "</td>");//12
                    tr.append("<td>" + response[i].INTRANSIST + "</td>");//13

                    $("#grdItemLedger").append(tr);
                }
                tr.append("</tbody>");
                RowCountProcessList();
                $('#grdItemLedger').DataTable({
                    "sScrollX": '100%',
                    "sScrollXInner": "110%",
                    "scrollY": "300px",
                    "scrollCollapse": true,
                    "dom": 'Bfrtip',
                    buttons: [
                        {
                            extend: 'excelHtml5',
                            title: 'Item Ledger' + ' for ' + productName+'('+ depotName +')' +' ' + fromDate + ' to ' + toDate 
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
}

function RowCountProcessList() {
    var table = document.getElementById("grdItemLedger");
    var rowCount = document.getElementById("grdItemLedger").rows.length - 1;
    if (rowCount > 0) {
        for (var i = 1; i <= rowCount; i++) {
            table.rows[i].cells[0].innerHTML = i.toString();
        }
    }
}

function getCurrentDate() {
    today_date = new Date();
    today_Date_Str = ((today_date.getDate() < 10) ? "0" : "") + String(today_date.getDate()) + "/" + ((today_date.getMonth() < 9) ? "0" : "") + String(today_date.getMonth() + 1) + "/" + today_date.getFullYear();
    return today_Date_Str;
}

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

function LoadDepotName() {
/*for COMPANY USER*/
    
   
    var _depotid = document.getElementById("hdn_Depot").value;
    
    if (tpuid == "ADMIN") {
        BindDepot_Primary();
    }
    else if (tpuid == "D") {
        debugger;
       
        BindDeptName(_depotid);
    }
    else {
        Depot_Accounts();
    }
}

function BindDepot_Primary(){

    $.ajax({
        type: "POST",
        url: "/Reports/BindDepot_Primary",
        data: {},
        async: false,
        dataType: "json",
        success: function (response) {

            var ddldepot = $("#ddldepot");
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

function BindDeptName() {
    var _depotid = document.getElementById("hdn_Depot").value;
    $.ajax({
        type: "POST",
        url: "/Reports/BindDeptName",
        data: { depotid: _depotid},
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

function Depot_Accounts() {
   
    $.ajax({
        type: "POST",
        url: "/Reports/Depot_Accounts",
        data: { IUSEID: Iuserid},
        async: false,
        dataType: "json",
        success: function (response) {
            var ddldepot = $("#ddldepot");
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

function LoadAllProduct() {
    $.ajax({
        type: "POST",
        url: "/Reports/BindProductALIAS",
        data: {},
        async:false,
        dataType: "json",
        success: function (response) {
            //alert(JSON.stringify(response));
            var ddlnewproduct = $("#ddlnewproduct");
            debugger;
            ddlnewproduct.empty().append('<option selected="selected" value="0">Select Product</option>');
            $.each(response, function () {
                debugger;
                ddlnewproduct.val(this['NAME']);
                ddlnewproduct.append($("<option value=''></option>").val(this['ID']).html(this['NAME']));
            });
            ddlnewproduct.chosen();
            ddlnewproduct.trigger("chosen:updated");


        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function LoadStorelocation() {
    $.ajax({
        type: "POST",
        url: "/Reports/LoadStorelocation",
        data: {},
        async: false,
        dataType: "json",
        success: function (response) {
            var ddlstorelocation = $("#ddlstorelocation");
            ddlstorelocation.empty().append('<option selected="selected" value="0">Select</option>');
            $.each(response, function () {
                ddlstorelocation.val(this['STORELOCATIONNAME']);
                ddlstorelocation.val('113BD8D6-E5DC-4164-BEE7-02A16F97ABCC');
                ddlstorelocation.append($("<option value=''></option>").val(this['STORELOCATIONID']).html(this['STORELOCATIONNAME']));
            });
            ddlstorelocation.chosen();
            ddlstorelocation.trigger("chosen:updated");


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