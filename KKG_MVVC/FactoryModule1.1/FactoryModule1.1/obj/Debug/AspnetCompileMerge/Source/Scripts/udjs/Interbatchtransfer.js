var MENUID
var CHECKER;
var DEPOTID;
var currentdt;
var frmdate;
var todate;
var FINYEAR;
var UserID;
var TPU;
var usertype;
$(document).ready(function () {
    var qs = getQueryStrings();

    if (qs["MENUID"] != undefined && qs["MENUID"] != "") {

        MENUID = qs["MENUID"];
    }
    if (qs["DEPOTID"] != undefined && qs["DEPOTID"] != "") {

        DEPOTID = qs["DEPOTID"];
    }
    FINYEAR = qs["FINYEAR"];
    UserID = qs["USERID"];
    $('#dvAdd').css("display", "none");
    $('#dvDisplay').css("display", "");
    finyrchk();
   
    BindDepot();
    
    BindStorelocation();
   
    LoadReason();
    
    Bindpacksize();
    
 
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
    $("#btnAddnew").click(function (e) {
    
        $('#dvAdd').css("display", "");
        $('#dvDisplay').css("display", "none");
       
        Addnew();
    })
    $("#btnSearch2").click(function (e) {

        LoadToBatchDetails();
    })
    $('#ddlfromdepot').change(function () {
        var depot = $("#ddlfromdepot").val();
        if (depot == '0') {
            $("#ddlpackingsize").empty().append('<option selected="selected" value="0">select </option>');
            $("#ddlpackingsize").chosen({
                search_contains: true
            });
            $("#ddlpackingsize").trigger("chosen:updated");

            $("#ddlreason").empty().append('<option selected="selected" value="0">SELECT REASON NAME </option>');
            $("#ddlreason").chosen({
                search_contains: true
            });
            $("#ddlreason").trigger("chosen:updated");

            $("#ddlproductname").empty().append('<option selected="selected" value="0">SELECT PRODUCT NAME </option>');
            $("#ddlproductname").chosen({
                search_contains: true
            });
            $("#ddlproductname").trigger("chosen:updated");
            $("#ddlbatchno").empty().append('<option selected="selected" value="0">SELECT BATCH </option>');

        }
        else {
            BindProduct();
        }


    })
    $('#ddlproductname').change(function () {
        var productid = $("#ddlproductname").val();
        var productidto = $("#ddlProductTo").val();
        var packsizeid = $("#ddlpackingsize").val();
        if (productid == '0') {
            $("#ddlreason").empty().append('<option selected="selected" value="0">SELECT REASON NAME </option>');
            $("#ddlreason").chosen({
                search_contains: true
            });
            $("#ddlreason").trigger("chosen:updated");
            $("#ddlbatchno").empty().append('<option selected="selected" value="0">SELECT BATCH </option>');
            $("#hdn_stockqtyinpcs").val('');
            $("#hdn_lockadjustmentqty").val('');
            $("#hdn_editedstockqty").val('');
        }
        else {
            $("#hdn_lockadjustmentqty").val('');
            $("#hdn_editedstockqty").val('');
          
        }
        if (productid != '0' && packsizeid != '0') {
            BindBatchDetailsInterbatch();
            $("#ddlProductTo").val(productid);
            $("#ddlProductTo").chosen({
                search_contains: true
            });
            $("#ddlProductTo").trigger("chosen:updated");
        }
        if (productidto != '0' && packsizeid != '0') {
        
            LoadToBatchDetails()
            GetToProductdetails();
            
        }
    })
    $('#ddlProductTo').change(function () {
        var productidto = $("#ddlProductTo").val();
        if (productidto != '0' && packsizeid != '0') {
            LoadToBatchDetails();
            GetToProductdetails();
        }
    })
    $('#ddlbatchno').change(function () {

        getBatchDetails();
    })
    $('#ddlBatchTo').change(function () {

        getToBatchDetails();
    })
    $('#txtmrp').change(function () {

        GetToProductBCP();
    })
    $("#btnclose").click(function (e) {
        $('#dvAdd').css("display", "none");
        $('#dvDisplay').css("display", "");
    })
    $("#btnADDGrid").click(function (e) {
        if ($("#ddlreason").val() == '0') {
            toastr.warning('<b><font color=black>Provide Reason..!</font></b>');
            return false;
        }
        if ($("#ddlproductname").val() == '0') {
            toastr.warning('<b><font color=black>Provide  Product..!</font></b>');
            return false;
        }
        if ($("#ddlbatchno").val() == '0') {
            toastr.warning('<b><font color=black>Provide  Batch No..!</font></b>');
            return false;
        }
        if ($("#txtqty").val() == '') {
            toastr.warning('<b><font color=black>Provide Reason..!</font></b>');
            return false;
        }
        if ($("#txtmrp").val() == '') {
            toastr.warning('<b><font color=black>MRP not available ..!</font></b>');
            return false;
        }
        if ($("#ddlProductTo").val() == '0') {
            toastr.warning('<b><font color=black>Provide To product..!</font></b>');
            return false;
        }
        if ($("#ddlBatchTo").val() == '0') {
            toastr.warning('<b><font color=black>Provide To Batch No..!</font></b>');
            return false;
        }
    })
    $("#btnSave").click(function (e) {
    })
    $("#btnsearch").click(function (e) {
    })
})
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
    var stockqty = splitval[1];
    var mrp = splitval[2];
   
    var mfgdate = splitval[3];
    var expdate = splitval[4];
    var batchno = splitval[0];
 //   $("#hdnbatchno").val(batchno);
    var rate;
    //rate = BindDepotRate(productid, mrp, invoicedate);
    $("#txtmfgdate").val(mfgdate);
    $("#txtexpdate").val(expdate);
    $("#hdn_mrp").val(mrp);
    $("#hdnMFGDATE").val(mfgdate);
    $("#hdnEXPRDATE").val(expdate);
    $("#hdn_stockqty").val(assessment);
    $("#hdn_batch").val(batchno);
   // $("#hdnrate").val(rate);
    //$('#ddlbatchno > option').each(function () {
    //    $(this).prop('disabled', true);
    //});
}
function getToBatchDetails() {

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
    var stockqty = splitval[1];
    var mrp = splitval[2];

    var mfgdate = splitval[3];
    var expdate = splitval[4];
    var batchno = splitval[0];
   // $("#hdnbatchno").val(batchno);
    var rate;
    //rate = BindDepotRate(productid, mrp, invoicedate);
    $("#txtmfgdate2").val(mfgdate);
    $("#txtexpdate2").val(expdate);
    $("#txtbatchno").val(batchno);
    $("#hdn_batchto").val(batchno);
    //notrequired in batch to
    //$("#hdn_mrp").val(mrp);
    //$("#hdnMFGDATE").val(mfgdate);
    //$("#hdnEXPRDATE").val(expdate);
    //$("#hdn_stockqty").val(assessment);
 
   
}
function addProduct() {
}
function addProductInDetailsGrid() {
    var productid = $("#ddlproductname").val();
    var productnmame = $('#ddlproductname').find('option:selected').text();
    var packsizeid = $("#ddlpackingsize").val();
    var packsizename = $('#ddlpackingsize').find('option:selected').text();
    var transferqty = $("#txttransferqty").val();
    var price = $("#hdn_price").val();
    var mrp = $("#hdn_mrp").val();
    var weight = $("#hdn_mrp").val();
    var mfgdt = $("#txtmfgdate").val();
    var expdt = $("#txtexpdate").val();
    var expdt = $("#txtexpdate").val();
    var batchno = $("#hdn_batch").val();
    var productidto = $("#ddlProductTo").val();
    var productnmameto = $('#ddlProductTo').find('option:selected').text();
    var mfgdt2 = $("#txtmfgdate2").val();
    var expdt2 = $("#txtexpdate2").val();
    var mfgdt2 = $("#txtmfgdate2").val();
    var reasonid = $("#ddlreason").val();
    var reasonname = $('#ddlreason').find('option:selected').text();
    var slocationid = $("#ddlstorelocation").val();
    var slocationname = $('#ddlstorelocation').find('option:selected').text();
    var assessment = $("#hdn_ASSESSABLEPERCENT").val();
    var mrp2 = $("#txtmrp").val();
    var batchno2 = $("#hdn_batchto").val();
    var assessment2 = $("#hdn_ToProductASSESSABLEPERCENT").val();
    var weight2 = $("#hdn_Toproductweight").val();
    var rate2 = $("#hdn_Toproductdepotrate").val();
    var amount = 0;
    amount = parseFloat(transferqty) * parseFloat(price);
    var amoun2 = 0;
    amoun2 = parseFloat(transferqty) * parseFloat(rate2);
    var tr;
    var srl = 0;

    srl = srl + 1;
    tr = $('#productDetailsGrid');
    var HeaderCount = $('#productDetailsGrid thead th').length;
    if (HeaderCount == 0) {
        tr.append("<thead><tr><th>Sl.No.</th><th style='display: none'>Product ID</th><th>Product</th><th>Batch</th><th style='display: none'>PackSize ID</th><th>Pack Size</th><th>Adj.Qty</th><th>PRICE</th><th>MRP</th><th>Weight</th><th>Mfg. Date</th><th >Exp Date</th><th style='display: none'>Reason id</th><th style='display: none'>Reason Name</th><th style='display: none'>Assesmet(%)</th><th style='display: none'>Assesment Amt.</th><th style='display: none'>Locationid</th><th style='display: none'>Buffer Qty</th><th>Approved</th><th>Delete</th></tr></thead><tbody>");
    }
    tr = $('<tr/>');
    tr.append("<td style='text-align: center'>" + srl + "</td>");//0
    tr.append("<td style='display: none'>" + productid + "</td>");//1
    tr.append("<td style='text-align: center'>" + productnmame + "</td>");//2
    tr.append("<td style='text-align: center'>" + batchno + "</td>");//2
    tr.append("<td style='display: none'>" + packsizeid + "</td>");//2
    tr.append("<td style='text-align: center'>" + packsizename + "</td>");//2
    tr.append("<td style='text-align: center'>" + transferqty + "</td>");//2
    tr.append("<td style='text-align: center'>" + price + "</td>");//2
    tr.append("<td style='text-align: center'>" + mrp + "</td>");//2
    tr.append("<td style='text-align: center'>" + weight + "</td>");//2
    tr.append("<td style='text-align: center'>" + mfgdt + "</td>");//2
    tr.append("<td style='text-align: center'>" + expdt + "</td>");//2
    tr.append("<td style='text-align: center'>" + reasonid + "</td>");//2
    tr.append("<td style='text-align: center'>" + reasonname + "</td>");//2
    tr.append("<td style='text-align: center'>" + assessment + "</td>");//2
    tr.append("<td style='text-align: center'>" + amount + "</td>");//2
    tr.append("<td style='text-align: center'>" + slocationid + "</td>");//2
    tr.append("<td style='text-align: center'>" + slocationname + "</td>");//2
    tr.append("<td style='text-align: center'>" + transferqty + "</td>");//2
    tr.append("<td style='text-align: center'> A </td>");//2
    tr.append("<td style='text-align: center'><input type='image' class='gvTempDelete'  id='btndelete' <img src='../Images/ico_delete_16.png'/></input></td>");
    $("#productDetailsGrid").append(tr);
    //to product
    tr = $('<tr/>');
    tr.append("<td style='text-align: center'>" + srl + "</td>");//0
    tr.append("<td style='display: none'>" + productidto + "</td>");//1
    tr.append("<td style='text-align: center'>" + productnmameto + "</td>");//2
    tr.append("<td style='text-align: center'>" + batchno2 + "</td>");//2
    tr.append("<td style='display: none'>" + packsizeid + "</td>");//2
    tr.append("<td style='text-align: center'>" + packsizename + "</td>");//2
    tr.append("<td style='text-align: center'>" + transferqty + "</td>");//2
    tr.append("<td style='text-align: center'>" + rate2 + "</td>");//2
    tr.append("<td style='text-align: center'>" + mrp2 + "</td>");//2
    tr.append("<td style='text-align: center'>" + weight2 + "</td>");//2
    tr.append("<td style='text-align: center'>" + mfgdt2 + "</td>");//2
    tr.append("<td style='text-align: center'>" + expdt2 + "</td>");//2
    tr.append("<td style='text-align: center'>" + reasonid + "</td>");//2
    tr.append("<td style='text-align: center'>" + reasonname + "</td>");//2
    tr.append("<td style='text-align: center'>" + assessment2 + "</td>");//2
    tr.append("<td style='text-align: center'>" + amoun2 + "</td>");//2
    tr.append("<td style='text-align: center'>" + slocationid + "</td>");//2
    tr.append("<td style='text-align: center'>" + slocationname + "</td>");//2
    tr.append("<td style='text-align: center'>" + transferqty + "</td>");//2
    tr.append("<td style='text-align: center'> A </td>");//2
    tr.append("<td style='text-align: center'><input type='image' class='gvTempDelete'  id='btndelete' <img src='../Images/ico_delete_16.png'/></input></td>");
    $("#productDetailsGrid").append(tr);
    tr.append("</tbody>");
}
function clearadd() {
}

function Addnew() {

    $('#dvAdd').css("display", "");
    $('#dvDisplay').css("display", "none");
    $('#divTransferNo').css("display", "none");
    BindProduct();
    $("#ddlfromdepot").chosen({
        search_contains: true
    });
    $("#ddlfromdepot").trigger("chosen:updated");
    $("#ddlproductname").chosen({
        search_contains: true
    });
    $("#ddlproductname").trigger("chosen:updated");

    $("#ddlProductTo").chosen({
        search_contains: true
    });
    $("#ddlProductTo").trigger("chosen:updated");
    
    $("#ddlreason").chosen({
        search_contains: true
    });
    $("#ddlreason").trigger("chosen:updated");
    $("#ddlstorelocation").chosen({
        search_contains: true
    });
    $("#ddlstorelocation").trigger("chosen:updated");
    $("#ddlpackingsize").chosen({
        search_contains: true
    });
    $("#ddlpackingsize").trigger("chosen:updated");
}
function BindDepot() {
    var ddlfromdepot = $("#ddlfromdepot");
    $.ajax({
        type: "POST",
        url: "/TranDepotStock/BindDepo",
        //data: '{}',
        data: { depotid: DEPOTID },
        async: false,
        dataType: "json",

        success: function (response) {
            //alert(JSON.stringify(response));
            ddlfromdepot.empty();
            $.each(response, function () {
                ddlfromdepot.append($("<option></option>").val(this['BRID']).html(this['BRNAME']));
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
function BindStorelocation() {
    var ddlstorelocation = $("#ddlstorelocation");

    $.ajax({
        type: "POST",
        url: "/TranDepotStock/BindStorelocation",
       data: '{}',
      
        async: false,
        dataType: "json",

        success: function (response) {
            //alert(JSON.stringify(response));
            ddlstorelocation.empty().append('<option selected="selected" value="0">select Store Location</option>');
           
            $.each(response, function () {
                ddlstorelocation.append($("<option></option>").val(this['ID']).html(this['NAME']));
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
function LoadReason() {
    var ddlreason = $("#ddlreason");
    $.ajax({
        type: "POST",
        url: "/TranDepotStock/LoadReason",
        //data: '{}',
        data: { menuid: MENUID },
        async: false,
        dataType: "json",

        success: function (response) {
            //alert(JSON.stringify(response));
            ddlreason.empty().append('<option selected="selected" value="0">select Reason Name</option>');
            $.each(response, function () {
                ddlreason.append($("<option></option>").val(this['ID']).html(this['NAME']));
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
function Bindpacksize() {
    var ddlpackingsize = $("#ddlpackingsize");
   
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
function BindProduct() {
    var ddlproductname = $("#ddlproductname");
    var ddlProductTo = $("#ddlProductTo");
    var sourcedepo = $("#ddlfromdepot").val();
    $.ajax({
        type: "POST",
        url: "/TranDepotStock/BindProduct",
        //data: '{}',
        data: { depotid: sourcedepo },
        async: false,
        dataType: "json",

        success: function (response) {
           
            ddlproductname.empty().append('<option selected="selected" value="0">select Product Name</option>');
            ddlProductTo.empty().append('<option selected="selected" value="0">select Product Name</option>');
            $.each(response, function () {
                ddlproductname.append($("<option></option>").val(this['PRODUCTID']).html(this['PRODUCTNAME']));
                ddlProductTo.append($("<option></option>").val(this['PRODUCTID']).html(this['PRODUCTNAME']));
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
function GetToProductBCP() {
    
  
    var product = $("#ddlProductTo").val();
    var mrp = $("#txtmrp").val();
    $.ajax({
        type: "POST",
        url: "/TranDepotStock/GetToProductBCP",
        //data: '{}',
        data: { Productid: product, mrp: mrp },
        async: false,
        dataType: "json",

        success: function (response) {

           
            $.each(response, function () {
                $("#hdn_Toproductdepotrate").val(this['RATE'].toString().trim());
                
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

function GetToProductdetails() {


    var product = $("#ddlProductTo").val();
    var mrp = $("#txtmrp").val();
    $.ajax({
        type: "POST",
        url: "/TranDepotStock/GetToProductdetails",
        //data: '{}',
        data: { Productid: product, mrp: mrp },
        async: false,
        dataType: "json",

        success: function (response) {


            $.each(response, function () {
                $("#hdn_ToProductASSESSABLEPERCENT").val(this['ASSESSABLEPERCENT'].toString().trim());
                $("#hdn_Toproductweight").val(this['WEIGHT'].toString().trim());
                $("#txtmrp").val(this['MRP'].toString().trim());

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
// from batch
function GetfrmProductdetails() {


    var product = $("#ddlproductname").val();
    var batch = $("#ddlbatchno").val();
    var mrp = $("#txtmrp").val();
    $.ajax({
        type: "POST",
        url: "/TranDepotStock/GetfrmProductdetails",
        //data: '{}',
        data: { Productid: product, batchno: batch },
        async: false,
        dataType: "json",

        success: function (response) {


            $.each(response, function () {
                $("#hdn_ASSESSABLEPERCENT").val(this['ASSESSABLEPERCENT'].toString().trim());
                $("#hdn_weight").val(this['WEIGHT'].toString().trim());
                $("#hdn_price").val(this['Price'].toString().trim());
                $("#txtmrp").val(this['MRP'].toString().trim());
               
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
//to batch product details

//BIND FROM BATCH
function BindBatchDetailsInterbatch() {
    var ddlBatch = $("#ddlbatchno");
var Batch = '0';
var desc = '';
var desc1 = '';
var val1 = '';
var listItems = '';
var depotid = $("#ddlfromdepot").val();
var productid = $("#ddlproductname").val();
var PacksizeID = $("#ddlpackingsize").val();
var storelocastion = $("#ddlstorelocation").val();

    desc = 'BATCH NO.'.padEnd(15, '\u00A0') + repeatstr("&nbsp;", 10) + 'STOCKQTY'.padEnd(13, '\u00A0') + repeatstr("&nbsp;", 15) + 'MRP'.padEnd(14, '\u00A0') + repeatstr("&nbsp;", 13) + 'MFG DATE'.padEnd(14, '\u00A0') + repeatstr("&nbsp;", 13) + 'EXP DATE'.padEnd(14, '\u00A0') + repeatstr("&nbsp;", 15);
listItems += "<option value='0'>" + desc + "</option>";

$.ajax({
    type: "POST",
    url: "/TranDepotStock/BindBatchDetailsInterbatch",
    data: { DepotID: depotid, ProductID: productid, PacksizeID: PacksizeID, BatchNo: '0', storelocation: storelocastion },
    async: false,
    dataType: "json",
    success: function (Batchdetail) {
        /*FIFO checking for Billing Product Start*/
        ddlBatch.empty().append('<option selected="selected" value="0">select Batch No</option>');
        //fifo checking ends
        var counter = 0;

        $.each(Batchdetail, function () {
            val1 = this["BATCHNO"] + "|" + this["INVOICESTOCKQTY"] + "|" + this["MRP"] + "|" + this["MFGDATE"] + "|" + this["EXPIRDATE"] ;

            desc1 = this["BATCHNO"].padEnd(15, '\u00A0') + repeatstr("&nbsp;", 9) + this["INVOICESTOCKQTY"].padEnd(15, '\u00A0') + repeatstr("&nbsp;", 25) + this["MRP"].padEnd(35, '\u00A0') + repeatstr("&nbsp;", 10) + this["MFGDATE"].padEnd(15, '\u00A0') + repeatstr("&nbsp;", 10) + this["EXPIRDATE"].padEnd(15, '\u00A0') + repeatstr("&nbsp;", 10) ;
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
//BIND TO BATCH
function LoadToBatchDetails() {
    var ddlBatch = $("#ddlBatchTo");
    var Batch = '0';
    var desc = '';
    var desc1 = '';
    var val1 = '';
    var listItems = '';
    var depotid = $("#ddlfromdepot").val();
    var productid = $("#ddlProductTo").val();
    var PacksizeID = $("#ddlpackingsize").val();
    var storelocastion = $("#ddlstorelocation").val();
    alert('a');
    var FromDate = $("#txtmfgfrm").val();
    var ToDate = $("#txtmfgto").val();
    
    desc = 'BATCH NO'.padEnd(15, '\u00A0') + repeatstr("&nbsp;", 10) + 'MRP'.padEnd(14, '\u00A0') + repeatstr("&nbsp;", 13) + 'MFG DATE'.padEnd(14, '\u00A0') + repeatstr("&nbsp;", 13) + 'EXP DATE'.padEnd(14, '\u00A0') + repeatstr("&nbsp;", 15);
    listItems += "<option value='0'>" + desc + "</option>";

    $.ajax({
        type: "POST",
        url: "/TranDepotStock/LoadToBatchDetails",
        data: { DepotID: depotid, ProductID: productid, BatchNo: '0', FromDate: FromDate, ToDate: ToDate, storelocation: storelocastion },
        async: false,
        dataType: "json",
        success: function (Batchdetail) {
            /*FIFO checking for Billing Product Start*/
            ddlBatch.empty().append('<option selected="selected" value="0">select Batch No</option>');
            //fifo checking ends
            var counter = 0;

            $.each(Batchdetail, function () {
                val1 = this["BATCHNO"] + "|" + this["MRP"] + "|" + this["MFGDATE"] + "|" + this["EXPIRDATE"];

                desc1 = this["BATCHNO"].padEnd(15, '\u00A0') + repeatstr("&nbsp;", 9) + this["MRP"].padEnd(35, '\u00A0') + repeatstr("&nbsp;", 10) + this["MFGDATE"].padEnd(15, '\u00A0') + repeatstr("&nbsp;", 10) + this["EXPIRDATE"].padEnd(15, '\u00A0') + repeatstr("&nbsp;", 10);
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

function repeatstr(ch, n) {
    var result = "&nbsp;";
    while (n-- > 0)
        result += ch;
    return result;
}

function finyrchk() {

    //date validation
    $.ajax({
        type: "POST",
        url: "/TranDepotStock/finyrchk",
        //  data: '{}',
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
    $("#txtadjustmentdate").datepicker({
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
    $("#txtmfgfrm").datepicker({
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
    $("#txtmfgto").datepicker({
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
   
    $("#txtfrmdate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
    $("#txttodate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
    $("#txtadjustmentdate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());

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
