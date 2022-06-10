//#region Developer Info
/*
* For MaterialMaster.cshtml Only
* Developer Name : Avishek Ghosh 
* Start Date     : 30/04/2020
* END Date       : 
 
*/
//#endregion

$(document).ready(function () {
    

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

    $('#dvAdd').css("display", "none");
    $('#dvDisplay').css("display", "");
    $('#btnsearch').css("display", "none");

    bindmaterialmastergrid();
    bindPrimaryItem();
    bindUom();
    bindFactory();
    if ($("#FactoryID").val().trim() != '0') {
        bindCustomer($("#FactoryID").val().trim());
    }
    else {
        
        $('#ddlCustomer').multiselect('destroy');
        $('#ddlCustomer').empty();
        $('#ddlCustomer').multiselect({
            columns: 1,
            placeholder: 'Select Customer',
            includeSelectAllOption: false
        });
        $("#ddlCustomer").multiselect("deselectAll", false);/*For De-Select All By Default*/
        $("#ddlCustomer").multiselect('updateButtonText');
    }

    $("#btnAddnew").click(function (e) {
        $('#dvAdd').css("display", "");
        $('#dvDisplay').css("display", "none");
        $('#btnsearch').css("display", "none");
        ClearControls();
        e.preventDefault();
    })

    $("#btnclose").click(function (e) {
        $('#dvAdd').css("display", "none");
        $('#dvDisplay').css("display", "");
        $('#btnsearch').css("display", "none");
        ClearControls();
        bindmaterialmastergrid();
        e.preventDefault();
    })

    $('#PrimaryItemID').change(function (e) {
        if ($('#PrimaryItemID').val().trim() != '0') {

            bindSubItemVendor($('#PrimaryItemID').val().trim());

            $("#SubItemID").chosen({
                search_contains: true
            });
            $("#SubItemID").trigger("chosen:updated");
        }
        else {
            $("#SubItemID").empty();
            $("#SubItemID").chosen({
                search_contains: true
            });
            $("#SubItemID").trigger("chosen:updated");

            $("#VendorID").empty();
        }
        e.preventDefault();
    })

    $("#btnsave").click(function (e) {
        //debugger;
        if ($("#ProductOwner").val() == '0') {
            toastr.warning('<b><font color=black>Please select product owner..!</font></b>');
            return false;
        }
        else if ($("#ProductName").val() == '') {
            toastr.warning('<b><font color=black>Please enter product name..!</font></b>');
            return false;
        }
        else if ($("#ProductCode").val() == '') {
            toastr.warning('<b><font color=black>Please enter product code..!</font></b>');
            return false;
        }
        else if ($("#PrimaryItemID").val() == '0') {
            toastr.warning('<b><font color=black>Please select primary item..!</font></b>');
            return false;
        }
        else if ($("#SubItemID").val() == '0') {
            toastr.warning('<b><font color=black>Please select sub item..!</font></b>');
            return false;
        }
        else if ($("#UomID").val() == '0') {
            toastr.warning('<b><font color=black>Please select uom..!</font></b>');
            return false;
        }
        else if ($("#UnitCapacity").val() == '') {
            toastr.warning('<b><font color=black>Please enter unit capacity..!</font></b>');
            return false;
        }
        else if ($("#MRP").val() == '') {
            toastr.warning('<b><font color=black>Please enter mrp..!</font></b>');
            return false;
        }
        else if ($("#UnitCapacityInput").val() == '') {
            toastr.warning('<b><font color=black>Please enter unit capacity..!</font></b>');
            return false;
        }
        else if ($("#PacksizeFrom").val() == '0') {
            toastr.warning('<b><font color=black>Please select packsize from..!</font></b>');
            return false;
        }
        else if ($("#PacksizeTo").val() == '0') {
            toastr.warning('<b><font color=black>Please select packsize to..!</font></b>');
            return false;
        }
        else {

                if ($('#hdnMaterialID').val() == '0') {
                    var isExists = Exists($("#ProductName").val().trim(), $("#ProductCode").val().trim());
                    if (isExists != 'na') {
                        toastr.warning('<b>' + isExists + '</b>');
                        return false;
                    }
                    else {
                        SaveProduct();
                    }
                }
                else {
                    SaveProduct();
                }
             }
        e.preventDefault();
    })

    $("#btnRefreshPrimaryItem").click(function (e) {
        if ($('#hdnMaterialID').val() == '0') {
            bindPrimaryItem();
            $("#PrimaryItemID").val('0');
            $("#PrimaryItemID").chosen({
                search_contains: true
            });
            $("#PrimaryItemID").trigger("chosen:updated");

            $("#SubItemID").empty();
            $("#SubItemID").chosen({
                search_contains: true
            });
            $("#SubItemID").trigger("chosen:updated");
        }
        e.preventDefault();
    })

    $("#btnRefreshSubItem").click(function (e) {
        if ($('#hdnMaterialID').val() == '0') {
            if ($('#PrimaryItemID').val().trim() != '0') {

                bindSubItemRefresh($('#PrimaryItemID').val().trim());
                $('#SubItemID').val('0');

                $("#SubItemID").chosen({
                    search_contains: true
                });
                $("#SubItemID").trigger("chosen:updated");
            }
            else {
                toastr.warning('<b><font color=black>Please select Primary Item..!</font></b>');
                return false;
                $("#SubItemID").empty();
                $("#SubItemID").chosen({
                    search_contains: true
                });
                $("#SubItemID").trigger("chosen:updated");
            }
        }
        e.preventDefault();
    })

    $("#btnRefreshUOM").click(function (e) {
        if ($('#hdnMaterialID').val() == '0') {
            bindUom();

            $("#UomID").val('0');
            $("#UomID").chosen({
                search_contains: true
            });
            $("#UomID").trigger("chosen:updated");

            $("#PacksizeFrom").val('0');
            $("#PacksizeFrom").chosen({
                search_contains: true
            });
            $("#PacksizeFrom").trigger("chosen:updated");

            $("#PacksizeTo").val('0');
            $("#PacksizeTo").chosen({
                search_contains: true
            });
            $("#PacksizeTo").trigger("chosen:updated");
        }
        e.preventDefault();
    })
});

function ClearControls() {

    $("#ProductName").removeAttr("disabled");
    $("#ProductCode").removeAttr("disabled");
    $("#PrimaryItemID").removeAttr("disabled");
    $("#SubItemID").removeAttr("disabled");
    $("#UomID").removeAttr("disabled");
    $("#UnitCapacity").removeAttr("disabled");
    $("#Assesment").removeAttr("disabled");

    $("#ProductName").val('');
    $("#ProductCode").val('');
    $("#UnitCapacity").val('');
    $("#MRP").val('');
    $("#ReorderLevel").val('');
    $("#UnitCapacityInput").val('');
    $("#Assesment").val('');
    $("#chkactive").prop("checked", true);
    $("#chkreturn").prop("checked", false);

    $("#ProductOwner").val('0');
    $("#ProductOwner").chosen({
        search_contains: true
    });
    $("#ProductOwner").trigger("chosen:updated");

    $("#PrimaryItemID").val('0');
    $("#PrimaryItemID").chosen({
        search_contains: true
    });
    $("#PrimaryItemID").trigger("chosen:updated");

    $("#SubItemID").empty();
    $("#SubItemID").chosen({
        search_contains: true
    });
    $("#SubItemID").trigger("chosen:updated");

    $("#UomID").val('0');
    $("#UomID").chosen({
        search_contains: true
    });
    $("#UomID").trigger("chosen:updated");

    $("#FactoryID").chosen({
        search_contains: true
    });
    $("#FactoryID").trigger("chosen:updated");

    $('#ddlCustomer').multiselect('destroy');
    $('#ddlCustomer').multiselect({
        columns: 1,
        placeholder: 'Select Customer',
        includeSelectAllOption: false
    });
    $("#ddlCustomer").multiselect("deselectAll", false);/*For De-Select All By Default*/
    $("#ddlCustomer").multiselect('updateButtonText');

    $('#ddlVendor').multiselect('destroy');
    $('#ddlVendor').empty();
    $('#ddlVendor').multiselect({
        columns: 1,
        placeholder: 'Select Customer',
        includeSelectAllOption: false
    });
    $("#ddlVendor").multiselect("deselectAll", false);/*For De-Select All By Default*/
    $("#ddlVendor").multiselect('updateButtonText');

    $("#PacksizeFrom").val('0');
    $("#PacksizeFrom").chosen({
        search_contains: true
    });
    $("#PacksizeFrom").trigger("chosen:updated");

    $("#PacksizeTo").val('0');
    $("#PacksizeTo").chosen({
        search_contains: true
    });
    $("#PacksizeTo").trigger("chosen:updated");
    $('#hdnMaterialID').val('0');
}

function bindPrimaryItem() {
    debugger;
    var PrimaryItem = $("#PrimaryItemID");
    $.ajax({
        type: "POST",
        url: "/Masterfac/GetPrimaryItem",
        data: '{}',
        async: false,
        dataType: "json",
        success: function (response) {
            //alert(JSON.stringify(response));
            debugger;
            PrimaryItem.empty().append('<option selected="selected" value="0">Please select</option>');;
            $.each(response, function () {
                debugger;
                PrimaryItem.append($("<option></option>").val(this['ID']).html(this['ITEMDESC']));
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

function bindSubItemVendor(primaryitemid) {
    var SubItem = $("#SubItemID");
    var Vendor = $("#ddlVendor");
    $.ajax({
        type: "POST",
        url: "/Masterfac/SubItemVendor",
        data: { PrimaryItemID: primaryitemid },
        async: false,
        dataType: "json",
        success: function (response) {
            //alert(JSON.stringify(response));
            var listSubItem = response.allDataset.varSubItem;
            var listVendor = response.allDataset.varVendor;

            /*Bind Sub Item Start*/
            if (listSubItem.length > 0) {
                SubItem.empty().append('<option selected="selected" value="0">Please select</option>');;
                $.each(listSubItem, function (index, record) {
                    SubItem.append($("<option></option>").val(this['SUBTYPEID']).html(this['SUBITEMDESC']));
                });
            }
            else {
                $("#SubItemID").empty();
                $("#SubItemID").chosen({
                    search_contains: true
                });
                $("#SubItemID").trigger("chosen:updated");
            }
            /*Bind Sub Item End*/


            /*Bind Vendor Start*/
            if (listVendor.length > 0) {
                $('#ddlVendor').multiselect('destroy');
                Vendor.empty().append('<option selected="selected" value="0">Please select</option>');;
                $.each(listVendor, function (index, record) {
                    Vendor.append($("<option></option>").val(this['VENDORID']).html(this['VENDORNAME']));
                });

                $('#ddlVendor').multiselect({
                    columns: 1,
                    placeholder: 'Select Vendor',
                    includeSelectAllOption: false
                });

                //$("#ddlCustomer").multiselect('selectAll', false);/*For Select All By Default*/
                $("#ddlVendor").multiselect("deselectAll", false);/*For De-Select All By Default*/
                $("#ddlVendor").multiselect('updateButtonText');
            }
            else {
                $('#ddlVendor').multiselect('destroy');
                $('#ddlVendor').empty();
                $('#ddlVendor').multiselect({
                    columns: 1,
                    placeholder: 'Select Customer',
                    includeSelectAllOption: false
                });
                $("#ddlVendor").multiselect("deselectAll", false);/*For De-Select All By Default*/
                $("#ddlVendor").multiselect('updateButtonText');
            }
            /*Bind Vendor End*/
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function bindVendor(primaryitemid) {
    var Vendor = $("#ddlVendor");
    $.ajax({
        type: "POST",
        url: "/Masterfac/SubItemVendor",
        data: { PrimaryItemID: primaryitemid },
        async: false,
        dataType: "json",
        success: function (response) {
            var listVendor = response.allDataset.varVendor;
            /*Bind Vendor Start*/
            if (listVendor.length > 0) {
                $('#ddlVendor').multiselect('destroy');
                Vendor.empty().append('<option selected="selected" value="0">Please select</option>');;
                $.each(listVendor, function (index, record) {
                    Vendor.append($("<option></option>").val(this['VENDORID']).html(this['VENDORNAME']));
                });

                $('#ddlVendor').multiselect({
                    columns: 1,
                    placeholder: 'Select Vendor',
                    includeSelectAllOption: false
                });

                //$("#ddlVendor").multiselect('selectAll', false);/*For Select All By Default*/
                $("#ddlVendor").multiselect("deselectAll", false);/*For De-Select All By Default*/
                $("#ddlVendor").multiselect('updateButtonText');
            }
            else {
                $('#ddlVendor').multiselect('destroy');
                $('#ddlVendor').empty();
                $('#ddlVendor').multiselect({
                    columns: 1,
                    placeholder: 'Select Customer',
                    includeSelectAllOption: false
                });
                $("#ddlVendor").multiselect("deselectAll", false);/*For De-Select All By Default*/
                $("#ddlVendor").multiselect('updateButtonText');
            }
            /*Bind Vendor End*/
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function bindUom() {
    var Uom = $("#UomID");
    var PacksizeFrom = $("#PacksizeFrom");
    var PacksizeTo = $("#PacksizeTo");
    $.ajax({
        type: "POST",
        url: "/Masterfac/GetUom",
        data: '{}',
        async: false,
        dataType: "json",
        success: function (response) {
            Uom.empty().append('<option selected="selected" value="0">Please select</option>');
            PacksizeFrom.empty().append('<option selected="selected" value="0">Please select</option>');;
            PacksizeTo.empty().append('<option selected="selected" value="0">Please select</option>');;
            $.each(response, function () {
                Uom.append($("<option></option>").val(this['UOMID']).html(this['UOMDESCRIPTION']));
                PacksizeFrom.append($("<option></option>").val(this['UOMID']).html(this['UOMDESCRIPTION']));
                PacksizeTo.append($("<option></option>").val(this['UOMID']).html(this['UOMDESCRIPTION']));
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

function bindFactory() {
    var SourceDepot = $("#FactoryID");
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
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function bindCustomer(factoryid) {
    var Customer = $("#ddlCustomer");
    $.ajax({
        type: "POST",
        url: "/Masterfac/GetCustomer",
        data: { FactoryID: factoryid },
        async: false,
        dataType: "json",
        success: function (response) {
            $('#ddlCustomer').multiselect('destroy');
            Customer.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(response, function () {
                Customer.append($("<option></option>").val(this['CUSTOMERID']).html(this['CUSTOMERNAME']));
            });

            $('#ddlCustomer').multiselect({
                columns: 1,
                placeholder: 'Select Customer',
                includeSelectAllOption: false
            });

            //$("#ddlCustomer").multiselect('selectAll', false);/*For Select All By Default*/
            $("#ddlCustomer").multiselect("deselectAll", false);/*For De-Select All By Default*/
            $("#ddlCustomer").multiselect('updateButtonText');
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function bindSubItemRefresh(primaryitemID) {
    debugger;
    var Subitem = $("#SubItemID");
    $.ajax({
        type: "POST",
        url: "/Masterfac/GetSubItem",
        data: { PrimaryItemID: primaryitemID },
        async: false,
        dataType: "json",
        success: function (response) {
            debugger;
            Subitem.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(response, function () {
                Subitem.append($("<option></option>").val(this['SUBTYPEID']).html(this['SUBITEMDESC']));
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

function SaveProduct() {
    debugger;
    var i = 0;
    var j = 0;
    var k = 0;
    factorylist = new Array();
    vendorlist = new Array();
    customerlist = new Array();
    var materilamastersave = {};

    materilamastersave.MaterialID = $('#hdnMaterialID').val().trim();
    if ($('#hdnMaterialID').val() == '0') {
        materilamastersave.FLAG = 'A';
    }
    else {
        materilamastersave.FLAG = 'U';
    }
    materilamastersave.ProductOwner = $("#ProductOwner").val().trim();
    materilamastersave.ProductName = $("#ProductName").val().trim();
    materilamastersave.ProductCode = $("#ProductCode").val().trim();
    materilamastersave.PrimaryItemID = $("#PrimaryItemID").val().trim();
    materilamastersave.PrimaryItemText = $("#PrimaryItemID option:selected").text();
    materilamastersave.SubItemID = $("#SubItemID").val().trim();
    materilamastersave.SubItemText = $("#SubItemID option:selected").text();
    materilamastersave.UomID = $("#UomID").val().trim();
    materilamastersave.UomText = $("#UomID option:selected").text();
    materilamastersave.UnitCapacity = $("#UnitCapacity").val().trim();
    materilamastersave.MRP = $("#MRP").val().trim();
    if ($("#ReorderLevel").val().trim() == '') {
        materilamastersave.ReorderLevel = '0';
    }
    else {
        materilamastersave.ReorderLevel = $("#ReorderLevel").val().trim();
    }
    materilamastersave.FactoryID = $("#FactoryID").val().trim();
    materilamastersave.FactoryText = $("#FactoryID option:selected").text();
    materilamastersave.UnitCapacityInput = $("#UnitCapacityInput").val().trim();
    materilamastersave.PacksizeFrom = $("#PacksizeFrom").val().trim();
    materilamastersave.PacksizeFromText = $("#PacksizeFrom option:selected").text();
    materilamastersave.PacksizeTo = $("#PacksizeTo").val().trim();
    materilamastersave.PacksizeToText = $("#PacksizeTo option:selected").text();
    if ($("#Assesment").val().trim() == '') {
        materilamastersave.Assesment = '0';
    }
    else {
        materilamastersave.Assesment = $("#Assesment").val().trim();
    }
    

    if ($("#chkactive").prop('checked') == true) {
        materilamastersave.Active = 'T';
    }
    else {
        materilamastersave.Active = 'F';
    }

    if ($("#chkreturn").prop('checked') == true) {
        materilamastersave.Returnable = 'Y';
    }
    else {
        materilamastersave.Returnable = 'N';
    }


    /*Factory List Start*/
    var factory = $("#FactoryID option:selected");
    var factorymapid = '';
    var finalFactoryid = ''
    if (factory.length > 0) {
        factory.each(function () {
            var factorydetails = {};
            factorymapid += $(this).val().trim() + ',';
            var factoryid = $(this).val().trim();
            var factoryname = $(this).text().trim();
            factorydetails.VENDORID = factoryid;
            factorydetails.VENDORNAME = factoryname;
            factorylist[i++] = factorydetails;
        });
        finalFactoryid = factorymapid.substring(0, factorymapid.length - 1);
        materilamastersave.FactoryMapID = finalFactoryid;
        materilamastersave.FactoryTypeList = factorylist;
    }
    else {
        var factoryid = ''
        var factoryname = ''
        var factorydetails = {};
        factoryid = '0';
        factoryname = 'NA';
        factorydetails.VENDORID = factoryid;
        factorydetails.VENDORNAME = factoryname;
        factorylist[i++] = factorydetails;
        materilamastersave.FactoryMapID = finalFactoryid;
        materilamastersave.FactoryTypeList = factorylist;
    }
    
    /*Factory List End*/


    /*Vendor List Start*/
    var vendor = $("#ddlVendor option:selected");
    
    
    if (vendor.length > 0) {
        var vendormapid = '';
        var finalVendorid = ''
        vendor.each(function () {
            debugger;
            var vendordetails = {};
            vendormapid += $(this).val().trim() + ',';
            var vendorid = $(this).val().trim();
            var vendorname = $(this).text().trim();
            vendordetails.VENDORID = vendorid;
            vendordetails.VENDORNAME = vendorname;
            vendorlist[j++] = vendordetails;
        });
        finalVendorid = vendormapid.substring(0, vendormapid.length - 1);
        materilamastersave.VendorMapID = finalVendorid;
        materilamastersave.VendorTypeList = vendorlist;
    }
    else {

        var vendordetails = {};
        var vendormapid = '';
        var finalVendorid = ''
        var vendorid = ''
        var vendorname = ''
        vendorid = '0';
        vendorname = 'NA';
        vendordetails.VENDORID = vendorid;
        vendordetails.VENDORNAME = vendorname;
        vendorlist[j++] = vendordetails;
        materilamastersave.VendorMapID = finalVendorid;
        materilamastersave.VendorTypeList = vendorlist;
    }
    
    /*Vendor List End*/


    /*Customer List Start*/
    var customer = $("#ddlCustomer option:selected");
    var customermapid = '';
    var finalCustomerid = ''
    if (customer.length > 0) {
        customer.each(function () {
            var customerdetails = {};
            customermapid += $(this).val().trim() + ',';
            var customerid = $(this).val().trim();
            var customername = $(this).text().trim();
            customerdetails.CUSTOMERID = customerid;
            customerdetails.CUSTOMERNAME = customername;
            customerlist[k++] = customerdetails;
        });
        finalCustomerid = customermapid.substring(0, customermapid.length - 1);
        materilamastersave.CustomerMapID = finalCustomerid;
        materilamastersave.CustomerTypeList = customerlist;
    }
    else {
        var customerid = ''
        var customername = ''
        var customerdetails = {};
        customerid = '0';
        customername = 'NA';
        customerdetails.CUSTOMERID = customerid;
        customerdetails.CUSTOMERNAME = customername;
        customerlist[k++] = customerdetails;
        materilamastersave.CustomerMapID = finalCustomerid;
        materilamastersave.CustomerTypeList = customerlist;
    }
    
    /*Customer List End*/
    

    //alert(JSON.stringify(invoicesave));

    $.ajax({
        url: "/Masterfac/MaterialMasterSaveData",
        data: '{materilamastersave:' + JSON.stringify(materilamastersave) + '}',
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
               
                ClearControls();
                bindmaterialmastergrid();
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

function bindmaterialmastergrid(){
    debugger;
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
        url: "/Masterfac/BindMaterialMasterGrid",
        data: '{}',
        dataType: "json",
        success: function (response) {
            debugger;
            var tr;
            tr = $('#MaterialGrid');
            tr.append("<thead><tr><th>Sl. No.</th><th style='display: none'>Material ID</th><th>Owner</th><th>Code</th><th>Primary Item</th><th>Sub Item</th><th>Name</th><th>MRP</th><th>Status</th><th>Factory Map</th><th>Vendor Map</th><th>Customer Map</th><th>Unit Map</th><th>Edit</th><th style='display: none'>Delete</th>");

            $('#MaterialGrid').DataTable().destroy();
            $("#MaterialGrid tbody tr").remove();
            for (var i = 0; i < response.length; i++) {

                tr = $('<tr/>');
                tr.append("<td style='text-align: center'>" + srl + "</td>");//0
                tr.append("<td style='display: none'>" + response[i].ID.trim() + "</td>");//1
                tr.append("<td>" + response[i].PRODUCTOWNER.trim() + "</td>");//2
                tr.append("<td>" + response[i].CODE.trim() + "</td>");//3
                tr.append("<td>" + response[i].DIVNAME.trim() + "</td>");//4
                tr.append("<td>" + response[i].CATNAME.trim() + "</td>");//5
                tr.append("<td>" + response[i].PRODUCTALIAS.trim() + "</td>");//6
                tr.append("<td>" + response[i].MRP.trim() + "</td>");//7
                if (response[i].STATUS.trim() == 'Active') {
                    tr.append("<td style='text-align: center'><font color='0x00B300'>" + response[i].STATUS.trim() + "</font></td>");//8
                }
                else {
                    tr.append("<td style='text-align: center'><font color='#ff2500'>" + response[i].STATUS.trim() + "</font></td>");//8
                }
                tr.append("<td>" + response[i].BRANCHNAME + "</td>");//9
                tr.append("<td>" + response[i].VENDORNAME + "</td>");//10
                tr.append("<td>" + response[i].CUSTOMERNAME + "</td>");//11
                tr.append("<td>" + response[i].UNITMAP + "</td>");//12
                tr.append("<td style='text-align: center'><input type='image' class='gvEdit'   id='btnmaterialedit'   <img src='../Images/Pencil-icon.png' title='Edit'/></input></td>");//13                
                tr.append("<td style='text-align: center;display: none;'><input type='image' class='gvDelete' id='btnmaterialdelete' <img src='../Images/ico_delete_16.png' title='Delete'/></input></td>");//14

                $("#MaterialGrid").append(tr);
            }
            tr.append("</tbody>");
            RowCountMaterialList();
            $('#MaterialGrid').DataTable({
                "sScrollX": '100%',
                "sScrollXInner": "110%",
                "scrollY": "300px",
                "scrollCollapse": true,
                "dom": 'Bfrtip',
                buttons: [
                    {
                        extend: 'excelHtml5',
                        title: 'Material Master List'
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
                fixedColumns: {
                    leftColumns: 7
                },
                "columnDefs": [
                    { "orderable": false, "targets": 0}
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

function RowCountMaterialList() {
    debugger;
    var table = document.getElementById("MaterialGrid");
    var rowCount = document.getElementById("MaterialGrid").rows.length - 1;
    if (rowCount > 0) {
        for (var i = 1; i <= rowCount; i++) {
            table.rows[i].cells[0].innerHTML = i.toString();
        }
    }
}

function Exists(productname,productcode) {
    var returnValue = null;
    $.ajax({
        type: "POST",
        url: "/Masterfac/IsExists",
        data: { ProductName: productname, Code: productcode },
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

function EditDetails(productid) {
    debugger;
    var SubItem = $("#SubItemID");
    $("#dialog").dialog({
        autoOpen: true,
        modal: true,
        title: "Loading.."
    });
    $("#imgLoader").css("visibility", "visible");
    $.ajax({
        type: "POST",
        url: "/Masterfac/EditMaterial",
        data: { ProductID: productid },
        dataType: "json",
        async: false,
        success: function (response) {
            var listHeader = response.allDataset.varHeader;
            var listFactory = response.allDataset.varFactory;
            var listVendor = response.allDataset.varVendor;
            var listCustomer = response.allDataset.varCustomer;
            var listPacksize = response.allDataset.varPacksize;

            /*Material Master Info*/
            $.each(listHeader, function (index, record) {
                //debugger;
                $("#ProductOwner").val(this['PRODUCTOWNER'].toString().trim());
                $("#ProductName").val(this['PRODUCTALIAS'].toString().trim());
                $("#ProductCode").val(this['CODE'].toString().trim());
                $("#PrimaryItemID").val(this['DIVID'].toString().trim());
                SubItem.empty();
                SubItem.append($("<option></option>").val(this['CATID']).html(this['CATNAME']));
                $("#UomID").val(this['UOMID'].toString().trim());
                $("#UnitCapacity").val(this['UNITVALUE'].toString().trim());
                $("#MRP").val(this['MRP'].toString().trim());
                $("#ReorderLevel").val(this['REORDERLEVEL'].toString().trim());
                $("#Assesment").val(this['ASSESSABLEPERCENT'].toString().trim());

                if (this['RETURNABLE'].toString().trim() == 'Y') {
                    $("#chkreturn").prop("checked", true);
                }
                else {
                    $("#chkreturn").prop("checked", false);
                }

                if (this['ACTIVE'].toString().trim() == 'T') {
                    $("#chkactive").prop("checked", true);
                }
                else {
                    $("#chkactive").prop("checked", false);
                }
            });

            /*Factory Mapping Info*/
            $.each(listFactory, function (index, record) {
                $("#FactoryID").val(this['FACTORYID'].toString().trim());
            });

            /*Customer Mapping Info*/
            $.each(listCustomer, function (index, record) {
                var optionVal = this['CUSTOMERID'].toString().trim();
                $('#ddlCustomer').multiselect('select', [optionVal]);
            });

            /*Vendor Mapping Info*/
            bindVendor($("#PrimaryItemID").val().trim());
            $.each(listVendor, function (index, record) {
                var optionVal2 = this['VENDORID'].toString().trim();
                $('#ddlVendor').multiselect('select', [optionVal2]);
            });

            /*Packsize Mapping Info*/
            $.each(listPacksize, function (index, record) {
                $("#UnitCapacityInput").val(this['CONVERSIONQTY'].toString().trim());
                $("#PacksizeFrom").val(this['PACKSIZEID_FROM'].toString().trim());
                $("#PacksizeTo").val(this['PACKSIZEID_TO'].toString().trim());
            });


            $("#ProductName").attr("disabled", "disabled");
            debugger;
            if (listHeader[0].TYPE.toString().trim().toUpperCase() == 'SFG') {
                $("#ProductCode").removeAttr("disabled");
            }
            else {
                $("#ProductCode").attr("disabled", "disabled");
            }
            $("#PrimaryItemID").attr("disabled", "disabled");
            $("#SubItemID").attr("disabled", "disabled");
            $("#UomID").attr("disabled", "disabled");
            $("#UnitCapacity").attr("disabled", "disabled");
            $("#Assesment").attr("disabled", "disabled");

            $("#ProductOwner").chosen({
                search_contains: true
            });
            $("#ProductOwner").trigger("chosen:updated");
            
            $("#PrimaryItemID").chosen({
                search_contains: true
            });
            $("#PrimaryItemID").trigger("chosen:updated");
           
            $("#SubItemID").chosen({
                search_contains: true
            });
            $("#SubItemID").trigger("chosen:updated");
           
            $("#UomID").chosen({
                search_contains: true
            });
            $("#UomID").trigger("chosen:updated");

            $("#FactoryID").chosen({
                search_contains: true
            });
            $("#FactoryID").trigger("chosen:updated");

            $("#PacksizeFrom").chosen({
                search_contains: true
            });
            $("#PacksizeFrom").trigger("chosen:updated");

            $("#PacksizeTo").chosen({
                search_contains: true
            });
            $("#PacksizeTo").trigger("chosen:updated");

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

function DeleteProcess(processid) {
    debugger;

    $.ajax({
        type: "POST",
        url: "/Process/DeleteProcess",
        data: { ProcessID: processid },
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
                bindprocessmastergrid();
                toastr.success('<b><font color=black>' + messagetext + '</font></b>');
            }
            else {
                toastr.error('<b><font color=black>' + messagetext + '</font></b>');
            }
        }
    });
}

/*Edit function for Process*/
$(function () {
    var materialid;
    $("body").on("click", "#MaterialGrid .gvEdit", function () {
        var row = $(this).closest("tr");
        materialid = row.find('td:eq(1)').html();
        $('#hdnMaterialID').val(materialid);           
        $('#btnsave').css("display", "");
        $('#btnAddnew').css("display", "");
        $('#dvAdd').css("display", "");
        $('#dvDisplay').css("display", "none");

        EditDetails($('#hdnMaterialID').val().trim());
    })

})

/*Delete function for Process*/
$(function () {
    var processid;
    $("body").on("click", "#ProcessGrid .gvDelete", function () {
        var row = $(this).closest("tr");
        processid = row.find('td:eq(1)').html();
        $('#hdnprocessID').val(processid);
        if (confirm("Do you want to delete this item?")) {
            DeleteProcess(processid);
        }

    })

})