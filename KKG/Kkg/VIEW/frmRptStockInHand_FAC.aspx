<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmRptStockInHand_FAC.aspx.cs" Inherits="VIEW_frmRptStockInHand_FAC" %>

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
     function ValidateListBox_ddlbsegment(sender, args) {
         var options = document.getElementById("<%=ddlbsegment.ClientID%>").options;
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
            $('#ContentPlaceHolder1_ddlstate').multiselect({
                includeSelectAllOption: true
            });
        });

        $(function () {
            $('#ContentPlaceHolder1_ddlbsegment').multiselect({
                includeSelectAllOption: true
            });
            $("#ContentPlaceHolder1_ddlbsegment").multiselect('selectAll', false);
            $("#ContentPlaceHolder1_ddlbsegment").multiselect('updateButtonText');
        });

        $(function () {
            $('#ContentPlaceHolder1_ddlbrand').multiselect({
                includeSelectAllOption: true
            });
            $("#ContentPlaceHolder1_ddlbrand").multiselect('selectAll', false);
            $("#ContentPlaceHolder1_ddlbrand").multiselect('updateButtonText');
        });

        $(function () {
            $('#ContentPlaceHolder1_ddlcategory ').multiselect({
                includeSelectAllOption: true
            });
            $("#ContentPlaceHolder1_ddlcategory").multiselect('selectAll', false);
            $("#ContentPlaceHolder1_ddlcategory").multiselect('updateButtonText');
        });

       
        $(function () {
            $('#ContentPlaceHolder1_ddlsupplingdepot').multiselect({
                includeSelectAllOption: true
            });
            $("#ContentPlaceHolder1_ddlsupplingdepot").multiselect('selectAll', false);
            $("#ContentPlaceHolder1_ddlsupplingdepot").multiselect('updateButtonText');
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
            <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
          <%--  <asp:PostBackTrigger ControlID="btnshow"  />--%>
        </Triggers>
        <ContentTemplate>
            <div id="content">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active">
                                <span class="h_icon_he list_images"></span>
                                <h6>
                                    Stock In Hand Report</h6>
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
                                                                <asp:Label ID="Label15" runat="server" Text="Stock"></asp:Label>
                                                            </td>

                                                            <td class="field_input" width="100">
                                                                <asp:DropDownList ID="ddlstockdtls" Width="100" runat="server" class="chosen-select"
                                                                    data-placeholder="Select" AppendDataBoundItems="True">
                                                                    <asp:ListItem Value="1" Text="Closing" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Value="2" Text="Opening"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>

                                                            <td class="field_title" width="100">
                                                                <asp:Label ID="Label16" runat="server" Text="Mrp Dtls"></asp:Label>
                                                            </td>

                                                            <td class="field_input" width="100">
                                                                <asp:DropDownList ID="ddlmrpdtls" Width="100" runat="server" class="chosen-select"
                                                                    data-placeholder="Select" AppendDataBoundItems="True">
                                                                    <asp:ListItem Value="1" Text="Mrp" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Value="2" Text="WithOut Mrp"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>

                                                            <td class="field_title" width="100">
                                                                <asp:Label ID="lblproductOwner" runat="server" Text="Product Owner"></asp:Label>
                                                            </td>

                                                            <td class="field_input" width="100">
                                                                <asp:DropDownList ID="ddlProductOwner" Width="100" runat="server" class="chosen-select"
                                                                    data-placeholder="Select" AppendDataBoundItems="True">
                                                                  <%--  <asp:ListItem Value="0" Text="All" Selected="True"></asp:ListItem>--%>
                                                                    <asp:ListItem Value="1" Text="KKG"></asp:ListItem>
                                                                   <%-- <asp:ListItem Value="2" Text="Riya"></asp:ListItem>--%>
                                                                </asp:DropDownList>
                                                            </td>



                                                            </tr>
                                                        <tr>
                                                            <td class="field_title" width="100">
                                                                <asp:Label ID="Label9" runat="server" Text="DATE (As on)"></asp:Label>
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
                                                            <td class="field_title" width="50">ADD
                                                                <asp:Label ID="Label3" runat="server" Text="Depot"> </asp:Label>
                                                            </td>
                                                            <td class="field_input" width="75">
                                                                <asp:ListBox ID="ddlsupplingdepot" runat="server" SelectionMode="Multiple" TabIndex="4"
                                                                    Width="200%" AppendDataBoundItems="True" ValidationGroup="ADD" name="options[]"
                                                                    multiple="multiple"></asp:ListBox>
                                                                      <asp:CustomValidator ID="cvddlsupplingdepot" runat="server" ValidateEmptyText="true"
                                                                ControlToValidate="ddlsupplingdepot" ValidationGroup="ADD" ErrorMessage="Required!"
                                                                Display="Dynamic" ClientValidationFunction="ValidateListBox_Depot" ForeColor="Red"></asp:CustomValidator>
                                                            </td>
                                                            <td class="field_title" width="100">
                                                                <asp:Label ID="Label5" runat="server" Text="Store Location"></asp:Label>
                                                            </td>
                                                            <td class="field_input" width="100">
                                                                <asp:DropDownList ID="ddlstorelocation" Width="150" runat="server" ValidationGroup="ADD"
                                                                    class="chosen-select" AppendDataBoundItems="True">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field_title" width="100" style="display:none">
                                                                <asp:Label ID="Label7" runat="server" Text="Business Segment"></asp:Label>
                                                            </td>
                                                            <td class="field_input" width="100" style="display:none">
                                                                <asp:ListBox ID="ddlbsegment" Width="150" runat="server" ValidationGroup="ADD"
                                                                     SelectionMode="Multiple" TabIndex="4"  data-placeholder="Select Business Segment" AppendDataBoundItems="True">
                                                                </asp:ListBox>                                                                
                                                                 <asp:CustomValidator ID="RequiredFieldValidator2" runat="server" ValidateEmptyText="true"
                                                                    ControlToValidate="ddlbsegment" ValidationGroup="ADD" ErrorMessage="Required!" Display="Dynamic"
                                                                    ClientValidationFunction="ValidateListBox_ddlbsegment" ForeColor="Red"></asp:CustomValidator>
                                                            </td>
                                                         
                                                            <td class="field_title" width="75">
                                                                <asp:Label ID="Label4" runat="server" Text="Brand"></asp:Label><span class="req">*</span>
                                                            </td>
                                                            <td class="field_input" width="75">
                                                                <asp:ListBox ID="ddlbrand" runat="server" SelectionMode="Multiple" TabIndex="4" 
                                                                    ValidationGroup="ADD" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlbrand_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                                                                <asp:CustomValidator ID="CustomValidator10" runat="server" ValidateEmptyText="true"
                                                                    ControlToValidate="ddlbrand" ValidationGroup="ADD" ErrorMessage="Required!" Display="Dynamic"
                                                                    ClientValidationFunction="ValidateListBox_ddlBrand" ForeColor="Red"></asp:CustomValidator>
                                                            </td>
                                                            <td class="field_title" width="100">
                                                                <asp:Label ID="Label1" runat="server" Text="Category"></asp:Label><span class="req">*</span>
                                                            </td>
                                                            <td class="field_input" width="100">
                                                                <asp:ListBox ID="ddlcategory" runat="server" SelectionMode="Multiple" 
                                                                    TabIndex="4" Width="200" ValidationGroup="ADD" AppendDataBoundItems="True"  OnSelectedIndexChanged="ddlcategory_SelectedIndexChanged" AutoPostBack="true" >
                                                                </asp:ListBox>
                                                                <asp:CustomValidator ID="CustomValidator121" runat="server" ValidateEmptyText="true"
                                                                    ControlToValidate="ddlcategory" ValidationGroup="ADD" ErrorMessage="Required!"
                                                                    Display="Dynamic" ClientValidationFunction="ValidateListBox_ddlCategory" ForeColor="Red"></asp:CustomValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field_title" width="50">
                                                                <asp:Label ID="Label10" runat="server" Text="Product"></asp:Label>
                                                            </td>
                                                            <td class="field_input" width="75">
                                                                <asp:DropDownList ID="ddlproduct" runat="server" class="chosen-select" Style="width: 250px;"
                                                                    data-placeholder="Choose a Business"  AppendDataBoundItems="true" AutoPostBack="true"
                                                                    OnSelectedIndexChanged="ddlproduct_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td class="field_title" width="75">
                                                                <asp:Label ID="Label14" runat="server" Text="Packsize"></asp:Label>
                                                            </td>
                                                            <td class="field_input" width="75">
                                                                <asp:DropDownList ID="ddlpacksize" Width="150" runat="server" ValidationGroup="ADD"
                                                                    class="chosen-select" data-placeholder="Select" AppendDataBoundItems="True">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td class="field_title" width="75">
                                                                <asp:Label ID="Label8" runat="server" Text="Batch"></asp:Label>
                                                            </td>
                                                            <td class="field_input" width="75">
                                                                <asp:DropDownList ID="ddlbatch" Width="150" runat="server" ValidationGroup="ADD"
                                                                    class="chosen-select" data-placeholder="Select" AppendDataBoundItems="True">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field_title" width="100">
                                                                <asp:Label ID="Label11" runat="server" Text="MRP"></asp:Label>
                                                            </td>
                                                            <td class="field_input" width="100">
                                                                <asp:TextBox ID="txtmrp" runat="server" MaxLength="10" Text="0" CssClass="mid" onkeypress="return validateFloatKeyPress(this,event);"></asp:TextBox>
                                                            </td>
                                                            <td class="field_title" width="100">
                                                                <asp:Label ID="Label2" runat="server" Text="Size"></asp:Label>
                                                            </td>
                                                            <td class="field_input" width="100">
                                                                <asp:TextBox ID="txtsize" runat="server" Text="0" MaxLength="10" CssClass="mid"></asp:TextBox>
                                                            </td>
                                                            <td class="field_title" width="100">
                                                                <asp:Label ID="Label6" Text="Details" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input" width="100">
                                                                <asp:DropDownList ID="ddldetails" Width="140" runat="server" class="chosen-select"
                                                                    data-placeholder="Select" AppendDataBoundItems="True">
                                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Value="2" Text="Yes"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field_title" width="100">
                                                                <asp:Label ID="Label12" runat="server" Text="Expiry From"></asp:Label>
                                                            </td>
                                                            <td class="field_input" width="100">
                                                                <asp:TextBox ID="txtfromdate" runat="server" Width="80" Enabled="false" placeholder="dd/mm/yyyy"
                                                                    MaxLength="10" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="ADD"></asp:TextBox>
                                                                <asp:ImageButton ID="Imgfrom" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="Imgfrom" runat="server"
                                                                    TargetControlID="txtfromdate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>
                                                            <td class="field_title" width="100">
                                                                <asp:Label ID="Label13" runat="server" Text="Expiry To"></asp:Label>
                                                            </td>
                                                            <td class="field_input" width="100">
                                                                <asp:TextBox ID="txtxtodate" runat="server" Width="80" Enabled="false" placeholder="dd/mm/yyyy"
                                                                    MaxLength="10" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="ADD"></asp:TextBox>
                                                                <asp:ImageButton ID="Imgto" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="Imgto" runat="server"
                                                                    TargetControlID="txtxtodate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>
                                                            <td width="100" colspan="2" class="field_input">
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
                                            <legend>Stock In Hand</legend>
                                            <div class="gridcontent" style="margin-bottom: 8px;">
                                                <cc1:Grid ID="grdstock" runat="server" Serialize="true" FolderStyle="../GridStyles/premiere_blue"
                                                    PageSizeOptions="-1" AutoGenerateColumns="true" AllowPageSizeSelection="true"
                                                    AllowPaging="true" AllowSorting="true" PageSize="200" ShowColumnsFooter="true"
                                                    OnExported="grdstock_Exported" AllowAddingRecords="false" AllowFiltering="true"
                                                    FolderExports="resources/exports" AllowGrouping="true" ViewStateMode="Enabled"
                                                    OnExporting="grdstock_Exporting">
                                                    <ExportingSettings ExportAllPages="true" AppendTimeStamp="false" />
                                                    <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size: 10px;background-color: #DCE3E8;color: #0000FF;"
                                                        CSSExportCellStyle="font-weight: normal;font-size: 10px;color: #0000;background-color: AFEEEE" />
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