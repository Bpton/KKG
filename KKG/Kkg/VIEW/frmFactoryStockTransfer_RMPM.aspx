<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmFactoryStockTransfer_RMPM.aspx.cs" Inherits="FACTORY_frmFactoryStockTransfer_RMPM" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"
    TagPrefix="cc1" %>
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
    <script type="text/javascript">
        function Check() {
            var chkstatus = document.getElementById('<%=chkActive.ClientID %>');
            if (chkstatus.checked) {
                document.getElementById('<%=lbltext.ClientID %>').innerHTML = "Yes!";
                document.getElementById('<%=lbltext.ClientID %>').style.color = 'green';
            } else {
                document.getElementById('<%=lbltext.ClientID %>').innerHTML = "No!";
                document.getElementById('<%=lbltext.ClientID %>').style.color = 'red';
            }
        }
    </script>
    <script type="text/javascript">
        function disableautocompletion(id) {
            var TextBoxControl = document.getElementById(id);
            TextBoxControl.setAttribute("autocomplete", "off");
        }

    </script>
    <asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
            <asp:Image ID="Image1" ImageUrl="~/images/loading123.gif" AlternateText="Processing"
                runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
        PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>
                                    Stock Dispatch Details</h6>
                                <div class="btn_30_light" style="float: right;" id="btnaddhide" runat="server">
                                    <span class="icon add_co"></span>
                                    <asp:Button ID="btnnewentry" runat="server" Text="Add New Record" CssClass="btn_link"
                                        CausesValidation="false" OnClick="btnnewentry_Click" />
                                    <asp:Button ID="btnback" runat="server" Text="Back" CssClass="btn_link" CausesValidation="false"
                                        OnClick="btnback_Click" Visible="false" />
                                </div>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px;">
                                                <fieldset>
                                                    <legend>DISPATCH INFORMATION</legend>
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                                        <tr id="trtransferid" runat="server">
                                                            <td>
                                                                <table cellpadding="0" cellspacing="0" border="0">
                                                                    <tr>
                                                                        <td width="110" class="field_titleTr">
                                                                            <asp:Label ID="lblqualitycontrol" runat="server" Text="Transfer No"></asp:Label>
                                                                        </td>
                                                                        <td width="210" class="field_inputTr">
                                                                            <asp:TextBox ID="txttransferno" Style="width: 195px;" runat="server" placeholder="Auto Generate Transfer No"
                                                                                Enabled="false"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                                    <tr>
                                                                        <td width="110" class="field_titleTr">
                                                                            <asp:Label ID="lblfromdepo" Text="Factory" runat="server"></asp:Label><span class="req">*</span>
                                                                        </td>
                                                                        <td width="210" class="field_inputTr">
                                                                            <asp:DropDownList ID="ddlfromdepot" runat="server" Style="width: 200px;" class="chosen-select"
                                                                                ValidationGroup="check">
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* Required!"
                                                                                Font-Bold="true" ForeColor="Red" ControlToValidate="ddlfromdepot" ValidateEmptyText="false"
                                                                                ValidationGroup="check" SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
                                                                        </td>
                                                                        <td width="110" class="field_titleTr">
                                                                            <asp:Label ID="Label2" Text="To Depot" runat="server"></asp:Label><span class="req">*</span>
                                                                        </td>
                                                                        <td width="210" class="field_inputTr">
                                                                            <asp:DropDownList ID="ddltodepot" runat="server" AppendDataBoundItems="true" Style="width: 200px;"
                                                                                class="chosen-select" OnSelectedIndexChanged="ddltodepot_SelectedIndexChanged"
                                                                                AutoPostBack="true">
                                                                                <asp:ListItem Text="-- Select Depot Name --" Value="0"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="* Required!"
                                                                                Font-Bold="true" ForeColor="Red" ControlToValidate="ddltodepot" ValidateEmptyText="false"
                                                                                ValidationGroup="checksave" SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
                                                                        </td>
                                                                        <td width="110" class="field_titleTr">
                                                                            <asp:Label ID="Label4" Text="eWayBill Key" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td class="field_inputTr">
                                                                            <asp:DropDownList ID="ddlwaybill" runat="server" AppendDataBoundItems="true" Style="width: 200px;
                                                                                background-color: Black" ValidationGroup="checksave" class="chosen-select" data-placeholder="">
                                                                                <asp:ListItem Text="SELECT EWAYBILL KEY" Value="0"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="* Required!"
                                                                                Font-Bold="true" ForeColor="Red" ControlToValidate="ddlwaybill" ValidateEmptyText="false"
                                                                                ValidationGroup="checksave" SetFocusOnError="true"> </asp:RequiredFieldValidator>--%>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="field_titleTr">
                                                                            <asp:Label ID="lblinsurancecompname" Text="INSURANCE CO." runat="server"></asp:Label>
                                                                        </td>
                                                                        <td class="field_inputTr">
                                                                            <asp:DropDownList ID="ddlinsurancecompname" Width="195px" runat="server" class="chosen-select"
                                                                                data-placeholder="" AppendDataBoundItems="True" AutoPostBack="True" 
                                                                                OnSelectedIndexChanged="ddlinsurancecompname_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td class="field_titleTr">
                                                                            <asp:Label ID="Label5" Text="Policy No" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td  class="field_inputTr">
                                                                            <asp:DropDownList ID="ddlinsuranceno" runat="server" AppendDataBoundItems="true"
                                                                                Style="width: 200px; background-color: Black" class="chosen-select" data-placeholder="-- Select Insurance No --">
                                                                                <asp:ListItem Text="SELECT POLICY NO" Value="0"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td class="field_titleTr">
                                                                            <asp:Label ID="Label16" Text="Mode of Trans" runat="server"></asp:Label><span class="req">*</span>
                                                                        </td>
                                                                        <td class="field_inputTr">
                                                                            <asp:DropDownList ID="ddlmodetransport" runat="server" AppendDataBoundItems="true"
                                                                                Style="width: 200px; background-color: Black" ValidationGroup="checksave" class="chosen-select">
                                                                                <asp:ListItem Text="SELECT MODE OF TRANSPORT" Value="0"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="* Required!"
                                                                                ValidationGroup="checksave" Font-Bold="true" ForeColor="Red" ControlToValidate="ddlmodetransport"
                                                                                ValidateEmptyText="false" SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
                                                                        </td>
                                                                        
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                                    <tr>
                                                                    <td width="110" class="field_titleTr">
                                                                            <asp:Label ID="Label3" Text="Transporter" runat="server"></asp:Label><span class="req">*</span>
                                                                        </td>
                                                                        <td width="210" class="field_inputTr">
                                                                            <asp:DropDownList ID="ddltranspoter" runat="server" AppendDataBoundItems="true" Style="width: 200px;
                                                                                background-color: Black" ValidationGroup="checksave" class="chosen-select" data-placeholder="-- Select Transporter Name --">
                                                                                <asp:ListItem Text="SELECT TRANSPORTER NAME" Value="0"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="* Required!"
                                                                                ValidationGroup="checksave" Font-Bold="true" ForeColor="Red" ControlToValidate="ddltranspoter"
                                                                                ValidateEmptyText="false" SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
                                                                        </td>
                                                                        <td width="110" class="field_titleTr">
                                                                            <asp:Label ID="Label6" Text="Vehicle No" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td class="field_inputTr" width="210">
                                                                             <asp:TextBox ID="txtvehicleno" runat="server" placeholder="Enter Vehicle No if any"
                                                                                Width="195px" AutoCompleteType="Disabled" onfocus="disableautocompletion(this.id);"></asp:TextBox>
                                                                        </td>
                                                                        <td  class="field_titleTr" width="110">
                                                                            <asp:Label ID="Label7" Text="LR/GR No" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td class="field_inputTr">
                                                                           <asp:TextBox ID="txtlrgrno" runat="server" Font-Bold="true" placeholder="Enter LR/GR No"
                                                                                Width="195px" AutoCompleteType="Disabled" onfocus="disableautocompletion(this.id);"></asp:TextBox>
                                                                         <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="* Required!"
                                                                                Font-Bold="true" ForeColor="Red" ControlToValidate="txtlrgrno" ValidateEmptyText="false"
                                                                                ValidationGroup="checksave" SetFocusOnError="true"> </asp:RequiredFieldValidator>--%>
                                                                        </td>
                                                                        
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="field_titleTr">
                                                                            <asp:Label ID="Label8" Text="LR/GR Date" runat="server"></asp:Label><span class="req">*</span>
                                                                        </td>
                                                                        <td class="field_inputTr">
                                                                            <asp:TextBox ID="txtlrgrdate" runat="server" Width="120" Enabled="false" placeholder="dd/mm/yyyy"
                                                                                MaxLength="10" ValidationGroup="checksave" onkeypress="return isNumberKeyWithslash(event);">
                                                                            </asp:TextBox>
                                                                            <asp:ImageButton ID="imgPopuplrgrdate" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                                runat="server" Height="24" />
                                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopuplrgrdate"
                                                                                runat="server" TargetControlID="txtlrgrdate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                            </ajaxToolkit:CalendarExtender>
                                                                            <asp:RequiredFieldValidator ID="CV_LRGRDate" runat="server" ControlToValidate="txtlrgrdate"
                                                                                ValidationGroup="checksave" SetFocusOnError="true" ErrorMessage="* Required!"
                                                                                Font-Bold="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                        </td>

                                                                        <td width="110" class="field_titleTr">
                                                                            <asp:Label ID="Label10" Text="Dispatch Date" runat="server"></asp:Label><span class="req">*</span>
                                                                        </td>
                                                                        <td width="210" class="field_inputTr">
                                                                            <asp:TextBox ID="txtchallandate" runat="server" Width="120" Enabled="false" placeholder="dd/mm/yyyy"
                                                                                MaxLength="10" ValidationGroup="check" onkeypress="return isNumberKeyWithslash(event);">
                                                                            </asp:TextBox>
                                                                            <asp:ImageButton ID="imgchallandate" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                                runat="server" Height="24" />
                                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="imgchallandate"
                                                                                runat="server" TargetControlID="txtchallandate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                            </ajaxToolkit:CalendarExtender>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtchallandate"
                                                                                ValidationGroup="checksave" SetFocusOnError="true" ErrorMessage="* Required!"
                                                                                Font-Bold="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                        </td>

                                                                     <td width="110" valign="top" style="padding-top: 15px;" class="field_title">
                                                                      <asp:Label ID="Label34" Text="Store Location" runat="server"></asp:Label>&nbsp;<span class="req">*</span>
                                                                    </td>
                                                                      <td width="162" class="field_input">
                                                                        <asp:DropDownList ID="ddlstorelocation" runat="server" class="chosen-select" data-placeholder=""
                                                                    AppendDataBoundItems="True" Width="165px" ValidationGroup="A" AutoPostBack="true" Enabled="false">
                                                                   
                                                                </asp:DropDownList>

                                                            </td>

                                                                        
                                                                    </tr>
                                                                    <tr>
                                                                        
                                                                        <td class="field_titleTr" width="110">
                                                                            <asp:Label ID="lblgatepassno" Text="GatePass No" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td class="field_inputTr">
                                                                            <asp:TextBox ID="txtgatepassno" runat="server" placeholder="Enter GatePass No" Width="195px"
                                                                                AutoCompleteType="Disabled" onfocus="disableautocompletion(this.id);"></asp:TextBox>
                                                                        </td>
                                                                        
                                                                        <td class="field_titleTr" width="110">
                                                                            <asp:Label ID="lblgatepassdate" runat="server" Text="GatePass Date"></asp:Label>
                                                                        </td>
                                                                        <td class="field_inputTr">
                                                                            <asp:TextBox ID="txtgatepassdate" runat="server" Width="120" Enabled="false" placeholder="dd/mm/yyyy"
                                                                                MaxLength="10" onkeypress="return isNumberKeyWithslash(event);">
                                                                            </asp:TextBox>
                                                                            <asp:ImageButton ID="ImageButton111" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                                runat="server" Height="24" />
                                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender6" PopupButtonID="ImageButton111"
                                                                                runat="server" TargetControlID="txtgatepassdate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                                            </ajaxToolkit:CalendarExtender>
                                                                        </td>
                                                                    </tr>
                                                                         <tr>
                                                        <td class="field_title"><asp:Label ID="Label33" Text="Shipping Address" runat="server"></asp:Label></td>
                                                             <td class="field_input" colspan="8">
                                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                                 <tr>
                                                    
                                                                <td width="220">
                                                                    <asp:TextBox ID="txtShippingAddress" runat="server" CssClass="x_large" Width="200px" Height="60"
                                                                                TextMode="MultiLine" MaxLength="250" onkeypress="return isShippingAddress(event);">
                                                                     </asp:TextBox>
                                                                    </td> 
                                                                  </tr>
                                                                 </table>
                                                              </td>
                                                          </tr>
                                                                    <tr style="display:none";>
                                                                        <td class="field_titleTr" width="110">
                                                                            <asp:Label ID="Label19" Text="WayBill Applicable" runat="server"></asp:Label><span
                                                                                class="req">*</span>
                                                                        </td>
                                                                        <td class="field_inputTr">
                                                                            <asp:DropDownList ID="ddlwaybillapplicale" runat="server" AppendDataBoundItems="true"
                                                                                OnSelectedIndexChanged="ddlwaybillapplicale_SelectedIndexChanged" AutoPostBack="true"
                                                                                Style="width: 200px; background-color: Black" ValidationGroup="checksave" class="chosen-select">
                                                                                <asp:ListItem Selected="True" Text="Yes" Value="1"></asp:ListItem>
                                                                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        
                                                                        <td class="field_titleTr" width="110" >
                                                                            <asp:Label ID="Label21" Text="F-Form Required" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td class="field_inputTr" >
                                                                            <asp:CheckBox ID="chkActive" runat="server" onchange="Check()" Text=" " Enabled="false" />
                                                                            <asp:Label ID="lbltext" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td class="field_title">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td class="field_input">
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>

                                                                    <tr style="display:none";>
                                                                        <td class="field_titleTr">
                                                                            <asp:Label ID="Label9" Text="Invoice No" runat="server"></asp:Label><span class="req">*</span>
                                                                        </td>
                                                                        <td class="field_input">
                                                                            <asp:TextBox ID="txtchallanno" runat="server" placeholder="Enter Invoice No" Width="195px"
                                                                                AutoCompleteType="Disabled" onfocus="disableautocompletion(this.id);"></asp:TextBox>
                                                                          <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="* Required!"
                                                                                Font-Bold="true" ForeColor="Red" ControlToValidate="txtchallanno" ValidateEmptyText="false"
                                                                                ValidationGroup="checksave" SetFocusOnError="true"> </asp:RequiredFieldValidator>--%>
                                                                        </td>
                                                                        <td class="field_titleTr">
                                                                            <asp:Label ID="Label15" runat="server" Text="Invoice&nbsp;Date"></asp:Label><span class="req">*</span>
                                                                        </td>
                                                                        <td class="field_inputTr">
                                                                            <asp:TextBox ID="txttransferdate" runat="server" Width="120" Enabled="false" placeholder="dd/mm/yyyy"
                                                                                MaxLength="10" ValidationGroup="check" onkeypress="return isNumberKeyWithslash(event);">
                                                                            </asp:TextBox>
                                                                            <asp:ImageButton ID="imgPopuppodate" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                                runat="server" Height="24" />
                                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender5" PopupButtonID="imgPopuppodate"
                                                                                runat="server" TargetControlID="txttransferdate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                            </ajaxToolkit:CalendarExtender>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txttransferdate"
                                                                                ValidationGroup="checksave" SetFocusOnError="true" ErrorMessage="* Required!"
                                                                                Font-Bold="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                        <tr id="trproductdetails" runat="server">
                                            <td class="field_input" style="padding-left: 10px;">
                                                <fieldset>
                                                    <legend>PRODUCT DETAILS</legend>
                                                    
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                    <td width="110" class="field_title" style="padding-bottom:15px;"><asp:Label ID="Label1" Text="Category" runat="server"></asp:Label></td>
                                                    <td width="300" class="field_input" colspan="3">
                                                      <asp:DropDownList ID="ddlcategory" runat="server" AppendDataBoundItems="true"
                                                                                Width="250" ValidationGroup="check" class="chosen-select" data-placeholder="-- Select Product Name --"
                                                                                OnSelectedIndexChanged="ddlcategory_SelectedChanged" AutoPostBack="true">
                                                                                <asp:ListItem Text="-- SELECT PRODUCT NAME --" Value="0"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            
                                                  </td> 
                                                  </tr>                        
                                                 <tr>
                                                   <td width="110" class="field_title" style="padding-bottom:15px;"><asp:Label ID="Label24" Text="Product" runat="server"></asp:Label>&nbsp;<span class="req">*</span></td>
                                                   <td width="360" class="field_input" colspan="3">
                                                      <asp:DropDownList ID="ddlproductname" runat="server" AppendDataBoundItems="true"
                                                                                Width="450" ValidationGroup="check" class="chosen-select" data-placeholder="-- Select Product Name --"
                                                                                OnSelectedIndexChanged="ddlproductname_SelectedChanged" AutoPostBack="true">
                                                                                <asp:ListItem Text="-- SELECT PRODUCT NAME --" Value="0"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="* Required!"
                                                                                ValidationGroup="check" Font-Bold="true" ForeColor="Red" ControlToValidate="ddlproductname"
                                                                                ValidateEmptyText="false" SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
                                                  
                                                  </td>

                                                   <td width="70" class="field_title" style="padding-bottom:15px;"><asp:Label ID="Label25" Text="Packsize" runat="server"></asp:Label>&nbsp;<span class="req">*</span></td>
                                                   <td class="field_input" colspan="3" style="padding-bottom:15px;">                                                
                                                        <asp:DropDownList ID="ddlpackingsize" runat="server" AppendDataBoundItems="true"
                                                                                Width="120" ValidationGroup="check" class="chosen-select" data-placeholder="-- Select Pack Size --"
                                                                                OnSelectedIndexChanged="ddlpackingsize_SelectedIndexChanged" AutoPostBack="true">
                                                                                
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="* Required!"
                                                                                Font-Bold="true" ForeColor="Red" ControlToValidate="ddlpackingsize" ValidateEmptyText="false"
                                                                                ValidationGroup="check" SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
                                                   </td>
                                                 </tr>
                                                 <tr>
                                                    <td class="field_title"><asp:Label ID="Label26" Text="BatchNo" runat="server"></asp:Label>&nbsp;<span class="req">*</span></td>
                                                    <td class="field_input" colspan="7">                                                
                                                         <asp:DropDownList ID="ddlbatchno" runat="server" Width="650px" CssClass="common-select" Height="24px" AutoPostBack="true" 
                                                                               AppendDataBoundItems="True"
                                                                               style="font-family: 'Courier New', Courier, monospace" 
                                                             onselectedindexchanged="ddlbatchno_SelectedIndexChanged" ValidationGroup="A" >
                                                         <asp:ListItem Text="SELECT BATCHNO" Value="0"></asp:ListItem>
                                                         </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="ddlbatchno" ValidateEmptyText="false" 
                                                            SetFocusOnError="true" ErrorMessage="Required!"  ValidationGroup="A" ForeColor="Red" InitialValue="0" ></asp:RequiredFieldValidator>
                                                    </td>                                                   
                                                 </tr>
                                                 <tr>
                                                 <td width="110" class="field_title">
                                                                <asp:Label ID="Label91" Text="MRP" runat="server"></asp:Label><span class="req">*</span>
                                                </td>
                                                <td width="150" class="field_input" >
                                                    <asp:TextBox ID="txtmrp" runat="server" placeholder="MRP" Enabled="false"
                                                        Style="width: 80px;"></asp:TextBox>
                                                </td>
                                                <td width="70" class="field_title">
                                                    <asp:Label ID="Label12" Text="Stock QTy" runat="server"></asp:Label><span class="req">*</span>
                                                </td>
                                                <td width="80" class="field_input" >
                                                    <asp:TextBox ID="txtstockqty" runat="server" placeholder="Stock Qty" Enabled="false"
                                                        Style="width: 80px;"></asp:TextBox>
                                                </td>
                                                <td width="100" class="field_title">
                                                    <asp:Label ID="Label14" Text="DISPATCH QTY" runat="server"></asp:Label><span class="req">*</span>
                                                </td>
                                                <td width="122" class="field_input" style="padding-top: 8px;">
                                                    <asp:TextBox ID="txttransferqty" runat="server" placeholder="Enter Dispatch Qty" MaxLength="15"
                                                        onkeypress="return isNumberKeyWithoutDot(event);" AutoCompleteType="Disabled" onfocus="disableautocompletion(this.id);"
                                                        Style="width: 90px;"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="*"
                                                        Font-Bold="true" ForeColor="Red" ControlToValidate="txttransferqty" ValidateEmptyText="false"
                                                        ValidationGroup="check" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    <asp:HiddenField ID="hdn_editedstockqty" runat="server" />
                                                </td>
                                                <td class="field_title" style="padding-left:60px; padding-bottom:8px;">
                                                    <div class="btn_24_blue">
                                                        <span class="icon add_co"></span>
                                                        <asp:Button ID="btnadd" runat="server" Text="ADD" CssClass="btn_link" ValidationGroup="check"
                                                            OnClick="btnadd_Click" CausesValidation="true" />
                                                    </div>
                                                </td>
                                                <td>&nbsp;</td>
                                                 </tr>
                                                 </table>

                                                </fieldset>
                                            </td>
                                        </tr>
                                    </table>                                    

                                    <div class="gridcontent-inner">
                                     <fieldset>
                                         <legend>DISPATCH DETAILS</legend>
                                         <div style="overflow: hidden; width: 100%;" id="DivHeaderRow">
                                            </div>
                                            <div style="overflow: scroll; margin-bottom: 8px; margin-right:6px;" onscroll="OnScrollDiv(this)" id="DivMainContent">
                                            <asp:GridView ID="gvtransfer" runat="server" Width="100%" ShowFooter="true" AutoGenerateColumns="true" 
                                                    EmptyDataText="No Records Available" CssClass="zebra">
                                                    <Columns>                                                    
                                                        <asp:TemplateField HeaderText="DELETE" ItemStyle-Width="20" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <nav style="height: 0px;z-index: 2; padding-left:50px; position: relative; text-align:center;"><span class="badge green"><%# Container.DataItemIndex + 1 %></span></nav>
                                                                 <asp:ImageButton ID="btngrddelete" runat="server" CssClass="action-icons c-delete"
                                                                    OnClientClick="return confirm('Are you sure you want to Delete?')" ToolTip="Delete"
                                                                    OnClick="btngrddelete_Click" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:HiddenField ID="hdndtDespatchDelete" runat="server" />
                                                <asp:HiddenField ID="hdndtPOIDDelete" runat="server" />
                                                <asp:HiddenField ID="hdndtPRODUCTIDDelete" runat="server" />
                                                <asp:HiddenField ID="hdndtBATCHDelete" runat="server" />
                                                 </div>
                                            <div id="DivFooterRow" style="overflow: hidden">
                                            </div>
                                    </fieldset>
                                    </div>

                                     <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                     <tr>
                                            <td class="field_input" style="padding-left: 10px">
                                                <fieldset>
                                                    <legend>Tax Details</legend>
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                         <td width="60" class="innerfield_title">
                                                                <asp:Label ID="Label27" Text="TOT.BASIC AMT." runat="server"></asp:Label>
                                                            </td>
                                                            <td width="150">
                                                                <asp:TextBox runat="server" ID="txtbasicamt" Width="130px" placeholder="TOT.BASIC AMT."
                                                                    Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td width="95" class="innerfield_title" >
                                                                <asp:Label ID="Label28" Text="TAX AMT." runat="server" ></asp:Label>
                                                            </td>
                                                            <td width="150">
                                                                <asp:TextBox ID="txttaxamt" runat="server" Width="130px" Enabled="false" placeholder="TAX AMT."> </asp:TextBox>
                                                            </td>
                                                            <td width="70" class="innerfield_title">
                                                                <asp:Label ID="Label32" Text="(BASIC + TAX) AMT." runat="server" ></asp:Label>
                                                            </td>
                                                            <td width="150">
                                                                <asp:TextBox ID="txttotal" runat="server" Width="130px" Enabled="false" placeholder="(BASIC + TAX) AMT."> </asp:TextBox>
                                                            </td>                                                           
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>

                                     <tr>
                                            <td class="field_input" style="padding-left: 10px">
                                                <fieldset>
                                                    <legend>Gross Details</legend>
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                         <td width="60" class="innerfield_title">
                                                                <asp:Label ID="lblgrossamt" Text="Gross Amt." runat="server"></asp:Label>
                                                            </td>
                                                            <td width="150">
                                                                <asp:TextBox runat="server" ID="txtTotalGross"  Width="130px" placeholder="Gross Amt."
                                                                    Enabled="false"></asp:TextBox>                                                                   
                                                            </td>
                                                             <td width="60" class="innerfield_title">
                                                                <asp:Label ID="lblroundoff" Text="R/O" runat="server"></asp:Label>
                                                            </td>
                                                            <td width="150">
                                                                <asp:TextBox runat="server" ID="txtRoundoff"  Width="130px" placeholder="R/O"
                                                                    Enabled="false"></asp:TextBox>
                                                            </td>

                                                            <td width="60" class="innerfield_title">
                                                                <asp:Label ID="lblnetamt" Text="Net Amt." runat="server"></asp:Label>
                                                            </td>
                                                            <td width="150">
                                                                <asp:TextBox runat="server" ID="txtnetamt"  Width="130px" placeholder="Net Amt."
                                                                    Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td width="95" class="innerfield_title" style="display:none";>
                                                                <asp:Label ID="Label11" Text="Total Case" runat="server" ></asp:Label>
                                                            </td>
                                                            <td width="150" style="display:none";>
                                                                <asp:TextBox ID="txtTotalCase" runat="server" Width="130px" Enabled="false" placeholder="Total Case"> </asp:TextBox>
                                                            </td>
                                                            <td width="70" class="innerfield_title">
                                                                <asp:Label ID="Label13" Text="Total PCS" runat="server" ></asp:Label>
                                                            </td>
                                                            <td width="150">
                                                                <asp:TextBox ID="txtTotalPCS" runat="server" Width="130px" Enabled="false" placeholder="Total PCS."> </asp:TextBox>
                                                            </td>
                                                           
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                     </table>                                    

                                    <div class="gridcontent-inner">
                                    <fieldset>
                                         <legend>OTHER DETAILS</legend>
                                         <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td width="80" class="innerfield_title" style="padding-left: 10px;">
                                                <asp:Label ID="lblRemarks" Text="Remarks" runat="server"></asp:Label>
                                            </td>
                                            <td width="320">
                                                <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Style="width: 300px;
                                                    height: 80px" MaxLength="255"> </asp:TextBox>
                                            </td>
                                            <td width="15">
                                                &nbsp;
                                            </td>
                                            <td width="80" align="left" class="innerfield_title">
                                                <asp:Label ID="lblCheckerNote" Text="Rejection Note" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCheckerNote" runat="server" TextMode="MultiLine" Enabled="false"
                                                    Style="width: 290px; height: 80px" MaxLength="255"> </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="padding-top:10px;">
                                                &nbsp;
                                            </td>
                                            <td colspan="4" style="padding-top:10px;">
                                                <div class="btn_24_blue" id="divbtnsave" runat="server">
                                                    <span class="icon disk_co"></span>
                                                    <asp:Button ID="btnsave" runat="server" Text="Save" CssClass="btn_link" OnClick="btnsave_Click"
                                                        ValidationGroup="checksave" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
                                                    <span class="icon cross_octagon_co"></span>
                                                    <asp:Button ID="btncancel" runat="server" CssClass="btn_link" Text="Cancel" CausesValidation="false"
                                                        OnClick="btncancel_Click" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_green" id="divbtnapprove" runat="server">
                                                    <span class="icon approve_co"></span>
                                                    <asp:LinkButton ID="btnApprove" runat="server" Text="Approve" CssClass="btn_link"
                                                        CausesValidation="false" OnClick="btnApprove_Click" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_red" id="divbtnreject" runat="server">
                                                    <span class="icon reject_co"></span>
                                                    <asp:LinkButton ID="btnReject" runat="server" Text="Reject" CssClass="btn_link" CausesValidation="false"
                                                        OnClick="btnReject_Click" />
                                                </div>
                                                <asp:HiddenField ID="hdn_stocktransferid" runat="server" />
                                                <asp:HiddenField ID="hdn_guid" runat="server" />
                                                <asp:HiddenField ID="hdnDepotID" runat="server" />
                                                <asp:HiddenField ID="hdnrate" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                    </fieldset>
                                  </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlDisplay" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_title">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="90">
                                                            <asp:Label ID="Label17" runat="server" Text="From Date"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="165">
                                                            <asp:TextBox ID="txtfromdateins" runat="server" MaxLength="10" Width="120" Enabled="false"
                                                                placeholder="dd/mm/yyyy" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="datecheckins"></asp:TextBox>
                                                            <asp:ImageButton ID="imgPopup" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="Calendar1" PopupButtonID="imgPopup" runat="server"
                                                                TargetControlID="txtfromdateins" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <asp:RequiredFieldValidator ID="CV_FromDate_QC" runat="server" ControlToValidate="txtfromdateins"
                                                                SetFocusOnError="true" ErrorMessage="From Date is required!" Display="None" ValidationGroup="datecheckins"></asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server"
                                                                TargetControlID="CV_FromDate_QC" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="~/images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                            <asp:CustomValidator ID="CV_FromDateValidation_qc" runat="server" ClientValidationFunction="ValidateDate"
                                                                ControlToValidate="txtfromdateins" SetFocusOnError="true" ErrorMessage="* Invalid Date!"
                                                                Display="None" ValidationGroup="datecheckins" />
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender28" runat="server"
                                                                TargetControlID="CV_FromDateValidation_qc" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="~/images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                        </td>
                                                        <td width="70">
                                                            <asp:Label ID="Label18" runat="server" Text="To Date"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="165">
                                                            <asp:TextBox ID="txttodateins" runat="server" MaxLength="10" Width="120" Enabled="false"
                                                                placeholder="dd/mm/yyyy" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="datecheckins"></asp:TextBox>
                                                            <asp:ImageButton ID="ImageButton1" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender4" PopupButtonID="ImageButton1"
                                                                runat="server" TargetControlID="txttodateins" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <asp:RequiredFieldValidator ID="CV_ToDate_QC" runat="server" ControlToValidate="txttodateins"
                                                                SetFocusOnError="true" ErrorMessage="To Date is required!" Display="None" ValidationGroup="datecheckins"></asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server"
                                                                TargetControlID="CV_ToDate_QC" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="~/images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                            <asp:CustomValidator ID="CV_ToDateValidation_QC" runat="server" ClientValidationFunction="ValidateDate"
                                                                ControlToValidate="txttodateins" SetFocusOnError="true" ErrorMessage="* Invalid Date!"
                                                                Display="None" ValidationGroup="datecheckins" />
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server"
                                                                TargetControlID="CV_ToDateValidation_QC" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="~/images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                        </td>
                                                        <td width="110">
                                                            <div class="btn_24_blue">
                                                                <span class="icon find_co"></span>
                                                                <asp:Button ID="btnSTfind" runat="server" Text="Search" CssClass="btn_link" ValidationGroup="datecheckins"
                                                                    OnClick="btnSTfind_Click" />
                                                            </div>
                                                        </td>
                                                        <td width="60">
                                                            <asp:Label ID="Label29" runat="server" Text="Filter" Visible="false"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlCFormFilter" Width="150" runat="server" class="chosen-select"
                                                                data-placeholder="Choose F-Form Filter" AutoPostBack="true"  Visible="false"
                                                                OnSelectedIndexChanged="ddlCFormFilter_SelectedIndexChanged">
                                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="Without F-Form" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="With F-Form" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="Without Waybill" Value="3"></asp:ListItem>
                                                                <asp:ListItem Text="With Waybill" Value="4"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            &nbsp;
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
                                    <div class="gridcontent-inner" style="padding-left: 10px;">
                                        <cc1:Grid ID="gvStockTransferDetails" runat="server" CallbackMode="true" Serialize="true"
                                            FolderStyle="../GridStyles/premiere_blue" AutoGenerateColumns="false" AllowPageSizeSelection="true"
                                            AllowPaging="true" PageSize="100" AllowAddingRecords="false" AllowFiltering="true"
                                            OnDeleteCommand="DeleteStockTransfer" OnRowDataBound="gvStockTransferDetails_RowDataBound">
                                            <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnClientDelete="OnDelete" />
                                            <FilteringSettings InitialState="Visible" />
                                            <Columns>
                                                <cc1:Column ID="Column3" DataField="STOCKTRANSFERID" HeaderText="TRANSFERID" runat="server"
                                                    Width="60" Visible="false">
                                                </cc1:Column>
                                                <cc1:Column ID="Column20" DataField="Sl" HeaderText="Sl" runat="server" Width="60">
                                                    <TemplateSettings TemplateId="slnoTemplateIN" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column21" DataField="STOCKTRANSFERDATE" HeaderText="DATE"
                                                    runat="server" Width="80">
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
                                                <cc1:Column ID="Column22" DataField="STOCKTRANSFERNO" HeaderText="DISPATCH NO" runat="server"
                                                    Width="120">
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
                                                <cc1:Column ID="Column24" DataField="TODEPOTNAME" HeaderText="DISPATCH TO" runat="server"
                                                    Width="120" Wrap="true">
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
                                                <cc1:Column ID="Column8" DataField="WAYBILLNO" HeaderText="WAYBILL NO" runat="server"
                                                    Width="170" Visible="false">
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
                                                <cc1:Column ID="Column14" DataField="FFORMDATE" HeaderText="F FORMDATE" runat="server"
                                                    Width="120" Wrap="true" Visible="false">
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
                                                <cc1:Column ID="Column15" DataField="FFORMNO" HeaderText="F FORMNO" runat="server"
                                                    Width="110" Wrap="true" Visible="false">
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
                                                <cc1:Column ID="Column16" DataField="FORMREQUIRED" HeaderText="FORM REQUIRED" runat="server"
                                                    Width="100" Wrap="true" Visible="false">
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
                                                <cc1:Column ID="Column17" DataField="WAYBILLKEY" HeaderText="WAYBILL KEY" runat="server"
                                                    Width="170" Visible="false">
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
                                                <cc1:Column ID="Column1" DataField="TODEPOTNAME" HeaderText="DISPATCH TO" runat="server"
                                                    Width="200">
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
                                                <cc1:Column ID="Column2" DataField="INVOICENO" HeaderText="INVOICE NO" runat="server"
                                                    Width="160" Visible="false">
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
                                                <cc1:Column ID="Column4" DataField="TRANSPORTERNAME" HeaderText="TRANSPORTER" runat="server"
                                                    Width="200">
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
                                                <cc1:Column ID="Column27" DataField="ISVERIFIED" HeaderText="ISVERIFIED" Visible="false"
                                                    runat="server" Width="100" />
                                                <cc1:Column ID="Column28" DataField="ISVERIFIEDDESC" HeaderText="FINANCIAL STATUS" runat="server"
                                                    Width="150" Wrap="true">
                                                </cc1:Column>
                                                <cc1:Column ID="Column629" DataField="MOTHERDEPOTID" HeaderText="MOTHERDEPOTID" Visible="false"
                                                    runat="server" Width="100" />
                                                <cc1:Column ID="Column630" DataField="TODEPOTID" HeaderText="TODEPOTID" Visible="false"
                                                    runat="server" Width="100" />
                                                <cc1:Column ID="Column11" DataField="USERNAME" HeaderText="ENTRY USER" runat="server"
                                                    Width="100" />
                                                <cc1:Column ID="Column29" DataField="TOTALCASE" HeaderText="TOTAL CASE" runat="server" Width="100" Visible="false"></cc1:Column>
                                                <cc1:Column ID="Column30" DataField="TOTALPCS" HeaderText="TOTAL PCS" runat="server" Width="125" ></cc1:Column>
                                                <cc1:Column ID="Column936" DataField="NETAMOUNT" HeaderText="NET AMOUNT" runat="server"  Width="120"  />
                                                <cc1:Column ID="Column129" AllowEdit="false" AllowDelete="false" HeaderText="Update Waybill"
                                                    runat="server" Width="85" Wrap="true" Visible="false">
                                                    <TemplateSettings TemplateId="editWaybillBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column888" AllowEdit="false" AllowDelete="false" HeaderText="Update F-Form"
                                                    runat="server" Width="85" Wrap="true"  Visible="false">
                                                    <TemplateSettings TemplateId="editCFormBtnTemplate" />
                                                </cc1:Column>

                                                <cc1:Column ID="Column31" AllowEdit="false" AllowDelete="false" HeaderText="PRINT" runat="server"
                                                Width="82" Wrap="true">
                                                <TemplateSettings TemplateId="PrintBtnTemplate" />
                                                </cc1:Column>

                                                <cc1:Column ID="Column23" AllowEdit="true" AllowDelete="true" HeaderText="EDIT" runat="server"
                                                    Width="82">
                                                    <TemplateSettings TemplateId="editBtnTemplate" />
                                                </cc1:Column>

                                                <cc1:Column ID="Column6" AllowEdit="true" AllowDelete="true" HeaderText="VIEW" runat="server"
                                                    Width="60">
                                                    <TemplateSettings TemplateId="ViewBtnTemplate" />
                                                </cc1:Column>

                                                <cc1:Column HeaderText="CANCEL" AllowEdit="false" AllowDelete="true" Width="80">
                                                    <TemplateSettings TemplateId="deletestockTemplate" />
                                                </cc1:Column>
                                            </Columns>
                                            <FilteringSettings MatchingType="AnyFilter" />
                                            <Templates>

                                            <cc1:GridTemplate runat="server" ID="PrintBtnTemplate">
                                                <Template>
                                                    <a href="javascript: //" class="filter_btn printer_co" id="btnPrint_<%# Container.PageRecordIndex %>"
                                                        onclick="CallServerMethodPrint(this)"></a>
                                                </Template>
                                            </cc1:GridTemplate>

                                                <cc1:GridTemplate runat="server" ID="editBtnTemplate">
                                                    <Template>                                                        
                                                        <a href="javascript: //" class="action-icons c-edit" id="btnGridEdit_<%# Container.PageRecordIndex %>"
                                                            onclick="CallServerMethod(this)"></a>
                                                    </Template>
                                                </cc1:GridTemplate>

                                                <cc1:GridTemplate runat="server" ID="ViewBtnTemplate">
                                                    <Template>                                                        
                                                        <a href="javascript: //" class="h_icon zoom_sl" id="btnGridView_<%# Container.PageRecordIndex %>"
                                                            onclick="CallServerMethod_View(this)"></a>
                                                    </Template>                                                       
                                                </cc1:GridTemplate>

                                            </Templates>
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="editWaybillBtnTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="filter_btn clipboard_text_co" id="btnGridWaybill_<%# Container.PageRecordIndex %>"
                                                            onclick="CallServerMethodWaybill(this)"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="editCFormBtnTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="filter_btn clipboard_text_co" id="btnGridCForm_<%# Container.PageRecordIndex %>"
                                                            onclick="CallServerMethodCForm(this)"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="slnoTemplateIN">
                                                    <Template>
                                                        <asp:Label ID="lblslnoin" runat="server" Text='<%#Container.PageRecordIndex+1 %>'></asp:Label>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="deletestockTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="action-icons c-delete" title="Delete" onclick="gvStockTransferDetails.delete_record(this)">
                                                        </a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <ScrollingSettings ScrollWidth="100%" ScrollHeight="290" />
                                        </cc1:Grid>
                                        <asp:Button ID="btngrdedit" runat="server" Text="grdedit" Style="display: none" OnClick="btngrdedit_Click"
                                            CausesValidation="false" />
                                         <asp:Button ID="btnview" runat="server" Text="View" Style="display: none" OnClick="btnview_Click"
                                            CausesValidation="false" />
                                        <asp:Button ID="btngrdUpdateWaybill" runat="server" Text="Update Waybill" Style="display: none"
                                            OnClick="btngrdUpdateWaybill_Click" CausesValidation="false" />
                                        <asp:Button ID="btngrdUpdateForm" runat="server" Text="Update F-Form" Style="display: none"
                                            OnClick="btngrdUpdateForm_Click" CausesValidation="false" />

                                        <asp:Button ID="btnPrint" runat="server" Text="Print" Style="display: none" 
                                                OnClick="btnPrint_Click" CausesValidation="false"  />

                                        <asp:HiddenField ID="hdnDespatchID" runat ="server" />
                                        <asp:HiddenField ID="hdn_transferid" runat="server" />
                                        <asp:HiddenField ID="hdnWaybillNo" runat="server" />
                                        <asp:HiddenField ID="hdnWaybillKey" runat="server" />
                                        <asp:HiddenField ID="hdnmrp" runat="server" />
                                        <asp:HiddenField ID="hdn_pcs" runat="server" />
                                        <asp:HiddenField ID="hdnCFormNo" runat="server" />
                                        <asp:HiddenField ID="hdnCFormDate" runat="server" />
                                        <asp:HiddenField ID="hdn_ASSESMENTPERCENTAGE" runat="server" />
                                        <asp:HiddenField ID="hdn_mfgdate" runat="server" />
                                        <asp:HiddenField ID="hdn_exprdate" runat="server" />
                                        <asp:HiddenField ID="Hdn_Print" runat ="server"  />
                                        <asp:HiddenField ID="hdnCustomerID" runat ="server" />
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                        <div id="light" class="white_content" runat="server">
                            <table width="50%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                <tr>
                                    <td width="10%" class="field_title">
                                        <asp:Label ID="Label22" runat="server" Text="Waybill Key"></asp:Label>
                                    </td>
                                    <td width="20%" class="field_input">
                                        <asp:TextBox ID="txtWaybillkey" runat="server" Width="69%" placeholder="Waybill Key"
                                            AutoCompleteType="Disabled" onfocus="disableautocompletion(this.id);"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="10%" class="field_title">
                                        <asp:Label ID="Label20" runat="server" Text="Waybill No."></asp:Label>
                                    </td>
                                    <td width="20%" class="field_input">
                                        <asp:TextBox ID="txtWaybillUpdate" runat="server" Width="69%" placeholder="Waybill No"
                                            AutoCompleteType="Disabled" onfocus="disableautocompletion(this.id);"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Button ID="btnWaybillUpdate" runat="server" Text="Update" CssClass="btn_small btn_blue"
                                            OnClick="btnWaybillUpdate_Click" />&nbsp;&nbsp;<asp:Button ID="btnCloseLightbox"
                                                runat="server" Text="Close" CssClass="btn_small btn_blue" OnClick="btnCloseLightbox_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="light2" class="white_content" runat="server">
                            <table width="50%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                <tr>
                                    <td width="10%" class="field_title">
                                        <asp:Label ID="Label30" runat="server" Text="F-Form No."></asp:Label>
                                    </td>
                                    <td width="20%" class="field_input">
                                        <asp:TextBox ID="txtCFormNo" runat="server" Width="69%" placeholder="F-Form No."
                                            AutoCompleteType="Disabled" onfocus="disableautocompletion(this.id);"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="10%" class="field_title">
                                        <asp:Label ID="Label31" runat="server" Text="F-Form No."></asp:Label>
                                    </td>
                                    <td width="20%" class="field_input">
                                        <asp:TextBox ID="txtCFormPopupDate" runat="server" Width="69%" placeholder="F-Form Date."
                                            Enabled="false"></asp:TextBox>
                                        <asp:ImageButton ID="ImageButton233" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                            runat="server" Height="24" />
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImageButton233"
                                            runat="server" TargetControlID="txtCFormPopupDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Button ID="btnCFormUpdate" runat="server" Text="Update" CssClass="btn_small btn_blue"
                                            OnClick="btnCFormUpdate_Click" />&nbsp;&nbsp;<asp:Button ID="btnCloseLightbox2" runat="server"
                                                Text="Close" CssClass="btn_small btn_blue" OnClick="btnCloseLightbox2_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="fade" class="black_overlay" runat="server">
                        </div>
                        <div id="lightRejectionNote" class="white_content" runat="server">
                            <table width="50%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                <tr>
                                    <td width="10%" class="field_title">
                                        <asp:Label ID="Label23" runat="server" Text="Note"></asp:Label>&nbsp;<span class="req">*</span>
                                    </td>
                                    <td width="20%" class="field_input">
                                        <asp:TextBox ID="txtRejectionNote" runat="server" CssClass="x_large" TextMode="MultiLine"
                                            Style="height: 119px" MaxLength="255"> </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="CV_txtRejectionNote" runat="server" ControlToValidate="txtRejectionNote"
                                            ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="RejectionNote"
                                            ForeColor="Red"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="field_title">
                                        &nbsp;
                                    </td>
                                    <td class="field_input">
                                        <asp:Button ID="btnRejectionNoteSubmit" runat="server" Text="Submit" CssClass="btn_small btn_blue"
                                            ValidationGroup="RejectionNote" OnClick="btnRejectionNoteSubmit_Click" />&nbsp;&nbsp;<asp:Button
                                                ID="btnRejectionCloseLightbox" runat="server" Text="Close" CssClass="btn_small btn_blue"
                                                OnClick="btnRejectionCloseLightbox_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="fadeRejectionNote" class="black_overlay" runat="server">
                        </div>
                    </div>
                    <cc1:MessageBox ID="MessageBox1" runat="server" />
                </div>
            </div>
            <script type="text/javascript">
                function OnBeforeDelete(record) {
                    record.Error = '';
                    document.getElementById("<%=hdn_transferid.ClientID %>").value = record.STOCKTRANSFERID;
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
                function isNumberKeyWithDot(evt) {
                    var charCode = (evt.which) ? evt.which : event.keyCode;
                    if ((charCode < 48 || charCode > 57) && (charCode != 8) && (charCode != 46))
                        return false;

                    return true;
                }

                function isNumberKeyWithoutDot(evt) {
                    var charCode = (evt.which) ? evt.which : event.keyCode;
                    if ((charCode < 48 || charCode > 57) && (charCode != 8))
                        return false;

                    return true;
                }

                function CallServerMethod(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnGridEdit_", "");
                    document.getElementById("<%=hdn_transferid.ClientID %>").value = gvStockTransferDetails.Rows[iRecordIndex].Cells[0].Value;
                    document.getElementById("<%=hdnDepotID.ClientID %>").value = gvStockTransferDetails.Rows[iRecordIndex].Cells[12].Value;
                    document.getElementById("<%=btngrdedit.ClientID %>").click();
                }

                function CallServerMethod_View(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnGridView_", "");
                    document.getElementById("<%=hdn_transferid.ClientID %>").value = gvStockTransferDetails.Rows[iRecordIndex].Cells[0].Value;
                    document.getElementById("<%=hdnDepotID.ClientID %>").value = gvStockTransferDetails.Rows[iRecordIndex].Cells[12].Value;
                    document.getElementById("<%=btnview.ClientID %>").click();
                }

                function CallServerMethodWaybill(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnGridWaybill_", "");
                    document.getElementById("<%=hdn_transferid.ClientID %>").value = gvStockTransferDetails.Rows[iRecordIndex].Cells[0].Value;
                    document.getElementById("<%=hdnWaybillNo.ClientID %>").value = gvStockTransferDetails.Rows[iRecordIndex].Cells[5].Value;
                    document.getElementById("<%=hdnWaybillKey.ClientID %>").value = gvStockTransferDetails.Rows[iRecordIndex].Cells[9].Value;
                    document.getElementById("<%=btngrdUpdateWaybill.ClientID %>").click();

                }
                function CallServerMethodPrint(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnPrint_", "");
                    document.getElementById("<%=hdn_transferid.ClientID %>").value = gvStockTransferDetails.Rows[iRecordIndex].Cells[0].Value;

                    document.getElementById("<%=btnPrint.ClientID %>").click();
                }

                function CallServerMethodCForm(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnGridCForm_", "");
                    document.getElementById("<%=hdn_transferid.ClientID %>").value = gvStockTransferDetails.Rows[iRecordIndex].Cells[0].Value;
                    document.getElementById("<%=hdnCFormNo.ClientID %>").value = gvStockTransferDetails.Rows[iRecordIndex].Cells[7].Value;
                    document.getElementById("<%=hdnCFormDate.ClientID %>").value = gvStockTransferDetails.Rows[iRecordIndex].Cells[6].Value;
                    document.getElementById("<%=btngrdUpdateForm.ClientID %>").click();

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
                    gvStockTransferDetails.addFilterCriteria('STOCKTRANSFERDATE', OboutGridFilterCriteria.Contains, searchValue);
                    gvStockTransferDetails.addFilterCriteria('STOCKTRANSFERNO', OboutGridFilterCriteria.Contains, searchValue);
                    gvStockTransferDetails.addFilterCriteria('TODEPOTNAME', OboutGridFilterCriteria.Contains, searchValue);
                    gvStockTransferDetails.addFilterCriteria('WAYBILLNO', OboutGridFilterCriteria.Contains, searchValue);
                    gvStockTransferDetails.addFilterCriteria('FFORMNO', OboutGridFilterCriteria.Contains, searchValue);
                    gvStockTransferDetails.addFilterCriteria('FORMREQUIRED', OboutGridFilterCriteria.Contains, searchValue);
                    gvStockTransferDetails.executeFilter();
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

                function OnScrollDiv(Scrollablediv) {
                    document.getElementById('DivHeaderRow').scrollLeft = Scrollablediv.scrollLeft;
                    document.getElementById('DivFooterRow').scrollLeft = Scrollablediv.scrollLeft;
                }

         </script>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>