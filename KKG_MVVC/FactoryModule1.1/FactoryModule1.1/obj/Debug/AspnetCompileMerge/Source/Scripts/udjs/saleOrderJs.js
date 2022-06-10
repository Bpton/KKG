/*Pagename:sale order
 Author:Pritam Basu
 Start Date:02.06.2020*/

var menuID;
var BSType;
var depotid;
var status ="N";
var MRPTag ="N";

$(document).ready(function () {
    var qs = getQueryStrings();
    if (qs["MENUID"] != undefined && qs["MENUID"] != "") {
        menuID = qs["MENUID"];
    }
    if (qs["BSID"] != undefined && qs["BSID"] != "") {
        BSType = qs["BSID"];
    }
    var currentdt;
    var frmdate;
    $('#pnlAdd').css("display", "none");
    $('#pnlDisplay').css("display", "");
   
   
    $.ajax({
        type: "POST",
        url: "/claims/finyrchk",
        data: '{}',
        async: false,
        dataType: "json",
        success: function (response) {
            //alert(JSON.stringify(response));
            //.append('<option selected="selected" value="0">Select Brancht</option>');;
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
    $("#txtfromdate").datepicker({
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
    $("#txtsaleorderdate").datepicker({
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
    $("#txtrefsaleorderdate").datepicker({
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        selectCurrent: true,
        todayBtn: "linked",
        showAnim: "slideDown",
        yearRange: "-3:+0",
        //maxDate: new Date(currentdt),
        //minDate: new Date(frmdate),
        dateFormat: "dd/mm/yy",
        showOn: 'button',
        buttonText: 'Show Date',
        buttonImageOnly: true,
        buttonImage: 'http://jqueryui.com/resources/demos/datepicker/images/calendar.gif'
    });
    $(".ui-datepicker-trigger").mouseover(function () {
        $(this).css('cursor', 'pointer');
    });
    $("#txtICDSDate").datepicker({
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        selectCurrent: true,
        todayBtn: "linked",
        showAnim: "slideDown",
        yearRange: "-3:+0",
        //maxDate: new Date(currentdt),
        //minDate: new Date(frmdate),
        dateFormat: "dd/mm/yy",
        showOn: 'button',
        buttonText: 'Show Date',
        buttonImageOnly: true,
        buttonImage: 'http://jqueryui.com/resources/demos/datepicker/images/calendar.gif'
    });
    $(".ui-datepicker-trigger").mouseover(function () {
        $(this).css('cursor', 'pointer');
    });
    $("#txttorequireddate").datepicker({
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        selectCurrent: true,
        todayBtn: "linked",
        showAnim: "slideDown",
        yearRange: "-3:+0",
        //maxDate: new Date(currentdt),
        //minDate: new Date(frmdate),
        dateFormat: "dd/mm/yy",
        showOn: 'button',
        buttonText: 'Show Date',
        buttonImageOnly: true,
        buttonImage: 'http://jqueryui.com/resources/demos/datepicker/images/calendar.gif'
    });
    $(".ui-datepicker-trigger").mouseover(function () {
        $(this).css('cursor', 'pointer');
    });
    $("#txtrequireddate").datepicker({
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        selectCurrent: true,
        todayBtn: "linked",
        showAnim: "slideDown",
        yearRange: "-3:+0",
        //maxDate: new Date(currentdt),
        //minDate: new Date(frmdate),
        dateFormat: "dd/mm/yy",
        showOn: 'button',
        buttonText: 'Show Date',
        buttonImageOnly: true,
        buttonImage: 'http://jqueryui.com/resources/demos/datepicker/images/calendar.gif'
    });
    $(".ui-datepicker-trigger").mouseover(function () {
        $(this).css('cursor', 'pointer');
    });
    $("#txtsaleorderdate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
    $("#txttorequireddate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
    $("#txtrequireddate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
    $("#txtfromdate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
    $("#txttodate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());

    /*for chosen class*/
    var currencey = $("#ddlCurrency");
    currencey.chosen();
    currencey.trigger("chosen:updated");
    /*end*/
   
    $('#tr_saleprderno').css("display", "none");
    FetchExportBS();
    LoadBUSINESSSEGMENT(BSType);
    LoadDeleveryTerms();
    LoadSaleOrder();
    LoadPrincipleGroup(BSType);
    $("#hdn_SumTotalAmount").val(0);
    GETTPU();
    GETDEPOT();

    

    var TPUID = document.getElementById("hdn_Tpu").value;
    if (TPUID == "DI" || TPUID == "SUDI" || TPUID == "ST" || TPUID == "SST") {
        $.ajax({
            type: "post",
            url: "/saleOrder/getGroupFromSession",
            data: {},
            datatype: "json",
            async: false,
            traditional: true,
            success: function (GROUPID) {
                $.each(GROUPID, function (key, item) {
                    group_id.val(GROUPID[0].GROUPID);
                });
                var group = $("#ddlgroup");
                group.val(this['group_id']);
                group.prop("disabled", true);
            },
        });
        $.ajax({
            type: "post",
            url: "/saleOrder/getCustomerFromSession",
            data: {},
            datatype: "json",
            async: false,
            traditional: true,
            success: function (CUSTOMERID) {
                cus_id = CUSTOMERID;
                var customer = $("#ddlcustomer");
                customer.empty();
                customer.val(this['cus_id']);
                customer.prop("disabled", true);
            },
        });
    }
    else {
        var group = $("#ddlgroup");
        var customer = $("#ddlcustomer");
        group.prop("disabled", false);
        customer.prop("disabled", false);
    }

    $("#hdn_Mode").val = null;
    LoadSale();

    /*change function start*/
    $('#ddlgroup').change(function () {
        var group = $("#ddlgroup").val();
        $("#dialog").dialog({
            autoOpen: true,
            modal: true,
            title: "Loading.."
        });
        $("#imgLoader").css("visibility", "visible");


        setTimeout(function () {
            debugger;
            LoadCurrency(group);
            LoadCustomer();
            loadProduct(BSType, group);
            bindPackSize();
            $("#imgLoader").css("visibility", "hidden");
            $("#dialog").dialog("close");
        }, 5);
       
        
    })

    $('#ddlPaymentterms').change(function () {
        var paymentterms = $('#ddlPaymentterms').val();
        if (paymentterms == 1) {
            $('#lblusanceperoid').css("display", "");
        }
        else {
            $('#lblusanceperoid').css("display", "none");
        }
    })

    $('#ddlProductName').change(function () {
        $("#dialog").dialog({
            autoOpen: true,
            modal: true,
            title: "Loading.."
        });
        $("#imgLoader").css("visibility", "visible");
     
        setTimeout(function () {
        var productId = $("#ddlProductName").val();
        var depotid = $("#hdn_sessiondepot").val();
        var customerId = $("#ddlcustomer").val();
        var groupId = $("#ddlgroup").val();
        var YMD = Conver_To_YMD($("#txtsaleorderdate").val());
        var mrp = 0;
        if (productId == "0") {
            var packSize = $("#ddlpackingsize");
            packSize.empty().append('<option selected="selected" value="0">Select Pack Size</option>');
        }
        else {
            bindPackSize(productId);
            var packSizeId = $("#ddlpackingsize").val();
            LoadStockQuantity(productId, packSizeId, depotid, "0");
            GetBaseCostPrice(customerId, productId, YMD, mrp, depotid, menuID, BSType, groupId);
        }
        $("#txtqty").focus();
        }, 5);
        
    })

    $('#ddlpackingsize').change(function () {
        $("#dialog").dialog({
            autoOpen: true,
            modal: true,
            title: "Loading.."
        });
        setTimeout(function () {
        $("#imgLoader").css("visibility", "visible");
        var productId = $("#ddlProductName").val();
        var depotid = $("#hdn_sessiondepot").val();
        var packSizeId = $("#ddlpackingsize").val();
        LoadStockQuantity(productId, packSizeId, depotid, "0");
        }, 5);
       
    })

    $('#btnAdd').click(function () {

        $("#dialog").dialog({
            autoOpen: true,
            modal: true,
            title: "Loading.."
        });
        $("#imgLoader").css("visibility", "visible");


        if ($('#txtqty').val() == 0){
            toastr.warning("Qty Cannot be zero");
            return;
        }
        if ($('#txtrate').val() == "") {
            $('#txtrate').val(0);
        }
        setTimeout(function () {
            bindGrid();
            $("#imgLoader").css("visibility", "hidden");
            $("#dialog").dialog("close");
        }, 5);
       
        
    })

    $('#btnAddNew').click(function () {
        debugger;
        $("#dialog").dialog({
            autoOpen: true,
            modal: true,
            title: "Loading.."
        });
        $("#imgLoader").css("visibility", "visible");
       
        
        setTimeout(function () {
            $('#pnlAdd').css("display", "");
            $('#pnlDisplay').css("display", "none");
            addNewRecord();
            $("#imgLoader").css("visibility", "hidden");
            $("#dialog").dialog("close");
        }, 5);
        

    })
    
    /*check box selection value*/
    $('#chkactive').click(function () {
        debugger;
        if ($(this).is(":checked")) {
            status = "Y";
        }
        else if ($(this).is(":not(:checked)")) {
            status = "N";
        }
    });

    $('#chkMRPTag').click(function () {
        debugger;
        if ($(this).is(":checked")) {
            MRPTag = "Y";
        }
        else if ($(this).is(":not(:checked)")) {
            MRPTag = "N";
        }
    });
    /*check box selection value*/

    $('#btnSave').click(function () {
        $("#dialog").dialog({
            autoOpen: true,
            modal: true,
            title: "Loading.."
        });
        $("#imgLoader").css("visibility", "visible");

        if ($("#ddlcustomer").val() == "0") {
            toastr.warning("Please Select Customer");
            return;
        }

        if ($("#ddlPaymentterms").val() == "1" && $("#ddlUsanceperiod").val() == "0") {
            toastr.warning('Please selete Usance Period');
            return;
        }
       
        if (BSType == "2E96A0A4-6256-472C-BE4F-C59599C948B0" || BSType == "A0A1E83E-1993-4FB9-AF53-9DD595D09596" || BSType == "C5038911-9331-40CF-B7F9-583D50583592"
            || BSType == "97547CB6-F40B-4B43-923D-B63F61A910C2" || BSType == "8AE0E8C9-F4F7-4382-B8AB-870A64ABF996" || BSType == "62240D9B-F7FC-4259-AECE-B15D1018A972"
            || BSType == "C8E2844A-B43F-4028-856B-1D8827CB9B8B") {
            if ($("#txtrefsaleorderno").val() == "") {
                toastr.warning("Please enter Reference Purchase Order No");
                return;
            }
            if ($("#txtrefsaleorderdate").val() == "") {
                toastr.warning("Please enter Reference Purchase Order Date");
                return;
            }
        }
         
       
        if ((jQuery("tr", "#gvSaleOrder").length) - 1 <= 0) {
            toastr.warning("Please Select Atleast One Record");
            return;
        }
        var RowCount = (jQuery("tr", "#gvSaleOrder").length) - 1;
        var _REASON='0';
        var _REASONID='0';
        debugger;
            for (var i = 0; i < RowCount; i++) {
                var lblPRODUCT = $("input[id*=lblPRODUCT]");
                var lblPRODUCTNAME = $("input[id*=lblPRODUCTNAME]");
                var txtORDERQTY = $("input[id*=txtORDERQTY]");
                var txtAMDORDERQTY = $("input[id*=txtAMDORDERQTY]");
                var lblPRODUCTPACKINGSIZEID = $("input[id*=lblPRODUCTPACKINGSIZEID]");
                var lblPRODUCTPACKINGSIZE = $("input[id*=lblPRODUCTPACKINGSIZE]");
                var lblREQUIREDDATE = $("input[id*=lblREQUIREDDATE]");
                var lblREQUIREDTODATE = $("input[id*=lblREQUIREDTODATE]");
                var lblRATE = $("input[id*=lblRATE]");
                var lblDISCOUNT = $("input[id*=lblDISCOUNT]");
                var lblDESPATCHQTY = $("input[id*=lblDESPATCHQTY]");
               
                var AMOUNT = $("input[id*=lblDISCOUNTAMOUNT]");
                var fromdate = $("input[id*=lblREQUIREDDATE]");
                var todate = $("input[id*=lblREQUIREDTODATE]");
                var delivery = $("input[id*=txtAMDORDERQTY]");

               

                var _PRODUCT = lblPRODUCT[i].value;
                var _PRODUCTNAME = lblPRODUCTNAME[i].value;
                var _ORDERQTY = txtORDERQTY[i].value;
                var _AMDORDERQTY = txtAMDORDERQTY[i].value;
                var _PRODUCTPACKINGSIZEID = lblPRODUCTPACKINGSIZEID[i].value;
                var _PRODUCTPACKINGSIZE = lblPRODUCTPACKINGSIZE[i].value;
                var _REQUIREDDATE = lblREQUIREDDATE[i].value;
                var _REQUIREDTODATE = lblREQUIREDTODATE[i].value;
                var _RATE = lblRATE[i].value;
                var _DISCOUNT = lblDISCOUNT[i].value;
                var _DESPATCHQTY = lblDESPATCHQTY[i].value;
                var _AMOUNT = AMOUNT[i].value;
                var _fromdate = fromdate[i].value;
                var _todate = todate[i].value;
                var _delivery = delivery[i].value;

                $('#gvSaleOrder tbody tr').each(function () {
                    debugger;
                    _REASONID = $(this).find('#ddlReason').val();
                    _REASON = $(this).find('#ddlReason option:selected').text();

                    if (_REASONID == "" || _REASONID == null) {
                        _REASONID=('---Please select---,0');
                    }
                })

                if (_REASONID == "0" && $('#ddlReason').is(':disabled') ) {
                    toastr.warning("Please Select Reason For <b><font color=red>" + _PRODUCTNAME + "</font></b>");
                    return;
                }

                var i = 0;
                saleOrderlist = new Array();
                $('#gvSaleOrder tbody tr').each(function () {
                var saleOrder = {};
                    saleOrder._PRODUCT = _PRODUCT;
                    saleOrder._PRODUCTNAME = _PRODUCTNAME;
                    saleOrder._ORDERQTY = _ORDERQTY;
                    saleOrder._AMDORDERQTY = _AMDORDERQTY;
                    saleOrder._PRODUCTPACKINGSIZEID = _PRODUCTPACKINGSIZEID;
                    saleOrder._PRODUCTPACKINGSIZE = _PRODUCTPACKINGSIZE;
                    saleOrder._REQUIREDDATE = _REQUIREDDATE;
                    saleOrder._REQUIREDTODATE = _REQUIREDTODATE;
                    saleOrder._RATE = _RATE;
                    saleOrder._DISCOUNT = _DISCOUNT;
                    saleOrder._DESPATCHQTY = _DESPATCHQTY;
                    saleOrder._AMOUNT = _AMOUNT;
                    saleOrder._fromdate = _fromdate;
                    saleOrder._todate = _todate;
                    saleOrder._REASONID = _REASONID;
                    saleOrder._REASON = _REASON;
                    saleOrder._delivery = _delivery;
                    saleOrderlist[i++] = saleOrder;
                });
               
             }
            if (saleOrderlist.length > 0) {
              
                if ($("#hdn_ExportBS").val() == BSType) {

                    if ($("#ddlCurrency").val() == "0") {
                        toastr.warning("Please select Currency");
                        return;
                    }
                }

                if ($("#hdn_saleorderid").val() != "") {
                    if (BSType == "2E96A0A4-6256-472C-BE4F-C59599C948B0") /*Export*/ {
                        if (RowCount != null) {
                            CalculateTotalAmount();
                            $.ajax({
                                type: "post",
                                url: "/saleOrder/GetProformastatus",
                                data: { saleOrderId },
                                datatype: "json",
                                async: false,
                                traditional: true,
                                success: function (Proformastatus) {
                                    if (Proformastatus.length > 0) {
                                        var ProformaID;
                                        $.ajax({
                                            type: "post",
                                            url: "/saleOrder/BindProformaInvoice",
                                            data: { saleOrderId },
                                            datatype: "json",
                                            async: false,
                                            traditional: true,
                                            success: function (ProformaInvoice) {
                                                if (ProformaInvoice.length > 0) {
                                                    $.each(ProformaInvoice, function (key, item) {
                                                        ProformaID.val(ProformaInvoice[0].PROFORMAINVOICEID);
                                                    });
                                                    ProformaAmount(ProformaID);
                                                }
                                            }
                                        });
                                    }
                                }
                            });
                        }
                    }
                }
                
                setTimeout(function () {
                    LoadTermsConditions();
                    $("#imgLoader").css("visibility", "hidden");
                    $("#dialog").dialog("close");
                }, 5);
                InsertSaleOrderDetails();
            }
    })

    $("body").on("click", "#gvSaleOrder .gvdel", function () {

        if (confirm("Do you want to delete this Product?")) {
            var row = $(this).closest("tr");
            for (var i = 0; i < 1; i++) {
                var lblPRODUCTNAME = $("input[id*=lblPRODUCTNAME]");
                var _PRODUCTNAME = lblPRODUCTNAME[i].value;
            }
            var _PRODUCT = row.find('td:eq(0)').html().trim();
            var lrate = $(this).closest("tr").find('#lblRATE').val();
            rate = parseFloat(lrate).toFixed(2);
            delgrid(_PRODUCT, rate, _PRODUCTNAME);
        }

    });

    $("#btngvfill").click(function () {
        LoadSale();
    });

     /*for edit*/
    $("body").on("click", "#gvsaleorderdetails .gvEdit", function () {

        $("#dialog").dialog({
            autoOpen: true,
            modal: true,
            title: "Loading.."
        });
        $("#imgLoader").css("visibility", "visible");
        row = $(this).closest("tr");
        saleOrderId = row.find('td:eq(0)').html();
        saleOrderNo = row.find('td:eq(3)').html();
        $("#hdn_saleorderid").val(saleOrderId);
        $("#hdn_saleorderno").val(saleOrderNo);
        $("#txtsaleorderno").val(saleOrderNo);
        setTimeout(function () {
            $("#hdn_Mode").val('Edit');
            $('#pnlAdd').css("display", "");
            $('#pnlDisplay').css("display", "none");
            $('#tr_saleprderno').css("display", "");
            $("#btnSave").show();
            $("#tdscheduledate").show();
        $("#ddlgroup").chosen('destroy');
        $("#ddlgroup").chosen({ width: '150px' });

        $("#ddlcustomer").chosen('destroy');
        $("#ddlcustomer").chosen({ width: '150px' });

        $("#ddlCurrency").chosen('destroy');
        $("#ddlCurrency").chosen({ width: '150px' });

        $("#ddldeliveryterms").chosen('destroy');
        $("#ddldeliveryterms").chosen({ width: '150px' });

        $("#ddlProductName").chosen('destroy');
        $("#ddlProductName").chosen({ width: '230px' });

        $("#ddlpackingsize").chosen('destroy');
        $("#ddlpackingsize").chosen({ width: '150px' });
        $('#tdscheduledate').css("display", "");
    
       
        //Checking BS is EXPORT or OTHERS
        $.ajax({
            type: "post",
            url: "/saleOrder/Getstatus",
            data: { saleOrderId },
            datatype: "json",
            async: false,
            traditional: true,
            success: function (statusCheck) {
                if (statusCheck.length > 0) {
                    toastr.warning("Day End Operation already done,not allow to edit.", 50, 450);
                    return;
                }
                else {
                    $.ajax({
                        type: "post",
                        url: "/saleOrder/GetProformastatus",
                        data: { saleOrderId },
                        datatype: "json",
                        async: false,
                        traditional: true,
                        success: function (Proformastatus) {
                            if (Proformastatus.length > 0) {
                                var APPROVEDTAG;
                                $.ajax({
                                    type: "post",
                                    url: "/saleOrder/BindProformaInvoice",
                                    data: { saleOrderId },
                                    datatype: "json",
                                    async: false,
                                    traditional: true,
                                    success: function (ProformaInvoice) {
                                        if (ProformaInvoice.length > 0) {
                                            $.each(ProformaInvoice, function (key, item) {
                                                APPROVEDTAG.val(ProformaInvoice[0].APPROVEDTAG);
                                            });
                                            if (APPROVEDTAG == "Y") {
                                                toastr.warning("Proforma Invoice done and approved,not allow to edit", 50, 550);
                                                return;
                                            }

                                        }
                                    }
                                });
                            }
                        }
                    });
                }

            },
        });

        //Checking BS is EXPORT or OTHERS
        if ($("#hdn_ExportBS").val() == BSType) {
            $('#trCurrency').css("display", "");
        }
        else {
            $('#trCurrency').css("display", "none");
        }
         // ==== CSD BS Checking
        if (BSType == "C5038911-9331-40CF-B7F9-583D50583592") {
            $('#trICDS').css("display", "");
        }
        else {
            $('#trICDS').css("display", "none");
        }

        LoadPrincipleGroup(BSType);
        LoadTermsConditions();
        LoadSaleOrder();
        FetchSaleOrderDetails();

        $("#imgLoader").css("visibility", "hidden");
        $("#dialog").dialog("close");
        }, 5);
    });

    $("#btnClose").click(function (e) {
        $("#dialog").dialog({
            autoOpen: true,
            modal: true,
            title: "Loading.."
        });
        $("#imgLoader").css("visibility", "visible");


        setTimeout(function () {
            close();
            $("#imgLoader").css("visibility", "hidden");
            $("#dialog").dialog("close");
        }, 5);
       
    });

    $("#btnScheduledate").click(function (e) {
        updateScheduleDate();
    })

    $("body").on("change", "#gvSaleOrder .gvReason", function () {
        var _REASONID = '0';
        var _REASON = '';
        var row = $(this).closest("tr");
        debugger;
        _REASONID = row.find("#ddlReason").val();
        _REASON = row.find('#ddlReason option:selected').text();
        var _PRODUCT = row.find('td:eq(0)').html().trim();
        upadateReason(_PRODUCT, _REASONID, _REASON);
        
       
    })

    $("body").on("change", "#gvSaleOrder .poqty", function () {
        debugger;
        var _DESPATCHQTY = 0;
        var _RATE = 0;
        var amount = 0;
        var row = $(this).closest("tr");
        var _PRODUCT = row.find('td:eq(0)').html().trim();
        var _DESPATCHQTY = row.find("#txtORDERQTY").val();
        var _RATE = row.find("#lblRATE").val();
        amount = (_DESPATCHQTY * _RATE);
        row.find("#lblDISCOUNTAMOUNT").val(amount.toFixed(2));
        updateQty(_PRODUCT, _RATE,_DESPATCHQTY, amount);
    });

    /*for view*/
    $("body").on("click", "#gvsaleorderdetails .gvView", function () {

        $("#dialog").dialog({
            autoOpen: true,
            modal: true,
            title: "Loading.."
        });
        $("#imgLoader").css("visibility", "visible");

        row = $(this).closest("tr");
        saleOrderId = row.find('td:eq(0)').html();
        saleOrderNo = row.find('td:eq(3)').html();
        $("#hdn_saleorderid").val(saleOrderId);
        $("#hdn_saleorderno").val(saleOrderNo);
        $("#txtsaleorderno").val(saleOrderNo);
        //$('#txtsaleorderno').css('background', 'lightblue');
        setTimeout(function () {

        $('#pnlAdd').css("display", "");
        $('#pnlDisplay').css("display", "none");
        $('#tr_saleprderno').css("display", "");
        $("#btnSave").hide();
        $("#tdscheduledate").show();
        $("#ddlgroup").chosen('destroy');
        $("#ddlgroup").chosen({ width: '150px' });

        $("#ddlcustomer").chosen('destroy');
        $("#ddlcustomer").chosen({ width: '150px' });

        $("#ddlCurrency").chosen('destroy');
        $("#ddlCurrency").chosen({ width: '150px' });

        $("#ddldeliveryterms").chosen('destroy');
        $("#ddldeliveryterms").chosen({ width: '150px' });

        $("#ddlProductName").chosen('destroy');
        $("#ddlProductName").chosen({ width: '230px' });

        $("#ddlpackingsize").chosen('destroy');
        $("#ddlpackingsize").chosen({ width: '150px' });

       
        //Checking BS is EXPORT or OTHERS
        $.ajax({
            type: "post",
            url: "/saleOrder/Getstatus",
            data: { saleOrderId },
            datatype: "json",
            async: false,
            traditional: true,
            success: function (statusCheck) {
                if (statusCheck.length > 0) {
                    toastr.warning("Day End Operation already done,not allow to edit.", 50, 450);
                    return;
                }
                else {
                    $.ajax({
                        type: "post",
                        url: "/saleOrder/GetProformastatus",
                        data: { saleOrderId },
                        datatype: "json",
                        async: false,
                        traditional: true,
                        success: function (Proformastatus) {
                            if (Proformastatus.length > 0) {
                                var APPROVEDTAG;
                                $.ajax({
                                    type: "post",
                                    url: "/saleOrder/BindProformaInvoice",
                                    data: { saleOrderId },
                                    datatype: "json",
                                    async: false,
                                    traditional: true,
                                    success: function (ProformaInvoice) {
                                        if (ProformaInvoice.length > 0) {
                                            $.each(ProformaInvoice, function (key, item) {
                                                APPROVEDTAG.val(ProformaInvoice[0].APPROVEDTAG);
                                            });
                                            if (APPROVEDTAG == "Y") {
                                                toastr.warning("Proforma Invoice done and approved,not allow to edit", 50, 550);
                                                return;
                                            }

                                        }
                                    }
                                });
                            }
                        }
                    });
                }
               
            },
        });

        //Checking BS is EXPORT or OTHERS
        if ($("#hdn_ExportBS").val() == BSType) {
            $('#trCurrency').css("display", "");
        }
        else {
            $('#trCurrency').css("display", "none");
        }
        // ==== CSD BS Checking
        if (BSType == "C5038911-9331-40CF-B7F9-583D50583592") {
            $('#trICDS').css("display", "");
        }
        else {
            $('#trICDS').css("display", "none");
        }
      
            LoadPrincipleGroup(BSType);
            LoadTermsConditions();
            LoadSaleOrder();
            FetchSaleOrderDetails();
            $("#imgLoader").css("visibility", "hidden");
            $("#dialog").dialog("close");
        }, 5);
    })

    /*for delete*/
    $("body").on("click", "#gvsaleorderdetails .gvdel", function () {
        debugger;
        var Flag = 0;
        if (confirm("Do you want to delete this file?")) {
            var row = $(this).closest("tr");
            var saleOrderId = row.find('td:eq(0)').html();
            var getStatus = GetDeletestatus(saleOrderId);
            if (getStatus == 1) {
                toastr.warning(" Invoice Already Done Against This Sale Order");
            }
            else {
                debugger;
                Flag = DeleteSaleOrderHeader(saleOrderId);
                if (Flag == 1) {
                    LoadSale();
                    toastr.success("Record deleted successfully!");
                }
                else {
                    toastr.error("Error On Deleting!");
                }
            }
        }
    })

    /*for delivary qty change*/
    $("body").on("change", "#gvSaleOrder .Amdqty", function () {
        debugger;
        var row = $(this).closest("tr");
        debugger;
        reason = row.find("#ddlReason");
        var _DESPATCHQTY = row.find("#txtAMDORDERQTY").val();
        var _PRODUCT = row.find('td:eq(0)').html().trim();
        var _RATE = row.find("#lblRATE").val();
        if (_DESPATCHQTY > 0) {
            toastr.info("Please Select Reason");
            $.ajax({
                type: "POST",
                url: "/saleOrder/bindReason",
                data: { menuID },
                async: false,
                success: function (response) {
                var ddl = row.find("#ddlReason");
                ddl.empty().append('<option selected="selected" value="0">Select</option>');;
                $.each(response, function () {
                     ddl.val(this['REASON']);
                     ddl.append($("<option value=''></option>").val(this['REASONID']).html(this['REASON']));
                });
                },
                failure: function (response) {
                    alert(response.responseText);
                },
                error: function (response) {
                    alert(response.responseText);
                }
            });
            reason.prop('disabled', false);
        }
        else {
            bindReason();
            reason.prop('disabled', true);
        }
        updateAmdQty(_PRODUCT, _RATE, _DESPATCHQTY);
       
    });

    $('#menu ul li a').hover(function (ev) {
        $('#menu ul li').removeClass('selected');
        $(ev.currentTarget).parent('li').addClass('selected');
    });

});

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

function LoadBUSINESSSEGMENT(BSType) {
  
    $.ajax({
        type: "POST",
        url: "/saleOrder/BindBUSINESSSEGMENT",
        data: { bstype: BSType},
        async: false,
        dataType: "json",

        success: function (response) {
            if (response.length > 0) {
               
                $.each(response, function (key, item) {
                    $("#hdn_bsid").val(response[0].BSID);
                    $("#hdn_bsname").val(response[0].BSNAME);
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

function LoadDeleveryTerms() {
   


    $.ajax({
        type: "POST",
        url: "/saleOrder/LoadDeleveryTerms",
        data: { },
        async: false,
        dataType: "json",

        success: function (response) {
             // alert(JSON.stringify(response));
                  
            var deliveryterms = $("#ddldeliveryterms");
           
            if (BSType == "2E96A0A4-6256-472C-BE4F-C59599C948B0") {
                deliveryterms.empty().append('<option selected="selected" value="0">Please select</option>');
                $.each(response, function () {
                    deliveryterms.val(this['NAME']);
                    deliveryterms.append($("<option value=''></option>").val(this['ID']).html(this['NAME']));
                });
            }
            else {
                $.each(response, function () {
                    deliveryterms.val(this['NAME']);
                    deliveryterms.append($("<option value=''></option>").val(this['ID']).html(this['NAME']));
                    
                });
                deliveryterms.val('4');
                
            }
            deliveryterms.chosen();
            deliveryterms.trigger("chosen:updated");
            
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });

}

function LoadSaleOrder() {
 

    $("#hdn_SumTotalAmount").val(0);
    $.ajax({
        type: "POST",
        url: "/saleOrder/createSaleOrderRecordsDataTable",

        // data: "{'FromDate': '" + FromDate + "', 'ToDate': '" + ToDate + "','VoucherID': '" + VoucherID + "','DepotID': '" + DepotID + "','checker': '" + checker + "'}",
        data: {},

        dataType: "json",
        success: function (response) {
           
        }
    })
}

function LoadPrincipleGroup(BSType) {
   
    var saleorderid = $("#hdn_saleorderid").val();
  
    $.ajax({
        type: "POST",
        url: "/saleOrder/LoadPrincipleGroup",
        data: { bstype:BSType},
        async: false,
        success: function (result) {
           
            var group = $("#ddlgroup");
            group.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(result, function () {
                group.val(this['DIS_CATNAME']);
                group.append($("<option value=''></option>").val(this['DIS_CATID']).html(this['DIS_CATNAME']));
            });
            group.chosen();
            group.trigger("chosen:updated");
            LoadCustomer();
            
            if ($("#ddlgroup").val('0')  || $("#ddlgroup").val('') ) {
                
            }
            else {
                loadProduct(BSType, group, saleorderid);
                bindPackSize();
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

function GETTPU() {
   
    $.ajax({
        type: "post",
        url: "/saleOrder/getTpuFromSession",
        data: {},
        datatype: "json",
        async: false,
        traditional: true,
        success: function (TPUID) {
            document.getElementById("hdn_Tpu").value = TPUID;
           
        },
    });
} 

function GETDEPOT() {
   
   
    $.ajax({
        type: "post",
        url: "/saleOrder/getDepotFromSession",
        data: {},
        datatype: "json",
        async: false,
        traditional: true,
        success: function (DEPOTID) {
            document.getElementById("hdn_sessiondepot").value = DEPOTID;
           
        },
    });
} 

function FetchExportBS() {
  

    $.ajax({
        type: "post",
        url: "/saleOrder/getEXPORTBSID",
        data: {},
        datatype: "json",
        async: false,
        traditional: true,
        success: function (EXPORTBSID) {
             
            $.each(EXPORTBSID, function (key, item) {
               
                $("#hdn_ExportBS").val(EXPORTBSID[0].EXPORTBSID);
                
            });
            
        },
    });

}

function LoadCurrency() {
  
    var groupid = $("#ddlgroup").val();
    $.ajax({
        type: "POST",
        url: "/saleOrder/BindCurrency",
        data: { groupid: groupid },
        async: false,
        success: function (result) {
           
            var currencey = $("#ddlCurrency");
            currencey.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(result, function () {
                currencey.val(this['CURRENCYNAME']);
                currencey.append($("<option value=''></option>").val(this['CURRENCYID']).html(this['CURRENCYNAME']));
            });
            currencey.chosen();
            currencey.trigger("chosen:updated");
           
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });

}

function LoadCustomer() {

    var TPUID = document.getElementById("hdn_Tpu").value;
    var groupid = $("#ddlgroup").val();
    var saleorderid = $("#hdn_saleorderid").val();
    var sessionDepotid = $("#hdn_sessiondepot").val();
    var depotid;

    if (TPUID == "D" || TPUID == "EXPU") {
        $.ajax({
            type: "POST",
            url: "/saleOrder/BindCustomer",
            data: { bstype: BSType, groupid: groupid, sessionDepotid: sessionDepotid, saleorderid: saleorderid },
            async: false,
            success: function (result) {
                var customer = $("#ddlcustomer");
                customer.empty().append('<option selected="selected" value="0">Please select</option>');
                $.each(result, function () {
                    customer.val(this['CUSTOMERID']);
                    customer.append($("<option value=''></option>").val(this['CUSTOMERID']).html(this['CUSTOMERNAME']));
                });
                customer.chosen();
                customer.trigger("chosen:updated");
                
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });

    }
    else {
        
        $.ajax({
            type: "POST",
            url: "/saleOrder/BindDepotBasedOnUser",
            data: { },
            async: false,
            success: function (BRID) {
                debugger;
                if (BRID.length == 0) {
                    depotid = BRID[0].BRID;
                    }
                    else {
                        depotid = sessionDepotid;
                }
            },
            failure: function (user) {
                alert(user.responseText);
            },
            error: function (user) {
                alert(user.responseText);
            }
        });
       

        $.ajax({
            type: "POST",
            url: "/saleOrder/BindCustomer",
            data: { bstype: BSType, groupid: groupid, sessionDepotid: depotid, saleorderid: saleorderid},
            async: false,
            success: function (result) {
                var customer = $("#ddlcustomer");
                customer.empty().append('<option selected="selected" value="0">Please select</option>');
                $.each(result, function () {
                    customer.val(this['CUSTOMERID']);
                    customer.append($("<option value=''></option>").val(this['CUSTOMERID']).html(this['CUSTOMERNAME']));
                });
                customer.chosen();
                customer.trigger("chosen:updated");
               
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    }

}

function loadProduct(BSType, group) {
   
    var saleorderid = $("#hdn_saleorderid").val();
    var group = $("#ddlgroup").val();
    $.ajax({
        type: "POST",
        url: "/saleOrder/BindProduct",
        data: { BSType, groupid: group, saleorderid},
        async: false,
        success: function (result) {
            
            var productname = $("#ddlProductName");
            productname.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(result, function () {
                productname.val(this['PRODUCTNAME']);
                productname.append($("<option value=''></option>").val(this['PRODUCTID']).html(this['PRODUCTNAME']));
            });
            productname.chosen();
            productname.trigger("chosen:updated");

           
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function bindPackSize() {
   

    var productId = $("#ddlProductName").val();
    var depotid = $("#hdn_sessiondepot").val();
    var customerId = $("#ddlcustomer").val();
    var groupId = $("#ddlgroup").val();
    var YMD = Conver_To_YMD($("#txtsaleorderdate").val());
    var mrp = 0;
    $.ajax({
        type: "POST",
        url: "/saleOrder/BindPackingSize",
        data: { productId },
        async: false,
        success: function (result) {
           
            var Packsize = $("#ddlpackingsize");
            Packsize.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(result, function () {
                Packsize.val(this['PACKSIZEName_FROM']);
                Packsize.val('1970C78A-D062-4FE9-85C2-3E12490463AF');
                Packsize.append($("<option value=''></option>").val(this['PACKSIZEID_FROM']).html(this['PACKSIZEName_FROM']));
            });
            Packsize.chosen();
            Packsize.trigger("chosen:updated");
           
            
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });

}

function LoadStockQuantity() {
   

    var productId = $("#ddlProductName").val();
    var depotid = $("#hdn_sessiondepot").val();
    var packSizeId = $("#ddlpackingsize").val();
    var BatchNo = 0;
    var mrp = 0;
    if (BSType != "2E96A0A4-6256-472C-BE4F-C59599C948B0")/*Except Export*/ {
        BindBatchDetails(depotid, productId, packSizeId, BatchNo);
    }
    else {
        var StoreLocationID = "5FA9501B-E8D0-4A0F-B917-17C636655514";/*EXPORT Location*/
        BindExportBatchDetails(depotid, productId, packSizeId, BatchNo, mrp, StoreLocationID);
    }
    
}

function BindBatchDetails() {
    $("#dialog").dialog({
        autoOpen: true,
        modal: true,
        title: "Loading.."
    });
    $("#imgLoader").css("visibility", "visible");
    var productId = $("#ddlProductName").val();
    var depotid = $("#hdn_sessiondepot").val();
    var packSizeId = $("#ddlpackingsize").val();
    var batchNo = 0;
    $.ajax({
        type: "POST",
        url: "/saleOrder/BindBatchDetails",
        data: { depotid, productId, packSizeId, batchNo },
        async: false,
        success: function (saleOrderModels) {
           
            if (saleOrderModels.length > 0) {
                if (packSizeId != "B9F29D12-DE94-40F1-A668-C79BF1BF4425") {
                    $("#txtStockQty").val(saleOrderModels[0].INVOICESTOCKQTY);
                }
                else {
                    $("#txtStockQty").val(saleOrderModels[0].INVOICESTOCKQTY);
                }
                $('#txtStockQty').css('background', '#00FF00');
            }
            else {
                $("#txtStockQty").val('0');
                $('#txtStockQty').css('background', '#FF6347');
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

function GetBaseCostPrice() {
   

    var productId = $("#ddlProductName").val();
    var depotid = $("#hdn_sessiondepot").val();
    var customerId = $("#ddlcustomer").val();
    var groupId = $("#ddlgroup").val();
    var YMD = Conver_To_YMD($("#txtsaleorderdate").val());
    var productName = $("#ddlProductName option:selected").text();
   
    var mrp = 0;
    $.ajax({
        type: "POST",
        url: "/saleOrder/GetBaseCostPrice",
        data: { customerId, productId, YMD, mrp, depotid, menuID, BSType, groupId },
        async: false,
        success: function (saleOrderModels) {
        
            if (saleOrderModels.length > 0) {
                if (saleOrderModels[0].BASECOSTPRICE > 0) {
                    $("#txtrate").val(saleOrderModels[0].BASECOSTPRICE);
                }
                else {
                    toastr.warning("<b><font color='red'>Rate not avilable for</font></b>" + " " + productName);
                    $("#txtrate").val('0')
                }
            }
            else {
                toastr.warning("<b><font color='red'>Rate not avilable for</font></b>" + " " + productName);
                $("#txtrate").val('0')
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

function bindGrid() {
 
    var scheduleOrderFromDate = $("#txtrequireddate").val();
    var scheduleOrderToDate = $("#txttorequireddate").val();
    var productId = $("#ddlProductName").val();
    var depotid = $("#hdn_sessiondepot").val();
    var packSizeId = $("#ddlpackingsize").val();
    var ratecheck = $("#txtrate").val();
    var BatchNo = 0;
    var mrp = 0; 

    if (scheduleOrderFromDate > scheduleOrderToDate) {
        toastr.warning("Order to date can not less than from date:<b><font color='red'> " + scheduleOrderFromDate + "!</font><b>");
    }
    else {
        if (SaleOrderRecordsCheck(productId, scheduleOrderFromDate, scheduleOrderToDate, BSType, ratecheck))
        {
            LoadSaleOrder();
        }
    }
   
}

function SaleOrderRecordsCheck(productId, scheduleOrderFromDate, scheduleOrderToDate, BSType, ratecheck) {
   
    var qty = $("#txtqty").val();
    var Packsize = $("#ddlpackingsize").val();
    var existsProducId = $("#ddlProductName").val();
    var existRrate = $("#txtrate").val();
    var exists = false;
    debugger;
    if ($('#gvSaleOrder').length)
    {
                $('#gvSaleOrder').each(function ()
                {
            
                    /*Same product check*/
                    debugger;
                   
                    var arraydetails = [];
                    var count = $('#gvSaleOrder tbody tr').length;
                    $('#gvSaleOrder tbody tr').each(function () {
                        var dispatchdetail = {};
                        var productidgrd = $(this).find('td:eq(0)').html().trim();
                        var rategrd = $(this).closest("tr").find('#lblRATE').val();
                        dispatchdetail.ProductID = productidgrd;
                        dispatchdetail.RATE = rategrd;
                        arraydetails.push(dispatchdetail);
                    });
            
                    var jsondispatchobj = {};
                    jsondispatchobj.fgDispatchDetails = arraydetails;
                    for (i = 0; i < jsondispatchobj.fgDispatchDetails.length; i++) {
                        if (jsondispatchobj.fgDispatchDetails[i].ProductID.trim() == existsProducId.trim() && jsondispatchobj.fgDispatchDetails[i].RATE.trim() === existRrate.trim()) {
                            exists = true;
                            break;
                        }
                    }
                })
    }

            if (exists != false)
            {
            toastr.warning('Item already exists...!');
             return false;
            }
       
            else
            {

                if ($("#hdn_saleorderid").val() != "") 
                {
                    bindReason();
                    $("#hdn_Mode").val('Add');
                    AlreadyDeliveredQty();
                }
                else {
                   
                    BindPackingSizeconversionqty(productId, Packsize, qty)
                }

            }
         
}

function bindReason() {
    
    if ($("#hdn_Mode").val() == 'Add') {
        $.ajax({
            type: "POST",
            url: "/saleOrder/bindReason",
            data: { menuID },
            async: false,
            success: function (response) {
                if ($('#gvSaleOrder').length) {
                    $('#gvSaleOrder').each(function () {
                        $('#gvSaleOrder tbody tr').each(function () {
                            var ddl = $(this).find('#ddlReason');
                            ddl.empty().append('<option selected="selected" value="0">Select</option>');;
                            $.each(response, function () {
                                ddl.val(this['REASON']);
                                ddl.append($("<option value=''></option>").val(this['REASONID']).html(this['REASON']));
                            });
                        })
                    })
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

    if ($("#hdn_Mode").val() == 'Edit') {
        $.ajax({
            type: "POST",
            url: "/saleOrder/saleOrderDetailsTable",
            data: { },
            async: false,
            success: function (response) {
                if (response.length === '0') {
                    toastr.error("Please Reloade the page");
                    return;
                }
                debugger;
                var splashArray = new Array();
                var splashArray1 = new Array();
                var ID ;
                $.each(response, function () {
                    splashArray.push($(this).attr("REASON"));
                });
                $.each(response, function () {
                    splashArray1.push($(this).attr("REASONID"));
                });
                var RowCount = (jQuery("tr", "#gvSaleOrder").length) - 1;
                for (var j = 0; j < RowCount; j++){
                 ID =j;
                 responCount = splashArray.length;
                 debugger;
                 var rows = $('#gvSaleOrder > tbody > tr');
                 var ddl=  $(rows.get(j)).addClass('my_class' + ID + '').find('#ddlReason');
                 ddl.empty();
                 ddl.val(splashArray[j]);
                 ddl.append($("<option value=''></option>").val(splashArray1[j]).html(splashArray[j]));
                 ddl.prop("disabled", true);
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

}

function AlreadyDeliveredQty() {
  
    var Packsize = $("#ddlpackingsize").val();
    var saleOrderid = $("#hdn_saleorderid").val();
    var productId = $("#ddlProductName").val();
    var InvoiceQty = 0;
    var DeliveredQty = 0;
    var DeliveredQty1 = 0;
    var qty = $("#txtqty").val();

    $.ajax({
        type: "POST",
        url: "/saleOrder/AlreadyDeliveredQty",
        data: { productId, Packsize, saleOrderid},
        async: false,
        success: function (saleOrderModels) {
            //alert(JSON.stringify(saleOrderModels));
            InvoiceQty = (saleOrderModels[0].ALREADY_DELIVEREDQTY);
            CalculateTotalPCS();
            DeliveredQty = InvoiceQty;
            DeliveredQty1 = DeliveredQty;

            if (parseInt(qty) < parseInt(InvoiceQty)) {
                toastr.warning("Order qty shoud n't be less than already Delivered <b>" + DeliveredQty1 + "</b> Pcs", 60, 500);
                return;
            }
            else {
                BindPackingSizeconversionqty(productId, Packsize, qty);
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

function BindPackingSizeconversionqty(productId, Packsize, qty) {
    debugger;
    var productName = $("#ddlProductName option:selected").text();
    var packsizeName = $("#ddlpackingsize option:selected").text();
    var requiredDate = $("#txtrequireddate").val();
    var toRequiredDate = $("#txttorequireddate").val();
    var covverqy = 0;
    var rate = $("#txtrate").val();
    $.ajax({
        type: "POST",
        url: "/saleOrder/BindPackingSizeconversionqty",
        data: { productId, Packsize, qty },
        async: false,
        success: function (saleOrderModels) {
            debugger;
            //alert(JSON.stringify(saleOrderModels));
            covverqy = saleOrderModels[0].converqty;
            if ($("#txtdiscount").val() == "") {
                $("#txtdiscount").val(0);
            }
            var dicount = $("#txtdiscount").val();
            var totalqty = covverqy * rate;
            var toalammount = ((totalqty * 100) / 100);
            debugger;
            var zero = 0;
            var reson = $("#ddlReason option:selected").text();
            var resonId = $("#ddlReason").val();
            var AmendmentQty = 0;
            BindSaleOrderGridRecords(productId, productName, productName, qty, Packsize, packsizeName, requiredDate, toRequiredDate, rate, dicount, toalammount, zero, reson, resonId, AmendmentQty);
            

        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function BindSaleOrderGridRecords(productId, productName, productName, qty, Packsize, packsizeName, requiredDate, toRequiredDate, rate, dicount, toalammount, zero, reson, resonId, AmendmentQty) {
    
    $.ajax({
        type: "POST",
        url: "/saleOrder/BindSaleOrderGridRecords",
        data: { productId, productName, productName, PQTY: qty, Packsize, packsizeName, requiredDate, toRequiredDate, rate, dicount, toalammount, QTY:zero, reson, resonId, AmendmentQty},
        async: false,
        dataType: "json",
        success: function (response) {
            var counter = 0;
            
                var tableNew = '<table id="gvSaleOrder" class="table table-striped table-bordered dt-responsive nowrap">';
                tableNew = tableNew + '<thead style="background-color:cornflowerblue;font-size:14px"><tr><th style="display:none"><th> Sl.</th><th>PRODUCT NAME</th><th>PO QTY</th><th>DELIVERY QTY</th><th>DISPATCH QTY</th> <th>PACKING SIZE</th> <th style="display:none"></th> <th>RATE</th> <th>DISCOUNT</th> <th>AMOUNT</th><th>FROM DATE</th> <th>TO DATE</th><th>REASON</th> <th>DELETE</th></tr></thead><tbody>';
            
           
            if ($("#hdn_saleorderid").val() != "") {

                $.each(response, function () {
                    counter = counter + 1;
                    tableNew = tableNew + "<tr><td style='display:none'>" + this["PRODUCTID"] + "</td> <td>" + counter + "</td><td><input type='lable' size='50' disabled='disabled'  id='lblPRODUCTNAME' style='text-align:left' value='" + this["PRODUCTNAME"] + "'></label></td><td><input type='text' size='4' class='poqty' id='txtORDERQTY' style='text-align:right' value='" + this["ORDERQTY"] + "'></td><td><input type='text' size='4' class='Amdqty' id='txtAMDORDERQTY' style='text-align:right' value='" + this["AMENDMENTQTY"] + "'></td><td><input type='Label'size='4' disabled='disabled' id='lblDESPATCHQTY' style='text-align:right' value='" + this["QTY"] + "'></td><td><input type='Label' size='10' disabled='disabled' id='lblPRODUCTPACKINGSIZE' style='text-align:left' value='" + this["PRODUCTPACKINGSIZE"] + "'></td><td style='display:none'><input type='Label' id='lblPRODUCTPACKINGSIZEID' style='text-align:right' value='" + this["PRODUCTPACKINGSIZEID"] + "'></td><td><input type='text' size='4' disabled='disabled' id='lblRATE' style='text-align:right' value='" + this["RATE"] + "'></td><td><input type='Label' size='4' disabled='disabled'  id='lblDISCOUNT' style='text-align:right' value='" + this["DISCOUNT"] + "'></td><td><input type='Label'size='4' disabled='disabled' id='lblDISCOUNTAMOUNT' style='text-align:right' value='" + this["DISCOUNTAMOUNT"] + "'></td><td><input type='Label' size='6' disabled='disabled' id='lblREQUIREDDATE' style='text-align:right' value='" + this["REQUIREDDATE"] + "'></td><td><input type='Label' size='6' disabled='disabled' id='lblREQUIREDTODATE' style='text-align:right' value='" + this["REQUIREDTODATE"] + "'></td><td><select class='gvReason'  id='ddlReason' style='width:100px; height:18px'></select></input></td><td><input type='image' class='gvdel' id='btnDel' src='../Images/ico_delete_16.png' title='Delete' /></td> </tr>";

                });
                document.getElementById("DivSaleOrder").innerHTML = tableNew + '</table>';
                bindReason();
            }
            else
            {
                $.each(response, function () {
                    counter = counter + 1;
                    tableNew = tableNew + "<tr><td style='display:none'>"+ this["PRODUCTID"] +"</td> <td>" + counter + "</td><td><input type='lable' size='50' disabled='disabled'  id='lblPRODUCTNAME' style='text-align:left' value='" + this["PRODUCTNAME"] + "'></label></td><td><input type='text' size='4' class='poqty' id='txtORDERQTY' style='text-align:right' value='" + this["ORDERQTY"] + "'></td><td><input type='text'size='4' class='Amdqty' disabled='disabled' id='txtAMDORDERQTY' style='text-align:right' value='" + this["ORDERQTY"] + "'></td><td><input type='Label'size='4' disabled='disabled' id='lblDESPATCHQTY' style='text-align:right' value='" + this["QTY"] + "'></td><td><input type='Label' size='10' disabled='disabled' id='lblPRODUCTPACKINGSIZE' style='text-align:left' value='" + this["PRODUCTPACKINGSIZE"] + "'></td><td style='display:none'><input type='Label' id='lblPRODUCTPACKINGSIZEID' style='text-align:right' value='" + this["PRODUCTPACKINGSIZEID"] + "'></td><td><input type='text'size='4' disabled='disabled' id='lblRATE' style='text-align:right' value='" + this["RATE"] + "'></td><td><input type='Label' size='4' disabled='disabled'  id='lblDISCOUNT' style='text-align:right' value='" + this["DISCOUNT"] + "'></td><td><input type='Label'size='4' disabled='disabled' id='lblDISCOUNTAMOUNT' style='text-align:right' value='" + this["DISCOUNTAMOUNT"] + "'></td><td><input type='Label' size='6' disabled='disabled' id='lblREQUIREDDATE' style='text-align:right' value='" + this["REQUIREDDATE"] + "'></td><td><input type='Label' size='6' disabled='disabled' id='lblREQUIREDTODATE' style='text-align:right' value='" + this["REQUIREDTODATE"] + "'></td><td><select class='gvReason'  id='ddlReason' style='width:100px; height:18px'></select></input></td><td><input type='image' class='gvdel' id='btnDel' src='../Images/ico_delete_16.png' title='Delete' /></td> </tr>";

                });
                document.getElementById("DivSaleOrder").innerHTML = tableNew + '</table>';
            }

               
             
            if (document.getElementById("hdn_ExportBS").value == document.getElementById("hdn_bsid").value)
            {
               
                if (response.length > 0) {
                    CalculateTotalCasePack();
                    CalculateTotalPCS();
                    CalculateTotalAmount();
                }

            }
            else
            {
                if (response.length > 0) {
                    CalculateTotalCasePack();
                    CalculateTotalPCS();
                    CalculateTotalAmount();
                }
            }
            $("#ddlProductName").focus();
        }
            
    });
   
}

function CalculateTotalCasePack() {
    debugger;
    var returnCase = 0;
    var RowCount = (jQuery("tr", "#gvSaleOrder").length) - 1;
    var row = $('#gvSaleOrder tbody tr').closest("tr");
   
    for (var i = 0; i < RowCount; i++) {
        var lblPRODUCTPACKINGSIZEID = $("input[id*=lblPRODUCTPACKINGSIZEID]");
        var txtORDERQTY = $("input[id*=txtORDERQTY]");
        var Productid = row.find('td:eq(0)').html().trim();
        PacksizeID = lblPRODUCTPACKINGSIZEID[i].value;
        Qty = txtORDERQTY[i].value;
        var mode = 'CasePack';
        $.ajax({
            type: "POST",
            url: "/saleOrder/CalculateCasePcs",
            data: { mode, Productid, PacksizeID, Qty },
            async: false,
            dataType: "json",
            success: function (saleOrderModels) {
            returnCase += (saleOrderModels[0].RETURNVALUE);
            }
        })
        debugger;
    }
    $('#txtTotalCase').val(returnCase);

}

function CalculateTotalPCS() {
    var returnPcs = 0;
    var row = $('#gvSaleOrder tbody tr').closest("tr");
    
    var RowCount = (jQuery("tr", "#gvSaleOrder").length) - 1;
    for (var i = 0; i < RowCount; i++) {
        var lblPRODUCTPACKINGSIZEID = $("input[id*=lblPRODUCTPACKINGSIZEID]");
        var txtORDERQTY = $("input[id*=txtORDERQTY]");
        var Productid = row.find('td:eq(0)').html().trim();
        PacksizeID = lblPRODUCTPACKINGSIZEID[i].value;
        Qty = txtORDERQTY[i].value;
      
        var mode = 'Pcs';
        $.ajax({
            type: "POST",
            url: "/saleOrder/CalculateCasePcs",
            data: { mode, Productid, PacksizeID, Qty },
            async: false,
            dataType: "json",
            success: function (saleOrderModels) {
                debugger;
                returnPcs +=(saleOrderModels[0].RETURNVALUE);
            }
        })
    }
    $('#txtTotalPCS').val(returnPcs);
}

function CalculateTotalAmount() {

    debugger;
    var RowCount = (jQuery("tr", "#gvSaleOrder").length) - 1;
    var TotalAmount = 0;
    var Amount = 0;
    for (var i = 0; i < RowCount; i++) {
        var lblAmount = $("input[id*=lblDISCOUNTAMOUNT]");
        Amount = lblAmount[i].value;
        TotalAmount += parseInt(Amount);
    }
    if ($("#hdn_saleorderid").val() != "") {
        debugger;
        if (RowCount > 0) {
            $("#ddlgroup").prop("disabled", true);
            $("#ddlgroup").chosen({
                search_contains: true
            });
            $("#ddlgroup").trigger("chosen:updated");
            $("#ddlcustomer").prop("disabled", true);
            $("#ddlcustomer").chosen({
                search_contains: true
            });
            $("#ddlcustomer").trigger("chosen:updated");
        }
        else {
            $("#ddlgroup").prop("disabled", false);
            $("#ddlgroup").chosen({
                search_contains: true
            });
            $("#ddlgroup").trigger("chosen:updated");
            $("#ddlcustomer").prop("disabled", false);
            $("#ddlcustomer").chosen({
                search_contains: true
            });
            $("#ddlcustomer").trigger("chosen:updated");
        }
    }
    
    $('#txtTotalAmount').val(TotalAmount.toFixed(2));
}

function InsertSaleOrderDetails()
{
   
    
    var strTermsID = '0';
    var sale = {};
    sale.saleOrderDate = $("#txtsaleorderdate").val();
    sale.saleOrderNo = $("#txtrefsaleorderno").val();
    sale.refSaleOrderDate = $("#txtrefsaleorderdate").val();
    sale.bsId = $("#hdn_bsid").val();
    sale.bsName = $("#hdn_bsname").val();
    sale.groupId = $("#ddlgroup").val();
    sale.groupName =  $("#ddlgroup").find('option:selected').text();
    sale.customerId = $("#ddlcustomer").val();
    sale.customerName = $("#ddlcustomer").find('option:selected').text();
    sale.remarks = $("#txtRemarks").val();
    sale.saleOrderId = $("#hdn_saleorderid").val();
    if ($("#hdn_saleorderid").val() == "") {
        sale.Flag = "A"
    }
    else {
        sale.Flag = "U"
    }
    sale.isCancelled = status;
    sale.MRPTag = MRPTag;
    //sale.strTermsID = "1";
    sale.currenceyId = $("#ddlCurrency").val();
    sale.currenceyName = $("#ddlCurrency").find('option:selected').text();
    sale.deliverytermsId = $("#ddldeliveryterms").val();
    
    sale.deliverytermsName = $("#ddldeliveryterms").find('option:selected').text();
    sale.icds = $("#txtICDS").val();
    sale.icdsDate = $("#txtICDSDate").val();
    sale.TotalCase = $('#txtTotalCase').val();
    sale.TotalPCS = $('#txtTotalPCS').val();
    
    sale.Paymentterms = $("#ddlPaymentterms").find('option:selected').text();
    sale.Usanceperiod = $("#ddlUsanceperiod").find('option:selected').text();
    sale.reasonid=
    sale.strTermsID = strTermsID;

    $.ajax({
        url: "/saleOrder/InsertSaleOrderDetails",
        data: '{sale:' + JSON.stringify(sale) + '}',
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
            close();
            LoadSale();
            ReleaseSession();
            cleeardata();
            $('#hdn_saleorderid').val('');

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

function addNewRecord() {
    debugger;
   
    $("#hdn_saleorderid").val = "";
    LoadCurrency();
    /*for chosen class*/
    var currencey = $("#ddlCurrency");
    var _ddlgroup = $("#ddlgroup");
    var _ddlcustomer = $("#ddlcustomer");
    var _deliveryTerms = $("#ddldeliveryterms");
    var _ddlProductName = $("#ddlProductName");
    var _ddlpackingsize = $("#ddlpackingsize");
    $("#txttorequireddate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
    $("#txtrequireddate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
    currencey.chosen('destroy');
    currencey.chosen({ width: '180px' });
    _ddlgroup.chosen('destroy');
    _ddlgroup.chosen({ width: '180px' });
    _ddlcustomer.chosen('destroy');
    _ddlcustomer.chosen({ width: '200px' });
    _deliveryTerms.chosen('destroy');
    _deliveryTerms.chosen({ width: '180px' });
    _ddlProductName.chosen('destroy');
    _ddlProductName.chosen({ width: '230px' });
    _ddlpackingsize.chosen('destroy');
    _ddlpackingsize.chosen({ width: '180px' });
    /*end*/
    $("#ddlgroup").prop("disabled", false);
    $("#ddlgroup").chosen({
        search_contains: true
    });
    $("#ddlgroup").trigger("chosen:updated");

    $("#ddlcustomer").prop("disabled", false);
    $("#ddlcustomer").chosen({
        search_contains: true
    });
    $("#ddlcustomer").trigger("chosen:updated");
   

    $('#tr_saleprderno').css("display", "none");
    $('#tdscheduledate').css("display", "none");
    $('#divcancelorder').css("display", "none");
    FetchExportBS();
    if ($("#hdn_ExportBS").val() == BSType) {
        $('#trCurrency').css("display", "");
    }
    else {
        $('#trCurrency').css("display", "none");
    }
    if (BSType == "C5038911-9331-40CF-B7F9-583D50583592") {
        $('#trICDS').css("display", "");
    } else {
        $('#trICDS').css("display", "none");
    }
     
    LoadBUSINESSSEGMENT(BSType);
    LoadDeleveryTerms();
    LoadSaleOrder();
    LoadPrincipleGroup(BSType);
    $("#hdn_SumTotalAmount").val(0);
    GETTPU();
    GETDEPOT();
    var TPUID = document.getElementById("hdn_Tpu").value;
    if (TPUID == "DI" || TPUID == "SUDI" || TPUID == "ST" || TPUID == "SST") {
         
        var group_id;
        $.ajax({
            type: "post",
            url: "/saleOrder/getGroupFromSession",
            data: {},
            datatype: "json",
            async: false,
            traditional: true,
            success: function (GROUPID) {
                $.each(GROUPID, function (key, item) {
                    group_id.val(GROUPID[0].GROUPID);
                });
                var group = $("#ddlgroup");
                group.val(this['group_id']);
                group.prop("disabled", true);
            },
        });
        $.ajax({
            type: "post",
            url: "/saleOrder/getCustomerFromSession",
            data: {},
            datatype: "json",
            async: false,
            traditional: true,
            success: function (CUSTOMERID) {
                cus_id = CUSTOMERID;
                var customer = $("#ddlcustomer");
                customer.empty();
                customer.val(this['cus_id']);
                customer.prop("disabled", true);
            },
        });
    }
    else {
        var group = $("#ddlgroup");
        var customer = $("#ddlcustomer");
        group.prop("disabled", false);
        customer.prop("disabled", false);
    }

    $("#hdn_Mode").val = null;
    
}

function close() {
    
    $('#pnlAdd').css("display", "none");
    $('#pnlDisplay').css("display", "block");
    cleeardata();
    ReleaseSession();
}

function delgrid(_PRODUCT, rate ,_PRODUCTNAME) {
    
    $.ajax({
        type: "POST",
        url: "/saleOrder/SaleOrderRecordsDelete",
        data: {
            PRODUCTID: _PRODUCT, rate,PRODUCTNAME: _PRODUCTNAME
        },

        success: function (response) {
            var counter = 0;
            var tableNew = '<table id="gvSaleOrder" class="table table-striped table-bordered dt-responsive nowrap">';
            tableNew = tableNew + '<thead style="background-color:cornflowerblue;font-size:14px"><tr><th style="display:none"><th> Sl.</th><th>PRODUCT NAME</th><th>PO QTY</th><th>DELIVERY QTY</th><th>DISPATCH QTY</th> <th>PACKING SIZE</th> <th style="display:none"></th> <th>RATE</th> <th>DISCOUNT</th> <th>AMOUNT</th><th>FROM DATE</th> <th>TO DATE</th><th>REASON</th> <th>DELETE</th></tr></thead><tbody>';
             
                $.each(response, function () {
                    counter = counter + 1;
                   // tableNew = tableNew + "<tr><td style='display:none'><input type='Label'  id='lblPRODUCT' style='text-align:right' value='" + this["PRODUCTID"] + "'></td> <td>" + counter + "</td><td><input type='lable' size='50' disabled='disabled'  id='lblPRODUCTNAME' style='text-align:left' value='" + this["PRODUCTNAME"] + "'></label></td><td><input type='text' size='4' class='poqty' id='txtORDERQTY' style='text-align:right' value='" + this["ORDERQTY"] + "'></td><td><input type='text' size='4' class='Amdqty' id='txtAMDORDERQTY' style='text-align:right' value='" + this["AMENDMENTQTY"] + "'></td><td><input type='Label'size='4' disabled='disabled' id='lblDESPATCHQTY' style='text-align:right' value='" + this["QTY"] + "'></td><td><input type='Label' size='10' disabled='disabled' id='lblPRODUCTPACKINGSIZE' style='text-align:left' value='" + this["PRODUCTPACKINGSIZE"] + "'></td><td style='display:none'><input type='Label' id='lblPRODUCTPACKINGSIZEID' style='text-align:right' value='" + this["PRODUCTPACKINGSIZEID"] + "'></td><td><input type='text'size='4' disabled='disabled' id='lblRATE' style='text-align:right' value='" + this["RATE"] + "'></td><td><input type='Label' size='4' disabled='disabled'  id='lblDISCOUNT' style='text-align:right' value='" + this["DISCOUNT"] + "'></td><td><input type='Label'size='4' disabled='disabled' id='lblDISCOUNTAMOUNT' style='text-align:right' value='" + this["DISCOUNTAMOUNT"] + "'></td><td><input type='Label' size='6' disabled='disabled' id='lblREQUIREDDATE' style='text-align:right' value='" + this["REQUIREDDATE"] + "'></td><td><input type='Label' size='6' disabled='disabled' id='lblREQUIREDTODATE' style='text-align:right' value='" + this["REQUIREDTODATE"] + "'></td><td><select class='gvReason'  id='ddlReason' style='width:100px; height:18px'></select></input></td><td><input type='image' class='gvdel' id='btnDel' src='../Images/ico_delete_16.png' title='Delete' /></td> </tr>";
                    tableNew = tableNew + "<tr><td style='display:none'>" + this["PRODUCTID"] + "</td> <td>" + counter + "</td><td><input type='lable' size='50' disabled='disabled'  id='lblPRODUCTNAME' style='text-align:left' value='" + this["PRODUCTNAME"] + "'></label></td><td><input type='text' size='4' class='poqty' id='txtORDERQTY' style='text-align:right' value='" + this["ORDERQTY"] + "'></td><td><input type='text' size='4' class='Amdqty' id='txtAMDORDERQTY' style='text-align:right' value='" + this["AMENDMENTQTY"] + "'></td><td><input type='Label'size='4' disabled='disabled' id='lblDESPATCHQTY' style='text-align:right' value='" + this["QTY"] + "'></td><td><input type='Label' size='10' disabled='disabled' id='lblPRODUCTPACKINGSIZE' style='text-align:left' value='" + this["PRODUCTPACKINGSIZE"] + "'></td><td style='display:none'><input type='Label' id='lblPRODUCTPACKINGSIZEID' style='text-align:right' value='" + this["PRODUCTPACKINGSIZEID"] + "'></td><td><input type='text' size='4' disabled='disabled' id='lblRATE' style='text-align:right' value='" + this["RATE"] + "'></td><td><input type='Label' size='4' disabled='disabled'  id='lblDISCOUNT' style='text-align:right' value='" + this["DISCOUNT"] + "'></td><td><input type='Label'size='4' disabled='disabled' id='lblDISCOUNTAMOUNT' style='text-align:right' value='" + this["DISCOUNTAMOUNT"] + "'></td><td><input type='Label' size='6' disabled='disabled' id='lblREQUIREDDATE' style='text-align:right' value='" + this["REQUIREDDATE"] + "'></td><td><input type='Label' size='6' disabled='disabled' id='lblREQUIREDTODATE' style='text-align:right' value='" + this["REQUIREDTODATE"] + "'></td><td><select class='gvReason'  id='ddlReason' style='width:100px; height:18px'></select></input></td><td><input type='image' class='gvdel' id='btnDel' src='../Images/ico_delete_16.png' title='Delete' /></td> </tr>";

                });
                document.getElementById("DivSaleOrder").innerHTML = tableNew + '</table>';
                bindReason();
            /*data table*/
            debugger;

            if (document.getElementById("hdn_ExportBS").value == document.getElementById("hdn_bsid").value) {

                if (response.length > 0) {
                    CalculateTotalCasePack();
                    CalculateTotalPCS();
                    CalculateTotalAmount();
                }
                else {
                    CalculateTotalCasePack();
                    CalculateTotalPCS();
                    CalculateTotalAmount();
                }

            }
            else {
                if (response.length > 0) {
                    CalculateTotalCasePack();
                    CalculateTotalPCS();
                    CalculateTotalAmount();
                } else{
                    CalculateTotalCasePack();
                    CalculateTotalPCS();
                    CalculateTotalAmount();
                }
            }
            $("#ddlProductName").focus();
         }
    });
}

function ReleaseSession() {
    $.ajax({
        type: "POST",
        url: "/saleOrder/RemovesaleOrderSession",
        data: '{}',
        dataType: "json",
        success: function (response) {


        }
    });
}

function cleeardata() {
    
    $('#divcancelorder').css("display", "none");
    $('#ddlProductName').empty(); 
    $('#ddlpackingsize').empty(); 
    $('#ddlgroup').empty(); 
    $('#ddlcustomer').empty(); 
    $('#ddldeliveryterms').empty(); 
    $('#ddlCurrency').empty(); 
    $('#txtdiscount').val(0);
    $('#txtrate').val(0);
    $('#txtStockQty').val(0);
    $('#txtqty').val(0);
    $('#txtTotalCase').val(0);
    $('#txtTotalPCS').val(0);
    $('#txtTotalAmount').val(0);
    $('#txtRemarks').val('');
    $('#txtrequireddate').val('');
    $('#txttorequireddate').val('');
    $('#txtrefsaleorderdate').val('');
    $('#txtrefsaleorderno').val('');
    $('#hdn_saleorderno').val('');
    $('#hdn_saleorderdelete').val('');
    $('#hdn_saleorderid').val('');
    $("#hdn_Mode").val('');
    $('#gvSaleOrder thead').remove();
    $('#gvSaleOrder tbody tr').remove();
    $('#grdTerms tbody tr').remove();
    $('#grdTerms thead').remove();

}

function LoadSale() {
    $("#dialog").dialog({
        autoOpen: true,
        modal: true,
        title: "Loading.."
    });
    $("#imgLoader").css("visibility", "visible");
    
     
    if ($('#hdn_Tpu').val() == "D" || $('#hdn_Tpu').val() == "EXPU") {
        loadSaleGrid($('#hdn_sessiondepot').val());
    }
    else {
        $.ajax({
            type: "POST",
            url: "/saleOrder/BindDepotBasedOnUser",
            data: {},
            async: false,
            success: function (result) {

                if (result.length > 0) {
                    if (result.length == 0) {
                        depotid=(result[0].BRID);
                    }
                    else {
                        depotid=(result[0].DEPOTID);
                    }
                }
                loadSaleGrid(depotid);

                
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });

    }
}

function loadSaleGrid(_depotid) {

 
    var fromdate=$('#txtfromdate').val();
    var todate = $('#txttodate').val();
    var counter = 0;
    $.ajax({
        type: "POST",
        url: "/saleOrder/LoadSale",
        data: { fromdate, todate, BSType, _depotid },
        dataType: "json",
        success: function (response) {
            var tableNew = '<table id="gvsaleorderdetails" class="table table-striped table-bordered dt-responsive nowrap" style="width:100%">';
            tableNew = tableNew + '<thead style="background-color:cornflowerblue;font-size:14px"><tr id="header"><th style="display:none">SALEORDERID</th><th> Sl.</th><th>ORDER DATE</th><th>ORDER NO</th><th>REF.PO.NO.</th><th>REF.PO.DATE</th><th>CUSTOMER</th><th>TOTAL VALUE</th><th>TOTAL PO CASE</th><th>TOTAL PO PCS</th><th>Edit</th><th>View</th><th>Print</th><th>Del</th></tr></thead> <tbody>';
            $.each(response, function () {
                 
                
                counter = counter + 1;
                tableNew = tableNew + "<tr><td style='display:none'>" + this["SALEORDERID"] + "</td> <td>" + counter + "</td><td>" + this["SALEORDERDATE"] + "</td>   <td>" + this["SALEORDERNO"] + "</td><td>" + this["REFERENCESALEORDERNO"] + "</td>   <td>" + this["REFERENCESALEORDERDATE"] + "</td><td>" + this["CUSTOMERNAME"] + "</td> <td>" + this["TOTALVALUE"] + "</td>  <td>" + this["TOTALCASE"] + "</td><td>" + this["TOTALPCS"] + "</td><td><input type='image' class='gvEdit' id='btnEdit'  src='../images/Pencil-icon.png ' width='30' height ='30'/></td> <td><input type='image' class='gvView' id='btnView'  src='../images/View.png ' width='30' height ='30'/></td> <td><input type='image' class='gvprint' id='btnprint'  src='../images/print.png ' width='30' height ='30'/></td>  <td><input type='image' class='gvdel' id='btnDel' src='../Images/ico_delete_16.png' title='Delete' /></td> </tr>";


            });
            document.getElementById("DivHeaderRow").innerHTML = tableNew + '</table>';
            $('#gvsaleorderdetails').DataTable({
                "sScrollX": '100%',
                "sScrollXInner": "110%",
                "scrollY": "238px",
                "scrollCollapse": true,
                "dom": 'Bfrtip',
                buttons: [
                    {
                        extend: 'excelHtml5',
                        title: 'Gift Claim'
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
        }
       
    })
    

}

function FetchSaleOrderDetails() {
     
    var saleorderId = $("#hdn_saleorderid").val();
   
    $.ajax({
        type: "POST",
        url: "/saleOrder/FetchSaleOrderDetails",
        data: { saleorderId: saleorderId },
        async: false,
        dataType: "json",
        success: function (response) {
            if (response.length > 0) {
                $.each(response, function () {
                    LoadPrincipleGroup(BSType);
                    $("#ddlgroup").val(this["groupId"]);
                    $("#ddlgroup").chosen({
                        search_contains: true
                    });

                    $("#ddlgroup").trigger("chosen:updated");
                     
                    LoadCustomer();
                    $("#ddlcustomer").val(this["customerId"]);
                    $("#ddlcustomer").chosen({
                        search_contains: true
                    });
                    $("#ddlcustomer").trigger("chosen:updated");
                    
                    $("#txtsaleorderdate").val(this["saleOrderDate"]);
                    $("#txtRemarks").val(this["remarks"]);
                   
                    if (this["isCancelled"] == "Y") {
                        $('#chkactive').prop('checked', true);

                    } else {
                        $('#chkactive').prop('checked', false);
                    }
                    $("#txtrefsaleorderno").val(this["saleOrderNo"]);
                    $("#txtrefsaleorderdate").val(this["refSaleOrderDate"]);
                    LoadCurrency($("#ddlgroup").val());
                    $("#ddlCurrency").val(this["currenceyId"]);
                    $("#ddlCurrency").prop("currenceyId", true);
                    $("#ddlCurrency").chosen({
                        search_contains: true
                    });
                   
                    LoadDeleveryTerms();
                    $("#ddldeliveryterms").val(this["deliverytermsId"]);
                    $("#ddldeliveryterms").prop("deliverytermsId", true);
                    $("#ddldeliveryterms").chosen({
                        search_contains: true
                    });
                   
                    if (this["MRPTag"] == "Y") {
                        $('#chkMRPTag').prop('checked', true);

                    } else {
                        $('#chkMRPTag').prop('checked', false);
                    }
                    $("#txtICDS").val(this["icds"]);
                    if (this["icdsDate"] == "01/01/1900") {
                        $("#txtICDSDate").val('');
                    }
                    else {
                        $("#txtICDSDate").val(this["icdsDate"]);
                    }
                    
                    $("#txtTotalCase").val(this["TotalCase"]);
                    $("#txtTotalPCS").val(this["TotalPCS"]);
                   // $("#ddlgroup").val(this["Paymentterms"]);
                   // $("#ddlgroup").val(this["Usanceperiod"]);

                    loadProduct(BSType, this["groupId"]);
                    $("#ddlProductName").val(this["deliverytermsId"]);
                    $("#ddlProductName").prop("deliverytermsId", true);
                    $("#ddlProductName").chosen({
                        search_contains: true
                    });
                    $('#divcancelorder').css("display", "");
                    FetchExportBS();
                    saleOrderDetailsTable();
                })
            }
        
        }
    })
}

function saleOrderDetailsTable() {
    $.ajax({
        type: "POST",
        url: "/saleOrder/saleOrderDetailsTable",
        data: '{}',
        async: false,
        dataType: "json",
        success: function (response) {
           
           
            var counter = 0;
            var reasonId;
            var tableNew = '<table id="gvSaleOrder" class="table table-striped table-bordered dt-responsive nowrap" style="width:100%">';
            tableNew = tableNew + '<thead style="background-color:cornflowerblue;font-size:14px"><tr><th style="display:none"><th> Sl.</th><th>PRODUCT NAME</th><th>PO QTY</th><th>DELIVERY QTY</th><th>DISPATCH QTY</th> <th>PACKING SIZE</th> <th style="display:none"></th> <th>RATE</th> <th>DISCOUNT</th> <th>AMOUNT</th><th>FROM DATE</th> <th>TO DATE</th><th>REASON</th> <th>DELETE</th></tr></thead><tbody>';
            $.each(response, function () {
                counter = counter + 1;
                //tableNew = tableNew + "<tr><td style='display:none'><input type='Label'  id='lblPRODUCT' style='text-align:right' value='" + this["PRODUCTID"] + "'></td> <td>" + counter + "</td><td><input type='lable' size='50' disabled='disabled'  id='lblPRODUCTNAME' style='text-align:left' value='" + this["PRODUCTNAME"] + "'></label></td><td><input type='text' size='4' class='poqty' id='txtORDERQTY'  style='text-align:right' value='" + this["ORDERQTY"] + "'></td><td><input type='text'size='4' class='Amdqty' id='txtAMDORDERQTY' style='text-align:right' value='" + this["AMENDMENTQTY"] + "'></td><td><input type='Label'size='4' disabled='disabled' id='lblDESPATCHQTY' style='text-align:right' value='" + this["QTY"] + "'></td><td><input type='Label' size='10' disabled='disabled' id='lblPRODUCTPACKINGSIZE' style='text-align:left' value='" + this["PRODUCTPACKINGSIZE"] + "'></td><td style='display:none'><input type='Label' id='lblPRODUCTPACKINGSIZEID' style='text-align:right' value='" + this["PRODUCTPACKINGSIZEID"] + "'></td><td><input type='text'size='4' disabled='disabled' id='lblRATE' style='text-align:right' value='" + this["RATE"] + "'></td><td><input type='Label' size='4' disabled='disabled'  id='lblDISCOUNT' style='text-align:right' value='" + this["DISCOUNT"] + "'></td><td><input type='Label'size='4' disabled='disabled' id='lblDISCOUNTAMOUNT' style='text-align:right' value='" + this["DISCOUNTAMOUNT"] + "'></td><td><input type='Label' size='6' disabled='disabled' id='lblREQUIREDDATE' style='text-align:right' value='" + this["REQUIREDDATE"] + "'></td><td><input type='Label' size='6' disabled='disabled' id='lblREQUIREDTODATE' style='text-align:right' value='" + this["REQUIREDTODATE"] + "'></td><td><select class='gvReason'  id='ddlReason' style='width:100px; height:18px'></select></input></td><td><input type='image' class='gvdel' id='btnDel' src='../Images/ico_delete_16.png' title='Delete' /></td> </tr>";

                tableNew = tableNew + "<tr><td style='display:none'>" + this["PRODUCTID"] + "</td> <td>" + counter + "</td><td><input type='lable' size='50' disabled='disabled'  id='lblPRODUCTNAME' style='text-align:left' value='" + this["PRODUCTNAME"] + "'></label></td><td><input type='text' size='4' class='poqty' id='txtORDERQTY' style='text-align:right' value='" + this["ORDERQTY"] + "'></td><td><input type='text' size='4' class='Amdqty' id='txtAMDORDERQTY' style='text-align:right' value='" + this["AMENDMENTQTY"] + "'></td><td><input type='Label'size='4' disabled='disabled' id='lblDESPATCHQTY' style='text-align:right' value='" + this["QTY"] + "'></td><td><input type='Label' size='10' disabled='disabled' id='lblPRODUCTPACKINGSIZE' style='text-align:left' value='" + this["PRODUCTPACKINGSIZE"] + "'></td><td style='display:none'><input type='Label' id='lblPRODUCTPACKINGSIZEID' style='text-align:right' value='" + this["PRODUCTPACKINGSIZEID"] + "'></td><td><input type='text' size='4' disabled='disabled' id='lblRATE' style='text-align:right' value='" + this["RATE"] + "'></td><td><input type='Label' size='4' disabled='disabled'  id='lblDISCOUNT' style='text-align:right' value='" + this["DISCOUNT"] + "'></td><td><input type='Label'size='4' disabled='disabled' id='lblDISCOUNTAMOUNT' style='text-align:right' value='" + this["DISCOUNTAMOUNT"] + "'></td><td><input type='Label' size='6' disabled='disabled' id='lblREQUIREDDATE' style='text-align:right' value='" + this["REQUIREDDATE"] + "'></td><td><input type='Label' size='6' disabled='disabled' id='lblREQUIREDTODATE' style='text-align:right' value='" + this["REQUIREDTODATE"] + "'></td><td><select class='gvReason'  id='ddlReason' style='width:100px; height:18px' value='" + this["REASONID"] + "'></select></input></td><td><input type='image' class='gvdel' id='btnDel' src='../Images/ico_delete_16.png' title='Delete' /></td> </tr>";
                
               
            });
            document.getElementById("DivSaleOrder").innerHTML = tableNew + '</table>';
            if ($("#hdn_saleorderid").val() != "") {
                bindReason();
            }
           /*data table*/
           
            if (document.getElementById("hdn_ExportBS").value == document.getElementById("hdn_bsid").value) {

                if (response.length > 0) {
                    CalculateTotalCasePack();
                    CalculateTotalPCS();
                    CalculateTotalAmount();
                }

            }

            else {
                if (response.length > 0) {
                    CalculateTotalCasePack();
                    CalculateTotalPCS();
                    CalculateTotalAmount();
                }
            }

            $("#imgLoader").css("visibility", "hidden");
            $("#dialog").dialog("close");
        }
    });
}

function LoadTermsConditions(strTermsID) {
   
    $.ajax({
        type: "POST",
        url: "/saleOrder/BindTerms",
        data: { menuID },
        async: false,
        dataType: "json",
        success: function (response) {
            var counter = 0;
            var tableNew = '<table id="grdTerms" class="table table-striped table-bordered dt-responsive nowrap" style="width:100%">';
            tableNew = tableNew + '<thead style="background-color:cornflowerblue;font-size:14px"><tr><th> Sl.</th><th>TERMS & Conditions</th><th>TERMSID</th></tr></thead><tbody>';
            $.each(response, function () {
                counter = counter + 1;
                tableNew = tableNew + "<tr><td>" + counter + "</td><td>" + this["DESCRIPTION"] + "</td><td>" + this["ID"] + "</td><td id='hdnTERMSName' style='display:none'><input type='textbox' id='ChkIDTERMS'>" + this["ID"] + "</td></tr>";

            });
            var terms = '';
            var termsId = '';
            
            if ($('#grdTerms').length) {
                $('#grdTerms tbody tr').each(function () {
                    if ($(this).find('#ChkIDTERMS').prop('checked') == true) {
                        flag = true;
                        count = count + 1;
                        terms = $(this).find('td:eq(1)').html();
                        termsId = $(this).find('td:eq(2)').html();
                        strTermsID = terms + '' + termsId ;
                    }
                    else {
                        strTermsID = terms + '' + termsId ;
                    }
                });
            }
            
            document.getElementById("grdTerms").innerHTML = tableNew + '</table>';
        }
    });
}

function ProformaAmount(ProformaID) {
    var proformaInvAmt=0;
    $.ajax({
        type: "post",
        url: "/saleOrder/ProformaAmount",
        data: { ProformaID },
        datatype: "json",
        async: false,
        traditional: true,
        success: function (ProformaAmount) {
            if (ProformaAmount.length > 0) {
                $.each(ProformaAmount, function (key, item) {
                    proformaInvAmt.val(ProformaInvoice[0].GROSSAMOUNT);
                });
            }
        }
    });
}

function updateScheduleDate() {
     
    var dtpofromdate;
    var dtpotodate;
    dtpofromdate = Conver_To_ISO($('#txtrequireddate').val());
    dtpotodate = Conver_To_ISO($('#txttorequireddate').val());
    if (dtpofromdate > dtpotodate) {
        toastr.warning("Order to date can not less than from date:<b><font color='red'> " + dtpofromdate + "!</font><b>", 80, 500);
    }
    else {
        var REQUIREDDATE;
        var REQUIREDTODATE;
        $.ajax({
            type: "POST",
            url: "/saleOrder/updateScheduleDate",
            data: { dtpofromdate: $('#txtrequireddate').val(), dtpotodate: $('#txttorequireddate').val()},
            dataType: "json",
            success: function (saleOrderModels) {
                debugger;
                $('#gvSaleOrder tbody tr').each(function () {
                    $.each(saleOrderModels, function (index, record) {
                            REQUIREDDATE = this['REQUIREDDATE'];
                            REQUIREDTODATE = this['REQUIREDTODATE'];
                    });
                    var RowCount = (jQuery("tr", "#gvSaleOrder").length) - 1;
                    for (var i = 0; i < RowCount; i++) {
                        $("input[id*=lblREQUIREDDATE]").val(REQUIREDDATE);
                        $("input[id*=lblREQUIREDTODATE]").val(REQUIREDTODATE);
                    }
                })
               
            }
        });

    }

}

function updateQty(_PRODUCT, _RATE,_DESPATCHQTY, amount) {

    $.ajax({
        type: "POST",
        url: "/saleOrder/updateQty",
        data: { _PRODUCT, rate: _RATE,_DESPATCHQTY, amount },
        dataType: "json",
        success: function (response) {
            if (document.getElementById("hdn_ExportBS").value == document.getElementById("hdn_bsid").value) {
                debugger;
                if (response.length > 0) {
                    CalculateTotalCasePack();
                    CalculateTotalPCS();
                    CalculateTotalAmount();
                }

            }
            else {
                debugger;
                if (response.length > 0) {
                    CalculateTotalCasePack();
                    CalculateTotalPCS();
                    CalculateTotalAmount();
                }
            }
        }
    });
}

function updateAmdQty(_PRODUCT, _RATE, _DESPATCHQTY) {
    $.ajax({
        type: "POST",
        url: "/saleOrder/updateAmdQty",
        data: { _PRODUCT, rate: _RATE, _DESPATCHQTY},
        dataType: "json",
        success: function (response) {
        }
    });
}

function upadateReason(_PRODUCT, _REASONID, _REASON) {
    $.ajax({
        type: "POST",
        url: "/saleOrder/upadateReason",
        data: { _PRODUCT, _REASONID, _REASON },
        dataType: "json",
        success: function (response) {

        }
    });
}

function Conver_To_YMD(dt)
{

    dteSplit = dt.split("/");
    day = dteSplit[0]; //special yr format, take last 2 digits
    month = dteSplit[1];
    year = dteSplit[2];
    dt = year + month + day;
    dt = year + '-' + month + '-' + day + " 00:00:00.000";
    return dt;

}

function Conver_To_ISO( dt)
{
    dteSplit = dt.split("/");
    day = dteSplit[0]; //special yr format, take last 2 digits
    month = dteSplit[1];
    year = dteSplit[2];
    dt = year + month + day;
    return dt;

}

function GetDeletestatus(saleOrderId) {
    var deleteStatus = 0;
    $.ajax({
        type: "POST",
        url: "/saleOrder/get_Delete_Status",
        data: { saleOrderId },
        dataType: "json",
        async: false,
        success: function (statusCheck) {
            debugger;
            if (statusCheck.length > 0) {
                deleteStatus = 1;
            }
            else {
                deleteStatus = 0;
            }
        },
    });
    return deleteStatus;
}

function DeleteSaleOrderHeader(saleOrderId) {
    var deleteFlag = 0;
    $.ajax({
        type: "POST",
        url: "/saleOrder/DeleteSaleOrderHeader",
        data: { saleOrderId },
        dataType: "json",
        async: false,
        success: function (statusCheck) {
            debugger;
           
            if (statusCheck.length > 0) {
                deleteFlag = 1;
            }
            else {
                deleteFlag = 0;
            }
        },
    });
    return deleteFlag;
}

function isNumberKeyWithDot(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if ((charCode < 48 || charCode > 57) && (charCode != 8) && (charCode != 46))
        return false;
    return true;
}