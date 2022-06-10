<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true"
    CodeFile="frmRptSupplyOutward.aspx.cs" Inherits="VIEW_frmRptSupplyOutward" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/bootstrap-multiselect.css" rel="stylesheet" />
    <script type="text/javascript" src="../js/bootstrap.js"></script>
    <script type="text/javascript" src="../js/bootstrap-multiselect.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

 <script type="text/javascript">
  
     function ValidateListBox_Depot(sender, args) {
         var options = document.getElementById("<%=ddlsupplingdepot.ClientID%>").options;
         for (var i = 0; i < options.length; i++) {
             if (options[i].selected == true) {
                 args.IsValid = true;
                 return;
             }
         }
         args.IsValid = false;
     }
     function ValidateListBox_ddlBrand(sender, args) {
         var options = document.getElementById("<%=ddlbrand.ClientID%>").options;
         for (var i = 0; i < options.length; i++) {
             if (options[i].selected == true) {
                 args.IsValid = true;
                 return;
             }
         }
         args.IsValid = false;
     }

     function ValidateListBox_ddlCategory(sender, args) {
         var options = document.getElementById("<%=ddlcategory.ClientID%>").options;
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
            $('#ContentPlaceHolder1_ddlbrand').multiselect({
                includeSelectAllOption: true
            });
        });

        $(function () {
            $('#ContentPlaceHolder1_ddlcategory ').multiselect({
                includeSelectAllOption: true
            });
        });

        $(function () {
            $('#ContentPlaceHolder1_ddlproduct ').multiselect({
                includeSelectAllOption: true
            });
        });

       
        $(function () {
            $('#ContentPlaceHolder1_ddlsupplingdepot').multiselect({
                includeSelectAllOption: true
            });
        });

        $(function () {
            $('#ContentPlaceHolder1_ddlparty').multiselect({
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
           
        <ContentTemplate>
            <div id="content">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active">
                                <span class="h_icon_he list_images"></span>
                                <h6>
                                    Supply Inward/Outward GST Report</h6>
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
                                                            <td class="field_title" width="100">
                                                                <asp:Label ID="Label9" runat="server" Text="From Date"></asp:Label>
                                                            </td>
                                                            <td class="field_input" width="100">
                                                                <asp:TextBox ID="txtdate" runat="server" Width="80" Enabled="false" placeholder="dd/mm/yyyy"
                                                                    MaxLength="10" ValidationGroup="ADD" onkeypress="return isNumberKeyWithslash(event);"></asp:TextBox>
                                                                <asp:ImageButton ID="imgPopup" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender4" PopupButtonID="imgPopup" runat="server"
                                                                    TargetControlID="txtdate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>
                                                            <td class="field_title" width="100">
                                                                <asp:Label ID="Label2" runat="server" Text="To Date"></asp:Label>
                                                            </td>
                                                            <td class="field_input" width="100">
                                                                <asp:TextBox ID="txttodate" runat="server" Width="80" Enabled="false" placeholder="dd/mm/yyyy"
                                                                    MaxLength="10" ValidationGroup="ADD" onkeypress="return isNumberKeyWithslash(event);"></asp:TextBox>
                                                                <asp:ImageButton ID="ImageButton1" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImageButton1" runat="server"
                                                                    TargetControlID="txttodate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>
                                                             <td class="field_title" width="50">
                                                                <asp:Label ID="Label6" runat="server" Text="Type"> </asp:Label>
                                                            </td>
                                                            <td class="field_title" width="50">
                                                                <asp:DropDownList ID="ddltype" runat="server" Width="80">
                                                                <asp:ListItem Selected="True" Text="Select" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="Outward" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Inward" Value="2"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                           
                                                        </tr>
                                                        <tr>  
                                                         <td class="field_title" width="50">
                                                                <asp:Label ID="Label3" runat="server" Text="Depot"> </asp:Label>
                                                            </td>
                                                            <td class="field_input" width="75">
                                                                <asp:ListBox ID="ddlsupplingdepot" runat="server" SelectionMode="Multiple" TabIndex="4"
                                                                    Width="200%" AppendDataBoundItems="True" ValidationGroup="ADD" name="options[]" OnSelectedIndexChanged="ddlsupplingdepot_OnSelectedIndexChanged" AutoPostBack="true"
                                                                    multiple="multiple"></asp:ListBox>
                                                                      <asp:CustomValidator ID="cvddlsupplingdepot" runat="server" ValidateEmptyText="true"
                                                                ControlToValidate="ddlsupplingdepot" ValidationGroup="ADD" ErrorMessage="Required!"
                                                                Display="Dynamic" ClientValidationFunction="ValidateListBox_Depot" ForeColor="Red"></asp:CustomValidator>
                                                            </td> 
                                                        <td class="field_title" width="50">
                                                                <asp:Label ID="Label5" runat="server" Text="Party"> </asp:Label>
                                                            </td>
                                                            <td class="field_input" width="75">
                                                                <asp:ListBox ID="ddlparty" runat="server" SelectionMode="Multiple" TabIndex="4"
                                                                    Width="200%" AppendDataBoundItems="True" ValidationGroup="ADD" name="options[]"
                                                                    multiple="multiple"></asp:ListBox>
                                                                      <asp:CustomValidator ID="CustomValidator1" runat="server" ValidateEmptyText="true"
                                                                ControlToValidate="ddlparty" ValidationGroup="ADD" ErrorMessage="Required!"
                                                                Display="Dynamic" ClientValidationFunction="ValidateListBox_Depot" ForeColor="Red"></asp:CustomValidator>
                                                            </td>                                                         
                                                            <td class="field_title" width="100">
                                                                <asp:Label ID="Label4" runat="server" Text="Brand"></asp:Label><span class="req">*</span>
                                                            </td>
                                                            <td class="field_input" width="100">
                                                                <asp:ListBox ID="ddlbrand" runat="server" SelectionMode="Multiple" TabIndex="4" 
                                                                    ValidationGroup="ADD" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlbrand_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                                                                <asp:CustomValidator ID="CustomValidator10" runat="server" ValidateEmptyText="true"
                                                                    ControlToValidate="ddlbrand" ValidationGroup="ADD" ErrorMessage="Required!" Display="Dynamic"
                                                                    ClientValidationFunction="ValidateListBox_ddlBrand" ForeColor="Red"></asp:CustomValidator>
                                                            </td>
                                                                                                                  
                                                        </tr>
                                                        <tr>
                                                        <td class="field_title" width="100">
                                                                <asp:Label ID="Label1" runat="server" Text="Category"></asp:Label><span class="req">*</span>
                                                            </td>
                                                            <td class="field_input" width="100">
                                                                <asp:ListBox ID="ddlcategory" runat="server" SelectionMode="Multiple" 
                                                                    TabIndex="4" Width="200%" ValidationGroup="ADD" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlcategory_SelectedIndexChanged" AutoPostBack="true">
                                                                </asp:ListBox>
                                                                <asp:CustomValidator ID="CustomValidator12" runat="server" ValidateEmptyText="true"
                                                                    ControlToValidate="ddlcategory" ValidationGroup="ADD" ErrorMessage="Required!"
                                                                    Display="Dynamic" ClientValidationFunction="ValidateListBox_ddlCategory" ForeColor="Red"></asp:CustomValidator>
                                                            </td>     
                                                         <td class="field_title" width="50">
                                                                <asp:Label ID="Label10" runat="server" Text="Product"></asp:Label>
                                                            </td>
                                                            <td class="field_input" width="75">
                                                            <asp:ListBox ID="ddlproduct" runat="server" SelectionMode="Multiple" TabIndex="4"
                                                                    Width="200%" AppendDataBoundItems="True" ValidationGroup="ADD" name="options[]"
                                                                    multiple="multiple"></asp:ListBox>
                                                            </td>  
                                                           <td width="100" class="field_input" colspan="2">
                                                                <div class="btn_24_blue">
                                                                    <span class="icon exclamation_co"></span>
                                                                    <asp:Button ID="btnshow" runat="server" Text="Show" CssClass="btn_link" ValidationGroup="ADD"
                                                                        OnClick="btnshow_Click" />
                                                                </div>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                                <div class="btn_24_blue">
                                                                    <span class="icon doc_excel_table_co"></span><a href="#" onclick="exportToExcel();">
                                                                        <asp:Button ID="btnExport" runat="server" Text="Export Excel" CssClass="btn_link"
                                                                            OnClientClick="return false;" CausesValidation="false" /></a>
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
                                            <legend>Supply Inward/Outward</legend>
                                            <div class="gridcontent" style="margin-bottom: 8px;">
                                                <cc1:Grid ID="grdstock" runat="server" Serialize="true" FolderStyle="../GridStyles/premiere_blue"
                                                    PageSizeOptions="-1" AutoGenerateColumns="false" AllowPageSizeSelection="true" OnRowDataBound="grdstock_OnRowDataBound"
                                                    AllowPaging="true" AllowSorting="true" PageSize="200" ShowColumnsFooter="true"
                                                    OnExported="grdstock_Exported" AllowAddingRecords="false" AllowFiltering="true"
                                                    FolderExports="resources/exports" AllowGrouping="true" ViewStateMode="Enabled"
                                                    OnExporting="grdstock_Exporting">
                                                    <ExportingSettings ExportAllPages="true" AppendTimeStamp="false" />
                                                    <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size: 10px;background-color: #DCE3E8;color: #0000FF;"
                                                        CSSExportCellStyle="font-weight: normal;font-size: 10px;color: #0000;background-color: AFEEEE" />
                                                   <Columns>
                                            <cc1:Column ID="Column1" DataField="DEPOT" ReadOnly="true" HeaderText="DEPOT" runat="server"  Width="85"/>
                                            <cc1:Column ID="Column2" DataField="DEPOT_STATE" HeaderText="DEPOT STATE" runat="server" Width="85">                                              
                                            </cc1:Column>
                                             <cc1:Column ID="Column3" DataField="INVOICE_DATE" HeaderText="INVOICE DATE" runat="server" Width="100" Wrap="true">                                               
                                            </cc1:Column>
                                             <cc1:Column ID="Column4" DataField="PARTY" HeaderText=" PARTY" runat="server" Width="120">                                               
                                            </cc1:Column>
                                             <cc1:Column ID="Column5" DataField="INVOICE_TYPE" HeaderText="INVOICE TYPE" runat="server" Width="80">                                               
                                            </cc1:Column>
                                            <cc1:Column ID="Column6" DataField="INVOICE_NO" HeaderText="INVOICENO" runat="server" Width="150">                                               
                                            </cc1:Column>                                              
                                            <cc1:Column ID="Column8" DataField="GST_NO" HeaderText="GST NO" runat="server" Width="100">                                               
                                            </cc1:Column>  
                                            <cc1:Column ID="Column9" DataField="GST_TYPE" HeaderText="GST TYPE" runat="server" Width="80">                                               
                                            </cc1:Column>
                                            <cc1:Column ID="Column10" DataField="PARTY_STATE" HeaderText="PARTY STATE" runat="server" Width="80">                                               
                                            </cc1:Column>  
                                            <cc1:Column ID="Column11" DataField="TAX_TYPE" HeaderText="TAX TYPE" runat="server" Width="80">                                               
                                            </cc1:Column>
                                            <cc1:Column ID="Column12" DataField="PRODUCT" HeaderText="PRODUCT" runat="server" Width="150">                                               
                                            </cc1:Column> 
                                            <cc1:Column ID="Column7" DataField="HSN_NO" HeaderText="HSN" runat="server" Width="80">                                               
                                            </cc1:Column>
                                            <cc1:Column ID="Column13" DataField="QTY" HeaderText="QTY" runat="server" Width="80">                                               
                                            </cc1:Column>
                                            <cc1:Column ID="Column14" DataField="TAXABLE_AMOUNT" HeaderText="TAXABLE AMOUNT" runat="server" Width="80">                                               
                                            </cc1:Column> 
                                             <cc1:Column ID="Column15" DataField="GST_RATE" HeaderText="GST RATE" runat="server" Width="80">                                               
                                            </cc1:Column> 
                                             <cc1:Column ID="Column16" DataField="IGST" HeaderText="IGST" runat="server" Width="80">                                               
                                            </cc1:Column>                                             
                                            <cc1:Column ID="Column18" DataField="CGST" HeaderText="CGST" runat="server" Width="80">                                               
                                            </cc1:Column> 
                                            <cc1:Column ID="Column19" DataField="SGST" HeaderText="SGST" runat="server" Width="80">                                               
                                            </cc1:Column> 
                                            <cc1:Column ID="Column17" DataField="NET_AMOUNT" HeaderText="NET AMOUNT" runat="server" Width="120">                                               
                                            </cc1:Column>                                          
                                        </Columns>                                                   
                                                    <ScrollingSettings ScrollWidth="100%" ScrollHeight="250" />
                                                </cc1:Grid>
                                                <asp:HiddenField ID="hdn_excel" runat="server" />
                                            </div>
                                        </fieldset>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function exportToExcel() {
            grdstock.exportToExcel();
        }         
    </script>
</asp:Content>
