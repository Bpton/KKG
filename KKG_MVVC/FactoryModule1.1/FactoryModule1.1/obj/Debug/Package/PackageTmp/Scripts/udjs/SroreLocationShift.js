//#region Developer Info
/*
* For InterStoreLocation.cshtml Only
* Developer Name : Avishek Ghosh 
* Start Date     : 01/08/2020
* END Date       : 
 
*/
//#endregion
var CHECKER;
var backdatestatus;
var entryLockstatus;
var BusinessSegment;
var Name;
var VoucherID;
var menuid;
var TotalPcsQty;
var PriceSchemeNotification = false;
var QuantitySchemeNotification = false;
var PopupOpen = false;

$(document).ready(function () {
    ////debugger;
    var qs = getQueryStrings();
    if (qs["CHECKER"] != undefined && qs["CHECKER"] != "") {
        CHECKER = qs["CHECKER"].trim();
    }

    if (qs["MENUID"] != undefined && qs["MENUID"] != "") {
        menuid = qs["MENUID"].trim();
    }

    if (qs["BSID"] != undefined && qs["BSID"] != "") {
        BusinessSegment = qs["BSID"].trim();
    }

    if (qs["FINYEAR"].toString() != undefined && qs["FINYEAR"].toString() != "") {
        $("#hdnFinYear").val(qs["FINYEAR"].toString());
    }

    if (qs["USERID"].toString() != undefined && qs["USERID"].toString() != "") {
        $("#hdnUserID").val(qs["USERID"].toString());
    }
    ////debugger;
    if (qs["DEPOTID"].toString() != undefined && qs["DEPOTID"].toString() != "") {
        $("#hdnDepotID").val(qs["DEPOTID"].toString());
    }

    if (qs["TPU"].toString() != undefined && qs["TPU"].toString() != "") {
        $("#hdnTPU").val(qs["TPU"].toString());
    }

    $("#hdnCHECKER").val(CHECKER);
    $("#hdnmenuid").val(menuid);
    finyearChecking($("#hdnFinYear").val().trim());

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

    $('#divTransferNo').css("display", "none");
    $('#dvAdd').css("display", "none");
    $('#dvDisplay').css("display", "");
    $("#txtfrmdate").attr("disabled", "disabled");
    $("#txttodate").attr("disabled", "disabled");
    $("#TransferNo").attr("disabled", "disabled");

    
    $('#btnsave').css("display", "");
    $('#btnAddnew').css("display", "");
    $('#btnApprove').css("display", "none");
   
    bindsourceDepot($("#hdnUserID").val().trim());
    $("#SearchDepotID").chosen({
        search_contains: true
    });
    $("#SearchDepotID").trigger("chosen:updated");
    bindStorelocation();
    bindReason(menuid);
    bindPacksize();
    bindproduct($("#BRID").val().trim());
    bindstockjournalgrid();


    $("#BRID").prop("disabled", true);
    $("#CUSTOMERID").prop("disabled", false);

    $("#btnsearch").click(function (e) {
        $("#dialog").dialog({
            autoOpen: true,
            modal: true,
            title: "Loading.."
        });
        $("#imgLoader").css("visibility", "visible");
        setTimeout(function () {
            bindstockjournalgrid();
            $("#imgLoader").css("visibility", "hidden");
            $("#dialog").dialog("close");
        }, 5);
        e.preventDefault();
    });

    $("#btnAddnew").click(function (e) {
        ////debugger;
        var offlinestatus = '';
        var offlinetag = '';

        offlinetag = OfflineTag();
        offlinestatus = OfflineStatus($("#SearchDepotID").val().trim());

        if (offlinetag == 'N') {
            if (offlinestatus == 'N') {
                loadAddNewRecord();
            }
            else {
                toastr.warning('<b><font color=black>Available for offline only</font></b>');
                return false;
            }
        }
        else {
            loadAddNewRecord();
        }
        e.preventDefault();
    });

    $("#btnclose").click(function (e) {
        $("#dialog").dialog({
            autoOpen: true,
            modal: true,
            title: "Loading.."
        });
        $("#imgLoader").css("visibility", "visible");
        setTimeout(function () {
            $('#dvAdd').css("display", "none");
            $('#dvDisplay').css("display", "");
            ClearControls();
            bindstockjournalgrid();
            $("#imgLoader").css("visibility", "hidden");
            $("#dialog").dialog("close");
        }, 5);
        e.preventDefault();
    });

    $('#SearchDepotID').change(function (e) {
        if ($('#SearchDepotID').val().trim() != '0') {

            $("#BRID").val($('#SearchDepotID').val().trim());
            bindstockjournalgrid();
        }
        e.preventDefault();
    });

    $('#PRODUCTID').change(function (e) {
       
        if ($('#PRODUCTID').val() != '0') {
            
            if ($('#StorelocationID').val() != '0') {

                $("#dialog").dialog({
                    autoOpen: true,
                    modal: true,
                    title: "Loading.."
                });
                $("#imgLoader").css("visibility", "visible");
                setTimeout(function () {

                    bindFGbatchno();
                    $("#ddlBatch").focus();

                    $("#imgLoader").css("visibility", "hidden");
                    $("#dialog").dialog("close");
                }, 5);
            }
            else {
                
                $('#JournalQuantity').val('');
                $('#StockQty').val('');
                $('#MRP').val('');
                $('#ddlBatch').empty();
                toastr.warning('<b>Please select Storelocation..</b>');
                return false;
            }
        }
        else {
           
            $('#JournalQuantity').val('');
            $('#StockQty').val('');
            $('#MRP').val('');

            $('#ddlBatch').empty();

            $("#ReasonID").val('0');
            $("#ReasonID").removeAttr("disabled");
            $("#ReasonID").chosen({
                search_contains: true
            });
            $("#ReasonID").trigger("chosen:updated");
        }

        $('#JournalQuantity').val('');
        $('#StockQty').val('');
        $('#MRP').val('');

        $("#ReasonID").val('0');
        $("#ReasonID").removeAttr("disabled");
        $("#ReasonID").chosen({
            search_contains: true
        });
        $("#ReasonID").trigger("chosen:updated");

        e.preventDefault();
    });

    $('#StorelocationID').change(function (e) {

        $("#ToStorelocationID").val('0');
        $("#ToStorelocationID").chosen({
            search_contains: true
        });
        $("#ToStorelocationID").trigger("chosen:updated");

        $("#PRODUCTID").val('0');
        $("#PRODUCTID").chosen({
            search_contains: true
        });
        $("#PRODUCTID").trigger("chosen:updated");

        

        $('#ddlBatch').empty();

            
        e.preventDefault();
    });

    $('#ddlBatch').change(function (e) {

        if ($('#ddlBatch').val() != '0') {
            $("#dialog").dialog({
                autoOpen: true,
                modal: true,
                title: "Loading.."
            });
            $("#imgLoader").css("visibility", "visible");
            setTimeout(function () {

                getBatchDetails();

                $("#imgLoader").css("visibility", "hidden");
                $("#dialog").dialog("close");
            }, 5);
        }
        else {
            
            $('#StockQty').val('');
            $('#MRP').val('');
        }

        e.preventDefault();
    });

    $("#btnadd").click(function (e) {
        //debugger;
        
        var AddFlag = true;
        
        if ($('#PRODUCTID').val() == '0') {

            toastr.warning('<b><font color=black>Please select Product..!</font></b>');
            AddFlag = false;
            return false;
        }
        else if ($('#ddlBatch').val() == '0') {

            toastr.warning('<b><font color=black>Please select Batch..!</font></b>');
            AddFlag = false;
            return false;
        }
        else if ($('#JournalQuantity').val() == '') {

            toastr.warning('<b><font color=black>Journal quantity should not be blank..!</font></b>');
            AddFlag = false;
            return false;
        }
        else if (parseInt($('#JournalQuantity').val()) == 0) {

            toastr.warning('<b><font color=black>Journal quantity should not be 0..!</font></b>');
            AddFlag = false;
            return false;
        }
        else if ($('#ReasonID').val().trim() == '0') {
            toastr.warning('<b><font color=black>Please select Reson..!</font></b>');
            AddFlag = false;
            return false;
        }
        if (AddFlag == true) {

            $("#dialog").dialog({
                autoOpen: true,
                modal: true,
                title: "Loading.."
            });
            $("#imgLoader").css("visibility", "visible");
            setTimeout(function () {

                var ProductId = $('#PRODUCTID').val().trim();
                var ProductName = $('#PRODUCTID').find('option:selected').text().trim();
                var BatchNo = $('#hdnbatchno').val().trim();
                var Mrp = $('#MRP').val().trim();
                var PacksizeID = 'B9F29D12-DE94-40F1-A668-C79BF1BF4425';
                var PacksizeName = 'PCS';
                var JournalQty = $('#JournalQuantity').val().trim();
                var ReasonID = $('#ReasonID').val().trim();
                var ReasonName = $('#ReasonID').find('option:selected').text().trim();
                var StorelocationID = $('#StorelocationID').val().trim();
                var StorelocationName = $('#StorelocationID').find('option:selected').text().trim();
                var ToStorelocationID = $('#ToStorelocationID').val().trim();
                var ToStorelocationName = $('#ToStorelocationID').find('option:selected').text().trim();
                var MfgDate = $('#hdn_mfgdate').val().trim();
                var ExpDate = $('#hdn_exprdate').val().trim();

                IsExistsJournalProduct(ProductId, ProductName, BatchNo, Mrp, PacksizeID, PacksizeName, JournalQty,
                    ReasonID, ReasonName, StorelocationID, StorelocationName, ToStorelocationID, ToStorelocationName, MfgDate, ExpDate);

                $("#imgLoader").css("visibility", "hidden");
                $("#dialog").dialog("close");
            }, 3);
            
        }
        e.preventDefault();
    });

    $("#btnsave").click(function (e) {
        debugger;
        entryLockstatus = bindlockdateflag($("#InvoiceDate").val(), $("#hdnFinYear").val());
        if (entryLockstatus == '0') {
            toastr.warning('<b><font color=black>Entry date is locked,please contact with admin...!</font></b>');
            return false;
        }
        else {
            debugger;
            var itemrowCount = document.getElementById("JournalDetailsGrid").rows.length - 1;

            if (itemrowCount <= 0) {
                toastr.warning('<b><font color=black>Please add Product..!</font></b>');
                return false;
            }
            else {
                SaveStockJounal();
            }
        }
        e.preventDefault();
    });
    
    
});

$(function () {

    var invoiceid;
    var dispatchid;

    /*Delete function for Journal details grid*/
    $("body").on("click", "#JournalDetailsGrid #btnJournalDetailsdelete", function () {
        debugger;
        var productid;
        var productid2;
        var rowCount;
        var deleteflag = 0;
       
        var rowCount;
        var deleteflag = 0;

        var row = $(this).closest("tr");
        productid = row.find('td:eq(1)').html().trim();
        //batchno = row.find('td:eq(3)').html().trim();

        if (confirm("Do you want to delete this item?")) {

            $('#JournalDetailsGrid tbody tr').each(function () {
                var row2 = $(this).closest("tr");
                productid2 = $(this).find('td:eq(1)').html().trim();

                if (productid == productid2) {
                    deleteflag = 1;
                    row2.remove();
                    JournalDetailsRowCount();
                }
            });
            rowCount = document.getElementById("JournalDetailsGrid").rows.length - 1;
            if (rowCount <= 0) {
                $("#JournalDetailsGrid").empty();
            }

        }
    });

    /*View function for Conversion*/
    $("body").on("click", "#StockJournalGrid .gvJournalView", function () {
        var row = $(this).closest("tr");
        invoiceid = row.find('td:eq(1)').html();
        $('#hdndispatchID').val(invoiceid);

        $("#dialog").dialog({
            autoOpen: true,
            modal: true,
            title: "Loading.."
        });
        $("#imgLoader").css("visibility", "visible");
        setTimeout(function () {

            $('#JournalDetailsGrid').empty();
            EditDetails($('#hdndispatchID').val().trim());


            $('#btnsave').css("display", "");
            $('#btnAddnew').css("display", "");
           

            $("#dvAdd").find("input, textarea, select,submit").attr("disabled", "disabled");
            $('#divTransferNo').css("display", "");
            $("#btnclose").prop("disabled", false);
            $('#dvAdd').css("display", "");
            $('#dvDisplay').css("display", "none");

            $("#BRID").chosen('destroy');
            $("#BRID").chosen({
                search_contains: true
            });
            $("#BRID").trigger("chosen:updated");

            $("#StorelocationID").val('0');
            $("#StorelocationID").chosen('destroy');
            $("#StorelocationID").chosen({
                search_contains: true
            });
            $("#StorelocationID").trigger("chosen:updated");
            

            $("#ToStorelocationID").val('0');
            $("#ToStorelocationID").chosen('destroy');
            $("#ToStorelocationID").chosen({
                search_contains: true
            });
            $("#ToStorelocationID").trigger("chosen:updated");   

            $("#PRODUCTID").val('0');
            $("#PRODUCTID").chosen('destroy');
            $("#PRODUCTID").chosen({
                search_contains: true
            });
            $("#PRODUCTID").trigger("chosen:updated");

            
            $("#PSID").chosen('destroy');
            $("#PSID").chosen({
                search_contains: true
            });
            $("#PSID").trigger("chosen:updated");

            $("#ReasonID").val('0');
            $("#ReasonID").chosen('destroy');
            $("#ReasonID").chosen({
                search_contains: true
            });
            $("#ReasonID").trigger("chosen:updated");

            $('#ddlBatch').empty();
            $("#InvoiceDate").datepicker("destroy");
            $("#JournalQuantity").val('');
            $("#StockQty").val('');
            $("#MRP").val('');


            $("#imgLoader").css("visibility", "hidden");
            $("#dialog").dialog("close");
        }, 3);
    });

    /*Print Tax Invoice function for Invoice*/
    $("body").on("click", "#StockJournalGrid .gvJournalPrint", function () {
        var row = $(this).closest("tr");
        invoiceid = row.find('td:eq(1)').html();
        $('#hdndispatchID').val(invoiceid);
        $("#dialog").dialog({
            autoOpen: true,
            modal: true,
            title: "Loading.."
        });
        $("#imgLoader").css("visibility", "visible");
        setTimeout(function () {

            var url = "http://mcnroeerp.com/mcworld/VIEW/frmRptInvoicePrint.aspx?StkJrnl_ID=" + invoiceid;
            window.open(url, "Archive", "channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100");

            $("#imgLoader").css("visibility", "hidden");
            $("#dialog").dialog("close");
        }, 3);

    })
});


function loadAddNewRecord() {
    debugger;
    finyearChecking($("#hdnFinYear").val().trim());
    $("#dialog").dialog({
        autoOpen: true,
        modal: true,
        title: "Loading.."
    });
    $("#imgLoader").css("visibility", "visible");
    setTimeout(function () {
        $('#dvAdd').css("display", "");
        $('#dvDisplay').css("display", "none");
        ClearControls();
        $("#imgLoader").css("visibility", "hidden");
        $("#dialog").dialog("close");
    }, 3);
    
}

/*Add Product Start*/
function IsExistsJournalProduct(ProductId, ProductName, BatchNo, Mrp, PacksizeID, PacksizeName, JournalQty,
    ReasonID, ReasonName, StorelocationID, StorelocationName, ToStorelocationID, ToStorelocationName, MfgDate, ExpDate) {
    debugger;
    if ($('#JournalDetailsGrid').length) {
        var exists = false;
        var arraydetails = [];
        $('#JournalDetailsGrid tbody tr').each(function () {
            var dispatchdetail = {};
            var inputproductgrd = $(this).find('td:eq(1)').html().trim();
            var inputbatchgrd = $(this).find('td:eq(3)').html().trim();
            var inputreasongrd = $(this).find('td:eq(8)').html().trim();
            dispatchdetail.INPUTPRODUCT = inputproductgrd;
            dispatchdetail.INPUTBATCH = inputbatchgrd;
            dispatchdetail.INPUTREASON = inputreasongrd;
            arraydetails.push(dispatchdetail);
        });
        var jsondispatchobj = {};
        jsondispatchobj.conversionproductDetails = arraydetails;

        for (i = 0; i < jsondispatchobj.conversionproductDetails.length; i++) {
            if (jsondispatchobj.conversionproductDetails[i].INPUTPRODUCT.trim() == ProductId.trim() && jsondispatchobj.conversionproductDetails[i].INPUTBATCH.trim() == BatchNo.trim() && jsondispatchobj.conversionproductDetails[i].INPUTREASON.trim() == ReasonID.trim()) {
                exists = true;
                break;
            }
        }
        if (exists != false) {
            toastr.error('Item already exists...!');
            return false;
        }
        else {
            addJournalFinalGrid(ProductId, ProductName, BatchNo, Mrp, PacksizeID, PacksizeName, parseInt(JournalQty * -1),
                ReasonID, ReasonName, StorelocationID, StorelocationName,MfgDate, ExpDate);

            addJournalFinalGrid(ProductId, ProductName, BatchNo, Mrp, PacksizeID, PacksizeName, JournalQty,
                ReasonID, ReasonName, ToStorelocationID, ToStorelocationName,MfgDate, ExpDate);
           
        }
    }
}

function addJournalFinalGrid(ProductId, ProductName, BatchNo, Mrp, PacksizeID,PacksizeName,JournalQty,
    ReasonID, ReasonName, StorelocationID, StorelocationName,MfgDate, ExpDate) {
    //debugger;
    //Create Table 
    var tr;
    tr = $('#JournalDetailsGrid');
    var HeaderCount = $('#JournalDetailsGrid thead th').length;
   
    if (HeaderCount == 0) {
        tr.append("<thead><tr><th style='text-align: center;'>Sl.No.</th><th style='display: none;'>Product ID</th><th>Product</th><th>Batch</th><th>MRP</th><th style='display: none;'>PacksizeID</th><th>Packsize</th><th>Journal Qty</th><th style='display: none;'>ReasonID</th><th>Reason</th><th style='display: none;'>StorelocationID</th><th>Storelocation</th><th>Mfg.Date</th><th>Exp.Date</th><th style='display: none;'>Weight</th><th style='display: none;'>Buffer Qty</th><th style='text-align: center;'>Delete</th></tr ></thead><tbody>");
        
    }
   
    tr = $('<tr/>');
    tr.append("<td style='text-align: center;'><label class='slno' id='lbljournalslno'></label></td>");//0
    tr.append("<td style='display: none;'>" + ProductId.toString().trim() + "</td>");//1
    tr.append("<td style='width:250px;'>" + ProductName.toString().trim() + "</td>");//2
    tr.append("<td >" + BatchNo.toString().trim() + "</td>");//3
    tr.append("<td style='text-align: right;'>" + Mrp.toString().trim() + "</td>");//4
    tr.append("<td style='display: none;'>" + PacksizeID.toString().trim() + "</td>");//5
    tr.append("<td >" + PacksizeName.toString().trim() + "</td>");//6
    tr.append("<td style='text-align: right;'>" + parseInt(JournalQty) + "</td>");//7
    tr.append("<td style='display: none;'>" + ReasonID.toString().trim() + "</td>");//8
    tr.append("<td >" + ReasonName.toString().trim() + "</td>");//9
    tr.append("<td style='display: none;'>" + StorelocationID.toString().trim() + "</td>");//10
    tr.append("<td >" + StorelocationName.toString().trim() + "</td>");//11
    tr.append("<td >" + MfgDate.toString().trim() + "</td>");//12
    tr.append("<td >" + ExpDate.toString().trim() + "</td>");//13
    tr.append("<td style='display: none;'>" + '0 ML' + "</td>");//14
    tr.append("<td style='text-align: right;display: none;'>" + parseInt(JournalQty) + "</td>");//15
    tr.append("<td style='text-align: center'><input type='image' class='gvJournalDetailsDelete'  id='btnJournalDetailsdelete' <img src='../Images/ico_delete_16.png' title='Delete'/></input></td>")//16;
    $("#JournalDetailsGrid").append(tr);
    tr.append("</tbody>");
    
    JournalDetailsRowCount();
}

function JournalDetailsRowCount() {
    debugger;
    var cnvdetailsrowCount = document.getElementById("JournalDetailsGrid").rows.length - 1;
    var finalcnvcount = 0;
    if (cnvdetailsrowCount > 0) {       

        $('#ddlBatch').empty();
        $('#JournalQuantity').val('');
        $('#StockQty').val('');
        $('#MRP').val('');

        $('#PRODUCTID').val('0');
        $("#PRODUCTID").chosen({
            search_contains: true
        });
        $("#PRODUCTID").trigger("chosen:updated");
        

        $('#JournalDetailsGrid tbody tr').each(function () {
            finalcnvcount = finalcnvcount + 1;
            $(this).find('#lbljournalslno').text(finalcnvcount.toString().trim());
        })
    }
}


/*Add Product End*/


function finyearChecking(finyear) {

    ////debugger;
    //fin yr check
    var currentdt;
    var frmdate;
    var todate;
    $.ajax({
        type: "POST",
        url: "/TranDepot/finyrchk",
        data: { FinYear: finyear },
        async: false,
        dataType: "json",
        success: function (response) {
            ////debugger;
            $.each(response, function () {
                debugger;
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
    $("#InvoiceDate").datepicker({
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        selectCurrent: true,
        todayBtn: "linked",
        showAnim: "slideDown",
        yearRange: "-3:+0",
        dateFormat: "dd/mm/yy",
        showOn: 'button',
        buttonText: 'Show Date',
        buttonImageOnly: true,
        minDate: new Date(currentdt),
        maxDate: new Date(currentdt),
        buttonImage: 'http://jqueryui.com/resources/demos/datepicker/images/calendar.gif'
    });
    $(".ui-datepicker-trigger").mouseover(function () {
        $(this).css('cursor', 'pointer');
    });

    $("#BatchFromDate").datepicker({
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        selectCurrent: true,
        todayBtn: "linked",
        showAnim: "slideDown",
        yearRange: "-5:+0",
        /*maxDate: new Date(currentdt),
        minDate: new Date(frmdate),*/
        dateFormat: "dd/mm/yy",
        showOn: 'button',
        buttonText: 'Show Date',
        buttonImageOnly: true,
        buttonImage: 'http://jqueryui.com/resources/demos/datepicker/images/calendar.gif'
    });
    $(".ui-datepicker-trigger").mouseover(function () {
        $(this).css('cursor', 'pointer');
    });
    $("#BatchToDate").datepicker({
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        selectCurrent: true,
        todayBtn: "linked",
        showAnim: "slideDown",
        yearRange: "-5:+0",
        /*maxDate: new Date(currentdt),
        minDate: new Date(frmdate),*/
        dateFormat: "dd/mm/yy",
        showOn: 'button',
        buttonText: 'Show Date',
        buttonImageOnly: true,
        buttonImage: 'http://jqueryui.com/resources/demos/datepicker/images/calendar.gif'
    });
    $(".ui-datepicker-trigger").mouseover(function () {
        $(this).css('cursor', 'pointer');
    });


    $("#txtfrmdate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
    $("#txttodate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
    $("#InvoiceDate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());

    /*$("#BatchFromDate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
    $("#BatchToDate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());*/
}

function getCurrentDate() {
    today_date = new Date();
    today_Date_Str = ((today_date.getDate() < 10) ? "0" : "") + String(today_date.getDate()) + "/" + ((today_date.getMonth() < 9) ? "0" : "") + String(today_date.getMonth() + 1) + "/" + today_date.getFullYear();
    return today_Date_Str;
}

function repeatstr(ch, n) {
    var result = "&nbsp;";
    while (n-- > 0)
        result += ch;
    return result;
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

function bindsourceDepot(userid) {
    ////debugger;
    var SourceDepot = $("#BRID");
    var SearchDepot = $("#SearchDepotID");
    var depotlength = 0;
    $.ajax({
        type: "POST",
        url: "/TranDepot/GetDepot",
        data: { UserID: userid},
        async: false,
        dataType: "json",
        success: function (response) {
            ////debugger;
            depotlength = response.length;
            SourceDepot.empty();
            $.each(response, function () {
                SourceDepot.append($("<option></option>").val(this['BRID']).html(this['BRNAME']));
                SearchDepot.append($("<option></option>").val(this['BRID']).html(this['BRNAME']));
            });
            if ($("#hdnTPU").val() == 'D' || $("#hdnTPU").val() == 'EXPU') {
                ////debugger;
                $("#BRID").val($("#hdnDepotID").val().trim());
                $("#SearchDepotID").val($("#hdnDepotID").val().trim());
            }
            else {
                ////debugger;
                if (depotlength == 1) {
                    $("#BRID").val(response[0].BRID.trim());
                    $("#SearchDepotID").val(response[0].BRID.trim());
                }
                else if (depotlength > 1) {
                    if ($("#hdnDepotID").val().trim() == $("#hdnUserID").val().trim()) {
                        $("#BRID").val('0EEDDA49-C3AB-416A-8A44-0B9DFECD6670');/*Kolkata*/
                        $("#SearchDepotID").val('0EEDDA49-C3AB-416A-8A44-0B9DFECD6670');/*Kolkata*/
                    }
                }
            }
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function bindStorelocation() {
    //debugger;
    var Storelocation = $("#StorelocationID");
    var ToStorelocation = $("#ToStorelocationID");
    $.ajax({
        type: "POST",
        url: "/TranDepot/GetStorelocation",
        data: '{}',
        async: false,
        dataType: "json",
        success: function (response) {
            //debugger;
            Storelocation.empty().append('<option selected="selected" value="0">Please select</option>');
            ToStorelocation.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(response, function () {
                Storelocation.append($("<option></option>").val(this['STORELOCATIONID']).html(this['STORELOCATIONNAME']));
                ToStorelocation.append($("<option></option>").val(this['STORELOCATIONID']).html(this['STORELOCATIONNAME']));
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

function bindReason(menuID) {
    //debugger;
    var Reason = $("#ReasonID");
    $.ajax({
        type: "POST",
        url: "/TranDepot/GetReason",
        data: { MenuID: menuID },
        async: false,
        dataType: "json",
        success: function (response) {
            //debugger;
            Reason.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(response, function () {
                Reason.append($("<option></option>").val(this['REASONID']).html(this['REASONNAME']));

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

function bindPacksize() {
    //debugger;
    var Packsize = $("#PSID");
    $.ajax({
        type: "POST",
        url: "/TranDepot/GetJournalPacksize",
        data: '{}',
        async: false,
        dataType: "json",
        success: function (response) {
            //debugger;
            Packsize.empty();
            $.each(response, function () {
                Packsize.append($("<option></option>").val(this['PACKSIZEID_FROM']).html(this['PACKSIZENAME_FROM']));

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

function bindproduct(depotid) {
    debugger;
    var Product = $("#PRODUCTID");
    $.ajax({
        type: "POST",
        url: "/TranDepot/GetInterStoreLocationProduct",
        data: { DepotID: depotid},
        async: false,
        dataType: "json",
        success: function (response) {
            //debugger;
            Product.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(response, function () {
                Product.append($("<option></option>").val(this['PRODUCTID']).html(this['PRODUCTNAME']));
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

function bindFGbatchno() {
    debugger;
    var ddlBatch = $("#ddlBatch");
    var Batch = '0';
    var desc = '';
    var desc1 = '';
    var val1 = '';
    var listItems = '';
    listItems = "<option value='0'>Select Batch</option>";
    desc = 'Stock Qty'.padEnd(12, '\u00A0') + repeatstr("&nbsp;", 8) + 'MRP'.padEnd(7, '\u00A0') + repeatstr("&nbsp;", 8) + 'Mfg.Date'.padEnd(8, '\u00A0') + repeatstr("&nbsp;", 8) + 'Exp.Date'.padEnd(8, '\u00A0') + repeatstr("&nbsp;", 8) + 'Batch'.padEnd(15, '\u00A0');
    listItems += "<option value='0'>" + desc + "</option>";
    
    $.ajax({
        type: "POST",
        url: "/TranDepot/GetJournalBatchDetails",
        data: { DepotID: $('#BRID').val(), ProductID: $('#PRODUCTID').val(), PacksizeID: $('#PSID').val(), BatchNo: Batch, StorelocationID: $('#StorelocationID').val() },
        dataType: "json",
        success: function (batchlist) {

            debugger;
            var counter = 0;
            $('#ddlBatch').empty();
            $.each(batchlist, function () {
                val1 = this["BATCHNO"] + "|" + this["BatchSTOCKQTY"] + "|" + this["BatchMRP"] + "|" + this["BatchMFGDATE"] + "|" + this["BatchEXPIRDATE"];
                desc1 = this["BatchSTOCKQTY"].padEnd(12, '\u00A0') + repeatstr("&nbsp;", 8) + this["BatchMRP"].padEnd(7, '\u00A0') + repeatstr("&nbsp;", 8) + this["BatchMFGDATE"].padEnd(8, '\u00A0') + repeatstr("&nbsp;", 6) + this["BatchEXPIRDATE"].padEnd(8, '\u00A0') + repeatstr("&nbsp;", 6) + this["BATCHNO"].padEnd(15, '\u00A0')
                listItems += "<option value='" + val1 + "'>" + desc1 + "</option>";
                counter = counter + 1;
            });
            ddlBatch.append(listItems);
            debugger;
            if (counter <= 0) {
                toastr.warning('<b>Stock not available...!</b>');
                return false;
            }
        },
        failure: function (batchlist) {
            alert(batchlist.responseText);
        },
        error: function (batchlist) {
            alert(batchlist.responseText);
        }
    });
}

function getBatchDetails() {
    debugger;    
    var strval2 = $("#ddlBatch").val();
    var splitval = strval2.split('|');
    var productid = $("#PRODUCTID").val();
    var assessmentpercent = '0';
    var rate = '0';
    var batchno = '';
    var stockqty = '';
    var mrp = '';
    var mfgdate = '';
    var expirydate = '';

    
    batchno = splitval[0];
    stockqty = splitval[1];
    mrp = splitval[2];
    mfgdate = splitval[3];
    expirydate = splitval[4];

    $("#hdnbatchno").val(batchno);
    $("#StockQty").val(parseInt(stockqty));
    $("#MRP").val(mrp);
    $("#hdn_mfgdate").val(mfgdate);
    $("#hdn_exprdate").val(expirydate);
    
    $("#JournalQuantity").focus();   
}

function RowCount() {
    ////debugger;
    var rowCount = document.getElementById("productDetailsGrid").rows.length - 2;
    var count=0;
    if (rowCount > 0) {
        $("#CUSTOMERID").prop("disabled", true);
        $("#CUSTOMERID").chosen({
            search_contains: true
        });
        $("#CUSTOMERID").trigger("chosen:updated");
        $('#productDetailsGrid tbody tr').each(function () {
            count = count + 1;
            $(this).find('#lblslno').text(count.toString().trim());
        })
    }
}

function RowCountEdit() {
    var table = document.getElementById("productDetailsGrid");
    var rowCount = document.getElementById("productDetailsGrid").rows.length - 2;
    var count = 0;
    if (rowCount > 0) {
        $('#productDetailsGrid tbody tr').each(function () {
            count = count + 1;
            $(this).find('#lblslno').text(count.toString().trim());
        })
    }
}

function RowCountJournalList() {
    var table = document.getElementById("StockJournalGrid");
    var rowCount = document.getElementById("StockJournalGrid").rows.length - 1;
    if (rowCount > 0) {
        for (var i = 1; i <= rowCount; i++) {
            table.rows[i].cells[0].innerHTML = i.toString();
        }
    }
}

function ClearControls() {
    $("#dvAdd").find("input, textarea, select,submit").removeAttr("disabled");
    
    $('#btnsave').css("display", "");
    $('#btnAddnew').css("display", "");
    
    $('#divTransferNo').css("display", "none");
    $("#btnadd").prop("disabled", false);
    $("#TransferNo").attr("disabled", "disabled");
    
    $("#InvoiceDate").attr("disabled", "disabled");
    $("#BatchFromDate").attr("disabled", "disabled");
    $("#BatchToDate").attr("disabled", "disabled");
    
    $("#StockQty").attr("disabled", "disabled");
    $("#MRP").attr("disabled", "disabled");

    $("#BRID").attr("disabled", "disabled");
    $("#BRID").chosen('destroy');
    $("#BRID").chosen({
        search_contains: true
    });
    $("#BRID").trigger("chosen:updated");

    $("#SearchDepotID").chosen({
        search_contains: true
    });
    $("#SearchDepotID").trigger("chosen:updated");
    //debugger;

    $("#StorelocationID").val('0');
    $("#StorelocationID").removeAttr("disabled");
    $("#StorelocationID").chosen({
        search_contains: true
    });
    $("#StorelocationID").trigger("chosen:updated");

    $("#ToStorelocationID").val('0');
    $("#ToStorelocationID").removeAttr("disabled");
    $("#ToStorelocationID").chosen({
        search_contains: true
    });
    $("#ToStorelocationID").trigger("chosen:updated");

    $("#PRODUCTID").val('0');
    $("#PRODUCTID").removeAttr("disabled");
    $("#PRODUCTID").chosen({
        search_contains: true
    });
    $("#PRODUCTID").trigger("chosen:updated");

    $("#ReasonID").val('0');
    $("#ReasonID").removeAttr("disabled");
    $("#ReasonID").chosen({
        search_contains: true
    });
    $("#ReasonID").trigger("chosen:updated");

    $('#BatchFromDate').val('');
    $('#BatchToDate').val('');

    $('#ddlBatch').empty();

    
    $("#PSID").chosen({
        search_contains: true
    });
    $("#PSID").trigger("chosen:updated");
    
    $('#Remarks').val('');
    $('#JournalDetailsGrid').empty();
    $('#hdndispatchID').val('0');
    
    
}

function availableStock(depotid,productid, batch, mrp, mfgdate, expdate, storelocationid) {
    var returnvalue = null;
    $.ajax({
        type: "POST",
        url: "/TranDepot/GetAvailableStock",
        data: { DepotID: depotid.trim(), Productid: productid.trim(), Batch: batch.trim(), MRP: mrp.trim(), MfgDate: mfgdate.trim(), ExpDate: expdate.trim(), StorelocationID: storelocationid.trim() },
        dataType: "json",
        async: false,
        success: function (stock) {
            var availablestockqty;
            $.each(stock, function (key, item) {
                ////debugger;
                availablestockqty = item.AVAILABLE_STOCK;
                returnvalue = availablestockqty;
                return false;
            });
        },
        failure: function (stock) {
            alert(stock.responseText);
        },
        error: function (stock) {
            stock(qtypcs.responseText);
        }
    });
    return returnvalue;
}

function SaveStockJounal() {
    debugger;
    var StockFlag = true;
    var rowCount = 0;
    rowCount = document.getElementById("JournalDetailsGrid").rows.length - 1;

    /*Stock Checking Start*/
    if (rowCount > 0) {

        /*Journal Details Grid Loop Start*/
        $('#JournalDetailsGrid tbody tr').each(function () {
            debugger;
            var journalProductid = $(this).find('td:eq(1)').html().trim();
            var journalProduct = $(this).find('td:eq(2)').html().trim();
            var journalBatch = $(this).find('td:eq(3)').html().trim();
            var journalMrp = $(this).find('td:eq(4)').html().trim();
            var journalQty = $(this).find('td:eq(7)').html().trim();
            var journalStorelocation = $(this).find('td:eq(10)').html().trim();
            var journalMfgDate = $(this).find('td:eq(12)').html().trim();
            var journalExpDate = $(this).find('td:eq(13)').html().trim();
            debugger;
            
            var AvailableStockQty = 0;

            if (parseInt(journalQty) < 0) {
                if ($('#hdndispatchID').val().trim() == '0') {
                    
                    debugger;
                    AvailableStockQty = availableStock($('#BRID').val().trim(), journalProductid.trim(), journalBatch.trim(),
                        journalMrp.trim(), journalMfgDate.trim(), journalExpDate.trim(), journalStorelocation.trim());

                }
                else if ($('#hdndispatchID').val().trim() != '0') {

                    AvailableStockQty = availableStock($('#BRID').val().trim(), journalProductid.trim(), journalBatch.trim(),
                        journalMrp.trim(), journalMfgDate.trim(), journalExpDate.trim(), journalStorelocation.trim());
                    
                    AvailableStockQty = parseInt(AvailableStockQty) + parseInt(journalQty);
                    
                }
                debugger;
                if (parseInt(-1 * journalQty) > parseInt(AvailableStockQty)) {
                    toastr.error('Stock not available for <b>' + journalProduct + '</b> in batch - <b>' + journalBatch + '</b>');
                    StockFlag = false;
                    return false;
                }
            }
            
        });
        /*Billing Grid Loop End*/
    }
    /* Stock Checking End*/

    if (StockFlag == true) {


        $("#dialog").dialog({
            autoOpen: true,
            modal: true,
            title: "Loading.."
        });
        $("#imgLoader").css("visibility", "visible");
        setTimeout(function () {

            var i = 0;
            stockjournallist = new Array();
            var stockjournalsave = {};

            debugger;
            stockjournalsave.FGInvoiceID = $('#hdndispatchID').val().trim();

            if ($('#hdndispatchID').val() == '0') {
                stockjournalsave.FLAG = 'A';
            }
            else {
                stockjournalsave.FLAG = 'U';
            }
            stockjournalsave.InvoiceDate = $("#InvoiceDate").val().trim();
            stockjournalsave.BRID = $("#BRID").val().trim();
            stockjournalsave.BRNAME = $("#BRID option:selected").text().trim();
            stockjournalsave.UserID = $("#hdnUserID").val().trim();
            stockjournalsave.FinYear = $("#hdnFinYear").val().trim();
            if ($("#Remarks").val().trim() == '') {
                stockjournalsave.Remarks = '';
            }
            else {
                stockjournalsave.Remarks = $("#Remarks").val().trim();
            }
            stockjournalsave.AdjustmentType = 'A';
            stockjournalsave.AdjustmentMenu = 'StockAdjustment';

            var count = 0;
            $('#JournalDetailsGrid tbody tr').each(function () {
                debugger;
                count = count + 1;
                var stockjournaldetails = {};

                var productid = $(this).find('td:eq(1)').html().trim();
                var productname = $(this).find('td:eq(2)').html().trim();
                var batchno = $(this).find('td:eq(3)').html().trim();
                var price = 0;
                var adjustmentqty = $(this).find('td:eq(7)').html().trim();
                var packingsizeid = $(this).find('td:eq(5)').html().trim();
                var packingsizename = $(this).find('td:eq(6)').html().trim();
                var reasonid = $(this).find('td:eq(8)').html().trim();
                var reasonname = $(this).find('td:eq(9)').html().trim();
                var storelocationid = $(this).find('td:eq(10)').html().trim();
                var storelocationname = $(this).find('td:eq(11)').html().trim();
                var mfdate = $(this).find('td:eq(12)').html().trim();
                var exprdate = $(this).find('td:eq(13)').html().trim();
                var mrp = $(this).find('td:eq(4)').html().trim();
                var weight = $(this).find('td:eq(14)').html().trim();
                var amount = 0;
                var bufferqty = $(this).find('td:eq(7)').html().trim();
                var approved = 'N';

                stockjournaldetails.PRODUCTID = productid;
                stockjournaldetails.PRODUCTNAME = productname;
                stockjournaldetails.BATCHNO = batchno;
                stockjournaldetails.PRICE = price;
                stockjournaldetails.ADJUSTMENTQTY = adjustmentqty;
                stockjournaldetails.PACKINGSIZEID = packingsizeid;
                stockjournaldetails.PACKINGSIZENAME = packingsizename;
                stockjournaldetails.REASONID = reasonid;
                stockjournaldetails.REASONNAME = reasonname;
                stockjournaldetails.STORELOCATIONID = storelocationid;
                stockjournaldetails.STORELOCATIONNAME = storelocationname;
                stockjournaldetails.MFDATE = mfdate;
                stockjournaldetails.EXPRDATE = exprdate;
                stockjournaldetails.MRP = mrp;
                stockjournaldetails.WEIGHT = weight;
                stockjournaldetails.AMOUNT = amount;
                stockjournaldetails.BUFFERQTY = bufferqty;
                stockjournaldetails.APPROVED = approved;
                stockjournallist[i++] = stockjournaldetails;
            });

            stockjournalsave.StockAdjustmentDetails = stockjournallist;

            //alert(JSON.stringify(stockjournalsave));

            $.ajax({
                url: "/TranDepot/stockjournalsavedata",
                data: '{stockjournalsave:' + JSON.stringify(stockjournalsave) + '}',
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
                        bindstockjournalgrid();
                    }
                    else if (messageid == '2') {
                        $('#dvAdd').css("display", "none");
                        $('#dvDisplay').css("display", "");
                        $("#txtfrmdate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
                        $("#txttodate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
                        ClearControls();
                        bindstockjournalgrid();

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

            $("#imgLoader").css("visibility", "hidden");
            $("#dialog").dialog("close");
        }, 3);
    }
       
   
}

function bindstockjournalgrid() {
    var srl = 0;
    srl = srl + 1; 
    $("#dialog").dialog({
        autoOpen: true,
        modal: true,
        title: "Loading.."
    });
    $("#imgLoader").css("visibility", "visible");
    $.ajax({
        type: "POST",
        url: "/TranDepot/BindStockJournalGrid",
        data: { FromDate: $('#txtfrmdate').val().trim(), ToDate: $('#txttodate').val().trim(), DepotID: $('#BRID').val().trim(), FinYear: $('#hdnFinYear').val().trim()},
        dataType: "json",
        success: function (response) {
            var tr;
            tr = $('#StockJournalGrid');
            tr.append("<thead><tr><th>Sl. No.</th><th style='display: none'>ADJUSTMENTID</th><th>Journal Date</th><th>Journal No</th><th>Depot</th><th>Journal Type</th><th>Entry User</th><th>Dayend Status</th><th>Approval Status</th><th>View</th>");
            
            $('#StockJournalGrid').DataTable().destroy();
            $("#StockJournalGrid tbody tr").remove();
            for (var i = 0; i < response.length; i++) {
                tr = $('<tr/>');
                tr.append("<td style='text-align: center'>" + srl + "</td>");//0
                tr.append("<td style='display: none'>" + response[i].ADJUSTMENTID + "</td>");//1
                tr.append("<td>" + response[i].ADJUSTMENTDATE1 + "</td>");//2
                tr.append("<td>" + response[i].ADJUSTMENTNO + "</td>");//3
                tr.append("<td >" + response[i].DEPOTNAME + "</td>");//4
                tr.append("<td >" + response[i].ADJUSTMENT_FROMMENU + "</td>");//5
                tr.append("<td >" + response[i].USERNAME + "</td>");//6
                if (response[i].DAYENDTAG.trim() == 'Done') {
                    tr.append("<td style='text-align: center'><font color='0x00B300'>" + response[i].DAYENDTAG + "</font></td>");//7
                }
                else {
                    tr.append("<td style='text-align: center'><font color='0x5184FF'>" + response[i].DAYENDTAG + "</font></td>");//7
                }
                if (response[i].APPROVED_STATUS.trim() == 'Approved') {
                    tr.append("<td style='text-align: center'><font color='0x00B300'>" + response[i].APPROVED_STATUS + "</font></td>");//8
                }
                else if (response[i].APPROVED_STATUS.trim() == 'Pending') {
                    tr.append("<td style='text-align: center'><font color='0x5184FF'>" + response[i].APPROVED_STATUS + "</font></td>");//8
                }
                else {
                    tr.append("<td style='text-align: center'><font color='#ff2500'>" + response[i].APPROVED_STATUS + "</font></td>");//8
                }
                tr.append("<td style='text-align: center'><input type='image' class='gvJournalView'   id='btnjournalview'   <img src='../Images/View.png' width='20' height ='20' title='View'/></input></td>");//9
                
                
                $("#StockJournalGrid").append(tr);
            }
            tr.append("</tbody>");
            RowCountJournalList();
            $('#StockJournalGrid').DataTable({
                "sScrollX": '100%',
                "sScrollXInner": "110%",
                "scrollY": "238px",
                "scrollCollapse": true,
                "dom": 'Bfrtip',
                buttons: [
                    {
                        extend: 'excelHtml5',
                        title: 'Inter Store Location List'
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
            $("#imgLoader").css("visibility", "hidden");
            $("#dialog").dialog("close");
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function bindbackdateflag(menuid) {
    var returnbackdateflag = null;
    $.ajax({
        type: "POST",
        url: "/tranfac/GetBackDateChecking",
        data: { MenuID: menuid.trim() },
        dataType: "json",
        async: false,
        success: function (backdate) {
            //alert(JSON.stringify(shipping));
            var flag;
            $.each(backdate, function (key, item) {
                flag = item.BACKDATEFLAG;
                returnbackdateflag = flag;
                return false;
            });
        },
        failure: function (backdate) {
            alert(backdate.responseText);
        },
        error: function (backdate) {
            alert(backdate.responseText);
        }
    });
    return returnbackdateflag;
}

function bindlockdateflag(entrydate,finyear) {
    var returnlockdateflag = null;
    $.ajax({
        type: "POST",
        url: "/TranDepot/GetEntryLockChecking",
        data: { EntryDate: entrydate.trim(), Finyear: finyear.trim()},
        dataType: "json",
        async: false,
        success: function (lockdate) {
            //alert(JSON.stringify(shipping));
            var flag;
            $.each(lockdate, function (key, item) {
                flag = item.LOCK_FLAG;
                returnlockdateflag = flag;
                return false;
            });
        },
        failure: function (lockdate) {
            alert(lockdate.responseText);
        },
        error: function (lockdate) {
            alert(lockdate.responseText);
        }
    });
    return returnlockdateflag;
}

function JournalProductType(productid) {
    var returnValue = null;
    $.ajax({
        type: "POST",
        url: "/TranDepot/JournalProductType",
        data: { ProductID: productid },
        dataType: "json",
        async: false,
        success: function (responseMessage) {
            var producttype;
            
            $.each(responseMessage, function (key, item) {
                producttype = item.TYPE;
                returnValue = producttype;
                return false;
            });
        }
    });
    return returnValue;
}

function Batchexists(productid,batch,mrp) {
    var returnValue = null;
    $.ajax({
        type: "POST",
        url: "/TranDepot/BatchExists",
        data: { ProductID: productid, BatchNo: batch, MRP: mrp },
        dataType: "json",
        async: false,
        success: function (responseMessage) {
            var exists;

            $.each(responseMessage, function (key, item) {
                exists = item.BATCH_EXISTS;
                returnValue = exists;
                return false;
            });
        }
    });
    return returnValue;
}

function BatchCountExists(productid, batch, mrp) {
    var returnValue = null;
    $.ajax({
        type: "POST",
        url: "/TranDepot/BatchCountExists",
        data: { ProductID: productid, BatchNo: batch, MRP: mrp },
        dataType: "json",
        async: false,
        success: function (responseMessage) {
            var countexists;

            $.each(responseMessage, function (key, item) {
                countexists = item.COUNT_EXISTS;
                returnValue = countexists;
                return false;
            });
        }
    });
    return returnValue;
}

function DayendFlag(invoiceid) {
    var returnValue = null;
    $.ajax({
        type: "POST",
        url: "/TranDepot/GetInvoiceStatus",
        data: { InvoiceID: invoiceid, ModuleID: '7', Type: '2' },
        dataType: "json",
        async: false,
        success: function (responseMessage) {
            var messageid;
            var messagetext;
            $.each(responseMessage, function (key, item) {
                messageid = item.MessageID;
                messagetext = item.MessageText;
                if (messageid == '1') {
                    returnValue = messagetext;
                }
                else {
                    returnValue = 'na';
                }
                return false;
            });

        }
    });
    return returnValue;
}

function OfflineStatus(depotid) {
    var returnValue = null;
    $.ajax({
        type: "POST",
        url: "/TranDepot/OfflineStatus",
        data: { DepotID: depotid},
        dataType: "json",
        async: false,
        success: function (response) {
            var status;
            $.each(response, function (key, item) {
                status = item.OFFLINE_STATUS;
                returnValue = status;
                
                return false;
            });

        }
    });
    return returnValue;
}

function OfflineTag() {
    var returnValue = null;
    $.ajax({
        type: "POST",
        url: "/TranDepot/OfflineTag",
        data: '{}',
        dataType: "json",
        async: false,
        success: function (response) {
            var status;
            $.each(response, function (key, item) {
                status = item.OFFLINE;
                returnValue = status;
                return false;
            });

        }
    });
    return returnValue;
}

function DayEndStatus(depotid, bsid, invoicedate,userid) {
    var returnValue = null;
    $.ajax({
        type: "POST",
        url: "/TranDepot/GetDayEndStatus",
        data: { DepotID: depotid, BSID: bsid, InvoiceDate: invoicedate, UserID: userid },
        dataType: "json",
        async: false,
        success: function (responseMessage) {
            var messageid;
            var messagetext;
            $.each(responseMessage, function (key, item) {
                messageid = item.MessageID;
                messagetext = item.MessageText;
                if (messageid == '1') {
                    returnValue = messagetext;
                }
                else {
                    returnValue = 'na';
                }
                return false;
            });

        }
    });
    return returnValue;
}

function EditDetails(stockjournalid) {
    ////debugger;
    var tr;
    
    var HeaderCount = 0;
    
    var srl = 0;
    srl = srl + 1; 
    //debugger;
    $('#JournalDetailsGrid').empty();

    $("#dialog").dialog({
        autoOpen: true,
        modal: true,
        title: "Loading.."
    });
    $("#imgLoader").css("visibility", "visible");
    $.ajax({
        type: "POST",
        url: "/TranDepot/StockAdjustmentEdit",
        data: { StockjournalID: stockjournalid},
        dataType: "json",
        async: false,
        success: function (response) {
            //alert(JSON.stringify(response))
            //debugger;
            var listHeader = response.StockJournaleditDataset.varHeader;
            var listDetails = response.StockJournaleditDataset.varDetails;
            
            //debugger;

            /*Journal Header Info*/
            $.each(listHeader, function (index, record) {
                //debugger;
                $("#hdndispatchID").val(this['ADJUSTMENTID'].toString().trim());
                $("#FGInvoiceNo").val(this['ADJUSTMENTNO'].toString().trim());
                $("#InvoiceDate").val(this['ADJUSTMENTDATE'].toString().trim());
                $("#Remarks").val(this['REMARKS'].toString().trim());
            });

            
            /*Journal Details Info*/
            if (listDetails.length > 0) {

                $("#JournalDetailsGrid").empty();
                
                    
                tr;
                tr = $('#JournalDetailsGrid');
                HeaderCount = $('#JournalDetailsGrid thead th').length;
                    
                if (HeaderCount == 0) {
                    tr.append("<thead><tr><th style='text-align: center;'>Sl.No.</th><th style='display: none;'>Product ID</th><th>Product</th><th>Batch</th><th>MRP</th><th style='display: none;'>PacksizeID</th><th>Packsize</th><th>Journal Qty</th><th style='display: none;'>ReasonID</th><th>Reason</th><th style='display: none;'>StorelocationID</th><th>Storelocation</th><th>Mfg.Date</th><th>Exp.Date</th><th style='display: none;'>Weight</th><th style='display: none;'>Buffer Qty</th><th style='text-align: center;'>Delete</th></tr></thead><tbody>");

                }
                $.each(listDetails, function (index, record) {

                    tr = $('<tr/>');
                    tr.append("<td style='text-align: center;'><label class='slno' id='lbljournalslno'></label></td>");//0
                    tr.append("<td style='display: none;'>" + this['PRODUCTID'].toString().trim() + "</td>");//1
                    tr.append("<td style='width:250px;'>" + this['PRODUCTNAME'].toString().trim() + "</td>");//2
                    tr.append("<td >" + this['BATCHNO'].toString().trim() + "</td>");//3
                    tr.append("<td style='text-align: right;'>" + this['MRP'].toString().trim() + "</td>");//4
                    tr.append("<td style='display: none;'>" + this['PACKINGSIZEID'].toString().trim() + "</td>");//5
                    tr.append("<td >" + this['PACKINGSIZENAME'].toString().trim() + "</td>");//6
                    tr.append("<td style='text-align: right;'>" + this['ADJUSTMENTQTY'].toString().trim() + "</td>");//7
                    tr.append("<td style='display: none;'>" + this['REASONID'].toString().trim() + "</td>");//8
                    tr.append("<td >" + this['REASONNAME'].toString().trim() + "</td>");//9
                    tr.append("<td style='display: none;'>" + this['STORELOCATIONID'].toString().trim() + "</td>");//10
                    tr.append("<td >" + this['STORELOCATIONNAME'].toString().trim() + "</td>");//11
                    tr.append("<td >" + this['MFDATE'].toString().trim() + "</td>");//12
                    tr.append("<td >" + this['EXPRDATE'].toString().trim() + "</td>");//13
                    tr.append("<td style='display: none;'>" + this['WEIGHT'].toString().trim() + "</td>");//14
                    tr.append("<td style='text-align: right;display: none;'>" + this['BUFFERQTY'].toString().trim() + "</td>");//15
                    tr.append("<td style='text-align: center'><input type='image' class='gvJournalDetailsDelete'  id='btnJournalDetailsdelete' <img src='../Images/ico_delete_16.png' title='Delete'/></input></td>")//16;
                    $("#JournalDetailsGrid").append(tr);
                    tr.append("</tbody>");
                    JournalDetailsRowCount();
                });

                CalculateJournalQty();
            } 
            $("#imgLoader").css("visibility", "hidden");
            $("#dialog").dialog("close");
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
   
}

function getqtyinPCS(productid, packsizefromid, deliveredqty, deliveredqtypcs,stockqty,saleorderid,invoiceid) {
    var returnvalue = null;
    $.ajax({
        type: "POST",
        url: "/TranDepot/QtyInPcsGT",
        data: { Productid: productid.trim(), PacksizefromID: packsizefromid.trim(), Deliveredqty: deliveredqty.trim(), Stockqty: stockqty.trim(), SaleorderID: saleorderid.trim(), InvoiceID: invoiceid.trim() },
        dataType: "json",
        async: false,
        success: function (qtypcs) {
            var deliveredqty;
            var stockqty;
            var invoiceqty;
            $.each(qtypcs, function (key, item) {
                ////debugger;
                deliveredqty = item.DELIVEREDQTY;
                stockqty = item.STOCKQTY;
                invoiceqty = parseFloat(deliveredqty.trim()) + parseFloat(deliveredqtypcs);
                stockqty = parseFloat(stockqty.trim()).toFixed(3);
                $("#hdninvoiceqty").val(invoiceqty);
                if (invoiceqty > stockqty) {
                    returnvalue = 'Invoice qty should not be greater than Stock qty..!';
                }
                else {
                    returnvalue = 'na';
                }
                return false;
            });
        },
        failure: function (qtypcs) {
            alert(qtypcs.responseText);
        },
        error: function (qtypcs) {
            alert(qtypcs.responseText);
        }
    });
    return returnvalue;
}

function getCaseToPcsConversion(productid, packsizefromid, packsizetoid, caseqty, pcsqty) {
    var returnvalue = null;
    $.ajax({
        type: "POST",
        url: "/TranDepot/CaseToPCSConversion",
        data: { Productid: productid.trim(), FromPacksize: packsizefromid.trim(), ToPacksize: packsizetoid.trim(), CaseQty: caseqty.trim(), PcsQty: pcsqty.trim() },
        dataType: "json",
        async: false,
        success: function (casetopcs) {
            var totalpcs;
            $.each(casetopcs, function (key, item) {
                ////debugger;
                totalpcs = item.PCS_QTY;
                returnvalue = totalpcs;
                return false;
            });
        },
        failure: function (casetopcs) {
            alert(casetopcs.responseText);
        },
        error: function (casetopcs) {
            alert(casetopcs.responseText);
        }
    });
    return returnvalue;
}

function getProductType(productid) {
    var returnvalue = null;
    $.ajax({
        type: "POST",
        url: "/TranDepot/ProductType",
        data: { Productid: productid.trim()},
        dataType: "json",
        async: false,
        success: function (productType) {
            var type;
            $.each(productType, function (key, item) {
                ////debugger;
                type = item.TYPE;
                returnvalue = type;
                return false;
            });
        },
        failure: function (productType) {
            alert(productType.responseText);
        },
        error: function (productType) {
            alert(productType.responseText);
        }
    });
    return returnvalue;
}

function getHSNCode(productid) {
    var returnvalue = null;
    $.ajax({
        type: "POST",
        url: "/TranDepot/GetHSNCode",
        data: { Productid: productid.trim() },
        dataType: "json",
        async: false,
        success: function (hsncode) {
            var hsn;
            $.each(hsncode, function (key, item) {
                ////debugger;
                hsn = item.HSNCODE;
                returnvalue = hsn;
                return false;
            });
        },
        failure: function (hsncode) {
            alert(hsncode.responseText);
        },
        error: function (hsncode) {
            alert(hsncode.responseText);
        }
    });
    return returnvalue;
}

function create_UUID() {
    var dt = new Date().getTime();
    var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = (dt + Math.random() * 16) % 16 | 0;
        dt = Math.floor(dt / 16);
        return (c == 'x' ? r : (r & 0x3 | 0x8)).toString(16);
    });
    return uuid;
}

function isNumberKey(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}

function isNumberKeyWithDot(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if ((charCode < 48 || charCode > 57) && (charCode != 8) && (charCode != 46))
        return false;

    return true;
}

function isNumberKeyWithMinus(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if ((charCode < 48 || charCode > 57) && (charCode != 8) && (charCode != 45))
        return false;

    return true;
}

function BatchDetailsRowCount() {
    //debugger;
    var batchrowCount = document.getElementById("stockDetailsGrid").rows.length - 1;
    var batchcount = 0;
    if (batchrowCount > 0) {
        $('#stockDetailsGrid tbody tr').each(function () {
            batchcount = batchcount + 1;
            $(this).find('#lblstockslno').text(batchcount.toString().trim());
        })
    }
}



