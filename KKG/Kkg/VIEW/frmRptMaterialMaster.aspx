<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmRptMaterialMaster.aspx.cs" 
    Inherits="FACTORY_frmRptMaterialMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/bootstrap-multiselect.css" rel="stylesheet" />
    <script type="text/javascript" src="../js/bootstrap.js"></script>
    <script type="text/javascript" src="../js/bootstrap-multiselect.js"></script>
   <%-- <script src="../js/table2excel.js"></script>--%>
    <script type="text/javascript">
        window.onload = function () {
            oboutGrid.prototype.selectRecordByClick = function () {
                return;
            }
            oboutGrid.prototype.showSelectionArea = function () {
                return;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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

         var today = new Date();
        var time = today.getDate() +":"+today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();


        // $("body").on("click", "#btnExport", function () {
        //    alert("File Getting Ready Click "+" Ok "+" and Wait for Some time");
        //    $("[id*=gvMiscellaneousProduct]").table2excel({
        //        filename: "MaterialMaster'"+time+"'.xls"
        //    });
           
        //});


    </script>
    <asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
            <asp:Image ID="Image1" ImageUrl="../images/loading123.gif" AlternateText="Processing"
                runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
        PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
        <ContentTemplate>
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_content">
                                <asp:Panel ID="pnlDisplay" runat="server">

                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px;">
                                                <fieldset>
                                                    <legend>MATERIAL MASTER</legend>
                                                    <table width="35%" cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td class="field_title">
                                                                <asp:Label ID="Label4" runat="server" Text="FACTORY"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:DropDownList ID="ddlFactory" runat="server" class="chosen-select" AutoPostBack="true"
                                                                    data-placeholder="Select Factory" AppendDataBoundItems="True" Width="120px"
                                                                    OnSelectedIndexChanged="ddlFactory_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>

                                                            <%--<td class="field_input">
                                                                <div class="btn_24_blue">
                                                                    <span class="icon doc_excel_table_co"></span>
                                                                    <asp:Button ID="btnExport" runat="server" Text="Export Excel" CssClass="btn_link"
                                                                        CausesValidation="false" OnClick="btnExport_Click" />
                                                                </div>
                                                            </td>--%>
                                                            <td>
                                                                 <div class="btn_24_blue" id="Div1" runat="server">
                                            <span class="icon excel_document"></span>
                                          <asp:Button  id="btnExport" style="color:crimson"  onclick="btnExportExcel_click"  runat="server" 
                                             Text="Excel Download" />
                                                            </div>
                                                            </td>
                                                           

                                                        </tr>
                                                    </table>

                                                </fieldset>
                                            </td>
                                        </tr>
                                    </table>

                                    <div class="gridcontent-inner">
                                        <fieldset>
                                            <legend>Material Master Details</legend>
                                            <div style="overflow: hidden;" id="DivHeaderRow"></div>
                                            <div id="DivMainContent" style="overflow: auto; height: 450px; margin-bottom: 8px; margin-right: 6px;" 
                                                onscroll="OnScrollDiv(this);">
                                                <asp:GridView ID="gvMiscellaneousProduct" EmptyDataText="There are no records available."
                                                    ShowFooter="true" Visible="true" ShowHeader="true" GridLines="Horizontal"
                                                    RowStyle-Height="27"
                                                    CssClass="reportgrid" runat="server" AutoGenerateColumns="true" Width="100%">
                                                </asp:GridView>
                                                <div class="clear">
                                                </div>
                                            </div>
                                            <div id="DivFooterRow" style="overflow: hidden;">
                                            </div>
                                        </fieldset>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                    <span class="clear"></span>
                </div>
                <span class="clear"></span>
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
                        gvProduct.addFilterCriteria('CODE', OboutGridFilterCriteria.Contains, searchValue);
                        gvProduct.addFilterCriteria('NAME', OboutGridFilterCriteria.Contains, searchValue);
                        gvProduct.executeFilter();
                        searchTimeout = null;
                        return false;
                    }
                </script>
                <script type="text/javascript">
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

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>