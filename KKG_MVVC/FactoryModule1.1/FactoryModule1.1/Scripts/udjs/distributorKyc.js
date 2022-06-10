/**/
var currentdt;
var frmdate;

var SOID;
var BSID;
var menuID;
$(document).ready(function () {
    var qs = getQueryStrings();
    if (qs["MENUID"] != undefined && qs["MENUID"] != "") {
        menuID = qs["MENUID"];
    }
    if (qs["USERID"] != undefined && qs["USERID"] != "") {
        SOID = qs["USERID"];
    }
    if (qs["USERTYPE"] != undefined && qs["USERTYPE"] != "") {
        BSID = qs["USERTYPE"];
    }
    loadDisttype();
 
    $("#txtanvdate").datepicker({
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        selectCurrent: true,
        todayBtn: "linked",
        showAnim: "slideDown",
        yearRange: "-100:+0",
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
    //$("#txtanvdate").val());


    $("#ddlotHercompaNydetailS").keydown(function (event) {
        if (event.ctrlKey == true && (event.which == '118' || event.which == '86')) {
            //alert(' not. PASTE!');
            event.preventDefault();
        }
    });
    $("#txtmcnroeturover").keydown(function (event) {
        if (event.ctrlKey == true && (event.which == '118' || event.which == '86')) {
            //alert(' not. PASTE!');
            event.preventDefault();
        }
    });
    $("#txttotalanualturnover").keydown(function (event) {
        if (event.ctrlKey == true && (event.which == '118' || event.which == '86')) {
            //alert(' not. PASTE!');
            event.preventDefault();
        }
    });
    $("#txtsaleperson").keydown(function (event) {
        if (event.ctrlKey == true && (event.which == '118' || event.which == '86')) {
            //alert(' not. PASTE!');
            event.preventDefault();
        }
    });
    $("#txtdeliveryperson").keydown(function (event) {
        if (event.ctrlKey == true && (event.which == '118' || event.which == '86')) {
            //alert(' not. PASTE!');
            event.preventDefault();
        }
    });

    $("#txtdateofbirth").datepicker({
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        selectCurrent: true,
        todayBtn: "linked",
        showAnim: "slideDown",
        yearRange: "-100:+0",
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
    //$("#txtanvdate").val(getCurrentDate());

    $("#ddlDIStype").change(function () {
        var type = $('#ddlDIStype').val();
        if (type == '0') {
            
            cleardata();
           
        }
        else {

            bindDistributorFromType(type, BSID);

        }
       
    })

    $("#txtdate").val(getCurrentDate())

    $("#ddldistributor").change(function () {
        debugger;
        $('body').append('<div style="" id="loadingDiv"><div class="loader">Loading Wait Please...</div></div>');
        var distribut = $("#ddldistributor").val();
        var type = $("#ddlDIStype").val();
        var mode = "DEPOT";
        if ($("#ddldistributor").val() == "" || $("#ddldistributor").val() == '0') {
            cleardata();
            setTimeout(removeLoader, 500);
            return;
        }
        loadCompany();
        bindDistributordepot(distribut, type, mode);
        bindDistributorstate(distribut, type, mode);
        bindDistributoraddress(distribut, type, mode);
        binDistributorUpdateDetails(distribut, type);
        //fetchCompanyDetails(distribut);
        setTimeout(removeLoader, 500);

    })

    $("#btnsavedetails").click(function () {
        $('body').append('<div style="" id="loadingDiv"><div class="loader">Loading Wait Please...</div></div>');
        if ($("#ddldistributor").val() == "" || $("#ddldistributor").val() == '0') {
            toastr.error("Please Select Distributor");
            setTimeout(removeLoader, 500);
            return;
        }
        InsertdistribuTorDetails();
        setTimeout(removeLoader, 500);
    })
    
    $('#btnnupload').click(function () {
        debugger;
       
        if ($("#ddldistributor").val() == "" || $("#ddldistributor").val() == null) {
            toastr.warning("Please Select Distributor for Upload file!")
            return;
        }
        else if ($("#txtchkupload").val() == "")
        {
            toastr.warning("Please Select file At least!")
            return;
        }
        else {
            // Checking whether FormData is available in browser  
            if (window.FormData !== undefined) {
              
                var distibid = $("#ddldistributor").val();
                var fileUpload = $("#txtchkupload").get(0);
                var files = fileUpload.files;
                // Create FormData object  
                var fileData = new FormData();
                fileData.append("distibid", distibid);


                // Looping over all files and add it to FormData object  
                for (var i = 0; i < files.length; i++) {
                    fileData.append(files[i].name, files[i]);
                }

                $.ajax({
                    url: "/distributorkyc/UploadFileInfo",
                    type: "POST",
                    contentType: false, // Not to set any content header  
                    processData: false, // Not to process data  
                    data: fileData,
                    success: function (response) {
                        debugger;
                        //var tableNew = '<table id="fileupload">';
                        //tableNew = tableNew + '<thead><tr><th>Counter</th> <th>File name </th><th>Download</th><th>Delete</th></thead><tbody>';
                        //var counter = 0;
                        toastr.info('File uploaded successfully');
                        //$.each(response, function () {
                        //    counter = counter + 1;
                        //    tableNew = tableNew + "<tr><td style='width:10%'>" + counter + "</td><td style='width:30%'>" + this["FILENAME"] + "</td><td style='width:20%'><a href='factorygrnMM' download='" + this["FILEPATH"] + "'><img src='../Images/download.png' width='20' height ='20' title='Download'/></a>  </td><td style='width:20%' ><input type='button' id='btndelfile'  class='clsfiledel' value='Del'/></td></tr > ";

                        //});
                        //document.getElementById("divuploadinfo").innerHTML = tableNew + '</tbody></table>';
                    },
                    error: function (err) {
                        toastr.error(err.statusText);
                    }
                });
            }
            else {
                toastr.error("FormData is not supported.");
            }
             }

        
    });

})

function isNumberKeyWithDot(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if ((charCode < 48 || charCode > 57) && (charCode != 8) && (charCode != 46))
        return false;
    return true;
}

function removeLoader() {
    $("#loadingDiv").fadeOut(500, function () {
        // fadeOut complete. Remove the loading div
        $("#loadingDiv").remove(); //makes page more lightweight 
    });
}

function loadDisttype() {
    $.ajax({
        type: "POST",
        url: "/distributorkyc/LoadDisttype",
        data: { USERID: SOID },
        async: false,
        success: function (result) {
            debugger
            var type = $("#ddlDIStype");
            var messagetext
            type.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(result, function () {
                type.val(this['DIST_TYPE']);
                type.append($("<option value=''></option>").val(this['DIST_TYPEID']).html(this['DIST_TYPE']));
            });
            type.chosen();
            type.trigger("chosen:updated");
            

        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function bindDistributorFromType(type) {
   // debugger
    $.ajax({
        type: "POST",
        url: "/distributorkyc/LoadDistributorFromType",
        data: { USERID: SOID, TYPE: type, BSID: BSID },
        async: false,
        success: function (result) {
            //alert(JSON.stringify(result));
            var distributor = $("#ddldistributor");
            distributor.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(result, function () {
                distributor.val(this['DISTRIBUTORNAME']);
                distributor.append($("<option value=''></option>").val(this['DISTRIBUTORID']).html(this['DISTRIBUTORNAME']));
            });
            distributor.chosen();
            distributor.trigger("chosen:updated");


        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function getCurrentDate() {
    today_date = new Date();
    today_Date_Str = ((today_date.getDate() < 10) ? "0" : "") + String(today_date.getDate()) + "/" + ((today_date.getMonth() < 9) ? "0" : "") + String(today_date.getMonth() + 1) + "/" + today_date.getFullYear();
    return today_Date_Str;
}


function bindDistributordepot(distribut, type, mode) {
   
        $.ajax({
            type: "POST",
            url: "/distributorkyc/bindDistributordepot",
            data: { USERID: SOID, TYPE: type, mode, distribut},
            async: false,
            success: function (result) {
              //  debugger;
                //alert(JSON.stringify(result));
                var distdepot = $("#ddldistdepot");
                distdepot.empty()
                    //.append('<option selected="selected" value="0">Please select</option>');
                $.each(result, function () {
                    distdepot.val(this['DEPOT_NAME']);
                    distdepot.append($("<option value=''></option>").val(this['DEPOTID']).html(this['DEPOT_NAME']));
                });
                distdepot.chosen();
                distdepot.trigger("chosen:updated");


            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
}

function bindDistributorstate(distribut, type, mode) {
   // debugger;
    var mode = "STATE";
    $.ajax({
        type: "POST",
        url: "/distributorkyc/bindDistributorstate",
        data: { USERID: SOID, TYPE: type, mode ,distribut },
        async: false,
        success: function (result) {
           
            //alert(JSON.stringify(result));
            var diststate = $("#ddlstate");
            //diststate.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(result, function () {
                diststate.val(this['STATE_NAME']);
                diststate.append($("<option value=''></option>").val(this['STATEID']).html(this['STATE_NAME']));
            });
            diststate.chosen();
            diststate.trigger("chosen:updated");


        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function bindDistributoraddress(distribut, type, mode) {
    debugger;
    var mode = "ADDRESS";
    $.ajax({
        type: "POST",
        url: "/distributorkyc/bindDistributorAddress",
        data: { USERID: SOID, TYPE: type, mode, distribut },
        async: false,
        success: function (result) {
            debugger;
            $("#txtaddressone").val(result[0].DIST_ADDRESS)
            $("#txtaddresS").val(result[0].ADDRESS2)
            $("#txtconPerson").val(result[0].CONTACTPERSON)
            $("#txtdistcode").val(result[0].DIST_CODE)
            $("#txtdateofbirth").val(result[0].DOB)
            $("#txtanvdate").val(result[0].ANVDATE)
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function loadCompany() {
    $.ajax({
        type: "POST",
        url: "/distributorkyc/loadcompanyDetails",
        data: {  },
        async: false,
        success: function (result) {
            var type = $("#ddlcompaNydetailS");
           
            $.each(result, function () {
                type.val(this['OTHERCOMPANYNAME']);
                type.append($("<option value=''></option>").val(this['OTHERCOMPANYID']).html(this['OTHERCOMPANYNAME']));
            });
            $('#ddlcompaNydetailS').multiselect({
                columns: 1, placeholder: 'Select Company',
                includeSelectAllOption: true,
            
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

function InsertdistribuTorDetails() {

    try {
      
      
        if ($("#txtconpersonT").val() == "") {
            $("#txtconpersonT").val("NA");
        }
        if ($("#txtconPerson").val() == "") {
            $("#txtconPerson").val("NA");
        }
        if ($("#txtaddressone").val() == "") {
            $("#txtaddressone").val("NA");
        }
        if ($("#txtaddresS").val() == "") {
            $("#txtaddresS").val("NA");
        }

        var companyDetails = $("#ddlcompaNydetailS option:selected");
        var companyDetails1 = "";
        companyDetails.each(function () {
            companyDetails1 += $(this).val() + ',';
        });
        var res = companyDetails1.substring(0, companyDetails1.length - 1);

        var detail = {};
        detail.DISTRIBUTORID = $("#ddldistributor").val();
        detail.DISTRIBUTORNAME = $("#ddldistributor").find('option:selected').text();
        detail.DIST_CODE = $("#txtdistcode").val();
        detail.DIST_TYPEID = $("#ddlDIStype").val();
        detail.DIST_TYPE = $("#ddlDIStype").find('option:selected').text();
        detail.CONTACTPERSON = $("#txtconPerson").val();
        detail.CONTACTPERSON2 = $("#txtconpersonT").val();
        detail.EMAILID1 = $('#txtemaiL').val();
        debugger;
        detail.DIST_ADDRESS = $("#txtaddressone").val();
        detail.ADDRESS2 = $("#txtaddresS").val();
        detail.CITYNAME = $("#txtcitY").val();
        detail.STATEID = $("#ddlstate").val();
        detail.STATE_NAME = $("#ddlstate").find('option:selected').text();

        detail.MOBILE2 = $("#txtwhatasapP").val();
        detail.OTHERS_COMPANY = $("#ddlotHercompaNydetailS").val();
        detail.REMARKS = $("#textremarks").val();

        detail.DOB = $("#txtdateofbirth").val();
        detail.ANVDATE = $("#txtanvdate").val();
        detail.Total_Godown_Size = $("#txttotalgsize").val();
        detail.Godown_Size_McNROE = $("#txtgsizeformcn").val();
        detail.No_Sales_Person = $("#txtsaleperson").val();
        detail.No_Delivery_Person = $("#txtdeliveryperson").val();
        detail.Total_Annual_Turnover_Lakh = $("#txttotalanualturnover").val();
        detail.McNROE_Turnover_Lakh = $("#txtmcnroeturover").val();

        detail.OTHERCOMPANYID = res;

        $.ajax({
            url: "/distributorkyc/InsertdistribuTorDetails",
            data: '{detail:' + JSON.stringify(detail) + '}',
            type: "POST",
            async: false,
            contentType: "application/json",
            success: function (responseMessage) {

                var messageid;
                var messagetext;
                $.each(responseMessage, function (key, item) {
                    messagetext = item.response;
                });

                toastr.success('<b><font color=black>' + messagetext + '</font></b>');
                if (messagetext != '0') {
                    cleardata();
                }
                else {

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
    catch (ex) {
        alert(ex);
    }
}

function cleardata() {
    $("#ddlstate").val('');
    $("#ddldistributor").val('0');
    $("#ddldistdepot").empty();
    //$("#ddlDIStype").empty();
    $("#ddlcompaNydetailS").val('');
    $("#ddlotHercompaNydetailS").val('');
    $("#txtdistcode").val('');
    $("#txtconPerson").val('');
    $("#txtconpersonT").val('');
    $("#txtaddressone").val('');
    $("#txtaddresS").val('');
    $("#txtcitY").val('');
    $("#txtpincodE").val('');
    $("#txtemaiL").val('');
    $("#txtwhatasapP").val('');
    $("#textremarks").val('');
    $("#txtdateofbirth").val('');
    $("#txtanvdate").val('');
    $("#txttotalgsize").val('');
    $("#txtgsizeformcn").val('');
    $("#txtdeliveryperson").val('');
    $("#txtsaleperson").val('');
    $("#txttotalanualturnover").val('');
    $("#txtmcnroeturover").val('');
    $("#txtchkupload").val('');


    $("#ddldistributor").chosen();
    $("#ddldistributor").trigger("chosen:updated");

    $("#ddldistdepot").chosen();
    $("#ddldistdepot").trigger("chosen:updated");

    $("#ddlstate").chosen();
    $("#ddlstate").trigger("chosen:updated");
    
}

function binDistributorUpdateDetails(distribut, type) {
    var mode = "UPDATE"
    $.ajax({
        type: "POST",
        url: "/distributorkyc/bindDistributorKycDetails",
        data: { distribut, type, mode},
        async: false,
        success: function (result) {
            debugger;
            var conpersonTwo = $("#txtconpersonT");
            var city = $("#txtcitY");
            var pinCode = $("#txtpincodE");
            var emaiL = $("#txtemaiL");
            var whatasappNumber = $("#txtwhatasapP");
            var remarks = $("#textremarks");
            var total_godownsize = $("#txttotalgsize");
            var total_McNROE_godownsize = $("#txtgsizeformcn");
            var salesperson = $("#txtsaleperson");
            var deliveryperson = $("#txtdeliveryperson");
            var total_turnover_lakh = $("#txttotalanualturnover");
            var total_Mcnroe_turnover = $("#txtmcnroeturover");
            var OTHERS_COMPANY = $("#ddlotHercompaNydetailS")
            $.each(result, function () {
                conpersonTwo.val(this['CONTACTPERSON2']);
                city.val(this['CITYNAME']);
                pinCode.val(this['PIN']);
                emaiL.val(this['EMAILID1']);
                whatasappNumber.val(this['MOBILE2']);
                remarks.val(this['REMARKS']);
                total_godownsize.val(this['Total_Godown_Size']);
                total_McNROE_godownsize.val(this['Godown_Size_McNROE']);
                salesperson.val(this['No_Sales_Person']);
                deliveryperson.val(this['No_Delivery_Person']);
                total_turnover_lakh.val(this['Total_Annual_Turnover_Lakh']);
                total_Mcnroe_turnover.val(this['McNROE_Turnover_Lakh']);
                OTHERS_COMPANY.val(this['OTHERS_COMPANY']);

               
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

function fetchCompanyDetails(distribut) {
    $.ajax({
        type: "POST",
        url: "/distributorkyc/loadcompanyDetailsForddl",
        data: { distribut},
        async: false,
        success: function (result) {
            debugger
            //var company;
            //$("#ddlcompaNydetailS").html("");
            //$('#ddlcompaNydetailS').multiselect('refresh');
            var companyId;
            for (var i = 0; i < result.length; i++) {
                
                companyId = result[i].COMPANY_ID;
            }
            var _companyId = companyId.split(',');

            $.each(_companyId.split(","), function (i, e) {
                $("#ddlcompaNydetailS option[value='" + e + "']").prop("selected", true);
            });
            ////var _company = company.split(",");
            //debugger;
            //$("#ddlcompaNydetailS").val(_companyId).trigger("chosen:updated");
            ////$("#ddlcompaNydetailS").val(_companyId).trigger("chosen:updated");
            //$("#ddlcompaNydetailS").html("");
            //$('#ddlcompaNydetailS').multiselect('refresh');

            //var type = $("#ddlcompaNydetailS");
            ////type.empty().append('<option selected="selected" value="0">Please select</option>');
            //$.each(result, function () {
            //    //type.val(this['COMPANY_NAME']);
            //    type.append($("<option value=''></option>").val(this['COMPANY_ID']).html(this['COMPANY_NAME']));
            //});
            //$('#ddlcompaNydetailS').multiselect('rebuild');
            //$("#ddlcompaNydetailS").multiselect('selectAll', true);
            //$("#ddlcompaNydetailS").multiselect('updateButtonText');

          


          

            //var values = new Array();
            //$(this).multiselect("getChecked").each(function (index, item) {
            //    values.push($(item).val());
            //});
            //$("#ddlcompaNydetailS").val(values.join(","));

            //for (var i in myList) {
            //    var optionVal = myList[i];
            //    $("#ddlcompaNydetailS").find("option[value=" + optionVal + "]").prop("selected", "selected");
            //}
           // $("#ddlcompaNydetailS").multiselect('reload');
        }
    })      
}  

function getCurrentDate() {
    today_date = new Date();
    today_Date_Str = ((today_date.getDate() < 10) ? "0" : "") + String(today_date.getDate()) + "/" + ((today_date.getMonth() < 9) ? "0" : "") + String(today_date.getMonth() + 1) + "/" + today_date.getFullYear();
    return today_Date_Str;
}


function getQueryStrings() {
    try {
        var assoc = {};
        var decode = function (s) { return decodeURIComponent(s.replace(/\+/g, " ")); };
        var queryString = location.search.substring(1);
        var keyValues = queryString.split('&');

        for (var i in keyValues) {
            var key = keyValues[i].split('=');
            if (key.length > 1) {
                assoc[decode(key[0])] = decode(key[1]);
            }
        }
        return assoc;
    }
    catch (ex) {
        swal("", "Some problem occurred please try again later", "info");
    }
}