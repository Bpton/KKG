//#region Developer Info
/*
* For PrimaryItemMaster.cshtml Only
* Developer Name : Rajeev Kumar
* Start Date     : 24/09/2020
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
        //document.getElementById('btnnewentry').style.visibility = 'hidden';
        //document.getElementById('btnClose').style.visibility = '';
        ClearControls();
        e.preventDefault();
    })

    $("#btnClose").click(function (e) {
        $('#addnew').css("display", "none");
        $('#showdata').css("display", "");
        //document.getElementById('btnClose').style.visibility = 'hidden';
        //document.getElementById('btnnewentry').style.visibility = '';
        ClearControls();
        e.preventDefault();
    })

    if ('@TempData["messageid"]' == "1") {        
        toastr.success('@TempData["messagetext"]');
    }
    bindPrimaryItemgrid();

    $("#btnsave").click(function (e) {
        if ($("#ItemOwner").val() == '0') {
            toastr.warning('<b><font color=black>Please select product owner..!</font></b>');
            return false;
        }
        else if ($("#ITEM_Name").val() == '') {
            toastr.warning('<b><font color=black>Please enter name..!</font></b>');
            return false;
        }
        else if ($("#ITEMDESC").val() == '') {
            toastr.warning('<b><font color=black>Please enter description..!</font></b>');
            return false;
        }
        else {
            if ($('#hdnPrimaryID').val() == '0') {
                var isExists = Exists($("#ITEM_Name").val().trim(), $("#hdnPrimaryID").val().trim());
                if (isExists != 'na') {
                    toastr.warning('<b>' + isExists + '</b>');
                    return false;
                }
                else {
                    SavePrimaryItem();
                }
            }
            else {
                SavePrimaryItem();
            }
        }
        e.preventDefault();
    })
});

function bindPrimaryItemgrid() {
    $("#dialog").dialog({
        autoOpen: true,
        modal: true,
        title: "Loading.."
    });
    $("#imgLoader").css("visibility", "visible");
    $.ajax({
        type: "POST",
        url: "/PrimaryItem/BindPrimaryItem",
        //data: {FromDate: $('#txtfrmdate').val(), ToDate: $('#txttodate').val() },
        dataType: "json",
        success: function (response) {
            var tr;
            tr = $('#PrimaryItemGrid');
            tr.append("<thead><tr><th style='display: none'>ID</th><th>ITEM OWNER</th><th>ALTERNATE CODE</th><th>ITEM CODE</th><th>ITEM NAME</th><th>ITEM DESC</th><th>STATUS</th><th style='display: none'>PREDEFINE</th><th style='display: none'>ACTIVE</th><th>Edit</th></tr></thead><tbody>");
            $('#PrimaryItemGrid').DataTable().destroy();
            $("#PrimaryItemGrid tbody tr").remove();
            for (var i = 0; i < response.length; i++) {
                tr = $('<tr/>');
                tr.append("<td style='display: none'>" + response[i].ID + "</td>");
                tr.append("<td>" + response[i].ITEMOWNER + "</td>");
                tr.append("<td>" + response[i].ALTERNATECODE + "</td>");
                tr.append("<td>" + response[i].ITEMCODE + "</td>");
                tr.append("<td>" + response[i].ITEM_Name + "</td>");
                tr.append("<td>" + response[i].ITEMDESC + "</td>");
                //tr.append("<td>" + response[i].STATUS + "</td>");

                if (response[i].STATUS.trim() == 'ACTIVE') {
                    tr.append("<td style='text-align: center'><font color='0x00B300'>" + response[i].STATUS.trim() + "</font></td>");//8
                }
                else {
                    tr.append("<td style='text-align: center'><font color='#ff2500'>" + response[i].STATUS.trim() + "</font></td>");//8
                }
                tr.append("<td style='display: none'>" + response[i].PREDEFINE + "</td>");
                tr.append("<td style='display: none'>" + response[i].ACTIVE + "</td>");
                tr.append("<td style='text-align: center'><input type='image' class='gvEdit' id='btnmaterialedit' <img src='../Images/Pencil-icon.png' title='Edit'/></input></td>");
                $("#PrimaryItemGrid").append(tr);
            }
            tr.append("</tbody>");

            $('#PrimaryItemGrid').DataTable({
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

function Exists(PrimaryName, PrimaryID) {
    var returnValue = null;
    $.ajax({
        type: "POST",
        url: "/PrimaryItem/IsExists",
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

function SavePrimaryItem() {
    var PrimaryItemSave = {};
    PrimaryItemSave.ID = $('#hdnPrimaryID').val().trim();    
    if ($('#hdnPrimaryID').val() == '0') {
        PrimaryItemSave.MODE = 'A';
    }
    else {
        PrimaryItemSave.MODE = 'U';
    }
    PrimaryItemSave.ITEMOWNER = $("#ItemOwner").val();
    PrimaryItemSave.ITEMCODE = $("#ITEMCODE").val();
    PrimaryItemSave.ITEM_Name = $("#ITEM_Name").val();
    PrimaryItemSave.ITEMDESC = $("#ITEMDESC").val();
    
    if ($("#chkServiceType").prop('checked') == true) {
        PrimaryItemSave.ISSERVICE = 'Y';
    }
    else {
        PrimaryItemSave.ISSERVICE = 'N';
    }

    if ($("#chkactive").prop('checked') == true) {
        PrimaryItemSave.ACTIVE = 'Y';
    }
    else {
        PrimaryItemSave.ACTIVE = 'N';
    }
    $.ajax({
        url: " /PrimaryItem/PrimaryItemSave",
        //data: JSON.stringify(CRUDPrimaryItem),
        data: '{PrimaryItemSave: ' + JSON.stringify(PrimaryItemSave) + '}',
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
                bindPrimaryItemgrid();
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

function EditDetails(Primaryid) {
    $("#dialog").dialog({
        autoOpen: true,
        modal: true,
        title: "Loading.."
    });
    $("#imgLoader").css("visibility", "visible");
    $.ajax({
        type: "POST",
        url: "/PrimaryItem/EditMaterial",
        data: { PrimaryID: Primaryid },
        dataType: "json",
        async: false,
        success: function (response) {
            var listHeader = response.allDataset.varHeader;
            /*Primary Item Master Info*/
            $.each(listHeader, function (index, record) {                
                $("#ItemOwner").val(this['ITEMOWNER'].toString().trim());
                $("#ITEMCODE").val(this['ITEMCODE'].toString().trim());
                $("#ITEM_Name").val(this['ITEM_Name'].toString().trim());
                $("#ITEMDESC").val(this['ITEMDESC'].toString().trim());

                if (this['ISSERVICE'].toString().trim() == 'Y') {
                    $("#chkServiceType").prop("checked", true);
                }
                else {
                    $("#chkServiceType").prop("checked", false);
                }

                if (this['ACTIVE'].toString().trim() == 'Y') {
                    $("#chkactive").prop("checked", true);
                }
                else {
                    $("#chkactive").prop("checked", false);
                }

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

/*Edit function for Primary Item*/
$(function () {
    var Primaryid, Predefine;
    $("body").on("click", "#PrimaryItemGrid .gvEdit", function () {
        var row = $(this).closest("tr");
        Primaryid = row.find('td:eq(0)').html();
        Predefine = row.find('td:eq(7)').html();        
        if (Predefine == "Y") {
            toastr.warning('<b><font color=black>Predefined Item Type can not be edited!</font></b>');
            return false;
        }
        else {
            $('#hdnPrimaryID').val(Primaryid);
            $('#btnsave').css("display", "");
            $('#addnew').css("display", "");
            $('#showdata').css("display", "none");

            EditDetails($('#hdnPrimaryID').val().trim());
        }
    })
})

function ClearControls() {
    $("#ItemOwner").val('0');
    //$("#ItemOwner").chosen({
    //    search_contains: true
    //});
    //$("#ItemOwner").trigger("chosen:updated");
    
    $("#ITEMCODE").attr("disabled", "disabled");
    $("#ITEM_Name").removeAttr("disabled");
    $("#ITEMDESC").removeAttr("disabled");   

    $("#chkServiceType").prop("checked", true);
    $("#chkactive").prop("checked", true);

    $("#ITEMCODE").val('');
    $("#ITEM_Name").val('');
    $("#ITEMDESC").val('');
    $('#hdnPrimaryID').val('0');
    //var R = $('#hdnPrimaryID').val().trim();
    //alert(R);
}