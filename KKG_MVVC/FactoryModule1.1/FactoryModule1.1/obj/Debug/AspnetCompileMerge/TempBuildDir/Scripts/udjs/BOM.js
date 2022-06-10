//#region Developer Info
/*
* For BOM.cshtml Only
* Developer Name : Avishek Ghosh 
* Start Date     : 18/04/2020
* END Date       : 
 
*/
//#endregion
var LoaderCount = 0;
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
    bindframeworkgrid();
    bindbomProduct();
    bindprocessmastergrid();

    $("#btnAddnew").click(function (e) {
        $('#dvAdd').css("display", "");
        $('#dvDisplay').css("display", "none");
        $('#btnsearch').css("display", "none");
        $('#btnsave').css("display", "");
        
        $($.fn.dataTable.tables(true)).DataTable().columns.adjust();
            
        ClearControls();

        $('#SourceMaterialOutputGrid').empty();
        $('#SourceMaterialInputGrid').empty();
        
        e.preventDefault();
        
    })

    $("#btnclose").click(function (e) {
        $('#dvAdd').css("display", "none");
        $('#dvDisplay').css("display", "");
        $('#btnsearch').css("display", "none");
        $($.fn.dataTable.tables(true)).DataTable().columns.adjust();
        ClearControls();
        bindframeworkgrid();
        e.preventDefault();
    })

    $("#btnAddProcess").click(function (e) {
        if($("#Bomcode").val() == '') {
            toastr.warning('<b><font color=black>Please enter framework code..!</font></b>');
            return false;
        }
        else if ($("#BomName").val() == '') {
            toastr.warning('<b><font color=black>Please enter framework name..!</font></b>');
            return false;
        }
        else if ($("#ProductID").val() == '0') {
            toastr.warning('<b><font color=black>Please select product..!</font></b>');
            return false;
        }
        else {
            checkProcessList();
        }
        e.preventDefault();
    })

    $("#btnsave").click(function (e) {
        debugger; 
        var ProcessrowCount = document.getElementById("ProcessDestinationGrid").rows.length - 1;
        var InputrowCount = document.getElementById("DestinationMaterialInputGrid").rows.length - 1;
        var OutputrowCount = document.getElementById("DestinationMaterialOutputGrid").rows.length - 1;
        var WorkstationrowCount = document.getElementById("DestinationWorkstationGrid").rows.length - 1;
        var ResourcerowCount = document.getElementById("DestinationResourceGrid").rows.length - 1;
        var QcrowCount = document.getElementById("DestinationQCGrid").rows.length - 1;

        if ($("#Bomcode").val() == '') {
            toastr.warning('<b><font color=black>Please enter framework code..!</font></b>');
            return false;
        }
        else if ($("#BomName").val() == '') {
            toastr.warning('<b><font color=black>Please enter framework name..!</font></b>');
            return false;
        }
        else if ($("#ProductID").val() == '0') {
            toastr.warning('<b><font color=black>Please select product..!</font></b>');
            return false;
        }
        else if (ProcessrowCount <= 0) {
            toastr.warning('<b><font color=black>Please add process..!</font></b>');
            return false;
        }
        else if (InputrowCount <= 0) {
            toastr.warning('<b><font color=black>Please add input material(s)..!</font></b>');
            return false;
        }
        else if (OutputrowCount <= 0) {
            toastr.warning('<b><font color=black>Please add output material..!</font></b>');
            return false;
        }
        else {
            SaveBom();
        }
        e.preventDefault();
    })
    
    $("#dvProcessTab").dialog({
        autoOpen: false,
        maxWidth: 1300,
        maxHeight: 580,
        width: 1300,
        height: 580,
        resizable: false,
        draggable: false,
        modal: true,
        closeOnEscape: false,
        open: function (event, ui) { $(".ui-dialog-titlebar-close").hide(); },
        show: {
            effect: "blind",
            duration: 700
        },
        hide: {
            effect: "explode",
            duration: 500
        }
    });

    $("#btncloseProcessTab").click(function (e) {
        $('#TabProcessname').val('');
        $("#txtInputMaterialSearch").val('');
        $("#txtOutputMaterialSearch").val('');
        $("#txtResourceSearch").val('');
        $("#SourceMaterialInputGrid").empty();
        $("#SourceMaterialOutputGrid").empty();
        $("#SourceWorkstationGrid").empty();
        $("#SourceResourceGrid").empty();
        $("#SourceQCGrid").empty();
        $("#dvProcessTab").dialog("close");
        e.preventDefault();
    })

    $('#dvProcessTab').on('dialogclose', function (event) {
        $('#TabProcessname').val('');
        $("#txtInputMaterialSearch").val('');
        $("#txtOutputMaterialSearch").val('');
        $("#txtResourceSearch").val('');
        $("#SourceMaterialInputGrid").empty();
        $("#SourceMaterialOutputGrid").empty();
        $("#SourceWorkstationGrid").empty();
        $("#SourceResourceGrid").empty();
        $("#SourceQCGrid").empty();
    });

    $("#txtInputMaterialSearch").keyup(function (e) {
        //var value = $(this).val().toLowerCase();
        //$("#SourceMaterialInputGrid tbody tr").filter(function () {
        //    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        //});
        searchInputMaterial();
        e.preventDefault();
    });

    $("#txtOutputMaterialSearch").keyup(function (e) {
        //var value = $(this).val().toLowerCase();
        //$("#SourceMaterialOutputGrid tbody tr").filter(function () {
        //    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        //});
        searchOutputMaterial();
        e.preventDefault();
    });

    $("#txtResourceSearch").keyup(function (e) {
        var value = $(this).val().toLowerCase();
        $("#SourceResourceGrid tbody tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
        e.preventDefault();
    });

    $("#btnAddInputMaterial").click(function (e) {

        if ($('#SourceMaterialInputGrid').length) {
            var flag = false;
            var count = 0;
            var inputMaterialid = '';
            var inputMaterialcode = '';
            var inputMaterialname = '';
            var inputMaterialtype = '';
            var inputMaterialuomid = '';
            var inputMaterialuomname = '';
            var inputMaterialqty= '';
            var inputMaterialrefqty = '';
            $('#SourceMaterialInputGrid tbody tr').each(function () {
                if ($(this).find('#txtinputqty').val() != '' && $(this).find('#txtinputrefqty').val() != '') {
                    debugger;
                    flag = true;
                    count = count + 1;
                    inputMaterialid = $(this).find('td:eq(0)').html().trim();
                    inputMaterialcode = $(this).find('td:eq(1)').html().trim();
                    inputMaterialname = $(this).find('td:eq(2)').html().trim();
                    inputMaterialtype = $(this).find('td:eq(3)').html().trim();
                    inputMaterialuomname = $(this).find('td:eq(4)').html().trim();
                    inputMaterialuomid = $(this).find('td:eq(5)').html().trim();
                    inputMaterialqty = $(this).find('#txtinputqty').val().trim();
                    inputMaterialrefqty = $(this).find('#txtinputrefqty').val().trim();
                    
                    IsExistsInputMaterial(inputMaterialid, inputMaterialcode, inputMaterialname, inputMaterialtype,
                        inputMaterialuomid, inputMaterialuomname, inputMaterialqty, inputMaterialrefqty);

                    $(this).find('#txtinputqty').val('');
                    $(this).find('#txtinputrefqty').val('');
                    $(this).find('#ddlinputuom').val('0');                   
                }
            });
            if (flag == false) {
                inputMaterialid = '';
                inputMaterialcode = '';
                inputMaterialname = '';
                inputMaterialtype = '';
                inputMaterialuomid = '';
                inputMaterialuomname = '';
                inputMaterialqty = '';
                inputMaterialrefqty = '';
                toastr.error('Please select Unit & enter Qty,Ref.Qty...!');
                return false;
            }
        }
        e.preventDefault();
    })

    $("#btnAddOutputMaterial").click(function (e) {

        if ($('#SourceMaterialOutputGrid').length) {
            var flag = false;
            var count = 0;
            var outputMaterialid = '';
            var outputMaterialcode = '';
            var outputMaterialname = '';
            var outputMaterialtype = '';
            var outputMaterialuomid = '';
            var outputMaterialuomname = '';
            var outputMaterialqty = '';
            var outputMaterialrefqty = '';
            $('#SourceMaterialOutputGrid tbody tr').each(function () {
                if ($(this).find('#txtoutputqty').val() != '' && $(this).find('#txtoutputrefqty').val() != '') {
                    flag = true;
                    count = count + 1;
                    outputMaterialid = $(this).find('td:eq(0)').html().trim();
                    outputMaterialcode = $(this).find('td:eq(1)').html().trim();
                    outputMaterialname = $(this).find('td:eq(2)').html().trim();
                    outputMaterialtype = $(this).find('td:eq(3)').html().trim();
                    outputMaterialuomname = $(this).find('td:eq(4)').html().trim();
                    outputMaterialuomid = $(this).find('td:eq(5)').html().trim();
                    outputMaterialqty = $(this).find('#txtoutputqty').val().trim();
                    outputMaterialrefqty = $(this).find('#txtoutputrefqty').val().trim();

                    $(this).find('#txtoutputqty').val('');
                    $(this).find('#txtoutputrefqty').val('');
                    $(this).find('#ddloutputuom').val('0');
                }
            });
            if (flag == false) {
                outputMaterialid = '';
                outputMaterialcode = '';
                outputMaterialname = '';
                outputMaterialtype = '';
                outputMaterialuomid = '';
                outputMaterialuomname = '';
                outputMaterialqty = '';
                outputMaterialrefqty = '';
                toastr.error('Please select Unit & enter Qty,Ref.Qty...!');
                return false;
            }
            else {

                if (count > 1) {
                    outputMaterialid = '';
                    outputMaterialcode = '';
                    outputMaterialname = '';
                    outputMaterialtype = '';
                    outputMaterialuomid = '';
                    outputMaterialuomname = '';
                    outputMaterialqty = '';
                    outputMaterialrefqty = '';
                    toastr.error('You can not enter multiple output material...!');
                    return false;
                }
                else if (count == 1 && outputMaterialid != '') {
                    IsExistsOutputMaterial(outputMaterialid, outputMaterialcode, outputMaterialname, outputMaterialtype,
                        outputMaterialuomid, outputMaterialuomname, outputMaterialqty, outputMaterialrefqty);
                }
            }
        }
        e.preventDefault();
    })

    $("#btnAddWorkstation").click(function (e) {

        if ($('#SourceWorkstationGrid').length) {
            var flag = false;
            var count = 0;
            var workstationid = '';
            var workstationcode = '';
            var workstationname = '';
            var workstationduration = '';
            $('#SourceWorkstationGrid tbody tr').each(function () {
                if ($(this).find('#txtWorkstationSource').val() != '') {
                    flag = true;
                    count = count + 1;
                    workstationid = $(this).find('td:eq(0)').html().trim();
                    workstationcode = $(this).find('td:eq(1)').html().trim();
                    workstationname = $(this).find('td:eq(2)').html().trim();
                    workstationduration = $(this).find('#txtWorkstationSource').val().trim();
                    
                    $(this).find('#txtWorkstationSource').val('');
                    
                }
            });
            if (flag == false) {
                workstationid = '';
                workstationcode = '';
                workstationname = '';
                workstationduration = '';
                toastr.error('Please enter Duration(Hrs)...!');
                return false;
            }
            else {

                if (count > 1) {
                    workstationid = '';
                    workstationcode = '';
                    workstationname = '';
                    workstationduration = '';
                    toastr.error('You can not enter multiple workstation...!');
                    return false;
                }
                else if (count == 1 && workstationid != '') {
                    IsExistsWorkstation(workstationid, workstationcode, workstationname, workstationduration);
                }
            }
        }
        e.preventDefault();
    })

    $("#btnAddResource").click(function (e) {

        if ($('#SourceResourceGrid').length) {
            var flag = false;
            var count = 0;
            var resourceid = '';
            var resourcename = '';
            var resourceno = '';
            $('#SourceResourceGrid tbody tr').each(function () {
                if ($(this).find('#txtResourceSource').val() != '') {
                    flag = true;
                    count = count + 1;
                    resourceid = $(this).find('td:eq(0)').html().trim();
                    resourcename = $(this).find('td:eq(1)').html().trim();
                    resourceno = $(this).find('#txtResourceSource').val().trim();

                    $(this).find('#txtResourceSource').val('');
                }
            });
            if (flag == false) {
                resourceid = '';
                resourcename = '';
                resourceno = '';
                toastr.error('Please enter no. of resource...!');
                return false;
            }
            else {

                if (count > 1) {
                    resourceid = '';
                    resourcename = '';
                    resourceno = '';
                    toastr.error('You can not enter multiple resource...!');
                    return false;
                }
                else if (count == 1 && resourceid != '') {
                    IsExistsResource(resourceid, resourcename, resourceno);
                }
            }
        }
        e.preventDefault();
    })

    $("#btnAddQC").click(function (e) {

        if ($('#SourceQCGrid').length) {
            var flag = false;
            var count = 0;
            var qcid = '';
            var qcname = '';
            $('#SourceQCGrid tbody tr').each(function () {
                if ($(this).find('#chkQCSource').prop('checked') == true) {
                    flag = true;
                    count = count + 1;
                    qcid = $(this).find('td:eq(0)').html().trim();
                    qcname = $(this).find('td:eq(1)').html().trim();

                    $(this).find('#chkQCSource').prop('checked') == false;
                }
            });
            if (flag == false) {
                qcid = '';
                qcname = '';
                toastr.error('Please select qc...!');
                return false;
            }
            else {

                if (count > 1) {
                    qcid = '';
                    qcname = '';
                    toastr.error('You can not enter multiple qc...!');
                    return false;
                }
                else if (count == 1 && qcid != '') {
                    IsExistsQc(qcid, qcname);
                }
            }
        }
        e.preventDefault();
    })

    /*new add for kkg*/
    /*product bind from type change*/
    $('#ddlInputType').change(function () {
        try {
            var type = $('#ddlInputType').find('option:selected').text();
            LoadBomProducts(type);
        }
        catch (exception) {
            toastr.error(exception);
        }

    })

     /*product bind from type change*/
    $('#ddlOutputType').change(function () {
        try {
        var type = $("#ddlOutputType").find('option:selected').text();
            LoadFgSfgProducts(type);
        }
        catch (exception) {
            toastr.error(exception);
        }
    })

    /*name code and descreption auto fill from product selectIndexChange*/
    $('#ProductID').change(function () {
        try {
           
           var code = $("#Bomcode").val();
           if (code == "") {
               code = "INHOUSE";
           }
            var productName = $("#ProductID").find('option:selected').text();
            var productId = $("#ProductID").val();
          
            $('#BomName').val(productName);
            $('#BomDesc').val(productName+'/'+code);
            $('#Bomcode').val(code);
            $('#txtOutputMaterialSearch').val(productName);
           
            LoadFgSfgProductsById(productId);
            packsizeMappedOrNot(productId);
           
        }
        catch (exception) {
        toastr.error(exception);
        }
    })

    /*textbox click event for all input type referqty*/
    $('#txtInputReferQty').change(function () {
        try {
            var inputReferQty = $('#txtInputReferQty').val();
            $('#SourceMaterialInputGrid tbody tr').each(function () {
                $(this).find('#txtinputrefqty').val(inputReferQty);
            })
            $('#txtInputReferQty').val('0');
        }
        catch (exception) {
            toastr.error(exception);
        }
    })
    /*textbox click event for all output type referqty*/
    $('#txtOutputReferQty').change(function () {
        try {
            var outputRefQty = $('#txtOutputReferQty').val();
            $('#SourceMaterialOutputGrid tbody tr').each(function () {
                $(this).find('#txtoutputrefqty').val(outputRefQty);
            })
            $('#txtOutputReferQty').val('0');
        }
        catch (exception) {
            toastr.error(exception);
        }
    })

    /*Bomname chnage event*/
    $('#BomName').change(function () {
        var productName = $('#BomName').val();
        checkBomNameAlreadyExistsOrNot(productName);
    })

    /*process change event*/
    $("#Processid").change(function () {
        var rowCount = document.getElementById("ProcessDestinationGrid").rows.length;
        if (rowCount == 0) {
            var Processid = $("#Processid").val();
            var ProcessName = $("#Processid").find('option:selected').text();
            var ProcessCode = ProcessName.split(',')[1];
            if ($("#Bomcode").val() == '') {
                toastr.warning('<b><font color=black>Please enter framework code..!</font></b>');
                return false;
            }
            else if ($("#BomName").val() == '') {
                toastr.warning('<b><font color=black>Please enter framework name..!</font></b>');
                return false;
            }
            else if ($("#ProductID").val() == '0') {
                toastr.warning('<b><font color=black>Please select product..!</font></b>');
                return false;
            }
            else {
                //checkProcessList();
                debugger;
                addProcessDestinationGrid(Processid, ProcessCode, ProcessName);
                openPopUp(Processid, ProcessName);
            }
        }
        else {
            toastr.warning("Cannot select multiple process!!!");
        }
       
       // e.preventDefault();
       
    })

    /*input qty chnage*/
    $('#txtBomQty').change(function () {
        debugger;
        if ($('#hdnBomID').val() != '0') {
            var productName = $("#ProductID").find('option:selected').text();
            var productId = $("#ProductID").val();
            $('#txtOutputMaterialSearch').val(productName);
            LoadFgSfgProductsById(productId);
            $('#txtRequiredQty').val(0);
            $('#DestinationMaterialOutputGrid').empty();
        }
        else {
            $('#txtRequiredQty').val(0);
            $('#DestinationMaterialOutputGrid').empty();
        }
           

    })

    /*output qty change event*/
    $('#txtRequiredQty').change(function () {
       
            if ($("#txtBomQty").val() == '0') {
                toastr.warning("Please enter bom qty first");
                return;
            }
            else {
              

                $('#DestinationMaterialOutputGrid').empty();
                var outputMaterialid = '';
                var outputMaterialcode = '';
                var outputMaterialname = '';
                var outputMaterialtype = '';
                var outputMaterialuomname = '';
                var outputMaterialuomid = '';
                var outputMaterialqty = 0;
                var outputMaterialrefqty = 0;

                outputMaterialrefqty = $("#txtBomQty").val();
                outputMaterialqty = $("#txtRequiredQty").val();
                
                debugger;
                $('#SourceMaterialOutputGrid tbody tr').each(function () {
                    debugger;
                    outputMaterialid = $(this).find('td:eq(0)').html().trim();
                    outputMaterialcode = $(this).find('td:eq(1)').html().trim();
                    outputMaterialname = $(this).find('td:eq(2)').html().trim();
                    outputMaterialtype = $(this).find('td:eq(3)').html().trim();
                    outputMaterialuomname = $(this).find('td:eq(4)').html().trim();
                    outputMaterialuomid = $(this).find('td:eq(5)').html().trim();

                    $(this).find('#txtoutputqty').val('');
                    $(this).find('#txtoutputrefqty').val('');
                    $(this).find('#ddloutputuom').val('0');

                });

                IsExistsOutputMaterial(outputMaterialid, outputMaterialcode, outputMaterialname, outputMaterialtype, outputMaterialuomid, outputMaterialuomname, outputMaterialqty, outputMaterialrefqty);
                bindbomforsimilar();
            }
        
        
       
      

        
    })

    $('#Bomid').change(function () {
        var processframewirkid = $('#Bomid').val();
        fetchBomDetailsforsimilar(processframewirkid);

    })
       
});



/*function for only qty input*/
function isNumberKeyWithDot(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if ((charCode < 48 || charCode > 57) && (charCode != 8) && (charCode != 46))
        return false;
    return true;
}

function ClearControls() {
   // $("#dvAdd").find("input,submit").removeAttr("disabled");
    $("#Bomcode").val('');
    $("#BomName").val('');
    $("#BomDesc").val('');
    $("#chkactive").prop("checked", true);
    $('#hdnBomID').val('0');
    $('#hdnProcessID').val('0');
    $('#ProcessDestinationGrid').empty();
    $("#ProductID").val('0');
    $("#ProductID").chosen({
        search_contains: true
    });
    $("#ProductID").trigger("chosen:updated");

    $("#Processid").val('0');
    $("#Processid").chosen({
        search_contains: true
    });
    $("#Processid").trigger("chosen:updated");

    debugger;
    if ($('#ProcessMasterGrid').length) {
        $('#ProcessMasterGrid').each(function () {
            $('#ProcessMasterGrid tbody tr').each(function () {
                if ($(this).find('#chkProcess').prop('checked') == true) {
                    $(this).find('#chkProcess').prop("checked", false);
                }
            });
        })
    }
   
    $('#DestinationMaterialInputGrid').empty();
    $('#DestinationMaterialOutputGrid').empty();
    $('#DestinationWorkstationGrid').empty();
    $('#DestinationResourceGrid').empty();
    $('#DestinationQCGrid').empty();
    $("#txtOutputMaterialSearch").val('');
    $("#txtBomQty").val('0');
    $("#txtRequiredQty").val('0');


    $("#Bomid").val('0');
    $("#Bomid").chosen({
        search_contains: true
    });
    $("#Bomid").trigger("chosen:updated");

}

function SaveBom() {
    debugger;
    var i = 0;
    var j = 0;
    var k = 0;
    var l = 0;
    var m = 0;
    var n = 0;
    var o = 0;

    sequencelist = new Array();
    inputmateriallist = new Array();
    outputmateriallist = new Array();
    fgmateriallist = new Array();
    workstationlist = new Array();
    resourcelist = new Array();
    qclist = new Array();
    var bomsave = {};
    debugger;


    bomsave.Bomid = $('#hdnBomID').val().trim();
    if ($('#hdnBomID').val() == '0') {
        bomsave.FLAG = 'I';
    }
    else {
        bomsave.FLAG = 'U';
    }
    bomsave.Processid = $("#hdnProcessID").val().trim();
    bomsave.BomName = $("#BomName").val().trim();
    bomsave.Bomcode = $("#Bomcode").val().trim();
    bomsave.BomDesc = $("#BomDesc").val().trim();
    bomsave.ProductID = $("#ProductID").val().trim();   

    /*Process Details*/
    $('#ProcessDestinationGrid tbody tr').each(function () {
        debugger
        var sequencedetails = {};
        var processid = $(this).find('td:eq(0)').html().trim();
        var totaldurationinhour = $(this).find('#txtduration').val().trim();
        sequencedetails.ProcessID = processid;
        sequencedetails.TotalDurationInHour = totaldurationinhour;
        
        sequencelist[i++] = sequencedetails;
    });
    bomsave.SequenceTypeList = sequencelist;



    /*Input Material Details*/
    $('#DestinationMaterialInputGrid tbody tr').each(function () {
        debugger
        var inputmaterialdetails = {};
        var processid = $("#hdnProcessID").val().trim();
        var itemid = $(this).find('td:eq(0)').html().trim();
        var qty = $(this).find('td:eq(6)').html().trim();
        var unit = $(this).find('td:eq(4)').html().trim();
        var refqty = $(this).find('td:eq(7)').html().trim();
        inputmaterialdetails.ProcessID = processid;
        inputmaterialdetails.ItemID = itemid;
        inputmaterialdetails.Qty = qty;
        inputmaterialdetails.Unit = unit;
        inputmaterialdetails.REFQty = refqty;

        inputmateriallist[j++] = inputmaterialdetails;
    });
    bomsave.InputMaterialTypeList = inputmateriallist;



    /*Output Material Details*/
    $('#DestinationMaterialOutputGrid tbody tr').each(function () {
        debugger
        var outputmaterialdetails = {};
        var processid = $("#hdnProcessID").val().trim();
        var itemid = $(this).find('td:eq(0)').html().trim();
        var qty = $(this).find('td:eq(6)').html().trim();
        var unit = $(this).find('td:eq(4)').html().trim();
        var refqty = $(this).find('td:eq(7)').html().trim();
        outputmaterialdetails.ProcessID = processid;
        outputmaterialdetails.ItemID = itemid;
        outputmaterialdetails.Qty = qty;
        outputmaterialdetails.Unit = unit;
        outputmaterialdetails.REFQty = refqty;

        outputmateriallist[k++] = outputmaterialdetails;
    });
    bomsave.OutputMaterialTypeList = outputmateriallist;



    /*FG Material Details*/
    debugger
    var fgmaterialdetails = {};
    var processid = '11';
    var itemid = $("#ProductID").val().trim();
    var qty = '1';
    var unit = 'B9F29D12-DE94-40F1-A668-C79BF1BF4425';
    var refqty = '1';
    fgmaterialdetails.ProcessID = processid;
    fgmaterialdetails.ItemID = itemid;
    fgmaterialdetails.Qty = qty;
    fgmaterialdetails.Unit = unit;
    fgmaterialdetails.REFQty = refqty;
    fgmateriallist[l++] = fgmaterialdetails;
    bomsave.FGMaterialTypeList = fgmateriallist;



    /*Workstation Details*/
    $('#DestinationWorkstationGrid tbody tr').each(function () {
        debugger
        var workstationdetails = {};
        var processid = $("#hdnProcessID").val().trim();
        var workstationid = $(this).find('td:eq(0)').html().trim();
        var durationinhour = $(this).find('td:eq(3)').html().trim();
        workstationdetails.ProcessID = processid;
        workstationdetails.WorkstationID = workstationid;
        workstationdetails.DurationInHour = durationinhour;

        workstationlist[m++] = workstationdetails;
    });
    bomsave.WorkstationTypeList = workstationlist;



    /*Resource Details*/
    $('#DestinationResourceGrid tbody tr').each(function () {
        debugger
        var resourcedetails = {};
        var processid = $("#hdnProcessID").val().trim();
        var resourceid = $(this).find('td:eq(0)').html().trim();
        var noofresourse = $(this).find('td:eq(2)').html().trim();
        resourcedetails.ProcessID = processid;
        resourcedetails.ResourceID = resourceid;
        resourcedetails.NoOfResource = noofresourse;

        resourcelist[n++] = resourcedetails;
    });
    bomsave.ResourceTypeList = resourcelist;


    /*QC Details*/
    $('#DestinationQCGrid tbody tr').each(function () {
        debugger
        var qcdetails = {};
        var processid = $("#hdnProcessID").val().trim();
        var qcid = $(this).find('td:eq(0)').html().trim();
        qcdetails.ProcessID = processid;
        qcdetails.QCID = qcid;

        qclist[o++] = qcdetails;
    });
    bomsave.QCTypeList = qclist;


    //alert(JSON.stringify(invoicesave));

    $.ajax({
        url: "/Process/BOMsavedata",
        data: '{bomsave:' + JSON.stringify(bomsave) + '}',
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
                bindframeworkgrid();
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

function bindbomProduct() {
    var Product = $("#ProductID");
    $.ajax({
        type: "POST",
        url: "/Process/GetBomProduct",
        data: '{}',
        async: false,
        dataType: "json",
        success: function (response) {
            //alert(JSON.stringify(response));
            Product.empty().append('<option selected="selected" value="0">Please select</option>');;
            $.each(response, function () {
                Product.append($("<option></option>").val(this['PRODUCTID']).html(this['PRODUCTNAME']));
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

function bindprocessmastergrid() {
    debugger;
    //var ProcessID = '';
    //var srl = 0;
    //srl = srl + 1;
    //$.ajax({
    //    type: "POST",
    //    url: "/Process/BindProcessMasterGrid",
    //    data: { processid: ProcessID },
    //    dataType: "json",
    //    async: false,
    //    success: function (response) {
    //        debugger;
    //        var tr;
    //        var HeaderCount = 0;
    //        $("#ProcessMasterGrid").empty();
    //        if (response.length > 0) {
    //            tr = $('#ProcessMasterGrid');
    //            HeaderCount = $('#ProcessMasterGrid thead th').length;
    //            if (HeaderCount == 0) {
    //                tr.append("<thead><tr><th style='display: none'>Sl. No.</th><th>Select</th><th style='display: none'>Process ID</th><th>Process Code</th><th>Process Name</th></tr></thead><tbody>");
    //            }
               
    //            for (var i = 0; i < response.length; i++) {
    //                tr = $('<tr/>');
    //                tr.append("<td style='display: none; text-align: center'>" + srl + "</td>");//0
    //                tr.append("<td style='text-align: center'><input type='checkbox' class='gvChekbox'  id='chkProcess'></input></td>");//1
    //                tr.append("<td style='display: none'>" + response[i].ProcessID + "</td>");//2
    //                tr.append("<td>" + response[i].ProcessCode + "</td>");//3
    //                tr.append("<td>" + response[i].ProcessName + "</td>");//4
    //                $("#ProcessMasterGrid").append(tr);
    //            }
    //            tr.append("</tbody>");
    //            RowCountProcessList();
    //        }
            
    //    },
    //    failure: function (response) {
    //        alert(response.responseText);
    //    },
    //    error: function (response) {
    //        alert(response.responseText);
    //    }
    //});


    var Processid = $("#Processid");
    var ID = '';
    $.ajax({
        type: "POST",
        url: "/Process/BindProcessMasterGrid",
        data: { processid: ID },
        async: false,
        dataType: "json",
        success: function (response) {
            //alert(JSON.stringify(response));
            Processid.empty().append('<option selected="selected" value="0">Please select</option>');;
            $.each(response, function () {
                Processid.append($("<option></option>").val(this['ProcessID']).html(this['ProcessName']));
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

//function bindprocessmasterDataTable() {
//    debugger;
//    var ProcessID = '';
//    var srl = 0;
//    srl = srl + 1;
    
//    $.ajax({
//        type: "POST",
//        url: "/Process/BindProcessMasterGrid",
//        data: { processid: ProcessID },
//        dataType: "json",
//        async: false,
//        success: function (response) {
//            debugger;
//            var tr;
//            tr = $('#ProcessMasterGrid');
//            tr.append("<thead><tr><th style='display: none'>Sl. No.</th><th>Select</th><th style='display: none'>Process ID</th><th>Process Code</th><th>Process Name</th></thead>");

//            $('#ProcessMasterGrid').DataTable().destroy();
//            $("#ProcessMasterGrid tbody tr").remove();
//            for (var i = 0; i < response.length; i++) {
//                tr = $('<tr/>');
//                tr.append("<td style='text-align: center; display: none;'>" + srl + "</td>");//0
//                tr.append("<td style='text-align: center'><input type='checkbox' class='gvChekbox'  id='chkProcess'></input></td>");//1
//                tr.append("<td style='display: none'>" + response[i].ProcessID + "</td>");//2
//                tr.append("<td>" + response[i].ProcessCode + "</td>");//3
//                tr.append("<td>" + response[i].ProcessName + "</td>");//4

//                $("#ProcessMasterGrid").append(tr);
//            }
//            tr.append("</tbody>");
//            RowCountProcessList();
//            $('#ProcessMasterGrid').DataTable({
//                "sScrollX": '100%',
//                "sScrollXInner": "110%",
//                "scrollY": "400px",
//                "scrollCollapse": true,
//                "initComplete": function (settings, json) {
//                    $('.dataTables_scrollBody thead tr').css({ visibility: 'collapse' });// this gets rid of duplicate headers
//                },
//                "scrollXInner": true,
//                "bRetrieve": false,
//                "bFilter": true,
//                "bSortClasses": false,
//                "bLengthChange": false,
//                "bInfo": false,
//                "bAutoWidth": false,
//                "paging": false,
//                "bSort": false,
//                "columnDefs": [
//                    { "orderable": false, "targets": 0 }
//                ],
//                "order": [],  // not set any order rule for any column.
//                "ordering": false
//            });
            
           
            
//        },
//        failure: function (response) {
//            alert(response.responseText);
//        },
//        error: function (response) {
//            alert(response.responseText);
//        }
//    });
//}

function bindframeworkgrid(){
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
        url: "/Process/BindFrameworkMasterGrid",
        data: '{}',
        dataType: "json",
        success: function (response) {
            debugger;
            var tr;
            tr = $('#FrameworkGrid');
            tr.append("<thead><tr><th>Sl. No.</th><th style='display: none'>Framework ID</th><th>Process Framework Code</th><th>Process Framework Name</th><th>Description</th><th>Active</th><th>Edit</th><th>Delete</th>");

            $('#FrameworkGrid').DataTable().destroy();
            $("#FrameworkGrid tbody tr").remove();
            for (var i = 0; i < response.length; i++) {
                tr = $('<tr/>');
                tr.append("<td style='text-align: center'>" + srl + "</td>");//0
                tr.append("<td style='display: none'>" + response[i].ProcessFrameworkID + "</td>");//1
                tr.append("<td>" + response[i].ProcessFrameworkCode + "</td>");//2
                tr.append("<td>" + response[i].ProcessFrameworkName + "</td>");//3
                tr.append("<td>" + response[i].ProcessFrameworkDescription + "</td>");//4
                if (response[i].Active.trim() == 'Active') {
                    tr.append("<td style='text-align: center'><font color='0x00B300'>" + response[i].Active + "</font></td>");//5
                }
                else {
                    tr.append("<td style='text-align: center'><font color='#ff2500'>" + response[i].Active + "</font></td>");//5
                }
                tr.append("<td style='text-align: center'><input type='image' class='gvEdit'   id='btnprocessedit'   <img src='../Images/Pencil-icon.png' title='Edit'/></input></td>");                
                tr.append("<td style='text-align: center'><input type='image' class='gvDelete' id='btnprocessdelete' <img src='../Images/ico_delete_16.png' title='Delete'/></input></td>");

                $("#FrameworkGrid").append(tr);
            }
            tr.append("</tbody>");
            RowCountFrameworkList();
            $('#FrameworkGrid').DataTable({
                "sScrollX": '100%',
                "sScrollXInner": "110%",
                "scrollY": "300px",
                "scrollCollapse": true,
                "dom": 'Bfrtip',
                buttons: [
                    {
                        extend: 'excelHtml5',
                        title: 'Process Framework List'
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

function RowCountFrameworkList() {
    var table = document.getElementById("FrameworkGrid");
    var rowCount = document.getElementById("FrameworkGrid").rows.length - 1;
    if (rowCount > 0) {
        for (var i = 1; i <= rowCount; i++) {
            table.rows[i].cells[0].innerHTML = i.toString();
        }
    }
}

function RowCountProcessList() {
    var table = document.getElementById("ProcessMasterGrid");
    var rowCount = document.getElementById("ProcessMasterGrid").rows.length - 1;
    if (rowCount > 0) {
        for (var i = 1; i <= rowCount; i++) {
            table.rows[i].cells[0].innerHTML = i.toString();
        }
    }
}

function EditDetails(frameworkID) {
    debugger;
   
    var tr;
    var HeaderCount = 0;
    var FooterCount = 0;
    var qty = 0;
    var Refqty = 0;
    var qty2 = 0;
    var Refqty2 = 0;
    $("#dialog").dialog({
        autoOpen: true,
        modal: true,
        title: "Loading.."
    });
    $("#imgLoader").css("visibility", "visible");

    $.ajax({
        type: "POST",
        url: "/Process/EditProcessFramework",
        data: { FrameworkID: frameworkID },
        dataType: "json",
        async: false,
        success: function (response) {

            debugger;
            var listEditHeader = response.allEditDataset.varEditHeader;
            var listEditSequence = response.allEditDataset.varEditSequence;
            var listEditInputMaterial = response.allEditDataset.varEditInputMaterial;
            var listEditOutputMaterial = response.allEditDataset.varEditOutputMaterial;
            var listEditFgMaterial = response.allEditDataset.varEditFgMaterial;
            var listEditWorkstation = response.allEditDataset.varEditWorkstation;
            var listEditResource = response.allEditDataset.varEditResource;
            var listEditQC = response.allEditDataset.varEditQC;
            var listEditProcess = response.allEditDataset.varEditProcess;


            /*Framework Header Start*/
            $.each(listEditHeader, function (index, record) {
                //debugger;
                $("#Bomcode").val(this['FrameworkCode'].toString().trim());
                $("#BomName").val(this['FrameworkName'].toString().trim());
                $("#BomDesc").val(this['FrameworkDesc'].toString().trim());
                $("#ProductID").val(this['ProductID'].toString().trim());
            });
            /*Framework Header End*/


            /*Framework Sequence Start*/
            if (listEditSequence.length > 0) {
                $("#ProcessDestinationGrid").empty();
                tr = $('#ProcessDestinationGrid');
                HeaderCount = $('#ProcessDestinationGrid thead th').length;
                if (HeaderCount == 0) {
                    tr.append("<thead><tr><th style='display: none'>Process ID</th><th>Process Code</th><th>Process Name</th><th style='display: none'>Total Duration(Hours)</th><th style='display: none'>BOMDETAILS</th><th>Delete</th></tr></thead><tbody>");
                }
                $.each(listEditSequence, function (index, record) {
                    tr = $('<tr/>');
                    tr.append("<td style='display: none'>" + this['ProcessID'].toString().trim() + "</td>");//0
                    tr.append("<td>" + this['ProcessCode'].toString().trim() + "</td>");//1
                    tr.append("<td>" + this['ProcessName'].toString().trim() + "</td>");//2
                    tr.append("<td style='display: none'><input type='text' class='gvduration'  id='txtduration' value='0' style='text-align: right; width:60px; height:20px' disabled></input></td>");//3
                    tr.append("<td style='display: none'><input type='image' class='gvDestPopup'   id='btnDestpopup'   <img src='../Images/ico_PackingList.png' width='20' height ='20' title='Open BomDetails'/></input></td>");//4
                    tr.append("<td style='text-align: center'><input type='image' class='gvDestDelete'  id='btnDestdelete' <img src='../Images/ico_delete_16.png' title='Delete'/></input></td>");//5
                    $("#ProcessDestinationGrid").append(tr);
                });
                tr.append("</tbody>");
            }
            /*Framework Sequence End*/


            /*Insput Material Destination Start*/
            if (listEditInputMaterial.length > 0) {

                $("#DestinationMaterialInputGrid").empty();
                tr = $('#DestinationMaterialInputGrid');
                HeaderCount = $('#DestinationMaterialInputGrid thead th').length;
                FooterCount = $('#DestinationMaterialInputGrid tfoot th').length;
                if (HeaderCount == 0) {
                    tr.append("<thead><tr><th style='display: none'>Product ID</th><th style='display: none'>Code</th><th>Product</th><th>Type</th><th style='display: none'>UnitID</th><th>Unit</th><th>I/P Qty.</th><th>Bom.Qty.</th><th class='word-wrap text-wrap width-45' style='text-align: center'>Del</th></tr></thead>");
                }
                if (FooterCount == 0) {
                    tr.append("<tfoot><tr><th style='display: none'></th><th style='display: none'></th><th>Total</th><th></th><th style='display: none'></th><th></th><th id='tfInputQty'></th><th id='tfInputRefQty'></th><th class='word-wrap text-wrap width-45' style='text-align: center'></th></tr></tfoot><tbody>");
                }
                $.each(listEditInputMaterial, function (index, record) {
                    tr = $('<tr/>');
                    tr.append("<td style='display: none'>" + this['ItemID'].toString().trim() + "</td>");//0
                    tr.append("<td style='display: none'>" + this['Code'].toString().trim()+ "</td>");//1
                    tr.append("<td class='word-wrap text-wrap width-180'>" + this['Product'].toString().trim() + "</td>");//2
                    tr.append("<td class='word-wrap text-wrap width-50'>" + this['ProductType'].toString().trim() + "</td>");//3
                    tr.append("<td style='display: none'>" + this['UnitID'].toString().trim() + "</td>");//4
                    tr.append("<td class='word-wrap text-wrap width-60'>" + this['UOMDESCRIPTION'].toString().trim() + "</td>");//5
                    tr.append("<td class='word-wrap text-wrap width-60'>" + this['Qty'].toString().trim() + "</td>");//6
                    tr.append("<td class='word-wrap text-wrap width-60'>" + this['RefQty'].toString().trim() + "</td>");//7
                    tr.append("<td class='word-wrap text-wrap width-45' style='text-align: center'><input type='image' class='gvDestInputMaterialDelete'  id='btnDestInputMaterialdelete' <img src='../Images/ico_delete_16.png' title='Delete'/></input></td>")//8;
                    $("#DestinationMaterialInputGrid").append(tr);
                });
                tr.append("</tbody>");
                $('#DestinationMaterialInputGrid tbody tr').each(function () {
                    qty += parseFloat($(this).find('td:eq(6)').html().trim());
                    Refqty += parseFloat($(this).find('td:eq(7)').html().trim());
                });
                $('tfoot th#tfInputQty').html(qty.toFixed(4));
                $('tfoot th#tfInputRefQty').html(Refqty.toFixed(4));
            }
            /*Insput Material Destination End*/



            /*Output Material Destination Start*/
            if (listEditOutputMaterial.length > 0) {

                $("#DestinationMaterialOutputGrid").empty();
                tr = $('#DestinationMaterialOutputGrid');
                HeaderCount = $('#DestinationMaterialOutputGrid thead th').length;
                FooterCount = $('#DestinationMaterialOutputGrid tfoot th').length;
                if (HeaderCount == 0) {
                    tr.append("<thead><tr><th style='display: none'>Product ID</th><th style='display: none'>Code</th><th>Product</th><th>Type</th><th style='display: none'>UnitID</th><th>Unit</th><th>O/P Qty.</th><th>Bom.Qty.</th><th class='word-wrap text-wrap width-45' style='text-align: center'>Del</th></tr></thead>");
                }
                if (FooterCount == 0) {
                    tr.append("<tfoot><tr><th style='display: none'></th><th style='display: none'></th><th>Total</th><th></th><th style='display: none'></th><th></th><th id='tfOutputQty'></th><th id='tfOutputRefQty'></th><th class='word-wrap text-wrap width-45' style='text-align: center'></th></tr></tfoot><tbody>");
                }
                $.each(listEditOutputMaterial, function (index, record) {
                    tr = $('<tr/>');
                    tr.append("<td style='display: none'>" + this['ItemID'].toString().trim() + "</td>");//0
                    tr.append("<td style='display: none'>" + this['Code'].toString().trim() + "</td>");//1
                    tr.append("<td class='word-wrap text-wrap width-180'>" + this['Product'].toString().trim() + "</td>");//2
                    tr.append("<td class='word-wrap text-wrap width-50'>" + this['ProductType'].toString().trim() + "</td>");//3
                    tr.append("<td style='display: none'>" + this['UnitID'].toString().trim() + "</td>");//4
                    tr.append("<td class='word-wrap text-wrap width-60'>" + this['UOMDESCRIPTION'].toString().trim() + "</td>");//5
                    tr.append("<td class='word-wrap text-wrap width-60'>" + this['Qty'].toString().trim() + "</td>");//6
                    tr.append("<td class='word-wrap text-wrap width-60'>" + this['RefQty'].toString().trim() + "</td>");//7
                    tr.append("<td class='word-wrap text-wrap width-45' style='text-align: center'><input type='image' class='gvDestOutputMaterialDelete'  id='btnDestOutputMaterialdelete' <img src='../Images/ico_delete_16.png' title='Delete'/></input></td>")//8;
                    $("#DestinationMaterialOutputGrid").append(tr);
                });
                tr.append("</tbody>");
                $('#DestinationMaterialOutputGrid tbody tr').each(function () {
                    qty2 += parseFloat($(this).find('td:eq(6)').html().trim());
                    Refqty2 += parseFloat($(this).find('td:eq(7)').html().trim());
                });
                $('tfoot th#tfOutputQty').html(qty2.toFixed(4));
                $('tfoot th#tfOutputRefQty').html(Refqty2.toFixed(4));




                $("#SourceMaterialOutputGrid").empty();
                tr = $('#SourceMaterialOutputGrid');
                HeaderCount = $('#SourceMaterialOutputGrid thead th').length;
                if (HeaderCount == 0) {
                    tr.append("<thead><tr><th style='display: none'>Product ID</th><th style='display: none'>Code</th><th>Product</th><th>Type</th><th style='display: none'>UnitID</th><th>Unit</th><th>O/P Qty.</th><th>Bom.Qty.</th><th class='word-wrap text-wrap width-45' style='display:none'>Del</th></tr></thead>");
                }
                $.each(listEditOutputMaterial, function (index, record) {
                    tr = $('<tr/>');
                    tr.append("<td style='display: none'>" + this['ItemID'].toString().trim() + "</td>");//0
                    tr.append("<td style='display: none'>" + this['Code'].toString().trim() + "</td>");//1
                    tr.append("<td class='word-wrap text-wrap width-180'>" + this['Product'].toString().trim() + "</td>");//2
                    tr.append("<td class='word-wrap text-wrap width-50'>" + this['ProductType'].toString().trim() + "</td>");//3
                    tr.append("<td style='display: none'>" + this['UnitID'].toString().trim() + "</td>");//4
                    tr.append("<td class='word-wrap text-wrap width-60'>" + this['UOMDESCRIPTION'].toString().trim() + "</td>");//5
                    tr.append("<td class='word-wrap text-wrap width-60'>" + this['Qty'].toString().trim() + "</td>");//6
                    tr.append("<td class='word-wrap text-wrap width-60'>" + this['RefQty'].toString().trim() + "</td>");//7
                    tr.append("<td class='word-wrap text-wrap width-45' style='display:none'><input type='image' class='gvDestOutputMaterialDelete'  id='btnDestOutputMaterialdelete' <img src='../Images/ico_delete_16.png' title='Delete'/></input></td>")//8;
                    $("#SourceMaterialOutputGrid").append(tr);
                });
                tr.append("</tbody>");

                

            }
            /*Output Material Destination End*/


            /*Workstation Destination Start*/
            if (listEditWorkstation.length > 0) {

                $("#DestinationWorkstationGrid").empty();
                tr = $('#DestinationWorkstationGrid');
                HeaderCount = $('#DestinationWorkstationGrid thead th').length;
                if (HeaderCount == 0) {
                    tr.append("<thead><tr><th style='display: none'>Workstation ID</th><th>Code</th><th>Workstation</th><th style='text-align: center'>Duration(Hrs.)</th><th style='text-align: center'>Delete</th></tr></thead><tbody>");
                }
                $.each(listEditWorkstation, function (index, record) {
                    tr = $('<tr/>');
                    tr.append("<td style='display: none'>" + this['WorkstationID'].toString().trim() + "</td>");//0
                    tr.append("<td>" + this['WorkStationCode'].toString().trim() + "</td>");//1
                    tr.append("<td class='word-wrap text-wrap width-180'>" + this['WorkStationName'].toString().trim() + "</td>");//2
                    tr.append("<td class='word-wrap text-wrap width-60' style='text-align: center'>" + this['DurationInHour'].toString().trim() + "</td>");//3
                    tr.append("<td style='text-align: center'><input type='image' class='gvDestWorkstationDelete'  id='btnDestWorkstationdelete' <img src='../Images/ico_delete_16.png' title='Delete'/></input></td>")//4;
                    $("#DestinationWorkstationGrid").append(tr);
                });
                tr.append("</tbody>");
            }
            /*Workstation Destination End*/


            /*Resource Destination Start*/
            if (listEditResource.length > 0) {
                $("#DestinationResourceGrid").empty();
                tr = $('#DestinationResourceGrid');
                HeaderCount = $('#DestinationResourceGrid thead th').length;
                if (HeaderCount == 0) {
                    tr.append("<thead><tr><th style='display: none'>Resource ID</th><th>Resource Type</th><th style='text-align: center'>No. of Resource</th><th style='text-align: center'>Delete</th></tr></thead><tbody>");
                }
                $.each(listEditResource, function (index, record) {
                    tr = $('<tr/>');
                    tr.append("<td style='display: none'>" + this['ResourceID'].toString().trim() + "</td>");//0
                    tr.append("<td class='word-wrap text-wrap width-180'>" + this['UTNAME'].toString().trim() + "</td>");//1
                    tr.append("<td class='word-wrap text-wrap width-50' style='text-align: center'>" + this['NoOfResource'].toString().trim() + "</td>");//2
                    tr.append("<td class='word-wrap text-wrap width-45' style='text-align: center'><input type='image' class='gvDestResourceDelete'  id='btnDestResourcedelete' <img src='../Images/ico_delete_16.png' title='Delete'/></input></td>")//3;
                    $("#DestinationResourceGrid").append(tr);
                });
                tr.append("</tbody>");
            }
            /*Resource Destination End*/


            /*QC Destination Start*/
            if (listEditQC.length > 0) {
                $("#DestinationQCGrid").empty();
                tr = $('#DestinationQCGrid');
                HeaderCount = $('#DestinationQCGrid thead th').length;
                if (HeaderCount == 0) {
                    tr.append("<thead><tr><th style='display: none'>QC ID</th><th>QC Name</th><th style='text-align: center'>Delete</th></tr></thead><tbody>");
                }
                $.each(listEditQC, function (index, record) {
                    tr = $('<tr/>');
                    tr.append("<td style='display: none'>" + this['QCID'].toString().trim() + "</td>");//0
                    tr.append("<td class='word-wrap text-wrap width-180'>" + this['QCName'].toString().trim() + "</td>");//1
                    tr.append("<td class='word-wrap text-wrap width-45' style='text-align: center'><input type='image' class='gvDestQcDelete'  id='btnDestQcdelete' <img src='../Images/ico_delete_16.png' title='Delete'/></input></td>")//2;
                    $("#DestinationQCGrid").append(tr);
                });
                tr.append("</tbody>");
            }
            /*QC Destination End*/


            /*Process ID Start*/
            $.each(listEditProcess, function (index, record) {
                //debugger;
                $("#hdnProcessID").val(this['ProcessID'].toString().trim());
            });
            /*Process ID End*/

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

function DeleteFramework(frameworkid) {
    debugger;

    $.ajax({
        type: "POST",
        url: "/Process/DeleteFramework",
        data: { FrameworkID: frameworkid },
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
                bindframeworkgrid();
                toastr.success('<b><font color=black>' + messagetext + '</font></b>');
            }
            else {
                toastr.error('<b><font color=black>' + messagetext + '</font></b>');
            }
        }
    });
}

//function checkProcessList() {
//    if ($('#ProcessMasterGrid').length) {
       
//        var chkflag = false;
//        var processid = '';
//        var processcode = '';
//        var processname = '';
//        chkflag = true;
//        processid = $("#ProcessID").val();
//        processcode = '';
//        processname = $("#Processid").find('option:selected').text();
//        if (chkflag == false) {
//            processid = '';
//            processcode = '';
//            processname = '';
//            toastr.error('Please select process...!');
//            return false;
//        }
//        else {
//                openPopUp(processid, processname);
//                //addProcess(processid, processcode, processname);
//        }
//    }
//    else {
//        toastr.warning('Process list not available...!');
//        return false;
//    }
//}

//function addProcess(ProcessId, ProcessCode, ProcessName) {
//    if ($('#ProcessDestinationGrid').length) {       
//        //debugger;
//        var count = 0;
//        var exists = false;
//        var arraydetails = [];
//        count = $('#ProcessDestinationGrid tbody tr').length;
//        $('#ProcessDestinationGrid tbody tr').each(function () {
//            var dispatchdetail = {};
//            var processidgrd = $(this).find('td:eq(0)').html().trim();
//            dispatchdetail.PROCESSID = processidgrd;
//            arraydetails.push(dispatchdetail);
//        });

//        var jsondispatchobj = {};
//        jsondispatchobj.processDetails = arraydetails;

//        for (i = 0; i < jsondispatchobj.processDetails.length; i++) {
//            if (jsondispatchobj.processDetails[i].PROCESSID.trim() == ProcessId.trim()) {
//                exists = true;
//                break;
//            }
//        }
//        if (exists != false) {
//            toastr.error('Process already exists...!');
//            return false;
//        }
//        else {
//            if (count >= 1) {
//                toastr.error('You can not add multiple process...!');
//                return false;
//            }
//            else {
//                addProcessDestinationGrid(ProcessId, ProcessCode, ProcessName);
//            }
//        }
       
//    }
//}

function addProcessDestinationGrid(ProcessId, ProcessCode, ProcessName) {
    debugger;               
    //Create Table 
   
    $('#hdnProcessID').val(ProcessId);
    var tr;
    tr = $('#ProcessDestinationGrid');
    var HeaderCount = $('#ProcessDestinationGrid thead th').length;
    if (HeaderCount == 0) {
        tr.append("<thead><tr><th style='display: none'>Process ID</th><th>Process Code</th><th>Process Name</th><th style='display: none'>Total Duration(Hours)</th><th style='display: none'>BOMDETAILS</th><th>Delete</th></tr></thead><tbody>");
    }

    tr = $('<tr/>');
    tr.append("<td style='display: none'>" + ProcessId.trim() + "</td>");//0
    tr.append("<td>" + ProcessCode + "</td>");//1
    tr.append("<td>" + ProcessName + "</td>");//2
    tr.append("<td style='display: none'><input type='text' class='gvduration'  id='txtduration' value='0' style='text-align: right; width:60px; height:20px' disabled></input></td>");//3
    tr.append("<td style='display: none'><input type='image' class='gvDestPopup'   id='btnDestpopup'   <img src='../Images/ico_PackingList.png' width='20' height ='20' title='Open BomDetails'/></input></td>");                
    tr.append("<td style='text-align: center'><input type='image' class='gvDestDelete'  id='btnDestdelete' <img src='../Images/ico_delete_16.png' title='Delete'/></input></td>");
    $("#ProcessDestinationGrid").append(tr);
    tr.append("</tbody>");
   
}

function searchInputMaterial() {
     debugger;
    // Declare variables
    var input, filter, table, tr, tdcode, tdname, i, j, txtValue, txtValue2;
    input = document.getElementById("txtInputMaterialSearch");
    filter = input.value.toUpperCase();
    table = document.getElementById("SourceMaterialInputGrid");
    tr = table.getElementsByTagName("tr");

    // Loop through all table rows, and hide those who don't match the search query
    for (i = 0; i < tr.length; i++) {
     
        tdname = tr[i].getElementsByTagName("td")[2];
        if (tdname) {
            txtValue = tdname.textContent || tdname.innerText 
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
           
        }
    }
}

function searchOutputMaterial() {
    debugger;
    // Declare variables
    var input, filter, table, tr, tdcode, tdname, i, j, txtValue, txtValue2;
    input = document.getElementById("txtOutputMaterialSearch");
    filter = input.value.toUpperCase();
    table = document.getElementById("SourceMaterialOutputGrid");
    tr = table.getElementsByTagName("tr");

    // Loop through all table rows, and hide those who don't match the search query
    for (i = 0; i < tr.length; i++) {
        tdname = tr[i].getElementsByTagName("td")[2];
        if (tdname) {
            txtValue = tdname.textContent || tdname.innerText
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}

function LoadProcessSequence(ProcessName) {
    debugger;
    $("#TabProcessname").text(ProcessName.trim());
    setTimeout(function () {
        debugger;
        
        bindProcessSequence();
        $('#Bom').removeClass('loader');
        $('#FgSfg').removeClass("loader");
        $('#Workstation').removeClass("loader");
        $('#Resource').removeClass("loader");
        $('#QC').removeClass("loader");
        $('#dvProcessTab').dialog({ dialogClass: 'none' });
        $('#DestinationMaterialInputGrid').css("display", "");
        $('#DestinationMaterialOutputGrid').css("display", "");
        $('#DestinationWorkstationGrid').css("display", "");
        $('#DestinationResourceGrid').css("display", "");
        $('#DestinationQCGrid').css("display", "");
        
    }, 1000);
    
}

function bindProcessSequence() {
    //debugger;
    var tr;
    var HeaderCount = 0;
    $.ajax({
        type: "POST",
        url: "/Process/ProcessSequence",
        data: '{}',
        dataType: "json",
        async: false,
        success: function (response) {

            debugger;
            var listInputMaterial = response.allsequenceDataset.varInputMaterial;
            var listOutputMaterial = response.allsequenceDataset.varOutputMaterial;
            var listWorkstation = response.allsequenceDataset.varWorkstation;
            var listResource = response.allsequenceDataset.varResource;
            var listQC = response.allsequenceDataset.varQC;
            var listInputUom = response.allsequenceDataset.varInputUom;
            var listOutputUom = response.allsequenceDataset.varOutputUom;

            /*Insput Material Source Start*/
            if (listInputMaterial.length > 0) {

                $("#SourceMaterialInputGrid").empty();
                tr = $('#SourceMaterialInputGrid');
                HeaderCount = $('#SourceMaterialInputGrid thead th').length;
                if (HeaderCount == 0) {
                    tr.append("<thead><tr><th style='display: none'>Product ID</th><th>Code</th><th>Product</th><th>Type</th><th>Unit</th><th>Qty.</th><th>Bom.Qty.</th></tr></thead><tbody>");
                }

                //$('#SourceMaterialInputGrid').DataTable().destroy();
                //$("#SourceMaterialInputGrid tbody tr").remove();
                $.each(listInputMaterial, function (index, record) {
                    tr = $('<tr/>');
                    tr.append("<td style='display: none'>" + this['ID'].toString().trim() + "</td>");//0
                    tr.append("<td class='word-wrap text-wrap width-130'>" + this['CODE'].toString().trim() + "</td>");//1
                    tr.append("<td class='word-wrap text-wrap width-180'>" + this['NAME'].toString().trim() + "</td>");//2
                    tr.append("<td class='word-wrap text-wrap width-60'>" + this['TYPE'].toString().trim() + "</td>");//3
                    tr.append("<td class='word-wrap text-wrap width-60' style='text-align: left'><select class='gvinputuom'  id='ddlinputuom' style='width:58px; height:18px'></select></input></td>");//4
                    tr.append("<td class='word-wrap text-wrap width-50' style='text-align: left'><input type='text' class='gvinputqty'  id='txtinputqty' style='text-align: right; width:50px; height:18px'></input></td>");//5
                    tr.append("<td class='word-wrap text-wrap width-50' style='text-align: left'><input type='text' class='gvinputrefqty'  id='txtinputrefqty' style='text-align: right; width:50px; height:18px'></input></td>");//6
                    $("#SourceMaterialInputGrid").append(tr);
                });
                tr.append("</tbody>");
                /*Bind Input UOM Start*/
                if ($('#SourceMaterialInputGrid').length) {
                    $('#SourceMaterialInputGrid').each(function () {
                        $('#SourceMaterialInputGrid tbody tr').each(function () {
                            var ddl = $(this).find('#ddlinputuom');
                            var Type = $(this).find('td:eq(3)').html().trim();
                            ddl.empty().append('<option selected="selected" value="0">Select</option>');;
                            $.each(listInputUom, function (index, record) {
                                ddl.append($("<option></option>").val(this['INPUTUOMID']).html(this['INPUTUOMDESCRIPTION']));
                            });
                            //if (Type == 'RM' || Type == 'SFG' || Type == 'TRADING' || Type == 'PF') {
                            //    ddl.val('DDE9A795-EB6C-48A2-AC09-6C7D851AD472');/*KG*/
                            //}
                            //else if (Type == 'PM') {
                            //    ddl.val('B9F29D12-DE94-40F1-A668-C79BF1BF4425');/*PCS*/
                            //}
                        })
                    })
                }
                /*Bind Input UOM End*/
            }
            /*Insput Material Source End*/



            /*Output Material Source Start*/
            if (listOutputMaterial.length > 0) {
                $("#SourceMaterialOutputGrid").empty();
                tr = $('#SourceMaterialOutputGrid');
                HeaderCount = $('#SourceMaterialOutputGrid thead th').length;
                if (HeaderCount == 0) {
                    tr.append("<thead><tr><th style='display: none'>Product ID</th><th>Code</th><th>Product</th><th>Type</th><th>Unit</th><th> Required Qty.</th><th>Bom.Qty.</th></tr></thead><tbody>");
                }
                $.each(listOutputMaterial, function (index, record) {
                    tr = $('<tr/>');
                    tr.append("<td style='display: none'>" + this['ID'].toString().trim() + "</td>");//0
                    tr.append("<td class='word-wrap text-wrap width-130'>" + this['CODE'].toString().trim() + "</td>");//1
                    tr.append("<td class='word-wrap text-wrap width-180'>" + this['NAME'].toString().trim() + "</td>");//2
                    tr.append("<td class='word-wrap text-wrap width-60'>" + this['TYPE'].toString().trim() + "</td>");//3
                    tr.append("<td class='word-wrap text-wrap width-60' style='text-align: left'><select class='gvoutputuom'  id='ddloutputuom' style='width:58px; height:18px'><option value=0>Select</select></input></td>");//4
                    tr.append("<td class='word-wrap text-wrap width-50' style='text-align: left'><input type='text' class='gvoutputqty'  id='txtoutputqty' style='text-align: right; width:50px; height:18px'></input></td>");//5
                    tr.append("<td class='word-wrap text-wrap width-50' style='text-align: left'><input type='text' class='gvoutputrefqty'  id='txtoutputrefqty' style='text-align: right; width:50px; height:18px'></input></td>");//6
                    $("#SourceMaterialOutputGrid").append(tr);
                });
                tr.append("</tbody>");
                /*Bind Output UOM Start*/
                if ($('#SourceMaterialOutputGrid').length) {
                    $('#SourceMaterialOutputGrid').each(function () {
                        $('#SourceMaterialOutputGrid tbody tr').each(function () {
                            var ddl = $(this).find('#ddloutputuom');
                            var Type = $(this).find('td:eq(3)').html().trim();
                            ddl.empty().append('<option selected="selected" value="0">Select</option>');;
                            $.each(listOutputUom, function (index, record) {
                                ddl.append($("<option></option>").val(this['OUTPUTUOMID']).html(this['OUTPUTUOMDESCRIPTION']));
                            });
                            if (Type == 'SFG') {
                                ddl.val('DDE9A795-EB6C-48A2-AC09-6C7D851AD472');/*KG*/
                            }
                            else if (Type == 'FG') {
                                ddl.val('B9F29D12-DE94-40F1-A668-C79BF1BF4425');/*PCS*/
                            }
                        })
                    })
                }
                /*Bind Input UOM End*/
            }
            /*Output Material Source End*/


            /*Workstation Source Start*/
            if (listWorkstation.length > 0) {
                $("#SourceWorkstationGrid").empty();
                tr = $('#SourceWorkstationGrid');
                HeaderCount = $('#SourceWorkstationGrid thead th').length;
                if (HeaderCount == 0) {
                    tr.append("<thead><tr><th style='display: none'>Workstation ID</th><th>Code</th><th>Workstation</th><th>Duration(Hrs.)</th></tr></thead><tbody>");
                }
                $.each(listWorkstation, function (index, record) {
                    tr = $('<tr/>');
                    tr.append("<td style='display: none'>" + this['WorkStationID'].toString().trim() + "</td>");//0
                    tr.append("<td class='word-wrap text-wrap width-130'>" + this['WorkStationCode'].toString().trim() + "</td>");//1
                    tr.append("<td class='word-wrap text-wrap width-180'>" + this['WorkStationName'].toString().trim() + "</td>");//2
                    tr.append("<td class='word-wrap text-wrap width-60' style='text-align: center'><input type='text' class='gvWorkstationSource'  id='txtWorkstationSource' style='text-align: right; width:45px; height:18px'></input></td>");//3
                    $("#SourceWorkstationGrid").append(tr);
                });
                tr.append("</tbody>");
            }
            /*Workstation Source End*/


            /*Resource Source Start*/
            if (listResource.length > 0) {
                $("#SourceResourceGrid").empty();
                tr = $('#SourceResourceGrid');
                HeaderCount = $('#SourceResourceGrid thead th').length;
                if (HeaderCount == 0) {
                    tr.append("<thead><tr><th style='display: none'>Resource ID</th><th>Resource Type</th><th>No. of Resource</th></tr></thead><tbody>");
                }
                $.each(listResource, function (index, record) {
                    tr = $('<tr/>');
                    tr.append("<td style='display: none'>" + this['UTID'].toString().trim() + "</td>");//0
                    tr.append("<td class='word-wrap text-wrap width-180'>" + this['UTNAME'].toString().trim() + "</td>");//1
                    tr.append("<td class='word-wrap text-wrap width-60' style='text-align: center'><input type='text' class='gvResourceSource'  id='txtResourceSource' style='text-align: right; width:45px; height:18px'></input></td>");//2
                    $("#SourceResourceGrid").append(tr);
                });
                tr.append("</tbody>");
            }
            /*Resource Source End*/


            /*QC Source Start*/
            if (listQC.length > 0) {
                $("#SourceQCGrid").empty();
                tr = $('#SourceQCGrid');
                HeaderCount = $('#SourceQCGrid thead th').length;
                if (HeaderCount == 0) {
                    tr.append("<thead><tr><th style='display: none'>QC ID</th><th>QC Name</th><th style='text-align: center'>Select</th></tr></thead><tbody>");
                }
                $.each(listQC, function (index, record) {
                    tr = $('<tr/>');
                    tr.append("<td style='display: none'>" + this['QCID'].toString().trim() + "</td>");//0
                    tr.append("<td class='word-wrap text-wrap width-130'>" + this['QCName'].toString().trim() + "</td>");//1
                    tr.append("<td class='word-wrap text-wrap width-45' style='text-align: center'><input type='checkbox' class='gvQCSource'  id='chkQCSource'></input></td>");//2
                    $("#SourceQCGrid").append(tr);
                });
                tr.append("</tbody>");
            }
            /*QC Source End*/
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

/* Input Material Destination Start*/
function IsExistsInputMaterial(InputMaterialID, InputMaterialCode, InputMaterialName, Type, InputUomID, InputUomName, InputQty, InputRefQty) {
    debugger;
    if ($('#DestinationMaterialInputGrid').length) {
            var exists = false;
            var arraydetails = [];
        $('#DestinationMaterialInputGrid tbody tr').each(function () {
            var dispatchdetail = {};
            var inputmaterialgrd = $(this).find('td:eq(0)').html().trim();
            dispatchdetail.INPUTMATERIAL = inputmaterialgrd;
            arraydetails.push(dispatchdetail);
        });
        var jsondispatchobj = {};
        jsondispatchobj.materialDetails = arraydetails;

        for (i = 0; i < jsondispatchobj.materialDetails.length; i++) {
            if (jsondispatchobj.materialDetails[i].INPUTMATERIAL.trim() == InputMaterialID.trim()) {
                exists = true;
                break;
            }
        }
        if (exists != false) {
            toastr.error('' + InputMaterialName +' already exists...!');
            return false;
        }
        else {
            addInputDestinationGrid(InputMaterialID, InputMaterialCode, InputMaterialName, Type, InputUomID, InputUomName, InputQty, InputRefQty);
        }
    }
}

function addInputDestinationGrid(InputMaterialID, InputMaterialCode, InputMaterialName, Type, InputUomID, InputUomName, InputQty, InputRefQty) {
    //debugger;
    //Create Table 
    var qty = 0;
    var Refqty = 0;
    var tr;
    tr = $('#DestinationMaterialInputGrid');
    var HeaderCount = $('#DestinationMaterialInputGrid thead th').length;
    var FooterCount = $('#DestinationMaterialInputGrid tfoot th').length;
    if (HeaderCount == 0) {
        tr.append("<thead><tr><th style='display: none'>Product ID</th><th style='display: none'>Code</th><th>Product</th><th>Type</th><th style='display: none'>UnitID</th><th>Unit</th><th>I/P Qty.</th><th>Bom.Qty.</th><th class='word-wrap text-wrap width-45' style='text-align: center'>Del</th></tr></thead>");
    }
    if (FooterCount == 0) {
        tr.append("<tfoot><tr><th style='display: none'></th><th style='display: none'></th><th>Total</th><th></th><th style='display: none'></th><th></th><th id='tfInputQty'></th><th id='tfInputRefQty'></th><th class='word-wrap text-wrap width-45' style='text-align: center'></th></tr></tfoot><tbody>");
    }
    tr = $('<tr/>');
    tr.append("<td style='display: none'>" + InputMaterialID.toString().trim() + "</td>");//0
    tr.append("<td style='display: none'>" + InputMaterialCode.toString().trim() + "</td>");//1
    tr.append("<td class='word-wrap text-wrap width-180'>" + InputMaterialName.toString().trim() + "</td>");//2
    tr.append("<td class='word-wrap text-wrap width-50'>" + Type.toString().trim() + "</td>");//3
    tr.append("<td style='display: none'>" + InputUomID.toString().trim() + "</td>");//4
    tr.append("<td class='word-wrap text-wrap width-60'>" + InputUomName.toString().trim() + "</td>");//5
    tr.append("<td class='word-wrap text-wrap width-60'>" + InputQty.toString().trim() + "</td>");//6
    tr.append("<td class='word-wrap text-wrap width-60'>" + InputRefQty.toString().trim() + "</td>");//7
    tr.append("<td class='word-wrap text-wrap width-45' style='text-align: center'><input type='image' class='gvDestInputMaterialDelete'  id='btnDestInputMaterialdelete' <img src='../Images/ico_delete_16.png' title='Delete'/></input></td>")//8;
    $("#DestinationMaterialInputGrid").append(tr);
    tr.append("</tbody>");

    debugger;
    $('#DestinationMaterialInputGrid tbody tr').each(function () {
        qty += parseFloat($(this).find('td:eq(6)').html().trim());
        Refqty += parseFloat($(this).find('td:eq(7)').html().trim());
    });
    $('tfoot th#tfInputQty').html(qty.toFixed(4));
    $('tfoot th#tfInputRefQty').html(Refqty.toFixed(4));
}
/* Input Material Destination End*/


/* Output Material Destination Start*/
function IsExistsOutputMaterial(OutputMaterialID, OutputMaterialCode, OutputMaterialName, Type, OutputUomID, OutputUomName, OutputQty, OutputRefQty) {
    debugger;
    var count = 0;
    if ($('#DestinationMaterialOutputGrid').length) {
        var exists = false;
        var arraydetails = [];
        count = $('#DestinationMaterialOutputGrid tbody tr').length;
        $('#DestinationMaterialOutputGrid tbody tr').each(function () {
            var dispatchdetail = {};
            var outputmaterialgrd = $(this).find('td:eq(0)').html().trim();
            dispatchdetail.OUTPUTMATERIAL = outputmaterialgrd;
            arraydetails.push(dispatchdetail);
        });
        var jsondispatchobj = {};
        jsondispatchobj.materialDetails = arraydetails;

        for (i = 0; i < jsondispatchobj.materialDetails.length; i++) {
            if (jsondispatchobj.materialDetails[i].OUTPUTMATERIAL.trim() == OutputMaterialID.trim()) {
                exists = true;
                break;
            }
        }
        if (exists != false) {
            toastr.error('' + OutputMaterialName + ' already exists...!');
            return false;
        }
        else {
            if (count >= 1) {
                toastr.error('You can not enter multiple output material...!');
                return false;
            }
            else {
                addOutputDestinationGrid(OutputMaterialID, OutputMaterialCode, OutputMaterialName, Type, OutputUomID, OutputUomName, OutputQty, OutputRefQty);
            }
        }
    }
}

function addOutputDestinationGrid(OutputMaterialID, OutputMaterialCode, OutputMaterialName, Type, OutputUomID, OutputUomName, OutputQty, OutputRefQty) {
    debugger;
    //Create Table 
    var qty = 0;
    var Refqty = 0;
    var tr;
    tr = $('#DestinationMaterialOutputGrid');
    var HeaderCount = $('#DestinationMaterialOutputGrid thead th').length;
    var FooterCount = $('#DestinationMaterialOutputGrid tfoot th').length;
    if (HeaderCount == 0) {
        tr.append("<thead><tr><th style='display: none'>Product ID</th><th style='display: none'>Code</th><th>Product</th><th>Type</th><th style='display: none'>UnitID</th><th>Unit</th><th>O/P Qty.</th><th>Bom.Qty.</th><th class='word-wrap text-wrap width-45' style='text-align: center'>Del</th></tr></thead>");
    }
    if (FooterCount == 0) {
        tr.append("<tfoot><tr><th style='display: none'></th><th style='display: none'></th><th>Total</th><th></th><th style='display: none'></th><th></th><th id='tfOutputQty'></th><th id='tfOutputRefQty'></th><th class='word-wrap text-wrap width-45' style='text-align: center'></th></tr></tfoot><tbody>");
    }
    tr = $('<tr/>');
    tr.append("<td style='display: none'>" + OutputMaterialID.toString().trim() + "</td>");//0
    tr.append("<td style='display: none'>" + OutputMaterialCode.toString().trim() + "</td>");//1
    tr.append("<td class='word-wrap text-wrap width-180'>" + OutputMaterialName.toString().trim() + "</td>");//2
    tr.append("<td class='word-wrap text-wrap width-50'>" + Type.toString().trim() + "</td>");//3
    tr.append("<td style='display: none'>" + OutputUomID.toString().trim() + "</td>");//4
    tr.append("<td class='word-wrap text-wrap width-60'>" + OutputUomName.toString().trim() + "</td>");//5
    tr.append("<td class='word-wrap text-wrap width-60'>" + OutputQty.toString().trim() + "</td>");//6
    tr.append("<td class='word-wrap text-wrap width-60'>" + OutputRefQty.toString().trim() + "</td>");//7
    tr.append("<td class='word-wrap text-wrap width-45' style='text-align: center'><input type='image' class='gvDestOutputMaterialDelete'  id='btnDestOutputMaterialdelete' <img src='../Images/ico_delete_16.png' title='Delete'/></input></td>")//8;
    $("#DestinationMaterialOutputGrid").append(tr);
    tr.append("</tbody>");

    debugger;
    $('#DestinationMaterialOutputGrid tbody tr').each(function () {
        qty += parseFloat($(this).find('td:eq(6)').html().trim());
        Refqty += parseFloat($(this).find('td:eq(7)').html().trim());
    });
    $('tfoot th#tfOutputQty').html(qty.toFixed(4));
    $('tfoot th#tfOutputRefQty').html(Refqty.toFixed(4));

}
/* Output Material Destination End*/


/* Workstation Destination Start*/
function IsExistsWorkstation(workstationid, workstationcode, workstationname, workstationduration) {
    debugger;
    var count = 0;
    if ($('#DestinationWorkstationGrid').length) {
        var exists = false;
        var arraydetails = [];
        count = $('#DestinationWorkstationGrid tbody tr').length;
        $('#DestinationWorkstationGrid tbody tr').each(function () {
            var dispatchdetail = {};
            var workstationgrd = $(this).find('td:eq(0)').html().trim();
            dispatchdetail.WORKSTATION = workstationgrd;
            arraydetails.push(dispatchdetail);
        });
        var jsondispatchobj = {};
        jsondispatchobj.workstationDetails = arraydetails;

        for (i = 0; i < jsondispatchobj.workstationDetails.length; i++) {
            if (jsondispatchobj.workstationDetails[i].WORKSTATION.trim() == workstationid.trim()) {
                exists = true;
                break;
            }
        }
        if (exists != false) {
            toastr.error('' + workstationname + ' already exists...!');
            return false;
        }
        else {
            if (count >= 1) {
                toastr.error('You can not enter multiple workstation...!');
                return false;
            }
            else {
                addWorkstationDestinationGrid(workstationid, workstationcode, workstationname, workstationduration);
            }
        }
    }
}

function addWorkstationDestinationGrid(workstationid, workstationcode, workstationname, workstationduration) {
    debugger;
    //Create Table 
    var tr;
    tr = $('#DestinationWorkstationGrid');
    var HeaderCount = $('#DestinationWorkstationGrid thead th').length;
    if (HeaderCount == 0) {
        tr.append("<thead><tr><th style='display: none'>Workstation ID</th><th>Code</th><th>Workstation</th><th style='text-align: center'>Duration(Hrs.)</th><th style='text-align: center'>Delete</th></tr></thead><tbody>");
    }

    tr = $('<tr/>');
    tr.append("<td style='display: none'>" + workstationid.toString().trim() + "</td>");//0
    tr.append("<td>" + workstationcode.toString().trim() + "</td>");//1
    tr.append("<td class='word-wrap text-wrap width-180'>" + workstationname.toString().trim() + "</td>");//2
    tr.append("<td class='word-wrap text-wrap width-60' style='text-align: center'>" + workstationduration.toString().trim() + "</td>");//3
    tr.append("<td style='text-align: center'><input type='image' class='gvDestWorkstationDelete'  id='btnDestWorkstationdelete' <img src='../Images/ico_delete_16.png' title='Delete'/></input></td>")//4;
    $("#DestinationWorkstationGrid").append(tr);
    tr.append("</tbody>");

}
/* Workstation Destination End*/


/* Resource Destination Start*/
function IsExistsResource(resourceid, resourcename, resourceno) {
    debugger;
    var count = 0;
    if ($('#DestinationResourceGrid').length) {
        var exists = false;
        var arraydetails = [];
        count = $('#DestinationResourceGrid tbody tr').length;
        $('#DestinationResourceGrid tbody tr').each(function () {
            var dispatchdetail = {};
            var resourcegrd = $(this).find('td:eq(0)').html().trim();
            dispatchdetail.RESOURCE = resourcegrd;
            arraydetails.push(dispatchdetail);
        });
        var jsondispatchobj = {};
        jsondispatchobj.resourceDetails = arraydetails;

        for (i = 0; i < jsondispatchobj.resourceDetails.length; i++) {
            if (jsondispatchobj.resourceDetails[i].RESOURCE.trim() == resourceid.trim()) {
                exists = true;
                break;
            }
        }
        if (exists != false) {
            toastr.error('' + resourcename + ' already exists...!');
            return false;
        }
        else {
            if (count >= 1) {
                toastr.error('You can not enter multiple resource...!');
                return false;
            }
            else {
                addResourceDestinationGrid(resourceid, resourcename, resourceno);
            }
        }
    }
}

function addResourceDestinationGrid(resourceid, resourcename, resourceno) {
    debugger;
    //Create Table 
    var tr;
    tr = $('#DestinationResourceGrid');
    var HeaderCount = $('#DestinationResourceGrid thead th').length;
    if (HeaderCount == 0) {
        tr.append("<thead><tr><th style='display: none'>Resource ID</th><th>Resource Type</th><th style='text-align: center'>No. of Resource</th><th style='text-align: center'>Delete</th></tr></thead><tbody>");
    }

    tr = $('<tr/>');
    tr.append("<td style='display: none'>" + resourceid.toString().trim() + "</td>");//0
    tr.append("<td class='word-wrap text-wrap width-180'>" + resourcename.toString().trim() + "</td>");//1
    tr.append("<td class='word-wrap text-wrap width-50' style='text-align: center'>" + resourceno.toString().trim() + "</td>");//2
    tr.append("<td class='word-wrap text-wrap width-45' style='text-align: center'><input type='image' class='gvDestResourceDelete'  id='btnDestResourcedelete' <img src='../Images/ico_delete_16.png' title='Delete'/></input></td>")//3;
    $("#DestinationResourceGrid").append(tr);
    tr.append("</tbody>");

}
/* Resource Destination End*/


/* QC Destination Start*/
function IsExistsQc(qcid, qcname) {
    debugger;
    var count = 0;
    if ($('#DestinationQCGrid').length) {
        var exists = false;
        var arraydetails = [];
        count = $('#DestinationQCGrid tbody tr').length;
        $('#DestinationQCGrid tbody tr').each(function () {
            var dispatchdetail = {};
            var qcgrd = $(this).find('td:eq(0)').html().trim();
            dispatchdetail.QC = qcgrd;
            arraydetails.push(dispatchdetail);
        });
        var jsondispatchobj = {};
        jsondispatchobj.qcDetails = arraydetails;

        for (i = 0; i < jsondispatchobj.qcDetails.length; i++) {
            if (jsondispatchobj.qcDetails[i].QC.trim() == qcid.trim()) {
                exists = true;
                break;
            }
        }
        if (exists != false) {
            toastr.error('' + qcname + ' already exists...!');
            return false;
        }
        else {
            if (count >= 1) {
                toastr.error('You can not enter multiple QC...!');
                return false;
            }
            else {
                addQcDestinationGrid(qcid, qcname);
            }
        }
    }
}

function addQcDestinationGrid(qcid, qcname) {
    debugger;
    //Create Table 
    var tr;
    tr = $('#DestinationQCGrid');
    var HeaderCount = $('#DestinationQCGrid thead th').length;
    if (HeaderCount == 0) {
        tr.append("<thead><tr><th style='display: none'>QC ID</th><th>QC Name</th><th style='text-align: center'>Delete</th></tr></thead><tbody>");
    }

    tr = $('<tr/>');
    tr.append("<td style='display: none'>" + qcid.toString().trim() + "</td>");//0
    tr.append("<td class='word-wrap text-wrap width-180'>" + qcname.toString().trim() + "</td>");//1
    tr.append("<td class='word-wrap text-wrap width-45' style='text-align: center'><input type='image' class='gvDestQcDelete'  id='btnDestQcdelete' <img src='../Images/ico_delete_16.png' title='Delete'/></input></td>")//2;
    $("#DestinationQCGrid").append(tr);
    tr.append("</tbody>");

}
/* QC Destination End*/

function CalculateInputMaterialFooter() {
    var qty = 0;
    var Refqty = 0;
    $('#DestinationMaterialInputGrid tbody tr').each(function () {
        qty += parseFloat($(this).find('td:eq(6)').html().trim());
        Refqty += parseFloat($(this).find('td:eq(7)').html().trim());
    });
    $('tfoot th#tfInputQty').html(qty.toFixed(4));
    $('tfoot th#tfInputRefQty').html(Refqty.toFixed(4));
}

function CalculateOutputMaterialFooter() {
    var qty = 0;
    var Refqty = 0;
    $('#DestinationMaterialOutputGrid tbody tr').each(function () {
        qty += parseFloat($(this).find('td:eq(6)').html().trim());
        Refqty += parseFloat($(this).find('td:eq(7)').html().trim());
    });
    $('tfoot th#tfOutputQty').html(qty.toFixed(4));
    $('tfoot th#tfOutputRefQty').html(Refqty.toFixed(4));
}

/*Edit function for Framework*/
$(function () {
    var frameworkid;
    $("body").on("click", "#FrameworkGrid .gvEdit", function () {
        debugger;
        var row = $(this).closest("tr");
        frameworkid = row.find('td:eq(1)').html();
        $('#hdnBomID').val(frameworkid);           
        $('#btnsave').css("display", "");
        $('#btnAddnew').css("display", "");
        $('#dvAdd').css("display", "");
        $('#dvDisplay').css("display", "none");

        LoadInputType();
        LoadOutputType();
        EditDetails($('#hdnBomID').val().trim());

        $($.fn.dataTable.tables(true)).DataTable().columns.adjust();
        
        $("#ProductID").chosen({
            search_contains: true
        });
        $("#ProductID").trigger("chosen:updated");
    })

})

/*Delete function for Framework*/
$(function () {
    var frameworkid;
    $("body").on("click", "#FrameworkGrid .gvDelete", function () {
        var row = $(this).closest("tr");
        frameworkid = row.find('td:eq(1)').html();
        $('#hdnBomID').val(frameworkid);
        if (confirm("Do you want to delete this item?")) {
            DeleteFramework(frameworkid);
        }

    })

})

/*Delete function for Process Destination grid*/
$(function () {
    var grdprocessid;
    var deleteflag = 0;
    $("body").on("click", "#ProcessDestinationGrid .gvDestDelete", function () {
        var row = $(this).closest("tr");
        grdprocessid = row.find('td:eq(0)').html();
        if (confirm("Do you want to delete this item?")) {
            deleteflag = 1;
            row.remove();
            var rowCount = document.getElementById("ProcessDestinationGrid").rows.length - 1;
            if (rowCount == 0) {
                $("#ProcessDestinationGrid").empty();
                $('#DestinationMaterialInputGrid').empty();
                $('#DestinationMaterialOutputGrid').empty();
                $('#DestinationWorkstationGrid').empty();
                $('#DestinationResourceGrid').empty();
                $('#DestinationQCGrid').empty();
            }
        }

    })

})

/*Process Sequence Popup*/
$(function () {
    var processid;
    var processname;
    $("body").on("click", "#ProcessDestinationGrid .gvDestPopup", function () {
        var row = $(this).closest("tr");
        processid = row.find('td:eq(0)').html();
        processname = row.find('td:eq(2)').html();
        $('#dvProcessTab').dialog({ dialogClass: 'transparent' }); 
        $('#dvProcessTab').dialog('open');
        //$("#divTab").find("input, textarea, select,submit,a").attr("disabled", "disabled"); 
        //$('#btnDestInputMaterialdelete').attr("disabled", "disabled"); 
        $('#DestinationMaterialInputGrid').css("display", "none");
        $('#DestinationMaterialOutputGrid').css("display", "none");
        $('#DestinationWorkstationGrid').css("display", "none");
        $('#DestinationResourceGrid').css("display", "none");
        $('#DestinationQCGrid').css("display", "none");
        $('#Bom').addClass("loader !important");
        $('#FgSfg').addClass("loader !important");
        $('#Workstation').addClass("loader !important");
        $('#Resource').addClass("loader !important");
        $('#QC').addClass("loader !important");
        debugger;
        setTimeout(function () {
        LoadInputType();
        LoadOutputType();
        /*NEW ADD  LoadProcessSequence(processname);*/
            $('#Bom').removeClass('loader');
            $('#FgSfg').removeClass("loader");
            $('#Workstation').removeClass("loader");
            $('#Resource').removeClass("loader");
            $('#QC').removeClass("loader");
            $('#dvProcessTab').dialog({ dialogClass: 'none' });
            $('#DestinationMaterialInputGrid').css("display", "");
            $('#DestinationMaterialOutputGrid').css("display", "");
            $('#DestinationWorkstationGrid').css("display", "");
            $('#DestinationResourceGrid').css("display", "");
            $('#DestinationQCGrid').css("display", "");
        }, 1000);
       
    })

})




/*Delete function for Input Material Destination grid*/
$(function () {
    var grdinputmaterialid;
    var deleteflag = 0;
    $("body").on("click", "#DestinationMaterialInputGrid .gvDestInputMaterialDelete", function () {
        debugger;
        var row = $(this).closest("tr");
        grdinputmaterialid = row.find('td:eq(0)').html();
        if (confirm("Do you want to delete this item?")) {
            deleteflag = 1;
            row.remove();
            var rowCount = document.getElementById("DestinationMaterialInputGrid").rows.length - 2;
            if (rowCount == 0) {
                $("#DestinationMaterialInputGrid").empty();
            }
            else if (rowCount > 0) {
                CalculateInputMaterialFooter();
            }
        }
    })

})

/*Delete function for Output Material Destination grid*/
$(function () {
    var grdoutputmaterialid;
    var deleteflag = 0;
    $("body").on("click", "#DestinationMaterialOutputGrid .gvDestOutputMaterialDelete", function () {
        var row = $(this).closest("tr");
        grdoutputmaterialid = row.find('td:eq(0)').html();
        if (confirm("Do you want to delete this item?")) {
            deleteflag = 1;
            row.remove();
            var rowCount = document.getElementById("DestinationMaterialOutputGrid").rows.length - 2;
            if (rowCount == 0) {
                $("#DestinationMaterialOutputGrid").empty();
            }
            else if (rowCount > 0) {
                CalculateOutputMaterialFooter();
            }
        }

    })

})

/*Delete function for Workstation Destination grid*/
$(function () {
    var grdworkstationid;
    var deleteflag = 0;
    $("body").on("click", "#DestinationWorkstationGrid .gvDestWorkstationDelete", function () {
        var row = $(this).closest("tr");
        grdworkstationid = row.find('td:eq(0)').html();
        if (confirm("Do you want to delete this item?")) {
            deleteflag = 1;
            row.remove();
            var rowCount = document.getElementById("DestinationWorkstationGrid").rows.length - 1;
            if (rowCount == 0) {
                $("#DestinationWorkstationGrid").empty();
            }
        }

    })

})

/*Delete function for Resource Destination grid*/
$(function () {
    var grdresourceid;
    var deleteflag = 0;
    $("body").on("click", "#DestinationResourceGrid .gvDestResourceDelete", function () {
        var row = $(this).closest("tr");
        grdresourceid = row.find('td:eq(0)').html();
        if (confirm("Do you want to delete this item?")) {
            deleteflag = 1;
            row.remove();
            var rowCount = document.getElementById("DestinationResourceGrid").rows.length - 1;
            if (rowCount == 0) {
                $("#DestinationResourceGrid").empty();
            }
        }

    })

})

/*Delete function for QC Destination grid*/
$(function () {
    var grdqcid;
    var deleteflag = 0;
    $("body").on("click", "#DestinationQCGrid .gvDestQcDelete", function () {
        var row = $(this).closest("tr");
        grdqcid = row.find('td:eq(0)').html();
        if (confirm("Do you want to delete this item?")) {
            deleteflag = 1;
            row.remove();
            var rowCount = document.getElementById("DestinationQCGrid").rows.length - 1;
            if (rowCount == 0) {
                $("#DestinationQCGrid").empty();
            }
        }

    })

})

function LoadInputType()
{
    var mode = "";
    mode = "Input";
    $.ajax({
        type: "POST",
        url: "/Process/LoadType",
        data: { MODE: mode },
        async: false,
        success: function (result) {

            var ddlInputType = $("#ddlInputType");
            ddlInputType.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(result, function () {
                ddlInputType.val(this['TYPENAME']);
                ddlInputType.append($("<option value=''></option>").val(this['TYPEID']).html(this['TYPENAME']));
            });
            ddlInputType.chosen();
            ddlInputType.trigger("chosen:updated");

        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function LoadOutputType() {
    var mode = "";
    mode = "Output";
    $.ajax({
        type: "POST",
        url: "/Process/LoadType",
        data: { MODE: mode },
        async: false,
        success: function (result) {

            var ddlOutputType = $("#ddlOutputType");
            ddlOutputType.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(result, function () {
                ddlOutputType.val(this['TYPENAME']);
                ddlOutputType.append($("<option value=''></option>").val(this['TYPEID']).html(this['TYPENAME']));
            });
            $("#ddlOutputType").chosen('destroy');
            $("#ddlOutputType").chosen({ width: '150px' });

        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function LoadBomProducts(type) {
    //debugger;
    var tr;
    var HeaderCount = 0;
    $.ajax({
        type: "POST",
        url: "/Process/ProductTypeWise",
        data: {TYPE:type},
        dataType: "json",
        async: false,
        success: function (response) {

            var listInputMaterial = response.allsequenceDataset.varInputMaterial;
            /*Insput Material Source Start*/
            if (listInputMaterial.length > 0) {

                $("#SourceMaterialInputGrid").empty();
                tr = $('#SourceMaterialInputGrid');
                HeaderCount = $('#SourceMaterialInputGrid thead th').length;
                if (HeaderCount == 0) {
                    tr.append("<thead><tr><th style='display: none'>Product ID</th><th>Code</th><th>Product</th><th>Type</th><th>Unit</th><th>Required Qty.</th><th>Bom.Qty.</th></tr></thead><tbody>");
                }

                //$('#SourceMaterialInputGrid').DataTable().destroy();
                //$("#SourceMaterialInputGrid tbody tr").remove();
                $.each(listInputMaterial, function (index, record) {
                    tr = $('<tr/>');
                    tr.append("<td style='display: none'>" + this['ID'].toString().trim() + "</td>");//0
                    tr.append("<td class='word-wrap text-wrap width-130'>" + this['CODE'].toString().trim() + "</td>");//1
                    tr.append("<td class='word-wrap text-wrap width-180'>" + this['NAME'].toString().trim() + "</td>");//2
                    tr.append("<td class='word-wrap text-wrap width-60'>" + this['TYPE'].toString().trim() + "</td>");//3
                    tr.append("<td class='word-wrap text-wrap width-60'>" + this['UOMNAME'].toString().trim() + "</td>");//4
                    tr.append("<td style='display:none'>" + this['UOMID'].toString().trim() + "</td>");//5
                    tr.append("<td class='word-wrap text-wrap width-50' style='text-align: left'><input type='text' class='gvinputqty'  id='txtinputqty' style='text-align: right; width:50px; height:18px'></input></td>");//5
                    tr.append("<td class='word-wrap text-wrap width-50' style='text-align: left'><input type='text' class='gvinputrefqty' id='txtinputrefqty' style='text-align: right; width:50px; height:18px'></input></td>");//6
                    $("#SourceMaterialInputGrid").append(tr);
                });
                tr.append("</tbody>");
                
            }
            /*Insput Material Source End*/

        },
    failure: function (response) {
        alert(response.responseText);
    },
    error: function (response) {
        alert(response.responseText);
    }
    });
}

function LoadFgSfgProducts(type) {
    var tr;
    var HeaderCount = 0;
    $.ajax({
        type: "POST",
        url: "/Process/ProductTypeWise",
        data: { TYPE: type },
        dataType: "json",
        async: false,
        success: function (response) {

            var listOutputMaterial = response.allsequenceDataset.varOutputMaterial;
            var listOutputUom = response.allsequenceDataset.varOutputUom;
            /*Output Material Source Start*/
            if (listOutputMaterial.length > 0) {
                $("#SourceMaterialOutputGrid").empty();
                tr = $('#SourceMaterialOutputGrid');
                HeaderCount = $('#SourceMaterialOutputGrid thead th').length;
                if (HeaderCount == 0) {
                    tr.append("<thead><tr><th style='display: none'>Product ID</th><th>Code</th><th>Product</th><th>Type</th><th>Unit</th><th>Required Qty.</th><th>Bom.Qty.</th></tr></thead><tbody>");
                }
                $.each(listOutputMaterial, function (index, record) {
                    tr = $('<tr/>');
                    tr.append("<td style='display: none'>" + this['ID'].toString().trim() + "</td>");//0
                    tr.append("<td class='word-wrap text-wrap width-130'>" + this['CODE'].toString().trim() + "</td>");//1
                    tr.append("<td class='word-wrap text-wrap width-180'>" + this['NAME'].toString().trim() + "</td>");//2
                    tr.append("<td class='word-wrap text-wrap width-60'>" + this['TYPE'].toString().trim() + "</td>");//3
                    tr.append("<td class='word-wrap text-wrap width-60'>" + this['UOMNAME'].toString().trim() + "</td>");//4
                    tr.append("<td style='display:none'>" + this['UOMID'].toString().trim() + "</td>");//5
                    tr.append("<td class='word-wrap text-wrap width-50' style='text-align: left'><input type='text' class='gvoutputqty'  id='txtoutputqty' style='text-align: right; width:50px; height:18px'></input></td>");//5
                    tr.append("<td class='word-wrap text-wrap width-50' style='text-align: left'><input type='text' class='gvoutputrefqty'  id='txtoutputrefqty' style='text-align: right; width:50px; height:18px'></input></td>");//6
                    $("#SourceMaterialOutputGrid").append(tr);
                });
                tr.append("</tbody>");
               
            }

        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function LoadFgSfgProductsById(productId) {
    var tr;
    var HeaderCount = 0;
    $.ajax({
        type: "POST",
        url: "/Process/ProductIdWise",
        data: { PRODUCTID: productId },
        dataType: "json",
        async: false,
        success: function (response) {

            var listOutputMaterial = response.allsequenceDataset.varOutputMaterial;
            /*Output Material Source Start*/
            if (listOutputMaterial.length > 0) {
                $("#SourceMaterialOutputGrid").empty();
                tr = $('#SourceMaterialOutputGrid');
                HeaderCount = $('#SourceMaterialOutputGrid thead th').length;
                if (HeaderCount == 0) {
                    tr.append("<thead><tr><th style='display: none'>Product ID</th><th>Code</th><th>Product</th><th>Type</th><th>Unit</th><th>Required Qty.</th><th>Bom.Qty.</th></tr></thead><tbody>");
                }
                $.each(listOutputMaterial, function (index, record) {
                    tr = $('<tr/>');
                    tr.append("<td style='display: none'>" + this['ID'].toString().trim() + "</td>");//0
                    tr.append("<td class='word-wrap text-wrap width-130'>" + this['CODE'].toString().trim() + "</td>");//1
                    tr.append("<td class='word-wrap text-wrap width-180'>" + this['NAME'].toString().trim() + "</td>");//2
                    tr.append("<td class='word-wrap text-wrap width-60'>" + this['TYPE'].toString().trim() + "</td>");//3
                    tr.append("<td class='word-wrap text-wrap width-60'>" + this['UOMNAME'].toString().trim() + "</td>");//4
                    tr.append("<td style='display:none'>" + this['UOMID'].toString().trim() + "</td>");//5
                    tr.append("<td class='word-wrap text-wrap width-50' style='text-align: left'><input type='text' class='gvoutputqty'  id='txtoutputqty' style='text-align: right; width:50px; height:18px'></input></td>");//5
                    tr.append("<td class='word-wrap text-wrap width-50' style='text-align: left'><input type='text' class='gvoutputrefqty'  id='txtoutputrefqty' style='text-align: right; width:50px; height:18px'></input></td>");//6
                    $("#SourceMaterialOutputGrid").append(tr);
                });
                tr.append("</tbody>");
               
            }

        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}



function openPopUp(processid,processname){
    //processid = row.find('td:eq(0)').html();
    //processname = row.find('td:eq(2)').html();
    //$('#dvProcessTab').dialog({ dialogClass: 'transparent' });
    //$('#dvProcessTab').dialog('open');
    $('#DestinationMaterialInputGrid').css("display", "none");
    $('#DestinationMaterialOutputGrid').css("display", "none");
    $('#DestinationWorkstationGrid').css("display", "none");
    $('#DestinationResourceGrid').css("display", "none");
    $('#DestinationQCGrid').css("display", "none");
    $('#Bom').addClass("loader !important");
    $('#FgSfg').addClass("loader !important");
    $('#Workstation').addClass("loader !important");
    $('#Resource').addClass("loader !important");
    $('#QC').addClass("loader !important");
    debugger;
    setTimeout(function () {
        LoadInputType();
        LoadOutputType();
        $('#Bom').removeClass('loader');
        $('#FgSfg').removeClass("loader");
        $('#Workstation').removeClass("loader");
        $('#Resource').removeClass("loader");
        $('#QC').removeClass("loader");
        $('#dvProcessTab').dialog({ dialogClass: 'none' });
        $('#DestinationMaterialInputGrid').css("display", "");
        $('#DestinationMaterialOutputGrid').css("display", "");
        $('#DestinationWorkstationGrid').css("display", "");
        $('#DestinationResourceGrid').css("display", "");
        $('#DestinationQCGrid').css("display", "");
    }, 1000);
}


/*Bomname Already exists or not checking*/
function checkBomNameAlreadyExistsOrNot(productName) {
    var mode = "BomNameCheck";
    $.ajax({
        type: "POST",
        url: "/Process/BomNameCheck",
        data: { MODE: mode, NAME: productName },
        dataType: "json",
        async: false,
        success: function (responseMessage) {
            var messageid;
            var messagetext;
            $.each(responseMessage, function (key, item) {
                messageid = item.MessageID;
                messagetext = item.MessageText;
            });
            if (messageid == '1') {
                $('#btnsave').hide();
                messagetext = "Process Name Already Exists Please Change it for further process";
                $('#BomName').css('background-color', '#BDB76B');
                $('#BomName').focus();
                toastr.warning('<b><font color=black>' + messagetext + '</font></b>');
            }
            else {
                $('#BomName').css('background-color', '#FFFFFF');
                $('#btnsave').show();
            }

        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}


/*packsize mapped or not*/
function packsizeMappedOrNot(productid) {
    var productName = $("#ProductID").find('option:selected').text();
    var mode = "packsizeCheck";
    $.ajax({
        type: "POST",
        url: "/Process/BomNameCheck",
        data: { MODE: mode, NAME: productid },
        dataType: "json",
        async: false,
        success: function (responseMessage) {
            var messageid;
            var messagetext;
            $.each(responseMessage, function (key, item) {
                messageid = item.MessageID;
                messagetext = item.MessageText;
            });
            if (messageid == '1') {
                messagetext = "Packsize not mapping,Please Check";
                toastr.warning('<b><font color=black>' + messagetext + '</font></b>');
                $('#btnsave').hide();
                return;
            }
            else {
                checkBomNameAlreadyExistsOrNot(productName);
               
            }
            

        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });

}

/*similar as bom*/
function bindbomforsimilar() {
    var mode = "";
    mode = "BOMPRODUCT";
    $.ajax({
        type: "POST",
        url: "/Process/LoadType",
        data: { MODE: mode },
        async: false,
        success: function (result) {

            var Bomid = $("#Bomid");
            Bomid.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(result, function () {
                Bomid.val(this['BomName']);
                Bomid.append($("<option value=''></option>").val(this['Bomid']).html(this['BomName']));
            });
            Bomid.chosen();
            Bomid.trigger("chosen:updated");

        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

/*function for similar product*/
function fetchBomDetailsforsimilar(frameworkID) {
    debugger;

    var tr;
    var HeaderCount = 0;
    var FooterCount = 0;
    var qty = 0;
    var Refqty = 0;
    var qty2 = 0;
    var Refqty2 = 0;
    $("#dialog").dialog({
        autoOpen: true,
        modal: true,
        title: "Loading.."
    });
    $("#imgLoader").css("visibility", "visible");

    $.ajax({
        type: "POST",
        url: "/Process/EditProcessFramework",
        data: { FrameworkID: frameworkID },
        dataType: "json",
        async: false,
        success: function (response) {
            var listEditInputMaterial = response.allEditDataset.varEditInputMaterial;
           

            /*Insput Material Destination Start*/
            if (listEditInputMaterial.length > 0) {

                $("#DestinationMaterialInputGrid").empty();
                tr = $('#DestinationMaterialInputGrid');
                HeaderCount = $('#DestinationMaterialInputGrid thead th').length;
                FooterCount = $('#DestinationMaterialInputGrid tfoot th').length;
                if (HeaderCount == 0) {
                    tr.append("<thead><tr><th style='display: none'>Product ID</th><th style='display: none'>Code</th><th>Product</th><th>Type</th><th style='display: none'>UnitID</th><th>Unit</th><th>I/P Qty.</th><th>Bom.Qty.</th><th class='word-wrap text-wrap width-45' style='text-align: center'>Del</th></tr></thead>");
                }
                if (FooterCount == 0) {
                    tr.append("<tfoot><tr><th style='display: none'></th><th style='display: none'></th><th>Total</th><th></th><th style='display: none'></th><th></th><th id='tfInputQty'></th><th id='tfInputRefQty'></th><th class='word-wrap text-wrap width-45' style='text-align: center'></th></tr></tfoot><tbody>");
                }
                $.each(listEditInputMaterial, function (index, record) {
                    tr = $('<tr/>');
                    tr.append("<td style='display: none'>" + this['ItemID'].toString().trim() + "</td>");//0
                    tr.append("<td style='display: none'>" + this['Code'].toString().trim() + "</td>");//1
                    tr.append("<td class='word-wrap text-wrap width-180'>" + this['Product'].toString().trim() + "</td>");//2
                    tr.append("<td class='word-wrap text-wrap width-50'>" + this['ProductType'].toString().trim() + "</td>");//3
                    tr.append("<td style='display: none'>" + this['UnitID'].toString().trim() + "</td>");//4
                    tr.append("<td class='word-wrap text-wrap width-60'>" + this['UOMDESCRIPTION'].toString().trim() + "</td>");//5
                    tr.append("<td class='word-wrap text-wrap width-60'>" + this['Qty'].toString().trim() + "</td>");//6
                    tr.append("<td class='word-wrap text-wrap width-60'>" + this['RefQty'].toString().trim() + "</td>");//7
                    tr.append("<td class='word-wrap text-wrap width-45' style='text-align: center'><input type='image' class='gvDestInputMaterialDelete'  id='btnDestInputMaterialdelete' <img src='../Images/ico_delete_16.png' title='Delete'/></input></td>")//8;
                    $("#DestinationMaterialInputGrid").append(tr);
                });
                tr.append("</tbody>");
                $('#DestinationMaterialInputGrid tbody tr').each(function () {
                    qty += parseFloat($(this).find('td:eq(6)').html().trim());
                    Refqty += parseFloat($(this).find('td:eq(7)').html().trim());
                });
                $('tfoot th#tfInputQty').html(qty.toFixed(4));
                $('tfoot th#tfInputRefQty').html(Refqty.toFixed(4));
            }
            /*Insput Material Destination End*/

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


