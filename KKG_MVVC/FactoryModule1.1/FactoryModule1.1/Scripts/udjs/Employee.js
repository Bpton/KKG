/*Developer:Pritam Basu
Start Date:16/12/2021*/

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
    debugger;
    bindEmployeegrid();//bind Emploee details


    $("body").on("click", "#grdEmployee .gvPromot", function () {
        debugger;
        var Employeid;
        var row = $(this).closest("tr");
        $("#popUpdiv").show();
        Employeid = row.find('td:eq(1)').html();
        $('#hdnEmployeId').val(Employeid);
        loadDesignantion();


    })

    $("body").on("click", "#grdEmployee .gvDemot", function () {
        var Employeid;
        var row = $(this).closest("tr");
        $("#popUpdiv").show();
        Employeid = row.find('td:eq(1)').html();
        $('#hdnEmployeId').val(Employeid);
        loadDesignantion();


    })

    $("#btnsave").click(function (e) {/*for updating data*/
        var employeId = document.getElementById("hdnEmployeId").value
        var designationId = $("#ddlDesignation").val();
        $.ajax({
            url: "/Process/UpdateEmployeDesignnation",
            data: { id: employeId, dId: designationId },
            type: "POST",
            datatype: "json",
            async: true,
            traditional: true,
            success: function (responseMessage) {
                debugger;
                var messageid;
                var messagetext;
                $.each(responseMessage, function (key, item) {
                    debugger;
                    messageid = item.MessageID;
                    messagetext = item.MessageText;
                });
                debugger;
                if (messageid == '1') {
                    alert('Update Done');
                    bindEmployeegrid();
                }
                else {
                    alert('Error');
                }
                $("#popUpdiv").hide();
            },
            failure: function (responseMessage) {
                alert(responseMessage.responseText);
            },
            error: function (responseMessage) {
                alert(responseMessage.responseText);
            }
        });
    })


})

function bindEmployeegrid() {
    $.ajax({
        type: "POST",
        url: "/Process/BindEmployeeDetials",
        data: {},
        dataType: "json",
        success: function (response) {
            debugger;
            var tr;
            var HeaderCount = 0;
            var srl = 1;
            $("#grdEmployee").empty();
            if (response.length > 0) {
                tr = $('#grdEmployee');
                HeaderCount = $('#grdEmployee thead th').length;
                if (HeaderCount == 0) {
                    tr.append("<thead><tr><th>Sl. No.</th><th style='display: none'>EmployeId</th><th>Employee Name</th><th>Designation</th><th>Salary</th><th>PROMOTED</th><th>DEMOTED</th></tr></thead><tbody>");
                }
                for (var i = 0; i < response.length; i++) {
                    debugger;
                    tr = $('<tr/>');
                    tr.append("<td>" + srl + "</td>");//0
                    tr.append("<td style='display: none'>" + response[i].EmployeId + "</td>");//1
                    tr.append("<td>" + response[i].EmployeeName + "</td>");//2
                    tr.append("<td>" + response[i].Designation + "</td>");//3
                    tr.append("<td>" + response[i].salary + "</td>");//3
                    if (response[i].Ismaxsalary.trim() == 'Y') {
                        tr.append("<td style='text-align: center'><input type='image' <img src='../Images/Pencil-icon.png' title='Edit'/></input></td>");
                    }
                    else {
                        tr.append("<td style='text-align: center'><input type='image' class='gvPromot'   id='btngvPromot'   <img src='../Images/Pencil-icon.png' title='Edit'/></input></td>");
                    }
                    if (response[i].Isminsalary.trim() == 'Y') {
                        tr.append("<td style='text-align: center'><input type='image'  <img src='../Images/Pencil-icon.png' /></input></td>");   
                    }
                    else {
                        tr.append("<td style='text-align: center'><input type='image' class='gvDemot'   id='btnDemot'   <img src='../Images/Pencil-icon.png' title='Edit'/></input></td>");
                    }
                    
                    
                    $("#grdEmployee").append(tr);
                    srl = srl + 1;
                }
                tr.append("</tbody>");

            }
        }
    })

}


function loadDesignantion() {
    $.ajax({
        type: "POST",
        url: "/Process/LoadDesignantion",
        data: {},
        async: false,
        success: function (result) {

            var ddlDesignation = $("#ddlDesignation");
            ddlDesignation.empty().append('<option selected="selected" value="0">select Designation</option>');
            $.each(result, function () {
                ddlDesignation.val(this['EmployeId']);
                ddlDesignation.append($("<option value=''></option>").val(this['EmployeId']).html(this['Designation']));
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




