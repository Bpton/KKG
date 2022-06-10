
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
    list1.options[1] = new Option('Attachment', 'Attached');
    list1.options[2] = new Option('Mail Body', 'Mailbody');
    



    loadReportname();
    loadserviceSubscriberUsers();

    $("#ddlReportname").change(function () {
        var repoid = $("#ddlReportname").val();
        
       

            loadSubscriberEmail(mode,repoid);


    })
    
    $("#btnsavedetails").click(function () {
        debugger
        UpdateautomailEmaildetails();
    })

    $("#ddlUsers").change(function () {
        var reportid = $("#ddlUsers").val();
        Bindusermail(reportid);
    })

    $("#btnaddUser").click("click", function () {
        debugger;
        $("#window").dialog({
            title: "Send",
            width: 430,
            height: 250,
            modal: true,
            buttons: {
                Close: function () {
                    $(this).dialog('close');
                }
            }
        });
        $("#newwindow").dialog({
            title: "textfor",
            width: 40,
            height: 20,
            modal: true,
            buttons: {
                Close: function () {
                    $(this).dialog('close');
                }
            }
        });
        return false;
    });

    $('#submit-button1').click(function () {
        alert('hi');
        
        var modelname = $("#txtReportname").val();
        var spname = $("#txtStoreproc").val();
        var sp_parameter = $("#txtStoreproc").val();
        if (spname == "0") {
            clearAutomaildata();
        }
        else {
            automailInsertEmaildetails(modelname, spname, sp_parameter);
        }
    })

    
        //$('#page-button').click(function () {
        //    $('#modal-text').val($('#textbox').val());
        //    $('#textbox').val("");
        //    $('#modal-text').val($('#textbox').val());
        //    $('#textbox').val("");
        //});
   
})

function Bindusermail(reportid) {
    debugger
    $.ajax({
        type: "POST",
        url: "/Automail/getuserMail",
        data: { reportid: reportid },
        async: false,
        success: function (result) {
            //alert(JSON.stringify(result));

            $.each(result, function () {
                alert(JSON.stringify(result));
                $("#hdnpageurl").val(this["email"]);
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

function loadReportname(mode) {
    debugger;
    var mode = "N";
    $.ajax({
        type: "POST",
        url: "/Automail/loadReportmodulename",
        data: { mode: mode },
        async: false,
        success: function (result) {
            debugger
            var module = $("#ddlReportname");

            module.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(result, function () {
                module.val(this['ReportName']);
                module.append($("<option value=''></option>").val(this['ReportID']).html(this['ReportName']));
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

function loadSubscriberEmail(mode, repoid) {
    debugger;
    var mode = "S";
    $.ajax({
        type: "POST",
        url: "/Automail/loadserviceSubscriberEmail",
        data: { mode: mode, repoid: repoid },
        async: false,
        success: function (result) {
            debugger
            var module = $("#ddlSubscriberEmail");

            //module.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(result, function () {
                module.val(this['ReportSubscriberEmail']);
                module.append($("<option value=''></option>").val(this['ReportID']).html(this['ReportSubscriberEmail']));
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

function loadserviceSubscriberUsers(mode) {
    debugger;
    var mode = "U";
    $.ajax({
        type: "POST",
        url: "/Automail/loadserviceSubscriberUsers",
        data: { mode: mode },
        async: false,
        success: function (result) {
            debugger
            var module = $("#ddlUsers");

            //module.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(result, function () {
                module.val(this['UserName']);
                module.append($("<option value=''></option>").val(this['UserId']).html(this['UserName']));
            });
            $('#ddlUsers').multiselect({
                columns: 1, placeholder: 'Select User',
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

function clearAutomaildata() {
    $("#ddlReportname").val('');
    $("#ddlSubscriberEmail").val('');
    $("#firstList").empty();
    $("#ddlUsers").empty();
    $("#txtReportname").empty();
    $("#txtReportcode").empty();

    $("#ddlReportname").chosen();
    $("#ddlReportname").trigger("chosen:updated");
    $("#ddlSubscriberEmail").chosen();
    $("#ddlSubscriberEmail").trigger("chosen:updated");
    $("#firstList").chosen();
    $("#firstList").trigger("chosen:updated");
    ////$("#ddldistributor").chosen();
    ////$("#ddldistributor").trigger("chosen:updated");
    //$("#firstList").chosen();
    //$("#firstList").trigger("chosen:updated");
}

function UpdateautomailEmaildetails() {
    debugger
    var mode;
    var detail = {};
    mode = 'UP';
    
    detail.ReportID = $("#ddlReportname").val();

    var alluserid = $("#ddlUsers option:selected");
    var alluserid1 = "";
    alluserid.each(function () {
        alluserid1 += $(this).val() + ',';
    });
    var res = alluserid1.substring(0, alluserid1.length - 1);
    alert(res);
    
    detail.UserId = res;
    detail.SendType = $("#firstList").val();
    



   
    detail.mode = mode;

    $.ajax({
        url: "/Automail/UpdateAutomailemailDetails",
        data: '{detail:' + JSON.stringify(detail) + '}',

        type: "POST",
        async: false,
        contentType: "application/json",
        success: function (responseMessage) {

            
            var messagetext;
            $.each(responseMessage, function (item) {
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

function getemailItem() {

    var list1 = document.getElementById('firstList');

    var list1SelectedValue = list1.options[list1.selectedIndex].value;

    if (list1SelectedValue == 'Attached') {

        $("#secondList").val();


    }
    else if (list1SelectedValue == 'Mailbody') {
        $("#secondList").val();



    }

}

function automailInsertEmaildetails() {
    debugger
  
    var detail = {};
   
    detail.ReportName = $("#txtReportname").val();
    detail.reportCode = $("#txtReportcode").val();
    detail.StoreProcedure = $("#txtStoreproc").val();
    detail.Sp_Parameters = $("#txtParameters").val();

    $.ajax({
        url: "/Automail/automailInsertEmaildetails",
        data: '{detail:' + JSON.stringify(detail) + '}',

        type: "POST",
        async: false,
        contentType: "application/json",
        success: function (responseMessage) {


            var messagetext;
            $.each(responseMessage, function (item) {
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

function BindautomailToexportexcel() {
    debugger

   

    var=
ReportName = $("#txtReportname").val();
    detail.reportCode = $("#txtReportcode").val();
    detail.StoreProcedure = $("#txtStoreproc").val();
    detail.Sp_Parameters = $("#txtParameters").val();

    $.ajax({
        url: "/Automail/BindautomailToexportexcel",
        data: '{detail:' + JSON.stringify(detail) + '}',

        type: "POST",
        async: false,
        contentType: "application/json",
        success: function (responseMessage) {


            var messagetext;
            $.each(responseMessage, function (item) {
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

function BindautomailToexportexcel(modelname, spname, sp_parameter) {
    // debugger
    $.ajax({
        type: "POST",
        url: "/distributorkyc/LoadDistributorFromType",
        data: { Modelname: modelname, Spname: spname, Sp_Parameters: sp_parameter },
        async: false,
        success: function (result) {
            //alert(JSON.stringify(result));
            var distributor = $("#ddldistributor");
            distributor.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(result, function () {
                distributor.val(this['DISTRIBUTORNAME']);
                distributor.append($("<option value=''></option>").val(this['DISTRIBUTORID']).html(this['DISTRIBUTORNAME']));
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