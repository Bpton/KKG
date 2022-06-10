//#region Developer Info
/*
* For FactoryDispatchFG.cshtml Only
* Developer Name : Avishek Ghosh 
* Start Date     : 25/09/2020
* END Date       : 
 
*/
//#endregion
var CHECKER;
var backdatestatus;
var entryLockstatus;
$(document).ready(function () {

    var qs = getQueryStrings();
    if (qs["CHECKER"] != undefined && qs["CHECKER"] != "") {
        CHECKER = qs["CHECKER"];
    }

    if (qs["MENUID"] != undefined && qs["MENUID"] != "") {
        var menuid = qs["MENUID"];
    }
    $("#hdnCHECKER").val(CHECKER);
    $("#hdnmenuid").val(menuid);

    finyearChecking();

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
        //"showMethod": "fadeIn",
        //"hideMethod": "fadeOut"
        "showMethod": "slideDown",
        "hideMethod": "slideUp"
    };

    //$("productDetailsGrid").fixMe();

    $('#divTransferNo').css("display", "none");
    $('#dvAdd').css("display", "none");
    $('#dvDisplay').css("display", "");
    $("#txtfrmdate").attr("disabled", "disabled");
    $("#txttodate").attr("disabled", "disabled");
    $("#TransferNo").attr("disabled", "disabled");

    //if (CHECKER == 'FALSE') {
        $('#btnsave').css("display", "");
        $('#btnAddnew').css("display", "");
        $('#btnApprove').css("display", "none");
    //}
    //else {
    //    $('#btnAddnew').css("display", "none");
    //    $('#btnsave').css("display", "none");
    //    $('#btnApprove').css("display", "");
    //}    
    //bindsourceDepot();
    //bindfgdispatchgrid();
    //bindWayBill();
    bindInsurance();
    //bindcategory();

    $("#BRID").prop("disabled", true);
    $("#TODEPOTID").prop("disabled", false);


    $("#btnsearch").click(function (e) {
        bindfgdispatchgrid();
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
        bindfgdispatchgrid();
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
        //$("#INSURANCE_NO").trigger("chosen:updated");
        $('#INSURANCE_NO').trigger('chosen:updated');
        e.preventDefault();
    })

    $('#TODEPOTID').change(function (e) {
        if ($('#TODEPOTID').val() != '0') {
            bindshippingAddress();
            bindtransitdays();
            bindtaxcount();
            $("#ID").focus();
            $('#ID').trigger('chosen:updated');
        }
        e.preventDefault();
    })

    $('#CATID').change(function (e) {

        var category = $("#CATID").val();
        if (category != '0') {
            bindCategorywiseProduct();
        }
        else {
            bindAllProduct();
        }
        $('#ddlBatch').empty();
        clearafterAdd();
        $("#PRODUCTID").focus();
        $('#PRODUCTID').trigger('chosen:updated');
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
        $("#DispatchQty").focus();
        e.preventDefault();
    })

    $("#btnadd").click(function (e) {
        debugger;
        if ($('#ddlBatch').val() == '0') {
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
        else if ($('#TODEPOTID').val() == '0') {
            toastr.warning('<b><font color=black>Please select destination depot..!</font></b>');
            return false;
        }
        else if ($('#DispatchQty').val() == '') {
            toastr.warning('<b><font color=black>Please enter dispatch quantity..!</font></b>');
            return false;
        }
        else if (parseFloat($('#DispatchQty').val()) == 0) {
            toastr.warning('<b><font color=black>Dispatch quantity should not be 0..!</font></b>');
            return false;
        }
        else if (parseFloat($('#DispatchQty').val()) > parseFloat($('#StockQty').val())) {
            toastr.warning('<b><font color=black>Dispatch quantity should not greater than Stock quantity..!</font></b>');
            return false;
        }
        else if (parseFloat($('#hdnrate').val().trim()) <= 0) {
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

        entryLockstatus = bindlockdateflag($("#DispatchDate").val());
        if (entryLockstatus == '0') {
            toastr.warning('<b><font color=black>Entry date is locked,please contact with admin...!</font></b>');
            return false;
        }
        else {
            var itemrowCount = document.getElementById("productDetailsGrid").rows.length - 1;
            if ($("#TODEPOTID").val() == '0') {
                toastr.warning('<b><font color=black>Please select destination depot..!</font></b>');
                return false;
            }
            else if ($("#ID").val() == '0') {
                toastr.warning('<b><font color=black>Please select insurance company..!</font></b>');
                return false;
            }
            else if ($("#INSURANCE_NO").val() == '0') {
                toastr.warning('<b><font color=black>Please select insurance no..!</font></b>');
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
            else if (itemrowCount == 0) {
                toastr.warning('<b><font color=black>Please add Product..!</font></b>');
                return false;
            }
            else {
                SaveDispatch();
            }
        }
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
    $("#DispatchDate").datepicker({
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
    $("#DispatchDate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
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

function bindsourceDepot() {
    var SourceDepot = $("#BRID");
    $.ajax({
        type: "POST",
        url: "/tranfac/GetSourceDepot",
        data: '{}',
        async: false,
        dataType: "json",

        success: function (response) {
            //alert(JSON.stringify(response));
            SourceDepot.empty();
            $.each(response, function () {
                SourceDepot.append($("<option></option>").val(this['BRID']).html(this['BRNAME']));
            });
            var sourceDepot = $('#BRID').val();
            binddestinationDepot(sourceDepot);
            bindtransporter(sourceDepot, '0');
            bindProduct(sourceDepot);

        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function binddestinationDepot(sourceDepot) {
    var DestinationDepot = $("#TODEPOTID");
    $.ajax({
        type: "POST",
        url: "/tranfac/GetDestinationDepot",
        data: { SourceDepot: sourceDepot },
        async: false,
        dataType: "json",
        success: function (response) {
            //alert(JSON.stringify(response));
            DestinationDepot.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(response, function () {
                DestinationDepot.append($("<option></option>").val(this['TODEPOTID']).html(this['BRPREFIX']));
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
        data: { DepotID: $('#TODEPOTID').val() },
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

function bindPolicyNo(company,insuranceno,dispatchID) {
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
            if (dispatchID != '0') {
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
    $("#dialog").dialog({
        autoOpen: true,
        modal: true,
        title: "Loading.."
    });
    $("#imgLoader").css("visibility", "visible");
    $.ajax({
        type: "POST",
        url: "/tranfac/GetShippingAddress",
        data: { FromDepot: $('#BRID').val(), ToDepot: $('#TODEPOTID').val() },
        async:false,
        dataType: "json",
        success: function (shipping) {
            //alert(JSON.stringify(shipping));
            $.each(shipping, function (key, item) {
                $("#ShippingAddress").val(item.ShippingAddress);
                $("#Transporter").val(item.hdnTransporterID);
                $("#TransporterID").val(item.hdnTransporterID);
            });
            $("#TransporterID").chosen({
                search_contains: true
            });
            $("#TransporterID").trigger("chosen:updated");

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

function bindtransitdays() {
    $.ajax({
        type: "POST",
        url: "/tranfac/GetTransitDays",
        data: { FromDepot: $('#BRID').val().trim(), ToDepot: $('#TODEPOTID').val().trim(), InvoiceDate: $('#DispatchDate').val().trim() },
        async: false,
        dataType: "json",
        success: function (days) {
            $.each(days, function (key, item) {
                $("#DeliveryDate").val(item.TRANSIT_DAYS);
                
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

function bindcategory() {
    var category = $("#CATID");
    $.ajax({
        type: "POST",
        url: "/tranfac/GetCategory",
        data: '{}',
        async: false,
        dataType: "json",
        success: function (response) {
            category.empty().append('<option selected="selected" value="0">All</option>');
            $.each(response, function () {
                category.append($("<option></option>").val(this['CATID']).html(this['CATNAME']));
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

function bindProduct(sourceDepot) {
    var Product = $("#PRODUCTID");
    $.ajax({
        type: "POST",
        url: "/tranfac/GetProduct",
        data: { SourceDepot: sourceDepot,Type: 'FG' },
        async: false,
        dataType: "json",
        success: function (response) {
            Product.empty().append('<option selected="selected" value="0">Select Product</option>');
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

function bindPacksize() {
    debugger;
    var Packsize = $("#PSID");
    //var counter = 0;
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
        url: "/tranfac/GetTaxcount",
        data: { MenuID: $('#hdnmenuid').val(), Flag: '1', DepotID: $('#BRID').val(), ProductID: '0', CustomerID: $('#TODEPOTID').val(), Date: $('#DispatchDate').val() },
        dataType: "json",
        success: function (taxcount) {
            //alert(JSON.stringify(taxcount));
            //alert(taxcount.length);
            if (taxcount.length > 0) {
                $.each(taxcount, function (key, item) {
                    $("#hdntaxcount").val(item.TAXCOUNT);
                    $("#hdntaxname").val(item.TAXNAME);
                    $("#hdntaxpercentage").val(item.TAXPERCENTAGE);
                    $("#hdnrelatedto").val(item.TAXRELATEDTO);
                });
            }
            else {
                $("#hdntaxcount").val(0);
                $("#hdntaxname").val('');
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

function bindCategorywiseProduct() {
    var Product = $("#PRODUCTID");
    $("#dialog").dialog({
        autoOpen: true,
        modal: true,
        title: "Loading.."
    });
    $("#imgLoader").css("visibility", "visible");
    $.ajax({
        type: "POST",
        url: "/tranfac/GetCategoryProduct",
        data: { CategoryID: $('#CATID').val(), DepotID: $('#BRID').val(), Type:'FG' },
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
        url: "/tranfac/GetBatchDetails",
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
            if (counter > 0) {
                if (counter == 1) {
                    $("#ddlBatch").val(val1)
                    getBatchDetails();
                    $("#DispatchQty").focus();
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
    //debugger;
    var batchmrp;
    var batchassessvalue;
    var batchmfgdt;
    var batchexpdt;
    var strval2 = $("#ddlBatch").val();
    var splitval = strval2.split('|');
    var transferdate = $("#DispatchDate").val();
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
    $("#StockQty").val(stockqty);
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
    bindtransferRate(productid, mrp, transferdate);
}

function bindtransferRate(productid, mrp, transferdate) {
    $.ajax({
        type: "POST",
        url: "/tranfac/GetTransferRate",
        data: { productID: productid.trim(), mrp: mrp.trim(), transferDate: transferdate.trim() },
        dataType: "json",
        success: function (rate) {
            //alert(JSON.stringify(shipping));
            $.each(rate, function (key, item) {
                $("#hdnrate").val(item.hdnRATE);
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

function addProductInDetailsGrid() {
    debugger;
    var assesment = '';
    var qty = '';
    var netweight = '';
    var grossweight = '';
    var hsn = '';
    var hsntax = '';
    var Amount = 0;
    var strval = $('#PRODUCTID').find('option:selected').text().trim();
    var splitval = strval.split('~');
    var productname = splitval[0];
    var batchno = $("#hdnbatchno").val().trim();
    var packsizename = $('#PSID').find('option:selected').text();
    var TransferQty = $('#DispatchQty').val();
    //var Rate = parseFloat($('#hdnrate').val());
    var Rate = 0;
    var DiscRate = 0;
    debugger;
    if ($('#TODEPOTID').val().trim() == '0EEDDA49-C3AB-416A-8A44-0B9DFECD6670' ||
        $('#TODEPOTID').val().trim() == '2F142E90-8927-4123-A935-B57B4CCEDA97' ||
        $('#TODEPOTID').val().trim() == '84E03F75-859A-4873-B5EE-07F97DB5B23E' ||
        $('#TODEPOTID').val().trim() == 'C81DAE4D-AD11-4C7F-AA76-AA5E60D47CF0') {

        Rate = parseFloat($('#hdnrate').val().trim()).toFixed(2);
        DiscRate = parseFloat(Rate - ((Rate * 10) / 100)).toFixed(2);
    }
    else {
        DiscRate = parseFloat($('#hdnrate').val().trim()).toFixed(2);
    }
    var MRP = $('#MRP').val();
    var AssesmentPercentage = $("#hdn_ASSESMENTPERCENTAGE").val();
    var Assesmentvalue = 0;
    var TaxPercentage = 0;
    var TaxAmount = 0;
    var NetAmount = 0;
    var Tag = "A";
    var BeforeEditedQty = 0;
    var srl = 0;
    srl = srl + 1; 
    $.ajax({
        type: "POST",
        url: "/tranfac/GetCalculateAmtInPcs",
        data: { Productid: $('#PRODUCTID').val().trim(), PacksizeID: $('#PSID').val().trim(), Qty: $('#DispatchQty').val().trim(), Rate: parseFloat(DiscRate), Assesment: $('#hdn_ASSESMENTPERCENTAGE').val().trim(), TaxName: $('#hdntaxname').val().trim(), date: $('#DispatchDate').val().trim()},
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
                        hsntax = this['HSNTAX'].trim();
                    });
                }
                else {
                    hsntax = 0;
                }
            }
            if ($("#hdntaxcount").val().trim() == '1') {
                if (listHSNTaxID.length > 0) {
                    $.each(listHSNTaxID, function (index, record) {
                        $("#hsntaxid").val(this['TAXID'].trim());
                    });
                }
                else {
                    $("#hsntaxid").val('1177D9CF-8F1E-4C91-B785-FDF940101EEE');
                }
            }
            //Add Mode
            if ($("#hdntaxcount").val().trim() == '1') {
                Amount = parseFloat(qty * DiscRate);
                Assesmentvalue = parseFloat(assesment);
                TaxPercentage = parseFloat(hsntax);
                if (isNaN(TaxPercentage) == true) {
                    TaxPercentage = 0;
                    TaxAmount = 0;
                }
                else {
                    TaxAmount = ((Amount * TaxPercentage) / 100);
                }
                NetAmount = Amount + TaxAmount;
                //Create Table 
                var tr;
                tr = $('#productDetailsGrid');
                var HeaderCount = $('#productDetailsGrid thead th').length;
                if (HeaderCount == 0) {
                    tr.append("<thead><tr><th>Sl.No.</th><th style='display: none'>Product ID</th><th>HSN Code</th><th>Product</th><th>Batch</th><th style='display: none'>PackSize ID</th><th>Pack Size</th><th>Qty</th><th>MRP</th><th>Rate</th><th>Amount</th><th style='display: none'>Assesmet(%)</th><th style='display: none'>Assesment Amt.</th><th style='display: none'>Net Wght.</th><th>Gross Wght.</th><th>Mfg.Date</th><th>Exp.Date</th><th style='display: none'>Tag</th><th style='display: none'>Before Edited Qty</th><th>IGST(%)</th><th>IGST</th><th>Net Amt.</th><th>Delete</th></tr></thead><tbody>");
                }
                tr = $('<tr/>');
                tr.append("<td style='text-align: center'>" + srl + "</td>");//0
                tr.append("<td style='display: none'>" + $('#PRODUCTID').val() + "</td>");//1
                tr.append("<td style='text-align: center'>" + hsn + "</td>");//2
                tr.append("<td style='width:250px'>" + productname + "</td>");//3
                tr.append("<td style='text-align: center'>" + batchno + "</td>");//4
                tr.append("<td style='display: none'>" + $('#PSID').val() + "</td>");//5
                tr.append("<td>" + packsizename + "</td>");//6
                if ($('#PSID').val().trim() == '1970C78A-D062-4FE9-85C2-3E12490463AF') {
                    tr.append("<td style='text-align: right'>" + parseFloat(TransferQty.trim()).toFixed(3) + "</td>");//7
                }
                else if ($('#PSID').val().trim() == 'B9F29D12-DE94-40F1-A668-C79BF1BF4425') {
                    tr.append("<td style='text-align: right'>" + parseFloat(TransferQty.trim()).toFixed(0) + "</td>");//7
                }
                else {
                    tr.append("<td style='text-align: right'>" + parseFloat(TransferQty.trim()).toFixed(3) + "</td>");//7
                }
                tr.append("<td style='text-align: right'>" + MRP + "</td>");//8
                tr.append("<td style='text-align: right'>" + DiscRate + "</td>");//9
                tr.append("<td style='text-align: right'>" + Amount + "</td>");//10
                tr.append("<td style='display: none'>" + AssesmentPercentage + "</td>");//11
                tr.append("<td style='display: none'>" + Assesmentvalue.toFixed(2) + "</td>");//12
                tr.append("<td style='display: none'>" + netweight + "</td>");//13
                tr.append("<td>" + grossweight + "</td>");//14
                tr.append("<td style='text-align: center'>" + $('#hdn_mfgdate').val() + "</td>");//15
                tr.append("<td style='text-align: center'>" + $('#hdn_exprdate').val() + "</td>");//16
                tr.append("<td style='display: none'>" + Tag + "</td>");//17
                tr.append("<td style='display: none'>" + BeforeEditedQty + "</td>");//18
                tr.append("<td style='text-align: right'>" + TaxPercentage.toFixed(2) + "</td>");//19
                tr.append("<td style='text-align: right'>" + TaxAmount.toFixed(2) + "</td>");//20
                tr.append("<td style='text-align: right'>" + NetAmount.toFixed(2) + "</td>");//21
                tr.append("<td style='text-align: center'><input type='image' class='gvTempDelete'  id='btndelete' <img src='../Images/ico_delete_16.png'/></input></td>");
                $("#productDetailsGrid").append(tr);
                tr.append("</tbody>");
                RowCount();
                CalculateAmount();
            }
            else if ($("#hdntaxcount").val() == '0') {
                Amount = qty * DiscRate;
                Assesmentvalue = parseFloat(assesment);
                TaxPercentage = 0;
                TaxAmount = 0;
                NetAmount = Amount;
                var tr;
                tr = $('#productDetailsGrid');
                var HeaderCount = $('#productDetailsGrid thead th').length;
                if (HeaderCount == 0) {
                    tr.append("<thead><tr><th>Sl.No.</th><th style='display: none'>Product ID</th><th>HSN Code</th><th>Product</th><th>Batch</th><th style='display: none'>PackSize ID</th><th>Pack Size</th><th>Qty</th><th>MRP</th><th>Rate</th><th>Amount</th><th style='display: none'>Assesmet(%)</th><th style='display: none'>Assesment Amt.</th><th style='display: none'>Net Wght.</th><th>Gross Wght.</th><th>Mfg.Date</th><th>Exp.Date</th><th style='display: none'>Tag</th><th style='display: none'>Before Edited Qty</th><th>Net Amt.</th><th>Delete</th></tr></thead><tbody>");
                }
                tr = $('<tr/>');
                tr.append("<td style='text-align: center'>" + srl + "</td>");//0
                tr.append("<td style='display: none'>" + $('#PRODUCTID').val() + "</td>");//1
                tr.append("<td style='text-align: center'>" + hsn + "</td>");//2
                tr.append("<td style='width:250px'>" + productname + "</td>");//3
                tr.append("<td style='text-align: center'>" + batchno + "</td>");//4
                tr.append("<td style='display: none'>" + $('#PSID').val() + "</td>");//5
                tr.append("<td>" + packsizename + "</td>");//6
                if ($('#PSID').val().trim() == '1970C78A-D062-4FE9-85C2-3E12490463AF') {
                    tr.append("<td style='text-align: right'>" + parseFloat(TransferQty.trim()).toFixed(3) + "</td>");//7
                }
                else if ($('#PSID').val().trim() == 'B9F29D12-DE94-40F1-A668-C79BF1BF4425') {
                    tr.append("<td style='text-align: right'>" + parseFloat(TransferQty.trim()).toFixed(0) + "</td>");//7
                }
                else {
                    tr.append("<td style='text-align: right'>" + parseFloat(TransferQty.trim()).toFixed(3) + "</td>");//7
                }
                tr.append("<td style='text-align: right'>" + MRP + "</td>");//8
                tr.append("<td style='text-align: right'>" + DiscRate + "</td>");//9
                tr.append("<td style='text-align: right'>" + Amount + "</td>");//10
                tr.append("<td style='display: none'>" + AssesmentPercentage + "</td>");//11
                tr.append("<td style='display: none'>" + Assesmentvalue.toFixed(2) + "</td>");//12
                tr.append("<td style='display: none'>" + netweight + "</td>");//13
                tr.append("<td>" + grossweight + "</td>");//14
                tr.append("<td style='text-align: center'>" + $('#hdn_mfgdate').val() + "</td>");//15
                tr.append("<td style='text-align: center'>" + $('#hdn_exprdate').val() + "</td>");//16
                tr.append("<td style='display: none'>" + Tag + "</td>");//17
                tr.append("<td style='display: none'>" + BeforeEditedQty + "</td>");//18
                tr.append("<td style='text-align: right'>" + NetAmount.toFixed(2) + "</td>");//21
                tr.append("<td style='text-align: center'><input type='image' class='gvTempDelete'  id='btndelete' <img src='../Images/ico_delete_16.png'/></input></td>");
                $("#productDetailsGrid").append(tr);
                tr.append("</tbody>");
                RowCount();
                CalculateAmount();
            }
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
    if ($("#hdntaxcount").val() == '1') {
        addRowinTaxTable(TaxPercentage, TaxAmount, MRP, '1');
    }
    else {
        addRowinTaxTable(TaxPercentage, TaxAmount, MRP, '0');
    }
    CalculateQuantity();
    clearafterAdd();
    $("#PRODUCTID").focus();
    $('#PRODUCTID').trigger('chosen:activate');
}

function addRowinTaxTable(TaxPercentage, TaxAmount, MRP,TaxFlag) {
    $.ajax({
        type: "POST",
        url: "/tranfac/FillTaxDatatable",
        data: { Productid: $('#PRODUCTID').val(), BatchNo: $("#hdnbatchno").val(), TaxID: $('#hsntaxid').val(), Percentage: parseFloat(TaxPercentage), TaxValue: parseFloat(TaxAmount), MRP: parseFloat(MRP), Flag: TaxFlag },
        async: false,
        dataType: "json",
        success: function (response) {

        }
    });
}
function addRowinTaxTableEdit(ProductID,batch,taxid,TaxPercentage, TaxAmount, MRP, TaxFlag) {
    $.ajax({
        type: "POST",
        url: "/tranfac/FillTaxDatatable",
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
        url: "/tranfac/DeleteTaxDatatable",
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
        $("#TODEPOTID").prop("disabled", true);
        $("#TODEPOTID").chosen({
            search_contains: true
        });
        $("#TODEPOTID").trigger("chosen:updated");
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
    var table = document.getElementById("dispatchGridFG");
    var rowCount = document.getElementById("dispatchGridFG").rows.length - 1;
    if (rowCount > 0) {
        for (var i = 1; i <= rowCount; i++) {
            table.rows[i].cells[0].innerHTML = i.toString();
        }
    }
}

function CalculateAmount() {
    var totalbasicamt = 0;
    var totaltaxamt = 0;
    var totalbasicplustaxamt = 0;
    var decimalvalue = 0;
    var parts = 0;
    var finalamt = 0;
    var rowCount = document.getElementById("productDetailsGrid").rows.length - 1;
    if (rowCount > 0) {
        if ($("#hdntaxcount").val() == '1') {
            $('#productDetailsGrid tbody tr').each(function () {
                totalbasicamt += parseFloat($(this).find('td:eq(10)').html().trim());
                totaltaxamt += parseFloat($(this).find('td:eq(20)').html().trim());
                totalbasicplustaxamt += parseFloat($(this).find('td:eq(21)').html().trim());
            });
        }
        else if ($("#hdntaxcount").val() == '0') {
            $('#productDetailsGrid tbody tr').each(function () {
                totalbasicamt += parseFloat($(this).find('td:eq(10)').html().trim());
                totaltaxamt += parseFloat(0);
                totalbasicplustaxamt += parseFloat($(this).find('td:eq(19)').html().trim());
            });
        }
        $('#BasicAmt').val(totalbasicamt.toFixed(2));
        $('#TaxAmt').val(totaltaxamt.toFixed(2));
        $('#GrossAmt1').val(totalbasicplustaxamt.toFixed(2));
        $('#GrossAmt').val(totalbasicplustaxamt.toFixed(2));
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
    }
}

function CalculateQuantity() {
    var caseqty = 0;
    var pcsqty = 0;
    var totalCase = 0;
    var totalPcs = 0;
    var rowCount = document.getElementById("productDetailsGrid").rows.length - 1;
    if (rowCount > 0) {
        $('#productDetailsGrid tbody tr').each(function () {
            $.ajax({
                type: "POST",
                url: "/tranfac/GetTotalQuantity",
                data: { Productid: $(this).find('td:eq(1)').html().trim(), FromPackSizeID: $(this).find('td:eq(5)').html().trim(), CasePacksizeID: '1970C78A-D062-4FE9-85C2-3E12490463AF', PCSPacksizeID: 'B9F29D12-DE94-40F1-A668-C79BF1BF4425', Qty: parseFloat($(this).find('td:eq(7)').html().trim()) },
                dataType: "json",
                success: function (response) {
                    var listcaseqty = response.allquantityDataset.varCase;
                    var listpcsqty = response.allquantityDataset.varPcs;
                    $.each(listcaseqty, function (index, record) {
                        caseqty = this['CASEQuantity'].toString().trim();
                    });
                    $.each(listpcsqty, function (index, record) {
                        pcsqty = this['PCSQuantity'].toString().trim();
                    });

                    totalCase += parseFloat(caseqty.trim());
                    totalPcs += parseFloat(pcsqty.trim());
                    $('#TotalCase').val(totalCase.toFixed(3));
                    $('#TotalPcs').val(totalPcs.toFixed(0));
                }
            });
        });


    }
    else {
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
    $("#TransferNo").attr("disabled", "disabled");
    $("#GatepassDate").attr("disabled", "disabled");
    $("#DispatchDate").attr("disabled", "disabled");
    $("#LrGrDate").attr("disabled", "disabled");
    $("#DeliveryDate").attr("disabled", "disabled");
    $("#MRP").attr("disabled", "disabled");
    $("#StockQty").attr("disabled", "disabled");
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

    $("#TODEPOTID").val('0');
    $("#TODEPOTID").prop("disabled", false);
    $("#TODEPOTID").chosen({
        search_contains: true
    });
    $("#TODEPOTID").trigger("chosen:updated");

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

    $("#VehichleNo").val('');
    $("#LrGrNo").val('');
    $("#GatepassNo").val('');
    $("#ShippingAddress").val('');
    $("#DeliveryDate").val('');

    debugger;
    //backdatestatus = bindbackdateflag('3105');
    //if (parseInt(backdatestatus) > 0) {
    //    $("#DispatchDate").datepicker({
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
    //    $("#DispatchDate").datepicker({
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

    $('#PSID').empty();
    $("#PSID").chosen({
        search_contains: true
    });
    $("#PSID").trigger("chosen:updated");

    $('#ddlBatch').empty();
    $('#DispatchQty').val('');
    $('#StockQty').val('');
    $('#MRP').val('');
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
    $('#DispatchQty').val('');
    $('#StockQty').val('');
    $('#MRP').val('');
}

function SaveDispatch() {
    debugger;
    var Taxreturnflag = true;

    /* Tax Checking Start*/
    if ($("#hdntaxcount").val() == '1') {
        $('#productDetailsGrid tbody tr').each(function () {
            var lineItemigstTax = 0;
            var TaxProduct = $(this).find('td:eq(3)').html().trim();
            var TaxBatch = $(this).find('td:eq(4)').html().trim();
            //lineItemigstTax = parseFloat($(this).find('td:eq(20)').html().trim());
            //if (parseFloat(lineItemigstTax) <= 0) {
            //    toastr.error('Tax amount should not be 0 for <b>' + TaxProduct + '</b> in batch - <b>' + TaxBatch + '</b>');
            //    Taxreturnflag = false;
            //    return false;
            //}
            if (isNaN($(this).find('td:eq(19)').html().trim()) == true) {
                toastr.error('Tax amount should not be 0 or NaN for <b>' + TaxProduct + '</b> in batch - <b>' + TaxBatch + '</b>');
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

        varDestinationDepotText = $("#TODEPOTID option:selected").text();
        $("#BRPREFIX").val(varDestinationDepotText);

        dispatchlist = new Array();
        var dispatchsave = {};

        dispatchsave.DESPATCHID = $('#hdndispatchID').val().trim();
        if ($('#hdndispatchID').val() == '0') {
            dispatchsave.FLAG = 'A';
        }
        else {
            dispatchsave.FLAG = 'U';
        }
        if ($('#hdntaxcount').val() == '1') {
            dispatchsave.InvoiceType = '1';
        }
        else {
            dispatchsave.InvoiceType = '0';
        }
        dispatchsave.Tranportmode = $("#Tranportmode").val().trim();
        dispatchsave.BRID = $("#BRID").val().trim();
        dispatchsave.BRNAME = $("#BRNAME").val().trim();
        dispatchsave.TODEPOTID = $("#TODEPOTID").val().trim();
        dispatchsave.BRPREFIX = $("#BRPREFIX").val().trim();
        dispatchsave.DispatchDate = $("#DispatchDate").val().trim();
        dispatchsave.WAYBILLNO = '0';
        dispatchsave.TransporterID = $("#TransporterID").val().trim();
        dispatchsave.VehichleNo = $("#VehichleNo").val().trim();
        dispatchsave.LrGrNo = $("#LrGrNo").val().trim();
        dispatchsave.LrGrDate = $("#LrGrDate").val().trim();
        dispatchsave.GatepassNo = $("#GatepassNo").val().trim();
        dispatchsave.GatepassDate = $("#GatepassDate").val().trim();
        dispatchsave.Remarks = $("#Remarks").val().trim();
        dispatchsave.NetAmt = $("#NetAmt").val().trim();
        dispatchsave.RoundOff = $("#RoundOff").val().trim();
        dispatchsave.ID = $("#ID").val().trim();
        dispatchsave.COMPANY_NAME = $("#COMPANY_NAME").val().trim();
        dispatchsave.INSURANCE_NO = $("#INSURANCE_NO").val().trim();
        dispatchsave.TotalCase = $("#TotalCase").val().trim();
        dispatchsave.TotalPcs = $("#TotalPcs").val().trim();
        dispatchsave.ShippingAddress = $("#ShippingAddress").val();
        dispatchsave.DeliveryDate = $("#DeliveryDate").val();

        $('#productDetailsGrid tbody tr').each(function () {

            //debugger
            var dispatchdetails = {};

            var productid = $(this).find('td:eq(1)').html().trim();
            var productname = $(this).find('td:eq(3)').html().trim();
            var batchno = $(this).find('td:eq(4)').html().trim();
            var packingsizeid = $(this).find('td:eq(5)').html().trim();
            var packingsizename = $(this).find('td:eq(6)').html().trim();
            var trnsferqty = $(this).find('td:eq(7)').html().trim();
            var mrp = $(this).find('td:eq(8)').html().trim();
            var rate = $(this).find('td:eq(9)').html().trim();
            var amount = $(this).find('td:eq(10)').html().trim();
            var assesmentpercentage = $(this).find('td:eq(11)').html().trim();
            var assesmentvalue = $(this).find('td:eq(12)').html().trim();
            var weight = $(this).find('td:eq(13)').html().trim();
            var grossweight = $(this).find('td:eq(14)').html().trim();
            var mfdate = $(this).find('td:eq(15)').html().trim();
            var exprdate = $(this).find('td:eq(16)').html().trim();
            var storelocationid = '113BD8D6-E5DC-4164-BEE7-02A16F97ABCC';

            dispatchdetails.PRODUCTID = productid;
            dispatchdetails.PRODUCTNAME = productname;
            dispatchdetails.PACKINGSIZEID = packingsizeid;
            dispatchdetails.PACKINGSIZENAME = packingsizename;
            dispatchdetails.MRP = mrp;
            dispatchdetails.QTY = trnsferqty;
            dispatchdetails.RATE = rate;
            dispatchdetails.BATCHNO = batchno;
            dispatchdetails.AMOUNT = amount;
            dispatchdetails.ASSESMENTPERCENTAGE = assesmentpercentage;
            dispatchdetails.TOTALASSESMENTVALUE = assesmentvalue;
            dispatchdetails.WEIGHT = weight;
            dispatchdetails.GROSSWEIGHT = grossweight;
            dispatchdetails.MFDATE = mfdate;
            dispatchdetails.EXPRDATE = exprdate;
            dispatchdetails.STORELOCATIONID = storelocationid;
            dispatchlist[i++] = dispatchdetails;
        });
        dispatchsave.DispatchDetailsFG = dispatchlist,

            //alert(JSON.stringify(dispatchsave));

            $.ajax({

                url: "/tranfac/fgDispatchsavedata",
                data: '{dispatchsave:' + JSON.stringify(dispatchsave) + '}',
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
                        bindfgdispatchgrid();
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
        url: "/tranfac/RemoveSession",
        data: '{}',
        dataType: "json",
        success: function (response) {
           
            
        }
    });
}

function bindfgdispatchgrid() {
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
        url: "/tranfac/BindFgDispatchGrid",
        data: { FromDate: $('#txtfrmdate').val().trim(), ToDate: $('#txttodate').val().trim(), CheckerFlag: CHECKER.trim(), depotID: $('#BRID').val().trim(), type: 'FG' },
        dataType: "json",
        success: function (response) {
            var tr;
            tr = $('#dispatchGridFG');
            tr.append("<thead><tr><th>Sl. No.</th><th style='display: none'>DispatchId</th><th>Dispatch No</th><th>Dispatch Date</th><th style='display: none'>MOTHERDEPOTID</th><th style='display: none'>MOTHERDEPOTNAME</th><th style='display: none'>TODEPOTID</th><th> Dispatch To</th><th style='display: none'>TRANSPORTERID</th><th>Transporter</th><th style='display: none'>ISVERIFIED</th><th>Financial Status</th><th style='display: none'>NEXTLEVELID</th><th>Entry User</th><th>Total Case</th> <th>Total Pcs</th><th>Net Amount</th><th>Print</th><th>Edit</th><th>View</th><th>Cancel</th>");
            
            $('#dispatchGridFG').DataTable().destroy();
            $("#dispatchGridFG tbody tr").remove();
            for (var i = 0; i < response.length; i++) {
                tr = $('<tr/>');
                tr.append("<td style='text-align: center'>" + srl + "</td>");//0
                tr.append("<td style='display: none'>" + response[i].STOCKTRANSFERID + "</td>");
                tr.append("<td>" + response[i].STOCKTRANSFERNO + "</td>");
                tr.append("<td>" + response[i].STOCKTRANSFERDATE + "</td>");
                tr.append("<td style='display: none'>" + response[i].MOTHERDEPOTID + "</td>");
                tr.append("<td style='display: none'>" + response[i].MOTHERDEPOTNAME + "</td>");
                tr.append("<td style='display: none'>" + response[i].TODEPOTID + "</td>");
                tr.append("<td>" + response[i].TODEPOTNAME + "</td>");
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
                tr.append("<td>" + response[i].TOTALCASE + "</td>");
                tr.append("<td>" + response[i].TOTALPCS + "</td>");
                tr.append("<td>" + response[i].NETAMOUNT + "</td>");
                tr.append("<td style='text-align: center'><input type='image' class='gvPrint'  id='btndispatchprint'  <img src='../Images/ico_Print.png' width='20' height ='20' title='Print'/></input></td>");
                tr.append("<td style='text-align: center'><input type='image' class='gvEdit'   id='btndispatchedit'   <img src='../Images/Pencil-icon.png' title='Edit'/></input></td>");
                tr.append("<td style='text-align: center'><input type='image' class='gvView'   id='btndispatchview'   <img src='../Images/View.png' width='20' height ='20' title='View'/></input></td>");
                tr.append("<td style='text-align: center'><input type='image' class='gvCancel' id='btndispatchdelete' <img src='../Images/ico_delete_16.png' title='Cancel'/></input></td>");
                
                $("#dispatchGridFG").append(tr);
            }
            tr.append("</tbody>");
            RowCountDispatchList();
            $('#dispatchGridFG').DataTable({
                "sScrollX": '100%',
                "sScrollXInner": "110%",
                "scrollY": "238px",
                "scrollCollapse": true,
                "dom": 'Bfrtip',
                buttons: [
                    {
                        extend: 'excelHtml5',
                        title: 'FG Dispatch List'
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

function ReceivedStatus(dispatchid) {
    var returnValue = null;
    $.ajax({
        type: "POST",
        url: "/tranfac/GetFGDispatchStatus",
        data: { DispatchID: dispatchid, ModuleID: '10', Type: '1' },
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

function SyncStatus(dispatchid) {
    var returnValue = null;
    $.ajax({
        type: "POST",
        url: "/tranfac/GetFGDispatchStatus",
        data: { DispatchID: dispatchid, ModuleID: '10', Type: '2' },
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

function FinanceStatus(dispatchid) {
    var returnValue = null;
    $.ajax({
        type: "POST",
        url: "/tranfac/GetFGDispatchStatus",
        data: { DispatchID: dispatchid, ModuleID: '10', Type: '3' },
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

function GetTaxOnEdit(dispatchid, taxid, productid, batchno) {
    debugger;
    var returnValue = null;
    $.ajax({
        type: "POST",
        url: "/tranfac/GetTaxOnEdit",
        data: { DispatchID: dispatchid, TaxID: taxid, ProductID: productid, BatchNo: batchno },
        dataType: "json",
        async: false,
        success: function (response) {
            var taxpercentage = 0;
            $.each(response, function (key, item) {
                taxpercentage = item.TAXPERCENTAGE;
                returnValue = taxpercentage;
                return false;
            });
        }
    });
    return returnValue;
}

function EditDetails(dispatchid) {
    var tr;
    var HeaderCount = 0;
    var srl = 0;
    srl = srl + 1; 
    var taxid = '';
    var TaxPercentage = 0;
    var TaxAmount = 0;
    var NetAmount = 0;
    var PolicyNo = '';
    $("#productDetailsGrid").empty();
    $("#dialog").dialog({
        autoOpen: true,
        modal: true,
        title: "Loading.."
    });
    $("#imgLoader").css("visibility", "visible");
    $.ajax({
        type: "POST",
        url: "/tranfac/DipatchFGEdit",
        data: { DispatchID: dispatchid},
        dataType: "json",
        async: false,
        success: function (response) {
            //alert(JSON.stringify(response))
            var listHeader = response.alleditDataset.varHeader;
            var listDetails = response.alleditDataset.varDetails;
            var listTaxcount = response.alleditDataset.varTaxCount;
            var listTax = response.alleditDataset.varTax;
            var listFooter = response.alleditDataset.varFooter;
            
            /*Tax Count Info*/
            if (listTaxcount.length > 0) {
                $.each(listTaxcount, function (index, record) {
                    taxid = this['TAXID'].toString().trim();
                    $("#hdntaxcount").val('1');
                    $("#hdntaxname").val(this['NAME'].toString().trim());
                });
            }
            else {
                $("#hdntaxcount").val('0');
                $("#hdntaxname").val('NA');
                taxid = 'NA';
            }

            /*Header Info*/
            $.each(listHeader, function (index, record) {
                $("#TransferNo").val(this['STOCKTRANSFERNO'].toString().trim());
                $("#TODEPOTID").val(this['TODEPOTID'].toString().trim());
                $("#ID").val(this['INSURANCECOMPID'].toString().trim());
                $("#TransporterID").val(this['TRANSPORTERID'].toString().trim());
                $("#WAYBILLID").val(this['WAYBILLNO'].toString().trim());
                $("#TransportMode").val(this['MODEOFTRANSPORT'].toString().trim());
                $("#VehichleNo").val(this['VEHICHLENO'].toString().trim());
                $("#LrGrNo").val(this['LRGRNO'].toString().trim());
                $("#GatepassNo").val(this['GATEPASSNO'].toString().trim());
                $("#ShippingAddress").val(this['SHIPPINGADDRESS'].toString().trim());
                $("#LrGrDate").val(this['LRGRDATE'].toString().trim());
                $("#DispatchDate").val(this['STOCKTRANSFERDATE'].toString().trim());
                $("#GatepassDate").val(this['GATEPASSDATE'].toString().trim());
                $("#DeliveryDate").val(this['DELIVERYDATE'].toString().trim());
                $("#TotalCase").val(this['TOTALCASE'].toString().trim());
                $("#TotalPcs").val(parseInt(this['TOTALPCS'].toString().trim()));
                $("#Remarks").val(this['REMARKS'].toString().trim());
                PolicyNo = this['INSURANCE_NO'].toString().trim();
            });

            debugger;
            /*Details Info*/
            if (listDetails.length > 0) {

                $("#productDetailsGrid").empty();
                if ($("#hdntaxcount").val().trim() == '1') {
                    tr = $('#productDetailsGrid');
                    HeaderCount = $('#productDetailsGrid thead th').length;
                    if (HeaderCount == 0) {
                        tr.append("<thead><tr><th>Sl.No.</th><th style='display: none'>Product ID</th><th>HSN Code</th><th>Product</th><th>Batch</th><th style='display: none'>PackSize ID</th><th>Pack Size</th><th>Qty</th><th>MRP</th><th>Rate</th><th>Amount</th><th style='display: none'>Assesmet(%)</th><th style='display: none'>Assesment Amt.</th><th style='display: none'>Net Wght.</th><th>Gross Wght.</th><th>Mfg.Date</th><th>Exp.Date</th><th style='display: none'>Tag</th><th style='display: none'>Before Edited Qty</th><th>IGST(%)</th><th>IGST</th><th>Net Amt.</th><th>Delete</th></tr></thead><tbody>");
                    }
                    $.each(listDetails, function (index, record) {
                        debugger;
                        tr = $('<tr/>');
                        tr.append("<td style='text-align: center'>" + srl + "</td>");//0
                        tr.append("<td style='display: none'>" + this['PRODUCTID'].toString().trim() + "</td>");//1
                        tr.append("<td style='text-align: center'>" + this['HSNCODE'].toString().trim() + "</td>");//2
                        tr.append("<td style='width:250px'>" + this['PRODUCTNAME'].toString().trim() + "</td>");//3
                        tr.append("<td style='text-align: center'>" + this['BATCHNO'].toString().trim() + "</td>");//4
                        tr.append("<td style='display: none'>" + this['PACKINGSIZEID'].toString().trim() + "</td>");//5
                        tr.append("<td>" + this['PACKINGSIZENAME'].toString().trim() + "</td>");//6
                        if (this['PACKINGSIZEID'].toString().trim() == '1970C78A-D062-4FE9-85C2-3E12490463AF') {
                            tr.append("<td style='text-align: right'>" + parseFloat(this['TRANSFERQTY'].toString().trim()).toFixed(3) + "</td>");//7
                        }
                        else if (this['PACKINGSIZEID'].toString().trim() == 'B9F29D12-DE94-40F1-A668-C79BF1BF4425') {
                            tr.append("<td style='text-align: right'>" + parseFloat(this['TRANSFERQTY'].toString().trim()).toFixed(0) + "</td>");//7
                        }
                        else {
                            tr.append("<td style='text-align: right'>" + parseFloat(this['TRANSFERQTY'].toString().trim()).toFixed(3) + "</td>");//7
                        }
                        tr.append("<td style='text-align: right'>" + this['MRP'].toString().trim() + "</td>");//8
                        tr.append("<td style='text-align: right'>" + parseFloat(this['RATE'].toString().trim()).toFixed(2) + "</td>");//9
                        tr.append("<td style='text-align: right'>" + parseFloat(this['AMOUNT'].toString().trim()).toFixed(2) + "</td>");//10
                        tr.append("<td style='display: none'>" + this['ASSESMENTPERCENTAGE'].toString().trim() + "</td>");//11
                        tr.append("<td style='display: none'>" + parseFloat(this['TOTALASSESMENTVALUE'].toString().trim()).toFixed(2) + "</td>");//12
                        tr.append("<td style='display: none'>" + this['NETWEIGHT'].toString().trim() + "</td>");//13
                        tr.append("<td>" + this['GROSSWEIGHT'].toString().trim() + "</td>");//14
                        tr.append("<td style='text-align: center'>" + this['MFDATE'].toString().trim() + "</td>");//15
                        tr.append("<td style='text-align: center'>" + this['EXPRDATE'].toString().trim() + "</td>");//16
                        tr.append("<td style='display: none'>" + this['TAG'].toString().trim() + "</td>");//17
                        tr.append("<td style='display: none'>" + this['BEFOREEDITEDQTY'].toString().trim() + "</td>");//18
                        TaxPercentage = GetTaxOnEdit(dispatchid.trim(), taxid, this['PRODUCTID'].toString().trim(), this['BATCHNO'].toString().trim());
                        TaxAmount = ((parseFloat(this['AMOUNT'].toString().trim()) * TaxPercentage) / 100);
                        NetAmount = (parseFloat(this['AMOUNT'].toString().trim()) + TaxAmount);
                        tr.append("<td style='text-align: right'>" + TaxPercentage.toFixed(2) + "</td>");//19
                        tr.append("<td style='text-align: right'>" + TaxAmount.toFixed(2) + "</td>");//20
                        tr.append("<td style='text-align: right'>" + NetAmount.toFixed(2) + "</td>");//21
                        tr.append("<td style='text-align: center'><input type='image' class='gvTempDelete'  id='btndelete' <img src='../Images/ico_delete_16.png'/></input></td>");
                        $("#productDetailsGrid").append(tr);
                        tr.append("</tbody>");
                        RowCountEdit();
                        CalculateAmount();
                    });
                 
                }
                else if ($("#hdntaxcount").val().trim() == '0') {
                    tr = $('#productDetailsGrid');
                    HeaderCount = $('#productDetailsGrid thead th').length;
                    if (HeaderCount == 0) {
                        tr.append("<thead><tr><th>Sl.No.</th><th style='display: none'>Product ID</th><th>HSN Code</th><th>Product</th><th>Batch</th><th style='display: none'>PackSize ID</th><th>Pack Size</th><th>Qty</th><th>MRP</th><th>Rate</th><th>Amount</th><th style='display: none'>Assesmet(%)</th><th style='display: none'>Assesment Amt.</th><th style='display: none'>Net Wght.</th><th>Gross Wght.</th><th>Mfg.Date</th><th>Exp.Date</th><th style='display: none'>Tag</th><th style='display: none'>Before Edited Qty</th><th>Net Amt.</th><th>Delete</th></tr></thead><tbody>");
                    }
                    $.each(listDetails, function (index, record) {
                        tr = $('<tr/>');
                        tr.append("<td style='text-align: center'>" + srl + "</td>");//0
                        tr.append("<td style='display: none'>" + this['PRODUCTID'].toString().trim() + "</td>");//1
                        tr.append("<td style='text-align: center'>" + this['HSNCODE'].toString().trim() + "</td>");//2
                        tr.append("<td style='width:250px'>" + this['PRODUCTNAME'].toString().trim() + "</td>");//3
                        tr.append("<td style='text-align: center'>" + this['BATCHNO'].toString().trim() + "</td>");//4
                        tr.append("<td style='display: none'>" + this['PACKINGSIZEID'].toString().trim() + "</td>");//5
                        tr.append("<td>" + this['PACKINGSIZENAME'].toString().trim() + "</td>");//6
                        if (this['PACKINGSIZEID'].toString().trim() == '1970C78A-D062-4FE9-85C2-3E12490463AF') {
                            tr.append("<td style='text-align: right'>" + parseFloat(this['TRANSFERQTY'].toString().trim()).toFixed(3) + "</td>");//7
                        }
                        else if (this['PACKINGSIZEID'].toString().trim() == 'B9F29D12-DE94-40F1-A668-C79BF1BF4425') {
                            tr.append("<td style='text-align: right'>" + parseFloat(this['TRANSFERQTY'].toString().trim()).toFixed(0) + "</td>");//7
                        }
                        else {
                            tr.append("<td style='text-align: right'>" + parseFloat(this['TRANSFERQTY'].toString().trim()).toFixed(3) + "</td>");//7
                        }
                        tr.append("<td style='text-align: right'>" + this['MRP'].toString().trim() + "</td>");//8
                        tr.append("<td style='text-align: right'>" + parseFloat(this['RATE'].toString().trim()).toFixed(2) + "</td>");//9
                        tr.append("<td style='text-align: right'>" + parseFloat(this['AMOUNT'].toString().trim()).toFixed(2) + "</td>");//10
                        tr.append("<td style='display: none'>" + this['ASSESMENTPERCENTAGE'].toString().trim() + "</td>");//11
                        tr.append("<td style='display: none'>" + parseFloat(this['TOTALASSESMENTVALUE'].toString().trim()).toFixed(2) + "</td>");//12
                        tr.append("<td style='display: none'>" + this['NETWEIGHT'].toString().trim() + "</td>");//13
                        tr.append("<td>" + this['GROSSWEIGHT'].toString().trim() + "</td>");//14
                        tr.append("<td style='text-align: center'>" + this['MFDATE'].toString().trim() + "</td>");//15
                        tr.append("<td style='text-align: center'>" + this['EXPRDATE'].toString().trim() + "</td>");//16
                        tr.append("<td style='display: none'>" + this['TAG'].toString().trim() + "</td>");//17
                        tr.append("<td style='display: none'>" + this['BEFOREEDITEDQTY'].toString().trim() + "</td>");//18
                        NetAmount = parseFloat(this['AMOUNT'].toString().trim());
                        tr.append("<td style='text-align: right'>" + NetAmount.toFixed(2) + "</td>");//19
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
                    addRowinTaxTableEdit(this['PRODUCTID'].toString().trim(), this['BATCHNO'].toString().trim(), this['TAXID'].toString().trim(), this['PERCENTAGE'].toString().trim(), this['TAXVALUE'].toString().trim(), this['MRP'].toString().trim(), '1');
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
                    $('#NetAmt').val(parseFloat(this['TOTALDESPATCHVALUE'].toString().trim()).toFixed(2));
                });
            }

            debugger;
            bindPolicyNo($('#ID').val().trim(), PolicyNo.trim(), dispatchid);
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

function DeleteDispatch(dispatchid) {
    debugger;
   
    $.ajax({
        type: "POST",
        url: "/tranfac/fgdeleteDispatch",
        data: { DispatchID: dispatchid},
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
                bindfgdispatchgrid();
                toastr.success('<b><font color=black>' + messagetext + '</font></b>');
            }
            else {
                toastr.error('<b><font color=black>' + messagetext + '</font></b>');
            }
        }
    });
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
        CalculateQuantity();
        var rowCount = document.getElementById("productDetailsGrid").rows.length - 1;
        if ($('#hdndispatchID').val() == '0') {
            if (rowCount > 0) {
                $("#TODEPOTID").prop("disabled", true);
                $("#TODEPOTID").chosen({
                    search_contains: true
                });
                $("#TODEPOTID").trigger("chosen:updated");
            }
            else {
                $('#productDetailsGrid').empty();
                $("#TODEPOTID").prop("disabled", false);
                $("#TODEPOTID").chosen({
                    search_contains: true
                });
                $("#TODEPOTID").trigger("chosen:updated");
            }
        }
    })

})

/*Edit function for Dispatch*/
$(function () {
    var dispatchid;
    $("body").on("click", "#dispatchGridFG .gvEdit", function () {
        var row = $(this).closest("tr");
        dispatchid = row.find('td:eq(1)').html();
        $('#hdndispatchID').val(dispatchid);
        var receivedstatus = ReceivedStatus($('#hdndispatchID').val().trim());
        var syncstatus = SyncStatus($('#hdndispatchID').val().trim());
        var financestatus = FinanceStatus($('#hdndispatchID').val().trim());

        if (receivedstatus != 'na') {
            $('#dvAdd').css("display", "none");
            $('#dvDisplay').css("display", "");
            toastr.info('<b>' + receivedstatus + '</b>');
            return false;
        }
        else if (syncstatus != 'na') {
            $('#dvAdd').css("display", "none");
            $('#dvDisplay').css("display", "");
            toastr.info('<b>' + syncstatus + '</b>');
            return false;
        }
        else if (financestatus != 'na') {
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
            $("#TransferNo").attr("disabled", "disabled");
            $("#GatepassDate").attr("disabled", "disabled");
            $("#DispatchDate").attr("disabled", "disabled");
            $("#LrGrDate").attr("disabled", "disabled");
            $("#DeliveryDate").attr("disabled", "disabled");
            $("#MRP").attr("disabled", "disabled");
            $("#StockQty").attr("disabled", "disabled");
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
            
            $("#TODEPOTID").prop("disabled", true);
            $("#TODEPOTID").chosen({
                search_contains: true
            });
            $("#TODEPOTID").trigger("chosen:updated");
            
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

            $("#DispatchDate").datepicker("destroy");
        }
    })

})

/*View function for Dispatch*/
$(function () {
    var dispatchid;
    $("body").on("click", "#dispatchGridFG .gvView", function () {
        var row = $(this).closest("tr");
        dispatchid = row.find('td:eq(1)').html();
        $('#hdndispatchID').val(dispatchid);
       
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
        $("#btnclose").prop("disabled",false);
        $('#dvAdd').css("display", "");
        $('#dvDisplay').css("display", "none");
        $("#BRID").chosen({
            search_contains: true
        });
        $("#BRID").trigger("chosen:updated");


        $("#TODEPOTID").prop("disabled", true);
        $("#TODEPOTID").chosen({
            search_contains: true
        });
        $("#TODEPOTID").trigger("chosen:updated");

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

        $("#DispatchDate").datepicker("destroy");

        
    })

})

/*Delete function for Dispatch*/
$(function () {
    var dispatchid;
    $("body").on("click", "#dispatchGridFG .gvCancel", function () {
        var row = $(this).closest("tr");
        dispatchid = row.find('td:eq(1)').html();
        $('#hdndispatchID').val(dispatchid);
        if (confirm("Do you want to delete this item?")) {
            if (CHECKER == 'FALSE') {
                var receivedstatus = ReceivedStatus($('#hdndispatchID').val().trim());
                var syncstatus = SyncStatus($('#hdndispatchID').val().trim());
                var financestatus = FinanceStatus($('#hdndispatchID').val().trim());
                if (receivedstatus != 'na') {
                    toastr.warning('<b>Stock Received already done,not allow to cancel...!</b>');
                    return false;
                }
                else if (syncstatus != 'na') {
                    toastr.warning('<b>Synchronization already done,not allow to cancel...!</b>');
                    return false;
                }
                else if (financestatus != 'na') {
                    toastr.warning('<b>Finance approve already done,not allow to cancel...!</b>');
                    return false;
                }
                else {
                    DeleteDispatch(dispatchid);
                }
            }
            else {
                toastr.warning('<b>not allow to cancel...!</b>');
            }
        }

    })

})

/*Print function for Dispatch*/
$(function () {
    var dispatchid;
    var menuid = $("#hdnmenuid").val();
    $("body").on("click", "#dispatchGridFG .gvPrint", function () {
        var row = $(this).closest("tr");
        dispatchid = row.find('td:eq(1)').html();
        $('#hdndispatchID').val(dispatchid);
        var url = "http://mcnroeerp.com/factory/FACTORY/frmPrintPopUpV2_FAC.aspx?Stnid=" + dispatchid + "&BSID=1&pid=1&MenuId=" + menuid;
        window.open(url, "", "width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100");
        
    })

})

