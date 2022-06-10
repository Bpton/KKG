var currentdt;
var frmdate;
var username;
$(document).ready(function () {
    var qs = getQueryStrings();
    if (qs["MENUID"] != undefined && qs["MENUID"] != "") {
        menuID = qs["MENUID"];
    }
    if (qs["USERID"] != undefined && qs["USERID"] != "") {
        SOID = qs["USERID"];
    }
   
    debugger;
    loadUsertype();
    debugger;
    $("#ddlUsertype").change(function () {
        $('body').append('<div style="" id="loadingDiv"><div class="loader">Loading Wait Please...</div></div>');
        var typeid = $('#ddlUsertype').val();
        loadUser(typeid)
        setTimeout(removeLoader, 100);
        return;
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
        dateFormat: "yy-mm-dd",
        showOn: 'button',
        buttonText: 'Show Date',
        buttonImageOnly: true,
        buttonImage: 'http://jqueryui.com/resources/demos/datepicker/images/calendar.gif'
    });
    $(".ui-datepicker-trigger").mouseover(function () {
        $(this).css('cursor', 'pointer');
    });

    debugger;
    $("#btnupdatedetails").click(function () {
        var name = '';
        var uname = '';

        var userid = $('#ddluserName').val();
        var username = $('#ddluserName option:selected').text();
        name = username.split('(');
        uname = name[0];
        var date = $('#txtstardate').val();
        var intime = $('#txtintime').val();
        var outtime = $('#txtouttime').val();

       
        alert($("#ddluserName").val());
        if ($("#ddluserName").val() == null || $("#ddluserName").val() == '0') {
            toastr.error("Please Select User");
            cleardata();
            return;
        }
        else {
            $('body').append('<div style="" id="loadingDiv"><div class="loader">Loading Wait Please...</div></div>');
            updateAttendence(userid, uname, date, intime, outtime);
            setTimeout(removeLoader, 500);
        }
        
    })

})

function loadUsertype() {
    debugger;
    $.ajax({
        type: "POST",
        url: "/distributorkyc/loadUsertype",
        
        async: false,
        success: function (result) {
            debugger
            var type = $("#ddlUsertype");
            var messagetext
            type.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(result, function () {
                type.val(this['USERNAME']);
                type.append($("<option value=''></option>").val(this['USERID']).html(this['USERNAME']));
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

function loadUser(typeid) {
    debugger;
    $.ajax({
        type: "POST",
        url: "/distributorkyc/loadUsername",
        data: { Mode: typeid },
        async: false,
        success: function (result) {
            debugger
            var name = $("#ddluserName");
            var messagetext
            name.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(result, function () {
                name.val(this['USERNAME']);
                name.append($("<option value=''></option>").val(this['USERID']).html(this['USERNAME']));
            });
            name.chosen();
            name.trigger("chosen:updated");


        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function updateAttendence(userid, uname, date, intime, outtime) {
    debugger;

    $.ajax({
        type: "POST",
        url: "/distributorkyc/updateAttendence",
        data: { userid, uname, date, intime, outtime
 },
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

function removeLoader() {
    $("#loadingDiv").fadeOut(1000, function () {
        // fadeOut complete. Remove the loading div
        $("#loadingDiv").remove(); //makes page more lightweight 
    });
}

function cleardata() {
    $("#ddlUsertype").val('');
    $("#ddluserName").val('0');
    
    $("#txtstardate").val('');
   
   
    $("#ddluserName").chosen();
    $("#ddluserName").trigger("chosen:updated");
}