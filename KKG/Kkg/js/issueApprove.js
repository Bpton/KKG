$(document).ready(function () {
    bindPendingIssue();
    $(".issueSearch").button().click(function () {
        bindPendingIssue();
    });
})

function bindPendingIssue() {
    debugger;
    let fromDate = $('#fromDatePicker').html();
    let toDate = $('#toDatePicker').html();
    let Opertion = 1;
    try {
        $.ajax({
            type: "POST",
            url: "frmIssueApprove.aspx/LoadPendingIssue",
            data: '{Opertion: "' + Opertion + '",FromDate: "' + fromDate + '",ToDate: "' + toDate + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (msg) {
                var json = JSON.parse(msg.d);
                $.each(json, function (index, obj) {
                });
            }
        });
    }
    catch (ex) {
        alert(ex);
    }
}