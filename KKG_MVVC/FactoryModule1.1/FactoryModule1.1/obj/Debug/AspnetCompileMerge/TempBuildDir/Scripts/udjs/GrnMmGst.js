/*
 *Developer Name :Pritam Basu 
 * Start Date:08.04.2020
 * End Date1:20.04.2020
 * End Date2:02.05.2020
 * Page Description:GRN MM GST
 * Page Description:GRN MM(Stock In)
 */

var CHECKER;
var FG;
var OP;
var TPUFLAG = "FALSE";
var CheckerFlag = "";
var yearstart = "";
var startdate = "";
var enddate = "";
var today = "";
var GLMENUID;

$(document).ready(function () {

    var qs = getQueryStrings();
    if (qs["CHECKER"] != undefined && qs["CHECKER"] != "") {
        CHECKER = qs["CHECKER"];
        OP = qs["OP"];;
        CheckerFlag = qs["CHECKER"];;
        FG = qs["FG"];;
    }
    if (qs["MENUID"] != undefined && qs["MENUID"] != "") {
        GLMENUID = qs["MENUID"];
        CHECKER = qs["CHECKER"];
        OP = qs["OP"];;
        CheckerFlag = qs["CHECKER"];;
        FG = qs["FG"];;
    }

    GETFINYEAR();
    getFinyearWiseDate();

    if (OP == "QC") {
        $('#addnew').css("display", "none");
        $('#addQcUpload').css("display", "none");
        $('#showdata').css("display", "");
        $("#hdnISVERIFIEDCHECKER1").val('N');
        $("#hdnSTOCKIN").val('Y');
        $('#btnnewentry').hide();
        $('#divuploadinfo').css("display", "none");
        $('#divSampleQty').css("display", "none");
    }
    else if (OP = "MAKER") {
        $('#addnew').css("display", "none");
        $('#addQcUpload').css("display", "none");
        $('#showdata').css("display", "");
        $("#hdnISVERIFIEDCHECKER1").val('N');
        $("#hdnSTOCKIN").val('N');
        $('#btnnewentry').show();
        $('#divuploadinfo').css("display", "none");
        $('#divSampleQty').css("display", "none");

    }
    else {
        $('#addnew').css("display", "none");
        $('#addQcUpload').css("display", "none");
        $('#showdata').css("display", "");
        $('#divuploadinfo').css("display", "none");
        $('#divSampleQty').css("display", "none");
    }

    /*calender*/
    $("#DESPATCHDATE").datepicker({
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

    $("#LRGRDATE").datepicker({
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

    $("#INVOICEDATE").datepicker({
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

    $("#GATEPASSDATE").datepicker({
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

    $("#MFGDATE").datepicker({
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        selectCurrent: true,
        todayBtn: "linked",
        showAnim: "slideDown",

        dateFormat: "dd/mm/yy",
        showOn: 'button',
        buttonText: 'Show Date',
        buttonImageOnly: true,
        buttonImage: 'http://jqueryui.com/resources/demos/datepicker/images/calendar.gif'
    });
    $(".ui-datepicker-trigger").mouseover(function () {
        $(this).css('cursor', 'pointer');
    });

    $("#EXPDATE").datepicker({
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        selectCurrent: true,
        todayBtn: "linked",
        showAnim: "slideDown",

        dateFormat: "dd/mm/yy",
        showOn: 'button',
        buttonText: 'Show Date',
        buttonImageOnly: true,
        buttonImage: 'http://jqueryui.com/resources/demos/datepicker/images/calendar.gif'
    });
    $(".ui-datepicker-trigger").mouseover(function () {
        $(this).css('cursor', 'pointer');
    });

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

    $("#DESPATCHDATE").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
    $("#LRGRDATE").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
    $("#INVOICEDATE").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
    $("#GATEPASSDATE").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
    $("#Fromdate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
    $("#Todate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());


    LoadGRN();
    LoadTpu($("#Fromdate").val(), $("#Todate").val());
    LoadTpu_Transporter();

    /*method call in pageload*/
    $("#VENDORID").chosen();
    $("#VENDORID").trigger("chosen:updated");
    $("#MODEOFTRANSPORT").chosen();
    $("#MODEOFTRANSPORT").trigger("chosen:updated");
    $("#TRANSPORTERID").chosen();
    $("#TRANSPORTERID").trigger("chosen:updated");
    $("#VENDORFROM").chosen();
    $("#VENDORFROM").trigger("chosen:updated");
    $("#POID").chosen();
    $("#POID").trigger("chosen:updated");
    $("#MATERIALID").chosen();
    $("#MATERIALID").trigger("chosen:updated");




    $("#VENDORFROM").change(function () {
        debugger;
        LoadTpu();

    });
    $('#VENDORID').change(function () {
        VendornameTExt = $("#VENDORID option:selected").text();
        $("#VENDORNAME").val(VendornameTExt);
        bindtaxcount();
        LoadTpu_Transporter();
        PoAutoClose();
        LoadPoMM();
    });
    $('#POID').change(function () {
        PONOTExt = $("#POID option:selected").text();
        $("#PONO").val(PONOTExt);
        LoadProductDetails();
    });
    $("#MATERIALID").change(function () {
        debugger;
        SelectProduct();
    });
    $("#PACKSIZEID_FROM").change(function () {
        PACKSIZENAMETExt = $("#PACKSIZEID_FROM option:selected").text();
        $("#PACKSIZENAME_FROM").val(PACKSIZENAMETExt);
    });

    $("#btnnewentry").click(function (e) {
        debugger;
        $('#addnew').css("display", "");
        $('#showdata').css("display", "none");
        $('#divuploadinfo').css("display", "none");
        $('#divSampleQty').css("display", "none");
        //document.getElementById('btnnewentry').style.visibility = 'hidden';
        $('#btnnewentry').hide();
        $("#btnsave").show();

        ReleaseSession();
        ResetControls();
        Clearcontrol();
        /*method call in pageload*/

        $("#VENDORFROM").chosen('destroy');
        $("#VENDORFROM").chosen({ width: '150px' });
        $("#VENDORID").chosen('destroy');
        $("#VENDORID").chosen({ width: '200px' });
        $("#MODEOFTRANSPORT").chosen('destroy');
        $("#MODEOFTRANSPORT").chosen({ width: '150px' });
        $("#TRANSPORTERID").chosen('destroy');
        $("#TRANSPORTERID").chosen({ width: '150px' });


        $("#POID").chosen('destroy');
        $("#POID").chosen({ width: '230px' });
        $("#MATERIALID").chosen('destroy');
        $("#MATERIALID").chosen({ width: '900px' });
        $("#hdngrnid").val('');

    });

    /**btn add click **/
    $("#btnadd").click(function (e) {

        debugger;
        var isexists = 'n';
        var mrate = 0;
        var mqcqty = 0;
        var tolerance = 0;
        var toleranceval = 0;
        var rtol = 0;
        var itol = 0;
        var actual = 0;
        var mdespatchqty = 0;
        var qcqty = 0;
        var FG = FG;
        var depoid = "";
        var poid = $("#POID").val();
        var pono = $('#PONO').val();
        var podate = $("#hdnpodate").val();
        var hsncode = $("#hdnhsn").val();
        var packsizeid = $("#PACKSIZEID_FROM").val();
        var packsizename = $('#PACKSIZENAME_FROM').val();
        var mrp = $("#MRP").val();
        var despatchqty = $("#RECEIVE_QTY").val();
        var rate = $("#RATE").val();
        mrate = $("#RATE").val();
        mdespatchqty = $("#RECEIVEDQTY").val();
        var remainingqty = $("#REMAININGQTY").val();
        var itemwisefreight = $("#FREIGHTCHARGES").val();
        var itemwiseaddcost = $("#ADDITIONALCOST").val();
        var batchno = $("#BATCHNO").val();
        var invoicedate = $("#INVOICEDATE").val();
        var tpu = $("#VENDORID").val();
        var mfgdt = $("#MFGDATE").val();
        var packsize = $("#PACKSIZEID_FROM").val();

        var expdt = $("#EXPDATE").val();
        var alreadydespatchqty = $("#RECEIVEDQTY").val();
        var assessmentpercent = $("#hdnasspercentage").val();
        var catid = $("#hdncatid").val();
        var TaxNameCgst = $('#hdntaxnameCGST').val().trim();
        var TaxNameSgst = $('#hdntaxnameSGST').val().trim();
        var TaxNameIgst = $('#hdntaxnameIGST').val().trim();

        var addqty = 0
        addqty = (remainingqty - despatchqty);

        if (addqty < 0) {
            toastr.warning('Remainingqty Qty should be lesser than despatchqty');
            return false;
        }

        if (expdt != '' && mfgdt != '') {
            if (expdt < mfgdt) {
                toastr.warning('Expiry date cannot be less than manufacturing date');
                return false;
            }
        }
        else
            if (catid == '3') {
                if (expdt == '' || mfgdt == '') {
                    toastr.warning('MFG.Date & EXP.Date mandatory for raw materials product.');
                    return false;
                }
            }
            else
                if (mrate == '0') {
                    toastr.warning('Rate cannot be zero');
                    return false;
                }
                else
                    if (mdespatchqty > mqcqty) {
                        debugger;
                        tolerance = mdespatchqty - mqcqty;
                        toleranceval = (mqcqty * 5) / 100;
                        if (toleranceval == 0) {
                            itol = toleranceval;
                            rtol = toleranceval;
                        }
                        else {
                            itol = toleranceval.split('.');
                            rtol = toleranceval - math.floor(toleranceval);
                        }


                        if (rtol > .50) {
                            actual = 1;
                        }
                        if (itol > 0) {
                            actual = actual + itol;
                        }
                        if (actual > tolerance) {
                            toastr.warning('Received qty. should not be greater than PO qty."');
                            return false;
                        }
                    }
        if (despatchqty == '0' || despatchqty == '0.00') {
            toastr.warning('Receive Qty should be greater than 0');
            return false;
        }

        if (packsize == '0' || packsize == '') {
            toastr.warning("Please Select Packsize");
            return;
        }
        if (itemwisefreight == '') {
            itemwisefreight = 0;
        }
        if (itemwiseaddcost == '') {
            itemwiseaddcost = 0;
        }
        //duplicatre check
        if ($('#grdAddGrn').length) {
            $("#grdAddGrn tbody tr").each(function () {
                var productidgrd = $(this).find('td:eq(2)').html();
                var poidgrd = $(this).find('td:eq(1)').html();
                var batchgrd = $(this).find('td:eq(17)').html();

                // var batchidgrd = $(this).find('td:eq(3)').html();
                if (batchno == '') {
                    if (poid == poidgrd && productid == productidgrd) {
                        isexists = 'y';
                        return false;
                    }
                }
                else
                    if (batchno != '') {
                        if (poid == poidgrd && batchno == batchgrd && productid == productidgrd) {
                            isexists = 'y';
                            return false;
                        }
                    }
            })
        }
        if (isexists == 'y') {
            toastr.warning('Product already exists');
            return false;
        }
        else {
            if ($("#hdntaxcount").val() == "2") {
                CalculateProductTax(depoid, poid, pono, podate, hsncode, productid, productname, packsizeid, packsizename, mrp, despatchqty, qcqty, rate, itemwisefreight, itemwiseaddcost, batchno, FG, invoicedate, tpu, mfgdt, expdt, alreadydespatchqty, assessmentpercent, TaxNameCgst, TaxNameSgst, "");
            }
            else {
                CalculateProductTax(depoid, poid, pono, podate, hsncode, productid, productname, packsizeid, packsizename, mrp, despatchqty, qcqty, rate, itemwisefreight, itemwiseaddcost, batchno, FG, invoicedate, tpu, mfgdt, expdt, alreadydespatchqty, assessmentpercent, "", "", TaxNameIgst);
            }
            var rowCount = document.getElementById("grdAddGrn").rows.length - 1;
            if (rowCount > 0) {
                $("#VENDORID").prop('disabled', "disabled");
            }
            else {
                $("#VENDORID").prop('disabled', "");
            }
        }


    })

    $("#btnsearch").click(function (e) {
        LoadGRN();

    });

    /*invoice no change function*/
    $(function () {
        $('#INVOICENO').change(function () {
            debugger
            var txtInvoiceNoL = $("#INVOICENO").val().length;
            var txtInvoiceNo = $('#INVOICENO').val();

            for (var i = 0; i < txtInvoiceNoL; i++) {

                var n1 = txtInvoiceNo.charCodeAt(i);
                if ((n1 >= 47 && n1 <= 57) || (n1 >= 65 && n1 <= 90) || (n1 >= 97 && n1 <= 122) || n1 == 45) {

                }
                else {
                    toastr.warnings('Invalid Invoice No.');
                    $('#INVOICENO').val('');
                    return false;
                }
            }
            var j = 0;
            for (var i = 0; i < txtInvoiceNoL; i++) {

                var n1 = txtInvoiceNo.charCodeAt(i);
                if (n1 != 48) {
                    j = 1;
                }
            }
            if (j == 0) {
                toastr.warning('Invalid Invoice No.');
                document.getElementById(txtInvoiceNo1).value = "";
                return false;
            }
            return true;
        })
    })

    $("#btnsave").click(function (e) {
        debugger;

        if ($("#TRANSPORTERID").val() == '0' || $("#TRANSPORTERID").val() == '') {
            toastr.warning("Please Select Transporter");
            return false;
        }
        else {
            InvoiceExists();
        }


    });

    $("#btncancel").click(function (e) {
        $('#addnew').css("display", "none");
        $('#btnnewentry').show();
        $('#ponoshow').css("display", "none");
        $('#showdata').css("display", "");
        $("#hdngrnid").val('');
        ReleaseSession();
        ResetControls();
        Clearcontrol();
        if (OP == "QC") {
            $('#btnnewentry').css("display", "none");
        }
        else if (OP = "MAKER") {
            $('#btnnewentry').css("display", "");

        }

    });


    /*for download*/
    $("#btnDocuments").click(function (e) {
        debugger;
        var grnid = $("#hdngrnid").val();
        FetchQCID(grnid);
        $('#myModalqcsample').css("display", "none");
        $('#myModal').css("display", "");
        $('#btnSamplesave').hide();
        $('#txtchkupload').hide();
        $('#Chksubmit').hide();
        $('#btnDivSave').hide();
        $('#divuploadinfo').css("display", "");
        $('#divSampleQty').css("display", "none");
    });

    $("#btnGrnSample").click(function (e) {
        debugger;
        var grnid = $("#hdngrnid").val();
        var mode = 'SampleQty';
        BindReceiveQty(grnid, mode);
        $('#btnSamplesave').show();
        $('#divuploadinfo').css("display", "none");
        $('#divSampleQty').css("display", "");
    });

    $("#ChkCapacity").click(function (e) {

        if ($(this).prop("checked") == true) {
            debugger;
            mode = 'CheckBox'
            FetchCapacity(mode);
            $('#myModalqcsample').css("display", "none");
            $('#myModal').css("display", "");
            $('#divuploadinfo').css("display", "");
            $('#btnSamplesave').show();
            $('#txtchkupload').show();
            $('#Chksubmit').show();
            $('#btnDivSave').show();

        }
        else {
         
            $('#myModalqcsample').css("display", "none");
            $('#myModal').css("display", "none");
            $('#divuploadinfo').css("display", "none");
            $('#btnSamplesave').hide();
            $('#txtchkupload').hide();
            $('#Chksubmit').hide();
            $('#btnDivSave').hide();
        }
    });

    /*for upload file*/
    $('#Chksubmit').click(function () {
        // Checking whether FormData is available in browser  
        if (window.FormData !== undefined) {
            debugger;
            var STOCKRECEIVEDID = document.getElementById("hdngrnid").value;
            var fileUpload = $("#txtchkupload").get(0);
            var files = fileUpload.files;
            // Create FormData object  
            var fileData = new FormData();
            fileData.append("STOCKRECEIVEDID", STOCKRECEIVEDID);

            // Looping over all files and add it to FormData object  
            for (var i = 0; i < files.length; i++) {
                fileData.append(files[i].name, files[i]);
            }

            $.ajax({
                url: "/mmpo/Chkupload_file",
                type: "POST",
                contentType: false, // Not to set any content header  
                processData: false, // Not to process data  
                data: fileData,
                success: function (response) {
                    debugger;
                    var tableNew = '<table id="fileupload">';
                    tableNew = tableNew + '<thead><tr><th>Counter</th> <th>File name </th><th>Download</th><th>Delete</th></thead><tbody>';
                    var counter = 0;
                    toastr.info('File uploaded successfully');
                    $.each(response, function () {
                        counter = counter + 1;
                        tableNew = tableNew + "<tr><td style='width:10%'>" + counter + "</td><td style='width:30%'>" + this["FILENAME"] + "</td><td style='width:20%'><a href='factorygrnMM' download='" + this["FILEPATH"] + "'><img src='../Images/download.png' width='20' height ='20' title='Download'/></a>  </td><td style='width:20%' ><input type='button' id='btndelfile'  class='clsfiledel' value='Del'/></td></tr > ";

                    });
                    document.getElementById("divuploadinfo").innerHTML = tableNew + '</tbody></table>';
                },
                error: function (err) {
                    toastr.error(err.statusText);
                }
            });
        }
        else {
            toastr.error("FormData is not supported.");
        }
    });

    $('#btnshowcapacity').click(function () {

        var mode = 'Dcmnt';
        FetchCapacity(mode);
        $('#divuploadinfo').css("display", "");
        //$('#divCheckSumbit').css("display", "");
        $('#txtchkupload').hide();
        $('#btnSamplesave').hide();
        $('#Chksubmit').hide();
        $('#btnDivSave').hide();

        
    });

    // Get the <span> element that closes the modal
    var span = document.getElementsByClassName("close")[0];
    var btn = document.getElementById("btnDocuments");
    var btn1 = document.getElementById("ChkCapacity");
    var btn3 = document.getElementById("btnshowcapacity");
    var modal = document.getElementById("myModal");

    btn.onclick = function () {
        modal.style.display = "block";
    }
    btn1.onclick = function () {
        if ($(this).prop("checked") == true) {
            modal.style.display = "block";
        }
        else {
            modal.style.display = "";
        }
       
    }
    btn3.onclick = function () {
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


    // Get the <span> element that closes the modal
    var span2 = document.getElementsByClassName("closeQc")[0];
    var btn2 = document.getElementById("btnGrnSample");


    var modal2 = document.getElementById("myModalqcsample");
    btn2.onclick = function () {
        modal2.style.display = "block";
    }


    // When the user clicks on <span> (x), close the modal
    span2.onclick = function () {
        modal2.style.display = "none";
    }
    window.onclick = function (event) {
        if (event.target == modal2) {
            modal2.style.display = "none";
        }
    }


    $("#btnSamplesave").click(function (e) {
        debugger;

        var grid = document.getElementById("uploadsampleqty");
        for (var i = 0; i < grid.rows.length - 1; i++) {
            var Sample = $("input[id*=txtSampleqty]");
            var Observation = $("input[id*=txtObservationqty]");

            var Sampleqty = Sample[i].value;
            var Observationqty = Observation[i].value;

            //}

            var i = 0;
            grnlist = new Array();
            $('#uploadsampleqty tbody tr').each(function () {
                var grdAddGrn = {};
                var target = e.target;
                var Stockreceivedid = $(this).find('td:eq(0)').html();

                var Poid = $(this).find('td:eq(1)').html();
                var Productid = $(this).find('td:eq(2)').html();
                var Productname = $(this).find('td:eq(3)').html();
                var Receivedqty = $(this).find('td:eq(4)').html();

                grdAddGrn.Stockreceivedid = Stockreceivedid;
                grdAddGrn.Poid = Poid;
                grdAddGrn.Productid = Productid;
                grdAddGrn.Productname = Productname;
                grdAddGrn.Receivedqty = Receivedqty;
                grnlist[i++] = grdAddGrn;

            });



            for (i = 0; i < grnlist.length; i++) {
                $.each(grnlist, function (key, item) {
                    Stockreceivedid = (grnlist[i].Stockreceivedid);
                    Poid = (grnlist[i].Poid);
                    Productid = (grnlist[i].Productid);
                    Productname = (grnlist[i].Productname);
                    Receivedqty = (grnlist[i].Receivedqty);
                });

                if (Sampleqty != 0 || Observationqty != 0) {
                    if (Receivedqty >= Sampleqty) {
                        addSampleQtyDatatable(Stockreceivedid, Poid, Productid, Productname, Receivedqty, Sampleqty, Observationqty);
                    }
                    else {
                        toastr.warning("Receive Qty Can not be greater than Balance Qty.");
                        return false;
                    }
                }
                else {
                    toastr.warning("Sample Qty OR Observation Qty Can not be 0.");
                    return false;
                }
            }

        }
       

    });

    $("body").on("click", "#divuploadinfo .clsfiledel", function () {



        //$('.debitdel').on('click', function () {


        if (confirm("Do you want to delete this Entry?")) {
            var row = $(this).closest("tr");


            var filename = row.find('td:eq(1)').html();

            row.remove();
            $.ajax({

                type: "POST",
                url: "/mmpo/deletefile",
                data: { filename: filename },
                //data: "{'ledgerid': '" + ledgerid + "','CostCatagoryID': '" + CostCatagoryID + "','BrandId': '" + BrandId + "','ProductId': '" + ProductId + "','DepartmentId': '" + DepartmentId + "'}",

                dataType: "json",
                success: function (r) {
                    $.each(function (r) {

                    })
                }
            });
        }
    })


    $('#btnDivSave').click(function (e) {

        $('#myModalqcsample').css("display", "none");
        $('#myModal').css("display", "none");
        $('#divuploadinfo').css("display", "none");
        $('#btnSamplesave').hide();
        $('#txtchkupload').hide();
        $('#Chksubmit').hide();
        $('#btnDivSave').hide();

    });
});

function FetchQCID(grnid) {
    var mode = "FindQcId"
    $.ajax({
        type: "POST",
        url: "/mmpo/GetFile_SR_Capacity",
        data: { grnid: grnid, mode: mode },
        dataType: "json",
        async: false,
        success: function (response) {
            $.each(response, function (index, record) {
                $("#hdNQcId").val(this['QCID']);
            });
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
    $('#myModalqcsample').css("display", "none");
    $('#myModal').css("display", "");
    $('#divuploadinfo').css("display", "");
    var qcid = $("#hdNQcId").val();
    Fetchshowcapacity(qcid);
}

function Fetchshowcapacity(qcid) {
    debugger;
    var mode = "StockIn"

    $.ajax({
        type: "POST",
        url: "/mmpo/GetFile_SR_Capacity",
        data: { grnid: qcid, mode: mode },
        dataType: "json",
        async: false,
        success: function (response) {
            var tableNew = '<table id="fileupload">';
            tableNew = tableNew + '<thead><tr><th>Counter</th> <th>File name </th><th>Download</th><th>Delete</th></thead><tbody>';
            var counter = 0;
            debugger;
            $.each(response, function () {
                counter = counter + 1;
                tableNew = tableNew + "<tr><td style='width:10%'>" + counter + "</td><td style='width:30%'>" + this["FILENAME"] + "</td><td style='width:20%'><a href='~/factorygrnMM' download='" + this["FILEPATH"] + "'><img src='../Images/download.png' width='20' height ='20' title='Download'/></a>  </td><td style='width:20%' ><input type='button' id='btndelfile'  class='clsfiledel' value='Del'/></td></tr > ";

            });
            document.getElementById("divuploadinfo").innerHTML = tableNew + '</tbody></table>';
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function BindReceiveQty(grnid, mode) {
    debugger;
    $.ajax({
        type: "POST",
        url: "/mmpo/GetFile_SR_Capacity",
        data: { grnid: grnid, mode: mode },
        dataType: "json",
        success: function (response) {
            var tr;
            debugger;
            tr = $('#uploadsampleqty');

            $('#uploadsampleqty').DataTable().destroy();
            $("#uploadsampleqty tbody tr").remove();

            for (var i = 0; i < response.length; i++) {
                tr = $('<tr/>');
                tr.append("<td style='display: none'>" + response[i].STOCKRECEIVEDID + "</td>");
                tr.append("<td style='display: none'>" + response[i].POID + "</td>");
                tr.append("<td style='display: none'>" + response[i].PRODUCTID + "</td>");
                tr.append("<td>" + response[i].PRODUCTNAME + "</td>");
                tr.append("<td>" + response[i].RECEIVEDQTY + "</td>");
                tr.append("<td ><input type='text' class='nontax' id='txtSampleqty' style='text-align:right' value='" + response[i].SAMPLEQTY + "' /></td>");
                tr.append("<td><input type='text' class='nontax' id='txtObservationqty' style='text-align:right' value='" + response[i].OBSERVATIONQTY + "' /> </td>");

                // tr.append("<td>"  + response[i].OBSERVATIONQTY + "</td>");
                $("#uploadsampleqty").append(tr);

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

function FetchCapacity(mode) {
    debugger;
    var grnid = $("#hdngrnid").val();
    $.ajax({
        type: "POST",
        url: "/mmpo/GetFile_SR_Capacity",
        data: { grnid: grnid, mode: mode },
        dataType: "json",
        success: function (response) {
            var tr;
            debugger;
            var tableNew = '<table id="fileupload">';
            tableNew = tableNew + '<thead><tr><th>Counter</th> <th>File name </th><th>Download</th><th>Delete</th></thead><tbody>';
            var counter = 0;
            $.each(response, function () {
                counter = counter + 1;
                tableNew = tableNew + "<tr><td style='width:10%'>" + counter + "</td><td style='width:30%'>" + this["FILENAME"] + "</td><td style='width:20%'><a href='~/factorygrnMM' download='" + this["FILEPATH"] + "' target='_blank' ><img src='../Images/download.png' width='20' height ='20' title='Download'/></a>  </td><td style='width:20%' ><input type='button' id='btndelfile'  class='clsfiledel' value='Del'/></td></tr > ";

            });
            document.getElementById("divuploadinfo").innerHTML = tableNew + '</tbody></table>';
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function InvoiceExists() {

    $.ajax({
        type: "POST",
        url: "/mmpo/CheckInvoiceNo",
        data: { INVOICENO: $("#INVOICENO").val(), VENDORID: $("#VENDORID").val() },
        dataType: "json",
        success: function (INVOICENO) {
            if (INVOICENO.length == "") {
                saveGrndata();
            }
            else {
                toastr.warning("INVOICE NO ALREADY EXISTS");
                return false;

            }

        }
    });

}

function LoadGRN() {
    debugger;
    $.ajax({
        type: "POST",
        url: "/mmpo/BindgrnGrid",
        data: { Fromdate: $('#Fromdate').val(), Todate: $('#Todate').val(), CheckerFlag: CheckerFlag, TPUFLAG: TPUFLAG, OP: OP },
        dataType: "json",
        async: false,
        success: function (response) {
            var tr;
            tr = $('#grndetailsGrid');
            var HeaderCount = $('#grndetailsGrid thead th').length;
            debugger;
            if (HeaderCount == 0) {

                tr.append("<thead><tr><th style='display:none'>STOCKDESPATCHID</th><th style='display:none'>STOCKRECEIVEDID</th><th>STOCKRECEIVEDNO</th><th>DESPATCHDATE</th><th>STOCKRECEIVEDDATE</th><th>MOTHERDEPOTNAME</th><th style='display:none'>MOTHERDEPOTID</th><th>VENDORNAME</th><th>FINYEAR</th><th>TRANSPORTERNAME</th><th style='display:none'>TRANSPORTERID</th><th style='display:none'>NEXTLEVELID</th><th>ISVERIFIEDDESC</th><th>TPUNAME</th><th>Edit</th><th>Print</th><th>Delete</th></tr></thead><tbody>");
                $('#grndetailsGrid').DataTable().destroy();
                $("#grndetailsGrid tbody tr").remove();

                //for grid table
                for (var i = 0; i < response.length; i++) {
                    tr = $('<tr/>');
                    tr.append("<td style='display: none'>" + response[i].STOCKDESPATCHID + "</td>");
                    tr.append("<td style='display: none'>" + response[i].STOCKRECEIVEDID + "</td>");
                    tr.append("<td>" + response[i].STOCKRECEIVEDNO + "</td>");
                    tr.append("<td>" + response[i].DESPATCHDATE + "</td>");
                    tr.append("<td>" + response[i].STOCKRECEIVEDDATE + "</td>");
                    //tr.append("<td>" + response[i].WAYBILLNO + "</td>");
                    //tr.append("<td>" + response[i].WAYBILLKEY + "</td>");
                    //tr.append("<td>" + response[i].INVOICENO + "</td>");
                    //tr.append("<td>" + response[i].VEHICHLENO + "</td>");
                    //tr.append("<td>" + response[i].LRGRNO + "</td>");
                    //tr.append("<td>" + response[i].MODEOFTRANSPORT + "</td>");
                    tr.append("<td>" + response[i].MOTHERDEPOTNAME + "</td>");
                    tr.append("<td style='display: none'>" + response[i].MOTHERDEPOTID + "</td>");
                    tr.append("<td>" + response[i].VENDORNAME + "</td>");
                    tr.append("<td>" + response[i].FINYEAR + "</td>");
                    tr.append("<td style='display: none'>" + response[i].TRANSPORTERID + "</td>");
                    tr.append("<td>" + response[i].TRANSPORTERNAME + "</td>");
                    tr.append("<td style='display: none'>" + response[i].NEXTLEVELID + "</td>");
                    tr.append("<td>" + response[i].ISVERIFIEDDESC + "</td>");
                    tr.append("<td>" + response[i].TPUNAME + "</td>");
                    tr.append("<td> <input type='image' class='gvdwn'  id='btnedit' '<img src='../Images/ico_edit_16.png' width='20' height ='20' title='Edit'/></td>");

                    tr.append("<td> <input type='image' class='gvprint'  id='btnPoPrint' '<img src='../Images/print.png' width='20' height ='20' title='Print'/></td>");
                    tr.append("<td> <input type='image' class='gvdelete'  id='btnPodelete' '<img src='../Images/ico_delete_16.png' width='20' height ='20' title='Delete'/></td>");
                    $("#grndetailsGrid").append(tr);
                }
            }
            else {

                $('#grndetailsGrid').DataTable().destroy();
                $("#grndetailsGrid tbody tr").remove();
                //for grid table
                for (var i = 0; i < response.length; i++) {
                    tr = $('<tr/>');
                    tr.append("<td style='display: none'>" + response[i].STOCKDESPATCHID + "</td>");
                    tr.append("<td style='display: none'>" + response[i].STOCKRECEIVEDID + "</td>");
                    tr.append("<td>" + response[i].STOCKRECEIVEDNO + "</td>");
                    tr.append("<td>" + response[i].DESPATCHDATE + "</td>");
                    tr.append("<td>" + response[i].STOCKRECEIVEDDATE + "</td>");
                    //tr.append("<td>" + response[i].WAYBILLNO + "</td>");
                    //tr.append("<td>" + response[i].WAYBILLKEY + "</td>");
                    //tr.append("<td>" + response[i].INVOICENO + "</td>");
                    //tr.append("<td>" + response[i].VEHICHLENO + "</td>");
                    //tr.append("<td>" + response[i].LRGRNO + "</td>");
                    //tr.append("<td>" + response[i].MODEOFTRANSPORT + "</td>");
                    tr.append("<td>" + response[i].MOTHERDEPOTNAME + "</td>");
                    tr.append("<td style='display: none'>" + response[i].MOTHERDEPOTID + "</td>");
                    tr.append("<td>" + response[i].VENDORNAME + "</td>");
                    tr.append("<td>" + response[i].FINYEAR + "</td>");
                    tr.append("<td style='display: none'>" + response[i].TRANSPORTERID + "</td>");
                    tr.append("<td>" + response[i].TRANSPORTERNAME + "</td>");
                    tr.append("<td style='display: none'>" + response[i].NEXTLEVELID + "</td>");
                    tr.append("<td>" + response[i].ISVERIFIEDDESC + "</td>");
                    tr.append("<td>" + response[i].TPUNAME + "</td>");
                    tr.append("<td> <input type='image' class='gvdwn'  id='btnedit' '<img src='../Images/ico_edit_16.png' width='20' height ='20' title='Edit'/></td>");
                    tr.append("<td> <input type='image' class='gvprint'  id='btnPoPrint' '<img src='../Images/print.png' width='20' height ='20' title='Print'/></td>");
                    tr.append("<td> <input type='image' class='gvdelete'  id='btnPodelete' '<img src='../Images/ico_delete_16.png' width='20' height ='20' title='Delete'/></td>");

                    $("#grndetailsGrid").append(tr);

                }
            }

            tr.append("</tbody>");

            $('#grndetailsGrid').DataTable({
                "sScrollX": '100%',
                "sScrollXInner": "110%",
                "scrollY": "238px",
                "scrollCollapse": true,
                "dom": 'Bfrtip',
                buttons: [
                    {
                        extend: 'excelHtml5',
                        title: 'Grn Details'
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
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}/*for search event*/

function LoadTpu() {
    debugger;
    $.ajax({
        type: "POST",
        url: "/mmpo/LoadTpu",
        data: '{}',
        async: false,
        data: { FG: FG },
        success: function (result) {
            var vendorid = $("#VENDORID");
            vendorid.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(result, function () {
                $('#VENDORID').val(this['VENDORNAME']);
                vendorid.append($("<option value=''></option>").val(this['VENDORID']).html(this['VENDORNAME']));
            });
            $("#VENDORID").chosen();
            $("#VENDORID").trigger("chosen:updated");
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function LoadTpu_Transporter() {
    debugger;
    $.ajax({
        type: "POST",
        url: "/mmpo/LoadTpu_Transporter",
        data: '{}',
        async: false,
        data: { Vendorid: $('#VENDORID').val() },
        success: function (result) {
            var TRANSPORTERID = $("#TRANSPORTERID");
            TRANSPORTERID.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(result, function () {
                $('#TRANSPORTERID').val(this['TRANSPORTERNAME']);
                TRANSPORTERID.append($("<option value=''></option>").val(this['TRANSPORTERID']).html(this['TRANSPORTERNAME']));
            });
            $("#TRANSPORTERID").chosen();
            $("#TRANSPORTERID").trigger("chosen:updated");
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function PoAutoClose() {

    $.ajax({
        type: "POST",
        url: "/mmpo/PoCloseAuto",
        data: '{}',
        async: false,
        data: { Vendorid: $('#VENDORID').val() },
    });
}

function LoadPoMM() {
    debugger;
    $.ajax({
        type: "POST",
        url: "/mmpo/LoadPoMM",
        data: '{}',
        async: false,
        data: { Vendorid: $('#VENDORID').val() },
        success: function (result) {
            var POID = $("#POID");
            POID.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(result, function () {
                $('#POID').val(this['PONO']);
                POID.append($("<option value=''></option>").val(this['POID']).html(this['PONO']));
            });
            $("#POID").chosen();
            $("#POID").trigger("chosen:updated");

        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function LoadProductDetails() {
    debugger;
    var desc = '';
    var desc1 = '';
    var val1 = '';
    var listItems = '';
    desc = 'PRODUCT NAME'.padEnd(15, '\u00A0') + repeatstr("&nbsp;", 60) + 'UNIT NAME'.padEnd(05, '\u00A0') + repeatstr("&nbsp;", 10) + 'POQTY' + repeatstr("&nbsp;", 6) + 'DESPATCHQTY' + repeatstr("&nbsp;", 8) + 'REMAININGQTY.' + repeatstr("&nbsp;", 8) + 'MRP' + repeatstr("&nbsp;", 6) + 'RATE';
    listItems += "<option value='0'>" + desc + "</option>";
    $.ajax({
        type: "POST",
        url: "/mmpo/LoadProductDetails",
        data: '{}',
        async: false,
        data: { Poid: $('#POID').val() },
        success: function (result) {
            var MATERIALID = $("#MATERIALID");
            MATERIALID.empty().append('<option selected="selected" value="0">Please select</option>');
            var counter = 0;
            //MATERIALID.empty();
            debugger;
            $.each(result, function () {
                val1 = this["PRODUCT_ID"] + "," + this["UNITID"] + "," + this["POQTY"] + "," + this["DESPATCHQTY"] + "," + this["REMAININGQTY"] + "," + this["MRP"] + "," + this["RATE"] + "," + this["UNITNAME"] + "," + this["PRODUCT_NAME"];
                var length = 60 - (this["PRODUCT_NAME"].length);
                desc1 = this["PRODUCT_NAME"].padEnd(15, '\u00A0') + repeatstr("&nbsp;", length) + this["UNITNAME"].padEnd(05, '\u00A0') + repeatstr("&nbsp;", 10) + this["POQTY"] + repeatstr("&nbsp;", 10) + this["DESPATCHQTY"] + repeatstr("&nbsp;", 18) + this["REMAININGQTY"] + repeatstr("&nbsp;", 20) + this["MRP"] + repeatstr("&nbsp;", 14) + this["RATE"]
                listItems += "<option value='" + val1 + "'>" + desc1 + "</option>";
                counter = counter + 1;
                length = 0;
            });
            debugger;
            MATERIALID.append(listItems);
            if (counter == 0) {
                toastr.info("No Purchase Order is available with remaining quantity");
            }
            $("#MATERIALID").chosen();
            $("#MATERIALID").trigger("chosen:updated");
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

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

function repeatstr(ch, n) {
    var result = "&nbsp;";
    while (n-- > 0)
        result += ch;
    return result;
}

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

function SelectProduct() {

    debugger;

    var listItems = "<option value='0'>SELECT Unit </option>";
    var strval2 = $("#MATERIALID").val();
    var splitval = strval2.split(',');
    unitid = splitval[1]
    productname = splitval[8]
    $("#RECEIVE_QTY").val(splitval[3]);
    $("#REMAININGQTY").val(splitval[4]);
    $("#MRP").val(splitval[5]);
    listItems += "<option value='" + splitval[1] + "'>" + splitval[7] + "</option>";
    $("#PACKSIZEID_FROM").html(listItems);
    $("PACKSIZEID_FROM").val(splitval[1]);
    $("#PACKSIZEID_FROM").chosen();
    $("#PACKSIZEID_FROM").trigger("chosen:updated");
    productid = splitval[0];
    var poid = $("#POID").val();
    var tpu = $("#VENDORID").val();
    var invoicedt = $("#INVOICEDATE").val();
    debugger;

    Loadproductdtl(poid, productid, unitid, FG, invoicedt, tpu);

}

function Loadproductdtl(poid, productid, unitid, FG, invoicedt, tpu) {
    $.ajax({
        type: "POST",
        url: "/mmpo/Loadproductdtl",
        data: '{}',
        async: false,
        data: { poid: poid, productid: productid, unitid: unitid, FG: FG, invoicedt: invoicedt, tpu: tpu },
        success: function (response) {
            //alert(JSON.stringify(response));
            $.each(response, function (key, item) {
                $("#RATE").val(item.RATE);
                $("#hdnasspercentage").val(item.ASSESSABLEPERCENT);
                $("#MRP").val(item.MRP);
                $("#hdnweight").val(item.WEIGHT);
                $("#RECEIVEDQTY").val(item.DESPATCH_QTY);
                $("#POQTY").val(item.PO_QTY);
                $("#hdnpodate").val(item.PODATE);
                $("#hdncatid").val(item.CATID);
                $("#hdnhsn").val(item.HSE);
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

function bindtaxcount() {

    $.ajax({
        type: "POST",
        url: "/mmpo/GetTaxcount",
        data: { MenuID: '1474', Flag: '1', ProductID: '0', CustomerID: $('#VENDORID').val(), Date: $('#DESPATCHDATE').val() },
        dataType: "json",
        success: function (taxcount) {
            //alert(JSON.stringify(taxcount));
            //alert(taxcount.length);
            debugger;
            if (taxcount.length > 0) {
                if (taxcount.length == 1) {
                    $.each(taxcount, function (key, item) {
                        debugger;
                        $("#hdntaxcount").val('1');
                        $("#hdntaxnameIGST").val(taxcount[0].TAXNAME);
                        $("#hdntaxpercentage").val(taxcount[0].TAXPERCENTAGE);
                        $("#hdnrelatedto").val(taxcount[0].TAXRELATEDTO);
                    });
                }
                else if (taxcount.length == 2) {
                    debugger;
                    $.each(taxcount, function (key, item) {
                        debugger;
                        $("#hdntaxcount").val('2');
                        $("#hdntaxnameCGST").val(taxcount[0].TAXNAME);
                        $("#hdntaxnameSGST").val(taxcount[1].TAXNAME);
                        $("#hdntaxpercentage").val(taxcount[0].TAXPERCENTAGE);
                        $("#hdnrelatedto").val(taxcount[0].TAXRELATEDTO);
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
        failure: function (taxcount) {
            alert(response.responseText);
        },
        error: function (taxcount) {
            alert(response.responseText);
        }
    });

}

function CalculateProductTax(depoid, poid, pono, podate, hsncode, productid, productname, packsizeid, packsizename, mrp, despatchqty, qcqty, rate, itemwisefreight, itemwiseaddcost, batchno, FG, invoicedate, tpu, mfgdt, expdt, alreadydespatchqty, assessmentpercent, TaxNameCgst, TaxNameSgst, TaxNameIgst) {
    var depotwisedespatchqty = 0;
    var alldepotdespatchqty = 0;
    var mastermfgdate = '';
    var masterexpdate = '';
    var Amount = 0;
    var TotAmt = 0;
    var TotMRP = 0;
    var DiscountPer = 0;
    var DiscountAmt = 0;
    var hsntax = 0;
    var AfterDiscountAmt = 0;
    var _batchno = '';
    var poqty = 0;
    var _poid = '';
    var _pono = '';
    var _podate = '';
    var _packsizeid = '';
    var _packsizename = '';
    var _mrp = 0;
    var _alreadydespatchqty = 0;
    var _rate = 0;
    var _despatchqty = 0;
    var _mrp = 0;
    var _hsn = '';
    var _productid = '';
    var _productname = '';
    var DESPATCHQTY = 0;
    var RECEIVEDQTY = 0;
    var REMAININGQTY = 0;
    var RATE = 0;
    var AMOUNT = 0;
    var DISCOUNTPER = 0;
    var DISCOUNTAMT = 0;
    var cgsttax = 0;
    var sgsttax = 0;
    var igsttax = 0;
    var AFTERDISCOUNTAMT = 0;
    var ITEMWISEFREIGHT = 0;
    var AfterItemWiseFreightAmt = 0;
    var AFTERITEMWISEFREIGHTAMT = 0;
    var ITEMWISEADDCOST = 0;
    var AfterItemWiseAddCostAmt = 0;
    var AFTERITEMWISEADDCOSTAMT = 0;
    var TOTMRP = 0;
    var BATCHNO = '';
    var ASSESMENTPERCENTAGE = 0;
    var TOTALASSESMENTVALUE = 0;
    var excise = 0;
    var INPUTCGST = 0;
    var INPUTSGST = 0;
    var INPUTIGST = 0;
    var GROSSWEIGHT = "";
    var DEPOTRATE = 0;
    var NETWEIGHT = "";
    var srl = 0;
    var TaxAmount = 0;
    srl = srl + 1;

    $.ajax({
        type: "POST",
        url: "/mmpo/addproductdtl1",
        data: '{}',
        async: false,
        data: { depoid: depoid, poid: poid, pono: pono, podate: podate, hsncode: hsncode, productid: productid, productname: productname, packsizeid: packsizeid, packsizename: packsizename, mrp: mrp, despatchqty: despatchqty, qcqty: 1, rate: rate, itemwisefreight: itemwisefreight, itemwiseaddcost: itemwiseaddcost, batchno: batchno, FG: FG, invoicedate: invoicedate, tpu: tpu, mfgdt: mfgdt, expdt: expdt, alreadydespatchqty: alreadydespatchqty, assessmentpercent: assessmentpercent, TaxNameCgst: TaxNameCgst, TaxNameSgst: TaxNameSgst, TaxNameIgst: TaxNameIgst },
        success: function (response) {
            //alert(JSON.stringify(response));
            debugger;
            var listInfoBatches = response.allcalculateDataset.varproductInfoBatches;
            var listmasterbatch = response.allcalculateDataset.varmasterbatch;
            var listreturnamount = response.allcalculateDataset.varproduct_returnamount;
            var listamount = response.allcalculateDataset.varproduct_amount;
            var listdiscount = response.allcalculateDataset.varproduct_discount;
            var listnetgrossweight = response.allcalculateDataset.varproduct_netgrossweight;
            var listgrossweight = response.allcalculateDataset.varproduct_grossweight;
            var listCGSTTax = response.allcalculateDataset.varCgstTax;
            var listSGSTTax = response.allcalculateDataset.varSgstTax;
            var listIGSTTax = response.allcalculateDataset.varIgstTax;
            var listCGSTID = response.allcalculateDataset.varCgstID;
            var listSGSTID = response.allcalculateDataset.varSgstID;
            var listIGSTID = response.allcalculateDataset.varIgstID;

            debugger;
            $.each(listInfoBatches, function (key, item) {
                depotwisedespatchqty = this['DEPOTWISE_DESPATCH_QTY'].toString().trim();
                alldepotdespatchqty = this['DESPATCH_QTY'].toString().trim();
                poqty = this['PO_QTY'].toString().trim();

            });
            //alert('hi');
            $.each(listmasterbatch, function (key, item) {
                mastermfgdate = this['MFDATE'].toString().trim();
                masterexpdate = this['EXPRDATE'].toString().trim();
            });
            $.each(listreturnamount, function (key, item) {
                Amount = this['RETURNAMOUNT'].toString().trim();
            });
            TotAmt += Amount;
            $.each(listamount, function (key, item) {
                TotMRP = this['AMOUNT'].toString().trim();
            });
            $.each(listdiscount, function (key, item) {
                DiscountPer = this['DISCOUNTPER'].toString().trim();
                DiscountAmt = this['DISCOUNTAMT'].toString().trim();
            });

            debugger;
            if ($("#hdntaxcount").val().trim() == '2') {
                $.each(listCGSTTax, function (key, item) {
                    cgsttax = this['CGSTTAX'].toString().trim();
                });
            }

            if ($("#hdntaxcount").val().trim() == '2') {
                $.each(listCGSTID, function (key, item) {
                    $("#hdncgsttaxid").val(this['CGSTID'].toString().trim());
                });
            }

            if ($("#hdntaxcount").val().trim() == '2') {
                $.each(listSGSTTax, function (key, item) {
                    sgsttax = this['SGSTTAX'].toString().trim();
                });
            }
            if ($("#hdntaxcount").val().trim() == '2') {
                $.each(listSGSTID, function (key, item) {
                    $("#hdnsgsttaxid").val(this['SGSTID'].toString().trim());
                });
            }
            if ($("#hdntaxcount").val().trim() == '1') {
                $.each(listIGSTTax, function (key, item) {
                    igsttax = this['IGSTTAX'].toString().trim();
                });
            }
            if ($("#hdntaxcount").val().trim() == '1') {
                $.each(listIGSTID, function (key, item) {
                    $("#hdnigsttaxid").val(this['IGSTID'].toString().trim());
                });
            }
            if (DiscountPer == 0) {
                AfterDiscountAmt = Amount - DiscountAmt;
            }
            else {
                AfterDiscountAmt = Amount - (Amount * DiscountPer / 100);
                DiscountAmt = (Amount * DiscountPer / 100);
            }
            if (batchno == "") {
                _batchno = "NA";
            }
            else {
                _batchno = batchno;
            }
            if (mastermfgdate == "") {
                mastermfgdate = "1900-01-01";
                masterexpdate = "1900-01-01";
            }
            _poid = poid;
            _podate = podate;
            _pono = pono;
            _hsn = hsncode;
            _productid = productid;
            _productname = productname;
            _packsizeid = packsizeid;
            _packsizename = packsizename;
            _mrp = mrp;
            _rate = rate;
            _despatchqty = parseFloat(despatchqty);
            _alreadydespatchqty = parseFloat(alreadydespatchqty);
            DESPATCHQTY = $.add(_despatchqty, _alreadydespatchqty);
            RECEIVEDQTY = parseFloat(despatchqty);
            REMAININGQTY = parseFloat(qcqty - _despatchqty);
            RATE = parseFloat(rate);
            AMOUNT = parseFloat(Amount);
            DISCOUNTPER = parseFloat(DiscountPer);
            DISCOUNTAMT = parseFloat(DiscountAmt);
            TAX = parseFloat(hsntax);
            AFTERDISCOUNTAMT = parseFloat(AfterDiscountAmt);
            ITEMWISEFREIGHT = parseFloat(itemwisefreight);
            AfterItemWiseFreightAmt = $.add(parseFloat(itemwisefreight), parseFloat(AfterDiscountAmt));
            AFTERITEMWISEFREIGHTAMT = parseFloat(AfterItemWiseFreightAmt);
            ITEMWISEADDCOST = parseFloat(itemwiseaddcost);
            AfterItemWiseAddCostAmt = $.add(parseFloat(itemwiseaddcost), parseFloat(AfterItemWiseFreightAmt));
            AFTERITEMWISEADDCOSTAMT = parseFloat(AfterItemWiseAddCostAmt);
            TOTMRP = parseFloat(TotMRP);

            TaxAmount = parseFloat((parseFloat(AfterItemWiseAddCostAmt) * parseFloat(cgsttax) / 100));


            $("#hdnGuid").val(uuidv4());
            var GUID = $("#hdnGuid").val();


            var tr;
            tr = $('#grdAddGrn');
            var HeaderCount = $('#grdAddGrn thead th').length;
            if (HeaderCount == 0) {
                if ($("#hdntaxcount").val().trim() == '2') {
                    tr.append("<thead style='background - color: cornflowerblue; font - size: 14px'><tr><th style='display: none'><th style='display: none'></th><th style='display: none'></th><th>SLNO</th><th>PO.DATE</th><th> PO.NO</th><th>PO.QTY</th><th>PACK SIZE</th><th> HSN CODE</th><th>PRODUCT NAME</th><th>RECEIVED QTY.</th><th>RATE </th> <th>AMOUNT </th><th>DISC % </th><th> DISC RATE </th><th> AFTERDISCOUNT AMT</th><th> ITEMWISE FREIGHT</th><th>AFTERITEMWISE FREIGHTAMT</th><th>ITEMWISE ADDCOST</th><th>AFTERITEMWISE ADDCOST</th><th>BATCH NO.</th><th>INPUT CGST %</th><th>INPUT CGST</th><th>INPUT SGST %</th><th>INPUT SGST.</th><th>NET WEIGHT</th><th>MFDATE</th><th>EXP DATE</th>.<th>GROSSWEIGHT</th><th>DEPOTRARE</th><th>DELETE</th></tr></thead><tbody>")

                    debugger;
                    tr = $('<tr/>');
                    tr.append("<td style='width:25px'>" + srl + "</td>");//0
                    tr.append("<td style='display: none'>" + _poid + "</td>");//1
                    tr.append("<td style='display: none'>" + _productid + "</td>");//2
                    tr.append("<td style='width:25px'>" + _podate + "</td>");//3
                    tr.append("<td style='width:25px'>" + _pono + "</td>");//4
                    tr.append("<td style='width:25px'>" + poqty + "</td>");//5
                    tr.append("<td style='width:25px'>" + packsizename + "</td>");//6
                    tr.append("<td style='width:25px'>" + hsncode + "</td>");//7
                    tr.append("<td style='width:25px'>" + productname + "</td>");//8
                    tr.append("<td style='width:25px'>" + RECEIVEDQTY + "</td>");//9
                    tr.append("<td style='width:25px'>" + _rate + "</td>");//10
                    tr.append("<td style='width:25px'>" + AMOUNT + "</td>");//11
                    tr.append("<td style='width:25px'>" + DISCOUNTPER + "</td>");//12
                    tr.append("<td style='width:25px'>" + DISCOUNTAMT + "</td>");//13
                    tr.append("<td style='width:25px'>" + AFTERDISCOUNTAMT + "</td>");//14
                    tr.append("<td style='width:25px'>" + ITEMWISEFREIGHT + "</td>");//15
                    tr.append("<td style='width:25px'>" + AFTERITEMWISEFREIGHTAMT + "</td>");//16
                    tr.append("<td style='width:25px'>" + ITEMWISEADDCOST + "</td>");//17
                    tr.append("<td style='width:25px'>" + AFTERITEMWISEADDCOSTAMT + "</td>");//18
                    tr.append("<td style='width:25px'>" + BATCHNO + "</td>");//19
                    tr.append("<td style='width:25px'>" + parseFloat(cgsttax).toFixed(2) + "</td>");//20
                    tr.append("<td style='width:25px'>" + parseFloat(TaxAmount).toFixed(2) + "</td>");//21
                    tr.append("<td style='width:25px'>" + parseFloat(sgsttax).toFixed(2) + "</td>");//22
                    tr.append("<td style='width:25px'>" + parseFloat(TaxAmount).toFixed(2) + "</td>");//23
                    tr.append("<td style='width:25px'>" + NETWEIGHT + "</td>");//24
                    tr.append("<td style='width:25px'>" + mastermfgdate + "</td>");//25
                    tr.append("<td style='width:25px'>" + masterexpdate + "</td>");//26
                    tr.append("<td style='width:25px'>" + GROSSWEIGHT + "</td>");//27
                    tr.append("<td style='width:25px'>" + DEPOTRATE + "</td>");//28
                    tr.append("<td style='display: none'>" + GUID + "</td>");//29
                    tr.append("<td style='display: none'>" + _packsizeid + "</td>");//30
                    tr.append("<td style='text-align: center'><input type='image' class='gvTempDelete'  id='btndelete' <img src='../Images/ico_delete_16.png'/></input></td>");
                    $("#grdAddGrn").append(tr);
                    tr.append("</tbody>");
                }
                else {
                    tr.append("<thead style='background - color: cornflowerblue; font - size: 14px'><tr><th style='display: none'><th style='display: none'></th><th style='display: none'></th><th>SLNO</th><th>PO.DATE</th><th> PO.NO</th><th>PO.QTY</th><th>PACK SIZE</th><th> HSN CODE</th><th>PRODUCT NAME</th><th>RECEIVED QTY.</th><th>RATE </th> <th>AMOUNT </th><th>DISC % </th><th> DISC RATE </th><th> AFTERDISCOUNT AMT</th><th> ITEMWISE FREIGHT</th><th>AFTERITEMWISE FREIGHTAMT</th><th>ITEMWISE ADDCOST</th><th>AFTERITEMWISE ADDCOST</th><th>BATCH NO.</th><th>INPUT IGST %</th><th>INPUT IGST</th><th>NET WEIGHT</th><th>MFDATE</th><th>EXP DATE</th>.<th>GROSSWEIGHT</th><th>DEPOTRARE</th><th>DELETE</th></tr></thead><tbody>")
                    debugger;
                    tr = $('<tr/>');
                    tr.append("<td style='width:25px'>" + srl + "</td>");//0
                    tr.append("<td style='display: none'>" + _poid + "</td>");//1
                    tr.append("<td style='display: none'>" + _productid + "</td>");//2
                    tr.append("<td style='width:25px'>" + _podate + "</td>");//3
                    tr.append("<td style='width:25px'>" + _pono + "</td>");//4
                    tr.append("<td style='width:25px'>" + poqty + "</td>");//5
                    tr.append("<td style='width:25px'>" + packsizename + "</td>");//6
                    tr.append("<td style='width:25px'>" + hsncode + "</td>");//7
                    tr.append("<td style='width:25px'>" + productname + "</td>");//8
                    tr.append("<td style='width:25px'>" + RECEIVEDQTY + "</td>");//9
                    tr.append("<td style='width:25px'>" + _rate + "</td>");//10
                    tr.append("<td style='width:25px'>" + AMOUNT + "</td>");//11
                    tr.append("<td style='width:25px'>" + DISCOUNTPER + "</td>");//12
                    tr.append("<td style='width:25px'>" + DISCOUNTAMT + "</td>");//13
                    tr.append("<td style='width:25px'>" + AFTERDISCOUNTAMT + "</td>");//14
                    tr.append("<td style='width:25px'>" + ITEMWISEFREIGHT + "</td>");//15
                    tr.append("<td style='width:25px'>" + AFTERITEMWISEFREIGHTAMT + "</td>");//16
                    tr.append("<td style='width:25px'>" + ITEMWISEADDCOST + "</td>");//17
                    tr.append("<td style='width:25px'>" + AFTERITEMWISEADDCOSTAMT + "</td>");//18
                    tr.append("<td style='width:25px'>" + BATCHNO + "</td>");//19
                    tr.append("<td style='width:25px'>" + igsttax + "</td>");//20
                    tr.append("<td style='width:25px'>" + TaxAmount + "</td>");//21
                    tr.append("<td style='width:25px'>" + NETWEIGHT + "</td>");//22
                    tr.append("<td style='width:25px'>" + mastermfgdate + "</td>");//23
                    tr.append("<td style='width:25px'>" + masterexpdate + "</td>");//24
                    tr.append("<td style='width:25px'>" + GROSSWEIGHT + "</td>");//25
                    tr.append("<td style='width:25px'>" + DEPOTRATE + "</td>");//26
                    tr.append("<td style='display: none'>" + GUID + "</td>");//27
                    tr.append("<td style='display: none'>" + _packsizeid + "</td>");//28
                    tr.append("<td style='text-align: center'><input type='image' class='gvTempDelete'  id='btndelete' <img src='../Images/ico_delete_16.png'/></input></td>");
                    $("#grdAddGrn").append(tr);
                    tr.append("</tbody>");
                }

            }
            else {
                if ($("#hdntaxcount").val().trim() == '2') {
                    debugger;
                    tr = $('<tr/>');
                    tr.append("<td style='width:25px'>" + srl + "</td>");//0
                    tr.append("<td style='display: none'>" + _poid + "</td>");//1
                    tr.append("<td style='display: none'>" + _productid + "</td>");//2
                    tr.append("<td style='width:25px'>" + _podate + "</td>");//3
                    tr.append("<td style='width:25px'>" + _pono + "</td>");//4
                    tr.append("<td style='width:25px'>" + poqty + "</td>");//5
                    tr.append("<td style='width:25px'>" + packsizename + "</td>");//6
                    tr.append("<td style='width:25px'>" + hsncode + "</td>");//7
                    tr.append("<td style='width:25px'>" + productname + "</td>");//8
                    tr.append("<td style='width:25px'>" + RECEIVEDQTY + "</td>");//9
                    tr.append("<td style='width:25px'>" + _rate + "</td>");//10
                    tr.append("<td style='width:25px'>" + AMOUNT + "</td>");//11
                    tr.append("<td style='width:25px'>" + DISCOUNTPER + "</td>");//12
                    tr.append("<td style='width:25px'>" + DISCOUNTAMT + "</td>");//13
                    tr.append("<td style='width:25px'>" + AFTERDISCOUNTAMT + "</td>");//14
                    tr.append("<td style='width:25px'>" + ITEMWISEFREIGHT + "</td>");//15
                    tr.append("<td style='width:25px'>" + AFTERITEMWISEFREIGHTAMT + "</td>");//16
                    tr.append("<td style='width:25px'>" + ITEMWISEADDCOST + "</td>");//17
                    tr.append("<td style='width:25px'>" + AFTERITEMWISEADDCOSTAMT + "</td>");//18
                    tr.append("<td style='width:25px'>" + BATCHNO + "</td>");//19
                    tr.append("<td style='width:25px'>" + cgsttax + "</td>");//20
                    tr.append("<td style='width:25px'>" + TaxAmount + "</td>");//21
                    tr.append("<td style='width:25px'>" + sgsttax + "</td>");//22
                    tr.append("<td style='width:25px'>" + TaxAmount + "</td>");//23
                    tr.append("<td style='width:25px'>" + NETWEIGHT + "</td>");//24
                    tr.append("<td style='width:25px'>" + mastermfgdate + "</td>");//25
                    tr.append("<td style='width:25px'>" + masterexpdate + "</td>");//26
                    tr.append("<td style='width:25px'>" + GROSSWEIGHT + "</td>");//27
                    tr.append("<td style='width:25px'>" + DEPOTRATE + "</td>");//28
                    tr.append("<td style='display: none'>" + GUID + "</td>");//29
                    tr.append("<td style='display: none'>" + _packsizeid + "</td>");//30
                    tr.append("<td style='text-align: center'><input type='image' class='gvTempDelete'  id='btndelete' <img src='../Images/ico_delete_16.png'/></input></td>");
                    $("#grdAddGrn").append(tr);
                    tr.append("</tbody>");
                }
                else {

                    debugger;
                    tr = $('<tr/>');
                    tr.append("<td style='width:25px'>" + srl + "</td>");//0
                    tr.append("<td style='display: none'>" + _poid + "</td>");//1
                    tr.append("<td style='display: none'>" + _productid + "</td>");//2
                    tr.append("<td style='width:25px'>" + _podate + "</td>");//3
                    tr.append("<td style='width:25px'>" + _pono + "</td>");//4
                    tr.append("<td style='width:25px'>" + poqty + "</td>");//5
                    tr.append("<td style='width:25px'>" + packsizename + "</td>");//6
                    tr.append("<td style='width:25px'>" + hsncode + "</td>");//7
                    tr.append("<td style='width:25px'>" + productname + "</td>");//8
                    tr.append("<td style='width:25px'>" + RECEIVEDQTY + "</td>");//9
                    tr.append("<td style='width:25px'>" + _rate + "</td>");//10
                    tr.append("<td style='width:25px'>" + AMOUNT + "</td>");//11
                    tr.append("<td style='width:25px'>" + DISCOUNTPER + "</td>");//12
                    tr.append("<td style='width:25px'>" + DISCOUNTAMT + "</td>");//13
                    tr.append("<td style='width:25px'>" + AFTERDISCOUNTAMT + "</td>");//14
                    tr.append("<td style='width:25px'>" + ITEMWISEFREIGHT + "</td>");//15
                    tr.append("<td style='width:25px'>" + AFTERITEMWISEFREIGHTAMT + "</td>");//16
                    tr.append("<td style='width:25px'>" + ITEMWISEADDCOST + "</td>");//17
                    tr.append("<td style='width:25px'>" + AFTERITEMWISEADDCOSTAMT + "</td>");//18
                    tr.append("<td style='width:25px'>" + BATCHNO + "</td>");//19
                    tr.append("<td style='width:25px'>" + igsttax + "</td>");//20
                    tr.append("<td style='width:25px'>" + TaxAmount + "</td>");//21
                    tr.append("<td style='width:25px'>" + NETWEIGHT + "</td>");//22
                    tr.append("<td style='width:25px'>" + mastermfgdate + "</td>");//23
                    tr.append("<td style='width:25px'>" + masterexpdate + "</td>");//24
                    tr.append("<td style='width:25px'>" + GROSSWEIGHT + "</td>");//25
                    tr.append("<td style='width:25px'>" + DEPOTRATE + "</td>");//26
                    tr.append("<td style='display: none'>" + GUID + "</td>");//27
                    tr.append("<td style='display: none'>" + _packsizeid + "</td>");//28
                    tr.append("<td style='text-align: center'><input type='image' class='gvTempDelete'  id='btndelete' <img src='../Images/ico_delete_16.png'/></input></td>");
                    $("#grdAddGrn").append(tr);
                    tr.append("</tbody>");
                }
            }

            updateRowCount();
            debugger;
            Calculateamount();
            $("#hdnGuid").val('');
            debugger;
            var MRP = 0;
            if ($("#hdntaxcount").val().trim() == '2') {
                addRowinTaxTable(cgsttax, TaxAmount, $("#hdncgsttaxid").val(), MRP, '1', _poid);
                addRowinTaxTable(sgsttax, TaxAmount, $("#hdnsgsttaxid").val(), MRP, '1', _poid);

                //debugger;
                addRowinRejectionTaxTable(_poid, _productid, cgsttax, TaxAmount, _productname,$("#hdncgsttaxid").val());
                addRowinRejectionTaxTable(_poid, _productid, sgsttax, TaxAmount, _productname, $("#hdnsgsttaxid").val());

            }
            else {
                addRowinTaxTable(TAX, TaxAmount, $("#hdnigsttaxid").val(), MRP, '0', _poid);
                addRowinRejectionTaxTable(_poid, _productid, TAX, TaxAmount, _productname, $("#hdnigsttaxid").val());
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

$(function () {
    var grnid;
    debugger;
    $("body").on("click", "#grndetailsGrid .gvdwn", function () {

        if (OP == "QC") {
            $('#addQcUpload').css("display", "");
        }
        else if (OP = "MAKER") {
            $('#addQcUpload').css("display", "none");

        }
        var row = $(this).closest("tr");
        grnid = row.find('td:eq(1)').html();
        $("#hdngrnid").val(grnid);

        $("#VENDORFROM").chosen('destroy');
        $("#VENDORFROM").chosen({ width: '150px' });
        $("#VENDORID").chosen('destroy');
        $("#VENDORID").chosen({ width: '200px' });
        $("#MODEOFTRANSPORT").chosen('destroy');
        $("#MODEOFTRANSPORT").chosen({ width: '150px' });
        $("#TRANSPORTERID").chosen('destroy');
        $("#TRANSPORTERID").chosen({ width: '150px' });


        $("#POID").chosen('destroy');
        $("#POID").chosen({ width: '230px' });
        $("#MATERIALID").chosen('destroy');
        $("#MATERIALID").chosen({ width: '900px' });

        $('#addnew').css("display", "");
        $('#showdata').css("display", "none");
        ReleaseSession();
        Clearcontrol();
        LoadTpu();
        LoadPoMM();
        LoadTpu_Transporter();
        EditDetails(grnid);

        $("#VENDORFROM").prop("disabled", true);
        $("#VENDORFROM").chosen({
            search_contains: true
        });
        $("#VENDORFROM").trigger("chosen:updated");

        $("#VENDORID").prop("disabled", true);
        $("#VENDORID").chosen({
            search_contains: true
        });
        $("#VENDORID").trigger("chosen:updated");

        $("#TRANSPORTERID").prop("disabled", true);
        $("#TRANSPORTERID").chosen({
            search_contains: true
        });
        $("#TRANSPORTERID").trigger("chosen:updated");

        $("#POID").chosen({
            search_contains: true
        });
        $("#POID").trigger("chosen:updated");

    })
});

function EditDetails(grnid) {
    debugger;
    var tr;
    var HeaderCount = 0;
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
    var PolicyNo = '';
    var OrderID = '';
    $.ajax({
        type: "POST",
        url: "/mmpo/EditReceivedDetails",
        data: { grnid: grnid },
        dataType: "json",
        async: false,
        success: function (response) {

            var listHeader = response.alleditDataset.varSTOCKRECEIVEDHEADERs;
            var listDetails = response.alleditDataset.varSTOCKRECEIVEDDETAILs;
            var listTaxcount = response.alleditDataset.varTAXCOMPONENTCOUNTs;
            var listFooter = response.alleditDataset.varSTOCKRECEIVEDFOOTERs;
            var listTax = response.alleditDataset.varSTOCKRECEIVEDTAXes;
            var listTERMS = response.alleditDataset.varSTOCKRECEIVEDTERMs;
            var listITEMWiseTax = response.alleditDataset.varSTOCKRECEIVEDITEMWISETAXes;
            var listRejectionDetails = response.alleditDataset.varSTOCKRECEIVEDREJECTIONDETAILs;
            var listAdditionaletais = response.alleditDataset.varGRNADDITIONALDETAILs;
            var listRejectionTax = response.alleditDataset.varSTOCKRECEIVEDREJECTIONTAXes;
            var listJOBORDERRECEIVED_DETAILS = response.alleditDataset.varJOBORDERRECEIVEDDETAILs;
            var listStockreceivedSampleQty = response.alleditDataset.varSTOCKRECEIVEDSAMPLEQTies;
            var listStockreceivedSampleName = response.alleditDataset.varSTOCKRECEIVEDSAMPLEQTYNAMEs;




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
                    $.each(listTaxcount, function (key, item) {
                        debugger;
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
            debugger;
            /*Invoice Header Info*/
            $.each(listHeader, function (key, item) {
                //debugger;
                $("#DESPATCHNO").val(item.STOCKRECEIVEDNO);
                $("#DESPATCHDATE").val(item.STOCKRECEIVEDDATE);
                $("#VENDORFROM").val(item.VENDORFROM);

                $("#VENDORID").val(item.VENDORID);
                $("#VENDORINAME").val(item.VENDORNAME);

                $("#TRANSPORTERID").val(this['TRANSPORTERID'].toString().trim());


                $("#MODEOFTRANSPORT").val(item.MODEOFTRANSPORT);
                $("#VEHICLENO").val(item.VEHICHLENO);
                $("#LRGRNO").val(item.LRGRNO);
                $("#LRGRDATE").val(item.LRGRDATE);
                $("#WAYBILLKEY").val(item.WAYBILLKEY);
                $("#INVOICENO").val(this['INVOICENO'].toString().trim());
                $("#REMARKS").val(item.REMARKS);
                $("#INVOICEDATE").val(item.INVOICEDATE);
                $("#GATEPASSNO").val(item.GATEPASSNO);
                $("#GATEPASSDATE").val(item.GATEPASSDATE);

            });

            LoadPoMM();

            debugger;
            $.each(listDetails, function (key, item) {
                $("#POID").val(item.POID);
                $("#PONO").val(item.PONO);
            });

            LoadProductDetails();

            /*Invoice Details Info*/
            if (listDetails.length > 0) {

                $("#grdAddGrn").empty();
                if ($("#hdntaxcount").val().trim() == '1') {
                    tr = $('#grdAddGrn');
                    HeaderCount = $('#grdAddGrn thead th').length;
                    if (HeaderCount == 0) {
                        tr.append("<thead style='background - color: cornflowerblue; font - size: 14px'><tr><th style='display: none'><th style='display: none'></th><th style='display: none'></th><th>SLNO</th><th>PO.DATE</th><th> PO.NO</th><th>PO.QTY</th><th>PACK SIZE</th><th> HSN CODE</th><th>PRODUCT NAME</th><th>RECEIVED QTY.</th><th>RATE </th> <th>AMOUNT </th><th>DISC % </th><th> DISC RATE </th><th> AFTERDISCOUNT AMT</th><th> ITEMWISE FREIGHT</th><th>AFTERITEMWISE FREIGHTAMT</th><th>ITEMWISE ADDCOST</th><th>AFTERITEMWISE ADDCOST</th><th>BATCH NO.</th><th>INPUT CGST %</th><th>INPUT CGST</th><th>INPUT SGST %</th><th>INPUT SGST.</th><th>INPUT IGST %</th><th>INPUT IGST</th><th>NET WEIGHT</th><th>MFDATE</th><th>EXP DATE</th>.<th>GROSSWEIGHT</th><th>DEPOTRARE</th><th></th><th></th><th>DELETE</th></tr></thead><tbody>")
                    }
                    $.each(listDetails, function (index, record) {
                        //debugger;
                        tr = $('<tr/>');
                        tr.append("<td style='text-align: center'>" + srl + "</td>");//0
                        tr.append("<td style='display: none'>" + this['POID'].toString().trim() + "</td>");//1
                        tr.append("<td style='display: none'>" + this['PRODUCTID'].toString().trim() + "</td>");//2
                        tr.append("<td style='width:250px'>" + this['PODATE'].toString().trim() + "</td>");//3
                        tr.append("<td style='text-align: center'>" + this['PONO'].toString().trim() + "</td>");//4
                        tr.append("<td style='text-align: center'>" + this['POQTY'].toString().trim() + "</td>");//5
                        tr.append("<td style='text-align: right'>" + this['PACKINGSIZENAME'].toString().trim() + "</td>");//7
                        tr.append("<td style='text-align: right'>" + this['HSNCODE'].toString().trim() + "</td>");//8
                        tr.append("<td style='text-align: right'>" + parseInt(this['PRODUCTNAME'].toString().trim()) + "</td>");//9
                        tr.append("<td style='text-align: right'>" + parseFloat(this['RECEIVEDQTY'].toString().trim()).toFixed(2) + "</td>");//10
                        tr.append("<td style='text-align: right'>" + this['RATE'].toString().trim() + "</td>");//11
                        tr.append("<td style='text-align: right'>" + parseFloat(this['AMOUNT'].toString().trim()).toFixed(2) + "</td>");;//12
                        tr.append("<td style='text-align: right'>" + this['DISCOUNTPER'].toString().trim() + "</td>");//13
                        tr.append("<td style='text-align: right'>" + this['DISCOUNTAMT'].toString().trim() + "</td>");//14
                        tr.append("<td style='text-align: right'>" + this['AFTERDISCOUNTAMT'].toString().trim() + "</td>");//15
                        tr.append("<td style='text-align: right'>" + this['ITEMWISEFREIGHT'].toString().trim() + "</td>");//16  
                        tr.append("<td style='text-align: right'>" + this['AFTERITEMWISEFREIGHTAMT'].toString().trim() + "</td>");//17  
                        tr.append("<td style='text-align: right'>" + this['ITEMWISEADDCOST'].toString().trim() + "</td>");//18 
                        tr.append("<td style='text-align: right'>" + this['AFTERITEMWISEADDCOSTAMT'].toString().trim() + "</td>");//19  
                        tr.append("<td style='text-align: right'>" + this['BATCHNO'].toString().trim() + "</td>");//20 
                        IgstPercentage = GetTaxOnEdit(grnid.trim(), Igstid, this['PRODUCTID'].toString().trim(), this['BATCHNO'].toString().trim());
                        IgstAmount = ((parseFloat(this['AMOUNT'].toString().trim()) * IgstPercentage) / 100);
                        NetAmount = (parseFloat(this['AMOUNT'].toString().trim()) + IgstAmount);
                        tr.append("<td style='text-align: right'>" + parseFloat(IgstPercentage).toFixed(2) + "</td>");//21
                        tr.append("<td style='text-align: right'>" + IgstAmount.toFixed(2) + "</td>");//22
                        tr.append("<td style='text-align: right'>" + this['WEIGHT'].toString().trim() + "</td>");//23 
                        tr.append("<td style='text-align: right'>" + this['MFDATE'].toString().trim() + "</td>");//24
                        tr.append("<td style='text-align: right'>" + this['EXPRDATE'].toString().trim() + "</td>");//25  
                        tr.append("<td style='text-align: right'>" + this['GROSSWEIGHT'].toString().trim() + "</td>");//26
                        tr.append("<td style='text-align: right'>" + this['DEPOTRATE'].toString().trim() + "</td>");//27 
                        tr.append("<td style='display: none'>" + this['STOCKRECEIVEDAUTOID'].toString().trim() + "</td>");//28
                        tr.append("<td style='display: none'>" + this['PACKINGSIZEID'].toString().trim() + "</td>");//29

                        tr.append("<td style='text-align: center'><input type='image' class='gvTempDelete'  id='btndelete' <img src='../Images/ico_delete_16.png'/></input></td>");
                        $("#grdAddGrn").append(tr);
                        tr.append("</tbody>");

                    });
                }
                else if ($("#hdntaxcount").val().trim() == '2') {
                    debugger;
                    tr = $('#grdAddGrn');
                    HeaderCount = $('#grdAddGrn thead th').length;
                    if (HeaderCount == 0) {
                        tr.append("<thead style='background - color: cornflowerblue; font - size: 14px'><tr><th>SLNO</th><th>PO.DATE</th><th> PO.NO</th><th>PO.QTY</th><th>PACK SIZE</th><th> HSN CODE</th><th>PRODUCT NAME</th><th>RECEIVED QTY.</th><th>RATE </th> <th>AMOUNT </th><th>DISC % </th><th> DISC RATE </th><th> AFTERDISCOUNT AMT</th><th> ITEMWISE FREIGHT</th><th>AFTERITEMWISE FREIGHTAMT</th><th>ITEMWISE ADDCOST</th><th>AFTERITEMWISE ADDCOST</th><th>BATCH NO.</th><th>INPUT CGST %</th><th>INPUT CGST</th><th>INPUT SGST %</th><th>INPUT SGST.</th><th>NET WEIGHT</th><th>MFDATE</th><th>EXP DATE</th>.<th>GROSSWEIGHT</th><th>DEPOTRARE</th></th><th>DELETE</th></tr></thead><tbody>")
                    }
                    $.each(listDetails, function (index, record) {
                        debugger;
                        tr = $('<tr/>');
                        tr.append("<td style='text-align: center'>" + srl + "</td>");//0
                        tr.append("<td style='display: none'>" + this['POID'].toString().trim() + "</td>");//1
                        tr.append("<td style='display: none'>" + this['PRODUCTID'].toString().trim() + "</td>");//2
                        tr.append("<td style='width:250px'>" + this['PODATE'].toString().trim() + "</td>");//3
                        tr.append("<td style='text-align: center'>" + this['PONO'].toString().trim() + "</td>");//4
                        tr.append("<td style='text-align: center'>" + this['POQTY'].toString().trim() + "</td>");//5
                        tr.append("<td style='text-align: right'>" + this['PACKINGSIZENAME'].toString().trim() + "</td>");//7
                        tr.append("<td style='text-align: right'>" + this['HSNCODE'].toString().trim() + "</td>");//8
                        tr.append("<td style='text-align: right'>" + this['PRODUCTNAME'].toString().trim() + "</td>");//10
                        tr.append("<td style='text-align: right'>" + parseFloat(this['RECEIVEDQTY'].toString().trim()).toFixed(2) + "</td>");//11
                        tr.append("<td style='text-align: right'>" + this['RATE'].toString().trim() + "</td>");//12
                        tr.append("<td style='text-align: right'>" + parseFloat(this['AMOUNT'].toString().trim()).toFixed(2) + "</td>");;//13
                        tr.append("<td style='text-align: right'>" + this['DISCOUNTPER'].toString().trim() + "</td>");//14
                        tr.append("<td style='text-align: right'>" + this['DISCOUNTAMT'].toString().trim() + "</td>");//15
                        tr.append("<td style='text-align: right'>" + this['AFTERDISCOUNTAMT'].toString().trim() + "</td>");//16
                        tr.append("<td style='text-align: right'>" + this['ITEMWISEFREIGHT'].toString().trim() + "</td>");//17  
                        tr.append("<td style='text-align: right'>" + this['AFTERITEMWISEFREIGHTAMT'].toString().trim() + "</td>");//18  
                        tr.append("<td style='text-align: right'>" + this['ITEMWISEADDCOST'].toString().trim() + "</td>");//19 
                        tr.append("<td style='text-align: right'>" + this['AFTERITEMWISEADDCOSTAMT'].toString().trim() + "</td>");//20  
                        tr.append("<td style='text-align: right'>" + this['BATCHNO'].toString().trim() + "</td>");//21  
                        CgstPercentage = GetTaxOnEdit(grnid.trim(), Cgstid, this['PRODUCTID'].toString().trim(), this['BATCHNO'].toString().trim()); CgstPercentage = GetTaxOnEdit(grnid.trim(), Cgstid, this['PRODUCTID'].toString().trim(), this['BATCHNO'].toString().trim());
                        CgstAmount = ((parseFloat(this['AMOUNT'].toString().trim()) * CgstPercentage) / 100);
                        SgstPercentage = GetTaxOnEdit(grnid.trim(), Sgstid, this['PRODUCTID'].toString().trim(), this['BATCHNO'].toString().trim());
                        SgstAmount = ((parseFloat(this['AMOUNT'].toString().trim()) * SgstPercentage) / 100);
                        NetAmount = (parseFloat(this['AMOUNT'].toString().trim()) + CgstAmount + SgstAmount);
                        tr.append("<td style='text-align: right'>" + parseFloat(CgstPercentage).toFixed(2) + "</td>");//22
                        tr.append("<td style='text-align: right'>" + CgstAmount.toFixed(2) + "</td>");//23
                        tr.append("<td style='text-align: right'>" + parseFloat(SgstPercentage).toFixed(2) + "</td>");//24
                        tr.append("<td style='text-align: right'>" + SgstAmount.toFixed(2) + "</td>");//25
                        tr.append("<td style='text-align: right'>" + this['WEIGHT'].toString().trim() + "</td>");//26 
                        tr.append("<td style='text-align: right'>" + this['MFDATE'].toString().trim() + "</td>");//27
                        tr.append("<td style='text-align: right'>" + this['EXPRDATE'].toString().trim() + "</td>");//28  
                        tr.append("<td style='text-align: right'>" + this['GROSSWEIGHT'].toString().trim() + "</td>");//29
                        tr.append("<td style='text-align: right'>" + this['DEPOTRATE'].toString().trim() + "</td>");//30 
                        tr.append("<td style='display: none'>" + this['STOCKRECEIVEDAUTOID'].toString().trim() + "</td>");//31
                        tr.append("<td style='display: none'>" + this['PACKINGSIZEID'].toString().trim() + "</td>");//32

                        tr.append("<td style='text-align: center'><input type='image' class='gvTempDelete'  id='btndelete' <img src='../Images/ico_delete_16.png'/></input></td>");
                        $("#grdAddGrn").append(tr);
                        tr.append("</tbody>");

                    });
                }
            }

            /*Tax Details Info*/
            if ($("#hdntaxcount").val() == '1') {
                $.each(listITEMWiseTax, function (index, record) {
                    debugger;
                    addRowinTaxTableEdit(this['PRODUCTID'].toString().trim(), this['BATCHNO'].toString().trim(), this['TAXID'].toString().trim(), this['PERCENTAGE'].toString().trim(), this['TAXVALUE'].toString().trim(), this['MRP'].toString().trim(), '1', this['POID'].toString().trim());

                });
                $.each(listRejectionTax, function (index, record) {
                    addRowinRejectionTaxTableEdit(this['PRODUCTID'].toString().trim(), this['BATCHNO'].toString().trim(), this['TAXID'].toString().trim(), this['PERCENTAGE'].toString().trim(), this['TAXVALUE'].toString().trim(), this['MRP'].toString().trim(), '1');
                });
            }
            else if ($("#hdntaxcount").val() == '2') {

                $.each(listITEMWiseTax, function (index, record) {
                    debugger;
                    addRowinTaxTableEdit(this['PRODUCTID'].toString().trim(), this['BATCHNO'].toString().trim(), this['TAXID'].toString().trim(), this['PERCENTAGE'].toString().trim(), this['TAXVALUE'].toString().trim(), this['MRP'].toString().trim(), '1', this['POID'].toString().trim());

                });
                $.each(listRejectionTax, function (index, record) {
                    addRowinRejectionTaxTableEdit(this['POID'].toString().trim(), this['PRODUCTID'].toString().trim(), this['PERCENTAGE'].toString().trim(), this['TAXVALUE'].toString().trim(), this['PRODUCTNAME'].toString().trim(), this['TAXID'].toString().trim());
                });
            }
            else {
                addRowinTaxTableEdit('NA', 'NA', 'NA', 0, 0, 0, '0', 'NA');
                addRowinRejectionTaxTableEdit('NA', 'NA', '0', '0', 'NA','0');
            }

            /*Footer Details Info*/
            if (listFooter.length > 0) {
                $.each(listFooter, function (index, record) {
                    $('#BASICVALUE').val(parseFloat(this['BASICVALUE'].toString().trim()).toFixed(2));
                    $('#MRPVALUE').val(parseFloat(this['MRPVALUE'].toString().trim()).toFixed(2));
                    $('#TAXVALUE').val(parseFloat(this['TAXVALUE'].toString().trim()).toFixed(2));
                    $('#BASICWITHTAX').val(parseFloat(this['BASICWITHTAX'].toString().trim()).toFixed(2));

                    $('#GROSSAMOUNT').val(parseFloat(this['GROSSAMOUNT'].toString().trim()).toFixed(2));
                    $('#ROUNDOFF').val(parseFloat(this['ROUNDOFFVALUE'].toString().trim()).toFixed(2));
                    $('#OTHCHARGEAMT').val(parseFloat(this['OTHERCHARGESVALUE'].toString().trim()).toFixed(2));
                    $("#NETAMT").val(this['TOTALDESPATCHVALUE'].toString().trim());
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

function addRowinTaxTable(TAX, TaxAmount, taxid, MRP, TaxFlag, _poid) {
    //alert('tax');
    //alert($('#hdntaxid').val());
    $.ajax({
        type: "POST",
        url: "/mmpo/FillTaxDatatable",
        data: { Productid: productid, BatchNo: $("#BATCHNO").val(), TaxID: taxid, Percentage: parseFloat(TAX), TaxValue: parseFloat(TaxAmount), MRP: parseFloat(MRP), Flag: TaxFlag, POID: _poid },
        async: false,
        dataType: "json",
        success: function (response) {

        }
    });
}

function addRowinTaxTableEdit(ProductID, batch, taxid, TaxPercentage, TaxAmount, MRP, TaxFlag, _poid) {
    $.ajax({
        type: "POST",
        url: "/mmpo/FillTaxDatatable",
        data: { Productid: ProductID, BatchNo: batch, TaxID: taxid, Percentage: parseFloat(TaxPercentage), TaxValue: parseFloat(TaxAmount), MRP: parseFloat(MRP), Flag: TaxFlag, POID: _poid },
        async: false,
        dataType: "json",
        success: function (response) {

        }
    });
}

function addRowinRejectionTaxTable(_poid, _productid, TAX, TaxAmount, _productname, taxid) {
    var NAME = $("#hdntaxname").val();
    $.ajax({
        type: "POST",
        url: "/mmpo/FillRejectionTaxDatatable",
        data: { POID: _poid, PRODUCTID: _productid, BatchNo: $("#BATCHNO").val(), NAME: NAME, Percentage: TAX, TaxValue: TaxAmount, PRODUCTNAME: _productname, TaxID: taxid },
        async: false,
        dataType: "json",
        success: function (response) {

        }
    });
}

function addRowinRejectionTaxTableEdit(_poid, _productid, TAX, TaxAmount, _productname, taxid) {
    var NAME = $("#hdntaxname").val();
    $.ajax({
        type: "POST",
        url: "/mmpo/FillRejectionTaxDatatable",
        data: { POID: _poid, PRODUCTID: _productid, BatchNo: $("#BATCHNO").val(), NAME: NAME, Percentage: TAX, TaxValue: TaxAmount, PRODUCTNAME: _productname, TaxID: taxid },
        async: false,
        dataType: "json",
        success: function (response) {

        }
    });
}

function GetTaxOnEdit(grnid, taxid, productid, batchno) {
    debugger;
    var returnValue = null;
    $.ajax({
        type: "POST",
        url: "/mmpo/GetGrnTaxOnEdit",
        data: { grnid: grnid, TaxID: taxid, ProductID: productid, BatchNo: batchno },
        dataType: "json",
        async: false,
        success: function (grntaxonedit) {
            debugger;
            if (grntaxonedit.length > 0) {

                $.each(grntaxonedit, function (key, item) {

                    $("#hdntaxpercentage").val(grntaxonedit[0].TAXPERCENTAGE);
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
        failure: function (grntaxonedit) {
            alert(invoicetaxonedit.responseText);
        },
        error: function (grntaxonedit) {
            alert(invoicetaxonedit.responseText);
        }
    });
    return returnValue;
}

$("body").on("click", "#grdAddGrn #btndelete", function () {
    var row = $(this).closest("tr");
    grdproductid = row.find('td:eq(2)').html();
    grdbatchno = row.find('td:eq(19)').html();
    if (confirm("Do you want to delete this product?")) {
        row.remove();
        var rowCount = document.getElementById("grdAddGrn").rows.length - 1;
       // alert(rowCount);
        if (rowCount > 0) {
            $("#VENDORID").prop('disabled', "disabled");
            $("#VENDORFROM").prop('disabled', "disabled");
        }
        else {
            $("#VENDORID").prop("disabled", false);
            $("#VENDORID").chosen({
                search_contains: true
            });
            $("#VENDORID").trigger("chosen:updated");

            $("#VENDORFROM").prop("disabled", false);
            $("#VENDORFROM").chosen({
                search_contains: true
            });
            $("#VENDORFROM").trigger("chosen:updated");

            $("#TRANSPORTERID").prop("disabled", false);
            $("#TRANSPORTERID").chosen({
                search_contains: true
            });
            $("#TRANSPORTERID").trigger("chosen:updated");
        }
        deleteRowfromTaxTable(grdproductid, grdbatchno);
        updateRowCount();
        Calculateamount();

    }
})/*for temp delete*/

function deleteRowfromTaxTable(grdproductid, grdbatchno) {
    $.ajax({
        type: "POST",
        url: "/mmpo/DeleteTaxDatatable",
        data: { Productid: grdproductid.trim(), BatchNo: grdbatchno.trim() },
        dataType: "json",
        success: function (response) {

        }
    });
}

function updateRowCount() {
    var table = document.getElementById("grdAddGrn");
    var rowcountAfterDelete = document.getElementById("grdAddGrn").rows.length;
    for (var i = 1; i < rowcountAfterDelete; i++) {
        table.rows[i].cells[0].innerHTML = i;
    }

    var rowCount = document.getElementById("grdAddGrn").rows.length - 1;
    if (rowCount > 0) {
        $("#VENDORID").prop("disabled", true);

    }
    else {
        $('#grdAddGrn').empty();
        $("#VENDORID").prop("disabled", false);

    }
} /*updqate row number*/

function Calculateamount() {
    debugger;
    var totalbasicvalue = 0;
    var totaltax = 0;
    var tltbsctx = 0
    var totalnet = 0;
    var totalmrp = 0;
    var rowCount = document.getElementById("grdAddGrn").rows.length - 1;
    if (rowCount > 0) {
        $('#grdAddGrn tbody tr').each(function () {
            debugger;
            if ($("#hdntaxcount").val().trim() == '2') {
                totalbasicvalue += parseFloat($(this).find('td:eq(11)').html());
                totaltax += $.add(parseFloat($(this).find('td:eq(21)').html()), parseFloat($(this).find('td:eq(23)').html()));
                tltbsctx += totalbasicvalue + totaltax;

            }
            else {
                totalbasicvalue += parseFloat($(this).find('td:eq(11)').html());
                totaltax += parseFloat($(this).find('td:eq(20)').html());
                tltbsctx += totalbasicvalue + totaltax;
            }

        });


        $('#BASICVALUE').val(totalbasicvalue.toFixed(2));
        $('#TAXVALUE').val(totaltax.toFixed(2));
        $('#BASICWITHTAX').val(tltbsctx.toFixed(2));
        $('#GROSSAMOUNT').val(tltbsctx.toFixed(2));
        debugger;
        parts = tltbsctx - Math.floor(tltbsctx);
        // decval = 1 - parseFloat(parts);
        decval = parts.toFixed(2);

        if (decval >= .50) {
            decval = 1 - decval;
            decval = decval.toFixed(2);
            totalnet = $.add(parseFloat(tltbsctx), parseFloat(decval));

        }
        else {
            decval = -decval;
            totalnet = tltbsctx - decval;
        }

        $('#ROUNDOFF').val(decval);
        $('#NETAMT').val(totalnet.toFixed(2));


    }
    else {
        $('#BASICVALUE').val(0);
        $('#TAXVALUE').val(0);
        $('#BASICWITHTAX').val(0);
        $('#GROSSAMOUNT').val(0);
        $('#ROUNDOFF').val(0);
        $('#NETAMT').val(0);

    }
} /*for calculate amount after add button click*/

function saveGrndata() {
    debugger;
    var i = 0;
    grnlist = new Array();
    $('#grdAddGrn tbody tr').each(function () {
        var grdAddGrn = {};
        var Poid = $(this).find('td:eq(1)').html();
        var Productid = $(this).find('td:eq(2)').html();
        var Podate = $(this).find('td:eq(3)').html();
        var Pono = $(this).find('td:eq(4)').html();
        var Poqty = $(this).find('td:eq(5)').html();
        var Packsizename = $(this).find('td:eq(6)').html();
        var Hsncode = $(this).find('td:eq(7)').html();
        var Productname = $(this).find('td:eq(8)').html();
        var RECEIVEDQTY = $(this).find('td:eq(9)').html();
        var Rate = $(this).find('td:eq(10)').html();
        var AMOUNT = $(this).find('td:eq(11)').html();
        var DISCOUNTPER = $(this).find('td:eq(12)').html();
        var DISCOUNTAMT = $(this).find('td:eq(13)').html();
        var AFTERDISCOUNTAMT = $(this).find('td:eq(14)').html();
        var ITEMWISEFREIGHT = $(this).find('td:eq(15)').html();
        var AFTERITEMWISEFREIGHTAMT = $(this).find('td:eq(16)').html();
        var ITEMWISEADDCOST = $(this).find('td:eq(17)').html();
        var AFTERITEMWISEADDCOSTAMT = $(this).find('td:eq(18)').html();
        var BATCHNO = $(this).find('td:eq(19)').html();

        if ($("#hdntaxcount").val().trim() == '2') {
            var NETWEIGHT = $(this).find('td:eq(24)').html();
            var mastermfgdate = $(this).find('td:eq(25)').html();
            var masterexpdate = $(this).find('td:eq(26)').html();
            var GROSSWEIGHT = $(this).find('td:eq(27)').html();
            var DEPOTRATE = $(this).find('td:eq(28)').html();
            var GUID = $(this).find('td:eq(29)').html();
            var Packsizeid = $(this).find('td:eq(30)').html();

        }
        else {
            var NETWEIGHT = $(this).find('td:eq(22)').html();
            var mastermfgdate = $(this).find('td:eq(23)').html();
            var masterexpdate = $(this).find('td:eq(24)').html();
            var GROSSWEIGHT = $(this).find('td:eq(25)').html();
            var DEPOTRATE = $(this).find('td:eq(26)').html();
            var GUID = $(this).find('td:eq(26)').html();
            var Packsizeid = $(this).find('td:eq(28)').html();
        }





        grdAddGrn.Poid = Poid;
        grdAddGrn.Productid = Productid;
        grdAddGrn.Podate = Podate;
        grdAddGrn.Pono = Pono;
        grdAddGrn.Poqty = Poqty;
        grdAddGrn.Packsizeid = Packsizeid;
        grdAddGrn.Packsizename = Packsizename;
        grdAddGrn.Hsncode = Hsncode;
        grdAddGrn.Productname = Productname;
        grdAddGrn.RECEIVEDQTY = RECEIVEDQTY;
        grdAddGrn.Rate = Rate;
        grdAddGrn.AMOUNT = AMOUNT;
        grdAddGrn.DISCOUNTPER = DISCOUNTPER;
        grdAddGrn.DISCOUNTAMT = DISCOUNTAMT;
        grdAddGrn.AFTERDISCOUNTAMT = AFTERDISCOUNTAMT;
        grdAddGrn.ITEMWISEFREIGHT = ITEMWISEFREIGHT;
        grdAddGrn.AFTERITEMWISEFREIGHTAMT = AFTERITEMWISEFREIGHTAMT;
        grdAddGrn.ITEMWISEADDCOST = ITEMWISEADDCOST;
        grdAddGrn.AFTERITEMWISEADDCOSTAMT = AFTERITEMWISEADDCOSTAMT;
        grdAddGrn.BATCHNO = BATCHNO;
        grdAddGrn.NETWEIGHT = NETWEIGHT;
        grdAddGrn.mastermfgdate = mastermfgdate;
        grdAddGrn.masterexpdate = masterexpdate;
        grdAddGrn.GROSSWEIGHT = GROSSWEIGHT;
        grdAddGrn.DEPOTRATE = DEPOTRATE;

        grnlist[i++] = grdAddGrn;
    });
    debugger;
    var grnsave = {};
    grnsave.DESPATCHDATE = $("#DESPATCHDATE").val();
    grnsave.VENDORID = $("#VENDORID").val();
    grnsave.VENDORNAME = $("#VENDORNAME").val();
    grnsave.INVOICENO = $("#INVOICENO").val();
    grnsave.INVOICEDATE = $("#INVOICEDATE").val();
    grnsave.TRANSPORTERID = $("#TRANSPORTERID").val();
    grnsave.TRANSPORTERNAME = $("#TRANSPORTERNAME").val();
    grnsave.VEHICLENO = $("#VEHICLENO").val();
    grnsave.LRGRNO = $("#LRGRNO").val();
    grnsave.LRGRNO = $("#LRGRDATE").val();
    grnsave.MODEOFTRANSPORT = $("#MODEOFTRANSPORT").val();
    grnsave.REMARKS = $("#REMARKS").val();
    grnsave.GATEPASSNO = $("#GATEPASSNO").val();
    grnsave.GATEPASSDATE = $("#GATEPASSDATE").val();
    grnsave.VENDORFROM = $("#VENDORFROM").val();
    grnsave.WAYBILLKEY = $("#WAYBILLKEY").val();
    grnsave.RATE = $("#RATE").val();
    grnsave.MRP = $("#MRP").val();
    grnsave.BASICVALUE = $("#BASICVALUE").val();
    grnsave.MRPVALUE = $("#MRPVALUE").val();
    grnsave.TAXVALUE = $("#TAXVALUE").val();
    grnsave.BASICWITHTAX = $("#BASICWITHTAX").val();
    grnsave.GROSSAMOUNT = $("#GROSSAMOUNT").val();
    grnsave.ROUNDOFF = $("#ROUNDOFF").val();
    grnsave.OTHCHARGEAMT = $("#OTHCHARGEAMT").val();
    grnsave.NETAMT = $("#NETAMT").val();
    grnsave.ISVERIFIEDSTOCKIN = $("#hdnSTOCKIN").val();
    grnsave.ISVERIFIEDCHECKER1 = $("#hdnISVERIFIEDCHECKER1").val();

    grnsave.RECEIVEDID = $("#hdngrnid").val();



    if (grnlist.length == 0) {
        toastr.info("Please Select atleast one record");
        return;
    }


    grnsave.grnOrderdetails = grnlist,
        //alert(JSON.stringify(grnsave));
        $.ajax({
            url: "/mmpo/grnsavedata",
            //data: JSON.stringify(nccModel),
            data: '{grnsave:' + JSON.stringify(grnsave) + '}',
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
                    debugger;
                    $('#addnew').css("display", "none");
                    $('#showdata').css("display", "");
                    $('#ponoshow').css("display", "none");
                    if (OP == "MAKER") {
                        $('#btnnewentry').show();
                    }
                    else {
                        $('#btnnewentry').hide();
                    }

                    toastr.success("Your Grn Is" + ':' + messagetext);
                }
                else {

                    toastr.error("Error On Saving Record" + ':' + messagetext);
                }

            },
            error: function (errormessage) {
                // alert(errormessage.responseText);
            }
        });


}

function uuidv4() {
    debugger;
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });
}

/*clear,Reset,Enable and Disable */

function ReleaseSession() {

    $.ajax({
        type: "POST",
        url: "/mmpo/RemoveSessionGrnMM",
        data: '{}',
        dataType: "json",
        success: function (response) {


        }
    });
}

function Clearcontrol() {
    $("#hdntaxnameCGST").val(''),
        $("#hdntaxnameSGST").val(''),
        $("#hdntaxnameIGST").val(''),
        $("#hdncgsttaxid").val(''),
        $("#hdnsgsttaxid").val(''),
        $("#hdnigsttaxid").val(''),
        $("#hdnGuid").val(''),
        $("#hdntaxcount").val(''),
        $("#hdnasspercentage").val(''),
        $("#hdnweight").val(''),
        $("#hdncatid").val(''),
        $("#hdnhsn").val(''),
        $("#DESPATCHNO").val(''),
        $("#txtchkupload").val(''),


        $("#VENDORFROM").val('0');
    $("#VENDORFROM").prop("disabled", false);
    $("#VENDORFROM").chosen({
        search_contains: true
    });
    $("#VENDORFROM").trigger("chosen:updated");

    $("#VENDORID").val('0');
    $("#VENDORID").prop("disabled", false);
    $("#VENDORID").chosen({
        search_contains: true
    });
    $("#VENDORID").trigger("chosen:updated");

    $("#POID").empty(),
        $("#MATERIALID").empty(),
        $("#BATCHNO").val(''),
        $("#PACKSIZEID_FROM").empty(),
        $("#MFGDATE").val(''),
        $("#EXPDATE").val(''),
        $("#RATE").val(0),
        $("#MRP").val(0),
        $("#POQTY").val(0),
        $("#RECEIVEDQTY").val(0),
        $("#REMAININGQTY").val(0),
        $("#RECEIVE_QTY").val(0),
        $("#FREIGHTCHARGES").val(0),
        $("#ADDITIONALCOST").val(0),
        $("#TRANSPORTERID").empty(),
        $("#VEHICLENO").val(''),
        $("#LRGRNO").val(''),
        $("#WAYBILLKEY").val(''),
        $("#INVOICENO").val(''),
        $("#GATEPASSNO").val(''),
        $('#grdAddGrn tbody tr').remove();
    $('#grdAddGrn thead').remove();
    $("#BASICVALUE").val(0),
        $("#MRPVALUE").val(0),
        $("#TAXVALUE").val(0),
        $("#BASICWITHTAX").val(0),
        $("#GROSSAMOUNT").val(0),
        $("#ROUNDOFF").val(0),
        $("#OTHCHARGEAMT").val(0),
        $("#NETAMT").val(0),
        $("#REMARKS").val('')
}

function ResetControls() {

}

$(function () {
    $("body").on("click", "#grndetailsGrid .gvdelete", function () {
        debugger;
        row = $(this).closest("tr");
        var stockreceivedid = row.find('td:eq(1)').html();
        //status = row.find('td:eq(4)').html();
        // if (status == "PENDING") {
        if (confirm("Do you want to delete this Grn?")) {
            $.ajax({
                type: "POST",
                url: "/mmpo/DeleteStockDespatch",
                data: { stockreceivedid: stockreceivedid },
                dataType: "json",
                success: function (grnmm) {
                    debugger;
                    if (grnmm != "") {
                        toastr.success("Delete Successfull");
                        LoadGRN();
                    }
                    else {

                        toastr.warning("Error On Delete Record");
                    }

                },
            });
        }
        //}
        //else {
        //    toastr.warning("You Can't Delete this Po");
        //    return false;
        //}
    })
}) /*for delete*/

$(function () {
    var stockreceivedid;
    debugger;
    $("body").on("click", "#grndetailsGrid .gvprint", function () {

        var row = $(this).closest("tr");
        stockreceivedid = row.find('td:eq(1)').html();

        var url = "http://mcnroeerp.com/factory/FACTORY/frmRptPurchaseBillMM.aspx?id=" + stockreceivedid + "";
        window.open(url, "", "width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100");

    })

}) /*For Print*/

function addSampleQtyDatatable(Stockreceivedid, Poid, Productid, Productname, Receivedqty, Sampleqty, Observationqty) {
    debugger;
    $.ajax({
        type: "POST",
        url: "/mmpo/FillSampleQtyDatatable",
        data: { Stockreceivedid: Stockreceivedid, Poid: Poid, Productid: Productid, Productname: Productname, Receivedqty: Receivedqty, Sampleqty: Sampleqty, Observationqty: Observationqty },
        async: false,
        dataType: "json",

        success: function (response) {

        }
    });
    toastr.success("Sample Qty Save Successfully");
    $('#myModalqcsample').css("display", "none");
}