/**/
var currentdt;
var frmdate;

var SOID;
var menuID;
$(document).ready(function () {
    var qs = getQueryStrings();
    if (qs["MENUID"] != undefined && qs["MENUID"] != "") {
        menuID = qs["MENUID"];
    }
  
    loadDistDepot();
 

    $("#ddlDepot").change(function () {
        debugger
        
        var mode = 'DI'
        var type = $('#ddlDepot').val();
        if (type == '0') {
            
            cleardata();
           
        }
        else {

            bindDistributorFromDepot(mode,type);

        }
       
    })
    
    $("#txtstardate").datepicker({
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        selectCurrent: true,
        todayBtn: "linked",
        showAnim: "slideDown",
        yearRange: "-100:+0",
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
    $("#txtstardate").val(getCurrentDate());

    $("#txtenddate").datepicker({
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        selectCurrent: true,
        todayBtn: "linked",
        showAnim: "slideDown",
        yearRange: "-100:+0",
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
    $("#txtenddate").val(getCurrentDate());

    $("#btnsavedetails").click(function () {
        $('body').append('<div style="" id="loadingDiv"><div class="loader">Loading Wait Please...</div></div>');
        var mode = 'FI'
        var type = $('#ddlDepot').val();
        var cusid = $('#ddlPartyName').val();
        var startdate = $('#txtstardate').val();
        var enddate = $('#txtenddate').val();
        if (cusid == '0') {

            cleardata();

        }
        else {

            bindSaleinvoicenoFromParty(mode, type, cusid, startdate, enddate);

        }
        setTimeout(removeLoader, 500);
        return;
    })

    $("#ddlsaleinvoiceno").change(function () {
        debugger

        var mode='SI'

        bindTsiFromParty(mode);
        

    })
    $("#btnupdatedetails").click(function () {

        try {
            var mode='I'
            var mode='I'
            var name = '';
            var tsiname = '';
            var saleinvoiceid = $('#ddlsaleinvoiceno').val();
            $('#ddlsale').val(saleinvoiceid);
            if (saleinvoiceid == "" || saleinvoiceid == "0") {
                toastr.warning('Please Select Saleinvoiceno');
                return;
            }
            var sfid = $('#ddltsiCode').val();
            if (sfid == "" || sfid == "0") {
                toastr.warning('Please Select Tsi');
                return;
            }
            var sfname = $('#ddltsiCode option:selected').text();
            name = sfname.split('(');
            tsiname = name[0];
            //alert(tsiname);
            debugger;
            setTimeout(removeLoader, 500);
            UpdateSaleinvoiceno(mode, saleinvoiceid, sfid, tsiname);
            bindautomailGrid(mode, saleinvoiceid);
        }
        catch (exception) {
            alert(exception);
        }
       
    })
    
})

function isNumberKeyWithDot(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if ((charCode < 48 || charCode > 57) && (charCode != 8) && (charCode != 46))
        return false;
    return true;
}

function removeLoader() {
    $("#loadingDiv").fadeOut(500, function () {
        // fadeOut complete. Remove the loading div
        $("#loadingDiv").remove(); //makes page more lightweight 
    });
}

function loadDistDepot(mode) {
    var mode='DE'
    $.ajax({
        type: "POST",
        url: "/distributorkyc/loadDistDepot",
        data: { Mode: mode },
        async: false,
        success: function (result) {
            debugger
            var type = $("#ddlDepot");
            var messagetext
            type.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(result, function () {
                type.val(this['DEPOTNAME']);
                type.append($("<option value=''></option>").val(this['DEPOTID']).html(this['DEPOTNAME']));
            });
            type.chosen();
            type.trigger("chosen:updated");
            

        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function bindDistributorFromDepot(mode,type) {
    debugger
    var mode='DI'
    $.ajax({
        type: "POST",
        url: "/distributorkyc/LoadDistributorFromDepot",
        data: { Mode: mode, TYPE: type, },
        async: false,
        success: function (result) {
            //alert(JSON.stringify(result));
            var distributor = $("#ddlPartyName");
            distributor.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(result, function () {
                distributor.val(this['DEPOTNAME']);
                distributor.append($("<option value=''></option>").val(this['DEPOTID']).html(this['DEPOTNAME']));
            });
            distributor.chosen();
            distributor.trigger("chosen:updated");


        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function bindSaleinvoicenoFromParty(mode, type,cusid,startdate,enddate) {
    debugger
    
    $.ajax({
        type: "POST",
        url: "/distributorkyc/loadSaleinvoicenoFromParty",
        data: { Mode: mode, type, Cusid: cusid, startdate, enddate },
        async: false,
        success: function (result) {
            //alert(JSON.stringify(result));
            var distributor = $("#ddlsaleinvoiceno");
            distributor.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(result, function () {
                distributor.val(this['SALEINVOICENO']);
                distributor.append($("<option value=''></option>").val(this['SALEINVOICEID']).html(this['SALEINVOICENO']));
            });
            distributor.chosen();
            distributor.trigger("chosen:updated");


        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function bindTsiFromParty(mode) {
    debugger

    $.ajax({
        type: "POST",
        url: "/distributorkyc/loadTsiFromParty",
        data: { Mode: mode},
        async: false,
        success: function (result) {
           // alert(JSON.stringify(result));
            var distributor = $("#ddltsiCode");
            distributor.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(result, function () {
                distributor.val(this['code']);
                distributor.append($("<option value=''></option>").val(this['userid']).html(this['code']));
            });
            distributor.chosen();
            distributor.trigger("chosen:updated");


        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function UpdateSaleinvoiceno(mode,saleinvoiceid, sfid, tsiname) {
    debugger;

    $.ajax({
        type: "POST",
        url: "/distributorkyc/loadUpdateSaleinvoiceno",
        data: { mode,saleinvoiceid, sfid, tsiname },
        async: false,
        success: function (responseMessage) {
            debugger;
            var messageid;
            var messagetext;
            $.each(responseMessage, function (key, item) {
                messagetext = item.response;
            });

            toastr.success('<b><font color=black>' + messagetext + '</font></b>');
            if (messagetext != '0') {
                
            }
            else {

                toastr.error('<b><font color=black>' + messagetext + '</font></b>');

            }
        },
        failure: function (responseMessage) {
            alert(responseMessage.responseText);
        },
        error: function (responseMessage) {
            alert(responseMessage.responseText);
        }
    });
}

function bindautomailGrid() {
    debugger

    var mode = "S";
    $('#ddlsale').val();
    /* bind ledger Report */
    var srl = 0;
    srl = srl + 1;
    $.ajax({
        type: "POST",
        url: "/distributorkyc/bindsaleinvoiceGrid",
        data: { mode, saleinvoiceid:  $('#ddlsale').val()},
        dataType: "json",
        success: function (response) {
            debugger;
            var tr;
            tr = $('#grdsaleinvoiceReport');
            tr.append("<thead style='background - color: cornflowerblue; font - size: 14px'><tr><th>SFNAME</th><th>SALEINVOICENO</th><th>SALEINVOICEDATE</th></tr></thead>");
            $('#grdsaleinvoiceReport').DataTable().destroy();
            $("#grdsaleinvoiceReport tbody tr").remove();
            for (var i = 0; i < response.length; i++) {
                tr = $('<tr/>');
                
                tr.append("<td>" + response[i].SFNAME + "</td>");//4
                tr.append("<td style='text-align: center; display: none;'>" + response[i].USERID + "</td>");//1
                tr.append("<td>" + response[i].SALEINVOICENO + "</td>");//6
                tr.append("<td>" + response[i].SALEINVOICEDATE + "</td>");//7

                $("#grdsaleinvoiceReport").append(tr);
            }
            tr.append("</tbody>");

            $('#grdsaleinvoiceReport').DataTable({
                "sScrollX": '100%',
                "sScrollXInner": "110%",
                "scrollY": "300px",
                "scrollCollapse": true,
                "dom": 'Bfrtip',
                buttons: [
                    {
                        extend: 'excelHtml5',
                        title: 'Distributor Kyc',
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

function getCurrentDate() {
    today_date = new Date();
    today_Date_Str = ((today_date.getDate() < 10) ? "0" : "") + String(today_date.getDate()) + "/" + ((today_date.getMonth() < 9) ? "0" : "") + String(today_date.getMonth() + 1) + "/" + today_date.getFullYear();
    return today_Date_Str;
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