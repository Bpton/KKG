<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmProductionOrderEnd.aspx.cs" Inherits="VIEW_frmProductionOrderEnd" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/bootstrap-multiselect.css" rel="stylesheet" />
    <script type="text/javascript" src="../js/bootstrap.js"></script>
    <script type="text/javascript" src="../js/bootstrap-multiselect.js"></script>
    <style type="text/css">
        .white_content {
            top: 40%;
        }
    </style>

     <style>
     th {
        background: cornflowerblue!important;
        color:white!important;
        position: sticky!important;
        top: 0;
        box-shadow: 0 2px 2px -1px rgb(255, 106, 0);
        width:400px;
    }
    th, td {
        padding: 0.25rem;
    }
</style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" type="text/css" href="../css/jquery.datetimepicker.css" />
    <script type="text/javascript" src="../js/jquery.datetimepicker.js"></script>

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


    <%--calcualte Qty--%>
    <script type="text/javascript" language="javascript">
        function calculateQty(a) {
           
            var BufferQtyID = 0;
            var BufferQty = 0;
            var consuambleId = 0;
            var consuambleQty = 0;
            var wastageQtyId = 0;
            var wastageQty = 0;
            var alreadyWastageQtyId = 0;
            var alreadyWastageQty = 0;
            

            var rowData = a.parentNode.parentNode;
            var rowIndex = rowData.rowIndex - 1;
            var grd = document.getElementById('<%= gvBOM.ClientID %>');
            

            debugger;
            
            BufferQtyID = "ContentPlaceHolder1_gvBOM_txtBufferQty_" + rowIndex;
            BufferQty = parseFloat(document.getElementById(BufferQtyID).value);

            consuambleId = "ContentPlaceHolder1_gvBOM_txtConsumptionQty_" + rowIndex;
            consuambleQty = parseFloat(document.getElementById(consuambleId).value);

            wastageQtyId = "ContentPlaceHolder1_gvBOM_txtWastageQty_" + rowIndex;
            wastageQty = parseFloat(document.getElementById(wastageQtyId).value);

            alreadyWastageQtyId = "ContentPlaceHolder1_gvBOM_txtAlreadyWastageQty_" + rowIndex;
            alreadyWastageQty = parseFloat(document.getElementById(alreadyWastageQtyId).value);

            if (alreadyWastageQty > 0) {

                var t = 0;
                t = parseFloat(alreadyWastageQty + wastageQty).toFixed(4);
                if (t > BufferQty) {
                    alert("Wastage Qty Cannot Be Greater Than BufferQty");
                    document.getElementById(wastageQtyId).value = 0;
                    return;
                }

            }
            else {

                if (wastageQty > BufferQty) {
                    alert("Wastage Qty Cannot Be Greater Than BufferQty");
                    document.getElementById(wastageQtyId).value = 0;
                    return;
                }
            }

            
        }

        function validation(a) {
            debugger;
            var rowData = a.parentNode.parentNode;
            var rowIndex = rowData.rowIndex - 1;
            var grd = document.getElementById('<%= gvItem.ClientID %>');
            var grid = document.getElementById('ContentPlaceHolder1_gvItem');
            var startQtyId = "";
            var startQty = "";

            var endQtyId = "";
            var endQty = "";

            var alreadyQtyId = "";
            var alreadyQty = "";

            startQtyId = "ContentPlaceHolder1_gvItem_TXTQTY_" + rowIndex;
            startQty = parseFloat(document.getElementById(startQtyId).value);

            endQtyId = "ContentPlaceHolder1_gvItem_txtEndQty_" + rowIndex;
            endQty = parseFloat(document.getElementById(endQtyId).value);

            alreadyQtyId = "ContentPlaceHolder1_gvItem_txtAlreadyProductionQty_" + rowIndex;
            alreadyQty = parseFloat(document.getElementById(alreadyQtyId).value);

            var v1 = 0;
            if (alreadyQty > 0) {
 
                v1 = parseFloat(startQty - alreadyQty).toFixed(4);
                if (endQty > v1) {
                    alert("End Qty Cannot Be Greater Than StartQty");
                     document.getElementById(endQtyId).value = 0;
                    return;
                }
            }
            else if (alreadyQty = 0) {
                v1 = parseFloat(startQty).toFixed(4);
                 if (endQty > v1) {
                     alert("End Qty Cannot Be Greater Than StartQty");
                      document.getElementById(endQtyId).value = 0;
                     return;
                 }
            }
            else {

            }



        }
    </script>



    <asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
            <asp:Image ID="Image1" ImageUrl="../images/103.gif" AlternateText="Processing"
                runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
        PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>Production Order End
                                </h6>
                                   <div class="btn_30_light" style="float: right;" runat="server" id="divAddNew">
                                         <span class="icon add_co"></span>
                                                <asp:Button ID="btnAddNew" runat="server" Text="Add New Record" CssClass="btn_link" OnClick="btnAddNew_Click"
                                                    ValidationGroup="Save" />
                                        </div>

                            </div>
                            <tr></tr>
                            <div class="widget_content">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <div id="addDiv" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>

                                             <td class="field_title">
                                                                <asp:Label ID="lblEntryDt" runat="server" Text="Entry Date"></asp:Label>
                                                            </td>
                                                            <td class="field_title">
                                                                <asp:TextBox ID="txtentrydt" runat="server" Width="100" placeholder="dd/mm/yyyy" AutoPostBack="true"
                                                                    MaxLength="10" ValidationGroup="datecheckpodetail" Enabled="false" onkeypress="return isNumberKeyWithslash(event);"></asp:TextBox>
                                                                <asp:ImageButton ID="ImgEntrydt" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImgEntrydt" runat="server"
                                                                    TargetControlID="txtentrydt" CssClass="cal_Theme1" Format="dd/MM/yyyy" >
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>



                                            <td class="field_title">Main Item
                                                                <span class="req">*</span>
                                            </td>
                                            <td class="field_title">
                                                <asp:DropDownList ID="ddlProduct" runat="server" AppendDataBoundItems="true" class="chosen-select"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged" Width="400px"
                                                    data-placeholder="Select Order Number" EnableViewState="True">
                                                </asp:DropDownList>
                                            </td>

                                            <td class="field_title">Production Order Number
                                                                <span class="req">*</span>
                                            </td>
                                            <td class="field_title">
                                                <asp:DropDownList ID="ddlProductionNumber" runat="server" AppendDataBoundItems="true" class="chosen-select"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlProductionNumber_SelectedIndexChanged" Width="400px"
                                                    data-placeholder="Select Order Number">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                    </table>
                                        </div>

                                    <div id="divEditTime" runat="server" style="display:none">
                                        <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td>
                                                <td class="field_title">Production Order Number
                                                                <span class="req">*</span>
                                            </td>
                                                <td class="field_title">
                                                <asp:TextBox ID="txteditProductionNumber" Enabled="false" runat="server" Width="200px" Height="20px" >
                                                </asp:TextBox>
                                               </td>
                                                <td>&nbsp;</td>
                                             
                                                <td class="field_title">Product Name
                                                                <span class="req">*</span>
                                            </td>
                                                 <td class="field_title">
                                                <asp:TextBox ID="txtMainProductName" Enabled="false" runat="server" Width="250px" Height="20px" >
                                                </asp:TextBox>
                                                     </td>
                                            </td>
                                        </tr>
                                            </table>
                                    </div>

                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <td class="field_input" style="padding-left: 10px;">
                                            <div class="gridcontent" id="td_gridview" runat="server">
                                                <table border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                                    <tr>
                                                        <td valign="top">
                                                            <asp:GridView ID="gvItem" runat="server" CssClass="zebra" HeaderStyle-CssClass="header"
                                                                AutoGenerateColumns="false" EmptyDataText=" There are no records available." 
                                                                Width="720px">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Name" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridHeader" HeaderStyle-Width="150px">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblItem" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="NameID" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridHeader"
                                                                        Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblItemID" runat="server" Text='<%#Eval("ID") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="SKU Size" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridHeader" HeaderStyle-Width="40px">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblUnitValue" runat="server" Text='<%#Eval("UNITVALUE") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Specific Gravity" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridHeader" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSpecificGravity" runat="server" Text='<%#Eval("SPECIFICGRAVITY") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Unit" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridHeader">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblUnit" runat="server" Text='<%#Eval("UOMNAME") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="UnitID" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridHeader"
                                                                        Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblUnitID" runat="server" Text='<%#Eval("UOMID") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Mrp Qty" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridHeader">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMRPQTY" runat="server" Text='<%#Eval("QTY") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Pack Size" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridHeader" HeaderStyle-Width="40px">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblConversionQty" runat="server" Text='<%#Eval("CONVERSIONQTY") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="QTY (IN KG/BOXES)" HeaderStyle-CssClass="GridHeader" HeaderStyle-Width="40px"
                                                                        ItemStyle-CssClass="GridHeader">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtQtyinKg" runat="server" Text='<%#Eval("QTYINKG") %>'
                                                                                AutoPostBack="true" Width="50px" Enabled="false"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Production Start Qty" HeaderStyle-CssClass="GridHeader"
                                                                        ItemStyle-CssClass="GridHeader">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TXTQTY" runat="server" Text='<%#Eval("Consumption") %>'
                                                                                AutoPostBack="true" Width="50px" Enabled="false"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Production End Qty" HeaderStyle-CssClass="GridHeader"
                                                                        ItemStyle-CssClass="GridHeader">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtEndQty" runat="server" Text='<%#Eval("EndQty") %>'
                                                                                AutoPostBack="true" Width="50px" onkeyup="validation(this)" OnTextChanged="txtEndQty_TextChanged"></asp:TextBox>
                                                                             <asp:CompareValidator ID="valQtyNumeric1" runat="server" ControlToValidate="txtEndQty"  Display="Dynamic" SetFocusOnError="true"
                                                                                     Text="" ErrorMessage="Error:Enter Only Number!"  ForeColor="Red" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    
                                                                    <asp:TemplateField HeaderText="Already Production Qty" HeaderStyle-CssClass="GridHeader"
                                                                        ItemStyle-CssClass="GridHeader">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtAlreadyProductionQty" runat="server" Text='<%#Eval("AlreadyProductionQty") %>'
                                                                                AutoPostBack="true" Width="50px" Enabled="false"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    

                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td style="padding-top: 10px;">
                                                            <div style="overflow: hidden; width: 100%;" id="DivHeaderRowBOMSource">
                                                            </div>
                                                            <div style="overflow: scroll; border: 1px solid #c3c9ce; margin-bottom: 8px;" onscroll="OnScrollDivBOMSource(this)"
                                                                id="DivMainContentBOMSource">
                                                                <asp:GridView ID="gvBOM" runat="server" AutoGenerateColumns="False" EmptyDataRowStyle-Font-Bold="true"
                                                                    EmptyDataRowStyle-VerticalAlign="Middle" CssClass="zebra" Width="500px" Height="75px"
                                                                    EmptyDataRowStyle-Font-Size="Large">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Product Name" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridHeader" HeaderStyle-Width="380px">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("NAME") %>' Width="180px"></asp:Label>
                                                                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("ITEMID") %>' Visible="false"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Unit" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridHeader">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblUOMNAME" runat="server" Text='<%# Eval("UOMNAME") %>' Height="30px"
                                                                                    Width="30px"></asp:Label>
                                                                                <asp:Label ID="lblUOMID" runat="server" Text='<%# Eval("UNIT") %>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lblCATID" runat="server" Text='<%# Eval("CATID") %>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lblCATNAME" runat="server" Text='<%# Eval("CATNAME") %>' Visible="false"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Qty" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridHeader">
                                                                            <ItemTemplate>
                                                                                <%-- <asp:Label ID="lblQty" runat="server" Text='<%# Eval("Qty") %>' Height="30px" Width="100px"></asp:Label>--%>
                                                                                <asp:TextBox ID="lblQty" runat="server" Text='<%#Eval("Qty") %>' Width="50px" Enabled="false"></asp:TextBox>

                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Buffer Qty" HeaderStyle-CssClass="GridHeader" HeaderStyle-Width="40px" ItemStyle-CssClass="GridHeader">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtBufferQty" runat="server" Text='<%#Eval("BUFFERQTY") %>'
                                                                                    Width="50px" Enabled="false"></asp:TextBox>
                                                                                  <asp:CompareValidator ID="valQtyNumeric11" runat="server" ControlToValidate="txtBufferQty"  Display="Dynamic" SetFocusOnError="true"
                                                                                     Text="" ErrorMessage="Error:Enter Only Number!"  ForeColor="Red" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Net Qty" HeaderStyle-CssClass="GridHeader" HeaderStyle-Width="40px" ItemStyle-CssClass="GridHeader">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtNetQty" runat="server" Text='<%#Eval("NETQTY") %>' Width="50px" Enabled="false"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Consumption Qty" HeaderStyle-CssClass="GridHeader" HeaderStyle-Width="40px" ItemStyle-CssClass="GridHeader">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtConsumptionQty" runat="server" Text='<%#Eval("ConsumptionQty") %>' Width="50px" Enabled="false"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Already Consumption Qty" HeaderStyle-CssClass="GridHeader" HeaderStyle-Width="40px" ItemStyle-CssClass="GridHeader">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtAlreadyConsumptionQty" runat="server" Text='<%#Eval("AlreadyConsumptionQty") %>' Width="50px" Enabled="false"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Wastage Qty" HeaderStyle-CssClass="GridHeader" HeaderStyle-Width="40px" ItemStyle-CssClass="GridHeader">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtWastageQty" runat="server" Text='<%#Eval("WASTAGEQTY") %>'
                                                                                    Width="50px" onkeyup="calculateQty(this)"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Already Wastage Qty" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridHeader">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox runat="server" ID="txtAlreadyWastageQty" Text='<%# Eval("ALREADYWASTAGEQTY") %>'
                                                                                    Width="50px" Enabled="false"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>


                                                                        <asp:TemplateField HeaderText="Source Location" Visible="false" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridHeader">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSourceLocationId" runat="server" Text='<%# Eval("OWNLOCATIONID") %>' Height="30px"
                                                                                    Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Source Location" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridHeader">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSourceLocation" runat="server" Text='<%# Eval("OWNLOCATION") %>' Height="30px"
                                                                                    Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Source Stock Qty" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridHeader">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblstockQty" runat="server" Text='<%# Eval("StockQty") %>' Height="30px"
                                                                                    Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Mapped Location" Visible="false" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridHeader">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblMappedLocationId" runat="server" Text='<%# Eval("TOLOCATIONID") %>' Height="30px"
                                                                                    Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Mapped Location" Visible="false" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridHeader">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblMappedLocation" runat="server" Text='<%# Eval("TOLOCATION") %>' Height="30px"
                                                                                    Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                       
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                            <div id="DivFooterRowBOMSource" style="overflow: hidden">
                                                            </div>
                                                        </td>
                                                        <tr>
                                                            <div id="divStoreLocationWiseStock" runat="server" style="display: none">

                                                                <td style="padding-top: 10px;">
                                                                    <asp:GridView ID="grdStoreWiseStock" runat="server" AutoGenerateColumns="true" EmptyDataRowStyle-Font-Bold="true"
                                                                        EmptyDataRowStyle-VerticalAlign="Middle" CssClass="zebra" Width="450px" Height="75px"
                                                                        EmptyDataRowStyle-Font-Size="Large">
                                                                    </asp:GridView>
                                                                </td>
                                                            </div>
                                                        </tr>

                                                    </tr>

                                                </table>
                                            </div>
                                        </td>
                                        </tr>
                                    </table>
  <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
      <div id="trChkChoice" runat="server">
                                 <tr>
                                   <td class="field_title">Check and Save For Production Order Close
                                     <asp:CheckBox id="chkChoice" runat="server" AutoPostBack="true" OnCheckedChanged="chkChoice_CheckedChanged" ></asp:CheckBox>
                                        
                                     </tr> 
                                     </div>
      <td class="field_title">Close Qty
                                       <asp:Textbox id="txtCloseQty" Enabled="false" Width="80px" Height="10px" Font-Bold="true"  runat="server"></asp:Textbox>
                                           </td>
                                     
      </table>                       
                                      <div id="modlaPopUp" runat="server" style="display:none">
                                             <td class="field_input"> 
                                                <asp:TextBox TextMode="MultiLine" Width="400px" Height="50px" Font-Bold="true"
                                                    ID="txtProductioCloseReason" runat="server">
                                                </asp:TextBox>
                                                 <asp:Label ID="lblErrorMEssage" Text="Please Give a reason for production close" Font-Bold="true" Font-Italic="true" ForeColor="#ff0000" runat="server"></asp:Label>
                                                <asp:RequiredFieldValidator ID="pass" runat="server" ControlToValidate="txtProductioCloseReason" 
                                                    ForeColor="Red" ></asp:RequiredFieldValidator>  
                                            </td>
                                         </div>

                                    

                                    <table>
                                     <tr>
                                         <td>&nbsp;&nbsp;&nbsp;</td>
                                     </tr>
                                        <tr>
                                            <td class="field_input">
                                            <div class="btn_24_blue" id="Div_Submit" runat="server">
                                                <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="btn_link" OnClick="btnSubmit_Click"
                                                    ValidationGroup="Save" />
                                            </div>
                                                </td>
                                            <td>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>

                                            <td class="field_input">
                                            <div class="btn_24_red"  runat="server">
                                                <asp:Button ID="btnClose" runat="server" Text="CLOSE" CssClass="btn_link" OnClick="btnClose_Click"
                                                    ValidationGroup="Save" />
                                            </div>
                                                </td>
                                        </tr>
                                     </table>

                                </asp:Panel>
                                <asp:Panel ID="pnlDisplay" runat="server">
                                  <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_title">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="90">
                                                            <asp:Label ID="Label5" runat="server" Text="From Date"></asp:Label>                                                            
                                                        </td>
                                                        <td width="125">
                                                            <asp:TextBox ID="txtfromdateins" runat="server" MaxLength="10" Width="80" Enabled="false"
                                                                placeholder="dd/mm/yyyy" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="datecheckins"
                                                                Font-Bold="true"></asp:TextBox>
                                                            <asp:ImageButton ID="imgPopup" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="Calendar1" PopupButtonID="imgPopup" runat="server"
                                                                TargetControlID="txtfromdateins" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                            </ajaxToolkit:CalendarExtender>                                                            
                                                        </td>
                                                        <td width="70">
                                                            <asp:Label ID="Label11" runat="server" Text="To Date"></asp:Label>                                                            
                                                        </td>
                                                        <td width="125">
                                                            <asp:TextBox ID="txttodateins" runat="server" MaxLength="10" Width="80" Enabled="false"
                                                                placeholder="dd/mm/yyyy" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="datecheckins"
                                                                Font-Bold="true"></asp:TextBox>
                                                            <asp:ImageButton ID="ImageButton1" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImageButton1"
                                                                runat="server" TargetControlID="txttodateins" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                            </ajaxToolkit:CalendarExtender>                                                           
                                                        </td>
                                                        <td>
                                                            <div class="btn_24_blue">
                                                                <span class="icon find_co"></span>
                                                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn_link" ValidationGroup="datecheckins"
                                                                    OnClick="btnSearch_Click" />
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                                    

 <div class="gridcontent-inner" id="divProductDetails" runat="server">
                                                                <fieldset>
                                            <legend>PRODUCT DETAILS</legend>
                                            <div style="overflow: hidden; width: 100%;" id="DivHeaderRow">
                                            </div>
                                                             <asp:TextBox ID="txtProductSearch" runat="server" Width="210px" Height="25px" Placeholder="Enter ProductName"></asp:TextBox>
                                             <i class="fa fa-search"></i>
                                                                    <asp:Button id="btnProductSearch"  runat="server" Text="search" ForeColor="White" BackColor="#339933" Width="40px" Height="20px" value="search" OnClick="btnProductSearch_Click" CausesValidation="False">
                                             </asp:Button>  
                                           
                                             <td>&nbsp;</td>
                                             <td>&nbsp;</td>
                                             <td>&nbsp;</td>
                                             <td>&nbsp;</td>
                                             <td>&nbsp;</td>
                                             <td>&nbsp;</td><i class="fa fa-refresh"></i>
                                             <asp:Button id="btnReset" runat="server" Text="Show All" value="search" ForeColor="White" BackColor="#000066" Width="60px" Height="20px" OnClick="btnReset_Click" CausesValidation="False"></asp:Button>
                                                            <div style="overflow: scroll; height: 300px;" onscroll="OnScrollDiv(this)" id="DivMainContent">
                                                                <asp:GridView ID="grdReturn" runat="server" AutoGenerateColumns="False" EmptyDataRowStyle-Font-Bold="true"
                                                                    EmptyDataRowStyle-VerticalAlign="Middle" CssClass="zebra" Width="100%" Height="75px"
                                                                    EmptyDataRowStyle-Font-Size="Large">
                                                                    <Columns>

                                                                        <asp:TemplateField HeaderText="Product Name" HeaderStyle-CssClass="GridHeader" Visible="false">
                                                                             <ItemTemplate>
                                                                            <asp:Label id="lblPRODUCTION_ORDERID" runat="server" Text='<%#Eval("PRODUCTION_ORDERID") %>'></asp:Label>
                                                                                  </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Product Name" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridHeader" HeaderStyle-Width="380px">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("PRODUCTNAME") %>' Width="200px"></asp:Label>
                                                                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("PRODUCTID") %>' Visible="false"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="PRODUCTIONNUMBER" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridHeader">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPRODUCTIONNUMBER" runat="server" Text='<%# Eval("PRODUCTIONNUMBER") %>' Height="30px"
                                                                                    Width="150px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                         <asp:TemplateField HeaderText="CREATE DATE" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridHeader">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblCREATION_DATE" runat="server" Text='<%# Eval("CREATION_DATE") %>' Height="30px"
                                                                                    Width="150px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="ENDQTY" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridHeader">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblENDQTY" runat="server" Text='<%# Eval("ENDQTY") %>' Height="30px"
                                                                                    Width="150px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="CLOSEQTY" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridHeader">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblCLOSEQTY" runat="server" Text='<%# Eval("CLOSEQTY") %>' Height="30px"
                                                                                    Width="150px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="VIEW" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridHeader">
                                                                            <ItemTemplate>
                                                                                <asp:Button ID="btnView" runat="server" OnClick="btnView_Click" class="action-icons c-edit " CausesValidation="False"  />
                                                                                  
                                                                            </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                      
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                            
                                                        </fieldset>
                                    </div>
                                    <asp:HiddenField ID="Hdn_Fld" runat="server" />
                                    <asp:HiddenField  ID="hdnProddutionStartQty" runat="server"/>
                                     <asp:HiddenField ID="hdnProddutionRunningQty" runat="server"/>
                                     <asp:HiddenField ID="hdnProddutionEndQty"  runat="server"/>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                    <cc1:MessageBox ID="MessageBox1" runat="server" />
                    <span class="clear"></span>
                </div>
                <span class="clear"></span>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

