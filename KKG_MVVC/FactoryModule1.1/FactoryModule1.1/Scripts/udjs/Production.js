//#region Developer Info
/*
* For Production.cshtml Only
* Developer Name : Rajeev Kumar
* Start Date     : 06/10/2020
* END Date       :  
*/
//#endregion

$(document).ready(function () {
    finyearChecking();
    $('#dvAdd').css("display", "none");
    $('#dvDisplay').css("display", "");
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
    $('#divProductioNo').css("display", "none");

    $("#btnAddnew").click(function (e) {
        $('#dvAdd').css("display", "");
        $('#dvDisplay').css("display", "none");      
        //ClearControls();
        e.preventDefault();
    })

    $("#btnClose").click(function (e) {
        $('#dvAdd').css("display", "none");
        $('#dvDisplay').css("display", "");
        //ClearControls();
        e.preventDefault();
    })

    if ('@TempData["messageid"]' == "1") {
        toastr.success('@TempData["messagetext"]');
    }
    //bindSubItemgrid();
    //bindPrimaryItem();
    //bindFactory();

    $("#btnsave").click(function (e) {
        if ($("#ITEMOWNER").val() == '0') {
            toastr.warning('<b><font color=black>Please select item owner..!</font></b>');
            return false;
        }
        else if ($("#PRIMARYITEMTYPEID").val() == '0') {
            toastr.warning('<b><font color=black>Please select primary item..!</font></b>');
            return false;
        }        
        else if ($("#SUBITEMNAME").val() == '') {
            toastr.warning('<b><font color=black>Please enter sub item name..!</font></b>');
            return false;
        }
        else if ($("#SUBITEMDESC").val() == '') {
            toastr.warning('<b><font color=black>Please enter description..!</font></b>');
            return false;
        }
        else if ($("#HSE").val() == '') {
            toastr.warning('<b><font color=black>Please enter HSN..!</font></b>');
            return false;
        }
        else {
            if ($('#hdnSubItemID').val() == '0') {
                var isExists = Exists($("#SUBITEMNAME").val().trim(), $("#hdnSubItemID").val().trim());
                if (isExists != 'na') {
                    toastr.warning('<b>' + isExists + '</b>');
                    return false;
                }
                else {
                    SaveSubItem();
                }
            }
            else {
                SaveSubItem();
            }
        }
        e.preventDefault();
    })
});

function finyearChecking() {
    var currentdt;
    var frmdate;
    var todate;
    $.ajax({
        type: "POST",
        url: "/Tranfac/finyrchk",
        data: '{}',
        async: false,
        dataType: "json",
        success: function (response) {
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
    $("#ENTRY_DATE").datepicker({
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
        maxDate: new Date(currentdt),
        minDate: new Date(currentdt),
        buttonImage: 'http://jqueryui.com/resources/demos/datepicker/images/calendar.gif'
    });
    $(".ui-datepicker-trigger").mouseover(function () {
        $(this).css('cursor', 'pointer');
    });
        
    $("#txtfrmdate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
    $("#txttodate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
    $("#ENTRY_DATE").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());    
}