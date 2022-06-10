<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true"
    CodeFile="SalesTaxSummary_Details_GST_Report.aspx.cs" Inherits="VIEW_SalesTaxSummary_Details_GST_Report" ClientIDMode="Static" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/bootstrap-multiselect.css" rel="stylesheet" />
    <link href="../css/gijgo.min.css" rel="stylesheet" type="text/css" />
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <style>
        #gridSummaryreport tfoot {
            float: left !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="contentarea">
        <div class="grid_container">
            <div class="grid_12">
                <div class="widget_wrap">
                    <div class="widget_top active" id="trBtn" runat="server">
                        <span class="h_icon_he list_images"></span>
                        <h6>Sales Tax Summary Details
                        </h6>
                    </div>
                    <div class="widget_content">
                        <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                            <tr>
                                <td>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td width="90" class="field_title">
                                                <asp:Label ID="Label1" Text="From Date" runat="server"></asp:Label>
                                                <span class="req">*</span>
                                            </td>
                                            <td width="200" class="field_input">
                                                <%-- <asp:TextBox ID="txtfromdate" runat="server" Width="120" placeholder="dd/MM/yyyy"
                                                    MaxLength="10" ValidationGroup="sales"></asp:TextBox>--%>
                                                <input id="txtfromdate" type="text" placeholder="dd/MM/yyyy" style="width: 150px;" readonly />
                                            </td>
                                            <td width="70" class="field_title">
                                                <asp:Label ID="Label2" Text="To Date" runat="server"></asp:Label>
                                                <span class="req">*</span>
                                            </td>
                                            <td width="190" class="field_input">
                                                <%-- <asp:TextBox ID="txttodate" runat="server" Width="120" placeholder="dd/MM/yyyy"
                                                    MaxLength="10" ValidationGroup="sales"></asp:TextBox>--%>
                                                <input id="txttodate" type="text" placeholder="dd/MM/yyyy" style="width: 150px;" readonly />
                                            </td>
                                            <td class="field_title" width="125">
                                                <asp:Label ID="Label7" runat="server" Text="Business Segment"></asp:Label>
                                            </td>
                                            <td class="field_input" width="190">
                                                <asp:DropDownList ID="ddlbsegment" Width="150" runat="server" class="chosen-select"
                                                    data-placeholder="Select Business Segment" AppendDataBoundItems="True">
                                                </asp:DropDownList>
                                            </td>
                                            <td width="70" class="field_title">
                                                <asp:Label ID="lbltypeinvoice" Text="Type" runat="server"></asp:Label>
                                            </td>
                                            <td width="150" class="field_input">
                                                <asp:DropDownList ID="ddltype_invoice" runat="server" AppendDataBoundItems="true"
                                                    Width="150" Height="28" class="chosen-select" data-placeholder="Choose a Type">
                                                    <asp:ListItem Text="All" Value="1" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Invoice" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Transfer" Value="3"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="70" class="field_title">
                                                <asp:Label ID="Label5" Text="Depot" runat="server"></asp:Label>
                                                <span class="req">*</span>
                                            </td>
                                            <td width="150" class="field_input">
                                                <asp:ListBox ID="ddldepot" runat="server" SelectionMode="Multiple" TabIndex="4" AppendDataBoundItems="True"
                                                    ValidationGroup="sales"></asp:ListBox>
                                            </td>
                                            <td width="70" class="field_title">
                                                <asp:Label ID="Label10" Text="Party" runat="server"></asp:Label>
                                                <span class="req">*</span>
                                            </td>
                                            <td width="150" class="field_input">
                                                <asp:ListBox ID="ddlparty" runat="server" SelectionMode="Multiple" TabIndex="4" AppendDataBoundItems="True"
                                                    ValidationGroup="sales"></asp:ListBox>
                                            </td>
                                            <td width="70" class="field_title" style="display: none">
                                                <asp:Label ID="Label3" Text="Scheme" runat="server"></asp:Label>
                                            </td>
                                            <td width="200" class="field_input" style="display: none">
                                                <asp:DropDownList ID="ddlscheme" Width="140" runat="server" class="chosen-select"
                                                    data-placeholder="Select" AppendDataBoundItems="True">
                                                    <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="No" Selected="True"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td width="70" class="field_title">
                                                <asp:Label ID="Label8" Text="Invoice Type" runat="server"></asp:Label>
                                            </td>
                                            <td width="200" class="field_input">
                                                <asp:DropDownList ID="ddltype" Width="140" runat="server" class="chosen-select" data-placeholder="Select"
                                                    AppendDataBoundItems="True">
                                                    <asp:ListItem Value="0" Text="ALL" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Sale Invoice"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Sales Return"></asp:ListItem>

                                                </asp:DropDownList>
                                            </td>
                                            <td width="70" class="field_title" style="display: none;">
                                                <asp:Label ID="Label9" Text="Tax Type" runat="server"></asp:Label>
                                            </td>
                                            <td width="200" class="field_input" style="display: none;">
                                                <asp:ListBox ID="ddltaxtype" runat="server" SelectionMode="Multiple" TabIndex="4"
                                                    AppendDataBoundItems="True" ValidationGroup="sales">
                                                    <asp:ListItem Value="2" Text="Central"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="State"></asp:ListItem>
                                                </asp:ListBox>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="70" class="field_title">
                                                <asp:Label ID="Label4" Text="Group By" runat="server"></asp:Label>
                                            </td>
                                            <td width="200" class="field_input">
                                                <asp:DropDownList ID="ddlgroupby" Width="140" runat="server" class="chosen-select"
                                                    data-placeholder="Select" AppendDataBoundItems="True">
                                                    <asp:ListItem Value="1" Text="Date" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Party"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td width="70" class="field_title">
                                                <asp:Label ID="Label6" Text="Details" runat="server"></asp:Label>
                                            </td>
                                            <td width="200" class="field_input">
                                                <asp:DropDownList ID="ddldetails" Width="140" runat="server" class="chosen-select"
                                                    data-placeholder="Select" AppendDataBoundItems="True">
                                                    <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="No" Selected="True"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td width="70" class="field_title">
                                                <asp:Label ID="Label11" Text="Invoice Status" runat="server"></asp:Label>
                                            </td>
                                            <td width="200" class="field_input">
                                                <asp:DropDownList ID="ddlinvoicetype" Width="140" runat="server" class="chosen-select"
                                                    data-placeholder="Select" AppendDataBoundItems="True">
                                                    <asp:ListItem Value="0" Text="ALL" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Normal"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Cancelled"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td width="150" class="field_input">
                                                <div class="btn_24_blue">
                                                    <span class="icon exclamation_co"></span>
                                                    <button type="button" id="btnShowData" class="btn_link">Show Report</button>
                                                </div>
                                            </td>
                                            <td width="150" class="field_input">
                                                <div id="btnexportDiv" class="btn_30_light">
                                                    <span class="icon doc_excel_table_co"></span>
                                                    <button type="button" id="btnExportExcel" class="btn_link">Export Excel</button>
                                                    <%-- <asp:Button ID="btnExportExcel" runat="server" Text="Export Excel"
                                                            CssClass="btn_link" OnClientClick="ShowProgress();" />--%>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>

                        <div class="gridcontent" style="overflow-x: auto;">
                            <table id="gridSummaryreport"></table>
                        </div>
                    </div>
                </div>
            </div>
            <span class="clear"></span>
        </div>
        <span class="clear"></span>
    </div>

    <div class="modal fade" id="loader" tabindex="-1" role="dialog" aria-labelledby="loaderLabel" aria-hidden="true">
        <div class="modal-dialog modal-sm modal-msg">
            <div class="modal-content">
                <div class="modal-header hidden">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title" id="loaderLabel">Loading</h4>
                </div>
                <div class="modal-body">
                    <i class="fa fa-refresh fa-spin fa-3x text-center"></i>
                    <h3 class="text-center">Loading</h3>
                </div>
            </div>
        </div>
    </div>

    <div id="error-modal" title="Error" style="display: none;">
        There was a problem generating your report, please re-login and try again.
    </div>

    <script type="text/javascript" src="../js/bootstrap.js"></script>
    <script type="text/javascript" src="../js/bootstrap-multiselect.js"></script>
    <script type="text/javascript" src="../js/gijgo.min.js"></script>
    <script type="text/javascript" src="../js/jquery.fileDownload.js"></script>

    <script type="text/javascript">

        $(document).ready(function () {
            $('#btnExportExcel').prop('disabled', true);
            $('#btnexportDiv').removeClass("btn_24_blue");
            $('#btnexportDiv').addClass("btn_30_light");

             var finyear = '<%=Session["FINYEAR"]%>';
            var startyear = finyear.substring(0, 4);
            var endyear = finyear.substring(5, 9);
            var yearstart = "";
            var todayDate = "";
            var todayDate1 = "";
            var today = "";
            var today1 =new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate());
            yearstart = new Date(startyear, 03, 01);
             
           
            if (today1 >= new Date(startyear, 03, 01) && today1 <= new Date(endyear, 02, 31)) {
                todayDate = ("0" + new Date().getDate()).slice(-2) + "/" + ("0" + (new Date().getMonth() + 1)).slice(-2) + "/" + new Date().getFullYear();
                todayDate1 = ("0" + new Date().getDate()).slice(-2) + "/" + ("0" + (new Date().getMonth() + 1)).slice(-2) + "/" + new Date().getFullYear();
                today = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate());
            }
            else {
                todayDate = ("0" + 1 + "/"+ "0" +4+ "/" + startyear);
                todayDate1 = (31 + "/" + "0" + 3 + "/" + endyear);
                today = new Date(endyear, 02, 31);
            }
           

            $('#txtfromdate').datepicker({
                format: 'dd/mm/yyyy',
                uiLibrary: 'bootstrap4',
                iconsLibrary: 'fontawesome',
                minDate: yearstart,
                value: todayDate,
                maxDate: function () {
                   return $('#txttodate').val();
                }
            });

            $('#txttodate').datepicker({
                format: 'dd/mm/yyyy',
                uiLibrary: 'bootstrap4',
                iconsLibrary: 'fontawesome',
                value: todayDate1,
                minDate: function () {
                    return $('#txtfromdate').val();
                },
                maxDate: today
            });

            $('#ddldepot').multiselect({
                includeSelectAllOption: true
            });
            $("#ddldepot").multiselect('selectAll', false);
            $("#ddldepot").multiselect('updateButtonText');

            $('#ddltaxtype').multiselect({
                includeSelectAllOption: true
            });
            $("#ddltaxtype").multiselect('selectAll', false);
            $("#ddltaxtype").multiselect('updateButtonText');

            $('#ddlparty').multiselect({
                includeSelectAllOption: true
            });
            $("#ddlparty").multiselect('selectAll', false);
            $("#ddlparty").multiselect('updateButtonText');

        });

        $('#btnShowData').click(function (event) {
            var data = PrepareJSONData("Show Report");
            var groupingName = $("#ddlgroupby option:selected").text().toUpperCase();;
            $.ajax({
                url: "<%=ServiceURL%>CommonServices.asmx/GetSalesTaxGSTReport",
                type: 'POST',
                data: data,
                contentType: 'application/json;',
                dataType: 'json',
                async: true,
                success: function (result) {
                    if (result != null) {
                        if (result.d.ParentData.length > 0) {
                            $('#btnExportExcel').prop('disabled', false);
                            $('#btnexportDiv').removeClass("btn_30_light");
                            $('#btnexportDiv').addClass("btn_24_blue");
                        }
                        else {
                            $('#btnExportExcel').prop('disabled', true);
                            $('#btnexportDiv').addClass("btn_30_light");
                            $('#btnexportDiv').removeClass("btn_24_blue");
                        }
                        BindGrid(result, groupingName);
                        $("#loader").fadeOut(200, function () { $("#loader").modal('hide'); })

                    }
                    else {
                        $('#btnExportExcel').prop('disabled', true);
                        $('#btnexportDiv').addClass("btn_30_light");
                        $('#btnexportDiv').removeClass("btn_24_blue");
                    }
                },
                error: function (result) {
                    $("#loader").fadeOut(200, function () { $("#loader").modal('hide'); })
                    $("#error-modal").dialog({ modal: true });
                }
            });
        });

        $('#btnExportExcel').click(function (event) {
            $("#loader").fadeIn(500).modal('show');
            $.ajax({
                url: "<%=ServiceURL%>CommonServices.asmx/GetSalesTaxGSTReportinExcel",
                type: 'POST',
                contentType: 'application/json;',
                dataType: 'json',
                async: true,
                success: function (result) {
                    $("#loader").fadeOut(200, function () { $("#loader").modal('hide'); })
                    $.fileDownload(result.d);
                },
                error: function (result) {
                    $("#loader").fadeOut(200, function () { $("#loader").modal('hide'); })
                    $("#error-modal").dialog({ modal: true });
                }
            });
        });

        <%--$('#ddlgroupby').change(function (event) {
            var data = PrepareJSONData("Show Grouping");
            var groupingName = $(this).text().toUpperCase();
            $.ajax({
                url: "<%=ServiceURL%>CommonServices.asmx/GetSalesTaxGSTReport",
                type: 'POST',
                data: data,
                contentType: 'application/json;',
                dataType: 'json',
                async: true,
                success: function (result) {
                    if (result != null) {
                        BindGrid(result, groupingName);
                        $("#loader").fadeOut(200, function () { $("#loader").modal('hide'); })

                    }
                    else {
                        $("#loader").fadeOut(200, function () { $("#loader").modal('hide'); })
                    }
                },
                error: function (result) {
                    $("#loader").fadeOut(200, function () { $("#loader").modal('hide'); })
                }
            });
        });--%>

        function BindGrid(result, groupingName) {
            var grid = $('#gridSummaryreport').grid({
                dataSource: result.d.ParentData,
                primaryKey: 'VCH NO.',
                selectionType: 'checkbox',
                uiLibrary: 'bootstrap',
                columns: [
                    { field: 'VCH NO.', title: 'VCH NO.', width: 150 },
                    { field: 'DATE', title: 'DATE', width: 150, type: 'date', format: 'dd/mm/yyyy'},
                    { field: 'DEPOT', title: 'DEPOT', width: 200 },
                    { field: 'PARTY', title: 'PARTY', width: 200 },
                    { field: 'VOUCHER TYPE', title: 'VOUCHER TYPE', width: 150 },
                    { field: 'STATUS', title: 'STATUS', width: 150 },
                    { field: 'GSTIN NO.', title: 'GSTIN NO.', width: 200 },
                    { field: 'TOTAL PCS', title: 'TOTAL PCS', width: 120 },
                    { field: 'BASIC AMT', title: 'BASIC AMT', width: 120 },
                    { field: 'SCHEME', title: 'SCHEME', width: 120 },
                    { field: 'SP. DISC.', title: 'SP. DISC.', width: 120 },
                    { field: 'ADDL. SS MARGIN', title: 'ADDL. SS MARGIN', width: 120 },
                    { field: 'GROSS AMT', title: 'GROSS AMT', width: 120 },
                    { field: '0% SALE', title: '0% SALE.', width: 120 },
                    { field: 'EXPORT', title: 'EXPORT', width: 120 },
                    { field: '18.0% CENTRAL SALE', title: '18.0% CENTRAL SALE', width: 120 },
                    { field: '18.0% OUTPUT IGST', title: '18.0% OUTPUT IGST', width: 120 },
                    { field: '28.0% CENTRAL SALE', title: '28.0% CENTRAL SALE', width: 120 },
                    { field: '18.0% LOCAL SALE', title: '18.0% LOCAL SALE', width: 120 },
                    { field: '9.0% OUTPUT CGST', title: '9.0% OUTPUT CGST', width: 120 },
                    { field: '9.0% OUTPUT SGST', title: '9.0% OUTPUT SGST', width: 120 },
                    { field: '14.0% OUTPUT CGST', title: '14.0% OUTPUT CGST', width: 120 },
                    { field: '14.0% OUTPUT SGST', title: '14.0% OUTPUT SGST', width: 120 },
                    { field: '5.0% CENTRAL SALE', title: '5.0% CENTRAL SALE', width: 120 },
                    { field: '5.0% LOCAL SALE', title: '5.0% LOCAL SALE', width: 120 },
                    { field: '2.5% OUTPUT CGST', title: '2.5% OUTPUT CGST', width: 120 },
                    { field: '2.5% OUTPUT SGST', title: '2.5% OUTPUT SGST', width: 120 },
                    { field: '3.0% CENTRAL SALE', title: '3.0% CENTRAL SALE', width: 120 },
                    { field: '3.0% OUTPUT IGST', title: '3.0% OUTPUT IGST', width: 120 },
                    { field: '6.0% CENTRAL SALE', title: '6.0% CENTRAL SALE', width: 120 },
                    { field: '6.0% OUTPUT IGST', title: '6.0% OUTPUT IGST', width: 120 },
                    { field: '6.0% LOCAL SALE', title: '6.0% LOCAL SALE', width: 120 },
                    { field: '3.0% OUTPUT CGST', title: '3.0% OUTPUT CGST', width: 120 },
                    { field: '3.0% OUTPUT SGST', title: '3.0% OUTPUT SGST', width: 120 },
                    { field: '12.0% LOCAL SALE', title: '12.0% LOCAL SALE', width: 120 },
                    { field: '6.0% OUTPUT CGST', title: '6.0% OUTPUT CGST', width: 120 },
                    { field: '12.0% CENTRAL SALE', title: '12.0% CENTRAL SALE', width: 120 },
                    { field: '3.0% LOCAL SALE', title: '3.0% LOCAL SALE', width: 120 },
                    { field: '1.5% OUTPUT CGST', title: '1.5% OUTPUT CGST', width: 120 },
                    { field: '1.5% OUTPUT SGST', title: '1.5% OUTPUT SGST', width: 120 },
                    { field: '12.0% OUTPUT IGST', title: '12.0% OUTPUT IGST', width: 120 },
                    { field: '6.0% OUTPUT SGST', title: '6.0% OUTPUT SGST', width: 120 },
                    { field: 'TOTAL TAX', title: 'TOTAL TAX', width: 120 },
                    { field: 'R/O', title: 'R/O', width: 120 },
                    { field: 'NET AMOUNT', title: 'NET AMOUNT', width: 120 }
                ],
                pager: { limit: 10 },
                //grouping: { groupBy: groupingName },
                detailTemplate: '<div><table/></div>'
            });
            grid.on('detailExpand', function (e, $detailWrapper, id) {
                $.ajax({
                    url: "<%=ServiceURL%>CommonServices.asmx/GetSalesTaxGSTReportDetail",
                    type: 'POST',
                    data: JSON.stringify({ 'FilterValue': id }),
                    contentType: 'application/json;',
                    dataType: 'json',
                    async: false,
                    success: function (result) {
                        if (result != null) {
                            $detailWrapper.find('table').grid({
                                dataSource: result.d.ChildData,
                                autoGenerateColumns: true
                            });
                        }
                    },
                    error: function (result) {
                    }
                });


            });
            grid.on('detailCollapse', function (e, $detailWrapper, id) {
                $detailWrapper.find('table').grid('destroy', true, true);
            });
        }

        function PrepareJSONData(CalledBy) {
            $('#gridSummaryreport').grid('destroy', true, true);
            $("#loader").fadeIn(500).modal('show');
            var DepotID = [];
            $("#ddldepot option:selected").each(function () {
                DepotID.push(this.value);
            });

            var PartyID = [];
            $("#ddlparty option:selected").each(function () {
                PartyID.push(this.value);
            });

            var TaxTypeID = [];
            $("#ddltaxtype option:selected").each(function () {
                TaxTypeID.push(this.value);
            });

            var ddlSchemeValue = $("#ddlscheme option:selected").val();
            var ddlSegmentValue = $("#ddlbsegment option:selected").val();
            var ddlInvoiceTypeValue = $("#ddlinvoicetype option:selected").val();
            var ddlDetailsValue = $("#ddldetails option:selected").val();
            var ddlTypeValue = $("#ddltype option:selected").val();


            var data = JSON.stringify({
                'DateFrom': $('#txtfromdate').val(),
                'DateTo': $('#txttodate').val(),
                'DepotID': DepotID.toString(),
                'Scheme': ddlSchemeValue,
                'Segment': ddlSegmentValue,
                'Type': ddlTypeValue,
                'TaxTypeID': TaxTypeID.toString(),
                'PartyID': PartyID.toString(),
                'InvoiceType': ddlInvoiceTypeValue,
                'Details': ddlDetailsValue,
                'Caller': CalledBy
            });

            return data;
        }

    </script>

</asp:Content>

