$(document).ready(function () {
    var qs = getQueryStrings();
    if (qs["MENUID"] != undefined && qs["MENUID"] != "") {
        menuID = qs["MENUID"];
    }
    if (qs["USERID"] != undefined && qs["USERID"] != "") {
        SOID = qs["USERID"];
    }
    
    $('#pnlDisplaysms').css("display", "none");
    $('#pnlDisplayemail').css("display", "none");
    var list1 = document.getElementById('messegetype');


    list1.options[0] = new Option('--Select--', 'Select');
    list1.options[1] = new Option('SMS', 'SMS');
    list1.options[2] = new Option('EMAIL', 'EMAIL');


   
    $("#btnsavedetails").click(function () {
       
        insertAutomailDetails();
        
        
        
    })
    $("#messegetype").change(function () {
        alert('a');

        getmessegeItem();

    })

    $("#txttime").val(mytime());
    
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


function insertAutomailDetails() {



    //alert($("#txtconpersonT").val());
    //if ($("#txtconpersonT").val() == "") {
    //    $("#txtconpersonT").val("NA");
    //}

    //var companyDetails = $("#ddlcompaNydetailS option:selected");
    //var companyDetails1 = "";
    //companyDetails.each(function () {
    //    companyDetails1 += $(this).val() + ',';
    //});
    //var res = companyDetails1.substring(0, companyDetails1.length - 1);
    //alert(res);
    var detail = {};
    detail.Messagetype = $("#messegetype").val();
    detail.serviceproviderName = $("#txtserviceproviderName").val();
    detail.senderId = $("#txtsenderid").val();
    detail.smsUserid = $("#txtsmsuserid").val();
    detail.Smspwd = $("#txtsmspwd").val();
    detail.smtp = $("#txtsmtpaddress").val();
    detail.emailFrm = $("#txtfrmemail").val();
    detail.port = $('#txtport').val();

    detail.emailUserid = $("#txtemilsender").val();
    detail.Emailpwd = $("#txtmailpassword").val();
    

    $.ajax({
        url: "/Automail/insertAutomailDetails",
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
                //close();
                //LoadSale();
                //ReleaseSession();
                
                //$('#hdn_saleorderid').val('');

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

    //function removeLoader() {
    //    $("#loadingDiv").fadeOut(500, function () {
    //        // fadeOut complete. Remove the loading div
    //        $("#loadingDiv").remove(); //makes page more lightweight 
    //    });
    //}

    
}
//debugger
function bindAutomailGrid() {
    debugger



    /*  */
    var srl = 0;
    srl = srl + 1;
    $.ajax({
        type: "POST",
        url: "/Automail/bindAutomaildetails",

        dataType: "json",
        success: function (response) {
            debugger;
            var tr;
            tr = $('#grdAutomailGrid');
            tr.append("<thead style='background - color: cornflowerblue; font - size: 14px'><th> ServiceproviderID</th><th>Messagetype</th><th>ServiceProviderName</th><th>SenderID</th><th>SmsUserid</th><th>Smspwd</th><th>Smtp</th><th>Emailfrm</th><th>Port</th><th>Emailuserid</th><th>Emailpwd</th><th>Isactive</th></tr></thead>");
            $('#grdAutomailGrid').DataTable().destroy();
            $("#grdAutomailGrid tbody tr").remove();
            for (var i = 0; i < response.length; i++) {
                tr = $('<tr/>');
                //tr.append("<td style='text-align: center; display: none;'>" + response[i].DISTRIBUTORID + "</td>");//0
                //tr.append("<td style='text-align: center; display: none;'>" + response[i].DIST_TYPEID + "</td>");//1
                tr.append("<td>" + response[i].ServiceproviderID + "</td>");//1
                tr.append("<td>" + response[i].Messagetype + "</td>");//2
                tr.append("<td>" + response[i].ServiceProviderName + "</td>");//3
                tr.append("<td>" + response[i].SenderID + "</td>");//4
                tr.append("<td>" + response[i].SmsUserid + "</td>");//5
                tr.append("<td>" + response[i].Smspwd + "</td>");//6
                tr.append("<td>" + response[i].Smtp + "</td>");//7
                tr.append("<td>" + response[i].Emailfrm + "</td>");//8
                tr.append("<td>" + response[i].Port + "</td>");//9
                tr.append("<td>" + response[i].Emailuserid + "</td>");//10
                tr.append("<td>" + response[i].Emailpwd + "</td>");//11
                tr.append("<td>" + response[i].Isactive + "</td>");//12


                $("#grdAutomailGrid").append(tr);
            }
            tr.append("</tbody>");

            $('#grdAutomailGrid').DataTable({
                "sScrollX": '100%',
                "sScrollXInner": "110%",
                "scrollY": "300px",
                "scrollCollapse": true,
                "dom": 'Bfrtip',
                buttons: [
                    {
                        extend: 'excelHtml5',
                        title: 'Automail details',
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

function bindAutomailGrid1() {
    debugger



    /*  */
    var srl = 0;
    srl = srl + 1;
    $.ajax({
        type: "POST",
        url: "/Automail/bindAutomaildetails",

        dataType: "json",
        success: function (response) {
            debugger;
            var tr;
            tr = $('#grdAutomailGridemail');
            tr.append("<thead style='background - color: cornflowerblue; font - size: 14px'><th> ServiceproviderID</th><th>Messagetype</th><th>ServiceProviderName</th><th>SenderID</th><th>SmsUserid</th><th>Smspwd</th><th>Smtp</th><th>Emailfrm</th><th>Port</th><th>Emailuserid</th><th>Emailpwd</th><th>Isactive</th></tr></thead>");
            $('#grdAutomailGridemail').DataTable().destroy();
            $("#grdAutomailGridemail tbody tr").remove();
            for (var i = 0; i < response.length; i++) {
                tr = $('<tr/>');
                //tr.append("<td style='text-align: center; display: none;'>" + response[i].DISTRIBUTORID + "</td>");//0
                //tr.append("<td style='text-align: center; display: none;'>" + response[i].DIST_TYPEID + "</td>");//1
                tr.append("<td>" + response[i].ServiceproviderID + "</td>");//1
                tr.append("<td>" + response[i].Messagetype + "</td>");//2
                tr.append("<td>" + response[i].ServiceProviderName + "</td>");//3
                tr.append("<td>" + response[i].SenderID + "</td>");//4
                tr.append("<td>" + response[i].SmsUserid + "</td>");//5
                tr.append("<td>" + response[i].Smspwd + "</td>");//6
                tr.append("<td>" + response[i].Smtp + "</td>");//7
                tr.append("<td>" + response[i].Emailfrm + "</td>");//8
                tr.append("<td>" + response[i].Port + "</td>");//9
                tr.append("<td>" + response[i].Emailuserid + "</td>");//10
                tr.append("<td>" + response[i].Emailpwd + "</td>");//11
                tr.append("<td>" + response[i].Isactive + "</td>");//12


                $("#grdAutomailGridemail").append(tr);
            }
            tr.append("</tbody>");

            $('#grdAutomailGridemail').DataTable({
                "sScrollX": '100%',
                "sScrollXInner": "110%",
                "scrollY": "300px",
                "scrollCollapse": true,
                "dom": 'Bfrtip',
                buttons: [
                    {
                        extend: 'excelHtml5',
                        title: 'Automail details',
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

function getmessegeItem() {

    var list1 = $('#messegetype').val();

   // var list1SelectedValue = list1.options[list1.selectedIndex].value;
    /*debugger*/;
    if (list1 == 'SMS') {
        bindAutomailGrid();
        $('#pnlDisplaysms').css("display", "");
        $('#pnlDisplayemail').css("display", "none");

    }
    else if (list1 == 'EMAIL') {

        bindAutomailGrid1();
        $('#pnlDisplayemail').css("display", "");
        $('#pnlDisplaysms').css("display", "none");
    }
    else if (list1 == 'Select') {
        $('#pnlDisplayemail').css("display", "none");
        $('#pnlDisplaysms').css("display", "none");
    }
}

function isNumberKeyWithDot(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if ((charCode < 48 || charCode > 57) && (charCode != 8) && (charCode != 46))
        return false;
    return true;
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