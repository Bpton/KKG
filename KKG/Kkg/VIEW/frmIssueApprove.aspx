<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmIssueApprove.aspx.cs" Inherits="VIEW_frmIssueApprove" %>

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
    <title></title>
</head>
<body>
    <form id="form1" runat="server" class="allDetails">
        <div class="container">
            <h2>Issue Approve</h2>
        </div>
        <fieldset id="fld" runat="server">
            <legend>Guidelines</legend>
            <p>
                <strong>Note :</strong>&nbsp;<span><i class="fa fa-folder"></i></span> Click on this button to edit tax<br />
                <strong>Note :</strong>&nbsp;<span><i class="fa fa-home"></i></span> Click on this button save edit tax
            </p>
        </fieldset>
        <div class="form-group">
            <label for="Taxname">Tax Name  </label>
            <br />
            <label class="lblTaxName"></label>
        </div>
        <div class="form-group">
            <label for="fromDate">From Date</label>
            <input type="text" id="fromDatePicker" />
            <label for="toDate">To Date</label>
            <input type="text" id="toDatePicker" />
        </div>
        <span class="btn btn-primary">
            <i class="fa fa-search"></i>
            <input type="button" value="Search" class="issueSearch" />
        </span>
        <div style="height: 50px">
            <div class="grid">
                <table id="tblTaxCategory" class="showData htmlTabel">
                    <thead>

                        <th colspan="8">
                            <span><i class="fa fa-search"></i></span>
                            <input type="text" id="myInput" onkeyup="myFunction()" style="color: black!important; width: 30%" placeholder="Search for category.." title="Type in a name">
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
