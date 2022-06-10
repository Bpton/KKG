//#region Developer Info
/*
* For InvoiceCSD.cshtml Only
* Developer Name : Avishek Ghosh 
* Start Date     : 20/07/2020
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
    //debugger;
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
    bindsourceDepot($("#hdnUserID").val().trim());
    $("#SearchDepotID").chosen({
        search_contains: true
    });
    $("#SearchDepotID").trigger("chosen:updated");
    bindPacksize();
    bindcsdinvoicegrid();
    binCsdComboProduct(BusinessSegment);

    $("#BRID").prop("disabled", true);
    $("#CUSTOMERID").prop("disabled", false);
    $("#SaleOrderID").prop("disabled", false);


    $("#btnsearch").click(function (e) {
        $("#dialog").dialog({
            autoOpen: true,
            modal: true,
            title: "Loading.."
        });
        $("#imgLoader").css("visibility", "visible");
        setTimeout(function () {
            bindcsdinvoicegrid();
            $("#imgLoader").css("visibility", "hidden");
            $("#dialog").dialog("close");
        }, 5);
        e.preventDefault();
    })

    $("#btnAddnew").click(function (e) {
        //debugger;
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
    })

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
            //ReleaseSession();
            bindcsdinvoicegrid();
            $("#imgLoader").css("visibility", "hidden");
            $("#dialog").dialog("close");
        }, 5);
        e.preventDefault();
    })

    $('#TransporterID').change(function (e) {       
        $("#CUSTOMERID").focus();
        $('#CUSTOMERID').trigger('chosen:activate');
        e.preventDefault();
    })

    $('#SearchDepotID').change(function (e) {
        if ($('#SearchDepotID').val().trim() != '0') {

            $("#BRID").val($('#SearchDepotID').val().trim());
            bindcsdinvoicegrid();
        }
        e.preventDefault();
    })

    $('#Tranportmode').change(function (e) {

        $("#VehichleNo").focus();
        e.preventDefault();
    })

    $('#PaymentMode').change(function (e) {

        $("#LrGrNo").focus();
        e.preventDefault();
    })

    $('#CUSTOMERID').change(function (e) {

        //var cpc_code_status = '';
        //debugger;

            if ($('#CUSTOMERID').val() != '0') {

                clearafterCustomerSelection();
                //cpc_code_status = CodeStatus($('#CUSTOMERID').val().trim());

                //if (cpc_code_status == 'Y') {

                    var MonthName = '';
                    var Month = $('#InvoiceDate').val().trim();
                    Month = Month.substr(3, 2);
                    switch (Month) {
                        case '01':
                            MonthName = "JAN.";
                            break;
                        case '02':
                            MonthName = "FEB.";
                            break;
                        case '03':
                            MonthName = "MAR.";
                            break;
                        case '04':
                            MonthName = "APR.";
                            break;
                        case '05':
                            MonthName = "MAY";
                            break;
                        case '06':
                            MonthName = "JUN.";
                            break;
                        case '07':
                            MonthName = "JUL.";
                            break;
                        case '08':
                            MonthName = "AUG.";
                            break;
                        case '09':
                            MonthName = "SEP.";
                            break;
                        case '10':
                            MonthName = "OCT.";
                            break;
                        case '11':
                            MonthName = "NOV.";
                            break;
                        case '12':
                            MonthName = "DEC.";
                            break;
                    }
                    $('#spnInvLimit').text('Inv Limit,Done & Bal For ' + MonthName + ' ');
                    $('#spnTGT').text('Tgt,Bal & Ach(%) For ' + MonthName + ' ');

                    $("#dialog").dialog({
                        autoOpen: true,
                        modal: true,
                        title: "Loading.."
                    });
                    $("#imgLoader").css("visibility", "visible");
                    setTimeout(function () {

                        bindtaxcount();
                        bindSaleorder($('#CUSTOMERID').val().trim());
                        ClBalanceCrLimit($('#CUSTOMERID').val().trim(), $('#BRID').val().trim(), $('#hdndispatchID').val().trim(), $('#hdnFinYear').val().trim());
                        CustomerDetails($('#CUSTOMERID').val().trim(), $('#BRID').val().trim(), Month, $('#hdndispatchID').val().trim(), $('#hdnFinYear').val().trim());

                        $("#SaleOrderID").chosen({
                            search_contains: true
                        });
                        $("#SaleOrderID").trigger("chosen:updated");

                        $("#PRODUCTID").chosen({
                            search_contains: true
                        });
                        $("#PRODUCTID").trigger("chosen:updated");

                        $('#NetAmt2').val('0.00');
                        $("#imgLoader").css("visibility", "hidden");
                        $("#dialog").dialog("close");
                    }, 5);
                //}
                //else {
                //    $('#ClosingBalance').val('');
                //    $('#CreditLimit').val('');
                //    $('#Target').val('');
                //    $('#Balance').val('');
                //    $('#AchPercentage').val('');
                //    $('#InvoiceLimit').val('');
                //    $('#InvoiceDone').val('');
                //    $('#InvoiceBalance').val('');
                //    $('#spnTGT').text('');
                //    $('#spnInvLimit').text('');
                //    $('#NetAmt2').val('');

                //    $('#SaleOrderID').empty();
                //    $("#SaleOrderID").chosen({
                //        search_contains: true
                //    });
                //    $("#SaleOrderID").trigger("chosen:updated");

                //    $('#PRODUCTID').empty();
                //    $("#PRODUCTID").chosen({
                //        search_contains: true
                //    });
                //    $("#PRODUCTID").trigger("chosen:updated");
                //    toastr.warning('<b><font color=black>Customer does not have CPC code,please contact with admin...!</font></b>');
                //    return false;
                //}
            }
            else if ($('#CUSTOMERID').val() == '0') {
                $('#ClosingBalance').val('');
                $('#CreditLimit').val('');
                $('#Target').val('');
                $('#Balance').val('');
                $('#AchPercentage').val('');
                $('#InvoiceLimit').val('');
                $('#InvoiceDone').val('');
                $('#InvoiceBalance').val('');
                $('#spnTGT').text('');
                $('#spnInvLimit').text('');
                $('#NetAmt2').val('');
                $('#ICDSNo').val('');
                $('#ICDSDate').val('');

                $('#SaleOrderID').empty();
                $("#SaleOrderID").chosen({
                    search_contains: true
                });
                $("#SaleOrderID").trigger("chosen:updated");

                $('#PRODUCTID').empty();
                $("#PRODUCTID").chosen({
                    search_contains: true
                });
                $("#PRODUCTID").trigger("chosen:updated");
            }
        e.preventDefault();
    });

    $('#SaleOrderID').change(function (e) {
        //debugger;
        $("#dialog").dialog({
            autoOpen: true,
            modal: true,
            title: "Loading.."
        });
        $("#imgLoader").css("visibility", "visible");
        setTimeout(function () {

            if ($('#SaleOrderID').val() != '0') {
                //debugger;
                bindICDS($('#SaleOrderID').val().trim());
                bindCPCProduct($('#SaleOrderID').val().trim());
                ChangeProductColour($('#SaleOrderID').val().trim(), $('#PSID').val().trim(), $('#hdndispatchID').val().trim());

               

                $("#PRODUCTID").chosen({
                    search_contains: true
                });
                $("#PRODUCTID").trigger("chosen:updated");

                
                $("#PRODUCTID").focus();
                $('#PRODUCTID').trigger('chosen:activate');

            }
            else if ($('#SaleOrderID').val() == '0') {

                $('#ICDSNo').val('');
                $('#ICDSDate').val('');

                $('#PRODUCTID').empty();
                $("#PRODUCTID").chosen({
                    search_contains: true
                });
                $("#PRODUCTID").trigger("chosen:updated");
            }

            $("#imgLoader").css("visibility", "hidden");
            $("#dialog").dialog("close");
        }, 5);


        e.preventDefault();
    });

    $('#PRODUCTID').change(function (e) {
        //debugger;
        $("#dialog").dialog({
            autoOpen: true,
            modal: true,
            title: "Loading.."
        });
        $("#imgLoader").css("visibility", "visible");
        setTimeout(function () {

            var product = $("#PRODUCTID").val();
            if (product != '0') {
                $('#ddlBatch').empty();
                bindbatchno('0');
                bindorderdetails($('#SaleOrderID').val().trim(), $('#PRODUCTID').val().trim(), $('#PSID').val().trim(), $('#hdndispatchID').val().trim());
            }
            else {
                $('#ddlBatch').empty();
                $("#RemainingQty").val('');
                $("#StockQty").val('');
                $("#OrderQty").val('');
                $("#DeliveredQty").val('');
                clearafterAdd();
            }
            

            $("#imgLoader").css("visibility", "hidden");
            $("#dialog").dialog("close");
        }, 5);

        e.preventDefault();
    })

    $('#PSID').change(function (e) {
        $("#dialog").dialog({
            autoOpen: true,
            modal: true,
            title: "Loading.."
        });
        $("#imgLoader").css("visibility", "visible");
        setTimeout(function () {
            var product = $("#PRODUCTID").val();
            var packsize = $("#PSID").val();
            if (product != '0' && packsize != '0') {
                bindbatchno('0');
                //$("#InvoiceQty").trigger("focus");
            }
            else {
                $('#ddlBatch').empty();
            }
            $("#ddlBatch").focus();
            clearafterAdd();

            $("#imgLoader").css("visibility", "hidden");
            $("#dialog").dialog("close");
        }, 5);
        e.preventDefault();
    })

    $('#ddlBatch').change(function (e) {
        $("#dialog").dialog({
            autoOpen: true,
            modal: true,
            title: "Loading.."
        });
        $("#imgLoader").css("visibility", "visible");
        setTimeout(function () {

            getBatchDetails('0');
            //$("#InvoiceQty").focus();

            $("#imgLoader").css("visibility", "hidden");
            $("#dialog").dialog("close");
        }, 5);

        e.preventDefault();
    })

    $("#btnadd").click(function (e) {
        //debugger;
        if ($("#chkFree").prop('checked') == false) {
            if ($('#CUSTOMERID').val() == '0') {
                toastr.warning('<b><font color=black>Please select customer..!</font></b>');
                return false;
            }
            else if ($('#ddlBatch').val() == '0') {
                toastr.warning('<b><font color=black>Please select Batch No..!</font></b>');
                return false;
            }
            else if ($('#Rate').val() == '') {
                toastr.warning('<b><font color=black>Please select Batch No..!</font></b>');
                return false;
            }
            else if ($('#MRP').val() == '') {
                toastr.warning('<b><font color=black>Please select Batch No..!</font></b>');
                return false;
            }
            else if ($('#StockQty').val() == '') {
                toastr.warning('<b><font color=black>Please select Batch No..!</font></b>');
                return false;
            }
            else if ($('#InvoiceQty').val() == '' && $('#InvoicePcs').val() == '') {
                toastr.warning('<b><font color=black>Please enter invoice quantity..!</font></b>');
                return false;
            }
            else if ($('#InvoiceQty').val() == '' && parseFloat($('#InvoicePcs').val()) == 0) {
                toastr.warning('<b><font color=black>Please enter invoice quantity..!</font></b>');
                return false;
            }
            else if ($('#InvoiceQty').val() == '' && parseFloat($('#InvoicePcs').val()) > 0) {
                toastr.warning('<b><font color=black>Please enter 0 in case..!</font></b>');
                return false;
            }
            else if (parseFloat($('#InvoiceQty').val()) > 0 && $('#InvoicePcs').val() =='') {
                toastr.warning('<b><font color=black>Please enter 0 in pcs..!</font></b>');
                return false;
            }
            else if (parseFloat($('#InvoiceQty').val()) == 0 && parseFloat($('#InvoicePcs').val()) == 0) {
                toastr.warning('<b><font color=black>Invoice quantity should not be 0..!</font></b>');
                return false;
            }
            else {

                $("#dialog").dialog({
                    autoOpen: true,
                    modal: true,
                    title: "Loading.."
                });
                $("#imgLoader").css("visibility", "visible");
                setTimeout(function () {

                    addProduct();

                    $("#imgLoader").css("visibility", "hidden");
                    $("#dialog").dialog("close");
                }, 3);


            }
        }
        else if ($("#chkFree").prop('checked') == true) {
            if ($('#CUSTOMERID').val() == '0') {
                toastr.warning('<b><font color=black>Please select customer..!</font></b>');
                return false;
            }
            else if ($('#ddlBatch').val() == '0') {
                toastr.warning('<b><font color=black>Please select Batch No..!</font></b>');
                return false;
            }
            else if ($('#Rate').val() == '') {
                toastr.warning('<b><font color=black>Please select Batch No..!</font></b>');
                return false;
            }
            else if ($('#MRP').val() == '') {
                toastr.warning('<b><font color=black>Please select Batch No..!</font></b>');
                return false;
            }
            else if ($('#StockQty').val() == '') {
                toastr.warning('<b><font color=black>Please select Batch No..!</font></b>');
                return false;
            }
            else if ($('#InvoiceQty').val() == '' && $('#InvoicePcs').val() == '') {
                toastr.warning('<b><font color=black>Please enter invoice quantity..!</font></b>');
                return false;
            }
            else if ($('#InvoiceQty').val() == '' && parseFloat($('#InvoicePcs').val()) == 0) {
                toastr.warning('<b><font color=black>Please enter invoice quantity..!</font></b>');
                return false;
            }
            else if ($('#InvoiceQty').val() == '' && parseFloat($('#InvoicePcs').val()) > 0) {
                toastr.warning('<b><font color=black>Please enter 0 in case..!</font></b>');
                return false;
            }
            else if (parseFloat($('#InvoiceQty').val()) > 0 && $('#InvoicePcs').val() == '') {
                toastr.warning('<b><font color=black>Please enter 0 in pcs..!</font></b>');
                return false;
            }
            else if (parseFloat($('#InvoiceQty').val()) == 0 && parseFloat($('#InvoicePcs').val()) == 0) {
                toastr.warning('<b><font color=black>Invoice quantity should not be 0..!</font></b>');
                return false;
            }
            else {
                $("#dialog").dialog({
                    autoOpen: true,
                    modal: true,
                    title: "Loading.."
                });
                $("#imgLoader").css("visibility", "visible");
                setTimeout(function () {

                    addProduct();

                    $("#imgLoader").css("visibility", "hidden");
                    $("#dialog").dialog("close");
                }, 3);

            }
        }
        e.preventDefault();
    })

    $("#btnsave").click(function (e) {
        //debugger;


        entryLockstatus = bindlockdateflag($("#InvoiceDate").val(), $("#hdnFinYear").val());
        if (entryLockstatus == '0') {
            toastr.warning('<b><font color=black>Entry date is locked,please contact with admin...!</font></b>');
            return false;
        }
        else {
            //debugger;
            var itemrowCount = document.getElementById("productDetailsGrid").rows.length - 2;

            if ($("#CUSTOMERID").val() == '0') {
                toastr.warning('<b><font color=black>Please select customer..!</font></b>');
                return false;
            }
            else if ($("#TransporterID").val() == '0') {
                toastr.warning('<b><font color=black>Please select transporter..!</font></b>');
                return false;
            }
            else if ($("#ICDSNo").val() == '') {
                toastr.warning('<b><font color=black>Please enter ICDS No..!</font></b>');
                return false;
            }
            else if ($("#ICDSDate").val() == '') {
                toastr.warning('<b><font color=black>Please enter ICDS Date..!</font></b>');
                return false;
            }
            else if ($("#NetAmt").val() == '') {
                toastr.warning('<b><font color=black>Invoice amount should not be blank..!</font></b>');
                return false;
            }
            else if (parseFloat($("#NetAmt").val()) <= 0) {
                toastr.warning('<b><font color=black>Invoice amount should not be 0..!</font></b>');
                return false;
            }
            else if (itemrowCount <= 0) {
                toastr.warning('<b><font color=black>Please add Product..!</font></b>');
                return false;
            }
            else {
                SaveInvoice();
            }
        }
        e.preventDefault();
    })

    $('#FREEPRODUCTID').change(function (e) {

        $("#dialog").dialog({
            autoOpen: true,
            modal: true,
            title: "Loading.."
        });
        $("#imgLoader").css("visibility", "visible");
        setTimeout(function () {

            var product = $("#FREEPRODUCTID").val().trim();
            if (product != '0') {

                FreeProductBatchDetails($('#BRID').val().trim(), $('#FREEPRODUCTID').val().trim(), 'B9F29D12-DE94-40F1-A668-C79BF1BF4425',
                    '0', $('#CUSTOMERID').val().trim(), $('#InvoiceDate').val().trim(), menuid.trim(), BusinessSegment.trim(),
                    $('#hdnGroupID').val().trim(), '0', '113BD8D6-E5DC-4164-BEE7-02A16F97ABCC');
            }
            else {
                $('#freeProductDetailsGrid').empty();
            }

            $("#imgLoader").css("visibility", "hidden");
            $("#dialog").dialog("close");
        }, 5);
        e.preventDefault();
    })

    $("#btnaddFreeProduct").click(function (e) {
        var freeBatchrowCount = 0;
        freeBatchrowCount = document.getElementById("freeProductDetailsGrid").rows.length - 1;
        if (freeBatchrowCount <= 0) {

            toastr.error('<b>Please select product..!</b>');
            return false;
        }
        else {
            //debugger;
            var freeqty = 0;
            var totalfreeqty = 0;
            var stockflag = true;
            var BatchNo = '';
            if ($('#freeProductDetailsGrid').length) {

                $('#freeProductDetailsGrid tbody tr').each(function () {
                    //debugger;
                    BatchNo = $(this).find('td:eq(3)').html().trim();
                    if ($(this).find('#txtfreequantity').val().trim() == '') {
                        freeqty = parseInt('0');
                    }
                    else {
                        if (parseInt($(this).find('#txtfreequantity').val().trim()) > parseInt($(this).find('td:eq(4)').html().trim())) {
                            stockflag = false;
                            return false;
                        }
                        else {
                            freeqty = parseInt($(this).find('#txtfreequantity').val().trim());
                        }
                    }
                    totalfreeqty += parseInt(freeqty);
                });

                if (stockflag == false) {
                    toastr.error('<b>Stock not available for Batch - ' + BatchNo + '</b>');
                    return false;
                }
                else {
                    if (parseInt(totalfreeqty) <= 0) {
                        toastr.error('<b>Please enter free quantity..!</b>');
                        return false;
                    }
                    else if (parseInt(totalfreeqty) > parseInt($('#SchemeQuantity').val().trim())) {
                        toastr.error('<b>Free quantity should be - ' + parseInt($('#SchemeQuantity').val().trim()) + ' </b>');
                        return false;
                    }
                    else {
                        if ($('#freeProductDetailsGrid').length) {
                            var flag = false;
                            var count = 0;
                            var inputProductId = '';
                            var inputProductName = '';
                            var inputBatchNo = '';
                            var inputStockQty = '';
                            var inputMrp = '';
                            var inputRate = '';
                            var inputMfgDate = '';
                            var inputExpDate = '';
                            var inputFreeQty = '';
                            var inputWeight = '';


                            $('#freeProductDetailsGrid tbody tr').each(function () {
                                //debugger;
                                if ($(this).find('#txtfreequantity').val() != '' && $(this).find('#txtfreequantity').val() != '0') {
                                    flag = true;
                                    count = count + 1;
                                    inputProductId = $(this).find('td:eq(1)').html().trim();
                                    inputProductName = $(this).find('td:eq(2)').html().trim();
                                    inputBatchNo = $(this).find('td:eq(3)').html().trim();
                                    inputStockQty = $(this).find('td:eq(4)').html().trim();
                                    inputMrp = $(this).find('td:eq(5)').html().trim();
                                    inputRate = $(this).find('td:eq(6)').html().trim();
                                    inputMfgDate = $(this).find('td:eq(7)').html().trim();
                                    inputExpDate = $(this).find('td:eq(8)').html().trim();
                                    inputFreeQty = $(this).find('#txtfreequantity').val().trim();
                                    inputWeight = $(this).find('td:eq(10)').html().trim();

                                    IsExistsInputFreeProduct(inputProductId, inputProductName, inputBatchNo, inputStockQty,
                                        inputMrp, inputRate, inputMfgDate, inputExpDate, inputFreeQty, inputWeight);

                                    $(this).find('#txtfreequantity').val('');
                                }
                            });


                            if (flag == false) {
                                inputProductId = '';
                                inputProductName = '';
                                inputBatchNo = '';
                                inputStockQty = '';
                                inputMrp = '';
                                inputRate = '';
                                inputMfgDate = '';
                                inputExpDate = '';
                                inputFreeQty = '';
                                inputWeight = '';
                                toastr.error('<b>Please enter free quantity...!</b>');
                                return false;
                            }
                        }
                    }
                }
            }
        }
        e.preventDefault();
    })

    $("#btnsavepopup").click(function (e) {

        var freefinalBatchrowCount = 0;
        freefinalBatchrowCount = document.getElementById("finalfreeDetailsGrid").rows.length - 2;
        if (freefinalBatchrowCount <= 0) {

            toastr.error('<b>Please add product..!</b>');
            return false;
        }
        else {

            //debugger;
            var finalfreeqty = 0;
            var totalfinalfreeqty = 0;

            $('#finalfreeDetailsGrid tbody tr').each(function () {
                //debugger;
                finalfreeqty = parseInt($(this).find('td:eq(9)').html().trim());
                totalfinalfreeqty += parseInt(finalfreeqty);
            });

            if (parseInt(totalfinalfreeqty) > parseInt($('#SchemeQuantity').val().trim())) {
                toastr.error('<b>Free quantity should be - ' + parseInt($('#SchemeQuantity').val().trim()) + ' </b>');
                return false;
            }
            else {

                $("#dialog").dialog({
                    autoOpen: true,
                    modal: true,
                    title: "Loading.."
                });
                $("#imgLoader").css("visibility", "visible");
                setTimeout(function () {

                    if ($('#finalfreeDetailsGrid').length) {
                        var flag = false;
                        var count = 0;
                        var finalProductId = '';
                        var finalProductName = '';
                        var finalBatchNo = '';
                        var finalMrp = '';
                        var finalRate = '';
                        var finalMfgDate = '';
                        var finalExpDate = '';
                        var finalFreeQty = '';
                        var finalFreeWeight = '';
                        var finalFreeAmount = '';

                        $('#finalfreeDetailsGrid tbody tr').each(function () {
                            //debugger;
                            flag = true;
                            count = count + 1;
                            finalProductId = $(this).find('td:eq(1)').html().trim();
                            finalProductName = $(this).find('td:eq(2)').html().trim();
                            finalBatchNo = $(this).find('td:eq(3)').html().trim();
                            finalMrp = $(this).find('td:eq(5)').html().trim();
                            finalRate = $(this).find('td:eq(6)').html().trim();
                            finalMfgDate = $(this).find('td:eq(7)').html().trim();
                            finalExpDate = $(this).find('td:eq(8)').html().trim();
                            finalFreeQty = $(this).find('td:eq(9)').html().trim();
                            finalFreeWeight = $(this).find('td:eq(10)').html().trim();
                            finalFreeAmount = parseFloat(finalFreeQty * finalRate).toFixed(2);

                            /*Add Free Product From Popup Start*/
                            addFreeDetailsGrid($('#hdnqtyschemeid').val().trim(), $('#PRODUCTID').val().trim(), $('#PRODUCTID').find('option:selected').text().trim(),
                                $('#hdnbatchno').val().trim(), $('#hdnschappqty').val().trim(), finalProductId.trim(), finalProductName.trim(), 'B9F29D12-DE94-40F1-A668-C79BF1BF4425',
                                'PCS', finalFreeQty.trim(), finalMrp.trim(), finalRate.trim(), finalFreeAmount.trim(), finalBatchNo.trim(), finalMfgDate.trim(),
                                finalExpDate.trim(), finalFreeWeight.trim(), $('#qsguid').val().trim());
                            /*Add Free Product From Popup End*/
                        });
                    }


                    /*Price Scheme Method Start*/
                    PriceScheme($('#PRODUCTID').val().trim(), $('#PRODUCTID').find('option:selected').text().trim(), $("#hdnbatchno").val().trim(),
                        $('#PSID').val().trim(), $('#PSID').find('option:selected').text().trim(), 'B9F29D12-DE94-40F1-A668-C79BF1BF4425',
                        '1970C78A-D062-4FE9-85C2-3E12490463AF', $('#hdnbillqty').val().trim(), $('#hdntaxnameCGST').val().trim(),
                        $('#hdntaxnameSGST').val().trim(), $('#hdntaxnameIGST').val().trim(), $('#InvoiceDate').val().trim(),
                        $('#MRP').val().trim(), parseFloat($('#Rate').val().trim()).toFixed(2), $('#hdnpriceschemeid').val().trim(),
                        $('#hdnpriceschemepercentage').val().trim(), $('#hdnpriceschemevalue').val().trim(), $('#SSMargin').val().trim(),
                        $('#hdndiscountvalue').val().trim(), $('#hdnHSNCode').val().trim(),
                        $('#hdn_mfgdate').val().trim(), $('#hdn_exprdate').val().trim(), $('#qsguid').val().trim(), $('#qsheader').val().trim());
                    /*Price Scheme Method Start*/


                    $("#dvQtyScheme").dialog("close");
                    $("#FREEPRODUCTID").empty();
                    $("#SchemeQuantity").val('');
                    $("#freeProductDetailsGrid").empty();
                    $("#finalfreeDetailsGrid").empty();

                    $("#imgLoader").css("visibility", "hidden");
                    $("#dialog").dialog("close");
                }, 5);
            }
        }
        e.preventDefault();
    });

    $("#btnclosepopup").click(function (e) {

        $("#dvQtyScheme").dialog("close");
        $("#FREEPRODUCTID").empty();
        $("#SchemeQuantity").val('');
        $("#freeProductDetailsGrid").empty();
        $("#finalfreeDetailsGrid").empty();
        e.preventDefault();
    })

    $("#dvQtyScheme").dialog({
        autoOpen: false,
        maxWidth: 1300,
        maxHeight: 580,
        width: 1300,
        height: 580,
        resizable: false,
        draggable: false,
        modal: true,
        closeOnEscape: false,
        open: function (event, ui) { $(".ui-dialog-titlebar-close").hide(); },
        show: {
            effect: "blind",
            duration: 900
        },
        hide: {
            effect: "explode",
            duration: 700
        }
    });

});

function loadAddNewRecord() {

    //debugger;
    finyearChecking($("#hdnFinYear").val().trim());
    bindtransporter($("#BRID").val().trim(), '0');
    //var accpostingstatus = AccountPostingStatus($('#BRID').val().trim(), BusinessSegment.trim(), $('#InvoiceDate').val().trim(), $("#hdnUserID").val().trim());
    //var dayendstatus = DayEndStatus($('#BRID').val().trim(), BusinessSegment.trim(), $('#InvoiceDate').val().trim(), $("#hdnUserID").val().trim());
    //if (accpostingstatus != 'na') {
    //    $('#dvAdd').css("display", "none");
    //    $('#dvDisplay').css("display", "");
    //    toastr.warning('<b><font color=black>' + accpostingstatus + '</font></b>');
    //    return false;
    //}
    //else if (dayendstatus != 'na') {
    //    $('#dvAdd').css("display", "none");
    //    $('#dvDisplay').css("display", "");
    //    toastr.warning('<b><font color=black>' + dayendstatus + '</font></b>');
    //    return false;
    //}
    //else {
        $("#dialog").dialog({
            autoOpen: true,
            modal: true,
            title: "Loading.."
        });
        $("#imgLoader").css("visibility", "visible");
        setTimeout(function () {
            $('#dvAdd').css("display", "");
            $('#dvDisplay').css("display", "none");
            bindCustomer(BusinessSegment, $('#BRID').val().trim(), $('#hdndispatchID').val().trim());
            ClearControls();
            //ReleaseSession();
            $("#imgLoader").css("visibility", "hidden");
            $("#dialog").dialog("close");
        }, 3);
    //}
}

function bindtaxcount() {
    $.ajax({
        type: "POST",
        url: "/TranDepot/GetInvoiceTaxcount",
        data: { MenuID: $('#hdnmenuid').val(), Flag: '1', DepotID: $('#BRID').val(), ProductID: '0', CustomerID: $('#CUSTOMERID').val(), Date: $('#InvoiceDate').val() },
        dataType: "json",
        async: false,
        success: function (invoicetaxcount) {
            //debugger;
            if (invoicetaxcount.length > 0) {
                if (invoicetaxcount.length == 1) {
                    $.each(invoicetaxcount, function (key, item) {
                        //debugger;
                        $("#hdntaxcount").val('1');
                        $("#hdntaxnameIGST").val(invoicetaxcount[0].TAXNAME);
                        $("#hdntaxpercentage").val(invoicetaxcount[0].TAXPERCENTAGE);
                        $("#hdnrelatedto").val(invoicetaxcount[0].TAXRELATEDTO);
                    });
                }
                else if (invoicetaxcount.length == 2) {
                    //debugger;
                    $.each(invoicetaxcount, function (key, item) {
                        //debugger;
                        $("#hdntaxcount").val('2');
                        $("#hdntaxnameCGST").val(invoicetaxcount[0].TAXNAME);
                        $("#hdntaxnameSGST").val(invoicetaxcount[1].TAXNAME);
                        $("#hdntaxpercentage").val(invoicetaxcount[0].TAXPERCENTAGE);
                        $("#hdnrelatedto").val(invoicetaxcount[0].TAXRELATEDTO);
                    });
                }
            }
            else {
                $("#hdntaxcount").val(0);
                $("#hdntaxnameIGST").val('');
                $("#hdntaxnameCGST").val('');
                $("#hdntaxnameSGST").val('');
            }
        },
        failure: function (invoicetaxcount) {
            alert(invoicetaxcount.responseText);
        },
        error: function (invoicetaxcount) {
            alert(invoicetaxcount.responseText);
        }
    });

}

function bindCPCProduct(saleorderid) {
    var Product = $("#PRODUCTID");

    $.ajax({
        type: "POST",
        url: "/TranDepot/GetMTProduct",
        data: { SaleorderID: saleorderid},
        async: false,
        dataType: "json",
        success: function (response) {
            //debugger;
            Product.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(response, function () {
                Product.append($("<option></option>").val(this['PRODUCTID']).html(this['PRODUCTNAME']));
            });
            $("#hdnGroupID").val(response[0].GROUPID);
            $("#OrderDate").val(response[0].SALEORDERDATE);
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function ChangeProductColour(saleorderid,packsizeid,invoiceid) {
    $("#PRODUCTID > option").each(function () {
        bindorderdetails(saleorderid.trim(), this.value.trim(), packsizeid.trim(), invoiceid.trim());
        $("#OrderQty").val('');
        $("#StockQty").val('');
        $("#DeliveredQty").val('');
        $("#RemainingQty").val('');
    });

    
}

function ClBalanceCrLimit(customerid, depotid, invoiceid, finyear) {
    $.ajax({
        type: "POST",
        url: "/TranDepot/GTCustomerOutstanding",
        data: { CustomerID: customerid, DepotID: depotid, InvoiceID: invoiceid, FinYear: finyear },
        dataType: "json",
        async: false,
        success: function (response) {
            //alert(JSON.stringify(response))
            var listOutstanding = response.allOutStandingDataset.varOutstanding;
            var listCreditlimit = response.allOutStandingDataset.varCreditLimit;

            /*Outstanding Info*/
            if (listOutstanding.length > 0) {
                $.each(listOutstanding, function (index, record) {
                    ////debugger;
                    $("#ClosingBalance").val(this['OUTSTANDING'].toString().trim());
                });
            }
            else {
                $("#ClosingBalance").val('0.00');
            }

            /*Credit Limit Info*/
            if (listCreditlimit.length > 0) {
                $.each(listCreditlimit, function (index, record) {
                    ////debugger;
                    $("#CreditLimit").val(this['CREDIT_LIMIT'].toString().trim());
                });
            }
            else {
                $("#CreditLimit").val('0.00');
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

function CustomerTargetDetails(customerid, depotid, bsid, monthid, invoiceid, finyear) {
    $.ajax({
        type: "POST",
        url: "/TranDepot/GetCustomerTarget",
        data: { CustomerID: customerid, DepotID: depotid, BSID: bsid, MonthID: monthid, InvoiceID: invoiceid, FinYear: finyear },
        async: false,
        dataType: "json",
        success: function (target) {
            if (target.length > 0) {
                $.each(target, function (key, item) {
                    $("#Target").val(item.TARGETAMT);
                    $("#Balance").val(item.BALANCE);
                    $("#AchPercentage").val(item.ACHIEVEMENT);
                });

                if (parseFloat($("#AchPercentage").val()) >= 0 && parseFloat($("#AchPercentage").val()) < 41) {
                    $("#AchPercentage").css("background-color", "LightCoral");
                }
                else if (parseFloat($("#AchPercentage").val()) > 40 && parseFloat($("#AchPercentage").val()) < 91) {
                    $("#AchPercentage").css("background-color", "Yellow");
                }
                else if (parseFloat($("#AchPercentage").val()) > 90) {
                    $("#AchPercentage").css("background-color", "LightGreen");
                }
                else {
                    $("#AchPercentage").css("background-color", "");
                }
            }
            else {
                $("#Target").val('0.00');
                $("#Balance").val('0.00');
                $("#AchPercentage").val('0.00');
            }
        },
        failure: function (target) {
            alert(target.responseText);
        },
        error: function (target) {
            alert(target.responseText);
        }
    });

}

function CustomerDetails(customerid, depotid, monthid, invoiceid, finyear) {
    $.ajax({
        type: "POST",
        url: "/TranDepot/GTCustomerDetails",
        data: { CustomerID: customerid, DepotID: depotid, MonthID: monthid, InvoiceID: invoiceid, FinYear: finyear },
        dataType: "json",
        async: false,
        success: function (response) {
            //alert(JSON.stringify(response))
            var listInvoiceLimit = response.alldetailsDataset.varInvoiceLimit;
            var listSSMargin = response.alldetailsDataset.varSSMargin;
            var listGstStatus = response.alldetailsDataset.varGSTStatus;

            /*Invoice Limit Info*/
            if (listInvoiceLimit.length > 0) {
                $.each(listInvoiceLimit, function (index, record) {
                    ////debugger;
                    $("#InvoiceLimit").val(this['INVOICELIMIT'].toString().trim());
                    $("#InvoiceDone").val(this['INVOICEDONE'].toString().trim());
                    $("#InvoiceBalance").val(this['INVOICEBALNCE'].toString().trim());
                });

                if ($("#InvoiceBalance").val() != '') {
                    if (parseInt($("#InvoiceBalance").val().trim()) == 0) {
                        $("#InvoiceBalance").css("background-color", "LightCoral");
                    }
                    else if (parseInt($("#InvoiceBalance").val().trim()) == 1) {
                        $("#InvoiceBalance").css("background-color", "Yellow");
                    }
                    else {
                        $("#InvoiceBalance").css("background-color", "");
                    }
                }
                //if ($("#InvoiceBalance").val() != '') {
                //    if (parseInt($("#InvoiceBalance").val().trim()) <= 0) {
                //        $("#btnadd").attr("disabled", "disabled");
                //        toastr.warning('<b><font color=black>Maximum invoice limit ' + $("#InvoiceDone").val().trim() + ' <br /> already reached for <br /> ' + $("#CUSTOMERID option:selected").text() + '<br />Please contact with admin...!</font></b>');

                //    }
                //    else {
                //        $("#btnadd").prop("disabled", false);
                //    }
                //}
                //else {
                //    $("#btnadd").prop("disabled", false);
                //}
            }
            else {
                $("#InvoiceLimit").val('0');
                $("#InvoiceDone").val('0');
                $("#InvoiceBalance").val('0');
            }

            /*SS Margin Info*/
            if (listSSMargin.length > 0) {
                $.each(listSSMargin, function (index, record) {
                    ////debugger;
                    $("#SSMargin").val(this['ADDSSMARGINPERCENTAGE'].toString().trim());
                });
            }
            else {
                $("#SSMargin").val('0');
            }

            /*GST Status Info*/
            if (listGstStatus[0].GSTSTATUS.toString().trim() == '1') {
                //debugger;
                toastr.warning('<b><font color=black>Customer doesnot have GST No...!</font></b>');
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

function addProduct() {
    if ($("#chkFree").prop('checked') == false) {
        if ($('#productDetailsGrid').length) {

            $('#productDetailsGrid').each(function () {
                ////debugger;
                var exists = false;
                var arraydetails = [];
                var count = $('#productDetailsGrid tbody tr').length;
                $('#productDetailsGrid tbody tr').each(function () {
                    var dispatchdetail = {};
                    var productidgrd = $(this).find('td:eq(1)').html().trim();
                    var batchgrd = $(this).find('td:eq(12)').html().trim();
                    dispatchdetail.PRODUCTID = productidgrd;
                    dispatchdetail.BATCHNO = batchgrd;
                    arraydetails.push(dispatchdetail);
                });

                var jsondispatchobj = {};
                jsondispatchobj.fgDispatchDetails = arraydetails;

                for (i = 0; i < jsondispatchobj.fgDispatchDetails.length; i++) {
                    if (jsondispatchobj.fgDispatchDetails[i].PRODUCTID.trim() == $('#PRODUCTID').val().trim() && jsondispatchobj.fgDispatchDetails[i].BATCHNO.trim() == $("#hdnbatchno").val().trim()) {
                        exists = true;
                        break;
                    }
                }
                if (exists != false) {
                    toastr.error('Item already exists...!');
                    return false;
                }
                else {
                    addProductInDetailsGrid();
                }
            })
        }
    }
    else if ($("#chkFree").prop('checked') == true) {

        if ($('#freeDetailsGrid').length) {
            $('#freeDetailsGrid').each(function () {
                ////debugger;
                var freeexists = false;
                var freearraydetails = [];
                var count = $('#freeDetailsGrid tbody tr').length;
                $('#freeDetailsGrid tbody tr').each(function () {
                    var freedetail = {};
                    var freeproductidgrd = $(this).find('td:eq(6)').html().trim();
                    var freebatchgrd = $(this).find('td:eq(16)').html().trim();
                    freedetail.FREEPRODUCTID = freeproductidgrd;
                    freedetail.FREEBATCHNO = freebatchgrd;
                    freearraydetails.push(freedetail);
                });

                var jsonfreeobj = {};
                jsonfreeobj.gtFreeDetails = freearraydetails;

                for (i = 0; i < jsonfreeobj.gtFreeDetails.length; i++) {
                    if (jsonfreeobj.gtFreeDetails[i].FREEPRODUCTID.trim() == $('#PRODUCTID').val().trim() && jsonfreeobj.gtFreeDetails[i].FREEBATCHNO.trim() == $("#hdnbatchno").val().trim()) {
                        freeexists = true;
                        break;
                    }
                }
                if (freeexists != false) {
                    toastr.error('Item already exists...!');
                    return false;
                }
                else {
                    addProductInFreeGrid();
                }
            })
        }
    }
}

function addProductInDetailsGrid() {
    //debugger;
    var srl = 0;
    var stockcheckingmessage = '';
    var productType = ''
    var priceschemediscount = ''
    var priceschemeidvalue = ''
    var priceschemeid = ''
    var priceschemepercentage = ''
    var priceschemevalue = ''
    var discountvalue = ''
    var stateID = ''
    var districtID = ''
    var zoneID = ''
    var teritoryID = ''
    var strPriceSchemeDiscount = '';
    var splitPriceSchemeDiscount = '';
    var strPriceScheme = '';
    var splitPriceScheme = '';
    var qty = '';
    var SSMarginPercentage = $('#SSMargin').val().trim();
    var productname = $('#PRODUCTID').find('option:selected').text().trim();
    var batchno = $("#hdnbatchno").val().trim();
    var packsizename = $('#PSID').find('option:selected').text();
    var Rate = 0;


    Rate = parseFloat($('#Rate').val().trim()).toFixed(2);
    stockcheckingmessage = getqtyinPCS($('#PRODUCTID').val().trim(), $('#PSID').val().trim(), $('#InvoiceQty').val().trim(), $('#InvoicePcs').val().trim(), $('#StockQty').val().trim(), $('#SaleOrderID').val().trim(), $('#hdndispatchID').val().trim(), $('#RemainingQty').val().trim())

    if (stockcheckingmessage != 'na') {
        toastr.warning('<b><font color=black>' + stockcheckingmessage + '</font></b>');
        return false;
    }
    else {
        productType = getProductType($('#PRODUCTID').val().trim());

        if ($("#chkFree").prop('checked') == false) {
            if (Rate <= 0) {
                toastr.warning('<b><font color=black>Rate 0 is not allowed to add in tax grid</font></b>');
                return false;
            }
            else if (productType != 'FG') {
                toastr.warning('<b><font color=black>Pop or Gift item(s) is not allowed to add in tax grid</font></b>');
                return false;
            }
            else {
                //debugger;
                priceschemediscount = getPriceSchemeDiscount($('#PRODUCTID').val().trim(), $("#hdninvoiceqty").val().trim(), 'B9F29D12-DE94-40F1-A668-C79BF1BF4425', $('#CUSTOMERID').val().trim(), '0', $('#InvoiceDate').val().trim(), BusinessSegment, $('#hdnGroupID').val().trim(), $('#BRID').val().trim(), $('#MRP').val().trim());

                if (priceschemediscount != '') {
                    //debugger;
                    strPriceSchemeDiscount = priceschemediscount;
                    splitPriceSchemeDiscount = strPriceSchemeDiscount.split('|');
                    priceschemeidvalue = splitPriceSchemeDiscount[0];
                    discountvalue = splitPriceSchemeDiscount[1];
                    stateID = splitPriceSchemeDiscount[2];
                    districtID = splitPriceSchemeDiscount[3];
                    zoneID = splitPriceSchemeDiscount[4];
                    teritoryID = splitPriceSchemeDiscount[5];
                    HSNCode = splitPriceSchemeDiscount[6];
                    $('#hdndiscountvalue').val(discountvalue);
                    $('#hdnHSNCode').val(HSNCode);

                    if (priceschemeidvalue != '') {
                        //debugger;
                        strPriceScheme = priceschemeidvalue;
                        splitPriceScheme = strPriceScheme.split('~');
                        priceschemeid = splitPriceScheme[0];
                        priceschemepercentage = splitPriceScheme[1];
                        priceschemevalue = splitPriceScheme[2];
                        PriceSchemeNotification = false;
                        $('#hdnpriceschemeid').val(priceschemeid);
                        $('#hdnpriceschemepercentage').val(priceschemepercentage);
                        $('#hdnpriceschemevalue').val(priceschemevalue);


                    }
                    else {
                        priceschemeid = '';
                        priceschemepercentage = '0.00';
                        priceschemevalue = '0.00';
                        PriceSchemeNotification = true;
                        $('#hdnpriceschemeid').val(priceschemeid);
                        $('#hdnpriceschemepercentage').val(priceschemepercentage);
                        $('#hdnpriceschemevalue').val(priceschemevalue);
                    }
                }
                else {
                    priceschemeid = '';
                    priceschemepercentage = '0.00';
                    priceschemevalue = '0.00';
                    discountvalue = '0.00';
                    HSNCode = '';
                    $('#hdndiscountvalue').val(discountvalue);
                    $('#hdnHSNCode').val(HSNCode);
                }

                /*Quantity Scheme Method Start*/
                QuantityScheme($('#InvoiceDate').val().trim(), stateID, districtID, $('#PRODUCTID').val().trim(), $('#hdnGroupID').val().trim(), BusinessSegment.trim(),
                    $('#hdninvoiceqty').val().trim(), 'B9F29D12-DE94-40F1-A668-C79BF1BF4425', zoneID, teritoryID, $('#CUSTOMERID').val().trim(),
                    $('#BRID').val().trim(), menuid.trim(), $('#hdnbatchno').val().trim(), $('#MRP').val().trim(), 'F', '0');
                /*Quantity Scheme Method End*/

                if (PopupOpen == false) {
                    //debugger;
                    /*Price Scheme Method Start*/
                    PriceScheme($('#PRODUCTID').val().trim(), $('#PRODUCTID').find('option:selected').text().trim(), $("#hdnbatchno").val().trim(),
                        $('#PSID').val().trim(), $('#PSID').find('option:selected').text().trim(), 'B9F29D12-DE94-40F1-A668-C79BF1BF4425',
                        '1970C78A-D062-4FE9-85C2-3E12490463AF', $('#hdnbillqty').val().trim(), $('#hdntaxnameCGST').val().trim(),
                        $('#hdntaxnameSGST').val().trim(), $('#hdntaxnameIGST').val().trim(), $('#InvoiceDate').val().trim(),
                        $('#MRP').val().trim(), parseFloat($('#Rate').val().trim()).toFixed(2), $('#hdnpriceschemeid').val().trim(),
                        $('#hdnpriceschemepercentage').val().trim(), $('#hdnpriceschemevalue').val().trim(), SSMarginPercentage,
                        $('#hdndiscountvalue').val().trim(), $('#hdnHSNCode').val().trim(),
                        $('#hdn_mfgdate').val().trim(), $('#hdn_exprdate').val().trim(), $('#qsguid').val().trim(), $('#qsheader').val().trim());
                    /*Price Scheme Method Start*/
                }

                /*Scheme Notification Message Start*/
                //if (PriceSchemeNotification == true && QuantitySchemeNotification == true) {
                //    toastr.error('<b><font color=white>Currently no scheme exists on ' + productname + '</font></b>');
                //}
                /*Scheme Notification Message End*/
            }
        }
    }

    //debugger;
}

function QuantityScheme(invoiceDate, stateID, districtID, productID, groupID, bsid, invoiceQty, packsizeID,
    zoneID, teritoryID, customerID, depotID, menuID, batchNO, mrp, flag, isdayEnd) {

    /*Start Quantity Scheme Grid*/
    var Freesrl = 0;
    var qtyschemeguid = '';

    var qtyschemeHeader = '';
    var BillQty = 0;

    //debugger;
    $.ajax({
        type: "POST",
        url: "/TranDepot/GetQuantityScheme",
        data: {
            Date: invoiceDate.trim(),
            StateID: stateID.trim(),
            DistrictID: districtID.trim(),
            ProductID: productID.trim(),
            GroupID: groupID.trim(),
            BSID: bsid.trim(),
            Qty: invoiceQty.trim(),
            PacksizeID: packsizeID.trim(),
            ZoneID: zoneID.trim(),
            TeritoryID: teritoryID.trim(),
            CustomerID: customerID.trim(),
            DepotID: depotID.trim(),
            ModuleID: menuID.trim(),
            BatchNo: batchNO.trim(),
            MRP: mrp.trim(),
            Flag: flag.trim(),
            IsDayend: isdayEnd.trim()
        },
        async: false,
        dataType: "json",
        success: function (qtyscheme) {
            //debugger;
            if (qtyscheme.length > 0) {

                //Create Table 
                var schemecount = 0;
                if (schemecount == 0) {
                    qtyschemeguid = create_UUID();
                    $('#qsguid').val(qtyschemeguid);
                }
                qtyschemeHeader = '1';
                $('#qsheader').val(qtyschemeHeader);
                var tr;
                tr = $('#freeDetailsGrid');
                var HeaderCount = $('#freeDetailsGrid thead th').length;
                var FooterCount = $('#freeDetailsGrid tfoot th').length;
                if (HeaderCount == 0) {
                    tr.append("<thead><tr><th style='text-align: center;'>Sl.No.</th><th style='display: none;'>Scheme ID</th><th style='display: none;>Primary Product ID</th><th style='display: none;'>Primary Product</th><th style='display: none;'>Primary Batch</th><th style='display: none;'>Qty</th><th style='display: none;'>Scheme Product ID</th><th>Free Product</th><th>HSN Code</th><th style='display: none;'>Packsize ID</th><th style='display: none;'>Packsize</th><th>Free Qty(Pcs)</th><th>MRP</th><th style='display: none;'>RateDisc</th><th>Rate</th><th>Disc.Amt.</th><th>Batch</th><th style='display: none;'>Assesment(%)</th><th style='display: none;'>Assesment Amt.</th><th>Mfg.Date</th><th>Exp.Date</th><th style='display: none;'>Weight</th><th style='display: none;'>NSR</th><th style='display: none;'>Ratedisc Value</th><th>Net Amt</th><th style='display: none;'>QSGUID</th></tr></thead>");
                }
                if (FooterCount == 0) {
                    tr.append("<tfoot><tr><th></th><th style='display: none;'></th><th style='display: none;></th><th style='display: none;'></th><th style='display: none;'></th><th style='display: none;'></th><th style='display: none;'></th><th></th><th style='color: blue;'>Total</th><th style='display: none;'></th><th style='display: none;'></th><th style='color: blue;text-align: right;' id='tfFreeQty'></th><th></th><th style='display: none;'></th><th></th><th style='color: blue;text-align: right;' id='tfFreeAmt'></th><th></th><th style='display: none;'></th><th style='display: none;'></th><th></th><th></th><th style='display: none;'></th><th style='display: none;'></th><th style='display: none;'></th><th style='color: blue;text-align: right;' id='tfFreeNetAmt'></th><th style='display: none;'></th></tr></tfoot><tbody>");
                }
                /*Quantity Scheme with ISWITHFREE = 'Y' Start*/
                if (qtyscheme[0].ISWITHFREE.toString().trim() == 'Y') {
                    //debugger;

                    /*For Loop Start*/
                    for (var i = 0; i < qtyscheme.length; i++) {

                        /*Free Stock Available in Same Batch Start*/
                        if (qtyscheme[i].BATCHNO.toString().trim() != '-NA') {

                            BillQty = parseFloat(invoiceQty.trim()) - parseFloat(qtyscheme[i].SCHEME_QTY);
                            $('#hdnbillqty').val(BillQty);

                            tr = $('<tr/>');
                            if (qtyscheme[i].SCHEMEID.toString().trim() == '0') {

                                tr.append("<td style='text-align: center;'><label class='slno' id='lblfreeslno'></label><span><input type='button' class='action-icons c-delete' id='btnfreedelete' value='Delete' /></span></td>");//0
                            }
                            else if (qtyscheme[i].SCHEMEID.toString().trim() != '0') {
                                tr.append("<td style='text-align: center;'><label class='slno' id='lblfreeslno'></label></td>");//0
                            }
                            tr.append("<td style='display: none;'>" + qtyscheme[i].SCHEMEID + "</td>");//1
                            tr.append("<td style='display: none;'>" + qtyscheme[i].SCHEME_PRODUCT_ID + "</td>");//2
                            tr.append("<td style='display: none;'>" + qtyscheme[i].SCHEME_PRODUCT_NAME + "</td>");//3
                            tr.append("<td style='display: none;'>" + qtyscheme[i].SCHEME_PRODUCT_BATCHNO + "</td>");//4
                            tr.append("<td style='display: none;'>" + qtyscheme[i].QTY + "</td>");//5
                            tr.append("<td style='display: none;'>" + qtyscheme[i].PRODUCTID + "</td>");//6
                            tr.append("<td style='width:250px;'>" + qtyscheme[i].PRODUCTNAME + "</td>");//7
                            tr.append("<td >" + getHSNCode(qtyscheme[i].PRODUCTID) + "</td>");//8
                            tr.append("<td style='display: none;'>" + qtyscheme[i].PACKSIZEID + "</td>");//9
                            tr.append("<td style='display: none;'>" + qtyscheme[i].PACKSIZENAME + "</td>");//10
                            tr.append("<td style='text-align: right;'>" + parseFloat(qtyscheme[i].SCHEME_QTY).toFixed(0) + "</td>");//11
                            tr.append("<td style='text-align: right;'>" + parseFloat(qtyscheme[i].MRP).toFixed(2) + "</td>");//12
                            tr.append("<td style='display: none;'>" + '0' + "</td>");//13
                            tr.append("<td style='text-align: right;'>" + parseFloat(qtyscheme[i].BRate).toFixed(2) + "</td>");//14
                            tr.append("<td style='text-align: right;'>" + parseFloat(qtyscheme[i].Amount).toFixed(2) + "</td>");//15
                            tr.append("<td >" + qtyscheme[i].BATCHNO + "</td>");//16
                            tr.append("<td style='display: none;'>" + '0' + "</td>");//17
                            tr.append("<td style='display: none;'>" + '0' + "</td>");//18
                            tr.append("<td >" + qtyscheme[i].MFDATE + "</td>");//19
                            tr.append("<td >" + qtyscheme[i].EXPRDATE + "</td>");//20
                            tr.append("<td style='display: none;'>" + qtyscheme[i].WEIGHT + "</td>");//21
                            tr.append("<td style='display: none;'>" + qtyscheme[i].NSR + "</td>");//22
                            tr.append("<td style='display: none;'>" + '0' + "</td>");//23
                            tr.append("<td style='text-align: right;'>" + parseFloat(qtyscheme[i].Amount).toFixed(2) + "</td>");//24
                            tr.append("<td style='display: none;'>" + qtyschemeguid.trim() + "</td>");//25
                            $("#freeDetailsGrid").append(tr);

                            schemecount = schemecount + 1;
                            PopupOpen = false;
                        }
                        /*Free Stock Available in Same Batch End*/

                        /*Free Stock Not Available in Same Batch Start*/
                        else if (qtyscheme[i].BATCHNO.toString().trim() == '-NA') {
                            BillQty = parseFloat(invoiceQty.trim()) - parseFloat(qtyscheme[i].SCHEME_QTY);
                            $('#hdnbillqty').val(BillQty);
                            $("#dvQtyScheme").dialog("open");

                            FreeProduct.append($("<option></option>").val(qtyscheme[i].PRODUCTID.toString().trim()).html(qtyscheme[i].PRODUCTNAME.toString().trim()));
                            $("#SchemeQuantity").val(parseInt(qtyscheme[0].SCHEME_QTY.toString().trim()));
                            $("#hdnqtyschemeid").val(qtyscheme[0].SCHEMEID.toString().trim());
                            $("#hdnschappqty").val(qtyscheme[0].QTY.toString().trim());

                            schemecount = schemecount + 1;
                            PopupOpen = true;
                        }
                        /*Free Stock Not Available in Same Batch End*/
                    }
                    /*For Loop End*/

                    tr.append("</tbody>");
                    RowCountFreeGrid();
                    QuantitySchemeNotification = false;
                }
                /*Quantity Scheme with ISWITHFREE = 'Y' End*/


                /*Quantity Scheme with ISWITHFREE = 'N' Start*/
                else if (qtyscheme[0].ISWITHFREE.toString().trim() != 'Y') {
                    //debugger;

                    /* Free Product Drop Down Declation Start */
                    var FreeProduct = $("#FREEPRODUCTID");
                    $("#freeProductDetailsGrid").empty();

                    if (qtyscheme.length == 1) {
                        FreeProduct.empty();
                    }
                    else if (qtyscheme.length > 1) {
                        FreeProduct.empty().append('<option selected="selected" value="0">Please Select</option>');
                    }
                    /* Free Product Drop Down Declation End */


                    /*For Loop Start*/
                    for (var i = 0; i < qtyscheme.length; i++) {

                        /*Free Stock Available in Same Batch Start*/
                        if (qtyscheme[i].BATCHNO.toString().trim() != '-NA') {
                            BillQty = parseFloat(invoiceQty.trim());
                            $('#hdnbillqty').val(BillQty);

                            tr = $('<tr/>');
                            if (qtyscheme[i].SCHEMEID.toString().trim() == '0') {

                                tr.append("<td style='text-align: center;'><label class='slno' id='lblfreeslno'></label><span><input type='button' class='action-icons c-delete' id='btnfreedelete' value='Delete' /></span></td>");//0
                            }
                            else if (qtyscheme[i].SCHEMEID.toString().trim() != '0') {
                                tr.append("<td style='text-align: center;'><label class='slno' id='lblfreeslno'></label></td>");//0
                            }
                            tr.append("<td style='display: none;'>" + qtyscheme[i].SCHEMEID + "</td>");//1
                            tr.append("<td style='display: none;'>" + qtyscheme[i].SCHEME_PRODUCT_ID + "</td>");//2
                            tr.append("<td style='display: none;'>" + qtyscheme[i].SCHEME_PRODUCT_NAME + "</td>");//3
                            tr.append("<td style='display: none;'>" + qtyscheme[i].SCHEME_PRODUCT_BATCHNO + "</td>");//4
                            tr.append("<td style='display: none;'>" + qtyscheme[i].QTY + "</td>");//5
                            tr.append("<td style='display: none;'>" + qtyscheme[i].PRODUCTID + "</td>");//6
                            tr.append("<td style='width:250px;'>" + qtyscheme[i].PRODUCTNAME + "</td>");//7
                            tr.append("<td >" + getHSNCode(qtyscheme[i].PRODUCTID) + "</td>");//8
                            tr.append("<td style='display: none;'>" + qtyscheme[i].PACKSIZEID + "</td>");//9
                            tr.append("<td style='display: none;'>" + qtyscheme[i].PACKSIZENAME + "</td>");//10
                            tr.append("<td style='text-align: right;'>" + parseFloat(qtyscheme[i].SCHEME_QTY).toFixed(0) + "</td>");//11
                            tr.append("<td style='text-align: right;'>" + parseFloat(qtyscheme[i].MRP).toFixed(2) + "</td>");//12
                            tr.append("<td style='display: none;'>" + '0' + "</td>");//13
                            tr.append("<td style='text-align: right;'>" + parseFloat(qtyscheme[i].BRate).toFixed(2) + "</td>");//14
                            tr.append("<td style='text-align: right;'>" + parseFloat(qtyscheme[i].Amount).toFixed(2) + "</td>");//15
                            tr.append("<td >" + qtyscheme[i].BATCHNO + "</td>");//16
                            tr.append("<td style='display: none;'>" + '0' + "</td>");//17
                            tr.append("<td style='display: none;'>" + '0' + "</td>");//18
                            tr.append("<td >" + qtyscheme[i].MFDATE + "</td>");//19
                            tr.append("<td >" + qtyscheme[i].EXPRDATE + "</td>");//20
                            tr.append("<td style='display: none;'>" + qtyscheme[i].WEIGHT + "</td>");//21
                            tr.append("<td style='display: none;'>" + qtyscheme[i].NSR + "</td>");//22
                            tr.append("<td style='display: none;'>" + '0' + "</td>");//23
                            tr.append("<td style='text-align: right;'>" + parseFloat(qtyscheme[i].Amount).toFixed(2) + "</td>");//24
                            tr.append("<td style='display: none;'>" + qtyschemeguid.trim() + "</td>");//25
                            $("#freeDetailsGrid").append(tr);

                            schemecount = schemecount + 1;
                            PopupOpen = false;
                        }
                        /*Free Stock Available in Same Batch End*/


                        /*Free Stock Not Available in Same Batch Start*/
                        else if (qtyscheme[i].BATCHNO.toString().trim() == '-NA') {
                            BillQty = parseFloat(invoiceQty.trim());
                            $('#hdnbillqty').val(BillQty);
                            $("#dvQtyScheme").dialog("open");

                            FreeProduct.append($("<option></option>").val(qtyscheme[i].PRODUCTID.toString().trim()).html(qtyscheme[i].PRODUCTNAME.toString().trim()));
                            $("#SchemeQuantity").val(parseInt(qtyscheme[0].SCHEME_QTY.toString().trim()));
                            $("#hdnqtyschemeid").val(qtyscheme[0].SCHEMEID.toString().trim());
                            $("#hdnschappqty").val(qtyscheme[0].QTY.toString().trim());

                            schemecount = schemecount + 1;
                            PopupOpen = true;
                        }
                        /*Free Stock Not Available in Same Batch End*/
                    }
                    /*For Loop End*/

                    tr.append("</tbody>");
                    RowCountFreeGrid();
                    QuantitySchemeNotification = false;

                    $("#FREEPRODUCTID").chosen('destroy');
                    $("#FREEPRODUCTID").chosen({ width: '390px' });

                    if (qtyscheme.length == 1) {
                        FreeProductBatchDetails($('#BRID').val().trim(), $('#FREEPRODUCTID').val().trim(), 'B9F29D12-DE94-40F1-A668-C79BF1BF4425',
                            '0', $('#CUSTOMERID').val().trim(), $('#InvoiceDate').val().trim(), menuid.trim(), BusinessSegment.trim(),
                            $('#hdnGroupID').val().trim(), '0', '113BD8D6-E5DC-4164-BEE7-02A16F97ABCC');
                    }
                }
                /*Quantity Scheme with ISWITHFREE = 'N' End*/
            }
            else {
                BillQty = invoiceQty.trim();
                $('#hdnbillqty').val(BillQty);
                $('#qsguid').val('0');
                $('#qsheader').val('0');
                QuantitySchemeNotification = true;
                PopupOpen = false;
            }

        },
        failure: function (qtyscheme) {
            alert(qtyscheme.responseText);
        },
        error: function (qtyscheme) {
            alert(qtyscheme.responseText);
        }
    });
    /*End Quantity Scheme Grid*/

}

function PriceScheme(productID, productname, batchno, packsizeid, packsizename, frompacksizeID,
    topacksizeID, billQty, cgst, sgst, igst, invoiceDate, mrp, rate, priceschemeid,
    priceschemepercentage, priceschemevalue, SSMarginPercentage, discountvalue, hsnCode,
    mfgdate, expdate, qsguid, qsheader) {

    //debugger;
    /*Start Tax Grid*/
    var HSNCode = ''
    var caseqty = '';
    var caseqnty = 0;
    var pcsqnty = 0;
    var netweight = '';
    var grossweight = '';
    var hsn = '';
    var igsttax = '';
    var cgsttax = '';
    var sgsttax = '';
    var caserate = 0;
    var Amount = 0;
    var PriceschemeValue = 0;
    var AfterSchemeAmount = 0;
    var DiscountValue = 0;
    var AfterDiscountAmount = 0;
    var SSMarginValue = 0;
    var AfterSSMarginAmount = 0;
    var MRP = mrp.trim();
    var Assesmentvalue = 0;
    var IgstTaxPercentage = 0;
    var IgstTaxAmount = 0;
    var CgstTaxPercentage = 0;
    var CgstTaxAmount = 0;
    var SgstTaxPercentage = 0;
    var SgstTaxAmount = 0;
    var NetAmount = 0;
    var totalqty = 0;
    var totalfreeqty = 0;
    var remainingqty = 0;
    var ratio = 0;
    var srl = 0;
    srl = srl + 1;


    /*IGST Start*/
    if ($("#hdntaxcount").val().trim() == '1') {
        //debugger;
        $.ajax({
            type: "POST",
            url: "/TranDepot/GetTaxDetails",
            data: {
                Productid: productID.trim(),
                FromPacksizeID: frompacksizeID.trim(),
                ToPacksizeID: topacksizeID.trim(),
                QtyPcs: billQty.trim(),
                CGST: cgst.trim(),
                SGST: sgst.trim(),
                IGST: igst.trim(),
                Date: invoiceDate.trim()
            },
            async: false,
            dataType: "json",
            success: function (response) {
                //debugger;
                var listCaseQty = response.taxDetailsDataset.varCaseQty;
                var listNetWeight = response.taxDetailsDataset.varNetwght;
                var listCGSTTax = response.taxDetailsDataset.varCgstTax;
                var listCGSTID = response.taxDetailsDataset.varCgstID;
                var listSGSTTax = response.taxDetailsDataset.varSgstTax;
                var listSGSTID = response.taxDetailsDataset.varSgstID;
                var listIGSTTax = response.taxDetailsDataset.varIgstTax;
                var listIGSTID = response.taxDetailsDataset.varIgstID;

                $.each(listCaseQty, function (index, record) {
                    //debugger;
                    caseqty = this['QTYINCASE'].toString().trim();
                    if ($('#PSID').val().trim() != 'B9F29D12-DE94-40F1-A668-C79BF1BF4425') {
                        if (caseqty.indexOf(".") > 0) {
                            caseqnty = parseInt(caseqty.substr(0, caseqty.indexOf("."))).toString().trim();
                            pcsqnty = parseInt(caseqty.substr(caseqty.indexOf(".") + 1, 3)).toString().trim();
                        }
                        else {
                            caseqnty = caseqty;
                            pcsqnty = '0';
                        }
                    }
                    else if ($('#PSID').val().trim() == 'B9F29D12-DE94-40F1-A668-C79BF1BF4425') {
                        caseqnty = '0';
                        pcsqnty = billQty;
                    }
                });

                $.each(listNetWeight, function (index, record) {
                    netweight = this['NETWEIGHT'].trim();
                });

                if (listIGSTTax.length > 0) {
                    $.each(listIGSTTax, function (index, record) {
                        igsttax = this['IGSTTAX'].trim();
                    });
                }
                else {
                    igsttax = 0;
                }
                if (listIGSTID.length > 0) {
                    $.each(listIGSTID, function (index, record) {
                        $("#igstid").val(this['IGSTID'].trim());
                    });
                }
                else {
                    $("#igstid").val('1177D9CF-8F1E-4C91-B785-FDF940101EEE');
                }

                Amount = parseFloat((billQty * rate * 100) / 100);
                //debugger;

                /*Price Scheme Calculation Start*/
                if (parseFloat(priceschemepercentage) > 0 && parseFloat(priceschemevalue) == 0) {
                    PriceschemeValue = (parseFloat(Amount) * parseFloat(priceschemepercentage)) / 100;
                    AfterSchemeAmount = parseFloat(Amount - PriceschemeValue);
                }
                else if (parseFloat(priceschemepercentage) == 0 && parseFloat(priceschemevalue) > 0) {
                    PriceschemeValue = parseFloat(priceschemevalue) * parseFloat(billQty);
                    AfterSchemeAmount = parseFloat(Amount - PriceschemeValue);
                }
                else if (parseFloat(priceschemepercentage) == 0 && parseFloat(priceschemevalue) == 0) {
                    AfterSchemeAmount = parseFloat(Amount);
                }
                /*Price Scheme Calculation End*/

                /*Additional SS Margin Calculation Start*/
                if (parseFloat(SSMarginPercentage) > 0) {
                    SSMarginValue = (parseFloat(AfterSchemeAmount) * parseFloat(SSMarginPercentage)) / 100;
                    AfterSSMarginAmount = parseFloat(AfterSchemeAmount - SSMarginValue);
                }
                else {
                    AfterSSMarginAmount = parseFloat(AfterSchemeAmount);
                }
                /*Additional SS Margin Calculation End*/

                /*Discount Calculation Start*/
                if (parseFloat(discountvalue) > 0) {

                    DiscountValue = (parseFloat(AfterSSMarginAmount) * parseFloat(discountvalue)) / 100;
                    AfterDiscountAmount = parseFloat(AfterSSMarginAmount - DiscountValue);
                }
                else {
                    AfterDiscountAmount = parseFloat(AfterSSMarginAmount);
                }
                /*Discount Calculation Start*/


                Assesmentvalue = parseFloat('0');
                IgstTaxPercentage = parseFloat(igsttax);
                if (isNaN(IgstTaxPercentage) == true) {
                    IgstTaxPercentage = 0;
                    IgstTaxAmount = 0;
                }
                else {
                    IgstTaxAmount = parseFloat((Math.round(((AfterDiscountAmount * IgstTaxPercentage) / 100) * 100) / 100));
                }
                NetAmount = AfterDiscountAmount + IgstTaxAmount;

                caserate = RatePerCase(BusinessSegment.trim(), $('#hdnGroupID').val().trim(), productID.trim());

                //Create Table
                var tr;
                tr = $('#productDetailsGrid');
                var HeaderCount = $('#productDetailsGrid thead th').length;
                var FooterCount = $('#productDetailsGrid tfoot th').length;
                if (HeaderCount == 0) {
                    tr.append("<thead><tr><th style='text-align: center;'>Sl.No.</th><th style='display: none;'>Product ID</th><th>Product</th><th>HSN Code</th><th style='display: none;'>PackSize ID</th><th style='display: none;'>Pack Size</th><th>MRP</th><th style='display: none;'>NSR</th><th>Rate/Pcs</th><th>Rate/Case</th><th>Case</th><th>Pcs</th><th>Batch</th><th style='display: none;'>Assesmet(%)</th><th style='display: none;'>Assesment Amt.</th><th style='display: none;'>PRIMARYPRICESCHEMEID</th><th style='display: none;'>Add.SS.Disc.</th><th>Sch(%)</th><th>Sch.Amt.</th><th style='display: none;'>Disc(%)</th><th style='display: none;'>Disc.Amt.</th><th>Amount</th><th style='display: none;'>Weight</th><th>Mfg.Date</th><th>Exp.Date</th><th style='display: none;'>QSH</th><th style='display: none;'>QSGUID</th><th style='display: none;'>FLAG</th><th>IGST(%)</th><th>IGST</th><th>Net Amt.</th></tr></thead>");
                }
                if (FooterCount == 0) {
                    tr.append("<tfoot><tr><th></th><th style='display: none;'></th><th></th><th style='color: blue;'>Total</th><th style='display: none;'></th><th style='display: none;'></th><th></th><th style='display: none;'></th><th></th><th></th><th style='color: blue;text-align: right;' id='tfCase'></th><th style='color: blue;text-align: right;' id='tfPcs'></th><th></th><th style='display: none;'></th><th style='display: none;'></th><th style='display: none;'></th><th style='color: blue;text-align: right;display: none;' id='tfAddSSDisc'></th><th></th><th style='color: blue;text-align: right;' id='tfSchemeAmt'></th><th style='display: none;'></th><th style='color: blue;text-align: right;display: none;' id='tfDiscAmt'></th><th style='color: blue;text-align: right;' id='tfBasicAmt'></th><th style='display: none;'></th><th></th><th></th><th style='display: none;'></th><th style='display: none;'></th><th style='display: none;'></th><th></th><th style='color: blue;text-align: right;' id='tfIGST'></th><th style='color: blue;text-align: right;' id='tfNetAmt'></th></tr></tfoot><tbody>");
                }
                //debugger;
                tr = $('<tr/>');
                tr.append("<td style='text-align: center;'><label class='slno' id='lblslno'></label><span><input type='button' class='action-icons c-delete' id='btnBilldelete' value='Delete' /></span></td>");//0
                tr.append("<td style='display: none;'>" + productID.trim() + "</td>");//1
                tr.append("<td style='width:250px;'>" + productname.substr(0, productname.indexOf("~")) + "</td>");//2
                tr.append("<td style='text-align: center;'>" + hsnCode.trim() + "</td>");//3
                tr.append("<td style='display: none;'>" + packsizeid.trim() + "</td>");//4
                tr.append("<td style='display: none;'>" + packsizename + "</td>");//5
                tr.append("<td style='text-align: right;'>" + parseFloat(MRP).toFixed(2) + "</td>");//6
                tr.append("<td style='display: none;'>" + parseFloat(rate).toFixed(2) + "</td>");//7-NSR
                tr.append("<td style='text-align: right;'>" + parseFloat(rate).toFixed(2) + "</td>");//8
                tr.append("<td style='text-align: right;'>" + parseFloat(caserate).toFixed(2) + "</td>");//9
                tr.append("<td style='text-align: right;'>" + caseqnty + "</td>");//10
                tr.append("<td style='text-align: right;'>" + pcsqnty + "</td>");//11
                tr.append("<td style='text-align: center;'>" + batchno + "</td>");//12
                tr.append("<td style='display: none;'>" + '0' + "</td>");//13
                tr.append("<td style='display: none;'>" + Assesmentvalue.toFixed(2) + "</td>");//14
                tr.append("<td style='display: none;'>" + priceschemeid + "</td>");//15
                tr.append("<td style='text-align: right;display: none;'>" + parseFloat(SSMarginValue).toFixed(2) + "</td>");//16
                tr.append("<td style='text-align: right;'>" + parseFloat(priceschemepercentage).toFixed(2) + "</td>");//17
                tr.append("<td style='text-align: right;'>" + parseFloat(PriceschemeValue).toFixed(2) + "</td>");//18
                tr.append("<td style='text-align: right;display: none;'>" + parseFloat(discountvalue).toFixed(2) + "</td>");//19
                tr.append("<td style='text-align: right;display: none;'>" + parseFloat(DiscountValue).toFixed(2) + "</td>");//20
                tr.append("<td style='text-align: right;'>" + AfterDiscountAmount.toFixed(2) + "</td>");//21
                tr.append("<td style='display: none;'>" + netweight.trim() + "</td>");//22
                tr.append("<td style='text-align: center;'>" + mfgdate.trim() + "</td>");//23
                tr.append("<td style='text-align: center;'>" + expdate.trim() + "</td>");//24
                tr.append("<td style='display: none;'>" + qsguid.trim() + "</td>");//25
                tr.append("<td style='display: none;'>" + qsheader.trim() + "</td>");//26 
                tr.append("<td style='display: none;'>" + '0' + "</td>");//27
                tr.append("<td style='text-align: right;'>" + IgstTaxPercentage.toFixed(2) + "</td>");//28
                tr.append("<td style='text-align: right;'>" + IgstTaxAmount.toFixed(2) + "</td>");//29
                tr.append("<td style='text-align: right;'>" + NetAmount.toFixed(2) + "</td>");//30
                $("#productDetailsGrid").append(tr);
                tr.append("</tbody>");
                RowCount();
                CalculateAmount();
                $("#InvoiceQty").trigger("focus");
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    }
    /*IGST End*/

    /*CGST & SGST Start*/
    else if ($("#hdntaxcount").val().trim() == '2') {
        //debugger;
        $.ajax({
            type: "POST",
            url: "/TranDepot/GetTaxDetails",
            data: {
                Productid: productID.trim(),
                FromPacksizeID: frompacksizeID.trim(),
                ToPacksizeID: topacksizeID.trim(),
                QtyPcs: billQty.trim(),
                CGST: cgst.trim(),
                SGST: sgst.trim(),
                IGST: igst.trim(),
                Date: invoiceDate.trim()
            },
            async: false,
            dataType: "json",
            success: function (response) {
                //debugger;
                var listCaseQty = response.taxDetailsDataset.varCaseQty;
                var listNetWeight = response.taxDetailsDataset.varNetwght;
                var listCGSTTax = response.taxDetailsDataset.varCgstTax;
                var listCGSTID = response.taxDetailsDataset.varCgstID;
                var listSGSTTax = response.taxDetailsDataset.varSgstTax;
                var listSGSTID = response.taxDetailsDataset.varSgstID;
                var listIGSTTax = response.taxDetailsDataset.varIgstTax;
                var listIGSTID = response.taxDetailsDataset.varIgstID;

                $.each(listCaseQty, function (index, record) {
                    //debugger;
                    caseqty = this['QTYINCASE'].toString().trim();
                    if ($('#PSID').val().trim() != 'B9F29D12-DE94-40F1-A668-C79BF1BF4425') {
                        if (caseqty.indexOf(".") > 0) {
                            caseqnty = parseInt(caseqty.substr(0, caseqty.indexOf("."))).toString().trim();
                            pcsqnty = parseInt(caseqty.substr(caseqty.indexOf(".") + 1, 3)).toString().trim();
                        }
                        else {
                            caseqnty = caseqty;
                            pcsqnty = '0';
                        }
                    }
                    else if ($('#PSID').val().trim() == 'B9F29D12-DE94-40F1-A668-C79BF1BF4425') {
                        caseqnty = '0';
                        pcsqnty = billQty;
                    }
                });

                $.each(listNetWeight, function (index, record) {
                    netweight = this['NETWEIGHT'].trim();
                });

                if (listCGSTTax.length > 0) {
                    $.each(listCGSTTax, function (index, record) {
                        cgsttax = this['CGSTTAX'].trim();
                    });
                }
                else {
                    cgsttax = 0;
                }
                if (listCGSTID.length > 0) {
                    $.each(listCGSTID, function (index, record) {
                        $("#cgstid").val(this['CGSTID'].trim());
                    });
                }
                else {
                    $("#cgstid").val('88DBD523-318C-4075-A089-9CA8088C9FA6');
                }
                if (listSGSTTax.length > 0) {
                    $.each(listSGSTTax, function (index, record) {
                        sgsttax = this['SGSTTAX'].trim();
                    });
                }
                else {
                    sgsttax = 0;
                }
                if (listSGSTID.length > 0) {
                    $.each(listSGSTID, function (index, record) {
                        $("#sgstid").val(this['SGSTID'].trim());
                    });
                }
                else {
                    $("#sgstid").val('C9221FA4-291C-43A9-9EC1-C3E8155AFE5B');
                }

                Amount = parseFloat((billQty * rate * 100)/100);
                //debugger;

                /*Price Scheme Calculation Start*/
                if (parseFloat(priceschemepercentage) > 0 && parseFloat(priceschemevalue) == 0) {
                    PriceschemeValue = (parseFloat(Amount) * parseFloat(priceschemepercentage)) / 100;
                    AfterSchemeAmount = parseFloat(Amount - PriceschemeValue);
                }
                else if (parseFloat(priceschemepercentage) == 0 && parseFloat(priceschemevalue) > 0) {
                    PriceschemeValue = parseFloat(priceschemevalue) * parseFloat(billQty);
                    AfterSchemeAmount = parseFloat(Amount - PriceschemeValue);
                }
                else if (parseFloat(priceschemepercentage) == 0 && parseFloat(priceschemevalue) == 0) {
                    AfterSchemeAmount = parseFloat(Amount);
                }
                /*Price Scheme Calculation End*/

                /*Additional SS Margin Calculation Start*/
                if (parseFloat(SSMarginPercentage) > 0) {
                    SSMarginValue = (parseFloat(AfterSchemeAmount) * parseFloat(SSMarginPercentage)) / 100;
                    AfterSSMarginAmount = parseFloat(AfterSchemeAmount - SSMarginValue);
                }
                else {
                    AfterSSMarginAmount = parseFloat(AfterSchemeAmount);
                }
                /*Additional SS Margin Calculation End*/

                /*Discount Calculation Start*/
                if (parseFloat(discountvalue) > 0) {

                    DiscountValue = (parseFloat(AfterSSMarginAmount) * parseFloat(discountvalue)) / 100;
                    AfterDiscountAmount = parseFloat(AfterSSMarginAmount - DiscountValue);
                }
                else {
                    AfterDiscountAmount = parseFloat(AfterSSMarginAmount);
                }
                /*Discount Calculation Start*/


                Assesmentvalue = parseFloat('0');
                CgstTaxPercentage = parseFloat(cgsttax);
                if (isNaN(CgstTaxPercentage) == true) {
                    CgstTaxPercentage = 0;
                    CgstTaxAmount = 0;
                }
                else {
                    CgstTaxAmount = parseFloat((Math.round(((AfterDiscountAmount * CgstTaxPercentage) / 100) * 100) / 100));
                }
                SgstTaxPercentage = parseFloat(sgsttax);
                if (isNaN(SgstTaxPercentage) == true) {
                    SgstTaxPercentage = 0;
                    SgstTaxAmount = 0;
                }
                else {
                    SgstTaxAmount = parseFloat((Math.round(((AfterDiscountAmount * SgstTaxPercentage) / 100) * 100) / 100));
                }
                NetAmount = AfterDiscountAmount + CgstTaxAmount + SgstTaxAmount;
                caserate = RatePerCase(BusinessSegment.trim(), $('#hdnGroupID').val().trim(), productID.trim());

                //Create Table
                var tr;
                tr = $('#productDetailsGrid');
                var HeaderCount = $('#productDetailsGrid thead th').length;
                var FooterCount = $('#productDetailsGrid tfoot th').length;
                if (HeaderCount == 0) {
                    tr.append("<thead><tr><th style='text-align: center;'>Sl.No.</th><th style='display: none;'>Product ID</th><th>Product</th><th>HSN Code</th><th style='display: none;'>PackSize ID</th><th style='display: none;'>Pack Size</th><th>MRP</th><th style='display: none;'>NSR</th><th>Rate/Pcs</th><th>Rate/Case</th><th>Case</th><th>Pcs</th><th>Batch</th><th style='display: none;'>Assesmet(%)</th><th style='display: none;'>Assesment Amt.</th><th style='display: none;'>PRIMARYPRICESCHEMEID</th><th style='display: none;'>Add.SS.Disc.</th><th>Sch(%)</th><th>Sch.Amt.</th><th style='display: none;'>Disc(%)</th><th style='display: none;'>Disc.Amt.</th><th>Amount</th><th style='display: none;'>Weight</th><th>Mfg.Date</th><th>Exp.Date</th><th style='display: none;'>QSH</th><th style='display: none;'>QSGUID</th><th style='display: none;'>FLAG</th><th>CGST(%)</th><th>CGST</th><th>SGST(%)</th><th>SGST</th><th>Net Amt.</th></tr></thead>");
                }
                if (FooterCount == 0) {
                    tr.append("<tfoot><tr><th></th><th style='display: none;'></th><th></th><th style='color: blue;'>Total</th><th style='display: none;'></th><th style='display: none;'></th><th></th><th style='display: none;'></th><th></th><th></th><th style='color: blue;text-align: right;' id='tfCase'></th><th style='color: blue;text-align: right;' id='tfPcs'></th><th></th><th style='display: none;'></th><th style='display: none;'></th><th style='display: none;'></th><th style='color: blue;text-align: right;display: none;' id='tfAddSSDisc'></th><th></th><th style='color: blue;text-align: right;' id='tfSchemeAmt'></th><th style='display: none;'></th><th style='color: blue;text-align: right;display: none;' id='tfDiscAmt'></th><th style='color: blue;text-align: right;' id='tfBasicAmt'></th><th style='display: none;'></th><th></th><th></th><th style='display: none;'></th><th style='display: none;'></th><th style='display: none;'></th><th></th><th style='color: blue;text-align: right;' id='tfCGST'></th><th></th><th style='color: blue;text-align: right;' id='tfSGST'></th><th style='color: blue;text-align: right;' id='tfNetAmt'></th></tr></tfoot><tbody>");
                }
                //debugger;
                tr = $('<tr/>');
                tr.append("<td style='text-align: center;'><label id='lblslno'></label><span><input type='button' class='action-icons c-delete' id='btnBilldelete' value='Delete' /></span></td>");//0
                tr.append("<td style='display: none;'>" + productID.trim() + "</td>");//1
                tr.append("<td style='width:250px;'>" + productname.substr(0, productname.indexOf("~")) + "</td>");//2
                tr.append("<td style='text-align: center;'>" + hsnCode.trim() + "</td>");//3
                tr.append("<td style='display: none;'>" + packsizeid.trim() + "</td>");//4
                tr.append("<td style='display: none;'>" + packsizename + "</td>");//5
                tr.append("<td style='text-align: right;'>" + parseFloat(MRP).toFixed(2) + "</td>");//6
                tr.append("<td style='display: none;'>" + parseFloat(rate).toFixed(2) + "</td>");//7-NSR
                tr.append("<td style='text-align: right;'>" + parseFloat(rate).toFixed(2) + "</td>");//8
                tr.append("<td style='text-align: right;'>" + parseFloat(caserate).toFixed(2) + "</td>");//9
                tr.append("<td style='text-align: right;'>" + caseqnty + "</td>");//10
                tr.append("<td style='text-align: right;'>" + pcsqnty + "</td>");//11
                tr.append("<td style='text-align: center;'>" + batchno + "</td>");//12
                tr.append("<td style='display: none;'>" + '0' + "</td>");//13
                tr.append("<td style='display: none;'>" + Assesmentvalue.toFixed(2) + "</td>");//14
                tr.append("<td style='display: none;'>" + priceschemeid + "</td>");//15
                tr.append("<td style='text-align: right;display: none;'>" + parseFloat(SSMarginValue).toFixed(2) + "</td>");//16
                tr.append("<td style='text-align: right;'>" + parseFloat(priceschemepercentage).toFixed(2) + "</td>");//17
                tr.append("<td style='text-align: right;'>" + parseFloat(PriceschemeValue).toFixed(2) + "</td>");//18
                tr.append("<td style='text-align: right;display: none;'>" + parseFloat(discountvalue).toFixed(2) + "</td>");//19
                tr.append("<td style='text-align: right;display: none;'>" + parseFloat(DiscountValue).toFixed(2) + "</td>");//20
                tr.append("<td style='text-align: right;'>" + AfterDiscountAmount.toFixed(2) + "</td>");//21
                tr.append("<td style='display: none;'>" + netweight.trim() + "</td>");//22
                tr.append("<td style='text-align: center;'>" + mfgdate.trim() + "</td>");//23
                tr.append("<td style='text-align: center;'>" + expdate.trim() + "</td>");//24
                tr.append("<td style='display: none;'>" + qsguid.trim() + "</td>");//25
                tr.append("<td style='display: none;'>" + qsheader.trim() + "</td>");//26
                tr.append("<td style='display: none;'>" + '0' + "</td>");//27
                tr.append("<td style='text-align: right;'>" + CgstTaxPercentage.toFixed(2) + "</td>");//28
                tr.append("<td style='text-align: right;'>" + CgstTaxAmount.toFixed(2) + "</td>");//29
                tr.append("<td style='text-align: right;'>" + SgstTaxPercentage.toFixed(2) + "</td>");//30
                tr.append("<td style='text-align: right;'>" + SgstTaxAmount.toFixed(2) + "</td>");//31
                tr.append("<td style='text-align: right;'>" + NetAmount.toFixed(2) + "</td>");//32
                $("#productDetailsGrid").append(tr);
                tr.append("</tbody>");
                RowCount();
                CalculateAmount();
                $("#InvoiceQty").trigger("focus");
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    }
    /*CGST & SGST End*/

    /*End Tax Grid*/


    /*Filling Tax Session Start*/
    if ($("#hdntaxcount").val() == '1') {
        //addRowinTaxTable($('#igstid').val().trim(), IgstTaxPercentage, IgstTaxAmount, MRP, '1');
        addTaxInTaxGrid($('#igstid').val().trim(), IgstTaxPercentage, IgstTaxAmount, MRP);
    }
    else if ($("#hdntaxcount").val() == '2') {
        //addRowinTaxTable($('#cgstid').val().trim(), CgstTaxPercentage, CgstTaxAmount, MRP, '1');
        //addRowinTaxTable($('#sgstid').val().trim(), SgstTaxPercentage, SgstTaxAmount, MRP, '1');
        addTaxInTaxGrid($('#cgstid').val().trim(), CgstTaxPercentage, CgstTaxAmount, MRP);
        addTaxInTaxGrid($('#sgstid').val().trim(), SgstTaxPercentage, SgstTaxAmount, MRP);
    }
    /*else {
        addRowinTaxTable($('#igstid').val().trim(), IgstTaxPercentage, IgstTaxAmount, MRP, '0');
    }*/
    /*Filling Tax Session End*/

    /*Combo Case breakup loops Start*/
    $('#comboProductGrid tbody tr').each(function () {
        //debugger;
        var primaryproductID = $(this).find('td:eq(0)').html().trim();
        var secondaryproductiD = $(this).find('td:eq(2)').html().trim();
        if (primaryproductID != secondaryproductiD && primaryproductID == productID) {

            ratio += parseInt($(this).find('td:eq(5)').html().trim());
        }
        
    });

    $('#comboProductGrid tbody tr').each(function () {
        //debugger;
        var primaryproduct = $(this).find('td:eq(0)').html().trim();
        var secondaryproduct = $(this).find('td:eq(2)').html().trim();
        var singlecartoon = $(this).find('td:eq(4)').html().trim();
        var braekupqty = $(this).find('td:eq(5)').html().trim();
        if (primaryproduct != secondaryproduct && primaryproduct == productID) {
            var outputqty = 0;
            if (singlecartoon == '0') {

                outputqty = billQty * (braekupqty / ratio);
            }
            else {

                outputqty = billQty * (braekupqty);
            }

            addComboBreakupGrid(primaryproduct.trim(), secondaryproduct.trim(), outputqty);
        }

    });
    /*Combo Case breakup loops End*/

    clearafterAdd();

    //debugger;

    $('#productDetailsGrid tbody tr').each(function () {
        if ($(this).find('td:eq(1)').html().trim() == productID) {
            totalqty += parseInt(getCaseToPcsConversion($(this).find('td:eq(1)').html().trim(), '1970C78A-D062-4FE9-85C2-3E12490463AF', 'B9F29D12-DE94-40F1-A668-C79BF1BF4425', $(this).find('td:eq(10)').html().trim(), $(this).find('td:eq(11)').html().trim()));
        }
    });
    $('#freeDetailsGrid tbody tr').each(function () {
        //debugger;
        if ($(this).find('td:eq(2)').html().trim() == productID) {
            totalfreeqty += parseInt($(this).find('td:eq(11)').html().trim());
        }
    });
    remainingqty = parseInt($("#hdnOrderqtyPcs").val()) - (parseInt($("#hdnFinalDeliveredqtyPcs").val()) + parseInt(totalqty) + parseInt(totalfreeqty));
    $("#RemainingQty").val(parseInt(remainingqty));

    $("#PRODUCTID > option").each(function () {
        if ($(this).val().trim() != '0') {
            if ($(this).val().trim() == productID) {
                if (parseInt(remainingqty) <= 0) {

                    $(this).css("color", "red");

                }
                else {
                    $(this).css("color", "");
                }
            }
        }
    });
    $("#PRODUCTID").chosen({
        search_contains: true
    });
    $("#PRODUCTID").trigger("chosen:updated");

    bindbatchno('1');
}


function FreeProductBatchDetails(depotid, productid, packsizeid, batchno, customerid,
    invoicedate, menuid, bsid, groupid, mrp, storelocationid) {

    ////debugger;
    $.ajax({
        type: "POST",
        url: "/TranDepot/GetFreeProductBatchDetails",
        data: {
            DepotID: depotid.trim(),
            ProductID: productid.trim(),
            PacksizeID: packsizeid.trim(),
            BatchNo: batchno.trim(),
            CustomerID: customerid.trim(),
            Date: invoicedate.trim(),
            ModuleID: menuid.trim(),
            BSID: bsid.trim(),
            GroupID: groupid.trim(),
            Mrp: mrp.trim(),
            StorelocationID: storelocationid.trim()
        },
        async: false,
        dataType: "json",
        success: function (freebatchdetails) {
            //debugger;
            for (var j = 0; j < freebatchdetails.length; j++) {
                //debugger;
                if (freebatchdetails[j].PRODUCTID.toString().trim() == $('#PRODUCTID').val().trim() && freebatchdetails[j].BATCHNO.toString().trim() == $('#hdnbatchno').val().trim()) {

                    var index = j;
                    freebatchdetails.splice(index, 1);
                }
            }
            if (freebatchdetails.length > 0) {

                $("#freeProductDetailsGrid").empty();

                var tr;
                tr = $('#freeProductDetailsGrid');
                var HeaderCount = $('#freeProductDetailsGrid thead th').length;
                if (HeaderCount == 0) {
                    tr.append("<thead><tr><th style='text-align: center;'>Sl.No.</th><th style='display: none;'>Product ID</th><th>Product</th><th>Batch</th><th>Stock Qty(Pcs)</th><th>MRP</th><th>Rate</th><th>Mfg.Date</th><th>Exp.Date</th><th style='text-align: center;'>Qty(Pcs)</th><th style='display: none;'>Weight</th></tr></thead><tbody>");
                }
                for (var i = 0; i < freebatchdetails.length; i++) {

                    tr = $('<tr/>');
                    tr.append("<td style='text-align: center;'><label class='slno' id='lblfreebatchslno'></label></td>");//0
                    tr.append("<td style='display: none;'>" + freebatchdetails[i].PRODUCTID + "</td>");//1
                    tr.append("<td style='width:250px;'>" + freebatchdetails[i].PRODUCTNAME + "</td>");//2
                    tr.append("<td >" + freebatchdetails[i].BATCHNO + "</td>");//3
                    tr.append("<td style='text-align: right;'>" + parseFloat(freebatchdetails[i].INVOICESTOCKQTY).toFixed(0) + "</td>");//4
                    tr.append("<td style='text-align: right;'>" + parseFloat(freebatchdetails[i].MRP).toFixed(2) + "</td>");//5
                    tr.append("<td style='text-align: right;'>" + parseFloat(freebatchdetails[i].BCP).toFixed(2) + "</td>");//6
                    tr.append("<td >" + freebatchdetails[i].MFGDATE + "</td>");//7
                    tr.append("<td >" + freebatchdetails[i].EXPIRDATE + "</td>");//8
                    tr.append("<td style='text-align: center'><input type='text' class='gvfreequantity'  id='txtfreequantity' style='text-align: right; width:60px; height:18px'></input></td>");//9
                    tr.append("<td style='display: none;'>" + freebatchdetails[i].NETWEIGHT + "</td>");//10
                    $("#freeProductDetailsGrid").append(tr);
                }
                tr.append("</tbody>");
                FreeProductBatchDetailsRowCount();
            }
        },
        failure: function (freebatchdetails) {
            alert(freebatchdetails.responseText);
        },
        error: function (freebatchdetails) {
            alert(freebatchdetails.responseText);
        }
    });

}

/*Free Product Final Destination Start*/
function IsExistsInputFreeProduct(InputProductID, InputProductName, InputBatch, InputStockQty, InputMrp, InputRate, InputMfgDate, InputExpDate, InputQty, InputWeight) {
    //debugger;
    if ($('#finalfreeDetailsGrid').length) {
        var exists = false;
        var arraydetails = [];
        $('#finalfreeDetailsGrid tbody tr').each(function () {
            var dispatchdetail = {};
            var inputproductgrd = $(this).find('td:eq(1)').html().trim();
            var inputbatchgrd = $(this).find('td:eq(3)').html().trim();
            dispatchdetail.INPUTPRODUCT = inputproductgrd;
            dispatchdetail.INPUTBATCHNO = inputbatchgrd;
            arraydetails.push(dispatchdetail);
        });
        var jsondispatchobj = {};
        jsondispatchobj.schemeproductDetails = arraydetails;

        for (i = 0; i < jsondispatchobj.schemeproductDetails.length; i++) {
            if (jsondispatchobj.schemeproductDetails[i].INPUTPRODUCT.trim() == InputProductID.trim() && jsondispatchobj.schemeproductDetails[i].INPUTBATCHNO.trim() == InputBatch.trim()) {
                exists = true;
                break;
            }
        }
        if (exists != false) {
            toastr.error('Item already exists...!');
            return false;
        }
        else {
            addFreeFinalGrid(InputProductID, InputProductName, InputBatch, InputStockQty, InputMrp, InputRate, InputMfgDate, InputExpDate, InputQty, InputWeight);
        }
    }
}

function addFreeFinalGrid(InputProductID, InputProductName, InputBatch, InputStockQty, InputMrp, InputRate, InputMfgDate, InputExpDate, InputQty, InputWeight) {
    ////debugger;
    //Create Table 
    var tr;
    tr = $('#finalfreeDetailsGrid');
    var HeaderCount = $('#finalfreeDetailsGrid thead th').length;
    var FooterCount = $('#finalfreeDetailsGrid tfoot th').length;
    if (HeaderCount == 0) {
        tr.append("<thead><tr><th style='text-align: center;'>Sl.No.</th><th style='display: none;'>Product ID</th><th>Product</th><th>Batch</th><th>Stock Qty(Pcs)</th><th>MRP</th><th>Rate</th><th>Mfg.Date</th><th>Exp.Date</th><th>Qty(Pcs)</th><th style='display: none;'>Weight</th><th style='text-align: center;'>Delete</th></tr></thead>");
    }
    if (FooterCount == 0) {
        tr.append("<tfoot><tr><th></th><th style='display: none;'></th><th></th><th></th><th style='color: blue;'>Total</th><th></th><th></th><th></th><th></th><th style='color: blue;text-align: right;' id='tfFinalfreeQty'></th><th style='display: none;'></th><th></th></tr></tfoot><tbody>");
    }
    tr = $('<tr/>');
    tr.append("<td style='text-align: center;'><label class='slno' id='lblfreefinalslno'></label></td>");//0
    tr.append("<td style='display: none;'>" + InputProductID.toString().trim() + "</td>");//1
    tr.append("<td style='width:250px;'>" + InputProductName.toString().trim() + "</td>");//2
    tr.append("<td >" + InputBatch.toString().trim() + "</td>");//3
    tr.append("<td style='text-align: right;'>" + InputStockQty.toString().trim() + "</td>");//4
    tr.append("<td style='text-align: right;'>" + InputMrp.toString().trim() + "</td>");//5
    tr.append("<td style='text-align: right;'>" + InputRate.toString().trim() + "</td>");//6
    tr.append("<td >" + InputMfgDate.toString().trim() + "</td>");//7
    tr.append("<td >" + InputExpDate.toString().trim() + "</td>");//8
    tr.append("<td style='text-align: right;'>" + InputQty.toString().trim() + "</td>");//9
    tr.append("<td style='display: none;'>" + InputWeight.toString().trim() + "</td>");//10
    tr.append("<td style='text-align: center'><input type='image' class='gvFinalFreeDelete'  id='btnFinalFreedelete' <img src='../Images/ico_delete_16.png' title='Delete'/></input></td>")//11;
    $("#finalfreeDetailsGrid").append(tr);
    tr.append("</tbody>");
    CalculateFinalFreeFooter();
    FinalFreeProductBatchDetailsRowCount();
}

function CalculateFinalFreeFooter() {
    //debugger;
    var qty = 0;
    $('#finalfreeDetailsGrid tbody tr').each(function () {
        qty += parseInt($(this).find('td:eq(9)').html().trim());
    });
    $('tfoot th#tfFinalfreeQty').html(qty);
}
/*Free Product Final Destination End*/

/*Quantity Scheme Popup Save Start*/
function IsExistsQtySchemeProduct(QuantitySchemeID, SchemeProductID, SchemeProductName, SchemeProductBatchNo, SchemeAppQty, FinalProductID, FinalProductName,
    FinalPacksizeID, FinalPacksizeName, FinalQty, FinalMrp, FinalRate, FinalAmount, FinalBatch, FinalMfgDate, FinalExpDate, FinalWeight, qsGuid) {
    //debugger;
    if ($('#freeDetailsGrid').length) {
        var exists = false;
        var arraydetails = [];
        $('#freeDetailsGrid tbody tr').each(function () {
            var dispatchdetail = {};
            var inputproductgrd = $(this).find('td:eq(6)').html().trim();
            var inputbatchgrd = $(this).find('td:eq(16)').html().trim();
            dispatchdetail.FINALQTYPRODUCT = inputproductgrd;
            dispatchdetail.FINALQTYBATCHNO = inputbatchgrd;
            arraydetails.push(dispatchdetail);
        });
        var jsonqtyobj = {};
        jsonqtyobj.qtyproductDetails = arraydetails;

        for (i = 0; i < jsonqtyobj.qtyproductDetails.length; i++) {
            if (jsonqtyobj.qtyproductDetails[i].FINALQTYPRODUCT.trim() == FinalProductID.trim() && jsonqtyobj.qtyproductDetails[i].FINALQTYBATCHNO.trim() == FinalBatch.trim()) {
                exists = true;
                break;
            }
        }
        if (exists != false) {
            toastr.error('Item already exists...!');
            return false;
        }
        else {
            addFreeDetailsGrid(QuantitySchemeID, SchemeProductID, SchemeProductName, SchemeProductBatchNo, SchemeAppQty, FinalProductID, FinalProductName,
                FinalPacksizeID, FinalPacksizeName, FinalQty, FinalMrp, FinalRate, FinalAmount, FinalBatch, FinalMfgDate, FinalExpDate, FinalWeight, qsGuid)
        }
    }
}

function addFreeDetailsGrid(QuantitySchemeID, SchemeProductID, SchemeProductName, SchemeProductBatchNo, SchemeAppQty, FinalProductID, FinalProductName,
    FinalPacksizeID, FinalPacksizeName, FinalQty, FinalMrp, FinalRate, FinalAmount, FinalBatch, FinalMfgDate, FinalExpDate, FinalWeight, qsGuid) {


    ////debugger;
    //Create Table 
    var tr;
    tr = $('#freeDetailsGrid');
    var HeaderCount = $('#freeDetailsGrid thead th').length;
    var FooterCount = $('#freeDetailsGrid tfoot th').length;
    if (HeaderCount == 0) {
        tr.append("<thead><tr><th style='text-align: center;'>Sl.No.</th><th style='display: none;'>Scheme ID</th><th style='display: none;>Primary Product ID</th><th style='display: none;'>Primary Product</th><th style='display: none;'>Primary Batch</th><th style='display: none;'>Qty</th><th style='display: none;'>Scheme Product ID</th><th>Free Product</th><th>HSN Code</th><th style='display: none;'>Packsize ID</th><th style='display: none;'>Packsize</th><th>Free Qty(Pcs)</th><th>MRP</th><th style='display: none;'>RateDisc</th><th>Rate</th><th>Disc.Amt.</th><th>Batch</th><th style='display: none;'>Assesment(%)</th><th style='display: none;'>Assesment Amt.</th><th>Mfg.Date</th><th>Exp.Date</th><th style='display: none;'>Weight</th><th style='display: none;'>NSR</th><th style='display: none;'>Ratedisc Value</th><th>Net Amt</th><th style='display: none;'>QSGUID</th></tr></thead>");
    }
    if (FooterCount == 0) {
        tr.append("<tfoot><tr><th></th><th style='display: none;'></th><th style='display: none;></th><th style='display: none;'></th><th style='display: none;'></th><th style='display: none;'></th><th style='display: none;'></th><th></th><th style='color: blue;'>Total</th><th style='display: none;'></th><th style='display: none;'></th><th style='color: blue;text-align: right;' id='tfFreeQty'></th><th></th><th style='display: none;'></th><th></th><th style='color: blue;text-align: right;' id='tfFreeAmt'></th><th></th><th style='display: none;'></th><th style='display: none;'></th><th></th><th></th><th style='display: none;'></th><th style='display: none;'></th><th style='display: none;'></th><th style='color: blue;text-align: right;' id='tfFreeNetAmt'></th><th style='display: none;'></th></tr></tfoot><tbody>");
    }
    tr = $('<tr/>');

    //debugger;
    tr.append("<td style='text-align: center;'><label class='slno' id='lblfreeslno'></label></td>");//0
    tr.append("<td style='display: none;'>" + QuantitySchemeID.trim() + "</td>");//1
    tr.append("<td style='display: none;'>" + SchemeProductID.trim() + "</td>");//2
    tr.append("<td style='display: none;'>" + SchemeProductName.substr(0, SchemeProductName.indexOf("~")).trim() + "</td>");//3
    tr.append("<td style='display: none;'>" + SchemeProductBatchNo.trim() + "</td>");//4
    tr.append("<td style='display: none;'>" + SchemeAppQty.trim() + "</td>");//5
    tr.append("<td style='display: none;'>" + FinalProductID.trim() + "</td>");//6
    tr.append("<td style='width:250px;'>" + FinalProductName.trim() + "</td>");//7
    tr.append("<td >" + getHSNCode(FinalProductID.trim()) + "</td>");//8
    tr.append("<td style='display: none;'>" + FinalPacksizeID.trim() + "</td>");//9
    tr.append("<td style='display: none;'>" + FinalPacksizeName.trim() + "</td>");//10
    tr.append("<td style='text-align: right;'>" + parseFloat(FinalQty).toFixed(0) + "</td>");//11
    tr.append("<td style='text-align: right;'>" + parseFloat(FinalMrp).toFixed(2) + "</td>");//12
    tr.append("<td style='display: none;'>" + '0' + "</td>");//13
    tr.append("<td style='text-align: right;'>" + parseFloat(FinalRate).toFixed(2) + "</td>");//14
    tr.append("<td style='text-align: right;'>" + parseFloat(FinalAmount).toFixed(2) + "</td>");//15
    tr.append("<td >" + FinalBatch.trim() + "</td>");//16
    tr.append("<td style='display: none;'>" + '0' + "</td>");//17
    tr.append("<td style='display: none;'>" + '0' + "</td>");//18
    tr.append("<td >" + FinalMfgDate.trim() + "</td>");//19
    tr.append("<td >" + FinalExpDate.trim() + "</td>");//20
    tr.append("<td style='display: none;'>" + FinalWeight.trim() + "</td>");//21
    tr.append("<td style='display: none;'>" + FinalRate.trim() + "</td>");//22
    tr.append("<td style='display: none;'>" + '0' + "</td>");//23
    tr.append("<td style='text-align: right;'>" + parseFloat(FinalAmount).toFixed(2) + "</td>");//24
    tr.append("<td style='display: none;'>" + qsGuid.trim() + "</td>");//25
    $("#freeDetailsGrid").append(tr);

    RowCountFreeGrid();
    QuantitySchemeNotification = false;
}
/*Quantity Scheme Popup Save End*/


/*Add combo case breakup grid Start*/
function addComboBreakupGrid(primaryproductid, secondaryproductid, breakupqty) {
    ////debugger;
    //Create Table 
    var tr;
    tr = $('#comboBreakupGrid');
    var HeaderCount = $('#comboBreakupGrid thead th').length;
    if (HeaderCount == 0) {
        tr.append("<thead><tr><th >Primary ID</th><th>Secondary ID</th><th>Qty(Pcs)</th></tr></thead><tbody>");
    }
    tr = $('<tr/>');
    tr.append("<td >" + primaryproductid.toString().trim() + "</td>");//1
    tr.append("<td >" + secondaryproductid.toString().trim() + "</td>");//2
    tr.append("<td style='text-align: right;'>" + parseInt(breakupqty) + "</td>");//3
    $("#comboBreakupGrid").append(tr);
    tr.append("</tbody>");
}
/*Add combo case breakup grid End*/

function addProductInFreeGrid() {
    var remainingqty = 0;
    var totalqty = 0;
    var totalfreeqty = 0;
    var Freesrl = 0;
    var stockcheckingmessage = '';
    var productType = ''
    var productname = $('#PRODUCTID').find('option:selected').text().trim();
    var batchno = $("#hdnbatchno").val().trim();
    var Rate = 0;
    var Amount = 0;
    var MRP = $('#MRP').val();
    Rate = parseFloat($('#Rate').val().trim()).toFixed(2);

    stockcheckingmessage = getqtyinPCS($('#PRODUCTID').val().trim(), $('#PSID').val().trim(), $('#InvoiceQty').val().trim(), $('#InvoicePcs').val().trim(), $('#StockQty').val().trim(), '0', $('#hdndispatchID').val().trim(), $('#RemainingQty').val().trim())
    if (stockcheckingmessage != 'na') {
        toastr.warning('<b><font color=black>' + stockcheckingmessage + '</font></b>');
        return false;
    }
    else {

        productType = getProductType($('#PRODUCTID').val().trim());

        if ($("#chkFree").prop('checked') == true) {
            /*Free Issue Start*/
            Amount = parseFloat($("#hdninvoiceqty").val().trim()) * parseFloat(Rate);
            var tr;
            tr = $('#freeDetailsGrid');
            var HeaderCount = $('#freeDetailsGrid thead th').length;
            var FooterCount = $('#freeDetailsGrid tfoot th').length;
            if (HeaderCount == 0) {
                tr.append("<thead><tr><th  style='text-align: center;'>Delete</th><th style='display: none;'>Scheme ID</th><th style='display: none;>Primary Product ID</th><th style='display: none;'>Primary Product</th><th style='display: none;'>Primary Batch</th><th style='display: none;'>Qty</th><th style='display: none;'>Scheme Product ID</th><th>Free Product</th><th>HSN Code</th><th style='display: none;'>Packsize ID</th><th style='display: none;'>Packsize</th><th>Free Qty(Pcs)</th><th>MRP</th><th style='display: none;'>RateDisc</th><th>Rate</th><th>Disc.Amt.</th><th>Batch</th><th style='display: none;'>Assesment(%)</th><th style='display: none;'>Assesment Amt.</th><th>Mfg.Date</th><th>Exp.Date</th><th style='display: none;'>Weight</th><th style='display: none;'>NSR</th><th style='display: none;'>Ratedisc Value</th><th>Net Amt</th><th style='display: none;'>QSGUID</th></tr></thead><tbody>");
            }
            if (FooterCount == 0) {
                tr.append("<tfoot><tr><th></th><th style='display: none;'></th><th style='display: none;></th><th style='display: none;'></th><th style='display: none;'></th><th style='display: none;'></th><th style='display: none;'></th><th></th><th style='color: blue;'>Total</th><th style='display: none;'></th><th style='display: none;'></th><th style='color: blue;text-align: right;' id='tfFreeQty'></th><th></th><th style='display: none;'></th><th></th><th style='color: blue;text-align: right;' id='tfFreeAmt'></th><th></th><th style='display: none;'></th><th style='display: none;'></th><th></th><th></th><th style='display: none;'></th><th style='display: none;'></th><th style='display: none;'></th><th style='color: blue;text-align: right;' id='tfFreeNetAmt'></th><th style='display: none;'></th></tr></tfoot><tbody>");
            }
            tr = $('<tr/>');
            tr.append("<td style='text-align: center;'><label class='slno' id='lblfreeslno'></label><span><input type='button' class='action-icons c-delete' id='btnfreedelete' value='Delete' /></span></td>");//0
            tr.append("<td style='display: none;'>" + '0' + "</td>");//1
            tr.append("<td style='display: none;'>" + $('#PRODUCTID').val().trim() + "</td>");//2
            tr.append("<td style='display: none;'>" + productname.substr(0, productname.indexOf("~")) + "</td>");//3
            tr.append("<td style='display: none;'>" + batchno + "</td>");//4
            tr.append("<td style='display: none;'>" + parseInt($("#hdninvoiceqty").val().trim()) + "</td>");//5
            tr.append("<td style='display: none;'>" + $('#PRODUCTID').val().trim() + "</td>");//6
            tr.append("<td style='width:250px;'>" + productname.substr(0, productname.indexOf("~")) + "</td>");//7
            tr.append("<td >" + getHSNCode($('#PRODUCTID').val().trim()) + "</td>");//8
            tr.append("<td style='display: none;'>" + 'B9F29D12-DE94-40F1-A668-C79BF1BF4425' + "</td>");//9
            tr.append("<td style='display: none;'>" + 'PCS' + "</td>");//10
            tr.append("<td style='text-align: right;'>" + parseInt($("#hdninvoiceqty").val().trim()) + "</td>");//11
            tr.append("<td style='text-align: right;'>" + parseFloat(MRP).toFixed(2) + "</td>");//12
            tr.append("<td style='display: none;'>" + '0' + "</td>");//13
            if (productType == 'FG') {
                tr.append("<td style='text-align: right;'>" + '0.00' + "</td>");//14
                tr.append("<td style='text-align: right;'>" + '0.00' + "</td>");//15
            }
            else {
                tr.append("<td style='text-align: right;'>" + '0.00' + "</td>");//14
                tr.append("<td style='text-align: right;'>" + '0.00' + "</td>");//15
            }
            tr.append("<td >" + batchno + "</td>");//16
            tr.append("<td style='display: none;'>" + '0' + "</td>");//17
            tr.append("<td style='display: none;'>" + '0' + "</td>");//18
            tr.append("<td >" + $('#hdn_mfgdate').val().trim() + "</td>");//19
            tr.append("<td >" + $('#hdn_exprdate').val().trim() + "</td>");//20
            tr.append("<td style='display: none;'>" + '0 ML' + "</td>");//21
            if (productType == 'FG') {
                tr.append("<td style='display: none;'>" + '0.00' + "</td>");//22
            }
            else {
                tr.append("<td style='display: none;'>" + '0.00' + "</td>");//22
            }
            tr.append("<td style='display: none;'>" + '0' + "</td>");//23
            if (productType == 'FG') {
                tr.append("<td style='text-align: right;'>" + '0.00' + "</td>");//24
            }
            else {
                tr.append("<td style='text-align: right;'>" + '0.00' + "</td>");//24
            }
            tr.append("<td style='display: none;'>" + '0' + "</td>");//25
            $("#freeDetailsGrid").append(tr);
            tr.append("</tbody>");
            RowCountFreeGrid();
            CalculateAmount();

            /*Free Issue End*/
        }
    }
    clearafterAdd();
    $('#productDetailsGrid tbody tr').each(function () {
        if ($(this).find('td:eq(1)').html().trim() == $('#PRODUCTID').val().trim()) {
            totalqty += parseInt(getCaseToPcsConversion($(this).find('td:eq(1)').html().trim(), '1970C78A-D062-4FE9-85C2-3E12490463AF', 'B9F29D12-DE94-40F1-A668-C79BF1BF4425', $(this).find('td:eq(10)').html().trim(), $(this).find('td:eq(11)').html().trim()));
        }
    });
    $('#freeDetailsGrid tbody tr').each(function () {
        //debugger;
        if ($(this).find('td:eq(2)').html().trim() == $('#PRODUCTID').val().trim()) {
            totalfreeqty += parseInt($(this).find('td:eq(11)').html().trim());
        }
    });
    remainingqty = parseInt($("#hdnOrderqtyPcs").val()) - (parseInt($("#hdnFinalDeliveredqtyPcs").val()) + parseInt(totalqty) + parseInt(totalfreeqty));
    $("#RemainingQty").val(parseInt(remainingqty));

    $("#PRODUCTID > option").each(function () {
        if ($(this).val().trim() != '0') {
            if ($(this).val().trim() == $('#PRODUCTID').val().trim()) {
                if (parseInt(remainingqty) <= 0) {

                    $(this).css("color", "red");

                }
                else {
                    $(this).css("color", "");
                }
            }
        }
    });
    $("#PRODUCTID").chosen({
        search_contains: true
    });
    $("#PRODUCTID").trigger("chosen:updated");

    bindbatchno('1');
}

function addTaxInTaxGrid(TaxId, Taxpercent, Tax, mrp) {
    var Taxsrl = 0;
    var productid = $('#PRODUCTID').val().trim();
    var batchno = $("#hdnbatchno").val().trim();
    var Taxid = TaxId;
    var TaxPercent = Taxpercent;
    var Taxvalue = Tax;
    var Tag = 'P';
    var MRP = mrp;

    /*Tax Grid Start*/

    var tr;
    tr = $('#taxDetailsGrid');
    var HeaderCount = $('#taxDetailsGrid thead th').length;
    var FooterCount = $('#taxDetailsGrid tfoot th').length;
    if (HeaderCount == 0) {
        tr.append("<thead><tr><th>Sl.No</th><th>Primary Product ID</th><th>Primary Product Batch</th><th>ProductID</th><th>Batch</th><th>TaxID</th><th>TaxPercentage</th><th>Taxvalue</th><th>Tag</th><th>Mrp</th></tr></thead><tbody>");
    }
    if (FooterCount == 0) {
        tr.append("<tfoot><tr><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr></tfoot><tbody>");
    }
    tr = $('<tr/>');
    tr.append("<td style='text-align: center;'><label class='slno' id='lbltaxslno'></label></td>");//0
    tr.append("<td >" + productid + "</td>");//1
    tr.append("<td >" + batchno + "</td>");//2
    tr.append("<td >" + productid + "</td>");//3
    tr.append("<td >" + batchno + "</td>");//4
    tr.append("<td >" + Taxid + "</td>");//5
    tr.append("<td >" + TaxPercent.toFixed(2) + "</td>");//6
    tr.append("<td >" + Taxvalue.toFixed(2) + "</td>");//7
    tr.append("<td >" + Tag + "</td>");//8
    tr.append("<td >" + MRP + "</td>");//9

    $("#taxDetailsGrid").append(tr);
    tr.append("</tbody>");


    /*Tax Grid Start*/

}

function finyearChecking(finyear) {

    //debugger;
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
            //debugger;
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
        //maxDate: 'today',
        maxDate: new Date(currentdt),
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
    $("#ICDSDate").datepicker({
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
    $("#InvoiceDate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date(currentdt));
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
    //debugger;
    var SourceDepot = $("#BRID");
    var SearchDepot = $("#SearchDepotID");
    var depotlength = 0;
    $.ajax({
        type: "POST",
        url: "/TranDepot/GetDepot",
        data: { UserID: userid },
        async: false,
        dataType: "json",
        success: function (response) {
            //debugger;
            depotlength = response.length;
            SourceDepot.empty();
            $.each(response, function () {
                SourceDepot.append($("<option></option>").val(this['BRID']).html(this['BRNAME']));
                SearchDepot.append($("<option></option>").val(this['BRID']).html(this['BRNAME']));
            });
            if ($("#hdnTPU").val() == 'D' || $("#hdnTPU").val() == 'EXPU') {
                //debugger;
                $("#BRID").val($("#hdnDepotID").val().trim());
                $("#SearchDepotID").val($("#hdnDepotID").val().trim());
            }
            else {
                //debugger;
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

function bindCustomer(bsid, depotid, invoiceid) {
    var Customer = $("#CUSTOMERID");
    var customerlength = 0;
    $.ajax({
        type: "POST",
        url: "/TranDepot/GetMTCustomer",
        data: { BSID: bsid, DepotID: depotid, InvoiceID: invoiceid },
        async: false,
        dataType: "json",
        success: function (response) {
            customerlength = response.length;
            if (customerlength == 1) {
                Customer.empty();
                $.each(response, function () {
                    //debugger;
                    Customer.append($("<option></option>").val(this['CUSTOMERID']).html(this['CUSTOMERNAME']));
                    var CustomerID = $('#CUSTOMERID').val().trim();
                    clearafterCustomerSelection();

                    var MonthName = '';
                    var Month = $('#InvoiceDate').val().trim();
                    Month = Month.substr(3, 2);
                    switch (Month) {
                        case '01':
                            MonthName = "JAN.";
                            break;
                        case '02':
                            MonthName = "FEB.";
                            break;
                        case '03':
                            MonthName = "MAR.";
                            break;
                        case '04':
                            MonthName = "APR.";
                            break;
                        case '05':
                            MonthName = "MAY";
                            break;
                        case '06':
                            MonthName = "JUN.";
                            break;
                        case '07':
                            MonthName = "JUL.";
                            break;
                        case '08':
                            MonthName = "AUG.";
                            break;
                        case '09':
                            MonthName = "SEP.";
                            break;
                        case '10':
                            MonthName = "OCT.";
                            break;
                        case '11':
                            MonthName = "NOV.";
                            break;
                        case '12':
                            MonthName = "DEC.";
                            break;
                    }
                    $('#spnInvLimit').text('Inv Limit,Done & Bal For ' + MonthName + ' ');
                    $('#spnTGT').text('Tgt,Bal & Ach(%) For ' + MonthName + ' ');

                    bindtaxcount();
                    bindSaleorder(CustomerID.trim());
                    ClBalanceCrLimit(CustomerID.trim(), $('#BRID').val().trim(), $('#hdndispatchID').val().trim(), $('#hdnFinYear').val().trim());
                    CustomerDetails(CustomerID.trim(), $('#BRID').val().trim(), Month, $('#hdndispatchID').val().trim(), $('#hdnFinYear').val().trim());

                    $("#SaleOrderID").chosen({
                        search_contains: true
                    });
                    $("#SaleOrderID").trigger("chosen:updated");

                    $("#PRODUCTID").chosen({
                        search_contains: true
                    });
                    $("#PRODUCTID").trigger("chosen:updated");

                    $('#NetAmt2').val('0.00');
                    $("#SaleOrderID").focus();
                    $('#SaleOrderID').trigger('chosen:activate');
                });
            }
            else if (customerlength > 1) {

                Customer.empty().append('<option selected="selected" value="0">Please select</option>');
                $.each(response, function () {
                    Customer.append($("<option></option>").val(this['CUSTOMERID']).html(this['CUSTOMERNAME']));
                });

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

function bindSaleorder(customerid) {
    //debugger;
    var Saleorder = $("#SaleOrderID");
    var orderlength = 0;
    $.ajax({
        type: "POST",
        url: "/TranDepot/GetCPCSaleOrder",
        data: { CustomerID: customerid },
        async: false,
        dataType: "json",
        success: function (response) {
            orderlength = response.length;
            if (orderlength == 1) {
                Saleorder.empty();
                $.each(response, function () {
                    ////debugger;
                    Saleorder.append($("<option></option>").val(this['SALEORDERID']).html(this['SALEORDERNO']));
                    var SaleorderID = $('#SaleOrderID').val().trim(); 
                    bindICDS($('#SaleOrderID').val().trim());
                    bindCPCProduct(SaleorderID.trim());
                    ChangeProductColour(SaleorderID.trim(), $('#PSID').val().trim(), $('#hdndispatchID').val().trim());

                    $("#PRODUCTID").chosen({
                        search_contains: true
                    });
                    $("#PRODUCTID").trigger("chosen:updated");
                   
                    $("#PRODUCTID").focus();
                    $('#PRODUCTID').trigger('chosen:activate');
                });
            }
            else if (orderlength > 1) {

                Saleorder.empty().append('<option selected="selected" value="0">Please select</option>');
                $.each(response, function () {
                    Saleorder.append($("<option></option>").val(this['SALEORDERID']).html(this['SALEORDERNO']));
                });

                $("#SaleOrderID").focus();
                $('#SaleOrderID').trigger('chosen:activate');

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

function binCsdComboProduct(bsid) {

    //debugger;
    $.ajax({
        type: "POST",
        url: "/TranDepot/GetCSDComboProduct",
        data: {BSID: bsid.trim()},
        async: false,
        dataType: "json",
        success: function (comboproductlist) {
            //debugger;
            if (comboproductlist.length > 0) {

                $("#comboProductGrid").empty();

                var tr;
                tr = $('#comboProductGrid');
                var HeaderCount = $('#comboProductGrid thead th').length;
                if (HeaderCount == 0) {
                    tr.append("<thead><tr><th style='display: none;'>Primary ID</th><th>Primary Product</th><th style='display: none;'>Secondary ID</th><th>Secondary Product</th><th>Single Cartoon</th><th>Qty</th></tr></thead><tbody>");
                }
                for (var i = 0; i < comboproductlist.length; i++) {

                    tr = $('<tr/>');
                    tr.append("<td style='display: none;'>" + comboproductlist[i].PRIMARYPRODUCTID + "</td>");//0
                    tr.append("<td style='width:250px;'>" + comboproductlist[i].PRIMARYPRODUCTNAME + "</td>");//1
                    tr.append("<td style='display: none;'>" + comboproductlist[i].SECONDARYPRODUCTID + "</td>");//2
                    tr.append("<td style='width:250px;'>" + comboproductlist[i].SECONDARYPRODUCTNAME + "</td>");//3
                    tr.append("<td >" + comboproductlist[i].ISSINGLECARTOON + "</td>");//4
                    tr.append("<td >" + comboproductlist[i].QTY + "</td>");//5
                    $("#comboProductGrid").append(tr);
                }
                tr.append("</tbody>");
                
            }
        },
        failure: function (comboproductlist) {
            alert(comboproductlist.responseText);
        },
        error: function (comboproductlist) {
            alert(comboproductlist.responseText);
        }
    });

}

function bindtransporter(sourceDepot, txnid) {
    var Transporter = $("#TransporterID");
    $.ajax({
        type: "POST",
        url: "/tranfac/GetTransporter",
        data: { SourceDepot: sourceDepot, TxnID: txnid },
        async: false,
        dataType: "json",
        success: function (response) {
            Transporter.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(response, function () {
                Transporter.append($("<option></option>").val(this['TransporterID']).html(this['TransporterNAME']));
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
    //var counter = 0;
    $.ajax({
        type: "POST",
        url: "/TranDepot/GetPacksize",
        data: '{}',
        async: false,
        dataType: "json",
        success: function (response) {
            Packsize.empty();
            $.each(response, function () {
                Packsize.append($("<option></option>").val(this['PSID']).html(this['PSNAME']));
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

function GetTaxOnEdit(invoiceid, taxid, productid, batchno) {
    ////debugger;
    var returnValue = null;
    $.ajax({
        type: "POST",
        url: "/TranDepot/GetGTInvoiceTaxOnEdit",
        data: { InvoiceID: invoiceid, TaxID: taxid, ProductID: productid, BatchNo: batchno },
        dataType: "json",
        async: false,
        success: function (invoicetaxonedit) {
            ////debugger;
            if (invoicetaxonedit.length > 0) {

                $.each(invoicetaxonedit, function (key, item) {

                    $("#hdntaxpercentage").val(invoicetaxonedit[0].TAXPERCENTAGE);
                    returnValue = $("#hdntaxpercentage").val();
                });

            }
            else {
                $("#hdntaxcount").val(0);
                $("#hdntaxnameIGST").val('');
                $("#hdntaxnameCGST").val('');
                $("#hdntaxnameSGST").val('');
            }
            return false;
        },
        failure: function (invoicetaxonedit) {
            alert(invoicetaxonedit.responseText);
        },
        error: function (invoicetaxonedit) {
            alert(invoicetaxonedit.responseText);
        }
    });
    return returnValue;
}

function bindAllProduct() {
    var Product = $("#PRODUCTID");
    $("#dialog").dialog({
        autoOpen: true,
        modal: true,
        title: "Loading.."
    });
    $("#imgLoader").css("visibility", "visible");
    $.ajax({
        type: "POST",
        url: "/tranfac/GetProduct",
        data: { SourceDepot: $('#BRID').val(), Type: 'FG' },
        dataType: "json",
        success: function (response) {
            Product.empty().append('<option selected="selected" value="0">Select Product</option>');
            $.each(response, function () {
                Product.append($("<option></option>").val(this['PRODUCTID']).html(this['PRODUCTNAME']));
            });
            $("#PRODUCTID").chosen({
                search_contains: true
            });
            $("#PRODUCTID").trigger("chosen:updated");

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

function bindbatchno(focusflag1) {
    //debugger;
    var ddlBatch = $("#ddlBatch");
    var Batch = '0';
    var desc = '';
    var desc1 = '';
    var val1 = '';
    var listItems = '';
    /*listItems = "<option value='0'>Select Batch</option>";*/
    desc = 'Stock Qty'.padEnd(15, '\u00A0') + repeatstr("&nbsp;", 10) + 'MRP'.padEnd(13, '\u00A0') + repeatstr("&nbsp;", 15) + 'Mfg.Date'.padEnd(14, '\u00A0') + repeatstr("&nbsp;", 13) + 'Exp.Date'.padEnd(14, '\u00A0') + repeatstr("&nbsp;", 15) + 'Batch'.padEnd(15, '\u00A0');
    //listItems += "<option value='0'>" + desc + "</option>";
    $("#dialog").dialog({
        autoOpen: true,
        modal: true,
        title: "Loading.."
    });
    $("#imgLoader").css("visibility", "visible");
    $.ajax({
        type: "POST",
        url: "/TranDepot/GetBatchDetails",
        data: { DepotID: $('#BRID').val(), ProductID: $('#PRODUCTID').val(), PacksizeID: $('#PSID').val(), BatchNo: Batch },
        dataType: "json",
        success: function (batchlist) {

            /*FIFO checking for Billing Product Start*/
            if ($('#productDetailsGrid').length) {
                var flag = false;
                var count = 0;
                var BillProductId = '';
                var BillProductBatchNo = '';
                $('#productDetailsGrid tbody tr').each(function () {
                    //debugger;
                    flag = true;
                    count = count + 1;
                    BillProductId = $(this).find('td:eq(1)').html().trim();
                    BillProductBatchNo = $(this).find('td:eq(12)').html().trim();
                    if ($('#PRODUCTID').val().trim() == BillProductId.trim()) {

                        for (var j = 0; j < batchlist.length; j++) {

                            var stockBatchNo = batchlist[j].BATCHNO.toString().trim();

                            if (stockBatchNo == BillProductBatchNo) {
                                var index = j;
                                batchlist.splice(index, 1);
                            }
                        }
                    }
                });
            }
            /*FIFO checking for Billing Product End*/


            /*FIFO checking for Free Product Start*/
            if ($('#freeDetailsGrid').length) {
                var freeflag = false;
                var freecount = 0;
                var FreeProductId = '';
                var FreeProductBatchNo = '';
                $('#freeDetailsGrid tbody tr').each(function () {
                    //debugger;
                    freeflag = true;
                    freecount = count + 1;
                    FreeProductId = $(this).find('td:eq(2)').html().trim();
                    FreeProductBatchNo = $(this).find('td:eq(4)').html().trim();
                    if ($('#PRODUCTID').val().trim() == FreeProductId.trim()) {

                        for (var k = 0; k < batchlist.length; k++) {

                            var stockBatchNo = batchlist[k].BATCHNO.toString().trim();

                            if (stockBatchNo == FreeProductBatchNo) {
                                var index = k;
                                batchlist.splice(index, 1);
                            }
                        }
                    }
                });
            }
            /*FIFO checking for Free Product End*/

            //debugger;
            var counter = 0;
            $('#ddlBatch').empty();
            $.each(batchlist, function () {
                val1 = this["BATCHNO"] + "|" + this["BatchSTOCKQTY"] + "|" + this["BatchMRP"] + "|" + this["BatchMFGDATE"] + "|" + this["BatchEXPIRDATE"];
                desc1 = this["BatchSTOCKQTY"].padEnd(15, '\u00A0') + repeatstr("&nbsp;", 9) + this["BatchMRP"].padEnd(15, '\u00A0') + repeatstr("&nbsp;", 15) + this["BatchMFGDATE"].padEnd(15, '\u00A0') + repeatstr("&nbsp;", 10) + this["BatchEXPIRDATE"].padEnd(15, '\u00A0') + repeatstr("&nbsp;", 12) + this["BATCHNO"].padEnd(15, '\u00A0')
                listItems += "<option value='" + val1 + "'>" + desc1 + "</option>";
                counter = counter + 1;
            });
            ddlBatch.append(listItems);

            //$('#ddlBatch:not(:selected)').attr('disabled', true);
            //$('#ddlBatch').attr("style", "pointer-events: none;");

            //debugger;
            if (counter > 0) {
                //debugger;
                //if (counter == 1) {
                $("#ddlBatch").val(batchlist[0].BATCHNO.toString().trim() + "|" + batchlist[0].BatchSTOCKQTY.toString().trim() + "|" + batchlist[0].BatchMRP.toString().trim() + "|" + batchlist[0].BatchMFGDATE.toString().trim() + "|" + batchlist[0].BatchEXPIRDATE.toString().trim())
                getBatchDetails(flag);
                //$("#InvoiceQty").focus();
                //}
            }
            else {
                toastr.info('<b>Stock not available..</b>');
            }

            $("#imgLoader").css("visibility", "hidden");
            $("#dialog").dialog("close");
            if (focusflag1 == '0') {
                $("#InvoiceQty").trigger("focus");
            }
            else if (focusflag1 == '1') {
                $("#PRODUCTID").focus();
                $('#PRODUCTID').trigger('chosen:activate');
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

function getBatchDetails(focusflag) {
    //debugger;
    var batchmrp;
    var batchassessvalue;
    var batchmfgdt;
    var batchexpdt;
    $('#ddlBatch > option').each(function () {
        $(this).removeAttr("disabled");
    });
    var strval2 = $("#ddlBatch").val();
    var splitval = strval2.split('|');
    var transferdate = $("#InvoiceDate").val();
    var productid = $("#PRODUCTID").val();

    var batchno = splitval[0];
    var stockqty = splitval[1];
    var mrp = splitval[2];
    var assessmentpercent = '0';
    if (mrp == '0') {
        assessmentpercent = '115';
    }
    else {
        assessmentpercent = '65';
    }
    var mfgdatre = splitval[3];
    var expirydate = splitval[4];
    if ($('#PSID').val().trim() == '1970C78A-D062-4FE9-85C2-3E12490463AF') {
        $("#StockQty").val(stockqty);
    }
    else {
        $("#StockQty").val(parseInt(stockqty));
    }
    batchmrp = mrp;
    batchassessvalue = assessmentpercent;
    batchmfgdt = mfgdatre;
    batchexpdt = expirydate;
    $("#MRP").val(mrp);
    $("#hdnmrp").val(mrp);
    $("#hdn_ASSESMENTPERCENTAGE").val(assessmentpercent);
    $("#hdn_mfgdate").val(mfgdatre);
    $("#hdn_exprdate").val(expirydate);
    $("#hdnbatchno").val(batchno);
    bindRate($('#CUSTOMERID').val().trim(), $('#PRODUCTID').val().trim(), $('#InvoiceDate').val().trim(), $('#MRP').val().trim(), $('#BRID').val().trim(), menuid.trim(), BusinessSegment.trim(), $('#hdnGroupID').val().trim(), 'F');
    if (focusflag == '0') {
        $("#InvoiceQty").focus();
    }
    else if (focusflag == '1') {
        $("#PRODUCTID").focus();
        $('#PRODUCTID').trigger('chosen:activate');
    }

    $('#ddlBatch > option').each(function () {
        $(this).prop('disabled', true);
    });
}

function bindRate(customerid, productid, invoicedate, mrp, depotid, menuid, bsid, groupid, tag) {

    $.ajax({
        type: "POST",
        url: "/TranDepot/GetBCP",
        data: { Customerid: customerid, Productid: productid, Invoicedate: invoicedate, Mrp: mrp, Depotid: depotid, Menuid: menuid, BSid: bsid, Groupid: groupid, Tag: tag },
        dataType: "json",
        async: false,
        success: function (bcp) {
            if (bcp.length > 0) {
                $.each(bcp, function (key, item) {
                    $("#Rate").val(item.BASECOSTPRICE);
                });
            }
            else {
                $("#Rate").val('0.00');
            }
        },
        failure: function (bcp) {
            alert(bcp.responseText);
        },
        error: function (bcp) {
            alert(bcp.responseText);
        }
    });
}

function bindorderdetails(orderid, productid, packsizeid, invoiceid) {
    //debugger;
    var orderqtypcs = 0;
    var deliveredqtypcs = 0;
    var dispatchqtypcs = 0;
    var finaldeliveredqty = 0;
    var totalqty = 0;
    var totalfreeqty = 0;
    var remainingqty = 0;

    $.ajax({
        type: "POST",
        url: "/TranDepot/GetOrdervsDispatch",
        data: { OrderID: orderid.trim(), ProductID: productid.trim(), PacksizeID: packsizeid.trim(), InvoiceID: invoiceid.trim()},
        dataType: "json",
        async: false,
        success: function (ordervsdispatch) {
            //debugger;
            if (ordervsdispatch.length > 0) {
                $.each(ordervsdispatch, function (key, item) {
                    $("#OrderQty").val(item.ORDERQTY);
                    $("#DeliveredQty").val(item.DESPATCHQTY);
                    orderqtypcs = item.ORDERQTYPCS;
                    $("#hdnOrderqtyPcs").val(orderqtypcs);
                    deliveredqtypcs = item.DELIVERYQTYPCS;
                    dispatchqtypcs = item.DESPATCHQTYPCS;
                    
                });
            }
            else {
                $("#OrderQty").val('');
                $("#DeliveredQty").val('');
                orderqtypcs = 0;
                $("#hdnOrderqtyPcs").val('0');
                deliveredqtypcs = 0;
                dispatchqtypcs = 0;
            }
        },
        failure: function (orderlist) {
            alert(orderlist.responseText);
        },
        error: function (orderlist) {
            alert(orderlist.responseText);
        }
    });

    if ($("#StockQty").val() == '') {

        $("#StockQty").val('0');
    }

    $.ajax({
        type: "POST",
        url: "/TranDepot/GetQuantityInPcs",
        data: { ProductID: productid.trim(), PacksizefromID: packsizeid.trim(), DeliveredQty: '0', StockQty: $("#StockQty").val().trim(), OrderID: orderid.trim(), InvoiceID: invoiceid.trim() },
        dataType: "json",
        async: false,
        success: function (qtyinpcs) {
            //debugger;
            if (qtyinpcs.length > 0) {
                $.each(qtyinpcs, function (key, item) {
                    finaldeliveredqty = item.FINAL_DELIVEREDQTY;
                    $("#hdnFinalDeliveredqtyPcs").val(finaldeliveredqty);
                });
            }
            else {
                finaldeliveredqty = 0;
                $("#hdnFinalDeliveredqtyPcs").val('0');
            }
        },
        failure: function (orderlist) {
            alert(orderlist.responseText);
        },
        error: function (orderlist) {
            alert(orderlist.responseText);
        }
    });

    $('#productDetailsGrid tbody tr').each(function () {
        if ($(this).find('td:eq(1)').html().trim() == productid) {
            totalqty += parseInt(getCaseToPcsConversion($(this).find('td:eq(1)').html().trim(), '1970C78A-D062-4FE9-85C2-3E12490463AF', 'B9F29D12-DE94-40F1-A668-C79BF1BF4425', $(this).find('td:eq(10)').html().trim(), $(this).find('td:eq(11)').html().trim()));
        }
    });
    $('#freeDetailsGrid tbody tr').each(function () {
        //debugger;
        if ($(this).find('td:eq(2)').html().trim() == productid) {
            totalfreeqty += parseInt($(this).find('td:eq(11)').html().trim());
        }
    });

    remainingqty = parseInt(orderqtypcs) - (parseInt(finaldeliveredqty) + parseInt(totalqty) + parseInt(totalfreeqty));
    $("#RemainingQty").val(parseInt(remainingqty));

    $("#PRODUCTID > option").each(function () {
        if ($(this).val().trim() != '0') {
            if ($(this).val().trim() == productid) {
                if (parseInt(remainingqty) <= 0) {

                    $(this).css("color", "red");

                }
                else {
                    $(this).css("color", "");
                }
            }
        }
    });
    $("#PRODUCTID").chosen({
        search_contains: true
    });
    $("#PRODUCTID").trigger("chosen:updated");
}

function bindICDS(saleorderid) {
    $.ajax({
        type: "POST",
        url: "/TranDepot/GetICDSDetails",
        data: { OrderID: saleorderid},
        dataType: "json",
        async: false,
        success: function (icds) {
            if (icds.length > 0) {
                $.each(icds, function (key, item) {
                    $("#ICDSNo").val(item.ICDSNO);
                    $("#ICDSDate").val(item.ICDSDATE);
                });
            }
            else {
                $("#ICDSNo").val('');
                $("#ICDSDate").val('');
            }
        },
        failure: function (icds) {
            alert(icds.responseText);
        },
        error: function (icds) {
            alert(icds.responseText);
        }
    });
}

function addRowinTaxTable(taxID, TaxPercentage, TaxAmount, MRP, TaxFlag) {
    $.ajax({
        type: "POST",
        url: "/TranDepot/FillCSDInvoiceTaxDatatable",
        data: { Productid: $('#PRODUCTID').val(), BatchNo: $("#hdnbatchno").val(), TaxID: taxID, Percentage: parseFloat(TaxPercentage), TaxValue: parseFloat(TaxAmount), MRP: parseFloat(MRP), Flag: TaxFlag },
        async: false,
        dataType: "json",
        success: function (response) {

        }
    });
}

function addRowinTaxTableEdit(ProductID, batch, taxid, TaxPercentage, TaxAmount, MRP, TaxFlag) {
    $.ajax({
        type: "POST",
        url: "/TranDepot/FillCSDInvoiceTaxDatatable",
        data: { Productid: ProductID.trim(), BatchNo: batch.trim(), TaxID: taxid.trim(), Percentage: parseFloat(TaxPercentage), TaxValue: parseFloat(TaxAmount), MRP: parseFloat(MRP), Flag: TaxFlag },
        async: false,
        dataType: "json",
        success: function (response) {

        }
    });
}

function deleteRowfromTaxTable(grdproductid, grdbatchno) {
    $.ajax({
        type: "POST",
        url: "/TranDepot/DeleteTaxDatatableCSDInvoice",
        data: { Productid: grdproductid.trim(), BatchNo: grdbatchno.trim() },
        dataType: "json",
        success: function (response) {

        }
    });
}

function RowCount() {
    //debugger;
    var rowCount = document.getElementById("productDetailsGrid").rows.length - 2;
    var count = 0;
    if (rowCount > 0) {
        $("#CUSTOMERID").prop("disabled", true);
        $("#CUSTOMERID").chosen({
            search_contains: true
        });
        $("#CUSTOMERID").trigger("chosen:updated");

        $("#SaleOrderID").prop("disabled", true);
        $("#SaleOrderID").chosen({
            search_contains: true
        });
        $("#SaleOrderID").trigger("chosen:updated");
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

function RowCountCsdInvoiceList() {
    var table = document.getElementById("CSDInvoiceGrid");
    var rowCount = document.getElementById("CSDInvoiceGrid").rows.length - 1;
    if (rowCount > 0) {
        for (var i = 1; i <= rowCount; i++) {
            table.rows[i].cells[0].innerHTML = i.toString();
        }
    }
}

function RowCountFreeGrid() {
    //debugger;
    var freerowCount = document.getElementById("freeDetailsGrid").rows.length - 2;
    var freecount = 0;
    if (freerowCount > 0) {
        $("#CUSTOMERID").prop("disabled", true);
        $("#CUSTOMERID").chosen({
            search_contains: true
        });
        $("#CUSTOMERID").trigger("chosen:updated");

        $("#SaleOrderID").prop("disabled", true);
        $("#SaleOrderID").chosen({
            search_contains: true
        });
        $("#SaleOrderID").trigger("chosen:updated");

        $('#freeDetailsGrid tbody tr').each(function () {
            freecount = freecount + 1;
            $(this).find('#lblfreeslno').text(freecount.toString().trim());
        })
    }
}

function RowCountFreeGridEdit() {
    var table = document.getElementById("freeDetailsGrid");
    var rowCount = document.getElementById("freeDetailsGrid").rows.length - 2;
    var count = 0;
    if (rowCount > 0) {
        $('#freeDetailsGrid tbody tr').each(function () {
            count = count + 1;
            $(this).find('#lblfreeslno').text(count.toString().trim());
        })
    }
}

function FreeProductBatchDetailsRowCount() {
    //debugger;
    var batchrowCount = document.getElementById("freeProductDetailsGrid").rows.length - 1;
    var batchcount = 0;
    if (batchrowCount > 0) {
        $('#freeProductDetailsGrid tbody tr').each(function () {
            batchcount = batchcount + 1;
            $(this).find('#lblfreebatchslno').text(batchcount.toString().trim());
        })
    }
}

function FinalFreeProductBatchDetailsRowCount() {
    //debugger;
    var finalbatchrowCount = document.getElementById("finalfreeDetailsGrid").rows.length - 1;
    var finalbatchcount = 0;
    if (finalbatchrowCount > 0) {
        $('#finalfreeDetailsGrid tbody tr').each(function () {
            finalbatchcount = finalbatchcount + 1;
            $(this).find('#lblfreefinalslno').text(finalbatchcount.toString().trim());
        })
    }
}

function CalculateAmount() {
    ////debugger;
    var totalbasicamt = 0;
    var totaltaxamt = 0;
    var totalschemeamt = 0;
    var totalfreeamt = 0;
    var totaldiscountamt = 0;
    var totalssdiscountamt = 0;
    var totalbasicplustaxamt = 0;
    var decimalvalue = 0;
    var totalcaseqty = 0;
    var totalpcsqty = 0;
    var totalBillpcsqty = 0;
    var totalFinalpcsqty = 0;
    var freetotalpcsqty = 0;
    var parts = 0;
    var finalamt = 0;
    var rowCount = 0;
    var freerowCount = 0;
    var Balance = 0;
    Balance = parseFloat($("#ClosingBalance").val()) + parseFloat($("#CreditLimit").val());
    rowCount = document.getElementById("productDetailsGrid").rows.length - 2;
    freerowCount = document.getElementById("freeDetailsGrid").rows.length - 2;



    if (rowCount > 0 && freerowCount > 0) {

        if ($("#hdntaxcount").val() == '1') {
            $('#productDetailsGrid tbody tr').each(function () {

                totalbasicamt += parseFloat($(this).find('td:eq(21)').html().trim());
                totaltaxamt += parseFloat($(this).find('td:eq(29)').html().trim());
                totalbasicplustaxamt += parseFloat($(this).find('td:eq(30)').html().trim());
                totalssdiscountamt += parseFloat($(this).find('td:eq(16)').html().trim());
                totalschemeamt += parseFloat($(this).find('td:eq(18)').html().trim());
                totaldiscountamt += parseFloat($(this).find('td:eq(20)').html().trim());
                totalcaseqty += parseFloat($(this).find('td:eq(10)').html().trim());
                totalBillpcsqty += parseFloat($(this).find('td:eq(11)').html().trim());
                totalpcsqty += parseInt(getCaseToPcsConversion($(this).find('td:eq(1)').html().trim(), '1970C78A-D062-4FE9-85C2-3E12490463AF', 'B9F29D12-DE94-40F1-A668-C79BF1BF4425', $(this).find('td:eq(10)').html().trim(), $(this).find('td:eq(11)').html().trim()));
            });

            $('tfoot th#tfCase').html(parseFloat(totalcaseqty).toFixed(0));
            $('tfoot th#tfPcs').html(parseFloat(totalBillpcsqty).toFixed(0));
            $('tfoot th#tfAddSSDisc').html(parseFloat(totalssdiscountamt).toFixed(2));
            $('tfoot th#tfSchemeAmt').html(parseFloat(totalschemeamt).toFixed(2));
            $('tfoot th#tfDiscAmt').html(parseFloat(totaldiscountamt).toFixed(2));
            $('tfoot th#tfBasicAmt').html(parseFloat(totalbasicamt).toFixed(2));
            $('tfoot th#tfIGST').html(parseFloat(totaltaxamt).toFixed(2));
            $('tfoot th#tfNetAmt').html(parseFloat(totalbasicplustaxamt).toFixed(2));
        }
        else if ($("#hdntaxcount").val() == '2') {
            $('#productDetailsGrid tbody tr').each(function () {
                totalbasicamt += parseFloat($(this).find('td:eq(21)').html().trim());
                totaltaxamt += parseFloat($(this).find('td:eq(29)').html().trim()) + parseFloat($(this).find('td:eq(31)').html().trim());
                totalbasicplustaxamt += parseFloat($(this).find('td:eq(32)').html().trim());
                totalssdiscountamt += parseFloat($(this).find('td:eq(16)').html().trim());
                totalschemeamt += parseFloat($(this).find('td:eq(18)').html().trim());
                totaldiscountamt += parseFloat($(this).find('td:eq(20)').html().trim());
                totalcaseqty += parseFloat($(this).find('td:eq(10)').html().trim());
                totalBillpcsqty += parseFloat($(this).find('td:eq(11)').html().trim());
                totalpcsqty += parseInt(getCaseToPcsConversion($(this).find('td:eq(1)').html().trim(), '1970C78A-D062-4FE9-85C2-3E12490463AF', 'B9F29D12-DE94-40F1-A668-C79BF1BF4425', $(this).find('td:eq(10)').html().trim(), $(this).find('td:eq(11)').html().trim()));
            });

            $('tfoot th#tfCase').html(parseFloat(totalcaseqty).toFixed(0));
            $('tfoot th#tfPcs').html(parseFloat(totalBillpcsqty).toFixed(0));
            $('tfoot th#tfAddSSDisc').html(parseFloat(totalssdiscountamt).toFixed(2));
            $('tfoot th#tfSchemeAmt').html(parseFloat(totalschemeamt).toFixed(2));
            $('tfoot th#tfDiscAmt').html(parseFloat(totaldiscountamt).toFixed(2));
            $('tfoot th#tfBasicAmt').html(parseFloat(totalbasicamt).toFixed(2));
            $('tfoot th#tfCGST').html(parseFloat(totaltaxamt / 2).toFixed(2));
            $('tfoot th#tfSGST').html(parseFloat(totaltaxamt / 2).toFixed(2));
            $('tfoot th#tfNetAmt').html(parseFloat(totalbasicplustaxamt).toFixed(2));
        }


        $('#freeDetailsGrid tbody tr').each(function () {
            freetotalpcsqty += parseInt($(this).find('td:eq(11)').html().trim());
            totalfreeamt += parseFloat($(this).find('td:eq(15)').html().trim());
        });

        $('tfoot th#tfFreeQty').html(parseFloat(freetotalpcsqty).toFixed(0));
        $('tfoot th#tfFreeAmt').html(parseFloat(totalfreeamt).toFixed(2));
        $('tfoot th#tfFreeNetAmt').html(parseFloat(totalfreeamt).toFixed(2));

        totalFinalpcsqty = parseInt(freetotalpcsqty + totalBillpcsqty);


        $('#BasicAmt').val(totalbasicamt.toFixed(2));
        $('#TaxAmt').val(totaltaxamt.toFixed(2));
        $('#GrossAmt1').val(totalbasicplustaxamt.toFixed(2));
        $('#TotalSchemeAmt').val(totalschemeamt.toFixed(2));
        $('#TotalDiscountAmt').val(totaldiscountamt.toFixed(2));
        $('#TotalFreeAmt').val(totalfreeamt.toFixed(2));
        $('#SSMarginAmt').val(totalssdiscountamt.toFixed(2));
        $('#TotalCase').val(totalcaseqty + '.' + totalFinalpcsqty);
        $('#TotalPcs').val(totalpcsqty + freetotalpcsqty);


        parts = totalbasicplustaxamt - Math.floor(totalbasicplustaxamt);
        decimalvalue = parts.toFixed(2);
        if (decimalvalue >= .50) {
            decimalvalue = 1 - decimalvalue;
            decimalvalue = decimalvalue.toFixed(2);

        }
        else {
            decimalvalue = -decimalvalue;
        }
        finalamt = Math.round(totalbasicplustaxamt);

        /*Round Up Logic start from 28-08-2020 commented on 08-10-2020 as per Business*/
        //if (decimalvalue > 0) {
        //    decimalvalue = 1 - decimalvalue;
        //    decimalvalue = decimalvalue.toFixed(2);
        //}
        //finalamt = Math.ceil(totalbasicplustaxamt);
        /*Round Up Logic end*/
        $("#NetAmt").val(finalamt.toFixed(2));
        $("#NetAmt2").val(finalamt.toFixed(2));
        $("#RoundOff").val(decimalvalue);
    }
    else if (rowCount > 0 && freerowCount <= 0) {
        if ($("#hdntaxcount").val() == '1') {
            $('#productDetailsGrid tbody tr').each(function () {

                totalbasicamt += parseFloat($(this).find('td:eq(21)').html().trim());
                totaltaxamt += parseFloat($(this).find('td:eq(29)').html().trim());
                totalbasicplustaxamt += parseFloat($(this).find('td:eq(30)').html().trim());
                totalssdiscountamt += parseFloat($(this).find('td:eq(16)').html().trim());
                totalschemeamt += parseFloat($(this).find('td:eq(18)').html().trim());
                totaldiscountamt += parseFloat($(this).find('td:eq(20)').html().trim());
                totalcaseqty += parseFloat($(this).find('td:eq(10)').html().trim());
                totalBillpcsqty += parseFloat($(this).find('td:eq(11)').html().trim());
                totalpcsqty += parseInt(getCaseToPcsConversion($(this).find('td:eq(1)').html().trim(), '1970C78A-D062-4FE9-85C2-3E12490463AF', 'B9F29D12-DE94-40F1-A668-C79BF1BF4425', $(this).find('td:eq(10)').html().trim(), $(this).find('td:eq(11)').html().trim()));
            });

            $('tfoot th#tfCase').html(parseFloat(totalcaseqty).toFixed(0));
            $('tfoot th#tfPcs').html(parseFloat(totalBillpcsqty).toFixed(0));
            $('tfoot th#tfAddSSDisc').html(parseFloat(totalssdiscountamt).toFixed(2));
            $('tfoot th#tfSchemeAmt').html(parseFloat(totalschemeamt).toFixed(2));
            $('tfoot th#tfDiscAmt').html(parseFloat(totaldiscountamt).toFixed(2));
            $('tfoot th#tfBasicAmt').html(parseFloat(totalbasicamt).toFixed(2));
            $('tfoot th#tfIGST').html(parseFloat(totaltaxamt).toFixed(2));
            $('tfoot th#tfNetAmt').html(parseFloat(totalbasicplustaxamt).toFixed(2));
        }
        else if ($("#hdntaxcount").val() == '2') {
            $('#productDetailsGrid tbody tr').each(function () {
                totalbasicamt += parseFloat($(this).find('td:eq(21)').html().trim());
                totaltaxamt += parseFloat($(this).find('td:eq(29)').html().trim()) + parseFloat($(this).find('td:eq(31)').html().trim());
                totalbasicplustaxamt += parseFloat($(this).find('td:eq(32)').html().trim());
                totalssdiscountamt += parseFloat($(this).find('td:eq(16)').html().trim());
                totalschemeamt += parseFloat($(this).find('td:eq(18)').html().trim());
                totaldiscountamt += parseFloat($(this).find('td:eq(20)').html().trim());
                totalcaseqty += parseFloat($(this).find('td:eq(10)').html().trim());
                totalBillpcsqty += parseFloat($(this).find('td:eq(11)').html().trim());
                totalpcsqty += parseInt(getCaseToPcsConversion($(this).find('td:eq(1)').html().trim(), '1970C78A-D062-4FE9-85C2-3E12490463AF', 'B9F29D12-DE94-40F1-A668-C79BF1BF4425', $(this).find('td:eq(10)').html().trim(), $(this).find('td:eq(11)').html().trim()));
            });

            $('tfoot th#tfCase').html(parseFloat(totalcaseqty).toFixed(0));
            $('tfoot th#tfPcs').html(parseFloat(totalBillpcsqty).toFixed(0));
            $('tfoot th#tfAddSSDisc').html(parseFloat(totalssdiscountamt).toFixed(2));
            $('tfoot th#tfSchemeAmt').html(parseFloat(totalschemeamt).toFixed(2));
            $('tfoot th#tfDiscAmt').html(parseFloat(totaldiscountamt).toFixed(2));
            $('tfoot th#tfBasicAmt').html(parseFloat(totalbasicamt).toFixed(2));
            $('tfoot th#tfCGST').html(parseFloat(totaltaxamt / 2).toFixed(2));
            $('tfoot th#tfSGST').html(parseFloat(totaltaxamt / 2).toFixed(2));
            $('tfoot th#tfNetAmt').html(parseFloat(totalbasicplustaxamt).toFixed(2));
        }

        totalFinalpcsqty = parseInt(freetotalpcsqty + totalBillpcsqty);

        $('#BasicAmt').val(totalbasicamt.toFixed(2));
        $('#TaxAmt').val(totaltaxamt.toFixed(2));
        $('#GrossAmt1').val(totalbasicplustaxamt.toFixed(2));
        $('#TotalSchemeAmt').val(totalschemeamt.toFixed(2));
        $('#TotalDiscountAmt').val(totaldiscountamt.toFixed(2));
        $('#TotalFreeAmt').val('0.00');
        $('#SSMarginAmt').val(totalssdiscountamt.toFixed(2));
        $('#TotalCase').val(totalcaseqty + '.' + totalFinalpcsqty);
        $('#TotalPcs').val(totalpcsqty);


        parts = totalbasicplustaxamt - Math.floor(totalbasicplustaxamt);
        decimalvalue = parts.toFixed(2);
        if (decimalvalue >= .50) {
            decimalvalue = 1 - decimalvalue;
            decimalvalue = decimalvalue.toFixed(2);

        }
        else {
            decimalvalue = -decimalvalue;
        }
        finalamt = Math.round(totalbasicplustaxamt);
        /*Round Up Logic start from 28-08-2020 commented on 08-10-2020 as per Business*/
        //if (decimalvalue > 0) {
        //    decimalvalue = 1 - decimalvalue;
        //    decimalvalue = decimalvalue.toFixed(2);
        //}
        //finalamt = Math.ceil(totalbasicplustaxamt);
        /*Round Up Logic end*/
        $("#NetAmt").val(finalamt.toFixed(2));
        $("#NetAmt2").val(finalamt.toFixed(2));
        $("#RoundOff").val(decimalvalue);
    }
    else if (rowCount <= 0 && freerowCount > 0) {

        $('#freeDetailsGrid tbody tr').each(function () {
            freetotalpcsqty += parseInt($(this).find('td:eq(11)').html().trim());
            totalfreeamt += parseFloat($(this).find('td:eq(15)').html().trim());
        });

        $('tfoot th#tfFreeQty').html(parseFloat(freetotalpcsqty).toFixed(0));
        $('tfoot th#tfFreeAmt').html(parseFloat(totalfreeamt).toFixed(2));
        $('tfoot th#tfFreeNetAmt').html(parseFloat(totalfreeamt).toFixed(2));

        totalFinalpcsqty = parseInt(freetotalpcsqty + totalBillpcsqty);

        $('#BasicAmt').val('0.00');
        $('#TaxAmt').val('0.00');
        $('#GrossAmt1').val('0.00');
        $('#TotalSchemeAmt').val('0.00');
        $('#TotalDiscountAmt').val('0.00');
        $('#TotalFreeAmt').val(totalfreeamt.toFixed(2));
        $('#SSMarginAmt').val('0.00');
        $('#TotalCase').val(totalcaseqty + '.' + totalFinalpcsqty);
        $('#TotalPcs').val(freetotalpcsqty);
        $("#NetAmt").val('0.00');
        $('#NetAmt2').val('0.00');
        $("#RoundOff").val('0.00');
    }
    else if (rowCount <= 0 && freerowCount <= 0) {
        $('#BasicAmt').val('0.00');
        $('#TaxAmt').val('0.00');
        $('#GrossAmt1').val('0.00');
        $('#TotalSchemeAmt').val('0.00');
        $('#TotalDiscountAmt').val('0.00');
        $('#TotalFreeAmt').val('0.00');
        $('#SSMarginAmt').val('0.00');
        $('#NetAmt').val('0.00');
        $('#NetAmt2').val('0.00');
        $('#RoundOff').val('0.00');
        $('#TotalCase').val('0.000');
        $('#TotalPcs').val('0');
    }

    if (parseFloat(Balance) >= parseFloat($("#NetAmt2").val())) {

        $("#NetAmt2").css("background-color", "LightGreen");
    }
    else {
        $("#NetAmt2").css("background-color", "LightCoral");
    }
}

function ClearControls() {
    $("#dvAdd").find("input, textarea, select,submit").removeAttr("disabled");
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
    $('#divTransferNo').css("display", "none");
    $("#btnadd").prop("disabled", false);
    $("#TransferNo").attr("disabled", "disabled");
    $("#GatepassDate").attr("disabled", "disabled");
    $("#InvoiceDate").attr("disabled", "disabled");
    $("#LrGrDate").attr("disabled", "disabled");
    $("#ICDSDate").attr("disabled", "disabled");
    $("#DeliveryDate").attr("disabled", "disabled");
    $("#MRP").attr("disabled", "disabled");
    $("#StockQty").attr("disabled", "disabled");
    $("#Rate").attr("disabled", "disabled");
    $("#OrderQty").attr("disabled", "disabled");
    $("#OrderDate").attr("disabled", "disabled");
    $("#DeliveredQty").attr("disabled", "disabled");
    $("#RemainingQty").attr("disabled", "disabled");
    $("#BasicAmt").attr("disabled", "disabled");
    $("#TaxAmt").attr("disabled", "disabled");
    $("#GrossAmt1").attr("disabled", "disabled");
    $("#GrossAmt").attr("disabled", "disabled");
    $("#RoundOff").attr("disabled", "disabled");
    $("#NetAmt").attr("disabled", "disabled");
    $("#NetAmt2").attr("disabled", "disabled");
    $("#NetAmt2").css("background-color", "LightGreen");
    $("#TotalPcs").attr("disabled", "disabled");
    $("#TotalSchemeAmt").attr("disabled", "disabled");
    $("#TotalDiscountAmt").attr("disabled", "disabled");
    $("#TotalFreeAmt").attr("disabled", "disabled");
    $("#SSMargin").attr("disabled", "disabled");
    $("#SSMarginAmt").attr("disabled", "disabled");

    $("#ClosingBalance").attr("disabled", "disabled");
    $("#CreditLimit").attr("disabled", "disabled");
    $("#Target").attr("disabled", "disabled");
    $("#Balance").attr("disabled", "disabled");
    $("#AchPercentage").attr("disabled", "disabled");
    $("#InvoiceLimit").attr("disabled", "disabled");
    $("#InvoiceDone").attr("disabled", "disabled");
    $("#InvoiceBalance").attr("disabled", "disabled");

    $("#AchPercentage").css("background-color", "");
    $("#InvoiceBalance").css("background-color", "");

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
    
    $("#CUSTOMERID").val('0');
    $("#CUSTOMERID").prop("disabled", false);
    $("#CUSTOMERID").chosen({
        search_contains: true
    });
    $("#CUSTOMERID").trigger("chosen:updated");

    $("#SaleOrderID").prop("disabled", false);
    $("#SaleOrderID").empty();
    $("#SaleOrderID").chosen({
        search_contains: true
    });
    $("#SaleOrderID").trigger("chosen:updated");



    $("#PaymentMode").chosen({
        search_contains: true
    });
    $("#PaymentMode").trigger("chosen:updated");

    $("#Tranportmode").chosen({
        search_contains: true
    });
    $("#Tranportmode").trigger("chosen:updated");

    $("#TransporterID").val('0');
    $("#TransporterID").chosen({
        search_contains: true
    });
    $("#TransporterID").trigger("chosen:updated");

    $("#VehichleNo").val('');
    $("#LrGrNo").val('');
    $("#GatepassNo").val('');

    $('#CATID').empty();
    $("#CATID").chosen({
        search_contains: true
    });
    $("#CATID").trigger("chosen:updated");

    $('#PRODUCTID').empty();
    $("#PRODUCTID").chosen({
        search_contains: true
    });
    $("#PRODUCTID").trigger("chosen:updated");

    $("#PSID").chosen({
        search_contains: true
    });
    $("#PSID").trigger("chosen:updated");

    $("#FREEPRODUCTID").chosen({
        search_contains: true
    });
    $("#FREEPRODUCTID").trigger("chosen:updated");

    $('#ddlBatch').empty();
    $('#InvoiceQty').val('');
    $('#InvoicePcs').val('0');
    $('#StockQty').val('');
    $('#MRP').val('');
    $('#Rate').val('');
    $('#OrderQty').val('');
    $('#DeliveredQty').val('');
    $('#RemainingQty').val('');
    $('#OrderDate').val('');
    $('#productDetailsGrid').empty();
    $('#freeDetailsGrid').empty();
    $('#freeProductDetailsGrid').empty();
    $('#finalfreeDetailsGrid').empty();
    $('#comboBreakupGrid').empty();
    $('#taxDetailsGrid').empty();
    $('#BasicAmt').val('');
    $('#TaxAmt').val('');
    $('#GrossAmt1').val('');
    $('#GrossAmt').val('');
    $('#RoundOff').val('');
    $('#NetAmt').val('');
    $('#NetAmt2').val('');
    $('#TotalCase').val('');
    $('#TotalPcs').val('');
    $('#Remarks').val('');
    $('#ShippingAddress').val('');
    $('#ICDSNo').val('');
    $('#ICDSDate').val('');

    $('#TotalSchemeAmt').val('');
    $('#TotalDiscountAmt').val('');
    $('#TotalFreeAmt').val('');
    $('#TotalGrossWght').val('');
    $('#SSMarginAmt').val('');

    $('#ClosingBalance').val('');
    $('#CreditLimit').val('');
    $('#ReferencePO').val('');
    $('#Target').val('');
    $('#Balance').val('');
    $('#AchPercentage').val('');
    $('#InvoiceLimit').val('');
    $('#InvoiceDone').val('');
    $('#InvoiceBalance').val('');
    $('#spnTGT').text('');
    $('#spnInvLimit').text('');
    $('#hdndispatchID').val('0');
    $("#hdntaxcount").val('');

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
    $('#hdnGroupID').val('');
    $("#OrderDate").val('');
    $("#OrderQty").val('');
    $("#StockQty").val('');
    $("#DeliveredQty").val('');
    $("#RemainingQty").val('');
    //finyearChecking();

}

function clearafterCustomerSelection() {
    $('#ClosingBalance').val('');
    $('#CreditLimit').val('');
    $('#ReferencePO').val('');
    $('#Target').val('');
    $('#Balance').val('');
    $('#AchPercentage').val('');
    $('#InvoiceLimit').val('');
    $('#InvoiceDone').val('');
    $('#InvoiceBalance').val('');
    $('#spnTGT').text('');
    $('#spnInvLimit').text('');
    $('#ddlBatch').empty();
    $('#InvoiceQty').val('');
    $('#InvoicePcs').val('0');
    $('#StockQty').val('');
    $('#MRP').val('');
    $('#Rate').val('');
    $('#OrderQty').val('');
    $('#OrderDate').val('');
    $('#DeliveredQty').val('');
    $('#RemainingQty').val('');
    $('#BasicAmt').val('');
    $('#TaxAmt').val('');
    $('#GrossAmt1').val('');
    $('#GrossAmt').val('');
    $('#RoundOff').val('');
    $('#NetAmt').val('');
    $('#NetAmt2').val('');
    $("#NetAmt2").css("background-color", "LightGreen");
    $('#TotalCase').val('');
    $('#TotalPcs').val('');
    $('#TotalSchemeAmt').val('');
    $('#TotalDiscountAmt').val('');
    $('#TotalFreeAmt').val('');
    $('#TotalGrossWght').val('');
    $('#SSMarginAmt').val('');
    $('#hdndispatchID').val('0');
    $('#hdnGroupID').val('');
    $("#hdntaxcount").val('');
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
    $('#productDetailsGrid').empty();
    $('#freeDetailsGrid').empty();
    $('#freeProductDetailsGrid').empty();
    $('#finalfreeDetailsGrid').empty();
    $('#comboBreakupGrid').empty();
    $('#taxDetailsGrid').empty();
}

function clearafterAdd() {
    $('#InvoiceQty').val('');
    $('#InvoicePcs').val('0');
    $('#StockQty').val('');
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
                ////debugger;
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

function availableStock(depotid, productid, batch, mrp, mfgdate, expdate, storelocationid) {
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
                //debugger;
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

function GetFullCasepack(productid, qty) {
    //debugger;
    var returnvalue = null;
    $.ajax({
        type: "POST",
        url: "/TranDepot/FullCasePack",
        data: { ProductID: productid, Qty: qty},
        dataType: "json",
        async: false,
        success: function (packsize) {
            var packsize;
            $.each(packsize, function (key, item) {
                ////debugger;
                packsize = item.PACKSIZE;
                returnvalue = packsize;
                return false;
            });
        },
        failure: function (packsize) {
            alert(packsize.responseText);
        },
        error: function (packsize) {
            alert(packsize.responseText);
        }
    });
    return returnvalue;
}

function SaveInvoice() {
    //debugger;

    var rowCount = 0;
    var freerowCount = 0;
    var comborowCount = 0;
    var returnflag = true;
    var Taxreturnflag = true;
    var grpstatus = '';
    var saveflag = false;
    var returnpacksize = 0;
    var returnresult = 0;
    rowCount = document.getElementById("productDetailsGrid").rows.length - 2;
    freerowCount = document.getElementById("freeDetailsGrid").rows.length - 2;
    comborowCount = document.getElementById("comboBreakupGrid").rows.length - 1;

    /* Tax Checking Start*/
    if ($("#hdntaxcount").val() == '1') {
        $('#productDetailsGrid tbody tr').each(function () {
            var lineItemigstTax = 0;
            var TaxProduct = $(this).find('td:eq(2)').html().trim();
            var TaxBatch = $(this).find('td:eq(12)').html().trim();
            //lineItemigstTax = parseFloat($(this).find('td:eq(29)').html().trim());
            //if (parseFloat(lineItemigstTax) <= 0) {
            //    toastr.error('Tax amount should not be 0 for <b>' + TaxProduct + '</b> in batch - <b>' + TaxBatch + '</b>');
            //    Taxreturnflag = false;
            //    return false;
            //}
            if (isNaN($(this).find('td:eq(28)').html().trim()) == true) {
                toastr.error('Tax amount should not be 0 or NaN for <b>' + TaxProduct + '</b> in batch - <b>' + TaxBatch + '</b>');
                Taxreturnflag = false;
                return false;
            }
        });
    }
    else if ($("#hdntaxcount").val() == '2') {
        $('#productDetailsGrid tbody tr').each(function () {
            var lineItemcgstTax = 0;
            var lineItemsgstTax = 0;
            var TaxProduct = $(this).find('td:eq(2)').html().trim();
            var TaxBatch = $(this).find('td:eq(12)').html().trim();
            //lineItemcgstTax = parseFloat($(this).find('td:eq(29)').html().trim());
            //lineItemsgstTax = parseFloat($(this).find('td:eq(31)').html().trim());
            //if (parseFloat(lineItemcgstTax) <= 0 || parseFloat(lineItemsgstTax) <= 0) {
            //    toastr.error('Tax amount should not be 0 for <b>' + TaxProduct + '</b> in batch - <b>' + TaxBatch + '</b>');
            //    Taxreturnflag = false;
            //    return false;
            //}
            if (isNaN($(this).find('td:eq(28)').html().trim()) == true || isNaN($(this).find('td:eq(30)').html().trim()) == true) {
                toastr.error('Tax amount should not be 0 or NaN for <b>' + TaxProduct + '</b> in batch - <b>' + TaxBatch + '</b>');
                Taxreturnflag = false;
                return false;
            }
        });
    }
    /* Tax Checking End*/

    /*Full Case-pack Checking Start*/
    if (rowCount > 0) {
        $('#productDetailsGrid tbody tr').each(function () {
            //debugger;
            var invProductid = $(this).find('td:eq(1)').html().trim();
            var invProduct = $(this).find('td:eq(2)').html().trim();
            var invCase = $(this).find('td:eq(10)').html().trim();
            var invPcs = $(this).find('td:eq(11)').html().trim();
            var InvCaseID = '1970C78A-D062-4FE9-85C2-3E12490463AF';
            var InvQty = TaxGridDeliveryQty(invProductid.trim(), InvCaseID.trim(), invCase.trim(), invPcs.trim(), '0', '0', $('#hdndispatchID').val().trim());
            returnpacksize = GetFullCasepack(invProductid, InvQty);
            returnresult = returnpacksize % 1;
            if (returnresult != 0) {
                toastr.error('Loose quantity is not allowed for <b>' + invProduct + '</br><b> Only multiple of case pack is allowed. </b>');
                returnflag = false;
                return false;
            }
        });
    }
    /*Full Case-pack Checking End*/

    /* Stock Checking Start*/
    if (rowCount > 0) {
        /*Billing Grid Loop Start*/
        $('#productDetailsGrid tbody tr').each(function () {
            //debugger;
            var invoiceProductid = $(this).find('td:eq(1)').html().trim();
            var invoiceProduct = $(this).find('td:eq(2)').html().trim();
            var invoiceBatch = $(this).find('td:eq(12)').html().trim();
            var invoiceMfgDate = $(this).find('td:eq(23)').html().trim();
            var invoiceExpDate = $(this).find('td:eq(24)').html().trim();
            var invoiceMrp = $(this).find('td:eq(6)').html().trim();
            var invoiceCase = $(this).find('td:eq(10)').html().trim();
            var invoicePcs = $(this).find('td:eq(11)').html().trim();
            var InvoiceCaseID = '1970C78A-D062-4FE9-85C2-3E12490463AF';
            var OperationFlag = $(this).find('td:eq(27)').html().trim();
            //debugger;
            var BillQty = TaxGridDeliveryQty(invoiceProductid.trim(), InvoiceCaseID.trim(), invoiceCase.trim(), invoicePcs.trim(), '0', '0', $('#hdndispatchID').val().trim());
            var FinalBillQty1 = 0;
            var FinalBillQty2 = 0;
            var FinalBillQty3 = 0;
            var FinalBillQty4 = 0;
            var Flag1 = '0';
            var Flag2 = '0';
            var Flag3 = '0';
            var Flag4 = '0';
            var finalfreePcs = 0;
            if (freerowCount > 0) {
                /*Free Grid Loop Start*/
                $('#freeDetailsGrid tbody tr').each(function () {
                    //debugger;
                    var freeProductID = $(this).find('td:eq(6)').html().trim();
                    var freeBATCHNO = $(this).find('td:eq(16)').html().trim();
                    var freeMFGDATE = $(this).find('td:eq(19)').html().trim();
                    var freeEXPRDATE = $(this).find('td:eq(20)').html().trim();
                    var freeMRP = $(this).find('td:eq(12)').html().trim();
                    var freePCS = $(this).find('td:eq(11)').html().trim();

                    if (invoiceProductid == freeProductID) {
                        //debugger;
                        if (invoiceBatch == freeBATCHNO && invoiceMfgDate == freeMFGDATE
                            && invoiceExpDate == freeEXPRDATE && invoiceMrp == freeMRP) {

                            //debugger;
                            finalfreePcs += parseFloat(freePCS);
                            FinalBillQty1 = parseFloat(BillQty) + parseFloat(finalfreePcs);
                            Flag1 = '1';
                            //return false;
                        }
                        else {
                            //debugger;
                            FinalBillQty2 = BillQty;
                            Flag2 = '2';
                            //return false;
                        }
                    }
                    else {
                        //debugger;
                        FinalBillQty3 = BillQty;
                        Flag3 = '3';
                        //return false;
                    }
                });
                /*Free Grid Loop End*/
            }
            else {
                FinalBillQty4 = BillQty;
                Flag4 = '4';
            }
            var AvailableStockQty = 0;
            if ($('#hdndispatchID').val().trim() == '0') {

                if (Flag1 == '1') {
                    //debugger;
                    AvailableStockQty = availableStock($('#BRID').val().trim(), invoiceProductid.trim(), invoiceBatch.trim(),
                        invoiceMrp.trim(), invoiceMfgDate.trim(), invoiceExpDate.trim(), '113BD8D6-E5DC-4164-BEE7-02A16F97ABCC');
                }
                else if (Flag2 == '2') {
                    //debugger;
                    AvailableStockQty = availableStock($('#BRID').val().trim(), invoiceProductid.trim(), invoiceBatch.trim(),
                        invoiceMrp.trim(), invoiceMfgDate.trim(), invoiceExpDate.trim(), '113BD8D6-E5DC-4164-BEE7-02A16F97ABCC');
                }
                else if (Flag3 == '3') {
                    //debugger;
                    AvailableStockQty = availableStock($('#BRID').val().trim(), invoiceProductid.trim(), invoiceBatch.trim(),
                        invoiceMrp.trim(), invoiceMfgDate.trim(), invoiceExpDate.trim(), '113BD8D6-E5DC-4164-BEE7-02A16F97ABCC');
                }
                else if (Flag4 == '4') {
                    //debugger;
                    AvailableStockQty = availableStock($('#BRID').val().trim(), invoiceProductid.trim(), invoiceBatch.trim(),
                        invoiceMrp.trim(), invoiceMfgDate.trim(), invoiceExpDate.trim(), '113BD8D6-E5DC-4164-BEE7-02A16F97ABCC');
                }
            }
            else if ($('#hdndispatchID').val().trim() != '0') {

                if (Flag1 == '1') {

                    AvailableStockQty = availableStock($('#BRID').val().trim(), invoiceProductid.trim(), invoiceBatch.trim(),
                        invoiceMrp.trim(), invoiceMfgDate.trim(), invoiceExpDate.trim(), '113BD8D6-E5DC-4164-BEE7-02A16F97ABCC');
                    if (OperationFlag == '1') {
                        AvailableStockQty = parseFloat(AvailableStockQty) + parseFloat(FinalBillQty1);
                    }
                }
                else if (Flag2 == '2') {

                    AvailableStockQty = availableStock($('#BRID').val().trim(), invoiceProductid.trim(), invoiceBatch.trim(),
                        invoiceMrp.trim(), invoiceMfgDate.trim(), invoiceExpDate.trim(), '113BD8D6-E5DC-4164-BEE7-02A16F97ABCC');
                    if (OperationFlag == '1') {
                        AvailableStockQty = parseFloat(AvailableStockQty) + parseFloat(FinalBillQty2);
                    }
                }
                else if (Flag3 == '3') {

                    AvailableStockQty = availableStock($('#BRID').val().trim(), invoiceProductid.trim(), invoiceBatch.trim(),
                        invoiceMrp.trim(), invoiceMfgDate.trim(), invoiceExpDate.trim(), '113BD8D6-E5DC-4164-BEE7-02A16F97ABCC');
                    if (OperationFlag == '1') {
                        AvailableStockQty = parseFloat(AvailableStockQty) + parseFloat(FinalBillQty3);
                    }
                }
                else if (Flag4 == '4') {

                    AvailableStockQty = availableStock($('#BRID').val().trim(), invoiceProductid.trim(), invoiceBatch.trim(),
                        invoiceMrp.trim(), invoiceMfgDate.trim(), invoiceExpDate.trim(), '113BD8D6-E5DC-4164-BEE7-02A16F97ABCC');
                    if (OperationFlag == '1') {
                        AvailableStockQty = parseFloat(AvailableStockQty) + parseFloat(FinalBillQty4);
                    }
                }
            }
            //debugger;
            if (parseFloat(FinalBillQty1) > parseFloat(AvailableStockQty)) {
                toastr.error('Stock not available for <b>' + invoiceProduct + '</b> in batch - <b>' + invoiceBatch + '</b>');
                returnflag = false;
                return false;
            }
            else if (parseFloat(FinalBillQty2) > parseFloat(AvailableStockQty)) {
                toastr.error('Stock not available for <b>' + invoiceProduct + '</b> in batch - <b>' + invoiceBatch + '</b>');
                returnflag = false;
                return false;
            }
            else if (parseFloat(FinalBillQty3) > parseFloat(AvailableStockQty)) {
                toastr.error('Stock not available for <b>' + invoiceProduct + '</b> in batch - <b>' + invoiceBatch + '</b>');
                returnflag = false;
                return false;
            }
            else if (parseFloat(FinalBillQty4) > parseFloat(AvailableStockQty)) {
                toastr.error('Stock not available for <b>' + invoiceProduct + '</b> in batch - <b>' + invoiceBatch + '</b>');
                returnflag = false;
                return false;
            }
        });
        /*Billing Grid Loop End*/
    }
    /* Stock Checking End*/

    if (returnflag == true && Taxreturnflag == true) {
        saveflag = true;
        if (saveflag == true) {

            $("#dialog").dialog({
                autoOpen: true,
                modal: true,
                title: "Loading.."
            });
            $("#imgLoader").css("visibility", "visible");
            setTimeout(function () {

                var i = 0;
                var j = 0;
                var k = 0;
                var l = 0;
                var Month = $('#InvoiceDate').val().trim();
                Month = Month.substr(3, 2);

                invoicelist = new Array();
                freelist = new Array();
                combolist = new Array();
                taxlist = new Array();
                var csdinvoicesave = {};

                //debugger;
                csdinvoicesave.FGInvoiceID = $('#hdndispatchID').val().trim();

                if ($('#hdndispatchID').val() == '0') {
                    csdinvoicesave.FLAG = 'A';
                }
                else {
                    csdinvoicesave.FLAG = 'U';
                }
                csdinvoicesave.InvoiceDate = $("#InvoiceDate").val().trim();
                csdinvoicesave.CUSTOMERID = $("#CUSTOMERID").val().trim();
                csdinvoicesave.CUSTOMERNAME = $("#CUSTOMERID option:selected").text().trim();
                csdinvoicesave.SaleOrderID = $("#SaleOrderID").val().trim();
                if ($("#ShippingAddress").val().trim() == '') {
                    csdinvoicesave.ShippingAddress = '';
                }
                else {
                    csdinvoicesave.ShippingAddress = $("#ShippingAddress").val().trim();
                }
                csdinvoicesave.WAYBILLNO = '0';
                csdinvoicesave.TransporterID = $("#TransporterID").val().trim();
                if ($("#VehichleNo").val().trim() == '') {
                    csdinvoicesave.VehichleNo = '';
                }
                else {
                    csdinvoicesave.VehichleNo = $("#VehichleNo").val().trim();
                }
                csdinvoicesave.BRID = $("#BRID").val().trim();
                csdinvoicesave.BRNAME = $("#BRID option:selected").text().trim();
                if ($("#LrGrNo").val().trim() == '') {
                    csdinvoicesave.LrGrNo = '';
                }
                else {
                    csdinvoicesave.LrGrNo = $("#LrGrNo").val().trim();
                }
                csdinvoicesave.LrGrDate = $("#LrGrDate").val().trim();
                if ($("#ICDSNo").val().trim() == '') {
                    csdinvoicesave.ICDSNo = '';
                }
                else {
                    csdinvoicesave.ICDSNo = $("#ICDSNo").val().trim();
                }
                csdinvoicesave.ICDSDate = $("#ICDSDate").val().trim();
                if ($("#GatepassNo").val().trim() == '') {
                    csdinvoicesave.GatepassNo = '';
                }
                else {
                    csdinvoicesave.GatepassNo = $("#GatepassNo").val().trim();
                }
                csdinvoicesave.GatepassDate = $("#GatepassDate").val().trim();
                csdinvoicesave.PaymentMode = $("#PaymentMode").val().trim();
                csdinvoicesave.Tranportmode = $("#Tranportmode").val().trim();
                if ($("#Remarks").val().trim() == '') {
                    csdinvoicesave.Remarks = '';
                }
                else {
                    csdinvoicesave.Remarks = $("#Remarks").val().trim();
                }
                csdinvoicesave.NetAmt = $("#NetAmt").val().trim();
                csdinvoicesave.RoundOff = $("#RoundOff").val().trim();
                csdinvoicesave.TotalFreeAmt = $("#TotalFreeAmt").val().trim();
                csdinvoicesave.GroupID = $("#hdnGroupID").val().trim();
                csdinvoicesave.TotalPcs = $("#TotalPcs").val().trim();
                csdinvoicesave.TotalCase = $("#TotalCase").val().trim();
                if ($('#hdntaxcount').val() == '1') {
                    csdinvoicesave.InvoiceType = '1';
                }
                else if ($('#hdntaxcount').val() == '2') {
                    csdinvoicesave.InvoiceType = '2';
                }
                else if ($('#hdntaxcount').val() == '3') {
                    csdinvoicesave.InvoiceType = '3';
                }
                else {
                    csdinvoicesave.InvoiceType = '0';
                }
                if ($("#TotalGrossWght").val().trim() == '') {
                    csdinvoicesave.TotalGrossWght = '0';
                }
                else {
                    csdinvoicesave.TotalGrossWght = $("#TotalGrossWght").val().trim();
                }
                if ($("#ReferencePO").val() == '') {
                    csdinvoicesave.ReferencePO = '';
                }
                else {
                    csdinvoicesave.ReferencePO = $("#ReferencePO").val().trim();
                }

                csdinvoicesave.BasicAmt = $("#BasicAmt").val().trim();
                csdinvoicesave.MonthID = Month.trim();

                csdinvoicesave.UserID = $("#hdnUserID").val().trim();
                csdinvoicesave.FinYear = $("#hdnFinYear").val().trim();
                if (freerowCount <= 0) {
                    csdinvoicesave.GTFreeTag = '0';
                }
                else {
                    csdinvoicesave.GTFreeTag = '1';
                }
                if (comborowCount <= 0) {
                    csdinvoicesave.CSDComboTag = '0';
                }
                else {
                    csdinvoicesave.CSDComboTag = '1';
                }

                $('#productDetailsGrid tbody tr').each(function () {

                    //debugger
                    var invoicedetails = {};

                    var productid = $(this).find('td:eq(1)').html().trim();
                    var productname = $(this).find('td:eq(2)').html().trim();
                    var packingsizeid = $(this).find('td:eq(4)').html().trim();
                    var packingsizename = $(this).find('td:eq(5)').html().trim();
                    var mrp = $(this).find('td:eq(6)').html().trim();
                    var qtycase = $(this).find('td:eq(10)').html().trim();
                    var qtypcs = $(this).find('td:eq(11)').html().trim();
                    var rate = $(this).find('td:eq(8)').html().trim();
                    var batchno = $(this).find('td:eq(12)').html().trim();
                    var amount = $(this).find('td:eq(21)').html().trim();
                    var weight = $(this).find('td:eq(22)').html().trim();
                    var mfdate = $(this).find('td:eq(23)').html().trim();
                    var exprdate = $(this).find('td:eq(24)').html().trim();
                    var nsr = $(this).find('td:eq(7)').html().trim();
                    var ratedisc = $(this).find('td:eq(9)').html().trim();
                    var discvalue = $(this).find('td:eq(16)').html().trim();
                    var qsh = $(this).find('td:eq(26)').html().trim();
                    var qsguid = $(this).find('td:eq(25)').html().trim();
                    var discper = $(this).find('td:eq(19)').html().trim();
                    var discamt = $(this).find('td:eq(20)').html().trim();
                    var priceschemeid = $(this).find('td:eq(15)').html().trim();
                    var percentage = $(this).find('td:eq(17)').html().trim();
                    var value = $(this).find('td:eq(18)').html().trim();

                    invoicedetails.PRODUCTID = productid;
                    invoicedetails.PRODUCTNAME = productname;
                    invoicedetails.PACKINGSIZEID = packingsizeid;
                    invoicedetails.PACKINGSIZENAME = packingsizename;
                    invoicedetails.MRP = mrp;
                    invoicedetails.QTY = qtycase;
                    invoicedetails.QTYPCS = qtypcs;
                    invoicedetails.RATE = rate;
                    invoicedetails.BATCHNO = batchno;
                    invoicedetails.AMOUNT = amount;
                    invoicedetails.WEIGHT = weight;
                    invoicedetails.MFDATE = mfdate;
                    invoicedetails.EXPRDATE = exprdate;
                    invoicedetails.NSR = nsr;
                    invoicedetails.RATEDISC = ratedisc;
                    invoicedetails.DISCVALUE = discvalue;
                    invoicedetails.QSH = qsh;
                    invoicedetails.QSGUID = qsguid;
                    invoicedetails.DISCPER = discper;
                    invoicedetails.DISCAMT = discamt;
                    invoicedetails.PRICESCHEMEID = priceschemeid;
                    invoicedetails.PERCENTAGE = percentage;
                    invoicedetails.VALUE = value;
                    invoicelist[i++] = invoicedetails;
                });
                csdinvoicesave.InvoiceDetailsGT = invoicelist;

                if (freerowCount <= 0) {

                    //debugger
                    var gtfreedetails = {};

                    gtfreedetails.SCHEMEID = '0';
                    gtfreedetails.SCHEME_PRODUCT_ID = 'NA';
                    gtfreedetails.SCHEME_PRODUCT_NAME = 'NA';
                    gtfreedetails.QTY = '0';
                    gtfreedetails.PRODUCTID = 'NA';
                    gtfreedetails.PRODUCTNAME = 'NA';
                    gtfreedetails.PACKSIZEID = 'NA';
                    gtfreedetails.PACKSIZENAME = 'NA';
                    gtfreedetails.SCHEME_QTY = '0';
                    gtfreedetails.MRP = '0';
                    gtfreedetails.BRATE = '0';
                    gtfreedetails.AMOUNT = '0';
                    gtfreedetails.BATCHNO = 'NA';
                    gtfreedetails.WEIGHT = 'NA';
                    gtfreedetails.MFDATE = 'NA';
                    gtfreedetails.EXPRDATE = 'NA';
                    gtfreedetails.NSR = '0';
                    gtfreedetails.SCHEME_PRODUCT_BATCHNO = 'NA';
                    gtfreedetails.QSGUID = 'NA';
                    freelist[j++] = gtfreedetails;

                }
                else {

                    $('#freeDetailsGrid tbody tr').each(function () {

                        //debugger
                        var gtfreedetails = {};

                        var schemeid = $(this).find('td:eq(1)').html().trim();
                        var schemeproductid = $(this).find('td:eq(2)').html().trim();
                        var schemeproductname = $(this).find('td:eq(3)').html().trim();
                        var qtypcs = $(this).find('td:eq(5)').html().trim();
                        var freeproductid = $(this).find('td:eq(6)').html().trim();
                        var freeproductname = $(this).find('td:eq(7)').html().trim();
                        var freepacksizeid = $(this).find('td:eq(9)').html().trim();
                        var freepacksizename = $(this).find('td:eq(10)').html().trim();
                        var schemeqtypcs = $(this).find('td:eq(11)').html().trim();
                        var freemrp = $(this).find('td:eq(12)').html().trim();
                        var brate = $(this).find('td:eq(14)').html().trim();
                        var freeamount = $(this).find('td:eq(15)').html().trim();
                        var freebatchno = $(this).find('td:eq(16)').html().trim();
                        var freeweight = $(this).find('td:eq(21)').html().trim();
                        var freemfdate = $(this).find('td:eq(19)').html().trim();
                        var freeexprdate = $(this).find('td:eq(20)').html().trim();
                        var freensr = $(this).find('td:eq(22)').html().trim();
                        var schemebatchno = $(this).find('td:eq(4)').html().trim();
                        var freeqsguid = $(this).find('td:eq(25)').html().trim();

                        gtfreedetails.SCHEMEID = schemeid;
                        gtfreedetails.SCHEME_PRODUCT_ID = schemeproductid;
                        gtfreedetails.SCHEME_PRODUCT_NAME = schemeproductname;
                        gtfreedetails.QTY = qtypcs;
                        gtfreedetails.PRODUCTID = freeproductid;
                        gtfreedetails.PRODUCTNAME = freeproductname;
                        gtfreedetails.PACKSIZEID = freepacksizeid;
                        gtfreedetails.PACKSIZENAME = freepacksizename;
                        gtfreedetails.SCHEME_QTY = schemeqtypcs;
                        gtfreedetails.MRP = freemrp;
                        gtfreedetails.BRATE = brate;
                        gtfreedetails.AMOUNT = freeamount;
                        gtfreedetails.BATCHNO = freebatchno;
                        gtfreedetails.WEIGHT = freeweight;
                        gtfreedetails.MFDATE = freemfdate;
                        gtfreedetails.EXPRDATE = freeexprdate;
                        gtfreedetails.NSR = freensr;
                        gtfreedetails.SCHEME_PRODUCT_BATCHNO = schemebatchno;
                        gtfreedetails.QSGUID = freeqsguid;
                        freelist[j++] = gtfreedetails;
                    });
                }
                csdinvoicesave.FreeDetailsGT = freelist;

                if (comborowCount <= 0) {

                    //debugger
                    var csdcombodetails = {};

                    csdcombodetails.PRIMARYPRODUCTID = 'NA';
                    csdcombodetails.SECONDARYPRODUCTID = 'NA';
                    csdcombodetails.QTY = '0';
                    combolist[k++] = csdcombodetails;

                }
                else {

                    $('#comboBreakupGrid tbody tr').each(function () {

                        //debugger
                        var csdcombodetails = {};

                        var primaryid = $(this).find('td:eq(0)').html().trim();
                        var secondaryid = $(this).find('td:eq(1)').html().trim();
                        var breakupqty = $(this).find('td:eq(2)').html().trim();

                        csdcombodetails.PRIMARYPRODUCTID = primaryid;
                        csdcombodetails.SECONDARYPRODUCTID = secondaryid;
                        csdcombodetails.QTY = breakupqty;
                        combolist[k++] = csdcombodetails;
                    });
                }
                csdinvoicesave.ComboDetailsCSD = combolist;

                $('#taxDetailsGrid tbody tr').each(function () {

                    //debugger
                    var gttaxdetails = {};

                    var primaryproductid = $(this).find('td:eq(1)').html().trim();
                    var primarybatch = $(this).find('td:eq(2)').html().trim();
                    var productid = $(this).find('td:eq(1)').html().trim();
                    var batch = $(this).find('td:eq(2)').html().trim();
                    var taxid = $(this).find('td:eq(5)').html().trim();
                    var taxpercentage = $(this).find('td:eq(6)').html().trim();
                    var tax = $(this).find('td:eq(7)').html().trim();
                    var tag = $(this).find('td:eq(8)').html().trim();
                    var taxmrp = $(this).find('td:eq(9)').html().trim();


                    gttaxdetails.PRIMARYPRODUCTID = primaryproductid;
                    gttaxdetails.PRIMARYPRODUCTBATCHNO = primarybatch;
                    gttaxdetails.PRODUCTID = productid;
                    gttaxdetails.BATCHNO = batch;
                    gttaxdetails.TAXID = taxid;
                    gttaxdetails.TAXPERCENTAGE = taxpercentage;
                    gttaxdetails.TAXVALUE = tax;
                    gttaxdetails.TAG = tag;
                    gttaxdetails.MRP = taxmrp;

                    taxlist[l++] = gttaxdetails;
                });
                csdinvoicesave.TaxDetailsGT = taxlist;

                //alert(JSON.stringify(invoicesave));

                $.ajax({
                    url: "/TranDepot/csdInvoicesavedata",
                    data: '{csdinvoicesave:' + JSON.stringify(csdinvoicesave) + '}',
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
                            $('#dvAdd').css("display", "");
                            $('#dvDisplay').css("display", "none");
                            toastr.success('<b><font color=black>' + messagetext + '</font></b>');
                            ClearControls();
                            //ReleaseSession();
                        }
                        else if (messageid == '2') {
                            $('#dvAdd').css("display", "none");
                            $('#dvDisplay').css("display", "");
                            $("#txtfrmdate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
                            $("#txttodate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
                            ClearControls();
                            bindcsdinvoicegrid();
                            //ReleaseSession();
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
}

function ReleaseSession() {

    $.ajax({
        type: "POST",
        url: "/TranDepot/RemoveSessionCSDInvoice",
        data: '{}',
        dataType: "json",
        success: function (response) {
        }
    });
}

function bindcsdinvoicegrid() {
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
        url: "/TranDepot/BindMtInvoiceGrid",
        data: { FromDate: $('#txtfrmdate').val().trim(), ToDate: $('#txttodate').val().trim(), BSID: BusinessSegment, CheckerFlag: CHECKER.trim(), depotID: $('#BRID').val().trim(), Challan: 'FALSE', UserID: $('#hdnUserID').val().trim(), FinYear: $('#hdnFinYear').val().trim() },
        dataType: "json",
        success: function (response) {
            var tr;
            tr = $('#CSDInvoiceGrid');
            tr.append("<thead><tr><th>Sl. No.</th><th style='display: none'>SALEINVOICEID</th><th>Invoice No</th><th>Invoice Date</th><th style='display: none'>Vehichle No</th><th style='display: none'>LR-GR No</th><th style='display: none'>Depot</th><th>Customer</th><th>Transporter</th><th style='display: none'>Finyear</th><th style='display: none'>ISVERIFIED</th><th>Financial Status</th><th>Dayend</th><th style='display: none'>Transit Status</th><th style='display: none'>NEXTLEVELID</th><th>Entry User</th><th>Approval Person</th><th>Net Amount</th><th>Print</th><th>Edit</th><th>View</th><th>Cancel</th>");

            $('#CSDInvoiceGrid').DataTable().destroy();
            $("#CSDInvoiceGrid tbody tr").remove();
            for (var i = 0; i < response.length; i++) {
                tr = $('<tr/>');
                tr.append("<td style='text-align: center'>" + srl + "</td>");//0
                tr.append("<td style='display: none'>" + response[i].SALEINVOICEID + "</td>");
                tr.append("<td>" + response[i].SALEINVOICENO + "</td>");
                tr.append("<td>" + response[i].SALEINVOICEDATE + "</td>");
                tr.append("<td style='display: none'>" + response[i].VEHICHLENO + "</td>");
                tr.append("<td style='display: none'>" + response[i].LRGRNO + "</td>");
                tr.append("<td style='display: none'>" + response[i].DEPOTNAME + "</td>");
                tr.append("<td>" + response[i].DISTRIBUTORNAME + "</td>");
                tr.append("<td>" + response[i].TRANSPORTERNAME + "</td>");
                tr.append("<td style='display: none'>" + response[i].FINYEAR + "</td>");
                tr.append("<td style='display: none'>" + response[i].ISVERIFIED + "</td>");
                if (response[i].ISVERIFIEDDESC.trim() == 'Approved') {
                    tr.append("<td style='text-align: center'><font color='0x00B300'>" + response[i].ISVERIFIEDDESC + "</font></td>");
                }
                else if (response[i].ISVERIFIEDDESC.trim() == 'Pending') {
                    tr.append("<td style='text-align: center'><font color='0x5184FF'>" + response[i].ISVERIFIEDDESC + "</font></td>");
                }
                else {
                    tr.append("<td style='text-align: center'><font color='#ff2500'>" + response[i].ISVERIFIEDDESC + "</font></td>");
                }
                if (response[i].DAYENDTAG.trim() == 'Done') {
                    tr.append("<td style='text-align: center'><font color='0x00B300'>" + response[i].DAYENDTAG + "</font></td>");
                }
                else {
                    tr.append("<td style='text-align: center'><font color='0x5184FF'>" + response[i].DAYENDTAG + "</font></td>");
                }
                if (response[i].INTRANSITDESC.trim() == 'Received') {
                    tr.append("<td style='text-align: center;display: none'><font color='0x00B300'>" + response[i].INTRANSITDESC + "</font></td>");
                }
                else {
                    tr.append("<td style='text-align: center;display: none'><font color='#ff2500'>" + response[i].INTRANSITDESC + "</font></td>");
                }
                tr.append("<td style='display: none'>" + response[i].NEXTLEVELID + "</td>");
                tr.append("<td>" + response[i].USERNAME + "</td>");
                tr.append("<td>" + response[i].APPROVAL_PERSON + "</td>");
                tr.append("<td style='text-align: right'>" + response[i].NETAMOUNT + "</td>");
                tr.append("<td style='text-align: center'><input type='image' class='gvCSDPrint'  id='btncsdprint'  <img src='../Images/ico_Print.png' width='20' height ='20' title='Print'/></input></td>");
                tr.append("<td style='text-align: center'><input type='image' class='gvCSDEdit'   id='btncsdedit'   <img src='../Images/Pencil-icon.png' title='Edit'/></input></td>");
                tr.append("<td style='text-align: center'><input type='image' class='gvCSDView'   id='btncsdview'   <img src='../Images/View.png' width='20' height ='20' title='View'/></input></td>");
                tr.append("<td style='text-align: center'><input type='image' class='gvCSDCancel' id='btncsddelete' <img src='../Images/ico_delete_16.png' title='Cancel'/></input></td>");

                $("#CSDInvoiceGrid").append(tr);
            }
            tr.append("</tbody>");
            RowCountCsdInvoiceList();
            $('#CSDInvoiceGrid').DataTable({
                "sScrollX": '100%',
                "sScrollXInner": "110%",
                "scrollY": "238px",
                "scrollCollapse": true,
                "dom": 'Bfrtip',
                buttons: [
                    {
                        extend: 'excelHtml5',
                        title: 'CSD Invoice List'
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

function bindlockdateflag(entrydate, finyear) {
    var returnlockdateflag = null;
    $.ajax({
        type: "POST",
        url: "/TranDepot/GetEntryLockChecking",
        data: { EntryDate: entrydate.trim(), Finyear: finyear.trim() },
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

function FinanceStatus(invoiceid) {
    var returnValue = null;
    $.ajax({
        type: "POST",
        url: "/TranDepot/GetInvoiceStatus",
        data: { InvoiceID: invoiceid, ModuleID: '7', Type: '3' },
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

function RatePerCase(bsid,groupid,productid) {
    var returnValue = null;
    $.ajax({
        type: "POST",
        url: "/TranDepot/Ratepercase",
        data: { BSID: bsid, GroupID: groupid, ProductID: productid },
        dataType: "json",
        async: false,
        success: function (response) {
            var status;
            $.each(response, function (key, item) {
                status = item.RATEPERCASE;
                returnValue = status;

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
        data: { DepotID: depotid },
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

function TransitStatus(invoiceid) {
    var returnValue = null;
    $.ajax({
        type: "POST",
        url: "/TranDepot/GetInvoiceStatus",
        data: { InvoiceID: invoiceid, ModuleID: '7', Type: '1' },
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

function PostingStatus(invoiceid) {
    var returnValue = null;
    $.ajax({
        type: "POST",
        url: "/TranDepot/PostingStatus",
        data: { InvoiceID: invoiceid},
        dataType: "json",
        async: false,
        success: function (responseMessage) {
            var status;
            $.each(responseMessage, function (key, item) {
                status = item.POSTING_STATUS;
                returnValue = status;
                
                return false;
            });

        }
    });
    return returnValue;
}

function CodeStatus(customerid) {
    var returnValue = null;
    $.ajax({
        type: "POST",
        url: "/TranDepot/CodeStatus",
        data: { CustomerID: customerid },
        dataType: "json",
        async: false,
        success: function (responseMessage) {
            var status;
            $.each(responseMessage, function (key, item) {
                status = item.CPC_CODE_STATUS;
                returnValue = status;
                return false;
            });
        }
    });
    return returnValue;
}

function ClosingStatus(invoicedate, depotid, finyear) {
    var returnValue = null;
    $.ajax({
        type: "POST",
        url: "/TranDepot/GetGTClosingStatus",
        data: { InvoiceDate: invoicedate, DepotID: depotid, Finyear: finyear },
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

function AccountPostingStatus(depotid, bsid, invoicedate, userid) {
    var returnValue = null;
    $.ajax({
        type: "POST",
        url: "/TranDepot/GetAccPostingStatus",
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

function DayEndStatus(depotid, bsid, invoicedate, userid) {
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

function EditDetails(invoiceid) {
    //debugger;
    var tr;
    var trfree;
    var trtax;
    var HeaderCount = 0;
    var FooterCount = 0;
    var HeaderCountfree = 0;
    var FooterCountfree = 0;
    var HeaderCounttax = 0;
    var FooterCounttax = 0;
    var srl = 0;
    srl = srl + 1;
    var Igstid = '';
    var Cgstid = '';
    var Sgstid = '';
    var IgstPercentage = 0;
    var IgstAmount = 0;
    var CgstPercentage = 0;
    var CgstAmount = 0;
    var SgstPercentage = 0;
    var SgstAmount = 0;
    var NetAmount = 0;
    var Product = $("#PRODUCTID");
    var Customer = $("#CUSTOMERID");
    var Saleorder = $("#SaleOrderID");
    var OrderID = '';
    //debugger;


    $('#productDetailsGrid').empty();
    $('#freeDetailsGrid').empty();
    $('#taxDetailsGrid').empty();

    $("#dialog").dialog({
        autoOpen: true,
        modal: true,
        title: "Loading.."
    });
    $("#imgLoader").css("visibility", "visible");
    $.ajax({
        type: "POST",
        url: "/TranDepot/GTInvoiceEdit",
        data: { InvoiceID: invoiceid },
        dataType: "json",
        async: false,
        success: function (response) {
            //alert(JSON.stringify(response))
            //debugger;
            var listHeader = response.GTeditDataset.varHeader;
            var listDetails = response.GTeditDataset.varDetails;
            var listTaxcount = response.GTeditDataset.varTaxCount;
            var listFooter = response.GTeditDataset.varFooter;
            var listTax = response.GTeditDataset.varTaxDetails;
            var listPriceSchemeDetails = response.GTeditDataset.varPricescheme;
            var listQtySchemeDetails = response.GTeditDataset.varQtyscheme;
            var listOrderHeader = response.GTeditDataset.varOrderHeader;
            var listComboProductDetails = response.GTeditDataset.varProductDetails;
            //debugger;

            bindtransporter($("#BRID").val().trim(), '0');

            /*Tax Count Info*/
            if (listTaxcount.length > 0) {
                if (listTaxcount.length == 1) {
                    $.each(listTaxcount, function (key, item) {
                        //debugger;
                        $("#hdntaxcount").val('1');
                        Igstid = listTaxcount[0].TAXID;
                        $("#hdntaxnameIGST").val(listTaxcount[0].NAME);
                        $("#hdnrelatedto").val(listTaxcount[0].RELATEDTO);
                    });
                }
                else if (listTaxcount.length == 2) {
                    $.each(listTaxcount, function (index, record) {
                        //debugger;
                        $("#hdntaxcount").val('2');
                        Cgstid = listTaxcount[0].TAXID;
                        Sgstid = listTaxcount[1].TAXID;
                        $("#hdntaxnameCGST").val(listTaxcount[0].NAME);
                        $("#hdntaxnameSGST").val(listTaxcount[1].NAME);
                        $("#hdnrelatedto").val(listTaxcount[0].RELATEDTO);
                    });
                }
            }
            else {
                $("#hdntaxcount").val('0');
                $("#hdntaxnameIGST").val('NA');
                $("#hdntaxnameCGST").val('NA');
                $("#hdntaxnameSGST").val('NA');
                Igstid = 'NA';
                Cgstid = 'NA';
                Sgstid = 'NA';
            }

            /*Invoice Header Info*/
            $.each(listHeader, function (index, record) {
                ////debugger;
                $("#hdndispatchID").val(this['SALEINVOICEID'].toString().trim());
                $("#FGInvoiceNo").val(this['SALEINVOICENO'].toString().trim());
                $("#InvoiceDate").val(this['SALEINVOICEDATE'].toString().trim());
                $("#TransporterID").val(this['TRANSPORTERID'].toString().trim());
                $("#TransportMode").val(this['MODEOFTRANSPORT'].toString().trim());
                $("#PaymentMode").val(this['PAYMENTMODE'].toString().trim());
                $("#VehichleNo").val(this['VEHICHLENO'].toString().trim());
                $("#LrGrNo").val(this['LRGRNO'].toString().trim());
                $("#LrGrDate").val(this['LRGRDATE'].toString().trim());
                $("#GatepassNo").val(this['GETPASSNO'].toString().trim());
                $("#GatepassDate").val(this['GETPASSDATE'].toString().trim());
                $("#TotalPcs").val(parseInt(this['TOTALPCS'].toString().trim()));
                $("#Remarks").val(this['REMARKS'].toString().trim());
                $("#ReferencePO").val(this['OTHERREFNO'].toString().trim());
                $("#ShippingAddress").val(this['DELIVERYADDRESS'].toString().trim());
                $("#ICDSNo").val(this['ICDSNO'].toString().trim());
                $("#ICDSDate").val(this['ICDSDATE'].toString().trim());
               

                Customer.empty();
                Customer.append($("<option></option>").val(this['DISTRIBUTORID']).html(this['DISTRIBUTORNAME']));

                var MonthName = '';
                var Month = $('#InvoiceDate').val().trim();
                Month = Month.substr(3, 2);
                switch (Month) {
                    case '01':
                        MonthName = "JAN.";
                        break;
                    case '02':
                        MonthName = "FEB.";
                        break;
                    case '03':
                        MonthName = "MAR.";
                        break;
                    case '04':
                        MonthName = "APR.";
                        break;
                    case '05':
                        MonthName = "MAY";
                        break;
                    case '06':
                        MonthName = "JUN.";
                        break;
                    case '07':
                        MonthName = "JUL.";
                        break;
                    case '08':
                        MonthName = "AUG.";
                        break;
                    case '09':
                        MonthName = "SEP.";
                        break;
                    case '10':
                        MonthName = "OCT.";
                        break;
                    case '11':
                        MonthName = "NOV.";
                        break;
                    case '12':
                        MonthName = "DEC.";
                        break;
                }
                $('#spnInvLimit').text('Inv. Limit, Done & Bal.For ' + MonthName + ' ');
                $('#spnTGT').text('Trgt, Bal. & Ach.(%) For ' + MonthName + ' ');
               
                ClBalanceCrLimit($('#CUSTOMERID').val().trim(), $('#BRID').val().trim(), $('#hdndispatchID').val().trim(), $('#hdnFinYear').val().trim());
                //CustomerTargetDetails($('#CUSTOMERID').val().trim(), $('#BRID').val().trim(), BusinessSegment, Month, $('#hdndispatchID').val().trim(), $('#hdnFinYear').val().trim());
                CustomerDetails($('#CUSTOMERID').val().trim(), $('#BRID').val().trim(), Month, $('#hdndispatchID').val().trim(), $('#hdnFinYear').val().trim());
            });


            /*Invoice Details Info*/
            if (listDetails.length > 0) {

                $("#productDetailsGrid").empty();
                if ($("#hdntaxcount").val().trim() == '1') {

                    tr;
                    tr = $('#productDetailsGrid');
                    HeaderCount = $('#productDetailsGrid thead th').length;
                    FooterCount = $('#productDetailsGrid tfoot th').length;
                    if (HeaderCount == 0) {
                        tr.append("<thead><tr><th style='text-align: center;'>Sl.No.</th><th style='display: none;'>Product ID</th><th>Product</th><th>HSN Code</th><th style='display: none;'>PackSize ID</th><th style='display: none;'>Pack Size</th><th>MRP</th><th style='display: none;'>NSR</th><th>Rate/Pcs</th><th>Rate/Case</th><th>Case</th><th>Pcs</th><th>Batch</th><th style='display: none;'>Assesmet(%)</th><th style='display: none;'>Assesment Amt.</th><th style='display: none;'>PRIMARYPRICESCHEMEID</th><th style='display: none;'>Add.SS.Disc.</th><th>Sch(%)</th><th>Sch.Amt.</th><th style='display: none;'>Disc(%)</th><th style='display: none;'>Disc.Amt.</th><th>Amount</th><th style='display: none;'>Weight</th><th>Mfg.Date</th><th>Exp.Date</th><th style='display: none;'>QSH</th><th style='display: none;'>QSGUID</th><th style='display: none;'>FLAG</th><th>IGST(%)</th><th>IGST</th><th>Net Amt.</th></tr></thead>");
                    }
                    if (FooterCount == 0) {
                        tr.append("<tfoot><tr><th></th><th style='display: none;'></th><th></th><th style='color: blue;'>Total</th><th style='display: none;'></th><th style='display: none;'></th><th></th><th style='display: none;'></th><th></th><th></th><th style='color: blue;text-align: right;' id='tfCase'></th><th style='color: blue;text-align: right;' id='tfPcs'></th><th></th><th style='display: none;'></th><th style='display: none;'></th><th style='display: none;'></th><th style='color: blue;text-align: right;display: none;' id='tfAddSSDisc'></th><th></th><th style='color: blue;text-align: right;' id='tfSchemeAmt'></th><th style='display: none;'></th><th style='color: blue;text-align: right;display: none;' id='tfDiscAmt'></th><th style='color: blue;text-align: right;' id='tfBasicAmt'></th><th style='display: none;'></th><th></th><th></th><th style='display: none;'></th><th style='display: none;'></th><th></th><th style='color: blue;text-align: right;' id='tfIGST'></th><th style='color: blue;text-align: right;' id='tfNetAmt'></th></tr></tfoot><tbody>");
                    }
                    $.each(listDetails, function (index, record) {
                        ////debugger;
                        tr = $('<tr/>');
                        tr.append("<td style='text-align: center;'><label id='lblslno'></label><span><input type='button' class='action-icons c-delete' id='btnBilldelete' value='Delete' /></span></td>");//0
                        tr.append("<td style='display: none;'>" + this['PRODUCTID'].toString().trim() + "</td>");//1
                        tr.append("<td style='width:250px;'>" + this['PRODUCTNAME'].toString().trim() + "</td>");//2
                        tr.append("<td style='text-align: center;'>" + this['HSNCODE'].toString().trim() + "</td>");//3
                        tr.append("<td style='display: none;'>" + this['PACKINGSIZEID'].toString().trim() + "</td>");//4
                        tr.append("<td style='display: none;'>" + this['PACKINGSIZENAME'].toString().trim() + "</td>");//5
                        tr.append("<td style='text-align: right;'>" + parseFloat(this['MRP'].toString().trim()).toFixed(2) + "</td>");//6
                        tr.append("<td style='display: none;'>" + parseFloat(this['NSR'].toString().trim()).toFixed(2) + "</td>");//7-NSR
                        tr.append("<td style='text-align: right;'>" + parseFloat(this['BCP'].toString().trim()).toFixed(2) + "</td>");//8
                        tr.append("<td style='text-align: right;'>" + parseFloat(this['RATEPERCASE'].toString().trim()).toFixed(2) + "</td>");//9
                        tr.append("<td style='text-align: right;'>" + parseInt(this['QTY'].toString().trim()) + "</td>");//10
                        tr.append("<td style='text-align: right;'>" + parseInt(this['QTYPCS'].toString().trim()) + "</td>");//11
                        tr.append("<td style='text-align: center;'>" + this['BATCHNO'].toString().trim() + "</td>");//12
                        tr.append("<td style='display: none;'>" + '0' + "</td>");//13
                        tr.append("<td style='display: none;'>" + '0' + "</td>");//14
                        tr.append("<td style='display: none;'>" + this['PRIMARYPRICESCHEMEID'].toString().trim() + "</td>");//15
                        tr.append("<td style='text-align: right;display: none;'>" + parseFloat(this['RATEDISCVALUE'].toString().trim()).toFixed(2) + "</td>");//16
                        tr.append("<td style='text-align: right;'>" + parseFloat(this['PERCENTAGE'].toString().trim()).toFixed(2) + "</td>");//17
                        tr.append("<td style='text-align: right;'>" + parseFloat(this['VALUE'].toString().trim()).toFixed(2) + "</td>");//18
                        tr.append("<td style='text-align: right;display: none;'>" + parseFloat(this['DISCPER'].toString().trim()).toFixed(2) + "</td>");//19
                        tr.append("<td style='text-align: right;display: none;'>" + parseFloat(this['DISCAMT'].toString().trim()).toFixed(2) + "</td>");//20
                        tr.append("<td style='text-align: right;'>" + parseFloat(this['AMOUNT'].toString().trim()).toFixed(2) + "</td>");//21
                        tr.append("<td style='display: none;'>" + this['WEIGHT'].toString().trim() + "</td>");//22
                        tr.append("<td style='text-align: center;'>" + this['MFDATE'].toString().trim() + "</td>");//23
                        tr.append("<td style='text-align: center;'>" + this['EXPRDATE'].toString().trim() + "</td>");//24
                        tr.append("<td style='display: none;'>" + this['QSGUID'].toString().trim() + "</td>");//25
                        tr.append("<td style='display: none;'>" + this['QSH'].toString().trim() + "</td>");//26
                        tr.append("<td style='display: none;'>" + '1' + "</td>");//27

                        IgstPercentage = GetTaxOnEdit(invoiceid.trim(), Igstid, this['PRODUCTID'].toString().trim(), this['BATCHNO'].toString().trim());
                        IgstAmount = parseFloat((Math.round(((this['AMOUNT'].toString().trim() * IgstPercentage) / 100) * 100) / 100));
                        NetAmount = (parseFloat(this['AMOUNT'].toString().trim()) + IgstAmount);

                        tr.append("<td style='text-align: right;'>" + parseFloat(IgstPercentage).toFixed(2) + "</td>");//28
                        tr.append("<td style='text-align: right;'>" + parseFloat(IgstAmount).toFixed(2) + "</td>");//29
                        tr.append("<td style='text-align: right;'>" + NetAmount.toFixed(2) + "</td>");//30
                        $("#productDetailsGrid").append(tr);
                        tr.append("</tbody>");
                        RowCountEdit();

                    });
                }
                else if ($("#hdntaxcount").val().trim() == '2') {
                    ////debugger;
                    tr = $('#productDetailsGrid');
                    HeaderCount = $('#productDetailsGrid thead th').length;
                    FooterCount = $('#productDetailsGrid tfoot th').length;
                    if (HeaderCount == 0) {
                        tr.append("<thead><tr><th style='text-align: center;'>Sl.No.</th><th style='display: none;'>Product ID</th><th>Product</th><th>HSN Code</th><th style='display: none;'>PackSize ID</th><th style='display: none;'>Pack Size</th><th>MRP</th><th style='display: none;'>NSR</th><th>Rate/Pcs</th><th>Rate/Case</th><th>Case</th><th>Pcs</th><th>Batch</th><th style='display: none;'>Assesmet(%)</th><th style='display: none;'>Assesment Amt.</th><th style='display: none;'>PRIMARYPRICESCHEMEID</th><th style='display: none;'>Add.SS.Disc.</th><th>Sch(%)</th><th>Sch.Amt.</th><th style='display: none;'>Disc(%)</th><th style='display: none;'>Disc.Amt.</th><th>Amount</th><th style='display: none;'>Weight</th><th>Mfg.Date</th><th>Exp.Date</th><th style='display: none;'>QSH</th><th style='display: none;'>QSGUID</th><th style='display: none;'>FLAG</th><th>CGST(%)</th><th>CGST</th><th>SGST(%)</th><th>SGST</th><th>Net Amt.</th></tr></thead>");
                    }
                    if (FooterCount == 0) {
                        tr.append("<tfoot><tr><th></th><th style='display: none;'></th><th></th><th style='color: blue;'>Total</th><th style='display: none;'></th><th style='display: none;'></th><th></th><th style='display: none;'></th><th></th><th></th><th style='color: blue;text-align: right;' id='tfCase'></th><th style='color: blue;text-align: right;' id='tfPcs'></th><th></th><th style='display: none;'></th><th style='display: none;'></th><th style='display: none;'></th><th style='color: blue;text-align: right;display: none;' id='tfAddSSDisc'></th><th></th><th style='color: blue;text-align: right;' id='tfSchemeAmt'></th><th style='display: none;'></th><th style='color: blue;text-align: right;display: none;' id='tfDiscAmt'></th><th style='color: blue;text-align: right;' id='tfBasicAmt'></th><th style='display: none;'></th><th></th><th></th><th style='display: none;'></th><th style='display: none;'></th><th style='display: none;'></th><th></th><th style='color: blue;text-align: right;' id='tfCGST'></th><th></th><th style='color: blue;text-align: right;' id='tfSGST'></th><th style='color: blue;text-align: right;' id='tfNetAmt'></th></tr></tfoot><tbody>");
                    }
                    $.each(listDetails, function (index, record) {
                        ////debugger;
                        tr = $('<tr/>');
                        tr.append("<td style='text-align: center;'><label id='lblslno'></label><span><input type='button' class='action-icons c-delete' id='btnBilldelete' value='Delete' /></span></td>");//0
                        tr.append("<td style='display: none;'>" + this['PRODUCTID'].toString().trim() + "</td>");//1
                        tr.append("<td style='width:250px;'>" + this['PRODUCTNAME'].toString().trim() + "</td>");//2
                        tr.append("<td style='text-align: center;'>" + this['HSNCODE'].toString().trim() + "</td>");//3
                        tr.append("<td style='display: none;'>" + this['PACKINGSIZEID'].toString().trim() + "</td>");//4
                        tr.append("<td style='display: none;'>" + this['PACKINGSIZENAME'].toString().trim() + "</td>");//5
                        tr.append("<td style='text-align: right;'>" + parseFloat(this['MRP'].toString().trim()).toFixed(2) + "</td>");//6
                        tr.append("<td style='display: none;'>" + parseFloat(this['NSR'].toString().trim()).toFixed(2) + "</td>");//7-NSR
                        tr.append("<td style='text-align: right;'>" + parseFloat(this['BCP'].toString().trim()).toFixed(2) + "</td>");//8
                        tr.append("<td style='text-align: right;'>" + parseFloat(this['RATEPERCASE'].toString().trim()).toFixed(2) + "</td>");//9
                        tr.append("<td style='text-align: right;'>" + parseInt(this['QTY'].toString().trim()) + "</td>");//10
                        tr.append("<td style='text-align: right;'>" + parseInt(this['QTYPCS'].toString().trim()) + "</td>");//11
                        tr.append("<td style='text-align: center;'>" + this['BATCHNO'].toString().trim() + "</td>");//12
                        tr.append("<td style='display: none;'>" + '0' + "</td>");//13
                        tr.append("<td style='display: none;'>" + '0' + "</td>");//14
                        tr.append("<td style='display: none;'>" + this['PRIMARYPRICESCHEMEID'].toString().trim() + "</td>");//15
                        tr.append("<td style='text-align: right;display: none;'>" + parseFloat(this['RATEDISCVALUE'].toString().trim()).toFixed(2) + "</td>");//16
                        tr.append("<td style='text-align: right;'>" + parseFloat(this['PERCENTAGE'].toString().trim()).toFixed(2) + "</td>");//17
                        tr.append("<td style='text-align: right;'>" + parseFloat(this['VALUE'].toString().trim()).toFixed(2) + "</td>");//18
                        tr.append("<td style='text-align: right;display: none;'>" + parseFloat(this['DISCPER'].toString().trim()).toFixed(2) + "</td>");//19
                        tr.append("<td style='text-align: right;display: none;'>" + parseFloat(this['DISCAMT'].toString().trim()).toFixed(2) + "</td>");//20
                        tr.append("<td style='text-align: right;'>" + parseFloat(this['AMOUNT'].toString().trim()).toFixed(2) + "</td>");//21
                        tr.append("<td style='display: none;'>" + this['WEIGHT'].toString().trim() + "</td>");//22
                        tr.append("<td style='text-align: center;'>" + this['MFDATE'].toString().trim() + "</td>");//23
                        tr.append("<td style='text-align: center;'>" + this['EXPRDATE'].toString().trim() + "</td>");//24
                        tr.append("<td style='display: none;'>" + this['QSGUID'].toString().trim() + "</td>");//25
                        tr.append("<td style='display: none;'>" + this['QSH'].toString().trim() + "</td>");//26
                        tr.append("<td style='display: none;'>" + '1' + "</td>");//27

                        CgstPercentage = GetTaxOnEdit(invoiceid.trim(), Cgstid, this['PRODUCTID'].toString().trim(), this['BATCHNO'].toString().trim());
                        CgstAmount = parseFloat((Math.round(((this['AMOUNT'].toString().trim() * CgstPercentage) / 100) * 100) / 100));
                        SgstPercentage = GetTaxOnEdit(invoiceid.trim(), Sgstid, this['PRODUCTID'].toString().trim(), this['BATCHNO'].toString().trim());
                        SgstAmount = parseFloat((Math.round(((this['AMOUNT'].toString().trim() * SgstPercentage) / 100) * 100) / 100));
                        NetAmount = (parseFloat(this['AMOUNT'].toString().trim()) + CgstAmount + SgstAmount);

                        tr.append("<td style='text-align: right;'>" + parseFloat(CgstPercentage).toFixed(2) + "</td>");//28
                        tr.append("<td style='text-align: right;'>" + parseFloat(CgstAmount).toFixed(2) + "</td>");//29
                        tr.append("<td style='text-align: right;'>" + parseFloat(SgstPercentage).toFixed(2) + "</td>");//30
                        tr.append("<td style='text-align: right;'>" + parseFloat(SgstAmount).toFixed(2) + "</td>");//31
                        tr.append("<td style='text-align: right;'>" + NetAmount.toFixed(2) + "</td>");//32
                        $("#productDetailsGrid").append(tr);
                        tr.append("</tbody>");
                        RowCountEdit();

                    });
                }
            }

            /*Free Details Info*/
            if (listQtySchemeDetails.length > 0) {
                //debugger;


                trfree = $('#freeDetailsGrid');
                HeaderCountfree = $('#freeDetailsGrid thead th').length;
                FooterCountfree = $('#freeDetailsGrid tfoot th').length;
                if (HeaderCountfree == 0) {
                    trfree.append("<thead><tr><th style='text-align: center;'>Delete</th><th style='display: none;'>Scheme ID</th><th style='display: none;>Primary Product ID</th><th style='display: none;'>Primary Product</th><th style='display: none;'>Primary Batch</th><th style='display: none;'>Qty</th><th style='display: none;'>Scheme Product ID</th><th>Free Product</th><th>HSN Code</th><th style='display: none;'>Packsize ID</th><th style='display: none;'>Packsize</th><th>Free Qty(Pcs)</th><th>MRP</th><th style='display: none;'>RateDisc</th><th>Rate</th><th>Disc.Amt.</th><th>Batch</th><th style='display: none;'>Assesment(%)</th><th style='display: none;'>Assesment Amt.</th><th>Mfg.Date</th><th>Exp.Date</th><th style='display: none;'>Weight</th><th style='display: none;'>NSR</th><th style='display: none;'>Ratedisc Value</th><th>Net Amt</th><th style='display: none;'>QSGUID</th></tr></thead>");
                }
                if (FooterCountfree == 0) {
                    trfree.append("<tfoot><tr><th></th><th style='display: none;'></th><th style='display: none;></th><th style='display: none;'></th><th style='display: none;'></th><th style='display: none;'></th><th style='display: none;'></th><th></th><th style='color: blue;'>Total</th><th style='display: none;'></th><th style='display: none;'></th><th style='color: blue;text-align: right;' id='tfFreeQty'></th><th></th><th style='display: none;'></th><th></th><th style='color: blue;text-align: right;' id='tfFreeAmt'></th><th></th><th style='display: none;'></th><th style='display: none;'></th><th></th><th></th><th style='display: none;'></th><th style='display: none;'></th><th style='display: none;'></th><th style='color: blue;text-align: right;' id='tfFreeNetAmt'></th><th style='display: none;'></th></tr></tfoot><tbody>");
                }

                $.each(listQtySchemeDetails, function (index, record) {
                    //debugger;
                    trfree = $('<tr/>');
                    if (this['SCHEMEID'].toString().trim() == '0') {

                        trfree.append("<td style='text-align: center;'><label class='slno' id='lblfreeslno'></label><span><input type='button' class='action-icons c-delete' id='btnfreedelete' value='Delete' /></span></td>");//0
                    }
                    else if (this['SCHEMEID'].toString().trim() != '0') {
                        trfree.append("<td style='text-align: center;'><label class='slno' id='lblfreeslno'></label></td>");//0
                    }
                    trfree.append("<td style='display: none;'>" + this['SCHEMEID'].toString().trim() + "</td>");//1
                    trfree.append("<td style='display: none;'>" + this['SCHEME_PRODUCT_ID'].toString().trim() + "</td>");//2
                    trfree.append("<td style='display: none;'>" + this['SCHEME_PRODUCT_NAME'].toString().trim() + "</td>");//3
                    trfree.append("<td style='display: none;'>" + this['SCHEME_PRODUCT_BATCHNO'].toString().trim() + "</td>");//4
                    trfree.append("<td style='display: none;'>" + this['QTY'].toString().trim() + "</td>");//5
                    trfree.append("<td style='display: none;'>" + this['PRODUCTID'].toString().trim() + "</td>");//6
                    trfree.append("<td style='width:250px;'>" + this['PRODUCTNAME'].toString().trim() + "</td>");//7
                    trfree.append("<td >" + getHSNCode(this['PRODUCTID'].toString().trim()) + "</td>");//8
                    trfree.append("<td style='display: none;'>" + this['PACKSIZEID'].toString().trim() + "</td>");//9
                    trfree.append("<td style='display: none;'>" + this['PACKSIZENAME'].toString().trim() + "</td>");//10
                    trfree.append("<td style='text-align: right;'>" + parseFloat(this['SCHEME_QTY'].toString().trim()).toFixed(0) + "</td>");//11
                    trfree.append("<td style='text-align: right;'>" + parseFloat(this['MRP'].toString().trim()).toFixed(2) + "</td>");//12
                    trfree.append("<td style='display: none;'>" + '0' + "</td>");//13
                    trfree.append("<td style='text-align: right;'>" + parseFloat(this['BRate'].toString().trim()).toFixed(2) + "</td>");//14
                    trfree.append("<td style='text-align: right;'>" + parseFloat(this['AMOUNT'].toString().trim()).toFixed(2) + "</td>");//15
                    trfree.append("<td >" + this['BATCHNO'].toString().trim() + "</td>");//16
                    trfree.append("<td style='display: none;'>" + '0' + "</td>");//17
                    trfree.append("<td style='display: none;'>" + '0' + "</td>");//18
                    trfree.append("<td >" + this['MFDATE'].toString().trim() + "</td>");//19
                    trfree.append("<td >" + this['EXPRDATE'].toString().trim() + "</td>");//20
                    trfree.append("<td style='display: none;'>" + this['WEIGHT'].toString().trim() + "</td>");//21
                    trfree.append("<td style='display: none;'>" + this['NSR'].toString().trim() + "</td>");//22
                    trfree.append("<td style='display: none;'>" + '0' + "</td>");//23
                    trfree.append("<td style='text-align: right;'>" + parseFloat(this['AMOUNT'].toString().trim()).toFixed(2) + "</td>");//24
                    trfree.append("<td style='display: none;'>" + this['QSGUID'].toString().trim() + "</td>");//25
                    $("#freeDetailsGrid").append(trfree);
                    trfree.append("</tbody>");
                    RowCountFreeGridEdit();
                });
            }

            /*Tax Details Info*/
            /*if ($("#hdntaxcount").val() == '1') {
                $.each(listTax, function (index, record) {
                    //debugger;
                    addRowinTaxTableEdit(this['PRODUCTID'].toString().trim(), this['BATCHNO'].toString().trim(), this['TAXID'].toString().trim(), this['TAXPERCENTAGE'].toString().trim(), this['TAXVALUE'].toString().trim(), this['MRP'].toString().trim(), '1');
                });
            }
            else if ($("#hdntaxcount").val() == '2') {
                $.each(listTax, function (index, record) {
                    //debugger;
                    addRowinTaxTableEdit(this['PRODUCTID'].toString().trim(), this['BATCHNO'].toString().trim(), this['TAXID'].toString().trim(), this['TAXPERCENTAGE'].toString().trim(), this['TAXVALUE'].toString().trim(), this['MRP'].toString().trim(), '1');
                });
            }
            else {
                addRowinTaxTableEdit('NA', 'NA', 'NA', 0, 0, 0, '0');
            }*/

            /*Tax Details Info New*/
            if (listTax.length > 0) {
                //debugger;
                trtax = $('#taxDetailsGrid');
                HeaderCounttax = $('#taxDetailsGrid thead th').length;
                FooterCounttax = $('#taxDetailsGrid tfoot th').length;
                if (HeaderCounttax == 0) {
                    trtax.append("<thead><tr><th>Sl.No</th><th>Primary Product ID</th><th>Primary Product Batch</th><th>ProductID</th><th>Batch</th><th>TaxID</th><th>TaxPercentage</th><th>Taxvalue</th><th>Tag</th><th>Mrp</th></tr></thead><tbody>");
                }
                if (FooterCounttax == 0) {
                    trtax.append("<tfoot><tr><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr></tfoot><tbody>");
                }

                $.each(listTax, function (index, record) {
                    //debugger;
                    trtax = $('<tr/>');
                    trtax.append("<td style='text-align: center;'><label class='slno' id='lbltaxslno'></label></td>");//0
                    trtax.append("<td >" + this['PRIMARYPRODUCTID'].toString().trim() + "</td>");//1
                    trtax.append("<td >" + this['PRIMARYPRODUCTBATCHNO'].toString().trim() + "</td>");//2
                    trtax.append("<td >" + this['PRODUCTID'].toString().trim() + "</td>");//3
                    trtax.append("<td >" + this['BATCHNO'].toString().trim() + "</td>");//4
                    trtax.append("<td >" + this['TAXID'].toString().trim() + "</td>");//5
                    trtax.append("<td >" + this['TAXPERCENTAGE'].toString().trim() + "</td>");//6
                    trtax.append("<td >" + this['TAXVALUE'].toString().trim() + "</td>");//7
                    trtax.append("<td >" + this['TAG'].toString().trim() + "</td>");//8
                    trtax.append("<td >" + this['MRP'].toString().trim() + "</td>");//9
                    $("#taxDetailsGrid").append(trtax);
                    trtax.append("</tbody>");
                });
            }

            /*Footer Details Info*/
            if (listFooter.length > 0) {
                $.each(listFooter, function (index, record) {
                    $('#RoundOff').val(parseFloat(this['ROUNDOFFVALUE'].toString().trim()).toFixed(2));
                    $('#TaxAmt').val(parseFloat(this['TOTALTAXAMT'].toString().trim()).toFixed(2));
                    $('#NetAmt').val(parseFloat(this['TOTALSALEINVOICEVALUE'].toString().trim()).toFixed(2));
                    $('#NetAmt2').val(parseFloat(this['TOTALSALEINVOICEVALUE'].toString().trim()).toFixed(2));
                    $("#TotalCase").val(this['ACTUALTOTCASE'].toString().trim());
                    $("#TotalGrossWght").val(this['TOTALGROSSWT'].toString().trim());

                });
            }


            /*Combo Product Breakup Info*/
            if (listComboProductDetails.length > 0) {

                $("#comboBreakupGrid").empty();
                tr;
                tr = $('#comboBreakupGrid');
                HeaderCount = $('#comboBreakupGrid thead th').length;
                if (HeaderCount == 0) {
                    tr.append("<thead><tr><th >Primary ID</th><th>Secondary ID</th><th>Qty(Pcs)</th></tr></thead><tbody>");
                }
                $.each(listComboProductDetails, function (index, record) {
                    tr = $('<tr/>');
                    tr.append("<td >" + this['PRIMARYPRODUCTID'].toString().trim() + "</td>");//0
                    tr.append("<td >" + this['SECONDARYPRODUCTID'].toString().trim() + "</td>");//1
                    tr.append("<td >" + this['QTY'].toString().trim() + "</td>");//2
                    $("#comboBreakupGrid").append(tr);
                    tr.append("</tbody>");
                });
            }

            CalculateAmount();

            /*Order Header*/
            $.each(listOrderHeader, function (index, record) {
                //debugger;
                OrderID = this['SALEORDERID'].toString().trim();
                Saleorder.append($("<option></option>").val(this['SALEORDERID']).html(this['REFERENCESALEORDERNO']));
            });
            $("#SaleOrderID").trigger("liszt:updated");

            /*Bind Product*/
            bindCPCProduct(OrderID.trim());
            ChangeProductColour(OrderID.trim(), $('#PSID').val().trim(), $('#hdndispatchID').val().trim());

            
            $("#PRODUCTID").chosen('destroy');
            $("#PRODUCTID").chosen({ width: '390px' });

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

function DeleteInvoice(invoiceid) {
    //debugger;

    $.ajax({
        type: "POST",
        url: "/TranDepot/GTdeleteInvoice",
        data: { InvoiceID: invoiceid },
        dataType: "json",
        async: false,
        success: function (response) {
            var messageid = 0;
            var messagetext = '';
            $.each(response, function (key, item) {
                messageid = item.MessageID;
                messagetext = item.MessageText;
            });
            if (messageid != '0') {
                bindcsdinvoicegrid();
                toastr.success('<b><font color=black>' + messagetext + '</font></b>');
            }
            else {
                toastr.error('<b><font color=black>' + messagetext + '</font></b>');
            }
        }
    });
}

function getqtyinPCS(productid, packsizefromid, deliveredqty, deliveredqtypcs, stockqty, saleorderid, invoiceid,remainingqty) {
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
            var remqty;
            $.each(qtypcs, function (key, item) {
                //debugger;
                deliveredqty = item.DELIVEREDQTY;
                stockqty = item.STOCKQTY;
                invoiceqty = parseFloat(deliveredqty.trim()) + parseFloat(deliveredqtypcs);
                stockqty = parseFloat(stockqty.trim()).toFixed(3);
                remqty = parseFloat(remainingqty);
                $("#hdninvoiceqty").val(invoiceqty);
                if (invoiceqty > stockqty) {
                    returnvalue = 'Invoice qty should not be greater than Stock qty..!';
                }
                else if (invoiceqty > remqty) {
                    returnvalue = 'Invoice qty should not be greater than Remaining qty..!';
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
                //debugger;
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
        data: { Productid: productid.trim() },
        dataType: "json",
        async: false,
        success: function (productType) {
            var type;
            $.each(productType, function (key, item) {
                //debugger;
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

function getGroupStatus(customerid,bsid) {
    var returnvalue = null;
    $.ajax({
        type: "POST",
        url: "/TranDepot/GroupStatus",
        data: { CustomerID: customerid.trim(), BSID: bsid.trim() },
        dataType: "json",
        async: false,
        success: function (groupstatus) {
            var status;
            $.each(groupstatus, function (key, item) {
                //debugger;
                status = item.GROUP_STATUS;
                returnvalue = status;
                return false;
            });
        },
        failure: function (groupstatus) {
            alert(groupstatus.responseText);
        },
        error: function (groupstatus) {
            alert(groupstatus.responseText);
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
                //debugger;
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

function getPriceSchemeDiscount(productid, qty, packsize, customerid, saleorderid, date, bsid, groupid, depotid, mrp) {
    var returnvalue = null;
    $.ajax({
        type: "POST",
        url: "/TranDepot/GetPriceSchemeDiscount",
        data: { Productid: productid.trim(), Qty: qty.trim(), Packsize: packsize.trim(), CustomerID: customerid.trim(), SaleorderID: saleorderid.trim(), Date: date.trim(), BSID: bsid.trim(), GroupID: groupid.trim(), DepotID: depotid.trim(), MRP: mrp.trim() },
        dataType: "json",
        async: false,
        success: function (priceschemediscount) {
            var priceschemevalue;
            var discountvalue;
            var stateid;
            var districtid;
            var zoneid;
            var teritoryid;
            var hsncode;
            $.each(priceschemediscount, function (key, item) {
                //debugger;
                priceschemevalue = item.PRICE_SCHEME_ID_VALUE;
                discountvalue = item.DISCOUNT;
                stateid = item.STATEID;
                districtid = item.DISTRICTID;
                zoneid = item.ZONEID;
                teritoryid = item.TERITORYID;
                hsncode = item.HSNCODE;
                returnvalue = priceschemevalue + '|' + discountvalue + '|' + stateid + '|' + districtid + '|' + zoneid + '|' + teritoryid + '|' + hsncode;
                return false;
            });
        },
        failure: function (priceschemediscount) {
            alert(priceschemediscount.responseText);
        },
        error: function (pricescheme) {
            alert(priceschemediscount.responseText);
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

function itemLedger() {

    $('#productDetailsGrid').empty();
    //ReleaseSession();

    EditDetails(VoucherID);


    $('#btnAddnew').css("display", "none");
    $('#btnsave').css("display", "none");
    $('#btnApprove').css("display", "none");

    $("#dvAdd").find("input, textarea, select,submit").attr("disabled", "disabled");
    $('#divTransferNo').css("display", "");
    $("#btnclose").prop("disabled", false);
    $('#dvAdd').css("display", "");
    $('#dvDisplay').css("display", "none");

    $("#BRID").chosen({
        search_contains: true
    });
    $("#BRID").trigger("chosen:updated");

    $("#CUSTOMERID").prop("disabled", true);
    $("#CUSTOMERID").chosen({
        search_contains: true
    });
    $("#CUSTOMERID").trigger("chosen:updated");

    $("#SaleOrderID").prop("disabled", true);
    $("#SaleOrderID").chosen({
        search_contains: true
    });
    $("#SaleOrderID").trigger("chosen:updated");

    $("#ID").chosen({
        search_contains: true
    });
    $("#ID").trigger("chosen:updated");

    $("#INSURANCE_NO").chosen({
        search_contains: true
    });
    $("#INSURANCE_NO").trigger("chosen:updated");

    $("#WAYBILLID").chosen({
        search_contains: true
    });
    $("#WAYBILLID").trigger("chosen:updated");

    $("#TransportMode").chosen({
        search_contains: true
    });
    $("#TransportMode").trigger("chosen:updated");

    $("#TransporterID").chosen({
        search_contains: true
    });
    $("#TransporterID").trigger("chosen:updated");

    $("#PRODUCTID").val('0');
    $("#PRODUCTID").chosen({
        search_contains: true
    });
    $("#PRODUCTID").trigger("chosen:updated");

    $("#PSID").chosen({
        search_contains: true
    });
    $("#PSID").trigger("chosen:updated");
    $('#ddlBatch').empty();

    $("#InvoiceDate").datepicker("destroy");
}

/*Delete function for Tax grid*/
$(function () {

    $("body").on("click", "#productDetailsGrid #btnBilldelete", function () {
        //debugger;
        var productid;
        var grdproductid;
        var batchno;
        var MfgDate;
        var ExprDate;
        var mfgDate;
        var exprDate;
        var pid;
        var pid2;
        var mfgDate2;
        var exprDate2;
        var deleteflag = 0;
        var freedeleteflag = 0;
        var combodeleteflag = 0;
        var taxdeleteflag = 0;
        var qsguid;
        var qsheader;
        var freeqsguid;
        var freeSchemeID;
        var rowCount;
        var freerowCount;
        var breakuprowCount;
        var taxrowCount;
        var fifoflag = 0;
        var taxpid;
        var taxbatch;
        var resetLoop = false;

        var row = $(this).closest("tr");
        productid = row.find('td:eq(1)').html().trim();
        batchno = row.find('td:eq(12)').html().trim();

        MfgDate = row.find('td:eq(23)').html().trim();
        ExprDate = row.find('td:eq(24)').html().trim();

        qsguid = row.find('td:eq(25)').html().trim();
        qsheader = row.find('td:eq(26)').html().trim();

        if (qsheader == '0') {
            //debugger;
            /*FIFO checking for without quantity scheme start*/
            $('#productDetailsGrid tbody tr').each(function () {
                //debugger;
                pid = $(this).find('td:eq(1)').html().trim();
                mfgDate = $(this).find('td:eq(23)').html().trim();
                exprDate = $(this).find('td:eq(24)').html().trim();
                if (productid == pid) {
                    //debugger;
                    $('#freeDetailsGrid tbody tr').each(function () {
                        //debugger;
                        pid2 = $(this).find('td:eq(2)').html().trim();
                        mfgDate2 = $(this).find('td:eq(19)').html().trim();
                        exprDate2 = $(this).find('td:eq(20)').html().trim();
                        if (productid == pid2) {
                            //debugger;
                            if (MfgDate != mfgDate2 && ExprDate != exprDate2) {
                                //debugger;
                                if (new Date(MfgDate.replace(/(\d{2})\/(\d{2})\/(\d{4})/, "$2/$1/$3")) < new Date(mfgDate2.replace(/(\d{2})\/(\d{2})\/(\d{4})/, "$2/$1/$3"))) {
                                    //debugger;
                                    fifoflag = 1;
                                    resetLoop = true;
                                    return false;
                                }
                                else {
                                    fifoflag = 0;
                                }
                            }
                        }
                    });
                    if (resetLoop == true) {
                        return false;
                    }
                    if (MfgDate != mfgDate && ExprDate != exprDate) {
                        if (new Date(MfgDate.replace(/(\d{2})\/(\d{2})\/(\d{4})/, "$2/$1/$3")) < new Date(mfgDate.replace(/(\d{2})\/(\d{2})\/(\d{4})/, "$2/$1/$3"))) {
                            fifoflag = 1;
                            return false;
                        }
                        else {
                            fifoflag = 0;
                        }
                    }
                }

            });
            /*FIFO checking for without quantity scheme end*/
        }
        if (qsheader == '1') {
            /*FIFO checking for with quantity scheme start*/
            $('#productDetailsGrid tbody tr').each(function () {
                pid = $(this).find('td:eq(1)').html().trim();
                mfgDate = $(this).find('td:eq(23)').html().trim();
                exprDate = $(this).find('td:eq(24)').html().trim();
                if (productid == pid) {
                    if (MfgDate != mfgDate && ExprDate != exprDate) {
                        if (new Date(MfgDate.replace(/(\d{2})\/(\d{2})\/(\d{4})/, "$2/$1/$3")) < new Date(mfgDate.replace(/(\d{2})\/(\d{2})\/(\d{4})/, "$2/$1/$3"))) {
                            fifoflag = 1;
                            return false;
                        }
                        else {
                            fifoflag = 0;
                        }
                    }
                }
            });
            /*FIFO checking for with quantity scheme end*/
        }

        if (fifoflag == 1) {

            toastr.warning('<b><font color=black> Delete should be in FIFO basis..!</font></b>');
            return false;
        }
        else {

            if (confirm("Do you want to delete this item?")) {

                if (qsheader == '1') {
                    $('#freeDetailsGrid tbody tr').each(function () {
                        var freerow = $(this).closest("tr");
                        freeqsguid = $(this).find('td:eq(25)').html().trim();
                        freeSchemeID = $(this).find('td:eq(1)').html().trim();
                        if (qsguid == freeqsguid && freeSchemeID != '0') {
                            freedeleteflag = 1;
                            freerow.remove();
                            RowCountFreeGrid();
                        }
                    });
                }

                $('#taxDetailsGrid tbody tr').each(function () {
                    var taxrow = $(this).closest("tr");
                    taxpid = $(this).find('td:eq(1)').html().trim();
                    taxbatch = $(this).find('td:eq(2)').html().trim();
                    if (taxpid == productid && taxbatch == batchno) {
                        taxdeleteflag = 1;
                        taxrow.remove();
                    }
                });

                /*Combo breakup Delete*/
                $('#comboBreakupGrid tbody tr').each(function () {
                    //debugger;
                    var breakuprow = $(this).closest("tr");
                    var comboID = $(this).find('td:eq(0)').html().trim();
                    if (comboID == productid) {
                        combodeleteflag = 1;
                        breakuprow.remove();
                    }

                });
                /**/

                deleteflag = 1;
                row.remove();

                rowCount = document.getElementById("productDetailsGrid").rows.length - 2;
                if (rowCount <= 0) {
                    $("#productDetailsGrid").empty();
                }

                freerowCount = document.getElementById("freeDetailsGrid").rows.length - 2;
                if (freerowCount <= 0) {
                    $("#freeDetailsGrid").empty();
                }

                breakuprowCount = document.getElementById("comboBreakupGrid").rows.length - 1;
                if (breakuprowCount <= 0) {
                    $("#comboBreakupGrid").empty();
                }

                taxrowCount = document.getElementById("taxDetailsGrid").rows.length - 2;
                if (taxrowCount <= 0) {
                    $("#taxDetailsGrid").empty();
                }

                RowCount();
                CalculateAmount();
                //deleteRowfromTaxTable(productid, batchno);
            }

            ChangeProductColour($('#SaleOrderID').val().trim(), $('#PSID').val().trim(), $('#hdndispatchID').val().trim());

            $('#PRODUCTID').val('0');
            $("#PRODUCTID").chosen({
                search_contains: true
            });
            $("#PRODUCTID").trigger("chosen:updated");

            $('#ddlBatch').empty();
            $('#MRP').val('');
            $('#Rate').val('');
            $('#OrderQty').val('');
            $('#OrderDate').val('');
            $('#DeliveredQty').val('');
            $('#RemainingQty').val('');
            $('#StockQty').val('');

            if ($('#hdndispatchID').val() == '0') {

                if (rowCount > 0 && freerowCount > 0) {
                    $("#CUSTOMERID").prop("disabled", true);
                    $("#CUSTOMERID").chosen({
                        search_contains: true
                    });
                    $("#CUSTOMERID").trigger("chosen:updated");

                    $("#SaleOrderID").prop("disabled", true);
                    $("#SaleOrderID").chosen({
                        search_contains: true
                    });
                    $("#SaleOrderID").trigger("chosen:updated");
                }
                else if (rowCount > 0 && freerowCount <= 0) {
                    $("#CUSTOMERID").prop("disabled", true);
                    $("#CUSTOMERID").chosen({
                        search_contains: true
                    });
                    $("#CUSTOMERID").trigger("chosen:updated");

                    $("#SaleOrderID").prop("disabled", true);
                    $("#SaleOrderID").chosen({
                        search_contains: true
                    });
                    $("#SaleOrderID").trigger("chosen:updated");
                }
                else if (rowCount <= 0 && freerowCount > 0) {
                    $("#CUSTOMERID").prop("disabled", true);
                    $("#CUSTOMERID").chosen({
                        search_contains: true
                    });
                    $("#CUSTOMERID").trigger("chosen:updated");

                    $("#SaleOrderID").prop("disabled", true);
                    $("#SaleOrderID").chosen({
                        search_contains: true
                    });
                    $("#SaleOrderID").trigger("chosen:updated");
                }
                else if (rowCount <= 0 && freerowCount <= 0) {
                    $("#CUSTOMERID").prop("disabled", false);
                    $("#CUSTOMERID").chosen({
                        search_contains: true
                    });
                    $("#CUSTOMERID").trigger("chosen:updated");

                    $("#SaleOrderID").prop("disabled", false);
                    $("#SaleOrderID").chosen({
                        search_contains: true
                    });
                    $("#SaleOrderID").trigger("chosen:updated");
                }
            }
        }
    })

});

/*Delete function for Free grid*/
$(function () {

    $("body").on("click", "#freeDetailsGrid #btnfreedelete", function () {
        var freedeleteflag = 0;
        var freeqsguid;
        var freeSchemeID;
        var grdSchemeproductid;
        var grdSchemebatchno;
        var pid;
        var MfgDate;
        var ExprDate;
        var mfgDate;
        var exprDate;
        var pid2;
        var mfgDate2;
        var exprDate2;
        var fifoflag = 0;
        var resetLoop = false;
        var rowCount;
        var freerowCount;

        var freerow = $(this).closest("tr");
        grdSchemeproductid = freerow.find('td:eq(2)').html().trim();
        grdSchemebatchno = freerow.find('td:eq(4)').html().trim();
        freeSchemeID = freerow.find('td:eq(1)').html().trim();
        freeqsguid = freerow.find('td:eq(25)').html().trim();
        MfgDate = freerow.find('td:eq(19)').html().trim();
        ExprDate = freerow.find('td:eq(20)').html().trim();

        if (freeSchemeID != '0' && freeqsguid != '0') {
            toastr.warning('<b><font color=black>Default scheme item not allowed to delete...!</font></b>');
            return false;
        }
        else if (freeSchemeID == '0' && freeqsguid == '0') {

            /*FIFO checking start*/
            $('#freeDetailsGrid tbody tr').each(function () {
                pid = $(this).find('td:eq(2)').html().trim();
                mfgDate = $(this).find('td:eq(19)').html().trim();
                exprDate = $(this).find('td:eq(20)').html().trim();
                if (grdSchemeproductid == pid) {

                    $('#productDetailsGrid tbody tr').each(function () {
                        pid2 = $(this).find('td:eq(1)').html().trim();
                        mfgDate2 = $(this).find('td:eq(23)').html().trim();
                        exprDate2 = $(this).find('td:eq(24)').html().trim();
                        if (grdSchemeproductid == pid2) {

                            if (MfgDate != mfgDate2 && ExprDate != exprDate2) {

                                if (new Date(MfgDate.replace(/(\d{2})\/(\d{2})\/(\d{4})/, "$2/$1/$3")) < new Date(mfgDate2.replace(/(\d{2})\/(\d{2})\/(\d{4})/, "$2/$1/$3"))) {
                                    fifoflag = 1;
                                    resetLoop = true;
                                    return false;
                                }
                                else {
                                    fifoflag = 0;
                                }
                            }
                        }
                    });
                    if (resetLoop == true) {
                        return false;
                    }
                    if (MfgDate != mfgDate && ExprDate != exprDate) {

                        if (new Date(MfgDate.replace(/(\d{2})\/(\d{2})\/(\d{4})/, "$2/$1/$3")) < new Date(mfgDate.replace(/(\d{2})\/(\d{2})\/(\d{4})/, "$2/$1/$3"))) {
                            fifoflag = 1;
                            return false;
                        }
                        else {
                            fifoflag = 0;
                        }
                    }
                }
            });
            /*FIFO checking end*/
            if (fifoflag == 1) {

                toastr.warning('<b><font color=black> Delete should be in FIFO basis..!</font></b>');
                return false;
            }
            else {
                if (confirm("Do you want to delete this item?")) {
                    freedeleteflag = 1;
                    freerow.remove();

                    rowCount = document.getElementById("productDetailsGrid").rows.length - 2;
                    if (rowCount <= 0) {
                        $("#productDetailsGrid").empty();
                    }

                    freerowCount = document.getElementById("freeDetailsGrid").rows.length - 2;
                    if (freerowCount <= 0) {
                        $("#freeDetailsGrid").empty();
                    }

                    RowCountFreeGrid();
                    CalculateAmount();
                }

                ChangeProductColour($('#SaleOrderID').val().trim(), $('#PSID').val().trim(), $('#hdndispatchID').val().trim());
                $('#PRODUCTID').val('0');
                $("#PRODUCTID").chosen({
                    search_contains: true
                });
                $("#PRODUCTID").trigger("chosen:updated");
                $('#ddlBatch').empty();

                if ($('#hdndispatchID').val() == '0') {

                    if (rowCount > 0 && freerowCount > 0) {
                        $("#CUSTOMERID").prop("disabled", true);
                        $("#CUSTOMERID").chosen({
                            search_contains: true
                        });
                        $("#CUSTOMERID").trigger("chosen:updated");

                        $("#SaleOrderID").prop("disabled", true);
                        $("#SaleOrderID").chosen({
                            search_contains: true
                        });
                        $("#SaleOrderID").trigger("chosen:updated");
                    }
                    else if (rowCount > 0 && freerowCount <= 0) {
                        $("#CUSTOMERID").prop("disabled", true);
                        $("#CUSTOMERID").chosen({
                            search_contains: true
                        });
                        $("#CUSTOMERID").trigger("chosen:updated");

                        $("#SaleOrderID").prop("disabled", true);
                        $("#SaleOrderID").chosen({
                            search_contains: true
                        });
                        $("#SaleOrderID").trigger("chosen:updated");
                    }
                    else if (rowCount <= 0 && freerowCount > 0) {
                        $("#CUSTOMERID").prop("disabled", true);
                        $("#CUSTOMERID").chosen({
                            search_contains: true
                        });
                        $("#CUSTOMERID").trigger("chosen:updated");

                        $("#SaleOrderID").prop("disabled", true);
                        $("#SaleOrderID").chosen({
                            search_contains: true
                        });
                        $("#SaleOrderID").trigger("chosen:updated");
                    }
                    else if (rowCount <= 0 && freerowCount <= 0) {
                        $("#CUSTOMERID").prop("disabled", false);
                        $("#CUSTOMERID").chosen({
                            search_contains: true
                        });
                        $("#CUSTOMERID").trigger("chosen:updated");

                        $("#SaleOrderID").prop("disabled", false);
                        $("#SaleOrderID").chosen({
                            search_contains: true
                        });
                        $("#SaleOrderID").trigger("chosen:updated");
                    }
                }
            }


        }
    })

});

/*Delete function for Popup Free grid*/
$(function () {

    var deleteflag = 0;
    $("body").on("click", "#finalfreeDetailsGrid .gvFinalFreeDelete", function () {

        var row = $(this).closest("tr");
        if (confirm("Do you want to delete this item?")) {
            deleteflag = 1;
            row.remove();
            var rowCount = document.getElementById("finalfreeDetailsGrid").rows.length - 2;
            if (rowCount <= 0) {
                $("#finalfreeDetailsGrid").empty();
            }
            else if (rowCount > 0) {

                CalculateFinalFreeFooter();
            }
            FinalFreeProductBatchDetailsRowCount();
        }


    });
});

/*Edit function for Invoice*/
$(function () {
    var invoiceid;
    $("body").on("click", "#CSDInvoiceGrid .gvCSDEdit", function () {
        var row = $(this).closest("tr");
        invoiceid = row.find('td:eq(1)').html();
        $('#hdndispatchID').val(invoiceid);
        var financestatus = FinanceStatus($('#hdndispatchID').val().trim());
        var dayendstatus = DayendFlag($('#hdndispatchID').val().trim());
        //var transitstatus = TransitStatus($('#hdndispatchID').val().trim());


        if (financestatus != 'na') {
            $('#dvAdd').css("display", "none");
            $('#dvDisplay').css("display", "");
            toastr.info('<b>' + financestatus + '</b>');
            return false;
        }
        else if (dayendstatus != 'na') {
            $('#dvAdd').css("display", "none");
            $('#dvDisplay').css("display", "");
            toastr.info('<b>' + dayendstatus + '</b>');
            return false;
        }
        //else if (transitstatus != 'na') {
        //    $('#dvAdd').css("display", "none");
        //    $('#dvDisplay').css("display", "");
        //    toastr.info('<b>' + transitstatus + '</b>');
        //    return false;
        //}
        else {
            $("#dialog").dialog({
                autoOpen: true,
                modal: true,
                title: "Loading.."
            });
            $("#imgLoader").css("visibility", "visible");
            setTimeout(function () {

                $('#productDetailsGrid').empty();
                $('#freeDetailsGrid').empty();
                $('#taxDetailsGrid').empty();
                //ReleaseSession();

                EditDetails($('#hdndispatchID').val().trim());

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
                $('#divTransferNo').css("display", "");
                $("#FGInvoiceNo").attr("disabled", "disabled");
                $("#GatepassDate").attr("disabled", "disabled");
                $("#InvoiceDate").attr("disabled", "disabled");
                $("#LrGrDate").attr("disabled", "disabled");
                $("#ICDSDate").attr("disabled", "disabled");
                $("#MRP").attr("disabled", "disabled");
                $("#StockQty").attr("disabled", "disabled");
                $("#Rate").attr("disabled", "disabled");
                $("#OrderQty").attr("disabled", "disabled");
                $("#OrderDate").attr("disabled", "disabled");
                $("#DeliveredQty").attr("disabled", "disabled");
                $("#RemainingQty").attr("disabled", "disabled");
                $("#BasicAmt").attr("disabled", "disabled");
                $("#TaxAmt").attr("disabled", "disabled");
                $("#GrossAmt1").attr("disabled", "disabled");
                $("#GrossAmt").attr("disabled", "disabled");
                $("#RoundOff").attr("disabled", "disabled");
                $("#NetAmt").attr("disabled", "disabled");
                $("#NetAmt2").attr("disabled", "disabled");
                $("#CreditLimit").attr("disabled", "disabled");
                $("#ClosingBalance").attr("disabled", "disabled");
                $("#Target").attr("disabled", "disabled");
                $("#Balance").attr("disabled", "disabled");
                $("#AchPercentage").attr("disabled", "disabled");
                $("#InvoiceLimit").attr("disabled", "disabled");
                $("#InvoiceDone").attr("disabled", "disabled");
                $("#InvoiceBalance").attr("disabled", "disabled");
                $("#TotalSchemeAmt").attr("disabled", "disabled");
                $("#TotalDiscountAmt").attr("disabled", "disabled");
                $("#TotalFreeAmt").attr("disabled", "disabled");
                $("#SSMargin").attr("disabled", "disabled");
                $("#SSMarginAmt").attr("disabled", "disabled");
                $("#TotalPcs").attr("disabled", "disabled");
                $("#BRID").attr("disabled", "disabled");


                $('#dvAdd').css("display", "");
                $('#dvDisplay').css("display", "none");


                $("#BRID").chosen('destroy');
                $("#BRID").chosen({
                    search_contains: true
                });
                $("#BRID").trigger("chosen:updated");

                

                $("#CUSTOMERID").prop("disabled", true);
                $("#CUSTOMERID").chosen({
                    search_contains: true
                });
                $("#CUSTOMERID").trigger("chosen:updated");

                $("#SaleOrderID").prop("disabled", true);
                $("#SaleOrderID").chosen({
                    search_contains: true
                });
                $("#SaleOrderID").trigger("chosen:updated");

                $("#PaymentMode").chosen({
                    search_contains: true
                });
                $("#PaymentMode").trigger("chosen:updated");

                $("#Tranportmode").chosen({
                    search_contains: true
                });
                $("#Tranportmode").trigger("chosen:updated");

                $("#TransporterID").chosen({
                    search_contains: true
                });
                $("#TransporterID").trigger("chosen:updated");

                $("#CATID").val('0');
                $("#CATID").chosen({
                    search_contains: true
                });
                $("#CATID").trigger("chosen:updated");

                $("#PRODUCTID").val('0');
                $("#PRODUCTID").chosen({
                    search_contains: true
                });
                $("#PRODUCTID").trigger("chosen:updated");

                $("#PSID").chosen({
                    search_contains: true
                });
                $("#PSID").trigger("chosen:updated");
                $('#ddlBatch').empty();
                $("#InvoiceDate").datepicker("destroy");
                $("#OrderDate").val('');
                $("#OrderQty").val('');
                $("#StockQty").val('');
                $("#DeliveredQty").val('');
                $("#RemainingQty").val('');

                $("#imgLoader").css("visibility", "hidden");
                $("#dialog").dialog("close");
            }, 3);
        }
    })

});

/*View function for Invoice*/
$(function () {
    var invoiceid;
    $("body").on("click", "#CSDInvoiceGrid .gvCSDView", function () {
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

            $('#productDetailsGrid').empty();
            $('#freeDetailsGrid').empty();
            $('#taxDetailsGrid').empty();
            //ReleaseSession();

            EditDetails($('#hdndispatchID').val().trim());

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

            $("#CUSTOMERID").prop("disabled", true);
            $("#CUSTOMERID").chosen({
                search_contains: true
            });
            $("#CUSTOMERID").trigger("chosen:updated");

            $("#SaleOrderID").prop("disabled", true);
            $("#SaleOrderID").chosen({
                search_contains: true
            });
            $("#SaleOrderID").trigger("chosen:updated");

            $("#PaymentMode").chosen({
                search_contains: true
            });
            $("#PaymentMode").trigger("chosen:updated");

            $("#Tranportmode").chosen({
                search_contains: true
            });
            $("#Tranportmode").trigger("chosen:updated");

            $("#TransporterID").chosen({
                search_contains: true
            });
            $("#TransporterID").trigger("chosen:updated");

            $("#CATID").val('0');
            $("#CATID").chosen({
                search_contains: true
            });
            $("#CATID").trigger("chosen:updated");

            $("#PRODUCTID").val('0');
            $("#PRODUCTID").chosen({
                search_contains: true
            });
            $("#PRODUCTID").trigger("chosen:updated");

            $("#PSID").chosen({
                search_contains: true
            });
            $("#PSID").trigger("chosen:updated");
            $('#ddlBatch').empty();
            $("#InvoiceDate").datepicker("destroy");
            $("#OrderDate").val('');
            $("#OrderQty").val('');
            $("#StockQty").val('');
            $("#DeliveredQty").val('');
            $("#RemainingQty").val('');

            $("#imgLoader").css("visibility", "hidden");
            $("#dialog").dialog("close");
        }, 3);
    })

});

/*Delete function for Invoice*/
$(function () {
    //debugger;
    var dispatchid;
    $("body").on("click", "#CSDInvoiceGrid .gvCSDCancel", function () {
        var row = $(this).closest("tr");
        dispatchid = row.find('td:eq(1)').html();
        $('#hdndispatchID').val(dispatchid);
        var financestatus = FinanceStatus($('#hdndispatchID').val().trim());
        var dayendstatus = DayendFlag($('#hdndispatchID').val().trim());
        var transitstatus = TransitStatus($('#hdndispatchID').val().trim());
        if (financestatus != 'na') {
            toastr.warning('<b>Finance approve already done,not allow to cancel...!</b>');
            return false;
        }
        else if (dayendstatus != 'na') {
            toastr.warning('<b>Dayend already done,not allow to cancel...!</b>');
            return false;
        }
        else if (transitstatus != 'na') {
            toastr.warning('<b>Received already done,not allow to cancel...!</b>');
            return false;
        }
        else {
            if (confirm("Do you want to delete this item?")) {
                if (CHECKER == 'FALSE') {
                    //debugger;
                    DeleteInvoice($('#hdndispatchID').val().trim());
                }
                else {
                    toastr.warning('<b>not allow to cancel...!</b>');
                    return false;
                }
            }
        }

    })

});

/*Print Tax Invoice function for Invoice*/
$(function () {
    var invoiceid;
    var acstatus = '';
    $("body").on("click", "#CSDInvoiceGrid .gvCSDPrint", function () {
        var row = $(this).closest("tr");
        invoiceid = row.find('td:eq(1)').html();
        $('#hdndispatchID').val(invoiceid);
        acstatus = PostingStatus($('#hdndispatchID').val().trim());
        if (acstatus == '0') {
            toastr.warning('<b>This invoice is yet to be approved...</b>');
            return false;
        }
        else {
            $("#dialog").dialog({
                autoOpen: true,
                modal: true,
                title: "Loading.."
            });
            $("#imgLoader").css("visibility", "visible");
            setTimeout(function () {

                var url = "http://mcnroeerp.com/mcworld/VIEW/frmPrintPopUpV2.aspx?pid=" + invoiceid + "&BSID=" + BusinessSegment;
                window.open(url, "", "width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100");
                $("#imgLoader").css("visibility", "hidden");
                $("#dialog").dialog("close");
            }, 3);
        }

    })
});

