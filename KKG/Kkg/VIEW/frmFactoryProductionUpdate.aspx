<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmFactoryProductionUpdate.aspx.cs" Inherits="VIEW_frmFactoryProductionUpdate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/bootstrap-multiselect.css" rel="stylesheet" />
    <script type="text/javascript" src="../js/bootstrap.js"></script>
    <script type="text/javascript" src="../js/bootstrap-multiselect.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" type="text/css" href="../css/jquery.datetimepicker.css" />
    <script type="text/javascript" src="../js/jquery.datetimepicker.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //BindTabFunctions();
            $('#txtStartTime').datetimepicker({
                format: 'd/m/Y H:i'
                //format : 'D/M/Y',                  -- Day format
                //format : 'D/M/Y',
                //hours12:true,
                // timepicker:false,
                // datepicker: true,               
                //yearStart:2000,
                //yearEnd: 2050,
            });
        });
    </script>
    <script type="text/javascript">
        function BindTabFunctions() {
            var tabValue = $("#" + '<%= hdfTabID.ClientID %>').val();
            if (tabValue == "") {
                $(".tabBox .content").hide();
                $(".tabBox .tabs li:first").addClass("selected");
                $("#tab1").show();

                //                $(".tabBoxinner .content").hide();
                //                $(".tabBoxinner .tabs li:first").addClass("selected");
                //                $("#tab5").show();

            }
            else {
                if (tabValue == 2) {
                    $(".tabBox .content").hide();
                    $("ul.tabs li:nth-child(2)").addClass("selected");
                    $("#tab2").show();
                    $("#" + '<%= hdfTabID.ClientID %>').val("");
                }
            }

            $(".tabBox .tabs li").click(function () {
                $(".tabBox .tabs li").removeClass("selected");
                $(this).addClass("selected");
                $(".tabBox .content").hide();
                var activeTab = $(this).find("a").attr("href");
                $(activeTab).show();
                return false;
            });
        };

    </script>
    <style type="text/css">
        .GridHeader {
            text-align: center !important;
        }
    </style>
    <style type="text/css">
        .ajax__tab_xp .ajax__tab_header .ajax__tab_tab {
            width: 50px !important;
            padding: 4px;
            margin: 0px;
            overflow: hidden;
            margin-bottom: -1px;
            height: 2.50em !important;
        }

        .tabBox .tabs {
            margin: 0;
            padding: 0;
            overflow: hidden;
            margin-bottom: -1px;
            height: 2.50em;
        }

            .tabBox .tabs li {
                float: left;
                list-style: none;
                margin: 0;
                padding: .25em .25em 0;
                overflow: hidden;
                position: relative;
                z-index: 1;
                border-bottom: 1px solid #FFF;
            }

                .tabBox .tabs li.selected {
                    z-index: 3;
                }

            .tabBox .tabs a {
                float: left;
                height: 3em;
                line-height: 2em;
                -webkit-border-radius: 4px 4px 0 0;
                -moz-border-radius: 4px 4px 0 0;
                border-radius: 4px 4px 0 0;
                background: #EEE;
                border: 1px solid #BDBDBD;
                border-bottom: 0;
                padding: 0px 15px;
                color: #000;
                padding-bottom: 20px;
                font-family: Lucida Grande,Lucida Sans,Arial,sans-serif;
                font-size: 1.1em;
                text-decoration: none;
                behavior: url(/pie/PIE.htc);
            }

            .tabBox .tabs .selected a {
                background: #FFF;
            }

            .tabBox .tabs a:hover {
                background: #FFF;
            }

            .tabBox .tabs a:active {
                background: #FFF;
            }

        .tabBox .content {
            display: none;
            clear: left;
            position: relative;
            height: 350px;
            z-index: 2;
            padding: 2em 1em;
            border: 1px solid #BDBDBD;
            background: #FFF; /*overflow-y:scroll;*/
        }

        .tabBoxinner .tabs {
            margin: 0;
            padding: 0;
            overflow: hidden;
            margin-bottom: -1px;
            height: 2.50em;
        }

            .tabBoxinner .tabs li {
                float: left;
                list-style: none;
                margin: 0;
                padding: .25em .25em 0;
                overflow: hidden;
                position: relative;
                z-index: 2;
                border-bottom: 1px solid #FFF;
            }

                .tabBoxinner .tabs li.selected {
                    z-index: 7;
                }

            .tabBoxinner .tabs a {
                float: left;
                height: 3em;
                line-height: 2em;
                -webkit-border-radius: 4px 4px 0 0;
                -moz-border-radius: 4px 4px 0 0;
                border-radius: 4px 4px 0 0;
                background: #EEE;
                border: 1px solid #BDBDBD;
                border-bottom: 0;
                padding: 0px 15px;
                color: #000;
                padding-bottom: 20px;
                font-family: Lucida Grande,Lucida Sans,Arial,sans-serif;
                font-size: 1.1em;
                text-decoration: none;
                behavior: url(/pie/PIE.htc);
            }

            .tabBoxinner .tabs .selected a {
                background: #FFF;
            }

            .tabBoxinner .tabs a:hover {
                background: #FFF;
            }

            .tabBoxinner .tabs a:active {
                background: #FFF;
            }

        .tabBoxinner .content {
            display: none;
            clear: left;
            position: relative;
            height: 300px;
            z-index: 2;
            padding: 2em 1em;
            border: 1px solid #BDBDBD;
            background: #FFF;
            overflow-y: scroll;
        }

        .selected {
            background-color: #A1DCF2;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function MakeStaticHeaderBOMSource(gridId, height, width, headerHeight, isFooter) {
            var tbl = document.getElementById(gridId);
            if (tbl) {
                var DivHR = document.getElementById('DivHeaderRowBOMSource');
                var DivMC = document.getElementById('DivMainContentBOMSource');
                var DivFR = document.getElementById('DivFooterRowBOMSource');

                //*** Set divheaderRow Properties ****
                DivHR.style.height = headerHeight + 'px';
                //DivHR.style.width = (parseInt(width) - 16) + 'px';
                DivHR.style.width = '98%';
                DivHR.style.position = 'relative';
                DivHR.style.top = '0px';
                DivHR.style.zIndex = '10';
                DivHR.style.verticalAlign = 'top';

                //*** Set divMainContent Properties ****
                DivMC.style.width = width + 'px';
                DivMC.style.height = height + 'px';
                DivMC.style.position = 'relative';
                DivMC.style.top = -headerHeight + 'px';
                DivMC.style.zIndex = '1';

                //*** Set divFooterRow Properties ****
                DivFR.style.width = (parseInt(width) - 16) + 'px';
                DivFR.style.position = 'relative';
                DivFR.style.top = -headerHeight + 'px';
                DivFR.style.verticalAlign = 'top';
                DivFR.style.paddingtop = '2px';

                if (isFooter) {
                    var tblfr = tbl.cloneNode(true);
                    tblfr.removeChild(tblfr.getElementsByTagName('tbody')[0]);
                    var tblBody = document.createElement('tbody');
                    tblfr.style.width = '100%';
                    tblfr.cellSpacing = "0";
                    tblfr.border = "0px";
                    tblfr.rules = "none";
                    //*****In the case of Footer Row *******
                    tblBody.appendChild(tbl.rows[tbl.rows.length - 1]);
                    tblfr.appendChild(tblBody);
                    DivFR.appendChild(tblfr);
                }
                //****Copy Header in divHeaderRow****
                DivHR.appendChild(tbl.cloneNode(true));
            }
        }

        function OnScrollDivBOMSource(Scrollablediv) {
            document.getElementById('DivHeaderRowBOMSource').scrollLeft = Scrollablediv.scrollLeft;
            document.getElementById('DivFooterRowBOMSource').scrollLeft = Scrollablediv.scrollLeft;
        }
    </script>
    <script type="text/javascript">
        $(function () {
            $('#ContentPlaceHolder1_ddlFG').multiselect({
                includeSelectAllOption: true
            });
            $("#ContentPlaceHolder1_ddlFG").multiselect('selectAll', false);
            $("#ContentPlaceHolder1_ddlFG").multiselect('updateButtonText');
        });

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if ((charCode < 48 || charCode > 57) && (charCode != 8))
                return false;
            return true;
        }
    </script>
    <script type="text/javascript">
        function OnBeforeDelete(record) {
            record.Error = '';
            document.getElementById("<%=Hdn_Fld.ClientID %>").value = record.COMPANYID;
            if (confirm("Are you sure you want to delete? "))
                return true;
            else
                return false;
        }
        function OnDelete(record) {
            alert(record.Error);
        }
    </script>
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode < 48 || charCode > 57)
                return false;
            return true;
        }
    </script>
    <script type="text/javascript">
        function CallServerMethod(oLink) {
            var iRecordIndex = oLink.id.toString().replace("btnGridEdit_", "");
            document.getElementById("<%=Hdn_Fld.ClientID %>").value = gvCompany.Rows[iRecordIndex].Cells[0].Value;
            document.getElementById("<%=btngridedit.ClientID %>").click();
        }

    </script>
    <script type="text/javascript">
        var searchTimeout = null;
        function FilterTextBox_KeyUp() {
            searchTimeout = window.setTimeout(performSearch, 500);
        }
        function performSearch() {
            var searchValue = document.getElementById('FilterTextBox').value;
            if (searchValue == FilterTextBox.WatermarkText) {
                searchValue = '';
            }
            gvCompany.addFilterCriteria('COMPANYCODE', OboutGridFilterCriteria.Contains, searchValue);
            gvCompany.addFilterCriteria('COMPANYNAME', OboutGridFilterCriteria.Contains, searchValue);

            gvCompany.executeFilter();
            searchTimeout = null;
            return false;
        }
    </script>
    <script type="text/javascript">
        oboutGrid.prototype._resizeColumn = oboutGrid.prototype.resizeColumn;
        oboutGrid.prototype.resizeColumn = function (columnIndex, amountToResize, keepGridWidth) {
            this._resizeColumn(columnIndex, amountToResize, false);

            var width = this._getColumnWidth();

            if (width > 0) {
                if (this.ScrollWidth == '0px') {
                    this.GridMainContainer.style.width = width + 'px';
                } else {
                    var scrollWidth = parseInt(this.ScrollWidth);
                    if (width < scrollWidth) {
                        this.GridMainContainer.style.width = width + 'px';
                        this.HorizontalScroller.style.display = 'none';
                    } else {
                        this.HorizontalScroller.firstChild.firstChild.style.width = width + 'px';
                        this.HorizontalScroller.style.display = '';
                    }
                }
            }
        }

        oboutGrid.prototype._getColumnWidth = function () {
            var totalWidth = 0;
            for (var i = 0; i < this.ColumnsCollection.length; i++) {
                if (this.ColumnsCollection[i].Visible) {
                    totalWidth += this.ColumnsCollection[i].Width;
                }
            }

            return totalWidth;
        }
    </script>
    <script type="text/javascript" language="javascript">
        function calculate(a) {
            var IssueQtyid = 0;
            var IssueQty = 0;
            var ConsumblesQtyID = 0;
            var ConsumblesQty = 0;
            var AllConsumblesQtyID = 0;
            var AllConsumblesQty = 0;
            var BeforeWastage = 0;
            var AllReadyWastageID = 0;
            var AllReadyWastageQty = 0;
            var WastageID = 0;
            var WastageQty = 0;
            var ReturnQtyID = 0;
            var ReturnQty = 0;
            var AllReadyReturnQtyID = 0;
            var AllReadyReturnQty = 0;
            var WipID = 0;
            var WipQty = 0;

            var rowData = a.parentNode.parentNode;
            var rowIndex = rowData.rowIndex - 1;
            var grd = document.getElementById('<%= GV_ISSUE.ClientID %>');
            var grid = document.getElementById('ContentPlaceHolder1_GV_ISSUE');

            IssueQtyid = "ContentPlaceHolder1_GV_ISSUE_lblISSUEQTY_" + rowIndex;
            IssueQty = parseFloat(document.getElementById(IssueQtyid).value);

            ConsumblesQtyID = "ContentPlaceHolder1_GV_ISSUE_txtCONSUMABLESQTY_" + rowIndex;
            ConsumblesQty = parseFloat(document.getElementById(ConsumblesQtyID).value);

            AllConsumblesQtyID = "ContentPlaceHolder1_GV_ISSUE_txtALLCONSUMABLESQTY_" + rowIndex;
            AllConsumblesQty = parseFloat(document.getElementById(AllConsumblesQtyID).value);

            WastageID = "ContentPlaceHolder1_GV_ISSUE_txtWastage_" + rowIndex;
            WastageQty = parseFloat(document.getElementById(WastageID).value);

            AllReadyWastageID = "ContentPlaceHolder1_GV_ISSUE_txtAllReadyWastage_" + rowIndex;
            AllReadyWastageQty = parseFloat(document.getElementById(AllReadyWastageID).value);

            ReturnQtyID = "ContentPlaceHolder1_GV_ISSUE_lblReturnQty_" + rowIndex;
            ReturnQty = parseFloat(document.getElementById(ReturnQtyID).value);

            AllReadyReturnQtyID = "ContentPlaceHolder1_GV_ISSUE_lblAllReturnQty_" + rowIndex;
            AllReadyReturnQty = parseFloat(document.getElementById(AllReadyReturnQtyID).value);

            //alert(AllReadyReturnQty);

            WipID = "ContentPlaceHolder1_GV_ISSUE_txtWipQty_" + rowIndex;
            WipQty = parseFloat(document.getElementById(WipID).value);

            BeforeWastage = IssueQty - (ConsumblesQty + AllConsumblesQty + AllReadyWastageQty + AllReadyReturnQty);
            //alert(BeforeWastage);

            if (WastageQty > BeforeWastage) {
                alert("Wastage Qty Can Not be greater Than Production Qty.");
                document.getElementById(WastageID).value = 0;
                WipQty = BeforeWastage; //(IssueQty) - (AllConsumblesQty + ConsumblesQty)
                document.getElementById(WipID).value = WipQty.toFixed(3);
                //document.getElementById(WipID).value = 0;
            }
            else {
                /*ReturnQty = BeforeWastage - WastageQty;
                document.getElementById(ReturnQtyID).value = ReturnQty.toFixed(3);*/

                WipQty = (BeforeWastage - ReturnQty) - parseFloat(WastageQty);
                document.getElementById(WipID).value = WipQty.toFixed(3);
            }
        }
    </script>

    <script type="text/javascript" language="javascript">
        function calculatereturn(a) {
            var IssueQtyid = 0;
            var IssueQty = 0;
            var ConsumblesQtyID = 0;
            var ConsumblesQty = 0;
            var AllConsumblesQtyID = 0;
            var AllConsumblesQty = 0;
            var BeforeWastage = 0;
            var AllReadyWastageID = 0;
            var AllReadyWastageQty = 0;
            var WastageID = 0;
            var WastageQty = 0;
            var ReturnQtyID = 0;
            var ReturnQty = 0;
            var AllReadyReturnQtyID = 0;
            var AllReadyReturnQty = 0;
            var WipID = 0;
            var WipQty = 0;

            var rowData = a.parentNode.parentNode;
            var rowIndex = rowData.rowIndex - 1;
            var grd = document.getElementById('<%= GV_ISSUE.ClientID %>');
            var grid = document.getElementById('ContentPlaceHolder1_GV_ISSUE');

            IssueQtyid = "ContentPlaceHolder1_GV_ISSUE_lblISSUEQTY_" + rowIndex;
            IssueQty = parseFloat(document.getElementById(IssueQtyid).value);

            ConsumblesQtyID = "ContentPlaceHolder1_GV_ISSUE_txtCONSUMABLESQTY_" + rowIndex;
            ConsumblesQty = parseFloat(document.getElementById(ConsumblesQtyID).value);

            AllConsumblesQtyID = "ContentPlaceHolder1_GV_ISSUE_txtALLCONSUMABLESQTY_" + rowIndex;
            AllConsumblesQty = parseFloat(document.getElementById(AllConsumblesQtyID).value);

            WastageID = "ContentPlaceHolder1_GV_ISSUE_txtWastage_" + rowIndex;
            WastageQty = parseFloat(document.getElementById(WastageID).value);

            AllReadyWastageID = "ContentPlaceHolder1_GV_ISSUE_txtAllReadyWastage_" + rowIndex;
            AllReadyWastageQty = parseFloat(document.getElementById(AllReadyWastageID).value);

            ReturnQtyID = "ContentPlaceHolder1_GV_ISSUE_lblReturnQty_" + rowIndex;
            ReturnQty = parseFloat(document.getElementById(ReturnQtyID).value);

            AllReadyReturnQtyID = "ContentPlaceHolder1_GV_ISSUE_lblAllReturnQty_" + rowIndex;
            AllReadyReturnQty = parseFloat(document.getElementById(AllReadyReturnQtyID).value);

            WipID = "ContentPlaceHolder1_GV_ISSUE_txtWipQty_" + rowIndex;
            WipQty = parseFloat(document.getElementById(WipID).value);

            ReturnQtyID = "ContentPlaceHolder1_GV_ISSUE_lblReturnQty_" + rowIndex;
            ReturnQty = parseFloat(document.getElementById(ReturnQtyID).value);

            //BeforeWastage = IssueQty - ConsumblesQty;
            BeforeWastage = IssueQty - (ConsumblesQty + AllConsumblesQty + AllReadyWastageQty + AllReadyReturnQty);

            if (ReturnQty > WipQty) {
                alert("Return Qty Can Not be greater Than Wip Qty.");
                document.getElementById(ReturnQtyID).value = 0;

                WipQty = BeforeWastage;
                document.getElementById(WipID).value = WipQty.toFixed(3);                
            }
            else {
                /*ReturnQty = BeforeWastage - WastageQty;
                document.getElementById(ReturnQtyID).value = ReturnQty.toFixed(3);*/

                WipQty = (BeforeWastage - WastageQty) - parseFloat(ReturnQty);//WipQty - ReturnQty;
                document.getElementById(WipID).value = WipQty.toFixed(3);
            }
        }
    </script>

    <script type="text/javascript">
        function CheckOne(obj) {
            var grid = obj.parentNode.parentNode.parentNode;
            var inputs = grid.getElementsByTagName("input");
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].type == "checkbox") {
                    if (obj.checked && inputs[i] != obj && inputs[i].checked) {
                        inputs[i].checked = false;
                    }
                }
            }
        }
    </script>

    <asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
            <asp:Image ID="Image1" ImageUrl="~/images/loading123.gif" AlternateText="Processing"
                runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <script type="text/javascript">
                Sys.Application.add_load(BindTabFunctions);

            </script>
            <script type="text/javascript">
                oboutGrid.prototype._resizeColumn = oboutGrid.prototype.resizeColumn;
                oboutGrid.prototype.resizeColumn = function (columnIndex, amountToResize, keepGridWidth) {
                    this._resizeColumn(columnIndex, amountToResize, false);

                    var width = this._getColumnWidth();

                    if (width > 0) {
                        if (this.ScrollWidth == '0px') {
                            this.GridMainContainer.style.width = width + 'px';
                        } else {
                            var scrollWidth = parseInt(this.ScrollWidth);
                            if (width < scrollWidth) {
                                this.GridMainContainer.style.width = width + 'px';
                                this.HorizontalScroller.style.display = 'none';
                            } else {
                                this.HorizontalScroller.firstChild.firstChild.style.width = width + 'px';
                                this.HorizontalScroller.style.display = '';
                            }
                        }
                    }
                }

                oboutGrid.prototype._getColumnWidth = function () {
                    var totalWidth = 0;
                    for (var i = 0; i < this.ColumnsCollection.length; i++) {
                        if (this.ColumnsCollection[i].Visible) {
                            totalWidth += this.ColumnsCollection[i].Width;
                        }
                    }
                    return totalWidth;
                }
            </script>
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>Production Update/Requisition</h6>
                                <div class="btn_30_light" style="float: right; display: none;" id="btnaddhide" runat="server">
                                    <span class="icon add_co"></span>
                                    <asp:Button ID="btnAddnewRecord" runat="server" Text="Add New Record" CssClass="btn_link"
                                        OnClick="btnAddnewRecord_Click" CausesValidation="false" />
                                </div>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr id="trHeader" runat="server">
                                            <td class="field_input" style="padding-left: 10px;" colspan="2">
                                                <fieldset>
                                                    <legend>Production Order Detail</legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                        <tr>

                                                            <td class="field_title">
                                                                <asp:Label ID="Label1" runat="server" Text="State"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:DropDownList ID="ddlstate" runat="server" AppendDataBoundItems="true" class="chosen-select" AutoPostBack="true"
                                                                    Style="width: 150px;" data-placeholder="Choose a State" OnSelectedIndexChanged="ddlstate_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0" Selected="True">Please Select</asp:ListItem>
                                                                    <asp:ListItem Value="I">Process Start</asp:ListItem>
                                                                    <asp:ListItem Value="O">Process End</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td class="field_title" runat="server" id="tdlblOdNo">
                                                                <asp:HiddenField ID="hdfTabID" runat="server" />
                                                                <asp:Label ID="lblPOOrder" Text="Production&nbsp;Order&nbsp;(BATCH&nbsp;No)" runat="server"></asp:Label>
                                                            </td>
                                                            <td id="Td1" class="field_input" runat="server">
                                                                <asp:DropDownList ID="ddlbatch" runat="server" AppendDataBoundItems="true" class="chosen-select"
                                                                    Style="width: 150px;" data-placeholder="Choose a Batch" OnSelectedIndexChanged="ddlbatch_SelectedIndexChanged"
                                                                    AutoPostBack="True">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="Req_ddlbatch" runat="server" ControlToValidate="ddlbatch"
                                                                    InitialValue="0" SetFocusOnError="true" ErrorMessage="Required!" ForeColor="Red"
                                                                    ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td class="field_title">
                                                                <asp:Label ID="Label4" runat="server" Text="Process"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:DropDownList ID="ddlProcess" runat="server" AppendDataBoundItems="true" class="chosen-select"
                                                                    Style="width: 150px;" data-placeholder="Choose a Process">
                                                                </asp:DropDownList>
                                                            </td>
                                                            
                                                            <td class="field_title">
                                                                <asp:Label ID="lblproductiondate" runat="server" Text="Production Date"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtProductiondt" runat="server" Width="80" placeholder="dd/mm/yyyy" MaxLength="10"
                                                                    Enabled="false" onkeypress="return isNumberKeyWithslash(event);"></asp:TextBox>
                                                                <asp:ImageButton ID="ImgEntrydt" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImgEntrydt" runat="server"
                                                                    TargetControlID="txtProductiondt" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>
                                                            <tr />
                                                        <tr>
                                                            <td align="left" width="80" class="innerfield_title">&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblRemarks" Text="Remarks" runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtRemarks" runat="server" Width="300px"
                                                                    MaxLength="50"> </asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <div class="btn_24_blue">
                                                                    <span class="icon find_co"></span>
                                                                    <asp:Button ID="btnShow" runat="server" Text="Search" CssClass="btn_link" OnClick="btnShow_Click" />
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px;" colspan="2">
                                                <fieldset>
                                                    <legend>Product Detail</legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                        <asp:GridView ID="grdQtySchemeDetails" runat="server" AutoGenerateColumns="false"
                                                            EmptyDataText="No Records Available" Width="100%" ShowFooter="false" CssClass="zebra">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="SL">
                                                                    <ItemTemplate>
                                                                        <%# Container.DataItemIndex + 1 %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="BULKPRODUCT" HeaderText="Bulk Product"></asp:BoundField>
                                                                <asp:BoundField DataField="catName" HeaderText="Category"></asp:BoundField>
                                                                <asp:TemplateField HeaderText="catid" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblcatid" runat="server" Text='<%# Bind("catid") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="Name" HeaderText="Name"></asp:BoundField>
                                                                <asp:TemplateField HeaderText="Itemid" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblItemid" runat="server" Text='<%# Bind("Itemid") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="unit" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblunit" runat="server" Text='<%# Bind("unit") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="UOMNAME" HeaderText="Unit"></asp:BoundField>

                                                                <asp:TemplateField HeaderText="BOM(QTY)">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBomQty" runat="server" Text='<%# Bind("QTY") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="BUFFER QTY">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBufferQty" runat="server" Text='<%# Bind("BUFFERQTY") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="NET QTY">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNetQty" runat="server" Text='<%# Bind("NETQTY") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Produce/Required Qty">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtproduceQty" runat="server" value='<%# Eval("NETQTY") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                        <asp:GridView ID="gvProcessEndItem" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Available"
                                                            Width="100%" ShowFooter="true" CssClass="zebra" OnRowCommand="gvProcessEndItem_RowCommand">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="SL">
                                                                    <ItemTemplate>
                                                                        <%# Container.DataItemIndex + 1 %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <%--<asp:BoundField DataField="BATCHNO" HeaderText="Batch No"></asp:BoundField>--%>

                                                                <asp:TemplateField HeaderText="Production ID" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblProductionID" runat="server" Text='<%# Bind("PRODUCTION_ORDERID") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Batch No">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkBatchNo" runat="server" ForeColor="Black" CommandName="BATCHNO"
                                                                            CommandArgument='<%#Eval("BATCHNO")+","+Eval("PRODUCTION_ORDERID") %>' Text='<%# Bind("BATCHNO") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:BoundField DataField="CATNAME" HeaderText="Category"></asp:BoundField>
                                                                <asp:TemplateField HeaderText="catid" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblcatid" runat="server" Text='<%# Bind("catid") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblItemName" runat="server" Text='<%# Bind("Name") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Itemid" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblItemid" runat="server" Text='<%# Bind("itemID") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="unit" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblunit" runat="server" Text='<%# Bind("unit") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Unit" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblUomName" runat="server" Text='<%# Bind("UOMNAME") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Pack Size">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblConversionQty" runat="server" Text='<%# Bind("CONVERSIONQTY") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Estimated (QTY/KG)">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEstimatedQtyKG" runat="server" Text='<%# Bind("QTYINKG") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Estimated (QTY)">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEstimatedQty" runat="server" Text='<%# Bind("QTY") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Actual Produced (Qty/kg)">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtProducedQtyKg" runat="server" Text='<%# Bind("QTYINKG") %>' AutoPostBack="true"
                                                                            OnTextChanged="txtProducedQtyKg_TextChanged"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Actual Produced Qty">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtProducedQty" runat="server" Text='<%# Bind("QTY") %>' Enabled="false"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Sample Qty">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtRejectedQty" runat="server" Text="0" OnTextChanged="txtRejectedQty_TextChanged"
                                                                            AutoPostBack="true"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Any Wastage(Y/N)">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkyn" runat="server" OnCheckedChanged="chkyn_OnCheckedChanged"
                                                                            AutoPostBack="true"></asp:CheckBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Yield(%)" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblYield" runat="server" Text='<%# Bind("Yield") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>

                                                        <asp:GridView ID="grdStoreReturn" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Available"
                                                            Width="100%" ShowFooter="true" CssClass="zebra">
                                                            <Columns>

                                                                <asp:TemplateField HeaderText="" HeaderStyle-Width="12">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkreq" runat="server" Text=" " value='<%# Eval("PRODUCTION_ORDERID") %>' onclick="CheckOne(this)" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="SL">
                                                                    <ItemTemplate>
                                                                        <%# Container.DataItemIndex + 1 %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="ProductionOrderID" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblProductionID" runat="server" Text='<%# Bind("PRODUCTION_ORDERID") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:BoundField DataField="PRODUCTIONNO" HeaderText="Production No"></asp:BoundField>
                                                                <asp:BoundField DataField="ENTRY_DATE" HeaderText="Entry Date"></asp:BoundField>
                                                                <asp:BoundField DataField="BATCHNO" HeaderText="Batch No"></asp:BoundField>
                                                                <asp:BoundField DataField="CATNAME" HeaderText="Category"></asp:BoundField>
                                                                <asp:TemplateField HeaderText="catid" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblcatid" runat="server" Text='<%# Bind("catid") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblItemName" runat="server" Text='<%# Bind("Name") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Itemid" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblItemid" runat="server" Text='<%# Bind("itemID") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="unit" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblunit" runat="server" Text='<%# Bind("unit") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Unit" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblUomName" runat="server" Text='<%# Bind("UOMNAME") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Pack Size">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblConversionQty" runat="server" Text='<%# Bind("CONVERSIONQTY") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Estimated (QTY/KG)">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEstimatedQtyKG" runat="server" Text='<%# Bind("QTYINKG") %>' Width="50" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Estimated (QTY)">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEstimatedQty" runat="server" Text='<%# Bind("QTY") %>' Width="50" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Actual Produced (Qty/kg)">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtProducedQtyKg" runat="server" Text='<%# Bind("PRODUCEQTYINKG") %>' Enabled="false" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Actual Produced Qty">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtProducedQty" runat="server" Text='<%# Bind("PRODUCEQTY") %>' Enabled="false"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Sample Qty">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtRejectedQty" runat="server" Text='<%# Bind("SAMPLEQTY") %>' Enabled="false" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Any Wastage(Y/N)">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkWastage" runat="server" Checked="true" Enabled="false" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Check Return Stock">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkRS" runat="server" OnCheckedChanged="chkRS_OnCheckedChanged"
                                                                            AutoPostBack="true"></asp:CheckBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Yield(%)" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblYield" runat="server" Text='<%# Bind("Yield") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>

                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                    </table>
                                    <tr>
                                        <td style="padding: 8px 0px;">
                                            <div class="btn_24_blue" id="divbtnSubmit" runat="server">
                                                <span class="icon disk_co"></span>
                                                <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="btn_link" OnClick="btnSubmit_Click"
                                                    CausesValidation="false" ValidationGroup="Save" />
                                            </div>
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <%--<div class="btn_24_blue">
                                                    <span class="icon cross_octagon_co"></span>
                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn_link" OnClick="btnCancel_Click"
                                                        CausesValidation="false" />
                                                </div>--%>
                                            <div class="btn_24_green" id="divbtnapprove" runat="server">
                                                <span class="icon approve_co"></span>
                                                <asp:LinkButton ID="btnApprove" runat="server" Text="Approve" CssClass="btn_link" ValidationGroup="Approved"
                                                    OnClick="btnApprove_Click" />
                                            </div>
                                            <asp:HiddenField ID="HiddenField1" runat="server" />
                                        </td>
                                    </tr>
                                </asp:Panel>
                                <div id="light2" class="white_content" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td>
                                                <div style="margin: 0 auto; width: 100%;">
                                                    <div style="overflow: hidden;" id="DivHeaderRow">
                                                    </div>
                                                    <div id="DivMainContent" style="overflow: scroll; height: 300px;" onscroll="OnScrollDiv(this);">
                                                        <asp:GridView ID="GV_ISSUE" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Available"
                                                            Width="100%" CssClass="zebra" OnRowDataBound="GV_ISSUE_RowDataBound">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="CATEGORYID" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCATEGORYID" runat="server" Text='<%# Bind("CATEGORYID") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="CATEGORY NAME">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCATEGORYNAME" runat="server" Text='<%# Bind("CATEGORYNAME") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="MATERIALID" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMATERIALID" runat="server" Text='<%# Bind("MATERIALID") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="MATERIAL NAME">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMATERIALNAME" runat="server" Text='<%# Bind("MATERIALNAME") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="UOMID" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblUOMID" runat="server" Text='<%# Bind("UOMID") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="UOM">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblUOMNAME" Width="40px" runat="server" Text='<%# Bind("UOMNAME") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ISSUE QTY">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="lblISSUEQTY" Width="80px" runat="server" Text='<%# Bind("ISSUEQTY") %>' Enabled="false" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ALLREADY CONSUMABLES QTY">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtALLCONSUMABLESQTY" Width="80px" runat="server" Text='<%# Bind("ALLREADYCONSUMABLESQTY") %>'
                                                                            Enabled="false" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="CONSUMABLES QTY">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtCONSUMABLESQTY" Width="80px" runat="server" Text='<%# Bind("CONSUMABLESQTY") %>'
                                                                            Enabled="false" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="ALLREADY WASTAGE QTY">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtAllReadyWastage" Width="80px" runat="server" Text='<%# Bind("ALLREADYWASTAGEQTY") %>' Enabled="false" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="WASTAGE QTY">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtWastage" Width="80px" runat="server" Text='<%# Bind("WASTAGEQTY") %>' onChange="calculate(this);" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="ALLREADY RETURN QTY">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="lblAllReturnQty" Width="70px" runat="server" Text='<%# Bind("ALLREADYRETURNQTY") %>' Enabled="false" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="RETURN QTY">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="lblReturnQty" Width="70px" runat="server" Text='<%# Bind("RETURNQTY") %>' onChange="calculatereturn(this);" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="WIP QTY">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtWipQty" Width="50px" runat="server" Text='<%# Bind("WIPQTY") %>' Enabled="false" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                    <div id="DivFooterRow" style="overflow: hidden;">
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 8px 0px;">
                                                <div class="btn_24_blue" id="divbtnSubmit1" runat="server">
                                                    <span class="icon disk_co"></span>
                                                    <asp:Button ID="btnSubmit1" runat="server" Text="Save" CssClass="btn_link" OnClick="btnSubmit1_Click"
                                                        CausesValidation="false" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
                                                    <span class="icon cross_octagon_co"></span>
                                                    <asp:Button ID="btnCancel1" runat="server" Text="Cancel" CssClass="btn_link" OnClick="btnCancel1_Click"
                                                        CausesValidation="false" />
                                                </div>
                                                <asp:HiddenField ID="Hdn_Fld" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>

                                <div id="ShowProduction" class="white_content" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td>
                                                <div id="DivMainContentShow" style="overflow: scroll; height: 150px;">
                                                    <asp:GridView ID="GvProductionQty" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Available"
                                                        Width="100%" CssClass="zebra">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="SL">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="PRODUCTION_ORDERID" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblProductionID" runat="server" Text='<%# Bind("PRODUCTION_ORDERID") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="BATCHNO">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBatchNO" runat="server" Text='<%# Bind("BATCHNO") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="PRODUCT NAME">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblProductName" runat="server" Text='<%# Bind("NAME") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="PRODUCTION QTY">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblProductionQty" runat="server" Text='<%# Bind("PRODUCTIONQTY") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="PRODUCTION QTYINKG">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblProductionQtyInKg" runat="server" Text='<%# Bind("PRODUCTIONQTYINKG") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="USE QTY">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUseQty" runat="server" Text='<%# Bind("USEQTY") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="USEQTY INKG">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUseQtyInKg" runat="server" Text='<%# Bind("USEQTYINKG") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="BALANCE QTY">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBalanceQty" runat="server" Text='<%# Bind("BALANCEQTY") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="BALANCEQTY INKG">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBalanceQtyInKg" runat="server" Text='<%# Bind("BALANCEQTYINKG") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 8px 0px;">
                                                <div class="btn_24_blue">
                                                    <span class="icon cross_octagon_co"></span>
                                                    <asp:Button ID="btnClose" runat="server" Text="Cancel" CssClass="btn_link" OnClick="btnClose_Click"
                                                        CausesValidation="false" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <asp:Panel ID="pnlDisplay" runat="server">
                                    <div class="gridcontent">
                                        <asp:Button ID="btngridedit" runat="server" Text="Gridedit" CausesValidation="false"
                                            Style="display: none" />
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                    <div class="footer" align="right">
                    </div>
                    <cc1:MessageBox ID="MessageBox1" runat="server" />
                    <span class="clear"></span>
                </div>
                <span class="clear"></span>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>