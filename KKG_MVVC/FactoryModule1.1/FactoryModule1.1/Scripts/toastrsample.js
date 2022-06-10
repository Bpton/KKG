
$(function(){
$('#showtoast').click(function () {
        toastr.options = {
        "debug": false,
            "positionClass": $("#toastrPositionGroup toast-bottom-full-width").val(),
        "onclick": null,
        "fadeIn": 300,
        "fadeOut": 100,
        "timeOut": 3000,
        "extendedTimeOut": 1000
        }
        
        var d= Date();
    toastr[$("#toastrTypeGroup success").val()](d,"Current Day & Time");  
});
});