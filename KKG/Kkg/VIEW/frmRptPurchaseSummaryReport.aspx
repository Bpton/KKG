<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmRptPurchaseSummaryReport.aspx.cs" Inherits="VIEW_frmRptPurchaseSummaryReport" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register assembly="ProudMonkey.Common.Controls" namespace="ProudMonkey.Common.Controls" tagprefix="cc1" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link href="../css/bootstrap-multiselect.css" rel="stylesheet" />

    <script type="text/javascript" src="../js/bootstrap.js"></script>
    <script type="text/javascript" src="../js/bootstrap-multiselect.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
    function ValidateListBox(sender, args) {
        var options = document.getElementById("<%=ddldepot.ClientID%>").options;
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
        $('#ContentPlaceHolder1_ddldepot').multiselect({
            includeSelectAllOption: true
        });
    });
    </script>

         <script type="text/javascript">
             function exportToExcel() {
                 gvSummaryreport.exportToExcel();
             }         
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
    <%--<asp:PostBackTrigger ControlID="btnExport" />--%>
    </Triggers>
        <ContentTemplate>
        <%--<div class="page_title">
                <span class="title_icon"><span class="computer_imac"></span></span>
                <h3>Beat Master</h3>
            </div>--%>
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6> Purchase Summary Details </h6>
                             </div>   
                            <div class="widget_content">
                                <asp:Panel ID="pnlDisplay" runat="server">
                                   <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                   <tr>
                                    <td>
                                        <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                            <tr>
                                            <td width="90" class="field_title"><asp:Label ID="Label1" Text="From Date" runat="server"></asp:Label> <span class="req">*</span></td>
                                            <td width="200" class="field_input"><asp:TextBox ID="txtfromdate" runat="server" Enabled="false" Width="120" placeholder="dd/MM/yyyy"   MaxLength="10" ></asp:TextBox> 
                                                <asp:ImageButton ID="Imgfrom" runat="server" ImageUrl="~/images/calendar.png" Width="24px" Height="24px" ImageAlign="AbsMiddle"  /> 
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="Imgfrom" runat="server" TargetControlID="txtfromdate"
                                                               CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                            </ajaxToolkit:CalendarExtender>      
                                            </td>
                                            <td width="70" class="field_title"><asp:Label ID="Label2" Text="To Date" runat="server"></asp:Label> <span class="req">*</span></td>
                                            <td width="190" class="field_input">
                                            <asp:TextBox ID="txttodate" runat="server" Enabled="false" Width="120" placeholder="dd/MM/yyyy"   MaxLength="10" ></asp:TextBox> 
                                                <asp:ImageButton ID="ImgToDate" runat="server" ImageUrl="~/images/calendar.png" Width="24px" Height="24px" ImageAlign="AbsMiddle"  /> 
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImgToDate" runat="server" TargetControlID="txttodate"
                                                               CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                            </ajaxToolkit:CalendarExtender>       
                                            </td>
                                            <td width="70" class="field_title"><asp:Label ID="Label5" Text="Party" runat="server"></asp:Label> <span class="req">*</span></td>
                                            <td width="200" class="field_input">
                                            <asp:ListBox ID="ddldepot" runat="server" SelectionMode="Multiple"  TabIndex="4" Width="280px" AppendDataBoundItems="True" ValidationGroup="ADD"></asp:ListBox>
                                             <asp:CustomValidator ID="CustomValidator1"  runat="server" ValidateEmptyText="true" ControlToValidate="ddldepot" ValidationGroup="ADD" ErrorMessage="Required!"
                                                     Display="Dynamic" ClientValidationFunction = "ValidateListBox" ForeColor="Red"></asp:CustomValidator>
                                             </td>
                                             <td>&nbsp;</td>
                                            </tr>
                                        </table>
                                        <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                            <tr>
                                            <td width="90" class="field_title" style="display:none;"><asp:Label ID="Label3" Text="Status" runat="server"></asp:Label> <span class="req">*</span></td>
                                            <td width="165" class="field_input" style="display:none;">
                                            <asp:DropDownList ID="ddlstatus" runat="server" AppendDataBoundItems="true" Width="200" Height="28" class="chosen-select" data-placeholder="Choose a Type" >
                                            
                                            <asp:ListItem Text="In-Transit" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Receipt" Value="2" Selected="True"></asp:ListItem>

                                             </asp:DropDownList>
                                             </td>
                                            <td width="70" class="field_title"><asp:Label ID="Label4" Text="Details" runat="server"></asp:Label> <span class="req">*</span></td>
                                            <td width="165" class="field_input">
                                            <asp:DropDownList ID="ddldetails" runat="server" AppendDataBoundItems="true" Width="200" Height="28" class="chosen-select" data-placeholder="Choose a Type" >
                                            
                                            <asp:ListItem Text="Yes" Value="1" Selected="True"></asp:ListItem>
                                            <%--<asp:ListItem Text="No" Value="2"></asp:ListItem>--%>
                                             </asp:DropDownList>     
                                            </td>
                                            <td width="105" class="field_input">
                                            <div class="btn_24_blue">
                                              <span class="icon exclamation_co"></span>
                                                <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn_link" OnClick="btnShow_Click" ValidationGroup="ADD"/>
                                            </div>                                            
                                            </td>
                                            <td width="150" class="field_input">
                                            <div class="btn_24_blue">
                                            <span class="icon doc_excel_table_co"></span>
                                            <a href="#" onclick="exportToExcel();"><asp:Button ID="btnExport" runat="server" Text="Export Excel" CssClass="btn_link"/></a>
                                            </div>
                                            </td> 
                                             <td  class="field_title">
                                                    <div class="btn_24_blue" style="display:none;">
                                                    <span class="icon doc_pdf_co"></span>
                                                     <asp:Button ID="btnPDFExport" runat="server" Text="Export PDF" CssClass="btn_link" OnClick="btnPDFExport_Click"/></a><br />
                                                    </div>
                                            </td>                                       
                                        </tr>
                                        </table>
                                        </td>
                                    </tr>                   
                                    </table>
                                    <ul id="search_box">
                                        <li>
                                            <input name="" id="FilterTextBox" type="text" class="search_input" placeholder="Enter text to search the whole grid"
                                                onkeyup="FilterTextBox_KeyUp();">
                                        </li>
                                        <li>
                                            <input name="" id="OboutButton2" type="submit" value="" class="search_btn" onclick="return performSearch();">
                                        </li>
                                    </ul>
                                    <div class="gridcontent">
                                    <cc1:Grid ID="gvSummaryreport" runat="server" CallbackMode="true" Serialize="true" FolderStyle="../GridStyles/premiere_blue" OnExporting="gvSummaryreport_Exporting" ViewStateMode="Disabled"
                                        AutoGenerateColumns="false" AllowPageSizeSelection="true" AllowSorting="false" PageSizeOptions="-1" PageSize="500" ShowColumnsFooter="true" 
                                        AllowAddingRecords="false" AllowFiltering="true" FolderExports="resources/exports" OnExported="gvSummaryreport_Exported" AllowGrouping="true" AllowPaging="false">                   
                                        <ExportingSettings ExportColumnsFooter="true" ExportGroupFooter="true" ExportGroupHeader="true" FileName="PurchaseSummaryDetails" ExportAllPages="true" ExportDetails="true"/>
                                        
                                                 <CssSettings 
                                                 CSSExportHeaderCellStyle="font-weight: bold;text-decoration: underline;color: #0000FF;"
                                                 CSSExportCellStyle="font-weight: normal;color: #696969;"/>
                                        
                                        <FilteringSettings InitialState="Visible" />
                                        <Columns>
                                            <cc1:Column ID="Column1" DataField="SLNO" ReadOnly="true" HeaderText="SL.NO." runat="server" Visible="false"/>
                                            <cc1:Column ID="Column13" DataField="STOCKRECEIVEDID" ReadOnly="true" HeaderText="STOCKDEPORECEIVEDID"  runat="server" Visible="false"/>
                                            <cc1:Column ID="Column3" DataField="STOCKRECEIVEDDATE" HeaderText="DATE" runat="server" Width="110px">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                </FilterOptions>
                                                </cc1:Column>
                                            <cc1:Column ID="Column2" DataField="VENDORNAME" HeaderText="PARTY" runat="server" Width="300px">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                </FilterOptions>
                                            </cc1:Column>
                                            
                                          <cc1:Column ID="Column8"  DataField="RECEIVEDSALENO" HeaderText="VOUCHER NO." runat="server"
                                                Width="140px">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                </FilterOptions>
                                            </cc1:Column>
                                            <cc1:Column ID="Column9"  DataField="INVOICENO" HeaderText="BILL NO." runat="server"
                                                Width="140px">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                </FilterOptions>
                                            </cc1:Column>

                                            <cc1:Column ID="Column4" DataField="INVOICEDATE" HeaderText="BILL DATE" FooterText="true" runat="server" Width="90px">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                </FilterOptions>
                                            </cc1:Column>
                                            <cc1:Column ID="Column5"  DataField="TINNO" HeaderText="TIN NO." runat="server"
                                                Width="130px">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                </FilterOptions>
                                            </cc1:Column>
                                            <cc1:Column  DataField="TAXVALUE" HeaderText="CST" runat="server"
                                                Width="130px">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                </FilterOptions>
                                            </cc1:Column>
                                           <cc1:Column ID="Column7"  DataField="GROSSAMT" HeaderText="GROSS AMT." runat="server">                                                
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                </FilterOptions>
                                            </cc1:Column>
                                             <cc1:Column ID="Column12"  DataField="NETAMT" HeaderText="NET AMT." runat="server">                                                
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                </FilterOptions>
                                            </cc1:Column>
                                                                                   
                                            <cc1:Column ID="Column6" AllowEdit="true" AllowDelete="true" HeaderText="Edit" runat="server" Visible="false"
                                                Width="80">
                                                <TemplateSettings TemplateId="editBtnTemplate" />
                                            </cc1:Column>
                                            <cc1:Column HeaderText="Delete" AllowEdit="false" AllowDelete="true" Width="80" Visible="false">
                                                <TemplateSettings TemplateId="deleteBtnTemplate" />
                                            </cc1:Column>
                                            <cc1:Column HeaderText="TSI Mapping" AllowEdit="false" AllowDelete="true" Width="70"
                                                    Wrap="true" Visible="false">
                                                    <TemplateSettings TemplateId="TSIMapping" />
                                                </cc1:Column>
                                                <cc1:Column HeaderText="Retailer Mapping" AllowEdit="false" AllowDelete="true" Width="70" Visible="false"
                                                    Wrap="true">
                                                    <TemplateSettings TemplateId="RetailerMapping" />
                                                </cc1:Column>
                                                <cc1:Column HeaderText="Distributor Mapping" AllowEdit="false" AllowDelete="true" Width="70" Visible="false"
                                                    Wrap="true">
                                                    <TemplateSettings TemplateId="DistributorMapping" />
                                                </cc1:Column>
                                        </Columns>
                                        <MasterDetailSettings LoadingMode="OnLoad" State="Collapsed" ShowEmptyDetails="false"/>
                            <DetailGrids>
                                <cc1:DetailGrid runat="server" ID="childgrid" AutoGenerateColumns="false" CallbackMode="true" AllowAddingRecords="false" ViewStateMode="Disabled" AllowGrouping="true"
                                     ShowFooter="true" AllowPageSizeSelection="false" AllowPaging="true" AllowSorting="false" FolderExports="resources/exports" OnExported="childgrid_Exported"
                                       FolderStyle="../GridStyles/premiere_blue" ForeignKeys="STOCKRECEIVEDID" PageSize="10" ShowColumnsFooter="true">
                                     <ExportingSettings ExportColumnsFooter="true" ExportGroupFooter="true" ExportGroupHeader="true" ExportAllPages="true" ExportDetails="true" />
                                   <CssSettings 
                                                 CSSExportHeaderCellStyle="font-weight: bold;text-decoration: underline;color: #0000FF;"
                                                 CSSExportCellStyle="font-weight: normal;color: #0000;"/>
                                    <Columns>
                                        <cc1:Column DataField="PRODUCTNAME" HeaderText="PRODUCT" ReadOnly="true" Width="340px" ExportAsText="true" >
                                        </cc1:Column>                                        
                                        
                                        <cc1:Column DataField="PACKINGSIZENAME" HeaderText="PACK SIZE" ReadOnly="true" Width="170px">
                                        </cc1:Column> 
                                        <cc1:Column DataField="QTY" HeaderText="QTY" Width="120px">
                                        </cc1:Column>
                                         <cc1:Column DataField="QTYPCS" HeaderText="QTY(In Pcs)" Width="120px">
                                        </cc1:Column>
                                        <cc1:Column DataField="RATE" HeaderText="RATE" Width="170px">
                                        </cc1:Column>                                        
                                        <cc1:Column DataField="AMOUNT" HeaderText="AMOUNT" Width="115px">
                                        </cc1:Column>
                                        <cc1:Column DataField="VAT ON PURCHASE" HeaderText="VAT ON PURCHASE" Width="140px">
                                        </cc1:Column>
                                        <cc1:Column DataField="EXCISE" HeaderText="EXCISE" Width="100px">
                                        </cc1:Column>
                                        <cc1:Column DataField="ADD.VAT ON PURCHASE" HeaderText="ADD.VAT ON PURCHASE" Width="120px" Wrap="true">
                                        </cc1:Column>
                                    </Columns>
                                    <ScrollingSettings ScrollWidth="100%" ScrollHeight="250" />
                                </cc1:DetailGrid>
                            </DetailGrids>
                                        <FilteringSettings MatchingType="AnyFilter" />
                                        <Templates>
                                            <cc1:GridTemplate runat="server" ID="editBtnTemplate">
                                                <Template>
                                                    <a href="javascript: //" class="action-icons c-edit" id="btnGridEdit_<%# Container.PageRecordIndex %>"
                                                        onclick="CallServerMethod(this);"></a>
                                                </Template>
                                            </cc1:GridTemplate>
                                        </Templates>
                                        <Templates>
                                            <cc1:GridTemplate runat="server" ID="deleteBtnTemplate">
                                                <Template>
                                                    <a href="javascript: //" class="action-icons c-delete" title="Delete" onclick="gvBeat.delete_record(this)">
                                                    </a>
                                                </Template>
                                            </cc1:GridTemplate>
                                        </Templates>
                                        <Templates>
                                                <cc1:GridTemplate runat="server" ID="TSIMapping">
                                                    <Template>
                                                        <a href="javascript: //" class="h_icon map" title="TSI Mapping" onclick="openTSI('<%# Container.DataItem["NAME"] %>','<%# Container.DataItem["BEAT_ID"] %>')" />
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>    
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="RetailerMapping">
                                                    <Template>
                                                        <a href="javascript: //" class="h_icon map" title="Retailer Mapping" onclick="openRetailer('<%# Container.DataItem["NAME"] %>','<%# Container.DataItem["BEAT_ID"] %>')" />
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>   
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="DistributorMapping">
                                                    <Template>
                                                        <a href="javascript: //" class="h_icon map" title="Distrbutor Mapping" onclick="openDistrbutor('<%# Container.DataItem["NAME"] %>','<%# Container.DataItem["BEAT_ID"] %>')" />
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>                                     
                                        <ScrollingSettings ScrollWidth="100%" ScrollHeight="290" />
                                    </cc1:Grid>
                                    <asp:HiddenField ID="hdn_excel" runat="server" />
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

