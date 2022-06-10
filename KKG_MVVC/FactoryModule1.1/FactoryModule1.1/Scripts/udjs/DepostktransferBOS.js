var MENUID
var CHECKER;
var DEPOTID;
var FINYEAR;
var UserID;
var TPU;
var usertype;
$(document).ready(function () {

    var qs = getQueryStrings();
    if (qs["CHECKER"] != undefined && qs["CHECKER"] != "") {

        CHECKER = qs["CHECKER"];
    }
    if (qs["MENUID"] != undefined && qs["MENUID"] != "") {

        MENUID = qs["MENUID"];
    }
    if (qs["DEPOTID"] != undefined && qs["DEPOTID"] != "") {

        DEPOTID = qs["DEPOTID"];
    }
    FINYEAR = qs["FINYEAR"];
    UserID = qs["USERID"];
    $('#divTransferNo').css("display", "none");
    $('#dvAdd').css("display", "none");
    $('#dvDisplay').css("display", "");
    $('#export').css("display", "none");

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
    $("#hdntaxcount").val('0');
    finyrchk();
    bindsourceDepot();
    bindstocktransfer();
    $("#btnAddnew").click(function (e) {
        $('#dvAdd').css("display", "");
        $('#dvDisplay').css("dis play", "none");
        Addnew();
    })
    $("#btnclose").click(function (e) {
        $('#dvAdd').css("display", "none");
        $('#dvDisplay').css("display", "");
    })
    $('#ddltodepot').change(function () {
        var todepoid = $("#ddltodepot").val();
        var waybillapp = $("#ddlwaybillapplicale").val();
        ShippingAddress();
        DeliveryDay();
        if (todepoid == '0') {
            if (waybillapp == '0') {

                $("#ddlwaybill").empty().append('<option selected="selected" value="0">Not Applicable </option>');
            }
            else
                if (waybillapp != '0') {
                    $("#ddlwaybill").empty().append('<option selected="selected" value="0">select Ewaybill Key </option>');
                }
        }
        else
            if (todepoid != '0') {
             //   bindtaxcount();
                if (waybillapp == '0') {

                    $("#ddlwaybill").empty().append('<option selected="selected" value="0">Not Applicable </option>');
                }
                else
                    if (waybillapp != '0') {
                        Bindewaybill();
                    }
            }

    });
    $('#ddlwaybillapplicale').change(function () {
        var waybillapp = $("#ddlwaybillapplicale").val();
        if (waybillapp == '0') {

            $("#ddlwaybill").empty().append('<option selected="selected" value="0">Not Applicable </option>');
            $("#ddlwaybill").prop("disabled", true);
            $("#ddlwaybill").chosen({
                search_contains: true
            });
            $("#ddlwaybill").trigger("chosen:updated");
        }
        else
            if (waybillapp != '0') {
                Bindewaybill();
            }
    });
    $('#ddlinsurancecompname').change(function () {
        var companyid = $("#ddlinsurancecompname").val();
        if (companyid != '0') {
            BindSTNInsuranceNumber();
        }
    });
    $('#ddlOrderType').change(function () {
        $('#export').css("display", "");
        var ordertype = $("#ddlOrderType").val();
        if (ordertype == '2E96A0A4-6256-472C-BE4F-C59599C948B0') {
            bindCountry();
            $("#ddlSaleOrder").chosen({
                search_contains: true
            });
            $("#ddlSaleOrder").trigger("chosen:updated");
        }
        else {
            $('#export').css("display", "none");
            $("#ddlCountry").val('0');
            $("#ddlSaleOrder").empty().append('<option selected="selected" value="0">select Saleorder </option>');

            $("#ddlCountry").chosen({
                search_contains: true
            });
            $("#ddlCountry").trigger("chosen:updated");
            $("#ddlSaleOrder").chosen({
                search_contains: true
            });
            $("#ddlSaleOrder").trigger("chosen:updated");
        }
    });
    $('#ddlCountry').change(function () {
        Saleorder();
    });
    $('#ddlcategory').change(function () {
        var category = $("#ddlcategory").val();
        if (category != '0') {
            LoadCategoryWiseProduct();

            $("#ddlproductname").chosen({
                search_contains: true
            });
            $("#ddlproductname").trigger("chosen:updated");
        }

        else
            if (category == '0') {
                bindallproduct();
                $("#ddlproductname").chosen({
                    search_contains: true
                });
                $("#ddlproductname").trigger("chosen:updated");
            }
    });
    $('#ddlproductname').change(function () {
        var productid = $("#ddlproductname").val();


        if (productid == '0') {

            $("#ddlpackingsize").empty().append('<option selected="selected" value="0">select </option>');
            $("#ddlpackingsize").chosen({
                search_contains: true
            });
            $("#ddlpackingsize").trigger("chosen:updated");
        }
        if (productid != '0') {
            //Bindpacksize();
            ProductTypeChecking();
        }
    })
    $('#ddlbatchno').change(function () {
        getBatchDetails();
    })

    $("#btnADDGrid").click(function (e) {
        var todepotid = $("#ddltodepot").val();
        var productid = $("#ddlproductname").val();
        var productnmame = $('#ddlproductname').find('option:selected').text();
        var packsizeid = $("#ddlpackingsize").val();
        var packsizename = $('#ddlpackingsize').find('option:selected').text();
        var transferqty = 0;
        var stkqty = 0;
        var remainqty = 0;
        if ($("#txttransferqty").val() == '') {
            toastr.warning('<b><font color=black>Provide Transfer Qty..!</font></b>');
            return false;
        }
        stkqty = parseFloat($("#txtstockqty").val());
        transferqty = parseFloat($("#txttransferqty").val());
        remainqty = stkqty - transferqty;
        if (todepotid == '0') {
            toastr.warning('<b><font color=black>Please select Depot..!</font></b>');
            return false;
        }
        if (transferqty == 0) {
            toastr.warning('<b><font color=black>Provide transfer qty</font></b>');
            return false;
        }
        if (remainqty >= 0 && transferqty != 0) {
            addProduct();
        }
        else {
            toastr.warning('<b><font color=black>Stock not available, please check!</font></b>');
            return false;
        }

    });
    //delete gtom detail grid
    $("body").on("click", "#productDetailsGrid .gvTempDelete", function () {
        var grdproductid;
        var grdbatchno;
        var deleteflag = 0;
        var row = $(this).closest("tr");
        grdproductid = row.find('td:eq(1)').html();
        grdbatchno = row.find('td:eq(5)').html();
        if (confirm("Do you want to delete this item?")) {
            deleteflag = 1;
            row.remove();
            RowCount();
            CalculateAmount();
            deleteRowfromTaxTable(grdproductid, grdbatchno);
        }
        CalculateQuantity();
        var rowCount = document.getElementById("productDetailsGrid").rows.length - 1;

        if (rowCount > 0) {
            $("#ddltodepot").prop("disabled", true);
            $("#ddltodepot").chosen({
                search_contains: true
            });
            $("#ddltodepot").trigger("chosen:updated");
        }
        else {
            $('#productDetailsGrid').empty();
            $("#ddltodepot").prop("disabled", false);
            $("#ddltodepot").chosen({
                search_contains: true
            });
            $("#ddltodepot").trigger("chosen:updated");
        }

    })
    //save data
    $("#btnSave").click(function (e) {

        if ($("#ddltodepot").val() == '0') {
            toastr.warning('<b><font color=black>Please select destination depot..!</font></b>');
            return false;
        }
        else
            if ($("#ddlmodetransport").val() == '0') {
                toastr.warning('<b><font color=black>Please select Mode of Transport..!</font></b>');
                return false;
            }
            else
                if ($("#ddltranspoter").val() == '0') {
                    toastr.warning('<b><font color=black>Please select Transporter..!</font></b>');
                    return false;
                }
                else
                    if ($("#txtdeliverydate").val() == '') {
                        toastr.warning('<b><font color=black>Please select Delivery Date..!</font></b>');
                        return false;
                    }


                    else {



                        Savestktransfer();
                    }
    })
    $("#btnclose").click(function (e) {
        ClearControls();
    })
    $("#btnsearch").click(function (e) {
        bindstocktransfer();
    })
    $("body").on("click", "#dispatchGrid .gvPrint", function () {
        var row = $(this).closest("tr");
        dispatchid = row.find('td:eq(0)').html();
        $('#hdndispatchID').val(dispatchid);
        var url = "http://mcnroeerp.com/mcworld/view/frmPrintPopUpv2.aspx?Stnid=" + dispatchid + "&BSID=1&pid=1&MenuId=" + MENUID;
        window.open(url, "", "width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100");

    })
    $("body").on("click", "#dispatchGrid .gvCancel", function () {
        var row = $(this).closest("tr");
        dispatchid = row.find('td:eq(0)').html();
        var isverified;
        var dayend;
        isverified = row.find('td:eq(10)').html();
        dayend = row.find('td:eq(13)').html();
        if (isverified == 'Y') {
            toastr.warning('<b>Already Approved...!</b>');
            return false;
        }
        if (dayend == 'DONE') {
            toastr.warning('<b>Day end is already done...!</b>');
            return false;
        }
        $('#hdntransferid').val(dispatchid);
        if (confirm("Do you want to delete this item?")) {
            if (CHECKER == 'FALSE') {
                // var receivedstatus = ReceivedStatus($('#hdndispatchID').val().trim());
                //var syncstatus = SyncStatus($('#hdndispatchID').val().trim());
                // var financestatus = FinanceStatus($('#hdndispatchID').val().trim());


                DeleteDispatch(dispatchid);
                bindstocktransfer();

            }
            else {
                toastr.warning('<b>not allow to cancel...!</b>');
            }
        }

    })

    $("body").on("click", "#dispatchGrid .gvView", function () {
        var row = $(this).closest("tr");
        var dispatchid = row.find('td:eq(0)').html();
        Confirmdispatch(dispatchid);
    })

    $("body").on("click", "#dispatchGrid .gvEdit", function () {
        var dispatchid;
        var isverified;
        var dayend;
        var row = $(this).closest("tr");
        dispatchid = row.find('td:eq(0)').html();
        isverified = row.find('td:eq(10)').html();
        dayend = row.find('td:eq(13)').html();
        if (dayend == 'DONE') {
            toastr.warning('<b>Day End is already done...!</b>');
            return false;
        }
        if (isverified == 'Y') {
            toastr.warning('<b>Stock Transfer is already approveed...!</b>');
            return false;
        }
        $('#productDetailsGrid').empty();
        $('#hdntransferid').val(dispatchid);
        $('#divTransferNo').css("display", "");
        $('#dvAdd').css("display", "");
        $('#dvDisplay').css("display", "none");
        $("#ddlfromdepot").prop("disabled", true);

        $("#ddlfromdepot").chosen({
            search_contains: true
        });
        $("#ddlfromdepot").trigger("chosen:updated");
        $("#ddltodepot").chosen({
            search_contains: true
        });
        $("#ddltodepot").trigger("chosen:updated");
        $("#ddltranspoter").chosen({
            search_contains: true
        });
        $("#ddltranspoter").trigger("chosen:updated");
        $("#ddlOrderType").chosen({
            search_contains: true
        });
        $("#ddlOrderType").trigger("chosen:updated");
        $("#ddlOrderType").chosen({
            search_contains: true
        });
        $("#ddlOrderType").trigger("chosen:updated");
        $("#ddlcategory").chosen({
            search_contains: true
        });
        $("#ddlcategory").trigger("chosen:updated");
        $("#ddlproductname").chosen({
            search_contains: true
        });
        $("#ddlproductname").trigger("chosen:updated");
        $("#ddlinsurancecompname").chosen({
            search_contains: true
        });
        $("#ddlinsurancecompname").trigger("chosen:updated");
        //$("#ddlinsuranceno").chosen({
        //    search_contains: true
        //});
        //$("#ddlinsuranceno").trigger("chosen:updated");

        $("#ddlinsurancecompname").trigger("chosen:updated");
        $("#ddlpackingsize").chosen({
            search_contains: true
        });
        $("#ddlpackingsize").trigger("chosen:updated");
        //$("#ddlbatchno").chosen({
        //    search_contains: true
        //});
        //$("#ddlbatchno").trigger("chosen:updated");
        $("#ddlwaybillapplicale").chosen({
            search_contains: true
        });
        $("#ddlwaybillapplicale").trigger("chosen:updated");
        $("#ddlmodetransport").chosen({
            search_contains: true
        });
        $("#ddlmodetransport").trigger("chosen:updated");
        $("#ddlwaybill").chosen({
            search_contains: true
        });
        $("#ddlwaybill").trigger("chosen:updated");
        ReleaseSession();
        $("#txttransferdate").datepicker("destroy");
        editdtl(dispatchid);
    })

})
//for view
$("body").on("click", "#dispatchGrid .gvview1", function () {
    var dispatchid;
    var isverified;
    var dayend;
    var row = $(this).closest("tr");
    dispatchid = row.find('td:eq(0)').html();
    $('#productDetailsGrid').empty();
    $('#hdntransferid').val(dispatchid);
    $('#divTransferNo').css("display", "");
    $('#dvAdd').css("display", "");
    $("#dvAdd").find("input, textarea, select,submit").attr("disabled", "disabled");
    $("#btnclose").prop("disabled", false);
    $('#dvDisplay').css("display", "none");
    $("#ddlfromdepot").prop("disabled", true);

    $("#ddlfromdepot").chosen({
        search_contains: true
    });
    $("#ddlfromdepot").trigger("chosen:updated");
    $("#ddltodepot").chosen({
        search_contains: true
    });
    $("#ddltodepot").trigger("chosen:updated");
    $("#ddltranspoter").chosen({
        search_contains: true
    });
    $("#ddltranspoter").trigger("chosen:updated");
    $("#ddlOrderType").chosen({
        search_contains: true
    });
    $("#ddlOrderType").trigger("chosen:updated");
    $("#ddlOrderType").chosen({
        search_contains: true
    });
    $("#ddlOrderType").trigger("chosen:updated");
    $("#ddlcategory").chosen({
        search_contains: true
    });
    $("#ddlcategory").trigger("chosen:updated");
    $("#ddlproductname").chosen({
        search_contains: true
    });
    $("#ddlproductname").trigger("chosen:updated");
    $("#ddlinsurancecompname").chosen({
        search_contains: true
    });
    $("#ddlinsurancecompname").trigger("chosen:updated");
    //$("#ddlinsuranceno").chosen({
    //    search_contains: true
    //});
    //$("#ddlinsuranceno").trigger("chosen:updated");

    $("#ddlinsurancecompname").trigger("chosen:updated");
    $("#ddlpackingsize").chosen({
        search_contains: true
    });
    $("#ddlpackingsize").trigger("chosen:updated");
    //$("#ddlbatchno").chosen({
    //    search_contains: true
    //});
    //$("#ddlbatchno").trigger("chosen:updated");
    $("#ddlwaybillapplicale").chosen({
        search_contains: true
    });
    $("#ddlwaybillapplicale").trigger("chosen:updated");
    $("#ddlmodetransport").chosen({
        search_contains: true
    });
    $("#ddlmodetransport").trigger("chosen:updated");
    $("#ddlwaybill").chosen({
        search_contains: true
    });
    $("#ddlwaybill").trigger("chosen:updated");
    ReleaseSession();
    $("#txttransferdate").datepicker("destroy");
    editdtl(dispatchid);
})


function Confirmdispatch(dispatchid) {
    debugger;

    $.ajax({
        type: "POST",
        url: "/TranDepotStock/Confirmdispatch",
        data: { DispatchID: dispatchid },
        dataType: "json",
        async: false,
        success: function (response) {
            var messageid = 0;
            var messagetext = '';
            $.each(response, function (key, item) {
                messageid = item.MessageID;
                messagetext = item.MessageText;
            });
            if (messageid == '0') {
                toastr.success('<b><font color=black>' + messagetext + '</font></b>');
                bindfgdispatchgrid();
               
            }
            else {
                toastr.info('<b><font color=black>' + messagetext + '</font></b>');
            }
        }
    });
}
function editdtl(dispatchid) {
    $("#ddlwaybill").empty().append('<option selected="selected" value="0">Select Ewaybill Key </option>');
    $("#ddlwaybill").chosen({
        search_contains: true
    });
    $("#ddlwaybill").trigger("chosen:updated");
    var tr;
    var HeaderCount = 0;
    var srl = 0;
    srl = srl + 1;
    var taxid = '';
    var TaxPercentage = 0;
    var TaxAmount = 0;
    var NetAmount = 0;
    var PolicyNo = '';
    //   $("#productDetailsGrid").empty();
    $.ajax({
        type: "POST",
        url: "/TranDepotStock/Stocktransferedit",
        data: { DispatchID: dispatchid },
        async: false,
        dataType: "json",

        success: function (response) {
            //alert(JSON.stringify(response))
            var listHeader = response.alleditDataset.varHeader;
            var listDetails = response.alleditDataset.varDetails;
            var listTaxcount = response.alleditDataset.varTaxCount;
            var listTax = response.alleditDataset.varTax;
            var listFooter = response.alleditDataset.varFooter;
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
            $("#hdntaxcount").val('0');
            //header
            $.each(listHeader, function (index, record) {

                $("#txttransferno").val(this['STOCKTRANSFERNO']);
                $("#txttransferdate").val(this['STOCKTRANSFERDATE']);
                $("#txtvehicleno").val(this['VEHICHLENO']);
                $("#txtlrgrno").val(this['LRGRNO']);
                $("#txtlrgrdate").val(this['LRGRDATE']);

                $("#txtchallanno").val(this['CHALLANNO']);
                $("#txtchallandate").val(this['CHALLANDATE']);
                $("#txtRemarks").val(this['REMARKS']);
                $("#txtCheckerNote").val(this['NOTE']);
                $("#txtgatepassno").val(this['GATEPASSNO']);
                $("#txtgatepassdate").val(this['GATEPASSDATE']);
                $("#txtTotalCase").val(this['TOTALCASE']);
                $("#txtTotalPCS").val(this['TOTALPCS']);
                $("#txtShippingAddress").val(this['SHIPPINGADDRESS']);


                $("#txtdeliverydate").val(this['DELIVERYDATE']);
                $("#ddlwaybillapplicale").val(this['WAYBILLNOAPPLICABLE']);
                $("#ddltodepot").val(this['TODEPOTID']);
                $("#ddltodepot").prop("disabled", true);
                $("#ddltodepot").chosen({
                    search_contains: true
                });
                $("#ddltodepot").trigger("chosen:updated");
                $("#ddlwaybill").val(this['WAYBILLKEY']);
                $("#ddlwaybill").chosen({
                    search_contains: true
                });
                $("#ddlwaybill").trigger("chosen:updated");
                $("#ddltranspoter").val(this['TRANSPORTERID']);
                $("#ddltranspoter").chosen({
                    search_contains: true
                });
                $("#ddltranspoter").trigger("chosen:updated");
                $("#ddlmodetransport").val(this['MODEOFTRANSPORT']);
                $("#ddlmodetransport").chosen({
                    search_contains: true
                });
                $("#ddlmodetransport").trigger("chosen:updated");
                $("#ddlinsurancecompname").val(this['INSURANCECOMPID']);
                $("#ddlinsurancecompname").chosen({
                    search_contains: true
                });
                $("#ddlinsurancecompname").trigger("chosen:updated");
                BindSTNInsuranceNumber();
                $("#ddlinsuranceno").val(this['INSURANCENO']);
                //$("#ddlinsuranceno").chosen({
                //    search_contains: true
                //});
                //$("#ddlinsuranceno").trigger("chosen:updated");
                //if (this['EXPORT'].toString().trim() == 'Y') {
                //    bindCountry();
                //    $("#ddlCountry").val(this['COUNTRYID']);
                //    $('#export').css("display", "");
                //}
            });
          
            $.each(listFooter, function (index, record) {

                $("#txtTotalGross").val(this['GROSSAMOUNT']);
                $("#txtnetamt").val(this['NETAMOUNT']);
                $("#txtRoundoff").val(this['ROUNDOFFVALUE']);
                $("#txttaxamt").val(this['TOTALTAXAMT']);
            });
            //tax detail
            if (listDetails.length > 0) {

                $("#productDetailsGrid").empty();
                if ($("#hdntaxcount").val().trim() == '1') {
                    tr = $('#productDetailsGrid');
                    HeaderCount = $('#productDetailsGrid thead th').length;
                    if (HeaderCount == 0) {
                        tr.append("<thead><tr><th>Sl.No.</th><th style='display: none'>Product ID</th><th>HSN Code</th><th>Product</th><th>Batch</th><th style='display: none'>PackSize ID</th><th>Pack Size</th><th>Qty</th><th>MRP</th><th>Rate</th><th>Amount</th><th style='display: none'>MRP</th><th style='display: none'>Assesmet(%)</th><th style='display: none'>Assesment Amt.</th><th style='display: none'>Net Wght.</th><th >Gross Wght.</th><th>Mfg.Date</th><th>Exp.Date</th><th style='display: none'>Tag</th><th style='display: none'>Before Edited Qty</th><th>IGST(%)</th><th>IGST</th><th>Net Amt.</th><th>Delete</th></tr></thead><tbody>");

                    }
                    $.each(listDetails, function (index, record) {
                        debugger;
                        tr = $('<tr/>');
                        tr.append("<td style='text-align: center'>" + srl + "</td>");//0
                        tr.append("<td style='display: none'>" + this['PRODUCTID'] + "</td>");//1
                        tr.append("<td style='text-align: center'>" + this['HSNCODE'] + "</td>");//2
                        tr.append("<td style='width:250px'>" + this['PRODUCTNAME'] + "</td>");//3
                        tr.append("<td style='text-align: center'>" + this['BATCHNO'] + "</td>");//4
                        tr.append("<td style='display: none'>" + this['PACKINGSIZEID'] + "</td>");//5
                        tr.append("<td>" + this['PACKINGSIZENAME'] + "</td>");//6
                        if (this['PACKINGSIZEID'].toString().trim() == '1970C78A-D062-4FE9-85C2-3E12490463AF') {
                            tr.append("<td style='text-align: right'>" + parseFloat(this['TRANSFERQTY']).toFixed(3) + "</td>");//7
                        }
                        else if (this['PACKINGSIZEID'].toString().trim() == 'B9F29D12-DE94-40F1-A668-C79BF1BF4425') {
                            tr.append("<td style='text-align: right'>" + parseFloat(this['TRANSFERQTY']).toFixed(0) + "</td>");//7
                        }
                        else {
                            tr.append("<td style='text-align: right'>" + parseFloat(this['TRANSFERQTY']).toFixed(3) + "</td>");//7
                        }
                        tr.append("<td style='text-align: right'>" + this['MRP'] + "</td>");//8

                        tr.append("<td style='text-align: right'>" + parseFloat(this['RATE']).toFixed(2) + "</td>");//9
                        tr.append("<td style='text-align: right'>" + parseFloat(this['AMOUNT']).toFixed(2) + "</td>");//10
                        tr.append("<td style='text-align: right;display: none'>" + this['TOTALMRP'] + "</td>");//8
                        tr.append("<td style='display: none'>" + this['ASSESMENTPERCENTAGE'] + "</td>");//11
                        tr.append("<td style='display: none'>" + parseFloat(this['TOTALASSESMENTVALUE']).toFixed(2) + "</td>");//12
                        tr.append("<td style='display: none'>" + this['NETWEIGHT'] + "</td>");//13
                        tr.append("<td>" + this['GROSSWEIGHT'] + "</td>");//14
                        tr.append("<td style='text-align: center'>" + this['MFDATE'] + "</td>");//15
                        tr.append("<td style='text-align: center'>" + this['EXPRDATE'] + "</td>");//16
                        tr.append("<td style='display: none'>" + this['TAG'] + "</td>");//17
                        tr.append("<td style='display: none'>" + this['BEFOREEDITEDQTY'] + "</td>");//18
                        TaxPercentage = GetTaxOnEdit(dispatchid, taxid, this['PRODUCTID'], this['BATCHNO'].toString().trim());
                        TaxAmount = ((parseFloat(this['AMOUNT']) * TaxPercentage) / 100);

                        NetAmount = (parseFloat(this['AMOUNT']) + TaxAmount);

                        
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
                        tr.append("<thead><tr><th>Sl.No.</th><th style='display: none'>Product ID</th><th>HSN Code</th><th>Product</th><th>Batch</th><th style='display: none'>PackSize ID</th><th>Pack Size</th><th>Qty</th><th>MRP</th><th>Rate</th><th>Amount</th><th style='display: none'>Tot MRP(%)</th><th style='display: none'>Assesmet(%)</th><th style='display: none'>Assesment Amt.</th><th style='display: none'>Net Wght.</th><th>Gross Wght.</th><th>Mfg.Date</th><th>Exp.Date</th><th style='display: none'>Tag</th><th style='display: none'>Before Edited Qty</th><th>Net Amt.</th><th>Delete</th></tr></thead><tbody>");
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
                        tr.append("<td style='text-align: right;display: none'>" + this['TOTALMRP'] + "</td>");//8
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
            //for tax on edit
            if ($("#hdntaxcount").val() == '1') {
                $.each(listTax, function (index, record) {
                    addRowinTaxTableEdit(this['PRODUCTID'], this['BATCHNO'], this['TAXID'], this['PERCENTAGE'], this['TAXVALUE'], this['MRP'], '1');
                });
            }
            else {
                addRowinTaxTableEdit('NA', 'NA', 'NA', 0, 0, 0, '0');
            }

        }
    })

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

function GetTaxOnEdit(dispatchid, taxid, productid, batchno) {

    var returnValue = null;
    $.ajax({
        type: "POST",
        url: "/TranDepotStock/GetHSNTaxOnEdit",
        data: { transferid: dispatchid, taxid: taxid, productid: productid, batchno: batchno },
        dataType: "json",
        async: false,
        success: function (response) {
            var taxpercentage = 0;
            $.each(response, function () {
                taxpercentage = this["TAXPERCENTAGE"];
                returnValue = taxpercentage;
                return false;
            });
        }
    });
    return returnValue;
}



function DeleteDispatch(dispatchid) {
    debugger;

    $.ajax({
        type: "POST",
        url: "/TranDepotStock/deletedispatch",
        data: { DispatchID: dispatchid },
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
function bindstocktransfer() {

    var frmdate = $('#txtfrmdate').val();
    var todate = $('#txttodate').val();
    var srl = 0;
    $.ajax({
        type: "POST",
        url: "/TranDepotStock/Bindstocktransferbos",
        data: { FromDate: frmdate, ToDate: todate, CheckerFlag: CHECKER, depotID: DEPOTID, type: 'FG', finyr: FINYEAR, userid: UserID },
        dataType: "json",
        success: function (response) {
            var tableNew = '<table id="dispatchGrid" class="table table-striped table-bordered dt-responsive nowrap" style="width:100%">';
            tableNew = tableNew + "<thead><tr><th style='display: none'>TRANSFERID</th><th>Sl. No.</th><th>Date</th><th>Transfer No.</th><th >Transfer to</th><th style='display: none'>WAYBILL NO</th><th style='display: none'>FFORMDATE</th><th style='display: none'> FFORM NO</th><th style='display: none'>FORMREQ</th><th style='display: none'>WAYBILLKEY</th><th style='display: none'>ISVERIFIED</th><th>Financial Status</th><th >Dayend</th><th style='display: none'>MOTHERDEPOTID</th><th style='display: none'>TODEPOTID</th><th>Total Case</th> <th>Total Pcs</th><th>Net Amount</th><th>Entry By</th><th>Approval Person</th><th style='display: none'>Type</th><th>Confirm</th><th>Print</th><th>Edit</th><th>View</th><th>Cancel</th</tr></thead><tbody>";

            
            for (var i = 0; i < response.length; i++) {
                srl = srl + 1;
                tableNew = tableNew + " <tr>";
                tableNew = tableNew +"<td style='display: none'>" + response[i].STOCKTRANSFERID + "</td>";
                tableNew = tableNew + "<td style='text-align: center'>" + srl + "</td>";
                tableNew = tableNew +"<td >" + response[i].STOCKTRANSFERDATE + "</td>";
                tableNew = tableNew +"<td>" + response[i].STOCKTRANSFERNO + "</td>";
                tableNew = tableNew +"<td>" + response[i].TODEPOTNAME + "</td>";
                tableNew = tableNew +"<td style='display: none'>" + response[i].WAYBILLNO + "</td>";
                tableNew = tableNew +"<td style='display: none'>" + response[i].FFORMDATE + "</td>";
    tableNew = tableNew +"<td style='display: none'>" + response[i].FFORMNO + "</td>";
    tableNew = tableNew +"<td style='display: none'>" + response[i].FORMREQUIRED + "</td>";
    tableNew = tableNew +"<td style='display: none'>" + response[i].WAYBILLKEY + "</td>";
    tableNew = tableNew +"<td style='display: none'>" + response[i].ISVERIFIED + "</td>";
    tableNew = tableNew +"<td style='display: none'>" + response[i].ISVERIFIED + "</td>";
    tableNew = tableNew +"<td>" + response[i].ISVERIFIEDDESC + "</td>";

    tableNew = tableNew +"<td >" + response[i].DAYENDTAG + "</td>";
    tableNew = tableNew +"<td style='display: none'>" + response[i].MOTHERDEPOTID + "</td>";
    tableNew = tableNew +"<td style='display: none'>" + response[i].TODEPOTID + "</td>";
    tableNew = tableNew +"<td>" + response[i].TOTALCASE + "</td>";
    tableNew = tableNew +"<td>" + response[i].TOTALPCS + "</td>";
    tableNew = tableNew +"<td>" + response[i].NETAMOUNT + "</td>";

    tableNew = tableNew +"<td>" + response[i].USERNAME + "</td>";
    tableNew = tableNew +"<td>" + response[i].APPROVAL_PERSON + "</td>";
    tableNew = tableNew +"<td style='display: none'>" + response[i].EXPORT + "</td>";
    tableNew = tableNew +"<td style='text-align: center'><input type='image' class='gvView'   id='btndispatchview'   <img src='../Images/success.png' width='14' height ='10' title='confirm'/></input></td>";
    tableNew = tableNew +"<td style='text-align: center'><input type='image' class='gvPrint'  id='btndispatchprint'  <img src='../Images/ico_Print.png' width='20' height ='20' title='Print'/></input></td>";
    tableNew = tableNew +"<td style='text-align: center'><input type='image' class='gvEdit'   id='btndispatchedit'   <img src='../Images/Pencil-icon.png' title='Edit'/></input></td>";
    tableNew = tableNew +"<td style='text-align: center'><input type='image' class='gvview1'   id='btndispatchedit1'   <img src='../Images/View.png' width='20' height ='20' title='view'/></input></td>";
    tableNew = tableNew +"<td style='text-align: center'><input type='image' class='gvCancel' id='btndispatchdelete' <img src='../Images/ico_delete_16.png' title='Cancel'/></input></td></tr>";

             
            }
            document.getElementById("dispatchdiv").innerHTML = tableNew + '</tbody></table>';
          //  RowCountDispatchList();
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
function bindstocktransfer1() {

    var frmdate = $('#txtfrmdate').val();
    var todate = $('#txttodate').val();
    var srl = 0;
    $.ajax({
        type: "POST",
        url: "/TranDepotStock/Bindstocktransferbos",
        data: { FromDate: frmdate, ToDate: todate, CheckerFlag: CHECKER, depotID: DEPOTID, type: 'FG', finyr: FINYEAR,userid: UserID },
        dataType: "json",
        success: function (response) {
            var tr;
            tr = $('#dispatchGrid');
            tr.append("<thead><tr><th style='display: none'>TRANSFERID</th><th>Sl. No.</th><th>Date</th><th>Transfer No.</th><th >Transfer to</th><th style='display: none'>WAYBILL NO</th><th style='display: none'>FFORMDATE</th><th style='display: none'> FFORM NO</th><th style='display: none'>FORMREQ</th><th style='display: none'>WAYBILLKEY</th><th style='display: none'>ISVERIFIED</th><th>Financial Status</th><th >Dayend</th><th style='display: none'>MOTHERDEPOTID</th><th style='display: none'>TODEPOTID</th><th>Total Case</th> <th>Total Pcs</th><th>Net Amount</th><th>Entry By</th><th>Approval Person</th><th style='display: none'>Type</th><th>Confirm</th><th>Print</th><th>Edit</th><th>View</th><th>Cancel</th</tr></thead>>");

            $('#dispatchGrid').DataTable().destroy();
            $("#dispatchGrid tbody tr").remove();
            for (var i = 0; i < response.length; i++) {
                srl = srl + 1;
                tr = $('<tr/>');
                tr.append("<td style='display: none'>" + response[i].STOCKTRANSFERID + "</td>");
                tr.append("<td style='text-align: center'>" + srl + "</td>");//0
                tr.append("<td >" + response[i].STOCKTRANSFERDATE + "</td>");
                tr.append("<td>" + response[i].STOCKTRANSFERNO + "</td>");
                tr.append("<td>" + response[i].TODEPOTNAME + "</td>");
                tr.append("<td style='display: none'>" + response[i].WAYBILLNO + "</td>");
                tr.append("<td style='display: none'>" + response[i].FFORMDATE + "</td>");
                tr.append("<td style='display: none'>" + response[i].FFORMNO + "</td>");
                tr.append("<td style='display: none'>" + response[i].FORMREQUIRED + "</td>");
                tr.append("<td style='display: none'>" + response[i].WAYBILLKEY + "</td>");
                tr.append("<td style='display: none'>" + response[i].ISVERIFIED + "</td>");
                tr.append("<td style='display: none'>" + response[i].ISVERIFIED + "</td>");
                tr.append("<td>" + response[i].ISVERIFIEDDESC + "</td>");

                tr.append("<td >" + response[i].DAYENDTAG + "</td>");
                tr.append("<td style='display: none'>" + response[i].MOTHERDEPOTID + "</td>");
                tr.append("<td style='display: none'>" + response[i].TODEPOTID + "</td>");
                tr.append("<td>" + response[i].TOTALCASE + "</td>");
                tr.append("<td>" + response[i].TOTALPCS + "</td>");
                tr.append("<td>" + response[i].NETAMOUNT + "</td>");

                tr.append("<td>" + response[i].USERNAME + "</td>");
                tr.append("<td>" + response[i].APPROVAL_PERSON + "</td>");
                tr.append("<td style='display: none'>" + response[i].EXPORT + "</td>");
                tr.append("<td style='text-align: center'><input type='image' class='gvView'   id='btndispatchview'   <img src='../Images/success.png' width='14' height ='10' title='confirm'/></input></td>");
                tr.append("<td style='text-align: center'><input type='image' class='gvPrint'  id='btndispatchprint'  <img src='../Images/ico_Print.png' width='20' height ='20' title='Print'/></input></td>");
                tr.append("<td style='text-align: center'><input type='image' class='gvEdit'   id='btndispatchedit'   <img src='../Images/Pencil-icon.png' title='Edit'/></input></td>");
                tr.append("<td style='text-align: center'><input type='image' class='gvview1'   id='btndispatchedit1'   <img src='../Images/View.png' width='20' height ='20' title='view'/></input></td>");
                tr.append("<td style='text-align: center'><input type='image' class='gvCancel' id='btndispatchdelete' <img src='../Images/ico_delete_16.png' title='Cancel'/></input></td>");

                $("#dispatchGrid").append(tr);
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
function Savestktransfer() {
    var countryid;
    var countryname;
    var saleorderid;
    var saleorderno;
    var totalcase = 0;
    var totalpcs = 0;
    var exporttag;
    var taxcount = '0';
    if ($('#ddlOrderType').val().trim() == '9A555D40-5E12-4F5C-8EE0-E085B5BAB169') {
        countryid = '8F35D2B4-417C-406A-AD7F-7CB34F4D0B35';
        countryname = 'INDIA';
        saleorderid = '0';
        saleorderno = 'NA';
        exporttag = 'N';
    }
    else {
        countryid = $('#ddlCountry').val().trim();
        countryname = $('#ddlCountry').find('option:selected').text();
        saleorderid = $('#ddlSaleOrder').val().trim()
        saleorderno = $('#ddlSaleOrder').find('option:selected').text();
        exporttag = 'Y';
    }

    if ($('#txtTotalCase').val() != '') {
        totalcase = parseFloat($('#txtTotalCase').val());
    }
    if ($('#txtTotalPCS').val() != '') {
        totalpcs = parseFloat($('#txtTotalPCS').val());
    }
    if ($('#hdntaxcount').val() != '0') {
        taxcount = '1';
    }
   
    Depotstocklist = new Array();
    freelist = new Array();
   
    var i = 0;
    var j = 0;
    var Depostockmodel = {};
    Depostockmodel.STOCKTRANSFERID = $('#hdntransferid').val().trim();
    if ($('#hdntransferid').val() == '0') {
        Depostockmodel.FLAG = 'A';
    }
    else {
        Depostockmodel.FLAG = 'U';
    }
    
    Depostockmodel.TRANSFERDATE = $('#txttransferdate').val().trim();
    Depostockmodel.MOTHERDEPOTID = $('#ddlfromdepot').val().trim();
    Depostockmodel.MOTHERDEPOTNAME = $('#ddlfromdepot').find('option:selected').text();
    Depostockmodel.TODEPOTID = $('#ddltodepot').val().trim();
    Depostockmodel.TODEPOTNAME = $('#ddltodepot').find('option:selected').text();
    Depostockmodel.WAYBILLNO = $('#ddlwaybill').val().trim();
    Depostockmodel.WAYBILLAPPLICABLE = $('#ddlwaybillapplicale').val().trim();
    Depostockmodel.INSURANCENO = $('#ddlinsuranceno').val().trim();
    Depostockmodel.TRANSPORTERID = $('#ddltranspoter').val().trim();
    Depostockmodel.TRANSPORTERNAME = $('#ddltranspoter').find('option:selected').text();
    Depostockmodel.MODEOFTRANSPORT = $('#ddlmodetransport').val().trim();
    Depostockmodel.VEHICLENO = $('#txtvehicleno').val().trim();
    Depostockmodel.LRGRNO = $('#txtlrgrno').val().trim();
    Depostockmodel.LRGRDATE = $('#txtlrgrdate').val().trim();
    Depostockmodel.CHALLANNO = $('#txtchallanno').val().trim();
    Depostockmodel.CHALLANDATE = $('#txtchallandate').val().trim();
    Depostockmodel.GATEPASSNO = $('#txtgatepassno').val().trim();
    Depostockmodel.GATEPASSDATE = $('#txtgatepassdate').val().trim();
    Depostockmodel.FFORM = 'N';
    Depostockmodel.INSURANCECOMPID = $('#ddlinsurancecompname').val().trim();
    Depostockmodel.INSURANCECOMPNAME = $('#ddlinsurancecompname').find('option:selected').text();
    Depostockmodel.REMARKS = $('#txtremarks').val().trim();
    Depostockmodel.MODULEID = MENUID;
    Depostockmodel.ORDERTYPE = $('#ddlOrderType').val().trim();
    Depostockmodel.ORDERTYPENAME = $('#ddlOrderType').find('option:selected').text();
    Depostockmodel.COUNTRYID = countryid;
    Depostockmodel.COUNTRYNAME = countryname;
    Depostockmodel.SALEORDERID = saleorderid;
    Depostockmodel.SALEORDERNO = saleorderno;
    Depostockmodel.TotalCase = totalcase;
    Depostockmodel.TotalPcs = totalpcs;
    Depostockmodel.GrossAmt = parseFloat($('#txtTotalGross').val().trim());
    Depostockmodel.NetAmt = parseFloat($('#txtnetamt').val().trim());
    Depostockmodel.TOTALTAXAMT = parseFloat($('#txttaxamt').val().trim());
    Depostockmodel.BASICAMT = parseFloat($('#txtbasicamt').val().trim());
    Depostockmodel.RoundOff = parseFloat($('#txtRoundoff').val().trim());
    Depostockmodel.INVOICETYPE = 9;
    Depostockmodel.EXPORT = exporttag;
    Depostockmodel.SHIPINGADDRESS = $('#txtShippingAddress').val().trim();
    Depostockmodel.DELIVERYDATE = $('#txtdeliverydate').val().trim();
    Depostockmodel.TAXCOUNT = '0';
    Depostockmodel.userid = UserID;
    Depostockmodel.Finyear = FINYEAR;
   
    $('#productDetailsGrid tbody tr').each(function () {

        //debugger
        var dispatchdetails = {};
        var productid = $(this).find('td:eq(1)').html().trim();
        var productname = $(this).find('td:eq(3)').html().trim();
        var packingsizeid = $(this).find('td:eq(5)').html().trim();
        var packingsizename = $(this).find('td:eq(6)').html().trim();
        var MRP = $(this).find('td:eq(8)').html().trim();
        var trnsferqty = $(this).find('td:eq(7)').html().trim();
        var rate = $(this).find('td:eq(9)').html().trim();
        var batchno = $(this).find('td:eq(4)').html().trim();




        var amount = $(this).find('td:eq(10)').html().trim();
        var weight = $(this).find('td:eq(14)').html().trim();
        var mfdate = $(this).find('td:eq(16)').html().trim();
        var exprdate = $(this).find('td:eq(17)').html().trim();


        var grossweight = $(this).find('td:eq(15)').html().trim();
        ;
        dispatchdetails.PRODUCTID = productid;
        dispatchdetails.PRODUCTNAME = productname;
        dispatchdetails.PACKINGSIZEID = packingsizeid;
        dispatchdetails.PACKINGSIZENAME = packingsizename;
        dispatchdetails.MRP = MRP;
        dispatchdetails.QTY = trnsferqty;
        dispatchdetails.RATE = rate;
        dispatchdetails.BATCHNO = batchno;
        dispatchdetails.AMOUNT = amount;

        dispatchdetails.WEIGHT = productid;
        dispatchdetails.MFDATE = mfdate;
        dispatchdetails.EXPRDATE = exprdate;
        dispatchdetails.NSR = 0;
        dispatchdetails.RATEDISC = 0;
        dispatchdetails.DISCVALUE = 0;
        dispatchdetails.QSH = '';
        dispatchdetails.QSGUID = grossweight;
        dispatchdetails.DISCPER = 0;
        dispatchdetails.DISCAMT = 0;
        dispatchdetails.PRICESCHEMEID = '';
        dispatchdetails.PERCENTAGE = 0;
        dispatchdetails.VALUE = 0;
        Depotstocklist[i++] = dispatchdetails;


    });
    
    Depostockmodel.Depotstockdtl = Depotstocklist;
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
    Depostockmodel.Freestock = freelist;
    $.ajax({

        url: "/TranDepotStock/DepotstocktranBOSsave",
        data: '{Depostockmodel:' + JSON.stringify(Depostockmodel) + '}',
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
                toastr.success('<b><font color=black>' + messagetext + '</font></b>');
                bindstocktransfer();
                ReleaseSession();
                clearcontrol();
               
              
                
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
function ClearControls() {

    $("#dvAdd").find("input, textarea, select,submit").removeAttr("disabled");
    $("#txtTotalGross").attr("disabled", "disabled");
    $("#txtRoundoff").attr("disabled", "disabled");
    $("#txtnetamt").attr("disabled", "disabled");
    $("#txtTotalPCS").attr("disabled", "disabled");
    $("#txtTotalCase").attr("disabled", "disabled");
    $("#txtbasicamt").attr("disabled", "disabled");
    $("#txttaxamt").attr("disabled", "disabled");
    $("#txttotal").attr("disabled", "disabled");
    $("#txtmrp").attr("disabled", "disabled");
    $("#txtstockqty").attr("disabled", "disabled");

    $('#dvAdd').css("display", "none");
    $('#dvDisplay').css("display", "");
    $('#divTransferNo').css("display", "none");
    $('#export').css("display", "none");
    $("#ddlwaybillapplicale").val('1');
    $("#ddlmodetransport").val('BY ROAD');
    $("#hdntaxcount").val('0');
    
    var category = $("#ddlcategory").val();
    if (category != '0') {
      
        bindallproduct();
        $("#ddlcategory").val('0');
        $("#ddlcategory").chosen({
            search_contains: true
        });
        $("#ddlcategory").trigger("chosen:updated");
       
    }
    else
        if (category == '0') {
            $("#ddlproductname").val('0');
            $("#ddlproductname").chosen({
                search_contains: true
            });
            $("#ddlproductname").trigger("chosen:updated");
            

          
        }
    $("#txtlrgrno").val('');
    $("#txtchallanno").val('');
    $("#txtlrgrdate").val('');
    $("#txtchallandate").val('');
    $("#txtgatepassno").val('');
    $("#txtgatepassdate").val('');
    $("#txtShippingAddress").val('');
    $("#txtdeliverydate").val('');
    $("#txtmrp").val('');
    $("#txtstockqty").val('');
    $("#txttransferqty").val('');
    $("#txtbasicamt").val('');
    $("#txttaxamt").val('');
    $("#txttotal").val('');
    $("#txtTotalGross").val('');
    $("#txtRoundoff").val('');
    $("#txtnetamt").val('');
    $("#txtTotalPCS").val('');
    $("#txtTotalCase").val('');
    $("#txtremarks").val('');
    $("#ddlSaleOrder").empty().append('<option selected="selected" value="0">select Saleorder </option>');
    $("#ddlCountry").empty().append('<option selected="selected" value="0">select Country </option>');
    $("#ddlpackingsize").empty().append('<option selected="selected" value="0">select  </option>');
    $("#ddlbatchno").empty().append('<option selected="selected" value="0">Select Batch No </option>');
    $("#ddltodepot").val('0');
    $("#ddlOrderType").val('9A555D40-5E12-4F5C-8EE0-E085B5BAB169');
    $("#ddltranspoter").val('0');
    $("#ddlinsurancecompname").val('0');
    $("#ddlinsuranceno").val('0');
   
   
    $("#ddltodepot").prop("disabled", false);
    $("#ddltodepot").chosen({
        search_contains: true
    });
    $("#ddltodepot").trigger("chosen:updated");
    $("#ddltranspoter").chosen({
        search_contains: true
    });
    $("#ddltranspoter").trigger("chosen:updated");
   
    $("#ddlOrderType").chosen({
        search_contains: true
    });
    $("#ddlOrderType").trigger("chosen:updated");
   
    
    $("#ddlinsurancecompname").chosen({
        search_contains: true
    });
    $("#ddlinsurancecompname").trigger("chosen:updated");
    //$("#ddlinsuranceno").chosen({
    //    search_contains: true
    //});
    //$("#ddlinsuranceno").trigger("chosen:updated");

  
    $("#ddlpackingsize").chosen({
        search_contains: true
    });
    $("#ddlpackingsize").trigger("chosen:updated");
    //$("#ddlbatchno").chosen({
    //    search_contains: true
    //});
    //$("#ddlbatchno").trigger("chosen:updated");
    $("#ddlwaybillapplicale").chosen({
        search_contains: true
    });
    $("#ddlwaybillapplicale").trigger("chosen:updated");
    $("#ddlmodetransport").chosen({
        search_contains: true
    });
    $("#ddlmodetransport").trigger("chosen:updated");
    $("#ddlwaybill").chosen({
        search_contains: true
    });
    $("#ddlwaybill").trigger("chosen:updated");
    $('#productDetailsGrid').empty();
    ReleaseSession();

}
// foir dublicate chk
function addProduct() {
    var isexists = 'n';
    if ($('#productDetailsGrid').length) {
        $("#productDetailsGrid tbody tr").each(function () {
            var prodid = $(this).find('td:eq(1)').html();
            var batch = $(this).find('td:eq(4)').html();
            if (prodid == $("#ddlproductname").val() && batch == $("#hdnbatchno").val()) {
                isexists = 'y';
                return false;
            }


        })
    }
    if (isexists == 'y') {
        toastr.error('Item already exists...!');
        return false;
    }
    else {
        addProductInDetailsGrid();
    }
}
function addProductInDetailsGrid() {
    var productid = $("#ddlproductname").val();
    var productnmame = $('#ddlproductname').find('option:selected').text();
    var packsizeid = $("#ddlpackingsize").val();
    var packsizename = $('#ddlpackingsize').find('option:selected').text();
    var transferqty = $("#txttransferqty").val();
    var stkqty = $("#txtstockqty").val();
    var batchno = $("#hdnbatchno").val();
    var assesment = '';
    var qty = '';
    var netweight = '';
    var rate = '';
    var grossweight = '';
    var hsn = '';
    var hsntax = '';
    var Amount = 0;
    var MRP = $('#txtmrp').val();
    var AssesmentPercentage = $("#hdn_ASSESMENTPERCENTAGE").val();
    var rate = $("#hdnrate").val();
    var Assesmentvalue = 0;
    var TaxPercentage = 0;
    var TaxAmount = 0;
    var NetAmount = 0;
    var totalmrp = 0;
    var Tag = "A";
    var BeforeEditedQty = 0;
    var srl = 0;
 
    srl = srl + 1;
    $.ajax({
        type: "POST",
        url: "/tranfac/GetCalculateAmtInPcs",
        data: { Productid: productid.trim(), PacksizeID: packsizeid, Qty: transferqty, Rate: parseFloat(rate), Assesment: AssesmentPercentage.trim(), TaxName: $('#hdntaxname').val().trim(), date: $('#txttransferdate').val().trim() },
        async: false,
        dataType: "json",
        success: function (response) {
           
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
                    hsntax = this['HSNTAX'].trim();
                });
            }
            if ($("#hdntaxcount").val().trim() == '1') {
                $.each(listHSNTaxID, function (index, record) {
                    $("#hsntaxid").val(this['TAXID'].trim());
                });
            }
            if ($("#hdntaxcount").val().trim() == '1') {
                Amount = parseFloat(rate) * parseFloat(qty);
                totalmrp = parseFloat(MRP) * parseFloat(qty);
                Assesmentvalue = parseFloat(assesment);
                TaxPercentage = parseFloat(hsntax);
                TaxAmount = ((Amount * TaxPercentage) / 100);
                NetAmount = Amount + TaxAmount;
                //Create Table 
                var tr;
                tr = $('#productDetailsGrid');
                var HeaderCount = $('#productDetailsGrid thead th').length;
                if (HeaderCount == 0) {
                    tr.append("<thead><tr><th>Sl.No.</th><th style='display: none'>Product ID</th><th>HSN Code</th><th>Product</th><th>Batch</th><th style='display: none'>PackSize ID</th><th>Pack Size</th><th>Qty</th><th>MRP</th><th>Rate</th><th>Amount</th><th style='display: none'>MRP</th><th style='display: none'>Assesmet(%)</th><th style='display: none'>Assesment Amt.</th><th style='display: none'>Net Wght.</th><th>Gross Wght.</th><th>Mfg.Date</th><th>Exp.Date</th><th style='display: none'>Tag</th><th style='display: none'>Before Edited Qty</th><th>IGST(%)</th><th>IGST</th><th>Net Amt.</th><th>Delete</th></tr></thead><tbody>");
                }
                tr = $('<tr/>');
                tr.append("<td style='text-align: center'>" + srl + "</td>");//0
                tr.append("<td style='display: none'>" + productid + "</td>");//1
                tr.append("<td style='text-align: center'>" + hsn + "</td>");//2
                tr.append("<td style='width:250px'>" + productnmame + "</td>");//3

                tr.append("<td style='text-align: center'>" + batchno + "</td>");//4
                tr.append("<td style='display: none'>" + packsizeid + "</td>");//5
                tr.append("<td>" + packsizename + "</td>");//6
                if (packsizeid.trim() == '1970C78A-D062-4FE9-85C2-3E12490463AF') {
                    tr.append("<td style='text-align: right'>" + parseFloat(transferqty.trim()).toFixed(3) + "</td>");//7
                }
                else if (packsizeid.trim() == 'B9F29D12-DE94-40F1-A668-C79BF1BF4425') {
                    tr.append("<td style='text-align: right'>" + parseFloat(transferqty.trim()).toFixed(0) + "</td>");//7
                }
                else {
                    tr.append("<td style='text-align: right'>" + parseFloat(transferqty.trim()).toFixed(3) + "</td>");//7
                }
                tr.append("<td style='text-align: right'>" + MRP + "</td>");//8
                tr.append("<td style='text-align: right'>" + rate + "</td>");//9
                tr.append("<td style='text-align: right'>" + Amount + "</td>");//9
                tr.append("<td style='display: none'>" + totalmrp.toFixed(2) + "</td>");
                tr.append("<td style='display: none'>" + AssesmentPercentage + "</td>");//11
                tr.append("<td style='display: none'>" + Assesmentvalue.toFixed(2) + "</td>");
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
                
                Amount = parseFloat($("#hdnrate").val()) * parseFloat(qty);
                totalmrp = parseFloat(MRP) * parseFloat(qty);
                Assesmentvalue = parseFloat(assesment);
                TaxPercentage = 0;
                TaxAmount = 0;
                NetAmount = Amount;
               
                tr = $('#productDetailsGrid');
                var HeaderCount = $('#productDetailsGrid thead th').length;
                if (HeaderCount == 0) {
                    tr.append("<thead><tr><th>Sl.No.</th><th style='display: none'>Product ID</th><th>HSN Code</th><th>Product</th><th>Batch</th><th style='display: none'>PackSize ID</th><th>Pack Size</th><th>Qty</th><th>MRP</th><th>Rate</th><th>Amount</th><th style='display: none'>Total Mrp</th><th style='display: none'>Assesmet(%)</th><th style='display: none'>Assesment Amt.</th><th style='display: none'>Net Wght.</th><th >Gross Wght.</th><th>Mfg.Date</th><th>Exp.Date</th><th style='display: none'>Tag</th><th style='display: none'>Before Edited Qty</th><th>Net Amt.</th><th>Delete</th></tr></thead><tbody>");
                }
                tr = $('<tr/>');
                tr.append("<td style='text-align: center'>" + srl + "</td>");//0
                tr.append("<td style='display: none'>" + productid + "</td>");//1
                tr.append("<td style='text-align: center'>" + hsn + "</td>");//2
                tr.append("<td style='width:250px'>" + productnmame + "</td>");//3

                tr.append("<td style='text-align: center'>" + batchno + "</td>");//4
                tr.append("<td style='display: none'>" + packsizeid + "</td>");//5
                tr.append("<td>" + packsizename + "</td>");//6
                if (packsizeid.trim() == '1970C78A-D062-4FE9-85C2-3E12490463AF') {
                    tr.append("<td style='text-align: right'>" + parseFloat(transferqty.trim()).toFixed(3) + "</td>");//7
                }
                else if (packsizeid.trim() == 'B9F29D12-DE94-40F1-A668-C79BF1BF4425') {
                    tr.append("<td style='text-align: right'>" + parseFloat(transferqty.trim()).toFixed(0) + "</td>");//7
                }
                else {
                    tr.append("<td style='text-align: right'>" + parseFloat(transferqty.trim()).toFixed(3) + "</td>");//7
                }
                tr.append("<td style='text-align: right'>" + MRP + "</td>");//8
                tr.append("<td style='text-align: right'>" + rate + "</td>");//9
                tr.append("<td style='text-align: right'>" + Amount + "</td>");//9
                tr.append("<td style='display: none'>" + totalmrp.toFixed(2) + "</td>");
                tr.append("<td style='display: none'>" + AssesmentPercentage + "</td>");//11
                tr.append("<td style='display: none'>" + Assesmentvalue.toFixed(2) + "</td>");
                tr.append("<td style='display: none'>" + netweight + "</td>");//13
                tr.append("<td >" + grossweight + "</td>");//14
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
}
function RowCount() {
    var table = document.getElementById("productDetailsGrid");
    var rowCount = document.getElementById("productDetailsGrid").rows.length - 1;
    if (rowCount > 0) {
        $("#ddltodepot").prop("disabled", true);
        $("#ddltodepot").chosen({
            search_contains: true
        });
        $("#ddltodepot").trigger("chosen:updated");
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
        
      
           
            $('#productDetailsGrid tbody tr').each(function () {
                totalbasicamt += parseFloat($(this).find('td:eq(10)').html().trim());
                totaltaxamt += parseFloat(0);
                totalbasicplustaxamt += parseFloat($(this).find('td:eq(20)').html().trim());
            });
        
        $('#txtbasicamt').val(totalbasicamt.toFixed(2));
        $('#txttaxamt').val(totaltaxamt.toFixed(2));
        $('#txttotal').val(totalbasicamt.toFixed(2));
        $('#txtTotalGross').val(totalbasicplustaxamt.toFixed(2));
        parts = totalbasicplustaxamt - Math.floor(totalbasicplustaxamt);
        decimalvalue = parts.toFixed(2);
        //change for round up
        //if (decimalvalue >= .50) {
        //    decimalvalue = 1 - decimalvalue;
        //    decimalvalue = decimalvalue.toFixed(2);

        //}
        //else {
        //    decimalvalue = -decimalvalue;
        //}
        if (decimalvalue > 0) {
            decimalvalue = 1 - decimalvalue;
            decimalvalue = decimalvalue.toFixed(2);
            finalamt = (totalbasicplustaxamt - parts) + 1;
        }
        else {

            finalamt = totalbasicplustaxamt;
        }
        //change for round up
      //  finalamt = Math.round(totalbasicplustaxamt);
      //  $("#txtnetamt").val(finalamt.toFixed(2));
        $("#txtnetamt").val(finalamt.toFixed(2));
        $("#txtRoundoff").val(decimalvalue);
    }
    else {
        $('#txtbasicamt').val(0);
        $('#txttaxamt').val(0);
        $('#txttotal').val(0);
        $('#txtTotalGross').val(0);
        $('#txtnetamt').val(0);
        $('#txtRoundoff').val(0);
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
                url: "/TranDepotStock/GetTotalQuantity",
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
                    $('#txtTotalCase').val(totalCase.toFixed(3));
                    $('#txtTotalPCS').val(totalPcs.toFixed(0));
                }
            });
        });


    }
    else {
        $('#TotalCase').val(0);
        $('#TotalPcs').val(0);
    }
}
function addRowinTaxTable(TaxPercentage, TaxAmount, MRP, TaxFlag) {
 
    $.ajax({
        type: "POST",
        url: "/TranDepotStock/FillTaxDatatablebos", 
        data: { Productid: $('#ddlproductname').val(), BatchNo: $("#hdnbatchno").val(), TaxID: $('#hsntaxid').val(), Percentage: parseFloat(TaxPercentage), TaxValue: parseFloat(TaxAmount), MRP: parseFloat(MRP), Flag: TaxFlag },
        async: false,
        dataType: "json",
        success: function (response) {

        }
    });
}
function addRowinTaxTableEdit(productid, batchno, taxid, TaxPercentage, TaxAmount, MRP, TaxFlag) {
    $.ajax({
        type: "POST",
        url: "/TranDepotStock/FillTaxDatatableEditbos",
        data: { Productid: productid, BatchNo: batchno, TaxID: taxid, Percentage: parseFloat(TaxPercentage), TaxValue: parseFloat(TaxAmount), MRP: parseFloat(MRP), Flag: TaxFlag },
        async: false,
        dataType: "json",
        success: function (response) {

        }
    });
}

function deleteRowfromTaxTable(grdproductid, grdbatchno) {
    $.ajax({
        type: "POST",
        url: "/TranDepotStock/DeleteTaxDatatablebos",
        data: { Productid: grdproductid.trim(), BatchNo: grdbatchno.trim() },
        dataType: "json",
        success: function (response) {

        }
    });
}

function clearafterAdd() {
    $('#txttransferqty').val('');
    $('#txtstockqty').val('');
    $('#txtmrp').val('');
    $('#hdn_ASSESMENTPERCENTAGE').val('');
    $('#hdn_mfgdate').val('');
    $('#hdn_exprdate').val('');
    $("#ddlbatchno").empty().append('<option selected="selected" value="0">select Batch No </option>');
    $('#ddlproductname').val('0');
  //  $("#ddlproductname").empty().append('<option selected="selected" value="0">select Product</option>');
    $("#ddlproductname").chosen({
        search_contains: true
    });
    $("#ddlproductname").trigger("chosen:updated");
}

function Addnew() {
    $('#hdntransferid').val('0');
    $('#dvAdd').css("display", "");
    $('#dvDisplay').css("display", "none");
    $('#divTransferNo').css("display", "none");
    $('#export').css("display", "none");
    $("#ddlwaybillapplicale").val('1');
    $("#ddlmodetransport").val('BY ROAD');
   // $("#ddlinsuranceno").empty().append('<option selected="selected" value="0">select Policy No </option>');
   // $("#ddlpackingsize").empty().append('<option selected="selected" value="0">select  </option>');
    $("#ddlbatchno").empty().append('<option selected="selected" value="0">select Batch No </option>');
    $("#ddlwaybill").empty().append('<option selected="selected" value="0">select Ewaybill Key </option>');
    $("#ddlSaleOrder").empty().append('<option selected="selected" value="0">select Saleorder </option>');
    $("#ddlfromdepot").prop("disabled", true);

    $("#ddlfromdepot").chosen({
        search_contains: true
    });
    $("#ddlfromdepot").trigger("chosen:updated");
    $("#ddltodepot").chosen({
        search_contains: true
    });
    $("#ddltodepot").trigger("chosen:updated");
    $("#ddltranspoter").chosen({
        search_contains: true
    });
    $("#ddltranspoter").trigger("chosen:updated");
    $("#ddlOrderType").chosen({
        search_contains: true
    });
    $("#ddlOrderType").trigger("chosen:updated");
    $("#ddlOrderType").chosen({
        search_contains: true
    });
    $("#ddlOrderType").trigger("chosen:updated");
    $("#ddlcategory").chosen({
        search_contains: true
    });
    $("#ddlcategory").trigger("chosen:updated");
    $("#ddlproductname").chosen({
        search_contains: true
    });
    $("#ddlproductname").trigger("chosen:updated");
    $("#ddlinsurancecompname").chosen({
        search_contains: true
    });
    $("#ddlinsurancecompname").trigger("chosen:updated");
    //$("#ddlinsuranceno").chosen({
    //    search_contains: true
    //});
    //$("#ddlinsuranceno").trigger("chosen:updated");

    $("#ddlinsurancecompname").trigger("chosen:updated");
    $("#ddlpackingsize").chosen({
        search_contains: true
    });
    $("#ddlpackingsize").trigger("chosen:updated");
    //$("#ddlbatchno").chosen({
    //    search_contains: true
    //});
    //$("#ddlbatchno").trigger("chosen:updated");
    $("#ddlwaybillapplicale").chosen({
        search_contains: true
    });
    $("#ddlwaybillapplicale").trigger("chosen:updated");
    $("#ddlmodetransport").chosen({
        search_contains: true
    });
    $("#ddlmodetransport").trigger("chosen:updated");
    $("#ddlwaybill").chosen({
        search_contains: true
    });
    $("#ddlwaybill").trigger("chosen:updated");
    ReleaseSession();
    finyrchk();


}
function bindtaxcount() {
    var fromdepot = $("#ddlfromdepot").val();
    var todepoid = $("#ddltodepot").val();
    var invoicedate = $("#txttransferdate").val();
    $.ajax({
        type: "POST",
        url: "/TranDepotStock/GetTaxcount",
        data: { MenuID: MENUID, Flag: '1', DepotID: fromdepot, ProductID: '0', CustomerID: todepoid, Date: invoicedate },
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

function ShippingAddress() {
    var todepoid = $("#ddltodepot").val();
    $.ajax({
        type: "POST",
        url: "/TranDepotStock/ShippingAddress",
        data: { depotid: todepoid },
        async: false,
        dataType: "json",
        success: function (response) {
            //alert(JSON.stringify(response));

            $.each(response, function () {

                $("#txtShippingAddress").val(this['DELIVERYADDRESS']);
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
function DeliveryDay() {
    var fromdepot = $("#ddlfromdepot").val();
    var todepoid = $("#ddltodepot").val();
    var invoicedate = $("#txttransferdate").val();
    $.ajax({
        type: "POST",
        url: "/TranDepotStock/DeliveryDay",
        data: { fromdepot: fromdepot, todepoid: todepoid, invoicedate: invoicedate },
        async: false,
        dataType: "json",
        success: function (response) {
            //alert(JSON.stringify(response));

            $.each(response, function () {

                $("#txtdeliverydate").val(this['TRANSIT_DAYS']);
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
function Bindewaybill() {
    var ddlwaybill = $("#ddlwaybill");
    var todepotid = $("#ddltodepot").val();
    var transferid;

    $.ajax({
        type: "POST",
        url: "/TranDepotStock/BindWayBillNo",
        data: { depotid: todepotid, transferid: transferid },
        async: false,
        dataType: "json",
        success: function (response) {
            //alert(JSON.stringify(response));
            ddlwaybill.empty().append('<option selected="selected" value="0">select Ewaybill Key</option>');
            $.each(response, function () {
                ddlwaybill.append($("<option></option>").val(this['WAYBILLNO']).html(this['WAYBILLID']));
            });
            $("#ddlwaybill").prop("disabled", false);
            $("#ddlwaybill").chosen({
                search_contains: true
            });
            $("#ddlwaybill").trigger("chosen:updated");
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
    var ddlfromdepot = $("#ddlfromdepot");
    $.ajax({
        type: "POST",
        url: "/TranDepotStock/Bindsourcedepot",
        data: { user: UserID },
        async: false,
        dataType: "json",

        success: function (response) {
            //alert(JSON.stringify(response));
            ddlfromdepot.empty();
            $.each(response, function () {
                ddlfromdepot.append($("<option></option>").val(this['BRID']).html(this['BRNAME']));
            });
            var sourceDepot = $('#ddlfromdepot').val();
            binddestinationDepot(sourceDepot);
            BindOrderType();
            BindCATEGORY();
            bindtransporter();
            Insurancecodepot();
            Waybillapplicable();
            Bindpacksize();
            Bindmodeoftransport();
            bindallproduct();

        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}
function Waybillapplicable() {
    var listItems = "<option value='1'>Yes</option>";
    listItems += "<option value='0'>No</option>";


    $("#ddlwaybillapplicale").html(listItems);

}

function Bindmodeoftransport() {
    var listItems = "<option value='BY ROAD'>BY ROAD</option>";

    listItems += "<option value='BY AIR'>BY AIR</option>";
    listItems += "<option value='BY RAI'>BY RAI</option>";
    listItems += "<option value='BY SHIP'>BY SHIP</option>";


    $("#ddlmodetransport").html(listItems);

}

function binddestinationDepot(sourceDepot) {
    var ddltodepot = $("#ddltodepot");
    $.ajax({
        type: "POST",
        url: "/TranDepotStock/BindToDepo",
        data: { depotid: sourceDepot },
        async: false,
        dataType: "json",
        success: function (response) {
            //alert(JSON.stringify(response));
            ddltodepot.empty().append('<option selected="selected" value="0">select Depot</option>');
            $.each(response, function () {
                ddltodepot.append($("<option></option>").val(this['BRID']).html(this['BRNAME']));
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
function bindtransporter() {
    var ddltranspoter = $("#ddltranspoter");
    $.ajax({
        type: "POST",
        url: "/TranDepotStock/BindTranspoter",
        data: { depotid: DEPOTID },
        async: false,
        dataType: "json",
        success: function (response) {
            //alert(JSON.stringify(response));
            ddltranspoter.empty().append('<option selected="selected" value="0">select Transporter</option>');
            $.each(response, function () {
                ddltranspoter.append($("<option></option>").val(this['ID']).html(this['NAME']));
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
function BindOrderType() {
    var ddlOrderType = $("#ddlOrderType");
    $.ajax({
        type: "POST",
        url: "/TranDepotStock/BindOrderType",
        data: '{}',
        async: false,
        dataType: "json",

        success: function (response) {

            ddlOrderType.empty();
            $.each(response, function () {
                ddlOrderType.append($("<option></option>").val(this['OrderTYPEID']).html(this['ORDERTYPENAME']));
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
    var ddlCountry = $("#ddlCountry");
    $.ajax({
        type: "POST",
        url: "/TranDepotStock/BindCountry",
        data: '{}',
        async: false,
        dataType: "json",

        success: function (response) {

            ddlCountry.empty().append('<option selected="selected" value="0"> Select Country</option>');;
            $.each(response, function () {
                ddlCountry.append($("<option></option>").val(this['COUNTRYID']).html(this['COUNTRYNAME']));
            });

            $("#ddlCountry").chosen({
                search_contains: true
            });
            $("#ddlCountry").trigger("chosen:updated");

        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}
function Saleorder() {
    var ddlSaleOrder = $("#ddlSaleOrder");
    var countryid = $("#ddlCountry").val();


    $.ajax({
        type: "POST",
        url: "/TranDepotStock/BindSaleOrder",
        data: { countryid: countryid },
        async: false,
        dataType: "json",
        success: function (response) {
            //alert(JSON.stringify(response));
            ddlSaleOrder.empty().append('<option selected="selected" value="0">select Order</option>');
            $.each(response, function () {
                ddlSaleOrder.append($("<option></option>").val(this['SALEORDERID']).html(this['SALEORDERNO']));
            });
            $("#ddlSaleOrder").chosen({
                search_contains: true
            });
            $("#ddlSaleOrder").trigger("chosen:updated");
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });

}
function Bindpacksize() {
    var ddlpackingsize = $("#ddlpackingsize");
    var ddlproductname = $("#ddlproductname").val();
    ddlpackingsize.append($("<option></option>").val('B9F29D12-DE94-40F1-A668-C79BF1BF4425').html('PCS'));

//    $.ajax({
//        type: "POST",
//        url: "/TranDepotStock/BindPackSize_productwise",
//        data: { ProductID: ddlproductname },
//        async: false,
//        dataType: "json",
//        success: function (response) {
//            //alert(JSON.stringify(response));
//            ddlpackingsize.empty();
//            $.each(response, function () {
              
//            });
//            $("#ddlpackingsize").chosen({
//                search_contains: true
//            });
//            $("#ddlpackingsize").trigger("chosen:updated");
//        },
//        failure: function (response) {
//            alert(response.responseText);
//        },
//        error: function (response) {
//            alert(response.responseText);
//        }
//    });
}
function BindCATEGORY() {
  
    var ddlcategory = $("#ddlcategory");
    $.ajax({
        type: "POST",
        url: "/TranDepotStock/BindCATEGORYBOS",
        data: '{}',
        async: false,
        dataType: "json",

        success: function (response) {

            ddlcategory.empty().append('<option selected="selected" value="0"> All</option>');
            $.each(response, function () {
                ddlcategory.append($("<option></option>").val(this['CATID']).html(this['CATNAME']));
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
function bindallproduct() {
    var ddlproductname = $("#ddlproductname");
    var sourcedepo = $("#ddlfromdepot").val();
    var typeid = $("#ddlOrderType").val();
    var saleorderid = $("#ddlSaleOrder").val();
    $.ajax({
        type: "POST",
        url: "/TranDepotStock/LoadProductBOS",
        data: { depotid: sourcedepo, saleorderid: saleorderid, typeid: typeid },
        async: false,
        dataType: "json",
        success: function (response) {
            //alert(JSON.stringify(response));
            ddlproductname.empty().append('<option selected="selected" value="0">select Product</option>');
            $.each(response, function () {
                ddlproductname.append($("<option></option>").val(this['PRODUCTID']).html(this['PRODUCTNAME']));
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
function LoadCategoryWiseProduct() {
    var ddlproductname = $("#ddlproductname");
    var sourcedepo = $("#ddlfromdepot").val();
    var catid = $("#ddlcategory").val();
    var typeid = $("#ddlOrderType").val();
    var saleorderid = $("#ddlSaleOrder").val();
    $.ajax({
        type: "POST",
        url: "/TranDepotStock/LoadCategoryWiseProductBOS",
        data: { catid: catid, depotid: sourcedepo, saleorderid: saleorderid, typeid: typeid },
        async: false,
        dataType: "json",
        success: function (response) {
            //alert(JSON.stringify(response));
            ddlproductname.empty().append('<option selected="selected" value="0">select Product</option>');
            $.each(response, function () {
                ddlproductname.append($("<option></option>").val(this['PRODUCTID']).html(this['PRODUCTNAME']));
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
function ProductTypeChecking() {

    var ddlproductname = $("#ddlproductname").val();

    $.ajax({
        type: "POST",
        url: "/TranDepotStock/ProductTypeChecking",
        data: { ProductID: ddlproductname },
        async: false,
        dataType: "json",
        success: function (response) {

            var productchk;
            $.each(response, function () {
                productchk = this['TYPE'];
            });
            if (productchk == '1') {
                Bindbatchno();
            }
            else {
                BindRM_PM_BatchDetails();
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
function Bindbatchno() {

    var ddlBatch = $("#ddlbatchno");
    var Batch = '0';
    var desc = '';
    var desc1 = '';
    var val1 = '';
    var listItems = '';
    var depotid = $("#ddlfromdepot").val();
    var productid = $("#ddlproductname").val();
    var PacksizeID = $("#ddlpackingsize").val();

    desc = 'STOCKQTY'.padEnd(15, '\u00A0') + repeatstr("&nbsp;", 10) + 'MRP'.padEnd(13, '\u00A0') + repeatstr("&nbsp;", 15) + 'ASSESMENT(%)'.padEnd(14, '\u00A0') + repeatstr("&nbsp;", 13) + 'MFG DATE'.padEnd(14, '\u00A0') + repeatstr("&nbsp;", 13) + 'EXP DATE'.padEnd(14, '\u00A0') + repeatstr("&nbsp;", 15) + 'BATCHNO'.padEnd(15, '\u00A0');
    listItems += "<option value='0'>" + desc + "</option>";

    $.ajax({
        type: "POST",
        url: "/TranDepotStock/BindBatchDetails",
        data: { DepotID: depotid, ProductID: productid, PacksizeID: PacksizeID, BatchNo: '0' },
        async: false,
        dataType: "json",
        success: function (Batchdetail) {
            /*FIFO checking for Billing Product Start*/
            ddlBatch.empty().append('<option selected="selected" value="0">select Batch No</option>');
            //fifo checking ends
            var counter = 0;
            
            $.each(Batchdetail, function () {
                val1 = this["INVOICESTOCKQTY"] + "|" + this["MRP"] + "|" + this["ASSESMENTPERCENTAGE"] + "|" + this["MFGDATE"] + "|" + this["EXPIRDATE"] + "|" + this["BATCHNO"];

                desc1 = this["INVOICESTOCKQTY"].padEnd(15, '\u00A0') + repeatstr("&nbsp;", 9) + this["MRP"].padEnd(15, '\u00A0') + repeatstr("&nbsp;", 25) + this["ASSESMENTPERCENTAGE"].padEnd(35, '\u00A0') + repeatstr("&nbsp;", 10) + this["MFGDATE"].padEnd(15, '\u00A0') + repeatstr("&nbsp;", 10) + this["EXPIRDATE"].padEnd(15, '\u00A0') + repeatstr("&nbsp;", 10) + this["BATCHNO"].padEnd(15, '\u00A0');
                listItems += "<option value='" + val1 + "'>" + desc1 + "</option>";
                counter = counter + 1;
            });
            ddlBatch.append(listItems);
            if (counter > 0) {
                
                //if (counter == 1) {

             //   $("#ddlbatchno").val(Batchdetail[0].INVOICESTOCKQTY.toString().trim() + "|" + Batchdetail[0].MRP.toString().trim() + "|" + Batchdetail[0].ASSESMENTPERCENTAGE.toString().trim() + "|" + Batchdetail[0].MFGDATE.toString().trim() + "|" + Batchdetail[0].EXPIRDATE.toString().trim() + "|" + Batchdetail[0].BATCHNO.toString().trim())
                //alert(Batchdetail[0].INVOICESTOCKQTY.toString().trim() + "|" + Batchdetail[0].MRP.toString().trim() + "|" + Batchdetail[0].ASSESMENTPERCENTAGE.toString().trim() + "|" + Batchdetail[0].MFGDATE.toString().trim() + "|" + Batchdetail[0].EXPIRDATE.toString().trim() + "|" + Batchdetail[0].BATCHNO.toString().trim());
               // getBatchDetails();
                //$("#InvoiceQty").focus();
                //}
            }
            else {
                toastr.info('<b>Stock not available..</b>');
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
function BindRM_PM_BatchDetails() {

}
function getBatchDetails() {
  
    var batchmrp;
    var batchassessvalue;
    var batchmfgdt;
    var batchexpdt;
    var productid = $("#ddlproductname").val();
    var invoicedate = $("#txttransferdate").val();
    //$('#ddlbatchno > option').each(function () {
    //    $(this).removeAttr("disabled");
    //});
    var strval2 = $("#ddlbatchno").val();
    var splitval = strval2.split('|');
    var stockqty = splitval[0];
    var mrp = splitval[1];
    var assessment = splitval[2];
    var mfgdate = splitval[3];
    var expdate = splitval[4];
    var batchno = splitval[5];
    $("#hdnbatchno").val(batchno);
    var rate;
    rate = BindDepotRate(productid, mrp, invoicedate);
    $("#txtstockqty").val(stockqty);
    $("#txtmrp").val(mrp);
    $("#hdn_mfgdate").val(mfgdate);
    $("#hdn_exprdate").val(expdate);
    $("#hdn_ASSESMENTPERCENTAGE").val(assessment);
    $("#hdnrate").val(rate);
    //$('#ddlbatchno > option').each(function () {
    //    $(this).prop('disabled', true);
    //});
}
function BindDepotRate(productid, mrp, invoicedate) {

    var rate;
    $.ajax({
        type: "POST",
        url: "/TranDepotStock/BindDepotRate",
        data: { Productid: productid, MRP: mrp, Date: invoicedate },
        async: false,
        dataType: "json",
        success: function (response) {


            $.each(response, function () {
                rate = this['RATE'];
            });

        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
    return rate;

}
function BindSTNInsuranceNumber() {
    var ddlinsuranceno = $("#ddlinsuranceno");
    var companyid = $("#ddlinsurancecompname").val();

    $.ajax({
        type: "POST",
        url: "/TranDepotStock/BindSTNInsuranceNumber",
        data: { companyid: companyid },
        async: false,
        dataType: "json",
        success: function (response) {

            ddlinsuranceno.empty().append('<option selected="selected" value="0">select Policy No</option>');
            $.each(response, function () {
                ddlinsuranceno.append($("<option></option>").val(this['INSURANCE_NO']).html(this['INSURANCE_NO']));
            });
            //$("#ddlinsuranceno").chosen({
            //    search_contains: true
            //});
            //$("#ddlinsuranceno").trigger("chosen:updated");
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });

}

function Insurancecodepot() {
    var ddlinsurancecompname = $("#ddlinsurancecompname");

    $.ajax({
        type: "POST",
        url: "/TranDepotStock/Bindinscomp",
        data: { menuid: MENUID },
        async: false,
        dataType: "json",
        success: function (response) {
            //alert(JSON.stringify(response));

            var counter = 0;
            ddlinsurancecompname.empty().append('<option selected="selected" value="0">select </option>');
            $.each(response, function () {
                ddlinsurancecompname.append($("<option></option>").val(this['ID']).html(this['COMPANY_NAME']));
                counter = counter + 1;
            });
            if (counter == 1) {
                ddlinsurancecompname.prop('selectedIndex', 1);
                BindSTNInsuranceNumber();
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

function finyrchk() {
    var currentdt;
    var frmdate;
    var todate;
    //date validation
    $.ajax({
        type: "POST",
        url: "/TranDepotStock/finyrchk",
        data: { finyr: FINYEAR },
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
    $("#txttransferdate").datepicker({
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
    $("#txtlrgrdate").datepicker({
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        selectCurrent: true,
        todayBtn: "linked",
        showAnim: "slideDown",
        yearRange: "-3:+0",
        maxDate: 'today',

        dateFormat: "dd/mm/yy",
        maxDate: 0,
        minDate: 0,
        showOn: 'button',
        buttonText: 'Show Date',
        buttonImageOnly: true,
        buttonImage: 'http://jqueryui.com/resources/demos/datepicker/images/calendar.gif'
    });
    $("#txtchallandate").datepicker({
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        selectCurrent: true,
        todayBtn: "linked",
        showAnim: "slideDown",
        yearRange: "-3:+0",
        maxDate: 'today',

        dateFormat: "dd/mm/yy",
        maxDate: 0,
        minDate: 0,
        showOn: 'button',
        buttonText: 'Show Date',
        buttonImageOnly: true,
        buttonImage: 'http://jqueryui.com/resources/demos/datepicker/images/calendar.gif'
    });
    $("#txtgatepassdate").datepicker({
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        selectCurrent: true,
        todayBtn: "linked",
        showAnim: "slideDown",
        yearRange: "-3:+0",
        maxDate: 'today',

        dateFormat: "dd/mm/yy",
        maxDate: 0,
        minDate: 0,
        showOn: 'button',
        buttonText: 'Show Date',
        buttonImageOnly: true,
        buttonImage: 'http://jqueryui.com/resources/demos/datepicker/images/calendar.gif'
    });

    $("#txtdeliverydate").datepicker({
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        selectCurrent: true,
        todayBtn: "linked",
        showAnim: "slideDown",
        yearRange: "-3:+0",
        maxDate: new Date(todate),
        minDate: new Date(currentdt),
        dateFormat: "dd/mm/yy",
        showOn: 'button',
        buttonText: 'Show Date',
        buttonImageOnly: true,
        buttonImage: 'http://jqueryui.com/resources/demos/datepicker/images/calendar.gif'



    });
    $("#txtfrmdate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
    $("#txttodate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
    $("#txttransferdate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());

}
function repeatstr(ch, n) {
    var result = "&nbsp;";
    while (n-- > 0)
        result += ch;
    return result;
}
function ReleaseSession() {

    $.ajax({
        type: "POST",
        url: "/TranDepotStock/RemoveSessionbos",
        data: '{}',
        dataType: "json",
        success: function (response) {


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
