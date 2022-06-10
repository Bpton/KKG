//#region Developer Info
/*
* For Conversion.cshtml Only
* Developer Name : Avishek Ghosh 
* Start Date     : 18/05/2020
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
    bindconversioncomboproduct();
    bindconversiongrid();

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
            bindconversiongrid();
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
            bindconversiongrid();
            $("#imgLoader").css("visibility", "hidden");
            $("#dialog").dialog("close");
        }, 5);
        e.preventDefault();
    });

    $('#SearchDepotID').change(function (e) {
        if ($('#SearchDepotID').val().trim() != '0') {

            $("#BRID").val($('#SearchDepotID').val().trim());
            bindconversiongrid();
        }
        e.preventDefault();
    });

    $('#ComboProductID').change(function (e) {
       
        if ($('#ComboProductID').val() != '0') {
            
            $("#ConversionQty").focus();
            bindconversioncomboproductdetails($('#ComboProductID').val().trim());
            /*Load BreakUp Ratio Start*/
            if ($('#ConversionQty').val() != '' && parseInt($('#ConversionQty').val()) > 0) {

                if ($('#ConversionTypeID').val().trim() == 'C2P') {

                    bindSingleProduct($('#ComboProductID').val().trim(), $('#ConversionQty').val().trim());
                }
            }
            else {
                $('#ddlBatch').empty();
            }
            /*Load BreakUp Ratio End*/
        }
        else {
            
            $('#spnQTY').text('');
            $('#stockDetailsGrid').empty();
            $('#ddlBatch').empty();
            $('#ConversionQty').val('');
            $('#SingleProductName').val('');
            $('#TotalStockQty').val('');
            $('#TotalConversionQty').val('');
        }
        $('#stockDetailsGrid').empty();
        $('#SingleProductName').val('');
        $('#TotalStockQty').val('');
        $('#TotalConversionQty').val('');
        e.preventDefault();
    });

    $("#ConversionQty").change(function (e) {
        //debugger;
        if ($('#ComboProductID').val() != '0' && parseInt($('#ConversionQty').val().trim()) > 0) {
            bindSingleProduct($('#ComboProductID').val().trim(), $('#ConversionQty').val().trim());
            
        }
        else {
            $('#ddlBatch').empty();
            toastr.warning('<b>Please select combo product & enter convertion quantity..</b>');
        }
        $('#stockDetailsGrid').empty();
        $('#SingleProductName').val('');
        $('#TotalStockQty').val('');
        $('#TotalConversionQty').val('');
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

                splitSingleProductDetails();

                $("#imgLoader").css("visibility", "hidden");
                $("#dialog").dialog("close");
            }, 5);
        }
        else {
            $('#stockDetailsGrid').empty();
            $('#SingleProductName').val('');
            $('#TotalStockQty').val('');
            $('#TotalConversionQty').val('');
        }

        e.preventDefault();
    });

    $("#btnadd").click(function (e) {
        ////debugger;

        var itemrowCount = document.getElementById("stockDetailsGrid").rows.length - 1;

        if (itemrowCount <= 0) {
            toastr.warning('<b><font color=black>Please select product..!</font></b>');
            return false;
        }
        else if (parseInt($('#TotalConversionQty').val()) <= 0) {

            toastr.warning('<b><font color=black>Conversion quantity should not be 0..!</font></b>');
            return false;
        }
        else {
                //debugger;
                var conversionqty = 0;
                var totalconversionqty = 0;

                if ($('#stockDetailsGrid').length) {

                    $('#stockDetailsGrid tbody tr').each(function () {

                        conversionqty = parseInt($(this).find('#txtcnvquantity').val().trim());
                        totalconversionqty += parseInt(conversionqty);
                    });
                };

                if (parseInt(totalconversionqty) != parseInt($("#hdnbreakupqty").val())) {

                    toastr.warning('<b><font color=black>Total converstion quantity should be ' + parseInt($('#hdnbreakupqty').val().trim()) + '</font></b>');
                    return false;
                }
                else {

                    if ($('#stockDetailsGrid').length) {

                        $("#dialog").dialog({
                            autoOpen: true,
                            modal: true,
                            title: "Loading.."
                        });
                        $("#imgLoader").css("visibility", "visible");
                        setTimeout(function () {

                        var flag = false;
                        var count = 0;
                        var conversionProductId = '';
                        var conversionProductName = '';
                        var conversionBatchNo = '';
                        var conversionMrp = '';
                        var conversionStockQty = '';
                        var conversionMfgDate = '';
                        var conversionExpDate = '';
                        var conversionQty = '';
                        var conversionStorelocation = '';

                        $('#stockDetailsGrid tbody tr').each(function () {

                            count = count + 1;
                            conversionProductId = $(this).find('td:eq(1)').html().trim();
                            conversionProductName = $(this).find('td:eq(2)').html().trim();
                            conversionBatchNo = $(this).find('td:eq(3)').html().trim();
                            conversionMrp = $(this).find('td:eq(4)').html().trim();
                            conversionStockQty = $(this).find('td:eq(5)').html().trim();
                            conversionMfgDate = $(this).find('td:eq(6)').html().trim();
                            conversionExpDate = $(this).find('td:eq(7)').html().trim();
                            conversionQty = $(this).find('#txtcnvquantity').val().trim();
                            conversionStorelocation = $(this).find('td:eq(8)').html().trim();
                            if (parseInt(conversionQty) != 0) {
                                IsExistsConversionProduct(conversionProductId, conversionProductName, conversionBatchNo, conversionMrp, conversionStockQty,
                                    conversionMfgDate, conversionExpDate, conversionQty, conversionStorelocation);
                            }


                            $("#imgLoader").css("visibility", "hidden");
                            $("#dialog").dialog("close");
                            }, 3);

                        });
                    };
                }
             }
        e.preventDefault();
    });


    $("#btnsave").click(function (e) {
        debugger;

        var totcnvqty = 0;
        var cnvqty = 0;
        var flag = true;
        var BatchFlag = 0;
        entryLockstatus = bindlockdateflag($("#InvoiceDate").val(), $("#hdnFinYear").val());
        if (entryLockstatus == '0') {
            toastr.warning('<b><font color=black>Entry date is locked,please contact with admin...!</font></b>');
            return false;
        }
        else {
            debugger;
            var itemrowCount = document.getElementById("conversionDetailsGrid").rows.length - 1;

            if (itemrowCount <= 0) {
                toastr.warning('<b><font color=black>Please add Product..!</font></b>');
                return false;
            }
            else {
                debugger;
                 $('#conversionDetailsGrid tbody tr').each(function () {
                 
                     cnvqty = parseInt($(this).find('td:eq(9)').html().trim());
                     totcnvqty += -1 * parseInt(cnvqty);
                 });
               
                
                if ($("#hdnSingleCartoon").val().trim() == '0') {

                    if (parseInt(totcnvqty) != parseInt($("#ConversionQty").val().trim())) {

                        toastr.warning('<b><font color=black>Incomplete conversion..!</font></b>');
                        flag = false;
                        return false;
                    }
                }
                else {

                    if (parseInt(totcnvqty) != parseInt($("#ConversionQty").val().trim())*4) {

                        toastr.warning('<b><font color=black>Incomplete conversion..!</font></b>');
                        flag = false;
                        return false;
                    }
                }  
            }
            if (flag == true) {
                debugger;
                var ComboBatchNO = '';
                var ComboBatchNO1 = '';
                var ComboBatchNO2 = '';
                var ComboMFGDate = '';
                var ComboExpDate = '';
                var ComboWeight = '0';
                var ComboMRP = 0;
                var ComboAssesment = 0;
                var combodepotrate = 0;

                if ($('#conversionDetailsGrid').length) {
                    debugger;
                    var count = 0;
                    var BatchLength = 0;
                    var ProductCount = 0;
                    var ProductName = '';
                    var BatchNo = '';
                    var MRP = '';
                    var MfgDate = '';
                    var ExpDate = '';

                    $('#conversionDetailsGrid tbody tr').each(function () {
                        debugger;
                        count = count + 1;
                        ProductName = $(this).find('td:eq(2)').html().trim();
                        BatchNo = $(this).find('td:eq(3)').html().trim();
                        MRP = $(this).find('td:eq(4)').html().trim();
                        MfgDate = $(this).find('td:eq(6)').html().trim();
                        ExpDate = $(this).find('td:eq(7)').html().trim();
                        if (count == 1) {
                            ComboAssesment = 65;
                        }
                        if (count == itemrowCount) {
                            ComboMFGDate = MfgDate;
                            ComboExpDate = ExpDate;
                        }
                        ComboBatchNO = ComboBatchNO + BatchNo + '~';
                    });

                    debugger;
                    ComboBatchNO = ComboBatchNO.substr(0, ComboBatchNO.length - 1);
                    BatchLength = ComboBatchNO.length;

                    if (BatchLength > 47) {
                        ComboBatchNO1 = ComboBatchNO.substr(0, 47).trim();
                    }
                    else {
                        ComboBatchNO1 = ComboBatchNO.trim();
                    }
                    ComboMRP = ComboMrp($("#ComboProductID").val().trim());

                    /*Batch Checking Start*/
                    debugger;
                    var exists = Batchexists($("#ComboProductID").val().trim(), ComboBatchNO1.trim(), ComboMRP.trim());
                    if (exists == '1') {
                        debugger;
                        var Batchcount = 0;
                        var BatchcountFinal = 0;
                        Batchcount = BatchCountExists($("#ComboProductID").val().trim(), ComboBatchNO1.trim(), ComboMRP.trim());
                        BatchcountFinal = parseInt(Batchcount) + 1;
                        ComboBatchNO2 = ComboBatchNO1.trim() + '~' + BatchcountFinal.toString().trim();

                        /*Insert new Batch in M_BATCHDETAILS*/
                        BatchFlag = SaveBatch($("#ComboProductID").val().trim(), ComboBatchNO2.trim(), ComboMFGDate.trim(), ComboExpDate.trim(), ComboMRP.trim(), $("#hdnFinYear").val().trim());
                       
                    }
                    else {
                        debugger;
                        ComboBatchNO2 = ComboBatchNO1.trim();
                        BatchFlag = 1;
                    }

                    if (BatchFlag > 0) {

                        SaveConversion($("#ComboProductID").val().trim(), $("#ComboProductID option:selected").text().trim(),
                            ComboBatchNO2, $("#ConversionQty").val().trim(), ComboMFGDate, ComboExpDate, ComboMRP)
                    }
                };
            }
        }
        e.preventDefault();
    });
    
});

$(function () {

    var invoiceid;
    var dispatchid;

    /*Delete function for conversion details grid*/
    $("body").on("click", "#conversionDetailsGrid #btnConversionDetailsdelete", function () {
        ////debugger;
        var productid;
        var productid2;
        var rowCount;
        var deleteflag = 0;

        var row = $(this).closest("tr");
        productid = row.find('td:eq(1)').html().trim();

        if (confirm("Do you want to delete this item?")) {

            
            $('#conversionDetailsGrid tbody tr').each(function () {
                var row2 = $(this).closest("tr");
                productid2 = $(this).find('td:eq(1)').html().trim();
                
                if (productid == productid2) {
                    deleteflag = 1;
                    row2.remove();
                    ConversionDetailsRowCount();
                }
            });

            rowCount = document.getElementById("conversionDetailsGrid").rows.length - 1;
            if (rowCount <= 0) {
                $("#conversionDetailsGrid").empty();
            }
        }

        if (rowCount > 0 ) {
            $("#ComboProductID").prop("disabled", true);
            $("#ComboProductID").chosen({
                search_contains: true
            });
            $("#ComboProductID").trigger("chosen:updated");
            $("#ConversionQty").prop("disabled", true);
        }
        else {
            $("#ComboProductID").prop("disabled", false);
            $("#ComboProductID").chosen({
                search_contains: true
            });
            $("#ComboProductID").trigger("chosen:updated");
            $("#ConversionQty").prop("disabled", false);
        }
        
    });

    /*View function for Conversion*/
    $("body").on("click", "#CSDConversionGrid .gvConversionView", function () {
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

            $('#conversionDetailsGrid').empty();
            EditDetails($('#hdndispatchID').val().trim());


            $('#btnsave').css("display", "");
            $('#btnAddnew').css("display", "");
           

            $("#dvAdd").find("input, textarea, select,submit").attr("disabled", "disabled");
            $('#divTransferNo').css("display", "");
            $("#btnclose").prop("disabled", false);
            $('#dvAdd').css("display", "");
            $('#dvDisplay').css("display", "none");
            $('#divStock').css("display", "none");

            $("#BRID").chosen('destroy');
            $("#BRID").chosen({
                search_contains: true
            });
            $("#BRID").trigger("chosen:updated");

            $("#ConversionTypeID").chosen({
                search_contains: true
            });
            $("#ConversionTypeID").trigger("chosen:updated");
            

            //$("#ComboProductID").prop("disabled", true);
            $("#ComboProductID").chosen('destroy');
            $("#ComboProductID").chosen({
                search_contains: true
            });
            $("#ComboProductID").trigger("chosen:updated");            

            $('#ddlBatch').empty();
            $("#InvoiceDate").datepicker("destroy");


            $("#imgLoader").css("visibility", "hidden");
            $("#dialog").dialog("close");
        }, 3);
    });

    /*Keyup function for Conversion*/
    $("body").on("keyup", "#stockDetailsGrid #txtcnvquantity", function () {
        var row = $(this).closest("tr");
        var conversionqty = row.find('#txtcnvquantity').val().trim();
        var stockqty = row.find('td:eq(5)').html().trim();

        if (conversionqty == '') {
            row.find('#txtcnvquantity').val('0');
        }
        if (parseInt(conversionqty) > parseInt(stockqty)) {
            row.find('#txtcnvquantity').val(parseInt(stockqty));
        }

        CalculateStock();
    });
});


function loadAddNewRecord() {

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
        $('#divStock').css("display", "");
        ClearControls();
        $("#imgLoader").css("visibility", "hidden");
        $("#dialog").dialog("close");
    }, 3);
    
}

/*Add Product Start*/
function IsExistsConversionProduct(ConversionProductId, ConversionProductName, ConversionBatchNo, ConversionMrp, ConversionStockQty,
    ConversionMfgDate, ConversionExpDate, ConversionQty, ConversionStorelocation) {
    debugger;
    if ($('#conversionDetailsGrid').length) {
        var exists = false;
        var arraydetails = [];
        $('#conversionDetailsGrid tbody tr').each(function () {
            var dispatchdetail = {};
            var inputproductgrd = $(this).find('td:eq(1)').html().trim();
            var inputbatchgrd = $(this).find('td:eq(3)').html().trim();
            dispatchdetail.INPUTPRODUCT = inputproductgrd;
            dispatchdetail.INPUTBATCH = inputbatchgrd;
            arraydetails.push(dispatchdetail);
        });
        var jsondispatchobj = {};
        jsondispatchobj.conversionproductDetails = arraydetails;

        for (i = 0; i < jsondispatchobj.conversionproductDetails.length; i++) {
            if (jsondispatchobj.conversionproductDetails[i].INPUTPRODUCT.trim() == ConversionProductId.trim() && jsondispatchobj.conversionproductDetails[i].INPUTBATCH.trim() == ConversionBatchNo.trim()) {
                exists = true;
                break;
            }
        }
        if (exists != false) {
            toastr.error('Item already exists...!');
            return false;
        }
        else {
            addConversionFinalGrid(ConversionProductId, ConversionProductName, ConversionBatchNo, ConversionMrp, ConversionStockQty,
                ConversionMfgDate, ConversionExpDate, ConversionQty, ConversionStorelocation);
           
        }
    }
}

function addConversionFinalGrid(ConversionProductId, ConversionProductName, ConversionBatchNo, ConversionMrp, ConversionStockQty,
    ConversionMfgDate, ConversionExpDate, ConversionQty, ConversionStorelocation) {
    //debugger;
    //Create Table 
    var tr;
    tr = $('#conversionDetailsGrid');
    var HeaderCount = $('#conversionDetailsGrid thead th').length;
   
    if (HeaderCount == 0) {
        tr.append("<thead><tr><th style='text-align: center;'>Sl.No.</th><th style='display: none;'>Product ID</th><th>Product</th><th>Batch</th><th>MRP</th><th>Stock Qty(Pcs)</th><th>Mfg.Date</th><th>Exp.Date</th><th style='display: none;'>Storelocation</th><th style='text-align: center;'>Conversion Qty(Pcs)</th><th style='text-align: center;'>Delete</th></tr></thead><tbody>");
        
    }
   
    tr = $('<tr/>');
    tr.append("<td style='text-align: center;'><label class='slno' id='lblconversionslno'></label></td>");//0
    tr.append("<td style='display: none;'>" + ConversionProductId.toString().trim() + "</td>");//1
    tr.append("<td style='width:250px;'>" + ConversionProductName.toString().trim() + "</td>");//2
    tr.append("<td >" + ConversionBatchNo.toString().trim() + "</td>");//3
    tr.append("<td style='text-align: right;'>" + ConversionMrp.toString().trim() + "</td>");//4
    tr.append("<td style='text-align: right;'>" + ConversionStockQty.toString().trim() + "</td>");//5
    tr.append("<td >" + ConversionMfgDate.toString().trim() + "</td>");//6
    tr.append("<td >" + ConversionExpDate.toString().trim() + "</td>");//7
    tr.append("<td style='display: none;'>" + ConversionStorelocation.toString().trim() + "</td>");//8
    tr.append("<td style='text-align: right;'>" + parseInt(ConversionQty)* -1 + "</td>");//9
    tr.append("<td style='text-align: center'><input type='image' class='gvConversionDetailsDelete'  id='btnConversionDetailsdelete' <img src='../Images/ico_delete_16.png' title='Delete'/></input></td>")//10;
    $("#conversionDetailsGrid").append(tr);
    tr.append("</tbody>");
    
    ConversionDetailsRowCount();
}

function ConversionDetailsRowCount() {
    debugger;
    var cnvdetailsrowCount = document.getElementById("conversionDetailsGrid").rows.length - 1;
    var finalcnvcount = 0;
    if (cnvdetailsrowCount > 0) {

        $("#ComboProductID").prop("disabled", true);
        $("#ComboProductID").chosen({
            search_contains: true
        });
        $("#ComboProductID").trigger("chosen:updated");

        $("#ConversionQty").prop("disabled", true);
        $('#ddlBatch').val('0');
        $('#SingleProductName').val('');
        $('#TotalStockQty').val('');
        $('#TotalConversionQty').val('');
        $('#stockDetailsGrid').empty();

        $('#conversionDetailsGrid tbody tr').each(function () {
            finalcnvcount = finalcnvcount + 1;
            $(this).find('#lblconversionslno').text(finalcnvcount.toString().trim());
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
        maxDate: 'today',
        dateFormat: "dd/mm/yy",
        showOn: 'button',
        buttonText: 'Show Date',
        buttonImageOnly: true,
        maxDate: 0,
        minDate: 0,
        buttonImage: 'http://jqueryui.com/resources/demos/datepicker/images/calendar.gif'
    });
    $(".ui-datepicker-trigger").mouseover(function () {
        $(this).css('cursor', 'pointer');
    });
    $("#LrGrDate").datepicker({
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
    $("#GatepassDate").datepicker({
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


    $("#txtfrmdate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
    $("#txttodate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
    $("#LrGrDate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
    $("#InvoiceDate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
    $("#GatepassDate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
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

function bindSingleProduct(comboproductid,conversionqty) {
    //debugger;
    var ddlBatch = $("#ddlBatch");
    var Batch = '0';
    var desc = '';
    var desc1 = '';
    var val1 = '';
    var listItems = '';
    listItems = "<option value='0'>Select Product</option>";
    desc = 'Product'.padEnd(25, '\u00A0') + repeatstr("&nbsp;", 10) + 'Quantity(Pcs)'.padEnd(15, '\u00A0');
    listItems += "<option value='0'>" + desc + "</option>";
    $("#dialog").dialog({
        autoOpen: true,
        modal: true,
        title: "Loading.."
    });
    $("#imgLoader").css("visibility", "visible");
    $.ajax({
        type: "POST",
        url: "/TranDepot/GetCSDComboProductDetails",
        data: { ComboProductID: comboproductid },
        async: false,
        dataType: "json",
        success: function (comboproductdetailslist) {

            //debugger;
            var counter = 0;
            $('#ddlBatch').empty();
            if (comboproductdetailslist.length > 0) {

                var totalqty = 0;
                $.each(comboproductdetailslist, function () {
                    //debugger;
                    totalqty += parseInt(this["QTY"]);
                });

                if ($('#ConversionTypeID').val().trim() == 'C2P') {

                    if (comboproductdetailslist[0].ISSINGLECARTOON == '0') {
                        var ReturnQty = '';
                        $.each(comboproductdetailslist, function () {
                            debugger;
                            ReturnQty = ((parseFloat(conversionqty) / totalqty) * this["QTY"]).toString();
                            if (ReturnQty.indexOf('.') == -1) {
                                val1 = this["PRODUCTID"] + "|" + this["PRODUCTNAME"] + "|" + ReturnQty.toString();
                                desc1 = this["PRODUCTNAME"].padEnd(25, '\u00A0') + repeatstr("&nbsp;", 10) + ReturnQty.toString().padEnd(10, '\u00A0');
                                listItems += "<option value='" + val1 + "'>" + desc1 + "</option>";
                                counter = counter + 1;
                            }
                            else {
                                toastr.warning('<b>Wrong conversion quantity...</b>');
                                return false;
                            }
                        });
                    }
                    else {
                        var ReturnQty = '';
                        $.each(comboproductdetailslist, function () {

                            ReturnQty = (parseFloat(conversionqty) * this["QTY"]).toString();
                            if (ReturnQty.indexOf('.') == -1) {
                                val1 = this["PRODUCTID"] + "|" + this["PRODUCTNAME"] + "|" + ReturnQty.toString();
                                desc1 = this["PRODUCTNAME"].padEnd(25, '\u00A0') + repeatstr("&nbsp;", 10) + ReturnQty.toString().padEnd(10, '\u00A0');
                                listItems += "<option value='" + val1 + "'>" + desc1 + "</option>";
                                counter = counter + 1;
                            }
                            else {
                                toastr.warning('<b>Wrong conversion quantity...</b>');
                                return false;
                            }
                        });
                    }
                }
                ddlBatch.append(listItems);
            }
            else {
                toastr.warning('<b>Combo product breakup details not available...</b>');
            }
            $("#imgLoader").css("visibility", "hidden");
            $("#dialog").dialog("close");
            
        },
        failure: function (comboproductdetailslist) {
            alert(comboproductdetailslist.responseText);
        },
        error: function (comboproductdetailslist) {
            alert(comboproductdetailslist.responseText);
        }
    });
}

function splitSingleProductDetails() {
    ////debugger;
    
    var strval2 = $("#ddlBatch").val();
    var splitval = strval2.split('|');
    var productid = splitval[0];
    var productname = splitval[1];
    var returnqty = splitval[2];
   
    $("#hdnproductid").val(productid);
    $("#hdnproductname").val(productname);
    $("#hdnbreakupqty").val(returnqty);
    bindBatchDetails($('#BRID').val().trim(), $("#hdnproductid").val().trim(), 'B9F29D12-DE94-40F1-A668-C79BF1BF4425', '0');
    
}

function bindBatchDetails(depotid,productid,packsizeid,batchno) {
    ////debugger;

    $.ajax({
        type: "POST",
        url: "/TranDepot/GetBatchDetails",
        data: { DepotID: depotid, ProductID: productid, PacksizeID: packsizeid, BatchNo: batchno },
        async: false,
        dataType: "json",
        success: function (batchlist) {
            ////debugger;
            if (batchlist.length > 0) {

                $("#stockDetailsGrid").empty();

                var tr;
                tr = $('#stockDetailsGrid');
                var HeaderCount = $('#stockDetailsGrid thead th').length;
                if (HeaderCount == 0) {
                    tr.append("<thead><tr><th style='text-align: center;'>Sl.No.</th><th style='display: none;'>Product ID</th><th>Product</th><th>Batch</th><th>MRP</th><th>Stock Qty(Pcs)</th><th>Mfg.Date</th><th>Exp.Date</th><th style='display: none;'>Storelocation</th><th style='text-align: center;'>Conversion Qty(Pcs)</th></tr></thead><tbody>");
                }
                for (var i = 0; i < batchlist.length; i++) {

                    tr = $('<tr/>');
                    tr.append("<td style='text-align: center;'><label class='slno' id='lblstockslno'></label></td>");//0
                    tr.append("<td style='display: none;'>" + batchlist[i].BatchPRODUCTID + "</td>");//1
                    tr.append("<td style='width:250px;'>" + batchlist[i].BatchPRODUCTNAME + "</td>");//2
                    tr.append("<td >" + batchlist[i].BATCHNO + "</td>");//3
                    tr.append("<td style='text-align: right;'>" + batchlist[i].BatchMRP + "</td>");//4
                    tr.append("<td style='text-align: right;'>" + batchlist[i].BatchSTOCKQTY + "</td>");//5
                    tr.append("<td >" + batchlist[i].BatchMFGDATE + "</td>");//6
                    tr.append("<td >" + batchlist[i].BatchEXPIRDATE + "</td>");//7
                    tr.append("<td style='display: none;'>" + batchlist[i].BatchSTORELOCATION + "</td>");//8
                    tr.append("<td style='text-align: center'><input type='text' class='gvcnvquantity'  id='txtcnvquantity' value='0' onkeypress='return isNumberKey(event)' style='text-align: right; width:60px; height:18px'></input></td>");//9
                    $("#stockDetailsGrid").append(tr);
                }
                tr.append("</tbody>");
                BatchDetailsRowCount();
            }
            else {
                $("#stockDetailsGrid").empty();
                toastr.warning('<b>Stock not available..</b>');
            }

            /*Setting convertion quantity in first stock available cell Start*/
            var gridstockqty = 0;
            $('#stockDetailsGrid tbody tr').each(function () {

                gridstockqty = $(this).find('td:eq(5)').html().trim();

                if (parseInt(gridstockqty) >= parseInt($('#hdnbreakupqty').val().trim())) {

                    $(this).find('#txtcnvquantity').val($('#hdnbreakupqty').val().trim());
                    return false;
                }
            });
            /*Setting convertion quantity in first stock available cell End*/

            CalculateStock();
        },
        failure: function (batchlist) {
            alert(batchlist.responseText);
        },
        error: function (batchlist) {
            alert(batchlist.responseText);
        }
    });
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

function RowCountConversionList() {
    var table = document.getElementById("CSDConversionGrid");
    var rowCount = document.getElementById("CSDConversionGrid").rows.length - 1;
    if (rowCount > 0) {
        for (var i = 1; i <= rowCount; i++) {
            table.rows[i].cells[0].innerHTML = i.toString();
        }
    }
}

function CalculateStock() {
    ////debugger;
    var totalstock = 0;
    var totalconversion = 0;
    var rowCount = 0;
    rowCount = document.getElementById("stockDetailsGrid").rows.length - 1;

    if (rowCount > 0) {
        
        $('#stockDetailsGrid tbody tr').each(function () {
        
            totalstock += parseInt($(this).find('td:eq(5)').html().trim());
            totalconversion += parseInt($(this).find('#txtcnvquantity').val().trim());
            
        });
        $('#TotalStockQty').val(totalstock);
        $('#TotalConversionQty').val(totalconversion);
        $('#SingleProductName').val($('#hdnproductname').val());
    }
}

function ClearControls() {
    $("#dvAdd").find("input, textarea, select,submit").removeAttr("disabled");
    
    $('#btnsave').css("display", "");
    $('#btnAddnew').css("display", "");
    $('#btnApprove').css("display", "none");
    
    $('#divTransferNo').css("display", "none");
    $("#btnadd").prop("disabled", false);
    $("#TransferNo").attr("disabled", "disabled");
    
    $("#InvoiceDate").attr("disabled", "disabled");
    
    $("#SingleProductName").attr("disabled", "disabled");
    $("#TotalStockQty").attr("disabled", "disabled");
    $("#TotalConversionQty").attr("disabled", "disabled");

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
    ////debugger;

    $("#ComboProductID").val('0');
    $("#ComboProductID").removeAttr("disabled");
    $("#ComboProductID").chosen({
        search_contains: true
    });
    $("#ComboProductID").trigger("chosen:updated");

    $("#ConversionTypeID").removeAttr("disabled");
    $("#ConversionTypeID").chosen({
        search_contains: true
    });
    $("#ConversionTypeID").trigger("chosen:updated");

    
    $("#ConversionQty").removeAttr("disabled");
    $("#ConversionQty").val('');   

    $('#ddlBatch').empty();
    
    $('#SingleProductName').val('');
    $('#TotalStockQty').val('');
    $('#TotalConversionQty').val('');
    $('#stockDetailsGrid').empty();
    $('#conversionDetailsGrid').empty();
    $('#spnQTY').text('');
    $('#hdndispatchID').val('0');
    
    
}

function clearafterAdd() {
    $('#InvoiceQty').val('');
    $('#InvoicePcs').val('0');
    $('#StockQty').val('');
    $('#MRP').val('');
    $('#Rate').val('');
    $('#OrderQty').val('');
    $('#DeliveredQty').val('');
    $('#RemainingQty').val('');
    $("#hdninvoiceqty").val('');
    $("#hdnbillqty").val('');
    $("#qsguid").val('');
    $("#qsheader").val('');
    $('#hdnpriceschemeid').val('');
    $('#hdnpriceschemepercentage').val('');
    $('#hdnpriceschemevalue').val('');
    $('#hdndiscountvalue').val('');
    $('#hdnHSNCode').val('');
    $('#hdnqtyschemeid').val('');
    $('#hdnschappqty').val('');
    if ($("#chkFree").prop('checked') == true) {
        $("#chkFree").prop("checked", false);
    }
}

function TaxGridDeliveryQty(productid, packsizefromid, deliveredqty, deliveredqtypcs, stockqty, saleorderid, invoiceid) {
    var returnvalue = null;
    $.ajax({
        type: "POST",
        url: "/TranDepot/QtyInPcsGT",
        data: { Productid: productid.trim(), PacksizefromID: packsizefromid.trim(), Deliveredqty: deliveredqty.trim(), Stockqty: stockqty.trim(), SaleorderID: saleorderid.trim(), InvoiceID: invoiceid.trim() },
        dataType: "json",
        async: false,
        success: function (qtypcs) {
            var deliveredqty;
            var invoiceqty;
            $.each(qtypcs, function (key, item) {
                //////debugger;
                deliveredqty = item.DELIVEREDQTY;
                invoiceqty = parseFloat(deliveredqty.trim()) + parseFloat(deliveredqtypcs);
                returnvalue = invoiceqty;
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

function SaveBatch(productid, batch, mfg, exp, mrp, finyear) {
    debugger;
    var returnvalue = null;
    var batchsave = {};
    batchsave.ComboProduct = productid.trim();
    batchsave.ComboBatch = batch.trim();
    batchsave.ComboMfg = mfg.trim();
    batchsave.ComboExpr = exp.trim();
    batchsave.ComboMrp = mrp.trim();
    batchsave.FinYear = finyear.trim();
    $.ajax({
        url: "/TranDepot/saveBatchMaster",
        data: '{batchsave:' + JSON.stringify(batchsave) + '}',
        type: "POST",
        async: false,
        contentType: "application/json",
        success: function (responseMessage) {
            var messageid;
            var messagetext;
            $.each(responseMessage, function (key, item) {
                messageid = item.MessageID;
                messagetext = item.MessageText;
                returnvalue = messageid;
                return false;
            });
            
            
        },
        failure: function (responseMessage) {
            alert(responseMessage.responseText);
        },
        error: function (responseMessage) {
            alert(responseMessage.responseText);
        }
    });
    return returnvalue;
};

function SaveConversion(ComboProductID,ComboProductName,ComboBatch,Conversionquantity,ComboMfg,ComboExp,ComboMrp) {
            debugger;
   
            var rowCount = 0;
            rowCount = document.getElementById("conversionDetailsGrid").rows.length - 1;

            $("#dialog").dialog({
                autoOpen: true,
                modal: true,
                title: "Loading.."
            });
            $("#imgLoader").css("visibility", "visible");
            setTimeout(function () {

                var i = 0;
                

                conversionlist = new Array();
                var csdconversionsave = {};

                debugger;
                csdconversionsave.FGInvoiceID = $('#hdndispatchID').val().trim();

                if ($('#hdndispatchID').val() == '0') {
                    csdconversionsave.FLAG = 'A';
                }
                else {
                    csdconversionsave.FLAG = 'U';
                }
                csdconversionsave.InvoiceDate = $("#InvoiceDate").val().trim();
                csdconversionsave.BRID = $("#BRID").val().trim();
                csdconversionsave.BRNAME = $("#BRID option:selected").text().trim();
                csdconversionsave.UserID = $("#hdnUserID").val().trim();
                csdconversionsave.FinYear = $("#hdnFinYear").val().trim();
                csdconversionsave.Remarks = 'NA';
                csdconversionsave.AdjustmentType = 'C';
                csdconversionsave.AdjustmentMenu = 'StockAdjustment';
            
            
            

            
            
                var count = 0;
                $('#conversionDetailsGrid tbody tr').each(function () {
                    debugger;
                    count = count + 1;
                    var conversiondetails = {};

                    var productid = $(this).find('td:eq(1)').html().trim();
                    var productname = $(this).find('td:eq(2)').html().trim();
                    var batchno = $(this).find('td:eq(3)').html().trim();
                    var price = 0;
                    var adjustmentqty = $(this).find('td:eq(9)').html().trim();
                    var packingsizeid = 'B9F29D12-DE94-40F1-A668-C79BF1BF4425';
                    var packingsizename = 'PCS';
                    var reasonid = '0';
                    var reasonname = 'NA';
                    var storelocationid = $(this).find('td:eq(8)').html().trim();
                    var storelocationname = 'Saleable';
                    var mfdate = $(this).find('td:eq(6)').html().trim();
                    var exprdate = $(this).find('td:eq(7)').html().trim();
                    var mrp = $(this).find('td:eq(4)').html().trim();
                    var weight = '0';
                    var amount = 0;
                    var bufferqty = $(this).find('td:eq(9)').html().trim();
                    var approved = 'N';                

                    conversiondetails.PRODUCTID = productid;
                    conversiondetails.PRODUCTNAME = productname;
                    conversiondetails.BATCHNO = batchno;
                    conversiondetails.PRICE = price;
                    conversiondetails.ADJUSTMENTQTY = adjustmentqty;
                    conversiondetails.PACKINGSIZEID = packingsizeid;
                    conversiondetails.PACKINGSIZENAME = packingsizename;
                    conversiondetails.REASONID = reasonid;
                    conversiondetails.REASONNAME = reasonname;
                    conversiondetails.STORELOCATIONID = storelocationid;
                    conversiondetails.STORELOCATIONNAME = storelocationname;
                    conversiondetails.MFDATE = mfdate;
                    conversiondetails.EXPRDATE = exprdate;
                    conversiondetails.MRP = mrp;
                    conversiondetails.WEIGHT = weight;
                    conversiondetails.AMOUNT = amount;
                    conversiondetails.BUFFERQTY = bufferqty;
                    conversiondetails.APPROVED = approved;
                    conversionlist[i++] = conversiondetails;

                    
                });

                /*For new generated combo product*/
                
                var conversiondetails2 = {};
                conversiondetails2.PRODUCTID = ComboProductID;
                conversiondetails2.PRODUCTNAME = ComboProductName;
                conversiondetails2.BATCHNO = ComboBatch;
                conversiondetails2.PRICE = 0;
                conversiondetails2.ADJUSTMENTQTY = Conversionquantity;
                conversiondetails2.PACKINGSIZEID = 'B9F29D12-DE94-40F1-A668-C79BF1BF4425';
                conversiondetails2.PACKINGSIZENAME = 'PCS';
                conversiondetails2.REASONID = '0';
                conversiondetails2.REASONNAME = 'NA';
                conversiondetails2.STORELOCATIONID = '113BD8D6-E5DC-4164-BEE7-02A16F97ABCC';
                conversiondetails2.STORELOCATIONNAME = 'Saleable';
                conversiondetails2.MFDATE = ComboMfg;
                conversiondetails2.EXPRDATE = ComboExp;
                conversiondetails2.MRP = ComboMrp;
                conversiondetails2.WEIGHT = '0';
                conversiondetails2.AMOUNT = 0;
                conversiondetails2.BUFFERQTY = Conversionquantity;
                conversiondetails2.APPROVED = 'N';
                conversionlist[i++] = conversiondetails2;
                
                /*For new generated combo product*/

                csdconversionsave.StockAdjustmentDetails = conversionlist;

            

                //alert(JSON.stringify(csdconversionsave));

                $.ajax({
                    url: "/TranDepot/conversionsavedata",
                    data: '{conversionsave:' + JSON.stringify(csdconversionsave) + '}',
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
                            bindconversiongrid();
                        }
                        else if (messageid == '2'){
                            $('#dvAdd').css("display", "none");
                            $('#dvDisplay').css("display", "");
                            $("#txtfrmdate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
                            $("#txttodate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
                            ClearControls();
                            bindconversiongrid();
                            
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

function bindconversiongrid() {
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
        url: "/TranDepot/BindConversionGrid",
        data: { FromDate: $('#txtfrmdate').val().trim(), ToDate: $('#txttodate').val().trim(), DepotID: $('#BRID').val().trim(), FinYear: $('#hdnFinYear').val().trim()},
        dataType: "json",
        success: function (response) {
            var tr;
            tr = $('#CSDConversionGrid');
            tr.append("<thead><tr><th>Sl. No.</th><th style='display: none'>ADJUSTMENTID</th><th>Conversion Date</th><th>Conversion No</th><th>Entry User</th><th>Type</th><th>View</th>");
            
            $('#CSDConversionGrid').DataTable().destroy();
            $("#CSDConversionGrid tbody tr").remove();
            for (var i = 0; i < response.length; i++) {
                tr = $('<tr/>');
                tr.append("<td style='text-align: center'>" + srl + "</td>");//0
                tr.append("<td style='display: none'>" + response[i].ADJUSTMENTID + "</td>");
                tr.append("<td>" + response[i].ADJUSTMENTDATE1 + "</td>");
                tr.append("<td>" + response[i].ADJUSTMENTNO + "</td>");
                tr.append("<td >" + response[i].USERNAME + "</td>");
                tr.append("<td >" + response[i].JOURNAL_TYPE + "</td>");
                tr.append("<td style='text-align: center'><input type='image' class='gvConversionView'   id='btnconversionview'   <img src='../Images/View.png' width='20' height ='20' title='View'/></input></td>");
                
                
                $("#CSDConversionGrid").append(tr);
            }
            tr.append("</tbody>");
            RowCountConversionList();
            $('#CSDConversionGrid').DataTable({
                "sScrollX": '100%',
                "sScrollXInner": "110%",
                "scrollY": "238px",
                "scrollCollapse": true,
                "dom": 'Bfrtip',
                buttons: [
                    {
                        extend: 'excelHtml5',
                        title: 'CSD Conversion List'
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

function ComboMrp(productid) {
    var returnValue = null;
    $.ajax({
        type: "POST",
        url: "/TranDepot/ComboMrp",
        data: { ProductID: productid },
        dataType: "json",
        async: false,
        success: function (responseMessage) {
            var mrp;
            
            $.each(responseMessage, function (key, item) {
                mrp = item.MRP;
                returnValue = mrp;
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

function bindconversioncomboproduct() {
    ////debugger;
    var Comboproduct = $("#ComboProductID");
    $.ajax({
        type: "POST",
        url: "/TranDepot/GetConversionComboProduct",
        data: '{}',
        async: false,
        dataType: "json",
        success: function (response) {
            ////debugger;
            Comboproduct.empty().append('<option selected="selected" value="0">Please select</option>');;
            $.each(response, function () {
                Comboproduct.append($("<option></option>").val(this['PRODUCTID']).html(this['PRODUCTNAME']));
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

function bindconversioncomboproductdetails(comboproductid) {
    ////debugger;
    
    $.ajax({
        type: "POST",
        url: "/TranDepot/GetCSDComboProductDetails",
        data: { ComboProductID: comboproductid },
        async: false,
        dataType: "json",
        success: function (comboproductdetailslist) {
            ////debugger;
            if (comboproductdetailslist.length > 0) {
                if (comboproductdetailslist[0].ISSINGLECARTOON == '0') {
                    $('#spnQTY').text('Tot.Pcs = (No.of Case X 72 Pcs)');
                    $('#hdnSingleCartoon').val('0');
                }
                else {
                    $('#spnQTY').text('Tot.Box');
                    $('#hdnSingleCartoon').val('1');
                }
            }
            else {
                $('#spnQTY').text('');
                $('#hdnSingleCartoon').val('');
            }
        },
        failure: function (comboproductdetailslist) {
            alert(comboproductdetailslist.responseText);
        },
        error: function (comboproductdetailslist) {
            alert(comboproductdetailslist.responseText);
        }
    });
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

function EditDetails(conversionid) {
    ////debugger;
    var tr;
    
    var HeaderCount = 0;
    
    var srl = 0;
    srl = srl + 1; 
    
    var ComboProduct = $("#ComboProductID");
    
    ////debugger;
    

    $('#conversionDetailsGrid').empty();
    

    $("#dialog").dialog({
        autoOpen: true,
        modal: true,
        title: "Loading.."
    });
    $("#imgLoader").css("visibility", "visible");
    $.ajax({
        type: "POST",
        url: "/TranDepot/CsdConversionEdit",
        data: { ConversionID: conversionid},
        dataType: "json",
        async: false,
        success: function (response) {
            //alert(JSON.stringify(response))
            ////debugger;
            var listHeader = response.ConversioneditDataset.varHeader;
            var listDetails = response.ConversioneditDataset.varDetails;
            
            ////debugger;

            /*Invoice Header Info*/
            $.each(listHeader, function (index, record) {
                //////debugger;
                $("#hdndispatchID").val(this['ADJUSTMENTID'].toString().trim());
                $("#FGInvoiceNo").val(this['ADJUSTMENTNO'].toString().trim());
                $("#InvoiceDate").val(this['ADJUSTMENTDATE'].toString().trim());
            });

            
            /*Invoice Details Info*/
            if (listDetails.length > 0) {

                $("#conversionDetailsGrid").empty();
                
                    
                tr;
                tr = $('#conversionDetailsGrid');
                HeaderCount = $('#conversionDetailsGrid thead th').length;
                    
                if (HeaderCount == 0) {
                    tr.append("<thead><tr><th style='text-align: center;'>Sl.No.</th><th style='display: none;'>Product ID</th><th>Product</th><th>Batch</th><th>MRP</th><th style='display: none;'>Stock Qty(Pcs)</th><th>Mfg.Date</th><th>Exp.Date</th><th style='display: none;'>Storelocation</th><th style='text-align: center;'>Conversion Qty(Pcs)</th><th style='text-align: center;'>Delete</th></tr></thead><tbody>");

                }
                $.each(listDetails, function (index, record) {

                    tr = $('<tr/>');
                    tr.append("<td style='text-align: center;'><label class='slno' id='lblconversionslno'></label></td>");//0
                    tr.append("<td style='display: none;'>" + this['PRODUCTID'].toString().trim() + "</td>");//1
                    tr.append("<td style='width:250px;'>" + this['PRODUCTNAME'].toString().trim() + "</td>");//2
                    tr.append("<td >" + this['BATCHNO'].toString().trim() + "</td>");//3
                    tr.append("<td style='text-align: right;'>" + this['MRP'].toString().trim() + "</td>");//4
                    tr.append("<td style='text-align: right;display: none;'>" + this['INVOICESTOCKQTY'].toString().trim() + "</td>");//5
                    tr.append("<td >" + this['MFDATE'].toString().trim() + "</td>");//6
                    tr.append("<td >" + this['EXPRDATE'].toString().trim() + "</td>");//7
                    tr.append("<td style='display: none;'>" + this['STORELOCATIONID'].toString().trim() + "</td>");//8
                    tr.append("<td style='text-align: right;'>" + this['ADJUSTMENTQTY'].toString().trim() + "</td>");//9
                    tr.append("<td style='text-align: center'><input type='image' class='gvConversionDetailsDelete'  id='btnConversionDetailsdelete' <img src='../Images/ico_delete_16.png' title='Delete'/></input></td>")//10;
                    $("#conversionDetailsGrid").append(tr);
                    tr.append("</tbody>");
                    ConversionDetailsRowCount();

                    if (this['PRODUCTTYPE'].toString().trim() == 'C') {

                        $("#ComboProductID").val(this['PRODUCTID'].toString().trim());
                        $("#ConversionQty").val(parseInt(this['ADJUSTMENTQTY'].toString().trim()));
                    }
                });
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



