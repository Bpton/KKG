<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmRptPurchaseDump.aspx.cs" Inherits="VIEW_frmRptPurchaseDump" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register assembly="ProudMonkey.Common.Controls" namespace="ProudMonkey.Common.Controls" tagprefix="cc1" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
           function exportToExcel() {
               grdInvoiceReport.exportToExcel();
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
    
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <Triggers>
    <asp:PostBackTrigger ControlID="btnPDFExport" />
    </Triggers>
        <ContentTemplate>
        
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>Purchase Dump</h6>
                             </div>   
                            <div class="widget_content">
                                <asp:Panel ID="pnlDisplay" runat="server">
                                   <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td>
                                                
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="90" class="field_title"><asp:Label ID="Label3" Text="From Date" runat="server"></asp:Label> <span class="req">*</span></td>
                                                        <td  width="165" class="field_input">
                                                        <asp:TextBox ID="txtInvoiceFromDate" runat="server" Enabled="false" Width="120" placeholder="dd/MM/yyyy"   MaxLength="10" ></asp:TextBox> 
                                                        <asp:ImageButton ID="ImgFromDate" runat="server" ImageUrl="~/images/calendar.png" Width="24px" Height="24px" ImageAlign="AbsMiddle"  /> 
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender4" PopupButtonID="ImgFromDate" runat="server" TargetControlID="txtInvoiceFromDate"
                                                               CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                        </ajaxToolkit:CalendarExtender>                                               
                                                        </td>
                                                        <td width="70" class="field_title"><asp:Label ID="Label4" Text="To Date" runat="server"></asp:Label> <span class="req">*</span></td>
                                                        <td width="165" class="field_input">
                                                        <asp:TextBox ID="txtInvoiceToDate" runat="server" Enabled="false" Width="120" placeholder="dd/MM/yyyy"   MaxLength="10" ></asp:TextBox> 
                                                        <asp:ImageButton ID="ImgToDate" runat="server" ImageUrl="~/images/calendar.png" Width="24px" Height="24px" ImageAlign="AbsMiddle"  /> 
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImgToDate" runat="server" TargetControlID="txtInvoiceToDate"
                                                               CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                        </ajaxToolkit:CalendarExtender>          
                                            </td>
                                            <td class="field_input">
                                            <div class="btn_24_blue">
                                              <span class="icon exclamation_co"></span>
                                                <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn_link" 
                                                    onclick="btnShow_Click"/>
                                            </div>
                                           
                                            <div class="btn_24_blue" >
                                                            <span class="icon doc_excel_table_co"></span>
                                                                  <a href="#" onclick="exportToExcel();"><asp:Button ID="btnExport" runat="server" Text="Export" CssClass="btn_link"/></a><br />
                                                        </div>
                                            
                                             <%--       <div class="btn_24_blue">
                                                    <span class="icon doc_pdf_co"></span>--%>
                                                     <asp:Button ID="btnPDFExport" runat="server" Text="Export PDF" CssClass="btn_link" OnClick="btnPDFExport_Click" Visible="false"/></a><br />
                                                   <%-- </div>--%>
                                            </td>   
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>

                                     
                                                                    
                                    </table>
                                    
                                    <div class="gridcontent-inner"> 
                                            <fieldset>
                                            <legend> Purchase Dump</legend>
                                                <div  style="margin-bottom:8px;">                                                        
                                                       <%-- <asp:GridView ID="grdInvoiceReport" runat="server" Width="100%" ShowFooter="true"
                                                                AutoGenerateColumns="true" 
                                                                 EmptyDataText="No Records Available" OnRowDataBound="grdInvoiceReport_OnRowDataBound">
                                                        </asp:GridView>--%>
                                                          <cc1:Grid ID="grdInvoiceReport" runat="server" AllowPageSizeSelection="true" CallbackMode="false" Serialize="true" FolderStyle="../GridStyles/premiere_blue"
                                        AllowSorting="true"  PageSize="50" ShowColumnsFooter="true" OnRowDataBound="grdInvoiceReport_RowDataBound" 
                                        AllowAddingRecords="false" AllowFiltering="true" FolderExports="resources/exports" OnExported="gvStockreport_Exported" AllowGrouping="true">                   
                                        <ExportingSettings ExportColumnsFooter="true" ExportGroupFooter="true" ExportGroupHeader="true" />
                                        <ScrollingSettings ScrollWidth="100%" />
                                                          </cc1:Grid>
                                                </div>
                                                                                          
                                            </fieldset>
                                        </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                      <cc1:MessageBox ID="MessageBox1" runat="server" /> 
                    <span class="clear"></span>
                </div>
                <span class="clear"></span>
                </div>

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
                        gvStockreport.addFilterCriteria('PRODUCTNAME', OboutGridFilterCriteria.Contains, searchValue);
                        gvStockreport.addFilterCriteria('BATCHNO', OboutGridFilterCriteria.Contains, searchValue);
                        gvStockreport.addFilterCriteria('MRP', OboutGridFilterCriteria.Contains, searchValue);
                        gvStockreport.addFilterCriteria('INVOICESTOCKQTY', OboutGridFilterCriteria.Contains, searchValue);
                        gvStockreport.addFilterCriteria('ASSESMENTPERCENTAGE', OboutGridFilterCriteria.Contains, searchValue);
                        gvStockreport.addFilterCriteria('MFGDATE', OboutGridFilterCriteria.Contains, searchValue);
                        gvStockreport.addFilterCriteria('EXPIRDATE', OboutGridFilterCriteria.Contains, searchValue);
                        gvStockreport.executeFilter();
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
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>



