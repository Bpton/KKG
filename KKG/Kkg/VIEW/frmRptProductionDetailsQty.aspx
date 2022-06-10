<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmRptProductionDetailsQty.aspx.cs" Inherits="VIEW_frmRptProductionDetailsQty" %>



<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <link href="../css/bootstrap-multiselect.css" rel="stylesheet" />
    <script type="text/javascript" src="../js/bootstrap.js"></script>
    <script type="text/javascript" src="../js/bootstrap-multiselect.js"></script>
    <script src="../js/table2excel.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <script type="text/javascript">  

          function exportToExcel() {
            var recordcount = grdRptData.getTotalNumberOfRecords();
            if (recordcount > 0) {
                grdRptData.exportToExcel();
            }
         }

        function ValidateListBox_Depot(sender, args) {
            var options = document.getElementById("<%=ddlDept.ClientID%>").options;
            for (var i = 0; i < options.length; i++) {
                if (options[i].selected == true) {
                    args.IsValid = true;
                    return;
                }
            }
            args.IsValid = false;
        }

        function ValidateListBox_ddlBrand(sender, args) {
            var options = document.getElementById("<%=ddlProduct.ClientID%>").options;
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
            $('#ContentPlaceHolder1_ddlProduct').multiselect({
                includeSelectAllOption: true
            });
        });
        $(function () {
            $('#ContentPlaceHolder1_ddlDept').multiselect({
                includeSelectAllOption: true
            });
        });
        

    </script>
    <script type="text/javascript">
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

        var today = new Date();
        var time = today.getDate() +":"+today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();


         $("body").on("click", "#btnExport", function () {
            alert("File Getting Ready Click "+" Ok "+" and Wait for Some time");
            $("[id*=grdRptData]").table2excel({
                filename: "RequisitionVsIssueQty'"+time+"'.xls"
            });
           
        });
        function validateFloatKeyPress(el, evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;

            if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }

            if (charCode == 46 && el.value.indexOf(".") !== -1) {
                return false;
            }

            if (el.value.indexOf(".") !== -1) {
                var range = document.selection.createRange();

                if (range.text != "") {
                }
                else {
                    var number = el.value.split('.');
                    if (number.length == 2 && number[1].length > 1)
                        return false;
                }
            }

            return true;
        }
    </script>
    <asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
            <asp:Image ID="Image1" ImageUrl="../images/loading123.gif" AlternateText="Processing"
                runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
        PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <Triggers>

            <%--<asp:PostBackTrigger ControlID="ButtonExcel"  />--%>
        </Triggers>
        <ContentTemplate>
            <div id="content">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active">
                                <span class="h_icon_he list_images"></span>
                                <h6>Requisition Vs Issue Qty</h6>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="InputTable" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px;">
                                                <fieldset>
                                                    <legend>Stock Details</legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td class="field_title">
                                                                <asp:Label ID="Label8" runat="server" Text="From Date"></asp:Label>
                                                            </td>
                                                            <td width="23%">
                                                                <asp:TextBox ID="txtFromDate" runat="server" Width="70" placeholder="dd/mm/yyyy" MaxLength="10" Enabled="false"></asp:TextBox>
                                                                <asp:ImageButton ID="ImageButtonFromDate" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle" runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtenderFromDate" PopupButtonID="ImageButtonFromDate"
                                                                    runat="server" TargetControlID="txtFromDate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>
                                                            <td class="field_title">
                                                                <asp:Label ID="Label1" runat="server" Text="To Date"></asp:Label>
                                                            </td>
                                                            <td width="23%">
                                                                <asp:TextBox ID="txtTodate" runat="server" Width="70" placeholder="dd/mm/yyyy" MaxLength="10" Enabled="false"></asp:TextBox>
                                                                <asp:ImageButton ID="ImageButtonTodate" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle" runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtenderTodate" PopupButtonID="ImageButtonTodate"
                                                                    runat="server" TargetControlID="txtTodate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>

                                                            <td class="field_title">
                                                                <asp:Label ID="lblPono" runat="server" Text="Department"></asp:Label>
                                                            </td>
                                                            <td width="23%">
                                                                <asp:ListBox ID="ddlDept" runat="server" SelectionMode="Multiple" ValidationGroup="ADD"
                                                                    AppendDataBoundItems="True" TabIndex="4" Width="150px"></asp:ListBox>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td class="field_title">
                                                                <asp:Label ID="Label3" runat="server" Text="Product Name"></asp:Label>
                                                            </td>
                                                            <td width="23%">
                                                                <asp:ListBox ID="ddlProduct" runat="server" SelectionMode="Multiple" ValidationGroup="ADD"
                                                                    AppendDataBoundItems="True" TabIndex="4" Width="150px"></asp:ListBox>
                                                            </td>

                                                            <td class="field_title">
                                                                <asp:Label ID="Label2" runat="server" Text="Details"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlDetails" runat="server" class="chosen-select" 
                                                                    data-placeholder="Vendor From" AppendDataBoundItems="True" Width="150px" ValidationGroup="Save" >
                                                                      <asp:ListItem Value="Y"  Selected="True" Text="Yes"></asp:ListItem>
                                                                    <asp:ListItem Value="N"  Text="No"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>




                                                        </tr>

                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                    </table>

                                    <div class="btn_24_blue" id="Generate" runat="server">
                                        <span class="icon page_white_gear_co"></span>
                                        <asp:Button ID="btnShowReport" runat="server" CssClass="btn_link" Text="Generate Report" OnClick="btnShowReport_Click" />
                                    </div>
                                      <div class="btn_24_blue" id="Div1" runat="server">
                                           <span class="icon excel_document"></span><a href="#" onclick="exportToExcel();">
                                        <%--<input type="button" id="btnExport" style="color: forestgreen" value="Export Excel" />--%>
                                        <asp:Button ID="btnExportExcel"   runat="server" Text="Excel" CssClass="btn_link" /></a>
                                    </div>

                                   
                                
                                     <div class="gridcontent" id="divDetailsData" runat="server">

                                               
                                                      <cc1:Grid ID="grdRptData" runat="server" CallbackMode="true" Serialize="true"
                                            FolderStyle="../GridStyles/premiere_blue" AutoGenerateColumns="true" PageSizeOptions="-1" PageSize="1000"
                                            EnableRecordHover="true" AllowAddingRecords="false" AllowPageSizeSelection="true"
                                            AllowSorting="false" ShowColumnsFooter="true" OnExported="grdRptData_Exported"
                                            ShowEmptyDetails="false" AllowFiltering="true" FolderExports="resources/exports"
                                            OnExporting="grdRptData_Exporting" OnRowDataBound="grdRptData_RowDataBound" >
                                            <ExportingSettings ExportColumnsFooter="true" ExportGroupFooter="true" ExportGroupHeader="true"
                                                FileName="Production Report With Qty" ExportAllPages="true" ExportDetails="true" AppendTimeStamp="false" />
                                            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size: 10px;background-color: #DCE3E8;color: #0000FF;"
                                                CSSExportCellStyle="font-weight: normal;font-size: 10px;color: #0000;background-color: AFEEEE" />
                                                          <FilteringSettings InitialState="Visible" />
                                                                <FilteringSettings MatchingType="AnyFilter" />
                                                         <ScrollingSettings ScrollWidth="100%" ScrollHeight="150" />
                                                    </cc1:Grid>
                                                    </div>
                                               
                                       
                                </asp:Panel>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
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
        </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>

