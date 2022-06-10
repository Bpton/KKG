/*
 * Developer Info :Pritam Basu
 * start date:05.05.2020    
 * end date:09.05.2020
 * Pagename :Damage Claim for asm 
 */




var ClaimType;
var MENUID;
$(document).ready(function () {
    var qs = getQueryStrings();
    if (qs["CTYPE"] != undefined && qs["CTYPE"] != "") {

        ClaimType = qs["CTYPE"];
    }
    if (qs["MNID"] != undefined && qs["MNID"] != "") {

        MENUID = qs["MNID"];
    }
    var currentdt;
    var frmdate;
    var todate;
    $('#pnlAdd').css("display", "none");
    //date validation
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
    $("#txtclaimdate").datepicker({
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        selectCurrent: true,
        todayBtn: "linked",
        showAnim: "slideDown",
        yearRange: "-1:+0",
        maxDate: new Date(currentdt),
        minDate: new Date(currentdt),
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
    $("#txtStartDate").datepicker({
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        selectCurrent: true,
        todayBtn: "linked",
        showAnim: "slideDown",
        yearRange: "-1:+0",
        //maxDate: new Date(currentdt),
        //minDate: new Date(currentdt),
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
    $("#txtEndDate").datepicker({
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        selectCurrent: true,
        todayBtn: "linked",
        showAnim: "slideDown",
        yearRange: "-1:+0",
        //maxDate: new Date(currentdt),
        //minDate: new Date(currentdt),
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
    $("#txtInvoiceDate").datepicker({
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        selectCurrent: true,
        todayBtn: "linked",
        showAnim: "slideDown",
        yearRange: "-1:+0",

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


    $("#txtfromdate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
    $("#txttodate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
    $("#txtclaimdate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());

    LoadDamageClaim();
    BindDepotByUserID();
    BindClaimStatus();

    $("#ddlsearchdepot").chosen({
        search_contains: true
    });

    $("#ddlsearchdepot").trigger("chosen:updated");

    $("#Btnadd").click(function (e) {

        addnew();

    });

    $("#btnClose").click(function (e) {

        close();

    });

    $('#ddlbusinesssegment').change(function () {
        BindDistributorByDepot();
    });

    $('#ddldistributor').change(function () {
        var bsid = $("#ddlbusinesssegment").val();
        if (bsid == '7F62F951-9D1F-4B8D-803B-74EEBA468CEE') {
            BindGroupByDistributor();
            BindClaimNarration();
        }
        BindRetailer();
    });

    $('#txtnarration').change(function () {
        BindClaimPeriod();
    })

    $('#btnADDGrid').click(function () {

        addgrid();
    })

    $('#txtCategory').change(function () {
        loadProduct();
    });

    $('#ddlProduct').change(function () {
        LoadPacksize();
        debugger;
        BindBatch();
    });

    $("body").on("click", "#grdProduct .grvdtldel", function () {

        if (confirm("Do you want to delete this Account?")) {
            var row = $(this).closest("tr");
            var GUID = row.find('td:eq(0)').html();
            delgrid(GUID);
        }


    });

    $('#btnSave').click(function () {
        SaveDamageClaim();
    })

    $("#btnvouchersearch").click(function () {
        BindDamage();
    });

  //  Bindqpsinitial();

    $("body").on("click", "#gvqpsclaim .gvedit", function () {
        var ClaimID;
        var processnumer;
        row = $(this).closest("tr");
        ClaimID = row.find('td:eq(0)').html();
        processnumer = row.find('td:eq(5)').html();
        $("#hdnClaimID").val(ClaimID);
        if (processnumer != '') {
            $("#btnSave").hide();
        }
        else {
            $("#btnSave").show();
        }
        editrec(ClaimID);

    });

    $('input[type="radio"]').click(function () {

        var inputValue = $(this).attr("value");
        if (inputValue == 'IA') {
            $('#Grosswise:input').attr('disabled', false);
            $("#Grosswise").prop('checked', false);
            $("#divinvoice").show();
            $("#hdnClaimtype").val('IA');
        }
        else {
            $('#Invoicewise:input').attr('disabled', false);
            $("#Invoicewise").prop('checked', false);
            $("#divinvoice").hide();
            $("#hdnClaimtype").val('GA');
        }

    });

})

function editrec(ClaimID) {
    ReleaseSession();

    $('#pnlAdd').css("display", "block");

    $('#pnlDisplay').css("display", "none");
    $('#tr_claimno').css("display", "");
    $.ajax({

        type: "POST",

        url: "/claims/EditDamage",

        //data: "{'voucherid': '" + voucherid + "','voucherapproived': '" + voucherapproived + "','dayend': '" + dayend + "','tdsrelated': '" + tdsrelated + "'}",
        data: { ClaimID: ClaimID },
        async: false,
        //data: {voucherid: voucherid},
        dataType: "json",
        success: function (response) {
            var tag;
            var REVERSECHARGE;
            var TDSAPPLICABLE;
            $.each(response, function () {
                $("#ddldepot").val(this["depotid"]);
                $("#ddldepot").prop("disabled", true);
                $("#ddldepot").chosen({
                    search_contains: true
                });



                $("#ddldepot").trigger("chosen:updated");
                BindBusinessSegment();
                $("#ddlbusinesssegment").val(this["bsid"]);
                $("#ddlbusinesssegment").chosen({
                    search_contains: true
                });



                $("#ddlbusinesssegment").trigger("chosen:updated");
                BindClaimNarration();
                BindDistributorByDepot();
                $("#ddldistributor").val(this["distid"]);
                $("#ddldistributor").chosen({
                    search_contains: true
                });

                $("#ddldistributor").trigger("chosen:updated");

                BindGroupByDistributor();
                $("#ddlgroup").val(this["grid"]);
                $("#ddlgroup").chosen({
                    search_contains: true
                });

                $("#ddlgroup").trigger("chosen:updated");
                BindRetailer();
                $("#ddlretailer").val(this["retrid"]);
                $("#ddlretailer").chosen({
                    search_contains: true
                });
                $("#ddlretailer").trigger("chosen:updated");
                LoadCategory();
                $("#txtCategory").chosen({
                    search_contains: true
                });
                $("#txtCategory").trigger("chosen:updated");
                $("#ddlProduct").chosen({
                    search_contains: true
                });
                $("#ddlProduct").trigger("chosen:updated");
                $("#txtPackSize").chosen({
                    search_contains: true
                });
                $("#txtPackSize").trigger("chosen:updated");
                $("#txtBatchno").chosen({
                    search_contains: true
                });
                $("#txtBatchno").trigger("chosen:updated");
                LoadReason();
                $("#ddlReason").chosen({
                    search_contains: true
                });
                $("#ddlReason").trigger("chosen:updated");
                 
                $("#txtRemarks").val(this["remarks"]);
                $("#txtclaimdate").val(this["date"]);
                $("#txtNetAmt").val(this["Amount"]);

                $("#txtprocessno").val(this["processno"]);
                $("#txtshowclaimno").val(this["claimno"]);
                debugger;
                tag = this["tag"];
                if (tag == 'GA') {
                    $("#Grosswise").prop("checked", true);
                    $("#Invoicewise").prop('checked', false);
                    $("#divinvoice").hide();
                    $("#hdnClaimtype").val('GA');
                }
                else  if (tag == 'IA') {
                        $("#Invoicewise").prop("checked", true);
                        $("#Grosswise").prop('checked', false);
                        $("#divinvoice").show();
                        $("#hdnClaimtype").val('IA');
                }

                Editclaimdetail();
            })
        }
    })
}

function Editclaimdetail() {
    $.ajax({

        type: "POST",
        //  url: "frmaccvouchernew2.aspx/addvouchercr1",
        url: "/claims/EditDamgaedtl",
        data: '{}',
        async: false,
        //data: "{'AccountID': '" + AccountID + "','AccountName': '" + AccountName + "'}",
        //data: "{'ledgerid': '" + ledgerid + "','CostCatagoryID': '" + CostCatagoryID + "','BrandId': '" + BrandId + "','ProductId': '" + ProductId + "','DepartmentId': '" + DepartmentId + "'}",

        dataType: "json",
        success: function (response) {
            var tableNew = '<table id="grdProduct" class="table table-striped table-bordered dt-responsive nowrap">';
            tableNew = tableNew + '<thead ><tr><th style="display:none"></th><th >Sl.No.</th><th>PRODUCT</th><th>PACKSIZE</th><th>CATEGORY</th><th>BATCH NO</th><th>INVOICE NO</th> <th>INVOICE DATE</th><th>MRP</th><th>CLAIM (%)</th><th>BASE RATE</th><th>CLAIM QTY</th><th>INVOICE QTY</th><th>AMOUNT</th><th>REASON</th><th>NARRATION</th><th>DELETE</th></tr></thead><tbody>';
            var totalamt = 0;
            var grossamount = 0;
            var counter = 0;

            $.each(response, function () {
                countr = counter + 1;
                tableNew = tableNew + "<tr><td style='display:none'>" + this["GUID"] + "</td><td>" + counter + "</td><td style='display:none'>" + this["CLAIM_PRODUCT_ID"] + "</td> <td>" + this["CLAIM_PRODUCT_NAME"] + "</td> <td style='display:none'>" + this["CLAIM_PACKSIZE_ID"] + "</td>  <td>" + this["CLAIM_PACKSIZE_NAME"] + "</td><td style='display:none'>" + this["CLAIM_CAT_ID"] + "</td><td>" + this["CLAIM_CAT_NAME"] + "</td><td>" + this["BATCHNO"] + "</td> <td>" + this["INVOICE_NO"] + "</td><td>" + this["INVOICE_DATE"] + "</td><td>" + this["MRP"] + "</td><td>" + this["DISCOUNT_PRICE"] + "</td><td>" + this["BASE_PRICE"] + "</td><td>" + this["CLAIM_QTY"] + "</td><td>" + this["TOTAL_INVOICE_QTY"] + "</td><td>" + this["AMOUNT"] + "</td><td style='display:none'>" + this["REASON_ID"] + "</td><td>" + this["REASON"] + "</td><td>" + this["NARRATION"] + "</td>   <td><input type='image' class='grvdtldel' id='btnDtldel' src='../Images/ico_delete_16.png' title='Delete' /></td>";

                totalamt = totalamt + parseFloat(this["AMOUNT"]);
                grossamount = grossamount + parseFloat(this["AMOUNT"]);

            })
            document.getElementById("grdProduct").innerHTML = tableNew + '</tbody></table>';
            $("#txtNetAmt").val(totalamt);
        }
    })
}

function delgrid(guid) {
    $.ajax({

        type: "POST",
        //  url: "frmaccvouchernew2.aspx/addvouchercr1",
        url: "/claims/deleTempDamageGrid",
        data: {
            GUID: guid
        },

        //data: "{'AccountID': '" + AccountID + "','AccountName': '" + AccountName + "'}",
        //data: "{'ledgerid': '" + ledgerid + "','CostCatagoryID': '" + CostCatagoryID + "','BrandId': '" + BrandId + "','ProductId': '" + ProductId + "','DepartmentId': '" + DepartmentId + "'}",

        dataType: "json",
        success: function (response) {
            var tableNew = '<table id="grdProduct" class="table table-striped table-bordered dt-responsive nowrap">';
            tableNew = tableNew + '<thead ><tr><th style="display:none"></th><th >Sl.No.</th><th>PRODUCT</th><th>PACKSIZE</th><th>CATEGORY</th><th>BATCH NO</th><th>INVOICE NO</th> <th>INVOICE DATE</th><th>MRP</th><th>CLAIM (%)</th><th>BASE RATE</th><th>CLAIM QTY</th><th>INVOICE QTY</th><th>AMOUNT</th><th>REASON</th><th>NARRATION</th><th>DELETE</th></tr></thead><tbody>';
            var totalamt = 0;
            var counter = 0;

            var iscostcenter;
            $.each(response, function () {
                tableNew = tableNew + "<tr><td style='display:none'>" + this["GUID"] + "</td><td>" + counter + "</td><td style='display:none'>" + this["CLAIM_PRODUCT_ID"] + "</td> <td>" + this["CLAIM_PRODUCT_NAME"] + "</td> <td style='display:none'>" + this["CLAIM_PACKSIZE_ID"] + "</td>  <td>" + this["CLAIM_PACKSIZE_NAME"] + "</td><td style='display:none'>" + this["CLAIM_CAT_ID"] + "</td><td>" + this["CLAIM_CAT_NAME"] + "</td><td>" + this["BATCHNO"] + "</td> <td>" + this["INVOICE_NO"] + "</td><td>" + this["INVOICE_DATE"] + "</td><td>" + this["MRP"] + "</td><td>" + this["DISCOUNT_PRICE"] + "</td><td>" + this["BASE_PRICE"] + "</td><td>" + this["CLAIM_QTY"] + "</td><td>" + this["TOTAL_INVOICE_QTY"] + "</td><td>" + this["AMOUNT"] + "</td><td style='display:none'>" + this["REASON_ID"] + "</td><td>" + this["REASON"] + "</td><td>" + this["NARRATION"] + "</td>   <td><input type='image' class='grvdtldel' id='btnDtldel' src='../Images/ico_delete_16.png' title='Delete' /></td>";

                totalamt = totalamt + parseFloat(this["AMOUNT"]);
                grossamount = grossamount + parseFloat(this["AMOUNT"]);



            })
            document.getElementById("grdProduct").innerHTML = tableNew + '</tbody></table>';
            $("#txtNetAmt").val(totalamt);
            $("#txtGrossAmt").val(grossamount);
        }
    });
}

function BindGroupByDistributor() {
    var DistributorID = $("#ddldistributor").val();
    var BSID = $("#ddlbusinesssegment").val();
    var ddlgroup = $("#ddlgroup");

    $.ajax({
        type: "POST",
        url: "/claims/BindGroupByDistributor",
        data: { DistributorID: DistributorID, BSID: BSID },
        async: false,
        dataType: "json",
        success: function (response) {
            //alert(JSON.stringify(response));
            ddlgroup.empty()
            //.append('<option selected="selected" value="0">Select </option>');
            $.each(response, function () {
                ddlgroup.append($("<option></option>").val(this['GROUPID']).html(this['GROUPNAME']));
            });
            $("#ddlgroup").chosen({
                search_contains: true
            });


            $("#ddlgroup").trigger("chosen:updated");
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function LoadDamageClaim() {

    var counter = 0;
    $.ajax({


        type: "POST",

        url: "/claims/BindDamageClaim",

        // data: "{'FromDate': '" + FromDate + "', 'ToDate': '" + ToDate + "','VoucherID': '" + VoucherID + "','DepotID': '" + DepotID + "','checker': '" + checker + "'}",
        data: { claimtype: ClaimType},

        dataType: "json",
        success: function (response) {
            var tableNew = '<table id="gvqpsclaim" class="table table-striped table-bordered dt-responsive nowrap" style="width:100%">';
            tableNew = tableNew + '<thead style="background-color:cornflowerblue;font-size:14px"><tr id="header"><th style="display:none"><th> Sl.</th><th>Edit</th><th>REG NO. </th><th> REG DATE.</th><th>CN NO.</th><th>PROCESS DATE</th><th style="display:none">CLAIM BUSINESSSEGMENT</th><th>BUSINESS SEGMENT</th> <th style="display:none">PRINCIPLE GROUPID</th><th>DEPOT </th><th style="display:none">DISTRIBUTORID</th><th>DISTRIBUTOR</th><th style="display:none">RETAILERID</th><th style="display:none">RETAILER</th><th>CLAIM AMT</th><th>PASSED AMT</th><th>NARRATION</th><th>STATUS</th><th >DOCUMENT RECEIVED DATE</th><th>DOCUMENT STATUS</th><th>REMARKS</th><th>DEDUCTION DETAILS</th><th>Print</th><th>Del</th></tr></thead> <tbody>';




            $.each(response, function () {
                debugger;
                if (this["CURRENTSTATUS"] == "Confirmed") {
                    current_status = 'current_status';
                }
                else {
                    current_status='';
                }


                counter = counter + 1;
                tableNew = tableNew + "<tr><td style='display:none'>" + this["ID"] + "</td> <td>" + counter + "</td> <td><input type='image' class='gvedit' id='btnedit' src='../Images/Pencil-icon.png' title='Edit'/></td>     <td class='" + current_status +"'>" + this["DAM_CLM_NO"] + "</td>   <td>" + this["CLAIM_DATE"] + "</td><td class='" + current_status+"'>" + this["PROCESSNUMBER"] + "</td>   <td>" + this["PROCESS_DATE"] + "</td><td style='display:none'>" + this["CLAIM_BUSINESSSEGMENT_ID"] + "</td> <td>" + this["CLAIM_BUSINESSSEGMENT_NAME"] + "</td>  <td style='display:none'>" + this["CLAIM_PRINCIPLEGROUP_ID"] + "</td><td>" + this["DEPOTNAME"] + "</td><td style='display:none'>" + this["CLAIM_DISTRIBUTOR_ID"] + "</td><td>" + this["CLAIM_DISTRIBUTOR_NAME"] + "</td><td style='display:none'>" + this["CLAIM_RETAILER_ID"] + "</td> <td style='display:none'>" + this["CLAIM_RETAILER_NAME"] + "</td><td>" + this["AMOUNT"] + "</td><td>" + this["CLAIM_MODI_AMT"] + "</td> <td>" + this["NARRATION"] + "</td><td class='" + current_status +"'>" + this["CURRENTSTATUS"] + "</td><td>" + this["DOCUMENTSRECEIVEDDATE"] + "</td><td>" + this["DOCUMENTSTATUS"] + "</td> <td>" + this["REMARKS"] + "</td><td>" + this["DEDUCTIONDETAILS"] + "</td>    <td><input type='image' class='gvprint' id='btnprint'  src='../images/print.png ' width='30' height ='30'/></td>  <td><input type='image' class='gvdel' id='btnDel' src='../Images/ico_delete_16.png' title='Delete' /></td> </tr>";

            });
            document.getElementById("DivHeaderRow").innerHTML = tableNew + '</table>';

            $('#gvqpsclaim').DataTable({
                "sScrollX": '100%',
                "sScrollXInner": "110%",
                "scrollY": "238px",
                "scrollCollapse": true,
                "dom": 'Bfrtip',
                buttons: [
                    {
                        extend: 'excelHtml5',
                        title: 'Damage Claim'
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
}

function BindDamage() {

    var depotid = $("#ddlsearchdepot").val();
    //var status1 = $("#ddlstatus").val();
    var fromdate = $("#txtfromdate").val();
    var todate = $("#txttodate").val();
    //var party = $("#ddlparty").val();
    var status = $("#ddlstatus option:selected");
    var party = $("#ddlparty option:selected");
    var status1 = "";
    status.each(function () {
        status1 += $(this).val() + ',';
    });
    var res = status1.substring(0, status1.length - 1);

    var party1 = "";
    party.each(function () {
        party1 += $(this).val() + ',';
    });
    var res1 = party1.substring(0, party1.length - 1);


    var counter = 0;
    $.ajax({


        type: "POST",

        url: "/claims/BindDamageClaim_Filtering",

        // data: "{'FromDate': '" + FromDate + "', 'ToDate': '" + ToDate + "','VoucherID': '" + VoucherID + "','DepotID': '" + DepotID + "','checker': '" + checker + "'}",
        data: { claimtype: ClaimType, depotid: depotid, status: res, fromdate: fromdate, todate: todate, party: res1 },

        dataType: "json",
        success: function (response) {
            var tableNew = '<table id="gvqpsclaim" class="table table-striped table-bordered dt-responsive nowrap" style="width:100%">';
            tableNew = tableNew + '<thead style="background-color:cornflowerblue;font-size:14px"><tr id="header"><th style="display:none"><th> Sl.</th><th>Edit</th><th>REG NO. </th><th> REG DATE.</th><th>CN NO.</th><th>PROCESS DATE</th><th style="display:none">CLAIM BUSINESSSEGMENT</th><th>BUSINESS SEGMENT</th> <th style="display:none">PRINCIPLE GROUPID</th><th>DEPOT </th><th style="display:none">DISTRIBUTORID</th><th>DISTRIBUTOR</th><th style="display:none">RETAILERID</th><th style="display:none">RETAILER</th><th>CLAIM AMT</th><th>PASSED AMT</th><th>NARRATION</th><th>STATUS</th><th >DOCUMENT RECEIVED DATE</th><th>DOCUMENT STATUS</th><th>REMARKS</th><th>DEDUCTION DETAILS</th><th>Print</th><th>Del</th></tr></thead> <tbody>';

           

            $.each(response, function () {

                debugger;
                if (this["CURRENTSTATUS"] == "Confirmed") {
                    current_statussearch = 'current_statussearch';
                }
                else {
                    current_statussearch = '';
                }

                counter = counter + 1;
                tableNew = tableNew + "<tr><td style='display:none'>" + this["ID"] + "</td> <td>" + counter + "</td> <td><input type='image' class='gvedit' id='btnedit' src='../Images/Pencil-icon.png' title='Edit'/></td >     <td>" + this["DAM_CLM_NO"] + "</td>   <td>" + this["CLAIM_DATE"] + "</td><td >" + this["PROCESSNUMBER"] + "</td>   <td>" + this["PROCESS_DATE"] + "</td><td style='display:none'>" + this["CLAIM_BUSINESSSEGMENT_ID"] + "</td> <td>" + this["CLAIM_BUSINESSSEGMENT_NAME"] + "</td>  <td style='display:none'>" + this["CLAIM_PRINCIPLEGROUP_ID"] + "</td><td>" + this["DEPOTNAME"] + "</td><td style='display:none'>" + this["CLAIM_DISTRIBUTOR_ID"] + "</td><td>" + this["CLAIM_DISTRIBUTOR_NAME"] + "</td><td style='display:none'>" + this["CLAIM_RETAILER_ID"] + "</td> <td style='display:none'>" + this["CLAIM_RETAILER_NAME"] + "</td><td>" + this["AMOUNT"] + "</td><td>" + this["CLAIM_MODI_AMT"] + "</td> <td>" + this["NARRATION"] + "</td><td class='" + current_statussearch +"'>" + this["CURRENTSTATUS"] + "</td><td>" + this["DOCUMENTSRECEIVEDDATE"] + "</td><td>" + this["DOCUMENTSTATUS"] + "</td> <td>" + this["REMARKS"] + "</td><td>" + this["DEDUCTIONDETAILS"] + "</td>    <td><input type='image' class='gvprint' id='btnprint'  src='../images/print.png ' width='30' height ='30'/></td>  <td><input type='image' class='gvdel' id='btnDel' src='../Images/ico_delete_16.png' title='Delete' /></td> </tr>";

            });
            document.getElementById("DivHeaderRow").innerHTML = tableNew + '</table>';

            $('#gvqpsclaim').DataTable({
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
}

function BindDepotByUserID() {
    var ddlsearchdepot = $("#ddlsearchdepot");
    var ddldepot = $("#ddldepot");


    $.ajax({
        type: "POST",
        url: "/claims/BindDepotByUserID",
        data: '{}',
        async: false,
        dataType: "json",

        success: function (response) {
            //alert(JSON.stringify(response));
            ddlsearchdepot.empty();
            ddldepot.empty();
            //.append('<option selected="selected" value="0">Select Brancht</option>');;
            $.each(response, function () {
                ddlsearchdepot.append($("<option></option>").val(this['BRID']).html(this['BRNAME']));
                ddldepot.append($("<option></option>").val(this['BRID']).html(this['BRNAME']));

            });
            var depoid = $('#ddlregion').val();
            // $("#hdndepoid").val(depoid);

            BindDistributorClaim();

        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function BindClaimStatus() {
    var ddlstatus = $("#ddlstatus");
    $.ajax({
        type: "POST",
        url: "/claims/BindClaimStatus",
        data: '{}',
        async: false,
        dataType: "json",

        success: function (response) {
            //alert(JSON.stringify(response));
            ddlstatus.empty()
            //.append('<option selected="selected" value="0">Select</option>');;;

            //.append('<option selected="selected" value="0">Select Brancht</option>');;
            $.each(response, function () {
                ddlstatus.append($("<option></option>").val(this['ID']).html(this['CLAIMSTATUSNAME']));


            });

            $('#ddlstatus').multiselect({
                columns: 1, placeholder: 'Select Languages',
                includeSelectAllOption: true,
                //search: true,
                // selectAll: true
                //allSelectedText: 'All Selected'
            });

            $("#ddlstatus").multiselect('selectAll', false);
            $("#ddlstatus").multiselect('updateButtonText');


        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function BindDistributorByDepot() {
    var DistributorID = $("#ddldepot").val();
    var BSID = $("#ddlbusinesssegment").val();
    var ddldistributor = $("#ddldistributor");
    $.ajax({
        type: "POST",
        url: "/claims/BindDistributorByDepot",
        data: { DistributorID: DistributorID, BSID: BSID },
        async: false,
        dataType: "json",
        success: function (response) {
            //alert(JSON.stringify(response));
            ddldistributor.empty().append('<option selected="selected" value="0">Select </option>');
            $.each(response, function () {
                ddldistributor.append($("<option></option>").val(this['CUSTOMERID']).html(this['CUSTOMERNAME']));
            });
            $("#ddldistributor").chosen({
                search_contains: true
            });


            $("#ddldistributor").trigger("chosen:updated");
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function BindRetailer() {
    var DistributorID = $("#ddldistributor").val();

    var ddlretailer = $("#ddlretailer");

    $.ajax({
        type: "POST",
        url: "/claims/BindRetailer",
        data: { DistributorID: DistributorID },
        async: false,
        dataType: "json",
        success: function (response) {
            //alert(JSON.stringify(response));
            ddlretailer.empty().append('<option selected="selected" value="0">All </option>');
            //
            $.each(response, function () {
                ddlretailer.append($("<option></option>").val(this['CUSTOMERID']).html(this['CUSTOMERNAME']));
            });
            $("#ddlretailer").chosen({
                search_contains: true
            });


            $("#ddlretailer").trigger("chosen:updated");
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function BindClaimNarration() {
    var CLAIM_TYPE;
    var Grosswise = $("input[name='Grosswise']:checked").val();
    var Invoicewise = $("input[name='Invoicewise']:checked").val();
    var DEPOTID = $("#ddldepot").val();
    var BSID = $("#ddlbusinesssegment").val();


    var txtnarration = $("#txtnarration");

    if (Grosswise) {
        CLAIM_TYPE = Grosswise;
    }
    if (Invoicewise) {
        CLAIM_TYPE = Invoicewise;
    }


    $.ajax({
        type: "POST",
        url: "/claims/BindClaimNarration",
        data: { CLAIM_TYPE: ClaimType, BSID: BSID, DEPOTID: DEPOTID },
        async: false,
        dataType: "json",
        success: function (response) {
            //alert(JSON.stringify(response));
            txtnarration.empty().append('<option selected="selected" value="0">Select Narration </option>');
            //
            $.each(response, function () {
                txtnarration.append($("<option></option>").val(this['NARRATIONID']).html(this['CLAIM_NARRATION']));
            });
            $("#txtnarration").chosen({
                search_contains: true
            });


            $("#txtnarration").trigger("chosen:updated");
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function BindClaimPeriod() {
    var CLAIM_TYPE;
    var narrationid = $("#txtnarration").val();
    $.ajax({
        type: "POST",
        url: "/claims/BindClaimPeriod",
        data: { narrationid: narrationid },
        async: false,
        dataType: "json",
        success: function (response) {
            //alert(JSON.stringify(response));

            //
            $.each(response, function () {

                $("#txtStartDate").val(this['FROM_DATE']);

                $("#txtEndDate").val(this['TO_DATE']);
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

function BindDistributorClaim() {
    var DistributorID = $("#ddlsearchdepot").val();

    var ddlparty = $("#ddlparty");
    $.ajax({
        type: "POST",
        url: "/claims/BindDistributorClaim",
        data: { DistributorID: DistributorID },
        async: false,
        dataType: "json",
        success: function (response) {
            //alert(JSON.stringify(response));
            ddlparty.empty().append('<option selected="selected" value="0">Select </option>');
            $.each(response, function () {
                ddlparty.append($("<option></option>").val(this['CUSTOMERID']).html(this['CUSTOMERNAME']));
            });
            $('#ddlparty').multiselect({
                columns: 1, placeholder: 'Select Languages',
                includeSelectAllOption: true,
                //search: true,
                // selectAll: true
                //allSelectedText: 'All Selected'
            });

            $("#ddlparty").multiselect('selectAll', false);
            $("#ddlparty").multiselect('updateButtonText');

            //$("#ddlparty").chosen({
            //    search_contains: true
            //});


            //$("#ddlparty").trigger("chosen:updated");
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function addnew() {
    ReleaseSession();
    $('#pnlAdd').css("display", "block");
    $('#pnlDisplay').css("display", "none");
    $('#tr_claimno').css("display", "none");
    $("#divinvoice").hide();

    $("#Grosswise").prop('checked', true);
    $("#btnSave").show();
    BindBusinessSegment();
    LoadCategory();
    loadProduct();
    BindBatch();
    debugger;
    LoadReason();
    CreateDataTable();
    $("#Grosswise").prop("disabled", true);
    $("#ddldepot").prop("disabled", true);
    $("#ddldepot").chosen({
        search_contains: true
    });
    $("#ddldepot").trigger("chosen:updated");
    $("#ddlbusinesssegment").chosen({
        search_contains: true
    });
    $("#ddlbusinesssegment").trigger("chosen:updated");

    $("#txtCategory").chosen({
        search_contains: true
    });
    $("#txtCategory").trigger("chosen:updated");

   
    $("#ddlProduct").chosen({
        search_contains: true
    });
    $("#ddlProduct").trigger("chosen:updated");
    $("#txtPackSize").chosen({
        search_contains: true
    });
    $("#txtPackSize").trigger("chosen:updated");
    $("#txtBatchno").chosen({
        search_contains: true
    });
    $("#txtBatchno").trigger("chosen:updated");

    $("#ddlReason").chosen({
        search_contains: true
    });
    $("#ddlReason").trigger("chosen:updated");
    $("#ddlgroup").chosen({
        search_contains: true
    });
    $("#ddlgroup").trigger("chosen:updated");
    $("#txtnarration").chosen({
        search_contains: true
    });
    $("#txtnarration").trigger("chosen:updated"); 

    $("#ddldistributor").empty().append('<option selected="selected" value="0">Select</option>');
    $("#ddlretailer").empty().append('<option selected="selected" value="0">Select Retailer</option>');
    $("#ddldistributor").chosen({
        search_contains: true
    });
    $("#ddldistributor").trigger("chosen:updated");
    $("#ddlretailer").chosen({
        search_contains: true
    });
    



    $("#ddlretailer").trigger("chosen:updated");




}

function close() {
    $('#pnlAdd').css("display", "none");

    $('#pnlDisplay').css("display", "block");

    $("#Grosswise").prop('checked', true);
   // clearall();
    clearcontrol();
    ReleaseSession();
}

function ReleaseSession() {

    $.ajax({
        type: "POST",
        url: "/claims/RemoveDamageClaimeSession",
        data: '{}',
        dataType: "json",
        success: function (response) {


        }
    });
}

function LoadReason() {

    var mode = 'Reason';

    debugger;
    $.ajax({
        type: "POST",
        url: "/claims/BindReason",
        async: false,
        dataType: "json",
        data: { mode: mode },
        success: function (result) {

            debugger;
            //alert(JSON.stringify(result));
            var ddlReason = $("#ddlReason");
            ddlReason.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(result, function () {
                ddlReason.append($("<option value=''></option>").val(this['ID']).html(this['NAME']));
            });
            //$('#ddlReason').multiselect({
            //    columns: 1, placeholder: 'Select Languages',
            //    includeSelectAllOption: true,
                //search: true,
                // selectAll: true
                //allSelectedText: 'All Selected'
            //});

            //$("#ddlReason").multiselect('selectAll', false);
            //$("#ddlReason").multiselect('updateButtonText');
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function BindBusinessSegment() {
    var ddlbusinesssegment = $("#ddlbusinesssegment");



    $.ajax({
        type: "POST",
        url: "/claims/BindBusinessSegmentByUserID",
        data: '{}',
        async: false,
        dataType: "json",

        success: function (response) {
           // alert(JSON.stringify(response));
            debugger;
            ddlbusinesssegment.empty().append('<option selected="selected" value="0">Select</option>');;;

            //.append('<option selected="selected" value="0">Select Brancht</option>');;
            $.each(response, function () {
                ddlbusinesssegment.append($("<option></option>").val(this['ID']).html(this['NAME']));


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
}

function LoadCategory() {
  
    var mode = 'Category';

    debugger;
    $.ajax({
        type: "POST",
        url: "/claims/BindCategory",
        async: false,
        dataType: "json",
        data:{mode:mode},
        success: function (result) {
           
            debugger;
            var ddlCategory = $("#txtCategory");
            ddlCategory.empty().append('<option selected="selected" value="-1">All </option>');
            $.each(result, function () {
                $('#txtCategory').val(this['CATNAME']);
                ddlCategory.append($("<option value=''></option>").val(this['CATID']).html(this['CATNAME']));
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

function loadProduct() {

    var mode = 'Product';


    $.ajax({
        type: "POST",
        url: "/claims/BindProductCatwise",
        data: '{}',
        async: false,
        dataType: "json",
        data: { mode: mode, catid: $("#txtCategory").val()},
        success: function (result) {
            debugger;
            var ddlProduct = $("#ddlProduct");
           // alert(JSON.stringify(result));
            ddlProduct.empty().append('<option selected="selected" value="-1">All </option>');
            $.each(result, function () {
                $('#ddlProduct').val(this['CATNAME']);
                ddlProduct.append($("<option value=''></option>").val(this['PRODUCTID']).html(this['PRODUCTNAME']));
            });
            $("#ddlProduct").chosen({
                search_contains: true
            });
            $("#ddlProduct").trigger("chosen:updated");
        },

        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function LoadPacksize() {
    var mode = 'Packsize';
    $.ajax({
        type: "POST",
        url: "/claims/BindPacksize",
        data: '{}',
        async: false,
        dataType: "json",
        data: { mode:mode,productid: $("#ddlProduct").val()},
        success: function (result) {
            debugger;
            var txtPackSize = $("#txtPackSize");
            //alert(JSON.stringify(result));
            txtPackSize.empty().append('<option selected="selected" value="-1">Please select</option>');
            $.each(result, function () {
                $('#txtPackSize').val(this['PSNAME']);
                txtPackSize.append($("<option value=''></option>").val(this['PSID']).html(this['PSNAME']));
            });
            $("#txtPackSize").chosen({
                search_contains: true
            });
            $("#txtPackSize").trigger("chosen:updated");
        },

        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function BindBatch() {
    var mode = 'Batch';
    var distributorid = $("#ddldistributor").val();
        $.ajax({
            type: "POST",
            url: "/claims/BindBatchno",
            data: '{}',
            async: false,
            dataType: "json",
            data: { mode: mode, productid: $("#ddlProduct").val(), distributorid: distributorid },
            success: function (result) {
                debugger;
                var txtBatchno = $("#txtBatchno");
                //alert(JSON.stringify(result));
                txtBatchno.empty().append('<option selected="selected" value="-1">Select All</option>');
                $.each(result, function () {
                    txtBatchno.append($("<option value='0'>Select All</option>"));
                    txtBatchno.append($("<option value=''></option>").val(this['SALEINVOICEID']).html(this['BATCHNO']));
                });
                $("#txtBatchno").chosen({
                    search_contains: true
                });
                $("#txtBatchno").trigger("chosen:updated");
            },

            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
}

function CreateDataTable() {
    debugger;
    $.ajax({
        type: "POST",
        url: "/claims/CreateDamageClaimDataTable",
        data: {},
        async: false,
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

function addgrid() {
    debugger;
    var isexists = 'n';
    var PRODUCTID = $("#ddlProduct").val();
    var PRODUCTNAME = $('#ddlProduct').find('option:selected').text();
    var CLAIM_PACKSIZE_ID = $("#txtPackSize").val();
    var CLAIM_PACKSIZE_NAME = $('#txtPackSize').find('option:selected').text();
    var CLAIM_CAT_ID = $("#txtCategory").val();
    var CLAIM_CAT_NAME = $('#txtCategory').find('option:selected').text();
    var BATCHNO = $("#txtBatchno").val();
    var INVOICENO = $("#txtInvoiceNo").val();
    var INVOICEDATE = $("#txtInvoiceDate").val();
    var MRP = $("#txtMRP").val();
    var BASE_PRICE = $("#txtBaseRate").val();
    var DISCOUNT_PRICE = 0;
    var CLAIM_QTY = $("#txtClaimQuantity").val();
    var TOTAL_INVOICE_QTY = $("#txtInvoiceQty").val();
    var AMOUNT = $("#txtAmount").val();
    var NARRATIONID = $("#txtnarration").val();
    var NARRATION = $('#txtnarration').find('option:selected').text();
    var bs = $('#ddlbusinesssegment').val();
    var dis = $('#ddldistributor').val();
    if (bs == '0' || bs == '') {
        toastr.warning("Businesssegment required...!");
        return false;
    }
    if (dis == '0' || dis == '') {
        toastr.warning("Distributorname required...!");
        return false;
    }

    if (BASE_PRICE == "") {
        BASE_PRICE = 0;
    }
    if (TOTAL_INVOICE_QTY == "") {
        TOTAL_INVOICE_QTY = 0;
    }

    if (AMOUNT == "" || AMOUNT ==0) {
        toastr.warning("Amount Should be greater than zero")
        return false;
    }
    if (MRP == "") {
        MRP = 0;
    }
    if (CLAIM_QTY == "") {
        CLAIM_QTY = 0;
    }
    if (NARRATIONID == '0' || NARRATIONID=="" ) {
        toastr.warning('Narration required...!');
        return false;
    }

    if (BATCHNO == "") {
        BATCHNO = 'ALL';
    }
   
    var REASON_ID = $("#ddlReason").val();
    var REASON = $('#ddlReason').find('option:selected').text();


    

    if ($('#grdProduct').length) {
        $("#grdProduct tbody tr").each(function () {
            var narr = $(this).find('td:eq(4)').html();
            // var batchidgrd = $(this).find('td:eq(3)').html();

            if (NARRATION == narr) {
                isexists = 'y';
                return false;
            }
        })
    }

    if (isexists == 'y') {
        toastr.warning('Narration Already Exist ...!');
        return false;
    }
    $.ajax({

        type: "POST",
        //  url: "frmaccvouchernew2.aspx/addvouchercr1",
        url: "/claims/addDamgeClaim",
        data: {PRODUCTID: PRODUCTID, PRODUCTNAME: PRODUCTNAME, CLAIM_PACKSIZE_ID: CLAIM_PACKSIZE_ID, CLAIM_PACKSIZE_NAME: CLAIM_PACKSIZE_NAME, CLAIM_CAT_ID: CLAIM_CAT_ID, CLAIM_CAT_NAME: CLAIM_CAT_NAME, BATCHNO: BATCHNO, INVOICE_NO: INVOICENO, INVOICE_DATE: INVOICEDATE, MRP: MRP, BASE_PRICE: BASE_PRICE, DISCOUNT_PRICE: DISCOUNT_PRICE, CLAIM_QTY: CLAIM_QTY, TOTAL_INVOICE_QTY: TOTAL_INVOICE_QTY, AMOUNT: AMOUNT, NARRATION: NARRATION, REASON_ID: REASON_ID, REASON: REASON },

        //data: "{'AccountID': '" + AccountID + "','AccountName': '" + AccountName + "'}",
        //data: "{'ledgerid': '" + ledgerid + "','CostCatagoryID': '" + CostCatagoryID + "','BrandId': '" + BrandId + "','ProductId': '" + ProductId + "','DepartmentId': '" + DepartmentId + "'}",

        dataType: "json",
        success: function (response) {
            var tableNew = '<table id="grdProduct" class="table table-striped table-bordered dt-responsive nowrap">';
            tableNew = tableNew + '<thead ><tr><th style="display:none"></th><th >Sl.No.</th><th>PRODUCT</th><th>PACKSIZE</th><th>CATEGORY</th><th>BATCH NO</th><th>INVOICE NO</th> <th>INVOICE DATE</th><th>MRP</th><th>CLAIM (%)</th><th>BASE RATE</th><th>CLAIM QTY</th><th>INVOICE QTY</th><th>AMOUNT</th><th>REASON</th><th>NARRATION</th><th>DELETE</th></tr></thead><tbody>';
            var totalamt = 0;
            var grossamount = 0;
            var counter = 0;

            var iscostcenter;
            $.each(response, function () {
                counter = counter + 1;
                tableNew = tableNew + "<tr><td style='display:none'>" + this["GUID"] + "</td><td>" + counter + "</td><td style='display:none'>" + this["CLAIM_PRODUCT_ID"] + "</td> <td>" + this["CLAIM_PRODUCT_NAME"] + "</td> <td style='display:none'>" + this["CLAIM_PACKSIZE_ID"] + "</td>  <td>" + this["CLAIM_PACKSIZE_NAME"] + "</td><td style='display:none'>" + this["CLAIM_CAT_ID"] + "</td><td>" + this["CLAIM_CAT_NAME"] + "</td><td>" + this["BATCHNO"] + "</td> <td>" + this["INVOICE_NO"] + "</td><td>" + this["INVOICE_DATE"] + "</td><td>" + this["MRP"] + "</td><td>" + this["DISCOUNT_PRICE"] + "</td><td>" + this["BASE_PRICE"] + "</td><td>" + this["CLAIM_QTY"] + "</td><td>" + this["TOTAL_INVOICE_QTY"] + "</td><td>" + this["AMOUNT"] + "</td><td style='display:none'>" + this["REASON_ID"] + "</td><td>" + this["REASON"] + "</td><td>" + this["NARRATION"] + "</td>   <td><input type='image' class='grvdtldel' id='btnDtldel' src='../Images/ico_delete_16.png' title='Delete' /></td>";

                totalamt = totalamt + parseFloat(this["AMOUNT"]);
                grossamount = grossamount + parseFloat(this["AMOUNT"]);
               

            })
            document.getElementById("grdProduct").innerHTML = tableNew + '</tbody></table>';
            $("#txtNetAmt").val(totalamt);
            $("#txtGrossAmt").val(grossamount);
            $("#hdnsumtotal").val(totalamt);
            $("#txtBaseRate").val('0');
            $("#txtMRP").val('0');
            $("#txtEndDate").val('');
            $("#txtInvoiceNo").val('');
            $("#ddlProduct").val('0');
            $("#ddlProduct").chosen({
                search_contains: true
            });


            $("#ddlProduct").trigger("chosen:updated");

            $("#txtPackSize").val('0');
            $("#txtPackSize").chosen({
                search_contains: true
            });


            $("#txtPackSize").trigger("chosen:updated");
        }
    });
}

function SaveDamageClaim() {

    debugger;
    var mode = '';
    var Tag = "R";
    var tag = $("#hdnClaimtype").val();
    if (tag == '' || tag == undefined) {
        tag = 'GA'
    }
    
    if ($('#hdnClaimID').val() == '') {
        mode = 'A'
    }
    else {
        mode = 'E';
    }
    
    var tran = {};
    tran.ClaimID = $('#hdnClaimID').val().trim();
    tran.Mode = mode;
    tran.bsid = $('#ddlbusinesssegment').val().trim();
    tran.bsname = $('#ddlbusinesssegment').find('option:selected').text();
    tran.grid = $('#ddlgroup').val().trim();
    tran.grname = $('#ddlgroup').find('option:selected').text();
    tran.distid = $('#ddldistributor').val().trim();
    tran.distname = $('#ddldistributor').find('option:selected').text();
    tran.retrid = $('#ddlretailer').val().trim();
    tran.retrname = $('#ddlretailer').find('option:selected').text();
    tran.reasonid = $('#ddlReason').val();
    tran.reasonname = $('#ddlReason').find('option:selected').text();

    tran.TypeID = ClaimType;
    tran.remarks = $('#txtRemarks').val().trim();
    tran.tag = tag;

    tran.Amount = $('#txtNetAmt').val().trim();
   
    tran.depotid = $('#ddldepot').val().trim();
    tran.depotname = $('#ddldepot').find('option:selected').text();


    
    tran.date = $('#txtclaimdate').val().trim();

    if ($('#ddlbusinesssegment').val().trim() == '0') {
        toastr.warning('Select Businesssegment...!');
        return false;
    }
    if ($('#ddldistributor').val().trim() == '0') {
        toastr.warning('Select Distributor...!');
        return false;
    }
    //if ($('#ddlretailer').val().trim() == '0') {
    //    toastr.warning('Select Retailer...!');
    //    return false;
    //}
    if ($('#ddlgroup').val().trim() == '0') {
        toastr.warning('Select Group...!');
        return false;
    }/*
    if ($("#hdnsumtotal").val() >= $('#txtNetAmt').val()) {

    }*/

    $.ajax({

        url: "/claims/SaveDamageClaim",
        data: '{tran:' + JSON.stringify(tran) + '}',
        type: "POST",
        async: false,
        contentType: "application/json",
        success: function (responseMessage) {
            var messagetext;
            $.each(responseMessage, function (key, item) {
                //  messageid = item.MessageID;
                messagetext = item.response;

            });
            toastr.success('<b><font color=black>' + messagetext + '</font></b>');
            if (messagetext != '0') {
                close();
                BindDamage();
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

function clearcontrol() {
    $("#txtshowclaimno").val(0);
    $("#txtprocessno").val(0);
    $("#txtclaimdate").val();
    $("#hdnClaimtype").val('');
    $("#ddlbusinesssegment").empty();
    $("#ddldistributor").empty();
    $("#ddlgroup").empty();
    $("#ddlretailer").empty();
    $("#txtCategory").empty();
    $("#ddlProduct").empty();
    $("#txtPackSize").empty();
    $("#txtBatchno").empty();
    $("#txtInvoiceNo").val(0);
    $("#txtMRP").val(0);
    $("#txtBaseRate").val(0);
    $("#txtClaimQuantity").val(0);
    $("#txtInvoiceQty").val(0);
    $("#txtAmount").val(0);
    $("#txtnarration").empty();
    $("#ddlReason").empty();
    $('#grdProduct tbody tr').remove();
    $('#grdProduct thead').remove();
}


$(function () {
    $("body").on("click", "#gvqpsclaim .gvdel", function () {
        debugger;
        row = $(this).closest("tr");
        var claimid = row.find('td:eq(0)').html();
        if (confirm("Do you want to delete this Claim?")) {
            $.ajax({
                type: "POST",
                url: "/claims/DamgeDeleteClaim",
                data: { claimid: claimid },
                dataType: "json",
                //success: function (response) {
                //    debugger;
                //    if (response != "") {
                //        toastr.success("Delete Successfull");
                //        BindDamage();
                //    }
                //    else {

                //        toastr.warning("Error On Delete Record");
                //    }

                //},

                success: function (responseMessage) {
                    var messagetext;
                    $.each(responseMessage, function (key, item) {
                        //  messageid = item.MessageID;
                        messagetext = item.response;

                    });
                    toastr.info('<b><font color=black>' + messagetext + '</font></b>');
                    if (messagetext != '0') {
                        close();
                        BindDamage();
                        ReleaseSession();

                    }
                    else {
                        // $('#dvAdd').css("display", "");
                        //$('#dvDisplay').css("display", "none");
                        toastr.error('<b><font color=black>' + messagetext + '</font></b>');
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
    var claimid;
    debugger;
    $("body").on("click", "#gvqpsclaim .gvprint", function () {

        var row = $(this).closest("tr");
        claimid = row.find('td:eq(0)').html();

        var url = "http://mcnroeerp.com/mcworld/View/frmRptClaimPrint.aspx?pid=" + claimid + "&&CLID=" + ClaimType + "";
        window.open(url, "", "width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100");

    })

}) /*For Print*/

function isNumberKeyWithDot(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if ((charCode < 48 || charCode > 57) && (charCode != 8) && (charCode != 46))
        return false;
    return true;
}