<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmTaxCategoryMapping.aspx.cs" Inherits="VIEW_frmTaxCategoryMapping" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js" type="text/javascript"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/gijgo/1.9.13/modular/css/grid.min.css" type="text/javascript"></script>
    <link href="https://code.jquery.com/ui/1.10.4/themes/ui-lightness/jquery-ui.css" rel="stylesheet">
    <script src="https://code.jquery.com/jquery-1.10.2.js"></script>
    <script src="https://code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
    <script src="https://getbootstrap.com/docs/3.3/components/"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css">
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@10/dist/sweetalert2.all.min.js"></script>
     <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css" integrity="sha512-c42qTSw/wPZ3/5LBzD+Bw5f7bSF2oxou6wEb+I/lqeaKV5FDIfMvvRp772y4jcJLKuGUOpbJMdg/BTl50fJYAw==" crossorigin="anonymous" referrerpolicy="no-referrer" />
   <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/limonte-sweetalert2/11.1.9/sweetalert2.min.css" integrity="sha512-cyIcYOviYhF0bHIhzXWJQ/7xnaBuIIOecYoPZBgJHQKFPo+TOBA+BY1EnTpmM8yKDU4ZdI3UGccNGCEUdfbBqw==" crossorigin="anonymous" referrerpolicy="no-referrer" />
   <script src="https://cdnjs.cloudflare.com/ajax/libs/limonte-sweetalert2/11.1.9/sweetalert2.all.min.js" integrity="sha512-IZ95TbsPTDl3eT5GwqTJH/14xZ2feLEGJRbII6bRKtE/HC6x3N4cHye7yyikadgAsuiddCY2+6gMntpVHL1gHw==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>

    <script src="../js/lobibox.js"></script>
    <link href="../js/lobibox.min.css" rel="stylesheet" />
    <style>
        th {
            background: cornflowerblue !important;
            color: white !important;
            position: sticky !important;
            top: 0;
            box-shadow: 0 2px 2px -1px rgb(255, 106, 0);
            width: 400px;
        }

        th, td {
            padding: 0.25rem;
        }

        .ChKInsertUpdate {
            visibility: hidden;
        }

        .btnSelect {
            background-color: DodgerBlue; /* Blue background */
            border: none; /* Remove borders */
            color: white; /* White text */
            padding: 12px 16px; /* Some padding */
            font-size: 16px; /* Set a font size */
            cursor: pointer; /* Mouse pointer on hover */
        }

        .btnUpdate {
            background-color: #00ff21; /* gree background */
            border: none; /* Remove borders */
            color: white; /* White text */
            padding: 12px 16px; /* Some padding */
            font-size: 16px; /* Set a font size */
            cursor: pointer; /* Mouse pointer on hover */
        }

        .HighlghtLabel {
            background-color: grey; /* grey background */
            border: none; /* Remove borders */
            color: black; /* White text */
            padding: 12px 16px; /* Some padding */
            font-size: 16px; /* Set a font size */
            cursor: pointer; /* Mouse pointer on hover */
        }


        * {
            box-sizing: border-box;
        }

        #myInput {
            background-image: url('/css/searchicon.png');
            background-position: 10px 10px;
            background-repeat: no-repeat;
            width: 100%;
            font-size: 16px;
            padding: 12px 20px 12px 40px;
            border: 1px solid #ddd;
            margin-bottom: 12px;
        }

        .htmlTabel {
            border-collapse: collapse;
            width: 100%;
            border: 1px solid #ddd;
            font-size: 18px;
        }

            .htmlTabel th, #myTable td {
                text-align: left;
                padding: 12px;
            }

            .htmlTabel tr {
                border-bottom: 1px solid #ddd;
            }

                .htmlTabel tr.header, .htmlTabel tr:hover {
                    background-color: #f1f1f1;
                }
    </style>
    <script type="text/javascript">


        let TAXID = "";
        let TAXNAME = "";
        $(document).ready(function () {

            var qs = getQueryStrings();
            if (qs["TaxId"] != undefined && qs["TaxId"] != "") {
                TAXID = qs["TaxId"].trim();
            }
            if (qs["TaxName"] != undefined && qs["TaxName"] != "") {
                TAXNAME = qs["TaxName"].trim();
            }
            var errnet = navigator.onLine;
            if (errnet != true) {
                alert("no internet");
                $('.allDetails').hide();
                return;
            }
            debugger;
            $('.lblTaxName').html(TAXNAME);
            bindData();
        })

        function MarKRecord(e) {
            if (($('#fromDatePicker').val() == "" || $('#fromDatePicker').val() == null) || ($('#toDatePicker').val() == "" || $('#toDatePicker').val() == null)) {
                Swal.fire({
                    icon: 'Warning',
                    title: 'Warning',
                    //showCancelButton: true,
                    //confirmButtonClass: "btn-danger",
                    text: 'Please select date for further process',
                    /*  footer: '<a href="../frmDashBordContent.aspx">Go to dashboard</a>',*/
                    timer: 3000,
                    timerProgressBar: true
                })
                return;
            }

            debugger;
            $(e).parents('tr').find('.ChKInsertUpdate').removeClass("ChKInsertUpdate");
            $(e).parents('tr').find('.ChkTax').hide();
            $(e).parents('tr').find('.percentage').removeAttr("disabled");

        }

        function resetControl(e) {
            $(e).parents('tr').find('.btnUpdate').addClass("ChKInsertUpdate");
            $(e).parents('tr').find('.ChkTax').show();
            $(e).parents('tr').find('.percentage').attr("disabled", "disabled");
        }
       


        function MarKRecordUpdate(e) {

            let taxid;
            let catid;
            let percentage = 0;
            let fromdate;
            let todate;

            taxid = TAXID;
            catid = $(e).parents('tr').find('.catId').attr('data-catid');
            percentage = $(e).parents('tr').find('.percentage').val();
            fromdate = $('#fromDatePicker').val();
            todate = $('#toDatePicker').val();
            try {
                $.ajax({
                    type: "POST",
                    url: "frmTaxCategoryMapping.aspx/updateTax",
                    data: '{TaxId: "' + taxid + '",catid: "' + catid + '",percentage: "' + percentage + '",fromdate: "' + fromdate + '",todate: "' + todate + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    success: function (msg) {
                        var json = JSON.parse(msg.d);
                        $.each(json, function (index, obj) {
                            obj.Messageid;
                            if (obj.MessageText ="Done")
                            Swal.fire({
                                icon: 'success',
                                title: 'success',
                                text: 'Tax mapping done,Click "Ok" to continue',
                                /*  footer: '<a href="../frmDashBordContent.aspx">Go to dashboard</a>',*/
                                timer: 3000,
                                timerProgressBar: true
                            })
                        });
                    }
                });
                resetControl(e);
            }
            catch (ex) {
                alert(ex);
            }
        }


        function bindData() {
            debugger;
           
            let slno = 0;
            let PERCENTAGE = 0;
            try {
                $.ajax({
                    type: "POST",
                    url: "frmTaxCategoryMapping.aspx/Main",
                    data: '{TaxId: "' + TAXID + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    success: function (msg) {
                        var json = JSON.parse(msg.d);
                        $.each(json, function (index, obj) {
                            slno = slno + 1;
                            PERCENTAGE = isNaN(obj.PERCENTAGE) ? '0.00' : obj.PERCENTAGE;
                          

                            var row = '<tr><td><button   onclick="MarKRecord(this);return false;"  class="ChkTax btnSelect"> <i class="fa fa-folder"></i></button><button onclick="MarKRecordUpdate(this);return false;" class="ChKInsertUpdate btnUpdate"><i class="fa fa-home"></i></button></td><td class="slno"> ' + slno + ' </td> <td data-catid="' + obj.CATID + '" class="catId"> ' + obj.CATNAME + ' </td> <td class="hsn">' + obj.HSN + '</td><td> ' +
                                '<input type="text" disabled="disabled" class="percentage"  value=' + PERCENTAGE.toFixed(2) + '></td><td class="fromdate">' + obj.FROMDATE + '</td> <td class="todate">' + obj.TODATE + '</td> </tr>'
                            $("#tblTaxCategory tbody").append(row);
                        });
                    }
                });
            }
            catch (ex) {
                alert(ex);
            }
        }

        $(function () {
            $("#fromDatePicker").datepicker({
                dateFormat: "dd-mm-yy",
                altField: "#datepick-2",
                altFormat: "DD, d MM, yy"
            });
        });
        $(function () {
            $("#toDatePicker").datepicker({
                dateFormat: "dd-mm-yy",
                altField: "#datepick-2",
                altFormat: "DD, d MM, yy"
            });
        });

        function myFunction() {
            var input, filter, table, tr, td, i, txtValue;
            input = document.getElementById("myInput");
            filter = input.value.toUpperCase();
            table = document.getElementById("tblTaxCategory");
            tr = table.getElementsByTagName("tr");
            for (i = 0; i < tr.length; i++) {
                td = tr[i].getElementsByTagName("td")[2];
                if (td) {
                    txtValue = td.textContent || td.innerText;
                    if (txtValue.toUpperCase().indexOf(filter) > -1) {
                        tr[i].style.display = "";
                    } else {
                        tr[i].style.display = "none";
                    }
                }
            }
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

    </script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server" class="allDetails">
        <div class="container">
            <h2>Tax Category Mapping</h2>
        </div>
        <fieldset id="fld" runat="server">
            <legend>Guidelines</legend>
            <p>
                <strong>Note :</strong>&nbsp;<span><i class="fa fa-folder"></i></span> Click on this button to edit tax<br />
                <strong>Note :</strong>&nbsp;<span><i class="fa fa-home"></i></span> Click on this button save edit tax
            </p>
        </fieldset>
        <div class="form-group">
            <label for="Taxname">Tax Name  </label><br />
            <label class="lblTaxName"></label>
        </div>
        <div class="form-group">
            <label for="fromDate">From Date</label>
            <input type="text" id="fromDatePicker" />
            <label for="toDate">To Date</label>
            <input type="text" id="toDatePicker" />
        </div>
      <%--  <div class="HighlghtLabel">
            <span><i class="fa fa-search"></i></span>
        <input type="text" id="myInput" onkeyup="myFunction()" style="width:30%" placeholder="Search for category.." title="Type in a name">
            </div>--%>
        <div style="height:50px">
        <div class="grid" >
            <table id="tblTaxCategory"  class="showData htmlTabel">
                <thead>
                    
                    <th colspan="8">
                         <span><i class="fa fa-search"></i></span>
        <input type="text" id="myInput" onkeyup="myFunction()" style="color: black!important; width:30%" placeholder="Search for category.." title="Type in a name">
                        </th>
                        
                    </thead>
                <thead>
                    <th>#
                    </th>
                    <th>slno
                    </th>
                    <th>Catname
                    </th>
                    <th>Hsn
                    </th>
                    <th>Percentage
                    </th>
                    <th>Fromdate
                    </th>
                    <th>Todate
                    </th>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
            </div>
    </form>
</body>
</html>
