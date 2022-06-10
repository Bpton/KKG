$(document).ready(function () {

    LOADBRAND();
    LOADCATEGORY();
    $('#BrandId').change(function () {
        BindBrandByCatname();
    });
    LOADNATURE();
    LOADUOM();
    LOADFRAGNANCE();
    LOADITEMTYPE();
    LOADDEPOT();
})

function LOADBRAND() {
    debugger;
    $.ajax({
        type: "POST",
        url: "/Masterfac/LOADBRAND",
        data: '{}',
        async: false,
        data: { },
        success: function (result) {
            var vendorid = $("#BrandId");
            vendorid.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(result, function () {
                $('#BrandId').val(this['BrandId']);
                vendorid.append($("<option value=''></option>").val(this['BrandId']).html(this['BrandName']));
            });
            $("#BrandId").chosen();
            $("#BrandId").trigger("chosen:updated");
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });

}

function LOADCATEGORY() {
    debugger;
    $.ajax({
        type: "POST",
        url: "/Masterfac/LOADCATEGORY",
        data: '{}',
        async: false,
        data: {},
        success: function (result) {
            var vendorid = $("#CATID");
            vendorid.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(result, function () {
                $('#CATID').val(this['CATID']);
                vendorid.append($("<option value=''></option>").val(this['CATID']).html(this['CATNAME']));
            });
            $("#CATID").chosen();
            $("#CATID").trigger("chosen:updated");
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });

}

function BindBrandByCatname() {

    var BRANDID = $("#BrandId").val();
    var CATID = $("#CATID").val();
    debugger;
    $.ajax({
        type: "POST",
        url: "/Masterfac/BindBrandByCatname",
        data: { BRANDID: BRANDID },
        async: false,
        dataType: "json",
        success: function (response) {
            //alert(JSON.stringify(response));
            CATID.empty().append('<option selected="selected" value="0">Select </option>');

            $.each(response, function () {
                CATID.append($("<option></option>").val(this['CATID']).html(this['CATNAME']));
            });
            $("#CATID").chosen({
                search_contains: true
            });


            $("#CATID").trigger("chosen:updated");
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function LOADNATURE() {
    debugger;
    $.ajax({
        type: "POST",
        url: "/Masterfac/LOADNATURE",
        data: '{}',
        async: false,
        data: {},
        success: function (result) {
            var vendorid = $("#NOPID");
            vendorid.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(result, function () {
                $('#NOPID').val(this['NOPID']);
                vendorid.append($("<option value=''></option>").val(this['NOPID']).html(this['NOPNAME']));
            });
            $("#NOPID").chosen();
            $("#NOPID").trigger("chosen:updated");
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });

}

function LOADUOM() {
    debugger;
    $.ajax({
        type: "POST",
        url: "/Masterfac/LOADUOM    ",
        data: '{}',
        async: false,
        data: {},
        success: function (result) {
            var vendorid = $("#UOMID");
            vendorid.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(result, function () {
                $('#UOMID').val(this['UOMID']);
                vendorid.append($("<option value=''></option>").val(this['UOMID']).html(this['UOMNAME']));
            });
            $("#UOMID").chosen();
            $("#UOMID").trigger("chosen:updated");
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });

}

function LOADFRAGNANCE() {
    debugger;
    $.ajax({
        type: "POST",
        url: "/Masterfac/LOADFRAGNANCE ",
        data: '{}',
        async: false,
        data: {},
        success: function (result) {
            var vendorid = $("#FRGID");
            vendorid.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(result, function () {
                $('#FRGID').val(this['FRGID']);
                vendorid.append($("<option value=''></option>").val(this['FRGID']).html(this['FRGNAME']));
            });
            $("#FRGID").chosen();
            $("#FRGID").trigger("chosen:updated");
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });

}

function LOADITEMTYPE() {
    debugger;
    $.ajax({
        type: "POST",
        url: "/Masterfac/LOADITEMTYPE ",
        data: '{}',
        async: false,
        data: {},
        success: function (result) {
            var vendorid = $("#ITID");
            vendorid.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(result, function () {
                $('#ITID').val(this['ITID']);
                vendorid.append($("<option value=''></option>").val(this['ITID']).html(this['ITNAME']));
            });
            $("#ITID").chosen();
            $("#ITID").trigger("chosen:updated");
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });

}

function LOADITEMTYPE() {
    debugger;
    $.ajax({
        type: "POST",
        url: "/Masterfac/LOADDEPOT ",
        data: '{}',
        async: false,
        data: {},
        success: function (result) {
            var vendorid = $("#BRID");
            vendorid.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(result, function () {
                $('#BRID').val(this['BRID']);
                vendorid.append($("<option value=''></option>").val(this['BRID']).html(this['BRNAME']));
            });
            $("#BRID").chosen();
            $("#BRID").trigger("chosen:updated");
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });

}


