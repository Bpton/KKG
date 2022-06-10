//#region Developer Info
/*
* For FactoryInvoiceExportGST.cshtml Only
* Developer Name : Avishek Ghosh 
* Start Date     : 01/04/2020
* END Date       : 
 
*/
//#endregion
var CHECKER;
var backdatestatus;
var entryLockstatus;
var BusinessSegment;
var Name;
var VoucherID;
$(document).ready(function () {
    debugger;
    var qs = getQueryStrings();
    if (qs["CHECKER"] != undefined && qs["CHECKER"] != "") {
        CHECKER = qs["CHECKER"].trim();
    }

    if (qs["MENUID"] != undefined && qs["MENUID"] != "") {
        var menuid = qs["MENUID"].trim();
    }

    if (qs["BSID"] != undefined && qs["BSID"] != "") {
        BusinessSegment = qs["BSID"].trim();
    }

    if (qs["Name"] != undefined && qs["Name"] != "") {
        Name = qs["Name"].trim();
    }

    if (qs["VCHID"] != undefined && qs["VCHID"] != "") {
        VoucherID = qs["VCHID"].trim();
    }

    $("#hdnCHECKER").val(CHECKER);
    $("#hdnmenuid").val(menuid);

    finyearChecking();

    if (Name != 'Itemledger') {

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
        $('#divSourceDepot').css("display", "none");
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
        bindsourceDepot();
        bindCountry();
        bindLoadingPort();
        bindDischargePort();
        bindexportinvoicegrid();
        $("#BRID").prop("disabled", true);
    }
    else if (Name == 'Itemledger') {
        bindsourceDepot();
        bindCountry();
        bindLoadingPort();
        bindDischargePort();
        itemLedger();
    }

    $("#btnsearch").click(function (e) {
        debugger;
        bindexportinvoicegrid();
        e.preventDefault();
    })

    $("#btnAddnew").click(function (e) {
        $('#dvAdd').css("display", "");
        $('#dvDisplay').css("display", "none");
        $('#dvProduct').css("display", "none");
        ClearControls();
        ReleaseSession();
        finyearChecking();
        e.preventDefault();
    })

    $("#btnclose").click(function (e) {
        $('#dvAdd').css("display", "none");
        $('#dvDisplay').css("display", "");
        $('#dvProduct').css("display", "none");
        ClearControls();
        ReleaseSession();
        bindexportinvoicegrid();
        e.preventDefault();
    })

    $("#btnAddProduct").click(function (e) {
        $('#dvProduct').css("display", "");
        $('#ddlBatch').empty();
        $("#PRODUCTID").chosen('destroy');
        $("#PSID").chosen('destroy');
        $("#PRODUCTID").chosen({ width: '390px' });
        $("#PSID").chosen({ width: '130px' });
        e.preventDefault();
    })

    $("#btnHideProduct").click(function (e) {
        $('#dvProduct').css("display", "none");
        $('#PRODUCTID').val('0');
        $('#PSID').empty();
        $('#ddlBatch').empty();
        clearafterAdd();
        e.preventDefault();
    })

    $("#ExchangeRate").change(function (e) {
        $("#CountryID").val('0');
        $("#CountryID").chosen({
            search_contains: true
        });
        $("#CountryID").trigger("chosen:updated");

        $("#SaleOrderID").empty();
        $("#SaleOrderID").chosen({
            search_contains: true
        });
        $("#SaleOrderID").trigger("chosen:updated");

        $("#ProformaOrderID").empty();
        $("#ProformaOrderID").chosen({
            search_contains: true
        });
        $("#ProformaOrderID").trigger("chosen:updated");

        $("#CUSTOMERID").empty();
        $("#CUSTOMERID").chosen({
            search_contains: true
        });
        $("#CUSTOMERID").trigger("chosen:updated");

        $('#BankID').empty();
        $("#BankID").chosen({
            search_contains: true
        });
        $("#BankID").trigger("chosen:updated");

        $("#LoadingPortID").val('0');
        $("#LoadingPortID").chosen({
            search_contains: true
        });
        $("#LoadingPortID").trigger("chosen:updated");

        $("#DischargePortID").val('0');
        $("#DischargePortID").chosen({
            search_contains: true
        });
        $("#DischargePortID").trigger("chosen:updated");

        $('#ddlBatch').empty();

        $("#LrGrNo").val('');
        $("#ExportRefNo").val('');
        $("#OtherRefNo").val('');
        $("#FinalDestination").val('');
        $("#ShippingBill").val('');
        $("#ContainerNo").val('');
        $("#VoyNo").val('');
        $("#LCNo").val('');
        $("#LCDate").val('');
        $("#LCBank").val('');
        $("#Branch").val('');
        $("#BankAddress").val('');
        $("#IFSC").val('');
        $("#SwiftCode").val('');
        $("#AccNo").val('');
        $("#Consignee").val('');
        $("#NotifyParty").val('');
        $("#BasicAmt").val('0.00');
        $("#TaxAmt").val('0.00');
        $("#GrossAmt1").val('0.00');
        $("#ProformaAmount").val('0.00');
        $("#GrossAmt").val('0.00');
        $("#AdjustmentAmount").val('0.00');
        $("#RoundOff").val('0.00');
        $("#NetAmt").val('0.00');
        $("#TotalCase").val('0.000');
        $("#TotalPcs").val('0');
        $("#productDetailsGrid").empty();
        e.preventDefault();
    });

    $("#ExchangeRate").keyup(function (e) {
        $("#CountryID").val('0');
        $("#CountryID").chosen({
            search_contains: true
        });
        $("#CountryID").trigger("chosen:updated");

        $("#SaleOrderID").empty();
        $("#SaleOrderID").chosen({
            search_contains: true
        });
        $("#SaleOrderID").trigger("chosen:updated");

        $("#ProformaOrderID").empty();
        $("#ProformaOrderID").chosen({
            search_contains: true
        });
        $("#ProformaOrderID").trigger("chosen:updated");

        $("#CUSTOMERID").empty();
        $("#CUSTOMERID").chosen({
            search_contains: true
        });
        $("#CUSTOMERID").trigger("chosen:updated");

        $('#BankID').empty();
        $("#BankID").chosen({
            search_contains: true
        });
        $("#BankID").trigger("chosen:updated");

        $("#LoadingPortID").val('0');
        $("#LoadingPortID").chosen({
            search_contains: true
        });
        $("#LoadingPortID").trigger("chosen:updated");

        $("#DischargePortID").val('0');
        $("#DischargePortID").chosen({
            search_contains: true
        });
        $("#DischargePortID").trigger("chosen:updated");

        $('#ddlBatch').empty();

        $("#LrGrNo").val('');
        $("#ExportRefNo").val('');
        $("#OtherRefNo").val('');
        $("#FinalDestination").val('');
        $("#ShippingBill").val('');
        $("#ContainerNo").val('');
        $("#VoyNo").val('');
        $("#LCNo").val('');
        $("#LCDate").val('');
        $("#LCBank").val('');
        $("#Branch").val('');
        $("#BankAddress").val('');
        $("#IFSC").val('');
        $("#SwiftCode").val('');
        $("#AccNo").val('');
        $("#Consignee").val('');
        $("#NotifyParty").val('');
        $("#BasicAmt").val('0.00');
        $("#TaxAmt").val('0.00');
        $("#GrossAmt1").val('0.00');
        $("#ProformaAmount").val('0.00');
        $("#GrossAmt").val('0.00');
        $("#AdjustmentAmount").val('0.00');
        $("#RoundOff").val('0.00');
        $("#NetAmt").val('0.00');
        $("#TotalCase").val('0.000');
        $("#TotalPcs").val('0');
        $("#productDetailsGrid").empty();
        e.preventDefault();
    });

    $("#CountryID").change(function (e) {
        debugger;
        $("#ExchangeRate").prop("disabled", false);
        if ($("#ExchangeRate").val() == '') {
            $("#ExchangeRate").focus();
            $("#CountryID").val('0');
            $("#CountryID").chosen({
                search_contains: true
            });
            $("#CountryID").trigger("chosen:updated");
            toastr.warning('<b><font color=black>Please enter exchange rate..!</font></b>');
            return false;
        }
        else {
            debugger;
            if ($("#CountryID").val().trim() != '0') {
                $('#CUSTOMERID').empty();
                $("#CUSTOMERID").chosen({
                    search_contains: true
                });
                $("#CUSTOMERID").trigger("chosen:updated");

                $('#SaleOrderID').empty();
                $("#SaleOrderID").chosen({
                    search_contains: true
                });
                $("#SaleOrderID").trigger("chosen:updated");

                $('#ProformaOrderID').empty();
                $("#ProformaOrderID").chosen({
                    search_contains: true
                });
                $("#ProformaOrderID").trigger("chosen:updated");

                $('#BankID').empty();
                $("#BankID").chosen({
                    search_contains: true
                });
                $("#BankID").trigger("chosen:updated");

                $("#LoadingPortID").val('0');
                $("#LoadingPortID").chosen({
                    search_contains: true
                });
                $("#LoadingPortID").trigger("chosen:updated");

                $("#DischargePortID").val('0');
                $("#DischargePortID").chosen({
                    search_contains: true
                });
                $("#DischargePortID").trigger("chosen:updated");

                $('#ddlBatch').empty();

                $("#LrGrNo").val('');
                $("#ExportRefNo").val('');
                $("#OtherRefNo").val('');
                $("#FinalDestination").val('');
                $("#ShippingBill").val('');
                $("#ContainerNo").val('');
                $("#VoyNo").val('');
                $("#LCNo").val('');
                $("#LCDate").val('');
                $("#LCBank").val('');
                $("#Branch").val('');
                $("#BankAddress").val('');
                $("#IFSC").val('');
                $("#SwiftCode").val('');
                $("#AccNo").val('');
                $("#Consignee").val('');
                $("#NotifyParty").val('');
                $("#BasicAmt").val('0.00');
                $("#TaxAmt").val('0.00');
                $("#GrossAmt1").val('0.00');
                $("#ProformaAmount").val('0.00');
                $("#GrossAmt").val('0.00');
                $("#AdjustmentAmount").val('0.00');
                $("#RoundOff").val('0.00');
                $("#NetAmt").val('0.00');
                $("#TotalCase").val('0.000');
                $("#TotalPcs").val('0');
                $("#productDetailsGrid").empty();

                bindExportSaleOrder($('#CountryID').val().trim(), '0','0');
               
            }
            else {
               
                $('#CUSTOMERID').empty();
                $("#CUSTOMERID").chosen({
                    search_contains: true
                });
                $("#CUSTOMERID").trigger("chosen:updated");

                $('#SaleOrderID').empty();
                $("#SaleOrderID").chosen({
                    search_contains: true
                });
                $("#SaleOrderID").trigger("chosen:updated");

                $('#ProformaOrderID').empty();
                $("#ProformaOrderID").chosen({
                    search_contains: true
                });
                $("#ProformaOrderID").trigger("chosen:updated");

                $('#BankID').empty();
                $("#BankID").chosen({
                    search_contains: true
                });
                $("#BankID").trigger("chosen:updated");

                $("#LoadingPortID").val('0');
                $("#LoadingPortID").chosen({
                    search_contains: true
                });
                $("#LoadingPortID").trigger("chosen:updated");

                $("#DischargePortID").val('0');
                $("#DischargePortID").chosen({
                    search_contains: true
                });
                $("#DischargePortID").trigger("chosen:updated");

                $('#ddlBatch').empty();

                $("#LrGrNo").val('');
                $("#ExportRefNo").val('');
                $("#OtherRefNo").val('');
                $("#FinalDestination").val('');
                $("#ShippingBill").val('');
                $("#ContainerNo").val('');
                $("#VoyNo").val('');
                $("#LCNo").val('');
                $("#LCDate").val('');
                $("#LCBank").val('');
                $("#Branch").val('');
                $("#BankAddress").val('');
                $("#IFSC").val('');
                $("#SwiftCode").val('');
                $("#AccNo").val('');
                $("#Consignee").val('');
                $("#NotifyParty").val('');
                $("#BasicAmt").val('0.00');
                $("#TaxAmt").val('0.00');
                $("#GrossAmt1").val('0.00');
                $("#ProformaAmount").val('0.00');
                $("#GrossAmt").val('0.00');
                $("#AdjustmentAmount").val('0.00');
                $("#RoundOff").val('0.00');
                $("#NetAmt").val('0.00');
                $("#TotalCase").val('0.000');
                $("#TotalPcs").val('0');
                $("#productDetailsGrid").empty();
            }
        }
        e.preventDefault();
    });

    

    $('#SaleOrderID').change(function (e) {
        debugger;
        $("#ExchangeRate").prop("disabled", false);
        if ($('#SaleOrderID').val().trim() != '0') {
            bindCustomer($('#SaleOrderID').val().trim());
            bindProforma($('#SaleOrderID').val().trim());
            $("#BasicAmt").val('0.00');
            $("#TaxAmt").val('0.00');
            $("#GrossAmt1").val('0.00');
            $("#ProformaAmount").val('0.00');
            $("#GrossAmt").val('0.00');
            $("#AdjustmentAmount").val('0.00');
            $("#RoundOff").val('0.00');
            $("#NetAmt").val('0.00');
            $("#TotalCase").val('0.000');
            $("#TotalPcs").val('0');
            debugger;
            var count = $("#ProformaOrderID option").length; 
            if (count > 0) {
                if (count == 1) {
                    bindtaxcount();
                    bindproformadetails($("#ProformaOrderID").val().trim());
                    bindlcdetails($("#SaleOrderID").val().trim(), $("#ProformaOrderID").val().trim(), $("#CUSTOMERID").val().trim());
                    bindFgProduct(BusinessSegment, $('#hdndispatchID').val().trim());
                    bindProformaAmount($("#ProformaOrderID").val().trim());
                    LoadProformaDetails($("#SaleOrderID").val().trim(), $("#BRID").val().trim(), $("#ProformaOrderID").val().trim(), $("#CUSTOMERID").val().trim(), $("#ExchangeRate").val().trim());
                    
                }
            }
        }
        else {
            $("#CUSTOMERID").empty();
            $("#CUSTOMERID").chosen({
                search_contains: true
            });
            $("#CUSTOMERID").trigger("chosen:updated");

            $("#ProformaOrderID").empty();
            $("#ProformaOrderID").chosen({
                search_contains: true
            });
            $("#ProformaOrderID").trigger("chosen:updated");

            $('#BankID').empty();
            $("#BankID").chosen({
                search_contains: true
            });
            $("#BankID").trigger("chosen:updated");

            $("#LoadingPortID").val('0');
            $("#LoadingPortID").chosen({
                search_contains: true
            });
            $("#LoadingPortID").trigger("chosen:updated");

            $("#DischargePortID").val('0');
            $("#DischargePortID").chosen({
                search_contains: true
            });
            $("#DischargePortID").trigger("chosen:updated");

            $('#ddlBatch').empty();

            $("#LrGrNo").val('');
            $("#ExportRefNo").val('');
            $("#OtherRefNo").val('');
            $("#FinalDestination").val('');
            $("#ShippingBill").val('');
            $("#ContainerNo").val('');
            $("#VoyNo").val('');
            $("#LCNo").val('');
            $("#LCDate").val('');
            $("#LCBank").val('');
            $("#Branch").val('');
            $("#BankAddress").val('');
            $("#IFSC").val('');
            $("#SwiftCode").val('');
            $("#AccNo").val('');
            $("#Consignee").val('');
            $("#NotifyParty").val('');
            $("#BasicAmt").val('0.00');
            $("#TaxAmt").val('0.00');
            $("#GrossAmt1").val('0.00');
            $("#ProformaAmount").val('0.00');
            $("#GrossAmt").val('0.00');
            $("#AdjustmentAmount").val('0.00');
            $("#RoundOff").val('0.00');
            $("#NetAmt").val('0.00');
            $("#TotalCase").val('0.000');
            $("#TotalPcs").val('0');
            $("#productDetailsGrid").empty();
        }
        e.preventDefault();
    })

    $('#PRODUCTID').change(function (e) {
        debugger;
        var product = $("#PRODUCTID").val();
        if (product != '0') {
            $('#ddlBatch').empty();
            bindPacksize();
        }
        else {
            $('#ddlBatch').empty();
            $('#PSID').empty();
            $("#PSID").chosen({
                search_contains: true
            });
            $("#PSID").trigger("chosen:updated");
        }
        $("#PSID").focus();
        $('#PSID').trigger('chosen:updated');
        clearafterAdd();
        e.preventDefault();
    })

    $('#PSID').change(function (e) {
        var product = $("#PRODUCTID").val();
        var packsize = $("#PSID").val();
        if (product != '0' && packsize != '0') {
            bindbatchno();
        }
        else {
            $('#ddlBatch').empty();
        }
        $("#ddlBatch").focus();
        clearafterAdd();
        e.preventDefault();
    })

    $('#ddlBatch').change(function (e) {
        getBatchDetails();
        $("#InvoiceQty").focus();
        e.preventDefault();
    })

    $("#btnadd").click(function (e) {
        debugger;
        if ($("#chkfree").prop('checked') == false) {
            if ($('#CUSTOMERID').val() == '0') {
                toastr.warning('<b><font color=black>Please select customer..!</font></b>');
                return false;
            }
            else if ($('#SaleOrderID').val() == '0') {
                toastr.warning('<b><font color=black>Please select Order..!</font></b>');
                return false;
            }

            else if ($('#Rate').val() == '') {
                toastr.warning('<b><font color=black>Please check Rate..!</font></b>');
                return false;
            }
            else if ($('#MRP').val() == '') {
                toastr.warning('<b><font color=black>Please check MRP..!</font></b>');
                return false;
            }
            else if ($('#StockQty').val() == '') {
                toastr.warning('<b><font color=black>Please check Stock quanity..!</font></b>');
                return false;
            }
            else if ($('#InvoiceQty').val() == '') {
                toastr.warning('<b><font color=black>Please enter invoice quantity..!</font></b>');
                return false;
            }
            else if (parseFloat($('#InvoiceQty').val()) == 0) {
                toastr.warning('<b><font color=black>Invoice quantity should not be 0..!</font></b>');
                return false;
            }
            else if (parseFloat($('#InvoiceQty').val()) > parseFloat($('#RemainingQty').val())) {
                toastr.warning('<b><font color=black>Invoice quantity should not greater than Order quantity..!</font></b>');
                return false;
            }
            else if (parseFloat($('#InvoiceQty').val()) > parseFloat($('#StockQty').val())) {
                toastr.warning('<b><font color=black>Invoice quantity should not greater than Stock quantity..!</font></b>');
                return false;
            }
            else if (parseFloat($('#Rate').val().trim()) <= 0) {
                toastr.warning('<b><font color=black>Rate should not be 0..!</font></b>');
                return false;
            }
            else {
                addProduct();
            }
        }
        else if ($("#chkfree").prop('checked') == true) {
            if ($('#CUSTOMERID').val() == '0') {
                toastr.warning('<b><font color=black>Please select customer..!</font></b>');
                return false;
            }
            else if ($('#SaleOrderID').val() == '0') {
                toastr.warning('<b><font color=black>Please select Order..!</font></b>');
                return false;
            }
            else if ($('#PSID').val() != 'B9F29D12-DE94-40F1-A668-C79BF1BF4425') {
                toastr.warning('<b><font color=black>Packsize should be pcs..!</font></b>');
                return false;
            }
            else if ($('#Rate').val() == '') {
                toastr.warning('<b><font color=black>Please check Rate..!</font></b>');
                return false;
            }
            else if ($('#MRP').val() == '') {
                toastr.warning('<b><font color=black>Please check MRP..!</font></b>');
                return false;
            }
            else if ($('#StockQty').val() == '') {
                toastr.warning('<b><font color=black>Please check Stock quanity..!</font></b>');
                return false;
            }
            else if ($('#InvoiceQty').val() == '') {
                toastr.warning('<b><font color=black>Please enter invoice quantity..!</font></b>');
                return false;
            }
            else if (parseFloat($('#InvoiceQty').val()) == 0) {
                toastr.warning('<b><font color=black>Invoice quantity should not be 0..!</font></b>');
                return false;
            }
            else if (parseFloat($('#InvoiceQty').val()) > parseFloat($('#RemainingQty').val())) {
                toastr.warning('<b><font color=black>Invoice quantity should not greater than Order quantity..!</font></b>');
                return false;
            }
            else if (parseFloat($('#InvoiceQty').val()) > parseFloat($('#StockQty').val())) {
                toastr.warning('<b><font color=black>Invoice quantity should not greater than Stock quantity..!</font></b>');
                return false;
            }
            else {
                addProduct();
            }
        }
        e.preventDefault();
    })

    $("#btnsave").click(function (e) {
        debugger;

        entryLockstatus = bindlockdateflag($("#InvoiceDate").val());
        if (entryLockstatus == '0') {
            toastr.warning('<b><font color=black>Entry date is locked,please contact with admin...!</font></b>');
            return false;
        }
        else {
            debugger;
            var itemrowCount = document.getElementById("productDetailsGrid").rows.length - 1;
            if ($("#Tranportmode").val() == '0') {
                toastr.warning('<b><font color=black>Please select Carriage By..!</font></b>');
                return false;
            }
            else if ($("#ExchangeRate").val() == '') {
                toastr.warning('<b><font color=black>Please enter Exchange Rate..!</font></b>');
                return false;
            }
            else if ($("#CountryID").val() == '0') {
                toastr.warning('<b><font color=black>Please select Country..!</font></b>');
                return false;
            }
            else if ($("#SaleOrderID").val() == '0') {
                toastr.warning('<b><font color=black>Please select Sale Order..!</font></b>');
                return false;
            }
            else if ($("#ProformaOrderID").val() == '0') {
                toastr.warning('<b><font color=black>Please select Proforma..!</font></b>');
                return false;
            }
            else if ($("#CUSTOMERID").val() == '0') {
                toastr.warning('<b><font color=black>Please select Customer..!</font></b>');
                return false;
            }
            else if ($("#DischargePortID").val() == '0') {
                toastr.warning('<b><font color=black>Please select Discharge port..!</font></b>');
                return false;
            }
            else if ($("#LrGrNo").val() == '') {
                toastr.warning('<b><font color=black>Please enter LR/BL no..!</font></b>');
                return false;
            }
            else if ($("#ExportRefNo").val() == '') {
                toastr.warning('<b><font color=black>Please enter Exporter(s) Ref..!</font></b>');
                return false;
            }
            else if (itemrowCount <= 0) {
                toastr.warning('<b><font color=black>Please add Product..!</font></b>');
                return false;
            }
            else if ($("#AmountInWords").val() == '') {
                toastr.warning('<b><font color=black>Please enter Amount in word(s)..!</font></b>');
                return false;
            }
            else {
                SaveInvoice();
            }
        }
        e.preventDefault();
    })

    $("#btnclosepackinglist").click(function (e) {
        $('#PackInvoiceNo').val('');
        $('#PackCaseQty').val('');
        $("#DescPackages").val('');
        $("#packinglistGrid").empty();
        $("#dvpackinglist").dialog("close");
        e.preventDefault();
    })

    $("#btnsavepackinglist").click(function (e) {
        checkPackingList();
        e.preventDefault();
    })

    $("#dvpackinglist").dialog({
        autoOpen: false,
        maxWidth: 1100,
        maxHeight: 480,
        width: 1100,
        height: 480,
        resizable: false,
        draggable: false,
        modal: true,
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

function finyearChecking() {

    debugger;
    //fin yr check
    var currentdt;
    var frmdate;
    var todate;
    $.ajax({
        type: "POST",
        url: "/Tranfac/finyrchk",
        data: '{}',
        async: false,
        dataType: "json",
        success: function (response) {
            debugger;
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

    backdatestatus = bindbackdateflag('4212');
    if (parseInt(backdatestatus) > 0) {
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
            buttonImage: 'http://jqueryui.com/resources/demos/datepicker/images/calendar.gif'
        });
        $(".ui-datepicker-trigger").mouseover(function () {
            $(this).css('cursor', 'pointer');
        });
    }
    else {
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
    }

    //$("#InvoiceDate").datepicker({
    //    changeMonth: true,
    //    changeYear: true,
    //    showButtonPanel: true,
    //    selectCurrent: true,
    //    todayBtn: "linked",
    //    showAnim: "slideDown",
    //    yearRange: "-3:+0",
    //    maxDate: 'today',
    //    dateFormat: "dd/mm/yy",
    //    showOn: 'button',
    //    buttonText: 'Show Date',
    //    buttonImageOnly: true,
    //    maxDate: new Date(currentdt),
    //    minDate: new Date(currentdt),
    //    buttonImage: 'http://jqueryui.com/resources/demos/datepicker/images/calendar.gif'
    //});
    //$(".ui-datepicker-trigger").mouseover(function () {
    //    $(this).css('cursor', 'pointer');
    //});


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
    $("#ShippingDate").datepicker({
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

function bindsourceDepot() {
    var SourceDepot = $("#BRID");
    $.ajax({
        type: "POST",
        url: "/tranfac/GetSourceDepot",
        data: '{}',
        async: false,
        dataType: "json",

        success: function (response) {
            SourceDepot.empty();
            $.each(response, function () {
                SourceDepot.append($("<option></option>").val(this['BRID']).html(this['BRNAME']));
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

function bindCountry() {
    var Country = $("#CountryID");
    $.ajax({
        type: "POST",
        url: "/tranfac/GetCountry",
        data: '{}',
        async: false,
        dataType: "json",
        success: function (response) {
            Country.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(response, function () {
                Country.append($("<option></option>").val(this['COUNTRYID']).html(this['COUNTRYNAME']));
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

function bindLoadingPort() {
    var LoadingPort = $("#LoadingPortID");
    $.ajax({
        type: "POST",
        url: "/tranfac/GetLoadingPort",
        data: '{}',
        async: false,
        dataType: "json",
        success: function (response) {
            LoadingPort.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(response, function () {
                LoadingPort.append($("<option></option>").val(this['LoadingPortID']).html(this['LoadingPortName']));
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

function bindDischargePort() {
    var DischargePort = $("#DischargePortID");
    $.ajax({
        type: "POST",
        url: "/tranfac/GetDischargePort",
        data: '{}',
        async: false,
        dataType: "json",
        success: function (response) {
            DischargePort.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(response, function () {
                DischargePort.append($("<option></option>").val(this['DischargePortID']).html(this['DischargePortName']));
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

function bindCustomer(saleorderID) {
    var Customer = $("#CUSTOMERID");
    $.ajax({
        type: "POST",
        url: "/tranfac/ExportCustomer",
        data: { SaleorderID: saleorderID},
        async: false,
        dataType: "json",
        success: function (response) {
            Customer.empty();
            $.each(response, function () {
                Customer.append($("<option></option>").val(this['CUSTOMERID']).html(this['CUSTOMERNAME']));
            });
            $("#CUSTOMERID").chosen({
                search_contains: true
            });
            $("#CUSTOMERID").trigger("chosen:updated");
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function bindProforma(saleorderID) {
    var Proforma = $("#ProformaOrderID");
    $.ajax({
        type: "POST",
        url: "/tranfac/ExportProforma",
        data: { SaleorderID: saleorderID },
        async: false,
        dataType: "json",
        success: function (response) {
            Proforma.empty();
            $.each(response, function () {
                Proforma.append($("<option></option>").val(this['PROFORMAINVOICEID']).html(this['PROFORMAINVOICENO']));
            });
            $("#ProformaOrderID").chosen({
                search_contains: true
            });
            $("#ProformaOrderID").trigger("chosen:updated");
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function bindproformadetails(proformaid) {
    var Bank = $("#BankID");
    $.ajax({
        type: "POST",
        url: "/tranfac/GetProformaDetails",
        data: { ProformaID: proformaid.trim()},
        dataType: "json",
        async: false,
        success: function (proformalist) {
            //alert(JSON.stringify(tradingorderlist));
            Bank.empty();
            if (proformalist.length > 0) {
                $.each(proformalist, function (key, item) {
                    debugger;
                    $("#ExportRefNo").val(proformalist[0].EXPORTERREFNO);
                    $("#OtherRefNo").val(proformalist[0].OTHERREFNO);
                    $("#FinalDestination").val(proformalist[0].FINALDESTINATION);
                    $("#Consignee").val(proformalist[0].CONSIGNEE);
                    $("#NotifyParty").val(proformalist[0].NOTIFYPARTIES);
                    $("#Branch").val(proformalist[0].BRANCHNAME);
                    $("#BankAddress").val(proformalist[0].BANKADDRESS);
                    $("#IFSC").val(proformalist[0].IFSCODE);
                    $("#SwiftCode").val(proformalist[0].SWIFTCODE);
                    $("#AccNo").val(proformalist[0].ACCOUNTNO);
                    Bank.append($("<option></option>").val(this['BANKID']).html(this['BANKNAME']));
                });
            }
            else {
                Bank.empty();
                $("#ExportRefNo").val('');
                $("#OtherRefNo").val('');
                $("#FinalDestination").val('');
                $("#Consignee").val('');
                $("#NotifyParty").val('');
                $("#Branch").val('');
                $('#BankAddress').val('');
                $('#IFSC').val('');
                $('#SwiftCode').val('');
                $('#AccNo').val('');
            }
            $("#BankID").chosen({
                search_contains: true
            });
            $("#BankID").trigger("chosen:updated");
        },
        failure: function (proformalist) {
            alert(proformalist.responseText);
        },
        error: function (proformalist) {
            alert(proformalist.responseText);
        }
    });
}

function bindlcdetails(saleorderid,proformaid,customerid) {
    $.ajax({
        type: "POST",
        url: "/tranfac/GetLCDetails",
        data: { SaleorderID: saleorderid.trim(), ProformaID: proformaid.trim(), CustomerID: customerid.trim() },
        dataType: "json",
        async: false,
        success: function (lclist) {
            //alert(JSON.stringify(tradingorderlist));
            if (lclist.length > 0) {
                $.each(lclist, function (key, item) {
                    debugger;
                    $("#LCNo").val(lclist[0].LCNO.trim());
                    if (lclist[0].LCDATE.trim() == '01/01/1900') {
                        $("#LCDate").val('');
                    }
                    else {
                        $("#LCDate").val(lclist[0].LCDATE.trim());
                    }
                    $("#LCBank").val(lclist[0].LCBANK.trim());
                });
            }
            else {
                $("#LCNo").val('');
                $("#LCDate").val('');
                $("#LCBank").val('');
            }
        },
        failure: function (lclist) {
            alert(lclist.responseText);
        },
        error: function (lclist) {
            alert(lclist.responseText);
        }
    });
}

function getProformaTax(productid, taxname, invoicedate) {
    //debugger;
    var returnValue = null;
    $.ajax({
        type: "POST",
        url: "/tranfac/GetProformaTax",
        data: { ProductID: productid.trim(), TaxName: taxname.trim(), InvoiceDate: invoicedate.trim() },
        dataType: "json",
        async: false,
        success: function (proformatax) {
            //debugger;
            //alert(JSON.stringify(tradingorderlist));
            var taxpercentage;
            if (proformatax.length > 0) {
                $.each(proformatax, function (key, item) {
                    taxpercentage = proformatax[0].HSNTAX.trim();
                    returnValue = taxpercentage;
                    return false;
                });
            }
            else {
                returnValue = '0';
            }
        },
        failure: function (proformatax) {
            alert(proformatax.responseText);
        },
        error: function (proformatax) {
            alert(proformatax.responseText);
        }
    });
    return returnValue;
}

function bindExportSaleOrder(countryid, invoiceID, orderno) {
    debugger;
    var SaleOrder = $("#SaleOrderID");
    $.ajax({
        type: "POST",
        url: "/tranfac/ExportSaleOrder",
        data: { CountryID: countryid},
        async: false,
        dataType: "json",
        success: function (response) {
            debugger;
            //alert(JSON.stringify(response));
            SaleOrder.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(response, function () {
                SaleOrder.append($("<option></option>").val(this['SALEORDERID']).html(this['SALEORDERNO']));
            });
            if (invoiceID != '0') {
                debugger;
                $("#SaleOrderID").val(orderno).trigger("liszt:updated");
                $("#SaleOrderID").prop("disabled", true);
            }
            else {
                $("#SaleOrderID").removeAttr("disabled");
                $("#SaleOrderID").chosen({
                search_contains: true
                });
                $("#SaleOrderID").trigger("chosen:updated");
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

function bindshippingAddress() {
    $.ajax({
        type: "POST",
        url: "/tranfac/fgShippingAddress",
        data: { CustomerID: $('#CUSTOMERID').val().trim()},
        async:false,
        dataType: "json",
        success: function (shipping) {
            //alert(JSON.stringify(shipping));
            $.each(shipping, function (key, item) {
                $("#ShippingAddress").val(item.ADDRESS);
                
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

function bindFgProduct(bsid,invoiceid) {
    var Product = $("#PRODUCTID");
    $.ajax({
        type: "POST",
        url: "/tranfac/GetExportProduct",
        data: { BSID: bsid, SaleinvoiceID: invoiceid },
        async: false,
        dataType: "json",
        success: function (response) {
            //alert(JSON.stringify(shipping));
            Product.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(response, function () {
                Product.append($("<option></option>").val(this['PRODUCTID']).html(this['PRODUCTNAME']));
            });
            $("#PRODUCTID").chosen({
                search_contains: true
            });
            $("#PRODUCTID").trigger("chosen:updated");
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function bindProformaAmount(proformaid) {
    $.ajax({
        type: "POST",
        url: "/tranfac/GetProformaAmountDetails",
        data: { ProformaID: proformaid},
        async: false,
        dataType: "json",
        success: function (response) {
            var listprfmvalue = response.ProformaValueDataset.varProformaAmount;
            var listadjvalue = response.ProformaValueDataset.varAdjustmentAmount;
            $.each(listprfmvalue, function (index, record) {
                $('#ProformaAmount').val(this['NETAMOUNT'].toString().trim());
            });
            $.each(listadjvalue, function (index, record) {
                $('#AdjustmentAmount').val(this['ADJAMOUNT'].toString().trim());
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
    debugger;
    var Packsize = $("#PSID");
    $.ajax({
        type: "POST",
        url: "/tranfac/GetPacksize",
        data: { ProductID: $('#PRODUCTID').val(), Type:'FG'},
        async: false,
        dataType: "json",
        success: function (response) {
            Packsize.empty().append('<option selected="selected" value="0">Select Packsize</option>');
            $.each(response, function () {
                Packsize.append($("<option></option>").val(this['PSID']).html(this['PSNAME']));
            });
            $("#PSID").chosen({
                search_contains: true
            });
            $("#PSID").trigger("chosen:updated");
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function bindtaxcount() {
    debugger;
    $.ajax({
        type: "POST",
        url: "/tranfac/GetInvoiceTaxcount",
        data: { MenuID: $('#hdnmenuid').val(), Flag: '1', DepotID: $('#BRID').val(), ProductID: '0', CustomerID: $('#CUSTOMERID').val(), Date: $('#InvoiceDate').val() },
        dataType: "json",
        async: false,
        success: function (fgtaxcount) {
            debugger;
            if (fgtaxcount.length > 0) {
                if (fgtaxcount.length == 1) {
                    $.each(fgtaxcount, function (key, item) {
                        debugger;
                        $("#hdntaxcount").val('1');
                        $("#hdntaxnameIGST").val(fgtaxcount[0].TAXNAME);
                        $("#hdntaxpercentage").val(fgtaxcount[0].TAXPERCENTAGE);
                        $("#hdnrelatedto").val(fgtaxcount[0].TAXRELATEDTO);
                    });
                }
                else if (fgtaxcount.length == 2) {
                    debugger;
                    $.each(fgtaxcount, function (key, item) {
                        debugger;
                        $("#hdntaxcount").val('2');
                        $("#hdntaxnameCGST").val(fgtaxcount[0].TAXNAME);
                        $("#hdntaxnameSGST").val(fgtaxcount[1].TAXNAME);
                        $("#hdntaxpercentage").val(fgtaxcount[0].TAXPERCENTAGE);
                        $("#hdnrelatedto").val(fgtaxcount[0].TAXRELATEDTO);
                    });
                }
                else if (fgtaxcount.length == 3) {
                    debugger;
                    $.each(fgtaxcount, function (key, item) {
                        debugger;
                        $("#hdntaxcount").val('3');
                        $("#hdntaxnameCGST").val(fgtaxcount[0].TAXNAME);
                        $("#hdntaxnameSGST").val(fgtaxcount[1].TAXNAME);
                        $("#hdntaxnameTCS").val(fgtaxcount[2].TAXNAME);
                        $("#hdntaxpercentage").val(fgtaxcount[0].TAXPERCENTAGE);
                        $("#hdnrelatedto").val(fgtaxcount[0].TAXRELATEDTO);
                    });
                }
            }
            else {
                $("#hdntaxcount").val(0);
                $("#hdntaxnameIGST").val('');
                $("#hdntaxnameCGST").val('');
                $("#hdntaxnameSGST").val('');
                $("#hdntaxnameTCS").val('');
            }
        },
        failure: function (fgtaxcount) {
            alert(fgtaxcount.responseText);
        },
        error: function (fgtaxcount) {
            alert(fgtaxcount.responseText);
        }
    });

}

function GetTaxOnEdit(invoiceid, taxid, productid, batchno) {
    debugger;
    var returnValue = null;
    $.ajax({
        type: "POST",
        url: "/tranfac/GetInvoiceTaxOnEdit",
        data: { InvoiceID: invoiceid, TaxID: taxid, ProductID: productid, BatchNo: batchno },
        dataType: "json",
        async: false,
        success: function (invoicetaxonedit) {
            debugger;
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
                $("#hdntaxnameTCS").val('');
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

function bindorderdetails() {

    $.ajax({
        type: "POST",
        url: "/tranfac/GetExportOrderQtyDetails",
        data: { InvoiceID: $('#hdndispatchID').val().trim(), CustomerID: $('#CUSTOMERID').val().trim(), SaleOrderID: $('#SaleOrderID').val().trim(), ProductID: $('#PRODUCTID').val().trim(), DepotID: $('#BRID').val().trim(), PacksizeID: $('#PSID').val().trim(), BSID: BusinessSegment },
        dataType: "json",
        async: false,
        success: function (orderlist) {
            if (orderlist.length > 0) {
                $.each(orderlist, function (key, item) {
                    if ($('#PSID').val().trim() == '1970C78A-D062-4FE9-85C2-3E12490463AF') {
                        $("#Rate").val(item.RATE);
                        $("#OrderQty").val(item.ORDERQTY);
                        $("#DeliveredQty").val(item.DESPATCHQTY);
                        $("#RemainingQty").val(item.REMAININGQTY);
                    }
                    else {
                        $("#Rate").val(item.RATE);
                        $("#OrderQty").val(item.ORDERQTYPCS);
                        $("#DeliveredQty").val(item.DESPATCHQTYPCS);
                        $("#RemainingQty").val(item.REMAININGQTYPCS);
                    }
                });
            }
            else {
                $("#Rate").val('');
                $("#MRP").val('');
                $("#OrderQty").val('');
                $("#DeliveredQty").val('');
                $("#RemainingQty").val('');
                $("#StockQty").val('');
            }
        },
        failure: function (orderlist) {
            alert(orderlist.responseText);
        },
        error: function (orderlist) {
            alert(orderlist.responseText);
        }
    });
}

function checkPackingList() {
    if ($('#packinglistGrid').length) {
        $('#packinglistGrid').each(function () {
            var flag = true;
            var arraydetails = [];
            var count = $('#packinglistGrid tbody tr').length;
            $('#packinglistGrid tbody tr').each(function () {
                var packingdetail = {};
                var startgrd = $(this).find('#txtstart').val();
                packingdetail.START = startgrd;
                arraydetails.push(packingdetail);
            });
            var jsonpackingobj = {};
            jsonpackingobj.packingListDetails = arraydetails;
            for (i = 0; i < jsonpackingobj.packingListDetails.length; i++) {
                if (jsonpackingobj.packingListDetails[i].START.trim() == '' || jsonpackingobj.packingListDetails[i].START.trim() == '0') {
                    flag = false;
                    break;
                }
            }
            if (flag != true) {
                $("#dvpackinglist").dialog("open");
                toastr.error('Start position should not be 0 or blank...!');
                return false;
            }
            else {
                SavePackingList();
            }
        })
    }
    else {
        toastr.warning('Packing list not available...!');
        return false;
    }
}

function addProduct() {
    if ($("#chkfree").prop('checked') == false) {
        if ($('#productDetailsGrid').length) {

            $('#productDetailsGrid').each(function () {
                //debugger;
                var exists = false;
                var arraydetails = [];
                var count = $('#productDetailsGrid tbody tr').length;
                $('#productDetailsGrid tbody tr').each(function () {
                    var dispatchdetail = {};
                    var productidgrd = $(this).find('td:eq(5)').html().trim();
                    var batchgrd = $(this).find('td:eq(8)').html().trim();
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
    else if ($("#chkfree").prop('checked') == true) {
        if ($('#freeDetailsGrid').length) {

            $('#freeDetailsGrid').each(function () {
                //debugger;
                var exists = false;
                var freearraydetails = [];
                var count = $('#freeDetailsGrid tbody tr').length;
                $('#freeDetailsGrid tbody tr').each(function () {
                    var freedispatchdetail = {};
                    var freeproductidgrd = $(this).find('td:eq(5)').html().trim();
                    var freebatchgrd = $(this).find('td:eq(8)').html().trim();
                    freedispatchdetail.PRODUCTID = freeproductidgrd;
                    freedispatchdetail.BATCHNO = freebatchgrd;
                    freearraydetails.push(freedispatchdetail);
                });

                var jsondispatchobj = {};
                jsondispatchobj.freeDispatchDetails = freearraydetails;

                for (i = 0; i < jsondispatchobj.freeDispatchDetails.length; i++) {
                    if (jsondispatchobj.freeDispatchDetails[i].PRODUCTID.trim() == $('#PRODUCTID').val().trim() && jsondispatchobj.freeDispatchDetails[i].BATCHNO.trim() == $("#hdnbatchno").val().trim()) {
                        exists = true;
                        break;
                    }
                }
                if (exists != false) {
                    toastr.error('Item already exists...!');
                    return false;
                }
                else {
                    addFreeProductInDetailsGrid();
                }
            })
        }
    }
}

function addProductInDetailsGrid() {
    debugger;
    var convertedqty = '0';   
    var assesment = '';
    var qty = '';
    var netweight = '';
    var grossweight = '';
    var hsn = '';
    var igsttax = '';
    var Amount = 0;
    var AmountINR = 0;
    var productname = $('#PRODUCTID').find('option:selected').text().trim();
    var batchno = $("#hdnbatchno").val().trim();
    var packsizename = $('#PSID').find('option:selected').text();
    var TransferQty = $('#InvoiceQty').val();
    var Rate = 0;
    var ExchangeRate = 0;
    Rate = parseFloat($('#Rate').val().trim()).toFixed(4);
    ExchangeRate = parseFloat($('#ExchangeRate').val().trim()).toFixed(4);
    debugger;

    if ($('#PSID').val().trim() == '1970C78A-D062-4FE9-85C2-3E12490463AF') {
        convertedqty = getconversionqty($('#PRODUCTID').val().trim(), '1970C78A-D062-4FE9-85C2-3E12490463AF', 'B9F29D12-DE94-40F1-A668-C79BF1BF4425', $('#InvoiceQty').val().trim());
    }
    else {
        convertedqty = getconversionqty($('#PRODUCTID').val().trim(), 'B9F29D12-DE94-40F1-A668-C79BF1BF4425', '1970C78A-D062-4FE9-85C2-3E12490463AF', $('#InvoiceQty').val().trim());
    }

    
    
    var MRP = $('#MRP').val();
    var AssesmentPercentage = $("#hdn_ASSESMENTPERCENTAGE").val();
    var Assesmentvalue = 0;
    var IgstTaxPercentage = 0;
    var IgstTaxAmount = 0;
    
    var NetAmount = 0;
    var srl = 0;
    srl = srl + 1; 

    /*Add Mode*/
    /*IGST*/
    if ($("#hdntaxcount").val().trim() == '1') {
        $.ajax({
            type: "POST",
            url: "/tranfac/GetExportCalculateAmtInPcs",
            data: { Productid: $('#PRODUCTID').val().trim(), PacksizeID: $('#PSID').val().trim(), Qty: $('#InvoiceQty').val().trim(), Rate: parseFloat(Rate), Assesment: $('#hdn_ASSESMENTPERCENTAGE').val().trim(), TaxName: $('#hdntaxnameIGST').val().trim(), date: $('#InvoiceDate').val().trim() },
            async: false,
            dataType: "json",
            success: function (response) {
                debugger;
                var listassesment = response.allcalculateDataset.varAssesment;
                var listqty = response.allcalculateDataset.varQty;
                var listNetWeight = response.allcalculateDataset.varNetwght;
                var listGrossWeight = response.allcalculateDataset.varGrosswght;
                var listHSN = response.allcalculateDataset.varHSN;
                var listHSNTax = response.allcalculateDataset.varHSNTax;
                var listHSNTaxID = response.allcalculateDataset.varHSNTaxID;


                $.each(listassesment, function (index, record) {
                    assesment = this['ASSESMENT'].toString().trim();
                });
                $.each(listqty, function (index, record) {
                    qty = this['QTYINPCS'].trim();
                });
                $.each(listNetWeight, function (index, record) {
                    netweight = this['NETWEIGHT'].trim();
                });
                $.each(listGrossWeight, function (index, record) {
                    grossweight = this['GROSSWEIGHT'].trim();
                });
                $.each(listHSN, function (index, record) {
                    hsn = this['HSNCODE'].trim();
                });
                if ($("#hdntaxcount").val().trim() == '1') {
                    

                    if (listHSNTax.length > 0) {
                        $.each(listHSNTax, function (index, record) {
                            igsttax = this['HSNTAX'].trim();
                        });
                    }
                    else {
                        igsttax = 0;
                    }
                }
                if ($("#hdntaxcount").val().trim() == '1') {
                    if (listHSNTaxID.length > 0) {
                        $.each(listHSNTaxID, function (index, record) {
                            $("#igstid").val(this['TAXID'].trim());
                        });
                    }
                    else {
                        $("#igstid").val('1177D9CF-8F1E-4C91-B785-FDF940101EEE');
                    }
                }
                
                Amount = parseFloat(qty * Rate);
                AmountINR = parseFloat(qty * Rate * ExchangeRate);
                Assesmentvalue = parseFloat(assesment);
                IgstTaxPercentage = parseFloat(igsttax);
                IgstTaxAmount = ((AmountINR * IgstTaxPercentage) / 100);
                NetAmount = AmountINR + IgstTaxAmount;
                //Create Table 
                var tr;
                tr = $('#productDetailsGrid');
                var HeaderCount = $('#productDetailsGrid thead th').length;
                if (HeaderCount == 0) {
                    tr.append("<thead><tr><th>Sl.No.</th><th style='display: none'>Order ID</th><th style='display: none'>Proforma ID</th><th style='display: none'>Order Date</th><th style='display: none'>Proforma Date</th><th style='display: none'>Product ID</th><th>HSN Code</th><th>Product</th><th>Batch</th><th style='display: none'>PackSize ID</th><th>Pack Size</th><th>MRP</th><th>Case</th><th>Pcs</th><th>Rate</th><th>Exchange Rate(INR)</th><th style='display: none'>Assesment(%)</th><th style='display: none'>Assesment Amount</th><th>Amount</th><th>Amount(INR)</th><th>SKU</th><th>Mfg.Date</th><th>Exp.Date</th><th>IGST(%)</th><th>IGST</th><th>Net Amt.(INR)</th><th style='display: none'>Tag</th><th>Delete</th></tr></thead><tbody>");
                }

                tr = $('<tr/>');
                tr.append("<td style='text-align: center'>" + srl + "</td>");//0
                tr.append("<td style='display: none'>" + $('#SaleOrderID').val() + "</td>");//1
                tr.append("<td style='display: none'>" + $('#ProformaOrderID').val() + "</td>");//2
                tr.append("<td style='display: none'>01/01/1900</td>");//3
                tr.append("<td style='display: none'>01/01/1900</td>");//4
                tr.append("<td style='display: none'>" + $('#PRODUCTID').val() + "</td>");//5
                tr.append("<td style='text-align: center'>" + hsn + "</td>");//6
                tr.append("<td style='width:250px'>" + productname + "</td>");//7
                tr.append("<td style='text-align: center'>" + batchno + "</td>");//8
                tr.append("<td style='display: none'>" + $('#PSID').val() + "</td>");//9
                tr.append("<td style='text-align: center'>" + packsizename + "</td>");//10
                tr.append("<td style='text-align: right'>" + MRP + "</td>");//11
                
                if ($('#PSID').val().trim() == 'B9F29D12-DE94-40F1-A668-C79BF1BF4425') {
                    tr.append("<td style='text-align: right'>" + parseFloat(convertedqty.trim()).toFixed(3) + "</td>");//12
                    tr.append("<td style='text-align: right'>" + parseFloat(TransferQty.trim()).toFixed(0) + "</td>");//13
                }
                else {
                    tr.append("<td style='text-align: right'>" + parseFloat(TransferQty.trim()).toFixed(3) + "</td>");//12
                    tr.append("<td style='text-align: right'>" + parseFloat(convertedqty.trim()).toFixed(0) + "</td>");//13
                }
                tr.append("<td style='text-align: right'>" + Rate + "</td>");//14
                tr.append("<td style='text-align: right'>" + ExchangeRate + "</td>");//15
                tr.append("<td style='display: none'>" + AssesmentPercentage + "</td>");//16
                tr.append("<td style='display: none'>" + Assesmentvalue.toFixed(2) + "</td>");//17
                tr.append("<td style='text-align: right'>" + Amount.toFixed(2) + "</td>");//18
                tr.append("<td style='text-align: right'>" + AmountINR.toFixed(2) + "</td>");//19
                tr.append("<td>" + netweight + "</td>");//20
                tr.append("<td style='text-align: center'>" + $('#hdn_mfgdate').val() + "</td>");//21
                tr.append("<td style='text-align: center'>" + $('#hdn_exprdate').val() + "</td>");//22
                tr.append("<td style='text-align: right'>" + IgstTaxPercentage.toFixed(2) + "</td>");//23
                tr.append("<td style='text-align: right'>" + IgstTaxAmount.toFixed(2) + "</td>");//24
                tr.append("<td style='text-align: right'>" + NetAmount.toFixed(2) + "</td>");//25
                tr.append("<td style='display: none'>1</td>");//26
                tr.append("<td style='text-align: center'><input type='image' class='gvTempDelete'  id='btndelete' <img src='../Images/ico_delete_16.png'/></input></td>");
                $("#productDetailsGrid").append(tr);
                tr.append("</tbody>");
                RowCount();
                CalculateAmount();
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    }
   
    
    /*Filling Tax Datatable*/
    if ($("#hdntaxcount").val() == '1') {
        addRowinTaxTable($('#PRODUCTID').val().trim(), batchno,$('#igstid').val().trim(),IgstTaxPercentage, IgstTaxAmount, MRP, '1');
    }
    else {
        addRowinTaxTable($('#PRODUCTID').val().trim(), batchno, $('#igstid').val().trim(), IgstTaxPercentage, IgstTaxAmount, MRP, '0');
    }
    /*********************/
    clearafterAdd();
    $("#PRODUCTID").focus();
    $('#PRODUCTID').trigger('chosen:activate');
}

function addFreeProductInDetailsGrid() {
    debugger;
    var convertedqty = '0';
    var assesment = '';
    var qty = '';
    var netweight = '';
    var grossweight = '';
    var hsn = '';
    var Amount = 0;
    var AmountINR = 0;
    var productname = $('#PRODUCTID').find('option:selected').text().trim();
    var batchno = $("#hdnbatchno").val().trim();
    var packsizename = $('#PSID').find('option:selected').text();
    var TransferQty = $('#InvoiceQty').val();
    var Rate = 0;
    var ExchangeRate = 0;
    /*Rate = parseFloat($('#Rate').val().trim()).toFixed(4);
    ExchangeRate = parseFloat($('#ExchangeRate').val().trim()).toFixed(4);*/
    if ($('#PSID').val().trim() == '1970C78A-D062-4FE9-85C2-3E12490463AF') {
        convertedqty = getconversionqty($('#PRODUCTID').val().trim(), '1970C78A-D062-4FE9-85C2-3E12490463AF', 'B9F29D12-DE94-40F1-A668-C79BF1BF4425', $('#InvoiceQty').val().trim());
    }
    else {
        convertedqty = getconversionqty($('#PRODUCTID').val().trim(), 'B9F29D12-DE94-40F1-A668-C79BF1BF4425', '1970C78A-D062-4FE9-85C2-3E12490463AF', $('#InvoiceQty').val().trim());
    }
    var MRP = $('#MRP').val();
    var AssesmentPercentage = $("#hdn_ASSESMENTPERCENTAGE").val();
    var Assesmentvalue = 0;
    var NetAmount = 0;
    var srl = 0;
    srl = srl + 1;
    /*Add Mode*/
    $.ajax({
        type: "POST",
        url: "/tranfac/GetExportCalculateAmtInPcs",
        data: { Productid: $('#PRODUCTID').val().trim(), PacksizeID: $('#PSID').val().trim(), Qty: $('#InvoiceQty').val().trim(), Rate: parseFloat(Rate), Assesment: $('#hdn_ASSESMENTPERCENTAGE').val().trim(), TaxName: $('#hdntaxnameIGST').val().trim(), date: $('#InvoiceDate').val().trim() },
        async: false,
        dataType: "json",
        success: function (response) {
            debugger;
            var listassesment = response.allcalculateDataset.varAssesment;
            var listqty = response.allcalculateDataset.varQty;
            var listNetWeight = response.allcalculateDataset.varNetwght;
            var listGrossWeight = response.allcalculateDataset.varGrosswght;
            var listHSN = response.allcalculateDataset.varHSN;
            var listHSNTax = response.allcalculateDataset.varHSNTax;
            var listHSNTaxID = response.allcalculateDataset.varHSNTaxID;


            $.each(listassesment, function (index, record) {
                assesment = this['ASSESMENT'].toString().trim();
            });
            $.each(listqty, function (index, record) {
                qty = this['QTYINPCS'].trim();
            });
            $.each(listNetWeight, function (index, record) {
                netweight = this['NETWEIGHT'].trim();
            });
            $.each(listGrossWeight, function (index, record) {
                grossweight = this['GROSSWEIGHT'].trim();
            });
            $.each(listHSN, function (index, record) {
                hsn = this['HSNCODE'].trim();
            });
            if ($("#hdntaxcount").val().trim() == '1') {
                $.each(listHSNTax, function (index, record) {
                    igsttax = this['HSNTAX'].trim();
                });
            }
            if ($("#hdntaxcount").val().trim() == '1') {
                $.each(listHSNTaxID, function (index, record) {
                    $("#igstid").val(this['TAXID'].trim());
                });
            }

            Amount = 0;
            AmountINR = 0;
            Assesmentvalue = 0;
            NetAmount = 0;
            //Create Table 
            var tr;
            tr = $('#freeDetailsGrid');
            var HeaderCount = $('#freeDetailsGrid thead th').length;
            if (HeaderCount == 0) {
                tr.append("<thead><tr><th>Sl.No.</th><th style='display: none'>Order ID</th><th style='display: none'>Proforma ID</th><th style='display: none'>Order Date</th><th style='display: none'>Proforma Date</th><th style='display: none'>Product ID</th><th>HSN Code</th><th>Product</th><th>Batch</th><th style='display: none'>PackSize ID</th><th>Pack Size</th><th>MRP</th><th>Case</th><th>Pcs</th><th>Rate</th><th>Exchange Rate(INR)</th><th style='display: none'>Assesment(%)</th><th style='display: none'>Assesment Amount</th><th>Amount</th><th>Amount(INR)</th><th>SKU</th><th>Mfg.Date</th><th>Exp.Date</th><th>Net Amt.(INR)</th><th>Delete</th></tr></thead><tbody>");
            }

            tr = $('<tr/>');
            tr.append("<td style='text-align: center'>" + srl + "</td>");//0
            tr.append("<td style='display: none'>" + $('#SaleOrderID').val() + "</td>");//1
            tr.append("<td style='display: none'>" + $('#ProformaOrderID').val() + "</td>");//2
            tr.append("<td style='display: none'>01/01/1900</td>");//3
            tr.append("<td style='display: none'>01/01/1900</td>");//4
            tr.append("<td style='display: none'>" + $('#PRODUCTID').val() + "</td>");//5
            tr.append("<td style='text-align: center'>" + hsn + "</td>");//6
            tr.append("<td style='width:250px'>" + productname + "</td>");//7
            tr.append("<td style='text-align: center'>" + batchno + "</td>");//8
            tr.append("<td style='display: none'>" + $('#PSID').val() + "</td>");//9
            tr.append("<td style='text-align: center'>" + packsizename + "</td>");//10
            tr.append("<td style='text-align: right'>" + MRP + "</td>");//11

            if ($('#PSID').val().trim() == 'B9F29D12-DE94-40F1-A668-C79BF1BF4425') {
                tr.append("<td style='text-align: right'>" + parseFloat(convertedqty.trim()).toFixed(3) + "</td>");//12
                tr.append("<td style='text-align: right'>" + parseFloat(TransferQty.trim()).toFixed(0) + "</td>");//13
            }
            else {
                tr.append("<td style='text-align: right'>" + parseFloat(TransferQty.trim()).toFixed(3) + "</td>");//12
                tr.append("<td style='text-align: right'>" + parseFloat(convertedqty.trim()).toFixed(0) + "</td>");//13
            }
            tr.append("<td style='text-align: right'>" + Rate + "</td>");//14
            tr.append("<td style='text-align: right'>" + ExchangeRate + "</td>");//15
            tr.append("<td style='display: none'>" + AssesmentPercentage + "</td>");//16
            tr.append("<td style='display: none'>" + Assesmentvalue.toFixed(2) + "</td>");//17
            tr.append("<td style='text-align: right'>" + Amount.toFixed(2) + "</td>");//18
            tr.append("<td style='text-align: right'>" + AmountINR.toFixed(2) + "</td>");//19
            tr.append("<td>" + netweight + "</td>");//20
            tr.append("<td style='text-align: center'>" + $('#hdn_mfgdate').val() + "</td>");//21
            tr.append("<td style='text-align: center'>" + $('#hdn_exprdate').val() + "</td>");//22
            tr.append("<td style='text-align: right'>" + NetAmount.toFixed(2) + "</td>");//23
            
            tr.append("<td style='text-align: center'><input type='image' class='gvFreeDelete'  id='btnfreedelete' <img src='../Images/ico_delete_16.png'/></input></td>");
            $("#freeDetailsGrid").append(tr);
            tr.append("</tbody>");
            FreeRowCount();
            CalculateAmount();
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
    
    clearafterAdd();
    $("#PRODUCTID").focus();
    $('#PRODUCTID').trigger('chosen:activate');
}

function addRowinTaxTable(ProductID, batch, taxid, TaxPercentage, TaxAmount, MRP, TaxFlag) {
    $.ajax({
        type: "POST",
        url: "/tranfac/FillExportTaxDatatable",
        data: { Productid: ProductID, BatchNo: batch, TaxID: taxid, Percentage: parseFloat(TaxPercentage), TaxValue: parseFloat(TaxAmount), MRP: parseFloat(MRP), Flag: TaxFlag },
        async: false,
        dataType: "json",
        success: function (response) {

        }
    });
}

function addRowinTaxTableEdit(ProductID, batch, taxid, TaxPercentage, TaxAmount, MRP, TaxFlag) {
    debugger;
    $.ajax({
        type: "POST",
        url: "/tranfac/FillExportTaxDatatable",
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
        url: "/tranfac/DeleteTaxDatatableExport",
        data: { Productid: grdproductid.trim(), BatchNo: grdbatchno.trim() },
        dataType: "json",
        success: function (response) {

        }
    });
}

function bindbatchno() {
    var ddlBatch = $("#ddlBatch");
    var Batch = '0';
    var desc = '';
    var desc1 = '';
    var val1 = '';
    var listItems = '';
    listItems = "<option value='0'>Select Batch</option>";
    desc = 'Stock Qty'.padEnd(8, '\u00A0') + repeatstr("&nbsp;", 8) + 'MRP'.padEnd(8, '\u00A0') + repeatstr("&nbsp;", 8) + 'Mfg.Date'.padEnd(8, '\u00A0') + repeatstr("&nbsp;", 8) + 'Exp.Date'.padEnd(8, '\u00A0') + repeatstr("&nbsp;", 8) + 'Batch'.padEnd(8, '\u00A0');
    listItems += "<option value='0'>" + desc + "</option>";
    $("#dialog").dialog({
        autoOpen: true,
        modal: true,
        title: "Loading.."
    });
    $("#imgLoader").css("visibility", "visible");
    $.ajax({
        type: "POST",
        url: "/tranfac/GetExportBatchDetails",
        data: { DepotID: $('#BRID').val(), ProductID: $('#PRODUCTID').val(), PacksizeID: $('#PSID').val(), BatchNo: Batch },
        dataType: "json",
        success: function (response) {
            var counter = 0;
            $('#ddlBatch').empty();
            $.each(response, function () {
                val1 = this["BATCHNO"] + "|" + this["BatchSTOCKQTY"] + "|" + this["BatchMRP"] + "|" + this["BatchMFGDATE"] + "|" + this["BatchEXPIRDATE"];
                desc1 = this["BatchSTOCKQTY"].padEnd(8, '\u00A0') + repeatstr("&nbsp;", 8) + this["BatchMRP"].padEnd(8, '\u00A0') + repeatstr("&nbsp;", 8) + this["BatchMFGDATE"].padEnd(8, '\u00A0') + repeatstr("&nbsp;", 8) + this["BatchEXPIRDATE"].padEnd(8, '\u00A0') + repeatstr("&nbsp;", 8) + this["BATCHNO"].padEnd(8, '\u00A0')
                listItems += "<option value='" + val1 + "'>" + desc1 + "</option>";
                counter = counter + 1;
            });
            ddlBatch.append(listItems);
            debugger;
            if (counter > 0) {
                debugger;
                if (counter == 1) {
                    $("#ddlBatch").val(val1)
                    getBatchDetails();
                    $("#InvoiceQty").focus();
                }
            }
            else {
                toastr.info('<b>Stock not available..</b>');
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

function getBatchDetails() {
    debugger;
    var batchmrp;
    var batchassessvalue;
    var batchmfgdt;
    var batchexpdt;
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
    bindorderdetails();
}

function RowCount() {
    var table = document.getElementById("productDetailsGrid");
    var rowCount = document.getElementById("productDetailsGrid").rows.length - 1;
    if (rowCount > 0) {
        $("#ExchangeRate").prop("disabled", true);
        for (var i = 1; i <= rowCount; i++) {
            table.rows[i].cells[0].innerHTML = i.toString();
        }
    }
}

function FreeRowCount() {
    var table = document.getElementById("freeDetailsGrid");
    var rowCount = document.getElementById("freeDetailsGrid").rows.length - 1;
    if (rowCount > 0) {
        for (var i = 1; i <= rowCount; i++) {
            table.rows[i].cells[0].innerHTML = i.toString();
        }
    }
}

function PackingListRowCount() {
    var table = document.getElementById("packinglistGrid");
    var rowCount = document.getElementById("packinglistGrid").rows.length - 1;
    if (rowCount > 0) {
        for (var i = 1; i <= rowCount; i++) {
            table.rows[i].cells[0].innerHTML = i.toString();
        }
    }
}

function RowCountEdit() {
    var table = document.getElementById("productDetailsGrid");
    var rowCount = document.getElementById("productDetailsGrid").rows.length - 1;
    if (rowCount > 0) {
        for (var i = 1; i <= rowCount; i++) {
            table.rows[i].cells[0].innerHTML = i.toString();
        }
    }
}

function RowCountDispatchList() {
    var table = document.getElementById("invoiceGridFG");
    var rowCount = document.getElementById("invoiceGridFG").rows.length - 1;
    if (rowCount > 0) {
        for (var i = 1; i <= rowCount; i++) {
            table.rows[i].cells[0].innerHTML = i.toString();
        }
    }
}

function CalculateAmount() {
    debugger;
    var totalbasicamt = 0;
    var totaltaxamt = 0;
    var totalbasicplustaxamt = 0;
    var decimalvalue = 0;
    var totalcaseqty = 0;
    var totalpcsqty = 0;
    var freetotalcaseqty = 0;
    var freetotalpcsqty = 0;
    var adjamt = 0;
    var afteradjustment = 0;
    var parts = 0;
    var finalamt = 0;
    var rowCount = 0;
    var freerowCount = 0;
    adjamt = parseFloat($("#AdjustmentAmount").val()).toFixed(2);
    rowCount = document.getElementById("productDetailsGrid").rows.length - 1;    
    freerowCount = document.getElementById("freeDetailsGrid").rows.length - 1;
    if (rowCount > 0 && freerowCount > 0) {
       
        $('#productDetailsGrid tbody tr').each(function () {
            totalbasicamt += parseFloat($(this).find('td:eq(19)').html().trim());
            totaltaxamt += parseFloat($(this).find('td:eq(24)').html().trim());
            totalbasicplustaxamt += parseFloat($(this).find('td:eq(25)').html().trim());
            totalcaseqty += parseFloat($(this).find('td:eq(12)').html().trim());
            totalpcsqty += parseInt($(this).find('td:eq(13)').html().trim());
        });
        $('#freeDetailsGrid tbody tr').each(function () {
            freetotalcaseqty += parseFloat($(this).find('td:eq(12)').html().trim());
            freetotalpcsqty += parseInt($(this).find('td:eq(13)').html().trim());
        });
        $('#BasicAmt').val(totalbasicamt.toFixed(2));
        $('#TaxAmt').val(totaltaxamt.toFixed(2));
        $('#GrossAmt1').val(totalbasicplustaxamt.toFixed(2));
        $('#GrossAmt').val(totalbasicplustaxamt.toFixed(2));
        $('#TotalCase').val((totalcaseqty + freetotalcaseqty).toFixed(3));
        $('#TotalPcs').val(totalpcsqty + freetotalpcsqty);
        if ($('#CountryID').val().trim() == 'F807170C-D422-4FEF-B909-165E543257D3' || $('#CountryID').val().trim() == '6F2354BC-C2DF-49C2-B0C4-2D4B114C0EF4')/*Nepal Or Bhutan*/ {

            afteradjustment = totalbasicplustaxamt - adjamt;
            parts = afteradjustment - Math.floor(afteradjustment);

            decimalvalue = parts.toFixed(2);
            //if (decimalvalue >= .50) {
            //    decimalvalue = 1 - decimalvalue;
            //    decimalvalue = decimalvalue.toFixed(2);

            //}
            //else {
            //    decimalvalue = -decimalvalue;
            //}
            //finalamt = Math.round(afteradjustment);
            /*Round Up Logic start from 01-09-2020*/
            if (decimalvalue > 0) {
                decimalvalue = 1 - decimalvalue;
                decimalvalue = decimalvalue.toFixed(2);
            }
            finalamt = Math.ceil(afteradjustment);
            /*Round Up Logic end*/
            $("#NetAmt").val(finalamt.toFixed(2));
            $("#RoundOff").val(decimalvalue.toFixed(2));
        }
        else {

            afteradjustment = totalbasicplustaxamt - adjamt;
            finalamt = afteradjustment;
            $("#NetAmt").val(finalamt.toFixed(2));
            $("#RoundOff").val('0.00');
        }
    }
    else if (rowCount > 0 && freerowCount == -1) {
        $('#productDetailsGrid tbody tr').each(function () {
            totalbasicamt += parseFloat($(this).find('td:eq(19)').html().trim());
            totaltaxamt += parseFloat($(this).find('td:eq(24)').html().trim());
            totalbasicplustaxamt += parseFloat($(this).find('td:eq(25)').html().trim());
            totalcaseqty += parseFloat($(this).find('td:eq(12)').html().trim());
            totalpcsqty += parseInt($(this).find('td:eq(13)').html().trim());
        });
        $('#BasicAmt').val(totalbasicamt.toFixed(2));
        $('#TaxAmt').val(totaltaxamt.toFixed(2));
        $('#GrossAmt1').val(totalbasicplustaxamt.toFixed(2));
        $('#GrossAmt').val(totalbasicplustaxamt.toFixed(2));
        $('#TotalCase').val(totalcaseqty.toFixed(3));
        $('#TotalPcs').val(totalpcsqty);
        if ($('#CountryID').val().trim() == 'F807170C-D422-4FEF-B909-165E543257D3' || $('#CountryID').val().trim() == '6F2354BC-C2DF-49C2-B0C4-2D4B114C0EF4')/*Nepal Or Bhutan*/ {

            afteradjustment = totalbasicplustaxamt - adjamt;
            parts = afteradjustment - Math.floor(afteradjustment);

            decimalvalue = parts.toFixed(2);
            //if (decimalvalue >= .50) {
            //    decimalvalue = 1 - decimalvalue;
            //    decimalvalue = decimalvalue.toFixed(2);

            //}
            //else {
            //    decimalvalue = -decimalvalue;
            //}
            //finalamt = Math.round(afteradjustment);

            /*Round Up Logic start from 01-09-2020*/
            if (decimalvalue > 0) {
                decimalvalue = 1 - decimalvalue;
                decimalvalue = decimalvalue.toFixed(2);
            }
            finalamt = Math.ceil(afteradjustment);
            /*Round Up Logic end*/
            $("#NetAmt").val(finalamt.toFixed(2));
            $("#RoundOff").val(decimalvalue.toFixed(2));
        }
        else {

            afteradjustment = totalbasicplustaxamt - adjamt;
            finalamt = afteradjustment;
            $("#NetAmt").val(finalamt.toFixed(2));
            $("#RoundOff").val('0.00');
        }
    }
    else if (rowCount == -1 && freerowCount > 0) {
        $('#freeDetailsGrid tbody tr').each(function () {
            freetotalcaseqty += parseFloat($(this).find('td:eq(12)').html().trim());
            freetotalpcsqty += parseInt($(this).find('td:eq(13)').html().trim());
        });
        $('#BasicAmt').val(0);
        $('#TaxAmt').val(0);
        $('#GrossAmt1').val(0);
        $('#GrossAmt').val(0);
        $('#NetAmt').val(0);
        $('#RoundOff').val(0);
        $('#TotalCase').val(freetotalcaseqty.toFixed(3));
        $('#TotalPcs').val(freetotalpcsqty);
    }
    else if (rowCount == -1 && freerowCount == -1){
        $('#BasicAmt').val(0);
        $('#TaxAmt').val(0);
        $('#GrossAmt1').val(0);
        $('#GrossAmt').val(0);
        $('#NetAmt').val(0);
        $('#RoundOff').val(0);
        $('#TotalCase').val(0);
        $('#TotalPcs').val(0);
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
    $('#divSourceDepot').css("display", "none");
    $('#dvProduct').css("display", "none");
    $("#TransferNo").attr("disabled", "disabled");
    $("#ShippingDate").attr("disabled", "disabled");
    $("#LCDate").attr("disabled", "disabled");
    $("#LCNo").attr("disabled", "disabled");
    $("#LCBank").attr("disabled", "disabled");
    $("#InvoiceDate").attr("disabled", "disabled");
    $("#LrGrDate").attr("disabled", "disabled");
    $("#ExportRefNo").attr("disabled", "disabled");
    $("#MRP").attr("disabled", "disabled");
    $("#StockQty").attr("disabled", "disabled");
    $("#Rate").attr("disabled", "disabled");
    $("#OrderQty").attr("disabled", "disabled");
    $("#DeliveredQty").attr("disabled", "disabled");
    $("#RemainingQty").attr("disabled", "disabled");
    $("#BasicAmt").attr("disabled", "disabled");
    $("#TaxAmt").attr("disabled", "disabled");
    $("#GrossAmt1").attr("disabled", "disabled");
    $("#GrossAmt").attr("disabled", "disabled");
    $("#RoundOff").attr("disabled", "disabled");
    $("#NetAmt").attr("disabled", "disabled");
    $("#TotalCase").attr("disabled", "disabled");
    $("#TotalPcs").attr("disabled", "disabled");
    $("#ProformaAmount").attr("disabled", "disabled");
    $("#AdjustmentAmount").attr("disabled", "disabled");
    $("#BRID").attr("disabled", "disabled");
    $("#BRID").chosen({
        search_contains: true
    });
    $("#BRID").trigger("chosen:updated");

    $("#CUSTOMERID").empty();
    $("#CUSTOMERID").chosen({
        search_contains: true
    });
    $("#CUSTOMERID").trigger("chosen:updated");

    $('#SaleOrderID').empty();
    $("#SaleOrderID").chosen({
        search_contains: true
    });
    $("#SaleOrderID").trigger("chosen:updated");

    $('#ProformaOrderID').empty();
    $("#ProformaOrderID").chosen({
        search_contains: true
    });
    $("#ProformaOrderID").trigger("chosen:updated");

    $("#Tranportmode").val('0');
    

    $("#CountryID").val('0');
    $("#CountryID").chosen({
        search_contains: true
    });
    $("#CountryID").trigger("chosen:updated");

    $("#LoadingPortID").val('0');
    $("#LoadingPortID").chosen({
        search_contains: true
    });
    $("#LoadingPortID").trigger("chosen:updated");

    $("#DischargePortID").val('0');
    $("#DischargePortID").chosen({
        search_contains: true
    });
    $("#DischargePortID").trigger("chosen:updated");

    $('#BankID').empty();
    $("#BankID").chosen({
        search_contains: true
    });
    $("#BankID").trigger("chosen:updated");
    
    $('#ddlBatch').empty();
    $("#VesselName").val('');
    $("#LrGrNo").val('');
    $("#ExportRefNo").val('');
    $("#OtherRefNo").val('');
    $("#ExchangeRate").val('');
    $("#FinalDestination").val('');
    $("#ShippingBill").val('');
    $("#ContainerNo").val('');
    $("#VoyNo").val('');
    $("#LCNo").val('');
    $("#LCDate").val('');
    $("#LCBank").val('');
    $("#Branch").val('');
    $("#BankAddress").val('');
    $("#IFSC").val('');
    $("#SwiftCode").val('');
    $("#AccNo").val('');
    $("#Consignee").val('');
    $("#NotifyParty").val('');
    $("#DeliveryTo").val('');
    $("#ShippingDate").val('');
    $("#chkfree").prop("checked", false);
    $("#radio_Export").prop("checked", true);
    
    //if (Name != 'Itemledger') {
        //backdatestatus = bindbackdateflag('4197');
        //if (parseInt(backdatestatus) > 0) {
        //    $("#InvoiceDate").datepicker({
        //        changeMonth: true,
        //        changeYear: true,
        //        showButtonPanel: true,
        //        selectCurrent: true,
        //        todayBtn: "linked",
        //        showAnim: "slideDown",
        //        yearRange: "-3:+0",
        //        maxDate: 'today',
        //        dateFormat: "dd/mm/yy",
        //        showOn: 'button',
        //        buttonText: 'Show Date',
        //        buttonImageOnly: true,
        //        buttonImage: 'http://jqueryui.com/resources/demos/datepicker/images/calendar.gif'
        //    });
        //    $(".ui-datepicker-trigger").mouseover(function () {
        //        $(this).css('cursor', 'pointer');
        //    });
        //}
        //else {
        //    $("#InvoiceDate").datepicker({
        //        changeMonth: true,
        //        changeYear: true,
        //        showButtonPanel: true,
        //        selectCurrent: true,
        //        todayBtn: "linked",
        //        showAnim: "slideDown",
        //        yearRange: "-3:+0",
        //        maxDate: 'today',
        //        dateFormat: "dd/mm/yy",
        //        showOn: 'button',
        //        buttonText: 'Show Date',
        //        buttonImageOnly: true,
        //        maxDate: 0,
        //        minDate: 0,
        //        buttonImage: 'http://jqueryui.com/resources/demos/datepicker/images/calendar.gif'
        //    });
        //    $(".ui-datepicker-trigger").mouseover(function () {
        //        $(this).css('cursor', 'pointer');
        //    });
        //}
    //}

    $('#PRODUCTID').empty();
    $("#PRODUCTID").chosen({
        search_contains: true
    });
    $("#PRODUCTID").trigger("chosen:updated");

    $('#PSID').empty();
    $("#PSID").chosen({
        search_contains: true
    });
    $("#PSID").trigger("chosen:updated");

    
    $('#InvoiceQty').val('');
    $('#StockQty').val('');
    $('#MRP').val('');
    $('#Rate').val('');
    $('#OrderQty').val('');
    $('#DeliveredQty').val('');
    $('#RemainingQty').val('');
    $('#productDetailsGrid').empty();
    $('#freeDetailsGrid').empty();
    $('#BasicAmt').val('');
    $('#TaxAmt').val('');
    $('#GrossAmt1').val('');
    $('#GrossAmt').val('');
    $('#RoundOff').val('');
    $('#NetAmt').val('');
    $("#ProformaAmount").val('');
    $("#AdjustmentAmount").val('');
    $('#TotalCase').val('');
    $('#TotalPcs').val('');
    $('#Remarks').val('');
    $("#AmountInWords").val('');
    $('#hdndispatchID').val('0');
    $("#hdntaxcount").val('');
    
}

function clearafterAdd() {
    $('#InvoiceQty').val('');
    $('#StockQty').val('');
    $('#MRP').val('');
    $('#Rate').val('');
    $('#OrderQty').val('');
    $('#DeliveredQty').val('');
    $('#RemainingQty').val('');
    if ($("#chkfree").prop('checked') == true) {
        $("#chkfree").prop("checked", false);
    }
}

function SavePackingList() {
    debugger;
    var i = 0;
    packinglist = new Array();
    var packingsave = {};
    debugger;
    packingsave.FGInvoiceID = $('#hdndispatchID').val().trim();
    packingsave.DescPackages = $("#DescPackages").val().trim();

    $('#packinglistGrid tbody tr').each(function () {

        debugger
        var packingdetails = {};

        var saleinvoiceid = $(this).find('td:eq(1)').html().trim();
        var saleorderid = $(this).find('td:eq(2)').html().trim();
        var productid = $(this).find('td:eq(3)').html().trim();
        var productname = $(this).find('td:eq(4)').html().trim();
        var batchno = $(this).find('td:eq(5)').html().trim();
        var qty = $(this).find('td:eq(6)').html().trim();
        var startposition = $(this).find('#txtstart').val().trim();
        var endposition = $(this).find('#txtend').val().trim();
        var grosswght = $(this).find('#txtgross').val().trim();
        var netwght = $(this).find('#txtnet').val().trim();
       

        packingdetails.SALEINVOICEID = saleinvoiceid;
        packingdetails.SALEORDERID = saleorderid;
        packingdetails.PRODUCTID = productid;
        packingdetails.PRODUCTNAME = productname;
        packingdetails.BATCHNO = batchno;
        packingdetails.QTY = qty;
        packingdetails.STARTPOSITION = startposition;
        packingdetails.ENDPOSITION = endposition;
        packingdetails.GROSSWEIGHT = grosswght;
        packingdetails.NETWEIGHT = netwght;
        packinglist[i++] = packingdetails;
    });
    packingsave.PackingDetails = packinglist;

    $.ajax({
        url: "/tranfac/PackingListInsert",
        data: '{packingsave:' + JSON.stringify(packingsave) + '}',
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
            if (messageid == '2') {
                $('#PackInvoiceNo').val('');
                $('#PackCaseQty').val('');
                $("#DescPackages").val('');
                $("#packinglistGrid").empty();
                $("#dvpackinglist").dialog("close");
                toastr.success('<b><font color=black>' + messagetext + '</font></b>');
            }
            else {
                $("#dvpackinglist").dialog("open");
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

function SaveInvoice() {
    debugger;
    var i = 0;
    var j = 0;
    varSourceDepotText = $("#BRID option:selected").text();
    $("#BRNAME").val(varSourceDepotText);

    varCustomerText = $("#CUSTOMERID option:selected").text();
    $("#CUSTOMERNAME").val(varCustomerText);

    invoicelist = new Array();
    freelist = new Array();
    var invoicesave = {};
    debugger;


    invoicesave.FGInvoiceID = $('#hdndispatchID').val().trim();
    if ($('#hdndispatchID').val() == '0') {
        invoicesave.FLAG = 'A';
    }
    else {
        invoicesave.FLAG = 'U';
    }
    invoicesave.InvoiceDate = $("#InvoiceDate").val().trim();
    invoicesave.CUSTOMERID = $("#CUSTOMERID").val().trim();
    invoicesave.CUSTOMERNAME = $("#CUSTOMERNAME").val().trim();
    invoicesave.ExportRefNo = $("#ExportRefNo").val().trim();
    invoicesave.OtherRefNo = $("#OtherRefNo").val().trim();
    invoicesave.VesselName = $("#VesselName").val().trim();
    invoicesave.BRID = $("#BRID").val().trim();
    invoicesave.BRNAME = $("#BRNAME").val().trim();
    invoicesave.LrGrNo = $("#LrGrNo").val().trim();
    invoicesave.LrGrDate = $("#LrGrDate").val().trim();
    invoicesave.Tranportmode = $("#Tranportmode").val().trim();
    invoicesave.Remarks = $("#Remarks").val().trim();
    invoicesave.NetAmt = $("#NetAmt").val().trim();
    invoicesave.AdjustmentAmount = $("#AdjustmentAmount").val().trim();
    invoicesave.RoundOff = $("#RoundOff").val().trim();
    invoicesave.LoadingPortID = $("#LoadingPortID").val().trim();
    invoicesave.LoadingPortName = $("#LoadingPortID option:selected").text().trim();
    invoicesave.DischargePortID = $("#DischargePortID").val().trim();
    invoicesave.DischargePortName = $("#DischargePortID option:selected").text().trim();
    invoicesave.FinalDestination = $("#FinalDestination").val().trim();
    invoicesave.TotalCase = $("#TotalCase").val().trim();
    invoicesave.ShippingBill = $("#ShippingBill").val().trim();
    invoicesave.ContainerNo = $("#ContainerNo").val().trim();
    invoicesave.LCNo = $("#LCNo").val().trim();
    invoicesave.LCDate = $("#LCDate").val().trim();
    invoicesave.LCBank = $("#LCBank").val().trim();
    invoicesave.Consignee = $("#Consignee").val().trim();
    invoicesave.NotifyParty = $("#NotifyParty").val().trim();
    invoicesave.CountryID = $("#CountryID").val().trim();
    invoicesave.CountryName = $("#CountryID option:selected").text().trim();
    invoicesave.ShippingDate = $("#ShippingDate").val().trim();
    invoicesave.VoyNo = $("#VoyNo").val().trim();
    invoicesave.TotalPcs = $("#TotalPcs").val().trim();
    invoicesave.BankID = $("#BankID").val().trim();
    invoicesave.BankName = $("#BankID option:selected").text().trim();
    invoicesave.Branch = $("#Branch").val().trim();
    invoicesave.BankAddress = $("#BankAddress").val().trim();
    invoicesave.IFSC = $("#IFSC").val().trim();
    invoicesave.SwiftCode = $("#SwiftCode").val().trim();
    invoicesave.AccNo = $("#AccNo").val().trim();
    invoicesave.SaleOrderID = $("#SaleOrderID").val().trim();
    invoicesave.ProformaOrderID = $("#ProformaOrderID").val().trim();

    invoicesave.DeliveryTo = $("#DeliveryTo").val().trim();
    invoicesave.AmountInWords = $("#AmountInWords").val().trim();

    invoicesave.InvoiceType = '1';
    if ($("#radio_Export").prop("checked")) {
        invoicesave.InvoiceSeqType = 'E';
    }
    else {
        invoicesave.InvoiceSeqType = 'D';
    }
    invoicesave.ExchangeRate = $("#ExchangeRate").val().trim();

    var freerowCount = document.getElementById("freeDetailsGrid").rows.length - 1;
    if (freerowCount <= 0) {
        invoicesave.FreeTag = '0';
    }
    else {
        invoicesave.FreeTag = '1';
    }
    


    $('#productDetailsGrid tbody tr').each(function () {

        debugger
        var invoicedetails = {};

        var productid = $(this).find('td:eq(5)').html().trim();
        var productname = $(this).find('td:eq(7)').html().trim();
        var batchno = $(this).find('td:eq(8)').html().trim();
        var packingsizeid = $(this).find('td:eq(9)').html().trim();
        var packingsizename = $(this).find('td:eq(10)').html().trim();
        var mrp = $(this).find('td:eq(11)').html().trim();
        var rate = $(this).find('td:eq(14)').html().trim();
        var erate = $(this).find('td:eq(15)').html().trim();
        var trnsferqtycase = $(this).find('td:eq(12)').html().trim();
        var trnsferqtypcs = $(this).find('td:eq(13)').html().trim();
        var amount = $(this).find('td:eq(18)').html().trim();
        var amountINR = $(this).find('td:eq(19)').html().trim();
        var assesmentpercentage = $(this).find('td:eq(16)').html().trim();
        var assesmentvalue = $(this).find('td:eq(17)').html().trim();
        var weight = $(this).find('td:eq(20)').html().trim();
        var mfdate = $(this).find('td:eq(21)').html().trim();
        var exprdate = $(this).find('td:eq(22)').html().trim();
        var tag = $(this).find('td:eq(26)').html().trim();
        var storelocationid = '5FA9501B-E8D0-4A0F-B917-17C636655514';
        var storelocationname = 'Export';
        var grossweight = '0';

        invoicedetails.PRODUCTID = productid;
        invoicedetails.PRODUCTNAME = productname;
        invoicedetails.PACKINGSIZEID = packingsizeid;
        invoicedetails.PACKINGSIZENAME = packingsizename;
        invoicedetails.MRP = mrp;
        invoicedetails.RATE = rate;
        invoicedetails.QTY = trnsferqtycase;
        invoicedetails.QTYPCS = trnsferqtypcs;
        invoicedetails.AMOUNT = amount;
        invoicedetails.BATCHNO = batchno;
        invoicedetails.ASSESMENTPERCENTAGE = assesmentpercentage;
        invoicedetails.TOTALASSESMENTVALUE = assesmentvalue;
        invoicedetails.WEIGHT = weight;
        invoicedetails.MFDATE = mfdate;
        invoicedetails.EXPRDATE = exprdate;
        invoicedetails.GROSSWEIGHT = grossweight;
        invoicedetails.STORELOCATIONID = storelocationid;
        invoicedetails.STORELOCATIONNAME = storelocationname;
        invoicedetails.NSR = erate;
        invoicedetails.RATEDISC = amountINR;
        invoicedetails.TAG = tag;
        invoicelist[i++] = invoicedetails;
    });
    invoicesave.InvoiceDetailsFG = invoicelist;

    if (freerowCount <= 0) {

        debugger
        var freedetails = {};

        var schemeproductid = 'NA';
        var schemeproductname = 'NA';
        var freeqtypcs = 0;
        var freeproductid = 'NA';
        var freeproductname = 'NA';
        var schemeqtypcs = 0;
        var freemrp = 0;
        var brate = 0;
        var freeamount = 0;
        var freebatchno = 'NA';
        var freeweight = 'NA';
        var freemfdate = 'NA';
        var freeexprdate = 'NA';
        var schemebatchno = 'NA';
        var freestorelocationid = 'NA';

        freedetails.SCHEME_PRODUCT_ID = schemeproductid;
        freedetails.SCHEME_PRODUCT_NAME = schemeproductname;
        freedetails.QTY = freeqtypcs;
        freedetails.PRODUCTID = freeproductid;
        freedetails.PRODUCTNAME = freeproductname;
        freedetails.SCHEME_QTY = schemeqtypcs;
        freedetails.MRP = freemrp;
        freedetails.BRATE = brate;
        freedetails.AMOUNT = freeamount;
        freedetails.BATCHNO = freebatchno;
        freedetails.WEIGHT = freeweight;
        freedetails.MFDATE = freemfdate;
        freedetails.EXPRDATE = freeexprdate;
        freedetails.SCHEME_PRODUCT_BATCHNO = schemebatchno;
        freedetails.STORELOCATIONID = freestorelocationid;
        freelist[j++] = freedetails;
       
    }
    else {

        $('#freeDetailsGrid tbody tr').each(function () {

            debugger
            var freedetails = {};

            var schemeproductid = $(this).find('td:eq(5)').html().trim();
            var schemeproductname = $(this).find('td:eq(7)').html().trim();
            var freeqtypcs = 0;
            var freeproductid = $(this).find('td:eq(5)').html().trim();
            var freeproductname = $(this).find('td:eq(7)').html().trim();
            var schemeqtypcs = $(this).find('td:eq(13)').html().trim();
            var freemrp = $(this).find('td:eq(11)').html().trim();
            var brate = 0;
            var freeamount = 0;
            var freebatchno = $(this).find('td:eq(8)').html().trim();
            var freeweight = $(this).find('td:eq(20)').html().trim();
            var freemfdate = $(this).find('td:eq(21)').html().trim();
            var freeexprdate = $(this).find('td:eq(22)').html().trim();
            var schemebatchno = $(this).find('td:eq(8)').html().trim();
            var freestorelocationid = '5FA9501B-E8D0-4A0F-B917-17C636655514';

            freedetails.SCHEME_PRODUCT_ID = schemeproductid;
            freedetails.SCHEME_PRODUCT_NAME = schemeproductname;
            freedetails.QTY = freeqtypcs;
            freedetails.PRODUCTID = freeproductid;
            freedetails.PRODUCTNAME = freeproductname;
            freedetails.SCHEME_QTY = schemeqtypcs;
            freedetails.MRP = freemrp;
            freedetails.BRATE = brate;
            freedetails.AMOUNT = freeamount;
            freedetails.BATCHNO = freebatchno;
            freedetails.WEIGHT = freeweight;
            freedetails.MFDATE = freemfdate;
            freedetails.EXPRDATE = freeexprdate;
            freedetails.SCHEME_PRODUCT_BATCHNO = schemebatchno;
            freedetails.STORELOCATIONID = freestorelocationid;
            freelist[j++] = freedetails;
        });
    }
    invoicesave.FreeDetailsFG = freelist;
    //alert(JSON.stringify(invoicesave));

    $.ajax({
        url: "/tranfac/exportGSTsavedata",
        data: '{invoicesave:' + JSON.stringify(invoicesave) + '}',
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
            if (messageid != '0') {
                $('#dvAdd').css("display", "none");
                $('#dvDisplay').css("display", "");
                $("#txtfrmdate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
                $("#txttodate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
                ClearControls();
                bindexportinvoicegrid();
                ReleaseSession();
                toastr.success('<b><font color=black>' + messagetext +'</font></b>');
            }
            else {
                $('#dvAdd').css("display", "");
                $('#dvDisplay').css("display", "none");
                toastr.error('<b><font color=black>'+messagetext+'</font></b>');
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

function ReleaseSession() {
   
    $.ajax({
        type: "POST",
        url: "/tranfac/RemoveSessionExportInvoice",
        data: '{}',
        dataType: "json",
        success: function (response) {
           
            
        }
    });
}

function bindexportinvoicegrid() {
    debugger;
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
        url: "/tranfac/BindExportInvoiceGrid",
        data: { FromDate: $('#txtfrmdate').val().trim(), ToDate: $('#txttodate').val().trim(), BSID: BusinessSegment, depotID: $('#BRID').val().trim(), type: '1' },
        dataType: "json",
        success: function (response) {
            debugger;
            var tr;
            tr = $('#invoiceGridFG');
            tr.append("<thead><tr><th>Sl. No.</th><th style='display: none'>InvoiceId</th><th>Invoice No</th><th>Invoice Date</th><th style='display: none'>DEPOTID</th><th style='display: none'>DEPOTNAME</th><th style='display: none'>CUSTOMERID</th><th>Customer</th><th>Loading Port</th><th>Discharge Port</th><th>Final Destination</th><th style='display: none'>ISVERIFIED</th><th>Financial Status</th><th style='display: none'>NEXTLEVELID</th><th>Total Case</th><th>Total Pcs</th><th>Net Amount</th><th>Entry User</th><th>Print Tax Invoice</th><th>Print Commercial Invoice</th><th>Packing List Print</th><th>Weight List Print</th><th>Edit</th><th>View</th><th>Packing List</th><th>Cancel</th>");
            
            $('#invoiceGridFG').DataTable().destroy();
            $("#invoiceGridFG tbody tr").remove();
            for (var i = 0; i < response.length; i++) {
                tr = $('<tr/>');
                tr.append("<td style='text-align: center'>" + srl + "</td>");//0
                tr.append("<td style='display: none'>" + response[i].SALEINVOICEID + "</td>");//1
                tr.append("<td>" + response[i].SALEINVOICENO + "</td>");//2
                tr.append("<td>" + response[i].SALEINVOICEDATE + "</td>");//3
                tr.append("<td style='display: none'>" + response[i].DEPOTID + "</td>");//4
                tr.append("<td style='display: none'>" + response[i].DEPOTNAME + "</td>");//5
                tr.append("<td style='display: none'>" + response[i].DISTRIBUTORID + "</td>");//6
                tr.append("<td>" + response[i].DISTRIBUTORNAME + "</td>");//7
                tr.append("<td>" + response[i].LOADINGPORTNAME + "</td>");//8
                tr.append("<td>" + response[i].DISCHARGEPORTNAME + "</td>");//9
                tr.append("<td>" + response[i].FINALDESTINATION + "</td>");//10
                tr.append("<td style='display: none'>" + response[i].ISVERIFIED + "</td>");//11
                if (response[i].ISVERIFIEDDESC.trim() == 'APPROVED') {
                    tr.append("<td><font color='0x00B300'>" + response[i].ISVERIFIEDDESC + "</font></td>");//12
                }
                else if (response[i].ISVERIFIEDDESC.trim() == 'PENDING'){
                    tr.append("<td><font color='0x5184FF'>" + response[i].ISVERIFIEDDESC + "</font></td>"); //12 
                }
                else {
                    tr.append("<td><font color='#ff2500'>" + response[i].ISVERIFIEDDESC + "</font></td>");//12
                }
                tr.append("<td style='display: none'>" + response[i].NEXTLEVELID + "</td>");//13
                tr.append("<td style='text-align: right'>" + parseFloat(response[i].TOTALCASEPACK).toFixed(3) + "</td>");//14
                tr.append("<td style='text-align: right'>" + parseFloat(response[i].TOTALPCS).toFixed(0) + "</td>");//15
                tr.append("<td style='text-align: right'>" + response[i].NETAMOUNT + "</td>");//16
                tr.append("<td>" + response[i].USERNAME + "</td>");//17
                tr.append("<td style='text-align: center'><input type='image' class='gvPrint' id='btntaxinvoiceprint'  <img src='../Images/ico_Print.png' width='20' height ='20' title='Print Tax Invoice'/></input></td>");
                tr.append("<td style='text-align: center'><input type='image' class='gvCommPrint' id='btncommercialinvoiceprint'  <img src='../Images/ico_Print.png' width='20' height ='20' title='Print Commercial Invoice'/></input></td>");
                tr.append("<td style='text-align: center'><input type='image' class='gvPackingPrint' id='btnpackingprint'  <img src='../Images/ico_Print.png' width='20' height ='20' title='Packing List Print'/></input></td>");
                tr.append("<td style='text-align: center'><input type='image' class='gvWeightPrint' id='btnweightprint'  <img src='../Images/ico_Print.png' width='20' height ='20' title='Weight List Print'/></input></td>");
                tr.append("<td style='text-align: center'><input type='image' class='gvEdit'   id='btndispatchedit'   <img src='../Images/Pencil-icon.png' title='Edit'/></input></td>");
                tr.append("<td style='text-align: center'><input type='image' class='gvView'   id='btndispatchview'   <img src='../Images/View.png' width='20' height ='20' title='View'/></input></td>");
                tr.append("<td style='text-align: center'><input type='image' class='gvPackingList'   id='btnpackingedit'   <img src='../Images/ico_PackingList.png' width='20' height ='20' title='Packing List'/></input></td>");
                tr.append("<td style='text-align: center'><input type='image' class='gvCancel' id='btndispatchdelete' <img src='../Images/ico_delete_16.png' title='Cancel'/></input></td>");
                
                $("#invoiceGridFG").append(tr);
            }
            tr.append("</tbody>");
            RowCountDispatchList();
            $('#invoiceGridFG').DataTable({
                "sScrollX": '100%',
                "sScrollXInner": "110%",
                "scrollY": "238px",
                "scrollCollapse": true,
                "dom": 'Bfrtip',
                buttons: [
                    {
                        extend: 'excelHtml5',
                        title: 'Export Invoice List'
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

function bindlockdateflag(entrydate) {
    var returnlockdateflag = null;
    $.ajax({
        type: "POST",
        url: "/tranfac/GetEntryLockChecking",
        data: { EntryDate: entrydate.trim() },
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
        url: "/tranfac/GetFGDispatchStatus",
        data: { DispatchID: invoiceid, ModuleID: '7', Type: '3' },
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
    debugger;
    var tr;
    var HeaderCount = 0;
    var srl = 0;
    srl = srl + 1; 
    var Igstid = '';
    var IgstPercentage = 0;
    var IgstAmount = 0;
    var NetAmount = 0;
    var OrderID = '';
    var Saleorder = $("#SaleOrderID");
    var Proforma = $("#ProformaOrderID");
    var Customer = $("#CUSTOMERID");
    var Bank = $("#BankID");
    $("#productDetailsGrid").empty();
    $("#freeDetailsGrid").empty();
    $("#dialog").dialog({
        autoOpen: true,
        modal: true,
        title: "Loading.."
    });
    $("#imgLoader").css("visibility", "visible");
    $.ajax({
        type: "POST",
        url: "/tranfac/ExportEdit",
        data: { InvoiceID: invoiceid},
        dataType: "json",
        async: false,
        success: function (response) {
            //alert(JSON.stringify(response))
            var listHeader = response.alleditDataset.varHeader;
            var listDetails = response.alleditDataset.varDetails;
            var listTaxcount = response.alleditDataset.varTaxCount;
            var listTax = response.alleditDataset.varTax;
            var listFooter = response.alleditDataset.varFooter;
            var listFree = response.alleditDataset.varFree;
            var listOrderHeader = response.alleditDataset.varOrderHeader;
            var listProformaHeader = response.alleditDataset.varProformaHeader;
            
            
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
                else {
                    $("#hdntaxcount").val('0');
                    $("#hdntaxnameIGST").val('NA');
                    Igstid = 'NA';
                }
            }

            /*Invoice Header Info*/
            $.each(listHeader, function (index, record) {
                //debugger;
                $("#FGInvoiceNo").val(this['SALEINVOICENO'].toString().trim());
                $("#InvoiceDate").val(this['SALEINVOICEDATE'].toString().trim());
                $("#Tranportmode").val(this['MODEOFTRANSPORT'].toString().trim());
                $("#VesselName").val(this['VEHICHLENO'].toString().trim());
                $("#ExchangeRate").val(this['SELLINGRATE'].toString().trim());
                $("#CountryID").val(this['COUNTRYID'].toString().trim());
                Customer.empty();
                Customer.append($("<option></option>").val(this['DISTRIBUTORID']).html(this['DISTRIBUTORNAME']));
                $("#LrGrNo").val(this['LRGRNO'].toString().trim());
                $("#LrGrDate").val(this['LRGRDATE'].toString().trim());
                $("#ExportRefNo").val(this['EXPORTERREFNO'].toString().trim());
                $("#OtherRefNo").val(this['OTHERREFNO'].toString().trim());
                $("#LoadingPortID").val(this['LOADINGPORTID'].toString().trim());
                $("#DischargePortID").val(this['DISCHARGEPORTID'].toString().trim());
                $("#FinalDestination").val(this['FINALDESTINATION'].toString().trim());
                $("#ShippingBill").val(this['SHIPPINGBILL'].toString().trim());
                $("#ShippingDate").val(this['SHIPPINGDATE'].toString().trim());
                $("#ContainerNo").val(this['CONTAINERNO'].toString().trim());
                $("#VoyNo").val(this['VOYNO'].toString().trim());
                $("#LCNo").val(this['LCNO'].toString().trim());
                if (this['LCDATE'].toString().trim() == '01/01/1900') {
                    $("#LCDate").val('');
                }
                else {
                    $("#LCDate").val(this['LCDATE'].toString().trim());
                }
                $("#LCBank").val(this['LCBANKNO'].toString().trim());
                Bank.empty();
                Bank.append($("<option></option>").val(this['BANKID']).html(this['BANKNAME']));
                $("#Branch").val(this['BRANCHNAME'].toString().trim());
                $("#BankAddress").val(this['BANKADDRESS'].toString().trim());
                $("#IFSC").val(this['IFSCODE'].toString().trim());
                $("#SwiftCode").val(this['SWIFTCODE'].toString().trim());
                $("#AccNo").val(this['ACCOUNTNO'].toString().trim());
                $("#Consignee").val(this['CONSIGNEE'].toString().trim());
                $("#NotifyParty").val(this['NOTIFYPARTIES'].toString().trim());
                $("#TotalCase").val(this['TOTALCASEPACK'].toString().trim());
                $("#TotalPcs").val(parseInt(this['TOTALPCS'].toString().trim()));
                $("#Remarks").val(this['REMARKS'].toString().trim());
                $("#DeliveryTo").val(this['DELIVERYTO'].toString().trim());
                $("#AmountInWords").val(this['AMOUNTINWORD'].toString().trim());
                
            });

            /*Order Header*/
            $("#SaleOrderID").empty();
            $.each(listOrderHeader, function (index, record) {
                debugger;
                OrderID = this['SALEORDERID'].toString().trim();
                Saleorder.append($("<option></option>").val(this['SALEORDERID']).html(this['SALEORDERNO']));
            });

            /*Proforma Header*/
            Proforma.empty();
            $.each(listProformaHeader, function (index, record) {
                //debugger;
                Proforma.append($("<option></option>").val(this['PROFORMAINVOICEID']).html(this['PROFORMANO']));
                $("#ProformaAmount").val(this['PROFORMAVALUE'].toString().trim());
            });
            
            /*Invoice Details Info*/
            if (listDetails.length > 0) {

                $("#productDetailsGrid").empty();
                if ($("#hdntaxcount").val().trim() == '1') {
                    tr = $('#productDetailsGrid');
                    HeaderCount = $('#productDetailsGrid thead th').length;
                    if (HeaderCount == 0) {
                        tr.append("<thead><tr><th>Sl.No.</th><th style='display: none'>Order ID</th><th style='display: none'>Proforma ID</th><th style='display: none'>Order Date</th><th style='display: none'>Proforma Date</th><th style='display: none'>Product ID</th><th>HSN Code</th><th>Product</th><th>Batch</th><th style='display: none'>PackSize ID</th><th>Pack Size</th><th>MRP</th><th>Case</th><th>Pcs</th><th>Rate</th><th>Exchange Rate(INR)</th><th style='display: none'>Assesment(%)</th><th style='display: none'>Assesment Amount</th><th>Amount</th><th>Amount(INR)</th><th>SKU</th><th>Mfg.Date</th><th>Exp.Date</th><th>IGST(%)</th><th>IGST</th><th>Net Amt.(INR)</th><th style='display: none'>Tag</th><th>Delete</th></tr></thead><tbody>");
                    }
                    $.each(listDetails, function (index, record) {
                        //debugger;
                        tr = $('<tr/>');
                        tr.append("<td style='text-align: center'>" + srl + "</td>");//0
                        tr.append("<td style='display: none'>" + this['SALEORDERID'].toString().trim() + "</td>");//1
                        tr.append("<td style='display: none'>" + this['PROFORMAINVOICEID'].toString().trim() + "</td>");//2
                        tr.append("<td style='display: none'>" + this['SALEORDERDATE'].toString().trim() + "</td>");//3
                        tr.append("<td style='display: none'>" + this['PROFORMAINVOICEDATE'].toString().trim() + "</td>");//4
                        tr.append("<td style='display: none'>" + this['PRODUCTID'].toString().trim() + "</td>");//5
                        tr.append("<td style='text-align: center'>" + this['HSNCODE'].toString().trim() + "</td>");//6
                        tr.append("<td>" + this['PRODUCTNAME'].toString().trim() + "</td>");//7
                        tr.append("<td style='text-align: center'>" + this['BATCHNO'].toString().trim() + "</td>");//8
                        tr.append("<td style='display: none'>" + this['PACKINGSIZEID'].toString().trim() + "</td>");//9
                        tr.append("<td style='text-align: center'>" + this['PACKINGSIZENAME'].toString().trim() + "</td>");//10
                        tr.append("<td style='text-align: right'>" + this['MRP'].toString().trim() + "</td>");//11
                        tr.append("<td style='text-align: right'>" + parseFloat(this['QTY'].toString().trim()).toFixed(3) + "</td>");//12
                        tr.append("<td style='text-align: right'>" + parseFloat(this['QTYPCS'].toString().trim()).toFixed(0) + "</td>");//13
                        tr.append("<td style='text-align: right'>" + parseFloat(this['RATE'].toString().trim()).toFixed(4) + "</td>");//14
                        tr.append("<td style='text-align: right'>" + parseFloat(this['NSR'].toString().trim()).toFixed(4) + "</td>");//15
                        tr.append("<td style='display: none'>" + this['ASSESMENTPERCENTAGE'].toString().trim() + "</td>");//16
                        tr.append("<td style='display: none'>" + this['TOTALASSESMENTVALUE'].toString().trim() + "</td>");//17
                        tr.append("<td style='text-align: right'>" + this['AMOUNT'].toString().trim() + "</td>");//18
                        tr.append("<td style='text-align: right'>" + this['AMOUNTINR'].toString().trim() + "</td>");//19
                        tr.append("<td>" + this['WEIGHT'].toString().trim() + "</td>");//20
                        tr.append("<td style='text-align: center;'>" + this['MFDATE'].toString().trim() + "</td>");//21
                        tr.append("<td style='text-align: center;'>" + this['EXPRDATE'].toString().trim() + "</td>");//22
                        IgstPercentage = GetTaxOnEdit(invoiceid.trim(), Igstid, this['PRODUCTID'].toString().trim(), this['BATCHNO'].toString().trim());
                        IgstAmount = ((parseFloat(this['AMOUNTINR'].toString().trim()) * IgstPercentage) / 100);
                        NetAmount = (parseFloat(this['AMOUNTINR'].toString().trim()) + IgstAmount);
                        tr.append("<td style='text-align: right'>" + parseFloat(IgstPercentage).toFixed(2) + "</td>");//23
                        tr.append("<td style='text-align: right'>" + IgstAmount.toFixed(2) + "</td>");//24
                        tr.append("<td style='text-align: right'>" + NetAmount.toFixed(2) + "</td>");//25
                        tr.append("<td style='display: none'>" + this['TAG'].toString().trim() + "</td>");//26
                        tr.append("<td style='text-align: center'><input type='image' class='gvTempDelete'  id='btndelete' <img src='../Images/ico_delete_16.png'/></input></td>");
                        $("#productDetailsGrid").append(tr);
                    });
                    tr.append("</tbody>");
                    RowCountEdit();
                }
            }

            /*Tax Details Info*/
            if ($("#hdntaxcount").val() == '1') {
                $.each(listTax, function (index, record) {
                    debugger;
                    addRowinTaxTableEdit(this['PRODUCTID'].toString().trim(), this['BATCHNO'].toString().trim(), this['TAXID'].toString().trim(), this['TAXPERCENTAGE'].toString().trim(), this['TAXVALUE'].toString().trim(), this['MRP'].toString().trim(), '1');
                });
            }
            else {
                addRowinTaxTableEdit('NA','NA','NA',0, 0, 0, '0');
            }

            /*Free Details Info*/
            if (listFree.length > 0) {

                $("#freeDetailsGrid").empty();
                tr = $('#freeDetailsGrid');
                HeaderCount = $('#freeDetailsGrid thead th').length;
                if (HeaderCount == 0) {
                    tr.append("<thead><tr><th>Sl.No.</th><th style='display: none'>Order ID</th><th style='display: none'>Proforma ID</th><th style='display: none'>Order Date</th><th style='display: none'>Proforma Date</th><th style='display: none'>Product ID</th><th>HSN Code</th><th>Product</th><th>Batch</th><th style='display: none'>PackSize ID</th><th>Pack Size</th><th>MRP</th><th>Case</th><th>Pcs</th><th>Rate</th><th>Exchange Rate(INR)</th><th style='display: none'>Assesment(%)</th><th style='display: none'>Assesment Amount</th><th>Amount</th><th>Amount(INR)</th><th>SKU</th><th>Mfg.Date</th><th>Exp.Date</th><th>Net Amt.(INR)</th><th>Delete</th></tr></thead><tbody>");
                }
                $.each(listFree, function (index, record) {
                    //debugger;
                    tr = $('<tr/>');
                    tr.append("<td style='text-align: center'>" + srl + "</td>");//0
                    tr.append("<td style='display: none'>" + this['SALEORDERID'].toString().trim() + "</td>");//1
                    tr.append("<td style='display: none'>" + this['SALEORDERID'].toString().trim() + "</td>");//2
                    tr.append("<td style='display: none'>01/01/1900</td>");//3
                    tr.append("<td style='display: none'>01/01/1900</td>");//4
                    tr.append("<td style='display: none'>" + this['PRODUCTID'].toString().trim() + "</td>");//5
                    tr.append("<td style='text-align: center'>" + this['HSNCODE'].toString().trim() + "</td>");//6
                    tr.append("<td>" + this['PRODUCTNAME'].toString().trim() + "</td>");//7
                    tr.append("<td style='text-align: center'>" + this['BATCHNO'].toString().trim() + "</td>");//8
                    tr.append("<td style='display: none'>" + this['PACKSIZEID'].toString().trim() + "</td>");//9
                    tr.append("<td style='text-align: center'>" + this['PACKSIZENAME'].toString().trim() + "</td>");//10
                    tr.append("<td style='text-align: right'>" + this['MRP'].toString().trim() + "</td>");//11
                    tr.append("<td style='text-align: right'>" + parseFloat(this['SCHEME_QTY_CASE'].toString().trim()).toFixed(3) + "</td>");//12
                    tr.append("<td style='text-align: right'>" + parseFloat(this['SCHEME_QTY_PCS'].toString().trim()).toFixed(0) + "</td>");//13
                    tr.append("<td style='text-align: right'>" + this['BRATE'].toString().trim() + "</td>");//14
                    tr.append("<td style='text-align: right'>" + this['NSR'].toString().trim() + "</td>");//15
                    tr.append("<td style='display: none'>" + this['ASSESMENTPERCENTAGE'].toString().trim() + "</td>");//16
                    tr.append("<td style='display: none'>" + this['TOTALASSESMENTVALUE'].toString().trim() + "</td>");//17
                    tr.append("<td style='text-align: right'>" + this['AMOUNT'].toString().trim() + "</td>");//18
                    tr.append("<td style='text-align: right'>" + this['RATEDISC'].toString().trim() + "</td>");//19
                    tr.append("<td>" + this['WEIGHT'].toString().trim() + "</td>");//20
                    tr.append("<td style='text-align: center;'>" + this['MFDATE'].toString().trim() + "</td>");//21
                    tr.append("<td style='text-align: center;'>" + this['EXPRDATE'].toString().trim() + "</td>");//22
                    NetAmount = (parseFloat(this['RATEDISC'].toString().trim()));
                    tr.append("<td style='text-align: right'>" + NetAmount.toFixed(2) + "</td>");//23
                    tr.append("<td style='text-align: center'><input type='image' class='gvFreeDelete'  id='btnfreedelete' <img src='../Images/ico_delete_16.png'/></input></td>");
                    $("#freeDetailsGrid").append(tr);
                });
                tr.append("</tbody>");
                FreeRowCount();
            }

            /*Footer Details Info*/
            if (listFooter.length > 0) {
                $.each(listFooter, function (index, record) {
                    debugger;
                    $('#BasicAmt').val(parseFloat(this['BASICAMOUNT'].toString().trim()).toFixed(2));
                    $('#RoundOff').val(parseFloat(this['ROUNDOFFVALUE'].toString().trim()).toFixed(2));
                    $('#TaxAmt').val(parseFloat(this['TOTALTAXAMT'].toString().trim()).toFixed(2));
                    $('#GrossAmt1').val(parseFloat(this['GROSSAMOUNT'].toString().trim()).toFixed(2));
                    $('#GrossAmt').val(parseFloat(this['GROSSAMOUNT'].toString().trim()).toFixed(2));
                    $('#NetAmt').val(parseFloat(this['TOTALSALEINVOICEVALUE'].toString().trim()).toFixed(2));
                    $("#AdjustmentAmount").val(this['ADJUSTMENTVALUE'].toString().trim());
                });
            }

            
           
            //bindExportSaleOrder($('#CountryID').val().trim(), invoiceid, OrderID);
            bindFgProduct(BusinessSegment, invoiceid);
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

function LoadProformaDetails(saleorderid, depotid, proformaid, customerid, exchangerate) {
    debugger;
    var srl = 0;
    srl = srl + 1;
    var IgstPercentage = 0;
    var IgstAmount = 0;
    var NetAmount = 0;
    $("#dialog").dialog({
        autoOpen: true,
        modal: true,
        title: "Loading.."
    });
    $("#imgLoader").css("visibility", "visible");
    $.ajax({
        
        type: "POST",
        url: "/tranfac/BindProformaList",
        data: { SaleorderID: saleorderid, DepotID: depotid, ProformaID: proformaid, CustomerID: customerid, ExchangeRate: exchangerate },
        dataType: "json",
        async: false,
        success: function (response) {
            debugger;
            /*IGST*/
            var tr;
            var HeaderCount = 0;
            //alert($("#hdntaxcount").val());
            $("#productDetailsGrid").empty();
            if (response.length > 0) {
                if ($("#hdntaxcount").val().trim() == '1') {
                    tr = $('#productDetailsGrid');
                    HeaderCount = $('#productDetailsGrid thead th').length;
                    if (HeaderCount == 0) {
                        tr.append("<thead><tr><th>Sl.No.</th><th style='display: none'>Order ID</th><th style='display: none'>Proforma ID</th><th style='display: none'>Order Date</th><th style='display: none'>Proforma Date</th><th style='display: none'>Product ID</th><th>HSN Code</th><th>Product</th><th>Batch</th><th style='display: none'>PackSize ID</th><th>Pack Size</th><th>MRP</th><th>Case</th><th>Pcs</th><th>Rate</th><th>Exchange Rate(INR)</th><th style='display: none'>Assesment(%)</th><th style='display: none'>Assesment Amount</th><th>Amount</th><th>Amount(INR)</th><th>SKU</th><th>Mfg.Date</th><th>Exp.Date</th><th>IGST(%)</th><th>IGST</th><th>Net Amt.(INR)</th><th style='display: none'>Tag</th><th>Delete</th></tr></thead><tbody>");
                    }
                    for (var i = 0; i < response.length; i++) {
                        tr = $('<tr/>');
                        tr.append("<td style='text-align: center'>" + srl + "</td>");//0
                        tr.append("<td style='display: none'>" + response[i].SALEORDERID + "</td>");//1
                        tr.append("<td style='display: none'>" + response[i].PROFORMAINVOICEID + "</td>");//2
                        tr.append("<td style='display: none'>" + response[i].SALEORDERDATE + "</td>");//3
                        tr.append("<td style='display: none'>" + response[i].PROFORMAINVOICEDATE + "</td>");//4
                        tr.append("<td style='display: none'>" + response[i].PRODUCTID + "</td>");//5
                        tr.append("<td style='text-align: center'>" + response[i].HSNCODE + "</td>");//6
                        tr.append("<td>" + response[i].PRODUCTNAME + "</td>");//7
                        tr.append("<td style='text-align: center'>" + response[i].BATCHNO + "</td>");//8
                        tr.append("<td style='display: none'>" + response[i].PACKINGSIZEID + "</td>");//9
                        tr.append("<td style='text-align: center'>" + response[i].PACKINGSIZENAME + "</td>");//10
                        tr.append("<td style='text-align: right'>" + response[i].MRP + "</td>");//11
                        tr.append("<td style='text-align: right'>" + parseFloat(response[i].QTY).toFixed(3) + "</td>");//12
                        tr.append("<td style='text-align: right'>" + parseFloat(response[i].QTYPCS).toFixed(0) + "</td>");//13
                        tr.append("<td style='text-align: right'>" + response[i].BCP + "</td>");//14
                        tr.append("<td style='text-align: right'>" + response[i].INR + "</td>");//15
                        tr.append("<td style='display: none'>" + response[i].ASSESMENTPERCENTAGE + "</td>");//16
                        tr.append("<td style='display: none'>" + response[i].TOTALASSESMENTVALUE + "</td>");//17
                        tr.append("<td style='text-align: right'>" + response[i].AMOUNT + "</td>");//18
                        tr.append("<td style='text-align: right'>" + response[i].AMOUNTINR + "</td>");//19
                        tr.append("<td>" + response[i].WEIGHT + "</td>");//20
                        tr.append("<td style='text-align: center;'>" + response[i].MFDATE + "</td>");//21
                        tr.append("<td style='text-align: center;'>" + response[i].EXPRDATE + "</td>");//22
                        IgstPercentage = getProformaTax(response[i].PRODUCTID.trim(), $("#hdntaxnameIGST").val().trim(), $("#InvoiceDate").val().trim());
                        IgstAmount = ((parseFloat(response[i].AMOUNTINR.toString().trim()) * IgstPercentage) / 100);
                        NetAmount = (parseFloat(response[i].AMOUNTINR.trim()) + IgstAmount);
                        tr.append("<td style='text-align: right'>" + parseFloat(IgstPercentage).toFixed(2) + "</td>");//23
                        tr.append("<td style='text-align: right'>" + IgstAmount.toFixed(2) + "</td>");//24
                        tr.append("<td style='text-align: right'>" + NetAmount.toFixed(2) + "</td>");//25
                        tr.append("<td style='display: none'>0</td>");//26
                        tr.append("<td style='text-align: center'><input type='image' class='gvTempDelete'  id='btndelete' <img src='../Images/ico_delete_16.png'/></input></td>");


                        $("#productDetailsGrid").append(tr);

                        /*Tax Details Info*/
                        addRowinTaxTableEdit(response[i].PRODUCTID.trim(), response[i].BATCHNO.trim(), '1177D9CF-8F1E-4C91-B785-FDF940101EEE', IgstPercentage, IgstAmount, response[i].MRP.trim(), '1');
                    }
                    tr.append("</tbody>");
                    RowCount();
                    CalculateAmount();
                }
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

function DeleteInvoice(invoiceid) {
    debugger;
   
    $.ajax({
        type: "POST",
        url: "/tranfac/fgdeleteInvoice",
        data: { InvoiceID: invoiceid},
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
                bindexportinvoicegrid();
                toastr.success('<b><font color=black>' + messagetext + '</font></b>');
            }
            else {
                toastr.error('<b><font color=black>' + messagetext + '</font></b>');
            }
        }
    });
}

function getconversionqty(productid,packsizefrom,packsizeto,qty) {
    var returnqty = null;
    $.ajax({
        type: "POST",
        url: "/tranfac/GetInvoiceConvertedQty",
        data: { ProductID: productid.trim(), PacksizeFrom: packsizefrom.trim(), PacksizeTo: packsizeto.trim(), Qty: qty },
        dataType: "json",
        async: false,
        success: function (convertedqtylist) {
            //alert(JSON.stringify(shipping));
            var qty;
            $.each(convertedqtylist, function (key, item) {
                qty = item.CONVERTED_QTY;
                returnqty = qty;
                return false;
            });
        },
        failure: function (convertedqtylist) {
            alert(convertedqtylist.responseText);
        },
        error: function (convertedqtylist) {
            alert(convertedqtylist.responseText);
        }
    });
    return returnqty;
}

function itemLedger() {

    $('#productDetailsGrid').empty();
    $('#freeDetailsGrid').empty();
    ReleaseSession();

    EditDetails(VoucherID);

    
    $('#btnAddnew').css("display", "none");
    $('#btnsave').css("display", "none");
    $('#btnApprove').css("display", "none");
    
    $("#dvAdd").find("input, textarea, select,submit").attr("disabled", "disabled");
    $('#divTransferNo').css("display", "");
    $('#divSourceDepot').css("display", "none");
    $('#dvProduct').css("display", "none");
    $("#btnclose").prop("disabled", false);
    $('#dvAdd').css("display", "");
    $('#dvDisplay').css("display", "none");

    $("#BRID").chosen({
        search_contains: true
    });
    $("#BRID").trigger("chosen:updated");

    $("#CUSTOMERID").empty();
    $("#CUSTOMERID").chosen({
        search_contains: true
    });
    $("#CUSTOMERID").trigger("chosen:updated");

    $("#SaleOrderID").empty();
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
    

    $("#InvoiceDate").datepicker("destroy");
}

function LoadPackingList(invoiceid,invoice,qty) {
    debugger;
    var srl = 0;
    srl = srl + 1;
    $('#PackInvoiceNo').val(invoice);
    $('#PackCaseQty').val(qty);
    $("#packinglistGrid").empty();
    $("#dialog").dialog({
        autoOpen: true,
        modal: true,
        title: "Loading.."
    });
    $("#imgLoader").css("visibility", "visible");
    $.ajax({

        type: "POST",
        url: "/tranfac/BindPackingList",
        data: { InvoiceID: invoiceid},
        dataType: "json",
        async: false,
        success: function (response) {
            debugger;
            
            var tr;
            var HeaderCount = 0;
            //alert($("#hdntaxcount").val());
            $("#packinglistGrid").empty();
            if (response.length > 0) {

                $("#DescPackages").val(response[0].DESCPACKAGES.trim());

                tr = $('#packinglistGrid');
                HeaderCount = $('#packinglistGrid thead th').length;
                if (HeaderCount == 0) {
                    tr.append("<thead><tr><th>Sl.No.</th><th style='display: none'>Invoice ID</th><th style='display: none'>Order ID</th><th style='display: none'>Product ID</th><th>Product</th><th>Batch</th><th>Qty(Case)</th><th>Start Position</th><th>End Position</th><th>Gross Wght</th><th>Net Wght</th><th style='display: none'>Category</th></tr></thead><tbody>");
                }
                for (var i = 0; i < response.length; i++) {
                    tr = $('<tr/>');
                    debugger;
                    tr.append("<td style='text-align: center'>" + srl + "</td>");//0
                    tr.append("<td style='display: none'>" + response[i].SALEINVOICEID.trim() + "</td>");//1
                    tr.append("<td style='display: none'>" + response[i].SALEORDERID.trim() + "</td>");//2
                    tr.append("<td style='display: none'>" + response[i].PRODUCTID.trim() + "</td>");//3
                    tr.append("<td>" + response[i].PRODUCTNAME.trim() + "</td>");//4
                    tr.append("<td style='text-align: center'>" + response[i].BATCHNO.trim() + "</td>");//5
                    tr.append("<td style='text-align: right'>" + response[i].QTY.trim() + "</td>");//6
                    tr.append("<td style='text-align: right'><input type='text' class='gvstartposition'  id='txtstart' style='text-align: right; width:60px; height:18px' value=" + response[i].STARTPOSITION.trim() + "></input></td>");//7
                    tr.append("<td style='text-align: right'><input type='text' class='gvendposition'  id='txtend' style='text-align: right; width:60px; height:18px' value=" + response[i].ENDPOSITION.trim() + " disabled></input></td>");//8
                    tr.append("<td style='text-align: right'><input type='text' class='gvgrosswght'  id='txtgross' style='text-align: right; width:60px; height:18px' value=" + response[i].GROSSWEIGHT.trim().split(" ").join("") + "></input></td>");//9
                    tr.append("<td style='text-align: right'><input type='text' class='gvnetwght'  id='txtnet' style='text-align: right; width:60px; height:18px' value=" + response[i].NETWEIGHT.trim().split(" ").join("") + "></input></td>");//10
                    tr.append("<td style='display: none'>" + response[i].CATNAME.trim() + "</td>");//11
                    $("#packinglistGrid").append(tr);
                }
                tr.append("</tbody>");
                PackingListRowCount();
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


/*Delete function for item details grid*/
$(function () {
    var grdproductid;
    var grdbatchno;
    var tag;
    var deleteflag = 0;
    $("body").on("click", "#productDetailsGrid .gvTempDelete", function () {
        var row = $(this).closest("tr");
        grdproductid = row.find('td:eq(5)').html();
        grdbatchno = row.find('td:eq(8)').html();
        tag = row.find('td:eq(26)').html();
        if (tag == '0') {
            toastr.info('<b>Not allow to delete as received through allocation.</b>');
            return false;
        }
        else {
            if (confirm("Do you want to delete this item?")) {
                deleteflag = 1;
                row.remove();
                var rowCount = document.getElementById("productDetailsGrid").rows.length - 1;
                if (rowCount == 0) {
                    $("#productDetailsGrid").empty();
                }
                RowCount();
                CalculateAmount();
                deleteRowfromTaxTable(grdproductid, grdbatchno);
            }
            var rowCount = document.getElementById("productDetailsGrid").rows.length - 1;
            if (rowCount > 0) {
                $("#ExchangeRate").prop("disabled", true);
            }
            else {
                $("#ExchangeRate").prop("disabled", false);
            }
        }
    })

})

/*Delete function for free details grid*/
$(function () {
    var grdproductid;
    var grdbatchno;
    var deleteflag = 0;
    $("body").on("click", "#freeDetailsGrid .gvFreeDelete", function () {
        var row = $(this).closest("tr");
        grdproductid = row.find('td:eq(5)').html();
        grdbatchno = row.find('td:eq(8)').html();
        if (confirm("Do you want to delete this item?")) {
            deleteflag = 1;
            row.remove();
            var rowCount = document.getElementById("freeDetailsGrid").rows.length - 1; 
            if (rowCount == 0) {
                $("#freeDetailsGrid").empty();
            }
            FreeRowCount();
            CalculateAmount();
        }
        
    })

})

/*Edit function for Invoice*/
$(function () {
    var invoiceid;
    $("body").on("click", "#invoiceGridFG .gvEdit", function () {
        var row = $(this).closest("tr");
        invoiceid = row.find('td:eq(1)').html();
        $('#hdndispatchID').val(invoiceid);
        var financestatus = FinanceStatus($('#hdndispatchID').val().trim());

       
        if (financestatus != 'na') {
            $('#dvAdd').css("display", "none");
            $('#dvDisplay').css("display", "");
            toastr.info('<b>' + financestatus + '</b>');
            return false;
        }
        else {
            $('#productDetailsGrid').empty();
            $('#freeDetailsGrid').empty();
            ReleaseSession();

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
            $('#divSourceDepot').css("display", "none");
            $('#dvProduct').css("display", "none");
            $("#FGInvoiceNo").attr("disabled", "disabled");
            $("#ShippingDate").attr("disabled", "disabled");
            $("#ExchangeRate").attr("disabled", "disabled");
            $("#ExportRefNo").attr("disabled", "disabled");
            $("#LCNo").attr("disabled", "disabled");
            $("#LCDate").attr("disabled", "disabled");
            $("#LCBank").attr("disabled", "disabled");
            $("#InvoiceDate").attr("disabled", "disabled");
            $("#LrGrDate").attr("disabled", "disabled");
            $("#MRP").attr("disabled", "disabled");
            $("#StockQty").attr("disabled", "disabled");
            $("#Rate").attr("disabled", "disabled");
            $("#OrderQty").attr("disabled", "disabled");
            $("#DeliveredQty").attr("disabled", "disabled");
            $("#RemainingQty").attr("disabled", "disabled");
            $("#BasicAmt").attr("disabled", "disabled");
            $("#TaxAmt").attr("disabled", "disabled");
            $("#GrossAmt1").attr("disabled", "disabled");
            $("#GrossAmt").attr("disabled", "disabled");
            $("#RoundOff").attr("disabled", "disabled");
            $("#NetAmt").attr("disabled", "disabled");
            $("#TotalCase").attr("disabled", "disabled");
            $("#TotalPcs").attr("disabled", "disabled");
            $("#ProformaAmount").attr("disabled", "disabled");
            $("#AdjustmentAmount").attr("disabled", "disabled");
            $("#BRID").attr("disabled", "disabled");
            $("#radio_Export").attr("disabled", "disabled");
            $("#radio_Domestic").attr("disabled", "disabled");
            $('#dvAdd').css("display", "");
            $('#dvDisplay').css("display", "none");

            $("#BRID").chosen({
                search_contains: true
            });
            $("#BRID").trigger("chosen:updated");

            $("#CountryID").prop("disabled", true);
            $("#CountryID").chosen({
                search_contains: true
            });
            $("#CountryID").trigger("chosen:updated");
            
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
            
            $("#ProformaOrderID").prop("disabled", true);
            $("#ProformaOrderID").chosen({
                search_contains: true
            });
            $("#ProformaOrderID").trigger("chosen:updated");
           
            $("#LoadingPortID").chosen({
                search_contains: true
            });
            $("#LoadingPortID").trigger("chosen:updated");
           
            $("#DischargePortID").chosen({
                search_contains: true
            });
            $("#DischargePortID").trigger("chosen:updated");

            $("#Tranportmode").prop("disabled", true);

            $("#PRODUCTID").val('0');
            $("#PRODUCTID").chosen({
                search_contains: true
            });
            $("#PRODUCTID").trigger("chosen:updated");

            $("#PSID").chosen({
                search_contains: true
            });
            $("#PSID").trigger("chosen:updated");

            $("#BankID").chosen({
                search_contains: true
            });
            $("#BankID").trigger("chosen:updated");
           

            $("#InvoiceDate").datepicker("destroy");
        }
    })

})

/*View function for Invoice*/
$(function () {
    var invoiceid;
    $("body").on("click", "#invoiceGridFG .gvView", function () {
        var row = $(this).closest("tr");
        invoiceid = row.find('td:eq(1)').html();
        $('#hdndispatchID').val(invoiceid);
       
        $('#productDetailsGrid').empty();
        $('#freeDetailsGrid').empty();
        ReleaseSession();

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
        $("#dvAdd").find("input, textarea, select,submit,radio").attr("disabled", "disabled"); 
        $('#divTransferNo').css("display", "");
        $('#divSourceDepot').css("display", "none");
        $('#dvProduct').css("display", "none");
        $("#btnclose").prop("disabled",false);
        $('#dvAdd').css("display", "");
        $('#dvDisplay').css("display", "none");

        $("#BRID").chosen({
            search_contains: true
        });
        $("#BRID").trigger("chosen:updated");

        $("#CountryID").chosen({
            search_contains: true
        });
        $("#CountryID").trigger("chosen:updated");

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

        $("#ProformaOrderID").prop("disabled", true);
        $("#ProformaOrderID").chosen({
            search_contains: true
        });
        $("#ProformaOrderID").trigger("chosen:updated");

        $("#LoadingPortID").chosen({
            search_contains: true
        });
        $("#LoadingPortID").trigger("chosen:updated");

        $("#DischargePortID").chosen({
            search_contains: true
        });
        $("#DischargePortID").trigger("chosen:updated");

        
       

        $("#PRODUCTID").val('0');
        $("#PRODUCTID").chosen({
            search_contains: true
        });
        $("#PRODUCTID").trigger("chosen:updated");

        $("#PSID").chosen({
            search_contains: true
        });
        $("#PSID").trigger("chosen:updated");

        $("#BankID").chosen({
            search_contains: true
        });
        $("#BankID").trigger("chosen:updated");

        $("#InvoiceDate").datepicker("destroy");

        
    })

})

/*Delete function for Invoice*/
$(function () {
    var dispatchid;
    $("body").on("click", "#invoiceGridFG .gvCancel", function () {
        var row = $(this).closest("tr");
        dispatchid = row.find('td:eq(1)').html();
        $('#hdndispatchID').val(dispatchid);
        if (confirm("Do you want to delete this item?")) {
            if (CHECKER == 'FALSE') {
                var financestatus = FinanceStatus($('#hdndispatchID').val().trim());
                if (financestatus != 'na') {
                    toastr.warning('<b>Finance approve already done,not allow to cancel...!</b>');
                    return false;
                }
                else {
                    DeleteInvoice(dispatchid);
                }
            }
            else {
                toastr.warning('<b>not allow to cancel...!</b>');
            }
        }

    })

})

/*Print function for Tax Invoice*/
$(function () {
    var invoiceid;
    var type = 'EXPORTGST';
    $("body").on("click", "#invoiceGridFG .gvPrint", function () {
        var row = $(this).closest("tr");
        invoiceid = row.find('td:eq(1)').html();
        $('#hdndispatchID').val(invoiceid);
        var url = "http://mcnroeerp.com/factory/FACTORY/frmPrintPopUpV2_FAC.aspx?pid=" + invoiceid + "&BSID=" + BusinessSegment + "&PSID=" + type;
        window.open(url, "", "width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100");
    })
})

/*Print function for Commercial Invoice*/
$(function () {
    var invoiceid;
    var type = 'EXPORT';
    $("body").on("click", "#invoiceGridFG .gvCommPrint", function () {
        var row = $(this).closest("tr");
        invoiceid = row.find('td:eq(1)').html();
        $('#hdndispatchID').val(invoiceid);
        var url = "http://mcnroeerp.com/factory/FACTORY/frmRptInvoicePrint_FAC.aspx?pid=" + invoiceid + "&BSID=" + BusinessSegment + "&PSID=" + type;
        window.open(url, "", "width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100");
    })
})

/*Print function for Packing List*/
$(function () {
    var invoiceid;
    var type = 'PACKING';
    $("body").on("click", "#invoiceGridFG .gvPackingPrint", function () {
        var row = $(this).closest("tr");
        invoiceid = row.find('td:eq(1)').html();
        $('#hdndispatchID').val(invoiceid);
        var url = "http://mcnroeerp.com/factory/FACTORY/frmRptInvoicePrint_FAC.aspx?pid=" + invoiceid + "&BSID=" + BusinessSegment + "&PSID=" + type;
        window.open(url, "", "width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100");
    })
})

/*Print function for Weight List*/
$(function () {
    var invoiceid;
    var type = 'WEIGHT';
    $("body").on("click", "#invoiceGridFG .gvWeightPrint", function () {
        var row = $(this).closest("tr");
        invoiceid = row.find('td:eq(1)').html();
        $('#hdndispatchID').val(invoiceid);
        var url = "http://mcnroeerp.com/factory/FACTORY/frmRptInvoicePrint_FAC.aspx?pid=" + invoiceid + "&BSID=" + BusinessSegment + "&PSID=" + type;
        window.open(url, "", "width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100");
    })
})

/*Packing List Popup*/
$(function () {
    var invoiceid;
    var invoiceno;
    var quantity;
    $("body").on("click", "#invoiceGridFG .gvPackingList", function () {
        var row = $(this).closest("tr");
        invoiceid = row.find('td:eq(1)').html();
        invoiceno = row.find('td:eq(2)').html();
        quantity = row.find('td:eq(14)').html();
        $('#hdndispatchID').val(invoiceid);
        LoadPackingList(invoiceid, invoiceno, quantity);
        $("#dvpackinglist").dialog("open");
        
    })

})

/*Packing List Grid Text Change*/
$(function () {
    var qty = 0;
    var start = 0;
    var end = 0;
    $("body").on("keyup", "#packinglistGrid .gvstartposition", function () {
        var row = $(this).closest("tr");
        qty = row.find('td:eq(6)').html().trim();
        start = row.find("#txtstart").val().trim();
        end = ((parseFloat(qty) + parseFloat(start)) - 1).toFixed(0);
        row.find("#txtend").val(end);
        
    })

})

