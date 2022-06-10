//#region Developer Info
/*
* For ProcessMaster.cshtml Only
* Developer Name : Avishek Ghosh 
* Start Date     : 16/04/2020
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
    bindprocessmastergrid();

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
        bindprocessmastergrid();
        e.preventDefault();
    })

    $("#btnsave").click(function (e) {
        debugger;
        if ($("#Processcode").val() == '') {
            toastr.warning('<b><font color=black>Please enter process code..!</font></b>');
            return false;
        }
        else if ($("#Processname").val() == '') {
            toastr.warning('<b><font color=black>Please enter process name..!</font></b>');
            return false;
        }
        else {
            SaveProcess();
        }
        e.preventDefault();
    })
});

function ClearControls() {
    $("#dvAdd").find("input,submit").removeAttr("disabled");
    $("#Processcode").val('');
    $("#Processname").val('');
    $("#ProcessDesc").val('');
    $("#chkactive").prop("checked", true);
    $('#hdnprocessID').val('0');
}

function SaveProcess() {
    debugger;
    var processsave = {};

    processsave.Processid = $('#hdnprocessID').val().trim();
    if ($('#hdnprocessID').val() == '0') {
        processsave.FLAG = 'I';
    }
    else {
        processsave.FLAG = 'U';
    }
    processsave.Processcode = $("#Processcode").val().trim();
    processsave.Processname = $("#Processname").val().trim();
    processsave.ProcessDesc = $("#ProcessDesc").val().trim();
    if ($("#chkactive").prop('checked') == true) {
        processsave.active = '1';
    }
    else {
        processsave.active = '0';
    }
    
    //alert(JSON.stringify(invoicesave));

    $.ajax({
        url: "/Process/saveProcessMaster",
        data: '{processsave:' + JSON.stringify(processsave) + '}',
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
                bindprocessmastergrid();
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

function bindprocessmastergrid(){
    debugger;
    var ProcessID = '';
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
        url: "/Process/BindProcessMasterGrid",
        data: { processid: ProcessID},
        dataType: "json",
        success: function (response) {
            debugger;
            var tr;
            tr = $('#ProcessGrid');
            tr.append("<thead><tr><th>Sl. No.</th><th style='display: none'>Process ID</th><th>Process Code</th><th>Process Name</th><th>Description</th><th>Active</th><th>Edit</th><th>Delete</th>");

            $('#ProcessGrid').DataTable().destroy();
            $("#ProcessGrid tbody tr").remove();
            for (var i = 0; i < response.length; i++) {
                tr = $('<tr/>');
                tr.append("<td style='text-align: center'>" + srl + "</td>");//0
                tr.append("<td style='display: none'>" + response[i].ProcessID + "</td>");//1
                tr.append("<td>" + response[i].ProcessCode + "</td>");//2
                tr.append("<td>" + response[i].ProcessName + "</td>");//3
                tr.append("<td>" + response[i].ProcessDescription + "</td>");//4
                if (response[i].Active.trim() == 'Active') {
                    tr.append("<td style='text-align: center'><font color='0x00B300'>" + response[i].Active + "</font></td>");//5
                }
                else {
                    tr.append("<td style='text-align: center'><font color='#ff2500'>" + response[i].Active + "</font></td>");//5
                }
                tr.append("<td style='text-align: center'><input type='image' class='gvEdit'   id='btnprocessedit'   <img src='../Images/Pencil-icon.png' title='Edit'/></input></td>");                
                tr.append("<td style='text-align: center'><input type='image' class='gvDelete' id='btnprocessdelete' <img src='../Images/ico_delete_16.png' title='Delete'/></input></td>");

                $("#ProcessGrid").append(tr);
            }
            tr.append("</tbody>");
            RowCountProcessList();
            $('#ProcessGrid').DataTable({
                "sScrollX": '100%',
                "sScrollXInner": "110%",
                "scrollY": "300px",
                "scrollCollapse": true,
                "dom": 'Bfrtip',
                buttons: [
                    {
                        extend: 'excelHtml5',
                        title: 'Process Master List'
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

function RowCountProcessList() {
    var table = document.getElementById("ProcessGrid");
    var rowCount = document.getElementById("ProcessGrid").rows.length - 1;
    if (rowCount > 0) {
        for (var i = 1; i <= rowCount; i++) {
            table.rows[i].cells[0].innerHTML = i.toString();
        }
    }
}

function EditDetails(processid) {
    debugger;
   
    $("#dialog").dialog({
        autoOpen: true,
        modal: true,
        title: "Loading.."
    });
    $("#imgLoader").css("visibility", "visible");
    $.ajax({
        type: "POST",
        url: "/Process/EditProcess",
        data: { Processid: processid },
        dataType: "json",
        async: false,
        success: function (editlist) {
            //alert(JSON.stringify(response))
            if (editlist.length > 0) {
                $.each(editlist, function (key, item) {
                    debugger;
                    $("#Processcode").val(editlist[0].ProcessCode.trim());
                    $("#Processname").val(editlist[0].ProcessName.trim());
                    $("#ProcessDesc").val(editlist[0].ProcessDescription.trim());
                    if (editlist[0].Active.trim() == 'Active') {
                        $("#chkactive").prop("checked", true);
                    }
                    else {
                        $("#chkactive").prop("checked", false);
                    }
                });
            }
            else {
                $("#Processcode").val('');
                $("#Processname").val('');
                $("#ProcessDesc").val('');
                $("#chkactive").prop("checked", true);
            }
            $("#imgLoader").css("visibility", "hidden");
            $("#dialog").dialog("close");
        },
        failure: function (editlist) {
            alert(editlist.responseText);
        },
        error: function (editlist) {
            alert(editlist.responseText);
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
    var processid;
    $("body").on("click", "#ProcessGrid .gvEdit", function () {
        var row = $(this).closest("tr");
        processid = row.find('td:eq(1)').html();
        $('#hdnprocessID').val(processid);           
        $('#btnsave').css("display", "");
        $('#btnAddnew').css("display", "");
        $('#btnApprove').css("display", "none");
        $("#dvAdd").find("input,submit").removeAttr("disabled");
        $('#dvAdd').css("display", "");
        $('#dvDisplay').css("display", "none");

        EditDetails($('#hdnprocessID').val().trim());
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