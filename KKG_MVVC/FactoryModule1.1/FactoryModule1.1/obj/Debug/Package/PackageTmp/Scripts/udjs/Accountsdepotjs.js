﻿

var VOUCHERID;
var isexpenseledger;
var FINYEAR;
var UserID;
var TPU;
var usertype;
$(document).ready(function () {
    //for cookie release if idle



    //for query strings

    var qs = getQueryStrings();
    if (qs["VOUCHERID"] != undefined && qs["VOUCHERID"] != "") {
        VOUCHERID = qs["VOUCHERID"];
        var CHECKER = qs["CHECKER"];
        var AUTOOPEN = qs["AUTOOPEN"];
        var AUTOVOUCHERID = qs["AUTOVOUCHERID"];
        var AUTOVOUCHERDATE = qs["AUTOVOUCHERDATE"];
        var MTCLAIM = qs["MTCLAIM"];
        FINYEAR = qs["FINYEAR"];
        UserID = qs["USERID"];
        TPU = qs["TPU"];
        usertype = qs["USERTYPE"];

        $("#hdnvoucherid").val(VOUCHERID);

        $("#hdnCHECKER").val(CHECKER);
        $("#hdnAUTOOPEN").val(AUTOOPEN);
        $("#hdnAUTOVOUCHERID").val(AUTOVOUCHERID);
        $("#hdnAUTOVOUCHERDATE").val(AUTOVOUCHERDATE);
        $("#hdnmtclaim").val(MTCLAIM);
        $("#hdn_totaldr").val('0');
        $("#hdn_totalcr").val('0');
        $("#hdn_costcenteramt").val('0');
        $("#txtvoucherno").prop("disabled", true);
        $('#pnlAdd').css("display", "none");
        $('#trDeptDr').css("display", "none");
        $('#trDeptCr').css("display", "none");
        if (AUTOOPEN == 'TRUE') {
            $("#hdn_mode").val(qs["MODE"]);
        }
        $("#hdn_accid").val('');

        if ($('#hdnmtclaim').val() == 'Y' && $('#hdnCHECKER').val() == 'TRUE') {
            $("#Btnadd").css("display", "none");
            $("#divbtnapproved").css("display", "");
            $("#divlblledgerdrp").css("display", "");
            $("#divlblledger").css("display", "");
            $("#btnSave").css("display", "none");
            $("#btnSavewithPrint").css("display", "none");
        }

        async: false;

    }
    $('#myModal').css("display", "none");
    $('#myModalgst').css("display", "none");
    $('#myModaltds').css("display", "none");
    var currentdt;
    var currentdt1;
    var frmdate;
    var todate;
    var today;
    //date validation
    $("#dialog").dialog({
        autoOpen: true,
        modal: true,
        title: "Loading.."
    });
    $("#imgLoader").css("visibility", "visible");

    setTimeout(function () {
        $.ajax({
            type: "POST",
            url: "/Accountstran/finyrchk",
            data: { finyr: FINYEAR, UserID: UserID, TPU: TPU },
            // data: '{}',
            async: false,
            dataType: "json",

            success: function (response) {


                //alert(JSON.stringify(response));

                //.append('<option selected="selected" value="0">Select Brancht</option>');;
                $.each(response, function () {
                    currentdt = this['currentdt'];
                    currentdt1 = this['currentdt1'];
                    frmdate = this['frmdate'];
                    todate = this['todate'];
                    today = this['today'];

                });



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
    $('#btnVendorDetails').css("display", "none");
    $('#trinvoicedetails').css("display", "none");
    $("#txtsearchfromdate").attr("disabled", "disabled");
    $("#txtsearchtodate").attr("disabled", "disabled");
    $("#txtdebittotalamount").attr("disabled", "disabled");
    $("#txtcredittotalamount").attr("disabled", "disabled");
    $("#txtbilldate").attr("disabled", "disabled");
    $("#txtgrdate").attr("disabled", "disabled");
    $("#txtwaybilldate").attr("disabled", "disabled");
    $("#txtDCJ_InvoiceDatenew").attr("disabled", "disabled");
    $('#btnshow').css("display", "none");
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

    $("#txtsearchfromdate").datepicker({
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        selectCurrent: true,
        todayBtn: "linked",
        showAnim: "slideDown",
        yearRange: "-1:+0",
        maxDate: new Date(today),
        minDate: new Date(frmdate),
        dateFormat: "dd/mm/yy",
        showOn: 'button',
        buttonText: 'Show Date',
        buttonImageOnly: false,
        buttonImage: 'http://jqueryui.com/resources/demos/datepicker/images/calendar.gif'
    });
    $(".ui-datepicker-trigger").mouseover(function () {
        $(this).css('cursor', 'pointer');
    });

    $("#txtsearchtodate").datepicker({
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        selectCurrent: true,
        todayBtn: "linked",
        showAnim: "slideDown",
        yearRange: "-1:+1",
        // maxDate: 'today',
        maxDate: new Date(today),
        minDate: new Date(frmdate),
        dateFormat: "dd/mm/yy",
        showOn: 'button',
        buttonText: 'Show Date',
        buttonImageOnly: false,
        buttonImage: 'http://jqueryui.com/resources/demos/datepicker/images/calendar.gif'
    });
    $(".ui-datepicker-trigger").mouseover(function () {
        $(this).css('cursor', 'pointer');
    });
    $("#txtvoucherdate").datepicker({
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        selectCurrent: true,
        todayBtn: "linked",
        showAnim: "slideDown",
        yearRange: "-1:+0",
        // maxDate: 'today',
        maxDate: new Date(currentdt1),
        minDate: new Date(currentdt),
        //defaultDate: "today",
        //gotoCurrent: true,
        dateFormat: "dd/mm/yy",
        showOn: 'button',
        buttonText: 'Show Date',
        buttonImageOnly: false,

        buttonImage: 'http://jqueryui.com/resources/demos/datepicker/images/calendar.gif'
    });
    $(".ui-datepicker-trigger").mouseover(function () {
        $(this).css('cursor', 'pointer');
    });

    $("#txtbilldate").datepicker({
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        selectCurrent: true,
        todayBtn: "linked",
        showAnim: "slideDown",
        yearRange: "-3:+0",
        maxDate: new Date(today),
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

    $("#txtgrdate").datepicker({
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        selectCurrent: true,
        todayBtn: "linked",
        showAnim: "slideDown",
        yearRange: "-3:+0",
        maxDate: new Date(today),
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
    $("#txtwaybilldate").datepicker({
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        selectCurrent: true,
        todayBtn: "linked",
        showAnim: "slideDown",
        yearRange: "-3:+0",
        maxDate: new Date(today),
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

    $("#txtDCJ_InvoiceDatenew").datepicker({
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        selectCurrent: true,
        todayBtn: "linked",
        showAnim: "slideDown",
        yearRange: "-3:+0",

        dateFormat: "dd/mm/yy",
        showOn: 'button',
        buttonText: 'Show Date',
        buttonImageOnly: true,
        buttonImage: 'http://jqueryui.com/resources/demos/datepicker/images/calendar.gif'
    });
    $(".ui-datepicker-trigger").mouseover(function () {
        $(this).css('cursor', 'pointer');
    });

    $("#txtcostcenterfromdate").datepicker({
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
    $("#txtcostcentertodate").datepicker({
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
    $("#txtsearchfromdate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
    $("#txtsearchtodate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
    $("#txtgrdate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
    $("#txtwaybilldate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
    var d = new Date();
    var strDate = '0' + d.getDate() + "/0" + (d.getMonth() + 1) + "/" + d.getFullYear();


    //$("#txtvoucherdate").val(strDate);
    // for panels
    $('#pnlDisplay').css("display", "");

    bindregion();

    bindvoucherhdr();


    $("#BRID").chosen({
        search_contains: true
    });


    $("#BRID").trigger("chosen:updated");


    $("#Btnadd").click(function (e) {

        addnew();

    })
    $("#btnClose").click(function (e) {
        close();

    })


    $('#ddlGSTVoucherType').change(function () {
        var vouchertype = $("#ddlGSTVoucherType").val();

        if (vouchertype == 'GSTVoucher') {
            $('#btnVendorDetails').css("display", "");
        }
        else {
            $('#btnVendorDetails').css("display", "none");
        }
    })



    $('#ddlAccTypeDr').change(function () {
        isexpenseledger = '0';
        $("#dialog").dialog({
            autoOpen: true,
            modal: true,
            title: "Loading.."
        });
        $("#imgLoader").css("visibility", "visible");

        setTimeout(function () {

            $("#ddlDepartmentDr").val('0');
            $("#ddlBusinessSegDr").val('0');
            //$("#ddlDepartmentDr").chosen({
            //    search_contains: true
            //});


            //$("#ddlDepartmentDr").trigger("chosen:updated");
            //$("#ddlBusinessSegDr").chosen({
            //    search_contains: true
            //});


            //$("#ddlBusinessSegDr").trigger("chosen:updated");
            $('#trDeptDr').css("display", "none");
            $('#lbldebitoutstanding').text('0.00');
            $('#trinvoicedetails').css("display", "none");
            var accdrvalue = $("#ddlAccTypeDr").val();
            $("#hdn_costcenterapplicable").val('N');
            $("input#txtAmtDr").focus();
            checkwxpenseledgerornot(accdrvalue, 'Dr');
            $("#imgLoader").css("visibility", "hidden");
            $("#dialog").dialog("close");


        }, 5);


    })
    $('#ddlDCJ_GroupName').change(function () {
        BindDCJ_Party();
    })
    $('#ddlToStateName').change(function () {

        BindDCJ_GstNo();

    })
    $('#ddlStateNamenew').change(function () {
        BindDCJ_GstNo();
    })
    $('#ddlDCJ_Partynew').change(function () {
        BindDCJ_GstNo();
    });
    $('#ddlAcctypeCr').change(function () {
        isexpenseledger = '0';

        $("#dialog").dialog({
            autoOpen: true,
            modal: true,
            title: "Loading.."
        });
        $("#imgLoader").css("visibility", "visible");

        setTimeout(function () {
            $("#ddlDepartmentCr").val('0');
            $("#ddlBusinessSegCr").val('0');
            $('#trDeptCr').css("display", "none");
            $('#lblcreditoutstanding').text('0.00');
            var acccrvalue = $("#ddlAcctypeCr").val();
            $("#hdn_costcenterapplicable").val('N');
            $("input#txtAmntCr").focus();
            checkwxpenseledgerornot(acccrvalue, 'Cr');

            $("#imgLoader").css("visibility", "hidden");
            $("#dialog").dialog("close");


        }, 5);


    })
    //debit add
    $("#btndebitadd").click(function (e) {
        var isexists = 'n';
        var accdrvalue = $("#ddlAccTypeDr").val();
        var deptid = $("#ddlDepartmentDr").val();
        var businesssegmentid = $("#ddlBusinessSegDr").val();
        if ($('#debitadd').length) {
            $("#debitadd tbody tr").each(function () {
                var accdr = $(this).find('td:eq(0)').html();
                // var batchidgrd = $(this).find('td:eq(3)').html();

                if (accdr == accdrvalue) {
                    isexists = 'y';
                    return false;
                }
            })
        }
        if (accdrvalue == '0') {
            return false;
        }
        else
            if (isexists == 'y') {
                //alert('This ledger already exists in Dr. side')
                toastr.warning('<b><font color=black>This ledger already exists in Dr. side..!</font></b>');
            }
            else
                if (isexpenseledger == '1' && deptid == '0') {
                    //alert('This ledger already exists in Dr. side')
                    toastr.warning('<b><font color=black>Provide Department in Dr. side..!</font></b>');
                }
                else
                    if (isexpenseledger == '1' && businesssegmentid == '0') {
                        //alert('This ledger already exists in Dr. side')
                        toastr.warning('<b>Provide Business Segment in Dr. side..!</font></b>');
                    }
                    else {

                        adddr();
                    }
    })
    //debit del
    $("body").on("click", "#debitadd .debitdel", function () {
        var totaldebit = 0;


        //$('.debitdel').on('click', function () {


        if (confirm("Do you want to delete this voucher?")) {
            var row = $(this).closest("tr");




            var accdrvalue = row.find('td:eq(0)').html();
            debitdel(accdrvalue);
            chktdsdel(accdrvalue);
            row.remove();
            //chktdsdel(accdrvalue);
        }

    })
    //credit del
    function chktdsdel(accdrvalue) {
        var totalcredit = 0;
        $("#creditadd tbody tr").each(function () {
            //var istds=$(this).find('td:eq(5)').html();
            var accledgerid = $(this).find('td:eq(10)').html();

            if (accledgerid == accdrvalue) {
                var amtcr = $(this).find('td:eq(2)').html();
                totalcredit = parseFloat($('#txtcredittotalamount').val());
                totalcredit = totalcredit - parseFloat(amtcr);
                $("#txtcredittotalamount").val(totalcredit);


                // $("#creditadd tbody tr").remove();
                $(this).remove();

                //return false;
            }


        })
    }

    $("body").on("click", "#creditadd .Creditdel", function () {
        var tdsledgerid = '';
        var accid = '';
        var tdsapplicable = '';


        if (confirm("Do you want to delete this Account?")) {
            var row = $(this).closest("tr");
            var accr = row.find('td:eq(0)').html();
            accid = row.find('td:eq(5)').html();
            tdsapplicable = row.find('td:eq(11)').html();

            var deducttableledgerid = row.find('td:eq(6)').html();
            var amtcr = row.find('td:eq(2)').html();

            //$("#creditadd tbody tr").each(function () {
            //    tdsledgerid = $(this).find('td:eq(0)').html();

            //    if ((deducttableledgerid == tdsledgerid) && (accr ==accid)) {
            //        tdsapplicable = 'y';
            //        return false;
            //    }
            //})
            if (tdsapplicable == 'false') {
                //totalcredit = parseFloat($('#<%=hdn_totalcr.ClientID%>').val());

                // _totaldebit//totalcredit = totalcredit - parseFloat(amtcr);
                //$("#txtcredittotalamount").val(totalcredit);
                //$("#hdn_totalcr").val(totalcredit);


                creditdel(accr, tdsapplicable);
                row.remove();
                // chktdsdel(accr
            }
            else {
                toastr.info('<b><font color=black>TDS ledger , you cant delete this!..!</font></b>');
            }
        }

    })

    //credit add
    $("#btncreditadd").click(function (e) {
        var isexists = 'n';
        var amtcr = $('#txtAmntCr').val();
        var acccrvalue = $("#ddlAcctypeCr").val();
        var deptid = $("#ddlDepartmentCr").val();
        var businesssegmentid = $("#ddlBusinessSegCr").val();
        if ($('#creditadd').length) {
            $("#creditadd tbody tr").each(function () {
                var accdr = $(this).find('td:eq(0)').html();
                // var batchidgrd = $(this).find('td:eq(3)').html();

                if (accdr == acccrvalue) {
                    isexists = 'y';
                    return false;
                }
            })
        }
        if (acccrvalue == '0') {
            return false;
        }
        if (isexists == 'y') {
            //alert('This ledger already exists in Cr. side')
            toastr.warning('<b><font color=black>This ledger already exists in Cr. side..!</font></b>');
        }

        if (isexpenseledger == '1' && deptid == '0') {
            //alert('This ledger already exists in Dr. side')
            toastr.warning('<b><font color=black>Provide Department in Cr. side..!</font></b>');
        }
        else
            if (isexpenseledger == '1' && businesssegmentid == '0') {
                //alert('This ledger already exists in Dr. side')
                toastr.warning('<b>Provide Business Segment in Cr. side..!</font></b>');
            }
            else {
                addcr();

            }


    })
    //save data
    $("#btnSave").click(function (e) {
        savedata(0);
    })
    $("#btnSavewithPrint").click(function (e) {
        savedata(1);
    })
    //approved data
    $("#btnapproved").click(function (e) {
        approveddata();
    })
    //gst related
    $("#btnVendorDetails").click(function (d) {
        if ($('#grdgstnew').length) {

            ShowDialoggstnew(this);
            e.preventDefault();
        }
        else
            if ($("#hdn_accid").val() != '') {

                Viewgst();
                ShowDialoggstnew(this);
                e.preventDefault();
            }
            else {


                gstpopup();
            }
    })
    $("#btngstclosenew").click(function (e) {

        var totaldebit = $('#txtdebittotalamount').val();
        var _totaldebit = 0;
        var _totalcredit = 0
        var totalcredit;

        var totalgst = $('#txtGSTTotal').val();

        var _totalgst = 0;
        _totalgst = parseFloat(totalgst);

        _totaldebit = parseFloat(totaldebit);

        if (_totaldebit != _totalgst) {
            toastr.warning('Please check total debit/credit amount and Net Amount or please refresh this page and try again ...!');
            return false;
        }
        else {
            var invoiceno = $('#txtDCJ_InvoiceNonew').val();
            var invoicedate = $('#txtDCJ_InvoiceDatenew').val();
            $('#txtbillno').val(invoiceno);
            $('#txtbilldate').val(invoicedate);
            $('#isgstpaid').val('Y');

            HideDialoggstnew();
            e.preventDefault();
        }
    })

    $("#btngstclosegstnew").click(function (e) {


        HideDialoggstnew();
        e.preventDefault();
    });
    $("#btntdsclose").click(function (e) {
        HideDialogtds();
        e.preventDefault();
    })
    $('#txtAmtDr').change(function (e) {

        var amtdr = $('#txtAmtDr').val();
        var accdrvalue = $("#ddlAccTypeDr").val();
        var accountdrtext = $('#ddlAccTypeDr').find('option:selected').text();
        var dewpartment = $("#hdn_Department").val();
        var costcenterapp = $("#hdn_costcenterapplicable").val();
        $("#hdn_Amount_Dr").val(amtdr);
        $("#hdn_drcr").val('Dr');
        $("#hdn_Account_Dr").val(accdrvalue);
        $("#hdn_AccountName_Dr").val(accountdrtext);
        var voucherdate = $('#txtvoucherdate').val();
        $("#hdn_accounttype").val('0');
        var region = $("#ddlregion").val();
        var trantype = 'Dr';
        $("#dialog").dialog({
            autoOpen: true,
            modal: true,
            title: "Loading.."
        });
        $("#imgLoader").css("visibility", "visible");

        setTimeout(function () {
            if (costcenterapp == 'y') {
                // checkbudget(accdrvalue, amtdr, voucherdate, dewpartment);
            }
            if (accdrvalue != '0') {
                Outstanding('Dr', accdrvalue, accountdrtext);
                InvoiceDetails('Dr', accdrvalue, accountdrtext);
                costcenterapplicable('Dr');
                addtds(trantype);
                $("input#btndebitadd").focus();
            }
            $("#imgLoader").css("visibility", "hidden");
            $("#dialog").dialog("close");


        }, 5);


    })
    //BindDepartment();
    $(function () {
        $("#btnaddproduct").click(function (e) {

            BindProduct();
        })
    })
    $("#btncostcenteradd").click(function (e) {

        costcenteradd();
    })
    $("#btncostcentersave").click(function (e) {
        costcentersave();
    })
    $("#btncostclose").click(function (e) {
        HideDialogcostcenter();
        e.preventDefault();
    });
    $("body").on("click", "#grdcostcenter .clscostdel", function () {



        //$('.debitdel').on('click', function () {


        if (confirm("Do you want to delete this Entry?")) {
            var row = $(this).closest("tr");


            var guid = row.find('td:eq(1)').html();
            var ledgerid = row.find('td:eq(0)').html();
            row.remove();
            $.ajax({

                type: "POST",
                url: "/Accountstran/deletecostcenter",
                data: { guid: guid, ledgerid: ledgerid },
                //data: "{'ledgerid': '" + ledgerid + "','CostCatagoryID': '" + CostCatagoryID + "','BrandId': '" + BrandId + "','ProductId': '" + ProductId + "','DepartmentId': '" + DepartmentId + "'}",

                dataType: "json",
                success: function (r) {

                    var counter = 0;

                    var tableNewcost1 = '<table id="costcentertable" class="table table-striped table-bordered dt-responsive nowrap">';
                    tableNewcost1 = tableNewcost1 + '<thead style="background-color:cornflowerblue;font-size:14px"><tr><th style="display:none"></th><th style="display:none"></th><th style="display:none"></th><th style="display:none"></th><th style="display:none"></th><th style="display:none"></th><th style="display:none"></th><th style="display:none"></th><th>CostCenter</th><th>Category</th><th>Brand</th><th>Product</th><th>Department</th> <th> FromDt</th><th>To Dt.</th><th>Branch</th><th>Amount</th><th></th></tr></thead><tbody>';




                    $.each(function (r) {

                        tableNewcost1 = tableNewcost1 + "<tr><td style='display:none'>" + this["LedgerId"] + "</td><td style='display:none'>" + this["GUID"] + " </td><td style='display:none'>" + this["CostCatagoryID"] + "</td> <td style='display:none'>" + this["BrandId"] + "</td><td style='display:none'> " + this["ProductId"] + "</td> <td style='display:none'> " + this["DepartmentId"] + "</td> <td style='display:none'> " + this["BranchID"] + "</td> <td style='display:none'> " + this["CostCenterID"] + " </td> <td> " + this["CostCenterName"] + " </td> <td> " + this["CostCatagoryName"] + " </td> <td> " + this["BrandName"] + "</td> <td> " + this["ProductName"] + " </td> <td> " + this["DepartmentName"] + " </td> <td> " + this["FromDate"] + " </td> <td> " + this["ToDate"] + " </td> <td> " + this["BranchName"] + "  </td> <td> <input type='text' class='costcenteramt' id='txtamount' style='text-align:right' value='" + this["amount"] + "'/></td><td style='width:10%' ><input type='button' id='btncostcenterdel'  class='clscostdel' value='Del'/></td></tr>";
                        counter = counter + 1;

                    })
                    document.getElementById("grdcostcenter").innerHTML = tableNewcost1 + '</tbody></table>';

                    if (counter == 0) {
                        $("#ddldepartment").prop("disabled", false);
                        $("#ddlcatagory").prop("disabled", false);
                        $("#ddldepartment").val('0');
                        $("#ddlcatagory").val('0');
                    }






                }
            });

        }
    })

    //other details

    $("#btnotherdetails").click(function (e) {
        ShowDialogothers(true);
        e.preventDefault();
    });
    $("#btnotherclose").click(function (e) {
        HideDialogothers();
        e.preventDefault();
    });
    $("#btnotherdetailsave").click(function (e) {
        HideDialogothers();
        e.preventDefault();
    });
    BindCostCenterCatagory();
    BindBrand();
    BindCostCenter();
    BindDepartment();

    //gstadd
    $("#btnGstAdd").click(function () {


        var GroupId = $("#ddlDCJ_GroupName").val();
        var PartyID = $("#ddlDCJ_Partynew").val();
        var InvoiceNo = $("#txtDCJ_InvoiceNonew").val();
        var InvoiceDate = $("#txtDCJ_InvoiceDatenew").val();
        var Taxable = $("#txtDCJ_Taxablenew").val();
        var TaxableValue = $("#txtDCJ_TaxableValuenew").val();
        var HSNCode = $("#txtDCJ_HSNCodenew").val();
        var PartyTrade = $("#txtDCJ_PartyTradenew").val();
        var StateID = $("#ddlStateNamenew").val();
        var GSTNo = $("#ddlDCJ_GstNonew").val();
        var TaxTypeID = $("#ddlDCJ_TaxType").val();
        var TaxType = $('#ddlDCJ_TaxType').find('option:selected').text();
        var TaxAmount = $("#txtDCJ_TaxAmount").val();
        var NetAmount = $("#txtDCJ_NetAmount").val();
        var Taxable1 = $("#txtDCJ_Taxable1").val();
        var TaxAmount1 = $("#txtDCJ_TaxableValue1").val();
        var TaxTypeID1 = $("#lblTaxTypeid").val();
        var Taxtype1 = $("#txtDCJ_TaxType1").val();
        var igst = $("#txtDCJ_ISIGST").val();
        var txtDCJ_NonTaxableAmount = $("#txtDCJ_NonTaxableAmount").val()
        var PlaceOfSupply = $("#ddlToStateName").val();
        var roundoff;
        if ($("#txtRoundOff").val() == '') {
            $("#txtRoundOff").val('0');
            roundoff = $("#txtRoundOff").val();

        }
        else {
            roundoff = $("#txtRoundOff").val();
            var _roundoff = parseFloat(roundoff);
            if (_roundoff > 5) {
                toastr.info('Round Off can not greater than 5.00 ...!');
                return false;
            }
        }
        if (StateID == '0') {
            toastr.info('Please select state name...!');
            return false
        }
        if (GroupId == '0') {
            toastr.info('Please select group name...!');
            return false
        }
        if (PartyID == '0') {
            toastr.info('Please select party name...!');
            return false
        }
        if (GSTNo == '0') {
            toastr.info('Please select GST No...!');
            return false
        }
        if (InvoiceNo == '') {
            toastr.info('Please enter invoice no....!');
            return false
        }
        if (InvoiceDate == '') {
            toastr.info('Please enter invoice date....!');
            return false
        }
        if (Taxable == '0') {
            toastr.info('Please enter taxable (%)....!');
            return false
        }

        if (TaxableValue == '') {
            toastr.info('Please enter taxable value....!');
            return false
        }
        if (HSNCode == '') {
            toastr.info('Please enter HSN Code....!');
            return false
        }
        if (PartyTrade == '') {
            toastr.info('Please enter party trade....!');
            return false
        }
        if (TaxType == '') {
            toastr.info('Please select Tax type....!');
            return false
        }
        addgstnew(GroupId, PartyID, InvoiceNo, InvoiceDate, Taxable, TaxableValue, HSNCode, PartyTrade, StateID, GSTNo, TaxTypeID, TaxType, TaxAmount, NetAmount, Taxable1, TaxAmount1, TaxTypeID1, Taxtype1, igst, roundoff, txtDCJ_NonTaxableAmount, PlaceOfSupply);

    })
    $("body").on("click", "#grdgstnew .gstdel", function () {

        var guid;


        //$('.debitdel').on('click', function () {

        var row = $(this).closest("tr");
        guid = row.find('td:eq(2)').html();
        var _gstamt = 0;;
        var _gsttotal = 0;
        if (confirm("Do you want to delete this GSt Details?")) {
            $.ajax({

                type: "POST",

                url: "/Accountstran/delnewgst",

                data: "{'guid': '" + guid + "'}",
                contentType: "application/json; charset=utf-8",
                success: function (r) {
                    var counter = 0;
                    var tableNew = '<table id="grdgstnew" class="reportgrid">';
                    tableNew = tableNew + "<thead style='background-color:cornflowerblue;font-size:14px'><tr><th style='display:none'></th><th style='display:none'></th><th style='display:none'></th><th>SL</th> <th>HSN CODE </th><th>TAX TYPE1</th> <th>TAX TYPE2</th> <th>TAXABLE (%)</th> <th>TAXABLE VALUE</th> <th>TAX AMOUNT</th> <th>NET AMOUNT</th><th></th> </tr></thead><tbody>";
                    $.each(r.d, function () {
                        _gstamt = _gstamt + parseFloat(this["NetAmount"]);
                        counter = counter + 1;
                        tableNew = tableNew + "<tr><td style='display:none'>" + this["GroupId"] + "</td><td style='display:none'>" + this["PartyID"] + "</td><td style='display:none'>" + this["GUID"] + "</td><td>  " + counter + "</td> <td>" + this["HSNCode"] + "</td><td>" + this["TaxType"] + "</td><td>" + this["TaxType1"] + "</td><td>" + this["Taxable1"] + "</td><td>" + this["TaxAmount"] + "</td><td>" + this["NetAmount"] + "</td><td style='width:10px'><input type='button' id='btndebitgrddelete'  class='gstdel' value='Del'/></td>";

                    })

                    document.getElementById("GridView_DCJ_GST").innerHTML = tableNew + '</tbody></table>';
                    $("#txtGSTTotal").val(_gstamt);
                    if (counter == 0) {
                        $("#ddlStateNamenew").prop("disabled", false);
                        $("#ddlDCJ_GroupName").prop("disabled", false);
                        $("#ddlDCJ_Partynew").prop("disabled", false);
                        $("#ddlDCJ_GstNonew").prop("disabled", false);

                        $("#txtDCJ_InvoiceNonew").removeAttr("disabled");
                        $("#txtDCJ_PartyTradenew").removeAttr("disabled");
                        $("#txtDCJ_InvoiceNonew").removeAttr("disabled");
                    }
                }
            })

        }









    })
    $('#ddlDCJ_TaxType').change(function () {
        var taxtypeid = $("#ddlDCJ_TaxType").val();
        var isigst = '';
        $("#txtDCJ_TaxType1").val('');
        $("#lblTaxTypeid").val('');
        $("#txtDCJ_ISIGST").val('');
        $.ajax(
            {
                type: "POST",
                url: "/Accountstran/BindDCJ_TaxTypeName",
                data: { taxtypeid: taxtypeid },


                dataType: "json",
                success: function (response) {




                    $.each(response, function () {

                        // alert(this["NAME"]);
                        $("#txtDCJ_TaxType1").val('');
                        $("#lblTaxTypeid").val(this[""]);
                        $("#txtDCJ_TaxType1").val(this["NAME"]);
                        $("#lblTaxTypeid").val(this["ID"]);

                        $("#txtDCJ_Taxable").val('');
                        $("#txtDCJ_TaxableValue").val('');

                        $("#txtDCJ_Taxable1").val('');
                        $("#txtDCJ_Taxable1").val('');
                        $("#txtDCJ_TaxableValue1").val('');
                        $("#txtDCJ_TaxAmount").val('');
                        $("#txtDCJ_NetAmount").val('');
                        isigtxtDCJ_Taxablenewst = this["ISIGST"];
                    })

                    if ($('#ddlGSTVoucherType').val() == 'CustomDutyVoucher') {
                        $("#txtDCJ_ISIGST").val('C');
                    }
                    else {
                        $("#txtDCJ_ISIGST").val(isigst);
                    }




                    //var ddlsupplingdepot = $("[id*=ddlsupplingdepot]");
                    //ddlsupplingdepot.empty().append('<option selected="selected" value="0">Please select</option>');
                    //$.each(r.d, function () {
                    //    ddlsupplingdepot.append($("<option></option>").val(this['Value']).html(this['Text']));
                    //});
                }
            })
    })
    //budget
    $("#btnBudgetSave").click(function (e) {
        HideDialog();
        e.preventDefault();
    });
    $("#btnbudgetcnc2").click(function (e) {
        HideDialog();
        e.preventDefault();
    });
    $("#btnBudgetclose").click(function (e) {
        HideDialog();
        e.preventDefault();
    });
    $("#btnBudget").click(function (e) {

        $('#div_BudgetSave').css("display", "");

        addbudget();
    });
    $("#btnDepartment").click(function (e) {

        ShowDialog(true);
        e.preventDefault();
        $('#div_BudgetSave').css("display", "none");
        $('#divbudgetadd').css("display", "none");
        $('#dept2').css("display", "none");
        $('#dept1').css("display", "none");
        $('#btnbudgetcncl').css("display", "");




    });
    $("#btnshow").click(function (e) {

        ShowDialogfile(true);
        e.preventDefault();
    })
    $("#chkfileupload").click(function (e) {

        var file = $('#chkfileupload').is(":checked");

        if (file == true) {
            $('#btnshow').css("display", "");
            ShowDialogfile(true);
            $("#chkfileupload").prop('checked', true);
            // e.preventDefault();

        }
        if (file == false) {
            $('#btnshow').css("display", "none");
            HideDialogfile();
            //e.preventDefault(); 
            $("#chkfileupload").prop('checked', false);
        }

    })
    $("#btnfileclose").click(function (e) {
        HideDialogfile();
        e.preventDefault();
    });
    $("#btnupload").click(function (e) {

        var fileUpload = $("#txtquotupload").get(0);
        var files = fileUpload.files;
        var fileData = new FormData();
        for (var i = 0; i < files.length; i++) {
            fileData.append(files[i].name, files[i]);
        }
        $.ajax({
            url: "/Accountstran/fileupload",
            type: "POST",
            contentType: false, // Not to set any content header  
            processData: false, // Not to process data  
            data: fileData,
            success: function (response) {
                var tableNew = '<table id="fileupload">';
                tableNew = tableNew + '<thead><tr><th>Counter</th> <th>File name </th><th>Download</th><th>Delete</th></thead><tbody>';
                var counter = 0;
                toastr.info('File uploaded successfully');
                $.each(response, function () {
                    counter = counter + 1;

                    tableNew = tableNew + "<tr><td style='width:80px'>" + counter + "</td><td>" + this["UPLOADACCOUNTSFILENAME"] + "</td><td ><a href='~/Accvoucher' download='" + this["filepath"] + "'><img src='../Images/download.png' width='20' height ='20' title='Download'/></a>  </td><td ><input type='button' id='btndelfile'  class='clsfiledel' value='Del'/></td></tr > ";

                });
                document.getElementById("grdfileupload").innerHTML = tableNew + '</tbody></table>';
            },
            error: function (err) {
                toastr.error(err.statusText);
            }
        });


    })

    $("body").on("click", "#grdfileupload .clsfiledel", function () {



        //$('.debitdel').on('click', function () {


        if (confirm("Do you want to delete this Entry?")) {
            var row = $(this).closest("tr");


            var filename = row.find('td:eq(0)').html();

            row.remove();
            $.ajax({

                type: "POST",
                url: "/Accountstran/deletefile",
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




    $("body").on("click", "#debitadd .clscost", function () {
        var row = $(this).closest("tr");


        var accdrvalue = row.find('td:eq(0)').html();
        var iscostcenter = row.find('td:eq(7)').html();

        if (iscostcenter == 'Y') {
            $.ajax({

                type: "POST",

                url: "/Accountstran/viewcostcenter",

                // data: "{'ledgerid': '" + accdrvalue + "'}",
                data: { ledgerid: accdrvalue },


                success: function (response) {

                    var tableNewcost1 = '<table id="costcentertable" class="table table-striped table-bordered dt-responsive nowrap">';
                    tableNewcost1 = tableNewcost1 + '<thead style="background-color:cornflowerblue;font-size:14px"><tr><th style="display:none"></th><th style="display:none"></th><th style="display:none"></th><th style="display:none"></th><th style="display:none"></th><th style="display:none"></th><th style="display:none"></th><th style="display:none"></th><th>CostCenter</th><th>Category</th><th>Brand</th><th>Product</th><th>Department</th> <th> FromDt</th><th>To Dt.</th><th>Branch</th><th>Amount</th><th></th></tr></thead><tbody>';




                    $.each(response, function () {

                        //tableNewcost1 = tableNewcost1 + "<tr><td style='display:none'>" + this["LedgerId"] + "</td><td style='display:none'> </td><td style='display:none'>" + this["CostCatagoryID"] + "</td> <td style='display:none'>" + this["BrandId"] + "</td><td style='display:none'> " + this["ProductId"] + "</td> <td style='display:none'> " + this["DepartmentId"] + "</td> <td style='display:none'> " + this["BranchID"] + "</td> <td style='display:none'> " + this["CostCenterID"] + " </td> <td> " + this["CostCenterName"] + " </td> <td> " + this["CostCatagoryName"] + " </td> <td> " + this["BrandName"] + "</td> <td> " + this["ProductName"] + " </td> <td> " + this["DepartmentName"] + " </td> <td> " + this["FromDate"] + " </td> <td> " + this["ToDate"] + " </td> <td> " + this["BranchName"] + "  </td> <td> <input type='text' class='costcenteramt' id='txtamount' style='text-align:right' value='" + this["amount"] + "'/></td><td style='width:10%' ><input type='button' id='btncostcenterdel'  class='clscostdel' value='Del'/></td></tr>";
                        tableNewcost1 = tableNewcost1 + "<tr><td style='display:none'>" + this["LedgerId"] + "</td><td style='display:none'>" + this["GUID"] + " </td><td style='display:none'>" + this["CostCatagoryID"] + "</td> <td style='display:none'>" + this["BrandId"] + "</td><td style='display:none'> " + this["ProductId"] + "</td> <td style='display:none'> " + this["DepartmentId"] + "</td> <td style='display:none'> " + this["BranchID"] + "</td> <td style='display:none'> " + this["CostCenterID"] + " </td> <td> " + this["CostCenterName"] + " </td> <td> " + this["CostCatagoryName"] + " </td> <td> " + this["BrandName"] + "</td> <td> " + this["ProductName"] + " </td> <td> " + this["DepartmentName"] + " </td> <td> " + this["FromDate"] + " </td> <td> " + this["ToDate"] + " </td> <td> " + this["BranchName"] + "  </td> <td> <input type='text' class='costcenteramt' id='txtamount' style='text-align:right' value='" + this["amount"] + "'/></td><td style='width:10%' ><input type='button' id='btncostcenterdel'  class='clscostdel' value='Del'/></td></tr>";


                    })

                    document.getElementById("grdcostcenter").innerHTML = tableNewcost1 + '</tbody></table>';

                    ShowDialogcostcenter(true);
                    e.preventDefault();

                }


            });
        }


    });

    $("body").on("change", "#tdslist .chktds", function () {

        var row = $(this).closest("tr");
        //var chkinv = row.find("#chktaxid").is(":checked");
        var chk = row.find("#chktaxid").val();
        //if (chkinv == true) {
        //    chkinv.val('1');
        //}
        //else {
        //    chkinv.val('0s')
        //}
        if (chk == '1') {
            row.find("#chktaxid").val('0');
        }
        else
            if (chk == '0') {
                row.find("#chktaxid").val('1');
            }


    })
    $("#btntdsadd").click(function (e) {
        var deducttableledgerid;
        var deducttableledgername;
        var DeductableAmount;
        var totaldeductableamt = 0;
        var totNonTaxableAmount = 0;
        var totByDefaultAmount = 0;
        var totTransactionAmount = 0;
        var IsCostCenter = 'N';

        var IsTagInvoice = 'N';
        var Isautoposting = 'Y';
        var amtcr;
        var tdssum = 0;
        var i = 0;
        var totaldebit = 0;
        var totalcreditt = 0;
        var accdrvalue;
        var accname;
        var drcr = $('#hdn_drcr').val();
        if (drcr == 'Dr') {
            accdrvalue = $("#ddlAccTypeDr").val();
            accname = $('#ddlAccTypeDr').find('option:selected').text();
            amount = $('#txtAmtDr').val();
            var departmentid = $("#ddlDepartmentDr").val();
            // var departmentname = $('#ddldepartmentBud').find('option:selected').text();
            var departmentname = $('#ddlDepartmentDr').find('option:selected').text();

            var BusinessSegId = $("#ddlBusinessSegDr").val();
            var BusinessSegName = $('#ddlBusinessSegDr').find('option:selected').text();

            // var departmentname = $('#ddldepartmentBud').find('option:selected').text();
            totaldebit = parseFloat($('#txtdebittotalamount').val());
            totaldebit = totaldebit + parseFloat(amount);
            $("#txtdebittotalamount").val(totaldebit);
            $("#hdn_totaldr").val(totaldebit);
        }
        else {
            accdrvalue = $("#ddlAcctypeCr").val();
            accname = $('#ddlAcctypeCr').find('option:selected').text();
            amount = $('#txtAmntCr').val();
            //totalcreditt = parseFloat($('#<%=hdn_totalcr.ClientID%>').val());
            // totalcreditt = totalcreditt + parseFloat(amount);
            //$("#txtcredittotalamount").val(totalcreditt);
            //$("#hdn_totalcr").val(totalcreditt); --%>
            var departmentid = $("#ddlDepartmentCr").val();
            // var departmentname = $('#ddldepartmentBud').find('option:selected').text();
            var departmentname = $('#ddlDepartmentCr').find('option:selected').text();

            var BusinessSegId = $("#ddlBusinessSegCr").val();
            var BusinessSegName = $('#ddlBusinessSegCr').find('option:selected').text();

        }

        $('#myModaltds').css("display", "");

        $('#tdslist tbody tr').each(function () {
            var chk = $(this).find("td:eq(1) input[type='checkbox']").val()

            // if ($(this).find('input[type="checkbox"]').is(':checked')) {
            if (chk == '1') {
                deducttableledgerid = $(this).find('td:eq(0)').html();

                deducttableledgername = $(this).find('td:eq(2)').html();
                amtcr = parseFloat($(this).find('td:eq(7)').html());

                actualamt = (parseFloat(amount) - parseFloat(amtcr));
                var drcrtds;

                drcrtds = taxaction(deducttableledgerid);
                totalcreditt = parseFloat($('#txtcredittotalamount').val());
                if (drcr == 'Dr') {
                    totalcreditt = totalcreditt + parseFloat(amtcr);
                }
                else
                    if (drcr == 'Cr') {
                        totalcreditt = totalcreditt + parseFloat(amtcr) + parseFloat(actualamt);
                    }

                $("#txtcredittotalamount").val(totalcreditt);

                // $("#hdn_totalcr").val(totalcreditt);

                //for tds ledger
                //var drcrtds = taxaction.isdrcr;
                var taxapplicable = 'true';
                var row = $(this);



                DeductableAmount = parseFloat($(this).find('td:eq(5)').html());
                totaldeductableamt = totaldeductableamt + DeductableAmount;
                var DeductablePercent = $(this).find('td:eq(6)').html();

             /*   var DeductableLedgerID=accdrvalue*/;

                var NonTaxableAmount = parseFloat($(this).find("td:eq(8) input[type='text']").val());
                totNonTaxableAmount = totNonTaxableAmount + NonTaxableAmount
                //var ByDefaultAmount = $(this).find('txtByDefault').val();
                var ByDefaultAmount = parseFloat($(this).find("td:eq(3) input[type='text']").val());
                totByDefaultAmount = totByDefaultAmount + ByDefaultAmount;
                var TransactionAmount = parseFloat($(this).find("td:eq(4) input[type='text']").val());
                totTransactionAmount = totTransactionAmount + TransactionAmount;
                var remarks = '';

                // if (drcrtds == 'Dr') {
                //     Debitadd(deducttableledgerid,deducttableledgername,amtcr,DeductableAmount,DeductablePercent,accdrvalue,IsCostCenter,IsTagInvoice,Isautoposting,NonTaxableAmount,ByDefaultAmount,TransactionAmount,remarks,departmentid,departmentname,taxapplicable);
                //}
                //else
                //     if (drcrtds ==  'Cr') {
                //     Creditadd(deducttableledgerid,deducttableledgername,amtcr,DeductableAmount,DeductablePercent,accdrvalue,IsCostCenter,IsTagInvoice,Isautoposting,NonTaxableAmount,ByDefaultAmount,TransactionAmount,remarks,departmentid,departmentname,taxapplicable,taxapplicable,'','','','');
                //       }


                Creditadd(deducttableledgerid, deducttableledgername, amtcr, DeductableAmount, DeductablePercent, accdrvalue, IsCostCenter, IsTagInvoice, Isautoposting, NonTaxableAmount, ByDefaultAmount, TransactionAmount, remarks, departmentid, departmentname, taxapplicable, '', '', '', '', BusinessSegId, BusinessSegName);
                //removed for multiple entry
                //if (drcr == 'Dr') {
                //    //addcr(accrid, accrname, amtcr, accdrvalue);
                //    //adddr();


                //    $('#txtAmtDr').val('');
                //    $("#ddlAccTypeDr").val('0');
                //    $("#ddlAccTypeDr").chosen({
                //        search_contains: true
                //    });


                //    $("#ddlAccTypeDr").trigger("chosen:updated");
                //}

            }

            // var accrid1 = $(t.rows[i].cells[2]).text();
            // "toGroup": $('td:eq(1) input',this).val(),

        })
        if (drcr == 'Dr') {
            //addcr(accrid, accrname, amtcr, accdrvalue);
            //adddr();

            Debitadd(accdrvalue, accname, actualamt, totaldeductableamt, 0, accdrvalue, IsCostCenter, IsTagInvoice, Isautoposting, totNonTaxableAmount, totByDefaultAmount, totTransactionAmount, '', departmentid, departmentname, 'false', BusinessSegId, BusinessSegName);
            $('#txtAmtDr').val('');
            $("#ddlAccTypeDr").val('0');
            $("#ddlAccTypeDr").chosen({
                search_contains: true
            });


            $("#ddlAccTypeDr").trigger("chosen:updated");
        }

        else if (drcr == 'Cr') {
            //addcractual();
            //addcr(accrid, accrname, amtcr, accdrvalue);
            Creditadd(accdrvalue, accname, actualamt, totaldeductableamt, 0, accdrvalue, IsCostCenter, IsTagInvoice, Isautoposting, totNonTaxableAmount, totByDefaultAmount, totTransactionAmount, '', departmentid, departmentname, 'false', '', '', '', '', BusinessSegId, BusinessSegName);

        }
        $('#myModaltds').css("display", "none");


        HideDialogtds();
        // e.preventDefault();


    });
    $('#txtAmntCr').change(function () {
        var ledgerid = $('#ddlAcctypeCr').val();
        var amtcr = $('#txtAmntCr').val();
        var acccrvalue = $("#ddlAcctypeCr").val();
        var accountcrtext = $('#ddlAcctypeCr').find('option:selected').text();
        $("#hdn_Amount_Cr").val(amtcr);
        $("#hdn_drcr").val('Cr');
        $("#hdn_Account_Cr").val(acccrvalue);
        $("#hdn_AccountName_Cr").val(accountcrtext);
        var voucherdate = $('#txtvoucherdate').val();
        var region = $("#ddlregion").val();
        //$.ajax({

        //    type: "POST",

        //    url: "frmaccvouchernew2.aspx/checkoutstandingcr",

        //    data: "{'acctypecr': '" + acccrvalue + "', 'region': '" + region + "','voucherdate': '" + voucherdate + "'}",
        //    contentType: "application/json; charset=utf-8",
        //    success: function (response) {




        //        $("#lblcreditoutstanding").val(response.d);



        //    }

        //});
        $("#dialog").dialog({
            autoOpen: true,
            modal: true,
            title: "Loading.."
        });
        $("#imgLoader").css("visibility", "visible");
        setTimeout(function () {
            $("input#btncreditadd").focus();
            var trantype = 'Cr';
            if (ledgerid != '0') {
                addtds(trantype);
                Outstanding('Cr', ledgerid, accountcrtext);
                costcenterapplicable('Cr');
            }
            $("#imgLoader").css("visibility", "hidden");
            $("#dialog").dialog("close");


        }, 5);

    });
    $("#btnvouchersearch").click(function () {
        bindvoucherhdr();
    });
    //delete voucher
    $("body").on("click", "#gvparentvoucher .gvdel", function () {


        var voucherid;
        var voucherapproived;
        var dayend;
        var row;
        var tdsrelated;

        //$('.debitdel').on('click', function () {

        row = $(this).closest("tr");
        voucherid = row.find('td:eq(0)').html();
        tdsrelated = row.find('td:eq(1)').html();
        dayend = row.find('td:eq(10)').html();

        voucherapproived = row.find('td:eq(9)').html();

        if (confirm("Do you want to delete this voucher?")) {


            $.ajax({

                type: "POST",

                url: "/Accountstran/deletevoucher",

                //data: "{'voucherid': '" + voucherid + "','voucherapproived': '" + voucherapproived + "','dayend': '" + dayend + "','tdsrelated': '" + tdsrelated + "'}",
                data: { voucherid: voucherid, voucherapproived: voucherapproived, dayend: dayend, tdsrelated: tdsrelated },
                //data: {voucherid: voucherid},
                dataType: "json",
                success: function (response) {
                    var flag = '';
                    $.each(response, function () {
                        flag = this['response'];
                    })


                    if (flag == 'Voucher deleted successfully!') {
                        row.remove();
                    }
                    // $("#txtShippingAddress").val(response.d[1]);
                    alert(flag)



                }

            });
        }
    });
    $("body").on("click", "#gvparentvoucher .gvprint", function () {




        //$('.debitdel').on('click', function () {
        var voucherid;
        var depoid;
        vouchertypeid = $('#hdnvoucherid').val();

        var row = $(this).closest("tr");
        voucherid = row.find('td:eq(0)').html();
        depoid = $('#hdndepoid').val();
        // var url = "frmRptInvoicePrint.aspx?AdvRecpt_ID=" + voucherid + "&Voucherid=" + vouchertypeid;
        // var url = "http://localhost:2319/VIEW/frmRptInvoicePrintv2.aspx?AdvRecpt_ID=" + voucherid + "&Voucherid=" + vouchertypeid + "&depoid=" + depoid;
        var url = "http://mcnroeerp.com/mcworld/VIEW/frmRptInvoicePrintv2.aspx?AdvRecpt_ID=" + voucherid + "&Voucherid=" + vouchertypeid + "&depoid=" + depoid;
        window.open(url, "", "toolbar=yes,scrollbars=yes,resizable=yes,top=500,left=500,width=700,height=600");








    });
    $("body").on("click", "#gvparentvoucher .gvedit", function () {
        var voucherid;
        var voucherapproived;
        var dayend;
        var row;
        var tdsrelated;
        row = $(this).closest("tr");
        voucherid = row.find('td:eq(0)').html();
        tdsrelated = row.find('td:eq(1)').html();
        dayend = row.find('td:eq(10)').html();

        voucherapproived = row.find('td:eq(9)').html();
        $("#hdn_accid").val(voucherid);
        $("#hdn_approved").val(voucherapproived);
        $("#hdn_dayend").val(dayend);
        editrec(voucherid);

    })
    $('#txtDCJ_TaxableValuenew').change(function () {
        var taxable = $('#txtDCJ_Taxablenew').val();
        if (taxable == '') {
            $("#txtDCJ_Taxablenew").val('');
        }
        else {
            $("#txtDCJ_Taxablenew").val(taxable);

        }

        Cal_GST_Tax(taxable);
    })

    $('#txtDCJ_Taxablenew').change(function () {
        var taxable = $('#txtDCJ_Taxablenew').val();
        if (taxable != '0') {
            if ($("#txtDCJ_TaxType1").val() != '') {
                $("#txtDCJ_Taxable1").val(taxable);
            }
            else {
                $("#txtDCJ_Taxable1").val('');
            }
            Cal_GST_Tax(taxable);
        }
    })
    $('#txtRoundOff').change(function () {

        if ($("#txtRoundOff").val() == '') {
            $("#txtRoundOff").val('0');


        }
        else {
            var roundoff = $("#txtRoundOff").val();
            if (roundoff > 5) {
                toastr.info('Round Off can not greater than 5.00 ...!');
            }
        }
        var taxable = $('#txtDCJ_Taxablenew').val();
        Cal_GST_Tax(taxable);
    })

    //for invoice
    $("body").on("change", "#grd_InvoiceDetails .clstxtamtpaid", function () {
        var row = $(this).closest("tr");
        var chkinv = row.find("#chkinv");
        var txtamtpaid = row.find("#txtamtpaid");
        var paidamt = row.find("#txtamtpaid").val();
        if (paidamt == '') {
            txtamtpaid.val('0');
            chkinv.prop('checked', false);
        }
        var _paidamt = parseFloat(paidamt);
        if (_paidamt > 0) {
            chkinv.prop('checked', true);
        }
        else
            if (_paidamt <= 0) {
                chkinv.prop('checked', false);
            }
        calcinvoiceamt();
    })
    $("body").on("change", "#grd_InvoiceDetails .clschkinv", function () {
        var row = $(this).closest("tr");
        var chkinv = row.find("#chkinv").is(":checked");
        var txtamtpaid = row.find("#txtamtpaid");
        var paidamt = row.find("#txtamtpaid").val();


        var remainingamt = row.find('td:eq(14)').html();
        if (chkinv == true) {
            if (paidamt != remainingamt) {
                txtamtpaid.val(remainingamt);
            }
        }
        else
            if (chkinv == false) {
                txtamtpaid.val('0');
            }
        calcinvoiceamt();
    })

    $('#txtDCJ_NonTaxableAmount').change(function () {
        var totalgst = 0;
        var netamt = 0;

        $('#grdgstnew tbody tr').each(function () {
            totalgst = totalgst + parseFloat($(this).find('td').eq(9).text());

        })
        netamt = totalgst + parseFloat($("#txtDCJ_NonTaxableAmount").val());

        $("#txtGSTTotal").val(netamt);

    })

    $("#btnpaidamt").click(function () {
        var paidamt = parseFloat($('#txtpaidamount').val().trim());
        var drcramt = parseFloat($('#txtdebittotalamount').val().trim());

        if (paidamt > drcramt) {
            toastr.warning('Adjustment amount!' + paidamt + ' cannot be greater than paid/receipt amount');
            return false;
        }
        if (paidamt = drcramt) {
            toastr.info('You have  fully adjusted paid/receipt amount' + drcramt + ' Thank you');

        }
        if (paidamt < drcramt) {
            toastr.info('You have not fully adjusted paid/receipt amount' + drcramt);

        }
    })
    $("#btnpaidamountclear").click(function () {
        var rows = $('grd_InvoiceDetails>tbody>tr');
        //for (var i = 0; i < rows.length; i++) {
        //    var cells = rows.eq(i).children('td');


        //    var chkinv = cells.eq(2).find('input');
        //    chkinv.prop('checked', false); 

        //}
        // alert('tt');
        $('#grd_InvoiceDetails').find('input:checkbox').prop('checked', false);
        $('#grd_InvoiceDetails').find('input:text').val('0');
        $('#txtpaidamount').val('0');
    })
    //auto voucher
    if ($('#hdnAUTOOPEN').val() == 'TRUE' && $('#hdn_mode').val() == 'A') {
        addnew();
        var autovoucherid = $('#hdnAUTOVOUCHERID').val()
        bindautovouchdercrside(autovoucherid);
    }

    $("body").on("change", "#tdslist .nontax", function () {

        var row = $(this).closest("tr");
        var nontaxableamt = parseFloat(row.find("#txtNonTaxableAmount").val());
        var transactionamount = parseFloat(row.find("#txtTransactionAmount").val());
        var defaultamt = row.find("#txtByDefault").val();
        var billamt = transactionamount - nontaxableamt;
        var amt = parseFloat(defaultamt);
        var taxid = row.find('td:eq(0)').html();
        var trantype = $("#hdn_drcr").val();
        var amtdr;

        var accdrvalue;
        var accountdrtext;
        var region = $("#ddlregion").val();
        var voucherdate = $('#txtvoucherdate').val();
        var voucherid = $('#hdnvoucherid').val();
        //   var deptvalue = $("#ddldepartmentBud").val();
        if (trantype == 'Dr') {

            accdrvalue = $("#ddlAccTypeDr").val();
            accountdrtext = $('#ddlAccTypeDr').find('option:selected').text();
            $("#hdnDrCrTDS").val('Dr');
            // var depttext = $('#ddldepartmentBud').find('option:selected').text();



        }
        else
            if (trantype == 'Cr') {
                accdrvalue = $("#ddlAcctypeCr").val();
                accountdrtext = $('#ddlAcctypeCr').find('option:selected').text();
                $("#hdnDrCrTDS").val('Cr');


                var drcr = 'Cr';
                amtdr = $('#txtAmntCr').val();
            }


        ////var row = $("[id*=GridViewBudget] tr:last").clone();
        //   $("td:nth-child(1)", row).html(accdrvalue);
        //   $("td:nth-child(2)", row).html(accountdrtext);
        //   $("td:nth-child(3)", row).html(deptvalue);
        //    $("td:nth-child(4)", row).html(depttext);
        //   $("[id*=GridViewBudget] tbody").append(row);
        $('#myModaltds').css("display", "");
        setTimeout(function () {

            $.ajax({

                type: "POST",

                // url: "<%=ServiceURL%>Accounts.asmx/TaxApplicable",
                url: "/Accountstran/TaxApplicable",
                //  data: "{'AccountID': '" + accdrvalue + "', 'AccountName': '" + accountdrtext + "','AccountType': '" + AccountType + "','Amount': '" + amtdr + "','BranchID': '" + region + "','voucherdate': '" + voucherdate + "','voucherid': '" + voucherid + "','drcr': '" + trantype + "'}",
                data: { AccountID: accdrvalue, AccountName: accountdrtext, AccountType: amt, Amount: billamt, BranchID: region, voucherdate: voucherdate, voucherid: voucherid, drcr: trantype, finyr: FINYEAR },
                // data: "{'AccountID': '" + accdrvalue + "', 'AccountName': '" + accountdrtext + "','AccountType': '" + AccountType + "','Amount': '" + amtdr + "','BranchID': '" + region + "','voucherdate': '" + voucherdate + "','voucherid': '" + voucherid + "','drcr': '" + trantype + "'}",

                dataType: "json",
                success: function (response) {





                    $.each(response, function () {

                        if (taxid == this["ID"]) {
                            row.find('td:eq(7)').html(this["TDSAMOUNT"]);
                            row.find('td:eq(5)').html(this["DEDUCTABLEAMOUNT"]);

                        }


                    });
                }














            })
            $('#myModaltds').css("display", "none");
        }, 5);
        e.preventDefault();
    })
})
function bindautovouchdercrside(autovoucherid) {
    $.ajax({

        type: "POST",
        url: "/Accountstran/Bindautovoucher",
        // data: "{'AccountID': '" + AccountID + "','AccountName': '" + AccountName + "','DebitAmount': '" + DebitAmount + "','DeductableAmount': '" + DeductableAmount + "','DeductablePercent': '" + DeductablePercent + "','DeductableLedgerID': '" + DeductableLedgerID + "','IsCostCenter': '" + IsCostCenter + "','IsTagInvoice': '" + IsTagInvoice + "','Isautoposting': '" + Isautoposting + "','NonTaxableAmount': '" + NonTaxableAmount + "','ByDefaultAmount': '" + ByDefaultAmount + "','TransactionAmount': '" + TransactionAmount + "','remarks': '" + remarks + "','departmentid': '" + departmentid + "','departmentname': '" + departmentname + "'}",
        data: { autovoucherid: autovoucherid },
        //data: "{'ledgerid': '" + ledgerid + "','CostCatagoryID': '" + CostCatagoryID + "','BrandId': '" + BrandId + "','ProductId': '" + ProductId + "','DepartmentId': '" + DepartmentId + "'}",
        // contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var totalcredit = 0;
            var tableNew;
            var iscostcenter;

            $.each(response, function () {

                iscostcenter = this["IsCostCenter"];

                if (VOUCHERID == '2') {
                    if (iscostcenter == 'Y') {
                        tableNew = "<tr><td style='display:none'>" + this["LedgerId"] + "</td>   <td style='width:60%'>" + this["LedgerName"] + "</td><td style='display:none'>" + this["DepartmentID"] + "</td><td >" + this["DepartmentName"] + "</td><td style='display:none'>" + this["BusinessSegId"] + "</td><td >" + this["BusinessSegName"] + "</td> <td style='width:30%;text-align:right'>" + this["Amount"] + "</td><td style='display:none'>" + this["IsCostCenter"] + "</td> <td style='width:10%' ><input type='button' id='btndebitgrddelete'  class='debitdel' value='Del'/></td><td style='width:10%' ><input type='button' id='btndebitgrddelete'  class='clscost' value='CostCenter'/></td><td style='display:none'>" + DeductableLedgerID + "</td></tr> ";
                    }
                    else
                        if (iscostcenter == 'N') {


                            tableNew = "<tr><td style='display:none'>" + this["LedgerId"] + "</td>   <td style='width:60%'>" + this["LedgerName"] + "</td><td style='display:none'>" + this["DepartmentID"] + "</td><td >" + this["DepartmentName"] + "</td><td style='display:none'>" + this["BusinessSegId"] + "</td><td >" + this["BusinessSegName"] + "</td><td style='width:30%;text-align:right'>" + this["Amount"] + "</td><td style='display:none'>" + this["IsCostCenter"] + "</td> <td style='width:10%' ><input type='button' id='btndebitgrddelete'  class='debitdel' value='Del'/></td><td style='width:10%' ><input type='button' id='btndebitgrddelete'  class='clscost' value='No CostCenter'/></td><td style='display:none'>" + this["IsCostCenter"] + "</td></tr> ";


                        }

                }

                totalcredit = totalcredit + parseFloat(this["Amount"]);

            })

            $("#creditadd").append(tableNew);

            $("#txtcredittotalamount").val(totalcredit);



        }
    });


}
function calcinvoiceamt() {
    var _paidamt;
    var totalinvoiceamt = 0;
    $('#grd_InvoiceDetails tbody tr').each(function () {


        $(this).find('input').each(function () {
            //totalccamttemp = row.find("#txtTransactionAmount").val();
            //totalccamttemp=row.find("#txtTransactionAmount").val();
            _paidamt = $(this).val();

            if (_paidamt != '') {
                totalinvoiceamt = totalinvoiceamt + parseFloat(_paidamt);
            }
            //totalccamt = totalccamt + parseFloat($(this).find('td:eq(15)').html());
        })



    })

    $('#txtpaidamount').val(totalinvoiceamt);


}
function addnew() {
    $("#dialog").dialog({
        autoOpen: true,
        modal: true,
        title: "Loading.."
    });
    $("#imgLoader").css("visibility", "visible");

    setTimeout(function () {

        ReleaseSession();

        $('#pnlDisplay').css("display", "none");
        $('#btndivsave').css("display", "");
        $('#trvoucherno').css("display", "none");

        $('#debitadd').find('tbody').detach();
        $('#debitadd').append($('<tbody>'));

        $('#creditadd').find('tbody').detach();
        $('#creditadd').append($('<tbody>'));
        $('#pnlAdd').css("display", "block");
        $("#ddlvouchertype").prop("disabled", true);
        $("#ddlregion").prop("disabled", false);

        $("#ddlregion").chosen({
            search_contains: true
        });


        $("#ddlregion").trigger("chosen:updated");
        BindVoucher();
        BindVoucherType();
        if (VOUCHERID == '2') {
            $('#trpaymentmode').css("display", "none");

            $('#ddlMode').val('C');
        }
        $("#ddlvouchertype").chosen({
            search_contains: true
        });


        if ($('#hdnmtclaim').val() == 'Y') {
            $("#ddlGSTVoucherType").val('GSTVoucher');
            $('#btnVendorDetails').css("display", "");
            $("#ddlGSTVoucherType").prop("disabled", true);
        }
        else {
            $('#btnVendorDetails').css("display", "none");
            $("#ddlGSTVoucherType").prop("disabled", false);
        }


        $("#ddlGSTVoucherType").chosen({
            search_contains: true
        });

        $("#ddlGSTVoucherType").trigger("chosen:updated");



        $("#ddlMode").chosen({
            search_contains: true
        });

        $("#ddlMode").trigger("chosen:updated");
        $("#ddlvouchertype").trigger("chosen:updated");
        $("#txtvoucherdate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());

        $('#isgstpaid').val('N');
        if ($('#grdgstnew').length) {
            $("#grdgstnew").remove();
        }
        $("#imgLoader").css("visibility", "hidden");
        $("#dialog").dialog("close");


    }, 5);
    e.preventDefault();



}
function bindregion() {
    var BRID = $("#BRID");
    var ddlregion = $("#ddlregion");
    var ddlbranch = $("#ddlbranch");
    $("#dialog").dialog({
        autoOpen: true,
        modal: true,
        title: "Loading.."
    });
    $("#imgLoader").css("visibility", "visible");
    setTimeout(function () {
        $.ajax({
            type: "POST",
            url: "/Accountstran/BindRegion",
            data: { userid: UserID, usertype: usertype },
            //data: '{}',
            async: false,
            dataType: "json",

            success: function (response) {
                //alert(JSON.stringify(response));
                BRID.empty();
                ddlregion.empty();
                //.append('<option selected="selected" value="0">Select Brancht</option>');;
                $.each(response, function () {
                    BRID.append($("<option></option>").val(this['BRID']).html(this['BRNAME']));
                    ddlregion.append($("<option></option>").val(this['BRID']).html(this['BRNAME']));
                    ddlbranch.append($("<option></option>").val(this['BRID']).html(this['BRNAME']));
                });
                var depoid = $('#ddlregion').val();
                $("#hdndepoid").val(depoid);



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
function BindVoucher() {
    var ddlvouchertype = $("#ddlvouchertype");
    $.ajax({
        type: "POST",
        url: "/Accountstran/BindVoucherType",
        data: '{}',
        async: false,
        dataType: "json",

        success: function (response) {
            //alert(JSON.stringify(response));
            ddlvouchertype.empty();
            //.append('<option selected="selected" value="0">Select Brancht</option>');;
            $.each(response, function () {
                ddlvouchertype.append($("<option></option>").val(this['ID']).html(this['VoucherName']));
            });
            var voucherid = $("#hdnvoucherid").val();


            $("#ddlvouchertype").val(voucherid);
            var vc = $("#ddlvouchertype").val();
            var region = $("#ddlregion").val();

            if ($('#hdnmtclaim').val() == 'Y') {
                BindAccountTypedr_MTClaim(vc, region);
                BindAccountTypecr_MTClaim(vc, region);
            }
            else {
                BindAccountTypedr(vc, region);
                BindAccountTypecr(vc, region);
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
function BindAccountTypedr(vc, region) {
    var ddlAccTypeDr = $("#ddlAccTypeDr");
    var ddlapprovalledger = $("#ddlapprovalledger");
    $.ajax({
        type: "POST",
        url: "/Accountstran/BindAccountTypedr",
        data: { VoucherType: vc, RegionId: region },
        async: false,
        dataType: "json",
        success: function (response) {
            //alert(JSON.stringify(response));
            ddlAccTypeDr.empty().append('<option selected="selected" value="0">Select Account</option>');
            $.each(response, function () {
                ddlAccTypeDr.append($("<option></option>").val(this['ID']).html(this['NAME']));
            });
            $("#ddlAccTypeDr").chosen({
                search_contains: true
            });


            // For MT Claim Approval Ledger
            ddlapprovalledger.empty().append('<option selected="selected" value="0">Select Transfer to Ledger</option>');
            $.each(response, function () {
                ddlapprovalledger.append($("<option></option>").val(this['ID']).html(this['NAME']));
            });
            $("#ddlapprovalledger").chosen({
                search_contains: true
            });

            $("#ddlapprovalledger").trigger("chosen:updated");

            $("#ddlAccTypeDr").trigger("chosen:updated");
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}
function BindAccountTypecr(vc, region) {
    var ddlAcctypeCr = $("#ddlAcctypeCr");

    $.ajax({
        type: "POST",
        url: "/Accountstran/BindAccountTypecr",
        data: { VoucherType: vc, RegionId: region },
        async: false,
        dataType: "json",
        success: function (response) {
            //alert(JSON.stringify(response));
            ddlAcctypeCr.empty().append('<option selected="selected" value="0">Select Account</option>');
            $.each(response, function () {
                ddlAcctypeCr.append($("<option></option>").val(this['ID']).html(this['NAME']));
            });
            $("#ddlAcctypeCr").chosen({
                search_contains: true
            });

            $("#ddlAcctypeCr").trigger("chosen:updated");
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function BindAccountTypecr_MTClaim(vc, region) {
    var ddlAcctypeCr = $("#ddlAcctypeCr");

    $.ajax({
        type: "POST",
        url: "/Accountstran/BindAccountTypecr_MTClaim",
        data: { VoucherType: vc, RegionId: region },
        async: false,
        dataType: "json",
        success: function (response) {
            //alert(JSON.stringify(response));
            ddlAcctypeCr.empty().append('<option selected="selected" value="0">Select Account</option>');
            $.each(response, function () {
                ddlAcctypeCr.append($("<option></option>").val(this['ID']).html(this['NAME']));
            });
            $("#ddlAcctypeCr").chosen({
                search_contains: true
            });


            $("#ddlAcctypeCr").trigger("chosen:updated");
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function BindAccountTypedr_MTClaim(vc, region) {
    var ddlAcctypeDr = $("#ddlAcctypeDr");

    $.ajax({
        type: "POST",
        url: "/Accountstran/BindAccountTypedr_MTClaim",
        data: { VoucherType: vc, RegionId: region },
        async: false,
        dataType: "json",
        success: function (response) {
            //alert(JSON.stringify(response));
            ddlAcctypeDr.empty().append('<option selected="selected" value="0">Select Account</option>');
            $.each(response, function () {
                ddlAcctypeDr.append($("<option></option>").val(this['ID']).html(this['NAME']));
            });
            $("#ddlAcctypeDr").chosen({
                search_contains: true
            });


            $("#ddlAcctypeDr").trigger("chosen:updated");
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function BindVoucherType() {
    var listItems = "<option value='NormalVoucher'>Normal</option>";
    listItems += "<option value='GSTVoucher'>GST</option>";
    listItems += "<option value='GSTGovtVoucher'>GST Govt</option>";

    $("#ddlGSTVoucherType").html(listItems);
    var vgst = $("#ddlGSTVoucherType").val();
    // $("#hdnvouchertypegst").val(vgst);
    //$("#hdngstvouchertype").val(vgst);


}
function close() {
    $('#pnlAdd').css("display", "none");

    $('#pnlDisplay').css("display", "block");
    // $("#btnaddhide").css("display", "block");
    $("#txtAmtDr").val('');
    $("#txtAmntCr").val('');
    //for other details
    $("#txtbillno").val('');
    $("#txtbilldate").val('');
    $("#txtgrno").val('');
    $("#txtgrdate").val('');
    $("#txtvehicleno").val('');
    $("#txttransport").val('');
    $("#txtwaybillno").val('');

    $("#txtwaybilldate").val('');
    $("#txtNarration").val('');
    $("#txtdebittotalamount").val('0');
    $("#hdn_totaldr").val('0');
    $("#hdn_totalcr").val('0');

    $("#txtcredittotalamount").val('0');

    $('#divVendorDetails').css("display", "none");
    $('#trinvoicedetails').css("display", "none");

    //bindvoucherhdr();
    $('#lbldebitoutstanding').text('0.00');
    $('#lblcreditoutstanding').text('0.00');

    $('#trDeptDr').css("display", "none");
    $('#trDeptCr').css("display", "none");
    bindvoucherhdr();
    $("#chkfileupload").prop('checked', false);
    $('#btnshow').css("display", "none");
    $("#ddlDepartmentDr").val('0');
    $("#ddlDepartmentCr").val('0');
    $("#ddlBusinessSegDr").val('0');
    $("#ddlBusinessSegCr").val('0');
    $("#ddlDepartmentDr").chosen({
        search_contains: true
    });


    $("#ddlDepartmentDr").trigger("chosen:updated");
    $("#ddlBusinessSegDr").chosen({
        search_contains: true
    });


    $("#ddlBusinessSegDr").trigger("chosen:updated");

    $("#ddlDepartmentCr").chosen({
        search_contains: true
    });


    $("#ddlDepartmentCr").trigger("chosen:updated");

    $("#ddlBusinessSegCr").chosen({
        search_contains: true
    });


    $("#ddlBusinessSegCr").trigger("chosen:updated");
    $("#ddlStateNamenew").prop("disabled", false);
    $("#ddlToStateName").prop("disabled", false);
    $("#ddlDCJ_GroupName").prop("disabled", false);
    $("#ddlDCJ_Partynew").prop("disabled", false);
    $("#ddlDCJ_GstNonew").prop("disabled", false);

    $("#ddlDCJ_Partynew").empty();

    $("#ddlDCJ_GstNonew").empty();
    if ($('#grdgstnew').length) {
        $("#grdgstnew").remove();
    }

}
function ReleaseSession() {

    $.ajax({
        type: "POST",
        url: "/Accountstran/RemoveSession",
        data: '{}',
        dataType: "json",
        success: function (response) {


        }
    });
}
//for debit add
function adddr() {

    var totaldebit = 0;
    var amtdr = $('#txtAmtDr').val();
    //totaldebit = parseFloat($('#<%=hdn_totaldr.ClientID%>').val());

    //totaldebit = totaldebit + parseFloat(amtdr);

    //$("#txtdebittotalamount").val(totaldebit);
    //$("#hdn_totaldr").val(totaldebit);

    var accdrvalue = $("#ddlAccTypeDr").val();
    var accountdrtext = $('#ddlAccTypeDr').find('option:selected').text();
    var DeductableAmount = $('#txtAmtDr').val();;
    var DeductablePercent = '0';
    var DeductableLedgerID = '';
    var IsCostCenter = $("#hdn_costcenterapplicable").val();

    var IsTagInvoice = 'N';
    var Isautoposting = 'N';
    var NonTaxableAmount = '0';
    var ByDefaultAmount = '0';
    var TransactionAmount = '0';
    // var departmentid = $("#ddldepartmentBud").val();
    var departmentid = $("#ddlDepartmentDr").val();
    // var departmentname = $('#ddldepartmentBud').find('option:selected').text();
    var departmentname = $('#ddlDepartmentDr').find('option:selected').text();

    var BusinessSegId = $("#ddlBusinessSegDr").val();
    // var departmentname = $('#ddldepartmentBud').find('option:selected').text();
    var BusinessSegName = $('#ddlBusinessSegDr').find('option:selected').text();
    var remarks = $('#txtdebitremarks').val();

    Debitadd(accdrvalue, accountdrtext, amtdr, DeductableAmount, DeductablePercent, DeductableLedgerID, IsCostCenter, IsTagInvoice, Isautoposting, NonTaxableAmount, ByDefaultAmount, TransactionAmount, remarks, departmentid, departmentname, 'false', BusinessSegId, BusinessSegName);

    //var tableNew = "<tr><td style='display:none'>" + accdrvalue + "</td>   <td style='width:60%'>" + accountdrtext + "</td><td style='width:30%;text-align:right'>" + amtdr + "</td> <td style='width:10%' ><input type='button' id='btndebitgrddelete'  class='debitdel' value='Del'/></td></tr> ";
    // $("#debitadd").append(tableNew);

    $('#txtAmtDr').val('');
    $("#ddlAccTypeDr").val('0');
    $("#ddlDepartmentDr").val('0');
    $("#ddlBusinessSegDr").val('0');
    $("#ddlAccTypeDr").chosen({
        search_contains: true
    });


    $("#ddlAccTypeDr").trigger("chosen:updated");
    $("#ddlDepartmentDr").chosen({
        search_contains: true
    });


    $("#ddlDepartmentDr").trigger("chosen:updated");
    $("#ddlBusinessSegDr").chosen({
        search_contains: true
    });


    $("#ddlBusinessSegDr").trigger("chosen:updated");
    $('#lbldebitoutstanding').val('0.00');
    $('#trDeptDr').css("display", "none");
    isexpenseledger = '0';

}

function Debitadd(AccountID, AccountName, DebitAmount, DeductableAmount, DeductablePercent, DeductableLedgerID, IsCostCenter, IsTagInvoice, Isautoposting, NonTaxableAmount, ByDefaultAmount, TransactionAmount, remarks, departmentid, departmentname, taxapplicable, BusinessSegId, BusinessSegName) {

    $("#dialog").dialog({
        autoOpen: true,
        modal: true,
        title: "Loading.."
    });
    $("#imgLoader").css("visibility", "visible");

    setTimeout(function () {
        $.ajax({

            type: "POST",
            url: "/Accountstran/addvoucherdrcr",
            // data: "{'AccountID': '" + AccountID + "','AccountName': '" + AccountName + "','DebitAmount': '" + DebitAmount + "','DeductableAmount': '" + DeductableAmount + "','DeductablePercent': '" + DeductablePercent + "','DeductableLedgerID': '" + DeductableLedgerID + "','IsCostCenter': '" + IsCostCenter + "','IsTagInvoice': '" + IsTagInvoice + "','Isautoposting': '" + Isautoposting + "','NonTaxableAmount': '" + NonTaxableAmount + "','ByDefaultAmount': '" + ByDefaultAmount + "','TransactionAmount': '" + TransactionAmount + "','remarks': '" + remarks + "','departmentid': '" + departmentid + "','departmentname': '" + departmentname + "'}",
            data: { AccountID: AccountID, AccountName: AccountName, DebitAmount: DebitAmount, DeductableAmount: DeductableAmount, DeductablePercent: DeductablePercent, DeductableLedgerID: DeductableLedgerID, IsCostCenter: IsCostCenter, IsTagInvoice: IsTagInvoice, Isautoposting: Isautoposting, NonTaxableAmount: NonTaxableAmount, ByDefaultAmount: ByDefaultAmount, TransactionAmount: TransactionAmount, remarks: remarks, departmentid: departmentid, departmentname: departmentname, BusinessSegId: BusinessSegId, BusinessSegName: BusinessSegName },
            //data: "{'ledgerid': '" + ledgerid + "','CostCatagoryID': '" + CostCatagoryID + "','BrandId': '" + BrandId + "','ProductId': '" + ProductId + "','DepartmentId': '" + DepartmentId + "'}",
            // contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var totaldebit = 0;
                var tableNew;
                var iscostcenter;
                $.each(response, function () {
                    iscostcenter = this["IsCostCenter"];

                    if (VOUCHERID == '2') {
                        if (iscostcenter == 'Y') {
                            tableNew = "<tr><td style='display:none'>" + this["LedgerId"] + "</td>   <td style='width:60%'>" + this["LedgerName"] + "</td><td style='display:none'>" + this["DepartmentID"] + "</td><td >" + this["DepartmentName"] + "</td><td style='display:none'>" + this["BusinessSegId"] + "</td><td >" + this["BusinessSegName"] + "</td> <td style='width:30%;text-align:right'>" + this["Amount"] + "</td><td style='display:none'>" + this["IsCostCenter"] + "</td> <td style='width:10%' ><input type='button' id='btndebitgrddelete'  class='debitdel' value='Del'/></td><td style='width:10%' ><input type='button' id='btndebitgrddelete'  class='clscost' value='CostCenter'/></td><td style='display:none'>" + DeductableLedgerID + "</td></tr> ";
                        }
                        else
                            if (iscostcenter == 'N') {

                                tableNew = "<tr><td style='display:none'>" + this["LedgerId"] + "</td>   <td style='width:60%'>" + this["LedgerName"] + "</td><td style='display:none'>" + this["DepartmentID"] + "</td><td >" + this["DepartmentName"] + "</td><td style='display:none'>" + this["BusinessSegId"] + "</td><td >" + this["BusinessSegName"] + "</td><td style='width:30%;text-align:right'>" + this["Amount"] + "</td><td style='display:none'>" + this["IsCostCenter"] + "</td> <td style='width:10%' ><input type='button' id='btndebitgrddelete'  class='debitdel' value='Del'/></td><td style='width:10%' ><input type='button' id='btndebitgrddelete'  class='clscost' value='No CostCenter'/></td><td style='display:none'>" + DeductableLedgerID + "</td></tr> ";
                            }

                    }
                    totaldebit = totaldebit + parseFloat(this["Amount"]);
                })
                $("#txtdebittotalamount").val(totaldebit);
                $("#hdn_totaldr").val(totaldebit);
                $("#debitadd").append(tableNew);


            }
        });
        $("#imgLoader").css("visibility", "hidden");
        $("#dialog").dialog("close");


    }, 5);
}
function binddr() {
    $.ajax({

        type: "POST",
        url: "/Accountstran/BindvoucherDr",

        data: '{}',
        //data: "{'ledgerid': '" + ledgerid + "','CostCatagoryID': '" + CostCatagoryID + "','BrandId': '" + BrandId + "','ProductId': '" + ProductId + "','DepartmentId': '" + DepartmentId + "'}",

        dataType: "json",
        success: function (response) {
            var totaldebit = 0;
            var tableNew;
            var iscostcenter;
            $.each(response, function () {
                iscostcenter = this["IsCostCenter"];
                if (VOUCHERID == '2') {
                    if (iscostcenter == 'Y' || iscostcenter == 'y') {
                        // tableNew = tableNew + "<tr><td style='display:none'>" + this["LedgerId"] + "</td>   <td style='width:60%'>" + this["LedgerName"] + "</td><td style='width:30%;text-align:right'>" + this["Amount"] + "</td><td style='display:none'>" + this["IsCostCenter"] + "</td> <td style='width:10%' ><input type='button' id='btndebitgrddelete'  class='debitdel' value='Del'/></td><td style='width:10%' ><input type='button' id='btndebitgrddelete'  class='clscost' value='CostCenter'/></td><td style='display:none'>" + this["DeductableLedgerId"] + "</td></tr> ";
                        tableNew = tableNew + "<tr><td style='display:none'>" + this["LedgerId"] + "</td>   <td style='width:60%'>" + this["LedgerName"] + "</td><td style='display:none'>" + this["DepartmentID"] + "</td><td >" + this["DepartmentName"] + "</td><td style='display:none'>" + this["BusinessSegId"] + "</td><td >" + this["BusinessSegName"] + "</td> <td style='width:30%;text-align:right'>" + this["Amount"] + "</td><td style='display:none'>" + this["IsCostCenter"] + "</td> <td style='width:10%' ><input type='button' id='btndebitgrddelete'  class='debitdel' value='Del'/></td><td style='width:10%' ><input type='button' id='btndebitgrddelete'  class='clscost' value='CostCenter'/></td><td style='display:none'>" + this["DeductableLedgerId"] + "</td></tr> ";
                    }
                    else
                        if (iscostcenter == 'N') {

                            tableNew = tableNew + "<tr><td style='display:none'>" + this["LedgerId"] + "</td>   <td style='width:60%'>" + this["LedgerName"] + "</td><td style='display:none'>" + this["DepartmentID"] + "</td><td >" + this["DepartmentName"] + "</td><td style='display:none'>" + this["BusinessSegId"] + "</td><td >" + this["BusinessSegName"] + "</td> <td style='width:30%;text-align:right'>" + this["Amount"] + "</td><td style='display:none'>" + this["IsCostCenter"] + "</td> <td style='width:10%' ><input type='button' id='btndebitgrddelete'  class='debitdel' value='Del'/></td><td style='width:10%' ><input type='button' id='btndebitgrddelete'  class='clscost' value='No CostCenter'/></td><td style='display:none'>" + this["DeductableLedgerId"] + "</td></tr> ";
                        }
                    ;
                }
                totaldebit = totaldebit + parseFloat(this["Amount"]);
                //  tableNew = "<tr><td style='display:none'>" + this["LedgerId"] + "</td>   <td style='width:60%'>" + this["LedgerName"] + "</td><td style='width:30%;text-align:right'>" + this["Amount"] + "</td><td style='display:none'>" + this["IsCostCenter"] + "</td> <td style='width:10%' ><input type='button' id='btndebitgrddelete'  class='debitdel' value='Del'/></td><td style='width:10%' ><input type='button' id='btndebitgrddelete'  class='clscost' value='CostCenter'/></td><td style='display:none'>" + DeductableLedgerID + "</td></tr> ";

            })
            $("#txtdebittotalamount").val(totaldebit);
            //  $("#hdn_totaldr").val(totaldebit);
            $("#debitadd").append(tableNew);

        }
    });

}
function debitdel(ledgerid) {
    var cashbank = $("#ddlMode").val();
    $.ajax({

        type: "POST",

        url: "/Accountstran/deletedr",

        data: "{'ledgerid': '" + ledgerid + "'}",
        contentType: "application/json; charset=utf-8",
        success: function (response) {

            var tableNew;
            var totaldebit = 0;
            var iscostcenter;
            $.each(response, function () {
                iscostcenter = this["IsCostCenter"];

                //if (VOUCHERID == '2' || VOUCHERID == '15' ) {
                //    alert('isadded');
                //        if (iscostcenter == 'N') {
                //            tableNew = "<tr><td style='display:none'>" + this["LedgerId"] + "</td>   <td style='width:60%'>" + this["LedgerName"] + "</td><td style='width:30%'><input type='text' id='txtamtdtgrd' class='gramtdr'  value='" + this["Amount"] + "' style='width:40px;text-align:right'/> </td><td style='display:none'>" + this["IsCostCenter"] + "</td> <td style='width:10%' ><input type='button' id='btndebitgrddelete'  class='debitdel' value='Del'/></td><td style='width:10%' ><input type='button' id='btndebitgrddelete'  class='clscost' value='No CostCenter'/></td><td style='display:none'>" + this["DeductableLedgerId"] + "</td></tr> ";
                //        }
                //        else
                //            if (iscostcenter == 'Y') {
                //                tableNew = "<tr><td style='display:none'>" + this["LedgerId"] + "</td>   <td style='width:60%'>" + this["LedgerName"] + "</td><td style='width:30%'><input type='text' id='txtamtdtgrd' disabled class='gramtdr' disabled value='" + this["Amount"] + "' style='width:40px;text-align:right'/> </td><td style='display:none'>" + this["IsCostCenter"] + "</td> <td style='width:10%' ><input type='button' id='btndebitgrddelete'  class='debitdel' value='Del'/></td><td style='width:10%' ><input type='button' id='btndebitgrddelete'  class='clscost' value='CostCenter'/></td><td style='display:none'>" + this["DeductableLedgerId"] + "</td></tr> ";
                //            }
                //    }

                //if (VOUCHERID == '16' && cashbank == 'C') {
                //      if (iscostcenter == 'N') {
                //            tableNew = "<tr><td style='display:none'>" + this["LedgerId"] + "</td>   <td style='width:60%'>" + this["LedgerName"] + "</td><td style='width:30%'><input type='text' id='txtamtdtgrd' class='gramtdr'  value='" + this["Amount"] + "' style='width:40px;text-align:right'/> </td><td style='display:none'>" + this["IsCostCenter"] + "</td> <td style='width:10%' ><input type='button' id='btndebitgrddelete'  class='debitdel' value='Del'/></td><td style='width:10%' ><input type='button' id='btndebitgrddelete'  class='clscost' value='No CostCenter'/></td><td style='display:none'>" + this["DeductableLedgerId"] + "</td></tr> ";
                //        }
                //        else
                //            if (iscostcenter == 'Y') {
                //                tableNew = "<tr><td style='display:none'>" + this["LedgerId"] + "</td>   <td style='width:60%'>" + this["LedgerName"] + "</td><td style='width:30%'><input type='text' id='txtamtdtgrd' disabled class='gramtdr' disabled value='" + this["Amount"] + "' style='width:40px;text-align:right'/> </td><td style='display:none'>" + this["IsCostCenter"] + "</td> <td style='width:10%' ><input type='button' id='btndebitgrddelete'  class='debitdel' value='Del'/></td><td style='width:10%' ><input type='button' id='btndebitgrddelete'  class='clscost' value='CostCenter'/></td><td style='display:none'>" + this["DeductableLedgerId"] + "</td></tr> ";
                //            }
                //}


                //if (VOUCHERID == '16' && cashbank == 'B') {

                //            if (iscostcenter == 'N') {
                //                tableNew = "<tr><td style='display:none'>" + this["LedgerId"] + "</td>   <td style='width:60%'>" + this["LedgerName"] + "</td><td style='width:30%'><input type='text' id='txtamtdtgrd' class='gramtdr'  value='" + this["Amount"] + "' style='width:40px;text-align:right'/> </td><td style='display:none'>" + this["IsCostCenter"] + "</td><td >" + this["PAYMENTTYPENAME"] + "</td> <td >" + this["ChequeNo"] + "</td><td >" + this["ChequeDate"] + "</td> <td style='width:10%' ><input type='button' id='btndebitgrddelete'  class='debitdel' value='Del'/></td><td style='width:10%' ><input type='button' id='btndebitgrddelete'  class='clscost' value='No CostCenter'/></td><td style='display:none'>" + this["DeductableLedgerId"] + "</td></tr> ";
                //            }
                //            else
                //                if (iscostcenter == 'Y') {
                //                    tableNew = "<tr><td style='display:none'>" + this["LedgerId"] + "</td>   <td style='width:60%'>" + this["LedgerName"] + "</td><td style='width:30%'><input type='text' id='txtamtdtgrd' disabled class='gramtdr' disabled value='" + this["Amount"] + "' style='width:40px;text-align:right'/> </td><td style='display:none'>" + this["IsCostCenter"] + "</td><td >" + this["PAYMENTTYPENAME"] + "</td> <td >" + this["ChequeNo"] + "</td><td >" + this["ChequeDate"] + "</td> <td style='width:10%' ><input type='button' id='btndebitgrddelete'  class='debitdel' value='Del'/></td><td style='width:10%' ><input type='button' id='btndebitgrddelete'  class='clscost' value='CostCenter'/></td><td style='display:none'>" + this["DeductableLedgerId"] + "</td></tr> ";
                //                }
                //        }

                totaldebit = totaldebit + parseFloat(this["Amount"]);
            })

            $("#txtdebittotalamount").val(totaldebit);

            $("#hdn_totaldr").val(totaldebit);
            $("#debitadd").append(tableNew);
        }
    });

}

//add credit
function addcr() {

    var totalcredit = 0;
    var amtcr = $('#txtAmntCr').val();
    var paymenttypeid = '';
    var chequeno = '';
    var paymenttypename = '';
    var chequedate = '';


    //totalcredit = parseFloat($('#<%=hdn_totalcr.ClientID%>').val());
    //alert(totalcredit);
    //totalcredit = totalcredit + parseFloat(amtcr);

    //$("#txtcredittotalamount").val(totalcredit);
    //$("#hdn_totalcr").val(totalcredit);
    var accdrvalue = $("#ddlAcctypeCr").val();
    var accountdrtext = $('#ddlAcctypeCr').find('option:selected').text();
    var DeductableAmount = $('#txtAmntCr').val();;
    var DeductablePercent = '0';
    var DeductableLedgerID = '';
    var IsCostCenter = $("#hdn_costcenterapplicable").val();

    var IsTagInvoice = 'N';
    var Isautoposting = 'N';
    var NonTaxableAmount = '0';
    var ByDefaultAmount = '0';
    var TransactionAmount = '0';
    // var departmentid = $("#ddldepartmentBud").val();
    var departmentid = $("#ddlDepartmentCr").val();
    // var departmentname = $('#ddldepartmentBud').find('option:selected').text();
    var departmentname = $('#ddlDepartmentCr').find('option:selected').text();

    var BusinessSegId = $("#ddlBusinessSegCr").val();
    // var departmentname = $('#ddldepartmentBud').find('option:selected').text();
    var BusinessSegName = $('#ddlBusinessSegCr').find('option:selected').text();
    isexpenseledger = '0';
    var remarks = $('#txtcreditremarks').val();
    //---will be added in adv payment
    //if (VOUCHERID == '15' || VOUCHERID == '16') {
    //    paymenttypeid = $("#<%= rdbcreditpaymenttype.ClientID %>").val();
    //    paymenttypename = $('#rdbcreditpaymenttype').find('option:selected').text();
    //    chequeno = $("#<%= txtcreditchequeno.ClientID %>").val();
    //    chequedate = $("#<%= txtcreditchequedate.ClientID %>").val();
    //}
    if (VOUCHERID == '15' || VOUCHERID == '16') {
        paymenttypeid = '';
        paymenttypename = '';
    }
    else {
        paymenttypeid = "2";
        paymenttypename = "NEFT";
    }


    Creditadd(accdrvalue, accountdrtext, amtcr, DeductableAmount, DeductablePercent, DeductableLedgerID, IsCostCenter, IsTagInvoice, Isautoposting, NonTaxableAmount, ByDefaultAmount, TransactionAmount, remarks, departmentid, departmentname, 'false', paymenttypeid, paymenttypename, chequeno, chequedate, BusinessSegId, BusinessSegName);


    //var tableNew = "<tr><td style='display:none'>" + accdrvalue + "</td>   <td style='width:60%'>" + accountdrtext + "</td><td style='width:30%;text-align:right'>" + amtdr + "</td> <td style='width:10%' ><input type='button' id='btndebitgrddelete'  class='debitdel' value='Del'/></td></tr> ";
    // $("#debitadd").append(tableNew);

    $('#txtAmntCr').val('');
    $("#ddlAcctypeCr").val('0');
    $("#ddlDepartmentCr").val('0');
    $("#ddlBusinessSegCr").val('0');
    $("#ddlAcctypeCr").chosen({
        search_contains: true
    });


    $("#ddlAcctypeCr").trigger("chosen:updated");
    $("#ddlDepartmentCr").chosen({
        search_contains: true
    });


    $("#ddlDepartmentCr").trigger("chosen:updated");
    $("#ddlBusinessSegCr").chosen({
        search_contains: true
    });


    $("#ddlBusinessSegCr").trigger("chosen:updated");
    $('#lblcreditoutstanding').text('0.00');

    $('#trDeptCr').css("display", "none");
    isexpenseledger = '0';
    //GetTableValuescr();
}
//==delete credit
function Creditadd(AccountID, AccountName, DebitAmount, DeductableAmount, DeductablePercent, DeductableLedgerID, IsCostCenter, IsTagInvoice, Isautoposting, NonTaxableAmount, ByDefaultAmount, TransactionAmount, remarks, departmentid, departmentname, taxapplicable, paymenttypeid, paymenttypename, chequeno, chequedate, BusinessSegId, BusinessSegName) {
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
            url: "/Accountstran/addvouchercr1",
            // data: "{'AccountID': '" + AccountID + "','AccountName': '" + AccountName + "','DebitAmount': '" + DebitAmount + "','DeductableAmount': '" + DeductableAmount + "','DeductablePercent': '" + DeductablePercent + "','DeductableLedgerID': '" + DeductableLedgerID + "','IsCostCenter': '" + IsCostCenter + "','IsTagInvoice': '" + IsTagInvoice + "','Isautoposting': '" + Isautoposting + "','NonTaxableAmount': '" + NonTaxableAmount + "','ByDefaultAmount': '" + ByDefaultAmount + "','TransactionAmount': '" + TransactionAmount + "','remarks': '" + remarks + "','departmentid': '" + departmentid + "','departmentname': '" + departmentname + "','paymenttypeid': '" + paymenttypeid + "','paymenttypename': '" + paymenttypename + "','chequeno': '" + chequeno + "','chequedate': '" + chequedate + "','taxapplicable': '" + taxapplicable + "'}",
            data: {
                AccountID: AccountID, AccountName: AccountName, DebitAmount: DebitAmount, DeductableAmount: DeductableAmount, DeductablePercent: DeductablePercent, DeductableLedgerID: DeductableLedgerID, IsCostCenter: IsCostCenter, IsTagInvoice: IsTagInvoice, Isautoposting: Isautoposting, NonTaxableAmount: NonTaxableAmount, ByDefaultAmount: ByDefaultAmount, TransactionAmount: TransactionAmount, remarks: remarks, departmentid: departmentid, departmentname: departmentname, paymenttypeid: paymenttypeid, paymenttypename: paymenttypename, chequeno: chequeno, chequedate: chequedate, taxapplicable: taxapplicable, BusinessSegId: BusinessSegId, BusinessSegName: BusinessSegName
            },

            //data: "{'AccountID': '" + AccountID + "','AccountName': '" + AccountName + "'}",
            //data: "{'ledgerid': '" + ledgerid + "','CostCatagoryID': '" + CostCatagoryID + "','BrandId': '" + BrandId + "','ProductId': '" + ProductId + "','DepartmentId': '" + DepartmentId + "'}",

            dataType: "json",
            success: function (response) {

                var totalcredit = 0;
                var tableNew;
                var iscostcenter;
                $.each(response, function () {
                    iscostcenter = this["IsCostCenter"];
                    if (VOUCHERID == '2') {
                        if (iscostcenter == 'N') {
                            tableNew = "<tr><td style='display:none'>" + this["LedgerId"] + "</td>   <td style='width:60%'>" + this["LedgerName"] + "</td><td style='display:none'>" + this["DepartmentID"] + "</td><td >" + this["DepartmentName"] + "</td><td style='display:none'>" + this["BusinessSegId"] + "</td><td >" + this["BusinessSegName"] + "</td> <td style='width:30%;text-align:right'>" + this["Amount"] + "</td><td style='display:none'>" + this["IsCostCenter"] + "</td><td style='width:10%' ><input type='button' id='btncreditgrddelete'  class='Creditdel' value='Del'/></td><td style='width:10%' ><input type='button' id='btncreditcost'  class='clscostcr' value='No CostCenter'/></td><td style='display:none'>" + DeductableLedgerID + "</td><td style='display:none'>" + this["taxapplicable"] + "</td></tr> ";
                        }
                        else
                            if (iscostcenter == 'Y') {
                                tableNew = "<tr><td style='display:none'>" + this["LedgerId"] + "</td>   <td style='width:60%'>" + this["LedgerName"] + "</td><td style='display:none'>" + this["DepartmentID"] + "</td><td >" + this["DepartmentName"] + "</td><td style='display:none'>" + this["BusinessSegId"] + "</td><td >" + this["BusinessSegName"] + "</td> <td style='width:30%;text-align:right'>" + this["Amount"] + "</td><td style='display:none'>" + this["IsCostCenter"] + "</td><td style='width:10%' ><input type='button' id='btncreditgrddelete'  class='Creditdel' value='Del'/></td><td style='width:10%' ><input type='button' id='btncreditcost'  class='clscostcr' value='CostCenter'/></td><td style='display:none'>" + DeductableLedgerID + "</td><td style='display:none'>" + this["taxapplicable"] + "</td></tr> ";
                            }
                    }
                    totalcredit = totalcredit + parseFloat(this["Amount"]);
                })
                $("#creditadd").append(tableNew);
                $("#txtcredittotalamount").val(totalcredit);
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
function bindcr() {
    $.ajax({

        type: "POST",
        url: "/Accountstran/Bindvouchercr",
        data: '{}',
        //data: "{'ledgerid': '" + ledgerid + "','CostCatagoryID': '" + CostCatagoryID + "','BrandId': '" + BrandId + "','ProductId': '" + ProductId + "','DepartmentId': '" + DepartmentId + "'}",

        dataType: "json",
        success: function (response) {

            var totalcredit = 0;
            var tableNew;
            var iscostcenter;
            $.each(response, function () {

                totalcredit = totalcredit + parseFloat(this["Amount"]);
                iscostcenter = this["IsCostCenter"];
                if (VOUCHERID == '2') {
                    if (iscostcenter == 'N') {
                        tableNew = tableNew + "<tr><td style='display:none'>" + this["LedgerId"] + "</td>   <td style='width:60%'>" + this["LedgerName"] + "</td><td style='display:none'>" + this["DepartmentID"] + "</td><td >" + this["DepartmentName"] + "</td><td style='display:none'>" + this["BusinessSegId"] + "</td><td >" + this["BusinessSegName"] + "</td> <td style='width:30%;text-align:right'>" + this["Amount"] + "</td><td style='display:none'>" + this["IsCostCenter"] + "</td><td style='width:10%' ><input type='button' id='btncreditgrddelete'  class='Creditdel' value='Del'/></td><td style='width:10%' ><input type='button' id='btncreditcost'  class='clscostcr' value='No CostCenter'/></td><td style='display:none'>" + this["DeductableLedgerId"] + "</td><td style='display:none'>" + this["IsTagInvoice"] + "</td></tr> ";
                    }
                    else
                        if (iscostcenter == 'Y') {
                            tableNew = tableNew + "<tr><td style='display:none'>" + this["LedgerId"] + "</td>   <td style='width:60%'>" + this["LedgerName"] + "</td><td style='display:none'>" + this["DepartmentID"] + "</td><td >" + this["DepartmentName"] + "</td><td style='display:none'>" + this["BusinessSegId"] + "</td><td >" + this["BusinessSegName"] + "</td> <td style='width:30%;text-align:right'>" + this["Amount"] + "</td><td style='display:none'>" + this["IsCostCenter"] + "</td><td style='width:10%' ><input type='button' id='btncreditgrddelete'  class='Creditdel' value='Del'/></td><td style='width:10%' ><input type='button' id='btncreditcost'  class='clscostcr' value='CostCenter'/></td><td style='display:none'>" + this["DeductableLedgerId"] + "</td><td style='display:none'>" + this["IsTagInvoice"] + "</td></tr> ";
                        }
                }
                //tableNew = "<tr><td style='display:none'>" + this["LedgerId"] + "</td>   <td style='width:60%'>" + this["LedgerName"] + "</td><td style='width:30%;text-align:right'>" + this["Amount"] + "</td><td style='display:none'>" + this["IsCostCenter"] + "</td> <td style='width:10%' ><input type='button' id='btndebitgrddelete'  class='debitdel' value='Del'/></td><td style='width:10%' ><input type='button' id='btndebitgrddelete'  class='clscost' value='CostCenter'/></td><td style='display:none'>" + DeductableLedgerID + "</td></tr> ";
            })

            $("#txtcredittotalamount").val(totalcredit);
            $("#creditadd").append(tableNew);

        }
    });
}

function creditdel(ledgerid, tdsapplicable) {
    $.ajax({

        type: "POST",

        url: "/Accountstran/deletecr",

        data: "{'ledgerid': '" + ledgerid + "','tdsapplicable': '" + tdsapplicable + "'}",
        data: { ledgerid: ledgerid, tdsapplicable: tdsapplicable },
        dataType: "json",
        success: function (response) {
            var iscostcenter;
            var totalcredit = 0;
            var tableNew;
            $.each(response, function () {
                iscostcenter = this["IsCostCenter"];

                //tableNew = "<tr><td style='display:none'>" + this["LedgerId"] + "</td>   <td style='width:60%'>" + this["LedgerName"] + "</td><td style='width:30%;text-align:right'>" + this["Amount"] + "</td><td style='display:none'>" + this["IsCostCenter"] + "</td> <td style='width:10%' ><input type='button' id='btncreditgrddelete'   class='Creditdel'  value='Del'/></td><td style='display:none'>" + this["DeductableLedgerId"] + "</td><td style='display:none'>" + this["taxapplicable"] + "</td></tr> ";
                totalcredit = totalcredit + parseFloat(this["Amount"]);
            })

            $("#txtcredittotalamount").val(totalcredit);

            // $("#creditadd").append(tableNew);
        }
    });

}
// valodate data
function validatedata() {
    var totalcredit;
    var totaldebit;
    var vouchertypde;
    var gstapplicable = 'n';
    var gstapplicablecr = 'n';
    var billno = '';
    var billdate = '';
    var counter = 0;
    var narration;
    var chkbank;
    var chk = '1';
    var cashbank;

    totaldebit = parseFloat($('#txtdebittotalamount').val());
    totalcredit = parseFloat($('#txtcredittotalamount').val());
    if (totaldebit != totalcredit) {

        toastr.error('Debit & Credit side not equal...!');
        return false;
    }
}
//save data
function savedata(ch) {
    var totalcredit;
    var totaldebit;
    var vouchertypde;
    var gstapplicable = 'n';
    var gstapplicablecr = 'n';
    var billno = '';
    var billdate = '';
    var counter = 0;
    var narration;
    var chkbank;
    var chk = '1';
    var chk1;
    var cashbank;
    vouchertypde = $('#ddlGSTVoucherType').find('option:selected').text();
    totaldebit = parseFloat($('#txtdebittotalamount').val());
    totalcredit = parseFloat($('#txtcredittotalamount').val());
    narration = $('#txtNarration').val();
    billno = $("#txtbillno").val();
    billdate = $("#txtbilldate").val();
    var isgst = $('#isgstpaid').val();
    $("#debitadd tbody tr").each(function () {
        var accrname = $(this).find('td:eq(1)').html();

        var N;
        N = accrname.match("GST");


        if (N == 'GST') {
            gstapplicable = 'y';
            counter = counter + 1;
            return false;

        }

    })
    $("#creditadd tbody tr").each(function () {
        var accrname = $(this).find('td:eq(1)').html();

        var N;
        N = accrname.match("GST");


        if (N == 'GST') {
            gstapplicablecr = 'y';
            counter = counter + 1;
            return false;

        }

    });
    //if (vouchertypde == 'Normal' && gstapplicable== 'y')
    //{
    //    toastr.error('GST voucher not allowed with normal Voucher in Debit side...!');
    //    return false;
    //}
    //if (vouchertypde == 'Normal' && gstapplicablecr == 'y') {
    //    toastr.error('GST voucher not allowed with normal Voucher in Credit side...!');
    //    return false;
    //}

    //if (vouchertypde == 'Normal' && counter == 1) {
    //    toastr.error('Please atleast one GST entry in Dr and Cr side<...!');
    //    return false;
    //}
    chk = gstchk();
    if (vouchertypde == 'Normal' && chk == '1') {
        toastr.error('Please atleast one GST entry in Dr and Cr side<...!');
        return false;
    }

    if (vouchertypde == 'GST') {
        chk1 = chkgstdetail();


    }
    if (billno == '') {

        toastr.error('Provide bill No...!');
        return false;
    }
    else
        if (billdate == '') {

            toastr.error('Provide bill date...!');
            return false;
        }
    if (totaldebit != totalcredit) {

        toastr.error('Debit & Credit side not equal...!');
        return false;
    }
    else
        if (narration == '') {

            toastr.error('Provide narration...!');
            return false;
        }
    if (vouchertypde == 'GST') {

        if (chk1 == '0' || isgst == 'N') {

            toastr.error('Provide GST Details...!');
            return false;
        }
    }
    if (vouchertypde == 'GST' || vouchertypde == 'GST Govt') {

        if (chk == '0') {

            toastr.error('Please atleast one GST entry in Dr or Cr side...!');
            return false;
        }
        else
            if (chk == '2') {

                toastr.error('Please provide  one GST entry in either Dr or Cr side...!');
                return false;
            }
            else
                if (chk == '0') {
                    toastr.error('provide at least one GST entry in either Dr or Cr side...!');

                    return false;
                }

    }


    var paymentmode = '';
    var mode = '';
    if ($('#hdn_accid').val() == '') {
        mode = 'A'
    }
    else {
        mode = 'U';
    }
    var messageid;
    var messagetext;
    var voucherid;
    var accountssavesave = {};
    accountssavesave.vouchertypeid = $('#ddlvouchertype').val().trim();
    accountssavesave.vouchertypename = $('#ddlvouchertype').find('option:selected').text().trim();
    accountssavesave.BRID = $('#ddlregion').val().trim();
    accountssavesave.BRNAME = $('#ddlregion').find('option:selected').text().trim();
    accountssavesave.paymentmode = paymentmode;
    accountssavesave.voucherdate = $('#txtvoucherdate').val().trim();
    //accountssavesave.finyear   = $('#ddlvouchertype').val().trim();
    accountssavesave.narration = $('#txtNarration').val().trim();
    accountssavesave.mode = mode;
    accountssavesave.accid = $('#hdn_accid').val().trim();
    accountssavesave.isvoucherappliocable = 'N';
    accountssavesave.isfrompage = 'Y';
    //for other details
    accountssavesave.billno = $('#txtbillno').val().trim();
    accountssavesave.billdate = $('#txtbilldate').val().trim();
    accountssavesave.grnno = $('#txtgrno').val().trim();
    accountssavesave.grdate = $('#txtgrdate').val().trim();
    accountssavesave.vehicleno = $('#txtvehicleno').val().trim();
    accountssavesave.transport = $('#txttransport').val().trim();
    accountssavesave.waybillno = $('#txtwaybillno').val().trim();
    accountssavesave.waybilldate = $('#txtwaybilldate').val().trim();
    //end other details
    accountssavesave.billtargetfrompage = "N";
    accountssavesave.drcrtds = $('#hdndrcrtds').val().trim();
    accountssavesave.gstvpuchertypeid = $('#ddlGSTVoucherType').val().trim();
    accountssavesave.mtclaim = $('#hdnmtclaim').val().trim();
    accountssavesave.finyear = FINYEAR;
    accountssavesave.userid = UserID;

    $("#dialog").dialog({
        autoOpen: true,
        modal: true,
        title: "Loading.."
    });
    $("#imgLoader").css("visibility", "visible");
    setTimeout(function () {
        $.ajax({

            url: "/Accountstran/savevoucher",
            data: '{accountssavesave:' + JSON.stringify(accountssavesave) + '}',
            type: "POST",
            async: false,
            contentType: "application/json",
            success: function (Accresponse) {

                $.each(Accresponse, function (key, item) {
                    //  messageid = item.MessageID;
                    messagetext = item.voucherno;
                    voucherid = item.voucherid;
                });

                if (messageid != '0') {
                    //$('#dvAdd').css("display", "none");
                    //$('#dvDisplay').css("display", "");
                    //$("#txtfrmdate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
                    //$("#txttodate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
                    //ClearControls();
                    $('#debitadd').find('tbody').detach();
                    $('#debitadd').append($('<tbody>'));

                    $('#creditadd').find('tbody').detach();
                    $('#creditadd').append($('<tbody>'));
                    close();
                    if (ch == 1) {

                        var depoid;
                        depoid = $('#ddlregion').val();

                        var url = "http://mcnroeerp.com/mcworld/VIEW/frmRptInvoicePrintv2.aspx?AdvRecpt_ID=" + voucherid + "&Voucherid=" + VOUCHERID + "&depoid=" + depoid;
                        window.open(url, "", "toolbar=yes,scrollbars=yes,resizable=yes,top=500,left=500,width=700,height=600");

                    }
                    //bindfgdispatchgrid();
                    ReleaseSession();
                    toastr.success('<b><font color=black>' + messagetext + '</font></b>');
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
        $("#imgLoader").css("visibility", "hidden");
        $("#dialog").dialog("close");
    }, 3);
    //validatedata();
}

//approved data
function approveddata() {
    if ($('#ddlapprovalledger').val() == '0') {
        toastr.error('Please select transfer to ledger for approval');
        return false;
    }

    var messageid;
    var messagetext;
    var voucherid;

    var voucherid = $('#hdn_accid').val();
    var ledgerid = $('#ddlapprovalledger').val();
    var ledgername = $('#ddlapprovalledger').find('option:selected').text().trim();

    $.ajax({

        type: "POST",
        url: "/Accountstran/Approvedvoucher",
        data: { VoucherID: voucherid, Ledgerid: ledgerid, LedgerName: ledgername, finyear: FINYEAR },
        async: false,
        success: function (Accresponse) {

            $.each(Accresponse, function (key, item) {
                messagetext = item.voucherno;
                voucherid = item.voucherid;
            });

            if (messageid != '0') {
                $('#debitadd').find('tbody').detach();
                $('#debitadd').append($('<tbody>'));

                $('#creditadd').find('tbody').detach();
                $('#creditadd').append($('<tbody>'));
                close();
                ReleaseSession();
                toastr.success('<b><font color=black>' + messagetext + '</font></b>');
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

//gst related
function BindAllState() {
    var ddlStateNamenew = $("#ddlStateNamenew");
    var ddlToStateName = $("#ddlToStateName");
    $.ajax({
        type: "POST",
        url: "/Accountstran/BindAllState",
        data: '{}',
        async: false,
        dataType: "json",

        success: function (response) {
            //alert(JSON.stringify(response));
            ddlStateNamenew.empty().append('<option selected="selected" value="0">Select State</option>');
            ddlToStateName.empty().append('<option selected="selected" value="0">Select State</option>');
            //.append('<option selected="selected" value="0">Select Brancht</option>');;
            $.each(response, function () {
                ddlStateNamenew.append($("<option></option>").val(this['State_ID']).html(this['State_Name']));
                ddlToStateName.append($("<option></option>").val(this['State_ID']).html(this['State_Name']));
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
function BindGSTGroupnew() {
    var ddlDCJ_Group = $("#ddlDCJ_GroupName");
    $('#myModalgst').css("display", "");
    setTimeout(function () {

        $.ajax({
            type: "POST",
            url: "/Accountstran/BindGSTGroup",
            data: '{}',
            async: false,
            dataType: "json",

            success: function (response) {
                //alert(JSON.stringify(response));
                ddlDCJ_Group.empty().append('<option selected="selected" value="0">Select GST Group</option>');
                //.append('<option selected="selected" value="0">Select Brancht</option>');;
                $.each(response, function () {
                    ddlDCJ_Group.append($("<option></option>").val(this['GROUP_TYPEID']).html(this['GROUP_TYPENAME']));
                });




            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });

        $('#myModalgst').css("display", "none");
    }, 5);
    e.preventDefault();
}

function BindDCJ_Party() {
    var ddlDCJ_Party = $("#ddlDCJ_Partynew");
    var ddlDCJ_Group = $("#ddlDCJ_GroupName").val();
    $('#myModalgst').css("display", "");
    setTimeout(function () {
        $.ajax({
            type: "POST",
            url: "/Accountstran/BindDCJ_Party",

            data: { djgroup: ddlDCJ_Group },
            async: false,
            dataType: "json",

            success: function (response) {
                //alert(JSON.stringify(response));
                ddlDCJ_Party.empty().append('<option selected="selected" value="0">Select Vendor</option>');
                //.append('<option selected="selected" value="0">Select Brancht</option>');;
                $.each(response, function () {
                    ddlDCJ_Party.append($("<option></option>").val(this['VENDORID']).html(this['VENDORNAME']));
                });




            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });

        $('#myModalgst').css("display", "none");
    }, 5);
    e.preventDefault();

}
function BindDCJ_Partyedit(groupid) {
    var ddlDCJ_Party = $("#ddlDCJ_Partynew");
    //  var ddlDCJ_Group = $("#ddlDCJ_GroupName").val();
    $('#myModalgst').css("display", "");
    setTimeout(function () {
        $.ajax({
            type: "POST",
            url: "/Accountstran/BindDCJ_Party",

            data: { djgroup: groupid },
            async: false,
            dataType: "json",

            success: function (response) {
                //alert(JSON.stringify(response));
                ddlDCJ_Party.empty().append('<option selected="selected" value="0">Select Vendor</option>');
                //.append('<option selected="selected" value="0">Select Brancht</option>');;
                $.each(response, function () {
                    ddlDCJ_Party.append($("<option></option>").val(this['VENDORID']).html(this['VENDORNAME']));
                });




            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });

        $('#myModalgst').css("display", "none");
    }, 5);
    e.preventDefault();
}

function BindTaxPercentage() {
    var txtDCJ_Taxablenew = $("#txtDCJ_Taxablenew");
    var partyname = $("#ddlDCJ_Partynew").val();

    var statename = $("#ddlStateNamenew").val();
    var region = $("#ddlToStateName").val();



    $.ajax({
        type: "POST",
        url: "/Accountstran/BindTaxPercentage",
        data: { partyname: partyname, stateid: statename, regionid: region },
        // data: "{'partyname': '" + partyname + "', 'statename': '" + statename + "'}",
        async: false,
        dataType: "json",

        success: function (response) {

            txtDCJ_Taxablenew.empty().append('<option selected="selected" value="0">Select</option>');

            //.append('<option selected="selected" value="0">Select Brancht</option>');;
            $.each(response, function () {

                txtDCJ_Taxablenew.append($("<option></option>").val(this['ID']).html(this['NAME']));

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
function BindDCJ_GstNo() {
    var ddlDCJ_GstNonew = $("#ddlDCJ_GstNonew");
    var partyname = $("#ddlDCJ_Partynew").val();
    $('#myModalgst').css("display", "");
    setTimeout(function () {
        var statename = $("#ddlStateNamenew").val();
        var depoid = $("#ddlToStateName").val();
        var vouchertype = $("#ddlGSTVoucherType").val();
        if (vouchertype == 'CustomDutyVoucher') {
            ddlDCJ_GstNonew.empty().append('<option selected="selected" value="NA">Select NA</option>');
        }
        else {
            $.ajax({
                type: "POST",
                url: "/Accountstran/BindDCJ_GstNo",
                data: { party: partyname, statename: statename },
                // data: "{'partyname': '" + partyname + "', 'statename': '" + statename + "'}",
                async: false,
                dataType: "json",

                success: function (response) {

                    ddlDCJ_GstNonew.empty();
                    //.append('<option selected="selected" value="0">Select GST No.</option>');

                    //.append('<option selected="selected" value="0">Select Brancht</option>');;
                    $.each(response, function () {

                        ddlDCJ_GstNonew.append($("<option></option>").val(this['GSTNO']).html(this['GSTNO']));

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
        bindtaxtype(partyname, statename, depoid);
        BindTaxPercentage();
        $('#myModalgst').css("display", "none");
    }, 5);
    e.preventDefault();
}
function Cal_GST_Tax(taxable) {
    var amt = 0;
    var rate = 0;
    var totaltaxamt = 0;
    var _totaltaxamt = 0;
    var roundoff = $('#txtRoundOff').val();
    var _roundoff = parseFloat(roundoff);

    var taxablevalue = $('#txtDCJ_TaxableValuenew').val();
    if (taxable != '' && taxablevalue != '') {
        amt = parseFloat($('#txtDCJ_TaxableValuenew').val());
        rate = parseFloat(taxable);
        totaltaxamt = (amt * rate) / 100;

        _totaltaxamt = totaltaxamt.toFixed(2);

        var taxtype1 = $('#txtDCJ_TaxType1').val();
        if (taxtype1 != '') {
            _totaltaxamt = _totaltaxamt * 2;
            $("#txtDCJ_TaxableValue1").val(taxablevalue);
            $("#txtDCJ_Taxable1").val(taxable);

        }

        var netamt = amt + _totaltaxamt;
        if (_roundoff < 0) {
            netamt = netamt - _roundoff;
        }
        else {
            netamt = netamt + _roundoff;
        }


        netamt = netamt.toFixed(2);
        alert(netamt);
        $("#txtDCJ_TaxAmount").val(_totaltaxamt);
        $("#txtDCJ_NetAmount").val(netamt);

    }


}


function bindtaxtype(partyid, stateid, depoid) {

    $('#myModalgst').css("display", "");
    setTimeout(function () {
        var ddlDCJ_TaxType = $("#ddlDCJ_TaxType");
        var vouchertype = $("#ddlGSTVoucherType").val();
        if (vouchertype == 'CustomDutyVoucher') {
            ddlDCJ_TaxType.empty().append('<option selected="selected" value="8C60D11D-9524-4DC4-AA9B-AF956C52E41F">INPUT IGST.</option>');
        }
        else {
            $.ajax({
                type: "POST",
                url: "/Accountstran/Gettaxtype",
                data: { partyid: partyid, stateid: stateid, depoid: depoid },

                async: false,
                dataType: "json",

                success: function (response) {

                    //alert(JSON.stringify(response));
                    ddlDCJ_TaxType.empty().append('<option selected="selected" value="0">Select Tax Type</option>');
                    //.append('<option selected="selected" value="0">Select Brancht</option>');;
                    $.each(response, function () {
                        ddlDCJ_TaxType.append($("<option></option>").val(this['ID']).html(this['NAME']));
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
        $('#myModalgst').css("display", "none");
    }, 5);
    e.preventDefault();
}
function gstpopup() {

    var totalcredit = 0;
    var totaldebit = 0;
    var counter = 0;
    var gstapplicablecr = 'n';
    var gstapplicable = 'n';
    var vouchertypde;
    var narration;
    narration = $('#txtNarration').val();
    $('#txtDCJ_NonTaxableAmount').val('0.00');
    $('#txtRoundOff').val('0.00');
    $('#txtGSTTotal').val('0.00')
    totaldebit = parseFloat($('#txtdebittotalamount').val());
    totalcredit = parseFloat($('#txtcredittotalamount').val());
    if (totaldebit != totalcredit) {
        toastr.error('Debit & Credit side not equal...!');
        return false;
    }
    if (narration == '') {

        toastr.error('Provide narration...!');
        return false;
    }
    vouchertypde = $('#ddlGSTVoucherType').find('option:selected').text();
    $("#debitadd tbody tr").each(function () {
        var accrname = $(this).find('td:eq(1)').html();

        var N;
        N = accrname.match("GST");


        if (N == 'GST') {
            gstapplicable = 'y';
            counter = counter + 1;
            return false;

        }

    });
    $("#creditadd tbody tr").each(function () {
        var accrname = $(this).find('td:eq(1)').html();

        var N;
        N = accrname.match("GST");


        if (N == 'GST') {
            gstapplicablecr = 'y';
            counter = counter + 1;
            return false;

        }

    });

    if (vouchertypde == 'GST' || vouchertypde == 'GST Govt') {

        if (counter == 0) {

            toastr.error('Please atleast one GST entry in Dr or Cr side');
            return false;
        }
        else
            if (counter == 2) {

                toastr.error('Please provide  one GST entry in either Dr or Cr sid');
                return false;
            }
    }

    var chk;
    $("#dialog").dialog({
        autoOpen: true,
        modal: true,
        title: "Loading.."
    });
    $("#imgLoader").css("visibility", "visible");

    setTimeout(function () {
        $.ajax({
            type: "POST",
            url: "/Accountstran/gstpopup",
            data: '{}',

            async: false,
            dataType: "json",

            success: function (response) {
                //alert(JSON.stringify(response));


                // $.each(response, function (index, record) {
                $.each(response, function () {
                    //chk = this['response'];


                    chk = this['response'];


                });
                if (chk == 'o') {
                    BindAllState();
                    BindGSTGroup();

                    // ShowDialoggst(true);
                    //e.preventDefault();
                }
                else
                    if (chk == 'n') {
                        var amt = $("#txtdebittotalamount").val();
                        BindAllState();
                        BindGSTGroupnew();
                        // bindgstdtledit();


                        $("#gstval").html("ONE TIME ENTRY AMOUNT  Rs. " + amt);
                        $("#txtDCJ_NonTaxableAmount").val('0.00');
                        $("#grdgstnew").remove();

                        ShowDialoggstnew(this);
                        e.preventDefault();

                        //$("#PanelDCJ_Invoicenew").dialog({
                        //    autoOpen: true,
                        //    modal: true,
                        //    title: "Loading.."
                        //});
                        //$("#PanelDCJ_Invoicenew1").css("visibility", "visible");
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
function BindGSTGroup() {

    $.ajax(
        {
            type: "POST",
            url: "/Accountstran/BindGSTGroup",
            data: '{}',

            dataType: "json",
            success: function (response) {

                var listItems = "<optiossn value='0'>Select GST Group</option>";


                $.each(response, function () {
                    listItems += "<option value='" + this["GROUP_TYPEID"] + "'>" + this["GROUP_TYPENAME"] + "</option>";
                });
                $("#ddlDCJ_GroupName").html(listItems);






                //var ddlsupplingdepot = $("[id*=ddlsupplingdepot]");
                //ddlsupplingdepot.empty().append('<option selected="selected" value="0">Please select</option>');
                //$.each(r.d, function () {
                //    ddlsupplingdepot.append($("<option></option>").val(this['Value']).html(this['Text']));
                //});
            }
        });
}
function gstchk() {
    var chk;
    $.ajax({
        type: "POST",
        url: "/Accountstran/gstchk",
        data: '{}',

        async: false,
        dataType: "json",

        success: function (response) {
            //alert(JSON.stringify(response));


            // $.each(response, function (index, record) {
            $.each(response, function () {
                //chk = this['response'];


                chk = this['response'];


            });





        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });

    return chk;
}
function chkgstdetail() {
    var chk1;
    $.ajax({

        type: "POST",

        url: "/Accountstran/chkgstdetail",

        data: '{}',
        async: false,

        success: function (response) {


            $.each(response, function () {
                chk1 = this['response'];
            })






        }

    });

    return chk1;
}
function addgstnew(GroupId, PartyID, InvoiceNo, InvoiceDate, Taxable, TaxableValue, HSNCode, PartyTrade, StateID, GSTNo, TaxTypeID, TaxType, TaxAmount, NetAmount, Taxable1, TaxAmount1, TaxTypeID1, Taxtype1, igst, roundoff, txtDCJ_NonTaxableAmount, PlaceOfSupply) {
    $('#myModalgst').css("display", "");
    setTimeout(function () {

        $.ajax({

            type: "POST",

            url: "/Accountstran/addgstnew",

            // data: "{'GroupId': '" + GroupId + "', 'PartyID': '" + PartyID + "','InvoiceNo': '" + InvoiceNo + "','InvoiceDate': '" + InvoiceDate + "','Taxable': '" + Taxable + "','TaxableValue': '" + TaxableValue + "','HSNCode': '" + HSNCode + "','PartyTrade': '" + PartyTrade + "','StateID': '" + StateID + "','GSTNo': '" + GSTNo + "','TaxTypeID': '" + TaxTypeID + "','TaxType': '" + TaxType + "','TaxAmount': '" + TaxAmount + "','NetAmount': '" + NetAmount + "','Taxable1': '" + Taxable1 + "','TaxAmount1': '" + TaxAmount1 + "','TaxTypeID1': '" + TaxTypeID1 + "','Taxtype1': '" + Taxtype1 + "','igst': '" + igst + "'}",
            data: { GroupId: GroupId, PartyID: PartyID, InvoiceNo: InvoiceNo, InvoiceDate: InvoiceDate, Taxable: Taxable, TaxableValue: TaxableValue, HSNCode: HSNCode, PartyTrade: PartyTrade, StateID: StateID, GSTNo: GSTNo, TaxTypeID: TaxTypeID, TaxType: TaxType, TaxAmount: TaxAmount, NetAmount: NetAmount, Taxable1: Taxable1, TaxAmount1: TaxAmount1, TaxTypeID1: TaxTypeID1, Taxtype1: Taxtype1, igst: igst, roundoff: roundoff, txtDCJ_NonTaxableAmount: txtDCJ_NonTaxableAmount, PlaceOfSupply: PlaceOfSupply },
            dataType: "json",
            success: function (response) {
                var counter = 0;
                var tableNew = '<table id="grdgstnew" class="reportgrid">';
                tableNew = tableNew + "<thead style='background-color:cornflowerblue;font-size:14px'><tr><th style='display:none'></th><th style='display:none'></th><th style='display:none'></th><th>SL</th> <th>HSN CODE </th><th>TAX TYPE1</th> <th>TAX TYPE2</th> <th>TAXABLE (%)</th> <th>TAXABLE VALUE</th> <th>TAX AMOUNT</th> <th>NET AMOUNT</th><th style=width:80px'></th> </tr></thead><tbody>";
                $.each(response, function () {
                    counter = counter + 1;
                    tableNew = tableNew + "<tr><td style='display:none'>" + this["GroupId"] + "</td><td style='display:none'>" + this["PartyID"] + "</td><td style='display:none'>" + this["GUID"] + "</td><td>  " + counter + "</td> <td>" + this["HSNCode"] + "</td><td>" + this["TaxType"] + "</td><td>" + this["TaxType1"] + "</td><td>" + this["Taxable1"] + "</td><td>" + this["TaxAmount"] + "</td><td>" + this["NetAmount"] + "</td><td style='width:10px'><input type='button' id='btndebitgrddelete'  class='gstdel' value='Del'/></td>";

                })
                document.getElementById("GridView_DCJ_GST").innerHTML = tableNew + '</tbody></table>';
                var totalgst = 0;
                var netamt = 0;
                var _totgst = parseFloat($("#txtGSTTotal").val());
                if (counter > 0) {
                    netamt = parseFloat($("#txtDCJ_NetAmount").val());
                    $("#ddlStateNamenew").prop("disabled", true);
                    $("#ddlToStateName").prop("disabled", true);
                    $("#ddlDCJ_GroupName").prop("disabled", true);
                    $("#ddlDCJ_Partynew").prop("disabled", true);
                    $("#ddlDCJ_GstNonew").prop("disabled", true);
                    $("#txtDCJ_InvoiceNonew").attr("disabled", "disabled");
                    $("#txtDCJ_PartyTradenew").attr("disabled", "disabled");
                    $("#txtDCJ_InvoiceNonew").attr("disabled", "disabled");
                }
                else {
                    netamt = parseFloat($("#txtDCJ_NetAmount").val()) + parseFloat($("#txtDCJ_NonTaxableAmount").val())
                }



                totalgst = netamt + _totgst;
                $("#txtGSTTotal").val(totalgst);

                $("#txtDCJ_HSNCodenew").val('');
                $("#txtDCJ_TaxableValuenew").val('');
                $("#txtDCJ_ISIGST").val('');
                $("#txtDCJ_TaxType1").val('');
                $("#txtDCJ_TaxableValue1").val('');
                $("#txtDCJ_TaxAmount").val('');
                $("#txtDCJ_NetAmount").val('');
                $("#ddlDCJ_TaxType").val('0');
                $("#txtDCJ_Taxablenew").val('0');
                $("#txtRoundOff").val('0');


            }
        })
        $('#myModalgst').css("display", "none");
    }, 5);
    e.preventDefault();

}
function HideDialoggstnew() {
    $("#overlay").hide();
    $("#PanelDCJ_Invoicenew").fadeOut(300);
}
function ShowDialoggstnew(modal) {
    $("#overlay").show();
    $("#PanelDCJ_Invoicenew").fadeIn(300);

    if (modal) {
        $("#overlay").unbind("click");
    }
    else {
        $("#overlay").click(function (e) {
            HideDialoggst();
        });
    }
}
function ShowDialogtds(modal) {
    $("#overlay").show();
    $("#dialogtds").fadeIn(300);

    if (modal) {
        $("#overlay").unbind("click");
    }
    else {
        $("#overlay").click(function (e) {
            HideDialogtds();
        });
    }
}
function addtds(trantype) {
    var amtdr;

    var accdrvalue;
    var accountdrtext;
    var region = $("#ddlregion").val();
    var voucherdate = $('#txtvoucherdate').val();
    var voucherid = $('#hdnvoucherid').val();
    //   var deptvalue = $("#ddldepartmentBud").val();
    if (trantype == 'Dr') {

        accdrvalue = $("#ddlAccTypeDr").val();
        accountdrtext = $('#ddlAccTypeDr').find('option:selected').text();
        $("#hdnDrCrTDS").val('Dr');
        // var depttext = $('#ddldepartmentBud').find('option:selected').text();

        var drcr = 'Cr';
        amtdr = $('#txtAmtDr').val();

    }
    else
        if (trantype == 'Cr') {
            accdrvalue = $("#ddlAcctypeCr").val();
            accountdrtext = $('#ddlAcctypeCr').find('option:selected').text();
            $("#hdnDrCrTDS").val('Cr');


            var drcr = 'Cr';
            amtdr = $('#txtAmntCr').val();
        }
    var AccountType = '0'
    var istaxapplicable = '0'
    var counter = 0;

    ////var row = $("[id*=GridViewBudget] tr:last").clone();
    //   $("td:nth-child(1)", row).html(accdrvalue);
    //   $("td:nth-child(2)", row).html(accountdrtext);
    //   $("td:nth-child(3)", row).html(deptvalue);
    //    $("td:nth-child(4)", row).html(depttext);
    //   $("[id*=GridViewBudget] tbody").append(row);

    $.ajax({

        type: "POST",

        // url: "<%=ServiceURL%>Accounts.asmx/TaxApplicable",
        url: "/Accountstran/TaxApplicable",
        //  data: "{'AccountID': '" + accdrvalue + "', 'AccountName': '" + accountdrtext + "','AccountType': '" + AccountType + "','Amount': '" + amtdr + "','BranchID': '" + region + "','voucherdate': '" + voucherdate + "','voucherid': '" + voucherid + "','drcr': '" + trantype + "'}",
        data: { AccountID: accdrvalue, AccountName: accountdrtext, AccountType: AccountType, Amount: amtdr, BranchID: region, voucherdate: voucherdate, voucherid: voucherid, drcr: trantype, finyr: FINYEAR },
        // data: "{'AccountID': '" + accdrvalue + "', 'AccountName': '" + accountdrtext + "','AccountType': '" + AccountType + "','Amount': '" + amtdr + "','BranchID': '" + region + "','voucherdate': '" + voucherdate + "','voucherid': '" + voucherid + "','drcr': '" + trantype + "'}",

        dataType: "json",
        success: function (response) {




            var tableNew = '<table id="tdslist" class="reportgrid">';
            tableNew = tableNew + '<thead><tr><th style="display:none"></th><th>SL</th> <th>Tax name</th><th>By Default</th><th>Transaction Amt</th> <th>Taxable Amt.<th>Percentage<th>Tax Amt</th><th>Non Taxable Amt</th> </tr></thead> <tbody>';
            $.each(response, function () {
                counter = counter + 1;
                istaxapplicable = '1';

                tableNew = tableNew + "<tr><td style='display:none'>" + this["ID"] + "</td>   <td><input type='checkbox' id='chktaxid' name='chktaxid' class='chktds' value='1' checked>" + counter + "</td><td>" + this["NAME"] + "</td><td class='defaultamt'> <input type='text' class='form-control' id='txtByDefault' class='ByDefault' style='text-align:center' value='" + this["BYDEFAULTAMOUNT"] + "'/>   </td><td class='tranamt'><input type='text' class='form-control' id='txtTransactionAmount' class='TransactionAmount' style='text-align:center' value='" + this["TransactionAmount"] + "'/></td><td >" + this["DEDUCTABLEAMOUNT"] + "</td><td>" + this["DEDUCTABLEPERCENT"] + "</td><td> " + this["TDSAMOUNT"] + " </td><td ><input type='text' class='nontax' id='txtNonTaxableAmount'  style='text-align:center' value='" + this["NonTaxableAmount"] + "'/></td></tr > "

            });
            document.getElementById("addtds").innerHTML = tableNew + '</tbody></table>';

            if (istaxapplicable == '1') {
                ShowDialogtds(true);
                e.preventDefault();
            }
            //   $('#tdslist').find('input:checkbox').prop('checked', true);






        }

    });


}
function taxaction(taxledgerid) {
    //taxaction.isnegative;
    //taxaction.isdrcr;
    $.ajax({

        type: "POST",

        url: "/Accountstran/TaxAction",

        // data: "{'taxid': '" + taxledgerid + "'}",
        data: { taxid: taxledgerid },
        success: function (response) {
            $.each(response, function () {





                isnisnegative = this["isnegative"];
                isdrcr = this["isdrcr"];
            })
        }
    })
    return isdrcr;
}

function HideDialogtds() {
    $("#overlay").hide();
    $("#dialogtds").fadeOut(300);
}

//other details
function ShowDialogothers(modal) {
    $("#overlay").show();
    $("#otherdetails").fadeIn(300);

    if (modal) {
        $("#overlay").unbind("click");
    }
    else {
        $("#overlay").click(function (e) {
            HideDialogothers();
        });
    }
}
function HideDialogothers() {
    $("#overlay").hide();
    $("#otherdetails").fadeOut(300);
}
//Invoice & outstanding
function Outstanding(drcr, LedgerID, ledgername) {
    //var LedgerID = $('#ddlAccTypeDr').val();
    var RegionID = $("#ddlregion").val();
    var FromDate = $('#txtvoucherdate').val();
    var ToDate = $('#txtvoucherdate').val();
    $.ajax({

        type: "POST",

        url: "/Accountstran/Outstanding",

        // data: "{'AccountID': '" + accdrvalue + "'}*/",
        data: { LedgerID: LedgerID, RegionID: RegionID, FromDate: FromDate, ToDate: ToDate, finyr: FINYEAR },
        async: false,
        dataType: "json",
        success: function (response) {
            var outstandingamt;
            $.each(response, function () {
                outstandingamt = this['OUTSTANDING'];





            })

            if (drcr == 'Dr') {
                $('#lbldebitoutstanding').text(outstandingamt);
            }
            if (drcr == 'Cr') {
                $('#lblcreditoutstanding').text(outstandingamt);
            }
        }

    });

}
function InvoiceDetails(drcr, Leadgerid, ledgername) {
    //var LedgerID = $('#ddlAccTypeDr').val();
    var Branchid = $("#ddlregion").val();

    var VoucherID = '';
    $.ajax({

        type: "POST",

        url: "/Accountstran/InvoiceDetails",

        // data: "{'AccountID': '" + accdrvalue + "'}*/",
        data: { VoucherID: VoucherID, Leadgerid: Leadgerid, VouchertYpe: VOUCHERID, Branchid: Branchid, finyr: FINYEAR },
        async: false,
        dataType: "json",
        success: function (response) {
            var counter = 0;
            var tableNew = '<table id="grd_InvoiceDetails" class="table table-striped table-bordered dt-responsive nowrap" style="width:100%">';
            tableNew = tableNew + '<thead><tr><th style="display:none"></th><th>SL</th><th></th> <th style="display:none">LedgerName</th><th style="display:none">VoucherType</th><th style="display:none">BranchID</th> <th style="display:none">InvoiceID.</th><th>Invoice/Voucher No</th><th>Invoice/Voucher Date</th><th>Others Info</th><th>Branch</th><th>Invoice Amt</th><th>Already Paid Amt</th><th>Return Amt</th><th>Remaining Amt</th><th>Currently Paid Amt</th> </tr></thead> <tbody>';


            $.each(response, function () {
                counter = counter + 1;


                tableNew = tableNew + "<tr><td style='display:none'>" + this["LedgerId"] + "</td>   <td>" + counter + "</td><td ><input type='checkbox' name='chkinv' id='chkinv' class='clschkinv' value='' ></td> <td style='display:none'>" + this["LedgerName"] + "</td><td style='display:none'> " + this["VoucherType"] + "   </td><td style='display:none'>" + this["BranchID"] + "</td><td style='display:none'>" + this["InvoiceID"] + "</td><td>" + this["InvoiceNo"] + "</td><td> " + this["InvoiceDate"] + " </td><td >" + this["InvoiceOthers"] + "</td><td >" + this["InvoiceBranchName"] + "</td><td >" + this["InvoiceAmt"] + "</td><td >" + this["AlreadyAmtPaid"] + "</td><td >" + this["ReturnAmt"] + "</td><td >" + this["RemainingAmt"] + "</td><td ><input type='text' class='clstxtamtpaid' id='txtamtpaid' style='text-align:right;width:100px' value='0'/></td></tr > "





            })

            document.getElementById("InvoiceDetails").innerHTML = tableNew + '</tbody></table>';

            $("#claimhdr").html(ledgername + ' Invoice Details');
            if (counter > 0) {
                $('#trinvoicedetails').css("display", "");
            }
        }

    });

}

//costcenter

function costcenterapplicable(drcr) {

    var accdrvalue;

    var accname;
    var amt;

    if (drcr == 'Dr') {

        accdrvalue = $("#ddlAccTypeDr").val();
        accname = $('#ddlAccTypeDr').find('option:selected').text();
        amt = $("#txtAmtDr").val();
        $("#hdn_ledgercostcenter").val(accdrvalue);
        $("#hdn_ledgercostcentername").val(accname);
    }
    else
        if (drcr == 'Cr') {
            accdrvalue = $("#ddlAcctypeCr").val();
            accname = $('#ddlAcctypeCr').find('option:selected').text();
            amt = $("#txtAmntCr").val();
            $("#hdn_ledgercostcenter").val(accdrvalue);
            $("#hdn_ledgercostcentername").val(accname);
        }

    $.ajax({

        type: "POST",

        url: "/Accountstran/costapplicable",

        // data: "{'AccountID': '" + accdrvalue + "'}*/",
        data: { AccountID: accdrvalue, finyr: FINYEAR },
        async: false,
        dataType: "json",
        success: function (response) {
            var department;
            var businesssegment;
            var chk;
            $.each(response, function () {
                chk = this['response'];





            })
            if (chk == "1") {
                $("#ddldepartment").prop("disabled", false);
                $("#ddlcatagory").prop("disabled", false);
                if (drcr == 'Dr') {
                    department = $("#ddlDepartmentDr").val();
                    businesssegment = $("#ddlBusinessSegDr").val();
                }
                else
                    if (drcr == 'Cr') {
                        department = $("#ddlDepartmentCr").val();
                        businesssegment = $("#ddlBusinessSegCr").val();
                    }
                $("#ddldepartment").val(department);
                $("#ddlcatagory").val(businesssegment);

                $("#costcentertable").remove();

                ShowDialogcostcenter(true);
                $("#hdn_costcenterapplicable").val('Y');
                //$('costdetail').append("Cost Center For Ledger "+accname + " Amount Rs. "+accdrvalue);

                $("#costdetail").html("Cost Center For Ledger " + accname + " Amount Rs. " + amt);
                e.preventDefault();

            }





            else {
                HideDialogcostcenter();
                e.preventDefault();

            }
        }

    });
}

function BindCostCenterCatagory() {
    var ddlcatagory = $("#ddlcatagory");
    var ddlBusinessSegDr = $("#ddlBusinessSegDr");
    var ddlBusinessSegCr = $("#ddlBusinessSegCr");

    $("#dialog").dialog({
        autoOpen: true,
        modal: true,
        title: "Loading.."
    });
    $("#imgLoader").css("visibility", "visible");

    setTimeout(function () {

        $.ajax({
            type: "POST",
            url: "/Accountstran/BindCostCenterCatagory",
            data: '{}',
            async: false,
            dataType: "json",

            success: function (response) {
                //alert(JSON.stringify(response));
                ddlcatagory.empty().append('<option selected="selected" value="0">NA</option>');
                ddlBusinessSegDr.empty().append('<option selected="selected" value="0">Select</option>');
                ddlBusinessSegCr.empty().append('<option selected="selected" value="0">Select</option>');
                //.append('<option selected="selected" value="0">Select Brancht</option>');;
                $.each(response, function () {
                    ddlcatagory.append($("<option></option>").val(this['BSID']).html(this['BSNAME']));
                    ddlBusinessSegDr.append($("<option></option>").val(this['BSID']).html(this['BSNAME']));
                    ddlBusinessSegCr.append($("<option></option>").val(this['BSID']).html(this['BSNAME']));
                });






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
function BindBrand() {
    $("#dialog").dialog({
        autoOpen: true,
        modal: true,
        title: "Loading.."
    });
    $("#imgLoader").css("visibility", "visible");

    setTimeout(function () {
        var ddlbrand = $("#ddlbrand");
        $.ajax({
            type: "POST",
            url: "/Accountstran/BindBrand",
            data: '{}',
            async: false,
            dataType: "json",

            success: function (response) {
                //alert(JSON.stringify(response));
                ddlbrand.empty().append('<option selected="selected" value="0">NA</option>');
                //.append('<option selected="selected" value="0">Select Brancht</option>');;
                $.each(response, function () {
                    ddlbrand.append($("<option></option>").val(this['DIVID']).html(this['DIVNAME']));
                });




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

function BindProduct() {
    var ddlproduct = $("#ddlproduct");
    $('#myModal').css("display", "");
    setTimeout(function () {

        $.ajax({
            type: "POST",
            url: "/Accountstran/BindProduct",
            data: '{}',
            async: false,
            dataType: "json",

            success: function (response) {
                //alert(JSON.stringify(response));
                ddlproduct.empty().append('<option selected="selected" value="0">NA</option>');
                //.append('<option selected="selected" value="0">Select Brancht</option>');;
                $.each(response, function () {
                    ddlproduct.append($("<option></option>").val(this['PRODUCTID']).html(this['PRODUCTNAME']));
                });




            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
        $('#myModal').css("display", "none");
    }, 5);
    e.preventDefault();

}
function BindDepartment() {
    var ddldepartment = $("#ddldepartment");
    var ddldepartmentBud = $("#ddldepartmentBud");
    var ddlDepartmentDr = $("#ddlDepartmentDr");
    var ddlDepartmentCr = $("#ddlDepartmentCr");
    $("#dialog").dialog({
        autoOpen: true,
        modal: true,
        title: "Loading.."
    });
    $("#imgLoader").css("visibility", "visible");

    setTimeout(function () {

        $.ajax({
            type: "POST",
            url: "/Accountstran/BindDepartment",
            data: '{}',
            async: false,
            dataType: "json",

            success: function (response) {
                //alert(JSON.stringify(response));
                ddldepartment.empty().append('<option selected="selected" value="0">Select Department</option>');
                ddldepartmentBud.empty().append('<option selected="selected" value="0">Select Department</option>');
                ddlDepartmentDr.empty().append('<option selected="selected" value="0">Select Department</option>');

                ddlDepartmentCr.empty().append('<option selected="selected" value="0">Select Department</option>');
                //.append('<option selected="selected" value="0">Select Brancht</option>');;
                $.each(response, function () {
                    ddldepartment.append($("<option></option>").val(this['DEPTID']).html(this['DEPTNAME']));
                    ddldepartmentBud.append($("<option></option>").val(this['DEPTID']).html(this['DEPTNAME']));
                    ddlDepartmentDr.append($("<option></option>").val(this['DEPTID']).html(this['DEPTNAME']));
                    ddlDepartmentCr.append($("<option></option>").val(this['DEPTID']).html(this['DEPTNAME']));

                });




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
function BindCostCenter() {
    var ddlcentername = $("#ddlcentername");
    $("#dialog").dialog({
        autoOpen: true,
        modal: true,
        title: "Loading.."
    });
    $("#imgLoader").css("visibility", "visible");

    setTimeout(function () {
        $.ajax({
            type: "POST",
            url: "/Accountstran/BindCostCenter",
            data: '{}',
            async: false,
            dataType: "json",

            success: function (response) {
                //alert(JSON.stringify(response));
                ddlcentername.empty().append('<option selected="selected" value="0">NA</option>');
                //.append('<option selected="selected" value="0">Select Brancht</option>');;
                $.each(response, function () {
                    ddlcentername.append($("<option></option>").val(this['COSTCENTREID']).html(this['COSTCENTRENAME']));
                });




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

function ShowDialogcostcenter(modal) {
    $("#overlay").show();
    $("#trcostcenterdetails").fadeIn(300);

    if (modal) {
        $("#overlay").unbind("click");
    }
    else {
        $("#overlay").click(function (e) {
            HideDialogcostcenter();
        });
    }
}
function ShowDialoginvoice(modal) {
    $("#overlay").show();
    $("#trinvoicedetails").fadeIn(300);

    if (modal) {
        $("#overlay").unbind("click");
    }
    else {
        $("#overlay").click(function (e) {
            HideDialogcostcenter();
        });
    }
}

function costcenteradd() {
    var amtdr = 0;
    var ledgerid = $('#hdn_ledgercostcenter').val();

    var CostCatagoryID = $("#ddlcatagory").val();
    var BrandId = $("#ddlbrand").val();
    var ProductId = $("#ddlproduct").val();
    var DepartmentId = $("#ddldepartment").val();
    var BranchID = $("#ddlbranch").val();
    var CostCenterID = $("#ddlcentername").val();
    var CostCenterName = $('#ddlcentername').find('option:selected').text();
    var CostCatagoryName = $('#ddlcatagory').find('option:selected').text();
    var BrandName = $('#ddlbrand').find('option:selected').text();
    var ProductName = $('#ddlproduct').find('option:selected').text();
    var DepartmentName = $('#ddldepartment').find('option:selected').text();
    var FromDate = $('#txtcostcenterfromdate').val();
    var ToDate = $('#txtcostcentertodate').val();
    var BranchName = $('#ddlbranch').find('option:selected').text();
    var Amount = parseFloat($('#txtamount').val());
    var txntype = $('#hdn_accounttype').val();
    var ledgercostcenterid = $('#hdn_ledgercostcenter').val();
    var ledgercostcentername = $('#hdn_ledgercostcentername').val();
    var totalccamt = 0;

    var drcr = $('#hdn_drcr').val();
    if (drcr == 'Dr') {
        amtdr = parseFloat($('#txtAmtDr').val());
    }

    else
        if (drcr == 'Cr') {
            amtdr = parseFloat($('#txtAmntCr').val());
        }

    //if ($('#costcentertable').length) {
    //    $('#costcentertable tbody tr').each(function () {

    //        totalccamt = totalccamt + parseFloat($('td:eq(16) input', this).val());
    //        alert($(this).find('td:eq(16)').html());
    //    })
    //    alert('tt');
    //}

    if (CostCatagoryID == '0') {
        alert('select Cost Segment');
    }
    else
        if (DepartmentId == '0') {
            alert('select Department');
        }

        else
            if (BranchID == '0') {
                alert('Select Branch');
            }
            else


                if (Amount == '0' || Amount == '') {
                    alert('Enter Amount in cost center')
                }
                else

                    if (Amount > amtdr) {
                        alert('Amount exceeds Ledger value');
                    }

                    else {
                        $('#myModal').css("display", "");
                        setTimeout(function () {
                            $.ajax({

                                type: "POST",
                                url: "/Accountstran/addcostcenterdtl",
                                // data: "{'ledgerid': '" + ledgerid + "','CostCatagoryID': '" + CostCatagoryID + "','BrandId': '" + BrandId + "','ProductId': '" + ProductId + "','DepartmentId': '" + DepartmentId + "','BranchID': '" + BranchID + "','CostCenterID': '" + CostCenterID + "','CostCenterName': '" + CostCenterName + "','CostCatagoryName': '" + CostCatagoryName + "','BrandName': '" + BrandName + "','ProductName': '" + ProductName + "','DepartmentName': '" + DepartmentName + "','FromDate': '" + FromDate + "','ToDate': '" + ToDate + "','BranchName': '" + BranchName + "','Amount': '" + Amount + "','txntype': '" + txntype + "','ledgercostcentername': '" + ledgercostcentername + "'}",
                                data: { ledgerid: ledgerid, CostCatagoryID: CostCatagoryID, BrandId: BrandId, ProductId: ProductId, DepartmentId: DepartmentId, BranchID: BranchID, CostCenterID: CostCenterID, CostCenterName: CostCenterName, CostCatagoryName: CostCatagoryName, BrandName: BrandName, ProductName: ProductName, DepartmentName: DepartmentName, FromDate: FromDate, ToDate: ToDate, BranchName: BranchName, Amount: Amount, txntype: txntype, ledgercostcentername: ledgercostcentername },
                                //data: "{'ledgerid': '" + ledgerid + "','CostCatagoryID': '" + CostCatagoryID + "','BrandId': '" + BrandId + "','ProductId': '" + ProductId + "','DepartmentId': '" + DepartmentId + "'}",

                                dataType: "json",
                                success: function (response) {
                                    var counter = 0;

                                    var tableNewcost1 = '<table id="costcentertable" class="table table-striped table-bordered dt-responsive nowrap">';
                                    tableNewcost1 = tableNewcost1 + '<thead style="background-color:cornflowerblue;font-size:14px"><tr><th style="display:none"></th><th style="display:none"></th><th style="display:none"></th><th style="display:none"></th><th style="display:none"></th><th style="display:none"></th><th style="display:none"></th><th style="display:none"></th><th>CostCenter</th><th>Category</th><th>Brand</th><th>Product</th><th>Department</th> <th> FromDt</th><th>To Dt.</th><th>Branch</th><th>Amount</th><th></th></tr></thead><tbody>';
                                    counter = counter + 1;



                                    $.each(response, function () {

                                        tableNewcost1 = tableNewcost1 + "<tr><td style='display:none'>" + this["LedgerId"] + "</td><td style='display:none'>" + this["GUID"] + " </td><td style='display:none'>" + this["CostCatagoryID"] + "</td> <td style='display:none'>" + this["BrandId"] + "</td><td style='display:none'> " + this["ProductId"] + "</td> <td style='display:none'> " + this["DepartmentId"] + "</td> <td style='display:none'> " + this["BranchID"] + "</td> <td style='display:none'> " + this["CostCenterID"] + " </td> <td> " + this["CostCenterName"] + " </td> <td> " + this["CostCatagoryName"] + " </td> <td> " + this["BrandName"] + "</td> <td> " + this["ProductName"] + " </td> <td> " + this["DepartmentName"] + " </td> <td> " + this["FromDate"] + " </td> <td> " + this["ToDate"] + " </td> <td> " + this["BranchName"] + "  </td> <td> <input type='text' class='costcenteramt' id='txtamount' style='text-align:right' value='" + this["amount"] + "'/></td><td style='width:10%' ><input type='button' id='btncostcenterdel'  class='clscostdel' value='Del'/></td></tr>";


                                    })
                                    if (counter > 0) {
                                        $("#ddldepartment").prop("disabled", true);
                                        $("#ddlcatagory").prop("disabled", true);
                                    }
                                    document.getElementById("grdcostcenter").innerHTML = tableNewcost1 + '</tbody></table>';
                                    resetcostcenter();






                                    //var ddlsupplingdepot = $("[id*=ddlsupplingdepot]");
                                    //ddlsupplingdepot.empty().append('<option selected="selected" value="0">Please select</option>');
                                    //$.each(r.d, function () {
                                    //    ddlsupplingdepot.append($("<option></option>").val(this['Value']).html(this['Text']));
                                    //});
                                }
                            });
                            $('#myModal').css("display", "none");
                        }, 5);
                        e.preventDefault();

                    }


}
function resetcostcenter() {
    //  $("#ddlcatagory").val('0');
    $("#ddlbrand").val('0');

    // $("#ddlbranch").val('0');
    $("#ddlcentername").val('0');
    $("#txtcostcenterfromdate").val('');
    $("#txtcostcentertodate").val('');
    $("#txtamount").val('');

}
function costcentersave() {
    var amtdr = 0;
    var totalccamt = 0;
    var totalccamttemp
    var drcr = $('#hdn_drcr').val();

    if (drcr == 'Dr') {

        amtdr = parseFloat($('#txtAmtDr').val());
    }
    else
        if (drcr == 'Cr') {
            amtdr = parseFloat($('#txtAmntCr').val());
        }
    var departmentcc;

    departmentcc = $("#ddldepartment").val();

    var businesssegmentcc = $("#ddlcatagory").val();

    var department;
    var businesssegment;
    if (drcr == 'Dr') {

        department = $("#ddlDepartmentDr").val();

        if (department == '0') {

            $("#ddlDepartmentDr").val(departmentcc);

            $("#ddlDepartmentDr").chosen({
                search_contains: true
            });


            $("#ddlDepartmentDr").trigger("chosen:updated");
        }

        businesssegment = $("#ddlBusinessSegDr").val();
        if (businesssegment == '0') {
            $("#ddlBusinessSegDr").val(businesssegmentcc);
            $("#ddlBusinessSegDr").chosen({
                search_contains: true
            });


            $("#ddlBusinessSegDr").trigger("chosen:updated");
        }
    }
    else
        if (drcr == 'Cr') {
            department = $("#ddlDepartmentCr").val();
            if (department == '0') {
                $("#ddlDepartmentCr").val(departmentcc);
                $("#ddlDepartmentCr").chosen({
                    search_contains: true
                });


                $("#ddlDepartmentCr").trigger("chosen:updated");
            }
            businesssegment = $("#ddlBusinessSegCr").val();
            if (businesssegment == '0') {
                $("#ddlBusinessSegCr").val(businesssegmentcc);
                $("#ddlBusinessSegCr").chosen({
                    search_contains: true
                });


                $("#ddlBusinessSegCr").trigger("chosen:updated");
            }
        }
    var tempamt;
    $('#costcentertable tbody tr').each(function () {
        $(this).find('input').each(function () {
            //totalccamttemp = row.find("#txtTransactionAmount").val();
            //totalccamttemp=row.find("#txtTransactionAmount").val();
            //var xx=  $(this).find("input.txtamount").val();
            tempamt = $(this).val();
            if (tempamt != 'Del') {

                // totalccamt = totalccamt + parseFloat($(this).val());
                //totalccamt = totalccamt + parseFloat($(this).find("input.txtamount").val());
                totalccamt = totalccamt + parseFloat(tempamt);


                //totalccamt = totalccamt + parseFloat($(this).find('td:eq(15)').html());
            }
        })
    })


    if (totalccamt > amtdr) {

        alert('Amount Exceeding debit value');
    }
    else
        if (totalccamt < amtdr) {

            alert('Please check total cost center with ledger amount');
        }
        else {
            if (drcr == "Dr") {
                adddr();
            }
            else
                if (drcr == 'Cr') {
                    addcr();
                }

            $("#ddlcatagory").val('0');
            $("#ddlbrand").val('0');

            // $("#ddlbranch").val('0');
            $("#ddlcentername").val('0');
            $("#txtcostcenterfromdate").val('');
            $("#txtcostcentertodate").val('');
            $("#txtamount").val('');
            HideDialogcostcenter();
            e.preventDefault();

        }

}
function HideDialogcostcenter() {
    $("#overlay").hide();
    $("#trcostcenterdetails").fadeOut(300);
}
function HideDialoginvoice() {
    $("#overlay").hide();
    $("#trinvoicedetails").fadeOut(300);
}
//for file upload
function ShowDialogfile(modal) {
    $("#overlay").show();
    $("#dialogfile").fadeIn(300);
}
function HideDialogfile() {
    $("#overlay").hide();
    $("#dialogfile").fadeOut(300);
}
//for budget
function ShowDialog(modal) {
    $("#overlay").show();
    $("#dialogbud").fadeIn(300);

    if (modal) {
        $("#overlay").unbind("click");
    }
    else {
        $("#overlay").click(function (e) {
            HideDialog();
        });
    }
}
function HideDialog() {
    $("#overlay").hide();
    $("#dialogbud").fadeOut(300);
}
function addbudget() {
    var accdrvalue = $("#ddlAccTypeDr").val();
    var accountdrtext = $('#ddlAccTypeDr').find('option:selected').text();
    var deptvalue = $("#ddldepartmentBud").val();
    var depttext = $('#ddldepartmentBud').find('option:selected').text();
    $("#hdn_Department").val(deptvalue);
    ////var row = $("[id*=GridViewBudget] tr:last").clone();
    //   $("td:nth-child(1)", row).html(accdrvalue);
    //   $("td:nth-child(2)", row).html(accountdrtext);
    //   $("td:nth-child(3)", row).html(deptvalue);
    //    $("td:nth-child(4)", row).html(depttext);
    //   $("[id*=GridViewBudget] tbody").append(row);
    checkbudgetledgerexists(accdrvalue);
    if (deptvalue == '0') {
        alert('Provide Budget Department');
    }
    else {
        $.ajax({

            type: "POST",

            url: "/Accountstran/addbudgetdtl",

            // data: "{'acctypedr': '" + accdrvalue + "', 'acnamedr': '" + accountdrtext + "','deptid': '" + deptvalue + "','deptname': '" + depttext + "'}",
            data: { acctypedr: accdrvalue, acnamedr: accountdrtext, deptid: deptvalue, deptname: depttext },
            //contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {

                var tableNew = '<table id="budgetlist">';
                tableNew = tableNew + '<thead><tr><th>Ledger NameName</th> <th>Department name</th><th></th><th></th> <th><th></tr></thead> <tbody>';
                $.each(response, function () {


                    tableNew = tableNew + "<tr><td style='width:80px'>" + this["LedgerName"] + "</td><td>" + this["DepartmentName"] + "</td><td style='display:none'>" + this["LedgerId"] + "</td><td style='display:none'>" + this["DepartmentId"] + "</td></tr > "

                });
                document.getElementById("addbudget").innerHTML = tableNew + '</tbody></table>';
                //alert('test')        





            }

        });
        $("#ddldepartmentBud").val('0');
    }


}
function checkbudgetledgerexists(accdr) {
    $.ajax({

        type: "POST",

        url: "/Accountstran/checkbudgetledgerexists",

        data: "{'accdr': '" + accdr + "'}",
        data: { accdr: accdr },
        //contentType: "application/json; charset=utf-8",
        success: function (response) {
            var chk;
            $.each(response, function () {
                chk = this["response"];



            })


        }

    });

}
function checkwxpenseledgerornot(accdrvalue, drcr) {
    $.ajax({

        type: "POST",

        url: "/Accountstran/checkwxpenseledgerornot",

        //data: "{'acctypedr': '" + accdrvalue + "'}",
        data: "{'acctypedr': '" + accdrvalue + "'}",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            var chk = '0';
            $.each(response, function () {
                chk = this['response'];
            })
            if (chk == "1") {

                $("#hdn_costcenterapplicable").val('N')
                isexpenseledger = '1';
                //$('#div_BudgetSave').css("display", "block");
                //$('#divbudgetadd').css("display", "block");
                //$('#dept2').css("display", "block");
                //$('#dept1').css("display", "block");

                //$('#btnbudgetcncl').css("display", "none");
                //ShowDialog(true);
                //e.preventDefault();
                if (drcr == 'Dr') {
                    $('#trDeptDr').css("display", "");
                    $("#ddlDepartmentDr").chosen({
                        search_contains: true
                    });


                    $("#ddlDepartmentDr").trigger("chosen:updated");
                    $("#ddlBusinessSegDr").chosen({
                        search_contains: true
                    });


                    $("#ddlBusinessSegDr").trigger("chosen:updated");
                }
                else
                    if (drcr == 'Cr') {

                        $('#trDeptCr').css("display", "");
                        $("#ddlDepartmentCr").chosen({
                            search_contains: true
                        });


                        $("#ddlDepartmentCr").trigger("chosen:updated");
                        $("#ddlBusinessSegCr").chosen({
                            search_contains: true
                        });


                        $("#ddlBusinessSegCr").trigger("chosen:updated");
                    }
            }

            else {
                HideDialog();
                e.preventDefault();

            }




        }

    });

}
//bind voucher header
function bindvoucherhdr() {

    var FromDate = $('#txtsearchfromdate').val();
    var ToDate = $('#txtsearchtodate').val();
    var VoucherID = $('#hdnvoucherid').val();
    var DepotID = $("#BRID").val();
    var checker = $('#hdnCHECKER').val();
    var MTClaim = $('#hdnmtclaim').val();

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

            url: "/Accountstran/BindVoucherDetails",

            // data: "{'FromDate': '" + FromDate + "', 'ToDate': '" + ToDate + "','VoucherID': '" + VoucherID + "','DepotID': '" + DepotID + "','checker': '" + checker + "'}",
            data: { FromDate: FromDate, ToDate: ToDate, VoucherID: VoucherID, DepotID: DepotID, checker: checker, IsMTClaim: MTClaim, finyr: FINYEAR, userid: UserID },

            dataType: "json",
            success: function (response) {

                var tableNew = '<table id="gvparentvoucher" class="table table-striped table-bordered dt-responsive nowrap" style="width:100%">';
                tableNew = tableNew + '<thead style="background-color:cornflowerblue;font-size:14px"><tr><th style="display:none"><th style="display:none"></th><th> Sl.</th><th>Voucher Type</th><th> Voucher No.</th><th>Date</th><th>Region</th><th>Party</th><th>Amount</th><th>Approved</th><th>Day End</th><th>Print</th><th>Edit</th><th>Del</th></tr></thead> <tbody>';



                $.each(response, function () {

                    counter = counter + 1;
                    tableNew = tableNew + "<tr><td style='display:none'>" + this["AccEntryID"] + "</td> <td style='display:none'>" + this["TDSRelated"] + "</td>     <td>" + counter + "</td>   <td>" + this["VoucherTypeName"] + "</td>   <td>" + this["VoucherNo"] + "</td><td>" + this["Date"] + "</td>   <td>" + this["BranchName"] + "</td><td>" + this["LedgerName"] + "</td> <td>" + this["AMOUNT"] + "</td>  <td>" + this["VoucherApproved"] + "</td><td>" + this["DAYENDTAG"] + "</td>    <td><input type='image' class='gvprint' id='btnprint'  src='../images/print.png ' width='30' height ='30'/></td><td><input type='image' class='gvedit' id='btnedit' src='../Images/Pencil-icon.png' title='Edit'/></td>     <td><input type='image' class='gvdel' id='btnDel' src='../Images/ico_delete_16.png' title='Delete' /></td> </tr>";

                });
                document.getElementById("DivHeaderRow").innerHTML = tableNew + '</table>';

                $('#gvparentvoucher').DataTable({
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

function editrec(voucherid) {
    ReleaseSession();
    $('#pnlAdd').css("display", "block");

    $('#pnlDisplay').css("display", "none");

    $('#trvoucherno').css("display", "");
    $('#debitadd').find('tbody').detach();
    $('#debitadd').append($('<tbody>'));

    $('#creditadd').find('tbody').detach();
    $('#creditadd').append($('<tbody>'));
    //at the time of adv vouvher
    if (VOUCHERID == '15' || VOUCHERID == '16') {
        $('#trpaymentmode').css("display", "");
        //    $('#trcreditbank').css("display", "none");
        //    $('#trcreditchequeno').css("display", "");

    }
    else {
        $('#trpaymentmode').css("display", "none");
        $('#ddlMode').val('C');
        //$('#trcreditchequeno').css("display", "none");

        //    $('#trcreditbank').css("display", "none");
        //    $('#trcreditchequeno').css("display", "none");
    }
    /// $('#trdebitchequeno').css("display", "none");
    //$('#trcreditchequeno').css("display", "none");
    $("#btnaddhide").css("display", "none");
    //$('#trcreditbank').css("display", "none");
    $("#trdebitbank").css("display", "none");
    $("#ddlvouchertype").prop("disabled", true);


    $("#gvparentvoucher").remove();
    $("#hdnOnlyBillTagFromPage").val('N');
    BindVoucher();
    BindVoucherType();
    $("#ddlregion").prop("disabled", true);

    $("#ddlregion").chosen({
        search_contains: true
    });


    $("#ddlregion").trigger("chosen:updated");
    $("#ddlMode").chosen({
        search_contains: true
    });

    $("#ddlMode").trigger("chosen:updated");


    //$("#ddldepartmentBud").chosen();
    //$("#ddldepartmentBud").trigger("chosen:updated");
    $("#ddlGSTVoucherType").chosen();
    // $("#rdbcreditpaymenttype").chosen();
    var isapproved = $('#hdn_approved').val();
    if (isapproved == 'Yes') {

        $('#btndivsave').css("display", "none");

    }
    else
        $('#btndivsave').css("display", "");
    $.ajax({

        type: "POST",

        url: "/Accountstran/editdtl",

        // data: "{'voucherid': '" + voucherid + "'}",
        data: { voucherid: voucherid },
        dataType: "json",
        success: function (response) {

            $.each(response, function () {


                $("#txtvoucherno").val(this["VoucherNo"]);
                $("#txtvoucherdate").val(this["Date"]);

                var vouchertype = this["IsGSTVoucher"];
                if (vouchertype == 'Y') {
                    $('#btnVendorDetails').css("display", "");
                    $("#ddlGSTVoucherType").val('GSTGovtVoucher');
                    $('#divVendorDetails').css("display", "");
                    $("#ddlGSTVoucherType").prop("disabled", true);
                    $("#ddlGSTVoucherType").chosen({
                        search_contains: true
                    });

                    $("#ddlGSTVoucherType").trigger("chosen:updated");

                    $("#hdnvouchertypegst").val('GSTGovtVoucher');
                }

                else
                    if (vouchertype == 'G') {
                        $('#btnVendorDetails').css("display", "");
                        $("#ddlGSTVoucherType").val('GSTVoucher');
                        $('#divVendorDetails').css("display", "");
                        $("#ddlGSTVoucherType").prop("disabled", true);
                        $("#ddlGSTVoucherType").chosen({
                            search_contains: true
                        });

                        $("#ddlGSTVoucherType").trigger("chosen:updated");

                        $("#hdnvouchertypegst").val('GSTGovtVoucher');
                    }

                    else {
                        $("#ddlGSTVoucherType").val('NormalVoucher');
                        $('#btnVendorDetails').css("display", "none");
                        $("#ddlGSTVoucherType").prop("disabled", true);
                        $("#ddlGSTVoucherType").chosen({
                            search_contains: true
                        });

                        $("#ddlGSTVoucherType").trigger("chosen:updated");
                        $("#hdnvouchertypegst").val('NormalVoucher');
                    }


                $("#ddlvouchertype").val(this["VoucherTypeID"]);
                $("#ddlregion").chosen({
                    search_contains: true
                });
                $("#ddlregion").trigger("chosen:updated");

                $("#ddlregion").val(this["BranchID"]);
                $("#ddlregion").chosen({
                    search_contains: true
                });
                $("#ddlregion").trigger("chosen:updated");


                $("#txtNarration").val(this["Narration"]);
                $("#txtbillno").val(this["BillNo"]);
                $("#txtbilldate").val(this["BillDate"]);
                $("#txtgrno").val(this["GRNo"]);
                $("#txtgrdate").val(this["GRDate"]);
                $("#txtvehicleno").val(this["VehicleNo"]);
                $("#txttransport").val(this["Transport"]);
                $("#txtwaybillno").val(this["WayBillNo"]);
                $("#txtwaybilldate").val(this["WayBillDate"]);

                if ($('#hdnmtclaim').val() == 'Y') {
                    BindAccountTypecr_MTClaim(this["VoucherTypeID"], this["BranchID"]);
                    BindAccountTypedr_MTClaim(this["VoucherTypeID"], this["BranchID"]);

                    var ddlapprovalledger = $("#ddlapprovalledger");

                    if (this["MTClaimChecker1"] == 'N') {

                        $("#btnapproved").prop("value", "Checked");
                    }
                    else if (this["MTClaimChecker1"] == 'Y' && this["MTClaimChecker2"] == 'N') {

                        ddlapprovalledger.empty();
                        ddlapprovalledger.append($("<option></option>").val(this['MTClaimLedgerID']).html(this['MTClaimLedgerName']));
                        $("#ddlapprovalledger").chosen({
                            search_contains: true
                        });

                        $("#ddlapprovalledger").trigger("chosen:updated");

                        $("#btnapproved").prop("value", "Confirmed");
                    }
                    else if (this["MTClaimChecker1"] == 'Y' && this["MTClaimChecker2"] == 'Y' && this["MTClaimChecker3"] == 'N') {

                        ddlapprovalledger.empty();
                        ddlapprovalledger.append($("<option></option>").val(this['MTClaimLedgerID']).html(this['MTClaimLedgerName']));
                        $("#ddlapprovalledger").chosen({
                            search_contains: true
                        });

                        $("#ddlapprovalledger").trigger("chosen:updated");

                        $("#btnapproved").prop("value", "Approved");
                    }
                }
                else {
                    BindAccountTypedr(this["VoucherTypeID"], this["BranchID"]);
                    BindAccountTypecr(this["VoucherTypeID"], this["BranchID"]);
                }
                binddr();
                bindcr();
                //bindbudget();

            })
        }
    })
}
function Viewgst() {

    var stateid1;
    var groupname;
    var partyname;
    var partygstno;
    var counter = 0;
    var _gstamt = 0;
    var _notaxamt = 0;

    BindAllState();
    BindGSTGroup();
    $.ajax({

        type: "POST",

        url: "/Accountstran/Viewgst",

        // data: "{'GroupId': '" + GroupId + "', 'PartyID': '" + PartyID + "','InvoiceNo': '" + InvoiceNo + "','InvoiceDate': '" + InvoiceDate + "','Taxable': '" + Taxable + "','TaxableValue': '" + TaxableValue + "','HSNCode': '" + HSNCode + "','PartyTrade': '" + PartyTrade + "','StateID': '" + StateID + "','GSTNo': '" + GSTNo + "','TaxTypeID': '" + TaxTypeID + "','TaxType': '" + TaxType + "','TaxAmount': '" + TaxAmount + "','NetAmount': '" + NetAmount + "','Taxable1': '" + Taxable1 + "','TaxAmount1': '" + TaxAmount1 + "','TaxTypeID1': '" + TaxTypeID1 + "','Taxtype1': '" + Taxtype1 + "','igst': '" + igst + "'}",
        data: '{}',
        async: false,
        dataType: "json",
        success: function (response) {

            var nettotal = 0;
            var tableNew = '<table id="grdgstnew" class="reportgrid">';
            tableNew = tableNew + "<thead style='background-color:cornflowerblue;font-size:14px'><tr><th style='display:none'></th><th style='display:none'></th><th style='display:none'></th><th>SL</th> <th>HSN CODE </th><th>TAX TYPE1</th> <th>TAX TYPE2</th> <th>TAXABLE (%)</th> <th>TAXABLE VALUE</th> <th>TAX AMOUNT</th> <th>NET AMOUNT</th><th style=width:80px'></th> </tr></thead><tbody>";
            $.each(response, function () {
                counter = counter + 1;
                tableNew = tableNew + "<tr><td style='display:none'>" + this["GroupId"] + "</td><td style='display:none'>" + this["PartyID"] + "</td><td style='display:none'>" + this["GUID"] + "</td><td>  " + counter + "</td> <td>" + this["HSNCode"] + "</td><td>" + this["TaxType"] + "</td><td>" + this["TaxType1"] + "</td><td>" + this["Taxable1"] + "</td><td>" + this["TaxAmount"] + "</td><td>" + this["NetAmount"] + "</td><td style='width:10px'><input type='button' id='btndebitgrddelete'  class='gstdel' value='Del'/></td>";
                _gstamt = _gstamt + parseFloat(this["NetAmount"]);
                _notaxamt = parseFloat(this["NonTaxableAmountGST"]);
                $("#ddlStateNamenew").val(this["StateID"]);
                $("#ddlToStateName").val(this["PlaceOfSupply"]);
                groupname = this["GroupId"];
                partyname = this["PartyID"];
                partygstno = this["GSTNo"];
                $("#txtDCJ_HSNCodenew").val(this["HSNCode"]);
                $("#txtDCJ_InvoiceNonew").val(this["InvoiceNo"]);
                $("#txtDCJ_InvoiceDatenew").val(this["InvoiceDate"]);

                $("#txtDCJ_PartyTradenew").val(this["PartyTrade"]);
                $("#txtDCJ_TaxAmount").val(this["TaxAmount"]);

                $("#txtDCJ_NetAmount").val(this["NetAmount"]);
                $("#txtDCJ_NonTaxableAmount").val(this["NonTaxableAmountGST"]);

            })
            document.getElementById("GridView_DCJ_GST").innerHTML = tableNew + '</tbody></table>';
            _gstamt = _gstamt + _notaxamt;
            var totalgst = 0;
            var netamt = 0;
            //var _totgst = parseFloat($("#txtGSTTotal").val());




            //   totalgst = netamt + _totgst;
            //$("#txtGSTTotal").val(totalgst);

            //$("#txtDCJ_HSNCodenew").val('');
            //$("#txtDCJ_TaxableValuenew").val('');
            //$("#txtDCJ_ISIGST").val('');
            //$("#txtDCJ_TaxType1").val('');
            //$("#txtDCJ_TaxableValue1").val('');
            //$("#txtDCJ_TaxAmount").val('');
            //$("#txtDCJ_NetAmount").val('');
            //$("#ddlDCJ_TaxType").val('0');
            //$("#txtDCJ_Taxablenew").val('0');
            //$("#txtRoundOff").val('0');


        }

    })



    $("#ddlDCJ_GroupName").val(groupname);

    BindDCJ_Partyedit(groupname);
    //BindDCJ_Party();
    $("#ddlDCJ_Partynew").val(partyname);
    BindDCJ_GstNo();
    $("#ddlDCJ_GstNonew").val(partygstno);
    $("#txtGSTTotal").val(_gstamt);
    if (counter > 0) {
        //  netamt = parseFloat($("#txtDCJ_NetAmount").val());
        $("#ddlStateNamenew").prop("disabled", true);
        $("#ddlDCJ_GroupName").prop("disabled", true);
        $("#ddlDCJ_Partynew").prop("disabled", true);
        $("#ddlDCJ_GstNonew").prop("disabled", true);
        $("#txtDCJ_InvoiceNonew").attr("disabled", "disabled");
        $("#txtDCJ_PartyTradenew").attr("disabled", "disabled");
        $("#txtDCJ_InvoiceNonew").attr("disabled", "disabled");
    }
    else {
        //netamt = parseFloat($("#txtDCJ_NetAmount").val()) + parseFloat($("#txtDCJ_NonTaxableAmount").val())
    }



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
