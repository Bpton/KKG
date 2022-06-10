<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmVendor_TPU.aspx.cs" Inherits="VIEW_frmVendor_TPU" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/bootstrap-multiselect.css" rel="stylesheet" />
    <script type="text/javascript" src="../js/bootstrap.js"></script>
    <script type="text/javascript" src="../js/bootstrap-multiselect.js"></script>
    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to Tax Mapping?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
                   <script type="text/javascript">
                       function CHKGSTM() {                          
                           var txtgstno = $('#<%=txtgstno.ClientID%>').val();
                           var txtgstnoL = txtgstno.length;
                            if (txtgstnoL == 3) { }
                           else {

                                alert('Invalid No.');
                                $('#<%=txtgstno.ClientID%>').val('');
                                    return false;
                           }
                            for (var i = 0; i < txtgstnoL; i++) {                    
                                if (i == 0) {
                                    var n = txtgstno.charCodeAt(0);
                                if (n < 48 || n > 57) {
                                    alert('Enter Only Number for 1st digit');
                                    $('#<%=txtgstno.ClientID%>').val('');
                                    return false;
                                }
                            }
                            if (i == 1) {
                                var n1 = txtgstno.charCodeAt(1);                      
                                if ((n1 >= 65 && n1 <= 90) || (n1 >= 97 && n1 <= 122)) {
                                }
                                else {
                                     alert('Enter Only Letter for 2nd digit');
                                     $('#<%=txtgstno.ClientID%>').val('');
                                    return false;
                                }
                            }
                             if (i == 2) {
                                var n2 = txtgstno.charCodeAt(2);
                                 if ((n2 >= 48 && n2 <= 57) || (n2 >= 65 && n2 <= 90) || (n2 >= 97 && n2 <= 122)) {
                                 }
                                 else {
                                      alert('Enter only Number or letter for 3rd digit');
                                      $('#<%=txtgstno.ClientID%>').val('');
                                     return false;
                                 }
                            }
                        }
                 return true;
                       }
                      </script>
         <script type="text/javascript">
        function calchkgst() {
                         var checkedValue = document.getElementById("<%= chkgst.ClientID%>");
                         var txtstatecode = "ContentPlaceHolder1_txtstatecode";
                         var txtgstpanno = "ContentPlaceHolder1_txtgstpanno";
                         var txtgstno = "ContentPlaceHolder1_txtgstno";
            
                         if (checkedValue.checked == true) {
                             checkedValue.checked = false;
                             document.getElementById(txtstatecode).value = "";
                             document.getElementById(txtgstpanno).value = "";
                             document.getElementById(txtgstno).value = "";
                         }
						
					}
				</script>

        <script type="text/javascript">
                       function CHKPAN() {                          
                           var txtPanNO = $('#<%=txtpanno.ClientID%>').val();
                           var txtgstnoL = txtPanNO.length; 
                            calchkgst();
                           if (txtgstnoL == 10) { }
                           else {

                                alert('PAN No. must be 10 digit. Invalid PAN No.');
                               $('#<%=txtpanno.ClientID%>').val('');
                                    return false;
                           }
                           for (var i = 0; i < txtgstnoL; i++) {  
                                if (i >=0 && i<=4) {
                                    var n1 = txtPanNO.charCodeAt(i);                                   
                                if ((n1 >= 65 && n1 <= 90) || (n1 >= 97 && n1 <= 122)) {
                                }
                                else {
                                     alert('1st 5 digit must be string.');
                                    $('#<%=txtpanno.ClientID%>').val('');
                                    return false;
                                }
                               }

                                if (i >4 && i<=8) {
                                    var n = txtPanNO.charCodeAt(i);
                                if (n < 48 || n > 57) {
                                     alert('6th to 9th must be number.');
                                    $('#<%=txtpanno.ClientID%>').val('');
                                     return false;
                                }
                            }
                            
                             if (i == 9) {
                                var n2 = txtPanNO.charCodeAt(i);
                                 if ((n2 >= 65 && n2 <= 90) || (n2 >= 97 && n2 <= 122)) {
                                 }
                                 else {
                                      alert('10th digit must be string.');
                                     $('#<%=txtpanno.ClientID%>').val('');
                                     return false;
                                 }
                            }
                        }
                 return true;
                       }
                      </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
            <asp:Image ID="Image1" ImageUrl="../images/loading123.gif" AlternateText="Processing" runat="server" />
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
                                <h6>TPU/Factory/Vendor Details
                                </h6>
                                <div class="btn_30_light" style="float: right;" id="btnaddhide" runat="server">
                                    <span class="icon add_co"></span>
                                    <asp:Button ID="btnAddTPUVendor" runat="server" Text="Add New Record" CssClass="btn_link"
                                        OnClick="btnAddTPUVendor_Click" CausesValidation="false" />
                                </div>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <td align="left" class="field_title">
                                                        <asp:Label ID="lblSupliedItem" runat="server" Text="Supplied&nbspItem&nbsp&nbsp" />
                                                        <span class="req">*</span>
                                                    </td>
                                                    <td align="left" valign="top" class="field_input">
                                                        <asp:DropDownList ID="ddlSupliedItem" Width="200" runat="server" class="chosen-select"
                                                            data-placeholder="Choose Suplied" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlSupliedItem_SelectedIndexChanged"
                                                            AutoPostBack="true">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="CV_ddlSupliedItem" runat="server" ForeColor="Red" ValidationGroup="TPUSubmit"
                                                            ErrorMessage="Required!" ControlToValidate="ddlSupliedItem" ValidateEmptyText="false"
                                                            SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
                                                    </td>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr id="hdnType" runat="server">
                                            <td align="left" class="field_title">Applicable <span class="req">*</span>
                                            </td>
                                            <td align="left" class="field_input">
                                                <asp:RadioButtonList ID="RbApplicable" AutoPostBack="true" RepeatDirection="Horizontal"
                                                    runat="server" OnSelectedIndexChanged="RbApplicable_SelectedIndexChanged">
                                                    <asp:ListItem Value="T">TPU</asp:ListItem>
                                                    <asp:ListItem Value="A">GST PURPOSE ONLY(NO LEDGER CREATION)</asp:ListItem>
                                                </asp:RadioButtonList>

                                                <%--<asp:RadioButton ID="rdbTPU" AutoPostBack="true" runat="server" Text="    TPU  " GroupName="BR" />
                                                <asp:RadioButton ID="rdbFactory" runat="server" Text="    ACCOUNTS  " GroupName="BR" />--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" class="field_title">
                                                <asp:Label ID="lblApplicable" runat="server" /><span class="req">*</span></td>
                                            <td class="field_input">                                                
                                                <asp:DropDownList ID="ddlTpumap" runat="server" class="chosen-select" TabIndex="4"
                                                    Width="180" AppendDataBoundItems="True"></asp:DropDownList>
                                                <asp:CustomValidator ID="cvddlTpumap" runat="server" ValidateEmptyText="true"
                                                    ControlToValidate="ddlTpumap" ValidationGroup="submitvalidation" ErrorMessage="Required!"
                                                    Display="Dynamic" ClientValidationFunction="ValidateListBox" ForeColor="Red"></asp:CustomValidator>

                                                <asp:DropDownList ID="ddlgroup" runat="server" class="chosen-select" TabIndex="4"
                                                    Width="180" AppendDataBoundItems="True"></asp:DropDownList>
                                                <asp:CustomValidator ID="CVddlgroup" runat="server" ValidateEmptyText="true"
                                                    ControlToValidate="ddlgroup" ValidationGroup="submitvalidation" ErrorMessage="Required!"
                                                    Display="Dynamic" ClientValidationFunction="ValidateListBox" ForeColor="Red"></asp:CustomValidator>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td width="120" class="field_title">
                                                <asp:Label ID="lblCode" Text="Code(Auto Generated)" runat="server"></asp:Label>

                                            </td>
                                            <td class="field_input">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="12%">
                                                            <asp:TextBox ID="txtCode" runat="server" CssClass="x-large" MaxLength="20" Enabled="false"></asp:TextBox>

                                                        </td>
                                                        <td width="25">&nbsp;
                                                        </td>
                                                        <td width="8%" class="innerfield_title">
                                                            <asp:Label ID="lblName" Text="Party Name" runat="server"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="28%">
                                                            <asp:TextBox ID="txtName" runat="server" CssClass="x-large"> </asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="CV_Name" runat="server" Display="None" ErrorMessage="Vendor Name is required!"
                                                                ControlToValidate="txtName" ValidateEmptyText="false" SetFocusOnError="true"
                                                                ValidationGroup="TPUSubmit"> </asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                                TargetControlID="CV_Name" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" valign="middle" class="field_title">
                                                <asp:Label ID="lblAdress" Text="Address Info 1" runat="server"></asp:Label>
                                                <span class="req">*</span>
                                            </td>
                                            <td align="left" class="field_input">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="15%">
                                                            <asp:DropDownList ID="ddlState" runat="server" Width="160" AutoPostBack="true" AppendDataBoundItems="True"
                                                                class="chosen-select" data-placeholder="Select State" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <span class="label_intro">State
                                                                <asp:RequiredFieldValidator ID="CV_ddlState" runat="server" ErrorMessage="Required!"
                                                                    ForeColor="Red" ControlToValidate="ddlState" ValidateEmptyText="false" SetFocusOnError="true"
                                                                    InitialValue="0" ValidationGroup="TPUSubmit"> </asp:RequiredFieldValidator></span>
                                                        </td>
                                                        <td width="15%">
                                                            <asp:DropDownList ID="ddlDistrict" runat="server" Width="160" AutoPostBack="true"
                                                                AppendDataBoundItems="True" class="chosen-select" data-placeholder="Select District"
                                                                OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <span class="label_intro">District
                                                                <asp:RequiredFieldValidator ID="CV_ddlDistrict" runat="server" ErrorMessage="Required!"
                                                                    ForeColor="Red" ControlToValidate="ddlDistrict" ValidateEmptyText="false" SetFocusOnError="true"
                                                                    ValidationGroup="TPUSubmit" InitialValue="0"> </asp:RequiredFieldValidator></span>
                                                        </td>
                                                        <td width="15%">
                                                            <asp:DropDownList ID="ddlCity" runat="server" Width="160" AppendDataBoundItems="true"
                                                                class="chosen-select" data-placeholder="Select City">
                                                            </asp:DropDownList>
                                                            <span class="label_intro">City
                                                                <asp:RequiredFieldValidator ID="CV_ddlCity" runat="server" ErrorMessage="Required!"
                                                                    ForeColor="Red" ControlToValidate="ddlCity" ValidateEmptyText="false" SetFocusOnError="true"
                                                                    InitialValue="0"> </asp:RequiredFieldValidator></span>
                                                        </td>
                                                        <td valign="top">
                                                            <asp:Button ID="btnAddCity" runat="server" CssClass="h_icon add_co" ToolTip="Add City"
                                                                OnClick="btnAddCity_Click" CausesValidation="false" Enabled="false" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:Button ID="btnRefresh" runat="server" CssClass="h_icon arrow_refresh_co" ToolTip="Refresh"
                                                                OnClick="btnRefresh_Click" CausesValidation="false" Enabled="false" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td colspan="2" height="2"></td>
                                                    </tr>
                                                    <tr>
                                                        <td width="40%">
                                                            <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" CssClass="large"
                                                                MaxLength="255"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="CV_txtAddress" runat="server" Display="None" ErrorMessage="Address is required!"
                                                                ControlToValidate="txtAddress" ValidateEmptyText="false" SetFocusOnError="true"
                                                                ValidationGroup="TPUSubmit"> </asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server"
                                                                TargetControlID="CV_txtAddress" PopupPosition="BottomLeft" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                            <span class="label_intro">Contact Address</span>
                                                        </td>
                                                        <td valign="top" style="padding: 0 0 0 15px;">
                                                            <asp:TextBox ID="txtPIN" runat="server" MaxLength="6" Width="100" onkeypress="return isNumberKey(event);"> </asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPIN"
                                                                ErrorMessage="Minimum 6 digits PIN/ZIP required." ValidationExpression="[0-9]{6}"
                                                                Display="None" SetFocusOnError="true" ValidationGroup="TPUSubmit"></asp:RegularExpressionValidator>
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
                                                <asp:Label ID="Label3" Text="Address Info 2" runat="server"></asp:Label>
                                                <span class="req"></span>
                                            </td>
                                            <td align="left" class="field_input">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0" style="display: none">
                                                    <tr>
                                                        <td width="21%">
                                                            <asp:DropDownList ID="ddlState1" runat="server" Width="160" AutoPostBack="true" AppendDataBoundItems="True"
                                                                class="chosen-select" data-placeholder="Select State" OnSelectedIndexChanged="ddlState1_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="21%">
                                                            <asp:DropDownList ID="ddlDistrict1" runat="server" Width="160" AutoPostBack="true"
                                                                AppendDataBoundItems="True" class="chosen-select" data-placeholder="Select District"
                                                                OnSelectedIndexChanged="ddlDistrict1_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="21%">
                                                            <asp:DropDownList ID="ddlCity1" runat="server" Width="160" AppendDataBoundItems="true"
                                                                class="chosen-select" data-placeholder="Select City">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td valign="top">
                                                            <asp:Button ID="btnAddCity1" runat="server" CssClass="h_icon add_co" ToolTip="Add City"
                                                                OnClick="btnAddCity1_Click" CausesValidation="false" Enabled="false" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:Button ID="btnnewRefresh" runat="server" CssClass="h_icon arrow_refresh_co"
                                                                ToolTip="Refresh" OnClick="btnnewRefresh_Click" CausesValidation="false" Enabled="false" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td colspan="2" height="2"></td>
                                                    </tr>
                                                    <tr>
                                                        <td width="40%">
                                                            <asp:TextBox ID="txtAddress1" runat="server" TextMode="MultiLine" CssClass="large"></asp:TextBox>
                                                            <span class="label_intro">Contact Address</span>
                                                        </td>
                                                        <td valign="top" style="padding: 0 0 0 15px;">
                                                            <asp:TextBox ID="txtPIN1" runat="server" MaxLength="6" Width="100" onkeypress="return isNumberKey(event);"> </asp:TextBox>
                                                            <span class="label_intro">Pin code</span>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" class="field_title">Contact Person
                                            </td>
                                            <td align="left" class="field_input">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td align="left" class="field_input" width="210">
                                                            <asp:TextBox ID="txtcontactperson" runat="server" Width="300"></asp:TextBox>
                                                            <span class="label_intro">Name</span>
                                                        </td>
                                                        <td align="left" class="field_input" width="210">
                                                            <asp:TextBox ID="txtEmailid" runat="server" Width="260"> </asp:TextBox>
                                                            <span class="label_intro">E-mail ID
                                                                <asp:RegularExpressionValidator ID="CV_email_valid" runat="server" Display="None"
                                                                    ErrorMessage="Enter Valid Email ID!" ControlToValidate="txtEmailid" SetFocusOnError="true"
                                                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="TPUSubmit"> </asp:RegularExpressionValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                                                    TargetControlID="CV_email_valid" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="../images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                            </span>
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" class="field_title">Contact Info
                                            </td>
                                            <td align="left" class="field_input">
                                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td width="130">
                                                            <asp:TextBox ID="txtMobileNo" runat="server" MaxLength="10" Width="120" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                            <span class=" label_intro">Mobile No</span>
                                                        </td>
                                                        <td width="12">&nbsp;
                                                        </td>
                                                        <td width="130">
                                                            <asp:TextBox ID="txtPhoneNo" runat="server" MaxLength="11" Width="120" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtPhoneNo"
                                                                ErrorMessage="Minimum 8 digits PhoneNo required." ValidationExpression="^[\s\S]{8,}$"
                                                                Display="None" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server"
                                                                TargetControlID="RegularExpressionValidator3" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                            <span class=" label_intro">LandLine No</span>
                                                        </td>
                                                        <td width="12">&nbsp;
                                                        </td>
                                                        <td width="130">
                                                            <asp:TextBox ID="txtMobileNo1" runat="server" MaxLength="10" Width="120" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtMobileNo1"
                                                                ErrorMessage="Minimum 10 digits MobileNo required." ValidationExpression="[0-9]{10}"
                                                                Display="None" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender25" runat="server"
                                                                TargetControlID="RegularExpressionValidator4" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                            <span class=" label_intro">Mobile No 2</span>
                                                        </td>
                                                        <td width="12">&nbsp;
                                                        </td>
                                                        <td width="130">
                                                            <asp:TextBox ID="txtPhoneNo1" runat="server" MaxLength="11" Width="120" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtPhoneNo1"
                                                                ErrorMessage="Minimum 8 digits PhoneNo required." ValidationExpression="^[\s\S]{8,}$"
                                                                Display="None" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender26" runat="server"
                                                                TargetControlID="RegularExpressionValidator5" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                            <span class=" label_intro">LandLine No 2</span>
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" class="field_title">
                                                <asp:Label ID="lblpartyInfo" Text="Party Info" runat="server"></asp:Label>
                                                            <span class="req">*</span>
                                            </td>
                                            <td align="left" class="field_input">
                                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td width="130">
                                                            <asp:TextBox ID="txtpanno" runat="server" Width="120" MaxLength="10" 
                                                                 onkeypress="return isLetterNumberKey(event);" onchange="CHKPAN();"
                                                                Style='text-transform: uppercase'
                                                                onkeyup="panno()"> </asp:TextBox>
                                                           <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtpanno"
                                                                Display="None" ForeColor="Red" ErrorMessage="Invalid PAN No" SetFocusOnError="true"
                                                                ValidationExpression="[A-Za-z]{5}\d{4}[A-Za-z]{1}" ValidationGroup="TPUSubmit"></asp:RegularExpressionValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
                                                                TargetControlID="RegularExpressionValidator2" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                             <asp:RequiredFieldValidator ID="Reqpanno" runat="server" Display="None" ErrorMessage="Panno Name is required!"
                                                                ControlToValidate="txtpanno" ValidateEmptyText="false" SetFocusOnError="true"
                                                                ValidationGroup="TPUSubmit"> </asp:RequiredFieldValidator>--%>
                                                            <span class="label_intro">
                                                                <asp:Label ID="lblpanno" Text="PAN NO &nbsp;(ex:AAACJ4124B)" runat="server"></asp:Label></span>
                                                        </td>
                                                        <td width="12">&nbsp;
                                                        </td>
                                                        <td width="130">
                                                            <asp:TextBox ID="txtcstno" runat="server" Width="120"> </asp:TextBox>
                                                            <span class="label_intro">
                                                                <asp:Label ID="lblcstno" Text="CST NO" runat="server"></asp:Label></span>
                                                        </td>
                                                        <td width="12">&nbsp;
                                                        </td>
                                                        <td width="130">
                                                            <asp:TextBox ID="txtvatno" runat="server" Width="120"> </asp:TextBox>
                                                            <span class="label_intro">
                                                                <asp:Label ID="lblvatno" Text="VAT NO" runat="server"></asp:Label></span>
                                                        </td>
                                                        <td width="12">&nbsp;
                                                        </td>
                                                        <td width="130">
                                                            <asp:TextBox ID="txttinno" runat="server" Width="120"> </asp:TextBox>
                                                            <span class="label_intro">
                                                                <asp:Label ID="lbltinno" Text="TIN NO" runat="server"></asp:Label></span>
                                                        </td>
                                                        <td width="12">&nbsp;
                                                        </td>
                                                        <td width="130">
                                                            <asp:TextBox ID="txtSTNo" runat="server" Width="120"> </asp:TextBox>
                                                            <span class="label_intro">
                                                                <asp:Label ID="Label1" Text="ST NO" runat="server"></asp:Label></span>
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr id="tr1" runat="server">
                                            <td class="field_title">
                                                 <asp:Label ID="lblgstApplicable" Text="GST APPLICABLE" runat="server"></asp:Label>
                                                            <span class="req">*</span>
                                            </td>
                                            <td class="field_input">
                                                <table border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td width="40">
                                                            <asp:CheckBox ID="chkgst" runat="server" Text=" " AutoPostBack="true" OnCheckedChanged="chkgst_CheckedChanged" />                                                           
                                                        </td>
                                                        <td width="60">
                                                            <asp:Label ID="Label5" CssClass="innerfield_title" Text="GSTIN" runat="server">
                                                            </asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtstatecode" runat="server" MaxLength="15" Enabled="false" Width="15px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtgstpanno" runat="server" MaxLength="15" Enabled="false" Width="80px"
                                                                Style='text-transform: uppercase'></asp:TextBox>
                                                        </td>
                                                        <td width="250">
                                                      
                                                            <asp:TextBox ID="txtgstno"  Style='text-transform: uppercase' onchange="CHKGSTM();"
                                                                 onkeyup="return isLetterNumberKey3D(event);"
                                                                 onkeypress="return isLetterNumberKey(event);" 
                                                                runat="server" MaxLength="03" Width="30"></asp:TextBox>
                                                        </td>



                                                        <td align="left" valign="top" class="field_input">
                                                            <asp:Button ID="btnaddgst" runat="server" ToolTip="Add GST" CssClass="h_icon add_co"
                                                                Enabled="true" CausesValidation="false" OnClick="btnaddgst_Click" />
                                                        </td>
                                                    </tr>


                                                    <tr runat="server" id="trGST">
                                                        <td class="field_input" style="padding-left: 10px;" colspan="2">
                                                            <fieldset>
                                                                <legend>STate Wise GST MAPPING</legend>
                                                                <table border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                                                    <tr>
                                                                        <td valign="top">
                                                                            <div style="margin: 0 auto; width: 500PX;">
                                                                                <div style="overflow: hidden;" id="DivHeaderRow" style="position: absolute">
                                                                                </div>
                                                                                <div id="DivMainContent" style="overflow: scroll; height: 300px;">
                                                                                    <asp:GridView ID="gvStateWiseGst" runat="server" Width="100%" CssClass="zebra" AutoGenerateColumns="false"
                                                                                        EmptyDataText="No Records Available" OnRowDataBound="gvStateWiseGst_RowDataBound">
                                                                                        <Columns>
                                                                                            <asp:TemplateField>
                                                                                                <HeaderTemplate>
                                                                                                    <asp:CheckBox ID="chkAllSelect" runat="server" onclick="CheckAllheader(this);" Text=" " />
                                                                                                </HeaderTemplate>
                                                                                                <HeaderStyle Width="10px" />
                                                                                                <ItemTemplate>
                                                                                                    <asp:CheckBox ID="chkSelect" runat="server" Text=" " />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="SL No.">
                                                                                                <ItemTemplate>
                                                                                                    <%#Container.DataItemIndex+1%>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle Width="2%" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="State_ID" Visible="false">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblState_ID" runat="server" Text='<%# Bind("State_ID") %>'
                                                                                                        value='<%# Eval("State_ID") %>' Visible="false"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>

                                                                                            <asp:TemplateField HeaderText="State Name" ItemStyle-Width="170px" HeaderStyle-Width="150px">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblState_Name" runat="server" Text='<%# Bind("State_Name") %>'
                                                                                                        value='<%# Eval("State_Name") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>

                                                                                            <asp:TemplateField HeaderText="GST NO" ItemStyle-Width="150px" HeaderStyle-Width="110px">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtgstnostatewise" runat="server" Text='<%# Bind("GSTNO") %>' />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>

                                                                                            <asp:TemplateField HeaderText="GST ADDRESS" ItemStyle-Width="100px" HeaderStyle-Width="100px">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtgstaddress" MaxLength="50" runat="server" Text='<%# Bind("GSTADDRESS") %>' />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>

                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </div>
                                                                                <div id="DivFooterRow" style="overflow: hidden;">
                                                                                </div>
                                                                            </div>
                                                                        </td>

                                                                    </tr>
                                                                </table>
                                                            </fieldset>
                                                        </td>
                                                    </tr>


                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title">Bank Info
                                            </td>
                                            <td align="left" class="field_input">
                                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td width="270" class="field_input">
                                                            <asp:DropDownList ID="ddlbankname" runat="server" Width="260" AppendDataBoundItems="True"
                                                                data-placeholder="Select Bank Name" class="chosen-select" />
                                                            <span class="label_intro">Bank Name</span>
                                                        </td>
                                                        <td width="270" class="field_input">
                                                            <asp:DropDownList ID="ddlbranchname" runat="server" Width="260" class="chosen-select"
                                                                data-placeholder="Select Branch Name" AppendDataBoundItems="True" />
                                                            <span class="label_intro">Bank branch</span>
                                                        </td>
                                                        <td align="left" valign="top" class="field_input">
                                                            <asp:Button ID="btnAddBranch" runat="server" ToolTip="Add Branch" CssClass="h_icon add_co"
                                                                Enabled="true" CausesValidation="false" OnClick="btnAddBranch_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:Button ID="btnRefresh1" runat="server" CssClass="h_icon arrow_refresh_co" ToolTip="Refresh"
                                                                OnClick="btnRefresh1_Click" CausesValidation="false" Enabled="true" />
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="270">
                                                            <asp:TextBox ID="txtbankacno" runat="server" Width="260"></asp:TextBox>
                                                            <span class="label_intro">
                                                                <asp:Label ID="lblbankacno" Text="Bank Account No" runat="server"></asp:Label></span>
                                                        </td>
                                                        <td width="270">
                                                            <asp:TextBox ID="txtIFSC" runat="server" Width="260"></asp:TextBox>
                                                            <span class="label_intro">
                                                                <asp:Label ID="lblIfsc" Text="IFSC Code" runat="server"></asp:Label></span>
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" height="10"></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td align="left" class="field_title">others</td>
                                            <td align="left" valign="top" class="field_input">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="90">
                                                            <asp:Label CssClass="innerfield_title" ID="lblactive" Text="ACTIVE" runat="server"></asp:Label>
                                                        </td>
                                                        <td width="50">
                                                            <asp:CheckBox ID="chkActive" runat="server" Text=" " />
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td style="padding: 8px 0px;">
                                                <div class="btn_24_blue">
                                                    <span class="icon disk_co"></span>
                                                    <asp:Button ID="btnTPUSubmit" runat="server" Text="Save" CssClass="btn_link" OnClick="btnTPUSubmit_Click"
                                                        ValidationGroup="TPUSubmit" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
                                                    <span class="icon cross_octagon_co"></span>
                                                    <asp:Button ID="btnTPUCancel" runat="server" Text="Cancel" CssClass="btn_link" OnClick="btnTPUCancel_Click"
                                                        CausesValidation="false" />
                                                </div>
                                                <asp:HiddenField ID="Hdn_Fld" runat="server" />
                                                <asp:HiddenField ID="Hdn_Supplied" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlmapping" runat="server">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                        <tr>
                                            <td width="120" class="field_title">
                                                <asp:Label ID="lblTPU" Text="VENDOR NAME" runat="server"></asp:Label><span class="req">*</span>
                                            </td>
                                            <td class="field_input">
                                                <asp:TextBox ID="txtTname" runat="server" CssClass="required large" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="trPRODUCT">
                                            <td class="field_input" style="padding-left: 10px;" colspan="2">
                                                <fieldset>
                                                    <legend>PRODUCT MAPPING</legend>
                                                    <table border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                                        <tr>
                                                            <td width="120" class="field_title">
                                                                <asp:Label ID="lblDiv" Text="Brand/Primary Item" runat="server"></asp:Label><span
                                                                    class="req">*</span>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:DropDownList ID="ddlDivision" runat="server" AppendDataBoundItems="true" class="chosen-select"
                                                                    Style="width: 250px;" data-placeholder="Choose a Brand" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"
                                                                    AutoPostBack="true">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="A"
                                                                    ForeColor="Red" ErrorMessage="Brand is required!" ControlToValidate="ddlDivision"
                                                                    ValidateEmptyText="false" SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field_title">
                                                                <asp:Label ID="lblCategory" Text="Category/Sub Item" runat="server"></asp:Label><span
                                                                    class="req">*</span>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:DropDownList ID="ddlCategory" runat="server" AppendDataBoundItems="true" class="chosen-select"
                                                                    Style="width: 250px;" data-placeholder="Choose a Category" AutoPostBack="true"
                                                                    OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="A"
                                                                    ForeColor="Red" ErrorMessage="Category is required!" ControlToValidate="ddlCategory"
                                                                    ValidateEmptyText="false" SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" style="padding: 8px 0px;">
                                                                <div class="gridcontent-short">
                                                                    <ul>
                                                                        <li class="left">
                                                                            <cc1:Grid ID="gvPRODUCTMap" runat="server" CallbackMode="false" Serialize="true"
                                                                                FolderStyle="../GridStyles/premiere_blue" AutoGenerateColumns="false" AllowPageSizeSelection="true"
                                                                                AllowPaging="false" AllowAddingRecords="false" AllowFiltering="false" AllowSorting="false"
                                                                                PageSize="200">
                                                                                <FilteringSettings InitialState="Visible" />
                                                                                <Columns>
                                                                                    <cc1:Column ID="Column3" DataField="Sl" HeaderText="SL" runat="server" Width="60">
                                                                                        <TemplateSettings TemplateId="slnoTemplate" />
                                                                                    </cc1:Column>
                                                                                    <cc1:Column ID="Column15" DataField="ID" HeaderText="PRODUCTID" runat="server" Width="60"
                                                                                        Wrap="true" Visible="false">
                                                                                    </cc1:Column>
                                                                                    <cc1:Column ID="Column4" DataField="NAME" HeaderText="PRODUCT" runat="server" Width="350"
                                                                                        Wrap="true">
                                                                                    </cc1:Column>
                                                                                    <cc1:CheckBoxSelectColumn ID="CheckBoxSelectColumn1" DataField="" ShowHeaderCheckBox="true"
                                                                                        ControlType="Obout" runat="server">
                                                                                    </cc1:CheckBoxSelectColumn>
                                                                                </Columns>
                                                                                <FilteringSettings MatchingType="AnyFilter" />
                                                                                <Templates>
                                                                                    <cc1:GridTemplate runat="server" ID="slnoTemplate">
                                                                                        <Template>
                                                                                            <asp:Label ID="lblslnoTax" runat="server" Text='<%#Container.PageRecordIndex+1 %>'></asp:Label>
                                                                                        </Template>
                                                                                    </cc1:GridTemplate>
                                                                                </Templates>
                                                                                <ScrollingSettings ScrollWidth="100%" ScrollHeight="200" />
                                                                            </cc1:Grid>
                                                                        </li>
                                                                        <li class="middle">
                                                                            <asp:Button ID="btnAddGrid" runat="server" Text=" " CausesValidation="false" CssClass="addbtn_blue"
                                                                                OnClick="btnAddGrid_Click" />
                                                                        </li>
                                                                        <li class="right">
                                                                            <cc1:Grid ID="gvProduct" runat="server" Serialize="true" AllowAddingRecords="false"
                                                                                AutoGenerateColumns="false" AllowSorting="false" AllowPageSizeSelection="false"
                                                                                FolderStyle="../GridStyles/premiere_blue" AllowPaging="false" PageSize="250">
                                                                                <Columns>
                                                                                    <cc1:Column ID="Column2" DataField="GUID" HeaderText="GUID" runat="server" Visible="false" />
                                                                                    <cc1:Column ID="Column11" DataField="Sl" HeaderText="SL" runat="server" Width="60">
                                                                                        <TemplateSettings TemplateId="slnoTemplateFinal" />
                                                                                    </cc1:Column>
                                                                                    <cc1:Column ID="Column777" DataField="DIVISIONID" HeaderText="DIVISIONID" runat="server"
                                                                                        Visible="false">
                                                                                    </cc1:Column>
                                                                                    <cc1:Column ID="Column10" DataField="DIVISIONNAME" HeaderText="DIVISION" runat="server"
                                                                                        Width="200" Wrap="true">
                                                                                    </cc1:Column>
                                                                                    <cc1:Column ID="Column8" DataField="CATEGORYID" HeaderText="CATEGORYID" runat="server"
                                                                                        Visible="false">
                                                                                    </cc1:Column>
                                                                                    <cc1:Column ID="Column9" DataField="CATEGORYNAME" HeaderText="CATEGORY" runat="server"
                                                                                        Width="200" Wrap="true">
                                                                                    </cc1:Column>
                                                                                    <cc1:Column ID="Column5" DataField="PRODUCTID" HeaderText="PRODUCTID" runat="server"
                                                                                        Visible="false">
                                                                                    </cc1:Column>
                                                                                    <cc1:Column ID="Column6" DataField="PRODUCTNAME" HeaderText="PRODUCT" runat="server"
                                                                                        Width="400" Wrap="true">
                                                                                    </cc1:Column>
                                                                                    <cc1:Column ID="Column7" DataField="VENDORID" HeaderText="VENDORID" runat="server"
                                                                                        Visible="false">
                                                                                    </cc1:Column>
                                                                                    <cc1:Column ID="Column77" DataField="VENDORNAME" HeaderText="VENDORNAME" runat="server"
                                                                                        Visible="false">
                                                                                    </cc1:Column>
                                                                                    <cc1:Column HeaderText="Delete" AllowEdit="false" AllowDelete="true" Width="80">
                                                                                        <TemplateSettings TemplateId="deleteBtnTemplate1" />
                                                                                    </cc1:Column>
                                                                                </Columns>
                                                                                <Templates>
                                                                                    <cc1:GridTemplate runat="server" ID="deleteBtnTemplate1">
                                                                                        <Template>
                                                                                            <a href="javascript: //" class="action-icons c-delete" id="btnGridDelete_<%# Container.PageRecordIndex %>"
                                                                                                onclick="CallDeleteServerMethod(this)"></a>
                                                                                        </Template>
                                                                                    </cc1:GridTemplate>
                                                                                    <cc1:GridTemplate runat="server" ID="slnoTemplateFinal">
                                                                                        <Template>
                                                                                            <asp:Label ID="lblslnoTaxFinal" runat="server" Text='<%#Container.PageRecordIndex+1 %>'></asp:Label>
                                                                                        </Template>
                                                                                    </cc1:GridTemplate>
                                                                                </Templates>
                                                                                <ScrollingSettings ScrollWidth="100%" ScrollHeight="200" />
                                                                            </cc1:Grid>
                                                                            <asp:Button ID="btngrddelete" runat="server" Text="grddelete" Style="display: none"
                                                                                OnClick="btngrddelete_Click" OnClientClick="return confirm('Are you sure you want to delete this record?');" />
                                                                            <asp:HiddenField ID="hdnProductDelete" runat="server" />
                                                                        </li>
                                                                    </ul>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="trbtnPRODUCT">
                                            <td colspan="2" class="field_input" style="padding-left: 10px;">
                                                <div class="btn_24_blue">
                                                    <span class="icon disk_co"></span>
                                                    <asp:Button ID="btnPRODUCTSave" runat="server" Text="Save" CssClass="btn_link" CausesValidation="false"
                                                        OnClick="btnPRODUCTSave_Click" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
                                                    <span class="icon cross_octagon_co"></span>
                                                    <asp:Button ID="btnPRODUCTCancel" runat="server" Text="Cancel" CssClass="btn_link"
                                                        CausesValidation="false" OnClick="btnPRODUCTCancel_Click" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlDisplay" runat="server">
                                    <tr>
                                        <td>
                                            <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                                <td width="100" class="field_title">
                                                    <asp:Label ID="Label7" Text="Supplied Type" runat="server"></asp:Label>
                                                </td>
                                                <td width="240" class="field_input">
                                                    <asp:DropDownList ID="ddltype" runat="server" AppendDataBoundItems="true" Width="198"
                                                        Height="28" class="chosen-select" data-placeholder="Choose">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <div class="btn_24_blue">
                                                        <span class="icon exclamation_co"></span>
                                                        <asp:Button ID="btnShow" runat="server" Text="Search" CssClass="btn_link" OnClick="btnShow_Click"
                                                            ValidationGroup="Search" />
                                                    </div>
                                                </td>
                                                <td class="field_input" id="tdExcel" runat="server">
                                                    <div class="btn_24_blue">
                                                        <span class="icon doc_excel_table_co"></span><a href="#" onclick="exportToExcel();">
                                                            <asp:Button ID="btnExport" runat="server" Text="Export Excel" CssClass="btn_link" /></a>
                                                    </div>
                                                </td>
                                            </table>
                                        </td>
                                    </tr>
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
                                        <cc1:Grid ID="gvTPUVendor" runat="server" CallbackMode="false" Serialize="true" FolderStyle="../GridStyles/premiere_blue"
                                            AutoGenerateColumns="false" AllowPageSizeSelection="false" OnDeleteCommand="DeleteRecord"
                                            OnExported="gvTPUVendor_Exported" OnExporting="gvTPUVendor_Exporting" AllowAddingRecords="false"
                                            AllowFiltering="true" OnRowDataBound="gvTPUVendor_RowDataBound" PageSize="500">
                                            <ExportingSettings ExportColumnsFooter="true" ExportGroupFooter="true" ExportGroupHeader="true"
                                                FileName="VendorList" ExportAllPages="true" ExportDetails="true" AppendTimeStamp="false" />
                                            <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnClientDelete="OnDelete" />
                                            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size: 10px;background-color: #DCE3E8;color: #0000FF;"
                                                CSSExportCellStyle="font-weight: normal;font-size: 10px;color: #0000;background-color: AFEEEE" />
                                            <FilteringSettings InitialState="Visible" />
                                            <Columns>
                                                <cc1:Column DataField="SLNO" ReadOnly="true" HeaderText="SLNO" runat="server" Width="80px" />

                                                <cc1:Column DataField="VENDORID" ReadOnly="true" HeaderText="VENDORID" runat="server"
                                                    Visible="false" />
                                                <cc1:Column DataField="CODE" HeaderText="PARTY CODE" runat="server" Width="100" Wrap="true">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column DataField="VENDORNAME" HeaderText="PARTY NAME" runat="server" Width="220" Wrap="true">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column DataField="COMPANY" HeaderText="COMPANY TYPE" runat="server" Width="180px" Visible="false">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column19" DataField="GSTNO" HeaderText="GST NO" runat="server" Width="130" Wrap="true">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column12" DataField="SUPLIEDITEMID" HeaderText="SUPLIEDITEMID" runat="server"
                                                    Width="120" Visible="false">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column DataField="SUPLIEDITEM" HeaderText="SUPPLIEDITEM" runat="server" Width="120" Wrap="true">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column13" DataField="ADDRESS" HeaderText="ADDRESS" runat="server" Visible="false"
                                                    Width="140">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column14" DataField="PINZIP" HeaderText="PIN/ZIP" runat="server" Visible="false"
                                                    Width="80">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column16" DataField="STATENAME" HeaderText="STATE" runat="server"
                                                    Width="100" Wrap="true">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column17" DataField="DISTRICTNAME" HeaderText="DISTRICT" runat="server"
                                                    Width="100" Wrap="true">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column18" DataField="CITYNAME" HeaderText="CITY" runat="server" Wrap="true"
                                                    Width="100">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column DataField="PANNO" HeaderText="PAN NO." runat="server" Width="120" Wrap="true">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column DataField="TINNO" HeaderText="TIN NO." runat="server" Width="120" Visible="false">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column DataField="MOBILENO" HeaderText="MOBILENO" runat="server" Width="100" Visible="false">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column DataField="EMAILID" HeaderText="EMAIL ID" runat="server" Width="160" Visible="false">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column DataField="ACCGRPNAME" HeaderText="ACCGRPNAME" runat="server" Width="160" Visible="false">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column DataField="ACTIVE_STATUS" HeaderText="STATUS" runat="server" Width="120">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column1" DataField="TAG" HeaderText="TAG" runat="server" Width="50"
                                                    Visible="false">
                                                </cc1:Column>

                                                <cc1:Column AllowEdit="true" AllowDelete="true" HeaderText="EDIT" runat="server"
                                                    Width="70">
                                                    <TemplateSettings TemplateId="editBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column HeaderText="DELETE" AllowEdit="false" AllowDelete="true" Width="70" Visible="false">
                                                    <TemplateSettings TemplateId="deleteBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column HeaderText="PRODUCT MAPPING" AllowEdit="false" AllowDelete="true" Width="100" Visible="false"
                                                    Wrap="true">
                                                    <TemplateSettings TemplateId="MappingTemplate" />
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
                                                        <a href="javascript: //" class="action-icons c-delete" title="Delete" onclick="gvTPUVendor.delete_record(this)"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="MappingTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="h_icon map" title="TPU Mapping" onclick="openMapping('<%# Container.DataItem["VENDORID"] %>','<%# Container.DataItem["VENDORNAME"] %>','<%# Container.DataItem["TAG"] %>','<%# Container.DataItem["SUPLIEDITEMID"] %>')" />
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <ScrollingSettings ScrollWidth="100%" ScrollHeight="290" />
                                        </cc1:Grid>
                                        <asp:HiddenField ID="hdnTid" runat="server" />
                                        <asp:HiddenField ID="hdnTag" runat="server" />
                                        <asp:Button ID="btngridedit" runat="server" Text="Gridedit" CausesValidation="false"
                                            Style="display: none" OnClick="btngridedit_Click" />
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
                function panno() {
                    var checkedValue = document.getElementById("<%= chkgst.ClientID%>");
                    if (checkedValue.checked == true) {
                        var panno = document.getElementById("<%=txtpanno.ClientID %>").value;
                        document.getElementById("<%=txtgstpanno.ClientID %>").value = panno;
                    }
                }
            </script>
            <script type="text/javascript">
                function exportToExcel() {
                    gvTPUVendor.exportToExcel();
                }
            </script>
            <script type="text/javascript">
                function OnBeforeDelete(record) {
                    record.Error = '';
                    document.getElementById("<%=Hdn_Fld.ClientID %>").value = record.VENDORID;
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
                function isLetterNumberKey3D() {                 
                      var txtgstno1 = "ContentPlaceHolder1_txtgstno";
                      var  txtgstno=document.getElementById(txtgstno1).value;
                    var txtgstnoL = txtgstno.length;                   
                    for (var i = 0; i < txtgstnoL; i++) {                    
                        if (i == 0) {
                            var n = txtgstno.charCodeAt(0);
                        if (n < 48 || n > 57) {
                            alert('Enter Only Number for 1st digit');
                           document.getElementById(txtgstno1).value = "";
                        }
                    }
                    if (i == 1) {
                        var n1 = txtgstno.charCodeAt(1);                      
                        if ((n1 >= 65 && n1 <= 90) || (n1 >= 97 && n1 <= 122)) {
                        }
                        else {
                             alert('Enter Only Letter for 2nd digit');
                            document.getElementById(txtgstno1).value = "";
                        }
                    }
                     if (i == 2) {
                        var n2 = txtgstno.charCodeAt(2);
                         if ((n2 >= 48 && n2 <= 57) || (n2 >= 65 && n2 <= 90) || (n2 >= 97 && n2 <= 122)) {
                         }
                         else {
                              alert('Enter only Number or letter for 3rd digit');
                             document.getElementById(txtgstno1).value = "";
                         }
                    }
                }
                 return true;
                };
           </script>
            <script type="text/javascript">
                function CallServerMethod(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnGridEdit_", "");
                    document.getElementById("<%=hdnTid.ClientID %>").value = "E";
                    document.getElementById("<%=Hdn_Fld.ClientID %>").value = gvTPUVendor.Rows[iRecordIndex].Cells[1].Value;
                    document.getElementById("<%=btngridedit.ClientID %>").click();
                }

                function openMapping(vendorID, vendorName, Tag, Supplied) {

                    document.getElementById("<%=Hdn_Fld.ClientID %>").value = vendorID;
                    document.getElementById("<%=Hdn_Supplied.ClientID %>").value = Supplied;
                    document.getElementById("<%=hdnTid.ClientID %>").value = "M";
                    document.getElementById("<%=hdnTag.ClientID %>").value = Tag;
                    document.getElementById("<%=txtTname.ClientID %>").value = vendorName;
                    document.getElementById("<%=btngridedit.ClientID %>").click();
                }


            </script>
            <script type="text/javascript">
                function CallDeleteServerMethod(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnGridDelete_", "");
                    document.getElementById("<%=hdnProductDelete.ClientID %>").value = gvProduct.Rows[iRecordIndex].Cells[0].Value;
                    document.getElementById("<%=btngrddelete.ClientID %>").click();

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
                    gvTPUVendor.addFilterCriteria('CODE', OboutGridFilterCriteria.Contains, searchValue);
                    gvTPUVendor.addFilterCriteria('VENDORNAME', OboutGridFilterCriteria.Contains, searchValue);
                    gvTPUVendor.addFilterCriteria('SUPLIEDITEM', OboutGridFilterCriteria.Contains, searchValue);
                    gvTPUVendor.executeFilter();
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
            <script type="text/javascript">   
             function isLetterNumberKey(evt) {
                         var keyCode = (evt.which) ? evt.which : event.keyCode;
                         return ((keyCode >= 48 && keyCode <= 57)||(keyCode >= 65 && keyCode <= 90)||(keyCode >= 97 && keyCode <= 122))

                        return true;                     
                };
                </script>

            <%--<script type="text/javascript">        
                $(function () {
                    $('#ContentPlaceHolder1_ddlTpumap').multiselect({
                        includeSelectAllOption: true
                    });                    
                });
            </script>

            <script type="text/javascript">        
                $(function () {
                    $('#ContentPlaceHolder1_ddlgroup').multiselect({
                        includeSelectAllOption: true
                    });
                });
            </script>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

