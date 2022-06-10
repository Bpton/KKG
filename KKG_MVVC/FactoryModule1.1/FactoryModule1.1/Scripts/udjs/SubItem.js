//#region Developer Info
/*
* For SubItemMaster.cshtml Only
* Developer Name : Rajeev Kumar
* Start Date     : 29/09/2020
* END Date       :  
*/
//#endregion

$(document).ready(function () {
    $('#addnew').css("display", "none");
    $('#showdata').css("display", "");
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
    $("#btnnewentry").click(function (e) {
        $('#addnew').css("display", "");
        $('#showdata').css("display", "none");
        ClearControls();
        e.preventDefault();
    })

    $("#btnClose").click(function (e) {
        $('#addnew').css("display", "none");
        $('#showdata').css("display", "");
        ClearControls();
        e.preventDefault();
    })

    if ('@TempData["messageid"]' == "1") {
        toastr.success('@TempData["messagetext"]');
    }
    bindSubItemgrid();
    bindPrimaryItem();
    bindFactory();

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

function ClearControls() {
    $("#ITEMOWNER").val('0');
    $("#ITEMOWNER").chosen({
        search_contains: true
    });
    $("#ITEMOWNER").trigger("chosen:updated");
    $("#ITEMOWNER").removeAttr("disabled");

    $("#PRIMARYITEMTYPEID").val('0');
    $("#PRIMARYITEMTYPEID").chosen({
        search_contains: true
    });
    $("#PRIMARYITEMTYPEID").trigger("chosen:updated");
    $("#PRIMARYITEMTYPEID").removeAttr("disabled");

    $("#ITEMDESC").removeAttr("disabled");
    $("#ALTERNATECODE").removeAttr("disabled");
    $("#SUBITEMCODE").attr("disabled", "disabled");
    $("#SUBITEMNAME").removeAttr("disabled");
    $("#SUBITEMDESC").removeAttr("disabled");
    $("#HSE").removeAttr("disabled");
    $("#chkactive").prop("checked", true);

    $("#ITEMDESC").val('');
    $("#ALTERNATECODE").val('');
    $("#SUBITEMCODE").val('');
    $("#SUBITEMNAME").val('');
    $("#SUBITEMDESC").val('');
    $("#HSE").val('');
    $('#hdnSubItemID').val('0');
}

function DisableControls() {
    $("#ITEMOWNER").attr("disabled", "disabled");
    $("#ITEMOWNER").chosen({
        search_contains: true
    });
    $("#ITEMOWNER").trigger("chosen:updated");

    $("#PRIMARYITEMTYPEID").attr("disabled", "disabled");   
    $("#PRIMARYITEMTYPEID").chosen({
        search_contains: true
    });
    $("#PRIMARYITEMTYPEID").trigger("chosen:updated");

    $("#SUBITEMCODE").attr("disabled", "disabled");
    $("#SUBITEMNAME").attr("disabled", "disabled");
    $("#SUBITEMDESC").attr("disabled", "disabled");
    $("#HSE").attr("disabled", "disabled");
    $("#chkactive").attr("disabled", "disabled");
}

function bindPrimaryItem() {
    var PrimaryItem = $("#PRIMARYITEMTYPEID");
    $.ajax({
        type: "POST",
        url: "/Masterfac/GetPrimaryItem",
        data: '{}',
        async: false,
        dataType: "json",
        success: function (response) {
            PrimaryItem.empty().append('<option selected="selected" value="0">Please select</option>');;
            $.each(response, function () {
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

function bindFactory() {
    var SourceDepot = $("#BRID");
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

function Exists(PrimaryName, PrimaryID) {
    var returnValue = null;
    $.ajax({
        type: "POST",
        url: "/SubItem/IsExists",
        data: { Name: PrimaryName, ID: PrimaryID },
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

function SaveSubItem() {
    var SubItemSave = {};
    SubItemSave.SUBTYPEID = $('#hdnSubItemID').val().trim();
    if ($('#hdnSubItemID').val() == '0') {
        SubItemSave.MODE = 'A';
    }
    else {
        SubItemSave.MODE = 'U';
    }
    SubItemSave.ITEMOWNER = $("#ITEMOWNER").val();
    SubItemSave.PRIMARYITEMTYPEID = $("#PRIMARYITEMTYPEID").val();
    SubItemSave.SUBITEMCODE = $("#SUBITEMCODE").val();
    SubItemSave.SUBITEMNAME = $("#SUBITEMNAME").val();
    SubItemSave.SUBITEMDESC = $("#SUBITEMDESC").val();
    SubItemSave.HSE = $("#HSE").val();
    SubItemSave.BRID = $("#BRID").val();

    if ($("#chkactive").prop('checked') == true) {
        SubItemSave.ACTIVE = 'Y';
    }
    else {
        SubItemSave.ACTIVE = 'N';
    }
    $.ajax({
        url: " /SubItem/SubItemSave",
        data: '{SubItemSave: ' + JSON.stringify(SubItemSave) + '}',
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
            if (messageid != '0') {
                $('#addnew').css("display", "none");
                $('#showdata').css("display", "");
                ClearControls();
                bindSubItemgrid();
                toastr.success('<b><font color=black>' + messagetext + '</font></b>');
            }
            else {
                $('#addnew').css("display", "");
                $('#showdata').css("display", "none");
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

/*Edit function for Sub Item*/
$(function () {
    var SubItemid;
    $("body").on("click", "#SubItemGrid .gvEdit", function () {
        var row = $(this).closest("tr");
        SubItemid = row.find('td:eq(0)').html();
        $('#hdnSubItemID').val(SubItemid);
        $('#btnsave').css("display", "");
        $('#addnew').css("display", "");
        $('#showdata').css("display", "none");
        EditSubItemDetails($('#hdnSubItemID').val().trim());
    })
})

function EditSubItemDetails(SubItemid) {
    $("#dialog").dialog({
        autoOpen: true,
        modal: true,
        title: "Loading.."
    });
    $("#imgLoader").css("visibility", "visible");
    $.ajax({
        type: "POST",
        url: "/SubItem/EditSubItem",
        data: { SubItemID: SubItemid },
        dataType: "json",
        async: false,
        success: function (response) {
            var listHeader = response.allDataset.varHeader;
            /*Sub Item Master Info*/
            $.each(listHeader, function (index, record) {
                $("#ITEMOWNER").val(this['ITEMOWNER'].toString().trim());
                $("#PRIMARYITEMTYPEID").val(this['PRIMARYITEMTYPEID'].toString().trim());
                $("#SUBITEMCODE").val(this['SUBITEMCODE'].toString().trim());
                $("#SUBITEMNAME").val(this['SUBITEMNAME'].toString().trim());
                $("#SUBITEMDESC").val(this['SUBITEMDESC'].toString().trim());
                $("#HSE").val(this['HSE'].toString().trim());
                if (this['BRID'].toString().trim() == '') {
                }
                else {
                    $("#BRID").val(this['BRID'].toString().trim());
                }
                if (this['STATUS'].toString().trim() == 'Y') {
                    $("#chkactive").prop("checked", true);
                }
                else {
                    $("#chkactive").prop("checked", false);
                }
                DisableControls();
                $("#imgLoader").css("visibility", "hidden");
                $("#dialog").dialog("close");
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

function bindSubItemgrid() {
    $("#dialog").dialog({
        autoOpen: true,
        modal: true,
        title: "Loading.."
    });
    $("#imgLoader").css("visibility", "visible");
    $.ajax({
        type: "POST",
        url: "/SubItem/BindSubItem",
        dataType: "json",
        success: function (response) {
            var tr;
            tr = $('#SubItemGrid');
            tr.append("<thead><tr><th style='display: none'>SUBTYPEID</th><th>ITEM OWNER</th><th style='display: none'>PRIMARYITEMTYPEID</th><th>PRIMARY ITEM</th><th>ALTERNATE CODE</th><th>SUBITEM CODE</th><th>SUBITEM NAME</th><th>SUBITEM DESC</th><th>HSN</th><th>STATUS</th><th style='display: none'>BRID</th><th>BRPREFIX</th><th>Edit</th></tr></thead><tbody>");
            $('#SubItemGrid').DataTable().destroy();
            $("#SubItemGrid tbody tr").remove();
            for (var i = 0; i < response.length; i++) {
                tr = $('<tr/>');
                tr.append("<td style='display: none'>" + response[i].SUBTYPEID + "</td>");
                tr.append("<td>" + response[i].ITEMOWNER + "</td>");
                tr.append("<td style='display: none'>" + response[i].PRIMARYITEMTYPEID + "</td>");
                tr.append("<td>" + response[i].ITEMDESC + "</td>");
                tr.append("<td>" + response[i].ALTERNATECODE + "</td>");
                tr.append("<td>" + response[i].SUBITEMCODE + "</td>");
                tr.append("<td>" + response[i].SUBITEMNAME + "</td>");
                tr.append("<td>" + response[i].SUBITEMDESC + "</td>");
                tr.append("<td>" + response[i].HSE + "</td>");
                if (response[i].STATUS.trim() == 'ACTIVE') {
                    tr.append("<td style='text-align: center'><font color='0x00B300'>" + response[i].STATUS.trim() + "</font></td>");
                }
                else {
                    tr.append("<td style='text-align: center'><font color='#ff2500'>" + response[i].STATUS.trim() + "</font></td>");
                }
                tr.append("<td style='display: none'>" + response[i].BRID + "</td>");
                tr.append("<td>" + response[i].BRPREFIX + "</td>");
                tr.append("<td style='text-align: center'><input type='image' class='gvEdit' id='btnmaterialedit' <img src='../Images/Pencil-icon.png' title='Edit'/></input></td>");
                $("#SubItemGrid").append(tr);
            }
            tr.append("</tbody>");

            $('#SubItemGrid').DataTable({
                "sScrollX": '100%',
                "sScrollXInner": "110%",
                "scrollY": "238px",
                "scrollCollapse": true,
                "dom": 'Bfrtip',
                buttons: [
                    {
                        extend: 'excelHtml5',
                        title: 'Primary Item'
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
                    leftColumns: 4
                },
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