//#region Developer Info
/*
* For FactoryInvoiceTrading.cshtml Only
* Developer Name : Avishek Ghosh 
* Start Date     : 19/03/2020
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
        bindtradinginvoicegrid();
        bindStorelocation();
        bindWayBill();
        bindInsurance();
        $("#BRID").prop("disabled", true);
        $("#CUSTOMERID").prop("disabled", false);
    }
    else if (Name == 'Itemledger') {
        bindsourceDepot();
        bindWayBill();
        bindInsurance();
        itemLedger();
    }

    $("#btnsearch").click(function (e) {
        bindtradinginvoicegrid();
        e.preventDefault();
    })

    $("#btnAddnew").click(function (e) {
        $('#dvAdd').css("display", "");
        $('#dvDisplay').css("display", "none");
        ClearControls();
        ReleaseSession();
        finyearChecking();
        e.preventDefault();
    })

    $("#btnclose").click(function (e) {
        $('#dvAdd').css("display", "none");
        $('#dvDisplay').css("display", "");
        ClearControls();
        ReleaseSession();
        bindtradinginvoicegrid();
        e.preventDefault();
    })

    $('#ID').change(function (e) {
        
        if ($('#ID').val() != '0') {
            bindPolicyNo($('#ID').val(),'0','0');
        }
        else {
            $('#INSURANCE_NO').empty();
        }
        $("#INSURANCE_NO").focus();
        $("#INSURANCE_NO").chosen({
            search_contains: true
        });
        $('#INSURANCE_NO').trigger('chosen:updated');
        e.preventDefault();
    })

    $('#CUSTOMERID').change(function (e) {
        if ($('#CUSTOMERID').val() != '0') {
            bindFgSaleOrder($('#CUSTOMERID').val().trim(), $('#BRID').val().trim(), '0', '0');
            bindshippingAddress();
            bindtaxcount();
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
        }
        else if ($('#CUSTOMERID').val() == '0') {
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
            $('#PSID').empty();
            $("#PSID").chosen({
                search_contains: true
            });
            $("#PSID").trigger("chosen:updated");
        }
        clearafterAdd();
        e.preventDefault();
    })

    $('#SaleOrderID').change(function (e) {
        if ($('#SaleOrderID').val() != '0') {
            bindFgProduct();
        }
        else if ($('#SaleOrderID').val() == '0') {
            $('#PRODUCTID').empty();
            $("#PRODUCTID").chosen({
                search_contains: true
            });
            $("#PRODUCTID").trigger("chosen:updated");
        }
        e.preventDefault();
    })

    $('#PRODUCTID').change(function (e) {
        debugger;
        var storelocation = $("#StorelocationID").val();
        var product = $("#PRODUCTID").val();
        if (storelocation == '0') {
            toastr.warning('<b><font color=black>Please select Storelocation..!</font></b>');
            return false;
        }
        if (product != '0') {
            bindorderdetails();
        }
        else {
            $('#PSID').empty();
            $("#PSID").chosen({
                search_contains: true
            });
            $("#PSID").trigger("chosen:updated");
        }
        $("#PSID").focus();
        $('#PSID').trigger('chosen:updated');
        

        e.preventDefault();
    })
    

    $("#btnadd").click(function (e) {
        debugger;
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
            if ($("#CUSTOMERID").val() == '0') {
                toastr.warning('<b><font color=black>Please select customer..!</font></b>');
                return false;
            }
            else if ($("#TransporterID").val() == '0') {
                toastr.warning('<b><font color=black>Please select transporter..!</font></b>');
                return false;
            }
            else if ($("#LrGrNo").val() == '') {
                toastr.warning('<b><font color=black>Please enter LR no..!</font></b>');
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

    $('#StorelocationID').change(function (e) {
        debugger;
        var storelocation = $("#StorelocationID").val();
        var product = $("#PRODUCTID").val();
       
        if (storelocation != '0' && product != '0') {
            bindorderdetails();
        }
        else {
            $('#PSID').empty();
            $("#PSID").chosen({
                search_contains: true
            });
            $("#PSID").trigger("chosen:updated");
        }
        $("#PSID").focus();
        $('#PSID').trigger('chosen:updated');


        e.preventDefault();
    })
    
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

    backdatestatus = bindbackdateflag('4197');
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

function bindStorelocation() {
    var Storelocation = $("#StorelocationID");
    $.ajax({
        type: "POST",
        url: "/tranfac/GetTradingStorelocation",
        data: '{}',
        async: false,
        dataType: "json",

        success: function (response) {
            Storelocation.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(response, function () {
                Storelocation.append($("<option></option>").val(this['StorelocationID']).html(this['StorelocationName']));
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
            var sourceDepot = $('#BRID').val();
            bindCustomer(sourceDepot, BusinessSegment,'');
            bindtransporter(sourceDepot, '0');
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function bindCustomer(sourceDepot,bsid,groupid) {
    var Customer = $("#CUSTOMERID");
    $.ajax({
        type: "POST",
        url: "/tranfac/GetTradingInvoiceCustomer",
        data: { FactoryID: sourceDepot, BSID: bsid, GroupID: groupid },
        async: false,
        dataType: "json",
        success: function (response) {
            //alert(JSON.stringify(response));
            Customer.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(response, function () {
                Customer.append($("<option></option>").val(this['CUSTOMERID']).html(this['CUSTOMERNAME']));
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

function bindWayBill() {
    var waybill = $("#WAYBILLID");
    $.ajax({
        type: "POST",
        url: "/tranfac/GetWaybillno",
        data: { DepotID: $('#CUSTOMERID').val() },
        async: false,
        dataType: "json",
        success: function (response) {
            waybill.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(response, function () {
                waybill.append($("<option></option>").val(this['WAYBILLID']).html(this['WAYBILLNO']));
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

function bindInsurance() {
    var counter = 0;
    var insuranceCompany = $("#ID");
    $.ajax({
        type: "POST",
        url: "/tranfac/GetInsurancecompany",
        data: '{}',
        async: false,
        dataType: "json",
        success: function (response) {
            insuranceCompany.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(response, function () {
                counter = counter + 1;
                insuranceCompany.append($("<option></option>").val(this['ID']).html(this['COMPANY_NAME']));
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

function bindPolicyNo(company,insuranceno,invoiceID) {
    debugger;
    var insuranceNo = $("#INSURANCE_NO");
    $("#dialog").dialog({
        autoOpen: true,
        modal: true,
        title: "Loading.."
    });
    $("#imgLoader").css("visibility", "visible");
    $.ajax({
        type: "POST",
        url: "/tranfac/GetPolicyno",
        data: { companyID: company },
        dataType: "json",
        success: function (response) {
            insuranceNo.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(response, function () {
                insuranceNo.append($("<option></option>").val(this['INSURANCE_NO']).html(this['INSURANCE_NO']));
            });
            if (invoiceID != '0') {
                $("#INSURANCE_NO").val(insuranceno);
            }
            $("#INSURANCE_NO").chosen({
                search_contains: true
            });
            $("#INSURANCE_NO").trigger("chosen:updated");
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

function bindFgSaleOrder(customerid, depotid, orderno, invoiceID) {
    debugger;
    var SaleOrder = $("#SaleOrderID");
    $("#dialog").dialog({
        autoOpen: true,
        modal: true,
        title: "Loading.."
    });
    $("#imgLoader").css("visibility", "visible");
  
    $.ajax({
        type: "POST",
        url: "/tranfac/fgSaleOrder",
        data: { CustomerID: customerid, DepotID: depotid, Type:'TS' },
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

function bindtransporter(sourceDepot, txnid) {
    var Transporter = $("#TransporterID");
    $.ajax({
        type: "POST",
        url: "/tranfac/GetTransporter",
        data: { SourceDepot: sourceDepot, TxnID: txnid },
        async: false,
        dataType: "json",
        success: function (response) {
            //alert(JSON.stringify(response));
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

function bindFgProduct() {
    var Product = $("#PRODUCTID");
    $("#dialog").dialog({
        autoOpen: true,
        modal: true,
        title: "Loading.."
    });
    $("#imgLoader").css("visibility", "visible");
    $.ajax({
        type: "POST",
        url: "/tranfac/GetInvoiceProduct",
        data: { SaleOrder: $('#SaleOrderID').val().trim() },
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
    $.ajax({
        type: "POST",
        url: "/tranfac/GetInvoiceTaxcount",
        data: { MenuID: $('#hdnmenuid').val(), Flag: '1', DepotID: $('#BRID').val(), ProductID: '0', CustomerID: $('#CUSTOMERID').val(), Date: $('#InvoiceDate').val() },
        dataType: "json",
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

function bindorderdetails() {
    var Packsize = $("#PSID");
    $.ajax({
        type: "POST",
        url: "/tranfac/GetTradingOrderQtyDetails",
        data: { CustomerID: $('#CUSTOMERID').val().trim(), SaleOrderID: $('#SaleOrderID').val().trim(), ProductID: $('#PRODUCTID').val().trim(), DepotID: $('#BRID').val().trim(), StorelocationID: $('#StorelocationID').val().trim()},
        dataType: "json",
        async:false,
        success: function (tradingorderlist) {
            //alert(JSON.stringify(tradingorderlist));
            Packsize.empty();
            if (tradingorderlist.length > 0) {
                $.each(tradingorderlist, function (key, item) {
                    debugger;
                    $("#Rate").val(tradingorderlist[0].RATE);
                    $("#OrderQty").val(tradingorderlist[0].ORDERQTY);
                    $("#DeliveredQty").val(tradingorderlist[0].DESPATCHQTY);
                    $("#RemainingQty").val(tradingorderlist[0].REMAININGQTY);
                    $("#StockQty").val(tradingorderlist[0].STOCKQTY);
                    $("#MRP").val(tradingorderlist[0].MRP);
                    $('#hdn_ASSESMENTPERCENTAGE').val(65);
                    Packsize.append($("<option></option>").val(this['UOMID']).html(this['UOMNAME']));
                });
            }
            else {
                $("#Rate").val('');
                $("#MRP").val('');
                $("#OrderQty").val('');
                $("#DeliveredQty").val('');
                $("#RemainingQty").val('');
                $("#StockQty").val('');
                $('#hdn_ASSESMENTPERCENTAGE').val('');
            }
            $("#PSID").chosen({
                search_contains: true
            });
            $("#PSID").trigger("chosen:updated");
        },
        failure: function (tradingorderlist) {
            alert(tradingorderlist.responseText);
        },
        error: function (tradingorderlist) {
            alert(tradingorderlist.responseText);
        }
    });
}

function addProduct() {

    if ($('#productDetailsGrid').length) {

        $('#productDetailsGrid').each(function () {
            //debugger;
            var exists = false;
            var arraydetails = [];
            var count = $('#productDetailsGrid tbody tr').length;
            $('#productDetailsGrid tbody tr').each(function () {
                var dispatchdetail = {};
                var productidgrd = $(this).find('td:eq(1)').html().trim();
                var batchgrd = $(this).find('td:eq(4)').html().trim();
                dispatchdetail.PRODUCTID = productidgrd;
                dispatchdetail.BATCHNO = batchgrd;
                arraydetails.push(dispatchdetail);
            });
            
            var jsondispatchobj = {};
            jsondispatchobj.fgDispatchDetails = arraydetails;

            for (i = 0; i < jsondispatchobj.fgDispatchDetails.length; i++) {
                if (jsondispatchobj.fgDispatchDetails[i].PRODUCTID.trim() == $('#PRODUCTID').val().trim() && jsondispatchobj.fgDispatchDetails[i].BATCHNO.trim() == 'NA') {
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

function addProductInDetailsGrid() {
    debugger;
    var convertedqty = '0';   
    var assesment = '';
    var qty = '';
    var netweight = '';
    var grossweight = '';
    var hsn = '';
    var igsttax = '';
    var cgsttax = '';
    var sgsttax = '';
    var tcstax = '';
    var Amount = 0;
    var productname = $('#PRODUCTID').find('option:selected').text().trim();
    var batchno = 'NA';
    var packsizename = $('#PSID').find('option:selected').text();
    var TransferQty = $('#InvoiceQty').val();
    var Rate = 0;
    var storelocationid = $('#StorelocationID').val().trim();
    var storelocationName = $('#StorelocationID').find('option:selected').text().trim();
    debugger;
    Rate = parseFloat($('#Rate').val().trim()).toFixed(4);
    
    var MRP = $('#MRP').val();
    var AssesmentPercentage = $("#hdn_ASSESMENTPERCENTAGE").val();
    var Assesmentvalue = 0;
    var IgstTaxPercentage = 0;
    var IgstTaxAmount = 0;
    var CgstTaxPercentage = 0;
    var CgstTaxAmount = 0;
    var SgstTaxPercentage = 0;
    var SgstTaxAmount = 0;
    var TcsTaxPercentage = 0;
    var TcsTaxAmount = 0;
    var NetAmount = 0;
    var srl = 0;
    srl = srl + 1; 

    /*Add Mode*/
    /*IGST*/
    if ($("#hdntaxcount").val().trim() == '1') {
        $.ajax({
            type: "POST",
            url: "/tranfac/GetInvoiceCalculateAmtInPcs",
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

                Amount = parseFloat(TransferQty * Rate);
                Assesmentvalue = parseFloat(assesment);
                IgstTaxPercentage = parseFloat(igsttax);
                if (isNaN(IgstTaxPercentage) == true) {
                    IgstTaxPercentage = 0;
                    IgstTaxAmount = 0;
                }
                else {
                    IgstTaxAmount = ((Amount * IgstTaxPercentage) / 100);
                }
                NetAmount = Amount + IgstTaxAmount;
                //Create Table 
                var tr;
                tr = $('#productDetailsGrid');
                var HeaderCount = $('#productDetailsGrid thead th').length;
                if (HeaderCount == 0) {
                    tr.append("<thead><tr><th>Sl.No.</th><th style='display: none'>Product ID</th><th>HSN Code</th><th>Product</th><th style='display: none'>Batch</th><th style='display: none'>PackSize ID</th><th style='display: none'>Pack Size</th><th>MRP</th><th>Rate</th><th style='display: none'>Case</th><th>Pcs/kg</th><th>Amount</th><th style='display: none'>Assesmet(%)</th><th style='display: none'>Assesment Amt.</th><th style='display: none'>Net Wght.</th><th>Gross Wght.</th><th style='display: none'>Mfg.Date</th><th style='display: none'>Exp.Date</th><th>IGST(%)</th><th>IGST</th><th>Net Amt.</th><th style='display: none'>Storelocation ID</th><th style='display: none'>Storelocation</th><th>Delete</th></tr></thead><tbody>");
                }

                tr = $('<tr/>');
                tr.append("<td style='text-align: center'>" + srl + "</td>");//0
                tr.append("<td style='display: none'>" + $('#PRODUCTID').val() + "</td>");//1
                tr.append("<td style='text-align: center'>" + hsn + "</td>");//2
                tr.append("<td style='width:250px'>" + productname + "</td>");//3
                tr.append("<td style='display: none'>" + batchno + "</td>");//4
                tr.append("<td style='display: none'>" + $('#PSID').val() + "</td>");//5
                tr.append("<td style='display: none'>" + packsizename + "</td>");//6
                tr.append("<td style='text-align: right'>" + MRP + "</td>");//7
                tr.append("<td style='text-align: right'>" + Rate + "</td>");//8
                if ($('#PSID').val().trim() == 'B9F29D12-DE94-40F1-A668-C79BF1BF4425') {
                    tr.append("<td style='text-align: right; display: none;'>" + parseFloat('0').toFixed(3) + "</td>");//9
                    tr.append("<td style='text-align: right'>" + parseFloat(TransferQty.trim()).toFixed(0) + "</td>");//10
                }
                else {
                    tr.append("<td style='text-align: right; display: none;'>" + parseFloat('0').toFixed(3) + "</td>");//9
                    tr.append("<td style='text-align: right'>" + parseFloat(TransferQty.trim()).toFixed(3) + "</td>");//10
                }
                tr.append("<td style='text-align: right'>" + Amount.toFixed(2) + "</td>");//11
                tr.append("<td style='display: none'>" + AssesmentPercentage + "</td>");//12
                tr.append("<td style='display: none'>" + Assesmentvalue.toFixed(2) + "</td>");//13
                tr.append("<td style='display: none'>" + netweight + "</td>");//14
                tr.append("<td>" + grossweight + "</td>");//15
                tr.append("<td style='text-align: center; display: none'>01/01/1900</td>");//16
                tr.append("<td style='text-align: center; display: none'>01/01/1900</td>");//17
                tr.append("<td style='text-align: right'>" + IgstTaxPercentage.toFixed(2) + "</td>");//18
                tr.append("<td style='text-align: right'>" + IgstTaxAmount.toFixed(2) + "</td>");//19
                tr.append("<td style='text-align: right'>" + NetAmount.toFixed(2) + "</td>");//20
                tr.append("<td style='display: none'>" + storelocationid + "</td>");//21
                tr.append("<td style='display: none'>" + storelocationName + "</td>");//22
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
    /*CGST & SGST*/
    else if ($("#hdntaxcount").val().trim() == '2') {
        debugger;
        $.ajax({
            type: "POST",
            url: "/tranfac/GetInvoiceCalculateAmtInPcsIntra",
            data: { Productid: $('#PRODUCTID').val().trim(), PacksizeID: $('#PSID').val().trim(), Qty: $('#InvoiceQty').val().trim(), Rate: parseFloat(Rate), Assesment: $('#hdn_ASSESMENTPERCENTAGE').val().trim(), CGST: $('#hdntaxnameCGST').val().trim(), SGST: $('#hdntaxnameSGST').val().trim(), date: $('#InvoiceDate').val().trim() },
            async: false,
            dataType: "json",
            success: function (response) {
                debugger;
                var listassesment = response.allcalculateDataset.varAssesment;
                var listqty = response.allcalculateDataset.varQty;
                var listNetWeight = response.allcalculateDataset.varNetwght;
                var listGrossWeight = response.allcalculateDataset.varGrosswght;
                var listHSN = response.allcalculateDataset.varHSN;
                var listCGSTTax = response.allcalculateDataset.varCgstTax;
                var listCGSTID = response.allcalculateDataset.varCgstID;
                var listSGSTTax = response.allcalculateDataset.varSgstTax;
                var listSGSTID = response.allcalculateDataset.varSgstID;


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
                if ($("#hdntaxcount").val().trim() == '2') {
                    $.each(listCGSTTax, function (index, record) {
                        cgsttax = this['CGSTTAX'].trim();
                    });
                }
                if ($("#hdntaxcount").val().trim() == '2') {
                    $.each(listCGSTID, function (index, record) {
                        $("#cgstid").val(this['CGSTID'].trim());
                    });
                }
                if ($("#hdntaxcount").val().trim() == '2') {
                    $.each(listSGSTTax, function (index, record) {
                        sgsttax = this['SGSTTAX'].trim();
                    });
                }
                if ($("#hdntaxcount").val().trim() == '2') {
                    $.each(listSGSTID, function (index, record) {
                        $("#sgstid").val(this['SGSTID'].trim());
                    });
                }

                Amount = parseFloat(TransferQty * Rate);
                Assesmentvalue = parseFloat(assesment);
                CgstTaxPercentage = parseFloat(cgsttax);
                if (isNaN(CgstTaxPercentage) == true) {
                    CgstTaxPercentage = 0;
                    CgstTaxAmount = 0;
                }
                else {
                    CgstTaxAmount = ((Amount * CgstTaxPercentage) / 100);
                }
                SgstTaxPercentage = parseFloat(sgsttax);
                if (isNaN(SgstTaxPercentage) == true) {
                    SgstTaxPercentage = 0;
                    SgstTaxAmount = 0;
                }
                else {
                    SgstTaxAmount = ((Amount * SgstTaxPercentage) / 100);
                }
                NetAmount = Amount + CgstTaxAmount + SgstTaxAmount;
                //Create Table 
                var tr;
                tr = $('#productDetailsGrid');
                var HeaderCount = $('#productDetailsGrid thead th').length;
                if (HeaderCount == 0) {
                    tr.append("<thead><tr><th>Sl.No.</th><th style='display: none'>Product ID</th><th>HSN Code</th><th>Product</th><th style='display: none'>Batch</th><th style='display: none'>PackSize ID</th><th style='display: none'>Pack Size</th><th>MRP</th><th>Rate</th><th style='display: none'>Case</th><th>Pcs/Kg</th><th>Amount</th><th style='display: none'>Assesmet(%)</th><th style='display: none'>Assesment Amt.</th><th style='display: none'>Net Wght.</th><th>Gross Wght.</th><th style='display: none'>Mfg.Date</th><th style='display: none'>Exp.Date</th><th>CGST(%)</th><th>CGST</th><th>SGST(%)</th><th>SGST</th><th>Net Amt.</th><th style='display: none'>Storelocation ID</th><th style='display: none'>Storelocation</th><th>Delete</th></tr></thead><tbody>");
                }
                tr = $('<tr/>');
                tr.append("<td style='text-align: center'>" + srl + "</td>");//0
                tr.append("<td style='display: none'>" + $('#PRODUCTID').val() + "</td>");//1
                tr.append("<td style='text-align: center'>" + hsn + "</td>");//2
                tr.append("<td style='width:250px'>" + productname + "</td>");//3
                tr.append("<td style='display: none'>" + batchno + "</td>");//4
                tr.append("<td style='display: none'>" + $('#PSID').val() + "</td>");//5
                tr.append("<td style='display: none'>" + packsizename + "</td>");//6
                tr.append("<td style='text-align: right'>" + MRP + "</td>");//7
                tr.append("<td style='text-align: right'>" + Rate + "</td>");//8
                if ($('#PSID').val().trim() == 'B9F29D12-DE94-40F1-A668-C79BF1BF4425') {
                    tr.append("<td style='text-align: right; display: none;'>" + parseFloat('0').toFixed(3) + "</td>");//9
                    tr.append("<td style='text-align: right'>" + parseFloat(TransferQty.trim()).toFixed(0) + "</td>");//10
                }
                else {
                    tr.append("<td style='text-align: right; display: none;'>" + parseFloat('0').toFixed(3) + "</td>");//9
                    tr.append("<td style='text-align: right'>" + parseFloat(TransferQty.trim()).toFixed(3) + "</td>");//10
                }
                tr.append("<td style='text-align: right'>" + Amount.toFixed(2) + "</td>");//11
                tr.append("<td style='display: none'>" + AssesmentPercentage + "</td>");//12
                tr.append("<td style='display: none'>" + Assesmentvalue.toFixed(2) + "</td>");//13
                tr.append("<td style='display: none'>" + netweight + "</td>");//14
                tr.append("<td>" + grossweight + "</td>");//15
                tr.append("<td style='text-align: center; display: none'>01/01/1900</td>");//16
                tr.append("<td style='text-align: center; display: none'>01/01/1900</td>");//17
                tr.append("<td style='text-align: right'>" + CgstTaxPercentage.toFixed(2) + "</td>");//18
                tr.append("<td style='text-align: right'>" + CgstTaxAmount.toFixed(2) + "</td>");//19
                tr.append("<td style='text-align: right'>" + SgstTaxPercentage.toFixed(2) + "</td>");//20
                tr.append("<td style='text-align: right'>" + SgstTaxAmount.toFixed(2) + "</td>");//21
                tr.append("<td style='text-align: right'>" + NetAmount.toFixed(2) + "</td>");//22
                tr.append("<td style='display: none'>" + storelocationid + "</td>");//23
                tr.append("<td style='display: none'>" + storelocationName + "</td>");//24
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
    /*CGST & SGST & TCS*/
    else if ($("#hdntaxcount").val().trim() == '3') {
        debugger;
        $.ajax({
            type: "POST",
            url: "/tranfac/GetTradingCalculateAmtInPcsIntra",
            data: { Productid: $('#PRODUCTID').val().trim(), PacksizeID: $('#PSID').val().trim(), Qty: $('#InvoiceQty').val().trim(), Rate: parseFloat(Rate), Assesment: $('#hdn_ASSESMENTPERCENTAGE').val().trim(), CustomerID: $("#CUSTOMERID").val().trim(),CGST: $('#hdntaxnameCGST').val().trim(), SGST: $('#hdntaxnameSGST').val().trim(), TCS: $('#hdntaxnameTCS').val().trim(),date: $('#InvoiceDate').val().trim() },
            async: false,
            dataType: "json",
            success: function (response) {
                debugger;
                var listassesment = response.allcalculateDataset.varAssesment;
                var listqty = response.allcalculateDataset.varQty;
                var listNetWeight = response.allcalculateDataset.varNetwght;
                var listGrossWeight = response.allcalculateDataset.varGrosswght;
                var listHSN = response.allcalculateDataset.varHSN;
                var listCGSTTax = response.allcalculateDataset.varCgstTax;
                var listCGSTID = response.allcalculateDataset.varCgstID;
                var listSGSTTax = response.allcalculateDataset.varSgstTax;
                var listSGSTID = response.allcalculateDataset.varSgstID;
                var listTCSTax = response.allcalculateDataset.varTcsTax;
                var listTCSID = response.allcalculateDataset.varTcsID;


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
                if ($("#hdntaxcount").val().trim() == '3') {
                    $.each(listCGSTTax, function (index, record) {
                        cgsttax = this['CGSTTAX'].trim();
                    });
                }
                if ($("#hdntaxcount").val().trim() == '3') {
                    $.each(listCGSTID, function (index, record) {
                        $("#cgstid").val(this['CGSTID'].trim());
                    });
                }
                if ($("#hdntaxcount").val().trim() == '3') {
                    $.each(listSGSTTax, function (index, record) {
                        sgsttax = this['SGSTTAX'].trim();
                    });
                }
                if ($("#hdntaxcount").val().trim() == '3') {
                    $.each(listSGSTID, function (index, record) {
                        $("#sgstid").val(this['SGSTID'].trim());
                    });
                }
                if ($("#hdntaxcount").val().trim() == '3') {
                    $.each(listTCSTax, function (index, record) {
                        tcstax = this['TCSTAX'].trim();
                    });
                }
                if ($("#hdntaxcount").val().trim() == '3') {
                    $.each(listTCSID, function (index, record) {
                        $("#tcsid").val(this['TCSID'].trim());
                    });
                }

                Amount = parseFloat(TransferQty * Rate);
                Assesmentvalue = parseFloat(assesment);
                CgstTaxPercentage = parseFloat(cgsttax);
                if (isNaN(CgstTaxPercentage) == true) {
                    CgstTaxPercentage = 0;
                    CgstTaxAmount = 0;
                }
                else {
                    CgstTaxAmount = ((Amount * CgstTaxPercentage) / 100);
                }
                SgstTaxPercentage = parseFloat(sgsttax);
                if (isNaN(SgstTaxPercentage) == true) {
                    SgstTaxPercentage = 0;
                    SgstTaxAmount = 0;
                }
                else {
                    SgstTaxAmount = ((Amount * SgstTaxPercentage) / 100);
                }
                TcsTaxPercentage = parseFloat(tcstax);
                if (isNaN(TcsTaxPercentage) == true) {
                    TcsTaxPercentage = 0;
                    TcsTaxAmount = 0;
                }
                else {
                    TcsTaxAmount = ((Amount * TcsTaxPercentage) / 100);
                }
                NetAmount = Amount + CgstTaxAmount + SgstTaxAmount + TcsTaxAmount;
                //Create Table 
                var tr;
                tr = $('#productDetailsGrid');
                var HeaderCount = $('#productDetailsGrid thead th').length;
                if (HeaderCount == 0) {
                    tr.append("<thead><tr><th>Sl.No.</th><th style='display: none'>Product ID</th><th>HSN Code</th><th>Product</th><th style='display: none'>Batch</th><th style='display: none'>PackSize ID</th><th style='display: none'>Pack Size</th><th>MRP</th><th>Rate</th><th style='display: none'>Case</th><th>Pcs/kg</th><th>Amount</th><th style='display: none'>Assesmet(%)</th><th style='display: none'>Assesment Amt.</th><th style='display: none'>Net Wght.</th><th>Gross Wght.</th><th style='display: none'>Mfg.Date</th><th style='display: none'>Exp.Date</th><th>CGST(%)</th><th>CGST</th><th>SGST(%)</th><th>SGST</th><th>TCS(%)</th><th>TCS</th><th>Net Amt.</th><th style='display: none'>Storelocation ID</th><th style='display: none'>Storelocation</th><th>Delete</th></tr></thead><tbody>");
                }
                tr = $('<tr/>');
                tr.append("<td style='text-align: center'>" + srl + "</td>");//0
                tr.append("<td style='display: none'>" + $('#PRODUCTID').val() + "</td>");//1
                tr.append("<td style='text-align: center'>" + hsn + "</td>");//2
                tr.append("<td style='width:250px'>" + productname + "</td>");//3
                tr.append("<td style='text-align: center; display: none;'>" + batchno + "</td>");//4
                tr.append("<td style='display: none'>" + $('#PSID').val() + "</td>");//5
                tr.append("<td style='display: none'>" + packsizename + "</td>");//6
                tr.append("<td style='text-align: right'>" + MRP + "</td>");//7
                tr.append("<td style='text-align: right'>" + Rate + "</td>");//8
                if ($('#PSID').val().trim() == 'B9F29D12-DE94-40F1-A668-C79BF1BF4425') {
                    tr.append("<td style='text-align: right; display: none;'>" + parseFloat('0').toFixed(3) + "</td>");//9
                    tr.append("<td style='text-align: right'>" + parseFloat(TransferQty.trim()).toFixed(0) + "</td>");//10
                }
                else {
                    tr.append("<td style='text-align: right; display: none;'>" + parseFloat('0').toFixed(3) + "</td>");//9
                    tr.append("<td style='text-align: right'>" + parseFloat(TransferQty.trim()).toFixed(3) + "</td>");//10
                }
                tr.append("<td style='text-align: right'>" + Amount.toFixed(2) + "</td>");//11
                tr.append("<td style='display: none'>" + AssesmentPercentage + "</td>");//12
                tr.append("<td style='display: none'>" + Assesmentvalue.toFixed(2) + "</td>");//13
                tr.append("<td style='display: none'>" + netweight + "</td>");//14
                tr.append("<td>" + grossweight + "</td>");//15
                tr.append("<td style='text-align: center; display: none'>01/01/1900</td>");//16
                tr.append("<td style='text-align: center; display: none'>01/01/1900</td>");//17
                tr.append("<td style='text-align: right'>" + CgstTaxPercentage.toFixed(2) + "</td>");//18
                tr.append("<td style='text-align: right'>" + CgstTaxAmount.toFixed(2) + "</td>");//19
                tr.append("<td style='text-align: right'>" + SgstTaxPercentage.toFixed(2) + "</td>");//20
                tr.append("<td style='text-align: right'>" + SgstTaxAmount.toFixed(2) + "</td>");//21
                tr.append("<td style='text-align: right'>" + TcsTaxPercentage.toFixed(2) + "</td>");//22
                tr.append("<td style='text-align: right'>" + TcsTaxAmount.toFixed(2) + "</td>");//23
                tr.append("<td style='text-align: right'>" + NetAmount.toFixed(2) + "</td>");//24
                tr.append("<td style='display: none'>" + storelocationid + "</td>");//25
                tr.append("<td style='display: none'>" + storelocationName + "</td>");//26
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
        addRowinTaxTable($('#igstid').val().trim(),IgstTaxPercentage, IgstTaxAmount, MRP, '1');
    }
    else if ($("#hdntaxcount").val() == '2') {
        addRowinTaxTable($('#cgstid').val().trim(),CgstTaxPercentage, CgstTaxAmount, MRP, '1');
        addRowinTaxTable($('#sgstid').val().trim(),SgstTaxPercentage, SgstTaxAmount, MRP, '1');
    }
    else if ($("#hdntaxcount").val() == '3') {
        addRowinTaxTable($('#cgstid').val().trim(), CgstTaxPercentage, CgstTaxAmount, MRP, '1');
        addRowinTaxTable($('#sgstid').val().trim(), SgstTaxPercentage, SgstTaxAmount, MRP, '1');
        addRowinTaxTable($('#tcsid').val().trim(),  TcsTaxPercentage,  TcsTaxAmount,  MRP, '1');
    }
    else {
        addRowinTaxTable($('#igstid').val().trim(),IgstTaxPercentage, IgstTaxAmount, MRP, '0');
    }
    /*********************/
    clearafterAdd();
    $("#PRODUCTID").focus();
    $('#PRODUCTID').trigger('chosen:activate');
}

function addRowinTaxTable(taxID,TaxPercentage,TaxAmount, MRP,TaxFlag) {
    $.ajax({
        type: "POST",
        url: "/tranfac/FillTradingTaxDatatable",
        data: { Productid: $('#PRODUCTID').val(), BatchNo: 'NA', TaxID: taxID, Percentage: parseFloat(TaxPercentage), TaxValue: parseFloat(TaxAmount), MRP: parseFloat(MRP), Flag: TaxFlag },
        async: false,
        dataType: "json",
        success: function (response) {

        }
    });
}

function addRowinTaxTableEdit(ProductID,batch,taxid,TaxPercentage, TaxAmount, MRP, TaxFlag) {
    $.ajax({
        type: "POST",
        url: "/tranfac/FillTradingTaxDatatable",
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
        url: "/tranfac/DeleteTaxDatatableTrading",
        data: { Productid: grdproductid.trim(), BatchNo: grdbatchno.trim() },
        dataType: "json",
        success: function (response) {

        }
    });
}

function RowCount() {
    var table = document.getElementById("productDetailsGrid");
    var rowCount = document.getElementById("productDetailsGrid").rows.length - 1;
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
    var totalbasicamt = 0;
    var totaltaxamt = 0;
    var totalcgstamt = 0;
    var totalsgstamt = 0;
    var totalbasicplustaxamt = 0;
    var decimalvalue = 0;
    var totalcaseqty = 0;
    var totalpcsqty = 0;
    var parts = 0;
    var finalamt = 0;
    var rowCount = document.getElementById("productDetailsGrid").rows.length - 1;
    if (rowCount > 0) {
        if ($("#hdntaxcount").val() == '1') {
            $('#productDetailsGrid tbody tr').each(function () {
                totalbasicamt += parseFloat($(this).find('td:eq(11)').html().trim());
                totaltaxamt += parseFloat($(this).find('td:eq(19)').html().trim());
                totalbasicplustaxamt += parseFloat($(this).find('td:eq(20)').html().trim());
                totalcaseqty += parseFloat($(this).find('td:eq(9)').html().trim());
                if ($("#PSID").val() == 'B9F29D12-DE94-40F1-A668-C79BF1BF4425') {
                    totalpcsqty += parseInt($(this).find('td:eq(10)').html().trim());
                }
                else {
                    totalpcsqty += parseFloat($(this).find('td:eq(10)').html().trim());
                }
            });
        }
        else if ($("#hdntaxcount").val() == '2') {
            $('#productDetailsGrid tbody tr').each(function () {
                totalbasicamt += parseFloat($(this).find('td:eq(11)').html().trim());
                totaltaxamt += parseFloat($(this).find('td:eq(19)').html().trim()) + parseFloat($(this).find('td:eq(21)').html().trim());
                totalbasicplustaxamt += parseFloat($(this).find('td:eq(22)').html().trim());
                totalcaseqty += parseFloat($(this).find('td:eq(9)').html().trim());
                if ($("#PSID").val() == 'B9F29D12-DE94-40F1-A668-C79BF1BF4425') {
                    totalpcsqty += parseInt($(this).find('td:eq(10)').html().trim());
                }
                else {
                    totalpcsqty += parseFloat($(this).find('td:eq(10)').html().trim());
                }
            });
        }
        else if ($("#hdntaxcount").val() == '3') {
            $('#productDetailsGrid tbody tr').each(function () {
                totalbasicamt += parseFloat($(this).find('td:eq(11)').html().trim());
                totaltaxamt += parseFloat($(this).find('td:eq(19)').html().trim()) + parseFloat($(this).find('td:eq(21)').html().trim()) + parseFloat($(this).find('td:eq(23)').html().trim());
                totalbasicplustaxamt += parseFloat($(this).find('td:eq(24)').html().trim());
                totalcaseqty += parseFloat($(this).find('td:eq(9)').html().trim());
                if ($("#PSID").val() == 'B9F29D12-DE94-40F1-A668-C79BF1BF4425') {
                    totalpcsqty += parseInt($(this).find('td:eq(10)').html().trim());
                }
                else {
                    totalpcsqty += parseFloat($(this).find('td:eq(10)').html().trim());
                }
            });
        }
        else if ($("#hdntaxcount").val() == '0') {
            $('#productDetailsGrid tbody tr').each(function () {
                totalbasicamt += parseFloat($(this).find('td:eq(11)').html().trim());
                totaltaxamt += parseFloat(0);
                totalbasicplustaxamt += parseFloat($(this).find('td:eq(20)').html().trim());
                totalcaseqty += parseFloat($(this).find('td:eq(9)').html().trim());
                if ($("#PSID").val() == 'B9F29D12-DE94-40F1-A668-C79BF1BF4425') {
                    totalpcsqty += parseInt($(this).find('td:eq(10)').html().trim());
                }
                else {
                    totalpcsqty += parseFloat($(this).find('td:eq(10)').html().trim());
                }
            });
        }
        $('#BasicAmt').val(totalbasicamt.toFixed(2));
        $('#TaxAmt').val(totaltaxamt.toFixed(2));
        $('#GrossAmt1').val(totalbasicplustaxamt.toFixed(2));
        $('#GrossAmt').val(totalbasicplustaxamt.toFixed(2));
        $('#TotalCase').val(totalcaseqty.toFixed(3));
        $('#TotalPcs').val(totalpcsqty);
        parts = totalbasicplustaxamt - Math.floor(totalbasicplustaxamt);
        decimalvalue = parts.toFixed(2);
        //if (decimalvalue >= .50) {
        //    decimalvalue = 1 - decimalvalue;
        //    decimalvalue = decimalvalue.toFixed(2);

        //}
        //else {
        //    decimalvalue = -decimalvalue;
        //}
        //finalamt = Math.round(totalbasicplustaxamt);
        /*Round Up Logic start from 01-09-2020*/
        if (decimalvalue > 0) {
            decimalvalue = 1 - decimalvalue;
            decimalvalue = decimalvalue.toFixed(2);
        }
        finalamt = Math.ceil(totalbasicplustaxamt);
        /*Round Up Logic end*/
        $("#NetAmt").val(finalamt.toFixed(2));
        $("#RoundOff").val(decimalvalue);
    }
    else {
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
    $("#TransferNo").attr("disabled", "disabled");
    $("#GatepassDate").attr("disabled", "disabled");
    $("#InvoiceDate").attr("disabled", "disabled");
    $("#LrGrDate").attr("disabled", "disabled");
    $("#DeliveryDate").attr("disabled", "disabled");
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
    $("#BRID").attr("disabled", "disabled");
    $("#BRID").chosen({
        search_contains: true
    });
    $("#BRID").trigger("chosen:updated");

    $("#CUSTOMERID").val('0');
    $("#CUSTOMERID").prop("disabled", false);
    $("#CUSTOMERID").chosen({
        search_contains: true
    });
    $("#CUSTOMERID").trigger("chosen:updated");

    $("#ID").val('0');
    $("#ID").chosen({
        search_contains: true
    });
    $("#ID").trigger("chosen:updated");

    $('#INSURANCE_NO').empty();
    $("#INSURANCE_NO").chosen({
        search_contains: true
    });
    $("#INSURANCE_NO").trigger("chosen:updated");

    $('#SaleOrderID').empty();
    $("#SaleOrderID").chosen({
        search_contains: true
    });
    $("#SaleOrderID").trigger("chosen:updated");

    $("#WAYBILLID").val('0');
    $("#WAYBILLID").chosen({
        search_contains: true
    });
    $("#WAYBILLID").trigger("chosen:updated");

    $("#TransportMode").chosen({
        search_contains: true
    });
    $("#TransportMode").trigger("chosen:updated");

    $("#TransporterID").val('0');
    $("#TransporterID").chosen({
        search_contains: true
    });
    $("#TransporterID").trigger("chosen:updated");

    $("#StorelocationID").val('0');
    $("#StorelocationID").chosen({
        search_contains: true
    });
    $("#StorelocationID").trigger("chosen:updated");

    $("#VehichleNo").val('');
    $("#LrGrNo").val('');
    $("#GatepassNo").val('');
    $("#ShippingAddress").val('');
    $("#DeliveryDate").val('');

    //if (Name != 'Itemledger') {
    //    backdatestatus = bindbackdateflag('4197');
    //    if (parseInt(backdatestatus) > 0) {
    //        $("#InvoiceDate").datepicker({
    //            changeMonth: true,
    //            changeYear: true,
    //            showButtonPanel: true,
    //            selectCurrent: true,
    //            todayBtn: "linked",
    //            showAnim: "slideDown",
    //            yearRange: "-3:+0",
    //            maxDate: 'today',
    //            dateFormat: "dd/mm/yy",
    //            showOn: 'button',
    //            buttonText: 'Show Date',
    //            buttonImageOnly: true,
    //            buttonImage: 'http://jqueryui.com/resources/demos/datepicker/images/calendar.gif'
    //        });
    //        $(".ui-datepicker-trigger").mouseover(function () {
    //            $(this).css('cursor', 'pointer');
    //        });
    //    }
    //    else {
    //        $("#InvoiceDate").datepicker({
    //            changeMonth: true,
    //            changeYear: true,
    //            showButtonPanel: true,
    //            selectCurrent: true,
    //            todayBtn: "linked",
    //            showAnim: "slideDown",
    //            yearRange: "-3:+0",
    //            maxDate: 'today',
    //            dateFormat: "dd/mm/yy",
    //            showOn: 'button',
    //            buttonText: 'Show Date',
    //            buttonImageOnly: true,
    //            maxDate: 0,
    //            minDate: 0,
    //            buttonImage: 'http://jqueryui.com/resources/demos/datepicker/images/calendar.gif'
    //        });
    //        $(".ui-datepicker-trigger").mouseover(function () {
    //            $(this).css('cursor', 'pointer');
    //        });
    //    }
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
    $('#BasicAmt').val('');
    $('#TaxAmt').val('');
    $('#GrossAmt1').val('');
    $('#GrossAmt').val('');
    $('#RoundOff').val('');
    $('#NetAmt').val('');
    $('#TotalCase').val('');
    $('#TotalPcs').val('');
    $('#Remarks').val('');//
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
}

function SaveInvoice() {
    debugger;
    var Taxreturnflag = true;

    /* Tax Checking Start*/
    if ($("#hdntaxcount").val() == '1') {
        $('#productDetailsGrid tbody tr').each(function () {
            var lineItemigstTax = 0;
            var TaxProduct = $(this).find('td:eq(3)').html().trim();
            
            if (isNaN($(this).find('td:eq(18)').html().trim()) == true) {
                toastr.error('Tax amount should not be 0 or NaN for <b>' + TaxProduct + '</b>');
                Taxreturnflag = false;
                return false;
            }
        });
    }
    else if ($("#hdntaxcount").val() == '2') {
        $('#productDetailsGrid tbody tr').each(function () {
            var lineItemcgstTax = 0;
            var lineItemsgstTax = 0;
            var TaxProduct = $(this).find('td:eq(3)').html().trim();
            
            if (isNaN($(this).find('td:eq(18)').html().trim()) == true || isNaN($(this).find('td:eq(20)').html().trim()) == true) {
                toastr.error('Tax amount should not be 0 or NaN for <b>' + TaxProduct + '</b>');
                Taxreturnflag = false;
                return false;
            }
        });
    }
    
    /* Tax Checking End*/

    if (Taxreturnflag == true) {

        var i = 0;
        varinsuranceCompanyText = $("#ID option:selected").text();
        $("#COMPANY_NAME").val(varinsuranceCompanyText);

        varSourceDepotText = $("#BRID option:selected").text();
        $("#BRNAME").val(varSourceDepotText);

        varCustomerText = $("#CUSTOMERID option:selected").text();
        $("#CUSTOMERNAME").val(varCustomerText);

        invoicelist = new Array();
        var invoicesave = {};
        debugger;
        invoicesave.FGInvoiceID = $('#hdndispatchID').val().trim();
        if ($('#hdndispatchID').val() == '0') {
            invoicesave.FLAG = 'A';
        }
        else {
            invoicesave.FLAG = 'U';
        }
        if ($('#hdntaxcount').val() == '1') {
            invoicesave.InvoiceType = '1';
        }
        else if ($('#hdntaxcount').val() == '2') {
            invoicesave.InvoiceType = '2';
        }
        else if ($('#hdntaxcount').val() == '3') {
            invoicesave.InvoiceType = '3';
        }
        else {
            invoicesave.InvoiceType = '0';
        }
        invoicesave.InvoiceDate = $("#InvoiceDate").val().trim();
        invoicesave.CUSTOMERID = $("#CUSTOMERID").val().trim();
        invoicesave.CUSTOMERNAME = $("#CUSTOMERNAME").val().trim();
        invoicesave.WAYBILLNO = '0';
        invoicesave.TransporterID = $("#TransporterID").val().trim();
        invoicesave.VehichleNo = $("#VehichleNo").val().trim();
        invoicesave.BRID = $("#BRID").val().trim();
        invoicesave.BRNAME = $("#BRNAME").val().trim();
        invoicesave.LrGrNo = $("#LrGrNo").val().trim();
        invoicesave.LrGrDate = $("#LrGrDate").val().trim();
        invoicesave.GatepassNo = $("#GatepassNo").val().trim();
        invoicesave.GatepassDate = $("#GatepassDate").val().trim();
        invoicesave.PaymentMode = $("#PaymentMode").val().trim();
        invoicesave.Tranportmode = $("#Tranportmode").val().trim();
        invoicesave.Remarks = $("#Remarks").val().trim();
        invoicesave.NetAmt = $("#NetAmt").val().trim();
        invoicesave.RoundOff = $("#RoundOff").val().trim();
        invoicesave.SaleOrderID = $("#SaleOrderID").val().trim();
        invoicesave.TotalPcs = $("#TotalPcs").val().trim();
        invoicesave.TotalCase = $("#TotalCase").val().trim();
        invoicesave.ID = $("#ID").val().trim();
        invoicesave.COMPANY_NAME = $("#COMPANY_NAME").val().trim();
        if ($("#ID").val().trim() != '0') {
            invoicesave.INSURANCE_NO = $("#INSURANCE_NO").val().trim();
        }
        else {
            invoicesave.INSURANCE_NO = '0';
        }
        invoicesave.ShippingAddress = $("#ShippingAddress").val();

        $('#productDetailsGrid tbody tr').each(function () {

            debugger
            var invoicedetails = {};

            var productid = $(this).find('td:eq(1)').html().trim();
            var productname = $(this).find('td:eq(3)').html().trim();
            var batchno = $(this).find('td:eq(4)').html().trim();
            var packingsizeid = $(this).find('td:eq(5)').html().trim();
            var packingsizename = $(this).find('td:eq(6)').html().trim();
            var mrp = $(this).find('td:eq(7)').html().trim();
            var rate = $(this).find('td:eq(8)').html().trim();
            var trnsferqtycase = $(this).find('td:eq(9)').html().trim();
            var trnsferqtypcs = $(this).find('td:eq(10)').html().trim();
            var amount = $(this).find('td:eq(11)').html().trim();
            var assesmentpercentage = $(this).find('td:eq(12)').html().trim();
            var assesmentvalue = $(this).find('td:eq(13)').html().trim();
            var weight = $(this).find('td:eq(14)').html().trim();
            var grossweight = $(this).find('td:eq(15)').html().trim();
            var mfdate = $(this).find('td:eq(16)').html().trim();
            var exprdate = $(this).find('td:eq(17)').html().trim();
            if ($("#hdntaxcount").val().trim() == '1') {
                var storelocationid = $(this).find('td:eq(21)').html().trim();
                var storelocationname = $(this).find('td:eq(22)').html().trim();
            }
            else if ($("#hdntaxcount").val().trim() == '2') {
                var storelocationid = $(this).find('td:eq(23)').html().trim();
                var storelocationname = $(this).find('td:eq(24)').html().trim();
            }
            else if ($("#hdntaxcount").val().trim() == '3') {
                var storelocationid = $(this).find('td:eq(25)').html().trim();
                var storelocationname = $(this).find('td:eq(26)').html().trim();
            }

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
            invoicelist[i++] = invoicedetails;
        });
        invoicesave.InvoiceDetailsFG = invoicelist,

            //alert(JSON.stringify(invoicesave));

            $.ajax({
                url: "/tranfac/tradingInvoicesavedata",
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
                        bindtradinginvoicegrid();
                        ReleaseSession();
                        toastr.success('<b><font color=black>' + messagetext + '</font></b>');
                    }
                    else {
                        $('#dvAdd').css("display", "");
                        $('#dvDisplay').css("display", "none");
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
}

function ReleaseSession() {
   
    $.ajax({
        type: "POST",
        url: "/tranfac/RemoveSessionTradingInvoice",
        data: '{}',
        dataType: "json",
        success: function (response) {
           
            
        }
    });
}

function bindtradinginvoicegrid() {
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
        url: "/tranfac/BindTradingInvoiceGrid",
        data: { FromDate: $('#txtfrmdate').val().trim(), ToDate: $('#txttodate').val().trim(), BSID: BusinessSegment, CheckerFlag: CHECKER.trim(), depotID: $('#BRID').val().trim(), type: 'FALSE' },
        dataType: "json",
        success: function (response) {
            var tr;
            tr = $('#invoiceGridFG');
            tr.append("<thead><tr><th>Sl. No.</th><th style='display: none'>InvoiceId</th><th>Invoice No</th><th>Invoice Date</th><th style='display: none'>DEPOTID</th><th style='display: none'>DEPOTNAME</th><th style='display: none'>CUSTOMERID</th><th>Customer</th><th style='display: none'>TRANSPORTERID</th><th>Transporter</th><th style='display: none'>ISVERIFIED</th><th>Financial Status</th><th style='display: none'>NEXTLEVELID</th><th>Entry User</th><th>Net Amount</th><th>Print</th><th>Edit</th><th>View</th><th>Cancel</th>");
            
            $('#invoiceGridFG').DataTable().destroy();
            $("#invoiceGridFG tbody tr").remove();
            for (var i = 0; i < response.length; i++) {
                tr = $('<tr/>');
                tr.append("<td style='text-align: center'>" + srl + "</td>");//0
                tr.append("<td style='display: none'>" + response[i].SALEINVOICEID + "</td>");
                tr.append("<td>" + response[i].SALEINVOICENO + "</td>");
                tr.append("<td>" + response[i].SALEINVOICEDATE + "</td>");
                tr.append("<td style='display: none'>" + response[i].DEPOTID + "</td>");
                tr.append("<td style='display: none'>" + response[i].DEPOTNAME + "</td>");
                tr.append("<td style='display: none'>" + response[i].DISTRIBUTORID + "</td>");
                tr.append("<td>" + response[i].DISTRIBUTORNAME + "</td>");
                tr.append("<td style='display: none'>" + response[i].TRANSPORTERID + "</td>");
                tr.append("<td>" + response[i].TRANSPORTERNAME + "</td>");
                tr.append("<td style='display: none'>" + response[i].ISVERIFIED + "</td>");
                if (response[i].ISVERIFIEDDESC.trim() == 'APPROVED') {
                    tr.append("<td><font color='0x00B300'>" + response[i].ISVERIFIEDDESC + "</font></td>");
                }
                else if (response[i].ISVERIFIEDDESC.trim() == 'PENDING'){
                    tr.append("<td><font color='0x5184FF'>" + response[i].ISVERIFIEDDESC + "</font></td>");  
                }
                else {
                    tr.append("<td><font color='#ff2500'>" + response[i].ISVERIFIEDDESC + "</font></td>");
                }
                tr.append("<td style='display: none'>" + response[i].NEXTLEVELID + "</td>");
                tr.append("<td>" + response[i].USERNAME + "</td>");
                tr.append("<td>" + response[i].NETAMOUNT + "</td>");
                tr.append("<td style='text-align: center'><input type='image' class='gvPrint'  id='btndispatchprint'  <img src='../Images/ico_Print.png' width='20' height ='20' title='Print'/></input></td>");
                tr.append("<td style='text-align: center'><input type='image' class='gvEdit'   id='btndispatchedit'   <img src='../Images/Pencil-icon.png' title='Edit'/></input></td>");
                tr.append("<td style='text-align: center'><input type='image' class='gvView'   id='btndispatchview'   <img src='../Images/View.png' width='20' height ='20' title='View'/></input></td>");
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
                        title: 'Trading Invoice List'
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
    var tr;
    var HeaderCount = 0;
    var srl = 0;
    srl = srl + 1; 
    var Igstid = '';
    var Cgstid = '';
    var Sgstid = '';
    var Tcsid = '';
    var IgstPercentage = 0;
    var IgstAmount = 0;
    var CgstPercentage = 0;
    var CgstAmount = 0;
    var SgstPercentage = 0;
    var SgstAmount = 0;
    var TcsPercentage = 0;
    var TcsAmount = 0;
    var NetAmount = 0;
    var PolicyNo = '';
    var OrderID = '';
    var Product = $("#PRODUCTID");
    $("#productDetailsGrid").empty();

    $("#dialog").dialog({
        autoOpen: true,
        modal: true,
        title: "Loading.."
    });
    $("#imgLoader").css("visibility", "visible");
    $.ajax({
        type: "POST",
        url: "/tranfac/InvoiceTradingEdit",
        data: { InvoiceID: invoiceid},
        dataType: "json",
        async: false,
        success: function (response) {
            //alert(JSON.stringify(response))
            var listHeader = response.alleditDataset.varHeader;
            var listDetails = response.alleditDataset.varDetails;
            var listTaxcount = response.alleditDataset.varTaxCount;
            var listFooter = response.alleditDataset.varFooter;
            var listTax = response.alleditDataset.varTax;
            var listOrderDetails = response.alleditDataset.varOrderDetails;
            var listOrderHeader = response.alleditDataset.varOrderHeader;
            
            
            /*Tax Count Info*/
            if (listTaxcount.length > 0) {
                if (listTaxcount.length == 1) {
                    $.each(listTaxcount, function (key, item) {
                        debugger;
                        $("#hdntaxcount").val('1');
                        Igstid = listTaxcount[0].TAXID;
                        $("#hdntaxnameIGST").val(listTaxcount[0].NAME);
                        $("#hdnrelatedto").val(listTaxcount[0].RELATEDTO);
                    });
                }
                else if (listTaxcount.length == 2) {
                    $.each(listTaxcount, function (index, record) {
                        debugger;
                        $("#hdntaxcount").val('2');
                        Cgstid = listTaxcount[0].TAXID;
                        Sgstid = listTaxcount[1].TAXID;
                        $("#hdntaxnameCGST").val(listTaxcount[0].NAME);
                        $("#hdntaxnameSGST").val(listTaxcount[1].NAME);
                        $("#hdnrelatedto").val(listTaxcount[0].RELATEDTO);
                    });
                }
                else if (listTaxcount.length == 3) {
                    $.each(listTaxcount, function (index, record) {
                        debugger;
                        $("#hdntaxcount").val('3');
                        Cgstid = listTaxcount[0].TAXID;
                        Sgstid = listTaxcount[1].TAXID;
                        Tcsid =  listTaxcount[2].TAXID;
                        $("#hdntaxnameCGST").val(listTaxcount[0].NAME);
                        $("#hdntaxnameSGST").val(listTaxcount[1].NAME);
                        $("#hdntaxnameTCS").val(listTaxcount[2].NAME);
                        $("#hdnrelatedto").val(listTaxcount[0].RELATEDTO);
                    });
                }
            }
            else {
                $("#hdntaxcount").val('0');
                $("#hdntaxnameIGST").val('NA');
                $("#hdntaxnameCGST").val('NA');
                $("#hdntaxnameSGST").val('NA');
                $("#hdntaxnameTCS").val('NA');
                Igstid = 'NA';
                Cgstid = 'NA';
                Sgstid = 'NA';
                Tcsid = 'NA';
            }

            /*Invoice Header Info*/
            $.each(listHeader, function (index, record) {
                //debugger;
                $("#FGInvoiceNo").val(this['SALEINVOICENO'].toString().trim());
                $("#InvoiceDate").val(this['SALEINVOICEDATE'].toString().trim());
                $("#CUSTOMERID").val(this['DISTRIBUTORID'].toString().trim());
                $("#ID").val(this['INSURANCECOMPID'].toString().trim());
                $("#TransporterID").val(this['TRANSPORTERID'].toString().trim());
                $("#WAYBILLID").val(this['WAYBILLNO'].toString().trim());
                $("#TransportMode").val(this['MODEOFTRANSPORT'].toString().trim());
                $("#VehichleNo").val(this['VEHICHLENO'].toString().trim());
                $("#LrGrNo").val(this['LRGRNO'].toString().trim());
                $("#GatepassNo").val(this['GETPASSNO'].toString().trim());
                $("#ShippingAddress").val(this['DELIVERYADDRESS'].toString().trim());
                $("#LrGrDate").val(this['LRGRDATE'].toString().trim());
                $("#GatepassDate").val(this['GETPASSDATE'].toString().trim());
                $("#TotalPcs").val(parseInt(this['TOTALPCS'].toString().trim()));
                $("#Remarks").val(this['REMARKS'].toString().trim());
                PolicyNo = this['INSURANCENUMBER'].toString().trim();
            });

            /*Order Header*/
            $.each(listOrderHeader, function (index, record) {
                //debugger;
                OrderID = this['SALEORDERID'].toString().trim();
            });

            /*Order Details*/
            Product.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(listOrderDetails, function (index, record) {
                //debugger;
                Product.append($("<option></option>").val(this['PRODUCTID']).html(this['PRODUCTNAME']));
            });
            $("#PRODUCTID").trigger("liszt:updated");

            
            /*Invoice Details Info*/
            if (listDetails.length > 0) {

                $("#productDetailsGrid").empty();
                if ($("#hdntaxcount").val().trim() == '1') {
                    tr = $('#productDetailsGrid');
                    HeaderCount = $('#productDetailsGrid thead th').length;
                    if (HeaderCount == 0) {
                        tr.append("<thead><tr><th>Sl.No.</th><th style='display: none'>Product ID</th><th>HSN Code</th><th>Product</th><th style='display: none'>Batch</th><th style='display: none'>PackSize ID</th><th style='display: none'>Pack Size</th><th>MRP</th><th>Rate</th><th style='display: none'>Case</th><th>Pcs/Kg</th><th>Amount</th><th style='display: none'>Assesmet(%)</th><th style='display: none'>Assesment Amt.</th><th style='display: none'>Net Wght.</th><th>Gross Wght.</th><th style='display: none'>Mfg.Date</th><th style='display: none'>Exp.Date</th><th>IGST(%)</th><th>IGST</th><th>Net Amt.</th><th style='display: none'>Storelocation ID</th><th style='display: none'>Storelocation</th><th>Delete</th></tr></thead><tbody>");
                    }
                    $.each(listDetails, function (index, record) {
                        //debugger;
                        tr = $('<tr/>');
                        tr.append("<td style='text-align: center'>" + srl + "</td>");//0
                        tr.append("<td style='display: none'>" + this['PRODUCTID'].toString().trim() + "</td>");//1
                        tr.append("<td style='text-align: center'>" + this['HSNCODE'].toString().trim() + "</td>");//2
                        tr.append("<td style='width:250px'>" + this['PRODUCTNAME'].toString().trim() + "</td>");//3
                        tr.append("<td style='text-align: center; display: none;'>" + this['BATCHNO'].toString().trim() + "</td>");//4
                        tr.append("<td style='display: none'>" + this['PACKINGSIZEID'].toString().trim() + "</td>");//5
                        tr.append("<td style='display: none'>" + this['PACKINGSIZENAME'].toString().trim() + "</td>");//6
                        tr.append("<td style='text-align: right'>" + this['MRP'].toString().trim() + "</td>");//7
                        tr.append("<td style='text-align: right'>" + parseFloat(this['RATE'].toString().trim()).toFixed(4) + "</td>");//8
                        tr.append("<td style='text-align: right; display: none;'>" + parseFloat(this['QTY'].toString().trim()).toFixed(3) + "</td>");//9
                        if (this['PACKINGSIZEID'].toString().trim() == 'B9F29D12-DE94-40F1-A668-C79BF1BF4425') {
                            tr.append("<td style='text-align: right'>" + parseInt(this['QTYPCS'].toString().trim()) + "</td>");//10
                        }
                        else {
                            tr.append("<td style='text-align: right'>" + parseFloat(this['QTYPCS'].toString().trim()).toFixed(3) + "</td>");//10
                        }
                        tr.append("<td style='text-align: right'>" + parseFloat(this['AMOUNT'].toString().trim()).toFixed(2) + "</td>");//11
                        tr.append("<td style='display: none'>" + this['ASSESMENTPERCENTAGE'].toString().trim() + "</td>");//12
                        tr.append("<td style='display: none'>" + parseFloat(this['TOTALASSESMENTVALUE'].toString().trim()).toFixed(2) + "</td>");//13
                        tr.append("<td style='display: none'>" + this['WEIGHT'].toString().trim() + "</td>");//14
                        tr.append("<td>" + this['GROSSWEIGHT'].toString().trim() + "</td>");//15
                        tr.append("<td style='text-align: center; display: none;'>" + this['MFDATE'].toString().trim() + "</td>");//16
                        tr.append("<td style='text-align: center; display: none;'>" + this['EXPRDATE'].toString().trim() + "</td>");//17  
                        
                        IgstPercentage = GetTaxOnEdit(invoiceid.trim(), Igstid, this['PRODUCTID'].toString().trim(), this['BATCHNO'].toString().trim());
                        IgstAmount = ((parseFloat(this['AMOUNT'].toString().trim()) * IgstPercentage) / 100);
                        NetAmount = (parseFloat(this['AMOUNT'].toString().trim()) + IgstAmount);
                        tr.append("<td style='text-align: right'>" + parseFloat(IgstPercentage).toFixed(2) + "</td>");//18
                        tr.append("<td style='text-align: right'>" + IgstAmount.toFixed(2) + "</td>");//19
                        tr.append("<td style='text-align: right'>" + NetAmount.toFixed(2) + "</td>");//20

                        tr.append("<td style='display: none'>" + this['STORELOCATIONID'].toString().trim() + "</td>");//21
                        tr.append("<td style='display: none'>" + this['STORELOCATIONNAME'].toString().trim() + "</td>");//22

                        tr.append("<td style='text-align: center'><input type='image' class='gvTempDelete'  id='btndelete' <img src='../Images/ico_delete_16.png'/></input></td>");
                        $("#productDetailsGrid").append(tr);
                        tr.append("</tbody>");
                        RowCountEdit();
                        CalculateAmount();
                    });
                }
                else if ($("#hdntaxcount").val().trim() == '2') {
                    debugger;
                    tr = $('#productDetailsGrid');
                    HeaderCount = $('#productDetailsGrid thead th').length;
                    if (HeaderCount == 0) {
                        tr.append("<thead><tr><th>Sl.No.</th><th style='display: none'>Product ID</th><th>HSN Code</th><th>Product</th><th style='display: none'>Batch</th><th style='display: none'>PackSize ID</th><th style='display: none'>Pack Size</th><th>MRP</th><th>Rate</th><th style='display: none'>Case</th><th>Pcs</th><th>Amount</th><th style='display: none'>Assesmet(%)</th><th style='display: none'>Assesment Amt.</th><th style='display: none'>Net Wght.</th><th>Gross Wght.</th><th style='display: none'>Mfg.Date</th><th style='display: none'>Exp.Date</th><th>CGST(%)</th><th>CGST</th><th>SGST(%)</th><th>SGST</th><th>Net Amt.</th><th style='display: none'>Storelocation ID</th><th style='display: none'>Storelocation</th><th>Delete</th></tr></thead><tbody>");
                    }
                    $.each(listDetails, function (index, record) {
                        debugger;
                        tr = $('<tr/>');
                        tr.append("<td style='text-align: center'>" + srl + "</td>");//0
                        tr.append("<td style='display: none'>" + this['PRODUCTID'].toString().trim() + "</td>");//1
                        tr.append("<td style='text-align: center'>" + this['HSNCODE'].toString().trim() + "</td>");//2
                        tr.append("<td style='width:250px'>" + this['PRODUCTNAME'].toString().trim() + "</td>");//3
                        tr.append("<td style='text-align: center; display: none;'>" + this['BATCHNO'].toString().trim() + "</td>");//4
                        tr.append("<td style='display: none'>" + this['PACKINGSIZEID'].toString().trim() + "</td>");//5
                        tr.append("<td style='display: none'>" + this['PACKINGSIZENAME'].toString().trim() + "</td>");//6
                        tr.append("<td style='text-align: right'>" + this['MRP'].toString().trim() + "</td>");//7
                        tr.append("<td style='text-align: right'>" + parseFloat(this['RATE'].toString().trim()).toFixed(2) + "</td>");//8
                        tr.append("<td style='text-align: right; display: none;'>" + parseFloat(this['QTY'].toString().trim()).toFixed(3) + "</td>");//9
                        if (this['PACKINGSIZEID'].toString().trim() == 'B9F29D12-DE94-40F1-A668-C79BF1BF4425') {
                            tr.append("<td style='text-align: right'>" + parseInt(this['QTYPCS'].toString().trim()) + "</td>");//10
                        }
                        else {
                            tr.append("<td style='text-align: right'>" + parseFloat(this['QTYPCS'].toString().trim()).toFixed(3) + "</td>");//10
                        }
                        tr.append("<td style='text-align: right'>" + parseFloat(this['AMOUNT'].toString().trim()).toFixed(2) + "</td>");//11
                        tr.append("<td style='display: none'>" + this['ASSESMENTPERCENTAGE'].toString().trim() + "</td>");//12
                        tr.append("<td style='display: none'>" + parseFloat(this['TOTALASSESMENTVALUE'].toString().trim()).toFixed(2) + "</td>");//13
                        tr.append("<td style='display: none'>" + this['WEIGHT'].toString().trim() + "</td>");//14
                        tr.append("<td>" + this['GROSSWEIGHT'].toString().trim() + "</td>");//15
                        tr.append("<td style='text-align: center; display: none;'>" + this['MFDATE'].toString().trim() + "</td>");//16
                        tr.append("<td style='text-align: center; display: none;'>" + this['EXPRDATE'].toString().trim() + "</td>");//17  
                        CgstPercentage = GetTaxOnEdit(invoiceid.trim(), Cgstid, this['PRODUCTID'].toString().trim(), this['BATCHNO'].toString().trim());
                        CgstAmount = ((parseFloat(this['AMOUNT'].toString().trim()) * CgstPercentage) / 100);
                        SgstPercentage = GetTaxOnEdit(invoiceid.trim(), Sgstid, this['PRODUCTID'].toString().trim(), this['BATCHNO'].toString().trim());
                        SgstAmount = ((parseFloat(this['AMOUNT'].toString().trim()) * SgstPercentage) / 100);
                        NetAmount = (parseFloat(this['AMOUNT'].toString().trim()) + CgstAmount + SgstAmount);
                        tr.append("<td style='text-align: right'>" + parseFloat(CgstPercentage).toFixed(2) + "</td>");//18
                        tr.append("<td style='text-align: right'>" + CgstAmount.toFixed(2) + "</td>");//19
                        tr.append("<td style='text-align: right'>" + parseFloat(SgstPercentage).toFixed(2) + "</td>");//20
                        tr.append("<td style='text-align: right'>" + SgstAmount.toFixed(2) + "</td>");//21
                        tr.append("<td style='text-align: right'>" + NetAmount.toFixed(2) + "</td>");//22

                        tr.append("<td style='display: none'>" + this['STORELOCATIONID'].toString().trim() + "</td>");//23
                        tr.append("<td style='display: none'>" + this['STORELOCATIONNAME'].toString().trim() + "</td>");//24

                        tr.append("<td style='text-align: center'><input type='image' class='gvTempDelete'  id='btndelete' <img src='../Images/ico_delete_16.png'/></input></td>");
                        $("#productDetailsGrid").append(tr);
                        tr.append("</tbody>");
                        RowCountEdit();
                        CalculateAmount();
                    });
                }
                else if ($("#hdntaxcount").val().trim() == '3') {
                    debugger;
                    tr = $('#productDetailsGrid');
                    HeaderCount = $('#productDetailsGrid thead th').length;
                    if (HeaderCount == 0) {
                        tr.append("<thead><tr><th>Sl.No.</th><th style='display: none'>Product ID</th><th>HSN Code</th><th>Product</th><th style='display: none'>Batch</th><th style='display: none'>PackSize ID</th><th style='display: none'>Pack Size</th><th>MRP</th><th>Rate</th><th style='display: none'>Case</th><th>Pcs/kg</th><th>Amount</th><th style='display: none'>Assesmet(%)</th><th style='display: none'>Assesment Amt.</th><th style='display: none'>Net Wght.</th><th>Gross Wght.</th><th style='display: none'>Mfg.Date</th><th style='display: none'>Exp.Date</th><th>CGST(%)</th><th>CGST</th><th>SGST(%)</th><th>SGST</th><th>TCS(%)</th><th>TCS</th><th>Net Amt.</th><th style='display: none'>Storelocation ID</th><th style='display: none'>Storelocation</th><th>Delete</th></tr></thead><tbody>");
                    }
                    $.each(listDetails, function (index, record) {
                        debugger;
                        tr = $('<tr/>');
                        tr.append("<td style='text-align: center'>" + srl + "</td>");//0
                        tr.append("<td style='display: none'>" + this['PRODUCTID'].toString().trim() + "</td>");//1
                        tr.append("<td style='text-align: center'>" + this['HSNCODE'].toString().trim() + "</td>");//2
                        tr.append("<td style='width:250px'>" + this['PRODUCTNAME'].toString().trim() + "</td>");//3
                        tr.append("<td style='text-align: center; display: none;'>" + this['BATCHNO'].toString().trim() + "</td>");//4
                        tr.append("<td style='display: none'>" + this['PACKINGSIZEID'].toString().trim() + "</td>");//5
                        tr.append("<td style='display: none'>" + this['PACKINGSIZENAME'].toString().trim() + "</td>");//6
                        tr.append("<td style='text-align: right'>" + this['MRP'].toString().trim() + "</td>");//7
                        tr.append("<td style='text-align: right'>" + parseFloat(this['RATE'].toString().trim()).toFixed(2) + "</td>");//8
                        tr.append("<td style='text-align: right; display: none;'>" + parseFloat(this['QTY'].toString().trim()).toFixed(3) + "</td>");//9
                        if (this['PACKINGSIZEID'].toString().trim() == 'B9F29D12-DE94-40F1-A668-C79BF1BF4425') {
                            tr.append("<td style='text-align: right'>" + parseInt(this['QTYPCS'].toString().trim()) + "</td>");//10
                        }
                        else {
                            tr.append("<td style='text-align: right'>" + parseFloat(this['QTYPCS'].toString().trim()).toFixed(3) + "</td>");//10
                        }
                        tr.append("<td style='text-align: right'>" + parseFloat(this['AMOUNT'].toString().trim()).toFixed(2) + "</td>");//11
                        tr.append("<td style='display: none'>" + this['ASSESMENTPERCENTAGE'].toString().trim() + "</td>");//12
                        tr.append("<td style='display: none'>" + parseFloat(this['TOTALASSESMENTVALUE'].toString().trim()).toFixed(2) + "</td>");//13
                        tr.append("<td style='display: none'>" + this['WEIGHT'].toString().trim() + "</td>");//14
                        tr.append("<td>" + this['GROSSWEIGHT'].toString().trim() + "</td>");//15
                        tr.append("<td style='text-align: center; display: none;'>" + this['MFDATE'].toString().trim() + "</td>");//16
                        tr.append("<td style='text-align: center; display: none;'>" + this['EXPRDATE'].toString().trim() + "</td>");//17
                        CgstPercentage = GetTaxOnEdit(invoiceid.trim(), Cgstid, this['PRODUCTID'].toString().trim(), this['BATCHNO'].toString().trim());
                        CgstAmount = ((parseFloat(this['AMOUNT'].toString().trim()) * CgstPercentage) / 100);
                        SgstPercentage = GetTaxOnEdit(invoiceid.trim(), Sgstid, this['PRODUCTID'].toString().trim(), this['BATCHNO'].toString().trim());
                        SgstAmount = ((parseFloat(this['AMOUNT'].toString().trim()) * SgstPercentage) / 100);
                        TcsPercentage = GetTaxOnEdit(invoiceid.trim(), Tcsid, this['PRODUCTID'].toString().trim(), this['BATCHNO'].toString().trim());
                        TcsAmount = ((parseFloat(this['AMOUNT'].toString().trim()) * TcsPercentage) / 100);
                        NetAmount = (parseFloat(this['AMOUNT'].toString().trim()) + CgstAmount + SgstAmount + TcsAmount);
                        tr.append("<td style='text-align: right'>" + parseFloat(CgstPercentage).toFixed(2) + "</td>");//18
                        tr.append("<td style='text-align: right'>" + CgstAmount.toFixed(2) + "</td>");//19
                        tr.append("<td style='text-align: right'>" + parseFloat(SgstPercentage).toFixed(2) + "</td>");//20
                        tr.append("<td style='text-align: right'>" + SgstAmount.toFixed(2) + "</td>");//21
                        tr.append("<td style='text-align: right'>" + parseFloat(TcsPercentage).toFixed(2) + "</td>");//22
                        tr.append("<td style='text-align: right'>" + TcsAmount.toFixed(2) + "</td>");//23
                        tr.append("<td style='text-align: right'>" + NetAmount.toFixed(2) + "</td>");//24
                        tr.append("<td style='display: none'>" + this['STORELOCATIONID'].toString().trim() + "</td>");//25
                        tr.append("<td style='display: none'>" + this['STORELOCATIONNAME'].toString().trim() + "</td>");//26
                        tr.append("<td style='text-align: center'><input type='image' class='gvTempDelete'  id='btndelete' <img src='../Images/ico_delete_16.png'/></input></td>");
                        $("#productDetailsGrid").append(tr);
                        tr.append("</tbody>");
                        RowCountEdit();
                        CalculateAmount();
                    });
                }
            }

            /*Tax Details Info*/
            if ($("#hdntaxcount").val() == '1') {
                $.each(listTax, function (index, record) {
                    debugger;
                    addRowinTaxTableEdit(this['PRODUCTID'].toString().trim(), this['BATCHNO'].toString().trim(), this['TAXID'].toString().trim(), this['TAXPERCENTAGE'].toString().trim(), this['TAXVALUE'].toString().trim(), this['MRP'].toString().trim(), '1');
                });
            }
            else if ($("#hdntaxcount").val() == '2') {
                $.each(listTax, function (index, record) {
                    debugger;
                    addRowinTaxTableEdit(this['PRODUCTID'].toString().trim(), this['BATCHNO'].toString().trim(), this['TAXID'].toString().trim(), this['TAXPERCENTAGE'].toString().trim(), this['TAXVALUE'].toString().trim(), this['MRP'].toString().trim(), '1');
                });
            }
            else if ($("#hdntaxcount").val() == '3') {
                $.each(listTax, function (index, record) {
                    debugger;
                    addRowinTaxTableEdit(this['PRODUCTID'].toString().trim(), this['BATCHNO'].toString().trim(), this['TAXID'].toString().trim(), this['TAXPERCENTAGE'].toString().trim(), this['TAXVALUE'].toString().trim(), this['MRP'].toString().trim(), '1');
                });
            }
            else {
                addRowinTaxTableEdit('NA','NA','NA',0, 0, 0, '0');
            }

            /*Footer Details Info*/
            if (listFooter.length > 0) {
                $.each(listFooter, function (index, record) {
                    $('#RoundOff').val(parseFloat(this['ROUNDOFFVALUE'].toString().trim()).toFixed(2));
                    $('#TaxAmt').val(parseFloat(this['TOTALTAXAMT'].toString().trim()).toFixed(2));
                    $('#NetAmt').val(parseFloat(this['TOTALSALEINVOICEVALUE'].toString().trim()).toFixed(2));
                    $("#TotalCase").val(this['ACTUALTOTCASE'].toString().trim());
                });
            }

            debugger;
            bindPolicyNo($('#ID').val().trim(), PolicyNo.trim(), invoiceid);
            bindFgSaleOrder($('#CUSTOMERID').val().trim(), $('#BRID').val().trim(), OrderID, invoiceid);
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
                bindtradinginvoicegrid();
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
    ReleaseSession();

    EditDetails(VoucherID);

    
    $('#btnAddnew').css("display", "none");
    $('#btnsave').css("display", "none");
    $('#btnApprove').css("display", "none");
    
    $("#dvAdd").find("input, textarea, select,submit").attr("disabled", "disabled");
    $('#divTransferNo').css("display", "");
    $('#divSourceDepot').css("display", "none");
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
    

    $("#InvoiceDate").datepicker("destroy");
}


/*Delete function for item details grid*/
$(function () {
    var grdproductid;
    var grdbatchno;
    var deleteflag = 0;
    $("body").on("click", "#productDetailsGrid .gvTempDelete", function () {
        var row = $(this).closest("tr");
        grdproductid = row.find('td:eq(1)').html();
        grdbatchno = row.find('td:eq(4)').html();
        if (confirm("Do you want to delete this item?")) {
            deleteflag = 1;
            row.remove();
            RowCount();
            CalculateAmount();
            deleteRowfromTaxTable(grdproductid, grdbatchno);
        }
        
        var rowCount = document.getElementById("productDetailsGrid").rows.length - 1;
        if ($('#hdndispatchID').val() == '0') {
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
            }
            else {
                $('#productDetailsGrid').empty();
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
            $("#FGInvoiceNo").attr("disabled", "disabled");
            $("#GatepassDate").attr("disabled", "disabled");
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
            $("#BRID").attr("disabled", "disabled");
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

            $("#StorelocationID").val('0');
            $("#StorelocationID").chosen({
                search_contains: true
            });
            $("#StorelocationID").trigger("chosen:updated");
           

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
        $("#dvAdd").find("input, textarea, select,submit").attr("disabled", "disabled"); 
        $('#divTransferNo').css("display", "");
        $('#divSourceDepot').css("display", "none");
        $("#btnclose").prop("disabled",false);
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

        $("#StorelocationID").val('0');
        $("#StorelocationID").chosen({
            search_contains: true
        });
        $("#StorelocationID").trigger("chosen:updated");
        

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

/*Print function for Invoice*/
$(function () {
    var invoiceid;
    /*var menuid = $("#hdnmenuid").val();*/
    $("body").on("click", "#invoiceGridFG .gvPrint", function () {
        var row = $(this).closest("tr");
        invoiceid = row.find('td:eq(1)').html();
        $('#hdndispatchID').val(invoiceid);
        var url = "http://mcnroeerp.com/factory/FACTORY/frmPrintPopUpV2_FAC.aspx?pid=" + invoiceid + "&BSID=" + BusinessSegment;
        window.open(url, "", "width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100");
        
    })

})

