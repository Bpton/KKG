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

    //for query strings

    var qs = getQueryStrings();
    if (qs["CHECKER"] != undefined && qs["CHECKER"] != "") {

        CHECKER = qs["CHECKER"];
    }
    if (qs["MENUID"] != undefined && qs["MENUID"] != "") {

        MENUID = qs["MENUID"];
    }
    if (qs["VISIBLE"] != undefined && qs["VISIBLE"] != "") {

        var VISIBLE = qs["VISIBLE"];
    }
    if (qs["DEPOTID"] != undefined && qs["DEPOTID"] != "") {

        DEPOTID = qs["DEPOTID"];
    }
    FINYEAR = qs["FINYEAR"];
    UserID = qs["USERID"];
    $("#hdnCHECKER").val(CHECKER);
    $("#hdnmenuid").val(MENUID);
    $("#hdnvisible").val(VISIBLE)
    if (VISIBLE == 'Y') {

        $('#td_chktdslabel').css("display", "");
        $('#td_chktdsinput').css("display", "");
        $('#td_chkgstlabel').css("display", "");
        $('#td_chkgstinput').css("display", "");
        $("#chktdsapplicable").prop('checked', true);
        $("#chkcstapplicable").prop('checked', true);
        // $("form #chkcstapplicable").attr('checked', true)
        // $(':chkcstapplicable').prop('checked', true);
        // $('.myCheckbox').attr('checked', true)
    }
    else
        if (VISIBLE == 'N') {
            $('#td_chktdslabel').css("display", "");
            $('#td_chktdsinput').css("display", "");
            $('#td_chkgstlabel').css("display", "");
            $('#td_chkgstinput').css("display", "");
            $("#chktdsapplicable").prop('checked', true);
            $("#chkcstapplicable").prop('checked', true);

        }

    //fin yr check
    var currentdt;
    var frmdate;
    var todate;
    //date validation
    $.ajax({
        type: "POST",
        url: "/Transporter/finyrchk",
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



    $('#td_chktdslabel').css("display", "none");
    $('#td_chktdsinput').css("display", "none");


    $('#pnlAdd').css("display", "none");
    $('#tdlblexportdepot').css("display", "none");
    $('#tdddlexportdepot').css("display", "none");
    $('#tdmodule').css("display", "none");


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
    var frmyr = 2020;
   
    $("#txtFromDate").datepicker({
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
    $("#txtToDate").datepicker({
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
    $("#txtbillDate").datepicker({
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        selectCurrent: true,
        todayBtn: "linked",
        showAnim: "slideDown",
        yearRange: "-1:+0",
        maxDate: new Date(currentdt),
        minDate: new Date(frmdate),
        // minDate: new Date(frmyr, 04 - 1, 01),

        dateFormat: "dd/mm/yy",
        showOn: 'button',
        buttonText: 'Show Date',
        buttonImageOnly: true,
        buttonImage: 'http://jqueryui.com/resources/demos/datepicker/images/calendar.gif'
    });
    $(".ui-datepicker-trigger").mouseover(function () {
        $(this).css('cursor', 'pointer');
    });
    $("#txtbilldate2").datepicker({
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        selectCurrent: true,
        todayBtn: "linked",
        showAnim: "slideDown",
        yearRange: "-3:+0",

        maxDate: new Date(todate),
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
    $("#txtlrdate").datepicker({
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        selectCurrent: true,
        todayBtn: "linked",
        showAnim: "slideDown",
        yearRange: "-3:+0",
        maxDate: new Date(todate),
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
    $("#txtFromDate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
    $("#txtToDate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
    $("#txtbillDate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
    $("#txtbilldate2").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
    $("#txtlrdate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
    BindDepot();
    BindTransporterbillall();
    $('#pnlDisplay').css("display", "");
    $("#ddlregion").chosen({
        search_contains: true
    });



    $("#ddlregion").trigger("chosen:updated");

    $("#Btnadd").click(function (e) {

        addnew();

    });
    $("#btnClose").click(function (e) {

        close();

    });
    $('#ddlfromstate').change(function () {
        BindGstPercentage();
    });
    $('#ddlinvoice').change(function () {
        $("#txtBillvalue").val('');
        $("#txttdsdeductablevalue").val('');
        BindTransporterbyInvoice();
        //GetTdscheck();
        //GetReverseCharge();

    });
    $('#ddltransporter').change(function () {

        GetTdscheck();
        GetReverseCharge();
    });
    $('#txtBillvalue').change(function () {
        var billvalue = $("#txtBillvalue").val()
        $("#txttdsdeductablevalue").val(billvalue);
        loadtaxes();
        $("input#txtgrossweight").focus();
    });
    $("#btnADDGrid").click(function (e) {
        addgrid();
    });
    $("body").on("click", "#grddetail .grvdtldel", function () {

        if (confirm("Do you want to delete this Account?")) {
            var row = $(this).closest("tr");
            var GUID = row.find('td:eq(0)').html();
            delgrid(GUID);
        }


    });
    $("#btnSave").click(function (e) {
        SaveTransporterbillV2fac();
    });


    $("#STKINV").click(function () {

        $("#STKTRN").prop("checked", false);
        $("#DPTRCVD").prop("checked", false);
        $("#STKDES").prop("checked", false);
        $("#EXPORT").prop("checked", false);
        BindAllInvoicenoNEWV2();
    });
    $("#STKTRN").click(function () {

        $("#STKINV").prop("checked", false);
        $("#DPTRCVD").prop("checked", false);
        $("#STKDES").prop("checked", false);
        $("#EXPORT").prop("checked", false);
        BindAllInvoicenoNEWV2();
    });
    $("#DPTRCVD").click(function () {

        $("#STKINV").prop("checked", false);
        $("#STKTRN").prop("checked", false);
        $("#STKDES").prop("checked", false);
        $("#EXPORT").prop("checked", false);
        BindAllInvoicenoNEWV2();
    });
    $("#STKDES").click(function () {

        $("#STKINV").prop("checked", false);
        $("#STKTRN").prop("checked", false);
        $("#DPTRCVD").prop("checked", false);
        $("#EXPORT").prop("checked", false);
        BindAllInvoicenoNEWV2();
    });
    $("#EXPORT").click(function () {

        $("#STKINV").prop("checked", false);
        $("#STKTRN").prop("checked", false);
        $("#DPTRCVD").prop("checked", false);
        $("#STKDES").prop("checked", false);
        BindAllInvoicenoNEWV2();
    });
    $("#btnvouchersearch").click(function () {
        BindTransporterbillall();
    });
    $("body").on("click", "#gvtransporterbill .gvdel", function () {


        var TransporterbillID;
        var region = $('#ddlregion').val();
        var voucherapproived;
        var dayend;
        var row;
        var tdsrelated;

        //$('.debitdel').on('click', function () {

        row = $(this).closest("tr");
        TransporterbillID = row.find('td:eq(0)').html();


        if (confirm("Do you want to delete this voucher?")) {
            $.ajax({

                type: "POST",

                url: "/Transporter/DeleteTransporterbilldepot",

                //data: "{'voucherid': '" + voucherid + "','voucherapproived': '" + voucherapproived + "','dayend': '" + dayend + "','tdsrelated': '" + tdsrelated + "'}",
                data: { TransporterbillID: TransporterbillID, checker: CHECKER, region: region, userid: UserID, finyr: FINYEAR },
                //data: {voucherid: voucherid},
                dataType: "json",
                success: function (response) {
                    var flag = '';
                    $.each(response, function () {
                        flag = this['response'];
                    })


                    if (flag == 'Record Deleted Successfully') {
                        row.remove();
                    }
                    // $("#txtShippingAddress").val(response.d[1]);
                    alert(flag)



                }

            });

        }
    });
    $("body").on("click", "#gvtransporterbill .gvprint", function () {
        var TransporterbillID;
        var AccEntryID = '';
        var row = $(this).closest("tr");
        TransporterbillID = row.find('td:eq(0)').html();
        $.ajax({

            type: "POST",

            url: "/Transporter/GetAccEntryID",

            //data: "{'voucherid': '" + voucherid + "','voucherapproived': '" + voucherapproived + "','dayend': '" + dayend + "','tdsrelated': '" + tdsrelated + "'}",
            data: { transporterbillid: TransporterbillID },
            async: false,
            //data: {voucherid: voucherid},
            dataType: "json",
            success: function (response) {

                $.each(response, function () {
                    AccEntryID = this['response'];
                })

            }
        })


        if (AccEntryID != '') {
            // var url = "frmRptInvoicePrint.aspx?AdvRecpt_ID=" + voucherid + "&Voucherid=" + vouchertypeid;
            var url = "http://mcnroeerp.com/mcworld/FACTORY/frmRptInvoicePrint_FACv2.aspx?Stnid=" + TransporterbillID + "&TAG=TB&MenuId=45";
            // var url = "http://localhost:2319/FACTORY/frmRptInvoicePrint_FAC.aspx?Stnid=" + TransporterbillID + "&TAG= TAG&MenuId=45";
            window.open(url, "", "toolbar=yes,scrollbars=yes,resizable=yes,top=500,left=500,width=700,height=800");


        }
        else {
            toastr.info('This Bill is yet to be approved...!');
        }


        //$('.debitdel').on('click', function () {
        //var voucherid;
        //var depoid;
        //vouchertypeid = $('#hdnvoucherid').val();








    });
    //edit
    $("body").on("click", "#gvtransporterbill .gvedit", function () {
        var TransporterbillID;
        row = $(this).closest("tr");
        TransporterbillID = row.find('td:eq(0)').html();
        $("#hdntrnsID").val(TransporterbillID);

        editrec(TransporterbillID);

    });
    $("#chktdsapplicable1").click(function (e) {
        calculatetds();
    });
    $("#chkReversecharge").click(function (e) {
        var ischecked = $('#chkReversecharge').is(":checked");
        if (ischecked == true) {
            $("#gst1").html("CGST AMT (RCM)");
            $("#sgst1").html("SCGST AMT (RCM)");
            $("#igst1").html("IGST AMT (RCM)");
            $("#ugst1").html("UGST AMT (RCM)");
        }
        else
            if (ischecked == false) {
                $("#gst1").html("CGST AMT");
                $("#sgst1").html("SCGST AMT");
                $("#igst1").html("IGST AMT");
                $("#ugst1").html("UGST AMT");
            }


        Isrcmcheck();

    })
})

function editrec(TransporterbillID) {
    ReleaseSession();
    $('#pnlAdd').css("display", "block");

    $('#pnlDisplay').css("display", "none");
    $('#tr_billno').css("display", "");

    $.ajax({

        type: "POST",

        url: "/Transporter/EditTransporterbill",

        //data: "{'voucherid': '" + voucherid + "','voucherapproived': '" + voucherapproived + "','dayend': '" + dayend + "','tdsrelated': '" + tdsrelated + "'}",
        data: { ID: TransporterbillID },
        async: false,
        //data: {voucherid: voucherid},
        dataType: "json",
        success: function (response) {
            var tag;
            var REVERSECHARGE;
            var TDSAPPLICABLE;
            var ISTRANSFERTOHO;
            $.each(response, function () {
                $("#txtbillentryno").val(this["TRANSPORTERBILLNO"]);
                $("#txtbillDate").val(this["TRANSPORTERBILLDATE"]);
                $("#txtGNGNO").val(this["GNGNO"]);
                $("#txtremarks").val(this["REMARKS"]);
                $("#Txtbillno").val(this["BILLNO"]);
                $("#txtsumnetamt").val(this["TOTALNETAMOUNT"]);
                $("#txtsumgrossweight").val(this["TOTALGROSSWEIGHT"]);
                $("#txtsumtds").val(this["TOTALTDS"]);
                $("#txtsumtdsdeuctablevalue").val(this["TOTALTDSDEDUCTABLE"]);
                $("#txtsumbillvalue").val(this["TOTALBILLAMOUNT"]);
                $("#txtsumcgst").val(this["TOTALCGST"]);
                $("#txtsumsgst").val(this["TOTALSGST"]);
                $("#txtsumigst").val(this["TOTALIGST"]);
                $("#txtTDSpercentage1").val(this["TDSPECENTAGE"]);
                $("#txtremarks").val(this["REMARKS"]);
                $("#Txtbillno").val(this["BILLNO"]);
                $("#Txtbillno").removeAttr("disabled");
                tag = this["BILLTYPEID"];
                if (tag == 'STKINV') {
                    $("#STKINV").prop("checked", true);
                }
                else
                    if (tag == 'STKTRN') {
                        $("#STKTRN").prop("checked", true);
                    }
                    else
                        if (tag == 'DPTRCVD') {
                            $("#DPTRCVD").prop("checked", true);
                        }
                        else
                            if (tag == 'STKDES') {
                                $("#STKDES").prop("checked", true);
                            }
                            else
                                if (tag == 'EXPORT') {
                                    $("#EXPORT").prop("checked", true);
                                }
                REVERSECHARGE = this["REVERSECHARGE"];
                TDSAPPLICABLE = this["TDSAPPLICABLE"];
                ISTRANSFERTOHO = this["ISTRANSFERTOHO"];
                BindState();
                $("#ddlfromstate").val(this["BILLINGFROMSTATEID"]);
                $("#ddlfromstate").prop("disabled", true);
                $("#ddlfromstate").chosen({
                    search_contains: true
                });
                $("#ddlfromstate").trigger("chosen:updated");
                if (TDSAPPLICABLE == 'Y') {
                    $("#chktdsapplicable1").prop('checked', true);
                }
                else
                    if (TDSAPPLICABLE == 'N') {
                        $("#chktdsapplicable1").prop('checked', false);
                    }
                $("#chkReversecharge").attr("disabled", "disabled");

                if (REVERSECHARGE == 'Y') {
                    $("#chkReversecharge").prop('checked', true);
                }
                else
                    if (REVERSECHARGE == 'N') {
                        $("#chkReversecharge").prop('checked', false);
                    }

                if (ISTRANSFERTOHO == 'Y') {
                    $("#chktransferHO").prop('checked', true);
                }
                else
                    if (ISTRANSFERTOHO == 'N') {
                        $("#chktransferHO").prop('checked', false);
                    }


                $("#ddldepot").val(this["DEPOTID"]);
                $("#ddldepot").prop("disabled", true);
                $("#ddldepot").chosen({
                    search_contains: true
                });




                BindTransporter();
                $("#ddltransporter").val(this["TRANSPORTERID"]);
                $("#hdntransporterid").val(this["TRANSPORTERID"]);
                Edittdsid(this["TRANSPORTERID"]);
                $("#ddltransporter").prop("disabled", true);

                //$("#radio").prop("disabled", true);

                $("#ddltransporter").chosen({
                    search_contains: true
                });
                var netamt = 0;
                if (REVERSECHARGE == 'Y') {
                    netamt = parseFloat($("#txtsumbillvalue").val()) - ($("#txtsumtds").val())
                    $("#txtsumnetamtshow").val(netamt);
                }
                else
                    if (REVERSECHARGE == 'N') {
                        netamt = (parseFloat($("#txtsumbillvalue").val()) - ($("#txtsumtds").val())) + parseFloat($("#txtsumcgst").val()) + parseFloat($("#txtsumsgst").val()) + parseFloat($("#txtsumigst").val()) + parseFloat($("#txtsumugst").val());
                        $("#txtsumnetamtshow").val(netamt);
                    }


                $("#ddltransporter").trigger("chosen:updated");
                $("#STKINV").prop("disabled", true);
                $("#STKTRN").prop("disabled", true);
                $("#DPTRCVD").prop("disabled", true);
                $("#STKDES").prop("disabled", true);
                $("#EXPORT").prop("disabled", true);
                if (TDSAPPLICABLE == 'N') {
                    Edittdsid(TransporterbillID);
                }
            });
            BindAllInvoicenoNEWV2();

        }
    })

    EditTransporterbilldtl();


    //$('.debitdel').on('click', function () {
    //var voucherid;
    //var depoid;
    //vouchertypeid = $('#hdnvoucherid').val();









}
function Edittdsid(transporterid) {

    $.ajax({

        type: "POST",
        //  url: "frmaccvouchernew2.aspx/addvouchercr1",
        url: "/Transporter/Edittdsid",
        data: { transporterid: transporterid },
        async: false,
        //data: "{'AccountID': '" + AccountID + "','AccountName': '" + AccountName + "'}",
        //data: "{'ledgerid': '" + ledgerid + "','CostCatagoryID': '" + CostCatagoryID + "','BrandId': '" + BrandId + "','ProductId': '" + ProductId + "','DepartmentId': '" + DepartmentId + "'}",

        dataType: "json",
        success: function (response) {


            var iscostcenter;
            $.each(response, function () {
                $("#txtTDSpercentage1").val(this["response"]);

            })











            //--- in advance credit
            //$("#txtcreditchequeno").val('');
            //$("#txtcreditchequedate").val('');
            //$("#<%= rdbcreditpaymenttype.ClientID %>").val('2');
            //$("#rdbdebitpaymenttype").trigger("chosen:updated");
        }
    });
}
function EditTransporterbilldtl() {

    $.ajax({

        type: "POST",
        //  url: "frmaccvouchernew2.aspx/addvouchercr1",
        url: "/Transporter/EditTransporterbilldtl",
        data: '{}',
        async: false,
        //data: "{'AccountID': '" + AccountID + "','AccountName': '" + AccountName + "'}",
        //data: "{'ledgerid': '" + ledgerid + "','CostCatagoryID': '" + CostCatagoryID + "','BrandId': '" + BrandId + "','ProductId': '" + ProductId + "','DepartmentId': '" + DepartmentId + "'}",

        dataType: "json",
        success: function (response) {
            var tableNew = '<table id="grddetail" class="table table-striped table-bordered dt-responsive nowrap">';
            tableNew = tableNew + '<thead ><tr><th style="display:none"></th><th >Sl.No.</th><th BILL DATE"></th><th>TRANSPORTER NAME</th><th>LR/GR No</th><th> TYPE NO</th><th>BILL NO"</th> <th> BILL AMOUNT</th><th>CGST %</th><th>CGST VALUE</th><th>SGST %.</th><th>SGST VALUE </th> <th>IGST % </th><th>IGST VALUE  </th><th> GROSS WEIGHT </th><th> (BASIC + GST) AMOUNT</th<th>DELETE</th></tr></thead><tbody>';
            var totalcredit = 0;
            var counter = 0;

            var iscostcenter;
            $.each(response, function () {
                counter = counter + 1;
                tableNew = tableNew + "<tr><td style='display:none'>" + this["GUID"] + "</td><td>" + counter + "</td><td>" + this["BILLDATE"] + "</td> <td >" + this["TRANSPORTERNAME"] + "</td> <td>" + this["LRGRNO"] + "</td>  <td>" + this["INVNO"] + "</td><td>" + this["BILLNO"] + "</td>   <td>" + this["BILLAMOUNT"] + "</td>   <td>" + this["CGSTPERCENTAGE"] + "</td>   <td>" + this["CGSTAX"] + "</td><td>" + this["SGSTPERCENTAGE"] + "</td><td>" + this["SGSTAX"] + "</td><td>" + this["IGSTPERCENTAGE"] + "</td><td>" + this["IGSTAX"] + "</td><td>" + this["GROSSWEIGHT"] + "</td><td>" + this["NETAMOUNT"] + "</td> <td><input type='image' class='grvdtldel' id='btnDtldel' src='../Images/ico_delete_16.png' title='Delete' /></td>";
                //$("#txtsumnetamt").val(this["_sumnetamount"]);
                //$("#txtsumgrossweight").val(this["_sumgrossweight"]);
                //$("#txtsumtds").val(this["_sumtds1"]);
                //$("#txtsumcgst").val(this["_sumcgst"]);
                //$("#txtsumsgst").val(this["_sumsgst"]);
                //$("#txtsumigst").val(this["_sumigst"]);
                //$("#txtsumugst").val(this["_sumugst"]);
                //$("#txtsumbillvalue").val(this["_sumbillamount"]);
                //$("#txtsumtdsdeuctablevalue").val(this["_sumtdsdeductableamount"]);



            })
            document.getElementById("productDetailsGrid").innerHTML = tableNew + '</tbody></table>';
            // var tdsvalue = $("#txtsumtdsdeuctablevalue").val();
            //$("#Txtbillno").attr("disabled", "disabled");












            //--- in advance credit
            //$("#txtcreditchequeno").val('');
            //$("#txtcreditchequedate").val('');
            //$("#<%= rdbcreditpaymenttype.ClientID %>").val('2');
            //$("#rdbdebitpaymenttype").trigger("chosen:updated");
        }
    });
}
function addnew() {
    ReleaseSession();
    clearall();
    $("#dialog").dialog({
        autoOpen: true,
        modal: true,
        title: "Loading.."
    });
    $("#imgLoader").css("visibility", "visible");
    $('#pnlAdd').css("display", "block");
    $('#pnlDisplay').css("display", "none");
    $('#tr_billno').css("display", "none");
    $("#ddldepot").prop("disabled", true);
    $("#ddltransporter").empty().append('<option selected="selected" value="0">Select</option>');
    $("#ddlinvoice").empty().append('<option selected="selected" value="0">Select</option>');

    $("#hdntransporterid").val('');

    BindState();
    $("#ddldepot").chosen({
        search_contains: true
    });



    $("#ddldepot").trigger("chosen:updated");
    $("#ddltransporter").prop("disabled", false);

    $("#ddlfromstate").prop("disabled", false);


    $("#ddlfromstate").chosen({
        search_contains: true
    });



    $("#ddlfromstate").trigger("chosen:updated");
    $("#ddltostate").chosen({
        search_contains: true
    });



    $("#ddltostate").trigger("chosen:updated");

    $("#chkReversecharge").removeAttr("disabled");
    $("#Txtbillno").removeAttr("disabled");


    BindTransporter();



    $("#ddlinvoice").chosen({
        search_contains: true
    });
    $("#ddlinvoice").trigger("chosen:updated");

    $("#txtbillDate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
    $("#txtbillDate2").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
    $("#txtlrdate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
    $("#grddetail").remove();
    $("#chktdsapplicable1").prop('checked', false);
    $("#chkReversecharge").prop('checked', false);
    $("#chktransferHO").prop('checked', false);
    $("#imgLoader").css("visibility", "hidden");
    $("#dialog").dialog("close");
}
function close() {
    $('#pnlAdd').css("display", "none");

    $('#pnlDisplay').css("display", "block");
    $('#tr_billno').css("display", "");
    BindTransporterbillall();
    clearall();
}
function BindDepot() {
    var ddldepot = $("#ddldepot");
    var ddlregion = $("#ddlregion");

    $("#dialog").dialog({
        autoOpen: true,
        modal: true,
        title: "Loading.."
    });
    $("#imgLoader").css("visibility", "visible");
    $.ajax({
        type: "POST",
        url: "/Transporter/BindDepot",
        data: { userid: UserID },
        async: false,
        dataType: "json",

        success: function (response) {
            //alert(JSON.stringify(response));
            ddldepot.empty();
            ddlregion.empty();
            //.append('<option selected="selected" value="0">Select Brancht</option>');;
            $.each(response, function () {
                ddldepot.append($("<option></option>").val(this['BRID']).html(this['BRNAME']));
                ddlregion.append($("<option></option>").val(this['BRID']).html(this['BRNAME']));

            });
            var depoid = $('#ddlregion').val();
            // $("#hdndepoid").val(depoid);



        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
    $("#imgLoader").css("visibility", "hidden");
    $("#dialog").dialog("close");

}
function BindState() {
    var ddlfromstate = $("#ddlfromstate");
    var ddltostate = $("#ddltostate");
    $.ajax({
        type: "POST",
        url: "/Transporter/BindState",
        data: '{}',
        async: false,
        dataType: "json",

        success: function (response) {
            //alert(JSON.stringify(response));
            ddlfromstate.empty().append('<option selected="selected" value="0">Select State</option>');
            ddltostate.empty().append('<option selected="selected" value="0">Select State</option>');
            //.append('<option selected="selected" value="0">Select Brancht</option>');;
            $.each(response, function () {
                ddlfromstate.append($("<option></option>").val(this['State_ID']).html(this['State_Name']));
                ddltostate.append($("<option></option>").val(this['State_ID']).html(this['State_Name']));
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
function BindTransporter() {
    var depoid = $("#ddldepot").val();
    var ddltransporter = $("#ddltransporter");
    $.ajax({
        type: "POST",
        url: "/Transporter/BindTransporterv2",
        data: { depoid: depoid },
        async: false,
        dataType: "json",
        success: function (response) {
            //alert(JSON.stringify(response));
            ddltransporter.empty().append('<option selected="selected" value="0">Select </option>');
            $.each(response, function () {
                ddltransporter.append($("<option></option>").val(this['ID']).html(this['NAME']));
            });
            $("#ddltransporter").chosen({
                search_contains: true
            });


            $("#ddltransporter").trigger("chosen:updated");
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function BindTransporterbyInvoice() {
    var tag = '';
    var STKINV = $("input[name='STKINV']:checked").val();
    var STKTRN = $("input[name='STKTRN']:checked").val();
    var DPTRCVD = $("input[name='DPTRCVD']:checked").val();
    var STKDES = $("input[name='STKDES']:checked").val();
    var EXPORT = $("input[name='EXPORT']:checked").val();
    if (STKINV) {
        tag = STKINV;
    }
    if (STKTRN) {
        tag = STKTRN;
    }
    if (DPTRCVD) {
        tag = DPTRCVD;
    }
    if (STKDES) {
        tag = STKDES;
    }
    if (EXPORT) {
        tag = EXPORT;
    }

    var invoiceid = $("#ddlinvoice").val();
    $("#dialog").dialog({
        autoOpen: true,
        modal: true,
        title: "Loading.."
    });
    $("#imgLoader").css("visibility", "visible");

    setTimeout(function () {

    $.ajax({
        type: "POST",
        url: "/Transporter/BindTransporterbyInvoicev2",
        data: { tag: tag, invoiceid: invoiceid, },
        async: false,
        dataType: "json",
        success: function (response) {
            var transporter;
            var lrno;
            var lrdate;
            //alert(JSON.stringify(response));

            $.each(response, function () {
                transporter = this["TRANSPORTERID"];
                lrno = this["LRGRNO"];
                lrdate = this["LRGRDATE"];
            });
            var transporteridold = $("#hdntransporterid").val();

            if (transporteridold == '') {
                $("#ddltransporter").val(transporter);
            }
            else {
                $("#ddltransporter").val(transporteridold);
            }
            $("#txtlrdate").val(lrdate);
            $("#txtlrno").val(lrno);
            GetTdscheck();
            GetReverseCharge();
           
            $("#ddltransporter").chosen({
                search_contains: true
            });


            $("#ddltransporter").trigger("chosen:updated");
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
        });
        $("#imgLoader").css("visibility", "hidden");
        $("#dialog").dialog("close");


    }, 5);
}

function BindAllInvoicenoNEWV2() {
    var depoid = $("#ddldepot").val();
    var transporterbillid = $("#hdntrnsID").val();
    var tag = '';
    var STKINV = $("input[name='STKINV']:checked").val();
    var STKTRN = $("input[name='STKTRN']:checked").val();
    var DPTRCVD = $("input[name='DPTRCVD']:checked").val();
    var STKDES = $("input[name='STKDES']:checked").val();
    var EXPORT = $("input[name='EXPORT']:checked").val();
    if (STKINV) {
        tag = STKINV;
    }
    if (STKTRN) {
        tag = STKTRN;
    }
    if (DPTRCVD) {
        tag = DPTRCVD;
    }
    if (STKDES) {
        tag = STKDES;
    }
    if (EXPORT) {
        tag = EXPORT;
    }

    var ddlinvoice = $("#ddlinvoice");
    $("#dialog").dialog({
        autoOpen: true,
        modal: true,
        title: "Loading.."
    });
    $("#imgLoader").css("visibility", "visible");

    setTimeout(function () {

    $.ajax({
        type: "POST",
        url: "/Transporter/BindAllInvoicenoNEWV2depot",
        data: { tag: tag, depoid: depoid, transporterbillid: transporterbillid, finyr: FINYEAR },
        async: false,
        dataType: "json",
        success: function (response) {
            //alert(JSON.stringify(response));
            ddlinvoice.empty().append('<option selected="selected" value="0">Select Invoice</option>');
            $.each(response, function () {
                ddlinvoice.append($("<option></option>").val(this['INVID']).html(this['INVNO']));
            });
            $("#ddlinvoice").chosen({
                search_contains: true
            });


            $("#ddlinvoice").trigger("chosen:updated");
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
        });
        $("#imgLoader").css("visibility", "hidden");
        $("#dialog").dialog("close");


    }, 5);
}
function GetTdscheck() {


    var transporterid = $("#ddltransporter").val();
    $("#dialog").dialog({
        autoOpen: true,
        modal: true,
        title: "Loading.."
    });
    $("#imgLoader").css("visibility", "visible");

    setTimeout(function () {
    $.ajax({
        type: "POST",
        url: "/Transporter/GetTdscheck",
        data: { transporterid: transporterid },
        async: false,
        dataType: "json",
        success: function (response) {
            var tag;

            //alert(JSON.stringify(response));

            $.each(response, function () {
                tag = this["response"];

            });
            if (tag == 'Y') {
                $("#chktdsapplicable1").prop('checked', true);
            }
            else
                if (tag == 'N') {
                    $("#chktdsapplicable1").prop('checked', false);
                }
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
        });
        $("#imgLoader").css("visibility", "hidden");
        $("#dialog").dialog("close");


    }, 5);
}
function GetReverseCharge() {


    var transporterid = $("#ddltransporter").val();
    var transportername = $('#ddltransporter').find('option:selected').text();
    $("#dialog").dialog({
        autoOpen: true,
        modal: true,
        title: "Loading.."
    });
    $("#imgLoader").css("visibility", "visible");

    setTimeout(function () {

    $.ajax({
        type: "POST",
        url: "/Transporter/GetReverseCharge",
        data: { transporterid: transporterid },
        async: false,
        dataType: "json",
        success: function (response) {
            var tag;

            //alert(JSON.stringify(response));

            $.each(response, function () {
                tag = this["response"];

            });
            if (tag == 'Y') {
                $("#chkReversecharge").prop('checked', true);
                $("#gst1").html("CGST AMT (RCM)");
                $("#sgst1").html("SCGST AMT (RCM)");
                $("#igst1").html("IGST AMT (RCM)");
                $("#ugst1").html("UGST AMT (RCM)");
            }
            else
                if (tag == 'N') {
                    $("#chkReversecharge").prop('checked', false);
                    $("#gst1").html("CGST AMT");
                    $("#sgst1").html("SCGST AMT");
                    $("#igst1").html("IGST AMT");
                    $("#ugst1").html("UGST AMT");
                }


        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
        });
        $("#imgLoader").css("visibility", "hidden");
        $("#dialog").dialog("close");


    }, 5);
}

function Isrcmcheck() {


    var transporterid = $("#ddltransporter").val();
    var transportername = $('#ddltransporter').find('option:selected').text();
    $.ajax({
        type: "POST",
        url: "/Transporter/GetReverseCharge",
        data: { transporterid: transporterid },
        async: false,
        dataType: "json",
        success: function (response) {
            var tag;

            //alert(JSON.stringify(response));

            $.each(response, function () {
                tag = this["response"];

            });
            //if (tag == 'Y') {
            //    $("#chkReversecharge").prop('checked', true);
            //}
            //else
            //    if (tag == 'N') {
            //        $("#chkReversecharge").prop('checked', false);
            //    }

            var ischecked = $('#chkReversecharge').is(":checked");
            if (ischecked == false && tag == 'Y') {

                toastr.warning('Reverse Charge  for the transporter :' + transportername + 'is applicable in the master form');
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


function GetIsTransferToHo() {


    var transporterid = $("#ddltransporter").val();
    $.ajax({
        type: "POST",
        url: "/Transporter/GetIsTransferToHo",
        data: { transporterid: transporterid },
        async: false,
        dataType: "json",
        success: function (response) {
            var tag;

            //alert(JSON.stringify(response));

            $.each(response, function () {
                tag = this["response"];

            });
            if (tag == 'Y') {
                //$("#chktransferHO").prop('checked', true);
            }
            else
                if (tag == 'N') {
                    // $("#chktransferHO").prop('checked', true);
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

function BindGstPercentage() {
    var tag = '';
    var STKINV = $("input[name='STKINV']:checked").val();
    var STKTRN = $("input[name='STKTRN']:checked").val();
    var DPTRCVD = $("input[name='DPTRCVD']:checked").val();
    var STKDES = $("input[name='STKDES']:checked").val();
    var EXPORT = $("input[name='EXPORT']:checked").val();
    if (STKINV) {
        tag = STKINV;
    }
    if (STKTRN) {
        tag = STKTRN;
    }
    if (DPTRCVD) {
        tag = DPTRCVD;
    }
    if (STKDES) {
        tag = STKDES;
    }
    if (EXPORT) {
        tag = EXPORT;
    }
    $("#txtcgstvalue").val('0.00');
    $("#txtsgstvalue").val('0.00');
    $("#txtigstvalue").val('0.00');
    $("#txtugstvalue").val('0.00');
    $("#txtugstpercentage").val('0')
    $("#txtBillvalue").val('');

    $("#txttdsdeductablevalue").val('');
    var tdsdeductablevalue = $("#txttdsdeductablevalue").val();
    var depoid = $("#ddldepot").val();
    var tranporterid = $("#ddltransporter").val();
    var bvalue;
    if (tdsdeductablevalue != '') {
        bvalue = tdsdeductablevalue
    }
    else {
        bvalue = '0';

    }
    var InvoiceId = $("#ddlinvoice").val();
    var BillingType = tag;
    var Fromstate = $("#ddlfromstate").val();
    var Tostate = $("#ddltostate").val();
    $("#dialog").dialog({
        autoOpen: true,
        modal: true,
        title: "Loading.."
    });
    $("#imgLoader").css("visibility", "visible");

    setTimeout(function () {
    $.ajax({
        type: "POST",
        url: "/Transporter/BindGstPercentage",
        data: {
            depoid: depoid, tranporterid: tranporterid, bvalue: bvalue, InvoiceId: InvoiceId, BillingType: BillingType, Fromstate: Fromstate, Tostate: Tostate, finyr: FINYEAR
        },
        async: false,
        dataType: "json",
        success: function (response) {
            var cgstid;
            var sgstid;
            var sgstid;
            var cgstpercentage;
            var sgstpercentage;
            var igstpercentage;
            //alert(JSON.stringify(response));

            $.each(response, function () {
                cgstid = this["CGSTID"];
                sgstid = this["SGSTID"];
                sgstid = this["IGSTID"];
                $("#txtcgstpercentage").val(this["CGST_PERCENTAGE"])
                cgstpercentage = this["CGST_PERCENTAGE"];
                $("#txtsgstpercentage").val(this["SGST_PERCENTAGE"]);
                $("#txtigstpercentage").val(this["IGST_PERCENTAGE"]);


            });



            $("#hdncgsttaxpercentage").val(cgstid);
            $("#hdnsgsttaxpercentage").val(sgstid);
            $("#hdnigsttaxpercentage").val(IGSTID);




        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
        });
        $("#imgLoader").css("visibility", "hidden");
        $("#dialog").dialog("close");


    }, 5);
}
function loadtaxes() {
    $("#hdncgsttaxpercentage").val('0');
    $("#hdnsgsttaxpercentage").val('0');
    $("#hdnigsttaxpercentage").val('0');

    $("#hdnugsttaxpercentage").val('0');
    $("#txtcgstpercentage").val('0.00');
    $("#txtsgstpercentage").val('0.00');
    $("#txtigstpercentage").val('0.00');
    $("#txtcgstvalue").val('0.00');
    $("#txtsgstvalue").val('0')
    $("#txtigstvalue").val('0')
    var counter = 0;
    var fromstate = $("#ddlfromstate").val();
    var depoid = $("#ddldepot").val();
    var tranporterid = $("#ddltransporter").val();
    var billamt = $("#txtBillvalue").val();
    var billvalue = 0;
    var cgstvalue = 0;
    var sgstvalue = 0;
    var igstvalue = 0;
    var totalamt = 0;
    var cgstpercentage;
    var sgstpercentage;
    var igstpercentage;
    var InvoiceId = $("#ddlinvoice").val();
    var BillingType = tag;
    var Fromstate = $("#ddlfromstate").val();
    var Tostate = $("#ddltostate").val();
    var bvalue = $("#txttdsdeductablevalue").val();
    var tag = '';
    var STKINV = $("input[name='STKINV']:checked").val();
    var STKTRN = $("input[name='STKTRN']:checked").val();
    var DPTRCVD = $("input[name='DPTRCVD']:checked").val();
    var STKDES = $("input[name='STKDES']:checked").val();
    var EXPORT = $("input[name='EXPORT']:checked").val();
    if (STKINV) {
        tag = STKINV;
    }
    if (STKTRN) {
        tag = STKTRN;
    }
    if (DPTRCVD) {
        tag = DPTRCVD;
    }
    if (STKDES) {
        tag = STKDES;
    }
    if (EXPORT) {
        tag = EXPORT;
    }
    var RCM;
    if (tranporterid == '0') {
        toastr.warning('Please Select Transporter...!');
        return false;
    }
    else
        if (fromstate == '0') {
            toastr.warning('Please Select Billing From...!');
            return false;
        }
        else
            if (billamt == '0') {
                toastr.warning('Please enter bill value...!');
                return false;
            }
            else
                if (billamt == '') {
                    toastr.warning('Please enter bill value...!');
                    return false;
                }
    var ischecked = $('#chkReversecharge').is(":checked")
    billvalue = parseFloat(billamt);
    if (billvalue > 750 || ischecked == 'false') {
        RCM = 'N';
    }
    else
        if (ischecked == 'true') {
            RCM = 'Y';
        }
    $("#dialog").dialog({
        autoOpen: true,
        modal: true,
        title: "Loading.."
    });
    $("#imgLoader").css("visibility", "visible");

    setTimeout(function () {
    $.ajax({
        type: "POST",
        url: "/Transporter/BindGstPercentage_New",
        data: {
            depoid: depoid, tranporterid: tranporterid, bvalue: bvalue, InvoiceId: InvoiceId, BillingType: tag, Fromstate: Fromstate, Tostate: Tostate, RCM: RCM
        },
        async: false,
        dataType: "json",
        success: function (response) {
            var cgstid;
            var sgstid;
            var igstid;

            //alert(JSON.stringify(response));

            $.each(response, function () {
                counter = counter + 1;
                cgstid = this["CGSTID"];
                sgstid = this["SGSTID"];
                igstid = this["IGSTID"];
                $("#txtcgstpercentage").val(this["CGST_PERCENTAGE"])
                cgstpercentage = this["CGST_PERCENTAGE"];
                $("#txtsgstpercentage").val(this["SGST_PERCENTAGE"]);
                $("#txtigstpercentage").val(this["IGST_PERCENTAGE"]);
                if (this["CGST_PERCENTAGE"] != '0' || this["CGST_PERCENTAGE"] != '') {
                    cgstvalue = (parseFloat(bvalue) * parseFloat($("#txtcgstpercentage").val())) / 100;
                    $("#txtcgstvalue").val(cgstvalue);


                }
                if (this["SGST_PERCENTAGE"] != '0' || this["SGST_PERCENTAGE"] != '') {
                    sgstvalue = (parseFloat(bvalue) * parseFloat($("#txtsgstpercentage").val())) / 100;
                    $("#txtsgstvalue").val(sgstvalue.toFixed(2))
                }
                if (this["IGST_PERCENTAGE"] != '0' || this["IGST_PERCENTAGE"] != '') {
                    igstvalue = (parseFloat(bvalue) * parseFloat($("#txtigstpercentage").val())) / 100;
                    $("#txtigstvalue").val(igstvalue.toFixed(2))
                }


            });



            $("#hdncgsttaxpercentage").val(cgstid);
            $("#hdnsgsttaxpercentage").val(sgstid);
            $("#hdnigsttaxpercentage").val(igstid);


            totalamt = billvalue + cgstvalue + sgstvalue + igstvalue;
            $("#Txtamount").val(totalamt.toFixed(2));


        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
        });
        $("#imgLoader").css("visibility", "hidden");
        $("#dialog").dialog("close");


    }, 5);
    if (counter == 0) {
        $("#txtcgstpercentage").val('0.00');
        $("#txtsgstpercentage").val('0.00');
        $("#txtigstpercentage").val('0.00');
        $("#txtcgstvalue").val('0.00');
        $("#txtsgstvalue").val('0')
        $("#txtigstvalue").val('0')

    }
}
function calculatetds() {
    var ischecked = $('#chktdsapplicable1').is(":checked");
    var tdsvalue = 0;
    var TRANSPORTERID = $("#ddltransporter").val();
    var tdsvalue1 = $("#txtsumtdsdeuctablevalue").val();
  
    var netamt = 0;
    if (ischecked == true) {

      
        TDSPercentage(TRANSPORTERID, tdsvalue1);


    }
    else {
        netamt = parseFloat($("#txtsumnetamt").val()) - parseFloat($("#txtsumtds").val());
        alert(netamt);
        $("#txtsumnetamt").val(netamt);
        $("#txtsumnetamtshow").val(netamt);
        $("#txtTDSpercentage1").val('0');
        $("#txtsumtds").val('0.00');
        toastr.info('TDS is applicable for Local Transporter in the master from...!');
       // $("#txtsumnetamt").val('0.00');


    }
}

function clearall() {
    $("#txtcgstvalue").val('0.00');
    $("#txtsgstvalue").val('0.00');
    $("#txtigstvalue").val('0.00');
    $("#txtugstvalue").val('0.00');
    $("#txtugstpercentage").val('0')
    $("#txtsumcgst").val('0.00');
    $("#txtsumgrossweight").val('0.00');
    $("#txtsumsgst").val('0.00');
    $("#txtsumbillvalue").val('0.00');
    $("#txtsumigst").val('0.00');
    $("#txtsumtdsdeuctablevalue").val('0.00');
    $("#txtsumugst").val('0.00');
    $("#txtsumtds").val('0.00');
    $("#txtTDSpercentage1").val('0.00');
    $("#txtsumnetamt").val('0.00');
    $("#txtsumnetamtshow").val('0.00');
    
    $("#TxtTds").val('0.00');
    $("#txttdspercentage").val('0.00');
    $("#hdn_tdsis").val('0');
    ReleaseSession();
    $("#txtremarks").val('');
    $("#txtlrno").val('');


    $("#txttdsdeductablevalue").val(this['']);

    $("#txtremarks").val('');
    $("#TxtTds").val('0.00');
    $("#txtcgstpercentage").val('0');
    $("#txtcgstvalue").val('0.00');
    $("#txtsgstpercentage").val('0');
    $("#txtsgstvalue").val('0.00');
    $("#txtigstpercentage").val('0');
    $("#txtigstvalue").val('0.00');
    $("#txtgrossweight").val('');
    $("#hdnsgsttaxpercentage").val('');
    $("#hdnigsttaxpercentage").val('');
    $("#hdncgsttaxpercentage").val('');
    $("#STKINV").prop("disabled", false);
    $("#STKTRN").prop("disabled", false);
    $("#DPTRCVD").prop("disabled", false);
    $("#STKDES").prop("disabled", false);
    $("#EXPORT").prop("disabled", false);

    $("#STKINV").prop("checked", false);
    $("#STKTRN").prop("checked", false);
    $("#STKDES").prop("checked", false);
    $("#DPTRCVD").prop("checked", false);
    $("#ddltransporter").prop("disabled", false);
    //$("#ddltransporter").chosen({
    //    search_contains: true
    //});



    //$("#ddltransporter").trigger("chosen:updated");

    //$("#ddlfromstate").prop("disabled", false);
    //$("#ddlfromstate").chosen({
    //    search_contains: true
    //});
    //$("#ddlfromstate").trigger("chosen:updated");

    $("#gst1").html("CGST AMT");
    $("#sgst1").html("SCGST AMT");
    $("#igst1").html("IGST AMT");
    $("#ugst1").html("UGST AMT");
}

function GetIsTransferToHo() {


    var transporterid = $("#ddltransporter").val();
    $.ajax({
        type: "POST",
        url: "/Transporter/GetIsTransferToHo",
        data: { transporterid: transporterid },
        async: false,
        dataType: "json",
        success: function (response) {
            var tag;

            //alert(JSON.stringify(response));

            $.each(response, function () {
                tag = this["response"];

            });
            if (tag == 'Y') {
                //$("#chktransferHO").prop('checked', true);
            }
            else
                if (tag == 'N') {
                    // $("#chktransferHO").prop('checked', true);
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
function addgrid() {
    var decimalvalue = 0;
    var parts = 0;
    var tag = '';
    var STKINV = $("input[name='STKINV']:checked").val();
    var STKTRN = $("input[name='STKTRN']:checked").val();
    var DPTRCVD = $("input[name='DPTRCVD']:checked").val();
    var STKDES = $("input[name='STKDES']:checked").val();
    var EXPORT = $("input[name='EXPORT']:checked").val();
    if (STKINV) {
        tag = STKINV;
    }
    if (STKTRN) {
        tag = STKTRN;
    }
    if (DPTRCVD) {
        tag = DPTRCVD;
    }
    if (STKDES) {
        tag = STKDES;
    }
    if (EXPORT) {
        tag = EXPORT;
    }
    var Fromstate = $("#ddlfromstate").val();
    var tranporterid = $("#ddltransporter").val();
    var INVID = $("#ddlinvoice").val();
    var INVNO = $('#ddlinvoice').find('option:selected').text();
    var LRGRNO = $("#txtlrno").val();
    var LRGRDATE = $("#txtlrdate").val();
    var GROSSWEIGHT = $("#txtgrossweight").val();
    var BILLNO = $("#Txtbillno").val();
    var BILLAMOUNT = $("#txtBillvalue").val();
    var TDSPERCENTRAGE = $("#txttdspercentage").val();
    var TDSPERCENTRAGE = $("#txttdspercentage").val();
    var TDS = $("#TxtTds").val();
  
    if (TDS == '') {
        TDS = '0.00';
    }
    var CGSTID = $("#hdncgsttaxpercentage").val();
    var CGSTPERCENTAGE = $("#txtcgstpercentage").val();
    var CGSTAX = $("#txtcgstvalue").val();
    var SGSTID = $("#hdnsgsttaxpercentage").val();
    var SGSTPERCENTAGE = $("#txtsgstpercentage").val();
    var SGSTAX = $("#txtsgstvalue").val();
    var IGSTID = $("#hdnigsttaxpercentage").val();
    var IGSTPERCENTAGE = $("#txtigstpercentage").val();
    var IGSTAX = $("#txtigstvalue").val();
    var UGSTID = $("#hdnugsttaxpercentage").val();
    var UGSTPERCENTAGE = $("#txtugstpercentage").val();
    var UGSTAX = $("#txtugstvalue").val();
    var NETAMOUNT = $("#Txtamount").val();
    var TDSID = $("#hdn_tdsis").val();
    var BILLDATE = $("#txtbilldate2").val();
    var TDSDEDUCTABLEAMOUNT = $("#txttdsdeductablevalue").val();
    var TRANSPORTERID = $("#ddltransporter").val();
    var isexists = 'n';
    var transporterbillid = $("#hdntrnsID").val();
    var flag = '0';
    flag = chklrgrno(tag, TRANSPORTERID, LRGRNO, transporterbillid, INVID);
    if ($('#grddetail').length) {
        $("#grddetail tbody tr").each(function () {
            var invid = $(this).find('td:eq(5)').html();
            // var batchidgrd = $(this).find('td:eq(3)').html();

            if (INVNO == invid) {
                isexists = 'y';
                return false;
            }
        })
    }
    var TRANSPORTERNAME = $('#ddltransporter').find('option:selected').text();
    if (Fromstate == '0') {
        toastr.warning('Please Select Billing From...!');
        return false;
    }
    else
        if (LRGRNO == '') {
            toastr.warning('LrGr no required...!');
            return false;
        }
        else
            if (BILLNO == '') {
                toastr.warning('Bill  no required...!');
                return false;
            }
            else
                if (BILLAMOUNT == '' || BILLAMOUNT == '0') {
                    toastr.warning('Bill  Amt required...!');
                    return false;
                }
                else
                    if (GROSSWEIGHT == '') {
                        toastr.warning('Please provide gross weight...!');
                        return false;
                    }
    if (isexists == 'y' && tag == 'STKINV') {
        toastr.warning('Same Invoice No. already exists...!');
        return false;
    }
    if (isexists == 'y' && tag == 'STKTRN') {
        toastr.warning('Same Transfer No. already exists...!');
        return false;
    }
    if (isexists == 'y' && tag == 'DPTRCVD') {
        toastr.warning('Same Depot Received No. already exists...!');
        return false;
    }
    if (isexists == 'y' && tag == 'STKDES') {
        toastr.warning('Same Stock Received No. already exists...!');
        return false;
    }
    if (isexists == 'y' && tag == 'EXPORT') {
        toastr.warning('Same Stock Received No. already exists...!');
        return false;
    }
    if (flag == '1') {
        toastr.warning('LRGR No. already exists...!');
        return false;
    }
    $("#dialog").dialog({
        autoOpen: true,
        modal: true,
        title: "Loading.."
    });
    $("#imgLoader").css("visibility", "visible");

    setTimeout(function () {


    $.ajax({

        type: "POST",
        //  url: "frmaccvouchernew2.aspx/addvouchercr1",
        url: "/Transporter/addgrid",
        data: {
            INVID: INVID, INVNO: INVNO, LRGRNO: LRGRNO, LRGRDATE: LRGRDATE, GROSSWEIGHT: GROSSWEIGHT, BILLNO: BILLNO, BILLAMOUNT: BILLAMOUNT, TDSPERCENTRAGE: TDSPERCENTRAGE, TDS: TDS, CGSTID: CGSTID, CGSTPERCENTAGE: CGSTPERCENTAGE, CGSTAX: CGSTAX, SGSTID: SGSTID, SGSTPERCENTAGE: SGSTPERCENTAGE, SGSTAX: SGSTAX, IGSTID: IGSTID, IGSTPERCENTAGE: IGSTPERCENTAGE, IGSTAX: IGSTAX, UGSTID: UGSTID, UGSTPERCENTAGE: UGSTPERCENTAGE, UGSTAX: UGSTAX, NETAMOUNT: NETAMOUNT, TDSID: TDSID, BILLDATE: BILLDATE, TDSDEDUCTABLEAMOUNT: TDSDEDUCTABLEAMOUNT, TRANSPORTERID: TRANSPORTERID, TRANSPORTERNAME: TRANSPORTERNAME
        },

        //data: "{'AccountID': '" + AccountID + "','AccountName': '" + AccountName + "'}",
        //data: "{'ledgerid': '" + ledgerid + "','CostCatagoryID': '" + CostCatagoryID + "','BrandId': '" + BrandId + "','ProductId': '" + ProductId + "','DepartmentId': '" + DepartmentId + "'}",

        dataType: "json",
        success: function (response) {
            var tableNew = '<table id="grddetail" class="table table-striped table-bordered dt-responsive nowrap">';
            tableNew = tableNew + '<thead ><tr><th style="display:none"></th><th >Sl.No.</th><th BILL DATE"></th><th>TRANSPORTER NAME</th><th>LR/GR No</th><th> TYPE NO</th><th>BILL NO"</th> <th> BILL AMOUNT</th><th>CGST %</th><th>CGST VALUE</th><th>SGST %.</th><th>SGST VALUE </th> <th>IGST % </th><th>IGST VALUE  </th><th> GROSS WEIGHT </th><th> (BASIC + GST) AMOUNT</th<th>DELETE</th></tr></thead><tbody>';
            var totalcredit = 0;
            var counter = 0;

            var iscostcenter;
            $.each(response, function () {
                counter = counter + 1;
                tableNew = tableNew + "<tr><td style='display:none'>" + this["GUID"] + "</td><td>" + counter + "</td><td>" + this["BILLDATE"] + "</td> <td >" + this["TRANSPORTERNAME"] + "</td> <td>" + this["LRGRNO"] + "</td>  <td>" + this["INVNO"] + "</td><td>" + this["BILLNO"] + "</td>   <td>" + this["BILLAMOUNT"] + "</td>   <td>" + this["CGSTPERCENTAGE"] + "</td>   <td>" + this["CGSTAX"] + "</td><td>" + this["SGSTPERCENTAGE"] + "</td><td>" + this["SGSTAX"] + "</td><td>" + this["IGSTPERCENTAGE"] + "</td><td>" + this["IGSTAX"] + "</td><td>" + this["GROSSWEIGHT"] + "</td><td>" + this["NETAMOUNT"] + "</td> <td><input type='image' class='grvdtldel' id='btnDtldel' src='../Images/ico_delete_16.png' title='Delete' /></td>";
                $("#txtsumnetamt").val(this["_sumnetamount"]);
                $("#txtsumgrossweight").val(this["_sumgrossweight"]);
                $("#txtsumtds").val(this["_sumtds1"]);
                $("#txtsumcgst").val(this["_sumcgst"]);
                $("#txtsumsgst").val(this["_sumsgst"]);
                $("#txtsumigst").val(this["_sumigst"]);
                $("#txtsumugst").val(this["_sumugst"]);
                $("#txtsumbillvalue").val(this["_sumbillamount"]);
                $("#txtsumtdsdeuctablevalue").val(this["_sumtdsdeductableamount"]);
                  $("#txtsumtds").val(decimalvalue);

                
            })
            document.getElementById("productDetailsGrid").innerHTML = tableNew + '</tbody></table>';
            var tdsvalue = $("#txtsumtdsdeuctablevalue").val();
            $("#hdntransporterid").val(TRANSPORTERID);

            TDSPercentage(TRANSPORTERID, tdsvalue);

            $("#chkReversecharge").attr("disabled", "disabled");
            $("#Txtbillno").attr("disabled", "disabled");
            $("#ddltransporter").prop("disabled", true);
            $("#ddlfromstate").prop("disabled", true);

            $("#ddltransporter").chosen({
                search_contains: true
            });



            $("#ddltransporter").trigger("chosen:updated");
            $("#ddlfromstate").chosen({
                search_contains: true
            });



            $("#ddlfromstate").trigger("chosen:updated");
            rcmcalculation();
            clearadd();
            //--- in advance credit
            //$("#txtcreditchequeno").val('');
            //$("#txtcreditchequedate").val('');
            //$("#<%= rdbcreditpaymenttype.ClientID %>").val('2');
            //$("#rdbdebitpaymenttype").trigger("chosen:updated");
        }
        });
        $("#imgLoader").css("visibility", "hidden");
        $("#dialog").dialog("close");


    }, 5);
}

function rcmcalculation() {
    var netamt = 0;
    var ischecked = $('#chkReversecharge').is(":checked");
    if (ischecked == true) {
        netamt = parseFloat($("#txtsumbillvalue").val()) - ($("#txtsumtds").val())
        $("#txtsumnetamtshow").val(netamt);
    }
    else
        if (ischecked == false) {
            netamt = (parseFloat($("#txtsumbillvalue").val()) - ($("#txtsumtds").val())) + parseFloat($("#txtsumcgst").val()) + parseFloat($("#txtsumsgst").val()) + parseFloat($("#txtsumigst").val()) + parseFloat($("#txtsumugst").val());
            $("#txtsumnetamtshow").val(netamt);
        }

    
    }
function chklrgrno(TAG, transporterID, LRGRNO, TRANSPORETERBILLID, INVID) {
    var tag = 0;;
    $.ajax({
        type: "POST",
        url: "/Transporter/checkLRGRNOV2",
        data: { TAG: TAG, transporterID: transporterID, LRGRNO: LRGRNO, TRANSPORETERBILLID: TRANSPORETERBILLID, INVID: INVID, finyr: FINYEAR },
        async: false,
        dataType: "json",
        success: function (response) {


            //alert(JSON.stringify(response));

            $.each(response, function () {
                tag = this["response"];

            });

        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }

    });
    return tag;
}
function TDSPercentage(tranporterid, bvalue) {
    var tdspercent = 0;
    var netamt = 0;
    var tdsvalue = 0;
    var billdate = $("#txtbilldate2").val();
    var parts = 0;

    var decimalvalue = 0;
    var finaltds = 0;
    var finaltds2 = 0;
    var afterdec;
    var before;
    var _actual = 0;
   
    $.ajax({
        type: "POST",
        url: "/Transporter/TDSPercentage",
        data: { tranporterid: tranporterid, bvalue: bvalue, finyr: FINYEAR, billdate: billdate},
        async: false,
        dataType: "json",
        success: function (response) {


            //alert(JSON.stringify(response));

            $.each(response, function () {
                tdspercent = parseFloat(this["PERCENTAGE"]);
               
                $("#txtTDSpercentage1").val(this["PERCENTAGE"]);

            });
            if (tdspercent != 0) {
                $("#hdntdspercentage").val(tdspercent);
                $("#txttdspercentage").val(tdspercent);
                
                tdsvalue = (parseFloat(bvalue) * tdspercent) / 100;
                finaltds = tdsvalue.toFixed(2);
               
                 //logic for round up
                parts = finaltds - Math.floor(tdsvalue);
              
                decimalvalue = parts.toFixed(2);
                
                //decimalvalue = tdsvalue.toFixed(2);
                //alert(decimalvalue);
                //afterdec = decimalvalue.toString().split('.')[1];
                //before = decimalvalue.toString().split('.')[0];
                //parts = parseFloat(afterdec);
                //_actual = parseFloat(before);
                //change for round up
               
        //        if (decimalvalue >= .50) {
        //            decimalvalue = 1 - decimalvalue;

        //            decimalvalue = decimalvalue.toFixed(2);

                  
        //           // finaltds = finaltds + decimalvalue;
        //            finaltds2 = Math.round(finaltds);
                 
        //            //Math.round(finaltds);
        //            finaltds2 = finaltds2.toFixed(2);
                   
        //}
        //        else 
        //        {

        //           decimalvalue = -decimalvalue;
        //            finaltds2 = Math.round(finaltds);
           
        //    }
                if (decimalvalue > 0) {
                    decimalvalue = 1 - decimalvalue;
                    decimalvalue = decimalvalue.toFixed(2);
                    finaltds2 = (finaltds - parts) + 1;
                   

                }
                else {
                    finaltds2 = finaltds;
                }
              
              
                $("#txtsumtds").val(finaltds2);
                netamt = parseFloat($("#txtsumnetamt").val()) - finaltds2;
               
                $("#txtsumnetamt").val(netamt);
                $("#txtsumnetamtshow").val(netamt);

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

function TDSPercentage1(tranporterid, bvalue) {
    var tdspercent = 0;
    var netamt = 0;
    var tdsvalue = 0;
    var billdate = $("#txtbilldate2").val();
    var parts = 0;

    var decimalvalue = 0;
    var finaltds = 0;
    var finaltds2 = 0;
    var afterdec;
    var before;
    var _actual = 0;
    alert('a');
    $.ajax({
        type: "POST",
        url: "/Transporter/TDSPercentage",
        data: { tranporterid: tranporterid, bvalue: bvalue, finyr: FINYEAR, billdate: billdate },
        async: false,
        dataType: "json",
        success: function (response) {


            //alert(JSON.stringify(response));

            $.each(response, function () {
                tdspercent = parseFloat(this["PERCENTAGE"]);

                $("#txtTDSpercentage1").val(this["PERCENTAGE"]);

            });
            if (tdspercent != 0) {
                $("#hdntdspercentage").val(tdspercent);
                $("#txttdspercentage").val(tdspercent);

                tdsvalue = (parseFloat(bvalue) * tdspercent) / 100;
                finaltds = tdsvalue.toFixed(2);
              
                //logic for round up
                parts = tdsvalue - Math.floor(tdsvalue);

                decimalvalue = parts.toFixed(2);

                //decimalvalue = tdsvalue.toFixed(2);
                //alert(decimalvalue);
                //afterdec = decimalvalue.toString().split('.')[1];
                //before = decimalvalue.toString().split('.')[0];
                //parts = parseFloat(afterdec);
                //_actual = parseFloat(before);
                //change for round up
                
                //if (decimalvalue >= .50) {
                //    decimalvalue = 1 - decimalvalue;

                //    decimalvalue = decimalvalue.toFixed(2);


                //    // finaltds = finaltds + decimalvalue;
                //    finaltds2 = Math.round(finaltds);
                   
                //    //Math.round(finaltds);
                //    finaltds2 = finaltds2.toFixed(2);

                //}
                //else {

                //    decimalvalue = -decimalvalue;
                //    finaltds2 = Math.round(finaltds);

                //}

                if (decimalvalue > 0) {
                    decimalvalue = 1 - decimalvalue;
                    decimalvalue = decimalvalue.toFixed(2);
                    finaltds2 = (finaltds - parts) + 1;


                }
                else {
                    finaltds2 = finaltds;
                }

                $("#txtsumtds").val(finaltds2);
               // netamt = parseFloat($("#txtsumnetamt").val()) - finaltds2;
                netamt = bvalue- finaltds2;

                $("#txtsumnetamt").val(netamt);
                $("#txtsumnetamtshow").val(netamt);

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

function delgrid(guid) {
    var TRANSPORTERID = $("#ddltransporter").val();
    $.ajax({

        type: "POST",
        //  url: "frmaccvouchernew2.aspx/addvouchercr1",
        url: "/Transporter/delgrid",
        data: {
            GUID: guid
        },

        //data: "{'AccountID': '" + AccountID + "','AccountName': '" + AccountName + "'}",
        //data: "{'ledgerid': '" + ledgerid + "','CostCatagoryID': '" + CostCatagoryID + "','BrandId': '" + BrandId + "','ProductId': '" + ProductId + "','DepartmentId': '" + DepartmentId + "'}",

        dataType: "json",
        success: function (response) {
            var tableNew = '<table id="grddetail" class="table table-striped table-bordered dt-responsive nowrap">';
            tableNew = tableNew + ' <thead > <tr><th style="display:none"></th><th >Sl.No.</th><th BILL DATE"></th><th>TRANSPORTER NAME</th><th>LR/GR No</th><th> TYPE NO</th><th>BILL NO"</th> <th> BILL AMOUNT</th> <th>CGST %</th> <th>CGST VALUE</th> <th>SGST %.</th> <th>SGST VALUE </th> <th>IGST % </th> <th>IGST VALUE  </th> <th> GROSS WEIGHT </th> <th> (BASIC + GST) AMOUNT</th<th>DELETE</th></tr ></thead > <tbody>';
            var totalcredit = 0;
            var counter = 0;

            var iscostcenter;
            $.each(response, function () {
                counter = counter + 1;
                tableNew = tableNew + "<tr><td style='display:none'>" + this["GUID"] + "</td><td>" + counter + "</td><td>" + this["BILLDATE"] + "</td> <td >" + this["TRANSPORTERNAME"] + "</td> <td>" + this["LRGRNO"] + "</td>  <td>" + this["INVNO"] + "</td><td>" + this["BILLNO"] + "</td>   <td>" + this["BILLAMOUNT"] + "</td>   <td>" + this["CGSTPERCENTAGE"] + "</td>   <td>" + this["CGSTAX"] + "</td><td>" + this["SGSTPERCENTAGE"] + "</td><td>" + this["SGSTAX"] + "</td><td>" + this["IGSTPERCENTAGE"] + "</td><td>" + this["IGSTAX"] + "</td><td>" + this["GROSSWEIGHT"] + "</td><td>" + this["NETAMOUNT"] + "</td> <td><input type='image' class='grvdtldel' id='btnDtldel' src='../Images/ico_delete_16.png' title='Delete' /></td>";
                $("#txtsumnetamt").val(this["_sumnetamount"]);
                $("#txtsumgrossweight").val(this["_sumgrossweight"]);
                $("#txtsumtds").val(this["_sumtds1"]);
                $("#txtsumcgst").val(this["_sumcgst"]);
                $("#txtsumsgst").val(this["_sumsgst"]);
                $("#txtsumigst").val(this["_sumigst"]);
                $("#txtsumugst").val(this["_sumugst"]);
                $("#txtsumbillvalue").val(this["_sumbillamount"]);
                $("#txtsumtdsdeuctablevalue").val(this["_sumtdsdeductableamount"]);



            })
            document.getElementById("productDetailsGrid").innerHTML = tableNew + '</tbody></table>';
            var tdsvalue = $("#txtsumtdsdeuctablevalue").val();
            TDSPercentage1(TRANSPORTERID, tdsvalue);
            if (counter == 0) {
                $("#hdntransporterid").val('');
                $("#ddltransporter").prop("disabled", false);
                $("#ddlfromstate").prop("disabled", false);

                $("#chkReversecharge").removeAttr("disabled");
                $("#Txtbillno").removeAttr("disabled");
                $("#ddltransporter").chosen({
                    search_contains: true
                });



                $("#ddltransporter").trigger("chosen:updated");
                $("#ddlfromstate").chosen({
                    search_contains: true
                });



                $("#ddlfromstate").trigger("chosen:updated");
                rcmcalculation();
                $("#txtsumnetamt").val('0.00');
                $("#txtsumgrossweight").val('0.00');
                $("#txtsumtds").val('0.00');
                $("#txtsumcgst").val('0.00');
                $("#txtsumsgst").val('0.00');
                $("#txtsumigst").val('0.00');
                $("#txtsumugst").val('0.00');
                $("#txtsumbillvalue").val('0.00');
                $("#txtsumtdsdeuctablevalue").val('0.00');
            }
            //--- in advance credit
            //$("#txtcreditchequeno").val('');
            //$("#txtcreditchequedate").val('');
            //$("#<%= rdbcreditpaymenttype.ClientID %>").val('2');
            //$("#rdbdebitpaymenttype").trigger("chosen:updated");
        }
    });
}
//savedata
function SaveTransporterbillV2fac() {

    var mode = '';
    var tag;
    var IsTransferToHO = 'N';
    var Reversecharge = 'N';
    var tdsapplicable1 = 'N';
    var EXPORTTAG = 'N';
    var VIRTUALdepotid = '0';
    var TDS_Reason = '0';
    var GST_Reason = '0';
    var STKINV = $("input[name='STKINV']:checked").val();
    var STKTRN = $("input[name='STKTRN']:checked").val();
    var DPTRCVD = $("input[name='DPTRCVD']:checked").val();
    var STKDES = $("input[name='STKDES']:checked").val();
    var EXPORT = $("input[name='EXPORT']:checked").val();
    if (STKINV) {
        tag = STKINV;
    }
    if (STKTRN) {
        tag = STKTRN;
    }
    if (DPTRCVD) {
        tag = DPTRCVD;
    }
    if (STKDES) {
        tag = STKDES;
    }

    if (EXPORT) {
        tag = EXPORT;
    }
    if ($('#hdntrnsID').val() == '') {
        mode = 'A'
    }
    else {
        mode = 'U';
    }
    var ischecked = $('#chkReversecharge').is(":checked")
    var istds = $('#chktdsapplicable1').is(":checked")
    var istransfertoho = $('#chktransferHO').is(":checked")
    if (ischecked == true) {
        Reversecharge = 'Y';
    }
    if (istds == true) {
        tdsapplicable1 = 'Y';
    }
    if (istransfertoho == true) {
        IsTransferToHO = 'Y';
    }
    var tran = {};
    tran.ID = $('#hdntrnsID').val().trim();
    tran.Mode = mode;
    tran.TransporterID = $('#ddltransporter').val().trim();
    tran.TransporterName = $('#ddltransporter').find('option:selected').text();
    tran.billdate = $('#txtbillDate').val().trim();
    tran.billtype = tag;
    tran.Remarks = $('#txtremarks').val().trim();
    tran.MenuId = MENUID;

    tran.TOTALNETAMOUNT = $('#txtsumnetamt').val().trim();

    tran.TOTALTDS = $('#txtsumtds').val().trim();
    tran.TOTALGROSSWEIGHT = $('#txtsumgrossweight').val().trim();

    tran.depotid = $('#ddldepot').val().trim();
    tran.depotname = $('#ddldepot').find('option:selected').text();
    tran.IsTransferToHO = IsTransferToHO;

    tran.TOTALBILLAMOUNT = $('#txtsumbillvalue').val().trim();
    tran.TOTALTDSDEDUCTABLE = $('#txtsumtdsdeuctablevalue').val().trim();
    tran.sumcgst = $('#txtsumcgst').val().trim();
    tran.sumsgst = $('#txtsumsgst').val().trim();
    tran.sumigst = $('#txtsumigst').val().trim();
    tran.sumugst = $('#txtsumugst').val().trim();

    tran.BILLINGFROMSTATEID = $('#ddlfromstate').val().trim();
    tran.Reversecharge = Reversecharge
    tran.GNGNO = '';

    tran.TDSAPPLICABLE = tdsapplicable1;
    tran.TDSPECENTAGE = $('#txtTDSpercentage1').val().trim();
    tran.TDSID = $('#hdn_tdsis').val().trim();
    tran.VIRTUALdepotid = VIRTUALdepotid;
    tran.EXPORTTAG = EXPORTTAG;
    tran.ReasonID = TDS_Reason;
    tran.GSTReasonID = GST_Reason;
    tran.Finyear = FINYEAR;
    tran.userid = UserID;

    $.ajax({

        url: "/Transporter/SaveTransporterbillV2",
        data: '{tran:' + JSON.stringify(tran) + '}',
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
                //$('#dvAdd').css("display", "none");
                //$('#dvDisplay').css("display", "");
                //$("#txtfrmdate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
                //$("#txttodate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
                //ClearControls();

                close();

                //bindfgdispatchgrid();
                ReleaseSession();

            }
            else {
                // $('#dvAdd').css("display", "");
                //$('#dvDisplay').css("display", "none");
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

function clearadd() {
    $("#Txtamount").val(this['']);
    $("#txtBillvalue").val(this['']);
    $("#txttdsdeductablevalue").val(this['']);

    $("#txtremarks").val('');
    $("#TxtTds").val('0.00');
    $("#txtcgstpercentage").val('0');
    $("#txtcgstvalue").val('0.00');
    $("#txtsgstpercentage").val('0');
    $("#txtsgstvalue").val('0.00');
    $("#txtigstpercentage").val('0');
    $("#txtigstvalue").val('0.00');
    $("#txtgrossweight").val('');
    $("#hdnsgsttaxpercentage").val('');
    $("#hdnigsttaxpercentage").val('');
    $("#hdncgsttaxpercentage").val('');
    $("#ddlinvoice").val('0');
    $("#ddlinvoice").chosen({
        search_contains: true
    });



    $("#ddlinvoice").trigger("chosen:updated");

}
//list and edit
function BindTransporterbillall() {

    var fromData = $('#txtFromDate').val();
    var ToDate = $('#txtToDate').val();

    var depotid = $("#ddlregion").val();


    var counter = 0;
    $("#dialog").dialog({
        autoOpen: true,
        modal: true,
        title: "Loading.."
    });
    $("#imgLoader").css("visibility", "visible");
    setTimeout(function () {
    $.ajax({


        type: "POST",

        url: "/Transporter/BindTransporterbillalldepot",

        // data: "{'FromDate': '" + FromDate + "', 'ToDate': '" + ToDate + "','VoucherID': '" + VoucherID + "','DepotID': '" + DepotID + "','checker': '" + checker + "'}",
        data: { fromData: fromData, ToDate: ToDate, depotid: depotid, finyr: FINYEAR},

        dataType: "json",
        success: function (response) {

            var tableNew = '<table id="gvtransporterbill" class="table table-striped table-bordered dt-responsive nowrap" style="width:100%">';
            tableNew = tableNew + '<thead style="background-color:cornflowerblue;font-size:14px"><tr><th style="display:none"><th> Sl.</th><th>DEPOT </th><th> ENTRY NO.</th><th>ENTRY DATE</th><th>BILL NO</th><th>TRANSPORTER VOUCHERNO</th><th>TRANSPORTER BILLDATE</th><th>TRANSPORTER NAME</th><th>BILL TYPE</th><th>WGT(kgs)</th><th>GROSS AMT</th><th>TDS</th><th>GST(INPUT)</th><th>NET AMOUNT</th><th>GST(RCM)</th><th>APPROVAL</th><th style="display: none">DAYEND STATUS</th><th>ENTRY USER</th><th>APPROVAL PERSON</th><th>Print</th><th>Edit</th><th>Del</th></tr></thead> <tbody>';



            $.each(response, function () {

                counter = counter + 1;
                tableNew = tableNew + "<tr><td style='display:none'>" + this["TRANSPORTERBILLID"] + "</td> <td>" + counter + "</td>   <td>" + this["DEPOTNAME"] + "</td>   <td>" + this["TRANSPORTERBILLNO"] + "</td><td>" + this["ENTRYDATE"] + "</td>   <td>" + this["BILLNO"] + "</td><td>" + this["VOUCHERNO"] + "</td> <td>" + this["TRANSPORTERBILLDATE"] + "</td>  <td>" + this["TRANSPORTERNAME"] + "</td><td>" + this["BILLTYPE"] + "</td><td>" + this["TOTALGROSSWEIGHT"] + "</td><td>" + this["TOTALBILLAMOUNT"] + "</td><td>" + this["TOTALTDS"] + "</td> <td>" + this["TOTALSERVICETAX"] + "</td><td>" + this["NETAMT"] + "</td><td>" + this["TOTALSERVICETAXRCM"] + "</td> <td>" + this["ISVERIFIEDDESC"] + "</td><td style='display:none'>" + this["DAYEND"] + "</td><td>" + this["USERNAME"] + "</td><td>" + this["APPROVALPERSONNAME"] + "</td>    <td><input type='image' class='gvprint' id='btnprint'  src='../images/print.png ' width='30' height ='30'/></td><td><input type='image' class='gvedit' id='btnedit' src='../Images/Pencil-icon.png' title='Edit'/></td>     <td><input type='image' class='gvdel' id='btnDel' src='../Images/ico_delete_16.png' title='Delete' /></td> </tr>";

            });
            document.getElementById("DivHeaderRow").innerHTML = tableNew + '</table>';

            $('#gvtransporterbill').DataTable({
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




        }


        })
    $("#imgLoader").css("visibility", "hidden");
        $("#dialog").dialog("close");
    }, 5);
}

function ReleaseSession() {

    $.ajax({
        type: "POST",
        url: "/Transporter/RemoveSession",
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
