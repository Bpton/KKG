var currentdt;
var frmdate;

var SOID;
var menuID;
var MODE;
$(document).ready(function () {
    var qs = getQueryStrings();
    if (qs["MENUID"] != undefined && qs["MENUID"] != "") {
        menuID = qs["MENUID"];
    }
    if (qs["USERID"] != undefined && qs["USERID"] != "") {
        SOID = qs["USERID"];
    }
    debugger;
    /*----DOCUMENT CHECKER---*/
    $('#txtchkupload').on('change', function () {
        debugger
        const size =
            (this.files[0].size / 1024 / 1024).toFixed(2);

        if (size > 3 || size <0.1 ) {
            alert("File size should not be greater then 2 MB");
        } else {
            $("#output").html('<b>' +
                'This file size is: ' + size + " MB" + '</b>');
        }
    });
    $("#txttodate").datepicker({
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        selectCurrent: true,
        todayBtn: "linked",
        showAnim: "slideDown",
        yearRange: "-1:+0",
        maxDate: new Date(currentdt),
        minDate: new Date(currentdt),
        // minDate: new Date(frmyr, 04 - 1, 01),

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

    $("#txtfromdate").datepicker({
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        selectCurrent: true,
        todayBtn: "linked",
        showAnim: "slideDown",
        yearRange: "-1:+0",
        maxDate: new Date(currentdt),
        minDate: new Date(currentdt),
        // minDate: new Date(frmyr, 04 - 1, 01),

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

    $('#pnlDisplaydistributor').css("display", "none");
    loadUser();


    $("#ddlotHercompaNydetailS").keydown(function (event) {
        if (event.ctrlKey == true && (event.which == '118' || event.which == '86')) {
            //alert(' not. PASTE!');
            event.preventDefault();
        }
    });

    $("#ddlusername").change(function () {
        debugger;
            bindReportingFromType();
    })
   

    $("#txtdate").val(getCurrentDate())

    $("#Btnadd").click(function () {
        // $('body').append('<div style="" id="loadingDiv"><div class="loader">Loading Wait Please...</div></div>');
        $('#pnlDisplay').hide();

        $('#pnlhome').hide();
        
        $("#pnlDisplaydistributor").show();
        $("#pnlsave").show();
    })
    $("#btnCanceldetails").click(function () {
        // $('body').append('<div style="" id="loadingDiv"><div class="loader">Loading Wait Please...</div></div>');
        $('#pnlDisplaydistributor').css("display", "block");
        $('#pnlDisplaydistributor').css("display", "none");
        $("#pnlDisplay").show();
    })
    $("#btnhome").click(function () {
        // $('body').append('<div style="" id="loadingDiv"><div class="loader">Loading Wait Please...</div></div>');
        $('#pnlDisplaydistributor').css("display", "block");
        $('#pnlDisplaydistributor').css("display", "none");
        $("#pnlDisplay").show();
    })


    $("#btnsavedetails").click(function () {
       // $('body').append('<div style="" id="loadingDiv"><div class="loader">Loading Wait Please...</div></div>');
       
        if ($("#ddlusername").val() == "" || $("#ddlusername").val() == '0') {

            toastr.error("Please Select User");
            return;
        }
        $('#pnlsave').css("display", "block");
        $('#pnlsave').css("display", "none");
        $("#pnlhome").show();
        debugger;
        InsertUserDetails();
        //setTimeout(removeLoader, 500);
    })

    $("#btnvouchersearch").click(function () {
        $('body').append('<div style="" id="loadingDiv"><div class="loader">Loading Wait Please...</div></div>');
        setTimeout(removeLoader, 500);
        bindUserGrid();
        
    })

    /*----upload file---*/
    $('#btnnupload').click(function () {
       

        if ($("#ddldistributor").val() == "" || $("#ddldistributor").val() == null) {
            toastr.warning("Please Select Distributor for Upload file!")
            return;
        }
        else if ($("#txtchkupload").val() == "") {
            toastr.warning("Please Select file At least!")
            return;
        }
        else {
            // Checking whether FormData is available in browser  
            if (window.FormData !== undefined) {
                
                var userid = $("#ddlusername").val();
                var fileUpload = $("#txtchkupload").get(0);
                var files = fileUpload.files;
                // Create FormData object  
                var fileData = new FormData();
                fileData.append("userid", userid);


                // Looping over all files and add it to FormData object  
                for (var i = 0; i < files.length; i++) {
                    fileData.append(files[i].name, files[i]);
                }

                $.ajax({
                    url: "/distributorkyc/UploadFileReportInfococ",
                    type: "POST",
                    contentType: false, // Not to set any content header  
                    processData: false, // Not to process data  
                    data: fileData,
                    success: function (response) {
                        
                        toastr.info('File uploaded successfully');
                        
                    },
                    error: function (err) {
                        toastr.error(err.statusText);
                    }
                });
            }
            else {
                toastr.error("FormData is not supported.");
            }
        }


    });

})

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

function loadUser() {
    var MODE = 'N'
    //alert (SOID)
    $.ajax({
        type: "POST",
        url: "/distributorkyc/bindUser",
        data: { MODE, USERID: SOID },
        async: false,
        success: function (result) {
            debugger
            var userid = $("#ddlusername");
            
            userid.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(result, function () {
                userid.val(this['USERNAME']);
                userid.append($("<option value=''></option>").val(this['USERID']).html(this['USERNAME']));
            });
            userid.chosen();
            userid.trigger("chosen:updated");
            $("#ddlusername").chosen('destroy');
            $("#ddlusername").chosen({ width: '200px' });

        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function bindReportingFromType() {
    debugger;
    var MODE = 'R'
    var USERID = $("#ddlusername").val();
    $.ajax({
        type: "POST",
        url: "/distributorkyc/bindUser",
        data: { MODE, USERID },
        async: false,
        success: function (result) {
            debugger
            var type = $("#ddldistributor");
          
            //type.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(result, function () {
                type.val(this['REPORTINGTONAME']);
                type.append($("<option value=''></option>").val(this['REPORTINGTOID']).html(this['REPORTINGTONAME']));
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

function InsertUserDetails() {
    //debugger;

    try {


        if ($("#textremarks").val() == "") {
            $("#textremarks").val("NA");
        }
       

        
        var detail = {};
        detail.MODE = 'UC',
        detail.USERID = $("#ddlusername").val();
        detail.REMARKS = $("#textremarks").val();
        
        $.ajax({
            url: "/distributorkyc/InsertUserDetails",
            data: '{detail:' + JSON.stringify(detail) + '}',
            type: "POST",
            async: false,
            contentType: "application/json",
            success: function (responseMessage) {
                debugger
                var messageid;
                var messagetext;
                $.each(responseMessage, function (key, item) {
                    messagetext = item.response;
                });

                toastr.success('<b><font color=black>' + messagetext + '</font></b>');
                if (messagetext != '0') {
                    cleardata();
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
    catch (ex) {
        alert(ex);
    }
}

function bindUserGrid() {
    debugger

    var MODE = 'SC';
    //detail.USERID = $("#ddlusername").val();
    var fromDate = $('#txtfromdate').val();
    var toDate = $('#txttodate').val();

    /* bind ledger Report */
    var srl = 0;
    srl = srl + 1;
    $.ajax({
        type: "POST",
        url: "/distributorkyc/bindUserDocReport",
        data: { MODE, USERID: SOID, fromDate, toDate },
        dataType: "json",
        success: function (response) {
            //debugger;
            var tr;
            tr = $('#dispatchGridFG');
            tr.append("<thead style='background - color: cornflowerblue; font - size: 14px'><th>USERNAME</th><th>PARTY_TYPE</th><th>REPORTINGTONAME</th><th>REMARKS</th><th>Date</th><th>FILE_PATH</th></tr></thead>");
            $('#dispatchGridFG').DataTable().destroy();
            $("#dispatchGridFG tbody tr").remove();
            for (var i = 0; i < response.length; i++) {
                tr = $('<tr/>');
                tr.append("<td style='display:none'>" + response[i].USERID + "</td>");//2
                tr.append("<td>" + response[i].USERNAME + "</td>");//2
                tr.append("<td>" + response[i].PARTY_TYPE + "</td>");//2
                tr.append("<td>" + response[i].REPORTINGTONAME + "</td>");//2
                tr.append("<td>" + response[i].REMARKS + "</td>");//2
                tr.append("<td>" + response[i].LDTOM + "</td>");

                //tr.append("<td><a href='/distributorkyc/Downloads?filename=chirag_ent_stock.xls' </a> <img src='../Images/download.png' width='20' height ='20' title='Download'</td>");
                tr.append("<td><a href='" + response[i].FILE_PATH + "' target='_blank'  </a> <img src='../Images/download.png' width='20' height ='20' title='Download'</td>");

                //tr.append("<td> <input type='image' class='gvdwndelete'  id='btndelete' onclick='myFunctionfordelete1()'<img src='../Images/cross.png' width='20' height ='20' title='Delete'/></td>");

                
                $("#dispatchGridFG").append(tr);
            }
            tr.append("</tbody>");

            $('#dispatchGridFG').DataTable({
                "sScrollX": '100%',
                "sScrollXInner": "110%",
                "scrollY": "300px",
                "scrollCollapse": true,
                "dom": 'Bfrtip',
                buttons: [
                    {
                        extend: 'excelHtml5',
                        title: 'UserDoc',
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

function cleardata() {
    
    $("#ddldistributor").val('0');
    $("#ddlusername").val('0');
    $("#textremarks").val('');
    $("#txtchkupload").val('');

    $("#ddlusername").chosen();
    $("#ddlusername").trigger("chosen:updated");
    $("#ddldistributor").chosen();
    $("#ddldistributor").trigger("chosen:updated");

}

function removeLoader() {
    $("#loadingDiv").fadeOut(500, function () {
        // fadeOut complete. Remove the loading div
        $("#loadingDiv").remove(); //makes page more lightweight 
    });
}
