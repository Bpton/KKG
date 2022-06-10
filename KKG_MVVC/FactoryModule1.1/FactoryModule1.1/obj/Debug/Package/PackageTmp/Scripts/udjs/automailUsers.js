var currentdt;
var frmdate;
var menuID;
var SOID;
var FINYEAR;
var mode;

$(document).ready(function () {
    var qs = getQueryStrings();
    if (qs["MENUID"] != undefined && qs["MENUID"] != "") {
        menuID = qs["MENUID"];
    }
    if (qs["USERID"] != undefined && qs["USERID"] != "") {
        SOID = qs["USERID"];
    }
    if (qs["FINYEAR"] != undefined && qs["FINYEAR"] != "") {
        FINYEAR = qs["FINYEAR"];
    }
    
    var list1 = document.getElementById('firstList');

    
    list1.options[0] = new Option('--Select--', '');
    list1.options[1] = new Option('Monthly', 'Monthly');
    list1.options[2] = new Option('Daily', 'Daily');
    list1.options[3] = new Option('Module', 'Module');

    $('#txtTimer').timepicker({
        timeFormat: 'h:mm p',
        interval: 60,
        minTime: '10',
        maxTime: '6:00pm',
        defaultTime: '11',
        startTime: '10:00',
        dynamic: false,
        dropdown: true,
        scrollbar: true
    });

    loadDisttype();
    bindautomailGrid();

    $("#ddlDIStype").change(function () {
        var type = $("#ddlDIStype").val();
        if (type == '0') {

            clearAutomaildata();

        }
        else {

            bindDistributorFromType(type);

        }
       
    })
    $("#ddlmoduletype").change(function () {
        var moduleid = $("#ddlmoduletype").val();
        Bindpageurl(moduleid);
    })
  
    loadServiceprovider();
    loadservicemodule();

    $("#ddlserviceprovider").change(function () {
       
    })

   
    $("#btnsavedetails").click(function () {
        debugger   
        insertAutomailSchedulerDetails();

    })

    $("#btnSearch").click(function () {
        setTimeout(removeLoader, 500);
        bindGrid();

    })

    $('#timefor').css("display", "none");
    $('#txttime').css("display", "none");

    $('#divsc').css("display", "none");
    $('#txtsecondlist').css("display", "none");
   // alert
    $("body").on("click", "#grdAutomialGrid .gvEdit", function () {
     
        row = $(this).closest("tr");
        TemplateID = row.find('td:eq(0)').html();
        $("#templateid").val(TemplateID);

        $("#ddlserviceprovider").chosen('destroy');
        $("#ddlserviceprovider").chosen({ width: '150px' });


        editTemplate(TemplateID);
    });


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

function loadServiceprovider(mode) {
    debugger;
    var mode = "U";
    $.ajax({
        type: "POST",
        url: "/Automail/loadserviceprovider",
        data: { mode: mode },
        async: false,
        success: function (result) {
            debugger
            var type = $("#ddlserviceprovider");

            type.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(result, function () {
                type.val(this['ServiceProviderName']);
                type.append($("<option value=''></option>").val(this['ServiceproviderID']).html(this['ServiceProviderName']));
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

function loadservicemodule(mode) {
    debugger;
    var mode = "M";
    $.ajax({
        type: "POST",
        url: "/Automail/loadservicemodule",
        data: { mode: mode },
        async: false,
        success: function (result) {
            debugger
            var module = $("#ddlmoduletype");

            module.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(result, function () {
                module.val(this['PageName']);
                module.append($("<option value=''></option>").val(this['ID']).html(this['PageName']));
            });
            module.chosen();
            module.trigger("chosen:updated");
            //search: true,
           // selectAll: true
                //allSelectedText: 'All Selected'

        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function loadDisttype() {
    $.ajax({
        type: "POST",
        url: "/Automail/loadcustomertype",
        //data: { USERID: SOID },
        async: false,
        success: function (result) {
            debugger
            var type = $("#ddlDIStype");

            type.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(result, function () {
                type.val(this['UTNAME']);
                type.append($("<option value=''></option>").val(this['UTID']).html(this['UTNAME']));
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

function bindDistributorFromType(type) {
    debugger
    $.ajax({
        type: "POST",
        url: "/Automail/loadDistributorFromCustomerType",
        data: { TYPE: type },
        async: false,
        success: function (result) {
            //alert(JSON.stringify(result));
            var distributor = $("#ddldistributor");
            //distributor.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(result, function () {
                distributor.val(this['CUSTOMERNAME']);
                distributor.append($("<option value=''></option>").val(this['USERID']).html(this['CUSTOMERNAME']));
            });
            $('#ddldistributor').multiselect({
                columns: 1, placeholder: 'Select Company',
                includeSelectAllOption: true,
                //search: true,
                // selectAll: true
                //allSelectedText: 'All Selected'
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

function Bindpageurl(moduleid) {
    debugger
    $.ajax({
        type: "POST",
        url: "/Automail/Getpageurl",
        data: { moduleid: moduleid },
        async: false,
        success: function (result) {
            //alert(JSON.stringify(result));
           
            $.each(result, function () {
               // alert(JSON.stringify(result));
                $("#hdnpageurl").val(this["PageURL"]);
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

function insertAutomailSchedulerDetails(mode) {
    debugger

    var mode; 
    var companyDetails = $("#ddldistributor option:selected");
    var companyDetails1 = "";
    companyDetails.each(function () {
        companyDetails1 += $(this).val() + ',';
    });
    var res = companyDetails1.substring(0, companyDetails1.length - 1);
    //alert(res);
    var detail = {};
    detail.ServiceproviderID = $("#ddlserviceprovider").val();
    detail.ServiceProviderName = $("#ddlserviceprovider").find('option:selected').text();
    detail.ID = $("#ddlmoduletype").val();
    detail.PageName = $("#ddlmoduletype").find('option:selected').text();
    detail.PageURL = $("#ddlmoduletype").val();
    detail.UTID = $("#ddlDIStype").val();
    detail.UTNAME = $("#ddlDIStype").find('option:selected').text();
    detail.USERID =res;
    detail.CUSTOMERNAME = $("#ddldistributor").find('option:selected').text();
    detail.MessageContent = $("#txtmessegecontent").val();
    detail.SchedulerType = $("#firstList").val();
    detail.Monthly = $("#secondList").val();
    detail.Daily = $("#txttime").val();


   
    if ($("#templateid").val() == '' || $("#templateid").val() == undefined) {
        mode = 'I';
    }
    else {
        mode = 'U';
    }

    detail.mode = mode;
  
    $.ajax({
        url: "/Automail/insertAutomailSchedulerDetails",
        data: '{detail:' + JSON.stringify(detail) + '}',
       
        type: "POST",
        async: false,
        contentType: "application/json",
        success: function (responseMessage) {

            var messageid;
            var messagetext;
            $.each(responseMessage, function (key, item) {
                //  messageid = item.MessageID;
                messagetext = item.response;

            });

            toastr.success('<b><font color=black>' + messagetext + '</font></b>');
            if (messagetext != '0') {
                clearAutomaildata();
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

function getSchedulerItem() {

    var list1 = document.getElementById('firstList');
    
    var list1SelectedValue = list1.options[list1.selectedIndex].value;

    if (list1SelectedValue == 'Monthly') {

        $("#ddlspan").change(function () {
            spanChangeFunction();
        })

        $(".date-picker").datepicker({
            dateFormat: 'dd-mm-yy'
        });
        $("#secondList").datepicker({
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
        $('#divsc').css("display", "");
        $('#timefor').css("display", "none");
        $('#txttime').css("display", "none");



    }
    else if (list1SelectedValue == 'Daily') {
        $("#secondList").val();
        $('#timefor').css("display", "");
        $('#txttime').css("display", "");
        $('#divsc').css("display", "none");
        

    }
    else if (list1SelectedValue == 'Module') {
        $('#divsc').css("display", "none");
        $('#timefor').css("display", "none");
        $('#txttime').css("display", "none");

    }
}

function getCurrentDate() {
    today_date = new Date();
    today_Date_Str = ((today_date.getDate() < 10) ? "0" : "") + String(today_date.getDate()) + "/" + ((today_date.getMonth() < 9) ? "0" : "") + String(today_date.getMonth() + 1) + "/" + today_date.getFullYear();
    return today_Date_Str;
}

function mytime() {
    var d = new Date();
    ap = "am";
    h = d.getHours();
    m = d.getMinutes();
    s = d.getSeconds();
    if (h > 11) { ap = "pm"; }
    if (h > 12) { h = h - 12; }
    if (h == 0) { h = 12; }
    if (m < 10) { m = "0" + m; }
    if (s < 10) { s = "0" + s; }
    document.getElementById('hello').innerHTML = h + ":" + m + ":" + s + " " + ap;
    t = setTimeout('mytime()', 500);
}

function clearAutomaildata() {
    $("#ddlserviceprovider").val('');
    $("#ddlmoduletype").val('');
    $("#ddlDIStype").val('');
    $("#ddldistributor").empty();
    $("#txtmessegecontent").val('');
    $("#firstList").empty();
    $("#secondList").empty();
    $("#txttime").empty();

    $("#ddlserviceprovider").chosen();
    $("#ddlserviceprovider").trigger("chosen:updated");
    $("#ddlmoduletype").chosen();
    $("#ddlmoduletype").trigger("chosen:updated");
    $("#ddlDIStype").chosen();
    $("#ddlDIStype").trigger("chosen:updated");
    //$("#ddldistributor").chosen();
    //$("#ddldistributor").trigger("chosen:updated");
    $("#firstList").chosen();
    $("#firstList").trigger("chosen:updated");
}

function bindautomailGrid() {
    debugger

    var mode = "S";

    /* bind ledger Report */
    var srl = 0;
    srl = srl + 1;
    $.ajax({
        type: "POST",
        url: "/Automail/bindautomailGrid",
        data: { mode },
        dataType: "json",
        success: function (response) {
            debugger;
            var tr;
            tr = $('#grdAutomialGrid');
            tr.append("<thead style='background - color: cornflowerblue; font - size: 14px'><tr><th>MessageContent</th><th>CUSTOMERNAME</th><th>SenderID</th><th>MOBILE</th><th>Edit</th></tr></thead>");
            $('#grdAutomialGrid').DataTable().destroy();
            $("#grdAutomialGrid tbody tr").remove();
            for (var i = 0; i < response.length; i++) {
                tr = $('<tr/>');
                tr.append("<td style='text-align: center; display: none;'>" + response[i].TemplateID + "</td>");//0
                tr.append("<td style='text-align: center; display: none;'>" + response[i].ID + "</td>");//1
               
                tr.append("<td>" + response[i].MessageContent + "</td>");//4
                tr.append("<td style='text-align: center; display: none;'>" + response[i].USERID + "</td>");//1
                tr.append("<td>" + response[i].CUSTOMERNAME + "</td>");//6
                tr.append("<td>" + response[i].SenderID + "</td>");//7
                
                tr.append("<td>" + response[i].MOBILE + "</td>");//8
                tr.append("<td><input type='image' class='gvEdit' id='btnEdit' src='../images/Pencil-icon.png ' width='30' height='30' /></td> ");

                $("#grdAutomialGrid").append(tr);
            }
            tr.append("</tbody>");

            $('#grdAutomialGrid').DataTable({
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

function editTemplate(TemplateID, mode) {
    var id = TemplateID;
    $.ajax({
        type: "POST",
        url: "/Automail/editTemplate",
        data: { TemplateId: id},
        async: false,
        success: function (response) {
            debugger
            //alert(JSON.stringify(response));
            $.each(response, function () {
              
                loadServiceprovider();
                $("#ddlserviceprovider").val(this["ServiceproviderID"]);
                $("#ddlserviceprovider").chosen({
                    search_contains: true
                });

                $("#ddlserviceprovider").trigger("chosen:updated");

                loadservicemodule();
                $("#ddlmoduletype").val(this["ID"]);
                $("#ddlmoduletype").chosen({
                    search_contains: true
                });

                $("#ddlmoduletype").trigger("chosen:updated");

                loadDisttype();
                $("#ddlDIStype").val(this["UTID"]);
                $("#ddlDIStype").chosen({
                    search_contains: true
                });

                $("#ddlDIStype").trigger("chosen:updated");

                bindDistributorFromType();
                $("#ddldistributor").val(this["USERID"]);
                var valArr = this["USERID"];
                

                $("#ddldistributor option[value='" + valArr + "']").attr("selected", 1);
                $("#ddldistributor").multiselect("refresh");

                $("#txtmessegecontent").val(this["MessageContent"]);

                $("#firstList").val(this["SchedulerType"]);
                $("#firstList").chosen({
                    search_contains: true
                });

                $("#firstList").trigger("chosen:updated");
             
              
            })


        }
    })

}

//$(function () {
//    $('#txtTime').datetimepicker({
//        format: 'LT'
//    });
//});