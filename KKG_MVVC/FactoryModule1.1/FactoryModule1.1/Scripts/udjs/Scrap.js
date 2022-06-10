//#region Developer Info
/*
* For Scrap.cshtml Only
* Developer Name : Rajeev Kumar
* Start Date     : 06/10/2020
* END Date       :  
*/
//#endregion
var CHECKER;
var FinYear;
var UserID;
//var DepotID = '14857CFC-2450-4D52-B93A-486D9507A1BE';

$(document).ready(function () {
    var qs = getQueryStrings();
    if (qs["CHECKER"] != undefined && qs["CHECKER"] != "") {
        CHECKER = qs["CHECKER"].trim();
    }
    if (qs["FINYEAR"].toString() != undefined && qs["FINYEAR"].toString() != "") {
        FinYear = qs["FINYEAR"].toString();
    }
    if (qs["USERID"].toString() != undefined && qs["USERID"].toString() != "") {
        UserID = qs["USERID"].toString();
    }
    //if (qs["DEPOTID"].toString() != undefined && qs["DEPOTID"].toString() != "") {
    //    DepotID = "14857CFC-2450-4D52-B93A-486D9507A1BE"; //qs["DEPOTID"].toString();
    //}
    finyearChecking(FinYear);
    $('#dvAdd').css("display", "none");
    $('#dvDisplay').css("display", "");
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "positionClass": "toast-top-center",
        "onclick": null,
        "showDuration": "500",
        "hideDuration": "1200",
        "timeOut": "3200",
        "extendedTimeOut": "6200",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "slideDown",
        "hideMethod": "slideUp"
    };
    $('#divScrapNo').css("display", "none");
    $("#SCRAPNO").attr("disabled", "disabled");

    if (CHECKER == 'FALSE') {
        $('#btnsave').css("display", "");
        $('#btnAddnew').css("display", "");
        $('#btnApprove').css("display", "none");
    }
    else {
        $('#btnAddnew').css("display", "none");
        $('#btnsave').css("display", "none");
        $('#btnApprove').css("display", "");
    }

    bindsourceDepot(UserID);    
    bindUserDepartement($('#BRANCHID').val());

    $("#btnAddnew").click(function (e) {
        $('#dvAdd').css("display", "");
        $('#dvDisplay').css("display", "none");
        ClearControls();
        e.preventDefault();
    }) 

    $("#btnClose").click(function (e) {
        $('#dvAdd').css("display", "none");
        $('#dvDisplay').css("display", "");
        ClearControls();
        e.preventDefault();
    })

    $("#btnsearch").click(function (e) {
        BindScrapRequest();
        e.preventDefault();
    })

    $('#USERTYPEID').change(function (e) {
        if ($('#USERTYPEID').val().trim() != '0') {
            bindUserDepartmentWise($('#USERTYPEID').val().trim());
            $("#USERID").chosen({
                search_contains: true
            });
            $("#USERID").trigger("chosen:updated");
        }
        else {
            $("#USERID").empty();
            $("#USERID").chosen({
                search_contains: true
            });
            $("#USERID").trigger("chosen:updated");
        }
        e.preventDefault();
    })

    $('#PRIMARYID').change(function (e) {
        if ($('#PRIMARYID').val().trim() != '0') {
            bindScrapSubItem($('#PRIMARYID').val().trim());
            $("#PRIMARYID").chosen({
                search_contains: true
            });
            $("#PRIMARYID").trigger("chosen:updated");
        }
        else {
            $("#PRIMARYID").empty();
            $("#PRIMARYID").chosen({
                search_contains: true
            });
            $("#PRIMARYID").trigger("chosen:updated");
        }
        e.preventDefault();
    })

    $('#SUBID').change(function (e) {
        if ($('#SUBID').val().trim() != '0') {
            GetScrapProduct($('#SUBID').val().trim(), $('#BRANCHID').val());
            $("#PRODUCTID").chosen({
                search_contains: true
            });
            $("#PRODUCTID").trigger("chosen:updated");
        }
        else {
            $("#PRODUCTID").empty();
            $("#PRODUCTID").chosen({
                search_contains: true
            });
            $("#PRODUCTID").trigger("chosen:updated");
        }
        e.preventDefault();
    })

    $('#PRODUCTID').change(function (e) {
        if ($('#PRODUCTID').val().trim() != '0') {
            $("#UOMID").empty();
            bindScrapProductUOM($('#PRODUCTID').val().trim());
            $("#UOMID").chosen({
                search_contains: true
            });
            $("#UOMID").trigger("chosen:updated");
        }
        else {
            $("#UOMID").empty();
            $("#UOMID").chosen({
                search_contains: true
            });
            $("#UOMID").trigger("chosen:updated");
        }
        e.preventDefault();
    })

    $("#btnadd").click(function (e) {
        if ($('#PRIMARYID').val() == '0') {
            toastr.warning('<b><font color=black>Please select Primary Item..!</font></b>');
            return false;
        }
        else if ($('#SUBID').val() == '0') {
            toastr.warning('<b><font color=black>Please select Sub Item..!</font></b>');
            return false;
        }
        else if ($('#PRODUCTID').val() == '0') {
            toastr.warning('<b><font color=black>Please select Product..!</font></b>');
            return false;
        }
        else if ($('#UOMID').val() == '0') {
            toastr.warning('<b><font color=black>Please select UOM..!</font></b>');
            return false;
        }
        else if ($('#SCRAPQTY').val() == '') {
            toastr.warning('<b><font color=black>Please enter Scrap quantity..!</font></b>');
            return false;
        }
        else {
            //$("#dialog").dialog({
            //    autoOpen: true,
            //    modal: true,
            //    title: "Loading.."
            //});
            //$("#imgLoader").css("visibility", "visible");
            //setTimeout(function () {
            var PRIMARYID = $('#PRIMARYID').val().trim();
            var PRIMARYNAME = $('#PRIMARYID').find('option:selected').text();
            var SUBID = $('#SUBID').val().trim();
            var SUBNAME = $('#SUBID').find('option:selected').text();
            var PRODUCTID = $('#PRODUCTID').val().trim();
            var PRODUCTNAME = $('#PRODUCTID').find('option:selected').text();
            var UOMID = $('#UOMID').val().trim();
            var UOMNAME = $('#UOMID').find('option:selected').text();
            var SCRAPQTY = $('#SCRAPQTY').val();
            //inputMaterialqty = $(this).find('#txtinputqty').val().trim();
            IsExistsScrapProduct(PRIMARYID, PRIMARYNAME, SUBID, SUBNAME, PRODUCTID, PRODUCTNAME, UOMID, UOMNAME, SCRAPQTY);

            //$("#imgLoader").css("visibility", "hidden");
            //$("#dialog").dialog("close");
            //}, 3);
        }
        e.preventDefault();
    })

    /*Add Product Start*/
    function IsExistsScrapProduct(PRIMARYID, PRIMARYNAME, SUBID, SUBNAME, PRODUCTID, PRODUCTNAME, UOMID, UOMNAME, SCRAPQTY) {
        if ($('#ScrapProductDetailsGrid').length) {
            var exists = false;
            var arraydetails = [];
            $('#ScrapProductDetailsGrid tbody tr').each(function () {
                var ScrapDtls = {};
                var inputproductgrd = $(this).find('td:eq(5)').html().trim();
                var inputScrapQtygrd = $(this).find('td:eq(9)').html().trim();
                ScrapDtls.INPUTPRODUCT = inputproductgrd;
                ScrapDtls.INPUTBATCH = inputScrapQtygrd;
                arraydetails.push(ScrapDtls);
            });
            var jsondispatchobj = {};
            jsondispatchobj.conversionproductDetails = arraydetails;

            for (i = 0; i < jsondispatchobj.conversionproductDetails.length; i++) {
                if (jsondispatchobj.conversionproductDetails[i].INPUTPRODUCT.trim() == PRODUCTID.trim() && jsondispatchobj.conversionproductDetails[i].INPUTBATCH.trim() == SCRAPQTY.trim()) {
                    exists = true;
                    break;
                }
            }
            if (exists != false) {
                toastr.error('Item already exists...!');
                return false;
            }
            else {
                addScrapFinalGrid(PRIMARYID, PRIMARYNAME, SUBID, SUBNAME, PRODUCTID, PRODUCTNAME, UOMID, UOMNAME, SCRAPQTY);
            }
        }
    }

    function addScrapFinalGrid(PRIMARYID, PRIMARYNAME, SUBID, SUBNAME, PRODUCTID, PRODUCTNAME, UOMID, UOMNAME, SCRAPQTY) {
        //Create Table 
        var tr;
        if (CHECKER.trim() == 'FALSE') {
            tr = $('#ScrapProductDetailsGrid');
            var HeaderCount = $('#ScrapProductDetailsGrid thead th').length;
            if (HeaderCount == 0) {
                tr.append("<thead><tr><tr><th>Sl.No.</th><th style='display: none'>PRIMARYID</th><th>PRIMARY NAME</th><th style='display: none'>SUBID</th><th>SUBITEM NAME</th><th style='display: none'>PRODUCTID</th><th>PRODUCT NAME</th><th style='display: none'>UOMID</th><th>UOM</th><th>SCRAP QTY</th><th style='display: none'>ALLREADY RECVDQTY</th><th>Delete</th></tr></thead><tbody>");
            }
            tr = $('<tr/>');
            tr.append("<td style='text-align: center;'><label class='slno' id='lblScrapSlno'></label></td>");//0
            tr.append("<td style='display: none'>" + PRIMARYID + "</td>");//1
            tr.append("<td>" + PRIMARYNAME + "</td>");//2

            tr.append("<td style='display: none'>" + SUBID + "</td>");//3
            tr.append("<td>" + SUBNAME + "</td>");//4

            tr.append("<td style='display: none'>" + PRODUCTID + "</td>");//5
            tr.append("<td>" + PRODUCTNAME + "</td>");//6

            tr.append("<td style='display: none'>" + UOMID + "</td>");//7
            tr.append("<td>" + UOMNAME + "</td>");//8

            tr.append("<td>" + SCRAPQTY + "</td>");//9
            tr.append("<td style='display: none'>" + "0" + "</td>");//10
            tr.append("<td style='display: none'>" + "0" + "</td>");//11
            tr.append("<td style='text-align: center'><input type='image' class='gvTempDelete'  id='btndelete' <img src='../Images/ico_delete_16.png'/></input></td>");
            $("#ScrapProductDetailsGrid").append(tr);
            tr.append("</tbody>");
            ScrapDetailsRowCount();
        }
        else {
        }
    }

    

    $("#btnsave").click(function (e) {
        if ($('#hdnScrapID').val() == '0') {
            SaveScrap();
        }
        else {
            SaveScrap();
        }
        e.preventDefault();
    })

    $("#btnApprove").click(function (e) {
        if ($('#hdnScrapID').val() != '0') {
            RecevdScrap();
        }
        e.preventDefault();
    })
});

function ScrapDetailsRowCount() {
    var cnvdetailsrowCount = document.getElementById("ScrapProductDetailsGrid").rows.length - 1;
    var finalcnvcount = 0;
    if (cnvdetailsrowCount > 0) {
        //$('#USERTYPEID').val('0');
        //$('#USERID').val('0');
        //$('#PRIMARYID').val('0');
        //$('#SUBID').val('0');
        //$('#UOMID').val('0');
        //$('#SCRAPQTY').val('');

        //$('#PRODUCTID').val('0');
        //$("#PRODUCTID").chosen({
        //    search_contains: true
        //});
        //$("#PRODUCTID").trigger("chosen:updated");

        $('#ScrapProductDetailsGrid tbody tr').each(function () {            
            finalcnvcount = finalcnvcount + 1;            
            $(this).find('#lblScrapSlno').text(finalcnvcount.toString().trim());
        })
    }
}


function RowCountEdit() {
    var table = document.getElementById("ScrapProductDetailsGrid");
    var rowCount = document.getElementById("ScrapProductDetailsGrid").rows.length - 1;
    var count = 0;
    if (rowCount > 0) {
        $('#ScrapProductDetailsGrid tbody tr').each(function () {
            count = count + 1;
            $(this).find('#lblScrapSlno').text(count.toString().trim());
        })
    }
}

function bindsourceDepot(UserID) {
    var SourceDepot = $("#BRANCHID");
    $.ajax({
        type: "POST",
        url: "/Transporter/BindDepot",
        data: { userid: UserID },
        async: false,
        dataType: "json",

        success: function (response) {
            SourceDepot.empty();
            $.each(response, function () {
                SourceDepot.append($("<option></option>").val(this['BRID']).html(this['BRNAME']));
            });
            var sourceDepot = $('#BRANCHID').val();
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function RecevdScrap() {
    //var StockFlag = true;
    //if (StockFlag == true) {
    //    $("#dialog").dialog({
    //        autoOpen: true,
    //        modal: true,
    //        title: "Loading.."
    //    });
    //    $("#imgLoader").css("visibility", "visible");
    //    setTimeout(function () {

    var i = 0;
    var count = 0;
    Scraplist = new Array();
    var ScrapHeaderSave = {};
    ScrapHeaderSave.SCRAPID = $('#hdnScrapID').val().trim();
    ScrapHeaderSave.SCRAPNO = $("#SCRAPNO").val().trim();
    var Chk = validateData();
    if (Chk == 'N') {
        toastr.warning('<b><font color=black>Received Qty Can not be greater than Scrap Qty.</font></b>');
        return false;
    }
    else {
        $('#ScrapProductDetailsGrid tbody tr').each(function () {            
            count = count + 1;
            var ScrapDetailsSave = {};
            var productid = $(this).find('td:eq(5)').html().trim();
            var productname = $(this).find('td:eq(6)').html().trim();
            var scrapqty = $(this).find('td:eq(9)').html().trim();
            var recevdqty = $(this).find('#txtrecvdQty').val().trim();
            if (recevdqty > 0) {
                ScrapDetailsSave.PRODUCTID = productid;
                ScrapDetailsSave.PRODUCTNAME = productname;
                ScrapDetailsSave.SCRAPQTY = scrapqty;
                ScrapDetailsSave.RECVDQTY = recevdqty;
                Scraplist[i++] = ScrapDetailsSave;
            }
        });

        ScrapHeaderSave.ScrapProductDetails = Scraplist;

        $.ajax({
            url: "/Scrap/ScrapRecvdSavedata",
            data: '{ScrapHeaderSave:' + JSON.stringify(ScrapHeaderSave) + '}',
            type: "POST",
            async: false,
            contentType: "application/json",
            success: function (responseMessage) {
                var messageid;
                var messagetext;
                $.each(responseMessage, function (key, item) {
                    messageid = item.MessageID;
                    messagetext = item.MessageText;
                });
                if (messageid == '0') {
                    $('#dvAdd').css("display", "");
                    $('#dvDisplay').css("display", "none");
                    toastr.error('<b><font color=white>' + messagetext + '</font></b>');
                }
                else if (messageid == '1') {
                    $('#dvAdd').css("display", "none");
                    $('#dvDisplay').css("display", "");
                    toastr.success('<b><font color=black>' + messagetext + '</font></b>');
                    ClearControls();
                    BindScrapRequest();
                }
                else if (messageid == '2') {
                    $('#dvAdd').css("display", "none");
                    $('#dvDisplay').css("display", "");
                    $("#txtfrmdate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
                    $("#txttodate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
                    ClearControls();
                    BindScrapRequest();
                    toastr.success('<b><font color=black>' + messagetext + '</font></b>');
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
    //        $("#imgLoader").css("visibility", "hidden");
    //        $("#dialog").dialog("close");
    //    }, 3);
    //}
}

function validateData() {    
    var chk = 'N';
    var count = 0;
    var Balanceqty = 0;
    $('#ScrapProductDetailsGrid tbody tr').each(function () {        
        count = count + 1;
        var scrapqty = $(this).find('td:eq(9)').html().trim();
        var recevdqty = $(this).find('#txtrecvdQty').val().trim();
        var Allrecevdqty = $(this).find('td:eq(11)').html().trim();
        Balanceqty = (scrapqty - Allrecevdqty);
        if (recevdqty > 0) {
            if (recevdqty <= Balanceqty) {
                chk = 'Y';
            }
            else {
                chk = 'N';
            }
        }
    });
    return chk;
}

function SaveScrap() {
    //var StockFlag = true;
    //if (StockFlag == true) {
    //    $("#dialog").dialog({
    //        autoOpen: true,
    //        modal: true,
    //        title: "Loading.."
    //    });
    //    $("#imgLoader").css("visibility", "visible");
    //    setTimeout(function () {

    var i = 0;
    Scraplist = new Array();
    var ScrapHeaderSave = {};
    ScrapHeaderSave.SCRAPID = $('#hdnScrapID').val().trim();

    if ($('#hdnScrapID').val() == '0') {
        ScrapHeaderSave.MODE = 'A';
    }
    else {
        ScrapHeaderSave.MODE = 'U';
    }
    ScrapHeaderSave.SCRAPDATE = $("#SCRAPDATE").val().trim();
    ScrapHeaderSave.USERTYPEID = $("#USERTYPEID").val().trim();
    ScrapHeaderSave.USERTYPE = $("#USERTYPEID option:selected").text().trim();

    ScrapHeaderSave.USERID = $("#USERID").val().trim();
    ScrapHeaderSave.USERNAME = $("#USERID option:selected").text().trim();
    ScrapHeaderSave.BRANCHID = $('#BRANCHID').val();
    ScrapHeaderSave.CREATEDBY = UserID;
    ScrapHeaderSave.FINYEAR = FinYear;

    if ($("#REMARKS").val().trim() == '') {
        ScrapHeaderSave.REMARKS = '';
    }
    else {
        ScrapHeaderSave.REMARKS = $("#REMARKS").val().trim();
    }
    var count = 0;
    $('#ScrapProductDetailsGrid tbody tr').each(function () {
        count = count + 1;
        var ScrapDetailsSave = {};
        var primaryid = $(this).find('td:eq(1)').html().trim();
        var primaryname = $(this).find('td:eq(2)').html().trim();
        var subid = $(this).find('td:eq(3)').html().trim();
        var subname = $(this).find('td:eq(4)').html().trim();
        var productid = $(this).find('td:eq(5)').html().trim();
        var productname = $(this).find('td:eq(6)').html().trim();
        var uomid = $(this).find('td:eq(7)').html().trim();
        var uomname = $(this).find('td:eq(8)').html().trim();
        var scrapqty = $(this).find('td:eq(9)').html().trim();
        var recevdqty = $(this).find('td:eq(10)').html().trim();

        ScrapDetailsSave.PRIMARYID = primaryid;
        ScrapDetailsSave.PRIMARYNAME = primaryname;
        ScrapDetailsSave.SUBID = subid;
        ScrapDetailsSave.SUBITEMNAME = subname;
        ScrapDetailsSave.PRODUCTID = productid;
        ScrapDetailsSave.PRODUCTNAME = productname;
        ScrapDetailsSave.UOMID = uomid;
        ScrapDetailsSave.UOM = uomname;
        ScrapDetailsSave.SCRAPQTY = scrapqty;
        ScrapDetailsSave.RECVDQTY = recevdqty;
        Scraplist[i++] = ScrapDetailsSave;
    });

    ScrapHeaderSave.ScrapProductDetails = Scraplist;

    //alert(JSON.stringify(ScrapHeaderSave));

    $.ajax({
        url: "/Scrap/ScrapHeaderSavedata",
        data: '{ScrapHeaderSave:' + JSON.stringify(ScrapHeaderSave) + '}',
        type: "POST",
        async: false,
        contentType: "application/json",
        success: function (responseMessage) {
            var messageid;
            var messagetext;
            $.each(responseMessage, function (key, item) {
                messageid = item.MessageID;
                messagetext = item.MessageText;
            });
            if (messageid == '0') {
                $('#dvAdd').css("display", "");
                $('#dvDisplay').css("display", "none");
                toastr.error('<b><font color=white>' + messagetext + '</font></b>');
            }
            else if (messageid == '1') {
                $('#dvAdd').css("display", "none");
                $('#dvDisplay').css("display", "");
                toastr.success('<b><font color=black>' + messagetext + '</font></b>');
                ClearControls();
                BindScrapRequest();
            }
            else if (messageid == '2') {
                $('#dvAdd').css("display", "none");
                $('#dvDisplay').css("display", "");
                $("#txtfrmdate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
                $("#txttodate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
                ClearControls();
                BindScrapRequest();
                toastr.success('<b><font color=black>' + messagetext + '</font></b>');
            }
        },
        failure: function (responseMessage) {
            alert(responseMessage.responseText);
        },
        error: function (responseMessage) {
            alert(responseMessage.responseText);
        }
    });

    //        $("#imgLoader").css("visibility", "hidden");
    //        $("#dialog").dialog("close");
    //    }, 3);
    //}
}

function finyearChecking(FinYear) {
    var currentdt;
    var frmdate;
    var todate;
    $.ajax({
        type: "POST",
        url: "/Transporter/finyrchk",
        data: { finyr: FinYear },
        async: false,
        dataType: "json",
        success: function (response) {
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

    $("#txtfrmdate").datepicker({
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
    $("#SCRAPDATE").datepicker({
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        selectCurrent: true,
        todayBtn: "linked",
        showAnim: "slideDown",
        yearRange: "-3:+0",
        maxDate: 'today',
        dateFormat: "dd/mm/yy",
        showOn: 'button',
        buttonText: 'Show Date',
        buttonImageOnly: true,
        maxDate: new Date(currentdt),
        minDate: new Date(currentdt),
        buttonImage: 'http://jqueryui.com/resources/demos/datepicker/images/calendar.gif'
    });
    $(".ui-datepicker-trigger").mouseover(function () {
        $(this).css('cursor', 'pointer');
    });

    $("#txtfrmdate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
    $("#txttodate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
    $("#SCRAPDATE").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
}

function bindUserDepartement(factoryid) {
    var UserTypeID = $("#USERTYPEID");
    $.ajax({
        type: "POST",
        url: "/Scrap/GetUserDepartment",
        data: { FactoryID: factoryid },
        async: false,
        dataType: "json",
        success: function (response) {
            //alert(JSON.stringify(response));            
            UserTypeID.empty().append('<option selected="selected" value="0">Please Select</option>');;
            $.each(response, function () {
                UserTypeID.append($("<option></option>").val(this['UTID']).html(this['UTNAME']));
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

function bindUserDepartmentWise(UserTypeID) {
    var USERID = $("#USERID");
    $.ajax({
        type: "POST",
        url: "/Scrap/GetUserDepartmentWise",
        data: { USERTYPEID: UserTypeID },
        async: false,
        dataType: "json",
        success: function (response) {
            //alert(JSON.stringify(response));
            USERID.empty().append('<option selected="selected" value="0">Please select</option>');;
            $.each(response, function () {
                USERID.append($("<option></option>").val(this['USERID']).html(this['USERNAME']));
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

function bindScrapSubItem(primaryID) {
    var SUBID = $("#SUBID");
    $.ajax({
        type: "POST",
        url: "/Scrap/GetScrapSubItem",
        data: { PrimaryID: primaryID },
        async: false,
        dataType: "json",
        success: function (response) {
            //alert(JSON.stringify(response));
            SUBID.empty().append('<option selected="selected" value="0">Please select</option>');;
            $.each(response, function () {
                SUBID.append($("<option></option>").val(this['SUBTYPEID']).html(this['SUBITEMNAME']));
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

function GetScrapProduct(subtypeID, factoryID) {
    var PRODUCTID = $("#PRODUCTID");
    $.ajax({
        type: "POST",
        url: "/Scrap/GetScrapProduct",
        data: { SubtypeID: subtypeID, FactoryID: factoryID },
        async: false,
        dataType: "json",
        success: function (response) {
            //alert(JSON.stringify(response));
            PRODUCTID.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(response, function () {
                PRODUCTID.append($("<option></option>").val(this['ID']).html(this['PRODUCTALIAS']));
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

function bindScrapProductUOM(productID) {
    var UOMID = $("#UOMID");
    $.ajax({
        type: "POST",
        url: "/Scrap/GetScrapProductUOM",
        data: { ProductID: productID },
        async: false,
        dataType: "json",
        success: function (response) {
            //alert(JSON.stringify(response));
            //UOMID.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(response, function () {
                UOMID.append($("<option></option>").val(this['UOMID']).html(this['UOMNAME']));
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

function ClearControls() {
    if (CHECKER == 'FALSE') {
        $('#btnsave').css("display", "");
        $('#btnAddnew').css("display", "");
        $('#btnApprove').css("display", "none");
    }
    else {
        $('#btnAddnew').css("display", "none");
        $('#btnsave').css("display", "none");
        $('#btnApprove').css("display", "");
    }
    $("#USERTYPEID").val('0');
    $("#USERTYPEID").chosen({
        search_contains: true
    });
    $("#USERTYPEID").trigger("chosen:updated");
    $("#USERTYPEID").removeAttr("disabled");

    $("#USERID").empty();
    $("#USERID").chosen({
        search_contains: true
    });
    $("#USERID").trigger("chosen:updated");
    $("#USERID").removeAttr("disabled");

    $("#PRIMARYID").val('0');
    $("#PRIMARYID").chosen({
        search_contains: true
    });
    $("#PRIMARYID").trigger("chosen:updated");
    $("#PRIMARYID").removeAttr("disabled");

    $("#SUBID").empty();
    //$("#SUBID").chosen({
    //    search_contains: true
    //});
    //$("#SUBID").trigger("chosen:updated");
    //$("#SUBID").removeAttr("disabled");

    $("#PRODUCTID").empty();
    $("#PRODUCTID").chosen({
        search_contains: true
    });
    $("#PRODUCTID").trigger("chosen:updated");
    $("#PRODUCTID").removeAttr("disabled");
    $('#divScrapNo').css("display", "none");
    $("#SCRAPNO").attr("disabled", "disabled");
    $("#UOMID").empty();
    $('#SCRAPQTY').val('');
    $('#REMARKS').val('');
    $('#ScrapProductDetailsGrid').empty();
    $('#hdnScrapID').val('0');
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

function BindScrapRequest() {
    var srl = 0;
    srl = srl + 1;
    //$("#dialog").dialog({
    //    autoOpen: true,
    //    modal: true,
    //    title: "Loading.."
    //});
    //$("#imgLoader").css("visibility", "visible");
    $.ajax({
        type: "POST",
        url: "/Scrap/BindScrapRequestGrid",
        data: { FromDate: $('#txtfrmdate').val().trim(), ToDate: $('#txttodate').val().trim(), Checker: CHECKER.trim(), FinYear: FinYear, DepotID: $('#BRANCHID').val() },
        dataType: "json",
        success: function (response) {
            var tr;
            tr = $('#ScrapRequestGrid');
            tr.append("<thead><tr><th>Sl. No.</th><th style='display: none'>SCRAPID</th><th>SCRAP NO</th><th>SCRAP DATE</th><th>USER TYPE</th><th>USER NAME</th><th>STATUS</th><th>ENTRY USER</th><th>EDIT</th><th>DELETE</th>");

            $('#ScrapRequestGrid').DataTable().destroy();
            $("#ScrapRequestGrid tbody tr").remove();
            for (var i = 0; i < response.length; i++) {
                tr = $('<tr/>');
                tr.append("<td style='text-align: center'>" + srl + "</td>");//0
                tr.append("<td style='display: none'>" + response[i].SCRAPID + "</td>");
                tr.append("<td>" + response[i].SCRAPNO + "</td>");
                tr.append("<td>" + response[i].SCRAPDATE + "</td>");
                tr.append("<td >" + response[i].USERTYPE + "</td>");
                tr.append("<td >" + response[i].USERNAME + "</td>");
                if (response[i].STATUS.trim() == 'APPROVE') {
                    tr.append("<td style='text-align: center'><font color='0x00B300'>" + response[i].STATUS + "</font></td>");                    
                }
                else {
                    tr.append("<td style='text-align: center'><font color='#ff2500'>" + response[i].STATUS.trim() + "</font></td>");//8                   
                }
                tr.append("<td >" + response[i].ENTRYUSER + "</td>");
                tr.append("<td style='text-align: center'><input type='image' class='gvEdit' id='btnScrapedit' <img src='../Images/Pencil-icon.png' title='Edit'/></input></td>");
                tr.append("<td style='text-align: center'><input type='image' class='gvCancel' id='btnScrapdelete' <img src='../Images/ico_delete_16.png' title='Cancel'/></input></td>");
                $("#ScrapRequestGrid").append(tr);
            }
            tr.append("</tbody>");
            RowCountScrapList();
            $('#ScrapRequestGrid').DataTable({
                "sScrollX": '100%',
                "sScrollXInner": "110%",
                "scrollY": "238px",
                "scrollCollapse": true,
                "dom": 'Bfrtip',
                buttons: [
                    {
                        extend: 'excelHtml5',
                        title: 'Stock Journal List'
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
            //$("#imgLoader").css("visibility", "hidden");
            //$("#dialog").dialog("close");
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function RowCountScrapList() {
    var table = document.getElementById("ScrapRequestGrid");
    var rowCount = document.getElementById("ScrapRequestGrid").rows.length - 1;
    if (rowCount > 0) {
        for (var i = 1; i <= rowCount; i++) {
            table.rows[i].cells[0].innerHTML = i.toString();
        }
    }
}

/*Edit function for Scrap Request*/
$(function () {
    var Scrapid;
    $("body").on("click", "#ScrapRequestGrid .gvEdit", function () {
        var row = $(this).closest("tr");
        Scrapid = row.find('td:eq(1)').html();
        $('#hdnScrapID').val(Scrapid);
        $('#ScrapProductDetailsGrid').empty();
        EditDetails($('#hdnScrapID').val().trim());

        if (CHECKER == 'FALSE') {
            $('#btnsave').css("display", "");
            $('#btnAddnew').css("display", "");
            $('#btnApprove').css("display", "none");
        }
        else {
            $('#btnAddnew').css("display", "none");
            $('#btnsave').css("display", "none");
            $('#btnApprove').css("display", "");
        }
        $("#dvAdd").find("input, textarea, select,submit").removeAttr("disabled");
        $('#divScrapNo').css("display", "");
        $("#SCRAPNO").attr("disabled", "disabled");
        $('#dvAdd').css("display", "");
        $('#dvDisplay').css("display", "none");
        $("#SCRAPDATE").datepicker("destroy");
    })
})

function EditDetails(scrapid) {
    var tr;
    var HeaderCount = 0;
    var srl = 0;
    srl = srl + 1;

    $("#ScrapProductDetailsGrid").empty();
    //$("#dialog").dialog({
    //    autoOpen: true,
    //    modal: true,
    //    title: "Loading.."
    //});
    //$("#imgLoader").css("visibility", "visible");
    $.ajax({
        type: "POST",
        url: "/Scrap/ScrapEdit",
        data: { ScrapID: scrapid },
        dataType: "json",
        async: false,
        success: function (response) {            
            //alert(JSON.stringify(response))
            var listHeader = response.alleditDataset.varHeader;
            var listDetails = response.alleditDataset.varDetails;

            /*Header Info*/
            $.each(listHeader, function (index, record) {
                $("#SCRAPNO").val(this['SCRAPNO'].toString().trim());
                $("#SCRAPDATE").val(this['SCRAPDATE'].toString().trim());
                $("#USERTYPEID").val(this['USERTYPEID'].toString().trim());
                //USERID.empty();

                bindUserDepartmentWise($('#USERTYPEID').val().trim());
                USERID.append($("<option></option>").val(this['USERID']).html(this['USERNAME']));
                //$("#USERID").chosen({
                //    search_contains: true
                //});
                //$("#USERID").trigger("chosen:updated");
                $("#REMARKS").val(this['REMARKS'].toString().trim());
            });

            /*Details Info*/
            if (listDetails.length > 0) {
                $("#ScrapProductDetailsGrid").empty();
                if (CHECKER.trim() == 'FALSE') {
                    tr = $('#ScrapProductDetailsGrid');
                    HeaderCount = $('#ScrapProductDetailsGrid thead th').length;
                    if (HeaderCount == 0) {
                        tr.append("<thead><tr><tr><th>Sl.No.</th><th style='display: none'>PRIMARYID</th><th>PRIMARY NAME</th><th style='display: none'>SUBID</th><th>SUBITEM NAME</th><th style='display: none'>PRODUCTID</th><th>PRODUCT NAME</th><th style='display: none'>UOMID</th><th>UOM</th><th>SCRAP QTY</th><th style='display: none'>ALLREADY RECVDQTY</th><th>Delete</th></tr></thead><tbody>");
                    }
                    $.each(listDetails, function (index, record) {
                        tr = $('<tr/>');
                        //tr.append("<td style='text-align: center'>" + srl + "</td>");//0
                        tr.append("<td style='text-align: center;'><label id='lblScrapSlno'></label></td>");//0
                        tr.append("<td style='display: none'>" + this['PRIMARYID'].toString().trim() + "</td>");//1
                        tr.append("<td>" + this['PRIMARYNAME'].toString().trim() + "</td>");//2
                        tr.append("<td style='display: none'>" + this['SUBID'].toString().trim() + "</td>");//3
                        tr.append("<td>" + this['SUBITEMNAME'].toString().trim() + "</td>");//4
                        tr.append("<td style='display: none'>" + this['PRODUCTID'].toString().trim() + "</td>");//5
                        tr.append("<td>" + this['PRODUCTNAME'].toString().trim() + "</td>");//6                        

                        tr.append("<td style='display: none'>" + this['UOMID'].toString().trim() + "</td>");//7
                        tr.append("<td>" + this['UOM'].toString().trim() + "</td>");//8
                        tr.append("<td>" + this['SCRAPQTY'].toString().trim() + "</td>");//9

                        tr.append("<td style='display: none'>" + this['RECVDQTY'].toString().trim() + "</td>");//10
                        tr.append("<td style='display: none'>" + this['ALLREADYRECVDQTY'].toString().trim() + "</td>");//11
                        tr.append("<td style='text-align: center'><input type='image' class='gvTempDelete'  id='btndelete' <img src='../Images/ico_delete_16.png'/></input></td>");
                        $("#ScrapProductDetailsGrid").append(tr);
                        tr.append("</tbody>");
                        //ScrapDetailsRowCount();
                        RowCountEdit();
                    });
                }
                else {
                    tr = $('#ScrapProductDetailsGrid');
                    HeaderCount = $('#ScrapProductDetailsGrid thead th').length;
                    if (HeaderCount == 0) {
                        tr.append("<thead><tr><tr><th>Sl.No.</th><th style='display: none'>PRIMARYID</th><th>PRIMARY NAME</th><th style='display: none'>SUBID</th><th>SUBITEM NAME</th><th style='display: none'>PRODUCTID</th><th>PRODUCT NAME</th><th style='display: none'>UOMID</th><th>UOM</th><th>SCRAP QTY</th><th>RECVD QTY</th><th>ALLREADY RECVDQTY</th><th>Delete</th></tr></thead><tbody>");
                    }
                    $.each(listDetails, function (index, record) {
                        tr = $('<tr/>');
                        //tr.append("<td style='text-align: center'>" + srl + "</td>");//0
                        tr.append("<td style='text-align: center;'><label id='lblScrapSlno'></label></td>");//0
                        tr.append("<td style='display: none'>" + this['PRIMARYID'].toString().trim() + "</td>");//1
                        tr.append("<td>" + this['PRIMARYNAME'].toString().trim() + "</td>");//2
                        tr.append("<td style='display: none'>" + this['SUBID'].toString().trim() + "</td>");//3
                        tr.append("<td>" + this['SUBITEMNAME'].toString().trim() + "</td>");//4
                        tr.append("<td style='display: none'>" + this['PRODUCTID'].toString().trim() + "</td>");//5
                        tr.append("<td>" + this['PRODUCTNAME'].toString().trim() + "</td>");//6
                        tr.append("<td style='display: none'>" + this['UOMID'].toString().trim() + "</td>");//7
                        tr.append("<td>" + this['UOM'].toString().trim() + "</td>");//8
                        tr.append("<td>" + this['SCRAPQTY'].toString().trim() + "</td>");//9
                        //tr.append("<td>" + this['RECVDQTY'].toString().trim() + "</td>");//10
                        tr.append("<td style='text-align: right'><input type='text' class='gvstartposition' id='txtrecvdQty' style='text-align: right; width:60px; height:18px' value=" + this['RECVDQTY'].toString().trim() + "></input></td>");//10
                        tr.append("<td>" + this['ALLREADYRECVDQTY'].toString().trim() + "</td>");//11
                        tr.append("<td style='text-align: center'><input type='image' class='gvTempDelete'  id='btndelete' <img src='../Images/ico_delete_16.png'/></input></td>");
                        $("#ScrapProductDetailsGrid").append(tr);
                        tr.append("</tbody>");
                        //ScrapDetailsRowCount();
                        RowCountEdit();
                    });
                }
            }
            //$("#imgLoader").css("visibility", "hidden");
            //$("#dialog").dialog("close");
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}
