$(document).ready(function () {
    bindMasterData();
})

function bindMasterData() {
    debugger;
    try {
        $.ajax({
            type: "POST",
            url: "frmDashBoardContent.aspx/Main",
            data: '{}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (msg) {
                var json = JSON.parse(msg.d);
                $.each(json, function (index, obj) {
                  
                    $('.pendingGateEntryCount').html(obj.PENING_GATE_ENTRYCOUNT);
                    $('.GateEntryCount').html(obj.GATE_ENTRYCOUNT);
                });
            }
        });
    }
    catch (ex) {
        alert(ex);
    }
}