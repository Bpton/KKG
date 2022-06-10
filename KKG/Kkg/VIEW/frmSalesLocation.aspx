<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmSalesLocation.aspx.cs" Inherits="VIEW_frmSalesLocation" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"
    TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/bootstrap-multiselect.css" rel="stylesheet" />
    <script type="text/javascript" src="../js/bootstrap.js"></script>
    <script type="text/javascript" src="../js/bootstrap-multiselect.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function ValidateListBox(sender, args) {
            var options = document.getElementById("<%=ddldistrictcustmap.ClientID%>").options;
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
            $('#ContentPlaceHolder1_ddldistrictcustmap').multiselect({
                includeSelectAllOption: true
            });
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $('#ContentPlaceHolder1_ddlstatetransporter').multiselect({
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
                <h3>Company Location Master</h3>
            </div>--%>
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>
                                    Company Location Details</h6>
                                <div class="btn_30_light" style="float: right;" id="btnaddhide" runat="server">
                                    <span class="icon add_co"></span>
                                    <asp:Button ID="btnAddBranch" runat="server" Text="Add New Record" CssClass="btn_link"
                                        OnClick="btnAddBranch_Click" CausesValidation="false" /></div>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td width="130" class="field_title">
                                                <asp:Label ID="Label1" Text="Sales Location Type" runat="server"></asp:Label>
                                            </td>
                                            <td class="field_input">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="10%">
                                                            <asp:RadioButton ID="rdbOffice" runat="server" Text="OFFICE" GroupName="BR" class="innerfield_title"
                                                                OnCheckedChanged="rdbOffice_CheckedChanged" AutoPostBack="true" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbDepo" runat="server" Text="DEPOT" class="innerfield_title"
                                                                GroupName="BR" OnCheckedChanged="rdbDepo_CheckedChanged" AutoPostBack="true" />
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox ID="chkMotherDepo" runat="server" Checked="true" Visible="false" AutoPostBack="true"
                                                                class="innerfield_title" Text="MOTHER DEPOT" OnCheckedChanged="chkMotherDepo_CheckedChanged" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr id="lihide" runat="server">
                                            <td class="field_title">
                                                Branch<span class="req">*</span>
                                            </td>
                                            <td class="field_input">
                                                <asp:DropDownList ID="ddlParentBranch" class="chosen-select" runat="server" AppendDataBoundItems="True"
                                                    Width="210px" Height="28px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title">
                                                <asp:Label ID="lblBRName" Text=" Short Name" runat="server"></asp:Label><span class="req">*</span>
                                            </td>
                                            <td class="field_input">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="16%">
                                                            <asp:TextBox ID="txtBranchName" runat="server" CssClass="full"> </asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="CV_BRName" runat="server" Display="None" ErrorMessage="Branch Name is required!"
                                                                ControlToValidate="txtBranchName" ValidateEmptyText="false" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                                TargetControlID="CV_BRName" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                        </td>
                                                        <td width="15">
                                                        </td>
                                                        <td class="innerfield_title" width="4%">
                                                            <asp:Label ID="lblBRCode" Text="Code" runat="server"></asp:Label>
                                                        </td>
                                                        <td width="9%">
                                                            <asp:TextBox ID="txtBRCode" runat="server" CssClass="full" MaxLength="20"></asp:TextBox>
                                                        </td>
                                                        <td width="15">
                                                        </td>
                                                        <td class="innerfield_title" width="8%">
                                                            <asp:Label ID="lblname" Text="Full Name" runat="server"></asp:Label>
                                                        </td>
                                                        <td width="35%">
                                                            <asp:TextBox ID="txtfullname" runat="server" CssClass="large"> </asp:TextBox>
                                                            <%--  <asp:RequiredFieldValidator ID="rfv_txtname" runat="server" Display="None" ErrorMessage=" Name is required!"
                                                        ControlToValidate="txtfullname" ValidateEmptyText="false" SetFocusOnError="true" > </asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server"
                                                        TargetControlID="rfv_txtname" PopupPosition="Right" HighlightCssClass="errormassage"
                                                        WarningIconImageUrl="../images/050.png">
                                                    </ajaxToolkit:ValidatorCalloutExtender>--%>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title">
                                                <asp:Label ID="lblDescription" Text="Description" runat="server"></asp:Label><span
                                                    class="req">*</span>
                                            </td>
                                            <td class="field_input">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="26%">
                                                            <asp:TextBox ID="txtDescription" runat="server" Width="100%"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="CV_Description" runat="server" Display="None" ErrorMessage="Description is required!"
                                                                ControlToValidate="txtDescription" ValidateEmptyText="false" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                                                TargetControlID="CV_Description" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                        </td>
                                                        <td width="16">
                                                            &nbsp
                                                        </td>
                                                        <td width="7%">
                                                            <asp:Label ID="lblBRPrefix" class="innerfield_title" Text="Prefix" runat="server"></asp:Label><span
                                                                class="req">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtBRPrefix" runat="server" CssClass="mid" onfocus="if(this.value==''){this.value=''}"
                                                                MaxLength="10"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="CV_BrPrefix" runat="server" Display="None" ErrorMessage="Prefix is required!"
                                                                ControlToValidate="txtBRPrefix" ValidateEmptyText="false" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                                                TargetControlID="CV_BrPrefix" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title">
                                                Location <span class="req">*</span>
                                            </td>
                                            <td class="field_input">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="21%">
                                                            <div style="width: 160px;">
                                                                <asp:DropDownList ID="ddlState" runat="server" data-placeholder="Select State" class="chosen-select"
                                                                    AutoPostBack="true" AppendDataBoundItems="True" Width="100%" Height="28px" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="CV_ddlState" runat="server" Display="None" ErrorMessage="State is required!"
                                                                    ControlToValidate="ddlState" ValidateEmptyText="false" SetFocusOnError="true"
                                                                    InitialValue="0"> </asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
                                                                    TargetControlID="CV_ddlState" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="../images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                            </div>
                                                            <span class="label_intro">State</span>
                                                        </td>
                                                        <td width="21%">
                                                            <div style="width: 160px;">
                                                                <asp:DropDownList ID="ddlDistrict" runat="server" data-placeholder="Select District"
                                                                    class="chosen-select" AutoPostBack="true" AppendDataBoundItems="True" Width="100%"
                                                                    Height="28px" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="CV_ddlDistrict" runat="server" Display="None" ErrorMessage="District is required!"
                                                                    ControlToValidate="ddlDistrict" ValidateEmptyText="false" SetFocusOnError="true"
                                                                    InitialValue="0"> </asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
                                                                    TargetControlID="CV_ddlDistrict" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="../images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                            </div>
                                                            <span class=" label_intro">District</span>
                                                        </td>
                                                        <td width="21%">
                                                            <div style="width: 160px;">
                                                                <asp:DropDownList ID="ddlCity" runat="server" AppendDataBoundItems="true" class="chosen-select"
                                                                    data-placeholder="Select City" Width="100%" Height="28px">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="CV_ddlCity" runat="server" Display="None" ErrorMessage="City is required!"
                                                                    ControlToValidate="ddlCity" ValidateEmptyText="false" SetFocusOnError="true"
                                                                    InitialValue="0"> </asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server"
                                                                    TargetControlID="CV_ddlCity" PopupPosition="BottomLeft" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="../images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                            </div>
                                                            <span class=" label_intro">City</span>
                                                        </td>
                                                        <td valign="top">
                                                            <asp:Button ID="btnAddCity" CssClass="h_icon add_co" runat="server" ToolTip="Add City"
                                                                OnClick="btnAddCity_Click" CausesValidation="false" Enabled="false" />
                                                            <asp:Button ID="btnRefresh" runat="server" CssClass="h_icon arrow_refresh_co" ToolTip="Refresh"
                                                                OnClick="btnRefresh_Click" CausesValidation="false" Enabled="false" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title">
                                                <asp:Label ID="lblAdress" Text="Address Details" runat="server"></asp:Label>
                                                <span class="req">*</span>
                                            </td>
                                            <td class="field_input">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="30%">
                                                            <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" CssClass="mid" MaxLength="255"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="CV_txtAddress" runat="server" Display="None" ErrorMessage="Address is required!"
                                                                ControlToValidate="txtAddress" ValidateEmptyText="false" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server"
                                                                TargetControlID="CV_txtAddress" PopupPosition="BottomLeft" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                            <span class=" label_intro">Address</span>
                                                        </td>
                                                        <td width="15">
                                                            &nbsp;
                                                        </td>
                                                        <td width="15%" valign="top">
                                                            <asp:TextBox ID="txtPIN" runat="server" MaxLength="6" CssClass="x-large" onkeypress="return isNumberKey(event);"> </asp:TextBox>
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
                                                            <span class=" label_intro">Pin/ Zip Code</span>
                                                        </td>
                                                        <td width="10">
                                                            &nbsp;
                                                        </td>
                                                        <td width="2%" valign="top">
                                                            <asp:CheckBox ID="ChkCopyAddressPIN" runat="server" OnCheckedChanged="ChkCopyAddressPIN_CheckedChanged"
                                                                AutoPostBack="true" Text=" " />
                                                        </td>
                                                        <td align="left" valign="top">
                                                            <span class=" label_intro">Select Delivery Address is same </span>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title">
                                                <asp:Label ID="Label2" Text="Delivery To" runat="server"></asp:Label>
                                            </td>
                                            <td class="field_input">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="30%">
                                                            <asp:TextBox ID="txtDeliveryaddress" runat="server" TextMode="MultiLine" CssClass="mid"
                                                                MaxLength="255"></asp:TextBox>
                                                            <span class=" label_intro">Delivery Address</span>
                                                        </td>
                                                        <td width="15">
                                                            &nbsp;
                                                        </td>
                                                        <td width="30%" valign="top">
                                                            <asp:TextBox ID="txtDeliveryPIN" runat="server" MaxLength="6" CssClass="large"> </asp:TextBox>
                                                            <span class=" label_intro">Pin/ Zip Code</span>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title">
                                                <asp:Label ID="Label4" runat="server" Text="Email"></asp:Label>
                                                <span class="req">*</span>
                                            </td>
                                            <td class="field_input">
                                                <asp:TextBox ID="txtEmail" runat="server" CssClass="mid"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="Cv_Email" runat="server" ControlToValidate="txtEmail"
                                                    SetFocusOnError="true" ErrorMessage="Email ID is required!" Display="None"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" runat="server"
                                                    TargetControlID="Cv_Email" PopupPosition="Right" HighlightCssClass="errormassage"
                                                    WarningIconImageUrl="../images/050.png">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                <asp:RegularExpressionValidator ID="CV_Email_valid" runat="server" ErrorMessage="Enter Valid Email ID!"
                                                    ControlToValidate="txtEmail" SetFocusOnError="true" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                    Display="None">
                                                </asp:RegularExpressionValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server"
                                                    TargetControlID="CV_Email_valid" PopupPosition="Right" HighlightCssClass="errormassage"
                                                    WarningIconImageUrl="../images/050.png">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title">
                                                Contact Info<span class="req">*</span>
                                            </td>
                                            <td class="field_input">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="17%">
                                                            <asp:TextBox ID="txtMobileNo" runat="server" CssClass="x-large" MaxLength="10" onkeypress="return isNumberKey(event);">
                                                            </asp:TextBox>
                                                            <span class=" label_intro">Mobile No.1</span>
                                                            <%--<asp:RequiredFieldValidator ID="CV_txtMobileNo" runat="server" Display="None" ErrorMessage="MobileNo is required!"
                                                                ControlToValidate="txtMobileNo" ValidateEmptyText="false" SetFocusOnError="true"> </asp:RequiredFieldValidator>--%>
                                                            <%--<ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server"
                                                                TargetControlID="CV_txtMobileNo" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>--%>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtMobileNo"
                                                                ErrorMessage="Minimum 10 digits Mobile No. required." ValidationExpression="[0-9]{10}"
                                                                Display="None" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server"
                                                                TargetControlID="RegularExpressionValidator2" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                        </td>
                                                        <td width="15">
                                                            &nbsp;
                                                        </td>
                                                        <td width="17%">
                                                            <asp:TextBox ID="txtPhoneNo" runat="server" CssClass="x-large" MaxLength="11" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                            <span class=" label_intro">LandLine No.1</span>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtPhoneNo"
                                                                ErrorMessage="Minimum 8 digits Landline No. required." ValidationExpression="^[\s\S]{8,}$"
                                                                Display="None" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server"
                                                                TargetControlID="RegularExpressionValidator3" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                        </td>
                                                        <td width="15">
                                                            &nbsp;
                                                        </td>
                                                        <td width="17%">
                                                            <asp:TextBox ID="txtMobileNo1" runat="server" CssClass="x-large" MaxLength="10" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                            <span class=" label_intro">Mobile No.2</span>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtMobileNo1"
                                                                ErrorMessage="Minimum 10 digits MobileNo required." ValidationExpression="[0-9]{10}"
                                                                Display="None" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender16" runat="server"
                                                                TargetControlID="RegularExpressionValidator4" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                        </td>
                                                        <td width="15">
                                                            &nbsp;
                                                        </td>
                                                        <td width="17%">
                                                            <asp:TextBox ID="txtPhoneNo1" runat="server" CssClass="x-large" MaxLength="11" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                            <span class=" label_intro">LandLine No.2</span>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtPhoneNo1"
                                                                ErrorMessage="Minimum 8 digits PhoneNo required." ValidationExpression="^[\s\S]{8,}$"
                                                                Display="None" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender17" runat="server"
                                                                TargetControlID="RegularExpressionValidator5" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" class="field_title">
                                                Others Info <span class="req">*</span>
                                            </td>
                                            <td align="left" class="field_input">
                                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td width="22%">
                                                            <asp:TextBox ID="txtcstno" runat="server" CssClass="x-large"> </asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="CV_txtCSTNO" runat="server" Display="None" ErrorMessage="CST NO is required!"
                                                                ControlToValidate="txtcstno" ValidateEmptyText="false" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server"
                                                                TargetControlID="CV_txtCSTNO" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                            <span class="label_intro">
                                                                <asp:Label ID="lblcstno" Text="CST NO" runat="server"></asp:Label></span>
                                                        </td>
                                                        <td width="15">
                                                            &nbsp;
                                                        </td>
                                                        <td width="22%">
                                                            <asp:TextBox ID="txtcinno" runat="server" CssClass="x-large"> </asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="CV_txtVCINNO" runat="server" Display="None" ErrorMessage="CIN NO is required!"
                                                                ControlToValidate="txtcinno" ValidateEmptyText="false" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender15" runat="server"
                                                                TargetControlID="CV_txtVCINNO" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                            <span class="label_intro">
                                                                <asp:Label ID="lblvatno" Text="CIN NO" runat="server"></asp:Label></span>
                                                        </td>
                                                        <td width="15">
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txttinno" runat="server" CssClass="mid"> </asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="CV_txtTINNO" runat="server" Display="None" ErrorMessage="TIN NO is required!"
                                                                ControlToValidate="txttinno" ValidateEmptyText="false" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender20" runat="server"
                                                                TargetControlID="CV_txtTINNO" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                            <span class="label_intro">
                                                                <asp:Label ID="lbltinno" Text="TIN NO" runat="server"></asp:Label></span>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title">
                                                <asp:Label ID="Label5" Text="Account Group" runat="server"></asp:Label><span class="req">*</span>
                                            </td>
                                            <td class="field_input">
                                                <asp:DropDownList ID="ddlaccountgroup" runat="server" AppendDataBoundItems="true"
                                                    Style="width: 220px;" class="chosen-select" data-placeholder="Choose a AppliedTo">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfv_ddlaccountgroup" runat="server" ErrorMessage="Required!"
                                                    Font-Bold="true" ForeColor="Red" ControlToValidate="ddlaccountgroup" ValidateEmptyText="false"
                                                    SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title"><asp:Label ID="Label10" Text="USED FOR EXPORT" runat="server"></asp:Label></td>
                                            <td class="field_input"><asp:CheckBox ID="Chkisusedforexport" runat="server" Text=" " /></td>
                                         </tr>
                                         <tr>
                                            <td class="field_title"><asp:Label ID="Label11" Text=" EXPORT TRANSFER TO HO" runat="server"></asp:Label></td>
                                            <td class="field_input"><asp:CheckBox ID="chkisexportranspertoho" runat="server" Text=" " /></td>
                                         </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td style="padding: 8px 0;">
                                                <div class="btn_24_blue">
                                                    <span class="icon disk_co"></span>
                                                    <asp:Button ID="btnBRSubmit" runat="server" Text="Save" CssClass="btn_link" OnClick="btnBRSubmit_Click" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
                                                    <span class="icon cross_octagon_co"></span>
                                                    <asp:Button ID="btnBRCancel" runat="server" Text="Cancel" CssClass="btn_link" OnClick="btnBRCancel_Click"
                                                        CausesValidation="false" />
                                                </div>
                                                <asp:HiddenField ID="Hdn_Fld" runat="server" />
                                                <asp:HiddenField ID="Hdn_Tag" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlCustomerMapping" runat="server">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                        <tr>
                                            <td class="field_title" width="10">
                                                <asp:Label ID="lblCustomername" Text="depot NAME" runat="server"></asp:Label>
                                            </td>
                                            <td class="field_input" width="260">
                                                <asp:TextBox ID="txtDepotname" runat="server" Width="250" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td class="field_title" width="80">
                                                <asp:Label ID="Label8" Text="Order type " runat="server"></asp:Label>
                                            </td>
                                            <td class="field_input" width="180">
                                                <asp:DropDownList ID="ddlordertype" runat="server" Width="150px" Height="28px" class="chosen-select"
                                                    data-placeholder="SELECT ORDER TYPE" AutoPostBack="true" OnSelectedIndexChanged="ddlordertype_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr id="trstate" runat="server">
                                            <td class="field_title" width="80">
                                                <asp:Label ID="Label3" Text="State" runat="server"></asp:Label>
                                            </td>
                                            <td class="field_input" width="180">
                                                <asp:DropDownList ID="ddlstateCustmap" runat="server" ValidationGroup="Customermap"
                                                    AppendDataBoundItems="True" class="chosen-select" data-placeholder="Select State"
                                                    Width="160" AutoPostBack="True" OnSelectedIndexChanged="ddlstateCustmap_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="field_title" width="80">
                                                <asp:Label ID="Label6" Text="district" runat="server"></asp:Label>
                                            </td>
                                            <td class="field_input">
                                                <asp:ListBox ID="ddldistrictcustmap" runat="server" SelectionMode="Multiple" TabIndex="4"
                                                    Width="250px" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddldistrictcustmap_SelectedIndexChanged">
                                                </asp:ListBox>
                                            </td>
                                        </tr>
                                        <tr id="trcountry" runat="server">
                                            <td class="field_title" width="80">
                                                <asp:Label ID="Label9" Text="Country" runat="server"></asp:Label>
                                            </td>
                                            <td class="field_input" width="180">
                                                <asp:DropDownList ID="ddlcountry" runat="server" ValidationGroup="Customermap" AppendDataBoundItems="True"
                                                    class="chosen-select" data-placeholder="Select State" Width="160" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlcountry_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title" width="180">
                                                <asp:Label ID="Label7" Text="Customer Name" runat="server"></asp:Label>
                                            </td>
                                            <td class="field_input" style="padding-left: 10px;" width="20%">
                                                <div class="gridcontent">
                                                    <cc1:Grid ID="gvCustomer" runat="server" CallbackMode="false" Serialize="true" FolderStyle="../GridStyles/premiere_blue"
                                                        PageSize="500" AutoGenerateColumns="false" AllowPageSizeSelection="false" AllowPaging="false"
                                                        AllowAddingRecords="false" AllowFiltering="false" AllowSorting="false">
                                                        <FilteringSettings InitialState="Visible" />
                                                        <Columns>
                                                            <cc1:Column ID="Column13" DataField="CUSTYPE_NAME" HeaderText="TYPE" runat="server"
                                                                Width="120" Wrap="true">
                                                            </cc1:Column>
                                                            <cc1:Column ID="Column11" DataField="CUSTOMERNAME" HeaderText="CUSTOMER NAME" runat="server"
                                                                Width="250" Wrap="true">
                                                            </cc1:Column>
                                                            <cc1:Column ID="Column12" DataField="CUSTOMERID" ReadOnly="true" HeaderText="ID"
                                                                runat="server" Width="80" Wrap="true" Visible="false">
                                                            </cc1:Column>
                                                            <cc1:CheckBoxSelectColumn ID="CheckBoxSelectColumn1" HeaderText=" Select All" DataField=""
                                                                ShowHeaderCheckBox="true" ControlType="Obout" runat="server">
                                                            </cc1:CheckBoxSelectColumn>
                                                        </Columns>
                                                        <ScrollingSettings ScrollHeight="180" />
                                                    </cc1:Grid>
                                                </div>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnAddGrid" runat="server" Text=" " CausesValidation="false" CssClass="addbtn_blue"
                                                    OnClick="btnAddGrid_Click" />
                                            </td>
                                            <td class="field_input" style="padding-left: 10px;" width="20%" colspan="4">
                                                <cc1:Grid ID="gvcustomerdepotmapping" runat="server" Serialize="true" AllowAddingRecords="false"
                                                    AutoGenerateColumns="false" AllowSorting="true" AllowPageSizeSelection="false"
                                                    FolderStyle="../GridStyles/premiere_blue" AllowPaging="false" PageSize="300">
                                                    <Columns>
                                                        <cc1:Column ID="Column14" DataField="GUID" HeaderText="GUID" runat="server" Visible="false" />
                                                        <%-- <cc1:Column ID="Column15"  DataField="Sl" HeaderText="SL" runat="server" Width="60">
                                               <TemplateSettings TemplateId="slnoTemplateFinal" />
                                           </cc1:Column>--%>
                                                        <cc1:Column ID="Column16" DataField="DEPOTID" HeaderText="DEPOTID " runat="server"
                                                            Visible="false">
                                                        </cc1:Column>
                                                        <cc1:Column ID="Column17" DataField="DEPOTNAME" HeaderText="DEPOT NAME " runat="server"
                                                            Visible="false">
                                                        </cc1:Column>
                                                        <cc1:Column ID="Column18" DataField="CUSTOMERTYPE" HeaderText="CUSTOMER TYPE" runat="server"
                                                            Width="220">
                                                        </cc1:Column>
                                                        <cc1:Column ID="Column19" DataField="CUSTOMERID" HeaderText="CUSTOMERID" runat="server"
                                                            Visible="false">
                                                        </cc1:Column>
                                                        <cc1:Column ID="Column20" DataField="CUSTOMERNAME" HeaderText="CUSTOMER NAME " runat="server"
                                                            Width="250">
                                                        </cc1:Column>
                                                        <cc1:Column HeaderText="Delete" AllowEdit="false" AllowDelete="true" Width="80">
                                                            <TemplateSettings TemplateId="deleteBtnTemplatecust" />
                                                        </cc1:Column>
                                                    </Columns>
                                                    <Templates>
                                                        <cc1:GridTemplate runat="server" ID="deleteBtnTemplatecust">
                                                            <Template>
                                                                <a href="javascript: //" class="action-icons c-delete" id="btnGridCustDelete_<%# Container.PageRecordIndex %>"
                                                                    onclick="CallDeleteServerMethod(this)"></a>
                                                            </Template>
                                                        </cc1:GridTemplate>
                                                        <cc1:GridTemplate runat="server" ID="slnoTemplateFinal">
                                                            <Template>
                                                                <asp:Label ID="lblslnoTaxFinal" runat="server" Text='<%#Container.PageRecordIndex+1 %>'></asp:Label>
                                                            </Template>
                                                        </cc1:GridTemplate>
                                                    </Templates>
                                                    <ScrollingSettings ScrollHeight="200" />
                                                </cc1:Grid>
                                                <asp:Button ID="btngrddelete" runat="server" Text="grddelete" Style="display: none"
                                                    OnClick="btngrddelete_Click" OnClientClick="return confirm('Are you sure you want to delete this record?');" />
                                                <asp:HiddenField ID="hdndcustomerDelete" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7" style="padding: 8px 10px;">
                                                <div class="btn_24_blue">
                                                    <span class="icon disk_co"></span>
                                                    <asp:Button ID="btnDepotCustMapSubmit" runat="server" Text="Save" CssClass="btn_link"
                                                        CausesValidation="false" OnClick="btnDepotCustMapSubmit_Click" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
                                                    <span class="icon cross_octagon_co"></span>
                                                    <asp:Button ID="btnDepotCustMapCancel" runat="server" Text="Cancel" CssClass="btn_link"
                                                        CausesValidation="false" OnClick="btnDepotCustMapCancel_Click" />
                                                </div>
                                            </td>
                                            <asp:HiddenField ID="hdnCustCategory" runat="server" />
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="PnlTransporter" runat="server">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                        <tr>
                                            <td  class="field_title">
                                                <asp:Label ID="Label25" Text="Depot" runat="server"></asp:Label>
                                            </td>
                                            <td class="field_input" >
                                                <asp:TextBox ID="txtDepot" runat="server" CssClass="large" Enabled="false"></asp:TextBox>
                                            </td>
                                             <td class="field_title"  >
                                                    <asp:Label ID="Label19" Text="State" runat="server"></asp:Label>
                                                </td>
                                                <td  class="field_input">
                                                    <asp:ListBox ID="ddlstatetransporter" runat="server" SelectionMode="Multiple" TabIndex="4"  name="options[]" multiple="multiple"
                                                        Width="200%" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlstatetransporter_SelectedIndexChanged"
                                                        AutoPostBack="true"></asp:ListBox>
                                                </td>
                                        </tr>
                                        <tr>
                                            
                                            <td colspan="4" class="field_input" style="padding-left: 10px;">
                                                <fieldset>
                                                    <legend>TRANSPORTER MAPPING</legend>
                                                    <table border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                                        <tr>
                                                            <td class="field_input">
                                                                <div class="gridcontent-short">
                                                                    <ul>
                                                                        <li class="left">
                                                                            <cc1:Grid ID="grdFirstGrid" runat="server" FolderStyle="../GridStyles/premiere_blue"
                                                                                AutoGenerateColumns="false" AllowSorting="false" AllowPageSizeSelection="true"
                                                                                Width="50" PageSize="300" AllowPaging="false" AllowFiltering="false" Serialize="true"
                                                                                AllowAddingRecords="false">
                                                                                <FilteringSettings InitialState="Visible" />
                                                                                <Columns>
                                                                                    <cc1:Column ID="Column15" DataField="NAME" HeaderText="TRANSPORTER" runat="server"
                                                                                        Width="350%" Wrap="true">
                                                                                        <FilterOptions>
                                                                                            <cc1:FilterOption Type="NoFilter" />
                                                                                            <cc1:FilterOption Type="Contains" />
                                                                                            <cc1:FilterOption Type="StartsWith" />
                                                                                        </FilterOptions>
                                                                                    </cc1:Column>
                                                                                    <cc1:Column ID="Column30" DataField="ID" ReadOnly="true" HeaderText="ID" runat="server"
                                                                                        Width="50" Wrap="true">
                                                                                        <TemplateSettings TemplateId="CheckProductTemplate" HeaderTemplateId="HeaderTemplate" />
                                                                                    </cc1:Column>
                                                                                    <cc1:CheckBoxSelectColumn ID="CheckBoxSelectColumn2" HeaderText="" DataField="" ShowHeaderCheckBox="true"
                                                                                        ControlType="Obout" runat="server">
                                                                                    </cc1:CheckBoxSelectColumn>
                                                                                </Columns>
                                                                                <FilteringSettings MatchingType="AnyFilter" />
                                                                                <Templates>
                                                                                </Templates>
                                                                                <ScrollingSettings ScrollWidth="90%" ScrollHeight="180" />
                                                                            </cc1:Grid>
                                                                        </li>
                                                                        <li class="middle">
                                                                            <asp:Button ID="btnTrnsporteradd" runat="server" Text="" CausesValidation="false"
                                                                                CssClass="addbtn_blue" OnClick="btnTrnsporteradd_Click" />
                                                                        </li>
                                                                        <li class="right">
                                                                            <cc1:Grid ID="grdFinalGrid" Width="50" runat="server" Serialize="true" AllowAddingRecords="false"
                                                                                AutoGenerateColumns="false" AllowSorting="true" AllowPageSizeSelection="true"
                                                                                FolderStyle="../GridStyles/premiere_blue" AllowPaging="false" PageSize="100">
                                                                                <Columns>
                                                                                    <cc1:Column ID="Column37" DataField="SlNo" HeaderText="SL" runat="server" Width="60">
                                                                                        <TemplateSettings TemplateId="slnoTemplateFinal1" />
                                                                                    </cc1:Column>
                                                                                    <cc1:Column ID="Column38" DataField="GUID" HeaderText="GUID" runat="server" Visible="false" />
                                                                                    <cc1:Column ID="Column39" DataField="DEPOTID" HeaderText="DEPOTID" Visible="false"
                                                                                        runat="server">
                                                                                    </cc1:Column>
                                                                                    <cc1:Column ID="Column40" DataField="DEPOTNAME" HeaderText="DEPOTNAME" Width="50"
                                                                                        Visible="false" runat="server">
                                                                                    </cc1:Column>
                                                                                    <cc1:Column ID="Column41" DataField="TRANSPOTERID" HeaderText="TRANSPORTERID" Visible="false"
                                                                                        runat="server">
                                                                                    </cc1:Column>
                                                                                    <cc1:Column ID="Column42" DataField="TRANSPOTERNAME" HeaderText="NAME" Width="220"
                                                                                        runat="server">
                                                                                    </cc1:Column>
                                                                                    <cc1:Column ID="Column44" DataField="TAG" HeaderText="TAG" Width="150" runat="server"
                                                                                        Visible="false">
                                                                                    </cc1:Column>
                                                                                    <cc1:Column HeaderText="Delete" AllowEdit="false" AllowDelete="true" Width="80">
                                                                                        <TemplateSettings TemplateId="deleteBtnTransporter" />
                                                                                    </cc1:Column>
                                                                                </Columns>
                                                                                <Templates>
                                                                                    <cc1:GridTemplate runat="server" ID="deleteBtnTransporter">
                                                                                        <Template>
                                                                                            <a href="javascript: //" class="action-icons c-delete" id="btnGridDelete_<%# Container.PageRecordIndex %>"
                                                                                                onclick="CallTransportDeleteMethod(this)"></a>
                                                                                        </Template>
                                                                                    </cc1:GridTemplate>
                                                                                </Templates>
                                                                                <Templates>
                                                                                    <cc1:GridTemplate runat="server" ID="slnoTemplateFinal1">
                                                                                        <Template>
                                                                                            <asp:Label ID="lblslnoTaxFinal" runat="server" Text='<%#Container.PageRecordIndex+1 %>'></asp:Label>
                                                                                        </Template>
                                                                                    </cc1:GridTemplate>
                                                                                </Templates>
                                                                                <ScrollingSettings ScrollWidth="90%" ScrollHeight="180" />
                                                                            </cc1:Grid>
                                                                            <asp:Button ID="btntransporterdelete" runat="server" CausesValidation="false" Text="grddelete"
                                                                                Style="display: none" OnClick="btntransporterdelete_Click" OnClientClick="return confirm('Are you sure you want to delete this record?');" />
                                                                            <asp:HiddenField ID="HiddenField1" runat="server" />
                                                                            <asp:HiddenField ID="HiddenField2" runat="server" />
                                                                            <asp:HiddenField ID="HiddenField3" runat="server" />
                                                                            <asp:HiddenField ID="HiddenField4" runat="server" />
                                                                            <asp:HiddenField ID="Hdn_Fld_Delete" runat="server" />
                                                                        </li>
                                                                    </ul>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 10px;" colspan="2" class="field_input">
                                                <div class="btn_24_blue">
                                                    <span class="icon disk_co"></span>
                                                    <asp:Button ID="btnTransSubmit" runat="server" Text="Save" CssClass="btn_link" CausesValidation="false"
                                                        OnClick="btnTransSubmit_Click" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
                                                    <span class="icon cross_octagon_co"></span>
                                                    <asp:Button ID="btnTransCancel" runat="server" Text="Cancel" CssClass="btn_link"
                                                        CausesValidation="false" OnClick="btnTransCancel_Click" />
                                                </div>
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
                                        <cc1:Grid ID="gvBranch" runat="server" CallbackMode="false" Serialize="true" FolderStyle="../GridStyles/premiere_blue"
                                            AutoGenerateColumns="false" AllowPageSizeSelection="false" OnDeleteCommand="DeleteRecord"
                                            EnableRecordHover="true" AllowAddingRecords="false" AllowFiltering="true" PageSize="200">
                                            <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnClientDelete="OnDelete" />
                                            <FilteringSettings InitialState="Visible" />
                                            <Columns>
                                                <cc1:Column ID="Column1" DataField="BRID" ReadOnly="true" HeaderText="BRID" runat="server"
                                                    Visible="false" />
                                                <cc1:Column ID="Column2" DataField="BRCODE" HeaderText="BRANCH CODE" runat="server"
                                                    Width="140" Wrap="true">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column3" DataField="BRNAME" HeaderText="BRANCH NAME" runat="server"
                                                    Width="140" Wrap="true">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column4" DataField="BRDESCRIPTION" HeaderText="DESCRIPTION" runat="server"
                                                    Width="180" Wrap="true" Visible="false">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column5" DataField="BRPREFIX" HeaderText="PREFIX" runat="server"
                                                    Width="140">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column6" DataField="ADDRESS" HeaderText="ADDRESS" runat="server"
                                                    Visible="false" />
                                                <cc1:Column ID="Column7" DataField="PHONENO" HeaderText="PHONENO" SortOrder="Asc"
                                                    runat="server" Width="140" Visible="false">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column10" DataField="BRANCHTAGDESC" HeaderText="TYPE" SortOrder="Asc"
                                                    runat="server" Width="140">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column8" DataField="MOBILENO" HeaderText="MOBILENO" runat="server"
                                                    Width="140">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column9" AllowEdit="true" AllowDelete="true" HeaderText="EDIT" runat="server"
                                                    Width="80">
                                                    <TemplateSettings TemplateId="editBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column HeaderText="Customer Mapping" AllowEdit="false" AllowDelete="true" Width="70"
                                                    Wrap="true">
                                                    <TemplateSettings TemplateId="DepotCustMapping" />
                                                </cc1:Column>
                                                <cc1:Column HeaderText="Transporter Mapping" AllowEdit="false" AllowDelete="true"
                                                    Width="70" Visible="true" Wrap="true">
                                                    <TemplateSettings TemplateId="TransporterMapping" />
                                                </cc1:Column>
                                                <cc1:Column HeaderText="DELETE" AllowEdit="false" AllowDelete="true" Width="80">
                                                    <TemplateSettings TemplateId="deleteBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column21" DataField="BRANCHTAG" HeaderText="BRANCHTAG" runat="server"
                                                    Visible="false" Width="140">
                                                </cc1:Column>
                                            </Columns>
                                            <FilteringSettings MatchingType="AnyFilter" />
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="editBtnTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="action-icons c-edit" id="btnGridEdit_<%# Container.PageRecordIndex %>"
                                                            onclick="CallServerMethod(this)"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="deleteBtnTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="action-icons c-delete" title="Delete" onclick="gvBranch.delete_record(this)">
                                                        </a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="DepotCustMapping">
                                                    <Template>
                                                        <a href="javascript: //" class="h_icon map" title="Customer Mapping" onclick="openDepotCustomer('<%# Container.DataItem["BRNAME"] %>','<%# Container.DataItem["BRID"] %>')" />
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="TransporterMapping">
                                                    <Template>
                                                        <a href="javascript: //" class="h_icon glyphicon-apple" title="Transporter Mapping"
                                                            onclick="openTransporter('<%# Container.DataItem["BRNAME"] %>','<%# Container.DataItem["BRID"] %>','<%# Container.DataItem["BRANCHTAG"] %>')" />
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <ScrollingSettings ScrollWidth="100%" ScrollHeight="350" />
                                        </cc1:Grid>
                                        <asp:Button ID="btngridsave" runat="server" Text="GridSave" Style="display: none"
                                            OnClick="btngridsave_Click" CausesValidation="false" />
                                        <asp:Button ID="btnDepotCustMapping" runat="server" Text="Mapping" Style="display: none"
                                            OnClick="btnDepotCustMapping_Click" CausesValidation="false" />
                                        <asp:Button ID="btnTransporter" runat="server" Text="Mapping" Style="display: none"
                                            OnClick="btnTransporter_Click" CausesValidation="false" />
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
                    document.getElementById("<%=Hdn_Fld.ClientID %>").value = record.BRID;
                    if (confirm("Are you sure you want to delete? "))
                        return true;
                    else
                        return false;
                }
                function OnDelete(record) {
                    alert(record.Error);
                }

                function openDepotCustomer(Depotname, Depotid) {
                    document.getElementById("<%=Hdn_Fld.ClientID %>").value = Depotid;
                    document.getElementById("<%=txtDepotname.ClientID %>").value = Depotname;
                    document.getElementById("<%=btnDepotCustMapping.ClientID %>").click();
                }
                function openTransporter(Depotname, Depotid, BRANCHTAG) {
                    document.getElementById("<%=txtDepot.ClientID %>").value = Depotname;
                    document.getElementById("<%=Hdn_Fld.ClientID %>").value = Depotid;
                    document.getElementById("<%=Hdn_Tag.ClientID %>").value = BRANCHTAG;
                    document.getElementById("<%=btnTransporter.ClientID %>").click();
                }
            </script>
            <script type="text/javascript">
                function CallDeleteServerMethod(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnGridCustDelete_", "");
                    document.getElementById("<%=hdndcustomerDelete.ClientID %>").value = gvcustomerdepotmapping.Rows[iRecordIndex].Cells[0].Value;
                    document.getElementById("<%=btngrddelete.ClientID %>").click();
                }

                function CallTransportDeleteMethod(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnGridDelete_", "");
                    document.getElementById("<%=Hdn_Fld_Delete.ClientID %>").value = grdFinalGrid.Rows[iRecordIndex].Cells[1].Value;
                    document.getElementById("<%=btntransporterdelete.ClientID %>").click();

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
                    document.getElementById("<%=Hdn_Fld.ClientID %>").value = gvBranch.Rows[iRecordIndex].Cells[0].Value;
                    document.getElementById("<%=btngridsave.ClientID %>").click();
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
                    gvBranch.addFilterCriteria('BRCODE', OboutGridFilterCriteria.Contains, searchValue);
                    gvBranch.addFilterCriteria('BRNAME', OboutGridFilterCriteria.Contains, searchValue);
                    gvBranch.addFilterCriteria('BRDESCRIPTION', OboutGridFilterCriteria.Contains, searchValue);
                    gvBranch.addFilterCriteria('BRPREFIX', OboutGridFilterCriteria.Contains, searchValue);
                    gvBranch.addFilterCriteria('PHONENO', OboutGridFilterCriteria.Contains, searchValue);
                    gvBranch.addFilterCriteria('MOBILENO', OboutGridFilterCriteria.Contains, searchValue);
                    gvBranch.executeFilter();
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