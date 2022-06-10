<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="frmInsuranceCompanyMaster.aspx.cs" Inherits="VIEW_frmInsuranceCompanyMaster" %>

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
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
        <%--<div class="page_title">
                <span class="title_icon"><span class="computer_imac"></span></span>
                <h3>Insurance Company</h3>
            </div>--%>
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6> Insurance Company Details </h6>
                                <div class="btn_30_light" style="float: right;" id="btnaddhide" runat="server" >
                                    <span class="icon add_co"></span>
                                    <asp:Button ID="btnAddTPUVendor" runat="server" Text="Add New Record" CssClass="btn_link"
                                        OnClick="btnAddTPUVendor_Click" CausesValidation="false" /></div>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        
                                        
                                        <tr>
                                            <td width="130" class="field_title"><asp:Label ID="lblCode" Text="Code" runat="server"></asp:Label> <span class="req">*</span></td>
                                            <td class="field_input">
                                            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td width="12%">
                                                        <asp:TextBox ID="txtCode" runat="server" CssClass="x-large" MaxLength="20"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="CV_txtCode" runat="server" Display="None" ErrorMessage=" Code is required!"
                                                            ControlToValidate="txtCode" ValidateEmptyText="false" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender19" runat="server"
                                                            TargetControlID="CV_txtCode" PopupPosition="Right" HighlightCssClass="errormassage"
                                                            WarningIconImageUrl="../images/050.png">
                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                                    </td>
                                                    <td width="25">&nbsp;</td>
                                                    <td width="8%" class="innerfield_title">
                                                        <asp:Label ID="lblName" Text="Name" runat="server"></asp:Label> <span class="req">*</span>
                                                    </td>
                                                    <td width="28%">
                                                 <asp:TextBox ID="txtName" runat="server" CssClass="x-large"> </asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="CV_Name" runat="server" Display="None" ErrorMessage="Insurance Name is required!"
                                                            ControlToValidate="txtName" ValidateEmptyText="false" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                            TargetControlID="CV_Name" PopupPosition="Right" HighlightCssClass="errormassage"
                                                            WarningIconImageUrl="../images/050.png">
                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                            <td>&nbsp;</td>
                                                </tr>
                                            </table>
                                            </td>
                                        </tr>                               
                                        <tr>
                                            <td align="left" valign="middle" class="field_title">
                                                <asp:Label ID="lblAdress" Text="Address Info 1" runat="server"></asp:Label> <span class="req">*</span>
                                            </td>
                                            <td align="left" class="field_input">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                       <td width="21%">
                                                            <asp:DropDownList ID="ddlState" runat="server" Width="160" AutoPostBack="true" AppendDataBoundItems="True" class="chosen-select" data-placeholder="Select State"
                                                                    OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <%--<span class="label_intro">State</span>--%>
                                                                <asp:RequiredFieldValidator ID="CV_ddlState" runat="server"  ErrorMessage="Required!" ForeColor="Red"
                                                                    ControlToValidate="ddlState" ValidateEmptyText="false" SetFocusOnError="true"
                                                                    InitialValue="0"> </asp:RequiredFieldValidator>
                                                                    
                                                        </td>
                                                       <td width="21%">
                                                            <asp:DropDownList ID="ddlDistrict" runat="server" Width="160" AutoPostBack="true" AppendDataBoundItems="True" class="chosen-select" data-placeholder="Select District"
                                                                    OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <%--<span class=" label_intro">District</span>--%>
                                                                <asp:RequiredFieldValidator ID="CV_ddlDistrict" runat="server"  ErrorMessage="Required!" ForeColor="Red"
                                                                    ControlToValidate="ddlDistrict" ValidateEmptyText="false" SetFocusOnError="true"
                                                                    InitialValue="0"> </asp:RequiredFieldValidator>
                                                                
                                                        </td>                                                        
                                                       <td width="21%">
                                                            <asp:DropDownList ID="ddlCity" runat="server" Width="160" AppendDataBoundItems="true" class="chosen-select" data-placeholder="Select City">
                                                                </asp:DropDownList>
                                                                <%--<span class="label_intro">City</span>--%>
                                                                <asp:RequiredFieldValidator ID="CV_ddlCity" runat="server"  ErrorMessage="Required!" ForeColor="Red"
                                                                    ControlToValidate="ddlCity" ValidateEmptyText="false" SetFocusOnError="true"
                                                                    InitialValue="0"> </asp:RequiredFieldValidator> 
                                                                
                                                        </td>
                                                       <td valign="top">
                                                            <asp:Button ID="btnAddCity" runat="server" CssClass="h_icon add_co" ToolTip="Add City" onclick="btnAddCity_Click" CausesValidation="false" Enabled="false" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:Button ID="btnRefresh" runat="server" CssClass="h_icon arrow_refresh_co" ToolTip="Refresh"  onclick="btnRefresh_Click" CausesValidation="false" Enabled="false" />
                                                        </td>
                                                    </tr>
                                                    </table>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                     <tr><td colspan="2" height="2"></td></tr>
                                                    <tr>
                                                        <td width="40%">
                                                            <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" CssClass="large" MaxLength="255"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="CV_txtAddress" runat="server" Display="None" ErrorMessage="Address is required!"
                                                            ControlToValidate="txtAddress" ValidateEmptyText="false" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server"
                                                            TargetControlID="CV_txtAddress" PopupPosition="BottomLeft" HighlightCssClass="errormassage"
                                                            WarningIconImageUrl="../images/050.png">
                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                                        <span class="label_intro">Contact Address</span>
                                                        </td>                                                         
                                                        <td valign="top" style="padding:0 0 0 15px;">
                                                            <asp:TextBox ID="txtPIN" runat="server" MaxLength="6" CssClass="mid" onkeypress="return isNumberKey(event);"> </asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="CV_txtPIN" runat="server" Display="None" ErrorMessage="PIN/ZIP is required!"
                                                            ControlToValidate="txtPIN" ValidateEmptyText="false" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server"
                                                            TargetControlID="CV_txtPIN" PopupPosition="Right" HighlightCssClass="errormassage"
                                                            WarningIconImageUrl="../images/050.png">
                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPIN"
                                                            ErrorMessage="Minimum 6 digits PIN/ZIP required." ValidationExpression="[0-9]{6}"
                                                            Display="None" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server"
                                                            TargetControlID="RegularExpressionValidator1" PopupPosition="Right" HighlightCssClass="errormassage"
                                                            WarningIconImageUrl="../images/050.png">
                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                                        <span class="label_intro">Pin code</span>
                                                        </td>
                                                    </tr>                           
                                                </table>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td align="left" valign="middle" class="field_title">
                                                <asp:Label ID="Label3" Text="Address Info 2" runat="server"></asp:Label> <span class="req"></span>
                                            </td>
                                            <td align="left" class="field_input">
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                     <tr><td colspan="2" height="2"></td></tr>
                                                    <tr>
                                                        <td width="40%">
                                                            <asp:TextBox ID="txtAddress1" runat="server" TextMode="MultiLine" CssClass="large" MaxLength="255"></asp:TextBox>
                                                        <%--<asp:RequiredFieldValidator ID="CV_txtAddress1" runat="server" Display="None" ErrorMessage="Address is required!"
                                                            ControlToValidate="txtAddress1" ValidateEmptyText="false" SetFocusOnError="true"> </asp:RequiredFieldValidator>--%>
                                                        <%--<ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
                                                            TargetControlID="CV_txtAddress1" PopupPosition="BottomLeft" HighlightCssClass="errormassage"
                                                            WarningIconImageUrl="../images/050.png">
                                                        </ajaxToolkit:ValidatorCalloutExtender>--%>
                                                        <span class="label_intro">Contact Address</span>
                                                        </td>                                                         
                                                        <td valign="top" style="padding:0 0 0 15px;">
                                                            <asp:TextBox ID="txtPIN1" runat="server" MaxLength="6" CssClass="mid" onkeypress="return isNumberKey(event);"> </asp:TextBox>
                                                        <%--<asp:RequiredFieldValidator ID="CV_txtPIN1" runat="server" Display="None" ErrorMessage="PIN/ZIP is required!"
                                                            ControlToValidate="txtPIN1" ValidateEmptyText="false" SetFocusOnError="true"> </asp:RequiredFieldValidator>--%>
                                                        <%--<ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server"
                                                            TargetControlID="CV_txtPIN1" PopupPosition="Right" HighlightCssClass="errormassage"
                                                            WarningIconImageUrl="../images/050.png">
                                                        </ajaxToolkit:ValidatorCalloutExtender>--%>
                                                        <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtPIN1"
                                                            ErrorMessage="Minimum 6 digits PIN/ZIP required." ValidationExpression="[0-9]{6}"
                                                            Display="None" SetFocusOnError="true"></asp:RegularExpressionValidator>--%>
                                                        <%--<ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server"
                                                            TargetControlID="RegularExpressionValidator6" PopupPosition="Right" HighlightCssClass="errormassage"
                                                            WarningIconImageUrl="../images/050.png">
                                                        </ajaxToolkit:ValidatorCalloutExtender>--%>
                                                        <span class="label_intro">Pin code</span>
                                                        </td>
                                                    </tr>                           
                                                </table>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td align="left" class="field_title">Contact Person <span class="req">*</span></td>
                                            <td align="left" class="field_input">
                                                <asp:TextBox ID="txtcontactperson" runat="server" CssClass="mid"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="CV_txtcontactperson" runat="server" Display="None"
                                                                ErrorMessage="Contact Person Name is required!" ControlToValidate="txtcontactperson"
                                                                ValidateEmptyText="false" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender17" runat="server"
                                                                TargetControlID="CV_txtcontactperson" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                        </tr>                                       
                                        <tr>  
                                            <td align="left" class="field_title">Contact Info <span class="req">*</span></td>                                     
                                            <td align="left" class="field_input">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td width="18%">
                                                    <asp:TextBox ID="txtMobileNo" runat="server" MaxLength="10" onkeypress="return isNumberKey(event);"></asp:TextBox>                                                            
                                                            <%--<asp:RequiredFieldValidator ID="CV_txtMobileNo" runat="server" Display="None" ErrorMessage="MobileNo is required!"
                                                                ControlToValidate="txtMobileNo" ValidateEmptyText="false" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server"
                                                                TargetControlID="CV_txtMobileNo" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtMobileNo"
                                                                ErrorMessage="Minimum 10 digits MobileNo required." ValidationExpression="[0-9]{10}"
                                                                Display="None" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server"
                                                                TargetControlID="RegularExpressionValidator2" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>--%>
                                                            <span class=" label_intro">Mobile No</span>
                                                </td>
                                                <td width="12">&nbsp;</td>
                                                <td width="18%">
                                                     <asp:TextBox ID="txtPhoneNo" runat="server" MaxLength="11" onkeypress="return isNumberKey(event);"></asp:TextBox>                                                            
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtPhoneNo"
                                                                ErrorMessage="Minimum 8 digits PhoneNo required." ValidationExpression="^[\s\S]{8,}$"
                                                                Display="None" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server"
                                                                TargetControlID="RegularExpressionValidator3" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                            <span class=" label_intro">LandLine No</span> 
                                                </td>
                                                <td width="12">&nbsp;</td>
                                                 <td width="18%">
                                                    <asp:TextBox ID="txtMobileNo1" runat="server" MaxLength="10" onkeypress="return isNumberKey(event);"></asp:TextBox>                                                           
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtMobileNo1"
                                                                ErrorMessage="Minimum 10 digits MobileNo required." ValidationExpression="[0-9]{10}"
                                                                Display="None" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender25" runat="server"
                                                                TargetControlID="RegularExpressionValidator4" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                            <span class=" label_intro">Mobile No 2</span>
                                                </td>
                                                <td width="12">&nbsp;</td>
                                                <td width="18%">
                                                     <asp:TextBox ID="txtPhoneNo1" runat="server" MaxLength="11" onkeypress="return isNumberKey(event);"></asp:TextBox>                                                            
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtPhoneNo1"
                                                                ErrorMessage="Minimum 8 digits PhoneNo required." ValidationExpression="^[\s\S]{8,}$"
                                                                Display="None" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender26" runat="server"
                                                                TargetControlID="RegularExpressionValidator5" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                            <span class=" label_intro">LandLine No 2</span> 
                                                </td>
                                                <td>&nbsp;</td>
                                                </tr>                                                
                                            </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" class="field_title"><asp:Label ID="lblemailid" Text="EMAIL ID" runat="server"></asp:Label> <span class="req">*</span></td>
                                            <td align="left" class="field_input">
                                                        <asp:TextBox ID="txtEmailid" runat="server" CssClass="mid"> </asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="CV_txtEmailid" runat="server" Display="None" ErrorMessage="Email Id  is required!"
                                                            ControlToValidate="txtEmailid" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender16" runat="server"
                                                            TargetControlID="CV_txtEmailid" PopupPosition="Right" HighlightCssClass="errormassage"
                                                            WarningIconImageUrl="../images/050.png">
                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                                        <asp:RegularExpressionValidator ID="CV_email_valid" runat="server" Display="None"
                                                            ErrorMessage="Enter Valid Email ID!" ControlToValidate="txtEmailid" SetFocusOnError="true"
                                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"> </asp:RegularExpressionValidator>
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                                            TargetControlID="CV_email_valid" PopupPosition="Right" HighlightCssClass="errormassage"
                                                            WarningIconImageUrl="../images/050.png">
                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>      
                                            <td align="left" class="field_title">Company Info <span class="req">*</span></td>                                      
                                            <td align="left" class="field_input">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                               <td width="22%">
                                                     <asp:TextBox ID="txtcstno" runat="server" CssClass="x-large"> </asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="CV_txtCSTNO" runat="server" Display="None" ErrorMessage="CST NO is required!"
                                                            ControlToValidate="txtcstno" ValidateEmptyText="false" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                                            TargetControlID="CV_txtCSTNO" PopupPosition="Right" HighlightCssClass="errormassage"
                                                            WarningIconImageUrl="../images/050.png">
                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                                    <span class="label_intro"><asp:Label ID="lblcstno" Text="CST NO" runat="server"></asp:Label></span>
                                                </td>
                                                <td width="15">&nbsp;</td>
                                               
                                                
                                                <td width="22%">
                                                    <asp:TextBox ID="txttinno" runat="server" CssClass="x-large"> </asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="CV_txtTINNO" runat="server" Display="None" ErrorMessage="VAT NO is required!"
                                                            ControlToValidate="txttinno" ValidateEmptyText="false" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender20" runat="server"
                                                            TargetControlID="CV_txtTINNO" PopupPosition="Right" HighlightCssClass="errormassage"
                                                            WarningIconImageUrl="../images/050.png">
                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                                    <span class="label_intro"><asp:Label ID="lbltinno" Text="TIN NO" runat="server"></asp:Label></span>
                                                </td>
                                                <td width="15">&nbsp;</td>
                                                <td>
                                                     <asp:TextBox ID="txtpanno" runat="server" CssClass="x-large" MaxLength="10" Width="50%"> </asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="CV_txtpanno" runat="server" Display="None" ErrorMessage="PAN No is required!"
                                                            ControlToValidate="txtpanno" ValidateEmptyText="false" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender23" runat="server"
                                                            TargetControlID="CV_txtpanno" PopupPosition="Right" HighlightCssClass="errormassage"
                                                            WarningIconImageUrl="../images/050.png">
                                                        </ajaxToolkit:ValidatorCalloutExtender>

                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtpanno"  
                                                        Display="None" ForeColor="Red" ErrorMessage="Invalid PAN No" SetFocusOnError="true" ValidationExpression="[A-Z]{5}\d{4}[A-Z]{1}"></asp:RegularExpressionValidator>
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
                                                            TargetControlID="RegularExpressionValidator2" PopupPosition="Right" HighlightCssClass="errormassage"
                                                            WarningIconImageUrl="../images/050.png">
                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                                    <span class="label_intro"><asp:Label ID="lblpanno" Text="PAN NO &nbsp;(ex:AAACJ4124B)" runat="server"></asp:Label></span>
                                                </td>                                                
                                                </tr>
                                             
                                            </table>
                                            </td>
                                        </tr>
                                        <tr> 
                                            <td class="field_title">Bank Info<span class="req">*</span></td>                                              
                                            <td align="left" class="field_input">
                                                 <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                    <tr>                                                        
                                                        <td width="22%">
                                                            <asp:TextBox ID="txtbankacno" runat="server" CssClass="x-large"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="CV_txtBANKACCOUNTNO" runat="server" Display="None"
                                                            ErrorMessage=" Bank A/C No is required!" ControlToValidate="txtbankacno" ValidateEmptyText="false"
                                                            SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server"
                                                            TargetControlID="CV_txtBANKACCOUNTNO" PopupPosition="Right" HighlightCssClass="errormassage"
                                                            WarningIconImageUrl="../images/050.png">
                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                                            <span class="label_intro"><asp:Label ID="lblbankacno" Text="BANK ACCOUNT NO" runat="server"></asp:Label></span>
                                                        </td>
                                                        <td width="15">&nbsp;</td>
                                                         <td width="22%">
                                                            <asp:TextBox ID="txtIFSC" runat="server" CssClass="x-large"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="None"
                                                            ErrorMessage=" IFSC is required!" ControlToValidate="txtIFSC" ValidateEmptyText="false"
                                                            SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender22" runat="server"
                                                            TargetControlID="RequiredFieldValidator2" PopupPosition="Right" HighlightCssClass="errormassage"
                                                            WarningIconImageUrl="../images/050.png">
                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                                            <span class="label_intro"><asp:Label ID="lblIfsc" Text="IFSC Code" runat="server"></asp:Label></span>
                                                        </td> 
                                                        <td>&nbsp;</td>
                                                         </tr>
                                                         <tr><td colspan="4" height="10"></td></tr>
                                                         <tr>
                                                       <td>
                                                            <asp:DropDownList ID="ddlbankname" runat="server" Width="180" AppendDataBoundItems="True" data-placeholder="Select Bank Name" class="chosen-select"/>
                                                            <asp:RequiredFieldValidator ID="CV_ddlbankname" runat="server"  ErrorMessage="Required!" ForeColor="Red"
                                                                ControlToValidate="ddlbankname" ValidateEmptyText="false" SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>                                                            
                                                        </td>
                                                        <td width="15">&nbsp;</td>
                                                        <td>                                                       
                                                              <asp:DropDownList ID="ddlbranchname" runat="server" Width="180"  class="chosen-select" data-placeholder="Select Branch Name" AppendDataBoundItems="True" />
                                                            <asp:RequiredFieldValidator ID="CV_ddlbranchname" runat="server"  ErrorMessage="Required!" ForeColor="Red"
                                                                ControlToValidate="ddlbranchname" ValidateEmptyText="false" SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
                                                                      
                                                        </td>
                                                        <td align="left" valign="top">
                                                        <asp:Button ID="btnAddBranch" runat="server" ToolTip="Add Branch" CssClass="h_icon add_co" Enabled="true"  CausesValidation="false" onclick="btnAddBranch_Click"  />&nbsp;&nbsp;&nbsp;&nbsp;
                                                         <asp:Button ID="btnRefresh1" runat="server" CssClass="h_icon arrow_refresh_co" ToolTip="Refresh"  onclick="btnRefresh1_Click" CausesValidation="false" Enabled="true" />
                                                         </td>                                                        
                                                    </tr>
                                                 </table>   
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" class="field_title">&nbsp;</td>
                                            <td align="left" valign="top" class="field_input"> 
                                              <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="13%"><asp:Label ID="Label2" CssClass="innerfield_title" Text="TDS DECLARATION" runat="server"></asp:Label></td>
                                                        <td width="4%"><asp:CheckBox ID="chkTDS" runat="server" Text=" " /></td>
                                                        <td width="10%"><asp:Label CssClass="innerfield_title" ID="lblactive" Text="ISAPPROVED" runat="server"></asp:Label></td>                                                    
                                                        <td><asp:CheckBox ID="chkActive" runat="server" Text=" " /></td>
                                                        <td   width="12%"><asp:Label ID="Label5" CssClass="innerfield_title" Text="Account Group" runat="server"></asp:Label><span class="req">*</span></td>
                                                        <td class="field_input"  > <asp:DropDownList ID="ddlaccountgroup" runat="server" AppendDataBoundItems="true" 
                                                                Style="width: 220px;" class="chosen-select" data-placeholder="Choose a AppliedTo"> </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfv_ddlaccountgroup" runat="server"  ErrorMessage="Required!" Font-Bold="true" ForeColor="Red"
                                                                    ControlToValidate="ddlaccountgroup" ValidateEmptyText="false" SetFocusOnError="true" InitialValue="0"  > </asp:RequiredFieldValidator>
                                                       </td>
                                                    </tr>                                    
                                                </table>
                                            </td>
                                        </tr> 
                                        <tr>
                                            <td></td>
                                            <td style="padding:8px 0px;">
                                                <div class="btn_24_blue">
                                                    <span class="icon disk_co"></span>
                                                    <asp:Button ID="btnInsuranceSubmit" runat="server" Text="Save" CssClass="btn_link" OnClick="btnInsuranceSubmit_Click" />
                                                </div>
                                                   &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
                                                    <span class="icon cross_octagon_co"></span>
                                                   <asp:Button ID="btnInsuranceCancel" runat="server" Text="Cancel" CssClass="btn_link" OnClick="btnInsuranceCancel_Click" CausesValidation="false" />
                                                 </div>
                                                   <asp:HiddenField ID="Hdn_Fld" runat="server" />
                                            </td>
                                        </tr>                                    
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlDisplay" runat="server">
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
                                    <cc1:Grid ID="gvInsurancecompany" runat="server" CallbackMode="false" Serialize="true" FolderStyle="../GridStyles/premiere_blue"
                                        AutoGenerateColumns="false" AllowPageSizeSelection="false" OnDeleteCommand="DeleteRecord"
                                        AllowAddingRecords="false" AllowFiltering="true" OnRowDataBound="gvInsurancecompany_RowDataBound">
                                        <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnClientDelete="OnDelete" />
                                        <FilteringSettings InitialState="Visible" />
                                        <Columns>
                                            <cc1:Column DataField="ID" ReadOnly="true" HeaderText="ID" runat="server"
                                                Visible="false" />
                                            <cc1:Column DataField="CODE" HeaderText=" CODE" runat="server" Width="90">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                </FilterOptions>
                                            </cc1:Column>
                                            <cc1:Column DataField="COMPANY_NAME" HeaderText="COMPANY NAME" runat="server" Width="140">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                </FilterOptions>
                                            </cc1:Column>
                                         
                                            <cc1:Column  DataField="ADDRESS" HeaderText="ADDRESS" runat="server"
                                                Width="140">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                </FilterOptions>
                                            </cc1:Column>
                                            <cc1:Column DataField="MOBILENO" HeaderText="MOBILENO" runat="server" Width="100">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                </FilterOptions>
                                            </cc1:Column>
                                            <cc1:Column DataField="EMAILID" HeaderText="EMAIL ID" runat="server" Width="160">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                </FilterOptions>
                                            </cc1:Column>
                                            <cc1:Column DataField="PINZIP" HeaderText="PIN/ZIP" runat="server" Width="80">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                </FilterOptions>
                                            </cc1:Column>
                                            <cc1:Column DataField="ISAPPROVED" HeaderText="ACTIVE" runat="server" Width="80">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="Contains" />
                                                </FilterOptions>
                                            </cc1:Column>
                                            
                                            <cc1:Column AllowEdit="true" AllowDelete="true" HeaderText="Edit" runat="server"
                                                Width="80">
                                                <TemplateSettings TemplateId="editBtnTemplate" />
                                            </cc1:Column>
                                            <cc1:Column HeaderText="Delete" AllowEdit="false" AllowDelete="true" Width="80">
                                                <TemplateSettings TemplateId="deleteBtnTemplate" />
                                            </cc1:Column>
                                             
                                        </Columns>
                                        <FilteringSettings MatchingType="AnyFilter" />
                                        <Templates>
                                            <cc1:GridTemplate runat="server" ID="editBtnTemplate">
                                                <Template>
                                                        <a href="javascript: //" class="action-icons c-edit" id="btnGridEdit_<%# Container.PageRecordIndex %>"  onclick="CallServerMethod(this)">
                                                         </a>
                                                </Template>
                                            </cc1:GridTemplate>
                                        </Templates>
                                        <Templates>
                                            <cc1:GridTemplate runat="server" ID="deleteBtnTemplate">
                                                <Template>
                                                    <a href="javascript: //" class="action-icons c-delete" title="Delete" onclick="gvInsurancecompany.delete_record(this)">
                                                    </a>
                                                </Template>
                                            </cc1:GridTemplate>
                                        </Templates>
                                        
                                        <ScrollingSettings ScrollWidth="100%" />
                                    </cc1:Grid>
                                    <asp:Button ID="btngridedit" runat="server" Text="Gridedit" CausesValidation="false" Style="display: none"
                                         onclick="btngridedit_Click" />
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
                </script>
                <script type="text/javascript">
                    function isNumberKey(evt) {
                        var charCode = (evt.which) ? evt.which : event.keyCode;
                        if (charCode < 48 || charCode > 57)
                            return false;
                        return true;
                    }    
                </script>
                <script type="text/javascript">
                    function CallServerMethod(oLink) {
                        var iRecordIndex = oLink.id.toString().replace("btnGridEdit_", "");
                        document.getElementById("<%=Hdn_Fld.ClientID %>").value = gvInsurancecompany.Rows[iRecordIndex].Cells[0].Value;
                        document.getElementById("<%=btngridedit.ClientID %>").click();
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
                        gvInsurancecompany.addFilterCriteria('CODE', OboutGridFilterCriteria.Contains, searchValue);
                        gvInsurancecompany.addFilterCriteria('COMPANY_NAME', OboutGridFilterCriteria.Contains, searchValue);
                        gvInsurancecompany.addFilterCriteria('ADDRESS', OboutGridFilterCriteria.Contains, searchValue);
                       
                        gvInsurancecompany.addFilterCriteria('PHONENO', OboutGridFilterCriteria.Contains, searchValue);
                        gvInsurancecompany.addFilterCriteria('MOBILENO', OboutGridFilterCriteria.Contains, searchValue);
                        gvInsurancecompany.addFilterCriteria('EMAILID', OboutGridFilterCriteria.Contains, searchValue);
                        gvInsurancecompany.addFilterCriteria('PINZIP', OboutGridFilterCriteria.Contains, searchValue);
                        gvInsurancecompany.addFilterCriteria('ISAPPROVED', OboutGridFilterCriteria.Contains, searchValue);
                        gvInsurancecompany.executeFilter();
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
