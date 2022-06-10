<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmRGPInvoice.aspx.cs" Inherits="VIEW_frmRGPInvoice" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register assembly="ProudMonkey.Common.Controls" namespace="ProudMonkey.Common.Controls" tagprefix="cc1" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
            <%--<div class="page_title">
                <span class="title_icon"><span class="computer_imac"></span></span>
                <h3>
                    Sale Order</h3>
            </div>--%>
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active">
                                <span class="h_icon_he list_images"></span>
                                <h6>
                                    RGP Details</h6>
                                <div id="divnew" runat="server" class="btn_30_light" style="float: right;">
                                    <span id="icon" class="icon add_co"></span>
                                    <asp:Button ID="btnnewentry" runat="server" Text="Add New Record" CssClass="btn_link"
                                        OnClick="btnnewentry_Click" CausesValidation="false" />
                                </div>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="InputTable" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px;">
                                            <blockquote class="quote_blue" id="divcancelorder" runat="server">
                                            <table width="20%" cellpadding="0" cellspacing="0" border="0">
                                                    <asp:Label ID="Label13"  Text="Invoice no" runat="server"></asp:Label></td>
							                     <asp:TextBox ID="txtinvoiceno" runat="server" Width="150" MaxLength="10" placeholder="Invoice no"  Enabled="false"></asp:TextBox>
                                                    
                                            </tr>
                                            </table>
                                             </blockquote>
                                                <fieldset>
                                                <legend>RGP No</legend>
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0"class="form_container_td">
                                                    <tr>
                                                        
                                                    </tr>
                                                    <tr>
                                                        <td width="70" clas s="field_title">
                                                            <asp:Label ID="Label6" runat="server" Text="Date"></asp:Label>
                                                        </td>
                                                        <td width="110" class="field_input">
                                                            <asp:TextBox ID="txtsalenrgodate" runat="server" Width="70" placeholder="dd/mm/yyyy"
                                                                Enabled="false" MaxLength="10" ValidationGroup="datecheckpodetail" onkeypress="return isNumberKeyWithslash(event);" ></asp:TextBox>
                                                            <asp:ImageButton ID="imgPopuppodate" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" TabIndex="1" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender6" PopupButtonID="imgPopuppodate"
                                                                runat="server" TargetControlID="txtsalenrgodate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                        <td width="135" class="innerfield_title">
                                                                            <asp:Label ID="lblTransportMode"  Width="70" Text="VENDOR" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td width="50">
                                                                            <asp:DropDownList ID="ddlTransportMode" runat="server" class="chosen-select" data-placeholder="Select Party Name"
                                                                                   AppendDataBoundItems="True" Width="150px" ValidationGroup="A" AutoPostBack="true" OnSelectedIndexChanged="ddlTransportMode_SelectedIndexChanged">
                                                                                      </asp:DropDownList>
                                                                        </td>
                                                        <td id="Td1" class="field_title" width="75" runat="server">
                                                            <asp:Label ID="Label7" runat="server" Text="Party Name"></asp:Label>
                                                            <asp:RadioButtonList ID="RbApplicable" AutoPostBack="true" RepeatDirection="Horizontal" 
                                                    runat="server"  onClick="toggleHideTr();" OnSelectedIndexChanged="PartyApplicable_SelectedIndexChanged">
                                                    <asp:ListItem Value="Y">Non-Text</asp:ListItem>
                                                    <asp:ListItem Value="N">Text</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                        </td>
                                                        
                                                        <td class="field_input" width="100">
                                                            <asp:TextBox ID="txtname" runat="server" Width="150" MaxLength="50" placeholder="Name" Visible="false"></asp:TextBox>
                                                            <asp:DropDownList ID="ddlparty" runat="server" class="chosen-select" data-placeholder="Select Party Name"
                                                                    AppendDataBoundItems="True" Width="150px" ValidationGroup="A" Visible="false">
                                                                </asp:DropDownList>
                                                        </td>
                                                       <%-- <td id="Td8" class="field_title" width="75" runat="server">
                                                            <asp:Label ID="Label16" runat="server" Text="OR"></asp:Label>
                                                        </td>--%>
                                                       
                                                      
                                                        <td class="field_title" width="75" style="display:none">
                                                            <asp:Label ID="Label19" runat="server" Text="Gate Pass No"></asp:Label>
                                                        </td>
                                                        <td class="field_input" width="100" style="display:none">
                                                            <asp:TextBox ID="lblgatepassno" runat="server" Width="150" MaxLength="50" placeholder="Gate Pass No"></asp:TextBox>
                                                        </td>
                                                        <td  width="110" class="field_title" style="display:none">
                                                            <asp:Label ID="txtgatepassno" runat="server" Text="Gate Pass Date"></asp:Label>
                                                        </td>
                                                        <td class="field_input" width="110" style="display:none">
                                                            <asp:TextBox ID="txtgatepassdate" runat="server" Width="65" placeholder="dd/mm/yyyy"
                                                                Enabled="false" MaxLength="10" ValidationGroup="datecheckpodetail" onkeypress="return isNumberKeyWithslash(event);"></asp:TextBox>
                                                            <asp:ImageButton ID="imgrefPopuppodate" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="imgrefPopuppodate"
                                                                runat="server" TargetControlID="txtgatepassdate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                        <td width="90" class="field_title" id="divsaleorderno" runat="server">
                                                            <asp:Label ID="Label15" runat="server" Text="Order No"></asp:Label>
                                                        </td>
                                                        <td class="field_input" id="divsaleorderno1" runat="server" width="220">
                                                            <asp:TextBox ID="txtsaleorderno" runat="server" Width="213px"></asp:TextBox>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                    </tr>                                                                    


                                                     <tr>
                                                         <td id="Td3" class="field_title" width="75" runat="server">
                                                            <asp:Label ID="Label11" runat="server" Text="LR No"></asp:Label>
                                                        </td>
                                                        <td class="field_input" width="80">
                                                            <asp:TextBox ID="txtlrgrno" runat="server" Width="100" MaxLength="50" placeholder="LR No"></asp:TextBox>
                                                        </td>
                                                        <td id="Td4" class="field_title" width="75" runat="server">
                                                            <asp:Label ID="Label12" runat="server" Text="Vehicle No"></asp:Label>
                                                        </td>
                                                        <td class="field_input" width="100">
                                                            <asp:TextBox ID="txtVehicleno" runat="server" Width="150" MaxLength="50" placeholder="Vehicle No"></asp:TextBox>
                                                        </td>
                                                          <td id="Td5" class="field_title" width="75" runat="server">
                                                            <asp:Label ID="Label3" runat="server" Text="Transport"></asp:Label>
                                                        </td>
                                                        <td class="field_input" width="100">
                                                           <%-- <asp:TextBox ID="txttransport" runat="server" Width="150" MaxLength="50" placeholder="Transport"></asp:TextBox>--%>
                                                            <asp:DropDownList ID="ddltransport" runat="server" class="chosen-select" data-placeholder="Select Transporter Name"
                                                                    AppendDataBoundItems="True" Width="150px" ValidationGroup="A" Visible="true">
                                                                </asp:DropDownList>
                                                        </td>
                                                        <td id="Td6" class="field_title" width="75" runat="server">
                                                            <asp:Label ID="Label4" runat="server" Text="Place of Delivery"></asp:Label>
                                                        </td>
                                                        <td class="field_input" width="100">
                                                            <asp:TextBox ID="txtplascedelivery" runat="server" Width="150" MaxLength="50" placeholder="Place of Delivery"></asp:TextBox>
                                                        </td>
                                                         <td id="Td7" class="field_title" width="75" runat="server">
                                                            <asp:Label ID="Label14" runat="server" Text="GSTIN"></asp:Label>
                                                        </td>
                                                        <td class="field_input" width="100">
                                                            <asp:TextBox ID="txtgstin" runat="server" Width="150" MaxLength="50" placeholder="GSTIN"></asp:TextBox>
                                                        </td>

                                                        <td>&nbsp;</td>
                                                    </tr>                  

                                                  <%--  <tr id="trCurrency" runat="server">
                                                        <td style="padding-bottom:17px;" class="field_title">
                                                            <asp:Label ID="Label3" runat="server" Text="Currency"></asp:Label>&nbsp;<span class="req">*</span>
                                                        </td>
                                                        <td class="field_input" colspan="5">
                                                            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td width="170">
                                                                        <asp:DropDownList ID="ddlCurrency" runat="server" Width="150px" Height="28px" 
                                                                            class="chosen-select" Enabled="false"
                                                                            data-placeholder="Select Currency">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td width="120" style="padding-bottom:8px;" class="innerfield_title">
                                                                        <asp:Label ID="Label14" runat="server" Text="With MRP"></asp:Label>
                                                                    </td>
                                                                    <td style="padding-bottom:8px;">
                                                                        <asp:CheckBox ID="chkMRPTag" runat="server" Text=" " />
                                                                    </td>
                                                                </tr>
                                                            </table>                                                                         
                                                        </td>
                                                    </tr>--%>
                                                  <%--  <tr id="trICDS" runat="server">
                                                         <td style="padding-bottom:17px;" class="field_title">
                                                            <asp:Label ID="Label17" runat="server" Text="ICDS"></asp:Label>
                                                        </td>
                                                        <td width="160">
                                                           <asp:TextBox  ID="txtICDS" runat="server"  placeholder="ICDS No" Width="195" ></asp:TextBox> 
                                                         </td>
                                                          <td class="field_input" colspan="5">
                                                                 <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                                    <tr>
                                                                         <td width="15%"  class="innerfield_title">
                                                                            <asp:Label ID="LabelICDSDate" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ICDS Date" runat="server"></asp:Label>
                                                                         </td>
                                                                           <td width="160">
                                                            <asp:TextBox ID="txtICDSDate" runat="server" Enabled="false" Width="110" placeholder="dd/MM/yyyy"   MaxLength="10"   ></asp:TextBox> 
                                                            <asp:ImageButton ID="ImageButtonICDS" runat="server" ImageUrl="~/images/calendar.png" Width="24px" Height="24px" ImageAlign="AbsMiddle"  />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtenderICDS" TargetControlID="txtICDSDate" PopupButtonID="ImageButtonICDS" 
                                                                               runat="server" Format="dd/MM/yyyy"  Enabled="true" BehaviorID="CalendarExtenderICDSDate" CssClass="cal_Theme1"/>   
                                                                             </td>
                                                                    </tr>
                                                                 </table>
                                                          </td>
                                                    </tr>--%>
                                                    <tr runat="server" id="trAddProduct">
                                            <td class="field_input" colspan="9">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                                        <tr>
                                                             <td width="110" class="field_title">
                                                                <asp:Label ID="Label20" Text="Supplied Item" runat="server"></asp:Label>&nbsp;<span class="req">*</span>
                                                            </td>
                                                            <td width="110" class="field_input">
                                                                <asp:DropDownList ID="ddlSuppliedItem" runat="server" class="chosen-select" data-placeholder="Select Supplied Item"
                                                                    AppendDataBoundItems="True" Width="170" ValidationGroup="A" AutoPostBack="true"
                                                                    OnSelectedIndexChanged="ddlSuppliedItem_SelectedIndexChanged">
                                                                </asp:DropDownList>

                                                            </td>
                                                            <td width="70" class="field_title">
                                                                <asp:Label ID="Label2" runat="server" Text="Product"></asp:Label>&nbsp;<span class="req">*</span>
                                                            </td>
                                                            <td width="250" class="field_input">
                                                                <asp:DropDownList ID="ddlProductName" runat="server" Width="250" 
                                                                    AutoPostBack="true" ValidationGroup="datecheckpodetail" OnSelectedIndexChanged="ddlProductName_SelectedIndexChanged"
                                                                    class="chosen-select" data-placeholder="Select Product Name">
                                                                    <asp:ListItem Text="Select Product Name" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="CV_PRODUCTNAME" runat="server" ControlToValidate="ddlProductName"
                                                                    ValidationGroup="datecheckpodetail" InitialValue="0" SetFocusOnError="true" ErrorMessage="*"
                                                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </td>

                                                            <td width="70" class="field_title">
                                                                <asp:Label ID="Label16" runat="server" Text="Store Location"></asp:Label>&nbsp;<span class="req">*</span>
                                                            </td>
                                                            <td width="250" class="field_input">
                                                                <asp:DropDownList ID="ddlstorelocation" runat="server" Width="180" ValidationGroup="datecheckpodetail" 
                                                                    class="chosen-select" OnSelectedIndexChanged="ddlstorelocation_SelectedIndexChanged" AutoPostBack="true">                                                                   
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlstorelocation"
                                                                    ValidationGroup="datecheckpodetail" InitialValue="0" SetFocusOnError="true" ErrorMessage="*"
                                                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </td>

                                                            <td width="70" class="field_title" style="display:none">
                                                                <asp:Label ID="Label17" runat="server" Text="To Store Location"></asp:Label>&nbsp;<span class="req">*</span>
                                                            </td>
                                                            <td width="250" class="field_input" style="display:none">
                                                                <asp:DropDownList ID="ddltostorelocation" runat="server" Width="180" ValidationGroup="datecheckpodetail" class="chosen-select">
                                                                </asp:DropDownList>
                                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                                    ValidationGroup="datecheckpodetail" InitialValue="0" SetFocusOnError="true" ErrorMessage="*"
                                                                    ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                            </td>

                                                            </tr>
                                                    <tr>
                                                            <td width="60" class="field_input">
                                                                <asp:TextBox ID="txtStockqty" runat="server" Width="60"  Enabled="false" onkeypress="return isNumberKey(event);"
                                                                    ValidationGroup="datecheckpodetail"></asp:TextBox>
                                                                <span class="label_intro">
                                                                    <asp:Label ID="Label5" runat="server" Text="STOCK QTY"></asp:Label>
                                                                   <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtrate"
                                                                    ValidationGroup="datecheckpodetail" InitialValue="" SetFocusOnError="true" ErrorMessage="*Req"
                                                                    ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                                </span>
                                                            </td>      
                                                            <td width="80" class="field_input">
                                                                <asp:TextBox ID="txtqty" runat="server" Width="70"  onkeypress="return isNumberKey(event);"
                                                                    ValidationGroup="datecheckpodetail"></asp:TextBox>
                                                                <span class="label_intro">
                                                                    <asp:Label ID="textqty" runat="server" Text="QTY"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="CV_QTY" runat="server" ControlToValidate="txtqty"
                                                                    ValidationGroup="datecheckpodetail" SetFocusOnError="true" ErrorMessage="Req"
                                                                    Display="None"></asp:RequiredFieldValidator></span>
                                                            </td>
                                                            <td width="80" class="field_input">
                                                                <asp:DropDownList ID="ddlpackingsize" runat="server" Width="100" ValidationGroup="datecheckpodetail"
                                                                    class="chosen-select" data-placeholder="UOM">
                                                                    <asp:ListItem Selected="True" Text="UOM" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <span class="label_intro">
                                                                    <asp:Label ID="Label8" runat="server" Text="UOM"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="CV_CASE" runat="server" ControlToValidate="ddlpackingsize"
                                                                    ValidationGroup="datecheckpodetail" InitialValue="0" SetFocusOnError="true" ErrorMessage="*Req"
                                                                    ForeColor="Red"></asp:RequiredFieldValidator></span>
                                                            </td>
                                                            <td width="60"  class="field_input">
                                                                <asp:TextBox ID="txtrate" runat="server" Width="60"  Enabled="true" onkeypress="return isNumberKeyWithDot(event);"
                                                                    ValidationGroup="datecheckpodetail"></asp:TextBox>
                                                                <span class="label_intro">
                                                                    <asp:Label ID="Label1" runat="server" Text="RATE"></asp:Label>
                                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtrate"
                                                                    ValidationGroup="datecheckpodetail" InitialValue="" SetFocusOnError="true" ErrorMessage="*Req"
                                                                    ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                                </span>
                                                            </td>                                                           
                                                            <td style="display:none;">
                                                            <asp:TextBox ID="txtdiscount" runat="server" Width="90" Text="0"  Enabled="false" onkeypress="return isNumberKey(event);"
                                                                    ValidationGroup="datecheckpodetail"></asp:TextBox>
                                                            </td>
                                                            <td class="field_input" colspan="3">
                                                            <div class="btn_24_blue" >
                                                                <span class="icon add_co"></span>
                                                                <asp:Button ID="btnadd" runat="server" Text="Add"  CssClass="btn_link" ValidationGroup="datecheckpodetail" OnClick="btnadd_Click" />
                                                            </div>
                                                                <asp:HiddenField ID="Hdn_Fld" runat="server" />

                                                                 <asp:HiddenField ID="hdn_igstId" runat="server" />
                                                            <asp:HiddenField ID="hdn_cgstId" runat="server" />
                                                            <asp:HiddenField ID="hdn_sgstId" runat="server" />

                                                            <asp:HiddenField ID="hdn_IgstPer" runat="server" />
                                                            <asp:HiddenField ID="hdn_CgstPer" runat="server" />
                                                            <asp:HiddenField ID="hdn_SgstPer" runat="server" />



                                                            </td>
                                                        </tr>
                                                    </table>
                                            </td>
                                        </tr>
                                                </table>
                                            </fieldset>                                                
                                            </td>
                                        </tr>
                                        
                                        </table>
                                        <div class="gridcontent-inner">
                                                <fieldset>
                                                    <legend>RGP PRODUCT DETAILS</legend>                                                    
                                                        <cc1:Grid ID="gvSaleOrder" runat="server" CallbackMode="true" Serialize="true" ShowFooter="true" ShowColumnsFooter="true"
                                                            AutoGenerateColumns="false" EnableRecordHover="true" AllowAddingRecords="false" PageSize="500" OnRowDataBound="gvSaleOrder_RowDataBound"
                                                            AllowFiltering="true" AllowSorting="false" AllowPageSizeSelection="false" FolderStyle="../GridStyles/premiere_blue">
                                                            <FilteringSettings InitialState="Visible" />
                                                            <Columns>
                                                                <cc1:Column ID="Column1" DataField="PRODUCTID" ReadOnly="true" HeaderText="ID" runat="server"
                                                                    Visible="false" Width="100" />
                                                                <cc1:Column ID="Column12" DataField="Sl" HeaderText="SL" runat="server" Width="60">
                                                                    <TemplateSettings TemplateId="slnoTemplate" />
                                                                </cc1:Column>
                                                                <cc1:Column ID="Column2" DataField="PRODUCTNAME" HeaderText="PRODUCT" runat="server"
                                                                    Width="100" Wrap="true">
                                                                    <FilterOptions>
                                                                        <cc1:FilterOption Type="NoFilter" />
                                                                        <cc1:FilterOption Type="EqualTo" />
                                                                        <cc1:FilterOption Type="Contains" />
                                                                        <cc1:FilterOption Type="DoesNotContain" />
                                                                        <cc1:FilterOption Type="StartsWith" />
                                                                        <cc1:FilterOption Type="EndsWith" />
                                                                        <cc1:FilterOption Type="IsEmpty" />
                                                                        <cc1:FilterOption Type="IsNotEmpty" />
                                                                    </FilterOptions>
                                                                </cc1:Column>
                                                                <cc1:Column ID="Column3" DataField="QTY" HeaderText="QTY" runat="server" Width="100" Wrap="true">
                                                                    <FilterOptions>
                                                                        <cc1:FilterOption Type="NoFilter" />
                                                                        <cc1:FilterOption Type="EqualTo" />
                                                                        <cc1:FilterOption Type="Contains" />
                                                                        <cc1:FilterOption Type="DoesNotContain" />
                                                                        <cc1:FilterOption Type="StartsWith" />
                                                                        <cc1:FilterOption Type="EndsWith" />
                                                                        <cc1:FilterOption Type="IsEmpty" />
                                                                        <cc1:FilterOption Type="IsNotEmpty" />
                                                                    </FilterOptions>
                                                                </cc1:Column>
                                                                 <cc1:Column ID="Column21" DataField="STORELOCATIONNAME" HeaderText="STORENAME" runat="server" Width="80">  </cc1:Column>
                                                                 <cc1:Column ID="Column210000" DataField="STORELOCATIONID" HeaderText="STORENAME" runat="server" Width="80" Visible="false">  </cc1:Column>
                                                                <cc1:Column ID="Column11" DataField="UOMNAME" HeaderText="UOM"
                                                                    runat="server" Width="50" Wrap="true">
                                                                    <FilterOptions>
                                                                        <cc1:FilterOption Type="NoFilter" />
                                                                        <cc1:FilterOption Type="EqualTo" />
                                                                        <cc1:FilterOption Type="Contains" />
                                                                        <cc1:FilterOption Type="DoesNotContain" />
                                                                        <cc1:FilterOption Type="StartsWith" />
                                                                        <cc1:FilterOption Type="EndsWith" />
                                                                        <cc1:FilterOption Type="IsEmpty" />
                                                                        <cc1:FilterOption Type="IsNotEmpty" />
                                                                    </FilterOptions>
                                                                </cc1:Column>
                                                                  <cc1:Column ID="Column5" DataField="RATE" HeaderText="RATE" runat="server" Width="80">
                                                                  </cc1:Column>
                                                                    <cc1:Column ID="Column9" DataField="DISCOUNT" HeaderText="DISCOUNT" runat="server" Width="1" Wrap="true" Visible="false">
                                                                  </cc1:Column>
                                                                   <cc1:Column ID="Column19" DataField="AMOUNT" HeaderText="AMOUNT" runat="server" Width="80">
                                                                  </cc1:Column>


                                                                 <cc1:Column ID="CLMNCGSTID" DataField="CGSTID" HeaderText="CGSTID" Visible="false" runat="server">
                                                                  </cc1:Column>
                                                                  <cc1:Column ID="CLMCGSTPER" DataField="CGSTPER" HeaderText="CGST_PER" runat="server" Width="80">
                                                                  </cc1:Column>
                                                                <cc1:Column ID="Column22" DataField="CGSTAMNT" HeaderText="CGST_AMNT" runat="server" Width="80">
                                                                  </cc1:Column>

                                                                <cc1:Column ID="CLMNSGSTID" DataField="SGSTID" HeaderText="SGSTID" Visible="false" runat="server">
                                                                  </cc1:Column>
                                                                  <cc1:Column ID="CLMSGSTPER" DataField="SGSTPER" HeaderText="SGST_PER" runat="server" Width="80">
                                                                  </cc1:Column>
                                                                <cc1:Column ID="CLMSGSTAMNT" DataField="SGSTAMNT" HeaderText="SGST_AMNT" runat="server" Width="80">
                                                                  </cc1:Column>
                                                                
                                                                <cc1:Column ID="CLMNIGSTID" DataField="IGSTID" HeaderText="IGSTID" Visible="false" runat="server">
                                                                  </cc1:Column>
                                                                  <cc1:Column ID="CLMSISTPER" DataField="IGSTPER" HeaderText="IGST_PER" runat="server" Width="80">
                                                                  </cc1:Column>
                                                                <cc1:Column ID="Column23" DataField="IGSTAMNT" HeaderText="IGST_AMNT" runat="server" Width="80">
                                                                  </cc1:Column>
                                                                
                                                                <cc1:Column ID="ColTOTALAMNT" DataField="TOTALAMNT" HeaderText="TOTAL_AMNT" runat="server" Width="80">
                                                                  </cc1:Column>







                                                                <cc1:Column ID="Column15" DataField="REQUIREDDATE" HeaderText="FROM DATE"
                                                                    runat="server" Width="100" Wrap="true" Visible="false">
                                                                    <FilterOptions>
                                                                        <cc1:FilterOption Type="NoFilter" />
                                                                        <cc1:FilterOption Type="EqualTo" />
                                                                        <cc1:FilterOption Type="Contains" />
                                                                        <cc1:FilterOption Type="DoesNotContain" />
                                                                        <cc1:FilterOption Type="StartsWith" />
                                                                        <cc1:FilterOption Type="EndsWith" />
                                                                        <cc1:FilterOption Type="IsEmpty" />
                                                                        <cc1:FilterOption Type="IsNotEmpty" />
                                                                    </FilterOptions>
                                                                </cc1:Column>
                                                                <cc1:Column ID="Column10" DataField="REQUIREDTODATE" HeaderText="TO DATE"
                                                                    runat="server" Width="100" Wrap="true" Visible="false">
                                                                    <FilterOptions>
                                                                        <cc1:FilterOption Type="NoFilter" />
                                                                        <cc1:FilterOption Type="EqualTo" />
                                                                        <cc1:FilterOption Type="Contains" />
                                                                        <cc1:FilterOption Type="DoesNotContain" />
                                                                        <cc1:FilterOption Type="StartsWith" />
                                                                        <cc1:FilterOption Type="EndsWith" />
                                                                        <cc1:FilterOption Type="IsEmpty" />
                                                                        <cc1:FilterOption Type="IsNotEmpty" />
                                                                    </FilterOptions>
                                                                </cc1:Column>
                                                                

                                                                <cc1:Column HeaderText="DELETE" AllowEdit="false" AllowDelete="true"  Width="70">
                                                                    <TemplateSettings TemplateId="deleteBtnTemplate" />
                                                                </cc1:Column>
                                                            </Columns>


                                                            <FilteringSettings MatchingType="AnyFilter" />
                                                            <Templates>
                                                                <cc1:GridTemplate runat="server" ID="deleteBtnTemplate">
                                                                    <Template>
                                                                        
                                                                        <a href="javascript: //" class="action-icons c-delete" id="btnGridDelete_<%# Container.PageRecordIndex %>"
                                                                            onclick="CallDeleteServerMethod(this)"></a>
                                                                    </Template>
                                                                </cc1:GridTemplate>
                                                                <cc1:GridTemplate runat="server" ID="slnoTemplate">
                                                                    <Template>
                                                                        <asp:Label ID="lblslno" runat="server" Text='<%#Container.PageRecordIndex+1 %>'></asp:Label>
                                                                    </Template>
                                                                </cc1:GridTemplate>
                                                            </Templates>
                                                            <ScrollingSettings ScrollWidth="100%" ScrollHeight="220" />
                                                        </cc1:Grid>
                                                        <asp:Button ID="btngrddelete" runat="server" Text="grddelete" Style="display: none"
                                                            OnClick="btngrddelete_Click" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete this record?');" />
                                                    
                                                </fieldset>
                                            </div>
                                             <div class="gridcontent-inner" style="display:none;">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                                 <tr>
                                                    <td class="field_title">
                                                    <div class="gridcontent-shortstock">
                                                    <fieldset>
                                                      <legend> Terms & Conditions</legend>
                                                                                                
                                                       <cc1:Grid ID="grdTerms" runat="server" CallbackMode="true" Serialize="true"
                                                                      AutoGenerateColumns="false" FolderStyle="../GridStyles/premiere_blue" EnableRecordHover="true"
                                                                      AllowAddingRecords="false" AllowFiltering="false" AllowPaging="false" PageSize="100" AllowSorting="false" 
                                                                      OnRowDataBound="grdTerms_RowDataBound" AllowPageSizeSelection="false">
                                                    
                                                            <FilteringSettings InitialState="Visible" />
                                                            <Columns>
                                                                   <cc1:Column ID="Column16" DataField="SLNO" HeaderText="Sl No" runat="server" Width="60">
                                                                       <TemplateSettings TemplateId="tplTermsNumbering"/>
                                                                   </cc1:Column>
                                                                   
                                                                   <cc1:Column ID="Column17" DataField="DESCRIPTION" ReadOnly="true" HeaderText="TERMS & Conditions" runat="server"
                                                                    Width="320"  Wrap="true"/>
                                                                    <cc1:Column ID="Column18" DataField="ID" ReadOnly="true" HeaderText="TERMSID" runat="server"  Width="60" >
                                                                        <TemplateSettings TemplateID="CheckTemplateTERMS" HeaderTemplateId="HeaderTemplateTERMS" />
                                                                    </cc1:Column>
                                                            </Columns>
                                                           <FilteringSettings MatchingType="AnyFilter" />
                                                            <Templates>
                                                                    <cc1:GridTemplate runat="server" ID="tplTermsNumbering">
                                                                    <Template>
                                                                        <asp:Label ID="lblslnoTax" runat="server" Text='<%#Container.PageRecordIndex+1 %>'></asp:Label>
                                                                    </Template>
                                                                    </cc1:GridTemplate>
                                                                    
                                                                    <cc1:GridTemplate runat="server" ID="CheckTemplateTERMS">
                                                                      <Template>
                                                                          <asp:HiddenField runat="server" ID="hdnTERMSName" Value='<%# Container.DataItem["ID"] %>' />                             
                                                                          <asp:CheckBox ID="ChkIDTERMS" runat="server" ToolTip="<%# Container.Value %>"  Text=" "/>
                                                                      </Template>
                                                                    </cc1:GridTemplate>
                                                            </Templates>
                                                            <ScrollingSettings  ScrollWidth="100%" ScrollHeight="160"  />
                                                         </cc1:Grid>
                                                    
                                                    </fieldset>
                                                </div>
                                                    </td>
                                                    <td class="field_title">
                                                        &nbsp;
                                                    </td>
                                                    <td class="field_title" runat="server" id="tdQty">
                                                    <div class="gridcontent-shortstock">
                                                    <fieldset>
                                                        <legend> Quantity Details</legend>
                                                        <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                                        <tr>
                                                        <td >
                                                                <asp:Label ID="Label18" runat="server" Text="Total Case"></asp:Label>
                                                            </td>
                                                            <td  >
                                                                <asp:TextBox ID="txtTotalCase" runat="server" Width="120" Text="0" Enabled="false"></asp:TextBox> 
                                                            </td>
                                                            <td >
                                                                <asp:Label ID="Label22" runat="server" Text="Total PCS"></asp:Label>
                                                            </td>
                                                            <td  >
                                                                <asp:TextBox ID="txtTotalPCS" runat="server" Width="120" Text="0"  Enabled="false"></asp:TextBox>
                                                            </td> 
                                                        </tr>
                                                    </table>
                                                    </fieldset>
                                                </div>
                                                    </td>
                                                 </tr>
                                                </table>
                                             </div>
                                                    
                                        <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px;">
                                                <fieldset>
                                                    <legend>REMARKS</legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="320">
                                                            <asp:TextBox ID="txtremarks" runat="server" MaxLength="255" TextMode="MultiLine" Height="50" CssClass="large" placeholder="Remarks" class="input_grow"></asp:TextBox>
                                                        </td>
                                                        <td width="15">&nbsp;</td>                                                        
                                                        <td>
                                                            <div class="btn_24_blue" runat="server" id="divBtnSave">
                                                                <span class="icon disk_co"></span>
                                                                <asp:Button ID="btnsave" runat="server" Text="Save" CssClass="btn_link" ValidationGroup="POFooter" OnClick="btnsave_Click" />
                                                            </div>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                            <div class="btn_24_blue">
                                                                <span class="icon cross_octagon_co"></span>
                                                                <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn_link" OnClick="btncancel_Click" CausesValidation="false" />
                                                            </div>
                                                        </td>
                                                    </tr>                                    
                                                </table>
                                              </fieldset>
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
                                                            <asp:Label ID="Label9" runat="server" Text="From Date"></asp:Label>                                                            
                                                        </td>
                                                        <td width="110">
                                                            <asp:TextBox ID="txtfromdate" runat="server" MaxLength="15" Width="70" Enabled="false"
                                                                placeholder="dd/mm/yyyy" ValidationGroup="datecheck"></asp:TextBox>
                                                            <asp:ImageButton ID="imgPopupfromdate" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopupfromdate"
                                                                runat="server" TargetControlID="txtfromdate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                        <td width="70">
                                                            <asp:Label ID="Label10" runat="server" Text="To Date"></asp:Label>                                                           
                                                        </td>
                                                        <td width="110">
                                                            <asp:TextBox ID="txttodate" runat="server" MaxLength="10" Width="70" placeholder="dd/mm/yyyy"
                                                                ValidationGroup="datecheck" Enabled="false"></asp:TextBox>
                                                            <asp:ImageButton ID="imgpopuptodate" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="imgpopuptodate"
                                                                runat="server" TargetControlID="txttodate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                        <td>
                                                        <div class="btn_24_blue">
                                                            <span class="icon find_co"></span>
                                                            <asp:Button ID="btngvfill" runat="server" Text="Search" CssClass="btn_link" ValidationGroup="datecheck" OnClick="btngvfill_Click" />
                                                        </div>
                                                            <asp:HiddenField ID="hdn_saleorderno" runat="server" />
                                                            <asp:HiddenField ID="hdn_saleorderdelete" runat="server" />
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
                                        <cc1:Grid ID="gvsaleorderdetailsdetails" runat="server" CallbackMode="true" Serialize="true" AutoGenerateColumns="false"
                                            AllowAddingRecords="false" AllowFiltering="true" AllowPageSizeSelection="true" PageSize="200"
                                            OnDeleteCommand="DeleteRecordSaleNRGP" FolderStyle="../GridStyles/premiere_blue"
                                            EnableRecordHover="true">
                                            <ClientSideEvents OnBeforeClientDelete="OnBeforesaleorderdetailsDelete" OnClientDelete="OnDeletesaleorderDetails" />
                                            <FilteringSettings InitialState="Visible" />
                                            <Columns>
                                            <cc1:Column ID="Column14" DataField="SALENRGPID" HeaderText="SALENRGPID" runat="server"
                                                Width="10" Visible="false">
                                            </cc1:Column>
                                                <cc1:Column ID="Column4" DataField="Sl" HeaderText="SL" runat="server" Width="60">
                                                    <TemplateSettings TemplateId="slnoTemplatePO" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column6" DataField="NRGPDATE" HeaderText="NRGP DATE" runat="server"
                                                    Width="70%" Wrap="true">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="EqualTo" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="DoesNotContain" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                        <cc1:FilterOption Type="EndsWith" />
                                                        <cc1:FilterOption Type="IsEmpty" />
                                                        <cc1:FilterOption Type="IsNotEmpty" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                               
                                                <cc1:Column ID="Column8" DataField="NAME" HeaderText="CUSTOMER NAME"
                                                    runat="server" Width="150%" Wrap="true">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="EqualTo" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="DoesNotContain" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                        <cc1:FilterOption Type="EndsWith" />
                                                        <cc1:FilterOption Type="IsEmpty" />
                                                        <cc1:FilterOption Type="IsNotEmpty" />
                                                    </FilterOptions>
                                                </cc1:Column>

                                                 <cc1:Column ID="Column7" DataField="SALENRGPNO" HeaderText="INVOICE NO"
                                                    runat="server" Width="150%" Wrap="true">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="EqualTo" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="DoesNotContain" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                        <cc1:FilterOption Type="EndsWith" />
                                                        <cc1:FilterOption Type="IsEmpty" />
                                                        <cc1:FilterOption Type="IsNotEmpty" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                               <%-- <cc1:Column ID="Column22" DataField="TOTALCASE" HeaderText="TOTAL CASE"
                                                    runat="server" Width="100%" Wrap="true"></cc1:Column>
                                                <cc1:Column ID="Column23" DataField="TOTALPCS" HeaderText="TOTAL PCS"
                                                    runat="server" Width="100%" Wrap="true"></cc1:Column>--%>
                                                <cc1:Column ID="Column20" AllowEdit="true" AllowDelete="true" HeaderText="VIEW" runat="server"
                                                    Width="60" Visible="false">
                                                    <TemplateSettings TemplateId="VIEWBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column13" AllowEdit="true" AllowDelete="true" HeaderText="EDIT" runat="server"
                                                    Width="60">
                                                    <TemplateSettings TemplateId="editBtnTemplate" />
                                                </cc1:Column>

                                                 <cc1:Column ID="Column333" AllowEdit="false" AllowDelete="false" HeaderText="PRINT"
                                                    runat="server" Width="80" Wrap="true">
                                                    <TemplateSettings TemplateId="PrintBtnTemplate" />
                                                </cc1:Column>

                                                <cc1:Column HeaderText="DELETE" AllowEdit="false" AllowDelete="true" Visible="false" Width="95">
                                                    <TemplateSettings TemplateId="deleteBtnTemplatePO" />
                                                </cc1:Column>
                                            </Columns>
                                            <FilteringSettings MatchingType="AnyFilter" />
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="VIEWBtnTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="h_icon zoom_sl" title="View" id="btnGridView_<%# Container.PageRecordIndex %>"
                                                            onclick="CallViewServerMethod(this)"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                                <cc1:GridTemplate runat="server" ID="editBtnTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="action-icons c-edit" title="Edit" id="btnGridEdit_<%# Container.PageRecordIndex %>"
                                                            onclick="CallServerMethod(this)"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                                 <cc1:GridTemplate runat="server" ID="PrintBtnTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="filter_btn printer_co" id="btnPOPrint_<%# Container.PageRecordIndex %>"
                                                            onclick="CallServerMethodPrint(this)" title="Print"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="deleteBtnTemplatePO">
                                                    <Template>
                                                        <a href="javascript: //" class="action-icons c-delete" title="Delete" onclick="gvsaleorderdetailsdetails.delete_record(this)">
                                                        </a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                                <cc1:GridTemplate runat="server" ID="slnoTemplatePO">
                                                    <Template>
                                                        <asp:Label ID="lblslnoPO" runat="server" Text='<%#Container.PageRecordIndex+1 %>'></asp:Label>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                           

                                            <ScrollingSettings ScrollWidth="100%" ScrollHeight="290" />
                                        </cc1:Grid>
                                        <asp:Button ID="btngrdedit" runat="server" Text="grdedit" Style="display: none" OnClick="btngrdedit_Click"
                                            CausesValidation="false" />
                                        <asp:Button ID="btngrdview" runat="server" Text="View" Style="display: none" OnClick="btngrdview_Click"
                                            CausesValidation="false" />
                                         <asp:Button ID="btnPOPrint" runat="server" Text="Print"  Style="display: none" OnClick="btnNRGPPrint_Click"
                                            CausesValidation="false" />
                                        <asp:HiddenField ID="hdn_saleorderid" runat="server" />
                                        <asp:HiddenField ID="hdn_bsid" runat="server" />
                                        <asp:HiddenField ID="hdn_bsname" runat="server" />
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
                function ValidateDate(sender, args) {
                    var dateString = document.getElementById(sender.controltovalidate).value;
                    var regex = /((0[0-9]|1[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$/;
                    if (regex.test(dateString)) {
                        var parts = dateString.split("/");
                        var dt = new Date(parts[1] + "/" + parts[0] + "/" + parts[2]);
                        args.IsValid = (dt.getDate() == parts[0] && dt.getMonth() + 1 == parts[1] && dt.getFullYear() == parts[2]);
                    } else {
                        args.IsValid = false;
                    }
                }
            </script>
            <script type="text/javascript">

                function isNumberKey(evt) {
                    var charCode = (evt.which) ? evt.which : event.keyCode;
                    if ((charCode < 48 || charCode > 57) && (charCode != 8))
                        return false;

                    return true;
                }

                function isNumberKeyWithslash(evt) {
                    var charCode = (evt.which) ? evt.which : event.keyCode;
                    if ((charCode < 48 || charCode > 57) && (charCode != 8) && (charCode != 47))
                        return false;

                    return true;
                }

                function isNumberKeyWithDot(evt) {
                    var charCode = (evt.which) ? evt.which : event.keyCode;
                    if ((charCode < 48 || charCode > 57) && (charCode != 8) && (charCode != 46))
                        return false;

                    return true;
                }

                function isNumberKeyWithDotMinus(evt) {
                    var charCode = (evt.which) ? evt.which : event.keyCode;
                    if ((charCode < 48 || charCode > 57) && (charCode != 8) && (charCode != 46) && (charCode != 45))
                        return false;

                    return true;
                }


                function OnBeforeDelete(record) {
                    record.Error = '';
                    document.getElementById("<%=Hdn_Fld.ClientID %>").value = record.ID;
                    if (confirm("Are you sure you want to delete? "))
                        return true;
                    else
                        return false;
                }
                function OnDelete(record) {
                    alert(record.Error);
                }

                function CallServerMethod(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnGridEdit_", "");
                    document.getElementById("<%=hdn_saleorderid.ClientID %>").value = gvsaleorderdetailsdetails.Rows[iRecordIndex].Cells[0].Value;
                    document.getElementById("<%=hdn_saleorderno.ClientID %>").value = gvsaleorderdetailsdetails.Rows[iRecordIndex].Cells[3].Value;
                    document.getElementById("<%=btngrdedit.ClientID %>").click();

                }

                function CallViewServerMethod(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnGridView_", "");
                    document.getElementById("<%=hdn_saleorderid.ClientID %>").value = gvsaleorderdetailsdetails.Rows[iRecordIndex].Cells[0].Value;
                    document.getElementById("<%=hdn_saleorderno.ClientID %>").value = gvsaleorderdetailsdetails.Rows[iRecordIndex].Cells[3].Value;
                    document.getElementById("<%=btngrdview.ClientID %>").click();

                }
            
                function CallDeleteServerMethod(oLink) {
                    var iRecordIndexdelete = oLink.id.toString().replace("btnGridDelete_", "");
                    document.getElementById("<%=hdn_saleorderdelete.ClientID %>").value = gvSaleOrder.Rows[iRecordIndexdelete].Cells[0].Value;
                    document.getElementById("<%=btngrddelete.ClientID %>").click();
                }
                  function CallServerMethodPrint(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnPOPrint_", "");
                    document.getElementById("<%=hdn_saleorderid.ClientID %>").value = gvsaleorderdetailsdetails.Rows[iRecordIndex].Cells[0].Value;
                    document.getElementById("<%=btnPOPrint.ClientID %>").click();

                }
                function OnBeforesaleorderdetailsDelete(record) {
                    record.Error = '';
                    document.getElementById("<%=hdn_saleorderid.ClientID %>").value = record.SALEORDERID;
                    if (confirm("Are you sure you want to delete? "))
                        return true;
                    else
                        return false;
                }
                function OnDeletesaleorderDetails(record) {
                    alert(record.Error);
                }

                function ShowPanel(ctlbtn, ctladd, ctlshow) {
                    if (document.getElementById(ctlbtn)) {
                        document.getElementById(ctladd).style.display = '';
                        document.getElementById(ctlshow).style.display = 'none';

                        return false;
                    }
                }

                function ClosePanel(ctlbtn, ctladd, ctlshow) {
                    if (document.getElementById(ctlbtn)) {
                        document.getElementById(ctlshow).style.display = true;
                        document.getElementById(ctladd).style.display = false;
                        document.getElementById("ctl00_ContentPlaceHolder1_btnAdd").disabled = false;
                        return false;
                    }
                }

                function ShowHideInputs() {

                    if (document.all.InputTable.style.display == "none") {
                        document.all.InputTable.style.display = "inline";
                        document.all.HideInput.value = " Hide ";
                    }
                    Else
                    {
                        document.all.InputTable.style.display = "none";
                        document.all.HideInput.value = " Show ";
                    }

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