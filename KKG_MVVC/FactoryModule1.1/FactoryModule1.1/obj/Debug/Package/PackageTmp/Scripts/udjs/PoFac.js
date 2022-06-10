/*Created by Pritam Basu on 29022020**/

var CHECKER;
var yearstart = "";
var startdate = "";
var enddate = "";
var today = "";

$(document).ready(function () {

    var qs = getQueryStrings();
    if (qs["CHECKER"] != undefined && qs["CHECKER"] != "") {
        CHECKER = qs["CHECKER"];
    }


    if (qs["MENUID"] != undefined && qs["MENUID"] != "") {
        var menuid = qs["MENUID"];
    }
    $("#hdnmenuid").val(menuid);

    if ($("#hdnmenuid").val() == "") {
        toastr.error("Login Error.Please Contact to your admin");
        $('#addnew').css("display", "none");
        $('#showdata').css("display", "none");
        $('#btnnewentry').hide();
        $('#ponoshow').css("display", "none");
        $("#btnsave").hide();
        $('#divuploadinfo').css("display", "show");
        return;
    }


    GETUSER();
    GETFINYEAR();
    getFinyearWiseDate();

    
  
    $("#Vendorid").chosen();
    $("#Vendorid").trigger("chosen:updated");

    $("#Productid").chosen();
    $("#Productid").trigger("chosen:updated");

    $("#PACKSIZEID_FROM").chosen();
    $("#PACKSIZEID_FROM").trigger("chosen:updated");

    $("#Currencyid").chosen();
    $("#Currencyid").trigger("chosen:updated");

    
    /*Start logic for checker*/
    if (CHECKER == "TRUE") {
        //$('#btnnewentry').css("display", "none");
        //$("#lbldvreject").show();
        //$("#dvreject").show()
        //$('#btnsave').css("display", "none");
        //$('#btnApprove').css("display", "");
        //$('#btnReject').css("display", "");
        userid = document.getElementById("hdnuser").value;
        finyear = document.getElementById("hdnfinyear").value;
        /*start of page load logic for different user*/
        if (userid == "4133" || userid == "4137" || userid == "37284") /*mayank and p.k verma*/ {
            $('#btnnewentry').css("display", "none");
            $('#btnsave').css("display", "none");
            $('#btnApprove').css("display", "");
            $('#btnReject').css("display", "");
            $('#btnHoldApproved').css("display", "");
            $('#btnConfirmed').css("display", "none");
            $("#lbldvreject").show();
            $("#dvreject").show()
        }
        else if (userid == "4135" || userid == "3882" || userid == "37241") /*for gla,vksarda and rksurana*/ {
            $('#btnnewentry').css("display", "none");
            $('#btnsave').css("display", "none");
            $('#btnApprove').css("display", "");
            $('#btnReject').css("display", "");
            $('#btnHoldApproved').css("display", "none");
            $('#btnConfirmed').css("display", "");
            $("#lbldvreject").show();
            $("#dvreject").show()
        }
        else if (userid == "3894" || userid == "9038") /*for sktapariya and sunil Agarwal */ {
            $('#btnnewentry').css("display", "none");
            $('#btnsave').css("display", "none");
            $('#btnApprove').css("display", "");
            $('#btnReject').css("display", "");
            $('#btnHoldApproved').css("display", "none");
            $('#btnConfirmed').css("display", "none");
            $("#lbldvreject").show();
            $("#dvreject").show()
        }
        else {
            $('#btnnewentry').hide();
            $('#btnsave').css("display", "none");
            $('#btnApprove').css("display", "none");
            $('#btnReject').css("display", "none");
            $('#btnHoldApproved').css("display", "none");
            $('#btnConfirmed').css("display", "none");
            $("#lbldvreject").hide();
            $("#dvreject").hide()
             }
    }
    else {
        $('#btnnewentry').show();
        $('#btnsave').css("display", "");
        $('#btnApprove').css("display", "none");
        $('#btnReject').css("display", "none");
        $('#btnHoldApproved').css("display", "none");
        $('#btnConfirmed').css("display", "none");
        $("#lbldvreject").hide();
        $("#dvreject").hide()
    }
    /*End of logic for checker*/
  
  
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "positionClass": "toast-top-center",
        "onclick": null,
        "showDuration": "1000",
        "hideDuration": "1000",
        "timeOut": "2000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "slideDown",
        "hideMethod": "slideUp"
    };
    $("#Fromdate").datepicker({
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        selectCurrent: true,
        todayBtn: "linked",
        showAnim: "slideDown",
        yearRange: "-3:+3",
        maxDate: enddate,
        minDate: startdate,
        dateFormat: "dd/mm/yy",
        showOn: 'button',
        buttonText: 'Show Date',
        buttonImageOnly: true,
        buttonImage: 'http://jqueryui.com/resources/demos/datepicker/images/calendar.gif'
    });
    $(".ui-datepicker-trigger").mouseover(function () {
        $(this).css('cursor', 'pointer');
    });
    $("#Todate").datepicker({
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        selectCurrent: true,
        todayBtn: "linked",
        showAnim: "slideDown",
        yearRange: "-3:+3",
        maxDate: enddate,
        minDate: startdate,
        dateFormat: "dd/mm/yy",
        showOn: 'button',
        buttonText: 'Show Date',
        buttonImageOnly: true,
        buttonImage: 'http://jqueryui.com/resources/demos/datepicker/images/calendar.gif'
    });
    $(".ui-datepicker-trigger").mouseover(function () {
        $(this).css('cursor', 'pointer');
    });

   
    $("#Podate").datepicker({


        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        selectCurrent: true,
        todayBtn: "linked",
        showAnim: "slideDown",
        yearRange: "-3:+3",
        maxDate: enddate,
        minDate: startdate,
        dateFormat: "dd/mm/yy",
        showOn: 'button',
        buttonText: 'Show Date',
        
        buttonImageOnly: true,
        buttonImage: 'http://jqueryui.com/resources/demos/datepicker/images/calendar.gif'

        
    });
    $(".ui-datepicker-trigger").mouseover(function () {
        $(this).css('cursor', 'pointer');
    });
    $("#REQUIREDDate").datepicker({
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        selectCurrent: true,
        todayBtn: "linked",
        showAnim: "slideDown",
        yearRange: "-3:+3",
        maxDate: enddate,
        minDate: startdate,
        dateFormat: "dd/mm/yy",
        showOn: 'button',
        buttonText: 'Show Date',
        buttonImageOnly: true,
        buttonImage: 'http://jqueryui.com/resources/demos/datepicker/images/calendar.gif'
    });
    $(".ui-datepicker-trigger").mouseover(function () {
        $(this).css('cursor', 'pointer');
    });
    $("#REQUIREDTODATE").datepicker({
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        selectCurrent: true,
        todayBtn: "linked",
        showAnim: "slideDown",
        yearRange: "-3:+3",
        maxDate: enddate,
        minDate: startdate,
        dateFormat: "dd/mm/yy",
        showOn: 'button',
        buttonText: 'Show Date',
        buttonImageOnly: true,
        buttonImage: 'http://jqueryui.com/resources/demos/datepicker/images/calendar.gif'
    });
    $(".ui-datepicker-trigger").mouseover(function () {
        $(this).css('cursor', 'pointer');
    });
    $("#Qutrefdate").datepicker({
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        selectCurrent: true,
        todayBtn: "linked",
        showAnim: "slideDown",
        yearRange: "-3:+3",
        maxDate: enddate,
        minDate: startdate,
        dateFormat: "dd/mm/yy",
        showOn: 'button',
        buttonText: 'Show Date',
        buttonImageOnly: true,
        buttonImage: 'http://jqueryui.com/resources/demos/datepicker/images/calendar.gif'
    });
    $(".ui-datepicker-trigger").mouseover(function () {
        $(this).css('cursor', 'pointer');
    });
    $("#Fromdate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
    $("#Todate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
    $("#Podate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
    $("#REQUIREDDate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
    $("#REQUIREDTODATE").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
    $("#Qutrefdate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());

    debugger;
    $('#addnew').css("display", "none");
    $('#ponoshow').css("display", "none");
    $('#showdata').css("display", "");
    $('#divuploadinfo').css("display", "none");

    /*start page load method*/
   
    Vendorandcurrencey();
    LoadTermsCondition();
    bindproduct();
    bindpogrid();
    /*end page load method*/

    /*add start*/
    var srl = 0;
    /*productname and unitname from here */
    $('#Productid').change(function () {
        varproductnameText = $("#Productid option:selected").text();
        $("#Productname").val(varproductnameText);
    });

    

    $('#PACKSIZEID_FROM').change(function () {
        PACKSIZEID_FROMText = $("#PACKSIZEID_FROM option:selected").text();
        $("#PACKSIZENAME_FROM").val(PACKSIZEID_FROMText);
    });

    $('#Vendorid').change(function () {
        VendornameTExt = $("#Vendorid option:selected").text();
        $("#Vendorname").val(VendornameTExt);
    });

    $('#Unitid').change(function () {
        UnitText = $("#Unitid option:selected").text();
        $("#Unit").val(UnitText);
    });

    $('#Currencyid').change(function () {
        CURRENCYTYPEText = $("#Currencyid option:selected").text();
        $("#CURRENCYTYPE").val(CURRENCYTYPEText);
    });

    /*add click function start here*/
    $("#btnadd").click(function (e) {
        debugger;
        var isexists = 'n';
        
        srl = srl + 1;
        var tablenew;
        var POID = $("#Poid").val();
        var Podate = $("#Podate").val();
        var vendorid = $("#Vendorid").val();
        var vendorname = $("#Vendorname").val();
        var productid = $("#Productid").val();
        var productname = $("#Productname").val();
        var qty = $("#Qty").val();
        var unitid = $("#PACKSIZEID_FROM").val();
        var unit = $("#PACKSIZENAME_FROM").val();
        var currencey = $("#Currencyid").val();
        var purchaserate = $("#RATE").val();
        var basicvalue = $("#RATE").val();
        var lastrate = $("#LASTRATE").val();
        var maxrate = $("#MAXRATE").val();
        var minrate = $("#MINRATE").val();
        var avgrate = $("#AVGRATE").val();

        var mrp = '0';
        var mrpvalue = '0';

        var cgst = $("#Cgst").val();
        var cgstid = $("#CGSTTAXID").val();
        var sgst = $("#Sgst").val();
        var sgstid = $("#SGSTTAXID").val();
        var igst = $("#Igst").val();
        var igstid = $("#IGSTTAXID").val();

        var REQUIREDDate = $("#REQUIREDDate").val();
        var REQUIREDTODATE = $("#REQUIREDTODATE").val();

        if ($('#podetail').length) {
            $('#podetail').each(function () {

                /*Same product check*/
                debugger;
                var exists = false;
                var arraydetails = [];
                var count = $('#podetail tbody tr').length;
                $('#podetail tbody tr').each(function () {
                    var dispatchdetail = {};
                    var productidgrd = $(this).find('td:eq(2)').html().trim();
                    dispatchdetail.ProductID = productidgrd;
                    arraydetails.push(dispatchdetail);
                });

                var jsondispatchobj = {};
                jsondispatchobj.fgDispatchDetails = arraydetails;
                for (i = 0; i < jsondispatchobj.fgDispatchDetails.length; i++) {
                    if (jsondispatchobj.fgDispatchDetails[i].ProductID.trim() == productid.trim()) {
                        exists = true;
                        break;
                    }
                }

                if (exists != false) {
                    toastr.error('Item already exists...!');
                    return false;
                }

                $.ajax({
                    type: "POST",
                    url: "/mmpo/ConvertionQty",
                    data: { Productid: $('#Productid').val(), unitid: $('#PACKSIZEID_FROM').val(), qty: $("#Qty").val(), purchaserate: $("#RATE").val(), mpr: mrp },
                    dataType: "json",

                    traditional: true,
                    success: function (convertionqty) {
                        $.each(convertionqty, function (index, record) {
                            Totalmrp = ' <label>' + record.toString() + '</label> <br/>';
                            $("#Totalmrp").val(this['MRP']);
                        });
                        if (parseFloat($("#Totalmrp").val()).toFixed(2) < "0") {
                            toastr.warning('Uom and Packsize Mapping are Missmatch');
                            return false;
                        }
                        else {
                            /*quantity check*/
                            if (qty == '' || qty == '0') {
                                toastr.warning('Enter Quantity');
                                $("#Qty").focus();
                                return false;
                            }
                            /*Zero rate Check*/
                            else if (purchaserate == '0' || purchaserate == '') {
                                toastr.warning('Zero rate billing not allow');
                                $("#Productid").focus();
                                return false;
                            }
                            /*convertion qty*/

                            /*date checking*/
                            else if (REQUIREDDate < Podate) {
                                toastr.warning('Delivery from date can not be less than PO Entry Date');
                                return false;
                            }
                            /*date checking*/
                            else if (REQUIREDTODATE > REQUIREDDate) {
                                toastr.warning('Delivery to date can not be less than Delivery from date');
                                return false;
                            }
                            else if ($("#PACKSIZEID_FROM option:selected").val() == "0") {
                                toastr.warning('please select Unit');
                                $("#PACKSIZEID_FROM").focus();
                                return false;
                            }
                            /*currencey checking*/
                            else if (currencey == 0) {
                                toastr.warning('please select Currencey type');
                                $("#Currencyid").focus();
                                return false;
                            }
                            else if (unitid == 0) {
                                toastr.warning('please select Unit type');
                                $("#PACKSIZEID_FROM").focus();
                                return false;
                            }

                            else {
                                /*table create*/
                                var tr;
                                tr = $('#podetail');
                                var HeaderCount = $('#podetail thead th').length;
                                if (HeaderCount == 0) {
                                    tr.append("<thead><tr><th>Sl.No.</th><th style='display: none'>PRoductid</th><th>Material</th><th>Qty</th><th>Unit</th><th>Purchase Rate</th><th>Basic Value</th><th>Last Rate</th><th>Max Rate</th><th>Avg Rate</th><th>Min Rate</th><th>Cgst(%)</th><th>Sgst(%)</th><th>Igst(%)</th><th>MRP</th><th>MRPValue</th><th>Delivary From Date</th><th> Delivary To Date</th><th>Delete</th></tr></thead><tbody>");
                                }
                                  tablenew = "<tr><td style='width:5%'>" + srl + "</td><td style='display:none'>" + POID + "</td><td style='display:none'>" + productid + "</td><td style='width:75px'>" + productname + "</td><td style='width:10%'>" + qty + "</td><td style='display:none'>" + unitid + "</td><td style='width:30%'>" + unit + " </td><td style='width:10%'>" + purchaserate + "</td><td style='width:10%'>" + basicvalue + "</td> <td style='width:10%' >" + lastrate + "</td><td style='width:10%' >" + maxrate + "</td><td style='width:10%'>" + avgrate + "</td><td style='width:10%'>" + minrate + "</td><td style='display:none'>" + cgstid + "</td><td style='width:10%'>" + cgst + "</td><td style='display:none'>" + sgstid + "</td><td style='width:10%'>" + sgst + "</td><td style='display:none'>" + igstid + "</td><td style='width:10%'>" + igst + "</td><td style='width:10%'>" + mrp + "</td><td style='width:10%'>" + mrpvalue + "</td><td style='width:10%'>" + REQUIREDDate + "</td><td style='width:10%'>" + REQUIREDTODATE + "</td><td> <input type='image' class='gvdwndelete'  id='btndelete' <img src='../Images/ico_delete_16.png'/></td></tr> ";
                                  $("#podetail").append(tablenew);
                                  /*after grid add calculate values*/
                                  debugger;
                                  updateRowCount();
                                  Calculateamount();
                                  /*after add all value clear*/
                                  $("#Qty").val('');
                                  $("#Igst").val('');
                                  $("#Sgst").val('');
                                  $("#Cgst").val('');
                                  $("#RATE").val('');
                                  $("#LASTRATE").val('');
                                  $("#MAXRATE").val('');
                                  $("#MINRATE").val('');
                                  $("#AVGRATE").val('');
                                  $("#Productid").val('0');
                                  $("#PACKSIZEID_FROM").val('0');
                                  debugger;
                                  $("#Productid").focus();

                                 }
                        }

                    }
                });
            })
        }
        /*end add*/
    });

    $("#btnsave").click(function (e) {
        savePodata();
    });

    $("#btncancel").click(function (e) {
        $('#addnew').css("display", "none");
        $('#ponoshow').css("display", "none");
        $('#showdata').css("display", "");

        //if (CHECKER == "TRUE") {
        //    $('#btnnewentry').hide();
        //    $("#lbldvreject").show();
        //    $("#dvreject").show()
        //}
        //else {
        //    $('#btnnewentry').show();
        //    $("#lbldvreject").hide();
        //    $("#dvreject").hide()
        //}



        userid = document.getElementById("hdnuser").value;
        if (userid == "4133" || userid == "4137" || userid == "37284" || userid == "4135" || userid == "3882" || userid == "37241" || userid == "3894" || userid == "9038") {
            $('#btnnewentry').hide();
        }
        else {
            
            $('#btnnewentry').show();
        }
        cleardata();
    });

    $("#btnsearch").click(function (e) {
        bindpogrid();

    });

    $('#Vendorid').change(function () {
        var Productid = $("#Productid");
        $.ajax({
            type: "post",
            url: "/mmpo/Bindproduct",
            data: { Vendorid: $('#Vendorid').val() },
            datatype: "json",

            traditional: true,
            success: function (response) {
                Productid.empty().append('<option selected="selected" value="0">Please select</option>');
                $.each(response, function () {
                    Productid.append($("<option></option>").val(this['Productid']).html(this['Productname']));
                    $('#RATE').val('0');
                    $('#LASTRATE').val('0');
                    $('#MAXRATE').val('0');
                    $('#MINRATE').val('0');
                    $('#AVGRATE').val('0');
                    $('#Cgst').val('0');
                    $('#Sgst').val('0');
                    $('#Igst').val('0');
                });

                $("#Productid").chosen('destroy');
                $("#Productid").chosen({ width: '200px' });
                    
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    });

    $('#Productid').change(function () {
        var Productid = $("#Productid");
        var Podate = $("#Podate").val();
        var Packsize = $("#PACKSIZEID_FROM");
        var purchaserate = $("#RATE");
        var Vendorid = $("#Vendorid");
        $.ajax({
            type: "post",
            url: "/mmpo/PackSize_MrpTaxWihtRate",
            data: { Productid: $('#Productid').val(), Vendorid: $('#Vendorid').val(), Podate: $('#Podate').val() },

            datatype: "json",
            traditional: true,
            success: function (result) {
                var unit = result.packsizemrpunitDataset.pounit;
                var mrp = result.packsizemrpunitDataset.pomrp;
                var rate = result.packsizemrpunitDataset.porate;
                //alert(JSON.stringify(result.packsizemrpunitDataset.pounit));
                //alert(unit.length);
                var unitid = $("#PACKSIZEID_FROM");
                if (unit.length == 1) {
                    unitid.empty();
                    $.each(unit, function (index, record) {
                        //alert(this['PACKSIZEID_FROM']);
                        $('#PACKSIZEID_FROM').val(this['PACKSIZEID_FROM']);
                        unitid.append($("<option></option>").val(this['PACKSIZEID_FROM']).html(this['PACKSIZENAME_FROM']));
                    });

                   
                }
                else {

                    unitid.empty().append('<option selected="selected" value="0">Please select</option>');
                    $.each(unit, function (index, record) {
                        //alert(this['PACKSIZEID_FROM']);
                        $('#PACKSIZEID_FROM').val(this['PACKSIZEID_FROM']);
                        unitid.append($("<option></option>").val(this['PACKSIZEID_FROM']).html(this['PACKSIZENAME_FROM']));
                    });

                   
                }
                
                $.each(rate, function (index, record) {
                    rate = ' <label>' + record.toString() + '</label> <br/>';
                    $("#RATE").val(this['RATE']);
                });
                producttpumapcheck();

                $("#PACKSIZEID_FROM").chosen('destroy');
                $("#PACKSIZEID_FROM").chosen({ width: '150px' });

            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    });

    $("#btnnewentry").click(function (e) {
        debugger;
        $('#addnew').css("display", "");
        $('#showdata').css("display", "none");
        //document.getElementById('btnnewentry').style.visibility = 'hidden';
       
        Vendorandcurrencey();
        $("#Podate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
        $("#REQUIREDDate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
        $("#REQUIREDTODATE").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
        $("#Qutrefdate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());

        userid = document.getElementById("hdnuser").value;
        if (userid == "4133" || userid == "4137" || userid == "37284" || userid == "4135" || userid == "3882" || userid == "37241" || userid == "3894" || userid == "9038") {
            $('#btnsave').hide();
        }
        else {
           
            $('#btnsave').show();
        }
        

    })

    /*for download*/
    $("#InfoQuotationsubmit").click(function (e) {
        debugger;
        var mode = "1";
        document.getElementById("hdnfetchmode1").value = mode;
        document.getElementById("hdnpoid").value;
        FetchPoDocumentComparative(document.getElementById("hdnpoid").value , mode);
        $('#divuploadinfo').css("display", "");
    });
    /*for download*/
    $("#InfoComparativesubmit").click(function (e) {
        debugger;
        var mode = "2";
        document.getElementById("hdnfetchmode2").value = mode;
        document.getElementById("hdnpoid").value;
        FetchPoDocument(document.getElementById("hdnpoid").value, mode);
        $('#divuploadinfo').css("display", "");
    });

    /*for upload file*/
    $('#Quotationsubmit').click(function () {
    // Checking whether FormData is available in browser  
    if (window.FormData !== undefined) {
        debugger;
        var POID = document.getElementById("hdnpoid").value;
        var fileUpload = $("#txtquotupload").get(0);
        var files = fileUpload.files;
        // Create FormData object  
        var fileData = new FormData();
        fileData.append("POID", POID);

        // Looping over all files and add it to FormData object  
        for (var i = 0; i < files.length; i++) {
            fileData.append(files[i].name, files[i]);
        }

        $.ajax({
            url: "/mmpo/Quotationupload_file",
            type: "POST",
            contentType: false, // Not to set any content header  
            processData: false, // Not to process data  
            data: fileData,
            success: function (result) {
                toastr.warning(result);
            },
            error: function (err) {
                toastr.error(err.statusText);
            }
        });
    } else {
        toastr.error("FormData is not supported.");
    }
    });

    $('#Comparativesubmit').click(function () {
        debugger;
        // Checking whether FormData is available in browser  
        if (window.FormData !== undefined) {

            var POID = document.getElementById("hdnpoid").value;
            var fileUpload = $("#txtcompupload").get(0);
            var files = fileUpload.files;
            // Create FormData object  
            var fileData = new FormData();
            fileData.append("POID", POID);
            // Looping over all files and add it to FormData object  
            for (var i = 0; i < files.length; i++) {
                fileData.append(files[i].name, files[i]);
            }

            $.ajax({
                url: "/mmpo/Comparative_file",
                type: "POST",
                contentType: false, // Not to set any content header  
                processData: false, // Not to process data  
                data: fileData,
                success: function (result) {
                    toastr.warning(result);
                },
                error: function (err) {
                    toastr.error(err.statusText);
                }
            });
        } else {
            toastr.error("FormData is not supported.");
        }
    });

    // Get the <span> element that closes the modal
    var span = document.getElementsByClassName("close")[0];
    var btn = document.getElementById("InfoQuotationsubmit");
    var btn1 = document.getElementById("InfoComparativesubmit");
    var modal = document.getElementById("myModal");
    btn.onclick = function () {
        modal.style.display = "block";
    }
    btn1.onclick = function () {
        modal.style.display = "block";
    }
    // When the user clicks on <span> (x), close the modal
    span.onclick = function () {
        modal.style.display = "none";
    }
    window.onclick = function (event) {
        if (event.target == modal) {
            modal.style.display = "none";
        }
    }

    

});

function Vendorandcurrencey() {
    $.ajax({
        type: "POST",
        url: "/mmpo/Bindvendorcurrencey",
        data: '{}',
        async:false,
        dataType: "json",
        success: function (result) {
            var vendor = result.vendorcurrenceyDataset.povendor;
            var currencey = result.vendorcurrenceyDataset.pocurrecncy;
            var vendorid = $("#Vendorid");
            vendorid.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(vendor, function (index, record) {
                $('#Vendorid').val(this['Vendorname']);
                vendorid.append($("<option value=''></option>").val(this['Vendorid']).html(this['Vendorname']));
            });

            $("#Vendorid").chosen();
            $("#Vendorid").trigger("chosen:updated");
            
            var currenceyid = $("#Currencyid");
            currenceyid.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(currencey, function (index, record) {
                $('#Currencyid').val(this['Currencyid']);
                $('#Currencyid').val('396C1D95-EF9A-4689-9FE0-9856714BA02E');
                currenceyid.append($("<option value=''></option>").val(this['Currencyid']).html(this['CURRENCYTYPE']));
            });
           
            $("#Currencyid").chosen();
            $("#Currencyid").trigger("chosen:updated");
          

            

        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function getCurrentDate() {
    today_date = new Date();
    today_Date_Str = ((today_date.getDate() < 10) ? "0" : "") + String(today_date.getDate()) + "/" + ((today_date.getMonth() < 9) ? "0" : "") + String(today_date.getMonth() + 1) + "/" + today_date.getFullYear();
    return today_Date_Str;
} /*for cuurent date*/

function LoadTermsCondition() {
    $.ajax({
        type: "POST",
        url: "/mmpo/BindTermsCondition",
        data: '{}',
        async: false,
        dataType: "json",
        traditional: true,
        success: function (Terms) {

            if (Terms == "") {
                toastr.info('Terms and condition not avilable.Please Contact To your admin..');
                return false;
            }
            else {
                $.each(Terms, function (index, record) {
                    Termscondition = ' <label>' + record.toString() + '</label> <br/>';
                    $("#Termscondition").val(this['Termscondition']);
                });
            }
        }
    });

} /*laod terms and condition*/

function GETUSER() {
    debugger;
    $.ajax({
        type: "post",
        url: "/mmpo/getuserid",
        data: {},
        datatype: "json",
        async: false,
        traditional: true,
        success: function (USERID) {
            document.getElementById("hdnuser").value = USERID;
        },
    });
} /*userid from session*/

function GETFINYEAR() {
    debugger;
    $.ajax({
        type: "post",
        url: "/mmpo/getfinyear",
        data: {},
        datatype: "json",
        async: false,
        traditional: true,
        success: function (FINYEAR) {
            document.getElementById("hdnfinyear").value = FINYEAR;
            
        },
    });
} /*finyear from session*/

function producttpumapcheck() {
    $.ajax({
        type: "POST",
        url: "/mmpo/producttpumapcheck",
        data: { Productid: $('#Productid').val(), Vendorid: $('#Vendorid').val() },
        dataType: "json",
        async: false,
        traditional: true,
        success: function (check) {
            if (check == "") {
                toastr.info('Product Not Mapped with this depot');
                $('#RATE').val('0');
                $('#LASTRATE').val('0');
                $('#MAXRATE').val('0');
                $('#MINRATE').val('0');
                $('#AVGRATE').val('0');
                $('#Cgst').val('0');
                $('#Sgst').val('0');
                $('#Igst').val('0');
                return false;
            }
            else {
                bindallrate();
            }

        }
    });

}/*for product checking that mapped or not */

function bindallrate() {

    $.ajax({
        type: "POST",
        url: "/mmpo/Bindmaxminlastrate",
        data: { Productid: $('#Productid').val(), Vendorid: $('#Vendorid').val(), Podate: $('#Podate').val() },
        dataType: "json",
        async: false,
        traditional: true,
        success: function (result) {
            var lastrate = result.allrateDataset.polasrate;
            var maxrate = result.allrateDataset.pomaxrate;
            var minrate = result.allrateDataset.pominrate;
            var avgrate = result.allrateDataset.poavgrate;

            $.each(lastrate, function (index, record) {
                lastrate = ' <label>' + record.toString() + '</label> <br/>';
                $("#LASTRATE").val(this['LASTRATE']);
            });
            if (this['LASTRATE'] == "") {
                toastr.warning("There is no details found in Previous Purchase");
            }

            $.each(maxrate, function (index, record) {
                maxrate = ' <label>' + record.toString() + '</label> <br/>';
                $("#MAXRATE").val(this['MAXRATE']);
            });

            $.each(minrate, function (index, record) {
                minrate = ' <label>' + record.toString() + '</label> <br/>';
                $("#MINRATE").val(this['MINRATE']);
            });

            $.each(avgrate, function (index, record) {
                avgrate = ' <label>' + record.toString() + '</label> <br/>';
                $("#AVGRATE").val(this['AVGRATE']);
            });
            bindtax();

        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}/*for max min avg last rate*/

function bindtax() {
    $.ajax({
        type: "POST",
        url: "/mmpo/Bindtax",
        data: { Productid: $('#Productid').val(), Vendorid: $('#Vendorid').val() },
        dataType: "json",
        traditional: true,
        async: false,
        success: function (taxpercentage) {
            $.each(taxpercentage, function (key, item) {


                if (item.CGSTTAXID == "DC552F34-827B-4FA7-95C2-20A2B6211B11" && item.CGSTPERCENTAGE > 0) {
                    $("#Cgst").val(item.CGSTPERCENTAGE);
                    $("#CGSTTAXID").val(item.CGSTTAXID);
                    $("#Sgst").val(item.SGSTPERCENTAGE);
                    $("#SGSTTAXID").val(item.SGSTTAXID);
                }
                else if (item.IGSTTAXID == "8C60D11D-9524-4DC4-AA9B-AF956C52E41F" && item.IGSTPERCENTAGE > 0) {
                    $("#Igst").val(item.IGSTPERCENTAGE);
                    $("#IGSTTAXID").val(item.IGSTTAXID);

                }

            });
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
};/*for tax*/

function Calculateamount() {
    debugger;
    var totalqty = 0;
    var totalgross = 0;
    var totalmrp = 0;
    var rowCount = document.getElementById("podetail").rows.length - 1;
    if (rowCount > 0) {
        $('#podetail tbody tr').each(function () {
            totalqty += parseFloat($(this).find('td:eq(4)').html()) * parseFloat($(this).find('td:eq(8)').html().trim());
        });


        $('#Totalbasicvalue').val(totalqty.toFixed(2));
        $('#Totalgross').val(totalqty.toFixed(2));
        $('#Totalmrp').val(totalmrp.toFixed(2));
      

    }
    else {
        $('#Totalbasicvalue').val(0);
        $('#Totalgross').val(0);
        $('#Totalmrp').val(0);

    }
} /*for calculate amount after add button click*/

function getdiscpercentage(a) {/*total net amount subtract from discount*/
    debugger;
    var totalgross = 0;
    var percentage = 0;
    var discountper = 0;
    var basicvalue = 0;
    var idtotalamount = $('#Totalgross').val();
    var iddiscper = $('#Discper').val();
    var iddiscamnt = $('#Discamnt').val();
    var idTotalbasic = $('#Totalgross').val();

    discountper = iddiscper;
    basicvalue = idTotalbasic;

    if (discountper > 0) {
        totalgross = idtotalamount;
        percentage = ((totalgross / 100) * discountper);
        basicvalue = totalgross - percentage;
        iddiscamnt = percentage;
        $('#Totalgross').val(totalgross);
        $('#Totalbasicvalue').val(basicvalue);
        $('#Discamnt').val(percentage);
    }
    else {
        totalgross = idtotalamount;
        percentage = ((totalgross / 100) * discountper);
        basicvalue = totalgross + percentage;
        iddiscamnt = percentage;
        $('#Totalgross').val(totalgross);
        $('#Totalbasicvalue').val(basicvalue);
        $('#Discamnt').val(percentage);
    }

} /*for discount percentage*/

function bindpogrid() {
    debugger;
    var QSTAG = "";
    var qs = getQueryStrings();
    var userid = document.getElementById("hdnuser").value;
    if (qs["INDENT"] != undefined && qs["INDENT"] != "") {

        var INDENT = qs["INDENT"];
    }
    if (INDENT == "Y")
    {
        QSTAG = "Y";
    }
    else 
    {
        QSTAG = "N";
    }
    
    $.ajax({
        type: "POST",
        url: "/mmpo/BindPoGrid",
        data: { Fromdate: $('#Fromdate').val(), Todate: $('#Todate').val(), QSTAG:QSTAG, CHECKER: "" ,Potype: $('#Potype').val(), },
        dataType: "json",
        async: false,
        success: function (response) {
            var tr;
            tr = $('#PodetailsGrid');

            tr.append("<thead><tr><th style='display:none'>POID</th><th>PONO</th><th>PODATE</th><th>VENDORNAME</th><th>STATUS</th><th>CREATEDFROM</th><th>Edit</th><th>Delete</th><th>Print</th></tr></thead><tbody>");
            $('#PodetailsGrid').DataTable().destroy();
            $("#PodetailsGrid tbody tr").remove();

            //for grid table
            for (var i = 0; i < response.length; i++) {
                tr = $('<tr/>');
                tr.append("<td style='display: none'>" + response[i].POID + "</td>");
                tr.append("<td>" + response[i].PONO + "</td>");
                tr.append("<td>" + response[i].PODATE + "</td>");
                tr.append("<td>" + response[i].VENDORNAME + "</td>");
                tr.append("<td>" + response[i].ISVERIFIEDDESC + "</td>");
                tr.append("<td>" + response[i].CREATEDFROM + "</td>");
                tr.append("<td> <input type='image' class='gvdwn'  id='btnedit' '<img src='../Images/ico_edit_16.png' width='20' height ='20' title='Edit'/></td>");
               
                if (CHECKER == "TRUE") {
                    tr.append("<td> </td>");
                }
                else {
                    tr.append("<td> <input type='image' class='gvdelete'  id='btnPodelete' '<img src='../Images/ico_delete_16.png' width='20' height ='20' title='Delete'/></td>");
                }
                

               

                


                tr.append("<td> <input type='image' class='gvprint'  id='btnPoPrint' '<img src='../Images/print.png' width='20' height ='20' title='Print'/></td>");

                $("#PodetailsGrid").append(tr);

            }
            //for background color change
            $('#PodetailsGrid tr').each(function (index) {
                var row = $(this).closest("tr");
                var status = row.find('td:eq(4)').html();
                if (status == "PENDING") {
                    row.css("background-color", "#a8c3ed");
                }
                else if (status == "CONFIRMED") {
                    row.css("background-color", "#ddffbf");
                }
                else if (status == "REJECTED") {
                    row.css("background-color", "#ffb3b8");
                }
                else if (status == "WAIT FOR APPROVED") {
                    row.css("background-color", "#edd2fa");
                }
                else if (status == "APPROVED") {
                    row.css("background-color", "#c0fadc");
                }
                else if (status == "HOLD") {
                    row.css("background-color", "#f5d2a6");
                }
                else {
                    row.css("background-color", "#b8b6b6");
                }
            });

            tr.append("</tbody>");

            $('#PodetailsGrid').DataTable({
                "scrollY": "238px",
                "scrollCollapse": true,
                "dom": 'l<"toolbar">frtip',
                "initComplete": function (settings, json) {
                    $('.dataTables_scrollBody thead tr').css({ visibility: 'collapse' });// this gets rid of duplicate headers
                },
                "scrollXInner": true,
                "bRetrieve": false,
                "bFilter": true,
                "bSortClasses": false,
                "bLengthChange": false,
                "bInfo": true,
                "serverSide": false,
                "bAutoWidth": false,
                "aaSorting": false,
                "paging": true,

            });
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}/*for search event*/

function myFunction() {
    $('#addnew').css("display", "");
    $('#showdata').css("display", "none");
    $('#ponoshow').css("display", "");
    document.getElementById('btnnewentry').style.visibility = 'hidden';

}; /*this is used to hide and show from grid edit button*/

$(function () {
    $("body").on("click", "#PodetailsGrid .gvdwn", function () {

       
        row = $(this).closest("tr");
        poid = row.find('td:eq(0)').html();
        document.getElementById("hdnpoid").value = poid;
        status = row.find('td:eq(4)').html();
        userid = document.getElementById("hdnuser").value;
        if (status == "PENDING" && (userid == "4133" || userid == "4137" || userid == "37284")) { /*for MAYANK and PKVERMA*/
            $('#btnApprove').css("display", "");
            $('#btnReject').css("display", "");
            $('#btnHoldApproved').css("display", "none");
            $('#btnConfirmed').css("display", "none");
            $('#btnsave').css("display", "none");
            $("#lbldvreject").show();
            $("#dvreject").show()
        }
        else if (status == "HOLD" && (userid == "4133" || userid == "4137" || userid == "37284")) { /*for MAYANK and PKVERMA*/
            $('#btnHoldApproved').css("display", "");
            $('#btnReject').css("display", "");
            $('#btnApprove').css("display", "none");
            $('#btnsave').css("display", "none");
            $('#btnConfirmed').css("display", "none");
            $("#lbldvreject").show();
            $("#dvreject").show()
            
        }
        else if (status == "CONFIRMED" && (userid == "3882" || userid == "4135" || userid == "37241")) { /*for VKSARDA and GLA*/
            $('#btnHoldApproved').css("display", "none");
            $('#btnReject').css("display", "");
            $('#btnApprove').css("display", "none");
            $('#btnsave').css("display", "none");
            $('#btnConfirmed').css("display", "");
            $("#lbldvreject").show();
            $("#dvreject").show()
        }
        else if (status == "WAIT FOR APPROVED" && (userid == "3894" || userid == "9038")) {
            $('#btnHoldApproved').css("display", "none");
            $('#btnReject').css("display", "");
            $('#btnApprove').css("display", "");
            $('#btnsave').css("display", "none");
            $('#btnConfirmed').css("display", "none");
            $("#lbldvreject").show();
            $("#dvreject").show()
        }
        else if (status == "PENDING" && (userid == "3894" || userid == "9038" || userid == "3882" || userid == "4135" || userid == "37241")) {
            $('#btnHoldApproved').css("display", "none");
            $('#btnReject').css("display", "none");
            $('#btnApprove').css("display", "none");
            $('#btnsave').css("display", "none");
            $('#btnConfirmed').css("display", "none");
        }
        else if (status == "REJECTED" && (userid == "3894" || userid == "9038" || userid == "3882" || userid == "4135" || userid == "37241" || userid == "4133" || userid == "4137" || userid == "37284")) {
            $('#btnHoldApproved').css("display", "none");
            $('#btnReject').css("display", "none");
            $('#btnApprove').css("display", "none");
            $('#btnsave').css("display", "none");
            $('#btnConfirmed').css("display", "none");
            $("#lbldvreject").show();
            $("#dvreject").show()
        }
        else if (status == "CONFIRMED" || status == "WAIT FOR APPROVED" || status == "APPROVED") {
            $('#btnsave').css("display", "none");
            $('#btnApprove').css("display", "none");
            $('#btnReject').css("display", "none");
            $('#btnHoldApproved').css("display", "none");
            $('#btnConfirmed').css("display", "none");
            $("#lbldvreject").show();
            $("#dvreject").show()
        }
        else {
            $('#btnsave').css("display", "");
            $('#btnApprove').css("display", "none");
            $('#btnReject').css("display", "none");
            $('#btnHoldApproved').css("display", "none");
            $('#btnConfirmed').css("display", "none");
            $("#lbldvreject").hide();
            $("#dvreject").hide()
             }

        
        $("#Productid").chosen('destroy');
        $("#Productid").chosen({ width: '230px' });

        $("#Vendorid").chosen('destroy');
        $("#Vendorid").chosen({ width: '200px' });

        Vendorandcurrencey();
        EditHeaderandFooterRecord(poid);

       
        $("#Vendorid").prop("disabled", true);
        $("#Vendorid").chosen({
            search_contains: true
        });
        $("#Vendorid").trigger("chosen:updated");

        

        $("#Productid").prop("disabled", false);
        $("#Productid").chosen({
            search_contains: true
        });
        $("#Productid").trigger("chosen:updated");

        $('#addnew').css("display", "");
        $('#ponoshow').css("display", "");
        $('#showdata').css("display", "none");
        $('#btnnewentry').css("display", "none");
        $('#ponoshow').css("display", "");

        /*approval logic*/
        $("#btnApprove").click(function (e) {
            var mode = "1";
            approvedpo(poid, mode);
           
           

        });
        $("#btnReject").click(function (e) {
            var mode = "2";
            var rejection = $("#REJECTIONNOTE").val();
            if (rejection == "") {
                toastr.warning("Please give a reason for rejection");
            }
            else {
                approvedpo(poid, mode);
            }
        });
        $("#btnConfirmed").click(function (e) {
            var mode = "3";
            approvedpo(poid, mode);
           
          
        });
        $("#btnHoldApproved").click(function (e) {
            var mode = "4";
            approvedpo(poid, mode);
           
           
        });
        /*approval logic*/
    })
}) /*for edit */

$(function () {
    $("body").on("click", "#PodetailsGrid .gvdelete", function () {
        debugger;
        row = $(this).closest("tr");
        poid = row.find('td:eq(0)').html();
        status = row.find('td:eq(4)').html();
        if (status == "PENDING") {
            if (confirm("Do you want to delete this file?")) {
                $.ajax({
                    type: "POST",
                    url: "/mmpo/DeletePo",
                    data: { poid: poid },
                    dataType: "json",
                    success: function (purchaseOrder) {
                        debugger;
                        if (purchaseOrder != "") {
                            toastr.success("Delete Successfull");
                            bindpogrid();
                        }
                        else {

                            toastr.warning("Error On Delete Record");
                        }

                    },
                });
            }
        }
        else {
            toastr.warning("You Can't Delete this Po");
            return false;
        }
    })
}) /*for delete*/

function EditHeaderandFooterRecord(poid) {
    $.ajax({
        type: "POST",
        url: "/mmpo/EditPurchaseOrder",
        data: { poid: poid },
        async: false,
        dataType: "json",
        success: function (result) {

            var headerfooter = result.podetailsDataset.editpos;
            var pogrid = result.podetailsDataset.podetails;
            debugger;
            $.each(headerfooter, function (key, item) {
                $("#Vendorid").val(item.VENDORID);
                $("#Vendorname").val(item.VENDORNAME);
                $("#Totalbasicvalue").val(item.NETTOTAL);
                $("#Totalmrp").val(item.MRPTOTAL);
                $("#Totaladjusment").val(item.ADJUSTMENT);
                $("#Discper").val(item.DISCOUNTPERCENTAGE);
                $("#Discamnt").val(item.DISCOUNT);
                $("#Totalgross").val(item.GROSSTOTAL);
                $("#Shippingadress").val(item.SHIPPING_ADRESS);
                $("#Termscondition").val(item.TERMS_CONDITION);
                $("#Remarks").val(item.REMARKS);
                $("#Currencyid").val(item.CURRENCYID);
                $("#CURRENCYTYPE").val(item.CURRENCYTYPE);
                $("#Poid").val(item.Poid);
                $("#PONO").val(item.PONO);
                $("#Podate").val(item.PODATE);
            });
          
            $.each(pogrid, function (key, item) {

                $("#podetail").empty();
                if (pogrid.length > 0) {
                    debugger;
                    var tr;
                    tr = $('#podetail');
                    tr.append("<thead><tr><th>Sl.No.</th><th style='display :none'>POID</th><th style='display:none'>productid</th><th>Material</th><th>Qty</th><th style='display:none'>unitid</th><th>Unit</th><th>Purchase Rate</th><th>Basic Value</th><th>Last Rate</th><th>Max Rate</th><th>Avg Rate</th><th>Min Rate</th><th style='display:none'>cgstid</th><th>Cgst</th><th style='display:none'>sgstid</th><th>Sgst</th><th style='display:none'>igstid</th><th>Igst</th><th>MRP</th><th>MRP Value</th><th>Delivary From Date</th><th>Delivary To Date</th><th>Delete</th></tr></thead><tbody>");
                    $('#podetail').DataTable().destroy();
                    $("#podetail tbody tr").remove();
                    for (var i = 0; i < pogrid.length; i++) {
                        tr = $('<tr/>');
                        tr.append("<td style='display: none'>" + pogrid[i].POID + "</td>");
                        tr.append("<td>" + pogrid[i].slno + "</td>");
                        tr.append("<td style='display: none'>" + pogrid[i].PRODUCTID + "</td>");
                        tr.append("<td>" + pogrid[i].PRODUCTName + "</td>");
                        tr.append("<td>" + pogrid[i].QTY + "</td>");
                        tr.append("<td style='display: none'>" + pogrid[i].UOMID + "</td>");
                        tr.append("<td>" + pogrid[i].UOMName + "</td>");
                        tr.append("<td>" + pogrid[i].RATE + "</td>");
                        tr.append("<td>" + pogrid[i].RATE + "</td>");
                        tr.append("<td>" + pogrid[i].LASTRATE + "</td>");
                        tr.append("<td>" + pogrid[i].MAXRATE + "</td>");
                        tr.append("<td>" + pogrid[i].AVGRATE + "</td>");
                        tr.append("<td>" + pogrid[i].MINRATE + "</td>");
                        tr.append("<td style='display: none'>" + pogrid[i].CGSTTAXID + "</td>");
                        tr.append("<td>" + pogrid[i].Cgst + "</td>");
                        tr.append("<td style='display: none'>" + pogrid[i].SGSTTAXID + "</td>");
                        tr.append("<td>" + pogrid[i].Sgst + "</td>");
                        tr.append("<td style='display: none'>" + pogrid[i].IGSTTAXID + "</td>");
                        tr.append("<td>" + pogrid[i].Igst + "</td>");
                        tr.append("<td>" + pogrid[i].MRP + "</td>");
                        tr.append("<td>" + pogrid[i].TOTMRP + "</td>");
                        tr.append("<td>" + pogrid[i].REQUIREDDate + "</td>");
                        tr.append("<td>" + pogrid[i].REQUIREDTODATE + "</td>");

                        tr.append("<td> <input type='image' class='gvdwndelete'  id='btndelete' '<img src='../Images/ico_delete_16.png' width='20' height ='20' title='Delete'/></td>");

                        $("#podetail").append(tr);
                        updateRowCount();
                        debugger;
                        Calculateamount();
                        bindproduct();
                    }

                }
            });
        }
    });
} /*edit function*/

function approvedpo(poid, mode) {
    $.ajax({
        type: "POST",
        url: "/mmpo/ApprovedPo",
        data: { poid: poid, mode: mode },
        dataType: "json",
        success: function (purchaseOrder) {
            if (purchaseOrder != "") {
                toastr.success('Update Done');
                bindpogrid();
                $('#addnew').css("display", "none");
                $('#showdata').css("display", "");
                $('#ponoshow').css("display", "none");
               
            }
            else {
                toastr.error('Error On updating');
                bindpogrid();
                $('#addnew').css("display", "none");
                $('#showdata').css("display", "");
                $('#ponoshow').css("display", "none");
                
            }
        }
    });
} /*for approved*/

$("body").on("click", "#podetail #btndelete", function () {
    var row = $(this).closest("tr");
    if (confirm("Do you want to delete this product?")) {
        row.remove();
        
        var rowCount = document.getElementById("podetail").rows.length - 1;
        if (rowCount > 0) {
            $("#Vendorid").prop("disabled", true);
            $("#Vendorid").chosen({
                search_contains: true
            });
            $("#Vendorid").trigger("chosen:updated");
        }
        else {

            $("#Vendorid").prop("disabled", false);
            $("#Vendorid").chosen({
                search_contains: true
            });
            $("#Vendorid").trigger("chosen:updated");
        }
        updateRowCount();
        Calculateamount();
        
    }
})/*for temp delete*/

function myFunctionfordelete1() {
    var UPLOADID;
    debugger;
    $("body").on("click", "#uploadinfogrid .gvdwndelete", function () {
        row = $(this).closest("tr");
        UPLOADID = row.find('td:eq(0)').html();
        var result = confirm('You Wnat to delete this File?');
        if (result) {
            row.remove();
            DeleteComplain(UPLOADID);
        }
    });
}

function updateRowCount() {
    var table = document.getElementById("podetail");
    var rowcountAfterDelete = document.getElementById("podetail").rows.length;
    for (var i = 1; i < rowcountAfterDelete; i++) {
        table.rows[i].cells[0].innerHTML = i;
    }

    var rowCount = document.getElementById("podetail").rows.length - 1;
    if (rowCount > 0) {
        $("#Vendorid").prop("disabled", true);

    }
    else {
        $('#podetail').empty();
        $("#Vendorid").prop("disabled", false);

    }
} /*updqate row number*/
 
function savePodata() {
    var i = 0;
    polist = new Array();
    $('#podetail tbody tr').each(function () {
        var podetail = {};
        var productid = $(this).find('td:eq(2)').html();
        var productname = $(this).find('td:eq(3)').html();
        var qty = $(this).find('td:eq(4)').html();
        var unitid = $(this).find('td:eq(5)').html();
        var unit = $(this).find('td:eq(6)').html();
        var purchaserate = $(this).find('td:eq(7)').html();
        var basicvalue = $(this).find('td:eq(8)').html();
        var lastrate = $(this).find('td:eq(9)').html();
        var maxrate = $(this).find('td:eq(10)').html();
        var avgrate = $(this).find('td:eq(11)').html();
        var minrate = $(this).find('td:eq(12)').html();
        var cgsttaxid = $(this).find('td:eq(13)').html();
        var cgst = $(this).find('td:eq(14)').html();
        var sgsttaxid = $(this).find('td:eq(15)').html();
        var sgst = $(this).find('td:eq(16)').html();
        var igsttaxid = $(this).find('td:eq(17)').html();
        var igst = $(this).find('td:eq(18)').html();
        var mrp = $(this).find('td:eq(19)').html();
        var mrpvalue = $(this).find('td:eq(20)').html();
        var delevarifromdate = $(this).find('td:eq(21)').html();
        var delivarytodate = $(this).find('td:eq(21)').html();



        podetail.Productid = productid;
        podetail.Productname = productname;
        podetail.QTY = qty;
        podetail.Unitid = unitid;
        podetail.Unit = unit;
        podetail.RATE = purchaserate;
        podetail.Basicvalue = basicvalue;
        podetail.Lastrate = lastrate;
        podetail.Maxrate = maxrate;
        podetail.Avgrate = avgrate;
        podetail.Minrate = minrate;
        podetail.CGSTTAXID = cgsttaxid;
        podetail.Cgst = cgst;
        podetail.SGSTTAXID = sgsttaxid;
        podetail.Sgst = sgst;
        podetail.IGSTTAXID = igsttaxid;
        podetail.Igst = igst;
        podetail.Mrp = mrp;
        podetail.Mrpvalue = mrpvalue;
        podetail.REQUIREDDate = delevarifromdate;
        podetail.REQUIREDTODATE = delivarytodate;

        polist[i++] = podetail;
    });

    debugger;
    var posave = {};
    posave.Podate = $("#Podate").val();
    posave.Vendorid = $("#Vendorid").val();
    posave.Vendorname = $("#Vendorname").val();
    posave.Shippingadress = $("#Shippingadress").val();
    posave.Remarks = $("#Remarks").val();
    posave.Totalbasicvalue = $("#Totalbasicvalue").val();
    posave.Totalmrp = $("#Totalmrp").val();
    posave.Totaladjusment = $("#Totaladjusment").val();
    posave.Discper = $("#Discper").val();
    posave.Discamnt = $("#Discamnt").val();
    posave.Totalgross = $("#Totalgross").val();
    posave.Termscondition = $("#Termscondition").val();
    posave.Currencyid = $("#Currencyid").val();
    posave.CURRENCYTYPE = $("#CURRENCYTYPE").val();
    
    if (posave.CURRENCYTYPE  == "")
    {
        posave.CURRENCYTYPE = "INR";
    }
    else
    {
        posave.CURRENCYTYPE = $("#CURRENCYTYPE").val();
    }
    debugger;
    posave.Poid = $("#Poid").val();

    if (polist.length == 0) {
        toastr.info("Please Select atleast one record");
        return;
    }
    posave.PurchaseOrderdetails = polist,
       // alert(JSON.stringify(posave));
        $.ajax({
            url: "/mmpo/posavedata",
            //data: JSON.stringify(nccModel),
            data: '{posave:' + JSON.stringify(posave) + '}',
            type: "POST",
            contentType: "application/json",
            contentType: "application/json;charset=utf-8",
            // dataType: "json",
            success: function (responseMessage) {
                var messageid;
                var messagetext;
                $.each(responseMessage, function (key, item) {
                    messageid = item.MessageID;
                    messagetext = item.MessageText;
                });
                if (messageid != "") {
                    $('#addnew').css("display", "none");
                    $('#showdata').css("display", "");
                    $('#ponoshow').css("display", "none");
                    $('#btnnewentry').show();
                    bindpogrid();
                    toastr.success("Your Pono Is" + ':' + messagetext);
                }
                else {
                    
                    toastr.error("Error On Saving Record" + ':' + messagetext);
                }
                
            },
            error: function (errormessage) {
                // alert(errormessage.responseText);
            }
        });
} /*save method*/

function bindproduct() {
    debugger;
    var Productid = $("#Productid");;
    $.ajax({
        type: "post",
        url: "/mmpo/Bindproduct",
        data: { Vendorid: $('#Vendorid').val() },
        datatype: "json",
        async: false,
        traditional: true,
        success: function (response) {
            Productid.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(response, function () {
                Productid.append($("<option></option>").val(this['Productid']).html(this['Productname']));
                $("#Igst").val('');
                $("#Sgst").val('');
                $("#Cgst").val('');
            });
           
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}; /*bind product*/

function cleardata() {
    $('#podetail tbody tr').remove();
    //$("#Vendorid").empty();
    //$("#Productid").empty();
    $('#Totalbasicvalue').val(0);
    $('#Totalgross').val(0);
    $('#Totalmrp').val(0);
    $('#PONO').val('');
    $("#Poid").val('');
    $('#Podate').val('');
    $('#REQUIREDDate').val('');
    $('#REQUIREDTODATE').val('');
    $('#Qutrefdate').val('');
    //$("#Vendorid").prop("disabled", false);
    $("#txtquotupload").val('');
    $("#txtcompupload").val('');
    Vendorandcurrencey();
   
    $("#Vendorid").val('0');
    $("#Vendorid").prop("disabled", false);
    $("#Vendorid").chosen({
        search_contains: true
    });
    $("#Vendorid").trigger("chosen:updated");

    $("#Productid").val('0');
    //$("#Productid").prop("disabled", true);
    //$("#Productid").chosen({
    //    search_contains: true
    //});
    //$("#Productid").trigger("chosen:updated");

} /*for clearing data*/

function getQueryStrings() {
    try {
        debugger;
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

function FetchPoDocumentComparative() {
    debugger;
    var POID = document.getElementById("hdnpoid").value;
    if (POID == "") {
        toastr.warning('Sorry.No record found');
        return;
    }
    var mode = $('#hdnfetchmode1').val();
    if (mode == "") {
        var mode = $('#hdnfetchmode2').val();
    }
    $.ajax({
        type: "POST",
        url: "/mmpo/FetchPoDocumentComparative",
        data: { poid: POID, mode: mode },
        dataType: "json",
        success: function (response) {
            var tr;
            debugger;
            tr = $('#uploadinfogrid');

            $('#uploadinfogrid').DataTable().destroy();
            $("#uploadinfogrid tbody tr").remove();

            for (var i = 0; i < response.length; i++) {
                tr = $('<tr/>');
                tr.append("<td >" + response[i].UPLOADID + "</td>");
                tr.append("<td style='display: none'>" + response[i].POID + "</td>");
                tr.append("<td>" + response[i].UPLOADDATE + "</td>");
                tr.append("<td>" + response[i].UPLOADFILENAME + "</td>");
                tr.append("<td><a href='" + response[i].UPLOADFILEPATH + "'</a> <img src='../Images/download.png' width='20' height ='20' title='Download'</td>");
                tr.append("<td> <input type='image' class='gvdwndelete'  id='btndelete' onclick='myFunctionfordelete()'<img src='../Images/ico_delete_16.png' width='20' height ='20' title='Delete'/></td>");
                $("#uploadinfogrid").append(tr);

            }
            tr.append("</tbody>");
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function FetchPoDocument() {
    var POID = document.getElementById("hdnpoid").value;
    if (POID == "") {
        toastr.warning('Sorry.No record found');
        return;
    }
    var mode = "2";
    $.ajax({
        type: "POST",
        url: "/mmpo/FetchPoDocumentComparative",
        data: { poid: POID, mode: mode },
        dataType: "json",
        success: function (response) {
            var tr;
            debugger;
            tr = $('#uploadinfogrid');

            $('#uploadinfogrid').DataTable().destroy();
            $("#uploadinfogrid tbody tr").remove();
            debugger;
            for (var i = 0; i < response.length; i++) {
                tr = $('<tr/>');
                tr.append("<td >" + response[i].UPLOADID + "</td>");
                tr.append("<td style='display: none'>" + response[i].POID + "</td>");
                tr.append("<td>" + response[i].UPLOADDATE + "</td>");
                tr.append("<td>" + response[i].UPLOADFILENAME + "</td>");
                tr.append("<td><a href='" + response[i].UPLOADFILEPATH + "'</a> <img src='../Images/download.png' width='20' height ='20' title='Download'</td>");
                tr.append("<td> <input type='image' class='gvdwndelete'  id='btndelete' onclick='myFunctionfordelete1()'<img src='../Images/cross.png' width='20' height ='20' title='Delete'/></td>");
                $("#uploadinfogrid").append(tr);

            }
            tr.append("</tbody>");
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function myFunctionfordelete() {
    var UPLOADID;
    debugger;
    $("body").on("click", "#uploadinfogrid .gvdwndelete", function () {
            row = $(this).closest("tr");
            UPLOADID = row.find('td:eq(0)').html();
            var result = confirm('You Wnat to delete this File?');
            if (result) {
                row.remove();
                DeleteComplain(UPLOADID);
            }
    });
}

function myFunctionfordelete1() {
    var UPLOADID;
    debugger;
    $("body").on("click", "#uploadinfogrid .gvdwndelete", function () {
        row = $(this).closest("tr");
        UPLOADID = row.find('td:eq(0)').html();
        var result = confirm('You Wnat to delete this complain?');
        if (result) {
            row.remove();
            DeleteComplain(UPLOADID);
        }
    });
}

function DeleteComplain(UPLOADID) {
    if (confirm("Do you want to delete this file?")) {
        $.ajax({
            type: "POST",
            url: "/mmpo/DeleteUploadfile",
            data: { uploadid: UPLOADID },
            dataType: "json",
            success: function (deletecomp) {
                $.each(deletecomp, function (key, item) {
                    toastr.success('file delete Sucessfully');
                });
            }
        });
    }
}

$(function () {
    var poid;
    debugger; 
   
    //var menuid = "29";
    var menuid = $("#hdnmenuid").val();
    $("body").on("click", "#PodetailsGrid .gvprint", function () {
        
        var row = $(this).closest("tr");
        poid = row.find('td:eq(0)').html();
        
        var url = "http://mcnroeerp.com/factory/FACTORY/frmRptInvoicePrint_FAC.aspx?PurchaseOrderId=" + poid + "&TAG=PO&MenuId=" + menuid;
        window.open(url, "", "width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100");

    })

}) /*For Print*/

function getFinyearWiseDate() {
    debugger;
    finyear = document.getElementById("hdnfinyear").value;
    var startyear = finyear.substring(0, 4);
    var endyear = finyear.substring(5, 9);
    var today1 = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate());
    yearstart = new Date(startyear, 03, 01);
    startdate = ("0" + 1 + "/" + "0" + 4 + "/" + startyear);
    enddate = (31 + "/" + "0" + 3 + "/" + endyear);
    today = new Date(endyear, 02, 31);
}   /*for finyearwise date*/







