<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmRptStockInHand_kkg.aspx.cs" Inherits="VIEW_Default" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/bootstrap-multiselect.css" rel="stylesheet" />
    <script type="text/javascript" src="../js/bootstrap.js"></script>
    <script type="text/javascript" src="../js/bootstrap-multiselect.js"></script>
    <script src="../js/table2excel.js"></script>


    <script language="javascript" type="text/javascript">
        function MakeStaticHeader(gridId, height, width, headerHeight, isFooter) {
            var tbl = document.getElementById(gridId);
            if (tbl) {
                var DivHR = document.getElementById('DivHeaderRow');
                var DivMC = document.getElementById('DivMainContent');
                var DivFR = document.getElementById('DivFooterRow');

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

        function OnScrollDivOut(Scrollablediv) {
            document.getElementById('DivHeaderRow').scrollLeft = Scrollablediv.scrollLeft;
            document.getElementById('DivFooterRow').scrollLeft = Scrollablediv.scrollLeft;
        }
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
    </style>



    <style>
        .hidden {
            display: none;
        }
    </style>



    <style type="text/css">
        .messagealert {
            width: 30%;
            position: absolute;
            top: 0px;
            padding-left: inherit;
            font-size: 15px;
            color: black;
            font-family: emoji;
        }
    </style>

    <style type="text/css">
        .alert-danger {
            color: orange;
            background-color: rgb(253, 247, 247);
            border-color: rgb(217, 83, 79);
        }
    </style>

    <script type="text/javascript">
        function ShowMessage(message, messagetype) {
            var cssclass;
            switch (messagetype) {
                case 'Success':
                    cssclass = 'alert-success'
                    break;
                case 'Error':
                    cssclass = 'alert-danger'
                    break;
                case 'Warning':
                    cssclass = 'alert-warning'
                    break;
                default:
                    cssclass = 'alert-info'
            }
            $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #fff; background-color: #BEF3B1; float:left;" class="alert fade in ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>');

            setTimeout(function () {
                $("#alert_div").fadeTo(2000, 500).slideUp(500, function () {
                    $("#alert_div").remove();
                });
            }, 1000);
        }
    </script>

    <script type="text/javascript">

        $(document).ready(function () {
           


        });


        $("body").on("click", "#btnExport", function () {

            var str = new String("Dowload Start Click " + " Ok " + " and Wait for Some time");
            alert(str);

            $("[id*=grdStockInHand]").table2excel({
                filename: "StockInHand.xls"
            });


            var str = new String("Dowload Complete");
            alert(str);

        })







        function HighlightRow(chkB) {

            var IsChecked = chkB.checked;

            if (IsChecked) {

                chkB.parentElement.parentElement.style.backgroundColor = 'PaleGreen';

                chkB.parentElement.parentElement.style.color = 'black';

            } else {

                chkB.parentElement.parentElement.style.backgroundColor = 'PowderBlue';

                chkB.parentElement.parentElement.style.color = 'black';

            }

        }


        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_beginRequest(BeginRequestHandler);
        prm.add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args) {
            var popup = $find('<%= modalPopup.ClientID %>');
            if (popup != null) {
                popup.show();
            }
        }

        function EndRequestHandler(sender, args) {
            var popup = $find('<%= modalPopup.ClientID %>');
            if (popup != null) {
                popup.hide();
            }
        }

    </script>
    <script type="text/javascript">
        function ValidateListBox_Depot(sender, args) {
            var options = document.getElementById("<%=ddlDiv.ClientID%>").options;
            for (var i = 0; i < options.length; i++) {
                if (options[i].selected == true) {
                    args.IsValid = true;
                    return;
                }
            }
            args.IsValid = false;
        }
        function ValidateListBox_ddlbsegment(sender, args) {
            var options = document.getElementById("<%=ddlCat.ClientID%>").options;
            for (var i = 0; i < options.length; i++) {
                if (options[i].selected == true) {
                    args.IsValid = true;
                    return;
                }
            }
            args.IsValid = false;
        }
    </script>
    <script type="text/javascript">

        $(function () {
            $('#ContentPlaceHolder1_ddlDiv').multiselect({
                includeSelectAllOption: true
            });
        });
        $(function () {
            $('#ContentPlaceHolder1_ddlCat').multiselect({
                includeSelectAllOption: true
            });
        });
    </script>













    <asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
            <asp:Image ID="Image1" ImageUrl="../images/103.gif" AlternateText="Processing"
                runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
        PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="page_title">
                <span class="title_icon"><span class="computer_imac"></span></span>
                <h3>Factory Stock In Hand</h3>
            </div>
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_content">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>

                                                        <td class="field_title">
                                                            <asp:Label ID="Label8" runat="server" Text="Date"></asp:Label>
                                                        </td>
                                                        <td width="23%">
                                                            <asp:TextBox ID="txtReqFromDate" runat="server" Width="70" placeholder="dd/mm/yyyy" MaxLength="10" Enabled="false"></asp:TextBox>
                                                            <asp:ImageButton ID="ImageButtonReqFromDate" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle" runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender6" PopupButtonID="ImageButtonReqFromDate"
                                                                runat="server" TargetControlID="txtReqFromDate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                        <td class="field_title">
                                                            <asp:Label ID="lblFactoryName" runat="server" Text="Factory Name"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="23%">
                                                            <asp:DropDownList ID="ddlFactory" runat="server"
                                                                AppendDataBoundItems="True" Width="250" class="chosen-select">
                                                            </asp:DropDownList>
                                                        </td>

                                                        <td class="field_title">
                                                            <asp:Label ID="lblPono" runat="server" Text="DIVISON NAME"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="23%">
                                                            <asp:ListBox ID="ddlDiv" runat="server" SelectionMode="Multiple" ValidationGroup="ADD"
                                                                AppendDataBoundItems="True" TabIndex="4" Width="150px"></asp:ListBox>
                                                        </td>

                                                        <td class="field_title">
                                                            <asp:Label ID="Label1" runat="server" Text="CATEGORY NAME"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="23%">
                                                            <asp:ListBox ID="ddlCat" runat="server" SelectionMode="Multiple" ValidationGroup="ADD"
                                                                AppendDataBoundItems="True" TabIndex="4" Width="150px"></asp:ListBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="field_title">
                                                            <asp:Label ID="Label2" runat="server" Text="Product Name"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="23%">
                                                            <asp:DropDownList ID="ddlProduct" runat="server"
                                                                AppendDataBoundItems="True" Width="250" class="chosen-select">
                                                            </asp:DropDownList>
                                                        </td>

                                                        <td class="field_title">
                                                            <asp:Label ID="Label3" runat="server" Text="Store Location Name"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="23%">
                                                            <asp:DropDownList ID="ddlStore" runat="server"
                                                                AppendDataBoundItems="True" Width="250" class="chosen-select">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding: 8px 0" id="save" runat="server" colspan="4">

                                                            <span class="icon page_white_get_co"></span>
                                                            <asp:Button ID="btnShowReport" runat="server" Text="Show Report" CssClass="btn btn-success" OnClick="btnShowReport_Click" />

                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                        <td width="23%">
                                                            <div id="divExport" class="btn_24_blue" runat="server" style="display: none">
                                                                <span class="icon excel_document"></span>
                                                                <input type="button" id="btnExport" style="color: green" value="Export Excel" />
                                                            </div>


                                                        </td>
                                                    </tr>
                                                </table>
                                                
                                                <div style="overflow: scroll; height: 600px; width: 100%" id="divProductDetails" onscroll="OnScrollDiv(this)" runat="server">
                                                    <fieldset>
                                                        <legend>STOCK REPORT</legend>
                                                        <div style="overflow: hidden; width: 100%;" id="DivHeaderRow">
                                                        </div>
                                                        <div  style="overflow: scroll; height: 500px; width: 1200px" onscroll="OnScrollDiv(this)" id="DivMainContent">

                                                            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                                <asp:GridView ID="grdStockInHand" runat="server" Width="100%" CssClass="reportgrid"
                                                                    AutoGenerateColumns="true" ShowFooter="true" AlternatingRowStyle-BackColor="AliceBlue"
                                                                    EmptyDataText="No Records Available">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="HIGHLIGHT" SortExpression="HIGHLIGHT">
                                                                            <EditItemTemplate>
                                                                                <asp:CheckBox ID="CheckBox1" runat="server" />
                                                                            </EditItemTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="CheckBox1" runat="server" onclick="javascript:HighlightRow(this);" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </table>
                                                        </div>
                                                    </fieldset>
                                                </div>

                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                    <div class="messagealert" id="alert_container">
                    </div>
                    <cc1:MessageBox ID="MessageBox1" runat="server" />
                    <span class="clear"></span>
                </div>
                <span class="clear"></span>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

